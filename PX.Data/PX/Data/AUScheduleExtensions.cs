// Decompiled with JetBrains decompiler
// Type: PX.Data.AUScheduleExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process;
using PX.SM;
using System;

#nullable disable
namespace PX.Data;

internal static class AUScheduleExtensions
{
  /// <summary>
  /// Determines whether defined time is in active schedule range [StartTime, EndTime].
  /// </summary>
  public static bool IsTimeInActiveRange(this AUSchedule schedule, System.DateTime value)
  {
    if (!schedule.StartTime.HasValue || !schedule.EndTime.HasValue)
      throw new ArgumentException("The schedule doesn't have start or end time.");
    System.DateTime dateTime1 = schedule.StartTime.Value;
    System.DateTime dateTime2 = schedule.EndTime.Value;
    System.DateTime dateTime3 = value;
    if (dateTime3 - AUSchedule.ZeroDate > TimeSpan.FromDays(2.0))
      dateTime3 = AUSchedule.ZeroDate.Add(value.TimeOfDay);
    if (dateTime1 > dateTime2)
    {
      dateTime2 = dateTime2.AddDays(1.0);
      if (dateTime3.AddDays(1.0) < dateTime2)
        dateTime3 = dateTime3.AddDays(1.0);
    }
    return dateTime3 >= dateTime1 && dateTime3 <= dateTime2;
  }

  /// <summary>
  /// Applies schedule adjustment rule provided by <see cref="T:PX.Data.Process.IScheduleAdjustmentRuleProvider" /> for defined schedule.
  /// If adjustment rule is null, nothing is applied.
  /// </summary>
  public static void ApplyAdjustmentRule(
    this AUSchedule schedule,
    IScheduleAdjustmentRuleProvider ruleProvider)
  {
    IScheduleAdjustmentRule rule = ruleProvider.GetRule(schedule);
    if (!schedule.LastRunDate.HasValue)
    {
      System.DateTime? nullable = schedule.StartDate;
      if (nullable.HasValue)
      {
        if (rule == null)
          return;
        IScheduleAdjustmentRule scheduleAdjustmentRule = rule;
        AUSchedule schedule1 = schedule;
        nullable = schedule.StartDateTime;
        System.DateTime startedAt = nullable.Value;
        scheduleAdjustmentRule.AdjustNextDate(schedule1, startedAt);
        return;
      }
    }
    rule.AdjustNextDate(schedule);
  }

  public static void AdjustNextDate(
    this AUSchedule schedule,
    System.DateTime startedAt,
    IScheduleAdjustmentRuleProvider ruleProvider)
  {
    ruleProvider.GetRule(schedule)?.AdjustNextDate(schedule, startedAt);
  }

  internal static System.DateTime AddInterval(this AUSchedule schedule, System.DateTime startedAt)
  {
    if (!schedule.Interval.HasValue)
      throw new InvalidOperationException("Interval is null.");
    double num1 = (double) schedule.Interval.Value;
    bool? exactTime = schedule.ExactTime;
    bool flag = true;
    if (!(exactTime.GetValueOrDefault() == flag & exactTime.HasValue) || !schedule.StartTime.HasValue && !schedule.NextRunTime.HasValue)
      return startedAt.AddMinutes(num1);
    System.DateTime dateTime = schedule.StartTime.HasValue ? schedule.StartTime.Value : schedule.NextRunTime.Value;
    long num2 = System.Math.Abs(startedAt.TimeOfDay.Ticks - dateTime.TimeOfDay.Ticks);
    long ticks = TimeSpan.FromMinutes(num1).Ticks;
    long num3 = System.Math.Abs(num2 - ticks * (num2 / ticks));
    return startedAt.AddTicks(-num3).AddMinutes(num1);
  }
}
