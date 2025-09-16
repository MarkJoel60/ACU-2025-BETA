// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusLookupExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable disable
namespace PX.Objects.IN;

public abstract class INSiteStatusLookupExt<TGraph> : 
  INSiteStatusLookupExt<TGraph, INSiteStatusSelected>
  where TGraph : INRegisterEntryBase
{
  protected override INTran InitTran(INTran newTran, INSiteStatusSelected siteStatus)
  {
    newTran.SiteID = siteStatus.SiteID ?? newTran.SiteID;
    newTran.InventoryID = siteStatus.InventoryID;
    newTran.SubItemID = siteStatus.SubItemID;
    newTran.UOM = siteStatus.BaseUnit;
    newTran = PXCache<INTran>.CreateCopy(this.Transactions.Update(newTran));
    if (siteStatus.LocationID.HasValue)
    {
      newTran.LocationID = siteStatus.LocationID;
      newTran = PXCache<INTran>.CreateCopy(this.Transactions.Update(newTran));
    }
    return newTran;
  }

  protected override Type CreateAdditionalWhere()
  {
    return typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusSelected.costCenterID, IsNull>>>>.Or<BqlOperand<INSiteStatusSelected.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>);
  }
}
