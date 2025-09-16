// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ExternalControls
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.FS;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.UI;

#nullable disable
namespace PX.Objects.FS;

public class ExternalControls : PXGraph<ExternalControls>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccounts;
  public PXSelect<FSSetup> SetupRecords;
  public PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>> UserPreferencesRecords;
  public PXSelect<PX.Objects.CS.CSCalendar, Where<PX.Objects.CS.CSCalendar.calendarID, Equal<Required<PX.Objects.CS.CSCalendar.calendarID>>>> CSCalendar;
  public PXSelect<PX.Objects.CS.CSCalendarExceptions, Where<PX.Objects.CS.CSCalendarExceptions.calendarID, Equal<Required<PX.Objects.CS.CSCalendarExceptions.calendarID>>, And<PX.Objects.CS.CSCalendarExceptions.date, Equal<Required<PX.Objects.CS.CSCalendarExceptions.date>>>>> CSCalendarExceptions;
  public PXSelect<PX.Objects.CS.CSCalendarExceptions, Where<PX.Objects.CS.CSCalendarExceptions.calendarID, Equal<Required<PX.Objects.CS.CSCalendarExceptions.calendarID>>, And<PX.Objects.CS.CSCalendarExceptions.date, GreaterEqual<Required<PX.Objects.CS.CSCalendarExceptions.date>>, And<PX.Objects.CS.CSCalendarExceptions.date, LessEqual<Required<PX.Objects.CS.CSCalendarExceptions.date>>>>>> FromToCSCalendarExceptions;
  public PXSelect<FSAppointmentStatusColor> AppointmentStatusColorRecords;
  public PXSelect<AppointmentBoxComponentField, Where<FSCalendarComponentField.isActive, Equal<True>>, OrderBy<Asc<AppointmentBoxComponentField.sortOrder>>> ActiveAppBoxFields;
  public PXSelect<ServiceOrderComponentField, Where<FSCalendarComponentField.isActive, Equal<True>>, OrderBy<Asc<ServiceOrderComponentField.sortOrder>>> ActiveSOGridFields;
  public PXSelect<UnassignedAppComponentField, Where<FSCalendarComponentField.isActive, Equal<True>>, OrderBy<Asc<UnassignedAppComponentField.sortOrder>>> ActiveUAGridFields;
  public PXSelect<FSRoom, Where<FSRoom.branchLocationID, Equal<Required<FSRoom.branchLocationID>>, And<FSRoom.roomID, Equal<Required<FSRoom.roomID>>>>, OrderBy<Asc<FSRoom.descr>>> RoomByBranchLocation;
  public PXSelect<FSRoom, Where<FSRoom.branchLocationID, Equal<Required<FSRoom.branchLocationID>>>, OrderBy<Asc<FSRoom.descr>>> RoomRecordsByBranchLocation;
  public PXSelect<FSSkill> SkillRecords;
  public PXSelect<FSGeoZone> GeoZoneRecords;
  public PXSelect<FSProblem> ProblemRecords;
  public PXSelect<FSLicenseType> LicenseTypeRecords;
  public PXSelect<FSEquipment, Where<FSEquipment.resourceEquipment, Equal<True>>> Resources;
  public PXSelect<INItemClass, Where<INItemClass.itemType, Equal<INItemTypes.serviceItem>>> ServiceClasses;
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.itemStatus, Equal<InventoryItemStatus.active>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>, And<Match<Current<AccessInfo.userName>>>>>> Services;
  public PXSelect<FSBranchLocation, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>> BranchLocationRecord;
  public PXSelect<FSBranchLocation, Where<FSBranchLocation.branchID, Equal<Required<FSBranchLocation.branchID>>, Or<Required<FSBranchLocation.branchID>, IsNull>>> BranchLocationRecordsByBranch;
  public PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>>> SrvOrdTypeRecord;
  public PXSelect<FSSrvOrdType, Where<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.quote>, And<FSSrvOrdType.active, Equal<True>>>> SrvOrdTypeRecords;
  public PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>> AppointmentRecord;
  public PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FSAppointmentEmployee.employeeID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>>>>>, Where<FSServiceOrder.canceled, Equal<False>, And2<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, OrderBy<Asc<FSAppointmentEmployee.employeeID>>> AppointmentRecords;
  public PXSelectJoin<EPEmployee, InnerJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.employeeID, Equal<EPEmployee.bAccountID>>, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, InnerJoin<BAccountStaffMember, On<BAccountStaffMember.bAccountID, Equal<EPEmployee.bAccountID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<FSAddress.countryID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<FSAddress.countryID>, And<PX.Objects.CS.State.stateID, Equal<FSAddress.state>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>>>>>>>>, Where<FSServiceOrder.canceled, Equal<False>, And<FSAppointmentEmployee.employeeID, Equal<Required<FSAppointmentEmployee.employeeID>>, And2<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>, OrderBy<Asc<FSAppointment.scheduledDateTimeBegin>>> AppointmentsByEmployee;
  public PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>, And<FSAppointmentEmployee.employeeID, Equal<Required<FSAppointmentEmployee.employeeID>>>>> AppointmentEmployeeByEmployee;
  public PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>>> AppointmentEmployees;
  public PXSelectJoin<EPEmployee, InnerJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.employeeID, Equal<EPEmployee.bAccountID>>>, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>>> AppointmentEPEmployees;
  public PXSelectJoin<BAccountStaffMember, InnerJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.employeeID, Equal<BAccountStaffMember.bAccountID>>>, Where<FSAppointmentEmployee.appointmentID, Equal<Required<FSAppointmentEmployee.appointmentID>>>> AppointmentBAccountStaffMember;
  public PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>, Where<FSSODet.lineType, Equal<ListField_LineType_UnifyTabs.Service>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>> AppointmentDets;
  public PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>>, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<Where<FSAppointment.notStarted, Equal<True>, Or<FSAppointment.inProcess, Equal<True>>>>>> ActiveAppointmentDetsBySO;
  public PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>>, Where<FSAppointment.appointmentID, Equal<Required<FSAppointmentDet.appointmentID>>, And<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.InProcess>>>>>> ActiveAppointmentDets;
  public PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<FSSODet, On<FSSODet.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, InnerJoin<FSAppointmentDet, On<FSAppointmentDet.sODetID, Equal<FSSODet.sODetID>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>>>>, Where<FSSODet.lineType, Equal<ListField_LineType_UnifyTabs.Service>, And<FSAppointmentDet.appointmentID, Equal<Required<FSAppointmentDet.appointmentID>>>>> AppointmentServices;
  public PXSelectJoin<FSEquipment, InnerJoin<FSAppointmentResource, On<FSAppointmentResource.SMequipmentID, Equal<FSEquipment.SMequipmentID>>>, Where<FSAppointmentResource.appointmentID, Equal<Required<FSAppointmentResource.appointmentID>>>> AppointmentResources;
  public PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSServiceOrder.assignedEmpID>>, LeftJoin<FSBranchLocation, On<FSBranchLocation.branchLocationID, Equal<FSServiceOrder.branchLocationID>>, LeftJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>>>>>, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>> ServiceOrderRecord;
  public PXSelectJoin<FSServiceOrder, InnerJoin<FSAppointment, On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>>, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>> ServiceOrderByAppointment;
  public PXSelect<FSContact, Where<FSContact.contactID, Equal<Required<FSContact.contactID>>>> ServiceOrderContact;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Optional<EPEmployee.parentBAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Optional<EPEmployee.defContactID>>>>> EmployeeContact;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Optional<PX.Objects.AP.Vendor.defContactID>>>> VendorContact;
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>> Customer;
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>> InventoryItem;
  public PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<FSSODet, On<FSSODet.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<FSSODet.lineType, Equal<ListField_LineType_UnifyTabs.Service>, And<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>> InventoryItemBySODet;
  public PXSelectJoin<EPEmployee, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<EPEmployee.defContactID>>>, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>> EmployeeSelected;
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>> EmployeeBAccount;
  public PXSelectJoinGroupBy<EPEmployee, InnerJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.employeeID, Equal<EPEmployee.bAccountID>>, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<EPEmployee.bAccountID>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>>>>, Where2<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Aggregate<GroupBy<EPEmployee.bAccountID>>, OrderBy<Asc<EPEmployee.acctName>>> EmployeeAppointmentsByDate;
  public PXSelectJoin<FSSODet, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSODet.inventoryID>>>, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>, And<FSSODet.lineType, Equal<ListField_LineType_UnifyTabs.Service>>>> SODetServices;
  public PXSelectJoin<FSSODet, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSODet.inventoryID>>>, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>, And<FSSODet.lineType, Equal<ListField_LineType_UnifyTabs.Service>, And<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.ScheduleNeeded>>>>> SODetPendingLines;
  public PXSelect<FSTimeSlot, Where<FSTimeSlot.timeSlotID, Equal<Required<FSTimeSlot.timeSlotID>>>> TimeSlotRecord;
  public PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>> RouteRecord;
  public PXSelectJoin<FSRouteDocument, LeftJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>, LeftJoin<FSEquipment, On<FSEquipment.SMequipmentID, Equal<FSRouteDocument.vehicleID>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FSRouteDocument.driverID>>>>>> RouteRecords;
  public PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<FSRouteDocument.driverID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<EPEmployee.bAccountID>>, LeftJoin<FSEquipment, On<FSEquipment.SMequipmentID, Equal<FSRouteDocument.vehicleID>>>>>>, Where<FSRouteDocument.date, GreaterEqual<Required<FSRouteDocument.date>>, And<FSRouteDocument.date, Less<Required<FSRouteDocument.date>>>>, OrderBy<Asc<FSRouteDocument.timeBegin>>> RoutesAndDriversByDate;
  public PXSelectJoin<FSGPSTrackingRequest, InnerJoin<Users, On<Users.username, Equal<FSGPSTrackingRequest.userName>>, InnerJoin<EPEmployee, On<EPEmployee.userID, Equal<Users.pKID>>>>, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>> GPSTrackingRequestByEmployee;
  public PXSelect<FSGPSTrackingHistory, Where<FSGPSTrackingHistory.trackingID, Equal<Required<FSGPSTrackingHistory.trackingID>>>, OrderBy<Desc<FSGPSTrackingHistory.executionDate>>> FSGPSTrackingHistoryByTrackingID;
  public PXSelect<FSGPSTrackingHistory, Where<FSGPSTrackingHistory.trackingID, Equal<Required<FSGPSTrackingHistory.trackingID>>, And<FSGPSTrackingHistory.executionDate, GreaterEqual<Required<FSGPSTrackingHistory.executionDate>>, And<FSGPSTrackingHistory.executionDate, LessEqual<Required<FSGPSTrackingHistory.executionDate>>>>>, OrderBy<Asc<FSGPSTrackingHistory.executionDate>>> FSGPSTrackingHistoryByTrackingIDAndDate;
  public PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<FSAddress.countryID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<FSAddress.countryID>, And<PX.Objects.CS.State.stateID, Equal<FSAddress.state>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>>>>>, Where<FSServiceOrder.canceled, Equal<False>, And<FSAppointment.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>, OrderBy<Asc<FSAppointment.routePosition>>> AppointmentRecordsByRoute;

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  public virtual Dictionary<string, object> GetCustomComponentFields(
    PXGraph graph,
    List<FSCalendarComponentField> list)
  {
    Dictionary<string, object> customComponentFields = new Dictionary<string, object>();
    foreach (FSCalendarComponentField calendarComponentField in list)
    {
      PXCache cach = graph.Caches[calendarComponentField.ObjectName];
      if (cach == null)
      {
        System.Type type = System.Type.GetType(calendarComponentField.ObjectName);
        cach = type != (System.Type) null ? graph.Caches[type] : (PXCache) null;
      }
      if (cach != null)
      {
        PXFieldState stateExt = (PXFieldState) cach.GetStateExt((object) null, calendarComponentField.FieldName);
        if (stateExt != null && !string.IsNullOrEmpty(this.ResolveFieldDisplayName(calendarComponentField.FieldName, stateExt.DisplayName, cach)))
        {
          string key = $"{calendarComponentField.ObjectName}.{calendarComponentField.FieldName.ToCapitalized()}";
          if (calendarComponentField.ComponentType == "AP")
          {
            StringWriter writer = new StringWriter();
            if (!string.IsNullOrEmpty(calendarComponentField.ImageUrl))
            {
              string[] strArray = calendarComponentField.ImageUrl.Split('@');
              ControlHelper.RenderSpriteImage(new HtmlTextWriter((TextWriter) writer), "", strArray[0], strArray[1]);
            }
            string str = this.ResolveFieldDisplayName(calendarComponentField.FieldName, stateExt.DisplayName, cach);
            customComponentFields[key] = (object) new
            {
              DisplayName = str,
              Icon = writer.ToString()
            };
          }
          else
            customComponentFields[key] = (object) this.ResolveFieldDisplayName(calendarComponentField.FieldName, stateExt.DisplayName, cach);
        }
      }
    }
    return customComponentFields;
  }

  public virtual string ResolveFieldDisplayName(
    string fieldName,
    string displayName,
    PXCache cache)
  {
    int num = displayName.IndexOf("-");
    return cache.GetBqlField(fieldName) == (System.Type) null && cache.Fields.Contains(fieldName) && num > 0 && num < displayName.Length ? displayName.Substring(num + 1) : displayName;
  }

  public virtual List<PXFilterRow> GetSearchTextFilters(
    List<FSCalendarComponentField> activeComponentFields,
    string filterTextValue,
    List<Tuple<string, string>> filterListField)
  {
    List<PXFilterRow> searchTextFilters = new List<PXFilterRow>();
    ExternalControls externalControls = this;
    foreach (FSCalendarComponentField activeComponentField in activeComponentFields)
    {
      System.Type type = System.Type.GetType(activeComponentField.ObjectName);
      string str = $"{type.Name}__{activeComponentField.FieldName}";
      PXFieldState stateExt = (PXFieldState) ((PXGraph) externalControls).Caches[type].GetStateExt((object) null, activeComponentField.FieldName);
      if (stateExt != null && this.GetStateType((object) stateExt) == typeof (string))
        filterListField.Add(new Tuple<string, string>(str, filterTextValue));
    }
    if (!string.IsNullOrEmpty(filterTextValue))
    {
      string[] strArray = filterTextValue.Split(' ');
      PXFilterRow pxFilterRow = (PXFilterRow) null;
      for (int index = 0; index < filterListField.Count; ++index)
      {
        bool flag = true;
        string str1 = filterListField[index].Item1;
        foreach (string str2 in strArray)
        {
          if (!string.IsNullOrEmpty(str2))
          {
            pxFilterRow = new PXFilterRow(str1, (PXCondition) 6, (object) str2);
            if (flag)
            {
              pxFilterRow.OpenBrackets = 1;
              flag = false;
            }
            searchTextFilters.Add(pxFilterRow);
          }
        }
        if (pxFilterRow != null)
        {
          pxFilterRow.CloseBrackets = 1;
          pxFilterRow.OrOperator = true;
        }
      }
      if (searchTextFilters.Count > 0)
      {
        searchTextFilters[0].OpenBrackets = 2;
        searchTextFilters[searchTextFilters.Count - 1].CloseBrackets = 2;
        searchTextFilters[searchTextFilters.Count - 1].OrOperator = false;
      }
    }
    return searchTextFilters;
  }

  public virtual System.Type GetStateType(object state)
  {
    switch (state)
    {
      case PXDateState _:
        return ((PXFieldState) state).DataType;
      case PXIntState _:
        return ((PXFieldState) state).DataType;
      case PXLongState _:
        return ((PXFieldState) state).DataType;
      case PXFloatState _:
        return ((PXFieldState) state).DataType;
      case PXDecimalState _:
        return ((PXFieldState) state).DataType;
      case PXStringState _:
        return ((PXFieldState) state).DataType;
      case PXFieldState _:
        return ((PXFieldState) state).DataType;
      default:
        return (System.Type) null;
    }
  }

  public virtual string GetStateValue(object state)
  {
    switch (state)
    {
      case PXDateState _:
        PXDateState pxDateState = (PXDateState) state;
        return !string.IsNullOrEmpty(pxDateState.DisplayMask) ? PXFieldState.GetStringValue((PXFieldState) pxDateState, (string) null, pxDateState.DisplayMask) : ((PXDateState) state).ToString();
      case PXIntState _:
        return ((PXIntState) state).ToString();
      case PXLongState _:
        return ((PXLongState) state).ToString();
      case PXFloatState _:
        return ((PXFloatState) state).ToString();
      case PXDecimalState _:
        return ((PXDecimalState) state).ToString();
      case PXStringState _:
        return ((PXStringState) state).ToString();
      case PXFieldState _:
        return ((PXFieldState) state).ToString();
      default:
        return string.Empty;
    }
  }

  public virtual void AddCustomFields(
    PXGraph graph,
    PXResult row,
    List<FSCalendarComponentField> customFields,
    ExternalControls.FSEntityInfo entity)
  {
    foreach (FSCalendarComponentField customField in customFields)
    {
      if (!string.IsNullOrEmpty(customField.ObjectName) && !string.IsNullOrEmpty(customField.FieldName))
      {
        PXFieldState state = this.GetState(graph, row, customField);
        if (state != null)
        {
          string uniqueName = this.GetUniqueName(customField, state);
          entity.fields.Add(new ExternalControls.FSFieldInfo(uniqueName, (object) this.GetStateValue((object) state)));
        }
      }
    }
  }

  public virtual string GetUniqueName(FSCalendarComponentField field, PXFieldState state)
  {
    return $"{field.ObjectName}.{state.Name}";
  }

  public virtual PXFieldState GetState(
    PXGraph graph,
    PXResult result,
    FSCalendarComponentField field)
  {
    System.Type objectType = ((IEnumerable<System.Type>) result.Tables).Where<System.Type>((Func<System.Type, bool>) (x => field.ObjectName == x.FullName)).FirstOrDefault<System.Type>();
    return this.GetState(graph, result[objectType], objectType, field.FieldName);
  }

  public virtual PXFieldState GetState(
    PXGraph graph,
    object row,
    System.Type objectType,
    string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName))
      return (PXFieldState) null;
    return objectType != (System.Type) null ? (PXFieldState) graph.Caches[objectType].GetStateExt(row, fieldName) : (PXFieldState) null;
  }

  public virtual List<ExternalControls.FSEntityInfo> ServiceOrderRecords(
    int? branchID,
    int? branchLocationID,
    ExternalControls.DispatchBoardFilters[] filters,
    DateTime? scheduledDateStart,
    DateTime? scheduledDateEnd,
    bool? isRoomCalendar,
    int limit,
    int start,
    out int totals)
  {
    ExternalControls graph = this;
    List<object> objectList1 = new List<object>();
    List<ExternalControls.FSEntityInfo> fsEntityInfoList = new List<ExternalControls.FSEntityInfo>();
    List<int?> source = new List<int?>();
    PXSelectBase<FSServiceOrder> pxSelectBase = (PXSelectBase<FSServiceOrder>) new PXSelectJoin<FSServiceOrder, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>>>>, Where<FSServiceOrder.quote, Equal<False>, And<FSServiceOrder.hold, Equal<False>, And<FSServiceOrder.closed, Equal<False>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceOrder.completed, Equal<False>, And<FSServiceOrder.appointmentsNeeded, Equal<True>, And<PX.Objects.CR.BAccount.type, NotEqual<BAccountType.prospectType>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>>>>((PXGraph) graph);
    if (branchID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<FSServiceOrder.branchID, Equal<Required<FSServiceOrder.branchID>>>>();
      objectList1.Add((object) branchID);
    }
    if (scheduledDateStart.HasValue)
    {
      pxSelectBase.WhereAnd<Where2<Where<FSServiceOrder.sLAETA, IsNull, And<FSServiceOrder.orderDate, GreaterEqual<Required<FSServiceOrder.orderDate>>>>, Or<Where<FSServiceOrder.sLAETA, IsNotNull, And<FSServiceOrder.sLAETA, GreaterEqual<Required<FSServiceOrder.sLAETA>>>>>>>();
      objectList1.Add((object) scheduledDateStart);
      objectList1.Add((object) scheduledDateStart);
    }
    if (scheduledDateEnd.HasValue)
    {
      pxSelectBase.WhereAnd<Where2<Where<FSServiceOrder.sLAETA, IsNotNull, And<FSServiceOrder.sLAETA, LessEqual<Required<FSServiceOrder.sLAETA>>>>, Or<FSServiceOrder.orderDate, LessEqual<Required<FSServiceOrder.orderDate>>>>>();
      objectList1.Add((object) scheduledDateEnd);
      objectList1.Add((object) scheduledDateEnd);
    }
    if (branchLocationID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>>();
      objectList1.Add((object) branchLocationID);
    }
    if (isRoomCalendar.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<FSServiceOrder.roomID, IsNull>>();
    List<FSCalendarComponentField> list1 = ((IQueryable<PXResult<ServiceOrderComponentField>>) ((PXSelectBase<ServiceOrderComponentField>) graph.ActiveSOGridFields).Select(Array.Empty<object>())).Select<PXResult<ServiceOrderComponentField>, FSCalendarComponentField>((Expression<Func<PXResult<ServiceOrderComponentField>, FSCalendarComponentField>>) (x => (ServiceOrderComponentField) x)).ToList<FSCalendarComponentField>();
    int num1 = 0;
    int num2 = 0;
    List<PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>> list2;
    if (filters != null)
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      ExternalControls.DispatchBoardFilters dispatchBoardFilters1 = ((IEnumerable<ExternalControls.DispatchBoardFilters>) filters).Where<ExternalControls.DispatchBoardFilters>((Func<ExternalControls.DispatchBoardFilters, bool>) (i => i.property == "LikeText")).FirstOrDefault<ExternalControls.DispatchBoardFilters>();
      if (dispatchBoardFilters1 != null && !string.IsNullOrEmpty(dispatchBoardFilters1.value[0]))
        pxFilterRowList = this.GetSearchTextFilters(list1, dispatchBoardFilters1.value[0], new List<Tuple<string, string>>()
        {
          new Tuple<string, string>(typeof (FSServiceOrder.refNbr).Name, dispatchBoardFilters1.value[0])
        });
      ExternalControls.DispatchBoardFilters dispatchBoardFilters2 = ((IEnumerable<ExternalControls.DispatchBoardFilters>) filters).Where<ExternalControls.DispatchBoardFilters>((Func<ExternalControls.DispatchBoardFilters, bool>) (i => i.property == "AssignedEmpID")).FirstOrDefault<ExternalControls.DispatchBoardFilters>();
      if (dispatchBoardFilters2 != null && !string.IsNullOrEmpty(dispatchBoardFilters2.value[0]))
      {
        pxSelectBase.WhereAnd<Where<FSServiceOrder.assignedEmpID, Equal<Required<FSServiceOrder.assignedEmpID>>>>();
        objectList1.Add((object) dispatchBoardFilters2.value[0]);
      }
      PXView view = ((PXSelectBase) pxSelectBase).View;
      list2 = ((PXSelectBase) pxSelectBase).View.Select((object[]) null, objectList1.ToArray(), PXView.Searches, (string[]) null, PXView.Descendings, pxFilterRowList.ToArray(), ref num1, PXView.MaximumRows, ref num2).Cast<PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>>().ToList<PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>>();
      ExternalControls.DispatchBoardFilters dispatchBoardFilters3 = ((IEnumerable<ExternalControls.DispatchBoardFilters>) filters).Where<ExternalControls.DispatchBoardFilters>((Func<ExternalControls.DispatchBoardFilters, bool>) (i => EnumerableExtensions.IsIn<string>(i.property, "Skill", "LicenseType", "Problem", "ServiceClass"))).FirstOrDefault<ExternalControls.DispatchBoardFilters>();
      totals = num2;
      if (dispatchBoardFilters3 != null)
      {
        for (int index = 0; index < list2.Count; ++index)
        {
          FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>.op_Implicit(list2[index]);
          List<PXResult<FSSODet>> list3 = ((IEnumerable<PXResult<FSSODet>>) ((PXSelectBase<FSSODet>) graph.SODetPendingLines).Select(new object[1]
          {
            (object) fsServiceOrder.SOID
          })).ToList<PXResult<FSSODet>>();
          List<int> intList1 = new List<int>();
          List<int> second1 = new List<int>();
          List<int> second2 = new List<int>();
          List<int> second3 = new List<int>();
          List<int> second4 = new List<int>();
          bool flag = false;
          if (list3.Count == 0)
          {
            list2.RemoveAt(index--);
          }
          else
          {
            List<int?> list4 = list3.Select<PXResult<FSSODet>, int?>((Func<PXResult<FSSODet>, int?>) (y => ((PXResult) y).GetItem<FSSODet>().InventoryID)).ToList<int?>();
            list3.Select<PXResult<FSSODet>, int?>((Func<PXResult<FSSODet>, int?>) (y => ((PXResult) y).GetItem<FSSODet>().SODetID)).ToList<int?>();
            List<SharedClasses.ItemList> itemWithList1 = SharedFunctions.GetItemWithList<FSServiceSkill, FSServiceSkill.serviceID, FSServiceSkill.skillID>((PXGraph) graph, list4);
            List<SharedClasses.ItemList> itemWithList2 = SharedFunctions.GetItemWithList<FSServiceLicenseType, FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID>((PXGraph) graph, list4);
            List<SharedClasses.ItemList> itemWithList3 = SharedFunctions.GetItemWithList<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.inventoryID, PX.Objects.IN.InventoryItem.itemClassID>((PXGraph) graph, list4);
            foreach (PXResult<FSSODet> pxResult in list3)
            {
              FSSODet fsSODetRow = PXResult<FSSODet>.op_Implicit(pxResult);
              SharedClasses.ItemList itemList1 = itemWithList1.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
              {
                int? itemId = y.itemID;
                int? inventoryId = fsSODetRow.InventoryID;
                return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
              }));
              SharedClasses.ItemList itemList2 = itemWithList2.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
              {
                int? itemId = y.itemID;
                int? inventoryId = fsSODetRow.InventoryID;
                return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
              }));
              SharedClasses.ItemList itemList3 = itemWithList3.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
              {
                int? itemId = y.itemID;
                int? inventoryId = fsSODetRow.InventoryID;
                return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
              }));
              if (itemList1 != null)
              {
                foreach (int num3 in itemList1.list)
                  second1.Add(num3);
              }
              if (itemList2 != null)
              {
                foreach (int num4 in itemList2.list)
                  second2.Add(num4);
              }
              if (itemList3 != null)
              {
                foreach (int num5 in itemList3.list)
                  second4.Add(num5);
              }
              int? problemId = fsServiceOrder.ProblemID;
              if (problemId.HasValue)
              {
                List<int> intList2 = second3;
                problemId = fsServiceOrder.ProblemID;
                int num6 = problemId.Value;
                intList2.Add(num6);
              }
            }
            foreach (ExternalControls.DispatchBoardFilters filter in filters)
            {
              switch (filter.property)
              {
                case "Skill":
                  if (((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>().Except<int>((IEnumerable<int>) second1).Any<int>())
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "LicenseType":
                  if (((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>().Except<int>((IEnumerable<int>) second2).Any<int>())
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "Problem":
                  if (((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>().Except<int>((IEnumerable<int>) second3).Any<int>())
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "ServiceClass":
                  if (((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>().Except<int>((IEnumerable<int>) second4).Any<int>())
                  {
                    flag = true;
                    break;
                  }
                  break;
              }
            }
            if (flag)
              list2.RemoveAt(index--);
          }
        }
        totals = list2.Count;
      }
    }
    else
    {
      list2 = ((PXSelectBase) pxSelectBase).View.Select((object[]) null, objectList1.ToArray(), PXView.Searches, (string[]) null, PXView.Descendings, (PXFilterRow[]) null, ref num1, PXView.MaximumRows, ref num2).Cast<PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>>().ToList<PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>>();
      totals = num2;
    }
    int num7 = limit == 0 || limit >= totals ? totals : limit;
    int num8 = start != 0 ? start : 0;
    ExternalControls.LoadCustGraph instance = PXGraph.CreateInstance<ExternalControls.LoadCustGraph>();
    for (int index = 0; index < num7 && start + index < list2.Count; ++index)
    {
      PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer> row = list2[num8 + index];
      FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder, FSContact, FSAddress, PX.Objects.CR.BAccount, PX.Objects.AR.Customer>.op_Implicit(row);
      ExternalControls.FSEntityInfo entity = new ExternalControls.FSEntityInfo();
      fsServiceOrder.SLARemaining = new int?();
      DateTime? slaeta = fsServiceOrder.SLAETA;
      if (slaeta.HasValue)
      {
        DateTime? nullable1 = PXContext.GetBusinessDate() ?? new DateTime?(PXTimeZoneInfo.Now);
        slaeta = fsServiceOrder.SLAETA;
        DateTime? nullable2 = nullable1;
        TimeSpan timeSpan = (slaeta.HasValue & nullable2.HasValue ? new TimeSpan?(slaeta.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new TimeSpan?()).Value;
        fsServiceOrder.SLARemaining = new int?(timeSpan.TotalMinutes > 0.0 ? Convert.ToInt32(timeSpan.TotalMinutes) : -1);
      }
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.sOID), (object) fsServiceOrder.SOID));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.srvOrdType), (object) fsServiceOrder.SrvOrdType));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.refNbr), (object) fsServiceOrder.RefNbr));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.estimatedDurationTotal), (object) fsServiceOrder.EstimatedDurationTotal));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.treeID), (object) fsServiceOrder.SOID));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.text), (object) fsServiceOrder.RefNbr));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.leaf), (object) false));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.sLARemaining), (object) fsServiceOrder.SLARemaining));
      List<object> objectList2 = new List<object>();
      foreach (PXResult<FSSODet, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<FSSODet>) graph.SODetPendingLines).Select(new object[1]
      {
        (object) fsServiceOrder.SOID
      }))
      {
        FSSODet fssoDet = PXResult<FSSODet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryItem inventoryItem = PXResult<FSSODet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        objectList2.Add((object) new
        {
          SODetID = fssoDet.SODetID,
          InventoryID = fssoDet.InventoryID,
          Text = (fssoDet.TranDesc ?? inventoryItem.Descr ?? inventoryItem.InventoryCD),
          Leaf = true,
          EstimatedDuration = fssoDet.EstimatedDuration
        });
        source.Add(fssoDet.InventoryID);
      }
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.rows), (object) objectList2));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSServiceOrder.serviceClassIDs), (object) source.Distinct<int?>().ToArray<int?>()));
      this.AddCustomFields((PXGraph) instance, (PXResult) row, list1, entity);
      fsEntityInfoList.Add(entity);
    }
    return fsEntityInfoList;
  }

  /// <summary>
  /// Gets the appointment records related for the given dates.
  /// </summary>
  /// <param name="timeBegin">Schedule Start Date.</param>
  /// <param name="timeEnd">Schedule End Date.</param>
  /// <param name="branchID">Current Branch ID.</param>
  /// <param name="branchLocationID">Branch Location ID.</param>
  /// <param name="employeeIDList">Employee id list.</param>
  /// <returns>Appointment list.</returns>
  public virtual List<ExternalControls.FSEntityInfo> GetAppointmentRecords(
    DateTime timeBegin,
    DateTime timeEnd,
    int? branchID,
    int? branchLocationID,
    int[] employeeIDList,
    bool isStaff)
  {
    List<object> objectList1 = new List<object>();
    List<ExternalControls.FSEntityInfo> appointmentRecords = new List<ExternalControls.FSEntityInfo>();
    BqlCommand bqlCommand1 = (BqlCommand) new Select5<FSAppointmentEmployee, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSWFStage, On<FSWFStage.wFStageID, Equal<FSAppointment.wFStageID>>>>>>>>>, Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, Aggregate<GroupBy<FSAppointmentEmployee.appointmentID, GroupBy<FSAppointmentEmployee.employeeID, GroupBy<FSAppointment.validatedByDispatcher, GroupBy<FSAppointment.confirmed>>>>>, OrderBy<Asc<FSAppointmentScheduleBoard.appointmentID>>>();
    objectList1.Add((object) timeBegin);
    objectList1.Add((object) timeEnd);
    if (branchID.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSAppointment.branchID, Equal<Required<FSAppointment.branchID>>>));
      objectList1.Add((object) branchID);
    }
    if (branchLocationID.HasValue)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(typeof (Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>));
      objectList1.Add((object) branchLocationID);
    }
    if (employeeIDList != null && employeeIDList.Length != 0)
    {
      bqlCommand1 = bqlCommand1.WhereAnd(InHelper<FSAppointmentEmployee.employeeID>.Create(employeeIDList.Length));
      foreach (int employeeId in employeeIDList)
        objectList1.Add((object) employeeId);
    }
    PXResultset<FSAppointmentStatusColor> pxResultset = PXSelectBase<FSAppointmentStatusColor, PXSelect<FSAppointmentStatusColor, Where<FSAppointmentStatusColor.isVisible, Equal<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<string> stringList = new List<string>();
    foreach (PXResult<FSAppointmentStatusColor> pxResult in pxResultset)
    {
      FSAppointmentStatusColor appointmentStatusColor = PXResult<FSAppointmentStatusColor>.op_Implicit(pxResult);
      stringList.Add(appointmentStatusColor.StatusID);
    }
    if (stringList.Count <= 0)
      return appointmentRecords;
    BqlCommand bqlCommand2 = bqlCommand1.WhereAnd(InHelper<FSAppointment.status>.Create(stringList.Count));
    foreach (string str in stringList)
      objectList1.Add((object) str);
    List<FSCalendarComponentField> list = ((IEnumerable<PXResult<AppointmentBoxComponentField>>) ((PXSelectBase<AppointmentBoxComponentField>) this.ActiveAppBoxFields).Select(Array.Empty<object>())).AsEnumerable<PXResult<AppointmentBoxComponentField>>().Cast<PXResult<AppointmentBoxComponentField>>().Select<PXResult<AppointmentBoxComponentField>, FSCalendarComponentField>((Func<PXResult<AppointmentBoxComponentField>, FSCalendarComponentField>) (x => (FSCalendarComponentField) PXResult<AppointmentBoxComponentField>.op_Implicit(x))).ToList<FSCalendarComponentField>();
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand2).SelectMulti(objectList1.ToArray());
    ExternalControls.LoadCustGraph instance = PXGraph.CreateInstance<ExternalControls.LoadCustGraph>();
    foreach (PXResult<FSAppointmentEmployee, FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage> row in objectList2)
    {
      ExternalControls.FSEntityInfo entity = new ExternalControls.FSEntityInfo();
      FSAppointment apptRow = PXResult<FSAppointmentEmployee, FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage>.op_Implicit(row);
      PXResult<FSAppointmentEmployee, FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage>.op_Implicit(row);
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee, FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage>.op_Implicit(row);
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.employeeCount), (object) apptRow.StaffCntr));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.canDeleteAppointment), (object) this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 4)));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.oldEmployeeID), (object) appointmentEmployee.EmployeeID));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentEmployee.employeeID), (object) appointmentEmployee.EmployeeID));
      string[] strArray = new string[5];
      DateTime? nullable1 = apptRow.ScheduledDateTimeBegin;
      int num = nullable1.Value.Month;
      strArray[0] = num.ToString("00");
      nullable1 = apptRow.ScheduledDateTimeBegin;
      num = nullable1.Value.Day;
      strArray[1] = num.ToString("00");
      nullable1 = apptRow.ScheduledDateTimeBegin;
      num = nullable1.Value.Year;
      strArray[2] = num.ToString("0000");
      strArray[3] = "-";
      int? nullable2 = appointmentEmployee.EmployeeID;
      strArray[4] = nullable2.ToString();
      string str1 = string.Concat(strArray);
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customDateID), (object) str1));
      string str2;
      if (!isStaff)
      {
        str2 = (string) null;
      }
      else
      {
        nullable2 = apptRow.AppointmentID;
        str2 = nullable2.ToString() + "-0";
      }
      string str3 = str2;
      string str4;
      if (!isStaff)
      {
        str4 = (string) null;
      }
      else
      {
        nullable1 = apptRow.ScheduledDateTimeBegin;
        num = nullable1.Value.Month;
        string str5 = num.ToString("00");
        nullable1 = apptRow.ScheduledDateTimeBegin;
        num = nullable1.Value.Day;
        string str6 = num.ToString("00");
        nullable1 = apptRow.ScheduledDateTimeBegin;
        num = nullable1.Value.Year;
        string str7 = num.ToString("0000");
        str4 = str5 + str6 + str7;
      }
      string str8 = str4;
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.appointmentCustomID), (object) str3));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customID), (object) str8));
      this.AddCommonAppointmentField(entity.fields, apptRow);
      List<ExternalControls.FSFieldInfo> fields1 = entity.fields;
      System.Type fieldType1 = typeof (FSAppointmentScheduleBoard.customDateTimeStart);
      nullable1 = apptRow.ScheduledDateTimeBegin;
      string str9 = nullable1.ToString();
      ExternalControls.FSFieldInfo fsFieldInfo1 = new ExternalControls.FSFieldInfo(fieldType1, (object) str9);
      fields1.Add(fsFieldInfo1);
      List<ExternalControls.FSFieldInfo> fields2 = entity.fields;
      System.Type fieldType2 = typeof (FSAppointmentScheduleBoard.customDateTimeEnd);
      nullable1 = apptRow.ScheduledDateTimeEnd;
      string str10 = nullable1.ToString();
      ExternalControls.FSFieldInfo fsFieldInfo2 = new ExternalControls.FSFieldInfo(fieldType2, (object) str10);
      fields2.Add(fsFieldInfo2);
      this.AddCustomFields((PXGraph) instance, (PXResult) row, list, entity);
      appointmentRecords.Add(entity);
    }
    return appointmentRecords;
  }

  /// <summary>
  /// Gets the appointment records related for the given dates.
  /// </summary>
  /// <param name="timeBegin">Schedule Start Date.</param>
  /// <param name="timeEnd">Schedule End Date.</param>
  /// <param name="branchID">Current Branch ID.</param>
  /// <param name="branchLocationID">Branch Location ID.</param>
  /// <param name="roomIDList">Room id list.</param>
  /// <returns>Appointment list.</returns>
  public virtual List<ExternalControls.FSEntityInfo> GetAppointmentRecordsByRooms(
    DateTime timeBegin,
    DateTime timeEnd,
    int? branchID,
    int? branchLocationID,
    int[] roomIDList)
  {
    List<object> objectList1 = new List<object>();
    List<ExternalControls.FSEntityInfo> appointmentRecordsByRooms = new List<ExternalControls.FSEntityInfo>();
    BqlCommand bqlCommand = (BqlCommand) new Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSRoom, On<FSRoom.roomID, Equal<FSServiceOrder.roomID>>, LeftJoin<FSWFStage, On<FSWFStage.wFStageID, Equal<FSAppointment.wFStageID>>>>>>>>>, Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>();
    objectList1.Add((object) timeBegin);
    objectList1.Add((object) timeEnd);
    if (branchID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSAppointment.branchID, Equal<Required<FSAppointment.branchID>>>));
      objectList1.Add((object) branchID);
    }
    if (branchLocationID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>));
      objectList1.Add((object) branchLocationID);
    }
    if (roomIDList != null && roomIDList.Length != 0)
    {
      bqlCommand = bqlCommand.WhereAnd(InHelper<FSServiceOrder.roomID>.Create(roomIDList.Length));
      foreach (int roomId in roomIDList)
        objectList1.Add((object) roomId);
    }
    PXResultset<FSAppointmentStatusColor> pxResultset = PXSelectBase<FSAppointmentStatusColor, PXSelect<FSAppointmentStatusColor, Where<FSAppointmentStatusColor.isVisible, Equal<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<string> stringList = new List<string>();
    foreach (PXResult<FSAppointmentStatusColor> pxResult in pxResultset)
    {
      FSAppointmentStatusColor appointmentStatusColor = PXResult<FSAppointmentStatusColor>.op_Implicit(pxResult);
      stringList.Add(appointmentStatusColor.StatusID);
    }
    if (stringList.Count > 0)
      bqlCommand = bqlCommand.WhereAnd(InHelper<FSAppointment.status>.Create(stringList.Count));
    foreach (string str in stringList)
      objectList1.Add((object) str);
    List<FSCalendarComponentField> list = ((IEnumerable<PXResult<AppointmentBoxComponentField>>) ((PXSelectBase<AppointmentBoxComponentField>) this.ActiveAppBoxFields).Select(Array.Empty<object>())).AsEnumerable<PXResult<AppointmentBoxComponentField>>().Cast<PXResult<AppointmentBoxComponentField>>().Select<PXResult<AppointmentBoxComponentField>, FSCalendarComponentField>((Func<PXResult<AppointmentBoxComponentField>, FSCalendarComponentField>) (x => (FSCalendarComponentField) PXResult<AppointmentBoxComponentField>.op_Implicit(x))).ToList<FSCalendarComponentField>();
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList1.ToArray());
    ExternalControls.LoadCustGraph instance = PXGraph.CreateInstance<ExternalControls.LoadCustGraph>();
    foreach (PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSRoom, FSWFStage> row in objectList2)
    {
      ExternalControls.FSEntityInfo entity = new ExternalControls.FSEntityInfo();
      FSAppointment apptRow = PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSRoom, FSWFStage>.op_Implicit(row);
      FSServiceOrder fsServiceOrder = PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSRoom, FSWFStage>.op_Implicit(row);
      FSRoom fsRoom = PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSRoom, FSWFStage>.op_Implicit(row);
      if (fsRoom != null)
        entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.roomDesc), (object) fsRoom.Descr));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.canDeleteAppointment), (object) this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 4)));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.appointmentCustomID), (object) null));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customID), (object) null));
      this.AddCommonAppointmentField(entity.fields, apptRow);
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.roomID), (object) fsServiceOrder.RoomID));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.branchLocationID), (object) fsServiceOrder.BranchLocationID));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customRoomID), (object) $"{fsServiceOrder.BranchLocationID.ToString()}-{fsServiceOrder.RoomID}"));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customDateTimeStart), (object) apptRow.ScheduledDateTimeBegin.ToString()));
      entity.fields.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointmentScheduleBoard.customDateTimeEnd), (object) apptRow.ScheduledDateTimeEnd.ToString()));
      this.AddCustomFields((PXGraph) instance, (PXResult) row, list, entity);
      appointmentRecordsByRooms.Add(entity);
    }
    return appointmentRecordsByRooms;
  }

  public virtual List<ExternalControls.FSEntityInfo> UnassignedAppointmentRecords(
    DateTime timeBegin,
    DateTime timeEnd,
    int? branchID,
    int? branchLocationID,
    ExternalControls.DispatchBoardFilters[] filters,
    int limit,
    int start,
    bool isRoom,
    out int totals)
  {
    PXGraph graph = new PXGraph();
    List<object> objectList = new List<object>();
    List<ExternalControls.FSEntityInfo> fsEntityInfoList = new List<ExternalControls.FSEntityInfo>();
    PXSelectBase<FSAppointment> pxSelectBase = (PXSelectBase<FSAppointment>) new PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, LeftJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSWFStage, On<FSWFStage.wFStageID, Equal<FSAppointment.wFStageID>>, LeftJoin<FSBranchLocation, On<FSBranchLocation.branchLocationID, Equal<FSServiceOrder.branchLocationID>>, LeftJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>, And<True, Equal<Required<FSAppointmentEmployee.isStaffCalendar>>>>>>>>>>>>, Where<FSAppointment.canceled, Equal<False>, And<FSAppointment.completed, Equal<False>, And<FSAppointment.closed, Equal<False>, And2<Where2<Where<FSAppointmentEmployee.employeeID, IsNull, And<True, Equal<Required<FSAppointmentEmployee.isStaffCalendar>>>>, Or<Where<FSServiceOrder.roomID, IsNull, And<True, Equal<Required<FSAppointmentEmployee.isStaffCalendar>>>>>>, And2<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>>>>((PXGraph) this);
    objectList.Add((object) !isRoom);
    objectList.Add((object) !isRoom);
    objectList.Add((object) isRoom);
    objectList.Add((object) timeBegin);
    objectList.Add((object) timeEnd);
    if (branchID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<FSAppointment.branchID, Equal<Required<FSAppointment.branchID>>>>();
      objectList.Add((object) branchID);
    }
    if (branchLocationID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>>();
      objectList.Add((object) branchLocationID);
    }
    List<FSCalendarComponentField> list1 = ((IEnumerable<PXResult<UnassignedAppComponentField>>) ((PXSelectBase<UnassignedAppComponentField>) this.ActiveUAGridFields).Select(Array.Empty<object>())).AsEnumerable<PXResult<UnassignedAppComponentField>>().Cast<PXResult<UnassignedAppComponentField>>().Select<PXResult<UnassignedAppComponentField>, FSCalendarComponentField>((Func<PXResult<UnassignedAppComponentField>, FSCalendarComponentField>) (x => (FSCalendarComponentField) PXResult<UnassignedAppComponentField>.op_Implicit(x))).ToList<FSCalendarComponentField>();
    int num1 = 0;
    int num2 = 0;
    List<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>> list2;
    if (filters != null)
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      ExternalControls.DispatchBoardFilters dispatchBoardFilters1 = ((IEnumerable<ExternalControls.DispatchBoardFilters>) filters).Where<ExternalControls.DispatchBoardFilters>((Func<ExternalControls.DispatchBoardFilters, bool>) (i => i.property == "LikeText")).FirstOrDefault<ExternalControls.DispatchBoardFilters>();
      if (dispatchBoardFilters1 != null && !string.IsNullOrEmpty(dispatchBoardFilters1.value[0]))
        pxFilterRowList = this.GetSearchTextFilters(list1, dispatchBoardFilters1.value[0], new List<Tuple<string, string>>()
        {
          new Tuple<string, string>(typeof (FSAppointment.refNbr).Name, dispatchBoardFilters1.value[0])
        });
      list2 = ((PXSelectBase) pxSelectBase).View.Select((object[]) null, objectList.ToArray(), PXView.Searches, (string[]) null, PXView.Descendings, pxFilterRowList.ToArray(), ref num1, PXView.MaximumRows, ref num2).Cast<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>().ToList<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>();
      ExternalControls.DispatchBoardFilters dispatchBoardFilters2 = ((IEnumerable<ExternalControls.DispatchBoardFilters>) filters).Where<ExternalControls.DispatchBoardFilters>((Func<ExternalControls.DispatchBoardFilters, bool>) (i => EnumerableExtensions.IsIn<string>(i.property, "Skill", "LicenseType", "Problem", "ServiceClass"))).FirstOrDefault<ExternalControls.DispatchBoardFilters>();
      totals = num2;
      if (dispatchBoardFilters2 != null)
      {
        for (int index = 0; index < list2.Count<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>(); ++index)
        {
          FSAppointment fsAppointment = PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>.op_Implicit(list2[index]);
          List<int> second = new List<int>();
          List<int> intList = new List<int>();
          bool flag = false;
          PXResultset<FSAppointmentDet> source = ((PXSelectBase<FSAppointmentDet>) this.ActiveAppointmentDets).Select(new object[1]
          {
            (object) fsAppointment.AppointmentID
          });
          if (source.Count == 0)
          {
            list2.RemoveAt(index--);
          }
          else
          {
            ParameterExpression parameterExpression;
            // ISSUE: method reference
            // ISSUE: method reference
            List<int?> list3 = ((IQueryable<PXResult<FSAppointmentDet>>) source).Select<PXResult<FSAppointmentDet>, int?>(Expression.Lambda<Func<PXResult<FSAppointmentDet>, int?>>((Expression) Expression.Property((Expression) Expression.Call(y, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (FSAppointmentDet.get_InventoryID))), parameterExpression)).ToList<int?>();
            List<SharedClasses.ItemList> itemWithList = SharedFunctions.GetItemWithList<FSServiceSkill, FSServiceSkill.serviceID, FSServiceSkill.skillID>(graph, list3);
            foreach (PXResult<FSAppointmentDet> pxResult in source)
            {
              FSAppointmentDet fsAppointmentDetRow = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
              SharedClasses.ItemList itemList = itemWithList.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
              {
                int? itemId = y.itemID;
                int? inventoryId = fsAppointmentDetRow.InventoryID;
                return itemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & itemId.HasValue == inventoryId.HasValue;
              }));
              if (itemList != null)
              {
                foreach (int num3 in itemList.list)
                  second.Add(num3);
              }
            }
            foreach (ExternalControls.DispatchBoardFilters filter in filters)
            {
              if (filter.property == "Skill" && ((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>().Except<int>((IEnumerable<int>) second).Any<int>())
                flag = true;
            }
            if (flag)
              list2.RemoveAt(index--);
          }
        }
        totals = list2.Count<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>();
      }
    }
    else
    {
      ((PXSelectBase) pxSelectBase).View.IsReadOnly = true;
      list2 = ((PXSelectBase) pxSelectBase).View.Select((object[]) null, objectList.ToArray(), PXView.Searches, (string[]) null, PXView.Descendings, (PXFilterRow[]) null, ref num1, PXView.MaximumRows, ref num2).Cast<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>().ToList<PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>>();
      totals = num2;
    }
    int num4 = limit == 0 || limit >= totals ? totals : limit;
    ExternalControls.LoadCustGraph instance = PXGraph.CreateInstance<ExternalControls.LoadCustGraph>();
    for (int index = 0; index < num4 && start + index < list2.Count; ++index)
    {
      PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee> row = list2[index];
      FSAppointment apptRow = PXResult<FSAppointment, FSServiceOrder, PX.Objects.AR.Customer, PX.Objects.CR.Location, FSContact, FSAddress, FSWFStage, FSBranchLocation, FSAppointmentEmployee>.op_Implicit(row);
      ExternalControls.FSEntityInfo entity = new ExternalControls.FSEntityInfo();
      this.AddCommonAppointmentField(entity.fields, apptRow);
      this.AddCustomFields((PXGraph) instance, (PXResult) row, list1, entity);
      fsEntityInfoList.Add(entity);
    }
    return fsEntityInfoList;
  }

  public virtual int? DBPutAppointments(
    FSAppointmentScheduleBoard updatedAppointment,
    out bool isAppointment,
    out ExternalControls.DispatchBoardAppointmentMessages response)
  {
    ExternalControls.DispatchBoardAppointmentMessages response1 = new ExternalControls.DispatchBoardAppointmentMessages();
    isAppointment = true;
    if (!this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 2))
      response1.ErrorMessages.Add("You have insufficient access rights to perform this action.");
    try
    {
      if (updatedAppointment != null)
      {
        using (new PXScreenIDScope("FS300000"))
        {
          AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) updatedAppointment.RefNbr, new object[1]
          {
            (object) updatedAppointment.SrvOrdType
          }));
          FSAppointment current = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
          if (current != null)
          {
            DateTime? nullable1 = current.ScheduledDateTimeBegin;
            DateTime? nullable2 = updatedAppointment.ScheduledDateTimeBegin;
            if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            {
              nullable2 = current.ScheduledDateTimeEnd;
              nullable1 = updatedAppointment.ScheduledDateTimeEnd;
              if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
                goto label_9;
            }
            current.HandleManuallyScheduleTime = new bool?(true);
label_9:
            current.Confirmed = updatedAppointment.Confirmed;
            current.ValidatedByDispatcher = updatedAppointment.ValidatedByDispatcher;
            current.ScheduledDateTimeBegin = updatedAppointment.ScheduledDateTimeBegin;
            current.ScheduledDateTimeEnd = updatedAppointment.ScheduledDateTimeEnd;
            ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(current);
            this.AssignAppointmentEmployee(updatedAppointment, this, instance, ref response1);
            ((PXGraph) instance).SelectTimeStamp();
            if (!((PXGraph) instance).PressSave(response1))
              response = response1;
          }
          else
          {
            response1.ErrorMessages.Add(ExternalControls.GetErrorMessage(ExternalControls.ErrorCode.APPOINTMENT_NOT_FOUND));
            response = response1;
          }
        }
      }
    }
    catch (Exception ex)
    {
      response1.ErrorMessages.Add(ex.Message);
    }
    response = response1;
    return new int?(-1);
  }

  public virtual int? DBUnassignAppointmentBridge(
    FSAppointmentScheduleBoard fsAppointmentRow,
    out bool isAppointment,
    out ExternalControls.DispatchBoardAppointmentMessages response)
  {
    ExternalControls.DispatchBoardAppointmentMessages response1 = new ExternalControls.DispatchBoardAppointmentMessages();
    WrkProcess instance1 = PXGraph.CreateInstance<WrkProcess>();
    FSWrkProcess fsWrkProcessRow = new FSWrkProcess()
    {
      RoomID = fsAppointmentRow.RoomID,
      SOID = fsAppointmentRow.SOID,
      AppointmentID = fsAppointmentRow.AppointmentID,
      SrvOrdType = fsAppointmentRow.SrvOrdType,
      TargetScreenID = "FS300200",
      SMEquipmentID = fsAppointmentRow.SMEquipmentID,
      EmployeeIDList = fsAppointmentRow.EmployeeID.ToString(),
      LineRefList = string.Empty,
      EquipmentIDList = string.Empty
    };
    int? nullable = new int?();
    try
    {
      AppointmentEntry instance2 = PXGraph.CreateInstance<AppointmentEntry>();
      ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentRow.RefNbr, new object[1]
      {
        (object) fsAppointmentRow.SrvOrdType
      }));
      this.AssignAppointmentEmployee(fsAppointmentRow, this, instance2, ref response1);
      ((PXGraph) instance2).Actions.PressSave();
      isAppointment = true;
    }
    catch
    {
      nullable = this.SaveWrkProcessParameters(instance1, fsWrkProcessRow);
      isAppointment = false;
    }
    response = response1;
    return nullable;
  }

  public virtual ExternalControls.DispatchBoardAppointmentMessages DBDeleteAppointments(
    FSAppointment fsAppointmentRow)
  {
    ExternalControls.DispatchBoardAppointmentMessages messages = new ExternalControls.DispatchBoardAppointmentMessages();
    if (!this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 4))
    {
      messages.ErrorMessages.Add("You have insufficient access rights to perform this action.");
      return messages;
    }
    try
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      PXContext.SetScreenID(this.PageIndexingService.GetScreenIDFromGraphType(((object) instance).GetType()));
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentRow.RefNbr, new object[1]
      {
        (object) fsAppointmentRow.SrvOrdType
      }));
      fsAppointmentRow = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
      if (!((PXGraph) instance).PressDelete(messages))
        return messages;
    }
    catch (Exception ex)
    {
      messages.ErrorMessages.Add(ex.Message);
    }
    return messages;
  }

  public virtual ExternalControls.DispatchBoardAppointmentMessages CheckApppointmentCreationByAccessRights()
  {
    ExternalControls.DispatchBoardAppointmentMessages appointmentMessages = new ExternalControls.DispatchBoardAppointmentMessages();
    if (!this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 3))
      appointmentMessages.ErrorMessages.Add("You have insufficient access rights to perform this action.");
    return appointmentMessages;
  }

  public virtual int? DBCreateAppointmentBridge(
    FSAppointmentScheduleBoard fsAppointmentRow,
    List<int> sODetIDList,
    List<int> employeeIDList,
    out bool isAppointment)
  {
    WrkProcess instance = PXGraph.CreateInstance<WrkProcess>();
    FSWrkProcess fsWrkProcessRow = new FSWrkProcess()
    {
      RoomID = fsAppointmentRow.RoomID,
      SOID = fsAppointmentRow.SOID,
      SrvOrdType = fsAppointmentRow.SrvOrdType,
      BranchID = fsAppointmentRow.BranchID.GetValueOrDefault() == -1 ? new int?() : fsAppointmentRow.BranchID,
      BranchLocationID = fsAppointmentRow.BranchLocationID,
      CustomerID = fsAppointmentRow.CustomerID,
      SMEquipmentID = fsAppointmentRow.SMEquipmentID,
      ScheduledDateTimeBegin = fsAppointmentRow.ScheduledDateTimeBegin,
      ScheduledDateTimeEnd = fsAppointmentRow.ScheduledDateTimeEnd,
      TargetScreenID = "FS300200",
      EmployeeIDList = string.Join<int>(",", (IEnumerable<int>) employeeIDList.ToArray()),
      LineRefList = string.Join<int>(",", (IEnumerable<int>) sODetIDList.ToArray()),
      EquipmentIDList = string.Empty
    };
    int? nullable = new int?();
    int? appointmentBridge;
    try
    {
      appointmentBridge = instance.LaunchAppointmentEntryScreen(fsWrkProcessRow, false);
      isAppointment = true;
    }
    catch
    {
      appointmentBridge = this.SaveWrkProcessParameters(instance, fsWrkProcessRow);
      isAppointment = false;
    }
    return appointmentBridge;
  }

  public virtual FSAppointmentScheduleBoard GetCalendarAppointment(
    FSAppointmentScheduleBoard fsAppointmentScheduleBoardRow)
  {
    PXResultset<PX.Objects.IN.InventoryItem> pxResultset1 = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.AppointmentServices).Select(new object[1]
    {
      (object) fsAppointmentScheduleBoardRow.AppointmentID
    });
    if (pxResultset1 != null && pxResultset1.Count > 0)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(pxResultset1);
      fsAppointmentScheduleBoardRow.FirstServiceDesc = inventoryItem.Descr;
    }
    FSRoom fsRoom = PXResultset<FSRoom>.op_Implicit(((PXSelectBase<FSRoom>) this.RoomByBranchLocation).Select(new object[2]
    {
      (object) fsAppointmentScheduleBoardRow.BranchLocationID,
      (object) fsAppointmentScheduleBoardRow.RoomID
    }));
    if (fsRoom != null)
      fsAppointmentScheduleBoardRow.RoomDesc = fsRoom.Descr;
    PXResultset<FSAppointmentEmployee> pxResultset2 = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentEmployees).Select(new object[1]
    {
      (object) fsAppointmentScheduleBoardRow.AppointmentID
    });
    fsAppointmentScheduleBoardRow.EmployeeCount = new int?(pxResultset2.Count);
    fsAppointmentScheduleBoardRow.CanDeleteAppointment = new bool?(this.CheckAccessRights("FS300200", typeof (AppointmentEntry), typeof (FSAppointment), (PXCacheRights) 4));
    return fsAppointmentScheduleBoardRow;
  }

  public virtual FSAppointmentStaffScheduleBoard GetStaffCalendarAppointment(
    FSAppointmentStaffScheduleBoard fsAppointmentScheduleBoardRow)
  {
    fsAppointmentScheduleBoardRow = (FSAppointmentStaffScheduleBoard) this.GetCalendarAppointment((FSAppointmentScheduleBoard) fsAppointmentScheduleBoardRow);
    fsAppointmentScheduleBoardRow.OldEmployeeID = fsAppointmentScheduleBoardRow.EmployeeID;
    fsAppointmentScheduleBoardRow.CustomDateID = $"{fsAppointmentScheduleBoardRow.ScheduledDateTimeBegin.Value.Month.ToString("00")}{fsAppointmentScheduleBoardRow.ScheduledDateTimeBegin.Value.Day.ToString("00")}{fsAppointmentScheduleBoardRow.ScheduledDateTimeBegin.Value.Year.ToString("0000")}-{fsAppointmentScheduleBoardRow.EmployeeID.ToString()}";
    return fsAppointmentScheduleBoardRow;
  }

  public virtual List<ExternalControls.RouteNode> GetTreeAppointmentNodesByRoute(
    int routeDocumentID,
    FSRoute fsRouteRow)
  {
    PXResultset<FSAppointment> pxResultset = ((PXSelectBase<FSAppointment>) this.AppointmentRecordsByRoute).Select(new object[1]
    {
      (object) routeDocumentID
    });
    List<ExternalControls.RouteNode> appointmentNodesByRoute = new List<ExternalControls.RouteNode>();
    if (fsRouteRow == null)
      fsRouteRow = PXResultset<FSRoute>.op_Implicit(PXSelectBase<FSRoute, PXSelectJoin<FSRoute, InnerJoin<FSRouteDocument, On<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>, Where<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) routeDocumentID
      }));
    FSAddress fsAddressRow1 = PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsRouteRow.BeginBranchLocationID
    }));
    appointmentNodesByRoute.Add(new ExternalControls.RouteNode(fsRouteRow, fsAddressRow1, PXMessages.LocalizeFormatNoPrefix("START LOCATION", Array.Empty<object>()), routeDocumentID.ToString()));
    foreach (PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer> pxResult in pxResultset)
    {
      FSAppointment fsAppointmentRow = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSServiceOrder fsServiceOrderRow = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSSrvOrdType fsSrvOrdTypeRow = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSAddress fsAddressRow2 = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PX.Objects.AR.Customer customerRow = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PX.Objects.CR.Location locationRow = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      appointmentNodesByRoute.Add(new ExternalControls.RouteNode(fsAppointmentRow, fsSrvOrdTypeRow, fsServiceOrderRow, customerRow, locationRow, fsAddressRow2));
    }
    FSAddress fsAddressRow3 = PXResultset<FSAddress>.op_Implicit(PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsRouteRow.EndBranchLocationID
    }));
    appointmentNodesByRoute.Add(new ExternalControls.RouteNode(fsRouteRow, fsAddressRow3, PXMessages.LocalizeFormatNoPrefix("END LOCATION", Array.Empty<object>()), routeDocumentID.ToString()));
    return appointmentNodesByRoute;
  }

  public virtual List<ExternalControls.RouteNode> GetTreeAppointmentNodesByEmployee(
    int employeeID,
    DateTime calendarDate)
  {
    DateHandler dateHandler = new DateHandler(calendarDate);
    DateTime dateTime1 = dateHandler.StartOfDay();
    DateTime dateTime2 = dateHandler.BeginOfNextDay();
    PXResultset<EPEmployee> pxResultset = ((PXSelectBase<EPEmployee>) this.AppointmentsByEmployee).Select(new object[3]
    {
      (object) employeeID,
      (object) dateTime1,
      (object) dateTime2
    });
    List<ExternalControls.RouteNode> appointmentNodesByEmployee = new List<ExternalControls.RouteNode>();
    foreach (PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer> pxResult in pxResultset)
    {
      FSAppointment fsAppointmentRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSServiceOrder fsServiceOrderRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSSrvOrdType fsSrvOrdTypeRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      FSAddress fsAddressRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PX.Objects.AR.Customer customerRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      BAccountStaffMember bAccountStaffMemberRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      PX.Objects.CR.Location locationRow = PXResult<EPEmployee, FSAppointmentEmployee, FSAppointment, FSServiceOrder, FSSrvOrdType, BAccountStaffMember, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State, PX.Objects.AR.Customer>.op_Implicit(pxResult);
      appointmentNodesByEmployee.Add(new ExternalControls.RouteNode(fsAppointmentRow, fsSrvOrdTypeRow, fsServiceOrderRow, customerRow, bAccountStaffMemberRow, locationRow, fsAddressRow));
    }
    return appointmentNodesByEmployee;
  }

  public virtual List<object> GetUnassignedAppointmentNode(
    DateTime calendarDate,
    int? branchID,
    int? branchLocationID)
  {
    DateHandler dateHandler = new DateHandler(calendarDate);
    DateTime dateTime1 = dateHandler.StartOfDay();
    DateTime dateTime2 = dateHandler.BeginOfNextDay();
    List<object> objectList1 = new List<object>();
    BqlCommand bqlCommand = (BqlCommand) new Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<FSAddress.countryID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<FSAddress.countryID>, And<PX.Objects.CS.State.stateID, Equal<FSAddress.state>>>>>>>>>>>, Where2<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>, And<FSAppointmentEmployee.employeeID, IsNull, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>();
    objectList1.Add((object) dateTime1);
    objectList1.Add((object) dateTime2);
    if (branchID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSServiceOrder.branchID, Equal<Required<FSServiceOrder.branchID>>>));
      objectList1.Add((object) branchID);
    }
    if (branchLocationID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>));
      objectList1.Add((object) branchLocationID);
    }
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList1.ToArray());
    List<object> unassignedAppointmentNode = new List<object>();
    foreach (PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State> pxResult in objectList2)
    {
      FSAppointment fsAppointment = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      FSSrvOrdType fsSrvOrdType = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      FSAddress row = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      PX.Objects.AR.Customer customer = PXResult<FSAppointment, FSServiceOrder, FSSrvOrdType, PX.Objects.AR.Customer, FSAppointmentEmployee, PX.Objects.CR.Location, FSAddress, PX.Objects.CS.Country, PX.Objects.CS.State>.op_Implicit(pxResult);
      unassignedAppointmentNode.Add((object) new
      {
        NodeID = ("Unassigned-" + fsAppointment.RefNbr),
        Text = fsAppointment.RefNbr,
        CustomerName = customer.AcctName,
        Duration = fsAppointment.EstimatedDurationTotal,
        ScheduledDateTimeBegin = fsAppointment.ScheduledDateTimeBegin,
        ScheduledDateTimeEnd = fsAppointment.ScheduledDateTimeEnd,
        AutoDocDesc = fsAppointment.AutoDocDesc,
        Address = ExternalControlsHelper.GetLongAddressText(row),
        CustomerLocation = ExternalControlsHelper.GetShortAddressText(row),
        PostalCode = row.PostalCode,
        SrvOrdType = fsSrvOrdType.SrvOrdType,
        SrvOrdTypeDescr = fsSrvOrdType.Descr,
        Leaf = true,
        Checked = true,
        Latitude = fsAppointment.MapLatitude,
        Longitude = fsAppointment.MapLongitude
      });
    }
    return unassignedAppointmentNode;
  }

  public virtual ExternalControls.DispatchBoardAppointmentMessages DBPutRoutes(
    ExternalControls.RouteNode[] routeNodes)
  {
    ExternalControls.DispatchBoardAppointmentMessages appointmentMessages = new ExternalControls.DispatchBoardAppointmentMessages();
    try
    {
      for (int index = 0; index < routeNodes.Length; ++index)
      {
        if (routeNodes[index].Leaf.GetValueOrDefault() && routeNodes[index].AppointmentID.HasValue)
        {
          int? routeDocumentId = routeNodes[index].RouteDocumentID;
          int? originalRouteDocumentId = routeNodes[index].OriginalRouteDocumentID;
          if (!(routeDocumentId.GetValueOrDefault() == originalRouteDocumentId.GetValueOrDefault() & routeDocumentId.HasValue == originalRouteDocumentId.HasValue))
            RouteAppointmentAssignmentHelper.ReassignAppointmentToRoute(PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) this.RouteRecord).Select(new object[1]
            {
              (object) routeNodes[index].RouteDocumentID
            })), routeNodes[index].RefNbr, routeNodes[index].SrvOrdType);
        }
      }
      for (int index = 0; index < routeNodes.Length; ++index)
      {
        if (routeNodes[index].Leaf.GetValueOrDefault())
          PXUpdate<Set<FSAppointment.routePosition, Required<FSAppointment.routePosition>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update((PXGraph) this, new object[2]
          {
            (object) routeNodes[index].RoutePosition,
            (object) routeNodes[index].AppointmentID
          });
      }
      return (ExternalControls.DispatchBoardAppointmentMessages) null;
    }
    catch (Exception ex)
    {
      appointmentMessages.ErrorMessages.Add(ex.Message);
      return appointmentMessages;
    }
  }

  /// <summary>
  /// Launches the AppointmentEntry screen with some preloaded values.
  /// </summary>
  /// <param name="fsWrkProcessRow"><c>FSWrkProcess</c> row.</param>
  public virtual void AssignAppointmentEmployee(
    FSAppointmentScheduleBoard fsAppointmentRow,
    ExternalControls graphExternalControls,
    AppointmentEntry graphAppointmentMaint,
    ref ExternalControls.DispatchBoardAppointmentMessages response)
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) graphAppointmentMaint.ServiceOrderRelated).Current;
    if (!string.IsNullOrEmpty(fsAppointmentRow.RoomID) && !string.IsNullOrWhiteSpace(fsAppointmentRow.RoomID))
      current.RoomID = fsAppointmentRow.RoomID;
    ((PXSelectBase<FSServiceOrder>) graphAppointmentMaint.ServiceOrderRelated).Update(current);
    int? oldEmployeeId1 = fsAppointmentRow.OldEmployeeID;
    int? employeeId = fsAppointmentRow.EmployeeID;
    if (oldEmployeeId1.GetValueOrDefault() == employeeId.GetValueOrDefault() & oldEmployeeId1.HasValue == employeeId.HasValue)
      return;
    if (PXResultset<FSAppointmentEmployee>.op_Implicit(((PXSelectBase<FSAppointmentEmployee>) graphExternalControls.AppointmentEmployeeByEmployee).Select(new object[2]
    {
      (object) fsAppointmentRow.AppointmentID,
      (object) fsAppointmentRow.EmployeeID
    })) != null)
    {
      response.ErrorMessages.Add(ExternalControls.GetErrorMessage(ExternalControls.ErrorCode.APPOINTMENT_SHARED));
    }
    else
    {
      int? oldEmployeeId2 = fsAppointmentRow.OldEmployeeID;
      int num = 0;
      if (oldEmployeeId2.GetValueOrDefault() > num & oldEmployeeId2.HasValue)
      {
        FSAppointmentEmployee[] array = ((IQueryable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Select(Array.Empty<object>())).Select<PXResult<FSAppointmentEmployee>, FSAppointmentEmployee>((Expression<Func<PXResult<FSAppointmentEmployee>, FSAppointmentEmployee>>) (x => (FSAppointmentEmployee) x)).Where<FSAppointmentEmployee>((Expression<Func<FSAppointmentEmployee, bool>>) (x => x.EmployeeID == fsAppointmentRow.OldEmployeeID)).ToArray<FSAppointmentEmployee>();
        bool? primaryDriver = (bool?) ((IEnumerable<FSAppointmentEmployee>) array).ElementAtOrDefault<FSAppointmentEmployee>(0)?.PrimaryDriver;
        foreach (FSAppointmentEmployee appointmentEmployee1 in array)
        {
          FSAppointmentEmployee appointmentEmployee2 = new FSAppointmentEmployee()
          {
            AppointmentID = fsAppointmentRow.AppointmentID,
            EmployeeID = fsAppointmentRow.EmployeeID,
            ServiceLineRef = appointmentEmployee1.ServiceLineRef,
            PrimaryDriver = primaryDriver
          };
          ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Insert(appointmentEmployee2);
          ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Delete(appointmentEmployee1);
        }
      }
      else
      {
        FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
        {
          AppointmentID = fsAppointmentRow.AppointmentID,
          EmployeeID = fsAppointmentRow.EmployeeID
        };
        ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentMaint.AppointmentServiceEmployees).Insert(appointmentEmployee);
      }
    }
  }

  /// <summary>
  /// Try to save a <c>FSWrkProcess</c> record to the database.
  /// </summary>
  /// <param name="fsWrkProcessRow"><c>FSWrkProcess</c> row.</param>
  /// <returns>Returns the ProcessID of the saved record.</returns>
  public virtual int? SaveWrkProcessParameters(
    WrkProcess graphWrkProcess,
    FSWrkProcess fsWrkProcessRow)
  {
    graphWrkProcess.LaunchTargetScreen = false;
    ((PXSelectBase<FSWrkProcess>) graphWrkProcess.WrkProcessRecords).Current = ((PXSelectBase<FSWrkProcess>) graphWrkProcess.WrkProcessRecords).Insert(fsWrkProcessRow);
    ((PXAction) graphWrkProcess.Save).Press();
    return ((PXSelectBase<FSWrkProcess>) graphWrkProcess.WrkProcessRecords).Current.ProcessID;
  }

  public virtual List<PXResult<EPEmployee>> EmployeeRecords(
    int? branchID,
    int? branchLocationID,
    bool? ignoreActiveFlag,
    bool? ignoreAvailabilityFlag,
    DateTime? scheduledStartDate,
    DateTime? scheduledEndDate,
    ExternalControls.DispatchBoardFilters[] filters)
  {
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<object> objectList3 = new List<object>();
    PXGraph graph = new PXGraph();
    bool flag1 = false;
    PXResultset<FSTimeSlot> source1 = (PXResultset<FSTimeSlot>) null;
    PXResultset<FSAppointmentEmployee> source2 = (PXResultset<FSAppointmentEmployee>) null;
    PXSelectBase<EPEmployee> pxSelectBase1 = (PXSelectBase<EPEmployee>) new PXSelectJoin<EPEmployee, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>>, Where<EPEmployee.parentBAccountID, IsNotNull, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, OrderBy<Asc<EPEmployee.acctName>>>(graph);
    if (branchID.HasValue)
    {
      pxSelectBase1.WhereAnd<Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>();
      objectList1.Add((object) branchID);
    }
    if (filters != null)
    {
      for (int index = 0; index < filters.Length; ++index)
      {
        if (filters[index].property == "DefinedScheduler")
        {
          flag1 = true;
          break;
        }
      }
    }
    if (!ignoreAvailabilityFlag.GetValueOrDefault())
    {
      if (flag1)
      {
        PXSelectBase<FSTimeSlot> pxSelectBase2 = (PXSelectBase<FSTimeSlot>) new PXSelectGroupBy<FSTimeSlot, Where<FSTimeSlot.timeEnd, GreaterEqual<Required<FSTimeSlot.timeEnd>>, And<FSTimeSlot.timeStart, LessEqual<Required<FSTimeSlot.timeStart>>>>, Aggregate<Max<FSTimeSlot.employeeID, GroupBy<FSTimeSlot.employeeID>>>>((PXGraph) this);
        objectList2.Add((object) scheduledStartDate);
        objectList2.Add((object) scheduledEndDate);
        if (branchID.HasValue)
        {
          pxSelectBase2.WhereAnd<Where<FSTimeSlot.branchID, Equal<Required<FSTimeSlot.branchID>>>>();
          objectList2.Add((object) branchID);
        }
        if (branchLocationID.HasValue)
        {
          pxSelectBase2.WhereAnd<Where<FSTimeSlot.branchLocationID, Equal<Required<FSTimeSlot.branchLocationID>>>>();
          objectList2.Add((object) branchLocationID);
        }
        source1 = pxSelectBase2.Select(objectList2.ToArray());
      }
      else
      {
        PXSelectBase<FSTimeSlot> pxSelectBase3 = (PXSelectBase<FSTimeSlot>) new PXSelectGroupBy<FSTimeSlot, Where<FSTimeSlot.timeEnd, GreaterEqual<Required<FSTimeSlot.timeEnd>>>, Aggregate<Max<FSTimeSlot.employeeID, GroupBy<FSTimeSlot.employeeID>>>>((PXGraph) this);
        objectList2.Add((object) scheduledStartDate);
        if (branchID.HasValue)
        {
          pxSelectBase3.WhereAnd<Where<FSTimeSlot.branchID, Equal<Required<FSTimeSlot.branchID>>>>();
          objectList2.Add((object) branchID);
        }
        if (branchLocationID.HasValue)
        {
          pxSelectBase3.WhereAnd<Where<FSTimeSlot.branchLocationID, Equal<Required<FSTimeSlot.branchLocationID>>>>();
          objectList2.Add((object) branchLocationID);
        }
        source1 = pxSelectBase3.Select(objectList2.ToArray());
      }
      PXSelectBase<FSAppointmentEmployee> pxSelectBase4 = (PXSelectBase<FSAppointmentEmployee>) new PXSelectJoinGroupBy<FSAppointmentEmployee, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>>, Where2<Where<FSServiceOrder.canceled, Equal<False>>, And<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>>>, Aggregate<Max<FSAppointmentEmployee.employeeID, GroupBy<FSAppointmentEmployee.employeeID>>>>((PXGraph) this);
      objectList3.Add((object) scheduledStartDate);
      objectList3.Add((object) scheduledEndDate);
      if (branchID.HasValue)
      {
        pxSelectBase4.WhereAnd<Where<FSServiceOrder.branchID, Equal<Required<FSServiceOrder.branchID>>>>();
        objectList3.Add((object) branchID);
      }
      if (branchLocationID.HasValue)
      {
        pxSelectBase4.WhereAnd<Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>>();
        objectList3.Add((object) branchLocationID);
      }
      source2 = pxSelectBase4.Select(objectList3.ToArray());
    }
    if (!ignoreActiveFlag.HasValue || !ignoreActiveFlag.Value)
      pxSelectBase1.WhereAnd<Where<FSxEPEmployee.sDEnabled, Equal<True>>>();
    List<PXResult<EPEmployee>> pxResultList;
    if (filters != null)
    {
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      for (int index = 0; index < filters.Length; ++index)
      {
        if (filters[index].property == "DisplayName")
        {
          pxSelectBase1.WhereAnd<Where<EPEmployee.acctName, Like<Required<EPEmployee.acctName>>>>();
          objectList1.Add((object) $"%{filters[index].value[0]}%");
          flag2 = true;
          if (flag3 & flag4)
            break;
        }
        if (filters[index].property == "EmployeeID")
        {
          pxSelectBase1.WhereAnd<Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>();
          objectList1.Add((object) filters[index].value[0]);
          flag3 = true;
          if (flag2 & flag4)
            break;
        }
        if (filters[index].property == "ReportsTo")
        {
          pxSelectBase1.WhereAnd<Where<EPEmployee.supervisorID, Equal<Required<EPEmployee.supervisorID>>>>();
          objectList1.Add((object) filters[index].value[0]);
          flag4 = true;
          if (flag3 & flag2)
            break;
        }
      }
      List<PXResult<EPEmployee>> list1;
      if (objectList1.Count <= 0)
      {
        PXResultset<EPEmployee> source3 = pxSelectBase1.Select(Array.Empty<object>());
        list1 = source3 != null ? ((IEnumerable<PXResult<EPEmployee>>) source3).ToList<PXResult<EPEmployee>>() : (List<PXResult<EPEmployee>>) null;
      }
      else
      {
        PXResultset<EPEmployee> source4 = pxSelectBase1.Select(objectList1.ToArray());
        list1 = source4 != null ? ((IEnumerable<PXResult<EPEmployee>>) source4).ToList<PXResult<EPEmployee>>() : (List<PXResult<EPEmployee>>) null;
      }
      pxResultList = list1;
      int? nullable1 = new int?(-1);
      for (int index = 0; index < pxResultList.Count; ++index)
      {
        EPEmployee epEmployee = PXResult<EPEmployee>.op_Implicit(pxResultList[index]);
        int? baccountId = epEmployee.BAccountID;
        int? nullable2 = nullable1;
        if (baccountId.GetValueOrDefault() == nullable2.GetValueOrDefault() & baccountId.HasValue == nullable2.HasValue)
          pxResultList.RemoveAt(index--);
        nullable1 = epEmployee.BAccountID;
      }
      if (!ignoreAvailabilityFlag.GetValueOrDefault())
        pxResultList = !flag1 ? this.FilterEmployeesByBranchLocation(pxResultList, source1 != null ? ((IEnumerable<PXResult<FSTimeSlot>>) source1).ToList<PXResult<FSTimeSlot>>() : (List<PXResult<FSTimeSlot>>) null, source2 != null ? ((IEnumerable<PXResult<FSAppointmentEmployee>>) source2).ToList<PXResult<FSAppointmentEmployee>>() : (List<PXResult<FSAppointmentEmployee>>) null) : this.FilterEmployeesByBranchLocationAndScheduled(pxResultList, source1 != null ? ((IEnumerable<PXResult<FSTimeSlot>>) source1).ToList<PXResult<FSTimeSlot>>() : (List<PXResult<FSTimeSlot>>) null, source2 != null ? ((IEnumerable<PXResult<FSAppointmentEmployee>>) source2).ToList<PXResult<FSAppointmentEmployee>>() : (List<PXResult<FSAppointmentEmployee>>) null);
      List<int?> list2 = pxResultList.Select<PXResult<EPEmployee>, int?>((Func<PXResult<EPEmployee>, int?>) (y => ((PXResult) y).GetItem<EPEmployee>().BAccountID)).ToList<int?>();
      int count1 = 100;
      List<SharedClasses.ItemList> source5 = new List<SharedClasses.ItemList>();
      List<SharedClasses.ItemList> source6 = new List<SharedClasses.ItemList>();
      List<SharedClasses.ItemList> source7 = new List<SharedClasses.ItemList>();
      for (int count2 = 0; count2 < list2.Count<int?>(); count2 += count1)
      {
        List<int?> list3 = list2.Skip<int?>(count2).Take<int?>(count1).ToList<int?>();
        source5.AddRange((IEnumerable<SharedClasses.ItemList>) SharedFunctions.GetItemWithList<FSEmployeeSkill, FSEmployeeSkill.employeeID, FSEmployeeSkill.skillID>(graph, list3));
        source6.AddRange((IEnumerable<SharedClasses.ItemList>) SharedFunctions.GetItemWithList<FSLicense, FSLicense.employeeID, FSLicense.licenseTypeID, Where<FSLicense.expirationDate, GreaterEqual<Required<FSLicense.expirationDate>>, Or<FSLicense.expirationDate, IsNull>>>(graph, list3, (object) scheduledStartDate));
        source7.AddRange((IEnumerable<SharedClasses.ItemList>) SharedFunctions.GetItemWithList<FSGeoZoneEmp, FSGeoZoneEmp.employeeID, FSGeoZoneEmp.geoZoneID>(graph, list3));
      }
      for (int index = 0; index < pxResultList.Count; ++index)
      {
        EPEmployee employee = PXResult<EPEmployee>.op_Implicit(pxResultList[index]);
        List<object> objectList4 = new List<object>();
        SharedClasses.ItemList itemList1 = source5.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
        {
          int? itemId = y.itemID;
          int? baccountId = employee.BAccountID;
          return itemId.GetValueOrDefault() == baccountId.GetValueOrDefault() & itemId.HasValue == baccountId.HasValue;
        }));
        SharedClasses.ItemList itemList2 = source6.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
        {
          int? itemId = y.itemID;
          int? baccountId = employee.BAccountID;
          return itemId.GetValueOrDefault() == baccountId.GetValueOrDefault() & itemId.HasValue == baccountId.HasValue;
        }));
        SharedClasses.ItemList itemList3 = source7.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
        {
          int? itemId = y.itemID;
          int? baccountId = employee.BAccountID;
          return itemId.GetValueOrDefault() == baccountId.GetValueOrDefault() & itemId.HasValue == baccountId.HasValue;
        }));
        bool flag5 = false;
        foreach (ExternalControls.DispatchBoardFilters filter in filters)
        {
          switch (filter.property)
          {
            case "Skill":
              List<object> list4 = ((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).Cast<object>().ToList<object>();
              if (list4.Count > 0 && (itemList1 == null || list4.Except<object>((IEnumerable<object>) itemList1.list).Any<object>()))
              {
                flag5 = true;
                break;
              }
              break;
            case "LicenseType":
              List<object> list5 = ((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).Cast<object>().ToList<object>();
              if (list5.Count > 0 && (itemList2 == null || list5.Except<object>((IEnumerable<object>) itemList2.list).Any<object>()))
              {
                flag5 = true;
                break;
              }
              break;
            case "GeoZone":
              List<object> list6 = ((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).Cast<object>().ToList<object>();
              if (list6.Count > 0 && (itemList3 == null || list6.Except<object>((IEnumerable<object>) itemList3.list).Any<object>()))
              {
                flag5 = true;
                break;
              }
              break;
            case "Service":
              List<int?> list7 = ((IEnumerable<string>) filter.value).Select<string, int>(new Func<string, int>(int.Parse)).Cast<int?>().ToList<int?>();
              List<SharedClasses.ItemList> itemWithList1 = SharedFunctions.GetItemWithList<FSServiceSkill, FSServiceSkill.serviceID, FSServiceSkill.skillID>(graph, list7);
              List<SharedClasses.ItemList> itemWithList2 = SharedFunctions.GetItemWithList<FSServiceLicenseType, FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID>(graph, list7);
              foreach (string s in filter.value)
              {
                int serviceID = int.Parse(s);
                SharedClasses.ItemList itemList4 = itemWithList1.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
                {
                  int? itemId = y.itemID;
                  int num = serviceID;
                  return itemId.GetValueOrDefault() == num & itemId.HasValue;
                }));
                SharedClasses.ItemList itemList5 = itemWithList2.FirstOrDefault<SharedClasses.ItemList>((Func<SharedClasses.ItemList, bool>) (y =>
                {
                  int? itemId = y.itemID;
                  int num = serviceID;
                  return itemId.GetValueOrDefault() == num & itemId.HasValue;
                }));
                if (itemList4 != null && (itemList1 == null || itemList4.list.Except<object>((IEnumerable<object>) itemList1.list).Any<object>()))
                {
                  flag5 = true;
                  break;
                }
                if (itemList5 != null && (itemList2 == null || itemList5.list.Except<object>((IEnumerable<object>) itemList2.list).Any<object>()))
                {
                  flag5 = true;
                  break;
                }
              }
              break;
          }
        }
        if (flag5)
          pxResultList.RemoveAt(index--);
      }
    }
    else
    {
      pxResultList = objectList1.Count > 0 ? ((IEnumerable<PXResult<EPEmployee>>) pxSelectBase1.Select(objectList1.ToArray())).ToList<PXResult<EPEmployee>>() : ((IEnumerable<PXResult<EPEmployee>>) pxSelectBase1.Select(Array.Empty<object>())).ToList<PXResult<EPEmployee>>();
      if (!ignoreAvailabilityFlag.GetValueOrDefault())
        pxResultList = !flag1 ? this.FilterEmployeesByBranchLocation(pxResultList, source1 != null ? ((IEnumerable<PXResult<FSTimeSlot>>) source1).ToList<PXResult<FSTimeSlot>>() : (List<PXResult<FSTimeSlot>>) null, source2 != null ? ((IEnumerable<PXResult<FSAppointmentEmployee>>) source2).ToList<PXResult<FSAppointmentEmployee>>() : (List<PXResult<FSAppointmentEmployee>>) null) : this.FilterEmployeesByBranchLocationAndScheduled(pxResultList, source1 != null ? ((IEnumerable<PXResult<FSTimeSlot>>) source1).ToList<PXResult<FSTimeSlot>>() : (List<PXResult<FSTimeSlot>>) null, source2 != null ? ((IEnumerable<PXResult<FSAppointmentEmployee>>) source2).ToList<PXResult<FSAppointmentEmployee>>() : (List<PXResult<FSAppointmentEmployee>>) null);
    }
    return pxResultList;
  }

  public virtual List<PXResult<EPEmployee>> FilterEmployeesByBranchLocation(
    List<PXResult<EPEmployee>> bqlResultSet_EPEmployee,
    List<PXResult<FSTimeSlot>> bqlResultSet_FSTimeSlot,
    List<PXResult<FSAppointmentEmployee>> bqlResultSet_FSAppointmentEmployee)
  {
    if (bqlResultSet_EPEmployee != null)
    {
      for (int index1 = 0; index1 < bqlResultSet_EPEmployee.Count; ++index1)
      {
        bool flag1 = false;
        bool flag2 = false;
        EPEmployee epEmployee = PXResult<EPEmployee>.op_Implicit(bqlResultSet_EPEmployee[index1]);
        if (bqlResultSet_FSTimeSlot != null)
        {
          for (int index2 = 0; index2 < bqlResultSet_FSTimeSlot.Count; ++index2)
          {
            FSTimeSlot fsTimeSlot = PXResult<FSTimeSlot>.op_Implicit(bqlResultSet_FSTimeSlot[index2]);
            int? baccountId = epEmployee.BAccountID;
            int? employeeId = fsTimeSlot.EmployeeID;
            if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
            {
              bqlResultSet_FSTimeSlot.RemoveAt(index2);
              flag1 = true;
              break;
            }
          }
        }
        if (bqlResultSet_FSAppointmentEmployee != null)
        {
          for (int index3 = 0; index3 < bqlResultSet_FSAppointmentEmployee.Count; ++index3)
          {
            FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(bqlResultSet_FSAppointmentEmployee[index3]);
            int? baccountId = epEmployee.BAccountID;
            int? employeeId = appointmentEmployee.EmployeeID;
            if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
            {
              bqlResultSet_FSAppointmentEmployee.RemoveAt(index3);
              flag2 = true;
              break;
            }
          }
        }
        if (!flag1 && !flag2)
          bqlResultSet_EPEmployee.RemoveAt(index1--);
      }
    }
    return bqlResultSet_EPEmployee;
  }

  public virtual List<PXResult<EPEmployee>> FilterEmployeesByBranchLocationAndScheduled(
    List<PXResult<EPEmployee>> bqlResultSet_EPEmployee,
    List<PXResult<FSTimeSlot>> bqlResultSet_FSEmployeeSchedule,
    List<PXResult<FSAppointmentEmployee>> bqlResultSet_FSAppointmentEmployee)
  {
    if (bqlResultSet_EPEmployee != null)
    {
      for (int index1 = 0; index1 < bqlResultSet_EPEmployee.Count; ++index1)
      {
        bool flag1 = false;
        bool flag2 = false;
        EPEmployee epEmployee = PXResult<EPEmployee>.op_Implicit(bqlResultSet_EPEmployee[index1]);
        if (bqlResultSet_FSEmployeeSchedule != null)
        {
          for (int index2 = 0; index2 < bqlResultSet_FSEmployeeSchedule.Count; ++index2)
          {
            FSTimeSlot fsTimeSlot = PXResult<FSTimeSlot>.op_Implicit(bqlResultSet_FSEmployeeSchedule[index2]);
            int? baccountId = epEmployee.BAccountID;
            int? employeeId = fsTimeSlot.EmployeeID;
            if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
            {
              bqlResultSet_FSEmployeeSchedule.RemoveAt(index2);
              flag1 = true;
              break;
            }
          }
        }
        if (bqlResultSet_FSAppointmentEmployee != null)
        {
          for (int index3 = 0; index3 < bqlResultSet_FSAppointmentEmployee.Count; ++index3)
          {
            FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(bqlResultSet_FSAppointmentEmployee[index3]);
            int? baccountId = epEmployee.BAccountID;
            int? employeeId = appointmentEmployee.EmployeeID;
            if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
            {
              bqlResultSet_FSAppointmentEmployee.RemoveAt(index3);
              flag1 = true;
              break;
            }
          }
        }
        if (!flag1 && !flag2)
          bqlResultSet_EPEmployee.RemoveAt(index1--);
      }
    }
    return bqlResultSet_EPEmployee;
  }

  public virtual PXResultset<PX.Objects.AP.Vendor> VendorRecords(
    int? branchID,
    int? branchLocationID,
    bool? ignoreActiveFlag,
    bool? ignoreAvailabilityFlag,
    DateTime? scheduledStartDate,
    DateTime? scheduledEndDate,
    ExternalControls.DispatchBoardFilters[] filters)
  {
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<object> objectList3 = new List<object>();
    PXSelectBase<PX.Objects.AP.Vendor> pxSelectBase1 = (PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<FSxVendor.sDEnabled, Equal<True>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, OrderBy<Asc<PX.Objects.AP.Vendor.acctName>>>(new PXGraph());
    PXSelectBase<FSAppointmentEmployee> pxSelectBase2 = (PXSelectBase<FSAppointmentEmployee>) new PXSelectJoinGroupBy<FSAppointmentEmployee, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>>, Where2<Where<FSServiceOrder.canceled, Equal<False>>, And<Where<FSAppointment.scheduledDateTimeEnd, GreaterEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeBegin, LessEqual<Required<FSAppointment.scheduledDateTimeBegin>>>>>>, Aggregate<Max<FSAppointmentEmployee.employeeID, GroupBy<FSAppointmentEmployee.employeeID>>>>((PXGraph) this);
    objectList3.Add((object) scheduledStartDate);
    objectList3.Add((object) scheduledEndDate);
    if (branchID.HasValue)
    {
      pxSelectBase2.WhereAnd<Where<FSServiceOrder.branchID, Equal<Required<FSServiceOrder.branchID>>>>();
      objectList3.Add((object) branchID);
    }
    if (branchLocationID.HasValue)
    {
      pxSelectBase2.WhereAnd<Where<FSServiceOrder.branchLocationID, Equal<Required<FSServiceOrder.branchLocationID>>>>();
      objectList3.Add((object) branchLocationID);
    }
    PXResultset<FSAppointmentEmployee> bqlResultSet_fsAppointmentEmployeeRows = pxSelectBase2.Select(objectList3.ToArray());
    PXResultset<PX.Objects.AP.Vendor> bqlResultSet_vendorRows = objectList1.Count > 0 ? pxSelectBase1.Select(objectList1.ToArray()) : pxSelectBase1.Select(Array.Empty<object>());
    if (!ignoreAvailabilityFlag.GetValueOrDefault())
      bqlResultSet_vendorRows = this.FilterVendorByBranchLocation(bqlResultSet_vendorRows, bqlResultSet_fsAppointmentEmployeeRows);
    return bqlResultSet_vendorRows;
  }

  public virtual PXResultset<PX.Objects.AP.Vendor> FilterVendorByBranchLocation(
    PXResultset<PX.Objects.AP.Vendor> bqlResultSet_vendorRows,
    PXResultset<FSAppointmentEmployee> bqlResultSet_fsAppointmentEmployeeRows)
  {
    if (bqlResultSet_vendorRows != null)
    {
      for (int index1 = 0; index1 < bqlResultSet_vendorRows.Count; ++index1)
      {
        bool flag = false;
        PX.Objects.AP.Vendor vendor = PXResult<PX.Objects.AP.Vendor>.op_Implicit(bqlResultSet_vendorRows[index1]);
        if (bqlResultSet_fsAppointmentEmployeeRows != null)
        {
          for (int index2 = 0; index2 < bqlResultSet_fsAppointmentEmployeeRows.Count; ++index2)
          {
            FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(bqlResultSet_fsAppointmentEmployeeRows[index2]);
            int? baccountId = vendor.BAccountID;
            int? employeeId = appointmentEmployee.EmployeeID;
            if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
            {
              bqlResultSet_fsAppointmentEmployeeRows.RemoveAt(index2);
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          bqlResultSet_vendorRows.RemoveAt(index1--);
      }
    }
    return bqlResultSet_vendorRows;
  }

  public virtual List<FSTimeSlot> GetWorkingScheduleRecords(
    DateTime timeBegin,
    DateTime timeEnd,
    int? branchID,
    int? branchLocationID,
    bool compressSlot,
    int[] employeeIDList)
  {
    List<object> objectList1 = new List<object>();
    List<FSTimeSlot> workingScheduleRecords = new List<FSTimeSlot>();
    BqlCommand bqlCommand = (BqlCommand) new Select2<FSTimeSlot, LeftJoin<FSBranchLocation, On<FSBranchLocation.branchLocationID, Equal<FSTimeSlot.branchLocationID>>>, Where<FSTimeSlot.timeEnd, GreaterEqual<Required<FSTimeSlot.timeEnd>>, And<FSTimeSlot.timeStart, LessEqual<Required<FSTimeSlot.timeStart>>, And<FSTimeSlot.slotLevel, Equal<Required<FSTimeSlot.slotLevel>>>>>, OrderBy<Asc<FSAppointment.appointmentID>>>();
    objectList1.Add((object) timeBegin);
    objectList1.Add((object) timeEnd);
    objectList1.Add((object) (compressSlot ? 1 : 0));
    if (branchID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSTimeSlot.branchID, Equal<Required<FSTimeSlot.branchID>>, Or<FSTimeSlot.branchID, IsNull>>));
      objectList1.Add((object) branchID);
    }
    else
    {
      List<PX.Objects.GL.Branch> list = GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.active, Equal<True>>>.Config>.Select(new PXGraph(), Array.Empty<object>())).ToList<PX.Objects.GL.Branch>();
      if (list.Count<PX.Objects.GL.Branch>() > 0 & compressSlot)
      {
        bqlCommand = bqlCommand.WhereAnd(InHelper<FSTimeSlot.branchID>.Create(list.Count<PX.Objects.GL.Branch>()));
        foreach (PX.Objects.GL.Branch branch in list)
          objectList1.Add((object) branch.BranchID);
      }
    }
    if (branchLocationID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSTimeSlot.branchLocationID, Equal<Required<FSTimeSlot.branchLocationID>>, Or<FSTimeSlot.branchLocationID, IsNull>>));
      objectList1.Add((object) branchLocationID);
    }
    else
    {
      PXResultset<FSBranchLocation> pxResultset = ((PXSelectBase<FSBranchLocation>) this.BranchLocationRecordsByBranch).Select(new object[1]
      {
        (object) branchID
      });
      if (pxResultset.Count > 0 & compressSlot)
      {
        bqlCommand = bqlCommand.WhereAnd(InHelper<FSTimeSlot.branchLocationID>.Create(pxResultset.Count));
        foreach (PXResult<FSBranchLocation> pxResult in pxResultset)
        {
          FSBranchLocation fsBranchLocation = PXResult<FSBranchLocation>.op_Implicit(pxResult);
          objectList1.Add((object) fsBranchLocation.BranchLocationID);
        }
      }
    }
    if (employeeIDList != null && employeeIDList.Length != 0)
    {
      bqlCommand = bqlCommand.WhereAnd(InHelper<FSTimeSlot.employeeID>.Create(employeeIDList.Length));
      foreach (int employeeId in employeeIDList)
        objectList1.Add((object) employeeId);
    }
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList1.ToArray());
    DateTime? nullable1 = new DateTime?();
    DateTime? nullable2 = new DateTime?();
    int num = 0;
    foreach (PXResult<FSTimeSlot, FSBranchLocation> pxResult in objectList2)
    {
      FSTimeSlot fsTimeSlot1 = PXResult<FSTimeSlot, FSBranchLocation>.op_Implicit(pxResult);
      FSBranchLocation fsBranchLocation = PXResult<FSTimeSlot, FSBranchLocation>.op_Implicit(pxResult);
      FSTimeSlot fsTimeSlot2 = fsTimeSlot1;
      DateTime? nullable3 = fsTimeSlot1.TimeStart;
      DateTime dateTime = nullable3.Value;
      string str1 = dateTime.Month.ToString("00");
      nullable3 = fsTimeSlot1.TimeStart;
      dateTime = nullable3.Value;
      string str2 = dateTime.Day.ToString("00");
      nullable3 = fsTimeSlot1.TimeStart;
      dateTime = nullable3.Value;
      string str3 = dateTime.Year.ToString("0000");
      string str4 = str1 + str2 + str3;
      fsTimeSlot2.CustomID = str4;
      fsTimeSlot1.CustomDateID = $"{fsTimeSlot1.CustomID}-{fsTimeSlot1.EmployeeID.ToString()}";
      if (fsBranchLocation != null)
      {
        fsTimeSlot1.BranchLocationDesc = fsBranchLocation.Descr;
        fsTimeSlot1.BranchLocationCD = fsBranchLocation.BranchLocationCD;
      }
      fsTimeSlot1.WrkEmployeeScheduleID = num.ToString();
      ++num;
      DateTime? nullable4;
      if (nullable1.HasValue)
      {
        nullable3 = nullable1;
        nullable4 = fsTimeSlot1.TimeStart;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_29;
      }
      nullable1 = fsTimeSlot1.TimeStart;
label_29:
      if (nullable2.HasValue)
      {
        nullable4 = nullable2;
        nullable3 = fsTimeSlot1.TimeEnd;
        if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_32;
      }
      nullable2 = fsTimeSlot1.TimeEnd;
label_32:
      workingScheduleRecords.Add(fsTimeSlot1);
    }
    return workingScheduleRecords;
  }

  public virtual ExternalControls.DispatchBoardAppointmentMessages DBPutAvailability(
    FSTimeSlot fsTimeSlotRow)
  {
    ExternalControls.DispatchBoardAppointmentMessages messages = new ExternalControls.DispatchBoardAppointmentMessages();
    try
    {
      TimeSlotMaint instance = PXGraph.CreateInstance<TimeSlotMaint>();
      FSTimeSlot fsTimeSlot = PXResultset<FSTimeSlot>.op_Implicit(((PXSelectBase<FSTimeSlot>) this.TimeSlotRecord).Select(new object[1]
      {
        (object) fsTimeSlotRow.TimeSlotID
      }));
      if (fsTimeSlot != null)
      {
        fsTimeSlot.TimeStart = fsTimeSlotRow.TimeStart;
        fsTimeSlot.TimeEnd = fsTimeSlotRow.TimeEnd;
        TimeSpan timeSpan = fsTimeSlot.TimeEnd.Value - fsTimeSlot.TimeStart.Value;
        fsTimeSlot.TimeDiff = new Decimal?((Decimal) timeSpan.TotalMinutes);
        ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Update(fsTimeSlot);
        if (!((PXGraph) instance).PressSave(messages))
          return messages;
      }
    }
    catch (Exception ex)
    {
      messages.ErrorMessages.Add(ex.Message);
    }
    return messages;
  }

  public virtual ExternalControls.DispatchBoardAppointmentMessages DBDeleteAvailability(
    FSTimeSlot fsTimeSlotRow)
  {
    ExternalControls.DispatchBoardAppointmentMessages messages = new ExternalControls.DispatchBoardAppointmentMessages();
    try
    {
      TimeSlotMaint instance = PXGraph.CreateInstance<TimeSlotMaint>();
      ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Current = PXResultset<FSTimeSlot>.op_Implicit(((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Search<FSTimeSlot.timeSlotID>((object) fsTimeSlotRow.TimeSlotID, Array.Empty<object>()));
      ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Delete(((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Current);
      if (!((PXGraph) instance).PressSave(messages))
        return messages;
    }
    catch (Exception ex)
    {
      messages.ErrorMessages.Add(ex.Message);
    }
    return messages;
  }

  public virtual int? DBCreateWrkSchedulerBridge(FSWrkEmployeeSchedule fsWrkEmployeeScheduleRow)
  {
    return this.SaveWrkProcessParameters(PXGraph.CreateInstance<WrkProcess>(), new FSWrkProcess()
    {
      BranchID = fsWrkEmployeeScheduleRow.BranchID,
      BranchLocationID = fsWrkEmployeeScheduleRow.BranchLocationID,
      EmployeeIDList = fsWrkEmployeeScheduleRow.EmployeeID.ToString(),
      ScheduledDateTimeBegin = fsWrkEmployeeScheduleRow.TimeStart,
      ScheduledDateTimeEnd = fsWrkEmployeeScheduleRow.TimeEnd,
      TargetScreenID = "FS202200"
    });
  }

  public virtual string GetBusinessDate()
  {
    if (!((PXGraph) this).Accessinfo.BusinessDate.HasValue)
      return string.Empty;
    DateTime now = PXTimeZoneInfo.Now;
    DateTime? nullable = ((PXGraph) this).Accessinfo.BusinessDate;
    nullable = PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(nullable.Value), new DateTime?(PXTimeZoneInfo.Now));
    return nullable.Value.ToString("MM/dd/yyyy h:mm:ss tt", (IFormatProvider) new CultureInfo("en-US"));
  }

  /// <summary>Gets the error message for a given error code.</summary>
  /// <returns>String with the error message and the error code.</returns>
  public static string GetErrorMessage(ExternalControls.ErrorCode code)
  {
    string str = ((int) code).ToString() + ":";
    string errorMessage;
    switch (code)
    {
      case ExternalControls.ErrorCode.APPOINTMENT_SHARED:
        errorMessage = str + "This employee has already been assigned to this appointment.";
        break;
      case ExternalControls.ErrorCode.APPOINTMENT_NOT_FOUND:
        errorMessage = str + "The appointment you select cannot be found. Refresh the appointment and try again.";
        break;
      default:
        errorMessage = "Technical Error: Please retry the requested action.";
        break;
    }
    return errorMessage;
  }

  /// <summary>
  /// Evaluates if the current user has the access right requested in accessRight for the screen provided.
  /// </summary>
  /// <param name="screenName">Screen name to evaluate.</param>
  /// <param name="graphType">Graph type of the screen.</param>
  /// <param name="cacheType">Main <c>DAC</c> type of the screen.</param>
  /// <param name="accessRight">Access right level to evaluate.</param>
  /// <returns>True if users has the access right level requested, False, otherwise.</returns>
  public virtual bool CheckAccessRights(
    string screenName,
    System.Type graphType,
    System.Type cacheType,
    PXCacheRights accessRight)
  {
    if (!PXAccess.VerifyRights(graphType))
      return false;
    PXCacheRights pxCacheRights;
    List<string> stringList1;
    List<string> stringList2;
    PXAccess.GetRights(screenName, graphType.Name, cacheType, ref pxCacheRights, ref stringList1, ref stringList2);
    return pxCacheRights >= accessRight;
  }

  public virtual void AddCommonAppointmentField(
    List<ExternalControls.FSFieldInfo> list,
    FSAppointment apptRow)
  {
    if (apptRow == null)
      return;
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.appointmentID), (object) apptRow.AppointmentID));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.sOID), (object) apptRow.SOID));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.srvOrdType), (object) apptRow.SrvOrdType));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.refNbr), (object) apptRow.RefNbr));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.soRefNbr), (object) apptRow.SORefNbr));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.customerID), (object) apptRow.CustomerID));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.status), (object) apptRow.Status));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.notStarted), (object) apptRow.NotStarted));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.hold), (object) apptRow.Hold));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.awaiting), (object) apptRow.Awaiting));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.inProcess), (object) apptRow.InProcess));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.paused), (object) apptRow.Paused));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.completed), (object) apptRow.Completed));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.closed), (object) apptRow.Closed));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.canceled), (object) apptRow.Canceled));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.confirmed), (object) apptRow.Confirmed));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.validatedByDispatcher), (object) apptRow.ValidatedByDispatcher));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.scheduledDateTimeBegin), (object) apptRow.ScheduledDateTimeBegin));
    list.Add(new ExternalControls.FSFieldInfo(typeof (FSAppointment.scheduledDateTimeEnd), (object) apptRow.ScheduledDateTimeEnd));
  }

  private class LoadCustGraph : PXGraph<ExternalControls.LoadCustGraph>
  {
  }

  [Serializable]
  public class FSEntityInfo : ISerializable
  {
    public List<ExternalControls.FSFieldInfo> fields;

    public FSEntityInfo() => this.fields = new List<ExternalControls.FSFieldInfo>();

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      foreach (ExternalControls.FSFieldInfo field in this.fields)
        info.AddValue(field.Name, field.Value);
    }
  }

  [Serializable]
  public class FSFieldInfo : ISerializable
  {
    public string Name;
    public object Value;

    public FSFieldInfo(string name, object value)
    {
      this.Name = name;
      this.Value = value;
    }

    public FSFieldInfo(System.Type fieldType, object value)
    {
      this.Name = fieldType.Name.ToCapitalized();
      this.Value = value;
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue(this.Name, this.Value);
    }
  }

  public class DispatchBoardFilters
  {
    public string property { get; set; }

    public string[] value { get; set; }
  }

  public class DispatchBoardAppointmentMessages
  {
    public List<string> ErrorMessages;
    public List<string> WarningMessages;

    public DispatchBoardAppointmentMessages()
    {
      this.ErrorMessages = new List<string>();
      this.WarningMessages = new List<string>();
    }
  }

  [Serializable]
  public class RouteNode
  {
    public string RefNbr;
    public string NodeID;
    public string Text;
    public string CustomerName;
    public string CustomerLocation;
    public int? AppointmentID;
    public int? RouteDocumentID;
    public int? RouteID;
    public string RouteCD;
    public int? RoutePosition;
    public string LocationCD;
    public string LocationDesc;
    public string ScheduledDateTimeBegin;
    public string ScheduledDateTimeEnd;
    public string ActualStartTime;
    public string ActualEndTime;
    public int? Duration;
    public string SrvOrdType;
    public string SrvOrdTypeDescr;
    public int? ServicesDuration;
    public string Vehicle;
    public string Driver;
    public string DriverName;
    public string AutoDocDesc;
    public string Status;
    public string Description;
    public string Address;
    public string PostalCode;
    public Decimal? Latitude;
    public Decimal? Longitude;
    public string TrackingID;
    public int? OriginalRouteDocumentID;
    public int? OriginalRouteID;
    public int? OriginalRoutePosition;
    public bool? Leaf = new bool?(true);
    public bool? Checked = new bool?(true);
    public bool allowDrag = true;
    public bool allowDrop = true;
    public List<ExternalControls.RouteNode> Rows;

    public RouteNode()
    {
    }

    /// <summary>
    /// Initializes a new instance of the RouteNode class for a Begin or End Route.
    /// </summary>
    /// <param name="fsRouteRow"> Route Record. </param>
    /// <param name="fsAddressRow"> ServiceOrder Address Record. </param>
    /// <param name="locationName"> Display Text Node. </param>
    public RouteNode(
      FSRoute fsRouteRow,
      FSAddress fsAddressRow,
      string locationName,
      string nodeID)
    {
      this.NodeID = $"{locationName} ({nodeID})";
      this.Text = $"{locationName} ( {fsRouteRow.RouteCD.Trim()})";
      this.ServicesDuration = new int?(0);
      this.Address = ExternalControlsHelper.GetLongAddressText(fsAddressRow);
      this.CustomerLocation = ExternalControlsHelper.GetShortAddressText(fsAddressRow);
      this.PostalCode = fsAddressRow.PostalCode;
      this.allowDrag = false;
      this.allowDrop = false;
    }

    /// <summary>
    /// Initializes a new instance of the RouteNode class for an Appointment-Employee Tree Node.
    /// </summary>
    /// <param name="fsAppointmentRow"> Appointment Record. </param>
    /// <param name="fsSrvOrdTypeRow"> Service Order Type Record. </param>
    /// <param name="fsServiceOrderRow"> Service Order Record. </param>
    /// <param name="customerRow"> Customer Record. </param>
    /// <param name="bAccountStaffMemberRow"> Staff Member Record. </param>
    public RouteNode(
      FSAppointment fsAppointmentRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSServiceOrder fsServiceOrderRow,
      PX.Objects.AR.Customer customerRow,
      BAccountStaffMember bAccountStaffMemberRow,
      PX.Objects.CR.Location locationRow,
      FSAddress fsAddressRow)
    {
      int? nullable = bAccountStaffMemberRow.BAccountID;
      string str1 = nullable.ToString();
      nullable = fsAppointmentRow.AppointmentID;
      string str2 = nullable.ToString();
      this.NodeID = $"{str1}-{str2}";
      this.Text = $"{bAccountStaffMemberRow.AcctCD.Trim()}-{fsAppointmentRow.RefNbr}";
      this.SetCommonAttributes(fsAppointmentRow, fsSrvOrdTypeRow, fsServiceOrderRow, customerRow, locationRow, fsAddressRow);
    }

    /// <summary>
    /// Initializes a new instance of the RouteNode class for an Appointment Tree Node.
    /// </summary>
    /// <param name="fsAppointmentRow"> Appointment Record. </param>
    /// <param name="fsSrvOrdTypeRow"> Service Order Type Record. </param>
    /// <param name="fsServiceOrderRow"> Service Order Record. </param>
    /// <param name="customerRow"> Customer Record. </param>
    public RouteNode(
      FSAppointment fsAppointmentRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSServiceOrder fsServiceOrderRow,
      PX.Objects.AR.Customer customerRow,
      PX.Objects.CR.Location locationRow,
      FSAddress fsAddressRow)
    {
      this.NodeID = fsAppointmentRow.RefNbr;
      this.Text = fsAppointmentRow.RefNbr;
      this.SetCommonAttributes(fsAppointmentRow, fsSrvOrdTypeRow, fsServiceOrderRow, customerRow, locationRow, fsAddressRow);
    }

    public RouteNode(
      FSRoute fsRouteRow,
      FSRouteDocument fsRouteDocumentRow,
      List<ExternalControls.RouteNode> childNodes,
      EPEmployee driver,
      FSEquipment vehicle,
      string displayText,
      PXResultset<FSGPSTrackingRequest> fsGPSTrackingRequestRows)
    {
      this.NodeID = fsRouteDocumentRow.RefNbr;
      this.Text = displayText;
      if (fsGPSTrackingRequestRows != null && fsGPSTrackingRequestRows.Count > 0)
        this.TrackingID = PXResult<FSGPSTrackingRequest>.op_Implicit(fsGPSTrackingRequestRows[0]).TrackingID.ToString();
      this.RouteDocumentID = fsRouteDocumentRow.RouteDocumentID;
      this.RouteID = fsRouteDocumentRow.RouteID;
      this.RouteCD = fsRouteRow.RouteCD;
      this.CustomerName = string.Empty;
      this.DriverName = driver != null ? driver.AcctName : string.Empty;
      this.Duration = fsRouteDocumentRow.TotalDuration;
      this.ServicesDuration = fsRouteDocumentRow.TotalServicesDuration;
      this.Leaf = new bool?(childNodes.Count == 0);
      this.Rows = childNodes;
      this.Vehicle = vehicle != null ? vehicle.RefNbr : string.Empty;
      this.Driver = driver != null ? driver.AcctName : string.Empty;
      this.Description = fsRouteRow.Descr;
      this.Status = fsRouteDocumentRow.Status;
      this.Checked = new bool?(true);
    }

    private void SetCommonAttributes(
      FSAppointment fsAppointmentRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSServiceOrder fsServiceOrderRow,
      PX.Objects.AR.Customer customerRow,
      PX.Objects.CR.Location locationRow,
      FSAddress fsAddressRow)
    {
      this.RefNbr = fsAppointmentRow.RefNbr;
      this.CustomerName = customerRow.AcctName;
      DateTime? nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      ref DateTime? local1 = ref nullable;
      this.ScheduledDateTimeBegin = local1.HasValue ? local1.GetValueOrDefault().ToString("MM/dd/yyyy h:mm:ss tt") : (string) null;
      this.AppointmentID = fsAppointmentRow.AppointmentID;
      nullable = fsAppointmentRow.ScheduledDateTimeEnd;
      ref DateTime? local2 = ref nullable;
      this.ScheduledDateTimeEnd = local2.HasValue ? local2.GetValueOrDefault().ToString("MM/dd/yyyy h:mm:ss tt") : (string) null;
      this.ServicesDuration = fsAppointmentRow.EstimatedDurationTotal;
      this.AutoDocDesc = fsAppointmentRow.AutoDocDesc;
      this.LocationDesc = locationRow.Descr;
      this.LocationCD = locationRow.LocationCD;
      this.Address = ExternalControlsHelper.GetLongAddressText(fsAddressRow);
      this.CustomerLocation = ExternalControlsHelper.GetShortAddressText(fsAddressRow);
      this.PostalCode = fsAddressRow.PostalCode;
      this.SrvOrdType = fsSrvOrdTypeRow.SrvOrdType;
      this.SrvOrdTypeDescr = fsSrvOrdTypeRow.Descr;
      this.RoutePosition = fsAppointmentRow.RoutePosition;
      this.RouteID = fsAppointmentRow.RouteID;
      this.RouteDocumentID = fsAppointmentRow.RouteDocumentID;
      this.OriginalRoutePosition = fsAppointmentRow.RoutePosition;
      this.OriginalRouteID = fsAppointmentRow.RouteID;
      this.OriginalRouteDocumentID = fsAppointmentRow.RouteDocumentID;
      this.Leaf = new bool?(true);
      this.Checked = new bool?(true);
      this.Latitude = fsAppointmentRow.MapLatitude;
      this.Longitude = fsAppointmentRow.MapLongitude;
      nullable = fsAppointmentRow.ActualDateTimeBegin;
      ref DateTime? local3 = ref nullable;
      this.ActualStartTime = local3.HasValue ? local3.GetValueOrDefault().ToString("MM/dd/yyyy h:mm:ss tt") : (string) null;
      nullable = fsAppointmentRow.ActualDateTimeEnd;
      ref DateTime? local4 = ref nullable;
      this.ActualEndTime = local4.HasValue ? local4.GetValueOrDefault().ToString("MM/dd/yyyy h:mm:ss tt") : (string) null;
    }
  }

  public enum ErrorCode
  {
    APPOINTMENT_SHARED,
    APPOINTMENT_NOT_FOUND,
  }
}
