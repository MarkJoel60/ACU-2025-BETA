// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.AddItemLookup.SiteStatusLookupExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Interfaces;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.Extensions.AddItemLookup;

public abstract class SiteStatusLookupExt<TGraph, TDocument, TLine, TItemInfo, TItemFilter> : 
  AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
  where TItemInfo : class, IQtySelectable, IPXSelectable, IBqlTable, new()
  where TItemFilter : INSiteStatusFilter, IBqlTable, new()
{
  protected readonly string QtySelected = nameof (QtySelected);

  public override void Initialize()
  {
    this.Base.FieldUpdated.AddHandler(typeof (TItemInfo), "Selected", new PXFieldUpdated(this.OnSelectedUpdated));
    base.Initialize();
  }

  protected override IEnumerable ShowItemsHandler(PXAdapter adapter)
  {
    if (this.ItemInfo.AskExt((PXView.InitializePanel) ((g, viewName) => this.ItemFilter.Cache.Clear())) == WebDialogResult.OK)
      return this.AddSelectedItems(adapter);
    this.ItemFilter.Cache.Clear();
    this.ItemInfo.Cache.Clear();
    return adapter.Get();
  }

  protected virtual void PrepareSelectedRecord(TItemInfo line)
  {
  }

  protected override IEnumerable AddSelectedItemsHandler(PXAdapter adapter)
  {
    this.Base.Caches<TLine>().ForceExceptionHandling = true;
    foreach (TItemInfo line in this.ItemInfo.Cache.Updated)
    {
      if (this.LineShallBeAdded(line))
      {
        this.PrepareSelectedRecord(line);
        this.CreateNewLine(line);
      }
    }
    this.ItemInfo.Cache.Clear();
    return adapter.Get();
  }

  protected override TItemInfo SetValuesOfSelectedRow(TItemInfo updatedItem, TItemInfo originalItem)
  {
    Decimal? qtySelected = updatedItem.QtySelected;
    this.ItemInfo.Cache.RestoreCopy((object) updatedItem, (object) originalItem);
    updatedItem.Selected = new bool?(true);
    updatedItem.QtySelected = qtySelected;
    return updatedItem;
  }

  protected virtual bool LineShallBeAdded(TItemInfo line)
  {
    this.Base.Caches<TItemInfo>();
    bool? selected = line.Selected;
    Decimal valueOrDefault = line.QtySelected.GetValueOrDefault();
    return selected.GetValueOrDefault() && valueOrDefault > 0M;
  }

  protected virtual void OnSelectedUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool? nullable1 = (bool?) sender.GetValue(e.Row, this.Selected);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this.QtySelected);
    if (nullable1.GetValueOrDefault())
    {
      if (nullable2.HasValue)
      {
        Decimal? nullable3 = nullable2;
        Decimal num = 0M;
        if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
          return;
      }
      sender.SetValue(e.Row, this.QtySelected, (object) 1M);
    }
    else
      sender.SetValue(e.Row, this.QtySelected, (object) 0M);
  }

  protected abstract TLine CreateNewLine(TItemInfo line);

  protected override void _(PX.Data.Events.RowSelected<TItemInfo> e)
  {
    base._(e);
    PXUIFieldAttribute.SetEnabled(e.Cache, (object) e.Row, this.QtySelected, true);
  }

  protected virtual void CopyCurrencyFields<TDocumentCurIDField, TDocumentCuryInfoIDField, TStatusSelectedCuryIDField, TStatusSelectedCuryInfoIDField>(
    PXCache siteSelectedCache,
    IBqlTable siteSelectedRow,
    PXCache documentCache,
    IBqlTable document)
    where TDocumentCurIDField : IBqlField
    where TDocumentCuryInfoIDField : IBqlField
    where TStatusSelectedCuryIDField : IBqlField
    where TStatusSelectedCuryInfoIDField : IBqlField
  {
    if (!siteSelectedCache.Fields.Contains(typeof (TStatusSelectedCuryIDField).Name) || siteSelectedCache.GetValue<TStatusSelectedCuryIDField>((object) siteSelectedRow) != null)
      return;
    siteSelectedCache.SetValue<TStatusSelectedCuryIDField>((object) siteSelectedRow, documentCache.GetValue<TDocumentCurIDField>((object) document));
    siteSelectedCache.SetValue<TStatusSelectedCuryInfoIDField>((object) siteSelectedRow, documentCache.GetValue<TDocumentCuryInfoIDField>((object) document));
  }
}
