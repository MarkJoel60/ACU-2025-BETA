// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.INTransferSiteStatusProjectionAttribute
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
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class INTransferSiteStatusProjectionAttribute : PXProjectionAttribute
{
  public INTransferSiteStatusProjectionAttribute()
    : base(INTransferSiteStatusProjectionAttribute.BuildSelect().GetType())
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>())
      return;
    this.tableselection = (BqlCommand) new INTransferSiteStatusProjectionAttribute.FTSBqlRowSelection(sender, ((PXDBInterceptorAttribute) this).Child ?? (PXDBInterceptorAttribute) this, this.tableselection);
  }

  private static BqlCommand BuildSelect()
  {
    return BqlCommand.AppendJoin<LeftJoin<INLocationStatusByCostCenter, On2<INLocationStatusByCostCenter.FK.InventoryItem, And<INLocationStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>, LeftJoin<INLocation, On<INLocationStatusByCostCenter.locationID, Equal<INLocation.locationID>>, LeftJoin<INSubItem, On<INLocationStatusByCostCenter.FK.SubItem>, LeftJoin<INSite, On<INLocationStatusByCostCenter.FK.Site>, LeftJoin<INItemXRef, On2<INItemXRef.FK.InventoryItem, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>, And2<Where<INItemXRef.subItemID, Equal<INLocationStatusByCostCenter.subItemID>, Or<INLocationStatusByCostCenter.subItemID, IsNull>>, And<CurrentValue<INSiteStatusFilter.barCode>, IsNotNull>>>>, LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.FK.ItemClass>, LeftJoin<INPriceClass, On<PX.Objects.IN.InventoryItem.FK.PriceClass>, LeftJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.receiptType, Equal<POReceiptType.poreceipt>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<CurrentValue<INTransferEntry.SiteStatusLookup.INTransferStatusFilter.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.siteID, Equal<CurrentValue<PX.Objects.IN.INRegister.siteID>>, And<PX.Objects.PO.POReceiptLine.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>>>>>>>>>>>(!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive ? (BqlCommand) new Select<PX.Objects.IN.InventoryItem>() : (BqlCommand) new Select2<InventorySearchIndexAlternateIDTop, InnerJoin<PX.Objects.IN.InventoryItem, On<InventorySearchIndexAlternateIDTop.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>()).WhereNew<Where2<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>, And2<Where<INLocationStatusByCostCenter.siteID, IsNull, Or<INSite.branchID, IsNotNull, And2<CurrentMatch<INSite, AccessInfo.userName>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<INSite.branchID, Current<PX.Objects.IN.INRegister.branchID>>>>>>>>, And2<Where<INLocationStatusByCostCenter.subItemID, IsNull, Or<CurrentMatch<INSubItem, AccessInfo.userName>>>, And2<Where<CurrentValue<INSiteStatusFilter.onlyAvailable>, Equal<boolFalse>, Or<INLocationStatusByCostCenter.qtyOnHand, Greater<decimal0>>>, And2<Where<CurrentValue<INTransferEntry.SiteStatusLookup.INTransferStatusFilter.receiptNbr>, IsNull, Or<PX.Objects.PO.POReceiptLine.lineNbr, IsNotNull>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<boolTrue>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotIn3<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>>>>>>>>();
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
      this._Command = INTransferSiteStatusProjectionAttribute.BuildSelect();
      return base.GetQueryInternal(graph, info, selection);
    }
  }
}
