// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores different options that apply to the entire Time and Expenses functional area.
/// The information will be displayed on the Time and Expenses Preferences (EP101000) form.
/// </summary>
[PXPrimaryGraph(typeof (EPSetupMaint))]
[PXCacheName("Time & Expenses Preferences")]
[Serializable]
public class EPSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  public const 
  #nullable disable
  string Minute = "MINUTE";
  public const string Hour = "HOUR";
  protected string _ClaimNumberingID;
  protected short? _PerRetainTran;
  protected short? _PerRetainHist;
  protected bool? _HoldEntry;
  protected bool? _CopyNotesAR;
  protected bool? _CopyFilesAR;
  protected bool? _CopyNotesAP;
  protected bool? _CopyFilesAP;
  protected bool? _CopyNotesPM;
  protected bool? _CopyFilesPM;
  protected bool? _AutomaticReleaseAP;
  protected bool? _AutomaticReleaseAR;
  protected bool? _AutomaticReleasePM;
  protected int? _ClaimAssignmentMapID;
  protected int? _ClaimAssignmentNotificationID;
  protected string _ExpenseSubMask;
  protected string _SalesSubMask;
  protected bool? _SendOnlyEventCard;
  protected bool? _IsSimpleNotification;
  protected bool? _AddContactInformation;
  protected bool? _SearchOnlyInWorkingTime;
  protected bool? _RequireTimes;
  protected int? _TimeCardAssignmentNotificationID;
  protected int? _EquipmentTimeCardAssignmentNotificationID;
  protected int? _OffBalanceAccountGroupID;
  protected bool? _CustomWeek;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("EPCLAIM")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ClaimNumberingID
  {
    get => this._ClaimNumberingID;
    set => this._ClaimNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("EPRECEIPT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ReceiptNumberingID { get; set; }

  [PXDBShort]
  [PXDefault(99)]
  [PXUIField]
  public virtual short? PerRetainTran
  {
    get => this._PerRetainTran;
    set => this._PerRetainTran = value;
  }

  [PXDBShort]
  [PXDefault(120)]
  [PXUIField]
  public virtual short? PerRetainHist
  {
    get => this._PerRetainHist;
    set => this._PerRetainHist = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Expense Claims on Entry")]
  public virtual bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Post Summarized Company Expenses by Corporate Cards")]
  public virtual bool? PostSummarizedCorpCardExpenseReceipts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Ref. Nbr. in Expense Receipts")]
  public virtual bool? RequireRefNbrInExpenseReceipts { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Notes to AR Documents")]
  public virtual bool? CopyNotesAR
  {
    get => this._CopyNotesAR;
    set => this._CopyNotesAR = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Files to AR Documents")]
  public virtual bool? CopyFilesAR
  {
    get => this._CopyFilesAR;
    set => this._CopyFilesAR = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Notes to AP Documents")]
  public virtual bool? CopyNotesAP
  {
    get => this._CopyNotesAP;
    set => this._CopyNotesAP = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Files to AP Documents")]
  public virtual bool? CopyFilesAP
  {
    get => this._CopyFilesAP;
    set => this._CopyFilesAP = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Notes to PM Documents")]
  public virtual bool? CopyNotesPM
  {
    get => this._CopyNotesPM;
    set => this._CopyNotesPM = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Copy Files to PM Documents")]
  public virtual bool? CopyFilesPM
  {
    get => this._CopyFilesPM;
    set => this._CopyFilesPM = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Automatically Release AP Documents")]
  public virtual bool? AutomaticReleaseAP
  {
    get => this._AutomaticReleaseAP;
    set => this._AutomaticReleaseAP = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Automatically Release AR Documents")]
  public virtual bool? AutomaticReleaseAR
  {
    get => this._AutomaticReleaseAR;
    set => this._AutomaticReleaseAR = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Automatically Release PM Documents")]
  public virtual bool? AutomaticReleasePM
  {
    get => this._AutomaticReleasePM;
    set => this._AutomaticReleasePM = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeExpenceClaim>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Expense Claim Approval Map")]
  public virtual int? ClaimAssignmentMapID
  {
    get => this._ClaimAssignmentMapID;
    set => this._ClaimAssignmentMapID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Expense Claim Notification")]
  public virtual int? ClaimAssignmentNotificationID
  {
    get => this._ClaimAssignmentNotificationID;
    set => this._ClaimAssignmentNotificationID = value;
  }

  /// <summary>
  /// The subaccount mask that defines the rule of selecting segment values for the billable expense subaccount
  /// to be used on the data entry forms related to employee time and expenses.
  /// </summary>
  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Billable Expense Sub. From")]
  public virtual string ExpenseSubMask
  {
    get => this._ExpenseSubMask;
    set => this._ExpenseSubMask = value;
  }

  /// <summary>
  /// The subaccount mask that defines the rule of selecting segment values for the non-billable expense subaccount
  /// to be used on the data entry forms related to employee time and expenses.
  /// </summary>
  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Non-Billable Expense Sub. From")]
  public virtual string ExpenseSubMaskNB { get; set; }

  [PXDefault]
  [SubAccountMask(DisplayName = "Combine Sales Sub. From")]
  public virtual string SalesSubMask
  {
    get => this._SalesSubMask;
    set => this._SalesSubMask = value;
  }

  [PXDefault]
  [NonStockItem(DisplayName = "Non-Taxable Tip Item", Required = false)]
  public virtual int? NonTaxableTipItem { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Receipt Accounts for Tips")]
  [PXDefault(true)]
  public virtual bool? UseReceiptAccountForTips { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Allow Mixed Tax Settings in Claims")]
  [PXDefault(false)]
  public virtual bool? AllowMixedTaxSettingInClaims { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Only iCalendar Card")]
  [PXDefault(false)]
  public virtual bool? SendOnlyEventCard
  {
    get => this._SendOnlyEventCard;
    set => this._SendOnlyEventCard = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Simple Notification")]
  [PXDefault(true)]
  public virtual bool? IsSimpleNotification
  {
    get => this._IsSimpleNotification;
    set => this._IsSimpleNotification = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Add Contact Information")]
  [PXDefault(false)]
  public virtual bool? AddContactInformation
  {
    get => this._AddContactInformation;
    set => this._AddContactInformation = value;
  }

  [EmailNotification(DisplayName = "Invitation Template")]
  public virtual int? InvitationTemplateID { get; set; }

  [EmailNotification(DisplayName = "Reschedule Template")]
  public virtual int? RescheduleTemplateID { get; set; }

  [EmailNotification(DisplayName = "Cancel Invitation Template")]
  public virtual int? CancelInvitationTemplateID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Search Only In Working Time")]
  [PXDefault(false)]
  public virtual bool? SearchOnlyInWorkingTime
  {
    get => this._SearchOnlyInWorkingTime;
    set => this._SearchOnlyInWorkingTime = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Require Time On Activity")]
  [PXDefault(true)]
  public virtual bool? RequireTimes
  {
    get => this._RequireTimes;
    set => this._RequireTimes = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault("TIMECARD")]
  [PXUIField(DisplayName = "Time Card Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string TimeCardNumberingID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeTimeCard>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Time Card Approval Map")]
  public virtual int? TimeCardAssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Time Card Notification")]
  public virtual int? TimeCardAssignmentNotificationID
  {
    get => this._TimeCardAssignmentNotificationID;
    set => this._TimeCardAssignmentNotificationID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault("EQTIMECARD")]
  [PXUIField(DisplayName = "Equipment Time Card Numbering Sequence", FieldClass = "PROJECT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string EquipmentTimeCardNumberingID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeEquipmentTimeCard>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Equipment Time Card Approval Map", FieldClass = "PROJECT")]
  public virtual int? EquipmentTimeCardAssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Equipment Time Card Notification", FieldClass = "PROJECT")]
  public virtual int? EquipmentTimeCardAssignmentNotificationID
  {
    get => this._EquipmentTimeCardAssignmentNotificationID;
    set => this._EquipmentTimeCardAssignmentNotificationID = value;
  }

  [PXDefault("MINUTE")]
  [INUnit(DisplayName = "Activity Time Unit", Visible = false)]
  public virtual string ActivityTimeUnit { get; set; }

  [PXDefault("HOUR")]
  [INUnit(DisplayName = "Employee Hour Rate Unit", Visible = false)]
  public virtual string EmployeeRateUnit { get; set; }

  [PXDBString(3, IsUnicode = true)]
  [PXDefault("N")]
  [EPGroupTransaction.ListAttribule]
  [PXUIField]
  public virtual string GroupTransactgion { get; set; }

  [PXDBString(5, IsUnicode = false, IsFixed = true)]
  [PXUIRequired(typeof (IIf<FeatureInstalled<FeaturesSet.timeReportingModule>, True, False>))]
  [PXDefault("W")]
  [PXSelector(typeof (Search<EPActivityType.type, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPActivityType.application, Equal<PXActivityApplicationAttribute.backend>>>>>.Or<BqlOperand<EPActivityType.isSystem, IBqlBool>.IsEqual<True>>>>), DescriptionField = typeof (EPActivityType.description))]
  [PXRestrictor(typeof (Where<EPActivityType.active, Equal<True>>), "Activity Type '{0}' is not active.", new System.Type[] {typeof (EPActivityType.type)})]
  [PXRestrictor(typeof (Where<BqlOperand<EPActivityType.application, IBqlInt>.IsNotEqual<PXActivityApplicationAttribute.system>>), "The '{0}' activity type is originated by System. Only System can create activities of this type.\r\n ", new System.Type[] {typeof (EPActivityType.type)})]
  [PXUIField]
  public virtual string DefaultActivityType { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXTimeList(5, 12)]
  [PXUIField]
  public virtual int? MinBillableTime { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEarningType.typeCD, Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeRG>>>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Regular Hours Earning Type")]
  public virtual string RegularHoursType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEarningType.typeCD, Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeHL>>>))]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Holiday Earning Type")]
  public virtual string HolidaysType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEarningType.typeCD, Where<EPEarningType.typeCD, Equal<EPSetup.EarningTypeVL>>>))]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Vacations Earning Type")]
  public virtual string VacationsType { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Preload Holidays on Time Card Entry", Visible = false)]
  [PXDefault(false)]
  public virtual bool? isPreloadHolidays { get; set; }

  [PXDBLong]
  [PXUIField(DisplayName = "Default Task Filter")]
  [PXSelector(typeof (Search<FilterHeader.filterID, Where<FilterHeader.isShared, Equal<True>>>), DescriptionField = typeof (FilterHeader.filterName))]
  public virtual long? DefTasksFilterID { get; set; }

  [PXDBLong]
  [PXUIField(DisplayName = "Default Event Filter")]
  [PXSelector(typeof (Search<FilterHeader.filterID, Where<FilterHeader.isShared, Equal<True>>>), DescriptionField = typeof (FilterHeader.filterName))]
  public virtual long? DefEventsFilterID { get; set; }

  [PXDBString(1, IsUnicode = false, IsFixed = true)]
  [PXDefault("P")]
  [EPPostOptions.List]
  [PXUIField(DisplayName = "Time Posting Option")]
  public virtual string PostingOption { get; set; }

  [AccountGroup(DisplayName = "Off-Balance Account Group")]
  public virtual int? OffBalanceAccountGroupID
  {
    get => this._OffBalanceAccountGroupID;
    set => this._OffBalanceAccountGroupID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Custom Week Configuration")]
  [PXDefault(false)]
  public virtual bool? CustomWeek
  {
    get => this._CustomWeek;
    set => this._CustomWeek = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "First Custom Week ID")]
  [PXDBScalar(typeof (FbqlSelect<SelectFromBase<EPCustomWeek, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<Asc<EPCustomWeek.weekID>>>, EPCustomWeek>.SearchFor<EPCustomWeek.weekID>))]
  public virtual int? FirstCustomWeekID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Last Custom Week ID")]
  [PXDBScalar(typeof (FbqlSelect<SelectFromBase<EPCustomWeek, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<Desc<EPCustomWeek.weekID>>>, EPCustomWeek>.SearchFor<EPCustomWeek.weekID>))]
  public virtual int? LastCustomWeekID { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6}, new string[] {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})]
  [PXUIField(DisplayName = "First Day of Week")]
  [PXUIEnabled(typeof (EPSetup.customWeek))]
  [PXDefault(0)]
  public virtual int? FirstDayOfWeek { get; set; }

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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeExpenceClaimDetails>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Expense Receipt Approval Map")]
  public virtual int? ClaimDetailsAssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<Notification.notificationID>), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Expense Receipt Notification")]
  public virtual int? ClaimDetailsAssignmentNotificationID { get; set; }

  [PXInt]
  public int? AssignmentMapID { get; set; }

  [PXInt]
  public int? AssignmentNotificationID { get; set; }

  [PXBool]
  public bool? IsActive { get; set; }

  public PXNoteAttribute.IPXCopySettings GetCopyNoteSettings<TModule>()
  {
    if (typeof (TModule) == typeof (PXModule.ar))
      return (PXNoteAttribute.IPXCopySettings) new EPSetup.CopyNoteSettings(this.CopyNotesAR, this.CopyFilesAR);
    if (typeof (TModule) == typeof (PXModule.ap))
      return (PXNoteAttribute.IPXCopySettings) new EPSetup.CopyNoteSettings(this.CopyNotesAP, this.CopyFilesAP);
    return !(typeof (TModule) == typeof (PXModule.pm)) ? (PXNoteAttribute.IPXCopySettings) new EPSetup.CopyNoteSettings() : (PXNoteAttribute.IPXCopySettings) new EPSetup.CopyNoteSettings(this.CopyNotesPM, this.CopyFilesPM);
  }

  public abstract class claimNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.claimNumberingID>
  {
  }

  public abstract class receiptNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.receiptNumberingID>
  {
  }

  public abstract class perRetainTran : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  EPSetup.perRetainTran>
  {
  }

  public abstract class perRetainHist : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  EPSetup.perRetainHist>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.holdEntry>
  {
  }

  public abstract class postSummarizedCorpCardExpenseReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.postSummarizedCorpCardExpenseReceipts>
  {
  }

  public abstract class requireRefNbrInExpenseReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.requireRefNbrInExpenseReceipts>
  {
  }

  public abstract class copyNotesAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyNotesAR>
  {
  }

  public abstract class copyFilesAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyFilesAR>
  {
  }

  public abstract class copyNotesAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyNotesAP>
  {
  }

  public abstract class copyFilesAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyFilesAP>
  {
  }

  public abstract class copyNotesPM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyNotesPM>
  {
  }

  public abstract class copyFilesPM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.copyFilesPM>
  {
  }

  public abstract class automaticReleaseAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.automaticReleaseAP>
  {
  }

  public abstract class automaticReleaseAR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.automaticReleaseAR>
  {
  }

  public abstract class automaticReleasePM : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.automaticReleasePM>
  {
  }

  public abstract class claimAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.claimAssignmentMapID>
  {
  }

  public abstract class claimAssignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.claimAssignmentNotificationID>
  {
  }

  public abstract class expenseSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSetup.expenseSubMask>
  {
  }

  public abstract class expenseSubMaskNB : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.expenseSubMaskNB>
  {
  }

  public abstract class salesSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSetup.salesSubMask>
  {
  }

  public abstract class nonTaxableTipItem : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSetup.nonTaxableTipItem>
  {
  }

  public abstract class useReceiptAccountForTips : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.useReceiptAccountForTips>
  {
  }

  public abstract class allowMixedTaxSettingInClaims : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.allowMixedTaxSettingInClaims>
  {
  }

  public abstract class sendOnlyEventCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.sendOnlyEventCard>
  {
  }

  public abstract class isSimpleNotification : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.isSimpleNotification>
  {
  }

  public abstract class addContactInformation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.addContactInformation>
  {
  }

  public abstract class invitationTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.invitationTemplateID>
  {
  }

  public abstract class rescheduleTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.rescheduleTemplateID>
  {
  }

  public abstract class cancelInvitationTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.cancelInvitationTemplateID>
  {
  }

  public abstract class searchOnlyInWorkingTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPSetup.searchOnlyInWorkingTime>
  {
  }

  public abstract class requireTimes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.requireTimes>
  {
  }

  public abstract class timeCardNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.timeCardNumberingID>
  {
  }

  public abstract class timeCardAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.timeCardAssignmentMapID>
  {
  }

  public abstract class timeCardAssignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.timeCardAssignmentNotificationID>
  {
  }

  public abstract class equipmentTimeCardNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.equipmentTimeCardNumberingID>
  {
  }

  public abstract class equipmentTimeCardAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.equipmentTimeCardAssignmentMapID>
  {
  }

  public abstract class equipmentTimeCardAssignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.equipmentTimeCardAssignmentNotificationID>
  {
  }

  public abstract class activityTimeUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.activityTimeUnit>
  {
  }

  public abstract class employeeRateUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.employeeRateUnit>
  {
  }

  public abstract class groupTransactgion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.groupTransactgion>
  {
  }

  public abstract class defaultActivityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.defaultActivityType>
  {
  }

  public abstract class minBillableTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSetup.minBillableTime>
  {
  }

  public abstract class regularHoursType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.regularHoursType>
  {
  }

  public abstract class holidaysType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSetup.holidaysType>
  {
  }

  public abstract class vacationsType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSetup.vacationsType>
  {
  }

  public abstract class ispreloadHolidays : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.ispreloadHolidays>
  {
  }

  public abstract class defTasksFilterID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPSetup.defTasksFilterID>
  {
  }

  public abstract class defEventsFilterID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  EPSetup.defEventsFilterID>
  {
  }

  public abstract class postingOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSetup.postingOption>
  {
  }

  public abstract class offBalanceAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.offBalanceAccountGroupID>
  {
  }

  public abstract class customWeek : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSetup.customWeek>
  {
  }

  public abstract class firstCustomWeekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSetup.firstCustomWeekID>
  {
  }

  public abstract class lastCustomWeekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSetup.lastCustomWeekID>
  {
  }

  public abstract class firstDayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSetup.firstDayOfWeek>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPSetup.Tstamp>
  {
  }

  public abstract class claimDetailsAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.claimDetailsAssignmentMapID>
  {
  }

  public abstract class claimDetailsAssignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSetup.claimDetailsAssignmentNotificationID>
  {
  }

  public sealed class EarningTypeRG : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPSetup.EarningTypeRG>
  {
    public EarningTypeRG()
      : base("RG")
    {
    }
  }

  public sealed class EarningTypeHL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPSetup.EarningTypeHL>
  {
    public EarningTypeHL()
      : base("HL")
    {
    }
  }

  public sealed class EarningTypeVL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPSetup.EarningTypeVL>
  {
    public EarningTypeVL()
      : base("VL")
    {
    }
  }

  protected class CopyNoteSettings : PXNoteAttribute.IPXCopySettings
  {
    public CopyNoteSettings(bool? copyNotes = false, bool? copyFiles = false)
    {
      this.CopyNotes = copyNotes;
      this.CopyFiles = copyFiles;
    }

    public bool? CopyNotes { get; set; }

    public bool? CopyFiles { get; set; }
  }
}
