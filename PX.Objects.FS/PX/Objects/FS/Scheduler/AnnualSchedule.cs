// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.AnnualSchedule
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
/// This class specifies the structure for a Annual Schedule.
/// </summary>
public abstract class AnnualSchedule : RepeatingSchedule
{
  /// <summary>
  /// The list of the months of the year in which the Schedule applies.
  /// </summary>
  public List<SharedFunctions.MonthsOfYear> months;

  /// <summary>
  /// Set the months of the year to the [_Months] Attribute.
  /// </summary>
  public void SetMonths(IEnumerable<SharedFunctions.MonthsOfYear> months)
  {
    this.months = months.Distinct<SharedFunctions.MonthsOfYear>().ToList<SharedFunctions.MonthsOfYear>();
  }

  /// <summary>
  /// Handles if the rule applies in the [date] using the Frequency of the Schedule.
  /// </summary>
  public override bool OccursOnDate(DateTime date)
  {
    return this.DateIsInPeriodAndIsANewDate(date) && this.IsOnCorrectDate(date) && (date.Year - this.StartOrLastDate.Year) % this.Frequency == 0;
  }

  /// <summary>
  /// Method to be implemented in child classes. Handles if the rule applies in the [date] depending of the monthly Schedule type.
  /// </summary>
  public abstract bool IsOnCorrectDate(DateTime date);
}
