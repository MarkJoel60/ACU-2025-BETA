// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Scheduler
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// A specialized utility class responsible for generation
/// of schedules given the schedule parameters.
/// </summary>
public class Scheduler
{
  private PXGraph _graph;
  private IFinPeriodRepository _finPeriodRepository;

  public Scheduler(PXGraph graph, IFinPeriodRepository finPeriodRepository = null)
  {
    this._graph = graph;
    this._finPeriodRepository = finPeriodRepository ?? this._graph.GetService<IFinPeriodRepository>();
  }

  public virtual IEnumerable<ScheduleDet> MakeSchedule(
    Schedule scheduleParameters,
    short numberOccurrences,
    DateTime? runDate = null)
  {
    runDate = runDate ?? this._graph.Accessinfo.BusinessDate;
    List<ScheduleDet> scheduleDetList = new List<ScheduleDet>();
    DateTime? nullable1 = scheduleParameters.NextRunDate;
    if (!nullable1.HasValue)
    {
      nullable1 = scheduleParameters.LastRunDate;
      if (!nullable1.HasValue)
      {
        scheduleParameters.NextRunDate = scheduleParameters.StartDate;
        goto label_5;
      }
    }
    nullable1 = scheduleParameters.NextRunDate;
    if (!nullable1.HasValue)
      scheduleParameters.NextRunDate = scheduleParameters.LastRunDate;
label_5:
    int num1 = 0;
    short? nullable2 = scheduleParameters.RunCntr;
    short valueOrDefault = nullable2.GetValueOrDefault();
    ScheduleDet occurrence;
    DateTime dateTime1;
    do
    {
      occurrence = new ScheduleDet();
      bool? nullable3;
      int? nullable4;
      int? nullable5;
      DateTime? nullable6;
      do
      {
        switch (scheduleParameters.ScheduleType)
        {
          case "D":
            occurrence.ScheduledDate = scheduleParameters.NextRunDate;
            Schedule schedule1 = scheduleParameters;
            nullable1 = scheduleParameters.NextRunDate;
            dateTime1 = nullable1.Value;
            ref DateTime local1 = ref dateTime1;
            nullable2 = scheduleParameters.DailyFrequency;
            double num2 = (double) nullable2.Value;
            DateTime? nullable7 = new DateTime?(local1.AddDays(num2));
            schedule1.NextRunDate = nullable7;
            break;
          case "W":
            Scheduler.CalcRunDatesWeekly(scheduleParameters, occurrence);
            break;
          case "P":
            try
            {
              switch (scheduleParameters.PeriodDateSel)
              {
                case "S":
                  occurrence.ScheduledDate = new DateTime?(this._finPeriodRepository.PeriodStartDate(this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0)), new int?(0)));
                  Schedule schedule2 = scheduleParameters;
                  IFinPeriodRepository periodRepository1 = this._finPeriodRepository;
                  IFinPeriodRepository periodRepository2 = this._finPeriodRepository;
                  string periodIdFromDate1 = this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0));
                  nullable2 = scheduleParameters.PeriodFrequency;
                  int offset1 = (int) nullable2.Value;
                  int? organizationID1 = new int?(0);
                  string offsetPeriodId1 = periodRepository2.GetOffsetPeriodId(periodIdFromDate1, offset1, organizationID1);
                  int? organizationID2 = new int?(0);
                  DateTime? nullable8 = new DateTime?(periodRepository1.PeriodStartDate(offsetPeriodId1, organizationID2));
                  schedule2.NextRunDate = nullable8;
                  break;
                case "E":
                  occurrence.ScheduledDate = new DateTime?(this._finPeriodRepository.PeriodEndDate(this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0)), new int?(0)));
                  Schedule schedule3 = scheduleParameters;
                  IFinPeriodRepository periodRepository3 = this._finPeriodRepository;
                  IFinPeriodRepository periodRepository4 = this._finPeriodRepository;
                  string periodIdFromDate2 = this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0));
                  nullable2 = scheduleParameters.PeriodFrequency;
                  int offset2 = (int) nullable2.Value;
                  int? organizationID3 = new int?(0);
                  string offsetPeriodId2 = periodRepository4.GetOffsetPeriodId(periodIdFromDate2, offset2, organizationID3);
                  int? organizationID4 = new int?(0);
                  DateTime? nullable9 = new DateTime?(periodRepository3.PeriodEndDate(offsetPeriodId2, organizationID4));
                  schedule3.NextRunDate = nullable9;
                  break;
                case "D":
                  ScheduleDet scheduleDet = occurrence;
                  dateTime1 = this._finPeriodRepository.PeriodStartDate(this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0)), new int?(0));
                  ref DateTime local2 = ref dateTime1;
                  nullable2 = scheduleParameters.PeriodFixedDay;
                  double num3 = (double) ((int) nullable2.Value - 1);
                  DateTime? nullable10 = new DateTime?(local2.AddDays(num3));
                  scheduleDet.ScheduledDate = nullable10;
                  dateTime1 = occurrence.ScheduledDate.Value;
                  if (dateTime1.CompareTo(this._finPeriodRepository.PeriodEndDate(this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0)), new int?(0))) > 0)
                    occurrence.ScheduledDate = new DateTime?(this._finPeriodRepository.PeriodEndDate(this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0)), new int?(0)));
                  DateTime? nextRunDate = scheduleParameters.NextRunDate;
                  Schedule schedule4 = scheduleParameters;
                  IFinPeriodRepository periodRepository5 = this._finPeriodRepository;
                  IFinPeriodRepository periodRepository6 = this._finPeriodRepository;
                  string periodIdFromDate3 = this._finPeriodRepository.GetPeriodIDFromDate(scheduleParameters.NextRunDate, new int?(0));
                  nullable2 = scheduleParameters.PeriodFrequency;
                  int offset3 = (int) nullable2.Value;
                  int? organizationID5 = new int?(0);
                  string offsetPeriodId3 = periodRepository6.GetOffsetPeriodId(periodIdFromDate3, offset3, organizationID5);
                  int? organizationID6 = new int?(0);
                  dateTime1 = periodRepository5.PeriodStartDate(offsetPeriodId3, organizationID6);
                  ref DateTime local3 = ref dateTime1;
                  nullable2 = scheduleParameters.PeriodFixedDay;
                  double num4 = (double) ((int) nullable2.Value - 1);
                  DateTime? nullable11 = new DateTime?(local3.AddDays(num4));
                  schedule4.NextRunDate = nullable11;
                  nullable1 = scheduleParameters.NextRunDate;
                  dateTime1 = nullable1.Value;
                  ref DateTime local4 = ref dateTime1;
                  IFinPeriodRepository periodRepository7 = this._finPeriodRepository;
                  IFinPeriodRepository periodRepository8 = this._finPeriodRepository;
                  string periodIdFromDate4 = this._finPeriodRepository.GetPeriodIDFromDate(nextRunDate, new int?(0));
                  nullable2 = scheduleParameters.PeriodFrequency;
                  int offset4 = (int) nullable2.Value;
                  int? organizationID7 = new int?(0);
                  string offsetPeriodId4 = periodRepository8.GetOffsetPeriodId(periodIdFromDate4, offset4, organizationID7);
                  int? organizationID8 = new int?(0);
                  DateTime dateTime2 = periodRepository7.PeriodEndDate(offsetPeriodId4, organizationID8);
                  if (local4.CompareTo(dateTime2) > 0)
                  {
                    Schedule schedule5 = scheduleParameters;
                    IFinPeriodRepository periodRepository9 = this._finPeriodRepository;
                    IFinPeriodRepository periodRepository10 = this._finPeriodRepository;
                    string periodIdFromDate5 = this._finPeriodRepository.GetPeriodIDFromDate(nextRunDate, new int?(0));
                    nullable2 = scheduleParameters.PeriodFrequency;
                    int offset5 = (int) nullable2.Value;
                    int? organizationID9 = new int?(0);
                    string offsetPeriodId5 = periodRepository10.GetOffsetPeriodId(periodIdFromDate5, offset5, organizationID9);
                    int? organizationID10 = new int?(0);
                    DateTime? nullable12 = new DateTime?(periodRepository9.PeriodEndDate(offsetPeriodId5, organizationID10));
                    schedule5.NextRunDate = nullable12;
                    break;
                  }
                  break;
              }
            }
            catch (PXFinPeriodException ex)
            {
              if (occurrence.ScheduledDate.HasValue)
              {
                nullable3 = scheduleParameters.NoRunLimit;
                bool flag = false;
                if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
                {
                  short? runCntr = scheduleParameters.RunCntr;
                  nullable4 = runCntr.HasValue ? new int?((int) runCntr.GetValueOrDefault() + 1) : new int?();
                  nullable2 = scheduleParameters.RunLimit;
                  nullable5 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
                  if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue && scheduleParameters.NextRunDate.HasValue)
                  {
                    scheduleParameters.NextRunDate = new DateTime?();
                    break;
                  }
                }
              }
              scheduleParameters.RunCntr = new short?(valueOrDefault);
              throw;
            }
            break;
          case "M":
            switch (scheduleParameters.MonthlyDaySel)
            {
              case "D":
                ScheduleDet scheduleDet1 = occurrence;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int year1 = (int) (short) dateTime1.Year;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int month1 = (int) (short) dateTime1.Month;
                nullable2 = scheduleParameters.MonthlyOnDay;
                int day = (int) nullable2.Value;
                DateTime? nullable13 = new DateTime?((DateTime) new PXDateTime(year1, month1, day));
                scheduleDet1.ScheduledDate = nullable13;
                Schedule schedule6 = scheduleParameters;
                DateTime Date = occurrence.ScheduledDate.Value;
                nullable2 = scheduleParameters.MonthlyFrequency;
                int Months = (int) nullable2.Value;
                nullable2 = scheduleParameters.MonthlyOnDay;
                int Day = (int) nullable2.Value;
                DateTime? nullable14 = new DateTime?(PXDateTime.DatePlusMonthSetDay(Date, Months, Day));
                schedule6.NextRunDate = nullable14;
                break;
              case "W":
                ScheduleDet scheduleDet2 = occurrence;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int year2 = (int) (short) dateTime1.Year;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int month2 = (int) (short) dateTime1.Month;
                nullable2 = scheduleParameters.MonthlyOnWeek;
                int Week1 = (int) nullable2.Value;
                nullable2 = scheduleParameters.MonthlyOnDayOfWeek;
                int DayOfWeek1 = (int) nullable2.Value;
                DateTime? nullable15 = new DateTime?(PXDateTime.MakeDayOfWeek((short) year2, (short) month2, (short) Week1, (short) DayOfWeek1));
                scheduleDet2.ScheduledDate = nullable15;
                Schedule schedule7 = scheduleParameters;
                dateTime1 = occurrence.ScheduledDate.Value;
                int year3 = dateTime1.Year;
                dateTime1 = occurrence.ScheduledDate.Value;
                int month3 = dateTime1.Month;
                PXDateTime pxDateTime = new PXDateTime(year3, month3, 1);
                nullable2 = scheduleParameters.MonthlyFrequency;
                int months = (int) nullable2.Value;
                DateTime? nullable16 = new DateTime?(pxDateTime.AddMonths(months));
                schedule7.NextRunDate = nullable16;
                Schedule schedule8 = scheduleParameters;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int year4 = (int) (short) dateTime1.Year;
                nullable1 = scheduleParameters.NextRunDate;
                dateTime1 = nullable1.Value;
                int month4 = (int) (short) dateTime1.Month;
                nullable2 = scheduleParameters.MonthlyOnWeek;
                int Week2 = (int) nullable2.Value;
                nullable2 = scheduleParameters.MonthlyOnDayOfWeek;
                int DayOfWeek2 = (int) nullable2.Value;
                DateTime? nullable17 = new DateTime?(PXDateTime.MakeDayOfWeek((short) year4, (short) month4, (short) Week2, (short) DayOfWeek2));
                schedule8.NextRunDate = nullable17;
                break;
            }
            break;
          default:
            throw new PXException();
        }
        nullable1 = occurrence.ScheduledDate;
        nullable6 = scheduleParameters.LastRunDate;
      }
      while ((nullable1.HasValue == nullable6.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable6.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0);
      if (occurrence.ScheduledDate.HasValue)
      {
        dateTime1 = occurrence.ScheduledDate.Value;
        if (dateTime1.CompareTo((object) scheduleParameters.StartDate) >= 0)
        {
          nullable3 = scheduleParameters.NoEndDate;
          if (!nullable3.Value)
          {
            dateTime1 = occurrence.ScheduledDate.Value;
            if (dateTime1.CompareTo((object) scheduleParameters.EndDate) > 0)
              goto label_46;
          }
          dateTime1 = occurrence.ScheduledDate.Value;
          if (dateTime1.CompareTo((object) runDate) <= 0)
          {
            nullable3 = scheduleParameters.NoRunLimit;
            if (!nullable3.Value)
            {
              nullable2 = scheduleParameters.RunCntr;
              nullable5 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
              nullable2 = scheduleParameters.RunLimit;
              nullable4 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
              if (!(nullable5.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue))
                goto label_46;
            }
            try
            {
              occurrence.ScheduledPeriod = this._finPeriodRepository.GetPeriodIDFromDate(occurrence.ScheduledDate, new int?(0));
            }
            catch (PXFinPeriodException ex)
            {
              scheduleParameters.RunCntr = new short?(valueOrDefault);
              throw;
            }
            scheduleDetList.Add(occurrence);
            Schedule schedule9 = scheduleParameters;
            nullable2 = schedule9.RunCntr;
            short? nullable18 = nullable2;
            schedule9.RunCntr = nullable18.HasValue ? new short?((short) ((int) nullable18.GetValueOrDefault() + 1)) : new short?();
            nullable3 = scheduleParameters.NoRunLimit;
            if (!nullable3.Value)
            {
              nullable2 = scheduleParameters.RunCntr;
              nullable4 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
              nullable2 = scheduleParameters.RunLimit;
              nullable5 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
              if (nullable4.GetValueOrDefault() >= nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue)
                goto label_42;
            }
            nullable6 = scheduleParameters.NextRunDate;
            if (nullable6.HasValue)
            {
              nullable3 = scheduleParameters.NoEndDate;
              if (!nullable3.Value)
              {
                nullable6 = scheduleParameters.NextRunDate;
                dateTime1 = nullable6.Value;
                if (dateTime1.CompareTo((object) scheduleParameters.EndDate) <= 0)
                  goto label_43;
              }
              else
                goto label_43;
            }
            else
              goto label_43;
label_42:
            scheduleParameters.Active = new bool?(false);
            Schedule schedule10 = scheduleParameters;
            nullable6 = new DateTime?();
            DateTime? nullable19 = nullable6;
            schedule10.NextRunDate = nullable19;
label_43:
            nullable6 = scheduleParameters.NextRunDate;
            if (nullable6.HasValue)
            {
              if (++num1 >= (int) numberOccurrences)
                return (IEnumerable<ScheduleDet>) scheduleDetList;
              continue;
            }
            goto label_49;
          }
        }
      }
label_46:
      dateTime1 = occurrence.ScheduledDate.Value;
    }
    while (dateTime1.CompareTo((object) scheduleParameters.StartDate) < 0);
    if (occurrence.ScheduledDate.HasValue)
      scheduleParameters.NextRunDate = occurrence.ScheduledDate;
label_49:
    return (IEnumerable<ScheduleDet>) scheduleDetList;
  }

  private static void CalcRunDatesWeekly(Schedule schedule, ScheduleDet occurrence)
  {
    DateTime? nullable1 = schedule.NextRunDate;
    DateTime? nullable2 = schedule.NextRunDate;
    DateTime? nullable3 = new DateTime?();
    DateTime? nullable4;
    DateTime? nullable5;
    do
    {
      DayOfWeek dayOfWeek = nullable2.Value.DayOfWeek;
      if (schedule.WeeklyOnDay1.GetValueOrDefault() && dayOfWeek == DayOfWeek.Sunday || schedule.WeeklyOnDay2.GetValueOrDefault() && dayOfWeek == DayOfWeek.Monday || schedule.WeeklyOnDay3.GetValueOrDefault() && dayOfWeek == DayOfWeek.Tuesday || schedule.WeeklyOnDay4.GetValueOrDefault() && dayOfWeek == DayOfWeek.Wednesday || schedule.WeeklyOnDay5.GetValueOrDefault() && dayOfWeek == DayOfWeek.Thursday || schedule.WeeklyOnDay6.GetValueOrDefault() && dayOfWeek == DayOfWeek.Friday)
      {
        if (!nullable3.HasValue)
          nullable3 = nullable2;
        else
          nullable1 = nullable2;
      }
      else if (dayOfWeek == DayOfWeek.Saturday)
      {
        if (schedule.WeeklyOnDay7.GetValueOrDefault())
        {
          if (!nullable3.HasValue)
            nullable3 = nullable2;
          else
            nullable1 = nullable2;
        }
        nullable4 = nullable1;
        nullable5 = schedule.NextRunDate;
        if ((nullable4.HasValue == nullable5.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable2 = new DateTime?(nullable2.Value.AddDays((double) (7 * ((int) schedule.WeeklyFrequency.Value - 1) + 1)));
          goto label_13;
        }
      }
      nullable2 = new DateTime?(nullable2.Value.AddDays(1.0));
label_13:
      nullable5 = nullable1;
      nullable4 = schedule.NextRunDate;
    }
    while ((nullable5.HasValue == nullable4.HasValue ? (nullable5.HasValue ? (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0);
    schedule.NextRunDate = nullable1;
    occurrence.ScheduledDate = nullable3;
  }

  public DateTime? GetNextRunDate(Schedule schedule)
  {
    bool? active = schedule.Active;
    bool flag = false;
    if (active.GetValueOrDefault() == flag & active.HasValue)
      return new DateTime?();
    Schedule copy = PXCache<Schedule>.CreateCopy(schedule);
    DateTime? nextRunDate = copy.LastRunDate;
    if (nextRunDate.HasValue)
    {
      if (copy.Active.GetValueOrDefault())
      {
        nextRunDate = copy.NextRunDate;
        if (!nextRunDate.HasValue)
          goto label_6;
      }
      return copy.NextRunDate;
    }
label_6:
    Schedule schedule1 = copy;
    nextRunDate = new DateTime?();
    DateTime? nullable = nextRunDate;
    schedule1.NextRunDate = nullable;
    try
    {
      DateTime? businessDate = this._graph.Accessinfo.BusinessDate;
      this._graph.Accessinfo.BusinessDate = copy.NoEndDate.GetValueOrDefault() ? new DateTime?(DateTime.MaxValue) : copy.EndDate;
      try
      {
        Scheduler scheduler = new Scheduler(this._graph);
        Schedule scheduleParameters = copy;
        nextRunDate = new DateTime?();
        DateTime? runDate = nextRunDate;
        IEnumerable<ScheduleDet> source = scheduler.MakeSchedule(scheduleParameters, (short) 1, runDate);
        if (source.Any<ScheduleDet>())
        {
          nextRunDate = source.First<ScheduleDet>().ScheduledDate;
          return nextRunDate;
        }
      }
      finally
      {
        this._graph.Accessinfo.BusinessDate = businessDate;
      }
    }
    catch (PXFinPeriodException ex) when (schedule.NextRunDate.HasValue)
    {
      ((PXCache) GraphHelper.Caches<Schedule>(this._graph)).RaiseExceptionHandling<Schedule.nextRunDate>((object) schedule, (object) schedule.NextRunDate, (Exception) new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 2));
      return schedule.NextRunDate;
    }
    return copy.NextRunDate;
  }
}
