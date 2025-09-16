// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INSiteStatusSummaryProjectionAttribute
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
namespace PX.Objects.IN.Attributes;

public class INSiteStatusSummaryProjectionAttribute : PXProjectionAttribute
{
  public INSiteStatusSummaryProjectionAttribute()
    : base(typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INSiteStatusByCostCenter.inventoryID>, GroupBy<INSiteStatusByCostCenter.siteID>, Sum<INSiteStatusByCostCenter.qtyOnHand>, Sum<INSiteStatusByCostCenter.qtyAvail>, Sum<INSiteStatusByCostCenter.qtyNotAvail>>))
  {
    this.Persistent = false;
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return INSiteStatusSummaryProjectionAttribute.SubItemsExist() || INSiteStatusProjectionAttribute.NonFreeStockExists() ? base.GetSelect(sender) : typeof (SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.subItemID, Equal<SubItemAttribute.defaultSubItemID>>>>>.And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>);
  }

  public static bool SubItemsExist()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.subItem>() && RecordExistsSlot<INSubItem, INSubItem.subItemID, Where<BqlOperand<INSubItem.subItemID, IBqlInt>.IsNotEqual<SubItemAttribute.defaultSubItemID>>>.IsRowsExists();
  }
}
