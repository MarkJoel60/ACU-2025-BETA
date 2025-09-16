// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INSiteStatusProjectionAttribute : PXProjectionAttribute
{
  public INSiteStatusProjectionAttribute()
    : base(typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INSiteStatusByCostCenter.inventoryID>, GroupBy<INSiteStatusByCostCenter.subItemID>, GroupBy<INSiteStatusByCostCenter.siteID>, Sum<INSiteStatusByCostCenter.qtyOnHand>, Sum<INSiteStatusByCostCenter.qtyNotAvail>, Sum<INSiteStatusByCostCenter.qtyAvail>, Sum<INSiteStatusByCostCenter.qtyHardAvail>, Sum<INSiteStatusByCostCenter.qtyActual>, Sum<INSiteStatusByCostCenter.qtyInTransit>, Sum<INSiteStatusByCostCenter.qtyInTransitToSO>, Sum<INSiteStatusByCostCenter.qtyPOPrepared>, Sum<INSiteStatusByCostCenter.qtyPOOrders>, Sum<INSiteStatusByCostCenter.qtyPOReceipts>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdBooked>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdAllocated>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdPrepared>, Sum<INSiteStatusByCostCenter.qtySOBackOrdered>, Sum<INSiteStatusByCostCenter.qtySOPrepared>, Sum<INSiteStatusByCostCenter.qtySOBooked>, Sum<INSiteStatusByCostCenter.qtySOShipped>, Sum<INSiteStatusByCostCenter.qtySOShipping>, Sum<INSiteStatusByCostCenter.qtyINIssues>, Sum<INSiteStatusByCostCenter.qtyINReceipts>, Sum<INSiteStatusByCostCenter.qtyINAssemblyDemand>, Sum<INSiteStatusByCostCenter.qtyINAssemblySupply>, Sum<INSiteStatusByCostCenter.qtyInTransitToProduction>, Sum<INSiteStatusByCostCenter.qtyProductionSupplyPrepared>, Sum<INSiteStatusByCostCenter.qtyProductionSupply>, Sum<INSiteStatusByCostCenter.qtyPOFixedProductionPrepared>, Sum<INSiteStatusByCostCenter.qtyPOFixedProductionOrders>, Sum<INSiteStatusByCostCenter.qtyProductionDemandPrepared>, Sum<INSiteStatusByCostCenter.qtyProductionDemand, Sum<INSiteStatusByCostCenter.qtyProductionAllocated, Sum<INSiteStatusByCostCenter.qtySOFixedProduction, Sum<INSiteStatusByCostCenter.qtyProdFixedPurchase, Sum<INSiteStatusByCostCenter.qtyProdFixedProduction, Sum<INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared, Sum<INSiteStatusByCostCenter.qtyProdFixedProdOrders, Sum<INSiteStatusByCostCenter.qtyProdFixedProdOrdersPrepared, Sum<INSiteStatusByCostCenter.qtyProdFixedProdOrders, Sum<INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared, Sum<INSiteStatusByCostCenter.qtyProdFixedSalesOrders, Sum<INSiteStatusByCostCenter.qtyINReplaned, Sum<INSiteStatusByCostCenter.qtyFixedFSSrvOrd, Sum<INSiteStatusByCostCenter.qtyPOFixedFSSrvOrd, Sum<INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdPrepared, Sum<INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdReceipts, Sum<INSiteStatusByCostCenter.qtySOFixed, Sum<INSiteStatusByCostCenter.qtyPOFixedOrders, Sum<INSiteStatusByCostCenter.qtyPOFixedPrepared, Sum<INSiteStatusByCostCenter.qtyPOFixedReceipts, Sum<INSiteStatusByCostCenter.qtySODropShip, Sum<INSiteStatusByCostCenter.qtyPODropShipOrders, Sum<INSiteStatusByCostCenter.qtyPODropShipPrepared, Sum<INSiteStatusByCostCenter.qtyPODropShipReceipts>>>>>>>>>>>>>>>>>>>>>>>>>))
  {
    this.Persistent = false;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }

  public static bool NonFreeStockExists()
  {
    return (PXAccess.FeatureInstalled<FeaturesSet.specialOrders>() || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>()) && RecordExistsSlot<INCostCenter, INCostCenter.costCenterID, Where<True, Equal<True>>>.IsRowsExists();
  }
}
