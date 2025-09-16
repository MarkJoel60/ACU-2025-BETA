// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.WorkflowStepAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class WorkflowStepAction
{
  public readonly bool IsEnable;
  public readonly bool TopLevel;
  public readonly bool BatchMode;
  public readonly string ProcessingScreenID;
  public readonly string EnableCondition;
  public readonly List<WorkflowStepTransition> Transitions = new List<WorkflowStepTransition>();

  public WorkflowStepAction(
    bool isEnable,
    bool topLevel,
    bool batchMode,
    string processingScreenID,
    string enableCondition)
  {
    this.IsEnable = isEnable;
    this.TopLevel = topLevel;
    this.BatchMode = batchMode;
    this.ProcessingScreenID = processingScreenID;
    this.EnableCondition = enableCondition;
  }
}
