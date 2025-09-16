// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSiteStatusLookup`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

[Obsolete("This class is obsolete. Use SOOrderSiteStatusLookupExt instead.")]
public class SOSiteStatusLookup<Status, StatusFilter> : INSiteStatusLookup<Status, StatusFilter>
  where Status : class, IBqlTable, new()
  where StatusFilter : SOSiteStatusFilter, new()
{
  public SOSiteStatusLookup(PXGraph graph)
    : base(graph)
  {
    PXGraph.RowSelectingEvents rowSelecting = graph.RowSelecting;
    Type type = typeof (SOOrderSiteStatusSelected);
    SOSiteStatusLookup<Status, StatusFilter> siteStatusLookup = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) siteStatusLookup, __vmethodptr(siteStatusLookup, OnRowSelecting));
    rowSelecting.AddHandler(type, pxRowSelecting);
  }

  public SOSiteStatusLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    PXGraph.RowSelectingEvents rowSelecting = graph.RowSelecting;
    Type type = typeof (SOOrderSiteStatusSelected);
    SOSiteStatusLookup<Status, StatusFilter> siteStatusLookup = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) siteStatusLookup, __vmethodptr(siteStatusLookup, OnRowSelecting));
    rowSelecting.AddHandler(type, pxRowSelecting);
  }

  protected virtual void OnRowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!sender.Fields.Contains(typeof (SOOrderSiteStatusSelected.curyID).Name) || sender.GetValue<SOOrderSiteStatusSelected.curyID>(e.Row) != null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (SOOrder)];
    sender.SetValue<SOOrderSiteStatusSelected.curyID>(e.Row, cach.GetValue<SOOrder.curyID>(cach.Current));
    sender.SetValue<SOOrderSiteStatusSelected.curyInfoID>(e.Row, cach.GetValue<SOOrder.curyInfoID>(cach.Current));
  }

  protected override void OnFilterSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.OnFilterSelected(sender, e);
    SOSiteStatusFilter filter = (SOSiteStatusFilter) e.Row;
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.historyDate>(sender, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.dropShipSales>(sender, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXCacheEx.Adjust<PXUIFieldAttribute>(sender, (object) null).For<SOSiteStatusFilter.customerLocationID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = filter.Behavior == "BL"));
    PXCache cach = sender.Graph.Caches[typeof (SOOrderSiteStatusSelected)];
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.qtyLastSale>(cach, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.curyID>(cach, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.curyUnitPrice>(cach, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.lastSalesDate>(cach, (object) null, filter.Mode.GetValueOrDefault() == 1);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cach, (object) null).For<SOOrderSiteStatusSelected.dropShipLastBaseQty>((Action<PXUIFieldAttribute>) (x => x.Visible = filter.DropShipSales.GetValueOrDefault()));
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipLastQty>();
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipLastUnitPrice>();
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipCuryUnitPrice>();
    chained.SameFor<SOOrderSiteStatusSelected.dropShipLastDate>();
    if (filter.HistoryDate.HasValue)
      return;
    filter.HistoryDate = new DateTime?(sender.Graph.Accessinfo.BusinessDate.GetValueOrDefault().AddMonths(-3));
  }
}
