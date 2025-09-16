// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowActionsEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac.Features.AttributeFilters;
using PX.Api;
using PX.Async.Internal;
using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.Automation.Services;
using PX.Data.Automation.State;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.WorkflowAPI;
using Serilog;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.SM;

internal class AUWorkflowActionsEngine : IAUWorkflowActionsEngine
{
  private readonly IAUWorkflowEngine _auWorkflowEngine;
  private readonly IAUWorkflowFormsEngine _auWorkflowFormsEngine;
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;
  private readonly IWorkflowConditionEvaluateService _workflowConditionEvaluateService;
  private readonly INavigationExpressionEvaluator _workflowFieldValueEvaluator;
  private readonly ILogger _logger;
  private const string AlreadyRunActionSequencesKey = "AlreadyRunActionSequences";
  private const string ActionMenuName = "Action";

  public AUWorkflowActionsEngine(
    IAUWorkflowEngine auWorkflowEngine,
    IAUWorkflowFormsEngine auWorkflowFormsEngine,
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService,
    IWorkflowConditionEvaluateService workflowConditionEvaluateService,
    ILogger logger,
    [KeyFilter("WorkflowFieldExpressionEvaluator")] INavigationExpressionEvaluator workflowFieldValueEvaluator)
  {
    this._auWorkflowEngine = auWorkflowEngine;
    this._auWorkflowFormsEngine = auWorkflowFormsEngine;
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
    this._workflowConditionEvaluateService = workflowConditionEvaluateService;
    this._workflowFieldValueEvaluator = workflowFieldValueEvaluator;
    this._logger = logger.ForContext<AUWorkflowActionsEngine>();
  }

  private string FindCurrentActionMenuName(PXGraph graph, string actionName)
  {
    foreach (string key in (IEnumerable) graph.Actions.Keys)
    {
      PXAction action = graph.Actions[key];
      if (action._Menus != null && ((IEnumerable<ButtonMenu>) action._Menus).Any<ButtonMenu>((Func<ButtonMenu, bool>) (b => string.Equals(b.Command, actionName, StringComparison.OrdinalIgnoreCase))))
        return key;
    }
    return (string) null;
  }

  public void InitActions(PXGraph graph, object row, Screen screen)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView))
      return;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out stateID);
    if (workflowAndState == null)
    {
      this.InitActionsWithoutWorkflow(graph, row, screen);
    }
    else
    {
      Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions = WorkflowCommonService.GetSpecialGraphActions(graph);
      this.InitActions(graph, row, screen, screenIdFromGraphType, workflowAndState, stateID, specialGraphActions);
    }
  }

  public void InitActions(
    PXGraph graph1,
    object row,
    Screen screen,
    string screenID,
    AUWorkflow workflow,
    string stateID,
    Dictionary<PXSpecialButtonType, Dictionary<string, PXAction>> specialGraphActions)
  {
    AUWorkflowActionsEngine.Slot locallyCachedSlot = AUWorkflowActionsEngine.Slot.LocallyCachedSlot;
    List<AUScreenActionBaseState> list = AUWorkflowActionsEngine.GetActionDefinitions(screenID, locallyCachedSlot).ToList<AUScreenActionBaseState>();
    AUWorkflowStateAction[] actionStatesForScreen = this.GetAllActionStatesForScreen(screenID, locallyCachedSlot);
    AUWorkflowStateAction[] screenAndWorkflow = this.GetAllActionStatesForScreenAndWorkflow(screenID, workflow.WorkflowID, workflow.WorkflowSubID, locallyCachedSlot);
    IEnumerable<AUWorkflowStateAction> actionStatesRecursive = this.GetActionStatesRecursive(screenID, workflow.WorkflowID, workflow.WorkflowSubID, stateID, locallyCachedSlot);
    IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(screenID, workflow.WorkflowID, workflow.WorkflowSubID, stateID);
    List<string> second = new List<string>();
    if (transitionsRecursive != null)
      second.AddRange(transitionsRecursive.Select<AUWorkflowTransition, string>((Func<AUWorkflowTransition, string>) (it => it.ActionName)));
    PXCache primaryCache = graph1.Caches[graph1.PrimaryItemType];
    foreach (string str in ((IEnumerable<AUWorkflowStateAction>) actionStatesForScreen).Select<AUWorkflowStateAction, string>((Func<AUWorkflowStateAction, string>) (it => it.ActionName)).Union<string>((IEnumerable<string>) second).Union<string>(list.Select<AUScreenActionBaseState, string>((Func<AUScreenActionBaseState, string>) (it => it.ActionName))).Distinct<string>())
    {
      string actionStateName = str;
      PXAction action = graph1.Actions[actionStateName];
      if (action != null)
      {
        string actionName = actionStateName;
        PXAction pxAction1 = (PXAction) null;
        if (screen != null)
        {
          ScreenActionBase settings;
          screen.Actions.TryGetValue(actionName, out settings);
          if (settings != null)
          {
            string forSpecialMenuType = WorkflowCommonService.FindMenuFolderNameForSpecialMenuType(specialGraphActions, settings, settings.MenuFolder);
            pxAction1 = !string.IsNullOrEmpty(forSpecialMenuType) ? this.GetOrCreateActionMenu(graph1, settings.MenuFolderType, forSpecialMenuType) : (PXAction) null;
          }
          else
          {
            string currentActionMenuName = this.FindCurrentActionMenuName(graph1, actionName);
            pxAction1 = !string.IsNullOrEmpty(currentActionMenuName) ? graph1.Actions[currentActionMenuName] : (PXAction) null;
          }
        }
        bool flag1 = ((IEnumerable<AUWorkflowStateAction>) screenAndWorkflow).Any<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => it.ActionName.Equals(actionStateName, StringComparison.OrdinalIgnoreCase)));
        AUWorkflowStateAction workflowStateAction = actionStatesRecursive.FirstOrDefault<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => it.ActionName.Equals(actionStateName, StringComparison.OrdinalIgnoreCase)));
        bool flag2 = false;
        if (!action.AutomationDisabled)
        {
          pxAction1?.SetEnabledInternal(actionName, false, false);
          action.AutomationDisabled = true;
          flag2 = true;
        }
        AUScreenActionBaseState actionDefinition = list.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
        action.BeforeRunHandler = (Action<PXAction, PXAdapter>) null;
        action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.PersistBeforeAction(primaryCache.Current, act, adapter, actionName, actionDefinition));
        action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionSetWorkflowObjectKeys(primaryCache.Current, actionName, act, adapter, actionDefinition, (Func<PXGraph, Dictionary<string, object>>) (graph2 => graph2.BusinessProcessEventProcessor.GetViewsFieldValuesIfActionEventsExist(graph2, screenID, actionName))));
        action.AutomationConnotation = workflowStateAction?.Connotation;
        if (!flag1 && ((IEnumerable<AUWorkflowStateAction>) actionStatesForScreen).Any<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => string.Equals(it.ActionName, actionStateName, StringComparison.OrdinalIgnoreCase))))
        {
          action.AutomationHidden = true;
        }
        else
        {
          bool? nullable;
          if (workflowStateAction == null && !action.AutomationHidden && pxAction1 != null)
          {
            PXAction pxAction2 = action;
            AUScreenActionBaseState screenActionBaseState = actionDefinition;
            bool? displayOnMainToolBar;
            if (screenActionBaseState == null)
            {
              nullable = new bool?();
              displayOnMainToolBar = nullable;
            }
            else
              displayOnMainToolBar = screenActionBaseState.IsTopLevel;
            nullable = (bool?) actionDefinition?.IsLockedOnToolbar;
            bool? isLockedOnToolbar = nullable ?? action?.GetIsLockedOnToolbar();
            string category = actionDefinition?.Category;
            int num = !WorkflowCommonService.IsActionDisplayedOnMainToolbar(displayOnMainToolBar, isLockedOnToolbar, category) ? 1 : 0;
            pxAction2.WorkflowHiddenOnMainToolbar = num != 0;
          }
          action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionShowFormAndApplyParams(graph1, primaryCache.Current, actionDefinition, adapter, act, screenID, actionName, screen));
          if (actionDefinition != null)
          {
            IEnumerable<AUWorkflowActionUpdateField> fields = this.GetActionFields(locallyCachedSlot, actionDefinition);
            if (fields != null && fields.Any<AUWorkflowActionUpdateField>())
              action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionsFieldUpdate(graph1, primaryCache.Current, actionDefinition, fields, primaryCache, adapter));
          }
          action.AfterRunHandler = (Action<PXAction, PXAdapter, Exception>) null;
          if (((IEnumerable<AUWorkflowActionSequence>) AUWorkflowActionsEngine.GetNextActionSequences(actionDefinition).ToArray<AUWorkflowActionSequence>()).Any<AUWorkflowActionSequence>())
            action.AfterRunHandler += (Action<PXAction, PXAdapter, Exception>) ((_, adapter, exception) => this.RunActionSequences(graph1, actionName, screen, screenID, adapter.MassProcess, ChainEventArgs.From<Exception>(exception)));
          if (!flag1 || workflowStateAction != null)
          {
            if (pxAction1 != null)
            {
              if (flag2 || !action.AutomationDisabled)
              {
                pxAction1.SetEnabledInternal(actionName, true, false);
                action.AutomationDisabled = false;
              }
            }
            else if (flag2)
              action.AutomationDisabled = false;
            if (workflowStateAction != null)
            {
              nullable = workflowStateAction.IsTopLevel;
              bool flag3 = true;
              if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
                goto label_37;
            }
            AUScreenActionBaseState screenActionBaseState = actionDefinition;
            bool? displayOnMainToolBar;
            if (screenActionBaseState == null)
            {
              nullable = new bool?();
              displayOnMainToolBar = nullable;
            }
            else
              displayOnMainToolBar = screenActionBaseState.IsTopLevel;
            nullable = (bool?) actionDefinition?.IsLockedOnToolbar;
            bool? isLockedOnToolbar = nullable ?? action?.GetIsLockedOnToolbar();
            string category = actionDefinition?.Category;
            if (!WorkflowCommonService.IsActionDisplayedOnMainToolbar(displayOnMainToolBar, isLockedOnToolbar, category))
              continue;
label_37:
            if (pxAction1 != null)
            {
              ButtonMenu buttonMenu = ((IEnumerable<ButtonMenu>) pxAction1._Menus).FirstOrDefault<ButtonMenu>((Func<ButtonMenu, bool>) (it => it.Command.Equals(actionStateName, StringComparison.OrdinalIgnoreCase)));
              if (buttonMenu != null)
              {
                if (buttonMenu.Visible)
                {
                  action.AutomationHidden = false;
                  action.WorkflowHiddenOnMainToolbar = false;
                }
                else
                  action.AutomationHidden = true;
                action.AutomationDisabled = !buttonMenu.Enabled;
              }
            }
          }
        }
      }
    }
  }

  public void RunActionSequences(
    PXGraph graph,
    string actionName,
    Screen screen,
    string screenId,
    bool massProcess,
    ChainEventArgs<Exception> exceptionArgs = null)
  {
    if (exceptionArgs == null)
      exceptionArgs = ChainEventArgs.From<Exception>((Exception) null);
    Exception exception = ((EventArgs<Exception>) exceptionArgs).Value;
    if (this.SuppressCompletions[graph.UID] || !this.HasAnyActionSequences(screenId, actionName))
      return;
    if (PXLongOperation.IsLongRunOperation)
    {
      switch (exception)
      {
        case null:
        case PXOperationCompletedException _:
          this.RunActionSequencesInternal(graph, actionName, screen, screenId, massProcess, (Exception) null);
          break;
        default:
          this.RunActionSequencesInternal(graph, actionName, screen, screenId, massProcess, exception);
          exceptionArgs.Handled = true;
          break;
      }
    }
    else
    {
      if (exception != null)
        return;
      if (graph.IsDirty)
        graph.Persist();
      PXGraph clone = graph.Clone<PXGraph>();
      clone.ApplyWorkflowState(clone.GetPrimaryCache().Current);
      PXLongOperation.StartOperation(graph, (PXToggleAsyncDelegate) (() => this.RunActionSequencesInternal(clone, actionName, screen, screenId, false, (Exception) null)));
    }
  }

  private void ClearActionSequencesAlreadyRun()
  {
    ((HashSet<string>) PXLongOperation.GetCustomInfoForCurrentThread("AlreadyRunActionSequences"))?.Clear();
  }

  private bool IsActionSequenceAlreadyRun(string actionName)
  {
    HashSet<string> forCurrentThread = (HashSet<string>) PXLongOperation.GetCustomInfoForCurrentThread("AlreadyRunActionSequences");
    return forCurrentThread != null && forCurrentThread.Contains(actionName);
  }

  private void AddActionSequenceAlreadyRun(string actionName)
  {
    HashSet<string> info = (HashSet<string>) PXLongOperation.GetCustomInfoForCurrentThread("AlreadyRunActionSequences");
    if (info == null)
    {
      info = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), "AlreadyRunActionSequences", (object) info);
    }
    info.Add(actionName);
  }

  private void RunActionSequencesInternal(
    PXGraph graph,
    string actionName,
    Screen screen,
    string screenId,
    bool massProcess,
    Exception exception)
  {
    if (ActionSequencesContext.InContext())
      return;
    using (new ActionSequencesContext())
    {
      if (massProcess)
      {
        this.ClearActionSequencesAlreadyRun();
        ActionSequencesLog.ClearLog();
      }
      else if (this.IsActionSequenceAlreadyRun(actionName))
        return;
      this.AddActionSequenceAlreadyRun(actionName);
      PXAction action = graph.Actions[actionName];
      try
      {
        switch (exception)
        {
          case PXRedirectRequiredException exception1:
            ActionSequencesLog.LogRedirect(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), (PXBaseRedirectException) exception1, massProcess);
            this._logger.Information<string>("The {0} action has completed.", action.GetCaption());
            this.RunActionSequencesRecursive(graph, actionName, screen, screenId, massProcess);
            break;
          case PXReportRequiredException exception2:
            ActionSequencesLog.LogRedirect(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), (PXBaseRedirectException) exception2, massProcess);
            this._logger.Information<string>("The {0} action has completed.", action.GetCaption());
            this.RunActionSequencesRecursive(graph, actionName, screen, screenId, massProcess);
            break;
          case null:
            ActionSequencesLog.Log(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), action.GetCaption(), massProcess);
            this._logger.Information<string>("The {0} action has completed.", action.GetCaption());
            this.RunActionSequencesRecursive(graph, actionName, screen, screenId, massProcess);
            break;
          default:
            ActionSequencesLog.Log(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has failed.", (object) action.GetCaption()), action.GetCaption(), massProcess, exception);
            this._logger.Error<string>(exception, "The {0} action has failed. The system did not execute other actions because the Stop on Error check box had been selected for this action.", action.GetCaption());
            break;
        }
      }
      catch (Exception ex)
      {
      }
    }
  }

  private void RunActionSequencesRecursive(
    PXGraph graph,
    string actionName,
    Screen screen,
    string screenId,
    bool massProcess)
  {
    List<AUScreenActionBaseState> list = this.GetActionDefinitions(screenId).ToList<AUScreenActionBaseState>();
    foreach (AUWorkflowActionSequence nextActionSequence in AUWorkflowActionsEngine.GetNextActionSequences(list.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (a => string.Equals(a.ActionName, actionName, StringComparison.OrdinalIgnoreCase)))))
    {
      AUWorkflowActionSequence actionSequence = nextActionSequence;
      if (!this.IsActionSequenceAlreadyRun(actionSequence.NextActionName))
      {
        this.AddActionSequenceAlreadyRun(actionSequence.NextActionName);
        if (graph.Actions[actionSequence.NextActionName] != null)
        {
          if (graph.IsDirty)
            graph.Persist();
          graph.SelectTimeStamp();
          PXCache primaryCache = graph.GetPrimaryCache();
          AUWorkflowActionsEngine.RefreshCurrent(primaryCache);
          object current = primaryCache.Current;
          PXAdapter adapter = AUWorkflowActionsEngine.CreateAdapter(graph, current);
          PXAction action = graph.Actions[actionSequence.NextActionName];
          if (!string.Equals(actionSequence.Condition, "true", StringComparison.OrdinalIgnoreCase) && !this._workflowConditionEvaluateService.EvaluateConditions(graph, current, screen, (string) null, (IReadOnlyDictionary<string, object>) null, screenId: screenId)[actionSequence.Condition].Value)
          {
            this._logger.Information<string, string>("The {0} action has not been executed because of the {1} condition.", action.GetCaption(), actionSequence.Condition);
          }
          else
          {
            bool flag1 = false;
            try
            {
              string stateID;
              AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, current, out stateID);
              if (workflowAndState != null && !this.GetActionStatesRecursive(screenId, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID).Any<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (a => string.Equals(a.ActionName, actionSequence.NextActionName, StringComparison.OrdinalIgnoreCase))))
                throw new PXActionDisabledException("The {0} action is unavailable in the {1} state.", new object[2]
                {
                  (object) action.GetCaption(),
                  (object) this.GetStateDisplayName(graph)
                });
              AUScreenActionBaseState actionDefinition = list.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionSequence.NextActionName, StringComparison.OrdinalIgnoreCase)));
              this.FillForm(primaryCache, screen, actionSequence, action, actionDefinition, current);
              try
              {
                using (new WorkflowSyncScope())
                  EnumerableExtensions.Consume(action.Press(adapter));
                ActionSequencesLog.Log(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), action.GetCaption(), massProcess);
              }
              catch (PXRedirectRequiredException ex)
              {
                ActionSequencesLog.LogRedirect(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), (PXBaseRedirectException) ex, massProcess);
              }
              catch (PXReportRequiredException ex)
              {
                ActionSequencesLog.LogRedirect(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has completed.", (object) action.GetCaption()), (PXBaseRedirectException) ex, massProcess);
              }
              this._logger.Information<string>("The {0} action has completed.", action.GetCaption());
              flag1 = true;
            }
            catch (Exception ex)
            {
              bool flag2 = ex is PXDialogRequiredException;
              string str;
              if (!flag2)
                str = (string) null;
              else
                str = PXMessages.LocalizeFormatNoPrefixNLA("The {0} action opens a dialog box and cannot be executed in this mode.", (object) action.GetCaption());
              string errorMessage = str;
              if (flag2)
                this._logger.Error<string>("The action opens a dialog box that is not supported in a sequence of actions. Modify the action's source code so that it does not display this dialog box when it runs in a sequence. Alternatively, run the action manually by clicking {0} on the More menu.", action.GetCaption());
              if (((int) actionSequence.StopOnError ?? 1) != 0)
              {
                ActionSequencesLog.Log(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has failed.", (object) action.GetCaption()), action.GetCaption(), massProcess, ex, errorMessage);
                this._logger.Error<string>(ex, "The {0} action has failed. The system did not execute other actions because the Stop on Error check box had been selected for this action.", action.GetCaption());
                throw;
              }
              ActionSequencesLog.Log(PXMessages.LocalizeFormatNoPrefixNLA("The {0} action has failed.", (object) action.GetCaption()), action.GetCaption(), massProcess, ex, errorMessage);
              this._logger.Error<string>(ex, "The {0} action has failed. The system executed other actions because the Stop on Error check box had been cleared for this action.", action.GetCaption());
            }
            if (flag1)
              this.RunActionSequencesRecursive(graph, actionSequence.NextActionName, screen, screenId, massProcess);
          }
        }
      }
    }
  }

  private string GetStateDisplayName(PXGraph graph)
  {
    PXCache primaryCache = graph.GetPrimaryCache();
    object current = primaryCache.Current;
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, current, out stateID);
    AUWorkflowState state = this._auWorkflowEngine.GetState(graph, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
    if (!(primaryCache.GetStateExt((object) null, this._auWorkflowEngine.GetScreenWorkflows(graph).StateField) is PXStringState stateExt) || stateExt.AllowedValues == null)
      return stateID;
    string str;
    stateExt.ValueLabelDic.TryGetValue(state.Identifier, out str);
    return str ?? stateID;
  }

  private static PXAdapter CreateAdapter(PXGraph graph, object currentRow)
  {
    BqlCommand bqlSelect = graph.Views[graph.PrimaryView].BqlSelect;
    return new PXAdapter((PXView) new PXView.Dummy(graph, bqlSelect, new List<object>()
    {
      currentRow
    }))
    {
      AllowRedirect = true,
      ForceButtonEnabledCheck = true
    };
  }

  private void FillForm(
    PXCache cache,
    Screen screen,
    AUWorkflowActionSequence sequence,
    PXAction action,
    AUScreenActionBaseState actionDefinition,
    object row)
  {
    if (actionDefinition == null || actionDefinition.Form == null)
      return;
    this._auWorkflowFormsEngine.PrepareFormData(action.Graph, actionDefinition.Form, true, screen);
    IEnumerable<AUWorkflowActionSequenceFormFieldValue> sequenceFormFields = this.GetActionSequenceFormFields(sequence);
    if (sequenceFormFields != null)
    {
      Dictionary<string, object> dictionary = EnumerableExtensions.ToDictionary<string, object>((IEnumerable<KeyValuePair<string, object>>) this._auWorkflowFormsEngine.GetFormValues(action.Graph, actionDefinition.Form));
      foreach (AUWorkflowActionSequenceFormFieldValue sequenceFormFieldValue in sequenceFormFields)
      {
        string fieldName = sequenceFormFieldValue.FieldName;
        dictionary[fieldName] = sequenceFormFieldValue.Value != null ? this._workflowFieldValueEvaluator.Evaluate(action.Graph, row, sequenceFormFieldValue.Value, new bool?(sequenceFormFieldValue.IsFromScheme.GetValueOrDefault())) : (object) null;
      }
      this._auWorkflowFormsEngine.SetFormValues(action.Graph, actionDefinition.Form, (IDictionary<string, object>) dictionary);
    }
    DialogManager.SetAnswer(action.Graph, "FilterPreview", actionDefinition.Form, WebDialogResult.OK);
    PXContext.SetSlot<IReadOnlyDictionary<string, object>>("Workflow.CurrentWorkflowFormData", this._auWorkflowFormsEngine.GetFormValues(action.Graph, actionDefinition.Form));
  }

  private static void RefreshCurrent(PXCache cache)
  {
    object current = cache.Current;
    object persistedRecord = PXTimeStampScope.GetPersistedRecord(cache, current);
    object obj1 = cache.Locate(current);
    if (persistedRecord != null && persistedRecord != current && cache.GetItemType().IsInstanceOfType(persistedRecord))
    {
      cache.Remove(current);
      cache.Hold(persistedRecord);
      object obj2 = persistedRecord;
      cache.Current = obj2;
    }
    else
      cache.RaiseRowSelected(cache.Current);
    if (persistedRecord == null || persistedRecord == obj1 || obj1 == null || !cache.GetItemType().IsInstanceOfType(persistedRecord))
      return;
    cache.Remove(obj1);
    cache.Hold(persistedRecord);
  }

  private void PersistBeforeAction(
    object row,
    PXAction act,
    PXAdapter adapter,
    string actionName,
    AUScreenActionBaseState actionDefinition)
  {
    PXGraph graph = act.Graph;
    string fullName = CustomizedTypeManager.GetTypeNotCustomized(graph.GetType()).FullName;
    if (actionDefinition == null)
      return;
    bool? disablePersist = actionDefinition.DisablePersist;
    bool flag = false;
    if (!(disablePersist.GetValueOrDefault() == flag & disablePersist.HasValue) || PXWorkflowService.PreventSaveOnAction[fullName] || !string.IsNullOrEmpty(actionDefinition.Form) && this._auWorkflowFormsEngine.HasChanges(graph))
      return;
    this.ThrowIfRowDoesNotBelongToTheCacheItemsList(row, graph.GetPrimaryCache());
    int status = (int) graph.GetPrimaryCache().GetStatus(row);
    graph.Persist();
    if (AUWorkflowActionsEngine.HasError(graph))
      throw new PXException($"Cannot save changes to the database and execute the {actionName} action due to a validation error in the {fullName} graph.");
    if (status != 2 || adapter.Searches == null || adapter.SortColumns == null || adapter.Searches.Length != adapter.SortColumns.Length || !((IEnumerable<object>) adapter.Searches).All<object>((Func<object, bool>) (_ => _ != null)) || !((IEnumerable<string>) adapter.SortColumns).All<string>((Func<string, bool>) (_ => _ != null)))
      return;
    for (int index = 0; index < adapter.SortColumns.Length; ++index)
      adapter.Searches[index] = PXFieldState.UnwrapValue(adapter.View.Cache.GetValueExt(row, adapter.SortColumns[index]));
  }

  private static bool HasError(PXGraph graph)
  {
    foreach (System.Type key in graph.Views.Caches.ToArray())
    {
      if (graph.Caches.ContainsKey(key))
      {
        foreach (PXEventSubscriberAttribute attribute in graph.Caches[key].GetAttributes((string) null))
        {
          if (attribute is IPXInterfaceField pxInterfaceField && !string.IsNullOrEmpty(pxInterfaceField.ErrorText) && (pxInterfaceField.ErrorLevel == PXErrorLevel.Error || pxInterfaceField.ErrorLevel == PXErrorLevel.RowError))
            return true;
        }
      }
    }
    return false;
  }

  public void InitActionsWithoutWorkflow(PXGraph graph1, object row, Screen screen)
  {
    string screenId = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph1.GetType());
    if (screenId == null || string.IsNullOrEmpty(graph1.PrimaryView))
      return;
    List<AUScreenActionBaseState> list = this.GetActionDefinitions(screenId).ToList<AUScreenActionBaseState>();
    PXCache primaryCache = graph1.Caches[graph1.PrimaryItemType];
    AUWorkflowActionsEngine.Slot locallyCachedSlot = AUWorkflowActionsEngine.Slot.LocallyCachedSlot;
    foreach (AUScreenActionBaseState screenActionBaseState in list)
    {
      AUScreenActionBaseState actionDefinition = screenActionBaseState;
      string actionName1 = actionDefinition.ActionName;
      PXAction action = graph1.Actions[actionName1];
      if (action != null)
      {
        string actionName = (action.GetState((object) null) is PXButtonState state ? state.Name : (string) null) ?? actionName1;
        action.BeforeRunHandler = (Action<PXAction, PXAdapter>) null;
        action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.PersistBeforeAction(primaryCache.Current, act, adapter, actionName, actionDefinition));
        action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionSetWorkflowObjectKeys(primaryCache.Current, actionName, act, adapter, actionDefinition, (Func<PXGraph, Dictionary<string, object>>) (graph2 => graph2.BusinessProcessEventProcessor.GetViewsFieldValuesIfActionEventsExist(graph2, screenId, actionName))));
        action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionShowFormAndApplyParams(graph1, primaryCache.Current, actionDefinition, adapter, act, screenId, actionName, (Screen) null));
        IEnumerable<AUWorkflowActionUpdateField> fields = this.GetActionFields(locallyCachedSlot, actionDefinition);
        if (fields != null && fields.Any<AUWorkflowActionUpdateField>())
          action.BeforeRunHandler += (Action<PXAction, PXAdapter>) ((act, adapter) => this.OnActionsFieldUpdate(graph1, primaryCache.Current, actionDefinition, fields, primaryCache, adapter));
        action.AfterRunHandler = (Action<PXAction, PXAdapter, Exception>) null;
        if (((IEnumerable<AUWorkflowActionSequence>) AUWorkflowActionsEngine.GetNextActionSequences(actionDefinition).ToArray<AUWorkflowActionSequence>()).Any<AUWorkflowActionSequence>())
          action.AfterRunHandler += (Action<PXAction, PXAdapter, Exception>) ((_, adapter, exception) => this.RunActionSequences(graph1, actionName, screen, screenId, adapter.MassProcess, ChainEventArgs.From<Exception>(exception)));
      }
    }
  }

  /// <summary>
  /// Checks if workflow engine should be executed for selected record and action.
  /// Currently checks existence of any transition from current records state and selected action
  /// and existence of any auto action in current current records
  /// </summary>
  /// <returns></returns>
  private bool IsWorkflowCouldBeExecuted(
    PXGraph graph,
    object row,
    string actionName,
    PXAdapter adapter,
    AUScreenActionBaseState actionDefinition)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null || string.IsNullOrEmpty(graph.PrimaryView) || this._auWorkflowEngine.GetScreenWorkflows(graph) == null)
      return false;
    if (adapter.MassProcess && actionDefinition != null)
    {
      bool? batchMode = actionDefinition.BatchMode;
      bool flag = true;
      if (batchMode.GetValueOrDefault() == flag & batchMode.HasValue)
      {
        foreach (object obj in adapter.Get())
        {
          object row1 = obj is PXResult pxResult ? pxResult[0] : obj;
          if (this.IsWorkflowCouldBeExecutedForRecord(graph, row1, actionName, screenIdFromGraphType))
            return true;
        }
        return false;
      }
    }
    return this.IsWorkflowCouldBeExecutedForRecord(graph, row, actionName, screenIdFromGraphType);
  }

  private bool IsWorkflowCouldBeExecutedForRecord(
    PXGraph graph,
    object row,
    string actionName,
    string screenId)
  {
    string stateID;
    AUWorkflow workflowAndState = this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out stateID);
    if (workflowAndState == null)
      return false;
    IEnumerable<AUWorkflowTransition> transitionsRecursive = this._auWorkflowEngine.GetTransitionsRecursive(graph, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID, actionName);
    if (transitionsRecursive != null && transitionsRecursive.Any<AUWorkflowTransition>())
      return true;
    IEnumerable<AUWorkflowStateAction> actionsRecursive = this.GetAutoActionsRecursive(screenId, workflowAndState.WorkflowID, workflowAndState.WorkflowSubID, stateID);
    return actionsRecursive != null && actionsRecursive.Any<AUWorkflowStateAction>();
  }

  private void OnActionSetWorkflowObjectKeys(
    object row,
    string actionName,
    PXAction act,
    PXAdapter adapter,
    AUScreenActionBaseState actionDefinition,
    Func<PXGraph, Dictionary<string, object>> getViewsFields)
  {
    this.CurrentWorkflowAction = actionName;
    this.CurrentWorkflowObjectType = act.Graph.GetPrimaryCache().BqlTable;
    this.CurrentWorkflowScreenId = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(act.Graph.GetType());
    this.CurrentWorkflowShouldBeExecuted = this.IsWorkflowCouldBeExecuted(act.Graph, row, actionName, adapter, actionDefinition);
    this.CurrentActionViewsValues = (IDictionary<string, object>) getViewsFields(act.Graph);
    if (row == null)
      return;
    PXCache cache = act.Graph.Caches[row.GetType()];
    this.CurrentWorkflowObjectKeys = (IDictionary) cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (cacheKey => cacheKey), (Func<string, object>) (cacheKey => cache.GetValue(row, cacheKey)));
  }

  private void OnActionShowFormAndApplyParams(
    PXGraph graph,
    object row,
    AUScreenActionBaseState actionDefinition,
    PXAdapter adapter,
    PXAction act,
    string screenID,
    string actionName,
    Screen screen)
  {
    if (!string.IsNullOrEmpty(actionDefinition?.Form))
    {
      if (!graph.IsImport && !adapter.MassProcess)
      {
        if (!adapter.InternalCall && !this.IsInAutoAction(graph))
        {
          IAUWorkflowFormsEngine workflowFormsEngine = this._auWorkflowFormsEngine;
          PXGraph graph1 = act.Graph;
          object row1 = row;
          string form = actionDefinition.Form;
          string screenID1 = screenID;
          Screen screen1 = screen;
          string actionName1 = actionName;
          int num;
          if (actionDefinition == null)
          {
            num = 0;
          }
          else
          {
            bool? disablePersist = actionDefinition.DisablePersist;
            bool flag = false;
            num = disablePersist.GetValueOrDefault() == flag & disablePersist.HasValue ? 1 : 0;
          }
          workflowFormsEngine.AskExt(graph1, row1, form, screenID1, screen1, actionName1, num != 0);
        }
        else
          this._auWorkflowFormsEngine.PrepareFormData(act.Graph, actionDefinition.Form, true, screen);
      }
      else
        this._auWorkflowFormsEngine.PrepareFormData(act.Graph, actionDefinition.Form, this.IsInAutoAction(graph), screen);
      PXContext.SetSlot<IReadOnlyDictionary<string, object>>("Workflow.CurrentWorkflowFormData", this._auWorkflowFormsEngine.GetFormValues(graph, actionDefinition.Form));
    }
    IEnumerable<KeyValuePair<string, object>> parameters = this.GetParameters(act.Graph, actionName, row);
    if (parameters == null)
      return;
    foreach (KeyValuePair<string, object> keyValuePair in parameters)
    {
      object obj;
      if (!adapter.Arguments.TryGetValue(keyValuePair.Key, out obj) || obj == null)
        adapter.Arguments[keyValuePair.Key] = keyValuePair.Value;
    }
  }

  internal bool IsInAutoAction(PXGraph graph)
  {
    List<string> appliedAutoAction = PXWorkflowService.AppliedAutoActions[graph];
    // ISSUE: explicit non-virtual call
    return (appliedAutoAction != null ? __nonvirtual (appliedAutoAction.Count) : 0) > 0;
  }

  private void OnActionsFieldUpdate(
    PXGraph graph,
    object row,
    AUScreenActionBaseState actionDefinition,
    IEnumerable<AUWorkflowActionUpdateField> fields,
    PXCache primaryCache,
    PXAdapter adapter)
  {
    if (adapter.MassProcess && actionDefinition != null)
    {
      bool? batchMode = actionDefinition.BatchMode;
      bool flag = true;
      if (batchMode.GetValueOrDefault() == flag & batchMode.HasValue && PXProcessing.GetProcessingInfo() != null)
      {
        IEnumerator enumerator = adapter.Get().GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            object row1 = current is PXResult pxResult ? pxResult[0] : current;
            IReadOnlyDictionary<string, object> forMassProcessing = this._auWorkflowFormsEngine.GetFormValuesForMassProcessing(graph, row1);
            this.ActionsFieldUpdateInternal(row1, actionDefinition, fields, primaryCache, forMassProcessing);
          }
          return;
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      }
    }
    IReadOnlyDictionary<string, object> formValues = this._auWorkflowFormsEngine.GetFormValues(graph, actionDefinition?.Form);
    this.ActionsFieldUpdateInternal(row, actionDefinition, fields, primaryCache, formValues);
  }

  private void ThrowIfRowDoesNotBelongToTheCacheItemsList(object row, PXCache cache)
  {
    if (row == null)
      return;
    object obj = cache.Locate(row);
    if (obj != null && obj != row)
      throw new InvalidOperationException("The \"Current\" property of the cache refers to a record that is not available in the list of items in the cache.");
  }

  private void ActionsFieldUpdateInternal(
    object row,
    AUScreenActionBaseState actionDefinition,
    IEnumerable<AUWorkflowActionUpdateField> fields,
    PXCache primaryCache,
    IReadOnlyDictionary<string, object> formValues)
  {
    object copy = primaryCache.CreateCopy(row);
    bool flag1 = this.ApplyFieldUpdates((IEnumerable<IWorkflowUpdateField>) fields, primaryCache, row, formValues);
    if (flag1)
    {
      this.ThrowIfRowDoesNotBelongToTheCacheItemsList(row, primaryCache);
      primaryCache.MarkUpdated(row);
      if (!primaryCache.HasPendingValues(row))
        primaryCache.RaiseRowUpdated(row, copy);
      primaryCache.IsDirty = true;
    }
    if (!(!this.IsInAutoAction(primaryCache.Graph) & flag1))
      return;
    int num;
    if (primaryCache.GetStatus(row) != PXEntryStatus.Inserted)
    {
      if (actionDefinition == null)
      {
        num = 1;
      }
      else
      {
        bool? disablePersist = actionDefinition.DisablePersist;
        bool flag2 = true;
        num = !(disablePersist.GetValueOrDefault() == flag2 & disablePersist.HasValue) ? 1 : 0;
      }
    }
    else
      num = 0;
    if (num == 0)
      return;
    this._auWorkflowEngine.SetSaveAfterActionSlot(primaryCache.Graph);
  }

  public bool ApplyFieldUpdates(
    IEnumerable<IWorkflowUpdateField> fields,
    PXCache cache,
    object currentRecord,
    IReadOnlyDictionary<string, object> formValues)
  {
    bool flag1 = false;
    if (fields != null && fields.Any<IWorkflowUpdateField>())
    {
      foreach (IWorkflowUpdateField field in fields)
      {
        bool? isFromScheme = field.IsFromScheme;
        bool flag2 = false;
        if (isFromScheme.GetValueOrDefault() == flag2 & isFromScheme.HasValue)
        {
          if (string.IsNullOrWhiteSpace(field.Value))
            cache.SetValueExt(currentRecord, field.FieldName, (object) null);
          else
            cache.SetValueExt(currentRecord, field.FieldName, WorkflowExpressionParser.Eval(cache, field.FieldName, field.Value, formValues, currentRecord));
          flag1 = true;
        }
        else
        {
          object obj = (object) field.Value;
          if (cache.GetStateExt((object) null, field.FieldName) is PXDateState)
            obj = !string.IsNullOrWhiteSpace(field.Value) ? (!RelativeDatesManager.IsRelativeDatesString(field.Value) ? (object) Convert.ToDateTime(field.Value, (IFormatProvider) CultureInfo.InvariantCulture) : (object) RelativeDatesManager.EvaluateAsDateTime(field.Value)) : (object) null;
          if (field.Value != null)
          {
            if (field.Value.Equals("@me", StringComparison.OrdinalIgnoreCase))
              obj = WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(cache, field.FieldName);
            else if (field.Value.Equals("@branch", StringComparison.OrdinalIgnoreCase))
              obj = (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch();
          }
          cache.SetValueExt(currentRecord, field.FieldName, obj);
          flag1 = true;
        }
      }
    }
    return flag1;
  }

  public string GetCategoryDisplayName(PXGraph graph, string categoryName)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return WorkflowCommonService.GetActionMenuDisplayName(categoryName);
    string displayName = ((IEnumerable<AUWorkflowCategory>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screenIdFromGraphType).IndexOnlyByScreenActionCategories).FirstOrDefault<AUWorkflowCategory>((Func<AUWorkflowCategory, bool>) (it => it.CategoryName == categoryName))?.DisplayName;
    return displayName != null ? PXLocalizer.Localize(displayName) : WorkflowCommonService.GetActionMenuDisplayName(categoryName);
  }

  public IEnumerable<AUWorkflowCategory> GetOrderedActionCategories(string screenID)
  {
    return this.GetOrderedActionCategories(screenID, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  public IEnumerable<AUWorkflowCategory> GetOrderedActionCategories(
    string screenID,
    AUWorkflowActionsEngine.Slot slot)
  {
    return screenID == null ? Enumerable.Empty<AUWorkflowCategory>() : (IEnumerable<AUWorkflowCategory>) slot.Get(screenID).IndexOnlyByScreenActionCategories;
  }

  public IEnumerable<AUScreenActionBaseState> GetActionDefinitions(string screenID)
  {
    return AUWorkflowActionsEngine.GetActionDefinitions(screenID, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  private static IEnumerable<AUScreenActionBaseState> GetActionDefinitions(
    string screenID,
    AUWorkflowActionsEngine.Slot slot)
  {
    return screenID == null ? Enumerable.Empty<AUScreenActionBaseState>() : (IEnumerable<AUScreenActionBaseState>) slot.Get(screenID).IndexByScreenActionDefinitions;
  }

  public IEnumerable<string> GetEnabledActionsForState(string screen, string state)
  {
    return ((IEnumerable<(AUWorkflowStateAction, string, string)>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screen).IndexOnlyByScreenWorkflowState).Where<(AUWorkflowStateAction, string, string)>((Func<(AUWorkflowStateAction, string, string), bool>) (it => it.StateAction.StateName == state)).Select<(AUWorkflowStateAction, string, string), string>((Func<(AUWorkflowStateAction, string, string), string>) (it => it.StateAction.ActionName)).Distinct<string>();
  }

  public IEnumerable<(string workflowId, string workflowSubId, string stateName)> GetStatesWithAction(
    string screen,
    string actionName)
  {
    return ((IEnumerable<(AUWorkflowStateAction, string, string)>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screen).IndexOnlyByScreenWorkflowState).Where<(AUWorkflowStateAction, string, string)>((Func<(AUWorkflowStateAction, string, string), bool>) (it => it.StateAction.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase))).Select<(AUWorkflowStateAction, string, string), (string, string, string)>((Func<(AUWorkflowStateAction, string, string), (string, string, string)>) (it => (it.WorkflowID, it.WorkflowSubID, it.StateAction.StateName)));
  }

  private PXAction GetOrCreateActionMenu(
    PXGraph graph,
    PXSpecialButtonType? menuFolderType,
    string menuFolder)
  {
    if (menuFolder == null)
      menuFolder = "Action";
    PXAction actionMenu = graph.Actions[menuFolder];
    if (actionMenu == null)
    {
      PXButtonAttribute pxButtonAttribute = (PXButtonAttribute) null;
      if (menuFolderType.HasValue)
      {
        pxButtonAttribute = PXEventSubscriberAttribute.CreateInstance<PXButtonAttribute>();
        pxButtonAttribute.SpecialType = menuFolderType.Value;
      }
      actionMenu = PXNamedAction.AddAction(graph, graph.PrimaryItemType, menuFolder, this.GetActionMenuDisplayName(menuFolder), (PXButtonDelegate) (adapter => adapter.Get()), (PXEventSubscriberAttribute) pxButtonAttribute);
      actionMenu.MenuAutoOpen = true;
    }
    return actionMenu;
  }

  private string GetActionMenuDisplayName(string menuFolder)
  {
    string message;
    switch (menuFolder)
    {
      case "Action":
        message = "Actions";
        break;
      case "Report":
        message = "Reports";
        break;
      case "Inquiry":
        message = "Inquiries";
        break;
      default:
        message = menuFolder;
        break;
    }
    return PXLocalizer.Localize(message);
  }

  private AUWorkflowStateAction[] GetAllActionStatesForScreen(string screenID)
  {
    return this.GetAllActionStatesForScreen(screenID, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  private AUWorkflowStateAction[] GetAllActionStatesForScreen(
    string screenID,
    AUWorkflowActionsEngine.Slot slot)
  {
    return screenID == null ? Array.Empty<AUWorkflowStateAction>() : ((IEnumerable<(AUWorkflowStateAction, string, string)>) slot.Get(screenID).IndexOnlyByScreenWorkflowState).Select<(AUWorkflowStateAction, string, string), AUWorkflowStateAction>((Func<(AUWorkflowStateAction, string, string), AUWorkflowStateAction>) (a => a.StateAction)).ToArray<AUWorkflowStateAction>();
  }

  private AUWorkflowStateAction[] GetAllActionStatesForScreenAndWorkflow(
    string screenID,
    string workflowId,
    string workflowSubId)
  {
    return this.GetAllActionStatesForScreenAndWorkflow(screenID, workflowId, workflowSubId, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  private AUWorkflowStateAction[] GetAllActionStatesForScreenAndWorkflow(
    string screenID,
    string workflowId,
    string workflowSubId,
    AUWorkflowActionsEngine.Slot slot)
  {
    return screenID == null ? Array.Empty<AUWorkflowStateAction>() : slot.Get(screenID).IndexOnlyByScreenAndWorkflowIdWorkflowState[$"{workflowId}.{workflowSubId}"].ToArray<AUWorkflowStateAction>();
  }

  public IEnumerable<AUWorkflowStateAction> GetActionStates(
    string screen,
    string workflowId,
    string workflowSubId,
    string state)
  {
    return this.GetActionStates(screen, workflowId, workflowSubId, state, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  public IEnumerable<AUWorkflowStateAction> GetActionStatesRecursive(
    string screen,
    string workflowId,
    string workflowSubId,
    string state)
  {
    return this.GetActionStatesRecursive(screen, workflowId, workflowSubId, state, AUWorkflowActionsEngine.Slot.LocallyCachedSlot);
  }

  private IEnumerable<AUWorkflowStateAction> GetActionStates(
    string screen,
    string workflowId,
    string workflowSubId,
    string state,
    AUWorkflowActionsEngine.Slot slot)
  {
    if (screen == null)
      return Enumerable.Empty<AUWorkflowStateAction>();
    return (IEnumerable<AUWorkflowStateAction>) slot.Get(screen).IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{state}"].ToArray<AUWorkflowStateAction>();
  }

  private IEnumerable<AUWorkflowStateAction> GetActionStatesRecursive(
    string screen,
    string workflowId,
    string workflowSubId,
    string stateId,
    AUWorkflowActionsEngine.Slot slot)
  {
    if (screen == null)
      return Enumerable.Empty<AUWorkflowStateAction>();
    List<AUWorkflowStateAction> result = new List<AUWorkflowStateAction>();
    AUWorkflowActionsEngine.ScreenActions screenActions = slot.Get(screen);
    result.AddRange(screenActions.IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{stateId}"]);
    for (AUWorkflowState state = this._auWorkflowEngine.GetState(screen, workflowId, workflowSubId, stateId); !string.IsNullOrEmpty(state?.ParentState); state = this._auWorkflowEngine.GetState(screen, workflowId, workflowSubId, state.ParentState))
    {
      IEnumerable<AUWorkflowStateAction> collection = screenActions.IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{state.ParentState}"].Where<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => result.All<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (aState => !string.Equals(aState.ActionName, it.ActionName, StringComparison.OrdinalIgnoreCase)))));
      result.AddRange(collection);
    }
    return (IEnumerable<AUWorkflowStateAction>) result;
  }

  public IEnumerable<AUWorkflowStateAction> GetAutoActions(
    string screen,
    string workflowId,
    string workflowSubId,
    string state)
  {
    if (screen == null)
      return Enumerable.Empty<AUWorkflowStateAction>();
    return (IEnumerable<AUWorkflowStateAction>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screen).IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{state}"].Where<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => !string.IsNullOrEmpty(it.AutoRun) && !it.AutoRun.Equals("false", StringComparison.OrdinalIgnoreCase))).ToArray<AUWorkflowStateAction>();
  }

  public IEnumerable<AUWorkflowStateAction> GetAutoActionsRecursive(
    string screen,
    string workflowId,
    string workflowSubId,
    string state)
  {
    if (screen == null)
      return Enumerable.Empty<AUWorkflowStateAction>();
    List<AUWorkflowStateAction> actionsRecursive = new List<AUWorkflowStateAction>();
    AUWorkflowActionsEngine.ScreenActions screenActions = AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screen);
    actionsRecursive.AddRange(screenActions.IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{state}"].Where<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => !string.IsNullOrEmpty(it.AutoRun) && !it.AutoRun.Equals("false", StringComparison.OrdinalIgnoreCase))));
    for (AUWorkflowState state1 = this._auWorkflowEngine.GetState(screen, workflowId, workflowSubId, state); !string.IsNullOrEmpty(state1?.ParentState); state1 = this._auWorkflowEngine.GetState(screen, workflowId, workflowSubId, state1.ParentState))
      actionsRecursive.AddRange(screenActions.IndexByScreenWorkflowState[$"{workflowId}.{workflowSubId}.{state1.ParentState}"].Where<AUWorkflowStateAction>((Func<AUWorkflowStateAction, bool>) (it => !string.IsNullOrEmpty(it.AutoRun) && !it.AutoRun.Equals("false", StringComparison.OrdinalIgnoreCase))));
    return (IEnumerable<AUWorkflowStateAction>) actionsRecursive;
  }

  public IEnumerable<AUWorkflowActionUpdateField> GetActionFields(AUScreenActionBaseState action)
  {
    List<AUWorkflowActionUpdateField> actionUpdateFieldList;
    return action != null && AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(action.ScreenID).WorkflowStateFields.TryGetValue(action, out actionUpdateFieldList) ? (IEnumerable<AUWorkflowActionUpdateField>) actionUpdateFieldList : Enumerable.Empty<AUWorkflowActionUpdateField>();
  }

  internal IEnumerable<AUWorkflowActionUpdateField> GetActionFields(
    AUWorkflowActionsEngine.Slot slot,
    AUScreenActionBaseState action)
  {
    List<AUWorkflowActionUpdateField> actionUpdateFieldList;
    return action != null && slot.Get(action.ScreenID).WorkflowStateFields.TryGetValue(action, out actionUpdateFieldList) ? (IEnumerable<AUWorkflowActionUpdateField>) actionUpdateFieldList : (IEnumerable<AUWorkflowActionUpdateField>) new List<AUWorkflowActionUpdateField>(0);
  }

  public IEnumerable<AUWorkflowActionParam> GetActionParams(AUScreenActionBaseState action)
  {
    List<AUWorkflowActionParam> workflowActionParamList;
    return action != null && AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(action.ScreenID).WorkflowStateParams.TryGetValue(action, out workflowActionParamList) ? (IEnumerable<AUWorkflowActionParam>) workflowActionParamList : (IEnumerable<AUWorkflowActionParam>) new List<AUWorkflowActionParam>(0);
  }

  IEnumerable<AUWorkflowActionSequence> IAUWorkflowActionsEngine.GetNextActionSequences(
    AUScreenActionBaseState action)
  {
    return AUWorkflowActionsEngine.GetNextActionSequences(action);
  }

  private static IEnumerable<AUWorkflowActionSequence> GetNextActionSequences(
    AUScreenActionBaseState action)
  {
    List<AUWorkflowActionSequence> workflowActionSequenceList;
    return action != null && AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(action.ScreenID).NextActionSequences.TryGetValue(action, out workflowActionSequenceList) ? (IEnumerable<AUWorkflowActionSequence>) workflowActionSequenceList : (IEnumerable<AUWorkflowActionSequence>) new List<AUWorkflowActionSequence>(0);
  }

  public static IEnumerable<AUWorkflowActionSequence> GetNextActionSequences(
    string screenId,
    string actionName)
  {
    AUScreenActionBaseState key = AUWorkflowActionsEngine.GetActionDefinitions(screenId, AUWorkflowActionsEngine.Slot.LocallyCachedSlot).ToList<AUScreenActionBaseState>().FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (a => string.Equals(a.ActionName, actionName, StringComparison.OrdinalIgnoreCase)));
    List<AUWorkflowActionSequence> workflowActionSequenceList;
    return key != null && AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(key.ScreenID).NextActionSequences.TryGetValue(key, out workflowActionSequenceList) ? (IEnumerable<AUWorkflowActionSequence>) workflowActionSequenceList : (IEnumerable<AUWorkflowActionSequence>) new List<AUWorkflowActionSequence>(0);
  }

  public IEnumerable<string> GetActionSequencesActionNames(string screenId)
  {
    if (string.IsNullOrEmpty(screenId))
      return (IEnumerable<string>) Array<string>.Empty;
    AUWorkflowActionsEngine.ScreenActions screenActions = AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screenId);
    return screenActions == null ? (IEnumerable<string>) null : screenActions.NextActionSequences.Select<KeyValuePair<AUScreenActionBaseState, List<AUWorkflowActionSequence>>, string>((Func<KeyValuePair<AUScreenActionBaseState, List<AUWorkflowActionSequence>>, string>) (it => it.Key.ActionName));
  }

  public IEnumerable<string> GetActionSequencesActionNames(PXGraph graph)
  {
    return this.GetActionSequencesActionNames(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()));
  }

  public bool IsActionsCustomized(string screenId)
  {
    return AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(screenId).IsCustomized;
  }

  public IEnumerable<AUWorkflowActionSequenceFormFieldValue> GetActionSequenceFormFields(
    AUWorkflowActionSequence actionSequence)
  {
    List<AUWorkflowActionSequenceFormFieldValue> sequenceFormFieldValueList;
    return actionSequence != null && AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(actionSequence.ScreenID).ActionSequenceFormFields.TryGetValue(actionSequence, out sequenceFormFieldValueList) ? (IEnumerable<AUWorkflowActionSequenceFormFieldValue>) sequenceFormFieldValueList : (IEnumerable<AUWorkflowActionSequenceFormFieldValue>) new List<AUWorkflowActionSequenceFormFieldValue>(0);
  }

  public string CurrentWorkflowAction
  {
    get => PXContext.GetSlot<string>("Workflow.CurrentWorkflowAction");
    set => PXContext.SetSlot<string>("Workflow.CurrentWorkflowAction", value);
  }

  public string LongRunWorkflowAction
  {
    get => PXContext.GetSlot<string>("Workflow.LongRunWorkflowAction");
    set => PXContext.SetSlot<string>("Workflow.LongRunWorkflowAction", value);
  }

  public IDictionary CurrentWorkflowObjectKeys
  {
    get => PXContext.GetSlot<IDictionary>("Workflow.CurrentWorkflowObject");
    set => PXContext.SetSlot<IDictionary>("Workflow.CurrentWorkflowObject", value);
  }

  public IDictionary LongRunWorkflowObjectKeys
  {
    get => PXContext.GetSlot<IDictionary>("Workflow.LongRunWorkflowObject");
    set => PXContext.SetSlot<IDictionary>("Workflow.LongRunWorkflowObject", value);
  }

  public IDictionary<IDictionary, bool> MassProcessingWorkflowObjectKeys
  {
    get
    {
      return PXContext.GetSlot<IDictionary<IDictionary, bool>>("Workflow.MassProcessingWorkflowObject");
    }
    set
    {
      PXContext.SetSlot<IDictionary<IDictionary, bool>>("Workflow.MassProcessingWorkflowObject", value);
    }
  }

  public IDictionary<string, object> CurrentFormValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.CurrentWorkflowFormData");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.CurrentWorkflowFormData", value);
    }
  }

  public IDictionary<string, object> LongRunFormValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.LongRunWorkflowFormData");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.LongRunWorkflowFormData", value);
    }
  }

  public IDictionary<string, object> CurrentActionViewsValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.CurrentActionViewsData");
    set => PXContext.SetSlot<IDictionary<string, object>>("Workflow.CurrentActionViewsData", value);
  }

  public IDictionary<string, object> LongRunActionViewsValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.LongRunActionViewsData");
    set => PXContext.SetSlot<IDictionary<string, object>>("Workflow.LongRunActionViewsData", value);
  }

  public System.Type CurrentWorkflowObjectType
  {
    get => PXContext.GetSlot<System.Type>("Workflow.CurrentWorkflowObjectType");
    set => PXContext.SetSlot<System.Type>("Workflow.CurrentWorkflowObjectType", value);
  }

  public System.Type LongRunWorkflowObjectType
  {
    get => PXContext.GetSlot<System.Type>("Workflow.LongRunWorkflowObjectType");
    set => PXContext.SetSlot<System.Type>("Workflow.LongRunWorkflowObjectType", value);
  }

  public string CurrentWorkflowScreenId
  {
    get => PXContext.GetSlot<string>("Workflow.CurrentWorkflowScreenId");
    set => PXContext.SetSlot<string>("Workflow.CurrentWorkflowScreenId", value);
  }

  public string LongRunWorkflowScreenId
  {
    get => PXContext.GetSlot<string>("Workflow.LongRunWorkflowScreenId");
    set => PXContext.SetSlot<string>("Workflow.LongRunWorkflowScreenId", value);
  }

  public bool CurrentWorkflowShouldBeExecuted
  {
    get => PXContext.GetSlot<bool>("Workflow.CurrentWorkflowShouldBeExecuted");
    set => PXContext.SetSlot<bool>("Workflow.CurrentWorkflowShouldBeExecuted", value);
  }

  public bool LongRunWorkflowShouldBeExecuted
  {
    get => PXContext.GetSlot<bool>("Workflow.LongRunWorkflowShouldBeExecuted");
    set => PXContext.SetSlot<bool>("Workflow.LongRunWorkflowShouldBeExecuted", value);
  }

  public string BackupCurrentWorkflowAction
  {
    get => PXContext.GetSlot<string>("Workflow.CurrentWorkflowActionBackup");
    set => PXContext.SetSlot<string>("Workflow.CurrentWorkflowActionBackup", value);
  }

  public string BackupLongRunWorkflowAction
  {
    get => PXContext.GetSlot<string>("Workflow.LongRunWorkflowActionBackup");
    set => PXContext.SetSlot<string>("Workflow.LongRunWorkflowActionBackup", value);
  }

  public IDictionary BackupCurrentWorkflowObjectKeys
  {
    get => PXContext.GetSlot<IDictionary>("Workflow.CurrentWorkflowObjectBackup");
    set => PXContext.SetSlot<IDictionary>("Workflow.CurrentWorkflowObjectBackup", value);
  }

  public IDictionary BackupLongRunWorkflowObjectKeys
  {
    get => PXContext.GetSlot<IDictionary>("Workflow.LongRunWorkflowObjectBackup");
    set => PXContext.SetSlot<IDictionary>("Workflow.LongRunWorkflowObjectBackup", value);
  }

  public IEnumerable<IDictionary> BackupMassProcessingWorkflowObjectKeys
  {
    get
    {
      return PXContext.GetSlot<IEnumerable<IDictionary>>("Workflow.MassProcessingWorkflowObjectBackup");
    }
    set
    {
      PXContext.SetSlot<IEnumerable<IDictionary>>("Workflow.MassProcessingWorkflowObjectBackup", value);
    }
  }

  public IDictionary<string, object> BackupCurrentFormValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.CurrentWorkflowFormDataBackup");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.CurrentWorkflowFormDataBackup", value);
    }
  }

  public IDictionary<string, object> BackupCurrentActionViewsValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.CurrentActionViewsDataBackup");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.CurrentActionViewsDataBackup", value);
    }
  }

  public IDictionary<string, object> BackupLongRunFormValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.LongRunWorkflowFormDataBackup");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.LongRunWorkflowFormDataBackup", value);
    }
  }

  public IDictionary<string, object> BackupLongRunViewsValues
  {
    get => PXContext.GetSlot<IDictionary<string, object>>("Workflow.LongRunActionViewsDataBackup");
    set
    {
      PXContext.SetSlot<IDictionary<string, object>>("Workflow.LongRunActionViewsDataBackup", value);
    }
  }

  public System.Type BackupCurrentWorkflowObjectType
  {
    get => PXContext.GetSlot<System.Type>("Workflow.CurrentWorkflowObjectTypeBackup");
    set => PXContext.SetSlot<System.Type>("Workflow.CurrentWorkflowObjectTypeBackup", value);
  }

  public System.Type BackupLongRunWorkflowObjectType
  {
    get => PXContext.GetSlot<System.Type>("Workflow.LongRunWorkflowObjectTypeBackup");
    set => PXContext.SetSlot<System.Type>("Workflow.LongRunWorkflowObjectTypeBackup", value);
  }

  public string BackupCurrentWorkflowScreenId
  {
    get => PXContext.GetSlot<string>("Workflow.CurrentWorkflowScreenIdBackup");
    set => PXContext.SetSlot<string>("Workflow.CurrentWorkflowScreenIdBackup", value);
  }

  public string BackupLongRunWorkflowScreenId
  {
    get => PXContext.GetSlot<string>("Workflow.LongRunWorkflowScreenIdBackup");
    set => PXContext.SetSlot<string>("Workflow.LongRunWorkflowScreenIdBackup", value);
  }

  public bool BackupCurrentWorkflowShouldBeExecuted
  {
    get => PXContext.GetSlot<bool>("Workflow.CurrentWorkflowShouldBeExecutedBackup");
    set => PXContext.SetSlot<bool>("Workflow.CurrentWorkflowShouldBeExecutedBackup", value);
  }

  public bool BackupLongRunWorkflowShouldBeExecuted
  {
    get => PXContext.GetSlot<bool>("Workflow.LongRunWorkflowShouldBeExecutedBackup");
    set => PXContext.SetSlot<bool>("Workflow.LongRunWorkflowShouldBeExecutedBackup", value);
  }

  public bool HasActionsWithFormOrFieldUpdates(PXGraph graph)
  {
    AUWorkflowActionsEngine.ScreenActions screenActions = AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()));
    return ((IEnumerable<AUScreenActionBaseState>) screenActions.IndexByScreenActionDefinitions).Any<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it =>
    {
      if (!string.IsNullOrEmpty(it.Form))
        return true;
      List<AUWorkflowActionUpdateField> source;
      return screenActions.WorkflowStateFields.TryGetValue(it, out source) && source.Any<AUWorkflowActionUpdateField>();
    }));
  }

  private static IEnumerable<AUWorkflowCategory> SortCategories(
    IEnumerable<AUWorkflowCategory> categories)
  {
    return PlacementHelper.SortBeforeAfter<AUWorkflowCategory>(categories, (Func<AUWorkflowCategory, string>) (cat => cat.CategoryName), (Func<AUWorkflowCategory, Placement>) (cat =>
    {
      byte? placement = cat.Placement;
      return (placement.HasValue ? new Placement?((Placement) placement.GetValueOrDefault()) : new Placement?()).GetValueOrDefault();
    }), (Func<AUWorkflowCategory, string>) (cat => cat.After));
  }

  public IReadOnlyDictionary<string, AUScreenActionBaseState> GetMassProcessingActions(PXGraph graph)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return (IReadOnlyDictionary<string, AUScreenActionBaseState>) null;
    return string.IsNullOrEmpty(graph.PrimaryView) ? (IReadOnlyDictionary<string, AUScreenActionBaseState>) null : this.GetMassProcessingActions(screenIdFromGraphType);
  }

  public IReadOnlyDictionary<string, AUScreenActionBaseState> GetMassProcessingActions(
    string screenId)
  {
    return (IReadOnlyDictionary<string, AUScreenActionBaseState>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.IndexByScreenActionDefinitions.Value.SelectMany<IGrouping<string, AUScreenActionBaseState>, AUScreenActionBaseState>((Func<IGrouping<string, AUScreenActionBaseState>, IEnumerable<AUScreenActionBaseState>>) (it => it.Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (a => a.MassProcessingScreen == screenId)))).ToDictionary<AUScreenActionBaseState, string, AUScreenActionBaseState>((Func<AUScreenActionBaseState, string>) (it => $"{it.ScreenID}${it.ActionName}"), (Func<AUScreenActionBaseState, AUScreenActionBaseState>) (it => it));
  }

  public void StartOperation(PXLongOperationPars longOperationPars)
  {
    longOperationPars.AddExtension<AUWorkflowActionsEngine.LongOperationParameters>(new AUWorkflowActionsEngine.LongOperationParameters(this.CurrentFormValues, this.CurrentWorkflowObjectType, this.CurrentWorkflowObjectKeys, this.CurrentActionViewsValues, this.CurrentWorkflowAction, this.CurrentWorkflowScreenId, this.CurrentWorkflowShouldBeExecuted));
    if (PXLongOperation.IsLongRunOperation)
      return;
    this.SuppressCompletions[longOperationPars.State.Key.Original] = true;
  }

  public void RestoreWorkflowParameters(PXLongOperationPars longOperationPars)
  {
    AUWorkflowActionsEngine.LongOperationParameters extension = longOperationPars.GetExtension<AUWorkflowActionsEngine.LongOperationParameters>();
    this.LongRunFormValues = extension.FormData;
    this.LongRunActionViewsValues = extension.ActionViewsValues;
    this.LongRunWorkflowObjectType = extension.ObjectType;
    this.LongRunWorkflowObjectKeys = extension.ObjectKeys;
    this.LongRunWorkflowAction = extension.Action;
    this.LongRunWorkflowScreenId = extension.ScreenId;
    this.LongRunWorkflowShouldBeExecuted = extension.ShouldBeExecuted;
  }

  public SlotIndexer<object, bool> SuppressCompletions
  {
    get
    {
      return new SlotIndexer<object, bool>((Func<object, string>) (key => "ActionSuppressCompletion@" + key?.ToString()));
    }
  }

  public void ClearActionData()
  {
    if (WorkflowSyncScope.IsInWorkflowSyncScope())
      return;
    this.BackupCurrentWorkflowAction = this.CurrentWorkflowAction;
    this.BackupCurrentWorkflowObjectKeys = this.CurrentWorkflowObjectKeys;
    this.BackupCurrentFormValues = this.CurrentFormValues;
    this.BackupCurrentActionViewsValues = this.CurrentActionViewsValues;
    this.BackupCurrentWorkflowObjectType = this.CurrentWorkflowObjectType;
    this.BackupCurrentWorkflowScreenId = this.CurrentWorkflowScreenId;
    this.BackupCurrentWorkflowShouldBeExecuted = this.CurrentWorkflowShouldBeExecuted;
    this.CurrentWorkflowAction = (string) null;
    this.CurrentWorkflowObjectKeys = (IDictionary) null;
    this.CurrentFormValues = (IDictionary<string, object>) null;
    this.CurrentActionViewsValues = (IDictionary<string, object>) null;
    this.CurrentWorkflowObjectType = (System.Type) null;
    this.CurrentWorkflowScreenId = (string) null;
    this.CurrentWorkflowShouldBeExecuted = false;
  }

  public void RestoreActionData()
  {
    if (WorkflowSyncScope.IsInWorkflowSyncScope())
      return;
    this.CurrentWorkflowAction = this.BackupCurrentWorkflowAction;
    this.CurrentWorkflowObjectKeys = this.BackupCurrentWorkflowObjectKeys;
    this.CurrentFormValues = this.BackupCurrentFormValues;
    this.CurrentActionViewsValues = this.BackupCurrentActionViewsValues;
    this.CurrentWorkflowObjectType = this.BackupCurrentWorkflowObjectType;
    this.CurrentWorkflowScreenId = this.BackupCurrentWorkflowScreenId;
    this.CurrentWorkflowShouldBeExecuted = this.BackupCurrentWorkflowShouldBeExecuted;
  }

  public void ClearLongRunActionData()
  {
    if (WorkflowSyncScope.IsInWorkflowSyncScope())
      return;
    this.BackupLongRunWorkflowAction = this.LongRunWorkflowAction;
    this.BackupLongRunFormValues = this.LongRunFormValues;
    this.BackupLongRunViewsValues = this.LongRunActionViewsValues;
    this.BackupLongRunWorkflowObjectType = this.LongRunWorkflowObjectType;
    this.BackupLongRunWorkflowScreenId = this.LongRunWorkflowScreenId;
    this.BackupLongRunWorkflowObjectKeys = this.LongRunWorkflowObjectKeys;
    this.BackupLongRunWorkflowShouldBeExecuted = this.LongRunWorkflowShouldBeExecuted;
    this.LongRunWorkflowAction = (string) null;
    this.LongRunFormValues = (IDictionary<string, object>) null;
    this.LongRunActionViewsValues = (IDictionary<string, object>) null;
    this.LongRunWorkflowObjectType = (System.Type) null;
    this.LongRunWorkflowScreenId = (string) null;
    this.LongRunWorkflowObjectKeys = (IDictionary) null;
    this.LongRunWorkflowShouldBeExecuted = false;
  }

  public void RestoreLongRunActionData()
  {
    if (WorkflowSyncScope.IsInWorkflowSyncScope())
      return;
    this.LongRunWorkflowAction = this.BackupLongRunWorkflowAction;
    this.LongRunFormValues = this.BackupLongRunFormValues;
    this.LongRunActionViewsValues = this.BackupLongRunViewsValues;
    this.LongRunWorkflowObjectType = this.BackupLongRunWorkflowObjectType;
    this.LongRunWorkflowScreenId = this.BackupLongRunWorkflowScreenId;
    this.LongRunWorkflowObjectKeys = this.BackupLongRunWorkflowObjectKeys;
    this.LongRunWorkflowShouldBeExecuted = this.BackupLongRunWorkflowShouldBeExecuted;
  }

  public bool HasAnyActionSequences(string screenId, string actionName)
  {
    IEnumerable<AUScreenActionBaseState> actionDefinitions = this.GetActionDefinitions(screenId);
    AUScreenActionBaseState action = actionDefinitions != null ? actionDefinitions.FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase))) : (AUScreenActionBaseState) null;
    if (action == null)
      return false;
    IEnumerable<AUWorkflowActionSequence> nextActionSequences = AUWorkflowActionsEngine.GetNextActionSequences(action);
    return nextActionSequences != null && nextActionSequences.Any<AUWorkflowActionSequence>();
  }

  public IEnumerable<KeyValuePair<string, object>> GetParameters(
    PXGraph graph,
    string actionName,
    object row)
  {
    string screenIdFromGraphType = this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType());
    if (screenIdFromGraphType == null)
      return (IEnumerable<KeyValuePair<string, object>>) null;
    if (string.IsNullOrEmpty(graph.PrimaryView))
      return (IEnumerable<KeyValuePair<string, object>>) null;
    if (this._auWorkflowEngine.GetCurrentWorkflowAndState(graph, row, out string _) == null)
      return (IEnumerable<KeyValuePair<string, object>>) null;
    AUScreenActionBaseState actionDefinition = this.GetActionDefinitions(screenIdFromGraphType).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
    if (actionDefinition == null)
      return (IEnumerable<KeyValuePair<string, object>>) null;
    IEnumerable<AUWorkflowActionParam> actionParams = this.GetActionParams(actionDefinition);
    PXCache primaryCache = graph.Caches[graph.PrimaryItemType];
    return actionParams == null ? (IEnumerable<KeyValuePair<string, object>>) null : actionParams.Select<AUWorkflowActionParam, KeyValuePair<string, object>>((Func<AUWorkflowActionParam, KeyValuePair<string, object>>) (it =>
    {
      string parameter = it.Parameter;
      bool? isFromScheme = it.IsFromScheme;
      bool flag = true;
      object obj = isFromScheme.GetValueOrDefault() == flag & isFromScheme.HasValue ? this.GetValue(primaryCache, it.Value, graph.Actions[actionName].GetParameterExt(it.Parameter)) : WorkflowExpressionParser.Eval(primaryCache, (string) null, it.Value, this._auWorkflowFormsEngine.GetFormValues(graph, actionDefinition?.Form), row);
      return new KeyValuePair<string, object>(parameter, obj);
    }));
  }

  private object GetValue(PXCache cache, string stringValue, PXFieldState parameterState)
  {
    object obj = (object) stringValue;
    if (parameterState is PXDateState)
      obj = !RelativeDatesManager.IsRelativeDatesString(stringValue) ? (object) Convert.ToDateTime(stringValue, (IFormatProvider) CultureInfo.InvariantCulture) : (object) RelativeDatesManager.EvaluateAsDateTime(stringValue);
    if (stringValue.Equals("@me", StringComparison.OrdinalIgnoreCase))
      obj = WorkflowFieldExpressionEvaluator.GetCurrentUserOrContact(cache, parameterState.Name);
    if (stringValue.Equals("@branch", StringComparison.OrdinalIgnoreCase))
      obj = (object) WorkflowFieldExpressionEvaluator.GetCurrentBranch();
    return obj;
  }

  public class ScreenActions
  {
    public (AUWorkflowStateAction StateAction, string WorkflowID, string WorkflowSubID)[] IndexOnlyByScreenWorkflowState;
    public ILookup<string, AUWorkflowStateAction> IndexOnlyByScreenAndWorkflowIdWorkflowState;
    public ILookup<string, AUWorkflowStateAction> IndexByScreenWorkflowState;
    public AUScreenActionBaseState[] IndexByScreenActionDefinitions;
    public AUWorkflowCategory[] IndexOnlyByScreenActionCategories;
    public Dictionary<AUScreenActionBaseState, List<AUWorkflowActionUpdateField>> WorkflowStateFields;
    public Dictionary<AUScreenActionBaseState, List<AUWorkflowActionParam>> WorkflowStateParams;
    public Dictionary<AUScreenActionBaseState, List<AUWorkflowActionSequence>> NextActionSequences;
    public Dictionary<AUWorkflowActionSequence, List<AUWorkflowActionSequenceFormFieldValue>> ActionSequenceFormFields;
    public bool IsCustomized;
  }

  public class Slot : IPrefetchable, IPXCompanyDependent
  {
    public Lazy<ILookup<string, AUScreenActionBaseState>> IndexByScreenActionDefinitions;
    private readonly ConcurrentDictionary<string, AUWorkflowActionsEngine.ScreenActions> _screenActions = new ConcurrentDictionary<string, AUWorkflowActionsEngine.ScreenActions>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public static AUWorkflowActionsEngine.Slot GetSlot()
    {
      return PXDatabase.GetSlot<AUWorkflowActionsEngine.Slot>("ActionsDefinition", ((IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes()).Union<System.Type>((IEnumerable<System.Type>) new System.Type[11]
      {
        typeof (AUWorkflowStateAction),
        typeof (AUWorkflow),
        typeof (AUScreenActionBaseState),
        typeof (AUScreenActionState),
        typeof (AUScreenNavigationActionState),
        typeof (AUWorkflowActionUpdateField),
        typeof (AUWorkflowActionParam),
        typeof (AUWorkflowCategory),
        typeof (PXGraph.FeaturesSet),
        typeof (AUWorkflowActionSequence),
        typeof (AUWorkflowActionSequenceFormFieldValue)
      }).ToArray<System.Type>());
    }

    public static AUWorkflowActionsEngine.Slot LocallyCachedSlot
    {
      get
      {
        return PXContext.GetSlot<AUWorkflowActionsEngine.Slot>("ActionsDefinition") ?? PXContext.SetSlot<AUWorkflowActionsEngine.Slot>("ActionsDefinition", AUWorkflowActionsEngine.Slot.GetSlot());
      }
    }

    public AUWorkflowActionsEngine.ScreenActions Get(string screenID)
    {
      return screenID != null ? this._screenActions.GetOrAdd(screenID, new Func<string, AUWorkflowActionsEngine.ScreenActions>(this.LoadForScreen)) : throw new ArgumentNullException(nameof (screenID));
    }

    public void Prefetch()
    {
      this.IndexByScreenActionDefinitions = new Lazy<ILookup<string, AUScreenActionBaseState>>((Func<ILookup<string, AUScreenActionBaseState>>) (() =>
      {
        AUScreenNavigationActionState[] array1 = PXSystemWorkflows.SelectTable<AUScreenNavigationActionState>().ToArray<AUScreenNavigationActionState>();
        AUScreenActionState[] array2 = PXSystemWorkflows.SelectTable<AUScreenActionState>().ToArray<AUScreenActionState>();
        return ((IEnumerable<AUScreenActionBaseState>) array1.Cast<AUScreenActionBaseState>().Union<AUScreenActionBaseState>((IEnumerable<AUScreenActionBaseState>) array2).ToArray<AUScreenActionBaseState>()).ToLookup<AUScreenActionBaseState, string, AUScreenActionBaseState>((Func<AUScreenActionBaseState, string>) (it => it.ScreenID), (Func<AUScreenActionBaseState, AUScreenActionBaseState>) (it => it));
      }));
    }

    private AUWorkflowActionsEngine.ScreenActions LoadForScreen(string screenID)
    {
      bool isCustomized = false;
      AUWorkflow[] array1 = PXSystemWorkflows.SelectTable<AUWorkflow>(screenID, ref isCustomized).ToArray<AUWorkflow>();
      AUWorkflowStateAction[] array2 = PXSystemWorkflows.SelectTable<AUWorkflowStateAction>(screenID, ref isCustomized).ToArray<AUWorkflowStateAction>();
      AUWorkflowActionUpdateField[] array3 = PXSystemWorkflows.SelectTable<AUWorkflowActionUpdateField>(screenID, ref isCustomized).ToArray<AUWorkflowActionUpdateField>();
      AUWorkflowActionParam[] array4 = PXSystemWorkflows.SelectTable<AUWorkflowActionParam>(screenID, ref isCustomized).ToArray<AUWorkflowActionParam>();
      AUScreenNavigationActionState[] array5 = PXSystemWorkflows.SelectTable<AUScreenNavigationActionState>(screenID, ref isCustomized).ToArray<AUScreenNavigationActionState>();
      AUScreenActionState[] array6 = PXSystemWorkflows.SelectTable<AUScreenActionState>(screenID, ref isCustomized).ToArray<AUScreenActionState>();
      AUScreenActionBaseState[] array7 = array5.Cast<AUScreenActionBaseState>().Union<AUScreenActionBaseState>((IEnumerable<AUScreenActionBaseState>) array6).ToArray<AUScreenActionBaseState>();
      AUWorkflowCategory[] array8 = PXSystemWorkflows.SelectTable<AUWorkflowCategory>(screenID, ref isCustomized).ToArray<AUWorkflowCategory>();
      AUWorkflowActionSequence[] array9 = PXSystemWorkflows.SelectTable<AUWorkflowActionSequence>(screenID, ref isCustomized).ToArray<AUWorkflowActionSequence>();
      AUWorkflowActionSequenceFormFieldValue[] array10 = PXSystemWorkflows.SelectTable<AUWorkflowActionSequenceFormFieldValue>(screenID, ref isCustomized).ToArray<AUWorkflowActionSequenceFormFieldValue>();
      AUWorkflowStateAction[] inner = array2;
      List<\u003C\u003Ef__AnonymousType66<AUWorkflow, AUWorkflowStateAction>> list = ((IEnumerable<AUWorkflow>) array1).Join((IEnumerable<AUWorkflowStateAction>) inner, w => new
      {
        ScreenID = w.ScreenID,
        WorkflowGUID = w.WorkflowGUID
      }, s => new
      {
        ScreenID = s.ScreenID,
        WorkflowGUID = s.WorkflowGUID
      }, (w, s) => new{ Workflow = w, StateAction = s }).Where(it =>
      {
        bool? isSystem1 = it.Workflow.IsSystem;
        bool flag1 = true;
        if (isSystem1.GetValueOrDefault() == flag1 & isSystem1.HasValue)
          return true;
        bool? isSystem2 = it.StateAction.IsSystem;
        bool flag2 = false;
        return isSystem2.GetValueOrDefault() == flag2 & isSystem2.HasValue;
      }).ToList();
      list.Select(it => it.StateAction).ToArray<AUWorkflowStateAction>();
      return new AUWorkflowActionsEngine.ScreenActions()
      {
        IndexOnlyByScreenWorkflowState = list.Select(it => (it.StateAction, it.Workflow.WorkflowID, it.Workflow.WorkflowSubID)).ToArray<(AUWorkflowStateAction, string, string)>(),
        IndexOnlyByScreenAndWorkflowIdWorkflowState = list.ToLookup(it => $"{it.Workflow.WorkflowID}.{it.Workflow.WorkflowSubID}", it => it.StateAction),
        IndexByScreenWorkflowState = list.ToLookup(it => $"{it.Workflow.WorkflowID}.{it.Workflow.WorkflowSubID}.{it.StateAction.StateName}", it => it.StateAction),
        IndexByScreenActionDefinitions = array7,
        IndexOnlyByScreenActionCategories = AUWorkflowActionsEngine.SortCategories((IEnumerable<AUWorkflowCategory>) array8).ToArray<AUWorkflowCategory>(),
        WorkflowStateFields = ((IEnumerable<AUScreenActionBaseState>) array7).Join((IEnumerable<AUWorkflowActionUpdateField>) array3, s => new
        {
          ScreenID = s.ScreenID,
          ActionName = s.ActionName
        }, sp => new
        {
          ScreenID = sp.ScreenID,
          ActionName = sp.ActionName
        }, (s, sp) => new
        {
          StateAction = s,
          StateActionField = sp
        }).GroupBy(it => it.StateAction).ToDictionary<IGrouping<AUScreenActionBaseState, \u003C\u003Ef__AnonymousType68<AUScreenActionBaseState, AUWorkflowActionUpdateField>>, AUScreenActionBaseState, List<AUWorkflowActionUpdateField>>(it => it.Key, it => it.Select(_ => _.StateActionField).OrderBy<AUWorkflowActionUpdateField, int?>((Func<AUWorkflowActionUpdateField, int?>) (_ => _.StateActionFieldLineNbr)).ToList<AUWorkflowActionUpdateField>()),
        WorkflowStateParams = ((IEnumerable<AUScreenActionBaseState>) array7).Join((IEnumerable<AUWorkflowActionParam>) array4, s => new
        {
          ScreenID = s.ScreenID,
          ActionName = s.ActionName
        }, sp => new
        {
          ScreenID = sp.ScreenID,
          ActionName = sp.ActionName
        }, (s, sp) => new
        {
          StateAction = s,
          StateActionParam = sp
        }).GroupBy(it => it.StateAction).ToDictionary<IGrouping<AUScreenActionBaseState, \u003C\u003Ef__AnonymousType69<AUScreenActionBaseState, AUWorkflowActionParam>>, AUScreenActionBaseState, List<AUWorkflowActionParam>>(it => it.Key, it => it.Select(_ => _.StateActionParam).OrderBy<AUWorkflowActionParam, int?>((Func<AUWorkflowActionParam, int?>) (_ => _.StateActionParamLineNbr)).ToList<AUWorkflowActionParam>()),
        NextActionSequences = ((IEnumerable<AUScreenActionBaseState>) array7).Join((IEnumerable<AUWorkflowActionSequence>) array9, s => new
        {
          ScreenID = s.ScreenID,
          ActionName = s.ActionName
        }, aseq => new
        {
          ScreenID = aseq.ScreenID,
          ActionName = aseq.PrevActionName
        }, (s, aseq) => new
        {
          StateAction = s,
          Sequence = aseq
        }).GroupBy(it => it.StateAction).ToDictionary<IGrouping<AUScreenActionBaseState, \u003C\u003Ef__AnonymousType70<AUScreenActionBaseState, AUWorkflowActionSequence>>, AUScreenActionBaseState, List<AUWorkflowActionSequence>>(it => it.Key, it => it.Select(x => x.Sequence).OrderBy<AUWorkflowActionSequence, int?>((Func<AUWorkflowActionSequence, int?>) (s => s.LineNbr)).ToList<AUWorkflowActionSequence>()),
        ActionSequenceFormFields = ((IEnumerable<AUWorkflowActionSequence>) array9).Join((IEnumerable<AUWorkflowActionSequenceFormFieldValue>) array10, s => new
        {
          ScreenID = s.ScreenID,
          PrevActionName = s.PrevActionName,
          NextActionName = s.NextActionName,
          Condition = s.Condition
        }, ff => new
        {
          ScreenID = ff.ScreenID,
          PrevActionName = ff.PrevActionName,
          NextActionName = ff.NextActionName,
          Condition = ff.Condition
        }, (s, ff) => new{ Sequence = s, FormField = ff }).GroupBy(it => it.Sequence).ToDictionary<IGrouping<AUWorkflowActionSequence, \u003C\u003Ef__AnonymousType72<AUWorkflowActionSequence, AUWorkflowActionSequenceFormFieldValue>>, AUWorkflowActionSequence, List<AUWorkflowActionSequenceFormFieldValue>>(it => it.Key, it => it.Select(x => x.FormField).OrderBy<AUWorkflowActionSequenceFormFieldValue, string>((Func<AUWorkflowActionSequenceFormFieldValue, string>) (ff => ff.FieldName)).ToList<AUWorkflowActionSequenceFormFieldValue>()),
        IsCustomized = isCustomized
      };
    }
  }

  private sealed class LongOperationParameters
  {
    internal LongOperationParameters(
      IDictionary<string, object> formData,
      System.Type objectType,
      IDictionary objectKeys,
      IDictionary<string, object> actionActionViewsValues,
      string action,
      string screenId,
      bool shouldBeExecuted)
    {
      this.FormData = formData;
      this.ObjectType = objectType;
      this.ObjectKeys = objectKeys;
      this.ActionViewsValues = actionActionViewsValues;
      this.Action = action;
      this.ScreenId = screenId;
      this.ShouldBeExecuted = shouldBeExecuted;
    }

    internal IDictionary<string, object> FormData { get; }

    internal System.Type ObjectType { get; }

    internal IDictionary ObjectKeys { get; }

    internal IDictionary<string, object> ActionViewsValues { get; }

    internal string Action { get; }

    internal string ScreenId { get; }

    internal bool ShouldBeExecuted { get; }
  }
}
