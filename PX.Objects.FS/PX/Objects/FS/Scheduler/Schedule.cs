// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.Schedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the template to define a rule for the Time Slot generation.
/// </summary>
public abstract class Schedule
{
  private int? priority;

  /// <summary>
  /// Gets or sets the priority for the Schedule the highest priority is 1.
  /// </summary>
  public int? Priority
  {
    get => this.priority;
    set
    {
      if (!value.HasValue)
        this.priority = new int?(3);
      else
        this.priority = value;
    }
  }

  /// <summary>
  /// Gets or sets the sequence for the Schedule (Routes Module).
  /// </summary>
  public int? Sequence { get; set; }

  /// <summary>
  /// Gets or sets list of route info (Route, ShiftID, Sequence) must be declared (Sunday, ..., Saturday, Default).
  /// </summary>
  public List<RouteInfo> RouteInfoList { get; set; }

  /// <summary>Gets or sets ID of the Schedule in the Database.</summary>
  public int ScheduleID { get; set; }

  /// <summary>
  /// Gets or sets ID of the SubSchedule, this option is used for a Schedule in the database which is converted in multiple Schedules in the Time Slot generation.
  /// </summary>
  public int SubScheduleID { get; set; }

  /// <summary>
  /// Gets or sets ID of the Source or Entity in the Database.
  /// </summary>
  public int? EntityID { get; set; }

  /// <summary>
  /// Gets or sets sourceType or EntityType of the source record in the Database: (Contract, EmployeeSchedule, etc).
  /// </summary>
  public string EntityType { get; set; }

  /// <summary>Gets or sets start time of the Schedule.</summary>
  public TimeSpan TimeOfDayBegin { get; set; }

  /// <summary>Gets or sets end time of the Schedule.</summary>
  public TimeSpan TimeOfDayEnd { get; set; }

  /// <summary>
  /// Gets or sets last date of a successfully generated Time Slot, useful for Schedules with frequencies.
  /// </summary>
  public DateTime? LastGeneratedTimeSlotDate { get; set; }

  /// <summary>Gets or sets an optional Name for the Schedule.</summary>
  public string Name { get; set; }

  /// <summary>Gets or sets Description field for the Schedule.</summary>
  public string Descr { get; set; }

  /// <summary>
  /// Gets or sets value if the season rule applies for the Schedule.
  /// </summary>
  public bool? ApplySeason { get; set; }

  /// <summary>
  /// Gets or sets value for season on January for the Schedule.
  /// </summary>
  public bool? SeasonOnJan { get; set; }

  /// <summary>
  /// Gets or sets value for season on February for the Schedule.
  /// </summary>
  public bool? SeasonOnFeb { get; set; }

  /// <summary>
  /// Gets or sets value for season on March for the Schedule.
  /// </summary>
  public bool? SeasonOnMar { get; set; }

  /// <summary>
  /// Gets or sets value for season on April for the Schedule.
  /// </summary>
  public bool? SeasonOnApr { get; set; }

  /// <summary>
  /// Gets or sets value for season on May for the Schedule.
  /// </summary>
  public bool? SeasonOnMay { get; set; }

  /// <summary>
  /// Gets or sets value for season on June for the Schedule.
  /// </summary>
  public bool? SeasonOnJun { get; set; }

  /// <summary>
  /// Gets or sets value for season on July for the Schedule.
  /// </summary>
  public bool? SeasonOnJul { get; set; }

  /// <summary>
  /// Gets or sets value for season on August for the Schedule.
  /// </summary>
  public bool? SeasonOnAug { get; set; }

  /// <summary>
  /// Gets or sets value for season on September for the Schedule.
  /// </summary>
  public bool? SeasonOnSep { get; set; }

  /// <summary>
  /// Gets or sets value for season on October for the Schedule.
  /// </summary>
  public bool? SeasonOnOct { get; set; }

  /// <summary>
  /// Gets or sets value for season on November for the Schedule.
  /// </summary>
  public bool? SeasonOnNov { get; set; }

  /// <summary>
  /// Gets or sets value for season on December for the Schedule.
  /// </summary>
  public bool? SeasonOnDec { get; set; }

  /// <summary>Handles if the season rule applies in [date].</summary>
  public bool OccursOnSeason(DateTime date)
  {
    if (!this.ApplySeason.GetValueOrDefault())
      return true;
    switch (date.Month)
    {
      case 1:
        return this.SeasonOnJan.GetValueOrDefault();
      case 2:
        return this.SeasonOnFeb.GetValueOrDefault();
      case 3:
        return this.SeasonOnMar.GetValueOrDefault();
      case 4:
        return this.SeasonOnApr.GetValueOrDefault();
      case 5:
        return this.SeasonOnMay.GetValueOrDefault();
      case 6:
        return this.SeasonOnJun.GetValueOrDefault();
      case 7:
        return this.SeasonOnJul.GetValueOrDefault();
      case 8:
        return this.SeasonOnAug.GetValueOrDefault();
      case 9:
        return this.SeasonOnSep.GetValueOrDefault();
      case 10:
        return this.SeasonOnOct.GetValueOrDefault();
      case 11:
        return this.SeasonOnNov.GetValueOrDefault();
      case 12:
        return this.SeasonOnDec.GetValueOrDefault();
      default:
        return false;
    }
  }

  /// <summary>
  /// Method to be implemented in children classes. Handles if the rule applies in the [date].
  /// </summary>
  public abstract bool OccursOnDate(DateTime date);

  public enum ScheduleGenerationPriority
  {
    TimeRestriction = 1,
    Sequence = 2,
    Nothing = 3,
  }
}
