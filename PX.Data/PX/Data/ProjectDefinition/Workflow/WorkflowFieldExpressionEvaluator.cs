// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.WorkflowFieldExpressionEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation.Services;
using PX.SM;
using System;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class WorkflowFieldExpressionEvaluator : INavigationExpressionEvaluator
{
  private readonly IAUWorkflowFormsEngine _auWorkflowFormsEngine;

  public WorkflowFieldExpressionEvaluator(IAUWorkflowFormsEngine auWorkflowFormsEngine)
  {
    this._auWorkflowFormsEngine = auWorkflowFormsEngine;
  }

  public object Evaluate(
    PXGraph graph,
    object row,
    string expression,
    bool? isFromSchema,
    bool useExt,
    bool applyMask = false)
  {
    PXCache primaryCache = graph.GetPrimaryCache();
    bool? nullable = isFromSchema;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      if (expression == null || expression.StartsWith("=") || expression.Contains("'") || expression.Contains("(") || expression.Contains("@") || expression.Contains("["))
        return WorkflowExpressionParser.Eval(primaryCache, (string) null, expression, this._auWorkflowFormsEngine.GetFormValues(graph), row ?? primaryCache.Current, true);
      string[] strArray = expression != null ? expression.Split('.', 2) : (string[]) null;
      string fieldName = strArray == null || strArray.Length < 2 ? expression : strArray[1];
      return PXFieldState.UnwrapValue(primaryCache.GetValueExt(primaryCache.Current, fieldName));
    }
    if (expression != null && RelativeDatesManager.IsRelativeDatesString(expression))
      return (object) RelativeDatesManager.EvaluateAsDateTime(expression);
    if (expression != null && expression.Equals("@me", StringComparison.OrdinalIgnoreCase))
      return WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(primaryCache, (string) null);
    return expression != null && expression.Equals("@branch", StringComparison.OrdinalIgnoreCase) ? (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch() : (object) expression;
  }

  internal static object GetCurrentUserOrContact(PXCache cache, string fieldName)
  {
    object returnValue = (object) PXAccess.GetUserID();
    if (fieldName != null)
    {
      if ((cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt ? stateExt.DataType : (System.Type) null) == typeof (int))
        returnValue = (object) PXAccess.GetContactID();
      if (stateExt?.DataType != returnValue.GetType())
      {
        cache.RaiseFieldSelecting(fieldName, (object) null, ref returnValue, false);
        returnValue = PXFieldState.UnwrapValue(returnValue);
        if (returnValue == null)
        {
          if (stateExt != null)
          {
            System.Type dataType = stateExt.DataType;
          }
          returnValue = !(stateExt?.DataType == typeof (int)) ? (!(stateExt?.DataType == typeof (string)) ? (object) PXAccess.GetUserID() : (object) PXAccess.GetUserName()) : (object) PXAccess.GetUserID();
        }
      }
      else if (stateExt?.DataType == typeof (string))
        returnValue = (object) PXAccess.GetUserName();
    }
    return returnValue;
  }

  internal static int? GetCurrentBranch() => PXAccess.GetBranchID();
}
