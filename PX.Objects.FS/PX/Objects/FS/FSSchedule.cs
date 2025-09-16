// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(new System.Type[] {typeof (ServiceContractScheduleEntry), typeof (RouteServiceContractScheduleEntry)}, new System.Type[] {typeof (Search2<FSSchedule.refNbr, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSchedule.srvOrdType>>>, Where<FSSchedule.entityID, Equal<Current<FSSchedule.entityID>>, And<FSSrvOrdType.behavior, NotEqual<ListField.ServiceOrderTypeBehavior.routeAppointment>>>>), typeof (Search2<FSSchedule.refNbr, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSchedule.srvOrdType>>>, Where<FSSchedule.entityID, Equal<Current<FSSchedule.entityID>>, And<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>>>>)})]
[Serializable]
public class FSSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _EndDate;
  protected DateTime? _RestrictionMaxTime;
  protected DateTime? _RestrictionMinTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search3<FSSchedule.refNbr, OrderBy<Desc<FSSchedule.refNbr>>>))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? ScheduleID { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSSrvOrdType">customer
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSSchedule.srvOrdType), typeof (FSSchedule.noteID))]
  public virtual string[] Attributes { get; set; }

  [PXDefault(true)]
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDefault(1)]
  [PXDBShort(MinValue = 0)]
  [PXUIField(DisplayName = "Every")]
  public virtual short? AnnualFrequency { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? AnnualOnDay { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Day of Week")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault(1)]
  public virtual short? AnnualOnDayOfWeek { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? AnnualOnWeek { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string AnnualRecurrenceType { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "January")]
  [PXDefault(false)]
  public virtual bool? AnnualOnJan { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "February")]
  [PXDefault(false)]
  public virtual bool? AnnualOnFeb { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "March")]
  [PXDefault(false)]
  public virtual bool? AnnualOnMar { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "April")]
  [PXDefault(false)]
  public virtual bool? AnnualOnApr { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "May")]
  [PXDefault(false)]
  public virtual bool? AnnualOnMay { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "June")]
  [PXDefault(false)]
  public virtual bool? AnnualOnJun { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "July")]
  [PXDefault(false)]
  public virtual bool? AnnualOnJul { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "August")]
  [PXDefault(false)]
  public virtual bool? AnnualOnAug { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "September")]
  [PXDefault(false)]
  public virtual bool? AnnualOnSep { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "October")]
  [PXDefault(false)]
  public virtual bool? AnnualOnOct { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "November")]
  [PXDefault(false)]
  public virtual bool? AnnualOnNov { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "December")]
  [PXDefault(false)]
  public virtual bool? AnnualOnDec { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch ID")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Branch Location ID")]
  [FSSelectorBranchLocationByFSSchedule]
  [PXFormula(typeof (Default<FSSchedule.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [FSSelectorBAccountTypeCustomerOrCombined]
  public virtual int? CustomerID { get; set; }

  [PXDefault(1)]
  [PXDBShort(MinValue = 0)]
  [PXUIField(DisplayName = "Every")]
  public virtual short? DailyFrequency { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Expiration Date")]
  [PXFormula(typeof (Default<FSSchedule.enableExpirationDate>))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set
    {
      this.EndDateUTC = value;
      this._EndDate = value;
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Entity ID", Enabled = false)]
  public virtual int? EntityID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [ListField_Schedule_EntityType.ListAtrribute]
  [PXUIField]
  public virtual string EntityType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_FrequencyType.ListAtrribute]
  [PXDefault("D")]
  [PXUIField(DisplayName = "Frequency")]
  public virtual string FrequencyType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Generated")]
  [PXFormula(typeof (Default<FSSchedule.startDate>))]
  public virtual DateTime? LastGeneratedElementDate { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Second Recurrence")]
  public virtual bool? Monthly2Selected { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Third Recurrence")]
  public virtual bool? Monthly3Selected { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Fourth Recurrence")]
  public virtual bool? Monthly4Selected { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})]
  public virtual short? MonthlyFrequency { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string MonthlyRecurrenceType1 { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string MonthlyRecurrenceType2 { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string MonthlyRecurrenceType3 { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule On")]
  [PXStringList(new string[] {"D", "W"}, new string[] {"Fixed Day of Month", "Fixed Day of Week"})]
  [PXDefault("D")]
  public virtual string MonthlyRecurrenceType4 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay1 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay2 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay3 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On Day")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnDay4 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek1 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek2 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek3 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "On the")]
  [PXIntList(new int[] {1, 2, 3, 4, 5}, new string[] {"First", "Second", "Third", "Fourth", "Last"})]
  [PXDefault(1)]
  public virtual short? MonthlyOnWeek4 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Day of Week")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault(1)]
  public virtual short? MonthlyOnDayOfWeek1 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Day of Week")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault(1)]
  public virtual short? MonthlyOnDayOfWeek2 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Day of Week")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault(1)]
  public virtual short? MonthlyOnDayOfWeek3 { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Day of Week")]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault(1)]
  public virtual short? MonthlyOnDayOfWeek4 { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "No Limit")]
  public virtual bool? NoRunLimit { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Enable Max. Restriction")]
  public virtual bool? RestrictionMax { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Enable Min. Restriction")]
  public virtual bool? RestrictionMin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Maximum Time Restriction", DisplayNameTime = "Maximum Time Restriction")]
  [PXDefault]
  [PXUIField(DisplayName = "Maximum Time Restriction")]
  public virtual DateTime? RestrictionMaxTime
  {
    get => this._RestrictionMaxTime;
    set
    {
      this.RestrictionMaxTimeUTC = value;
      this._RestrictionMaxTime = value;
    }
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Minimum Time Restriction", DisplayNameTime = "Minimum Time Restriction")]
  [PXDefault]
  [PXUIField(DisplayName = "Minimum Time Restriction")]
  public virtual DateTime? RestrictionMinTime
  {
    get => this._RestrictionMinTime;
    set
    {
      this.RestrictionMinTimeUTC = value;
      this._RestrictionMinTime = value;
    }
  }

  [PXDefault(0)]
  [PXDBShort]
  [PXUIField(DisplayName = "Executed (times)")]
  public virtual short? RunCntr { get; set; }

  [PXDefault]
  [PXDBShort(MinValue = 0)]
  [PXUIField(DisplayName = "Execution Limit (times)", Enabled = false)]
  public virtual short? RunLimit { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type")]
  [PXDefault]
  public virtual string SrvOrdType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? StartDate { get; set; }

  [PXDBShort(MinValue = 0)]
  [PXUIField(DisplayName = "Every")]
  [PXDefault(1)]
  public virtual short? WeeklyFrequency { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Sunday")]
  [PXDefault(true)]
  public virtual bool? WeeklyOnSun { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Monday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnMon { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Tuesday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnTue { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Wednesday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnWed { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Thursday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnThu { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Friday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnFri { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Saturday")]
  [PXDefault(false)]
  public virtual bool? WeeklyOnSat { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor", Enabled = true)]
  [FSSelectorVendor]
  public virtual int? VendorID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vehicle Type ID")]
  [PXSelector(typeof (FSVehicleType.vehicleTypeID), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD))]
  public virtual int? VehicleTypeID { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Recurrence Description")]
  public virtual string RecurrenceDescription { get; set; }

  [PXDBInt]
  [FSSelector_Employee_All]
  [PXUIField(DisplayName = "Employee ID")]
  public virtual int? EmployeeID { get; set; }

  [ListField_ScheduleType.ListAtrribute]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Schedule Type")]
  public virtual string ScheduleType { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Week Codes e.g.: 1, 2B, 1ACS")]
  public virtual string WeekCode { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "January")]
  [PXDefault(true)]
  public virtual bool? SeasonOnJan { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "February")]
  [PXDefault(true)]
  public virtual bool? SeasonOnFeb { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "March")]
  [PXDefault(true)]
  public virtual bool? SeasonOnMar { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "April")]
  [PXDefault(true)]
  public virtual bool? SeasonOnApr { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "May")]
  [PXDefault(true)]
  public virtual bool? SeasonOnMay { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "June")]
  [PXDefault(true)]
  public virtual bool? SeasonOnJun { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "July")]
  [PXDefault(true)]
  public virtual bool? SeasonOnJul { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "August")]
  [PXDefault(true)]
  public virtual bool? SeasonOnAug { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "September")]
  [PXDefault(true)]
  public virtual bool? SeasonOnSep { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "October")]
  [PXDefault(true)]
  public virtual bool? SeasonOnOct { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "November")]
  [PXDefault(true)]
  public virtual bool? SeasonOnNov { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "December")]
  [PXDefault(true)]
  public virtual bool? SeasonOnDec { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Expiration Date")]
  public virtual bool? EnableExpirationDate { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string YearlyLabel => PXMessages.LocalizeNoPrefix("year(s)");

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string MonthlyLabel => PXMessages.LocalizeNoPrefix("month(s)");

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string WeeklyLabel => PXMessages.LocalizeNoPrefix("week(s)");

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string DailyLabel => PXMessages.LocalizeNoPrefix("day(s)");

  [PXString]
  [PXUIField]
  [PXDefault("This Service Order Type will be used for the recurring appointments")]
  public virtual string SrvOrdTypeMessage { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXString]
  [PXUIField]
  [PXDefault]
  public virtual string ContractDescr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Location")]
  [PXDimensionSelector("LOCATION", typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSSchedule.customerID>>>>), typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBString(2, IsUnicode = false)]
  [ListField_ScheduleGenType_ContractSchedule.ListAtrribute]
  [PXUIField(DisplayName = "Schedule Generation Type")]
  [PXDefault(typeof (ListField_ScheduleGenType_ContractSchedule.None))]
  public virtual string ScheduleGenType { get; set; }

  [PXDBDateAndTime(PreserveTime = true, DisplayNameTime = "Schedule Start Time")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Schedule Start Time")]
  public virtual DateTime? ScheduleStartTime { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Next Execution Date", Enabled = false)]
  [PXDefault]
  public virtual DateTime? NextExecutionDate { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Maximum Time Restriction", DisplayNameTime = "Maximum Time Restriction")]
  [PXUIField(DisplayName = "Maximum Time Restriction")]
  public virtual DateTime? RestrictionMaxTimeUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Minimum Time Restriction", DisplayNameTime = "Minimum Time Restriction")]
  [PXUIField(DisplayName = "Minimum Time Restriction")]
  public virtual DateTime? RestrictionMinTimeUTC { get; set; }

  [PXDBDate(UseTimeZone = false)]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? EndDateUTC { get; set; }

  [PXInt]
  [PXSelector(typeof (Search2<FSSchedule.refNbr, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>>>, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>, And<FSServiceContract.refNbr, Equal<Optional<FSServiceContract.refNbr>>, And<FSSchedule.customerID, Equal<Optional<FSSchedule.customerID>>>>>>), new System.Type[] {typeof (FSSchedule.refNbr), typeof (FSSchedule.customerID), typeof (FSSchedule.active), typeof (FSSchedule.customerLocationID)})]
  [PXUIField(DisplayName = "Schedule ID", Enabled = false)]
  public virtual int? ReportScheduleID { get; set; }

  [PXDefault]
  [ProjectBase(typeof (FSSchedule.billCustomerID), Enabled = false)]
  [PXForeignReference(typeof (FSSchedule.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Default<FSSchedule.projectID>))]
  [PXUIField(DisplayName = "Default Project Task", Enabled = false, FieldClass = "PROJECT")]
  [PXDefault]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSSchedule.projectID>>>))]
  [PXForeignReference(typeof (FSSchedule.FK.DefaultTask))]
  public virtual int? DfltProjectTaskID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideDuration { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Schedule Duration")]
  [PXDefault(0)]
  [PXUIEnabled(typeof (FSSchedule.overrideDuration))]
  [PXFormula(typeof (Switch<Case<Where<FSSchedule.overrideDuration, Equal<False>>, FSSchedule.estimatedDurationTotal>, FSSchedule.scheduleDuration>))]
  public virtual int? ScheduleDuration { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration Total")]
  [PXDefault(0)]
  public virtual int? EstimatedDurationTotal { get; set; }

  [PXString(15)]
  [PXUIField(DisplayName = "Orig. Schedule ID", Enabled = false)]
  public virtual string OrigScheduleRefNbr { get; set; }

  [PXString(15)]
  [PXUIField(DisplayName = "Orig. Service Contract ID", Enabled = false)]
  public virtual string OrigServiceContractRefNbr { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSServiceContract.billCustomerID, Where<FSServiceContract.serviceContractID, Equal<Current<FSSchedule.entityID>>, And<Current<FSSchedule.entityType>, Equal<ListField_Schedule_EntityType.Contract>>>>))]
  [PXUIField(DisplayName = "Billing Customer")]
  public virtual int? BillCustomerID { get; set; }

  public static bool TryParse(object row, out FSSchedule fsScheduleRow)
  {
    fsScheduleRow = (FSSchedule) null;
    if (!(row is FSSchedule))
      return false;
    fsScheduleRow = (FSSchedule) row;
    return true;
  }

  public class PK : PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>
  {
    public static FSSchedule Find(PXGraph graph, int? scheduleID, PKFindOptions options = 0)
    {
      return (FSSchedule) PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSSchedule>.By<FSSchedule.refNbr, FSSchedule.customerID>
  {
    public static FSSchedule Find(
      PXGraph graph,
      string refNbr,
      int? customerID,
      PKFindOptions options = 0)
    {
      return (FSSchedule) PrimaryKeyOf<FSSchedule>.By<FSSchedule.refNbr, FSSchedule.customerID>.FindBy(graph, (object) refNbr, (object) customerID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.branchLocationID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.customerID, FSSchedule.customerLocationID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.vendorID>
    {
    }

    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.vehicleTypeID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.employeeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.projectID>
    {
    }

    public class DefaultTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSSchedule>.By<FSSchedule.projectID, FSSchedule.dfltProjectTaskID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.refNbr>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.scheduleID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.active>
  {
  }

  public abstract class annualFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.annualFrequency>
  {
  }

  public abstract class annualOnDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.annualOnDay>
  {
  }

  public abstract class annualOnDayOfWeek : ListField_WeekDaysNumber
  {
  }

  public abstract class annualOnWeek : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.annualOnWeek>
  {
  }

  public abstract class annualRecurrenceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.annualRecurrenceType>
  {
  }

  public abstract class annualOnJan : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnJan>
  {
  }

  public abstract class annualOnFeb : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnFeb>
  {
  }

  public abstract class annualOnMar : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnMar>
  {
  }

  public abstract class annualOnApr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnApr>
  {
  }

  public abstract class annualOnMay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnMay>
  {
  }

  public abstract class annualOnJun : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnJun>
  {
  }

  public abstract class annualOnJul : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnJul>
  {
  }

  public abstract class annualOnAug : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnAug>
  {
  }

  public abstract class annualOnSep : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnSep>
  {
  }

  public abstract class annualOnOct : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnOct>
  {
  }

  public abstract class annualOnNov : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnNov>
  {
  }

  public abstract class annualOnDec : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.annualOnDec>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.branchID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.branchLocationID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.customerID>
  {
  }

  public abstract class dailyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.dailyFrequency>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSchedule.endDate>
  {
  }

  public abstract class entityID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.entityID>
  {
  }

  public abstract class entityType : ListField_Schedule_EntityType
  {
  }

  public abstract class frequencyType : ListField_FrequencyType
  {
  }

  public abstract class lastGeneratedElementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.lastGeneratedElementDate>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.lineCntr>
  {
  }

  public abstract class monthly2Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.monthly2Selected>
  {
  }

  public abstract class monthly3Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.monthly3Selected>
  {
  }

  public abstract class monthly4Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.monthly4Selected>
  {
  }

  public abstract class monthlyFrequency : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    FSSchedule.monthlyFrequency>
  {
  }

  public abstract class monthlyRecurrenceType1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.monthlyRecurrenceType1>
  {
  }

  public abstract class monthlyRecurrenceType2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.monthlyRecurrenceType2>
  {
  }

  public abstract class monthlyRecurrenceType3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.monthlyRecurrenceType3>
  {
  }

  public abstract class monthlyRecurrenceType4 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.monthlyRecurrenceType4>
  {
  }

  public abstract class monthlyOnDay1 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnDay1>
  {
  }

  public abstract class monthlyOnDay2 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnDay2>
  {
  }

  public abstract class monthlyOnDay3 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnDay3>
  {
  }

  public abstract class monthlyOnDay4 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnDay4>
  {
  }

  public abstract class monthlyOnWeek1 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnWeek1>
  {
  }

  public abstract class monthlyOnWeek2 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnWeek2>
  {
  }

  public abstract class monthlyOnWeek3 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnWeek3>
  {
  }

  public abstract class monthlyOnWeek4 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.monthlyOnWeek4>
  {
  }

  public abstract class monthlyOnDayOfWeek1 : ListField_WeekDaysNumber
  {
  }

  public abstract class monthlyOnDayOfWeek2 : ListField_WeekDaysNumber
  {
  }

  public abstract class monthlyOnDayOfWeek3 : ListField_WeekDaysNumber
  {
  }

  public abstract class monthlyOnDayOfWeek4 : ListField_WeekDaysNumber
  {
  }

  public abstract class noRunLimit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.noRunLimit>
  {
  }

  public abstract class restrictionMax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.restrictionMax>
  {
  }

  public abstract class restrictionMin : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.restrictionMin>
  {
  }

  public abstract class restrictionMaxTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.restrictionMaxTime>
  {
  }

  public abstract class restrictionMinTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.restrictionMinTime>
  {
  }

  public abstract class runCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.runCntr>
  {
  }

  public abstract class runLimit : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.runLimit>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.srvOrdType>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSchedule.startDate>
  {
  }

  public abstract class weeklyFrequency : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSSchedule.weeklyFrequency>
  {
  }

  public abstract class weeklyOnSun : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnSun>
  {
  }

  public abstract class weeklyOnMon : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnMon>
  {
  }

  public abstract class weeklyOnTue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnTue>
  {
  }

  public abstract class weeklyOnWed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnWed>
  {
  }

  public abstract class weeklyOnThu : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnThu>
  {
  }

  public abstract class weeklyOnFri : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnFri>
  {
  }

  public abstract class weeklyOnSat : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.weeklyOnSat>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.vendorID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSchedule.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSchedule.Tstamp>
  {
  }

  public abstract class vehicleTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.vehicleTypeID>
  {
  }

  public abstract class recurrenceDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.recurrenceDescription>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.employeeID>
  {
  }

  public abstract class scheduleType : ListField_ScheduleType
  {
  }

  public abstract class weekCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.weekCode>
  {
  }

  public abstract class seasonOnJan : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnJan>
  {
  }

  public abstract class seasonOnFeb : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnFeb>
  {
  }

  public abstract class seasonOnMar : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnMar>
  {
  }

  public abstract class seasonOnApr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnApr>
  {
  }

  public abstract class seasonOnMay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnMay>
  {
  }

  public abstract class seasonOnJun : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnJun>
  {
  }

  public abstract class seasonOnJul : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnJul>
  {
  }

  public abstract class seasonOnAug : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnAug>
  {
  }

  public abstract class seasonOnSep : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnSep>
  {
  }

  public abstract class seasonOnOct : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnOct>
  {
  }

  public abstract class seasonOnNov : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnNov>
  {
  }

  public abstract class seasonOnDec : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.seasonOnDec>
  {
  }

  public abstract class enableExpirationDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSchedule.enableExpirationDate>
  {
  }

  public abstract class yearlyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.yearlyLabel>
  {
  }

  public abstract class monthlyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.monthlyLabel>
  {
  }

  public abstract class weeklyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.weeklyLabel>
  {
  }

  public abstract class dailyLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.dailyLabel>
  {
  }

  public abstract class srvOrdTypeMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.srvOrdTypeMessage>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.selected>
  {
  }

  public abstract class contractDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSchedule.contractDescr>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSchedule.customerLocationID>
  {
  }

  public abstract class scheduleGenType : ListField_ScheduleGenType_ContractSchedule
  {
  }

  public abstract class scheduleStartTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.scheduleStartTime>
  {
  }

  public abstract class nextExecutionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.nextExecutionDate>
  {
  }

  public abstract class restrictionMaxTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.restrictionMaxTimeUTC>
  {
  }

  public abstract class restrictionMinTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSchedule.restrictionMinTimeUTC>
  {
  }

  public abstract class endDateUTC : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSSchedule.endDateUTC>
  {
  }

  public abstract class reportScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.reportScheduleID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.projectID>
  {
  }

  public abstract class dfltProjectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.dfltProjectTaskID>
  {
  }

  public abstract class overrideDuration : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSchedule.overrideDuration>
  {
  }

  public abstract class scheduleDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.scheduleDuration>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSchedule.estimatedDurationTotal>
  {
  }

  public abstract class origScheduleRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.origScheduleRefNbr>
  {
  }

  public abstract class origServiceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSchedule.origServiceContractRefNbr>
  {
  }

  public abstract class billCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSchedule.billCustomerID>
  {
  }
}
