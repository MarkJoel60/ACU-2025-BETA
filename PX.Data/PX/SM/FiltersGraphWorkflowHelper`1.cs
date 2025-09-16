// Decompiled with JetBrains decompiler
// Type: PX.SM.FiltersGraphWorkflowHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Automation;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

internal class FiltersGraphWorkflowHelper<T> : FiltersGraphHelper<T> where T : class, IFilterHeader
{
  private readonly IWorkflowService _workflowService;

  public FiltersGraphWorkflowHelper(PXGraph filterMaint, IWorkflowService workflowService)
    : base(filterMaint)
  {
    this._workflowService = workflowService;
  }

  protected override PXFieldState[] GetFieldsStates(PXGraph graph, T key)
  {
    PXView pxView;
    return !graph.Views.TryGetValue(key.ViewName, out pxView) ? this._workflowService.GetFormFields(key.ScreenID, graph, key.ViewName) : PXFieldState.GetFields(graph, pxView.BqlSelect.GetTables(), false);
  }

  protected override bool GraphViewContains(PXGraph graph, T key)
  {
    IEnumerable<string> source = graph.WorkflowService.GetActionDefinitions(key.ScreenID).Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (actionDefinition => actionDefinition.Form != null)).Select<AUScreenActionBaseState, string>((Func<AUScreenActionBaseState, string>) (actionDefinition => actionDefinition.Form));
    return graph.Views.ContainsKey(key.ViewName) || source.Contains<string>(key.ViewName);
  }
}
