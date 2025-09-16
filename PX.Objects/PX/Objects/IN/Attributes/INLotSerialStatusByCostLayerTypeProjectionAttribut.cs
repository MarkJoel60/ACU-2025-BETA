// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INLotSerialStatusByCostLayerTypeProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class INLotSerialStatusByCostLayerTypeProjectionAttribute : PXProjectionAttribute
{
  public INLotSerialStatusByCostLayerTypeProjectionAttribute()
    : base(typeof (SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<INCostCenter.costCenterID>>>>.AggregateTo<GroupBy<INLotSerialStatusByCostCenter.inventoryID>, GroupBy<INLotSerialStatusByCostCenter.subItemID>, GroupBy<INLotSerialStatusByCostCenter.siteID>, GroupBy<INLotSerialStatusByCostCenter.locationID>, GroupBy<INLotSerialStatusByCostCenter.lotSerialNbr>, GroupBy<INCostCenter.costLayerType>, Sum<INLotSerialStatusByCostCenter.qtyOnHand>, Sum<INLotSerialStatusByCostCenter.qtyAvail>, Sum<INLotSerialStatusByCostCenter.qtyHardAvail>, Sum<INLotSerialStatusByCostCenter.qtyActual>, Sum<INLotSerialStatusByCostCenter.qtyInTransit>, Sum<INLotSerialStatusByCostCenter.qtyInTransitToSO>, Sum<INLotSerialStatusByCostCenter.qtyPOPrepared>, Sum<INLotSerialStatusByCostCenter.qtyPOOrders>, Sum<INLotSerialStatusByCostCenter.qtyPOReceipts>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdBooked>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdAllocated>, Sum<INLotSerialStatusByCostCenter.qtyFSSrvOrdPrepared>, Sum<INLotSerialStatusByCostCenter.qtySOBackOrdered>, Sum<INLotSerialStatusByCostCenter.qtySOPrepared>, Sum<INLotSerialStatusByCostCenter.qtySOBooked>, Sum<INLotSerialStatusByCostCenter.qtySOShipped>, Sum<INLotSerialStatusByCostCenter.qtySOShipping>, Sum<INLotSerialStatusByCostCenter.qtyINIssues>, Sum<INLotSerialStatusByCostCenter.qtyINReceipts>, Sum<INLotSerialStatusByCostCenter.qtyINAssemblyDemand>, Sum<INLotSerialStatusByCostCenter.qtyINAssemblySupply>>))
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<int1, IBqlInt>.IsEqual<int0>>>>.Where<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }
}
