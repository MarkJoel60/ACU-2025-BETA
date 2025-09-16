// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXQueryDescription
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXQueryDescription : ICloneable
{
  private int _selectTop;
  private string _externalWhere = string.Empty;

  private PXQueryDescription()
  {
  }

  public int SelectTop
  {
    get => this._selectTop;
    set => this._selectTop = value < 0 ? 0 : value;
  }

  public bool ResetTopCount { get; set; }

  public bool ShowDeletedRecords { get; set; }

  public bool ShowArchivedRecords { get; set; }

  public Dictionary<string, PXTable> Tables { get; private set; } = new Dictionary<string, PXTable>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public Dictionary<string, PXParameter> Parameters { get; private set; } = new Dictionary<string, PXParameter>();

  /// <summary>
  /// Contains all tables that are used in the result set or in the relations.
  /// </summary>
  public Dictionary<string, PXTable> UsedTables { get; private set; } = new Dictionary<string, PXTable>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public List<PXRelation> Relations { get; private set; } = new List<PXRelation>();

  public List<PXWhereCond> Wheres { get; set; } = new List<PXWhereCond>();

  public List<PXWhereCond> FilterWheres { get; set; } = new List<PXWhereCond>();

  public List<PXWhereCond> Searches { get; set; } = new List<PXWhereCond>();

  public List<PXExtField> ExtFields { get; private set; } = new List<PXExtField>();

  public IEnumerable<PXFormulaField> FormulaFields => this.ExtFields.OfType<PXFormulaField>();

  public IEnumerable<PXAggregateField> AggregateFields => this.ExtFields.OfType<PXAggregateField>();

  public List<PXAggregateField> TotalFields { get; private set; } = new List<PXAggregateField>();

  public List<PXSort> Sorts { get; set; } = new List<PXSort>();

  public List<PXGroupBy> GroupBys { get; private set; } = new List<PXGroupBy>();

  public bool RetrieveTotalRowCount { get; set; }

  public bool RetrieveTotals { get; set; }

  public SQLExpression ExternalWhereExpression { get; set; }

  public bool HasExistingTables
  {
    get
    {
      foreach (string key in this.Tables.Keys)
      {
        if (this.Tables[key].IsInDbExist)
          return true;
      }
      return false;
    }
  }

  public static PXQueryDescription Create(IGenericQueryProvider prov)
  {
    if (prov == null)
      return (PXQueryDescription) null;
    PXQueryDescription queryDescription = new PXQueryDescription();
    prov.FillDescription(queryDescription);
    return queryDescription;
  }

  public object Clone()
  {
    PXQueryDescription queryDescription1 = new PXQueryDescription();
    queryDescription1._selectTop = this._selectTop;
    queryDescription1.Tables = this.Tables == null ? (Dictionary<string, PXTable>) null : new Dictionary<string, PXTable>((IDictionary<string, PXTable>) this.Tables.ToDictionary<KeyValuePair<string, PXTable>, string, PXTable>((Func<KeyValuePair<string, PXTable>, string>) (p => p.Key), (Func<KeyValuePair<string, PXTable>, PXTable>) (p => (PXTable) p.Value.Clone())), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    queryDescription1.Parameters = this.Parameters == null ? (Dictionary<string, PXParameter>) null : new Dictionary<string, PXParameter>((IDictionary<string, PXParameter>) this.Parameters.ToDictionary<KeyValuePair<string, PXParameter>, string, PXParameter>((Func<KeyValuePair<string, PXParameter>, string>) (p => p.Key), (Func<KeyValuePair<string, PXParameter>, PXParameter>) (p => (PXParameter) p.Value.Clone())));
    List<PXRelation> relations = this.Relations;
    queryDescription1.Relations = relations != null ? relations.Select<PXRelation, PXRelation>((Func<PXRelation, PXRelation>) (r => (PXRelation) r.Clone())).ToList<PXRelation>() : (List<PXRelation>) null;
    List<PXWhereCond> wheres = this.Wheres;
    queryDescription1.Wheres = wheres != null ? wheres.Select<PXWhereCond, PXWhereCond>((Func<PXWhereCond, PXWhereCond>) (r => (PXWhereCond) r.Clone())).ToList<PXWhereCond>() : (List<PXWhereCond>) null;
    List<PXWhereCond> filterWheres = this.FilterWheres;
    queryDescription1.FilterWheres = filterWheres != null ? filterWheres.Select<PXWhereCond, PXWhereCond>((Func<PXWhereCond, PXWhereCond>) (r => (PXWhereCond) r.Clone())).ToList<PXWhereCond>() : (List<PXWhereCond>) null;
    List<PXWhereCond> searches = this.Searches;
    queryDescription1.Searches = searches != null ? searches.Select<PXWhereCond, PXWhereCond>((Func<PXWhereCond, PXWhereCond>) (r => (PXWhereCond) r.Clone())).ToList<PXWhereCond>() : (List<PXWhereCond>) null;
    List<PXExtField> extFields = this.ExtFields;
    queryDescription1.ExtFields = extFields != null ? extFields.Select<PXExtField, PXExtField>((Func<PXExtField, PXExtField>) (r => (PXExtField) r.Clone())).ToList<PXExtField>() : (List<PXExtField>) null;
    List<PXAggregateField> totalFields = this.TotalFields;
    queryDescription1.TotalFields = totalFields != null ? totalFields.Select<PXAggregateField, PXAggregateField>((Func<PXAggregateField, PXAggregateField>) (r => (PXAggregateField) r.Clone())).ToList<PXAggregateField>() : (List<PXAggregateField>) null;
    List<PXSort> sorts = this.Sorts;
    queryDescription1.Sorts = sorts != null ? sorts.Select<PXSort, PXSort>((Func<PXSort, PXSort>) (r => (PXSort) r.Clone())).ToList<PXSort>() : (List<PXSort>) null;
    List<PXGroupBy> groupBys = this.GroupBys;
    queryDescription1.GroupBys = groupBys != null ? groupBys.Select<PXGroupBy, PXGroupBy>((Func<PXGroupBy, PXGroupBy>) (r => (PXGroupBy) r.Clone())).ToList<PXGroupBy>() : (List<PXGroupBy>) null;
    queryDescription1._externalWhere = this._externalWhere;
    queryDescription1.ShowDeletedRecords = this.ShowDeletedRecords;
    queryDescription1.ShowArchivedRecords = this.ShowArchivedRecords;
    PXQueryDescription queryDescription2 = queryDescription1;
    queryDescription2.UsedTables = new Dictionary<string, PXTable>(this.UsedTables == null || this.UsedTables.Count <= 0 ? (IDictionary<string, PXTable>) queryDescription2.Tables : (IDictionary<string, PXTable>) this.UsedTables, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXWhereCond pxWhereCond in queryDescription2.Wheres.Concat<PXWhereCond>((IEnumerable<PXWhereCond>) queryDescription2.FilterWheres).Concat<PXWhereCond>((IEnumerable<PXWhereCond>) queryDescription2.Searches))
    {
      PXParameterValue pxParameterValue1 = pxWhereCond.Value1 as PXParameterValue;
      PXParameterValue pxParameterValue2 = pxWhereCond.Value2 as PXParameterValue;
      PXParameter parameter;
      if (pxParameterValue1 != null && queryDescription2.Parameters.TryGetValue(pxParameterValue1.GetParameterName(), out parameter))
        pxWhereCond.Value1 = (IPXValue) new PXParameterValue(parameter, pxParameterValue1.Graph);
      if (pxParameterValue2 != null && queryDescription2.Parameters.TryGetValue(pxParameterValue2.GetParameterName(), out parameter))
        pxWhereCond.Value2 = (IPXValue) new PXParameterValue(parameter, pxParameterValue2.Graph);
      if (pxWhereCond.DataField is PXParameterValue dataField && queryDescription2.Parameters.TryGetValue(dataField.GetParameterName(), out parameter))
        pxWhereCond.DataField = (IPXValue) new PXParameterValue(parameter, dataField.Graph);
    }
    return (object) queryDescription2;
  }
}
