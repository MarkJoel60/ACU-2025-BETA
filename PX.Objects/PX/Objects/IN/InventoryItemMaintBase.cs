// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemMaintBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public abstract class InventoryItemMaintBase : PXGraph<
#nullable disable
InventoryItemMaintBase, InventoryItem>
{
  public bool doResetDefaultsOnItemClassChange;
  [PXViewName("Inventory Item")]
  public FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InventoryItem.itemStatus, 
  #nullable disable
  NotEqual<InventoryItemStatus.unknown>>>>, And<BqlOperand<
  #nullable enable
  InventoryItem.isTemplate, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.And<MatchUser>>, InventoryItem>.View Item;
  [PXDependToCache(new System.Type[] {typeof (InventoryItem)})]
  public FbqlSelect<SelectFromBase<InventoryItemCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InventoryItemCurySettings.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  InventoryItem.inventoryID, IBqlInt>.AsOptional>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  InventoryItemCurySettings.curyID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.AsOptional>>>, 
  #nullable disable
  InventoryItemCurySettings>.View ItemCurySettings;
  public FbqlSelect<SelectFromBase<InventoryItemCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  InventoryItemCurySettings.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>, InventoryItemCurySettings>.View AllItemCurySettings;
  [PXCopyPasteHiddenView]
  public INSubItemSegmentValueList SegmentValues;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (InventoryItem.body), typeof (InventoryItem.imageUrl)})]
  public FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  InventoryItem.inventoryID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  InventoryItem>.View ItemSettings;
  public FbqlSelect<SelectFromBase<INComponent, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INComponent.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INComponent>, InventoryItem, INComponent>.SameAsCurrent>, INComponent>.View Components;
  [PXDependToCache(new System.Type[] {typeof (InventoryItem)})]
  public FbqlSelect<SelectFromBase<INCategory, TypeArrayOf<IFbqlJoin>.Empty>.Order<PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  INCategory.sortOrder, IBqlInt>.Asc>>, 
  #nullable disable
  INCategory>.View Categories;
  public FbqlSelect<SelectFromBase<ARSalesPrice, TypeArrayOf<IFbqlJoin>.Empty>, ARSalesPrice>.View SalesPrice;
  public PXSetupOptional<INSetup> insetup;
  public PXSetupOptional<SOSetup> sosetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public FbqlSelect<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  InventoryItem.itemClassID, IBqlInt>.AsOptional>>, 
  #nullable disable
  INItemClass>.View ItemClass;
  public POVendorInventorySelect<POVendorInventory, LeftJoin<PX.Objects.AP.Vendor, On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  POVendorInventory.vendorID>>, LeftJoin<PX.Objects.CR.Standalone.Location, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Standalone.Location.bAccountID, 
  #nullable disable
  Equal<POVendorInventory.vendorID>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  POVendorInventory.vendorLocationID>>>>>, Where<POVendorInventory.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<Where<PX.Objects.AP.Vendor.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>>, InventoryItem> VendorItems;
  public FbqlSelect<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemXRef.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemXRef>, InventoryItem, INItemXRef>.SameAsCurrent>, INItemXRef>.View itemxrefrecords;
  public CRAttributeList<InventoryItem> Answers;
  public FbqlSelect<SelectFromBase<INItemCategory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCategory>.On<INItemCategory.FK.Category>>>.Where<KeysRelation<Field<INItemCategory.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INItemCategory>, InventoryItem, INItemCategory>.SameAsCurrent>, INItemCategory>.View Category;
  [PXDependToCache(new System.Type[] {typeof (InventoryItem)})]
  public FbqlSelect<SelectFromBase<CacheEntityItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CacheEntityItem.path, IBqlString>.IsEqual<
  #nullable disable
  CacheEntityItem.path>>.Order<PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  CacheEntityItem.number, IBqlInt>.Asc>>, 
  #nullable disable
  CacheEntityItem>.View EntityItems;
  public FbqlSelect<SelectFromBase<ARPriceWorksheetDetail, TypeArrayOf<IFbqlJoin>.Empty>, ARPriceWorksheetDetail>.View arpriceworksheetdetails;
  public FbqlSelect<SelectFromBase<DiscountItem, TypeArrayOf<IFbqlJoin>.Empty>, DiscountItem>.View discountitems;
  public FbqlSelect<SelectFromBase<PMItemRate, TypeArrayOf<IFbqlJoin>.Empty>, PMItemRate>.View pmitemrates;
  public FbqlSelect<SelectFromBase<INKitSpecHdr, TypeArrayOf<IFbqlJoin>.Empty>, INKitSpecHdr>.View kitheaders;
  public FbqlSelect<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INKitSpecStkDet.compInventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INKitSpecStkDet>, InventoryItem, INKitSpecStkDet>.SameAsCurrent>, INKitSpecStkDet>.View kitspecs;
  public FbqlSelect<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INKitSpecNonStkDet.compInventoryID>.IsRelatedTo<InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<InventoryItem, INKitSpecNonStkDet>, InventoryItem, INKitSpecNonStkDet>.SameAsCurrent>, INKitSpecNonStkDet>.View kitnonstockdet;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.BAccount>.View dummy_BAccount;
  public FbqlSelect<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AP.Vendor>.View dummy_Vendor;
  public PXChangeID<InventoryItem, InventoryItem.inventoryCD> ChangeID;
  public PXAction<InventoryItem> updateCost;
  public PXAction<InventoryItem> viewSalesPrices;
  public PXAction<InventoryItem> viewVendorPrices;
  public PXAction<InventoryItem> viewRestrictionGroups;

  public abstract bool IsStockItemFlag { get; }

  public virtual bool DefaultSiteFromItemClass { get; set; } = true;

  public InventoryItemMaintBase()
  {
    INSetup current1 = ((PXSelectBase<INSetup>) this.insetup).Current;
    SOSetup current2 = ((PXSelectBase<SOSetup>) this.sosetup).Current;
    CommonSetup current3 = ((PXSelectBase<CommonSetup>) this.commonsetup).Current;
    PXDBDefaultAttribute.SetDefaultForInsert<INItemXRef.inventoryID>(((PXSelectBase) this.itemxrefrecords).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INComponent.amtOption>(((PXSelectBase) this.Components).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.aSC606>());
  }

  [PXMergeAttributes]
  [INParentItemClass(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemClass.parentItemClassID> e)
  {
  }

  [LocationID(typeof (Where<BqlOperand<PX.Objects.CR.Location.bAccountID, IBqlInt>.IsEqual<BqlField<POVendorInventory.vendorID, IBqlInt>.FromCurrent>>))]
  [PXFormula(typeof (Selector<POVendorInventory.vendorID, PX.Objects.AP.Vendor.defLocationID>))]
  [PXParent(typeof (SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Location.bAccountID, Equal<BqlField<POVendorInventory.vendorID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<BqlField<POVendorInventory.vendorLocationID, IBqlInt>.FromCurrent>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POVendorInventory.vendorLocationID> e)
  {
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (DRDeferredCode.deferredCodeID))]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DRDeferredCode.multiDeliverableArrangement, NotEqual<True>>>>>.And<BqlOperand<DRDeferredCode.accountType, IBqlString>.IsEqual<DeferredAccountType.income>>>), "Multi-Deliverable Arrangement codes can't be used on components", new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INComponent.deferredCode> e)
  {
  }

  [PXParent(typeof (INItemXRef.FK.InventoryItem))]
  [Inventory(Filterable = true, DirtyRead = true, Enabled = false, IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID), DefaultForInsert = true, DefaultForUpdate = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemXRef.inventoryID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (INCategory.categoryID), DescriptionField = typeof (INCategory.description))]
  [PXUIField(DisplayName = "Category ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemCategory.categoryID> e)
  {
  }

  [PXDependToCache(new System.Type[] {typeof (InventoryItem)})]
  public virtual IEnumerable itemCurySettings()
  {
    InventoryItemMaintBase inventoryItemMaintBase = this;
    InventoryItemCurySettings itemCurySettings = (InventoryItemCurySettings) new PXView((PXGraph) inventoryItemMaintBase, false, ((PXSelectBase) inventoryItemMaintBase.ItemCurySettings).View.BqlSelect).SelectSingle(PXView.Parameters);
    if (itemCurySettings == null && PXView.Parameters.Length != 0 && PXView.Parameters[0] is int parameter)
    {
      InventoryItem current = ((PXSelectBase<InventoryItem>) inventoryItemMaintBase.Item).Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        int? inventoryId = current.InventoryID;
        num = inventoryId.GetValueOrDefault() == parameter & inventoryId.HasValue ? 1 : 0;
      }
      if (num != 0)
      {
        bool isDirty1 = ((PXSelectBase) inventoryItemMaintBase.ItemCurySettings).Cache.IsDirty;
        bool isDirty2 = ((PXSelectBase) inventoryItemMaintBase.Item).Cache.IsDirty;
        itemCurySettings = (InventoryItemCurySettings) ((PXSelectBase) inventoryItemMaintBase.ItemCurySettings).Cache.Insert();
        ((PXSelectBase) inventoryItemMaintBase.ItemCurySettings).Cache.IsDirty = isDirty1;
        ((PXSelectBase) inventoryItemMaintBase.Item).Cache.IsDirty = isDirty2;
      }
    }
    yield return (object) itemCurySettings;
  }

  protected virtual IEnumerable categories([PXInt] int? categoryID)
  {
    return (IEnumerable) this.GetCategories(categoryID);
  }

  protected IEnumerable entityItems(string parent) => this.GetEntityItems(parent);

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable UpdateCost(PXAdapter adapter) => adapter.Get();

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter a)
  {
    InventoryItemMaintBase graph = this;
    foreach (InventoryItem inventoryItem1 in ((PXAction) new PXCancel<InventoryItem>((PXGraph) graph, "Cancel")).Press(a))
    {
      if (((PXSelectBase) graph.Item).Cache.GetStatus((object) inventoryItem1) == 2)
      {
        InventoryItem inventoryItem2 = InventoryItem.UK.Find((PXGraph) graph, inventoryItem1.InventoryCD);
        if (inventoryItem2 != null)
          ((PXSelectBase) graph.Item).Cache.RaiseExceptionHandling<InventoryItem.inventoryCD>((object) inventoryItem1, (object) inventoryItem1.InventoryCD, (Exception) new PXSetPropertyException(inventoryItem2.IsTemplate.GetValueOrDefault() ? "This ID is already used for another template item. Specify another ID." : (inventoryItem2.StkItem.GetValueOrDefault() ? "This ID is already used for another Stock Item." : "This ID is already used for another Non-Stock Item.")));
      }
      yield return (object) inventoryItem1;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewSalesPrices(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      ARSalesPriceMaint instance = PXGraph.CreateInstance<ARSalesPriceMaint>();
      ((PXSelectBase<ARSalesPriceFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Sales Prices");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewVendorPrices(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      APVendorPriceMaint instance = PXGraph.CreateInstance<APVendorPriceMaint>();
      ((PXSelectBase<APVendorPriceFilter>) instance.Filter).Current.InventoryID = ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Vendor Prices");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      INAccessDetailByItem instance = PXGraph.CreateInstance<INAccessDetailByItem>();
      ((PXSelectBase<InventoryItem>) instance.Item).Current = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) instance.Item).Search<InventoryItem.inventoryCD>((object) ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowInserted<InventoryItem> e)
  {
    if (e.Row.InventoryCD == null || e.Row.IsConversionMode.GetValueOrDefault())
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.ItemCurySettings).Cache
    }))
      this.SetDefaultSiteID(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<InventoryItem> e)
  {
    if (e.Row == null)
      return;
    if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      PXUIFieldAttribute.SetWarning<InventoryItem.weightUOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, string.IsNullOrEmpty(((PXSelectBase<CommonSetup>) this.commonsetup).Current.WeightUOM) ? "Default values for weight UOM and volume UOM are not specified on the Companies (CS101500) form." : (string) null);
      PXUIFieldAttribute.SetWarning<InventoryItem.volumeUOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, string.IsNullOrEmpty(((PXSelectBase<CommonSetup>) this.commonsetup).Current.VolumeUOM) ? "Default values for weight UOM and volume UOM are not specified on the Companies (CS101500) form." : (string) null);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
      PXUIFieldAttribute.SetVisible<InventoryItem.totalPercentage>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) null, false);
    PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXViewOf<DRDeferredCode>.BasedOn<SelectFromBase<DRDeferredCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<DRDeferredCode.deferredCodeID, IBqlString>.IsEqual<BqlField<InventoryItem.deferredCode, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<POVendorInventory.isDefault>(((PXSelectBase) this.VendorItems).Cache, (object) null, true);
    InventoryItemMaintBase.SetDefaultTermControlsState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, e.Row);
    ((PXSelectBase) this.Components).Cache.AllowDelete = false;
    ((PXSelectBase) this.Components).Cache.AllowInsert = false;
    ((PXSelectBase) this.Components).Cache.AllowUpdate = false;
    if (e.Row.IsSplitted.GetValueOrDefault())
    {
      ((PXSelectBase) this.Components).Cache.AllowDelete = true;
      ((PXSelectBase) this.Components).Cache.AllowInsert = true;
      ((PXSelectBase) this.Components).Cache.AllowUpdate = true;
      e.Row.TotalPercentage = new Decimal?(this.SumComponentsPercentage());
      PXUIFieldAttribute.SetEnabled<InventoryItem.useParentSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, true);
    }
    else
    {
      e.Row.TotalPercentage = new Decimal?((Decimal) 100);
      e.Row.UseParentSubID = new bool?(false);
      PXUIFieldAttribute.SetEnabled<InventoryItem.useParentSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row, false);
    }
    InventoryHelper.CheckZeroDefaultTerm<InventoryItem.deferredCode, InventoryItem.defaultTerm>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) e.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItem>>) e).Cache, (object) null).For<InventoryItem.itemClassID>((Action<PXUIFieldAttribute>) (a => a.Enabled = !e.Row.TemplateItemID.HasValue));
    chained1 = chained1.SameFor<InventoryItem.baseUnit>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained2 = chained1.SameFor<InventoryItem.decimalBaseUnit>();
    chained2 = chained2.SameFor<InventoryItem.itemType>();
    chained2.SameFor<InventoryItem.taxCategoryID>();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<InventoryItem> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 1)
      return;
    bool? nullable = e.Row.KitItem;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PXSelectBase) this.Item).Cache.GetValueOriginal<InventoryItem.kitItem>((object) e.Row);
    if (!nullable.GetValueOrDefault())
      return;
    if (PXResultset<INKitSpecHdr>.op_Implicit(PXSelectBase<INKitSpecHdr, PXViewOf<INKitSpecHdr>.BasedOn<SelectFromBase<INKitSpecHdr, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INKitSpecHdr.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) e.Row.InventoryID
    })) != null && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItem>>) e).Cache.RaiseExceptionHandling<InventoryItem.kitItem>((object) e.Row, (object) e.Row.KitItem, (Exception) new PXSetPropertyException<InventoryItem.kitItem>("The check box cannot be cleared because a kit specification exists for this item.")))
      throw new PXRowPersistingException("kitItem", (object) e.Row.KitItem, "The check box cannot be cleared because a kit specification exists for this item.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.deferredCode> e)
  {
    InventoryItemMaintBase.UpdateSplittedFromDeferralCode(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.deferredCode>>) e).Cache, e.Row);
    InventoryItemMaintBase.SetDefaultTerm(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.deferredCode>>) e).Cache, (object) e.Row, typeof (InventoryItem.deferredCode), typeof (InventoryItem.defaultTerm), typeof (InventoryItem.defaultTermUOM));
  }

  public static void UpdateSplittedFromDeferralCode(PXCache cache, InventoryItem item)
  {
    if (item == null)
      return;
    DRDeferredCode drDeferredCode = (DRDeferredCode) PXSelectorAttribute.Select<InventoryItem.deferredCode>(cache, (object) item);
    cache.SetValueExt<InventoryItem.isSplitted>((object) item, (object) (bool) (drDeferredCode == null ? 0 : (drDeferredCode.MultiDeliverableArrangement.GetValueOrDefault() ? 1 : 0)));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID> e)
  {
    int? nullable1;
    if (e.Row != null)
    {
      nullable1 = e.Row.ItemClassID;
      int num = 0;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      {
        INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) this.ItemClass).Select(Array.Empty<object>()));
        InventoryItem row = e.Row;
        int? nullable2;
        if (inItemClass == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = inItemClass.ParentItemClassID;
        row.ParentItemClassID = nullable2;
        goto label_8;
      }
    }
    if (e.Row != null)
      e.Row.ParentItemClassID = e.Row.ItemClassID;
label_8:
    if (e.Row != null)
    {
      nullable1 = e.Row.ItemClassID;
      if (nullable1.HasValue && ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).ExternalCall)
        ((PXSelectBase) this.Answers).Cache.Clear();
    }
    InventoryItem current = ((PXSelectBase<InventoryItem>) this.Item).Current;
    if ((current != null ? (current.IsConversionMode.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      InventoryItemCurySettings curySettings = this.GetCurySettings(e.Row.InventoryID);
      int num;
      if (curySettings == null)
      {
        num = 1;
      }
      else
      {
        nullable1 = curySettings.DfltSiteID;
        num = !nullable1.HasValue ? 1 : 0;
      }
      if (num != 0)
        this.SetDefaultSiteID(e.Row, false);
      if (e.Row.DeferredCode == null)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.deferredCode>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.postClassID>((object) e.Row);
      if (EnumerableExtensions.IsIn<Decimal?>(e.Row.MarkupPct, new Decimal?(), new Decimal?(0M)))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.markupPct>((object) e.Row);
      if (EnumerableExtensions.IsIn<Decimal?>(e.Row.MinGrossProfitPct, new Decimal?(), new Decimal?(0M)))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.minGrossProfitPct>((object) e.Row);
      if (e.Row.TaxCategoryID == null)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.taxCategoryID>((object) e.Row);
      if (e.Row.PriceClassID == null)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.priceClassID>((object) e.Row);
      INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) this.ItemClass).Select(Array.Empty<object>()));
      nullable1 = e.Row.PriceWorkgroupID;
      if (!nullable1.HasValue)
      {
        PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache;
        InventoryItem row = e.Row;
        int? nullable3;
        if (inItemClass == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = inItemClass.PriceWorkgroupID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) nullable3;
        cache.SetValue<InventoryItem.priceWorkgroupID>((object) row, (object) local);
      }
      nullable1 = e.Row.PriceManagerID;
      if (!nullable1.HasValue)
      {
        PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache;
        InventoryItem row = e.Row;
        int? nullable4;
        if (inItemClass == null)
        {
          nullable1 = new int?();
          nullable4 = nullable1;
        }
        else
          nullable4 = inItemClass.PriceManagerID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) nullable4;
        cache.SetValue<InventoryItem.priceManagerID>((object) row, (object) local);
      }
      if (EnumerableExtensions.IsIn<Decimal?>(e.Row.UndershipThreshold, new Decimal?(), new Decimal?(100M)))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.undershipThreshold>((object) e.Row);
      if (EnumerableExtensions.IsIn<Decimal?>(e.Row.OvershipThreshold, new Decimal?(), new Decimal?(100M)))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.overshipThreshold>((object) e.Row);
      if (e.Row.PreferredItemClasses == null)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.preferredItemClasses>((object) e.Row);
      if (e.Row.PriceOfSuggestedItems != null)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID>>) e).Cache.SetDefaultExt<InventoryItem.priceOfSuggestedItems>((object) e.Row);
    }
    else
    {
      if (!this.doResetDefaultsOnItemClassChange)
        return;
      this.ResetDefaultsOnItemClassChange(e.Row);
    }
  }

  protected virtual void ResetDefaultsOnItemClassChange(InventoryItem row)
  {
    PXCache cache = ((PXSelectBase) this.Item).Cache;
    this.ResetConversionsSettings(cache, row);
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      this.SetDefaultSiteID(row);
    if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      cache.SetDefaultExt<InventoryItem.deferredCode>((object) row);
      cache.SetDefaultExt<InventoryItem.postClassID>((object) row);
      cache.SetDefaultExt<InventoryItem.markupPct>((object) row);
      cache.SetDefaultExt<InventoryItem.minGrossProfitPct>((object) row);
    }
    cache.SetDefaultExt<InventoryItem.exportToExternal>((object) row);
    cache.SetDefaultExt<InventoryItem.taxCategoryID>((object) row);
    cache.SetDefaultExt<InventoryItem.itemType>((object) row);
    cache.SetDefaultExt<InventoryItem.priceClassID>((object) row);
    cache.SetDefaultExt<InventoryItem.priceWorkgroupID>((object) row);
    cache.SetDefaultExt<InventoryItem.priceManagerID>((object) row);
    cache.SetDefaultExt<InventoryItem.undershipThreshold>((object) row);
    cache.SetDefaultExt<InventoryItem.overshipThreshold>((object) row);
    cache.SetDefaultExt<InventoryItem.commodityCodeType>((object) row);
    cache.SetDefaultExt<InventoryItem.hSTariffCode>((object) row);
    INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) this.ItemClass).Select(Array.Empty<object>()));
    if (inItemClass != null)
    {
      cache.SetValue<InventoryItem.priceWorkgroupID>((object) row, (object) inItemClass.PriceWorkgroupID);
      cache.SetValue<InventoryItem.priceManagerID>((object) row, (object) inItemClass.PriceManagerID);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.relatedItemAssistant>())
      return;
    cache.SetDefaultExt<InventoryItem.preferredItemClasses>((object) row);
    cache.SetDefaultExt<InventoryItem.priceOfSuggestedItems>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.itemClassID> e)
  {
    this.doResetDefaultsOnItemClassChange = false;
    if (PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) this.ItemClass).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.itemClassID>, InventoryItem, object>) e).NewValue
    })) == null)
      return;
    this.doResetDefaultsOnItemClassChange = true;
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.itemClassID>>) e).ExternalCall || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<InventoryItem, InventoryItem.itemClassID>>) e).Cache.GetStatus((object) e.Row) == 2)
      return;
    InventoryItem row = e.Row;
    if ((row != null ? (!row.IsConversionMode.GetValueOrDefault() ? 1 : 0) : 1) == 0 || ((PXSelectBase<InventoryItem>) this.Item).Ask("Warning", "Please confirm if you want to update current Item settings with the Inventory Class defaults. Original settings will be preserved otherwise.", (MessageButtons) 4) != 7)
      return;
    this.doResetDefaultsOnItemClassChange = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.isSplitted> e)
  {
    if (e.Row == null)
      return;
    bool? isSplitted = e.Row.IsSplitted;
    bool flag = false;
    if (isSplitted.GetValueOrDefault() == flag & isSplitted.HasValue)
    {
      foreach (PXResult<INComponent> pxResult in ((PXSelectBase<INComponent>) this.Components).Select(Array.Empty<object>()))
        ((PXSelectBase<INComponent>) this.Components).Delete(PXResult<INComponent>.op_Implicit(pxResult));
      e.Row.TotalPercentage = new Decimal?((Decimal) 100);
    }
    else
      e.Row.TotalPercentage = new Decimal?(0M);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemXRef> e)
  {
    if (e.Row == null)
      return;
    this.VerifyXRefUOMExists(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemXRef>>) e).Cache, e.Row);
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null && e.Row.AlternateID != null && !PXDimensionAttribute.MatchMask<InventoryItem.inventoryCD>(((PXSelectBase) this.Item).Cache, e.Row.AlternateID))
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemXRef>>) e).Cache.RaiseExceptionHandling<INItemXRef.alternateID>((object) e.Row, (object) e.Row.AlternateID, (Exception) new PXSetPropertyException("The specified alternate ID does not comply with the INVENTORY segmented key settings. It might be not possible to use this alternate ID directly in entry forms.", (PXErrorLevel) 2));
    PXUIFieldAttribute.SetEnabled<INItemXRef.bAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemXRef>>) e).Cache, (object) e.Row, EnumerableExtensions.IsIn<string>(e.Row.AlternateType, "0CPN", "0VPN"));
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemXRef>>) e).Cache;
    INItemXRef row = e.Row;
    int? baccountId = e.Row.BAccountID;
    int num1;
    if (baccountId.HasValue)
    {
      baccountId = e.Row.BAccountID;
      int num2 = 0;
      if (!(baccountId.GetValueOrDefault() == num2 & baccountId.HasValue))
      {
        num1 = 0;
        goto label_7;
      }
    }
    num1 = e.Row.AlternateID == null ? 1 : 0;
label_7:
    PXUIFieldAttribute.SetEnabled<INItemXRef.alternateType>(cache, (object) row, num1 != 0);
  }

  private void VerifyXRefUOMExists(PXCache sender, INItemXRef row)
  {
    sender.RaiseExceptionHandling<INItemXRef.uOM>((object) row, (object) row.UOM, (Exception) (EnumerableExtensions.IsIn<string>(row.UOM, GraphHelper.RowCast<INUnit>((IEnumerable) ((PXSelectBase<INUnit>) this.UnitsOfMeasureExt.itemunits).Select(Array.Empty<object>())).Select<INUnit, string>((Func<INUnit, string>) (u => u.FromUnit)).Concat<string>((IEnumerable<string>) new string[2]
    {
      null,
      ((PXSelectBase<InventoryItem>) this.Item).Current.BaseUnit
    })) ? (PXSetPropertyException) null : new PXSetPropertyException("The specified unit of measure is not defined for this inventory item.", (PXErrorLevel) 2)));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID> e)
  {
    if (e.Row == null || ((PXSelectBase<InventoryItem>) this.Item).Current == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID>, INItemXRef, object>) e).NewValue == null)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID>, INItemXRef, object>) e).NewValue = (object) ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID>, INItemXRef, object>) e).NewValue).Trim();
    if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID>, INItemXRef, object>) e).NewValue == string.Empty))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.alternateID>, INItemXRef, object>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INItemXRef, INItemXRef.inventoryID> e)
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INItemXRef, INItemXRef.inventoryID>, INItemXRef, object>) e).NewValue = (object) ((PXSelectBase<InventoryItem>) this.Item).Current.InventoryID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INItemXRef, INItemXRef.inventoryID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.inventoryID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.inventoryID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemXRef> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || !EnumerableExtensions.IsNotIn<string>(e.Row.AlternateType, "0VPN", "0CPN"))
      return;
    e.Row.BAccountID = new int?(0);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INItemXRef>>) e).Cache.Normalize();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.bAccountID> e)
  {
    if (((PXGraph) this).IsCopyPasteContext && e.Row.AlternateType == "0VPN")
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.bAccountID>>) e).Cancel = true;
    if (!EnumerableExtensions.IsNotIn<string>(e.Row.AlternateType, "0VPN", "0CPN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemXRef, INItemXRef.bAccountID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INComponent, INComponent.percentage> e)
  {
    if (e.Row == null || !(e.Row.AmtOption == "P"))
      return;
    e.Row.Percentage = new Decimal?(this.GetRemainingPercentage());
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INComponent, INComponent.componentID> e)
  {
    if (e.Row == null)
      return;
    InventoryItem dirty = InventoryItem.PK.FindDirty((PXGraph) this, e.Row.ComponentID);
    DRDeferredCode drDeferredCode = (DRDeferredCode) PXSelectorAttribute.Select<InventoryItem.deferredCode>(((PXSelectBase) this.Item).Cache, (object) dirty);
    bool flag = drDeferredCode != null && !drDeferredCode.MultiDeliverableArrangement.GetValueOrDefault();
    if (dirty == null)
      return;
    e.Row.SalesAcctID = dirty.SalesAcctID;
    e.Row.SalesSubID = dirty.SalesSubID;
    e.Row.UOM = dirty.SalesUnit;
    e.Row.DeferredCode = flag ? dirty.DeferredCode : (string) null;
    e.Row.DefaultTerm = dirty.DefaultTerm;
    e.Row.DefaultTermUOM = dirty.DefaultTermUOM;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INComponent, INComponent.amtOption> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.AmtOption == "P")
    {
      e.Row.FixedAmt = new Decimal?();
      e.Row.Percentage = new Decimal?(this.GetRemainingPercentage());
    }
    else
      e.Row.Percentage = new Decimal?(0M);
    if (!(e.Row.AmtOption == "R"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponent, INComponent.amtOption>>) e).Cache.SetValueExt<INComponent.deferredCode>((object) e.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponent, INComponent.amtOption>>) e).Cache.SetDefaultExt<INComponent.fixedAmt>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponent, INComponent.amtOption>>) e).Cache.SetDefaultExt<INComponent.percentage>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INComponent> e)
  {
    InventoryItemMaintBase.SetComponentControlsState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INComponent>>) e).Cache, e.Row);
    InventoryHelper.CheckZeroDefaultTerm<INComponent.deferredCode, INComponent.defaultTerm>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INComponent>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INComponent, INComponent.deferredCode> e)
  {
    InventoryItemMaintBase.SetDefaultTerm(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INComponent, INComponent.deferredCode>>) e).Cache, (object) e.Row, typeof (INComponent.deferredCode), typeof (INComponent.defaultTerm), typeof (INComponent.defaultTermUOM));
  }

  public static void SetDefaultTerm(
    PXCache cache,
    object row,
    System.Type deferralCode,
    System.Type defaultTerm,
    System.Type defaultTermUOM)
  {
    if (row == null)
      return;
    DRDeferredCode code = (DRDeferredCode) PXSelectorAttribute.Select(cache, row, deferralCode.Name);
    if (code != null && DeferredMethodType.RequiresTerms(code))
      return;
    cache.SetDefaultExt(row, defaultTerm.Name, (object) null);
    cache.SetDefaultExt(row, defaultTermUOM.Name, (object) null);
  }

  public static void SetComponentControlsState(PXCache cache, INComponent component)
  {
    bool flag1 = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
    {
      flag1 = true;
      PXUIFieldAttribute.SetEnabled<INComponent.amtOption>(cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<INComponent.fixedAmt>(cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<INComponent.percentage>(cache, (object) null, false);
      PXDefaultAttribute.SetPersistingCheck<INComponent.amtOption>(cache, (object) null, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<INComponent.fixedAmt>(cache, (object) null, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<INComponent.percentage>(cache, (object) null, (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetVisible<INComponent.amtOption>(cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<INComponent.fixedAmt>(cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<INComponent.percentage>(cache, (object) null, false);
    }
    if (component == null)
      return;
    bool flag2 = true;
    bool flag3;
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
    {
      flag3 = component.AmtOptionASC606 == "R";
    }
    else
    {
      flag3 = component.AmtOption == "R";
      flag2 = !flag3;
    }
    PXDefaultAttribute.SetPersistingCheck<INComponent.deferredCode>(cache, (object) component, flag3 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetEnabled<INComponent.deferredCode>(cache, (object) component, flag2);
    PXUIFieldAttribute.SetEnabled<INComponent.fixedAmt>(cache, (object) component, !flag1 && component.AmtOption == "F");
    PXUIFieldAttribute.SetEnabled<INComponent.percentage>(cache, (object) component, !flag1 && component.AmtOption == "P");
    bool flag4 = DeferredMethodType.RequiresTerms((DRDeferredCode) PXSelectorAttribute.Select<INComponent.deferredCode>(cache, (object) component)) & flag2;
    PXUIFieldAttribute.SetEnabled<INComponent.defaultTerm>(cache, (object) component, flag4);
    PXUIFieldAttribute.SetEnabled<INComponent.defaultTermUOM>(cache, (object) component, flag4);
    PXUIFieldAttribute.SetEnabled<INComponent.overrideDefaultTerm>(cache, (object) component, flag4);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INUnit> e)
  {
    PXFieldState stateExt = (PXFieldState) ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache.GetStateExt<INUnit.fromUnit>((object) e.Row);
    string str;
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null && e.Row.ToUnit == ((PXSelectBase<InventoryItem>) this.Item).Current.BaseUnit && (stateExt.Error == null || stateExt.Error == PXMessages.Localize("The base unit is not the smallest unit of measure available for this item. Ensure that the quantity precision configured in the system is large enough. See the Quantity Decimal Places setting on the Branches form.", ref str) || stateExt.ErrorLevel == 1))
    {
      Decimal? unitRate;
      if (e.Row.UnitMultDiv == "M")
      {
        unitRate = e.Row.UnitRate;
        Decimal num = (Decimal) 1;
        if (unitRate.GetValueOrDefault() < num & unitRate.HasValue)
          goto label_5;
      }
      if (e.Row.UnitMultDiv == "D")
      {
        unitRate = e.Row.UnitRate;
        Decimal num = (Decimal) 1;
        if (unitRate.GetValueOrDefault() > num & unitRate.HasValue)
          goto label_5;
      }
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache.RaiseExceptionHandling<INUnit.fromUnit>((object) e.Row, (object) e.Row.FromUnit, (Exception) null);
      goto label_7;
label_5:
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache.RaiseExceptionHandling<INUnit.fromUnit>((object) e.Row, (object) e.Row.FromUnit, (Exception) new PXSetPropertyException("The base unit is not the smallest unit of measure available for this item. Ensure that the quantity precision configured in the system is large enough. See the Quantity Decimal Places setting on the Branches form.", (PXErrorLevel) 3));
    }
label_7:
    PXUIFieldAttribute.SetEnabled<INUnit.fromUnit>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INUnit>>) e).Cache.GetStatus((object) e.Row) == 2);
  }

  public virtual Decimal? GetUnitRate(PXCache sender, INUnit unit, int? itemClassID)
  {
    Decimal? unitRate = new Decimal?(unit != null ? unit.UnitRate ?? 1M : 1M);
    INUnit inUnit = PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXViewOf<INUnit>.BasedOn<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, NotEqual<INUnitType.inventoryItem>>>>, And<BqlOperand<INUnit.fromUnit, IBqlString>.IsEqual<BqlField<INUnit.fromUnit, IBqlString>.FromCurrent>>>, And<BqlOperand<INUnit.toUnit, IBqlString>.IsEqual<BqlField<INUnit.toUnit, IBqlString>.FromCurrent>>>, And<BqlOperand<INUnit.unitMultDiv, IBqlString>.IsEqual<BqlField<INUnit.unitMultDiv, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.itemClassID, Equal<P.AsInt>>>>>.Or<BqlOperand<INUnit.unitType, IBqlShort>.IsEqual<INUnitType.global>>>>.Order<PX.Data.BQL.Fluent.By<BqlField<INUnit.unitType, IBqlShort>.Asc>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) unit
    }, new object[1]{ (object) itemClassID }));
    if (inUnit != null)
      unitRate = inUnit.UnitRate;
    return unitRate;
  }

  public UnitsOfMeasure UnitsOfMeasureExt => ((PXGraph) this).FindImplementation<UnitsOfMeasure>();

  protected virtual void _(PX.Data.Events.RowUpdated<INUnit> e)
  {
    if (e.Row != null && e.Row.FromUnit != null && !((PXSelectBase) this.UnitsOfMeasureExt.itemunits).Cache.ObjectsEqual<INUnit.fromUnit>((object) e.Row, (object) e.OldRow) && ((PXSelectBase<InventoryItem>) this.Item).Current != null)
      e.Row.UnitRate = this.GetUnitRate(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INUnit>>) e).Cache, e.Row, ((PXSelectBase<InventoryItem>) this.Item).Current.ItemClassID);
    foreach (PXResult<INItemXRef> pxResult in ((PXSelectBase<INItemXRef>) this.itemxrefrecords).Select(Array.Empty<object>()))
      this.VerifyXRefUOMExists(((PXSelectBase) this.itemxrefrecords).Cache, PXResult<INItemXRef>.op_Implicit(pxResult));
  }

  protected virtual void _(PX.Data.Events.RowInserted<INUnit> e)
  {
    if (e.Row != null && e.Row.FromUnit != null && ((PXSelectBase<InventoryItem>) this.Item).Current != null)
      e.Row.UnitRate = this.GetUnitRate(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INUnit>>) e).Cache, e.Row, ((PXSelectBase<InventoryItem>) this.Item).Current.ItemClassID);
    foreach (PXResult<INItemXRef> pxResult in ((PXSelectBase<INItemXRef>) this.itemxrefrecords).Select(Array.Empty<object>()))
      this.VerifyXRefUOMExists(((PXSelectBase) this.itemxrefrecords).Cache, PXResult<INItemXRef>.op_Implicit(pxResult));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INUnit> e)
  {
    this.VerifyKitComponentsUOM(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INUnit> e)
  {
    foreach (PXResult<INItemXRef> pxResult in ((PXSelectBase<INItemXRef>) this.itemxrefrecords).Select(Array.Empty<object>()))
      this.VerifyXRefUOMExists(((PXSelectBase) this.itemxrefrecords).Cache, PXResult<INItemXRef>.op_Implicit(pxResult));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INUnit> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 3)
      return;
    this.VerifyKitComponentsUOM(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<InventoryItemCurySettings, InventoryItemCurySettings.preferredVendorID> e)
  {
    foreach (PXResult<InventoryItemCurySettings> pxResult in ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
    {
      (object) (int?) e.Row?.InventoryID
    }))
    {
      InventoryItemCurySettings itemCurySettings = PXResult<InventoryItemCurySettings>.op_Implicit(pxResult);
      if (e.Row != itemCurySettings && itemCurySettings != null && itemCurySettings.PreferredVendorID.HasValue)
      {
        PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, itemCurySettings.PreferredVendorID);
        if (vendor == null || vendor.BaseCuryID != null)
          break;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventoryItemCurySettings, InventoryItemCurySettings.preferredVendorID>, InventoryItemCurySettings, object>) e).NewValue = (object) itemCurySettings.PreferredVendorID;
        break;
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<InventoryItemCurySettings, InventoryItemCurySettings.preferredVendorLocationID> e)
  {
    foreach (PXResult<InventoryItemCurySettings> pxResult in ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
    {
      (object) (int?) e.Row?.InventoryID
    }))
    {
      InventoryItemCurySettings itemCurySettings = PXResult<InventoryItemCurySettings>.op_Implicit(pxResult);
      if (e.Row != itemCurySettings && itemCurySettings != null && itemCurySettings.PreferredVendorID.HasValue)
      {
        PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, itemCurySettings.PreferredVendorID);
        if (vendor == null || vendor.BaseCuryID != null)
          break;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventoryItemCurySettings, InventoryItemCurySettings.preferredVendorLocationID>, InventoryItemCurySettings, object>) e).NewValue = (object) itemCurySettings.PreferredVendorLocationID;
        break;
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POVendorInventory, POVendorInventory.isDefault> e)
  {
    if (PXResultset<POVendorInventory>.op_Implicit(((PXSelectBase<POVendorInventory>) this.VendorItems).SelectWindowed(0, 1, new object[1]
    {
      (object) e.Row.InventoryID
    })) != null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POVendorInventory, POVendorInventory.isDefault>, POVendorInventory, object>) e).NewValue = (object) true;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POVendorInventory, POVendorInventory.isDefault>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POVendorInventory> e)
  {
    if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Item).Cache.GetStatus((object) ((PXSelectBase<InventoryItem>) this.Item).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    IEnumerable<InventoryItemCurySettings> itemCurySettingses = GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
    {
      (object) e.Row.InventoryID
    }));
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, e.Row.VendorID);
    if (vendor == null)
      return;
    foreach (InventoryItemCurySettings itemCurySettings in itemCurySettingses)
    {
      object valueExt = ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<POVendorInventory>>) e).Cache.GetValueExt<POVendorInventory.isDefault>((object) e.Row);
      if (valueExt is PXFieldState pxFieldState)
        valueExt = pxFieldState.Value;
      bool flag = string.Equals(itemCurySettings.CuryID, vendor.BaseCuryID, StringComparison.OrdinalIgnoreCase) || vendor.BaseCuryID == null;
      if (((itemCurySettings == null ? 0 : (((bool?) valueExt).GetValueOrDefault() ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      {
        itemCurySettings.PreferredVendorID = new int?();
        itemCurySettings.PreferredVendorLocationID = new int?();
        ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<POVendorInventory> e)
  {
    if (!e.Row.VendorID.HasValue || !e.Row.IsDefault.GetValueOrDefault() || this.IsStockItemFlag && !e.Row.SubItemID.HasValue)
      return;
    this.GetCurySettings(e.Row.InventoryID);
    IEnumerable<InventoryItemCurySettings> itemCurySettingses = GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
    {
      (object) e.Row.InventoryID
    }));
    PX.Objects.AP.Vendor vendor1 = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, e.Row.VendorID);
    foreach (InventoryItemCurySettings itemCurySettings1 in itemCurySettingses)
    {
      PX.Objects.AP.Vendor vendor2 = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, itemCurySettings1.PreferredVendorID);
      if ((vendor1.BaseCuryID == null ? 1 : (string.Equals(itemCurySettings1.CuryID, vendor1.BaseCuryID, StringComparison.OrdinalIgnoreCase) ? 1 : 0)) != 0)
      {
        InventoryItemCurySettings itemCurySettings2 = itemCurySettings1;
        bool? isDefault = e.Row.IsDefault;
        int? nullable1 = isDefault.GetValueOrDefault() ? e.Row.VendorID : new int?();
        itemCurySettings2.PreferredVendorID = nullable1;
        InventoryItemCurySettings itemCurySettings3 = itemCurySettings1;
        isDefault = e.Row.IsDefault;
        int? nullable2 = isDefault.GetValueOrDefault() ? e.Row.VendorLocationID : new int?();
        itemCurySettings3.PreferredVendorLocationID = nullable2;
        ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings1);
      }
      else if (vendor2 != null && vendor2.BaseCuryID == null)
      {
        itemCurySettings1.PreferredVendorID = new int?();
        itemCurySettings1.PreferredVendorLocationID = new int?();
        ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings1);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POVendorInventory> e)
  {
    if (e.OldRow == null || e.Row == null || this.IsStockItemFlag && !e.Row.SubItemID.HasValue)
      return;
    this.GetCurySettings(e.Row.InventoryID);
    IEnumerable<InventoryItemCurySettings> itemCurySettingses = GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
    {
      (object) e.Row.InventoryID
    }));
    PX.Objects.AP.Vendor vendor1 = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, e.Row.VendorID);
    bool flag = false;
    foreach (InventoryItemCurySettings itemCurySettings1 in itemCurySettingses)
    {
      PX.Objects.AP.Vendor vendor2 = PX.Objects.AP.Vendor.PK.Find((PXGraph) this, itemCurySettings1.PreferredVendorID);
      int? nullable1;
      if ((vendor1 == null || vendor1.BaseCuryID == null ? 1 : (string.Equals(itemCurySettings1.CuryID, vendor1.BaseCuryID, StringComparison.OrdinalIgnoreCase) ? 1 : 0)) != 0)
      {
        bool? isDefault = e.Row.IsDefault;
        int? nullable2;
        if (isDefault.GetValueOrDefault())
        {
          nullable1 = itemCurySettings1.PreferredVendorID;
          nullable2 = e.Row.VendorID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = itemCurySettings1.PreferredVendorLocationID;
            nullable1 = e.Row.VendorLocationID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              goto label_11;
          }
          else
            goto label_11;
        }
        isDefault = e.Row.IsDefault;
        if (!isDefault.GetValueOrDefault())
        {
          nullable1 = itemCurySettings1.PreferredVendorID;
          nullable2 = e.Row.VendorID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = itemCurySettings1.PreferredVendorLocationID;
            nullable1 = e.Row.VendorLocationID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              continue;
          }
          else
            continue;
        }
        else
          continue;
label_11:
        InventoryItemCurySettings itemCurySettings2 = itemCurySettings1;
        isDefault = e.Row.IsDefault;
        int? nullable3;
        if (!isDefault.GetValueOrDefault())
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = e.Row.VendorID;
        itemCurySettings2.PreferredVendorID = nullable3;
        InventoryItemCurySettings itemCurySettings3 = itemCurySettings1;
        isDefault = e.Row.IsDefault;
        int? nullable4;
        if (!isDefault.GetValueOrDefault())
        {
          nullable1 = new int?();
          nullable4 = nullable1;
        }
        else
          nullable4 = e.Row.VendorLocationID;
        itemCurySettings3.PreferredVendorLocationID = nullable4;
        ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings1);
        flag = true;
      }
      else if (vendor2 != null && vendor2.BaseCuryID == null)
      {
        InventoryItemCurySettings itemCurySettings4 = itemCurySettings1;
        nullable1 = new int?();
        int? nullable5 = nullable1;
        itemCurySettings4.PreferredVendorID = nullable5;
        InventoryItemCurySettings itemCurySettings5 = itemCurySettings1;
        nullable1 = new int?();
        int? nullable6 = nullable1;
        itemCurySettings5.PreferredVendorLocationID = nullable6;
        ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings1);
      }
    }
    if (!(e.Row.IsDefault.GetValueOrDefault() & flag))
      return;
    foreach (PXResult<POVendorInventory> pxResult in ((PXSelectBase<POVendorInventory>) this.VendorItems).Select(Array.Empty<object>()))
    {
      POVendorInventory poVendorInventory = PXResult<POVendorInventory>.op_Implicit(pxResult);
      int? recordId1 = poVendorInventory.RecordID;
      int? recordId2 = e.Row.RecordID;
      if (!(recordId1.GetValueOrDefault() == recordId2.GetValueOrDefault() & recordId1.HasValue == recordId2.HasValue) && poVendorInventory.IsDefault.GetValueOrDefault())
        ((PXSelectBase) this.VendorItems).Cache.SetValue<POVendorInventory.isDefault>((object) poVendorInventory, (object) false);
    }
    ((PXSelectBase) this.VendorItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.VendorItems).View.RequestRefresh();
  }

  public virtual void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      INItemClass inItemClass = (INItemClass) null;
      if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
      {
        int? itemClassId = ((PXSelectBase<InventoryItem>) this.Item).Current.ItemClassID;
        int num = 0;
        if (itemClassId.GetValueOrDefault() < num & itemClassId.HasValue)
        {
          inItemClass = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) this.ItemClass).Select(Array.Empty<object>()));
          INItemClassMaint instance = PXGraph.CreateInstance<INItemClassMaint>();
          INItemClass copy = (INItemClass) ((PXSelectBase) instance.itemclass).Cache.CreateCopy((object) inItemClass);
          ((PXSelectBase<INItemClass>) instance.itemclass).Insert(copy);
          ((PXGraph) instance).Actions.PressSave();
          foreach (object obj in ((PXSelectBase) this.ItemClass).Cache.Inserted)
            ((PXSelectBase) this.ItemClass).Cache.SetStatus(obj, (PXEntryStatus) 5);
          ((PXSelectBase<InventoryItem>) this.Item).Current.ItemClassID = ((PXSelectBase<INItemClass>) instance.itemclass).Current.ItemClassID;
        }
      }
      try
      {
        ((PXGraph) this).Persist();
      }
      catch
      {
        if (inItemClass != null)
        {
          ((PXSelectBase<InventoryItem>) this.Item).Current.ItemClassID = inItemClass.ItemClassID;
          ((PXSelectBase) this.ItemClass).Cache.SetStatus((object) inItemClass, (PXEntryStatus) 2);
        }
        throw;
      }
      ((PXSelectBase) this.ItemClass).Cache.Clear();
      transactionScope.Complete();
    }
  }

  protected Decimal GetRemainingPercentage() => Math.Max(0M, 100M - this.SumComponentsPercentage());

  protected Decimal SumComponentsPercentage()
  {
    return GraphHelper.RowCast<INComponent>((IEnumerable) ((PXSelectBase<INComponent>) this.Components).Select(Array.Empty<object>())).Where<INComponent>((Func<INComponent, bool>) (c => c.AmtOption == "P")).Sum<INComponent>((Func<INComponent, Decimal>) (c => c.Percentage.GetValueOrDefault()));
  }

  public bool AlwaysFromBaseCurrency
  {
    get
    {
      bool fromBaseCurrency = false;
      ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (arSetup != null)
        fromBaseCurrency = arSetup.AlwaysFromBaseCury.GetValueOrDefault();
      return fromBaseCurrency;
    }
  }

  public string SalesPriceUpdateUnit => "B";

  protected virtual void ResetConversionsSettings(PXCache cache, InventoryItem item)
  {
    cache.SetValueExt<InventoryItem.baseUnit>((object) item, (object) null);
    cache.SetValue<InventoryItem.salesUnit>((object) item, (object) null);
    cache.SetValue<InventoryItem.purchaseUnit>((object) item, (object) null);
    cache.SetDefaultExt<InventoryItem.baseUnit>((object) item);
    cache.SetDefaultExt<InventoryItem.salesUnit>((object) item);
    cache.SetDefaultExt<InventoryItem.purchaseUnit>((object) item);
    cache.SetDefaultExt<InventoryItem.decimalBaseUnit>((object) item);
    cache.SetDefaultExt<InventoryItem.decimalSalesUnit>((object) item);
    cache.SetDefaultExt<InventoryItem.decimalPurchaseUnit>((object) item);
  }

  protected virtual void SetDefaultSiteID(InventoryItem item, bool allCurrencies = true)
  {
    if (!this.DefaultSiteFromItemClass || item == null)
      return;
    List<INItemClassCurySettings> itemClassCurySettingsRows = GraphHelper.RowCast<INItemClassCurySettings>((IEnumerable) PXSelectBase<INItemClassCurySettings, PXViewOf<INItemClassCurySettings>.BasedOn<SelectFromBase<INItemClassCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClassCurySettings.itemClassID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, Equal<True>>>>>.Or<BqlOperand<INItemClassCurySettings.curyID, IBqlString>.IsEqual<BqlField<AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) item.ParentItemClassID,
      (object) allCurrencies
    })).ToList<INItemClassCurySettings>();
    if (!itemClassCurySettingsRows.Any<INItemClassCurySettings>((Func<INItemClassCurySettings, bool>) (i => string.Equals(i.CuryID, ((PXGraph) this).Accessinfo.BaseCuryID, StringComparison.OrdinalIgnoreCase))) && ((PXGraph) this).Accessinfo.BaseCuryID != null)
      itemClassCurySettingsRows.Add(new INItemClassCurySettings()
      {
        CuryID = ((PXGraph) this).Accessinfo.BaseCuryID
      });
    IEnumerable<InventoryItemCurySettings> source;
    if (allCurrencies)
      source = GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) this.AllItemCurySettings).Select(new object[1]
      {
        (object) item.InventoryID
      }));
    else
      source = (IEnumerable<InventoryItemCurySettings>) new InventoryItemCurySettings[1]
      {
        this.GetCurySettings(item.InventoryID)
      };
    foreach (INItemClassCurySettings classCurySettings in itemClassCurySettingsRows)
    {
      INItemClassCurySettings itemClassCurySettings = classCurySettings;
      InventoryItemCurySettings itemCurySettings = source.FirstOrDefault<InventoryItemCurySettings>((Func<InventoryItemCurySettings, bool>) (i => string.Equals(i.CuryID, itemClassCurySettings.CuryID, StringComparison.OrdinalIgnoreCase)));
      if (itemCurySettings == null)
        itemCurySettings = ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Insert(new InventoryItemCurySettings()
        {
          InventoryID = item.InventoryID,
          CuryID = itemClassCurySettings.CuryID
        });
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
      {
        itemCurySettings.DfltSiteID = itemClassCurySettings.DfltSiteID;
        try
        {
          ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings);
        }
        catch (PXSetPropertyException ex)
        {
          ((PXSelectBase) this.ItemCurySettings).Cache.RaiseExceptionHandling<InventoryItemCurySettings.dfltSiteID>((object) itemCurySettings, (object) itemClassCurySettings.DfltSiteID, (Exception) ex);
        }
      }
      else
        ((PXSelectBase) this.ItemCurySettings).Cache.RaiseFieldUpdated<InventoryItemCurySettings.dfltSiteID>((object) itemCurySettings, (object) null);
    }
    if (!allCurrencies)
      return;
    foreach (InventoryItemCurySettings itemCurySettings in source.Where<InventoryItemCurySettings>((Func<InventoryItemCurySettings, bool>) (i => !itemClassCurySettingsRows.Any<INItemClassCurySettings>((Func<INItemClassCurySettings, bool>) (c => string.Equals(c.CuryID, i.CuryID, StringComparison.OrdinalIgnoreCase))))))
    {
      itemCurySettings.DfltSiteID = new int?();
      ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings);
    }
  }

  public virtual InventoryItemCurySettings GetCurySettings(int? inventoryID, string curyID = null)
  {
    if (curyID == null)
      curyID = ((PXGraph) this).Accessinfo.BaseCuryID;
    InventoryItemCurySettings curySettings = ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).SelectSingle(new object[2]
    {
      (object) inventoryID,
      (object) curyID
    });
    if (curySettings != null)
      return curySettings;
    return ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Insert(new InventoryItemCurySettings()
    {
      InventoryID = inventoryID,
      CuryID = curyID
    });
  }

  public virtual InventoryItem CopyTemplateItem(
    InventoryItem item,
    InventoryItem templateItem,
    List<string> excludeFields)
  {
    InventoryItem copy = (InventoryItem) ((PXSelectBase) this.Item).Cache.CreateCopy((object) item);
    ((PXSelectBase) this.Item).Cache.RestoreCopy((object) copy, (object) templateItem);
    if (excludeFields != null)
    {
      foreach (string excludeField in excludeFields)
        ((PXSelectBase) this.Item).Cache.SetValue((object) copy, excludeField, ((PXSelectBase) this.Item).Cache.GetValue((object) item, excludeField));
    }
    return copy;
  }

  private void VerifyKitComponentsUOM(INUnit conversion)
  {
    foreach (PXResult<INKitSpecStkDet> pxResult in ((PXSelectBase<INKitSpecStkDet>) this.kitspecs).Select(Array.Empty<object>()))
    {
      INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet>.op_Implicit(pxResult);
      if (inKitSpecStkDet.UOM == conversion.FromUnit)
        throw new PXException("The {0} item's conversion rules cannot be deleted because the item is a component of the {1} kit.", new object[2]
        {
          (object) GetFormattedInventoryCD(((PXSelectBase<InventoryItem>) this.Item).Current),
          (object) GetFormattedInventoryCD((InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecStkDet>.By<INKitSpecStkDet.kitInventoryID>.FindParent((PXGraph) this, (INKitSpecStkDet.kitInventoryID) inKitSpecStkDet, (PKFindOptions) 0))
        });
    }
    foreach (PXResult<INKitSpecNonStkDet> pxResult in ((PXSelectBase<INKitSpecNonStkDet>) this.kitnonstockdet).Select(Array.Empty<object>()))
    {
      INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INKitSpecNonStkDet>.op_Implicit(pxResult);
      if (kitSpecNonStkDet.UOM == conversion.FromUnit)
        throw new PXException("The {0} item's conversion rules cannot be deleted because the item is a component of the {1} kit.", new object[2]
        {
          (object) GetFormattedInventoryCD(((PXSelectBase<InventoryItem>) this.Item).Current),
          (object) GetFormattedInventoryCD((InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecNonStkDet>.By<INKitSpecNonStkDet.kitInventoryID>.FindParent((PXGraph) this, (INKitSpecNonStkDet.kitInventoryID) kitSpecNonStkDet, (PKFindOptions) 0))
        });
    }

    string GetFormattedInventoryCD(InventoryItem item)
    {
      return ((PXCache) GraphHelper.Caches<InventoryItem>((PXGraph) this)).GetStateExt<InventoryItem.inventoryCD>((object) item)?.ToString();
    }
  }

  public static void CheckSameTermOnAllComponents(
    PXCache compCache,
    IEnumerable<INComponent> components)
  {
    IEnumerable<INComponent> source = components.Where<INComponent>((Func<INComponent, bool>) (c => c.OverrideDefaultTerm.GetValueOrDefault()));
    if (source.GroupBy(c => new
    {
      OverrideDefaultTerm = c.OverrideDefaultTerm,
      DefaultTerm = c.DefaultTerm,
      DefaultTermUOM = c.DefaultTermUOM
    }).Count<IGrouping<\u003C\u003Ef__AnonymousType64<bool?, Decimal?, string>, INComponent>>() > 1)
    {
      INComponent inComponent = source.First<INComponent>();
      compCache.RaiseExceptionHandling<INComponent.defaultTerm>((object) inComponent, (object) inComponent.DefaultTerm, (Exception) new PXSetPropertyException<INComponent.defaultTerm>("The default term must be the same for all components with flexible deferral codes for which the Override Default Term check box is selected."));
      throw new PXRowPersistingException(typeof (INComponent.defaultTerm).Name, (object) inComponent.DefaultTerm, "The default term must be the same for all components with flexible deferral codes for which the Override Default Term check box is selected.");
    }
  }

  public static void VerifyComponentPercentages(
    PXCache itemCache,
    InventoryItem item,
    IEnumerable<INComponent> components)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
      return;
    bool flag = components.Any<INComponent>((Func<INComponent, bool>) (c => c.AmtOption == "R"));
    Decimal? totalPercentage1 = item.TotalPercentage;
    Decimal num1 = (Decimal) 100;
    if (!(totalPercentage1.GetValueOrDefault() == num1 & totalPercentage1.HasValue) && !flag)
    {
      if (itemCache.RaiseExceptionHandling<InventoryItem.totalPercentage>((object) item, (object) item.TotalPercentage, (Exception) new PXSetPropertyException("Total Percentage for Components must be 100. Please correct the percentage split for the components.")))
        throw new PXRowPersistingException(typeof (InventoryItem.totalPercentage).Name, (object) item.TotalPercentage, "Total Percentage for Components must be 100. Please correct the percentage split for the components.");
    }
    else
    {
      Decimal? totalPercentage2 = item.TotalPercentage;
      Decimal num2 = (Decimal) 100;
      if (totalPercentage2.GetValueOrDefault() >= num2 & totalPercentage2.HasValue && flag && itemCache.RaiseExceptionHandling<InventoryItem.totalPercentage>((object) item, (object) item.TotalPercentage, (Exception) new PXSetPropertyException("Total Percentage for Components must be less than 100 when there is a component with 'Residual' allocation method. Please correct the percentage split for the components.")))
        throw new PXRowPersistingException(typeof (InventoryItem.totalPercentage).Name, (object) item.TotalPercentage, "Total Percentage for Components must be less than 100 when there is a component with 'Residual' allocation method. Please correct the percentage split for the components.");
    }
  }

  public static void VerifyOnlyOneResidualComponent(
    PXCache compCache,
    IEnumerable<INComponent> components)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
    {
      IEnumerable<INComponent> source = components.Where<INComponent>((Func<INComponent, bool>) (c => c.AmtOptionASC606 == "R"));
      if (source.Count<INComponent>() > 1)
      {
        compCache.RaiseExceptionHandling<INComponent.amtOptionASC606>((object) source.First<INComponent>(), (object) source.First<INComponent>().AmtOptionASC606, (Exception) new PXSetPropertyException("There must be only one component with 'Residual' allocation method for an item."));
        throw new PXRowPersistingException(typeof (INComponent.amtOptionASC606).Name, (object) source.First<INComponent>().AmtOptionASC606, "There must be only one component with 'Residual' allocation method for an item.");
      }
    }
    else
    {
      IEnumerable<INComponent> source = components.Where<INComponent>((Func<INComponent, bool>) (c => c.AmtOption == "R"));
      if (source.Count<INComponent>() > 1)
      {
        compCache.RaiseExceptionHandling<INComponent.amtOption>((object) source.First<INComponent>(), (object) source.First<INComponent>().AmtOption, (Exception) new PXSetPropertyException("There must be only one component with 'Residual' allocation method for an item."));
        throw new PXRowPersistingException(typeof (INComponent.amtOption).Name, (object) source.First<INComponent>().AmtOption, "There must be only one component with 'Residual' allocation method for an item.");
      }
    }
  }

  public static void SetDefaultTermControlsState(PXCache cache, InventoryItem item)
  {
    if (item == null)
      return;
    bool flag = DeferredMethodType.RequiresTerms((DRDeferredCode) PXSelectorAttribute.Select<InventoryItem.deferredCode>(cache, (object) item));
    PXUIFieldAttribute.SetEnabled<InventoryItem.defaultTerm>(cache, (object) item, flag);
    PXUIFieldAttribute.SetEnabled<InventoryItem.defaultTermUOM>(cache, (object) item, flag);
  }

  private IEnumerable GetEntityItems(string parent)
  {
    InventoryItemMaintBase inventoryItemMaintBase = this;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(inventoryItemMaintBase.IsStockItemFlag ? "IN202500" : "IN202000");
    if (mapNodeByScreenId != null)
    {
      foreach (object entityItem in EMailSourceHelper.TemplateEntity((PXGraph) inventoryItemMaintBase, parent, (string) null, mapNodeByScreenId.GraphType, true))
        yield return entityItem;
    }
  }

  private IEnumerable<INCategory> GetCategories(int? categoryID)
  {
    InventoryItemMaintBase inventoryItemMaintBase1 = this;
    if (!categoryID.HasValue)
      yield return new INCategory()
      {
        CategoryID = new int?(0),
        Description = PXSiteMap.RootNode.Title
      };
    InventoryItemMaintBase inventoryItemMaintBase2 = inventoryItemMaintBase1;
    object[] objArray = new object[1]{ (object) categoryID };
    foreach (PXResult<INCategory> pxResult in PXSelectBase<INCategory, PXViewOf<INCategory>.BasedOn<SelectFromBase<INCategory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCategory.parentID, Equal<P.AsInt>>>>, And<BqlOperand<INCategory.description, IBqlString>.IsNotEqual<Empty>>>>.And<BqlOperand<INCategory.description, IBqlString>.IsNotNull>>.Order<PX.Data.BQL.Fluent.By<BqlField<INCategory.sortOrder, IBqlInt>.Asc>>>.Config>.Select((PXGraph) inventoryItemMaintBase2, objArray))
      yield return PXResult<INCategory>.op_Implicit(pxResult);
  }

  public static class ActionCategories
  {
    public const string PricesCategoryID = "Prices Category";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Prices = "Prices";
    }
  }

  public class DefaultKitRevisionID : 
    PXFieldAttachedTo<InventoryItem>.By<InventoryItemMaintBase>.AsString.Named<InventoryItemMaintBase.DefaultKitRevisionID>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>();

    public override string GetValue(InventoryItem Row)
    {
      return Row.With<InventoryItem, INKitSpecHdr>(new Func<InventoryItem, INKitSpecHdr>(this.GetLatestKitRevision)).With<INKitSpecHdr, string>((Func<INKitSpecHdr, string>) (kit => kit.RevisionID));
    }

    protected virtual INKitSpecHdr GetLatestKitRevision(InventoryItem item)
    {
      return PXResultset<INKitSpecHdr>.op_Implicit(PXSelectBase<INKitSpecHdr, PXViewOf<INKitSpecHdr>.BasedOn<SelectFromBase<INKitSpecHdr, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecHdr.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INKitSpecHdr.isActive, IBqlBool>.IsEqual<True>>>.Order<PX.Data.BQL.Fluent.By<BqlField<INKitSpecHdr.lastModifiedDateTime, IBqlDateTime>.Desc>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) item.InventoryID
      }));
    }
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string CannotDeleteUnitSinceItIsReservedByKitComponent = "The {0} item's conversion rules cannot be deleted because the item is a component of the {1} kit.";
  }
}
