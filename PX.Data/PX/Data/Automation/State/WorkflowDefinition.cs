// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.WorkflowDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class WorkflowDefinition
{
  public readonly bool IsEnabled;
  public readonly string StateFieldName;
  public readonly bool IsMultiple;
  public readonly string FlowFieldName;
  public readonly StateMap<Workflow> Workflows = new StateMap<Workflow>();

  public WorkflowDefinition(
    bool isEnabled,
    string stateFieldName,
    bool isMultiple,
    string flowFieldName)
  {
    this.IsEnabled = isEnabled;
    this.StateFieldName = stateFieldName;
    this.IsMultiple = isMultiple;
    this.FlowFieldName = flowFieldName;
  }
}
