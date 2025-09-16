// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.WorkTimeCalculatorProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public static class WorkTimeCalculatorProvider
{
  public static IWorkTimeCalculator GetWorkTimeCalculator(string calendarId)
  {
    Dictionary<string, IWorkTimeCalculator> calculators = PXDatabase.GetSlot<WorkTimeCalculatorProvider.WorkTimeCalendarsContainer>(typeof (WorkTimeCalculatorProvider.WorkTimeCalendarsContainer).FullName, WorkTimeCalculatorProvider.WorkTimeCalendarsContainer.Tables)?.Calculators;
    if (calculators == null)
      throw new PXInvalidOperationException("The calendar slot was not initialized.");
    IWorkTimeCalculator workTimeCalculator;
    if (calculators.TryGetValue(calendarId, out workTimeCalculator))
      return workTimeCalculator;
    throw new PXArgumentException(nameof (calendarId), "The calendar {0} is not found.", new object[1]
    {
      (object) calendarId
    });
  }

  public static CalendarInfo ConvertToCalendarInfo(
    CSCalendar calendar,
    IReadOnlyCollection<CSCalendarExceptions> exceptions,
    IReadOnlyCollection<CSCalendarBreakTime> breakTimes)
  {
    Dictionary<DayOfWeek, List<TimeRange>> dictionary = new Dictionary<DayOfWeek, List<TimeRange>>(7);
    foreach (DayOfWeek dayOfWeek1 in Enum.GetValues(typeof (DayOfWeek)).Cast<DayOfWeek>())
    {
      DayOfWeek dayOfWeek = dayOfWeek1;
      dictionary[dayOfWeek] = breakTimes.Where<CSCalendarBreakTime>((Func<CSCalendarBreakTime, bool>) (c =>
      {
        int? dayOfWeek2 = c.DayOfWeek;
        int num = (int) dayOfWeek;
        return dayOfWeek2.GetValueOrDefault() == num & dayOfWeek2.HasValue || c.DayOfWeek.GetValueOrDefault() == 10;
      })).Select<CSCalendarBreakTime, TimeRange>((Func<CSCalendarBreakTime, TimeRange>) (c => new TimeRange(WorkTimeCalculatorProvider.DateTimeToTimeSpan(c.StartTime), WorkTimeCalculatorProvider.DateTimeToTimeSpan(c.EndTime)))).OrderBy<TimeRange, TimeSpan>((Func<TimeRange, TimeSpan>) (c => c.Start)).ToList<TimeRange>();
    }
    Dictionary<DayOfWeek, DayOfWeekInfo> daysOfWeek = new Dictionary<DayOfWeek, DayOfWeekInfo>()
    {
      [DayOfWeek.Sunday] = GetDayOfWeek(calendar.SunWorkDay, calendar.SunStartTime, calendar.SunEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Sunday]),
      [DayOfWeek.Monday] = GetDayOfWeek(calendar.MonWorkDay, calendar.MonStartTime, calendar.MonEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Monday]),
      [DayOfWeek.Tuesday] = GetDayOfWeek(calendar.TueWorkDay, calendar.TueStartTime, calendar.TueEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Tuesday]),
      [DayOfWeek.Wednesday] = GetDayOfWeek(calendar.WedWorkDay, calendar.WedStartTime, calendar.WedEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Wednesday]),
      [DayOfWeek.Thursday] = GetDayOfWeek(calendar.ThuWorkDay, calendar.ThuStartTime, calendar.ThuEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Thursday]),
      [DayOfWeek.Friday] = GetDayOfWeek(calendar.FriWorkDay, calendar.FriStartTime, calendar.FriEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Friday]),
      [DayOfWeek.Saturday] = GetDayOfWeek(calendar.SatWorkDay, calendar.SatStartTime, calendar.SatEndTime, (IReadOnlyList<TimeRange>) dictionary[DayOfWeek.Saturday])
    };
    List<CalendarExceptionInfo> list = EnumerableExtensions.ToList<CalendarExceptionInfo>(exceptions.Select<CSCalendarExceptions, CalendarExceptionInfo>((Func<CSCalendarExceptions, CalendarExceptionInfo>) (e =>
    {
      DateTime date = e.Date.Value;
      TimeRange timeRange = WorkTimeCalculatorProvider.DateTimesToTimeRange(e.StartTime, e.EndTime);
      bool? workDay = e.WorkDay;
      int num = !workDay.HasValue ? 0 : (workDay.GetValueOrDefault() ? 1 : 0);
      return new CalendarExceptionInfo(date, timeRange, num != 0);
    })), exceptions.Count);
    return new CalendarInfo(calendar.CalendarID, GetCalendarTimeZone(calendar.TimeZone), (double) calendar.WorkdayTime.Value / 60.0, (IReadOnlyDictionary<DayOfWeek, DayOfWeekInfo>) daysOfWeek, (IReadOnlyCollection<CalendarExceptionInfo>) list);

    static DayOfWeekInfo GetDayOfWeek(
      bool? isWorkingDay,
      DateTime? startTime,
      DateTime? endTime,
      IReadOnlyList<TimeRange> breakTimesRanges)
    {
      return isWorkingDay.HasValue && isWorkingDay.GetValueOrDefault() ? new DayOfWeekInfo(WorkTimeCalculatorProvider.DateTimesToTimeRange(startTime, endTime), (IReadOnlyCollection<TimeRange>) breakTimesRanges) : DayOfWeekInfo.NotWorkingDay((IReadOnlyCollection<TimeRange>) breakTimesRanges);
    }

    static PXTimeZoneInfo GetCalendarTimeZone(string timeZoneId)
    {
      return PXTimeZoneInfo.FindSystemTimeZoneById(timeZoneId) ?? PXTimeZoneInfo.Invariant;
    }
  }

  public static TimeSpan DateTimeToTimeSpan(DateTime? value)
  {
    return !value.HasValue ? TimeSpan.Zero : value.GetValueOrDefault().TimeOfDay;
  }

  public static TimeRange DateTimesToTimeRange(DateTime? start, DateTime? end)
  {
    TimeSpan timeSpan1 = WorkTimeCalculatorProvider.DateTimeToTimeSpan(start);
    TimeSpan timeSpan2 = WorkTimeCalculatorProvider.DateTimeToTimeSpan(end);
    if (timeSpan2 == TimeSpan.Zero)
      timeSpan2 = TimeSpan.FromHours(24.0);
    TimeSpan end1 = timeSpan2;
    return new TimeRange(timeSpan1, end1);
  }

  internal class WorkTimeCalendarsContainer : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<string, IWorkTimeCalculator> Calculators { get; private set; }

    public static Type[] Tables
    {
      get
      {
        return new Type[3]
        {
          typeof (CSCalendar),
          typeof (CSCalendarExceptions),
          typeof (CSCalendarBreakTime)
        };
      }
    }

    public void Prefetch()
    {
      PXGraph instance = PXGraph.CreateInstance<PXGraph>();
      List<CSCalendar> list1 = this.PXSelectToList<CSCalendar>(instance);
      List<CSCalendarExceptions> list2 = this.PXSelectToList<CSCalendarExceptions>(instance);
      List<CSCalendarBreakTime> list3 = this.PXSelectToList<CSCalendarBreakTime>(instance);
      this.Calculators = new Dictionary<string, IWorkTimeCalculator>(list1.Count);
      foreach (CSCalendar csCalendar in list1)
      {
        CSCalendar calendar = csCalendar;
        List<CSCalendarExceptions> list4 = list2.Where<CSCalendarExceptions>((Func<CSCalendarExceptions, bool>) (e => e.CalendarID == calendar.CalendarID)).ToList<CSCalendarExceptions>();
        List<CSCalendarBreakTime> list5 = list3.Where<CSCalendarBreakTime>((Func<CSCalendarBreakTime, bool>) (b => b.CalendarID == calendar.CalendarID)).ToList<CSCalendarBreakTime>();
        WorkTimeCalculator workTimeCalculator = new WorkTimeCalculator(WorkTimeCalculatorProvider.ConvertToCalendarInfo(calendar, (IReadOnlyCollection<CSCalendarExceptions>) list4, (IReadOnlyCollection<CSCalendarBreakTime>) list5));
        this.Calculators[calendar.CalendarID] = (IWorkTimeCalculator) workTimeCalculator;
      }
    }

    private List<T> PXSelectToList<T>(PXGraph graph) where T : PXBqlTable, IBqlTable, new()
    {
      return GraphHelper.RowCast<T>((IEnumerable) PXSelectBase<T, PXSelectReadonly<T>.Config>.Select(graph, Array.Empty<object>())).ToList<T>();
    }
  }
}
