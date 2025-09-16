// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentClosingMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class AppointmentClosingMaint : AppointmentEntry
{
  public PXSelect<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<FSAppointment.routeDocumentID, Equal<Optional<FSAppointment.routeDocumentID>>>>> ClosingAppointmentRecords;
  public PXMenuAction<FSAppointment> appClosingMenuActions;
  public PXAction<FSAppointment> postToInventory;
  public PXAction<FSAppointment> OpenAppointment;

  public AppointmentClosingMaint()
  {
    ((PXAction) this.appClosingMenuActions).AddMenuAction((PXAction) this.completeAppointment);
    ((PXAction) this.appClosingMenuActions).AddMenuAction((PXAction) this.closeAppointment);
    PXGraph.FieldUpdatingEvents fieldUpdating = ((PXGraph) this).FieldUpdating;
    Type type = typeof (FSAppointment);
    string str = typeof (FSAppointment.actualDateTimeBegin).Name + "_Time";
    AppointmentClosingMaint appointmentClosingMaint = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) appointmentClosingMaint, __vmethodptr(appointmentClosingMaint, FSAppointment_ActualDateTimeBegin_Time_FieldUpdating));
    fieldUpdating.AddHandler(type, str, pxFieldUpdating);
    ((PXSelectBase) this.ClosingAppointmentRecords).Cache.AllowInsert = false;
  }

  public virtual Type PrimaryItemType => typeof (FSAppointment);

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  protected virtual void FSAppointment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField(DisplayName = "Actions")]
  public virtual IEnumerable AppClosingMenuActions(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable PostToInventory(PXAdapter adapter)
  {
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.ClosingAppointmentRecords).Current;
    if (current != null)
    {
      UpdateInventoryPost instance = PXGraph.CreateInstance<UpdateInventoryPost>();
      ((PXSelectBase<UpdateInventoryFilter>) instance.Filter).Current.RouteDocumentID = current.RouteDocumentID;
      ((PXSelectBase<UpdateInventoryFilter>) instance.Filter).Current.AppointmentID = current.AppointmentID;
      ((PXSelectBase<UpdateInventoryFilter>) instance.Filter).Current.CutOffDate = current.ExecutionDate;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void openAppointment()
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    if (((PXSelectBase<FSAppointment>) this.ClosingAppointmentRecords).Current != null && ((PXSelectBase<FSAppointment>) this.ClosingAppointmentRecords).Current.RefNbr != null)
    {
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) ((PXSelectBase<FSAppointment>) this.ClosingAppointmentRecords).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<FSAppointment>) this.ClosingAppointmentRecords).Current.SrvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  /// <summary>
  /// Verifies if the status of the appointment is CANCELED or CLOSED.
  /// </summary>
  public virtual bool AreServicesDBActionsAllowed(FSAppointment fsAppointmentRow)
  {
    return !fsAppointmentRow.Canceled.GetValueOrDefault() && !fsAppointmentRow.Closed.GetValueOrDefault();
  }

  /// <summary>
  /// Allow Or Forbid Insert, Update and Delete operations in the Services tab.
  /// </summary>
  public virtual void AllowOrForbidDetailsDBactions(FSAppointment fsAppointmentRow)
  {
    ((PXSelectBase) this.AppointmentDetails).AllowInsert = this.AreServicesDBActionsAllowed(fsAppointmentRow);
    ((PXSelectBase) this.AppointmentDetails).AllowDelete = this.AreServicesDBActionsAllowed(fsAppointmentRow);
    ((PXSelectBase) this.AppointmentDetails).AllowUpdate = this.AreServicesDBActionsAllowed(fsAppointmentRow);
  }

  protected override void _(Events.RowSelected<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSAppointment>>) e).Cache;
    base._(e);
    ((PXAction) this.OpenAppointment).SetEnabled(true);
    PXAction<FSAppointment> postToInventory = this.postToInventory;
    bool? nullable = row.Closed;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.EnableINPosting;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    ((PXAction) postToInventory).SetEnabled(num != 0);
    this.AllowOrForbidDetailsDBactions(row);
    PXUIFieldAttribute.SetEnabled<FSAppointment.refNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSAppointment.srvOrdType>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSAppointment.routeID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.customerID>(cache, (object) row, false);
  }
}
