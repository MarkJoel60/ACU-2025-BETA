// Decompiled with JetBrains decompiler
// Type: PX.Common.DaylightSavingTime
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Extensions;
using System;
using System.Globalization;

#nullable disable
namespace PX.Common;

public sealed class DaylightSavingTime
{
  private readonly int \u0002;
  private readonly ITimeRegion \u000E;
  private bool \u0006;
  private bool \u0008;
  private DateTime \u0003;
  private DateTime \u000F;
  private TimeSpan \u0005;
  private TimeSpan \u0002\u2009;

  public DaylightSavingTime(int year, ITimeRegion zone)
  {
    this.\u0002 = year;
    this.\u000E = zone;
  }

  public bool IsActive
  {
    get
    {
      this.\u0002();
      return this.\u0008;
    }
  }

  /// <summary>
  /// Represents start date and time of the DST in the time zone's standard time.
  /// </summary>
  public DateTime Start
  {
    get
    {
      this.\u0002();
      return this.\u000F;
    }
  }

  /// <summary>
  /// Represents end date and time of the DST in the time zone's daylight saving time.
  /// </summary>
  public DateTime End
  {
    get
    {
      this.\u0002();
      return this.\u0003;
    }
  }

  public TimeSpan DaylightOffset
  {
    get
    {
      this.\u0002();
      return this.\u0005;
    }
  }

  public TimeSpan BaseOffset
  {
    get
    {
      this.\u0002();
      return this.\u0002\u2009;
    }
  }

  private void \u0002()
  {
    if (this.\u0006)
      return;
    TimeZoneInfo.AdjustmentRule adjustmentRule;
    if (this.\u000E != null && this.\u000E.SupportsDaylightSavingTime && (adjustmentRule = this.\u000E.GetAdjustmentRule(this.\u0002)) != null)
    {
      this.\u0005 = adjustmentRule.DaylightDelta;
      this.\u0002\u2009 = AdjustmentRuleExtensions.GetBaseUftOffsetDelta(adjustmentRule);
      this.\u0008 = this.\u0005.Ticks != 0L || this.\u0002\u2009.Ticks != 0L;
      if (this.\u0008)
      {
        this.\u000F = this.\u0002(adjustmentRule.DaylightTransitionStart, this.\u0002);
        this.\u0003 = this.\u0002(adjustmentRule.DaylightTransitionEnd, this.\u0002);
      }
    }
    this.\u0006 = true;
  }

  private DateTime \u0002(TimeZoneInfo.TransitionTime _param1, int _param2)
  {
    DateTime dateTime;
    if (_param1.IsFixedDateRule)
    {
      dateTime = new DateTime(_param2, _param1.Month, _param1.Day);
    }
    else
    {
      Calendar calendar = CultureInfo.CurrentCulture.Calendar;
      int num1 = _param1.Week * 7 - 6;
      DateTime time = new DateTime(_param2, _param1.Month, 1);
      int dayOfWeek1 = (int) calendar.GetDayOfWeek(time);
      int dayOfWeek2 = (int) _param1.DayOfWeek;
      int day = dayOfWeek1 > dayOfWeek2 ? num1 + (7 - dayOfWeek1 + dayOfWeek2) : num1 + (dayOfWeek2 - dayOfWeek1);
      int num2 = this.\u0002(calendar, _param2, _param1.Month);
      if (day > num2)
        day -= 7;
      dateTime = new DateTime(_param2, _param1.Month, day);
    }
    return dateTime.AddTicks(_param1.TimeOfDay.Ticks);
  }

  private int \u0002(Calendar _param1, int _param2, int _param3)
  {
    int year = _param2 <= _param1.MinSupportedDateTime.Year ? _param1.GetYear(_param1.MinSupportedDateTime) : _param1.GetYear(new DateTime(_param2, _param3, 1));
    return _param1.GetDaysInMonth(year, _param3);
  }
}
