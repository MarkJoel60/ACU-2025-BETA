// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowTransitionField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Transition Update Fields After")]
public class AUWorkflowTransitionField : 
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

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (AUWorkflowTransition.transitionID))]
  [PXUIField(DisplayName = "Transition ID", Visible = false)]
  public virtual Guid? TransitionID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBInt]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXLineNbr(typeof (AUWorkflowTransition))]
  [PXParent(typeof (Select<AUWorkflowTransition, Where2<Where<AUWorkflowTransition.screenID, Equal<Current<AUWorkflowTransitionField.screenID>>>, PX.Data.And<Where2<Where<AUWorkflowTransition.workflowGUID, Equal<Current<AUWorkflowTransitionField.workflowGUID>>>, PX.Data.And<Where<AUWorkflowTransition.transitionID, Equal<Current<AUWorkflowTransitionField.transitionID>>>>>>>>))]
  public virtual int? TransitionFieldLineNbr { get; set; }

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
    AUWorkflowTransitionField.screenID>
  {
  }

  public abstract class workflowGUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowTransitionField.workflowGUID>
  {
  }

  public abstract class transitionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransitionField.transitionID>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowTransitionField.fieldName>
  {
  }

  public abstract class transitionFieldLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowTransitionField.transitionFieldLineNbr>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionField.isFromScheme>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowTransitionField.value>
  {
  }
}
