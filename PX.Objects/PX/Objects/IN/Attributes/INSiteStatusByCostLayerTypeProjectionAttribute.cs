// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INSiteStatusByCostLayerTypeProjectionAttribute
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

public class INSiteStatusByCostLayerTypeProjectionAttribute : PXProjectionAttribute
{
  public INSiteStatusByCostLayerTypeProjectionAttribute()
    : base(typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<INCostCenter.costCenterID>>>>.AggregateTo<GroupBy<INSiteStatusByCostCenter.inventoryID>, GroupBy<INSiteStatusByCostCenter.subItemID>, GroupBy<INSiteStatusByCostCenter.siteID>, GroupBy<INCostCenter.costLayerType>, Sum<INSiteStatusByCostCenter.qtyOnHand>, Sum<INSiteStatusByCostCenter.qtyNotAvail>, Sum<INSiteStatusByCostCenter.qtyAvail>, Sum<INSiteStatusByCostCenter.qtyHardAvail>, Sum<INSiteStatusByCostCenter.qtyActual>, Sum<INSiteStatusByCostCenter.qtyInTransit>, Sum<INSiteStatusByCostCenter.qtyInTransitToSO>, Sum<INSiteStatusByCostCenter.qtyPOPrepared>, Sum<INSiteStatusByCostCenter.qtyPOOrders>, Sum<INSiteStatusByCostCenter.qtyPOReceipts>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdBooked>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdAllocated>, Sum<INSiteStatusByCostCenter.qtyFSSrvOrdPrepared>, Sum<INSiteStatusByCostCenter.qtySOBackOrdered>, Sum<INSiteStatusByCostCenter.qtySOPrepared>, Sum<INSiteStatusByCostCenter.qtySOBooked>, Sum<INSiteStatusByCostCenter.qtySOShipped>, Sum<INSiteStatusByCostCenter.qtySOShipping>, Sum<INSiteStatusByCostCenter.qtyINIssues>, Sum<INSiteStatusByCostCenter.qtyINReceipts>, Sum<INSiteStatusByCostCenter.qtyINAssemblyDemand>, Sum<INSiteStatusByCostCenter.qtyINAssemblySupply>>))
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INCostCenter>.On<BqlOperand<int1, IBqlInt>.IsEqual<int0>>>>.Where<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }
}
