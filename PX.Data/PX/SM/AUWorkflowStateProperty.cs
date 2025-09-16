// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateProperty
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("State Property")]
public class AUWorkflowStateProperty : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  public 
  #nullable disable
  AUWorkflowStateProperty FlowLevelDefaults;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflowState.screenID))]
  public virtual string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.workflowGUID))]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.identifier))]
  [PXUIField(DisplayName = "State")]
  public virtual string StateName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Table Name")]
  public virtual string ObjectName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowState))]
  [PXParent(typeof (Select<AUWorkflowState, Where2<Where<AUWorkflowState.screenID, Equal<Current<AUWorkflowStateProperty.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowState.workflowGUID, Equal<Current<AUWorkflowStateProperty.workflowGUID>>>, PX.Data.And<Where<AUWorkflowState.identifier, Equal<Current<AUWorkflowStateProperty.stateName>>>>>>>>))]
  public virtual int? StatePropertyLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disabled")]
  public bool? IsDisabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden")]
  public bool? IsHide { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public bool? IsRequired { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string DefaultValue { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Combo Box Values", Visible = false)]
  public virtual string ComboBoxValues { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowStateProperty.workflowGUID>
  {
  }

  public abstract class stateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.stateName>
  {
  }

  public abstract class objectName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.objectName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.fieldName>
  {
  }

  public abstract class statePropertyLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowStateProperty.statePropertyLineNbr>
  {
  }

  public abstract class isDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateProperty.isDisabled>
  {
  }

  public abstract class isHide : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowStateProperty.isHide>
  {
  }

  public abstract class isRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateProperty.isRequired>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.defaultValue>
  {
  }

  public abstract class comboBoxValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowStateProperty.comboBoxValues>
  {
  }
}
