// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.WeeklySchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This Class specifies the structure for a weekly Schedule.
/// </summary>
public class WeeklySchedule : RepeatingSchedule
{
  /// <summary>
  /// The list of the days of the week in which the Schedule applies.
  /// </summary>
  private List<DayOfWeek> days;

  /// <summary>Set the days of the week to the [days] Attribute.</summary>
  public void SetDays(IEnumerable<DayOfWeek> days)
  {
    this.days = days.Distinct<DayOfWeek>().ToList<DayOfWeek>();
  }

  /// <summary>
  /// Handles if the rule applies in the [date] using the List [days] and the Frequency of the Schedule.
  /// </summary>
  public override bool OccursOnDate(DateTime date)
  {
    if (!this.DateIsInPeriodAndIsANewDate(date) || !this.days.Contains(date.DayOfWeek))
      return false;
    int num1 = (int) (1 - this.StartOrLastDate.DayOfWeek);
    int num2 = (int) (1 - date.DayOfWeek);
    return (int) this.StartOrLastDate.AddDays((double) num1).Subtract(date.AddDays((double) num2)).TotalDays / 7 % this.Frequency == 0;
  }
}
