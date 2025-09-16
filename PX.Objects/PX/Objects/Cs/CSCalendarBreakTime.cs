// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSCalendarBreakTime
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// The table for the data that ia shown on the "Break Times" tab of the Work Calendar (CS209000) form.
/// The data drives break time information for use in production scheduling.
/// A work calendar can have zero to many break time records.
/// </summary>
/// <remarks>
/// The parent is <see cref="T:PX.Objects.CS.CSCalendar" />.
/// </remarks>
[PXCacheName("Calendar Break Time")]
[Serializable]
public class CSCalendarBreakTime : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The work calendar identifier.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.CSCalendar.CalendarID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CSCalendar.calendarID))]
  [PXUIField(DisplayName = "Calendar ID", Enabled = false, Visible = false)]
  [PXParent(typeof (Select<CSCalendar, Where<CSCalendar.calendarID, Equal<Current<CSCalendarBreakTime.calendarID>>>>))]
  public virtual 
  #nullable disable
  string CalendarID { get; set; }

  /// <summary>
  /// The field contains an option how the break period is applied to the calendar.
  /// This field is a key field.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CS.CSCalendarBreakTime.dayOfWeek.List" /> class.
  /// The default value is <see cref="F:PX.Objects.CS.CSCalendarBreakTime.dayOfWeek.All" />.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Day Of Week")]
  [CSCalendarBreakTime.dayOfWeek.List]
  public virtual int? DayOfWeek { get; set; }

  /// <summary>Start time of break period.</summary>
  [PXDBTime(DisplayMask = "t", InputMask = "t", UseTimeZone = false, IsKey = true)]
  [PXUIField(DisplayName = "Start Time")]
  [PXDefault]
  public virtual DateTime? StartTime { get; set; }

  /// <summary>End time of the break period.</summary>
  [PXDBTime(DisplayMask = "t", InputMask = "t", UseTimeZone = false)]
  [PXUIField(DisplayName = "End Time")]
  [PXDefault]
  public virtual DateTime? EndTime { get; set; }

  /// <summary>Amount of time (in minutes) of the break period.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendarBreakTime.EndTime">End Time</see> - <see cref="P:PX.Objects.CS.CSCalendarBreakTime.StartTime">Start Time</see>.
  /// </value>
  [PXDBTimeSpanLong]
  [CSCalendarBreakTime.breakTime.Default]
  [PXUIField(DisplayName = "Break Duration", Enabled = false)]
  [PXFormula(typeof (Default<CSCalendarBreakTime.startTime, CSCalendarBreakTime.endTime>))]
  public virtual int? BreakTime { get; set; }

  /// <summary>Description of the break period.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CSCalendarBreakTime>.By<CSCalendarBreakTime.calendarID, CSCalendarBreakTime.dayOfWeek, CSCalendarBreakTime.startTime>
  {
    public static CSCalendarBreakTime Find(
      PXGraph graph,
      string calendarID,
      int? dayOfWeek,
      DateTime? startTime,
      PKFindOptions options = 0)
    {
      return (CSCalendarBreakTime) PrimaryKeyOf<CSCalendarBreakTime>.By<CSCalendarBreakTime.calendarID, CSCalendarBreakTime.dayOfWeek, CSCalendarBreakTime.startTime>.FindBy(graph, (object) calendarID, (object) dayOfWeek, (object) startTime, options);
    }
  }

  public static class FK
  {
    public class CSCalendar : 
      PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>.ForeignKeyOf<CSCalendarBreakTime>.By<CSCalendarBreakTime.calendarID>
    {
    }
  }

  public abstract class calendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarBreakTime.calendarID>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendarBreakTime.dayOfWeek>
  {
    /// <summary>Indicates all day of week days apply (value = 10)</summary>
    public const int All = 10;
    public const int Sunday = 0;
    public const int Monday = 1;
    public const int Tuesday = 2;
    public const int Wednesday = 3;
    public const int Thursday = 4;
    public const int Friday = 5;
    public const int Saturday = 6;

    public class all : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.all>
    {
      public all()
        : base(10)
      {
      }
    }

    public class sunday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.sunday>
    {
      public sunday()
        : base(0)
      {
      }
    }

    public class monday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.monday>
    {
      public monday()
        : base(1)
      {
      }
    }

    public class tuesday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.tuesday>
    {
      public tuesday()
        : base(2)
      {
      }
    }

    public class wednesday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.wednesday>
    {
      public wednesday()
        : base(3)
      {
      }
    }

    public class thursday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.thursday>
    {
      public thursday()
        : base(4)
      {
      }
    }

    public class friday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.friday>
    {
      public friday()
        : base(5)
      {
      }
    }

    public class saturday : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    CSCalendarBreakTime.dayOfWeek.saturday>
    {
      public saturday()
        : base(6)
      {
      }
    }

    /// <summary>
    /// List following standard date enum DayOfWeek (0-6 for Sunday-Saturday) plus an "All" for al day of weeks.
    /// </summary>
    public class List : PXIntListAttribute
    {
      public List()
        : base(new int[8]{ 0, 1, 2, 3, 4, 5, 6, 10 }, new string[8]
        {
          "Sunday",
          "Monday",
          "Tuesday",
          "Wednesday",
          "Thursday",
          "Friday",
          "Saturday",
          "All"
        })
      {
      }
    }
  }

  public abstract class startTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSCalendarBreakTime.startTime>
  {
  }

  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendarBreakTime.endTime>
  {
  }

  public abstract class breakTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendarBreakTime.breakTime>
  {
    public class DefaultAttribute : PXDefaultAttribute
    {
      public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
      {
        CSCalendarBreakTime row = (CSCalendarBreakTime) e.Row;
        if (row == null)
          e.NewValue = (object) 0;
        else
          e.NewValue = (object) Convert.ToInt32(CSCalendarBreakTime.breakTime.DefaultAttribute.GetTotalMinutes(row.StartTime, row.EndTime));
      }

      public static double GetTotalMinutes(DateTime? startDateTime, DateTime? endDatetime)
      {
        return !startDateTime.HasValue || !endDatetime.HasValue ? 0.0 : Math.Max((endDatetime.GetValueOrDefault() - startDateTime.GetValueOrDefault()).TotalMinutes, 0.0);
      }
    }
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarBreakTime.description>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSCalendarBreakTime.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarBreakTime.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSCalendarBreakTime.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSCalendarBreakTime.Tstamp>
  {
  }
}
