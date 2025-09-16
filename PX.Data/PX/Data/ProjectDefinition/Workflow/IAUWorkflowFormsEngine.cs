// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.IAUWorkflowFormsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation.State;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal interface IAUWorkflowFormsEngine
{
  void PrepareFormData(PXGraph graph, string formName, bool clearFormData, Screen screen);

  void PrepareFormDataForMassProcessing(PXGraph graph, string formName, string screenID);

  /// <remarks>Use this method only to show test form</remarks>
  WebDialogResult AskExt(PXGraph graph, FormDefinition formDefinition);

  bool AskExt(
    PXGraph graph,
    object row,
    string formName,
    string screenID,
    Screen screen,
    string actionName,
    bool repaintControls = false);

  bool AskMassUpdateExt(
    PXGraph graph,
    string formName,
    string screenID,
    Screen screen,
    string actionName);

  bool HasError(PXCache cache, string fieldName);

  bool HasChanges(PXGraph graph);

  FormDefinition OnInit(PXGraph graph, bool useCurrentData = false);

  object GetFieldDefaultValue(
    PXGraph schemaGraph,
    FormDefinitionRecord definition,
    AUWorkflowFormField field);

  IReadOnlyDictionary<string, object> GetFormValues(PXGraph graph);

  IReadOnlyDictionary<string, object> GetFormValues(PXGraph graph, string form);

  IReadOnlyDictionary<string, object> GetFormValues(PXGraph graph, string screen, string form);

  IReadOnlyDictionary<string, object> GetFormValuesForMassProcessing(
    PXGraph graph,
    object row,
    string screenId = null);

  void InitFormView(PXGraph graph);

  void ClearFormData(PXGraph graph);

  void SetFormValues(
    PXGraph graph,
    string form,
    IDictionary<string, object> values,
    bool useMulti = false);

  IEnumerable<AUWorkflowFormField> GetScreenFields(string screen);

  FormDefinition GetFormDefinition(string screen, string formName);

  IReadOnlyDictionary<string, object> GetOnlyProvidedFormValues(
    PXGraph graph,
    string screen,
    string form);

  Dictionary<string, string> GetErrors();

  bool IsFormCustomized(string screenId);
}
