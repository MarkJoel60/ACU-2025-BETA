// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.WorkflowExpressionParserParametersProviderHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

public static class WorkflowExpressionParserParametersProviderHelper
{
  public static Dictionary<string, ExpressionParameterInfo> GetAvailableExpressionParameters(
    this PXGraph graph)
  {
    Dictionary<string, ExpressionParameterInfo> expressionParameters = new Dictionary<string, ExpressionParameterInfo>();
    if (graph is IWorkflowExpressionParserParametersProvider parametersProvider)
      EnumerableExtensions.AddRange<string, ExpressionParameterInfo>((IDictionary<string, ExpressionParameterInfo>) expressionParameters, (IEnumerable<KeyValuePair<string, ExpressionParameterInfo>>) parametersProvider.GetAvailableExpressionParameters());
    if (graph.Extensions != null)
    {
      foreach (KeyValuePair<string, ExpressionParameterInfo> keyValuePair in graph.Extensions.OfType<IWorkflowExpressionParserParametersProvider>().SelectMany<IWorkflowExpressionParserParametersProvider, KeyValuePair<string, ExpressionParameterInfo>>((Func<IWorkflowExpressionParserParametersProvider, IEnumerable<KeyValuePair<string, ExpressionParameterInfo>>>) (ext => (IEnumerable<KeyValuePair<string, ExpressionParameterInfo>>) ext.GetAvailableExpressionParameters())))
      {
        if (expressionParameters.ContainsKey(keyValuePair.Key))
          expressionParameters[keyValuePair.Key] = keyValuePair.Value;
        else
          expressionParameters.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    return expressionParameters;
  }
}
