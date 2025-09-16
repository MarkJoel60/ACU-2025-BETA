// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemClassMaintExt.VariantAttributesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INItemClassMaintExt;

public class VariantAttributesExt : PXGraphExtension<INItemClassMaint>
{
  protected Lazy<bool> _hasTemplateWithChild;
  protected Lazy<bool> _childItemClassHasTemplateWithItems;

  public virtual void Initialize()
  {
    this._hasTemplateWithChild = new Lazy<bool>(new Func<bool>(this.HasTemplateWithChild));
    ((PXGraphExtension) this).Initialize();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  protected virtual void _(Events.RowSelected<CSAttributeGroup> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CSAttributeGroup.attributeCategory>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CSAttributeGroup>>) eventArgs).Cache, (object) eventArgs.Row, eventArgs.Row.ControlType.GetValueOrDefault() == 2);
    PXUIFieldAttribute.SetEnabled<CSAttributeGroup.required>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CSAttributeGroup>>) eventArgs).Cache, (object) eventArgs.Row, eventArgs.Row.AttributeCategory != "V");
  }

  protected virtual void _(
    Events.FieldUpdated<CSAttributeGroup, CSAttributeGroup.attributeID> eventArgs)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CSAttributeGroup, CSAttributeGroup.attributeID>>) eventArgs).Cache.SetDefaultExt<CSAttributeGroup.attributeCategory>((object) eventArgs.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<CSAttributeGroup, CSAttributeGroup.attributeCategory> eventArgs)
  {
    if (!(eventArgs.Row?.AttributeCategory == "V"))
      return;
    eventArgs.Row.Required = new bool?(false);
  }

  protected virtual void _(Events.RowInserted<CSAttributeGroup> eventArgs)
  {
    CSAttributeGroup row = eventArgs.Row;
    if (row == null)
      return;
    int? entityClassID = eventArgs.Row.EntityClassID != null ? new int?(int.Parse(eventArgs.Row.EntityClassID)) : new int?();
    this.AddInactiveVarianAttributeToChildren(row, this._hasTemplateWithChild, entityClassID, false);
  }

  protected virtual void _(Events.RowUpdated<CSAttributeGroup> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    int? entityClassID = eventArgs.Row.EntityClassID != null ? new int?(int.Parse(eventArgs.Row.EntityClassID)) : new int?();
    this.UpdateChildVariantsIfNeeded(eventArgs.Row, eventArgs.OldRow, this._hasTemplateWithChild, entityClassID, false);
  }

  private void UpdateChildVariantsIfNeeded(
    CSAttributeGroup row,
    CSAttributeGroup oldRow,
    Lazy<bool> hasTemplateWithChild,
    int? entityClassID,
    bool updateDB)
  {
    bool? isActive1 = row.IsActive;
    bool? isActive2 = oldRow.IsActive;
    if (!(isActive1.GetValueOrDefault() == isActive2.GetValueOrDefault() & isActive1.HasValue == isActive2.HasValue) || row.AttributeCategory != oldRow.AttributeCategory)
      this.AddInactiveVarianAttributeToChildren(row, hasTemplateWithChild, entityClassID, updateDB);
    if (row.AttributeCategory == "V")
    {
      bool? isActive3 = row.IsActive;
      bool flag = false;
      if (isActive3.GetValueOrDefault() == flag & isActive3.HasValue)
      {
        isActive3 = row.IsActive;
        bool? isActive4 = oldRow.IsActive;
        if (!(isActive3.GetValueOrDefault() == isActive4.GetValueOrDefault() & isActive3.HasValue == isActive4.HasValue))
          goto label_6;
      }
    }
    if (!(row.AttributeCategory == "A") || !(row.AttributeCategory != oldRow.AttributeCategory))
      return;
label_6:
    this.DeleteAttributeFromTemplates(row, entityClassID, updateDB);
  }

  private void AddInactiveVarianAttributeToChildren(
    CSAttributeGroup row,
    Lazy<bool> hasTemplateWithChild,
    int? entityClassID,
    bool updateDB)
  {
    if (row.AttributeCategory != "V")
      return;
    bool? isActive = row.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue || !hasTemplateWithChild.Value)
      return;
    foreach (PXResult<InventoryItem> pxResult in PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TemplateChildInventoryItem>.On<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<TemplateChildInventoryItem.templateItemID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TemplateChildInventoryItem.itemClassID, Equal<P.AsInt>>>>, And<BqlOperand<TemplateChildInventoryItem.isTemplate, IBqlBool>.IsEqual<boolFalse>>>>.And<BqlOperand<TemplateChildInventoryItem.templateItemID, IBqlInt>.IsNotNull>>.Aggregate<To<GroupBy<InventoryItem.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) entityClassID
    }))
    {
      InventoryItem inventoryItem = PXResult<InventoryItem>.op_Implicit(pxResult);
      CSAnswers csAnswers = new CSAnswers()
      {
        AttributeID = row.AttributeID,
        RefNoteID = inventoryItem.NoteID,
        IsActive = new bool?(false)
      };
      if (updateDB)
      {
        if (!PXDatabase.Update<CSAnswers>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldRestrict<CSAnswers.attributeID>((object) csAnswers.AttributeID),
          (PXDataFieldParam) new PXDataFieldRestrict<CSAnswers.refNoteID>((object) csAnswers.RefNoteID),
          (PXDataFieldParam) new PXDataFieldAssign<CSAnswers.isActive>((object) csAnswers.IsActive)
        }))
          PXDatabase.Insert<CSAnswers>(new PXDataFieldAssign[4]
          {
            (PXDataFieldAssign) new PXDataFieldAssign<CSAnswers.attributeID>((object) csAnswers.AttributeID),
            (PXDataFieldAssign) new PXDataFieldAssign<CSAnswers.refNoteID>((object) csAnswers.RefNoteID),
            (PXDataFieldAssign) new PXDataFieldAssign<CSAnswers.isActive>((object) csAnswers.IsActive),
            new PXDataFieldAssign("NoteID", (PXDbType) 14, (object) Guid.NewGuid())
          });
      }
      else
        GraphHelper.Caches<CSAnswers>((PXGraph) this.Base).Update(csAnswers);
    }
  }

  private void DeleteAttributeFromTemplates(
    CSAttributeGroup row,
    int? entityClassID,
    bool updateDB)
  {
    foreach (PXResult<CSAnswers> pxResult in PXSelectBase<CSAnswers, PXViewOf<CSAnswers>.BasedOn<SelectFromBase<CSAnswers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.itemClassID, Equal<P.AsInt>>>>, And<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<boolTrue>>>>.And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) entityClassID,
      (object) row.AttributeID
    }))
    {
      CSAnswers csAnswers = PXResult<CSAnswers>.op_Implicit(pxResult);
      if (updateDB)
        PXDatabase.Delete<CSAnswers>(new PXDataFieldRestrict[2]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<CSAnswers.attributeID>((object) csAnswers.AttributeID),
          (PXDataFieldRestrict) new PXDataFieldRestrict<CSAnswers.refNoteID>((object) csAnswers.RefNoteID)
        });
      else
        GraphHelper.Caches<CSAnswers>((PXGraph) this.Base).Delete(csAnswers);
    }
  }

  protected virtual void _(Events.RowPersisting<CSAttributeGroup> eventArgs)
  {
    CSAttributeGroup row = eventArgs.Row;
    if (row == null)
      return;
    switch ((PXDBOperation) (eventArgs.Operation & 3) - 1)
    {
      case 0:
        this.ValidateUpdate(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CSAttributeGroup>>) eventArgs).Cache, this._hasTemplateWithChild, row);
        break;
      case 1:
        this.ValidateInsert(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CSAttributeGroup>>) eventArgs).Cache, this._hasTemplateWithChild, row);
        break;
      case 2:
        this.ValidateDelete(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CSAttributeGroup>>) eventArgs).Cache, this._hasTemplateWithChild, row, true);
        break;
    }
  }

  protected virtual void ValidateInsert(
    PXCache cache,
    Lazy<bool> hasTemplateWithChild,
    CSAttributeGroup row,
    bool throwException = false)
  {
  }

  protected virtual void ValidateUpdate(
    PXCache cache,
    Lazy<bool> hasTemplateWithChild,
    CSAttributeGroup row,
    bool throwException = false,
    CSAttributeGroup oldRow = null)
  {
    if (oldRow == null)
      oldRow = CSAttributeGroup.PK.Find((PXGraph) this.Base, row.AttributeID, row.EntityClassID, row.EntityType);
    this.ValidateActiveAttributeCategoryUpdate(cache, hasTemplateWithChild, row, throwException, oldRow);
    this.ValidateVariantAttributeIsActiveUpdate(cache, hasTemplateWithChild, row, throwException, oldRow);
  }

  private void ValidateActiveAttributeCategoryUpdate(
    PXCache cache,
    Lazy<bool> hasTemplateWithChild,
    CSAttributeGroup row,
    bool throwException,
    CSAttributeGroup oldRow)
  {
    string templateIDs;
    bool flag = this.IsAttributeDefaultRowColumnAttribute(oldRow, out templateIDs);
    if (oldRow == null || !(oldRow.AttributeCategory != row.AttributeCategory) || !row.IsActive.GetValueOrDefault() || !(hasTemplateWithChild.Value | flag) || row.AttributeCategory == "V" || row.AttributeCategory == "A" && !flag && hasTemplateWithChild.Value && this.VariantCanBeDeactivated(oldRow.EntityClassID, oldRow.AttributeID))
      return;
    PXSetPropertyException<CSAttributeGroup.attributeCategory> propertyException1;
    if (!flag)
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The value in the Category column cannot be changed if at least one matrix item exists with this item class assigned. Remove all matrix items of the template item first.");
    else
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The attribute category cannot be changed because this attribute is specified as the default column or row attribute for the following templates on the Template Items (IN203000) form: {0}. Select another attribute as the default column or row attribute for the templates first.", new object[1]
      {
        (object) templateIDs
      });
    PXSetPropertyException<CSAttributeGroup.attributeCategory> propertyException2 = propertyException1;
    if (throwException)
      throw propertyException2;
    cache.RaiseExceptionHandling<CSAttributeGroup.attributeCategory>((object) row, (object) row.AttributeCategory, (Exception) propertyException2);
  }

  private void ValidateVariantAttributeIsActiveUpdate(
    PXCache cache,
    Lazy<bool> hasTemplateWithChild,
    CSAttributeGroup row,
    bool throwException,
    CSAttributeGroup oldRow)
  {
    string templateIDs;
    bool flag = this.IsAttributeDefaultRowColumnAttribute(oldRow, out templateIDs);
    if (!(oldRow?.AttributeCategory == "V"))
      return;
    bool? isActive1 = oldRow.IsActive;
    bool? isActive2 = row.IsActive;
    if (isActive1.GetValueOrDefault() == isActive2.GetValueOrDefault() & isActive1.HasValue == isActive2.HasValue || !(hasTemplateWithChild.Value | flag))
      return;
    isActive2 = row.IsActive;
    if (isActive2.GetValueOrDefault() || !flag && hasTemplateWithChild.Value && this.VariantCanBeDeactivated(oldRow.EntityClassID, oldRow.AttributeID))
      return;
    PXSetPropertyException<CSAttributeGroup.isActive> propertyException1;
    if (!flag)
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.isActive>("The value of the Active check box for the variant attribute cannot be changed if at least one matrix item exists with this item class assigned. Remove all matrix items of the template item first.");
    else
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.isActive>("The Active check box cannot be cleared for this attribute because it is specified as the default column or row attribute for the following templates on the Template Items (IN203000) form: {0}. Select another attribute as the default column or row attribute for the templates first.", new object[1]
      {
        (object) templateIDs
      });
    PXSetPropertyException<CSAttributeGroup.isActive> propertyException2 = propertyException1;
    if (throwException)
      throw propertyException2;
    cache.RaiseExceptionHandling<CSAttributeGroup.isActive>((object) row, (object) row.IsActive, (Exception) propertyException2);
  }

  protected virtual void ValidateDelete(
    PXCache cache,
    Lazy<bool> hasTemplateWithChild,
    CSAttributeGroup row,
    bool throwException = false)
  {
    string templateIDs = string.Empty;
    CSAttributeGroup attributeGroup = CSAttributeGroup.PK.Find((PXGraph) this.Base, row.AttributeID, row.EntityClassID, row.EntityType);
    if (!(attributeGroup?.AttributeCategory == "V") || !attributeGroup.IsActive.GetValueOrDefault() || !hasTemplateWithChild.Value && !this.IsAttributeDefaultRowColumnAttribute(attributeGroup, out templateIDs) || hasTemplateWithChild.Value && this.VariantCanBeDeactivated(attributeGroup.EntityClassID, attributeGroup.AttributeID))
      return;
    PXSetPropertyException<CSAttributeGroup.attributeCategory> propertyException1;
    if (!hasTemplateWithChild.Value)
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The {0} attribute cannot be deleted because it is specified as the default column or row attribute for the following templates on the Template Items (IN203000) form: {1}. Select another attribute as the default column or row attribute for all the templates first.", new object[2]
      {
        (object) row.AttributeID,
        (object) templateIDs
      });
    else
      propertyException1 = new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The {0} attribute cannot be deleted because it is a variant attribute and at least one matrix item exists with this item class assigned. Remove all matrix items of the template item first.", new object[1]
      {
        (object) row.AttributeID
      });
    PXSetPropertyException<CSAttributeGroup.attributeCategory> propertyException2 = propertyException1;
    if (throwException)
      throw propertyException2;
    cache.RaiseExceptionHandling<CSAttributeGroup.attributeCategory>((object) row, (object) row.AttributeCategory, (Exception) propertyException2);
  }

  private bool VariantCanBeDeactivated(string classID, string attributeID)
  {
    return PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAnswers>.On<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.itemClassID, Equal<P.AsInt>>>>, And<BqlOperand<InventoryItem.isTemplate, IBqlBool>.IsEqual<boolFalse>>>, And<BqlOperand<InventoryItem.templateItemID, IBqlInt>.IsNotNull>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<CSAnswers.isActive, IBqlBool>.IsEqual<boolTrue>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) classID,
      (object) attributeID
    })) == null;
  }

  protected virtual bool IsAttributeDefaultRowColumnAttribute(
    CSAttributeGroup attributeGroup,
    out string templateIDs)
  {
    if (attributeGroup?.EntityType != typeof (InventoryItem).FullName)
    {
      templateIDs = (string) null;
      return false;
    }
    PXResultset<InventoryItem> pxResultset = PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.isTemplate, Equal<True>, And<InventoryItem.itemClassID, Equal<Required<CSAttributeGroup.entityClassID>>, And<Where<InventoryItem.defaultColumnMatrixAttributeID, Equal<Required<CSAttributeGroup.attributeID>>, Or<InventoryItem.defaultRowMatrixAttributeID, Equal<Required<CSAttributeGroup.attributeID>>>>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 10, new object[3]
    {
      (object) attributeGroup.EntityClassID,
      (object) attributeGroup.AttributeID,
      (object) attributeGroup.AttributeID
    });
    if (pxResultset.Count == 0)
    {
      templateIDs = (string) null;
      return false;
    }
    templateIDs = string.Join(", ", GraphHelper.RowCast<InventoryItem>((IEnumerable) pxResultset).Select<InventoryItem, string>((Func<InventoryItem, string>) (s => s.InventoryCD)));
    return true;
  }

  protected virtual void _(Events.RowDeleting<CSAttributeGroup> eventArgs)
  {
    CSAttributeGroup row = eventArgs.Row;
    if (row == null)
      return;
    CSAttributeGroup attributeGroup = CSAttributeGroup.PK.Find((PXGraph) this.Base, row.AttributeID, row.EntityClassID, row.EntityType);
    if (!(attributeGroup?.AttributeCategory == "V"))
      return;
    string templateIDs;
    bool flag = this.IsAttributeDefaultRowColumnAttribute(attributeGroup, out templateIDs);
    if (!flag && this._hasTemplateWithChild.Value && this.VariantCanBeDeactivated(attributeGroup.EntityClassID, attributeGroup.AttributeID))
      return;
    if (flag)
      throw new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The {0} attribute cannot be deleted because it is specified as the default column or row attribute for the following templates on the Template Items (IN203000) form: {1}. Select another attribute as the default column or row attribute for all the templates first.", new object[2]
      {
        (object) row.AttributeID,
        (object) templateIDs
      });
    if (this._hasTemplateWithChild.Value)
      throw new PXSetPropertyException<CSAttributeGroup.attributeCategory>("The {0} attribute cannot be deleted because it is a variant attribute and at least one matrix item exists with this item class assigned. Remove all matrix items of the template item first.", new object[1]
      {
        (object) row.AttributeID
      });
  }

  protected virtual bool HasTemplateWithChild(int? itemClassID, bool clearQueryCache)
  {
    PXSelectReadonly<InventoryItem, Where<InventoryItem.itemClassID, Equal<Required<InventoryItem.itemClassID>>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNotNull>>>> pxSelectReadonly = new PXSelectReadonly<InventoryItem, Where<InventoryItem.itemClassID, Equal<Required<InventoryItem.itemClassID>>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNotNull>>>>((PXGraph) this.Base);
    if (clearQueryCache)
      ((PXSelectBase) pxSelectReadonly).Cache.ClearQueryCache();
    return ((PXSelectBase<InventoryItem>) pxSelectReadonly).SelectSingle(new object[1]
    {
      (object) itemClassID
    }) != null;
  }

  protected virtual bool HasTemplateWithChild()
  {
    return this.HasTemplateWithChild(((PXSelectBase<INItemClass>) this.Base.itemclass).Current.ItemClassID, true);
  }

  [PXOverride]
  public virtual void MergeAttributes(
    INItemClass child,
    IEnumerable<CSAttributeGroup> attributesTemplate,
    Action<INItemClass, IEnumerable<CSAttributeGroup>> baseMethod)
  {
    int? childItemClassID = child.ItemClassID;
    this._childItemClassHasTemplateWithItems = new Lazy<bool>((Func<bool>) (() => this.HasTemplateWithChild(childItemClassID, false)));
    baseMethod(child, attributesTemplate);
  }

  [PXOverride]
  public virtual void MergeAttribute(
    INItemClass child,
    CSAttributeGroup existingAttribute,
    CSAttributeGroup attr,
    Action<INItemClass, CSAttributeGroup, CSAttributeGroup> baseMethod)
  {
    if (existingAttribute == null)
    {
      this.ValidateInsert(((PXSelectBase) this.Base.Mapping).Cache, this._childItemClassHasTemplateWithItems, attr, true);
      this.AddInactiveVarianAttributeToChildren(attr, this._childItemClassHasTemplateWithItems, child.ItemClassID, true);
    }
    else
    {
      this.ValidateUpdate(((PXSelectBase) this.Base.Mapping).Cache, this._childItemClassHasTemplateWithItems, attr, true, existingAttribute);
      this.UpdateChildVariantsIfNeeded(attr, existingAttribute, this._childItemClassHasTemplateWithItems, child.ItemClassID, true);
    }
    baseMethod(child, existingAttribute, attr);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.INItemClassMaint.Persist" />
  /// </summary>
  [PXOverride]
  public virtual void Persist(Action basePersist)
  {
    PXCache groupCache = ((PXSelectBase) this.Base.Mapping).Cache;
    int num = GraphHelper.RowCast<CSAttributeGroup>(groupCache.Updated).Any<CSAttributeGroup>((Func<CSAttributeGroup, bool>) (g =>
    {
      if (!(g.AttributeCategory == "V"))
        return false;
      short? nullable1 = g.SortOrder;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = (short?) groupCache.GetValueOriginal<CSAttributeGroup.sortOrder>((object) g);
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      return !(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue);
    })) ? 1 : 0;
    if (basePersist != null)
      basePersist();
    if (num == 0 || !this._hasTemplateWithChild.Value)
      return;
    this.RecalcAttributeDescriptionGroup(((PXSelectBase<INItemClass>) this.Base.itemclass).Current.ItemClassID);
  }

  public virtual void RecalcAttributeDescriptionGroup(int? itemClassID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new VariantAttributesExt.\u003C\u003Ec__DisplayClass26_0()
    {
      itemClassID = itemClassID
    }, __methodptr(\u003CRecalcAttributeDescriptionGroup\u003Eb__0)));
  }
}
