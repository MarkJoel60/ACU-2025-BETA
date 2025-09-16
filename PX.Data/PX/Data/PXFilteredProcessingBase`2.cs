// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilteredProcessingBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data;

public abstract class PXFilteredProcessingBase<Table, FilterTable> : 
  PXProcessing<Table>,
  IPXFilteredProcessing
  where Table : class, IBqlTable, new()
  where FilterTable : class, IBqlTable, new()
{
  protected PXView _Filter;
  protected bool _ShowSelectedOnly = true;
  protected string _ViewName;
  protected PXLongRunStatus _Status;
  protected bool _ProcessPending;
  protected AUScheduleMaint _ScheduleAddPending;
  protected bool _HeaderSelected;
  protected object _SavedFilter;

  public string ViewName => this._ViewName;

  public string ProcessAllActionKey => "ProcessAll";

  public virtual bool ShowSelectedOnly
  {
    get => this._ShowSelectedOnly;
    set => this._ShowSelectedOnly = value;
  }

  protected PXFilteredProcessingBase()
  {
  }

  public PXFilteredProcessingBase(PXGraph graph)
    : this(graph, (Delegate) null)
  {
  }

  public PXFilteredProcessingBase(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this.InitFilterView();
    this.View = (PXView) new PXProcessingBase<Table>.ParametrizedView(graph, this.GetCommand(), (PXProcessingBase<Table>) this, new PXSelectDelegate(((PXProcessingBase<Table>) this)._List));
    this.SetOuterViewDelegate(handler);
    this._PrepareGraph<FilterTable>();
  }

  private void InitFilterView()
  {
    this._ViewName = (string) null;
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) this._Graph.Views)
    {
      PXView pxView = view.Value;
      if (pxView.GetItemType() == typeof (FilterTable))
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
    if (this._Filter == null)
      return;
    this._Graph.Views[this._ViewName] = (PXView) new PXProcessingBase<Table>.ParametrizedView(this._Graph, (BqlCommand) new PX.Data.Select<FilterTable>(), (PXProcessingBase<Table>) this, new PXSelectDelegate(this._Header));
  }

  protected override void _PrepareGraph<Primary>()
  {
    base._PrepareGraph<Primary>();
    if (typeof (Primary) != typeof (FilterTable))
      this._ProcessAllButton.ClearAnswerAfterPress = false;
    else
      this._ProcessAllButton.ClearAnswerAfterPress = false;
  }

  protected override IEnumerable _List()
  {
    List<Table> items = (List<Table>) null;
    if (this._ProcessPending)
    {
      this._ProcessPending = false;
      items = this._PendingList(this._Parameters, this.View.GetExternalSorts(), this.View.GetExternalDescendings(), this._Filters);
      this.startPendingProcess(items);
    }
    else
    {
      if (this._ScheduleAddPending != null)
      {
        AUScheduleMaint scheduleAddPending = this._ScheduleAddPending;
        this._ScheduleAddPending = (AUScheduleMaint) null;
        if (this._Filters != null && this._Filters.Length != 0)
        {
          for (int index = 0; index < this._Filters.Length; ++index)
          {
            PXFilterRow filter = this._Filters[index];
            AUScheduleFilter auScheduleFilter1 = new AUScheduleFilter();
            auScheduleFilter1.OpenBrackets = new int?(filter.OpenBrackets);
            if (index == 0)
            {
              AUScheduleFilter auScheduleFilter2 = auScheduleFilter1;
              int? openBrackets = auScheduleFilter2.OpenBrackets;
              auScheduleFilter2.OpenBrackets = openBrackets.HasValue ? new int?(openBrackets.GetValueOrDefault() + 1) : new int?();
            }
            auScheduleFilter1.FieldName = filter.DataField;
            auScheduleFilter1.Condition = new int?((int) (filter.Condition + 1));
            if (filter.Value != null)
              auScheduleFilter1.Value = filter.Value.ToString();
            if (filter.Value2 != null)
              auScheduleFilter1.Value2 = filter.Value2.ToString();
            auScheduleFilter1.CloseBrackets = new int?(filter.CloseBrackets);
            if (index == this._Filters.Length - 1)
            {
              AUScheduleFilter auScheduleFilter3 = auScheduleFilter1;
              int? closeBrackets = auScheduleFilter3.CloseBrackets;
              auScheduleFilter3.CloseBrackets = closeBrackets.HasValue ? new int?(closeBrackets.GetValueOrDefault() + 1) : new int?();
            }
            scheduleAddPending.Filters.Insert(auScheduleFilter1);
          }
          scheduleAddPending.Filters.Cache.IsDirty = false;
        }
        throw new PXPopupRedirectException((PXGraph) scheduleAddPending, "Add Schedule", true);
      }
      if (!this._HeaderSelected && !this._IsInstance)
        this._Header().GetEnumerator().MoveNext();
    }
    if (this._InProc != null && this._InProc.Count > 0)
    {
      PXCache cache = this._OuterView.Cache;
      if (this._ShowSelectedOnly)
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
      for (int index = 0; index < this._InProc.Count; ++index)
      {
        if (this._Info != null && this._Info.Messages != null && index < this._Info.Messages.Length && this._Info.Messages[index] != null)
        {
          Table i0 = (Table) cache.Locate(this._InProc[index][0]);
          if ((object) i0 != null)
          {
            if ((object) i0 != this._InProc[index][0])
              cache.RestoreCopy((object) i0, this._InProc[index][0]);
            this._InProc[index] = new PXResult<Table>(i0);
          }
          this._SelectedInfo[this._InProc[index][0]] = this._Info.Messages[index];
        }
      }
      if (this._ShowSelectedOnly)
      {
        if (PXLongOperation.GetStatus(this._Graph.UID) == PXLongRunStatus.Completed && this._Info != null && !this._Info.ProcessingCompleted)
        {
          this._Info.ProcessingCompleted = true;
          PXProcessing.SetProcessingInfoInternal(this._Graph.UID, (object) this._Info);
          this.View.RequestFiltersReset();
        }
        if (PXView.Filters != null && PXView.Filters.Length != 0 || PXView.MaximumRows == 0 || PXView.MaximumRows == 1)
          return (IEnumerable) this._InProc;
        bool flag = PXView.StartRow < 0;
        PXResultset<Table> pxResultset = new PXResultset<Table>();
        for (int index1 = 0; index1 < PXView.MaximumRows; ++index1)
        {
          if (flag)
          {
            int index2 = this._InProc.Count + PXView.StartRow + index1;
            if (index2 >= 0)
            {
              if (index2 <= this._InProc.Count - 1)
                pxResultset.Add(this._InProc[index2]);
              else
                break;
            }
          }
          else if (PXView.StartRow + index1 <= this._InProc.Count - 1)
            pxResultset.Add(this._InProc[PXView.StartRow + index1]);
          else
            break;
        }
        PXView.StartRow = 0;
        return (IEnumerable) pxResultset;
      }
      if (this._Status != PXLongRunStatus.InProcess)
      {
        foreach (Table data in cache.Cached)
          cache.SetValue((object) data, this._SelectedField, (object) false);
      }
    }
    if (!this._IsInstance && items != null && items.Count > 0)
    {
      PXResultset<Table> pxResultset = new PXResultset<Table>();
      foreach (Table i0 in items)
        pxResultset.Add(new PXResult<Table>(i0));
      return (IEnumerable) pxResultset;
    }
    if (!PXLongOperation.Exists(this._Graph.UID))
      PXUIFieldAttribute.SetEnabled(this.Cache, this._SelectedField, true);
    return this._SelectRecords();
  }

  protected IEnumerable _Header()
  {
    PXFilteredProcessingBase<Table, FilterTable> filteredProcessingBase = this;
    filteredProcessingBase._HeaderSelected = true;
    if (PXLongOperation.Exists(filteredProcessingBase._Graph.UID))
    {
      if (filteredProcessingBase._Filter != null && filteredProcessingBase._Filter.Cache.AutomationFieldSelecting == null)
      {
        // ISSUE: reference to a compiler-generated method
        filteredProcessingBase._Filter.Cache.AutomationFieldSelecting = new PXCache.FieldSelectingDelegate(filteredProcessingBase.\u003C_Header\u003Eb__17_0);
        foreach (string field in (List<string>) filteredProcessingBase._Filter.Cache.Fields)
          filteredProcessingBase._Filter.Cache.SetAltered(field, true);
      }
      object[] processingList;
      filteredProcessingBase._Info = PXProcessing.GetProcessingInfo(filteredProcessingBase._Graph.UID, out processingList) as PXProcessingInfo<Table>;
      filteredProcessingBase._Status = PXLongOperation.GetStatus(filteredProcessingBase._Graph.UID);
      if (processingList != null)
        filteredProcessingBase.FillInProcWithProcessingList((IEnumerable) processingList);
      else
        filteredProcessingBase.FillInProcWithSelectedItemsOfOuterView();
      yield return filteredProcessingBase._Parameters == null ? filteredProcessingBase._Filter.SelectSingle() : filteredProcessingBase._Filter.SelectSingle(filteredProcessingBase._Parameters);
    }
    else
    {
      filteredProcessingBase._InProc = (PXResultset<Table>) null;
      object obj = filteredProcessingBase._Parameters == null ? filteredProcessingBase._Filter.SelectSingle() : filteredProcessingBase._Filter.SelectSingle(filteredProcessingBase._Parameters);
      if (!filteredProcessingBase._IsInstance && filteredProcessingBase._SavedFilter != null && filteredProcessingBase._SavedFilter != obj && filteredProcessingBase._SelectFromUI)
      {
        Dictionary<string, object> dictionary = filteredProcessingBase._Filter.Cache.ToDictionary(filteredProcessingBase._SavedFilter);
        filteredProcessingBase._Filter.Graph.ExecuteUpdate(filteredProcessingBase._ViewName, (IDictionary) new Dictionary<string, object>(), (IDictionary) dictionary);
        filteredProcessingBase._SavedFilter = (object) null;
        filteredProcessingBase._Filter.Cache.IsDirty = false;
      }
      yield return obj;
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Process All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable ProcessAll(PXAdapter adapter)
  {
    if (PXLongOperation.Exists(this._Graph.UID))
      throw new PXException("The previous operation has not been completed yet.");
    this._ProcessPending = true;
    if (!this._IsInstance)
    {
      object[] array = this._OuterView.Cache.Updated.ToArray<object>();
      bool flag;
      try
      {
        flag = this._ParametersDelegate == null || this._ParametersDelegate(this._PendingList(adapter.Parameters, adapter.SortColumns, adapter.Descendings, adapter.Filters));
      }
      catch
      {
        this._ProcessPending = false;
        foreach (object obj in this._OuterView.Cache.Updated)
        {
          if (!((IEnumerable<object>) array).Contains<object>(obj))
            this._OuterView.Cache.Remove(obj);
        }
        throw;
      }
      this._ProcessPending = flag;
      if (flag && this._Filter != null && this._Filter.Cache.AutomationFieldSelecting == null)
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
      if (flag && PXContext.GetSlot<AUSchedule>() == null && !adapter.ImportFlag)
      {
        PXLongOperation.StartOperation(this._Graph, (PXToggleAsyncDelegate) null);
        if (HttpContext.Current != null && this.IsUIRequest(HttpContext.Current) && this._Graph.IsProcessing)
          HttpContext.Current.Items.Add((object) (this._Graph.UID?.ToString() + "_Processing"), (object) true);
      }
    }
    if (adapter.ImportFlag)
    {
      this._ProcessPending = false;
      this.startPendingProcess(this._PendingList(adapter.Parameters, adapter.SortColumns, adapter.Descendings, adapter.Filters));
    }
    return adapter.Get();
  }

  protected virtual bool startPendingProcess(List<Table> items)
  {
    PXCache cache = this._OuterView.Cache;
    object current = cache.Current;
    if (this._IsInstance)
      cache.Current = current;
    cache.IsDirty = false;
    List<Table> list = this.GetSelectedItems(cache, (IEnumerable) items);
    AUSchedule schedule = PXContext.GetSlot<AUSchedule>();
    if (list.Count > 0)
    {
      if (this._AutoPersist)
      {
        try
        {
          this._Graph.Actions.PressSave(this._ProcessAllButton);
        }
        catch
        {
          PXLongOperation.ForceClearStatus(this._Graph);
          throw;
        }
        foreach (Table able in list)
          cache.SetStatus((object) able, PXEntryStatus.Updated);
      }
      if (!this._IsInstance)
      {
        if (schedule == null)
        {
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.ProcessDelegate(list, cancellationToken)));
        }
        else
        {
          PXContext.SetSlot<AUSchedule>((AUSchedule) null);
          AUSchedule scheduleparam = schedule;
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, scheduleparam, cancellationToken)));
          schedule = (AUSchedule) null;
        }
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
      }
      else
      {
        this.ProcessDelegate(list, CancellationToken.None);
        DialogManager.Clear(this._Graph);
      }
      return true;
    }
    if (schedule != null)
    {
      PXContext.SetSlot<AUSchedule>((AUSchedule) null);
      this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, schedule, cancellationToken)));
      return true;
    }
    PXLongOperation.ForceClearStatus(this._Graph);
    return false;
  }

  [PXButton(ImageKey = "AddNew", DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "Add", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected override IEnumerable _ScheduleAdd_(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this._Graph.Accessinfo.ScreenID))
    {
      AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
      AUSchedule auSchedule1 = new AUSchedule();
      string str = this._Graph.Accessinfo.ScreenID.Replace(".", "");
      instance.ScheduleCurrentScreen.Current.ScreenID = str;
      auSchedule1.ViewName = this._Graph.ViewNames[this.View];
      foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) this._Graph.Views)
      {
        if (view.Value is PXProcessingBase<Table>.ParametrizedView && view.Value != this.View)
        {
          auSchedule1.FilterName = view.Key;
          break;
        }
      }
      auSchedule1.GraphName = CustomizedTypeManager.GetTypeNotCustomized(this._Graph.GetType()).FullName;
      AUSchedule auSchedule2 = instance.Schedule.Insert(auSchedule1);
      instance.Schedule.Cache.IsDirty = false;
      PXCache cache = this._OuterView.Cache;
      List<AUScheduleFilter[]> source = new List<AUScheduleFilter[]>();
      int count = cache.Keys.Count;
      foreach (Table data in cache.Cached)
      {
        if (Convert.ToBoolean(cache.GetValue((object) data, this._SelectedField)) && EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus((object) data), PXEntryStatus.Inserted, PXEntryStatus.Updated))
        {
          AUScheduleFilter[] auScheduleFilterArray = new AUScheduleFilter[count];
          source.Add(auScheduleFilterArray);
          for (int index = 0; index < count; ++index)
          {
            string key = cache.Keys[index];
            object obj = PXFieldState.UnwrapValue(cache.GetValueExt((object) data, key));
            auScheduleFilterArray[index] = new AUScheduleFilter();
            auScheduleFilterArray[index].FieldName = key;
            if (obj != null)
            {
              auScheduleFilterArray[index].Condition = new int?(1);
              auScheduleFilterArray[index].Value = obj.ToString();
            }
            else
              auScheduleFilterArray[index].Condition = new int?(12);
          }
          auScheduleFilterArray[count - 1].Operator = new int?(1);
          auScheduleFilterArray[0].OpenBrackets = new int?(count > 1 ? 1 : 0);
          auScheduleFilterArray[count - 1].CloseBrackets = new int?(count > 1 ? 1 : 0);
        }
      }
      if (source.Count > 0 && count > 0)
      {
        AUScheduleFilter auScheduleFilter1 = ((IEnumerable<AUScheduleFilter>) source.First<AUScheduleFilter[]>()).First<AUScheduleFilter>();
        AUScheduleFilter auScheduleFilter2 = ((IEnumerable<AUScheduleFilter>) source.Last<AUScheduleFilter[]>()).Last<AUScheduleFilter>();
        AUScheduleFilter auScheduleFilter3 = auScheduleFilter1;
        int? nullable = auScheduleFilter3.OpenBrackets;
        auScheduleFilter3.OpenBrackets = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
        nullable = auScheduleFilter2.CloseBrackets;
        auScheduleFilter2.CloseBrackets = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
        auScheduleFilter2.Operator = new int?(0);
      }
      foreach (AUScheduleFilter[] auScheduleFilterArray in source)
      {
        foreach (AUScheduleFilter auScheduleFilter in auScheduleFilterArray)
          instance.Filters.Insert(auScheduleFilter);
      }
      auSchedule2.ActionName = "ProcessAll";
      foreach (PXCache pxCache in this._Graph.Caches.Values)
        pxCache.IsDirty = false;
      this._ScheduleAddPending = instance;
    }
    return adapter.Get();
  }
}
