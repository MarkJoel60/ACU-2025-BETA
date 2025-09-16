// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.GraphExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.GraphExtensions.InventoryItemMaintExt;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.IN;

public class InventoryItemMaint : InventoryItemMaintBase
{
  private const 
  #nullable disable
  string lotSerNumValueFieldName = "LotSerNumVal";
  [PXHidden]
  public FbqlSelect<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>, INLotSerClass>.View lotSerClass;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Location>.View location;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.BAccount>.View baccount;
  public FbqlSelect<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AP.Vendor>.View vendor;
  public FbqlSelect<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AR.Customer>.View customer;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (INItemCost.lastCost)})]
  public FbqlSelect<SelectFromBase<INItemCost, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemCost.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INItemCost.curyID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INItemCost>.View ItemCosts;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<KeysRelation<Field<INItemSite.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INItemSite>, INSite, INItemSite>.And<CurrentMatch<INSite, AccessInfo.userName>>>>, FbqlJoins.Left<INSiteStatusSummary>.On<INSiteStatusSummary.FK.ItemSite>>>.Where<KeysRelation<Field<INItemSite.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSite>, InventoryItem, INItemSite>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  INSite.baseCuryID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.AsOptional>>>, 
  #nullable disable
  INItemSite>.View itemsiterecords;
  public PXSetup<INPostClass>.Where<BqlOperand<
  #nullable enable
  INPostClass.postClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  InventoryItem.postClassID, IBqlString>.FromCurrent>> postclass;
  public 
  #nullable disable
  PXSetup<INLotSerClass>.Where<BqlOperand<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  InventoryItem.lotSerClassID, IBqlString>.FromCurrent>> lotserclass;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<InventoryItemLotSerNumVal, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<InventoryItemLotSerNumVal.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, InventoryItemLotSerNumVal>, InventoryItem, InventoryItemLotSerNumVal>.SameAsCurrent>, InventoryItemLotSerNumVal>.View lotSerNumVal;
  public FbqlSelect<SelectFromBase<INItemRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemRep.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemRep>, InventoryItem, INItemRep>.SameAsCurrent.And<BqlOperand<
  #nullable enable
  INItemRep.curyID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INItemRep>.View replenishment;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INSubItemRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INSubItemRep.inventoryID>.IsRelatedTo<INItemRep.inventoryID>, Field<INSubItemRep.curyID>.IsRelatedTo<INItemRep.curyID>, Field<INSubItemRep.replenishmentClassID>.IsRelatedTo<INItemRep.replenishmentClassID>>.WithTablesOf<INItemRep, INSubItemRep>, INItemRep, INSubItemRep>.SameAsCurrent>, INSubItemRep>.View subreplenishment;
  public FbqlSelect<SelectFromBase<INItemSiteReplenishment, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemSiteReplenishment.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSiteReplenishment>, InventoryItem, INItemSiteReplenishment>.SameAsCurrent>, INItemSiteReplenishment>.View itemsitereplenihments;
  public FbqlSelect<SelectFromBase<INItemBoxEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemBoxEx.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemBoxEx>, InventoryItem, INItemBoxEx>.SameAsCurrent>, INItemBoxEx>.View Boxes;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.View sitestatusbycostcenter;
  public FbqlSelect<SelectFromBase<ItemStats, TypeArrayOf<IFbqlJoin>.Empty>, ItemStats>.View itemstats;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost>.View itemcost;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemPlan.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemPlan>, InventoryItem, INItemPlan>.SameAsCurrent>, INItemPlan>.View itemplans;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INSiteStatusByCostCenter.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INSiteStatusByCostCenter>, InventoryItem, INSiteStatusByCostCenter>.SameAsCurrent.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INSiteStatusByCostCenter.qtyOnHand, 
  #nullable disable
  NotEqual<decimal0>>>>>.Or<BqlOperand<
  #nullable enable
  INSiteStatusByCostCenter.qtyAvail, IBqlDecimal>.IsNotEqual<
  #nullable disable
  decimal0>>>>, INSiteStatusByCostCenter>.View nonemptysitestatuses;
  public FbqlSelect<SelectFromBase<INPIClassItem, TypeArrayOf<IFbqlJoin>.Empty>, INPIClassItem>.View inpiclassitems;
  public FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>, PMBudget>.View projectBudget;
  [PXDependToCache(new System.Type[] {typeof (InventoryItem)})]
  public FbqlSelect<SelectFromBase<RelationGroup, TypeArrayOf<IFbqlJoin>.Empty>, RelationGroup>.View Groups;
  private Dictionary<int?, int?> _persisted = new Dictionary<int?, int?>();
  protected bool _JustInserted;
  public PXAction<InventoryItem> viewSummary;
  public PXAction<InventoryItem> viewAllocationDetails;
  public PXAction<InventoryItem> viewTransactionSummary;
  public PXAction<InventoryItem> viewTransactionDetails;
  public PXAction<InventoryItem> viewTransactionHistory;
  public PXAction<InventoryItem> addWarehouseDetail;
  public PXAction<InventoryItem> updateReplenishment;
  public PXAction<InventoryItem> generateSubitems;
  public PXAction<InventoryItem> viewGroupDetails;

  public override bool IsStockItemFlag => true;

  public InventoryItemMaint()
  {
    ((PXSelectBase) this.Item).View = new PXView((PXGraph) this, false, (BqlCommand) new SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.stkItem, Equal<True>>>>, And<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.unknown>>>, And<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<False>>>>.And<MatchUser>>());
    ((PXGraph) this).Views["Item"] = ((PXSelectBase) this.Item).View;
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.Vendor.curyID>(((PXGraph) this).Caches[typeof (PX.Objects.AP.Vendor)], (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<InventoryItem.pPVAcctID>(((PXSelectBase) this.Item).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<InventoryItem.pPVSubID>(((PXSelectBase) this.Item).Cache, (object) null, true);
    ((PXSelectBase) this.itemsiterecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.itemsiterecords).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.itemsiterecords).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSite.isDefault>(((PXSelectBase) this.itemsiterecords).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<INItemSite.siteStatus>(((PXSelectBase) this.itemsiterecords).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Groups).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RelationGroup.included>(((PXSelectBase) this.Groups).Cache, (object) null, true);
    ((PXSelectBase) this.subreplenishment).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.replenishment>() && PXAccess.FeatureInstalled<FeaturesSet.subItem>();
    PXDBDefaultAttribute.SetDefaultForInsert<INItemXRef.inventoryID>(((PXSelectBase) this.itemxrefrecords).Cache, (object) null, true);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(InventoryItemMaint.\u003C\u003Ec.\u003C\u003E9__4_0 ?? (InventoryItemMaint.\u003C\u003Ec.\u003C\u003E9__4_0 = new PXFieldDefaulting((object) InventoryItemMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__4_0))));
    ((PXSelectBase) this.Item).Cache.Fields.Add("LotSerNumVal");
    PXGraph.FieldSelectingEvents fieldSelecting = ((PXGraph) this).FieldSelecting;
    System.Type type1 = typeof (InventoryItem);
    InventoryItemMaint inventoryItemMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) inventoryItemMaint1, __vmethodptr(inventoryItemMaint1, LotSerNumValueFieldSelecting));
    fieldSelecting.AddHandler(type1, "LotSerNumVal", pxFieldSelecting);
    PXGraph.FieldUpdatingEvents fieldUpdating = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (InventoryItem);
    InventoryItemMaint inventoryItemMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) inventoryItemMaint2, __vmethodptr(inventoryItemMaint2, LotSerNumValueFieldUpdating));
    fieldUpdating.AddHandler(type2, "LotSerNumVal", pxFieldUpdating);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    InventoryItemMaint.Configure(config.GetScreenConfigurationContext<InventoryItemMaint, InventoryItem>());
  }

  protected static void Configure(
    WorkflowContext<InventoryItemMaint, InventoryItem> context)
  {
    BoundedTo<InventoryItemMaint, InventoryItem>.Condition isKit = context.Conditions.FromBql<BqlOperand<InventoryItem.kitItem, IBqlBool>.IsEqual<True>>().WithSharedName("IsKit");
    BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IConfigured pricesCategory = context.Categories.CreateNew("Prices Category", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IConfigured>) (category => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IConfigured) category.DisplayName("Prices")));
    BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IConfigured otherCategory = CommonActionCategories.Get<InventoryItemMaint, InventoryItem>(context).Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<InventoryItemMaint, InventoryItem>.ScreenConfiguration.IStartConfigScreen, BoundedTo<InventoryItemMaint, InventoryItem>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<InventoryItemMaint, InventoryItem>.ScreenConfiguration.IConfigured) ((BoundedTo<InventoryItemMaint, InventoryItem>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewSalesPrices), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(pricesCategory)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewVendorPrices), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(pricesCategory)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.updateCost), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.ChangeID), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add<ConvertStockToNonStockExt>((Expression<Func<ConvertStockToNonStockExt, PXAction<InventoryItem>>>) (g => g.convert), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewSummary), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewAllocationDetails), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewTransactionSummary), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewTransactionDetails), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<InventoryItemMaint, PXAction<InventoryItem>>>) (g => g.viewTransactionHistory), (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.AddNew("ShowItemSalesPrices", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Item Sales Prices").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<ARSalesPriceMaint>().WithIcon("account_balance").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<ARSalesPriceFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowItemVendorPrices", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Item Vendor Prices").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<APVendorPriceMaint>().WithIcon("local_offer").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<APVendorPriceFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowInventorySummary", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Inventory Summary").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<InventorySummaryEnq>().WithIcon("business").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<InventorySummaryEnqFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowInventoryAllocationDetails", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Inventory Allocation Details").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<InventoryAllocDetEnq>().WithIcon("account_details").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<InventoryAllocDetEnqFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowInventoryTransactionHistory", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Inventory Transaction History").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<InventoryTranHistEnq>().WithIcon("archive").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<InventoryTranHistEnqFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowDeadStock", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Dead Stock").IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<INDeadStockEnq>().WithIcon("trending_down").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass =>
      {
        ass.Add<INDeadStockEnqFilter.siteID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField("DefaultSiteID")));
        ass.Add<INDeadStockEnqFilter.inventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()));
      }))))));
      actions.AddNew("ShowKitSpecifications", (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<InventoryItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Kit Specifications").IsHiddenWhen((BoundedTo<InventoryItemMaint, InventoryItem>.ISharedCondition) BoundedTo<InventoryItemMaint, InventoryItem>.Condition.op_LogicalNot(isKit)).IsSidePanelScreen((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<INKitSpecMaint>().WithIcon("description").WithAssignments((Action<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<INKitSpecHdr.kitInventoryID>((Func<BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<InventoryItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
    })).WithCategories((Action<BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(pricesCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.ConfiguratorCategory, BoundedTo<InventoryItemMaint, InventoryItem>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
    }))));
  }

  [PXMergeAttributes]
  [PXDefault(true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemClass.stkItem> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("F", typeof (SearchFor<INItemClass.itemType>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.itemClassID, Equal<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemClass.itemType> e)
  {
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Specific Type")]
  [PXStringList(new string[] {"PX.Objects.CS.SegmentValue", "PX.Objects.IN.InventoryItem"}, new string[] {"Subitem", "Inventory Item Restriction"})]
  protected virtual void _(PX.Data.Events.CacheAttached<RelationGroup.specificType> e)
  {
  }

  [PXDefault]
  [InventoryRaw(typeof (Where<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>), IsKey = true, DisplayName = "Inventory ID", Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.inventoryCD> e)
  {
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (InventoryItem.itemType))]
  [PXDefault("F", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.itemType), CacheGlobal = true)]
  [PXUIField]
  [INItemTypes.StockList]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.itemType> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.lotSerClassID), CacheGlobal = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.lotSerClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (INPostClass.postClassID), DescriptionField = typeof (INPostClass.descr))]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.postClassID), CacheGlobal = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.postClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<INItemClass.stkItem, Equal<True>>), "The class you have selected can not be assigned to a stock item, because the Stock Item check box is cleared for this class on the Item Classes(IN201000) form.Select another item class which is designated to group stock items.", new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.itemClassID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SubItemAttribute), "ValidateValueOnPersisting", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.defaultSubItemID> e)
  {
  }

  [PXDBString]
  [PXDefault("P")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.postToExpenseAccount> e)
  {
  }

  [PXMergeAttributes]
  [CommodityCodeTypes.StockCommodityCodeList]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.commodityCodeType> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<NotExists<Select2<TaxCategoryDet, InnerJoin<PX.Objects.TX.Tax, On<TaxCategoryDet.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<TaxCategoryDet.taxCategoryID>, And<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<False>, And<PX.Objects.TX.Tax.directTax, Equal<True>>>>>>>), null, new System.Type[] {})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.taxCategoryID> e)
  {
  }

  [AnyInventory(IsKey = true, DirtyRead = true, CacheGlobal = false)]
  [PXRestrictor(typeof (Where<InventoryItem.stkItem, Equal<boolTrue>>), "The inventory item is not a stock item.", new System.Type[] {})]
  [PXDefault]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.inventoryID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SubItemAttribute), "ValidateValueOnPersisting", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POVendorInventory.subItemID> e)
  {
  }

  [StockItem(IsKey = true, DirtyRead = true, CacheGlobal = false, TabOrder = 1)]
  [PXParent(typeof (INItemSite.FK.InventoryItem))]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemSite.inventoryID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [ItemSite]
  [PXUIField(DisplayName = "Warehouse", Enabled = false, TabOrder = 2)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemSite.siteID> e)
  {
  }

  [PXMergeAttributes]
  [INDefaultWarehouse(typeof (INItemSite.siteID), typeof (INItemSite.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemSite.isDefault> e)
  {
  }

  [StockItem(IsKey = true, DirtyRead = true)]
  [PXParent(typeof (INItemCategory.FK.InventoryItem))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemCategory.inventoryID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SubItemAttribute), "ValidateValueOnPersisting", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemXRef.subItemID> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  protected IEnumerable groups() => (IEnumerable) this.GetGroups();

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (INSiteStatusByCostCenter), typeof (INSiteStatusByCostCenter));
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
  }

  protected virtual void LotSerNumValueFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    FbqlSelect<SelectFromBase<InventoryItemLotSerNumVal, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<InventoryItemLotSerNumVal.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, InventoryItemLotSerNumVal>, InventoryItem, InventoryItemLotSerNumVal>.SameAsCurrent>, InventoryItemLotSerNumVal>.View lotSerNumVal = this.lotSerNumVal;
    PXView view = ((PXSelectBase) this.lotSerNumVal).View;
    object[] objArray1 = new object[1]{ e.Row };
    object[] objArray2 = Array.Empty<object>();
    InventoryItemLotSerNumVal itemLotSerNumVal1;
    InventoryItemLotSerNumVal itemLotSerNumVal2 = itemLotSerNumVal1 = (InventoryItemLotSerNumVal) view.SelectSingleBound(objArray1, objArray2);
    ((PXSelectBase<InventoryItemLotSerNumVal>) lotSerNumVal).Current = itemLotSerNumVal1;
    InventoryItemLotSerNumVal itemLotSerNumVal3 = itemLotSerNumVal2;
    e.ReturnState = ((PXSelectBase) this.lotSerNumVal).Cache.GetStateExt<InventoryItemLotSerNumVal.lotSerNumVal>((object) itemLotSerNumVal3);
    INLotSerClass inLotSerClass = (INLotSerClass) PXSelectorAttribute.Select<InventoryItem.lotSerClassID>(sender, e.Row);
    if (inLotSerClass == null || !(inLotSerClass.LotSerTrack != "N"))
      return;
    e.ReturnValue = inLotSerClass.LotSerNumShared.GetValueOrDefault() ? (object) INLotSerClassLotSerNumVal.PK.Find(sender.Graph, inLotSerClass.LotSerClassID)?.LotSerNumVal : (object) itemLotSerNumVal3?.LotSerNumVal;
  }

  protected virtual void LotSerNumValueFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if ((InventoryItem) e.Row == null)
      return;
    string newValue = (string) e.NewValue;
    InventoryItemLotSerNumVal inventoryNumVal = (InventoryItemLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    string lotSerNumVal = inventoryNumVal?.LotSerNumVal;
    if (sender.ObjectsEqual((object) lotSerNumVal, (object) newValue) || (INLotSerClass) PXSelectorAttribute.Select<InventoryItem.lotSerClassID>(sender, e.Row) == null)
      return;
    this.SetLotSerNumber(inventoryNumVal, newValue);
  }

  private void SetLotSerNumber(InventoryItemLotSerNumVal inventoryNumVal, string newNumber)
  {
    if (inventoryNumVal == null)
    {
      if (string.IsNullOrEmpty(newNumber))
        return;
      ((PXSelectBase<InventoryItemLotSerNumVal>) this.lotSerNumVal).Insert(new InventoryItemLotSerNumVal()
      {
        LotSerNumVal = newNumber
      });
    }
    else if (string.IsNullOrWhiteSpace(newNumber))
    {
      ((PXSelectBase<InventoryItemLotSerNumVal>) this.lotSerNumVal).Delete(inventoryNumVal);
    }
    else
    {
      InventoryItemLotSerNumVal copy = (InventoryItemLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).Cache.CreateCopy((object) inventoryNumVal);
      copy.LotSerNumVal = newNumber;
      ((PXSelectBase) this.lotSerNumVal).Cache.Update((object) copy);
    }
  }

  protected override void _(PX.Data.Events.RowSelected<InventoryItem> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    INLotSerClass inLotSerClass = (INLotSerClass) PXSelectorAttribute.Select<InventoryItem.lotSerClassID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row);
    bool? nullable;
    if (inLotSerClass == null)
    {
      PXUIFieldAttribute.SetEnabled<InventoryItemLotSerNumVal.lotSerNumVal>(((PXSelectBase) this.lotSerNumVal).Cache, (object) null, false);
    }
    else
    {
      PXCache cache = ((PXSelectBase) this.lotSerNumVal).Cache;
      InventoryItemLotSerNumVal current = ((PXSelectBase<InventoryItemLotSerNumVal>) this.lotSerNumVal).Current;
      nullable = inLotSerClass.LotSerNumShared;
      int num = nullable.GetValueOrDefault() ? 0 : (inLotSerClass.LotSerTrack != "N" ? 1 : 0);
      PXUIFieldAttribute.SetEnabled<InventoryItemLotSerNumVal.lotSerNumVal>(cache, (object) current, num != 0);
    }
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row).For<InventoryItem.valMethod>((Action<PXUIFieldAttribute>) (fa => fa.Enabled = !e.Row.TemplateItemID.HasValue)).SameFor<InventoryItem.kitItem>();
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache;
    InventoryItem row1 = e.Row;
    int num1;
    if (((PXSelectBase<INPostClass>) this.postclass).Current != null)
    {
      nullable = ((PXSelectBase<INPostClass>) this.postclass).Current.COGSSubFromSales;
      bool flag = false;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<InventoryItem.cOGSSubID>(cache1, (object) row1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<InventoryItem.stdCstVarAcctID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, e.Row != null && e.Row.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<InventoryItem.stdCstVarSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, e.Row != null && e.Row.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<InventoryItem.stdCstRevAcctID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, e.Row != null && e.Row.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<InventoryItem.stdCstRevSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, e.Row != null && e.Row.ValMethod == "T");
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<InventoryItem.defaultSubItemOnEntry>(cache2, (object) null, num2 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache;
    InventoryItem row2 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<InventoryItem.defaultSubItemID>(cache3, (object) row2, num3 != 0);
    INAcctSubDefault.Required(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Args);
    bool flag1 = ((PXSelectBase<INSiteStatusByCostCenter>) this.nonemptysitestatuses).SelectSingle(Array.Empty<object>()) != null;
    PXUIFieldAttribute.SetEnabled<InventoryItem.baseUnit>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, !flag1 && !e.Row.TemplateItemID.HasValue);
    ((PXSelectBase) this.Boxes).Cache.AllowInsert = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    ((PXSelectBase) this.Boxes).Cache.AllowUpdate = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    ((PXSelectBase) this.Boxes).Cache.AllowSelect = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    PXUIFieldAttribute.SetEnabled<InventoryItem.packSeparately>(((PXSelectBase) this.Item).Cache, (object) ((PXSelectBase<InventoryItem>) this.Item).Current, e.Row.PackageOption == "W");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.qty>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "Q");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.uOM>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "Q");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxQty>(((PXSelectBase) this.Boxes).Cache, (object) null, EnumerableExtensions.IsIn<string>(e.Row.PackageOption, "W", "V"));
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxWeight>(((PXSelectBase) this.Boxes).Cache, (object) null, EnumerableExtensions.IsIn<string>(e.Row.PackageOption, "W", "V"));
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxVolume>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "V");
    if (PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
      this.ValidatePackaging(e.Row);
    this.SetLastCostEnabled();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.lotSerClassID> e)
  {
    INLotSerClass inLotSerClass1 = INLotSerClass.PK.Find((PXGraph) this, e.Row.OrigLotSerClassID);
    if (inLotSerClass1 == null)
      return;
    INLotSerClass inLotSerClass2 = INLotSerClass.PK.Find((PXGraph) this, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.lotSerClassID>, InventoryItem, object>) e).NewValue);
    if (inLotSerClass1.LotSerAssign == "U" && EnumerableExtensions.IsIn<string>(inLotSerClass1.LotSerTrack, "S", "L") && (inLotSerClass2?.LotSerTrack == "N" || inLotSerClass2?.LotSerAssign == "R"))
    {
      if (this.IsWhenUsedQtyStillPresent())
        throw new PXSetPropertyException("Lot/serial class cannot be changed when its tracking method is not compatible with the previous class and the item is in use.");
    }
    else
    {
      if (inLotSerClass2 != null && !(inLotSerClass1.LotSerTrack != inLotSerClass2.LotSerTrack))
      {
        bool? serTrackExpiration1 = inLotSerClass1.LotSerTrackExpiration;
        bool? serTrackExpiration2 = inLotSerClass2.LotSerTrackExpiration;
        if (serTrackExpiration1.GetValueOrDefault() == serTrackExpiration2.GetValueOrDefault() & serTrackExpiration1.HasValue == serTrackExpiration2.HasValue && !(inLotSerClass1.LotSerAssign != inLotSerClass2.LotSerAssign))
          return;
      }
      if (InventoryItemMaint.IsQtyStillPresent((PXGraph) this, e.Row.InventoryID))
        throw new PXSetPropertyException("Lot/serial class cannot be changed when its tracking method is not compatible with the previous class and the item is in use.");
    }
  }

  private bool IsWhenUsedQtyStillPresent()
  {
    return PXResultset<INSiteStatusByCostCenter>.op_Implicit(PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INSiteStatusByCostCenter.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INSiteStatusByCostCenter>, InventoryItem, INSiteStatusByCostCenter>.SameAsCurrent.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINReceipts, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyInTransit, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINIssues, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINAssemblyDemand, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINAssemblySupply, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtySOShipped, IBqlDecimal>.IsNotEqual<decimal0>>>>.Or<BqlOperand<INSiteStatusByCostCenter.qtySOShipping, IBqlDecimal>.IsNotEqual<decimal0>>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())) != null;
  }

  public static bool IsQtyStillPresent(PXGraph graph, int? inventoryID)
  {
    INItemLotSerial inItemLotSerial = PXResultset<INItemLotSerial>.op_Implicit(PXSelectBase<INItemLotSerial, PXViewOf<INItemLotSerial>.BasedOn<SelectFromBase<INItemLotSerial, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemLotSerial.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemLotSerial.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) inventoryID
    }));
    INSiteStatusByCostCenter statusByCostCenter = PXResultset<INSiteStatusByCostCenter>.op_Implicit(PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINReceipts, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyInTransit, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINIssues, IBqlDecimal>.IsNotEqual<decimal0>>>, Or<BqlOperand<INSiteStatusByCostCenter.qtyINAssemblyDemand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Or<BqlOperand<INSiteStatusByCostCenter.qtyINAssemblySupply, IBqlDecimal>.IsNotEqual<decimal0>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) inventoryID
    }));
    return inItemLotSerial != null || statusByCostCenter != null;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.defaultSubItemID> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.defaultSubItemID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<InventoryItem> e)
  {
    this.UpdateItemSite(e.Row, e.OldRow);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<InventoryItem>>) e).Cache.ObjectsEqual<InventoryItem.lotSerClassID>((object) e.Row, (object) e.OldRow))
      return;
    INLotSerClass inLotSerClass = (INLotSerClass) PXSelectorAttribute.Select<InventoryItem.lotSerClassID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<InventoryItem>>) e).Cache, (object) e.Row);
    if (inLotSerClass == null)
      return;
    InventoryItemLotSerNumVal itemLotSerNumVal1 = ((PXSelectBase<InventoryItemLotSerNumVal>) this.lotSerNumVal).Current;
    if (itemLotSerNumVal1 == null)
      itemLotSerNumVal1 = (InventoryItemLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).View.SelectSingleBound(new object[1]
      {
        (object) inLotSerClass
      }, Array.Empty<object>());
    InventoryItemLotSerNumVal inventoryNumVal = itemLotSerNumVal1;
    if (inLotSerClass.LotSerTrack == "N")
    {
      this.SetLotSerNumber(inventoryNumVal, (string) null);
    }
    else
    {
      if (inventoryNumVal == null)
      {
        InventoryItemLotSerNumVal itemLotSerNumVal2 = ((PXSelectBase) this.lotSerNumVal).Cache.Deleted.OfType<InventoryItemLotSerNumVal>().FirstOrDefault<InventoryItemLotSerNumVal>();
        if (itemLotSerNumVal2 != null)
        {
          this.SetLotSerNumber(inventoryNumVal, itemLotSerNumVal2.LotSerNumVal);
          return;
        }
      }
      else if (!string.IsNullOrEmpty(inventoryNumVal.LotSerNumVal))
        return;
      this.SetLotSerNumber(inventoryNumVal, "000000");
    }
  }

  protected virtual void UpdateItemSite(InventoryItem item, InventoryItem oldItem)
  {
    if ((!((PXSelectBase) this.Item).Cache.ObjectsEqual<InventoryItem.valMethod, InventoryItem.markupPct, InventoryItem.aBCCodeID, InventoryItem.aBCCodeIsFixed, InventoryItem.movementClassID, InventoryItem.movementClassIsFixed, InventoryItem.salesUnit>((object) item, (object) oldItem) ? 1 : (!((PXSelectBase) this.Item).Cache.ObjectsEqual<InventoryItem.purchaseUnit, InventoryItem.productManagerID, InventoryItem.productWorkgroupID, InventoryItem.itemClassID, InventoryItem.planningMethod>((object) item, (object) oldItem) ? 1 : 0)) == 0 && !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.itemsiterecords).Cache.Inserted))
      return;
    foreach (PXResult<INItemSite> pxResult in PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemSite.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSite>, InventoryItem, INItemSite>.SameAsCurrent>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      bool flag1 = false;
      if (!string.Equals(item.ValMethod, oldItem.ValMethod) || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.ValMethod = item.ValMethod;
        flag1 = true;
      }
      bool? nullable1 = inItemSite.MarkupPctOverride;
      bool flag2 = false;
      if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.MarkupPct = item.MarkupPct;
        flag1 = true;
      }
      nullable1 = inItemSite.ABCCodeOverride;
      bool flag3 = false;
      if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.ABCCodeID = item.ABCCodeID;
        inItemSite.ABCCodeIsFixed = item.ABCCodeIsFixed;
        flag1 = true;
      }
      nullable1 = inItemSite.MovementClassOverride;
      bool flag4 = false;
      if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.MovementClassID = item.MovementClassID;
        inItemSite.MovementClassIsFixed = item.MovementClassIsFixed;
        flag1 = true;
      }
      if (!string.Equals(item.SalesUnit, oldItem.SalesUnit) || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.DfltSalesUnit = item.SalesUnit;
        flag1 = true;
      }
      if (!string.Equals(item.PurchaseUnit, oldItem.PurchaseUnit) || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite) == 2)
      {
        inItemSite.DfltPurchaseUnit = item.PurchaseUnit;
        flag1 = true;
      }
      nullable1 = inItemSite.ProductManagerOverride;
      if (!nullable1.GetValueOrDefault())
      {
        int? nullable2 = inItemSite.ProductManagerID;
        int? nullable3 = item.ProductManagerID;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          nullable3 = inItemSite.ProductWorkgroupID;
          nullable2 = item.ProductWorkgroupID;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
            goto label_20;
        }
        inItemSite.ProductManagerID = item.ProductManagerID;
        inItemSite.ProductWorkgroupID = item.ProductWorkgroupID;
        flag1 = true;
      }
label_20:
      if (!string.Equals(item.PlanningMethod, oldItem.PlanningMethod))
      {
        inItemSite.PlanningMethod = item.PlanningMethod;
        flag1 = true;
      }
      if (flag1)
        GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) inItemSite, true);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<InventoryItemCurySettings> e)
  {
    this.UpdateItemSiteByCurySettings(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<InventoryItemCurySettings> e)
  {
    if (((PXSelectBase) this.ItemCurySettings).Cache.ObjectsEqual<InventoryItemCurySettings.pendingStdCost, InventoryItemCurySettings.pendingStdCostDate, InventoryItemCurySettings.stdCost, InventoryItemCurySettings.basePrice, InventoryItemCurySettings.recPrice, InventoryItemCurySettings.preferredVendorID, InventoryItemCurySettings.preferredVendorLocationID>((object) e.Row, (object) e.OldRow) && !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.itemsiterecords).Cache.Inserted))
      return;
    this.UpdateItemSiteByCurySettings(e.Row);
  }

  protected virtual void UpdateItemSiteByCurySettings(InventoryItemCurySettings itemCurySettings)
  {
    foreach (PXResult<INItemSite, INSite, INSiteStatusSummary> pxResult in ((PXSelectBase<INItemSite>) this.itemsiterecords).Select(new object[1]
    {
      (object) itemCurySettings.CuryID
    }))
    {
      INItemSite inItemSite1 = PXResult<INItemSite, INSite, INSiteStatusSummary>.op_Implicit(pxResult);
      bool flag1 = false;
      if (inItemSite1.ValMethod == "T")
      {
        bool? stdCostOverride = inItemSite1.StdCostOverride;
        bool flag2 = false;
        if (stdCostOverride.GetValueOrDefault() == flag2 & stdCostOverride.HasValue)
        {
          DateTime? nullable1 = itemCurySettings.PendingStdCostDate;
          if (nullable1.HasValue)
          {
            inItemSite1.PendingStdCost = itemCurySettings.PendingStdCost;
            inItemSite1.PendingStdCostDate = itemCurySettings.PendingStdCostDate;
            inItemSite1.PendingStdCostReset = new bool?(false);
          }
          else
          {
            Decimal? stdCost1 = itemCurySettings.StdCost;
            Decimal? stdCost2 = inItemSite1.StdCost;
            bool flag3 = stdCost1.GetValueOrDefault() == stdCost2.GetValueOrDefault() & stdCost1.HasValue == stdCost2.HasValue;
            inItemSite1.PendingStdCost = flag3 ? itemCurySettings.PendingStdCost : itemCurySettings.StdCost;
            INItemSite inItemSite2 = inItemSite1;
            nullable1 = new DateTime?();
            DateTime? nullable2 = nullable1;
            inItemSite2.PendingStdCostDate = nullable2;
            inItemSite1.PendingStdCostReset = new bool?(!flag3);
          }
          flag1 = true;
        }
      }
      bool? basePriceOverride = inItemSite1.BasePriceOverride;
      bool flag4 = false;
      if (basePriceOverride.GetValueOrDefault() == flag4 & basePriceOverride.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite1) == 2)
      {
        inItemSite1.BasePrice = itemCurySettings.BasePrice;
        flag1 = true;
      }
      bool? recPriceOverride = inItemSite1.RecPriceOverride;
      bool flag5 = false;
      if (recPriceOverride.GetValueOrDefault() == flag5 & recPriceOverride.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite1) == 2)
      {
        inItemSite1.RecPrice = itemCurySettings.RecPrice;
        flag1 = true;
      }
      bool? preferredVendorOverride = inItemSite1.PreferredVendorOverride;
      bool flag6 = false;
      if (preferredVendorOverride.GetValueOrDefault() == flag6 & preferredVendorOverride.HasValue || ((PXSelectBase) this.itemsiterecords).Cache.GetStatus((object) inItemSite1) == 2)
      {
        inItemSite1.PreferredVendorID = itemCurySettings.PreferredVendorID;
        inItemSite1.PreferredVendorLocationID = itemCurySettings.PreferredVendorLocationID;
        flag1 = true;
      }
      if (flag1)
        GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) inItemSite1, true);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.dfltSiteID> e)
  {
    INItemSite inItemSite1 = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<BqlField<InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.DfltSiteID
    }));
    INSite site = INSite.PK.Find((PXGraph) this, e.Row.DfltSiteID);
    if (inItemSite1 != null)
    {
      INItemSite copy = PXCache<INItemSite>.CreateCopy(inItemSite1);
      copy.IsDefault = new bool?(true);
      ((PXSelectBase<INItemSite>) this.itemsiterecords).Update(copy);
      e.Row.DfltShipLocationID = copy.DfltShipLocationID;
      e.Row.DfltReceiptLocationID = copy.DfltReceiptLocationID;
      e.Row.DfltPutawayLocationID = copy.DfltPutawayLocationID;
    }
    else if (site != null)
    {
      INItemSite itemsite = new INItemSite();
      itemsite.InventoryID = e.Row.InventoryID;
      itemsite.SiteID = e.Row.DfltSiteID;
      INItemSiteMaint.DefaultItemSiteByItem((PXGraph) this, itemsite, ((PXSelectBase<InventoryItem>) this.Item).Current, site, ((PXSelectBase<INPostClass>) this.postclass).Current, e.Row);
      itemsite.IsDefault = new bool?(true);
      itemsite.StdCostOverride = new bool?(false);
      itemsite.DfltReceiptLocationID = site.ReceiptLocationID;
      itemsite.DfltShipLocationID = site.ShipLocationID;
      itemsite.DfltPutawayLocationID = new int?();
      ((PXSelectBase<INItemSite>) this.itemsiterecords).Insert(itemsite);
      e.Row.DfltShipLocationID = itemsite.DfltShipLocationID;
      e.Row.DfltReceiptLocationID = itemsite.DfltReceiptLocationID;
    }
    else
    {
      e.Row.DfltShipLocationID = new int?();
      e.Row.DfltReceiptLocationID = new int?();
      e.Row.DfltPutawayLocationID = new int?();
      foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) this.itemsiterecords).Select(new object[1]
      {
        (object) e.Row.CuryID
      }))
      {
        INItemSite inItemSite2 = PXResult<INItemSite>.op_Implicit(pxResult);
        if (inItemSite2.IsDefault.GetValueOrDefault())
        {
          inItemSite2.IsDefault = new bool?(false);
          GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) inItemSite2, true);
        }
      }
    }
  }

  protected override void _(PX.Data.Events.RowPersisting<InventoryItem> e)
  {
    base._(e);
    INAcctSubDefault.Required(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Args);
    if (e.Row.IsSplitted.GetValueOrDefault())
    {
      if (string.IsNullOrEmpty(e.Row.DeferredCode))
      {
        if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache.RaiseExceptionHandling<InventoryItem.deferredCode>((object) e.Row, (object) e.Row.DeferredCode, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[deferredCode]"
        })))
          throw new PXRowPersistingException("deferredCode", (object) e.Row.DeferredCode, "'{0}' cannot be empty.", new object[1]
          {
            (object) "deferredCode"
          });
      }
      List<INComponent> list = GraphHelper.RowCast<INComponent>((IEnumerable) ((PXSelectBase<INComponent>) this.Components).Select(Array.Empty<object>())).ToList<INComponent>();
      InventoryItemMaintBase.VerifyComponentPercentages(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache, e.Row, (IEnumerable<INComponent>) list);
      InventoryItemMaintBase.VerifyOnlyOneResidualComponent(((PXSelectBase) this.Components).Cache, (IEnumerable<INComponent>) list);
      InventoryItemMaintBase.CheckSameTermOnAllComponents(((PXSelectBase) this.Components).Cache, (IEnumerable<INComponent>) list);
    }
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && e.Row.ValMethod == "S" && ((PXSelectBase<INLotSerClass>) this.lotserclass).Current != null && (((PXSelectBase<INLotSerClass>) this.lotserclass).Current.LotSerTrack == "N" || ((PXSelectBase<INLotSerClass>) this.lotserclass).Current.LotSerAssign != "R") && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache.RaiseExceptionHandling<InventoryItem.valMethod>((object) e.Row, (object) "S", (Exception) new PXSetPropertyException("Specific valuated items should be lot or serial numbered during receipt.")))
      throw new PXRowPersistingException(typeof (InventoryItem.valMethod).Name, (object) "S", "Specific valuated items should be lot or serial numbered during receipt.", new object[1]
      {
        (object) typeof (InventoryItem.valMethod).Name
      });
    if (PXDBOperationExt.Command(e.Operation) == 3)
    {
      PXDatabase.Delete<INSiteStatusByCostCenter>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<INLocationStatusByCostCenter>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLocationStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<INLotSerialStatusByCostCenter>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.qtyAvail>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<INCostStatus>(new PXDataFieldRestrict[2]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INCostStatus.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INCostStatus.qtyOnHand>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<INItemCostHist>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.finYtdQty>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.finYtdCost>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<INItemSiteHist>(new PXDataFieldRestrict[2]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHist.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHist.finYtdQty>((PXDbType) 5, new int?(8), (object) 0M, (PXComp) 0)
      });
      PXDatabase.Delete<CSAnswers>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<CSAnswers.refNoteID>((PXDbType) 14, (object) e.Row.NoteID)
      });
    }
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      INLotSerClass current = ((PXSelectBase<INLotSerClass>) this.lotserclass).Current;
      if (current != null && current.LotSerTrack != "N")
      {
        bool? lotSerNumShared = current.LotSerNumShared;
        bool flag = false;
        if (lotSerNumShared.GetValueOrDefault() == flag & lotSerNumShared.HasValue)
        {
          PXStringState valueExt = (PXStringState) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache.GetValueExt((object) e.Row, "LotSerNumVal");
          if (valueExt == null || ((PXFieldState) valueExt).Value == null)
          {
            if (PXResultset<INLotSerSegment>.op_Implicit(PXSelectBase<INLotSerSegment, PXViewOf<INLotSerSegment>.BasedOn<SelectFromBase<INLotSerSegment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerSegment.lotSerClassID, Equal<P.AsString>>>>>.And<BqlOperand<INLotSerSegment.segmentType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
            {
              (object) current.LotSerClassID,
              (object) "N"
            })) != null)
              PXUIFieldAttribute.SetError<InventoryItemLotSerNumVal.lotSerNumVal>(((PXSelectBase) this.lotSerNumVal).Cache, (object) null, ((Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
              {
                (object) ((PXFieldState) valueExt).DisplayName
              })).Message);
          }
        }
      }
    }
    if (PXDBOperationExt.Command(e.Operation) != 1 || !(e.Row.LotSerClassID != e.Row.OrigLotSerClassID) || ((PXSelectBase<INLotSerClass>) this.lotserclass).Current == null || !(((PXSelectBase<INLotSerClass>) this.lotserclass).Current.LotSerAssign != "U") || !(INLotSerClass.PK.Find((PXGraph) this, e.Row.OrigLotSerClassID)?.LotSerAssign == "U"))
      return;
    PXDatabase.Delete<INItemLotSerial>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemLotSerial.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemLotSerial.lotSerAssign>((PXDbType) 3, new int?(1), (object) "U", (PXComp) 0)
    });
    PXDatabase.Delete<INSiteLotSerial>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteLotSerial.inventoryID>((PXDbType) 8, (object) e.Row.InventoryID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteLotSerial.lotSerAssign>((PXDbType) 3, new int?(1), (object) "U", (PXComp) 0)
    });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.dfltReceiptLocationID> e)
  {
    this.UpdateItemSiteDefaultField<InventoryItemCurySettings.dfltReceiptLocationID, INItemSite.dfltReceiptLocationID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.dfltPutawayLocationID> e)
  {
    this.UpdateItemSiteDefaultField<InventoryItemCurySettings.dfltPutawayLocationID, INItemSite.dfltPutawayLocationID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.dfltShipLocationID> e)
  {
    this.UpdateItemSiteDefaultField<InventoryItemCurySettings.dfltShipLocationID, INItemSite.dfltShipLocationID>(e.Row);
  }

  protected virtual void UpdateItemSiteDefaultField<TSourceField, TDestinationField>(
    InventoryItemCurySettings itemCurySettings)
    where TSourceField : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TSourceField>
    where TDestinationField : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TDestinationField>
  {
    int? dfltSiteId = itemCurySettings.DfltSiteID;
    if (!dfltSiteId.HasValue)
      return;
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<BqlField<InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) dfltSiteId.GetValueOrDefault()
    }));
    if (inItemSite == null)
      return;
    ((PXSelectBase) this.itemsiterecords).Cache.SetValue<TDestinationField>((object) inItemSite, ((PXCache) GraphHelper.Caches<InventoryItemCurySettings>((PXGraph) this)).GetValue<TSourceField>((object) itemCurySettings));
    GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) inItemSite, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.defaultSubItemID> e)
  {
    this.AddVendorDetail(e.Row, (InventoryItemCurySettings) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.preferredVendorLocationID> e)
  {
    this.AddVendorDetail((InventoryItem) null, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.planningMethod> e)
  {
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.planningMethod>>) e).Cache.ObjectsEqual<InventoryItem.planningMethod>(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.planningMethod>, InventoryItem, object>) e).OldValue, e.NewValue))
      return;
    foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) this.itemsiterecords).Select(Array.Empty<object>()))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      if (e.NewValue.Equals((object) "N"))
        inItemSite.ReplenishmentMethod = "N";
      inItemSite.PlanningMethod = (string) e.NewValue;
      ((PXSelectBase<INItemSite>) this.itemsiterecords).Update(inItemSite);
    }
  }

  private POVendorInventory AddVendorDetail(
    InventoryItem row,
    InventoryItemCurySettings curySettings)
  {
    if (row == null)
      row = ((PXSelectBase<InventoryItem>) this.Item).Current;
    if (row != null && curySettings == null)
      curySettings = ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).SelectSingle(new object[1]
      {
        (object) row.InventoryID
      });
    if ((curySettings != null ? (!curySettings.PreferredVendorID.HasValue ? 1 : 0) : 1) != 0 || (row != null ? (!row.DefaultSubItemID.HasValue ? 1 : 0) : 1) != 0)
      return (POVendorInventory) null;
    POVendorInventory poVendorInventory = PXResultset<POVendorInventory>.op_Implicit(PXSelectBase<POVendorInventory, PXViewOf<POVendorInventory>.BasedOn<SelectFromBase<POVendorInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventory.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<POVendorInventory.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<POVendorInventory.vendorID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventory.vendorLocationID, Equal<P.AsInt>>>>>.Or<BqlOperand<POVendorInventory.vendorLocationID, IBqlInt>.IsNull>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) row.InventoryID,
      (object) row.DefaultSubItemID,
      (object) curySettings.PreferredVendorID,
      (object) curySettings.PreferredVendorLocationID
    }));
    if (poVendorInventory == null)
      poVendorInventory = (POVendorInventory) ((PXSelectBase) this.VendorItems).Cache.Insert((object) new POVendorInventory()
      {
        InventoryID = row.InventoryID,
        SubItemID = row.DefaultSubItemID,
        PurchaseUnit = row.PurchaseUnit,
        VendorID = curySettings.PreferredVendorID,
        VendorLocationID = curySettings.PreferredVendorLocationID
      });
    return poVendorInventory;
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID> e)
  {
    base._(e);
    InventoryItem row = e.Row;
    bool flag = row != null && row.IsConversionMode.GetValueOrDefault();
    if (flag)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.itemType>((object) e.Row);
    if (this.doResetDefaultsOnItemClassChange | flag)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.lotSerClassID>((object) e.Row);
      if (this.doResetDefaultsOnItemClassChange && (!flag || e.Row?.ValMethod == null))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.valMethod>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.countryOfOrigin>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.hSTariffCode>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.planningMethod>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.replenishmentSource>((object) e.Row);
    }
    this.AppendGroupMask(e.Row.ItemClassID, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.GetStatus((object) e.Row) == 2 | flag);
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.GetStatus((object) e.Row) == 2 | flag))
      return;
    foreach (PXResult<INItemRep> pxResult in PXSelectBase<INItemRep, PXViewOf<INItemRep>.BasedOn<SelectFromBase<INItemRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemRep.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.InventoryID
    }))
      ((PXSelectBase<INItemRep>) this.replenishment).Delete(PXResult<INItemRep>.op_Implicit(pxResult));
    foreach (PXResult<INItemClassRep> pxResult in PXSelectBase<INItemClassRep, PXViewOf<INItemClassRep>.BasedOn<SelectFromBase<INItemClassRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClassRep.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.ParentItemClassID
    }))
    {
      INItemClassRep inItemClassRep = PXResult<INItemClassRep>.op_Implicit(pxResult);
      ((PXSelectBase<INItemRep>) this.replenishment).Insert(new INItemRep()
      {
        ReplenishmentClassID = inItemClassRep.ReplenishmentClassID,
        ReplenishmentMethod = inItemClassRep.ReplenishmentMethod,
        ReplenishmentPolicyID = inItemClassRep.ReplenishmentPolicyID,
        LaunchDate = inItemClassRep.LaunchDate,
        TerminationDate = inItemClassRep.TerminationDate,
        CuryID = inItemClassRep.CuryID
      });
    }
  }

  protected override void _(PX.Data.Events.RowInserted<InventoryItem> e)
  {
    e.Row.TotalPercentage = new Decimal?((Decimal) 100);
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.replenishment).Cache
    }))
    {
      foreach (PXResult<INItemClassRep> pxResult in PXSelectBase<INItemClassRep, PXViewOf<INItemClassRep>.BasedOn<SelectFromBase<INItemClassRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClassRep.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.ParentItemClassID
      }))
      {
        INItemClassRep inItemClassRep = PXResult<INItemClassRep>.op_Implicit(pxResult);
        ((PXSelectBase<INItemRep>) this.replenishment).Insert(new INItemRep()
        {
          ReplenishmentClassID = inItemClassRep.ReplenishmentClassID,
          ReplenishmentMethod = inItemClassRep.ReplenishmentMethod,
          ReplenishmentPolicyID = inItemClassRep.ReplenishmentPolicyID,
          LaunchDate = inItemClassRep.LaunchDate,
          TerminationDate = inItemClassRep.TerminationDate,
          CuryID = inItemClassRep.CuryID
        });
      }
    }
    base._(e);
    this.AppendGroupMask(e.Row.ItemClassID, true);
    this._JustInserted = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.purchaseUnit> e)
  {
    if (e.Row == null || string.Equals(e.Row.PurchaseUnit, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.purchaseUnit>, InventoryItem, object>) e).OldValue, StringComparison.InvariantCultureIgnoreCase))
      return;
    IEnumerable<POVendorInventory> source = GraphHelper.RowCast<POVendorInventory>((IEnumerable) ((IEnumerable<PXResult<POVendorInventory>>) PXSelectBase<POVendorInventory, PXViewOf<POVendorInventory>.BasedOn<SelectFromBase<POVendorInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventory.inventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventory.purchaseUnit, Equal<P.AsString>>>>>.Or<BqlOperand<POVendorInventory.purchaseUnit, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) e.Row.InventoryID,
      (object) e.Row.PurchaseUnit,
      ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.purchaseUnit>, InventoryItem, object>) e).OldValue
    })).AsEnumerable<PXResult<POVendorInventory>>());
    foreach (POVendorInventory poVendorInventory1 in source.Where<POVendorInventory>((Func<POVendorInventory, bool>) (x => x.PurchaseUnit == (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.purchaseUnit>, InventoryItem, object>) e).OldValue)))
    {
      POVendorInventory detailWithOldPurchaseUnit = poVendorInventory1;
      if (source.FirstOrDefault<POVendorInventory>((Func<POVendorInventory, bool>) (x =>
      {
        if (!(x.PurchaseUnit == e.Row.PurchaseUnit))
          return false;
        int? vendorId1 = x.VendorID;
        int? vendorId2 = detailWithOldPurchaseUnit.VendorID;
        return vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue;
      })) == null)
      {
        Decimal? lastPrice = detailWithOldPurchaseUnit.LastPrice;
        if (lastPrice.HasValue)
        {
          POVendorInventory poVendorInventory2 = detailWithOldPurchaseUnit;
          InventoryItem row = e.Row;
          string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.purchaseUnit>, InventoryItem, object>) e).OldValue;
          lastPrice = detailWithOldPurchaseUnit.LastPrice;
          Decimal cost = lastPrice.Value;
          string purchaseUnit = e.Row.PurchaseUnit;
          Decimal? nullable = new Decimal?(POItemCostManager.ConvertUOM((PXGraph) this, row, oldValue, cost, purchaseUnit));
          poVendorInventory2.LastPrice = nullable;
        }
        detailWithOldPurchaseUnit.PurchaseUnit = e.Row.PurchaseUnit;
        ((PXSelectBase<POVendorInventory>) this.VendorItems).Update(detailWithOldPurchaseUnit);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.valMethod> e)
  {
    if (e.Row.ValMethod == null || string.Equals(e.Row.ValMethod, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.valMethod>, InventoryItem, object>) e).NewValue) || e.Row.IsConversionMode.GetValueOrDefault() && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.valMethod>, InventoryItem, object>) e).NewValue == null)
      return;
    ParameterExpression parameterExpression;
    // ISSUE: field reference
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    INCostStatus inCostStatus = PXResult<INCostStatus>.op_Implicit(((IQueryable<PXResult<INCostStatus>>) PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INCostStatus.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INCostStatus>, InventoryItem, INCostStatus>.SameAsCurrent.And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).OrderBy<PXResult<INCostStatus>, int>(Expression.Lambda<Func<PXResult<INCostStatus>, int>>((Expression) Expression.Condition((Expression) Expression.Equal(((INCostStatus) layer).CostSiteID, (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) this, typeof (InventoryItemMaint)), FieldInfo.GetFieldFromHandle(__fieldref (InventoryItemMaintBase.insetup))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<INSetup>.get_Current), __typeref (PXSelectBase<INSetup>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (INSetup.get_TransitSiteID)))), (Expression) Expression.Constant((object) 1, typeof (int)), (Expression) Expression.Constant((object) 0, typeof (int))), parameterExpression)).FirstOrDefault<PXResult<INCostStatus>>());
    if (inCostStatus != null)
    {
      INValMethod.ListAttribute listAttribute = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.valMethod>>) e).Cache.GetAttributesReadonly<InventoryItem.valMethod>((object) e.Row).OfType<INValMethod.ListAttribute>().First<INValMethod.ListAttribute>();
      string str1;
      listAttribute.ValueLabelDic.TryGetValue(e.Row.ValMethod, out str1);
      string str2;
      listAttribute.ValueLabelDic.TryGetValue((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.valMethod>, InventoryItem, object>) e).NewValue, out str2);
      int? costSiteId = inCostStatus.CostSiteID;
      int? transitSiteId = ((PXSelectBase<INSetup>) this.insetup).Current.TransitSiteID;
      throw new PXSetPropertyException(costSiteId.GetValueOrDefault() == transitSiteId.GetValueOrDefault() & costSiteId.HasValue == transitSiteId.HasValue ? "The valuation method cannot be changed from {0} to {1} because the item is in transit." : "Valuation method cannot be changed from '{0}' to '{1}' while stock is not zero.", new object[2]
      {
        (object) str1,
        (object) str2
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<InventoryItem> e)
  {
    if (e.Row == null)
      return;
    INSiteStatusByCostCenter statusByCostCenter = ((PXSelectBase<INSiteStatusByCostCenter>) this.nonemptysitestatuses).SelectSingle(Array.Empty<object>());
    if (statusByCostCenter != null)
      throw new PXException("There is a non-zero quantity of the '{0}' item at the '{1}' warehouse.", new object[2]
      {
        (object) e.Row.InventoryCD,
        ((PXSelectBase<INSiteStatusByCostCenter>) this.nonemptysitestatuses).GetValueExt<INSiteStatusByCostCenter.siteID>(statusByCostCenter)
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.packageOption> e)
  {
    if (e.Row == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.packageOption>, InventoryItem, object>) e).NewValue.ToString() == "Q") || ((PXSelectBase<INItemBoxEx>) this.Boxes).Select(Array.Empty<object>()).Count != 0)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.packageOption>>) e).Cache.RaiseExceptionHandling<InventoryItem.packageOption>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.packageOption>, InventoryItem, object>) e).NewValue, (Exception) new PXSetPropertyException("At least one box must be specified in the Boxes grid for the given packaging option.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.taxCategoryID> e)
  {
    string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.taxCategoryID>, InventoryItem, object>) e).NewValue as string;
    if (e.Row == null || string.IsNullOrEmpty(newValue))
      return;
    TaxCategoryDet taxCategoryDet = PXResultset<TaxCategoryDet>.op_Implicit(PXSelectBase<TaxCategoryDet, PXViewOf<TaxCategoryDet>.BasedOn<SelectFromBase<TaxCategoryDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.TaxCategory>.On<BqlOperand<PX.Objects.TX.TaxCategory.taxCategoryID, IBqlString>.IsEqual<TaxCategoryDet.taxCategoryID>>>, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxCategoryID, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.TX.TaxCategory.taxCatFlag, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.TX.Tax.directTax, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newValue
    }));
    if (taxCategoryDet != null)
      throw new PXSetPropertyException("The {0} tax category containing the {1} direct-entry tax cannot be selected for a stock item.", new object[2]
      {
        (object) taxCategoryDet.TaxCategoryID,
        (object) taxCategoryDet.TaxID
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.packageOption> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.PackageOption == "Q")
      e.Row.PackSeparately = new bool?(true);
    else if (e.Row.PackageOption == "V")
    {
      e.Row.PackSeparately = new bool?(false);
    }
    else
    {
      if (!(e.Row.PackageOption == "N"))
        return;
      e.Row.PackSeparately = new bool?(false);
      foreach (PXResult<INItemBoxEx> pxResult in ((PXSelectBase<INItemBoxEx>) this.Boxes).Select(Array.Empty<object>()))
        ((PXSelectBase<INItemBoxEx>) this.Boxes).Delete(PXResult<INItemBoxEx>.op_Implicit(pxResult));
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<InventoryItem> e)
  {
    if (e.TranStatus != 1)
      return;
    DiscountEngine.RemoveFromCachedInventoryPriceClasses(e.Row.InventoryID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.valMethod> e)
  {
    ((PXSelectBase) this.ItemCurySettings).Cache.RaiseRowSelected((object) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemCost> e)
  {
    this.SetLastCostEnabled();
    INItemCost row = e.Row;
    if (row == null)
      return;
    Decimal? avgCost = row.AvgCost;
    Decimal num = 0M;
    if (!(avgCost.GetValueOrDefault() < num & avgCost.HasValue))
      return;
    ((PXSelectBase) this.ItemCosts).Cache.RaiseExceptionHandling<INItemCost.avgCost>((object) row, (object) row.AvgCost, (Exception) new PXSetPropertyException("Negative average cost is caused by a negative quantity of the item in one of its warehouses.", (PXErrorLevel) 2));
  }

  protected virtual void SetLastCostEnabled()
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current == null)
      return;
    Lazy<bool> lazy = Lazy.By<bool>((Func<bool>) (() => ((IEnumerable<PXResult<INItemSite>>) PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemSite.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSite>, InventoryItem, INItemSite>.SameAsCurrent>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new InventoryItem[1]
    {
      ((PXSelectBase<InventoryItem>) this.Item).Current
    }, Array.Empty<object>())).AsEnumerable<PXResult<INItemSite>>().Any<PXResult<INItemSite>>()));
    PXUIFieldAttribute.SetEnabled<INItemCost.lastCost>(((PXSelectBase) this.ItemCosts).Cache, (object) null, EnumerableExtensions.IsNotIn<string>(((PXSelectBase<InventoryItem>) this.Item).Current.ValMethod, "T", "S") && (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || lazy.Value));
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemCost> e)
  {
    if (e.Row == null)
      return;
    Decimal? lastCost = e.Row.LastCost;
    Decimal num = 0M;
    if (lastCost.GetValueOrDefault() == num & lastCost.HasValue)
      return;
    lastCost = e.Row.LastCost;
    if (!lastCost.HasValue)
      return;
    this.UpdateLastCost(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INItemCost> e)
  {
    if (e.Row == null || e.OldRow == null)
      return;
    Decimal? lastCost1 = e.Row.LastCost;
    Decimal? lastCost2 = e.OldRow.LastCost;
    if (lastCost1.GetValueOrDefault() == lastCost2.GetValueOrDefault() & lastCost1.HasValue == lastCost2.HasValue)
      return;
    lastCost2 = e.Row.LastCost;
    if (!lastCost2.HasValue)
      return;
    this.UpdateLastCost(e.Row);
  }

  private void UpdateLastCost(INItemCost row)
  {
    foreach (ItemStats itemStats in ((PXSelectBase) this.itemstats).Cache.Inserted)
      ((PXSelectBase) this.itemstats).Cache.Delete((object) itemStats);
    DateTime lastCostTime = INReleaseProcess.GetLastCostTime(((PXSelectBase) this.itemstats).Cache);
    foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) this.itemsiterecords).Select(Array.Empty<object>()))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      ItemStats itemStats1 = new ItemStats();
      itemStats1.InventoryID = inItemSite.InventoryID;
      itemStats1.SiteID = inItemSite.SiteID;
      ItemStats itemStats2 = ((PXSelectBase<ItemStats>) this.itemstats).Insert(itemStats1);
      itemStats2.LastCost = row.LastCost;
      itemStats2.LastCostDate = new DateTime?(lastCostTime);
    }
    foreach (PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost in ((PXSelectBase) this.itemcost).Cache.Inserted)
      ((PXSelectBase) this.itemstats).Cache.Delete((object) itemCost);
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost();
    itemCost1.InventoryID = row.InventoryID;
    itemCost1.CuryID = row.CuryID;
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost>) this.itemcost).Insert(itemCost1);
    itemCost2.LastCost = row.LastCost;
    itemCost2.LastCostDate = new DateTime?(lastCostTime);
  }

  protected virtual void _(PX.Data.Events.RowInserted<INSubItemRep> e)
  {
    this.UpdateSubItemSiteReplenishment(e.Row, (PXDBOperation) 2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INSubItemRep> e)
  {
    this.UpdateSubItemSiteReplenishment(e.Row, (PXDBOperation) 1);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INSubItemRep> e)
  {
    this.UpdateSubItemSiteReplenishment(e.Row, (PXDBOperation) 3);
  }

  private void UpdateSubItemSiteReplenishment(INSubItemRep row, PXDBOperation operation)
  {
    if (row == null || !row.InventoryID.HasValue || !row.SubItemID.HasValue)
      return;
    foreach (PXResult<INItemSite> pxResult in PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INItemSite.replenishmentClassID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INItemSite.subItemOverride, IBqlBool>.IsEqual<False>>>.Order<PX.Data.BQL.Fluent.By<BqlField<INItemSite.inventoryID, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.InventoryID,
      (object) row.ReplenishmentClassID
    }))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      PXCache cach = ((PXGraph) this).Caches[typeof (INItemSiteReplenishment)];
      INItemSiteReplenishment siteReplenishment1 = PXResultset<INItemSiteReplenishment>.op_Implicit(PXSelectBase<INItemSiteReplenishment, PXViewOf<INItemSiteReplenishment>.BasedOn<SelectFromBase<INItemSiteReplenishment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteReplenishment.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INItemSiteReplenishment.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INItemSiteReplenishment.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) row.InventoryID,
        (object) inItemSite.SiteID,
        (object) row.SubItemID
      }));
      INItemSiteReplenishment siteReplenishment2;
      if (siteReplenishment1 == null)
      {
        if (operation != 3)
        {
          operation = (PXDBOperation) 2;
          siteReplenishment2 = new INItemSiteReplenishment()
          {
            InventoryID = row.InventoryID,
            SiteID = inItemSite.SiteID,
            SubItemID = row.SubItemID
          };
        }
        else
          continue;
      }
      else
        siteReplenishment2 = PXCache<INItemSiteReplenishment>.CreateCopy(siteReplenishment1);
      siteReplenishment2.SafetyStock = row.SafetyStock;
      siteReplenishment2.MinQty = row.MinQty;
      siteReplenishment2.MaxQty = row.MaxQty;
      siteReplenishment2.TransferERQ = row.TransferERQ;
      siteReplenishment2.ItemStatus = row.ItemStatus;
      switch (operation - 1)
      {
        case 0:
          cach.Update((object) siteReplenishment2);
          continue;
        case 1:
          cach.Insert((object) siteReplenishment2);
          continue;
        case 2:
          cach.Delete((object) siteReplenishment2);
          continue;
        default:
          continue;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ItemStats> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 2)
      return;
    int? inventoryId = e.Row.InventoryID;
    int num = 0;
    if (!(inventoryId.GetValueOrDefault() < num & inventoryId.HasValue) || ((PXSelectBase<InventoryItem>) this.Item).Current == null)
      return;
    int? key = (int?) ((PXSelectBase) this.Item).Cache.GetValue<InventoryItem.inventoryID>((object) ((PXSelectBase<InventoryItem>) this.Item).Current);
    if (!this._persisted.ContainsKey(key))
      this._persisted.Add(key, e.Row.InventoryID);
    e.Row.InventoryID = key;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ItemStats>>) e).Cache.Normalize();
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ItemStats> e)
  {
    int? nullable;
    if (e.TranStatus != 2 || PXDBOperationExt.Command(e.Operation) != 2 || !this._persisted.TryGetValue(e.Row.InventoryID, out nullable))
      return;
    e.Row.InventoryID = nullable;
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.AP.Vendor, PX.Objects.AP.Vendor.curyID> e)
  {
    if (((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.AP.Vendor, PX.Objects.AP.Vendor.curyID>>) e).ReturnValue != null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.AP.Vendor, PX.Objects.AP.Vendor.curyID>>) e).ReturnValue = (object) ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<INItemXRef, INItemXRef.bAccountID> e)
  {
    if (e.Row == null || e.Row.BAccountID.HasValue || !(((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INItemXRef, INItemXRef.bAccountID>, INItemXRef, object>) e).NewValue is 0) && (!(((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INItemXRef, INItemXRef.bAccountID>, INItemXRef, object>) e).NewValue is string newValue) || !(newValue == "0")))
      return;
    e.Row.BAccountID = new int?(0);
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<INItemXRef, INItemXRef.bAccountID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemSite> e)
  {
    if (e.Row.IsDefault.GetValueOrDefault())
      this.SetSiteDefault(e.Row);
    if (e.Row == null || ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault() || !e.Row.InventoryID.HasValue || !e.Row.SiteID.HasValue)
      return;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter.InventoryID = e.Row.InventoryID;
    statusByCostCenter.SiteID = e.Row.SiteID;
    statusByCostCenter.CostCenterID = new int?(0);
    statusByCostCenter.PersistEvenZero = new bool?(true);
    ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>) this.sitestatusbycostcenter).Insert(statusByCostCenter);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INItemSite> e)
  {
    bool? isDefault1 = e.OldRow.IsDefault;
    bool? isDefault2 = e.Row.IsDefault;
    if (!(isDefault1.GetValueOrDefault() == isDefault2.GetValueOrDefault() & isDefault1.HasValue == isDefault2.HasValue))
      this.SetSiteDefault(e.Row);
    if (e.Row == null || ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault())
      return;
    int? nullable = e.Row.InventoryID;
    if (!nullable.HasValue)
      return;
    nullable = e.Row.SiteID;
    if (!nullable.HasValue)
      return;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter.InventoryID = e.Row.InventoryID;
    statusByCostCenter.SiteID = e.Row.SiteID;
    statusByCostCenter.CostCenterID = new int?(0);
    statusByCostCenter.PersistEvenZero = new bool?(true);
    ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>) this.sitestatusbycostcenter).Insert(statusByCostCenter);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemSite> e)
  {
    int? nullable;
    if (e.Row != null)
    {
      if ((e.Row == null ? 0 : (INReplenishmentSource.IsTransfer(e.Row.ReplenishmentSource) ? 1 : 0)) != 0)
      {
        int? replenishmentSourceSiteId = e.Row.ReplenishmentSourceSiteID;
        nullable = e.Row.SiteID;
        if (replenishmentSourceSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & replenishmentSourceSiteId.HasValue == nullable.HasValue)
        {
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemSite>>) e).Cache.RaiseExceptionHandling<INItemSite.replenishmentSourceSiteID>((object) e.Row, (object) e.Row.ReplenishmentSourceSiteID, (Exception) new PXSetPropertyException("Replenishment Source Warehouse must be different from current Warehouse", (PXErrorLevel) 2));
          goto label_5;
        }
      }
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemSite>>) e).Cache.RaiseExceptionHandling<INItemSite.replenishmentSourceSiteID>((object) e.Row, (object) e.Row.ReplenishmentSourceSiteID, (Exception) null);
    }
label_5:
    if (e.Row == null)
      return;
    nullable = e.Row.InvtAcctID;
    if (nullable.HasValue)
      return;
    INSite site = INSite.PK.Find((PXGraph) this, e.Row.SiteID);
    try
    {
      INItemSiteMaint.DefaultInvtAcctSub((PXGraph) this, e.Row, ((PXSelectBase<InventoryItem>) this.Item).Current, site, ((PXSelectBase<INPostClass>) this.postclass).Current);
    }
    catch (PXMaskArgumentException ex)
    {
    }
  }

  protected virtual void _(PX.Data.Events.CommandPreparing<INItemSite.invtAcctID> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || ((INItemSite) e.Row).OverrideInvtAcctSub.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXCommandPreparingEventArgs, PX.Data.Events.CommandPreparing<INItemSite.invtAcctID>>) e).Args.ExcludeFromInsertUpdate();
  }

  protected virtual void _(PX.Data.Events.CommandPreparing<INItemSite.invtSubID> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || ((INItemSite) e.Row).OverrideInvtAcctSub.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXCommandPreparingEventArgs, PX.Data.Events.CommandPreparing<INItemSite.invtSubID>>) e).Args.ExcludeFromInsertUpdate();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemSite, INItemSite.inventoryID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemSite, INItemSite.inventoryID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<RelationGroup> e)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current == null || e.Row == null || ((PXSelectBase) this.Groups).Cache.GetStatus((object) e.Row) != null)
      return;
    e.Row.Included = new bool?(UserAccess.IsIncluded(((PXSelectBase<InventoryItem>) this.Item).Current.GroupMask, e.Row));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<RelationGroup> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.RowSelected<INItemRep> e)
  {
    if (e.Row != null)
    {
      bool flag = INReplenishmentSource.IsTransfer(e.Row.ReplenishmentSource);
      PXUIFieldAttribute.SetEnabled<INItemRep.replenishmentMethod>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, EnumerableExtensions.IsNotIn<string>(e.Row.ReplenishmentSource, "O", "D"));
      PXUIFieldAttribute.SetEnabled<INItemRep.replenishmentSourceSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, EnumerableExtensions.IsIn<string>(e.Row.ReplenishmentSource, "O", "D", "T", "P"));
      PXUIFieldAttribute.SetEnabled<INItemRep.maxShelfLife>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.launchDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.terminationDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.serviceLevelPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.safetyStock>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.minQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.maxQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.forecastModelType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.forecastPeriodType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.historyDepth>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
      PXUIFieldAttribute.SetEnabled<INItemRep.transferERQ>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemRep>>) e).Cache, (object) e.Row, flag && e.Row.ReplenishmentMethod == "F");
      PXUIFieldAttribute.SetEnabled<INSubItemRep.transferERQ>(((PXSelectBase) this.subreplenishment).Cache, (object) null, flag && e.Row.ReplenishmentMethod == "F");
    }
    ((PXSelectBase) this.subreplenishment).Cache.AllowInsert = e.Row != null && !string.IsNullOrEmpty(e.Row.ReplenishmentClassID) && ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault();
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemRep> e)
  {
    if (e.Row == null || e.Row.ReplenishmentClassID == null)
      return;
    this.UpdateItemSiteReplenishment(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentSource> e)
  {
    if (e.Row == null)
      return;
    if (EnumerableExtensions.IsIn<string>(e.Row.ReplenishmentSource, "O", "D"))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentSource>>) e).Cache.SetValueExt<INItemRep.replenishmentMethod>((object) e.Row, (object) "N");
    if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || !EnumerableExtensions.IsNotIn<string>(e.Row.ReplenishmentSource, "O", "D", "T"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentSource>>) e).Cache.SetDefaultExt<INItemRep.replenishmentSourceSiteID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod> e)
  {
    if (e.Row == null || !(e.Row.ReplenishmentMethod == "N"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.maxShelfLife>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.launchDate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.terminationDate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.serviceLevelPct>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.safetyStock>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.minQty>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.maxQty>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.forecastModelType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.forecastPeriodType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemRep, INItemRep.replenishmentMethod>>) e).Cache.SetDefaultExt<INItemRep.historyDepth>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INItemRep> e)
  {
    if (e.Row == null)
      return;
    if (!INReplenishmentSource.IsTransfer(e.Row.ReplenishmentSource))
      e.Row.ReplenishmentSourceSiteID = new int?();
    this.UpdateItemSiteReplenishment(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INItemRep> e)
  {
    if (e.Row == null)
      return;
    this.UpdateItemSiteReplenishment(new INItemRep()
    {
      ReplenishmentClassID = e.Row.ReplenishmentClassID,
      CuryID = e.Row.CuryID
    });
  }

  private void UpdateItemSiteReplenishment(INItemRep rep)
  {
    FbqlSelect<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<KeysRelation<Field<INItemSite.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INItemSite>, INSite, INItemSite>.And<CurrentMatch<INSite, AccessInfo.userName>>>>, FbqlJoins.Left<INSiteStatusSummary>.On<INSiteStatusSummary.FK.ItemSite>>>.Where<KeysRelation<Field<INItemSite.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemSite>, InventoryItem, INItemSite>.SameAsCurrent.And<BqlOperand<INSite.baseCuryID, IBqlString>.IsEqual<BqlField<AccessInfo.baseCuryID, IBqlString>.AsOptional>>>, INItemSite>.View itemsiterecords = this.itemsiterecords;
    object[] objArray = new object[1]{ (object) rep.CuryID };
    foreach (INItemSite itemSite in ((PXSelectBase<INItemSite>) itemsiterecords).SelectMain(objArray))
    {
      bool flag = false;
      if (itemSite.ReplenishmentClassID == null)
      {
        itemSite.ReplenishmentClassID = rep.ReplenishmentClassID;
        flag = true;
      }
      if (!(itemSite.ReplenishmentClassID != rep.ReplenishmentClassID) && this.UpdateItemSiteReplenishment(itemSite, rep) | flag)
        GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) itemSite, true);
    }
  }

  protected virtual bool UpdateItemSiteReplenishment(INItemSite itemSite, INItemRep rep)
  {
    return INItemSiteMaint.UpdateItemSiteReplenishment(itemSite, rep);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemBoxEx> e)
  {
    if (e.Row == null || ((PXSelectBase<InventoryItem>) this.Item).Current == null || !EnumerableExtensions.IsIn<string>(((PXSelectBase<InventoryItem>) this.Item).Current.PackageOption, "W", "V"))
      return;
    e.Row.MaxQty = this.CalculateMaxQtyInBox(((PXSelectBase<InventoryItem>) this.Item).Current, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemBoxEx> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CS.CSBox csBox = PX.Objects.CS.CSBox.PK.Find((PXGraph) this, e.Row.BoxID);
    if (csBox != null)
    {
      e.Row.MaxWeight = csBox.MaxWeight;
      e.Row.MaxVolume = csBox.MaxVolume;
      e.Row.BoxWeight = csBox.BoxWeight;
      e.Row.Description = csBox.Description;
    }
    if (!EnumerableExtensions.IsIn<string>(((PXSelectBase<InventoryItem>) this.Item).Current.PackageOption, "W", "V"))
      return;
    e.Row.MaxQty = this.CalculateMaxQtyInBox(((PXSelectBase<InventoryItem>) this.Item).Current, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemBoxEx, INItemBoxEx.uOM> e)
  {
    if (e.Row != null && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemBoxEx, INItemBoxEx.uOM>, INItemBoxEx, object>) e).NewValue != null)
    {
      InventoryItem current = ((PXSelectBase<InventoryItem>) this.Item).Current;
      if (current != null && INUnit.UK.ByInventory.FindDirty(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INItemBoxEx, INItemBoxEx.uOM>>) e).Cache.Graph, current.InventoryID, current.BaseUnit) == null)
        throw new PXSetPropertyException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
        {
          (object) "uOM",
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemBoxEx, INItemBoxEx.uOM>, INItemBoxEx, object>) e).NewValue
        });
    }
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemBoxEx, INItemBoxEx.uOM>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POVendorInventory, POVendorInventory.purchaseUnit> e)
  {
    if (e.Row == null)
      return;
    foreach (INUnit inUnit in ((PXGraph) this).Caches[typeof (INUnit)].Inserted)
    {
      short? unitType = inUnit.UnitType;
      int? nullable = unitType.HasValue ? new int?((int) unitType.GetValueOrDefault()) : new int?();
      int num = 1;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        nullable = inUnit.InventoryID;
        int? inventoryId = e.Row.InventoryID;
        if (nullable.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable.HasValue == inventoryId.HasValue && string.Equals(inUnit.FromUnit, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POVendorInventory, POVendorInventory.purchaseUnit>, POVendorInventory, object>) e).NewValue, StringComparison.InvariantCultureIgnoreCase))
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POVendorInventory, POVendorInventory.purchaseUnit>>) e).Cancel = true;
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<InventoryItemCurySettings> eventArgs)
  {
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItemCurySettings>>) eventArgs).Cache, (object) null).For<InventoryItemCurySettings.pendingStdCost>((Action<PXUIFieldAttribute>) (a => a.Enabled = ((PXSelectBase<InventoryItem>) this.Item).Current?.ValMethod == "T")).SameFor<InventoryItemCurySettings.pendingStdCostDate>();
  }

  protected virtual void AppendGroupMask(int? itemClassID, bool clear)
  {
    if (itemClassID.GetValueOrDefault() == 0)
      return;
    INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXViewOf<INItemClass>.BasedOn<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) itemClassID
    }));
    if (inItemClass == null || inItemClass.GroupMask == null)
      return;
    if (clear)
      ((PXSelectBase) this.Groups).Cache.Clear();
    using (IEnumerator<PXResult<RelationGroup>> enumerator = ((PXSelectBase<RelationGroup>) this.Groups).Select(Array.Empty<object>()).GetEnumerator())
    {
label_11:
      while (enumerator.MoveNext())
      {
        RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(enumerator.Current);
        int index = 0;
        while (true)
        {
          if (index < relationGroup.GroupMask.Length && index < inItemClass.GroupMask.Length)
          {
            if (relationGroup.Included.GetValueOrDefault() || relationGroup.GroupMask[index] == (byte) 0 || ((int) inItemClass.GroupMask[index] & (int) relationGroup.GroupMask[index]) != (int) relationGroup.GroupMask[index])
              ++index;
            else
              break;
          }
          else
            goto label_11;
        }
        relationGroup.Included = new bool?(true);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Groups).Cache, (object) relationGroup, true);
        ((PXSelectBase) this.Groups).Cache.IsDirty = true;
      }
    }
  }

  public virtual bool IsDirty
  {
    get => (!this._JustInserted || ((PXGraph) this).IsContractBasedAPI) && ((PXGraph) this).IsDirty;
  }

  protected virtual void SetSiteDefault(INItemSite itemsite)
  {
    InventoryItem dirty = InventoryItem.PK.FindDirty((PXGraph) this, itemsite.InventoryID);
    INSite inSite = INSite.PK.Find((PXGraph) this, itemsite.SiteID);
    bool? isDefault;
    if (dirty != null)
    {
      InventoryItemCurySettings curySettings = this.GetCurySettings(dirty.InventoryID, inSite.BaseCuryID);
      curySettings.DfltSiteID = itemsite.IsDefault.GetValueOrDefault() ? itemsite.SiteID : new int?();
      InventoryItemCurySettings itemCurySettings1 = curySettings;
      isDefault = itemsite.IsDefault;
      int? nullable1 = isDefault.GetValueOrDefault() ? itemsite.DfltReceiptLocationID : new int?();
      itemCurySettings1.DfltReceiptLocationID = nullable1;
      InventoryItemCurySettings itemCurySettings2 = curySettings;
      isDefault = itemsite.IsDefault;
      int? nullable2 = isDefault.GetValueOrDefault() ? itemsite.DfltShipLocationID : new int?();
      itemCurySettings2.DfltShipLocationID = nullable2;
      ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(curySettings);
    }
    bool flag = false;
    foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) this.itemsiterecords).Select(new object[1]
    {
      (object) inSite.BaseCuryID
    }))
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      if (!object.Equals((object) inItemSite.SiteID, (object) itemsite.SiteID))
      {
        isDefault = inItemSite.IsDefault;
        if (isDefault.Value)
        {
          inItemSite.IsDefault = new bool?(false);
          GraphHelper.MarkUpdated(((PXSelectBase) this.itemsiterecords).Cache, (object) inItemSite, true);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.itemsiterecords).View.RequestRefresh();
  }

  public override void Persist()
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      if (string.IsNullOrEmpty(((PXSelectBase<InventoryItem>) this.Item).Current.LotSerClassID) && !PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
        ((PXSelectBase<InventoryItem>) this.Item).Current.LotSerClassID = INLotSerClass.GetDefaultLotSerClass((PXGraph) this);
      if (((PXSelectBase) this.Groups).Cache.IsDirty)
      {
        UserAccess.PopulateNeighbours<InventoryItem>(((PXSelectBase) this.Item).Cache, ((PXSelectBase<InventoryItem>) this.Item).Current, new PXDataFieldValue[1]
        {
          new PXDataFieldValue(typeof (InventoryItem.inventoryID).Name, (PXDbType) 8, new int?(4), (object) ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID, (PXComp) 1)
        }, (PXSelectBase<RelationGroup>) this.Groups, new System.Type[1]
        {
          typeof (SegmentValue)
        });
        PXSelectorAttribute.ClearGlobalCache<InventoryItem>();
      }
    }
    foreach (INItemSiteReplenishment siteReplenishment in ((PXSelectBase) this.itemsitereplenihments).Cache.Inserted)
    {
      FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.View sitestatusbycostcenter = this.sitestatusbycostcenter;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
      statusByCostCenter.InventoryID = siteReplenishment.InventoryID;
      statusByCostCenter.SubItemID = siteReplenishment.SubItemID;
      statusByCostCenter.SiteID = siteReplenishment.SiteID;
      statusByCostCenter.CostCenterID = new int?(0);
      statusByCostCenter.PersistEvenZero = new bool?(true);
      ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>) sitestatusbycostcenter).Insert(statusByCostCenter);
    }
    base.Persist();
    ((PXSelectBase) this.Groups).Cache.Clear();
    GroupHelper.Clear();
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
    if (this.DisableCopyPastingSubitems())
    {
      foreach (int index in EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (_ => this.IsMatchingPatternWithTrailingNumber(_.ObjectName, "SubItem_"))).Reverse<int>())
      {
        script.RemoveAt(index);
        containers.RemoveAt(index);
      }
    }
    EnumerableExtensions.ForEach<Command>(script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName == "itemxrefrecords")), (Action<Command>) (_ => _.Commit = false));
    script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName == "itemxrefrecords")).Last<Command>().Commit = true;
    foreach (SubItemAttribute subItemAttribute in ((PXSelectBase) this.ItemSettings).Cache.GetAttributesReadonly<InventoryItem.defaultSubItemID>().Concat<PXEventSubscriberAttribute>((IEnumerable<PXEventSubscriberAttribute>) ((PXSelectBase) this.VendorItems).Cache.GetAttributesReadonly<POVendorInventory.subItemID>()).Concat<PXEventSubscriberAttribute>((IEnumerable<PXEventSubscriberAttribute>) ((PXSelectBase) this.itemxrefrecords).Cache.GetAttributesReadonly<INItemXRef.subItemID>()).OfType<SubItemAttribute>())
      subItemAttribute.ValidateValueOnFieldUpdating = false;
  }

  protected virtual bool DisableCopyPastingSubitems()
  {
    int? segmentsNumber = this.SegmentValues.SegmentsNumber;
    int num = 1;
    return segmentsNumber.GetValueOrDefault() > num & segmentsNumber.HasValue;
  }

  protected virtual bool IsMatchingPatternWithTrailingNumber(string input, string pattern)
  {
    int? length1 = input?.Length;
    int length2 = pattern.Length;
    return length1.GetValueOrDefault() > length2 & length1.HasValue && Regex.IsMatch(input, $"^{pattern}[0-9]+$");
  }

  protected virtual void ValidatePackaging(InventoryItem row)
  {
    PXUIFieldAttribute.SetError<InventoryItem.weightUOM>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<InventoryItem.baseItemWeight>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<InventoryItem.volumeUOM>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<InventoryItem.baseItemVolume>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    Decimal? nullable;
    if (EnumerableExtensions.IsIn<string>(row.PackageOption, "W", "V"))
    {
      if (string.IsNullOrEmpty(row.WeightUOM))
        ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<InventoryItem.weightUOM>((object) row, (object) row.WeightUOM, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      nullable = row.BaseItemWeight;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() <= num1 & nullable.HasValue)
        ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<InventoryItem.baseItemWeight>((object) row, (object) row.BaseItemWeight, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      if (row.PackageOption == "V")
      {
        if (string.IsNullOrEmpty(row.VolumeUOM))
          ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<InventoryItem.volumeUOM>((object) row, (object) row.VolumeUOM, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
        nullable = row.BaseItemVolume;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
          ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<InventoryItem.baseItemVolume>((object) row, (object) row.BaseItemVolume, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      }
    }
    foreach (PXResult<INItemBoxEx> pxResult in ((PXSelectBase<INItemBoxEx>) this.Boxes).Select(Array.Empty<object>()))
    {
      INItemBoxEx inItemBoxEx = PXResult<INItemBoxEx>.op_Implicit(pxResult);
      PXUIFieldAttribute.SetError<INItemBoxEx.boxID>(((PXSelectBase) this.Boxes).Cache, (object) inItemBoxEx, (string) null);
      PXUIFieldAttribute.SetError<INItemBoxEx.maxQty>(((PXSelectBase) this.Boxes).Cache, (object) inItemBoxEx, (string) null);
      if (EnumerableExtensions.IsIn<string>(row.PackageOption, "W", "V"))
      {
        nullable = inItemBoxEx.MaxWeight;
        if (nullable.GetValueOrDefault() == 0M)
          ((PXSelectBase) this.Boxes).Cache.RaiseExceptionHandling<INItemBoxEx.boxID>((object) inItemBoxEx, (object) inItemBoxEx.BoxID, (Exception) new PXSetPropertyException("Box Max. Weight must be defined for Auto Packaging to work correctly.", (PXErrorLevel) 2));
      }
      if (row.PackageOption == "V")
      {
        nullable = inItemBoxEx.MaxVolume;
        if (nullable.GetValueOrDefault() == 0M)
          ((PXSelectBase) this.Boxes).Cache.RaiseExceptionHandling<INItemBoxEx.boxID>((object) inItemBoxEx, (object) inItemBoxEx.BoxID, (Exception) new PXSetPropertyException("Box Max. Volume must be defined for Auto Packaging to work correctly.", (PXErrorLevel) 2));
      }
      if (EnumerableExtensions.IsIn<string>(row.PackageOption, "W", "V"))
      {
        nullable = inItemBoxEx.MaxWeight;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = row.BaseItemWeight;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        if (!(valueOrDefault1 < valueOrDefault2))
        {
          nullable = inItemBoxEx.MaxVolume;
          Decimal num = 0M;
          if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          {
            nullable = row.BaseItemVolume;
            Decimal? maxVolume = inItemBoxEx.MaxVolume;
            if (!(nullable.GetValueOrDefault() > maxVolume.GetValueOrDefault() & nullable.HasValue & maxVolume.HasValue))
              continue;
          }
          else
            continue;
        }
        ((PXSelectBase) this.Boxes).Cache.RaiseExceptionHandling<INItemBoxEx.boxID>((object) inItemBoxEx, (object) inItemBoxEx.BoxID, (Exception) new PXSetPropertyException("The item can't fit the given Box.", (PXErrorLevel) 2));
      }
    }
  }

  private IEnumerable<RelationGroup> GetGroups()
  {
    InventoryItemMaint inventoryItemMaint = this;
    if (((PXGraph) inventoryItemMaint).IsImport)
      ((PXSelectBase) inventoryItemMaint.Groups).View.Clear();
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXViewOf<RelationGroup>.BasedOn<SelectFromBase<RelationGroup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) inventoryItemMaint, Array.Empty<object>()))
    {
      RelationGroup group = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (EnumerableExtensions.IsIn<string>(group.SpecificModule, (string) null, typeof (InventoryItem).Namespace) && EnumerableExtensions.IsIn<string>(group.SpecificType, (string) null, typeof (SegmentValue).FullName, typeof (InventoryItem).FullName) || ((PXSelectBase<InventoryItem>) inventoryItemMaint.Item).Current != null && UserAccess.IsIncluded(((PXSelectBase<InventoryItem>) inventoryItemMaint.Item).Current.GroupMask, group))
      {
        ((PXSelectBase<RelationGroup>) inventoryItemMaint.Groups).Current = group;
        yield return group;
      }
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewSummary(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      InventorySummaryEnq instance = PXGraph.CreateInstance<InventorySummaryEnq>();
      ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Inventory Summary");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewAllocationDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      InventoryAllocDetEnq instance = PXGraph.CreateInstance<InventoryAllocDetEnq>();
      ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Inventory Allocation Details");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewTransactionSummary(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      InventoryTranSumEnq instance = PXGraph.CreateInstance<InventoryTranSumEnq>();
      ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Inventory Transaction Summary");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewTransactionDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      InventoryTranDetEnq instance = PXGraph.CreateInstance<InventoryTranDetEnq>();
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Inventory Transaction Details");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewTransactionHistory(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      InventoryTranHistEnq instance = PXGraph.CreateInstance<InventoryTranHistEnq>();
      ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Inventory Transaction History");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXInsertButton]
  protected virtual IEnumerable AddWarehouseDetail(PXAdapter adapter)
  {
    foreach (InventoryItem inventoryItem in adapter.Get())
    {
      int? inventoryId = inventoryItem.InventoryID;
      int num = 0;
      if (inventoryId.GetValueOrDefault() > num & inventoryId.HasValue)
      {
        INItemSiteMaint instance = PXGraph.CreateInstance<INItemSiteMaint>();
        PXCache cache = ((PXSelectBase) instance.itemsiterecord).Cache;
        INItemSite copy = (INItemSite) cache.CreateCopy(cache.Insert());
        copy.InventoryID = inventoryItem.InventoryID;
        cache.Update((object) copy);
        cache.IsDirty = false;
        throw new PXRedirectRequiredException((PXGraph) instance, "Add Warehouse Detail");
      }
      yield return (object) inventoryItem;
    }
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField]
  protected virtual IEnumerable UpdateReplenishment(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemRep>) this.replenishment).Current != null && ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault())
    {
      foreach (PXResult<INSubItemRep> pxResult in ((PXSelectBase<INSubItemRep>) this.subreplenishment).Select(Array.Empty<object>()))
      {
        INSubItemRep copy = PXCache<INSubItemRep>.CreateCopy(PXResult<INSubItemRep>.op_Implicit(pxResult));
        copy.SafetyStock = ((PXSelectBase<INItemRep>) this.replenishment).Current.SafetyStock;
        copy.MinQty = ((PXSelectBase<INItemRep>) this.replenishment).Current.MinQty;
        copy.MaxQty = ((PXSelectBase<INItemRep>) this.replenishment).Current.MaxQty;
        ((PXSelectBase<INSubItemRep>) this.subreplenishment).Update(copy);
      }
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "AddNew")]
  [PXUIField]
  protected virtual IEnumerable GenerateSubitems(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemRep>) this.replenishment).Current != null && ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault())
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      List<Segment> list1 = ((IQueryable<PXResult<Segment>>) PXSelectBase<Segment, PXViewOf<Segment>.BasedOn<SelectFromBase<Segment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Segment.dimensionID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) "INSUBITEM"
      })).Select<PXResult<Segment>, Segment>(Expression.Lambda<Func<PXResult<Segment>, Segment>>((Expression) Expression.Call(res, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).ToList<Segment>();
      Dictionary<short?, List<string>> dictionary = list1.ToDictionary<Segment, short?, List<string>>((Func<Segment, short?>) (segment => segment.SegmentID), (Func<Segment, List<string>>) (segement => new List<string>()));
      foreach (PXResult<INSubItemSegmentValue> pxResult in PXSelectBase<INSubItemSegmentValue, PXViewOf<INSubItemSegmentValue>.BasedOn<SelectFromBase<INSubItemSegmentValue, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SegmentValue>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SegmentValue.segmentID, Equal<INSubItemSegmentValue.segmentID>>>>, And<BqlOperand<SegmentValue.value, IBqlString>.IsEqual<INSubItemSegmentValue.value>>>>.And<BqlOperand<SegmentValue.dimensionID, IBqlString>.IsEqual<SubItemAttribute.dimensionName>>>>>.Where<KeysRelation<Field<INSubItemSegmentValue.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INSubItemSegmentValue>, InventoryItem, INSubItemSegmentValue>.SameAsCurrent>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        INSubItemSegmentValue itemSegmentValue = PXResult<INSubItemSegmentValue>.op_Implicit(pxResult);
        dictionary[itemSegmentValue.SegmentID].Add(itemSegmentValue.Value);
      }
      foreach (Segment segment in list1)
      {
        if (!dictionary[segment.SegmentID].Any<string>())
        {
          if (segment.Validate.GetValueOrDefault())
            throw new PXException("At least one value in each segment that requires validation should be selected on the SUBITEMS tab.");
          dictionary[segment.SegmentID].Add(new string(' ', (int) (segment.Length ?? (short) 1)));
        }
      }
      List<string> list2 = dictionary.First<KeyValuePair<short?, List<string>>>().Value;
      foreach (List<string> inner in dictionary.Skip<KeyValuePair<short?, List<string>>>(1).Select<KeyValuePair<short?, List<string>>, List<string>>((Func<KeyValuePair<short?, List<string>>, List<string>>) (kvp => kvp.Value)))
        list2 = list2.Join<string, string, int, string>((IEnumerable<string>) inner, (Func<string, int>) (s => 0), (Func<string, int>) (s => 0), (Func<string, string, string>) ((subItemId, segment) => subItemId + segment)).ToList<string>();
      foreach (string source in list2)
      {
        if (!source.All<char>(new Func<char, bool>(char.IsWhiteSpace)))
        {
          INSubItemRep inSubItemRep = new INSubItemRep();
          inSubItemRep.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
          inSubItemRep.ReplenishmentClassID = ((PXSelectBase<INItemRep>) this.replenishment).Current.ReplenishmentClassID;
          ((PXSelectBase<INSubItemRep>) this.subreplenishment).SetValueExt<INSubItemRep.subItemID>(inSubItemRep, (object) source);
          ((PXSelectBase<INSubItemRep>) this.subreplenishment).Insert(inSubItemRep);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ViewGroupDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<RelationGroup>) this.Groups).Current != null)
    {
      RelationGroups instance = PXGraph.CreateInstance<RelationGroups>();
      ((PXSelectBase<RelationHeader>) instance.HeaderGroup).Current = PXResultset<RelationHeader>.op_Implicit(((PXSelectBase<RelationHeader>) instance.HeaderGroup).Search<RelationGroup.groupName>((object) ((PXSelectBase<RelationGroup>) this.Groups).Current.GroupName, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Group Details");
    }
    return adapter.Get();
  }

  public static void Redirect(int? inventoryID) => InventoryItemMaint.Redirect(inventoryID, false);

  public static void Redirect(int? inventoryID, bool newWindow)
  {
    InventoryItemMaint instance = PXGraph.CreateInstance<InventoryItemMaint>();
    ((PXSelectBase<InventoryItem>) instance.Item).Current = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) instance.Item).Search<InventoryItem.inventoryID>((object) inventoryID, Array.Empty<object>()));
    if (((PXSelectBase<InventoryItem>) instance.Item).Current == null)
      return;
    if (newWindow)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Inventory Item");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    throw new PXRedirectRequiredException((PXGraph) instance, "Inventory Item");
  }

  protected virtual Decimal? CalculateMaxQtyInBox(InventoryItem item, INItemBoxEx box)
  {
    Decimal? maxQtyInBox1 = new Decimal?();
    Decimal? maxQtyInBox2 = new Decimal?();
    Decimal? baseWeight = item.BaseWeight;
    Decimal num1 = 0M;
    Decimal? maxQtyInBox3;
    if (baseWeight.GetValueOrDefault() > num1 & baseWeight.HasValue)
    {
      maxQtyInBox3 = box.MaxWeight;
      Decimal num2 = 0M;
      if (maxQtyInBox3.GetValueOrDefault() > num2 & maxQtyInBox3.HasValue)
      {
        ref Decimal? local = ref maxQtyInBox1;
        maxQtyInBox3 = box.MaxWeight;
        Decimal num3 = maxQtyInBox3.Value;
        maxQtyInBox3 = box.BoxWeight;
        Decimal valueOrDefault = maxQtyInBox3.GetValueOrDefault();
        Decimal num4 = num3 - valueOrDefault;
        maxQtyInBox3 = item.BaseWeight;
        Decimal num5 = maxQtyInBox3.Value;
        Decimal num6 = Math.Floor(num4 / num5);
        local = new Decimal?(num6);
      }
    }
    if (item.PackageOption == "W")
      return maxQtyInBox1;
    maxQtyInBox3 = item.BaseVolume;
    Decimal num7 = 0M;
    if (maxQtyInBox3.GetValueOrDefault() > num7 & maxQtyInBox3.HasValue)
    {
      maxQtyInBox3 = box.MaxVolume;
      Decimal num8 = 0M;
      if (maxQtyInBox3.GetValueOrDefault() > num8 & maxQtyInBox3.HasValue)
      {
        ref Decimal? local = ref maxQtyInBox2;
        maxQtyInBox3 = box.MaxVolume;
        Decimal num9 = maxQtyInBox3.Value;
        maxQtyInBox3 = item.BaseVolume;
        Decimal num10 = maxQtyInBox3.Value;
        Decimal num11 = Math.Floor(num9 / num10);
        local = new Decimal?(num11);
      }
    }
    if (maxQtyInBox1.HasValue && maxQtyInBox2.HasValue)
      return new Decimal?(Math.Min(maxQtyInBox1.Value, maxQtyInBox2.Value));
    if (maxQtyInBox1.HasValue)
      return maxQtyInBox1;
    if (maxQtyInBox2.HasValue)
      return maxQtyInBox2;
    maxQtyInBox3 = new Decimal?();
    return maxQtyInBox3;
  }

  public class CurySettings : 
    CurySettingsExtension<InventoryItemMaint, InventoryItem, InventoryItemCurySettings>
  {
    public static bool IsActive() => true;

    [PXButton(CommitChanges = true)]
    [PXUIField]
    protected virtual IEnumerable UpdateCost(PXAdapter adapter)
    {
      InventoryItemCurySettings itemCurySettings1 = (InventoryItemCurySettings) this.curySettings.SelectSingle(Array.Empty<object>());
      if (itemCurySettings1 != null && itemCurySettings1.PendingStdCostDate.HasValue && ((PXSelectBase<InventoryItem>) this.Base.ItemSettings).Current?.ValMethod == "T")
      {
        if (PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INCostStatus.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INCostStatus>, InventoryItem, INCostStatus>.SameAsCurrent.And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) != null)
          throw new PXException("There is non zero Quantity on Hand for this item. You can only change Cost when the Qty on Hand is equal to zero");
        Decimal valueOrDefault = itemCurySettings1.PendingStdCost.GetValueOrDefault();
        DateTime? nullable1 = itemCurySettings1.PendingStdCostDate;
        DateTime dateTime = nullable1 ?? ((PXGraph) this.Base).Accessinfo.BusinessDate.Value;
        itemCurySettings1.LastStdCost = itemCurySettings1.StdCost;
        itemCurySettings1.StdCost = new Decimal?(valueOrDefault);
        itemCurySettings1.StdCostDate = new DateTime?(dateTime);
        itemCurySettings1.PendingStdCost = new Decimal?(0M);
        InventoryItemCurySettings itemCurySettings2 = itemCurySettings1;
        nullable1 = new DateTime?();
        DateTime? nullable2 = nullable1;
        itemCurySettings2.PendingStdCostDate = nullable2;
        GraphHelper.MarkUpdated(this.curySettings.Cache, (object) itemCurySettings1, true);
        if (string.Equals(itemCurySettings1.CuryID, CurrencyCollection.GetBaseCurrency()?.CuryID, StringComparison.OrdinalIgnoreCase))
        {
          InventoryItem current = ((PXSelectBase<InventoryItem>) this.Base.ItemSettings).Current;
          current.LastStdCost = itemCurySettings1.LastStdCost;
          current.StdCost = itemCurySettings1.StdCost;
          current.StdCostDate = itemCurySettings1.StdCostDate;
          current.PendingStdCost = itemCurySettings1.PendingStdCost;
          current.PendingStdCostDate = itemCurySettings1.PendingStdCostDate;
          GraphHelper.MarkUpdated(((PXSelectBase) this.Base.ItemSettings).Cache, (object) current, true);
        }
        foreach (PXResult<INItemSite> pxResult in ((PXSelectBase<INItemSite>) this.Base.itemsiterecords).Select(Array.Empty<object>()))
        {
          INItemSite inItemSite1 = PXResult<INItemSite>.op_Implicit(pxResult);
          if (!inItemSite1.StdCostOverride.GetValueOrDefault())
          {
            inItemSite1.LastStdCost = inItemSite1.StdCost;
            inItemSite1.StdCost = new Decimal?(valueOrDefault);
            inItemSite1.StdCostDate = new DateTime?(dateTime);
            inItemSite1.PendingStdCost = new Decimal?(0M);
            INItemSite inItemSite2 = inItemSite1;
            nullable1 = new DateTime?();
            DateTime? nullable3 = nullable1;
            inItemSite2.PendingStdCostDate = nullable3;
            inItemSite1.PendingStdCostReset = new bool?(false);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.itemsiterecords).Cache, (object) inItemSite1, true);
          }
        }
        ((PXAction) this.Base.Save).Press();
      }
      return adapter.Get();
    }
  }

  public class DefaultSiteID : 
    PXFieldAttachedTo<InventoryItem>.By<InventoryItemMaint>.AsInteger.Named<InventoryItemMaint.DefaultSiteID>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.warehouse>();

    public override int? GetValue(InventoryItem Row)
    {
      return Row.With<InventoryItem, InventoryItemCurySettings>((Func<InventoryItem, InventoryItemCurySettings>) (ii => this.Base.GetCurySettings(ii.InventoryID))).With<InventoryItemCurySettings, int?>((Func<InventoryItemCurySettings, int?>) (iici => iici.DfltSiteID));
    }
  }
}
