// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRPMTimeActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXBreakInheritance]
[PXProjection(typeof (SelectFrom<CRActivity, TypeArrayOf<IFbqlJoin>.Empty>.LeftJoin<PMTimeActivity>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTimeActivity.refNoteID, Equal<CRActivity.noteID>>>>>.And<BqlOperand<PMTimeActivity.isCorrected, IBqlBool>.IsEqual<False>>>), Persistent = true)]
[Serializable]
public class CRPMTimeActivity : CRActivity
{
  protected 
  #nullable disable
  string _ExtRefNbr;

  /// <inheritdoc />
  [PXDBString(2, IsFixed = true, BqlField = typeof (CRActivity.uistatus))]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.type, IsNull>, ActivityStatusListAttribute.open, Case<Where<CRPMTimeActivity.trackTime, IsNull, Or<CRPMTimeActivity.trackTime, Equal<False>>>, ActivityStatusListAttribute.completed>>, ActivityStatusListAttribute.open>))]
  [ActivityStatus]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("OP")]
  public override string UIStatus { get; set; }

  /// <inheritdoc />
  [PXUIField(DisplayName = "Complete Icon", IsReadOnly = true)]
  [PXImage(HeaderImage = "control@CompleteHead")]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.uistatus, Equal<ActivityStatusListAttribute.completed>>, CRActivity.isCompleteIcon.completed, Case<Where<CRPMTimeActivity.approvalStatus, Equal<ActivityStatusListAttribute.completed>, Or<CRPMTimeActivity.approvalStatus, Equal<ActivityStatusListAttribute.released>>>, CRActivity.isCompleteIcon.completed>>>))]
  public override string IsCompleteIcon { get; set; }

  /// <inheritdoc />
  [PXDBInt(BqlField = typeof (CRActivity.workgroupID))]
  [PXChildUpdatable(UpdateRequest = true)]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSubordinateGroupSelector]
  public override int? WorkgroupID { get; set; }

  [PXDBSequentialGuid(BqlField = typeof (PMTimeActivity.noteID))]
  [PXExtraKey]
  public virtual Guid? TimeActivityNoteID { get; set; }

  [PXDBGuid(false, BqlField = typeof (PMTimeActivity.refNoteID))]
  public virtual Guid? TimeActivityRefNoteID { get; set; }

  [PXDBGuid(false, BqlField = typeof (PMTimeActivity.parentTaskNoteID))]
  [PXDBDefault(null)]
  [PXUIField(DisplayName = "Task")]
  [PXFormula(typeof (Switch<Case<Where<Current2<CRPMTimeActivity.classID>, Equal<CRActivityClass.task>>, CRPMTimeActivity.noteID>, CRPMTimeActivity.parentTaskNoteID>))]
  public virtual Guid? ParentTaskNoteID { get; set; }

  [PXDBBool(BqlField = typeof (PMTimeActivity.trackTime))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Time")]
  [PXFormula(typeof (IIf<Where<Current2<CRActivity.classID>, Equal<CRActivityClass.activity>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>, IsNull<Selector<Current<CRActivity.type>, EPActivityType.requireTimeByDefault>, False>, False>))]
  public virtual bool? TrackTime { get; set; }

  [PXDBString(10, BqlField = typeof (PMTimeActivity.timeCardCD))]
  [PXUIField(Visible = false)]
  public virtual string TimeCardCD { get; set; }

  [PXDBString(15, BqlField = typeof (PMTimeActivity.timeSheetCD))]
  [PXUIField(Visible = false)]
  public virtual string TimeSheetCD { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true, BqlField = typeof (PMTimeActivity.summary))]
  [PXDefault]
  [PXFormula(typeof (CRPMTimeActivity.subject))]
  [PXUIField]
  [PXNavigateSelector(typeof (CRPMTimeActivity.summary))]
  public virtual string Summary { get; set; }

  [PXDBDateAndTime(DisplayNameDate = "Date", DisplayNameTime = "Time", UseTimeZone = true, BqlField = typeof (PMTimeActivity.date))]
  [PXUIField(DisplayName = "Date")]
  [PXFormula(typeof (CRPMTimeActivity.startDate))]
  public virtual DateTime? Date { get; set; }

  [PXChildUpdatable(AutoRefresh = true)]
  [Owner(typeof (CRPMTimeActivity.workgroupID), BqlField = typeof (PMTimeActivity.ownerID))]
  [PXFormula(typeof (CRPMTimeActivity.ownerID))]
  public virtual int? TimeActivityOwner { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.approverID))]
  [PXEPEmployeeSelector]
  [PXFormula(typeof (Switch<Case<Where<Current2<CRPMTimeActivity.projectID>, Equal<NonProject>>, Null, Case<Where<Current2<CRPMTimeActivity.projectTaskID>, IsNull>, Null>>, Selector<CRPMTimeActivity.projectTaskID, PMTask.approverID>>))]
  [PXUIField]
  public virtual int? ApproverID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PMTimeActivity.approvalStatus))]
  [ActivityStatusList]
  [PXUIField(DisplayName = "Approval Status", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.trackTime, Equal<True>, And<Current2<CRPMTimeActivity.approvalStatus>, IsNull>>, ActivityStatusListAttribute.open, Case<Where<CRPMTimeActivity.released, Equal<True>>, ActivityStatusListAttribute.released, Case<Where<CRPMTimeActivity.approverID, IsNotNull>, ActivityStatusListAttribute.pendingApproval>>>, ActivityStatusListAttribute.completed>))]
  public virtual string ApprovalStatus { get; set; }

  [PXDBDate(DisplayMask = "d", PreserveTime = true, BqlField = typeof (PMTimeActivity.approvedDate))]
  [PXUIField(DisplayName = "Approved Date")]
  public virtual DateTime? ApprovedDate { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PMTimeActivity.earningTypeID))]
  [PXDefault("RG", typeof (Search<EPSetup.regularHoursType>))]
  [PXUIRequired(typeof (CRPMTimeActivity.trackTime))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningTypeID { get; set; }

  [PXDBBool(BqlField = typeof (PMTimeActivity.isBillable))]
  [PXUIField(DisplayName = "Billable", FieldClass = "BILLABLE")]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.classID, Equal<CRActivityClass.task>, Or<CRPMTimeActivity.classID, Equal<CRActivityClass.events>>>, False, Case<Where2<FeatureInstalled<FeaturesSet.timeReportingModule>, And<CRPMTimeActivity.trackTime, Equal<True>>>, IsNull<Selector<CRPMTimeActivity.earningTypeID, EPEarningType.isbillable>, False>>>, False>))]
  public virtual bool? IsBillable { get; set; }

  [EPActivityProjectDefault(typeof (CRPMTimeActivity.isBillable))]
  [EPProject(typeof (CRPMTimeActivity.ownerID), FieldClass = "PROJECT", BqlField = typeof (PMTimeActivity.projectID))]
  [PXFormula(typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.projectAccounting>>>, DefaultValue<CRPMTimeActivity.projectID>, Case<Where<CRPMTimeActivity.parentNoteID, IsNotNull, And<Selector<CRPMTimeActivity.parentNoteID, Selector<CRPMTimeActivity.projectID, PMProject.contractCD>>, IsNotNull>>, Selector<CRPMTimeActivity.parentNoteID, Selector<CRPMTimeActivity.projectID, PMProject.contractCD>>, Case<Where<CRPMTimeActivity.isBillable, Equal<True>, And<Current2<CRPMTimeActivity.projectID>, Equal<NonProject>>>, Null, Case<Where<CRPMTimeActivity.isBillable, Equal<False>, And<Current2<CRPMTimeActivity.projectID>, IsNull>>, DefaultValue<CRPMTimeActivity.projectID>>>>>, CRPMTimeActivity.projectID>))]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (CRPMTimeActivity.projectID), "TA", DisplayName = "Project Task", BqlField = typeof (PMTimeActivity.projectTaskID))]
  [PXFormula(typeof (Switch<Case<Where<Current2<CRPMTimeActivity.projectID>, Equal<NonProject>>, Null, Case<Where<CRPMTimeActivity.parentNoteID, IsNotNull>, Selector<CRPMTimeActivity.parentNoteID, Selector<CRPMTimeActivity.projectTaskID, PMTask.taskCD>>>>, CRPMTimeActivity.projectTaskID>))]
  public virtual int? ProjectTaskID { get; set; }

  [CostCode(null, typeof (CRPMTimeActivity.projectTaskID), "E", ProjectField = typeof (CRPMTimeActivity.projectID), InventoryField = typeof (CRPMTimeActivity.labourItemID), UseNewDefaulting = true, BqlField = typeof (PMTimeActivity.costCodeID), ReleasedField = typeof (CRPMTimeActivity.released))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PMTimeActivity.extRefNbr))]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBInt(BqlField = typeof (PMTimeActivity.contractID))]
  [PXUIField(DisplayName = "Contract", Visible = false)]
  [PXSelector(typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), DescriptionField = typeof (PX.Objects.CT.Contract.description), SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  public virtual int? ContractID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.timeSpent))]
  [PXTimeList]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Time Spent")]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.trackTime, NotEqual<True>>, int0>, CRPMTimeActivity.timeSpent>))]
  public virtual int? TimeSpent { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.overtimeSpent))]
  [PXTimeList]
  [PXDefault(0)]
  [PXFormula(typeof (Switch<Case<Where<Selector<CRPMTimeActivity.earningTypeID, EPEarningType.isOvertime>, Equal<True>>, CRPMTimeActivity.timeSpent>, int0>))]
  [PXUIField(DisplayName = "Overtime", Enabled = false)]
  public virtual int? OvertimeSpent { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.timeBillable))]
  [PXTimeList]
  [PXDefault(0)]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.isBillable, Equal<True>>, CRPMTimeActivity.timeSpent, Case<Where<CRPMTimeActivity.isBillable, Equal<False>>, int0>>, CRPMTimeActivity.timeBillable>))]
  [PXUIField(DisplayName = "Billable Time", FieldClass = "BILLABLE")]
  [PXUIVerify]
  [PXUIVerify]
  public virtual int? TimeBillable { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.overtimeBillable))]
  [PXTimeList]
  [PXDefault(0)]
  [PXUIVerify]
  [PXFormula(typeof (Switch<Case<Where<CRPMTimeActivity.isBillable, Equal<True>, And<CRPMTimeActivity.overtimeSpent, GreaterEqual<CRPMTimeActivity.timeBillable>>>, CRPMTimeActivity.timeBillable, Case<Where<CRPMTimeActivity.isBillable, Equal<True>, And<CRPMTimeActivity.overtimeSpent, GreaterEqual<Zero>>>, CRPMTimeActivity.overtimeBillable, Case<Where<CRPMTimeActivity.isBillable, Equal<False>>, int0>>>, CRPMTimeActivity.overtimeBillable>))]
  [PXUIField(DisplayName = "Billable Overtime", FieldClass = "BILLABLE")]
  public virtual int? OvertimeBillable { get; set; }

  [PXDBBool(BqlField = typeof (PMTimeActivity.billed))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed", FieldClass = "BILLABLE")]
  public virtual bool? Billed { get; set; }

  [PXDBBool(BqlField = typeof (PMTimeActivity.released))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false, Visible = false, FieldClass = "BILLABLE")]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// If true this Activity has been corrected in the Timecard and is no longer valid. Please hide this activity in all lists displayed in the UI since there is another valid activity.
  /// The valid activity has a refence back to the corrected activity via OrigTaskID field.
  /// </summary>
  [PXDBBool(BqlField = typeof (PMTimeActivity.isCorrected))]
  [PXDefault(false)]
  public virtual bool? IsCorrected { get; set; }

  /// <summary>
  /// Use for correction. Stores the reference to the original activity.
  /// </summary>
  [PXDBGuid(false, BqlField = typeof (PMTimeActivity.origNoteID))]
  public virtual Guid? OrigNoteID { get; set; }

  [PXDBLong(BqlField = typeof (PMTimeActivity.tranID))]
  public virtual long? TranID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.weekID))]
  [PXUIField(DisplayName = "Time Card Week", Enabled = false)]
  [PXWeekSelector2]
  [PXFormula(typeof (Default<CRPMTimeActivity.date, CRPMTimeActivity.trackTime>))]
  [EPActivityDefaultWeek(typeof (CRPMTimeActivity.date))]
  public virtual int? WeekID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.labourItemID))]
  [PXUIField(Visible = false)]
  public virtual int? LabourItemID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.overtimeItemID))]
  [PXUIField(Visible = false)]
  public virtual int? OvertimeItemID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.jobID))]
  public virtual int? JobID { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.shiftID))]
  [TimeActivityShiftCodeSelector(typeof (CRPMTimeActivity.ownerID), typeof (CRPMTimeActivity.date))]
  [EPShiftCodeActiveRestrictor]
  public virtual int? ShiftID { get; set; }

  /// <summary>
  /// Stores Employee's Hourly rate at the time the activity was released to PM
  /// </summary>
  [PXDBPriceCost(BqlField = typeof (PMTimeActivity.employeeRate))]
  [PXUIField(Visible = false)]
  public virtual Decimal? EmployeeRate { get; set; }

  /// <summary>
  /// This is a adjusting activity for the summary line in the Timecard.
  /// </summary>
  [PXDBInt(BqlField = typeof (PMTimeActivity.summaryLineNbr))]
  public virtual int? SummaryLineNbr { get; set; }

  [PX.Objects.AR.ARDocType.List]
  [PXString(3, IsFixed = true)]
  [PXUIField]
  public virtual string ARDocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AR.ARRegister.refNbr, Where<PX.Objects.AR.ARRegister.docType, Equal<Current<CRPMTimeActivity.arDocType>>>>), DescriptionField = typeof (PX.Objects.AR.ARRegister.docType))]
  public virtual string ARRefNbr { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true, BqlField = typeof (PMTimeActivity.createdByID))]
  [PXUIField(Enabled = false)]
  public virtual Guid? TimeActivityCreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (PMTimeActivity.createdByScreenID))]
  public virtual string TimeActivityCreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime(BqlField = typeof (PMTimeActivity.createdDateTime))]
  public virtual DateTime? TimeActivityCreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PMTimeActivity.lastModifiedByID))]
  public virtual Guid? TimeActivityLastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PMTimeActivity.lastModifiedByScreenID))]
  public virtual string TimeActivityLastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PMTimeActivity.lastModifiedDateTime))]
  public virtual DateTime? TimeActivityLastModifiedDateTime { get; set; }

  [PXGuid]
  public virtual Guid? ChildKey
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRPMTimeActivity.timeActivityNoteID)})] get
    {
      return this.TimeActivityNoteID;
    }
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.selected>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMTimeActivity.noteID>
  {
  }

  public new abstract class parentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.parentNoteID>
  {
  }

  public new abstract class refNoteIDType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.refNoteIDType>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMTimeActivity.refNoteID>
  {
  }

  public new abstract class documentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.documentNoteID>
  {
  }

  public new abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.source>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.classID>
  {
  }

  public new abstract class classIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.classIcon>
  {
  }

  public new abstract class classInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.classInfo>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.type>
  {
  }

  public new abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.subject>
  {
  }

  public new abstract class location : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.location>
  {
  }

  public new abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.body>
  {
  }

  public new abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.priority>
  {
  }

  public new abstract class priorityIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.priorityIcon>
  {
  }

  public new abstract class uistatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.uistatus>
  {
  }

  public new abstract class isOverdue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isOverdue>
  {
  }

  public new abstract class isCompleteIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.isCompleteIcon>
  {
  }

  public new abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.categoryID>
  {
  }

  public new abstract class allDay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.allDay>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.startDate>
  {
  }

  public new abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRPMTimeActivity.endDate>
  {
  }

  public new abstract class completedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.completedDate>
  {
  }

  public new abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.dayOfWeek>
  {
  }

  public new abstract class percentCompletion : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRPMTimeActivity.percentCompletion>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.ownerID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.workgroupID>
  {
  }

  public new abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isExternal>
  {
  }

  public new abstract class isPrivate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isPrivate>
  {
  }

  public new abstract class incoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.incoming>
  {
  }

  public new abstract class outgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.outgoing>
  {
  }

  public new abstract class synchronize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.synchronize>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.bAccountID>
  {
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.contactID>
  {
  }

  public new abstract class entityDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.entityDescription>
  {
  }

  public new abstract class showAsID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.showAsID>
  {
  }

  public new abstract class isLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isLocked>
  {
  }

  public new abstract class deletedDatabaseRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRPMTimeActivity.deletedDatabaseRecord>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.createdDateTime>
  {
  }

  public abstract class timeActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityNoteID>
  {
  }

  public abstract class timeActivityRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityRefNoteID>
  {
  }

  public abstract class parentTaskNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.parentTaskNoteID>
  {
  }

  public abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.trackTime>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.timeCardCD>
  {
  }

  public abstract class timeSheetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.timeSheetCD>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.summary>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRPMTimeActivity.date>
  {
  }

  public abstract class timeActivityOwner : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityOwner>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.approverID>
  {
  }

  public abstract class approvalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.approvalStatus>
  {
  }

  public abstract class approvedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.approvedDate>
  {
  }

  public abstract class earningTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.earningTypeID>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isBillable>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.costCodeID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.extRefNbr>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.contractID>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.timeSpent>
  {
  }

  public abstract class overtimeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.overtimeSpent>
  {
  }

  public abstract class timeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.timeBillable>
  {
  }

  public abstract class overtimeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRPMTimeActivity.overtimeBillable>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.billed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.released>
  {
  }

  public abstract class isCorrected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRPMTimeActivity.isCorrected>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMTimeActivity.origNoteID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CRPMTimeActivity.tranID>
  {
  }

  public abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.weekID>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.labourItemID>
  {
  }

  public abstract class overtimeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.overtimeItemID>
  {
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.jobID>
  {
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.shiftID>
  {
  }

  public abstract class employeeRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRPMTimeActivity.employeeRate>
  {
  }

  public abstract class summaryLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRPMTimeActivity.summaryLineNbr>
  {
  }

  public abstract class arDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.arDocType>
  {
  }

  public abstract class arRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRPMTimeActivity.arRefNbr>
  {
  }

  public abstract class timeActivityCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityCreatedByID>
  {
  }

  public abstract class timeActivityCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityCreatedByScreenID>
  {
  }

  public abstract class timeActivityCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityCreatedDateTime>
  {
  }

  public abstract class timeActivityLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityLastModifiedByID>
  {
  }

  public abstract class timeActivityLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityLastModifiedByScreenID>
  {
  }

  public abstract class timeActivityLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRPMTimeActivity.timeActivityLastModifiedDateTime>
  {
  }

  public abstract class childKey : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRPMTimeActivity.childKey>
  {
  }
}
