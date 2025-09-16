// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EPAssignmentMaint))]
[DebuggerDisplay("Name={Name} WorkgroupID={WorkgroupID} RuleID={RuleID}")]
[PXCacheName("Assignment/Approval Rule")]
[Serializable]
public class EPRule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXSequentialNote(SuppressActivitiesCount = true, IsKey = true)]
  [PXUIField(DisplayName = "Rule ID")]
  public virtual Guid? RuleID { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (EPAssignmentMap.assignmentMapID))]
  [PXParent(typeof (Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPRule.assignmentMapID>>>>))]
  public virtual int? AssignmentMapID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Step ID")]
  public virtual Guid? StepID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Seq.", Enabled = false)]
  [PXDefault(1)]
  public virtual int? Sequence { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string Name { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Step")]
  public virtual string StepName { get; set; }

  [PXString(250)]
  public virtual string Icon { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPRuleType.List]
  [PXUIField(DisplayName = "Approver")]
  [PXDefault("D")]
  public virtual string RuleType { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPApproveType.List]
  [PXUIField(DisplayName = "On Approval")]
  [PXDefault("W")]
  public virtual string ApproveType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPEmptyStepType.List]
  [PXUIField(DisplayName = "If No Approver Found")]
  [PXDefault("N")]
  public virtual string EmptyStepType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPExecuteStep.List]
  [PXUIField(DisplayName = "Execute Step")]
  [PXDefault("A")]
  public virtual string ExecuteStep { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPReasonSettings.List]
  [PXUIField(DisplayName = "Reason for Approval")]
  [PXDefault("N")]
  public virtual string ReasonForApprove { get; set; }

  [PXDBString(1, IsFixed = true)]
  [EPReasonSettings.List]
  [PXUIField(DisplayName = "Reason for Rejection")]
  [PXDefault("N")]
  public virtual string ReasonForReject { get; set; }

  [PXDefault(0)]
  [PXUIField]
  [PXDBTimeSpanLong]
  public virtual int? WaitTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXCompanyTreeSelector]
  public virtual int? WorkgroupID { get; set; }

  [Owner(typeof (EPRule.workgroupID), DisplayName = "Employee")]
  public virtual int? OwnerID { get; set; }

  [PXDBString(250)]
  [PXUIField]
  public virtual string OwnerSource { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, the reassign (via action Reassign) of approval request is prohibited.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Reassignment of Approvals")]
  public virtual bool? AllowReassignment { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

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

  public class PK : PrimaryKeyOf<EPRule>.By<EPRule.ruleID>
  {
    public static EPRule Find(PXGraph graph, Guid? ruleID, PKFindOptions options = 0)
    {
      return (EPRule) PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.FindBy(graph, (object) ruleID, options);
    }
  }

  public static class FK
  {
  }

  public abstract class ruleID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRule.ruleID>
  {
  }

  public abstract class assignmentMapID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRule.assignmentMapID>
  {
  }

  public abstract class stepID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRule.stepID>
  {
  }

  public abstract class sequence : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRule.sequence>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.name>
  {
  }

  public abstract class stepname : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.stepname>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.icon>
  {
  }

  public abstract class ruleType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.ruleType>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPRule.isActive>
  {
  }

  public abstract class approveType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.approveType>
  {
  }

  public abstract class emptyStepType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.emptyStepType>
  {
  }

  public abstract class executeStep : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.executeStep>
  {
  }

  public abstract class reasonForApprove : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.reasonForApprove>
  {
  }

  public abstract class reasonForReject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.reasonForReject>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRule.waitTime>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRule.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRule.ownerID>
  {
  }

  public abstract class ownerSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRule.ownerSource>
  {
  }

  public abstract class allowReassignment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPRule.allowReassignment>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPRule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPRule.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPRule.Tstamp>
  {
  }
}
