// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.StaffContractScheduleEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public class StaffContractScheduleEntry : PXGraph<
#nullable disable
StaffContractScheduleEntry, FSStaffSchedule>
{
  public PXSelect<FSStaffSchedule, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Employee>>> StaffScheduleRecords;
  public PXSelect<FSStaffSchedule, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Employee>, And<FSSchedule.scheduleID, Equal<Current<FSSchedule.scheduleID>>>>> StaffScheduleSelected;
  public PXSelect<FSContractGenerationHistory, Where<FSContractGenerationHistory.scheduleID, Equal<Current<FSSchedule.scheduleID>>>> HistoryRecords;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXAction<FSStaffSchedule> openStaffContractScheduleProcess;
  public PXAction<FSStaffSchedule> rollbackScheduleGeneration;

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "End Date", DisplayNameTime = "End Time")]
  [PXDefault]
  [PXUIField]
  protected virtual void FSStaffSchedule_EndDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault]
  [PXFormula(typeof (Default<FSSchedule.branchID>))]
  [PXUIField(DisplayName = "Branch Location")]
  [FSSelectorBranchLocationByFSSchedule]
  protected virtual void FSStaffSchedule_BranchLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Description")]
  protected virtual void FSStaffSchedule_RecurrenceDescription_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenStaffContractScheduleProcess()
  {
    StaffContractScheduleProcess instance = PXGraph.CreateInstance<StaffContractScheduleProcess>();
    StaffScheduleFilter staffScheduleFilter1 = new StaffScheduleFilter();
    staffScheduleFilter1.ScheduleID = ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.ScheduleID;
    staffScheduleFilter1.BAccountID = ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.EmployeeID;
    if (((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.EndDate.HasValue)
    {
      StaffScheduleFilter staffScheduleFilter2 = staffScheduleFilter1;
      int year = ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.EndDate.Value.Year;
      DateTime dateTime = ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.EndDate.Value;
      int month = dateTime.Month;
      dateTime = ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.EndDate.Value;
      int day = dateTime.Day;
      DateTime? nullable = new DateTime?(new DateTime(year, month, day, 23, 59, 59));
      staffScheduleFilter2.ToDate = nullable;
    }
    else
    {
      DateTime? nullable1 = new DateTime?(((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Current.StartDate.Value.AddYears(1));
      StaffScheduleFilter staffScheduleFilter3 = staffScheduleFilter1;
      int year = nullable1.Value.Year;
      DateTime dateTime = nullable1.Value;
      int month = dateTime.Month;
      dateTime = nullable1.Value;
      int day = dateTime.Day;
      DateTime? nullable2 = new DateTime?(new DateTime(year, month, day, 23, 59, 59));
      staffScheduleFilter3.ToDate = nullable2;
    }
    ((PXSelectBase<StaffScheduleFilter>) instance.Filter).Insert(staffScheduleFilter1);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable RollbackScheduleGeneration(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    StaffContractScheduleEntry.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new StaffContractScheduleEntry.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.\u003C\u003E4__this = this;
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.list = new List<FSStaffSchedule>();
    foreach (FSStaffSchedule fsStaffSchedule in adapter.Get<FSStaffSchedule>())
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.list.Add(fsStaffSchedule);
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass120, __methodptr(\u003CRollbackScheduleGeneration\u003Eb__0)));
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSStaffSchedule.startTime> e)
  {
    DateTime? nullable;
    ref DateTime? local = ref nullable;
    DateTime now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int year = now.Year;
    now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int month = now.Month;
    now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int day = now.Day;
    now = DateTime.Now;
    int hour = now.Hour;
    now = DateTime.Now;
    int minute = now.Minute;
    now = DateTime.Now;
    int second = now.Second;
    DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
    local = new DateTime?(dateTime);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSStaffSchedule.startTime>, FSStaffSchedule, object>) e).NewValue = (object) nullable.Value;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSStaffSchedule.endTime> e)
  {
    DateTime? nullable;
    ref DateTime? local = ref nullable;
    DateTime now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int year = now.Year;
    now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int month = now.Month;
    now = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    int day = now.Day;
    now = DateTime.Now;
    int hour = now.Hour;
    now = DateTime.Now;
    int minute = now.Minute;
    now = DateTime.Now;
    int second = now.Second;
    DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
    local = new DateTime?(dateTime);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSStaffSchedule.endTime>, FSStaffSchedule, object>) e).NewValue = (object) nullable.Value.AddHours(1.0);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSSchedule.recurrenceDescription> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSStaffSchedule, FSSchedule.recurrenceDescription>, FSStaffSchedule, object>) e).NewValue = (object) ScheduleHelperGraph.GetRecurrenceDescription((FSSchedule) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSStaffSchedule, FSSchedule.employeeID> e)
  {
    if (e.Row == null)
      return;
    FSStaffSchedule row = e.Row;
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.parentBAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>, Where<EPEmployee.bAccountID, Equal<Required<FSSchedule.employeeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EmployeeID
    }));
    row.BranchID = (int?) branch?.BranchID;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    e.Row.SrvOrdTypeMessage = "This Service Order Type will be used for the recurring appointments";
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    FSStaffSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSStaffSchedule>>) e).Cache;
    bool? nullable = row.EnableExpirationDate;
    int num1;
    if (!nullable.HasValue)
    {
      num1 = 0;
    }
    else
    {
      nullable = row.EnableExpirationDate;
      num1 = nullable.Value ? 1 : 0;
    }
    bool flag1 = num1 != 0;
    this.SetControlsState(cache, (FSSchedule) row);
    PXCache pxCache1 = cache;
    FSStaffSchedule fsStaffSchedule1 = row;
    nullable = row.Monthly2Selected;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthly3Selected>(pxCache1, (object) fsStaffSchedule1, num2 != 0);
    PXCache pxCache2 = cache;
    FSStaffSchedule fsStaffSchedule2 = row;
    nullable = row.Monthly3Selected;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthly4Selected>(pxCache2, (object) fsStaffSchedule2, num3 != 0);
    PXUIFieldAttribute.SetEnabled<FSSchedule.endDate>(cache, (object) row, flag1 || ((PXGraph) this).IsCopyPasteContext);
    PXDefaultAttribute.SetPersistingCheck<FSSchedule.endDate>(cache, (object) row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool flag2 = SharedFunctions.ShowWarningScheduleNotProcessed(cache, (FSSchedule) e.Row);
    ((PXAction) this.openStaffContractScheduleProcess).SetEnabled(!flag2);
    PXAction<FSStaffSchedule> scheduleGeneration = this.rollbackScheduleGeneration;
    int num4;
    if (flag2)
    {
      int? scheduleId = e.Row.ScheduleID;
      int num5 = 0;
      num4 = scheduleId.GetValueOrDefault() > num5 & scheduleId.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    ((PXAction) scheduleGeneration).SetEnabled(num4 != 0);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSStaffSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSStaffSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSStaffSchedule> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSStaffSchedule>>) e).Cache.RaiseFieldDefaulting<FSSchedule.recurrenceDescription>((object) e.Row, ref obj);
    e.Row.RecurrenceDescription = (string) obj;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    if (PXResultset<FSTimeSlot>.op_Implicit(PXSelectBase<FSTimeSlot, PXViewOf<FSTimeSlot>.BasedOn<SelectFromBase<FSTimeSlot, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSTimeSlot.scheduleID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) e.Row.ScheduleID
    })) != null)
      throw new PXException("The staff schedule rule cannot be deleted because the time slots have been generated for it. To be able to delete the staff schedule rule, remove the time slots by using the Roll Back Schedule Generation command.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    PXUpdate<Set<FSTimeSlot.scheduleID, Required<FSTimeSlot.scheduleID>>, FSTimeSlot, Where<FSTimeSlot.scheduleID, Equal<Required<FSTimeSlot.scheduleID>>>>.Update((PXGraph) this, new object[2]
    {
      null,
      (object) e.Row.ScheduleID
    });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSStaffSchedule> e)
  {
    if (e.Row == null)
      return;
    FSStaffSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSStaffSchedule>>) e).Cache;
    bool? enableExpirationDate = row.EnableExpirationDate;
    bool flag = false;
    if (enableExpirationDate.GetValueOrDefault() == flag & enableExpirationDate.HasValue)
    {
      DateTime? nullable1 = row.EndDate;
      if (nullable1.HasValue)
      {
        FSStaffSchedule fsStaffSchedule = row;
        nullable1 = new DateTime?();
        DateTime? nullable2 = nullable1;
        fsStaffSchedule.EndDate = nullable2;
      }
    }
    if (e.Operation != 2 && e.Operation != 1)
      return;
    row.NextExecutionDate = SharedFunctions.GetNextExecution(cache, (FSSchedule) row);
    this.CheckDates(cache, row);
    this.CheckTimes(cache, row);
    if (e.Operation != 2 || ((PXGraph) this).IsImport)
      return;
    ((PXSelectBase<FSStaffSchedule>) this.StaffScheduleRecords).Ask("Service Management", "This schedule will not affect the system until a generation process takes place.", (MessageButtons) 0, (MessageIcon) 3);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSStaffSchedule> e)
  {
  }

  /// <summary>Check if Start Date is prior to End Date.</summary>
  /// <param name="cache">FSStaffSchedule cache.</param>
  /// <param name="fsStaffScheduleRow">FSStaffSchedule Row.</param>
  public virtual void CheckDates(PXCache cache, FSStaffSchedule fsStaffScheduleRow)
  {
    if (!fsStaffScheduleRow.StartDate.HasValue || !fsStaffScheduleRow.EndDate.HasValue)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    DateTime? nullable = fsStaffScheduleRow.StartDate;
    DateTime dateTime1 = nullable.Value;
    nullable = fsStaffScheduleRow.EndDate;
    DateTime dateTime2 = nullable.Value;
    if (dateTime1.Year > dateTime2.Year || dateTime1.Year == dateTime2.Year && dateTime1.Month > dateTime2.Month || dateTime1.Year == dateTime2.Year && dateTime1.Month == dateTime2.Month && dateTime1.Day > dateTime2.Day)
      propertyException = new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date.", (PXErrorLevel) 5);
    cache.RaiseExceptionHandling<FSSchedule.startDate>((object) fsStaffScheduleRow, (object) dateTime1, (Exception) propertyException);
    cache.RaiseExceptionHandling<FSSchedule.endDate>((object) fsStaffScheduleRow, (object) dateTime2, (Exception) propertyException);
  }

  /// <summary>Check if Start Time is prior to End Time.</summary>
  /// <param name="cache">FSStaffSchedule cache.</param>
  /// <param name="fsStaffScheduleRow">FSStaffSchedule Row.</param>
  public virtual void CheckTimes(PXCache cache, FSStaffSchedule fsStaffScheduleRow)
  {
    if (!fsStaffScheduleRow.StartTime.HasValue || !fsStaffScheduleRow.EndTime.HasValue)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    DateTime? nullable1 = fsStaffScheduleRow.StartTime;
    DateTime dateTime1 = nullable1.Value;
    nullable1 = fsStaffScheduleRow.EndTime;
    DateTime dateTime2 = nullable1.Value;
    if (dateTime1 > dateTime2)
      propertyException = new PXSetPropertyException("The times are invalid. The end time cannot be earlier than the start time.", (PXErrorLevel) 5);
    cache.RaiseExceptionHandling<FSStaffSchedule.startTime>((object) fsStaffScheduleRow, (object) dateTime1, (Exception) propertyException);
    cache.RaiseExceptionHandling<FSStaffSchedule.endTime>((object) fsStaffScheduleRow, (object) dateTime2, (Exception) propertyException);
    nullable1 = fsStaffScheduleRow.StartDate;
    if (!nullable1.HasValue)
      return;
    nullable1 = fsStaffScheduleRow.EndDate;
    if (!nullable1.HasValue)
      return;
    FSStaffSchedule fsStaffSchedule1 = fsStaffScheduleRow;
    nullable1 = fsStaffScheduleRow.StartDate;
    int year1 = nullable1.Value.Year;
    nullable1 = fsStaffScheduleRow.StartDate;
    int month1 = nullable1.Value.Month;
    nullable1 = fsStaffScheduleRow.StartDate;
    DateTime dateTime3 = nullable1.Value;
    int day1 = dateTime3.Day;
    nullable1 = fsStaffScheduleRow.StartTime;
    dateTime3 = nullable1.Value;
    int hour1 = dateTime3.Hour;
    nullable1 = fsStaffScheduleRow.StartTime;
    dateTime3 = nullable1.Value;
    int minute1 = dateTime3.Minute;
    nullable1 = fsStaffScheduleRow.StartTime;
    dateTime3 = nullable1.Value;
    int second1 = dateTime3.Second;
    DateTime? nullable2 = new DateTime?(new DateTime(year1, month1, day1, hour1, minute1, second1));
    fsStaffSchedule1.StartDate = nullable2;
    FSStaffSchedule fsStaffSchedule2 = fsStaffScheduleRow;
    nullable1 = fsStaffScheduleRow.EndDate;
    dateTime3 = nullable1.Value;
    int year2 = dateTime3.Year;
    nullable1 = fsStaffScheduleRow.EndDate;
    dateTime3 = nullable1.Value;
    int month2 = dateTime3.Month;
    nullable1 = fsStaffScheduleRow.EndDate;
    dateTime3 = nullable1.Value;
    int day2 = dateTime3.Day;
    nullable1 = fsStaffScheduleRow.EndTime;
    dateTime3 = nullable1.Value;
    int hour2 = dateTime3.Hour;
    nullable1 = fsStaffScheduleRow.EndTime;
    dateTime3 = nullable1.Value;
    int minute2 = dateTime3.Minute;
    nullable1 = fsStaffScheduleRow.EndTime;
    dateTime3 = nullable1.Value;
    int second2 = dateTime3.Second;
    DateTime? nullable3 = new DateTime?(new DateTime(year2, month2, day2, hour2, minute2, second2));
    fsStaffSchedule2.EndDate = nullable3;
  }

  /// <summary>
  /// Makes visible the group that corresponds to the selected FrequencyType.
  /// </summary>
  public virtual void SetControlsState(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag1 = fsScheduleRow.FrequencyType == "W";
    bool flag2 = fsScheduleRow.FrequencyType == "D";
    bool flag3 = fsScheduleRow.FrequencyType == "M";
    bool flag4 = fsScheduleRow.FrequencyType == "A";
    bool valueOrDefault1 = fsScheduleRow.Monthly2Selected.GetValueOrDefault();
    bool valueOrDefault2 = fsScheduleRow.Monthly3Selected.GetValueOrDefault();
    bool valueOrDefault3 = fsScheduleRow.Monthly4Selected.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<FSSchedule.dailyFrequency>(cache, (object) fsScheduleRow, flag2);
    PXUIFieldAttribute.SetVisible<FSSchedule.dailyLabel>(cache, (object) fsScheduleRow, flag2);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyFrequency>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnSun>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnMon>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnTue>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnWed>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnThu>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnFri>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyOnSat>(cache, (object) fsScheduleRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSchedule.weeklyLabel>(cache, (object) fsScheduleRow, flag1);
    this.SetMonthlyControlsState(cache, fsScheduleRow);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyFrequency>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyLabel>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly2Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly3Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthly4Selected>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType1>(cache, (object) fsScheduleRow, flag3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyRecurrenceType4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3);
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDay4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnWeek4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek1>(cache, (object) fsScheduleRow, flag3 && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek2>(cache, (object) fsScheduleRow, flag3 & valueOrDefault1 && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek3>(cache, (object) fsScheduleRow, flag3 & valueOrDefault2 && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.monthlyOnDayOfWeek4>(cache, (object) fsScheduleRow, flag3 & valueOrDefault3 && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualFrequency>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.yearlyLabel>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualRecurrenceType>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJan>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnFeb>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnMar>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnApr>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnMay>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJun>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnJul>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnAug>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnSep>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnOct>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnNov>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDec>(cache, (object) fsScheduleRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDay>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "D");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetVisible<FSSchedule.annualOnDayOfWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnDay>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.annualOnDayOfWeek>(cache, (object) fsScheduleRow, flag4 && fsScheduleRow.AnnualRecurrenceType == "W");
  }

  public virtual void SetMonthlyControlsState(PXCache cache, FSSchedule fsScheduleRow)
  {
    bool flag = fsScheduleRow.FrequencyType == "M";
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek1>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType1 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek2>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType2 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek3>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType3 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDay4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "D");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnWeek4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "W");
    PXUIFieldAttribute.SetEnabled<FSSchedule.monthlyOnDayOfWeek4>(cache, (object) fsScheduleRow, flag && fsScheduleRow.MonthlyRecurrenceType4 == "W");
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    StaffContractScheduleEntry.endDate>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    StaffContractScheduleEntry.branchLocationID>
  {
  }
}
