// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSCalendarExceptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Calendar Exception")]
public class CSCalendarExceptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Calendar ID")]
  public virtual 
  #nullable disable
  string CalendarID { get; set; }

  [PXDBInt]
  [PXDefault(2008)]
  [PXUIField]
  public virtual int? YearID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Day Of Week", Enabled = false)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})]
  public virtual int? DayOfWeek { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Work Day")]
  public virtual bool? WorkDay { get; set; }

  [PXDBTime(DisplayMask = "t", InputMask = "t", UseTimeZone = false)]
  [PXDefault]
  [PXUIField(DisplayName = "Start Time")]
  public virtual DateTime? StartTime { get; set; }

  [PXDBTime(DisplayMask = "t", InputMask = "t", UseTimeZone = false)]
  [PXDefault]
  [PXUIField(DisplayName = "End Time")]
  public virtual DateTime? EndTime { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Break Duration")]
  public virtual int? UnpaidTime { get; set; }

  /// <summary>
  /// Similar to <see cref="P:PX.Objects.CS.CSCalendarExceptions.YearID" /> but it is a calculated string field and is used for UI only.
  /// </summary>
  [PXString]
  [PXDBCalced(typeof (ConvertToStr<CSCalendarExceptions.yearID>), typeof (string))]
  [PXUIField]
  public virtual string YearIDAsString { get; set; }

  public abstract class calendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarExceptions.calendarID>
  {
  }

  public abstract class yearID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendarExceptions.yearID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendarExceptions.date>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendarExceptions.dayOfWeek>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarExceptions.description>
  {
  }

  public abstract class workDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSCalendarExceptions.workDay>
  {
  }

  public abstract class startTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSCalendarExceptions.startTime>
  {
  }

  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CSCalendarExceptions.endTime>
  {
  }

  public abstract class unpaidTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSCalendarExceptions.unpaidTime>
  {
  }

  public abstract class yearIDAsString : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSCalendarExceptions.yearIDAsString>
  {
  }
}
