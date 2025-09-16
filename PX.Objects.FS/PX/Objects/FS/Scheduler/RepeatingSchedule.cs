// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.RepeatingSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the structure for repeating Schedules.
/// </summary>
public abstract class RepeatingSchedule : Schedule
{
  /// <summary>Used to specify the frequency of a Schedule rule.</summary>
  private int frequency;

  /// <summary>
  /// Gets or sets the period to be consider in order to generate the Time Slots for the specific repeating Schedule.
  /// </summary>
  public Period SchedulingRange { get; set; }

  /// <summary>
  /// Gets or sets attribute to specify the frequency. It also validates if the value &gt; 0.
  /// </summary>
  public int Frequency
  {
    get => this.frequency;
    set
    {
      this.frequency = value > 0 ? value : throw new ArgumentException("There must be at least one day between time-slots");
    }
  }

  /// <summary>
  /// Gets attribute to specify the Date of the last successful Time Slot generated with this Schedule. It will be set as SchedulingRange if the [LastGeneratedTimeSlotDate] is null.
  /// </summary>
  protected DateTime StartOrLastDate
  {
    get
    {
      return !this.LastGeneratedTimeSlotDate.HasValue ? this.SchedulingRange.Start : this.LastGeneratedTimeSlotDate.Value;
    }
  }

  /// <summary>
  /// Validates if the [date] is within the Scheduling Range and if it has not been already generated in a previous run of the Schedule.
  /// </summary>
  protected bool DateIsInPeriodAndIsANewDate(DateTime date)
  {
    if (this.SchedulingRange == null || !(date >= this.SchedulingRange.Start))
      return false;
    DateTime? nullable = this.SchedulingRange.End;
    if (nullable.HasValue)
    {
      DateTime dateTime = date;
      nullable = this.SchedulingRange.End;
      if ((nullable.HasValue ? (dateTime <= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
    }
    nullable = this.LastGeneratedTimeSlotDate;
    if (!nullable.HasValue)
      return true;
    DateTime dateTime1 = date;
    nullable = this.LastGeneratedTimeSlotDate;
    return nullable.HasValue && dateTime1 > nullable.GetValueOrDefault();
  }
}
