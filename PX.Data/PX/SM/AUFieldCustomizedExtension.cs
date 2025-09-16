// Decompiled with JetBrains decompiler
// Type: PX.SM.AUFieldCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUFieldCustomizedExtension : PXCacheExtension<
#nullable disable
AUScreenFieldState>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisableConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HideConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsRequiredCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? RequiredConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ComboBoxValuesCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsFromSchemaCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DefaultValueCustomized { get; set; } = new bool?(false);

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class disableConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.disableConditionCustomized>
  {
  }

  public abstract class hideConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.hideConditionCustomized>
  {
  }

  public abstract class isRequiredCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.isRequiredCustomized>
  {
  }

  public abstract class requiredConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.requiredConditionCustomized>
  {
  }

  public abstract class comboBoxValuesCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.comboBoxValuesCustomized>
  {
  }

  public abstract class isFromSchemaCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.isFromSchemaCustomized>
  {
  }

  public abstract class defaultValueCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUFieldCustomizedExtension.defaultValueCustomized>
  {
  }
}
