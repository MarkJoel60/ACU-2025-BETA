// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowStateEventHandlerCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowStateEventHandlerCustomizedExtension : 
  PXCacheExtension<
  #nullable disable
  AUWorkflowStateEventHandler>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateHandlerLineNbrCustomized { get; set; } = new bool?(false);

  public abstract class stateHandlerLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowStateEventHandlerCustomizedExtension.stateHandlerLineNbrCustomized>
  {
  }
}
