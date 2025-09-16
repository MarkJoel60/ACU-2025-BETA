// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlGenericCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// A class, which is derived from the <see cref="T:PX.Data.BqlCommand" /> class, that is used by the system during the processing of generic inquiries.
/// The main purpose of <tt>BqlCommand</tt> classes is to convert BQL commands to SQL query tree (see <see cref="N:PX.Data.SQLTree" />).
/// The <tt>BqlGenericCommand</tt> class works with generic inquiry schema to get the resulting SQL query tree.
/// </summary>
public class BqlGenericCommand : BqlCommand
{
  private readonly PXGraph _graph;
  private readonly PXCache _cache;
  private List<PXTable> _tables;
  private List<PXDataValue> _pars;
  private readonly List<KeyValuePair<System.Type, string>> _maskedTypes;
  private int _positionInQuery;
  private int _positionInResult;
  private const string InnerQueryAlias = "InnerQuery";
  private List<string> _recordMapEntryAliases = new List<string>();
  private const string AttributeSuffix = "_Attributes";

  public PXQueryDescription Description { get; }

  public BqlGenericCommand(PXGraph graph, PXCache recordCache, PXQueryDescription descr)
  {
    this._graph = graph;
    this._cache = recordCache;
    this.Description = descr;
    this._maskedTypes = new List<KeyValuePair<System.Type, string>>();
  }

  internal IReadOnlyCollection<PXTable> Tables
  {
    get => (IReadOnlyCollection<PXTable>) this._tables.AsReadOnly();
  }

  private bool HasWheres
  {
    get
    {
      return this.Description.Wheres.Count > 0 || !this.HasGrouping && this.Description.FilterWheres.Count > 0 || this.Description.Searches.Count > 0 || this.Description.ExternalWhereExpression != null;
    }
  }

  private bool HasGrouping => this.Description.GroupBys.Count > 0;

  private bool UseHavingForFilters => this.HasGrouping && this.Description.FilterWheres.Count > 0;

  private bool HasOuterAggregates
  {
    get
    {
      try
      {
        foreach (PXWhereCond filterWhere in this.Description.FilterWheres)
        {
          PXWhereCond where = filterWhere;
          int parNum = 0;
          where.DataField.GetExpression((Func<string, SQLExpression>) (_ => this.ParameterHandlerExpression(_, ref parNum, PXDBOperation.WhereClause, where.UseExt)));
        }
      }
      catch (PXVirtualFieldException ex)
      {
        return false;
      }
      return this.Description.RetrieveTotalRowCount || this.Description.RetrieveTotals;
    }
  }

  public PXDataValue[] Parameters
  {
    get
    {
      if (this._pars != null)
        return this._pars.ToArray();
      this._pars = new List<PXDataValue>();
      this.CollectValueParameters(this.Description.FormulaFields.Select<PXFormulaField, IPXValue>((Func<PXFormulaField, IPXValue>) (x => x.Value)));
      this.CollectTableParameters(this.Description);
      this.CollectWhereParameters((IEnumerable<PXWhereCond>) this.Description.Wheres);
      if (this.UseHavingForFilters)
      {
        this.CollectWhereParameters((IEnumerable<PXWhereCond>) this.Description.Searches);
        this.CollectValueParameters(this.Description.GroupBys.Select<PXGroupBy, IPXValue>((Func<PXGroupBy, IPXValue>) (x => x.DataField)));
        this.CollectWhereParameters((IEnumerable<PXWhereCond>) this.Description.FilterWheres);
      }
      else
      {
        this.CollectWhereParameters((IEnumerable<PXWhereCond>) this.Description.FilterWheres);
        this.CollectWhereParameters((IEnumerable<PXWhereCond>) this.Description.Searches);
        this.CollectValueParameters(this.Description.GroupBys.Select<PXGroupBy, IPXValue>((Func<PXGroupBy, IPXValue>) (x => x.DataField)));
      }
      if (!this.HasOuterAggregates)
        this.CollectValueParameters(this.Description.Sorts.Select<PXSort, IPXValue>((Func<PXSort, IPXValue>) (x => x.DataField)));
      return this._pars.ToArray();
    }
  }

  public PXTable[] GetTables() => this._tables.ToArray();

  private void CollectValueParameters(IEnumerable<IPXValue> values)
  {
    foreach (IPXValue ipxValue in values)
    {
      PXDataValue[] dataValueParameters = ipxValue?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler));
      if (dataValueParameters != null)
        this._pars.AddRange((IEnumerable<PXDataValue>) dataValueParameters);
    }
  }

  private void CollectTableParameters(PXQueryDescription description)
  {
    HashSet<string> addedTableAliases = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (!description.Relations.Any<PXRelation>())
    {
      foreach (PXTable table in description.UsedTables.Values)
        AddTableParameters(table);
    }
    else
    {
      foreach (PXRelation relation in description.Relations)
      {
        AddTableParameters(relation.First);
        AddTableParameters(relation.Second);
        foreach (PXOnCond pxOnCond in relation.On)
        {
          if (pxOnCond.Cond != Condition.IsNull && pxOnCond.Cond != Condition.NotNull)
          {
            PXDataValue[] dataValueParameters1 = pxOnCond.FirstField?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
            if (dataValueParameters1 != null)
              this._pars.AddRange((IEnumerable<PXDataValue>) dataValueParameters1);
            PXDataValue[] dataValueParameters2 = pxOnCond.SecondField?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
            if (dataValueParameters2 != null)
              this._pars.AddRange((IEnumerable<PXDataValue>) dataValueParameters2);
          }
          else if (pxOnCond.FirstField is PXParameterValue)
          {
            PXDataValue[] dataValueParameters = pxOnCond.FirstField?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
            if (dataValueParameters != null)
              this._pars.AddRange((IEnumerable<PXDataValue>) dataValueParameters);
          }
        }
      }
    }

    void AddTableParameters(PXTable table)
    {
      if (!addedTableAliases.Add(table.Alias))
        return;
      PXDataValue[] parameters = table.Graph?.LastCommand?.Parameters;
      if (parameters == null)
        return;
      this._pars.AddRange((IEnumerable<PXDataValue>) parameters);
    }
  }

  private void CollectWhereParameters(IEnumerable<PXWhereCond> wheres)
  {
    foreach (PXWhereCond where1 in wheres)
    {
      PXWhereCond where = where1;
      PXDataValue[] dataValueParameters1 = where.DataField?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
      if (where.Cond != Condition.IsNull && where.Cond != Condition.NotNull)
      {
        if (dataValueParameters1 != null)
          this._pars.AddRange((IEnumerable<PXDataValue>) dataValueParameters1);
        PXDataValue[] dataValueParameters2 = where.Value1?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
        PXDataValue[] dataValueParameters3 = where.Value2?.GetDataValueParameters(new Func<string, IPXValue>(this.ParameterHandler), true);
        if (where.DataField != null)
        {
          try
          {
            int parNum = 0;
            where.DataField.GetExpression((Func<string, SQLExpression>) (field => this.ParameterHandlerExpression(field, ref parNum, PXDBOperation.WhereClause, where.UseExt)));
          }
          catch (PXVirtualFieldException ex)
          {
            continue;
          }
        }
        bool isParamMustBeString = where.Cond == Condition.StartsWith || where.Cond == Condition.EndsWith || where.Cond == Condition.Contains || where.Cond == Condition.NotContains;
        if (dataValueParameters2 != null)
          this._pars.AddRange(((IEnumerable<PXDataValue>) dataValueParameters2).Select<PXDataValue, PXDataValue>((Func<PXDataValue, PXDataValue>) (v => this.CreateParameterValue(where.DataField, v, where.UseExt, !isParamMustBeString && !(where.Value1 is PXCalcedValue)))));
        if (where.Cond == Condition.Between && dataValueParameters3 != null)
          this._pars.AddRange(((IEnumerable<PXDataValue>) dataValueParameters3).Select<PXDataValue, PXDataValue>((Func<PXDataValue, PXDataValue>) (v => this.CreateParameterValue(where.DataField, v, where.UseExt, !isParamMustBeString && !(where.Value1 is PXCalcedValue)))));
      }
      else if (dataValueParameters1 != null)
        this._pars.AddRange(((IEnumerable<PXDataValue>) dataValueParameters1).Select<PXDataValue, PXDataValue>((Func<PXDataValue, PXDataValue>) (v => this.CreateParameterValue(where.DataField, v, where.UseExt, true))));
    }
  }

  private PXDataValue CreateParameterValue(
    IPXValue field,
    PXDataValue value,
    bool useExt,
    bool getDescription)
  {
    if (!getDescription)
      return this.CreateParameterValue(value, (System.Type) null, (PXCommandPreparingEventArgs.FieldDescription) null, useExt);
    try
    {
      System.Type type;
      PXCommandPreparingEventArgs.FieldDescription description = this.GetDescription(field, useExt, out type);
      if (description != null)
        return this.CreateParameterValue(value, type, description, useExt);
    }
    catch
    {
    }
    SQLExpression expression = this.GetExpression(field as PXParameterValue, useExt);
    return this.CreateParameterValue(value, expression);
  }

  private PXDataValue CreateParameterValue(PXDataValue value, SQLExpression expression)
  {
    return value.ValueType != PXDbType.Unspecified || expression == null ? value : new PXDataValue(expression.GetDBType(), value.Value);
  }

  private SQLExpression GetExpression(PXParameterValue dataField, bool forceExt)
  {
    if (dataField == null)
      return (SQLExpression) null;
    try
    {
      int parNum = 0;
      return dataField.GetExpression((Func<string, SQLExpression>) (field => this.ParameterHandlerExpression(field, ref parNum, PXDBOperation.WhereClause, forceExt)), false);
    }
    catch (PXVirtualFieldException ex)
    {
    }
    return (SQLExpression) null;
  }

  private PXDataValue CreateParameterValue(
    PXDataValue value,
    System.Type type,
    PXCommandPreparingEventArgs.FieldDescription desc,
    bool useExt)
  {
    if (value.ValueType != PXDbType.Unspecified || desc == null || value.Value != null && !(value.Value.GetType() == type))
      return value;
    int? valueLength = desc.DataLength;
    if (useExt && value.Value != null && type == typeof (string))
      valueLength = new int?(((string) value.Value).Length);
    return new PXDataValue(desc.DataType, valueLength, value.Value);
  }

  private PXCommandPreparingEventArgs.FieldDescription GetDescription(
    IPXValue field,
    bool forceExt,
    out System.Type type)
  {
    type = (System.Type) null;
    if (!(field is PXFieldValue pxFieldValue))
      return (PXCommandPreparingEventArgs.FieldDescription) null;
    PXTable pxTable;
    if (!this.Description.Tables.TryGetValue(pxFieldValue.TableName, out pxTable))
      return (PXCommandPreparingEventArgs.FieldDescription) null;
    PXCache cach = this._graph.Caches[this.Description.Tables[pxFieldValue.TableName].CacheType];
    if (cach == null || !cach.Fields.Contains(pxFieldValue.FieldName))
      return (PXCommandPreparingEventArgs.FieldDescription) null;
    type = cach.GetFieldType(pxFieldValue.FieldName);
    PXDBOperation operation = (PXDBOperation) ((forceExt ? 16 /*0x10*/ : 0) | 256 /*0x0100*/);
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(pxFieldValue.FieldName, (object) null, (object) null, operation, pxTable.CacheType, out description);
    if (!forceExt && description?.Expr == null)
      cach.RaiseCommandPreparing(pxFieldValue.FieldName, (object) null, (object) null, operation | PXDBOperation.External, pxTable.CacheType, out description);
    return description;
  }

  protected Query BuildQuery(PXGraph graph, BqlCommand.Selection selection, ref int parnum)
  {
    Query query1 = this.CreateQuery(graph);
    this._maskedTypes.Clear();
    this._positionInQuery = 0;
    this._positionInResult = 0;
    ISqlDialect sqlDialect = graph.SqlDialect;
    parnum = this.InsertFieldsExpressions(selection, parnum, sqlDialect);
    foreach (PXTable table in this._tables)
    {
      if (GroupHelper.IsRestricted(typeof (Users), table.CacheType))
        this._maskedTypes.Add(new KeyValuePair<System.Type, string>(table.CacheType, string.IsNullOrEmpty(table.Alias) ? table.CacheType.Name : table.Alias));
    }
    IDictionary<string, HashSet<string>> allUsedColumns = this.GetAllUsedColumns(selection);
    ISet<string> usedTableAliases;
    parnum = this.AppendFROM(query1, parnum, allUsedColumns, out usedTableAliases);
    if (this.HasWheres)
    {
      SQLExpression w = (SQLExpression) null;
      if (this.Description.Wheres.Count > 0)
        w = this.GetWHERE(this.Description.Wheres, ref parnum, false, usedTableAliases, false);
      if (!this.HasGrouping && this.Description.FilterWheres.Count > 0)
      {
        SQLExpression where = this.GetWHERE(this.Description.FilterWheres, ref parnum, false, usedTableAliases, true);
        if (where != null)
          w = w == null ? where : w.And(where);
      }
      if (this.Description.Searches.Count > 0)
      {
        SQLExpression where = this.GetWHERE(this.Description.Searches, ref parnum, false, usedTableAliases, false);
        if (where != null)
          w = w == null ? where : w.And(where);
      }
      if (this.Description.ExternalWhereExpression != null)
        w = w == null ? this.Description.ExternalWhereExpression : w.And(this.Description.ExternalWhereExpression);
      query1.Where(w);
    }
    Query query2 = TableChangingScope.AddIsNewWhereForGI(this.Description, graph, query1);
    this.AppendRestriction(query2);
    parnum = this.AppendGroupBy(query2, parnum, usedTableAliases);
    if (this.UseHavingForFilters)
    {
      SQLExpression where = this.GetWHERE(this.Description.FilterWheres, ref parnum, true, usedTableAliases, true);
      if (where != null)
        query2.Having(where);
    }
    if (this.HasOuterAggregates)
    {
      this._positionInQuery = 0;
      this._positionInResult = 0;
      foreach (PXDataRecordMap.FieldEntry recordMapEntry in this.RecordMapEntries)
      {
        recordMapEntry.PositionInResult = -1;
        recordMapEntry.PositionInQuery = -1;
      }
      query2 = this.AppendOuterAggregate(query2, selection);
    }
    else
      parnum = this.AppendOrder(query2, parnum, usedTableAliases);
    query2.Select(selection.ColExprs.ToArray());
    return query2;
  }

  private IDictionary<string, HashSet<string>> GetAllUsedColumns(BqlCommand.Selection selection)
  {
    List<(string, string)> allColumns = new List<(string, string)>();
    this.Description.Relations.ForEach((System.Action<PXRelation>) (r => r.On.ForEach((System.Action<PXOnCond>) (o => AddColumns(new IPXValue[2]
    {
      o.FirstField,
      o.SecondField
    })))));
    this.Description.Searches.ForEach((System.Action<PXWhereCond>) (w => AddColumns(new IPXValue[3]
    {
      w.DataField,
      w.Value1,
      w.Value2
    })));
    this.Description.Wheres.ForEach((System.Action<PXWhereCond>) (w => AddColumns(new IPXValue[3]
    {
      w.DataField,
      w.Value1,
      w.Value2
    })));
    this.Description.FilterWheres.ForEach((System.Action<PXWhereCond>) (w => AddColumns(new IPXValue[3]
    {
      w.DataField,
      w.Value1,
      w.Value2
    })));
    this.Description.Sorts.ForEach((System.Action<PXSort>) (s => AddColumns(new IPXValue[1]
    {
      s.DataField
    })));
    this.Description.ExtFields.ForEach(new System.Action<PXExtField>(AddExtColumns));
    RestrictedFieldsSet restrictedFields = selection.RestrictedFields;
    if (restrictedFields != null)
      EnumerableExtensions.ForEach<RestrictedField>((IEnumerable<RestrictedField>) restrictedFields, (System.Action<RestrictedField>) (f => AddField(f.Field, '_')));
    selection.ColExprs?.ForEach((System.Action<SQLExpression>) (e => e.GetExpressionsOfType<Column>().ForEach((System.Action<Column>) (c => allColumns.Add((c.Table().AliasOrName(), c.Name))))));
    return (IDictionary<string, HashSet<string>>) allColumns.GroupBy<(string, string), string>((Func<(string, string), string>) (x => x.Item1.ToLowerInvariant())).ToDictionary<IGrouping<string, (string, string)>, string, HashSet<string>>((Func<IGrouping<string, (string, string)>, string>) (x => x.Key), (Func<IGrouping<string, (string, string)>, HashSet<string>>) (x => x.Select<(string, string), string>((Func<(string, string), string>) (v => v.Item2)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    void AddColumns(IPXValue[] ipxValues)
    {
      foreach (IPXValue ipxValue in ipxValues)
      {
        if (ipxValue is PXFieldValue pxFieldValue)
        {
          allColumns.Add((pxFieldValue.TableName, pxFieldValue.FieldName));
          if (this.IsAttributesField(pxFieldValue.FieldName))
          {
            string str = this.TrimFieldName(pxFieldValue.FieldName);
            if (str != null && str.Length > 0)
              allColumns.Add((pxFieldValue.TableName, str + "_NoteID"));
          }
        }
        else
          ipxValue?.GetExpression((Func<string, SQLExpression>) (x =>
          {
            AddField(x, '.');
            return SQLExpression.Null();
          }));
      }
    }

    void AddField(string field, char delimiter)
    {
      string[] strArray = field.Split(delimiter, 2);
      if (strArray.Length != 2)
        return;
      allColumns.Add((strArray[0], strArray[1]));
    }

    void AddExtColumns(PXExtField extField)
    {
      switch (extField)
      {
        case PXFormulaField pxFormulaField:
          AddColumns(new IPXValue[1]{ pxFormulaField.Value });
          break;
        case PXAggregateField pxAggregateField:
          allColumns.Add((pxAggregateField.Table.Alias, pxAggregateField.Name));
          break;
        default:
          throw new NotImplementedException();
      }
    }
  }

  protected Query AppendOuterAggregate(Query query, BqlCommand.Selection selection)
  {
    List<SQLExpression> sqlExpressionList1 = new List<SQLExpression>();
    List<SQLExpression> sqlExpressionList2 = new List<SQLExpression>();
    if (this.Description.RetrieveTotalRowCount && this.Description.TotalFields.Count == 0 && !this.HasGrouping && this.Description.Relations.Count == 0)
      sqlExpressionList1.Add((SQLExpression) new Asterisk());
    else
      sqlExpressionList1.AddRange((IEnumerable<SQLExpression>) selection.ColExprs);
    foreach (PXAggregateField totalField in this.Description.TotalFields)
    {
      PXTable table = totalField.Table;
      string alias = table.Alias;
      string name = totalField.Name;
      string str1 = name;
      string str2 = $"{alias}_{str1}";
      PXCommandPreparingEventArgs.FieldDescription description;
      this._graph.Caches[table.CacheType].RaiseCommandPreparing(name, (object) null, (object) null, PXDBOperation.Select, table.BqlTable, out description);
      PXDbType type = description != null ? description.DataType : PXDbType.Unspecified;
      Column operand = new Column(str2, "InnerQuery", type);
      SQLExpression sqlExpression = ValFromStr.GetSqlExpression(totalField.Function, (SQLExpression) operand).As(str2);
      sqlExpressionList2.Add(sqlExpression);
      for (int index = 0; index < this.RecordMapEntries.Count; ++index)
      {
        PXDataRecordMap.FieldEntry recordMapEntry = this.RecordMapEntries[index];
        if (string.Equals(this._recordMapEntryAliases[index], totalField.Table.Alias, StringComparison.OrdinalIgnoreCase) && recordMapEntry.TableType == table.BqlTable)
        {
          AggregateFunction? function = totalField.Function;
          AggregateFunction aggregateFunction = AggregateFunction.Count;
          if (!(function.GetValueOrDefault() == aggregateFunction & function.HasValue) && string.Equals(recordMapEntry.FieldName, totalField.Name, StringComparison.OrdinalIgnoreCase) || string.Equals(recordMapEntry.FieldName, totalField.Alias, StringComparison.OrdinalIgnoreCase))
          {
            recordMapEntry.PositionInResult = this._positionInResult++;
            recordMapEntry.PositionInQuery = this._positionInQuery++;
            break;
          }
        }
      }
    }
    if (this.Description.RetrieveTotalRowCount)
    {
      sqlExpressionList2.Add(SQLExpression.Count((SQLExpression) new Asterisk()));
      this.AddRecordMapEntry((PXTable) null, (string) null, this._positionInResult++, this._positionInQuery++);
    }
    sqlExpressionList1.ForEach((System.Action<SQLExpression>) (col => query.Field(col)));
    Table t = query.As("InnerQuery");
    query = new Query();
    query.AddJoin(new Joiner(Joiner.JoinType.MAIN_TABLE, t, query));
    selection.ColExprs.Clear();
    sqlExpressionList2.ForEach(new System.Action<SQLExpression>(selection.AddExpr));
    return query;
  }

  private PXDataRecordMap.FieldEntry AddRecordMapEntry(
    PXTable table,
    string field,
    int positionInResult = -1,
    int positionInQuery = -1)
  {
    this._recordMapEntryAliases.Add(table?.Alias);
    PXGraph owner = (table != null ? (PXGraph) table.Graph : (PXGraph) null) ?? this._graph;
    GIDataRecordMap.GIFieldEntry giFieldEntry = new GIDataRecordMap.GIFieldEntry(field, table, positionInResult, positionInQuery, owner);
    this.RecordMapEntries.Add((PXDataRecordMap.FieldEntry) giFieldEntry);
    return (PXDataRecordMap.FieldEntry) giFieldEntry;
  }

  protected int InsertFieldsExpressions(
    BqlCommand.Selection selection,
    int parnum,
    ISqlDialect sqlDialect)
  {
    this._tables = new List<PXTable>();
    if (this.Description.Relations.Count > 0)
    {
      List<string> stringList = new List<string>();
      foreach (PXRelation relation in this.Description.Relations)
      {
        if (!stringList.Contains(relation.First.Alias))
          this.InsertFieldsExpressions(relation.First, selection);
        if (!stringList.Contains(relation.Second.Alias))
          this.InsertFieldsExpressions(relation.Second, selection);
        stringList.Add(relation.First.Alias);
        stringList.Add(relation.Second.Alias);
      }
    }
    else
    {
      PXTable table = this.Description.UsedTables.FirstOrDefault<KeyValuePair<string, PXTable>>().Value ?? this.Description.Tables.FirstOrDefault<KeyValuePair<string, PXTable>>((Func<KeyValuePair<string, PXTable>, bool>) (p => p.Value.IsInDbExist)).Value;
      if (table != null)
        this.InsertFieldsExpressions(table, selection);
    }
    parnum = this.InsertExtFieldsExpressions(selection, parnum, sqlDialect);
    this.InsertTotalFieldsExpressions();
    return parnum;
  }

  protected void InsertFieldsExpressions(PXTable table, BqlCommand.Selection selection)
  {
    this._tables.Add(table);
    PXCache cach = ((PXGraph) table.Graph ?? this._graph).Caches[table.CacheType];
    TableChangingScope.InsertIsNewIfNeeded(table.CacheType, cach, selection, table.Alias);
    PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(table.CacheType, cach, selection);
    bool flag1 = cach.BqlSelect != null || table.Graph != null;
    bool flag2 = false;
    foreach (string field in (List<string>) cach.Fields)
    {
      bool flag3 = cach._ReportGetFirstKeyValueStored(field) != null || cach._ReportGetFirstKeyValueAttribute(field) != null;
      if (this.IsRestricted(selection, $"{table.Alias}_{field}") & flag3)
      {
        flag2 = true;
        break;
      }
    }
    try
    {
      TableChangingScope.SetCurrentLevelTable(table.Alias);
      foreach (string field in (List<string>) cach.Fields)
      {
        SQLExpression sqlExpression1 = (SQLExpression) null;
        PXDBOperation operation = PXDBOperation.Select;
        Column fieldCol = new Column(field);
        if (this.HasGrouping && !this.Description.GroupBys.Any<PXGroupBy>((Func<PXGroupBy, bool>) (g => g.DataField.GetExpression((Func<string, SQLExpression>) (s => (SQLExpression) new Column(s))).Equals((SQLExpression) fieldCol))))
          operation |= PXDBOperation.GroupBy;
        if (string.Equals(field, "NoteID", StringComparison.OrdinalIgnoreCase))
        {
          cach.SetValueExt((object) null, "NoteText", (object) null);
          cach.SetValueExt((object) null, "NoteFiles", (object) null);
        }
        bool flag4 = cach._ReportGetFirstKeyValueStored(field) != null || cach._ReportGetFirstKeyValueAttribute(field) != null;
        PXGenericInqGrph graph = table.Graph;
        int num;
        if (graph == null)
        {
          num = 0;
        }
        else
        {
          bool? nullable = graph.Columns?.Contains(field);
          bool flag5 = true;
          num = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
        }
        if (num != 0 && !this.IsAttributesField(field) && !this.IsDescriptionField(field))
          operation |= PXDBOperation.External;
        PXCommandPreparingEventArgs.FieldDescription description1;
        cach.RaiseCommandPreparing(field, (object) null, (object) null, operation, table.CacheType, out description1);
        if (description1?.Expr != null)
        {
          PXDataRecordMap.FieldEntry fieldEntry = this.AddRecordMapEntry(table, field, this._positionInResult);
          if (this.IsRestricted(selection, $"{table.Alias}_{field}") || flag2 & flag4 || BqlCommand.IsFieldRecordStatus(cach, field))
          {
            if (description1.Expr.IsNullExpression())
            {
              if (!flag1 && (operation & PXDBOperation.Option) == PXDBOperation.GroupBy)
              {
                PXCommandPreparingEventArgs.FieldDescription description2;
                cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, table.CacheType, out description2);
                if (description2?.Expr != null)
                  sqlExpression1 = this.ApplyAggregateToFieldsInSubquery(this.ReplaceWithAlias(description2.Expr, table), field, table, cach);
              }
              if (sqlExpression1 == null)
                sqlExpression1 = description1.Expr.Duplicate().As($"{table.Alias}_{field}");
            }
            else
            {
              SQLExpression sqlExpression2 = this.GetSqlExpressionStringToAggregateField((SQLExpression) new SQLConst((object) 1), table, field, description1.DataType);
              SQLExpression operand;
              if (flag1)
              {
                operand = (SQLExpression) new Column(field, table.Alias);
              }
              else
              {
                operand = this.ReplaceWithAlias(description1.Expr, table);
                if (operand.GetExpressionsOfType<SubQuery>().Any<SubQuery>())
                  sqlExpression2 = (SQLExpression) null;
              }
              if (sqlExpression2 != null)
                operand = this.GetSqlExpressionStringToAggregateField(operand, table, field, description1.DataType);
              sqlExpression1 = operand.As($"{table.Alias}_{field}");
            }
            fieldEntry.PositionInQuery = this._positionInQuery;
            ++this._positionInQuery;
          }
          ++this._positionInResult;
          if (sqlExpression1 != null)
          {
            if ((sqlExpression1.IsNullExpression() || sqlExpression1 is SubQuery) && sqlExpression1.Alias() == null)
              sqlExpression1 = sqlExpression1.As($"{table.Alias}_{field}");
            selection.ColExprs.Add(sqlExpression1);
          }
        }
      }
    }
    finally
    {
      TableChangingScope.RemoveCurrentLevelTable(table.Alias);
    }
  }

  protected int InsertExtFieldsExpressions(
    BqlCommand.Selection selection,
    int parnum,
    ISqlDialect sqlDialect)
  {
    HashSet<Tuple<string, SQLExpression, PXTable>> accessorySelections = new HashSet<Tuple<string, SQLExpression, PXTable>>();
    foreach (PXExtField extField in this.Description.ExtFields)
    {
      SQLExpression sqlExpression1 = (SQLExpression) null;
      bool flag1 = this.IsRestricted(selection, $"{extField.Table.Alias}_{extField.Name}");
      bool flag2 = false;
      bool flag3 = false;
      PXFormulaField formula = extField as PXFormulaField;
      AggregateFunction? function;
      string field;
      if (formula != null)
      {
        int num1;
        if (!flag1)
        {
          function = formula.Function;
          AggregateFunction aggregateFunction = AggregateFunction.Count;
          num1 = function.GetValueOrDefault() == aggregateFunction & function.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        flag2 = num1 != 0;
        int num2;
        if (!flag1)
        {
          function = formula.Function;
          AggregateFunction aggregateFunction = AggregateFunction.StringAgg;
          num2 = function.GetValueOrDefault() == aggregateFunction & function.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        flag3 = num2 != 0;
        field = formula.Name;
        string a = $"{formula.Table.Alias}_{formula.Name}";
        (SQLExpression LeftPart, int ParamNumber, List<(SQLExpression SqlExpr, string SqlText)> NameExpressionsWithSqlText1) = this.GetSqlExpressionForFormulaLeftPart(formula, sqlDialect, parnum, false);
        bool flag4 = LeftPart.GetExpressionsOfType<SubQuery>().Count > 0;
        bool flag5 = this.HasGrouping && !flag4;
        SQLExpression sqlExpression2;
        List<(SQLExpression, string)> NameExpressionsWithSqlText2;
        if (this.HasGrouping & flag4)
        {
          (sqlExpression2, parnum, NameExpressionsWithSqlText2) = this.GetSqlExpressionForFormulaLeftPart(formula, sqlDialect, parnum, true);
        }
        else
        {
          sqlExpression2 = LeftPart;
          parnum = ParamNumber;
          NameExpressionsWithSqlText2 = NameExpressionsWithSqlText1;
        }
        if (flag1 || sqlExpression2 is Md5Hash)
        {
          AggregateFunction? type = formula.Function;
          if (flag5 && !type.HasValue && !sqlExpression2.DoesContainAggregate())
          {
            type = new AggregateFunction?(AggregateFunction.Max);
            if (this.UseHavingForFilters && sqlExpression2.GetExpressionsOfType(SQLExpression.Operation.ISNULL_FUNC).Any<SQLExpression>())
              NameExpressionsWithSqlText2.ForEach((System.Action<(SQLExpression, string)>) (n => accessorySelections.Add(Tuple.Create<string, SQLExpression, PXTable>(n.SqlText, n.SqlExpr, formula.Table))));
          }
          sqlExpression1 = !flag5 || !type.HasValue ? sqlExpression2.Embrace() : ValFromStr.GetSqlExpression(type, sqlExpression2);
          sqlExpression1.SetAlias(a);
        }
      }
      else
      {
        field = extField is PXAggregateField aggregate ? aggregate.Alias : throw new NotSupportedException();
        function = aggregate.Function;
        AggregateFunction aggregateFunction1 = AggregateFunction.CountAll;
        if (!(function.GetValueOrDefault() == aggregateFunction1 & function.HasValue))
        {
          function = aggregate.Function;
          AggregateFunction aggregateFunction2 = AggregateFunction.Count;
          if (!(function.GetValueOrDefault() == aggregateFunction2 & function.HasValue))
          {
            function = aggregate.Function;
            AggregateFunction aggregateFunction3 = AggregateFunction.StringAgg;
            if (!(function.GetValueOrDefault() == aggregateFunction3 & function.HasValue))
              continue;
          }
        }
        function = aggregate.Function;
        AggregateFunction aggregateFunction4 = AggregateFunction.StringAgg;
        SQLExpression sqlExpression3 = function.GetValueOrDefault() == aggregateFunction4 & function.HasValue ? this.GetSqlExpressionForStringAggField(aggregate, ref parnum, true) : this.GetSqlExpressionForCountField(aggregate, ref parnum, true);
        if (!flag1)
          flag1 = this.IsRestricted(selection, $"{aggregate.Table.Alias}_{aggregate.Alias}");
        if (flag1)
          sqlExpression1 = sqlExpression3;
      }
      if (sqlExpression1 != null)
        selection.ColExprs.Add(sqlExpression1);
      int positionInQuery = -1;
      if (flag1)
        positionInQuery = this._positionInQuery++;
      if (!flag2 && !flag3)
        this.AddRecordMapEntry(extField.Table, field, this._positionInResult++, positionInQuery);
    }
    foreach (Tuple<string, SQLExpression, PXTable> tuple in accessorySelections)
      selection.AddExpr(tuple.Item2);
    return parnum;
  }

  private (SQLExpression LeftPart, int ParamNumber, List<(SQLExpression SqlExpr, string SqlText)> NameExpressionsWithSqlText) GetSqlExpressionForFormulaLeftPart(
    PXFormulaField formula,
    ISqlDialect sqlDialect,
    int inputParamNumber,
    bool applyAggregates)
  {
    List<(SQLExpression, string)> nameExpressionsWithSqlText = new List<(SQLExpression, string)>();
    return (formula.Value.GetExpression((Func<string, SQLExpression>) (name =>
    {
      string str1 = ((IEnumerable<string>) ((IEnumerable<string>) name.Split('.')).Last<string>().Split('_')).Last<string>();
      bool forceExt = str1.StartsWith("Attribute", StringComparison.OrdinalIgnoreCase) && !str1.Equals("Attributes", StringComparison.OrdinalIgnoreCase);
      SQLExpression forFormulaLeftPart = this.ParameterHandlerExpression(name, ref inputParamNumber, PXDBOperation.SelectClause, forceExt, applyAggregates);
      string a = name.Replace('.', '_');
      SQLExpression sqlExpression = forFormulaLeftPart.Duplicate().As(a);
      string str2 = sqlExpression.SQLQuery(sqlDialect.GetConnection()).ToString();
      nameExpressionsWithSqlText.Add((sqlExpression, str2));
      return forFormulaLeftPart;
    })), inputParamNumber, nameExpressionsWithSqlText);
  }

  private SQLExpression ReplaceWithAlias(SQLExpression query, PXTable table)
  {
    return query.Duplicate().substituteTableName(table.CacheType.Name, table.Alias);
  }

  /// <summary>
  /// In sub-queries, replaces original DAC columns with sub-GI columns.
  /// DAC.Field =&gt; SubGI.DACAlias_Field
  /// </summary>
  private SQLExpression ReplaceWithSubGiColumn(
    SQLExpression query,
    PXTable table,
    string fieldName,
    out bool skipTestAggregate)
  {
    if (query is Column)
    {
      skipTestAggregate = true;
      return (SQLExpression) new Column($"{table.Alias}_{fieldName}");
    }
    System.Type cacheType = this.GetRealCacheType(table, fieldName);
    int length = this.TrimSpecialFieldsSuffixIfNeeded(fieldName).LastIndexOf('_');
    string str = length == -1 ? string.Empty : fieldName.Substring(0, length);
    SQLExpression sqlExpression = query.Duplicate();
    IEnumerable<Column> columns = sqlExpression.GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (x => string.Equals(cacheType.Name, x.Table()?.AliasOrName(), StringComparison.OrdinalIgnoreCase)));
    skipTestAggregate = false;
    foreach (Column from in columns)
    {
      Column to = new Column($"{str}_{from.Name}", table.Alias);
      sqlExpression.substituteNode((SQLExpression) from, (SQLExpression) to);
    }
    return sqlExpression;
  }

  private string TrimSpecialFieldsSuffixIfNeeded(string fieldName)
  {
    return this.IsDescriptionField(fieldName) || this.IsAttributesField(fieldName) ? fieldName.Substring(0, fieldName.LastIndexOf('_')) : fieldName;
  }

  private System.Type GetRealCacheType(PXTable table, string fieldName)
  {
    if (table.Graph == null)
      return table.CacheType;
    string[] strArray = fieldName.Split(new char[1]{ '_' }, 2);
    PXTable table1;
    return strArray.Length != 2 || !table.Graph.BaseQueryDescription.Tables.TryGetValue(strArray[0], out table1) ? table.CacheType : this.GetRealCacheType(table1, strArray[1]);
  }

  private SQLExpression GetSqlExpressionForCountField(
    PXAggregateField aggregate,
    ref int parnum,
    bool appendAlias)
  {
    AggregateFunction? function1 = aggregate.Function;
    AggregateFunction aggregateFunction1 = AggregateFunction.CountAll;
    if (!(function1.GetValueOrDefault() == aggregateFunction1 & function1.HasValue))
    {
      AggregateFunction? function2 = aggregate.Function;
      AggregateFunction aggregateFunction2 = AggregateFunction.Count;
      if (!(function2.GetValueOrDefault() == aggregateFunction2 & function2.HasValue))
        throw new ArgumentException("aggregateField");
    }
    string a = $"{aggregate.Table.Alias}_{aggregate.Alias}";
    AggregateFunction? function3 = aggregate.Function;
    AggregateFunction aggregateFunction3 = AggregateFunction.CountAll;
    if (function3.GetValueOrDefault() == aggregateFunction3 & function3.HasValue)
    {
      SQLExpression expressionForCountField = SQLExpression.Count((SQLExpression) new Asterisk());
      if (appendAlias)
        expressionForCountField.SetAlias(a);
      return expressionForCountField;
    }
    SQLExpression sqlExpression = SQLExpression.CountDistinct(this.GetLeftPart(aggregate, ref parnum));
    return !appendAlias ? sqlExpression : sqlExpression.As(a);
  }

  private SQLExpression GetSqlExpressionForStringAggField(
    PXAggregateField aggregate,
    ref int parnum,
    bool appendAlias)
  {
    AggregateFunction? function = aggregate.Function;
    AggregateFunction aggregateFunction = AggregateFunction.StringAgg;
    if (!(function.GetValueOrDefault() == aggregateFunction & function.HasValue))
      throw new ArgumentException("aggregateField");
    string a = $"{aggregate.Table.Alias}_{aggregate.Alias}";
    SQLExpression sqlExpression = this.GetLeftPart(aggregate, ref parnum).StringAgg();
    return !appendAlias ? sqlExpression : sqlExpression.As(a);
  }

  private SQLExpression GetLeftPart(PXAggregateField aggregate, ref int parnum)
  {
    SQLExpression leftPart = (SQLExpression) null;
    PXFormulaField pxFormulaField = this.Description.FormulaFields.FirstOrDefault<PXFormulaField>((Func<PXFormulaField, bool>) (f => string.Equals(f.Name, aggregate.Name, StringComparison.OrdinalIgnoreCase)));
    if (pxFormulaField != null)
    {
      int parnumTemp = parnum;
      leftPart = pxFormulaField.Value.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumTemp, PXDBOperation.SelectClause)));
      parnum = parnumTemp;
    }
    else
    {
      PXTable table = aggregate.Table;
      PXCache cach = ((PXGraph) table.Graph ?? this._graph).Caches[table.CacheType];
      PXDBOperation operation = this.HasGrouping ? PXDBOperation.GroupBy : PXDBOperation.Select;
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(aggregate.Name, (object) null, (object) null, operation, table.CacheType, out description);
      if (description?.Expr != null && !description.Expr.IsNullExpression())
      {
        if (cach.BqlSelect != null || table.Graph != null)
        {
          leftPart = (SQLExpression) new Column(aggregate.Name, table.Alias);
        }
        else
        {
          leftPart = description.Expr.Duplicate();
          leftPart.substituteTableName(table.CacheType.Name, table.Alias);
        }
      }
      else if (description?.Expr != null && description.Expr.IsNullExpression())
        leftPart = description.Expr;
    }
    return leftPart;
  }

  protected void InsertTotalFieldsExpressions()
  {
    foreach (PXAggregateField pxAggregateField in this.Description.TotalFields.Where<PXAggregateField>((Func<PXAggregateField, bool>) (f =>
    {
      AggregateFunction? function = f.Function;
      AggregateFunction aggregateFunction = AggregateFunction.Count;
      return function.GetValueOrDefault() == aggregateFunction & function.HasValue && !this.Description.AggregateFields.Contains<PXAggregateField>(f);
    })))
      this.AddRecordMapEntry(pxAggregateField.Table, pxAggregateField.Alias, this._positionInResult++);
  }

  protected int AppendFROM(
    Query query,
    int parnum,
    IDictionary<string, HashSet<string>> usedColumns,
    out ISet<string> usedTableAliases)
  {
    usedTableAliases = (ISet<string>) new HashSet<string>(System.Math.Max(1, this.Description.Relations.Count), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (this.Description.Relations.Count > 0)
    {
      PXRelation relation1 = this.Description.Relations[0];
      PXTable first = relation1.First;
      query.From(this.GetSQLTableFromChangingScope(first, this._graph, usedColumns, ref parnum).As(relation1.First.Alias));
      usedTableAliases.Add(first.Alias);
      PXRelation pxRelation = (PXRelation) null;
      int num = 0;
      foreach (PXRelation relation2 in this.Description.Relations)
      {
        ++num;
        bool flag = true;
        Joiner j;
        SQLExpression sqlExpression;
        if (pxRelation == null || !pxRelation.Second.Equals(relation2.Second))
        {
          int sqlJoinType = (int) ValFromStr.GetSqlJoinType(relation2.Relation);
          PXTable second = relation2.Second;
          Table table = this.GetSQLTableFromChangingScope(second, this._graph, usedColumns, ref parnum).As(relation2.Second.Alias);
          usedTableAliases.Add(second.Alias);
          Table t = table;
          Query q = query;
          j = new Joiner((Joiner.JoinType) sqlJoinType, t, q);
          query.AddJoin(j);
          sqlExpression = TableChangingScope.ProcessOnsForGI((IReadOnlyList<PXRelation>) this.Description.Relations, num - 1, this._graph);
        }
        else
        {
          j = query.GetFrom().Last<Joiner>();
          sqlExpression = j.getOn();
          flag = false;
        }
        if (relation2.Relation != RelationType.Cross || !flag)
        {
          SQLExpression onConditions = this.GetONConditions(relation2, ref parnum);
          SQLExpression e = sqlExpression?.And(onConditions) ?? onConditions;
          j.On(e);
        }
        pxRelation = relation2;
      }
    }
    else
    {
      PXTable table = this.Description.UsedTables.FirstOrDefault<KeyValuePair<string, PXTable>>().Value ?? this.Description.Tables.FirstOrDefault<KeyValuePair<string, PXTable>>((Func<KeyValuePair<string, PXTable>, bool>) (p => p.Value.IsInDbExist)).Value;
      if (table != null)
      {
        query.From(this.GetSQLTableFromChangingScope(table, this._graph, usedColumns, ref parnum).As(table.Alias));
        usedTableAliases.Add(table.Alias);
      }
    }
    return parnum;
  }

  /// <exception cref="T:PX.Data.PXException">Thrown when brackets are unbalanced</exception>
  protected SQLExpression GetONConditions(PXRelation rel, ref int parnum)
  {
    int parnumLocal = parnum;
    PX.Data.Description.GI.Operation operation = PX.Data.Description.GI.Operation.And;
    int num1 = 0;
    List<(SQLExpression, PX.Data.Description.GI.Operation)> bracketRegion = new List<(SQLExpression, PX.Data.Description.GI.Operation)>();
    Stack<List<(SQLExpression, PX.Data.Description.GI.Operation)>> valueTupleListStack = new Stack<List<(SQLExpression, PX.Data.Description.GI.Operation)>>();
    valueTupleListStack.Push(bracketRegion);
    bool flag = false;
    foreach (PXOnCond pxOnCond in rel.On)
    {
      flag = true;
      if (pxOnCond.OpenBrackets > 0)
      {
        num1 += pxOnCond.OpenBrackets;
        for (int index = 0; index < pxOnCond.OpenBrackets; ++index)
          valueTupleListStack.Push(new List<(SQLExpression, PX.Data.Description.GI.Operation)>());
      }
      SQLExpression expression1 = pxOnCond.FirstField.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause)), true);
      SQLExpression right = (SQLExpression) null;
      if (pxOnCond.SecondField != null)
        right = pxOnCond.SecondField.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause)), true);
      SQLExpression sqlExpression = ValFromStr.GetSqlExpression(pxOnCond.Cond, expression1, right);
      valueTupleListStack.Peek().Add((sqlExpression, operation));
      operation = pxOnCond.Op;
      if (pxOnCond.CloseBrackets > 0 && num1 > 0)
      {
        int num2 = pxOnCond.CloseBrackets <= num1 ? pxOnCond.CloseBrackets : num1;
        num1 -= num2;
        if (valueTupleListStack.Count < num2 + 1)
          throw new PXException("Brackets in the On condition are not balanced.");
        for (int index = 0; index < num2; ++index)
        {
          List<(SQLExpression, PX.Data.Description.GI.Operation)> valueTupleList = valueTupleListStack.Pop();
          SQLExpression expression2 = this.GetExpression(valueTupleList);
          if (expression2 != null)
            valueTupleListStack.Peek().Add((expression2, valueTupleList.First<(SQLExpression, PX.Data.Description.GI.Operation)>().Item2));
        }
      }
    }
    while (valueTupleListStack.Count > 1)
    {
      List<(SQLExpression, PX.Data.Description.GI.Operation)> valueTupleList = valueTupleListStack.Pop();
      SQLExpression expression = this.GetExpression(valueTupleList);
      if (expression != null)
        valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, PX.Data.Description.GI.Operation)>().Item2));
    }
    SQLExpression onConditions = !flag ? new SQLConst((object) 1).GT((SQLExpression) new SQLConst((object) 0)).Embrace() : this.GetExpression(bracketRegion);
    parnum = parnumLocal;
    return onConditions;
  }

  /// <exception cref="T:PX.Data.PXException">Thrown when brackets are unbalanced</exception>
  protected SQLExpression GetWHERE(
    List<PXWhereCond> wheres,
    ref int parnum,
    bool applyAggregate,
    ISet<string> usedTableAliases,
    bool omitMissingTables)
  {
    if (wheres == null || wheres.Count == 0)
      return (SQLExpression) null;
    int parnumLocal = parnum;
    int num1 = 0;
    PX.Data.Description.GI.Operation operation = PX.Data.Description.GI.Operation.And;
    List<(SQLExpression, PX.Data.Description.GI.Operation)> bracketRegion = new List<(SQLExpression, PX.Data.Description.GI.Operation)>();
    Stack<List<(SQLExpression, PX.Data.Description.GI.Operation)>> valueTupleListStack = new Stack<List<(SQLExpression, PX.Data.Description.GI.Operation)>>();
    valueTupleListStack.Push(bracketRegion);
    foreach (PXWhereCond where in wheres)
    {
      if (where.OpenBrackets > 0)
      {
        num1 += where.OpenBrackets;
        for (int index = 0; index < where.OpenBrackets; ++index)
          valueTupleListStack.Push(new List<(SQLExpression, PX.Data.Description.GI.Operation)>());
      }
      if (where.DataField == null)
        throw new PXException("Empty table in \"Where\" clause");
      if (where.DataField is PXFieldValue && where.Table == null)
        throw new PXException("Empty table in \"Where\" clause");
      SQLExpression sqlExpression1;
      try
      {
        IPXValue dataField = where.DataField;
        bool flag = dataField.ToString().StartsWith("=");
        bool forceExt = where.UseExt && !flag;
        SQLExpression sqlExpression2;
        if (this.HasGrouping & flag)
        {
          PXExtField pxExtField = this.Description.ExtFields.First<PXExtField>((Func<PXExtField, bool>) (x => dataField.Equals(x is PXFormulaField pxFormulaField ? pxFormulaField.Value : (IPXValue) null)));
          SQLExpression expression = dataField.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause, forceExt)), true);
          sqlExpression2 = !expression.DoesContainAggregate() ? ValFromStr.GetSqlExpression(new AggregateFunction?((AggregateFunction) ((int) pxExtField.Function ?? 3)), expression) : expression;
        }
        else
          sqlExpression2 = where.DataField.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause, forceExt, applyAggregate)), true);
        if (where.Table?.Graph != null && sqlExpression2 is SubQuery && dataField is PXFieldValue pxFieldValue)
        {
          string fieldName = ((IEnumerable<string>) pxFieldValue.FieldName.Split('.')).Last<string>();
          sqlExpression2 = this.ReplaceWithSubGiColumn(sqlExpression2, where.Table, fieldName, out bool _);
        }
        SQLExpression right = (SQLExpression) null;
        Condition cond = where.Cond;
        if (where.Cond != Condition.IsNull && where.Cond != Condition.NotNull)
        {
          if (where.Value1 == null)
            throw new PXException("The {0} cannot be empty for the {1} condition.", new object[2]
            {
              (object) "Value1",
              (object) where.Cond.ToString()
            });
          SQLExpression expression1 = where.Value1.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause)), true);
          switch (where.Cond)
          {
            case Condition.Between:
              if (where.Value2 == null)
                throw new PXException("The {0} cannot be empty for the {1} condition.", new object[2]
                {
                  (object) "Value2",
                  (object) where.Cond.ToString()
                });
              SQLExpression expression2 = where.Value2.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnumLocal, PXDBOperation.WhereClause)), true);
              right = SQLExpression.BetweenArgs(expression1, expression2);
              break;
            case Condition.In:
            case Condition.NotIn:
              if (expression1 == null)
              {
                cond = Condition.NotEqual;
                right = sqlExpression2.Duplicate();
                break;
              }
              right = expression1;
              break;
            default:
              right = expression1;
              break;
          }
        }
        else if (CanApplyNullIf(sqlExpression2))
          sqlExpression2 = sqlExpression2.NullIf((SQLExpression) new SQLConst((object) string.Empty));
        if (where.Table?.Alias != null && where.DataField is PXFieldValue dataField1 && dataField1 != null && !usedTableAliases.Contains(where.Table.Alias))
        {
          if (!omitMissingTables)
            throw new PXException("The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new object[2]
            {
              (object) dataField1.FieldName,
              (object) where.Table.Alias
            });
          sqlExpression1 = SQLExpressionExt.EQ(new SQLConst((object) 1), (SQLExpression) new SQLConst((object) 1));
        }
        else
          sqlExpression1 = ValFromStr.GetSqlExpression(cond, sqlExpression2, right);
      }
      catch (PXVirtualFieldException ex)
      {
        sqlExpression1 = SQLExpressionExt.EQ(new SQLConst((object) 1), (SQLExpression) new SQLConst((object) 1));
      }
      valueTupleListStack.Peek().Add((sqlExpression1, operation));
      operation = where.Op;
      if (where.CloseBrackets > 0 && num1 > 0)
      {
        int num2 = where.CloseBrackets <= num1 ? where.CloseBrackets : num1;
        num1 -= num2;
        if (valueTupleListStack.Count < num2 + 1)
          throw new PXException("Brackets in the Where condition are not balanced.");
        for (int index = 0; index < num2; ++index)
        {
          List<(SQLExpression, PX.Data.Description.GI.Operation)> valueTupleList = valueTupleListStack.Pop();
          SQLExpression expression = this.GetExpression(valueTupleList);
          if (expression != null)
            valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, PX.Data.Description.GI.Operation)>().Item2));
        }
      }
    }
    while (valueTupleListStack.Count > 1)
    {
      List<(SQLExpression, PX.Data.Description.GI.Operation)> valueTupleList = valueTupleListStack.Pop();
      SQLExpression expression = this.GetExpression(valueTupleList);
      if (expression != null)
        valueTupleListStack.Peek().Add((expression, valueTupleList.First<(SQLExpression, PX.Data.Description.GI.Operation)>().Item2));
    }
    SQLExpression expression3 = this.GetExpression(bracketRegion);
    parnum = parnumLocal;
    return expression3;

    static bool CanApplyNullIf(SQLExpression v)
    {
      switch (v.GetDBType())
      {
        case PXDbType.NChar:
        case PXDbType.NText:
        case PXDbType.NVarChar:
        case PXDbType.Text:
        case PXDbType.VarChar:
        case PXDbType.Variant:
        case PXDbType.Xml:
          return true;
        default:
          return false;
      }
    }
  }

  private SQLExpression GetExpression(
    List<(SQLExpression Expr, PX.Data.Description.GI.Operation PrevOper)> bracketRegion)
  {
    if (bracketRegion == null || !bracketRegion.Any<(SQLExpression, PX.Data.Description.GI.Operation)>())
      return (SQLExpression) null;
    List<SQLExpression> args = new List<SQLExpression>();
    PX.Data.Description.GI.Operation op = PX.Data.Description.GI.Operation.And;
    foreach ((SQLExpression Expr, PX.Data.Description.GI.Operation PrevOper) tuple in bracketRegion)
    {
      if (args.Count > 1 && op != tuple.PrevOper)
      {
        SQLExpression sqlExpression = ValFromStr.GetSqlExpression(op, (IEnumerable<SQLExpression>) args);
        args.Clear();
        args.Add(sqlExpression);
      }
      args.Add(tuple.Expr);
      if (args.Count == 2)
        op = tuple.PrevOper;
    }
    return (args.Count == 1 ? args[0] : ValFromStr.GetSqlExpression(op, (IEnumerable<SQLExpression>) args))?.Embrace();
  }

  protected void AppendRestriction(Query query)
  {
    if (this._maskedTypes.Count <= 0)
      return;
    byte[] referencedValue = GroupHelper.GetReferencedValue(this._graph.Caches[typeof (AccessInfo)], (object) this._graph.Accessinfo, "UserName", (object) this._graph.Accessinfo.UserName, this._graph._ForceUnattended) as byte[];
    SQLExpression w = SQLExpression.None();
    foreach (KeyValuePair<System.Type, string> maskedType in this._maskedTypes)
    {
      uint num = 0;
      SQLExpression r1 = SQLExpression.None();
      SQLExpression r2 = SQLExpression.None();
      Column column = new Column("GroupMask", maskedType.Value);
      foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(typeof (Users), maskedType.Key, referencedValue))
      {
        SQLExpression r3 = SQLExpressionExt.EQ(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) num * 4 + 1), 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.First)));
        SQLExpression r4 = SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) num * 4 + 1), 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.Second)));
        ++num;
        r1 = r1.And(r3);
        r2 = r2.Or(r4);
      }
      if (num > 0U)
      {
        SQLExpression r5 = column.IsNull().Or(r1).Or(r2).Embrace();
        w = w.And(r5);
      }
    }
    if (w.Oper() == SQLExpression.Operation.NONE)
      return;
    query.AndWhere(w);
  }

  protected int AppendOrder(Query query, int parnum, ISet<string> usedTableAliases)
  {
    if (this.Description.Sorts.Count == 0)
      return parnum;
    List<OrderSegment> order = query.GetOrder();
    HashSet<string> existingOrders = (order != null ? order.Select<OrderSegment, string>((Func<OrderSegment, string>) (x => x.expr_.ToString())).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : (HashSet<string>) null) ?? new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXSort sort1 in this.Description.Sorts)
    {
      PXSort sort = sort1;
      if (sort.DataField == null)
        throw new PXException("Empty table in \"Where\" clause");
      if (sort.DataField is PXFieldValue && sort.Table == null)
        throw new PXException("Empty table in \"Where\" clause");
      try
      {
        OrderSegment orderSegment;
        if (sort.DataField is PXCalcedValue dataField1)
        {
          Func<string, SQLExpression> onParameter = (Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnum, PXDBOperation.OrderByClause));
          SQLExpression expression = dataField1.GetExpression(onParameter, false);
          orderSegment = new OrderSegment(this.GetSqlExpressionFormulaToAggregateField(expression, PXDbType.DirectExpression) ?? expression, sort.Order == SortOrder.Asc);
        }
        else
        {
          PXFieldValue dataField = sort.DataField as PXFieldValue;
          if (!usedTableAliases.Contains(sort.Table.Alias))
            throw new PXException("The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new object[2]
            {
              (object) dataField.FieldName,
              (object) sort.Table.Alias
            });
          string fieldName = ((IEnumerable<string>) dataField.FieldName.Split('.')).Last<string>();
          SQLExpression sqlExpression;
          if (fieldName != null && this.Description.FormulaFields.Any<PXFormulaField>((Func<PXFormulaField, bool>) (f => string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase))))
          {
            sqlExpression = (SQLExpression) new Column(sort.DataField is PXCalcedValue ? fieldName : $"{sort.Table.Alias}_{fieldName}");
          }
          else
          {
            PXCache cach = ((PXGraph) sort.Table.Graph ?? this._graph).Caches[sort.Table.CacheType];
            PXAggregateField aggregate1 = this.Description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (f =>
            {
              if (!f.Table.Equals(sort.Table) || !string.Equals(f.Alias, fieldName, StringComparison.OrdinalIgnoreCase))
                return false;
              AggregateFunction? function1 = f.Function;
              AggregateFunction aggregateFunction1 = AggregateFunction.Count;
              if (function1.GetValueOrDefault() == aggregateFunction1 & function1.HasValue)
                return true;
              AggregateFunction? function2 = f.Function;
              AggregateFunction aggregateFunction2 = AggregateFunction.CountAll;
              return function2.GetValueOrDefault() == aggregateFunction2 & function2.HasValue;
            }));
            PXAggregateField aggregate2 = this.Description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (f =>
            {
              if (!f.Table.Equals(sort.Table) || !string.Equals(f.Alias, fieldName, StringComparison.OrdinalIgnoreCase))
                return false;
              AggregateFunction? function = f.Function;
              AggregateFunction aggregateFunction = AggregateFunction.StringAgg;
              return function.GetValueOrDefault() == aggregateFunction & function.HasValue;
            }));
            if (aggregate1 != null)
              sqlExpression = this.GetSqlExpressionForCountField(aggregate1, ref parnum, false);
            else if (aggregate2 != null)
            {
              sqlExpression = this.GetSqlExpressionForStringAggField(aggregate2, ref parnum, false);
            }
            else
            {
              PXDBOperation operation = (PXDBOperation) ((this.HasGrouping ? 4 : 0) | 16 /*0x10*/);
              PXCommandPreparingEventArgs.FieldDescription description;
              cach.RaiseCommandPreparing(fieldName, (object) null, (object) null, operation, sort.Table.CacheType, out description);
              if (description?.Expr != null)
              {
                if (!description.Expr.IsNullExpression())
                {
                  bool skipTestAggregate = false;
                  sqlExpression = cach.BqlSelect == null || this.IsAttributesField(fieldName) || this.IsKVExtField(cach, fieldName) || this.IsDescriptionField(fieldName) || description.Expr is SubQuery ? (sort.Table.Graph == null ? this.ReplaceWithAlias(description.Expr, sort.Table) : this.ReplaceWithSubGiColumn(description.Expr, sort.Table, fieldName, out skipTestAggregate)) : (SQLExpression) new Column(fieldName, sort.Table.Alias);
                  if (!skipTestAggregate && this.GetSqlExpressionStringToAggregateField((SQLExpression) new SQLConst((object) 1), sort.Table, fieldName, description.DataType) != null)
                  {
                    if (sqlExpression is SubQuery)
                    {
                      string descriptionField = GenericInquiryHelpers.GetBaseFieldNameForDescriptionField(fieldName);
                      sqlExpression = this.ApplyAggregateToFieldsInSubquery(sqlExpression, descriptionField, sort.Table, cach);
                    }
                    else
                      sqlExpression = this.GetSqlExpressionStringToAggregateField(sqlExpression, sort.Table, fieldName, description.DataType);
                  }
                }
                else
                  continue;
              }
              else
                continue;
            }
          }
          orderSegment = new OrderSegment(sqlExpression, sort.Order == SortOrder.Asc);
        }
        AddUniqueOrderSegment(orderSegment);
      }
      catch (PXVirtualFieldException ex)
      {
      }
    }
    return parnum;

    void AddUniqueOrderSegment(OrderSegment orderSegment)
    {
      string str = orderSegment.expr_.ToString();
      if (existingOrders.Contains(str))
        return;
      query.AddOrderSegment(orderSegment);
      existingOrders.Add(str);
    }
  }

  protected int AppendGroupBy(Query query, int parnum, ISet<string> usedTableAliases)
  {
    if (!this.HasGrouping)
      return parnum;
    List<SQLExpression> gb = new List<SQLExpression>();
    foreach (PXGroupBy groupBy in this.Description.GroupBys)
    {
      if (groupBy.DataField == null)
        throw new PXException("Empty table in \"Where\" clause");
      if (groupBy.DataField is PXFieldValue && groupBy.Table == null)
        throw new PXException("Empty table in \"Where\" clause");
      try
      {
        SQLExpression sqlExpression;
        if (groupBy.DataField is PXCalcedValue dataField1)
        {
          Func<string, SQLExpression> onParameter = (Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnum, PXDBOperation.GroupByClause));
          sqlExpression = dataField1.GetExpression(onParameter, false);
        }
        else
        {
          PXFieldValue dataField = groupBy.DataField as PXFieldValue;
          if (!usedTableAliases.Contains(groupBy.Table.Alias))
            throw new PXException("The {0} field cannot be used because either the table with the {1} alias is not joined with other tables on the Relations tab or the corresponding relation is inactive.", new object[2]
            {
              (object) dataField.FieldName,
              (object) groupBy.Table.Alias
            });
          string fieldName = ((IEnumerable<string>) dataField.FieldName.Split('.')).Last<string>();
          if (this.Description.FormulaFields.Any<PXFormulaField>((Func<PXFormulaField, bool>) (f => f.Name == fieldName)))
            sqlExpression = (SQLExpression) new Column($"{groupBy.Table.Alias}_{fieldName}");
          else if (this.Description.Tables[groupBy.Table.Alias].Graph != null)
          {
            sqlExpression = (SQLExpression) new Column(fieldName, groupBy.Table.Alias);
          }
          else
          {
            PXCache cach = this._graph.Caches[groupBy.Table.CacheType];
            PXCommandPreparingEventArgs.FieldDescription description;
            cach.RaiseCommandPreparing(dataField.FieldName, (object) null, (object) null, PXDBOperation.GroupByClause, groupBy.Table.CacheType, out description);
            if (description?.Expr != null)
            {
              if (!description.Expr.IsNullExpression())
                sqlExpression = cach.BqlSelect != null || description.Expr is SubQuery ? (SQLExpression) new Column(dataField.FieldName, groupBy.Table.Alias) : description.Expr.substituteTableName(groupBy.Table.CacheType.Name, groupBy.Table.Alias);
              else
                continue;
            }
            else
              continue;
          }
        }
        gb.Add(sqlExpression);
      }
      catch (PXVirtualFieldException ex)
      {
      }
    }
    if (gb.Count <= 0)
      return parnum;
    query.GroupBy(gb);
    return parnum;
  }

  protected internal SQLExpression ParameterHandlerExpression(
    string name,
    ref int parnum,
    PXDBOperation place,
    bool forceExt = false,
    bool applyAggregate = false)
  {
    if (name == null)
      return (SQLExpression) Literal.NewParameter(parnum++);
    name = name.Trim(' ', '[', ']', '`');
    SQLExpression sqlExpression1 = (SQLExpression) null;
    PXDbType type = PXDbType.Unspecified;
    PXParameter pxParameter;
    if (this.Description.Parameters.TryGetValue(name, out pxParameter))
    {
      if (pxParameter.Table != (System.Type) null)
      {
        PXCache cach = this._graph.Caches[pxParameter.Table];
        if (cach != null && cach.Fields.Contains(pxParameter.DataField))
        {
          PXCommandPreparingEventArgs.FieldDescription fieldDescription = GetFieldDescription(pxParameter.DataField, cach, cach.GetItemType(), out PXDBOperation _);
          if (fieldDescription?.Expr != null)
            type = fieldDescription.Expr.GetDBType();
        }
        if (cach?.GetStateExt((object) null, pxParameter.DataField) is PXBranchSelectorState)
          sqlExpression1 = Literal.NewParameter(parnum++).InsideBranch();
      }
      SQLExpression sqlExpression2 = sqlExpression1 ?? (SQLExpression) Literal.NewParameter(parnum++);
      if (sqlExpression2 is ISQLDBTypedExpression sqldbTypedExpression)
        sqldbTypedExpression.SetDBType(type);
      return sqlExpression2;
    }
    if (name.Contains("."))
    {
      string[] strArray = name.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length == 2 && this.Description.Tables.ContainsKey(strArray[0]))
      {
        string key = strArray[0];
        string fieldName = strArray[1];
        PXTable table;
        if (this.Description.Tables.TryGetValue(key, out table))
        {
          PXCache cach = ((PXGraph) table.Graph ?? this._graph).Caches[this.Description.Tables[key].CacheType];
          if (cach != null && cach.Fields.Contains(fieldName))
          {
            PXDBOperation operation;
            PXCommandPreparingEventArgs.FieldDescription description = GetFieldDescription(fieldName, cach, table.CacheType, out operation);
            bool isExternal = false;
            if (!forceExt && description?.Expr == null)
            {
              cach.RaiseCommandPreparing(fieldName, (object) null, (object) null, operation | PXDBOperation.External, table.CacheType, out description);
              if (description?.Expr != null)
                isExternal = true;
            }
            if (description?.Expr == null)
              throw new PXVirtualFieldException(fieldName);
            bool flag = this.UseCalculatedExpression(forceExt, cach, fieldName, isExternal, description, table);
            SQLExpression sqlExpression3;
            if (table.Graph == null & flag)
            {
              sqlExpression3 = description.Expr.Duplicate();
              sqlExpression3.substituteTableName(table.CacheType.Name, table.Alias);
            }
            else
              sqlExpression3 = ((!(table.Graph != null & flag) ? 0 : (description.Expr is SubQuery ? 1 : 0)) & (forceExt ? 1 : 0)) == 0 ? (SQLExpression) new Column(fieldName, table.Alias, description.Expr.GetDBType()) : ReplaceDirectColumnsWithSubqueryColumns(table, description.BqlTable, description.Expr, fieldName);
            if (applyAggregate && this.GetSqlExpressionStringToAggregateField((SQLExpression) new SQLConst((object) 1), table, fieldName, description.DataType) != null)
            {
              if (sqlExpression3 is SubQuery || sqlExpression3.LExpr() is SubQuery && sqlExpression3.Oper() == SQLExpression.Operation.ISNULL_FUNC)
              {
                string descriptionField = GenericInquiryHelpers.GetBaseFieldNameForDescriptionField(fieldName);
                sqlExpression3 = this.ApplyAggregateToFieldsInSubquery(sqlExpression3, descriptionField, table, cach);
                if (table.Graph != null)
                  sqlExpression3 = ReplaceDirectColumnsWithSubqueryColumns(table, description.BqlTable, sqlExpression3, fieldName);
              }
              else
                sqlExpression3 = this.GetSqlExpressionStringToAggregateField(sqlExpression3, table, fieldName, description.DataType);
            }
            return sqlExpression3;
          }
          PXAggregateField aggregate = this.Description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (a =>
          {
            bool flag1 = a.Table.Equals(table) && string.Equals(a.Alias, fieldName, StringComparison.OrdinalIgnoreCase);
            if (flag1)
            {
              AggregateFunction? function = a.Function;
              bool flag2;
              if (function.HasValue)
              {
                switch (function.GetValueOrDefault())
                {
                  case AggregateFunction.Count:
                  case AggregateFunction.CountAll:
                  case AggregateFunction.StringAgg:
                    flag2 = true;
                    goto label_5;
                }
              }
              flag2 = false;
label_5:
              flag1 = flag2;
            }
            return flag1;
          }));
          if (aggregate != null)
          {
            AggregateFunction? function = aggregate.Function;
            AggregateFunction aggregateFunction = AggregateFunction.StringAgg;
            return !(function.GetValueOrDefault() == aggregateFunction & function.HasValue) ? this.GetSqlExpressionForCountField(aggregate, ref parnum, false) : this.GetSqlExpressionForStringAggField(aggregate, ref parnum, false);
          }
        }
      }
    }
    throw new PXException("A field with the name {0} cannot be found.", new object[1]
    {
      (object) name
    });

    SQLExpression ReplaceDirectColumnsWithSubqueryColumns(
      PXTable table,
      System.Type bqlTable,
      SQLExpression expression,
      string fieldName)
    {
      PXCache cach = table.Graph.Caches[bqlTable];
      SQLExpression sqlExpression = expression.Duplicate();
      string str = this.TrimFieldName(fieldName);
      if (str == null || str.Length <= 0)
        return sqlExpression;
      string expressionTableName = bqlTable.Name;
      HashSet<string> usedColumns = sqlExpression.GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (x => x.Table() is SimpleTable simpleTable && simpleTable.Name.Equals(expressionTableName, StringComparison.OrdinalIgnoreCase))).Select<Column, string>((Func<Column, string>) (x => x.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (string name in cach.Fields.Where<string>((Func<string, bool>) (x => usedColumns.Contains(x))))
        sqlExpression = sqlExpression.substituteNode((SQLExpression) new Column(name, expressionTableName), (SQLExpression) new Column($"{str}_{name}", table.Alias, expression.GetDBType()));
      return sqlExpression;
    }

    PXCommandPreparingEventArgs.FieldDescription GetFieldDescription(
      string fieldName,
      PXCache cache,
      System.Type table,
      out PXDBOperation operation)
    {
      operation = forceExt ? PXDBOperation.External : PXDBOperation.Select;
      switch (place)
      {
        case PXDBOperation.SelectClause:
          if (this.HasGrouping)
            operation |= PXDBOperation.GroupBy;
          operation |= PXDBOperation.SelectClause;
          break;
        case PXDBOperation.WhereClause:
          operation |= PXDBOperation.WhereClause;
          break;
      }
      PXCommandPreparingEventArgs.FieldDescription description;
      cache.RaiseCommandPreparing(fieldName, (object) null, (object) null, operation, table, out description);
      return description;
    }
  }

  private string TrimFieldName(string fieldNameWithTableAlias)
  {
    int startIndex = fieldNameWithTableAlias.Length - 1;
    if (this.IsAttributesField(fieldNameWithTableAlias))
      startIndex -= "_Attributes".Length;
    int length = fieldNameWithTableAlias.LastIndexOf('_', startIndex);
    return length > 0 ? fieldNameWithTableAlias.Substring(0, length) : (string) null;
  }

  private bool UseCalculatedExpression(
    bool forceExt,
    PXCache cache,
    string fieldName,
    bool isExternal,
    PXCommandPreparingEventArgs.FieldDescription description,
    PXTable table)
  {
    if (((cache.BqlSelect == null || this.IsAttributesField(fieldName) ? 1 : (this.IsKVExtField(cache, fieldName) ? 1 : 0)) | (isExternal ? 1 : 0)) != 0)
      return true;
    if (!forceExt)
      return false;
    if (this.SubQueryCanBeFlattened(description.Expr as SubQuery, table))
      return true;
    List<Column> expressionsOfType = description.Expr.GetExpressionsOfType<Column>();
    return expressionsOfType != null && this.AreColumnsFromTablesInDescription((IEnumerable<Column>) expressionsOfType) && this.IsNotSingleColumnOrSingleColumnAndExistsInCache(cache, (IList<Column>) expressionsOfType);
  }

  private bool IsNotSingleColumnOrSingleColumnAndExistsInCache(PXCache cache, IList<Column> columns)
  {
    IList<Column> columnList = columns;
    return (columnList != null ? (columnList.Count != 1 ? 1 : 0) : 1) != 0 || cache.Fields.Any<string>((Func<string, bool>) (f => f.Equals(columns[0].Name, StringComparison.OrdinalIgnoreCase)));
  }

  private bool SubQueryCanBeFlattened(SubQuery subQuery, PXTable table)
  {
    if (subQuery?.Query() == null)
      return false;
    HashSet<string> tablesNames = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    Stack<Query> queryStack = new Stack<Query>();
    queryStack.Push(subQuery.Query());
    while (queryStack.Count > 0)
    {
      foreach (Joiner joiner in queryStack.Pop().GetFrom())
      {
        if (joiner.Table() is Query)
          queryStack.Push((Query) joiner.Table());
        tablesNames.Add(joiner.Table().AliasOrName());
      }
    }
    SQLExpression where = subQuery.Query().GetWhere();
    List<Column> list = where != null ? where.GetExpressionsOfType<Column>().Where<Column>((Func<Column, bool>) (column => column.Table() != null && !tablesNames.Contains(column.Table().AliasOrName()))).Select<Column, Column>((Func<Column, Column>) (c => c.Duplicate() as Column)).ToList<Column>() : (List<Column>) null;
    if (table != null && list != null)
      list.ForEach((System.Action<Column>) (c => c.substituteTableName(table.CacheType.Name, table.Alias)));
    return this.AreColumnsFromTablesInDescription((IEnumerable<Column>) list);
  }

  private bool AreColumnsFromTablesInDescription(IEnumerable<Column> columns)
  {
    return columns == null || columns.All<Column>((Func<Column, bool>) (c => c.Table() != null && this.Description.Tables.ContainsKey(c.Table().AliasOrName())));
  }

  private SQLExpression ApplyAggregateToFieldsInSubquery(
    SQLExpression query,
    string field,
    PXTable table,
    PXCache cache)
  {
    SQLExpression objB = query;
    ColumnsNotInAggregatesCollector visitor = new ColumnsNotInAggregatesCollector();
    foreach (Column from in query.Accept<List<Column>>((ISQLExpressionVisitor<List<Column>>) visitor).Distinct<Column>())
    {
      string b = from.Table()?.AliasOrName();
      if (b != null && string.Equals(table.Alias, b, StringComparison.OrdinalIgnoreCase) && cache.Fields.Contains(from.Name))
      {
        string name = from.Name;
        PXCommandPreparingEventArgs.FieldDescription description;
        cache.RaiseCommandPreparing(name, (object) null, (object) null, PXDBOperation.Select, table.CacheType, out description);
        if (description?.Expr != null && this.GetSqlExpressionStringToAggregateField((SQLExpression) new SQLConst((object) 1), table, name, description.DataType) != null)
        {
          SQLExpression toAggregateField = this.GetSqlExpressionStringToAggregateField(description.Expr, table, name, description.DataType);
          SQLExpression objA = this.ReplaceWithAlias(objB.Duplicate().substituteNode((SQLExpression) from, toAggregateField), table);
          if (!object.Equals((object) objA, (object) objB))
            objB = objA.As($"{table.Alias}_{field}");
        }
      }
    }
    return objB;
  }

  protected SQLExpression GetSqlExpressionStringToAggregateField(
    SQLExpression operand,
    PXTable table,
    string field,
    PXDbType fieldType)
  {
    if (!this.HasGrouping || this.Description.GroupBys.Any<PXGroupBy>((Func<PXGroupBy, bool>) (g => g.Table != null && g.Table.Alias == table.Alias && string.Equals(((PXFieldValue) g.DataField).FieldName, field, StringComparison.OrdinalIgnoreCase))))
      return (SQLExpression) null;
    PXGraph pxGraph = (PXGraph) table.Graph ?? this._graph;
    PXAggregateField pxAggregateField = this.Description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (f => f.Table.Alias == table.Alias && string.Equals(f.Name, field, StringComparison.OrdinalIgnoreCase)));
    PXCache cach = pxGraph.Caches[table.CacheType];
    PXFieldState stateExt = cach.GetStateExt((object) null, field) as PXFieldState;
    if (operand is Column column && column.GetDBType() == PXDbType.Unspecified)
      column.SetDBType(fieldType);
    AggregateFunction? type;
    if (pxAggregateField != null)
    {
      AggregateFunction? function1 = pxAggregateField.Function;
      AggregateFunction aggregateFunction1 = AggregateFunction.Count;
      if (!(function1.GetValueOrDefault() == aggregateFunction1 & function1.HasValue))
      {
        AggregateFunction? function2 = pxAggregateField.Function;
        AggregateFunction aggregateFunction2 = AggregateFunction.CountAll;
        if (!(function2.GetValueOrDefault() == aggregateFunction2 & function2.HasValue))
        {
          type = pxAggregateField.Function;
          goto label_9;
        }
      }
    }
    type = cach.Keys.Contains(field) || string.Equals(cach.RowId, field, StringComparison.OrdinalIgnoreCase) || stateExt != null && (stateExt.DataType == typeof (string) || stateExt.DataType == typeof (bool) || !string.IsNullOrEmpty(stateExt.ValueField)) ? new AggregateFunction?(AggregateFunction.Max) : (fieldType == PXDbType.BigInt || fieldType == PXDbType.Decimal || fieldType == PXDbType.Float || fieldType == PXDbType.Int || fieldType == PXDbType.Money || fieldType == PXDbType.Real ? new AggregateFunction?(AggregateFunction.Sum) : new AggregateFunction?(AggregateFunction.Max));
label_9:
    return ValFromStr.GetSqlExpression(type, operand);
  }

  protected SQLExpression GetSqlExpressionFormulaToAggregateField(
    SQLExpression fieldFormula,
    PXDbType fieldType)
  {
    int parnum = 0;
    return !this.HasGrouping || this.Description.GroupBys.Any<PXGroupBy>((Func<PXGroupBy, bool>) (g => fieldFormula.Equals(g.DataField.GetExpression((Func<string, SQLExpression>) (name => this.ParameterHandlerExpression(name, ref parnum, PXDBOperation.OrderByClause)))))) ? (SQLExpression) null : ValFromStr.GetSqlExpression(new AggregateFunction?(AggregateFunction.Max), fieldFormula);
  }

  protected bool IsRestricted(BqlCommand.Selection selection, string field)
  {
    return BqlCommand.IsFieldRestricted(this._cache, selection, field) || BqlCommand.IsFieldRestricted(this._cache, selection, field + "_description") || this.IsAttributesField(field) && selection.RestrictedFields != null && selection.RestrictedFields.Any<RestrictedField>((Func<RestrictedField, bool>) (f => this.IsAttributesField(f.Field)));
  }

  protected bool IsAttributesField(string field)
  {
    return !string.IsNullOrEmpty(field) && field.EndsWith("_Attributes", StringComparison.OrdinalIgnoreCase);
  }

  protected bool IsKVExtField(PXCache cache, string field)
  {
    if (string.IsNullOrEmpty(field))
      return false;
    return cache.IsKvExtAttribute(field) || cache.IsKvExtField(field);
  }

  protected bool IsDescriptionField(string field)
  {
    return GenericInquiryHelpers.IsDescriptionField(field);
  }

  protected IPXValue ParameterHandler(string name)
  {
    name = name.Trim(' ', '[', ']', '`');
    PXParameter pxParameter;
    if (this.Description.Parameters.TryGetValue(name, out pxParameter))
    {
      if (pxParameter.Table != (System.Type) null)
      {
        PXCache cach = this._graph.Caches[pxParameter.Table];
        if (cach != null && cach.Fields.Contains(pxParameter.DataField))
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          cach.RaiseCommandPreparing(pxParameter.DataField, (object) null, (object) null, PXDBOperation.Select, pxParameter.Table, out description);
          if (description?.Expr == null)
            cach.RaiseCommandPreparing(pxParameter.DataField, (object) null, (object) null, PXDBOperation.External, pxParameter.Table, out description);
          if (description?.Expr != null)
            return (IPXValue) new PXSimpleValue(pxParameter.Value, description.Expr.GetDBType());
        }
      }
      return (IPXValue) new PXSimpleValue(pxParameter.Value);
    }
    if (name.Contains("."))
    {
      string[] strArray = name.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length == 2)
      {
        string str1 = strArray[0];
        PXTable pxTable;
        if (this.Description.Tables.TryGetValue(str1, out pxTable))
        {
          PXCache cach = ((PXGraph) pxTable.Graph ?? this._graph).Caches[pxTable.CacheType];
          string str2 = strArray[1];
          PXCommandPreparingEventArgs.FieldDescription description;
          cach.RaiseCommandPreparing(str2, (object) null, (object) null, PXDBOperation.Select, pxTable.BqlTable, out description);
          if (description?.Expr == null)
            cach.RaiseCommandPreparing(str2, (object) null, (object) null, PXDBOperation.External, pxTable.BqlTable, out description);
          if (description?.Expr != null)
            return (IPXValue) new PXFieldValue(str1, str2);
        }
      }
    }
    throw new PXException("A field with the name {0} cannot be found.", new object[1]
    {
      (object) name
    });
  }

  private Table GetSQLTableFromChangingScope(
    PXTable table,
    PXGraph graph,
    IDictionary<string, HashSet<string>> usedColumns,
    ref int parnum)
  {
    int localParnum = parnum;
    Table sqlTable = TableChangingScope.GetSQLTable((Func<Table>) (() =>
    {
      if (table.Graph == null)
        return BqlCommand.GetSQLTable(table.CacheType, graph);
      BqlGenericCommand bqlGenericCommand = new BqlGenericCommand((PXGraph) table.Graph, (PXCache) table.Graph.Caches<GenericResult>(), table.Graph.BaseQueryDescription);
      BqlCommand.Selection selection = new BqlCommand.Selection();
      HashSet<string> source;
      if (usedColumns.TryGetValue(table.Alias, out source))
      {
        IEnumerable<RestrictedField> fields = source.Select<string, RestrictedField>((Func<string, RestrictedField>) (x => new RestrictedField(x)));
        selection.RestrictedFields = new RestrictedFieldsSet(fields);
      }
      Query fromChangingScope = bqlGenericCommand.BuildQuery((PXGraph) table.Graph, selection, ref localParnum);
      table.Graph.LastCommand = bqlGenericCommand;
      if (fromChangingScope.GetLimit() <= 0 && fromChangingScope.GetOffset() <= 0)
        fromChangingScope.GetOrder()?.Clear();
      return (Table) fromChangingScope;
    }), table.Alias);
    parnum = localParnum;
    return sqlTable;
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression)
      return new Query();
    int parnum = 0;
    return this.BuildQuery(graph, selection, ref parnum);
  }

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public override BqlCommand WhereNew<newWhere>() => throw new NotSupportedException();

  public override BqlCommand WhereNew(System.Type newWhere) => throw new NotSupportedException();

  public override BqlCommand WhereAnd<where>() => throw new NotSupportedException();

  public override BqlCommand WhereAnd(System.Type where) => throw new NotSupportedException();

  public override BqlCommand WhereOr<where>() => throw new NotSupportedException();

  public override BqlCommand WhereOr(System.Type where) => throw new NotSupportedException();

  public override BqlCommand WhereNot() => throw new NotSupportedException();

  public override BqlCommand OrderByNew<newOrderBy>() => throw new NotSupportedException();

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    throw new NotSupportedException();
  }
}
