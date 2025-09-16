// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStatePropertyCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowStatePropertyCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowStateProperty>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StatePropertyLineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsDisabledCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsHideCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsRequiredCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DefaultValueCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ComboBoxValuesCustomized { get; set; } = new bool?(false);

  public abstract class statePropertyLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.statePropertyLineNbrCustomized>
  {
  }

  public abstract class isDisabledCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.isDisabledCustomized>
  {
  }

  public abstract class isHideCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.isHideCustomized>
  {
  }

  public abstract class isRequiredCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.isRequiredCustomized>
  {
  }

  public abstract class defaultValueCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.defaultValueCustomized>
  {
  }

  public abstract class comboBoxValuesCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStatePropertyCustomizedExtension.comboBoxValuesCustomized>
  {
  }
}
