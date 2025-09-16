// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.Automation.LayoutHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json.Linq;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Process.Automation;

public class LayoutHelper
{
  internal static string GetJSON(
    AUWorkflow workflow,
    PXGraph graph,
    IAUWorkflowEngine auWorkflowEngine,
    IAUWorkflowActionsEngine auWorkflowActionsEngine,
    IAUWorkflowEventsEngine auWorkflowEventsEngine,
    IPXPageIndexingService pageIndexingService)
  {
    string screenIdFromGraphType = pageIndexingService.GetScreenIDFromGraphType(graph.GetType());
    JObject jobject1 = new JObject()
    {
      ["class"] = JToken.op_Implicit("GraphLinksModel"),
      ["id"] = JToken.op_Implicit($"{workflow.ScreenID}_{workflow.WorkflowID}_{workflow.WorkflowSubID}"),
      ["copiesArrays"] = JToken.op_Implicit(true),
      ["copiesArrayObjects"] = JToken.op_Implicit(true),
      ["linkFromPortIdProperty"] = JToken.op_Implicit("fromPort"),
      ["linkToPortIdProperty"] = JToken.op_Implicit("toPort")
    };
    JArray jarray1 = new JArray();
    jobject1["nodeDataArray"] = (JToken) jarray1;
    AUWorkflowDefinition screenWorkflows = auWorkflowEngine.GetScreenWorkflows(graph);
    foreach (AUWorkflowState auWorkflowState in (IEnumerable<AUWorkflowState>) auWorkflowEngine.GetStates(workflow).OrderBy<AUWorkflowState, int?>((Func<AUWorkflowState, int?>) (it => it.StateLineNbr)))
    {
      AUWorkflowState state = auWorkflowState;
      string identifier = state.Identifier;
      if (graph.Caches[graph.PrimaryItemType].GetStateExt((object) null, screenWorkflows.StateField) is PXStringState stateExt && stateExt.AllowedValues != null)
        stateExt.ValueLabelDic.TryGetValue(state.Identifier, out identifier);
      JObject jobject2 = new JObject()
      {
        ["key"] = JToken.op_Implicit("Task_" + state.Identifier),
        ["name"] = JToken.op_Implicit(identifier)
      };
      jarray1.Add((JToken) jobject2);
      bool? isInitial = state.IsInitial;
      bool flag = true;
      if (isInitial.GetValueOrDefault() == flag & isInitial.HasValue)
        jobject2["isStart"] = JToken.op_Implicit(true);
      jobject2["loc"] = JToken.op_Implicit(state.GetLayoutOrDefault());
      JArray jarray2 = new JArray();
      jobject2["fields"] = (JToken) jarray2;
      List<string> stringList = new List<string>();
      foreach (AUWorkflowTransition workflowTransition in (IEnumerable<AUWorkflowTransition>) auWorkflowEngine.GetTransitions(workflow).Where<AUWorkflowTransition>((Func<AUWorkflowTransition, bool>) (it => it.FromStateName == state.Identifier)).OrderBy<AUWorkflowTransition, int?>((Func<AUWorkflowTransition, int?>) (it => it.TransitionLineNbr)))
      {
        if (!stringList.Contains(workflowTransition.ActionName.ToLower()))
        {
          string caption = graph.Actions[workflowTransition.ActionName]?.GetCaption();
          AUWorkflowHandler handlerDefinition = auWorkflowEventsEngine.GetHandlerDefinition(screenIdFromGraphType, workflowTransition.ActionName);
          JObject jobject3 = new JObject()
          {
            ["key"] = JToken.op_Implicit(workflowTransition.ActionName.ToLower()),
            ["name"] = JToken.op_Implicit(caption ?? PXLocalizer.Localize(handlerDefinition.DisplayName) ?? workflowTransition.ActionName),
            ["isEvent"] = JToken.op_Implicit(handlerDefinition != null)
          };
          jarray2.Add((JToken) jobject3);
          stringList.Add(workflowTransition.ActionName.ToLower());
        }
      }
    }
    JArray jarray3 = new JArray();
    jobject1["linkDataArray"] = (JToken) jarray3;
    foreach (AUWorkflowTransition workflowTransition in (IEnumerable<AUWorkflowTransition>) (auWorkflowEngine.GetTransitions(workflow) ?? new List<AUWorkflowTransition>()).OrderBy<AUWorkflowTransition, string>((Func<AUWorkflowTransition, string>) (it => it.FromStateName)).ThenBy<AUWorkflowTransition, int?>((Func<AUWorkflowTransition, int?>) (it => it.TransitionLineNbr)))
    {
      AUWorkflowTransition transition = workflowTransition;
      JObject jobject4 = new JObject();
      Guid? nullable = transition.TransitionID;
      ref Guid? local1 = ref nullable;
      jobject4["id"] = JToken.op_Implicit(local1.HasValue ? local1.GetValueOrDefault().ToString().Replace("-", "_") : (string) null);
      jobject4["from"] = JToken.op_Implicit("Task_" + transition.FromStateName);
      jobject4["to"] = JToken.op_Implicit("Task_" + transition.TargetStateName);
      jobject4["fromPort"] = JToken.op_Implicit(transition.ActionName.ToLower());
      JObject jobject5 = jobject4;
      if (!string.IsNullOrEmpty(transition.Layout))
      {
        string layout = transition.Layout;
        char[] chArray = new char[1]{ ',' };
        foreach (string str in layout.Split(chArray))
        {
          if (str == "T" || str == "B")
            jobject5["toPort"] = JToken.op_Implicit(str);
          if (str == "R" || str == "L")
            jobject5["fromPort"] = JToken.op_Implicit($"{jobject5["fromPort"]?.ToString()}_{str}");
        }
      }
      else
        jobject5["toPort"] = JToken.op_Implicit("");
      jarray3.Add((JToken) jobject5);
      nullable = transition.ConditionID;
      if (nullable.HasValue)
      {
        jobject5["hasCondition"] = JToken.op_Implicit(true);
        JObject jobject6 = jobject5;
        IEnumerable<AUScreenConditionState> systemConditionsList = PXSystemWorkflows.GetSystemConditionsList(screenIdFromGraphType);
        JToken jtoken = JToken.op_Implicit(systemConditionsList != null ? systemConditionsList.FirstOrDefault<AUScreenConditionState>((Func<AUScreenConditionState, bool>) (it =>
        {
          Guid? conditionId1 = it.ConditionID;
          Guid? conditionId2 = transition.ConditionID;
          if (conditionId1.HasValue != conditionId2.HasValue)
            return false;
          return !conditionId1.HasValue || conditionId1.GetValueOrDefault() == conditionId2.GetValueOrDefault();
        }))?.ConditionName : (string) null);
        jobject6["condition"] = jtoken;
      }
      AUWorkflowStateAction stateAction = auWorkflowActionsEngine.GetActionStates(screenIdFromGraphType, workflow.WorkflowID, workflow.WorkflowSubID, transition.FromStateName).FirstOrDefault<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (sa => sa.ActionName == transition.ActionName));
      if (stateAction != null && !string.IsNullOrEmpty(stateAction.AutoRun) && !stateAction.AutoRun.Equals("false", StringComparison.OrdinalIgnoreCase))
      {
        jobject5["isAutoRun"] = JToken.op_Implicit(true);
        JObject jobject7 = jobject5;
        IEnumerable<AUScreenConditionState> systemConditionsList = PXSystemWorkflows.GetSystemConditionsList(screenIdFromGraphType);
        JToken jtoken = JToken.op_Implicit((systemConditionsList != null ? systemConditionsList.FirstOrDefault<AUScreenConditionState>((Func<AUScreenConditionState, bool>) (it =>
        {
          Guid? conditionId = it.ConditionID;
          ref Guid? local2 = ref conditionId;
          return (local2.HasValue ? local2.GetValueOrDefault().ToString() : (string) null) == stateAction.AutoRun;
        }))?.ConditionName : (string) null) ?? stateAction.AutoRun);
        jobject7["autoRunCondition"] = jtoken;
      }
    }
    return jobject1.ToString();
  }
}
