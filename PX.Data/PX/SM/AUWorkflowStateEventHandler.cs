// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateEventHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("State Event Handler")]
public class AUWorkflowStateEventHandler : 
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
  [PXUIField(DisplayName = "Handler Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string HandlerName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowState))]
  [PXParent(typeof (Select<AUWorkflowState, Where2<Where<AUWorkflowState.screenID, Equal<Current<AUWorkflowStateEventHandler.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowState.workflowGUID, Equal<Current<AUWorkflowStateEventHandler.workflowGUID>>>, PX.Data.And<Where<AUWorkflowState.identifier, Equal<Current<AUWorkflowStateEventHandler.stateName>>>>>>>>))]
  public virtual int? StateHandlerLineNbr { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateEventHandler.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowStateEventHandler.workflowGUID>
  {
  }

  public abstract class stateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateEventHandler.stateName>
  {
  }

  public abstract class handlerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateEventHandler.handlerName>
  {
  }

  public abstract class stateHandlerLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowStateEventHandler.stateHandlerLineNbr>
  {
  }
}
