// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.WorkflowService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

#nullable disable
namespace PX.Data.Automation;

internal class WorkflowService : IWorkflowService
{
  private readonly PXWorkflowService _pxWorkflowService;
  private readonly IAUWorkflowEngine _auWorkflowEngine;
  private readonly IAUWorkflowFormsEngine _auWorkflowFormsEngine;
  private readonly IAUWorkflowActionsEngine _workflowActionsEngine;
  private readonly IWorkflowConditionEvaluateService _wfConditionEvaluateService;
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;

  public WorkflowService(
    PXWorkflowService pxWorkflowService,
    IAUWorkflowEngine auWorkflowEngine,
    IAUWorkflowFormsEngine auWorkflowFormsEngine,
    IAUWorkflowActionsEngine workflowActionsEngine,
    IWorkflowConditionEvaluateService wfConditionEvaluateService,
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService)
  {
    this._pxWorkflowService = pxWorkflowService;
    this._auWorkflowEngine = auWorkflowEngine;
    this._auWorkflowFormsEngine = auWorkflowFormsEngine;
    this._workflowActionsEngine = workflowActionsEngine;
    this._wfConditionEvaluateService = wfConditionEvaluateService;
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
  }

  public (string stateField, string flowField, string subFlowField) GetWorkflowDefinitionFields(
    PXGraph graph)
  {
    AUWorkflowDefinition screenWorkflows = this._auWorkflowEngine.GetScreenWorkflows(graph);
    return screenWorkflows == null ? ((string) null, (string) null, (string) null) : (screenWorkflows.StateField, screenWorkflows.FlowTypeField, screenWorkflows.FlowSubTypeField);
  }

  public IEnumerable<TransitionInfo> GetPossibleTransition(PXGraph graph)
  {
    if (graph == null)
      return (IEnumerable<TransitionInfo>) Array.Empty<TransitionInfo>();
    string screenID = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (string.IsNullOrEmpty(screenID))
      return (IEnumerable<TransitionInfo>) null;
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (IEnumerable<TransitionInfo>) null;
    string stateID;
    AUWorkflow flow = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID);
    if (flow == null)
      return (IEnumerable<TransitionInfo>) Array.Empty<TransitionInfo>();
    IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(screenID, flow.WorkflowID, flow.WorkflowSubID, stateID);
    return transitionsRecursive == null ? (IEnumerable<TransitionInfo>) Array.Empty<TransitionInfo>() : transitionsRecursive.Select<AUWorkflowTransition, TransitionInfo>((Func<AUWorkflowTransition, TransitionInfo>) (it =>
    {
      TransitionInfo possibleTransition = new TransitionInfo()
      {
        Name = it.DisplayName,
        ActionName = it.ActionName,
        Condition = it.ConditionID,
        FromState = it.FromStateName,
        TargetState = it.TargetStateName
      };
      IEnumerable<AUWorkflowStateAction> actionStatesRecursive = this._workflowActionsEngine.GetActionStatesRecursive(screenID, flow.WorkflowID, flow.WorkflowSubID, stateID);
      if ((actionStatesRecursive != null ? actionStatesRecursive.FirstOrDefault<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (state => it.ActionName.Equals(state.ActionName, StringComparison.OrdinalIgnoreCase))) : (AUWorkflowStateAction) null) == null)
        return possibleTransition;
      IEnumerable<AUScreenActionBaseState> actionDefinitions = this._workflowActionsEngine.GetActionDefinitions(screenID);
      AUScreenActionBaseState action = actionDefinitions != null ? actionDefinitions.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (state => it.ActionName.Equals(state.ActionName, StringComparison.OrdinalIgnoreCase))) : (AUScreenActionBaseState) null;
      FormDefinition form = !string.IsNullOrEmpty(action?.Form) ? this._auWorkflowFormsEngine.GetFormDefinition(screenID, action.Form) : (FormDefinition) null;
      if (form == null)
        return possibleTransition;
      possibleTransition.FormName = form.Form.FormName;
      possibleTransition.FormCaption = form.Form.DisplayName;
      IEnumerable<AUWorkflowActionUpdateField> source1 = this._workflowActionsEngine.GetActionFields(action) ?? (IEnumerable<AUWorkflowActionUpdateField>) new List<AUWorkflowActionUpdateField>();
      IEnumerable<AUWorkflowTransitionField> source2 = this._auWorkflowEngine.GetTransitionFields(it) ?? (IEnumerable<AUWorkflowTransitionField>) new List<AUWorkflowTransitionField>();
      foreach (AUWorkflowFormField field in form.Fields)
      {
        AUWorkflowFormField auWorkflowFormField = field;
        Dictionary<string, string> source3 = (Dictionary<string, string>) null;
        SchemaFieldEditors? schemaFieldEditor = auWorkflowFormField.SchemaFieldEditor;
        if (schemaFieldEditor.HasValue)
        {
          switch (schemaFieldEditor.GetValueOrDefault())
          {
            case SchemaFieldEditors.ComboBox:
              source3 = (!string.IsNullOrEmpty(auWorkflowFormField.ComboBoxValues) ? new PXStringListAttribute(auWorkflowFormField.ComboBoxValues) : new PXStringListAttribute()).ValueLabelDic;
              goto label_11;
            case SchemaFieldEditors.CheckBox:
            case SchemaFieldEditors.RichTextEdit:
              goto label_11;
          }
        }
        System.Type dacType;
        string name;
        if (FormFieldHelper.TryGetFieldFromFormFieldName(graph, auWorkflowFormField.SchemaField, out dacType, out name) && graph.Caches[dacType].GetStateExt((object) null, name) is PXStringState stateExt2)
        {
          source3 = stateExt2.ValueLabelDic;
          if (!string.IsNullOrEmpty(auWorkflowFormField.ComboBoxValues))
          {
            string[] filteredValues = auWorkflowFormField.ComboBoxValues.Split(';');
            source3 = source3.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (str => ((IEnumerable<string>) filteredValues).Contains<string>(str.Key))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (str => str.Key), (Func<KeyValuePair<string, string>, string>) (str => str.Value));
          }
        }
label_11:
        possibleTransition.Fields.Add(auWorkflowFormField.FieldName, new FormFieldInfo()
        {
          IsRequired = this.GetRequiedSimple(auWorkflowFormField),
          PossibleComboBoxValues = source3,
          Assignments = source1.Where<AUWorkflowActionUpdateField>((Func<AUWorkflowActionUpdateField, bool>) (af => af.Value.Contains($"[{form.Form.FormName}.{auWorkflowFormField.FieldName}]"))).Select<AUWorkflowActionUpdateField, AssignmentInfo>((Func<AUWorkflowActionUpdateField, AssignmentInfo>) (af => new AssignmentInfo()
          {
            Expression = af.Value,
            InTransition = false,
            FieldName = af.FieldName
          })).Union<AssignmentInfo>(source2.Where<AUWorkflowTransitionField>((Func<AUWorkflowTransitionField, bool>) (tf => tf.Value.Contains($"[{form.Form.FormName}.{auWorkflowFormField.FieldName}]"))).Select<AUWorkflowTransitionField, AssignmentInfo>((Func<AUWorkflowTransitionField, AssignmentInfo>) (tf => new AssignmentInfo()
          {
            Expression = tf.Value,
            InTransition = true,
            FieldName = tf.FieldName
          }))).ToList<AssignmentInfo>()
        });
      }
      return possibleTransition;
    }));
  }

  public ExposedActionInfo GetActionState(PXGraph graph, string actionName)
  {
    return this.GetAllWorkflowActions(graph, false, false).FirstOrDefault<ExposedActionInfo>((Func<ExposedActionInfo, bool>) (_ => _.ActionName.OrdinalEquals(actionName)));
  }

  public IEnumerable<ExposedActionInfo> GetAllExposedWorkflowActions(PXGraph graph)
  {
    return this.GetAllWorkflowActions(graph, true, true);
  }

  private IEnumerable<ExposedActionInfo> GetAllWorkflowActions(
    PXGraph graph,
    bool onlyExposed,
    bool collectFields)
  {
    if (graph == null)
      return (IEnumerable<ExposedActionInfo>) Array.Empty<ExposedActionInfo>();
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (IEnumerable<ExposedActionInfo>) Array.Empty<ExposedActionInfo>();
    string screenID = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID);
    IEnumerable<AUWorkflowStateAction> states = workflowAndState == null ? Enumerable.Empty<AUWorkflowStateAction>() : this._workflowActionsEngine.GetActionStates(workflowAndState.ScreenID, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
    return this._workflowActionsEngine.GetActionDefinitions(screenID).Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it =>
    {
      if (!onlyExposed)
        return true;
      bool? exposedToMobile = it.ExposedToMobile;
      bool flag = true;
      return exposedToMobile.GetValueOrDefault() == flag & exposedToMobile.HasValue;
    })).Select<AUScreenActionBaseState, ExposedActionInfo>((Func<AUScreenActionBaseState, ExposedActionInfo>) (it =>
    {
      FormDefinition formDefinition = !string.IsNullOrEmpty(it.Form) ? this._auWorkflowFormsEngine.GetFormDefinition(screenID, it.Form) : (FormDefinition) null;
      ExposedActionInfo allWorkflowActions = new ExposedActionInfo()
      {
        FormName = it.Form,
        ActionName = it.ActionName,
        FormCaption = formDefinition?.Form.DisplayName,
        Connotation = it.Connotation,
        IsTopLevel = it.IsTopLevel
      };
      AUWorkflowStateAction actionState = states.FirstOrDefault<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (_ => _.ActionName.OrdinalEquals(it.ActionName)));
      if (actionState != null)
        allWorkflowActions.State = new ActionState(actionState);
      if (formDefinition != null & collectFields)
      {
        foreach (AUWorkflowFormField field in formDefinition.Fields)
        {
          Dictionary<string, string> source = (Dictionary<string, string>) null;
          System.Type dacType = (System.Type) null;
          string name = (string) null;
          SchemaFieldEditors? schemaFieldEditor = field.SchemaFieldEditor;
          if (schemaFieldEditor.HasValue)
          {
            switch (schemaFieldEditor.GetValueOrDefault())
            {
              case SchemaFieldEditors.ComboBox:
                source = (!string.IsNullOrEmpty(field.ComboBoxValues) ? new PXStringListAttribute(field.ComboBoxValues) : new PXStringListAttribute()).ValueLabelDic;
                goto label_10;
              case SchemaFieldEditors.CheckBox:
              case SchemaFieldEditors.RichTextEdit:
                goto label_10;
            }
          }
          if (FormFieldHelper.TryGetFieldFromFormFieldName(graph, field.SchemaField, out dacType, out name) && graph.Caches[dacType].GetStateExt((object) null, name) is PXStringState stateExt2)
          {
            source = stateExt2.ValueLabelDic;
            if (!string.IsNullOrEmpty(field.ComboBoxValues))
            {
              string[] filteredValues = field.ComboBoxValues.Split(';');
              source = source.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (str => ((IEnumerable<string>) filteredValues).Contains<string>(str.Key))).ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (str => str.Key), (Func<KeyValuePair<string, string>, string>) (str => str.Value));
            }
          }
label_10:
          Dictionary<string, FormFieldInfo> fields = allWorkflowActions.Fields;
          string fieldName = field.FieldName;
          FormFieldInfo formFieldInfo = new FormFieldInfo();
          formFieldInfo.IsRequired = this.GetRequiedSimple(field);
          formFieldInfo.PossibleComboBoxValues = source;
          formFieldInfo.DacType = dacType;
          formFieldInfo.DacField = name;
          schemaFieldEditor = field.SchemaFieldEditor;
          SchemaFieldEditors schemaFieldEditors = SchemaFieldEditors.RichTextEdit;
          formFieldInfo.TextType = schemaFieldEditor.GetValueOrDefault() == schemaFieldEditors & schemaFieldEditor.HasValue ? "HTML" : (string) null;
          fields.Add(fieldName, formFieldInfo);
        }
      }
      return allWorkflowActions;
    }));
  }

  public IEnumerable<TransitionInfo> GetPossibleTransition(PXGraph graph, string targetState)
  {
    if (graph == null)
      return (IEnumerable<TransitionInfo>) Array.Empty<TransitionInfo>();
    string statusValue = this.ConvertStateToSystemValue(graph, targetState);
    return this.GetPossibleTransition(graph).Where<TransitionInfo>((Func<TransitionInfo, bool>) (it => it.TargetState.Equals(statusValue, StringComparison.OrdinalIgnoreCase)));
  }

  private bool? GetRequiedSimple(AUWorkflowFormField auWorkflowFormField)
  {
    if (string.IsNullOrEmpty(auWorkflowFormField.RequiredCondition))
      return new bool?();
    bool result;
    return !bool.TryParse(auWorkflowFormField.RequiredCondition, out result) ? new bool?() : new bool?(result);
  }

  private string ConvertStateToSystemValue(PXGraph graph, string state)
  {
    object stateExt = graph.Views[graph.PrimaryView].Cache.GetStateExt((object) null, this.GetWorkflowDefinitionFields(graph).stateField);
    string label = state;
    string result;
    if (label != null && stateExt is PXStringState pxStringState && pxStringState.TryGetListValue(label, out result))
      label = result;
    return label;
  }

  public void FillFormValues(PXGraph graph, string formName, Dictionary<string, object> values)
  {
    this._auWorkflowFormsEngine.SetFormValues(graph, formName, (IDictionary<string, object>) values);
  }

  public void FillFormValues(
    PXGraph graph,
    TransitionInfo transition,
    Dictionary<string, object> values)
  {
    this._auWorkflowFormsEngine.SetFormValues(graph, transition.FormName, (IDictionary<string, object>) values);
  }

  public void FillFormValuesByAction(
    PXGraph graph,
    string actionName,
    Dictionary<string, object> values)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return;
    IEnumerable<AUScreenActionBaseState> actionDefinitions = this._workflowActionsEngine.GetActionDefinitions(screenIdFromGraphType);
    AUScreenActionBaseState screenActionBaseState = actionDefinitions != null ? actionDefinitions.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase))) : (AUScreenActionBaseState) null;
    this._auWorkflowFormsEngine.SetFormValues(graph, screenActionBaseState?.Form, (IDictionary<string, object>) values);
  }

  public IEnumerable<string> GetWorkflowAddedActions(string screenId)
  {
    return this._workflowActionsEngine.GetActionDefinitions(screenId).OfType<AUScreenNavigationActionState>().Select<AUScreenNavigationActionState, string>((Func<AUScreenNavigationActionState, string>) (it => it.ActionName));
  }

  public string GetWorkflowStatePropertyName(string screenId)
  {
    return this._auWorkflowEngine.GetWorkflowStatePropertyName(screenId);
  }

  public bool ApplyComboBoxValues(
    PXGraph graph,
    string tableName,
    string fieldName,
    ref string[] allowedValues,
    ref string[] allowedLabels)
  {
    return !PXUseSystemOnlyWorkflow.UseSystemOnlyWorkflow() ? this._pxWorkflowService.ApplyComboBoxValues(graph, tableName, fieldName, ref allowedValues, ref allowedLabels) : this._pxWorkflowService.ApplySystemOnlyComboBoxValues(graph, tableName, fieldName, ref allowedValues, ref allowedLabels);
  }

  public void AskExt(PXGraph graph, FormDefinition definition)
  {
    int num = (int) this._auWorkflowFormsEngine.AskExt(graph, definition);
  }

  public bool IsWorkflowDefinitionDefined(PXGraph graph)
  {
    return this._pxWorkflowService.IsWorkflowDefinitionDefined(graph);
  }

  public FormInfo GetCurrentFormValues(PXGraph graph)
  {
    IReadOnlyDictionary<string, object> formValues = this._auWorkflowFormsEngine.GetFormValues(graph);
    if (formValues == null)
      return (FormInfo) null;
    FormDefinitionRecord current = graph.Views["FilterPreview"].Cache.Current as FormDefinitionRecord;
    return new FormInfo()
    {
      Name = current.FormName,
      Values = formValues.ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (it => it.Key), (Func<KeyValuePair<string, object>, object>) (it => it.Value))
    };
  }

  public Dictionary<string, string> GetPossibleComboBoxValuesForState<TField>(
    PXGraph graph,
    string status)
    where TField : IBqlField
  {
    return this.GetPossibleComboBoxValuesForState(graph, typeof (TField).DeclaringType, typeof (TField).Name, status);
  }

  public Dictionary<string, string> GetPossibleComboBoxValuesForState(
    PXGraph graph,
    System.Type dacType,
    string fieldName,
    string state)
  {
    if (graph == null || dacType == (System.Type) null || string.IsNullOrEmpty(fieldName))
      return (Dictionary<string, string>) null;
    PXStringState stringState = graph.Caches[dacType].GetStateExt((object) null, fieldName) as PXStringState;
    if (stringState == null)
      return (Dictionary<string, string>) null;
    string str1 = state;
    string result;
    if (str1 != null && stringState.TryGetListValue(str1, out result))
      str1 = result;
    IEnumerable<AUWorkflowStateProperty> stateProperties = this._auWorkflowEngine.GetStateProperties(this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _), str1);
    if (stateProperties == null)
      return stringState.ValueLabelDic;
    AUWorkflowStateProperty workflowStateProperty = stateProperties.FirstOrDefault<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => fieldName.Equals(it.FieldName, StringComparison.OrdinalIgnoreCase) && dacType.FullName.Equals(it.ObjectName, StringComparison.OrdinalIgnoreCase)));
    if (workflowStateProperty == null)
      return stringState.ValueLabelDic;
    string str2;
    return new PXStringListAttribute(((IEnumerable<string>) workflowStateProperty.ComboBoxValues.Split(new char[1]
    {
      ';'
    }, StringSplitOptions.RemoveEmptyEntries)).Select<string, (string, string)>((Func<string, (string, string)>) (it => (it, stringState.ValueLabelDic.TryGetValue(it, out str2) ? str2 : (string) null))).Where<(string, string)>((Func<(string, string), bool>) (it => it.Item2 != null)).ToArray<(string, string)>()).ValueLabelDic;
  }

  public IEnumerable<(string objectName, string fieldName)> GetRequiredFieldsForState(
    PXGraph graph,
    string state)
  {
    IEnumerable<AUWorkflowStateProperty> propertiesRecursive = this._auWorkflowEngine.GetStatePropertiesRecursive(this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _), this.ConvertStateToSystemValue(graph, state));
    return propertiesRecursive == null ? (IEnumerable<(string, string)>) null : propertiesRecursive.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it =>
    {
      bool? isRequired = it.IsRequired;
      bool flag = true;
      return isRequired.GetValueOrDefault() == flag & isRequired.HasValue;
    })).Select<AUWorkflowStateProperty, (string, string)>((Func<AUWorkflowStateProperty, (string, string)>) (it => (it.ObjectName, it.FieldName)));
  }

  public bool IsInSpecifiedStatus(PXGraph graph, string state)
  {
    string systemValue = this.ConvertStateToSystemValue(graph, state);
    string stateID;
    this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID);
    string str = stateID;
    return systemValue.Equals(str, StringComparison.OrdinalIgnoreCase);
  }

  public AUWorkflowFormField[] GetWorkflowFormFields(string screenID, string formName)
  {
    return this._auWorkflowFormsEngine.GetFormDefinition(screenID, formName).Fields;
  }

  public PXFieldState[] GetFormFields(string screenID, PXGraph schemaGraph, string formName)
  {
    AUWorkflowFormField[] fields = this._auWorkflowFormsEngine.GetFormDefinition(screenID, formName).Fields;
    return this.ConvertAUWorkflowFormFieldToPXFieldState(screenID, schemaGraph, (IEnumerable<AUWorkflowFormField>) fields, formName);
  }

  public PXFieldState[] GetScreenFields(string screenID, PXGraph schemaGraph, string formName = null)
  {
    IEnumerable<AUWorkflowFormField> screenFields = this._auWorkflowFormsEngine.GetScreenFields(screenID);
    return this.ConvertAUWorkflowFormFieldToPXFieldState(screenID, schemaGraph, screenFields, formName);
  }

  private PXFieldState[] ConvertAUWorkflowFormFieldToPXFieldState(
    string screenID,
    PXGraph schemaGraph,
    IEnumerable<AUWorkflowFormField> formFields,
    string formName = null)
  {
    List<PXFieldState> pxFieldStateList = new List<PXFieldState>();
    IReadOnlyDictionary<string, Lazy<bool>> readOnlyDictionary = string.IsNullOrEmpty(formName) ? (IReadOnlyDictionary<string, Lazy<bool>>) ImmutableDictionary<string, Lazy<bool>>.Empty : this.GetScreenFormConditions(screenID, schemaGraph, formName);
    foreach (AUWorkflowFormField formField in formFields)
    {
      System.Type dacType;
      string name;
      if (FormFieldHelper.TryGetFieldFromFormFieldName(schemaGraph, formField.SchemaField, out dacType, out name))
      {
        bool? nullable1;
        if (!(schemaGraph.Caches[dacType].GetStateExt((object) null, name) is PXFieldState pxFieldState))
        {
          bool flag = string.IsNullOrEmpty(formField.HideCondition) || string.IsNullOrEmpty(formName) || !readOnlyDictionary[formField.HideCondition].Value;
          SchemaFieldEditors? schemaFieldEditor = formField.SchemaFieldEditor;
          if (schemaFieldEditor.HasValue)
          {
            switch (schemaFieldEditor.GetValueOrDefault())
            {
              case SchemaFieldEditors.ComboBox:
                PXStringListAttribute stringListAttribute = !string.IsNullOrEmpty(formField.ComboBoxValues) ? new PXStringListAttribute(formField.ComboBoxValues) : new PXStringListAttribute();
                int? length1 = new int?();
                bool? isUnicode1 = new bool?(true);
                string fieldName1 = formField.FieldName;
                bool? isKey1 = new bool?(false);
                nullable1 = formField.Required;
                int? required1;
                if (!nullable1.HasValue)
                {
                  required1 = new int?();
                }
                else
                {
                  nullable1 = formField.Required;
                  required1 = new int?(Convert.ToInt32(nullable1.Value));
                }
                string[] array1 = stringListAttribute.ValueLabelDic.Keys.ToArray<string>();
                string[] array2 = stringListAttribute.ValueLabelDic.Values.ToArray<string>();
                bool? exclusiveValues1 = new bool?(false);
                pxFieldState = PXStringState.CreateInstance((object) null, length1, isUnicode1, fieldName1, isKey1, required1, (string) null, array1, array2, exclusiveValues1, (string) null);
                break;
              case SchemaFieldEditors.CheckBox:
                System.Type dataType = typeof (bool);
                nullable1 = new bool?();
                bool? isKey2 = nullable1;
                bool? nullable2 = new bool?(true);
                nullable1 = formField.Required;
                int? required2;
                if (!nullable1.HasValue)
                {
                  required2 = new int?();
                }
                else
                {
                  nullable1 = formField.Required;
                  required2 = new int?(Convert.ToInt32(nullable1.Value));
                }
                int? precision = new int?();
                int? length2 = new int?();
                string fieldName2 = formField.FieldName;
                string displayName1 = formField.DisplayName;
                string displayName2 = formField.DisplayName;
                nullable1 = new bool?();
                bool? enabled = nullable1;
                nullable1 = new bool?();
                bool? visible = nullable1;
                nullable1 = new bool?();
                bool? readOnly = nullable1;
                pxFieldState = PXFieldState.CreateInstance((object) null, dataType, isKey2, nullable2, required2, precision, length2, fieldName: fieldName2, descriptionName: displayName1, displayName: displayName2, enabled: enabled, visible: visible, readOnly: readOnly);
                break;
              case SchemaFieldEditors.RichTextEdit:
                int? length3 = new int?();
                bool? isUnicode2 = new bool?(true);
                string fieldName3 = formField.FieldName;
                bool? isKey3 = new bool?(false);
                nullable1 = formField.Required;
                int? required3;
                if (!nullable1.HasValue)
                {
                  required3 = new int?();
                }
                else
                {
                  nullable1 = formField.Required;
                  required3 = new int?(Convert.ToInt32(nullable1.Value));
                }
                bool? exclusiveValues2 = new bool?(false);
                pxFieldState = PXStringState.CreateInstance((object) null, length3, isUnicode2, fieldName3, isKey3, required3, (string) null, (string[]) null, (string[]) null, exclusiveValues2, (string) null);
                break;
            }
          }
          if (flag && pxFieldState != null)
            pxFieldState.Visibility = PXUIVisibility.Visible;
        }
        else
        {
          pxFieldState.SetFieldName(formField.FieldName);
          pxFieldState.Visible = true;
          if (!string.IsNullOrEmpty(formField.DisplayName))
            pxFieldState.DisplayName = formField.DisplayName;
          pxFieldState.Enabled = true;
          pxFieldState.IsReadOnly = false;
          pxFieldState.PrimaryKey = false;
          if (!string.IsNullOrEmpty(formField.ComboBoxValues))
          {
            PXStringState stringState = pxFieldState as PXStringState;
            if (stringState != null)
            {
              if (formField.ComboBoxValues.Contains(","))
              {
                PXStringListAttribute stringListAttribute = new PXStringListAttribute(formField.ComboBoxValues);
                stringState.AllowedValues = stringListAttribute.ValueLabelDic.Keys.ToArray<string>();
                stringState.AllowedLabels = stringListAttribute.ValueLabelDic.Values.ToArray<string>();
              }
              else
              {
                string[] array3 = ((IEnumerable<string>) formField.ComboBoxValues.Split(';')).Where<string>((Func<string, bool>) (it => stringState.ValueLabelDic.ContainsKey(it))).ToArray<string>();
                string[] array4 = ((IEnumerable<string>) array3).Select<string, string>((Func<string, string>) (it => stringState.ValueLabelDic[it])).ToArray<string>();
                stringState.AllowedValues = array3;
                stringState.AllowedLabels = array4;
              }
            }
          }
        }
        if (pxFieldState != null)
        {
          bool? nullable3;
          if (!string.IsNullOrEmpty(formField.RequiredCondition))
          {
            if (!string.IsNullOrEmpty(formName))
            {
              nullable3 = new bool?(readOnlyDictionary[formField.RequiredCondition].Value);
            }
            else
            {
              nullable1 = new bool?();
              nullable3 = nullable1;
            }
          }
          else
          {
            nullable1 = new bool?();
            nullable3 = nullable1;
          }
          bool? nullable4 = nullable3;
          bool flag = string.IsNullOrEmpty(formField.HideCondition) || string.IsNullOrEmpty(formName) || !readOnlyDictionary[formField.HideCondition].Value;
          pxFieldState.Required = nullable4;
          pxFieldState.Visible = flag;
          pxFieldStateList.Add(pxFieldState);
        }
      }
    }
    return pxFieldStateList.ToArray();
  }

  public Dictionary<string, AUScreenActionBaseState> GetMassProcessingActions(string screenId)
  {
    return this._workflowActionsEngine.GetMassProcessingActions(screenId).ToDictionary<KeyValuePair<string, AUScreenActionBaseState>, string, AUScreenActionBaseState>((Func<KeyValuePair<string, AUScreenActionBaseState>, string>) (it => it.Key), (Func<KeyValuePair<string, AUScreenActionBaseState>, AUScreenActionBaseState>) (it => it.Value));
  }

  public void ClearFormData(PXGraph graph) => this._auWorkflowFormsEngine.ClearFormData(graph);

  public bool IsInAutoAction(PXGraph graph) => this._pxWorkflowService.IsInAutoAction(graph);

  private IReadOnlyDictionary<string, Lazy<bool>> GetScreenFormConditions(
    string screenID,
    PXGraph schemaGraph,
    string formName)
  {
    return this._wfConditionEvaluateService.EvaluateConditions(schemaGraph, schemaGraph.GetPrimaryCache()?.Current, this._pxWorkflowService.GetScreen(screenID), formName, this._auWorkflowFormsEngine.GetFormValues(schemaGraph, formName));
  }
}
