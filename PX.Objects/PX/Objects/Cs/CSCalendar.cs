// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSCalendar
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

[PXPrimaryGraph(new Type[] {typeof (CSCalendarMaint)}, new Type[] {typeof (Select<CSCalendar, Where<CSCalendar.calendarID, Equal<Current<CSCalendar.calendarID>>>>)})]
[PXCacheName("Calendar")]
[Serializable]
public class CSCalendar : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  [PXReferentialIntegrityCheck]
  public virtual 
  #nullable disable
  string CalendarID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>Average amount of working time for this calendar.</summary>
  /// <remarks>
  /// Shown in UI as time: hh mm, persisted in database as minutes.
  /// </remarks>
  [CSDBTimeSpanShortWith24Hours]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Workday Hours")]
  [PXUIEnabled(typeof (BqlOperand<CSCalendar.workdayTimeOverride, IBqlBool>.IsEqual<True>))]
  public virtual int? WorkdayTime { get; set; }

  /// <summary>
  /// This flag indicates whether the user can set <see cref="P:PX.Objects.CS.CSCalendar.WorkdayTime">Workday Time</see> manually.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? WorkdayTimeOverride { get; set; }

  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Time Zone")]
  [PXTimeZone(true)]
  public virtual string TimeZone { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Sunday")]
  public virtual bool? SunWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Sunday Start Time", Required = false)]
  public virtual DateTime? SunStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Sunday End Time", Required = false)]
  public virtual DateTime? SunEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Sun Unpaid Break Time", Enabled = false)]
  public virtual int? SunUnpaidTime { get; set; }

  /// <summary>Working time on Sunday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.SunEndTime">End Time on Sunday</see> - <see cref="P:PX.Objects.CS.CSCalendar.SunStartTime">Start Time on Sunday</see> - <see cref="P:PX.Objects.CS.CSCalendar.SunUnpaidTime">Sum of Break Times on Sunday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Sun Hours Worked", Enabled = false)]
  public virtual int? SunWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Monday")]
  public virtual bool? MonWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Monday Start Time")]
  public virtual DateTime? MonStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Monday End Time")]
  public virtual DateTime? MonEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Mon Unpaid Break Time", Enabled = false)]
  public virtual int? MonUnpaidTime { get; set; }

  /// <summary>Working time on Monday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.MonEndTime">End Time on Monday</see> - <see cref="P:PX.Objects.CS.CSCalendar.MonStartTime">Start Time on Monday</see> - <see cref="P:PX.Objects.CS.CSCalendar.MonUnpaidTime">Sum of Break Times on Monday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Mon Hours Worked", Enabled = false)]
  public virtual int? MonWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Tuesday")]
  public virtual bool? TueWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Tuesday Start Time")]
  public virtual DateTime? TueStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Tuesday End Time")]
  public virtual DateTime? TueEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Tue Unpaid Break Time", Enabled = false)]
  public virtual int? TueUnpaidTime { get; set; }

  /// <summary>Working time on Tuesday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.TueEndTime">End Time on Tuesday</see> - <see cref="P:PX.Objects.CS.CSCalendar.TueStartTime">Start Time on Tuesday</see> - <see cref="P:PX.Objects.CS.CSCalendar.TueUnpaidTime">Sum of Break Times on Tuesday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Tue Hours Worked", Enabled = false)]
  public virtual int? TueWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Wednesday")]
  public virtual bool? WedWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Wednesday Start Time")]
  public virtual DateTime? WedStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Wednesday End Time")]
  public virtual DateTime? WedEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Wed Unpaid Break Time", Enabled = false)]
  public virtual int? WedUnpaidTime { get; set; }

  /// <summary>Working time on Wednesday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.WedEndTime">End Time on Wednesday</see> - <see cref="P:PX.Objects.CS.CSCalendar.WedStartTime">Start Time on Wednesday</see> - <see cref="P:PX.Objects.CS.CSCalendar.WedUnpaidTime">Sum of Break Times on Wednesday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Wed Hours Worked", Enabled = false)]
  public virtual int? WedWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Thursday")]
  public virtual bool? ThuWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Thursday Start Time")]
  public virtual DateTime? ThuStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Thursday End Time")]
  public virtual DateTime? ThuEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Thu Unpaid Break Time", Enabled = false)]
  public virtual int? ThuUnpaidTime { get; set; }

  /// <summary>Working time on Thursday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.ThuEndTime">End Time on Thursday</see> - <see cref="P:PX.Objects.CS.CSCalendar.ThuStartTime">Start Time on Thursday</see> - <see cref="P:PX.Objects.CS.CSCalendar.ThuUnpaidTime">Sum of Break Times on Thursday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Thu Hours Worked", Enabled = false)]
  public virtual int? ThuWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Friday")]
  public virtual bool? FriWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Friday Start Time")]
  public virtual DateTime? FriStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Friday End Time")]
  public virtual DateTime? FriEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Fri Unpaid Break Time", Enabled = false)]
  public virtual int? FriUnpaidTime { get; set; }

  /// <summary>Working time on Friday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.FriEndTime">End Time on Friday</see> - <see cref="P:PX.Objects.CS.CSCalendar.FriStartTime">Start Time on Friday</see> - <see cref="P:PX.Objects.CS.CSCalendar.FriUnpaidTime">Sum of Break Times on Friday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Fri Hours Worked", Enabled = false)]
  public virtual int? FriWorkTime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday")]
  public virtual bool? SatWorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
  [PXUIField(DisplayName = "Saturday Start Time", Required = false)]
  public virtual DateTime? SatStartTime { get; set; }

  [PXDBTime(DisplayMask = "t", UseTimeZone = false)]
  [PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
  [PXUIField(DisplayName = "Saturday End Time", Required = false)]
  public virtual DateTime? SatEndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault]
  [PXUIField(DisplayName = "Sat Unpaid Break Time", Enabled = false)]
  public virtual int? SatUnpaidTime { get; set; }

  /// <summary>Working time on Saturday.</summary>
  /// <value>
  /// <see cref="P:PX.Objects.CS.CSCalendar.SatEndTime">End Time on Saturday</see> - <see cref="P:PX.Objects.CS.CSCalendar.SatStartTime">Start Time on Saturday</see> - <see cref="P:PX.Objects.CS.CSCalendar.SatUnpaidTime">Sum of Break Times on Saturday</see>.
  /// Value represented in minutes.
  /// </value>
  [CSTimeSpanShortWith24Hours]
  [PXUIField(DisplayName = "Sat Hours Worked", Enabled = false)]
  public virtual int? SatWorkTime { get; set; }

  public virtual bool IsWorkDay(DateTime date)
  {
    switch (date.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        return this.SunWorkDay.GetValueOrDefault();
      case DayOfWeek.Monday:
        return this.MonWorkDay.GetValueOrDefault();
      case DayOfWeek.Tuesday:
        return this.TueWorkDay.GetValueOrDefault();
      case DayOfWeek.Wednesday:
        return this.WedWorkDay.GetValueOrDefault();
      case DayOfWeek.Thursday:
        return this.ThuWorkDay.GetValueOrDefault();
      case DayOfWeek.Friday:
        return this.FriWorkDay.GetValueOrDefault();
      case DayOfWeek.Saturday:
        return this.SatWorkDay.GetValueOrDefault();
      default:
        return false;
    }
  }

  public class PK : PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>
  {
    public static CSCalendar Find(PXGraph graph, string calendarID, PKFindOptions options = 0)
    {
      return (CSCalendar) PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>.FindBy(graph, (object) calendarID, options);
    }
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSCalendar.calendarID>
  {
    public const string DefaultCalendarID = "24H7WD";

    public class defaultCalendarID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CSCalendar.calendarID.defaultCalendarID>
    {
      public defaultCalendarID()
        : base("24H7WD")
      {
      }
    }
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSCalendar.description>
  {
  }

  public abstract class workdayTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.workdayTime>
  {
  }

  public abstract class workdayTimeOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CSCalendar.workdayTimeOverride>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSCalendar.timeZone>
  {
  }

  public abstract class sunWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.sunWorkDay>
  {
  }

  public abstract class sunStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.sunStartTime>
  {
  }

  public abstract class sunEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.sunEndTime>
  {
  }

  public abstract class sunUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.sunUnpaidTime>
  {
  }

  public abstract class sunWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.sunWorkTime>
  {
  }

  public abstract class monWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.monWorkDay>
  {
  }

  public abstract class monStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.monStartTime>
  {
  }

  public abstract class monEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.monEndTime>
  {
  }

  public abstract class monUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.monUnpaidTime>
  {
  }

  public abstract class monWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.monWorkTime>
  {
  }

  public abstract class tueWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.tueWorkDay>
  {
  }

  public abstract class tueStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.tueStartTime>
  {
  }

  public abstract class tueEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.tueEndTime>
  {
  }

  public abstract class tueUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.tueUnpaidTime>
  {
  }

  public abstract class tueWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.tueWorkTime>
  {
  }

  public abstract class wedWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.wedWorkDay>
  {
  }

  public abstract class wedStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.wedStartTime>
  {
  }

  public abstract class wedEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.wedEndTime>
  {
  }

  public abstract class wedUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.wedUnpaidTime>
  {
  }

  public abstract class wedWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.wedWorkTime>
  {
  }

  public abstract class thuWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.thuWorkDay>
  {
  }

  public abstract class thuStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.thuStartTime>
  {
  }

  public abstract class thuEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.thuEndTime>
  {
  }

  public abstract class thuUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.thuUnpaidTime>
  {
  }

  public abstract class thuWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.thuWorkTime>
  {
  }

  public abstract class friWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.friWorkDay>
  {
  }

  public abstract class friStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.friStartTime>
  {
  }

  public abstract class friEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.friEndTime>
  {
  }

  public abstract class friUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.friUnpaidTime>
  {
  }

  public abstract class friWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.friWorkTime>
  {
  }

  public abstract class satWorkDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendar.satWorkDay>
  {
  }

  public abstract class satStartTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.satStartTime>
  {
  }

  public abstract class satEndTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendar.satEndTime>
  {
  }

  public abstract class satUnpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.satUnpaidTime>
  {
  }

  public abstract class satWorkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendar.satWorkTime>
  {
  }
}
