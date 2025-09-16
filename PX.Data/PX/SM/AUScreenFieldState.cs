// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenFieldState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUScreenFieldState : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  public virtual string TableName { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  public virtual string FieldName { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName { get; set; }

  [PXDBBool]
  public virtual bool? IsEnabled { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string EnableCondition { get; set; }

  [PXDBBool]
  public virtual bool? IsVisible { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string VisibleCondition { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Disable Condition")]
  public virtual string DisableCondition { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Hide Condition")]
  public virtual string HideCondition { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? IsRequired { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Required Condition")]
  public virtual string RequiredCondition { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Combo Box Values")]
  public virtual string ComboBoxValues { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "From Schema")]
  public virtual bool? IsFromSchema { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string DefaultValue { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldState.screenID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldState.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenFieldState.fieldName>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.displayName>
  {
  }

  public abstract class isEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenFieldState.isEnabled>
  {
  }

  public abstract class enableCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.enableCondition>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenFieldState.isVisible>
  {
  }

  public abstract class visibleCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.visibleCondition>
  {
  }

  public abstract class disableCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.disableCondition>
  {
  }

  public abstract class hideCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.hideCondition>
  {
  }

  public abstract class isRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenFieldState.isRequired>
  {
  }

  public abstract class requiredCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.requiredCondition>
  {
  }

  public abstract class comboBoxValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.comboBoxValues>
  {
  }

  public abstract class isFromSchema : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenFieldState.isFromSchema>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenFieldState.defaultValue>
  {
  }
}
