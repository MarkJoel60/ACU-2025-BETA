// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.TimeCardHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public static class TimeCardHelper
{
  public static bool IsAccessedFromAppointment(string screenID)
  {
    return screenID == SharedFunctions.SetScreenIDToDotFormat("FS300200");
  }

  public static bool CanCurrentUserEnterTimeCards(PXGraph graph, string callerScreenID)
  {
    return callerScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300200") && callerScreenID != SharedFunctions.SetScreenIDToDotFormat("FS400100") || PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select(graph, Array.Empty<object>())) != null;
  }

  public static void PMTimeActivity_RowPersisting_Handler(
    PXCache cache,
    PXGraph graph,
    AppointmentEntry appGraph,
    PMTimeActivity pmTimeActivityRow,
    PXRowPersistingEventArgs e)
  {
    if (pmTimeActivityRow == null)
      return;
    FSxPMTimeActivity extension = PXCache<PMTimeActivity>.GetExtension<FSxPMTimeActivity>(pmTimeActivityRow);
    if (pmTimeActivityRow.OrigNoteID.HasValue || !extension.AppointmentID.HasValue || !extension.LogLineNbr.HasValue)
      return;
    if (e.Operation == 3 && graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS300200"))
      PXUpdate<Set<FSAppointmentLog.trackTime, False>, FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.lineNbr, Equal<Required<FSAppointmentLog.lineNbr>>>>>.Update(graph, new object[2]
      {
        (object) extension.AppointmentID,
        (object) extension.LogLineNbr
      });
    if (e.Operation != 2 && e.Operation != 1 || TimeCardHelper.IsAccessedFromAppointment(graph.Accessinfo.ScreenID))
      return;
    graph.Caches[typeof (FSAppointmentLog)].ClearQueryCache();
    FSAppointmentLog fsAppointmentLogRow = PXResultset<FSAppointmentLog>.op_Implicit(PXSelectBase<FSAppointmentLog, PXSelectReadonly<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.lineNbr, Equal<Required<FSAppointmentLog.lineNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) extension.AppointmentID,
      (object) extension.LogLineNbr
    }));
    if (fsAppointmentLogRow == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSAppointmentLog))
      });
    int? nullable1;
    int? nullable2;
    if (e.Operation == 1)
    {
      nullable1 = fsAppointmentLogRow.ProjectID;
      nullable2 = pmTimeActivityRow.ProjectID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        throw new PXException("The project cannot be modified because the time activity has been created from an appointment with a specific project.");
    }
    AppointmentEntry appointmentEntry = (AppointmentEntry) null;
    if (graph is EmployeeActivitiesEntry)
      appointmentEntry = graph.GetExtension<SM_EmployeeActivitiesEntry>()?.GraphAppointmentEntryCaller;
    AppointmentEntry graphAppointmentEntry = appointmentEntry ?? PXGraph.CreateInstance<AppointmentEntry>();
    bool flag = false;
    nullable2 = fsAppointmentLogRow.ProjectTaskID;
    nullable1 = pmTimeActivityRow.ProjectTaskID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      TimeCardHelper.LoadAppointmentGraph(graph, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
      fsAppointmentLogRow.ProjectTaskID = pmTimeActivityRow.ProjectTaskID;
      flag = true;
    }
    if (fsAppointmentLogRow.EarningType != pmTimeActivityRow.EarningTypeID)
    {
      TimeCardHelper.LoadAppointmentGraph(graph, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
      fsAppointmentLogRow.EarningType = pmTimeActivityRow.EarningTypeID;
      flag = true;
    }
    nullable1 = fsAppointmentLogRow.TimeDuration;
    nullable2 = pmTimeActivityRow.TimeSpent;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      TimeCardHelper.LoadAppointmentGraph(graph, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
      fsAppointmentLogRow.TimeDuration = pmTimeActivityRow.TimeSpent;
      flag = true;
    }
    bool? isBillable1 = fsAppointmentLogRow.IsBillable;
    bool? isBillable2 = pmTimeActivityRow.IsBillable;
    if (!(isBillable1.GetValueOrDefault() == isBillable2.GetValueOrDefault() & isBillable1.HasValue == isBillable2.HasValue))
    {
      TimeCardHelper.LoadAppointmentGraph(graph, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
      if (graphAppointmentEntry.ShouldUpdateAppointmentLogBillableFieldsFromTimeCard())
      {
        fsAppointmentLogRow.IsBillable = pmTimeActivityRow.IsBillable;
        flag = true;
      }
    }
    nullable2 = fsAppointmentLogRow.BillableTimeDuration;
    nullable1 = pmTimeActivityRow.TimeBillable;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
    {
      TimeCardHelper.LoadAppointmentGraph(graph, extension, fsAppointmentLogRow, ref graphAppointmentEntry);
      if (graphAppointmentEntry.ShouldUpdateAppointmentLogBillableFieldsFromTimeCard())
      {
        fsAppointmentLogRow.BillableTimeDuration = pmTimeActivityRow.TimeBillable;
        flag = true;
      }
    }
    if (!flag)
      return;
    graphAppointmentEntry.SkipTimeCardUpdate = true;
    ((PXSelectBase<FSAppointmentLog>) graphAppointmentEntry.LogRecords).Update(fsAppointmentLogRow);
    ((PXAction) graphAppointmentEntry.Save).Press();
  }

  public static void LoadAppointmentGraph(
    PXGraph graph,
    FSxPMTimeActivity fsxPMTimeActivityRow,
    FSAppointmentLog fsAppointmentLogRow,
    ref AppointmentEntry graphAppointmentEntry)
  {
    if (fsxPMTimeActivityRow != null && fsAppointmentLogRow != null)
    {
      int? appointmentId = fsxPMTimeActivityRow.AppointmentID;
      int? docId = fsAppointmentLogRow.DocID;
      if (appointmentId.GetValueOrDefault() == docId.GetValueOrDefault() & appointmentId.HasValue == docId.HasValue)
      {
        if (graphAppointmentEntry == null)
          graphAppointmentEntry = PXGraph.CreateInstance<AppointmentEntry>();
        else
          ((PXGraph) graphAppointmentEntry).Clear();
        int? nullable;
        if (((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current != null)
        {
          nullable = fsAppointmentLogRow.DocID;
          appointmentId = ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current.AppointmentID;
          if (nullable.GetValueOrDefault() == appointmentId.GetValueOrDefault() & nullable.HasValue == appointmentId.HasValue)
            return;
        }
        FSAppointment fsAppointment = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.Select(graph, new object[1]
        {
          (object) fsxPMTimeActivityRow.AppointmentID
        }));
        if (fsAppointment == null)
          throw new PXException("The {0} record was not found.", new object[1]
          {
            (object) DACHelper.GetDisplayName(typeof (FSAppointment))
          });
        ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
        {
          (object) fsAppointment.SrvOrdType
        }));
        appointmentId = ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current.AppointmentID;
        nullable = fsAppointment.AppointmentID;
        if (appointmentId.GetValueOrDefault() == nullable.GetValueOrDefault() & appointmentId.HasValue == nullable.HasValue)
          return;
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (FSAppointment))
        });
      }
    }
    throw new PXInvalidOperationException();
  }

  /// <summary>
  /// Checks if the Employee Time Cards Integration is enabled in the Service Management Setup.
  /// </summary>
  public static bool IsTheTimeCardIntegrationEnabled(PXGraph graph)
  {
    FSSetup serviceManagementSetup = ServiceManagementSetup.GetServiceManagementSetup(graph);
    return serviceManagementSetup != null && serviceManagementSetup.EnableEmpTimeCardIntegration.GetValueOrDefault();
  }

  public static void PMTimeActivity_RowSelected_Handler(
    PXCache cache,
    PMTimeActivity pmTimeActivityRow)
  {
    cache.GetExtension<FSxPMTimeActivity>((object) pmTimeActivityRow);
    PXUIFieldAttribute.SetEnabled<FSxPMTimeActivity.appointmentID>(cache, (object) pmTimeActivityRow, false);
    PXUIFieldAttribute.SetEnabled<FSxPMTimeActivity.appointmentCustomerID>(cache, (object) pmTimeActivityRow, false);
    PXUIFieldAttribute.SetEnabled<FSxPMTimeActivity.logLineNbr>(cache, (object) pmTimeActivityRow, false);
    PXUIFieldAttribute.SetEnabled<FSxPMTimeActivity.serviceID>(cache, (object) pmTimeActivityRow, false);
  }

  /// <summary>
  /// Checks if the all Appointment Service lines are approved by a Time Card, then sets Time Register to true and completes the appointment.
  /// </summary>
  public static void CheckTimeCardAppointmentApprovalsAndComplete(
    AppointmentEntry graphAppointmentEntry,
    PXCache cache,
    FSAppointment fsAppointmentRow)
  {
    bool flag1 = true;
    bool flag2 = false;
    if (!fsAppointmentRow.Completed.GetValueOrDefault())
      return;
    foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) graphAppointmentEntry.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (y => y.BAccountType == "EP" && y.TrackTime.GetValueOrDefault())))
    {
      flag2 = true;
      flag1 = flag1 && fsAppointmentLog.ApprovedTime.Value;
      if (!flag1)
        break;
    }
    if (!(flag1 & flag2))
      return;
    fsAppointmentRow.TimeRegistered = new bool?(true);
  }
}
