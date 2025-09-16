// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowActionSequenceFormFieldValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Dialog Box Value")]
public class AUWorkflowActionSequenceFormFieldValue : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem,
  IWorkflowUpdateField,
  IFieldName
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenActionBaseState.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXUIField(DisplayName = "Action Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string PrevActionName { get; set; }

  [PXDBString(50, IsKey = true)]
  [PXUIField(DisplayName = "Action Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string NextActionName { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Condition")]
  public virtual string Condition { get; set; }

  [PXDBString(50, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public virtual bool? IsFromScheme { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "New Value")]
  public virtual string Value { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.screenID>
  {
  }

  public abstract class prevActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.prevActionName>
  {
  }

  public abstract class nextActionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.nextActionName>
  {
  }

  public abstract class condition : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.condition>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.fieldName>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.isFromScheme>
  {
  }

  public abstract class value : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowActionSequenceFormFieldValue.value>
  {
  }
}
