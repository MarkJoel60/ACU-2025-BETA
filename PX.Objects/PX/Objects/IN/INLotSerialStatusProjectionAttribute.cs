// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerialStatusProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INLotSerialStatusProjectionAttribute : PXProjectionAttribute
{
  public INLotSerialStatusProjectionAttribute()
    : base(typeof (SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemLotSerial>.On<INLotSerialStatusByCostCenter.FK.ItemLotSerial>>>.AggregateTo<GroupBy<INLotSerialStatusByCostCenter.inventoryID>, GroupBy<INLotSerialStatusByCostCenter.subItemID>, GroupBy<INLotSerialStatusByCostCenter.siteID>, GroupBy<INLotSerialStatusByCostCenter.locationID>, GroupBy<INLotSerialStatusByCostCenter.lotSerialNbr>, Sum<INLotSerialStatusByCostCenter.qtyOnHand>, Sum<INLotSerialStatusByCostCenter.qtyAvail>, Sum<INLotSerialStatusByCostCenter.qtyHardAvail>, Sum<INLotSerialStatusByCostCenter.qtyActual>, Sum<INLotSerialStatusByCostCenter.qtyInTransit>, Sum<INLotSerialStatusByCostCenter.qtyInTransitToSO>, Sum<INLotSerialStatusByCostCenter.qtyPOPrepared>, Sum<INLotSerialStatusByCostCenter.qtyPOOrders>, Sum<INLotSerialStatusByCostCenter.qtyPOReceipts>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdBooked>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdAllocated>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdPrepared>, Sum<INLotSerialStatusByCostCenter.qtySOBackOrdered>, Sum<INLotSerialStatusByCostCenter.qtySOPrepared>, Sum<INLotSerialStatusByCostCenter.qtySOBooked>, Sum<INLotSerialStatusByCostCenter.qtySOShipped>, Sum<INLotSerialStatusByCostCenter.qtySOShipping>, Sum<INLotSerialStatusByCostCenter.qtyINIssues>, Sum<INLotSerialStatusByCostCenter.qtyINReceipts>, Sum<INLotSerialStatusByCostCenter.qtyINAssemblyDemand>, Sum<INLotSerialStatusByCostCenter.qtyINAssemblySupply>, Sum<INLotSerialStatusByCostCenter.qtyInTransitToProduction>, Sum<INLotSerialStatusByCostCenter.qtyProductionSupplyPrepared>, Sum<INLotSerialStatusByCostCenter.qtyProductionSupply>, Sum<INLotSerialStatusByCostCenter.qtyPOFixedProductionPrepared>, Sum<INLotSerialStatusByCostCenter.qtyPOFixedProductionOrders>, Sum<INLotSerialStatusByCostCenter.qtyProductionDemandPrepared, Sum<INLotSerialStatusByCostCenter.qtyProductionDemand, Sum<INLotSerialStatusByCostCenter.qtyProductionAllocated, Sum<INLotSerialStatusByCostCenter.qtySOFixedProduction, Sum<INLotSerialStatusByCostCenter.qtyProdFixedPurchase, Sum<INLotSerialStatusByCostCenter.qtyProdFixedProduction, Sum<INLotSerialStatusByCostCenter.qtyProdFixedSalesOrdersPrepared, Sum<INLotSerialStatusByCostCenter.qtyProdFixedProdOrders, Sum<INLotSerialStatusByCostCenter.qtyProdFixedProdOrdersPrepared, Sum<INLotSerialStatusByCostCenter.qtyProdFixedProdOrders, Sum<INLotSerialStatusByCostCenter.qtyProdFixedSalesOrdersPrepared, Sum<INLotSerialStatusByCostCenter.qtyProdFixedSalesOrders, Sum<INLotSerialStatusByCostCenter.qtyFixedFSSrvOrd, Sum<INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrd, Sum<INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrdPrepared, Sum<INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrdReceipts, Sum<INLotSerialStatusByCostCenter.qtySOFixed, Sum<INLotSerialStatusByCostCenter.qtyPOFixedOrders, Sum<INLotSerialStatusByCostCenter.qtyPOFixedPrepared, Sum<INLotSerialStatusByCostCenter.qtyPOFixedReceipts, Sum<INLotSerialStatusByCostCenter.qtySODropShip, Sum<INLotSerialStatusByCostCenter.qtyPODropShipOrders, Sum<INLotSerialStatusByCostCenter.qtyPODropShipPrepared, Sum<INLotSerialStatusByCostCenter.qtyPODropShipReceipts, Min<INLotSerialStatusByCostCenter.receiptDate>>>>>>>>>>>>>>>>>>>>>>>>>>))
  {
    this.Persistent = false;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemLotSerial>.On<INLotSerialStatusByCostCenter.FK.ItemLotSerial>>>.Where<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }
}
