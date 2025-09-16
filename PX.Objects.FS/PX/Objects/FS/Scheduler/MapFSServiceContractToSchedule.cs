// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.MapFSServiceContractToSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class allows to map a FSSchedule in the Service Management module to a Schedule in the Scheduler module.
/// </summary>
public static class MapFSServiceContractToSchedule
{
  /// <summary>
  /// This function converts a FSServiceContract to a List[Schedule].
  /// </summary>
  public static List<Schedule> convertFSServiceContractToSchedule(
    FSServiceContract fsServiceContractRow,
    DateTime? lastGeneratedElement,
    Period period = null)
  {
    List<Schedule> schedule = new List<Schedule>();
    switch (fsServiceContractRow.BillingPeriod)
    {
      case "W":
        schedule.Add(MapFSServiceContractToSchedule.mapDailyFrequency(fsServiceContractRow, lastGeneratedElement, 6, period));
        break;
      case "M":
        schedule.Add(MapFSServiceContractToSchedule.mapMonthlyFrequency(fsServiceContractRow, lastGeneratedElement, 1, period));
        break;
      case "Q":
        schedule.Add(MapFSServiceContractToSchedule.mapMonthlyFrequency(fsServiceContractRow, lastGeneratedElement, 3, period));
        break;
      case "H":
        schedule.Add(MapFSServiceContractToSchedule.mapMonthlyFrequency(fsServiceContractRow, lastGeneratedElement, 6, period));
        break;
      case "Y":
        schedule.Add(MapFSServiceContractToSchedule.mapMonthlyFrequency(fsServiceContractRow, lastGeneratedElement, 12, period));
        break;
    }
    return schedule;
  }

  /// <summary>
  /// This function maps a FSServiceCOoract daily frequency to a DailySchedule in the Scheduler module.
  /// </summary>
  public static Schedule mapDailyFrequency(
    FSServiceContract fsServiceContractRow,
    DateTime? lastGeneratedElementDate,
    int frequency,
    Period period = null)
  {
    DailySchedule dailySchedule = new DailySchedule();
    dailySchedule.Name = "Daily";
    dailySchedule.LastGeneratedTimeSlotDate = lastGeneratedElementDate;
    dailySchedule.SubScheduleID = 0;
    dailySchedule.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    dailySchedule.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    dailySchedule.SchedulingRange = period ?? new Period(fsServiceContractRow.StartDate.Value, fsServiceContractRow.EndDate);
    dailySchedule.Frequency = frequency;
    return (Schedule) dailySchedule;
  }

  /// <summary>
  /// This function maps a FSServiceContract weekly frequency to a WeeklySchedule in the Scheduler module.
  /// </summary>
  public static Schedule mapWeeklyFrequency(
    FSServiceContract fsServiceContractRow,
    DateTime? lastGeneratedElementDate,
    Period period = null)
  {
    List<DayOfWeek> dayOfWeekList = new List<DayOfWeek>();
    WeeklySchedule weeklySchedule = new WeeklySchedule();
    weeklySchedule.Name = "Weekly";
    weeklySchedule.LastGeneratedTimeSlotDate = lastGeneratedElementDate;
    weeklySchedule.SubScheduleID = 0;
    weeklySchedule.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    weeklySchedule.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    weeklySchedule.SchedulingRange = period ?? new Period(fsServiceContractRow.StartDate.Value, fsServiceContractRow.EndDate);
    weeklySchedule.Frequency = 1;
    dayOfWeekList.Add(fsServiceContractRow.StartDate.Value.DayOfWeek);
    weeklySchedule.SetDays((IEnumerable<DayOfWeek>) dayOfWeekList.ToArray());
    return (Schedule) weeklySchedule;
  }

  public static Schedule mapMonthlyFrequency(
    FSServiceContract fsServiceContractRow,
    DateTime? lastGeneratedElementDate,
    int frequency,
    Period period = null)
  {
    Period period1 = period ?? new Period(fsServiceContractRow.StartDate.Value, fsServiceContractRow.EndDate);
    MonthlyScheduleSpecificDay scheduleSpecificDay = new MonthlyScheduleSpecificDay();
    scheduleSpecificDay.Name = "Specific Date in a Month";
    scheduleSpecificDay.LastGeneratedTimeSlotDate = lastGeneratedElementDate;
    scheduleSpecificDay.SubScheduleID = 1;
    scheduleSpecificDay.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    scheduleSpecificDay.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    scheduleSpecificDay.Frequency = frequency;
    scheduleSpecificDay.SchedulingRange = period1;
    scheduleSpecificDay.DayOfMonth = fsServiceContractRow.StartDate.Value.Day;
    return (Schedule) scheduleSpecificDay;
  }
}
