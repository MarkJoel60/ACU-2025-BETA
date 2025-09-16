// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsDueDateTime
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public static class TermsDueDateTime
{
  public static DateTime GetEndOfMonth(this DateTime date)
  {
    DateTime dateTime = new DateTime(date.Year, date.Month, 1);
    dateTime = dateTime.AddMonths(1);
    return dateTime.AddDays(-1.0);
  }

  public static DateTime SetDayInMonthAfter(this DateTime date, int dayNumber)
  {
    DateTime dateTime = date.AddMonths(1);
    return new DateTime(dateTime.Year, dateTime.Month, Math.Min(DateTime.DaysInMonth(dateTime.Year, dateTime.Month), dayNumber));
  }

  public static DateTime SetDateByEndOfDecade(this DateTime date)
  {
    if (date.Day <= 10)
      return new DateTime(date.Year, date.Month, 10);
    if (date.Day <= 20)
      return new DateTime(date.Year, date.Month, 20);
    int day = DateTime.DaysInMonth(date.Year, date.Month);
    return new DateTime(date.Year, date.Month, day);
  }
}
