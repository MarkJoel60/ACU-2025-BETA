// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.StaffContractScheduleProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.FS.Scheduler;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class StaffContractScheduleProcess : PXGraph<StaffContractScheduleProcess>
{
  public int? nextGenerationID;
  public PXSelect<PX.Objects.CR.BAccount> BAccountRecords;
  public PXFilter<StaffScheduleFilter> Filter;
  public PXCancel<StaffScheduleFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (StaffScheduleFilter))]
  public PXFilteredProcessingJoin<FSStaffSchedule, StaffScheduleFilter, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSSchedule.employeeID>, And<PX.Objects.CR.BAccount.vStatus, NotEqual<VendorStatus.inactive>>>>, Where<FSSchedule.active, Equal<True>, And2<Where<FSSchedule.nextExecutionDate, LessEqual<Current<StaffScheduleFilter.toDate>>, And<Where<FSSchedule.enableExpirationDate, Equal<False>, Or<FSSchedule.endDate, Greater<FSSchedule.nextExecutionDate>>>>>, And2<Where<Current<StaffScheduleFilter.bAccountID>, IsNull, Or<FSSchedule.employeeID, Equal<Current<StaffScheduleFilter.bAccountID>>>>, And2<Where<Current<StaffScheduleFilter.branchID>, IsNull, Or<FSSchedule.branchID, Equal<Current<StaffScheduleFilter.branchID>>>>, And2<Where<Current<StaffScheduleFilter.branchLocationID>, IsNull, Or<FSSchedule.branchLocationID, Equal<Current<StaffScheduleFilter.branchLocationID>>>>, And2<Where<Current<StaffScheduleFilter.scheduleID>, IsNull, Or<FSSchedule.scheduleID, Equal<Current<StaffScheduleFilter.scheduleID>>>>, And<FSSchedule.startDate, LessEqual<Current<StaffScheduleFilter.toDate>>>>>>>>>> StaffSchedules;
  public PXSelectGroupBy<FSContractGenerationHistory, Where<FSContractGenerationHistory.recordType, Equal<ListField_RecordType_ContractSchedule.EmployeeScheduleContract>>, Aggregate<GroupBy<FSContractGenerationHistory.generationID>>, OrderBy<Desc<FSContractGenerationHistory.generationID>>> ContractHistoryRecords;
  public PXAction<StaffScheduleFilter> fixSchedulesWithoutNextExecutionDate;

  public StaffContractScheduleProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    StaffContractScheduleProcess.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new StaffContractScheduleProcess.\u003C\u003Ec__DisplayClass1_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.processor = (StaffContractScheduleProcess) null;
    // ISSUE: method pointer
    ((PXProcessingBase<FSStaffSchedule>) this.StaffSchedules).SetProcessDelegate(new PXProcessingBase<FSStaffSchedule>.ProcessListDelegate((object) cDisplayClass10, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  /// <summary>
  /// Process all FSStaffSchedule. Generates one or more TimeSlot in the Scheduler Module.
  /// </summary>
  protected virtual void processStaffSchedule(
    PXCache cache,
    StaffContractScheduleProcess staffContractScheduleEnq,
    FSStaffSchedule fsScheduleRow,
    DateTime? fromDate,
    DateTime? toDate)
  {
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
    this.generateTimeSlotAndUpdateStaffSchedule(MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, (FSSchedule) fsScheduleRow, toDate, "EPSC"), "EPSC", fromDate, toDate, (FSSchedule) fsScheduleRow);
  }

  /// <summary>
  /// Generates an FSTimeSlot for each TimeSlot in the [scheduleRules] List.
  /// </summary>
  protected virtual void generateTimeSlotAndUpdateStaffSchedule(
    List<PX.Objects.FS.Scheduler.Schedule> scheduleRules,
    string recordType,
    DateTime? fromDate,
    DateTime? toDate,
    FSSchedule fsScheduleRow)
  {
    TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
    DateTime lastProcessDate = this.getProcessEndDate(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0), toDate).Value;
    Period period1 = new Period(fromDate.Value, new DateTime?(toDate.Value));
    if (!this.nextGenerationID.HasValue)
    {
      FSProcessIdentity fsProcessIdentity = new FSProcessIdentity()
      {
        ProcessType = recordType,
        FilterFromTo = fromDate,
        FilterUpTo = toDate
      };
      ProcessIdentityMaint instance = PXGraph.CreateInstance<ProcessIdentityMaint>();
      ((PXSelectBase<FSProcessIdentity>) instance.processIdentityRecords).Insert(fsProcessIdentity);
      ((PXAction) instance.Save).Press();
      this.nextGenerationID = ((PXSelectBase<FSProcessIdentity>) instance.processIdentityRecords).Current.ProcessID;
    }
    Period period2 = period1;
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = scheduleRules;
    int? nextGenerationId = this.nextGenerationID;
    List<TimeSlot> calendar = timeSlotGenerator.GenerateCalendar(period2, (IEnumerable<PX.Objects.FS.Scheduler.Schedule>) scheduleList, nextGenerationId);
    DateTime? nullable1 = new DateTime?();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        foreach (TimeSlot timeSlot in calendar)
        {
          nullable1 = new DateTime?(timeSlot.DateTimeBegin);
          this.createTimeSlot(timeSlot);
        }
        DateTime? nullable2 = new DateTime?();
        if (calendar.Count > 0)
          nullable2 = new DateTime?(calendar.Max<TimeSlot, DateTime>((Func<TimeSlot, DateTime>) (a => a.DateTimeBegin)).Date);
        this.createContractGenerationHistory(this.nextGenerationID.Value, scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID, lastProcessDate, nullable2, recordType);
        this.updateGeneratedSchedule(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID, new DateTime?(lastProcessDate), nullable2, fsScheduleRow);
      }
      catch (PXException ex)
      {
        FSGenerationLogError generationLogError = new FSGenerationLogError()
        {
          ProcessType = recordType,
          ErrorMessage = ((Exception) ex).Message,
          ScheduleID = new int?(scheduleRules.ElementAt<PX.Objects.FS.Scheduler.Schedule>(0).ScheduleID),
          GenerationID = this.nextGenerationID,
          ErrorDate = nullable1
        };
        transactionScope.Dispose();
        GenerationLogErrorMaint instance = PXGraph.CreateInstance<GenerationLogErrorMaint>();
        ((PXSelectBase<FSGenerationLogError>) instance.LogErrorMessageRecords).Insert(generationLogError);
        ((PXAction) instance.Save).Press();
        throw new PXException(((Exception) ex).Message);
      }
      transactionScope.Complete((PXGraph) this);
    }
  }

  /// <summary>Create an FSTimeSlot from a TimeSlot.</summary>
  protected virtual void createTimeSlot(TimeSlot timeSlot)
  {
    TimeSlotMaint instance = PXGraph.CreateInstance<TimeSlotMaint>();
    FSStaffSchedule fsStaffSchedule = PXResultset<FSStaffSchedule>.op_Implicit(PXSelectBase<FSStaffSchedule, PXSelect<FSStaffSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) timeSlot.ScheduleID
    }));
    FSTimeSlot fsTimeSlot = new FSTimeSlot();
    fsTimeSlot.BranchID = fsStaffSchedule.BranchID;
    fsTimeSlot.BranchLocationID = fsStaffSchedule.BranchLocationID;
    fsTimeSlot.EmployeeID = fsStaffSchedule.EmployeeID;
    DateTime dateTime1;
    ref DateTime local1 = ref dateTime1;
    DateTime dateTimeBegin = timeSlot.DateTimeBegin;
    int year1 = dateTimeBegin.Year;
    dateTimeBegin = timeSlot.DateTimeBegin;
    int month1 = dateTimeBegin.Month;
    dateTimeBegin = timeSlot.DateTimeBegin;
    int day1 = dateTimeBegin.Day;
    dateTimeBegin = fsStaffSchedule.StartTime.Value;
    int hour1 = dateTimeBegin.Hour;
    dateTimeBegin = fsStaffSchedule.StartTime.Value;
    int minute1 = dateTimeBegin.Minute;
    dateTimeBegin = fsStaffSchedule.StartTime.Value;
    int second1 = dateTimeBegin.Second;
    local1 = new DateTime(year1, month1, day1, hour1, minute1, second1);
    DateTime dateTime2;
    ref DateTime local2 = ref dateTime2;
    DateTime dateTimeEnd = timeSlot.DateTimeEnd;
    int year2 = dateTimeEnd.Year;
    dateTimeEnd = timeSlot.DateTimeEnd;
    int month2 = dateTimeEnd.Month;
    dateTimeEnd = timeSlot.DateTimeEnd;
    int day2 = dateTimeEnd.Day;
    dateTimeEnd = fsStaffSchedule.EndTime.Value;
    int hour2 = dateTimeEnd.Hour;
    dateTimeEnd = fsStaffSchedule.EndTime.Value;
    int minute2 = dateTimeEnd.Minute;
    dateTimeEnd = fsStaffSchedule.EndTime.Value;
    int second2 = dateTimeEnd.Second;
    local2 = new DateTime(year2, month2, day2, hour2, minute2, second2);
    TimeSpan timeSpan = dateTime2 - dateTime1;
    fsTimeSlot.TimeStart = new DateTime?(dateTime1);
    fsTimeSlot.TimeEnd = new DateTime?(dateTime2);
    fsTimeSlot.RecordCount = new int?(1);
    fsTimeSlot.ScheduleType = fsStaffSchedule.ScheduleType;
    fsTimeSlot.TimeDiff = new Decimal?((Decimal) timeSpan.TotalMinutes);
    fsTimeSlot.ScheduleID = new int?(timeSlot.ScheduleID);
    fsTimeSlot.GenerationID = timeSlot.GenerationID;
    ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Insert(fsTimeSlot);
    ((PXAction) instance.Save).Press();
  }

  /// <summary>Create a ContractGenerationHistory.</summary>
  protected virtual void createContractGenerationHistory(
    int nextProcessID,
    int scheduleID,
    DateTime lastProcessDate,
    DateTime? lastGeneratedTimeSlotDate,
    string recordType)
  {
    ContractGenerationHistoryMaint instance = PXGraph.CreateInstance<ContractGenerationHistoryMaint>();
    FSContractGenerationHistory generationHistory = new FSContractGenerationHistory()
    {
      GenerationID = new int?(nextProcessID),
      ScheduleID = new int?(scheduleID),
      LastProcessedDate = new DateTime?(lastProcessDate),
      LastGeneratedElementDate = lastGeneratedTimeSlotDate,
      EntityType = "E",
      RecordType = recordType
    };
    FSContractGenerationHistory historyRowBySchedule = this.getLastGenerationHistoryRowBySchedule(scheduleID);
    if (historyRowBySchedule != null && historyRowBySchedule.ContractGenerationHistoryID.HasValue && historyRowBySchedule != null)
    {
      generationHistory.PreviousGeneratedElementDate = historyRowBySchedule.LastGeneratedElementDate;
      generationHistory.PreviousProcessedDate = historyRowBySchedule.LastProcessedDate;
      if (!lastGeneratedTimeSlotDate.HasValue)
        generationHistory.LastGeneratedElementDate = generationHistory.PreviousGeneratedElementDate;
    }
    ((PXSelectBase<FSContractGenerationHistory>) instance.ContractGenerationHistoryRecords).Insert(generationHistory);
    ((PXAction) instance.Save).Press();
  }

  /// <summary>
  /// Update an Schedule (lastGeneratedTimeSlotBySchedules and lastProcessedDate).
  /// </summary>
  protected virtual void updateGeneratedSchedule(
    int scheduleID,
    DateTime? toDate,
    DateTime? lastGeneratedTimeSlotBySchedules,
    FSSchedule fsScheduleRow)
  {
    if (PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) scheduleID
    })) == null || !lastGeneratedTimeSlotBySchedules.HasValue)
      return;
    if (fsScheduleRow != null)
    {
      fsScheduleRow.LastGeneratedElementDate = lastGeneratedTimeSlotBySchedules;
      fsScheduleRow.NextExecutionDate = SharedFunctions.GetNextExecution(((PXSelectBase) this.StaffSchedules).Cache, fsScheduleRow);
    }
    PXUpdate<Set<FSSchedule.lastGeneratedElementDate, Required<FSSchedule.lastGeneratedElementDate>, Set<FSSchedule.nextExecutionDate, Required<FSSchedule.nextExecutionDate>>>, FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Update((PXGraph) this, new object[3]
    {
      (object) lastGeneratedTimeSlotBySchedules,
      (object) fsScheduleRow.NextExecutionDate,
      (object) scheduleID
    });
  }

  /// <summary>Return the last FSContractGenerationHistory.</summary>
  protected virtual FSContractGenerationHistory getLastGenerationHistoryRow(string recordType)
  {
    FSContractGenerationHistory generationHistory = PXResultset<FSContractGenerationHistory>.op_Implicit(PXSelectBase<FSContractGenerationHistory, PXSelectGroupBy<FSContractGenerationHistory, Where<FSContractGenerationHistory.recordType, Equal<Required<FSContractGenerationHistory.recordType>>>, Aggregate<Max<FSContractGenerationHistory.generationID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) recordType
    }));
    return generationHistory != null && generationHistory.GenerationID.HasValue ? generationHistory : (FSContractGenerationHistory) null;
  }

  /// <summary>
  /// Return the last FSContractGenerationHistory by Schedule.
  /// </summary>
  protected virtual FSContractGenerationHistory getLastGenerationHistoryRowBySchedule(int scheduleID)
  {
    FSContractGenerationHistory generationHistory = PXResultset<FSContractGenerationHistory>.op_Implicit(PXSelectBase<FSContractGenerationHistory, PXSelectGroupBy<FSContractGenerationHistory, Where<FSContractGenerationHistory.scheduleID, Equal<Required<FSContractGenerationHistory.scheduleID>>>, Aggregate<Max<FSContractGenerationHistory.generationID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scheduleID
    }));
    return generationHistory != null && generationHistory.GenerationID.HasValue ? generationHistory : (FSContractGenerationHistory) null;
  }

  /// <summary>
  /// Return the smallest date between schedule EndDate and Process EndDate.
  /// </summary>
  protected virtual DateTime? getProcessEndDate(PX.Objects.FS.Scheduler.Schedule fsScheduleRule, DateTime? toDate)
  {
    FSSchedule fsSchedule = PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsScheduleRule.ScheduleID
    }));
    bool? enableExpirationDate = fsSchedule.EnableExpirationDate;
    bool flag = false;
    if (enableExpirationDate.GetValueOrDefault() == flag & enableExpirationDate.HasValue)
      return new DateTime?(toDate.Value);
    DateTime? endDate = fsSchedule.EndDate;
    DateTime? nullable = toDate;
    return (endDate.HasValue & nullable.HasValue ? (endDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? new DateTime?(toDate.Value) : fsSchedule.EndDate;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Employee Name", Enabled = false)]
  protected virtual void BAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Generated Schedule")]
  protected virtual void FSStaffSchedule_LastGeneratedElementDate_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Fix Schedules Without Next Execution Date", Visible = false)]
  public virtual IEnumerable FixSchedulesWithoutNextExecutionDate(PXAdapter adapter)
  {
    SharedFunctions.UpdateSchedulesWithoutNextExecution((PXGraph) this, ((PXSelectBase) this.Filter).Cache);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<StaffScheduleFilter> e)
  {
    if (e.Row == null)
      return;
    bool warning = false;
    StaffScheduleFilter row = e.Row;
    string str = SharedFunctions.WarnUserWithSchedulesWithoutNextExecution((PXGraph) this, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<StaffScheduleFilter>>) e).Cache, (PXAction) this.fixSchedulesWithoutNextExecutionDate, out warning);
    if (!warning)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<StaffScheduleFilter>>) e).Cache.RaiseExceptionHandling<StaffScheduleFilter.toDate>((object) row, (object) row.ToDate, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
  }
}
