// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleMaintBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Represents the base BLC for module-specific Recurring Transactions controllers.
/// </summary>
/// <typeparam name="TGraph">The specific type of the derived graph.</typeparam>
/// <typeparam name="TProcessGraph">The type of the graph used for the documents generation.</typeparam>
public class ScheduleMaintBase<TGraph, TProcessGraph> : PXGraph<TGraph, Schedule>
  where TGraph : PXGraph
  where TProcessGraph : PXGraph<TProcessGraph>, IScheduleProcessing, new()
{
  public PXSelect<Schedule> Schedule_Header;
  public PXAction<Schedule> RunNow;

  protected virtual IEnumerable schedule_Header()
  {
    return GraphHelper.QuickSelect(new PXView((PXGraph) this, false, ((PXSelectBase) this.Schedule_Header).View.BqlSelect));
  }

  /// <summary>Starts document generation according to the schedule.</summary>
  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable runNow(PXAdapter adapter)
  {
    ScheduleMaintBase<TGraph, TProcessGraph> scheduleMaintBase = this;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ScheduleMaintBase<TGraph, TProcessGraph>.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new ScheduleMaintBase<TGraph, TProcessGraph>.\u003C\u003Ec__DisplayClass3_0();
    ((PXAction) scheduleMaintBase.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.schedule = ((PXSelectBase<Schedule>) scheduleMaintBase.Schedule_Header).Current;
    // ISSUE: reference to a compiler-generated field
    DateTime? nextRunDate = cDisplayClass30.schedule.NextRunDate;
    DateTime? businessDate = ((PXGraph) scheduleMaintBase).Accessinfo.BusinessDate;
    if ((nextRunDate.HasValue & businessDate.HasValue ? (nextRunDate.GetValueOrDefault() > businessDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("The next generation date for this task is greater than the current business date.");
    // ISSUE: reference to a compiler-generated field
    bool? noRunLimit = cDisplayClass30.schedule.NoRunLimit;
    bool flag = false;
    if (noRunLimit.GetValueOrDefault() == flag & noRunLimit.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      short? runCntr = cDisplayClass30.schedule.RunCntr;
      int? nullable1 = runCntr.HasValue ? new int?((int) runCntr.GetValueOrDefault()) : new int?();
      // ISSUE: reference to a compiler-generated field
      short? runLimit = cDisplayClass30.schedule.RunLimit;
      int? nullable2 = runLimit.HasValue ? new int?((int) runLimit.GetValueOrDefault()) : new int?();
      if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        throw new PXException("The task reached the configured limit and will not be processed. Please change the task limit or deactivate it.");
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) scheduleMaintBase, new PXToggleAsyncDelegate((object) cDisplayClass30, __methodptr(\u003CrunNow\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    yield return (object) cDisplayClass30.schedule;
  }

  public virtual bool CanClipboardCopyPaste() => false;

  /// <summary>Identifies the module of the deriving schedule graph.</summary>
  protected virtual string Module => "GL";

  /// <summary>
  /// Returns a value indicating whether the current schedule
  /// contains any details.
  /// </summary>
  internal virtual bool AnyScheduleDetails() => false;

  protected virtual void Schedule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.SetControlsState(cache, (Schedule) e.Row);
  }

  protected virtual void Schedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    Schedule row = (Schedule) e.Row;
    bool flag = !PXUIFieldAttribute.GetErrors(sender, (object) row, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Any<KeyValuePair<string, string>>();
    if (!((e.Operation & 3) != 3 & flag))
      return;
    row.NextRunDate = new Scheduler((PXGraph) this).GetNextRunDate(row);
  }

  protected virtual void Schedule_Module_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Module;
  }

  protected virtual void Schedule_NoEndDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if ((bool) e.OldValue || ((Schedule) e.Row).NoEndDate.Value == (bool) e.OldValue)
      return;
    ((Schedule) e.Row).EndDate = new DateTime?();
  }

  protected virtual void Schedule_NoRunLimit_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (((Schedule) e.Row).NoRunLimit.Value == (bool) e.OldValue)
      return;
    Schedule row = (Schedule) e.Row;
    if ((bool) e.OldValue)
      row.RunLimit = new short?((short) 1);
    else
      row.RunLimit = new short?((short) 0);
  }

  protected virtual void Schedule_ScheduleType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Schedule row = (Schedule) e.Row;
    if (row.NextRunDate.HasValue && !object.Equals(e.OldValue, (object) row.ScheduleType))
      throw new PXException("Schedule Type cannot be changed for the processed Schedule.");
  }

  protected virtual void SetControlsState(PXCache cache, Schedule s)
  {
    bool isDaily = s.ScheduleType == "D";
    bool isWeekly = s.ScheduleType == "W";
    bool isMonthly = s.ScheduleType == "M";
    bool isPeriodically = s.ScheduleType == "P";
    bool flag = !s.LastRunDate.HasValue;
    this.SetDailyControlsState(cache, s, isDaily);
    this.SetWeeklyControlsState(cache, s, isWeekly);
    this.SetMonthlyControlsState(cache, s, isMonthly);
    this.SetPeriodicallyControlsState(cache, s, isPeriodically);
    PXUIFieldAttribute.SetEnabled<Schedule.runLimit>(cache, (object) s, !s.NoRunLimit.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<Schedule.endDate>(cache, (object) s, !s.NoEndDate.GetValueOrDefault());
    PXDefaultAttribute.SetPersistingCheck<Schedule.endDate>(cache, (object) s, s.NoEndDate.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
    cache.AllowDelete = flag;
    ((PXAction) this.RunNow).SetEnabled(cache.GetStatus((object) s) != 2 && s.Active.GetValueOrDefault() && this.AnyScheduleDetails());
  }

  private void SetDailyControlsState(PXCache cache, Schedule s, bool isDaily)
  {
    PXUIFieldAttribute.SetVisible<Schedule.dailyFrequency>(cache, (object) s, isDaily);
    PXUIFieldAttribute.SetVisible<Schedule.days>(cache, (object) s, isDaily);
  }

  private void SetWeeklyControlsState(PXCache cache, Schedule s, bool isWeekly)
  {
    PXUIFieldAttribute.SetVisible<Schedule.weeklyFrequency>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay1>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay2>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay3>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay4>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay5>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay6>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay7>(cache, (object) s, isWeekly);
    PXUIFieldAttribute.SetVisible<Schedule.weeks>(cache, (object) s, isWeekly);
  }

  private void SetMonthlyControlsState(PXCache cache, Schedule s, bool isMonthly)
  {
    PXUIFieldAttribute.SetVisible<Schedule.monthlyFrequency>(cache, (object) s, isMonthly);
    PXUIFieldAttribute.SetVisible<Schedule.monthlyDaySel>(cache, (object) s, isMonthly);
    PXUIFieldAttribute.SetVisible<Schedule.monthlyOnDay>(cache, (object) s, isMonthly && s.MonthlyDaySel == "D");
    PXUIFieldAttribute.SetVisible<Schedule.monthlyOnWeek>(cache, (object) s, isMonthly && s.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetVisible<Schedule.monthlyOnDayOfWeek>(cache, (object) s, isMonthly && s.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetVisible<Schedule.months>(cache, (object) s, isMonthly);
  }

  private void SetPeriodicallyControlsState(PXCache cache, Schedule s, bool isPeriodically)
  {
    PXUIFieldAttribute.SetVisible<Schedule.periodFrequency>(cache, (object) s, isPeriodically);
    PXUIFieldAttribute.SetVisible<Schedule.periodDateSel>(cache, (object) s, isPeriodically);
    PXUIFieldAttribute.SetVisible<Schedule.periodFixedDay>(cache, (object) s, isPeriodically);
    PXUIFieldAttribute.SetVisible<Schedule.periods>(cache, (object) s, isPeriodically);
    PXUIFieldAttribute.SetVisible<Schedule.periodFixedDay>(cache, (object) s, isPeriodically && s.PeriodDateSel == "D");
  }
}
