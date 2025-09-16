// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.BqlSoapCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Common.Extensions;
using PX.Data.Description.GI;
using PX.Data.SQLTree;
using PX.Reports;
using PX.Reports.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.Reports;

/// <summary>
/// A class, which is derived from the <see cref="T:PX.Data.BqlCommand" /> class, that is used by the system during the processing of reports.
/// The main purpose of <tt>BqlCommand</tt> classes is to convert BQL commands to SQL text.
/// The <tt>BqlSoapCommand</tt> class works with report schema to get the resulting SQL query tree.
/// </summary>
internal sealed class BqlSoapCommand : BqlCommand
{
  private readonly ReportSelectArguments _arguments;
  internal Dictionary<string, System.Type> _tablemap;
  public object IncomingRow;
  public System.Type IncomingRowType;
  private System.Type ReplacedIncomingRowType;
  private const int DummyParameterNumber = -1;
  internal List<PXDataRecordMap.FieldEntry> FieldMap = new List<PXDataRecordMap.FieldEntry>();
  private int FieldsCount;
  private readonly Dictionary<string, HashSet<string>> _fieldInTables = new Dictionary<string, HashSet<string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  internal List<BqlSoapCommand.RowSelectingInfo> RowSelectingHandlers = new List<BqlSoapCommand.RowSelectingInfo>();
  private static IPXRowSelectingSubscriber rowSelectingCrutch = (IPXRowSelectingSubscriber) new BqlSoapCommand.shift1Rowselecting();

  public static SQLExpression ConditionToSqlExpression(
    FilterCondition condition,
    SQLExpression left,
    SQLExpression right)
  {
    switch ((int) condition)
    {
      case 0:
        return SQLExpressionExt.EQ(left, right);
      case 1:
        return SQLExpressionExt.NE(left, right);
      case 2:
        return left.GT(right);
      case 3:
        return left.GE(right);
      case 4:
        return left.LT(right);
      case 5:
        return left.LE(right);
      case 6:
      case 7:
      case 8:
        return left.Like(right);
      case 9:
        return left.NotLike(right);
      case 10:
      case 13:
      case 14:
      case 15:
      case 16 /*0x10*/:
      case 17:
      case 18:
      case 19:
      case 20:
        return left.Between(right);
      case 11:
        return left.IsNull();
      case 12:
        return left.IsNotNull();
      case 21:
        return left.In(right);
      case 22:
        return left.In(right);
      default:
        throw new InvalidOperationException(condition.ToString() + " filter can not be processed in BqlReportFilter");
    }
  }

  public static bool isUnary(FilterCondition fCondition) => fCondition == 12 || fCondition == 11;

  public static bool isDateTimeOperator(FilterCondition fCondition)
  {
    return fCondition == 13 || fCondition == 14 || fCondition == 15 || fCondition == 16 /*0x10*/ || fCondition == 17 || fCondition == 18 || fCondition == 19 || fCondition == 20;
  }

  public BqlSoapCommand(PXGraph graph, ReportSelectArguments args)
  {
    if (args == null || args.Tables == null)
      return;
    this._arguments = args;
    this._tablemap = new Dictionary<string, System.Type>();
    foreach (ReportTable table in (List<ReportTable>) args.Tables)
    {
      System.Type type = !string.IsNullOrEmpty(table.Name) ? ServiceManager.ReportNameResolver.ResolveTable(table) : throw new Exception("Table name cannot be empty.");
      if (type == (System.Type) null)
        this.throwTable(table.Name);
      this._tablemap[table.Name] = type;
    }
  }

  private string removePref(string rowName)
  {
    return !rowName.StartsWith("Row") ? rowName : rowName.Substring(3);
  }

  internal void throwTable(string tablename)
  {
    throw new Exception($"The table {tablename} does not exist.");
  }

  internal Tuple<System.Type, string, System.Type> findBqlField(PXGraph graph, ref string name)
  {
    Tuple<System.Type, string, System.Type> bqlField = this.tryFindBqlField(graph, ref name);
    if (bqlField != null)
      return bqlField;
    int num = name.IndexOf('.');
    if (num == name.Length - 1)
      throw new Exception("An empty field name has been specified.");
    this.throwTable(name);
    throw new Exception($"An invalid field name {(num < 0 ? (object) name : (object) name.Substring(num + 1))} has been specified.");
  }

  internal Tuple<System.Type, string, System.Type> tryFindBqlField(PXGraph graph, ref string name)
  {
    string fname = "";
    PXCache cache = this.tryFindCache(graph, ref name, ref fname);
    if (cache == null)
      return (Tuple<System.Type, string, System.Type>) null;
    System.Type field = (System.Type) null;
    List<System.Type> bqlFields = cache.BqlFields;
    for (int index = bqlFields.Count - 1; index >= 0; --index)
    {
      if (string.Equals(bqlFields[index].Name, fname, StringComparison.OrdinalIgnoreCase))
        field = bqlFields[index];
    }
    if (field != (System.Type) null)
      return new Tuple<System.Type, string, System.Type>(BqlCommand.GetItemType(field), field.Name, field);
    return cache.Fields.Contains(fname) ? new Tuple<System.Type, string, System.Type>(cache.GetItemType(), fname, (System.Type) null) : (Tuple<System.Type, string, System.Type>) null;
  }

  internal PXCache tryFindCache(PXGraph graph, ref string name, ref string fname)
  {
    int length = name.IndexOf('.');
    System.Type key;
    if (length == -1)
    {
      key = this._tablemap[((List<ReportTable>) this._arguments.Tables)[0].Name];
      fname = name;
      name = ((List<ReportTable>) this._arguments.Tables)[0].Name;
    }
    else
    {
      if (length == name.Length - 1)
        return (PXCache) null;
      fname = name.Substring(length + 1);
      name = name.Substring(0, length);
      if (!this._tablemap.TryGetValue(name, out key))
      {
        foreach (ReportRelation relation in (CollectionBase) this._arguments.Relations)
        {
          if (string.Equals(relation.ParentAlias, name, StringComparison.OrdinalIgnoreCase))
          {
            if (!this._tablemap.TryGetValue(relation.ParentName, out key))
              return (PXCache) null;
            break;
          }
          if (string.Equals(relation.ChildAlias, name, StringComparison.OrdinalIgnoreCase))
          {
            if (!this._tablemap.TryGetValue(relation.ChildName, out key))
              return (PXCache) null;
            break;
          }
        }
        if (key == (System.Type) null)
          return (PXCache) null;
      }
    }
    return graph.Caches[key];
  }

  internal void IndexReportFields(Dictionary<string, PXCache> cacheMap)
  {
    string[] reportFields = this._arguments.ReportFields;
    if (reportFields == null)
      return;
    foreach (string source1 in reportFields)
    {
      if (source1.Contains<char>('.'))
      {
        string[] source2;
        if (!source1.StartsWith("="))
          source2 = source1.Split('.');
        else
          source2 = source1.Substring(2, source1.Length - 3).Split('.');
        string key1 = ((IEnumerable<string>) source2).First<string>();
        string str = ((IEnumerable<string>) source2).Last<string>();
        PXCache table;
        if (cacheMap.TryGetValue(key1, out table))
        {
          string key2 = table._ReportGetFirstKeyValueStored(str) ?? table._ReportGetFirstKeyValueAttribute(str);
          if (key2 != null)
          {
            this._fieldInTables.Ensure<string, HashSet<string>>(key2, (Func<HashSet<string>>) (() => new HashSet<string>())).Add(table.GetItemType().FullName);
          }
          else
          {
            foreach (string key3 in PXDependsOnFieldsAttribute.GetDependsRecursive(table, str))
              this._fieldInTables.Ensure<string, HashSet<string>>(key3, (Func<HashSet<string>>) (() => new HashSet<string>())).Add(table.GetItemType().FullName);
            if (str.Contains<char>('_'))
            {
              if (str.EndsWith("_Attributes") && str.Length >= 12 && str[str.Length - 12] != '_')
              {
                this._fieldInTables.Ensure<string, HashSet<string>>(str, (Func<HashSet<string>>) (() => new HashSet<string>())).Add(table.GetItemType().FullName);
                this._fieldInTables.Ensure<string, HashSet<string>>(StringExtensions.LastSegment(str, '_'), (Func<HashSet<string>>) (() => new HashSet<string>())).Add(table.GetItemType().FullName);
              }
              else
                this._fieldInTables.Ensure<string, HashSet<string>>(StringExtensions.FirstSegment(str, '_'), (Func<HashSet<string>>) (() => new HashSet<string>())).Add(table.GetItemType().FullName);
            }
          }
        }
      }
    }
    HashSet<System.Type> usedTables = new HashSet<System.Type>(cacheMap.Values.Select<PXCache, System.Type>((Func<PXCache, System.Type>) (c => c.GetItemType())));
    foreach (PXCache cache in cacheMap.Values)
    {
      foreach (string specialField in BqlSoapCommand.GetSpecialFields(cache.GetItemType()))
        this._fieldInTables.Ensure<string, HashSet<string>>(specialField, (Func<HashSet<string>>) (() => new HashSet<string>())).Add(cache.GetItemType().FullName);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      foreach (System.Type field in PXPrimaryGraphAttribute.GetAttributes(cache).OfType<PXPrimaryGraphAttribute>().SelectMany<PXPrimaryGraphAttribute, System.Type>((Func<PXPrimaryGraphAttribute, IEnumerable<System.Type>>) (a => a.GetConditions())).SelectMany<System.Type, System.Type>(BqlSoapCommand.\u003C\u003EO.\u003C0\u003E__GetFields ?? (BqlSoapCommand.\u003C\u003EO.\u003C0\u003E__GetFields = new Func<System.Type, IEnumerable<System.Type>>(BqlCommand.GetFields))).Where<System.Type>((Func<System.Type, bool>) (f => usedTables.Contains(BqlCommand.GetItemType(f)))).Distinct<System.Type>())
        this._fieldInTables.Ensure<string, HashSet<string>>(field.Name, (Func<HashSet<string>>) (() => new HashSet<string>())).Add(BqlCommand.GetItemType(field).FullName);
    }
  }

  private static IEnumerable<string> GetSpecialFields(System.Type table)
  {
    HashSet<string> specialFields = new HashSet<string>();
    foreach (PropertyInfo property in table.GetProperties())
    {
      if (((IEnumerable<object>) property.GetCustomAttributes(typeof (IPXReportRequiredField), true)).Any<object>())
        specialFields.Add(property.Name);
    }
    return (IEnumerable<string>) specialFields;
  }

  internal bool IsInReport(PXCache cache, string field)
  {
    if (this._fieldInTables.Count == 0 || cache.Keys.Contains(field))
      return true;
    HashSet<string> stringSet;
    return this._fieldInTables.TryGetValue(field, out stringSet) && stringSet.Contains(cache.GetItemType().FullName);
  }

  private static List<T> GetAllScusribers<T>(PXCache cache) where T : class
  {
    List<T> subscribers = new List<T>();
    foreach (string field in (List<string>) cache.Fields)
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(field, false))
        subscriberAttribute.GetSubscriber<T>(subscribers);
    }
    return subscribers;
  }

  private static List<T> GetSubscribers<T>(
    PXCache cache,
    List<T> cacheSubscribers,
    string field,
    T defT = null)
    where T : class
  {
    List<T> list = cacheSubscribers.Where<T>((Func<T, bool>) (rs => rs is PXEventSubscriberAttribute subscriberAttribute && string.Equals(subscriberAttribute.FieldName, field, StringComparison.OrdinalIgnoreCase))).ToList<T>();
    List<System.Type> extensionTables = cache.GetExtensionTables();
    if ((object) defT != null && !list.Any<T>() && extensionTables != null && extensionTables.Any<System.Type>((Func<System.Type, bool>) (t => field.StartsWith(t.Name + "_"))))
      list.Add(defT);
    return list.Count == 0 ? (List<T>) null : list;
  }

  private ReportParameter DetermineParameter(ReportSelectArguments args, string value)
  {
    if (value == null)
      return (ReportParameter) null;
    if (value.StartsWith("@") && value.Length > 2)
    {
      string str = value.Substring(1, value.Length - 1);
      if (args.Parameters.Contains(str))
        return args.Parameters[str];
    }
    return (ReportParameter) null;
  }

  private IPXValue ParamHandler(string name) => (IPXValue) new PXFieldValue("dummy.field");

  internal IPXValue GetValue(string formulaText)
  {
    if (formulaText.StartsWith("@"))
      formulaText = "=" + formulaText;
    IPXValue ipxValue = (IPXValue) null;
    if (!formulaText.StartsWith("="))
    {
      ReportParameter parameter = this.DetermineParameter(this._arguments, formulaText);
      if (parameter != null)
        ipxValue = (IPXValue) new PXSimpleValue(parameter.Value);
      if (ipxValue == null)
      {
        string fieldName = formulaText;
        string[] strArray = fieldName.Split('.');
        ipxValue = strArray.Length > 1 ? (IPXValue) new PXFieldValue(strArray[0], strArray[1]) : (IPXValue) new PXFieldValue("", fieldName);
      }
    }
    if (ipxValue == null)
    {
      string str = formulaText;
      PXGIFormulaProcessor processor = new PXGIFormulaProcessor();
      if (!processor.ContainsFields(str))
        ipxValue = (IPXValue) new PXSimpleValue(processor.Evaluate(str, (SyFormulaFinalDelegate) null));
      if (ipxValue == null)
        ipxValue = (IPXValue) new PXCalcedValue(str, processor);
    }
    return ipxValue;
  }

  private Query BuildQuery(PXGraph graph, List<SQLExpression> selection)
  {
    Query query = this.CreateQuery(graph);
    this.FieldsCount = 0;
    this.RowSelectingHandlers.Clear();
    byte[] referencedValue = GroupHelper.GetReferencedValue(graph.Caches[typeof (AccessInfo)], (object) graph.Accessinfo, "UserName", (object) graph.Accessinfo.UserName, graph._ForceUnattended) as byte[];
    List<KeyValuePair<System.Type, string>> maskedTypes = new List<KeyValuePair<System.Type, string>>();
    if (((CollectionBase) this._arguments.Relations).Count > 0)
    {
      ReportRelation relation1 = this._arguments.Relations[0];
      string parentName = relation1.ParentName;
      System.Type type1;
      if (!this._tablemap.TryGetValue(parentName, out type1))
        this.throwTable(parentName);
      PXCache cach1 = graph.Caches[type1];
      this.InsertFields(type1, cach1, relation1.ParentAlias, selection);
      if (GroupHelper.IsRestricted(typeof (Users), type1))
        maskedTypes.Add(new KeyValuePair<System.Type, string>(type1, string.IsNullOrEmpty(relation1.ParentAlias) ? type1.Name : relation1.ParentAlias));
      foreach (ReportRelation relation2 in (CollectionBase) this._arguments.Relations)
      {
        string childName = relation2.ChildName;
        System.Type type2;
        if (!this._tablemap.TryGetValue(childName, out type2))
          this.throwTable(childName);
        PXCache cach2 = graph.Caches[type2];
        this.InsertFields(type2, cach2, relation2.ChildAlias, selection);
        if (GroupHelper.IsRestricted(typeof (Users), type2))
          maskedTypes.Add(new KeyValuePair<System.Type, string>(type2, string.IsNullOrEmpty(relation2.ChildAlias) ? type2.Name : relation2.ChildAlias));
      }
    }
    else
    {
      string name = ((List<ReportTable>) this._arguments.Tables)[0].Name;
      System.Type type;
      if (!this._tablemap.TryGetValue(name, out type))
        this.throwTable(name);
      PXCache cach = graph.Caches[type];
      this.InsertFields(type, cach, (string) null, selection);
      if (GroupHelper.IsRestricted(typeof (Users), type))
        maskedTypes.Add(new KeyValuePair<System.Type, string>(type, type.Name));
    }
    int iParam = 0;
    bool flag = false;
    IEnumerable<Joiner> joiners = (IEnumerable<Joiner>) null;
    System.Type type3;
    string alias;
    if (((CollectionBase) this._arguments.Relations).Count > 0)
    {
      ReportRelation relation = this._arguments.Relations[0];
      type3 = this._tablemap[relation.ParentName];
      if (this.ReplacedIncomingRowType == (System.Type) null && this.IncomingRowType != (System.Type) null && (type3 == this.IncomingRowType || type3.IsAssignableFrom(this.IncomingRowType) || this.IncomingRowType.IsAssignableFrom(type3)))
      {
        this.ReplacedIncomingRowType = type3;
        flag = true;
      }
      joiners = this.BuildJoinsExpression(query, graph, ref iParam);
      alias = string.IsNullOrEmpty(relation.ParentAlias) ? type3.Name : relation.ParentAlias;
    }
    else
    {
      type3 = this._tablemap[((List<ReportTable>) this._arguments.Tables)[0].Name];
      alias = type3.Name;
    }
    SQLExpression w = this.BuildWhereExpression(graph, ref iParam, (IEnumerable<KeyValuePair<System.Type, string>>) maskedTypes, referencedValue);
    if (flag)
      this.ReplacedIncomingRowType = (System.Type) null;
    Table t = this.BuildSqlTable(type3, graph).As(alias);
    query.From(t);
    if (joiners != null)
      EnumerableExtensions.ForEach<Joiner>(joiners, (System.Action<Joiner>) (j => query.AddJoin(j)));
    query.Where(w);
    System.Type type4 = (System.Type) null;
    try
    {
      type4 = this.IncomingRowType;
      this.IncomingRowType = (System.Type) null;
      List<OrderSegment> source = new List<OrderSegment>();
      foreach (GroupExp groupExp in this._arguments.Groups.SelectMany<GroupExpCollection, GroupExp>((Func<GroupExpCollection, IEnumerable<GroupExp>>) (col => (IEnumerable<GroupExp>) col)))
      {
        string dataField = groupExp.DataField;
        SQLExpression sf;
        if (SoapNavigator.IsFormula(dataField))
        {
          sf = this.GetValue(dataField).GetExpression((Func<string, SQLExpression>) (name => this.ParamHandlerFilterExpression(name, ref iParam, graph, out bool _))).Embrace();
        }
        else
        {
          Tuple<System.Type, string, System.Type> bqlField = this.tryFindBqlField(graph, ref dataField);
          sf = this.GetFieldExpression(bqlField.Item1, graph.Caches[bqlField.Item1], bqlField.Item2, this.removePref(dataField), BqlCommand.FieldPlace.Condition);
        }
        if (sf != null && !source.Any<OrderSegment>((Func<OrderSegment, bool>) (os => os.expr_.Equals(sf))))
          source.Add(new OrderSegment(sf, groupExp.SortOrder == 0));
      }
      foreach (SortExp sortExp in (List<SortExp>) this._arguments.Sorting)
      {
        string dataField = sortExp.DataField;
        Tuple<System.Type, string, System.Type> bqlField = this.tryFindBqlField(graph, ref dataField);
        SQLExpression sf = this.GetFieldExpression(bqlField.Item1, graph.Caches[bqlField.Item1], bqlField.Item2, this.removePref(dataField), BqlCommand.FieldPlace.OrderBy);
        if (!source.Any<OrderSegment>((Func<OrderSegment, bool>) (os => os.expr_.Equals(sf))))
          source.Add(new OrderSegment(sf, sortExp.SortOrder == 0));
      }
      source.ForEach((System.Action<OrderSegment>) (os => query.AddOrderSegment(os)));
    }
    finally
    {
      this.IncomingRowType = type4;
    }
    query.Select(selection.ToArray());
    return query;
  }

  /// <summary>
  /// Optimization reports, insert only those fields that are used in the report.
  /// </summary>
  public void InsertFields(System.Type table, PXCache cache, string alias, List<SQLExpression> selection)
  {
    if (string.IsNullOrEmpty(alias))
      alias = cache.GetItemType().Name;
    BqlSoapCommand.RowSelectingInfo rowSelectingInfo = new BqlSoapCommand.RowSelectingInfo(cache);
    this.RowSelectingHandlers.Add(rowSelectingInfo);
    List<IPXRowSelectingSubscriber> allScusribers = BqlSoapCommand.GetAllScusribers<IPXRowSelectingSubscriber>(cache);
    BqlSoapCommand.FInfo[] array1 = cache.Fields.Select<string, BqlSoapCommand.FInfo>((Func<string, BqlSoapCommand.FInfo>) (it =>
    {
      BqlSoapCommand.FInfo finfo1 = new BqlSoapCommand.FInfo();
      finfo1.FieldName = it;
      finfo1.Expr = BqlSoapCommand.GetFieldExprInternal(table, cache, it, alias, BqlCommand.FieldPlace.Select);
      finfo1.InReport = this.IsInReport(cache, it);
      BqlSoapCommand.FInfo finfo2 = finfo1;
      List<IPXRowSelectingSubscriber> selectingSubscriberList;
      if (!string.Equals(cache._ReportGetFirstKeyValueStored(it), it, StringComparison.OrdinalIgnoreCase))
      {
        if (!string.Equals(cache._ReportGetFirstKeyValueAttribute(it), it, StringComparison.OrdinalIgnoreCase))
          selectingSubscriberList = BqlSoapCommand.GetSubscribers<IPXRowSelectingSubscriber>(cache, allScusribers, it, BqlSoapCommand.rowSelectingCrutch);
        else
          selectingSubscriberList = new List<IPXRowSelectingSubscriber>((IEnumerable<IPXRowSelectingSubscriber>) new IPXRowSelectingSubscriber[1]
          {
            (IPXRowSelectingSubscriber) new BqlSoapCommand.fetchKeyValueAttributes(cache)
          });
      }
      else
        selectingSubscriberList = new List<IPXRowSelectingSubscriber>((IEnumerable<IPXRowSelectingSubscriber>) new IPXRowSelectingSubscriber[1]
        {
          (IPXRowSelectingSubscriber) new BqlSoapCommand.fetchKeyValuePairs(cache)
        });
      finfo2.RowSelecting = selectingSubscriberList;
      finfo1.IsKey = cache.Keys.Contains(it);
      return finfo1;
    })).Where<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (it => it.Expr != null)).ToArray<BqlSoapCommand.FInfo>();
    if (!((IEnumerable<BqlSoapCommand.FInfo>) array1).Any<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (it => it.InReport)))
      return;
    BqlSoapCommand.FInfo finfo3 = (BqlSoapCommand.FInfo) null;
    foreach (BqlSoapCommand.FInfo finfo4 in array1)
    {
      if (finfo4.RowSelecting != null)
        finfo3 = finfo4;
      finfo3.Range.Add(finfo4);
    }
    BqlSoapCommand.FInfo[] array2 = ((IEnumerable<BqlSoapCommand.FInfo>) array1).Where<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (it => it.IsKey)).Union<BqlSoapCommand.FInfo>(((IEnumerable<BqlSoapCommand.FInfo>) array1).Where<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (it => it.RowSelecting != null))).ToArray<BqlSoapCommand.FInfo>();
    int count = cache.Keys.Count;
    rowSelectingInfo.KeysCount = count;
    BqlSoapCommand.FInfo[] array3 = ((IEnumerable<BqlSoapCommand.FInfo>) array2).Where<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (it => it.IsKey || it.Range.Any<BqlSoapCommand.FInfo>((Func<BqlSoapCommand.FInfo, bool>) (r => r.InReport)))).ToArray<BqlSoapCommand.FInfo>();
    for (int index = 0; index < array3.Length; ++index)
    {
      BqlSoapCommand.FInfo finfo5 = array3[index];
      BqlSoapCommand.RowSelectingFieldInfo selectingFieldInfo = new BqlSoapCommand.RowSelectingFieldInfo()
      {
        Pos = this.FieldsCount,
        Field = finfo5.FieldName,
        IsLastKey = index + 1 == count,
        RowSelectingHandlers = finfo5.RowSelecting
      };
      rowSelectingInfo.Fields.Add(selectingFieldInfo);
      ++this.FieldsCount;
      selectingFieldInfo.Text = finfo5.Range.Select<BqlSoapCommand.FInfo, string>((Func<BqlSoapCommand.FInfo, string>) (it => it.FieldName)).JoinToString<string>(",");
      foreach (BqlSoapCommand.FInfo finfo6 in finfo5.Range)
        selection.Add(finfo6.Expr);
    }
  }

  private IEnumerable<Joiner> BuildJoinsExpression(Query query, PXGraph graph, ref int iParam)
  {
    List<Joiner> joinerList = new List<Joiner>();
    foreach (ReportRelation relation in (CollectionBase) this._arguments.Relations)
    {
      Joiner.JoinType jt = Joiner.JoinType.INNER_JOIN;
      switch ((int) relation.JoinType)
      {
        case 0:
          jt = Joiner.JoinType.LEFT_JOIN;
          break;
        case 1:
          jt = Joiner.JoinType.RIGHT_JOIN;
          break;
        case 2:
          jt = Joiner.JoinType.INNER_JOIN;
          break;
        case 3:
          jt = Joiner.JoinType.FULL_JOIN;
          break;
        case 4:
          jt = Joiner.JoinType.CROSS_JOIN;
          break;
      }
      System.Type table = this._tablemap[relation.ChildName];
      SQLExpression e = this.BuildJoinCondition(relation, graph, ref iParam);
      Table t = this.BuildSqlTable(table, graph).As(!string.IsNullOrEmpty(relation.ChildAlias) ? relation.ChildAlias : table.Name);
      Joiner joiner = new Joiner(jt, t, query);
      if (relation.JoinType != 4)
        joiner.On(e);
      joinerList.Add(joiner);
    }
    return (IEnumerable<Joiner>) joinerList;
  }

  private SQLExpression BuildWhereExpression(
    PXGraph graph,
    ref int zParam,
    IEnumerable<KeyValuePair<System.Type, string>> maskedTypes,
    byte[] mask)
  {
    int iParam = zParam;
    int val1 = 0;
    List<(SQLExpression, FilterOperator)> bracketRegion = new List<(SQLExpression, FilterOperator)>();
    Stack<List<(SQLExpression, FilterOperator)>> valueTupleListStack = new Stack<List<(SQLExpression, FilterOperator)>>();
    valueTupleListStack.Push(bracketRegion);
    for (int index1 = 0; index1 < ((List<FilterExp>) this._arguments.Filters).Count; ++index1)
    {
      FilterExp filter = ((List<FilterExp>) this._arguments.Filters)[index1];
      bool bypassCondition = false;
      if (filter.OpenBraces > 0)
      {
        val1 += filter.OpenBraces;
        for (int index2 = 0; index2 < filter.OpenBraces; ++index2)
          valueTupleListStack.Push(new List<(SQLExpression, FilterOperator)>());
      }
      IPXValue ipxValue = !string.IsNullOrEmpty(filter.DataField) ? this.GetValue(filter.DataField) : throw new Exception("An empty filter field has been specified.");
      SQLExpression left = ipxValue.GetExpression((Func<string, SQLExpression>) (name => this.ParamHandlerFilterExpression(name, ref iParam, graph, out bypassCondition)));
      if (filter.DataField.StartsWith("="))
        left.Embrace();
      List<SQLExpression> res = new List<SQLExpression>();
      left.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is Literal literal && literal.Number == -1), res);
      object[] parameters = ipxValue.GetParameters(new Func<string, IPXValue>(this.ParamHandler));
      int num1 = System.Math.Min(res.Count, parameters.Length);
      for (int index3 = 0; index3 < num1; ++index3)
        left = left.substituteNode(res[index3], (SQLExpression) new SQLConst(parameters[index3]));
      SQLExpression sqlExpression = BqlSoapCommand.isDateTimeOperator(filter.Condition) || BqlSoapCommand.isUnary(filter.Condition) ? (SQLExpression) null : this.CreateParameterExpression(graph, filter.Value, ref iParam);
      if (filter.Condition == 21)
        sqlExpression = sqlExpression.InsideBranch();
      FilterCondition condition = filter.Condition;
      if (condition != 10)
      {
        if (condition - 13 <= 7)
          sqlExpression = SQLExpression.BetweenArgs((SQLExpression) Literal.NewParameter(iParam++), (SQLExpression) Literal.NewParameter(iParam++));
      }
      else
        sqlExpression = SQLExpression.BetweenArgs(sqlExpression, this.CreateParameterExpression(graph, filter.Value2, ref iParam));
      SQLExpression r = BqlSoapCommand.ConditionToSqlExpression(filter.Condition, left, sqlExpression);
      if (bypassCondition)
        r = SQLExpressionExt.EQ(new SQLConst((object) 1), (SQLExpression) new SQLConst((object) 1)).Or(r).Embrace();
      FilterOperator filterOperator = index1 > 0 ? ((List<FilterExp>) this._arguments.Filters)[index1 - 1].Operator : (FilterOperator) (object) 0;
      valueTupleListStack.Peek().Add((r, filterOperator));
      int num2 = System.Math.Min(val1, filter.CloseBraces);
      if (filter.CloseBraces > 0 && val1 > 0)
      {
        if (valueTupleListStack.Count < num2 + 1)
          throw new PXException("Brackets in the Where condition are not balanced.");
        for (int index4 = 0; index4 < num2; ++index4)
        {
          List<(SQLExpression, FilterOperator)> valueTupleList = valueTupleListStack.Pop();
          SQLExpression expression = this.GetExpression(valueTupleList);
          if (expression != null)
            valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, FilterOperator)>().Item2));
        }
        val1 -= num2;
      }
    }
    while (valueTupleListStack.Count > 1)
    {
      List<(SQLExpression, FilterOperator)> valueTupleList = valueTupleListStack.Pop();
      SQLExpression expression = this.GetExpression(valueTupleList);
      if (expression != null)
        valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, FilterOperator)>().Item2));
    }
    SQLExpression sqlExpression1 = this.GetExpression(bracketRegion);
    SQLExpression r1 = (SQLExpression) null;
    foreach (KeyValuePair<System.Type, string> maskedType in maskedTypes)
    {
      int pos = -3;
      bool flag = true;
      SQLExpression r2 = SQLExpression.None();
      SQLExpression r3 = SQLExpression.None();
      Column column = new Column("GroupMask", maskedType.Value);
      foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(typeof (Users), maskedType.Key, mask))
      {
        pos += 4;
        if (paramsPair.First != 0 || paramsPair.Second != 0)
        {
          flag = false;
          SQLExpression r4 = SQLExpressionExt.EQ(new SQLConst((object) 0), column.ConvertBinToInt((uint) pos, 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.First)));
          SQLExpression r5 = SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) pos, 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.Second)));
          r2 = r2.And(r4);
          r3 = r3.Or(r5);
        }
      }
      if (!flag)
      {
        SQLExpression r6 = column.IsNull().Or(r2).Or(r3).Embrace();
        r1 = r1 == null ? r6 : r1.And(r6);
      }
    }
    if (r1 != null)
      sqlExpression1 = sqlExpression1 == null ? r1 : sqlExpression1.And(r1);
    zParam = iParam;
    return sqlExpression1;
  }

  private SQLExpression GetExpression(
    List<(SQLExpression Expr, FilterOperator PrevOper)> bracketRegion)
  {
    if (bracketRegion == null || !bracketRegion.Any<(SQLExpression, FilterOperator)>())
      return (SQLExpression) null;
    return this.GetBalancedExpression(bracketRegion, 0, bracketRegion.Count - 1).Expr?.Embrace();
  }

  private (SQLExpression Expr, FilterOperator PrevOper) GetBalancedExpression(
    List<(SQLExpression Expr, FilterOperator PrevOper)> nodes,
    int start,
    int end)
  {
    if (start > end)
      return ((SQLExpression) null, (FilterOperator) 0);
    int index = (start + end) / 2;
    (SQLExpression, FilterOperator) balancedExpression1 = nodes[index];
    (SQLExpression Expr, FilterOperator PrevOper) balancedExpression2 = this.GetBalancedExpression(nodes, start, index - 1);
    if (balancedExpression2.Expr != null)
      balancedExpression1 = balancedExpression1.Item2 == null ? (balancedExpression2.Expr.And(balancedExpression1.Item1).Unembrace(), balancedExpression2.PrevOper) : (balancedExpression2.Expr.Or(balancedExpression1.Item1).Unembrace(), balancedExpression2.PrevOper);
    (SQLExpression Expr, FilterOperator PrevOper) balancedExpression3 = this.GetBalancedExpression(nodes, index + 1, end);
    if (balancedExpression3.Expr != null)
      balancedExpression1 = balancedExpression3.PrevOper == null ? (balancedExpression1.Item1.And(balancedExpression3.Expr).Unembrace(), balancedExpression1.Item2) : (balancedExpression1.Item1.Or(balancedExpression3.Expr).Unembrace(), balancedExpression1.Item2);
    return balancedExpression1;
  }

  private Table BuildSqlTable(System.Type table, PXGraph graph)
  {
    if (this.ReplacedIncomingRowType != (System.Type) null || this.IncomingRowType == (System.Type) null || table != this.IncomingRowType && !table.IsAssignableFrom(this.IncomingRowType) && !this.IncomingRowType.IsAssignableFrom(table))
      return BqlCommand.GetSQLTable(table, graph);
    this.ReplacedIncomingRowType = table;
    Query query = new Query();
    PXCache cach = graph.Caches[table];
    object obj;
    if (!(this.IncomingRowType != table) || !this.IncomingRowType.IsAssignableFrom(table) || !(cach.GetItemType() != this.IncomingRow.GetType()))
      obj = this.IncomingRow;
    else
      obj = cach.GetType().GetMethod("Extend").MakeGenericMethod(this.IncomingRowType).Invoke((object) cach, new object[1]
      {
        this.IncomingRow
      });
    object row = obj;
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (string field in (List<string>) cach.Fields)
    {
      SQLExpression fieldName;
      SQLExpression singleFieldExpression = BqlSoapCommand.GetSingleFieldExpression(cach, row, field, out fieldName);
      if (singleFieldExpression != null)
      {
        bool flag;
        string str;
        if (fieldName is Column column1)
        {
          flag = true;
          str = column1.Name;
        }
        else
        {
          str = field;
          flag = false;
        }
        string a = str;
        if (!stringSet.Add(a))
        {
          if (flag)
          {
            Column column2 = (Column) fieldName;
            a = $"{((SimpleTable) column2.Table()).Name}_{column2.Name}";
            stringSet.Add(a);
          }
          else
            continue;
        }
        query.Field(singleFieldExpression.As(a));
      }
    }
    return (Table) query;
  }

  private SQLExpression ParamHandlerFilterExpression(
    string name,
    ref int parnum,
    PXGraph graph,
    out bool bypassCondition)
  {
    bypassCondition = false;
    if (name == null)
      return (SQLExpression) Literal.NewParameter(parnum++);
    name = name.Trim(' ', '[', ']', '`');
    if (name.StartsWith("@"))
      return (SQLExpression) Literal.NewParameter(parnum++);
    string name1 = name;
    Tuple<System.Type, string, System.Type> bqlField = this.tryFindBqlField(graph, ref name1);
    if (bqlField != null)
    {
      SQLExpression fieldExpression = this.GetFieldExpression(bqlField.Item1, graph.Caches[bqlField.Item1], bqlField.Item2, this.removePref(name1), BqlCommand.FieldPlace.Condition);
      if (!(this.IncomingRowType != (System.Type) null) || (!(this.ReplacedIncomingRowType == (System.Type) null) || !(bqlField.Item1 == this.IncomingRowType) && !bqlField.Item1.IsAssignableFrom(this.IncomingRowType) && !this.IncomingRowType.IsAssignableFrom(bqlField.Item1)) && (!(this.ReplacedIncomingRowType != (System.Type) null) || !(bqlField.Item1 == this.ReplacedIncomingRowType)))
        return fieldExpression;
      bypassCondition = true;
      return fieldExpression;
    }
    System.Type key = (System.Type) null;
    string rowName = (string) null;
    int length = name.IndexOf('.');
    if (length != -1)
    {
      rowName = name.Substring(0, length);
      if (name.Length > length + 2 && EnumerableExtensions.IsIn<char>(name[length + 1], '[', '`'))
        name = name.Remove(length + 1, 1);
      if (((CollectionBase) this._arguments.Relations).Count > 0)
      {
        if (this._arguments.Relations[0].ParentAlias == rowName || string.IsNullOrEmpty(this._arguments.Relations[0].ParentAlias) && this._arguments.Relations[0].ParentName == rowName)
          key = this._tablemap[this._arguments.Relations[0].ParentName];
        if (key == (System.Type) null)
        {
          foreach (ReportRelation relation in (CollectionBase) this._arguments.Relations)
          {
            if (!(relation.ChildAlias != rowName) || string.IsNullOrEmpty(relation.ChildAlias) && !(relation.ChildName != rowName))
            {
              key = this._tablemap[relation.ChildName];
              break;
            }
          }
        }
      }
      else
        key = this._tablemap[((List<ReportTable>) this._arguments.Tables)[0].Name];
    }
    string fieldName1;
    if (PXLocalesProvider.CollationComparer.Equals(name, rowName + ".DeletedDatabaseRecord") || key != (System.Type) null && PXDatabase.IsReadDeletedSupported(graph.Caches[key].BqlTable, out fieldName1) && PXLocalesProvider.CollationComparer.Equals(name, $"{rowName}.{fieldName1}"))
      return (SQLExpression) new Column(name.Substring(length + 1), this.removePref(rowName));
    string fieldName2;
    if (PXLocalesProvider.CollationComparer.Equals(name, rowName + ".DatabaseRecordStatus") || key != (System.Type) null && PXDatabase.Provider.IsRecordStatusSupported(graph.Caches[key].BqlTable, out fieldName2) && PXLocalesProvider.CollationComparer.Equals(name, $"{rowName}.{fieldName2}"))
      return (SQLExpression) new Column(name.Substring(length + 1), this.removePref(rowName));
    throw new PXException($"A field with the name {name} cannot be found.");
  }

  private SQLExpression GetFieldExpression(
    System.Type table,
    PXCache cache,
    string field,
    string alias,
    BqlCommand.FieldPlace fieldPlace)
  {
    return BqlSoapCommand.GetFieldExprInternal(table, cache, field, string.IsNullOrEmpty(alias) ? cache.GetItemType().Name : alias, fieldPlace);
  }

  internal static SQLExpression GetFieldExprInternal(
    System.Type table,
    PXCache cache,
    string field,
    string alias,
    BqlCommand.FieldPlace place)
  {
    if (CustomizedTypeManager.IsCustomizedType(table) && cache.GetItemType().IsSubclassOf(table))
      table = cache.GetItemType();
    PXDBOperation operation = PXDBOperation.Select;
    bool flag = field.EndsWith("_Attributes") && place != BqlCommand.FieldPlace.Select;
    if (flag)
      operation |= PXDBOperation.External;
    if (place == BqlCommand.FieldPlace.Condition)
      operation |= PXDBOperation.WhereClause;
    if (place == BqlCommand.FieldPlace.OrderBy)
      operation |= PXDBOperation.OrderByClause;
    PXCommandPreparingEventArgs.FieldDescription description;
    cache.RaiseCommandPreparing(field, (object) null, (object) null, operation, table, out description);
    if ((description?.Expr != null || place == BqlCommand.FieldPlace.Select ? 0 : ((operation & PXDBOperation.External) != PXDBOperation.External ? 1 : 0)) != 0)
      cache.RaiseCommandPreparing(field, (object) null, (object) null, operation | PXDBOperation.External, table, out description);
    if (description == null || description.Expr == null)
      return (SQLExpression) null;
    if (description.Expr.IsNullExpression())
      return description.Expr;
    SQLExpression fieldExprInternal = description.Expr;
    if (description.Expr is Column expr)
      return cache.BqlSelect == null ? (SQLExpression) new Column(expr.Name, alias, expr.GetDBType()) : (SQLExpression) new Column(field, alias);
    if (!(cache.BqlSelect == null | flag) && !cache.IsKvExtAttribute(field))
      return (SQLExpression) new Column(field, alias);
    if (string.Compare(alias, cache.GetItemType().Name, StringComparison.OrdinalIgnoreCase) != 0)
      fieldExprInternal = fieldExprInternal.substituteTableName(cache.GetItemType().Name, alias);
    return fieldExprInternal;
  }

  private SQLExpression BuildJoinCondition(ReportRelation r, PXGraph graph, ref int iParam)
  {
    System.Type type1 = this._tablemap[r.ChildName];
    PXCache cach1 = graph.Caches[type1];
    string parentName = r.ParentName;
    System.Type type2;
    if (!this._tablemap.TryGetValue(parentName, out type2))
      this.throwTable(parentName);
    PXCache cach2 = graph.Caches[type2];
    FilterOperator filterOperator = (FilterOperator) 0;
    int val1 = 0;
    List<(SQLExpression, FilterOperator)> bracketRegion = new List<(SQLExpression, FilterOperator)>();
    Stack<List<(SQLExpression, FilterOperator)>> valueTupleListStack = new Stack<List<(SQLExpression, FilterOperator)>>();
    valueTupleListStack.Push(bracketRegion);
    foreach (RelationRow link in (List<RelationRow>) r.Links)
    {
      bool flag = link.Condition != 6 && link.Condition != 7;
      if (string.IsNullOrEmpty(link.ParentField) || flag && string.IsNullOrEmpty(link.ChildField))
        throw new Exception("An empty relation field has been specified.");
      if (link.OpenBraces > 0)
      {
        val1 += link.OpenBraces;
        for (int index = 0; index < link.OpenBraces; ++index)
          valueTupleListStack.Push(new List<(SQLExpression, FilterOperator)>());
      }
      SQLExpression operandExpression1 = this.GetSingleOperandExpression(link.ParentField, graph, type2, cach2, r.ParentAlias, ref iParam);
      SQLExpression operandExpression2 = flag ? this.GetSingleOperandExpression(link.ChildField, graph, type1, cach1, r.ChildAlias, ref iParam) : (SQLExpression) null;
      SQLExpression forLinkCondition = this.GetSqlExpressionForLinkCondition(link.Condition, operandExpression1, operandExpression2);
      valueTupleListStack.Peek().Add((forLinkCondition, filterOperator));
      filterOperator = link.Operator;
      int num = System.Math.Min(val1, link.CloseBraces);
      if (link.CloseBraces > 0 && val1 > 0)
      {
        if (valueTupleListStack.Count < num + 1)
          throw new PXException("Brackets in the Where condition are not balanced.");
        for (int index = 0; index < num; ++index)
        {
          List<(SQLExpression, FilterOperator)> valueTupleList = valueTupleListStack.Pop();
          SQLExpression expression = this.GetExpression(valueTupleList);
          if (expression != null)
            valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, FilterOperator)>().Item2));
        }
        val1 -= num;
      }
    }
    while (valueTupleListStack.Count > 1)
    {
      List<(SQLExpression, FilterOperator)> valueTupleList = valueTupleListStack.Pop();
      SQLExpression expression = this.GetExpression(valueTupleList);
      if (expression != null)
        valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, FilterOperator)>().Item2));
    }
    return this.GetExpression(bracketRegion);
  }

  private SQLExpression CreateParameterExpression(PXGraph graph, string name, ref int nbr)
  {
    if (name == null || name.Length <= 2 || name[0] != '[' || name[name.Length - 1] != ']')
      return (SQLExpression) Literal.NewParameter(nbr++);
    name = name.Substring(1, name.Length - 2);
    Tuple<System.Type, string, System.Type> bqlField = this.findBqlField(graph, ref name);
    return this.GetFieldExpression(bqlField.Item1, graph.Caches[bqlField.Item1], bqlField.Item2, this.removePref(name), BqlCommand.FieldPlace.Condition);
  }

  private static SQLExpression GetSingleFieldExpression(
    PXCache cache,
    object row,
    string field,
    out SQLExpression fieldName)
  {
    object v = cache.GetValue(row, field);
    PXCommandPreparingEventArgs.FieldDescription description;
    cache.RaiseCommandPreparing(field, row, v, PXDBOperation.NestedSelectInReport, (System.Type) null, out description);
    if (description == null || description.Expr == null || description.Expr.IsNullExpression())
    {
      fieldName = (SQLExpression) new Column(field);
      if (v == null)
        return SQLExpression.Null();
      System.Type type = v.GetType();
      return type.IsPrimitive || type == typeof (string) ? (SQLExpression) new SQLConst(v) : (SQLExpression) null;
    }
    fieldName = description.Expr;
    if (description.DataValue == null || description.DataValue.GetType().IsArray && !(description.DataValue is byte[]))
      return SQLExpression.Null();
    string dataValue = description.DataValue as string;
    SQLConst singleFieldExpression = new SQLConst((string.Equals(dataValue, cache.Graph.SqlDialect.GetDate, StringComparison.OrdinalIgnoreCase) ? 1 : (string.Equals(dataValue, cache.Graph.SqlDialect.GetUtcDate, StringComparison.OrdinalIgnoreCase) ? 1 : 0)) == 0 || !(v is System.DateTime) ? description.DataValue : v);
    singleFieldExpression.SetDBType(description.DataType);
    return (SQLExpression) singleFieldExpression;
  }

  private SQLExpression GetSingleOperandExpression(
    string field,
    PXGraph graph,
    System.Type ttype,
    PXCache cache,
    string alias,
    ref int zParam)
  {
    int iParam = zParam;
    IPXValue ipxValue = this.GetValue(field);
    SQLExpression operandExpression = ipxValue.GetExpression((Func<string, SQLExpression>) (name => this.ParamHandlerRelationExpression(name, ref iParam, ttype, cache, alias, graph)));
    if (operandExpression == null)
      throw new PXException("The report cannot be opened because it includes a table ({0}) or a field ({1}) that does not exist.", new object[2]
      {
        string.IsNullOrEmpty(alias) ? (object) cache.GetItemType().Name : (object) alias,
        (object) field
      });
    if (field.StartsWith("="))
      operandExpression.Embrace();
    List<SQLExpression> res = new List<SQLExpression>();
    operandExpression.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is Literal literal && literal.Number == -1), res);
    object[] parameters = ipxValue.GetParameters(new Func<string, IPXValue>(this.ParamHandler));
    int num = System.Math.Min(res.Count, parameters.Length);
    for (int index = 0; index < num; ++index)
      operandExpression = operandExpression.substituteNode(res[index], (SQLExpression) new SQLConst(parameters[index]));
    zParam = iParam;
    return operandExpression;
  }

  private SQLExpression GetSqlExpressionForLinkCondition(
    LinkCondition linkCondition,
    SQLExpression left,
    SQLExpression right)
  {
    switch ((int) linkCondition)
    {
      case 0:
        return SQLExpressionExt.EQ(left, right);
      case 1:
        return SQLExpressionExt.NE(left, right);
      case 2:
        return left.GT(right);
      case 3:
        return left.GE(right);
      case 4:
        return left.LT(right);
      case 5:
        return left.LE(right);
      case 6:
        return left.IsNull();
      case 7:
        return left.IsNotNull();
      default:
        throw new InvalidOperationException("Incorrect link condition");
    }
  }

  private SQLExpression ParamHandlerRelationExpression(
    string name,
    ref int parnum,
    System.Type type,
    PXCache cache,
    string alias,
    PXGraph graph)
  {
    if (name == null)
      return (SQLExpression) Literal.NewParameter(-1);
    name = name.Trim(' ', '[', ']');
    if (name.StartsWith("@"))
      return (SQLExpression) Literal.NewParameter(parnum++);
    name = !name.Contains<char>('.') || !string.IsNullOrEmpty(StringExtensions.FirstSegment(name, '.').Trim()) ? name : name.Trim().Substring(1);
    if (!name.Contains("."))
      return this.GetFieldExpression(type, cache, name, alias, BqlCommand.FieldPlace.Condition);
    string name1 = name;
    Tuple<System.Type, string, System.Type> bqlField = this.findBqlField(graph, ref name1);
    return this.GetFieldExpression(bqlField.Item1, graph.Caches[bqlField.Item1], bqlField.Item2, this.removePref(name1), BqlCommand.FieldPlace.Condition);
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return graph == null || !info.BuildExpression ? new Query() : this.BuildQuery(graph, selection.ColExprs);
  }

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereAnd<where1>()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereNot()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereOr<where1>()
  {
    throw new PXException("The method or operation is not implemented.");
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    throw new PXException("The method or operation is not implemented.");
  }

  internal class RowSelectingInfo
  {
    public readonly PXCache Cache;
    public List<BqlSoapCommand.RowSelectingFieldInfo> Fields = new List<BqlSoapCommand.RowSelectingFieldInfo>();
    private object _KeyNode;
    public int LastPos = -1;
    public int KeysCount;
    public readonly Dictionary<object, object> RowIndex;

    public RowSelectingInfo(PXCache cache)
    {
      this.Cache = cache;
      this.RowIndex = new Dictionary<object, object>((IEqualityComparer<object>) new BqlSoapCommand.RowSelectingInfo.Comparer()
      {
        Cache = cache
      });
    }

    public object KeyNode => this._KeyNode ?? (this._KeyNode = this.Cache.CreateInstance());

    public void ResetKeyNode() => this._KeyNode = (object) null;

    private class Comparer : IEqualityComparer<object>
    {
      public PXCache Cache;
      private int cnt;

      bool IEqualityComparer<object>.Equals(object x, object y)
      {
        ++this.cnt;
        int cnt = this.cnt;
        return this.Cache.ObjectsEqual(x, y);
      }

      int IEqualityComparer<object>.GetHashCode(object obj)
      {
        this.cnt = 0;
        return this.Cache.GetObjectHashCode(obj);
      }
    }
  }

  internal class RowSelectingFieldInfo
  {
    public string Text;
    public int Pos;
    public List<IPXRowSelectingSubscriber> RowSelectingHandlers = new List<IPXRowSelectingSubscriber>();
    public string Field;
    public bool IsLastKey;
    public int PosAfter = -1;
    public int PosBefore = -1;
  }

  private class FInfo
  {
    public SQLExpression Expr;
    public bool InReport;
    public List<IPXRowSelectingSubscriber> RowSelecting;
    public bool IsKey;
    public List<BqlSoapCommand.FInfo> Range = new List<BqlSoapCommand.FInfo>();
    public string FieldName;
  }

  private class shift1Rowselecting : IPXRowSelectingSubscriber
  {
    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e) => ++e.Position;
  }

  private class fetchKeyValuePairs : IPXRowSelectingSubscriber
  {
    private List<IPXRowSelectingSubscriber> fields = new List<IPXRowSelectingSubscriber>();

    public fetchKeyValuePairs(PXCache sender)
    {
      foreach (string key in sender._KeyValueStoredNames.Keys)
      {
        foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly(key, false))
          subscriberAttribute.GetSubscriber<IPXRowSelectingSubscriber>(this.fields);
      }
    }

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      string firstColumn = e.Record.GetString(e.Position);
      ++e.Position;
      ISqlDialect sqlDialect = sender.Graph.SqlDialect;
      string[] attributes1;
      if (sqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) sender._KeyValueStoredNames, out attributes1))
      {
        PXRowSelectingEventArgs e1 = new PXRowSelectingEventArgs(e.Row, (PXDataRecord) new PXDummyDataRecord(attributes1), 0, e.IsReadOnly);
        foreach (IPXRowSelectingSubscriber field in this.fields)
          field.RowSelecting(sender, e1);
      }
      string[] attributes2;
      if (sender._KeyValueAttributeNames == null || !sqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) sender._KeyValueAttributeNames, out attributes2))
        return;
      sender.SetSlot<object[]>(e.Row, sender._KeyValueAttributeSlotPosition, sender.convertAttributesFromString(attributes2), true);
    }
  }

  private class fetchKeyValueAttributes : IPXRowSelectingSubscriber
  {
    public fetchKeyValueAttributes(PXCache sender)
    {
    }

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      string firstColumn = e.Record.GetString(e.Position);
      ++e.Position;
      string[] attributes;
      if (!sender.Graph.SqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) sender._KeyValueAttributeNames, out attributes))
        return;
      sender.SetSlot<object[]>(e.Row, sender._KeyValueAttributeSlotPosition, sender.convertAttributesFromString(attributes), true);
    }
  }
}
