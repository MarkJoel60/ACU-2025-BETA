// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.WorkflowStepField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.State;

internal sealed class WorkflowStepField
{
  public readonly string ViewName;
  public readonly string FieldName;
  public readonly bool Hidden;
  public readonly bool Enabled;
  public readonly bool Required;

  public WorkflowStepField(
    string viewName,
    string fieldName,
    bool hidden,
    bool enabled,
    bool required)
  {
    this.ViewName = viewName;
    this.FieldName = fieldName;
    this.Hidden = hidden;
    this.Enabled = enabled;
    this.Required = required;
  }
}
