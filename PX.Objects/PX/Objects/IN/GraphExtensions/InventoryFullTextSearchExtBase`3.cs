// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryFullTextSearchExtBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TLine : class, IBqlTable, new()
  where TLineInventoryIDField : IBqlField
{
  private string _InventoryViewName;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXIntAttribute))]
  [DBRank]
  protected virtual void _(Events.CacheAttached<InventorySearchIndex.rank> e)
  {
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Data.PXGraph.ExecuteSelect(System.String,System.Object[],System.Object[],System.Object[],System.String[],System.Boolean[],PX.Data.PXFilterRow[],System.Int32@,System.Int32,System.Int32@)" />
  /// </summary>
  [PXOverride]
  public IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows,
    InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField>.ExecuteSelectDel basedel)
  {
    return this.UseFullTextSearch(viewName, filters) ? this.FullTextSearchSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows) : basedel(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  private bool UseFullTextSearch(string viewName, PXFilterRow[] filters)
  {
    if (this._InventoryViewName == null)
      this._InventoryViewName = ((PXFieldState) this.Base.Caches[typeof (TLine)].GetStateExt<TLineInventoryIDField>((object) null))?.ViewName;
    PXCondition condition = this.GetSearchCondition();
    return this._InventoryViewName != null && viewName == this._InventoryViewName && filters != null && ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (a => (string.Equals(a.DataField, "Descr", StringComparison.OrdinalIgnoreCase) || string.Equals(a.DataField, "inventoryCD", StringComparison.OrdinalIgnoreCase)) && a.Condition == condition && a.Value != null && ((string) a.Value).Length > 0)).Count<PXFilterRow>() >= 2;
  }

  private IEnumerable FullTextSearchSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    (InventoryFullTextSearchFilter fullTextSearchFilter, PXFilterRow[] newFilters, PXGraph.GetDefaultDelegate oldFilterDelegate) tuple1 = this.ApplyFilters(filters);
    try
    {
      (string[] newSortcolumns, bool[] newDescendings) tuple2 = this.ApplyOrderBy(sortcolumns, descendings);
      return (IEnumerable) new PXView((PXGraph) this.Base, true, this.GetCommandWithFullTextSearchJoin(viewName)).Select(new object[1]
      {
        (object) tuple1.fullTextSearchFilter
      }, parameters, searches, tuple2.newSortcolumns, tuple2.newDescendings, tuple1.newFilters, ref startRow, maximumRows, ref totalRows);
    }
    catch (DbException ex)
    {
      if (!this.IsFTSAvailable())
        throw new PXException("The search by a phrase or a list of keywords might not work because the Full-Text Search feature is not enabled. Contact an administrative user to enable the feature.", (Exception) ex);
      throw ex;
    }
    finally
    {
      this.CleanFilters(tuple1.oldFilterDelegate);
    }
  }

  private (InventoryFullTextSearchFilter fullTextSearchFilter, PXFilterRow[] newFilters, PXGraph.GetDefaultDelegate oldFilterDelegate) ApplyFilters(
    PXFilterRow[] filters)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField>.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField>.\u003C\u003Ec__DisplayClass6_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.fullTextSearchFilter = new InventoryFullTextSearchFilter()
    {
      ContainsSearchCondition = this.GetContainsSearchCondition(filters),
      Top = new int?(this.GetInventoryFTSResultsMax())
    };
    PXFilterRow[] array = ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (x => !string.Equals(x.DataField, "descr", StringComparison.OrdinalIgnoreCase) && !string.Equals(x.DataField, "inventoryCD", StringComparison.OrdinalIgnoreCase) || x.Tag != null)).ToArray<PXFilterRow>();
    ((PXCache) GraphHelper.Caches<InventoryFullTextSearchFilter>((PXGraph) this.Base)).Clear();
    PXGraph.GetDefaultDelegate getDefaultDelegate;
    this.Base.Defaults.TryGetValue(typeof (InventoryFullTextSearchFilter), out getDefaultDelegate);
    // ISSUE: method pointer
    this.Base.Defaults[typeof (InventoryFullTextSearchFilter)] = new PXGraph.GetDefaultDelegate((object) cDisplayClass60, __methodptr(\u003CApplyFilters\u003Eb__1));
    // ISSUE: reference to a compiler-generated field
    return (cDisplayClass60.fullTextSearchFilter, array, getDefaultDelegate);
  }

  private BqlCommand GetCommandWithFullTextSearchJoin(string viewName)
  {
    return BqlCommand.CreateInstance(new Type[1]
    {
      BqlCommand.AppendJoin(this.Base.Views[viewName].BqlSelect.GetType(), typeof (InnerJoin<InventorySearchIndexIDDescrTop, On<InventorySearchIndexIDDescrTop.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>))
    });
  }

  private (string[] newSortcolumns, bool[] newDescendings) ApplyOrderBy(
    string[] sortcolumns,
    bool[] descendings)
  {
    string str = "InventorySearchIndexIDDescrTop__Rank";
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    if (sortcolumns != null && sortcolumns.Length == 2 && sortcolumns[0] == "inventoryCD" && sortcolumns[1] == "descr" && ((IEnumerable<bool>) descendings).All<bool>((Func<bool, bool>) (d => !d)))
    {
      stringList.Add(str);
      stringList.AddRange((IEnumerable<string>) sortcolumns);
      boolList.Add(true);
      boolList.AddRange((IEnumerable<bool>) descendings);
    }
    else
    {
      stringList.AddRange((IEnumerable<string>) sortcolumns);
      stringList.Add(str);
      boolList.AddRange((IEnumerable<bool>) descendings);
      boolList.Add(true);
    }
    return (stringList.ToArray(), boolList.ToArray());
  }

  private void CleanFilters(PXGraph.GetDefaultDelegate oldFilterDelegate)
  {
    if (oldFilterDelegate == null)
      this.Base.Defaults.Remove(typeof (InventoryFullTextSearchFilter));
    else
      this.Base.Defaults[typeof (InventoryFullTextSearchFilter)] = oldFilterDelegate;
  }

  public virtual string GetContainsSearchCondition(PXFilterRow[] filters)
  {
    string[] searchWords = this.GetSearchWords(filters);
    if (searchWords == null || searchWords.Length == 0)
      return "@";
    if (searchWords.Length == 1)
      return $"\"{searchWords[0]}*\"";
    return $"ISABOUT(\"{string.Join(" ", searchWords).TrimEnd(' ')}*\" WEIGHT(1), {$"\"{string.Join("*\" NEAR \"", searchWords)}*\""} WEIGHT(0.2))";
  }

  public virtual PXCondition GetSearchCondition()
  {
    PreferencesGeneral preferencesGeneral = PXResultset<PreferencesGeneral>.op_Implicit(PXSelectBase<PreferencesGeneral, PXSelectReadonly<PreferencesGeneral>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object[]) null));
    return (preferencesGeneral != null ? (preferencesGeneral.GridFastFilterCondition.GetValueOrDefault() == 7 ? 1 : 0) : 0) == 0 ? (PXCondition) 6 : (PXCondition) 7;
  }

  public virtual string[] GetSearchWords(PXFilterRow[] filters)
  {
    PXCondition condition = this.GetSearchCondition();
    return ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (x => x.DataField == filters[0].DataField && x.Tag == null && x.Condition == condition)).Select<PXFilterRow, string>((Func<PXFilterRow, string>) (x => this.ProcessWord(x.Value as string))).ToArray<string>();
  }

  protected virtual string ProcessWord(string word)
  {
    return word?.Trim().Replace("\"", "").Replace("'", "").Replace("*", "");
  }

  public virtual bool IsFTSAvailable() => InventoryFullTextSearchHelper.IsFTSAvailable();

  public virtual int GetInventoryFTSResultsMax()
  {
    int result;
    return !int.TryParse(ConfigurationManager.AppSettings["InventoryFTSResultsMax"], out result) ? 500 : result;
  }

  public delegate IEnumerable ExecuteSelectDel(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
    where TGraph : PXGraph
    where TLine : class, IBqlTable, new()
    where TLineInventoryIDField : IBqlField;
}
