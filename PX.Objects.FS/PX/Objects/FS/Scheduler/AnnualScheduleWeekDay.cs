// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.AnnualScheduleWeekDay
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the structure for a Monthly Schedule in a specific weekday of the month.
/// </summary>
public class AnnualScheduleWeekDay : AnnualSchedule
{
  /// <summary>
  /// Gets or sets attribute to specify the number of the week in the month.
  /// </summary>
  public short MonthlyOnWeek { get; set; }

  /// <summary>
  /// Gets or sets attribute to specify the day of the week in which applies the Schedule.
  /// </summary>
  public DayOfWeek MonthlyOnDayOfWeek { get; set; }

  /// <summary>
  /// Validates if the [date] matches with the [MonthlyOnWeek] and [MonthlyOnDayOfWeek] specified in the Schedule.
  /// </summary>
  public override bool IsOnCorrectDate(DateTime date)
  {
    if (!this.months.Contains(SharedFunctions.getMonthOfYearByID(date.Month)))
      return false;
    DateTime dateTime = new DateTime(date.Year, date.Month, 1);
    while (dateTime.DayOfWeek != this.MonthlyOnDayOfWeek)
      dateTime = dateTime.AddDays(1.0);
    int num = dateTime.Day + ((int) this.MonthlyOnWeek - 1) * 7;
    if (num > DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
      num -= 7;
    return date.Day == num;
  }
}
