// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.TimeRange
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public readonly struct TimeRange(TimeSpan start, TimeSpan end) : IEquatable<TimeRange>
{
  public static readonly TimeRange Zero = new TimeRange(TimeSpan.Zero, TimeSpan.Zero);

  public TimeSpan Start { get; } = start;

  public TimeSpan End { get; } = end;

  public TimeSpan Duration => this.End - this.Start;

  public bool IsWithinRange(TimeSpan time) => time >= this.Start && time < this.End;

  public bool IsWithinRange(TimeRange other)
  {
    return this.IsWithinRange(other.Start) && this.IsWithinRange(other.End);
  }

  public static TimeSpan GetDuration(IEnumerable<TimeRange> ranges)
  {
    return new TimeSpan(ranges.Select<TimeRange, TimeSpan>((Func<TimeRange, TimeSpan>) (d => d.Duration)).Sum<TimeSpan>((Func<TimeSpan, long>) (r => r.Ticks)));
  }

  public bool IntersectsWith(TimeRange other)
  {
    return this.IsWithinRange(other.Start) || other.IsWithinRange(this.End);
  }

  public TimeRange GetIntersection(TimeRange other)
  {
    if (!this.IntersectsWith(other))
      return TimeRange.Zero;
    TimeSpan timeSpan = this.Start;
    long ticks1 = timeSpan.Ticks;
    timeSpan = other.Start;
    long ticks2 = timeSpan.Ticks;
    TimeSpan start = new TimeSpan(Math.Max(ticks1, ticks2));
    timeSpan = this.End;
    long ticks3 = timeSpan.Ticks;
    timeSpan = other.End;
    long ticks4 = timeSpan.Ticks;
    TimeSpan end = new TimeSpan(Math.Min(ticks3, ticks4));
    return new TimeRange(start, end);
  }

  public TimeRange MergeWith(TimeRange other)
  {
    TimeSpan timeSpan = this.Start;
    long ticks1 = timeSpan.Ticks;
    timeSpan = other.Start;
    long ticks2 = timeSpan.Ticks;
    TimeSpan start = new TimeSpan(Math.Min(ticks1, ticks2));
    timeSpan = this.End;
    long ticks3 = timeSpan.Ticks;
    timeSpan = other.End;
    long ticks4 = timeSpan.Ticks;
    TimeSpan end = new TimeSpan(Math.Max(ticks3, ticks4));
    return new TimeRange(start, end);
  }

  public override string ToString() => $"{this.Start} - {this.End}, Duration: {this.Duration}";

  public bool Equals(TimeRange other)
  {
    return this.Start.Equals(other.Start) && this.End.Equals(other.End);
  }

  public override bool Equals(object obj) => obj is TimeRange other && this.Equals(other);

  public override int GetHashCode()
  {
    TimeSpan timeSpan = this.Start;
    int num = timeSpan.GetHashCode() * 397;
    timeSpan = this.End;
    int hashCode = timeSpan.GetHashCode();
    return num ^ hashCode;
  }

  public static bool operator ==(TimeRange left, TimeRange right) => left.Equals(right);

  public static bool operator !=(TimeRange left, TimeRange right) => !left.Equals(right);
}
