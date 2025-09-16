// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSetup
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Management Preferences")]
[PXPrimaryGraph(typeof (SetupMaint))]
[Serializable]
public class FSSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string ServiceManagementFieldClass = "SERVICEMANAGEMENT";
  public const string EquipmentManagementFieldClass = "EQUIPMENTMANAGEMENT";
  public const string RouteManagementFieldClass = "ROUTEMANAGEMENT";
  protected string _ScheduleNumberingID;
  protected string _ServiceContractNumberingID;

  [PXDBTimeSpanLong]
  [PXDefault(720)]
  [PXUIField(DisplayName = "Appointment Auto-Confirm Time")]
  public virtual int? AppAutoConfirmGap { get; set; }

  [PXDBInt]
  [PXDefault(30)]
  [ListField_AppResizePrecision.ListAtrribute]
  [PXUIField(DisplayName = "Appointment Resize Precision")]
  public virtual int? AppResizePrecision { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Work Calendar")]
  [PXSelector(typeof (CSCalendar.calendarID), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID { get; set; }

  [PXDefault(14)]
  [PXDBInt(MinValue = 0, MaxValue = 1000)]
  [PXUIField(DisplayName = "Show Service Orders in a Period Of", Visible = true)]
  public virtual int? ShowServiceOrderDaysGap { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_AppointmentValidation.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Service Areas")]
  public virtual string DenyWarnByGeoZone { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_AppointmentValidation.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Licenses")]
  public virtual string DenyWarnByLicense { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_AppointmentValidation.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Skills")]
  public virtual string DenyWarnBySkill { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("BA")]
  [PXUIField(DisplayName = "Default Appointment Contact Info Source", Visible = false)]
  public virtual string DfltAppContactInfoSource { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Employee Schedule Precision", Visible = false)]
  public virtual int? EmpSchedulePrecision { get; set; }

  [PXDBString(4, InputMask = "9999")]
  [PXDefault("1")]
  [PXUIField(DisplayName = "Initial Appointment Ref. Nbr.")]
  public virtual string InitialAppRefNbr { get; set; }

  [PXDBString(10)]
  [ListField_SrvOrdType_NewBusinessAcctType.ListAtrribute]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Default Business Account Type")]
  public virtual string DfltBusinessAcctType { get; set; }

  [PXDBString(4, IsUnicode = false, IsFixed = true)]
  [PXUIField(DisplayName = "Default Service Order Type")]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new Type[] {})]
  [FSSelectorSrvOrdTypeNOTQuote]
  public virtual string DfltSrvOrdType { get; set; }

  [PXDBString(4, IsUnicode = false, IsFixed = true)]
  [PXUIField(DisplayName = "Default Service Order Type for Sales Orders")]
  [FSSelectorActiveSrvOrdType]
  public virtual string DfltSOSrvOrdType { get; set; }

  [PXDBString(2, IsUnicode = false, IsFixed = true)]
  [PXDefault("HO")]
  [PXUIField(DisplayName = "View Mode")]
  [ListField_DfltCalendarViewMode.ListAtrribute]
  public virtual string DfltCalendarViewMode { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Number of Staff Members")]
  public virtual int? DfltCalendarPageSize { get; set; }

  [PXDBString(4, IsUnicode = false, IsFixed = true)]
  [PXUIField(DisplayName = "Default Service Order Type for Cases")]
  [FSSelectorActiveSrvOrdType]
  public virtual string DfltCasesSrvOrdType { get; set; }

  [PXDBString(4, IsUnicode = false)]
  [PXUIField(DisplayName = "Default Opportunities Service Order Type")]
  [FSSelectorActiveSrvOrdType]
  public virtual string DfltOpportunitySrvOrdType { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Number of days ahead for recurring appointments", Visible = false)]
  public virtual int? DaysAheadRecurringAppointments { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_AppointmentValidation.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Equipments")]
  public virtual string DenyWarnByEquipment { get; set; }

  [PXDBString(10)]
  [PXDefault]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Staff Schedule Numbering Sequence")]
  public virtual string EmpSchdlNumberingID { get; set; }

  [PXDBString(10)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "License Numbering Sequence")]
  public virtual string LicenseNumberingID { get; set; }

  [PXDBString(10)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Equipment Numbering Sequence")]
  public virtual string EquipmentNumberingID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_AppointmentValidation.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Overlapping Appointments")]
  public virtual string DenyWarnByAppOverlap { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Rooms")]
  public virtual bool? ManageRooms { get; set; }

  [PXInt]
  public virtual int? DfltBranchID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Time & Expenses Integration")]
  [PXUIVisible(typeof (IIf<FeatureInstalled<FeaturesSet.timeReportingModule>, True, False>))]
  public virtual bool? EnableEmpTimeCardIntegration { get; set; }

  [PXDBString(10)]
  [PXDefault]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Batch Numbering Sequence")]
  public virtual string PostBatchNumberingID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("FSSCHEDULE")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Service Contract Schedule Numbering Sequence")]
  public virtual string ScheduleNumberingID
  {
    get => this._ScheduleNumberingID;
    set => this._ScheduleNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("FSCONTRACT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Service Contract Numbering Sequence")]
  public virtual string ServiceContractNumberingID
  {
    get => this._ServiceContractNumberingID;
    set => this._ServiceContractNumberingID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manage Multiple Billing Options per Customer")]
  public virtual bool? CustomerMultipleBillingOptions { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Alert About Open Appointments Before Service Orders Are Closed")]
  public virtual bool? AlertBeforeCloseServiceOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Manual Filtering on Billing Forms")]
  public virtual bool? FilterInvoicingManually { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Seasons in Schedule Contracts")]
  public virtual bool? EnableSeasonScheduleContract { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SD")]
  [PXUIField(DisplayName = "Calculate Warranty From")]
  [EquipmentCalculateWarrantyType.List]
  public virtual string EquipmentCalculateWarrantyFrom { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameTime = "Day Start Time")]
  [PXUIField(DisplayName = "Day Start Time")]
  [PXDefault]
  public virtual DateTime? DfltCalendarStartTime { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameTime = "Day End Time")]
  [PXUIField(DisplayName = "Day End Time")]
  [PXDefault]
  public virtual DateTime? DfltCalendarEndTime { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [ListField_TimeRange_Setup.ListAtrribute]
  [PXUIField(DisplayName = "Time Range")]
  public virtual string TimeRange { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CF")]
  [ListField_TimeFilter_Setup.ListAtrribute]
  [PXUIField(DisplayName = "Time Filter")]
  public virtual string TimeFilter { get; set; }

  [PXDBInt]
  [PXDefault(16 /*0x10*/)]
  [ListField_DayResolution_Setup.ListAtrribute]
  [PXUIField(DisplayName = "Day Resolution")]
  public virtual int? DayResolution { get; set; }

  [PXDBInt]
  [PXDefault(12)]
  [ListField_WeekResolution_Setup.ListAtrribute]
  [PXUIField(DisplayName = "Week Resolution")]
  public virtual int? WeekResolution { get; set; }

  [PXDBInt]
  [PXDefault(10)]
  [ListField_MonthResolution_Setup.ListAtrribute]
  [PXUIField(DisplayName = "Month Resolution")]
  public virtual int? MonthResolution { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Map API Key")]
  public virtual string MapApiKey { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Start and Completion Appointment Locations")]
  public virtual bool? TrackAppointmentLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Location Tracking")]
  public virtual bool? EnableGPSTracking { get; set; }

  [PXDBInt]
  [PXDefault(30)]
  [PXUIField(DisplayName = "Refresh GPS Locations Every")]
  public virtual int? GPSRefreshTrackingTime { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "History Distance Accuracy")]
  public virtual int? HistoryDistanceAccuracy { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(15)]
  [PXUIField(DisplayName = "History Time Accuracy")]
  public virtual int? HistoryTimeAccuracy { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Fix Schedules Without Next Execution Date")]
  public virtual bool? DisableFixScheduleAction { get; set; }

  [PXDBString(2)]
  [PXDefault("AR")]
  [FSPostTo.List]
  [PXUIField(DisplayName = "Generated Billing Documents")]
  public virtual string ContractPostTo { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string DfltContractTermIDARSO { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<FSxSOOrderType.enableFSIntegration, Equal<True>>>>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string ContractPostOrderType { get; set; }

  [PXDefault]
  [SubAccountMask(true, DisplayName = "Combine Sales Sub. From")]
  public virtual string ContractCombineSubFrom { get; set; }

  [PXDBString(2)]
  [ListField_Contract_SalesAcctSource.ListAtrribute]
  [PXDefault("CL")]
  [PXUIField(DisplayName = "Use Sales Account From")]
  public virtual string ContractSalesAcctSource { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Automatically Activate Upcoming Period")]
  public virtual bool? EnableContractPeriodWhenInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ShowWorkflowStageField { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Service on All Target Equipment")]
  public virtual bool? EnableAllTargetEquipment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Default Staff in Service Orders")]
  public virtual bool? EnableDfltStaffOnServiceOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Default Resource Equipment in Service Orders")]
  public virtual bool? EnableDfltResEquipOnServiceOrder { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? ReadyToUpgradeTo2017R2 { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField]
  public virtual string ROWWApiEndPoint { get; set; }

  [PXRSACryptString(1024 /*0x0400*/, IsUnicode = true)]
  [PXUIField]
  public virtual string ROWWLicensekey { get; set; }

  [PXDBTimeSpanLong]
  [PXDefault(60)]
  [PXUIField(DisplayName = "Lunch Break Duration", FieldClass = "ROUTEOPTIMIZER")]
  public virtual int? ROLunchBreakDuration { get; set; }

  [PXDefault]
  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Lunch Break Start Date", DisplayNameTime = "Lunch Break Start Time")]
  [PXUIField(DisplayName = "Lunch Break Start Time", FieldClass = "ROUTEOPTIMIZER")]
  public virtual DateTime? ROLunchBreakStartTimeFrame { get; set; }

  [PXDefault]
  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Lunch Break End Date", DisplayNameTime = "Lunch Break End Time")]
  [PXUIField(DisplayName = "Lunch Break End Time", FieldClass = "ROUTEOPTIMIZER")]
  public virtual DateTime? ROLunchBreakEndTimeFrame { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXString]
  public virtual string CustomDfltCalendarStartTime
  {
    get
    {
      return this.DfltCalendarStartTime.HasValue ? this.DfltCalendarStartTime.ToString() : string.Empty;
    }
  }

  [PXString]
  public virtual string CustomDfltCalendarEndTime
  {
    get => this.DfltCalendarEndTime.HasValue ? this.DfltCalendarEndTime.ToString() : string.Empty;
  }

  public static class FK
  {
    public class Calendar : 
      PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>.ForeignKeyOf<FSSetup>.By<FSSetup.calendarID>
    {
    }

    public class DfltServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSetup>.By<FSSetup.dfltSrvOrdType>
    {
    }

    public class DfltSOServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSetup>.By<FSSetup.dfltSOSrvOrdType>
    {
    }

    public class DfltCasesServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSetup>.By<FSSetup.dfltCasesSrvOrdType>
    {
    }

    public class DfltOpportunityServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSSetup>.By<FSSetup.dfltOpportunitySrvOrdType>
    {
    }

    public class EmpSchdlNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.empSchdlNumberingID>
    {
    }

    public class LicenseNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.licenseNumberingID>
    {
    }

    public class EquipmentNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.equipmentNumberingID>
    {
    }

    public class PostBatchNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.postBatchNumberingID>
    {
    }

    public class ScheduleNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.scheduleNumberingID>
    {
    }

    public class ServiceContractNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<FSSetup>.By<FSSetup.serviceContractNumberingID>
    {
    }
  }

  public abstract class appAutoConfirmGap : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSetup.appAutoConfirmGap>
  {
  }

  public abstract class appResizePrecision : ListField_AppResizePrecision
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.calendarID>
  {
  }

  public abstract class showServiceOrderDaysGap : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.showServiceOrderDaysGap>
  {
  }

  public abstract class denyWarnByGeoZone : ListField_AppointmentValidation
  {
  }

  public abstract class denyWarnByLicense : ListField_AppointmentValidation
  {
  }

  public abstract class denyWarnBySkill : ListField_AppointmentValidation
  {
  }

  public abstract class dfltAppContactInfoSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltAppContactInfoSource>
  {
  }

  public abstract class empSchedulePrecision : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.empSchedulePrecision>
  {
  }

  public abstract class initialAppRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.initialAppRefNbr>
  {
  }

  public abstract class dfltBusinessAcctType : ListField_SrvOrdType_NewBusinessAcctType
  {
  }

  public abstract class dfltSrvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.dfltSrvOrdType>
  {
  }

  public abstract class dfltSOSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltSOSrvOrdType>
  {
  }

  public abstract class dfltCalendarViewMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltCalendarViewMode>
  {
  }

  public abstract class dfltCalendarPageSize : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.dfltCalendarPageSize>
  {
  }

  public abstract class dfltCasesSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltCasesSrvOrdType>
  {
  }

  public abstract class dfltOpportunitySrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltOpportunitySrvOrdType>
  {
  }

  public abstract class daysAheadRecurringAppointments : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.daysAheadRecurringAppointments>
  {
  }

  public abstract class denyWarnByEquipment : ListField_AppointmentValidation
  {
  }

  public abstract class empSchdlNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.empSchdlNumberingID>
  {
  }

  public abstract class licenseNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.licenseNumberingID>
  {
  }

  public abstract class equipmentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.equipmentNumberingID>
  {
  }

  public abstract class denyWarnByAppOverlap : ListField_AppointmentValidation
  {
  }

  public abstract class manageRooms : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSetup.manageRooms>
  {
  }

  public abstract class dfltBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSetup.dfltBranchID>
  {
  }

  public abstract class enableEmpTimeCardIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableEmpTimeCardIntegration>
  {
  }

  public abstract class postBatchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.postBatchNumberingID>
  {
  }

  public abstract class scheduleNumberingID : IBqlField, IBqlOperand
  {
  }

  public abstract class serviceContractNumberingID : IBqlField, IBqlOperand
  {
  }

  public abstract class customerMultipleBillingOptions : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.customerMultipleBillingOptions>
  {
  }

  public abstract class alertBeforeCloseServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.alertBeforeCloseServiceOrder>
  {
  }

  public abstract class filterInvoicingManually : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.filterInvoicingManually>
  {
  }

  public abstract class enableSeasonScheduleContract : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableSeasonScheduleContract>
  {
  }

  public abstract class equipmentCalculateWarrantyFrom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.equipmentCalculateWarrantyFrom>
  {
  }

  public abstract class dfltCalendarStartTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.dfltCalendarStartTime>
  {
  }

  public abstract class dfltCalendarEndTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.dfltCalendarEndTime>
  {
  }

  public abstract class timeRange : ListField_TimeRange_Setup
  {
  }

  public abstract class timeFilter : ListField_TimeFilter_Setup
  {
  }

  public abstract class dayResolution : ListField_DayResolution_Setup
  {
  }

  public abstract class weekResolution : ListField_WeekResolution_Setup
  {
  }

  public abstract class monthResolution : ListField_MonthResolution_Setup
  {
  }

  public abstract class mapApiKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.mapApiKey>
  {
  }

  public abstract class trackAppointmentLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.trackAppointmentLocation>
  {
  }

  public abstract class enableGPSTracking : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSetup.enableGPSTracking>
  {
  }

  public abstract class gPSRefreshTrackingTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.gPSRefreshTrackingTime>
  {
  }

  public abstract class historyDistanceAccuracy : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.gPSRefreshTrackingTime>
  {
  }

  public abstract class historyTimeAccuracy : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.gPSRefreshTrackingTime>
  {
  }

  public abstract class disableFixScheduleAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.disableFixScheduleAction>
  {
  }

  public abstract class contractPostTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.contractPostTo>
  {
  }

  public abstract class dfltContractTermIDARSO : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.dfltContractTermIDARSO>
  {
  }

  public abstract class contractPostOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.contractPostOrderType>
  {
  }

  public abstract class contractCombineSubFrom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.contractCombineSubFrom>
  {
  }

  public abstract class contractSalesAcctSource : ListField_Contract_SalesAcctSource
  {
  }

  public abstract class enableContractPeriodWhenInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableContractPeriodWhenInvoice>
  {
  }

  public abstract class showWorkflowStageField : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.showWorkflowStageField>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSetup.Tstamp>
  {
  }

  public abstract class enableAllTargetEquipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableAllTargetEquipment>
  {
  }

  public abstract class enableDfltStaffOnServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableDfltStaffOnServiceOrder>
  {
  }

  public abstract class enableDfltResEquipOnServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.enableDfltResEquipOnServiceOrder>
  {
  }

  public abstract class readyToUpgradeTo2017R2 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSSetup.readyToUpgradeTo2017R2>
  {
  }

  public abstract class rOWWApiEndPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.rOWWApiEndPoint>
  {
  }

  public abstract class rOWWLicensekey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSetup.rOWWLicensekey>
  {
  }

  public abstract class rOLunchBreakDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSetup.rOLunchBreakDuration>
  {
  }

  public abstract class rOLunchBreakStartTimeFrame : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.rOLunchBreakStartTimeFrame>
  {
  }

  public abstract class rOLunchBreakEndTimeFrame : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSetup.rOLunchBreakEndTimeFrame>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSetup.noteID>
  {
  }

  public abstract class customDfltCalendarStartTime : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.customDfltCalendarStartTime>
  {
  }

  public abstract class customDfltCalendarEndTime : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSetup.customDfltCalendarEndTime>
  {
  }
}
