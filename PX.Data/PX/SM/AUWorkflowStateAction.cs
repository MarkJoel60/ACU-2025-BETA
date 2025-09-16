// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("State Action")]
public class AUWorkflowStateAction : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflowState.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.workflowGUID))]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.identifier))]
  [PXUIField(DisplayName = "State")]
  public virtual string StateName { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Action")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ActionName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowState))]
  [PXParent(typeof (Select<AUWorkflowState, Where2<Where<AUWorkflowState.screenID, Equal<Current<AUWorkflowStateAction.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowState.workflowGUID, Equal<Current<AUWorkflowStateAction.workflowGUID>>>, PX.Data.And<Where<AUWorkflowState.identifier, Equal<Current<AUWorkflowStateAction.stateName>>>>>>>>))]
  public virtual int? StateActionLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Top Level")]
  public bool? IsTopLevel { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disabled")]
  public bool? IsDisabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden")]
  public bool? IsHide { get; set; }

  [PXDBString]
  [PXDefault("False")]
  [PXUIField(DisplayName = "Auto-Run Action")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string AutoRun { get; set; }

  [PXDBString(15)]
  [PXUIField(DisplayName = "Connotation")]
  public virtual string Connotation { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowStateAction.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowStateAction.workflowGUID>
  {
  }

  public abstract class stateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateAction.stateName>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateAction.actionName>
  {
  }

  public abstract class stateActionLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowStateAction.stateActionLineNbr>
  {
  }

  public abstract class isTopLevel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowStateAction.isTopLevel>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowStateAction.isDisabled>
  {
  }

  public abstract class isHide : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowStateAction.isHide>
  {
  }

  public abstract class autoRun : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowStateAction.autoRun>
  {
  }

  public abstract class connotation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowStateAction.connotation>
  {
  }
}
