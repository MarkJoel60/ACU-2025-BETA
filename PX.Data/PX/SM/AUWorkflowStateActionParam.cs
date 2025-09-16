// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateActionParam
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Action Parameter")]
public class AUWorkflowStateActionParam : AUWorkflowBaseTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenActionBaseState.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXDefault(typeof (AUScreenActionBaseState.actionName))]
  [PXUIField(DisplayName = "Action")]
  public virtual string ActionName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Parameter")]
  public virtual string Parameter { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenActionBaseState))]
  [PXParent(typeof (Select<AUScreenActionBaseState, Where2<Where<AUScreenActionBaseState.screenID, Equal<Current<AUWorkflowStateActionParam.screenID>>>, PX.Data.And<Where<AUScreenActionBaseState.actionName, Equal<Current<AUWorkflowStateActionParam.actionName>>>>>>))]
  public virtual int? StateActionParamLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsFromScheme { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateActionParam.screenID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateActionParam.actionName>
  {
  }

  public abstract class parameter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateActionParam.parameter>
  {
  }

  public abstract class stateActionParamLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowStateActionParam.stateActionParamLineNbr>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionParam.isFromScheme>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowStateActionParam.value>
  {
  }
}
