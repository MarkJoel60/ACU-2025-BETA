// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CalendarHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class CalendarHelper
{
  private const int MinutesInDay = 1440;
  private CSCalendar calendar;
  private CSCalendarExceptions calendarException;
  private DateTime date;

  public CalendarHelper(CSCalendar calendar, CSCalendarExceptions calendarException, DateTime date)
  {
    this.calendar = calendar != null ? calendar : throw new ArgumentNullException(nameof (calendar));
    this.calendarException = calendarException;
    this.date = date;
  }

  public virtual DateInfo GetInfo()
  {
    if (this.calendarException != null)
      return new DateInfo(this.calendarException.WorkDay, this.calendarException.StartTime, this.calendarException.EndTime);
    switch (this.date.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        return new DateInfo(this.calendar.SunWorkDay, this.calendar.SunStartTime, this.calendar.SunEndTime);
      case DayOfWeek.Monday:
        return new DateInfo(this.calendar.MonWorkDay, this.calendar.MonStartTime, this.calendar.MonEndTime);
      case DayOfWeek.Tuesday:
        return new DateInfo(this.calendar.TueWorkDay, this.calendar.TueStartTime, this.calendar.TueEndTime);
      case DayOfWeek.Wednesday:
        return new DateInfo(this.calendar.WedWorkDay, this.calendar.WedStartTime, this.calendar.WedEndTime);
      case DayOfWeek.Thursday:
        return new DateInfo(this.calendar.ThuWorkDay, this.calendar.ThuStartTime, this.calendar.ThuEndTime);
      case DayOfWeek.Friday:
        return new DateInfo(this.calendar.FriWorkDay, this.calendar.FriStartTime, this.calendar.FriEndTime);
      case DayOfWeek.Saturday:
        return new DateInfo(this.calendar.SatWorkDay, this.calendar.SatStartTime, this.calendar.SatEndTime);
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private bool IsWorkingDay(CSCalendar calendar)
  {
    switch (this.date.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        return calendar.SunWorkDay.GetValueOrDefault();
      case DayOfWeek.Monday:
        return calendar.MonWorkDay.GetValueOrDefault();
      case DayOfWeek.Tuesday:
        return calendar.TueWorkDay.GetValueOrDefault();
      case DayOfWeek.Thursday:
        return calendar.ThuWorkDay.GetValueOrDefault();
      case DayOfWeek.Friday:
        return calendar.FriWorkDay.GetValueOrDefault();
      case DayOfWeek.Saturday:
        return calendar.SatWorkDay.GetValueOrDefault();
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public virtual void CalculateStartEndTime(out DateTime startDate, out DateTime endDate)
  {
    DateInfo info = this.GetInfo();
    if (info.IsWorkingDay)
    {
      startDate = this.date.Date + info.StartTime;
      endDate = this.date.Date + info.EndTime;
    }
    else
    {
      startDate = this.date.Date;
      endDate = this.date.Date.AddDays(1.0);
    }
  }

  public static void CalculateStartEndTime(
    PXGraph graph,
    string calendarID,
    DateTime date,
    out DateTime startDate,
    out DateTime endDate)
  {
    new CalendarHelper(PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelect<CSCalendar>.Config>.Search<CSCalendar.calendarID>(graph, (object) calendarID, Array.Empty<object>())) ?? throw new InvalidOperationException("Calendar with specified Calendar ID cannot be found."), PXResultset<CSCalendarExceptions>.op_Implicit(PXSelectBase<CSCalendarExceptions, PXSelect<CSCalendarExceptions>.Config>.Search<CSCalendarExceptions.calendarID, CSCalendarExceptions.date>(graph, (object) calendarID, (object) date, Array.Empty<object>())), date).CalculateStartEndTime(out startDate, out endDate);
  }

  public virtual TimeSpan CalculateOvertimeForOneDay(TimeSpan startTime, TimeSpan endTime)
  {
    TimeSpan overtimeForOneDay = endTime - startTime;
    DateInfo info = this.GetInfo();
    if (startTime >= info.StartTime && endTime <= info.EndTime)
      overtimeForOneDay -= endTime - startTime;
    else if ((!(startTime >= info.EndTime) || !(endTime >= info.EndTime)) && (!(startTime <= info.StartTime) || !(endTime <= info.StartTime)))
    {
      if (startTime >= info.StartTime && endTime > info.EndTime)
        overtimeForOneDay -= info.EndTime - startTime;
      else if (startTime < info.StartTime && endTime <= info.EndTime)
        overtimeForOneDay -= endTime - info.StartTime;
      else
        overtimeForOneDay -= info.EndTime - info.StartTime;
    }
    return overtimeForOneDay;
  }

  public static TimeSpan CalculateOvertime(
    PXGraph graph,
    DateTime start,
    DateTime end,
    string calendarId)
  {
    if ((end - start).TotalHours > 24.0)
      throw new Exception("Activity Duration cannot exceed 24 hours. Please split.");
    if (end < start)
      throw new Exception("End Time cannot be less than Start Time");
    CSCalendar calendar = !string.IsNullOrEmpty(calendarId) ? PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelect<CSCalendar>.Config>.Search<CSCalendar.calendarID>(graph, (object) calendarId, Array.Empty<object>())) : throw new ArgumentNullException(nameof (calendarId));
    if (calendar == null)
      throw new InvalidOperationException("Calendar with specified Calendar ID cannot be found.");
    TimeSpan overtime = new TimeSpan();
    if (start.Date == end.Date)
    {
      TimeSpan overtimeForDay = CalendarHelper.CalculateOvertimeForDay(graph, calendar, start.Date, start.TimeOfDay, end.TimeOfDay);
      overtime = overtime.Add(overtimeForDay);
    }
    else
    {
      TimeSpan overtimeForDay1 = CalendarHelper.CalculateOvertimeForDay(graph, calendar, start, start.TimeOfDay, new TimeSpan(24, 0, 0));
      overtime = overtime.Add(overtimeForDay1);
      DateTime date = start.Date.AddDays(1.0);
      while (date < end.Date)
      {
        TimeSpan overtimeForDay2 = CalendarHelper.CalculateOvertimeForDay(graph, calendar, date, date.TimeOfDay, new TimeSpan(24, 0, 0));
        overtime = overtime.Add(overtimeForDay2);
      }
      TimeSpan overtimeForDay3 = CalendarHelper.CalculateOvertimeForDay(graph, calendar, date, date.TimeOfDay, end.TimeOfDay);
      overtime = overtime.Add(overtimeForDay3);
    }
    return overtime;
  }

  private static TimeSpan CalculateOvertimeForDay(
    PXGraph graph,
    CSCalendar calendar,
    DateTime date,
    TimeSpan startTime,
    TimeSpan endTime)
  {
    CSCalendarExceptions calendarException = PXResultset<CSCalendarExceptions>.op_Implicit(PXSelectBase<CSCalendarExceptions, PXSelect<CSCalendarExceptions>.Config>.Search<CSCalendarExceptions.calendarID, CSCalendarExceptions.date>(graph, (object) calendar.CalendarID, (object) date, Array.Empty<object>()));
    return new CalendarHelper(calendar, calendarException, date).CalculateOvertimeForOneDay(startTime, endTime);
  }

  public static bool IsHoliday(PXGraph graph, string calendarID, DateTime date)
  {
    PXResult<CSCalendar, CSCalendarExceptions> pxResult = CalendarHelper.SelectCalendar(graph, calendarID, date);
    if (pxResult == null)
      return false;
    CSCalendar csCalendar = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    CSCalendarExceptions calendarExceptions = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    if (calendarExceptions.Date.HasValue)
    {
      bool? workDay = calendarExceptions.WorkDay;
      bool flag = false;
      if (workDay.GetValueOrDefault() == flag & workDay.HasValue)
        return csCalendar.IsWorkDay(date);
    }
    return false;
  }

  public static bool IsWorkDay(PXGraph graph, string calendarID, DateTime date)
  {
    PXResult<CSCalendar, CSCalendarExceptions> pxResult = CalendarHelper.SelectCalendar(graph, calendarID, date);
    if (pxResult == null)
      return true;
    CSCalendar csCalendar = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    CSCalendarExceptions calendarExceptions = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    return !calendarExceptions.Date.HasValue ? csCalendar.IsWorkDay(date) : calendarExceptions.WorkDay.GetValueOrDefault();
  }

  private static PXResult<CSCalendar, CSCalendarExceptions> SelectCalendar(
    PXGraph graph,
    string calendarID,
    DateTime date)
  {
    return (PXResult<CSCalendar, CSCalendarExceptions>) PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelectJoin<CSCalendar, LeftJoin<CSCalendarExceptions, On<CSCalendarExceptions.calendarID, Equal<CSCalendar.calendarID>, And<CSCalendarExceptions.date, Equal<Required<CSCalendarExceptions.date>>>>>, Where<CSCalendar.calendarID, Equal<Required<CSCalendar.calendarID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) date,
      (object) calendarID
    }));
  }

  public static List<DayOfWeek> GetHolydaiByWeek(
    PXGraph graph,
    string CalendarID,
    int year,
    int WeekNumber)
  {
    List<DayOfWeek> holydaiByWeek = (List<DayOfWeek>) null;
    DateTime weekStart = PXDateTimeInfo.GetWeekStart(year, WeekNumber);
    for (DateTime date = weekStart; date <= weekStart.AddDays(7.0); date = date.AddDays(1.0))
    {
      if (!CalendarHelper.IsWorkDay(graph, CalendarID, date))
        holydaiByWeek.Add(date.DayOfWeek);
    }
    return holydaiByWeek;
  }

  public static Decimal GetHoursWorkedOnDay(CSCalendar calendar, DayOfWeek dayOfWeek)
  {
    return new CalendarHelper.CalendarDayInfo(calendar, dayOfWeek).GetHoursWorkedOnDay();
  }

  public static int GetMinutesWorkedOnDay(CSCalendar calendar, DayOfWeek dayOfWeek)
  {
    return new CalendarHelper.CalendarDayInfo(calendar, dayOfWeek).GetMinutesWorkedOnDay();
  }

  public static int? GetMinutesFromExceptionOnDay(PXGraph graph, string calendarID, DateTime date)
  {
    PXResult<CSCalendar, CSCalendarExceptions> pxResult = CalendarHelper.SelectCalendar(graph, calendarID, date);
    if (pxResult == null)
      return new int?();
    CSCalendarExceptions calendarExceptions = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    DateTime? nullable = calendarExceptions.StartTime;
    if (nullable.HasValue)
    {
      nullable = calendarExceptions.EndTime;
      if (nullable.HasValue)
      {
        nullable = calendarExceptions.EndTime;
        DateTime dateTime1 = nullable.Value;
        nullable = calendarExceptions.StartTime;
        DateTime dateTime2 = nullable.Value;
        return new int?((int) ((dateTime1 - dateTime2).TotalMinutes - (double) calendarExceptions.UnpaidTime.GetValueOrDefault()));
      }
    }
    return new int?();
  }

  public static double GetMaxPossibleUnpaidBreak(CSCalendar calendar, DayOfWeek dayOfWeek)
  {
    return new CalendarHelper.CalendarDayInfo(calendar, dayOfWeek).GetMaxPossibleUnpaidBreak();
  }

  public static void EnsureUnpaidTimeValid(CSCalendar calendar)
  {
    if (calendar == null)
      return;
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Sunday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Monday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Tuesday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Wednesday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Thursday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Friday);
    CalendarHelper.EnsureUnpaidTimeValid(calendar, DayOfWeek.Saturday);
  }

  private static void EnsureUnpaidTimeValid(CSCalendar calendar, DayOfWeek dayOfWeek)
  {
    CalendarHelper.CalendarDayInfo calendarDayInfo = new CalendarHelper.CalendarDayInfo(calendar, dayOfWeek);
    if (!calendarDayInfo.UnpaidTime.HasValue)
    {
      if (!calendarDayInfo.WorkDay.GetValueOrDefault())
        return;
      CalendarHelper.SetUnpaidTime(calendar, dayOfWeek, new int?(0));
    }
    else
    {
      double num1 = calendarDayInfo.GetMaxPossibleUnpaidBreak();
      if (num1 > 1440.0)
        num1 = 1440.0;
      if (num1 < 0.0)
        num1 = 0.0;
      int? unpaidTime = calendarDayInfo.UnpaidTime;
      int num2 = 0;
      if (!(unpaidTime.GetValueOrDefault() < num2 & unpaidTime.HasValue))
      {
        unpaidTime = calendarDayInfo.UnpaidTime;
        double? nullable = unpaidTime.HasValue ? new double?((double) unpaidTime.GetValueOrDefault()) : new double?();
        double num3 = num1;
        if (!(nullable.GetValueOrDefault() > num3 & nullable.HasValue))
          return;
      }
      CalendarHelper.SetUnpaidTime(calendar, dayOfWeek, new int?((int) num1));
    }
  }

  private static void SetUnpaidTime(CSCalendar calendar, DayOfWeek dayOfWeek, int? unpaidTime)
  {
    switch (dayOfWeek)
    {
      case DayOfWeek.Sunday:
        calendar.SunUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Monday:
        calendar.MonUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Tuesday:
        calendar.TueUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Wednesday:
        calendar.WedUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Thursday:
        calendar.ThuUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Friday:
        calendar.FriUnpaidTime = unpaidTime;
        break;
      case DayOfWeek.Saturday:
        calendar.SatUnpaidTime = unpaidTime;
        break;
    }
  }

  private class CalendarDayInfo
  {
    public bool? WorkDay { get; private set; }

    public DateTime? StartTime { get; private set; }

    public DateTime? EndTime { get; private set; }

    public int? UnpaidTime { get; private set; }

    public CalendarDayInfo(CSCalendar c, DayOfWeek dayOfWeek)
    {
      switch (dayOfWeek)
      {
        case DayOfWeek.Sunday:
          this.SetDayInfo(c.SunWorkDay, c.SunStartTime, c.SunEndTime, c.SunUnpaidTime);
          break;
        case DayOfWeek.Monday:
          this.SetDayInfo(c.MonWorkDay, c.MonStartTime, c.MonEndTime, c.MonUnpaidTime);
          break;
        case DayOfWeek.Tuesday:
          this.SetDayInfo(c.TueWorkDay, c.TueStartTime, c.TueEndTime, c.TueUnpaidTime);
          break;
        case DayOfWeek.Wednesday:
          this.SetDayInfo(c.WedWorkDay, c.WedStartTime, c.WedEndTime, c.WedUnpaidTime);
          break;
        case DayOfWeek.Thursday:
          this.SetDayInfo(c.ThuWorkDay, c.ThuStartTime, c.ThuEndTime, c.ThuUnpaidTime);
          break;
        case DayOfWeek.Friday:
          this.SetDayInfo(c.FriWorkDay, c.FriStartTime, c.FriEndTime, c.FriUnpaidTime);
          break;
        case DayOfWeek.Saturday:
          this.SetDayInfo(c.SatWorkDay, c.SatStartTime, c.SatEndTime, c.SatUnpaidTime);
          break;
      }
    }

    private void SetDayInfo(
      bool? workDay,
      DateTime? startTime,
      DateTime? endTime,
      int? unpaidTime)
    {
      this.WorkDay = workDay;
      this.StartTime = startTime;
      this.EndTime = endTime;
      this.UnpaidTime = unpaidTime;
    }

    public Decimal GetHoursWorkedOnDay()
    {
      return !this.WorkDay.GetValueOrDefault() || !this.StartTime.HasValue || !this.EndTime.HasValue ? 0M : (Decimal) ((this.EndTime.Value - this.StartTime.Value).TotalHours - (double) this.UnpaidTime.GetValueOrDefault() / 60.0);
    }

    public int GetMinutesWorkedOnDay()
    {
      if (!this.WorkDay.GetValueOrDefault() || !this.StartTime.HasValue || !this.EndTime.HasValue)
        return 0;
      DateTime t1 = this.StartTime.Value;
      DateTime? nullable = this.EndTime;
      DateTime t2 = nullable.Value;
      DateTime dateTime1;
      if (DateTime.Compare(t1, t2) < 0)
      {
        nullable = this.EndTime;
        dateTime1 = nullable.Value;
      }
      else
      {
        nullable = this.EndTime;
        dateTime1 = nullable.Value.AddDays(1.0);
      }
      nullable = this.StartTime;
      DateTime dateTime2 = nullable.Value;
      return (int) (dateTime1 - dateTime2).TotalMinutes - this.UnpaidTime.GetValueOrDefault();
    }

    public double GetMaxPossibleUnpaidBreak()
    {
      return !this.WorkDay.GetValueOrDefault() || !this.StartTime.HasValue || !this.EndTime.HasValue ? 0.0 : (this.EndTime.Value - this.StartTime.Value).TotalMinutes;
    }
  }
}
