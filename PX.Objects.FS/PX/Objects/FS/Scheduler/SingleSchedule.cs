// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.SingleSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the structure for a single day Schedule.
/// </summary>
public class SingleSchedule : Schedule
{
  /// <summary>
  /// Gets or sets the specific date of the Single Schedule.
  /// </summary>
  public DateTime Date { get; set; }

  /// <summary>
  /// Validates if the Schedule occurs in the parameter [date].
  /// </summary>
  public override bool OccursOnDate(DateTime date) => this.Date.Date == date;
}
