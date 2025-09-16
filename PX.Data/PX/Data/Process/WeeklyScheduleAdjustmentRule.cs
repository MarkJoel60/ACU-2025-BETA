// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.WeeklyScheduleAdjustmentRule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Data.Process;

internal class WeeklyScheduleAdjustmentRule : ScheduleAdjustmentRuleBase
{
  public override string TypeID => "W";

  public override void AdjustNextDate(AUSchedule schedule)
  {
    short num = schedule.WeeklyFrequency.GetValueOrDefault();
    if (num < (short) 1)
      num = (short) 1;
    schedule.NextRunDate = new System.DateTime?(this.GetClosestDayOfWeek(schedule).AddDays((double) (7 * ((int) num - 1))));
  }

  private System.DateTime GetClosestDayOfWeek(AUSchedule schedule)
  {
    bool? weeklyOnDay1_1 = schedule.WeeklyOnDay1;
    bool flag1 = true;
    if (!(weeklyOnDay1_1.GetValueOrDefault() == flag1 & weeklyOnDay1_1.HasValue))
    {
      bool? weeklyOnDay2 = schedule.WeeklyOnDay2;
      bool flag2 = true;
      if (!(weeklyOnDay2.GetValueOrDefault() == flag2 & weeklyOnDay2.HasValue))
      {
        bool? weeklyOnDay3 = schedule.WeeklyOnDay3;
        bool flag3 = true;
        if (!(weeklyOnDay3.GetValueOrDefault() == flag3 & weeklyOnDay3.HasValue))
        {
          bool? weeklyOnDay4 = schedule.WeeklyOnDay4;
          bool flag4 = true;
          if (!(weeklyOnDay4.GetValueOrDefault() == flag4 & weeklyOnDay4.HasValue))
          {
            bool? weeklyOnDay5 = schedule.WeeklyOnDay5;
            bool flag5 = true;
            if (!(weeklyOnDay5.GetValueOrDefault() == flag5 & weeklyOnDay5.HasValue))
            {
              bool? weeklyOnDay6 = schedule.WeeklyOnDay6;
              bool flag6 = true;
              if (!(weeklyOnDay6.GetValueOrDefault() == flag6 & weeklyOnDay6.HasValue))
              {
                bool? weeklyOnDay7 = schedule.WeeklyOnDay7;
                bool flag7 = true;
                if (!(weeklyOnDay7.GetValueOrDefault() == flag7 & weeklyOnDay7.HasValue))
                  schedule.WeeklyOnDay1 = new bool?(true);
              }
            }
          }
        }
      }
    }
    System.DateTime closestDayOfWeek = schedule.NextRunDate.Value;
    while (true)
    {
      bool? weeklyOnDay1_2 = schedule.WeeklyOnDay1;
      bool flag8 = true;
      if (!(weeklyOnDay1_2.GetValueOrDefault() == flag8 & weeklyOnDay1_2.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Sunday)
      {
        bool? weeklyOnDay2 = schedule.WeeklyOnDay2;
        bool flag9 = true;
        if (!(weeklyOnDay2.GetValueOrDefault() == flag9 & weeklyOnDay2.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Monday)
        {
          bool? weeklyOnDay3 = schedule.WeeklyOnDay3;
          bool flag10 = true;
          if (!(weeklyOnDay3.GetValueOrDefault() == flag10 & weeklyOnDay3.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Tuesday)
          {
            bool? weeklyOnDay4 = schedule.WeeklyOnDay4;
            bool flag11 = true;
            if (!(weeklyOnDay4.GetValueOrDefault() == flag11 & weeklyOnDay4.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Wednesday)
            {
              bool? weeklyOnDay5 = schedule.WeeklyOnDay5;
              bool flag12 = true;
              if (!(weeklyOnDay5.GetValueOrDefault() == flag12 & weeklyOnDay5.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Thursday)
              {
                bool? weeklyOnDay6 = schedule.WeeklyOnDay6;
                bool flag13 = true;
                if (!(weeklyOnDay6.GetValueOrDefault() == flag13 & weeklyOnDay6.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Friday)
                {
                  bool? weeklyOnDay7 = schedule.WeeklyOnDay7;
                  bool flag14 = true;
                  if (!(weeklyOnDay7.GetValueOrDefault() == flag14 & weeklyOnDay7.HasValue) || closestDayOfWeek.DayOfWeek != DayOfWeek.Saturday)
                    closestDayOfWeek = closestDayOfWeek.AddDays(1.0);
                  else
                    break;
                }
                else
                  break;
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
        else
          break;
      }
      else
        break;
    }
    return closestDayOfWeek;
  }
}
