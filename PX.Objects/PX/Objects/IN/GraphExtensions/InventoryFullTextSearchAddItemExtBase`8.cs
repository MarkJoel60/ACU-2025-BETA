// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryFullTextSearchAddItemExtBase`8
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Interfaces;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.IN.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class InventoryFullTextSearchAddItemExtBase<TGraph, TFullTextSearchExt, TLine, TLineInventoryIDField, TAddItemLookupExt, TDocument, TItemInfo, TItemFilter> : 
  PXGraphExtension<TAddItemLookupExt, TGraph>
  where TGraph : PXGraph
  where TFullTextSearchExt : InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField>
  where TLine : class, IBqlTable, new()
  where TLineInventoryIDField : IBqlField
  where TAddItemLookupExt : AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter>
  where TDocument : class, IBqlTable, new()
  where TItemInfo : class, IPXSelectable, IFTSSelectable, IBqlTable, new()
  where TItemFilter : INSiteStatusFilter, IBqlTable, new()
{
  private const string _addItemViewName = "ItemInfo";

  protected virtual bool OrderByHasDefaultValue(string[] sortcolumns, bool[] descendings)
  {
    return sortcolumns != null && sortcolumns.Length == 5 && "InventoryID".Equals(sortcolumns[0], StringComparison.OrdinalIgnoreCase) && "SiteCD".Equals(sortcolumns[1], StringComparison.OrdinalIgnoreCase) && "LocationCD".Equals(sortcolumns[2], StringComparison.OrdinalIgnoreCase) && "SubItemCD".Equals(sortcolumns[3], StringComparison.OrdinalIgnoreCase) && "CostCenterCD".Equals(sortcolumns[4], StringComparison.OrdinalIgnoreCase) && ((IEnumerable<bool>) descendings).All<bool>((Func<bool, bool>) (d => !d));
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
    InventoryFullTextSearchAddItemExtBase<TGraph, TFullTextSearchExt, TLine, TLineInventoryIDField, TAddItemLookupExt, TDocument, TItemInfo, TItemFilter>.ExecuteSelectDel basedel)
  {
    return this.UseFullTextSearch(viewName, filters) ? this.FullTextSearchSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows, basedel) : basedel(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  protected virtual string GetContainsSearchCondition(PXFilterRow[] filters)
  {
    return ((PXGraphExtension<TGraph>) this).Base.GetExtension<TFullTextSearchExt>().GetContainsSearchCondition(filters);
  }

  protected virtual PXCondition GetSearchCondition()
  {
    return ((PXGraphExtension<TGraph>) this).Base.GetExtension<TFullTextSearchExt>().GetSearchCondition();
  }

  protected virtual string[] GetSearchWords(PXFilterRow[] filters)
  {
    return ((PXGraphExtension<TGraph>) this).Base.GetExtension<TFullTextSearchExt>().GetSearchWords(filters);
  }

  protected virtual bool IsFTSAvailable()
  {
    return ((PXGraphExtension<TGraph>) this).Base.GetExtension<TFullTextSearchExt>().IsFTSAvailable();
  }

  protected virtual int GetInventoryFTSResultsMax()
  {
    return ((PXGraphExtension<TGraph>) this).Base.GetExtension<TFullTextSearchExt>().GetInventoryFTSResultsMax();
  }

  private bool UseFullTextSearch(string viewName, PXFilterRow[] filters)
  {
    return this.IsAddItemViewName(viewName) && this.IsFullTextSearchFilter(filters);
  }

  private bool IsAddItemViewName(string viewName)
  {
    return viewName != null && viewName.Equals("ItemInfo", StringComparison.OrdinalIgnoreCase);
  }

  private bool IsFullTextSearchFilter(PXFilterRow[] filters)
  {
    PXCondition condition = this.GetSearchCondition();
    return filters != null && ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (a => (string.Equals(a.DataField, "descr", StringComparison.OrdinalIgnoreCase) || string.Equals(a.DataField, "inventoryCD", StringComparison.OrdinalIgnoreCase)) && a.Condition == condition && a.Value != null && ((string) a.Value).Length > 0)).Count<PXFilterRow>() >= 2;
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
    ref int totalRows,
    InventoryFullTextSearchAddItemExtBase<TGraph, TFullTextSearchExt, TLine, TLineInventoryIDField, TAddItemLookupExt, TDocument, TItemInfo, TItemFilter>.ExecuteSelectDel basedel)
  {
    (InventoryFullTextSearchFilter fullTextSearchFilter, PXFilterRow[] newFilters, PXGraph.GetDefaultDelegate oldFilterDelegate) tuple1 = this.ApplyFilters(filters);
    try
    {
      (string[] newSortcolumns, bool[] newDescendings) tuple2 = this.ApplyOrderBy(sortcolumns, descendings);
      using (new InventoryFullTextSearchSelectScope())
        return basedel(viewName, parameters, searches, tuple2.newSortcolumns, tuple2.newDescendings, tuple1.newFilters, ref startRow, maximumRows, ref totalRows);
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
    InventoryFullTextSearchAddItemExtBase<TGraph, TFullTextSearchExt, TLine, TLineInventoryIDField, TAddItemLookupExt, TDocument, TItemInfo, TItemFilter>.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new InventoryFullTextSearchAddItemExtBase<TGraph, TFullTextSearchExt, TLine, TLineInventoryIDField, TAddItemLookupExt, TDocument, TItemInfo, TItemFilter>.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter = new InventoryFullTextSearchFilter()
    {
      ContainsSearchCondition = this.GetContainsSearchCondition(filters),
      Top = new int?(this.GetInventoryFTSResultsMax())
    };
    string[] searchWords = this.GetSearchWords(filters);
    string wildcardAnything = PXDatabase.Provider.SqlDialect.WildcardAnything;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word1 = searchWords.Length != 0 ? wildcardAnything + searchWords[0] + wildcardAnything : (string) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word2 = searchWords.Length > 1 ? wildcardAnything + searchWords[1] + wildcardAnything : (string) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word3 = searchWords.Length > 2 ? wildcardAnything + searchWords[2] + wildcardAnything : (string) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word4 = searchWords.Length > 3 ? wildcardAnything + searchWords[3] + wildcardAnything : (string) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word5 = searchWords.Length > 4 ? wildcardAnything + searchWords[4] + wildcardAnything : (string) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.fullTextSearchFilter.Word6 = searchWords.Length > 5 ? wildcardAnything + searchWords[5] + wildcardAnything : (string) null;
    List<PXFilterRow> list = ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (x => !string.Equals(x.DataField, "descr", StringComparison.OrdinalIgnoreCase) && !string.Equals(x.DataField, "inventoryCD", StringComparison.OrdinalIgnoreCase) && !string.Equals(x.DataField, "alternateID", StringComparison.OrdinalIgnoreCase) || x.Tag != null)).ToList<PXFilterRow>();
    foreach (string str in searchWords)
    {
      PXFilterRow pxFilterRow = new PXFilterRow()
      {
        DataField = "CombinedSearchString",
        Condition = (PXCondition) 6,
        Value = (object) str,
        OrOperator = false
      };
      list.Add(pxFilterRow);
    }
    ((PXCache) GraphHelper.Caches<InventoryFullTextSearchFilter>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).Clear();
    PXGraph.GetDefaultDelegate getDefaultDelegate;
    ((PXGraphExtension<TGraph>) this).Base.Defaults.TryGetValue(typeof (InventoryFullTextSearchFilter), out getDefaultDelegate);
    // ISSUE: method pointer
    ((PXGraphExtension<TGraph>) this).Base.Defaults[typeof (InventoryFullTextSearchFilter)] = new PXGraph.GetDefaultDelegate((object) cDisplayClass130, __methodptr(\u003CApplyFilters\u003Eb__1));
    // ISSUE: reference to a compiler-generated field
    return (cDisplayClass130.fullTextSearchFilter, list.ToArray(), getDefaultDelegate);
  }

  private (string[] newSortcolumns, bool[] newDescendings) ApplyOrderBy(
    string[] sortcolumns,
    bool[] descendings)
  {
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    if (this.OrderByHasDefaultValue(sortcolumns, descendings))
    {
      stringList.Add("Rank");
      stringList.AddRange((IEnumerable<string>) sortcolumns);
      boolList.Add(true);
      boolList.AddRange((IEnumerable<bool>) descendings);
    }
    else if (!((IEnumerable<string>) sortcolumns).Contains<string>("Rank", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      stringList.AddRange((IEnumerable<string>) sortcolumns);
      stringList.Add("Rank");
      boolList.AddRange((IEnumerable<bool>) descendings);
      boolList.Add(true);
    }
    else
    {
      stringList.AddRange((IEnumerable<string>) sortcolumns);
      boolList.AddRange((IEnumerable<bool>) descendings);
    }
    return (stringList.ToArray(), boolList.ToArray());
  }

  private void CleanFilters(PXGraph.GetDefaultDelegate oldFilterDelegate)
  {
    if (oldFilterDelegate == null)
      ((PXGraphExtension<TGraph>) this).Base.Defaults.Remove(typeof (InventoryFullTextSearchFilter));
    else
      ((PXGraphExtension<TGraph>) this).Base.Defaults[typeof (InventoryFullTextSearchFilter)] = oldFilterDelegate;
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
    where TFullTextSearchExt : InventoryFullTextSearchExtBase<TGraph, TLine, TLineInventoryIDField>
    where TLine : class, IBqlTable, new()
    where TLineInventoryIDField : IBqlField
    where TAddItemLookupExt : AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter>
    where TDocument : class, IBqlTable, new()
    where TItemInfo : class, IPXSelectable, IFTSSelectable, IBqlTable, new()
    where TItemFilter : INSiteStatusFilter, IBqlTable, new();
}
