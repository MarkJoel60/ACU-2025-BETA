// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.AnnualScheduleSpecificDay
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the structure for a Annual Schedule in a specific day of the month.
/// </summary>
public class AnnualScheduleSpecificDay : AnnualSchedule
{
  /// <summary>
  /// Gets or sets the number of the specific day of the month.
  /// </summary>
  public int DayOfMonth { get; set; }

  /// <summary>
  /// Handles if the rule applies in the specific [date] using the [DayOfMonth]. It will return the last day if the [DayOfMonth] is incorrect for that month.
  /// </summary>
  public override bool IsOnCorrectDate(DateTime date)
  {
    return this.months.Contains(SharedFunctions.getMonthOfYearByID(date.Month)) && (date.Day == this.DayOfMonth || date.Day == DateTime.DaysInMonth(date.Year, date.Month) && this.DayOfMonth > date.Day);
  }
}
