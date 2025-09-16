// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.DayOfWeekInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public readonly struct DayOfWeekInfo
{
  public static DayOfWeekInfo NotWorkingDay(IReadOnlyCollection<TimeRange> breakTimes = null)
  {
    return new DayOfWeekInfo(false, TimeRange.Zero, (IReadOnlyCollection<TimeRange>) ((object) breakTimes ?? (object) Array.Empty<TimeRange>()));
  }

  private DayOfWeekInfo(
    bool isWorkingDay,
    TimeRange timeRange,
    IReadOnlyCollection<TimeRange> breakTimes)
  {
    this.IsWorkingDay = isWorkingDay;
    this.TimeRange = timeRange;
    this.BreakTimes = DayOfWeekInfo.MergeTimeRanges(breakTimes);
  }

  public DayOfWeekInfo(TimeRange timeRange, IReadOnlyCollection<TimeRange> breakTimes)
    : this(true, timeRange, breakTimes)
  {
  }

  public bool IsWorkingDay { get; }

  public TimeRange TimeRange { get; }

  public IReadOnlyCollection<TimeRange> BreakTimes { get; }

  public TimeSpan GetBreakTimeDuration()
  {
    TimeSpan zero = TimeSpan.Zero;
    foreach (TimeRange breakTime in (IEnumerable<TimeRange>) this.BreakTimes)
      zero += this.TimeRange.GetIntersection(breakTime).Duration;
    return zero;
  }

  public static IReadOnlyCollection<TimeRange> MergeTimeRanges(
    IReadOnlyCollection<TimeRange> timeRanges)
  {
    List<TimeRange> timeRangeList = new List<TimeRange>((IEnumerable<TimeRange>) timeRanges);
    timeRangeList.Sort((Comparison<TimeRange>) ((a, b) => a.Start.CompareTo(b.Start)));
    for (int index = 0; index < timeRangeList.Count - 1; ++index)
    {
      if (timeRangeList[index].IntersectsWith(timeRangeList[index + 1]))
      {
        timeRangeList[index] = timeRangeList[index].MergeWith(timeRangeList[index + 1]);
        timeRangeList.RemoveAt(index + 1);
        --index;
      }
    }
    return (IReadOnlyCollection<TimeRange>) timeRangeList;
  }
}
