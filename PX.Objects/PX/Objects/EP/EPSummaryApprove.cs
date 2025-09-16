// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPSummaryApprove
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Provides a mechanism to approve or reject time activities in groups by approving or rejecting summary rows that represent the related time cards.
/// Only available if the Project Accounting feature is enabled.
/// </summary>
[PXProjection(typeof (Select5<EPTimeCardSummary, InnerJoin<EPTimeCard, On<EPTimeCardSummary.timeCardCD, Equal<EPTimeCard.timeCardCD>>, LeftJoin<EPTimeCardEx, On<EPTimeCard.timeCardCD, Equal<EPTimeCardEx.origTimeCardCD>>, InnerJoin<CREmployee, On<EPTimeCard.employeeID, Equal<CREmployee.bAccountID>>, InnerJoin<PMTimeActivity, On<EPTimeCardSummary.earningType, Equal<PMTimeActivity.earningTypeID>, And<EPTimeCardSummary.projectID, Equal<PMTimeActivity.projectID>, And<EPTimeCardSummary.projectTaskID, Equal<PMTimeActivity.projectTaskID>, And<EPTimeCardSummary.isBillable, Equal<PMTimeActivity.isBillable>, And2<Where<EPTimeCardSummary.parentNoteID, Equal<PMTimeActivity.parentTaskNoteID>, Or<EPTimeCardSummary.parentNoteID, IsNull, And<PMTimeActivity.parentTaskNoteID, IsNull>>>, And2<Where<EPTimeCardSummary.jobID, Equal<PMTimeActivity.jobID>, Or<EPTimeCardSummary.jobID, IsNull, And<PMTimeActivity.jobID, IsNull>>>, And2<Where<EPTimeCardSummary.shiftID, Equal<PMTimeActivity.shiftID>, Or<EPTimeCardSummary.shiftID, IsNull, And<PMTimeActivity.shiftID, IsNull>>>, And<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>, And<EPTimeCard.weekId, Equal<PMTimeActivity.weekID>, And<PMTimeActivity.released, Equal<False>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.approverID, IsNotNull, And<Where<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>>>>>>>>>>>>>>>, LeftJoin<EPActivityApprove, On<EPActivityApprove.noteID, Equal<PMTimeActivity.noteID>, And<EPActivityApprove.approvalStatus, Equal<ActivityStatusListAttribute.approved>>>, LeftJoin<EPActivityReject, On<EPActivityReject.noteID, Equal<PMTimeActivity.noteID>, And<EPActivityReject.approvalStatus, Equal<ActivityStatusListAttribute.rejected>>>, LeftJoin<EPActivityComplite, On<EPActivityComplite.noteID, Equal<PMTimeActivity.noteID>, And<EPActivityComplite.approvalStatus, Equal<ActivityStatusListAttribute.pendingApproval>>>, LeftJoin<EPActivityOpen, On<EPActivityOpen.noteID, Equal<PMTimeActivity.noteID>, And<EPActivityOpen.approvalStatus, Equal<ActivityStatusListAttribute.open>>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<EPTimeCardSummary.projectID>>, LeftJoin<PMTask, On<PMTask.projectID, Equal<EPTimeCardSummary.projectID>, And<PMTask.taskID, Equal<EPTimeCardSummary.projectTaskID>>>>>>>>>>>>>, Where<EPTimeCardEx.timeCardCD, IsNull>, Aggregate<GroupBy<EPTimeCardSummary.timeCardCD, GroupBy<EPTimeCardSummary.lineNbr, GroupBy<EPTimeCardSummary.earningType, GroupBy<EPTimeCardSummary.jobID, GroupBy<EPTimeCardSummary.shiftID, GroupBy<EPTimeCardSummary.parentNoteID, GroupBy<EPTimeCardSummary.projectID, GroupBy<EPTimeCardSummary.projectTaskID, GroupBy<EPTimeCardSummary.mon, GroupBy<EPTimeCardSummary.tue, GroupBy<EPTimeCardSummary.wed, GroupBy<EPTimeCardSummary.thu, GroupBy<EPTimeCardSummary.fri, GroupBy<EPTimeCardSummary.sat, GroupBy<EPTimeCardSummary.sun, GroupBy<EPTimeCardSummary.isBillable, GroupBy<EPTimeCardSummary.description, GroupBy<EPTimeCardSummary.noteID, GroupBy<EPTimeCard.weekId, GroupBy<PMProject.approverID, GroupBy<PMTask.approverID, GroupBy<CREmployee.userID, GroupBy<CREmployee.noteID, GroupBy<CREmployee.bAccountID, Max<EPActivityComplite.approvalStatus, Max<EPActivityApprove.approvalStatus, Max<EPActivityReject.approvalStatus, Max<EPActivityOpen.approvalStatus>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
[Serializable]
public class EPSummaryApprove : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _IsApprove;
  /// <summary>
  /// Indicates (if set to <see langword="true" />) that this summary row is rejected.
  /// </summary>
  protected bool? _IsReject;
  private int? _ProjectApproverID;
  protected Guid? _NoteID;

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that this summary row is approved.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Approve")]
  public virtual bool? IsApprove
  {
    get
    {
      return new bool?(((int) this._IsApprove ?? (this.HasApprove == null || this.HasComplite != null || this.HasOpen != null ? 0 : (this.HasReject == null ? 1 : 0))) != 0);
    }
    set
    {
      this._IsApprove = value;
      if (!this._IsApprove.HasValue || !this._IsApprove.Value)
        return;
      this._IsReject = new bool?(false);
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Reject")]
  public virtual bool? IsReject
  {
    get
    {
      return new bool?(((int) this._IsReject ?? (this.HasApprove != null || this.HasComplite != null || this.HasOpen != null ? 0 : (this.HasReject != null ? 1 : 0))) != 0);
    }
    set
    {
      this._IsReject = value;
      if (!this._IsReject.HasValue || !this._IsReject.Value)
        return;
      this._IsApprove = new bool?(false);
    }
  }

  [PXDBDefault(typeof (EPTimeCard.timeCardCD))]
  [PXDBString(10, IsKey = true, BqlField = typeof (EPTimeCardSummary.timeCardCD))]
  [PXUIField(DisplayName = "Time Card")]
  [PXSelector(typeof (Search<EPTimeCard.timeCardCD>))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  /// <summary>
  /// Represents the position of this item within this group of time card activities.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (EPTimeCardSummary.lineNbr))]
  [PXLineNbr(typeof (EPTimeCard.summaryLineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPEarningType">earning type</see> of the work time spent by the employee.<br></br>
  /// Only active earning types can be selected.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (EPTimeCardSummary.earningType))]
  [PXDefault(typeof (Search<EPSetup.regularHoursType>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningType { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.PMTimeActivity">job</see> associated with this activity.
  /// <para></para>
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.PMTimeActivity.JobID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.jobID))]
  public virtual int? JobID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPShiftCode">shift code </see> for this time activity.<br></br>
  /// Only available if the Shift Differential feature is enabled.
  /// </summary>
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.shiftID))]
  [PXUIField(DisplayName = "Shift Code", FieldClass = "ShiftDifferential")]
  [TimeCardShiftCodeSelector(typeof (EPSummaryApprove.employeeID), typeof (EPTimeCard.weekEndDate))]
  [EPShiftCodeActiveRestrictor]
  public virtual int? ShiftID { get; set; }

  [PXUIField(DisplayName = "Task ID")]
  [PXDBGuid(false, BqlField = typeof (EPTimeCardSummary.parentNoteID))]
  [CRTaskSelector]
  public virtual Guid? ParentNoteID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> that the employee has worked on.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [ProjectDefault("TA", ForceProjectExplicitly = true)]
  [EPTimeCardProject(BqlField = typeof (EPTimeCardSummary.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>Gets sets Project's Description</summary>
  [PXUIField(DisplayName = "Project Description", IsReadOnly = true, Visible = false)]
  [PXString]
  [PXFieldDescription]
  [PXUnboundDefault(typeof (Search<PMProject.description, Where<PMProject.contractID, Equal<Current<EPSummaryApprove.projectID>>>>))]
  [PXFormula(typeof (Default<EPSummaryApprove.projectID>))]
  public string ProjectDescription { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see> that the employee has worked on.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPSummaryApprove.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPTimeCardSummary.projectID), "TA", DisplayName = "Project Task", BqlField = typeof (EPTimeCardSummary.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>Gets sets Project Task's Description</summary>
  [PXUIField(DisplayName = "Project Task Description", IsReadOnly = true, Visible = false)]
  [PXString]
  [PXFieldDescription]
  [PXUnboundDefault(typeof (Search<PMTask.description, Where<PMTask.projectID, Equal<Current<EPSummaryApprove.projectID>>, And<PMTask.taskID, Equal<Current<EPSummaryApprove.projectTaskID>>>>>))]
  [PXFormula(typeof (Default<EPSummaryApprove.projectID, EPSummaryApprove.projectTaskID>))]
  public string ProjectTaskDescription { get; set; }

  /// <summary>
  /// The sum of the minutes of work time spent for all the days of the week. <para></para>
  /// This is a read-only calculated field.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpent
  {
    get
    {
      int? nullable = this.Mon;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.Tue;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.Wed;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      int num2 = num1 + valueOrDefault3;
      nullable = this.Thu;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      int num3 = num2 + valueOrDefault4;
      nullable = this.Fri;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      int num4 = num3 + valueOrDefault5;
      nullable = this.Sat;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      int num5 = num4 + valueOrDefault6;
      nullable = this.Sun;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      return new int?(num5 + valueOrDefault7);
    }
  }

  /// <summary>
  /// The minutes of work time reported for Sunday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.sun))]
  [PXUIField(DisplayName = "Sun")]
  public virtual int? Sun { get; set; }

  /// <summary>
  /// The minutes of work time reported for Monday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.mon))]
  [PXUIField(DisplayName = "Mon")]
  public virtual int? Mon { get; set; }

  /// <summary>
  /// The minutes of work time reported for Tuesday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.tue))]
  [PXUIField(DisplayName = "Tue")]
  public virtual int? Tue { get; set; }

  /// <summary>
  /// The minutes of work time reported for Wednesday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.wed))]
  [PXUIField(DisplayName = "Wed")]
  public virtual int? Wed { get; set; }

  /// <summary>
  /// The minutes of work time reported for Thursday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.thu))]
  [PXUIField(DisplayName = "Thu")]
  public virtual int? Thu { get; set; }

  /// <summary>
  /// The minutes of work time reported for Friday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.fri))]
  [PXUIField(DisplayName = "Fri")]
  public virtual int? Fri { get; set; }

  /// <summary>
  /// The minutes of work time reported for Saturday, including overtime.
  /// </summary>
  [PXTimeList]
  [PXDBInt(BqlField = typeof (EPTimeCardSummary.sat))]
  [PXUIField(DisplayName = "Sat")]
  public virtual int? Sat { get; set; }

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that the time is billable.
  /// </summary>
  [PXDBBool(BqlField = typeof (EPTimeCardSummary.isBillable))]
  [PXDefault]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? IsBillable { get; set; }

  /// <summary>The description of the reported work hours.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (EPTimeCardSummary.description))]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (EPActivityApprove.approvalStatus))]
  public virtual string HasApprove { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (EPActivityReject.approvalStatus))]
  public virtual string HasReject { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (EPActivityComplite.approvalStatus))]
  public virtual string HasComplite { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (EPActivityOpen.approvalStatus))]
  public virtual string HasOpen { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPWeekRaw">week</see> associated with this time activity.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.EP.EPWeekRaw.Description" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (EPTimeCard.weekId))]
  [PXUIField(DisplayName = "Week")]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  public virtual int? WeekID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">person</see> authorized to approve these time activities.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (PMProject.approverID))]
  [PXEPEmployeeSelector]
  [PXUIField]
  public int? ApproverID
  {
    get => this.TaskApproverID ?? this._ProjectApproverID;
    set => this._ProjectApproverID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">person</see> authorized to approve the task associated with these time activities.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (PMTask.approverID))]
  [PXEPEmployeeSelector]
  [PXUIField]
  public int? TaskApproverID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CREmployee">user</see> to whom this time activity's <see cref="T:PX.Objects.EP.EPSummaryApprove">employee</see> is associated with, if any.
  /// <para></para>
  /// See also <see cref="T:PX.TM.SubordinateOwnerEmployeeAttribute"></see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [Owner(BqlField = typeof (PMTimeActivity.ownerID), DisplayName = "Employee")]
  public virtual int? OwnerID { set; get; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.EP.EPEmployee">employee</see> whose summary rows you want to work with.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CREmployee.bAccountID))]
  [PXSelector(typeof (Search<CREmployee.bAccountID>), DescriptionField = typeof (CREmployee.acctName))]
  [PXUIField(DisplayName = "Employee")]
  public virtual int? EmployeeID { set; get; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object associated with this activity.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID"></see> field.
  /// </value>
  [PXNote(BqlField = typeof (EPTimeCardSummary.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<EPSummaryApprove>.By<EPSummaryApprove.timeCardCD, EPSummaryApprove.lineNbr>
  {
    public static EPSummaryApprove Find(
      PXGraph graph,
      string timeCardCD,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (EPSummaryApprove) PrimaryKeyOf<EPSummaryApprove>.By<EPSummaryApprove.timeCardCD, EPSummaryApprove.lineNbr>.FindBy(graph, (object) timeCardCD, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Time Card</summary>
    public class Timecard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<EPSummaryApprove>.By<EPSummaryApprove.timeCardCD>
    {
    }

    /// <summary>Earning Type</summary>
    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<EPSummaryApprove>.By<EPSummaryApprove.earningType>
    {
    }

    /// <summary>Project</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<EPSummaryApprove>.By<EPSummaryApprove.projectID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<EPSummaryApprove>.By<EPSummaryApprove.projectID, EPSummaryApprove.projectTaskID>
    {
    }

    /// <summary>Shift Code</summary>
    public class ShiftCode : 
      PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>.ForeignKeyOf<EPSummaryApprove>.By<EPSummaryApprove.shiftID>
    {
    }
  }

  public abstract class isApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSummaryApprove.isApprove>
  {
  }

  public abstract class isReject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSummaryApprove.isReject>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.timeCardCD>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.lineNbr>
  {
  }

  public abstract class earningType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.earningType>
  {
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.jobID>
  {
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.shiftID>
  {
  }

  public abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPSummaryApprove.parentNoteID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.projectID>
  {
  }

  public abstract class projectDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSummaryApprove.projectDescription>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.projectTaskID>
  {
  }

  public abstract class projectTaskDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSummaryApprove.projectTaskDescription>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.timeSpent>
  {
  }

  public abstract class sun : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.sun>
  {
  }

  public abstract class mon : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.mon>
  {
  }

  public abstract class tue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.tue>
  {
  }

  public abstract class wed : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.wed>
  {
  }

  public abstract class thu : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.thu>
  {
  }

  public abstract class fri : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.fri>
  {
  }

  public abstract class sat : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.sat>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPSummaryApprove.isBillable>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.description>
  {
  }

  public abstract class hasApprove : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.hasApprove>
  {
  }

  public abstract class hasReject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.hasReject>
  {
  }

  public abstract class hasComplite : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.hasComplite>
  {
  }

  public abstract class hasOpen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPSummaryApprove.hasOpen>
  {
  }

  public abstract class weekId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.weekId>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.approverID>
  {
  }

  public abstract class taskApproverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.taskApproverID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.ownerID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryApprove.employeeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPSummaryApprove.noteID>
  {
  }
}
