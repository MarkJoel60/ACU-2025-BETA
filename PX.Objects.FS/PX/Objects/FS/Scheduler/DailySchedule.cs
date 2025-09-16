// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.DailySchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This Class specifies the structure for an daily Schedule.
/// </summary>
public class DailySchedule : RepeatingSchedule
{
  /// <summary>
  /// Handles if the rule applies in the [date] using the Frequency of the Schedule.
  /// </summary>
  public override bool OccursOnDate(DateTime date)
  {
    return this.DateIsInPeriodAndIsANewDate(date) && this.DateIsValidForSchedule(date);
  }

  /// <summary>
  /// Validate if the [date] is valid for the Schedule using the Frequency.
  /// </summary>
  private bool DateIsValidForSchedule(DateTime date)
  {
    return (int) date.Subtract(this.StartOrLastDate).TotalDays % this.Frequency == 0;
  }
}
