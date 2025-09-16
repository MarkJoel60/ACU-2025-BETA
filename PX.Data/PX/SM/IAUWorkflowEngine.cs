// Decompiled with JetBrains decompiler
// Type: PX.SM.IAUWorkflowEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

internal interface IAUWorkflowEngine
{
  bool IsWorkflowCustomized(string screenId);

  AUWorkflowDefinition GetScreenWorkflows(PXGraph graph);

  AUWorkflowDefinition GetScreenWorkflows(string screenID);

  AUWorkflow GetCurrentWorkflowAndState(PXGraph graph, out string stateID);

  AUWorkflow GetCurrentWorkflowAndState(PXGraph graph, object row, out string stateID);

  AUWorkflow GetCurrentWorkflowAndState(PXCache cache, object row, out string stateID);

  AUWorkflowState GetInitialState(PXGraph graph, string workflowId, string workflowSubId);

  AUWorkflowState GetInitialState(string screenID, string workflowId, string workflowSubId);

  AUWorkflowState GetState(PXGraph graph, string workflowId, string workflowSubId, string stateId);

  AUWorkflowState GetState(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId);

  IEnumerable<AUWorkflowState> GetStates(AUWorkflow workflow);

  IEnumerable<AUWorkflowTransition> GetTransitions(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName);

  IEnumerable<AUWorkflowTransition> GetTransitions(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName);

  IEnumerable<AUWorkflowTransition> GetTransitions(
    string screenID,
    string workflowId,
    string workflowSubId,
    string stateId);

  IEnumerable<AUWorkflowStateProperty> GetStateProperties(PXGraph graph);

  IEnumerable<AUWorkflowStateProperty> GetStateProperties(AUWorkflow workflow, string stateID);

  IEnumerable<string> GetWorkflowStateCacheTypes(PXGraph graph);

  IEnumerable<string> GetWorkflowStateCacheTypes(string screenID);

  bool InitStateProperties(PXGraph graph);

  bool InitStateProperties(PXGraph graph, object row);

  bool InitStateProperties(PXGraph graph, string workflowId, string workflowSubId, string stateID);

  IEnumerable<AUWorkflowTransitionField> GetTransitionFields(
    AUWorkflowTransition auWorkflowTransition);

  IEnumerable<AUWorkflowOnEnterStateField> GetStateOnEnterFields(AUWorkflowState auWorkflowState);

  IEnumerable<AUWorkflowOnLeaveStateField> GetStateOnLeaveFields(AUWorkflowState auWorkflowState);

  string GetWorkflowStatePropertyName(PXGraph graph);

  string GetWorkflowStatePropertyName(string screenID);

  IEnumerable<string> GetAllStates(string screenID);

  IEnumerable<AUWorkflow> GetAllFlows(string screenID);

  void BeforeStartOperation(PXGraph graph);

  AUWorkflow GetCurrentWorkflowAndState(
    string screenID,
    PXCache cache,
    object row,
    out string stateID);

  void SetSaveAfterActionSlot(PXGraph graph);

  List<AUWorkflowTransition> GetTransitions(AUWorkflow workflow);

  IEnumerable<(AUWorkflowTransition transition, AUWorkflow workflow)> GetTransitions(
    string actionValue,
    string sourceStateName);

  bool InitStateProperties(
    PXGraph graph,
    object row,
    string workflowId,
    string workflowSubId,
    string stateID);

  AUWorkflowState GetNextStateForSequence(
    AUWorkflow workflow,
    AUWorkflowState currentState,
    AUWorkflowState sequence);

  IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    PXGraph graph,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName);

  IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    string screenId,
    string workflowId,
    string workflowSubId,
    string stateId,
    string actionName);

  IEnumerable<AUWorkflowStateProperty> GetStatePropertiesRecursive(
    AUWorkflow workflow,
    string stateId);

  IEnumerable<AUWorkflowStateProperty> GetStatePropertiesRecursive(PXGraph graph);

  IEnumerable<AUWorkflowTransition> GetTransitionsRecursive(
    string screenId,
    string workflowId,
    string workflowSubId,
    string stateId);
}
