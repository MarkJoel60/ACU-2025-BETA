// Decompiled with JetBrains decompiler
// Type: PX.SM.AUSchedule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Web.UI;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Schedule")]
public class AUSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _ScheduleID;
  protected string _Description;
  protected string _GraphName;
  protected string _ViewName;
  protected string _FilterName;
  protected string _TableName;
  protected short? _FilterCntr;
  protected short? _FillCntr;
  protected bool? _IsActive;
  protected string _ActionName;
  protected string _ScheduleType;
  protected short? _DailyFrequency;
  protected short? _WeeklyFrequency;
  protected bool? _WeeklyOnDay1;
  protected bool? _WeeklyOnDay2;
  protected bool? _WeeklyOnDay3;
  protected bool? _WeeklyOnDay4;
  protected bool? _WeeklyOnDay5;
  protected bool? _WeeklyOnDay6;
  protected bool? _WeeklyOnDay7;
  protected short? _MonthlyFrequency;
  protected string _MonthlyDaySel;
  protected short? _MonthlyOnDay;
  protected short? _MonthlyOnWeek;
  protected short? _MonthlyOnDayOfWeek;
  protected short? _PeriodFrequency;
  protected string _PeriodDateSel;
  protected short? _PeriodFixedDay;
  protected System.DateTime? _StartDate;
  protected bool? _NoEndDate;
  protected System.DateTime? _EndDate;
  protected bool? _NoRunLimit;
  protected short? _RunLimit;
  protected int? _RunCntr;
  protected System.DateTime? _NextRunDate;
  protected System.DateTime? _LastRunDate;
  protected string _LastRunResult;
  protected System.DateTime? _StartTime;
  protected System.DateTime? _EndTime;
  protected short? _Interval;
  protected System.DateTime? _NextRunTime;
  protected string _TemplateScreenID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;
  private static readonly System.DateTime _zeroDate = new System.DateTime(1900, 1, 1);

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector]
  [PXForeignReference(typeof (AUSchedule.FK.SiteMap))]
  [PXForeignReference(typeof (AUSchedule.FK.PortalMap))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Schedule ID")]
  [PXSelector(typeof (Search<AUSchedule.scheduleID>), DescriptionField = typeof (AUSchedule.description))]
  [PXReferentialIntegrityCheck]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string GraphName
  {
    get => this._GraphName;
    set => this._GraphName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXDBString(128 /*0x80*/)]
  public virtual string FilterName
  {
    get => this._FilterName;
    set => this._FilterName = value;
  }

  public bool? FilterVisible
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUSchedule.filterName)})] get
    {
      return new bool?(this._FilterName != null);
    }
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Process with Branch", Visible = false)]
  [PXSelector(typeof (Search<Branch.branchID, Where<MatchWithBranch<Branch.branchID>>>), new System.Type[] {typeof (Branch.branchCD), typeof (Branch.roleName)}, SubstituteKey = typeof (Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FilterCntr
  {
    get => this._FilterCntr;
    set => this._FilterCntr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FillCntr
  {
    get => this._FillCntr;
    set => this._FillCntr = value;
  }

  [PXDBShort(MinValue = 0)]
  [PXDefault(0)]
  public virtual short? AbortCntr { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Max Consecutive Aborted Executions")]
  public virtual short? MaxAbortCount { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Do Not Deactivate")]
  public virtual bool? DoNotDeactivate { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXUIField(DisplayName = "Action")]
  [PXDefault("ProcessAll")]
  [PXDBString(128 /*0x80*/)]
  [ScheduledJobHandlersList]
  public virtual string Action { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Action Name", Required = true, Enabled = false)]
  [PXStringList(new string[] {null}, new string[] {""})]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Time Zone")]
  [PXDefault]
  [PXTimeZone(false)]
  public string TimeZoneID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXUIField(DisplayName = "Frequency", Visibility = PXUIVisibility.SelectorVisible)]
  [PXStringList(new string[] {"D", "W", "M", "P"}, new string[] {"Daily", "Weekly", "Monthly", "Financial Period"})]
  public virtual string ScheduleType
  {
    get => this._ScheduleType;
    set => this._ScheduleType = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  public virtual short? DailyFrequency
  {
    get => this._DailyFrequency;
    set => this._DailyFrequency = value;
  }

  [PXUIField(DisplayName = "Day(s)")]
  [PXString(1)]
  public virtual string DailyLabel => PXLocalizer.Localize("Day(s)");

  [PXDBShort]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  public virtual short? WeeklyFrequency
  {
    get => this._WeeklyFrequency;
    set => this._WeeklyFrequency = value;
  }

  [PXUIField(DisplayName = "Week(s)")]
  [PXString(1)]
  public virtual string WeeklyLabel => PXLocalizer.Localize("Week(s)");

  [PXDBBool]
  [PXUIField(DisplayName = "Sunday")]
  [PXDefault(true)]
  public virtual bool? WeeklyOnDay1
  {
    get => this._WeeklyOnDay1;
    set => this._WeeklyOnDay1 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Monday")]
  public virtual bool? WeeklyOnDay2
  {
    get => this._WeeklyOnDay2;
    set => this._WeeklyOnDay2 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tuesday")]
  public virtual bool? WeeklyOnDay3
  {
    get => this._WeeklyOnDay3;
    set => this._WeeklyOnDay3 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Wednesday")]
  public virtual bool? WeeklyOnDay4
  {
    get => this._WeeklyOnDay4;
    set => this._WeeklyOnDay4 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Thursday")]
  public virtual bool? WeeklyOnDay5
  {
    get => this._WeeklyOnDay5;
    set => this._WeeklyOnDay5 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Friday")]
  public virtual bool? WeeklyOnDay6
  {
    get => this._WeeklyOnDay6;
    set => this._WeeklyOnDay6 = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday")]
  public virtual bool? WeeklyOnDay7
  {
    get => this._WeeklyOnDay7;
    set => this._WeeklyOnDay7 = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})]
  public virtual short? MonthlyFrequency
  {
    get => this._MonthlyFrequency;
    set => this._MonthlyFrequency = value;
  }

  [PXUIField(DisplayName = "Month(s)")]
  [PXString(1)]
  public virtual string MonthlyLabel => PXLocalizer.Localize("Month(s)");

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Day Based On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string MonthlyDaySel
  {
    get => this._MonthlyDaySel;
    set => this._MonthlyDaySel = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay
  {
    get => this._MonthlyOnDay;
    set => this._MonthlyOnDay = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek
  {
    get => this._MonthlyOnWeek;
    set => this._MonthlyOnWeek = value;
  }

  [PXDBShort]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Day Of Week")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Weekday", "Weekend"})]
  public virtual short? MonthlyOnDayOfWeek
  {
    get => this._MonthlyOnDayOfWeek;
    set => this._MonthlyOnDayOfWeek = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  public virtual short? PeriodFrequency
  {
    get => this._PeriodFrequency;
    set => this._PeriodFrequency = value;
  }

  [PXUIField(DisplayName = "Period(s)")]
  [PXString(1)]
  public virtual string PeriodLabel => PXLocalizer.Localize("Period(s)");

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Date Based On")]
  [PXDefault("S")]
  [PXStringList(new string[] {"S", "E", "D"}, new string[] {"Start of the Period", "End of the Period", "Fixed Day of the Period"})]
  public virtual string PeriodDateSel
  {
    get => this._PeriodDateSel;
    set => this._PeriodDateSel = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Fixed Day of the Period")]
  [PXDefault(1)]
  public virtual short? PeriodFixedDay
  {
    get => this._PeriodFixedDay;
    set => this._PeriodFixedDay = value;
  }

  [AUSchedule.AUSeparateDate(typeof (AUSchedule.startTime), UseTimeZone = false)]
  [PXUIField(DisplayName = "Starts On")]
  [PXDefault]
  public virtual System.DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "No Expiration Date")]
  [PXDefault(true)]
  public virtual bool? NoEndDate
  {
    get => this._NoEndDate;
    set => this._NoEndDate = value;
  }

  [AUSchedule.AUSeparateDate(typeof (AUSchedule.endTime), UseTimeZone = false)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Expires On", Enabled = false)]
  public virtual System.DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "No Execution Limit")]
  [PXDefault(false)]
  public virtual bool? NoRunLimit
  {
    get => this._NoRunLimit;
    set => this._NoRunLimit = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Execution Limit")]
  [PXDefault(1, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? RunLimit
  {
    get => this._RunLimit;
    set => this._RunLimit = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Executed", Enabled = false)]
  public virtual int? RunCntr
  {
    get => this._RunCntr;
    set => this._RunCntr = value;
  }

  [PXDefault]
  [AUSchedule.AUSeparateDate(typeof (AUSchedule.nextRunTime), UseTimeZone = false)]
  [PXUIField(DisplayName = "Next Execution Date")]
  public virtual System.DateTime? NextRunDate
  {
    get => this._NextRunDate;
    set => this._NextRunDate = value;
  }

  [PXDBDateWithTimezone(typeof (AUSchedule.timeZoneID), PreserveTime = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Last Executed", Enabled = false)]
  public virtual System.DateTime? LastRunDate
  {
    get => this._LastRunDate;
    set => this._LastRunDate = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string LastRunStatus
  {
    get
    {
      if (!this.LastRunDate.HasValue || !this.LastRunErrorLevel.HasValue)
        return "";
      switch (this.LastRunErrorLevel.Value)
      {
        case 2:
        case 3:
          return Sprite.Control.GetFullUrl("Warning");
        case 4:
        case 5:
          return Sprite.Control.GetFullUrl("Error");
        default:
          return Sprite.Control.GetFullUrl("Info");
      }
    }
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? LastRunErrorLevel { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Last Execution Result")]
  public virtual string LastRunResult
  {
    get => this._LastRunResult;
    set => this._LastRunResult = value;
  }

  [AUSchedule.AUSeparateTime(typeof (AUSchedule.startDate), UseTimeZone = false, DisplayMask = "t")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Start Time")]
  public virtual System.DateTime? StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [AUSchedule.AUSeparateTime(typeof (AUSchedule.endDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "Stop Time")]
  public virtual System.DateTime? EndTime
  {
    get => this._EndTime;
    set => this._EndTime = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Every (hh:mm)")]
  [PXDefault(0)]
  public virtual short? Interval
  {
    get => this._Interval;
    set => this._Interval = value;
  }

  [PXDefault]
  [AUSchedule.AUSeparateTime(typeof (AUSchedule.nextRunDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "Next Execution Time")]
  public virtual System.DateTime? NextRunTime
  {
    get => this._NextRunTime;
    set => this._NextRunTime = value;
  }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string TemplateScreenID
  {
    get => this._TemplateScreenID;
    set => this._TemplateScreenID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exact Time")]
  public virtual bool? ExactTime { get; set; }

  public bool? FilterFillValues { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Executions to Keep in History")]
  [PXDefault(1, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? HistoryRetainCount { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Keep Full History")]
  [PXDefault(false)]
  public virtual bool? KeepFullHistory { get; set; }

  /// <summary>
  /// Determines whether schedule is fully active and should be ran (checks all required conditions like End Date, Run Counter and so on).
  /// </summary>
  public bool ShouldBeRan
  {
    get
    {
      bool? isActive = this.IsActive;
      bool flag1 = true;
      if (isActive.GetValueOrDefault() == flag1 & isActive.HasValue)
      {
        bool? noRunLimit = this.NoRunLimit;
        bool flag2 = true;
        if (noRunLimit.GetValueOrDefault() == flag2 & noRunLimit.HasValue || this.RunCntr.HasValue && this.RunLimit.HasValue && this.RunCntr.Value < (int) this.RunLimit.Value)
        {
          bool? noEndDate = this.NoEndDate;
          bool flag3 = true;
          if (noEndDate.GetValueOrDefault() == flag3 & noEndDate.HasValue)
            return true;
          return this.NextRunDateTime.HasValue && this.EndDateTime.HasValue && this.NextRunDateTime.Value <= this.EndDateTime.Value;
        }
      }
      return false;
    }
  }

  /// <summary>
  /// A service field that is used to hide the Events tab on the Automation Schedules (SM205020) form.
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  [PXDependsOnFields(new System.Type[] {typeof (AUSchedule.actionName)})]
  public bool ShowEventsTabExpr
  {
    get
    {
      return this.ScheduleID.HasValue && this.ScheduleID.Value > 0 && this.ActionName.OrdinalEquals("RaiseEvent");
    }
  }

  /// <summary>
  /// A service field that is used to hide the Conditions tab on the Automation Schedules (SM205020) form.
  /// </summary>
  [PXBool]
  [PXDefault(true)]
  public bool ShowConditionsTabExpr { get; set; }

  [PXBool]
  [PXDefault(false)]
  public bool ShowEmailNotificationsTabExpr { get; set; }

  /// <summary>
  /// A service field that is used to hide warning about no events on the Automation Schedules (SM205020) form.
  /// </summary>
  [PXBool]
  public bool? CreatedFromBusinessEvent { get; set; }

  public static System.DateTime ZeroDate => AUSchedule._zeroDate;

  private static System.DateTime? MergeDateTime(System.DateTime? date, System.DateTime? time)
  {
    System.DateTime? nullable = time;
    System.DateTime dateTime1;
    if (time.HasValue)
    {
      System.DateTime date1 = time.Value.Date;
      dateTime1 = AUSchedule.ZeroDate;
      System.DateTime dateTime2 = dateTime1.AddDays(1.0);
      if (date1 > dateTime2)
      {
        ref System.DateTime? local = ref nullable;
        dateTime1 = AUSchedule.ZeroDate;
        System.DateTime dateTime3 = dateTime1.Add(time.Value.TimeOfDay);
        local = new System.DateTime?(dateTime3);
      }
    }
    if (date.HasValue)
    {
      if (!time.HasValue)
        return date;
      dateTime1 = date.Value;
      return new System.DateTime?(dateTime1.Add(nullable.Value - AUSchedule.ZeroDate));
    }
    if (!time.HasValue)
      return date;
    dateTime1 = PXTimeZoneInfo.Now;
    return new System.DateTime?(dateTime1.Add(nullable.Value - AUSchedule.ZeroDate));
  }

  private static void SplitDateTime(System.DateTime? value, out System.DateTime? date, out System.DateTime? time)
  {
    if (value.HasValue)
    {
      date = new System.DateTime?(value.Value.Date);
      time = new System.DateTime?(AUSchedule.ZeroDate.Add(new TimeSpan(value.Value.TimeOfDay.Hours, value.Value.TimeOfDay.Minutes, value.Value.TimeOfDay.Seconds)));
    }
    else
    {
      date = new System.DateTime?();
      time = new System.DateTime?();
    }
  }

  /// <summary>Represents merged StartDate and StartTime.</summary>
  public System.DateTime? StartDateTime => AUSchedule.MergeDateTime(this.StartDate, this.StartTime);

  /// <summary>Represents merged EndDate and EndTime.</summary>
  public System.DateTime? EndDateTime => AUSchedule.MergeDateTime(this.EndDate, this.EndTime);

  [PXDateAndTime(UseTimeZone = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Next Execution", Enabled = false)]
  public System.DateTime? NextRunDateTime
  {
    get => AUSchedule.MergeDateTime(this.NextRunDate, this.NextRunTime);
  }

  public System.DateTime? NextRunDateTimeUTC
  {
    get
    {
      if (!this.NextRunDateTime.HasValue)
        return new System.DateTime?();
      return new System.DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(this.NextRunDateTime.Value, (string.IsNullOrEmpty(this.TimeZoneID) ? PXTimeZoneInfo.Invariant : PXTimeZoneInfo.FindSystemTimeZoneById(this.TimeZoneID)) ?? throw new PXException("The time zone {0} cannot be found in the system.", new object[1]
      {
        (object) this.TimeZoneID
      })));
    }
  }

  public class PK : PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>
  {
    public static AUSchedule Find(PXGraph graph, int? scheduleID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<AUSchedule>.By<AUSchedule.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<AUSchedule>.By<AUSchedule.screenID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<AUSchedule>.By<AUSchedule.branchID>
    {
    }
  }

  public static class Types
  {
    public const string Daily = "D";
    public const string Weekly = "W";
    public const string Monthly = "M";
    public const string ByFinancialPeriod = "P";
  }

  public static class MonthlyDaySelTypes
  {
    public const string Day = "D";
    public const string DayOnTheWeek = "W";
  }

  public static class DayOfWeekTypes
  {
    public const short Sunday = 1;
    public const short Monday = 2;
    public const short Tuesday = 3;
    public const short Wednesday = 4;
    public const short Thursday = 5;
    public const short Friday = 6;
    public const short Saturday = 7;
    public const short Weekday = 8;
    public const short Weekend = 9;
  }

  public static class WeekTypes
  {
    public const short First = 1;
    public const short Second = 2;
    public const short Third = 3;
    public const short Forth = 4;
    public const short Last = 5;
  }

  public static class FinPeriodDaySelTypes
  {
    public const string Start = "S";
    public const string End = "E";
    public const string FixedDay = "D";
  }

  public class AUSeparateDateAttribute : PXDBDateAttribute
  {
    protected System.Type _TimeField;

    public AUSeparateDateAttribute(System.Type timeField) => this._TimeField = timeField;

    public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
    {
      if (e.Row != null && e.Value is System.DateTime)
      {
        System.DateTime? nullable = sender.GetValue(e.Row, this._TimeField.Name) as System.DateTime?;
        if (nullable.HasValue)
        {
          PXCommandPreparingEventArgs preparingEventArgs = e;
          System.DateTime dateTime1 = (System.DateTime) e.Value;
          ref System.DateTime local1 = ref dateTime1;
          System.DateTime dateTime2 = nullable.Value;
          int hour = dateTime2.Hour;
          dateTime2 = nullable.Value;
          int minute = dateTime2.Minute;
          int second = nullable.Value.Second;
          TimeSpan timeSpan = new TimeSpan(hour, minute, second);
          // ISSUE: variable of a boxed type
          __Boxed<System.DateTime> local2 = (ValueType) local1.Add(timeSpan);
          preparingEventArgs.Value = (object) local2;
        }
        this._PreserveTime = true;
      }
      base.CommandPreparing(sender, e);
      if (e.Row == null || !(e.DataValue is System.DateTime))
        return;
      this._PreserveTime = false;
      PXCommandPreparingEventArgs preparingEventArgs1 = e;
      System.DateTime dataValue = (System.DateTime) e.DataValue;
      int year = dataValue.Year;
      dataValue = (System.DateTime) e.DataValue;
      int month = dataValue.Month;
      int day = ((System.DateTime) e.DataValue).Day;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local = (ValueType) new System.DateTime(year, month, day);
      preparingEventArgs1.DataValue = (object) local;
    }

    public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      System.DateTime? nullable1 = e.Record.GetDateTime(e.Position);
      if (nullable1.HasValue)
      {
        System.DateTime? nullable2 = sender.GetValue(e.Row, this._TimeField.Name) as System.DateTime?;
        if (nullable2.HasValue && this.UseTimeZone)
        {
          ref System.DateTime? local1 = ref nullable1;
          System.DateTime dateTime1 = nullable1.Value;
          ref System.DateTime local2 = ref dateTime1;
          System.DateTime dateTime2 = nullable2.Value;
          int hour1 = dateTime2.Hour;
          dateTime2 = nullable2.Value;
          int minute1 = dateTime2.Minute;
          dateTime2 = nullable2.Value;
          int second1 = dateTime2.Second;
          TimeSpan timeSpan = new TimeSpan(hour1, minute1, second1);
          System.DateTime dateTime3 = local2.Add(timeSpan);
          local1 = new System.DateTime?(dateTime3);
          nullable1 = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(nullable1.Value, LocaleInfo.GetTimeZone()));
          PXCache pxCache = sender;
          object row = e.Row;
          string name = this._TimeField.Name;
          int hour2 = nullable1.Value.Hour;
          System.DateTime dateTime4 = nullable1.Value;
          int minute2 = dateTime4.Minute;
          dateTime4 = nullable1.Value;
          int second2 = dateTime4.Second;
          // ISSUE: variable of a boxed type
          __Boxed<System.DateTime> local3 = (ValueType) new System.DateTime(1900, 1, 1, hour2, minute2, second2);
          pxCache.SetValue(row, name, (object) local3);
          ref System.DateTime? local4 = ref nullable1;
          dateTime4 = nullable1.Value;
          int year = dateTime4.Year;
          dateTime4 = nullable1.Value;
          int month = dateTime4.Month;
          dateTime4 = nullable1.Value;
          int day = dateTime4.Day;
          System.DateTime dateTime5 = new System.DateTime(year, month, day);
          local4 = new System.DateTime?(dateTime5);
        }
      }
      sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable1);
      ++e.Position;
    }
  }

  public class AUSeparateTimeAttribute : PXDBDateAttribute
  {
    protected System.Type _DateField;

    public AUSeparateTimeAttribute(System.Type dateField)
    {
      this._DateField = dateField;
      this.PreserveTime = true;
    }

    public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
    {
      System.DateTime dataValue;
      if (e.Row != null && e.Value is System.DateTime)
      {
        System.DateTime? nullable = sender.GetValue(e.Row, this._DateField.Name) as System.DateTime?;
        if (!nullable.HasValue)
          nullable = new System.DateTime?(PXTimeZoneInfo.Now);
        PXCommandPreparingEventArgs preparingEventArgs = e;
        int year = nullable.Value.Year;
        System.DateTime dateTime = nullable.Value;
        int month = dateTime.Month;
        dateTime = nullable.Value;
        int day = dateTime.Day;
        int hour = ((System.DateTime) e.Value).Hour;
        dataValue = (System.DateTime) e.Value;
        int minute = dataValue.Minute;
        dataValue = (System.DateTime) e.Value;
        int second = dataValue.Second;
        // ISSUE: variable of a boxed type
        __Boxed<System.DateTime> local = (ValueType) new System.DateTime(year, month, day, hour, minute, second);
        preparingEventArgs.Value = (object) local;
        this._PreserveTime = true;
      }
      base.CommandPreparing(sender, e);
      if (e.Row == null || !(e.DataValue is System.DateTime))
        return;
      this._PreserveTime = false;
      PXCommandPreparingEventArgs preparingEventArgs1 = e;
      dataValue = (System.DateTime) e.DataValue;
      int hour1 = dataValue.Hour;
      dataValue = (System.DateTime) e.DataValue;
      int minute1 = dataValue.Minute;
      dataValue = (System.DateTime) e.DataValue;
      int second1 = dataValue.Second;
      // ISSUE: variable of a boxed type
      __Boxed<System.DateTime> local1 = (ValueType) new System.DateTime(1900, 1, 1, hour1, minute1, second1);
      preparingEventArgs1.DataValue = (object) local1;
    }

    public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      System.DateTime? nullable1 = e.Record.GetDateTime(e.Position);
      if (nullable1.HasValue)
      {
        System.DateTime? nullable2 = sender.GetValue(e.Row, this._DateField.Name) as System.DateTime?;
        bool flag = false;
        if (!nullable2.HasValue)
        {
          nullable2 = new System.DateTime?(PXTimeZoneInfo.Now);
          flag = true;
        }
        if (nullable2.HasValue && this.UseTimeZone)
        {
          ref System.DateTime? local1 = ref nullable1;
          System.DateTime date = nullable2.Value.Date;
          ref System.DateTime local2 = ref date;
          System.DateTime dateTime1 = nullable1.Value;
          int hour = dateTime1.Hour;
          dateTime1 = nullable1.Value;
          int minute = dateTime1.Minute;
          int second = nullable1.Value.Second;
          TimeSpan timeSpan = new TimeSpan(hour, minute, second);
          System.DateTime dateTime2 = local2.Add(timeSpan);
          local1 = new System.DateTime?(dateTime2);
          nullable1 = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(nullable1.Value, LocaleInfo.GetTimeZone()));
          if (!flag)
          {
            PXCache pxCache = sender;
            object row = e.Row;
            string name = this._DateField.Name;
            System.DateTime dateTime3 = nullable1.Value;
            int year = dateTime3.Year;
            dateTime3 = nullable1.Value;
            int month = dateTime3.Month;
            dateTime3 = nullable1.Value;
            int day = dateTime3.Day;
            // ISSUE: variable of a boxed type
            __Boxed<System.DateTime> local3 = (ValueType) new System.DateTime(year, month, day);
            pxCache.SetValue(row, name, (object) local3);
          }
          nullable1 = new System.DateTime?(new System.DateTime(1900, 1, 1, nullable1.Value.Hour, nullable1.Value.Minute, nullable1.Value.Second));
        }
      }
      sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable1);
      ++e.Position;
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.screenID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUSchedule.scheduleID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.description>
  {
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.graphName>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.viewName>
  {
  }

  public abstract class filterName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.filterName>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.tableName>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUSchedule.branchID>
  {
  }

  public abstract class filterCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.filterCntr>
  {
  }

  public abstract class fillCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.fillCntr>
  {
  }

  public abstract class abortCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.abortCntr>
  {
  }

  public abstract class maxAbortCount : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.maxAbortCount>
  {
  }

  public abstract class doNotDeactivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.doNotDeactivate>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.isActive>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.action>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.actionName>
  {
  }

  public abstract class timeZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.timeZoneID>
  {
  }

  public abstract class scheduleType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.scheduleType>
  {
  }

  public abstract class dailyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.dailyFrequency>
  {
  }

  public abstract class dailyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.dailyLabel>
  {
  }

  public abstract class weeklyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.weeklyFrequency>
  {
  }

  public abstract class weeklyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.weeklyLabel>
  {
  }

  public abstract class weeklyOnDay1 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay1>
  {
  }

  public abstract class weeklyOnDay2 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay2>
  {
  }

  public abstract class weeklyOnDay3 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay3>
  {
  }

  public abstract class weeklyOnDay4 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay4>
  {
  }

  public abstract class weeklyOnDay5 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay5>
  {
  }

  public abstract class weeklyOnDay6 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay6>
  {
  }

  public abstract class weeklyOnDay7 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.weeklyOnDay7>
  {
  }

  public abstract class monthlyFrequency : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    AUSchedule.monthlyFrequency>
  {
  }

  public abstract class monthlyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.monthlyLabel>
  {
  }

  public abstract class monthlyDaySel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.monthlyDaySel>
  {
  }

  public abstract class monthlyOnDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.monthlyOnDay>
  {
  }

  public abstract class monthlyOnWeek : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.monthlyOnWeek>
  {
  }

  public abstract class monthlyOnDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    AUSchedule.monthlyOnDayOfWeek>
  {
  }

  public abstract class periodFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.periodFrequency>
  {
  }

  public abstract class periodLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.periodLabel>
  {
  }

  public abstract class periodDateSel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.periodDateSel>
  {
  }

  public abstract class periodFixedDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.periodFixedDay>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.startDate>
  {
  }

  public abstract class noEndDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.noEndDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.endDate>
  {
  }

  public abstract class noRunLimit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.noRunLimit>
  {
  }

  public abstract class runLimit : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.runLimit>
  {
  }

  public abstract class runCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUSchedule.runCntr>
  {
  }

  public abstract class nextRunDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.nextRunDate>
  {
  }

  public abstract class lastRunDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.lastRunDate>
  {
  }

  public abstract class lastRunStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.lastRunStatus>
  {
  }

  public abstract class lastRunErrorLevel : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    AUSchedule.lastRunErrorLevel>
  {
  }

  public abstract class lastRunResult : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUSchedule.lastRunResult>
  {
  }

  public abstract class startTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.startTime>
  {
  }

  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.endTime>
  {
  }

  public abstract class interval : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUSchedule.interval>
  {
  }

  public abstract class nextRunTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUSchedule.nextRunTime>
  {
  }

  public abstract class templateScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUSchedule.templateScreenID>
  {
  }

  public abstract class exactTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.exactTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUSchedule.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUSchedule.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUSchedule.tStamp>
  {
  }

  public abstract class historyRetainCount : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    AUSchedule.historyRetainCount>
  {
  }

  public abstract class keepFullHistory : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUSchedule.keepFullHistory>
  {
  }

  /// <summary>Represents merged NextRunDate and NextRunTime.</summary>
  public abstract class nextRunDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUSchedule.nextRunDateTime>
  {
  }
}
