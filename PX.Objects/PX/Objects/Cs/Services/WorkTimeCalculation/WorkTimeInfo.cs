// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.WorkTimeInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public readonly struct WorkTimeInfo(int workdays, int hours, int minutes) : IEquatable<WorkTimeInfo>
{
  public static readonly WorkTimeInfo Empty;

  public static WorkTimeInfo FromWorkTimeSpan(WorkTimeSpan workTimeSpan)
  {
    return new WorkTimeInfo(workTimeSpan.RoundWorkdays, workTimeSpan.RoundHours, workTimeSpan.RoundMinutes);
  }

  public int Workdays { get; } = workdays;

  public int Hours { get; } = hours;

  public int Minutes { get; } = minutes;

  public bool Equals(WorkTimeInfo other)
  {
    return this.Workdays == other.Workdays && this.Hours == other.Hours && this.Minutes == other.Minutes;
  }

  public override bool Equals(object obj) => obj is WorkTimeInfo other && this.Equals(other);

  public override int GetHashCode() => (this.Workdays * 397 ^ this.Hours) * 397 ^ this.Minutes;
}
