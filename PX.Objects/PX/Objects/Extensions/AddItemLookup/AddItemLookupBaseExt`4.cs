// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.AddItemLookup.AddItemLookupBaseExt`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.AddItemLookup;

public abstract class AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TItemInfo : class, IPXSelectable, IBqlTable, new()
  where TItemFilter : INSiteStatusFilter, IBqlTable, new()
{
  protected readonly string Selected = nameof (Selected);
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<TItemInfo, TypeArrayOf<IFbqlJoin>.Empty>, TItemInfo>.View ItemInfo;
  public PXFilter<TItemFilter> ItemFilter;
  public PXAction<TDocument> showItems;
  public PXAction<TDocument> addSelectedItems;

  [PXUIField(DisplayName = "Add Items", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton(CommitChanges = true, DisplayOnMainToolbar = false)]
  public virtual IEnumerable ShowItems(PXAdapter adapter) => this.ShowItemsHandler(adapter);

  protected abstract IEnumerable ShowItemsHandler(PXAdapter adapter);

  [PXUIField(DisplayName = "Add", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton(CommitChanges = true, DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddSelectedItems(PXAdapter adapter)
  {
    return this.AddSelectedItemsHandler(adapter);
  }

  protected abstract IEnumerable AddSelectedItemsHandler(PXAdapter adapter);

  protected virtual IEnumerable itemInfo()
  {
    PXView itemInfoView = this.CreateItemInfoView();
    int startRow = PXView.StartRow;
    int num = 0;
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    foreach (object row in itemInfoView.Select((object[]) null, (object[]) null, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2))
    {
      TItemInfo originalItem = PXResult.Unwrap<TItemInfo>(row);
      TItemInfo itemInfo = originalItem;
      TItemInfo updatedItem = this.ItemInfo.Locate(originalItem);
      if ((object) updatedItem != null && updatedItem.Selected.GetValueOrDefault())
        itemInfo = this.SetValuesOfSelectedRow(updatedItem, originalItem);
      pxDelegateResult.Add((object) itemInfo);
    }
    PXView.StartRow = 0;
    if (PXView.ReverseOrder)
      pxDelegateResult.Reverse();
    pxDelegateResult.IsResultSorted = true;
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual TItemInfo SetValuesOfSelectedRow(TItemInfo updatedItem, TItemInfo originalItem)
  {
    this.ItemInfo.Cache.RestoreCopy((object) updatedItem, (object) originalItem);
    updatedItem.Selected = new bool?(true);
    return updatedItem;
  }

  protected virtual PXView CreateItemInfoView()
  {
    return (PXView) new AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter>.LookupView((PXGraph) this.Base, this.GetSelect().WhereAnd(this.CreateWhere()).WhereAnd(this.CreateAdditionalWhere()));
  }

  protected virtual BqlCommand GetSelect() => (BqlCommand) new Select<TItemInfo>();

  protected System.Type CreateWhere()
  {
    PXFieldCollection fields1 = this.ItemFilter.Cache.Fields;
    PXFieldCollection fields2 = this.ItemInfo.Cache.Fields;
    System.Type actual = typeof (Where<True, Equal<True>>);
    foreach (string field1 in (List<string>) fields1)
    {
      if (fields2.Contains(field1))
      {
        if (!fields1.Contains(field1 + "Wildcard") && (!field1.Contains("SubItem") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subItem>()) && (!field1.Contains("Site") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.warehouse>()) && (!field1.Contains("Location") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.warehouseLocation>()))
        {
          System.Type bqlField1 = this.ItemFilter.Cache.GetBqlField(field1);
          System.Type bqlField2 = this.ItemInfo.Cache.GetBqlField(field1);
          if (bqlField1 != (System.Type) null && bqlField2 != (System.Type) null)
            actual = BqlTemplate.OfCondition<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<BqlPlaceholder.S>, PX.Data.IsNull>>>>.Or<BqlOperand<Current<BqlPlaceholder.S>, BqlPlaceholder.IBqlAny>.IsEqual<BqlPlaceholder.Named<BqlPlaceholder.D>.AsOperand>>>>>.And<BqlPlaceholder.W>>>.Replace<BqlPlaceholder.S>(bqlField1).Replace<BqlPlaceholder.D>(bqlField2).Replace<BqlPlaceholder.W>(actual).ToType();
        }
        else
          continue;
      }
      string field2;
      if (field1.Length > 8 && field1.EndsWith("Wildcard") && fields2.Contains(field2 = field1.Substring(0, field1.Length - 8)) && (!field1.Contains("SubItem") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subItem>()) && (!field1.Contains("Site") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.warehouse>()) && (!field1.Contains("Location") || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.warehouseLocation>()))
      {
        System.Type bqlField3 = this.ItemFilter.Cache.GetBqlField(field1);
        System.Type bqlField4 = this.ItemInfo.Cache.GetBqlField(field2);
        actual = BqlTemplate.OfCondition<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<BqlPlaceholder.L>, PX.Data.IsNull>>>>.Or<BqlOperand<BqlPlaceholder.D, BqlPlaceholder.IBqlAny>.IsLike<BqlField<BqlPlaceholder.L, BqlPlaceholder.IBqlAny>.FromCurrent>>>>>.And<BqlPlaceholder.W>>>.Replace<BqlPlaceholder.L>(bqlField3).Replace<BqlPlaceholder.D>(bqlField4).Replace<BqlPlaceholder.W>(actual).ToType();
      }
    }
    return actual;
  }

  protected virtual void _(PX.Data.Events.RowSelected<TItemInfo> e)
  {
    PXUIFieldAttribute.SetEnabled(e.Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled(e.Cache, (object) e.Row, this.Selected, true);
  }

  protected virtual System.Type CreateAdditionalWhere() => typeof (Where<True, Equal<True>>);

  protected virtual void _(PX.Data.Events.RowSelected<TItemFilter> e)
  {
    if ((object) e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible(this.ItemInfo.Cache, "SiteID", !e.Row.SiteID.HasValue);
  }

  public class LookupView : PXView
  {
    public LookupView(PXGraph graph, BqlCommand command)
      : base(graph, true, command)
    {
    }

    public LookupView(PXGraph graph, BqlCommand command, Delegate handler)
      : base(graph, true, command, handler)
    {
    }

    protected PXView.PXSearchColumn CorrectFieldName(PXView.PXSearchColumn orig, bool idFound)
    {
      switch (orig.Column.ToLower())
      {
        case "inventoryid":
          return !idFound ? new PXView.PXSearchColumn("InventoryCD", orig.Descending, orig.OrigSearchValue ?? orig.SearchValue) : (PXView.PXSearchColumn) null;
        case "subitemid":
          return new PXView.PXSearchColumn("SubItemCD", orig.Descending, orig.OrigSearchValue ?? orig.SearchValue);
        case "siteid":
          return new PXView.PXSearchColumn("SiteCD", orig.Descending, orig.OrigSearchValue ?? orig.SearchValue);
        case "locationid":
          return new PXView.PXSearchColumn("LocationCD", orig.Descending, orig.OrigSearchValue ?? orig.SearchValue);
        default:
          return orig;
      }
    }

    protected override List<object> InvokeDelegate(object[] parameters)
    {
      if (PXView.MaximumRows == 1 && PXView.StartRow == 0)
      {
        int? length = PXView.Searches?.Length;
        int count1 = this.Cache.Keys.Count;
        if (length.GetValueOrDefault() == count1 & length.HasValue && ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (s => s != null)))
        {
          length = PXView.SortColumns?.Length;
          int count2 = this.Cache.Keys.Count;
          if (length.GetValueOrDefault() == count2 & length.HasValue && ((IEnumerable<string>) PXView.SortColumns).SequenceEqual<string>((IEnumerable<string>) this.Cache.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            return base.InvokeDelegate(parameters);
        }
      }
      PXView.Context context = PXView._Executing.Peek();
      PXView.PXSearchColumn[] sorts = context.Sorts;
      bool idFound = false;
      List<PXView.PXSearchColumn> pxSearchColumnList = new List<PXView.PXSearchColumn>();
      for (int index = 0; index < sorts.Length - this.Cache.Keys.Count; ++index)
      {
        pxSearchColumnList.Add(this.CorrectFieldName(sorts[index], false));
        if (sorts[index].Column == "InventoryCD")
          idFound = true;
      }
      for (int index = sorts.Length - this.Cache.Keys.Count; index < sorts.Length; ++index)
      {
        PXView.PXSearchColumn pxSearchColumn = this.CorrectFieldName(sorts[index], idFound);
        if (pxSearchColumn != null)
          pxSearchColumnList.Add(pxSearchColumn);
      }
      context.Sorts = pxSearchColumnList.ToArray();
      return base.InvokeDelegate(parameters);
    }
  }
}
