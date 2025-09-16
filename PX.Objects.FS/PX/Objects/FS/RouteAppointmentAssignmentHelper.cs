// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentAssignmentHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RouteAppointmentAssignmentHelper
{
  public virtual IEnumerable RouteRecordsDelegate(
    PXFilter<RouteAppointmentAssignmentFilter> filter,
    PXSelectBase<FSRouteDocument> cmd)
  {
    if (((PXSelectBase<RouteAppointmentAssignmentFilter>) filter).Current != null)
    {
      foreach (PXResult<FSRouteDocument, FSRoute> pxResult in cmd.Select(Array.Empty<object>()))
      {
        FSRouteDocument fsRouteDocument = PXResult<FSRouteDocument, FSRoute>.op_Implicit(pxResult);
        PXResult<FSRouteDocument, FSRoute>.op_Implicit(pxResult);
        DateTime? nullable = ((PXSelectBase<RouteAppointmentAssignmentFilter>) filter).Current.RouteDate;
        if (nullable.HasValue)
        {
          nullable = fsRouteDocument.TimeBegin;
          if (!nullable.HasValue)
            fsRouteDocument.TimeBegin = this.GetDateTimeEnd(fsRouteDocument.Date);
          nullable = fsRouteDocument.Date;
          DateTime date1 = nullable.Value.Date;
          nullable = ((PXSelectBase<RouteAppointmentAssignmentFilter>) filter).Current.RouteDate;
          DateTime date2 = nullable.Value.Date;
          if (date1 >= date2)
          {
            DateTime date3 = fsRouteDocument.Date.Value.Date;
            nullable = this.GetDateTimeEnd(new DateTime?(((PXSelectBase<RouteAppointmentAssignmentFilter>) filter).Current.RouteDate.Value.Date), 23, 59, 59);
            if ((nullable.HasValue ? (date3 <= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              yield return (object) pxResult;
          }
        }
        else
          yield return (object) pxResult;
      }
    }
  }

  /// <summary>
  /// Reassign the selected appointment <c>RefNbr</c> to the selected RouteDocumentID from the SmartPanel.
  /// </summary>
  /// <param name="fsRouteDocumentRow">New RouteDocumentID where the appointment is going to be assigned.</param>
  /// <param name="refNbr"><c>RefNbr</c> of the appointment to be assigned.</param>
  /// <param name="srvOrdType"><c>SrvOrdType</c> of the appointment to be assigned.</param>
  public static void ReassignAppointmentToRoute(
    FSRouteDocument fsRouteDocumentRow,
    string refNbr,
    string srvOrdType)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      FSAppointment fsAppointment = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) refNbr, new object[1]
      {
        (object) srvOrdType
      }));
      int? routeDocumentId = fsAppointment.RouteDocumentID;
      int? routePosition = fsAppointment.RoutePosition;
      fsAppointment.RoutePosition = new int?();
      if (fsRouteDocumentRow != null)
      {
        fsAppointment.RouteID = fsRouteDocumentRow.RouteID;
        fsAppointment.RouteDocumentID = fsRouteDocumentRow.RouteDocumentID;
        fsAppointment.ScheduledDateTimeBegin = fsRouteDocumentRow.TimeBegin.HasValue ? fsRouteDocumentRow.TimeBegin : fsRouteDocumentRow.Date;
      }
      else
      {
        fsAppointment.RouteID = new int?();
        fsAppointment.RouteDocumentID = new int?();
        fsAppointment.VehicleID = new int?();
        FSAppointmentEmployee appointmentEmployee = PXResultset<FSAppointmentEmployee>.op_Implicit(PXSelectBase<FSAppointmentEmployee, PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>, And<FSAppointmentEmployee.isDriver, Equal<True>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) fsAppointment.AppointmentID
        }));
        if (appointmentEmployee != null)
          ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Delete(appointmentEmployee);
      }
      fsAppointment.IsReassigned = new bool?(true);
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment);
      ((PXGraph) instance).SelectTimeStamp();
      ((PXAction) instance.Save).Press();
      RouteAppointmentAssignmentHelper.ReassignAppointmentPositionsInRoute(routeDocumentId, routePosition);
      transactionScope.Complete();
    }
  }

  /// <summary>
  /// Deletes the selected appointment <c>RefNbr</c> from Database.
  /// </summary>
  /// <param name="refNbr"><c>RefNbr</c> of the appointment to be deleted.</param>
  /// <param name="srvOrdType"><c>SrvOrdType</c> of the appointment to be deleted.</param>
  public static void DeleteAppointmentRoute(string refNbr, string srvOrdType)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) refNbr, new object[1]
      {
        (object) srvOrdType
      }));
      int? routeDocumentId = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.RouteDocumentID;
      int? routePosition = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.RoutePosition;
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Delete(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current);
      ((PXAction) instance.Save).Press();
      int? initialPosition = routePosition;
      RouteAppointmentAssignmentHelper.ReassignAppointmentPositionsInRoute(routeDocumentId, initialPosition);
      transactionScope.Complete();
    }
  }

  /// <summary>
  /// Reassign the positions of the appointments in a route beginning from a given position.
  /// </summary>
  private static void ReassignAppointmentPositionsInRoute(
    int? routeDocumentID,
    int? initialPosition)
  {
    if (!routeDocumentID.HasValue)
      return;
    int? nullable = initialPosition;
    int num = 0;
    if (nullable.GetValueOrDefault() <= num & nullable.HasValue)
      return;
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    PXResultset<FSAppointment> pxResultset = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>, And<FSAppointment.routePosition, GreaterEqual<Required<FSAppointment.routePosition>>>>, OrderBy<Asc<FSAppointment.routePosition>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) routeDocumentID,
      (object) initialPosition
    });
    if (pxResultset == null)
      return;
    foreach (PXResult<FSAppointment> pxResult in pxResultset)
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      fsAppointment.RoutePosition = initialPosition;
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment);
      nullable = initialPosition;
      initialPosition = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
    }
    instance.SkipServiceOrderUpdate = true;
    ((PXAction) instance.Save).Press();
  }

  public virtual DateTime? GetDateTimeEnd(
    DateTime? dateTimeBegin,
    int hour = 0,
    int minute = 0,
    int second = 0,
    int milisecond = 0)
  {
    return AppointmentEntry.GetDateTimeEndInt(dateTimeBegin, hour, minute, second, milisecond);
  }

  public class AppointmentEmployees
  {
    public FSAppointment fsAppointmentRow;
    public List<FSAppointmentEmployee> fsAppointmentEmployeeList;

    public AppointmentEmployees()
    {
    }

    public AppointmentEmployees(FSAppointment fsAppointmentRow)
    {
      this.fsAppointmentRow = fsAppointmentRow;
      this.fsAppointmentEmployeeList = new List<FSAppointmentEmployee>();
    }
  }

  public class RouteRecords_View : 
    PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRouteDocument.routeID, Equal<FSRoute.routeID>>>, Where<FSRouteDocument.routeDocumentID, NotEqual<Current<RouteAppointmentAssignmentFilter.routeDocumentID>>, And<Where<Current<RouteAppointmentAssignmentFilter.routeID>, IsNull, Or<FSRouteDocument.routeID, Equal<Current<RouteAppointmentAssignmentFilter.routeID>>>>>>>
  {
    public RouteRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public RouteRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
