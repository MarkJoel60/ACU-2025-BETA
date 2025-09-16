// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXWorkflowService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac.Features.AttributeFilters;
using Newtonsoft.Json.Linq;
using PX.Api;
using PX.Async.Internal;
using PX.Common;
using PX.Data.Automation.Services;
using PX.Data.Automation.State;
using PX.Data.BusinessProcess;
using PX.Data.LocalizationKeyGenerators;
using PX.Data.Process.Automation;
using PX.Data.Process.Automation.State;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.WorkflowAPI;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Automation;

internal class PXWorkflowService : ILongOperationWorkflowAdapter
{
  public static readonly PXWorkflowService.SlotIndexerByGraph<bool> RunAutoActions = new PXWorkflowService.SlotIndexerByGraph<bool>(nameof (RunAutoActions));
  public static readonly PXWorkflowService.SlotIndexerByGraph<bool> SaveAfterAction = new PXWorkflowService.SlotIndexerByGraph<bool>(nameof (SaveAfterAction));
  public static readonly PXWorkflowService.SlotIndexerByGraph<bool> PreventSaveOnAction = new PXWorkflowService.SlotIndexerByGraph<bool>(nameof (PreventSaveOnAction));
  internal static readonly PXWorkflowService.SlotIndexerByGraph<List<string>> AppliedAutoActions = new PXWorkflowService.SlotIndexerByGraph<List<string>>(nameof (AppliedAutoActions));
  internal const string ActionMenuName = "Action";
  internal const string InquiriesMenuName = "Inquiry";
  internal const string ReportsMenuName = "Report";
  private const string InitialPseudoStateSuffix = "@Initial";
  private const string FinalPseudoStateSuffix = "@Final";
  internal const string ActionMenuDisplayName = "Actions";
  internal const string InquiriesMenuDisplayName = "Inquiries";
  internal const string ReportMenuDisplayName = "Reports";
  private const string PREVENT_RECURSION_IN_CONDITIONS = "Workflow.PreventRecursionInCalculation";
  internal const string OperationCompletedInTransaction = "Workflow.OperationCompletedInTransaction";
  internal const string NonBatchModeProcessing = "Workflow.NonBatchModeProcessing";
  private const string PrimaryWorkflowItemPersisted = "Workflow.PrimaryWorkflowItemPersisted";
  public const string ALLOW_WORKFLOW_EXTENDED_COMBO_BOX_VALUES = "AllowWorkflowExtendedComboBoxValues";
  private const string RUN_AUTO_ACTIONS = "RunAutoActions";
  private const string SAVE_AFTER_ACTION = "SaveAfterAction";
  private const string PREVENT_SAVE_ON_ACTION = "PreventSaveOnAction";
  private const string APPLIED_AUTO_ACTIONS = "AppliedAutoActions";
  internal const string CURRENT_WORKFLOW_ACTION = "Workflow.CurrentWorkflowAction";
  internal const string LONG_RUN_WORKFLOW_ACTION = "Workflow.LongRunWorkflowAction";
  internal const string CURRENT_WORKFLOW_OBJECT = "Workflow.CurrentWorkflowObject";
  internal const string LONG_RUN_WORKFLOW_OBJECT = "Workflow.LongRunWorkflowObject";
  internal const string MASS_PROCESSING_WORKFLOW_OBJECT = "Workflow.MassProcessingWorkflowObject";
  internal const string CURRENT_WORKFLOW_FORM_DATA = "Workflow.CurrentWorkflowFormData";
  internal const string LONG_RUN_WORKFLOW_FORM_DATA = "Workflow.LongRunWorkflowFormData";
  internal const string CURRENT_ACTION_VIEWS_DATA = "Workflow.CurrentActionViewsData";
  internal const string LONG_RUN_ACTION_VIEWS_DATA = "Workflow.LongRunActionViewsData";
  internal const string CURRENT_WORKFLOW_OBJECT_TYPE = "Workflow.CurrentWorkflowObjectType";
  internal const string LONG_RUN_WORKFLOW_OBJECT_TYPE = "Workflow.LongRunWorkflowObjectType";
  internal const string CURRENT_WORKFLOW_SCREEN_ID = "Workflow.CurrentWorkflowScreenId";
  internal const string LONG_RUN_WORKFLOW_SCREEN_ID = "Workflow.LongRunWorkflowScreenId";
  internal const string CURRENT_WORKFLOW_SHOULD_BE_EXECUTED = "Workflow.CurrentWorkflowShouldBeExecuted";
  internal const string LONG_RUN_WORKFLOW_SHOULD_BE_EXECUTED = "Workflow.LongRunWorkflowShouldBeExecuted";
  internal const string SUPPRESS_COMPLETION = "Workflow.SuppressCompletion";
  internal const string PRIMARY_CACHE_ROW_INSERTED = "PrimaryCacheRowInserted";
  internal System.Action OnReuseInitialize;
  private readonly Func<IScreenMap> _confFactory;
  private readonly INavigationService _navigationService;
  private readonly IAUWorkflowEngine _auWorkflowEngine;
  private readonly IAUWorkflowFormsEngine _auWorkflowFormsEngine;
  private readonly IWorkflowConditionEvaluateService _workflowConditionEvaluateService;
  private readonly IAUWorkflowEventsEngine _auWorkflowEventsEngine;
  private readonly IAUWorkflowActionsEngine _workflowActionsEngine;
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;
  private readonly IPXIdentityAccessor _pxIdentityAccessor;
  private readonly IPXPageIndexingService _pageIndexingService;
  private readonly IBusinessProcessEventProcessor _businessProcessEventProcessor;
  private readonly PXSiteMapProvider _siteMapProvider;
  private readonly INavigationExpressionEvaluator _workflowFieldValueEvaluator;
  private readonly IExtraActionHanlderService _extraActionRunHandlerService;

  private IScreenMap Configuration => this._confFactory() ?? (IScreenMap) new SimpleScreenMap();

  public PXWorkflowService(
    Func<IScreenMap> confFactory,
    [KeyFilter("Automation")] INavigationService navigationService,
    IAUWorkflowEngine auWorkflowEngine,
    IAUWorkflowFormsEngine auWorkflowFormsEngine,
    IWorkflowConditionEvaluateService workflowConditionEvaluateService,
    IAUWorkflowEventsEngine auWorkflowEventsEngine,
    IAUWorkflowActionsEngine workflowActionsEngine,
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService,
    IPXIdentityAccessor pxIdentityAccessor,
    IPXPageIndexingService pageIndexingService,
    IBusinessProcessEventProcessor businessProcessEventProcessor,
    PXSiteMapProvider siteMapProvider,
    [KeyFilter("WorkflowFieldExpressionEvaluator")] INavigationExpressionEvaluator workflowFieldValueEvaluator,
    IExtraActionHanlderService extraActionRunHandlerService)
  {
    this._confFactory = confFactory ?? throw new ArgumentNullException(nameof (confFactory));
    this._navigationService = navigationService ?? throw new ArgumentNullException(nameof (navigationService));
    this._auWorkflowEngine = auWorkflowEngine ?? throw new ArgumentNullException(nameof (auWorkflowEngine));
    this._auWorkflowFormsEngine = auWorkflowFormsEngine ?? throw new ArgumentNullException(nameof (auWorkflowFormsEngine));
    this._workflowConditionEvaluateService = workflowConditionEvaluateService;
    this._auWorkflowEventsEngine = auWorkflowEventsEngine ?? throw new ArgumentNullException(nameof (auWorkflowEventsEngine));
    this._workflowActionsEngine = workflowActionsEngine ?? throw new ArgumentNullException(nameof (workflowActionsEngine));
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
    this._pxIdentityAccessor = pxIdentityAccessor;
    this._pageIndexingService = pageIndexingService;
    this._businessProcessEventProcessor = businessProcessEventProcessor ?? throw new ArgumentNullException(nameof (businessProcessEventProcessor));
    this._siteMapProvider = siteMapProvider;
    this._workflowFieldValueEvaluator = workflowFieldValueEvaluator;
    this._extraActionRunHandlerService = extraActionRunHandlerService;
  }

  private bool IsAllowedToExecute(PXGraph graph)
  {
    return this.IsUserAuthenticated() && graph != null && !this.IgnoreGraph(graph);
  }

  public bool IsEnabled(PXGraph graph)
  {
    return this.IsAllowedToExecute(graph) && this.Configuration.ContainsGraph(CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName);
  }

  public bool IsWorkflowExists(PXGraph graph)
  {
    return this.IsAllowedToExecute(graph) && this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _) != null;
  }

  public bool IsWorkflowExists(PXGraph graph, object row)
  {
    return this.IsAllowedToExecute(graph) && this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out string _) != null;
  }

  public bool IsScreenConfigurationCustomized(PXGraph graph)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return false;
    Screen byScreen = this.Configuration.GetByScreen(screenIdFromGraphType);
    return (byScreen != null ? (byScreen.IsCustomized ? 1 : 0) : 0) != 0 || this._auWorkflowEngine.IsWorkflowCustomized(screenIdFromGraphType) || this._auWorkflowFormsEngine.IsFormCustomized(screenIdFromGraphType) || this._workflowActionsEngine.IsActionsCustomized(screenIdFromGraphType);
  }

  private bool IgnoreGraph(PXGraph g)
  {
    System.Type type = g.GetType();
    System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(g);
    PXDisableWorkflowAttribute customAttribute = (PXDisableWorkflowAttribute) Attribute.GetCustomAttribute((MemberInfo) typeNotCustomized, typeof (PXDisableWorkflowAttribute));
    bool flag1 = customAttribute != null;
    bool flag2 = customAttribute != null && customAttribute.AlsoDisableForCustomizedGraph;
    if (type == typeof (PXGraph) | flag2)
      return true;
    return flag1 && type == typeNotCustomized;
  }

  public bool IsWorkflowDefinitionDefined(PXGraph graph)
  {
    return this.IsAllowedToExecute(graph) && this._auWorkflowEngine.GetScreenWorkflows(graph) != null;
  }

  public bool WorkflowHasSequences(PXGraph graph)
  {
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _);
    if (workflowAndState == null)
      return false;
    IEnumerable<AUWorkflowState> states = this._auWorkflowEngine.GetStates(workflowAndState);
    return states != null && states.Any<AUWorkflowState>((Func<AUWorkflowState, bool>) (s => !string.IsNullOrEmpty(s.StateType)));
  }

  public void Configure(PXGraph graph)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (graph.WorkflowID != null || graph.WorkflowStepID != null)
      return;
    System.Type type1 = graph.GetType();
    string graphName = CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName;
    PXCache primaryCache = graph.Caches[graph.PrimaryItemType];
    bool flag = this.IsWorkflowDefinitionDefined(graph);
    if (flag)
    {
      graph.RowInserted.AddHandler(primaryCache.GetItemType(), (PXRowInserted) ((sender, args) =>
      {
        graph.WorkflowID = graph.WorkflowStepID = "PrimaryCacheRowInserted";
        if (args.Row == null)
          return;
        string stateID;
        AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(sender, args.Row, out stateID);
        if (workflowAndState == null)
          return;
        AUWorkflowState initialState = this._auWorkflowEngine.GetInitialState(graph, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID);
        if (initialState.Identifier != stateID)
        {
          AUWorkflowDefinition screenWorkflows = this._auWorkflowEngine.GetScreenWorkflows(graph);
          primaryCache.SetValueExt(args.Row, screenWorkflows.StateField, (object) initialState.Identifier);
          this.ApplyWorkflowState(graph, args.Row);
        }
        this.ApplyAutoActions(graph, args.Row);
      }));
      graph.RowSelected.AddHandler(primaryCache.GetItemType(), (PXRowSelected) ((sender, args) =>
      {
        if (args.Row == null)
          return;
        string stateID;
        AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID);
        if (graph.WorkflowID == $"{workflowAndState?.WorkflowID ?? "DEF_WORKFLOW_ID"}@{workflowAndState?.WorkflowSubID ?? "DEF_WORKFLOW_ID"}" && graph.WorkflowStepID == stateID)
          return;
        this.ApplyWorkflowState(graph, args.Row);
      }));
      this.SetWorkflowViewerJSON(graph);
      AUWorkflowDefinition wd = this._auWorkflowEngine.GetScreenWorkflows(graph);
      if (wd != null)
      {
        Exception initException = PXSystemWorkflows.CheckErrors(graph);
        if (initException != null)
          graph.FieldSelecting.AddHandler(primaryCache.GetItemType(), wd.StateField, (PXFieldSelecting) ((sender, args) => sender.RaiseExceptionHandling(wd.StateField, args.Row, args.ReturnValue, initException)));
        graph.FieldDefaulting.AddHandler(primaryCache.GetItemType(), wd.StateField, (PXFieldDefaulting) ((sender, args) =>
        {
          AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _);
          if (workflowAndState == null)
            return;
          AUWorkflowState initialState = this._auWorkflowEngine.GetInitialState(graph, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID);
          if (initialState == null)
            return;
          args.NewValue = (object) initialState.Identifier;
        }));
        if (!string.IsNullOrEmpty(wd.FlowTypeField))
        {
          bool useSubType = !string.IsNullOrEmpty(wd.FlowSubTypeField);
          if (useSubType)
            graph.FieldUpdated.AddHandler(primaryCache.GetItemType(), wd.FlowSubTypeField, new PXFieldUpdated(FlowSubTypeFieldUpdated));
          graph.FieldUpdated.AddHandler(primaryCache.GetItemType(), wd.FlowTypeField, new PXFieldUpdated(FlowTypeFieldUpdated));
          graph.RowUpdated.AddHandler(primaryCache.GetItemType(), new PXRowUpdated(RowUpdated));

          void FlowTypeFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs args)
          {
            string flowType = (string) primaryCache.GetValue(args.Row, wd.FlowTypeField);
            if (flowType == null || flowType == (string) args.OldValue)
              return;
            if (useSubType)
            {
              string flowSubType = (string) primaryCache.GetValue(args.Row, wd.FlowSubTypeField);
              if (flowSubType == null)
                return;
              ResetToInitialState(args.Row, flowType, flowSubType);
            }
            else
              ResetToInitialState(args.Row, flowType, (string) null);
          }

          void RowUpdated(PXCache sender, PXRowUpdatedEventArgs args)
          {
            string flowType = (string) primaryCache.GetValue(args.Row, wd.FlowTypeField);
            if (flowType == null)
              return;
            string str1 = (string) primaryCache.GetValue(args.OldRow, wd.FlowTypeField);
            if (str1 == null)
              return;
            if (useSubType)
            {
              string flowSubType = (string) primaryCache.GetValue(args.Row, wd.FlowSubTypeField);
              if (flowSubType == null)
                return;
              string str2 = (string) primaryCache.GetValue(args.OldRow, wd.FlowSubTypeField);
              if (str2 == null || !(flowType != str1) && !(flowSubType != str2))
                return;
              ResetToInitialState(args.Row, flowType, flowSubType);
            }
            else
            {
              if (!(flowType != str1))
                return;
              ResetToInitialState(args.Row, flowType, (string) null);
            }
          }
        }
        graph.RowUpdated.AddHandler(primaryCache.GetItemType(), new PXRowUpdated(FirePropertyChangedHandler));
        string stateID;
        AUWorkflow workflowAndState1 = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID);
        if (!string.IsNullOrEmpty(stateID))
        {
          this._auWorkflowEngine.InitStateProperties(graph, workflowAndState1.WorkflowID, workflowAndState1.WorkflowSubID, stateID);
        }
        else
        {
          AUWorkflowState initialState = workflowAndState1 != null ? this._auWorkflowEngine.GetInitialState(graph, workflowAndState1.WorkflowID, workflowAndState1.WorkflowSubID) : (AUWorkflowState) null;
          if (initialState != null)
            this._auWorkflowEngine.InitStateProperties(graph, workflowAndState1.WorkflowID, workflowAndState1.WorkflowSubID, initialState.Identifier);
        }
        foreach (string workflowStateCacheType in this._auWorkflowEngine.GetWorkflowStateCacheTypes(graph))
        {
          System.Type type2 = GraphHelper.GetType(workflowStateCacheType);
          graph.RowPersisting.AddHandler(type2, new PXRowPersisting(this.WorkflowRowPersisting));
        }
        graph.RowPersisting.AddHandler(graph.PrimaryItemType, new PXRowPersisting(this.CheckNonPersistentStateWhilePersisting));
      }

      void FlowSubTypeFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs args)
      {
        string flowType = (string) primaryCache.GetValue(args.Row, wd.FlowTypeField);
        if (flowType == null)
          return;
        string flowSubType = (string) primaryCache.GetValue(args.Row, wd.FlowSubTypeField);
        if (flowSubType == null || flowSubType == (string) args.OldValue)
          return;
        ResetToInitialState(args.Row, flowType, flowSubType);
      }
    }
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(type1);
    Screen screen = screenIdFromGraphType != null ? this.Configuration.GetByScreen(screenIdFromGraphType) : (graphName != null ? this.Configuration.GetByGraph(graphName) : (Screen) null);
    if (screen != null)
    {
      this.AddActions(graph, screen);
      this._auWorkflowFormsEngine.InitFormView(graph);
      PXCache primaryViewCache = graph.Views[graph.PrimaryView].Cache;
      foreach (StateMap<ScreenTable>.NamedState namedState in screen.Tables.GetList())
      {
        System.Type type3 = PXBuildManager.GetType(namedState.Name, true, true);
        graph.RowPersisting.AddHandler(type3, new PXRowPersisting(this.RowPersisting));
        PXCache cach = graph.Caches[namedState.Value.CacheType];
        List<ScreenTableField> fields = ((IEnumerable<StateMap<ScreenTableField>.NamedState>) namedState.Value.Fields.GetList()).Select<StateMap<ScreenTableField>.NamedState, ScreenTableField>((Func<StateMap<ScreenTableField>.NamedState, ScreenTableField>) (s => s.Value)).ToList<ScreenTableField>();
        PXCache.FieldOfRowDefaultingDelegate defaultingDelegate = (PXCache.FieldOfRowDefaultingDelegate) ((PXCache sender, string field, object defRow, ref object value) => this.FieldDefaulting((IEnumerable<ScreenTableField>) fields, primaryViewCache, field, defRow, ref value));
        cach.WorkflowFieldDefaulting = defaultingDelegate;
      }
    }
    if (flag)
    {
      object current = graph.Views[graph.PrimaryView].Cache.Current;
      this._workflowActionsEngine.InitActions(graph, current, screen);
      graph.OnAfterPersist += new System.Action<PXGraph>(this.Graph_OnAfterPersist);
    }
    else
    {
      if (!this._workflowActionsEngine.HasActionsWithFormOrFieldUpdates(graph))
        return;
      object current = graph.Views[graph.PrimaryView].Cache.Current;
      this._workflowActionsEngine.InitActionsWithoutWorkflow(graph, current, screen);
      graph.OnAfterPersist += new System.Action<PXGraph>(this.Graph_OnAfterPersist);
    }

    void FirePropertyChangedHandler(PXCache sender, PXRowUpdatedEventArgs args)
    {
      IEnumerable<string[]> cachePropertyEvents = this._auWorkflowEventsEngine.GetCachePropertyEvents(primaryCache.GetItemType().FullName);
      string[][] array = cachePropertyEvents != null ? cachePropertyEvents.ToArray<string[]>() : (string[][]) null;
      if (array != null && ((IEnumerable<string[]>) array).Any<string[]>())
        this.TryFireOnPropertyChangedEvent(array, sender, args);
      else
        graph.RowUpdated.RemoveHandler(primaryCache.GetItemType(), new PXRowUpdated(FirePropertyChangedHandler));
    }

    void ResetToInitialState(object row, string flowType, string flowSubType)
    {
      // ISSUE: reference to a compiler-generated field
      string stateId = (string) closure_0.primaryCache.GetValue(row, wd.StateField);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if ((closure_0.primaryCache.Current == null || closure_0.primaryCache.GetStatus(closure_0.primaryCache.Current) != PXEntryStatus.Inserted) && closure_0.\u003C\u003E4__this._auWorkflowEngine.GetState(closure_0.graph, flowType, flowSubType, stateId) != null)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AUWorkflowState initialState = closure_0.\u003C\u003E4__this._auWorkflowEngine.GetInitialState(closure_0.graph, flowType, flowSubType);
      if (initialState != null)
      {
        // ISSUE: reference to a compiler-generated field
        closure_0.primaryCache.SetValueExt(row, wd.StateField, (object) initialState.Identifier);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        closure_0.\u003C\u003E4__this._auWorkflowEngine.InitStateProperties(closure_0.graph, flowType, flowSubType, initialState.Identifier);
      }
      // ISSUE: reference to a compiler-generated field
      PXWorkflowService.RunAutoActions[closure_0.graphName] = true;
    }
  }

  private void TryFireOnPropertyChangedEvent(
    string[][] events,
    PXCache sender,
    PXRowUpdatedEventArgs args)
  {
    if (PXRowUpdatedWorkflowScope.IsInScope())
      return;
    using (new PXRowUpdatedWorkflowScope())
    {
      foreach (string[] source in events)
      {
        if (((IEnumerable<string>) source).Any<string>((Func<string, bool>) (property => !object.Equals(PXFieldState.UnwrapValue(sender.GetValueExt(args.Row, property)), PXFieldState.UnwrapValue(sender.GetValueExt(args.OldRow, property))))))
          this.FireEvent(sender.Graph, $"@{sender.GetItemType()?.ToString()}PropertyChanged", string.Join("|", source), args.Row as IBqlTable, (object) null, sender.GetItemType());
      }
    }
  }

  private void Graph_OnAfterPersist(PXGraph graph)
  {
    PXCache cache = graph.Caches[graph.PrimaryItemType];
    if (this._workflowActionsEngine.CurrentWorkflowObjectKeys != null && cache.Current != null)
    {
      this._workflowActionsEngine.CurrentWorkflowObjectKeys = (IDictionary) cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (cacheKey => cacheKey), (Func<string, object>) (cacheKey => cache.GetValue(cache.Current, cacheKey)));
      this._workflowActionsEngine.CurrentWorkflowObjectType = cache.BqlTable;
    }
    string currentWorkflowAction = this._workflowActionsEngine.CurrentWorkflowAction;
    try
    {
      this.ApplyAutoActions(graph, cache.Current);
    }
    finally
    {
      this._workflowActionsEngine.CurrentWorkflowAction = currentWorkflowAction;
    }
  }

  public void ApplyWorkflowState(PXGraph graph, object row)
  {
    if (PXGraph.ProxyIsActive)
      return;
    string str = graph != null ? CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName : throw new ArgumentNullException(nameof (graph));
    if (this.IgnoreGraph(graph))
      return;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out stateID);
    bool flag = workflowAndState != null;
    if (flag)
    {
      foreach (PXAction pxAction in (IEnumerable) graph.Actions.Values)
      {
        pxAction.AutomationDisabled = false;
        pxAction.AutomationHidden = false;
        pxAction.WorkflowHiddenOnMainToolbar = false;
      }
      foreach (PXCache pxCache in graph.Caches.Values)
      {
        pxCache.AutomationDeleteDisabled = false;
        pxCache.AutomationInsertDisabled = false;
        pxCache.AutomationUpdateDisabled = false;
        pxCache.AutomationHidden = false;
      }
    }
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    Screen screen = screenIdFromGraphType != null ? this.Configuration.GetByScreen(screenIdFromGraphType) : (str != null ? this.Configuration.GetByGraph(str) : (Screen) null);
    if (screen != null)
    {
      foreach (PXCache pxCache in graph.Caches.Values)
        pxCache.AutomationFieldSelecting = (PXCache.FieldSelectingDelegate) null;
      IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, row, screen, (string) null);
      this.AlterActions(graph, screen, conditions, row, workflowAndState, stateID);
      this.AlterFields(graph, screen, conditions, row, workflowAndState, stateID);
    }
    if (!flag || !PXWorkflowService.RunAutoActions[str])
      return;
    PXWorkflowService.RunAutoActions[str] = false;
    this.ApplyAutoActions(graph, row);
  }

  private IReadOnlyDictionary<string, Lazy<bool>> EvaluateConditions(
    PXGraph graph,
    object row,
    Screen screen,
    string formName,
    PXCache cache = null,
    string screenID = null)
  {
    return this._workflowConditionEvaluateService.EvaluateConditions(graph, row, screen, formName, this._auWorkflowFormsEngine.GetFormValues(graph, formName), cache, screenID);
  }

  public Screen GetScreen(string screenId) => this.Configuration.GetByScreen(screenId);

  public IEnumerable<ScreenNavigationAction> GetAutomationActions(
    string graphName,
    string dataMember)
  {
    return this.BaseGetAutomationActions<ScreenNavigationAction>(graphName);
  }

  public IEnumerable<ScreenAction> GetExtraActions(string graphName)
  {
    return this.BaseGetAutomationActions<ScreenAction>(graphName).Where<ScreenAction>((Func<ScreenAction, bool>) (action => action.ExtraData != null));
  }

  private IEnumerable<TAction> BaseGetAutomationActions<TAction>(string graphName) where TAction : ScreenActionBase
  {
    Screen byGraph = this.Configuration.GetByGraph(graphName);
    return byGraph == null ? (IEnumerable<TAction>) Array.Empty<TAction>() : ((IEnumerable<StateMap<ScreenActionBase>.NamedState>) byGraph.Actions.GetList()).Select<StateMap<ScreenActionBase>.NamedState, ScreenActionBase>((Func<StateMap<ScreenActionBase>.NamedState, ScreenActionBase>) (c => c.Value)).OfType<TAction>();
  }

  private StateMap<ScreenActionBase> GetSortedActions(Screen screen)
  {
    StateMap<ScreenActionBase> result = new StateMap<ScreenActionBase>();
    Dictionary<string, ScreenActionBase> inProcess = screen.Actions.GetDictionary();
    foreach (StateMap<ScreenActionBase>.NamedState namedState in screen.Actions.GetList())
      SortStep(namedState.Name);
    return result;

    void SortStep(string actionKey)
    {
      if (string.IsNullOrEmpty(actionKey) || result.TryGetValue(actionKey, out ScreenActionBase _) || !inProcess.ContainsKey(actionKey))
        return;
      ScreenActionBase screenActionBase = inProcess[actionKey];
      inProcess.Remove(actionKey);
      SortStep(screenActionBase.After);
      result.Add(actionKey, screenActionBase);
      SortStep(screenActionBase.Before);
    }
  }

  private List<ScreenActionBase> GetSortedActions(Screen screen, string category)
  {
    return PlacementHelper.SortBeforeAfter<ScreenActionBase>(((IEnumerable<StateMap<ScreenActionBase>.NamedState>) screen.Actions.GetList()).Select<StateMap<ScreenActionBase>.NamedState, ScreenActionBase>((Func<StateMap<ScreenActionBase>.NamedState, ScreenActionBase>) (it => it.Value)).Where<ScreenActionBase>((Func<ScreenActionBase, bool>) (it => it.Category == category)), (Func<ScreenActionBase, string>) (a => a.ActionId), (Func<ScreenActionBase, Placement>) (a => a.PlacementInCategory), (Func<ScreenActionBase, string>) (a => a.AfterInMenu)).ToList<ScreenActionBase>();
  }

  private void AddActions(PXGraph graph, Screen screen)
  {
    StateMap<ScreenActionBase>.NamedState[] list = this.GetSortedActions(screen).GetList();
    List<string> source = new List<string>();
    List<PXGraph.SidePanelAction> actions = new List<PXGraph.SidePanelAction>();
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(graph.GetType().FullName);
    foreach (StateMap<ScreenActionBase>.NamedState namedState in list)
    {
      PXAction action = graph.Actions[namedState.Name];
      PXSpecialButtonType? nullable1 = namedState.Value.ActionFolderType;
      if (!nullable1.HasValue)
      {
        if (action != null)
        {
          nullable1 = namedState.Value.MenuFolderType;
          PXSpecialButtonType specialButtonType = PXSpecialButtonType.SidePanelFolder;
          if (!(nullable1.GetValueOrDefault() == specialButtonType & nullable1.HasValue))
            continue;
        }
        ActionConnotation result;
        PXButtonAttribute pxButtonAttribute = new PXButtonAttribute()
        {
          CommitChanges = true,
          IsLockedOnToolbar = namedState.Value.IsLockedOnToolbar.GetValueOrDefault(),
          Connotation = Enum.TryParse<ActionConnotation>(namedState.Value.Connotation, false, out result) ? result : ActionConnotation.None
        };
        PXUIFieldAttribute attribute = new PXUIFieldAttribute()
        {
          DisplayName = namedState.Value.ActionName ?? namedState.Name,
          Visible = true
        };
        PXEventSubscriberAttribute[] subscriberAttributeArray = new PXEventSubscriberAttribute[2]
        {
          (PXEventSubscriberAttribute) pxButtonAttribute,
          (PXEventSubscriberAttribute) attribute
        };
        PXButtonDelegate actionHandler = this.GetActionHandler(namedState.Value, graph);
        PXCacheRights? nullable2 = namedState.Value.MapViewRights;
        if (nullable2.HasValue)
        {
          PXUIFieldAttribute pxuiFieldAttribute = attribute;
          nullable2 = namedState.Value.MapViewRights;
          int num = (int) nullable2.Value;
          pxuiFieldAttribute.MapViewRights = (PXCacheRights) num;
          PXAccess.Secure(graph.GetPrimaryCache(), (PXEventSubscriberAttribute) attribute);
        }
        if (namedState.Value is ScreenNavigationAction navigationAction)
        {
          PXCacheRights pxCacheRights = string.IsNullOrEmpty(navigationAction.DestinationScreenId) ? PXCacheRights.Update : PXCacheRights.Select;
          PXUIFieldAttribute pxuiFieldAttribute = attribute;
          nullable2 = namedState.Value.MapEnableRights;
          int num1 = (int) nullable2 ?? (int) pxCacheRights;
          pxuiFieldAttribute.MapEnableRights = (PXCacheRights) num1;
          nullable1 = namedState.Value.MenuFolderType;
          PXSpecialButtonType specialButtonType = PXSpecialButtonType.SidePanelFolder;
          if (nullable1.GetValueOrDefault() == specialButtonType & nullable1.HasValue)
          {
            if (NavigationTemplateHelper.IsExternalUrlOrTemplate(navigationAction.DestinationScreenId) || this._siteMapProvider.FindSiteMapNodeByScreenID(navigationAction.DestinationScreenId) != null)
            {
              string screenId = navigationAction.DestinationScreenId;
              string str = "NavigateToLayer$" + screenId;
              int num2 = source.Count<string>((Func<string, bool>) (x => x.Equals(screenId, StringComparison.OrdinalIgnoreCase)));
              if (num2 > 0)
                str += $"_{num2 + 1}";
              source.Add(screenId);
              PXNamedAction.AddHiddenAction(graph, graph.PrimaryItemType, str, actionHandler);
              actions.Add(new PXGraph.SidePanelAction(str, navigationAction.Icon, PXLocalizer.Localize(navigationAction.ActionName, nameLocalizationKey), navigationAction.AfterInMenu, navigationAction.PlacementInCategory));
            }
            else
              continue;
          }
          else
            PXNamedAction.AddAction(graph, graph.PrimaryItemType, namedState.Name, namedState.Value.ActionName ?? namedState.Name, (string) null, true, actionHandler, subscriberAttributeArray);
        }
        if (namedState.Value is ScreenAction screenAction && screenAction.ExtraData != null)
        {
          PXUIFieldAttribute pxuiFieldAttribute = attribute;
          nullable2 = namedState.Value.MapEnableRights;
          int num = (int) nullable2 ?? 2;
          pxuiFieldAttribute.MapEnableRights = (PXCacheRights) num;
          PXNamedAction.AddAction(graph, graph.PrimaryItemType, namedState.Name, namedState.Value.ActionName ?? namedState.Name, (string) null, true, actionHandler, subscriberAttributeArray);
        }
      }
    }
    graph.SidePanelActions.AddRange(this.SortSidePanelActions((IEnumerable<PXGraph.SidePanelAction>) actions));
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions = WorkflowCommonService.GetSpecialGraphActions(graph);
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    List<AUWorkflowCategory> workflowCategoryList = (List<AUWorkflowCategory>) null;
    if (!string.IsNullOrEmpty(screenIdFromGraphType))
    {
      workflowCategoryList = this._workflowActionsEngine.GetOrderedActionCategories(screenIdFromGraphType).ToList<AUWorkflowCategory>();
      foreach (AUWorkflowCategory workflowCategory in workflowCategoryList)
      {
        AUWorkflowCategory category = workflowCategory;
        PXSpecialButtonType? specialButtonType = WorkflowCommonService.GetSpecialButtonType(category.CategoryName);
        Dictionary<string, PXAction> dictionary;
        if (specialButtonType.HasValue && specialGraphActions.TryGetValue(specialButtonType.Value, out dictionary))
        {
          PXAction pxAction;
          if (dictionary.TryGetValue(category.CategoryName, out pxAction))
            pxAction.SetCaption(category.DisplayName);
          else
            dictionary.Values.FirstOrDefault<PXAction>()?.SetCaption(category.DisplayName);
        }
        else if (graph.Actions[category.CategoryName] == null && ((IEnumerable<StateMap<ScreenActionBase>.NamedState>) list).Any<StateMap<ScreenActionBase>.NamedState>((Func<StateMap<ScreenActionBase>.NamedState, bool>) (it => string.Equals(it.Value.Category, category.CategoryName, StringComparison.CurrentCultureIgnoreCase))))
        {
          PXButtonAttribute instance = PXEventSubscriberAttribute.CreateInstance<PXButtonAttribute>();
          instance.CommitChanges = true;
          instance.PopupVisible = true;
          PXNamedAction.AddAction(graph, graph.PrimaryItemType, category.CategoryName, category.DisplayName ?? WorkflowCommonService.GetActionMenuDisplayName(category.CategoryName), (PXButtonDelegate) (adapter => adapter.Get()), (PXEventSubscriberAttribute) instance).MenuAutoOpen = true;
        }
      }
      string str = (string) null;
      foreach (AUWorkflowCategory workflowCategory in workflowCategoryList)
      {
        if (graph.Actions[workflowCategory.CategoryName] != null && str != null && graph.Actions[str] != null)
          graph.Actions.Move(str, workflowCategory.CategoryName, true);
        str = workflowCategory.CategoryName;
      }
    }
    foreach (StateMap<ScreenActionBase>.NamedState namedState in list)
      this.ApplyActionProperties(specialGraphActions, graph, namedState.Value, applyActionsLabelsAndOrder: true);
    foreach (StateMap<ScreenActionBase>.NamedState namedState in list)
    {
      string after = namedState.Value.After;
      if (!string.IsNullOrEmpty(after))
      {
        while (this.MoveAction(graph, namedState.Value.ActionId, after, true) == PXWorkflowService.MoveMenuActionResult.PreviousActionNotInMenu)
        {
          ScreenActionBase screenActionBase = ((IEnumerable<StateMap<ScreenActionBase>.NamedState>) list).FirstOrDefault<StateMap<ScreenActionBase>.NamedState>((Func<StateMap<ScreenActionBase>.NamedState, bool>) (it => it.Name.Equals(after, StringComparison.OrdinalIgnoreCase)))?.Value;
          if (screenActionBase != null)
          {
            after = screenActionBase?.After;
            if (string.IsNullOrEmpty(after))
              break;
          }
          else
            break;
        }
      }
    }
    if (workflowCategoryList != null)
    {
      foreach (AUWorkflowCategory workflowCategory in workflowCategoryList)
      {
        PXAction currentMenu = graph.Actions[workflowCategory.CategoryName];
        if (currentMenu == null)
        {
          PXSpecialButtonType? specialButtonType = WorkflowCommonService.GetSpecialButtonType(workflowCategory.CategoryName);
          Dictionary<string, PXAction> dictionary;
          if (specialButtonType.HasValue && specialGraphActions.TryGetValue(specialButtonType.Value, out dictionary) && !dictionary.TryGetValue(workflowCategory.CategoryName, out currentMenu))
            currentMenu = dictionary.Values.FirstOrDefault<PXAction>();
        }
        if (currentMenu != null)
        {
          List<ScreenActionBase> sortedActions = this.GetSortedActions(screen, workflowCategory.CategoryName);
          foreach (ScreenActionBase settings in sortedActions)
          {
            Placement placement = settings.PlacementInCategory;
            string afterInMenu = settings.AfterInMenu;
            if (PlacementHelper.HasPlacement(placement, afterInMenu))
            {
              while (this.ReorderActionInMenus(graph, settings, currentMenu, placement, afterInMenu) == PXWorkflowService.MoveMenuActionResult.PreviousActionNotInMenu)
              {
                ScreenActionBase screenActionBase = sortedActions.FirstOrDefault<ScreenActionBase>((Func<ScreenActionBase, bool>) (it => it.ActionId.Equals(afterInMenu, StringComparison.OrdinalIgnoreCase)));
                if (screenActionBase != null)
                {
                  placement = screenActionBase != null ? screenActionBase.PlacementInCategory : Placement.After;
                  afterInMenu = screenActionBase?.AfterInMenu;
                  if (string.IsNullOrEmpty(afterInMenu))
                    break;
                }
                else
                  break;
              }
            }
          }
        }
      }
    }
    if (this.IsWorkflowDefinitionDefined(graph) && !this.WorkflowHasSequences(graph))
    {
      List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
      {
        (PXEventSubscriberAttribute) new PXButtonAttribute()
        {
          CommitChanges = true,
          DisplayOnMainToolbar = false,
          PopupVisible = false
        },
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          Visibility = PXUIVisibility.Invisible,
          MapEnableRights = PXCacheRights.Select
        }
      };
      PXButtonDelegate actionHandlerShow = this.GetActionHandlerShow(graph);
      PXNamedAction.AddAction(graph, graph.PrimaryItemType, "ShowWorkflow", "Show state diagram", (string) null, true, actionHandlerShow, subscriberAttributeList.ToArray());
      SetSlots();
      this.OnReuseInitialize = new System.Action(SetSlots);
    }
    List<PXEventSubscriberAttribute> subscriberAttributeList1 = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = false,
        DisplayOnMainToolbar = false,
        PopupVisible = false
      },
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        Visibility = PXUIVisibility.Invisible,
        MapEnableRights = PXCacheRights.Select
      }
    };
    PXButtonDelegate workflowFormDelegate = this.GetCloseWorkflowFormDelegate(graph);
    PXNamedAction.AddAction(graph, graph.PrimaryItemType, "CloseWorkflowForm", "Cancel Workflow Form", (string) null, true, workflowFormDelegate, subscriberAttributeList1.ToArray());

    void SetSlots()
    {
      AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out string _);
      PXContext.SetSlot<System.Type>("Workflow_Current_GraphType", graph.GetType());
      PXContext.SetSlot<AUWorkflow>("Workflow_Current", workflowAndState);
    }
  }

  private IEnumerable<PXGraph.SidePanelAction> SortSidePanelActions(
    IEnumerable<PXGraph.SidePanelAction> actions)
  {
    return PlacementHelper.SortBeforeAfter<PXGraph.SidePanelAction>(actions, (Func<PXGraph.SidePanelAction, string>) (a => a.ActionName), (Func<PXGraph.SidePanelAction, Placement>) (a => a.Placement), (Func<PXGraph.SidePanelAction, string>) (a => a.After));
  }

  private PXWorkflowService.MoveMenuActionResult MoveMenuAction(
    PXAction actionMenu,
    string actionName,
    Placement placementInCategory,
    string prevAction)
  {
    if (actionMenu?._Menus == null || string.IsNullOrEmpty(actionName) || !PlacementHelper.HasPlacement(placementInCategory, prevAction))
      return PXWorkflowService.MoveMenuActionResult.ActionNotInMenu;
    List<ButtonMenu> buttonMenuList = new List<ButtonMenu>((IEnumerable<ButtonMenu>) actionMenu._Menus);
    int index1 = buttonMenuList.FindIndex((Predicate<ButtonMenu>) (o => string.Equals(o.Command, actionName, StringComparison.OrdinalIgnoreCase)));
    if (index1 < 0)
      return PXWorkflowService.MoveMenuActionResult.ActionNotInMenu;
    ButtonMenu buttonMenu = buttonMenuList[index1];
    buttonMenuList.Remove(buttonMenu);
    switch (placementInCategory)
    {
      case Placement.First:
        buttonMenuList.Insert(0, buttonMenu);
        break;
      case Placement.Last:
        buttonMenuList.Add(buttonMenu);
        break;
      default:
        bool flag = placementInCategory == Placement.After;
        int index2 = buttonMenuList.FindIndex((Predicate<ButtonMenu>) (o => string.Equals(o.Command, prevAction, StringComparison.OrdinalIgnoreCase)));
        if (index2 < 0)
          return PXWorkflowService.MoveMenuActionResult.PreviousActionNotInMenu;
        if (flag && index1 == index2 + 1 || !flag && index1 == index2)
          return PXWorkflowService.MoveMenuActionResult.Success;
        if (flag)
        {
          buttonMenuList.Insert(index2 + 1, buttonMenu);
          break;
        }
        buttonMenuList.Insert(index2, buttonMenu);
        break;
    }
    actionMenu.SetMenu(buttonMenuList.ToArray());
    return PXWorkflowService.MoveMenuActionResult.Success;
  }

  private PXWorkflowService.MoveMenuActionResult MoveAction(
    PXGraph graph,
    string actionName,
    string prevAction,
    bool insertAfter)
  {
    if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(prevAction) || graph.Actions[actionName] == null)
      return PXWorkflowService.MoveMenuActionResult.ActionNotInMenu;
    if (graph.Actions[prevAction] == null)
      return PXWorkflowService.MoveMenuActionResult.PreviousActionNotInMenu;
    graph.Actions.Move(prevAction, actionName, insertAfter);
    return PXWorkflowService.MoveMenuActionResult.Success;
  }

  private PXButtonDelegate GetActionHandlerShow(PXGraph graph)
  {
    return (PXButtonDelegate) (adapter =>
    {
      int num = (int) graph.Views["WorkflowView"].AskExt(true);
      return adapter.Get();
    });
  }

  private PXButtonDelegate GetCloseWorkflowFormDelegate(PXGraph graph)
  {
    return (PXButtonDelegate) (adapter =>
    {
      this._auWorkflowFormsEngine.ClearFormData(graph);
      return adapter.Get();
    });
  }

  public void SetWorkflowViewerJSON(PXGraph graph)
  {
    PXFilter<AUWorkflowFormsEngine.WorkflowView> pxFilter = new PXFilter<AUWorkflowFormsEngine.WorkflowView>(graph);
    graph.Views.Add("WorkflowView", (PXSelectBase) pxFilter);
    graph.FieldSelecting.AddHandler<AUWorkflowFormsEngine.WorkflowView.layout>((PXFieldSelecting) ((sender, args) =>
    {
      if (args.Row == null)
        return;
      string stateID;
      string json = LayoutHelper.GetJSON(this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, out stateID), graph, this._auWorkflowEngine, this._workflowActionsEngine, this._auWorkflowEventsEngine, this._pageIndexingService);
      if (string.IsNullOrEmpty(json))
        return;
      JObject jobject = JObject.Parse(json);
      foreach (JToken jtoken in (JArray) jobject["nodeDataArray"])
      {
        if (jtoken[(object) "key"].ToString() == "Task_" + stateID)
        {
          jtoken[(object) "isSelected"] = JToken.op_Implicit(true);
          break;
        }
      }
      args.ReturnValue = (object) jobject.ToString();
    }));
  }

  private PXButtonDelegate GetActionHandler(ScreenActionBase actionBase, PXGraph graph)
  {
    ScreenNavigationAction navigationAction = actionBase as ScreenNavigationAction;
    if (navigationAction != null && !string.IsNullOrWhiteSpace(navigationAction.DestinationScreenId))
      return (PXButtonDelegate) (adapter =>
      {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        PXCache cache = adapter.View.Cache;
        IEnumerable<AUScreenActionBaseState> actionDefinitions = this._workflowActionsEngine.GetActionDefinitions(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()));
        AUScreenActionBaseState screenActionBaseState = actionDefinitions != null ? actionDefinitions.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => string.Equals(it.ActionName, actionBase.ActionId, StringComparison.OrdinalIgnoreCase))) : (AUScreenActionBaseState) null;
        foreach (StateMap<ScreenNavigationParameter>.NamedState namedState in navigationAction.Parameters.GetList())
        {
          ScreenNavigationParameter parameter = namedState.Value;
          object navigationParameterValue = this.GetNavigationParameterValue(cache, graph, parameter, screenActionBaseState?.Form);
          parameters[namedState.Value.FieldName] = navigationParameterValue;
        }
        this._navigationService.NavigateTo(navigationAction.DestinationScreenId, (IReadOnlyDictionary<string, object>) parameters, navigationAction.WindowMode);
        return adapter.Get();
      });
    if (actionBase is ScreenAction screenAction)
    {
      if (!string.IsNullOrWhiteSpace(screenAction.Method))
        return new PXButtonDelegate((graph.Actions[screenAction.Method] ?? throw new PXException("Method '{0}' specified for the action '{1}' does not exist.", new object[2]
        {
          (object) screenAction.Method,
          (object) screenAction.ActionId
        })).Press);
      if (screenAction.ExtraData != null && this._extraActionRunHandlerService != null)
        return this._extraActionRunHandlerService.GetActionHandler(screenAction.ExtraData);
    }
    return (PXButtonDelegate) (adapter => adapter.Get());
  }

  private object GetNavigationParameterValue(
    PXCache cache,
    PXGraph graph,
    ScreenNavigationParameter parameter,
    string formName)
  {
    return this._workflowFieldValueEvaluator.Evaluate(graph, (object) null, parameter.Value, new bool?(parameter.IsFromSchema));
  }

  private string FindCurrentActionMenuName(PXGraph graph, string actionName)
  {
    foreach (DictionaryEntry dictionaryEntry in graph.Actions.Cast<DictionaryEntry>())
    {
      string key = (string) dictionaryEntry.Key;
      PXAction pxAction = (PXAction) dictionaryEntry.Value;
      if (pxAction._Menus != null && ((IEnumerable<ButtonMenu>) pxAction._Menus).Any<ButtonMenu>((Func<ButtonMenu, bool>) (b => string.Equals(b.Command, actionName, StringComparison.OrdinalIgnoreCase))))
        return key;
    }
    return (string) null;
  }

  private PXAction GetOrCreateActionMenu(
    PXGraph graph,
    PXSpecialButtonType? menuFolderType,
    string menuFolder)
  {
    if (menuFolder == null)
      menuFolder = !menuFolderType.HasValue ? "Action" : WorkflowCommonService.GetActionNameByFolderType(menuFolderType);
    PXAction actionMenu = graph.Actions[menuFolder];
    if (actionMenu == null)
    {
      string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
      AUWorkflowCategory workflowCategory = (AUWorkflowCategory) null;
      if (!menuFolderType.HasValue && screenIdFromGraphType != null)
        workflowCategory = this._workflowActionsEngine.GetOrderedActionCategories(screenIdFromGraphType).FirstOrDefault<AUWorkflowCategory>((Func<AUWorkflowCategory, bool>) (it => it.CategoryName == menuFolder));
      PXButtonAttribute instance;
      if (menuFolderType.HasValue)
      {
        instance = PXEventSubscriberAttribute.CreateInstance<PXButtonAttribute>();
        instance.SpecialType = menuFolderType.Value;
        instance.CommitChanges = true;
        instance.PopupVisible = true;
      }
      else
      {
        instance = PXEventSubscriberAttribute.CreateInstance<PXButtonAttribute>();
        instance.CommitChanges = true;
        instance.PopupVisible = true;
      }
      actionMenu = PXNamedAction.AddAction(graph, graph.PrimaryItemType, menuFolder, workflowCategory?.DisplayName ?? WorkflowCommonService.GetActionMenuDisplayName(menuFolder), (PXButtonDelegate) (adapter => adapter.Get()), (PXEventSubscriberAttribute) instance);
      actionMenu.MenuAutoOpen = true;
    }
    return actionMenu;
  }

  private void ApplyActionProperties(
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions,
    PXGraph graph,
    ScreenActionBase settings,
    IReadOnlyDictionary<string, Lazy<bool>> conditionResults = null,
    bool applyActionsLabelsAndOrder = false)
  {
    PXAction action1 = graph.Actions[settings.ActionId];
    string currentActionMenuName = applyActionsLabelsAndOrder ? this.FindCurrentActionMenuName(graph, settings.ActionId) : (string) null;
    if (action1 == null && string.IsNullOrEmpty(currentActionMenuName))
      return;
    if (applyActionsLabelsAndOrder && action1 != null && !string.IsNullOrEmpty(settings.Category))
    {
      action1.AutomationCategory = this._workflowActionsEngine.GetCategoryDisplayName(graph, settings.Category);
      PXSpecialButtonType? specialButtonType = WorkflowCommonService.GetSpecialButtonType(settings.Category);
      Dictionary<string, PXAction> dictionary;
      if (specialButtonType.HasValue && specialGraphActions.TryGetValue(specialButtonType.Value, out dictionary))
      {
        PXAction pxAction;
        action1.AutomationCategory = !dictionary.TryGetValue(settings.Category, out pxAction) ? dictionary.Values.FirstOrDefault<PXAction>()?.GetCaption() ?? action1.AutomationCategory : pxAction.GetCaption();
      }
    }
    bool flag1 = false;
    string forSpecialMenuType = WorkflowCommonService.FindMenuFolderNameForSpecialMenuType(specialGraphActions, settings, currentActionMenuName);
    bool? isTopLevel = settings.IsTopLevel;
    bool? nullable1 = settings.IsLockedOnToolbar;
    bool? isLockedOnToolbar = nullable1 ?? action1?.GetIsLockedOnToolbar();
    string category = settings.Category;
    bool flag2 = WorkflowCommonService.IsActionDisplayedOnMainToolbar(isTopLevel, isLockedOnToolbar, category);
    PXAction actionMenu;
    if (applyActionsLabelsAndOrder)
    {
      int num1;
      if (string.IsNullOrEmpty(forSpecialMenuType) || string.Equals(currentActionMenuName, forSpecialMenuType, StringComparison.OrdinalIgnoreCase))
      {
        if (string.IsNullOrEmpty(forSpecialMenuType))
        {
          PXSpecialButtonType? menuFolderType = settings.MenuFolderType;
          if (menuFolderType.HasValue)
          {
            PXSpecialButtonType[] source = new PXSpecialButtonType[3]
            {
              PXSpecialButtonType.ActionsFolder,
              PXSpecialButtonType.InquiriesFolder,
              PXSpecialButtonType.ReportsFolder
            };
            menuFolderType = settings.MenuFolderType;
            num1 = ((IEnumerable<PXSpecialButtonType>) source).Contains<PXSpecialButtonType>(menuFolderType.Value) ? 1 : 0;
            goto label_12;
          }
        }
        num1 = 0;
      }
      else
        num1 = 1;
label_12:
      bool flag3 = num1 != 0;
      actionMenu = !string.IsNullOrEmpty(forSpecialMenuType) | flag3 ? this.GetOrCreateActionMenu(graph, settings.MenuFolderType, forSpecialMenuType) : (PXAction) null;
      int num2;
      if (!string.IsNullOrEmpty(currentActionMenuName))
      {
        nullable1 = settings.IsTopLevel;
        if (nullable1.GetValueOrDefault())
        {
          num2 = string.IsNullOrEmpty(settings.Category) ? 1 : 0;
          goto label_16;
        }
      }
      num2 = 0;
label_16:
      flag1 = num2 != 0;
      if (action1 != null && flag3 | flag1)
      {
        string menu = (action1.GetState((object) null) is PXButtonState state1 ? state1.Name : (string) null) ?? settings.ActionId;
        if (flag3)
        {
          actionMenu?.AddMenuAction(action1);
          if (state1 != null)
            action1.SetVisible(state1.Visible);
          action1.WorkflowHiddenOnMainToolbar = !flag2;
        }
        if (!string.IsNullOrEmpty(currentActionMenuName))
        {
          PXAction action2 = graph.Actions[currentActionMenuName];
          if (!string.IsNullOrEmpty(currentActionMenuName) && conditionResults != null)
          {
            ButtonMenu buttonMenu = action2?.GetState((object) null) is PXButtonState state2 ? ((IEnumerable<ButtonMenu>) state2.Menus).FirstOrDefault<ButtonMenu>((Func<ButtonMenu, bool>) (m => string.Equals(m.Command, settings.ActionId, StringComparison.OrdinalIgnoreCase))) : (ButtonMenu) null;
            if (flag1 && buttonMenu != null)
            {
              action1.SetVisible(buttonMenu.Visible);
              action1.SetEnabled(buttonMenu.Enabled);
            }
            if (flag3 && actionMenu != null && buttonMenu != null)
            {
              actionMenu.SetEnabledInternal(menu, buttonMenu.Enabled, false);
              actionMenu.SetVisibleInternal(menu, buttonMenu.Visible, false);
            }
          }
          action2?.RemoveMenuAction(action1);
        }
        else if (flag3 && state1 != null && actionMenu != null)
        {
          actionMenu.SetEnabledInternal(menu, state1.Enabled, false);
          actionMenu.SetVisibleInternal(menu, state1.Visible, false);
        }
      }
      if (actionMenu != null && PlacementHelper.HasPlacement(settings.PlacementInCategory, settings.AfterInMenu))
      {
        int num3 = (int) this.MoveMenuAction(actionMenu, settings.ActionId, settings.PlacementInCategory, settings.AfterInMenu);
      }
      if (!string.IsNullOrEmpty(settings.Before))
        graph.Actions.Move(settings.Before, settings.ActionId);
      else if (!string.IsNullOrEmpty(settings.After))
        graph.Actions.Move(settings.After, settings.ActionId, true);
      else if (actionMenu != null)
      {
        List<DictionaryEntry> list = graph.Actions.Cast<DictionaryEntry>().ToList<DictionaryEntry>();
        string str = (string) null;
        foreach (DictionaryEntry dictionaryEntry in list)
        {
          DictionaryEntry actionEntry = dictionaryEntry;
          if (actionEntry.Value == actionMenu)
          {
            graph.Actions.Move(string.IsNullOrEmpty(str) ? "Last" : str, settings.ActionId, true);
            break;
          }
          if (!((string) actionEntry.Key).Equals(settings.ActionId, StringComparison.OrdinalIgnoreCase))
          {
            if (((IEnumerable<ButtonMenu>) actionMenu._Menus).Any<ButtonMenu>((Func<ButtonMenu, bool>) (it => string.Equals(it.Command, (string) actionEntry.Key, StringComparison.OrdinalIgnoreCase))))
              str = (string) actionEntry.Key;
          }
          else
            break;
        }
      }
    }
    else
    {
      actionMenu = !string.IsNullOrEmpty(forSpecialMenuType) ? this.GetOrCreateActionMenu(graph, settings.MenuFolderType, forSpecialMenuType) : (PXAction) null;
      if (actionMenu != null)
        action1.WorkflowHiddenOnMainToolbar = !flag2;
    }
    PXCacheRights? nullable2;
    if (action1 != null)
    {
      nullable2 = settings.MapEnableRights;
      if (nullable2.HasValue)
      {
        PXAction pxAction = action1;
        nullable2 = settings.MapEnableRights;
        int mapping = (int) nullable2.Value;
        pxAction.SetMapEnableRights((PXCacheRights) mapping);
      }
    }
    if (action1 != null)
    {
      nullable2 = settings.MapViewRights;
      if (nullable2.HasValue)
      {
        PXAction pxAction = action1;
        nullable2 = settings.MapViewRights;
        int mapping = (int) nullable2.Value;
        pxAction.SetMapViewRights((PXCacheRights) mapping);
        PXEventSubscriberAttribute attribute = action1.Attributes.FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (it => it is IPXInterfaceField));
        if (attribute != null)
          PXAccess.Secure(graph.GetPrimaryCache(), attribute);
      }
    }
    bool? nullable3 = new bool?();
    bool? nullable4 = new bool?();
    if (conditionResults != null)
    {
      if (!string.IsNullOrEmpty(settings.DisableCondition))
      {
        Lazy<bool> lazy;
        if (!conditionResults.TryGetValue(settings.DisableCondition, out lazy))
          throw new PXException("Condition '{0}' does not exist.", new object[1]
          {
            (object) settings.DisableCondition
          });
        nullable3 = new bool?(lazy.Value);
      }
      if (!string.IsNullOrEmpty(settings.HideCondition))
      {
        Lazy<bool> lazy;
        if (!conditionResults.TryGetValue(settings.HideCondition, out lazy))
          throw new PXException("Condition '{0}' does not exist.", new object[1]
          {
            (object) settings.HideCondition
          });
        nullable4 = new bool?(lazy.Value);
      }
    }
    string caption = (string) null;
    if (!string.IsNullOrEmpty(settings.ActionName) & applyActionsLabelsAndOrder)
    {
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(graph.GetType().FullName);
      caption = PXLocalizer.Localize(settings.ActionName, nameLocalizationKey, out bool _);
    }
    if (flag1)
      actionMenu = (PXAction) null;
    if (action1 != null)
      action1.WorkflowHiddenOnMainToolbar = !flag2;
    if (actionMenu == null)
    {
      if (action1 != null)
      {
        if (!string.IsNullOrEmpty(caption))
          action1.SetCaption(caption);
        if (nullable3.HasValue)
          action1.AutomationDisabled = nullable3.Value;
        if (nullable4.HasValue)
          action1.AutomationHidden = nullable4.Value;
      }
    }
    else
    {
      string actionId = settings.ActionId;
      if (nullable3.HasValue)
      {
        actionMenu.SetEnabledInternal(actionId, !nullable3.Value, false);
        if (action1 != null)
          action1.AutomationDisabled = nullable3.Value;
      }
      if (nullable4.HasValue)
      {
        actionMenu.SetVisibleInternal(actionId, !nullable4.Value, false);
        if (action1 != null)
          action1.AutomationHidden = nullable4.Value;
      }
      if (!string.IsNullOrEmpty(caption))
      {
        if (action1 != null)
        {
          action1.SetCaption(caption);
        }
        else
        {
          foreach (ButtonMenu menu in actionMenu._Menus)
          {
            if (string.Equals(menu.Command, actionId))
              menu.Text = caption;
          }
        }
      }
    }
    if (action1 != null)
    {
      nullable1 = settings.IsTopLevel;
      bool flag4 = true;
      if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
      {
        PXAction pxAction = action1;
        nullable1 = settings.IsTopLevel;
        int num = nullable1.Value ? 1 : 0;
        pxAction.SetDisplayOnMainToolbar(num != 0);
      }
    }
    if (action1 != null)
    {
      nullable1 = settings.IsLockedOnToolbar;
      if (nullable1.HasValue)
      {
        PXAction pxAction = action1;
        nullable1 = settings.IsLockedOnToolbar;
        int num = nullable1.Value ? 1 : 0;
        pxAction.SetIsLockedOnToolbar(num != 0);
      }
    }
    if (action1 != null)
    {
      nullable1 = settings.IgnoresArchiveDisabling;
      if (nullable1.HasValue)
      {
        PXAction pxAction = action1;
        nullable1 = settings.IgnoresArchiveDisabling;
        int num = nullable1.Value ? 1 : 0;
        pxAction.SetIgnoresArchiveDisabling(num != 0);
      }
    }
    ActionConnotation result;
    if (action1 == null || !Enum.TryParse<ActionConnotation>(settings.Connotation, false, out result))
      return;
    action1.SetConnotation(result);
  }

  private void ApplySidePanelActionProperties(
    PXGraph graph,
    ScreenNavigationAction settings,
    IReadOnlyDictionary<string, Lazy<bool>> conditionResults)
  {
    if (conditionResults == null || string.IsNullOrEmpty(settings.HideCondition))
      return;
    PXGraph.SidePanelAction sidePanelAction = graph.SidePanelActions.FirstOrDefault<PXGraph.SidePanelAction>((Func<PXGraph.SidePanelAction, bool>) (spa => spa.ActionName == "NavigateToLayer$" + settings.DestinationScreenId));
    if (sidePanelAction == null)
      return;
    Lazy<bool> lazy;
    if (!conditionResults.TryGetValue(settings.HideCondition, out lazy))
      throw new PXException("Condition '{0}' does not exist.", new object[1]
      {
        (object) settings.HideCondition
      });
    sidePanelAction.Visible = !lazy.Value;
  }

  private PXWorkflowService.MoveMenuActionResult ReorderActionInMenus(
    PXGraph graph,
    ScreenActionBase settings,
    PXAction currentMenu,
    Placement placementInCategory,
    string afterInMenu)
  {
    return graph.Actions[settings.ActionId] == null || currentMenu == null || !PlacementHelper.HasPlacement(settings.PlacementInCategory, settings.AfterInMenu) ? PXWorkflowService.MoveMenuActionResult.ActionNotInMenu : this.MoveMenuAction(currentMenu, settings.ActionId, placementInCategory, afterInMenu);
  }

  private void AlterActions(
    PXGraph graph,
    Screen screen,
    IReadOnlyDictionary<string, Lazy<bool>> conditions,
    object row,
    AUWorkflow currentWorkflow,
    string stateID)
  {
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions = WorkflowCommonService.GetSpecialGraphActions(graph);
    foreach (StateMap<ScreenActionBase>.NamedState namedState in this.GetSortedActions(screen).GetList())
    {
      if (namedState.Value is ScreenNavigationAction settings && settings.WindowMode == PXBaseRedirectException.WindowMode.Layer)
        this.ApplySidePanelActionProperties(graph, settings, conditions);
      else
        this.ApplyActionProperties(specialGraphActions, graph, namedState.Value, conditions, currentWorkflow == null);
    }
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView) || currentWorkflow == null)
      return;
    this._workflowActionsEngine.InitActions(graph, row, screen, screenIdFromGraphType, currentWorkflow, stateID, specialGraphActions);
  }

  private void AlterFields(
    PXGraph graph,
    Screen screen,
    IReadOnlyDictionary<string, Lazy<bool>> conditions,
    object row,
    AUWorkflow workflow,
    string stateId)
  {
    foreach (ScreenTable screenTable in ((IEnumerable<StateMap<ScreenTable>.NamedState>) screen.Tables.GetList()).Select<StateMap<ScreenTable>.NamedState, ScreenTable>((Func<StateMap<ScreenTable>.NamedState, ScreenTable>) (s => s.Value)))
    {
      PXCache cache = graph.Caches[screenTable.CacheType];
      List<ScreenTableField> list = ((IEnumerable<StateMap<ScreenTableField>.NamedState>) screenTable.Fields.GetList()).Select<StateMap<ScreenTableField>.NamedState, ScreenTableField>((Func<StateMap<ScreenTableField>.NamedState, ScreenTableField>) (s => s.Value)).ToList<ScreenTableField>();
      Dictionary<string, ScreenTableField> fieldsMap = new Dictionary<string, ScreenTableField>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (ScreenTableField screenTableField in list)
      {
        cache.SetAltered(screenTableField.FieldName, true);
        fieldsMap.Add(screenTableField.FieldName, screenTable.Fields.GetValue(screenTableField.FieldName));
      }
      PXCache.FieldSelectingDelegate selectingDelegate = (PXCache.FieldSelectingDelegate) ((string field, ref object value, object cacheRow) => this.FieldSelecting((IDictionary<string, ScreenTableField>) fieldsMap, cache, conditions, field, ref value));
      cache.AutomationFieldSelecting += selectingDelegate;
    }
    this._auWorkflowEngine.InitStateProperties(graph, row, workflow?.WorkflowID, workflow?.WorkflowSubID, stateId);
  }

  /// <summary>
  /// Evaluates default value for the field <paramref name="fieldName" /> based on default values of screen fields.
  /// </summary>
  private bool FieldDefaulting(
    IEnumerable<ScreenTableField> fields,
    PXCache primaryViewCache,
    string fieldName,
    object primaryRow,
    ref object value)
  {
    if (fields == null)
      throw new ArgumentNullException(nameof (fields));
    if (primaryViewCache == null)
      throw new ArgumentNullException(nameof (primaryViewCache));
    ScreenTableField screenTableField = fields.FirstOrDefault<ScreenTableField>((Func<ScreenTableField, bool>) (f => string.Equals(f.FieldName, fieldName, StringComparison.OrdinalIgnoreCase)));
    if (string.IsNullOrEmpty(screenTableField?.DefaultValue))
      return false;
    value = this._workflowFieldValueEvaluator.Evaluate(primaryViewCache.Graph, primaryViewCache.Current ?? primaryRow, screenTableField.DefaultValue, new bool?(screenTableField.IsFromSchema));
    return true;
  }

  private void FieldSelecting(
    IDictionary<string, ScreenTableField> configuration,
    PXCache cache,
    IReadOnlyDictionary<string, Lazy<bool>> conditions,
    string field,
    ref object value)
  {
    ScreenTableField extension;
    if (!configuration.TryGetValue(field, out extension) || extension == null || !(value is PXFieldState state))
      return;
    this.AlterFieldState(extension, cache, state, conditions);
  }

  /// <summary>
  /// If current state is non persistent, user should not be able to save the entity
  /// </summary>
  private void CheckNonPersistentStateWhilePersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PXGraph graph = sender?.Graph;
    if (graph == null)
      return;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, e.Row, out stateID);
    if (string.IsNullOrEmpty(stateID))
      return;
    AUWorkflowState state = this._auWorkflowEngine.GetState(graph, workflowAndState?.WorkflowID, workflowAndState?.WorkflowSubID, stateID);
    if ((state != null ? (state.IsNonPersistent ? 1 : 0) : 0) != 0)
    {
      PXCache cach = graph.Caches[graph.PrimaryItemType];
      AUWorkflowDefinition screenWorkflows = this._auWorkflowEngine.GetScreenWorkflows(graph);
      string message = $"Your changes cannot be saved due to the status of the record stored in the {graph.PrimaryItemType.Name}.{screenWorkflows.StateField} field. Modify the record so that its status changes, and then save it.";
      object row = e.Row;
      string stateField = screenWorkflows.StateField;
      string error = message;
      PXUIFieldAttribute.SetError(cach, row, stateField, error, (string) null, true, PXErrorLevel.RowError);
      throw new PXRowPersistingException(screenWorkflows.StateField, (object) null, message);
    }
  }

  private void WorkflowRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    IEnumerable<AUWorkflowStateProperty> propertiesRecursive = this._auWorkflowEngine.GetStatePropertiesRecursive(sender.Graph);
    if (propertiesRecursive == null)
      return;
    foreach (string str1 in propertiesRecursive.Where<AUWorkflowStateProperty>((Func<AUWorkflowStateProperty, bool>) (it => sender.Graph.Caches[GraphHelper.GetType(it.ObjectName)] == sender && it.IsRequired.GetValueOrDefault())).Select<AUWorkflowStateProperty, string>((Func<AUWorkflowStateProperty, string>) (it => it.FieldName)))
    {
      object obj = sender.GetValue(e.Row, str1) ?? PXFieldState.UnwrapValue(sender.GetValueExt(e.Row, str1));
      if (obj == null || obj is string str2 && string.IsNullOrWhiteSpace(str2))
        throw new PXRowPersistingException(str1, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) ((sender.GetStateExt((object) null, str1) is PXFieldState stateExt ? stateExt.DisplayName : (string) null) ?? str1)
        });
    }
  }

  private void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    string fullName = CustomizedTypeManager.GetTypeNotCustomized(sender.Graph.GetType()).FullName;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(sender.Graph.GetType());
    Screen screen = screenIdFromGraphType != null ? this.Configuration.GetByScreen(screenIdFromGraphType) : (fullName != null ? this.Configuration.GetByGraph(fullName) : (Screen) null);
    ScreenTable screenTable;
    if (screen == null || !screen.Tables.TryGetValue(sender.GetItemType().FullName, out screenTable))
      return;
    PXCache cache = sender.Graph.GetPrimaryCache();
    Lazy<IReadOnlyDictionary<string, Lazy<bool>>> lazy1 = new Lazy<IReadOnlyDictionary<string, Lazy<bool>>>((Func<IReadOnlyDictionary<string, Lazy<bool>>>) (() => this.EvaluateConditions(sender.Graph, cache?.Current, screen, (string) null, cache)));
    foreach (ScreenTableField screenTableField in ((IEnumerable<StateMap<ScreenTableField>.NamedState>) screenTable.Fields.GetList()).Select<StateMap<ScreenTableField>.NamedState, ScreenTableField>((Func<StateMap<ScreenTableField>.NamedState, ScreenTableField>) (s => s.Value)))
    {
      object obj = sender.GetValue(e.Row, screenTableField.FieldName) ?? PXFieldState.UnwrapValue(sender.GetValueExt(e.Row, screenTableField.FieldName));
      if (obj == null || obj is string str && string.IsNullOrWhiteSpace(str))
      {
        bool? nullable = new bool?();
        if (!string.IsNullOrEmpty(screenTableField.RequiredCondition))
        {
          Lazy<bool> lazy2;
          if (!lazy1.Value.TryGetValue(screenTableField.RequiredCondition, out lazy2))
            throw new PXException("Condition '{0}' does not exist.", new object[1]
            {
              (object) screenTableField.RequiredCondition
            });
          nullable = new bool?(lazy2.Value);
        }
        else
        {
          bool? isRequired = screenTableField.IsRequired;
          if (isRequired.HasValue)
          {
            ref bool? local = ref nullable;
            isRequired = screenTableField.IsRequired;
            int num = isRequired.Value ? 1 : 0;
            local = new bool?(num != 0);
          }
        }
        if (nullable.GetValueOrDefault())
        {
          if (sender.RaiseExceptionHandling(screenTableField.FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) ((sender.GetStateExt((object) null, screenTableField.FieldName) is PXFieldState stateExt2 ? stateExt2.DisplayName : (string) null) ?? $"[{screenTableField.FieldName}]")))))
            throw new PXRowPersistingException(screenTableField.FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
            {
              (object) ((sender.GetStateExt((object) null, screenTableField.FieldName) is PXFieldState stateExt1 ? stateExt1.DisplayName : (string) null) ?? screenTableField.FieldName)
            });
        }
      }
    }
  }

  private void AlterFieldState(
    ScreenTableField extension,
    PXCache cache,
    PXFieldState state,
    IReadOnlyDictionary<string, Lazy<bool>> conditions)
  {
    bool? nullable1 = new bool?();
    bool? nullable2 = new bool?();
    bool? nullable3 = new bool?();
    if (!PXWorkflowService.PXPreventRecursionInConditionsScope.IsInScope())
    {
      using (new PXWorkflowService.PXPreventRecursionInConditionsScope())
      {
        if (!string.IsNullOrEmpty(extension.RequiredCondition))
        {
          Lazy<bool> lazy;
          if (!conditions.TryGetValue(extension.RequiredCondition, out lazy))
            throw new PXException("Condition '{0}' does not exist.", new object[1]
            {
              (object) extension.RequiredCondition
            });
          nullable1 = new bool?(lazy.Value);
        }
        else
        {
          bool? isRequired = extension.IsRequired;
          if (isRequired.HasValue)
          {
            isRequired = extension.IsRequired;
            nullable1 = new bool?(isRequired.Value);
          }
        }
        if (!string.IsNullOrEmpty(extension.DisableCondition))
        {
          Lazy<bool> lazy;
          if (!conditions.TryGetValue(extension.DisableCondition, out lazy))
            throw new PXException("Condition '{0}' does not exist.", new object[1]
            {
              (object) extension.DisableCondition
            });
          nullable2 = new bool?(lazy.Value);
        }
        if (!string.IsNullOrEmpty(extension.HideCondition))
        {
          Lazy<bool> lazy;
          if (!conditions.TryGetValue(extension.HideCondition, out lazy))
            throw new PXException("Condition '{0}' does not exist.", new object[1]
            {
              (object) extension.HideCondition
            });
          nullable3 = new bool?(lazy.Value);
        }
      }
    }
    if (extension.ComboBoxValues != null && state is PXStringState stringState)
    {
      Dictionary<string, ComboBoxItemsModification>.ValueCollection values = extension.ComboBoxValues.Values;
      PXSystemWorkflows.ApplyComboBoxModifications(cache, stringState, values);
    }
    if (extension.DisplayName != null)
      state.DisplayName = PXLocalizerRepository.SpecialLocalizer.LocalizeWorkflowField(cache.GetType().Name, extension.DisplayName);
    bool? nullable4 = state.Required;
    if (!nullable4.HasValue && nullable1.HasValue)
    {
      state.Required = new bool?(nullable1.Value);
    }
    else
    {
      nullable4 = state.Required;
      bool flag1 = true;
      if (!(nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue))
      {
        nullable4 = nullable1;
        bool flag2 = true;
        if (nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue)
          state.Required = new bool?(true);
      }
    }
    if (state.Enabled)
    {
      nullable4 = nullable2;
      bool flag = true;
      if (nullable4.GetValueOrDefault() == flag & nullable4.HasValue)
        state.Enabled = false;
    }
    if (!state.Visible)
      return;
    nullable4 = nullable3;
    bool flag3 = true;
    if (!(nullable4.GetValueOrDefault() == flag3 & nullable4.HasValue))
      return;
    state.Visible = false;
  }

  internal bool IsCompletionsSuppressed(PXGraph graph)
  {
    return this._workflowActionsEngine.SuppressCompletions[graph.UID];
  }

  public bool ApplyTransition(PXGraph graph, object row, string actionName)
  {
    if (this._workflowActionsEngine.SuppressCompletions[graph.UID])
      return false;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView))
      return false;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out stateID);
    if (workflowAndState == null)
      return false;
    AUWorkflowDefinition screenWorkflows = this._auWorkflowEngine.GetScreenWorkflows(graph);
    string fullName = CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName;
    Screen byScreen = this.Configuration.GetByScreen(screenIdFromGraphType);
    IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(graph, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID, actionName);
    if (EnumerableExtensions.IsNullOrEmpty<AUWorkflowTransition>(transitionsRecursive))
      return false;
    PXCache cach = graph.Caches[graph.PrimaryItemType];
    string actionForm = this.GetActionForm(screenIdFromGraphType, actionName);
    IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, row, byScreen, actionForm);
    PXWorkflowService.TransitionMovementData transtionData = new PXWorkflowService.TransitionMovementData(workflowAndState, stateID, actionName, screenWorkflows.StateField, transitionsRecursive, conditions)
    {
      ActionForm = actionForm
    };
    int num1;
    if (cach.GetStatus(row) == PXEntryStatus.Inserted)
    {
      List<string> appliedAutoAction = PXWorkflowService.AppliedAutoActions[fullName];
      num1 = !string.Equals(appliedAutoAction != null ? appliedAutoAction.LastOrDefault<string>() : (string) null, actionName, StringComparison.OrdinalIgnoreCase) ? 1 : 0;
    }
    else
      num1 = 1;
    bool needPersist = num1 != 0;
    int num2 = this.ApplyTransitionInternal(graph, cach, row, transtionData, screenIdFromGraphType, byScreen, needPersist) ? 1 : 0;
    if (num2 == 0)
      return num2 != 0;
    if (!PXLongOperation.IsLongRunOperation)
      return num2 != 0;
    if (!PXContext.GetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted"))
      return num2 != 0;
    if (this._workflowActionsEngine.MassProcessingWorkflowObjectKeys != null)
      return num2 != 0;
    PXContext.SetSlot<bool>("Workflow.OperationCompletedInTransaction", true);
    return num2 != 0;
  }

  private string GetActionForm(string screenId, string actionName)
  {
    string actionForm = (string) null;
    AUScreenActionBaseState screenActionBaseState = this._workflowActionsEngine.GetActionDefinitions(screenId).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
    if (screenActionBaseState != null)
      actionForm = screenActionBaseState.Form;
    return actionForm;
  }

  private bool ApplyTransitionInternal(
    PXGraph graph,
    PXCache cache,
    object row,
    PXWorkflowService.TransitionMovementData transtionData,
    string screenId,
    Screen screen,
    bool needPersist)
  {
    IEnumerable<AUWorkflowTransition> possibleTranstions = transtionData.PossibleTranstions;
    if (EnumerableExtensions.IsNullOrEmpty<AUWorkflowTransition>(possibleTranstions))
      return false;
    foreach (AUWorkflowTransition auWorkflowTransition in possibleTranstions)
    {
      bool flag1 = true;
      if (auWorkflowTransition.ConditionID.HasValue)
      {
        string key = auWorkflowTransition.ConditionID.Value.ToString();
        Lazy<bool> lazy;
        if (!transtionData.Conditions.TryGetValue(key, out lazy))
        {
          PXTrace.Logger.ForSystemEvents("System", "System_WorkflowWillProbablyFail").ForCurrentCompanyContext().WithStack().Error<object, Guid?, Guid?>("The current workflow will fail to obtain the condition of the transition. record:{Record}, transition:{Transition}, condition:{Condition}", row, auWorkflowTransition.TransitionID, auWorkflowTransition.ConditionID);
          throw new PXInconsistentCachesException($"The condition '{auWorkflowTransition.ConditionID}' for the transition '{auWorkflowTransition.TransitionID}' does not exist.");
        }
        flag1 = lazy.Value;
      }
      if (flag1)
      {
        if (graph.IsImport && cache.GetStatus(row) != PXEntryStatus.Inserted)
          row = cache.Locate(row);
        object obj = cache.GetValue(row, transtionData.StateFieldName);
        object copy = cache.CreateCopy(row);
        this._workflowActionsEngine.ApplyFieldUpdates((IEnumerable<IWorkflowUpdateField>) this.GetFieldUpdates(cache, screenId, transtionData.Workflow, auWorkflowTransition, (string) obj, (string) null).ToList<IWorkflowUpdateField>(), cache, row, this._auWorkflowFormsEngine.GetFormValues(graph, transtionData.ActionForm));
        string targetState = this.MoveToTargetState(graph, row, transtionData.Workflow, transtionData.StateId, screenId, screen, (string) obj, auWorkflowTransition.TargetStateName, cache, transtionData.ActionForm);
        if (string.IsNullOrEmpty(targetState))
          throw new PXException("The further processing cannot be performed because the flow was configured incorrectly. Please contact your system administrator.");
        this._workflowActionsEngine.ApplyFieldUpdates((IEnumerable<IWorkflowUpdateField>) this.GetFieldUpdates(cache, screenId, transtionData.Workflow, (AUWorkflowTransition) null, (string) null, targetState).ToList<IWorkflowUpdateField>(), cache, row, this._auWorkflowFormsEngine.GetFormValues(graph, transtionData.ActionForm));
        cache.SetValue(row, transtionData.StateFieldName, (object) targetState);
        if (cache.Locate(row) != row)
        {
          if (!EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(row), PXEntryStatus.Held, PXEntryStatus.Notchanged))
          {
            PXTrace.Logger.ForSystemEvents("System", "System_WorkflowWillProbablyFail").ForCurrentCompanyContext().WithStack().Error<object, object, string>("The current workflow will fail to update the record status and other record fields due to an error in the workflow code. record:{Record}, old state:{OldState}, new state:{NewState}", row, obj, auWorkflowTransition.TargetStateName);
          }
          else
          {
            cache.Remove(row);
            cache.Hold(row);
          }
        }
        cache.MarkUpdated(row);
        if (!cache.HasPendingValues(row))
          cache.RaiseRowUpdated(row, copy);
        graph.EnsureCachePersistence(cache.GetItemType());
        cache.IsDirty = true;
        cache.RaiseFieldUpdated(transtionData.StateFieldName, row, (object) this.GetStateRealName((string) obj));
        if (needPersist)
        {
          bool? disablePersist = auWorkflowTransition.DisablePersist;
          bool flag2 = true;
          if (!(disablePersist.GetValueOrDefault() == flag2 & disablePersist.HasValue) || PXLongOperation.IsLongRunOperation)
            this._auWorkflowEngine.SetSaveAfterActionSlot(graph);
        }
        return true;
      }
    }
    return false;
  }

  /// <summary>Get root state name of nested State.</summary>
  /// <param name="state"></param>
  /// <param name="graph"></param>
  /// <param name="workflow"></param>
  /// <returns></returns>
  private string GetRootStateName(string stateName, AUWorkflow workflow, string screenId)
  {
    if (stateName == null)
      throw new ArgumentNullException(nameof (stateName));
    string parentState = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateName).ParentState;
    if (!Str.IsNullOrEmpty(parentState))
    {
      for (AUWorkflowState state = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, parentState); state != null && state.ParentState != null; state = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, state.ParentState))
        parentState = state.ParentState;
    }
    return parentState;
  }

  private IEnumerable<string> GetParentStates(
    string stateName,
    PXGraph graph,
    AUWorkflow workflow,
    string screenId)
  {
    if (stateName == null)
      throw new ArgumentNullException(nameof (stateName));
    List<string> parentStates = new List<string>();
    AUWorkflowState state1 = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateName);
    if (!Str.IsNullOrEmpty(state1.ParentState))
    {
      AUWorkflowState state2 = this._auWorkflowEngine.GetState(graph, workflow.WorkflowID, workflow.WorkflowSubID, state1.ParentState);
      if (state2 != null)
        parentStates.Add(state2.Identifier);
      while (state2 != null && state2.ParentState != null)
      {
        state2 = this._auWorkflowEngine.GetState(graph, workflow.WorkflowID, workflow.WorkflowSubID, state2.ParentState);
        if (state2 != null)
          parentStates.Add(state2.Identifier);
      }
    }
    return (IEnumerable<string>) parentStates;
  }

  private IEnumerable<IWorkflowUpdateField> GetFieldUpdates(
    PXCache primaryCache,
    string screenID,
    AUWorkflow workflow,
    AUWorkflowTransition auWorkflowTransition,
    string oldStateName,
    string targetStateName)
  {
    List<IWorkflowUpdateField> fieldUpdates = new List<IWorkflowUpdateField>();
    if (oldStateName != null && !oldStateName.EndsWith("@Initial"))
    {
      string stateId = oldStateName;
      if (stateId.EndsWith("@Final"))
        stateId = stateId.Replace("@Final", "");
      AUWorkflowState state = this._auWorkflowEngine.GetState(screenID, workflow.WorkflowID, workflow.WorkflowSubID, stateId);
      if (state != null && (state.StateType != "S" || oldStateName.EndsWith("@Final")))
      {
        IEnumerable<AUWorkflowOnLeaveStateField> stateOnLeaveFields = this._auWorkflowEngine.GetStateOnLeaveFields(state);
        if (stateOnLeaveFields != null)
          fieldUpdates.AddRange((IEnumerable<IWorkflowUpdateField>) stateOnLeaveFields);
      }
    }
    if (auWorkflowTransition != null)
    {
      IEnumerable<AUWorkflowTransitionField> transitionFields = this._auWorkflowEngine.GetTransitionFields(auWorkflowTransition);
      if (transitionFields != null)
        fieldUpdates.AddRange((IEnumerable<IWorkflowUpdateField>) transitionFields);
    }
    if (targetStateName != null && !targetStateName.EndsWith("@Final"))
    {
      string stateId = targetStateName;
      if (stateId.EndsWith("@Initial"))
        stateId = stateId.Replace("@Initial", "");
      AUWorkflowState state = this._auWorkflowEngine.GetState(screenID, workflow.WorkflowID, workflow.WorkflowSubID, stateId);
      if (state != null && (state.StateType != "S" || targetStateName.EndsWith("@Initial")))
      {
        IEnumerable<AUWorkflowOnEnterStateField> stateOnEnterFields = this._auWorkflowEngine.GetStateOnEnterFields(state);
        if (stateOnEnterFields != null)
          fieldUpdates.AddRange((IEnumerable<IWorkflowUpdateField>) stateOnEnterFields);
      }
    }
    return (IEnumerable<IWorkflowUpdateField>) fieldUpdates;
  }

  private string GetStateRealName(string stateName)
  {
    if (string.IsNullOrEmpty(stateName))
      return stateName;
    return stateName.Split('@')[0];
  }

  private bool IsStateNextOrParentNext(string state) => state == "@N" || state == "@P";

  private string MoveToTargetState(
    PXGraph graph,
    object row,
    AUWorkflow workflow,
    string stateId,
    string screenId,
    Screen screen,
    string sourceStateName,
    string targetStateName,
    PXCache primaryCache,
    string form)
  {
    string targetStateName1 = targetStateName;
    Lazy<IEnumerable<string>> targetParents = new Lazy<IEnumerable<string>>((Func<IEnumerable<string>>) (() => this.IsStateNextOrParentNext(targetStateName) ? Enumerable.Empty<string>() : this.GetParentStates(targetStateName, graph, workflow, screenId)));
    Predicate<string> isTargetDirection = (Predicate<string>) (stateName => this.IsStateNextOrParentNext(targetStateName) || this.IsStateNextOrParentNext(stateName) || targetStateName == stateName || targetParents.Value.Contains<string>(stateName));
    string nextStateName1 = this.GetNextStateName(workflow, stateId, screenId, targetStateName1, isTargetDirection);
    string targetState = this.MoveToNextState(graph, row, workflow, screenId, screen, nextStateName1, primaryCache, form, isTargetDirection);
    if (targetState != null && targetState.EndsWith("@Final"))
    {
      string rootStateName1 = this.GetRootStateName(targetStateName, workflow, screenId);
      string rootStateName2 = this.GetRootStateName(sourceStateName, workflow, screenId);
      if (!this.IsStateNextOrParentNext(targetStateName) && rootStateName2 != rootStateName1)
      {
        string nextStateName2 = this.GetNextStateName(workflow, targetState, screenId, targetStateName, isTargetDirection);
        if (!Str.IsNullOrEmpty(nextStateName2))
        {
          this._workflowActionsEngine.ApplyFieldUpdates((IEnumerable<IWorkflowUpdateField>) this.GetFieldUpdates(primaryCache, screenId, workflow, (AUWorkflowTransition) null, targetState, (string) null).ToList<IWorkflowUpdateField>(), primaryCache, row, this._auWorkflowFormsEngine.GetFormValues(graph, form));
          targetState = this.MoveToNextState(graph, row, workflow, screenId, screen, nextStateName2, primaryCache, form, isTargetDirection);
        }
      }
      else
        targetState = (string) null;
    }
    return targetState;
  }

  private string MoveToNextState(
    PXGraph graph,
    object row,
    AUWorkflow workflow,
    string screenId,
    Screen screen,
    string targetStateName,
    PXCache primaryCache,
    string form,
    Predicate<string> isTargetDirection)
  {
    string stateRealName = this.GetStateRealName(targetStateName);
    if (stateRealName == null)
      return (string) null;
    string str = targetStateName;
    AUWorkflowState state = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateRealName);
    if (state == null)
      return (string) null;
    Guid? nullable = state.SkipConditionID;
    if (nullable.HasValue)
    {
      IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, row, screen, form, primaryCache);
      nullable = state.SkipConditionID;
      string key = nullable.Value.ToString();
      if (!conditions[key].Value)
        return stateRealName;
      targetStateName = this.GetNextStateName(workflow, targetStateName, screenId, "@N", isTargetDirection);
      return string.IsNullOrEmpty(targetStateName) ? stateRealName : this.MoveToNextState(graph, row, workflow, screenId, screen, targetStateName, primaryCache, form, isTargetDirection);
    }
    if (!(state.StateType == "S"))
      return stateRealName;
    object copy = primaryCache.CreateCopy(row);
    if (str.EndsWith("@Final"))
    {
      IEnumerable<AUWorkflowTransition> transitions = this._auWorkflowEngine.GetTransitions(graph, workflow.WorkflowID, workflow.WorkflowSubID, this.GetStateRealName(str), "@OnSequenceLeaving");
      if (!EnumerableExtensions.IsNullOrEmpty<AUWorkflowTransition>(transitions))
      {
        IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, row, screen, form, primaryCache);
        foreach (AUWorkflowTransition auWorkflowTransition in transitions)
        {
          bool flag = true;
          nullable = auWorkflowTransition.ConditionID;
          if (nullable.HasValue)
          {
            IReadOnlyDictionary<string, Lazy<bool>> readOnlyDictionary = conditions;
            nullable = auWorkflowTransition.ConditionID;
            string key = nullable.Value.ToString();
            flag = readOnlyDictionary[key].Value;
          }
          if (flag)
          {
            string targetStateName1 = auWorkflowTransition.TargetStateName;
            if (this._workflowActionsEngine.ApplyFieldUpdates((IEnumerable<IWorkflowUpdateField>) this.GetFieldUpdates(primaryCache, screenId, workflow, auWorkflowTransition, str, (string) null).ToList<IWorkflowUpdateField>(), primaryCache, row, this._auWorkflowFormsEngine.GetFormValues(graph, form)))
            {
              primaryCache.MarkUpdated(row);
              if (!primaryCache.HasPendingValues(row))
                primaryCache.RaiseRowUpdated(row, copy);
            }
            return this.MoveToNextState(graph, row, workflow, screenId, screen, targetStateName1, primaryCache, form, isTargetDirection);
          }
        }
      }
    }
    targetStateName = this.GetNextStateName(workflow, targetStateName, screenId, "@N", isTargetDirection);
    if (string.IsNullOrEmpty(targetStateName))
      return str.EndsWith("@Final") ? str : (string) null;
    if (this._workflowActionsEngine.ApplyFieldUpdates(this.GetFieldUpdates(primaryCache, screenId, workflow, (AUWorkflowTransition) null, str, targetStateName.EndsWith("@Initial") ? targetStateName : (string) null), primaryCache, row, this._auWorkflowFormsEngine.GetFormValues(graph, form)))
    {
      primaryCache.MarkUpdated(row);
      if (!primaryCache.HasPendingValues(row))
        primaryCache.RaiseRowUpdated(row, copy);
    }
    return this.MoveToNextState(graph, row, workflow, screenId, screen, targetStateName, primaryCache, form, isTargetDirection);
  }

  private string GetNextStateName(
    AUWorkflow workflow,
    string stateId,
    string screenId,
    string targetStateName,
    Predicate<string> isTargetDirection)
  {
    string stateRealName = this.GetStateRealName(stateId);
    bool flag1 = false;
    bool flag2 = false;
    if (stateId.EndsWith("@Initial"))
      flag1 = true;
    if (stateId.EndsWith("@Final"))
      flag2 = true;
    AUWorkflowState state1 = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateRealName);
    switch (targetStateName)
    {
      case "@N":
        if (state1.StateType == "S")
        {
          if (flag1)
          {
            AUWorkflowState stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, (AUWorkflowState) null, state1);
            while (stateForSequence != null && !isTargetDirection(stateForSequence.Identifier))
              stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, stateForSequence, state1);
            if (stateForSequence == null)
              stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, (AUWorkflowState) null, state1);
            return stateForSequence != null ? stateForSequence.Identifier : stateRealName + "@Final";
          }
          if (!flag2)
            return stateRealName + "@Initial";
          if (!string.IsNullOrEmpty(state1.NextState))
            return state1.NextState;
          if (!string.IsNullOrEmpty(state1.ParentState))
          {
            AUWorkflowState state2 = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, state1.ParentState);
            if (state2 != null && state2.StateType == "S")
            {
              AUWorkflowState stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, state1, state2);
              while (stateForSequence != null && !isTargetDirection(stateForSequence.Identifier))
                stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, stateForSequence, state2);
              return stateForSequence != null ? stateForSequence.Identifier : state1.ParentState + "@Final";
            }
          }
          return (string) null;
        }
        if (!string.IsNullOrEmpty(state1.ParentState))
        {
          AUWorkflowState state3 = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, state1.ParentState);
          if (state3 == null || !(state3.StateType == "S"))
            return (string) null;
          AUWorkflowState stateForSequence = this._auWorkflowEngine.GetNextStateForSequence(workflow, state1, state3);
          if (stateForSequence != null)
            return stateForSequence.Identifier;
          return state1.NextState != null ? state1.NextState : state1.ParentState + "@Final";
        }
        return !string.IsNullOrEmpty(state1.NextState) ? state1.NextState : (string) null;
      case "@P":
        return this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, state1.ParentState) != null ? state1.ParentState + "@Final" : (string) null;
      default:
        AUWorkflowState state4 = this._auWorkflowEngine.GetState(screenId, workflow.WorkflowID, workflow.WorkflowSubID, this.GetStateRealName(targetStateName));
        if (state1.ParentState != state4.ParentState)
        {
          if (!Str.IsNullOrEmpty(state1.ParentState))
            return state1.ParentState + "@Final";
          string rootStateName = this.GetRootStateName(state4.Identifier, workflow, screenId);
          if (!Str.IsNullOrEmpty(rootStateName))
            return rootStateName;
        }
        return ((state4 == null ? 0 : (state4.StateType == "S" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && !flag2 ? targetStateName + "@Initial" : targetStateName;
    }
  }

  public void ApplyAutoActions(PXGraph graph, object row)
  {
    if (this._workflowActionsEngine.SuppressCompletions[graph.UID])
      return;
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView))
      return;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out stateID);
    if (workflowAndState == null)
      return;
    string fullName = CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName;
    Screen byScreen = this.Configuration.GetByScreen(screenIdFromGraphType);
    IEnumerable<AUWorkflowStateAction> actionsRecursive = this._workflowActionsEngine.GetAutoActionsRecursive(screenIdFromGraphType, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
    List<AUWorkflowStateAction> list = actionsRecursive != null ? actionsRecursive.ToList<AUWorkflowStateAction>() : (List<AUWorkflowStateAction>) null;
    if (list == null || !list.Any<AUWorkflowStateAction>())
      return;
    IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, row, byScreen, (string) null);
    foreach (AUWorkflowStateAction workflowStateAction in list)
    {
      AUWorkflowStateAction autoAction = workflowStateAction;
      if ((autoAction.AutoRun.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : (conditions[autoAction.AutoRun].Value ? 1 : 0)) != 0)
      {
        List<string> source = PXWorkflowService.AppliedAutoActions[fullName];
        if (source == null)
        {
          source = new List<string>();
          PXWorkflowService.AppliedAutoActions[fullName] = source;
        }
        if (!source.Any<string>((Func<string, bool>) (aa => aa.Equals(autoAction.ActionName, StringComparison.OrdinalIgnoreCase))))
        {
          PXAction action = graph.Actions[autoAction.ActionName];
          if (action != null)
          {
            if (action.GetEnabled())
            {
              try
              {
                source.Add(autoAction.ActionName);
                PXCache primaryCache = graph.GetPrimaryCache();
                if (graph.IsImport && primaryCache.GetStatus(row) != PXEntryStatus.Inserted)
                  row = primaryCache.Locate(row);
                AUScreenActionBaseState screenActionBaseState = this._workflowActionsEngine.GetActionDefinitions(screenIdFromGraphType).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(autoAction.ActionName, StringComparison.OrdinalIgnoreCase)));
                int num1;
                if (primaryCache.GetStatus(row) != PXEntryStatus.Inserted)
                {
                  int num2;
                  if (screenActionBaseState == null)
                  {
                    num2 = 1;
                  }
                  else
                  {
                    bool? disablePersist = screenActionBaseState.DisablePersist;
                    bool flag = true;
                    num2 = !(disablePersist.GetValueOrDefault() == flag & disablePersist.HasValue) ? 1 : 0;
                  }
                  num1 = num2 != 0 ? 1 : (PXLongOperation.IsLongRunOperation ? 1 : 0);
                }
                else
                  num1 = 0;
                if (num1 != 0)
                  this._auWorkflowEngine.SetSaveAfterActionSlot(graph);
                action.Press();
                if (num1 != 0)
                  break;
                this.ApplyAutoActions(graph, row);
                break;
              }
              finally
              {
                source.Remove(autoAction.ActionName);
              }
            }
          }
        }
      }
    }
  }

  internal bool IsInAutoAction(PXGraph graph)
  {
    List<string> appliedAutoAction = PXWorkflowService.AppliedAutoActions[graph];
    // ISSUE: explicit non-virtual call
    return (appliedAutoAction != null ? __nonvirtual (appliedAutoAction.Count) : 0) > 0;
  }

  internal IEnumerable<KeyValuePair<string, object>> GetParameters(
    PXGraph graph,
    string actionName,
    object row)
  {
    return this._workflowActionsEngine.GetParameters(graph, actionName, row);
  }

  public void ClearFormData(PXGraph graph) => this._auWorkflowFormsEngine.ClearFormData(graph);

  public bool ApplyComboBoxValues(
    PXGraph graph,
    string tableName,
    string fieldName,
    ref string[] allowedValues,
    ref string[] allowedLabels)
  {
    if (!this.IsAllowedToExecute(graph))
      return false;
    bool flag = false;
    foreach (ScreenTableField screenTableField in this.Configuration.GetAllScreenFields(tableName, fieldName).Where<ScreenTableField>((Func<ScreenTableField, bool>) (it => it.ComboBoxValues != null)))
    {
      List<string> allowedValues1 = new List<string>((IEnumerable<string>) allowedValues);
      List<string> allowedLabels1 = new List<string>((IEnumerable<string>) allowedLabels);
      PXSystemWorkflows.ApplyComboBoxModifications(screenTableField.ComboBoxValues.Values, allowedValues1, allowedLabels1);
      allowedValues = allowedValues1.ToArray();
      allowedLabels = allowedLabels1.ToArray();
      flag = true;
    }
    return flag;
  }

  private bool IsUserAuthenticated() => this._pxIdentityAccessor.Identity != null;

  public bool ApplySystemOnlyComboBoxValues(
    PXGraph graph,
    string tableName,
    string fieldName,
    ref string[] allowedValues,
    ref string[] allowedLabels)
  {
    if (graph == null || this.IgnoreGraph(graph))
      return false;
    bool flag = false;
    List<AUScreenFieldState> source;
    if (PXSystemWorkflows.Definition.SystemWorkflowContainer.SystemFieldStates.TryGetValue(tableName, out source))
    {
      foreach (AUScreenFieldState screenFieldState in source.Where<AUScreenFieldState>((Func<AUScreenFieldState, bool>) (it => string.Equals(it.FieldName, fieldName, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(it.ComboBoxValues))))
      {
        List<string> allowedValues1 = new List<string>((IEnumerable<string>) allowedValues);
        List<string> allowedLabels1 = new List<string>((IEnumerable<string>) allowedLabels);
        PXSystemWorkflows.ApplyComboBoxModifications(((IEnumerable<string>) screenFieldState.ComboBoxValues.Split(';')).Select<string, string[]>((Func<string, string[]>) (it => it.Split('|'))).Select<string[], ComboBoxItemsModification>((Func<string[], ComboBoxItemsModification>) (it => new ComboBoxItemsModification()
        {
          Action = (ComboBoxItemsModificationAction) Enum.Parse(typeof (ComboBoxItemsModificationAction), it[0]),
          ID = it[1],
          Description = it[2]
        })).GroupBy<ComboBoxItemsModification, string>((Func<ComboBoxItemsModification, string>) (it => it.ID)).ToDictionary<IGrouping<string, ComboBoxItemsModification>, string, ComboBoxItemsModification>((Func<IGrouping<string, ComboBoxItemsModification>, string>) (it => it.Key), (Func<IGrouping<string, ComboBoxItemsModification>, ComboBoxItemsModification>) (it => it.OrderByDescending<ComboBoxItemsModification, ComboBoxItemsModificationAction>((Func<ComboBoxItemsModification, ComboBoxItemsModificationAction>) (p => p.Action)).First<ComboBoxItemsModification>())).Values, allowedValues1, allowedLabels1);
        allowedValues = allowedValues1.ToArray();
        allowedLabels = allowedLabels1.ToArray();
        flag = true;
      }
    }
    return flag;
  }

  /// <summary>
  /// Clear workflow data that is usually copied into long run parameters. This parameters are used to finish workflow actions
  /// that run long runs in code
  /// Sometimes, long run's can be started not for action (f.e. telemetry data collection) - and if it happens, than such long run
  /// will capture current workflow status and finish it at the end
  /// </summary>
  public void ClearActionData() => this._workflowActionsEngine.ClearActionData();

  public void RestoreActionData() => this._workflowActionsEngine.RestoreActionData();

  public void ClearLongRunActionData() => this._workflowActionsEngine.ClearLongRunActionData();

  public void RestoreLongRunActionData() => this._workflowActionsEngine.RestoreLongRunActionData();

  internal virtual void PressSave(PXAction caller)
  {
    PXGraph graph1 = caller.Graph;
    System.Type itemType = !string.IsNullOrEmpty(graph1.PrimaryView) ? graph1.Views[graph1.PrimaryView].GetItemType() : (System.Type) null;
    List<PXAction> source = new List<PXAction>();
    foreach (PXAction pxAction in (IEnumerable) graph1.Actions.Values)
    {
      if (pxAction != caller && !(pxAction is ISaveCloseToList) && pxAction.GetState((object) null) is PXButtonState state && state.SpecialType == PXSpecialButtonType.Save && (state.ItemType == (System.Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
        source.Add(pxAction);
    }
    if (source.Any<PXAction>())
    {
      PXAction pxAction = source.Last<PXAction>();
      BqlCommand bqlCommand;
      if (!string.IsNullOrEmpty(graph1.PrimaryView))
      {
        PXView view = graph1.Views[graph1.PrimaryView];
        if (view.GetItemType() == itemType || view.GetItemType().IsAssignableFrom(itemType))
          bqlCommand = view.BqlSelect;
        else
          bqlCommand = BqlCommand.CreateInstance(typeof (Select<>), itemType);
      }
      else
        bqlCommand = BqlCommand.CreateInstance(typeof (Select<>), itemType);
      PXGraph graph2 = graph1;
      BqlCommand command = bqlCommand;
      List<object> records;
      if (graph1.Caches[itemType].Current == null)
      {
        records = new List<object>();
      }
      else
      {
        records = new List<object>();
        records.Add(graph1.Caches[itemType].Current);
      }
      PXAdapter adapter = new PXAdapter((PXView) new PXView.Dummy(graph2, command, records))
      {
        InternalCall = caller != null
      };
      foreach (object obj in pxAction.Press(adapter))
        ;
    }
    else
      graph1.Persist();
  }

  public void BeforeStartOperation(PXGraph graph)
  {
    this._auWorkflowEngine.BeforeStartOperation(graph);
  }

  void ILongOperationWorkflowAdapter.StartOperation(PXLongOperationPars longOperationPars)
  {
    this._workflowActionsEngine.StartOperation(longOperationPars);
  }

  /// <summary>
  /// Restore all workflow parameters (if any existed at the begin)
  /// </summary>
  /// <param name="pxLongOperationPars"></param>
  void ILongOperationWorkflowAdapter.RestoreWorkflowParameters(
    PXLongOperationPars pxLongOperationPars)
  {
    this._workflowActionsEngine.RestoreWorkflowParameters(pxLongOperationPars);
  }

  void ILongOperationWorkflowAdapter.CompleteOperation(
    PXLongOperationPars pxLongOperationPars,
    OperationCompletion operationCompletion,
    ChainEventArgs<Exception> exceptionArgs)
  {
    if (exceptionArgs == null)
      exceptionArgs = ChainEventArgs.From<Exception>((Exception) null);
    if (PXContext.GetSlot<bool>("Workflow.OperationCompletedInTransaction"))
      return;
    this.CompleteOperationInternal(operationCompletion, exceptionArgs);
  }

  public void CompleteOperation(Guid uid)
  {
    if (!PXLongOperation.IsLongRunOperation)
      return;
    if (PXContext.GetSlot<bool>("Workflow.OperationCompletedInTransaction"))
      return;
    try
    {
      if (!PXContext.GetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted"))
        return;
      this.CompleteOperationInternal(OperationCompletion.WorkflowAndEventsAndActionSequences);
    }
    finally
    {
      this.ClearPrimaryWorkflowItemPersisted();
    }
  }

  private void CompleteOperationInternal(
    OperationCompletion operationCompletion,
    ChainEventArgs<Exception> exceptionArgs = null)
  {
    if (exceptionArgs == null)
      exceptionArgs = ChainEventArgs.From<Exception>((Exception) null);
    bool flag1 = false;
    if (operationCompletion == OperationCompletion.OnlyActionSequences && (string.IsNullOrEmpty(this._workflowActionsEngine.LongRunWorkflowAction) || string.IsNullOrEmpty(this._workflowActionsEngine.LongRunWorkflowScreenId)))
    {
      this._workflowActionsEngine.RestoreLongRunActionData();
      flag1 = true;
    }
    try
    {
      if (string.IsNullOrEmpty(this._workflowActionsEngine.LongRunWorkflowAction) || string.IsNullOrEmpty(this._workflowActionsEngine.LongRunWorkflowScreenId) || this._workflowActionsEngine.LongRunWorkflowObjectKeys == null && this._workflowActionsEngine.MassProcessingWorkflowObjectKeys == null)
        return;
      string actionName = this._workflowActionsEngine.LongRunWorkflowAction;
      string str = this._workflowActionsEngine.LongRunWorkflowScreenId.Replace(".", "");
      if (!this._workflowActionsEngine.LongRunWorkflowShouldBeExecuted && !this._businessProcessEventProcessor.HasAnyActionEvent(str, actionName) && !this._workflowActionsEngine.HasAnyActionSequences(str, actionName))
        return;
      string graphTypeByScreenId = this._screenToGraphWorkflowMappingService.GetGraphTypeByScreenID(str);
      PXGraph graph = (PXGraph) null;
      System.Type type = PXBuildManager.GetType(graphTypeByScreenId, false);
      if (type != (System.Type) null)
        graph = PXGraph.CreateInstance(type);
      if (graph == null)
        return;
      bool flag2 = this._workflowActionsEngine.LongRunWorkflowShouldBeExecuted && this.IsEnabled(graph) && this.IsWorkflowDefinitionDefined(graph);
      PXCache cache = graph.Views[graph.PrimaryView].Cache;
      bool flag3 = this._workflowActionsEngine.LongRunWorkflowObjectKeys != null;
      IDictionary[] dictionaryArray;
      if (!flag3)
        dictionaryArray = this._workflowActionsEngine.MassProcessingWorkflowObjectKeys.Where<KeyValuePair<IDictionary, bool>>((Func<KeyValuePair<IDictionary, bool>, bool>) (pair => !pair.Value)).Select<KeyValuePair<IDictionary, bool>, IDictionary>((Func<KeyValuePair<IDictionary, bool>, IDictionary>) (pair => pair.Key)).ToArray<IDictionary>();
      else
        dictionaryArray = new IDictionary[1]
        {
          this._workflowActionsEngine.LongRunWorkflowObjectKeys
        };
      foreach (IDictionary dictionary in dictionaryArray)
      {
        if (cache.Locate(dictionary) != 0)
        {
          object row = cache.Current;
          object persistedRecord = PXTimeStampScope.GetPersistedRecord(cache, row);
          if (persistedRecord != null && persistedRecord != row && cache.GetItemType().IsInstanceOfType(persistedRecord))
          {
            cache.Remove(row);
            cache.Hold(persistedRecord);
            row = persistedRecord;
            cache.Current = row;
          }
          if (operationCompletion == OperationCompletion.WorkflowAndEventsAndActionSequences & flag2)
          {
            IEnumerable<AUScreenActionBaseState> actionDefinitions = this._workflowActionsEngine.GetActionDefinitions(str);
            AUScreenActionBaseState screenActionBaseState = actionDefinitions != null ? actionDefinitions.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase))) : (AUScreenActionBaseState) null;
            this._auWorkflowFormsEngine.SetFormValues(graph, screenActionBaseState?.Form, this._workflowActionsEngine.LongRunFormValues);
            if (this.ApplyTransition(graph, row, actionName))
              this.ApplyWorkflowState(graph, row);
            if (!flag3)
              this._workflowActionsEngine.MassProcessingWorkflowObjectKeys[dictionary] = true;
            if (PXWorkflowService.SaveAfterAction[type])
            {
              PXWorkflowService.SaveAfterAction[type] = false;
              this.ClearFormData(graph);
              this.PressSave(graph.Actions[actionName]);
            }
          }
          if (operationCompletion == OperationCompletion.WorkflowAndEventsAndActionSequences)
            this._businessProcessEventProcessor.TriggerActionEvents(graph, str, actionName, this._workflowActionsEngine.LongRunActionViewsValues, new List<object>()
            {
              row
            });
          Screen byScreen = this.Configuration.GetByScreen(str);
          this._workflowActionsEngine.RunActionSequences(graph, actionName, byScreen, str, !flag3, exceptionArgs);
          if (!flag3)
            graph.Clear();
        }
      }
    }
    finally
    {
      if (flag1)
        this._workflowActionsEngine.ClearLongRunActionData();
    }
  }

  public void FireOnPropertyChangedEvent<TEntity, TField>(PXGraph graph, TEntity eventTarget)
    where TEntity : class, IBqlTable, new()
    where TField : IBqlField
  {
    string fieldName = typeof (TField).Name;
    IEnumerable<string[]> cachePropertyEvents = this._auWorkflowEventsEngine.GetCachePropertyEvents(typeof (TEntity).FullName);
    string[][] array = cachePropertyEvents != null ? cachePropertyEvents.Where<string[]>((Func<string[], bool>) (it => ((IEnumerable<string>) it).Contains<string>(fieldName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))).ToArray<string[]>() : (string[][]) null;
    if (array == null || !((IEnumerable<string[]>) array).Any<string[]>())
      return;
    foreach (string[] strArray in array)
      this.FireEvent(graph, $"@{typeof (TEntity).FullName}PropertyChanged", string.Join("|", strArray), (IBqlTable) eventTarget, (object) null, typeof (TEntity));
  }

  public void FireEvent<TEntity>(
    PXGraph graph,
    string eventContainerName,
    string eventName,
    TEntity eventTarget,
    object eventParameter)
    where TEntity : class, IBqlTable, new()
  {
    this.FireEvent(graph, eventContainerName, eventName, (IBqlTable) eventTarget, eventParameter, typeof (TEntity));
  }

  private void FireEvent(
    PXGraph graph,
    string eventContainerName,
    string eventName,
    IBqlTable eventTarget,
    object eventParameter,
    System.Type itemType)
  {
    IEnumerable<AUWorkflowHandler> subscribedHandlers = this._auWorkflowEventsEngine.GetSubscribedHandlers(eventContainerName, eventName);
    List<AUWorkflowHandler> list1 = subscribedHandlers != null ? subscribedHandlers.ToList<AUWorkflowHandler>() : (List<AUWorkflowHandler>) null;
    if (list1 == null || !list1.Any<AUWorkflowHandler>())
      return;
    foreach (IGrouping<string, AUWorkflowHandler> grouping in list1.GroupBy<AUWorkflowHandler, string>((Func<AUWorkflowHandler, string>) (it => it.ScreenID)))
    {
      string screenId = grouping.Key;
      string graphTypeByScreenId = this._screenToGraphWorkflowMappingService.GetGraphTypeByScreenID(screenId);
      if (graphTypeByScreenId != null)
      {
        string primaryViewForScreen = this._screenToGraphWorkflowMappingService.GetPrimaryViewForScreen(screenId);
        if (!string.IsNullOrEmpty(primaryViewForScreen))
        {
          PXCacheInfo cache1 = GraphHelper.GetGraphView(graphTypeByScreenId, primaryViewForScreen).Cache;
          AUWorkflowDefinition wd = this._auWorkflowEngine.GetScreenWorkflows(screenId);
          Screen screen = this.Configuration.GetByScreen(screenId);
          List<KeyValuePair<PXCache, object>> keyValuePairList = new List<KeyValuePair<PXCache, object>>();
          foreach (AUWorkflowHandler subscribedHandler in (IEnumerable<AUWorkflowHandler>) grouping)
          {
            System.Type type = !string.IsNullOrEmpty(subscribedHandler.UpcastType) ? PXBuildManager.GetType(subscribedHandler.UpcastType, false) : cache1.CacheType;
            PXCache cache = graph.Caches[type];
            System.Type cacheItemType = cache.GetItemType();
            bool? nullable = subscribedHandler.UseTargetAsPrimarySource;
            bool flag1 = true;
            int num;
            if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
            {
              nullable = subscribedHandler.UseParameterAsPrimarySource;
              bool flag2 = true;
              num = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
            }
            else
              num = 1;
            bool flag3 = num != 0;
            IEnumerable eventHandlerRecords = this.GetEventHandlerRecords(graph, eventTarget, eventParameter, subscribedHandler, type, itemType);
            List<IBqlTable> list2 = eventHandlerRecords != null ? eventHandlerRecords.Cast<object>().Select<object, IBqlTable>((Func<object, IBqlTable>) (r => PXResult.Unwrap(r, cacheItemType))).ToList<IBqlTable>() : (List<IBqlTable>) null;
            if (list2 != null && list2.Count != 0)
            {
              List<System.Action> actionList = new List<System.Action>();
              foreach (object obj1 in list2)
              {
                object currentRecord = obj1;
                if (EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(currentRecord), PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted, PXEntryStatus.Updated, PXEntryStatus.Deleted))
                {
                  object obj2 = cache.Locate(currentRecord);
                  if (obj2 != currentRecord & flag3 && cache.GetItemType().IsInstanceOfType(currentRecord))
                    PXTrace.Logger.ForContext("EventID", (object) "System_WorkflowWillNotUsePassedRecordCache", false).ForContext("SourceContext", (object) "System", false).ForCurrentCompanyContext().WithStack().Warning("The record used in the workflow differs from the record updated in the cache. record:{Record}, keys:{Keys}, eventContainerName:{EventContainerName}, eventName:{EventName}, handlerName:{HandlerName}", new object[5]
                    {
                      currentRecord,
                      (object) string.Join(";", cache.Keys.Select<string, string>((Func<string, string>) (k => cache.GetValue(currentRecord, k)?.ToString()))),
                      (object) eventContainerName,
                      (object) eventName,
                      (object) subscribedHandler.HandlerName
                    });
                  else
                    currentRecord = obj2;
                }
                else
                {
                  object persistedRecord = PXTimeStampScope.GetPersistedRecord(cache, currentRecord);
                  if (persistedRecord != null && persistedRecord != currentRecord && cache.GetItemType().IsInstanceOfType(persistedRecord))
                  {
                    if (!flag3)
                      currentRecord = persistedRecord;
                    else
                      PXTrace.Logger.ForContext("EventID", (object) "System_WorkflowWillNotUsePassedRecordPersisted", false).ForContext("SourceContext", (object) "System", false).ForCurrentCompanyContext().WithStack().Warning("The latest saved record may be more actual than the record used in the workflow. record:{Record}, keys:{Keys}, eventContainerName:{EventContainerName}, eventName:{EventName}, handlerName:{HandlerName}", new object[5]
                      {
                        currentRecord,
                        (object) string.Join(";", cache.Keys.Select<string, string>((Func<string, string>) (k => cache.GetValue(currentRecord, k)?.ToString()))),
                        (object) eventContainerName,
                        (object) eventName,
                        (object) subscribedHandler.HandlerName
                      });
                  }
                }
                string stateId;
                AUWorkflow workflow = this._auWorkflowEngine.GetCurrentWorkflowAndState(screenId, cache, currentRecord, out stateId);
                if (workflow != null && this._auWorkflowEventsEngine.IsSubscribedRecursive(subscribedHandler, stateId, workflow.WorkflowID, workflow.WorkflowSubID))
                {
                  if (!string.IsNullOrEmpty(subscribedHandler.Condition))
                  {
                    IReadOnlyDictionary<string, Lazy<bool>> conditions = this.EvaluateConditions(graph, currentRecord, screen, (string) null, cache, screenId);
                    bool flag4 = true;
                    if (!string.IsNullOrEmpty(subscribedHandler.Condition))
                      flag4 = conditions[subscribedHandler.Condition].Value;
                    if (!flag4)
                      continue;
                  }
                  if (keyValuePairList.All<KeyValuePair<PXCache, object>>((Func<KeyValuePair<PXCache, object>, bool>) (pair => pair.Key != cache)))
                    keyValuePairList.Add(new KeyValuePair<PXCache, object>(cache, cache.Current));
                  string handlerName = subscribedHandler.HandlerName;
                  System.Action action = (System.Action) (() => this.ProcessEventStep(screenId, handlerName, cache, currentRecord, workflow, stateId, screen, wd, false));
                  actionList.Add(action);
                }
              }
              using (new ReplaceCurrentScope((IEnumerable<KeyValuePair<PXCache, object>>) keyValuePairList))
              {
                foreach (System.Action action in actionList)
                  action();
              }
            }
          }
        }
      }
    }
  }

  private void ProcessEventStep(
    string screenId,
    string handlerName,
    PXCache cache,
    object row,
    AUWorkflow workflow,
    string stateId,
    Screen screen,
    AUWorkflowDefinition wd,
    bool isActionProcessed)
  {
    PXGraph graph = cache.Graph;
    object copy = cache.CreateCopy(row);
    if (this._workflowActionsEngine.ApplyFieldUpdates(this.GetWorkflowUpdateFields(screenId, handlerName, isActionProcessed), cache, row, (IReadOnlyDictionary<string, object>) null))
    {
      if (cache.Locate(row) != row && !EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(row), PXEntryStatus.Held, PXEntryStatus.Notchanged))
        PXTrace.Logger.ForContext("EventID", (object) "System_WorkflowWillProbablyFail", false).ForContext("SourceContext", (object) "System", false).ForCurrentCompanyContext().WithStack().Error<PXCache>("The current workflow will fail to update the record status and other record fields due to an error in the workflow code. record:{Record}", cache);
      cache.MarkUpdated(row);
      if (!cache.HasPendingValues(row))
        this.RaiseRowUpdatedInEvent(cache, row, copy);
      cache.Graph.EnsureCachePersistence(cache.GetItemType());
    }
    IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateId, handlerName);
    if (transitionsRecursive == null)
      return;
    IReadOnlyDictionary<string, Lazy<bool>> conditions1 = this.EvaluateConditions(graph, row, screen, (string) null, cache, screenId);
    this.ApplyTransitionInternal(graph, cache, row, new PXWorkflowService.TransitionMovementData(workflow, stateId, handlerName, wd.StateField, transitionsRecursive, conditions1), screenId, screen, false);
    stateId = cache.GetValue(row, wd.StateField) as string;
    IEnumerable<AUWorkflowStateAction> actionsRecursive = this._workflowActionsEngine.GetAutoActionsRecursive(screenId, workflow.WorkflowID, workflow.WorkflowSubID, stateId);
    List<AUWorkflowStateAction> list = actionsRecursive != null ? actionsRecursive.ToList<AUWorkflowStateAction>() : (List<AUWorkflowStateAction>) null;
    if (list == null || !list.Any<AUWorkflowStateAction>())
      return;
    IReadOnlyDictionary<string, Lazy<bool>> conditions2 = this.EvaluateConditions(graph, row, screen, (string) null, cache, screenId);
    IEnumerable<AUScreenActionBaseState> actionDefinitions = this._workflowActionsEngine.GetActionDefinitions(screenId);
    foreach (AUWorkflowStateAction workflowStateAction in list.Where<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => actionDefinitions.Any<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (ad => string.Equals(ad.ActionName, it.ActionName, StringComparison.OrdinalIgnoreCase) && ad is AUScreenNavigationActionState && string.IsNullOrEmpty(ad.ActionType))))))
    {
      AUWorkflowStateAction autoAction = workflowStateAction;
      if ((autoAction.AutoRun.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : (conditions2[autoAction.AutoRun].Value ? 1 : 0)) != 0)
      {
        List<string> source = PXWorkflowService.AppliedAutoActions[graph];
        if (source == null)
        {
          source = new List<string>();
          PXWorkflowService.AppliedAutoActions[graph] = source;
        }
        if (!source.Any<string>((Func<string, bool>) (aa => aa.Equals(autoAction.ActionName, StringComparison.OrdinalIgnoreCase))))
        {
          PXAction action = graph.Actions[autoAction.ActionName];
          if (action != null)
          {
            if (action.GetEnabled())
            {
              try
              {
                source.Add(autoAction.ActionName);
                this.ProcessEventStep(screenId, autoAction.ActionName, cache, row, workflow, stateId, screen, wd, true);
                break;
              }
              finally
              {
                source.Remove(autoAction.ActionName);
              }
            }
          }
        }
      }
    }
  }

  private void RaiseRowUpdatedInEvent(PXCache cache, object currentRecord, object oldRow)
  {
    PXGraph graph = cache.Graph;
    cache.RaiseRowUpdated(currentRecord, oldRow);
    if (graph.GetPrimaryCache() == cache && (this.IsEnabled(graph) || this.IsWorkflowExists(graph)))
      return;
    IEnumerable<string[]> cachePropertyEvents = this._auWorkflowEventsEngine.GetCachePropertyEvents(cache.GetItemType().FullName);
    string[][] array = cachePropertyEvents != null ? cachePropertyEvents.ToArray<string[]>() : (string[][]) null;
    if (array == null || !((IEnumerable<string[]>) array).Any<string[]>())
      return;
    this.TryFireOnPropertyChangedEvent(array, cache, new PXRowUpdatedEventArgs(currentRecord, oldRow, false));
  }

  private IEnumerable<IWorkflowUpdateField> GetWorkflowUpdateFields(
    string screenId,
    string name,
    bool isAction)
  {
    return isAction ? (IEnumerable<IWorkflowUpdateField>) this._workflowActionsEngine.GetActionFields(this._workflowActionsEngine.GetActionDefinitions(screenId).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => string.Equals(it.ActionName, name, StringComparison.OrdinalIgnoreCase)))) : this._auWorkflowEventsEngine.GetHandlerFields(this._auWorkflowEventsEngine.GetHandlerDefinition(screenId, name));
  }

  private IEnumerable GetEventHandlerRecords(
    PXGraph graph,
    IBqlTable eventTarget,
    object eventParameter,
    AUWorkflowHandler subscribedHandler,
    System.Type cacheType,
    System.Type entityType)
  {
    bool? targetAsPrimarySource = subscribedHandler.UseTargetAsPrimarySource;
    bool flag1 = true;
    if (targetAsPrimarySource.GetValueOrDefault() == flag1 & targetAsPrimarySource.HasValue)
    {
      if (!cacheType.IsAssignableFrom(entityType))
        return (IEnumerable) null;
      return (IEnumerable) new IBqlTable[1]{ eventTarget };
    }
    bool? parameterAsPrimarySource = subscribedHandler.UseParameterAsPrimarySource;
    bool flag2 = true;
    if (parameterAsPrimarySource.GetValueOrDefault() == flag2 & parameterAsPrimarySource.HasValue)
    {
      if (eventParameter == null || cacheType != eventParameter.GetType())
        return (IEnumerable) null;
      return (IEnumerable) new object[1]{ eventParameter };
    }
    if (subscribedHandler.SelectType == null)
      return (IEnumerable) null;
    BqlCommand instance = (BqlCommand) Activator.CreateInstance(System.Type.GetType(subscribedHandler.SelectType));
    PXView pxView = new PXView(graph, false, instance);
    bool? allowMultipleSelect = subscribedHandler.AllowMultipleSelect;
    bool flag3 = true;
    return allowMultipleSelect.GetValueOrDefault() == flag3 & allowMultipleSelect.HasValue ? (eventParameter != null ? (IEnumerable) pxView.SelectMultiBound(new object[2]
    {
      (object) eventTarget,
      eventParameter
    }) : (IEnumerable) pxView.SelectMultiBound((object[]) new IBqlTable[1]
    {
      eventTarget
    })) : (eventParameter != null ? (IEnumerable) new object[1]
    {
      pxView.SelectSingleBound(new object[2]
      {
        (object) eventTarget,
        eventParameter
      })
    } : (IEnumerable) new object[1]
    {
      pxView.SelectSingleBound((object[]) new IBqlTable[1]
      {
        eventTarget
      })
    });
  }

  public IEnumerable<(string categoryName, string displayName)> GetOrderedCategories(PXGraph graph)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return (IEnumerable<(string, string)>) null;
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions = WorkflowCommonService.GetSpecialGraphActions(graph);
    IEnumerable<AUWorkflowCategory> actionCategories = this._workflowActionsEngine.GetOrderedActionCategories(screenIdFromGraphType);
    return actionCategories == null ? (IEnumerable<(string, string)>) null : actionCategories.Select<AUWorkflowCategory, (string, string)>((Func<AUWorkflowCategory, (string, string)>) (it =>
    {
      string str = it.DisplayName;
      PXSpecialButtonType? specialButtonType = WorkflowCommonService.GetSpecialButtonType(it.CategoryName);
      Dictionary<string, PXAction> dictionary;
      if (specialButtonType.HasValue && specialGraphActions.TryGetValue(specialButtonType.Value, out dictionary))
      {
        PXAction pxAction;
        str = !dictionary.TryGetValue(it.CategoryName, out pxAction) ? dictionary.Values.FirstOrDefault<PXAction>()?.GetCaption() ?? str : pxAction.GetCaption();
      }
      return (it.CategoryName, str);
    }));
  }

  public void CheckPrimaryWorkflowItemPersisted(PXGraph graph, System.Type table, List<object> items)
  {
    if (!PXLongOperation.IsLongRunOperation || this._workflowActionsEngine.LongRunWorkflowObjectType == (System.Type) null || this._workflowActionsEngine.LongRunWorkflowObjectType != table || PXContext.GetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted"))
      return;
    if (this._workflowActionsEngine.LongRunWorkflowObjectKeys == null)
    {
      PXContext.SetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted", items.Count > 0);
      PXCache cache = graph.Caches[table];
      this._workflowActionsEngine.MassProcessingWorkflowObjectKeys = (IDictionary<IDictionary, bool>) items.ToDictionary<object, IDictionary, bool>((Func<object, IDictionary>) (it => (IDictionary) cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (cacheKey => cacheKey), (Func<string, object>) (cacheKey => cache.GetValue(it, cacheKey)))), (Func<object, bool>) (_ => false));
    }
    else
    {
      if (PXContext.GetSlot<bool>("Workflow.OperationCompletedInTransaction"))
        return;
      PXCache cache = graph.Caches[table];
      object workflowObject = cache.CreateInstance();
      foreach (DictionaryEntry workflowObjectKey in this._workflowActionsEngine.LongRunWorkflowObjectKeys)
        cache.SetValue(workflowObject, workflowObjectKey.Key.ToString(), workflowObjectKey.Value);
      PXContext.SetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted", items.Any<object>((Func<object, bool>) (it => cache.ObjectsEqual(workflowObject, it))));
    }
  }

  public void ClearPrimaryWorkflowItemPersisted()
  {
    PXContext.SetSlot<bool>("Workflow.PrimaryWorkflowItemPersistedBackup", PXContext.GetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted"));
    PXContext.SetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted", false);
  }

  public void ClearMassProcessingWorkflowObjectKeys()
  {
    this._workflowActionsEngine.MassProcessingWorkflowObjectKeys = (IDictionary<IDictionary, bool>) null;
  }

  internal void PrepareFormDataForMassProcessing(PXGraph graph, string formName, string screenID)
  {
    this._auWorkflowFormsEngine.PrepareFormDataForMassProcessing(graph, formName, screenID);
  }

  internal bool AskMassUpdateExt(
    PXGraph graph,
    string formName,
    string screenID,
    Screen screen,
    string actionName)
  {
    return this._auWorkflowFormsEngine.AskMassUpdateExt(graph, formName, screenID, screen, actionName);
  }

  internal IEnumerable<AUScreenActionBaseState> GetActionDefinitions(string screenID)
  {
    return this._workflowActionsEngine.GetActionDefinitions(screenID);
  }

  internal IReadOnlyDictionary<string, object> GetOnlyProvidedFormValues(
    PXGraph graph,
    string screen,
    string form)
  {
    return this._auWorkflowFormsEngine.GetOnlyProvidedFormValues(graph, screen, form);
  }

  internal void SetFormValues(
    PXGraph graph,
    string form,
    IDictionary<string, object> values,
    bool useMulti = false)
  {
    this._auWorkflowFormsEngine.SetFormValues(graph, form, values, useMulti);
  }

  internal string GetGraphTypeByScreenID(string screenId)
  {
    return this._screenToGraphWorkflowMappingService.GetGraphTypeByScreenID(screenId);
  }

  public void RestorePrimaryWorkflowItemPersisted()
  {
    PXContext.SetSlot<bool>("Workflow.PrimaryWorkflowItemPersisted", PXContext.GetSlot<bool>("Workflow.PrimaryWorkflowItemPersistedBackup"));
  }

  public bool HasAnyActionSequences(string screen, string actionName)
  {
    return this._workflowActionsEngine.HasAnyActionSequences(screen, actionName);
  }

  public IEnumerable<string> GetCustomizedScreenConfiguration(PXGraph graph)
  {
    return this.SelectCustomizedAUDac<AUWorkflowDefinition>(graph).Union<string>(this.SelectCustomizedAUDac<AUWorkflow>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowState>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowTransition>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowCategory>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowForm>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowHandler>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowActionParam>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowActionSequence>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowFormField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowStateAction>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowStateProperty>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowTransitionField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowActionUpdateField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowHandlerUpdateField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowOnEnterStateField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowOnLeaveStateField>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowActionSequenceFormFieldValue>(graph)).Union<string>(this.SelectCustomizedAUDac<AUWorkflowStateEventHandler>(graph)).Union<string>(this.SelectCustomizedAUDac<AUScreenActionState>(graph)).Union<string>(this.SelectCustomizedAUDac<AUScreenNavigationActionState>(graph)).Union<string>(this.SelectCustomizedAUDac<AUScreenConditionState>(graph)).Union<string>(this.SelectCustomizedAUDac<AUScreenFieldState>(graph)).Union<string>(this.SelectCustomizedAUDac<AUScreenNavigationParameterState>(graph)).Distinct<string>();
  }

  private IEnumerable<string> SelectCustomizedAUDac<T>(PXGraph graph) where T : PXBqlTable, IScreenItem, new()
  {
    return (IEnumerable<string>) PXSelectBase<T, PXSelect<T>.Config>.Select(graph).FirstTableItems.ToList<T>().Select<T, string>((Func<T, string>) (it => it.ScreenID)).ToList<string>();
  }

  public class SlotIndexerByGraph<TValue> : SlotIndexer<string, TValue>
  {
    private readonly string _slotName;

    public SlotIndexerByGraph(string slotName) => this._slotName = slotName;

    protected override string ToSlotIndex(string graphName) => $"{this._slotName}@{graphName}";

    public TValue this[PXGraph graph]
    {
      get => this[CustomizedTypeManager.GetTypeNotCustomized(graph.GetType())];
      set => this[CustomizedTypeManager.GetTypeNotCustomized(graph.GetType())] = value;
    }

    public TValue this[System.Type graphType]
    {
      get => this[graphType.FullName];
      set => this[graphType.FullName] = value;
    }
  }

  private enum MoveMenuActionResult
  {
    Success,
    ActionNotInMenu,
    PreviousActionNotInMenu,
  }

  private class PXPreventRecursionInConditionsScope : IDisposable
  {
    private readonly bool _previousValue;

    public PXPreventRecursionInConditionsScope()
    {
      this._previousValue = PXContext.GetSlot<bool>("Workflow.PreventRecursionInCalculation");
      PXContext.SetSlot<bool>("Workflow.PreventRecursionInCalculation", true);
    }

    public void Dispose()
    {
      PXContext.SetSlot<bool>("Workflow.PreventRecursionInCalculation", this._previousValue);
    }

    public static bool IsInScope()
    {
      return PXContext.GetSlot<bool>("Workflow.PreventRecursionInCalculation");
    }
  }

  /// <summary>Data to execute workflow transition</summary>
  private class TransitionMovementData
  {
    public TransitionMovementData(
      AUWorkflow workflow,
      string stateId,
      string actionName,
      string stateFieldName,
      IEnumerable<AUWorkflowTransition> possibleTranstions,
      IReadOnlyDictionary<string, Lazy<bool>> conditions)
    {
      this.Workflow = workflow;
      this.StateId = stateId;
      this.StateFieldName = stateFieldName;
      this.PossibleTranstions = possibleTranstions;
      this.ActionName = actionName;
      this.Conditions = conditions;
    }

    public AUWorkflow Workflow { get; private set; }

    public string StateFieldName { get; private set; }

    public IEnumerable<AUWorkflowTransition> PossibleTranstions { get; }

    public string StateId { get; private set; }

    public string ActionName { get; private set; }

    public string ActionForm { get; set; }

    public IReadOnlyDictionary<string, Lazy<bool>> Conditions { get; private set; }
  }
}
