// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_InventoryItemMaint
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

public class SM_InventoryItemMaint : PXGraphExtension<InventoryItemMaint>
{
  public PXSelect<FSModelComponent, Where<FSModelComponent.modelID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>> ModelComponents;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <summary>
  /// Manages the <c>SetEnabled</c> attribute for the <c>eQEnabled</c>, <c>manufacturerID</c>, <c>modelType</c> and <c>hasWarranty</c> fields of the <c>FSxEquipmentModel</c> DAC.
  /// </summary>
  public virtual void EnableDisable_InventoryItem(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    bool flag1 = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
    {
      FSxEquipmentModel extension = cache.GetExtension<FSxEquipmentModel>((object) inventoryItemRow);
      bool flag2 = extension.EquipmentItemClass == "ME" || extension.EquipmentItemClass == "CT";
      flag1 = extension.EQEnabled.GetValueOrDefault() && extension.EquipmentItemClass == "ME";
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.eQEnabled>(cache, (object) inventoryItemRow, false);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.manufacturerID>(cache, (object) inventoryItemRow, extension.EquipmentItemClass == "ME" || extension.EquipmentItemClass == "CT");
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.manufacturerModelID>(cache, (object) inventoryItemRow, extension.EquipmentItemClass == "ME" || extension.EquipmentItemClass == "CT");
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.equipmentTypeID>(cache, (object) inventoryItemRow, extension.EquipmentItemClass == "ME");
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.cpnyWarrantyType>(cache, (object) inventoryItemRow, flag2);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.cpnyWarrantyValue>(cache, (object) inventoryItemRow, flag2);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.vendorWarrantyType>(cache, (object) inventoryItemRow, flag2);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.vendorWarrantyValue>(cache, (object) inventoryItemRow, flag2);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.modelType>(cache, (object) inventoryItemRow, false);
      PXUIFieldAttribute.SetEnabled<FSxEquipmentModel.equipmentItemClass>(cache, (object) inventoryItemRow, false);
      extension.Mem_ShowComponent = flag1;
    }
    ((PXSelectBase) this.ModelComponents).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.ModelComponents).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ModelComponents).Cache.AllowDelete = flag1;
  }

  /// <summary>
  /// Manages the <c>SetEnabled</c> attribute for the <c>componentCD</c>, <c>descr</c>, <c>vendorWarrantyDuration</c>, <c>vendorID</c> and <c>cpnyWarrantyDuration</c> fields of the <c>FSModelComponent</c> DAC.
  /// </summary>
  public virtual void EnableDisable_FSModelComponent(
    PXCache cache,
    FSModelComponent fsModelComponentRow)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || !fsModelComponentRow.ComponentID.HasValue)
      return;
    bool valueOrDefault = fsModelComponentRow.Active.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<FSModelComponent.componentID>(cache, (object) fsModelComponentRow, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSModelComponent.descr>(cache, (object) fsModelComponentRow, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSModelComponent.classID>(cache, (object) fsModelComponentRow, !fsModelComponentRow.ClassID.HasValue);
    PXUIFieldAttribute.SetEnabled<FSModelComponent.requireSerial>(cache, (object) fsModelComponentRow, valueOrDefault);
  }

  /// <summary>
  /// Reset the values on the 'Components' grid and loads the Component registers from the selected 'ItemClass' for the current 'InventoryItem' ('StockItem').
  /// </summary>
  public virtual void ResetValuesFromItemClass(
    PXCache cache,
    PX.Objects.IN.InventoryItem inventoryItemRow,
    int? itemClassID)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || inventoryItemRow == null || !inventoryItemRow.ItemClassID.HasValue)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      FSxEquipmentModelTemplate extension1 = ((PXSelectBase) this.Base.ItemClass).Cache.GetExtension<FSxEquipmentModelTemplate>((object) PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelect<INItemClass, Where<INItemClass.itemClassID, Equal<Required<INItemClass.itemClassID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) itemClassID
      })));
      FSxEquipmentModel extension2 = cache.GetExtension<FSxEquipmentModel>((object) inventoryItemRow);
      PXResultset<FSModelTemplateComponent> pxResultset = PXSelectBase<FSModelTemplateComponent, PXSelect<FSModelTemplateComponent, Where<FSModelTemplateComponent.modelTemplateID, Equal<Required<FSModelTemplateComponent.modelTemplateID>>, And<FSModelTemplateComponent.active, Equal<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) itemClassID
      });
      extension2.EQEnabled = extension1.EQEnabled;
      extension2.EquipmentItemClass = extension1.EquipmentItemClass;
      extension2.ModelType = extension1.DefaultEquipmentModelType;
      foreach (PXResult<FSModelComponent> pxResult in ((PXSelectBase<FSModelComponent>) this.ModelComponents).Select(Array.Empty<object>()))
        ((PXSelectBase<FSModelComponent>) this.ModelComponents).Delete(PXResult<FSModelComponent>.op_Implicit(pxResult));
      foreach (PXResult<FSModelTemplateComponent> pxResult in pxResultset)
      {
        FSModelTemplateComponent templateComponent = PXResult<FSModelTemplateComponent>.op_Implicit(pxResult);
        if (extension1.EQEnabled.GetValueOrDefault())
          ((PXSelectBase) this.ModelComponents).Cache.Insert((object) new FSModelComponent()
          {
            Active = templateComponent.Active,
            ComponentID = templateComponent.ComponentID,
            Descr = templateComponent.Descr,
            ClassID = templateComponent.ClassID,
            Optional = templateComponent.Optional,
            Qty = templateComponent.Qty
          });
      }
      transactionScope.Complete();
    }
  }

  /// <summary>Show or Hide Model Fields.</summary>
  public virtual void ShowOrHideFields(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    FSxEquipmentModel extension = cache.GetExtension<FSxEquipmentModel>((object) inventoryItemRow);
    bool flag = extension.EquipmentItemClass != "OI" && extension.EquipmentItemClass != "CE";
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.manufacturerID>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.manufacturerModelID>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.equipmentTypeID>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.cpnyWarrantyValue>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.cpnyWarrantyType>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.vendorWarrantyValue>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxEquipmentModel.vendorWarrantyType>(cache, (object) inventoryItemRow, flag);
  }

  /// <summary>Show or hide Components tab.</summary>
  /// <param name="cache">Cache of the Inventory Item.</param>
  /// <param name="inventoryItemRow">Inventory Item Row.</param>
  public virtual void ShowOrHideComponetsTab(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    ((PXSelectBase) this.ModelComponents).AllowSelect = cache.GetExtension<FSxEquipmentModel>((object) inventoryItemRow).Mem_ShowComponent;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemClassID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem row = e.Row;
    this.ResetValuesFromItemClass(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemClassID>>) e).Cache, row, row.ItemClassID);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache;
    this.ShowOrHideFields(cache, row);
    this.EnableDisable_InventoryItem(cache, row);
    this.ShowOrHideComponetsTab(cache, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.IN.InventoryItem> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem row = e.Row;
    this.ResetValuesFromItemClass(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.IN.InventoryItem>>) e).Cache, row, row.ItemClassID);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSModelComponent, FSModelComponent.componentID> e)
  {
    if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    FSModelComponent row = e.Row;
    if (!row.ComponentID.HasValue)
      return;
    FSModelTemplateComponent templateComponent = PXResultset<FSModelTemplateComponent>.op_Implicit(PXSelectBase<FSModelTemplateComponent, PXSelect<FSModelTemplateComponent, Where<FSModelTemplateComponent.componentID, Equal<Required<FSModelTemplateComponent.componentID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.ComponentID
    }));
    row.Active = templateComponent.Active;
    row.ComponentID = templateComponent.ComponentID;
    row.Descr = templateComponent.Descr;
    row.ClassID = templateComponent.ClassID;
    row.Optional = templateComponent.Optional;
    row.Qty = templateComponent.Qty;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSModelComponent, FSModelComponent.inventoryID> e)
  {
    if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    FSModelComponent row = e.Row;
    if (!row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) this.Base, row.InventoryID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, row.InventoryID, ((PXGraph) this.Base).Accessinfo.BaseCuryID);
    FSxEquipmentModel extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItemRow);
    row.CpnyWarrantyValue = extension.CpnyWarrantyValue;
    row.CpnyWarrantyType = extension.CpnyWarrantyType;
    row.VendorWarrantyValue = extension.VendorWarrantyValue;
    row.VendorWarrantyType = extension.VendorWarrantyType;
    row.VendorID = itemCurySettings.PreferredVendorID;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSModelComponent> e)
  {
    if (e.Row == null)
      return;
    FSModelComponent row = e.Row;
    this.EnableDisable_FSModelComponent(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSModelComponent>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSModelComponent> e)
  {
    if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
      return;
    FSModelComponent row = e.Row;
    if (PXResultset<FSModelComponent>.op_Implicit(PXSelectBase<FSModelComponent, PXSelect<FSModelComponent, Where<FSModelComponent.componentID, Equal<Required<FSModelComponent.componentID>>, And<FSModelComponent.modelID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) row.ComponentID
    })) == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSModelComponent>>) e).Cache.RaiseExceptionHandling<FSModelComponent.componentID>((object) e.Row, (object) row.ComponentID, (Exception) new PXException("This ID is already in use."));
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSModelComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSModelComponent> e)
  {
  }
}
