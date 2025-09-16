// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.CalendarInfo
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
public readonly struct CalendarInfo(
  string calendarID,
  PXTimeZoneInfo timeZone,
  double workdayHours,
  IReadOnlyDictionary<DayOfWeek, DayOfWeekInfo> daysOfWeek,
  IReadOnlyCollection<CalendarExceptionInfo> exceptions)
{
  public string CalendarID { get; } = calendarID;

  public PXTimeZoneInfo TimeZone { get; } = timeZone;

  public double WorkdayHours { get; } = workdayHours;

  public IReadOnlyDictionary<DayOfWeek, DayOfWeekInfo> DaysOfWeek { get; } = daysOfWeek;

  public IReadOnlyCollection<CalendarExceptionInfo> Exceptions { get; } = exceptions;

  public CalendarExceptionInfo? TryFindExceptionForDate(DateTime date)
  {
    date = date.Date;
    foreach (CalendarExceptionInfo exception in (IEnumerable<CalendarExceptionInfo>) this.Exceptions)
    {
      if (exception.Date == date)
        return new CalendarExceptionInfo?(exception);
    }
    return new CalendarExceptionInfo?();
  }
}
