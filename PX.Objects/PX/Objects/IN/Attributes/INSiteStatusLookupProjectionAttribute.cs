// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INSiteStatusLookupProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class INSiteStatusLookupProjectionAttribute : PXProjectionAttribute
{
  public INSiteStatusLookupProjectionAttribute()
    : base(INSiteStatusLookupProjectionAttribute.BuildSelect().GetType())
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>())
      return;
    this.tableselection = (BqlCommand) new INSiteStatusLookupProjectionAttribute.FTSBqlRowSelection(sender, ((PXDBInterceptorAttribute) this).Child ?? (PXDBInterceptorAttribute) this, this.tableselection);
  }

  private static BqlCommand BuildSelect()
  {
    return BqlCommand.AppendJoin<LeftJoin<INLocationStatusByCostCenter, On<INLocationStatusByCostCenter.FK.InventoryItem>, LeftJoin<INLocation, On<INLocationStatusByCostCenter.locationID, Equal<INLocation.locationID>>, LeftJoin<INSubItem, On<INLocationStatusByCostCenter.FK.SubItem>, LeftJoin<INSite, On<INSite.siteID, Equal<INLocationStatusByCostCenter.siteID>, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<INSite.baseCuryID, EqualBaseCuryID<Current2<INRegister.branchID>>>>>, LeftJoin<INItemXRef, On<INItemXRef.inventoryID, Equal<InventoryItem.inventoryID>, And2<Where<INItemXRef.subItemID, Equal<INLocationStatusByCostCenter.subItemID>, Or<INLocationStatusByCostCenter.subItemID, IsNull>>, And<Where<CurrentValue<INSiteStatusFilter.barCode>, IsNotNull, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>>, LeftJoin<INItemClass, On<InventoryItem.FK.ItemClass>, LeftJoin<INPriceClass, On<InventoryItem.FK.PriceClass>, LeftJoin<INCostCenter, On<INLocationStatusByCostCenter.costCenterID, Equal<INCostCenter.costCenterID>>>>>>>>>>>(!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive ? (BqlCommand) new Select<InventoryItem>() : (BqlCommand) new Select2<InventorySearchIndexAlternateIDTop, InnerJoin<InventoryItem, On<InventorySearchIndexAlternateIDTop.inventoryID, Equal<InventoryItem.inventoryID>>>>()).WhereNew<Where2<CurrentMatch<InventoryItem, AccessInfo.userName>, And2<Where<INLocationStatusByCostCenter.siteID, IsNull, Or<INSite.branchID, IsNotNull, And2<CurrentMatch<INSite, AccessInfo.userName>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<INSite.branchID, Current<INRegister.branchID>>>>>>>>, And2<Where<INLocationStatusByCostCenter.subItemID, IsNull, Or<CurrentMatch<INSubItem, AccessInfo.userName>>>, And2<Where<CurrentValue<INSiteStatusFilter.onlyAvailable>, Equal<boolFalse>, Or<INLocationStatusByCostCenter.qtyOnHand, Greater<decimal0>>>, And<InventoryItem.stkItem, Equal<boolTrue>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>>>>>();
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
      this._Command = INSiteStatusLookupProjectionAttribute.BuildSelect();
      return base.GetQueryInternal(graph, info, selection);
    }
  }
}
