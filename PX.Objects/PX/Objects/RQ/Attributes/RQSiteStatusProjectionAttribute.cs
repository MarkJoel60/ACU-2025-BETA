// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.Attributes.RQSiteStatusProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.RQ.Attributes;

public class RQSiteStatusProjectionAttribute : PXProjectionAttribute
{
  public RQSiteStatusProjectionAttribute()
    : base(RQSiteStatusProjectionAttribute.BuildSelect().GetType())
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>())
      return;
    this.tableselection = (BqlCommand) new RQSiteStatusProjectionAttribute.FTSBqlRowSelection(sender, ((PXDBInterceptorAttribute) this).Child ?? (PXDBInterceptorAttribute) this, this.tableselection);
  }

  private static BqlCommand BuildSelect()
  {
    return BqlCommand.AppendJoin<LeftJoin<INSiteStatusByCostCenter, On2<INSiteStatusByCostCenter.FK.InventoryItem, And<INSiteStatusByCostCenter.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>>, LeftJoin<INSubItem, On<INSiteStatusByCostCenter.FK.SubItem>, LeftJoin<INSite, On<INSiteStatusByCostCenter.FK.Site>, LeftJoin<INItemXRef, On<INItemXRef.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And2<Where<INItemXRef.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>, And<Where<CurrentValue<INSiteStatusFilter.barCode>, IsNotNull, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>>, LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.FK.ItemClass>, LeftJoin<INPriceClass, On<INPriceClass.priceClassID, Equal<PX.Objects.IN.InventoryItem.priceClassID>>, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<RQRequest.branchID>>>>, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<InventoryItemCurySettings.preferredVendorID>>, LeftJoin<POVendorInventory, On<POVendorInventory.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<POVendorInventory.vendorID, Equal<CurrentValue<POSiteStatusFilter.vendorID>>>>, LeftJoin<INUnit, On<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.fromUnit, Equal<PX.Objects.IN.InventoryItem.purchaseUnit>, And<INUnit.toUnit, Equal<PX.Objects.IN.InventoryItem.baseUnit>>>>>>>>>>>>>>>>(!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive ? (BqlCommand) new Select<PX.Objects.IN.InventoryItem>() : (BqlCommand) new Select2<InventorySearchIndexAlternateIDTop, InnerJoin<PX.Objects.IN.InventoryItem, On<InventorySearchIndexAlternateIDTop.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>()).WhereNew<Where2<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>, And2<Where<INSiteStatusByCostCenter.siteID, IsNull, Or<INSite.branchID, IsNotNull, And2<CurrentMatch<INSite, AccessInfo.userName>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<INSite.branchID, Current<AccessInfo.branchID>>>>>>>>, And2<Where<INSiteStatusByCostCenter.subItemID, IsNull, Or<CurrentMatch<INSubItem, AccessInfo.userName>>>, And2<Where<CurrentValue<INSiteStatusFilter.onlyAvailable>, Equal<boolFalse>, Or<POVendorInventory.vendorID, IsNotNull>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<boolTrue>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotIn3<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion, InventoryItemStatus.noPurchases, InventoryItemStatus.noRequest>>>>>>>>>();
  }

  private class FTSBqlRowSelection(
    PXCache cache,
    PXDBInterceptorAttribute child,
    BqlCommand command) : PXProjectionAttribute.BqlRowSelection(cache, child, command)
  {
    public virtual Query GetQueryInternal(
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      this._Command = RQSiteStatusProjectionAttribute.BuildSelect();
      return base.GetQueryInternal(graph, info, selection);
    }
  }
}
