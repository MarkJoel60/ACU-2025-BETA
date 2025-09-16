// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.MonthlyScheduleAdjustmentRule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Data.Process;

internal class MonthlyScheduleAdjustmentRule : ScheduleAdjustmentRuleBase
{
  public override string TypeID => "M";

  public override void AdjustNextDate(AUSchedule schedule)
  {
    System.DateTime target1 = schedule.NextRunDate.Value.AddDays(1.0 - (double) schedule.NextRunDate.Value.Day);
    if (schedule.MonthlyDaySel == "W")
    {
      System.DateTime target2 = this.CalculateWeeklyOnDay(schedule, target1);
      if (schedule.NextRunDate.Value.Day > target2.Day)
      {
        System.DateTime dateTime = schedule.NextRunDate.Value;
        dateTime = dateTime.AddDays(1.0 - (double) schedule.NextRunDate.Value.Day);
        target2 = dateTime.AddMonths(1);
        target2 = this.CalculateWeeklyOnDay(schedule, target2);
      }
      target2 = target2.AddMonths((int) schedule.MonthlyFrequency.Value - 1);
      schedule.NextRunDate = new System.DateTime?(target2);
    }
    else
    {
      System.DateTime dateTime1 = target1.AddMonths((int) schedule.MonthlyFrequency.Value);
      int day = schedule.NextRunDate.Value.Day;
      short? monthlyOnDay = schedule.MonthlyOnDay;
      int? nullable = monthlyOnDay.HasValue ? new int?((int) monthlyOnDay.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable.GetValueOrDefault();
      if (day <= valueOrDefault & nullable.HasValue)
        dateTime1 = dateTime1.AddMonths(-1);
      int month = dateTime1.Month;
      ref System.DateTime local = ref dateTime1;
      monthlyOnDay = schedule.MonthlyOnDay;
      double num = (double) monthlyOnDay.Value - 1.0;
      System.DateTime dateTime2 = local.AddDays(num);
      while (dateTime2.Month > month)
        dateTime2 = dateTime2.AddDays(-1.0);
      schedule.NextRunDate = new System.DateTime?(dateTime2);
    }
  }

  private System.DateTime CalculateWeeklyOnDay(AUSchedule schedule, System.DateTime target)
  {
    short? monthlyOnWeek = schedule.MonthlyOnWeek;
    int? nullable1 = monthlyOnWeek.HasValue ? new int?((int) monthlyOnWeek.GetValueOrDefault()) : new int?();
    int num1 = 5;
    if (nullable1.GetValueOrDefault() < num1 & nullable1.HasValue)
    {
      target = target.AddDays(-1.0);
      short? nullable2 = schedule.MonthlyOnWeek;
      int num2 = (int) nullable2.Value;
      while (num2 > 0)
      {
        target = target.AddDays(1.0);
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num3 = 1;
        if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num4 = 9;
          if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
            goto label_5;
        }
        if (target.DayOfWeek == DayOfWeek.Sunday)
          goto label_23;
label_5:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num5 = 2;
        if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num6 = 8;
          if (!(nullable1.GetValueOrDefault() == num6 & nullable1.HasValue))
            goto label_8;
        }
        if (target.DayOfWeek == DayOfWeek.Monday)
          goto label_23;
label_8:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num7 = 3;
        if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num8 = 8;
          if (!(nullable1.GetValueOrDefault() == num8 & nullable1.HasValue))
            goto label_11;
        }
        if (target.DayOfWeek == DayOfWeek.Tuesday)
          goto label_23;
label_11:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num9 = 4;
        if (!(nullable1.GetValueOrDefault() == num9 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num10 = 8;
          if (!(nullable1.GetValueOrDefault() == num10 & nullable1.HasValue))
            goto label_14;
        }
        if (target.DayOfWeek == DayOfWeek.Wednesday)
          goto label_23;
label_14:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num11 = 5;
        if (!(nullable1.GetValueOrDefault() == num11 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num12 = 8;
          if (!(nullable1.GetValueOrDefault() == num12 & nullable1.HasValue))
            goto label_17;
        }
        if (target.DayOfWeek == DayOfWeek.Thursday)
          goto label_23;
label_17:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num13 = 6;
        if (!(nullable1.GetValueOrDefault() == num13 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num14 = 8;
          if (!(nullable1.GetValueOrDefault() == num14 & nullable1.HasValue))
            goto label_20;
        }
        if (target.DayOfWeek == DayOfWeek.Friday)
          goto label_23;
label_20:
        nullable2 = schedule.MonthlyOnDayOfWeek;
        nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
        int num15 = 7;
        if (!(nullable1.GetValueOrDefault() == num15 & nullable1.HasValue))
        {
          nullable2 = schedule.MonthlyOnDayOfWeek;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num16 = 9;
          if (!(nullable1.GetValueOrDefault() == num16 & nullable1.HasValue))
            continue;
        }
        if (target.DayOfWeek != DayOfWeek.Saturday)
          continue;
label_23:
        --num2;
      }
    }
    else
    {
      target = target.AddMonths(1);
      do
      {
        target = target.AddDays(-1.0);
        short? monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num17 = 1;
        if (!(nullable1.GetValueOrDefault() == num17 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num18 = 9;
          if (!(nullable1.GetValueOrDefault() == num18 & nullable1.HasValue))
            goto label_29;
        }
        if (target.DayOfWeek == DayOfWeek.Sunday)
          break;
label_29:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num19 = 2;
        if (!(nullable1.GetValueOrDefault() == num19 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num20 = 8;
          if (!(nullable1.GetValueOrDefault() == num20 & nullable1.HasValue))
            goto label_32;
        }
        if (target.DayOfWeek == DayOfWeek.Monday)
          break;
label_32:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num21 = 3;
        if (!(nullable1.GetValueOrDefault() == num21 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num22 = 8;
          if (!(nullable1.GetValueOrDefault() == num22 & nullable1.HasValue))
            goto label_35;
        }
        if (target.DayOfWeek == DayOfWeek.Tuesday)
          break;
label_35:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num23 = 4;
        if (!(nullable1.GetValueOrDefault() == num23 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num24 = 8;
          if (!(nullable1.GetValueOrDefault() == num24 & nullable1.HasValue))
            goto label_38;
        }
        if (target.DayOfWeek == DayOfWeek.Wednesday)
          break;
label_38:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num25 = 5;
        if (!(nullable1.GetValueOrDefault() == num25 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num26 = 8;
          if (!(nullable1.GetValueOrDefault() == num26 & nullable1.HasValue))
            goto label_41;
        }
        if (target.DayOfWeek == DayOfWeek.Thursday)
          break;
label_41:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num27 = 6;
        if (!(nullable1.GetValueOrDefault() == num27 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num28 = 8;
          if (!(nullable1.GetValueOrDefault() == num28 & nullable1.HasValue))
            goto label_44;
        }
        if (target.DayOfWeek == DayOfWeek.Friday)
          break;
label_44:
        monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
        nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
        int num29 = 7;
        if (!(nullable1.GetValueOrDefault() == num29 & nullable1.HasValue))
        {
          monthlyOnDayOfWeek = schedule.MonthlyOnDayOfWeek;
          nullable1 = monthlyOnDayOfWeek.HasValue ? new int?((int) monthlyOnDayOfWeek.GetValueOrDefault()) : new int?();
          int num30 = 9;
          if (!(nullable1.GetValueOrDefault() == num30 & nullable1.HasValue))
            ;
        }
      }
      while (target.DayOfWeek != DayOfWeek.Saturday);
    }
    return target;
  }
}
