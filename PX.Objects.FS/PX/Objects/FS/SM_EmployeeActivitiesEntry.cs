// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_EmployeeActivitiesEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_EmployeeActivitiesEntry : PXGraphExtension<EmployeeActivitiesEntry>
{
  public AppointmentEntry GraphAppointmentEntryCaller;
  public PXSelect<FSSetup> SetupRecord;
  public PXSelect<PMSetup> PMSetupRecord;
  public PXAction<EmployeeActivitiesEntry.PMTimeActivityFilter> OpenAppointment;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>()));
    if (fsSetup == null)
      return;
    bool flag = fsSetup.EnableEmpTimeCardIntegration.Value;
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.appointmentID>(((PXSelectBase) this.Base.Activity).Cache, (object) ((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.appointmentCustomerID>(((PXSelectBase) this.Base.Activity).Cache, (object) ((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.logLineNbr>(((PXSelectBase) this.Base.Activity).Cache, (object) ((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current, flag);
    PXUIFieldAttribute.SetVisible<FSxPMTimeActivity.serviceID>(((PXSelectBase) this.Base.Activity).Cache, (object) ((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current, flag);
  }

  [PXUIField(DisplayName = "Open Appointment")]
  [PXLookupButton]
  protected virtual void openAppointment()
  {
    if (((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current == null)
      return;
    FSxPMTimeActivity extension = ((PXSelectBase) this.Base.Activity).Cache.GetExtension<FSxPMTimeActivity>((object) ((PXSelectBase<EPActivityApprove>) this.Base.Activity).Current);
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    FSAppointment fsAppointment = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension.AppointmentID
    }));
    if (fsAppointment == null)
      return;
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointment.RefNbr, new object[1]
    {
      (object) fsAppointment.SrvOrdType
    }));
    if (((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPActivityApprove> e)
  {
    if (e.Row == null || !TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this.Base))
      return;
    EPActivityApprove row = e.Row;
    TimeCardHelper.PMTimeActivity_RowSelected_Handler(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPActivityApprove>>) e).Cache, (PMTimeActivity) row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<EPActivityApprove> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPActivityApprove> e)
  {
    if (e.Row == null)
      return;
    TimeCardHelper.PMTimeActivity_RowPersisting_Handler(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPActivityApprove>>) e).Cache, (PXGraph) this.Base, this.GraphAppointmentEntryCaller, (PMTimeActivity) e.Row, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<EPActivityApprove>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPActivityApprove> e)
  {
  }
}
