// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.SOInvoiceSiteStatusLookupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class SOInvoiceSiteStatusLookupExt : 
  SiteStatusLookupExt<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, SOInvoiceSiteStatusSelected, SOSiteStatusFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();

  [PXMergeAttributes]
  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current2<PX.Objects.AR.ARInvoice.branchID>>, Or<Where<Current2<PX.Objects.AR.ARInvoice.branchID>, IsNull, And<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOSiteStatusFilter.siteID> e)
  {
  }

  protected override PX.Objects.AR.ARTran CreateNewLine(SOInvoiceSiteStatusSelected line)
  {
    PX.Objects.AR.ARTran arTran1 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Insert(new PX.Objects.AR.ARTran());
    arTran1.SiteID = line.SiteID ?? arTran1.SiteID;
    arTran1.InventoryID = line.InventoryID;
    if (line.SubItemID.HasValue)
      arTran1.SubItemID = line.SubItemID;
    arTran1.UOM = line.SalesUnit;
    PX.Objects.AR.ARTran arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran1);
    arTran2.Qty = line.QtySelected;
    return ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran2);
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOSiteStatusFilter> e)
  {
    if (e.Row == null)
      return;
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      e.Row.OnlyAvailable = new bool?(false);
    if (e.Row.HistoryDate.HasValue)
      return;
    e.Row.HistoryDate = new DateTime?(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<SOSiteStatusFilter>>) e).Cache.Graph.Accessinfo.BusinessDate.GetValueOrDefault().AddMonths(-3));
  }

  protected virtual void _(PX.Data.Events.RowSelecting<SOInvoiceSiteStatusSelected> e)
  {
    this.CopyCurrencyFields<PX.Objects.AR.ARInvoice.curyID, PX.Objects.AR.ARInvoice.curyInfoID, SOInvoiceSiteStatusSelected.curyID, SOInvoiceSiteStatusSelected.curyInfoID>(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<SOInvoiceSiteStatusSelected>>) e).Cache, (IBqlTable) e.Row, ((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current);
  }

  protected override void _(PX.Data.Events.RowSelected<SOSiteStatusFilter> e)
  {
    base._(e);
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.historyDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.dropShipSales>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null);
    attributeAdjuster.For<SOSiteStatusFilter.customerLocationID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = false));
    PXCache<SOInvoiceSiteStatusSelected> pxCache = GraphHelper.Caches<SOInvoiceSiteStatusSelected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache.Graph);
    PXUIFieldAttribute.SetVisible<SOInvoiceSiteStatusSelected.qtyLastSale>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOInvoiceSiteStatusSelected.curyID>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOInvoiceSiteStatusSelected.curyUnitPrice>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOInvoiceSiteStatusSelected.lastSalesDate>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>((PXCache) pxCache, (object) null);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<SOInvoiceSiteStatusSelected.dropShipLastBaseQty>((Action<PXUIFieldAttribute>) (x => x.Visible = e.Row.DropShipSales.GetValueOrDefault()));
    chained = chained.SameFor<SOInvoiceSiteStatusSelected.dropShipLastQty>();
    chained = chained.SameFor<SOInvoiceSiteStatusSelected.dropShipLastUnitPrice>();
    chained.SameFor<SOInvoiceSiteStatusSelected.dropShipCuryUnitPrice>().SameFor<SOInvoiceSiteStatusSelected.dropShipLastDate>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    ((PXAction) this.showItems).SetEnabled(((PXSelectBase) this.Base.Transactions).Cache.AllowInsert);
  }

  protected override Type CreateAdditionalWhere()
  {
    return typeof (Where<BqlOperand<SOInvoiceSiteStatusSelected.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>);
  }
}
