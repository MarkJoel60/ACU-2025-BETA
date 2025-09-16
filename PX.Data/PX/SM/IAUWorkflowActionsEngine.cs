// Decompiled with JetBrains decompiler
// Type: PX.SM.IAUWorkflowActionsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async.Internal;
using PX.Common;
using PX.Data;
using PX.Data.Automation.State;
using PX.Data.ProjectDefinition.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

internal interface IAUWorkflowActionsEngine
{
  void InitActions(PXGraph graph, object row, Screen screen);

  void InitActionsWithoutWorkflow(PXGraph graph, object row, Screen screen);

  IEnumerable<string> GetEnabledActionsForState(string screen, string state);

  IEnumerable<(string workflowId, string workflowSubId, string stateName)> GetStatesWithAction(
    string screen,
    string actionName);

  IEnumerable<AUWorkflowStateAction> GetActionStates(
    string screen,
    string workflowId,
    string workflowSubId,
    string state);

  IEnumerable<AUWorkflowStateAction> GetAutoActions(
    string screen,
    string workflowId,
    string workflowSubId,
    string state);

  IEnumerable<AUWorkflowActionUpdateField> GetActionFields(AUScreenActionBaseState action);

  IEnumerable<AUWorkflowActionParam> GetActionParams(AUScreenActionBaseState action);

  IEnumerable<AUWorkflowActionSequence> GetNextActionSequences(AUScreenActionBaseState action);

  IEnumerable<AUWorkflowActionSequenceFormFieldValue> GetActionSequenceFormFields(
    AUWorkflowActionSequence actionSequence);

  bool HasActionsWithFormOrFieldUpdates(PXGraph graph);

  IReadOnlyDictionary<string, AUScreenActionBaseState> GetMassProcessingActions(PXGraph graph);

  IReadOnlyDictionary<string, AUScreenActionBaseState> GetMassProcessingActions(string screenId);

  SlotIndexer<object, bool> SuppressCompletions { get; }

  void ClearActionData();

  void ClearLongRunActionData();

  IEnumerable<AUScreenActionBaseState> GetActionDefinitions(string screenID);

  IEnumerable<AUWorkflowCategory> GetOrderedActionCategories(string screenID);

  string CurrentWorkflowAction { get; set; }

  string LongRunWorkflowAction { get; set; }

  IDictionary CurrentWorkflowObjectKeys { get; set; }

  IDictionary LongRunWorkflowObjectKeys { get; set; }

  IDictionary<string, object> CurrentFormValues { get; set; }

  IDictionary<string, object> LongRunFormValues { get; set; }

  IDictionary<string, object> CurrentActionViewsValues { get; set; }

  IDictionary<string, object> LongRunActionViewsValues { get; set; }

  System.Type CurrentWorkflowObjectType { get; set; }

  System.Type LongRunWorkflowObjectType { get; set; }

  IDictionary<IDictionary, bool> MassProcessingWorkflowObjectKeys { get; set; }

  string CurrentWorkflowScreenId { get; set; }

  string LongRunWorkflowScreenId { get; set; }

  bool CurrentWorkflowShouldBeExecuted { get; set; }

  bool LongRunWorkflowShouldBeExecuted { get; set; }

  void StartOperation(PXLongOperationPars longOperationPars);

  void RestoreWorkflowParameters(PXLongOperationPars longOperationPars);

  IEnumerable<KeyValuePair<string, object>> GetParameters(
    PXGraph graph,
    string actionName,
    object row);

  bool ApplyFieldUpdates(
    IEnumerable<IWorkflowUpdateField> fields,
    PXCache cache,
    object currentRecord,
    IReadOnlyDictionary<string, object> formValues);

  void InitActions(
    PXGraph graph,
    object row,
    Screen screen,
    string screenID,
    AUWorkflow workflow,
    string stateID,
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions);

  IEnumerable<AUWorkflowStateAction> GetAutoActionsRecursive(
    string screen,
    string workflowId,
    string workflowSubId,
    string state);

  IEnumerable<AUWorkflowStateAction> GetActionStatesRecursive(
    string screen,
    string workflowId,
    string workflowSubId,
    string state);

  string GetCategoryDisplayName(PXGraph graph, string categoryName);

  void RestoreActionData();

  void RestoreLongRunActionData();

  bool HasAnyActionSequences(string screenId, string actionName);

  void RunActionSequences(
    PXGraph graph,
    string actionName,
    Screen screen,
    string screenId,
    bool massProcess,
    ChainEventArgs<Exception> exceptionArgs = null);

  IEnumerable<string> GetActionSequencesActionNames(PXGraph graph);

  bool IsActionsCustomized(string screenId);
}
