// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.WorkTimeCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

internal class WorkTimeCalculator : IWorkTimeCalculator
{
  private readonly CalendarInfo _calendar;

  public WorkTimeCalculator(CalendarInfo calendarInfo) => this._calendar = calendarInfo;

  public bool IsValid
  {
    get => this.DoesCalendarHaveCorrectWorkdayHours() && this.DoesCalendarHaveWorkingDays();
  }

  public void Validate()
  {
    if (!this.DoesCalendarHaveWorkingDays())
      throw new PXInvalidOperationException("The calendar {0} is not found.", new object[1]
      {
        (object) this._calendar.CalendarID
      });
    if (!this.DoesCalendarHaveCorrectWorkdayHours())
      throw new PXInvalidOperationException("The calendar {0} does not have working days.", new object[1]
      {
        (object) this._calendar.CalendarID
      });
  }

  public WorkTimeSpan ToWorkTimeSpan(TimeSpan timeSpan)
  {
    this.Validate();
    return new WorkTimeSpan(this._calendar.WorkdayHours, timeSpan);
  }

  public WorkTimeSpan ToWorkTimeSpan(WorkTimeInfo workTimeInfo)
  {
    this.Validate();
    return WorkTimeSpan.FromWorkdays(this._calendar.WorkdayHours, workTimeInfo.Workdays, workTimeInfo.Hours, workTimeInfo.Minutes);
  }

  public DateTimeInfo AddWorkTime(DateTimeInfo startDateTime, WorkTimeSpan workTimeDiff)
  {
    this.Validate();
    this.AssertWorkTimeSpanIsPositive(workTimeDiff);
    WorkTimeCalculator.WorkTimeAdder workTimeAdder = new WorkTimeCalculator.WorkTimeAdder(this._calendar, startDateTime, workTimeDiff);
    while (!workTimeAdder.IsFinished)
    {
      workTimeAdder.MoveToClosesWorkday();
      workTimeAdder.CalculateCurrentWorkday();
    }
    return workTimeAdder.ResultDateTimeInfo.ToTimeZone(startDateTime.TimeZoneInfo);
  }

  private bool DoesCalendarHaveWorkingDays()
  {
    return this._calendar.DaysOfWeek.Values.Any<DayOfWeekInfo>((Func<DayOfWeekInfo, bool>) (d => d.IsWorkingDay));
  }

  private bool DoesCalendarHaveCorrectWorkdayHours()
  {
    double workdayHours = this._calendar.WorkdayHours;
    return workdayHours > 0.0 && workdayHours <= 24.0;
  }

  private void AssertWorkTimeSpanIsPositive(WorkTimeSpan workTime)
  {
    if (workTime.TimeSpan.Ticks < 0L)
      throw new PXNotSupportedException("Negative working time");
  }

  private static TimeSpan ToUtc(DateTime date, TimeSpan time, PXTimeZoneInfo originalTimeZone)
  {
    return originalTimeZone == PXTimeZoneInfo.Invariant ? time : PXTimeZoneInfo.ConvertTimeToUtc(date.Add(time), originalTimeZone) - date;
  }

  private static TimeSpan FromUtc(DateTime date, TimeSpan time, PXTimeZoneInfo targetTimeZone)
  {
    return targetTimeZone == PXTimeZoneInfo.Invariant ? time : PXTimeZoneInfo.ConvertTimeFromUtc(date.Add(time), targetTimeZone) - date;
  }

  private static TimeRange ToUtc(
    DateTime date,
    TimeRange timeRange,
    PXTimeZoneInfo originalTimeZone)
  {
    return new TimeRange(WorkTimeCalculator.ToUtc(date, timeRange.Start, originalTimeZone), WorkTimeCalculator.ToUtc(date, timeRange.End, originalTimeZone));
  }

  private struct WorkTimeAdder
  {
    private readonly CalendarInfo _calendar;
    private TimeSpan _time;
    private WorkTimeCalculator.DayInfo _day;
    private TimeSpan _remainTime;

    public WorkTimeAdder(CalendarInfo calendar, DateTimeInfo startDateTime, WorkTimeSpan timeToAdd)
    {
      this._calendar = calendar;
      DateTimeInfo utc = startDateTime.ToUtc();
      DateTime date = startDateTime.ToTimeZone(calendar.TimeZone).DateTime.Date;
      this._time = utc.DateTime - date;
      this._day = WorkTimeCalculator.DayInfo.ConvertFrom(this._calendar.TimeZone, date, this._calendar.DaysOfWeek[date.DayOfWeek], this._calendar.TryFindExceptionForDate(date));
      this._remainTime = timeToAdd.TimeSpan;
    }

    public DateTime ResultDateTime
    {
      get
      {
        if (!this.IsFinished)
          throw new InvalidOperationException("ResultDateTime is not calculated.");
        return this._day.Date.Add(WorkTimeCalculator.FromUtc(this._day.Date, this._time, this._calendar.TimeZone));
      }
    }

    public DateTimeInfo ResultDateTimeInfo
    {
      get => new DateTimeInfo(this._calendar.TimeZone, this.ResultDateTime);
    }

    public bool IsFinished => this._remainTime <= TimeSpan.Zero;

    public void CalculateCurrentWorkday()
    {
      if (this._remainTime <= TimeSpan.Zero || !this._day.IsWorkingDay)
        return;
      this.MoveToWorkTime();
      if (this.IsCurrentDayOver())
        return;
      TimeSpan remainWorkTime = this.GetRemainWorkTime();
      if (this._remainTime >= remainWorkTime)
      {
        this._time = this.GetEndOfDay();
        this._remainTime -= remainWorkTime;
      }
      else
        this.MoveToFinalTime();
    }

    public void MoveToClosesWorkday()
    {
      if (this._remainTime <= TimeSpan.Zero || this._day.IsWorkingDay && !this.IsCurrentDayOver())
        return;
      DateTime date = this._day.Date;
      DayOfWeekInfo dayOfWeek;
      CalendarExceptionInfo? exceptionForDate;
      do
      {
        date = date.AddDays(1.0);
        dayOfWeek = this._calendar.DaysOfWeek[date.DayOfWeek];
        exceptionForDate = this._calendar.TryFindExceptionForDate(date);
      }
      while ((exceptionForDate.HasValue ? (!exceptionForDate.GetValueOrDefault().IsWorkingDay ? 1 : 0) : (!dayOfWeek.IsWorkingDay ? 1 : 0)) != 0);
      this._day = WorkTimeCalculator.DayInfo.ConvertFrom(this._calendar.TimeZone, date, dayOfWeek, exceptionForDate);
      this._time = this._day.TimeRange.Start;
    }

    private bool IsCurrentDayOver() => this._time >= this.GetEndOfDay();

    private void MoveToWorkTime()
    {
      if (this._time < this._day.TimeRange.Start)
        this._time = this._day.TimeRange.Start;
      if (this._day.BreakTimes.Count == 0)
        return;
      this._time = this.GetTimeAfterBreakTime();
    }

    private void MoveToFinalTime()
    {
      if (this._day.BreakTimes.Count != 0)
      {
        TimeSpan time = this._time;
        foreach (TimeRange timeRange in (IEnumerable<TimeRange>) this._day.BreakTimes.Where<TimeRange>((Func<TimeRange, bool>) (bt => bt.Start > time)).OrderBy<TimeRange, TimeSpan>((Func<TimeRange, TimeSpan>) (bt => bt.Start)))
        {
          TimeSpan timeSpan = timeRange.Start - this._time;
          if (this._remainTime <= timeSpan)
          {
            this._time += this._remainTime;
            this._remainTime = TimeSpan.Zero;
            return;
          }
          this._time = timeRange.End;
          this._remainTime -= timeSpan;
        }
      }
      this._time += this._remainTime;
      this._remainTime = TimeSpan.Zero;
    }

    private TimeSpan GetTimeAfterBreakTime()
    {
      TimeSpan time = this._time;
      return this._day.BreakTimes.Where<TimeRange>((Func<TimeRange, bool>) (bt => bt.Start <= time && bt.End > time)).Select<TimeRange, TimeSpan>((Func<TimeRange, TimeSpan>) (bt => bt.End)).DefaultIfEmpty<TimeSpan>(time).Max<TimeSpan>();
    }

    private TimeSpan GetRemainWorkTime()
    {
      return this._day.BreakTimes.Count == 0 ? this._day.TimeRange.End - this._time : this._day.TimeRange.End - this._time - this.GetRemainTimeBreakTimeDuration();
    }

    private TimeSpan GetRemainTimeBreakTimeDuration()
    {
      if (this._day.BreakTimes.Count == 0)
        return TimeSpan.Zero;
      TimeSpan time = this._time;
      return TimeRange.GetDuration(this._day.BreakTimes.Where<TimeRange>((Func<TimeRange, bool>) (bt => bt.Start >= time))) - this.GetBreakTimeDurationAfterDayEnd();
    }

    private TimeSpan GetBreakTimeDurationAfterDayEnd()
    {
      WorkTimeCalculator.DayInfo day = this._day;
      return TimeRange.GetDuration(this._day.BreakTimes.Where<TimeRange>((Func<TimeRange, bool>) (bt => bt.Start >= day.TimeRange.End))) + new TimeSpan(this._day.BreakTimes.Where<TimeRange>((Func<TimeRange, bool>) (bt => bt.Start < day.TimeRange.End && bt.End > day.TimeRange.End)).Select<TimeRange, TimeSpan>((Func<TimeRange, TimeSpan>) (bt => bt.End - day.TimeRange.End)).Sum<TimeSpan>((Func<TimeSpan, long>) (t => t.Ticks)));
    }

    private TimeSpan GetEndOfDay()
    {
      if (this._day.BreakTimes.Count == 0)
        return this._day.TimeRange.End;
      TimeRange timeRange = this._day.TimeRange;
      TimeSpan endOfDay = timeRange.End;
      foreach (TimeRange breakTime in (IEnumerable<TimeRange>) this._day.BreakTimes)
      {
        TimeSpan start = breakTime.Start;
        timeRange = this._day.TimeRange;
        TimeSpan end1 = timeRange.End;
        if (start < end1)
        {
          TimeSpan end2 = breakTime.End;
          timeRange = this._day.TimeRange;
          TimeSpan end3 = timeRange.End;
          if (end2 >= end3 && breakTime.Start < endOfDay)
            endOfDay = breakTime.Start;
        }
      }
      return endOfDay;
    }
  }

  private readonly struct DayInfo(
    DateTime date,
    bool isWorkingDay,
    TimeRange timeRange,
    IReadOnlyList<TimeRange> breakTimes)
  {
    public static WorkTimeCalculator.DayInfo ConvertFrom(
      PXTimeZoneInfo originalTimeZone,
      DateTime date,
      DayOfWeekInfo dayOfWeek,
      CalendarExceptionInfo? exception)
    {
      if (exception.HasValue && !exception.GetValueOrDefault().IsWorkingDay)
        return new WorkTimeCalculator.DayInfo(date, false, new TimeRange(), (IReadOnlyList<TimeRange>) Array.Empty<TimeRange>());
      TimeRange timeRange = exception.HasValue ? exception.GetValueOrDefault().TimeRange : dayOfWeek.TimeRange;
      bool isWorkingDay = exception.HasValue ? exception.GetValueOrDefault().IsWorkingDay : dayOfWeek.IsWorkingDay;
      return new WorkTimeCalculator.DayInfo(date, isWorkingDay, WorkTimeCalculator.ToUtc(date, timeRange, originalTimeZone), dayOfWeek.BreakTimes.Count == 0 ? (IReadOnlyList<TimeRange>) Array.Empty<TimeRange>() : (IReadOnlyList<TimeRange>) dayOfWeek.BreakTimes.Select<TimeRange, TimeRange>((Func<TimeRange, TimeRange>) (bt => WorkTimeCalculator.ToUtc(date, bt, originalTimeZone))).ToList<TimeRange>());
    }

    public DateTime Date { get; } = date;

    public bool IsWorkingDay { get; } = isWorkingDay;

    public TimeRange TimeRange { get; } = timeRange;

    public IReadOnlyList<TimeRange> BreakTimes { get; } = breakTimes;
  }
}
