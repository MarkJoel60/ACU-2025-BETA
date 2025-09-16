// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DateHandler
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

public class DateHandler
{
  private DateTime date;

  public DateHandler() => this.date = DateTime.Now;

  public DateHandler(double date)
  {
    this.date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddMilliseconds(date);
  }

  public DateHandler(DateTime date) => this.date = date;

  public DateHandler(DateTime? date)
  {
    if (!date.HasValue)
      return;
    this.date = date.Value;
  }

  public void SetDate(DateTime date) => this.date = date;

  public DateTime GetDate() => this.date;

  public DateTime StartOfDay()
  {
    return new DateTime(this.date.Year, this.date.Month, this.date.Day, 0, 0, 0, 0);
  }

  public DateTime BeginOfNextDay()
  {
    return new DateTime(this.date.Year, this.date.Month, this.date.Day, 0, 0, 0, 0).AddDays(1.0);
  }

  public DateTime EndOfDay()
  {
    return new DateTime(this.date.Year, this.date.Month, this.date.Day, 23, 59, 59);
  }

  public DateTime SetHours(DateTime? date)
  {
    int year = this.date.Year;
    int month = this.date.Month;
    int day = this.date.Day;
    DateTime dateTime = date.Value;
    int hour = dateTime.Hour;
    dateTime = date.Value;
    int minute = dateTime.Minute;
    dateTime = date.Value;
    int second = dateTime.Second;
    dateTime = date.Value;
    int millisecond = dateTime.Millisecond;
    return new DateTime(year, month, day, hour, minute, second, millisecond);
  }

  public string GetDay() => this.date.ToString("ddd");

  public bool IsSameDate(DateTime date)
  {
    return this.date.Day == date.Day && this.date.Month == date.Month && this.date.Year == date.Year;
  }
}
