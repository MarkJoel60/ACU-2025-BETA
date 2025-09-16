// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXScreenConfiguration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>A container for the workflow context.</summary>
public class PXScreenConfiguration
{
  private IWorkflowContext _workflowContext;

  internal Readonly.ScreenConfiguration Result => this._workflowContext?.Result;

  /// <summary>
  /// Creates an empty configuration or returns an existing screen configuration.
  /// </summary>
  /// <typeparam name="TGraph">A graph that defines business logic of the screen.</typeparam>
  /// <typeparam name="TPrimary">A primary DAC of the screen.</typeparam>
  /// <returns></returns>
  public WorkflowContext<TGraph, TPrimary> GetScreenConfigurationContext<TGraph, TPrimary>()
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    if (this._workflowContext == null)
      this._workflowContext = (IWorkflowContext) new WorkflowContext<TGraph, TPrimary>();
    if (this._workflowContext is WorkflowContext<TGraph, TPrimary>)
      return (WorkflowContext<TGraph, TPrimary>) this._workflowContext;
    this._workflowContext = (IWorkflowContext) new WorkflowContext<TGraph, TPrimary>(this._workflowContext.Result.To<TGraph, TPrimary>());
    return (WorkflowContext<TGraph, TPrimary>) this._workflowContext;
  }
}
