// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockItemMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.GraphExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions.NonStockItemMaintExt;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN;

public class NonStockItemMaint : InventoryItemMaintBase
{
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>, PX.Objects.GL.Branch>.View CurrentBranch;

  public override bool IsStockItemFlag => false;

  public NonStockItemMaint()
  {
    ((PXSelectBase) this.Item).View = new PXView((PXGraph) this, false, (BqlCommand) new SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.stkItem, Equal<False>>>>, And<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.unknown>>>, And<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<False>>>>.And<MatchUser>>());
    ((PXGraph) this).Views["Item"] = ((PXSelectBase) this.Item).View;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(NonStockItemMaint.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (NonStockItemMaint.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXFieldDefaulting((object) NonStockItemMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__3_0))));
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    NonStockItemMaint.Configure(config.GetScreenConfigurationContext<NonStockItemMaint, InventoryItem>());
  }

  protected static void Configure(
    WorkflowContext<NonStockItemMaint, InventoryItem> context)
  {
    BoundedTo<NonStockItemMaint, InventoryItem>.Condition isKit = context.Conditions.FromBql<BqlOperand<InventoryItem.kitItem, IBqlBool>.IsEqual<True>>().WithSharedName("IsKit");
    BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IConfigured pricesCategory = context.Categories.CreateNew("Prices Category", (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IConfigured>) (category => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IConfigured) category.DisplayName("Prices")));
    BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IConfigured otherCategory = CommonActionCategories.Get<NonStockItemMaint, InventoryItem>(context).Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<NonStockItemMaint, InventoryItem>.ScreenConfiguration.IStartConfigScreen, BoundedTo<NonStockItemMaint, InventoryItem>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<NonStockItemMaint, InventoryItem>.ScreenConfiguration.IConfigured) ((BoundedTo<NonStockItemMaint, InventoryItem>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<NonStockItemMaint, PXAction<InventoryItem>>>) (g => g.viewSalesPrices), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(pricesCategory)));
      actions.Add((Expression<Func<NonStockItemMaint, PXAction<InventoryItem>>>) (g => g.viewVendorPrices), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(pricesCategory)));
      actions.Add((Expression<Func<NonStockItemMaint, PXAction<InventoryItem>>>) (g => g.updateCost), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<NonStockItemMaint, PXAction<InventoryItem>>>) (g => g.ChangeID), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<NonStockItemMaint, PXAction<InventoryItem>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add<ConvertNonStockToStockExt>((Expression<Func<ConvertNonStockToStockExt, PXAction<InventoryItem>>>) (g => g.convert), (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.AddNew("ShowItemSalesPrices", (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Item Sales Prices").IsSidePanelScreen((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<ARSalesPriceMaint>().WithIcon("account_balance").WithAssignments((Action<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<ARSalesPriceFilter.inventoryID>((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowItemVendorPrices", (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Item Vendor Prices").IsSidePanelScreen((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<APVendorPriceMaint>().WithIcon("local_offer").WithAssignments((Action<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add<APVendorPriceFilter.inventoryID>((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()))))))));
      actions.AddNew("ShowKitSpecifications", (Func<BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<NonStockItemMaint, InventoryItem>.ActionDefinition.IConfigured) a.DisplayName("Kit Specifications").IsHiddenWhen((BoundedTo<NonStockItemMaint, InventoryItem>.ISharedCondition) BoundedTo<NonStockItemMaint, InventoryItem>.Condition.op_LogicalNot(isKit)).IsSidePanelScreen((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen<INKitSpecMaint>().WithIcon("description").WithAssignments((Action<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass =>
      {
        ass.Add<INKitSpecHdr.kitInventoryID>((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField<InventoryItem.inventoryID>()));
        ass.Add<INKitSpecHdr.revisionID>((Func<BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.INeedRightOperand, BoundedTo<NonStockItemMaint, InventoryItem>.NavigationParameter.IConfigured>) (e => e.SetFromField("DefaultKitRevisionID")));
      }))))));
    })).WithCategories((Action<BoundedTo<NonStockItemMaint, InventoryItem>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(pricesCategory);
      categories.Add(otherCategory);
    }))));
  }

  [PXDefault]
  [InventoryRaw(typeof (Where<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<False>>), IsKey = true, DisplayName = "Inventory ID", Filterable = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.inventoryCD> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INPostClass.postClassID), DescriptionField = typeof (INPostClass.descr))]
  [PXUIField(DisplayName = "Posting Class", Required = true)]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.postClassID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.lotSerClassID> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.itemType), CacheGlobal = true)]
  [PXUIField]
  [INItemTypes.NonStockList]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.itemType> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.valMethod> e)
  {
  }

  [PXMergeAttributes]
  [Account(DisplayName = "Expense Accrual Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.invtAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (SubAccountAttribute), "DisplayName", "Expense Accrual Sub.")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.invtSubID> e)
  {
  }

  [PXMergeAttributes]
  [Account(DisplayName = "Expense Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.cOGSAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (SubAccountAttribute), "DisplayName", "Expense Sub.")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.cOGSSubID> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.stkItem> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsNotEqual<True>>), "The class you have selected can not be assigned to a non-stock item, because the Stock Item check box is selected for this class on the Item Classes (IN201000) form. Select another item class which is designated to group non-stock items.", new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.itemClassID> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is a Kit")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.kitItem> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.defaultSubItemOnEntry> e)
  {
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (InventoryItem.deferredCode))]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (SearchFor<DRDeferredCode.deferredCodeID>))]
  [PXRestrictor(typeof (Where<BqlOperand<DRDeferredCode.active, IBqlBool>.IsEqual<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.deferredCode> e)
  {
  }

  [PXDBBool(BqlField = typeof (InventoryItem.isSplitted))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Split into Components")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.isSplitted> e)
  {
  }

  [PXDBBool(BqlField = typeof (InventoryItem.useParentSubID))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Component Subaccounts")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.useParentSubID> e)
  {
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Total Percentage", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.totalPercentage> e)
  {
  }

  [PXDBBool(BqlField = typeof (InventoryItem.nonStockReceipt))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Receipt")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.nonStockReceipt> e)
  {
  }

  [PXDBBool(BqlField = typeof (InventoryItem.nonStockShip))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Shipment")]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.nonStockShip> e)
  {
  }

  [PXMergeAttributes]
  [CommodityCodeTypes.NonStockCommodityCodeList]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.commodityCodeType> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemClass.stkItem> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("N", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.itemClassID, Equal<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<False>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemClass.itemType> e)
  {
  }

  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (INComponent.FK.InventoryItem))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INComponent.inventoryID> e)
  {
  }

  [PXDefault]
  [Inventory(typeof (SearchFor<InventoryItem.inventoryID>.Where<MatchUser>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), Filterable = true, IsKey = true, DisplayName = "Inventory ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<INComponent.componentID> e)
  {
  }

  [PXDBInt]
  [PXParent(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<POVendorInventory.inventoryID, IBqlInt>.FromCurrent>>))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POVendorInventory.inventoryID> e)
  {
  }

  [SubItem(typeof (POVendorInventory.inventoryID), DisplayName = "Subitem")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POVendorInventory.subItemID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SubItemAttribute), "Disabled", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemXRef.subItemID> e)
  {
  }

  [NonStockItem(IsKey = true, DirtyRead = true)]
  [PXParent(typeof (INItemCategory.FK.InventoryItem))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemCategory.inventoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<InventoryItem> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<InventoryItem.kitItem>((Action<PXUIFieldAttribute>) (fa => fa.Enabled = !e.Row.TemplateItemID.HasValue));
    PXUIFieldAttribute.SetRequired<InventoryItem.postClassID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, e.Row.NonStockReceipt.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<InventoryItem.taxCalcMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, e.Row.ItemType == "E");
    attributeAdjuster = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<InventoryItem.completePOLine>((Action<PXUIFieldAttribute>) (fa => fa.Enabled = !e.Row.TemplateItemID.HasValue));
    chained = chained.SameFor<InventoryItem.nonStockReceipt>();
    chained.SameFor<InventoryItem.nonStockShip>();
  }

  protected override void ResetDefaultsOnItemClassChange(InventoryItem row)
  {
    base.ResetDefaultsOnItemClassChange(row);
    ((PXSelectBase) this.Item).Cache.SetDefaultExt<InventoryItem.postToExpenseAccount>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.kitItem> e)
  {
    if (e.Row == null || !e.Row.KitItem.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.kitItem>>) e).Cache.SetValueExt<InventoryItem.postToExpenseAccount>((object) e.Row, (object) "P");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.nonStockShip> e)
  {
    if (((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.nonStockShip>, InventoryItem, object>) e).NewValue).GetValueOrDefault())
      return;
    if (PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INKitSpecStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.InventoryID
    })) != null)
      throw new PXSetPropertyException<InventoryItem.nonStockShip>("The check box cannot be cleared because the item is a kit and it contains at least one component that requires shipping.");
    if (PXResultset<INKitSpecNonStkDet>.op_Implicit(PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<InventoryItem.nonStockShip, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.InventoryID
    })) != null)
      throw new PXSetPropertyException<InventoryItem.nonStockShip>("The check box cannot be cleared because the item is a kit and it contains at least one component that requires shipping.");
  }

  protected override void _(PX.Data.Events.RowInserted<InventoryItem> e)
  {
    base._(e);
    e.Row.TotalPercentage = new Decimal?((Decimal) 100);
  }

  protected override void _(PX.Data.Events.RowPersisting<InventoryItem> e)
  {
    base._(e);
    if (e.Row.IsSplitted.GetValueOrDefault())
    {
      if (string.IsNullOrEmpty(e.Row.DeferredCode))
      {
        if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache.RaiseExceptionHandling<InventoryItem.deferredCode>((object) e.Row, (object) e.Row.DeferredCode, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[deferredCode]"
        })))
          throw new PXRowPersistingException(typeof (InventoryItem.deferredCode).Name, (object) e.Row.DeferredCode, "'{0}' cannot be empty.", new object[1]
          {
            (object) typeof (InventoryItem.deferredCode).Name
          });
      }
      List<INComponent> list = GraphHelper.RowCast<INComponent>((IEnumerable) ((PXSelectBase<INComponent>) this.Components).Select(Array.Empty<object>())).ToList<INComponent>();
      InventoryItemMaintBase.VerifyComponentPercentages(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache, e.Row, (IEnumerable<INComponent>) list);
      InventoryItemMaintBase.VerifyOnlyOneResidualComponent(((PXSelectBase) this.Components).Cache, (IEnumerable<INComponent>) list);
      InventoryItemMaintBase.CheckSameTermOnAllComponents(((PXSelectBase) this.Components).Cache, (IEnumerable<INComponent>) list);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      e.Row.NonStockReceipt = new bool?(false);
      e.Row.NonStockShip = new bool?(false);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>())
      e.Row.NonStockReceiptAsService = e.Row.NonStockReceipt;
    if (e.Row.NonStockReceipt.GetValueOrDefault() && string.IsNullOrEmpty(e.Row.PostClassID))
      throw new PXRowPersistingException(typeof (InventoryItem.postClassID).Name, (object) e.Row.PostClassID, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (InventoryItem.postClassID).Name
      });
    if (PXDBOperationExt.Command(e.Operation) != 3)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXDatabase.Delete<CSAnswers>(new PXDataFieldRestrict[1]
      {
        new PXDataFieldRestrict("RefNoteID", (PXDbType) 14, (object) e.Row.NoteID)
      });
      transactionScope.Complete((PXGraph) this);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<InventoryItem> e)
  {
    DiscountEngine.RemoveFromCachedInventoryPriceClasses(e.Row.InventoryID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.subItemID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.subItemID>>) e).Cancel = true;
  }

  public class CostPriceSettings : 
    CurySettingsExtension<NonStockItemMaint, InventoryItem, InventoryItemCurySettings>
  {
    [PXButton(CommitChanges = true)]
    [PXUIField]
    public virtual IEnumerable UpdateCost(PXAdapter adapter)
    {
      InventoryItemCurySettings itemCurySettings = (InventoryItemCurySettings) this.curySettings.SelectSingle(Array.Empty<object>());
      if (itemCurySettings != null && itemCurySettings.PendingStdCostDate.HasValue)
      {
        InventoryItemCurySettings copy = (InventoryItemCurySettings) this.curySettings.Cache.CreateCopy((object) itemCurySettings);
        copy.LastStdCost = copy.StdCost;
        copy.StdCostDate = new DateTime?(copy.PendingStdCostDate ?? ((PXGraph) this.Base).Accessinfo.BusinessDate.Value);
        copy.StdCost = copy.PendingStdCost;
        copy.PendingStdCost = new Decimal?(0M);
        copy.PendingStdCostDate = new DateTime?();
        this.curySettings.Cache.Update((object) copy);
        ((PXAction) this.Base.Save).Press();
      }
      return adapter.Get();
    }
  }
}
