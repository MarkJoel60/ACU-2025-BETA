// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.WorkTimeRemindDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.EP;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class WorkTimeRemindDateAttribute : PXRemindDateAttribute
{
  private readonly Type _activityEmployeeBqlField;
  private int _activityEmployeeFieldOrigin;
  private PXGraph _graph;

  public WorkTimeRemindDateAttribute(
    Type isReminderOnBqlField,
    Type startDateBqlField,
    Type activityEmployeeBqlField)
    : base(isReminderOnBqlField, startDateBqlField)
  {
    this._activityEmployeeBqlField = activityEmployeeBqlField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._activityEmployeeFieldOrigin = sender.GetFieldOrdinal(sender.GetField(this._activityEmployeeBqlField));
    this._graph = sender.Graph;
  }

  protected virtual DateTime? CalcCorrectValue(PXCache sender, object row)
  {
    this._reversedRemindAt = 0.0;
    DateTime? nullable = base.CalcCorrectValue(sender, row);
    if (nullable.HasValue && sender.GetValue(row, this._isReminderOnFieldOrigin) is true)
    {
      PXResultset<CSCalendar> pxResultset = PXSelectBase<CSCalendar, PXSelectJoin<CSCalendar, InnerJoin<EPEmployee, On<EPEmployee.calendarID, Equal<CSCalendar.calendarID>>>, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Config>.Select(this._graph, new object[1]
      {
        sender.GetValue(row, this._activityEmployeeFieldOrigin)
      });
      if (pxResultset != null && pxResultset.Count != 0)
      {
        CSCalendar csCalendar = (CSCalendar) ((PXResult) pxResultset[0])[typeof (CSCalendar)];
        PXTimeZoneInfo pxTimeZoneInfo = string.IsNullOrEmpty(csCalendar.TimeZone) ? PXTimeZoneInfo.Invariant : PXTimeZoneInfo.FindSystemTimeZoneById(csCalendar.TimeZone);
        long num = 0;
        DateTime date = nullable.Value;
        switch (date.DayOfWeek)
        {
          case DayOfWeek.Sunday:
            if (csCalendar.SunWorkDay.GetValueOrDefault())
            {
              date = csCalendar.SunStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Monday:
            if (csCalendar.MonWorkDay.GetValueOrDefault())
            {
              date = csCalendar.MonStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Tuesday:
            if (csCalendar.TueWorkDay.GetValueOrDefault())
            {
              date = csCalendar.TueStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Wednesday:
            if (csCalendar.WedWorkDay.GetValueOrDefault())
            {
              date = csCalendar.WedStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Thursday:
            if (csCalendar.ThuWorkDay.GetValueOrDefault())
            {
              date = csCalendar.ThuStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Friday:
            if (csCalendar.FriWorkDay.GetValueOrDefault())
            {
              date = csCalendar.FriStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
          case DayOfWeek.Saturday:
            if (csCalendar.SatWorkDay.GetValueOrDefault())
            {
              date = csCalendar.SatStartTime.Value;
              num = date.TimeOfDay.Ticks;
              break;
            }
            break;
        }
        date = nullable.Value;
        date = date.Date;
        nullable = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(date.AddTicks(num - pxTimeZoneInfo.BaseUtcOffset.Ticks), LocaleInfo.GetTimeZone()));
      }
    }
    return nullable;
  }
}
