// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowTransition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Transition")]
public class AUWorkflowTransition : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflow.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.workflowGUID))]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Transition ID", Visible = false)]
  public virtual Guid? TransitionID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (AUWorkflowState.identifier))]
  [PXUIField(DisplayName = "Original State", Enabled = false, Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FromStateName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowState))]
  [PXParent(typeof (Select<AUWorkflowState, Where2<Where<AUWorkflowState.screenID, Equal<Current<AUWorkflowTransition.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowState.workflowGUID, Equal<Current<AUWorkflowTransition.workflowGUID>>>, PX.Data.And<Where<AUWorkflowState.identifier, Equal<Current<AUWorkflowTransition.fromStateName>>>>>>>>))]
  public virtual int? TransitionLineNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Target State", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string TargetStateName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Condition")]
  public virtual Guid? ConditionID { get; set; }

  [PXDBString(50)]
  [PXDefault]
  [PXUIField(DisplayName = "Action Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ActionName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Persist")]
  public virtual bool? DisablePersist { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {1, 2}, new string[] {"Triggered By Action", "Triggered By Event Handler"})]
  [PXDefault(1)]
  public virtual int? TriggeredBy { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Layout")]
  public virtual string Layout { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowTransition.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowTransition.workflowGUID>
  {
  }

  public abstract class transitionID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowTransition.transitionID>
  {
  }

  public abstract class fromStateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransition.fromStateName>
  {
  }

  public abstract class transitionLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowTransition.transitionLineNbr>
  {
  }

  public abstract class targetStateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransition.targetStateName>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransition.displayName>
  {
  }

  public abstract class conditionID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUWorkflowTransition.conditionID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransition.actionName>
  {
  }

  public abstract class disablePersist : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransition.disablePersist>
  {
  }

  public abstract class triggeredBy : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowTransition.triggeredBy>
  {
    public const int ByAction = 1;
    public const int ByEventHandler = 2;

    public class byAction : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    AUWorkflowTransition.triggeredBy.byAction>
    {
      public byAction()
        : base(1)
      {
      }
    }

    public class byEventHandler : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      AUWorkflowTransition.triggeredBy.byEventHandler>
    {
      public byEventHandler()
        : base(2)
      {
      }
    }
  }

  public abstract class layout : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowTransition.layout>
  {
  }
}
