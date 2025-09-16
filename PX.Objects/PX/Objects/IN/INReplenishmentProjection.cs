// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.TM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class INReplenishmentProjection : OwnedFilter.ProjectionAttribute
{
  public INReplenishmentProjection()
    : base(typeof (INReplenishmentFilter), typeof (Select<PX.Objects.IN.S.INItemSite>))
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.subItem>();
    List<Type> typeList = new List<Type>();
    typeList.AddRange((IEnumerable<Type>) new Type[11]
    {
      typeof (Select2<,,>),
      typeof (PX.Objects.IN.S.INItemSite),
      typeof (InnerJoin<,,>),
      typeof (InventoryItem),
      typeof (On2<PX.Objects.IN.S.INItemSite.FK.InventoryItem, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>),
      typeof (InnerJoin<,,>),
      typeof (INSite),
      typeof (On<PX.Objects.IN.S.INItemSite.FK.Site>),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AP.Vendor),
      typeof (On<PX.Objects.IN.S.INItemSite.FK.PreferredVendor>)
    });
    if (flag)
      typeList.AddRange((IEnumerable<Type>) new Type[12]
      {
        typeof (LeftJoin<,,>),
        typeof (INSiteStatusByCostCenter),
        typeof (On<INSiteStatusByCostCenter.inventoryID, Equal<PX.Objects.IN.S.INItemSite.inventoryID>, And<INSiteStatusByCostCenter.siteID, Equal<PX.Objects.IN.S.INItemSite.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>>),
        typeof (LeftJoin<,,>),
        typeof (POVendorInventoryRepo),
        typeof (On<POVendorInventoryRepo.inventoryID, Equal<PX.Objects.IN.S.INItemSite.inventoryID>, And<POVendorInventoryRepo.vendorID, Equal<PX.Objects.IN.S.INItemSite.preferredVendorID>, And<IsNull<POVendorInventoryRepo.vendorLocationID, int_1>, Equal<IsNull<PX.Objects.IN.S.INItemSite.preferredVendorLocationID, int_1>>, And<IsNull<POVendorInventoryRepo.subItemID, int_1>, Equal<IsNull<INSiteStatusByCostCenter.subItemID, int_1>>>>>>),
        typeof (LeftJoin<,,>),
        typeof (INSubItem),
        typeof (On<INSiteStatusByCostCenter.FK.SubItem>),
        typeof (LeftJoin<,,>),
        typeof (INItemSiteReplenishment),
        typeof (On<INItemSiteReplenishment.inventoryID, Equal<INItemSite.inventoryID>, And<INItemSiteReplenishment.siteID, Equal<INItemSite.siteID>, And<INItemSiteReplenishment.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, And<INItemSiteReplenishment.itemStatus, NotEqual<INItemStatus.inactive>>>>>)
      });
    else
      typeList.AddRange((IEnumerable<Type>) new Type[12]
      {
        typeof (LeftJoin<,,>),
        typeof (INSubItem),
        typeof (On<INSubItem.subItemCD, Equal<string0>>),
        typeof (LeftJoin<,,>),
        typeof (INSiteStatusByCostCenter),
        typeof (On<INSiteStatusByCostCenter.inventoryID, Equal<PX.Objects.IN.S.INItemSite.inventoryID>, And<INSiteStatusByCostCenter.siteID, Equal<PX.Objects.IN.S.INItemSite.siteID>, And<INSiteStatusByCostCenter.subItemID, Equal<INSubItem.subItemID>, And<INSiteStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>>>),
        typeof (LeftJoin<,,>),
        typeof (POVendorInventoryRepo),
        typeof (On<POVendorInventoryRepo.inventoryID, Equal<PX.Objects.IN.S.INItemSite.inventoryID>, And<POVendorInventoryRepo.vendorID, Equal<PX.Objects.IN.S.INItemSite.preferredVendorID>, And<IsNull<POVendorInventoryRepo.vendorLocationID, int_1>, Equal<IsNull<PX.Objects.IN.S.INItemSite.preferredVendorLocationID, int_1>>, And<IsNull<POVendorInventoryRepo.subItemID, int_1>, Equal<IsNull<INSubItem.subItemID, int_1>>>>>>),
        typeof (LeftJoin<,,>),
        typeof (INItemSiteReplenishment),
        typeof (On<int1, Equal<int0>>)
      });
    Type type = BqlCommand.Compose(new Type[4]
    {
      typeof (Where2<,>),
      OwnedFilter.ProjectionAttribute.ComposeWhere(typeof (INReplenishmentFilter), typeof (PX.Objects.IN.S.INItemSite.productWorkgroupID), typeof (PX.Objects.IN.S.INItemSite.productManagerID)),
      typeof (And<>),
      typeof (Where<PX.Objects.IN.S.INItemSite.planningMethod, Equal<INPlanningMethod.inventoryReplenishment>>)
    });
    typeList.AddRange((IEnumerable<Type>) new Type[2]
    {
      typeof (InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>, LeftJoin<INAvailabilityScheme, On<INItemClass.FK.AvailabilityScheme>>>),
      type
    });
    return BqlCommand.Compose(typeList.ToArray());
  }
}
