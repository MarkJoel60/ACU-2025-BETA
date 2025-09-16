// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Graphs.TemplateInventoryItemMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.Common.GraphExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.Matrix.Utility;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.Matrix.Graphs;

public class TemplateInventoryItemMaint : InventoryItemMaintBase, ICreateMatrixHelperFactory
{
  protected bool _JustInserted;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<INItemBoxEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemBoxEx.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.InventoryItem, INItemBoxEx>, PX.Objects.IN.InventoryItem, INItemBoxEx>.SameAsCurrent>, INItemBoxEx>.View Boxes;
  public PXSetup<INPostClass>.Where<BqlOperand<
  #nullable enable
  INPostClass.postClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.postClassID, IBqlString>.FromCurrent>> postclass;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<ExcludedField, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  ExcludedField.templateID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  PX.Data.BQL.Fluent.By<BqlField<
  #nullable enable
  INMatrixExcludedData.createdDateTime, IBqlDateTime>.Asc>>, 
  #nullable disable
  ExcludedField>.View FieldsExcludedFromUpdate;
  public FbqlSelect<SelectFromBase<ExcludedAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CSAnswers>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CSAnswers.attributeID, 
  #nullable disable
  Equal<ExcludedAttribute.fieldName>>>>>.And<BqlOperand<
  #nullable enable
  CSAnswers.refNoteID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.noteID, IBqlGuid>.FromCurrent>>>>>.Where<
  #nullable disable
  BqlOperand<
  #nullable enable
  ExcludedAttribute.templateID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  CSAnswers.order, IBqlShort>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  CSAnswers.attributeID, IBqlString>.Asc>>, 
  #nullable disable
  ExcludedAttribute>.View AttributesExcludedFromUpdate;
  public FbqlSelect<SelectFromBase<CSAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CSAttribute.attributeID, IBqlString>.IsEqual<
  #nullable disable
  MatrixAttributeSelectorAttribute.dummyAttributeName>>, CSAttribute>.View DummyAttribute;
  public FbqlSelect<SelectFromBase<CSAttributeDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CSAttributeDetail.attributeID, IBqlString>.IsEqual<
  #nullable disable
  MatrixAttributeSelectorAttribute.dummyAttributeName>>, CSAttributeDetail>.View DummyAttributeValue;

  public override bool IsStockItemFlag
  {
    get
    {
      PX.Objects.IN.InventoryItem current = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.ItemSettings).Current;
      return current != null && current.StkItem.GetValueOrDefault();
    }
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    TemplateInventoryItemMaint.Configure(config.GetScreenConfigurationContext<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>());
  }

  protected static void Configure(
    WorkflowContext<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem> context)
  {
    BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionCategory.IConfigured otherCategory = CommonActionCategories.Get<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>(context).Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ScreenConfiguration.IConfigured) ((BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionDefinition.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TemplateInventoryItemMaint, PXAction<PX.Objects.IN.InventoryItem>>>) (g => g.ChangeID), (Func<BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionDefinition.IConfigured>) (a => (BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionDefinition.IConfigured) a.WithCategory(otherCategory))))).WithCategories((Action<BoundedTo<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(otherCategory)))));
  }

  public virtual bool IsDirty
  {
    get => (!this._JustInserted || ((PXGraph) this).IsContractBasedAPI) && ((PXGraph) this).IsDirty;
  }

  public IEnumerable attributesExcludedFromUpdate()
  {
    return (IEnumerable) this.GetAttributesExcludedFromUpdate();
  }

  public TemplateInventoryItemMaint()
  {
    ((PXSelectBase) this.Item).View = new PXView((PXGraph) this, false, (BqlCommand) new SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.isTemplate, Equal<True>>>>>.And<MatchUser>>());
    ((PXGraph) this).Views["Item"] = ((PXSelectBase) this.Item).View;
    ((PXAction) this.updateCost).SetVisible(false);
    ((PXAction) this.viewSalesPrices).SetVisible(false);
    ((PXAction) this.viewVendorPrices).SetVisible(false);
    ((PXAction) this.viewRestrictionGroups).SetVisible(false);
    ((PXSelectBase) this.Answers).Cache.Fields.Add("Description");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldSelecting.AddHandler(typeof (CSAnswers), "Description", TemplateInventoryItemMaint.\u003C\u003Ec.\u003C\u003E9__15_0 ?? (TemplateInventoryItemMaint.\u003C\u003Ec.\u003C\u003E9__15_0 = new PXFieldSelecting((object) TemplateInventoryItemMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__15_0))));
  }

  [PXMergeAttributes]
  [PXDefault("F", typeof (SearchFor<INItemClass.itemType>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemClass.itemClassID, Equal<BqlField<INItemClass.parentItemClassID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<BqlField<INItemClass.stkItem, IBqlBool>.FromCurrent>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemClass.itemType> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.stkItem>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemClass.stkItem> eventArgs)
  {
  }

  [TemplateInventory(IsKey = true, DirtyRead = true)]
  [PXParent(typeof (INItemCategory.FK.InventoryItem))]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemCategory.inventoryID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (IIf<Where<FeatureInstalled<FeaturesSet.inventory>>, True, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.stkItem> eventArgs)
  {
  }

  [PXDefault]
  [TemplateInventoryRaw(IsKey = true, Filterable = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.inventoryCD> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.isTemplate> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.FromCurrent>>), "The item class specified for the stock item must be the same as in the template item.", new System.Type[] {})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.itemClassID> eventArgs)
  {
  }

  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.lotSerClassID), CacheGlobal = true)]
  [PXUIRequired(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.lotSerClassID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (INPostClass.postClassID), DescriptionField = typeof (INPostClass.descr))]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.postClassID), CacheGlobal = true)]
  [PXUIRequired(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.postClassID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (AccountAttribute))]
  [TemplateInventoryAccount(true, typeof (PX.Objects.IN.InventoryItem.invtAcctID))]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.invtAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (SubAccountAttribute))]
  [TemplateInventorySubAccount(true, typeof (PX.Objects.IN.InventoryItem.invtSubID), typeof (PX.Objects.IN.InventoryItem.invtAcctID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.invtSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (AccountAttribute))]
  [TemplateInventoryAccount(true, typeof (PX.Objects.IN.InventoryItem.cOGSAcctID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.cOGSAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (SubAccountAttribute))]
  [TemplateInventorySubAccount(true, typeof (PX.Objects.IN.InventoryItem.cOGSSubID), typeof (PX.Objects.IN.InventoryItem.cOGSAcctID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.cOGSSubID> e)
  {
  }

  [PXMergeAttributes]
  [TemplateInventoryAccount(false, typeof (PX.Objects.IN.InventoryItem.invtAcctID), DisplayName = "Expense Accrual Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXFormula(typeof (Selector<PX.Objects.IN.InventoryItem.postClassID, INPostClass.invtAcctID>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.expenseAccrualAcctID> e)
  {
  }

  [PXMergeAttributes]
  [TemplateInventorySubAccount(false, typeof (PX.Objects.IN.InventoryItem.invtSubID), typeof (PX.Objects.IN.InventoryItem.invtAcctID), DisplayName = "Expense Accrual Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXFormula(typeof (Selector<PX.Objects.IN.InventoryItem.postClassID, INPostClass.invtSubID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.expenseAccrualSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<PX.Objects.IN.InventoryItem.postClassID, INPostClass.cOGSAcctID>))]
  [PXDefault]
  [PXUIRequired(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>))]
  [TemplateInventoryAccount(false, typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.expenseAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<PX.Objects.IN.InventoryItem.postClassID, INPostClass.cOGSSubID>))]
  [PXDefault]
  [PXUIRequired(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>))]
  [TemplateInventorySubAccount(false, typeof (PX.Objects.IN.InventoryItem.cOGSSubID), typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.expenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [DBMatrixLocalizableDescription(60, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CSAttribute.description> eventArgs)
  {
  }

  [PXMergeAttributes]
  [DBMatrixLocalizableDescription(60, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CSAttributeDetail.description> eventArgs)
  {
  }

  protected override void _(PX.Data.Events.RowInserted<PX.Objects.IN.InventoryItem> eventArgs)
  {
    base._(eventArgs);
    this._JustInserted = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.hasChild> eventArgs)
  {
    PX.Objects.IN.InventoryItem row1 = eventArgs.Row;
    int num1;
    if (row1 == null)
    {
      num1 = 0;
    }
    else
    {
      int? inventoryId = row1.InventoryID;
      int num2 = 0;
      num1 = inventoryId.GetValueOrDefault() > num2 & inventoryId.HasValue ? 1 : 0;
    }
    bool? nullable1;
    if (num1 != 0)
    {
      nullable1 = eventArgs.Row.IsTemplate;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = eventArgs.Row.HasChild;
        if (!nullable1.HasValue)
        {
          PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.templateItemID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.templateItemID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly((PXGraph) this);
          using (new PXFieldScope(((PXSelectBase) readOnly).View, new System.Type[1]
          {
            typeof (PX.Objects.IN.InventoryItem.inventoryID)
          }))
          {
            PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) readOnly).Select(new object[1]
            {
              (object) eventArgs.Row.InventoryID
            }));
            eventArgs.Row.HasChild = new bool?(inventoryItem != null);
          }
        }
      }
    }
    PX.Data.Events.FieldSelecting<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.hasChild> fieldSelecting = eventArgs;
    PX.Objects.IN.InventoryItem row2 = eventArgs.Row;
    bool? nullable2;
    if (row2 == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = row2.HasChild;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> local = (ValueType) nullable2;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.hasChild>>) fieldSelecting).ReturnValue = (object) local;
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache;
    PX.Objects.IN.InventoryItem row = e.Row;
    bool? nullable;
    int num1;
    if (((PXSelectBase<INPostClass>) this.postclass).Current != null)
    {
      nullable = ((PXSelectBase<INPostClass>) this.postclass).Current.COGSSubFromSales;
      bool flag = false;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.cOGSSubID>(cache1, (object) row, num1 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.stdCstVarAcctID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row, e.Row?.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.stdCstVarSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row, e.Row?.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.stdCstRevAcctID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row, e.Row?.ValMethod == "T");
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.stdCstRevSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row, e.Row?.ValMethod == "T");
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry>(cache2, (object) null, num2 != 0);
    INAcctSubDefault.Required(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, new PXRowSelectedEventArgs((object) e.Row));
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry>(cache3, (object) null, num3 != 0);
    ((PXSelectBase) this.Boxes).Cache.AllowInsert = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    ((PXSelectBase) this.Boxes).Cache.AllowUpdate = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    ((PXSelectBase) this.Boxes).Cache.AllowSelect = e.Row.PackageOption != "N" && PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>();
    PXUIFieldAttribute.SetEnabled<PX.Objects.IN.InventoryItem.packSeparately>(((PXSelectBase) this.Item).Cache, (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current, e.Row.PackageOption == "W");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.qty>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "Q");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.uOM>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "Q");
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxQty>(((PXSelectBase) this.Boxes).Cache, (object) null, EnumerableExtensions.IsIn<string>(e.Row.PackageOption, "W", "V"));
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxWeight>(((PXSelectBase) this.Boxes).Cache, (object) null, EnumerableExtensions.IsIn<string>(e.Row.PackageOption, "W", "V"));
    PXUIFieldAttribute.SetVisible<INItemBoxEx.maxVolume>(((PXSelectBase) this.Boxes).Cache, (object) null, e.Row.PackageOption == "V");
    if (PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
      this.ValidatePackaging(e.Row);
    this.FieldsDependOnStkItemFlag(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, e.Row);
    object valueExt = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache.GetValueExt<PX.Objects.IN.InventoryItem.hasChild>((object) e.Row);
    nullable = valueExt is PXFieldState pxFieldState ? (bool?) pxFieldState.Value : (bool?) valueExt;
    bool hasChild = (nullable.GetValueOrDefault() ? 1 : 0) != 0;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) null).For<PX.Objects.IN.InventoryItem.itemClassID>((Action<PXUIFieldAttribute>) (a => a.Enabled = !hasChild)).SameFor<PX.Objects.IN.InventoryItem.stkItem>();
    chained = chained.SameFor<PX.Objects.IN.InventoryItem.baseUnit>();
    chained.SameFor<PX.Objects.IN.InventoryItem.decimalBaseUnit>();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<IDGenerationRule, INMatrixGenerationRule.separator> e)
  {
    if (e.Row == null || string.IsNullOrEmpty((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<IDGenerationRule, INMatrixGenerationRule.separator>, IDGenerationRule, object>) e).NewValue))
      return;
    PXViewOf<Segment>.BasedOn<SelectFromBase<Segment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Segment.dimensionID, IBqlString>.IsEqual<BaseInventoryAttribute.dimensionName>>>.ReadOnly readOnly = new PXViewOf<Segment>.BasedOn<SelectFromBase<Segment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Segment.dimensionID, IBqlString>.IsEqual<BaseInventoryAttribute.dimensionName>>>.ReadOnly((PXGraph) this);
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<IDGenerationRule, INMatrixGenerationRule.separator>, IDGenerationRule, object>) e).NewValue;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<Segment> pxResult in ((PXSelectBase<Segment>) readOnly).Select(objArray))
    {
      Segment segment = PXResult<Segment>.op_Implicit(pxResult);
      if (newValue.Contains(segment.PromptCharacter))
        throw new PXSetPropertyException("The {0} character cannot be used as a separator because it is used as a prompt character for the INVENTORY segmented key on the Segmented Keys (CS202000) form.", new object[1]
        {
          (object) segment.PromptCharacter
        });
    }
  }

  protected override void _(PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem> eventArgs)
  {
    base._(eventArgs);
    PX.Objects.IN.InventoryItem row = eventArgs.Row;
    bool? nullable;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      nullable = row.IsTemplate;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    this.ValidateChangeStkItemFlag();
    this.ValidateMainFieldsAreNotChanged(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem>>) eventArgs).Cache, eventArgs.Row);
    nullable = eventArgs.Row.StkItem;
    if (nullable.GetValueOrDefault())
      return;
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      eventArgs.Row.NonStockReceipt = new bool?(false);
      eventArgs.Row.NonStockShip = new bool?(false);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>())
      eventArgs.Row.NonStockReceiptAsService = eventArgs.Row.NonStockReceipt;
    nullable = eventArgs.Row.NonStockReceipt;
    if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(eventArgs.Row.PostClassID))
      throw new PXRowPersistingException("postClassID", (object) eventArgs.Row.PostClassID, "'{0}' cannot be empty.", new object[1]
      {
        (object) "postClassID"
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem> eventArgs)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.itemType>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.itemClassID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.invtAcctID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.invtSubID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.cOGSAcctID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.cOGSSubID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.expenseAccrualAcctID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.expenseAccrualSubID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.expenseAcctID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetValueExt<PX.Objects.IN.InventoryItem.expenseSubID>((object) eventArgs.Row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) eventArgs).Cache.SetDefaultExt<PX.Objects.IN.InventoryItem.valMethod>((object) eventArgs.Row);
    ((PXSelectBase) this.ItemCurySettings).Cache.RaiseRowSelected((object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.defaultColumnMatrixAttributeID> e)
  {
    if (e.NewValue as string == "~MX~DUMMY~")
      this.EnsureDummyAttributeExists();
    this.GetAttributeGroupHelper().Recalculate(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.defaultRowMatrixAttributeID> e)
  {
    if (e.NewValue as string == "~MX~DUMMY~")
      this.EnsureDummyAttributeExists();
    this.GetAttributeGroupHelper().Recalculate(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.valMethod> e)
  {
    ((PXSelectBase) this.ItemCurySettings).Cache.RaiseRowSelected((object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<ExcludedField, ExcludedField.tableName> eventArgs)
  {
    (System.Type, string)[] tablesToUpdateItem = this.GetCreateMatrixItemsHelper().GetTablesToUpdateItem();
    PXStringListAttribute.SetList<ExcludedField.tableName>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<ExcludedField, ExcludedField.tableName>>) eventArgs).Cache, (object) eventArgs.Row, ((IEnumerable<(System.Type, string)>) tablesToUpdateItem).Select<(System.Type, string), (string, string)>((Func<(System.Type, string), (string, string)>) (t => (t.Dac.FullName, t.DisplayName))).ToArray<(string, string)>());
  }

  protected virtual void _(PX.Data.Events.RowInserted<ExcludedAttribute> eventArgs)
  {
    ((PXSelectBase) this.AttributesExcludedFromUpdate).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItemCurySettings, InventoryItemCurySettings.dfltSiteID> eventArgs)
  {
    INSite inSite = INSite.PK.Find((PXGraph) this, eventArgs.Row.DfltSiteID);
    eventArgs.Row.DfltShipLocationID = (int?) inSite?.ShipLocationID;
    eventArgs.Row.DfltReceiptLocationID = (int?) inSite?.ReceiptLocationID;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<InventoryItemCurySettings> eventArgs)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItemCurySettings>>) eventArgs).Cache;
    PX.Objects.IN.InventoryItem current = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current;
    int num = current != null ? (current.StkItem.GetValueOrDefault() ? 1 : 0) : 0;
    this.DefaultLocationsDependOnStkItemFlag(cache, (InventoryItemCurySettings) null, num != 0);
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InventoryItemCurySettings>>) eventArgs).Cache, (object) null).For<InventoryItemCurySettings.pendingStdCost>((Action<PXUIFieldAttribute>) (a => a.Enabled = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current?.ValMethod == "T")).SameFor<InventoryItemCurySettings.pendingStdCostDate>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<CSAnswers> e)
  {
    if (e.Row == null)
      return;
    bool enabled = this.GetAttributeCategory(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CSAnswers>>) e).Cache, e.Row) == "V";
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CSAnswers>>) e).Cache, (object) e.Row).For<CSAnswers.isActive>((Action<PXUIFieldAttribute>) (a => a.Enabled = enabled));
  }

  private string GetAttributeCategory(PXCache cache, CSAnswers row)
  {
    object valueExt = cache.GetValueExt<CSAnswers.attributeCategory>((object) row);
    return !(valueExt is PXFieldState pxFieldState) ? (string) valueExt : (string) pxFieldState.Value;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CSAnswers> e)
  {
    if (!e.Row.IsActive.GetValueOrDefault() && e.OldRow.IsActive.GetValueOrDefault())
    {
      PX.Objects.IN.InventoryItem inventoryItem = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current;
      if (inventoryItem.DefaultColumnMatrixAttributeID == e.Row.AttributeID)
      {
        inventoryItem.DefaultColumnMatrixAttributeID = (string) null;
        inventoryItem = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Update(inventoryItem);
      }
      if (inventoryItem.DefaultRowMatrixAttributeID == e.Row.AttributeID)
      {
        inventoryItem.DefaultRowMatrixAttributeID = (string) null;
        ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Update(inventoryItem);
      }
    }
    bool? isActive1 = e.Row.IsActive;
    bool? isActive2 = e.OldRow.IsActive;
    if (isActive1.GetValueOrDefault() == isActive2.GetValueOrDefault() & isActive1.HasValue == isActive2.HasValue)
      return;
    PXCacheEx.AdjustReadonly<MatrixAttributeSelectorAttribute>(((PXSelectBase) this.Item).Cache, (object) null).ForAllFields((Action<MatrixAttributeSelectorAttribute>) (a => a.RefreshDummyValue(((PXSelectBase) this.Item).Cache, (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current)));
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<CSAnswers.isActive> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CSAnswers.isActive>, object, object>) e).NewValue;
    bool? nullable1 = (bool?) e.OldValue;
    if (newValue.GetValueOrDefault() == nullable1.GetValueOrDefault() & newValue.HasValue == nullable1.HasValue)
      return;
    CSAnswers row = (CSAnswers) e.Row;
    if (this.GetAttributeCategory(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CSAnswers.isActive>>) e).Cache, (CSAnswers) e.Row) != "V")
      return;
    object valueExt = ((PXSelectBase) this.Item).Cache.GetValueExt<PX.Objects.IN.InventoryItem.hasChild>((object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current);
    nullable1 = valueExt is PXFieldState pxFieldState ? (bool?) pxFieldState.Value : (bool?) valueExt;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    nullable1 = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CSAnswers.isActive>, object, object>) e).NewValue;
    int num1;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = (bool?) e.OldValue;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = valueOrDefault ? 1 : 0;
    if ((num1 & num2) != 0)
      throw new PXSetPropertyException<CSAnswers.isActive>("The {0} attribute cannot be activated because it is a variant attribute, and at least one matrix item exists for the template item. Remove the matrix items of the template item first.", new object[1]
      {
        (object) row.AttributeID
      });
    nullable1 = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CSAnswers.isActive>, object, object>) e).NewValue;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = (bool?) e.OldValue;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    int num4 = valueOrDefault ? 1 : 0;
    if ((num3 & num4) == 0)
      return;
    if (GraphHelper.RowCast<CSAnswers>((IEnumerable) this.Answers.Select(Array.Empty<object>())).Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
    {
      if (!string.Equals(this.GetAttributeCategory(((PXSelectBase) this.Answers).Cache, x), "V", StringComparison.OrdinalIgnoreCase) || !x.IsActive.GetValueOrDefault())
        return false;
      Guid? refNoteId1 = x.RefNoteID;
      Guid? refNoteId2 = row.RefNoteID;
      if (refNoteId1.HasValue != refNoteId2.HasValue)
        return false;
      return !refNoteId1.HasValue || refNoteId1.GetValueOrDefault() == refNoteId2.GetValueOrDefault();
    })).Count<CSAnswers>() <= 1)
      throw new PXSetPropertyException<CSAnswers.isActive>("The {0} attribute cannot be deactivated because it is a variant attribute, and at least one matrix item exists for the template item. Remove the matrix items of the template item first.", new object[1]
      {
        (object) row.AttributeID
      });
    int? rowCount = PXSelectBase<CSAnswers, PXViewOf<CSAnswers>.BasedOn<SelectFromBase<CSAnswers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<PX.Objects.IN.InventoryItem.noteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.templateItemID, Equal<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<BqlField<CSAnswers.attributeID, IBqlString>.FromCurrent>>>.Aggregate<To<Count<CSAnswers.value>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current.InventoryID,
      (object) row.AttributeID
    }).RowCount;
    int? nullable2 = rowCount;
    int num5 = 1;
    if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
      throw new PXSetPropertyException<CSAnswers.isActive>("The {0} attribute cannot be deactivated because it is a variant attribute, and at least two matrix items exist with different values. Remove the matrix items of the template item first.", new object[1]
      {
        (object) row.AttributeID
      });
    if (rowCount.GetValueOrDefault() != 1 || this.Answers.Ask("Warning", PXMessages.LocalizeNoPrefix("This action will deactivate the attribute for the template item and its related matrix items. Do you want to continue?"), (MessageButtons) 4) == 6)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CSAnswers.isActive>, object, object>) e).NewValue = (object) true;
  }

  public virtual CreateMatrixItemsHelper GetCreateMatrixItemsHelper()
  {
    return new CreateMatrixItemsHelper((PXGraph) this);
  }

  public virtual AttributeGroupHelper GetAttributeGroupHelper()
  {
    return new AttributeGroupHelper((PXGraph) this);
  }

  protected virtual void ValidateChangeStkItemFlag()
  {
    if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.templateItemID, Equal<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsNotEqual<BqlField<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<PX.Objects.IN.InventoryItem.stkItem>("You cannot change the value of the Stock Item check box if the template item has at least one matrix item. Remove all matrix items of the template item first.");
  }

  protected virtual void ValidateMainFieldsAreNotChanged(PXCache cache, PX.Objects.IN.InventoryItem row)
  {
    if (!this.ReloadHasChild())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (inventoryItem != null && !cache.ObjectsEqual<PX.Objects.IN.InventoryItem.itemClassID, PX.Objects.IN.InventoryItem.baseUnit, PX.Objects.IN.InventoryItem.decimalBaseUnit>((object) inventoryItem, (object) row))
      throw new PXException("You cannot change the values of the Item Class, Base Unit, and Sales Categories boxes if the template item has at least one matrix item. Remove all matrix items of the template item first.");
  }

  protected virtual void ValidatePackaging(PX.Objects.IN.InventoryItem row)
  {
    PXUIFieldAttribute.SetError<PX.Objects.IN.InventoryItem.weightUOM>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<PX.Objects.IN.InventoryItem.baseItemWeight>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<PX.Objects.IN.InventoryItem.volumeUOM>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    PXUIFieldAttribute.SetError<PX.Objects.IN.InventoryItem.baseItemVolume>(((PXSelectBase) this.Item).Cache, (object) row, (string) null);
    Decimal? nullable;
    if (row.PackageOption == "W" || row.PackageOption == "V")
    {
      if (string.IsNullOrEmpty(row.WeightUOM))
        ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<PX.Objects.IN.InventoryItem.weightUOM>((object) row, (object) row.WeightUOM, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      nullable = row.BaseItemWeight;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() <= num1 & nullable.HasValue)
        ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<PX.Objects.IN.InventoryItem.baseItemWeight>((object) row, (object) row.BaseItemWeight, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      if (row.PackageOption == "V")
      {
        if (string.IsNullOrEmpty(row.VolumeUOM))
          ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<PX.Objects.IN.InventoryItem.volumeUOM>((object) row, (object) row.VolumeUOM, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
        nullable = row.BaseItemVolume;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
          ((PXSelectBase) this.Item).Cache.RaiseExceptionHandling<PX.Objects.IN.InventoryItem.baseItemVolume>((object) row, (object) row.BaseItemVolume, (Exception) new PXSetPropertyException("Value is required for Auto packaging to work correctly.", (PXErrorLevel) 2));
      }
    }
    foreach (PXResult<INItemBoxEx> pxResult in ((PXSelectBase<INItemBoxEx>) this.Boxes).Select(Array.Empty<object>()))
    {
      INItemBoxEx inItemBoxEx = PXResult<INItemBoxEx>.op_Implicit(pxResult);
      PXUIFieldAttribute.SetError<INItemBoxEx.boxID>(((PXSelectBase) this.Boxes).Cache, (object) inItemBoxEx, (string) null);
      PXUIFieldAttribute.SetError<INItemBoxEx.maxQty>(((PXSelectBase) this.Boxes).Cache, (object) inItemBoxEx, (string) null);
      if (row.PackageOption == "W" || row.PackageOption == "V")
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
      if (row.PackageOption == "W" || row.PackageOption == "V")
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

  protected virtual bool ReloadHasChild()
  {
    PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.templateItemID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.templateItemID, IBqlInt>.IsEqual<BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly((PXGraph) this);
    ((PXSelectBase) readOnly).Cache.ClearQueryCache();
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new System.Type[1]
    {
      typeof (PX.Objects.IN.InventoryItem.inventoryID)
    }))
      return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) readOnly).Select(Array.Empty<object>())) != null;
  }

  public virtual PX.Objects.IN.InventoryItem UpdateChild(PX.Objects.IN.InventoryItem item)
  {
    PX.Objects.IN.InventoryItem current = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current;
    try
    {
      return ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Update(item);
    }
    finally
    {
      if (current != null)
        ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current = current;
    }
  }

  public virtual InventoryItemCurySettings UpdateChildCurySettings(InventoryItemCurySettings item)
  {
    PX.Objects.IN.InventoryItem current = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current;
    try
    {
      return ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(item);
    }
    finally
    {
      if (current != null)
        ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current = current;
    }
  }

  protected override void ResetDefaultsOnItemClassChange(PX.Objects.IN.InventoryItem row)
  {
    base.ResetDefaultsOnItemClassChange(row);
    PXCache cache = ((PXSelectBase) this.Item).Cache;
    cache.SetDefaultExt<PX.Objects.IN.InventoryItem.lotSerClassID>((object) row);
    cache.SetDefaultExt<PX.Objects.IN.InventoryItem.valMethod>((object) row);
    cache.SetDefaultExt<PX.Objects.IN.InventoryItem.countryOfOrigin>((object) row);
    cache.SetDefaultExt<PX.Objects.IN.InventoryItem.planningMethod>((object) row);
    cache.SetDefaultExt<PX.Objects.IN.InventoryItem.postToExpenseAccount>((object) row);
  }

  private PXDelegateResult GetAttributesExcludedFromUpdate()
  {
    PXView pxView = new PXView((PXGraph) this, false, PXView.View.BqlSelect);
    int startRow = PXView.StartRow;
    int maximumRows = PXView.MaximumRows;
    int num1 = 0;
    PXDelegateResult excludedFromUpdate = new PXDelegateResult();
    excludedFromUpdate.IsResultTruncated = true;
    excludedFromUpdate.IsResultFiltered = true;
    excludedFromUpdate.IsResultSorted = !PXView.ReverseOrder;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int num2 = maximumRows;
    ref int local2 = ref num1;
    foreach (PXResult<ExcludedAttribute, CSAnswers> pxResult in pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, num2, ref local2))
    {
      ExcludedAttribute excludedAttribute = PXResult<ExcludedAttribute, CSAnswers>.op_Implicit(pxResult);
      PXResult.Unwrap<CSAnswers>((object) pxResult);
      if (!(((PXSelectBase) this.Answers).Cache.Locate((object) new CSAnswers()
      {
        AttributeID = excludedAttribute.FieldName,
        RefNoteID = (PXView.Parameters.Length != 0 ? PXView.Parameters[0] as Guid? : new Guid?())
      }) is CSAnswers csAnswers))
        ((List<object>) excludedFromUpdate).Add((object) pxResult);
      else if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Answers).Cache.GetStatus((object) csAnswers), (PXEntryStatus) 3, (PXEntryStatus) 4))
        ((List<object>) excludedFromUpdate).Add((object) new PXResult<ExcludedAttribute, CSAnswers>(excludedAttribute, (CSAnswers) null));
      else
        ((List<object>) excludedFromUpdate).Add((object) new PXResult<ExcludedAttribute, CSAnswers>(excludedAttribute, csAnswers));
    }
    PXView.StartRow = 0;
    return excludedFromUpdate;
  }

  protected virtual void FieldsDependOnStkItemFlag(PXCache cache, PX.Objects.IN.InventoryItem row)
  {
    bool valueOrDefault = row.StkItem.GetValueOrDefault();
    this.ItemTypeValuesDependOnStkItemFlag(cache, row, valueOrDefault);
    this.GeneralSettingsFieldsDependOnStkItemFlag(cache, row, valueOrDefault);
    this.FulfillmentFieldsDependOnStkItemFlag(cache, row, valueOrDefault);
    this.PriceCostInfoFieldsDependOnStkItemFlag(cache, row, valueOrDefault);
    this.VendorDetailsFieldsDependOnStkItemFlag(row, valueOrDefault);
    this.GLAccountsFieldsDependOnStkItemFlag(cache, row, valueOrDefault);
  }

  protected virtual void ItemTypeValuesDependOnStkItemFlag(
    PXCache cache,
    PX.Objects.IN.InventoryItem row,
    bool isStock)
  {
    INItemTypes.CustomListAttribute customListAttribute = isStock ? (INItemTypes.CustomListAttribute) new INItemTypes.StockListAttribute() : (INItemTypes.CustomListAttribute) new INItemTypes.NonStockListAttribute();
    PXStringListAttribute.SetList<PX.Objects.IN.InventoryItem.itemType>(cache, (object) row, customListAttribute.AllowedValues, customListAttribute.AllowedLabels);
  }

  protected virtual void DefaultLocationsDependOnStkItemFlag(
    PXCache cache,
    InventoryItemCurySettings row,
    bool isStock)
  {
    PXCacheEx.AdjustUI(cache, (object) row).For<InventoryItemCurySettings.dfltShipLocationID>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock)).SameFor<InventoryItemCurySettings.dfltReceiptLocationID>();
  }

  protected virtual void GeneralSettingsFieldsDependOnStkItemFlag(
    PXCache cache,
    PX.Objects.IN.InventoryItem row,
    bool isStock)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.valMethod>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock));
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.lotSerClassID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.countryOfOrigin>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.cycleID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.aBCCodeID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.aBCCodeIsFixed>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.movementClassID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.movementClassIsFixed>();
    chained1.SameFor<PX.Objects.IN.InventoryItem.defaultSubItemID>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained2 = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.completePOLine>((Action<PXUIFieldAttribute>) (fa => fa.Visible = !isStock));
    chained2 = chained2.SameFor<PX.Objects.IN.InventoryItem.nonStockReceipt>();
    chained2.SameFor<PX.Objects.IN.InventoryItem.nonStockShip>();
    bool flag = row.ItemType == "S";
    bool fieldServiceVisible = !isStock & flag && PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    PXCacheEx.AdjustUI(cache, (object) row).For("EstimatedDuration", (Action<PXUIFieldAttribute>) (fa => fa.Visible = fieldServiceVisible));
    PXCacheEx.AdjustUI(((PXSelectBase) this.ItemClass).Cache, (object) null).For("Mem_RouteService", (Action<PXUIFieldAttribute>) (fa => fa.Visible = fieldServiceVisible));
  }

  protected virtual void FulfillmentFieldsDependOnStkItemFlag(
    PXCache cache,
    PX.Objects.IN.InventoryItem row,
    bool isStock)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.hSTariffCode>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock));
    chained = chained.SameFor<PX.Objects.IN.InventoryItem.packageOption>();
    chained.SameFor<PX.Objects.IN.InventoryItem.packSeparately>();
  }

  protected virtual void PriceCostInfoFieldsDependOnStkItemFlag(
    PXCache cache,
    PX.Objects.IN.InventoryItem row,
    bool isStock)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.postToExpenseAccount>((Action<PXUIFieldAttribute>) (fa => fa.Visible = !isStock));
    chained = chained.SameFor<PX.Objects.IN.InventoryItem.costBasis>();
    chained = chained.SameFor<PX.Objects.IN.InventoryItem.percentOfSalesPrice>();
    chained.SameFor("DfltEarningType");
  }

  protected virtual void VendorDetailsFieldsDependOnStkItemFlag(PX.Objects.IN.InventoryItem row, bool isStock)
  {
    PXCacheEx.AdjustUI(((PXGraph) this).Caches[typeof (PX.Objects.CR.Standalone.Location)], (object) null).For<PX.Objects.CR.Standalone.Location.vSiteID>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock)).SameFor<PX.Objects.CR.Standalone.Location.vLeadTime>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.VendorItems).Cache, (object) ((PXSelectBase<POVendorInventory>) this.VendorItems).Current).For<POVendorInventory.subItemID>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock));
    chained = chained.SameFor<POVendorInventory.overrideSettings>();
    chained = chained.SameFor<POVendorInventory.subItemID>();
    chained = chained.SameFor<POVendorInventory.addLeadTimeDays>();
    chained = chained.SameFor<POVendorInventory.minOrdFreq>();
    chained = chained.SameFor<POVendorInventory.minOrdQty>();
    chained = chained.SameFor<POVendorInventory.maxOrdQty>();
    chained = chained.SameFor<POVendorInventory.lotSize>();
    chained.SameFor<POVendorInventory.eRQ>();
  }

  protected virtual void GLAccountsFieldsDependOnStkItemFlag(
    PXCache cache,
    PX.Objects.IN.InventoryItem row,
    bool isStock)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.stdCstRevAcctID>((Action<PXUIFieldAttribute>) (fa => fa.Visible = isStock));
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.stdCstRevSubID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.stdCstVarAcctID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.stdCstVarSubID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.lCVarianceAcctID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.lCVarianceSubID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.invtAcctID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.invtSubID>();
    chained1 = chained1.SameFor<PX.Objects.IN.InventoryItem.cOGSAcctID>();
    chained1.SameFor<PX.Objects.IN.InventoryItem.cOGSSubID>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained2 = PXCacheEx.AdjustUI(cache, (object) row).For<PX.Objects.IN.InventoryItem.expenseAccrualAcctID>((Action<PXUIFieldAttribute>) (fa => fa.Visible = !isStock));
    chained2 = chained2.SameFor<PX.Objects.IN.InventoryItem.expenseAccrualSubID>();
    chained2 = chained2.SameFor<PX.Objects.IN.InventoryItem.expenseAcctID>();
    chained2.SameFor<PX.Objects.IN.InventoryItem.expenseSubID>();
  }

  protected virtual void EnsureDummyAttributeExists()
  {
    this.EnsureDummyAttributeHeaderExists();
    this.EnsureDummyAttributeValueExists();
  }

  protected virtual void EnsureDummyAttributeHeaderExists()
  {
    if (((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)["~MX~DUMMY~"] != null || ((PXSelectBase<CSAttribute>) this.DummyAttribute).SelectSingle(Array.Empty<object>()) != null)
      return;
    CSAttribute newAttribute = ((PXSelectBase<CSAttribute>) this.DummyAttribute).Insert(new CSAttribute()
    {
      AttributeID = "~MX~DUMMY~"
    });
    newAttribute.ControlType = new int?(2);
    newAttribute.Description = "~MX~DUMMY~";
    DBMatrixLocalizableDescriptionAttribute.SetTranslations<CSAttribute.description>(((PXSelectBase) this.DummyAttribute).Cache, (object) newAttribute, (Func<string, string>) (locale => newAttribute.Description));
    ((PXSelectBase<CSAttribute>) this.DummyAttribute).Update(newAttribute);
  }

  protected virtual void EnsureDummyAttributeValueExists()
  {
    CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)["~MX~DUMMY~"];
    int num;
    if (attribute == null)
    {
      num = 1;
    }
    else
    {
      List<CRAttribute.AttributeValue> values = attribute.Values;
      num = !(values != null ? new bool?(values.Any<CRAttribute.AttributeValue>()) : new bool?()).GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0 || ((PXSelectBase<CSAttributeDetail>) this.DummyAttributeValue).SelectSingle(Array.Empty<object>()) != null)
      return;
    CSAttributeDetail newDetail = ((PXSelectBase<CSAttributeDetail>) this.DummyAttributeValue).Insert(new CSAttributeDetail()
    {
      AttributeID = "~MX~DUMMY~",
      ValueID = "Value"
    });
    newDetail.Description = newDetail.ValueID;
    DBMatrixLocalizableDescriptionAttribute.SetTranslations<CSAttribute.description>(((PXSelectBase) this.DummyAttributeValue).Cache, (object) newDetail, (Func<string, string>) (locale => newDetail.Description));
    ((PXSelectBase<CSAttributeDetail>) this.DummyAttributeValue).Update(newDetail);
  }

  public override void Persist()
  {
    this.UpdateMatrixAttributes();
    base.Persist();
  }

  private void UpdateMatrixAttributes()
  {
    string[] strArray = this.DeactivateMatrixVariantAttributes();
    if (strArray == null || !((IEnumerable<string>) strArray).Any<string>())
      return;
    this.AssignDummyAttributeWhenOnlyOneVariantRemains(strArray);
  }

  private string[] DeactivateMatrixVariantAttributes()
  {
    string[] array = GraphHelper.RowCast<CSAnswers>(((PXSelectBase) this.Answers).Cache.Cached).Where<CSAnswers>((Func<CSAnswers, bool>) (x =>
    {
      if (!EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Answers).Cache.GetStatus((object) x), (PXEntryStatus) 2, (PXEntryStatus) 1) || !string.Equals(this.GetAttributeCategory(((PXSelectBase) this.Answers).Cache, x), "V", StringComparison.OrdinalIgnoreCase) || x.IsActive.GetValueOrDefault())
        return false;
      bool? valueOriginal = (bool?) ((PXSelectBase) this.Answers).Cache.GetValueOriginal<CSAnswers.isActive>((object) x);
      bool? isActive = x.IsActive;
      return !(valueOriginal.GetValueOrDefault() == isActive.GetValueOrDefault() & valueOriginal.HasValue == isActive.HasValue);
    })).Select<CSAnswers, string>((Func<CSAnswers, string>) (x => x.AttributeID)).ToArray<string>();
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) array))
      return (string[]) null;
    PXResultset<CSAnswers> pxResultset = ((PXSelectBase<CSAnswers>) new FbqlSelect<SelectFromBase<CSAnswers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<PX.Objects.IN.InventoryItem.noteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.templateItemID, Equal<P.AsInt>>>>>.And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsIn<P.AsString>>>, CSAnswers>.View((PXGraph) this)).Select(new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current.InventoryID,
      (object) array
    });
    if (pxResultset.Count == 0)
      return (string[]) null;
    foreach (PXResult<CSAnswers> pxResult in pxResultset)
      ((PXSelectBase) this.Answers).Cache.Delete((object) PXResult<CSAnswers>.op_Implicit(pxResult));
    return array;
  }

  private void AssignDummyAttributeWhenOnlyOneVariantRemains(string[] deactivatedVarianAttributes)
  {
    PXResultset<PX.Objects.IN.InventoryItem> pxResultset = ((PXSelectBase<PX.Objects.IN.InventoryItem>) new FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAnswers>.On<BqlOperand<PX.Objects.IN.InventoryItem.noteID, IBqlGuid>.IsEqual<CSAnswers.refNoteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.templateItemID, Equal<P.AsInt>>>>>.And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsNotIn<P.AsString>>>.Aggregate<To<GroupBy<CSAnswers.refNoteID>, Count<CSAnswers.attributeID>>>.Having<BqlAggregatedOperand<Count<CSAnswers.attributeID>, IBqlInt>.IsEqual<int1>>, PX.Objects.IN.InventoryItem>.View((PXGraph) this)).Select(new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current.InventoryID,
      (object) deactivatedVarianAttributes
    });
    CreateMatrixItemsHelper matrixItemsHelper = this.GetCreateMatrixItemsHelper();
    foreach (PXResult<PX.Objects.IN.InventoryItem> pxResult in pxResultset)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      matrixItemsHelper.AssignDummyAttribute((InventoryItemMaintBase) this, ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Item).Current, inventoryItem.NoteID);
    }
  }

  public class CurySettings : 
    CurySettingsExtension<TemplateInventoryItemMaint, PX.Objects.IN.InventoryItem, InventoryItemCurySettings>
  {
    public static bool IsActive() => true;
  }
}
