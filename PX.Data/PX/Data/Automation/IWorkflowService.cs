// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.IWorkflowService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation;

[PXInternalUseOnly]
public interface IWorkflowService
{
  /// <summary>Returns workflow state and flowId fields for graph</summary>
  (string stateField, string flowField, string subFlowField) GetWorkflowDefinitionFields(
    PXGraph graph);

  /// <summary>
  /// Returns list of all possible transitions from current state and in current flow
  /// </summary>
  IEnumerable<TransitionInfo> GetPossibleTransition(PXGraph graph);

  /// <summary>Return current workflow action by action name</summary>
  ExposedActionInfo GetActionState(PXGraph graph, string actionName);

  /// <summary>
  /// Return list of all actions in graph, that have linked dynamic form
  /// or that are exposed to mobile via workflow
  /// </summary>
  IEnumerable<ExposedActionInfo> GetAllExposedWorkflowActions(PXGraph graph);

  /// <summary>
  /// Fill dynamic form with corresponded values. For form fields, that are not presented in <paramref name="values" /> collection
  /// default value will be used
  /// </summary>
  /// <param name="formName">Form name</param>
  /// <param name="values">Fields with values, where value can be in internal and external format</param>
  void FillFormValues(PXGraph graph, string formName, Dictionary<string, object> values);

  /// <summary>
  /// Fill dynamic form with corresponded values. For form fields, that are not presented in <paramref name="values" /> collection
  /// default value will be used
  /// </summary>
  /// <param name="transition">Transition, that use action with dynamic form</param>
  /// <param name="values">Fields with values, where value can be in internal and external format</param>
  void FillFormValues(PXGraph graph, TransitionInfo transition, Dictionary<string, object> values);

  /// <summary>
  /// Fill dynamic form with corresponded values. For form fields, that are not presented in <paramref name="values" /> collection
  /// default value will be used
  /// </summary>
  /// <param name="actionName">Action with dynamic form</param>
  /// <param name="values">Fields with values, where value can be in internal and external format</param>
  void FillFormValuesByAction(PXGraph graph, string actionName, Dictionary<string, object> values);

  /// <summary>Returns currently stored dynamic form values</summary>
  FormInfo GetCurrentFormValues(PXGraph graph);

  /// <summary>
  /// Return pairs of system value/desciption for selected combo-box field in selected workflow state
  /// </summary>
  /// <param name="graph">Graph</param>
  /// <param name="dacType">Type of DAC, that contains field</param>
  /// <param name="fieldName">Field name</param>
  /// <param name="state">Workflow state name (system or description)</param>
  Dictionary<string, string> GetPossibleComboBoxValuesForState(
    PXGraph graph,
    System.Type dacType,
    string fieldName,
    string state);

  /// <summary>
  /// Return list of dynamically required fields for selected workflow state
  /// </summary>
  IEnumerable<(string objectName, string fieldName)> GetRequiredFieldsForState(
    PXGraph graph,
    string state);

  /// <summary>
  /// Return pairs of system value/desciption for selected combo-box field in selected workflow state
  /// </summary>
  /// <param name="graph">Graph</param>
  /// <param name="status">Workflow state name (system or description)</param>
  Dictionary<string, string> GetPossibleComboBoxValuesForState<TField>(PXGraph graph, string status) where TField : IBqlField;

  /// <summary>
  /// Returns list of all possible transitions from current state and in current flow, that targets to <paramref name="targetState" /> state
  /// </summary>
  IEnumerable<TransitionInfo> GetPossibleTransition(PXGraph graph, string targetState);

  /// <summary>
  /// Checks, if workflow for current graph is in <paramref name="state" />
  /// </summary>
  bool IsInSpecifiedStatus(PXGraph graph, string state);

  /// <summary>
  /// Returns merged list of all form field state for selected screen
  /// </summary>
  PXFieldState[] GetScreenFields(string screenID, PXGraph schemaGraph, string formName = null);

  /// <summary>
  /// Returns merged list of the form fields for selected screen
  /// </summary>
  AUWorkflowFormField[] GetWorkflowFormFields(string screenID, string formName);

  /// <summary>
  /// Returns merged list of the form field state for selected screen
  /// </summary>
  PXFieldState[] GetFormFields(string screenID, PXGraph schemaGraph, string formName);

  void ClearFormData(PXGraph graph);

  /// <summary>
  /// Returns Navigation and Workflow Actions, added in Workflow Configuration.
  /// </summary>
  /// <param name="screenId"></param>
  /// <returns></returns>
  IEnumerable<string> GetWorkflowAddedActions(string screenId);

  string GetWorkflowStatePropertyName(string screenId);

  bool ApplyComboBoxValues(
    PXGraph graph,
    string tableName,
    string fieldName,
    ref string[] allowedValues,
    ref string[] allowedLabels);

  void AskExt(PXGraph graph, FormDefinition definition);

  bool IsWorkflowDefinitionDefined(PXGraph graph);

  bool IsInAutoAction(PXGraph graph);
}
