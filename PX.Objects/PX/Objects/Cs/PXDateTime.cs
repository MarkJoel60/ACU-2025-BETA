// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXDateTime
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public class PXDateTime
{
  protected int _day;
  protected int _month;
  protected int _year;
  protected DateTime _value;

  public static short DayOfWeekOrdinal(DayOfWeek dow)
  {
    switch (dow)
    {
      case DayOfWeek.Sunday:
        return 1;
      case DayOfWeek.Monday:
        return 2;
      case DayOfWeek.Tuesday:
        return 3;
      case DayOfWeek.Wednesday:
        return 4;
      case DayOfWeek.Thursday:
        return 5;
      case DayOfWeek.Friday:
        return 6;
      case DayOfWeek.Saturday:
        return 7;
      default:
        return 0;
    }
  }

  public static bool WeekDay(DayOfWeek dow)
  {
    switch (dow)
    {
      case DayOfWeek.Monday:
      case DayOfWeek.Tuesday:
      case DayOfWeek.Wednesday:
      case DayOfWeek.Thursday:
      case DayOfWeek.Friday:
        return true;
      default:
        return false;
    }
  }

  public static bool WeekEnd(DayOfWeek dow) => !PXDateTime.WeekDay(dow);

  public static DateTime MakeDayOfWeek(short Year, short Month, short Week, short DayOfWeek)
  {
    if (Week == (short) 5 && DayOfWeek == (short) 8)
    {
      DateTime dateTime1 = new DateTime((int) Year, (int) Month, 1);
      dateTime1 = dateTime1.AddMonths(1);
      DateTime dateTime2 = dateTime1.AddDays(-1.0);
      if (PXDateTime.WeekDay(dateTime2.DayOfWeek))
        return dateTime2;
      return PXDateTime.DayOfWeekOrdinal(dateTime2.DayOfWeek) == (short) 1 ? dateTime2.AddDays(-2.0) : dateTime2.AddDays(-1.0);
    }
    if (Week == (short) 5 && DayOfWeek == (short) 9)
    {
      DateTime dateTime3 = new DateTime((int) Year, (int) Month, 1);
      dateTime3 = dateTime3.AddMonths(1);
      DateTime dateTime4 = dateTime3.AddDays(-1.0);
      return PXDateTime.WeekEnd(dateTime4.DayOfWeek) ? dateTime4 : dateTime4.AddDays((double) (1 - (int) PXDateTime.DayOfWeekOrdinal(dateTime4.DayOfWeek)));
    }
    if (Week == (short) 5)
    {
      DateTime dateTime5 = new DateTime((int) Year, (int) Month, 1);
      dateTime5 = dateTime5.AddMonths(1);
      DateTime dateTime6 = dateTime5.AddDays(-1.0);
      return (int) PXDateTime.DayOfWeekOrdinal(dateTime6.DayOfWeek) >= (int) DayOfWeek ? dateTime6.AddDays((double) ((int) DayOfWeek - (int) PXDateTime.DayOfWeekOrdinal(dateTime6.DayOfWeek))) : dateTime6.AddDays((double) ((int) DayOfWeek - (int) PXDateTime.DayOfWeekOrdinal(dateTime6.DayOfWeek) - 7));
    }
    switch (DayOfWeek)
    {
      case 8:
        DateTime dateTime7 = new DateTime((int) Year, (int) Month, 1);
        if (PXDateTime.WeekDay(dateTime7.DayOfWeek) && Week == (short) 1)
          return dateTime7;
        DayOfWeek = (short) 2;
        break;
      case 9:
        DateTime dateTime8 = new DateTime((int) Year, (int) Month, 1);
        if (PXDateTime.WeekEnd(dateTime8.DayOfWeek) && Week == (short) 1)
          return dateTime8;
        DayOfWeek = (short) 7;
        break;
    }
    DateTime dateTime9 = new DateTime((int) Year, (int) Month, 1);
    dateTime9 = (int) PXDateTime.DayOfWeekOrdinal(dateTime9.DayOfWeek) > (int) DayOfWeek ? dateTime9.AddDays((double) (7 - ((int) PXDateTime.DayOfWeekOrdinal(dateTime9.DayOfWeek) - (int) DayOfWeek))) : dateTime9.AddDays((double) ((int) DayOfWeek - (int) PXDateTime.DayOfWeekOrdinal(dateTime9.DayOfWeek)));
    return dateTime9.AddDays((double) (7 * ((int) Week - 1)));
  }

  public static DateTime DatePlusMonthSetDay(DateTime Date, int Months, int Day)
  {
    DateTime dateTime = Date.AddMonths(Months);
    if (Day - dateTime.Day < 4)
      dateTime = (DateTime) new PXDateTime(dateTime.Year, dateTime.Month, Day);
    return dateTime;
  }

  public PXDateTime(int year, int month, int day)
  {
    this._year = year;
    this._month = month;
    this._day = day;
    try
    {
      this._value = new DateTime(year, month, day);
    }
    catch (ArgumentOutOfRangeException ex1)
    {
label_3:
      try
      {
        this._value = new DateTime(year, month, --day);
      }
      catch (ArgumentOutOfRangeException ex2)
      {
        if (day <= 28)
          throw;
        goto label_3;
      }
    }
    this._value = new DateTime(year, month, day);
  }

  public virtual DateTime AddMonths(int months)
  {
    return PXDateTime.DatePlusMonthSetDay(this._value, months, this._day);
  }

  public static implicit operator DateTime(PXDateTime item) => item._value;
}
