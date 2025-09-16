// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteScheduleProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.Scheduler;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RouteScheduleProcess : 
  ContractGenerationEnqBase<RouteScheduleProcess, FSRouteContractScheduleFSServiceContract, RouteServiceContractFilter, ListField_RecordType_ContractSchedule.RouteServiceContract>
{
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (RouteServiceContractFilter))]
  public PXFilteredProcessingJoin<FSRouteContractScheduleFSServiceContract, RouteServiceContractFilter, InnerJoin<FSScheduleRoute, On<FSScheduleRoute.scheduleID, Equal<FSSchedule.scheduleID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSRouteContractSchedule.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where2<Where<FSSchedule.active, Equal<True>, And<Where<FSSchedule.nextExecutionDate, LessEqual<Current<RouteServiceContractFilter.toDate>>, And<Where<FSSchedule.enableExpirationDate, Equal<False>, Or<FSSchedule.endDate, Greater<FSSchedule.nextExecutionDate>>>>>>>, And2<Where<Current<RouteServiceContractFilter.routeID>, IsNull, Or<FSScheduleRoute.routeIDMonday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDTuesday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDWednesday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDThursday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDFriday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDSaturday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.routeIDSunday, Equal<Current<RouteServiceContractFilter.routeID>>, Or<FSScheduleRoute.dfltRouteID, Equal<Current<RouteServiceContractFilter.routeID>>>>>>>>>>>, And2<Where<Current<RouteServiceContractFilter.scheduleID>, IsNull, Or<FSSchedule.scheduleID, Equal<Current<RouteServiceContractFilter.scheduleID>>>>, And<FSSchedule.startDate, LessEqual<Current<RouteServiceContractFilter.toDate>>>>>>, OrderBy<Asc<FSRouteContractSchedule.customerID, Asc<FSRouteContractSchedule.entityID, Asc<FSRouteContractSchedule.refNbr>>>>> RouteContractSchedules;
  public new ContractGenerationEnqBase<RouteScheduleProcess, FSRouteContractScheduleFSServiceContract, RouteServiceContractFilter, ListField_RecordType_ContractSchedule.RouteServiceContract>.ContractHistoryRecords_View ContractHistoryRecords;
  public new ContractGenerationEnqBase<RouteScheduleProcess, FSRouteContractScheduleFSServiceContract, RouteServiceContractFilter, ListField_RecordType_ContractSchedule.RouteServiceContract>.ErrorMessageRecords_View ErrorMessageRecords;
  public PXSelect<FSRoute> Routes;
  public PXSelect<FSServiceContract> ServiceContractsRecords;
  [PXHidden]
  public PXSetup<FSRouteSetup> RouteSetupRecord;
  public PXAction<RouteServiceContractFilter> fixSchedulesWithoutNextExecutionDate;

  public RouteScheduleProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RouteScheduleProcess.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new RouteScheduleProcess.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.processor = (RouteScheduleProcess) null;
    // ISSUE: method pointer
    ((PXProcessingBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).SetProcessDelegate(new PXProcessingBase<FSRouteContractScheduleFSServiceContract>.ProcessListDelegate((object) cDisplayClass00, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  /// <summary>
  /// Process all Schedules (FSSchedule) in each Contract (FSContract).
  /// </summary>
  protected virtual void processServiceContract(
    PXCache cache,
    FSRouteContractScheduleFSServiceContract fsScheduleRow,
    DateTime? fromDate,
    DateTime? toDate)
  {
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
    fsScheduleRow.ContractDescr = fsScheduleRow.DocDesc;
    List<PX.Objects.FS.Scheduler.Schedule> schedule1 = MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, (FSSchedule) fsScheduleRow, toDate, "IRSC");
    foreach (PX.Objects.FS.Scheduler.Schedule schedule2 in schedule1)
    {
      schedule2.Priority = new int?((int) RouteScheduleProcess.SetSchedulePriority(schedule2, (PXGraph) this));
      schedule2.RouteInfoList = RouteScheduleProcess.getRouteListFromSchedule(schedule2, (PXGraph) this);
    }
    this.GenerateAPPSOUpdateContracts(schedule1, "IRSC", fromDate, toDate, (FSSchedule) fsScheduleRow);
  }

  /// <summary>Update all routes by generation ID.</summary>
  protected virtual void updateRoutes(int? generationID)
  {
    PXResultset<FSRouteDocument> pxResultset = PXSelectBase<FSRouteDocument, PXSelectJoinGroupBy<FSRouteDocument, InnerJoin<FSAppointment, On<FSAppointment.routeDocumentID, Equal<FSRouteDocument.routeDocumentID>>>, Where<FSAppointment.generationID, Equal<Required<FSAppointment.generationID>>>, Aggregate<GroupBy<FSRouteDocument.routeDocumentID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) generationID
    });
    RouteDocumentMaint instance = PXGraph.CreateInstance<RouteDocumentMaint>();
    foreach (PXResult<FSRouteDocument> pxResult in pxResultset)
    {
      FSRouteDocument fsRouteDocument = PXResult<FSRouteDocument>.op_Implicit(pxResult);
      try
      {
        ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Search<FSRouteDocument.routeDocumentID>((object) fsRouteDocument.RouteDocumentID, Array.Empty<object>()));
        instance.NormalizeAppointmentPosition();
        if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current != null)
        {
          if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.AutoCalculateRouteStats.GetValueOrDefault())
            GraphHelper.PressButton((PXAction) instance.calculateRouteStats);
          else
            instance.calculateSimpleRouteStatistic();
        }
      }
      catch (Exception ex)
      {
      }
    }
  }

  /// <summary>
  /// Returns the priority of the Schedule depending on its Time Restrictions or Route information.
  /// </summary>
  public static PX.Objects.FS.Scheduler.Schedule.ScheduleGenerationPriority SetSchedulePriority(
    PX.Objects.FS.Scheduler.Schedule schedule,
    PXGraph graph)
  {
    foreach (PXResult<FSContractSchedule, FSScheduleRoute> pxResult in PXSelectBase<FSContractSchedule, PXSelectJoin<FSContractSchedule, LeftJoin<FSScheduleRoute, On<FSScheduleRoute.scheduleID, Equal<FSSchedule.scheduleID>>>, Where<FSContractSchedule.entityID, Equal<Required<FSContractSchedule.entityID>>>>.Config>.Select(graph, new object[1]
    {
      (object) schedule.EntityID
    }))
    {
      FSContractSchedule contractSchedule = PXResult<FSContractSchedule, FSScheduleRoute>.op_Implicit(pxResult);
      FSScheduleRoute fsScheduleRoute = PXResult<FSContractSchedule, FSScheduleRoute>.op_Implicit(pxResult);
      bool? nullable = contractSchedule.RestrictionMax;
      if (!nullable.GetValueOrDefault())
      {
        nullable = contractSchedule.RestrictionMin;
        if (!nullable.GetValueOrDefault())
        {
          if (fsScheduleRoute.DfltRouteID.HasValue)
            return PX.Objects.FS.Scheduler.Schedule.ScheduleGenerationPriority.Sequence;
          continue;
        }
      }
      return PX.Objects.FS.Scheduler.Schedule.ScheduleGenerationPriority.TimeRestriction;
    }
    return PX.Objects.FS.Scheduler.Schedule.ScheduleGenerationPriority.Nothing;
  }

  /// <summary>
  /// Prepares a new List[RouteInfo] with the information of the routes defined in a particular FSScheduleRoute.
  /// </summary>
  public static List<RouteInfo> getRouteListFromSchedule(PX.Objects.FS.Scheduler.Schedule schedule, PXGraph graph)
  {
    List<RouteInfo> listFromSchedule = new List<RouteInfo>();
    foreach (PXResult<FSContractSchedule, FSScheduleRoute> pxResult in PXSelectBase<FSContractSchedule, PXSelectJoin<FSContractSchedule, LeftJoin<FSScheduleRoute, On<FSScheduleRoute.scheduleID, Equal<FSSchedule.scheduleID>>>, Where<FSContractSchedule.entityID, Equal<Required<FSContractSchedule.entityID>>>>.Config>.Select(graph, new object[1]
    {
      (object) schedule.EntityID
    }))
    {
      FSScheduleRoute fsScheduleRoute = PXResult<FSContractSchedule, FSScheduleRoute>.op_Implicit(pxResult);
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDSunday, new int?(int.Parse(fsScheduleRoute.SequenceSunday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDMonday, new int?(int.Parse(fsScheduleRoute.SequenceMonday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDTuesday, new int?(int.Parse(fsScheduleRoute.SequenceTuesday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDWednesday, new int?(int.Parse(fsScheduleRoute.SequenceWednesday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDThursday, new int?(int.Parse(fsScheduleRoute.SequenceThursday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDFriday, new int?(int.Parse(fsScheduleRoute.SequenceFriday))));
      listFromSchedule.Add(new RouteInfo(fsScheduleRoute.RouteIDSaturday, new int?(int.Parse(fsScheduleRoute.SequenceSaturday))));
    }
    return listFromSchedule;
  }

  [PXString]
  protected virtual void FSServiceContract_FormCaptionDescription_CacheAttached(PXCache sender)
  {
  }

  [CustomerActive]
  [PXDefault]
  protected virtual void FSRouteContractScheduleFSServiceContract_CustomerID_CacheAttached(
    PXCache sender)
  {
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<ARRegister.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  protected virtual void FSRouteContractScheduleFSServiceContract_CustomerLocationID_CacheAttached(
    PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search3<FSSchedule.refNbr, OrderBy<Desc<FSSchedule.refNbr>>>))]
  protected virtual void FSSchedule_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Generated Appointment Date")]
  protected virtual void FSRouteContractSchedule_FSServiceContract_LastGeneratedElementDate_CacheAttached(
    PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Schedule Start Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  protected virtual void FSRouteContractScheduleFSServiceContract_StartDate_CacheAttached(
    PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Schedule Expiration Date")]
  protected virtual void FSRouteContractScheduleFSServiceContract_EndDate_CacheAttached(
    PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public override void openScheduleScreenBySchedules()
  {
    RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
    FSRouteContractSchedule contractSchedule = PXResultset<FSRouteContractSchedule>.op_Implicit(PXSelectBase<FSRouteContractSchedule, PXSelect<FSRouteContractSchedule, Where<FSRouteContractSchedule.refNbr, Equal<Required<FSRouteContractSchedule.refNbr>>, And<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSRouteContractSchedule.entityID, Equal<Required<FSRouteContractSchedule.entityID>>, And<FSRouteContractSchedule.customerID, Equal<Required<FSRouteContractSchedule.customerID>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.RefNbr,
      (object) ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.EntityID,
      (object) ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.CustomerID
    }));
    ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openScheduleScreenByGenerationLogError()
  {
    RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
    FSRouteContractSchedule contractSchedule = PXResultset<FSRouteContractSchedule>.op_Implicit(PXSelectBase<FSRouteContractSchedule, PXSelect<FSRouteContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSGenerationLogError>) this.ErrorMessageRecords).Current.ScheduleID
    }));
    ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openServiceContractScreenBySchedules()
  {
    RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = FSServiceContract.PK.Find((PXGraph) this, ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.EntityID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openServiceContractScreenByGenerationLogError()
  {
    RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
    FSServiceContract fsServiceContract = FSServiceContract.PK.Find((PXGraph) this, PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSGenerationLogError>) this.ErrorMessageRecords).Current.ScheduleID
    })).EntityID);
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = fsServiceContract;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void clearAll()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new RouteScheduleProcess.\u003C\u003Ec__DisplayClass22_0()
    {
      graphGenerationLogError = PXGraph.CreateInstance<GenerationLogErrorMaint>(),
      bqlResultSet = PXSelectBase<FSGenerationLogError, PXSelect<FSGenerationLogError, Where<FSGenerationLogError.ignore, Equal<False>, And<FSGenerationLogError.processType, Equal<Required<FSGenerationLogError.processType>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) "IRSC"
      })
    }, __methodptr(\u003CclearAll\u003Eb__0)));
  }

  [PXButton]
  [PXUIField(DisplayName = "Fix Schedules Without Next Execution Date", Visible = false)]
  public virtual IEnumerable FixSchedulesWithoutNextExecutionDate(PXAdapter adapter)
  {
    SharedFunctions.UpdateSchedulesWithoutNextExecution((PXGraph) this, ((PXSelectBase) this.Filter).Cache);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<RouteServiceContractFilter> e)
  {
    if (e.Row == null)
      return;
    RouteServiceContractFilter row = e.Row;
    PXUIFieldAttribute.SetVisible<RouteServiceContractFilter.preassignedDriver>(((PXSelectBase) this.Filter).Cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<RouteServiceContractFilter.preassignedVehicle>(((PXSelectBase) this.Filter).Cache, (object) row, false);
    bool warning = false;
    string str = SharedFunctions.WarnUserWithSchedulesWithoutNextExecution((PXGraph) this, ((PXSelectBase) this.Filter).Cache, (PXAction) this.fixSchedulesWithoutNextExecutionDate, out warning);
    if (!warning)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<RouteServiceContractFilter>>) e).Cache.RaiseExceptionHandling<StaffScheduleFilter.toDate>((object) row, (object) row.ToDate, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
  }
}
