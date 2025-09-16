// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions.INItemClassMaintExt;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.Matrix.Utility;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN;

public class INItemClassMaint : PXGraph<
#nullable disable
INItemClassMaint, INItemClass>, ICreateMatrixHelperFactory
{
  [PXHidden]
  public FbqlSelect<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>, INLotSerClass>.View lotSerClass;
  public PXSetup<INSetup> inSetup;
  public FbqlSelect<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>, INItemClass>.View itemclass;
  public FbqlSelect<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  INItemClass>.View itemclasssettings;
  public FbqlSelect<SelectFromBase<INItemClassCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemClassCurySettings.itemClassID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INItemClassCurySettings.curyID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INItemClassCurySettings>.View itemclasscurysettings;
  public FbqlSelect<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  INItemClass>.View TreeViewAndPrimaryViewSynchronizationHelper;
  public PXSetup<INLotSerClass>.Where<BqlOperand<
  #nullable enable
  INLotSerClass.lotSerClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.lotSerClassID, IBqlString>.FromCurrent>> lotserclass;
  public 
  #nullable disable
  CSAttributeGroupList<INItemClass.itemClassID, InventoryItem> Mapping;
  public FbqlSelect<SelectFromBase<INItemClassRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemClassRep.curyID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  AccessInfo.baseCuryID, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INItemClassRep.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.AsOptional>>>, 
  #nullable disable
  INItemClassRep>.View replenishment;
  public FbqlSelect<SelectFromBase<SegmentValue, TypeArrayOf<IFbqlJoin>.Empty>, SegmentValue>.View segmentvalue;
  public FbqlSelect<SelectFromBase<RelationGroup, TypeArrayOf<IFbqlJoin>.Empty>, RelationGroup>.View Groups;
  [PXCopyPasteHiddenView]
  public PXViewOf<ItemClassTree.INItemClass>.BasedOn<SelectFromBase<ItemClassTree.INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Order<PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  ItemClassTree.INItemClass.itemClassCD, IBqlString>.Asc>>>.ReadOnly ItemClassNodes;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<InventoryItem.FK.ItemClass>, InventoryItem>.View Items;
  public PXFilter<INItemClassMaint.GoTo> goTo;
  public INItemClassMaint.ImmediatelyChangeID ChangeID;
  public PXAction<INItemClass> action;
  public PXAction<INItemClass> viewGroupDetails;
  public PXAction<INItemClass> viewRestrictionGroups;
  public PXAction<INItemClass> resetGroup;
  public PXAction<INItemClass> applyToChildren;
  public PXAction<INItemClass> GoToNodeSelectedInTree;
  private readonly Lazy<bool> _timestampSelected = new Lazy<bool>((Func<bool>) (() =>
  {
    PXDatabase.SelectTimeStamp();
    return true;
  }));
  private int? initialTreeNodeID;
  private bool _allowToSyncTreeCurrentWithPrimaryViewCurrent;
  private bool _forbidToSyncTreeCurrentWithPrimaryViewCurrent;

  [PXMergeAttributes]
  [INParentItemClass(true, InsertCurySettings = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemClass.parentItemClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXDimensionSelector("INITEMCLASS", typeof (SearchFor<INItemClass.itemClassCD>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.stkItem, Equal<True>>>>>.And<FeatureInstalled<FeaturesSet.distributionModule>>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  protected virtual void _(PX.Data.Events.CacheAttached<INItemClass.itemClassCD> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (RelationGroup.ModuleAllAttribute))]
  [PXMergeAttributes]
  [PXStringList(new string[] {"PX.Objects.CS.SegmentValue", "PX.Objects.IN.InventoryItem"}, new string[] {"Subitem", "Inventory Item Restriction"})]
  protected virtual void _(PX.Data.Events.CacheAttached<RelationGroup.specificType> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INVENTORY", typeof (InventoryItem.inventoryCD), typeof (InventoryItem.inventoryCD))]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.inventoryCD> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXUIRequiredAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.itemClassID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItem.lotSerClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<INItemClass, Where<INItemClass.itemClassID, Equal<Current<CSAttributeGroup.entityClassID>>>>), LeaveChildren = true)]
  [PXDBDefault(typeof (INItemClass.itemClassStrID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CSAttributeGroup.entityClassID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (INUnitAttribute), "Visible", false)]
  [PXCustomizeBaseAttribute(typeof (INUnitAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INUnit.toUnit> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INUnit.sampleToUnit> e)
  {
  }

  protected virtual IEnumerable treeViewAndPrimaryViewSynchronizationHelper()
  {
    this._allowToSyncTreeCurrentWithPrimaryViewCurrent = true;
    yield return (object) ((PXSelectBase<INItemClass>) this.itemclass).Current;
  }

  protected IEnumerable groups() => (IEnumerable) this.GetRelationGroups();

  protected virtual IEnumerable itemClassNodes([PXInt] int? itemClassID)
  {
    return (IEnumerable) DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.EnrollNodes(itemClassID);
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
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

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemClass>) this.itemclass).Current != null)
    {
      INAccessDetailByClass instance = PXGraph.CreateInstance<INAccessDetailByClass>();
      ((PXSelectBase<INItemClass>) instance.Class).Current = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) instance.Class).Search<INItemClass.itemClassID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.ItemClassID, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  [PXProcessButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Apply Restriction Settings to All Inventory Items")]
  protected virtual IEnumerable ResetGroup(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemClass>) this.itemclass).Ask("Warning", "Restriction Groups will be reset for all Items that belongs to this item class.  This might override the custom settings. Please confirm your action", (MessageButtons) 1) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      INItemClassMaint.\u003C\u003Ec__DisplayClass34_0 cDisplayClass340 = new INItemClassMaint.\u003C\u003Ec__DisplayClass34_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass340.classID = ((PXSelectBase<INItemClass>) this.itemclass).Current.ItemClassID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass340, __methodptr(\u003CResetGroup\u003Eb__0)));
    }
    return adapter.Get();
  }

  protected static void Reset(int? classID)
  {
    INItemClassMaint instance = PXGraph.CreateInstance<INItemClassMaint>();
    INItemClass inItemClass = ((PXSelectBase<INItemClass>) instance.itemclass).Current = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) instance.itemclass).Search<INItemClass.itemClassID>((object) classID, Array.Empty<object>()));
    if (inItemClass == null)
      return;
    PXDatabase.Update<InventoryItem>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.itemClassID>((object) inItemClass.ItemClassID),
      (PXDataFieldParam) new PXDataFieldAssign<InventoryItem.groupMask>((object) inItemClass.GroupMask)
    });
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ApplyToChildren(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemClass>) this.itemclass).Current != null && ((PXSelectBase<INItemClass>) this.itemclass).Ask("Confirmation", "The settings of this item class will be assigned to its child item classes, which might override the custom settings. Please confirm your action.", (MessageButtons) 1) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      INItemClassMaint.\u003C\u003Ec__DisplayClass37_0 cDisplayClass370 = new INItemClassMaint.\u003C\u003Ec__DisplayClass37_0();
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass370.itemclassID = ((PXSelectBase<INItemClass>) this.itemclass).Current.ItemClassID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass370, __methodptr(\u003CApplyToChildren\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXDeleteButton]
  [PXUIField]
  protected virtual IEnumerable delete(PXAdapter adapter)
  {
    INItemClassMaint inItemClassMaint = this;
    if (((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current != null)
    {
      ItemClassTree instance1 = DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.Instance;
      int? itemClassId = ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current.ItemClassID;
      ItemClassTree.INItemClass inItemClass1 = instance1.GetParentsOf(itemClassId.Value).FirstOrDefault<ItemClassTree.INItemClass>();
      itemClassId = ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current.ItemClassID;
      IEnumerable<INItemClass> allChildrenOf = (IEnumerable<INItemClass>) instance1.GetAllChildrenOf(itemClassId.Value);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (allChildrenOf.Any<INItemClass>() && ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Ask("Confirmation", "The item class that you want to delete has child item classes. Would you like to keep the child item classes? (If you keep the child item classes, they will become children of the item class at the level immediately above the deleted class.)", (MessageButtons) 4) == 7)
        {
          INItemClassMaint instance2 = PXGraph.CreateInstance<INItemClassMaint>();
          foreach (INItemClass inItemClass2 in allChildrenOf)
          {
            ((PXSelectBase<INItemClass>) instance2.itemclass).Current = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) instance2.itemclass).Search<INItemClass.itemClassID>((object) inItemClass2.ItemClassID, Array.Empty<object>()));
            ((PXSelectBase<INItemClass>) instance2.itemclass).Delete(((PXSelectBase<INItemClass>) instance2.itemclass).Current);
          }
          ((PXGraph) instance2).Actions.PressSave();
        }
        ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Delete(((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current);
        ((PXGraph) inItemClassMaint).Actions.PressSave();
        transactionScope.Complete();
      }
      if (inItemClass1 == null)
      {
        if (((PXSelectBase) inItemClassMaint.itemclass).AllowInsert)
        {
          ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Insert();
          ((PXSelectBase) inItemClassMaint.itemclass).Cache.IsDirty = false;
        }
      }
      else
        ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current = (INItemClass) inItemClass1;
      ((PXGraph) inItemClassMaint).SelectTimeStamp();
      yield return (object) ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current;
    }
    else
    {
      foreach (object obj in adapter.Get())
        yield return obj;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable goToNodeSelectedInTree(PXAdapter adapter)
  {
    if (((PXSelectBase) this.itemclass).Cache.IsDirty && ((PXSelectBase) this.itemclass).View.Answer == null)
      ((PXSelectBase<INItemClassMaint.GoTo>) this.goTo).Current.ItemClassID = (int?) ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current?.ItemClassID;
    if (!((PXSelectBase) this.itemclass).Cache.IsDirty || ((PXSelectBase<INItemClass>) this.itemclass).AskExt() == 1)
    {
      int? itemClassID = ((PXSelectBase) this.itemclass).Cache.IsDirty ? ((PXSelectBase<INItemClassMaint.GoTo>) this.goTo).Current.ItemClassID : ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current?.ItemClassID;
      ((PXGraph) this).Actions.PressCancel();
      this._forbidToSyncTreeCurrentWithPrimaryViewCurrent = true;
      ((PXSelectBase<INItemClass>) this.itemclass).Current = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXViewOf<INItemClass>.BasedOn<SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Search<INItemClass.itemClassID>((PXGraph) this, (object) itemClassID, Array.Empty<object>()));
      this.SetTreeCurrent(itemClassID);
    }
    else
      this.SetTreeCurrent(((PXSelectBase<INItemClass>) this.itemclass).Current.ItemClassID);
    return (IEnumerable) new INItemClass[1]
    {
      ((PXSelectBase<INItemClass>) this.itemclass).Current
    };
  }

  public INItemClassMaint()
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Groups).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RelationGroup.included>(((PXSelectBase) this.Groups).Cache, (object) null, true);
    if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      return;
    ((PXAction) this.action).SetVisible(false);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.replenishment).Cache, (string) null, false);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.itemclass).Cache, (string) null, false);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Groups).Cache, (string) null, false);
    PXUIFieldAttribute.SetVisible<INItemClass.itemClassCD>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INItemClass.descr>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INItemClass.itemType>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INItemClass.taxCategoryID>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INItemClass.baseUnit>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<INItemClass.priceClassID>(((PXSelectBase) this.itemclass).Cache, (object) null, true);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    INItemClassMaint.Configure(config.GetScreenConfigurationContext<INItemClassMaint, INItemClass>());
  }

  protected static void Configure(
    WorkflowContext<INItemClassMaint, INItemClass> context)
  {
    BoundedTo<INItemClassMaint, INItemClass>.ActionCategory.IConfigured otherCategory = CommonActionCategories.Get<INItemClassMaint, INItemClass>(context).Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<INItemClassMaint, INItemClass>.ScreenConfiguration.IStartConfigScreen, BoundedTo<INItemClassMaint, INItemClass>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<INItemClassMaint, INItemClass>.ScreenConfiguration.IConfigured) ((BoundedTo<INItemClassMaint, INItemClass>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((System.Action<BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<INItemClassMaint, PXAction<INItemClass>>>) (g => g.applyToChildren), (Func<BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured>) (a => (BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<INItemClassMaint, PXAction<INItemClass>>>) (g => g.resetGroup), (Func<BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured>) (a => (BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<INItemClassMaint, PXAction<INItemClass>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured>) (a => (BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<INItemClassMaint, PXAction<INItemClass>>>) (g => g.ChangeID), (Func<BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured>) (a => (BoundedTo<INItemClassMaint, INItemClass>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
    })).WithCategories((System.Action<BoundedTo<INItemClassMaint, INItemClass>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(otherCategory)))));
  }

  protected virtual void _(PX.Data.Events.RowSelected<ItemClassTree.INItemClass> e)
  {
    if (e.Row == null)
      return;
    if (!this.initialTreeNodeID.HasValue)
      this.initialTreeNodeID = e.Row.ItemClassID;
    int? initialTreeNodeId = this.initialTreeNodeID;
    int? itemClassId = e.Row.ItemClassID;
    if (initialTreeNodeId.GetValueOrDefault() == itemClassId.GetValueOrDefault() & initialTreeNodeId.HasValue == itemClassId.HasValue)
      return;
    this._forbidToSyncTreeCurrentWithPrimaryViewCurrent = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemClass> e)
  {
    bool flag = e.Row == null || e.Row.StkItem.GetValueOrDefault();
    INItemTypes.CustomListAttribute customListAttribute1 = flag ? (INItemTypes.CustomListAttribute) new INItemTypes.StockListAttribute() : (INItemTypes.CustomListAttribute) new INItemTypes.NonStockListAttribute();
    CommodityCodeTypes.CustomListAttribute customListAttribute2 = flag ? (CommodityCodeTypes.CustomListAttribute) new CommodityCodeTypes.StockCommodityCodeListAttribute() : (CommodityCodeTypes.CustomListAttribute) new CommodityCodeTypes.NonStockCommodityCodeListAttribute();
    PXUIFieldAttribute.SetVisible<INItemClass.taxCalcMode>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, e.Row.ItemType == "E");
    this._timestampSelected.Init<bool>();
    PXStringListAttribute.SetList<INItemClass.itemType>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, customListAttribute1.AllowedValues, customListAttribute1.AllowedLabels);
    PXStringListAttribute.SetList<INItemClass.commodityCodeType>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, customListAttribute2.AllowedValues, customListAttribute2.AllowedLabels);
    PXUIFieldAttribute.SetEnabled<INItemClass.stkItem>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, !this.IsDefaultItemClass(e.Row));
    PXDefaultAttribute.SetPersistingCheck<INItemClass.availabilitySchemeID>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<INItemClass.availabilitySchemeID>(((PXSelectBase) this.itemclass).Cache, flag);
    PXUIFieldAttribute.SetEnabled<INItemClass.availabilitySchemeID>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<INItemClass.lotSerClassID>(((PXSelectBase) this.itemclass).Cache, (object) e.Row, flag);
    this.SyncTreeCurrentWithPrimaryViewCurrent(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<INItemClass> e)
  {
    if (e.Row != null && (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.inventory>()))
      ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<INItemClass>>) e).Cache.SetValueExt<INItemClass.stkItem>((object) e.Row, (object) false);
    ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current = (ItemClassTree.INItemClass) null;
    ((PXSelectBase) this.ItemClassNodes).Cache.ActiveRow = (IBqlTable) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INItemClass, INItemClass.stkItem> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INItemClass, INItemClass.stkItem>, INItemClass, object>) e).NewValue;
    bool? stkItem = e.Row.StkItem;
    bool? nullable = newValue;
    if (stkItem.GetValueOrDefault() == nullable.GetValueOrDefault() & stkItem.HasValue == nullable.HasValue)
      return;
    InventoryItem firstItem = this.GetFirstItem(e.Row);
    if (firstItem != null)
      throw new PXSetPropertyException<INItemClass.stkItem>("The value of the Stock Item check box cannot be changed because the item class is assigned to the {0} item. Select another item class for this item first.", new object[1]
      {
        (object) firstItem.InventoryCD
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.parentItemClassID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.itemType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.valMethod>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.postToExpenseAccount>((object) e.Row);
    if (!e.Row.StkItem.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetValueExt<INItemClass.availabilitySchemeID>((object) e.Row, (object) null);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetValueExt<INItemClass.lotSerClassID>((object) e.Row, (object) null);
    }
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.GetStatus((object) e.Row) != 2)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.negQty>((object) e.Row);
    bool? stkItem = e.Row.StkItem;
    if (stkItem.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.availabilitySchemeID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetValueExt<INItemClass.baseUnit>((object) e.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetValue<INItemClass.salesUnit>((object) e.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetValue<INItemClass.purchaseUnit>((object) e.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.baseUnit>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.salesUnit>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.purchaseUnit>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.postClassID>((object) e.Row);
    stkItem = e.Row.StkItem;
    if (stkItem.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.lotSerClassID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.taxCategoryID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.deferredCode>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.priceClassID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.priceWorkgroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.priceManagerID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.minGrossProfitPct>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.markupPct>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.demandCalculation>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<INItemClass.groupMask>((object) e.Row);
    INParentItemClassAttribute itemClassAttribute = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.GetAttributesReadonly<INItemClass.parentItemClassID>().OfType<INParentItemClassAttribute>().FirstOrDefault<INParentItemClassAttribute>();
    if (itemClassAttribute == null)
      throw new PXException("'{0}' '{1}' cannot be found in the system.", new object[2]
      {
        (object) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.DisplayName,
        (object) "INParentItemClassAttribute"
      });
    itemClassAttribute?.CopyCurySettings((PXGraph) this, e.Row);
    this.InitDetailsFromParentItemClass(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemClass> e)
  {
    this.InitDetailsFromParentItemClass(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INItemClass> e)
  {
    InventoryItem inventoryItem = !this.IsDefaultItemClass(e.Row) ? this.GetFirstItem(e.Row) : throw new PXException("This Item Class can not be deleted because it is used in Inventory Setup.");
    if (inventoryItem != null)
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("This Item Class cannot be deleted because it is used for inventory item: {0}.", new object[1]
      {
        (object) inventoryItem.InventoryCD
      }));
    INLocation inLocation = PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INLocation.primaryItemClassID>.IsRelatedTo<INItemClass.itemClassID>.AsSimpleKey.WithTablesOf<INItemClass, INLocation>, INItemClass, INLocation>.SameAsCurrent>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (inLocation != null)
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("This Item Class can not be deleted because it is used in Warehouse/Location: {0}/{1}.", new object[2]
      {
        (object) (INSite.PK.Find((PXGraph) this, inLocation.SiteID).SiteCD ?? ""),
        (object) inLocation.LocationCD
      }));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemClass> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && e.Row.ValMethod == "S" && ((PXSelectBase<INLotSerClass>) this.lotserclass).Current != null && (((PXSelectBase<INLotSerClass>) this.lotserclass).Current.LotSerTrack == "N" || ((PXSelectBase<INLotSerClass>) this.lotserclass).Current.LotSerAssign != "R") && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INItemClass>>) e).Cache.RaiseExceptionHandling<INItemClass.valMethod>((object) e.Row, (object) "S", (Exception) new PXSetPropertyException("Specific valuated items should be lot or serial numbered during receipt.")))
      throw new PXRowPersistingException(typeof (INItemClass.valMethod).Name, (object) "S", "Specific valuated items should be lot or serial numbered during receipt.", new object[1]
      {
        (object) typeof (INItemClass.valMethod).Name
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisted<INItemClass> e)
  {
    if (e.TranStatus != 1 || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 3, (PXDBOperation) 1))
      return;
    ((PXGraph) this).SelectTimeStamp();
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemClassRep> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = e.Row.ReplenishmentSource == "T";
    bool flag2 = e.Row.ReplenishmentMethod == "F";
    PXUIFieldAttribute.SetEnabled<INItemClassRep.replenishmentMethod>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, EnumerableExtensions.IsNotIn<string>(e.Row.ReplenishmentSource, "O", "D"));
    PXUIFieldAttribute.SetEnabled<INItemClassRep.replenishmentSourceSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, EnumerableExtensions.IsIn<string>(e.Row.ReplenishmentSource, "O", "D", "T", "P"));
    PXUIFieldAttribute.SetEnabled<INItemClassRep.transferLeadTime>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<INItemClassRep.transferERQ>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, flag1 & flag2 && e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.forecastModelType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.forecastPeriodType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.historyDepth>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.launchDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.terminationDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXUIFieldAttribute.SetEnabled<INItemClassRep.serviceLevelPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, e.Row.ReplenishmentMethod != "N");
    PXDefaultAttribute.SetPersistingCheck<INItemClassRep.transferLeadTime>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, flag1 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<INItemClassRep.transferERQ>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClassRep>>) e).Cache, (object) e.Row, flag1 & flag2 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemClassRep, INItemClassRep.replenishmentSource> e)
  {
    if (e.Row == null)
      return;
    if (EnumerableExtensions.IsIn<string>(e.Row.ReplenishmentSource, "O", "D"))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClassRep, INItemClassRep.replenishmentSource>>) e).Cache.SetValueExt<INItemClassRep.replenishmentMethod>((object) e.Row, (object) "N");
    if (EnumerableExtensions.IsNotIn<string>(e.Row.ReplenishmentSource, "O", "D", "T"))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClassRep, INItemClassRep.replenishmentSource>>) e).Cache.SetDefaultExt<INItemClassRep.replenishmentSourceSiteID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClassRep, INItemClassRep.replenishmentSource>>) e).Cache.SetDefaultExt<INItemClassRep.transferLeadTime>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<RelationGroup> e)
  {
    if (((PXSelectBase<INItemClass>) this.itemclass).Current == null || e.Row == null || ((PXSelectBase) this.Groups).Cache.GetStatus((object) e.Row) != null)
      return;
    e.Row.Included = new bool?(UserAccess.IsIncluded(((PXSelectBase<INItemClass>) this.itemclass).Current.GroupMask, e.Row));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<RelationGroup> e) => e.Cancel = true;

  public virtual void Persist()
  {
    if (((PXSelectBase<INItemClass>) this.itemclass).Current != null && ((PXSelectBase<INItemClass>) this.itemclass).Current.StkItem.GetValueOrDefault() && string.IsNullOrEmpty(((PXSelectBase<INItemClass>) this.itemclass).Current.LotSerClassID) && !PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
      ((PXSelectBase<INItemClass>) this.itemclass).Current.LotSerClassID = INLotSerClass.GetDefaultLotSerClass((PXGraph) this);
    if (((PXSelectBase<INItemClass>) this.itemclass).Current != null && ((PXSelectBase) this.Groups).Cache.IsDirty)
    {
      UserAccess.PopulateNeighbours<INItemClass>((PXSelectBase<INItemClass>) this.itemclass, (PXSelectBase<RelationGroup>) this.Groups, new System.Type[1]
      {
        typeof (SegmentValue)
      });
      PXSelectorAttribute.ClearGlobalCache<INItemClass>();
    }
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Groups).Cache.Clear();
    GroupHelper.Clear();
  }

  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    if (cacheType == typeof (INUnit) && operation == 1)
      ((PXGraph) this).Persist(cacheType, (PXDBOperation) 2);
    return ((PXGraph) this).Persist(cacheType, operation);
  }

  protected virtual InventoryItem GetFirstItem(INItemClass itemClass)
  {
    if (itemClass == null)
      return (InventoryItem) null;
    return PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) itemClass.ItemClassID
    }));
  }

  protected virtual bool IsDefaultItemClass(INItemClass itemClass)
  {
    INSetup inSetup = PXResultset<INSetup>.op_Implicit(((PXSelectBase<INSetup>) this.inSetup).Select(Array.Empty<object>()));
    return itemClass != null && inSetup != null && EnumerableExtensions.IsIn<int?>(itemClass.ItemClassID, inSetup.DfltNonStkItemClassID, inSetup.DfltStkItemClassID);
  }

  public UnitsOfMeasure UnitsOfMeasureExt => ((PXGraph) this).FindImplementation<UnitsOfMeasure>();

  protected virtual void InitDetailsFromParentItemClass(INItemClass itemClass)
  {
    if (itemClass == null || !itemClass.ParentItemClassID.HasValue)
      return;
    using (new ReadOnlyScope(new PXCache[3]
    {
      ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache,
      ((PXSelectBase) this.replenishment).Cache,
      ((PXSelectBase) this.Mapping).Cache
    }))
    {
      ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.ClearQueryCache();
      ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.Clear();
      foreach (PXResult<INUnit> pxResult in ((PXSelectBase<INUnit>) this.UnitsOfMeasureExt.classunits).Select(new object[1]
      {
        (object) itemClass.ParentItemClassID
      }))
      {
        object copy = ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.CreateCopy((object) PXResult<INUnit>.op_Implicit(pxResult));
        ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.SetValue<INUnit.recordID>(copy, (object) null);
        ((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.SetValue<INUnit.itemClassID>(copy, (object) itemClass.ItemClassID);
        if (((PXSelectBase) this.UnitsOfMeasureExt.classunits).Cache.Insert(copy) == null)
          throw new PXException("Copying settings from the selected item class has completed with errors; some settings have not been copied. Try to select the item class again and save the changes.");
      }
      ((PXSelectBase) this.replenishment).Cache.ClearQueryCache();
      ((PXSelectBase) this.replenishment).Cache.Clear();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXFieldVerifying pxFieldVerifying = INItemClassMaint.\u003C\u003Ec.\u003C\u003E9__67_0 ?? (INItemClassMaint.\u003C\u003Ec.\u003C\u003E9__67_0 = new PXFieldVerifying((object) INItemClassMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitDetailsFromParentItemClass\u003Eb__67_0)));
      ((PXGraph) this).FieldVerifying.AddHandler<INItemClassRep.replenishmentSourceSiteID>(pxFieldVerifying);
      try
      {
        foreach (PXResult<INItemClassRep> pxResult in PXSelectBase<INItemClassRep, PXViewOf<INItemClassRep>.BasedOn<SelectFromBase<INItemClassRep, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClassRep.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) itemClass.ParentItemClassID
        }))
        {
          object copy = ((PXSelectBase) this.replenishment).Cache.CreateCopy((object) PXResult<INItemClassRep>.op_Implicit(pxResult));
          ((PXSelectBase) this.replenishment).Cache.SetValue<INItemClassRep.itemClassID>(copy, (object) itemClass.ItemClassID);
          if (((PXSelectBase) this.replenishment).Cache.Insert(copy) == null)
            throw new PXException("Copying settings from the selected item class has completed with errors; some settings have not been copied. Try to select the item class again and save the changes.");
        }
      }
      finally
      {
        ((PXGraph) this).FieldVerifying.RemoveHandler<INItemClassRep.replenishmentSourceSiteID>(pxFieldVerifying);
      }
      ((PXSelectBase) this.Mapping).Cache.ClearQueryCache();
      ((PXSelectBase) this.Mapping).Cache.Clear();
      foreach (object attributeGroupRecord in this.SelectCSAttributeGroupRecords(itemClass.ParentItemClassID.ToString()))
      {
        object copy = ((PXSelectBase) this.Mapping).Cache.CreateCopy(attributeGroupRecord);
        ((PXSelectBase) this.Mapping).Cache.SetValue<CSAttributeGroup.entityClassID>(copy, (object) itemClass.ItemClassStrID);
        if (((PXSelectBase) this.Mapping).Cache.Insert(copy) == null)
          throw new PXException("Copying settings from the selected item class has completed with errors; some settings have not been copied. Try to select the item class again and save the changes.");
      }
    }
  }

  protected static void UpdateChildren(int? itemClassID)
  {
    INItemClassMaint instance = PXGraph.CreateInstance<INItemClassMaint>();
    ((PXSelectBase<INItemClass>) instance.itemclass).Current = PXResultset<INItemClass>.op_Implicit(((PXSelectBase<INItemClass>) instance.itemclass).Search<INItemClass.itemClassID>((object) itemClassID, Array.Empty<object>()));
    ((PXSelectBase<INItemClassCurySettings>) instance.itemclasscurysettings).Current = PXResultset<INItemClassCurySettings>.op_Implicit(((PXSelectBase<INItemClassCurySettings>) instance.itemclasscurysettings).Select(Array.Empty<object>()));
    if (((PXSelectBase<INItemClass>) instance.itemclass).Current == null)
      return;
    IEnumerable<INItemClass> allChildrenOf = (IEnumerable<INItemClass>) DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.Instance.GetAllChildrenOf(((PXSelectBase<INItemClass>) instance.itemclass).Current.ItemClassID.Value);
    if (!allChildrenOf.Any<INItemClass>())
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      IEnumerable<INItemClassRep> replenishmentTemplate = GraphHelper.RowCast<INItemClassRep>((IEnumerable) ((PXSelectBase<INItemClassRep>) instance.replenishment).Select(Array.Empty<object>()));
      IEnumerable<INUnit> unitConversionsTemplate = GraphHelper.RowCast<INUnit>((IEnumerable) ((PXSelectBase<INUnit>) instance.UnitsOfMeasureExt.classunits).Select(Array.Empty<object>()));
      IEnumerable<CSAttributeGroup> attributesTemplate = GraphHelper.RowCast<CSAttributeGroup>((IEnumerable) instance.Mapping.Select(Array.Empty<object>()));
      foreach (INItemClass child in allChildrenOf)
      {
        instance.UpdateChildItemClass(child);
        instance.MergeReplenishment(child, replenishmentTemplate);
        instance.MergeUnitConversions(child, unitConversionsTemplate);
        instance.MergeAttributes(child, attributesTemplate);
      }
      transactionScope.Complete();
    }
  }

  protected virtual void UpdateChildItemClass(INItemClass child)
  {
    bool? stkItem1 = child.StkItem;
    bool? stkItem2 = ((PXSelectBase<INItemClass>) this.itemclass).Current.StkItem;
    if (!(stkItem1.GetValueOrDefault() == stkItem2.GetValueOrDefault() & stkItem1.HasValue == stkItem2.HasValue))
    {
      InventoryItem firstItem = this.GetFirstItem(child);
      if (firstItem != null)
        throw new PXSetPropertyException<INItemClass.stkItem>("The value of the Stock Item check box cannot be changed for the {0} child item class because the item class is assigned to the {1} item. Select another item class for this item first.", new object[2]
        {
          (object) child.ItemClassCD,
          (object) firstItem.InventoryCD
        });
    }
    if (child.BaseUnit != ((PXSelectBase<INItemClass>) this.itemclass).Current.BaseUnit)
    {
      this.DeleteConversionsExceptBaseConversion(child);
      this.UpdateBaseConversion(child, ((PXSelectBase<INItemClass>) this.itemclass).Current.BaseUnit);
    }
    PXDatabase.Update<INItemClass>(new PXDataFieldParam[21]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<INItemClass.itemClassID>((object) child.ItemClassID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.exportToExternal>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.ExportToExternal),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.stkItem>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.StkItem),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.negQty>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.NegQty),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.postToExpenseAccount>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PostToExpenseAccount),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.availabilitySchemeID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.AvailabilitySchemeID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.valMethod>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.ValMethod),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.baseUnit>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.BaseUnit),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.salesUnit>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.SalesUnit),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.purchaseUnit>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PurchaseUnit),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.postClassID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PostClassID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.lotSerClassID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.LotSerClassID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.taxCategoryID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.TaxCategoryID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.deferredCode>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.DeferredCode),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.itemType>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.ItemType),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.priceClassID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PriceClassID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.priceWorkgroupID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PriceWorkgroupID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.priceManagerID>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.PriceManagerID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.minGrossProfitPct>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.MinGrossProfitPct),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.markupPct>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.MarkupPct),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClass.demandCalculation>((object) ((PXSelectBase<INItemClass>) this.itemclass).Current.DemandCalculation)
    });
    if (PXDatabase.Update<INItemClassCurySettings>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<INItemClassCurySettings.itemClassID>((object) child.ItemClassID),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemClassCurySettings.curyID>((object) ((PXGraph) this).Accessinfo.BaseCuryID),
      (PXDataFieldParam) new PXDataFieldAssign<INItemClassCurySettings.dfltSiteID>((object) (int?) ((PXSelectBase<INItemClassCurySettings>) this.itemclasscurysettings).Current?.DfltSiteID)
    }))
      return;
    INItemClassCurySettings current = ((PXSelectBase<INItemClassCurySettings>) this.itemclasscurysettings).Current;
    if ((current != null ? (current.DfltSiteID.HasValue ? 1 : 0) : 0) == 0)
      return;
    PXCache cache = ((PXSelectBase) this.itemclasscurysettings).Cache;
    PXDatabase.Insert<INItemClassCurySettings>(((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (f => cache.GetAttributesReadonly(f).OfType<PXDBFieldAttribute>().Any<PXDBFieldAttribute>() && !f.Equals("tstamp", StringComparison.OrdinalIgnoreCase) && !f.Equals("itemClassID", StringComparison.OrdinalIgnoreCase))).Select<string, PXDataFieldAssign>((Func<string, PXDataFieldAssign>) (f => new PXDataFieldAssign(f, ((PXSelectBase) this.itemclasscurysettings).Cache.GetValue((object) ((PXSelectBase<INItemClassCurySettings>) this.itemclasscurysettings).Current, f)))).Append<PXDataFieldAssign>((PXDataFieldAssign) new PXDataFieldAssign<INItemClassCurySettings.itemClassID>((object) child.ItemClassID)).ToArray<PXDataFieldAssign>());
  }

  protected virtual void DeleteConversionsExceptBaseConversion(INItemClass child)
  {
    PXDatabase.Delete<INUnit>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INUnit.itemClassID>((object) child.ItemClassID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INUnit.fromUnit>((PXDbType) 12, new int?(6), (object) child.BaseUnit, (PXComp) 1)
    });
  }

  protected virtual void UpdateBaseConversion(INItemClass child, string newBaseUnit)
  {
    PXDatabase.Update<INUnit>(new PXDataFieldParam[5]
    {
      (PXDataFieldParam) new PXDataFieldAssign<INUnit.toUnit>((object) newBaseUnit),
      (PXDataFieldParam) new PXDataFieldAssign<INUnit.fromUnit>((object) newBaseUnit),
      (PXDataFieldParam) new PXDataFieldRestrict<INUnit.itemClassID>((object) child.ItemClassID),
      (PXDataFieldParam) new PXDataFieldRestrict<INUnit.toUnit>((object) child.BaseUnit),
      (PXDataFieldParam) new PXDataFieldRestrict<INUnit.fromUnit>((object) child.BaseUnit)
    });
  }

  protected virtual void MergeReplenishment(
    INItemClass child,
    IEnumerable<INItemClassRep> replenishmentTemplate)
  {
    if (!replenishmentTemplate.Any<INItemClassRep>())
      return;
    IEnumerable<INItemClassRep> source = GraphHelper.RowCast<INItemClassRep>((IEnumerable) ((PXSelectBase<INItemClassRep>) this.replenishment).Select(new object[1]
    {
      (object) child.ItemClassID
    }));
    foreach (INItemClassRep inItemClassRep in replenishmentTemplate)
    {
      INItemClassRep rep = inItemClassRep;
      PXDataFieldAssign[] collection = new PXDataFieldAssign[22]
      {
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.replenishmentPolicyID>((object) rep.ReplenishmentPolicyID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.replenishmentMethod>((object) rep.ReplenishmentMethod),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.replenishmentSource>((object) rep.ReplenishmentSource),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.replenishmentSourceSiteID>((object) rep.ReplenishmentSourceSiteID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.launchDate>((object) rep.LaunchDate),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.terminationDate>((object) rep.TerminationDate),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.forecastModelType>((object) rep.ForecastModelType),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.forecastPeriodType>((object) rep.ForecastPeriodType),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.historyDepth>((object) rep.HistoryDepth),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.transferLeadTime>((object) rep.TransferLeadTime),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.transferERQ>((object) rep.TransferERQ),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.serviceLevel>((object) rep.ServiceLevel),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.eSSmoothingConstantL>((object) rep.ESSmoothingConstantL),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.eSSmoothingConstantT>((object) rep.ESSmoothingConstantT),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.eSSmoothingConstantS>((object) rep.ESSmoothingConstantS),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.autoFitModel>((object) rep.AutoFitModel),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.createdByID>((object) rep.CreatedByID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.createdByScreenID>((object) rep.CreatedByScreenID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.createdDateTime>((object) rep.CreatedDateTime),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.lastModifiedByID>((object) rep.LastModifiedByID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.lastModifiedByScreenID>((object) rep.LastModifiedByScreenID),
        (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.lastModifiedDateTime>((object) rep.LastModifiedDateTime)
      };
      if (source.Any<INItemClassRep>((Func<INItemClassRep, bool>) (r => r.ReplenishmentClassID == rep.ReplenishmentClassID)))
        PXDatabase.Update<INItemClassRep>(new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) collection)
        {
          (PXDataFieldParam) new PXDataFieldRestrict<INItemClassRep.itemClassID>((object) child.ItemClassID),
          (PXDataFieldParam) new PXDataFieldRestrict<INItemClassRep.replenishmentClassID>((object) rep.ReplenishmentClassID),
          (PXDataFieldParam) new PXDataFieldRestrict<INItemClassRep.curyID>((object) rep.CuryID)
        }.ToArray());
      else
        PXDatabase.Insert<INItemClassRep>(new List<PXDataFieldAssign>((IEnumerable<PXDataFieldAssign>) collection)
        {
          (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.itemClassID>((object) child.ItemClassID),
          (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.replenishmentClassID>((object) rep.ReplenishmentClassID),
          (PXDataFieldAssign) new PXDataFieldAssign<INItemClassRep.curyID>((object) rep.CuryID)
        }.ToArray());
    }
  }

  protected virtual void MergeUnitConversions(
    INItemClass child,
    IEnumerable<INUnit> unitConversionsTemplate)
  {
    if (!unitConversionsTemplate.Any<INUnit>())
      return;
    IEnumerable<INUnit> source = GraphHelper.RowCast<INUnit>((IEnumerable) ((PXSelectBase<INUnit>) this.UnitsOfMeasureExt.classunits).Select(new object[1]
    {
      (object) child.ItemClassID
    }));
    foreach (INUnit inUnit in unitConversionsTemplate)
    {
      INUnit conv = inUnit;
      PXDataFieldAssign[] collection = new PXDataFieldAssign[9]
      {
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.unitMultDiv>((object) conv.UnitMultDiv),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.unitRate>((object) conv.UnitRate),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.priceAdjustmentMultiplier>((object) conv.PriceAdjustmentMultiplier),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.createdByID>((object) conv.CreatedByID),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.createdByScreenID>((object) conv.CreatedByScreenID),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.createdDateTime>((object) conv.CreatedDateTime),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.lastModifiedByID>((object) conv.LastModifiedByID),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.lastModifiedByScreenID>((object) conv.LastModifiedByScreenID),
        (PXDataFieldAssign) new PXDataFieldAssign<INUnit.lastModifiedDateTime>((object) conv.LastModifiedDateTime)
      };
      if (source.Any<INUnit>((Func<INUnit, bool>) (r =>
      {
        short? unitType1 = r.UnitType;
        int? nullable1 = unitType1.HasValue ? new int?((int) unitType1.GetValueOrDefault()) : new int?();
        short? unitType2 = conv.UnitType;
        int? nullable2 = unitType2.HasValue ? new int?((int) unitType2.GetValueOrDefault()) : new int?();
        return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && r.FromUnit == conv.FromUnit && r.ToUnit == conv.ToUnit;
      })))
        PXDatabase.Update<INUnit>(new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) collection)
        {
          (PXDataFieldParam) new PXDataFieldRestrict<INUnit.itemClassID>((object) child.ItemClassID),
          (PXDataFieldParam) new PXDataFieldRestrict<INUnit.inventoryID>((object) conv.InventoryID),
          (PXDataFieldParam) new PXDataFieldRestrict<INUnit.unitType>((object) conv.UnitType),
          (PXDataFieldParam) new PXDataFieldRestrict<INUnit.fromUnit>((object) conv.FromUnit),
          (PXDataFieldParam) new PXDataFieldRestrict<INUnit.toUnit>((object) conv.ToUnit)
        }.ToArray());
      else
        PXDatabase.Insert<INUnit>(new List<PXDataFieldAssign>((IEnumerable<PXDataFieldAssign>) collection)
        {
          (PXDataFieldAssign) new PXDataFieldAssign<INUnit.itemClassID>((object) child.ItemClassID),
          (PXDataFieldAssign) new PXDataFieldAssign<INUnit.inventoryID>((object) conv.InventoryID),
          (PXDataFieldAssign) new PXDataFieldAssign<INUnit.unitType>((object) conv.UnitType),
          (PXDataFieldAssign) new PXDataFieldAssign<INUnit.fromUnit>((object) conv.FromUnit),
          (PXDataFieldAssign) new PXDataFieldAssign<INUnit.toUnit>((object) conv.ToUnit)
        }.ToArray());
    }
  }

  protected virtual void MergeAttributes(
    INItemClass child,
    IEnumerable<CSAttributeGroup> attributesTemplate)
  {
    if (!attributesTemplate.Any<CSAttributeGroup>())
      return;
    IEnumerable<CSAttributeGroup> source = this.SelectCSAttributeGroupRecords(child.ItemClassStrID);
    foreach (CSAttributeGroup csAttributeGroup in attributesTemplate)
    {
      CSAttributeGroup attr = csAttributeGroup;
      CSAttributeGroup existingAttribute = source.FirstOrDefault<CSAttributeGroup>((Func<CSAttributeGroup, bool>) (r => r.AttributeID == attr.AttributeID && r.EntityType == attr.EntityType));
      this.MergeAttribute(child, existingAttribute, attr);
    }
  }

  protected virtual void MergeAttribute(
    INItemClass child,
    CSAttributeGroup existingAttribute,
    CSAttributeGroup parentAttribute)
  {
    PXDataFieldAssign[] collection = new PXDataFieldAssign[11]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.isActive>((object) parentAttribute.IsActive),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.sortOrder>((object) parentAttribute.SortOrder),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.required>((object) parentAttribute.Required),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.defaultValue>((object) parentAttribute.DefaultValue),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.attributeCategory>((object) parentAttribute.AttributeCategory),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.createdByID>((object) parentAttribute.CreatedByID),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.createdByScreenID>((object) parentAttribute.CreatedByScreenID),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.createdDateTime>((object) parentAttribute.CreatedDateTime),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.lastModifiedByID>((object) parentAttribute.LastModifiedByID),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.lastModifiedByScreenID>((object) parentAttribute.LastModifiedByScreenID),
      (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.lastModifiedDateTime>((object) parentAttribute.LastModifiedDateTime)
    };
    if (existingAttribute != null)
      PXDatabase.Update<CSAttributeGroup>(new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) collection)
      {
        (PXDataFieldParam) new PXDataFieldRestrict<CSAttributeGroup.entityClassID>((object) child.ItemClassStrID),
        (PXDataFieldParam) new PXDataFieldRestrict<CSAttributeGroup.attributeID>((object) parentAttribute.AttributeID),
        (PXDataFieldParam) new PXDataFieldRestrict<CSAttributeGroup.entityType>((object) parentAttribute.EntityType)
      }.ToArray());
    else
      PXDatabase.Insert<CSAttributeGroup>(new List<PXDataFieldAssign>((IEnumerable<PXDataFieldAssign>) collection)
      {
        (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.entityClassID>((object) child.ItemClassStrID),
        (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.attributeID>((object) parentAttribute.AttributeID),
        (PXDataFieldAssign) new PXDataFieldAssign<CSAttributeGroup.entityType>((object) parentAttribute.EntityType)
      }.ToArray());
  }

  private IEnumerable<CSAttributeGroup> SelectCSAttributeGroupRecords(string itemClassID)
  {
    return ((IEnumerable<PXResult<CSAttributeGroup>>) PXSelectBase<CSAttributeGroup, PXViewOf<CSAttributeGroup>.BasedOn<SelectFromBase<CSAttributeGroup, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAttribute>.On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAttribute.attributeID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.entityClassID, Equal<P.AsString>>>>>.And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) itemClassID,
      (object) typeof (InventoryItem).FullName
    })).AsEnumerable<PXResult<CSAttributeGroup>>().Select<PXResult<CSAttributeGroup>, CSAttributeGroup>((Func<PXResult<CSAttributeGroup>, CSAttributeGroup>) (r => PXResult.Unwrap<CSAttributeGroup>((object) r))).Where<CSAttributeGroup>((Func<CSAttributeGroup, bool>) (r => r != null));
  }

  private IEnumerable<RelationGroup> GetRelationGroups()
  {
    INItemClassMaint inItemClassMaint = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXViewOf<RelationGroup>.BasedOn<SelectFromBase<RelationGroup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) inItemClassMaint, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (InventoryItem).Namespace || ((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current != null && UserAccess.IsIncluded(((PXSelectBase<INItemClass>) inItemClassMaint.itemclass).Current.GroupMask, relationGroup))
      {
        ((PXSelectBase<RelationGroup>) inItemClassMaint.Groups).Current = relationGroup;
        yield return relationGroup;
      }
    }
  }

  private void SyncTreeCurrentWithPrimaryViewCurrent(INItemClass primaryViewCurrent)
  {
    if (!this._allowToSyncTreeCurrentWithPrimaryViewCurrent || this._forbidToSyncTreeCurrentWithPrimaryViewCurrent || primaryViewCurrent == null)
      return;
    if (((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current != null)
    {
      int? itemClassId1 = ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current.ItemClassID;
      int? itemClassId2 = primaryViewCurrent.ItemClassID;
      if (itemClassId1.GetValueOrDefault() == itemClassId2.GetValueOrDefault() & itemClassId1.HasValue == itemClassId2.HasValue)
        return;
    }
    this.SetTreeCurrent(primaryViewCurrent.ItemClassID);
  }

  private void SetTreeCurrent(int? itemClassID)
  {
    ItemClassTree.INItemClass nodeById = DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.Instance.GetNodeByID(itemClassID.GetValueOrDefault());
    ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClassNodes).Current = nodeById;
    ((PXSelectBase) this.ItemClassNodes).Cache.ActiveRow = (IBqlTable) nodeById;
    ((PXSelectBase) this.ItemClassNodes).View.RequestRefresh();
  }

  public virtual CreateMatrixItemsHelper GetCreateMatrixItemsHelper()
  {
    return new CreateMatrixItemsHelper((PXGraph) this);
  }

  public class ImmediatelyChangeID : PXImmediatelyChangeID<INItemClass, INItemClass.itemClassCD>
  {
    public ImmediatelyChangeID(PXGraph graph, string name)
      : base(graph, name)
    {
    }

    public ImmediatelyChangeID(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void Initialize()
    {
      ((PXChangeID<INItemClass, INItemClass.itemClassCD>) this).Initialize();
      ((PXChangeID<INItemClass, INItemClass.itemClassCD>) this).DuplicatedKeyMessage = "The {0} item class ID already exists. Specify another item class ID.";
    }
  }

  public class GoTo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public int? ItemClassID { get; set; }

    public abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INItemClassMaint.GoTo.itemClassID>
    {
    }
  }
}
