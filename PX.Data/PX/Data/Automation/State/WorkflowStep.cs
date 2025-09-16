// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.WorkflowStep
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class WorkflowStep
{
  public readonly StateMap<WorkflowStepAction> Actions = new StateMap<WorkflowStepAction>();
  public readonly List<WorkflowStepField> Fields = new List<WorkflowStepField>();
}
