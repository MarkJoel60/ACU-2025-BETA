// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowActionSequence
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Action Sequence")]
public class AUWorkflowActionSequence : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenActionBaseState.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string PrevActionName { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string NextActionName { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Execution Condition")]
  public virtual string Condition { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenActionBaseState))]
  [PXParent(typeof (Select<AUScreenActionBaseState, Where<AUScreenActionBaseState.screenID, Equal<Current<AUWorkflowActionSequence.screenID>>, PX.Data.And<Where<AUScreenActionBaseState.actionName, Equal<Current<AUWorkflowActionSequence.prevActionName>>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Stop on Error")]
  public virtual bool? StopOnError { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequence.screenID>
  {
  }

  public abstract class prevActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequence.prevActionName>
  {
  }

  public abstract class nextActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequence.nextActionName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUWorkflowActionSequence.condition>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowActionSequence.lineNbr>
  {
  }

  public abstract class stopOnError : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowActionSequence.stopOnError>
  {
  }
}
