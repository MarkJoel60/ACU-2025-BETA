// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PMTimeActivity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

[CRTimeActivityPrimaryGraph]
[PXCacheName("Time Activity")]
[Serializable]
public class PMTimeActivity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ExtRefNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBSequentialGuid(IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.CRActivity" />.
  /// This field is included in <see cref="T:PX.Objects.CR.PMTimeActivity.FK.Related" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> field.
  /// </value>
  [PXSequentialSelfRefNote(SuppressActivitiesCount = true, NoteField = typeof (PMTimeActivity.noteID), Persistent = true)]
  [PXUIField(Visible = false)]
  [PXParent(typeof (Select<CRActivity, Where<CRActivity.noteID, Equal<Current<PMTimeActivity.refNoteID>>>>), ParentCreate = true)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBGuid(false)]
  [PXDBDefault(null)]
  [CRTaskSelector]
  [PXRestrictor(typeof (Where<CRActivity.ownerID, Equal<Current<AccessInfo.contactID>>>), null, new System.Type[] {})]
  [PXUIField(DisplayName = "Task")]
  public virtual Guid? ParentTaskNoteID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Time and Costs")]
  [PXFormula(typeof (IIf<Where<Current2<CRActivity.classID>, Equal<CRActivityClass.activity>, And<FeatureInstalled<FeaturesSet.timeReportingModule>>>, IsNull<Selector<Current<CRActivity.type>, EPActivityType.requireTimeByDefault>, False>, False>))]
  public virtual bool? TrackTime { get; set; }

  [PXDBString(10)]
  [PXUIField(Visible = false)]
  public virtual string TimeCardCD { get; set; }

  [PXDBString(15)]
  [PXUIField(Visible = false)]
  public virtual string TimeSheetCD { get; set; }

  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Summary { get; set; }

  [PXDBDateAndTime(DisplayNameDate = "Date", DisplayNameTime = "Time", UseTimeZone = true)]
  [PXUIField(DisplayName = "Date")]
  [PXFormula(typeof (IsNull<Current<CRActivity.startDate>, Current<CRSMEmail.startDate>>))]
  public virtual DateTime? Date { get; set; }

  [PXInt(MaxValue = 6)]
  [PXUIField(DisplayName = "Day")]
  [PXDependsOnFields(new System.Type[] {typeof (PMTimeActivity.date)})]
  public virtual int? DayOfWeek
  {
    get
    {
      DateTime? date = this.Date;
      ref DateTime? local = ref date;
      System.DayOfWeek? nullable = local.HasValue ? new System.DayOfWeek?(local.GetValueOrDefault().DayOfWeek) : new System.DayOfWeek?();
      return !nullable.HasValue ? new int?() : new int?((int) nullable.GetValueOrDefault());
    }
  }

  [PXChildUpdatable(AutoRefresh = true)]
  [SubordinateOwnerEmployee]
  public virtual int? OwnerID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault("RG", typeof (Search<EPSetup.regularHoursType>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningTypeID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Billable", FieldClass = "BILLABLE")]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<IsNull<Current<CRActivity.classID>, Current<CRSMEmail.classID>>, Equal<CRActivityClass.task>, Or<IsNull<Current<CRActivity.classID>, Current<CRSMEmail.classID>>, Equal<CRActivityClass.events>>>, False, Case<Where2<FeatureInstalled<FeaturesSet.timeReportingModule>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.earningTypeID, IsNotNull>>>, Selector<PMTimeActivity.earningTypeID, EPEarningType.isbillable>>>, False>), KeepIdleSelfUpdates = true)]
  public virtual bool? IsBillable { get; set; }

  [EPActivityProjectDefault(typeof (PMTimeActivity.isBillable))]
  [EPProject(typeof (PMTimeActivity.ownerID), FieldClass = "PROJECT")]
  [PXFormula(typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.projectAccounting>>>, DefaultValue<PMTimeActivity.projectID>, Case<Where<PMTimeActivity.isBillable, Equal<True>, And<Current2<PMTimeActivity.projectID>, Equal<NonProject>>>, Null, Case<Where<PMTimeActivity.isBillable, Equal<False>, And<Current2<PMTimeActivity.projectID>, IsNull>>, DefaultValue<PMTimeActivity.projectID>>>>, PMTimeActivity.projectID>))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMTimeActivity.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (PMTimeActivity.projectID), "TA", DisplayName = "Project Task")]
  [PXFormula(typeof (Switch<Case<Where<Current2<PMTimeActivity.projectID>, Equal<NonProject>>, Null>, PMTimeActivity.projectTaskID>))]
  [PXForeignReference(typeof (CompositeKey<Field<PMTimeActivity.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMTimeActivity.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Coalesce<Search<PMProject.certifiedJob, Where<PMProject.contractID, Equal<Current<PMTimeActivity.projectID>>>>, Search<PMProject.certifiedJob, Where<PMProject.nonProject, Equal<True>>>>))]
  [PXUIField(DisplayName = "Certified Job", FieldClass = "Construction")]
  public virtual bool? CertifiedJob { get; set; }

  [PXForeignReference(typeof (Field<PMTimeActivity.unionID>.IsRelatedTo<PMUnion.unionID>))]
  [PMUnion(typeof (PMTimeActivity.projectID), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<PMTimeActivity.ownerID>>>>), DescriptionField = typeof (PMUnion.description))]
  public virtual string UnionID { get; set; }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXFormula(typeof (Switch<Case<Where<Current2<PMTimeActivity.projectID>, Equal<NonProject>>, Null, Case<Where<Current2<PMTimeActivity.projectTaskID>, IsNull>, Null>>, Selector<PMTimeActivity.projectTaskID, PMTask.approverID>>))]
  [PXUIField]
  public virtual int? ApproverID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PX.Objects.CR.ApprovalStatus]
  [PXUIField(DisplayName = "Approval Status", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.trackTime, Equal<True>, And<Where<Current2<PMTimeActivity.approvalStatus>, IsNull, Or<Current2<PMTimeActivity.approvalStatus>, Equal<ActivityStatusListAttribute.open>>>>>, ActivityStatusListAttribute.open, Case<Where<PMTimeActivity.released, Equal<True>>, ActivityStatusListAttribute.released, Case<Where<PMTimeActivity.approverID, IsNotNull>, ActivityStatusListAttribute.pendingApproval>>>, ActivityStatusListAttribute.completed>))]
  public virtual string ApprovalStatus { get; set; }

  [PXDBDate(DisplayMask = "d", PreserveTime = true)]
  [PXUIField(DisplayName = "Approved Date")]
  public virtual DateTime? ApprovedDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXWorkgroupSelector]
  [PXParent(typeof (Select<EPTimeActivitiesSummary, Where<EPTimeActivitiesSummary.workgroupID, Equal<Current<PMTimeActivity.workgroupID>>, And<EPTimeActivitiesSummary.week, Equal<Current<PMTimeActivity.weekID>>, And<EPTimeActivitiesSummary.contactID, Equal<Current<PMTimeActivity.ownerID>>>>>>), ParentCreate = true, LeaveChildren = true)]
  [PXDefault(typeof (SearchFor<PX.Objects.EP.EPEmployee.defaultWorkgroupID>.Where<BqlOperand<PX.Objects.EP.EPEmployee.defContactID, IBqlInt>.IsEqual<BqlField<PMTimeActivity.ownerID, IBqlInt>.FromCurrent>>))]
  public virtual int? WorkgroupID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract", Visible = false)]
  [PXSelector(typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), DescriptionField = typeof (PX.Objects.CT.Contract.description), SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  public virtual int? ContractID { get; set; }

  [PXDBInt]
  [PXTimeList]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Time Spent")]
  [PXUnboundFormula(typeof (Switch<Case<Where<Selector<PMTimeActivity.earningTypeID, EPEarningType.isOvertime>, Equal<False>>, PMTimeActivity.timeSpent>, int0>), typeof (SumCalc<EPTimeActivitiesSummary.totalRegularTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int0>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.sundayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int1>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.mondayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int2>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.tuesdayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int3>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.wednesdayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int4>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.thursdayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int5>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.fridayTime>))]
  [PXUnboundFormula(typeof (BqlOperand<PMTimeActivity.timeSpent, IBqlInt>.When<BqlOperand<PMTimeActivity.dayOfWeek, IBqlInt>.IsEqual<int6>>.Else<int0>), typeof (SumCalc<EPTimeActivitiesSummary.saturdayTime>))]
  public virtual int? TimeSpent { get; set; }

  [PXDBInt]
  [PXTimeList]
  [PXDefault(0)]
  [PXFormula(typeof (Switch<Case<Where<Selector<PMTimeActivity.earningTypeID, EPEarningType.isOvertime>, Equal<True>>, PMTimeActivity.timeSpent>, int0>))]
  [PXUIField(DisplayName = "Overtime", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<EPTimeActivitiesSummary.totalOvertime>))]
  public virtual int? OvertimeSpent { get; set; }

  [PXDBInt]
  [PXTimeList]
  [PXDefault(0)]
  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.isBillable, Equal<True>>, PMTimeActivity.timeSpent, Case<Where<PMTimeActivity.isBillable, Equal<False>>, int0>>, PMTimeActivity.timeBillable>))]
  [PXUIField(DisplayName = "Billable Time", FieldClass = "BILLABLE")]
  [PXUIVerify]
  [PXUIVerify]
  [PXUnboundFormula(typeof (Switch<Case<Where<Selector<PMTimeActivity.earningTypeID, EPEarningType.isOvertime>, Equal<False>>, PMTimeActivity.timeBillable>, int0>), typeof (SumCalc<EPTimeActivitiesSummary.totalBillableTime>))]
  public virtual int? TimeBillable { get; set; }

  [PXDBInt]
  [PXTimeList]
  [PXDefault(0)]
  [PXUIVerify]
  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.isBillable, Equal<True>, And<PMTimeActivity.overtimeSpent, GreaterEqual<PMTimeActivity.timeBillable>>>, PMTimeActivity.timeBillable, Case<Where<PMTimeActivity.isBillable, Equal<True>, And<PMTimeActivity.overtimeSpent, GreaterEqual<Zero>>>, PMTimeActivity.overtimeBillable, Case<Where<PMTimeActivity.isBillable, Equal<False>>, int0>>>, PMTimeActivity.overtimeBillable>))]
  [PXUIField(DisplayName = "Billable Overtime", FieldClass = "BILLABLE")]
  [PXFormula(null, typeof (SumCalc<EPTimeActivitiesSummary.totalBillableOvertime>))]
  public virtual int? OvertimeBillable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed", FieldClass = "BILLABLE")]
  public virtual bool? Billed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false, Visible = false, FieldClass = "BILLABLE")]
  public virtual bool? Released { get; set; }

  /// <summary>
  /// If true this Activity has been corrected in the Timecard and is no longer valid. Please hide this activity in all lists displayed in the UI since there is another valid activity.
  /// The valid activity has a refence back to the corrected activity via OrigTaskID field.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCorrected { get; set; }

  /// <summary>
  /// Use for correction. Stores the reference to the original activity.
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? OrigNoteID { get; set; }

  [PXDBLong]
  public virtual long? TranID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Time Card Week", Enabled = false)]
  [PXWeekSelector2]
  [PXFormula(typeof (Default<PMTimeActivity.date>))]
  [EPActivityDefaultWeek(typeof (PMTimeActivity.date))]
  public virtual int? WeekID { get; set; }

  [PMActiveLaborItem(typeof (PMTimeActivity.projectID), typeof (PMTimeActivity.earningTypeID), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<PMTimeActivity.ownerID>>>>), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<PMTimeActivity.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LabourItemID { get; set; }

  [CostCode(null, typeof (PMTimeActivity.projectTaskID), "E", ReleasedField = typeof (PMTimeActivity.released), ProjectField = typeof (PMTimeActivity.projectID), InventoryField = typeof (PMTimeActivity.labourItemID), UseNewDefaulting = true)]
  public virtual int? CostCodeID { get; set; }

  [PXForeignReference(typeof (PMTimeActivity.FK.WorkCode))]
  [PMWorkCodeInTimeActivity(typeof (PMTimeActivity.costCodeID), typeof (PMTimeActivity.projectID), typeof (PMTimeActivity.projectTaskID), typeof (PMTimeActivity.labourItemID), typeof (PMTimeActivity.ownerID))]
  public virtual string WorkCodeID { get; set; }

  [PXDBInt]
  [PXUIField(Visible = false)]
  public virtual int? OvertimeItemID { get; set; }

  [PXDBInt]
  public virtual int? JobID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Shift Code", FieldClass = "ShiftDifferential")]
  [TimeActivityShiftCodeSelector(typeof (PMTimeActivity.ownerID), typeof (PMTimeActivity.date))]
  [EPShiftCodeActiveRestrictor]
  public virtual int? ShiftID { get; set; }

  /// <summary>
  /// Stores Employee's Hourly rate at the time the activity was released to PM
  /// </summary>
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Cost Rate", Enabled = false)]
  public virtual Decimal? EmployeeRate { get; set; }

  /// <summary>
  /// This is a adjusting activity for the summary line in the Timecard.
  /// </summary>
  [PXDBInt]
  public virtual int? SummaryLineNbr { get; set; }

  [PX.Objects.AR.ARDocType.List]
  [PXString(3, IsFixed = true)]
  [PXUIField]
  public virtual string ARDocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AR.ARRegister.refNbr, Where<PX.Objects.AR.ARRegister.docType, Equal<Current<PMTimeActivity.arDocType>>>>), DescriptionField = typeof (PX.Objects.AR.ARRegister.docType))]
  public virtual string ARRefNbr { get; set; }

  [PXUIField(DisplayName = "Reported in Time Zone", Enabled = false, Visible = false)]
  [PXDBString(32 /*0x20*/)]
  [PXTimeZone(true)]
  public virtual string ReportedInTimeZoneID { get; set; }

  [PXDBCreatedByID(DontOverrideValue = true)]
  [PXUIField(Enabled = false)]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created At", Enabled = false)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.trackTime, NotEqual<True>, And<Where<PMTimeActivity.projectID, IsNull, Or<PMTimeActivity.projectID, Equal<NonProject>>>>>, True>, False>))]
  public bool? NeedToBeDeleted { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Activity Exists", Enabled = false, Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.refNoteID, NotEqual<PMTimeActivity.noteID>>, True>, Null>))]
  public bool? IsActivityExists { get; set; }

  /// <summary>
  /// Date and Time are displayed and modified depending on the <see cref="P:PX.Objects.CR.PMTimeActivity.ReportedInTimeZoneID" />
  /// </summary>
  [PXDateAndTimeWithTimeZone(typeof (PMTimeActivity.date), typeof (PMTimeActivity.reportedInTimeZoneID), DisplayNameDate = "Reported On", DisplayNameTime = "Reported At")]
  [PXUIField(DisplayName = "Reported On", Enabled = false, Visible = false)]
  public virtual DateTime? ReportedOnDate { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<PMTimeActivity>.By<PMTimeActivity.noteID>
  {
    public static PMTimeActivity Find(PXGraph graph, Guid? noteID, PKFindOptions options = 0)
    {
      return (PMTimeActivity) PrimaryKeyOf<PMTimeActivity>.By<PMTimeActivity.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Time Card</summary>
    public class Timecard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.timeCardCD>
    {
    }

    /// <summary>Earning Type</summary>
    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.earningTypeID>
    {
    }

    /// <summary>Owner</summary>
    public class OwnerContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.ownerID>
    {
    }

    /// <summary>Project</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.projectID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.projectID, PMTimeActivity.projectTaskID>
    {
    }

    /// <summary>Cost Code</summary>
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.costCodeID>
    {
    }

    /// <summary>Related Activity.</summary>
    public class Related : 
      PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.refNoteID>
    {
    }

    /// <summary>Parent Activity.</summary>
    public class Parent : 
      PrimaryKeyOf<CRActivity>.By<CRActivity.noteID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.parentTaskNoteID>
    {
    }

    /// <summary>Union</summary>
    public class Union : 
      PrimaryKeyOf<PMUnion>.By<PMUnion.unionID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.unionID>
    {
    }

    /// <summary>Work Code</summary>
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.workCodeID>
    {
    }

    /// <summary>Contract</summary>
    public class Contract : 
      PrimaryKeyOf<PX.Objects.CT.Contract>.By<PX.Objects.CT.Contract.contractID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.contractID>
    {
    }

    /// <summary>Approver</summary>
    public class Approver : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.approverID>
    {
    }

    /// <summary>Original/Corrected Acivity</summary>
    public class OriginalActivity : 
      PrimaryKeyOf<PMTimeActivity>.By<PMTimeActivity.noteID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.origNoteID>
    {
    }

    /// <summary>Labor Item</summary>
    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.labourItemID>
    {
    }

    /// <summary>Overtime Labor Item</summary>
    public class OvertimeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.overtimeItemID>
    {
    }

    /// <summary>Shift Code</summary>
    public class ShiftCode : 
      PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>.ForeignKeyOf<PMTimeActivity>.By<PMTimeActivity.shiftID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.selected>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTimeActivity.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTimeActivity.refNoteID>
  {
  }

  public abstract class parentTaskNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMTimeActivity.parentTaskNoteID>
  {
  }

  public abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.trackTime>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.timeCardCD>
  {
  }

  public abstract class timeSheetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.timeSheetCD>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.summary>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTimeActivity.date>
  {
  }

  public abstract class dayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.dayOfWeek>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.ownerID>
  {
  }

  public abstract class earningTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTimeActivity.earningTypeID>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.isBillable>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.projectTaskID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.extRefNbr>
  {
  }

  public abstract class certifiedJob : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.certifiedJob>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.unionID>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.approverID>
  {
  }

  public abstract class approvalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTimeActivity.approvalStatus>
  {
  }

  public abstract class approvedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTimeActivity.approvedDate>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.workgroupID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.contractID>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.timeSpent>
  {
  }

  public abstract class overtimeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.overtimeSpent>
  {
  }

  public abstract class timeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.timeBillable>
  {
  }

  public abstract class overtimeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTimeActivity.overtimeBillable>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.billed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.released>
  {
  }

  public abstract class isCorrected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTimeActivity.isCorrected>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTimeActivity.origNoteID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTimeActivity.tranID>
  {
  }

  public abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.weekID>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.labourItemID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.costCodeID>
  {
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.workCodeID>
  {
  }

  public abstract class overtimeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.overtimeItemID>
  {
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.jobID>
  {
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.shiftID>
  {
  }

  public abstract class employeeRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTimeActivity.employeeRate>
  {
  }

  public abstract class summaryLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivity.summaryLineNbr>
  {
  }

  public abstract class arDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.arDocType>
  {
  }

  public abstract class arRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTimeActivity.arRefNbr>
  {
  }

  public abstract class reportedInTimeZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTimeActivity.reportedInTimeZoneID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTimeActivity.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTimeActivity.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTimeActivity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMTimeActivity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTimeActivity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTimeActivity.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMTimeActivity.Tstamp>
  {
  }

  public abstract class needToBeDeleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTimeActivity.needToBeDeleted>
  {
  }

  public abstract class isActivityExists : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTimeActivity.isActivityExists>
  {
  }

  public abstract class reportedOnDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTimeActivity.reportedOnDate>
  {
  }
}
