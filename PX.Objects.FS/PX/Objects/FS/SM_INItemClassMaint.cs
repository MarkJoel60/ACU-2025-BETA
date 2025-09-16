// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_INItemClassMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_INItemClassMaint : PXGraphExtension<INItemClassMaint>
{
  public PXSelect<FSModelTemplateComponent, Where<FSModelTemplateComponent.modelTemplateID, Equal<Current<INItemClass.itemClassID>>>> ModelTemplateComponentRecords;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
  }

  /// <summary>
  /// Enables/Disables the Item Type field depending of there is at least one service related to.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="itemClassRow">The current INItemClass object row.</param>
  /// <param name="fsxServiceClassRow">The current <c>FSxServiceClass</c> object row.</param>
  public virtual void EnableDisable_ItemType(
    PXCache cache,
    INItemClass itemClassRow,
    FSxServiceClass fsxServiceClassRow)
  {
    bool flag = true;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (itemClassRow.ItemType == "S" && cache.GetStatus((object) itemClassRow) != 2)
      flag = PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>>, Where<PX.Objects.IN.InventoryItem.itemClassID, Equal<Required<PX.Objects.IN.InventoryItem.itemClassID>>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
      {
        (object) itemClassRow.ItemClassID
      }).Count == 0;
    if (!flag)
      propertyException = new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("This element cannot be modified because there are {0} related to this {1}.", new object[2]
      {
        (object) "Services",
        (object) "Item Class"
      }), (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<INItemClass.itemType>((object) itemClassRow, (object) itemClassRow.ItemType, (Exception) propertyException);
    cache.RaiseExceptionHandling<FSxServiceClass.requireRoute>((object) itemClassRow, (object) fsxServiceClassRow.RequireRoute, (Exception) propertyException);
    PXUIFieldAttribute.SetEnabled<INItemClass.itemType>(cache, (object) itemClassRow, flag);
    PXUIFieldAttribute.SetEnabled<FSxServiceClass.requireRoute>(cache, (object) itemClassRow, flag);
  }

  /// <summary>Enables or disables fields.</summary>
  public virtual void EnableDisable(PXCache cache, INItemClass itemClassRow)
  {
    bool flag = false;
    bool valueOrDefault = itemClassRow.StkItem.GetValueOrDefault();
    FSxServiceClass extension1 = cache.GetExtension<FSxServiceClass>((object) itemClassRow);
    PXUIFieldAttribute.SetEnabled<FSxServiceClass.defaultBillingRule>(cache, (object) itemClassRow, itemClassRow.ItemType == "S");
    this.EnableDisable_ItemType(cache, itemClassRow, extension1);
    ((PXSelectBase) this.ModelTemplateComponentRecords).AllowSelect = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
    {
      FSxEquipmentModelTemplate extension2 = cache.GetExtension<FSxEquipmentModelTemplate>((object) itemClassRow);
      flag = extension2.EQEnabled.GetValueOrDefault() && extension2.EquipmentItemClass == "ME";
      extension2.Mem_ShowComponent = flag;
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModelTemplate.equipmentItemClass>(cache, (object) itemClassRow, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModelTemplate.eQEnabled>(cache, (object) itemClassRow, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModelTemplate.defaultEquipmentModelType>(cache, (object) itemClassRow, flag);
      PXDefaultAttribute.SetPersistingCheck<FSxEquipmentModelTemplate.defaultEquipmentModelType>(cache, (object) itemClassRow, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      ((PXSelectBase) this.ModelTemplateComponentRecords).AllowSelect = extension2.Mem_ShowComponent;
    }
    ((PXSelectBase) this.ModelTemplateComponentRecords).Cache.AllowInsert = flag;
    ((PXSelectBase) this.ModelTemplateComponentRecords).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.ModelTemplateComponentRecords).Cache.AllowDelete = flag;
    if (flag)
      return;
    ((PXSelectBase) this.ModelTemplateComponentRecords).Cache.Clear();
  }

  public virtual void CheckComponentsClassID(
    FSxEquipmentModelTemplate fsxEquipmentModelTemplateRow)
  {
    if (fsxEquipmentModelTemplateRow == null && fsxEquipmentModelTemplateRow.EquipmentItemClass != "ME")
      return;
    foreach (PXResult<FSModelTemplateComponent> pxResult in ((PXSelectBase<FSModelTemplateComponent>) this.ModelTemplateComponentRecords).Select(Array.Empty<object>()))
    {
      FSModelTemplateComponent templateComponent = PXResult<FSModelTemplateComponent>.op_Implicit(pxResult);
      if (((PXSelectBase) this.ModelTemplateComponentRecords).Cache.GetStatus((object) templateComponent) == null && !templateComponent.ClassID.HasValue)
        ((PXSelectBase) this.ModelTemplateComponentRecords).Cache.SetStatus((object) templateComponent, (PXEntryStatus) 1);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INItemClass, FSxServiceClass.defaultBillingRule> e)
  {
    if (e.Row == null)
      return;
    bool? stkItem = e.Row.StkItem;
    if (!stkItem.GetValueOrDefault())
    {
      stkItem = e.Row.StkItem;
      bool flag = false;
      if (!(stkItem.GetValueOrDefault() == flag & stkItem.HasValue) || !(e.Row.ItemType != "S"))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INItemClass, FSxServiceClass.defaultBillingRule>, INItemClass, object>) e).NewValue = (object) "TIME";
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INItemClass, FSxServiceClass.defaultBillingRule>, INItemClass, object>) e).NewValue = (object) "FLRA";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemClass, INItemClass.itemType> e)
  {
    if (e.Row == null)
      return;
    INItemClass row = e.Row;
    FSxServiceClass extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.itemType>>) e).Cache.GetExtension<FSxServiceClass>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.itemType>>) e).Cache.SetDefaultExt<FSxServiceClass.defaultBillingRule>((object) e.Row);
    if (!(row.ItemType != "S"))
      return;
    extension.RequireRoute = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemClass, INItemClass.stkItem>>) e).Cache.SetDefaultExt<FSxServiceClass.defaultBillingRule>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemClass> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisable(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClass>>) e).Cache, e.Row);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModelTemplate.equipmentItemClass>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemClass>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
  }

  protected virtual void _(PX.Data.Events.RowInserting<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INItemClass> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemClass> e)
  {
    if (e.Row == null)
      return;
    INItemClass row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INItemClass>>) e).Cache;
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    this.CheckComponentsClassID(cache.GetExtension<FSxEquipmentModelTemplate>((object) row));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<INItemClass> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSModelTemplateComponent, FSModelTemplateComponent.qty> e)
  {
    if (e.Row != null && (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSModelTemplateComponent, FSModelTemplateComponent.qty>, FSModelTemplateComponent, object>) e).NewValue < 1)
      throw new PXSetPropertyException("The quantity must be greater than 0.");
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSModelTemplateComponent> e)
  {
    if (e.Row == null)
      return;
    FSModelTemplateComponent row = e.Row;
    if (PXResultset<FSModelTemplateComponent>.op_Implicit(PXSelectBase<FSModelTemplateComponent, PXSelect<FSModelTemplateComponent, Where<FSModelTemplateComponent.componentCD, Equal<Required<FSModelTemplateComponent.componentCD>>, And<FSModelTemplateComponent.modelTemplateID, Equal<Current<FSModelTemplateComponent.modelTemplateID>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) row.ComponentCD
    })) == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSModelTemplateComponent>>) e).Cache.RaiseExceptionHandling<FSModelTemplateComponent.componentCD>((object) e.Row, (object) row.ComponentCD, (Exception) new PXException("This ID is already in use."));
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSModelTemplateComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSModelTemplateComponent> e)
  {
  }
}
