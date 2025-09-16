// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INLocationStatusByCostLayerTypeProjectionAttribute
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

public class INLocationStatusByCostLayerTypeProjectionAttribute : PXProjectionAttribute
{
  public INLocationStatusByCostLayerTypeProjectionAttribute()
    : base(typeof (SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<INLocationStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<INCostCenter.costCenterID>>>>.AggregateTo<GroupBy<INLocationStatusByCostCenter.inventoryID>, GroupBy<INLocationStatusByCostCenter.subItemID>, GroupBy<INLocationStatusByCostCenter.siteID>, GroupBy<INLocationStatusByCostCenter.locationID>, GroupBy<INCostCenter.costLayerType>, Sum<INLocationStatusByCostCenter.qtyOnHand>, Sum<INLocationStatusByCostCenter.qtyAvail>, Sum<INLocationStatusByCostCenter.qtyHardAvail>, Sum<INLocationStatusByCostCenter.qtyActual>, Sum<INLocationStatusByCostCenter.qtyInTransit>, Sum<INLocationStatusByCostCenter.qtyInTransitToSO>, Sum<INLocationStatusByCostCenter.qtyPOPrepared>, Sum<INLocationStatusByCostCenter.qtyPOOrders>, Sum<INLocationStatusByCostCenter.qtyPOReceipts>, Sum<INLocationStatusByCostCenter.qtyFSSrvOrdBooked>, Sum<INLocationStatusByCostCenter.qtyFSSrvOrdAllocated>, Sum<INLocationStatusByCostCenter.qtyFSSrvOrdPrepared>, Sum<INLocationStatusByCostCenter.qtySOBackOrdered>, Sum<INLocationStatusByCostCenter.qtySOPrepared>, Sum<INLocationStatusByCostCenter.qtySOBooked>, Sum<INLocationStatusByCostCenter.qtySOShipped>, Sum<INLocationStatusByCostCenter.qtySOShipping>, Sum<INLocationStatusByCostCenter.qtyINIssues>, Sum<INLocationStatusByCostCenter.qtyINReceipts>, Sum<INLocationStatusByCostCenter.qtyINAssemblyDemand>, Sum<INLocationStatusByCostCenter.qtyINAssemblySupply>>))
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<int1, IBqlInt>.IsEqual<int0>>>>.Where<BqlOperand<INLocationStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }
}
