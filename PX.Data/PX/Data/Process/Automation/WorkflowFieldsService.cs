// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.Automation.WorkflowFieldsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Process.Automation;

internal class WorkflowFieldsService : IWorkflowFieldsService
{
  private readonly IAUWorkflowFormsEngine _workflowFormsEngine;
  private readonly IAUWorkflowActionsEngine _workflowActionsEngine;

  public WorkflowFieldsService(
    IAUWorkflowFormsEngine workflowFormsEngine,
    IAUWorkflowActionsEngine workflowActionsEngine)
  {
    this._workflowFormsEngine = workflowFormsEngine;
    this._workflowActionsEngine = workflowActionsEngine;
  }

  public void SetFormValues(
    PXGraph graph,
    string form,
    IDictionary<string, object> values,
    bool useMulti = false)
  {
    this._workflowFormsEngine.SetFormValues(graph, form, values, useMulti);
  }

  public Dictionary<string, AUWorkflowFormField[]> GetFormFields(string screenId)
  {
    return EnumerableExtensions.Distinct<AUScreenActionBaseState, string>(this._workflowActionsEngine.GetActionDefinitions(screenId).Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (definition => definition.Form != null)), (Func<AUScreenActionBaseState, string>) (definition => definition.Form)).ToDictionary<AUScreenActionBaseState, string, AUWorkflowFormField[]>((Func<AUScreenActionBaseState, string>) (definition => definition.Form), (Func<AUScreenActionBaseState, AUWorkflowFormField[]>) (definition => this._workflowFormsEngine.GetFormDefinition(screenId, definition.Form).Fields));
  }
}
