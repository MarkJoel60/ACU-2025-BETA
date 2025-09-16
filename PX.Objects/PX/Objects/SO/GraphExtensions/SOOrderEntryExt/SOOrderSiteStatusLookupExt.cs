// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderSiteStatusLookupExt
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
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderSiteStatusLookupExt : 
  AlternateIDLookupExt<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, SOOrderSiteStatusSelected, SOSiteStatusFilter, SOOrderSiteStatusSelected.salesUnit>
{
  [PXMergeAttributes]
  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<PX.Objects.SO.SOOrder.branchID>>, Or<Current<PX.Objects.SO.SOOrder.behavior>, Equal<SOBehavior.qT>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOSiteStatusFilter.siteID> e)
  {
  }

  protected override PX.Objects.SO.SOLine CreateNewLine(SOOrderSiteStatusSelected line)
  {
    PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(new PX.Objects.SO.SOLine()));
    copy1.SiteID = line.SiteID ?? copy1.SiteID;
    copy1.InventoryID = line.InventoryID;
    if (line.SubItemID.HasValue)
      copy1.SubItemID = line.SubItemID;
    copy1.UOM = line.SalesUnit;
    copy1.AlternateID = line.AlternateID;
    SOSiteStatusFilter current = ((PXSelectBase<SOSiteStatusFilter>) this.ItemFilter).Current;
    int? nullable1;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable1 = current.CustomerLocationID;
      num = nullable1.HasValue ? 1 : 0;
    }
    if (num != 0)
      copy1.CustomerLocationID = ((PXSelectBase<SOSiteStatusFilter>) this.ItemFilter).Current.CustomerLocationID;
    PX.Objects.SO.SOLine copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy1));
    if (!copy2.RequireLocation.GetValueOrDefault())
    {
      PX.Objects.SO.SOLine soLine = copy2;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      soLine.LocationID = nullable2;
      copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy2));
    }
    copy2.Qty = line.QtySelected;
    return ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy2);
  }

  protected override Dictionary<string, int> GetAlternateTypePriority()
  {
    return new Dictionary<string, int>()
    {
      {
        "0CPN",
        0
      },
      {
        "GLBL",
        10
      },
      {
        "BAR",
        20
      },
      {
        "GIN",
        30
      },
      {
        "0VPN",
        40
      }
    };
  }

  protected override bool ScreenSpecificFilter(INItemXRef xRef)
  {
    if (xRef.AlternateType != "0CPN")
      return true;
    int? baccountId = xRef.BAccountID;
    int? customerId = (int?) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.CustomerID;
    return baccountId.GetValueOrDefault() == customerId.GetValueOrDefault() & baccountId.HasValue == customerId.HasValue;
  }

  protected override Type CreateAdditionalWhere()
  {
    return ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior != "TR" ? typeof (Where<BqlOperand<SOOrderSiteStatusSelected.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>) : base.CreateAdditionalWhere();
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOSiteStatusFilter> e)
  {
    if (e.Row == null)
      return;
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null)
    {
      e.Row.SiteID = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.DefaultSiteID;
      e.Row.Behavior = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Behavior;
      e.Row.CustomerLocationID = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CustomerLocationID;
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      e.Row.OnlyAvailable = new bool?(false);
    DateTime? nullable1 = e.Row.HistoryDate;
    if (nullable1.HasValue)
      return;
    SOSiteStatusFilter row = e.Row;
    nullable1 = ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<SOSiteStatusFilter>>) e).Cache.Graph.Accessinfo.BusinessDate;
    DateTime? nullable2 = new DateTime?(nullable1.GetValueOrDefault().AddMonths(-3));
    row.HistoryDate = nullable2;
  }

  protected override void _(PX.Data.Events.RowSelecting<SOOrderSiteStatusSelected> e)
  {
    base._(e);
    this.CopyCurrencyFields<PX.Objects.SO.SOOrder.curyID, PX.Objects.SO.SOOrder.curyInfoID, SOOrderSiteStatusSelected.curyID, SOOrderSiteStatusSelected.curyInfoID>(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<SOOrderSiteStatusSelected>>) e).Cache, (IBqlTable) e.Row, ((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
  }

  protected override void _(PX.Data.Events.RowSelected<SOSiteStatusFilter> e)
  {
    base._(e);
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.historyDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOSiteStatusFilter.dropShipSales>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache, (object) null).For<SOSiteStatusFilter.customerLocationID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = e.Row.Behavior == "BL"));
    PXCache<SOOrderSiteStatusSelected> pxCache = GraphHelper.Caches<SOOrderSiteStatusSelected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOSiteStatusFilter>>) e).Cache.Graph);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.qtyLastSale>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.curyID>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.curyUnitPrice>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXUIFieldAttribute.SetVisible<SOOrderSiteStatusSelected.lastSalesDate>((PXCache) pxCache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>((PXCache) pxCache, (object) null).For<SOOrderSiteStatusSelected.dropShipLastBaseQty>((Action<PXUIFieldAttribute>) (x => x.Visible = e.Row.DropShipSales.GetValueOrDefault()));
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipLastQty>();
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipLastUnitPrice>();
    chained = chained.SameFor<SOOrderSiteStatusSelected.dropShipCuryUnitPrice>();
    chained.SameFor<SOOrderSiteStatusSelected.dropShipLastDate>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    PX.Objects.SO.SOOrder row = e.Row;
    if ((row != null ? (row.IsExpired.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      ((PXAction) this.showItems).SetEnabled(false);
    else
      ((PXAction) this.showItems).SetEnabled(((PXSelectBase) this.Base.Transactions).Cache.AllowInsert);
  }
}
