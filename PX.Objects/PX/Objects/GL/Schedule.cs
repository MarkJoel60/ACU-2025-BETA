// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Schedule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.WZ;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A schedule according to which documents are generated on a regular basis from a template document.
/// The Schedule table contains the schedule parameters, such as the schedule type
/// (monthly, weekly, daily, by the financial period) and frequency.
/// The table is used by different Acumatica ERP functional areas.
/// </summary>
[PXCacheName("Schedule")]
[PXPrimaryGraph(new Type[] {typeof (ScheduleMaint), typeof (APScheduleMaint), typeof (ARScheduleMaint), typeof (WZScheduleMaint)}, new Type[] {typeof (Where<Schedule.module, Equal<BatchModule.moduleGL>>), typeof (Where<Schedule.module, Equal<BatchModule.moduleAP>>), typeof (Where<Schedule.module, Equal<BatchModule.moduleAR>>), typeof (Where<Schedule.module, Equal<BatchModule.moduleWZ>>)})]
[Serializable]
public class Schedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _CreatedDateTime;
  protected 
  #nullable disable
  string _LastModifiedByScreenID;
  protected string _Days;
  protected string _Weeks;
  protected string _Months;
  protected string _Periods;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (GLSetup.scheduleNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search<Schedule.scheduleID, Where<Schedule.module, Equal<Current<Schedule.module>>>>))]
  [PXFieldDescription]
  [PXDefault]
  public virtual string ScheduleID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string ScheduleName { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? Active { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PXUIField]
  [GLScheduleType.List]
  public virtual string ScheduleType { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  [GLScheduleType.List]
  public virtual string FormScheduleType
  {
    get => this.ScheduleType;
    set => this.ScheduleType = value;
  }

  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? DailyFrequency { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? WeeklyFrequency { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  [PXUIVerify]
  public virtual bool? WeeklyOnDay1 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay2 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay3 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay4 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay5 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay6 { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? WeeklyOnDay7 { get; set; }

  [PXDBShort]
  [PXUIField]
  [PXDefault(1)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})]
  public virtual short? MonthlyFrequency { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [GLScheduleMonthlyType.List]
  [PXDefault("D")]
  public virtual string MonthlyDaySel { get; set; }

  [PXDBShort]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay { get; set; }

  [PXDBShort]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"1st", "2nd", "3rd", "4th", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek { get; set; }

  [PXDBShort]
  [PXDefault(1)]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Weekday", "Weekend"})]
  public virtual short? MonthlyOnDayOfWeek { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? PeriodFrequency { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXDefault("S")]
  [PeriodDateSelOption.List]
  public virtual string PeriodDateSel { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? PeriodFixedDay { get; set; }

  [PXDBDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? StartDate { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? NoEndDate { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  [PXUIVerify]
  public virtual DateTime? EndDate { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? NoRunLimit { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXUIField]
  [PXDefault(1)]
  public virtual short? RunLimit { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? RunCntr { get; set; }

  [PXDBDate]
  [PXRequiredExpr(typeof (Where<Schedule.active, Equal<True>>))]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? NextRunDate { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastRunDate { get; set; }

  [PXNote(DescriptionField = typeof (Schedule.scheduleID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("GL")]
  public virtual string Module { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string Days => PXLocalizer.Localize("day(s)", typeof (Messages).FullName);

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string Weeks => PXLocalizer.Localize("week(s)", typeof (Messages).FullName);

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string Months => PXLocalizer.Localize("month(s)", typeof (Messages).FullName);

  [PXString(IsUnicode = true)]
  [PXUIField]
  public virtual string Periods => PXLocalizer.Localize("period(s)", typeof (Messages).FullName);

  public class PK : PrimaryKeyOf<Schedule>.By<Schedule.scheduleID>
  {
    public static Schedule Find(PXGraph graph, string scheduleID, PKFindOptions options = 0)
    {
      return (Schedule) PrimaryKeyOf<Schedule>.By<Schedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.selected>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.scheduleID>
  {
  }

  public abstract class scheduleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.scheduleName>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.active>
  {
  }

  public abstract class scheduleType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.scheduleType>
  {
  }

  public abstract class formScheduleType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Schedule.formScheduleType>
  {
  }

  public abstract class dailyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.dailyFrequency>
  {
  }

  public abstract class weeklyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.weeklyFrequency>
  {
  }

  public abstract class weeklyOnDay1 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay1>
  {
  }

  public abstract class weeklyOnDay2 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay2>
  {
  }

  public abstract class weeklyOnDay3 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay3>
  {
  }

  public abstract class weeklyOnDay4 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay4>
  {
  }

  public abstract class weeklyOnDay5 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay5>
  {
  }

  public abstract class weeklyOnDay6 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay6>
  {
  }

  public abstract class weeklyOnDay7 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.weeklyOnDay7>
  {
  }

  public abstract class monthlyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.monthlyFrequency>
  {
  }

  public abstract class monthlyDaySel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.monthlyDaySel>
  {
  }

  public abstract class monthlyOnDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.monthlyOnDay>
  {
  }

  public abstract class monthlyOnWeek : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.monthlyOnWeek>
  {
  }

  public abstract class monthlyOnDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    Schedule.monthlyOnDayOfWeek>
  {
  }

  public abstract class periodFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.periodFrequency>
  {
  }

  public abstract class periodDateSel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.periodDateSel>
  {
  }

  public abstract class periodFixedDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.periodFixedDay>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Schedule.startDate>
  {
  }

  public abstract class noEndDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.noEndDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Schedule.endDate>
  {
  }

  public abstract class noRunLimit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Schedule.noRunLimit>
  {
  }

  public abstract class runLimit : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.runLimit>
  {
  }

  public abstract class runCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Schedule.runCntr>
  {
  }

  public abstract class nextRunDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Schedule.nextRunDate>
  {
  }

  public abstract class lastRunDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Schedule.lastRunDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Schedule.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Schedule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Schedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Schedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Schedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Schedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Schedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Schedule.lastModifiedDateTime>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.module>
  {
  }

  public abstract class days : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.days>
  {
  }

  public abstract class weeks : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.weeks>
  {
  }

  public abstract class months : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.months>
  {
  }

  public abstract class periods : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Schedule.periods>
  {
  }
}
