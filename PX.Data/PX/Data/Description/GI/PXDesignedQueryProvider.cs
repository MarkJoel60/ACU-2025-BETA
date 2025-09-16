// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXDesignedQueryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Maintenance.GI;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Description.GI;

public class PXDesignedQueryProvider : IGenericQueryProvider
{
  protected PXGraph graph;
  protected GIDescription description;
  private PXGIFormulaProcessor formulaProc = new PXGIFormulaProcessor();
  private static readonly Regex _formulaOrCountFieldRegex = new Regex(".*(Formula|Count)[0-9a-f]{32}", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.RightToLeft);

  public PXDesignedQueryProvider(PXGraph graph, GIDescription description)
  {
    this.graph = graph;
    this.description = description;
  }

  public void FillDescription(PXQueryDescription queryDescription)
  {
    if (this.graph == null || this.description == null)
      return;
    queryDescription.SelectTop = this.description.Design.SelectTop.GetValueOrDefault();
    queryDescription.RetrieveTotals = this.description.RetrieveTotalsOnly;
    PXQueryDescription queryDescription1 = queryDescription;
    bool? nullable = this.description.Design.ShowDeletedRecords;
    bool flag1 = true;
    int num1 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    queryDescription1.ShowDeletedRecords = num1 != 0;
    PXQueryDescription queryDescription2 = queryDescription;
    nullable = this.description.Design.ShowArchivedRecords;
    bool flag2 = true;
    int num2 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    queryDescription2.ShowArchivedRecords = num2 != 0;
    this.CollectTables(queryDescription);
    this.InitializeSubqueryParameters(queryDescription);
    this.CollectParameters(queryDescription);
    this.CollectRelations(queryDescription);
    this.CollectWheres(queryDescription);
    this.CollectSorts(queryDescription);
    this.CollectPXGroupBys(queryDescription);
    this.CollectExtFields(queryDescription);
    this.CollectUsedTables(queryDescription);
  }

  private void CollectTables(PXQueryDescription descr)
  {
    foreach (GITable table in this.description.Tables)
    {
      int? type1 = table.Type;
      int num = 1;
      Guid result;
      if (type1.GetValueOrDefault() == num & type1.HasValue && Guid.TryParse(table.Name, out result))
      {
        this.ThrowOnCircularReference(result);
        PXGenericInqGrph subQueryInstance = PXGenericInqGrph.CreateSubQueryInstance(result);
        System.Type type2 = typeof (GenericResult);
        System.Type bqlTable = subQueryInstance.Caches[type2].BqlTable;
        string alias = table.Alias;
        descr.Tables[alias] = new PXTable(alias, bqlTable, type2, subQueryInstance);
      }
      else
      {
        System.Type type3 = PXBuildManager.GetType(table.Name, true);
        PXCache cach = this.graph.Caches[type3];
        System.Type bqlTable = cach.BqlTable;
        string str = table.Alias == type3.FullName ? type3.Name : table.Alias;
        bool isInDbExist = cach.TableExistsInDb();
        descr.Tables[str] = new PXTable(str, bqlTable, type3, isInDbExist);
      }
    }
  }

  private void ThrowOnCircularReference(Guid subDesignId)
  {
    PXGenericInqGrph genericInqGrph = this.graph as PXGenericInqGrph;
    if (genericInqGrph == null)
      return;
    Guid? designId = this.description.Design.DesignID;
    if (!designId.HasValue)
      return;
    Guid valueOrDefault = designId.GetValueOrDefault();
    if (genericInqGrph.ReferenceInfoProvider.HasReferenceTo(subDesignId, valueOrDefault))
      throw new PXException("The generic inquiry's description cannot be built due to circular referencing. The {0} generic inquiry references the {1} generic inquiry, which in turn references the {0} generic inquiry.", new object[2]
      {
        (object) GetName(subDesignId),
        (object) GetName(valueOrDefault)
      });

    string GetName(Guid designId)
    {
      return genericInqGrph.DescriptionProvider.Get(designId)?.Design?.Name ?? designId.ToString();
    }
  }

  private void InitializeSubqueryParameters(
    PXQueryDescription descr,
    GenericFilter filters = null,
    string prefix = "")
  {
    if (filters == null)
      filters = this.graph.Caches[typeof (GenericFilter)].Current as GenericFilter;
    foreach (KeyValuePair<string, PXTable> keyValuePair in descr.Tables.Where<KeyValuePair<string, PXTable>>((Func<KeyValuePair<string, PXTable>, bool>) (x => x.Value.Graph != null)))
    {
      string str1;
      PXTable pxTable1;
      EnumerableExtensions.Deconstruct<string, PXTable>(keyValuePair, ref str1, ref pxTable1);
      string str2 = str1;
      PXTable pxTable2 = pxTable1;
      string prefix1 = str2 + "_";
      foreach (KeyValuePair<string, PXParameter> parameter in pxTable2.Graph.BaseQueryDescription.Parameters)
      {
        string key = prefix1 + parameter.Key;
        object obj;
        if (filters != null && parameter.Value.Value == null && filters.Values.TryGetValue(prefix + key, out obj))
          parameter.Value.Value = obj;
        descr.Parameters[key] = parameter.Value;
      }
      this.InitializeSubqueryParameters(pxTable2.Graph.BaseQueryDescription, filters, prefix1);
    }
  }

  private void CollectRelations(PXQueryDescription descr)
  {
    Dictionary<int, PXRelation> dictionary1 = new Dictionary<int, PXRelation>();
    foreach (var data in this.description.Relations.GroupJoin(this.description.Ons, (Func<GIRelation, int?>) (relation => relation.LineNbr), (Func<GIOn, int?>) (relationOn => relationOn.RelationNbr), (relation, ons) => new
    {
      relation = relation,
      ons = ons
    }).SelectMany(_param1 => _param1.ons.DefaultIfEmpty<GIOn>(), (_param1, subOn) => new
    {
      Relation = _param1.relation,
      On = subOn
    }))
    {
      GIRelation relation = data.Relation;
      GIOn on = data.On;
      PXTable table1 = (PXTable) null;
      PXTable table2 = (PXTable) null;
      Dictionary<int, PXRelation> dictionary2 = dictionary1;
      int? lineNbr = relation.LineNbr;
      int key1 = lineNbr.Value;
      if (!dictionary2.ContainsKey(key1))
      {
        Dictionary<int, PXRelation> dictionary3 = dictionary1;
        lineNbr = relation.LineNbr;
        int key2 = lineNbr.Value;
        PXRelation pxRelation = new PXRelation();
        dictionary3.Add(key2, pxRelation);
      }
      Dictionary<int, PXRelation> dictionary4 = dictionary1;
      lineNbr = relation.LineNbr;
      int key3 = lineNbr.Value;
      PXRelation pxRelation1 = dictionary4[key3];
      if (descr.Tables.ContainsKey(relation.ParentTable))
        table1 = descr.Tables[relation.ParentTable];
      if (descr.Tables.ContainsKey(relation.ChildTable))
        table2 = descr.Tables[relation.ChildTable];
      pxRelation1.First = table1;
      pxRelation1.Relation = ValFromStr.GetRelation(relation.JoinType);
      pxRelation1.Second = table2;
      if (on != null)
        pxRelation1.On.Add(new PXOnCond()
        {
          OpenBrackets = on.OpenBrackets == null ? 0 : on.OpenBrackets.Length,
          FirstField = this.GetValue(descr, on.ParentField, (IPXValue) null, table1, relation.ParentTable),
          Cond = ValFromStr.GetCondition(on.Condition),
          SecondField = this.GetValue(descr, on.ChildField, (IPXValue) null, table2, relation.ChildTable),
          CloseBrackets = on.CloseBrackets == null ? 0 : on.CloseBrackets.Length,
          Op = ValFromStr.GetOperation(on.Operation)
        });
    }
    descr.Relations.AddRange((IEnumerable<PXRelation>) dictionary1.Values);
  }

  private void CollectWheres(PXQueryDescription descr)
  {
    foreach (GIWhere where in this.description.Wheres)
    {
      System.Type fieldType;
      string alias;
      PXWhereCond pxWhereCond = new PXWhereCond()
      {
        OpenBrackets = where.OpenBrackets == null ? 0 : where.OpenBrackets.Trim().Length,
        Table = this.DetermineTable(descr, where.DataFieldName, out fieldType, out System.Type _, out string _, out alias, out bool _)
      };
      pxWhereCond.DataField = this.GetField(descr, where.DataFieldName, pxWhereCond.Table, fieldType);
      Condition? condition = new Condition?(ValFromStr.GetCondition(where.Condition));
      bool useExt1;
      pxWhereCond.Value1 = this.GetValue(descr, where.Value1, pxWhereCond.DataField, pxWhereCond.Table, alias, out useExt1, ref condition);
      bool useExt2;
      pxWhereCond.Value2 = this.GetValue(descr, where.Value2, pxWhereCond.DataField, pxWhereCond.Table, alias, out useExt2);
      pxWhereCond.Cond = condition.Value;
      pxWhereCond.CloseBrackets = where.CloseBrackets == null ? 0 : where.CloseBrackets.Trim().Length;
      pxWhereCond.Op = ValFromStr.GetOperation(where.Operation);
      pxWhereCond.UseExt = useExt1 | useExt2;
      if (pxWhereCond.Cond != Condition.IsNull && pxWhereCond.Cond != Condition.NotNull)
      {
        if (pxWhereCond.Value1 == null)
          throw new PXException("The {0} cannot be empty for the {1} condition.", new object[2]
          {
            (object) "Value1",
            (object) pxWhereCond.Cond.ToString()
          });
        if (pxWhereCond.Cond == Condition.Between && pxWhereCond.Value2 == null)
          throw new PXException("The {0} cannot be empty for the {1} condition.", new object[2]
          {
            (object) "Value2",
            (object) pxWhereCond.Cond.ToString()
          });
      }
      descr.Wheres.Add(pxWhereCond);
    }
  }

  private void CollectSorts(PXQueryDescription descr)
  {
    foreach (GISort sort in this.description.Sorts)
    {
      PXSort pxSort = new PXSort();
      string alias = "";
      if (!sort.DataFieldName.StartsWith("="))
      {
        System.Type fieldType;
        bool specialField;
        pxSort.Table = this.DetermineTable(descr, sort.DataFieldName, out fieldType, out System.Type _, out string _, out alias, out specialField);
        if (fieldType == (System.Type) null && !specialField)
          throw new PXException("A field with the name {0} cannot be found.", new object[1]
          {
            (object) sort.DataFieldName
          });
        if (pxSort.Table == null)
          throw new PXException("A table with the alias {0} cannot be found.", new object[1]
          {
            (object) alias
          });
      }
      pxSort.DataField = this.GetValue(descr, sort.DataFieldName, pxSort.DataField, pxSort.Table, alias, out bool _);
      pxSort.Order = ValFromStr.GetSort(sort.SortOrder);
      if (!descr.Sorts.Contains(pxSort))
        descr.Sorts.Add(pxSort);
    }
  }

  private void CollectPXGroupBys(PXQueryDescription descr)
  {
    foreach (GIGroupBy groupBy in this.description.GroupBys)
    {
      PXGroupBy pxGroupBy = new PXGroupBy();
      string alias = "";
      if (!groupBy.DataFieldName.StartsWith("="))
      {
        System.Type fieldType;
        bool specialField;
        pxGroupBy.Table = this.DetermineTable(descr, groupBy.DataFieldName, out fieldType, out System.Type _, out string _, out alias, out specialField);
        if (fieldType == (System.Type) null && !specialField)
          throw new PXException("A field with the name {0} cannot be found.", new object[1]
          {
            (object) groupBy.DataFieldName
          });
        if (pxGroupBy.Table == null)
          throw new PXException("A table with the alias {0} cannot be found.", new object[1]
          {
            (object) alias
          });
      }
      pxGroupBy.DataField = this.GetValue(descr, groupBy.DataFieldName, pxGroupBy.DataField, pxGroupBy.Table, alias, out bool _);
      descr.GroupBys.Add(pxGroupBy);
    }
  }

  private void CollectParameters(PXQueryDescription descr)
  {
    foreach (GIFilter filter in this.description.Filters)
    {
      PXParameter par = new PXParameter();
      if (this.graph.Caches[typeof (GenericFilter)].Current is GenericFilter current)
      {
        par.Name = filter.Name;
        if (current.Values.ContainsKey(filter.Name))
          par.Value = current.Values[filter.Name];
        string str = par.Value?.ToString();
        if (RelativeDatesManager.IsRelativeDatesString(str))
          par.Value = (object) RelativeDatesManager.EvaluateAsDateTime(str);
        this.FillTableAndDataField(par, descr, filter.FieldName);
        par.Required = filter.Required.GetValueOrDefault();
        descr.Parameters[par.Name] = par;
      }
    }
  }

  private void CollectExtFields(PXQueryDescription descr)
  {
    foreach (GIResult result in this.description.Results)
    {
      PXFormulaField pxFormulaField = (PXFormulaField) null;
      PXAggregateField pxAggregateField = (PXAggregateField) null;
      if (result.Field.StartsWith("="))
      {
        pxFormulaField = new PXFormulaField();
        if (descr.Tables.ContainsKey(result.ObjectName))
          pxFormulaField.Table = descr.Tables[result.ObjectName];
        pxFormulaField.Name = "Formula" + PXGenericInqGrph.GetExtFieldId(result);
        pxFormulaField.Value = (IPXValue) new PXCalcedValue(result.Field, this.formulaProc);
        pxFormulaField.Function = string.IsNullOrEmpty(result.AggregateFunction) ? new AggregateFunction?() : new AggregateFunction?(ValFromStr.GetAggregate(result.AggregateFunction));
        bool exists = true;
        pxFormulaField.Value.GetExpression((Func<string, SQLExpression>) (fn =>
        {
          string[] strArray = fn.Split(new char[1]{ '.' }, 2);
          PXTable pxTable;
          if (strArray.Length == 2 && descr.Tables.TryGetValue(strArray[0], out pxTable) && !((PXGraph) pxTable.Graph ?? this.graph).Caches[pxTable.CacheType].Fields.Contains(strArray[1]))
            exists = false;
          return (SQLExpression) new Column(fn);
        }));
        if (exists)
          descr.ExtFields.Add((PXExtField) pxFormulaField);
      }
      if (result.Field == "$<Count>" && descr.GroupBys.Count == 0)
        throw new PXException("You must define Group By to use the <Count> field.");
      if ((!string.IsNullOrEmpty(result.AggregateFunction) || result.Field == "$<Count>") && descr.GroupBys.Count > 0)
      {
        if (descr.Tables.ContainsKey(result.ObjectName))
        {
          PXAggregateField aggregateField = this.CreateAggregateField(descr, result, result.AggregateFunction);
          if (pxFormulaField != null)
            aggregateField.Name = pxFormulaField.Name;
          descr.ExtFields.Add((PXExtField) aggregateField);
          pxAggregateField = aggregateField;
        }
        else
          continue;
      }
      if (!string.IsNullOrEmpty(result.TotalAggregateFunction) && descr.Tables.ContainsKey(result.ObjectName))
      {
        PXAggregateField aggregateField = this.CreateAggregateField(descr, result, result.TotalAggregateFunction);
        if (PXDesignedQueryProvider.IsCountFunction((AggregateFunction?) pxAggregateField?.Function) && !string.IsNullOrEmpty(pxAggregateField?.Alias))
          aggregateField.Name = pxAggregateField.Alias;
        else if (pxFormulaField != null)
          aggregateField.Name = pxFormulaField.Name;
        else if (!string.IsNullOrEmpty(result.FieldName))
          aggregateField.Name = result.FieldName.Substring(result.FieldName.IndexOf('_') + 1);
        descr.TotalFields.Add(aggregateField);
      }
    }
  }

  private PXAggregateField CreateAggregateField(
    PXQueryDescription descr,
    GIResult rec,
    string aggregateFunc)
  {
    PXAggregateField pxAggregateField1 = new PXAggregateField();
    pxAggregateField1.Function = new AggregateFunction?(rec.Field == "$<Count>" ? AggregateFunction.CountAll : ValFromStr.GetAggregate(aggregateFunc));
    pxAggregateField1.Name = rec.Field;
    pxAggregateField1.Table = descr.Tables[rec.ObjectName];
    PXAggregateField aggregateField = pxAggregateField1;
    PXAggregateField pxAggregateField2 = aggregateField;
    AggregateFunction? function = aggregateField.Function;
    string str;
    if (function.HasValue)
    {
      switch (function.GetValueOrDefault())
      {
        case AggregateFunction.Count:
        case AggregateFunction.CountAll:
          str = "Count" + PXGenericInqGrph.GetExtFieldId(rec);
          goto label_5;
        case AggregateFunction.StringAgg:
          str = "StringAgg" + PXGenericInqGrph.GetExtFieldId(rec);
          goto label_5;
      }
    }
    str = "Aggr" + PXGenericInqGrph.GetExtFieldId(rec);
label_5:
    pxAggregateField2.Alias = str;
    return aggregateField;
  }

  private static bool IsCountFunction(AggregateFunction? function)
  {
    bool flag;
    if (function.HasValue)
    {
      switch (function.GetValueOrDefault())
      {
        case AggregateFunction.Count:
        case AggregateFunction.CountAll:
          flag = true;
          goto label_4;
      }
    }
    flag = false;
label_4:
    return flag;
  }

  private void CollectUsedTables(PXQueryDescription descr)
  {
    foreach (GIResult result in this.description.Results)
    {
      if (descr.Tables.ContainsKey(result.ObjectName))
        descr.UsedTables[result.ObjectName] = descr.Tables[result.ObjectName];
    }
    foreach (GIRelation relation in this.description.Relations)
    {
      if (descr.Tables.ContainsKey(relation.ParentTable))
        descr.UsedTables[relation.ParentTable] = descr.Tables[relation.ParentTable];
      if (descr.Tables.ContainsKey(relation.ChildTable))
        descr.UsedTables[relation.ChildTable] = descr.Tables[relation.ChildTable];
    }
  }

  private void FillTableAndDataField(PXParameter par, PXQueryDescription descr, string fieldName)
  {
    string[] aliasAndfield = this.GetAliasAndfield(descr, fieldName);
    if (aliasAndfield == null)
    {
      par.Table = (System.Type) null;
      par.DataField = (string) null;
    }
    else
    {
      string key = aliasAndfield[0];
      PXTable table = descr.Tables[key];
      par.Table = table.BqlTable;
      par.DataField = aliasAndfield[1];
      PXCache cach = ((PXGraph) table.Graph ?? this.graph).Caches[table.BqlTable];
      if (cach.GetBqlField(par.DataField) == (System.Type) null && !cach.Fields.Contains(par.DataField))
        throw new PXException("A field with the name {0} cannot be found.", new object[1]
        {
          (object) fieldName
        });
    }
  }

  private string[] GetAliasAndfield(PXQueryDescription descr, string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName))
      return (string[]) null;
    int length = fieldName.LastIndexOf('.');
    if (length == -1 || length == fieldName.Length - 1)
      return (string[]) null;
    string key = fieldName.Substring(0, length).Trim();
    string str = fieldName.Substring(length + 1).Trim();
    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(str) || !descr.Tables.ContainsKey(key))
      return (string[]) null;
    return new string[2]{ key, str };
  }

  private PXParameter DetermineParameter(PXQueryDescription descr, string value)
  {
    if (value == null)
      return (PXParameter) null;
    if (value.StartsWith("[") && value.EndsWith("]") && value.Length > 2)
    {
      string key = value.Substring(1, value.Length - 2);
      if (descr.Parameters.ContainsKey(key))
        return descr.Parameters[key];
    }
    return (PXParameter) null;
  }

  private bool IsAttributesField(string fieldName)
  {
    return !string.IsNullOrEmpty(fieldName) && fieldName.EndsWith("_Attributes", StringComparison.OrdinalIgnoreCase);
  }

  private static bool IsFormulaOrCountFieldName(string fieldName)
  {
    return PXDesignedQueryProvider._formulaOrCountFieldRegex.IsMatch(fieldName);
  }

  private PXTable DetermineTable(
    PXQueryDescription descr,
    string typename,
    out System.Type fieldType,
    out System.Type cacheType,
    out string fieldName,
    out string alias,
    out bool specialField)
  {
    cacheType = (System.Type) null;
    fieldType = (System.Type) null;
    fieldName = (string) null;
    alias = (string) null;
    specialField = false;
    if (string.IsNullOrEmpty(typename) || typename.StartsWith("[") && typename.EndsWith("]"))
      return (PXTable) null;
    string[] aliasAndfield = this.GetAliasAndfield(descr, typename);
    if (aliasAndfield == null)
      return (PXTable) null;
    alias = aliasAndfield[0];
    fieldName = aliasAndfield[1];
    PXTable table = descr.Tables[alias];
    PXGraph pxGraph = (PXGraph) table.Graph ?? this.graph;
    PXCache cach1 = pxGraph.Caches[table.BqlTable];
    specialField = this.IsAttributesField(fieldName) || cach1.IsKvExtAttribute(fieldName) || PXDesignedQueryProvider.IsFormulaOrCountFieldName(fieldName);
    fieldType = cach1.GetBqlField(fieldName);
    if (fieldType == (System.Type) null && !table.BqlTable.Name.Equals(alias))
    {
      PXCache cach2 = pxGraph.Caches[table.CacheType];
      if (cach2 != null)
        fieldType = cach2.GetBqlField(fieldName);
    }
    if (fieldType != (System.Type) null)
    {
      cacheType = BqlCommand.GetItemType(fieldType);
    }
    else
    {
      cacheType = cach1.GetItemType();
      if (!specialField)
        specialField = PXGenericInqGrph.GetDeletedDatabaseRecord(cach1).Field != null || PXGenericInqGrph.GetDatabaseRecordStatus(cach1).Field != null;
    }
    return table;
  }

  private IPXValue GetField(PXQueryDescription descr, string field, PXTable table, System.Type fieldType)
  {
    string fieldName = !(fieldType != (System.Type) null) || table.Graph != null ? (field == null || table == null || table.Alias == null || !field.StartsWith(table.Alias + ".") ? field : field.Substring(table.Alias.Length + 1)) : fieldType.Name;
    PXParameter parameter = this.DetermineParameter(descr, fieldName);
    if (parameter != null)
      return (IPXValue) new PXParameterValue(parameter, this.graph);
    if (table == null)
      throw new PXException("The field {0} cannot be determined.", new object[1]
      {
        (object) fieldName
      });
    return (IPXValue) new PXFieldValue(table.Alias, fieldName);
  }

  private IPXValue GetValue(
    PXQueryDescription descr,
    string value,
    IPXValue field,
    PXTable table,
    string alias)
  {
    Condition? condition = new Condition?();
    return this.GetValue(descr, value, field, table, alias, out bool _, ref condition);
  }

  private IPXValue GetValue(
    PXQueryDescription descr,
    string value,
    IPXValue field,
    PXTable table,
    string alias,
    out bool useExt)
  {
    Condition? condition = new Condition?();
    return this.GetValue(descr, value, field, table, alias, out useExt, ref condition);
  }

  private IPXValue GetValue(
    PXQueryDescription descr,
    string value,
    IPXValue field,
    PXTable table,
    string alias,
    out bool useExt,
    ref Condition? condition)
  {
    useExt = false;
    if (string.IsNullOrEmpty(value))
      return (IPXValue) null;
    PXParameter parameter1 = this.DetermineParameter(descr, value);
    if (parameter1 != null)
      return (IPXValue) new PXParameterValue(parameter1, (table != null ? (PXGraph) table.Graph : (PXGraph) null) ?? this.graph);
    if (value.StartsWith("=", StringComparison.Ordinal))
      return !this.formulaProc.ContainsFields(value) ? (IPXValue) new PXSimpleValue(this.formulaProc.Evaluate(value, (SyFormulaFinalDelegate) null)) : (IPXValue) new PXCalcedValue(value, this.formulaProc);
    string[] aliasAndfield = this.GetAliasAndfield(descr, value);
    bool flag = aliasAndfield == null || aliasAndfield[0].Equals(table?.Alias, StringComparison.OrdinalIgnoreCase);
    if (flag && aliasAndfield != null)
    {
      string str = value;
      int startIndex = aliasAndfield[0].Length + 1;
      value = str.Substring(startIndex, str.Length - startIndex);
    }
    PXCache cach1 = table?.Graph?.Caches[table.CacheType];
    if (cach1 != null & flag && (cach1.GetBqlField(value) != (System.Type) null || PXDesignedQueryProvider.IsFormulaOrCountFieldName(value)))
      return (IPXValue) new PXFieldValue(table.Alias, value);
    PXCache cach2;
    int num1;
    if (table != null)
    {
      cach2 = ((PXGraph) table.Graph ?? this.graph).Caches[table.CacheType];
      num1 = cach2 != null ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = flag ? 1 : 0;
    if ((num1 & num2) != 0 && (cach2.GetBqlField(value) != (System.Type) null || this.IsAttributesField(value) || string.Equals(PXGenericInqGrph.GetDeletedDatabaseRecord(cach2).Field, value, StringComparison.OrdinalIgnoreCase) || string.Equals(PXGenericInqGrph.GetDatabaseRecordStatus(cach2).Field, value, StringComparison.OrdinalIgnoreCase)))
      return (IPXValue) new PXFieldValue(table.Alias, value);
    if (value.Contains("."))
    {
      string[] strArray = value.Trim(' ', ']', '[').Split(new string[1]
      {
        "."
      }, StringSplitOptions.RemoveEmptyEntries);
      PXTable pxTable;
      if (descr.Tables.TryGetValue(strArray[0], out pxTable))
      {
        PXCache cach3 = ((PXGraph) pxTable.Graph ?? this.graph).Caches[pxTable.CacheType];
        if (strArray.Length == 2 && cach3.GetBqlField(strArray[1]) != (System.Type) null || string.Equals(PXGenericInqGrph.GetDeletedDatabaseRecord(cach3).Field, strArray[1], StringComparison.OrdinalIgnoreCase) || string.Equals(PXGenericInqGrph.GetDatabaseRecordStatus(cach3).Field, strArray[1], StringComparison.OrdinalIgnoreCase) || cach3.IsKvExtAttribute(strArray[1]))
          return (IPXValue) new PXFieldValue(strArray[0], strArray[1]);
      }
    }
    if (table != null && this.graph.Caches[table.CacheType].CommandPreparingEvents.TryGetValue(value, out PXCommandPreparing _))
      return (IPXValue) new PXFieldValue(table.Alias, value);
    if (RelativeDatesManager.IsRelativeDatesString(value))
      value = RelativeDatesManager.EvaluateAsString(value);
    if (field is PXParameterValue)
      return (IPXValue) new PXSimpleValue((object) value);
    if (table == null)
      throw new PXException("A table with the alias {0} cannot be found.", new object[1]
      {
        (object) alias
      });
    if (field == null)
      throw new PXException("A field with the name {0} cannot be found.", new object[1]
      {
        (object) value
      });
    System.Type type = table.CacheType;
    string str1 = field is PXFieldValue pxFieldValue1 ? pxFieldValue1.ToString() : throw new PXException("A field with the name {0} cannot be found.", new object[1]
    {
      (object) field.ToString()
    });
    if (str1 != null && table.Alias != null && str1.StartsWith(table.Alias + "."))
      str1 = str1.Substring(table.Alias.Length + 1);
    PXParameter parameter2 = this.DetermineParameter(descr, str1);
    if (parameter2 != null)
    {
      str1 = parameter2.DataField;
      type = parameter2.Table;
    }
    object newValue = (object) value;
    using (new ReplaceCurrentScope((IEnumerable<KeyValuePair<PXCache, object>>) ((PXGraph) table.Graph ?? this.graph).Caches.Where<KeyValuePair<System.Type, PXCache>>((Func<KeyValuePair<System.Type, PXCache>, bool>) (c => c.Key != typeof (GenericResult) && c.Key != typeof (GenericFilter))).Select<KeyValuePair<System.Type, PXCache>, KeyValuePair<PXCache, object>>((Func<KeyValuePair<System.Type, PXCache>, KeyValuePair<PXCache, object>>) (c => new KeyValuePair<PXCache, object>(c.Value, (object) null))).ToArray<KeyValuePair<PXCache, object>>()))
    {
      PXCache cach4 = ((PXGraph) table.Graph ?? this.graph).Caches[type];
      try
      {
        FilterVariableType? variableType = FilterVariable.GetVariableType(value);
        if (variableType.HasValue && condition.HasValue)
        {
          string violationMessage = FilterVariable.GetConditionViolationMessage(value, ValFromStr.GetCondition(condition.Value));
          if (violationMessage != null)
            throw new PXException(violationMessage);
          System.Type fieldType = cach4.GetFieldType(str1);
          switch (variableType.Value)
          {
            case FilterVariableType.CurrentUser:
              if (table != null && field is PXFieldValue pxFieldValue2)
              {
                Condition? nullable = condition;
                Condition condition1 = Condition.Equals;
                if (nullable.GetValueOrDefault() == condition1 & nullable.HasValue && this.graph.Caches[table.CacheType].GetValueExt((object) null, pxFieldValue2.FieldName) is PXBranchSelectorState)
                {
                  condition = new Condition?(Condition.In);
                  return (IPXValue) new PXInValue<int>(PXAccess.GetActiveBranchesWithParents());
                }
              }
              newValue = !(fieldType == typeof (Guid)) ? (!(fieldType == typeof (int)) ? (!(fieldType == typeof (string)) ? newValue : (object) PXAccess.GetUserName()) : (object) PXAccess.GetContactID()) : (object) PXAccess.GetUserID();
              break;
            case FilterVariableType.CurrentUserGroups:
              return (IPXValue) new PXInValue<int>((IEnumerable<int>) UserGroupLazyCache.Current.GetUserGroupIds(PXAccess.GetUserID()));
            case FilterVariableType.CurrentUserGroupsTree:
              return (IPXValue) new PXInValue<int>((IEnumerable<int>) UserGroupLazyCache.Current.GetUserWorkTreeIds(PXAccess.GetUserID()));
            case FilterVariableType.CurrentBranch:
              newValue = (object) PXAccess.GetBranchID();
              break;
            case FilterVariableType.CurrentOrganization:
              return (IPXValue) new PXInValue<int>((IEnumerable<int>) PXAccess.GetBranchIDsForCurrentOrganization());
          }
        }
        else
          cach4.RaiseFieldUpdating(str1, (object) null, ref newValue);
        PXCommandPreparingEventArgs.FieldDescription description;
        cach4.RaiseCommandPreparing(str1, (object) null, newValue, PXDBOperation.External, type, out description);
        newValue = description?.DataValue ?? newValue;
      }
      catch (PXSetPropertyException ex)
      {
        useExt = true;
        if (ex.ErrorValue is string)
        {
          if (newValue is string)
            newValue = ex.ErrorValue;
        }
      }
      finally
      {
        Condition? nullable = condition;
        Condition condition2 = Condition.Contains;
        if (!(nullable.GetValueOrDefault() == condition2 & nullable.HasValue))
        {
          nullable = condition;
          Condition condition3 = Condition.NotContains;
          if (!(nullable.GetValueOrDefault() == condition3 & nullable.HasValue))
          {
            nullable = condition;
            Condition condition4 = Condition.StartsWith;
            if (!(nullable.GetValueOrDefault() == condition4 & nullable.HasValue))
            {
              nullable = condition;
              Condition condition5 = Condition.EndsWith;
              if (!(nullable.GetValueOrDefault() == condition5 & nullable.HasValue))
                goto label_62;
            }
          }
        }
        if (!(newValue is string))
          useExt = true;
        newValue = (object) value;
label_62:;
      }
      return (IPXValue) new PXSimpleValue(newValue);
    }
  }
}
