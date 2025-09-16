// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowOnLeaveStateField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Update Field On Leave State")]
public class AUWorkflowOnLeaveStateField : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IWorkflowUpdateField,
  IFieldName,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUWorkflowTransition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUWorkflowTransition.workflowGUID))]
  [PXUIField(DisplayName = " ", Visible = false)]
  public virtual string WorkflowGUID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (AUWorkflowState.identifier))]
  [PXUIField(DisplayName = "State", Enabled = false, Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string StateName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXLineNbr(typeof (AUWorkflowState))]
  [PXParent(typeof (Select<AUWorkflowState, Where2<Where<AUWorkflowState.screenID, Equal<Current<AUWorkflowOnLeaveStateField.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowState.workflowGUID, Equal<Current<AUWorkflowOnLeaveStateField.workflowGUID>>>, PX.Data.And<Where<AUWorkflowState.identifier, Equal<Current<AUWorkflowOnLeaveStateField.stateName>>>>>>>>))]
  public virtual int? OnLeaveStateFieldLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsFromScheme { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "New Value")]
  public virtual string Value { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.workflowGUID>
  {
  }

  public abstract class stateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.stateName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.fieldName>
  {
  }

  public abstract class onLeaveStateFieldLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.onLeaveStateFieldLineNbr>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowOnLeaveStateField.isFromScheme>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowOnLeaveStateField.value>
  {
  }
}
