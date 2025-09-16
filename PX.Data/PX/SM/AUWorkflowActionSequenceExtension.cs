// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowActionSequenceExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowActionSequenceExtension : PXCacheExtension<
#nullable disable
AUWorkflowActionSequence>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LineNbrCustomized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StopOnErrorCustomized { get; set; }

  public abstract class lineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowActionSequenceExtension.lineNbrCustomized>
  {
  }

  public abstract class stopOnErrorCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowActionSequenceExtension.stopOnErrorCustomized>
  {
  }
}
