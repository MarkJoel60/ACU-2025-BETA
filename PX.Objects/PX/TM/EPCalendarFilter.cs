// Decompiled with JetBrains decompiler
// Type: PX.TM.EPCalendarFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using System;

#nullable enable
namespace PX.TM;

[PXHidden]
[Serializable]
public class EPCalendarFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int DASHBOARD_TYPE = 7;
  protected 
  #nullable disable
  string _Type;
  protected DateTime? _CentralDate;

  [PXDefault("Week")]
  [PXDBString]
  [PXUIField]
  [EPCalendarFilter.CalendarType]
  public virtual string Type
  {
    get => this._Type ?? "Week";
    set => this._Type = value;
  }

  [PXDBDate(PreserveTime = true)]
  [EPNowDefault]
  public virtual DateTime? CentralDate
  {
    get => this._CentralDate ?? new DateTime?(DateTime.Today);
    set => this._CentralDate = value;
  }

  [PXDBDate(PreserveTime = true)]
  public virtual DateTime? StartDate
  {
    get => new DateTime?(EPCalendarFilter.GetStartDate(this.Type, this.CentralDate.Value));
  }

  [PXDBDate(PreserveTime = true)]
  public virtual DateTime? EndDate
  {
    get => new DateTime?(EPCalendarFilter.GetEndDate(this.Type, this.CentralDate.Value));
  }

  [PXDBString]
  public virtual string DisplayPeriod
  {
    get
    {
      string format = "{0:dd MMMM yyyy}";
      switch (this.Type)
      {
        case "Week":
          format = "{0:dd MMMM yyyy} - {1:dd MMMM yyyy}";
          break;
        case "Month":
          format = "{2:MMMM yyyy}";
          break;
      }
      return string.Format(format, (object) this.StartDate, (object) this.EndDate, (object) this.CentralDate);
    }
  }

  public static DateTime GetStartDate(string periodType, DateTime centralDate)
  {
    DateTime date = centralDate.Date;
    switch (periodType)
    {
      case "Week":
        return date.AddDays((double) -EPCalendarFilter.CalendarTypeAttribute.GetDayIndexInWeek(date.DayOfWeek));
      case "Month":
        return EPCalendarFilter.GetStartDate("Week", date.AddDays((double) (1 - date.Day)));
      default:
        return date;
    }
  }

  public static DateTime GetEndDate(string periodType, DateTime centralDate)
  {
    DateTime date = centralDate.Date;
    switch (periodType)
    {
      case "Week":
        return EPCalendarFilter.GetStartDate(periodType, date).AddDays(7.0);
      case "Month":
        return EPCalendarFilter.GetStartDate(periodType, date).AddDays(35.0);
      default:
        return date.AddDays(1.0);
    }
  }

  public class CalendarTypeAttribute : PXStringListAttribute
  {
    private static string[] weekDayNames = new string[7]
    {
      "Sunday",
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday"
    };
    public const string DAY = "Day";
    public const string WEEK = "Week";
    public const string MONTH = "Month";

    public static string[] WeekDayNames
    {
      get
      {
        string[] weekDayNames = new string[EPCalendarFilter.CalendarTypeAttribute.weekDayNames.Length];
        for (int index = 0; index < EPCalendarFilter.CalendarTypeAttribute.weekDayNames.Length; ++index)
          weekDayNames[index] = PXMessages.LocalizeNoPrefix(EPCalendarFilter.CalendarTypeAttribute.weekDayNames[index]);
        return weekDayNames;
      }
    }

    public static string GetDayName(DayOfWeek dayOfWeek)
    {
      return EPCalendarFilter.CalendarTypeAttribute.WeekDayNames[EPCalendarFilter.CalendarTypeAttribute.GetDayIndexInWeek(dayOfWeek)];
    }

    public static int GetDayIndexInWeek(DayOfWeek dayOfWeek)
    {
      switch (dayOfWeek)
      {
        case DayOfWeek.Monday:
          return 1;
        case DayOfWeek.Tuesday:
          return 2;
        case DayOfWeek.Wednesday:
          return 3;
        case DayOfWeek.Thursday:
          return 4;
        case DayOfWeek.Friday:
          return 5;
        case DayOfWeek.Saturday:
          return 6;
        default:
          return 0;
      }
    }

    public CalendarTypeAttribute()
      : base(new string[3]{ "Day", "Week", "Month" }, new string[3]
      {
        "Day",
        "Week",
        "Month"
      })
    {
    }

    public class Month : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPCalendarFilter.CalendarTypeAttribute.Month>
    {
      public Month()
        : base(nameof (Month))
      {
      }
    }

    public class Day : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPCalendarFilter.CalendarTypeAttribute.Day>
    {
      public Day()
        : base(nameof (Day))
      {
      }
    }

    public class Week : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      EPCalendarFilter.CalendarTypeAttribute.Week>
    {
      public Week()
        : base(nameof (Week))
      {
      }
    }
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPCalendarFilter.type>
  {
  }

  public abstract class centralDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPCalendarFilter.centralDate>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPCalendarFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPCalendarFilter.endDate>
  {
  }

  public abstract class displayPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPCalendarFilter.displayPeriod>
  {
  }
}
