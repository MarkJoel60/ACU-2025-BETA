// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.TimeSlot
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies a Time Slot for the recurrence module.
/// </summary>
public class TimeSlot : Slot
{
  /// <summary>Gets or sets ID of the Appointment in the Database.</summary>
  public int AppointmentID { get; set; }

  /// <summary>Gets or sets ID of the Schedule in the Database.</summary>
  public int ScheduleID { get; set; }

  /// <summary>Gets or sets an additional description for the Slot.</summary>
  public string Descr { get; set; }

  /// <summary>
  /// Gets or sets the priority for the Slot the highest priority is 1.
  /// </summary>
  public int? Priority { get; set; }

  /// <summary>
  /// Gets or sets the sequence for the Slot (Routes module).
  /// </summary>
  public int? Sequence { get; set; }

  /// <summary>Gets or sets the GenerationID for the Slot.</summary>
  public int? GenerationID { get; set; }
}
