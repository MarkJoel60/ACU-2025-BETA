// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityApprove
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[Serializable]
public class EPActivityApprove : PMTimeActivity
{
  protected bool? _IsApproved;
  protected bool? _IsReject;

  [PXDBBool]
  [PXDefault(true)]
  public override bool? TrackTime { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Approve")]
  public virtual bool? IsApproved
  {
    get => new bool?(((int) this._IsApproved ?? (this.ApprovalStatus == "AP" ? 1 : 0)) != 0);
    set
    {
      this._IsApproved = value;
      if (!this._IsApproved.GetValueOrDefault())
        return;
      this._IsReject = new bool?(false);
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Reject")]
  public virtual bool? IsReject
  {
    get => new bool?(((int) this._IsReject ?? (this.ApprovalStatus == "RJ" ? 1 : 0)) != 0);
    set
    {
      this._IsReject = value;
      if (!this._IsReject.GetValueOrDefault())
        return;
      this._IsApproved = new bool?(false);
    }
  }

  [PXDBInt(BqlField = typeof (PMTimeActivity.contractID))]
  [PXUIField(DisplayName = "Contract", Visible = false)]
  [PXSelector(typeof (Search2<ContractExEx.contractID, LeftJoin<ContractBillingSchedule, On<ContractExEx.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<ContractExEx.baseType, Equal<CTPRType.contract>>, OrderBy<Desc<ContractExEx.contractCD>>>), DescriptionField = typeof (ContractExEx.description), SubstituteKey = typeof (ContractExEx.contractCD), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  public override int? ContractID { set; get; }

  [EPActivityProjectDefault(typeof (PMTimeActivity.isBillable))]
  [EPProject(typeof (EPActivityApprove.ownerID), FieldClass = "PROJECT", BqlField = typeof (PMTimeActivity.projectID))]
  [PXFormula(typeof (Switch<Case<Where<Not<FeatureInstalled<FeaturesSet.projectAccounting>>>, DefaultValue<EPActivityApprove.projectID>, Case<Where<PMTimeActivity.isBillable, Equal<True>, And<Current2<EPActivityApprove.projectID>, Equal<NonProject>>>, Null, Case<Where<PMTimeActivity.isBillable, Equal<False>, And<Current2<EPActivityApprove.projectID>, IsNull>>, DefaultValue<EPActivityApprove.projectID>>>>, EPActivityApprove.projectID>))]
  public override int? ProjectID { get; set; }

  /// <summary>Gets sets Project's Description</summary>
  [PXUIField(DisplayName = "Project Description", IsReadOnly = true, Visible = false)]
  [PXString]
  [PXFieldDescription]
  [PXUnboundDefault(typeof (Search<PMProject.description, Where<PMProject.contractID, Equal<Current<EPActivityApprove.projectID>>>>))]
  [PXFormula(typeof (Default<EPActivityApprove.projectID>))]
  public 
  #nullable disable
  string ProjectDescription { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPActivityApprove.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ProjectTask(typeof (EPActivityApprove.projectID), "TA", DisplayName = "Project Task")]
  [PXFormula(typeof (Switch<Case<Where<Current2<EPActivityApprove.projectID>, Equal<NonProject>>, Null>, EPActivityApprove.projectTaskID>))]
  [PXForeignReference(typeof (CompositeKey<Field<EPActivityApprove.projectID>.IsRelatedTo<PMTask.projectID>, Field<EPActivityApprove.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public override int? ProjectTaskID { get; set; }

  /// <summary>Gets sets Project Task's Description</summary>
  [PXUIField(DisplayName = "Project Task Description", IsReadOnly = true, Visible = false)]
  [PXString]
  [PXFieldDescription]
  [PXUnboundDefault(typeof (Search<PMTask.description, Where<PMTask.projectID, Equal<Current<EPActivityApprove.projectID>>, And<PMTask.taskID, Equal<Current<EPActivityApprove.projectTaskID>>>>>))]
  [PXFormula(typeof (Default<EPActivityApprove.projectID, EPActivityApprove.projectTaskID>))]
  public string ProjectTaskDescription { get; set; }

  /// <summary>Gets sets Cost Code's Description</summary>
  [PXUIField(DisplayName = "Cost Code Description", IsReadOnly = true, Visible = false, FieldClass = "COSTCODE")]
  [PXString]
  [PXFieldDescription]
  [PXUnboundDefault(typeof (Search<PMCostCode.description, Where<PMCostCode.costCodeID, Equal<Current<PMTimeActivity.costCodeID>>>>))]
  [PXFormula(typeof (Default<PMTimeActivity.costCodeID>))]
  public string CostCodeDescription { get; set; }

  [PXDBDateAndTime(BqlField = typeof (PMTimeActivity.date), DisplayNameDate = "Date", DisplayNameTime = "Time")]
  [PXUIField(DisplayName = "Date")]
  public override DateTime? Date { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.weekID))]
  [PXUIField(DisplayName = "Time Card Week", Enabled = false)]
  [PXWeekSelector2]
  [PXFormula(typeof (Default<EPActivityApprove.date, EPActivityApprove.trackTime>))]
  [EPActivityDefaultWeek(typeof (EPActivityApprove.date))]
  public override int? WeekID { get; set; }

  [PXDBString(10, BqlField = typeof (PMTimeActivity.timeCardCD))]
  [PXUIField]
  public override string TimeCardCD { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PMTimeActivity.approvalStatus))]
  [ActivityStatusList]
  [PXUIField(DisplayName = "Status")]
  [PXDefault("OP")]
  [PXFormula(typeof (Switch<Case<Where<EPActivityApprove.hold, IsNull, Or<EPActivityApprove.hold, Equal<True>>>, ActivityStatusListAttribute.open, Case<Where<PMTimeActivity.released, Equal<True>>, ActivityStatusListAttribute.released, Case<Where<EPActivityApprove.approverID, IsNotNull, And<EPActivityApprove.hold, Equal<False>>>, ActivityStatusListAttribute.pendingApproval>>>, ActivityStatusListAttribute.completed>))]
  public override string ApprovalStatus { get; set; }

  [PXDBInt(BqlField = typeof (PMTimeActivity.approverID))]
  [PXSelector(typeof (Search<EPEmployee.bAccountID>), SubstituteKey = typeof (EPEmployee.acctCD))]
  [PXFormula(typeof (Switch<Case<Where<Selector<EPActivityApprove.projectTaskID, PMTask.approverID>, IsNotNull>, Selector<EPActivityApprove.projectTaskID, PMTask.approverID>>, Null>))]
  [PXUIField]
  public override int? ApproverID { get; set; }

  [PXBool]
  [PXUIField]
  public virtual bool? Hold { get; set; }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPActivityApprove.noteID>
  {
  }

  public new abstract class parentTaskNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPActivityApprove.parentTaskNoteID>
  {
  }

  public new abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPActivityApprove.summary>
  {
  }

  public new abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.timeSpent>
  {
  }

  public new abstract class timeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPActivityApprove.timeBillable>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.ownerID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.workgroupID>
  {
  }

  public new abstract class trackTime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityApprove.trackTime>
  {
  }

  public abstract class isApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityApprove.isApproved>
  {
  }

  public abstract class isReject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityApprove.isReject>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.contractID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.projectID>
  {
  }

  public abstract class projectDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPActivityApprove.projectDescription>
  {
  }

  /// <summary>
  /// Identifier of the <see cref="P:PX.Objects.PM.PMTask.TaskID">TaskID</see>.
  /// </summary>
  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPActivityApprove.projectTaskID>
  {
  }

  public abstract class projectTaskDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPActivityApprove.projectTaskDescription>
  {
  }

  public abstract class costCodeDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPActivityApprove.costCodeDescription>
  {
  }

  public new abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPActivityApprove.date>
  {
  }

  public new abstract class weekID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.weekID>
  {
  }

  public new abstract class timeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPActivityApprove.timeCardCD>
  {
  }

  public new abstract class approvalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPActivityApprove.approvalStatus>
  {
  }

  public new abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPActivityApprove.approverID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPActivityApprove.hold>
  {
  }

  public new abstract class overtimeSpent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPActivityApprove.overtimeSpent>
  {
  }

  public new abstract class overtimeBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPActivityApprove.overtimeBillable>
  {
  }
}
