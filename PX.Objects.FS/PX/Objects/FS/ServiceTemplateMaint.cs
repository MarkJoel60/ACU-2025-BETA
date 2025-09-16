// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceTemplateMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ServiceTemplateMaint : PXGraph<ServiceTemplateMaint, FSServiceTemplate>
{
  public PXSelect<FSServiceTemplate> ServiceTemplateRecords;
  public PXSelect<FSServiceTemplateDet, Where<FSServiceTemplateDet.serviceTemplateID, Equal<Current<FSServiceTemplate.serviceTemplateID>>>> ServiceTemplateDetails;
  public PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceTemplate.srvOrdType>>>> ServiceOrderTypeSelected;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSServiceTemplate.serviceTemplateCD>))]
  protected virtual void _(
    Events.CacheAttached<FSServiceTemplate.serviceTemplateCD> e)
  {
  }

  protected virtual void _(Events.RowSelecting<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowInserting<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSServiceTemplate> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSServiceTemplate> e)
  {
  }

  protected virtual void _(
    Events.FieldUpdated<FSServiceTemplateDet, FSServiceTemplateDet.lineType> e)
  {
    if (e.Row == null)
      return;
    this.LineTypeBlankFields(e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<FSServiceTemplateDet, FSServiceTemplateDet.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSServiceTemplateDet row = e.Row;
    if (row.LineType == null)
    {
      object obj;
      ((PXSelectBase) this.ServiceTemplateDetails).Cache.RaiseFieldDefaulting<FSServiceTemplateDet.lineType>((object) ((PXSelectBase<FSServiceTemplateDet>) this.ServiceTemplateDetails).Current, ref obj);
      row.LineType = (string) obj;
    }
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<FSServiceTemplateDet, FSServiceTemplateDet.inventoryID>>) e).Cache.SetDefaultExt<FSServiceTemplateDet.uOM>((object) e.Row);
  }

  protected virtual void _(Events.RowSelecting<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSServiceTemplateDet> e)
  {
    if (e.Row == null)
      return;
    FSServiceTemplateDet row = e.Row;
    this.LineTypeBlankFields(row);
    this.LineTypeEnableDisable(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSServiceTemplateDet>>) e).Cache, row);
  }

  protected virtual void _(Events.RowInserting<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSServiceTemplateDet> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSServiceTemplateDet> e)
  {
    if (e.Row == null)
      return;
    FSServiceTemplateDet row = e.Row;
    if (e.Operation != 2 && e.Operation != 1)
      return;
    this.LineTypeValidateLine(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<FSServiceTemplateDet>>) e).Cache, row);
  }

  protected virtual void _(Events.RowPersisted<FSServiceTemplateDet> e)
  {
  }

  /// <summary>
  /// This method enables or disables the fields on the <c>FSserviceTemplateDet</c> grid depending on the <c>FSServiceTemplateDet.LineType</c> field.
  /// </summary>
  public virtual void LineTypeEnableDisable(
    PXCache cache,
    FSServiceTemplateDet fsServiceTemplateDetRow)
  {
    switch (fsServiceTemplateDetRow.LineType)
    {
      case "CM_LN":
      case "IT_LN":
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.serviceTemplateID>(cache, (object) fsServiceTemplateDetRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.serviceTemplateID>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 2);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 2);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 2);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.uOM>(cache, (object) fsServiceTemplateDetRow, false);
        break;
      case "SLPRO":
      case "SERVI":
      case "NSTKI":
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.serviceTemplateID>(cache, (object) fsServiceTemplateDetRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.serviceTemplateID>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 2);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.uOM>(cache, (object) fsServiceTemplateDetRow, true);
        break;
      default:
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.inventoryID>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.qty>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSServiceTemplateDet.tranDesc>(cache, (object) fsServiceTemplateDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSServiceTemplateDet.uOM>(cache, (object) fsServiceTemplateDetRow, true);
        break;
    }
  }

  /// <summary>
  /// This method blanks the fields that aren't needed depending on the <c>FSServiceTemplateDet.LineType</c> field.
  /// </summary>
  public virtual void LineTypeBlankFields(FSServiceTemplateDet fsServiceTemplateDetRow)
  {
    string lineType = fsServiceTemplateDetRow.LineType;
    if (!(lineType == "CM_LN") && !(lineType == "IT_LN"))
      return;
    fsServiceTemplateDetRow.InventoryID = new int?();
    fsServiceTemplateDetRow.Qty = new Decimal?(0M);
  }

  /// <summary>
  /// This method validates if necessary fields are not null and launch the corresponding exception and error message.
  /// </summary>
  public virtual void LineTypeValidateLine(
    PXCache cache,
    FSServiceTemplateDet fsServiceTemplateDetRow,
    PXErrorLevel errorLevel = 4)
  {
    switch (fsServiceTemplateDetRow.LineType)
    {
      case "SLPRO":
        if (!fsServiceTemplateDetRow.InventoryID.HasValue)
          cache.RaiseExceptionHandling<FSServiceTemplateDet.inventoryID>((object) fsServiceTemplateDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        Decimal? qty1 = fsServiceTemplateDetRow.Qty;
        Decimal num1 = 0M;
        if (!(qty1.GetValueOrDefault() < num1 & qty1.HasValue))
          break;
        cache.RaiseExceptionHandling<FSServiceTemplateDet.qty>((object) fsServiceTemplateDetRow, (object) null, (Exception) new PXSetPropertyException("This value cannot be negative.", errorLevel));
        break;
      case "CM_LN":
      case "IT_LN":
        if (!string.IsNullOrEmpty(fsServiceTemplateDetRow.TranDesc))
          break;
        cache.RaiseExceptionHandling<FSServiceTemplateDet.tranDesc>((object) fsServiceTemplateDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        break;
      case "SERVI":
      case "NSTKI":
        if (!fsServiceTemplateDetRow.InventoryID.HasValue)
          cache.RaiseExceptionHandling<FSServiceTemplateDet.inventoryID>((object) fsServiceTemplateDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        Decimal? qty2 = fsServiceTemplateDetRow.Qty;
        Decimal num2 = 0M;
        if (!(qty2.GetValueOrDefault() < num2 & qty2.HasValue))
          break;
        cache.RaiseExceptionHandling<FSServiceTemplateDet.qty>((object) fsServiceTemplateDetRow, (object) null, (Exception) new PXSetPropertyException("This value cannot be negative.", errorLevel));
        break;
    }
  }
}
