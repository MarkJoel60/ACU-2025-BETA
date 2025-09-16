// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowTransitionCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUWorkflowTransitionCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowTransition>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FromStateNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? TransitionLineNbrCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? TargetStateNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ConditionIDCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ActionNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisablePersistCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? TriggeredByCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LayoutCustomized { get; set; } = new bool?(false);

  public abstract class fromStateNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.fromStateNameCustomized>
  {
  }

  public abstract class transitionLineNbrCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.transitionLineNbrCustomized>
  {
  }

  public abstract class targetStateNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.targetStateNameCustomized>
  {
  }

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class conditionIDCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.conditionIDCustomized>
  {
  }

  public abstract class actionNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.actionNameCustomized>
  {
  }

  public abstract class disablePersistCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.disablePersistCustomized>
  {
  }

  public abstract class triggeredByCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.triggeredByCustomized>
  {
  }

  public abstract class layoutCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUWorkflowTransitionCustomizedExtension.layoutCustomized>
  {
  }
}
