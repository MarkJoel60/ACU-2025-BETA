// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateActionFieldCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowStateActionFieldCustomizedExtension : 
  PXCacheExtension<
  #nullable disable
  AUWorkflowStateActionField>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateActionFieldLineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IsFromSchemeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ValueCustomized { get; set; } = new bool?(false);

  public abstract class stateActionFieldLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionFieldCustomizedExtension.stateActionFieldLineNbrCustomized>
  {
  }

  public abstract class isFromSchemeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionFieldCustomizedExtension.isFromSchemeCustomized>
  {
  }

  public abstract class valueCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateActionFieldCustomizedExtension.valueCustomized>
  {
  }
}
