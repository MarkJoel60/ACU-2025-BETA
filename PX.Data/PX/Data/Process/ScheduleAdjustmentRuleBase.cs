// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduleAdjustmentRuleBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;

#nullable disable
namespace PX.Data.Process;

[PXInternalUseOnly]
public abstract class ScheduleAdjustmentRuleBase : IScheduleAdjustmentRule
{
  public abstract string TypeID { get; }

  public abstract void AdjustNextDate(AUSchedule schedule);

  public void AdjustNextDate(AUSchedule schedule, System.DateTime startedAt)
  {
    if (!schedule.NextRunDate.HasValue)
      schedule.NextRunDate = new System.DateTime?(startedAt.Date);
    System.DateTime? nullable1 = schedule.NextRunDate;
    System.DateTime date1 = startedAt.Date;
    if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < date1 ? 1 : 0) : 0) != 0)
      schedule.NextRunDate = new System.DateTime?(startedAt.Date);
    bool findNextDay = false;
    nullable1 = schedule.StartTime;
    if (nullable1.HasValue)
    {
      if (schedule.Interval.HasValue)
      {
        short? interval = schedule.Interval;
        int? nullable2 = interval.HasValue ? new int?((int) interval.GetValueOrDefault()) : new int?();
        int num = 0;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
          goto label_10;
      }
      schedule.NextRunTime = schedule.StartTime;
      nullable1 = schedule.EndTime;
      if (!nullable1.HasValue || !schedule.IsTimeInActiveRange(startedAt))
      {
        TimeSpan timeOfDay1 = startedAt.TimeOfDay;
        nullable1 = schedule.StartTime;
        TimeSpan timeOfDay2 = nullable1.Value.TimeOfDay;
        if (!(timeOfDay1 >= timeOfDay2))
          goto label_17;
      }
      findNextDay = true;
      goto label_17;
    }
label_10:
    schedule.NextRunTime = new System.DateTime?(schedule.AddInterval(startedAt));
    nullable1 = schedule.NextRunTime;
    if (nullable1.Value.Date > startedAt.Date)
      findNextDay = true;
    System.DateTime zeroDate = AUSchedule.ZeroDate;
    ref System.DateTime local = ref zeroDate;
    nullable1 = schedule.NextRunTime;
    System.DateTime dateTime1 = nullable1.Value;
    nullable1 = schedule.NextRunTime;
    System.DateTime date2 = nullable1.Value.Date;
    TimeSpan timeSpan = dateTime1 - date2;
    System.DateTime dateTime2 = local.Add(timeSpan);
    nullable1 = schedule.StartTime;
    if (nullable1.HasValue)
    {
      nullable1 = schedule.EndTime;
      if (nullable1.HasValue && !schedule.IsTimeInActiveRange(dateTime2))
      {
        nullable1 = schedule.NextRunTime;
        zeroDate = nullable1.Value;
        TimeSpan timeOfDay3 = zeroDate.TimeOfDay;
        nullable1 = schedule.StartTime;
        zeroDate = nullable1.Value;
        TimeSpan timeOfDay4 = zeroDate.TimeOfDay;
        if (timeOfDay3 > timeOfDay4)
          findNextDay = true;
        schedule.NextRunTime = schedule.StartTime;
      }
    }
label_17:
    this.AdjustNextDate(schedule, findNextDay);
    int num1;
    if (!findNextDay)
    {
      nullable1 = schedule.NextRunDate;
      zeroDate = nullable1.Value;
      num1 = zeroDate.Date > startedAt.Date ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 == 0)
      return;
    nullable1 = schedule.StartTime;
    if (!nullable1.HasValue)
      return;
    schedule.NextRunTime = schedule.StartTime;
  }

  protected virtual void AdjustNextDate(AUSchedule schedule, bool findNextDay)
  {
    if (findNextDay)
      schedule.NextRunDate = new System.DateTime?(schedule.NextRunDate.Value.AddDays(1.0));
    this.AdjustNextDate(schedule);
  }
}
