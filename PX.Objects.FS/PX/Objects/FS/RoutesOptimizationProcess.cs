// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RoutesOptimizationProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FS.RouteOtimizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.FS;

public class RoutesOptimizationProcess : PXGraph<RoutesOptimizationProcess>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccounts;
  [PXHidden]
  public PXSelect<BAccountSelectorBase> BAccountSelectorBaseView;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> Vendors;
  public PXCancel<FSAppointmentFilter> Cancel;
  public PXFilter<FSAppointmentFilter> Filter;
  public PXSelectJoinOrderBy<FSAppointmentStaffMember, LeftJoin<CSCalendar, On<CSCalendar.calendarID, Equal<FSAppointmentStaffMember.calendarID>>>, OrderBy<Asc<FSAppointmentStaffMember.acctCD>>> StaffMemberFilter;
  [PXHidden]
  public PXSelect<FSAppointment> Appointments;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<FSAppointmentFSServiceOrder, FSAppointmentFilter, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointmentFSServiceOrder.customerID>>>, Where2<Where<FSAppointment.canceled, Equal<False>, And<FSAppointmentFSServiceOrder.hold, Equal<False>, And<FSAppointmentFSServiceOrder.branchID, Equal<Current<FSAppointmentFilter.branchID>>, And<FSAppointmentFSServiceOrder.branchLocationID, Equal<Current<FSAppointmentFilter.branchLocationID>>>>>>, And2<Where2<Where<Current<FSAppointmentFilter.type>, Equal<ListField_ROType.UnassignedApp>, And<FSAppointment.primaryDriver, IsNull>>, Or<Where<Current<FSAppointmentFilter.type>, Equal<ListField_ROType.AssignedApp>, And<FSAppointment.primaryDriver, IsNotNull, And<FSAppointment.primaryDriver, In<Required<FSAppointment.primaryDriver>>>>>>>, And2<Where<Current<FSAppointmentFilter.startDateWithTime>, IsNull, Or<Current<FSAppointmentFilter.startDateWithTime>, LessEqual<FSAppointmentFSServiceOrder.scheduledDateTimeBegin>>>, And<Where<Current<FSAppointmentFilter.endDateWithTime>, IsNull, Or<Current<FSAppointmentFilter.endDateWithTime>, GreaterEqual<FSAppointmentFSServiceOrder.scheduledDateTimeEnd>>>>>>>, OrderBy<Asc<FSAppointmentFSServiceOrder.scheduledDateTimeBegin>>> AppointmentList;
  public PXAction<FSAppointmentFilter> ShowOnMap;

  public virtual IEnumerable appointmentList()
  {
    PXView pxView = new PXView((PXGraph) this, true, ((PXSelectBase) this.AppointmentList).View.BqlSelect);
    int[] array = GraphHelper.RowCast<FSAppointmentStaffMember>((IEnumerable) ((PXSelectBase<FSAppointmentStaffMember>) this.StaffMemberFilter).Select(Array.Empty<object>())).Where<FSAppointmentStaffMember>((Func<FSAppointmentStaffMember, bool>) (_ => _.Selected.GetValueOrDefault())).Select<FSAppointmentStaffMember, int?>((Func<FSAppointmentStaffMember, int?>) (_ => _.BAccountID)).Cast<int>().ToArray<int>();
    int num = 0;
    int startRow = PXView.StartRow;
    object[] currents = PXView.Currents;
    object[] objArray = new object[1]{ (object) array };
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, objArray, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  [PXMergeAttributes]
  [PXUIField]
  protected void FSAppointmentFSServiceOrder_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Address Line 1", Visible = false)]
  protected void FSAppointmentFSServiceOrder_AddressLine1_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (Where<Current<FSAppointmentFilter.type>, Equal<ListField_ROType.AssignedApp>>))]
  [PXUIField(DisplayName = "Staff Member", FieldClass = "ROUTEOPTIMIZER")]
  protected virtual void FSAppointmentFSServiceOrder_PrimaryDriver_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void FSAppointmentStaffMember_Type_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXUIField]
  protected virtual void FSAppointmentStaffMember_AcctCD_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXUIField]
  protected virtual void FSAppointmentStaffMember_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable showOnMap(PXAdapter adapter)
  {
    if (((PXSelectBase<FSAppointmentFilter>) this.Filter).Current != null)
    {
      DateTime? startDate = ((PXSelectBase<FSAppointmentFilter>) this.Filter).Current.StartDate;
      if (startDate.HasValue)
      {
        KeyValuePair<string, string>[] parameters = new KeyValuePair<string, string>[1];
        startDate = ((PXSelectBase<FSAppointmentFilter>) this.Filter).Current.StartDate;
        parameters[0] = new KeyValuePair<string, string>("Date", startDate.Value.ToString());
        throw new PXRedirectToBoardRequiredException("pages/fs/GoogleMaps/StaffMap/FS301100.aspx", parameters);
      }
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointmentFilter.startDate> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentFilter row = (FSAppointmentFilter) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSAppointmentFilter.startDate>>) e).Cache, ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentFilter.startDate>>) e).NewValue);
    if (handlingDateTime.HasValue)
      row.StartDate = new DateTime?(handlingDateTime.Value.Date);
    else
      row.StartDate = new DateTime?();
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentStaffMember> e)
  {
    this.SetDelegate();
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentFilter> e)
  {
    if (e.Row == null)
      return;
    ((PXProcessing<FSAppointmentFSServiceOrder>) this.AppointmentList).SetProcessAllEnabled(e.Row.StartDate.HasValue);
    ((PXAction) this.ShowOnMap).SetEnabled(((PXSelectBase<FSAppointmentFilter>) this.Filter).Current.StartDate.HasValue);
    this.SetDelegate();
  }

  private void SetDelegate()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RoutesOptimizationProcess.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new RoutesOptimizationProcess.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.staffSelected = new PXResultset<FSAppointmentStaffMember, CSCalendar>();
    foreach (PXResult<FSAppointmentStaffMember, CSCalendar> pxResult in ((PXSelectBase<FSAppointmentStaffMember>) this.StaffMemberFilter).Select(Array.Empty<object>()))
    {
      if (PXResult<FSAppointmentStaffMember, CSCalendar>.op_Implicit(pxResult).Selected.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        ((PXResultset<FSAppointmentStaffMember>) cDisplayClass210.staffSelected).Add((PXResult<FSAppointmentStaffMember>) pxResult);
      }
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.filter = ((PXSelectBase<FSAppointmentFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<FSAppointmentFSServiceOrder>) this.AppointmentList).SetProcessDelegate(new PXProcessingBase<FSAppointmentFSServiceOrder>.ProcessListDelegate((object) cDisplayClass210, __methodptr(\u003CSetDelegate\u003Eb__0)));
  }

  public virtual int convertTimeToSec(DateTime? date)
  {
    DateTime dateTime = new DateHandler(date).StartOfDay();
    return (int) (date.Value - dateTime).TotalSeconds;
  }

  public virtual DateTime convertSecToTime(int sec, DateTime? date)
  {
    return new DateHandler(date).StartOfDay().AddSeconds((double) sec);
  }

  public virtual void OptimizeRoutes(
    List<FSAppointmentFSServiceOrder> list,
    FSAppointmentFilter filter,
    PXResultset<FSAppointmentStaffMember, CSCalendar> staffSelected)
  {
    RouteOptimizerClient routeOptimizerClient = new RouteOptimizerClient();
    SingleDayOptimizationInput requestBody = new SingleDayOptimizationInput();
    List<FSAppointment> fsAppointmentList = new List<FSAppointment>();
    FSSetup current = ((PXSelectBase<FSSetup>) this.SetupRecord).Current;
    requestBody.balanced = true;
    requestBody.vehicles = new List<PX.Objects.FS.RouteOtimizer.Vehicle>();
    requestBody.waypoints = new List<Waypoint>();
    string empty = string.Empty;
    if (((IQueryable<PXResult<FSAppointmentStaffMember>>) staffSelected).Count<PXResult<FSAppointmentStaffMember>>() == 0)
      throw new PXException(PXMessages.LocalizeNoPrefix("You must select at least one staff member."));
    FSAddress fsAddress1 = PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) list[0].BranchLocationID
    }));
    GLocation[] glocationArray = Geocoder.Geocode(SharedFunctions.GetAddressForGeolocation(fsAddress1.PostalCode, fsAddress1.AddressLine1, fsAddress1.AddressLine2, fsAddress1.City, fsAddress1.State, fsAddress1.CountryID), current.MapApiKey);
    if (glocationArray.Length == 0)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("{0} address cannot be interpreted as Latitude/Logitude.", new object[1]
      {
        (object) "Branch Location"
      }));
    CSCalendar csCalendar1 = PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelect<CSCalendar, Where<CSCalendar.calendarID, Equal<Required<CSCalendar.calendarID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.CalendarID
    }));
    bool valueOrDefault1 = filter.ConsiderSkills.GetValueOrDefault();
    int[] array = ((IQueryable<PXResult<FSAppointmentStaffMember>>) staffSelected).Select<PXResult<FSAppointmentStaffMember>, int>((Expression<Func<PXResult<FSAppointmentStaffMember>, int>>) (s => ((FSAppointmentStaffMember) s).BAccountID ?? 0)).ToArray<int>();
    Dictionary<int, List<string>> staffSkills = valueOrDefault1 ? this.GetStaffSkills(array) : (Dictionary<int, List<string>>) null;
    foreach (PXResult<FSAppointmentStaffMember, CSCalendar> pxResult in (PXResultset<FSAppointmentStaffMember>) staffSelected)
    {
      FSAppointmentStaffMember appointmentStaffMember = PXResult<FSAppointmentStaffMember, CSCalendar>.op_Implicit(pxResult);
      CSCalendar csCalendar2 = PXResult<FSAppointmentStaffMember, CSCalendar>.op_Implicit(pxResult);
      List<string> stringList1 = new List<string>();
      int? baccountId = appointmentStaffMember.BAccountID;
      stringList1.Add(baccountId.ToString());
      List<string> stringList2 = stringList1;
      if (valueOrDefault1)
      {
        Dictionary<int, List<string>> dictionary = staffSkills;
        baccountId = appointmentStaffMember.BAccountID;
        int valueOrDefault2 = baccountId.GetValueOrDefault();
        List<string> collection;
        ref List<string> local = ref collection;
        if (dictionary.TryGetValue(valueOrDefault2, out local))
          stringList2.AddRange((IEnumerable<string>) collection);
      }
      PX.Objects.FS.RouteOtimizer.Vehicle vehicle1 = new PX.Objects.FS.RouteOtimizer.Vehicle();
      baccountId = appointmentStaffMember.BAccountID;
      vehicle1.name = baccountId.ToString();
      vehicle1.origin = new RouteLocation()
      {
        latitude = glocationArray[0].LatLng.Latitude,
        longitude = glocationArray[0].LatLng.Longitude
      };
      vehicle1.destination = new RouteLocation()
      {
        latitude = glocationArray[0].LatLng.Latitude,
        longitude = glocationArray[0].LatLng.Longitude
      };
      vehicle1.tags = stringList2;
      PX.Objects.FS.RouteOtimizer.Vehicle vehicle2 = vehicle1;
      TimeWindow workingTimeWindow = this.GetWorkingTimeWindow(appointmentStaffMember.EmployeeSDEnabled.GetValueOrDefault() ? csCalendar2 : csCalendar1, filter.StartDate);
      Break breakWindow = this.GetBreakWindow(current);
      if (breakWindow != null)
        vehicle2.breaks = new List<Break>() { breakWindow };
      if (workingTimeWindow != null)
      {
        vehicle2.timeWindow = workingTimeWindow;
        requestBody.vehicles.Add(vehicle2);
      }
    }
    if (requestBody.vehicles.Count == 0)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        FSAppointment fsAppointment = (FSAppointment) list[index];
        fsAppointment.ROOptimizationStatus = "NA";
        ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointment);
        PXProcessing<FSAppointmentFSServiceOrder>.SetError(index, PXMessages.LocalizeNoPrefix("This appointment could not be reached/serviced because there is no driver available for the given time frame."));
      }
      if (!((PXSelectBase) this.Appointments).Cache.IsDirty)
        return;
      ((PXSelectBase) this.Appointments).Cache.Persist((PXDBOperation) 1);
    }
    else
    {
      if (filter.Type == "UA")
      {
        foreach (PXResult<FSAppointment, FSServiceOrder, FSAddress> assignedAppointment in this.GetAssignedAppointments(filter, array))
        {
          FSAppointment fsAppointmentRow = PXResult<FSAppointment, FSServiceOrder, FSAddress>.op_Implicit(assignedAppointment);
          FSAddress fsAddress2 = PXResult<FSAppointment, FSServiceOrder, FSAddress>.op_Implicit(assignedAppointment);
          string addressForGeolocation = SharedFunctions.GetAddressForGeolocation(fsAddress2.PostalCode, fsAddress2.AddressLine1, fsAddress2.AddressLine2, fsAddress2.City, fsAddress2.State, fsAddress2.CountryID);
          Waypoint waypointFromAppointment = this.GetWaypointFromAppointment(current, fsAppointmentRow, addressForGeolocation, (List<string>) null);
          if (waypointFromAppointment != null)
          {
            requestBody.waypoints.Add(waypointFromAppointment);
            fsAppointmentList.Add(fsAppointmentRow);
          }
          else
          {
            fsAppointmentRow.ROOptimizationStatus = "AE";
            ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointmentRow);
          }
        }
      }
      for (int index = list.Count - 1; index >= 0; --index)
      {
        bool flag1 = false;
        bool flag2 = false;
        try
        {
          string addressForGeolocation = SharedFunctions.GetAddressForGeolocation(list[index].PostalCode, list[index].AddressLine1, list[index].AddressLine2, list[index].City, list[index].State, list[index].CountryID);
          List<string> skills = valueOrDefault1 ? this.GetAppointmentSkills(list[index].AppointmentID.Value) : (List<string>) null;
          if (valueOrDefault1 && skills.Count > 0 && !staffSkills.Values.Any<List<string>>((Func<List<string>, bool>) (ss => skills.All<string>((Func<string, bool>) (s => ss.Contains(s))))))
          {
            flag2 = true;
          }
          else
          {
            Waypoint waypointFromAppointment = this.GetWaypointFromAppointment(current, (FSAppointment) list[index], addressForGeolocation, skills);
            if (waypointFromAppointment != null)
            {
              requestBody.waypoints.Add(waypointFromAppointment);
              fsAppointmentList.Add((FSAppointment) list[index]);
            }
            else
              flag1 = true;
          }
        }
        catch (Exception ex)
        {
          flag1 = true;
        }
        if (flag1 | flag2)
        {
          FSAppointment fsAppointment = (FSAppointment) list[index];
          if (flag1)
            fsAppointment.ROOptimizationStatus = "AE";
          else if (flag2)
            fsAppointment.ROOptimizationStatus = "NA";
          ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointment);
          if (flag1)
            PXProcessing<FSAppointmentFSServiceOrder>.SetError(index, PXMessages.LocalizeFormatNoPrefix("{0} address cannot be interpreted as Latitude/Logitude.", new object[1]
            {
              (object) "Appointment"
            }));
          else if (flag2)
            PXProcessing<FSAppointmentFSServiceOrder>.SetError(index, "This appointment could not be assigned to a staff member because no employee satisfies the skills required to perform the services in this appointment.");
        }
      }
      if (((PXSelectBase) this.Appointments).Cache.IsDirty)
        ((PXSelectBase) this.Appointments).Cache.Persist((PXDBOperation) 1);
      try
      {
        AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
        if (requestBody.waypoints.Any<Waypoint>())
        {
          SingleDayOptimizationOutput singleDayOptimization = routeOptimizerClient.getSingleDayOptimization(current.ROWWApiEndPoint, current.ROWWLicensekey, requestBody);
          int? nullable;
          for (int index1 = 0; index1 < singleDayOptimization.routes.Count; ++index1)
          {
            int result;
            int.TryParse(singleDayOptimization.routes[index1].vehicle.name, out result);
            bool flag = false;
            for (int index2 = 1; index2 < singleDayOptimization.routes[index1].steps.Count - 1; ++index2)
            {
              PX.Objects.FS.RouteOtimizer.RouteStep step = singleDayOptimization.routes[index1].steps[index2];
              int appointmentID;
              int.TryParse(step.waypoint.name, out appointmentID);
              FSAppointment fsAppointment1 = fsAppointmentList.Find((Predicate<FSAppointment>) (x =>
              {
                int? appointmentId = x.AppointmentID;
                int num = appointmentID;
                return appointmentId.GetValueOrDefault() == num & appointmentId.HasValue;
              }));
              DateTime time = this.convertSecToTime(step.serviceStartTimeSec, fsAppointment1.ScheduledDateTimeBegin);
              DateTime dateTime = time.AddSeconds((double) (step.departureTimeSec - step.arrivalTimeSec));
              nullable = fsAppointment1.PrimaryDriver;
              if (nullable.HasValue)
              {
                FSAppointment copy = (FSAppointment) ((PXSelectBase) this.Appointments).Cache.CreateCopy((object) fsAppointment1);
                FSAppointment fsAppointmentRow = copy;
                int? sortOrder = new int?(index2);
                nullable = new int?();
                int? assignedstaffID = nullable;
                DateTime? newBegin = new DateTime?(time);
                DateTime? newEnd = new DateTime?(dateTime);
                this.UpdateAppointmentHeader(fsAppointmentRow, sortOrder, assignedstaffID, newBegin, newEnd);
                FSAppointment fsAppointment2 = ((PXSelectBase<FSAppointment>) this.Appointments).Update(copy);
                fsAppointment2.ROOptimizationStatus = "OP";
                ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointment2);
                flag = true;
              }
              else
              {
                PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords1 = instance.AppointmentRecords;
                PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords2 = instance.AppointmentRecords;
                string refNbr = fsAppointment1.RefNbr;
                object[] objArray = new object[1]
                {
                  (object) fsAppointment1.SrvOrdType
                };
                FSAppointment fsAppointment3;
                FSAppointment fsAppointment4 = fsAppointment3 = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) appointmentRecords2).Search<FSAppointment.refNbr>((object) refNbr, objArray));
                ((PXSelectBase<FSAppointment>) appointmentRecords1).Current = fsAppointment3;
                FSAppointment fsAppointmentRow = fsAppointment4;
                fsAppointmentRow.ROOptimizationStatus = "OP";
                this.UpdateAppointmentHeader(fsAppointmentRow, new int?(index2), new int?(result), new DateTime?(time), new DateTime?(dateTime));
                FSAppointment fsAppointment5 = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointmentRow);
                fsAppointment5.ROOptimizationStatus = "OP";
                ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment5);
                FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
                {
                  AppointmentID = fsAppointment5.AppointmentID,
                  EmployeeID = new int?(result)
                };
                ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Insert(appointmentEmployee);
                ((PXAction) instance.Save).Press();
              }
            }
            if (flag)
              ((PXSelectBase) this.Appointments).Cache.Persist((PXDBOperation) 1);
          }
          foreach (OutputWaypoint outputWaypoint in singleDayOptimization.unreachableWaypoints.Concat<OutputWaypoint>((IEnumerable<OutputWaypoint>) singleDayOptimization.unreachedWaypoints).GroupBy<OutputWaypoint, string>((Func<OutputWaypoint, string>) (p => p.name)).Select<IGrouping<string, OutputWaypoint>, OutputWaypoint>((Func<IGrouping<string, OutputWaypoint>, OutputWaypoint>) (g => g.First<OutputWaypoint>())).ToList<OutputWaypoint>())
          {
            int appointmentID;
            int.TryParse(outputWaypoint.name, out appointmentID);
            FSAppointment fsAppointment = (FSAppointment) list.Find((Predicate<FSAppointmentFSServiceOrder>) (x =>
            {
              int? appointmentId = x.AppointmentID;
              int num = appointmentID;
              return appointmentId.GetValueOrDefault() == num & appointmentId.HasValue;
            }));
            if (fsAppointment != null)
            {
              for (int index = 0; index < list.Count; ++index)
              {
                nullable = fsAppointment.AppointmentID;
                int? appointmentId = list[index].AppointmentID;
                if (nullable.GetValueOrDefault() == appointmentId.GetValueOrDefault() & nullable.HasValue == appointmentId.HasValue)
                {
                  fsAppointment.ROOptimizationStatus = "NA";
                  ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointment);
                  PXProcessing<FSAppointmentFSServiceOrder>.SetError(index, PXMessages.LocalizeNoPrefix("This appointment cannot be optimized because the selected employee or employees have no available working hours in their work calendars."));
                }
              }
            }
          }
        }
        for (int index = 0; index < list.Count; ++index)
        {
          if (list[index].ROOptimizationStatus == "AE")
          {
            PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords3 = instance.AppointmentRecords;
            PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords4 = instance.AppointmentRecords;
            string refNbr = list[index].RefNbr;
            object[] objArray = new object[1]
            {
              (object) list[index].SrvOrdType
            };
            FSAppointment fsAppointment6;
            FSAppointment fsAppointment7 = fsAppointment6 = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) appointmentRecords4).Search<FSAppointment.refNbr>((object) refNbr, objArray));
            ((PXSelectBase<FSAppointment>) appointmentRecords3).Current = fsAppointment6;
            FSAppointment fsAppointment8 = fsAppointment7;
            fsAppointment8.ROOptimizationStatus = "NA";
            ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(fsAppointment8);
          }
        }
        ((PXAction) instance.Save).Press();
        ((PXSelectBase) this.Appointments).Cache.Persist((PXDBOperation) 1);
      }
      catch (PXException ex)
      {
        for (int index = 0; index < list.Count; ++index)
        {
          FSAppointment fsAppointment = (FSAppointment) list[index];
          fsAppointment.ROOptimizationStatus = "NA";
          ((PXSelectBase<FSAppointment>) this.Appointments).Update(fsAppointment);
          PXProcessing<FSAppointmentFSServiceOrder>.SetError(index, PXMessages.LocalizeFormatNoPrefix(((Exception) ex).Message, Array.Empty<object>()));
        }
        ((PXSelectBase) this.Appointments).Cache.Persist((PXDBOperation) 1);
      }
    }
  }

  /// <summary>Retrieves the already assigned appointmens by the filter</summary>
  private List<object> GetAssignedAppointments(FSAppointmentFilter filter, int[] staffSelected)
  {
    List<object> objectList = new List<object>();
    BqlCommand bqlCommand1 = (BqlCommand) new Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>>>>();
    if (filter.BranchID.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSServiceOrder.branchID, Equal<Required<FSServiceOrder.branchID>>>));
      objectList.Add((object) filter.BranchID);
    }
    if (filter.BranchLocationID.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>));
      objectList.Add((object) filter.BranchLocationID);
    }
    DateTime? nullable = filter.StartDate;
    if (nullable.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSAppointment.scheduledDateTimeBegin, GreaterEqual<Required<FSAppointment.scheduledDateTimeBegin>>>));
      objectList.Add((object) filter.StartDateWithTime);
    }
    nullable = filter.EndDateWithTime;
    if (nullable.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSAppointment.scheduledDateTimeEnd, LessEqual<Required<FSAppointment.scheduledDateTimeEnd>>>));
      objectList.Add((object) filter.EndDateWithTime);
    }
    BqlCommand bqlCommand2 = bqlCommand1.WhereAnd(typeof (Where<FSAppointment.primaryDriver, In<Required<FSAppointment.primaryDriver>>>));
    objectList.Add((object) staffSelected);
    return new PXView((PXGraph) this, true, bqlCommand2).SelectMulti(objectList.ToArray());
  }

  private List<string> GetAppointmentSkills(int appointmentId)
  {
    return PXSelectBase<FSSkill, PXViewOf<FSSkill>.BasedOn<SelectFromBase<FSSkill, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSServiceSkill>.On<BqlOperand<FSSkill.skillID, IBqlInt>.IsEqual<FSServiceSkill.skillID>>>, FbqlJoins.Inner<FSAppointmentDet>.On<BqlOperand<FSServiceSkill.serviceID, IBqlInt>.IsEqual<FSAppointmentDet.inventoryID>>>>.Where<BqlOperand<FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<P.AsInt>>.Aggregate<To<GroupBy<FSSkill.skillCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) appointmentId
    }).FirstTableItems.Select<FSSkill, string>((Func<FSSkill, string>) (s => s.SkillCD.Trim())).ToList<string>();
  }

  private Dictionary<int, List<string>> GetStaffSkills(int[] staffIds)
  {
    ParameterExpression instance;
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    // ISSUE: type reference
    return ((IQueryable<PXResult<FSSkill>>) PXSelectBase<FSSkill, PXViewOf<FSSkill>.BasedOn<SelectFromBase<FSSkill, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSEmployeeSkill>.On<BqlOperand<FSSkill.skillID, IBqlInt>.IsEqual<FSEmployeeSkill.skillID>>>>.Where<BqlOperand<FSEmployeeSkill.employeeID, IBqlInt>.IsIn<P.AsInt>>>.Config>.Select<PXResultset<FSSkill, FSEmployeeSkill>>((PXGraph) this, new object[1]
    {
      (object) staffIds
    })).Select(Expression.Lambda<Func<PXResult<FSSkill>, \u003C\u003Ef__AnonymousType15<int?, string>>>((Expression) Expression.New((ConstructorInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType15<int?, string>.\u002Ector), __typeref (\u003C\u003Ef__AnonymousType15<int?, string>)), (IEnumerable<Expression>) new Expression[2]
    {
      (Expression) Expression.Property((Expression) Expression.Call(s, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FSEmployeeSkill.get_EmployeeID))),
      (Expression) Expression.Property((Expression) Expression.Call((Expression) instance, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FSSkill.get_SkillCD)))
    }, (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType15<int?, string>.get_EmployeeID), __typeref (\u003C\u003Ef__AnonymousType15<int?, string>)), (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType15<int?, string>.get_SkillCD), __typeref (\u003C\u003Ef__AnonymousType15<int?, string>))), instance)).GroupBy(s => s.EmployeeID.Value).ToDictionary<IGrouping<int, \u003C\u003Ef__AnonymousType15<int?, string>>, int, List<string>>(g => g.Key, g => g.Select(s => s.SkillCD.Trim()).ToList<string>());
  }

  public virtual TimeWindow GetWorkingTimeWindow(CSCalendar csCalendarRow, DateTime? date)
  {
    if (!csCalendarRow.MonStartTime.HasValue && !csCalendarRow.MonStartTime.HasValue)
      return (TimeWindow) null;
    TimeWindow timeWindow1 = new TimeWindow();
    timeWindow1.startTimeSec = 0;
    timeWindow1.stopTimeSec = 0;
    DateTime dateTime = date.Value;
    switch (dateTime.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        if (csCalendarRow.SunStartTime.HasValue && csCalendarRow.SunStartTime.HasValue)
        {
          TimeWindow timeWindow2 = timeWindow1;
          dateTime = csCalendarRow.SunStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow2.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.SunEndTime.HasValue && csCalendarRow.SunEndTime.HasValue)
        {
          TimeWindow timeWindow3 = timeWindow1;
          dateTime = csCalendarRow.SunEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow3.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Monday:
        if (csCalendarRow.MonStartTime.HasValue && csCalendarRow.MonStartTime.HasValue)
        {
          TimeWindow timeWindow4 = timeWindow1;
          dateTime = csCalendarRow.MonStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow4.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.MonEndTime.HasValue && csCalendarRow.MonEndTime.HasValue)
        {
          TimeWindow timeWindow5 = timeWindow1;
          dateTime = csCalendarRow.MonEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow5.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Tuesday:
        if (csCalendarRow.TueStartTime.HasValue && csCalendarRow.TueStartTime.HasValue)
        {
          TimeWindow timeWindow6 = timeWindow1;
          dateTime = csCalendarRow.TueStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow6.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.TueEndTime.HasValue && csCalendarRow.TueEndTime.HasValue)
        {
          TimeWindow timeWindow7 = timeWindow1;
          dateTime = csCalendarRow.TueEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow7.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Wednesday:
        if (csCalendarRow.WedStartTime.HasValue && csCalendarRow.WedStartTime.HasValue)
        {
          TimeWindow timeWindow8 = timeWindow1;
          dateTime = csCalendarRow.WedStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow8.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.WedEndTime.HasValue && csCalendarRow.WedEndTime.HasValue)
        {
          TimeWindow timeWindow9 = timeWindow1;
          dateTime = csCalendarRow.MonEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow9.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Thursday:
        if (csCalendarRow.ThuStartTime.HasValue && csCalendarRow.ThuStartTime.HasValue)
        {
          TimeWindow timeWindow10 = timeWindow1;
          dateTime = csCalendarRow.ThuStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow10.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.ThuEndTime.HasValue && csCalendarRow.ThuEndTime.HasValue)
        {
          TimeWindow timeWindow11 = timeWindow1;
          dateTime = csCalendarRow.ThuEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow11.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Friday:
        if (csCalendarRow.FriStartTime.HasValue && csCalendarRow.FriStartTime.HasValue)
        {
          TimeWindow timeWindow12 = timeWindow1;
          dateTime = csCalendarRow.FriStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow12.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.FriEndTime.HasValue && csCalendarRow.FriEndTime.HasValue)
        {
          TimeWindow timeWindow13 = timeWindow1;
          dateTime = csCalendarRow.FriEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow13.stopTimeSec = totalSeconds;
          break;
        }
        break;
      case DayOfWeek.Saturday:
        if (csCalendarRow.SatStartTime.HasValue && csCalendarRow.SatStartTime.HasValue)
        {
          TimeWindow timeWindow14 = timeWindow1;
          dateTime = csCalendarRow.SatStartTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow14.startTimeSec = totalSeconds;
        }
        if (csCalendarRow.SatEndTime.HasValue && csCalendarRow.SatEndTime.HasValue)
        {
          TimeWindow timeWindow15 = timeWindow1;
          dateTime = csCalendarRow.SatEndTime.Value;
          int totalSeconds = (int) dateTime.TimeOfDay.TotalSeconds;
          timeWindow15.stopTimeSec = totalSeconds;
          break;
        }
        break;
    }
    return timeWindow1.startTimeSec != timeWindow1.stopTimeSec ? timeWindow1 : (TimeWindow) null;
  }

  public virtual Break GetBreakWindow(FSSetup fsSetupRow)
  {
    Break breakWindow = (Break) null;
    int? lunchBreakDuration = fsSetupRow.ROLunchBreakDuration;
    int num = 0;
    if (lunchBreakDuration.GetValueOrDefault() > num & lunchBreakDuration.HasValue)
    {
      breakWindow = new Break()
      {
        durationSec = fsSetupRow.ROLunchBreakDuration.Value * 60,
        startTimeSec = (int) fsSetupRow.ROLunchBreakStartTimeFrame.Value.TimeOfDay.TotalSeconds
      };
      breakWindow.stopTimeSec = (int) fsSetupRow.ROLunchBreakEndTimeFrame.Value.TimeOfDay.TotalSeconds - breakWindow.durationSec;
    }
    return breakWindow;
  }

  public virtual Waypoint GetWaypointFromAppointment(
    FSSetup fsSetupRow,
    FSAppointment fsAppointmentRow,
    string address,
    List<string> skills)
  {
    Waypoint waypointFromAppointment = new Waypoint();
    if (!fsAppointmentRow.MapLatitude.HasValue || !fsAppointmentRow.MapLongitude.HasValue)
    {
      GLocation[] glocationArray = Geocoder.Geocode(address, fsSetupRow.MapApiKey);
      if (glocationArray.Length != 0)
      {
        fsAppointmentRow.MapLatitude = new Decimal?((Decimal) glocationArray[0].LatLng.Latitude);
        fsAppointmentRow.MapLongitude = new Decimal?((Decimal) glocationArray[0].LatLng.Longitude);
      }
    }
    if (!fsAppointmentRow.MapLatitude.HasValue || !fsAppointmentRow.MapLongitude.HasValue)
      return (Waypoint) null;
    if (fsAppointmentRow.Confirmed.GetValueOrDefault() && fsAppointmentRow.ScheduledDateTimeBegin.HasValue)
    {
      TimeWindow timeWindow = new TimeWindow();
      waypointFromAppointment.timeWindows = new List<TimeWindow>();
      timeWindow.startTimeSec = (int) fsAppointmentRow.ScheduledDateTimeBegin.Value.TimeOfDay.TotalSeconds;
      timeWindow.stopTimeSec = (int) fsAppointmentRow.ScheduledDateTimeBegin.Value.TimeOfDay.TotalSeconds;
      waypointFromAppointment.timeWindows.Add(timeWindow);
    }
    Waypoint waypoint1 = waypointFromAppointment;
    int? nullable = fsAppointmentRow.AppointmentID;
    string str = nullable.ToString();
    waypoint1.name = str;
    Waypoint waypoint2 = waypointFromAppointment;
    DateTime? scheduledDateTimeEnd = fsAppointmentRow.ScheduledDateTimeEnd;
    DateTime? scheduledDateTimeBegin = fsAppointmentRow.ScheduledDateTimeBegin;
    int totalSeconds = (int) (scheduledDateTimeEnd.HasValue & scheduledDateTimeBegin.HasValue ? new TimeSpan?(scheduledDateTimeEnd.GetValueOrDefault() - scheduledDateTimeBegin.GetValueOrDefault()) : new TimeSpan?()).Value.TotalSeconds;
    waypoint2.serviceTimeSec = totalSeconds;
    waypointFromAppointment.location = new RouteLocation()
    {
      latitude = (double) fsAppointmentRow.MapLatitude.Value,
      longitude = (double) fsAppointmentRow.MapLongitude.Value
    };
    nullable = fsAppointmentRow.PrimaryDriver;
    if (nullable.HasValue)
    {
      Waypoint waypoint3 = waypointFromAppointment;
      List<string> stringList = new List<string>();
      nullable = fsAppointmentRow.PrimaryDriver;
      stringList.Add(nullable.ToString());
      waypoint3.tagsIncludeAnd = stringList;
      waypointFromAppointment.priority = 99;
    }
    else if (skills != null)
      waypointFromAppointment.tagsIncludeAnd = skills.ToList<string>();
    return waypointFromAppointment;
  }

  public virtual void UpdateAppointmentHeader(
    FSAppointment fsAppointmentRow,
    int? sortOrder,
    int? assignedstaffID,
    DateTime? newBegin,
    DateTime? newEnd)
  {
    fsAppointmentRow.ROOriginalSortOrder = fsAppointmentRow.ROSortOrder;
    fsAppointmentRow.ROSortOrder = sortOrder;
    if (assignedstaffID.HasValue)
      fsAppointmentRow.PrimaryDriver = assignedstaffID;
    if (newBegin.HasValue)
      fsAppointmentRow.ScheduledDateTimeBegin = newBegin;
    if (!newEnd.HasValue)
      return;
    fsAppointmentRow.ScheduledDateTimeEnd = newEnd;
  }
}
