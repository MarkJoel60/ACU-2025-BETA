// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.Slot
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS.Scheduler;

public class Slot
{
  /// <summary>
  /// Gets or sets date and time for the beginning of the Slot.
  /// </summary>
  public DateTime DateTimeBegin { get; set; }

  /// <summary>
  /// Gets or sets date and time for the ending of the Slot.
  /// </summary>
  public DateTime DateTimeEnd { get; set; }

  /// <summary>
  /// Gets or sets type of the Slot (Availability, Unavailability).
  /// </summary>
  public string SlotType { get; set; }
}
