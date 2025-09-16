// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.Period
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the Period time for the generation of the Time Slots.
/// </summary>
public class Period
{
  /// <summary>Gets the beginning date for the Time Slot generation.</summary>
  public DateTime Start { get; private set; }

  /// <summary>Gets the end date for the Time Slot generation.</summary>
  public DateTime? End { get; private set; }

  /// <summary>
  /// Initializes a new instance of the Period class which validates if the start Period time &gt; end Period time.
  /// </summary>
  public Period(DateTime start, DateTime? end)
  {
    this.Start = start.Date;
    this.End = end;
    if (this.End.HasValue && this.Start > this.End.Value)
      throw new ArgumentException(PXMessages.LocalizeFormatNoPrefix("The dates are invalid. The end date cannot be earlier than the start date.", Array.Empty<object>()));
  }
}
