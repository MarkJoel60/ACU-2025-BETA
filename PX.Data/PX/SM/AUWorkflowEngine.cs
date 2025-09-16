// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.ProjectDefinition.Workflow;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.SM;

internal class AUWorkflowEngine : IAUWorkflowEngine
{
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;
  public const string AllFields = "<All Fields>";
  public const string Table = "<Table>";
  public const string DefaultWorkflowID = "DEF_WORKFLOW_ID";

  public AUWorkflowEngine(
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService)
  {
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
  }

  private static IEnumerable<T> SelectTable<T>(string screenID, ref bool isCustomized) where T : class, IScreenItem, new()
  {
    return PXSystemWorkflows.SelectTable<T>(screenID, ref isCustomized);
  }

  public bool IsWorkflowCustomized(string screenId)
  {
    return AUWorkflowEngine.Slot.LocallyCachedSlot.Get(screenId).IsCustomized;
  }

  public AUWorkflowDefinition GetScreenWorkflows(PXGraph graph)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    return screenIdFromGraphType == null ? (AUWorkflowDefinition) null : this.GetScreenWorkflows(screenIdFromGraphType);
  }

  public AUWorkflowDefinition GetScreenWorkflows(string screenID)
  {
    return screenID == null ? (AUWorkflowDefinition) null : AUWorkflowEngine.Slot.LocallyCachedSlot.Get(screenID).WorkflowDefinition;
  }

  public AUWorkflow GetCurrentWorkflowAndState(PXGraph graph, out string stateID)
  {
    stateID = (string) null;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return (AUWorkflow) null;
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (AUWorkflow) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (AUWorkflow) null;
    AUWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenIdFromGraphType).WorkflowDefinition;
    if (workflowDefinition == null)
      return (AUWorkflow) null;
    PXCache cache = graph.Views[graph.PrimaryView].Cache;
    object current = cache.Current;
    return this.GetCurrentWorkflowAndState(out stateID, cache, current, workflowDefinition, locallyCachedSlot);
  }

  public AUWorkflow GetCurrentWorkflowAndState(PXGraph graph, object row, out string stateID)
  {
    stateID = (string) null;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return (AUWorkflow) null;
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (AUWorkflow) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (AUWorkflow) null;
    AUWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenIdFromGraphType).WorkflowDefinition;
    if (workflowDefinition == null)
      return (AUWorkflow) null;
    PXCache cache = graph.Views[graph.PrimaryView].Cache;
    object current = row;
    return this.GetCurrentWorkflowAndState(out stateID, cache, current, workflowDefinition, locallyCachedSlot);
  }

  private AUWorkflow FindApplicableWorkflow(
    List<AUWorkflow> workflows,
    string workflowId,
    string workflowSubId)
  {
    if (workflows == null)
      return (AUWorkflow) null;
    return workflowSubId == null ? workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == workflowId && it.WorkflowSubID == null)) ?? workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == null && it.WorkflowSubID == null)) : workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == workflowId && it.WorkflowSubID == workflowSubId)) ?? workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == workflowId && it.WorkflowSubID == null)) ?? workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == null && it.WorkflowSubID == workflowSubId)) ?? workflows.FirstOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID == null && it.WorkflowSubID == null));
  }

  private AUWorkflow GetCurrentWorkflowAndState(
    out string stateId,
    PXCache cache,
    object current,
    AUWorkflowDefinition wd,
    AUWorkflowEngine.Slot slot)
  {
    object obj = PXFieldState.UnwrapValue(cache.GetValueExt(current, wd.StateField));
    stateId = (string) obj;
    return this.FindApplicableWorkflow(slot.Get(wd.ScreenID).Workflows, !string.IsNullOrEmpty(wd.FlowTypeField) ? (string) PXFieldState.UnwrapValue(cache.GetValueExt(current, wd.FlowTypeField)) : (string) null, !string.IsNullOrEmpty(wd.FlowSubTypeField) ? (string) PXFieldState.UnwrapValue(cache.GetValueExt(current, wd.FlowSubTypeField)) : (string) null);
  }

  public AUWorkflow GetCurrentWorkflowAndState(PXCache cache, object row, out string stateID)
  {
    stateID = (string) null;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(cache.Graph.GetType());
    if (screenIdFromGraphType == null)
      return (AUWorkflow) null;
    return string.IsNullOrEmpty(cache.Graph.PrimaryView) ? (AUWorkflow) null : this.GetCurrentWorkflowAndState(screenIdFromGraphType, cache, row, out stateID);
  }

  public AUWorkflow GetCurrentWorkflowAndState(
    string screenID,
    PXCache cache,
    object row,
    out string stateID)
  {
    stateID = (string) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (AUWorkflow) null;
    if (screenID == null)
      return (AUWorkflow) null;
    AUWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID).WorkflowDefinition;
    return workflowDefinition == null ? (AUWorkflow) null : this.GetCurrentWorkflowAndState(out stateID, cache, row, workflowDefinition, locallyCachedSlot);
  }

  public AUWorkflowState GetInitialState(PXGraph graph, string workflowId, string workflowSubId)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (AUWorkflowState) null : this.GetInitialState(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()), workflowId, workflowSubId);
  }

  public AUWorkflowState GetInitialState(string screenID, string workflowId, string workflowSubId)
  {
    if (screenID == null)
      return (AUWorkflowState) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (AUWorkflowState) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    if (workflowDefinition.WorkflowDefinition == null)
      return (AUWorkflowState) null;
    AUWorkflow applicableWorkflow = this.FindApplicableWorkflow(workflowDefinition.Workflows, workflowId, workflowSubId);
    if (applicableWorkflow == null)
      return (AUWorkflowState) null;
    List<AUWorkflowState> source;
    workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow, out source);
    return source == null ? (AUWorkflowState) null : source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it =>
    {
      bool? isInitial = it.IsInitial;
      bool flag = true;
      return isInitial.GetValueOrDefault() == flag & isInitial.HasValue;
    }));
  }

  public AUWorkflowState GetState(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateId)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (AUWorkflowState) null : this.GetState(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()), workflowId, workflowSubId, stateId);
  }

  public AUWorkflowState GetState(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId)
  {
    if (screenID == null)
      return (AUWorkflowState) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (AUWorkflowState) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    if (workflowDefinition.WorkflowDefinition == null)
      return (AUWorkflowState) null;
    AUWorkflow applicableWorkflow = this.FindApplicableWorkflow(workflowDefinition.Workflows, workflowId, workflowSubId);
    if (applicableWorkflow == null)
      return (AUWorkflowState) null;
    List<AUWorkflowState> source;
    workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow, out source);
    return source == null ? (AUWorkflowState) null : source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateId));
  }

  public AUWorkflowState GetNextStateForSequence(
    AUWorkflow workflow,
    AUWorkflowState currentState,
    AUWorkflowState sequence)
  {
    if (sequence?.StateType != "S")
      return (AUWorkflowState) null;
    List<AUWorkflowState> source;
    AUWorkflowEngine.Slot.LocallyCachedSlot.Get(workflow.ScreenID).WorkflowStates.TryGetValue(workflow, out source);
    return source == null ? (AUWorkflowState) null : source.Where<AUWorkflowState>((Func<AUWorkflowState, bool>) (it =>
    {
      if (!(it.ParentState == sequence.Identifier))
        return false;
      int? stateLineNbr = it.StateLineNbr;
      int num = (int?) currentState?.StateLineNbr ?? -1;
      return stateLineNbr.GetValueOrDefault() > num & stateLineNbr.HasValue;
    })).OrderBy<AUWorkflowState, int?>((Func<AUWorkflowState, int?>) (it => it.StateLineNbr)).FirstOrDefault<AUWorkflowState>();
  }

  public IEnumerable<AUWorkflowTransition> GetTransitions(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (IEnumerable<AUWorkflowTransition>) null : this.GetTransitions(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()), workflowId, workflowSubId, stateId, actionName);
  }

  public IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (IEnumerable<AUWorkflowTransition>) null : this.GetTransitionsRecursive(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()), workflowId, workflowSubId, stateId, actionName);
  }

  public IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    string screenId,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName)
  {
    List<AUWorkflowTransition> transitionsRecursive = new List<AUWorkflowTransition>();
    IEnumerable<AUWorkflowTransition> transitions1 = this.GetTransitions(screenId, workflowId, workflowSubId, stateId, actionName);
    if (transitions1 != null)
      transitionsRecursive.AddRange(transitions1);
    for (AUWorkflowState state = this.GetState(screenId, workflowId, workflowSubId, stateId); !string.IsNullOrEmpty(state?.ParentState); state = this.GetState(screenId, workflowId, workflowSubId, state.ParentState))
    {
      IEnumerable<AUWorkflowTransition> transitions2 = this.GetTransitions(screenId, workflowId, workflowSubId, state.ParentState, actionName);
      if (transitions2 != null)
        transitionsRecursive.AddRange(transitions2);
    }
    return (IEnumerable<AUWorkflowTransition>) transitionsRecursive;
  }

  public IEnumerable<AUWorkflowTransition> GetTransitions(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName)
  {
    if (screenID == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    if (workflowDefinition.WorkflowDefinition == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflow applicableWorkflow = this.FindApplicableWorkflow(workflowDefinition.Workflows, workflowId, workflowSubId);
    if (applicableWorkflow == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    List<AUWorkflowState> source1;
    workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow, out source1);
    AUWorkflowState key = source1 != null ? source1.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateId)) : (AUWorkflowState) null;
    if (key == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    List<AUWorkflowTransition> source2;
    workflowDefinition.WorkflowStateTransitions.TryGetValue(key, out source2);
    return source2 == null ? (IEnumerable<AUWorkflowTransition>) null : (IEnumerable<AUWorkflowTransition>) source2.Where<AUWorkflowTransition>((Func<AUWorkflowTransition, bool>) (transition => transition.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase))).ToList<AUWorkflowTransition>();
  }

  public IEnumerable<AUWorkflowTransition> GetTransitions(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId)
  {
    if (screenID == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    if (workflowDefinition.WorkflowDefinition == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    AUWorkflow applicableWorkflow = this.FindApplicableWorkflow(workflowDefinition.Workflows, workflowId, workflowSubId);
    if (applicableWorkflow == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    List<AUWorkflowState> source1;
    workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow, out source1);
    AUWorkflowState key = source1 != null ? source1.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateId)) : (AUWorkflowState) null;
    if (key == null)
      return (IEnumerable<AUWorkflowTransition>) null;
    List<AUWorkflowTransition> source2;
    workflowDefinition.WorkflowStateTransitions.TryGetValue(key, out source2);
    return source2 == null ? (IEnumerable<AUWorkflowTransition>) null : (IEnumerable<AUWorkflowTransition>) source2.ToList<AUWorkflowTransition>();
  }

  public IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    string screenId,
    string workflowId,
    string workflowSubId,
    string stateId)
  {
    List<AUWorkflowTransition> transitionsRecursive = new List<AUWorkflowTransition>();
    IEnumerable<AUWorkflowTransition> transitions1 = this.GetTransitions(screenId, workflowId, workflowSubId, stateId);
    if (transitions1 != null)
      transitionsRecursive.AddRange(transitions1);
    for (AUWorkflowState state = this.GetState(screenId, workflowId, workflowSubId, stateId); !string.IsNullOrEmpty(state?.ParentState); state = this.GetState(screenId, workflowId, workflowSubId, state.ParentState))
    {
      IEnumerable<AUWorkflowTransition> transitions2 = this.GetTransitions(screenId, workflowId, workflowSubId, state.ParentState);
      if (transitions2 != null)
        transitionsRecursive.AddRange(transitions2);
    }
    return (IEnumerable<AUWorkflowTransition>) transitionsRecursive;
  }

  public List<AUWorkflowTransition> GetTransitions(AUWorkflow workflow)
  {
    if (workflow == null)
      return (List<AUWorkflowTransition>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (List<AUWorkflowTransition>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(workflow.ScreenID);
    List<AUWorkflow> workflows = workflowDefinition.Workflows;
    List<AUWorkflowState> states;
    workflowDefinition.WorkflowStates.TryGetValue(workflow, out states);
    return states == null ? (List<AUWorkflowTransition>) null : workflowDefinition.WorkflowStateTransitions.Where<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, bool>) (it => states.Contains(it.Key))).SelectMany<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, AUWorkflowTransition>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, IEnumerable<AUWorkflowTransition>>) (it => (IEnumerable<AUWorkflowTransition>) it.Value)).ToList<AUWorkflowTransition>();
  }

  public IEnumerable<(AUWorkflowTransition transition, AUWorkflow workflow)> GetTransitions(
    string actionValue,
    string sourceStateName)
  {
    if (string.IsNullOrEmpty(actionValue))
      return (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) null;
    if (!actionValue.Contains<char>('$'))
      return (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) null;
    string str1;
    string str2;
    ArrayDeconstruct.Deconstruct<string>(actionValue.Split('$'), ref str1, ref str2);
    string screenId = str1;
    string actionName = str2;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenId);
    if (workflowDefinition.WorkflowDefinition == null)
      return (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) null;
    List<AUWorkflow> workflows = workflowDefinition.Workflows;
    return workflowDefinition.WorkflowStateTransitions.SelectMany<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, AUWorkflowTransition>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, IEnumerable<AUWorkflowTransition>>) (it => (IEnumerable<AUWorkflowTransition>) it.Value)).Where<AUWorkflowTransition>((Func<AUWorkflowTransition, bool>) (tr =>
    {
      if (!string.Equals(tr.ScreenID, screenId, StringComparison.OrdinalIgnoreCase) || !string.Equals(tr.ActionName, actionName, StringComparison.OrdinalIgnoreCase))
        return false;
      return string.IsNullOrEmpty(sourceStateName) || string.Equals(tr.FromStateName, sourceStateName, StringComparison.OrdinalIgnoreCase);
    })).Select<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>((Func<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>) (tr =>
    {
      AUWorkflowTransition workflowTransition = tr;
      List<AUWorkflow> source = workflows;
      AUWorkflow auWorkflow = source != null ? source.SingleOrDefault<AUWorkflow>((Func<AUWorkflow, bool>) (w => w.WorkflowGUID == tr.WorkflowGUID)) : (AUWorkflow) null;
      return (workflowTransition, auWorkflow);
    }));
  }

  public IEnumerable<AUWorkflowStateProperty> GetStatePropertiesRecursive(PXGraph graph)
  {
    string stateID;
    return this.GetStatePropertiesRecursive(this.GetCurrentWorkflowAndState(graph, out stateID), stateID);
  }

  public IEnumerable<AUWorkflowStateProperty> GetStateProperties(PXGraph graph)
  {
    string stateID;
    return this.GetStateProperties(this.GetCurrentWorkflowAndState(graph, out stateID), stateID);
  }

  public IEnumerable<AUWorkflowStateProperty> GetStatePropertiesRecursive(
    AUWorkflow workflow,
    string stateId)
  {
    if (workflow == null)
      return (IEnumerable<AUWorkflowStateProperty>) null;
    List<AUWorkflowStateProperty> result = new List<AUWorkflowStateProperty>();
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = AUWorkflowEngine.Slot.LocallyCachedSlot.Get(workflow.ScreenID);
    List<AUWorkflow> workflows = workflowDefinition.Workflows;
    List<AUWorkflowState> source;
    workflowDefinition.WorkflowStates.TryGetValue(workflow, out source);
    AUWorkflowState state = source != null ? source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateId)) : (AUWorkflowState) null;
    if (state == null)
      return (IEnumerable<AUWorkflowStateProperty>) null;
    List<AUWorkflowStateProperty> collection;
    workflowDefinition.WorkflowStateProperties.TryGetValue(state, out collection);
    if (collection != null)
      result.AddRange((IEnumerable<AUWorkflowStateProperty>) collection);
    while (!string.IsNullOrEmpty(state.ParentState))
    {
      state = source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == state.ParentState));
      if (state != null)
      {
        List<AUWorkflowStateProperty> list;
        workflowDefinition.WorkflowStateProperties.TryGetValue(state, out list);
        if (list != null)
        {
          list = list.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => !result.Any<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (sp => string.Equals(sp.FieldName, it.FieldName, StringComparison.OrdinalIgnoreCase) && string.Equals(sp.ObjectName, it.ObjectName, StringComparison.OrdinalIgnoreCase))))).ToList<AUWorkflowStateProperty>();
          result.AddRange((IEnumerable<AUWorkflowStateProperty>) list);
        }
      }
      else
        break;
    }
    return (IEnumerable<AUWorkflowStateProperty>) result;
  }

  public IEnumerable<AUWorkflowStateProperty> GetStateProperties(
    AUWorkflow workflow,
    string stateID)
  {
    if (workflow == null)
      return (IEnumerable<AUWorkflowStateProperty>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<AUWorkflowStateProperty>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(workflow.ScreenID);
    List<AUWorkflowState> source;
    workflowDefinition.WorkflowStates.TryGetValue(workflow, out source);
    AUWorkflowState key = source != null ? source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateID)) : (AUWorkflowState) null;
    if (key == null)
      return (IEnumerable<AUWorkflowStateProperty>) null;
    List<AUWorkflowStateProperty> stateProperties;
    workflowDefinition.WorkflowStateProperties.TryGetValue(key, out stateProperties);
    return (IEnumerable<AUWorkflowStateProperty>) stateProperties;
  }

  public IEnumerable<string> GetWorkflowStateCacheTypes(PXGraph graph)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (IEnumerable<string>) null : this.GetWorkflowStateCacheTypes(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()));
  }

  public IEnumerable<string> GetWorkflowStateCacheTypes(string screenID)
  {
    if (screenID == null)
      return (IEnumerable<string>) new List<string>();
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<string>) new List<string>();
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    if (workflowDefinition.WorkflowDefinition == null)
      return (IEnumerable<string>) new List<string>();
    List<AUWorkflow> workflows = workflowDefinition.Workflows;
    IEnumerable<AUWorkflowState> allStates = workflowDefinition.WorkflowStates.Where<KeyValuePair<AUWorkflow, List<AUWorkflowState>>>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, bool>) (it => workflows.Contains(it.Key))).SelectMany<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, AUWorkflowState>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) it.Value));
    return (IEnumerable<string>) workflowDefinition.WorkflowStateProperties.Where<KeyValuePair<AUWorkflowState, List<AUWorkflowStateProperty>>>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowStateProperty>>, bool>) (it => allStates.Contains<AUWorkflowState>(it.Key))).SelectMany<KeyValuePair<AUWorkflowState, List<AUWorkflowStateProperty>>, AUWorkflowStateProperty>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowStateProperty>>, IEnumerable<AUWorkflowStateProperty>>) (it => (IEnumerable<AUWorkflowStateProperty>) it.Value)).Distinct<AUWorkflowStateProperty>().Select<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (it => it.ObjectName)).Distinct<string>().ToList<string>();
  }

  public bool InitStateProperties(PXGraph graph)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView))
      return false;
    string stateID;
    AUWorkflow workflowAndState = this.GetCurrentWorkflowAndState(graph, out stateID);
    if (workflowAndState == null)
      return false;
    PXCache cach = graph.Caches[graph.PrimaryItemType];
    return this.InitStateProperties(graph, cach.Current, screenIdFromGraphType, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
  }

  public bool InitStateProperties(PXGraph graph, object row)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView))
      return false;
    string stateID;
    AUWorkflow workflowAndState = this.GetCurrentWorkflowAndState(graph, row, out stateID);
    return workflowAndState != null && this.InitStateProperties(graph, row, screenIdFromGraphType, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
  }

  public bool InitStateProperties(
    PXGraph graph,
    object row,
    string workflowId,
    string workflowSubId,
    string stateID)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    return screenIdFromGraphType != null && !string.IsNullOrEmpty(graph.PrimaryView) && this.InitStateProperties(graph, row, screenIdFromGraphType, workflowId, workflowSubId, stateID);
  }

  public bool InitStateProperties(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateID)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return false;
    PXCache cach = graph.Caches[graph.PrimaryItemType];
    return this.InitStateProperties(graph, cach.Current, screenIdFromGraphType, workflowId, workflowSubId, stateID);
  }

  private bool InitStateProperties(
    PXGraph graph,
    object row,
    string screenId,
    string workflowId,
    string workflowSubId,
    string stateId)
  {
    if (screenId == null)
      return false;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = AUWorkflowEngine.Slot.LocallyCachedSlot.Get(screenId);
    AUWorkflowDefinition wd = workflowDefinition.WorkflowDefinition;
    if (wd == null)
      return false;
    PXCache cach1 = graph.Caches[graph.PrimaryItemType];
    List<AUWorkflow> workflows = workflowDefinition.Workflows;
    string workflowId1 = graph.WorkflowID;
    string[] strArray1;
    if (workflowId1 == null)
      strArray1 = (string[]) null;
    else
      strArray1 = workflowId1.Split('@');
    string[] strArray2 = strArray1;
    string workflowId2 = strArray2 == null || strArray2.Length == 0 ? (string) null : (strArray2[0] == "DEF_WORKFLOW_ID" ? (string) null : strArray2[0]);
    string workflowSubId1 = strArray2 == null || strArray2.Length <= 1 ? (string) null : (strArray2?[1] == "DEF_WORKFLOW_ID" ? (string) null : strArray2[1]);
    if (graph.WorkflowStepID != null)
    {
      AUWorkflow applicableWorkflow = this.FindApplicableWorkflow(workflows, workflowId2, workflowSubId1);
      if (applicableWorkflow != null)
      {
        List<AUWorkflowState> source;
        workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow, out source);
        if ((source != null ? source.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == graph.WorkflowStepID)) : (AUWorkflowState) null) != null)
        {
          IEnumerable<AUWorkflowStateProperty> propertiesRecursive = this.GetStatePropertiesRecursive(applicableWorkflow, graph.WorkflowStepID);
          List<AUWorkflowStateProperty> list = propertiesRecursive != null ? propertiesRecursive.ToList<AUWorkflowStateProperty>() : (List<AUWorkflowStateProperty>) null;
          cach1.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) null;
          if (list != null)
          {
            foreach (string typename in list.Select<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (sp => sp.ObjectName)).Distinct<string>())
            {
              PXCache cach2 = graph.Caches[GraphHelper.GetType(typename)];
              cach2.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) null;
              cach2.WorkflowStateFieldDefaulting = (PXCache.FieldDefaultingDelegate) null;
            }
          }
        }
        else
        {
          foreach (PXCache cach3 in graph.Caches.Caches)
          {
            cach3.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) null;
            cach3.WorkflowStateFieldDefaulting = (PXCache.FieldDefaultingDelegate) null;
          }
        }
      }
      else if (graph.WorkflowStepID == "PrimaryCacheRowInserted")
      {
        foreach (PXCache cach4 in graph.Caches.Caches)
        {
          cach4.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) null;
          cach4.WorkflowStateFieldDefaulting = (PXCache.FieldDefaultingDelegate) null;
        }
      }
    }
    else
      cach1.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) null;
    bool flag1 = !string.IsNullOrEmpty(graph.WorkflowID) && (workflowId2 != workflowId || workflowSubId1 != workflowSubId);
    graph.WorkflowID = $"{workflowId ?? "DEF_WORKFLOW_ID"}@{workflowSubId ?? "DEF_WORKFLOW_ID"}";
    graph.WorkflowStepID = (string) null;
    AUWorkflow applicableWorkflow1 = this.FindApplicableWorkflow(workflows, workflowId, workflowSubId);
    if (applicableWorkflow1 == null)
      return false;
    List<AUWorkflowState> source1;
    workflowDefinition.WorkflowStates.TryGetValue(applicableWorkflow1, out source1);
    AUWorkflowState s = source1 != null ? source1.FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.Identifier == stateId)) : (AUWorkflowState) null;
    if (s == null & flag1 && row != null && cach1.GetStatus(row) == PXEntryStatus.Inserted)
    {
      s = this.GetInitialState(graph, workflowId, workflowSubId);
      if (s != null)
      {
        cach1.SetValueExt(row, wd.StateField, (object) s.Identifier);
        PXWorkflowService.RunAutoActions[graph] = true;
      }
    }
    if (s == null)
      return false;
    graph.WorkflowStepID = stateId;
    if (cach1.WorkflowFieldSelecting != null)
    {
      PXCache.FieldSelectingDelegate selectingDelegate = (PXCache.FieldSelectingDelegate) ((string name, ref object value, object cacheRow) => this.WorkflowFieldSelecting(wd, s, name, ref value, row != null && row == cacheRow, graph));
      cach1.WorkflowFieldSelecting += selectingDelegate;
    }
    else
      cach1.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) ((string name, ref object value, object cacheRow) => this.WorkflowFieldSelecting(wd, s, name, ref value, row != null && row == cacheRow, graph));
    IEnumerable<AUWorkflowStateProperty> propertiesRecursive1 = this.GetStatePropertiesRecursive(applicableWorkflow1, stateId);
    List<AUWorkflowStateProperty> list1 = propertiesRecursive1 != null ? propertiesRecursive1.ToList<AUWorkflowStateProperty>() : (List<AUWorkflowStateProperty>) null;
    if (list1 == null)
      return false;
    foreach (IGrouping<string, AUWorkflowStateProperty> source2 in list1.GroupBy<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (it => it.ObjectName)))
    {
      PXCache cach5 = graph.Caches[GraphHelper.GetType(source2.Key)];
      AUWorkflowStateProperty workflowStateProperty = source2.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => string.Equals(it.FieldName, "<Table>", StringComparison.OrdinalIgnoreCase)));
      List<AUWorkflowStateProperty> list2 = source2.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => !string.Equals(it.FieldName, "<Table>", StringComparison.OrdinalIgnoreCase))).ToList<AUWorkflowStateProperty>();
      if (workflowStateProperty != null)
      {
        bool flag2 = cach5.GetItemType() == graph.PrimaryItemType;
        bool? isDisabled = workflowStateProperty.IsDisabled;
        bool flag3 = true;
        if (isDisabled.GetValueOrDefault() == flag3 & isDisabled.HasValue)
        {
          cach5.AutomationInsertDisabled = !flag2;
          cach5.AutomationDeleteDisabled = true;
          if (!list2.Any<AUWorkflowStateProperty>())
            cach5.AutomationUpdateDisabled = true;
        }
        bool? isHide = workflowStateProperty.IsHide;
        bool flag4 = true;
        if (isHide.GetValueOrDefault() == flag4 & isHide.HasValue)
          cach5.AutomationHidden = !flag2;
      }
      if (list2.Any<AUWorkflowStateProperty>())
      {
        List<AUWorkflowStateProperty> fullPropertiesList = source2.ToList<AUWorkflowStateProperty>();
        this.AlterWorkflowStateProperties(cach5, fullPropertiesList);
        if (cach5.WorkflowFieldSelecting != null)
        {
          PXCache.FieldSelectingDelegate selectingDelegate = (PXCache.FieldSelectingDelegate) ((string field, ref object value, object cacheRow) => this.FieldSelecting(fullPropertiesList, field, ref value, row != null && row == cacheRow));
          cach5.WorkflowFieldSelecting += selectingDelegate;
        }
        else
          cach5.WorkflowFieldSelecting = (PXCache.FieldSelectingDelegate) ((string field, ref object value, object cacheRow) => this.FieldSelecting(fullPropertiesList, field, ref value, row != null && row == cacheRow));
      }
    }
    bool? isInitial = s.IsInitial;
    bool flag5 = true;
    if (isInitial.GetValueOrDefault() == flag5 & isInitial.HasValue)
    {
      foreach (IGrouping<string, AUWorkflowStateProperty> grouping in list1.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => !string.IsNullOrEmpty(it.DefaultValue))).GroupBy<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (it => it.ObjectName)))
      {
        IGrouping<string, AUWorkflowStateProperty> auWorkflowStatePropertyGroup = grouping;
        graph.Caches[GraphHelper.GetType(auWorkflowStatePropertyGroup.Key)].WorkflowStateFieldDefaulting = (PXCache.FieldDefaultingDelegate) ((PXCache sender, string field, ref object value, bool rowSpecific) => this.FieldDefaulting(auWorkflowStatePropertyGroup.ToList<AUWorkflowStateProperty>(), sender, field, ref value));
      }
    }
    isInitial = s.IsInitial;
    bool flag6 = true;
    if (isInitial.GetValueOrDefault() == flag6 & isInitial.HasValue & flag1 && row != null && cach1.GetStatus(row) == PXEntryStatus.Inserted)
    {
      foreach (AUWorkflowStateProperty workflowStateProperty in list1.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => !string.Equals(it.ObjectName, "<Table>", StringComparison.OrdinalIgnoreCase) && !string.Equals(it.ObjectName, "<All Fields>", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(it.DefaultValue))))
      {
        PXCache cach6 = graph.Caches[GraphHelper.GetType(workflowStateProperty.ObjectName)];
        if (cach6.Current != null)
        {
          object current = cach6.Current;
          object oldValue = cach6.GetValue(current, workflowStateProperty.FieldName);
          object newValue;
          if (cach6.RaiseFieldDefaulting(workflowStateProperty.FieldName, current, out newValue))
          {
            cach6.RaiseFieldUpdating(workflowStateProperty.FieldName, current, ref newValue);
            cach6.SetValue(current, workflowStateProperty.FieldName, newValue);
            cach6.RaiseFieldUpdated(workflowStateProperty.FieldName, current, oldValue);
          }
        }
      }
    }
    return true;
  }

  private void AlterWorkflowStateProperties(
    PXCache cache,
    List<AUWorkflowStateProperty> stateProperties)
  {
    foreach (string field in stateProperties.Any<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (property => property.FieldName == "<All Fields>")) ? cache.Fields.ToArray() : stateProperties.Select<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (prop => prop.FieldName)).Where<string>((Func<string, bool>) (property => property != "<Table>")).ToArray<string>())
      cache.SetAltered(field, true);
  }

  private void FieldSelecting(
    List<AUWorkflowStateProperty> propertiesList,
    string field,
    ref object value,
    bool rowSpecific)
  {
    AUWorkflowStateProperty workflowStateProperty1 = propertiesList.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => string.Equals(it.FieldName, "<All Fields>", StringComparison.OrdinalIgnoreCase)));
    AUWorkflowStateProperty workflowStateProperty2 = propertiesList.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => string.Equals(it.FieldName, "<Table>", StringComparison.OrdinalIgnoreCase)));
    AUWorkflowStateProperty workflowStateProperty3 = propertiesList.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => field.Equals(it.FieldName, StringComparison.OrdinalIgnoreCase)));
    if (workflowStateProperty1 == null && workflowStateProperty3 == null && workflowStateProperty2 == null || !(value is PXFieldState pxFieldState1))
      return;
    bool? nullable1 = pxFieldState1.Required;
    bool flag1 = true;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue) && workflowStateProperty3 != null)
    {
      PXFieldState pxFieldState2 = pxFieldState1;
      nullable1 = workflowStateProperty3.IsRequired;
      bool? nullable2 = nullable1 ?? workflowStateProperty3.FlowLevelDefaults?.IsRequired;
      pxFieldState2.Required = nullable2;
    }
    nullable1 = (bool?) workflowStateProperty1?.IsDisabled;
    bool? nullable3 = (bool?) (nullable1 ?? workflowStateProperty2?.IsDisabled);
    if (workflowStateProperty3 != null)
    {
      nullable1 = workflowStateProperty3.IsDisabled;
      nullable3 = (bool?) (nullable1 ?? workflowStateProperty3.FlowLevelDefaults?.IsDisabled);
    }
    nullable1 = (bool?) workflowStateProperty1?.IsHide;
    bool? nullable4 = (bool?) (nullable1 ?? workflowStateProperty2?.IsHide);
    if (workflowStateProperty3 != null)
    {
      nullable1 = workflowStateProperty3.IsHide;
      nullable4 = (bool?) (nullable1 ?? workflowStateProperty3.FlowLevelDefaults?.IsHide);
    }
    if (pxFieldState1.Enabled)
    {
      nullable1 = nullable3;
      bool flag2 = true;
      if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
        pxFieldState1.Enabled = false;
    }
    if (pxFieldState1.Visible)
    {
      nullable1 = nullable4;
      bool flag3 = true;
      if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
        pxFieldState1.Visible = false;
    }
    if (!rowSpecific || string.IsNullOrEmpty(workflowStateProperty3?.ComboBoxValues))
      return;
    PXStringState stringState = pxFieldState1 as PXStringState;
    if (stringState == null || PXContext.GetSlot<bool>("AllowWorkflowExtendedComboBoxValues"))
      return;
    List<string> list1 = ((IEnumerable<string>) workflowStateProperty3.ComboBoxValues.Split(new char[1]
    {
      ';'
    }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
    string str1;
    List<(string, string)> list2 = list1.Select<string, (string, string)>((Func<string, (string, string)>) (it => (it, stringState.ValueLabelDic.TryGetValue(it, out str1) ? str1 : (string) null))).Where<(string, string)>((Func<(string, string), bool>) (it => it.Item2 != null)).ToList<(string, string)>();
    if (pxFieldState1.Value is string key && !string.IsNullOrEmpty(key) && !list1.Contains(key))
    {
      string str2;
      string str3 = stringState.ValueLabelDic.TryGetValue(key, out str2) ? str2 : key;
      list2.Add((key, str3));
      pxFieldState1.ErrorLevel = PXErrorLevel.Warning;
      pxFieldState1.Error = "This value is invalid in the current workflow state.";
    }
    PXStringListAttribute stringListAttribute = new PXStringListAttribute(list2.ToArray());
    stringState.AllowedValues = stringListAttribute.ValueLabelDic.Keys.ToArray<string>();
    stringState.AllowedLabels = stringListAttribute.ValueLabelDic.Values.ToArray<string>();
  }

  private bool FieldDefaulting(
    List<AUWorkflowStateProperty> auWorkflowStateProperties,
    PXCache sender,
    string field,
    ref object value)
  {
    AUWorkflowStateProperty auWorkflowStateProperty = auWorkflowStateProperties.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => field.Equals(it.FieldName, StringComparison.OrdinalIgnoreCase)));
    return auWorkflowStateProperty != null && this.FieldDefaulting(auWorkflowStateProperty, sender, field, ref value);
  }

  private bool FieldDefaulting(
    AUWorkflowStateProperty auWorkflowStateProperty,
    PXCache sender,
    string field,
    ref object value)
  {
    value = !(sender.GetStateExt((object) null, auWorkflowStateProperty.FieldName) is PXDateState) ? (!auWorkflowStateProperty.DefaultValue.Equals("@me", StringComparison.OrdinalIgnoreCase) ? (!auWorkflowStateProperty.DefaultValue.Equals("@branch", StringComparison.OrdinalIgnoreCase) ? (object) auWorkflowStateProperty.DefaultValue : (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch()) : WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(sender, auWorkflowStateProperty.FieldName)) : (!RelativeDatesManager.IsRelativeDatesString(auWorkflowStateProperty.DefaultValue) ? (object) Convert.ToDateTime(auWorkflowStateProperty.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture) : (object) RelativeDatesManager.EvaluateAsDateTime(auWorkflowStateProperty.DefaultValue));
    return !string.IsNullOrEmpty(auWorkflowStateProperty.DefaultValue);
  }

  private void WorkflowFieldSelecting(
    AUWorkflowDefinition wd,
    AUWorkflowState s,
    string name,
    ref object value,
    bool specific,
    PXGraph graph)
  {
    if (name.Equals(wd.StateField, StringComparison.OrdinalIgnoreCase))
    {
      if (!(value is PXFieldState pxFieldState))
        return;
      pxFieldState.Enabled = false;
    }
    bool? nullable;
    if (!string.IsNullOrEmpty(wd.FlowTypeField) && name.Equals(wd.FlowTypeField, StringComparison.OrdinalIgnoreCase))
    {
      nullable = wd.EnableWorkflowIDField;
      bool flag1 = true;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      {
        nullable = s.IsInitial;
        bool flag2 = true;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || graph.Caches[graph.PrimaryItemType].Current == null || graph.Caches[graph.PrimaryItemType].GetStatus(graph.Caches[graph.PrimaryItemType].Current) != PXEntryStatus.Inserted)
        {
          if (!(value is PXFieldState pxFieldState))
            return;
          pxFieldState.Enabled = false;
        }
      }
    }
    if (string.IsNullOrEmpty(wd.FlowSubTypeField) || !name.Equals(wd.FlowSubTypeField, StringComparison.OrdinalIgnoreCase))
      return;
    nullable = wd.EnableWorkflowSubTypeField;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue || graph.Caches[graph.PrimaryItemType].Current != null && graph.Caches[graph.PrimaryItemType].GetStatus(graph.Caches[graph.PrimaryItemType].Current) == PXEntryStatus.Inserted || !(value is PXFieldState pxFieldState1))
      return;
    pxFieldState1.Enabled = false;
  }

  private bool AreValuesEquals(object valueA, object valueB, System.Type type)
  {
    if (this.CanDirectlyCompare(type))
    {
      if (!this.AreValuesEqual(valueA, valueB))
        return false;
    }
    else if (typeof (IEnumerable).IsAssignableFrom(type))
    {
      if (valueA == null && valueB != null || valueA != null && valueB == null)
        return false;
      if (valueA != null && valueB != null)
      {
        IEnumerable<object> source1 = ((IEnumerable) valueA).Cast<object>();
        IEnumerable<object> source2 = ((IEnumerable) valueB).Cast<object>();
        int num1 = source1.Count<object>();
        int num2 = source2.Count<object>();
        if (num1 != num2)
          return false;
        for (int index = 0; index < num1; ++index)
        {
          object obj1 = source1.ElementAt<object>(index);
          object obj2 = source2.ElementAt<object>(index);
          if (obj1 == null || obj2 == null || this.CanDirectlyCompare(obj1.GetType()))
          {
            if (!this.AreValuesEqual(obj1, obj2))
              return false;
          }
          else if (!this.AreObjectsEqual(obj1, obj2))
            return false;
        }
      }
    }
    else if (!type.IsClass || !this.AreObjectsEqual(valueA, valueB))
      return false;
    return true;
  }

  private bool CanDirectlyCompare(System.Type type)
  {
    return typeof (IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
  }

  private bool AreValuesEqual(object valueA, object valueB)
  {
    IComparable comparable = valueA as IComparable;
    if (valueA == null && valueB != null || valueA != null && valueB == null)
      return false;
    if (valueA == null && valueB == null)
      return true;
    if (valueA.GetType() != valueB.GetType())
      return false;
    if (valueA is string str1 && valueB is string str2)
      return string.Equals(str1.Trim(), str2.Trim());
    return (comparable == null || comparable.CompareTo(valueB) == 0) && object.Equals(valueA, valueB);
  }

  private bool AreObjectsEqual(object objectA, object objectB)
  {
    if (objectA == null || objectB == null)
      return object.Equals(objectA, objectB);
    if (objectA.GetType() != objectB.GetType())
      return false;
    foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>) objectA.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.CanRead)))
    {
      if (!this.AreValuesEquals(propertyInfo.GetValue(objectA, (object[]) null), propertyInfo.GetValue(objectB, (object[]) null), propertyInfo.PropertyType))
        return false;
    }
    return true;
  }

  public IEnumerable<AUWorkflowTransitionField> GetTransitionFields(
    AUWorkflowTransition auWorkflowTransition)
  {
    List<AUWorkflowTransitionField> transitionFields;
    AUWorkflowEngine.Slot.LocallyCachedSlot.Get(auWorkflowTransition.ScreenID).WorkflowTransitionFields.TryGetValue(auWorkflowTransition, out transitionFields);
    return (IEnumerable<AUWorkflowTransitionField>) transitionFields;
  }

  public IEnumerable<AUWorkflowOnEnterStateField> GetStateOnEnterFields(
    AUWorkflowState auWorkflowState)
  {
    List<AUWorkflowOnEnterStateField> stateOnEnterFields;
    AUWorkflowEngine.Slot.LocallyCachedSlot.Get(auWorkflowState.ScreenID).WorkflowStateOnEnterFields.TryGetValue(auWorkflowState, out stateOnEnterFields);
    return (IEnumerable<AUWorkflowOnEnterStateField>) stateOnEnterFields;
  }

  public IEnumerable<AUWorkflowOnLeaveStateField> GetStateOnLeaveFields(
    AUWorkflowState auWorkflowState)
  {
    List<AUWorkflowOnLeaveStateField> stateOnLeaveFields;
    AUWorkflowEngine.Slot.LocallyCachedSlot.Get(auWorkflowState.ScreenID).WorkflowStateOnLeaveFields.TryGetValue(auWorkflowState, out stateOnLeaveFields);
    return (IEnumerable<AUWorkflowOnLeaveStateField>) stateOnLeaveFields;
  }

  public string GetWorkflowStatePropertyName(PXGraph graph)
  {
    return string.IsNullOrEmpty(graph.PrimaryView) ? (string) null : this.GetWorkflowStatePropertyName(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()));
  }

  public string GetWorkflowStatePropertyName(string screenID)
  {
    if (screenID == null)
      return (string) null;
    return AUWorkflowEngine.Slot.LocallyCachedSlot?.Get(screenID).WorkflowDefinition?.StateField;
  }

  public IEnumerable<string> GetAllStates(string screenID)
  {
    if (screenID == null)
      return (IEnumerable<string>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<string>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition definition = locallyCachedSlot.Get(screenID);
    return definition.WorkflowDefinition == null ? (IEnumerable<string>) null : definition.Workflows.SelectMany<AUWorkflow, AUWorkflowState>((Func<AUWorkflow, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) definition.WorkflowStates[it])).Select<AUWorkflowState, string>((Func<AUWorkflowState, string>) (it => it.Identifier)).Distinct<string>();
  }

  public IEnumerable<AUWorkflowState> GetStates(AUWorkflow workflow)
  {
    if (workflow == null)
      return (IEnumerable<AUWorkflowState>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<AUWorkflowState>) null;
    List<AUWorkflowState> states;
    locallyCachedSlot.Get(workflow.ScreenID).WorkflowStates.TryGetValue(workflow, out states);
    return (IEnumerable<AUWorkflowState>) states;
  }

  public IEnumerable<AUWorkflow> GetAllFlows(string screenID)
  {
    if (screenID == null)
      return (IEnumerable<AUWorkflow>) null;
    AUWorkflowEngine.Slot locallyCachedSlot = AUWorkflowEngine.Slot.LocallyCachedSlot;
    if (locallyCachedSlot == null)
      return (IEnumerable<AUWorkflow>) null;
    AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition = locallyCachedSlot.Get(screenID);
    return workflowDefinition.WorkflowDefinition == null ? (IEnumerable<AUWorkflow>) null : workflowDefinition.Workflows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (it => it.WorkflowID != null || it.WorkflowSubID != null));
  }

  public void BeforeStartOperation(PXGraph graph)
  {
    if (!PXWorkflowService.SaveAfterAction[graph])
      return;
    PXWorkflowService.SaveAfterAction[graph] = false;
    graph.Persist();
  }

  public void SetSaveAfterActionSlot(PXGraph graph)
  {
    if (PXWorkflowService.PreventSaveOnAction[graph])
      return;
    PXWorkflowService.SaveAfterAction[graph] = true;
  }

  public class ScreenWorkflowDefinition
  {
    public AUWorkflowDefinition WorkflowDefinition;
    public List<AUWorkflow> Workflows = new List<AUWorkflow>();
    public Dictionary<AUWorkflow, List<AUWorkflowState>> WorkflowStates = new Dictionary<AUWorkflow, List<AUWorkflowState>>();
    public Dictionary<AUWorkflowState, List<AUWorkflowStateProperty>> WorkflowStateProperties = new Dictionary<AUWorkflowState, List<AUWorkflowStateProperty>>();
    public Dictionary<AUWorkflowState, List<AUWorkflowTransition>> WorkflowStateTransitions = new Dictionary<AUWorkflowState, List<AUWorkflowTransition>>();
    public Dictionary<AUWorkflowState, List<AUWorkflowOnEnterStateField>> WorkflowStateOnEnterFields = new Dictionary<AUWorkflowState, List<AUWorkflowOnEnterStateField>>();
    public Dictionary<AUWorkflowState, List<AUWorkflowOnLeaveStateField>> WorkflowStateOnLeaveFields = new Dictionary<AUWorkflowState, List<AUWorkflowOnLeaveStateField>>();
    public Dictionary<AUWorkflowTransition, List<AUWorkflowTransitionField>> WorkflowTransitionFields = new Dictionary<AUWorkflowTransition, List<AUWorkflowTransitionField>>();
    public bool IsCustomized;
  }

  private class Slot : IPrefetchable, IPXCompanyDependent
  {
    private readonly ConcurrentDictionary<string, AUWorkflowEngine.ScreenWorkflowDefinition> _screenWorkflowDefinitions = new ConcurrentDictionary<string, AUWorkflowEngine.ScreenWorkflowDefinition>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public static AUWorkflowEngine.Slot GetSlot()
    {
      return PXDatabase.GetSlot<AUWorkflowEngine.Slot>("WorkflowStateProperties", ((IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes()).Union<System.Type>((IEnumerable<System.Type>) new System.Type[9]
      {
        typeof (AUWorkflowDefinition),
        typeof (AUWorkflow),
        typeof (AUWorkflowState),
        typeof (AUWorkflowStateProperty),
        typeof (AUWorkflowTransition),
        typeof (AUWorkflowTransitionField),
        typeof (AUWorkflowOnEnterStateField),
        typeof (AUWorkflowOnLeaveStateField),
        typeof (PXGraph.FeaturesSet)
      }).ToArray<System.Type>());
    }

    public AUWorkflowEngine.ScreenWorkflowDefinition Get(string screenID)
    {
      return screenID != null ? this._screenWorkflowDefinitions.GetOrAdd(screenID, new Func<string, AUWorkflowEngine.ScreenWorkflowDefinition>(this.LoadForScreen)) : throw new ArgumentNullException(nameof (screenID));
    }

    public static AUWorkflowEngine.Slot LocallyCachedSlot
    {
      get
      {
        return PXContext.GetSlot<AUWorkflowEngine.Slot>("WorkflowStateProperties") ?? PXContext.SetSlot<AUWorkflowEngine.Slot>("WorkflowStateProperties", AUWorkflowEngine.Slot.GetSlot());
      }
    }

    public void Prefetch()
    {
    }

    private AUWorkflowEngine.ScreenWorkflowDefinition LoadForScreen(string screenID)
    {
      bool isCustomized = false;
      AUWorkflowDefinition workflowDefinition1 = AUWorkflowEngine.SelectTable<AUWorkflowDefinition>(screenID, ref isCustomized).FirstOrDefault<AUWorkflowDefinition>();
      List<AUWorkflow> list = AUWorkflowEngine.SelectTable<AUWorkflow>(screenID, ref isCustomized).ToList<AUWorkflow>();
      AUWorkflowState[] array1 = AUWorkflowEngine.SelectTable<AUWorkflowState>(screenID, ref isCustomized).ToArray<AUWorkflowState>();
      AUWorkflowStateProperty[] array2 = AUWorkflowEngine.SelectTable<AUWorkflowStateProperty>(screenID, ref isCustomized).ToArray<AUWorkflowStateProperty>();
      AUWorkflowTransition[] array3 = AUWorkflowEngine.SelectTable<AUWorkflowTransition>(screenID, ref isCustomized).ToArray<AUWorkflowTransition>();
      AUWorkflowOnEnterStateField[] array4 = AUWorkflowEngine.SelectTable<AUWorkflowOnEnterStateField>(screenID, ref isCustomized).ToArray<AUWorkflowOnEnterStateField>();
      AUWorkflowOnLeaveStateField[] array5 = AUWorkflowEngine.SelectTable<AUWorkflowOnLeaveStateField>(screenID, ref isCustomized).ToArray<AUWorkflowOnLeaveStateField>();
      AUWorkflowTransitionField[] array6 = AUWorkflowEngine.SelectTable<AUWorkflowTransitionField>(screenID, ref isCustomized).ToArray<AUWorkflowTransitionField>();
      AUWorkflowEngine.ScreenWorkflowDefinition workflowDefinition2 = new AUWorkflowEngine.ScreenWorkflowDefinition()
      {
        WorkflowDefinition = workflowDefinition1,
        Workflows = list,
        WorkflowStates = list.Join((IEnumerable<AUWorkflowState>) array1, w => new
        {
          ScreenID = w.ScreenID,
          WorkflowGUID = w.WorkflowGUID
        }, s => new
        {
          ScreenID = s.ScreenID,
          WorkflowGUID = s.WorkflowGUID
        }, (w, s) => new{ Workflow = w, State = s }).Where(it =>
        {
          bool? isSystem1 = it.Workflow.IsSystem;
          bool flag1 = true;
          if (isSystem1.GetValueOrDefault() == flag1 & isSystem1.HasValue)
            return true;
          bool? isSystem2 = it.State.IsSystem;
          bool flag2 = false;
          return isSystem2.GetValueOrDefault() == flag2 & isSystem2.HasValue;
        }).GroupBy(it => it.Workflow).ToDictionary<IGrouping<AUWorkflow, \u003C\u003Ef__AnonymousType73<AUWorkflow, AUWorkflowState>>, AUWorkflow, List<AUWorkflowState>>(it => it.Key, it => it.Select(_ => _.State).ToList<AUWorkflowState>())
      };
      workflowDefinition2.WorkflowStateProperties = workflowDefinition2.WorkflowStates.SelectMany<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, AUWorkflowState>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) it.Value)).Join((IEnumerable<AUWorkflowStateProperty>) array2, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID,
        StateName = s.Identifier
      }, sp => new
      {
        ScreenID = sp.ScreenID,
        WorkflowGUID = sp.WorkflowGUID,
        StateName = sp.StateName
      }, (s, sp) => new{ State = s, StateProperty = sp }).Where(it =>
      {
        bool? isSystem3 = it.State.IsSystem;
        bool flag3 = true;
        if (isSystem3.GetValueOrDefault() == flag3 & isSystem3.HasValue)
          return true;
        bool? isSystem4 = it.StateProperty.IsSystem;
        bool flag4 = false;
        return isSystem4.GetValueOrDefault() == flag4 & isSystem4.HasValue;
      }).GroupBy(it => it.State).ToDictionary<IGrouping<AUWorkflowState, \u003C\u003Ef__AnonymousType75<AUWorkflowState, AUWorkflowStateProperty>>, AUWorkflowState, List<AUWorkflowStateProperty>>(it => it.Key, it => it.Select(_ => _.StateProperty).ToList<AUWorkflowStateProperty>());
      workflowDefinition2.WorkflowStateTransitions = workflowDefinition2.WorkflowStates.SelectMany<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, AUWorkflowState>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) it.Value)).Join((IEnumerable<AUWorkflowTransition>) array3, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID,
        StateName = s.Identifier
      }, sp => new
      {
        ScreenID = sp.ScreenID,
        WorkflowGUID = sp.WorkflowGUID,
        StateName = sp.FromStateName
      }, (s, sp) => new{ State = s, Transition = sp }).Where(it =>
      {
        bool? isSystem5 = it.State.IsSystem;
        bool flag5 = true;
        if (isSystem5.GetValueOrDefault() == flag5 & isSystem5.HasValue)
          return true;
        bool? isSystem6 = it.Transition.IsSystem;
        bool flag6 = false;
        return isSystem6.GetValueOrDefault() == flag6 & isSystem6.HasValue;
      }).GroupBy(it => it.State).ToDictionary<IGrouping<AUWorkflowState, \u003C\u003Ef__AnonymousType76<AUWorkflowState, AUWorkflowTransition>>, AUWorkflowState, List<AUWorkflowTransition>>(it => it.Key, it => it.Select(_ => _.Transition).OrderBy<AUWorkflowTransition, int>((Func<AUWorkflowTransition, int>) (_ => !_.ConditionID.HasValue ? 1 : 0)).ThenBy<AUWorkflowTransition, int?>((Func<AUWorkflowTransition, int?>) (_ => _.TransitionLineNbr)).ToList<AUWorkflowTransition>());
      workflowDefinition2.WorkflowStateOnEnterFields = workflowDefinition2.WorkflowStates.SelectMany<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, AUWorkflowState>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) it.Value)).Join((IEnumerable<AUWorkflowOnEnterStateField>) array4, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID,
        StateName = s.Identifier
      }, f => new
      {
        ScreenID = f.ScreenID,
        WorkflowGUID = f.WorkflowGUID,
        StateName = f.StateName
      }, (s, f) => new{ State = s, Field = f }).Where(it =>
      {
        bool? isSystem7 = it.State.IsSystem;
        bool flag7 = true;
        if (isSystem7.GetValueOrDefault() == flag7 & isSystem7.HasValue)
          return true;
        bool? isSystem8 = it.Field.IsSystem;
        bool flag8 = false;
        return isSystem8.GetValueOrDefault() == flag8 & isSystem8.HasValue;
      }).GroupBy(it => it.State).ToDictionary<IGrouping<AUWorkflowState, \u003C\u003Ef__AnonymousType77<AUWorkflowState, AUWorkflowOnEnterStateField>>, AUWorkflowState, List<AUWorkflowOnEnterStateField>>(it => it.Key, it => it.Select(j => j.Field).OrderBy<AUWorkflowOnEnterStateField, int?>((Func<AUWorkflowOnEnterStateField, int?>) (j => j.OnEnterStateFieldLineNbr)).ToList<AUWorkflowOnEnterStateField>());
      workflowDefinition2.WorkflowStateOnLeaveFields = workflowDefinition2.WorkflowStates.SelectMany<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, AUWorkflowState>((Func<KeyValuePair<AUWorkflow, List<AUWorkflowState>>, IEnumerable<AUWorkflowState>>) (it => (IEnumerable<AUWorkflowState>) it.Value)).Join((IEnumerable<AUWorkflowOnLeaveStateField>) array5, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID,
        StateName = s.Identifier
      }, f => new
      {
        ScreenID = f.ScreenID,
        WorkflowGUID = f.WorkflowGUID,
        StateName = f.StateName
      }, (s, f) => new{ State = s, Field = f }).Where(it =>
      {
        bool? isSystem9 = it.State.IsSystem;
        bool flag9 = true;
        if (isSystem9.GetValueOrDefault() == flag9 & isSystem9.HasValue)
          return true;
        bool? isSystem10 = it.Field.IsSystem;
        bool flag10 = false;
        return isSystem10.GetValueOrDefault() == flag10 & isSystem10.HasValue;
      }).GroupBy(it => it.State).ToDictionary<IGrouping<AUWorkflowState, \u003C\u003Ef__AnonymousType77<AUWorkflowState, AUWorkflowOnLeaveStateField>>, AUWorkflowState, List<AUWorkflowOnLeaveStateField>>(it => it.Key, it => it.Select(j => j.Field).OrderBy<AUWorkflowOnLeaveStateField, int?>((Func<AUWorkflowOnLeaveStateField, int?>) (j => j.OnLeaveStateFieldLineNbr)).ToList<AUWorkflowOnLeaveStateField>());
      workflowDefinition2.WorkflowTransitionFields = workflowDefinition2.WorkflowStateTransitions.SelectMany<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, AUWorkflowTransition>((Func<KeyValuePair<AUWorkflowState, List<AUWorkflowTransition>>, IEnumerable<AUWorkflowTransition>>) (it => (IEnumerable<AUWorkflowTransition>) it.Value)).Join((IEnumerable<AUWorkflowTransitionField>) array6, s => new
      {
        ScreenID = s.ScreenID,
        TransitionID = s.TransitionID,
        WorkflowGUID = s.WorkflowGUID
      }, sp => new
      {
        ScreenID = sp.ScreenID,
        TransitionID = sp.TransitionID,
        WorkflowGUID = sp.WorkflowGUID
      }, (s, sp) => new
      {
        Transition = s,
        TransitionField = sp
      }).Where(it =>
      {
        bool? isSystem11 = it.Transition.IsSystem;
        bool flag11 = true;
        if (isSystem11.GetValueOrDefault() == flag11 & isSystem11.HasValue)
          return true;
        bool? isSystem12 = it.TransitionField.IsSystem;
        bool flag12 = false;
        return isSystem12.GetValueOrDefault() == flag12 & isSystem12.HasValue;
      }).GroupBy(it => it.Transition).ToDictionary<IGrouping<AUWorkflowTransition, \u003C\u003Ef__AnonymousType79<AUWorkflowTransition, AUWorkflowTransitionField>>, AUWorkflowTransition, List<AUWorkflowTransitionField>>(it => it.Key, it => it.Select(_ => _.TransitionField).OrderBy<AUWorkflowTransitionField, int?>((Func<AUWorkflowTransitionField, int?>) (_ => _.TransitionFieldLineNbr)).ToList<AUWorkflowTransitionField>());
      workflowDefinition2.IsCustomized = isCustomized;
      return workflowDefinition2;
    }
  }
}
