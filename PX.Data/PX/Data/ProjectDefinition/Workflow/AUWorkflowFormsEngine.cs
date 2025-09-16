// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.AUWorkflowFormsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Automation;
using PX.Data.Automation.State;
using PX.Data.DacDescriptorGeneration;
using PX.Data.Utility;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal class AUWorkflowFormsEngine : IAUWorkflowFormsEngine
{
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;
  private readonly IWorkflowConditionEvaluateService _wfConditionEvaluateService;
  private readonly IAUWorkflowEngine _auWorkflowEngine;
  public const string WorkflowFormView = "FilterPreview";
  public const string WorkflowActionName = "WorkflowTransition";
  public const string FormDefinition = "FormDefinition";
  public const string FieldUsedSuffix = "_FieldUsed";
  public const string SelectorViewNamePrefix = "WorkflowForm$";
  public const string DacTypeViewNamePrefix = "WorkflowDactTypeForm$";
  public const string CurrentFormActionSlotName = "CurrentFormActionName";

  private PXSelectBase GetFilter<TFilter>(PXGraph graph) where TFilter : class, IBqlTable, new()
  {
    return (PXSelectBase) new PXFilter<TFilter>(graph);
  }

  public AUWorkflowFormsEngine(
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService,
    IWorkflowConditionEvaluateService wfConditionEvaluateService,
    IAUWorkflowEngine auWorkflowEngine)
  {
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
    this._wfConditionEvaluateService = wfConditionEvaluateService;
    this._auWorkflowEngine = auWorkflowEngine;
  }

  public PX.Data.ProjectDefinition.Workflow.FormDefinition GetFormDefinition(
    string screen,
    string formName)
  {
    return this.GetFormDefinition(screen, formName, false);
  }

  public void PrepareFormData(PXGraph graph, string formName, bool clearFormData, Screen screen)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    this.PrepareDataInternal(graph, formName, clearFormData, screenIdFromGraphType, screen);
  }

  public void PrepareFormDataForMassProcessing(PXGraph graph, string formName, string screen)
  {
    this.ClearFormData(graph);
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(screen, formName);
    PXCache cach = graph.Caches[typeof (FormDefinitionRecord)];
    FormDefinitionRecord definitionRecord = this.InsertFormDefinitionRecord(definition, cach, true);
    if (definitionRecord == null || definitionRecord.Key != definition.Key)
      throw new Exception("Fail to init form fields");
    PXFilter<FormDefinitionRecord> filterPreview = this.GetFilterPreview(graph);
    this.InitFormFields(definitionRecord, filterPreview);
    foreach (AUWorkflowFormField field in definition.Fields)
    {
      bool? isActive = field.IsActive;
      bool flag = true;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue && cach.GetValueExt((object) definitionRecord, field.FieldName) is PXFieldState valueExt && valueExt.Value == null)
        cach.SetValueExt((object) definitionRecord, field.FieldName, this.GetFieldDefaultValue(graph, definitionRecord, field));
    }
  }

  private FormDefinitionRecord InsertFormDefinitionRecord(
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition,
    PXCache cache,
    bool useMulti,
    bool fromSession = false,
    string actionName = null)
  {
    FormDefinitionRecord definitionRecord = new FormDefinitionRecord()
    {
      Key = definition.Key,
      FormName = definition.FormName,
      Screen = definition.Screen,
      FormDacName = definition.DacType,
      UseMulti = new bool?(useMulti),
      GetFromSession = new bool?(fromSession),
      ActionName = actionName
    };
    return (FormDefinitionRecord) cache.Insert((object) definitionRecord);
  }

  private PX.Data.ProjectDefinition.Workflow.FormDefinition GetFormDefinition(
    string screen,
    string formName,
    bool getFromSession)
  {
    return getFromSession && this.SessionDefinition != null ? this.SessionDefinition : AUWorkflowFormsEngine.Slot.GetDefinition(screen, formName);
  }

  private void InitFormDefinitionAndFields(
    PXGraph graph,
    string formName,
    bool clearFormData,
    string screenId,
    Screen screen)
  {
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition1 = AUWorkflowFormsEngine.Slot.GetDefinition(screenId, formName);
    PXCache cach = graph.Caches[typeof (FormDefinitionRecord)];
    FormDefinitionRecord definition2 = cach.Current as FormDefinitionRecord;
    if (clearFormData || definition2 == null || definition2.Key == null)
      definition2 = this.InsertFormDefinitionRecord(definition1, cach, false);
    if (definition2 == null || definition2.Key != definition1.Key)
      throw new Exception("Fail to init form fields");
    PXFilter<FormDefinitionRecord> filterPreview = this.GetFilterPreview(graph);
    this.InitFormFields(definition2, filterPreview, screen, true);
  }

  private PXFilter<FormDefinitionRecord> GetFilterPreview(PXGraph graph)
  {
    PXView view;
    return graph.Views.TryGetValue("FilterPreview", out view) ? graph.Views.GetExternalMember(view) as PXFilter<FormDefinitionRecord> : AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
  }

  private void PrepareDataInternal(
    PXGraph graph,
    string formName,
    bool clearFormData,
    string screenId,
    Screen screen)
  {
    if (clearFormData)
      this.ClearFormData(graph);
    this.InitFormDefinitionAndFields(graph, formName, clearFormData, screenId, screen);
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(screenId, formName);
    PXCache cach = graph.Caches[typeof (FormDefinitionRecord)];
    FormDefinitionRecord current = cach.Current as FormDefinitionRecord;
    foreach (AUWorkflowFormField field in definition.Fields)
    {
      bool? isActive = field.IsActive;
      bool flag1 = true;
      if (isActive.GetValueOrDefault() == flag1 & isActive.HasValue && cach.GetValueExt((object) current, field.FieldName) is PXFieldState valueExt && valueExt.Value == null)
      {
        bool? useMulti = current.UseMulti;
        bool flag2 = true;
        if (!(useMulti.GetValueOrDefault() == flag2 & useMulti.HasValue))
          cach.SetValueExt((object) current, field.FieldName, this.GetFieldDefaultValue(graph, current, field));
      }
    }
  }

  /// <remarks>Use this method only to show test form</remarks>
  public WebDialogResult AskExt(PXGraph graph, PX.Data.ProjectDefinition.Workflow.FormDefinition formDefinition)
  {
    PXFilter<FormDefinitionRecord> workflowFormFilter = AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
    graph.Caches[typeof (FormDefinitionRecord)].Clear();
    FormDefinitionRecord formDefinitionRecord = this.InsertFormDefinitionRecord(formDefinition, graph.Caches[typeof (FormDefinitionRecord)], false, true);
    this.SessionDefinition = formDefinition;
    this.InitFormFieldsInternal(formDefinitionRecord, false, formDefinition, workflowFormFilter.Cache, graph, false, (Screen) null);
    return workflowFormFilter.View.AskExtWithHeader(PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowForm(formDefinition.Form.FormName, formDefinition.Form.DisplayName), (List<string>) null);
  }

  public bool AskExt(
    PXGraph graph,
    object row,
    string formName,
    string screenID,
    Screen screen,
    string actionName,
    bool repaintControls = false)
  {
    if (DialogManager.GetAnswer(graph, "FilterPreview", formName).IsPositive())
      return true;
    FormDefinitionRecord current = graph.Caches[typeof (FormDefinitionRecord)].Current as FormDefinitionRecord;
    PXFilter<FormDefinitionRecord> workflowFormFilter = AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(screenID, formName);
    if (current?.FormName != definition.Form.FormName)
      this.ResetFormDefinitionData(graph, definition, actionName, workflowFormFilter);
    this.InitFormDefinitionAndFields(graph, formName, false, screenID, screen);
    IEnumerable<AUWorkflowFormField> source = ((IEnumerable<AUWorkflowFormField>) definition.Fields).Where<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => field.IsVisible));
    string header = PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowForm(definition.Form.FormName, definition.Form.DisplayName);
    if (!source.Any<AUWorkflowFormField>() || workflowFormFilter.View.AskExtWithHeader(header, new PXView.InitializePanel(InitializeHandler), repaintControls: repaintControls) == WebDialogResult.OK)
    {
      if (!this.ValidateRequiredFields(workflowFormFilter))
      {
        PXCache primaryCache = graph.GetPrimaryCache();
        DacDescriptor? emptyDacDescriptor = primaryCache.GetNonEmptyDacDescriptor(row);
        string itemName = PXUIFieldAttribute.GetItemName(primaryCache);
        throw new PXException(emptyDacDescriptor, "{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
        {
          (object) ErrorMessages.GetLocal("Updating "),
          (object) itemName
        });
      }
      return true;
    }
    this.ClearFormData(graph);
    return false;

    void InitializeHandler(PXGraph gr, string viewName)
    {
      string stateID;
      this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, graph.GetPrimaryCache().Current, out stateID);
      this.ValidateComboBoxSource(definition, actionName, stateID);
    }
  }

  public bool AskMassUpdateExt(
    PXGraph graph,
    string formName,
    string screenID,
    Screen screen,
    string actionName)
  {
    FormDefinitionRecord current = graph.Caches[typeof (FormDefinitionRecord)].Current as FormDefinitionRecord;
    PXFilter<FormDefinitionRecord> workflowFormFilter = AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(screenID, formName);
    if (current?.FormName != definition.Form.FormName)
      this.ResetFormDefinitionData(graph, definition, actionName, workflowFormFilter, true);
    EnumerableExtensions.ForEach<AUWorkflowFormField>((IEnumerable<AUWorkflowFormField>) this.InitFormFields(graph.Caches[typeof (FormDefinitionRecord)].Current as FormDefinitionRecord, workflowFormFilter, screen).Fields, (System.Action<AUWorkflowFormField>) (field => field.IsVisible = field.HideCondition == null || field.HideCondition.ToLower() != bool.TrueString.ToLower()));
    AUWorkflowFormField[] array = ((IEnumerable<AUWorkflowFormField>) definition.Fields).Where<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => field.IsVisible)).ToArray<AUWorkflowFormField>();
    string header = PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowForm(definition.Form.FormName, definition.Form.DisplayName);
    if (!((IEnumerable<AUWorkflowFormField>) array).Any<AUWorkflowFormField>() && !((IEnumerable<AUWorkflowFormField>) array).Any<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => string.Equals(field.RequiredCondition, true.ToString(), StringComparison.OrdinalIgnoreCase) && Str.IsNullOrEmpty(field.DefaultValue))) || workflowFormFilter.View.AskExtWithHeader(header, new PXView.InitializePanel(InitializeHandler)) == WebDialogResult.OK)
    {
      if (!this.ValidateRequiredFields(workflowFormFilter))
        throw new PXDynamicFormValidationException();
      return true;
    }
    this.ClearFormData(graph);
    return false;

    void InitializeHandler(PXGraph gr, string viewName)
    {
      this.ValidateComboBoxSource(definition, actionName, (string) null);
    }
  }

  private void ValidateComboBoxSource(
    PX.Data.ProjectDefinition.Workflow.FormDefinition form,
    string actionName,
    string currentStateId)
  {
    if (form == null)
      throw new ArgumentNullException(nameof (form));
    if (string.IsNullOrEmpty(actionName))
      return;
    List<AUWorkflowFormField> list = ((IEnumerable<AUWorkflowFormField>) form.Fields).Where<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => field.ComboBoxValuesSource == "T" && string.Equals(form.FormName, field.FormName, StringComparison.OrdinalIgnoreCase) && string.Equals(form.Screen, field.Screen, StringComparison.OrdinalIgnoreCase))).ToList<AUWorkflowFormField>();
    if (NonGenericIEnumerableExtensions.Empty_((IEnumerable) list))
      return;
    actionName = ((IEnumerable<string>) actionName.Split('$')).Last<string>();
    IEnumerable<(AUWorkflowTransition, AUWorkflow)> source = this._auWorkflowEngine.GetTransitions($"{form.Screen}${actionName}", (string) null);
    if (source == null)
      return;
    if (!string.IsNullOrEmpty(currentStateId))
      source = source.Where<(AUWorkflowTransition, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), bool>) (pair => pair.transition.FromStateName == currentStateId));
    if (EnumerableExtensions.Distinct<(string, AUWorkflow), string>(source.Select<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>) (pair => (pair.transition.TargetStateName, pair.workflow))), (Func<(string, AUWorkflow), string>) (pair => (pair.workflow?.WorkflowGUID ?? Guid.NewGuid().ToString()) + pair.TargetStateName)).Count<(string, AUWorkflow)>() <= 1)
      return;
    foreach (AUWorkflowFormField workflowFormField in list)
      PXTrace.Logger.ForSystemEvents("System", "System_WorkflowWillProbablyFail").ForCurrentCompanyContext().Warning<string, string, string>("The {ActionName} action of the {Screen} form is used in multiple transitions that have different target states so the system cannot determinate the target state in advance, and, therefore, the list of combo box values for the {SchemaField} field. You need to specify the list of combo box values without using a target state or use the action only in transitions with the same target state.", actionName, workflowFormField.Screen, workflowFormField.SchemaField);
  }

  private void ResetFormDefinitionData(
    PXGraph graph,
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition,
    string actionName,
    PXFilter<FormDefinitionRecord> filter,
    bool useMulti = false)
  {
    this.ClearFormData(graph);
    this.InsertFormDefinitionRecord(definition, filter.Cache, useMulti, actionName: actionName);
  }

  private bool ValidateRequiredFields(PXFilter<FormDefinitionRecord> pxFilter)
  {
    PXCache cache = pxFilter.Cache;
    cache.RaiseRowSelected(cache.Current);
    bool flag1 = true;
    foreach (string field in (List<string>) cache.Fields)
    {
      if (this.HasError(cache, field))
        return false;
      if (cache.GetStateExt(cache.Current, field) is PXFieldState stateExt)
      {
        if (stateExt.ErrorLevel == PXErrorLevel.Error)
          flag1 = false;
        bool? required = stateExt.Required;
        bool flag2 = true;
        if (required.GetValueOrDefault() == flag2 & required.HasValue)
        {
          object obj = stateExt.Value;
          if (obj == null || obj is string str && string.IsNullOrWhiteSpace(str))
          {
            PXRowPersistingException persistingException = new PXRowPersistingException(field, (object) null, "'{0}' cannot be empty.", new object[1]
            {
              (object) (stateExt.DisplayName ?? field)
            });
            cache.RaiseExceptionHandling(field, cache.Current, (object) null, (Exception) persistingException);
            flag1 = false;
          }
        }
      }
    }
    return flag1;
  }

  public bool HasError(PXCache cache, string fieldName)
  {
    return !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly(cache, cache.Current, fieldName));
  }

  public bool HasChanges(PXGraph graph)
  {
    PXView pxView;
    if (!graph.Views.TryGetValue("FilterPreview", out pxView))
      return false;
    foreach (object obj in pxView.Cache.Cached)
    {
      if (obj != null)
      {
        for (int idx = 0; idx < pxView.Cache.SlotsCount; ++idx)
        {
          if (pxView.Cache.GetSlot<AUWorkflowFormsEngine.ValueContainer>(obj, idx)?.Value != null)
            return true;
        }
      }
    }
    return false;
  }

  public PX.Data.ProjectDefinition.Workflow.FormDefinition OnInit(
    PXGraph graph,
    bool useCurrentData = false)
  {
    if (ResourceCollectingManager.IsStringCollecting)
      PXPageRipper.RipWorkflow(graph);
    if (graph.Caches[typeof (FormDefinitionRecord)].Current is FormDefinitionRecord current && current.Key != null)
    {
      PXFilter<FormDefinitionRecord> workflowFormFilter = AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
      workflowFormFilter.Current = current;
      return this.InitFormFields(current, workflowFormFilter, graph.WorkflowService?.GetScreen(current.Screen), useCurrentData);
    }
    this.ClearFormData(graph);
    return (PX.Data.ProjectDefinition.Workflow.FormDefinition) null;
  }

  public static void InitDiagramWorkflowView(PXGraph graph)
  {
    PXFilter<AUWorkflowFormsEngine.WorkflowView> pxFilter = new PXFilter<AUWorkflowFormsEngine.WorkflowView>(graph);
    graph.Views.Add("WorkflowView", (PXSelectBase) pxFilter);
  }

  private PX.Data.ProjectDefinition.Workflow.FormDefinition InitFormFields(
    FormDefinitionRecord definition,
    PXFilter<FormDefinitionRecord> view,
    Screen screen = null,
    bool useCurrentData = false)
  {
    bool? useMulti = definition.UseMulti;
    bool flag1 = true;
    bool multiMode = useMulti.GetValueOrDefault() == flag1 & useMulti.HasValue;
    string screen1 = definition.Screen;
    PXCache cache = view.Cache;
    PXGraph graph = cache.Graph;
    string screen2 = screen1;
    string formName = definition.FormName;
    bool? getFromSession = definition.GetFromSession;
    bool flag2 = true;
    int num = getFromSession.GetValueOrDefault() == flag2 & getFromSession.HasValue ? 1 : 0;
    PX.Data.ProjectDefinition.Workflow.FormDefinition formDefinition = this.GetFormDefinition(screen2, formName, num != 0);
    if (formDefinition == null)
      return (PX.Data.ProjectDefinition.Workflow.FormDefinition) null;
    this.InitFormFieldsInternal(definition, useCurrentData, formDefinition, cache, graph, multiMode, screen);
    return formDefinition;
  }

  private PX.Data.ProjectDefinition.Workflow.FormDefinition SessionDefinition
  {
    get => PXContext.SessionTyped<AUWorkflowFormsEngine.SessionState>().TestFormDefinition;
    set => PXContext.SessionTyped<AUWorkflowFormsEngine.SessionState>().TestFormDefinition = value;
  }

  private void InitFormFieldsInternal(
    FormDefinitionRecord formDefinitionRecord,
    bool useCurrentData,
    PX.Data.ProjectDefinition.Workflow.FormDefinition formDefinition,
    PXCache formFieldsCache,
    PXGraph schemaGraph,
    bool multiMode,
    Screen screen)
  {
    this.Errors.Clear();
    this.ErrorLevels.Clear();
    PXContext.SetSlot<string>("CurrentFormActionName", formDefinitionRecord.ActionName);
    Dictionary<PXCache, List<Tuple<string, int?>>> dependedCaches = new Dictionary<PXCache, List<Tuple<string, int?>>>();
    bool canUseCacheCurrentRecord = useCurrentData && !multiMode;
    IReadOnlyDictionary<string, Lazy<bool>> conditions = this._wfConditionEvaluateService.EvaluateConditions(schemaGraph, canUseCacheCurrentRecord ? schemaGraph.GetCurrentPrimaryObject() : (object) null, screen, formDefinition.FormName, multiMode ? this.GetFormValuesForMassProcessing(schemaGraph, (object) null, formDefinition.Screen) : this.GetFormValues(schemaGraph, formDefinition.Screen, formDefinition.FormName));
    PXCache formDacTypeCache = (PXCache) null;
    if (!string.IsNullOrEmpty(formDefinition.DacType))
    {
      try
      {
        PXView pxView;
        if (!schemaGraph.Views.TryGetValue("WorkflowDactTypeForm$" + formDefinition.DacType, out pxView))
        {
          System.Type type = PXBuildManager.GetType(formDefinition.DacType, false);
          PXSelectBase pxSelectBase = GenericCall.Of<PXSelectBase>((Expression<Func<PXSelectBase>>) (() => this.GetFilter<AUWorkflowFormsEngine.DummyFilter>(schemaGraph))).ButWith(type, Array.Empty<System.Type>());
          schemaGraph.Views.Add("WorkflowDactTypeForm$" + formDefinition.DacType, pxSelectBase);
          formDacTypeCache = pxSelectBase.Cache;
        }
        else
          formDacTypeCache = pxView.Cache;
      }
      catch
      {
      }
    }
    foreach (AUWorkflowFormField field1 in formDefinition.Fields)
    {
      AUWorkflowFormField field = field1;
      bool? nullable1 = field.IsActive;
      bool flag1 = true;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue && !formFieldsCache.Fields.Contains(field.FieldName))
      {
        int? slotId = new int?();
        if (formDacTypeCache == null || !formDacTypeCache.Fields.Contains(field.FieldName))
          slotId = new int?(formFieldsCache.SetupSlot<AUWorkflowFormsEngine.ValueContainer>((Func<AUWorkflowFormsEngine.ValueContainer>) (() => new AUWorkflowFormsEngine.ValueContainer()), (Func<AUWorkflowFormsEngine.ValueContainer, AUWorkflowFormsEngine.ValueContainer, AUWorkflowFormsEngine.ValueContainer>) ((item, copy) =>
          {
            item.Value = copy?.Value;
            return item;
          }), (Func<AUWorkflowFormsEngine.ValueContainer, AUWorkflowFormsEngine.ValueContainer>) (item => new AUWorkflowFormsEngine.ValueContainer()
          {
            Value = item.Value
          })));
        formFieldsCache.Fields.Add(field.FieldName);
        System.Type dacType1;
        string schemaFieldName;
        if (FormFieldHelper.TryGetFieldFromFormFieldName(schemaGraph, field.SchemaField, out dacType1, out schemaFieldName))
        {
          PXCache schemaCache = schemaGraph.Caches[dacType1];
          if (!dependedCaches.ContainsKey(schemaCache))
            dependedCaches.Add(schemaCache, new List<Tuple<string, int?>>());
          dependedCaches[schemaCache].Add(new Tuple<string, int?>(schemaFieldName, slotId));
          object defaultSchemaState = schemaCache.GetStateExt((object) null, schemaFieldName);
          object defaultComboboxSourceFieldSchemaState = defaultSchemaState;
          System.Type dacType2;
          string name;
          if (!string.IsNullOrEmpty(field.ComboboxAndDefaultSourceField) && FormFieldHelper.TryGetFieldFromFormFieldName(schemaGraph, field.ComboboxAndDefaultSourceField, out dacType2, out name))
            defaultComboboxSourceFieldSchemaState = schemaGraph.Caches[dacType2].GetStateExt((object) null, name);
          object currentSchemaState = canUseCacheCurrentRecord ? schemaCache.GetStateExt(schemaCache.Current, schemaFieldName) : defaultSchemaState;
          object fieldDefaultValue = this.GetFieldDefaultValue(formDefinitionRecord, field, schemaFieldName, currentSchemaState, schemaCache, canUseCacheCurrentRecord, schemaCache.Current);
          bool? nullable2;
          if (!string.IsNullOrEmpty(field.RequiredCondition))
          {
            Lazy<bool> lazy;
            nullable2 = new bool?(useCurrentData ? conditions.TryGetValue(field.RequiredCondition, out lazy) && lazy.Value : bool.TrueString.OrdinalEquals(field.RequiredCondition));
          }
          else
          {
            nullable1 = new bool?();
            nullable2 = nullable1;
          }
          bool? isFieldRequired = nullable2;
          bool? nullable3;
          if (!string.IsNullOrEmpty(field.HideCondition))
          {
            Lazy<bool> lazy;
            nullable3 = new bool?(useCurrentData ? conditions.TryGetValue(field.HideCondition, out lazy) && !lazy.Value : bool.FalseString.OrdinalEquals(field.HideCondition));
          }
          else
          {
            nullable1 = new bool?();
            nullable3 = nullable1;
          }
          bool? isFieldVisible = nullable3;
          nullable1 = isFieldRequired;
          bool flag2 = true;
          if ((!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue) ? 0 : (fieldDefaultValue == null ? 1 : 0)) != 0)
            isFieldVisible = new bool?(true);
          AUWorkflowFormField workflowFormField = field;
          nullable1 = isFieldVisible;
          int num1 = (int) nullable1 ?? 1;
          workflowFormField.IsVisible = num1 != 0;
          formFieldsCache.Graph.ExceptionHandling.AddHandler(formFieldsCache.GetItemType(), field.FieldName, (PXExceptionHandling) ((_, args) =>
          {
            this.Errors[field.FieldName] = args.Exception.Message;
            this.ErrorLevels[field.FieldName] = PXErrorLevel.Error;
            if (!(args.Exception is PXSetPropertyException exception2))
              return;
            this.ErrorLevels[field.FieldName] = exception2.ErrorLevel;
            this.Errors[field.FieldName] = exception2.MessageNoPrefix;
          }));
          int slotFieldUsed = 0;
          nullable1 = field.FromScheme;
          bool flag3 = true;
          bool defaultValueIsFormula = (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue || field.DefaultValue == null ? 0 : (field.DefaultValue.Contains("[") ? 1 : (field.DefaultValue.Contains("@") ? 1 : 0))) != 0;
          if (multiMode)
          {
            string fieldUsedName = field.FieldName + "_FieldUsed";
            formFieldsCache.Fields.Add(fieldUsedName);
            slotFieldUsed = formFieldsCache.SetupSlot<bool?>((Func<bool?>) (() => new bool?()), (Func<bool?, bool?, bool?>) ((_, copy) => copy), (Func<bool?, bool?>) (item => item));
            formFieldsCache.Graph.FieldSelecting.AddHandler(formFieldsCache.GetItemType(), fieldUsedName, (PXFieldSelecting) ((sender, args) => this.FormFieldUsedFieldSelecting(field, fieldUsedName, defaultValueIsFormula, args, sender, slotFieldUsed)));
            formFieldsCache.Graph.FieldUpdating.AddHandler(formFieldsCache.GetItemType(), fieldUsedName, (PXFieldUpdating) ((sender, args) =>
            {
              object newValue = args.NewValue;
              sender.SetSlot<object>(args.Row, slotFieldUsed, newValue);
            }));
          }
          if (currentSchemaState == null)
          {
            formFieldsCache.Graph.FieldSelecting.AddHandler(formFieldsCache.GetItemType(), field.FieldName, (PXFieldSelecting) ((sender, args) =>
            {
              PXFieldState pxFieldState1 = (PXFieldState) null;
              SchemaFieldEditors? schemaFieldEditor = field.SchemaFieldEditor;
              bool? nullable4;
              if (schemaFieldEditor.HasValue)
              {
                switch (schemaFieldEditor.GetValueOrDefault())
                {
                  case SchemaFieldEditors.ComboBox:
                    PXStringListAttribute stringListAttribute = !string.IsNullOrEmpty(field.ComboBoxValues) ? new PXStringListAttribute(field.ComboBoxValues) : new PXStringListAttribute();
                    pxFieldState1 = PXStringState.CreateInstance((object) null, new int?(), new bool?(true), field.FieldName, new bool?(false), isFieldRequired.HasValue ? new int?(Convert.ToInt32(isFieldRequired.Value)) : new int?(), (string) null, stringListAttribute.ValueLabelDic.Keys.ToArray<string>(), stringListAttribute.ValueLabelDic.Values.ToArray<string>(), new bool?(false), fieldDefaultValue?.ToString());
                    break;
                  case SchemaFieldEditors.CheckBox:
                    pxFieldState1 = PXFieldState.CreateInstance((object) null, typeof (bool), nullable: new bool?(true), required: isFieldRequired.HasValue ? new int?(Convert.ToInt32(isFieldRequired.Value)) : new int?(), defaultValue: (object) fieldDefaultValue?.ToString(), fieldName: field.FieldName, descriptionName: field.DisplayName, displayName: field.DisplayName);
                    break;
                  case SchemaFieldEditors.RichTextEdit:
                    int? length = new int?();
                    bool? isUnicode = new bool?(true);
                    string fieldName = field.FieldName;
                    bool? isKey = new bool?(false);
                    nullable4 = field.Required;
                    int? required;
                    if (!nullable4.HasValue)
                    {
                      required = new int?();
                    }
                    else
                    {
                      nullable4 = field.Required;
                      required = new int?(Convert.ToInt32(nullable4.Value));
                    }
                    bool? exclusiveValues = new bool?(false);
                    string defaultValue = fieldDefaultValue?.ToString();
                    pxFieldState1 = PXStringState.CreateInstance((object) null, length, isUnicode, fieldName, isKey, required, (string) null, (string[]) null, (string[]) null, exclusiveValues, defaultValue);
                    break;
                }
              }
              if (pxFieldState1 == null)
                return;
              PXFieldState pxFieldState2 = pxFieldState1;
              bool? nullable5;
              if (multiMode)
              {
                nullable4 = (bool?) sender.GetSlot<object>(args.Row, slotFieldUsed);
                nullable5 = !nullable4.GetValueOrDefault() ? new bool?(false) : isFieldRequired;
              }
              else
                nullable5 = isFieldRequired;
              pxFieldState2.Required = nullable5;
              if (!string.IsNullOrEmpty(field.DisplayName))
                pxFieldState1.DisplayName = PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowField(formFieldsCache.GetType().Name, field.DisplayName);
              pxFieldState1.DefaultValue = fieldDefaultValue;
              PXFieldState pxFieldState3 = pxFieldState1;
              int num2;
              if (multiMode)
              {
                nullable4 = (bool?) sender.GetSlot<object>(args.Row, slotFieldUsed);
                num2 = (int) nullable4 ?? (!defaultValueIsFormula ? 1 : 0);
              }
              else
                num2 = 1;
              pxFieldState3.Enabled = num2 != 0;
              pxFieldState1.PrimaryKey = false;
              PXFieldState pxFieldState4 = pxFieldState1;
              nullable4 = isFieldVisible;
              int num3 = ((int) nullable4 ?? 1) | (multiMode ? 1 : 0);
              pxFieldState4.Visible = num3 != 0;
              if (this.Errors.ContainsKey(field.FieldName))
              {
                pxFieldState1.Error = this.Errors[field.FieldName];
                pxFieldState1.ErrorLevel = this.ErrorLevels[field.FieldName];
              }
              else
              {
                pxFieldState1.Error = (string) null;
                pxFieldState1.ErrorLevel = PXErrorLevel.Undefined;
              }
              if (args.Row != null)
                pxFieldState1.Value = sender.GetSlot<AUWorkflowFormsEngine.ValueContainer>(args.Row, slotId.Value)?.Value;
              args.ReturnState = (object) pxFieldState1;
            }));
            formFieldsCache.Graph.FieldUpdating.AddHandler(formFieldsCache.GetItemType(), field.FieldName, (PXFieldUpdating) ((sender, args) =>
            {
              object newValue = args.NewValue;
              sender.SetSlot<AUWorkflowFormsEngine.ValueContainer>(args.Row, slotId.Value, new AUWorkflowFormsEngine.ValueContainer()
              {
                Value = newValue
              });
            }));
          }
          else
          {
            formFieldsCache.Graph.FieldSelecting.AddHandler(formFieldsCache.GetItemType(), field.FieldName, (PXFieldSelecting) ((sender, args) =>
            {
              PXFieldState stateForWorkflowForm = this.GetClonedFieldStateForWorkflowForm(canUseCacheCurrentRecord, defaultSchemaState, schemaCache, formFieldsCache, dependedCaches, args, schemaFieldName, currentSchemaState, formDacTypeCache != null);
              PXFieldState pxFieldState5 = stateForWorkflowForm;
              bool? nullable6;
              bool? nullable7;
              if (multiMode)
              {
                nullable6 = (bool?) sender.GetSlot<object>(args.Row, slotFieldUsed);
                nullable7 = !nullable6.GetValueOrDefault() ? new bool?(false) : isFieldRequired;
              }
              else if (formDacTypeCache != null)
              {
                nullable6 = isFieldRequired;
                nullable7 = nullable6 ?? stateForWorkflowForm.Required;
              }
              else
                nullable7 = isFieldRequired;
              pxFieldState5.Required = nullable7;
              if (!string.IsNullOrEmpty(field.DisplayName))
                stateForWorkflowForm.DisplayName = PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowField(field.SchemaField, field.DisplayName);
              stateForWorkflowForm.DefaultValue = fieldDefaultValue ?? (formDacTypeCache == null ? (object) null : stateForWorkflowForm.DefaultValue);
              PXFieldState pxFieldState6 = stateForWorkflowForm;
              int num4;
              if (multiMode)
              {
                nullable6 = (bool?) sender.GetSlot<object>(args.Row, slotFieldUsed);
                num4 = (int) nullable6 ?? (!defaultValueIsFormula ? 1 : 0);
              }
              else
                num4 = 1;
              pxFieldState6.Enabled = num4 != 0;
              stateForWorkflowForm.IsReadOnly = false;
              stateForWorkflowForm.PrimaryKey = false;
              PXFieldState pxFieldState7 = stateForWorkflowForm;
              int num5;
              if (!multiMode)
              {
                if (formDacTypeCache != null)
                {
                  nullable6 = isFieldVisible;
                  num5 = (int) nullable6 ?? (stateForWorkflowForm.Visible ? 1 : 0);
                }
                else
                {
                  nullable6 = isFieldVisible;
                  num5 = (int) nullable6 ?? 1;
                }
              }
              else
                num5 = 1;
              pxFieldState7.Visible = num5 != 0;
              if (args.Row != null && slotId.HasValue)
                stateForWorkflowForm.Value = sender.GetSlot<AUWorkflowFormsEngine.ValueContainer>(args.Row, slotId.Value)?.Value;
              if (formDacTypeCache == null)
                this.PrepareFieldStateView(stateForWorkflowForm, formFieldsCache, dependedCaches, args);
              if (this.Errors.ContainsKey(field.FieldName))
              {
                stateForWorkflowForm.Error = this.Errors[field.FieldName];
                stateForWorkflowForm.ErrorLevel = this.ErrorLevels[field.FieldName];
              }
              else
              {
                stateForWorkflowForm.Error = (string) null;
                stateForWorkflowForm.ErrorLevel = PXErrorLevel.Undefined;
              }
              if (stateForWorkflowForm is PXStringState pxStringState4)
              {
                if (field.ComboBoxValuesSource != "E")
                {
                  IDictionary<string, string> valuesFromStates = this.GetComboboxValuesFromStates(schemaGraph, field, formDefinitionRecord.ActionName, defaultComboboxSourceFieldSchemaState, (object) pxStringState4);
                  if (valuesFromStates != null && valuesFromStates.Any<KeyValuePair<string, string>>())
                    ApplyComboBoxAllowedData(pxStringState4, valuesFromStates);
                }
                else if (!string.IsNullOrEmpty(field.ComboBoxValues))
                  ApplyComboBoxAllowedData(pxStringState4, this.ParseComboBoxValues(field.ComboBoxValues, defaultSchemaState));
                else if (!useCurrentData && defaultSchemaState is PXStringState pxStringState3)
                {
                  pxStringState4.AllowedValues = pxStringState3.AllowedValues;
                  pxStringState4.AllowedLabels = pxStringState3.AllowedLabels;
                  if (!ResourceCollectingManager.IsStringCollecting && pxStringState4.AllowedLabels != null)
                  {
                    string[] neutralAllowedLabels = (string[]) pxStringState4.AllowedLabels.Clone();
                    PXLocalizerRepository.ListLocalizer.Localize(stateForWorkflowForm.Name, schemaCache, neutralAllowedLabels, pxStringState4.AllowedLabels);
                  }
                }
              }
              args.ReturnState = (object) stateForWorkflowForm;
            }));
            formFieldsCache.Graph.FieldUpdating.AddHandler(formFieldsCache.GetItemType(), field.FieldName, (PXFieldUpdating) ((sender, args) =>
            {
              object newValue = args.NewValue;
              string result;
              if (newValue is string label2 && defaultSchemaState is PXStringState pxStringState6 && pxStringState6.TryGetListValue(label2, out result))
                newValue = (object) result;
              if (slotId.HasValue)
              {
                object formsModifiedContext = this.GetValueInFormsModifiedContext<object>(formFieldsCache, dependedCaches, args.Row, (Func<object>) (() =>
                {
                  schemaCache.RaiseFieldUpdating(schemaFieldName, schemaCache.Current, ref newValue);
                  schemaCache.RaiseFieldSelecting(schemaFieldName, schemaCache.Current, ref newValue, false);
                  newValue = PXFieldState.UnwrapValue(newValue);
                  return newValue;
                }));
                args.NewValue = formsModifiedContext;
                sender.SetSlot<AUWorkflowFormsEngine.ValueContainer>(args.Row, slotId.Value, new AUWorkflowFormsEngine.ValueContainer()
                {
                  Value = formsModifiedContext
                });
              }
              else
              {
                formDacTypeCache.SetValueExt(formDacTypeCache.Current, field.FieldName, newValue);
                args.NewValue = newValue;
              }
              this.Errors.Remove(field.FieldName);
              this.ErrorLevels.Remove(field.FieldName);
            }));
            if (!multiMode)
              formFieldsCache.Graph.ExceptionHandling.AddHandler(schemaCache.GetItemType(), schemaFieldName, (PXExceptionHandling) ((_, args) =>
              {
                if (args.Exception == null || slotId.HasValue && formFieldsCache.GetSlot<AUWorkflowFormsEngine.ValueContainer>((object) formDefinitionRecord, slotId.Value) == null)
                  return;
                this.Errors[field.FieldName] = args.Exception.Message;
                this.ErrorLevels[field.FieldName] = PXErrorLevel.Error;
                if (!(args.Exception is PXSetPropertyException exception4))
                  return;
                this.ErrorLevels[field.FieldName] = exception4.ErrorLevel;
                this.Errors[field.FieldName] = exception4.MessageNoPrefix;
              }));
          }
          nullable1 = formDefinitionRecord.Initialized;
          bool flag4 = true;
          if (!(nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue) && fieldDefaultValue != null)
            formFieldsCache.SetValueExt(formFieldsCache.Current, field.FieldName, fieldDefaultValue);
        }
      }
    }
    formDefinitionRecord.Initialized = new bool?(true);

    static void ApplyComboBoxAllowedData(
      PXStringState state,
      IDictionary<string, string> comboBoxValues)
    {
      if (comboBoxValues == null)
        return;
      state.AllowedValues = comboBoxValues.Keys.ToArray<string>();
      state.AllowedLabels = comboBoxValues.Values.ToArray<string>();
    }
  }

  private void FormFieldUsedFieldSelecting(
    AUWorkflowFormField field,
    string fieldUsedName,
    bool defaultValueIsFormula,
    PXFieldSelectingEventArgs args,
    PXCache sender,
    int slotFieldUsed)
  {
    System.Type dataType = typeof (bool);
    bool? isKey = new bool?();
    bool? nullable1 = new bool?(true);
    int? required = new int?();
    int? precision = new int?();
    int? length = new int?();
    bool? nullable2 = field.FromScheme;
    bool flag = true;
    // ISSUE: variable of a boxed type
    __Boxed<bool> defaultValue = (ValueType) (nullable2.GetValueOrDefault() == flag & nullable2.HasValue);
    string fieldName = fieldUsedName;
    nullable2 = new bool?();
    bool? enabled = nullable2;
    nullable2 = new bool?();
    bool? visible = nullable2;
    nullable2 = new bool?();
    bool? readOnly = nullable2;
    PXFieldState instance = PXFieldState.CreateInstance((object) null, dataType, isKey, nullable1, required, precision, length, (object) defaultValue, fieldName, "", "", enabled: enabled, visible: visible, readOnly: readOnly);
    instance.Enabled = defaultValueIsFormula;
    instance.PrimaryKey = false;
    instance.Visible = true;
    if (args.Row != null)
      instance.Value = sender.GetSlot<object>(args.Row, slotFieldUsed) ?? (object) !defaultValueIsFormula;
    args.ReturnState = (object) instance;
  }

  private PXFieldState GetClonedFieldStateForWorkflowForm(
    bool canUseCacheCurrentRecord,
    object defaultSchemaState,
    PXCache schemaCache,
    PXCache formFieldsCache,
    Dictionary<PXCache, List<Tuple<string, int?>>> dependedCaches,
    PXFieldSelectingEventArgs args,
    string schemaFieldName,
    object currentSchemaState,
    bool useSpecialCache)
  {
    object obj;
    if (!useSpecialCache)
    {
      if (!canUseCacheCurrentRecord)
      {
        obj = defaultSchemaState;
      }
      else
      {
        if (!PXWorkflowFormDefaultValueScope.IsWorkflowFormDefaultValueScope() && defaultSchemaState is PXStringState pxStringState)
        {
          string[] allowedValues = pxStringState.AllowedValues;
          if (allowedValues != null && allowedValues.Length > 0)
          {
            obj = this.GetValueInFormsModifiedContext<object>(formFieldsCache, dependedCaches, args.Row, (Func<object>) (() => schemaCache.GetStateExt(schemaCache.Current, schemaFieldName)));
            goto label_8;
          }
        }
        obj = currentSchemaState;
      }
    }
    else
      obj = schemaCache.GetStateExt(schemaCache.Current, schemaFieldName);
label_8:
    return (PXFieldState) ((ICloneable) obj).Clone();
  }

  private IDictionary<string, string> ParseComboBoxValues(
    string comboBoxValues,
    object defaultSchemaState)
  {
    if (comboBoxValues == null)
      return !(defaultSchemaState is PXStringState pxStringState) ? (IDictionary<string, string>) new Dictionary<string, string>() : (IDictionary<string, string>) pxStringState.ValueLabelDic;
    if (comboBoxValues.Contains(","))
      return (IDictionary<string, string>) new PXStringListAttribute(comboBoxValues).ValueLabelDic;
    PXStringState defaultStringState = defaultSchemaState as PXStringState;
    if (defaultStringState == null)
      return (IDictionary<string, string>) null;
    return (IDictionary<string, string>) ((IEnumerable<string>) comboBoxValues.Split(';')).Where<string>((Func<string, bool>) (it => defaultStringState.ValueLabelDic.ContainsKey(it))).ToDictionary<string, string, string>((Func<string, string>) (it => it), (Func<string, string>) (it => defaultStringState.ValueLabelDic[it]));
  }

  private IDictionary<string, string> GetComboboxValuesFromStates(
    PXGraph graph,
    AUWorkflowFormField field,
    string actionName,
    object defaultComboboxSourceFieldSchemaState,
    object currentSchemaState)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    object current = graph.GetPrimaryCache().Current;
    string stateID;
    AUWorkflow workflow = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, current, out stateID);
    (string, AUWorkflow)[] valueTupleArray;
    if (string.IsNullOrEmpty(stateID))
      valueTupleArray = Array.Empty<(string, AUWorkflow)>();
    else
      valueTupleArray = new (string, AUWorkflow)[1]
      {
        (stateID, workflow)
      };
    (string, AUWorkflow)[] source1 = valueTupleArray;
    if (field.ComboBoxValuesSource == "T" || string.IsNullOrEmpty(stateID))
    {
      List<(AUWorkflowTransition, AUWorkflow)> list;
      if (workflow == null)
      {
        IEnumerable<(AUWorkflowTransition, AUWorkflow)> transitions = this._auWorkflowEngine.GetTransitions(actionName, stateID);
        list = transitions != null ? transitions.ToList<(AUWorkflowTransition, AUWorkflow)>() : (List<(AUWorkflowTransition, AUWorkflow)>) null;
      }
      else
      {
        IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(graph, workflow.WorkflowID, workflow.WorkflowSubID, stateID, actionName);
        list = transitionsRecursive != null ? transitionsRecursive.Select<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>((Func<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>) (tr => (tr, workflow))).ToList<(AUWorkflowTransition, AUWorkflow)>() : (List<(AUWorkflowTransition, AUWorkflow)>) null;
      }
      IEnumerable<(AUWorkflowTransition, AUWorkflow)> source2 = (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) list;
      if (source2 == null)
        return (IDictionary<string, string>) null;
      source1 = !(field.ComboBoxValuesSource == "T") ? EnumerableExtensions.Distinct<(string, AUWorkflow), string>(source2.Select<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>) (pair => (pair.transition.FromStateName, pair.workflow))), (Func<(string, AUWorkflow), string>) (pair => pair.FromStateName)).ToArray<(string, AUWorkflow)>() : EnumerableExtensions.Distinct<(string, AUWorkflow), string>(source2.Select<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>) (pair => (pair.transition.TargetStateName, pair.workflow))), (Func<(string, AUWorkflow), string>) (pair => (pair.workflow?.WorkflowGUID ?? Guid.NewGuid().ToString()) + pair.TargetStateName)).ToArray<(string, AUWorkflow)>();
    }
    string fieldName;
    if (!FormFieldHelper.TryGetFieldFromFormFieldName(graph, field.ComboboxAndDefaultSourceField ?? field.SchemaField, out System.Type _, out fieldName))
      return (IDictionary<string, string>) null;
    AUWorkflowStateProperty[] array = ((IEnumerable<(string, AUWorkflow)>) source1).Select<(string, AUWorkflow), IEnumerable<AUWorkflowStateProperty>>((Func<(string, AUWorkflow), IEnumerable<AUWorkflowStateProperty>>) (pair => this._auWorkflowEngine.GetStatePropertiesRecursive(workflow ?? pair.workflow, pair.stateId))).Where<IEnumerable<AUWorkflowStateProperty>>((Func<IEnumerable<AUWorkflowStateProperty>, bool>) (props => props != null)).SelectMany<IEnumerable<AUWorkflowStateProperty>, AUWorkflowStateProperty>((Func<IEnumerable<AUWorkflowStateProperty>, IEnumerable<AUWorkflowStateProperty>>) (props => props)).Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (prop => string.Equals(prop.FieldName, fieldName, StringComparison.OrdinalIgnoreCase))).ToArray<AUWorkflowStateProperty>();
    if (((IEnumerable<AUWorkflowStateProperty>) array).Count<AUWorkflowStateProperty>() >= ((IEnumerable<(string, AUWorkflow)>) source1).Count<(string, AUWorkflow)>())
      return ((IEnumerable<AUWorkflowStateProperty>) array).Select<AUWorkflowStateProperty, IDictionary<string, string>>((Func<AUWorkflowStateProperty, IDictionary<string, string>>) (prop => this.ParseComboBoxValues(prop.ComboBoxValues, defaultComboboxSourceFieldSchemaState))).Aggregate<IDictionary<string, string>, IDictionary<string, string>>((IDictionary<string, string>) new Dictionary<string, string>(), (Func<IDictionary<string, string>, IDictionary<string, string>, IDictionary<string, string>>) ((res, dict) => res.Merge<string, string>(dict)));
    if (currentSchemaState is PXStringState pxStringState1)
      return (IDictionary<string, string>) pxStringState1.ValueLabelDic;
    return !(defaultComboboxSourceFieldSchemaState is PXStringState pxStringState2) ? (IDictionary<string, string>) null : (IDictionary<string, string>) pxStringState2.ValueLabelDic;
  }

  private string GetDefaultValuesFromStates(
    PXGraph graph,
    AUWorkflowFormField field,
    string actionName)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    object current = graph.GetPrimaryCache().Current;
    string stateID;
    AUWorkflow workflow = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, current, out stateID);
    (string, AUWorkflow)[] valueTupleArray;
    if (string.IsNullOrEmpty(stateID))
      valueTupleArray = Array.Empty<(string, AUWorkflow)>();
    else
      valueTupleArray = new (string, AUWorkflow)[1]
      {
        (stateID, workflow)
      };
    (string, AUWorkflow)[] source1 = valueTupleArray;
    if (field.DefaultValueSource == "T" || string.IsNullOrEmpty(stateID))
    {
      List<(AUWorkflowTransition, AUWorkflow)> list;
      if (workflow == null)
      {
        IEnumerable<(AUWorkflowTransition, AUWorkflow)> transitions = this._auWorkflowEngine.GetTransitions(actionName, stateID);
        list = transitions != null ? transitions.ToList<(AUWorkflowTransition, AUWorkflow)>() : (List<(AUWorkflowTransition, AUWorkflow)>) null;
      }
      else
      {
        IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(graph, workflow.WorkflowID, workflow.WorkflowSubID, stateID, actionName);
        list = transitionsRecursive != null ? transitionsRecursive.Select<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>((Func<AUWorkflowTransition, (AUWorkflowTransition, AUWorkflow)>) (tr => (tr, workflow))).ToList<(AUWorkflowTransition, AUWorkflow)>() : (List<(AUWorkflowTransition, AUWorkflow)>) null;
      }
      IEnumerable<(AUWorkflowTransition, AUWorkflow)> source2 = (IEnumerable<(AUWorkflowTransition, AUWorkflow)>) list;
      if (source2 == null)
        return (string) null;
      source1 = !(field.DefaultValueSource == "T") ? EnumerableExtensions.Distinct<(string, AUWorkflow), string>(source2.Select<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>) (pair => (pair.transition.FromStateName, pair.workflow))), (Func<(string, AUWorkflow), string>) (pair => pair.FromStateName)).ToArray<(string, AUWorkflow)>() : EnumerableExtensions.Distinct<(string, AUWorkflow), string>(source2.Select<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>((Func<(AUWorkflowTransition, AUWorkflow), (string, AUWorkflow)>) (pair => (pair.transition.TargetStateName, pair.workflow))), (Func<(string, AUWorkflow), string>) (pair => (pair.workflow?.WorkflowGUID ?? Guid.NewGuid().ToString()) + pair.TargetStateName)).ToArray<(string, AUWorkflow)>();
    }
    string fieldName;
    return FormFieldHelper.TryGetFieldFromFormFieldName(graph, field.ComboboxAndDefaultSourceField ?? field.SchemaField, out System.Type _, out fieldName) ? ((IEnumerable<AUWorkflowStateProperty>) ((IEnumerable<(string, AUWorkflow)>) source1).Select<(string, AUWorkflow), IEnumerable<AUWorkflowStateProperty>>((Func<(string, AUWorkflow), IEnumerable<AUWorkflowStateProperty>>) (pair => this._auWorkflowEngine.GetStatePropertiesRecursive(workflow ?? pair.workflow, pair.stateId))).Where<IEnumerable<AUWorkflowStateProperty>>((Func<IEnumerable<AUWorkflowStateProperty>, bool>) (props => props != null)).SelectMany<IEnumerable<AUWorkflowStateProperty>, AUWorkflowStateProperty>((Func<IEnumerable<AUWorkflowStateProperty>, IEnumerable<AUWorkflowStateProperty>>) (props => props)).Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (prop => string.Equals(prop.FieldName, fieldName, StringComparison.OrdinalIgnoreCase))).ToArray<AUWorkflowStateProperty>()).Select<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (prop => prop.DefaultValue)).FirstOrDefault<string>() : (string) null;
  }

  private void PrepareFieldStateView(
    PXFieldState clonedState,
    PXCache cache,
    Dictionary<PXCache, List<Tuple<string, int?>>> dependedCaches,
    PXFieldSelectingEventArgs args)
  {
    string originalViewName = clonedState.ViewName;
    if (string.IsNullOrEmpty(originalViewName) || originalViewName.StartsWith("WorkflowForm$"))
      return;
    string key = "WorkflowForm$" + originalViewName;
    if (!cache.Graph.Views.ContainsKey(key))
      cache.Graph.Views.Add(key, new PXView(cache.Graph, true, cache.Graph.Views[originalViewName].BqlSelect, (Delegate) (() => (IEnumerable) this.GetValueInFormsModifiedContext<List<object>>(cache, dependedCaches, args.Row, (Func<List<object>>) (() => AUWorkflowFormsEngine.SelectWithinDelegate(cache.Graph.Views[originalViewName]))))));
    clonedState.ViewName = key;
  }

  private T GetValueInFormsModifiedContext<T>(
    PXCache formFieldsCache,
    Dictionary<PXCache, List<Tuple<string, int?>>> dependedCaches,
    object row,
    Func<T> valueGetter)
  {
    List<KeyValuePair<PXCache, object>> caches = new List<KeyValuePair<PXCache, object>>();
    foreach (KeyValuePair<PXCache, List<Tuple<string, int?>>> dependedCach in dependedCaches)
    {
      object data = (object) null;
      PXCache key = dependedCach.Key;
      if (key.Current != null)
      {
        data = key.CreateCopy(key.Current);
      }
      else
      {
        using (new ReadOnlyScope(new PXCache[1]{ key }))
        {
          bool flag;
          try
          {
            data = key.Insert();
            flag = data != null;
            key.Current = data;
          }
          catch
          {
            flag = key.Insert((IDictionary) new Dictionary<string, object>()) > 0;
          }
          if (flag)
            key.SetStatus(key.Current, PXEntryStatus.Notchanged);
        }
      }
      foreach (Tuple<string, int?> tuple in dependedCach.Value)
      {
        object obj = tuple.Item2.HasValue ? formFieldsCache.GetSlot<AUWorkflowFormsEngine.ValueContainer>(row, tuple.Item2.Value)?.Value : (object) null;
        if (key.GetValue(data, tuple.Item1) != obj)
        {
          try
          {
            key.SetValueExt(data, tuple.Item1, obj);
          }
          catch (PXSetPropertyException ex)
          {
            try
            {
              key.SetValue(data, tuple.Item1, obj);
            }
            catch
            {
            }
          }
        }
      }
      caches.Add(new KeyValuePair<PXCache, object>(key, data));
    }
    using (new ReplaceCurrentScope((IEnumerable<KeyValuePair<PXCache, object>>) caches))
      return valueGetter();
  }

  private static List<object> SelectWithinDelegate(PXView view)
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultTruncated = true;
    int startRow = PXView.StartRow;
    int totalRows = 0;
    pxDelegateResult.AddRange((IEnumerable<object>) view.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows));
    return (List<object>) pxDelegateResult;
  }

  public object GetFieldDefaultValue(
    PXGraph schemaGraph,
    FormDefinitionRecord definition,
    AUWorkflowFormField field)
  {
    System.Type dacType;
    string name;
    if (!FormFieldHelper.TryGetFieldFromFormFieldName(schemaGraph, field.SchemaField, out dacType, out name))
      return (object) null;
    PXCache cach = schemaGraph.Caches[dacType];
    object stateExt = cach.GetStateExt(cach.Current, name);
    return this.GetFieldDefaultValue(definition, field, name, stateExt, cach, true, cach.Current);
  }

  public object GetFieldDefaultValue(
    PXGraph schemaGraph,
    FormDefinitionRecord definition,
    AUWorkflowFormField field,
    object row)
  {
    System.Type dacType;
    string name;
    if (!FormFieldHelper.TryGetFieldFromFormFieldName(schemaGraph, field.SchemaField, out dacType, out name))
      return (object) null;
    PXCache cach = schemaGraph.Caches[dacType];
    object stateExt = cach.GetStateExt(row, name);
    return this.GetFieldDefaultValue(definition, field, name, stateExt, cach, true, row);
  }

  private object GetFieldDefaultValue(
    FormDefinitionRecord definition,
    AUWorkflowFormField field,
    string schemaFieldName,
    object schemaState,
    PXCache schemaCache,
    bool useCacheCurrentRecord,
    object row)
  {
    bool? fromScheme1 = field.FromScheme;
    bool flag1 = false;
    if (fromScheme1.GetValueOrDefault() == flag1 & fromScheme1.HasValue || schemaState == null)
    {
      if (field.DefaultValue == null)
        return field.DefaultValueSource != "E" ? (object) this.GetDefaultValuesFromStates(schemaCache.Graph, field, definition.ActionName) : (object) null;
      try
      {
        using (new PXWorkflowFormDefaultValueScope())
          return WorkflowExpressionParser.Eval(schemaCache, schemaFieldName, field.DefaultValue, this.GetOnlyProvidedFormValues(schemaCache.Graph, definition.Screen, definition.FormName), useCacheCurrentRecord ? row : (object) null);
      }
      catch
      {
        bool? fromScheme2 = field.FromScheme;
        bool flag2 = true;
        if (!(fromScheme2.GetValueOrDefault() == flag2 & fromScheme2.HasValue))
          return (object) null;
        SchemaFieldEditors? schemaFieldEditor = field.SchemaFieldEditor;
        SchemaFieldEditors schemaFieldEditors = SchemaFieldEditors.CheckBox;
        return schemaFieldEditor.GetValueOrDefault() == schemaFieldEditors & schemaFieldEditor.HasValue ? (object) Convert.ToBoolean(field.DefaultValue) : (object) field.DefaultValue;
      }
    }
    else
    {
      if (field.DefaultValue == null)
        return field.DefaultValueSource != "E" ? (object) this.GetDefaultValuesFromStates(schemaCache.Graph, field, definition.ActionName) : (object) null;
      object fieldDefaultValue = (object) field.DefaultValue;
      if (schemaCache.GetStateExt((object) null, schemaFieldName) is PXDateState)
        fieldDefaultValue = (object) (RelativeDatesManager.IsRelativeDatesString(field.DefaultValue) ? RelativeDatesManager.EvaluateAsDateTime(field.DefaultValue) : new System.DateTime?(Convert.ToDateTime(field.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture)));
      if (field.DefaultValue != null && field.DefaultValue.Equals("@me", StringComparison.OrdinalIgnoreCase))
        fieldDefaultValue = WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(schemaCache, schemaFieldName);
      if (field.DefaultValue != null && field.DefaultValue.Equals("@branch", StringComparison.OrdinalIgnoreCase))
        fieldDefaultValue = (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch();
      return fieldDefaultValue;
    }
  }

  public IReadOnlyDictionary<string, object> GetFormValues(PXGraph graph)
  {
    string form = (string) null;
    if (graph.Views["FilterPreview"].Cache.Current is FormDefinitionRecord current && current.FormName != null)
      form = current.FormName;
    return form == null ? (IReadOnlyDictionary<string, object>) null : this.GetFormValues(graph, form);
  }

  public IReadOnlyDictionary<string, object> GetFormValues(PXGraph graph, string form)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (IReadOnlyDictionary<string, object>) null;
    return form == null ? (IReadOnlyDictionary<string, object>) null : this.GetFormValues(graph, screenIdFromGraphType, form);
  }

  public IReadOnlyDictionary<string, object> GetFormValues(
    PXGraph graph,
    string screenId,
    string form)
  {
    if (form == null)
      return (IReadOnlyDictionary<string, object>) null;
    PXCache cache = graph.Views["FilterPreview"].Cache;
    if (!(cache.Current is FormDefinitionRecord current) || current.FormName == null)
      return (IReadOnlyDictionary<string, object>) null;
    Dictionary<string, object> formValues = new Dictionary<string, object>();
    string screen = screenId;
    string formName = current.FormName;
    bool? nullable = current.GetFromSession;
    bool flag1 = true;
    int num = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    foreach (AUWorkflowFormField field in this.GetFormDefinition(screen, formName, num != 0).Fields)
    {
      nullable = current.UseMulti;
      bool flag2 = true;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = (bool?) PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName + "_FieldUsed"));
        bool flag3 = true;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          formValues.Add(field.FieldName, PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName)));
        else
          formValues.Add(field.FieldName, this.GetFieldDefaultValue(graph, current, field, graph.GetCurrentPrimaryObject()));
      }
      else
        formValues.Add(field.FieldName, PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName)));
    }
    return (IReadOnlyDictionary<string, object>) formValues;
  }

  public IReadOnlyDictionary<string, object> GetOnlyProvidedFormValues(
    PXGraph graph,
    string screen,
    string form)
  {
    PXCache cache = graph.Views["FilterPreview"].Cache;
    if (!(cache.Current is FormDefinitionRecord current) || current.FormName == null)
      return (IReadOnlyDictionary<string, object>) null;
    Dictionary<string, object> providedFormValues = new Dictionary<string, object>();
    foreach (AUWorkflowFormField field in AUWorkflowFormsEngine.Slot.GetDefinition(screen, current.FormName).Fields)
    {
      bool? nullable = current.UseMulti;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = (bool?) PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName + "_FieldUsed"));
        bool flag2 = true;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          providedFormValues.Add(field.FieldName, PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName)));
      }
      else
        providedFormValues.Add(field.FieldName, PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName)));
    }
    return (IReadOnlyDictionary<string, object>) providedFormValues;
  }

  public IReadOnlyDictionary<string, object> GetFormValuesForMassProcessing(
    PXGraph graph,
    object row,
    string screenId = null)
  {
    string screen = string.IsNullOrEmpty(screenId) ? this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()) : screenId;
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (IReadOnlyDictionary<string, object>) null;
    PXCache cache = graph.Views["FilterPreview"].Cache;
    if (!(cache.Current is FormDefinitionRecord current) || current.FormName == null)
      return (IReadOnlyDictionary<string, object>) null;
    Dictionary<string, object> forMassProcessing = new Dictionary<string, object>();
    foreach (AUWorkflowFormField field in AUWorkflowFormsEngine.Slot.GetDefinition(screen, current.FormName).Fields)
    {
      bool? nullable = (bool?) PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName + "_FieldUsed"));
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        forMassProcessing.Add(field.FieldName, PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field.FieldName)));
      else
        forMassProcessing.Add(field.FieldName, this.GetFieldDefaultValue(graph, current, field, row));
    }
    return (IReadOnlyDictionary<string, object>) forMassProcessing;
  }

  public void InitFormView(PXGraph graph)
  {
    if (graph.Views.ContainsKey("FilterPreview"))
      return;
    AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
  }

  public void ClearFormData(PXGraph graph)
  {
    PXView pxView1;
    string formDacName;
    if (graph.Views.TryGetValue("FilterPreview", out pxView1))
    {
      formDacName = ((FormDefinitionRecord) pxView1.Cache.Current)?.FormDacName;
      foreach (object obj in pxView1.Cache.Cached)
      {
        if (obj != null)
        {
          for (int idx = 0; idx < pxView1.Cache.SlotsCount; ++idx)
            pxView1.Cache.SetSlot<object>(obj, idx, (object) null);
        }
      }
      pxView1.Cache.Clear();
    }
    else
    {
      PXFilter<FormDefinitionRecord> workflowFormFilter = AUWorkflowFormsEngine.CreateNewWorkflowFormFilter(graph);
      formDacName = workflowFormFilter.Current?.FormDacName;
      foreach (object obj in workflowFormFilter.Cache.Cached)
      {
        if (obj != null)
        {
          for (int idx = 0; idx < workflowFormFilter.Cache.SlotsCount; ++idx)
            workflowFormFilter.Cache.SetSlot<object>(obj, idx, (object) null);
        }
      }
      workflowFormFilter.Cache.Clear();
    }
    PXView pxView2;
    if (!string.IsNullOrEmpty(formDacName) && graph.Views.TryGetValue("WorkflowDactTypeForm$" + formDacName, out pxView2))
    {
      pxView2.Cache.Clear();
      pxView2.Cache.Current = (object) null;
    }
    PXContext.SetSlot("Workflow.CurrentWorkflowFormData", (object) null);
  }

  private static PXFilter<FormDefinitionRecord> CreateNewWorkflowFormFilter(PXGraph graph)
  {
    PXFilter<FormDefinitionRecord> workflowFormFilter = new PXFilter<FormDefinitionRecord>(graph);
    graph.Views.Add("FilterPreview", (PXSelectBase) workflowFormFilter);
    graph.Views["FilterPreview"]._Cache = graph.Caches[typeof (FormDefinitionRecord)];
    return workflowFormFilter;
  }

  public void SetFormValues(
    PXGraph graph,
    string form,
    IDictionary<string, object> values,
    bool useMulti = false)
  {
    this.ClearFormData(graph);
    if (form == null)
      return;
    PX.Data.ProjectDefinition.Workflow.FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()), form);
    PXCache cache = graph.Views["FilterPreview"].Cache;
    FormDefinitionRecord data = this.InsertFormDefinitionRecord(definition, cache, useMulti);
    this.OnInit(graph, true);
    if (values == null)
      return;
    foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) values)
    {
      string[] strArray = keyValuePair.Key.Split('!');
      string fieldName = strArray[0];
      if (strArray.Length == 2)
      {
        if (cache.GetStateExt((object) data, fieldName) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.ViewName) && !string.IsNullOrEmpty(stateExt.ValueField))
        {
          PXView view = graph.Views[stateExt.ViewName];
          int startRow = 0;
          int totalRows = 0;
          object obj = view.Select((object[]) null, (object[]) null, new object[1]
          {
            keyValuePair.Value
          }, new string[1]{ strArray[1] }, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows).FirstOrDefault<object>();
          if (obj != null)
            obj = (object) PXResult.UnwrapMain(obj);
          if (obj != null)
            cache.SetValueExt((object) data, fieldName, view.Cache.GetValue(obj, stateExt.ValueField));
        }
      }
      else
        cache.SetValueExt((object) data, fieldName, keyValuePair.Value);
      if (useMulti)
        cache.SetValueExt((object) data, fieldName + "_FieldUsed", (object) true);
    }
  }

  public IEnumerable<AUWorkflowFormField> GetScreenFields(string screen)
  {
    return (IEnumerable<AUWorkflowFormField>) AUWorkflowFormsEngine.Slot.GetScreenFields(screen) ?? (IEnumerable<AUWorkflowFormField>) Array.Empty<AUWorkflowFormField>();
  }

  public Dictionary<string, string> GetErrors() => this.Errors;

  public bool IsFormCustomized(string screenId)
  {
    return AUWorkflowFormsEngine.Slot.LocallyCachedSlot.IsCustomized(screenId);
  }

  private Dictionary<string, string> Errors
  {
    get
    {
      Dictionary<string, string> errors = PXContext.GetSlot<Dictionary<string, string>>("Workflow.Errors");
      if (errors == null)
      {
        errors = new Dictionary<string, string>();
        PXContext.SetSlot<Dictionary<string, string>>("Workflow.Errors", errors);
      }
      return errors;
    }
  }

  private Dictionary<string, PXErrorLevel> ErrorLevels
  {
    get
    {
      Dictionary<string, PXErrorLevel> errorLevels = PXContext.GetSlot<Dictionary<string, PXErrorLevel>>("Workflow.ErrorLevels");
      if (errorLevels == null)
      {
        errorLevels = new Dictionary<string, PXErrorLevel>();
        PXContext.SetSlot<Dictionary<string, PXErrorLevel>>("Workflow.ErrorLevels", errorLevels);
      }
      return errorLevels;
    }
  }

  [PXHidden]
  private class DummyFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  protected class ValueContainer
  {
    public object Value;
  }

  [PXHidden]
  public class WorkflowView : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    public string Key { get; set; }

    [PXString]
    public string Layout { get; set; }

    public abstract class layout : IBqlField, IBqlOperand
    {
    }
  }

  private sealed class SessionState : PXSessionState
  {
    private const string Key = "Workflow.TestFormDefinition";

    internal PX.Data.ProjectDefinition.Workflow.FormDefinition TestFormDefinition
    {
      get => PXSessionState.GetValue("Workflow.TestFormDefinition") as PX.Data.ProjectDefinition.Workflow.FormDefinition;
      set => PXSessionState.SetValue("Workflow.TestFormDefinition", (object) value);
    }
  }

  private class ScreenForms
  {
    public Dictionary<string, PX.Data.ProjectDefinition.Workflow.FormDefinition> Items;
    public AUWorkflowFormField[] ScreenFields;
    public bool IsCustomized;
  }

  public class Slot : IPrefetchable, IPXCompanyDependent
  {
    private readonly ConcurrentDictionary<string, AUWorkflowFormsEngine.ScreenForms> _screenForms = new ConcurrentDictionary<string, AUWorkflowFormsEngine.ScreenForms>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public static AUWorkflowFormsEngine.Slot GetSlot()
    {
      return PXDatabase.GetSlot<AUWorkflowFormsEngine.Slot>("FormDefinition", ((IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes()).Union<System.Type>((IEnumerable<System.Type>) new System.Type[3]
      {
        typeof (AUWorkflowForm),
        typeof (AUWorkflowFormField),
        typeof (PXGraph.FeaturesSet)
      }).ToArray<System.Type>());
    }

    public static AUWorkflowFormsEngine.Slot LocallyCachedSlot
    {
      get
      {
        return PXContext.GetSlot<AUWorkflowFormsEngine.Slot>("FormDefinition") ?? PXContext.SetSlot<AUWorkflowFormsEngine.Slot>("FormDefinition", AUWorkflowFormsEngine.Slot.GetSlot());
      }
    }

    public static PX.Data.ProjectDefinition.Workflow.FormDefinition GetDefinition(
      string screen,
      string form)
    {
      if (screen == null)
        return (PX.Data.ProjectDefinition.Workflow.FormDefinition) null;
      AUWorkflowFormsEngine.Slot locallyCachedSlot = AUWorkflowFormsEngine.Slot.LocallyCachedSlot;
      string key = screen + form;
      string screenID = screen;
      PX.Data.ProjectDefinition.Workflow.FormDefinition definition;
      locallyCachedSlot.Get(screenID).Items.TryGetValue(key, out definition);
      return definition;
    }

    public static AUWorkflowFormField[] GetScreenFields(string screen)
    {
      return screen == null ? (AUWorkflowFormField[]) null : AUWorkflowFormsEngine.Slot.LocallyCachedSlot.Get(screen).ScreenFields;
    }

    private AUWorkflowFormsEngine.ScreenForms Get(string screenID)
    {
      return screenID != null ? this._screenForms.GetOrAdd(screenID, new Func<string, AUWorkflowFormsEngine.ScreenForms>(this.LoadForScreen)) : throw new ArgumentNullException(nameof (screenID));
    }

    public bool IsCustomized(string screenID) => this.Get(screenID).IsCustomized;

    public void Prefetch()
    {
    }

    private AUWorkflowFormsEngine.ScreenForms LoadForScreen(string screenID)
    {
      bool isCustomized = false;
      AUWorkflowForm[] array = PXSystemWorkflows.SelectTable<AUWorkflowForm>(screenID, ref isCustomized).ToArray<AUWorkflowForm>();
      Dictionary<string, AUWorkflowFormField[]> dictionary1 = ((IEnumerable<AUWorkflowForm>) array).Join((IEnumerable<AUWorkflowFormField>) PXSystemWorkflows.SelectTable<AUWorkflowFormField>(screenID, ref isCustomized).ToArray<AUWorkflowFormField>(), w => new
      {
        Screen = w.Screen,
        FormName = w.FormName
      }, s => new
      {
        Screen = s.Screen,
        FormName = s.FormName
      }, (w, s) => new{ Form = w, Field = s }).Where(it =>
      {
        bool? isSystem1 = it.Form.IsSystem;
        bool flag1 = true;
        if (isSystem1.GetValueOrDefault() == flag1 & isSystem1.HasValue)
          return true;
        bool? isSystem2 = it.Field.IsSystem;
        bool flag2 = false;
        return isSystem2.GetValueOrDefault() == flag2 & isSystem2.HasValue;
      }).GroupBy(it => it.Form.Screen + it.Form.FormName).ToDictionary<IGrouping<string, \u003C\u003Ef__AnonymousType84<AUWorkflowForm, AUWorkflowFormField>>, string, AUWorkflowFormField[]>(it => it.Key, it => it.Select(i => i.Field).OrderBy<AUWorkflowFormField, int?>((Func<AUWorkflowFormField, int?>) (f => f.LineNumber)).ToArray<AUWorkflowFormField>());
      Dictionary<string, PX.Data.ProjectDefinition.Workflow.FormDefinition> dictionary2 = new Dictionary<string, PX.Data.ProjectDefinition.Workflow.FormDefinition>(array.Length, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (AUWorkflowForm auWorkflowForm in array)
      {
        string key = auWorkflowForm.Screen + auWorkflowForm.FormName;
        AUWorkflowFormField[] source;
        if (!dictionary1.TryGetValue(key, out source))
          source = Array.Empty<AUWorkflowFormField>();
        PX.Data.ProjectDefinition.Workflow.FormDefinition formDefinition = new PX.Data.ProjectDefinition.Workflow.FormDefinition()
        {
          Form = auWorkflowForm,
          Fields = source != null ? ((IEnumerable<AUWorkflowFormField>) source).ToArray<AUWorkflowFormField>() : (AUWorkflowFormField[]) null,
          Key = key,
          FormName = auWorkflowForm.FormName,
          Screen = auWorkflowForm.Screen,
          DacType = auWorkflowForm.DacType
        };
        dictionary2.Add(key, formDefinition);
      }
      return new AUWorkflowFormsEngine.ScreenForms()
      {
        Items = dictionary2,
        ScreenFields = dictionary1.SelectMany<KeyValuePair<string, AUWorkflowFormField[]>, AUWorkflowFormField>((Func<KeyValuePair<string, AUWorkflowFormField[]>, IEnumerable<AUWorkflowFormField>>) (it => (IEnumerable<AUWorkflowFormField>) it.Value)).GroupBy<AUWorkflowFormField, string>((Func<AUWorkflowFormField, string>) (f => f.FieldName), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Select<IGrouping<string, AUWorkflowFormField>, AUWorkflowFormField>((Func<IGrouping<string, AUWorkflowFormField>, AUWorkflowFormField>) (f => f.First<AUWorkflowFormField>())).ToArray<AUWorkflowFormField>(),
        IsCustomized = isCustomized
      };
    }
  }
}
