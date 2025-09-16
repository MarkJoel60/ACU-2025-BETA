// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSiteStatusProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.SO;

public class SOSiteStatusProjectionAttribute : PXProjectionAttribute
{
  protected System.Type _branch;
  protected System.Type _customer;
  protected System.Type _customerLocation;
  protected System.Type _currentBehavior;

  public SOSiteStatusProjectionAttribute(
    System.Type branch,
    System.Type customer,
    System.Type customerLocation,
    System.Type currentBehavior)
    : base(SOSiteStatusProjectionAttribute.BuildSelect(branch, customer, customerLocation, currentBehavior).GetType())
  {
    this._branch = branch;
    this._customer = customer;
    this._customerLocation = customerLocation;
    this._currentBehavior = currentBehavior;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>())
      return;
    this.tableselection = (BqlCommand) new SOSiteStatusProjectionAttribute.FTSBqlRowSelection(this._branch, this._customer, this._customerLocation, this._currentBehavior, sender, ((PXDBInterceptorAttribute) this).Child ?? (PXDBInterceptorAttribute) this, this.tableselection);
  }

  private static BqlCommand BuildSelect(
    System.Type branch,
    System.Type customer,
    System.Type customerLocation,
    System.Type currentBehavior)
  {
    BqlCommand bqlCommand = BqlCommand.AppendJoin<LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<boolTrue>, And<INSiteStatusByCostCenter.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>>>, LeftJoin<INSubItem, On<INSiteStatusByCostCenter.FK.SubItem>, LeftJoin<PX.Objects.IN.INSite, On2<INSiteStatusByCostCenter.FK.Site, And<Where<PX.Objects.IN.INSite.baseCuryID, EqualBaseCuryID<Current2<BqlPlaceholder.B>>, Or<CurrentValue<BqlPlaceholder.B>, IsNull, And<PX.Objects.IN.INSite.baseCuryID, EqualBaseCuryID<Current2<AccessInfo.branchID>>>>>>>, LeftJoin<INItemXRef, On<INItemXRef.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And2<Where<INItemXRef.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>, And<Where<CurrentValue<INSiteStatusFilter.barCode>, IsNotNull, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>>>>>>>(!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive ? (BqlCommand) new Select<PX.Objects.IN.InventoryItem>() : (BqlCommand) new Select2<InventorySearchIndexAlternateIDTop, InnerJoin<PX.Objects.IN.InventoryItem, On<InventorySearchIndexAlternateIDTop.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>());
    return BqlTemplate.FromCommand(BqlCommand.AppendJoin<LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.FK.ItemClass>, LeftJoin<INPriceClass, On<INPriceClass.priceClassID, Equal<PX.Objects.IN.InventoryItem.priceClassID>>, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<Where<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<BqlPlaceholder.B>>, Or<CurrentValue<BqlPlaceholder.B>, IsNull, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<AccessInfo.branchID>>>>>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<InventoryItemCurySettings.preferredVendorID>>, LeftJoin<INItemCustSalesStats, On<CurrentValue<SOSiteStatusFilter.mode>, Equal<SOAddItemMode.byCustomer>, And<INItemCustSalesStats.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCustSalesStats.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, And<INItemCustSalesStats.siteID, Equal<INSiteStatusByCostCenter.siteID>, And<INItemCustSalesStats.bAccountID, Equal<CurrentValue<BqlPlaceholder.C>>, And<Where<INItemCustSalesStats.lastDate, GreaterEqual<CurrentValue<SOSiteStatusFilter.historyDate>>, Or<CurrentValue<SOSiteStatusFilter.dropShipSales>, Equal<True>, And<INItemCustSalesStats.dropShipLastDate, GreaterEqual<CurrentValue<SOSiteStatusFilter.historyDate>>>>>>>>>>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<CurrentValue<BqlPlaceholder.C>>, And<PX.Objects.CR.Location.locationID, Equal<CurrentValue<BqlPlaceholder.L>>>>, LeftJoin<INUnit, On<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.fromUnit, Equal<PX.Objects.IN.InventoryItem.salesUnit>, And<INUnit.toUnit, Equal<PX.Objects.IN.InventoryItem.baseUnit>>>>>>>>>>>>>(!FlaggedModeScopeBase<InventoryFullTextSearchSelectScope>.IsActive ? BqlCommand.AppendJoin<LeftJoin<INItemPartNumber, On<INItemPartNumber.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemPartNumber.alternateID, Like<CurrentValue<INSiteStatusFilter.inventory_Wildcard>>, And2<Where<INItemPartNumber.bAccountID, Equal<Zero>, Or<INItemPartNumber.bAccountID, Equal<CurrentValue<BqlPlaceholder.C>>, Or<INItemPartNumber.alternateType, Equal<INAlternateType.vPN>>>>, And<Where<INItemPartNumber.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>>>>>>>(bqlCommand) : BqlCommand.AppendJoin<LeftJoin<INItemPartNumber, On<INItemPartNumber.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And2<Where<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word1>>, Or<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word2>>, Or<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word3>>, Or<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word4>>, Or<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word5>>, Or<INItemPartNumber.alternateID, Like<CurrentValue<InventoryFullTextSearchFilter.word6>>>>>>>>, And2<Where<INItemPartNumber.bAccountID, Equal<Zero>, Or<INItemPartNumber.bAccountID, Equal<CurrentValue<BqlPlaceholder.C>>, Or<INItemPartNumber.alternateType, Equal<INAlternateType.vPN>>>>, And<Where<INItemPartNumber.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>>>>>>>(bqlCommand)).WhereNew<Where<CurrentValue<BqlPlaceholder.C>, IsNotNull, And2<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>, And2<Where<INSiteStatusByCostCenter.siteID, IsNull, Or<PX.Objects.IN.INSite.branchID, IsNotNull, And2<CurrentMatch<PX.Objects.IN.INSite, AccessInfo.userName>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current2<BqlPlaceholder.B>>, Or2<Where<CurrentValue<BqlPlaceholder.B>, IsNull, And<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current2<AccessInfo.branchID>>>>, Or<BqlPlaceholder.E, Equal<SOBehavior.qT>>>>>>>>>, And2<Where<INSiteStatusByCostCenter.subItemID, IsNull, Or<CurrentMatch<INSubItem, AccessInfo.userName>>>, And2<Where<CurrentValue<INSiteStatusFilter.onlyAvailable>, Equal<boolFalse>, Or<INSiteStatusByCostCenter.qtyAvail, Greater<decimal0>>>, And2<Where<CurrentValue<SOSiteStatusFilter.mode>, Equal<SOAddItemMode.bySite>, Or<INItemCustSalesStats.lastQty, Greater<decimal0>, Or<CurrentValue<SOSiteStatusFilter.dropShipSales>, Equal<True>, And<INItemCustSalesStats.dropShipLastQty, Greater<decimal0>>>>>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotIn3<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion, InventoryItemStatus.noSales>>>>>>>>>>()).Replace<BqlPlaceholder.B>(branch).Replace<BqlPlaceholder.C>(customer).Replace<BqlPlaceholder.L>(customerLocation).Replace<BqlPlaceholder.E>(currentBehavior).ToCommand();
  }

  private class FTSBqlRowSelection : PXProjectionAttribute.BqlRowSelection
  {
    protected System.Type _branch;
    protected System.Type _customer;
    protected System.Type _customerLocation;
    protected System.Type _currentBehavior;

    public FTSBqlRowSelection(
      System.Type branch,
      System.Type customer,
      System.Type customerLocation,
      System.Type currentBehavior,
      PXCache cache,
      PXDBInterceptorAttribute child,
      BqlCommand command)
      : base(cache, child, command)
    {
      this._branch = branch;
      this._customer = customer;
      this._customerLocation = customerLocation;
      this._currentBehavior = currentBehavior;
    }

    public virtual Query GetQueryInternal(
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      this._Command = SOSiteStatusProjectionAttribute.BuildSelect(this._branch, this._customer, this._customerLocation, this._currentBehavior);
      return base.GetQueryInternal(graph, info, selection);
    }
  }
}
