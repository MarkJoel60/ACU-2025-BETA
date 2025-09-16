// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.FS.Scheduler;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

[TableAndChartDashboardType]
public class ServiceContractInq : 
  ContractGenerationEnqBase<ServiceContractInq, FSContractSchedule, ServiceContractFilter, ListField_RecordType_ContractSchedule.ServiceContract>
{
  [PXHidden]
  public PXSelect<FSServiceContract> ServiceContracts;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (ServiceContractFilter))]
  public PXFilteredProcessingJoin<FSContractSchedule, ServiceContractFilter, InnerJoin<FSServiceContract, On<FSSchedule.entityID, Equal<FSServiceContract.serviceContractID>, And<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where2<Where<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>, And<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>>>, And2<Where<FSSchedule.active, Equal<True>>, And2<Where<FSSchedule.nextExecutionDate, LessEqual<Current<ServiceContractFilter.toDate>>, And<Where<FSSchedule.enableExpirationDate, Equal<False>, Or<FSSchedule.endDate, Greater<FSSchedule.nextExecutionDate>>>>>, And2<Where<Current<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<Current<ServiceContractFilter.customerID>>>>, And2<Where<Current<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<Current<ServiceContractFilter.branchID>>>>, And2<Where<Current<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<Current<ServiceContractFilter.branchLocationID>>>>, And2<Where<Current<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<Current<ServiceContractFilter.customerLocationID>>>>, And2<Where<Current<ServiceContractFilter.scheduleID>, IsNull, Or<FSSchedule.scheduleID, Equal<Current<ServiceContractFilter.scheduleID>>>>, And<FSSchedule.startDate, LessEqual<Current<ServiceContractFilter.toDate>>>>>>>>>>>, OrderBy<Asc<FSContractSchedule.customerID, Asc<FSContractSchedule.entityID, Asc<FSContractSchedule.refNbr>>>>> ServiceContractSchedules;
  public new ContractGenerationEnqBase<ServiceContractInq, FSContractSchedule, ServiceContractFilter, ListField_RecordType_ContractSchedule.ServiceContract>.ContractHistoryRecords_View ContractHistoryRecords;
  public new ContractGenerationEnqBase<ServiceContractInq, FSContractSchedule, ServiceContractFilter, ListField_RecordType_ContractSchedule.ServiceContract>.ErrorMessageRecords_View ErrorMessageRecords;
  public PXAction<ServiceContractFilter> fixSchedulesWithoutNextExecutionDate;

  public ServiceContractInq()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceContractInq.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new ServiceContractInq.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    ((PXProcessingBase<FSContractSchedule>) this.ServiceContractSchedules).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings =>
    {
      settings.IsEnabled = true;
      settings.BatchSize = 10;
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.ServiceContractFilterCurrent = ((PXSelectBase<ServiceContractFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<FSContractSchedule>) this.ServiceContractSchedules).SetProcessDelegate<ServiceContractInq>(new PXProcessingBase<FSContractSchedule>.ProcessItemDelegate<ServiceContractInq>((object) cDisplayClass00, __methodptr(\u003C\u002Ector\u003Eb__1)));
  }

  protected virtual void ProcessSchedules(
    ServiceContractInq processor,
    FSContractSchedule fsScheduleRow,
    ServiceContractFilter Filter)
  {
    try
    {
      DateTime? fromDate = fsScheduleRow.NextExecutionDate ?? fsScheduleRow.StartDate;
      processor.processServiceContract(((PXSelectBase) processor.Filter).Cache, fsScheduleRow, fromDate, Filter.ToDate);
      PXProcessing<FSContractSchedule>.SetInfo("Record processed successfully.");
    }
    catch (Exception ex)
    {
      PXProcessing<FSContractSchedule>.SetError(ex);
    }
  }

  /// <summary>
  /// Process all Schedules (FSSchedule) in each Contract (FSContract).
  /// </summary>
  public virtual void processServiceContract(
    PXCache cache,
    FSContractSchedule fsScheduleRow,
    DateTime? fromDate,
    DateTime? toDate)
  {
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
    FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Select(new object[1]
    {
      (object) fsScheduleRow.EntityID
    }));
    if (fsServiceContract != null)
      fsScheduleRow.ContractDescr = fsServiceContract.DocDesc;
    this.GenerateAPPSOUpdateContracts(MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, (FSSchedule) fsScheduleRow, toDate, "NRSC"), "NRSC", fromDate, toDate, (FSSchedule) fsScheduleRow);
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void FSServiceContract_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void FSSchedule_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search3<FSSchedule.refNbr, OrderBy<Desc<FSSchedule.refNbr>>>))]
  protected virtual void FSContractSchedule_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Generated Service Order Date")]
  protected virtual void FSContractSchedule_LastGeneratedElementDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Schedule Location")]
  [PXDimensionSelector("LOCATION", typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSSchedule.customerID>>>>), typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  protected virtual void FSContractSchedule_CustomerLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceContract.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Contract Location", DirtyRead = true)]
  protected virtual void FSServiceContract_CustomerLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public override void openScheduleScreenBySchedules()
  {
    ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
    FSContractSchedule contractSchedule = PXResultset<FSContractSchedule>.op_Implicit(PXSelectBase<FSContractSchedule, PXSelect<FSContractSchedule, Where<FSContractSchedule.refNbr, Equal<Required<FSContractSchedule.refNbr>>, And<FSContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSContractSchedule.entityID, Equal<Required<FSContractSchedule.entityID>>, And<FSContractSchedule.customerID, Equal<Required<FSContractSchedule.customerID>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) ((PXSelectBase<FSContractSchedule>) this.ServiceContractSchedules).Current.RefNbr,
      (object) ((PXSelectBase<FSContractSchedule>) this.ServiceContractSchedules).Current.EntityID,
      (object) ((PXSelectBase<FSContractSchedule>) this.ServiceContractSchedules).Current.CustomerID
    }));
    ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openScheduleScreenByGenerationLogError()
  {
    ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
    FSContractSchedule contractSchedule = PXResultset<FSContractSchedule>.op_Implicit(PXSelectBase<FSContractSchedule, PXSelect<FSContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSGenerationLogError>) this.ErrorMessageRecords).Current.ScheduleID
    }));
    ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openServiceContractScreenBySchedules()
  {
    ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = FSServiceContract.PK.Find((PXGraph) this, ((PXSelectBase<FSContractSchedule>) this.ServiceContractSchedules).Current.EntityID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public override void openServiceContractScreenByGenerationLogError()
  {
    ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
    FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelectJoin<FSServiceContract, InnerJoin<FSSchedule, On<FSSchedule.entityID, Equal<FSServiceContract.serviceContractID>, And<FSSchedule.customerID, Equal<FSServiceContract.customerID>>>>, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSGenerationLogError>) this.ErrorMessageRecords).Current.ScheduleID
    }));
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
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new ServiceContractInq.\u003C\u003Ec__DisplayClass17_0()
    {
      graphGenerationLogError = PXGraph.CreateInstance<GenerationLogErrorMaint>(),
      bqlResultSet = PXSelectBase<FSGenerationLogError, PXSelect<FSGenerationLogError, Where<FSGenerationLogError.ignore, Equal<False>, And<FSGenerationLogError.processType, Equal<Required<FSGenerationLogError.processType>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) "NRSC"
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

  protected virtual void _(PX.Data.Events.RowSelected<ServiceContractFilter> e)
  {
    if (e.Row == null)
      return;
    ServiceContractFilter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ServiceContractFilter>>) e).Cache;
    bool warning = false;
    string str = SharedFunctions.WarnUserWithSchedulesWithoutNextExecution((PXGraph) this, cache, (PXAction) this.fixSchedulesWithoutNextExecutionDate, out warning);
    if (!warning)
      return;
    cache.RaiseExceptionHandling<StaffScheduleFilter.toDate>((object) row, (object) row.ToDate, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
  }
}
