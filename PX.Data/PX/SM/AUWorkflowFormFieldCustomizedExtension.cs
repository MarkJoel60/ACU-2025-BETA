// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowFormFieldCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowFormFieldCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowFormField>
{
  [PXDBBool]
  [PXDefault(false)]
  public bool? LineNumberCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? SchemaFieldCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? FromSchemeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DefaultValueCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? RequiredCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? RequiredConditionCustomized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? ColumnSpanCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? ControlSizeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? AvailableValuesCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ComboBoxValuesCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? HideConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ComboBoxValuesSourceCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? ComboboxAndDefaultSourceFieldCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? DefaultValueSourceCustomized { get; set; } = new bool?(false);

  public abstract class lineNumberCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.lineNumberCustomized>
  {
  }

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class schemaFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.schemaFieldCustomized>
  {
  }

  public abstract class fromSchemeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.fromSchemeCustomized>
  {
  }

  public abstract class defaultValueCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.defaultValueCustomized>
  {
  }

  public abstract class requiredCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.requiredCustomized>
  {
  }

  public abstract class requiredConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.requiredConditionCustomized>
  {
  }

  public abstract class columnSpanCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.columnSpanCustomized>
  {
  }

  public abstract class controlSizeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.controlSizeCustomized>
  {
  }

  public abstract class availableValuesCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.availableValuesCustomized>
  {
  }

  public abstract class comboBoxValuesCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.comboBoxValuesCustomized>
  {
  }

  public abstract class hideConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.hideConditionCustomized>
  {
  }

  public abstract class comboBoxValuesSourceCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.comboBoxValuesSourceCustomized>
  {
  }

  public abstract class comboboxAndDefaultSourceFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.comboboxAndDefaultSourceFieldCustomized>
  {
  }

  public abstract class defaultValueSourceCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowFormFieldCustomizedExtension.defaultValueSourceCustomized>
  {
  }
}
