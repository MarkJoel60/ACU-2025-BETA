// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistDProjectionAttribute
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

public class INItemSiteHistDProjectionAttribute : PXProjectionAttribute
{
  public INItemSiteHistDProjectionAttribute()
    : base(typeof (SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INItemSiteHistByCostCenterD.siteID>, GroupBy<INItemSiteHistByCostCenterD.inventoryID>, GroupBy<INItemSiteHistByCostCenterD.subItemID>, GroupBy<INItemSiteHistByCostCenterD.sDate>, Sum<INItemSiteHistByCostCenterD.qtyReceived>, Sum<INItemSiteHistByCostCenterD.qtyIssued>, Sum<INItemSiteHistByCostCenterD.qtySales>, Sum<INItemSiteHistByCostCenterD.qtyCreditMemos>, Sum<INItemSiteHistByCostCenterD.qtyDropShipSales>, Sum<INItemSiteHistByCostCenterD.qtyTransferIn>, Sum<INItemSiteHistByCostCenterD.qtyTransferOut>, Sum<INItemSiteHistByCostCenterD.qtyAssemblyIn>, Sum<INItemSiteHistByCostCenterD.qtyAssemblyOut>, Sum<INItemSiteHistByCostCenterD.qtyAdjusted>, Sum<INItemSiteHistByCostCenterD.begQty>, Sum<INItemSiteHistByCostCenterD.endQty>, Sum<INItemSiteHistByCostCenterD.qtyDebit>, Sum<INItemSiteHistByCostCenterD.qtyCredit>, Sum<INItemSiteHistByCostCenterD.costDebit>, Sum<INItemSiteHistByCostCenterD.costCredit>>))
  {
    this.Persistent = false;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INItemSiteHistByCostCenterD, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemSiteHistByCostCenterD.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>);
  }
}
