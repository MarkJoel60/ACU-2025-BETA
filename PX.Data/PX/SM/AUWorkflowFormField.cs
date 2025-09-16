// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowFormField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Workflow Form Field")]
public class AUWorkflowFormField : 
  AUWorkflowBaseTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IScreenItem
{
  [PXUIField(DisplayName = "Screen")]
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public 
  #nullable disable
  string Screen { get; set; }

  string IScreenItem.ScreenID => this.Screen;

  [PXUIField(DisplayName = "Dialog Box Name")]
  [PXDBString(50, IsKey = true)]
  public string FormName { get; set; }

  [PXUIField(DisplayName = "Field Name")]
  [PXDBString(50, IsKey = true)]
  public string FieldName { get; set; }

  [PXUIField(DisplayName = "Column Span")]
  [PXDBInt]
  public int? LineNumber { get; set; }

  [PXUIField(DisplayName = "Display Name")]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXDBString(100, IsUnicode = true)]
  public string DisplayName { get; set; }

  [PXUIField(DisplayName = "Schema Field", Required = true)]
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public string SchemaField { get; set; }

  internal SchemaFieldEditors? SchemaFieldEditor
  {
    get
    {
      SchemaFieldEditors? schemaFieldEditors;
      return this.SchemaField.TryParseSchemaFieldEditor(out schemaFieldEditors) ? schemaFieldEditors : new SchemaFieldEditors?();
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? FromScheme { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string DefaultValue { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? Required { get; set; }

  [PXUIField(DisplayName = "Required")]
  [PXDBString(40, IsUnicode = false)]
  public virtual string RequiredCondition { get; set; }

  [PXDBString(40, IsUnicode = false)]
  [PXUIField(DisplayName = "Hidden")]
  public virtual string HideCondition { get; set; }

  public bool IsVisible { get; set; }

  [PXUIField(DisplayName = "Column Span")]
  [PXDBInt]
  public int? ColumnSpan { get; set; }

  [PXUIField(DisplayName = "Control Size")]
  [PXDBString(10)]
  public string ControlSize { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Available Values", Visible = false, Visibility = PXUIVisibility.Dynamic)]
  public string AvailableValues { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Combo Box Values", Visible = false)]
  public virtual string ComboBoxValues { get; set; }

  [PXDBString(1)]
  [PXUIField(DisplayName = "Combo Box Values Source")]
  public virtual string ComboBoxValuesSource { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Source Field Name", Visible = false)]
  public virtual string ComboboxAndDefaultSourceField { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Combo Box Source Field Name", Visible = false)]
  public virtual string DefaultValueSource { get; set; }

  public abstract class screen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowFormField.screen>
  {
  }

  public abstract class formName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowFormField.formName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUWorkflowFormField.fieldName>
  {
  }

  public abstract class lineNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowFormField.lineNumber>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.displayName>
  {
  }

  public abstract class schemaField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.schemaField>
  {
  }

  public abstract class fromScheme : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowFormField.fromScheme>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.defaultValue>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowFormField.required>
  {
  }

  public abstract class requiredCondition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormField.requiredCondition>
  {
  }

  public abstract class hideCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.hideCondition>
  {
  }

  public abstract class columnSpan : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUWorkflowFormField.columnSpan>
  {
  }

  public abstract class controlSize : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.controlSize>
  {
  }

  public abstract class availableValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.availableValues>
  {
  }

  public abstract class comboBoxValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.comboBoxValues>
  {
  }

  public abstract class comboBoxValuesSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.comboBoxValuesSource>
  {
  }

  public abstract class comboboxAndDefaultSourceField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.comboboxAndDefaultSourceField>
  {
  }

  public abstract class defaultValueSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUWorkflowFormField.defaultValueSource>
  {
  }
}
