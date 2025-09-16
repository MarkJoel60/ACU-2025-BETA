// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.WorkTimeSpan
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
public readonly struct WorkTimeSpan(double hoursInWorkday, TimeSpan timeSpan) : 
  IEquatable<WorkTimeSpan>
{
  private const int MinutesInHour = 60;

  public TimeSpan TimeSpan { get; } = timeSpan;

  public double HoursInWorkday { get; } = hoursInWorkday > 0.0 && hoursInWorkday <= 24.0 ? hoursInWorkday : throw new ArgumentOutOfRangeException(nameof (hoursInWorkday), (object) hoursInWorkday, "Hours in workday must be in range (0, 24]");

  public double TotalWorkdays => this.TotalHours / this.HoursInWorkday;

  public double TotalHours => this.TimeSpan.TotalHours;

  public double TotalMinutes => this.TimeSpan.TotalMinutes;

  public double Workdays => this.TotalWorkdays;

  public double Hours => this.TotalHours % this.HoursInWorkday;

  public double Minutes => this.Hours % 1.0 * 60.0;

  public int RoundWorkdays => this.GetRoundTime().workdays;

  public int RoundHours => this.GetRoundTime().hours;

  public int RoundMinutes => this.GetRoundTime().minutes;

  private int LastHourMinutes => (int) (this.HoursInWorkday % 1.0 * 60.0);

  private (int workdays, int hours, int minutes) GetRoundTime()
  {
    int num1 = (int) Math.Round(this.Minutes, 0, MidpointRounding.AwayFromZero);
    int hours = (int) this.Hours;
    int workdays = (int) this.Workdays;
    bool flag = this.LastHourMinutes != 0 & this.Hours >= (double) (int) this.HoursInWorkday && num1 == this.LastHourMinutes;
    if (num1 < 60 && !flag)
      return (workdays, hours, num1);
    int num2 = 0;
    int num3 = hours + 1;
    if ((double) num3 >= this.HoursInWorkday)
    {
      num3 = 0;
      ++workdays;
    }
    return (workdays, num3, num2);
  }

  public WorkTimeSpan Duration() => this.TimeSpan.Ticks >= 0L ? this : this.Negate();

  public WorkTimeSpan Negate() => new WorkTimeSpan(this.HoursInWorkday, this.TimeSpan.Negate());

  public override string ToString()
  {
    return $"{this.RoundWorkdays}d {this.RoundHours}h {this.RoundMinutes}m, {this.HoursInWorkday:0.00}h/d";
  }

  public static WorkTimeSpan FromWorkdays(
    double hoursInWorkday,
    int workdays,
    int hours,
    int minutes)
  {
    return new WorkTimeSpan(hoursInWorkday, new TimeSpan(0, hours, minutes, 0, 0) + TimeSpan.FromHours((double) workdays * hoursInWorkday));
  }

  public static WorkTimeSpan FromWorkdays(double hoursInWorkday, double businessDays)
  {
    return new WorkTimeSpan(hoursInWorkday, TimeSpan.FromHours(businessDays * hoursInWorkday));
  }

  public static WorkTimeSpan FromRealDays(
    double hoursInWorkday,
    int realDays,
    int hours,
    int minutes)
  {
    return new WorkTimeSpan(hoursInWorkday, new TimeSpan(realDays, hours, minutes, 0, 0));
  }

  public static WorkTimeSpan FromRealDays(double hoursInWorkday, double realDays)
  {
    return new WorkTimeSpan(hoursInWorkday, TimeSpan.FromDays(realDays));
  }

  public static WorkTimeSpan FromHours(double hoursInWorkday, double hours)
  {
    return new WorkTimeSpan(hoursInWorkday, TimeSpan.FromHours(hours));
  }

  public static WorkTimeSpan FromMinutes(double hoursInWorkday, double minutes)
  {
    return new WorkTimeSpan(hoursInWorkday, TimeSpan.FromMinutes(minutes));
  }

  public static IEqualityComparer<WorkTimeSpan> EqualityComparer { get; } = (IEqualityComparer<WorkTimeSpan>) new WorkTimeSpan.WorkTimeSpanComparer();

  public bool Equals(WorkTimeSpan other) => WorkTimeSpan.EqualityComparer.Equals(this, other);

  public override bool Equals(object obj) => obj is WorkTimeSpan other && this.Equals(other);

  public override int GetHashCode() => WorkTimeSpan.EqualityComparer.GetHashCode(this);

  public static bool operator ==(WorkTimeSpan left, WorkTimeSpan right) => left.Equals(right);

  public static bool operator !=(WorkTimeSpan left, WorkTimeSpan right) => !left.Equals(right);

  private sealed class WorkTimeSpanComparer : IEqualityComparer<WorkTimeSpan>
  {
    public bool Equals(WorkTimeSpan x, WorkTimeSpan y)
    {
      return x.TimeSpan.Equals(y.TimeSpan) && x.HoursInWorkday.Equals(y.HoursInWorkday);
    }

    public int GetHashCode(WorkTimeSpan obj)
    {
      return obj.TimeSpan.GetHashCode() * 397 ^ obj.HoursInWorkday.GetHashCode();
    }
  }
}
