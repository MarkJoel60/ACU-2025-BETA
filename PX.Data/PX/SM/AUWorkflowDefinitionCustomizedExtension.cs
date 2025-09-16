// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowDefinitionCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowDefinitionCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowDefinition>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StateFieldCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FlowTypeFieldCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? EnableWorkflowIDFieldCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FlowSubTypeFieldCustomized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public bool? EnableWorkflowSubTypeFieldCustomized { get; set; }

  public abstract class stateFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinitionCustomizedExtension.stateFieldCustomized>
  {
  }

  public abstract class flowTypeFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinitionCustomizedExtension.flowTypeFieldCustomized>
  {
  }

  public abstract class enableWorkflowIDFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinitionCustomizedExtension.enableWorkflowIDFieldCustomized>
  {
  }

  public abstract class flowSubTypeFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinitionCustomizedExtension.flowSubTypeFieldCustomized>
  {
  }

  public abstract class enableWorkflowSubTypeFieldCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowDefinitionCustomizedExtension.enableWorkflowSubTypeFieldCustomized>
  {
  }
}
