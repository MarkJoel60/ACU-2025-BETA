// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.WZ;

public class WZScheduleMaint : PXGraph<WZScheduleMaint, Schedule>
{
  public PXSelect<Schedule, Where<Schedule.module, Equal<BatchModule.moduleWZ>>> Schedule_Header;
  public PXSelect<WZScenario, Where<WZScenario.scheduled, Equal<True>, And<WZScenario.scheduleID, Equal<Current<Schedule.scheduleID>>>>> Scenarios;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<Schedule> RunNow;

  public WZScheduleMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  public virtual bool CanClipboardCopyPaste() => false;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (WZScenario.scenarioID), SubstituteKey = typeof (WZScenario.name))]
  protected virtual void WZScenario_ScenarioID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (Schedule.scheduleID))]
  [PXParent(typeof (Select<Schedule, Where<Schedule.scheduleID, Equal<Current<WZScenario.scheduleID>>>>), LeaveChildren = true)]
  protected virtual void WZScenario_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (PX.Objects.GL.GLSetup.scheduleNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search<Schedule.scheduleID, Where<Schedule.module, Equal<BatchModule.moduleWZ>>>))]
  [PXDefault]
  protected virtual void Schedule_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Schedule_Module_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "WZ";
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

  protected virtual void Schedule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.SetControlsState(cache, (Schedule) e.Row);
  }

  protected virtual void Schedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    Schedule row = (Schedule) e.Row;
    bool flag1 = true;
    short? nullable1 = row.RunLimit;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    bool? nullable3;
    if (nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue)
    {
      nullable3 = row.NoRunLimit;
      if (!nullable3.Value)
      {
        sender.RaiseExceptionHandling<Schedule.runLimit>((object) row, (object) row.RunLimit, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        }));
        flag1 = false;
      }
    }
    if (row.ScheduleType == "D")
    {
      nullable1 = row.DailyFrequency;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<Schedule.dailyFrequency>((object) row, (object) row.DailyFrequency, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        }));
        flag1 = false;
      }
    }
    if (row.ScheduleType == "W")
    {
      nullable1 = row.WeeklyFrequency;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num3 = 0;
      if (nullable2.GetValueOrDefault() <= num3 & nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<Schedule.weeklyFrequency>((object) row, (object) row.WeeklyFrequency, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        }));
        flag1 = false;
      }
    }
    if (row.ScheduleType == "P")
    {
      nullable1 = row.PeriodFrequency;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num4 = 0;
      if (nullable2.GetValueOrDefault() <= num4 & nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<Schedule.periodFrequency>((object) row, (object) row.PeriodFrequency, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        }));
        flag1 = false;
      }
    }
    if (row.ScheduleType == "P" && row.PeriodDateSel == "D")
    {
      nullable1 = row.PeriodFixedDay;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num5 = 0;
      if (nullable2.GetValueOrDefault() <= num5 & nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<Schedule.periodFixedDay>((object) row, (object) row.PeriodFixedDay, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        }));
        flag1 = false;
      }
    }
    if (!row.EndDate.HasValue)
    {
      nullable3 = row.NoEndDate;
      bool flag2 = false;
      if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue)
      {
        sender.RaiseExceptionHandling<Schedule.endDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' may not be empty.", new object[1]
        {
          (object) "[endDate]"
        }));
        flag1 = false;
      }
    }
    nullable3 = row.WeeklyOnDay1;
    if (!nullable3.GetValueOrDefault())
    {
      nullable3 = row.WeeklyOnDay2;
      if (!nullable3.GetValueOrDefault())
      {
        nullable3 = row.WeeklyOnDay3;
        if (!nullable3.GetValueOrDefault())
        {
          nullable3 = row.WeeklyOnDay4;
          if (!nullable3.GetValueOrDefault())
          {
            nullable3 = row.WeeklyOnDay5;
            if (!nullable3.GetValueOrDefault())
            {
              nullable3 = row.WeeklyOnDay6;
              if (!nullable3.GetValueOrDefault())
              {
                nullable3 = row.WeeklyOnDay7;
                if (!nullable3.GetValueOrDefault())
                {
                  sender.RaiseExceptionHandling<Schedule.weeklyOnDay1>((object) row, (object) null, (Exception) new PXSetPropertyException("You must select at least one day of week"));
                  flag1 = false;
                }
              }
            }
          }
        }
      }
    }
    if (!((e.Operation & 3) != 3 & flag1))
      return;
    ((Schedule) e.Row).NextRunDate = new Scheduler((PXGraph) this).GetNextRunDate((Schedule) e.Row);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable runNow(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    WZScheduleMaint.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new WZScheduleMaint.\u003C\u003Ec__DisplayClass14_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.schedule = ((PXSelectBase<Schedule>) this.Schedule_Header).Current;
    // ISSUE: reference to a compiler-generated field
    DateTime? nextRunDate = cDisplayClass140.schedule.NextRunDate;
    DateTime? businessDate1 = ((PXGraph) this).Accessinfo.BusinessDate;
    if ((nextRunDate.HasValue & businessDate1.HasValue ? (nextRunDate.GetValueOrDefault() > businessDate1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("The next generation date for this task is greater than the current business date.");
    // ISSUE: reference to a compiler-generated field
    bool? noRunLimit = cDisplayClass140.schedule.NoRunLimit;
    bool flag1 = false;
    if (noRunLimit.GetValueOrDefault() == flag1 & noRunLimit.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      short? nullable1 = cDisplayClass140.schedule.RunCntr;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      // ISSUE: reference to a compiler-generated field
      nullable1 = cDisplayClass140.schedule.RunLimit;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXException("The task reached the configured limit and will not be processed. Please change the task limit or deactivate it.");
    }
    // ISSUE: reference to a compiler-generated field
    bool? noEndDate = cDisplayClass140.schedule.NoEndDate;
    bool flag2 = false;
    if (noEndDate.GetValueOrDefault() == flag2 & noEndDate.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      DateTime? endDate = cDisplayClass140.schedule.EndDate;
      DateTime? businessDate2 = ((PXGraph) this).Accessinfo.BusinessDate;
      if ((endDate.HasValue & businessDate2.HasValue ? (endDate.GetValueOrDefault() < businessDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXException("The task will not be processed. The expiration date is less than the current business date. Please change the business date or deactivate the task.");
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CrunNow\u003Eb__0)));
    return adapter.Get();
  }

  protected virtual void WZScenario_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    WZScenario row1 = (WZScenario) e.Row;
    if (row1 == null || !row1.ScenarioID.HasValue)
      return;
    WZScenario wzScenario = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelectReadonly<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row1.ScenarioID
    }));
    if (wzScenario != null)
    {
      if (wzScenario.ScheduleID != null)
        cache.RaiseExceptionHandling<WZScenario.scenarioID>((object) wzScenario, (object) wzScenario.ScenarioID, (Exception) new PXSetPropertyException("The scenario is already used in the {0} schedule.", new object[1]
        {
          (object) wzScenario.ScheduleID
        }));
      ((PXSelectBase<WZScenario>) this.Scenarios).Delete(wzScenario);
      ((PXSelectBase<WZScenario>) this.Scenarios).Update(wzScenario);
    }
    else
    {
      WZScenario row2 = (WZScenario) e.Row;
      ((PXSelectBase<WZScenario>) this.Scenarios).Delete(row2);
      cache.RaiseExceptionHandling<WZScenario.scenarioID>((object) row2, (object) row2.ScenarioID, (Exception) new PXSetPropertyException("The scenario reference is not valid."));
    }
  }

  protected virtual void WZScenario_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is WZScenario row))
      return;
    bool? scheduled = row.Scheduled;
    bool flag = false;
    if (scheduled.GetValueOrDefault() == flag & scheduled.HasValue)
      return;
    if (row.ScheduleID == null)
    {
      row.ScheduleID = ((PXSelectBase<Schedule>) this.Schedule_Header).Current.ScheduleID;
      row.Scheduled = new bool?(true);
    }
    else
    {
      WZScenario wzScenario = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelectReadonly<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ScenarioID
      }));
      if (wzScenario.ScheduleID == null || !(wzScenario.ScheduleID != row.ScheduleID))
        return;
      cache.RaiseExceptionHandling<WZScenario.scenarioID>((object) row, (object) row.Name, (Exception) new PXSetPropertyException("The scenario is already used in the {0} schedule.", new object[1]
      {
        (object) wzScenario.ScheduleID
      }));
    }
  }

  public virtual void Persist()
  {
    foreach (WZScenario wzScenario in ((PXSelectBase) this.Scenarios).Cache.Updated)
    {
      wzScenario.Scheduled = new bool?(true);
      wzScenario.ScheduleID = ((PXSelectBase<Schedule>) this.Schedule_Header).Current.ScheduleID;
      ((PXSelectBase) this.Scenarios).Cache.Update((object) wzScenario);
    }
    foreach (WZScenario wzScenario in ((PXSelectBase) this.Scenarios).Cache.Deleted)
    {
      PXDBDefaultAttribute.SetDefaultForUpdate<WZScenario.scheduleID>(((PXSelectBase) this.Scenarios).Cache, (object) wzScenario, false);
      wzScenario.Scheduled = new bool?(false);
      wzScenario.ScheduleID = (string) null;
      ((PXSelectBase) this.Scenarios).Cache.SetStatus((object) wzScenario, (PXEntryStatus) 1);
      ((PXSelectBase) this.Scenarios).Cache.Update((object) wzScenario);
    }
    ((PXGraph) this).Persist();
  }

  private void SetControlsState(PXCache cache, Schedule s)
  {
    bool flag1 = s.ScheduleType == "M";
    bool flag2 = s.ScheduleType == "P";
    bool flag3 = s.ScheduleType == "W";
    bool flag4 = s.ScheduleType == "D";
    bool flag5 = !s.LastRunDate.HasValue;
    if (s.Active.GetValueOrDefault())
      ((PXAction) this.RunNow).SetEnabled(true);
    else
      ((PXAction) this.RunNow).SetEnabled(false);
    PXUIFieldAttribute.SetVisible<Schedule.dailyFrequency>(cache, (object) s, flag4);
    PXUIFieldAttribute.SetVisible<Schedule.days>(cache, (object) s, flag4);
    this.SetMonthlyControlsState(cache, s);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyFrequency>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay1>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay2>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay3>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay4>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay5>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay6>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeklyOnDay7>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.weeks>(cache, (object) s, flag3);
    PXUIFieldAttribute.SetVisible<Schedule.monthlyFrequency>(cache, (object) s, flag1);
    PXUIFieldAttribute.SetVisible<Schedule.monthlyDaySel>(cache, (object) s, flag1);
    PXUIFieldAttribute.SetVisible<Schedule.periodFrequency>(cache, (object) s, flag2);
    PXUIFieldAttribute.SetVisible<Schedule.periodDateSel>(cache, (object) s, flag2);
    PXUIFieldAttribute.SetVisible<Schedule.periodFixedDay>(cache, (object) s, flag2);
    PXUIFieldAttribute.SetVisible<Schedule.periods>(cache, (object) s, flag2);
    this.SetPeriodicallyControlsState(cache, s);
    PXUIFieldAttribute.SetEnabled<Schedule.endDate>(cache, (object) s, !s.NoEndDate.Value);
    PXUIFieldAttribute.SetEnabled<Schedule.runLimit>(cache, (object) s, !s.NoRunLimit.Value);
    PXDefaultAttribute.SetPersistingCheck<Schedule.endDate>(cache, (object) s, s.NoEndDate.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
    cache.AllowDelete = flag5;
  }

  private void SetMonthlyControlsState(PXCache cache, Schedule s)
  {
    bool flag = s.ScheduleType == "M";
    PXUIFieldAttribute.SetEnabled<Schedule.monthlyOnDay>(cache, (object) s, flag && s.MonthlyDaySel == "D");
    PXUIFieldAttribute.SetEnabled<Schedule.monthlyOnWeek>(cache, (object) s, flag && s.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetEnabled<Schedule.monthlyOnDayOfWeek>(cache, (object) s, flag && s.MonthlyDaySel == "W");
  }

  private void SetPeriodicallyControlsState(PXCache cache, Schedule s)
  {
    bool flag = s.ScheduleType == "P";
    PXUIFieldAttribute.SetEnabled<Schedule.periodFixedDay>(cache, (object) s, flag && s.PeriodDateSel == "D");
  }
}
