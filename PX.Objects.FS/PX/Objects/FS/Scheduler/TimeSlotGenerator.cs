// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.TimeSlotGenerator
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS.Scheduler;

/// <summary>
/// This class specifies the structure of the Time Slot Generation for a list of Schedules and a Period.
/// </summary>
public class TimeSlotGenerator
{
  /// <summary>
  /// Iterates for every day in the range of the period and check if the Schedules applies.
  /// </summary>
  public List<TimeSlot> GenerateCalendar(
    Period period,
    IEnumerable<Schedule> schedules,
    int? generationID = null)
  {
    List<TimeSlot> timeSlotList = new List<TimeSlot>();
    DateTime checkDate = period.Start;
    while (true)
    {
      DateTime dateTime = checkDate;
      DateTime? end = period.End;
      if ((end.HasValue ? (dateTime <= end.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        this.AddTimeSlotsForDate(checkDate, schedules, timeSlotList, generationID);
        checkDate = checkDate.AddDays(1.0);
      }
      else
        break;
    }
    return timeSlotList.OrderBy<TimeSlot, int?>((Func<TimeSlot, int?>) (a => a.Priority)).ThenBy<TimeSlot, int?>((Func<TimeSlot, int?>) (a => a.Sequence)).ThenBy<TimeSlot, DateTime>((Func<TimeSlot, DateTime>) (a => a.DateTimeBegin)).ThenBy<TimeSlot, DateTime>((Func<TimeSlot, DateTime>) (a => a.DateTimeEnd)).ToList<TimeSlot>();
  }

  /// <summary>
  /// Validates if a Schedule applies in a specific day and add every Time Slot generated to the [timeSlots] list.
  /// </summary>
  private void AddTimeSlotsForDate(
    DateTime checkDate,
    IEnumerable<Schedule> schedules,
    List<TimeSlot> timeSlots,
    int? generationID = null)
  {
    foreach (Schedule schedule in schedules)
    {
      if (schedule.OccursOnDate(checkDate) && schedule.OccursOnSeason(checkDate))
      {
        if (schedule.Priority.GetValueOrDefault() == 2)
          schedule.Sequence = this.setRouteSequence(checkDate, schedule);
        timeSlots.Add(this.GenerateTimeSlot(checkDate, schedule, generationID));
        schedule.LastGeneratedTimeSlotDate = new DateTime?(checkDate);
      }
    }
  }

  /// <summary>
  /// Generates a Time Slot using the day of the [checkDate] and the hours of the [schedule.TimeOfDayBegin] and [schedule.TimeOfDayEnd].
  /// </summary>
  private TimeSlot GenerateTimeSlot(DateTime checkDate, Schedule schedule, int? generationID = null)
  {
    TimeSlot timeSlot = new TimeSlot();
    timeSlot.Priority = schedule.Priority;
    timeSlot.Descr = string.IsNullOrEmpty(schedule.Descr) ? schedule.Name : schedule.Descr;
    timeSlot.ScheduleID = schedule.ScheduleID;
    timeSlot.Sequence = schedule.Sequence;
    timeSlot.DateTimeBegin = checkDate.Add(schedule.TimeOfDayBegin);
    timeSlot.DateTimeEnd = checkDate.Add(schedule.TimeOfDayEnd);
    timeSlot.GenerationID = generationID;
    return timeSlot;
  }

  /// <summary>
  /// Set sequence to the Schedule using the List of routes defined in the [schedule].RouteInfoList.
  /// </summary>
  private int? setRouteSequence(DateTime checkDate, Schedule schedule)
  {
    RouteInfo routeInfo = schedule.RouteInfoList.ElementAt<RouteInfo>((int) checkDate.Date.DayOfWeek);
    if (!routeInfo.RouteID.HasValue || !routeInfo.ShiftID.HasValue)
      routeInfo = schedule.RouteInfoList.Last<RouteInfo>();
    return routeInfo.Sequence;
  }

  public DateTime? GenerateNextOccurrence(
    IEnumerable<Schedule> schedules,
    DateTime fromDate,
    DateTime? expirationDate,
    out bool expired)
  {
    DateTime? nextOccurrence = new DateTime?();
    expired = false;
    DateTime date = fromDate;
    try
    {
      for (; !nextOccurrence.HasValue && (!expirationDate.HasValue || expirationDate.Value >= date); date = date.AddDays(1.0))
      {
        foreach (Schedule schedule in schedules)
        {
          if (schedule.OccursOnDate(date) && schedule.OccursOnSeason(date) && (!nextOccurrence.HasValue || nextOccurrence.Value > date))
            nextOccurrence = new DateTime?(date);
        }
      }
      if (expirationDate.HasValue && expirationDate.Value < date)
        expired = true;
      return nextOccurrence;
    }
    catch
    {
      return new DateTime?();
    }
  }
}
