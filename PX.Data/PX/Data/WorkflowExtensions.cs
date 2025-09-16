// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class WorkflowExtensions
{
  public static bool IsWorkflowExists(this PXGraph graph)
  {
    if (graph == null)
      return false;
    bool? nullable = graph.WorkflowService?.IsWorkflowExists(graph);
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public static bool IsWorkflowDefinitionDefined(this PXGraph graph)
  {
    if (graph == null)
      return false;
    bool? nullable = graph.WorkflowService?.IsWorkflowDefinitionDefined(graph);
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public static bool IsWorkflowServiceEnabled(this PXGraph graph)
  {
    if (graph != null)
    {
      bool? nullable = graph.WorkflowService?.IsEnabled(graph);
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        return true;
    }
    if (graph == null)
      return false;
    bool? nullable1 = graph.WorkflowService?.IsWorkflowExists(graph);
    bool flag1 = true;
    return nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue;
  }

  public static bool IsScreenConfigurationCustomized(this PXGraph graph)
  {
    if (graph == null)
      return false;
    bool? nullable = graph.WorkflowService?.IsScreenConfigurationCustomized(graph);
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public static void ApplyWorkflowState(this PXGraph graph, object record)
  {
    object row = PXResult.UnwrapFirst(record);
    PXWorkflowService workflowService = graph.WorkflowService;
    if (workflowService == null || !workflowService.IsEnabled(graph) && !workflowService.IsWorkflowExists(graph, row))
      return;
    workflowService.ApplyWorkflowState(graph, row);
  }
}
