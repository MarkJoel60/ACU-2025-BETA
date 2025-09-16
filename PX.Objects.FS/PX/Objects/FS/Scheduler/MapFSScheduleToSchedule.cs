// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.MapFSScheduleToSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class allows to map a FSSchedule in the Service Management module to a Schedule in the Scheduler module.
/// </summary>
public static class MapFSScheduleToSchedule
{
  /// <summary>
  /// This function converts a FSSchedule to a List[Schedule].
  /// </summary>
  public static List<Schedule> convertFSScheduleToSchedule(
    PXCache cache,
    FSSchedule fsScheduleRow,
    DateTime? toDate,
    string recordType,
    Period period = null)
  {
    List<Schedule> schedule = new List<Schedule>();
    switch (fsScheduleRow.FrequencyType)
    {
      case "D":
        schedule.Add(MapFSScheduleToSchedule.mapDailyFrequency(cache, fsScheduleRow, toDate, recordType, period));
        break;
      case "W":
        schedule.Add(MapFSScheduleToSchedule.mapWeeklyFrequency(cache, fsScheduleRow, toDate, recordType, period));
        break;
      case "M":
        schedule.Add(MapFSScheduleToSchedule.mapMonthlyFrequency(cache, fsScheduleRow, 1, toDate, recordType, fsScheduleRow.MonthlyRecurrenceType1, period));
        if (fsScheduleRow.Monthly2Selected.GetValueOrDefault())
          schedule.Add(MapFSScheduleToSchedule.mapMonthlyFrequency(cache, fsScheduleRow, 2, toDate, recordType, fsScheduleRow.MonthlyRecurrenceType2, period));
        if (fsScheduleRow.Monthly3Selected.GetValueOrDefault())
          schedule.Add(MapFSScheduleToSchedule.mapMonthlyFrequency(cache, fsScheduleRow, 3, toDate, recordType, fsScheduleRow.MonthlyRecurrenceType3, period));
        if (fsScheduleRow.Monthly4Selected.GetValueOrDefault())
        {
          schedule.Add(MapFSScheduleToSchedule.mapMonthlyFrequency(cache, fsScheduleRow, 4, toDate, recordType, fsScheduleRow.MonthlyRecurrenceType4, period));
          break;
        }
        break;
      case "A":
        schedule.Add(MapFSScheduleToSchedule.mapAnnualFrequency(fsScheduleRow, toDate, period));
        break;
    }
    return schedule;
  }

  /// <summary>
  /// This function maps a FSSchedule daily frequency to a DailySchedule in the Scheduler module.
  /// </summary>
  public static Schedule mapDailyFrequency(
    PXCache cache,
    FSSchedule fsScheduleRow,
    DateTime? toDate,
    string recordType,
    Period period = null)
  {
    bool flag = recordType != "EPSC" && SharedFunctions.GetEnableSeasonSetting(cache.Graph, fsScheduleRow);
    DailySchedule dailySchedule = new DailySchedule();
    dailySchedule.Name = "Daily";
    dailySchedule.EntityID = fsScheduleRow.EntityID;
    dailySchedule.EntityType = fsScheduleRow.EntityType;
    dailySchedule.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
    dailySchedule.ScheduleID = fsScheduleRow.ScheduleID.Value;
    dailySchedule.SubScheduleID = 0;
    dailySchedule.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    dailySchedule.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    dailySchedule.SchedulingRange = period ?? new Period((fsScheduleRow.StartDate ?? toDate).Value, fsScheduleRow.EndDate);
    dailySchedule.Frequency = (int) fsScheduleRow.DailyFrequency.Value;
    dailySchedule.Descr = fsScheduleRow.ContractDescr;
    dailySchedule.ApplySeason = new bool?(flag);
    dailySchedule.SeasonOnJan = fsScheduleRow.SeasonOnJan;
    dailySchedule.SeasonOnFeb = fsScheduleRow.SeasonOnFeb;
    dailySchedule.SeasonOnMar = fsScheduleRow.SeasonOnMar;
    dailySchedule.SeasonOnApr = fsScheduleRow.SeasonOnApr;
    dailySchedule.SeasonOnMay = fsScheduleRow.SeasonOnMay;
    dailySchedule.SeasonOnJun = fsScheduleRow.SeasonOnJun;
    dailySchedule.SeasonOnJul = fsScheduleRow.SeasonOnJul;
    dailySchedule.SeasonOnAug = fsScheduleRow.SeasonOnAug;
    dailySchedule.SeasonOnSep = fsScheduleRow.SeasonOnSep;
    dailySchedule.SeasonOnOct = fsScheduleRow.SeasonOnOct;
    dailySchedule.SeasonOnNov = fsScheduleRow.SeasonOnNov;
    dailySchedule.SeasonOnDec = fsScheduleRow.SeasonOnDec;
    return (Schedule) dailySchedule;
  }

  /// <summary>
  /// This function maps a FSSchedule weekly frequency to a WeeklySchedule in the Scheduler module.
  /// </summary>
  public static Schedule mapWeeklyFrequency(
    PXCache cache,
    FSSchedule fsScheduleRow,
    DateTime? toDate,
    string recordType,
    Period period = null)
  {
    bool flag = recordType != "EPSC" && SharedFunctions.GetEnableSeasonSetting(cache.Graph, fsScheduleRow);
    List<DayOfWeek> dayOfWeekList = new List<DayOfWeek>();
    WeeklySchedule weeklySchedule = new WeeklySchedule();
    weeklySchedule.Name = "Weekly";
    weeklySchedule.EntityID = fsScheduleRow.EntityID;
    weeklySchedule.EntityType = fsScheduleRow.EntityType;
    weeklySchedule.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
    weeklySchedule.ScheduleID = fsScheduleRow.ScheduleID.Value;
    weeklySchedule.SubScheduleID = 0;
    weeklySchedule.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    weeklySchedule.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    weeklySchedule.SchedulingRange = period ?? new Period(fsScheduleRow.StartDate.Value, fsScheduleRow.EndDate);
    weeklySchedule.Frequency = (int) fsScheduleRow.WeeklyFrequency.Value;
    weeklySchedule.Descr = fsScheduleRow.ContractDescr;
    weeklySchedule.ApplySeason = new bool?(flag);
    weeklySchedule.SeasonOnJan = fsScheduleRow.SeasonOnJan;
    weeklySchedule.SeasonOnFeb = fsScheduleRow.SeasonOnFeb;
    weeklySchedule.SeasonOnMar = fsScheduleRow.SeasonOnMar;
    weeklySchedule.SeasonOnApr = fsScheduleRow.SeasonOnApr;
    weeklySchedule.SeasonOnMay = fsScheduleRow.SeasonOnMay;
    weeklySchedule.SeasonOnJun = fsScheduleRow.SeasonOnJun;
    weeklySchedule.SeasonOnJul = fsScheduleRow.SeasonOnJul;
    weeklySchedule.SeasonOnAug = fsScheduleRow.SeasonOnAug;
    weeklySchedule.SeasonOnSep = fsScheduleRow.SeasonOnSep;
    weeklySchedule.SeasonOnOct = fsScheduleRow.SeasonOnOct;
    weeklySchedule.SeasonOnNov = fsScheduleRow.SeasonOnNov;
    weeklySchedule.SeasonOnDec = fsScheduleRow.SeasonOnDec;
    if (fsScheduleRow.WeeklyOnFri.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Friday);
    bool? nullable = fsScheduleRow.WeeklyOnMon;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Monday);
    nullable = fsScheduleRow.WeeklyOnSat;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Saturday);
    nullable = fsScheduleRow.WeeklyOnSun;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Sunday);
    nullable = fsScheduleRow.WeeklyOnThu;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Thursday);
    nullable = fsScheduleRow.WeeklyOnTue;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Tuesday);
    nullable = fsScheduleRow.WeeklyOnWed;
    if (nullable.GetValueOrDefault())
      dayOfWeekList.Add(DayOfWeek.Wednesday);
    weeklySchedule.SetDays((IEnumerable<DayOfWeek>) dayOfWeekList.ToArray());
    return (Schedule) weeklySchedule;
  }

  /// <summary>
  /// This function maps a FSSchedule Monthly frequency to a MonthlyScheduleSpecificDay or MonthlyScheduleWeekDay in the Scheduler module depending of the
  /// <c>fsScheduleRow.MonthlyDaySel</c>. The [SubScheduleID] correspond to which of the four types of Monthly is specified in the [fsScheduleRow].
  /// </summary>
  public static Schedule mapMonthlyFrequency(
    PXCache cache,
    FSSchedule fsScheduleRow,
    int subScheduleID,
    DateTime? toDate,
    string recordType,
    string monthlyRecurrenceType,
    Period period = null)
  {
    bool flag = recordType != "EPSC" && SharedFunctions.GetEnableSeasonSetting(cache.Graph, fsScheduleRow);
    Period period1 = period ?? new Period(fsScheduleRow.StartDate.Value, fsScheduleRow.EndDate);
    if (monthlyRecurrenceType == "D")
    {
      MonthlyScheduleSpecificDay scheduleSpecificDay1 = new MonthlyScheduleSpecificDay();
      scheduleSpecificDay1.Name = "Specific Date in a Month";
      scheduleSpecificDay1.EntityID = fsScheduleRow.EntityID;
      scheduleSpecificDay1.EntityType = fsScheduleRow.EntityType;
      scheduleSpecificDay1.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
      scheduleSpecificDay1.ScheduleID = fsScheduleRow.ScheduleID.Value;
      scheduleSpecificDay1.SubScheduleID = subScheduleID;
      scheduleSpecificDay1.TimeOfDayBegin = new TimeSpan(5, 0, 0);
      scheduleSpecificDay1.TimeOfDayEnd = new TimeSpan(11, 0, 0);
      scheduleSpecificDay1.Frequency = (int) fsScheduleRow.MonthlyFrequency.Value;
      scheduleSpecificDay1.SchedulingRange = period1;
      scheduleSpecificDay1.Descr = fsScheduleRow.ContractDescr;
      scheduleSpecificDay1.ApplySeason = new bool?(flag);
      scheduleSpecificDay1.SeasonOnJan = fsScheduleRow.SeasonOnJan;
      scheduleSpecificDay1.SeasonOnFeb = fsScheduleRow.SeasonOnFeb;
      scheduleSpecificDay1.SeasonOnMar = fsScheduleRow.SeasonOnMar;
      scheduleSpecificDay1.SeasonOnApr = fsScheduleRow.SeasonOnApr;
      scheduleSpecificDay1.SeasonOnMay = fsScheduleRow.SeasonOnMay;
      scheduleSpecificDay1.SeasonOnJun = fsScheduleRow.SeasonOnJun;
      scheduleSpecificDay1.SeasonOnJul = fsScheduleRow.SeasonOnJul;
      scheduleSpecificDay1.SeasonOnAug = fsScheduleRow.SeasonOnAug;
      scheduleSpecificDay1.SeasonOnSep = fsScheduleRow.SeasonOnSep;
      scheduleSpecificDay1.SeasonOnOct = fsScheduleRow.SeasonOnOct;
      scheduleSpecificDay1.SeasonOnNov = fsScheduleRow.SeasonOnNov;
      scheduleSpecificDay1.SeasonOnDec = fsScheduleRow.SeasonOnDec;
      MonthlyScheduleSpecificDay scheduleSpecificDay2 = scheduleSpecificDay1;
      switch (subScheduleID)
      {
        case 1:
          scheduleSpecificDay2.DayOfMonth = (int) fsScheduleRow.MonthlyOnDay1.Value;
          break;
        case 2:
          scheduleSpecificDay2.DayOfMonth = (int) fsScheduleRow.MonthlyOnDay2.Value;
          break;
        case 3:
          scheduleSpecificDay2.DayOfMonth = (int) fsScheduleRow.MonthlyOnDay3.Value;
          break;
        case 4:
          scheduleSpecificDay2.DayOfMonth = (int) fsScheduleRow.MonthlyOnDay4.Value;
          break;
      }
      return (Schedule) scheduleSpecificDay2;
    }
    MonthlyScheduleWeekDay monthlyScheduleWeekDay1 = new MonthlyScheduleWeekDay();
    monthlyScheduleWeekDay1.Name = "Specific Week Day of the Month";
    monthlyScheduleWeekDay1.EntityID = fsScheduleRow.EntityID;
    monthlyScheduleWeekDay1.EntityType = fsScheduleRow.EntityType;
    monthlyScheduleWeekDay1.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
    monthlyScheduleWeekDay1.ScheduleID = fsScheduleRow.ScheduleID.Value;
    monthlyScheduleWeekDay1.SubScheduleID = subScheduleID;
    monthlyScheduleWeekDay1.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    monthlyScheduleWeekDay1.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    monthlyScheduleWeekDay1.Frequency = (int) fsScheduleRow.MonthlyFrequency.Value;
    monthlyScheduleWeekDay1.SchedulingRange = period1;
    monthlyScheduleWeekDay1.Descr = fsScheduleRow.ContractDescr;
    monthlyScheduleWeekDay1.ApplySeason = new bool?(flag);
    monthlyScheduleWeekDay1.SeasonOnJan = fsScheduleRow.SeasonOnJan;
    monthlyScheduleWeekDay1.SeasonOnFeb = fsScheduleRow.SeasonOnFeb;
    monthlyScheduleWeekDay1.SeasonOnMar = fsScheduleRow.SeasonOnMar;
    monthlyScheduleWeekDay1.SeasonOnApr = fsScheduleRow.SeasonOnApr;
    monthlyScheduleWeekDay1.SeasonOnMay = fsScheduleRow.SeasonOnMay;
    monthlyScheduleWeekDay1.SeasonOnJun = fsScheduleRow.SeasonOnJun;
    monthlyScheduleWeekDay1.SeasonOnJul = fsScheduleRow.SeasonOnJul;
    monthlyScheduleWeekDay1.SeasonOnAug = fsScheduleRow.SeasonOnAug;
    monthlyScheduleWeekDay1.SeasonOnSep = fsScheduleRow.SeasonOnSep;
    monthlyScheduleWeekDay1.SeasonOnOct = fsScheduleRow.SeasonOnOct;
    monthlyScheduleWeekDay1.SeasonOnNov = fsScheduleRow.SeasonOnNov;
    monthlyScheduleWeekDay1.SeasonOnDec = fsScheduleRow.SeasonOnDec;
    MonthlyScheduleWeekDay monthlyScheduleWeekDay2 = monthlyScheduleWeekDay1;
    switch (subScheduleID)
    {
      case 1:
        MonthlyScheduleWeekDay monthlyScheduleWeekDay3 = monthlyScheduleWeekDay2;
        short? nullable1 = fsScheduleRow.MonthlyOnWeek1;
        int num1 = (int) nullable1.Value;
        monthlyScheduleWeekDay3.MonthlyOnWeek = (short) num1;
        MonthlyScheduleWeekDay monthlyScheduleWeekDay4 = monthlyScheduleWeekDay2;
        nullable1 = fsScheduleRow.MonthlyOnDayOfWeek1;
        int dayOfWeekById1 = (int) SharedFunctions.getDayOfWeekByID((int) nullable1.Value);
        monthlyScheduleWeekDay4.MonthlyOnDayOfWeek = (DayOfWeek) dayOfWeekById1;
        break;
      case 2:
        MonthlyScheduleWeekDay monthlyScheduleWeekDay5 = monthlyScheduleWeekDay2;
        short? nullable2 = fsScheduleRow.MonthlyOnWeek2;
        int num2 = (int) nullable2.Value;
        monthlyScheduleWeekDay5.MonthlyOnWeek = (short) num2;
        MonthlyScheduleWeekDay monthlyScheduleWeekDay6 = monthlyScheduleWeekDay2;
        nullable2 = fsScheduleRow.MonthlyOnDayOfWeek2;
        int dayOfWeekById2 = (int) SharedFunctions.getDayOfWeekByID((int) nullable2.Value);
        monthlyScheduleWeekDay6.MonthlyOnDayOfWeek = (DayOfWeek) dayOfWeekById2;
        break;
      case 3:
        MonthlyScheduleWeekDay monthlyScheduleWeekDay7 = monthlyScheduleWeekDay2;
        short? nullable3 = fsScheduleRow.MonthlyOnWeek3;
        int num3 = (int) nullable3.Value;
        monthlyScheduleWeekDay7.MonthlyOnWeek = (short) num3;
        MonthlyScheduleWeekDay monthlyScheduleWeekDay8 = monthlyScheduleWeekDay2;
        nullable3 = fsScheduleRow.MonthlyOnDayOfWeek3;
        int dayOfWeekById3 = (int) SharedFunctions.getDayOfWeekByID((int) nullable3.Value);
        monthlyScheduleWeekDay8.MonthlyOnDayOfWeek = (DayOfWeek) dayOfWeekById3;
        break;
      case 4:
        MonthlyScheduleWeekDay monthlyScheduleWeekDay9 = monthlyScheduleWeekDay2;
        short? nullable4 = fsScheduleRow.MonthlyOnWeek4;
        int num4 = (int) nullable4.Value;
        monthlyScheduleWeekDay9.MonthlyOnWeek = (short) num4;
        MonthlyScheduleWeekDay monthlyScheduleWeekDay10 = monthlyScheduleWeekDay2;
        nullable4 = fsScheduleRow.MonthlyOnDayOfWeek4;
        int dayOfWeekById4 = (int) SharedFunctions.getDayOfWeekByID((int) nullable4.Value);
        monthlyScheduleWeekDay10.MonthlyOnDayOfWeek = (DayOfWeek) dayOfWeekById4;
        break;
    }
    return (Schedule) monthlyScheduleWeekDay2;
  }

  public static Schedule mapAnnualFrequency(
    FSSchedule fsScheduleRow,
    DateTime? toDate,
    Period period = null)
  {
    Period period1 = period ?? new Period(fsScheduleRow.StartDate.Value, fsScheduleRow.EndDate);
    if (fsScheduleRow.AnnualRecurrenceType == "D")
    {
      AnnualScheduleSpecificDay scheduleSpecificDay = new AnnualScheduleSpecificDay();
      scheduleSpecificDay.Name = "Specific Date in a Month";
      scheduleSpecificDay.EntityID = fsScheduleRow.EntityID;
      scheduleSpecificDay.EntityType = fsScheduleRow.EntityType;
      scheduleSpecificDay.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
      scheduleSpecificDay.ScheduleID = fsScheduleRow.ScheduleID.Value;
      scheduleSpecificDay.TimeOfDayBegin = new TimeSpan(5, 0, 0);
      scheduleSpecificDay.TimeOfDayEnd = new TimeSpan(11, 0, 0);
      scheduleSpecificDay.Frequency = (int) fsScheduleRow.AnnualFrequency.Value;
      scheduleSpecificDay.SchedulingRange = period1;
      scheduleSpecificDay.Descr = fsScheduleRow.ContractDescr;
      scheduleSpecificDay.ApplySeason = new bool?(false);
      scheduleSpecificDay.DayOfMonth = (int) fsScheduleRow.AnnualOnDay.Value;
      scheduleSpecificDay.SetMonths((IEnumerable<SharedFunctions.MonthsOfYear>) MapFSScheduleToSchedule.SetMonthsToList(fsScheduleRow).ToArray());
      return (Schedule) scheduleSpecificDay;
    }
    AnnualScheduleWeekDay annualScheduleWeekDay = new AnnualScheduleWeekDay();
    annualScheduleWeekDay.Name = "Specific Week Day of the Month";
    annualScheduleWeekDay.EntityID = fsScheduleRow.EntityID;
    annualScheduleWeekDay.EntityType = fsScheduleRow.EntityType;
    annualScheduleWeekDay.LastGeneratedTimeSlotDate = fsScheduleRow.LastGeneratedElementDate;
    annualScheduleWeekDay.ScheduleID = fsScheduleRow.ScheduleID.Value;
    annualScheduleWeekDay.TimeOfDayBegin = new TimeSpan(5, 0, 0);
    annualScheduleWeekDay.TimeOfDayEnd = new TimeSpan(11, 0, 0);
    annualScheduleWeekDay.Frequency = (int) fsScheduleRow.AnnualFrequency.Value;
    annualScheduleWeekDay.SchedulingRange = period1;
    annualScheduleWeekDay.Descr = fsScheduleRow.ContractDescr;
    annualScheduleWeekDay.ApplySeason = new bool?(false);
    annualScheduleWeekDay.MonthlyOnWeek = fsScheduleRow.AnnualOnWeek.Value;
    annualScheduleWeekDay.MonthlyOnDayOfWeek = SharedFunctions.getDayOfWeekByID((int) fsScheduleRow.AnnualOnDayOfWeek.Value);
    annualScheduleWeekDay.SetMonths((IEnumerable<SharedFunctions.MonthsOfYear>) MapFSScheduleToSchedule.SetMonthsToList(fsScheduleRow).ToArray());
    return (Schedule) annualScheduleWeekDay;
  }

  /// <summary>
  /// Set a new list with the selected months of the Schedule.
  /// </summary>
  /// <param name="fsScheduleRow">Instance of FSSchedule DAC.</param>
  /// <returns>List with the selected months of the Schedule.</returns>
  private static List<SharedFunctions.MonthsOfYear> SetMonthsToList(FSSchedule fsScheduleRow)
  {
    List<SharedFunctions.MonthsOfYear> list = new List<SharedFunctions.MonthsOfYear>();
    if (fsScheduleRow.AnnualOnJan.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.January);
    if (fsScheduleRow.AnnualOnFeb.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.February);
    if (fsScheduleRow.AnnualOnMar.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.March);
    if (fsScheduleRow.AnnualOnApr.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.April);
    if (fsScheduleRow.AnnualOnMay.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.May);
    if (fsScheduleRow.AnnualOnJun.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.June);
    if (fsScheduleRow.AnnualOnJul.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.July);
    if (fsScheduleRow.AnnualOnAug.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.August);
    if (fsScheduleRow.AnnualOnSep.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.September);
    if (fsScheduleRow.AnnualOnOct.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.October);
    if (fsScheduleRow.AnnualOnNov.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.November);
    if (fsScheduleRow.AnnualOnDec.GetValueOrDefault())
      list.Add(SharedFunctions.MonthsOfYear.December);
    return list;
  }

  /// <summary>
  /// SubSchedule types, defined to split a complex FSSchedule in multiple Schedules.
  /// </summary>
  public class SubScheduleType
  {
    public const int FIRST = 1;
    public const int SECOND = 2;
    public const int THIRD = 3;
    public const int FOURTH = 4;
  }
}
