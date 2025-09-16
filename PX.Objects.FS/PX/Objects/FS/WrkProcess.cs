// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.WrkProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class WrkProcess : PXGraph<WrkProcess, FSWrkProcess>
{
  public const char SEPARATOR = ',';
  public bool LaunchTargetScreen = true;
  public int? processID;
  public PXSelect<FSWrkProcess> WrkProcessRecords;

  /// <summary>
  /// Split a string in several substrings by a separator character.
  /// </summary>
  /// <param name="parameters">String representing the whole parameters.</param>
  /// <param name="separator">Char representing the separation of parameters.</param>
  /// <returns>A string list.</returns>
  public virtual List<string> GetParameterList(string parameters, char separator)
  {
    List<string> parameterList = new List<string>();
    if (string.IsNullOrEmpty(parameters))
      return parameterList;
    return ((IEnumerable<string>) parameters.Split(separator)).ToList<string>();
  }

  public virtual void ValidateSrvOrdTypeNumberingSequence(PXGraph graph, string srvOrdType)
  {
    AppointmentEntry.ValidateSrvOrdTypeNumberingSequenceInt(graph, srvOrdType);
  }

  /// <summary>Delete old records from database.</summary>
  private void DeleteOldRecords()
  {
    PXDatabase.Delete<FSWrkProcess>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<FSWrkProcess.createdDateTime>((PXDbType) 4, new int?(8), (object) DateTime.Now.AddDays(-2.0), (PXComp) 5)
    });
  }

  /// <summary>
  /// Try to get the appropriate ServiceOrderType from this sources:
  /// a. <c>FSServiceOrder</c>
  /// b. <c>FSWrkProcessRow</c>
  /// c. <c>FSSetup</c>
  /// </summary>
  /// <param name="fsWrkProcessRow"><c>FSWrkProcess</c> row.</param>
  /// <param name="fsServiceOrderRow">FSServiceOrder row.</param>
  public virtual string GetSrvOrdType(
    PXGraph graph,
    FSWrkProcess fsWrkProcessRow,
    FSServiceOrder fsServiceOrderRow)
  {
    if (fsWrkProcessRow.SOID.HasValue && fsServiceOrderRow != null && !string.IsNullOrEmpty(fsServiceOrderRow.SrvOrdType))
      return fsServiceOrderRow.SrvOrdType;
    if (!string.IsNullOrEmpty(fsWrkProcessRow.SrvOrdType) && !string.IsNullOrWhiteSpace(fsWrkProcessRow.SrvOrdType))
      return fsWrkProcessRow.SrvOrdType;
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>.Config>.Select(graph, Array.Empty<object>()));
    if (userPreferences != null)
    {
      FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(userPreferences);
      if (!string.IsNullOrEmpty(extension.DfltSrvOrdType))
        return extension.DfltSrvOrdType;
    }
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(PXSetup<FSSetup>.Select(graph, Array.Empty<object>()));
    return fsSetup != null && !string.IsNullOrEmpty(fsSetup.DfltSrvOrdType) ? fsSetup.DfltSrvOrdType : (string) null;
  }

  /// <summary>
  /// Try to retrieve a ServiceOrder row associated to the supplied <c>WrkProcess</c> row.
  /// </summary>
  /// <param name="fsWrkProcessRow"><c>FSWrkProcess</c> row.</param>
  /// <returns><c>FSServiceOrder</c> row.</returns>
  public virtual FSServiceOrder GetServiceOrder(PXGraph graph, FSWrkProcess fsWrkProcessRow)
  {
    if (fsWrkProcessRow == null)
      return (FSServiceOrder) null;
    if (!fsWrkProcessRow.SOID.HasValue)
      return (FSServiceOrder) null;
    return PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsWrkProcessRow.SOID
    }));
  }

  /// <summary>
  /// Launches the target screen specified in the <c>FSWrkProcess</c> row.
  /// </summary>
  /// <param name="processID"><c>Int</c> id of the process.</param>
  public virtual void LaunchScreen(int? processID)
  {
    this.DeleteOldRecords();
    FSWrkProcess fsWrkProcessRow = PXResultset<FSWrkProcess>.op_Implicit(PXSelectBase<FSWrkProcess, PXSelect<FSWrkProcess, Where<FSWrkProcess.processID, Equal<Required<FSWrkProcess.processID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) processID
    }));
    if (fsWrkProcessRow == null || !(fsWrkProcessRow.TargetScreenID == "FS300200"))
      return;
    this.LaunchAppointmentEntryScreen(fsWrkProcessRow);
  }

  public virtual void AssignAppointmentRoom(
    AppointmentEntry graphAppointmentEntry,
    FSWrkProcess fsWrkProcessRow,
    FSServiceOrder fsServiceOrderRow = null)
  {
    if (string.IsNullOrEmpty(fsWrkProcessRow.RoomID) || string.IsNullOrWhiteSpace(fsWrkProcessRow.RoomID))
      return;
    ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).SetValueExt<FSServiceOrder.roomID>(((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current, (object) fsWrkProcessRow.RoomID);
    if (fsServiceOrderRow == null)
      fsServiceOrderRow = this.GetServiceOrder((PXGraph) graphAppointmentEntry, fsWrkProcessRow);
    if (fsServiceOrderRow == null)
      return;
    ((PXSelectBase) graphAppointmentEntry.ServiceOrderRelated).Cache.SetStatus((object) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current, (PXEntryStatus) 1);
  }

  public virtual void AssignAppointmentEmployeeByList(
    AppointmentEntry graphAppointmentEntry,
    List<string> employeeList,
    List<string> soDetIDList)
  {
    employeeList.Reverse();
    if (employeeList.Count <= 0 || soDetIDList.Count > 0)
      return;
    for (int index = 0; index < employeeList.Count; ++index)
    {
      FSAppointmentEmployee appointmentEmployee1 = new FSAppointmentEmployee();
      FSAppointmentEmployee appointmentEmployee2 = ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentEntry.AppointmentServiceEmployees).Insert(appointmentEmployee1);
      ((PXSelectBase) graphAppointmentEntry.AppointmentServiceEmployees).Cache.SetValueExt<FSAppointmentEmployee.employeeID>((object) appointmentEmployee2, (object) Convert.ToInt32(employeeList[index]));
    }
  }

  public virtual int? LaunchAppointmentEntryScreen(
    FSWrkProcess fsWrkProcessRow,
    bool redirect = true,
    bool fromCalendar = false,
    MainAppointmentFilter query = null,
    PXBaseRedirectException.WindowMode? editorMode = null)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    if (fromCalendar)
    {
      instance.DisableServiceOrderUnboundFieldCalc = true;
      instance.SkipEarningTypeCheck = true;
    }
    List<string> parameterList1 = this.GetParameterList(fsWrkProcessRow.LineRefList, ',');
    List<string> parameterList2 = this.GetParameterList(fsWrkProcessRow.EmployeeIDList, ',');
    int? nullable1 = fsWrkProcessRow.AppointmentID;
    int? nullable2;
    if (nullable1.HasValue)
    {
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsWrkProcessRow.AppointmentID, new object[1]
      {
        (object) fsWrkProcessRow.SrvOrdType
      }));
      this.AssignAppointmentRoom(instance, fsWrkProcessRow);
      this.AssignAppointmentEmployeeByList(instance, parameterList2, parameterList1);
    }
    else
    {
      FSAppointment fsAppointment = new FSAppointment();
      FSServiceOrder serviceOrder = this.GetServiceOrder((PXGraph) this, fsWrkProcessRow);
      fsAppointment.SrvOrdType = this.GetSrvOrdType((PXGraph) this, fsWrkProcessRow, serviceOrder);
      nullable1 = fsAppointment.SrvOrdType != null ? fsAppointment.SOID : throw new PXException("The default service order type has not been defined. Specify the default type in the Default Service Order Type box on the Service Management Preferences (FS100100) form.");
      if (!nullable1.HasValue)
        this.ValidateSrvOrdTypeNumberingSequence((PXGraph) this, fsAppointment.SrvOrdType);
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Insert(fsAppointment);
      bool flag = true;
      if (fsWrkProcessRow.ScheduledDateTimeBegin.HasValue)
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.scheduledDateTimeBegin>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) fsWrkProcessRow.ScheduledDateTimeBegin);
      else
        flag = false;
      DateTime? nullable3 = fsWrkProcessRow.ScheduledDateTimeEnd;
      if (nullable3.HasValue & flag)
      {
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.handleManuallyScheduleTime>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) true);
        nullable3 = fsWrkProcessRow.ScheduledDateTimeBegin;
        DateTime? nullable4 = fsWrkProcessRow.ScheduledDateTimeEnd;
        if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.scheduledDateTimeEnd>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) fsWrkProcessRow.ScheduledDateTimeEnd);
        }
        else
        {
          PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords = instance.AppointmentRecords;
          FSAppointment current = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
          nullable4 = fsWrkProcessRow.ScheduledDateTimeBegin;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local = (ValueType) nullable4.Value.AddHours(1.0);
          ((PXSelectBase<FSAppointment>) appointmentRecords).SetValueExt<FSAppointment.scheduledDateTimeEnd>(current, (object) local);
        }
      }
      if (serviceOrder == null)
      {
        nullable1 = fsWrkProcessRow.BranchID;
        if (nullable1.HasValue)
          ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).SetValueExt<FSServiceOrder.branchID>(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).Current, (object) fsWrkProcessRow.BranchID);
        nullable1 = fsWrkProcessRow.BranchLocationID;
        if (nullable1.HasValue)
        {
          nullable1 = fsWrkProcessRow.BranchLocationID;
          int num = 0;
          if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
            ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).SetValueExt<FSServiceOrder.branchLocationID>(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).Current, (object) fsWrkProcessRow.BranchLocationID);
        }
        nullable1 = fsWrkProcessRow.CustomerID;
        if (nullable1.HasValue)
        {
          nullable1 = fsWrkProcessRow.CustomerID;
          int num = 0;
          if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
            ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.customerID>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) fsWrkProcessRow.CustomerID);
        }
      }
      else
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.soRefNbr>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) serviceOrder.RefNbr);
      this.AssignAppointmentRoom(instance, fsWrkProcessRow, serviceOrder);
      if (parameterList1.Count == 0 && serviceOrder != null)
      {
        List<FSSODet> list = GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) new PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.lineType, Equal<FSLineType.Service>>>>>.And<BqlOperand<FSSODet.sOID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly((PXGraph) instance)).Select(new object[1]
        {
          (object) serviceOrder.SOID
        })).ToList<FSSODet>();
        if (list.Count == 1)
        {
          FSSODet fssoDet = list.First<FSSODet>();
          if (fssoDet != null)
          {
            nullable1 = fssoDet.SODetID;
            if (nullable1.HasValue)
            {
              nullable1 = fssoDet.SODetID;
              int num = 0;
              if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
              {
                List<string> stringList = parameterList1;
                nullable1 = fssoDet.SODetID;
                string str = nullable1.Value.ToString();
                stringList.Add(str);
              }
            }
          }
        }
      }
      parameterList1.Reverse();
      if (parameterList1.Count > 0)
      {
        foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) instance.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => !x.IsInventoryItem)))
        {
          List<string> stringList = parameterList1;
          nullable1 = fsAppointmentDet.SODetID;
          string str = nullable1.ToString();
          if (!stringList.Contains(str))
            ((PXSelectBase<FSAppointmentDet>) instance.AppointmentDetails).Delete(fsAppointmentDet);
          else
            this.InsertEmployeeLinkedToService(instance, parameterList2, fsAppointmentDet.LineRef);
        }
        foreach (FSAppointmentEmployee appointmentEmployee in GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ => _.ServiceLineRef != null)))
        {
          FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) PXSelectorAttribute.Select<FSAppointmentEmployee.serviceLineRef>(((PXSelectBase) instance.AppointmentServiceEmployees).Cache, (object) appointmentEmployee);
          if (fsAppointmentDet != null && !parameterList1.Contains(fsAppointmentDet.SODetID.ToString()))
            ((PXSelectBase<FSAppointmentEmployee>) instance.AppointmentServiceEmployees).Delete(appointmentEmployee);
        }
      }
      this.AssignAppointmentEmployeeByList(instance, parameterList2, parameterList1);
      List<string> parameterList3 = this.GetParameterList(fsWrkProcessRow.EquipmentIDList, ',');
      parameterList3.Reverse();
      if (parameterList3.Count > 0)
      {
        for (int index = 0; index < parameterList3.Count; ++index)
          ((PXSelectBase<FSAppointmentResource>) instance.AppointmentResources).Insert(new FSAppointmentResource()
          {
            SMEquipmentID = new int?(Convert.ToInt32(parameterList3[index]))
          });
      }
      if (query != null)
      {
        nullable1 = query.Description?.Length;
        int num = 0;
        if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.docDesc>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) query?.Description);
      }
      if (query != null)
      {
        nullable1 = query.LongDescr?.Length;
        int num = 0;
        if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.longDescr>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) query?.LongDescr);
      }
      if (query != null)
      {
        nullable1 = query.ContactID;
        if (nullable1.HasValue)
        {
          AppointmentEntry.ServiceOrderRelated_View serviceOrderRelated = instance.ServiceOrderRelated;
          FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).Current;
          int? nullable5;
          if (query == null)
          {
            nullable1 = new int?();
            nullable5 = nullable1;
          }
          else
            nullable5 = query.ContactID;
          // ISSUE: variable of a boxed type
          __Boxed<int?> local = (ValueType) nullable5;
          ((PXSelectBase<FSServiceOrder>) serviceOrderRelated).SetValueExt<FSServiceOrder.contactID>(current, (object) local);
        }
      }
      if (query != null)
      {
        nullable1 = query.LocationID;
        if (nullable1.HasValue)
        {
          nullable1 = ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).Current.LocationID;
          nullable2 = (int?) query?.LocationID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            AppointmentEntry.ServiceOrderRelated_View serviceOrderRelated = instance.ServiceOrderRelated;
            FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRelated).Current;
            int? nullable6;
            if (query == null)
            {
              nullable2 = new int?();
              nullable6 = nullable2;
            }
            else
              nullable6 = query.LocationID;
            // ISSUE: variable of a boxed type
            __Boxed<int?> local = (ValueType) nullable6;
            ((PXSelectBase<FSServiceOrder>) serviceOrderRelated).SetValueExt<FSServiceOrder.locationID>(current, (object) local);
          }
        }
      }
    }
    nullable2 = fsWrkProcessRow.SMEquipmentID;
    if (nullable2.HasValue)
    {
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.mem_SMequipmentID>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) fsWrkProcessRow.SMEquipmentID);
      redirect = true;
    }
    if (redirect)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = editorMode ?? (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
    try
    {
      instance.RecalculateExternalTaxesSync = true;
      ((PXGraph) instance).Actions.PressSave();
    }
    catch (Exception ex)
    {
      throw ex;
    }
    finally
    {
      instance.RecalculateExternalTaxesSync = false;
    }
    if (query != null && query.OnHold.GetValueOrDefault())
      ((PXAction) instance.putOnHold).Press();
    return ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current?.AppointmentID;
  }

  public virtual void InsertEmployeeLinkedToService(
    AppointmentEntry graphAppointmentEntry,
    List<string> employeeList,
    string lineRef)
  {
    foreach (string employee in employeeList)
    {
      int num = -1;
      ref int local = ref num;
      if (int.TryParse(employee, out local))
      {
        FSAppointmentEmployee appointmentEmployee1 = new FSAppointmentEmployee();
        FSAppointmentEmployee appointmentEmployee2 = ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentEntry.AppointmentServiceEmployees).Insert(appointmentEmployee1);
        ((PXSelectBase) graphAppointmentEntry.AppointmentServiceEmployees).Cache.SetValueExt<FSAppointmentEmployee.employeeID>((object) appointmentEmployee2, (object) num);
        ((PXSelectBase) graphAppointmentEntry.AppointmentServiceEmployees).Cache.SetValueExt<FSAppointmentEmployee.serviceLineRef>((object) appointmentEmployee2, (object) lineRef);
      }
    }
  }

  protected virtual void FSWrkProcess_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || !this.LaunchTargetScreen)
      return;
    FSWrkProcess row = (FSWrkProcess) e.Row;
    int? processId1 = row.ProcessID;
    int num1 = 0;
    if (processId1.GetValueOrDefault() > num1 & processId1.HasValue && !this.processID.HasValue)
      this.processID = row.ProcessID;
    int? processId2 = this.processID;
    int num2 = 0;
    if (!(processId2.GetValueOrDefault() > num2 & processId2.HasValue))
      return;
    this.LaunchScreen(this.processID);
    this.LaunchTargetScreen = false;
  }
}
