// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_NonStockItemMaint
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

public class SM_NonStockItemMaint : PXGraphExtension<NonStockItemMaint>
{
  private bool baseUnitChanged;
  public SM_NonStockItemMaint.ServiceSkills_View ServiceSkills;
  public SM_NonStockItemMaint.ServiceLicenseTypes_View ServiceLicenseTypes;
  public SM_NonStockItemMaint.ServiceEquipmentTypes_View ServiceEquipmentTypes;
  public SM_NonStockItemMaint.ServiceInventoryItems_View ServiceInventoryItems;
  public SM_NonStockItemMaint.ServiceVehicleTypes_View ServiceVehicleTypes;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <summary>Enable or Disable fields.</summary>
  public virtual void EnableDisable(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    if (inventoryItemRow == null)
      return;
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelect<FSSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    bool flag1 = inventoryItemRow.ItemType == "S";
    PXUIFieldAttribute.SetEnabled<FSxService.billingRule>(cache, (object) inventoryItemRow, flag1);
    PXUIFieldAttribute.SetEnabled<FSxService.estimatedDuration>(cache, (object) inventoryItemRow, flag1);
    ((PXSelectBase) this.ServiceSkills).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.ServiceSkills).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.ServiceSkills).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ServiceLicenseTypes).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.ServiceLicenseTypes).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.ServiceLicenseTypes).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ServiceEquipmentTypes).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.ServiceEquipmentTypes).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.ServiceEquipmentTypes).Cache.AllowUpdate = flag1;
    if (fsSetup == null)
      return;
    bool flag2 = fsSetup.EnableEmpTimeCardIntegration.Value;
    PXUIFieldAttribute.SetEnabled<FSxService.dfltEarningType>(cache, (object) inventoryItemRow, flag2);
  }

  /// <summary>Assign the default Billing Rule set in the ItemClass.</summary>
  public virtual void SetDefaultBillingRule(PXCache cache, PX.Objects.IN.InventoryItem nonStockItemRow)
  {
    if (nonStockItemRow == null || !nonStockItemRow.ItemClassID.HasValue || !(nonStockItemRow.ItemType == "S"))
      return;
    FSxServiceClass extension = PXCache<INItemClass>.GetExtension<FSxServiceClass>(PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelect<INItemClass, Where<INItemClass.itemClassID, Equal<Required<PX.Objects.IN.InventoryItem.itemClassID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) nonStockItemRow.ItemClassID
    })));
    cache.GetExtension<FSxService>((object) nonStockItemRow).BillingRule = extension.DefaultBillingRule;
  }

  /// <summary>
  /// Set required and visible the PostClassID Field if the distribution module is installed in Acumatica.
  /// </summary>
  public virtual void SetPostClassIDField(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
    PXUIFieldAttribute.SetRequired<PX.Objects.IN.InventoryItem.postClassID>(cache, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.IN.InventoryItem.postClassID>(cache, (object) inventoryItemRow, flag);
  }

  /// <summary>
  /// Enable/Disable the PickUp/Delivery Grid for a Service if the ActionType is No Items Related or another option.
  /// </summary>
  public virtual void EnableDisablePickUpDelivery(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
    if (extension == null)
      return;
    bool flag = extension.ActionType != "N";
    ((PXSelectBase) this.ServiceInventoryItems).Cache.AllowInsert = flag;
    ((PXSelectBase) this.ServiceInventoryItems).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.ServiceInventoryItems).Cache.AllowDelete = flag;
  }

  /// <summary>
  /// Enable/Disable the ActionType field for a Service depending on the appointments related to that service.
  /// </summary>
  public virtual void EnableDisableActionType(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    bool flag = true;
    PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
    if (cache.GetStatus((object) inventoryItemRow) != 2)
      flag = PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.pickupDeliveryServiceID, Equal<Required<FSAppointmentDet.pickupDeliveryServiceID>>>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, new object[1]
      {
        (object) inventoryItemRow.InventoryID
      }).Count == 0;
    PXUIFieldAttribute.SetEnabled<FSxService.actionType>(cache, (object) inventoryItemRow, flag);
    if (flag)
      return;
    cache.RaiseExceptionHandling<FSxService.actionType>((object) inventoryItemRow, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("This element cannot be modified because there are {0} related to this {1}.", new object[2]
    {
      (object) "Appoinments",
      (object) "Service"
    }), (PXErrorLevel) 2));
  }

  public virtual void HideOrShowTabs(PXCache cache, PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    bool flag = inventoryItemRow.ItemType == "S";
    ((PXSelectBase) this.ServiceSkills).AllowSelect = flag;
    ((PXSelectBase) this.ServiceLicenseTypes).AllowSelect = flag;
    ((PXSelectBase) this.ServiceInventoryItems).AllowSelect = flag;
    ((PXSelectBase) this.ServiceEquipmentTypes).AllowSelect = flag;
    PXUIFieldAttribute.SetVisible<FSxService.actionType>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxService.estimatedDuration>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxService.billingRule>(cache, (object) inventoryItemRow, flag);
    PXUIFieldAttribute.SetVisible<FSxServiceClass.mem_RouteService>(((PXSelectBase) this.Base.ItemClass).Cache, (object) ((PXSelectBase<INItemClass>) this.Base.ItemClass).Current, flag);
  }

  /// <summary>
  /// Verifies if there is at least one Service Contract detail related to the specified Service.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="inventoryID">Inventory ID related to the service.</param>
  /// <returns>True if at least there is one detail related to, false otherwise.</returns>
  public virtual bool IsServiceRelatedToAnyContract(PXCache cache, int? inventoryID)
  {
    return PXSelectBase<FSSalesPrice, PXSelectJoin<FSSalesPrice, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSalesPrice.inventoryID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
    {
      (object) inventoryID
    }).Count > 0;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, FSxService.estimatedDuration> e)
  {
    PX.Objects.IN.InventoryItem row = e.Row;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemClassID> e)
  {
    this.SetDefaultBillingRule(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemClassID>>) e).Cache, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemType> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.itemType>>) e).Cache.SetDefaultExt<FSxService.billingRule>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.stkItem>>) e).Cache.SetDefaultExt<FSxService.billingRule>((object) e.Row);
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
    this.EnableDisable(cache, row);
    this.SetPostClassIDField(cache, row);
    this.EnableDisablePickUpDelivery(cache, row);
    this.EnableDisableActionType(cache, row);
    this.HideOrShowTabs(cache, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.IN.InventoryItem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.IN.InventoryItem> e)
  {
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
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem>>) e).Cache;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(cache.Graph, row.InventoryID);
    if (inventoryItemRow != null && e.Operation == 1)
    {
      string baseUnit = row.BaseUnit;
      if ((baseUnit != null ? (!baseUnit.Equals(inventoryItemRow.BaseUnit) ? 1 : 0) : 0) != 0 && this.IsServiceRelatedToAnyContract(cache, row.InventoryID))
        this.baseUnitChanged = true;
    }
    if (!(row.ItemType == "S") || !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || row.ItemClassID.HasValue)
      return;
    cache.RaiseExceptionHandling<PX.Objects.IN.InventoryItem.itemClassID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PX.Objects.IN.InventoryItem.itemClassID>("This element cannot be empty.", (PXErrorLevel) 4));
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceSkill> e)
  {
    if (e.Row == null)
      return;
    FSServiceSkill row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSServiceSkill.skillID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceSkill>>) e).Cache, (object) row, string.IsNullOrEmpty(row.SkillID.ToString()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceLicenseType> e)
  {
    if (e.Row == null)
      return;
    FSServiceLicenseType row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSServiceLicenseType.licenseTypeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceLicenseType>>) e).Cache, (object) row, string.IsNullOrEmpty(row.LicenseTypeID.ToString()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceLicenseType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceEquipmentType> e)
  {
    if (e.Row == null)
      return;
    FSServiceEquipmentType row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSServiceEquipmentType.equipmentTypeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceEquipmentType>>) e).Cache, (object) row, string.IsNullOrEmpty(row.EquipmentTypeID.ToString()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceEquipmentType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceEquipmentType> e)
  {
  }

  public class ServiceSkills_View : 
    PXSelectJoin<FSServiceSkill, LeftJoin<FSSkill, On<FSSkill.skillID, Equal<FSServiceSkill.skillID>>>, Where<FSServiceSkill.serviceID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>
  {
    public ServiceSkills_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceSkills_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceLicenseTypes_View : 
    PXSelectJoin<FSServiceLicenseType, LeftJoin<FSLicenseType, On<FSLicenseType.licenseTypeID, Equal<FSServiceLicenseType.licenseTypeID>>>, Where<FSServiceLicenseType.serviceID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>
  {
    public ServiceLicenseTypes_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceLicenseTypes_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceEquipmentTypes_View : 
    PXSelectJoin<FSServiceEquipmentType, InnerJoin<FSEquipmentType, On<FSEquipmentType.equipmentTypeID, Equal<FSServiceEquipmentType.equipmentTypeID>>>, Where<FSServiceEquipmentType.serviceID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>
  {
    public ServiceEquipmentTypes_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceEquipmentTypes_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceInventoryItems_View : 
    PXSelectJoin<FSServiceInventoryItem, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSServiceInventoryItem.inventoryID>>>, Where<FSServiceInventoryItem.serviceID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>
  {
    public ServiceInventoryItems_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceInventoryItems_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceVehicleTypes_View : 
    PXSelectJoin<FSServiceVehicleType, InnerJoin<FSVehicleType, On<FSVehicleType.vehicleTypeID, Equal<FSServiceVehicleType.vehicleTypeID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSServiceVehicleType.serviceID>>>>, Where<FSServiceVehicleType.serviceID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>>>
  {
    public ServiceVehicleTypes_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceVehicleTypes_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
