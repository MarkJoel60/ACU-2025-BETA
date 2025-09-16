// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflow>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DescriptionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LayoutClientCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? WorkflowSubIDCustomized { get; set; }

  public abstract class lineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCustomizedExtension.lineNbrCustomized>
  {
  }

  public abstract class descriptionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCustomizedExtension.descriptionCustomized>
  {
  }

  public abstract class layoutClientCustomized : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUWorkflowCustomizedExtension.layoutClientCustomized>
  {
  }

  public abstract class workflowSubIDCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowCustomizedExtension.workflowSubIDCustomized>
  {
  }
}
