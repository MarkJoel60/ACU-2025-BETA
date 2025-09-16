// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIFilteredProcessing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BusinessProcess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data.Maintenance.GI;

/// <exclude />
public class GIFilteredProcessing : PXProcessingBase<GenericResult>
{
  protected bool _ProcessPending;
  protected bool _HeaderSelected;
  protected object _SavedFilter;
  protected PXLongRunStatus _Status;
  protected PXView _Filter;
  protected string _ViewName;
  protected string _CurrentAction;
  private const string MASSDELETE_ACTION_NAME = "Delete";
  internal const string MASSACTION_ALL_SUFFIX = "__all";
  private const string MASSUPDATE_ACTION_NAME = "Update";
  private const string MASSUPDATE_ACTIONALL_NAME = "Update All";
  public PXMenuAction<GenericFilter> ActionsMenu;

  protected GIFilteredProcessing()
  {
  }

  public GIFilteredProcessing(PXGraph graph)
    : this(graph, (Delegate) null)
  {
  }

  public GIFilteredProcessing(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this._ViewName = (string) null;
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) graph.Views)
    {
      PXView pxView = view.Value;
      if (pxView.GetItemType() == typeof (GenericFilter))
      {
        this._Filter = pxView;
        this._ViewName = view.Key;
        if (PXLongOperation.Exists(this._Graph.UID))
        {
          this._SavedFilter = this._Filter.Cache.Current;
          break;
        }
        break;
      }
    }
    if (this._Filter != null)
      graph.Views[this._ViewName] = (PXView) new GIFilteredProcessing.GIParametrizedView(graph, (BqlCommand) new PX.Data.Select<GenericFilter>(), this, new PXSelectDelegate(this._Header));
    this.View = (PXView) new GIFilteredProcessing.GIParametrizedView(graph, this.GetCommand(), this, new PXSelectDelegate(((PXProcessingBase<GenericResult>) this)._List));
    this.SetGIOuterViewDelegate(handler);
    this._PrepareGraph<GenericFilter>();
  }

  [InjectDependency]
  private IBusinessProcessEventProcessor BusinessProcessEventProcessor { get; set; }

  protected PXGenericInqGrph _GenInqGraph => (PXGenericInqGrph) this._Graph;

  public GenericResultCache Cache => (GenericResultCache) base.Cache;

  protected override BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<GenericResult>();

  protected void SetGIOuterViewDelegate(Delegate handler)
  {
    this._OuterView = (object) handler != null ? (PXView) new GIFilteredProcessing.GIOuterView(this.View, this.GetCommand(), handler) : (PXView) new GIFilteredProcessing.GIOuterView(this.View, this.GetCommand());
  }

  protected override IEnumerable _List()
  {
    PXUIFieldAttribute.SetEnabled(this._OuterView.Cache, this._SelectedField, true);
    List<GenericResult> genericResultList = (List<GenericResult>) null;
    if (this._ProcessPending)
    {
      this._ProcessPending = false;
      genericResultList = this._PendingList(this._Parameters, this.View.GetExternalSorts(), this.View.GetExternalDescendings(), this._Filters);
      if (this._ParametersDelegate != null && !this._ParametersDelegate(genericResultList))
      {
        PXLongOperation.ForceClearStatus(this._Graph);
        return (IEnumerable) genericResultList;
      }
      this.startPendingProcess(genericResultList);
    }
    else if (!this._HeaderSelected && !this._IsInstance)
      this._Header().GetEnumerator().MoveNext();
    if (this._InProc != null && this._InProc.Count > 0)
    {
      PXCache cache = this._OuterView.Cache;
      PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
      for (int index = 0; index < this._InProc.Count; ++index)
      {
        if (this._Info != null && this._Info.Messages != null && index < this._Info.Messages.Length && this._Info.Messages[index] != null)
        {
          GenericResult i0 = (GenericResult) cache.Locate(this._InProc[index][0]);
          if (i0 != null)
          {
            if (i0 != this._InProc[index][0])
              cache.RestoreCopy((object) i0, this._InProc[index][0]);
            this._InProc[index] = new PXResult<GenericResult>(i0);
          }
          this._SelectedInfo[this._InProc[index][0]] = this._Info.Messages[index];
        }
      }
      if (PXView.MaximumRows != 0 && PXView.MaximumRows != 1)
      {
        bool flag = PXView.StartRow < 0;
        PXResultset<GenericResult> pxResultset = new PXResultset<GenericResult>();
        for (int index1 = 0; index1 < PXView.MaximumRows; ++index1)
        {
          if (flag)
          {
            int index2 = this._InProc.Count + PXView.StartRow + index1;
            if (index2 < this._InProc.Count)
            {
              if (index2 >= 0)
                pxResultset.Add(this._InProc[index2]);
            }
            else
              break;
          }
          else if (PXView.StartRow + index1 <= this._InProc.Count - 1)
            pxResultset.Add(this._InProc[PXView.StartRow + index1]);
          else
            break;
        }
        PXView.StartRow = 0;
        if (PXLongOperation.GetStatus(this._Graph.UID) == PXLongRunStatus.Completed && this._Info != null && !this._Info.ProcessingCompleted)
        {
          this._Info.ProcessingCompleted = true;
          PXProcessing.SetProcessingInfoInternal(this._Graph.UID, (object) this._Info);
          this.View.RequestFiltersReset();
        }
        return (IEnumerable) pxResultset;
      }
      if (this._Status != PXLongRunStatus.InProcess)
      {
        using (new GISelectedOnlyScope())
        {
          foreach (GenericResult data in this._SelectRecords().RowCast<GenericResult>())
            cache.SetValue((object) data, this._SelectedField, (object) false);
        }
      }
      if (PXView.RetrieveTotalRowCount)
      {
        PXResultset<GenericResult> pxResultset = new PXResultset<GenericResult>();
        PXResult<GenericResult> pxResult = new PXResult<GenericResult>((GenericResult) null);
        pxResult.RowCount = new int?(this._InProc.Count);
        pxResultset.Add(pxResult);
        return (IEnumerable) pxResultset;
      }
    }
    if (!this._IsInstance && genericResultList != null && genericResultList.Count > 0)
    {
      PXResultset<GenericResult> pxResultset = new PXResultset<GenericResult>();
      foreach (GenericResult i0 in genericResultList)
        pxResultset.Add(new PXResult<GenericResult>(i0));
      return (IEnumerable) pxResultset;
    }
    if (!PXLongOperation.Exists(this._Graph.UID))
      PXUIFieldAttribute.SetEnabled((PXCache) this.Cache, this._SelectedField, true);
    return this._SelectRecords();
  }

  protected IEnumerable _Header()
  {
    GIFilteredProcessing filteredProcessing = this;
    filteredProcessing._HeaderSelected = true;
    if (PXLongOperation.Exists(filteredProcessing._Graph.UID))
    {
      if (filteredProcessing._Filter != null && filteredProcessing._Filter.Cache.AutomationFieldSelecting == null)
      {
        // ISSUE: reference to a compiler-generated method
        filteredProcessing._Filter.Cache.AutomationFieldSelecting = new PXCache.FieldSelectingDelegate(filteredProcessing.\u003C_Header\u003Eb__27_0);
        foreach (string field in (List<string>) filteredProcessing._Filter.Cache.Fields)
          filteredProcessing._Filter.Cache.SetAltered(field, true);
      }
      object[] processingList;
      filteredProcessing._Info = (PXProcessingInfo<GenericResult>) PXProcessing.GetProcessingInfo(filteredProcessing._Graph.UID, out processingList);
      filteredProcessing._Status = PXLongOperation.GetStatus(filteredProcessing._Graph.UID);
      if (processingList != null)
        filteredProcessing.FillInProcWithProcessingList((IEnumerable) processingList);
      else
        filteredProcessing.FillInProcWithSelectedItemsOfOuterView();
      yield return filteredProcessing._Parameters == null ? filteredProcessing._Filter.SelectSingle() : filteredProcessing._Filter.SelectSingle(filteredProcessing._Parameters);
    }
    else
    {
      filteredProcessing._InProc = (PXResultset<GenericResult>) null;
      object obj = filteredProcessing._Parameters == null ? filteredProcessing._Filter.SelectSingle() : filteredProcessing._Filter.SelectSingle(filteredProcessing._Parameters);
      if (!filteredProcessing._IsInstance && filteredProcessing._SavedFilter != null && filteredProcessing._SavedFilter != obj && filteredProcessing._SelectFromUI)
      {
        Dictionary<string, object> dictionary = filteredProcessing._Filter.Cache.ToDictionary(filteredProcessing._SavedFilter);
        filteredProcessing._Filter.Graph.ExecuteUpdate(filteredProcessing._ViewName, (IDictionary) new Dictionary<string, object>(), (IDictionary) dictionary);
        filteredProcessing._SavedFilter = (object) null;
        filteredProcessing._Filter.Cache.IsDirty = false;
      }
      yield return obj;
    }
  }

  protected virtual void startPendingProcess(List<GenericResult> items)
  {
    PXCache cache = this._OuterView.Cache;
    object current = cache.Current;
    if (this._IsInstance)
      cache.Current = current;
    cache.IsDirty = false;
    List<GenericResult> list = this.GetSelectedItems(cache, (IEnumerable) items);
    if (list.Count > 0)
    {
      if (!this._IsInstance)
      {
        this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.ProcessDelegate(list, cancellationToken)));
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
      }
      else
      {
        this.ProcessDelegate(list, CancellationToken.None);
        DialogManager.Clear(this._Graph);
      }
    }
    else
      PXLongOperation.ForceClearStatus(this._Graph);
  }

  protected override void _PrepareGraph<Primary>()
  {
    base._PrepareGraph<Primary>();
    this._OuterView.Cache.AllowInsert = true;
    this._OuterView.Cache.AllowUpdate = true;
  }

  /// <summary>
  /// Adds StateSelecting event handler to an action with enable/disable logic.
  /// </summary>
  private void AddStateSelectingHandler(PXAction<GenericFilter> action)
  {
    action.StateSelectingEvents += (PXFieldSelecting) ((sender, e) =>
    {
      if (PXLongOperation.GetStatus(this._OuterView.Cache.Graph.UID) == PXLongRunStatus.NotExists)
        return;
      e.ReturnState = (object) PXButtonState.CreateDefaultState<GenericResult>(e.ReturnState);
      ((PXFieldState) e.ReturnState).Enabled = false;
    });
  }

  private PXAction<GenericFilter> AddMenuAction(
    string internalName,
    string displayName,
    string actionName,
    PXProcessingBase<GenericResult>.ParametersDelegate parametersDelegate,
    PXProcessingBase<GenericResult>.ProcessListDelegate processDelegate,
    GIFilteredProcessing.GetRecordsDelegate getRecordsDelegate,
    IList<PXAction<GenericFilter>> createdActions)
  {
    PXAction<GenericFilter> action = PXNamedAction<GenericFilter>.AddAction((PXGraph) this._GenInqGraph, internalName, displayName, (PXButtonDelegate) (adapter =>
    {
      this._CurrentAction = actionName;
      this.SetParametersDelegate(parametersDelegate);
      this.SetProcessDelegate(processDelegate);
      return getRecordsDelegate(adapter);
    }));
    action.ClearAnswerAfterPress = false;
    this.ActionsMenu.AddMenuAction((PXAction) action);
    createdActions.Add(action);
    return action;
  }

  public void InitializeActions()
  {
    IReadOnlyCollection<(string actionName, string uniqueActionName, bool showMassAction, ProcessEventDelegate processDelegate)> manualActions = this.BusinessProcessEventProcessor.GetManualActions(this._Graph.Accessinfo.ScreenID?.Replace(".", ""));
    bool flag1 = manualActions != null && manualActions.Any<(string, string, bool, ProcessEventDelegate)>();
    int num1;
    if (this._GenInqGraph.Design.PrimaryScreenID != null)
    {
      bool? onRecordsEnabled = this._GenInqGraph.Design.MassActionsOnRecordsEnabled;
      bool flag2 = true;
      num1 = onRecordsEnabled.GetValueOrDefault() == flag2 & onRecordsEnabled.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag3 = num1 != 0;
    int num2;
    if (this._GenInqGraph.Design.PrimaryScreenID != null)
    {
      bool? recordsUpdateEnabled = this._GenInqGraph.Design.MassRecordsUpdateEnabled;
      bool flag4 = true;
      if (recordsUpdateEnabled.GetValueOrDefault() == flag4 & recordsUpdateEnabled.HasValue)
      {
        num2 = this._GenInqGraph.Description.MassUpdateFields.Any<GIMassUpdateField>() ? 1 : 0;
        goto label_7;
      }
    }
    num2 = 0;
label_7:
    bool flag5 = num2 != 0;
    int num3;
    if (this._GenInqGraph.Design.PrimaryScreenID != null)
    {
      bool? massDeleteEnabled = this._GenInqGraph.Design.MassDeleteEnabled;
      bool flag6 = true;
      num3 = massDeleteEnabled.GetValueOrDefault() == flag6 & massDeleteEnabled.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag7 = num3 != 0;
    IList<PXAction<GenericFilter>> createdActions = (IList<PXAction<GenericFilter>>) new List<PXAction<GenericFilter>>();
    if (flag7 && this._GenInqGraph.Caches[GIScreenHelper.GetCacheType(this._GenInqGraph.Design.PrimaryScreenID)].AllowDelete)
    {
      PXAction<GenericFilter> pxAction = new PXAction<GenericFilter>((PXGraph) this._GenInqGraph, (Delegate) new PXButtonDelegate(this.Delete));
      PXProcessingBase<GenericResult>.AddAction((PXGraph) this._GenInqGraph, "Delete", (PXAction) pxAction);
      this._GenInqGraph.SecureAction((PXAction) pxAction);
      createdActions.Add(pxAction);
    }
    this.ActionsMenu = new PXMenuAction<GenericFilter>((PXGraph) this._GenInqGraph, (Delegate) new PXButtonDelegate(this.actionsMenu));
    PXProcessingBase<GenericResult>.AddAction((PXGraph) this._GenInqGraph, "ActionsMenu", (PXAction) this.ActionsMenu);
    createdActions.Add((PXAction<GenericFilter>) this.ActionsMenu);
    this.ActionsMenu.ClearAnswerAfterPress = false;
    if (flag1)
    {
      foreach ((string str1, string str2, bool _, ProcessEventDelegate _) in (IEnumerable<(string actionName, string uniqueActionName, bool showMassAction, ProcessEventDelegate processDelegate)>) manualActions)
      {
        (string, string, bool, ProcessEventDelegate) valueTuple;
        int num4 = valueTuple.Item3 ? 1 : 0;
        ProcessEventDelegate manualEventHandler = valueTuple.Item4;
        this.SecureAction((PXAction) this.AddMenuAction(str2, str1, (string) null, (PXProcessingBase<GenericResult>.ParametersDelegate) null, new PXProcessingBase<GenericResult>.ProcessListDelegate(TriggerBusinessEvent), new GIFilteredProcessing.GetRecordsDelegate(this.Process), createdActions));
        if (num4 != 0)
          this.SecureAction((PXAction) this.AddMenuAction(ManualEventHelper.GetInternalMassActionName(str2), ManualEventHelper.GetInternalMassActionDisplayName(str1), (string) null, (PXProcessingBase<GenericResult>.ParametersDelegate) null, new PXProcessingBase<GenericResult>.ProcessListDelegate(TriggerBusinessEvent), new GIFilteredProcessing.GetRecordsDelegate(this.ProcessAll), createdActions));

        void TriggerBusinessEvent(List<GenericResult> items)
        {
          if (!manualEventHandler(this._GenInqGraph, items))
            throw new PXException("At least one record hasn't been processed.");
        }
      }
    }
    if (flag5)
    {
      // ISSUE: method pointer
      this.AddMenuAction("Update", "Update", (string) null, new PXProcessingBase<GenericResult>.ParametersDelegate((object) this, __methodptr(\u003CInitializeActions\u003Eg__AskParameters\u007C32_0)), new PXProcessingBase<GenericResult>.ProcessListDelegate(this.PerformUpdate), new GIFilteredProcessing.GetRecordsDelegate(this.Process), createdActions);
      // ISSUE: method pointer
      this.AddMenuAction("Update All", "Update All", (string) null, new PXProcessingBase<GenericResult>.ParametersDelegate((object) this, __methodptr(\u003CInitializeActions\u003Eg__AskParameters\u007C32_0)), new PXProcessingBase<GenericResult>.ProcessListDelegate(this.PerformUpdate), new GIFilteredProcessing.GetRecordsDelegate(this.ProcessAll), createdActions);
    }
    if (flag3)
    {
      IEnumerable<GIMassAction> massActions = this._GenInqGraph.Description.MassActions;
      PXGraph pxGraph = GIScreenHelper.InstantiateGraph(this._GenInqGraph.Design.PrimaryScreenID, false);
      Dictionary<string, PXButtonState> dictionary = new Dictionary<string, PXButtonState>();
      foreach (GIMassAction giMassAction in massActions)
      {
        PXAction action1 = pxGraph.Actions[giMassAction.ActionName];
        string str3 = (string) null;
        if (action1 == null)
        {
          string[] strArray = giMassAction.ActionName.Split('@');
          if (strArray.Length >= 2)
          {
            string actualCommandName = strArray[0];
            string str4 = strArray[1];
            action1 = pxGraph.Actions[str4];
            str3 = actualCommandName;
            PXButtonState state1;
            dictionary.TryGetValue(str4, out state1);
            if (action1 != null && state1 == null)
              dictionary[str4] = state1 = action1.GetState((object) null) as PXButtonState;
            if (state1?.Menus != null)
            {
              ButtonMenu buttonMenu = ((IEnumerable<ButtonMenu>) state1.Menus).FirstOrDefault<ButtonMenu>((Func<ButtonMenu, bool>) (m => m.Command == actualCommandName));
              if (buttonMenu != null)
                str3 = buttonMenu.Text;
            }
            PXAction action2 = pxGraph.Actions[actualCommandName];
            if (action2 != null)
            {
              action1 = action2;
              if (str3 == null && action1.GetState((object) null) is PXButtonState state2)
                str3 = state2.DisplayName;
            }
          }
          if (action1 == null)
            throw new PXException(PXMessages.LocalizeFormat("An action with the name '{0}' cannot be found.", (object) giMassAction.ActionName));
        }
        this._GenInqGraph.SecureAction(action1);
        PXUIFieldAttribute uiAttr = action1.Attributes.OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
        string strMessage = str3 ?? (uiAttr == null ? giMassAction.ActionName : uiAttr.DisplayName);
        PXUIFieldAttribute pxuiFieldAttribute1 = (PXUIFieldAttribute) uiAttr.Clone(PXAttributeLevel.Type);
        pxuiFieldAttribute1.FieldName = giMassAction.ActionName;
        PXUIFieldAttribute pxuiFieldAttribute2 = (PXUIFieldAttribute) pxuiFieldAttribute1.Clone(PXAttributeLevel.Item);
        pxuiFieldAttribute2.InvokeCacheAttached(pxGraph.Caches[action1.GetRowType()]);
        uiAttr.MapEnableRights = (PXCacheRights) System.Math.Min((int) uiAttr.MapEnableRights, (int) pxuiFieldAttribute2.MapEnableRights);
        uiAttr.MapViewRights = (PXCacheRights) System.Math.Min((int) uiAttr.MapViewRights, (int) pxuiFieldAttribute2.MapViewRights);
        uiAttr.EnableRights &= pxuiFieldAttribute2.EnableRights;
        uiAttr.ViewRights &= pxuiFieldAttribute2.ViewRights;
        System.Action<PXUIFieldAttribute> evaluator = (System.Action<PXUIFieldAttribute>) (ui =>
        {
          ui.MapEnableRights = uiAttr.MapEnableRights;
          ui.MapViewRights = uiAttr.MapViewRights;
          ui.FieldClass = uiAttr.FieldClass;
          ui.ViewRights = uiAttr.ViewRights;
          ui.EnableRights = uiAttr.EnableRights;
        });
        this.AddMenuAction(giMassAction.ActionName, PXMessages.LocalizeNoPrefix(strMessage), giMassAction.ActionName, (PXProcessingBase<GenericResult>.ParametersDelegate) null, new PXProcessingBase<GenericResult>.ProcessListDelegate(this.PerformAction), new GIFilteredProcessing.GetRecordsDelegate(this.Process), createdActions).Attributes.OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>().Call<PXUIFieldAttribute>(evaluator);
        this.AddMenuAction(giMassAction.ActionName + "__all", PXMessages.LocalizeNoPrefix(strMessage) + PXMessages.LocalizeNoPrefix(" (All)"), giMassAction.ActionName, (PXProcessingBase<GenericResult>.ParametersDelegate) null, new PXProcessingBase<GenericResult>.ProcessListDelegate(this.PerformAction), new GIFilteredProcessing.GetRecordsDelegate(this.ProcessAll), createdActions).Attributes.OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>().Call<PXUIFieldAttribute>(evaluator);
      }
      flag3 = flag3 && massActions.Any<GIMassAction>();
    }
    foreach (PXAction<GenericFilter> action in (IEnumerable<PXAction<GenericFilter>>) createdActions)
      this.AddStateSelectingHandler(action);
    PXUIFieldAttribute.SetVisible<GenericResult.selected>((PXCache) this.Cache, (object) null, flag1 | flag3 | flag5 | flag7);
    this.ActionsMenu.SetVisible(flag1 | flag3 | flag5);
  }

  private void SecureAction(PXAction action)
  {
    PXUIFieldAttribute attribute = action.Attributes.OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    if (attribute == null || this._Graph.Accessinfo.ScreenID == null)
      return;
    PXAccess.Secure(this._GenInqGraph.Caches[GIScreenHelper.GetCacheType(this._Graph.Accessinfo.ScreenID)], (PXEventSubscriberAttribute) attribute);
  }

  /// <summary>
  /// Handles all exceptions for the specified action and sets info about them in PXProcessing.
  /// </summary>
  /// <returns>True, if there were any exceptions; false otherwise.</returns>
  private bool HandleProcessingExceptionsFor(
    GenericResult item,
    int itemIndex,
    Func<GenericResult, int, bool> action)
  {
    try
    {
      int num = action(item, itemIndex) ? 1 : 0;
      if (num != 0)
        PXProcessing<GenericResult>.SetError(itemIndex, "The action cannot be applied to this record.");
      return num != 0;
    }
    catch (PXOuterException ex)
    {
      PXProcessing<GenericResult>.SetError(itemIndex, ex.GetFullMessage(" "));
      return true;
    }
    catch (PXSetPropertyException ex)
    {
      if (ex.ErrorLevel == PXErrorLevel.Error || ex.ErrorLevel == PXErrorLevel.RowError)
        PXProcessing<GenericResult>.SetError(itemIndex, ex.Message);
      else if (ex.ErrorLevel == PXErrorLevel.Warning || ex.ErrorLevel == PXErrorLevel.RowWarning)
        PXProcessing<GenericResult>.SetWarning(itemIndex, ex.Message);
      else
        PXProcessing<GenericResult>.SetInfo(itemIndex, ex.MessageNoPrefix);
      return true;
    }
    catch (PXBaseRedirectException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      PXProcessing<GenericResult>.SetError(itemIndex, ex.Message);
      return true;
    }
  }

  /// <summary>
  /// Handles all exceptions for the specified action and sets info about them in PXProcessing.
  /// </summary>
  /// <returns>True, if there were any exceptions; false otherwise.</returns>
  private bool HandleProcessingExceptionsFor(
    GenericResult item,
    int itemIndex,
    Action<GenericResult, int> action)
  {
    return this.HandleProcessingExceptionsFor(item, itemIndex, (Func<GenericResult, int, bool>) ((gr, i) =>
    {
      action(gr, i);
      return false;
    }));
  }

  private void PerformAction(List<GenericResult> items)
  {
    PXView primaryView;
    PXGraph graph = GIScreenHelper.InstantiateGraph(this._GenInqGraph.Design.PrimaryScreenID, out primaryView);
    PXCache cache = primaryView.Cache;
    bool flag1 = false;
    string actionName = (string) null;
    PXAction action = graph.Actions[this._CurrentAction];
    if (action == null)
    {
      string[] strArray = this._CurrentAction.Split('@');
      actionName = strArray.Length >= 2 ? strArray[0] : throw new PXException("An action with the name '{0}' cannot be found.", new object[1]
      {
        (object) this._CurrentAction
      });
      string name = strArray[1];
      action = graph.Actions[name];
      if (action == null)
        throw new PXException("An action with the name '{0}' cannot be found.", new object[1]
        {
          (object) this._CurrentAction
        });
      flag1 = true;
    }
    bool flag2 = false;
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      graph.Clear();
      graph.SelectTimeStamp();
      GenericResult genericRow = items[index1];
      object row = this._GenInqGraph.SelectPrimaryRow(graph, cache, genericRow);
      PXContext.SetSlot<bool>("Workflow.OperationCompletedInTransaction", false);
      cache.PlaceNotChanged(row);
      cache.Current = row;
      if (action.GetState(row) is PXButtonState state)
      {
        bool flag3 = state.Enabled && state.Visible;
        bool recordProcessed = false;
        if (flag3)
        {
          if (flag1)
          {
            ButtonMenu buttonMenu = ((IEnumerable<ButtonMenu>) state.Menus).FirstOrDefault<ButtonMenu>((Func<ButtonMenu, bool>) (m => PXLocalesProvider.CollationComparer.Equals(m.Command, actionName)));
            if (buttonMenu != null)
              flag3 = buttonMenu.GetEnabled() && buttonMenu.Visible;
            if (flag3)
              flag2 |= this.HandleProcessingExceptionsFor(genericRow, index1, (Action<GenericResult, int>) ((rec, index) =>
              {
                PXGraph graph1 = graph;
                BqlCommand bqlSelect = primaryView.BqlSelect;
                foreach (object obj in action.Press(new PXAdapter((PXView) new PXView.Dummy(graph1, bqlSelect, new List<object>()
                {
                  row
                }))
                {
                  Menu = actionName
                }))
                  ;
                if (graph.IsDirty)
                  graph.Actions.PressSave();
                recordProcessed = true;
              }));
          }
          else
            flag2 |= this.HandleProcessingExceptionsFor(genericRow, index1, (Action<GenericResult, int>) ((rec, index) =>
            {
              action.Press();
              graph.Actions.PressSave(action);
              recordProcessed = true;
            }));
        }
        if (!flag3)
        {
          PXProcessing.SetWarning(index1, "The action is unavailable for this record.");
          flag2 = true;
        }
        if (recordProcessed)
        {
          PXProcessing.SetInfo(index1, "The record has been processed successfully.");
          string tableAlias = this._GenInqGraph.GetTableAlias(cache.GetItemType().FullName);
          if (tableAlias != null)
            genericRow.Values[tableAlias] = row;
        }
      }
      else
        PXProcessing.SetError(index1, "An action with the name '{0}' cannot be found.");
    }
    if (flag2)
      throw new PXException("At least one record hasn't been processed.");
  }

  private void PerformUpdate(List<GenericResult> items)
  {
    PXView primaryView;
    PXGraph graph = GIScreenHelper.InstantiateGraph(this._GenInqGraph.Design.PrimaryScreenID, out primaryView);
    PXCache cache = primaryView.Cache;
    int itemIndex = 0;
    bool flag1 = false;
    foreach (GenericResult genericRow in items)
    {
      object row = this._GenInqGraph.SelectPrimaryRow(graph, cache, genericRow);
      cache.PlaceNotChanged(row);
      cache.Current = row;
      flag1 |= this.HandleProcessingExceptionsFor(genericRow, itemIndex, (Action<GenericResult, int>) ((rec, index) =>
      {
        IEnumerable<PXGenericInqGrph.GIUpdateValue> giUpdateValues = this._GenInqGraph.FieldsToUpdate.Select(field => new
        {
          field = field,
          refDict = Enumerable.Range(0, cache.Fields.Count).ToDictionary<int, string, int>((Func<int, string>) (i => cache.Fields[i]), (Func<int, int>) (i => i))
        }).OrderBy(_param1 => _param1.refDict[_param1.field.FieldName]).Select(_param1 => _param1.field);
        ISet<string> stringSet = this._GenInqGraph.SelectAUComboFields();
        bool flag2 = true;
        foreach (PXGenericInqGrph.GIUpdateValue giUpdateValue in giUpdateValues)
        {
          string fieldName = giUpdateValue.FieldName;
          PXFieldState pxFieldState1 = (PXFieldState) null;
          if (!flag2 && stringSet.Contains(giUpdateValue.FieldName))
          {
            if (cache.Update(row) == null)
              throw GetUpdateRecordException(cache, row);
            bool flag3 = true;
            pxFieldState1 = cache.GetStateExt(row, fieldName) as PXFieldState;
            if (pxFieldState1.Value is PXStringState pxStringState2)
            {
              flag3 = pxStringState2.AllowedValues != null && ((IEnumerable<string>) pxStringState2.AllowedValues).Contains<string>(giUpdateValue.Value);
            }
            else
            {
              int result;
              if (pxFieldState1.Value is PXIntState pxIntState2 && int.TryParse(giUpdateValue.Value, out result))
                flag3 = pxIntState2.AllowedValues != null && ((IEnumerable<int>) pxIntState2.AllowedValues).Contains<int>(result);
            }
            if (!flag3)
              throw new PXSetPropertyException(cache.GetNonEmptyDacDescriptor(row), row as IBqlTable, "The list value '{0}' is not allowed for the {1} field.", PXErrorLevel.RowError, new object[2]
              {
                (object) giUpdateValue.Value,
                (object) pxFieldState1.DisplayName
              });
          }
          PXFieldState pxFieldState2 = pxFieldState1 ?? cache.GetStateExt(row, fieldName) as PXFieldState;
          if (!pxFieldState2.Enabled)
            throw new PXSetPropertyException(cache.GetNonEmptyDacDescriptor(row), row as IBqlTable, "The field {0} cannot be updated in this record because the field is disabled.", PXErrorLevel.RowError, new object[1]
            {
              (object) pxFieldState2.DisplayName
            });
          cache.SetValueExt(row, giUpdateValue.FieldName, (object) giUpdateValue.Value);
          flag2 = false;
        }
        cache.Remove(row);
        object obj = cache.Update(row);
        if (obj == null)
          throw GetUpdateRecordException(cache, row);
        graph.Actions.PressSave();
        string tableAlias = this._GenInqGraph.GetTableAlias(cache.GetItemType().FullName);
        if (tableAlias != null)
          rec.Values[tableAlias] = obj;
        PXProcessing<GenericResult>.SetInfo(index, "The record has been processed successfully.");
      }));
      ++itemIndex;
    }
    if (flag1)
      throw new PXException("At least one record hasn't been processed.");

    static PXSetPropertyException GetUpdateRecordException(PXCache dacCache, object dac)
    {
      return new PXSetPropertyException(dacCache.GetNonEmptyDacDescriptor(dac), dac as IBqlTable, "The record cannot be updated.", PXErrorLevel.RowError);
    }
  }

  [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  [PXDeleteButton(ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable Delete(PXAdapter adapter)
  {
    this.SetParametersDelegate((PXProcessingBase<GenericResult>.ParametersDelegate) (items => this._GenInqGraph.Filter.Ask("Confirm Delete", PXMessages.LocalizeFormat("Are you sure you want to delete {0} record(s)?", (object) items.Count), MessageButtons.YesNo) == WebDialogResult.Yes));
    this.SetProcessDelegate(new PXProcessingBase<GenericResult>.ProcessListDelegate(this.PerformDelete));
    return this.Process(adapter);
  }

  private void PerformDelete(List<GenericResult> items)
  {
    bool? autoConfirmDelete = this._GenInqGraph.Design.AutoConfirmDelete;
    bool flag1 = true;
    bool autoConfirm = (autoConfirmDelete.GetValueOrDefault() == flag1 & autoConfirmDelete.HasValue ? 1 : 0) != 0;
    PXView primaryView;
    PXGraph graph = GIScreenHelper.InstantiateGraph(this._GenInqGraph.Design.PrimaryScreenID, out primaryView);
    PXCache cache = primaryView.Cache;
    Dictionary<int, object> rows = new Dictionary<int, object>();
    bool flag2 = false;
    PXDialogRequiredException dialogException = (PXDialogRequiredException) null;
    for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
    {
      GenericResult item = items[itemIndex];
      flag2 |= this.HandleProcessingExceptionsFor(item, itemIndex, (Action<GenericResult, int>) ((rec, index) =>
      {
        try
        {
          object row = this._GenInqGraph.SelectPrimaryRow(graph, cache, item);
          if (dialogException != null)
          {
            DialogManager.SetAnswer(dialogException.Graph, dialogException.ViewName, dialogException.Key, DialogManager.PositiveAnswerFor(dialogException.Buttons));
            dialogException = (PXDialogRequiredException) null;
          }
          if (row == null || !cache.AllowDelete)
            throw new PXException("The record cannot be deleted.");
          if (cache.Delete(row) == null)
            throw new PXException(cache.GetNonEmptyDacDescriptor(row), "The record cannot be deleted.");
          PXProcessing<GenericResult>.SetInfo(index, "The record has been successfully deleted.");
          rows[index] = row;
        }
        catch (PXDialogRequiredException ex)
        {
          if (autoConfirm)
          {
            dialogException = ex;
            --itemIndex;
          }
          else
            PXProcessing<GenericResult>.SetError(index, "The record requires special actions on removal. Please try to delete it manually on the data entry form.");
        }
        catch (NotSupportedException ex)
        {
          PXProcessing<GenericResult>.SetError(index, "The record requires special actions on removal. Please try to delete it manually on the data entry form.");
        }
      }));
    }
    try
    {
      graph.Actions.PressSave();
    }
    catch
    {
      Dictionary<int, Dictionary<string, string>> dictionary = rows.ToDictionary<KeyValuePair<int, object>, int, Dictionary<string, string>>((Func<KeyValuePair<int, object>, int>) (r => r.Key), (Func<KeyValuePair<int, object>, Dictionary<string, string>>) (r => PXUIFieldAttribute.GetErrors(cache, r.Value)));
      graph.Clear();
      foreach (KeyValuePair<int, object> keyValuePair in rows)
      {
        Dictionary<string, string> source;
        if (dictionary.TryGetValue(keyValuePair.Key, out source) && source != null && source.Count > 0)
        {
          PXProcessing<GenericResult>.SetError(keyValuePair.Key, source.First<KeyValuePair<string, string>>().Value);
        }
        else
        {
          try
          {
            cache.Delete(keyValuePair.Value);
            graph.Actions.PressSave();
          }
          catch (Exception ex)
          {
            PXProcessing<GenericResult>.SetError(keyValuePair.Key, ex.Message);
          }
        }
      }
      flag2 = true;
    }
    if (flag2)
      throw new PXException("At least one record hasn't been deleted.");
  }

  protected virtual IEnumerable ProcessAll(PXAdapter adapter)
  {
    if (PXLongOperation.Exists(this._Graph.UID))
      throw new PXException("The previous operation has not been completed yet.");
    this._ProcessPending = true;
    if (!this._IsInstance)
    {
      if (this._Filter != null && this._Filter.Cache.AutomationFieldSelecting == null)
      {
        this._Filter.Cache.AutomationFieldSelecting = (PXCache.FieldSelectingDelegate) ((string field, ref object value, object row) =>
        {
          if (!(value is PXFieldState pxFieldState2))
            return;
          pxFieldState2.Enabled = false;
        });
        foreach (string field in (List<string>) this._Filter.Cache.Fields)
          this._Filter.Cache.SetAltered(field, true);
      }
      if ((this._ParametersDelegate == null || DialogManager.GetRecords(this._Graph).Cast<DialogAnswer>().Any<DialogAnswer>((Func<DialogAnswer, bool>) (o =>
      {
        int? answer1 = o.Answer;
        int num1 = 1;
        if (answer1.GetValueOrDefault() == num1 & answer1.HasValue)
          return true;
        int? answer2 = o.Answer;
        int num2 = 6;
        return answer2.GetValueOrDefault() == num2 & answer2.HasValue;
      }))) && !adapter.ImportFlag)
        PXLongOperation.StartOperation(this._Graph, (PXToggleAsyncDelegate) null);
    }
    if (adapter.ImportFlag)
    {
      this._ProcessPending = false;
      this.startPendingProcess(this._PendingList(adapter.Parameters, adapter.SortColumns, adapter.Descendings, adapter.Filters));
    }
    return adapter.Get();
  }

  protected virtual IEnumerable Process(PXAdapter adapter)
  {
    if (PXLongOperation.Exists(this._Graph.UID))
      throw new PXException("The previous operation has not been completed yet.");
    PXCache cache = this._OuterView.Cache;
    cache.IsDirty = false;
    PXView pxView = (PXView) new GIFilteredProcessing.GIOuterView(this._OuterView, (BqlCommand) new PX.Data.Select<GenericResult>(), (Delegate) (() => (IEnumerable) this.GetSelectedItems(cache, cache.Cached)));
    int startRow = 0;
    int totalRows = 0;
    List<GenericResult> list = new List<GenericResult>();
    using (new GISelectedOnlyScope())
    {
      foreach (object obj in pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows))
        list.Add(obj is PXResult ? (GenericResult) (PXResult<GenericResult>) obj : (GenericResult) obj);
    }
    if (list.Count > 0 && (this._ParametersDelegate == null || this._ParametersDelegate(list)))
    {
      if (!this._IsInstance)
      {
        this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.ProcessDelegate(list, cancellationToken)));
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
        adapter.StartRow = 0;
      }
      else
        this.ProcessDelegate(list, CancellationToken.None);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Actions")]
  [PXButton(MenuAutoOpen = true, SpecialType = PXSpecialButtonType.ActionsFolder)]
  protected IEnumerable actionsMenu(PXAdapter adapter) => adapter.Get();

  /// <exclude />
  protected internal class GIParametrizedView : PXProcessingBase<GenericResult>.ParametrizedView
  {
    private readonly GIFilteredProcessing _processing;

    public GIParametrizedView(
      PXGraph graph,
      BqlCommand select,
      GIFilteredProcessing processing,
      PXSelectDelegate handler)
      : base(graph, select, (PXProcessingBase<GenericResult>) processing, handler)
    {
      this._processing = processing;
    }

    private bool HasProcessedRecords()
    {
      return this._processing != null && this._processing._InProc != null && this._processing._InProc.Count > 0 && PXLongOperation.Exists(this._processing._Graph.UID);
    }

    protected internal void FilterResult(
      List<object> list,
      PXFilterRow[] filters,
      bool forceFilter = false)
    {
      if (!forceFilter && !this.HasProcessedRecords())
        return;
      base.FilterResult(list, filters);
    }

    protected internal override void FilterResult(List<object> list, PXFilterRow[] filters)
    {
      if (!this.HasProcessedRecords())
        return;
      base.FilterResult(list, filters);
    }

    protected override void SortResult(
      List<object> list,
      PXView.PXSearchColumn[] sorts,
      bool reverseOrder)
    {
      if (!this.HasProcessedRecords())
        return;
      base.SortResult(list, sorts, reverseOrder);
    }

    protected override List<object> SearchResult(
      List<object> list,
      PXView.PXSearchColumn[] sorts,
      bool reverseOrder,
      bool findAll,
      ref int startRow,
      int maximumRows,
      ref int totalRows,
      out bool searchFound)
    {
      if (this.HasProcessedRecords() || totalRows == -1)
        return base.SearchResult(list, sorts, reverseOrder, findAll, ref startRow, maximumRows, ref totalRows, out searchFound);
      totalRows = list.Count;
      searchFound = true;
      return list;
    }
  }

  /// <exclude />
  protected class GIOuterView : PXProcessingBase<GenericResult>.OuterView
  {
    public GIOuterView(PXView view, BqlCommand select)
      : base(view, select)
    {
    }

    public GIOuterView(PXView view, BqlCommand select, Delegate handler)
      : base(view, select, handler)
    {
    }

    protected override void SortResult(
      List<object> list,
      PXView.PXSearchColumn[] sorts,
      bool reverseOrder)
    {
      PXGenericInqGrph graph = (PXGenericInqGrph) this.Graph;
      if (!((IEnumerable<PXView.PXSearchColumn>) sorts).Any<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (c => graph.IsVirtualField(c.Column))))
        return;
      base.SortResult(list, sorts, reverseOrder);
    }
  }

  private delegate IEnumerable GetRecordsDelegate(PXAdapter adapter);
}
