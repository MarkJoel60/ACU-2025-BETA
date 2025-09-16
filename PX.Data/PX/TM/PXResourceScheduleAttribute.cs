// Decompiled with JetBrains decompiler
// Type: PX.TM.PXResourceScheduleAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#nullable enable
namespace PX.TM;

/// <exclude />
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Serializable]
public sealed class PXResourceScheduleAttribute : PXViewExtensionAttribute
{
  private const 
  #nullable disable
  string _ADDCOMMAND_POSTFIX = "$AddResource";
  private const string _DELETECOMMAND_POSTFIX = "$DeleteResource";
  private const string _DETAILSCOMMAND_POSTFIX = "$DetailsResource";
  private const string _PREVIOUSCOMMAND_POSTFIX = "$PreviousRegion";
  private const string _NEXTCOMMAND_POSTFIX = "$NextRegion";
  private const string _NAVIGATECOMMAND_POSTFIX = "$NavigateToResource";
  private const string _SEARCHCOMMAND_POSTFIX = "$SearchRegion";
  private const string _SEARCHNEXTCOMMAND_POSTFIX = "$SearchNextRegion";
  private const string _SEARCHPREVIOUSCOMMAND_POSTFIX = "$SearchPreviousRegion";
  private const string _UPDATECOMMAND_POSTFIX = "$UpdateResource";
  private const string _STATECACHE_POSTFIX = "$State";
  private const string _DURATION_FIELD = "$ScheduleResourceDuration";
  private const string _START_DATE_FIELD = "StartDate";
  private const string _END_DATE_FIELD = "EndDate";
  private const string _DURATION_DATE_FIELD = "Duration";
  private const string _SIMPLE_DURATION_DATE_FIELD = "SimpleDuration";
  private const string _DURATION_INPUTMASK = "### d\\ays ## hrs ## mins";
  private const string _DURATION_FORMAT = "{0,3}{1,2}{2,2}";
  private const long _ONE_MINUTE_TICKS = 600000000;
  private const long _DAY_TICKS = 864000000000;
  private static readonly string _ADD_BUTTON_IMG = Sprite.Main.GetFullUrl("RecordAdd");
  private static readonly string _DELETE_BUTTON_IMG = Sprite.Main.GetFullUrl("RecordDel");
  private static readonly string _DETAILS_BUTTON_IMG = Sprite.Main.GetFullUrl("DataEntry");
  private static readonly string _PREVIOUS_BUTTON_IMG = Sprite.Main.GetFullUrl("PagePrev");
  private static readonly string _NEXT_BUTTON_IMG = Sprite.Main.GetFullUrl("PageCurrent");
  private static readonly string _NAVIGATE_BUTTON_IMG = Sprite.Main.GetFullUrl("PageNext");
  private static readonly string _SEARCH_BUTTON_IMG = Sprite.Main.GetFullUrl("Search");
  private static readonly string _SEARCH_NEXT_BUTTON_IMG = Sprite.Main.GetFullUrl("SearchNext");
  private static readonly string _SEARCH_PREVIOUS_BUTTON_IMG = Sprite.Main.GetFullUrl("SearchPrev");
  private const string _SEARCH_BUTTON_TEXT = "Search First";
  private const string _SEARCH_NEXT_BUTTON_TEXT = "Search Forward";
  private const string _SEARCH_PREVIOUS_BUTTON_TEXT = "Search Backward";
  private const string _SEARCH_BUTTON_TITLE = "Search first available";
  private const string _SEARCH_NEXT_BUTTON_TITLE = "Search next available";
  private const string _SEARCH_PREVIOUS_BUTTON_TITLE = "Search previous available";
  private const int _DAYS_IN_WEEK = 7;
  private readonly System.Type _primaryTable;
  private readonly System.Type _resourceTable;
  private readonly System.Type _resourceStartBqlField;
  private readonly System.Type _resourceEndBqlField;
  private System.Type _descriptionBqlField;
  private System.Type _itemDescriptionBqlField;
  private System.Type _targetTable;
  private System.Type _targetStartBqlField;
  private System.Type _targetEndBqlField;
  private PXView _stateView;
  private PXView _restrictedView;
  private string _restrictedViewName;
  private PXResourceScheduleAttribute.Definition _definition;
  private bool _assumeWorkingTime = true;
  private bool _allowUpdate = true;

  private PXResourceScheduleAttribute()
  {
  }

  private PXResourceScheduleAttribute(PXResourceScheduleAttribute source)
  {
    this._primaryTable = source._primaryTable;
    this._resourceStartBqlField = source._resourceStartBqlField;
    this._resourceEndBqlField = source._resourceEndBqlField;
    this._definition = source._definition;
  }

  public PXResourceScheduleAttribute(
    System.Type primaryTable,
    System.Type resourceTable,
    System.Type resourceStartDateField,
    System.Type resourceEndDateField)
  {
    PXResourceScheduleAttribute.AssertBqlTable(primaryTable, nameof (primaryTable));
    this._primaryTable = primaryTable;
    PXResourceScheduleAttribute.AssertBqlTable(resourceTable, nameof (resourceTable));
    this._resourceTable = resourceTable;
    PXResourceScheduleAttribute.AssertBqlField(resourceStartDateField, nameof (resourceStartDateField));
    this._resourceStartBqlField = resourceStartDateField;
    PXResourceScheduleAttribute.AssertBqlField(resourceEndDateField, nameof (resourceEndDateField));
    this._resourceEndBqlField = resourceEndDateField;
  }

  public override void ViewCreated(PXGraph graph, string viewName)
  {
    this.InitializeDefinition(graph, viewName);
    this.AddStateCache(graph, viewName);
    this.AddDurationField(graph);
    this.AddActions(graph);
    this.ReplaceMainView(graph, viewName);
  }

  private void ReplaceMainView(PXGraph graph, string viewName)
  {
    PXView view = graph.Views[viewName];
    MethodInfo method = PXResourceScheduleAttribute.MakeGenericType(typeof (PXResourceScheduleAttribute.PXRestrictedSelectView<,,>), this._resourceTable, this._resourceStartBqlField, this._resourceEndBqlField).GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
    graph.Views[viewName] = this._restrictedView = (PXView) method.Invoke((object) null, new object[3]
    {
      (object) view,
      (object) this._definition.StateView,
      (object) viewName
    });
    this._restrictedViewName = viewName;
  }

  public static void AssumeWorkingTime(PXView view, bool assume)
  {
    PXResourceScheduleAttribute attribute = PXResourceScheduleAttribute.GetAttribute(view);
    if (attribute == null)
      return;
    attribute._assumeWorkingTime = assume;
  }

  public static void AllowUpdate(PXView view, bool allow)
  {
    PXResourceScheduleAttribute attribute = PXResourceScheduleAttribute.GetAttribute(view);
    if (attribute == null)
      return;
    attribute._allowUpdate = allow;
  }

  private static PXResourceScheduleAttribute GetAttribute(PXView view)
  {
    if (view.Attributes != null)
    {
      foreach (PXViewExtensionAttribute attribute1 in view.Attributes)
      {
        if (attribute1 is PXResourceScheduleAttribute attribute2)
          return attribute2;
      }
    }
    return (PXResourceScheduleAttribute) null;
  }

  public System.Type DescriptionBqlField
  {
    get => this._descriptionBqlField;
    set
    {
      PXResourceScheduleAttribute.AssertBqlField(value, nameof (value));
      this._descriptionBqlField = value;
    }
  }

  public System.Type ItemDescriptionBqlField
  {
    get => this._itemDescriptionBqlField;
    set
    {
      PXResourceScheduleAttribute.AssertBqlField(value, nameof (value));
      this._itemDescriptionBqlField = value;
    }
  }

  public System.Type TargetTable
  {
    get => this._targetTable;
    set
    {
      PXResourceScheduleAttribute.AssertBqlTable(value, nameof (value));
      this._targetTable = value;
    }
  }

  public System.Type TargetStartBqlField
  {
    get => this._targetStartBqlField;
    set
    {
      PXResourceScheduleAttribute.AssertBqlField(value, nameof (value));
      this._targetStartBqlField = value;
    }
  }

  public System.Type TargetEndBqlField
  {
    get => this._targetEndBqlField;
    set
    {
      PXResourceScheduleAttribute.AssertBqlField(value, nameof (value));
      this._targetEndBqlField = value;
    }
  }

  private static string GenerateKey(System.Type graphType, string dataMember)
  {
    return $"{graphType.FullName}$${dataMember}";
  }

  [PXButton(ImageKey = "RecordAdd")]
  private IEnumerable AddRecord(PXAdapter adapter)
  {
    if (PXResourceScheduleAttribute.ConfirmAction(adapter))
    {
      PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler addRecordHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetAddRecordHandlers(adapter.View.Graph, this._restrictedViewName);
      if (addRecordHandlers != null)
      {
        addRecordHandlers();
        this._restrictedView.Clear();
        yield break;
      }
    }
  }

  [PXButton(ImageKey = "RecordDel")]
  private IEnumerable DeleteRecord(PXAdapter adapter)
  {
    if (PXResourceScheduleAttribute.ConfirmAction(adapter))
    {
      PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler removeRecordHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetRemoveRecordHandlers(adapter.View.Graph, this._restrictedViewName);
      PXResourceScheduleAttribute.PXScheduleState scheduleState;
      if (removeRecordHandlers != null && (scheduleState = PXResourceScheduleAttribute.GetScheduleState(this._stateView)) != null)
      {
        removeRecordHandlers(scheduleState.SelectedItem);
        this._restrictedView.Clear();
      }
      if (this._stateView.Cache != null && this._stateView.Cache.Current != null)
      {
        object selectedItem = ((PXResourceScheduleAttribute.PXScheduleState) this._stateView.Cache.Current).SelectedItem;
        if (selectedItem != null)
        {
          this._restrictedView.Cache.Delete(selectedItem);
          yield break;
        }
      }
    }
  }

  [PXButton(ImageKey = "DataEntry")]
  private IEnumerable ShowDetails(PXAdapter adapter)
  {
    PXResourceScheduleAttribute.PXScheduleState scheduleState;
    if ((scheduleState = PXResourceScheduleAttribute.GetScheduleState(this._stateView)) != null && scheduleState.SelectedItem != null)
    {
      PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler viewDetailsHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetViewDetailsHandlers(adapter.View.Graph, this._restrictedViewName);
      if (viewDetailsHandlers != null)
      {
        viewDetailsHandlers(scheduleState.SelectedItem);
        this._restrictedView.Clear();
      }
      else
      {
        PXRedirectHelper.TryOpenPopup(adapter.View.Graph.Caches[this._resourceTable], scheduleState.SelectedItem, (string) null);
        yield break;
      }
    }
  }

  [PXButton(ImageKey = "PagePrev")]
  private IEnumerable Previous(PXAdapter adapter)
  {
    if (this._stateView.Cache != null && this._stateView.Cache.Current != null)
    {
      PXResourceScheduleAttribute.ShiftDateRegion((PXResourceScheduleAttribute.PXScheduleState) this._stateView.Cache.Current, false);
      yield break;
    }
  }

  [PXButton(ImageKey = "PageNext")]
  private IEnumerable Next(PXAdapter adapter)
  {
    if (this._stateView.Cache != null && this._stateView.Cache.Current != null)
    {
      PXResourceScheduleAttribute.ShiftDateRegion((PXResourceScheduleAttribute.PXScheduleState) this._stateView.Cache.Current, true);
      yield break;
    }
  }

  [PXButton(ImageKey = "PageNext")]
  private IEnumerable Navigate(PXAdapter adapter)
  {
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState = PXResourceScheduleAttribute.CurrentState(adapter.View.Graph, this._definition.StateView);
    System.DateTime? nullable = pxScheduleState.TargetStartDate;
    if (nullable.HasValue)
    {
      nullable = pxScheduleState.TargetStartDate;
      System.DateTime date = (nullable ?? System.DateTime.Today).Date;
      System.DateTime dateTime = date.AddDays(1.0);
      nullable = pxScheduleState.TargetEndDate;
      if (nullable.HasValue)
      {
        nullable = pxScheduleState.TargetEndDate;
        if (nullable.Value.Ticks - date.Ticks > 864000000000L)
        {
          nullable = pxScheduleState.TargetEndDate;
          dateTime = nullable.Value.Date.AddDays(1.0);
        }
      }
      pxScheduleState.StartDate = new System.DateTime?(date);
      pxScheduleState.EndDate = new System.DateTime?(dateTime);
      yield break;
    }
  }

  [PXButton(ImageKey = "Search", Tooltip = "Search first available")]
  [PXUIField(DisplayName = "Search First")]
  private IEnumerable Search(PXAdapter adapter)
  {
    this.Search(adapter.View.Graph, new System.DateTime?(), new System.DateTime?(), false, false, 0L);
    yield break;
  }

  [PXButton(ImageKey = "SearchNext", Tooltip = "Search next available")]
  [PXUIField(DisplayName = "Search Forward")]
  private IEnumerable SearchNext(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    PXResourceScheduleAttribute scheduleAttribute1 = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    PXGraph graph1 = adapter.View.Graph;
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState = PXResourceScheduleAttribute.CurrentState(graph1, scheduleAttribute1._definition.StateView);
    System.DateTime? nullable1 = pxScheduleState.TargetEndDate;
    System.DateTime? nullable2;
    if (!nullable1.HasValue)
    {
      nullable2 = pxScheduleState.StartDate;
    }
    else
    {
      nullable1 = pxScheduleState.TargetEndDate;
      nullable2 = new System.DateTime?(nullable1.Value);
    }
    System.DateTime? nullable3 = nullable2;
    System.DateTime dateTime1;
    if (PXResourceScheduleAttribute.TryParseParameters<System.DateTime>(adapter.Parameters, out dateTime1))
      nullable3 = new System.DateTime?(dateTime1);
    long dateTime2;
    PXResourceScheduleAttribute.TryParseParameters<long>(adapter.Parameters, out dateTime2);
    PXResourceScheduleAttribute scheduleAttribute2 = scheduleAttribute1;
    PXGraph graph2 = graph1;
    System.DateTime? customStartEdge = nullable3;
    nullable1 = new System.DateTime?();
    System.DateTime? customEndEdge = nullable1;
    long regionWidth = dateTime2;
    scheduleAttribute2.Search(graph2, customStartEdge, customEndEdge, false, true, regionWidth);
    return false;
  }

  [PXButton(ImageKey = "SearchPrev", Tooltip = "Search previous available")]
  [PXUIField(DisplayName = "Search Backward")]
  private IEnumerable SearchPrevious(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    PXResourceScheduleAttribute scheduleAttribute1 = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    PXGraph graph1 = adapter.View.Graph;
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState = PXResourceScheduleAttribute.CurrentState(graph1, scheduleAttribute1._definition.StateView);
    System.DateTime? nullable1 = pxScheduleState.TargetStartDate;
    System.DateTime? nullable2;
    if (!nullable1.HasValue)
    {
      nullable2 = pxScheduleState.EndDate;
    }
    else
    {
      nullable1 = pxScheduleState.TargetStartDate;
      nullable2 = new System.DateTime?(nullable1.Value);
    }
    System.DateTime? nullable3 = nullable2;
    System.DateTime dateTime1;
    if (PXResourceScheduleAttribute.TryParseParameters<System.DateTime>(adapter.Parameters, out dateTime1))
      nullable3 = new System.DateTime?(dateTime1);
    long dateTime2;
    PXResourceScheduleAttribute.TryParseParameters<long>(adapter.Parameters, out dateTime2);
    PXResourceScheduleAttribute scheduleAttribute2 = scheduleAttribute1;
    PXGraph graph2 = graph1;
    nullable1 = new System.DateTime?();
    System.DateTime? customStartEdge = nullable1;
    System.DateTime? customEndEdge = nullable3;
    long regionWidth = dateTime2;
    scheduleAttribute2.Search(graph2, customStartEdge, customEndEdge, true, true, regionWidth);
    return false;
  }

  [PXButton]
  private IEnumerable UpdateResource(PXAdapter adapter)
  {
    this.UpdateTargetResource(adapter.View.Graph, PXResourceScheduleAttribute.GetScheduleState(this._stateView));
    yield break;
  }

  private static bool TryParseParameters<T>(object[] adapterParameters, out T dateTime)
  {
    dateTime = default (T);
    if (adapterParameters != null)
    {
      foreach (object adapterParameter in adapterParameters)
      {
        if (adapterParameter is T obj)
        {
          dateTime = obj;
          return true;
        }
      }
    }
    return false;
  }

  private void Search(
    PXGraph graph,
    System.DateTime? customStartEdge,
    System.DateTime? customEndEdge,
    bool backward,
    bool loop,
    long regionWidth)
  {
    PXResourceScheduleAttribute.PXScheduleState state = PXResourceScheduleAttribute.CurrentState(graph, this._definition.StateView);
    PXResourceScheduleAttribute attribute = PXResourceScheduleAttribute.GetAttribute(graph.Views[state.MainView]);
    bool assumeWorkingTime = attribute == null || attribute._assumeWorkingTime;
    long ticks1 = state.TargetEndDate.Value.Ticks;
    System.DateTime? nullable = state.TargetStartDate;
    long ticks2 = nullable.Value.Ticks;
    long requiredWidth = ticks1 - ticks2;
    nullable = state.TargetStartDate;
    if (!nullable.HasValue)
      return;
    nullable = state.TargetEndDate;
    if (!nullable.HasValue)
      return;
    nullable = state.StartDate;
    if (!nullable.HasValue && !customStartEdge.HasValue)
      return;
    nullable = state.EndDate;
    if (!nullable.HasValue && !customEndEdge.HasValue)
      return;
    System.DateTime dateTime1;
    if (!customStartEdge.HasValue)
    {
      nullable = state.StartDate;
      dateTime1 = nullable.Value;
    }
    else
      dateTime1 = customStartEdge.Value;
    System.DateTime edgeStartDate = dateTime1;
    System.DateTime dateTime2;
    if (!customEndEdge.HasValue)
    {
      nullable = state.EndDate;
      dateTime2 = nullable.Value;
    }
    else
      dateTime2 = customEndEdge.Value;
    System.DateTime edgeEndDate = dateTime2;
    long num1 = edgeEndDate.Ticks - edgeStartDate.Ticks;
    if (backward)
      num1 *= -1L;
    System.DateTime? startDate = state.StartDate;
    System.DateTime? endDate = state.EndDate;
    state.StartDate = new System.DateTime?(edgeStartDate);
    state.EndDate = new System.DateTime?(edgeEndDate);
    System.DateTime newStartDate;
    System.DateTime newEndDate;
    bool noWorkingTime;
    bool flag = this.SearchNext(graph.Caches[this._resourceTable], edgeStartDate, edgeEndDate, requiredWidth, backward, assumeWorkingTime, out newStartDate, out newEndDate, out noWorkingTime);
    int num2 = 1;
    if (loop && System.Math.Abs(regionWidth - num1) > 0L)
      num1 = regionWidth * (backward ? -1L : 1L);
    for (; loop && !flag; flag = this.SearchNext(graph.Caches[this._resourceTable], edgeStartDate, edgeEndDate, requiredWidth, backward, assumeWorkingTime, out newStartDate, out newEndDate, out noWorkingTime))
    {
      if (noWorkingTime)
        ++num2;
      else
        num2 = 0;
      if (assumeWorkingTime && num2 >= 7)
        throw new Exception("There is no free time.");
      if (backward)
      {
        state.EndDate = new System.DateTime?(edgeEndDate = edgeStartDate);
        state.StartDate = new System.DateTime?(edgeStartDate = edgeStartDate.AddTicks(num1));
      }
      else
      {
        state.StartDate = new System.DateTime?(edgeStartDate = edgeEndDate);
        state.EndDate = new System.DateTime?(edgeEndDate = edgeEndDate.AddTicks(num1));
      }
    }
    if (flag)
    {
      PXResourceScheduleAttribute.SetTargetStartEndDates(state, newStartDate, newEndDate);
      this.UpdateTargetResource(graph, state);
    }
    state.StartDate = startDate;
    state.EndDate = endDate;
  }

  private bool SearchNext(
    PXCache resourceCache,
    System.DateTime edgeStartDate,
    System.DateTime edgeEndDate,
    long requiredWidth,
    bool backward,
    bool assumeWorkingTime,
    out System.DateTime newStartDate,
    out System.DateTime newEndDate,
    out bool noWorkingTime)
  {
    noWorkingTime = false;
    long ticks1 = edgeStartDate.Ticks;
    long ticks2 = edgeEndDate.Ticks;
    SortedDictionary<long, int> marks1 = backward ? new SortedDictionary<long, int>((IComparer<long>) new PXResourceScheduleAttribute.LongDescComparer()) : new SortedDictionary<long, int>();
    SortedDictionary<long, int> marks2 = new SortedDictionary<long, int>();
    PXGraph graph = resourceCache.Graph;
    PXCache cache = graph.Views[this._definition.ViewName].Cache;
    System.Type itemType = cache.GetItemType();
    List<int> intList = new List<int>();
    foreach (object data1 in PXResourceScheduleAttribute.Select(graph, this._definition.ViewName, this._resourceTable.Name, this._definition.StartDateField, this._definition.EndDateField))
    {
      object data2 = PXResourceScheduleAttribute.ExtractItem(data1, itemType);
      int objectHashCode = cache.GetObjectHashCode(data2);
      System.DateTime dateTime1;
      if (assumeWorkingTime && !intList.Contains(objectHashCode))
      {
        System.DateTime dateTime2;
        for (System.DateTime date = edgeStartDate.Date; date < edgeEndDate; date = dateTime2)
        {
          long startTicks1 = System.Math.Max(date.Ticks, ticks1);
          dateTime2 = date.AddDays(1.0);
          long endTicks1 = System.Math.Min(dateTime2.Ticks, ticks2);
          System.DateTime? startWorkTime = PXResourceScheduleAttribute.GetStartWorkTime(graph, this._definition.ViewName, data2, date);
          System.DateTime? endWorkTime = PXResourceScheduleAttribute.GetEndWorkTime(graph, this._definition.ViewName, data2, date);
          if (!startWorkTime.HasValue || !endWorkTime.HasValue)
          {
            PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks1, startTicks1, endTicks1);
            PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks2, startTicks1, endTicks1);
          }
          else
          {
            dateTime1 = startWorkTime.Value;
            long ticks3 = dateTime1.Ticks;
            if (ticks3 > startTicks1)
            {
              long endTicks2 = System.Math.Min(ticks3, ticks2);
              PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks1, startTicks1, endTicks2);
              PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks2, startTicks1, endTicks2);
            }
            dateTime1 = endWorkTime.Value;
            long ticks4 = dateTime1.Ticks;
            if (ticks4 < endTicks1)
            {
              long startTicks2 = System.Math.Max(ticks4, ticks1);
              PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks1, startTicks2, endTicks1);
              PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks2, startTicks2, endTicks1);
            }
          }
        }
        intList.Add(objectHashCode);
      }
      object data3 = PXResourceScheduleAttribute.ExtractItem(data1, this._resourceTable);
      System.DateTime? nullable1 = (System.DateTime?) resourceCache.GetValue(data3, this._definition.StartDateField);
      System.DateTime? nullable2 = (System.DateTime?) resourceCache.GetValue(data3, this._definition.EndDateField);
      if (nullable1.HasValue || nullable2.HasValue)
      {
        long val1_1;
        if (!nullable1.HasValue)
        {
          val1_1 = ticks1;
        }
        else
        {
          dateTime1 = nullable1.Value;
          val1_1 = dateTime1.Ticks;
        }
        long val2_1 = ticks1;
        long startTicks = PXResourceScheduleAttribute.RoundTicks(System.Math.Max(val1_1, val2_1), true);
        long val1_2;
        if (!nullable2.HasValue)
        {
          val1_2 = ticks2;
        }
        else
        {
          dateTime1 = nullable2.Value;
          val1_2 = dateTime1.Ticks;
        }
        long val2_2 = ticks2;
        long endTicks = PXResourceScheduleAttribute.RoundTicks(System.Math.Min(val1_2, val2_2), false);
        if (startTicks < endTicks && startTicks <= ticks2 && endTicks >= ticks1)
          PXResourceScheduleAttribute.AddBusyMark((IDictionary<long, int>) marks1, startTicks, endTicks);
      }
    }
    int num1 = 0;
    long num2 = backward ? ticks2 : ticks1;
    foreach (KeyValuePair<long, int> keyValuePair in marks1)
    {
      num1 += keyValuePair.Value;
      if (num2 > 0L && (backward && num1 >= 1 || !backward && num1 <= -1) && System.Math.Abs(keyValuePair.Key - num2) >= requiredWidth)
      {
        newStartDate = new System.DateTime(num2 - (backward ? requiredWidth : 0L));
        newEndDate = new System.DateTime(num2 + (backward ? 0L : requiredWidth));
        return true;
      }
      num2 = num1 == 0 ? keyValuePair.Key : 0L;
    }
    if (num1 == 0 && backward && num2 - ticks1 >= requiredWidth || !backward && ticks2 - num2 >= requiredWidth)
    {
      newStartDate = new System.DateTime(num2 - (backward ? requiredWidth : 0L));
      newEndDate = new System.DateTime(num2 + (backward ? 0L : requiredWidth));
      return true;
    }
    ref System.DateTime local1 = ref newStartDate;
    ref System.DateTime local2 = ref newEndDate;
    System.DateTime dateTime3 = new System.DateTime();
    System.DateTime dateTime4;
    System.DateTime dateTime5 = dateTime4 = dateTime3;
    local2 = dateTime4;
    System.DateTime dateTime6 = dateTime5;
    local1 = dateTime6;
    if (assumeWorkingTime)
    {
      noWorkingTime = true;
      long num3 = ticks1;
      int num4 = 0;
      foreach (KeyValuePair<long, int> keyValuePair in marks2)
      {
        num4 += keyValuePair.Value;
        if (num3 > 0L && num1 <= -1 && keyValuePair.Key > num3)
        {
          noWorkingTime = false;
          break;
        }
        num3 = num4 == 0 ? keyValuePair.Key : 0L;
      }
      if (num4 == 0 && ticks2 > num3)
        noWorkingTime = false;
    }
    return false;
  }

  private static void AddBusyMark(IDictionary<long, int> marks, long startTicks, long endTicks)
  {
    if (!marks.ContainsKey(startTicks))
      marks.Add(startTicks, -1);
    else
      --marks[startTicks];
    if (!marks.ContainsKey(endTicks))
      marks.Add(endTicks, 1);
    else
      ++marks[endTicks];
  }

  private static long RoundTicks(long ticks, bool left) => ticks;

  private static bool ConfirmAction(PXAdapter adapter)
  {
    if (adapter.Parameters == null || adapter.Parameters.Length == 0)
      return true;
    if (adapter.Parameters[0].ToString() == "simple")
      return adapter.View.Ask(adapter.Parameters[1].ToString(), MessageButtons.YesNo) == WebDialogResult.Yes;
    string key = Convert.ToString(adapter.Parameters[1]);
    return adapter.View.Graph.Views[key].AskExt() == WebDialogResult.OK;
  }

  private static IEnumerable Select(
    PXGraph graph,
    string viewName,
    string resourceTableName,
    string startDateField,
    string endDateField)
  {
    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
    PXView view = graph.Views[viewName];
    view.Clear();
    foreach (string key in (IEnumerable<string>) view.Cache.Keys)
      dictionary.Add(key, false);
    dictionary.Add($"{resourceTableName}__{startDateField}", false);
    dictionary.Add($"{resourceTableName}__{endDateField}", true);
    string[] strArray = new string[dictionary.Count];
    dictionary.Keys.CopyTo(strArray, 0);
    bool[] flagArray = new bool[dictionary.Count];
    dictionary.Values.CopyTo(flagArray, 0);
    int startRow = 0;
    int totalRows = 0;
    return graph.ExecuteSelect(viewName, (object[]) null, (object[]) null, strArray, flagArray, (PXFilterRow[]) null, ref startRow, -1, ref totalRows);
  }

  private static void SetTargetStartEndDates(
    PXGraph graph,
    string stateViewName,
    System.DateTime startDate,
    System.DateTime endDate)
  {
    PXResourceScheduleAttribute.SetTargetStartEndDates(PXResourceScheduleAttribute.CurrentState(graph, stateViewName), startDate, endDate);
  }

  private static void SetTargetStartEndDates(
    PXResourceScheduleAttribute.PXScheduleState state,
    System.DateTime startDate,
    System.DateTime endDate)
  {
    state.TargetStartDate = new System.DateTime?(PXResourceScheduleAttribute.RoundDateTime(startDate, 5));
    state.TargetEndDate = new System.DateTime?(PXResourceScheduleAttribute.RoundDateTime(endDate, 5));
  }

  private static void SetStartEndDates(
    PXGraph graph,
    string stateViewName,
    System.DateTime startDate,
    System.DateTime endDate)
  {
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState = PXResourceScheduleAttribute.CurrentState(graph, stateViewName);
    pxScheduleState.StartDate = new System.DateTime?(startDate);
    pxScheduleState.EndDate = new System.DateTime?(endDate);
  }

  private void UpdateTargetResource(
    PXGraph graph,
    PXResourceScheduleAttribute.PXScheduleState state)
  {
    if (!(this._targetTable != (System.Type) null) || !(this._targetStartBqlField != (System.Type) null) && !(this._targetEndBqlField != (System.Type) null) || this._stateView.Cache == null || this._stateView.Cache.Current == null)
      return;
    PXCache cach = graph.Caches[this._targetTable];
    if (this._targetStartBqlField != (System.Type) null)
      cach.SetValue(cach.Current, cach.GetField(this._targetStartBqlField), (object) state.TargetStartDate);
    if (this._targetEndBqlField != (System.Type) null)
      cach.SetValue(cach.Current, cach.GetField(this._targetEndBqlField), (object) state.TargetEndDate);
    cach.Update(cach.Current);
  }

  private static System.DateTime RoundDateTime(System.DateTime date, int minuteStep)
  {
    return new System.DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute - date.Minute % minuteStep, 0);
  }

  private static object ExtractItem(object data, System.Type itemType)
  {
    return data is PXResult pxResult ? pxResult[itemType] : (object) null;
  }

  private static PXResourceScheduleAttribute.PXScheduleState CurrentState(
    PXGraph graph,
    string stateViewName)
  {
    return PXResourceScheduleAttribute.GetScheduleState(graph.Views[stateViewName]);
  }

  private static void AssertBqlTable(System.Type table, string parameter)
  {
    if (table == (System.Type) null)
      throw new NullReferenceException(parameter);
    if (!typeof (IBqlTable).IsAssignableFrom(table))
      throw new Exception($"The type {table} doesn't impelemt the IBqlTable interface.");
  }

  private static void AssertBqlField(System.Type field, string parameter)
  {
    if (field == (System.Type) null)
      throw new NullReferenceException(parameter);
    if (!typeof (IBqlField).IsAssignableFrom(field))
      throw new Exception($"The type {field} doesn't impelemt the IBqlField interface.");
  }

  private void InitializeDefinition(PXGraph graph, string viewName)
  {
    PXCache cach = graph.Caches[this._resourceTable];
    PXCache cache = graph.Views[viewName].Cache;
    string name = cache.GetItemType().Name;
    this._definition = new PXResourceScheduleAttribute.Definition(name + "$State", this._resourceTable, viewName);
    this._definition.AddItemAction = name + "$AddResource";
    this._definition.DeleteItemAction = name + "$DeleteResource";
    this._definition.DetailsItemAction = name + "$DetailsResource";
    this._definition.PreviousAction = name + "$PreviousRegion";
    this._definition.NextAction = name + "$NextRegion";
    this._definition.NavigateAction = name + "$NavigateToResource";
    this._definition.SearchAction = name + "$SearchRegion";
    this._definition.SearchNextAction = name + "$SearchNextRegion";
    this._definition.SearchPreviousAction = name + "$SearchPreviousRegion";
    this._definition.UpdateResourceAction = name + "$UpdateResource";
    this._definition.ResourceStartDateField = cach.GetField(this._resourceStartBqlField);
    this._definition.ResourceEndDateField = cach.GetField(this._resourceEndBqlField);
    this._definition.StartDateField = "StartDate";
    this._definition.EndDateField = "EndDate";
    this._definition.DurationField = "Duration";
    this._definition.SimpleDurationField = "SimpleDuration";
    this._definition.DescriptionField = cache.GetField(this.DescriptionBqlField);
    this._definition.ResourceDescriptionField = cach.GetField(this.ItemDescriptionBqlField);
    PXResourceScheduleAttribute.Definition.SaveDefinition(graph, this._definition);
  }

  private void AddDurationField(PXGraph graph)
  {
    graph.Caches[this._resourceTable].Fields.Add("$ScheduleResourceDuration");
    graph.FieldSelecting.AddHandler(this._resourceTable, "$ScheduleResourceDuration", new PXFieldSelecting(this._resourceTable_DURATION_FIELD_FieldSelecting));
    graph.RowSelected.AddHandler(this._resourceTable, new PXRowSelected(this._resourceTable_RowSelected));
    graph.RowUpdating.AddHandler(this._resourceTable, new PXRowUpdating(this._resourceTable_RowUpdating));
  }

  private void _resourceTable_DURATION_FIELD_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.IsAltered)
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(7), new bool?(), "$ScheduleResourceDuration", new bool?(false), new int?(), PXMessages.LocalizeNoPrefix("### d\\ays ## hrs ## mins"), (string[]) null, (string[]) null, new bool?(), (string) null);
    if (e.ReturnValue == null)
      return;
    TimeSpan timeSpan = new TimeSpan(0, 0, (int) e.ReturnValue, 0);
    e.ReturnValue = (object) $"{timeSpan.Days,3}{timeSpan.Hours,2}{timeSpan.Minutes,2}";
  }

  private void _resourceTable_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable = new int?();
    System.DateTime? resourceStartDate = this.GetResourceStartDate(e.Row);
    System.DateTime? resourceEndDate = this.GetResourceEndDate(e.Row);
    if (resourceStartDate.HasValue && resourceEndDate.HasValue && resourceStartDate.Value <= resourceEndDate.Value)
      nullable = new int?(Convert.ToInt32((resourceEndDate.Value - resourceStartDate.Value).Ticks / 600000000L));
    this.SetResourceDuration(e.Row, nullable);
  }

  private void _resourceTable_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewRow == null)
      return;
    System.DateTime? resourceStartDate = this.GetResourceStartDate(e.NewRow);
    if (!resourceStartDate.HasValue)
      return;
    System.DateTime? resourceEndDate1 = this.GetResourceEndDate(e.Row);
    System.DateTime? resourceEndDate2 = this.GetResourceEndDate(e.NewRow);
    int? resourceDuration1 = this.GetResourceDuration(e.Row);
    int? resourceDuration2 = this.GetResourceDuration(e.NewRow);
    System.DateTime? nullable1 = resourceEndDate1;
    System.DateTime? nullable2 = resourceEndDate2;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || !resourceDuration2.HasValue)
      return;
    int? nullable3 = resourceDuration1;
    int? nullable4 = resourceDuration2;
    if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
      return;
    this.SetResourceEndDate(e.NewRow, new System.DateTime?(resourceStartDate.Value.AddMinutes((double) resourceDuration2.Value)));
  }

  private System.DateTime? GetResourceStartDate(object row)
  {
    return (System.DateTime?) this._restrictedView.Cache.GetValue(row, this._definition.ResourceStartDateField);
  }

  private System.DateTime? GetResourceEndDate(object row)
  {
    return (System.DateTime?) this._restrictedView.Cache.GetValue(row, this._definition.ResourceEndDateField);
  }

  private int? GetResourceDuration(object row)
  {
    return (int?) this._restrictedView.Cache.GetValue(row, "$ScheduleResourceDuration");
  }

  private void SetResourceEndDate(object row, System.DateTime? value)
  {
    this._restrictedView.Cache.SetValue(row, this._definition.ResourceEndDateField, (object) value);
  }

  private void SetResourceDuration(object row, int? value)
  {
    this._restrictedView.Cache.SetValue(row, "$ScheduleResourceDuration", (object) value);
  }

  private void AddStateCache(PXGraph graph, string viewName)
  {
    PXCache pxCache = (PXCache) new PXNotCleanableCache<PXResourceScheduleAttribute.PXScheduleState>(graph);
    pxCache.Load();
    graph.Caches[typeof (PXResourceScheduleAttribute.PXScheduleState)] = pxCache;
    this._stateView = new PXView(graph, false, (BqlCommand) new PX.Data.Select<PXResourceScheduleAttribute.PXScheduleState>(), (Delegate) new PXSelectDelegate(this._stateHandler));
    graph.Views.Add(this._definition.StateView, this._stateView);
    graph.RowPersisting.AddHandler<PXResourceScheduleAttribute.PXScheduleState>(new PXRowPersisting(this.PXScheduleState_RowPersisting));
    graph.RowSelected.AddHandler<PXResourceScheduleAttribute.PXScheduleState>(new PXRowSelected(this.PXScheduleState_RowSelected));
    graph.RowUpdating.AddHandler<PXResourceScheduleAttribute.PXScheduleState>(new PXRowUpdating(this.PXScheduleState_RowUpdating));
    if (PXResourceScheduleAttribute.GetScheduleState(this._stateView, this) != null)
      return;
    this._stateView.Cache.Insert((object) new PXResourceScheduleAttribute.PXScheduleState()
    {
      Key = this._resourceTable.FullName,
      TargetTable = (object) this._targetTable,
      MainView = viewName,
      AllowUpdate = new bool?(this._allowUpdate && graph.Caches[this._targetTable].AllowUpdate)
    });
    PXResourceScheduleAttribute.GetScheduleState(this._stateView).SelectedItem = (object) null;
    this._stateView.Cache.IsDirty = false;
  }

  private static PXResourceScheduleAttribute.PXScheduleState GetScheduleState(PXView stateView)
  {
    return PXResourceScheduleAttribute.GetScheduleState(stateView, (PXResourceScheduleAttribute) null);
  }

  private static PXResourceScheduleAttribute.PXScheduleState GetScheduleState(
    PXView stateView,
    PXResourceScheduleAttribute att)
  {
    PXResourceScheduleAttribute.PXScheduleState scheduleState = (PXResourceScheduleAttribute.PXScheduleState) stateView.SelectSingle();
    if (scheduleState != null && scheduleState.TargetTable != null)
    {
      if (att == null)
        att = PXResourceScheduleAttribute.GetAttribute(stateView.Graph.Views[scheduleState.MainView]);
      scheduleState.AllowUpdate = new bool?((att == null || att._allowUpdate) && stateView.Graph.Caches[(System.Type) scheduleState.TargetTable].AllowUpdate);
    }
    return scheduleState;
  }

  private static System.DateTime? GetStartWorkTime(
    PXGraph graph,
    string viewName,
    object item,
    System.DateTime date)
  {
    PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler startTimeHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetGetWorkStartTimeHandlers(graph, viewName);
    return startTimeHandlers == null ? new System.DateTime?() : startTimeHandlers(item, date);
  }

  private static System.DateTime? GetEndWorkTime(
    PXGraph graph,
    string viewName,
    object item,
    System.DateTime date)
  {
    PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler workEndTimeHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetGetWorkEndTimeHandlers(graph, viewName);
    return workEndTimeHandlers == null ? new System.DateTime?() : workEndTimeHandlers(item, date);
  }

  private IEnumerable _stateHandler()
  {
    foreach (PXResourceScheduleAttribute.PXScheduleState pxScheduleState in this._stateView.Cache.Inserted)
    {
      if (string.Compare(pxScheduleState.Key, this._resourceTable.FullName, true) == 0)
        yield return (object) pxScheduleState;
    }
  }

  private void PXScheduleState_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  private void PXScheduleState_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PXResourceScheduleAttribute.PXScheduleState row))
      return;
    PXCache cach;
    if (this._targetTable != (System.Type) null && (this._targetStartBqlField != (System.Type) null || this._targetEndBqlField != (System.Type) null) && (cach = cache.Graph.Caches[this._targetTable]) != null)
    {
      if (this._targetStartBqlField != (System.Type) null)
        row.TargetStartDate = (System.DateTime?) cach.GetValue(cach.Current, cach.GetField(this._targetStartBqlField));
      if (this._targetEndBqlField != (System.Type) null)
        row.TargetEndDate = (System.DateTime?) cach.GetValue(cach.Current, cach.GetField(this._targetEndBqlField));
    }
    PXResourceScheduleAttribute.EnsureDateRegion(row);
    row.Duration = new int?();
    if (!row.StartDate.HasValue)
      return;
    System.DateTime? nullable1 = row.EndDate;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.StartDate;
    System.DateTime dateTime1 = nullable1.Value;
    nullable1 = row.EndDate;
    System.DateTime dateTime2 = nullable1.Value;
    if (!(dateTime1 <= dateTime2))
      return;
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState = row;
    nullable1 = row.EndDate;
    System.DateTime dateTime3 = nullable1.Value;
    nullable1 = row.StartDate;
    System.DateTime dateTime4 = nullable1.Value;
    int? nullable2 = new int?(Convert.ToInt32((dateTime3 - dateTime4).Ticks / 600000000L));
    pxScheduleState.Duration = nullable2;
  }

  private void PXScheduleState_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    PXResourceScheduleAttribute.PXScheduleState row = e.Row as PXResourceScheduleAttribute.PXScheduleState;
    PXResourceScheduleAttribute.PXScheduleState newRow = e.NewRow as PXResourceScheduleAttribute.PXScheduleState;
    if (row == null || newRow == null)
      return;
    System.DateTime? nullable1 = newRow.StartDate;
    System.DateTime dateTime1;
    if (nullable1.HasValue)
    {
      nullable1 = row.EndDate;
      System.DateTime? endDate = newRow.EndDate;
      if ((nullable1.HasValue == endDate.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == endDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        int? duration1 = newRow.Duration;
        if (duration1.HasValue)
        {
          duration1 = row.Duration;
          int? duration2 = newRow.Duration;
          if (!(duration1.GetValueOrDefault() == duration2.GetValueOrDefault() & duration1.HasValue == duration2.HasValue))
          {
            PXResourceScheduleAttribute.PXScheduleState pxScheduleState = newRow;
            dateTime1 = newRow.StartDate.Value;
            System.DateTime? nullable2 = new System.DateTime?(dateTime1.AddMinutes((double) newRow.Duration.Value));
            pxScheduleState.EndDate = nullable2;
          }
        }
      }
    }
    System.DateTime? nullable3 = newRow.EndDate;
    System.DateTime dateTime2 = nullable3.Value;
    nullable3 = newRow.StartDate;
    System.DateTime dateTime3 = nullable3.Value;
    if (!(dateTime2 < dateTime3))
      return;
    nullable3 = row.EndDate;
    System.DateTime dateTime4 = nullable3.Value;
    nullable3 = newRow.StartDate;
    System.DateTime dateTime5 = nullable3.Value;
    if (dateTime4 < dateTime5)
    {
      PXResourceScheduleAttribute.PXScheduleState pxScheduleState = newRow;
      nullable3 = newRow.StartDate;
      dateTime1 = nullable3.Value;
      ref System.DateTime local = ref dateTime1;
      nullable3 = row.EndDate;
      long ticks1 = nullable3.Value.Ticks;
      nullable3 = row.StartDate;
      long ticks2 = nullable3.Value.Ticks;
      long num = ticks1 - ticks2;
      System.DateTime? nullable4 = new System.DateTime?(local.AddTicks(num));
      pxScheduleState.EndDate = nullable4;
    }
    else
      newRow.EndDate = row.EndDate;
  }

  private void AddActions(PXGraph graph)
  {
    this.AddAction(graph, this._definition.AddItemAction, new PXButtonDelegate(this.AddRecord));
    this.AddAction(graph, this._definition.DeleteItemAction, new PXButtonDelegate(this.DeleteRecord));
    this.AddAction(graph, this._definition.DetailsItemAction, new PXButtonDelegate(this.ShowDetails));
    this.AddAction(graph, this._definition.PreviousAction, new PXButtonDelegate(this.Previous));
    this.AddAction(graph, this._definition.NextAction, new PXButtonDelegate(this.Next));
    this.AddAction(graph, this._definition.NavigateAction, new PXButtonDelegate(this.Navigate));
    this.AddAction(graph, this._definition.SearchAction, new PXButtonDelegate(this.Search));
    this.AddAction(graph, this._definition.SearchNextAction, new PXButtonDelegate(this.SearchNext));
    this.AddAction(graph, this._definition.SearchPreviousAction, new PXButtonDelegate(this.SearchPrevious));
    this.AddAction(graph, this._definition.UpdateResourceAction, new PXButtonDelegate(this.UpdateResource));
  }

  private void AddAction(PXGraph graph, string name, PXButtonDelegate handler)
  {
    PXAction instance = (PXAction) Activator.CreateInstance(PXResourceScheduleAttribute.MakeGenericType(typeof (PXNamedAction<>), this._primaryTable), (object) graph, (object) name, (object) handler);
    instance.SetVisible(false);
    graph.Actions.Add((object) name, (object) instance);
  }

  private static void ShiftDateRegion(PXResourceScheduleAttribute.PXScheduleState row, bool forward)
  {
    PXResourceScheduleAttribute.EnsureDateRegion(row);
    System.DateTime dateTime1 = row.StartDate.Value;
    System.DateTime dateTime2 = row.EndDate.Value;
    if (forward)
    {
      System.DateTime dateTime3;
      if ((dateTime3 = dateTime1.AddMonths(1)) == dateTime2)
      {
        row.StartDate = new System.DateTime?(dateTime3);
        row.EndDate = new System.DateTime?(dateTime3.AddMonths(1));
      }
      else
      {
        System.DateTime dateTime4;
        if ((dateTime4 = dateTime1.AddDays(1.0)) == dateTime2)
        {
          row.StartDate = new System.DateTime?(dateTime4);
          row.EndDate = new System.DateTime?(dateTime4.AddDays(1.0));
        }
        else
        {
          long num = dateTime2.Ticks - dateTime1.Ticks;
          row.StartDate = new System.DateTime?(dateTime1.AddTicks(num));
          row.EndDate = new System.DateTime?(dateTime2.AddTicks(num));
        }
      }
    }
    else
    {
      System.DateTime dateTime5;
      if ((dateTime5 = dateTime2.AddMonths(-1)) == dateTime1)
      {
        row.StartDate = new System.DateTime?(dateTime5.AddMonths(-1));
        row.EndDate = new System.DateTime?(dateTime5);
      }
      else
      {
        System.DateTime dateTime6;
        if ((dateTime6 = dateTime2.AddDays(-1.0)) == dateTime1)
        {
          row.StartDate = new System.DateTime?(dateTime6.AddDays(-1.0));
          row.EndDate = new System.DateTime?(dateTime6);
        }
        else
        {
          long num = dateTime1.Ticks - dateTime2.Ticks;
          row.StartDate = new System.DateTime?(dateTime1.AddTicks(num));
          row.EndDate = new System.DateTime?(dateTime2.AddTicks(num));
        }
      }
    }
  }

  private static void EnsureDateRegion(PXResourceScheduleAttribute.PXScheduleState row)
  {
    System.DateTime dateTime1;
    if (!row.StartDate.HasValue)
    {
      PXResourceScheduleAttribute.PXScheduleState pxScheduleState = row;
      System.DateTime dateTime2;
      if (!row.TargetStartDate.HasValue)
      {
        dateTime2 = System.DateTime.Today;
      }
      else
      {
        dateTime1 = row.TargetStartDate.Value;
        dateTime2 = dateTime1.Date;
      }
      System.DateTime? nullable = new System.DateTime?(dateTime2);
      pxScheduleState.StartDate = nullable;
    }
    if (row.EndDate.HasValue)
      return;
    PXResourceScheduleAttribute.PXScheduleState pxScheduleState1 = row;
    dateTime1 = row.StartDate.Value;
    System.DateTime? nullable1 = new System.DateTime?(dateTime1.AddDays(1.0));
    pxScheduleState1.EndDate = nullable1;
  }

  private static System.Type MakeGenericType(params System.Type[] types)
  {
    int index = 0;
    return PXResourceScheduleAttribute.MakeGenericType(types, ref index);
  }

  private static System.Type MakeGenericType(System.Type[] types, ref int index)
  {
    if (types == null)
      throw new ArgumentNullException(nameof (types));
    if (types.Length == 0)
      throw new ArgumentException("The types list is empty.");
    if (index >= types.Length)
      throw new ArgumentOutOfRangeException(nameof (types), "The types list is not correct.");
    System.Type type = types[index];
    ++index;
    if (!type.IsGenericTypeDefinition)
      return type;
    System.Type[] typeArray = new System.Type[type.GetGenericArguments().Length];
    for (int index1 = 0; index1 < typeArray.Length; ++index1)
      typeArray[index1] = PXResourceScheduleAttribute.MakeGenericType(types, ref index);
    return type.MakeGenericType(typeArray);
  }

  /// <exclude />
  [Serializable]
  public class PXScheduleState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Key;
    protected object _TargetTable;
    protected string _MainView;
    protected System.DateTime? _TargetStartDate;
    protected System.DateTime? _TargetEndDate;
    protected object _SelectedItem;
    protected System.DateTime? _StartDate;
    protected int? _Duration;
    protected System.DateTime? _EndDate;
    protected int? _RegionType;
    protected bool? _AllowUpdate;

    [PXString(IsKey = true)]
    [PXUIField(Visible = false)]
    public string Key
    {
      get => this._Key;
      set => this._Key = value;
    }

    [PXVariant]
    [PXUIField(Visible = false)]
    public object TargetTable
    {
      get => this._TargetTable;
      set => this._TargetTable = value;
    }

    [PXString]
    [PXUIField(Visible = false)]
    public virtual string MainView
    {
      get => this._MainView;
      set => this._MainView = value;
    }

    [PXDate]
    [PXUIField(Visible = false)]
    public System.DateTime? TargetStartDate
    {
      get => this._TargetStartDate;
      set => this._TargetStartDate = value;
    }

    [PXDate]
    [PXUIField(Visible = false)]
    public System.DateTime? TargetEndDate
    {
      get => this._TargetEndDate;
      set => this._TargetEndDate = value;
    }

    [PXVariant]
    [PXUIField(Visible = false)]
    public object SelectedItem
    {
      get => this._SelectedItem;
      set => this._SelectedItem = value;
    }

    [PXDateAndTime]
    [PXUIField(DisplayName = "Start Date")]
    public System.DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXTimeSpanLong]
    [PXUIField(DisplayName = "Duration")]
    public int? Duration
    {
      get => this._Duration;
      set => this._Duration = value;
    }

    [PXDateAndTime]
    [PXUIField(DisplayName = "End Date")]
    public System.DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXInt]
    [PXUIField(Visible = false)]
    public int? RegionType
    {
      get => this._RegionType;
      set => this._RegionType = value;
    }

    [PXBool]
    [PXUIField(Visible = false)]
    public bool? AllowUpdate
    {
      get => this._AllowUpdate;
      set => this._AllowUpdate = value;
    }

    /// <exclude />
    public abstract class key : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.key>
    {
    }

    /// <exclude />
    public abstract class targetTable : IBqlField, IBqlOperand
    {
    }

    /// <exclude />
    public abstract class mainView : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.mainView>
    {
    }

    /// <exclude />
    public abstract class targetStartDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.targetStartDate>
    {
    }

    /// <exclude />
    public abstract class targetEndDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.targetEndDate>
    {
    }

    /// <exclude />
    public abstract class selectedItem : IBqlField, IBqlOperand
    {
    }

    /// <exclude />
    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.startDate>
    {
    }

    /// <exclude />
    public abstract class duration : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.duration>
    {
    }

    /// <exclude />
    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.endDate>
    {
    }

    /// <exclude />
    public abstract class regionType : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.regionType>
    {
    }

    /// <exclude />
    public abstract class allowUpdate : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXResourceScheduleAttribute.PXScheduleState.allowUpdate>
    {
    }
  }

  /// <exclude />
  [Serializable]
  public class DateTimeRestriction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(PreserveTime = true)]
    public System.DateTime? StartEdge
    {
      get => new System.DateTime?();
      set
      {
      }
    }

    [PXDBDate(PreserveTime = true)]
    public System.DateTime? EndEdge
    {
      get => new System.DateTime?();
      set
      {
      }
    }

    /// <exclude />
    public abstract class startEdge : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.DateTimeRestriction.startEdge>
    {
    }

    /// <exclude />
    public abstract class endEdge : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PXResourceScheduleAttribute.DateTimeRestriction.endEdge>
    {
    }
  }

  /// <exclude />
  private class PXRestrictedSelectView<Table, StartField, EndField> : PXView
    where Table : class, IBqlTable
    where StartField : IBqlField
    where EndField : IBqlField
  {
    private string _startFieldName;
    private string _endFieldName;
    private string _stateViewName;
    private string _viewName;
    private PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler[] _additionalDataHandlers;

    private PXRestrictedSelectView(PXView baseView, BqlCommand select)
      : base(baseView.Graph, (baseView.IsReadOnly ? 1 : 0) != 0, select.OrderByNew(PXResourceScheduleAttribute.MakeGenericType(typeof (OrderBy<>), typeof (Asc<,>), typeof (StartField), typeof (Asc<>), typeof (EndField))))
    {
      this.Attributes = baseView.Attributes;
    }

    private PXRestrictedSelectView(PXView baseView, BqlCommand select, Delegate handler)
      : base(baseView.Graph, baseView.IsReadOnly, select, handler)
    {
    }

    public static PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField> Create(
      PXView baseView,
      string stateViewName,
      string viewName)
    {
      System.Type[] tables = baseView.BqlSelect.GetTables();
      int num = tables != null && tables.Length >= 1 ? Array.IndexOf<System.Type>(tables, typeof (Table)) : throw new ArgumentException("Wrong bqlSelect of specified view", nameof (baseView));
      if (num < 0)
        throw new ArgumentException("BqlSelect of specified view doesn't contain " + typeof (Table).FullName, nameof (baseView));
      System.Type type = typeof (Where<StartField, GreaterEqual<Optional<PXResourceScheduleAttribute.DateTimeRestriction.startEdge>>, And<StartField, LessEqual<Optional<PXResourceScheduleAttribute.DateTimeRestriction.endEdge>>, PX.Data.Or<Where<StartField, Less<Optional<PXResourceScheduleAttribute.DateTimeRestriction.startEdge>>, And<EndField, Greater<Optional<PXResourceScheduleAttribute.DateTimeRestriction.startEdge>>>>>>>);
      BqlCommand select = num == 0 ? baseView.BqlSelect.WhereAnd(type) : PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField>.AddJoinConditions(baseView.BqlSelect, typeof (Table), type);
      PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField> restrictedSelectView = (object) baseView.BqlDelegate == null ? new PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField>(baseView, select) : new PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField>(baseView, select, baseView.BqlDelegate);
      PXCache cach = baseView.Graph.Caches[typeof (Table)];
      restrictedSelectView._startFieldName = cach.GetField(typeof (StartField));
      restrictedSelectView._endFieldName = cach.GetField(typeof (EndField));
      restrictedSelectView._stateViewName = stateViewName;
      restrictedSelectView._viewName = viewName;
      return restrictedSelectView;
    }

    public override List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      object[] objArray = this.PrepareParameters(currents, parameters);
      PXResourceScheduleAttribute.PXScheduleState scheduleState = PXResourceScheduleAttribute.GetScheduleState(this._Graph.Views[this._stateViewName]);
      if (scheduleState != null)
      {
        IBqlParameter[] parameters1 = this.BqlSelect.GetParameters();
        for (int index = 0; index < parameters1.Length; ++index)
        {
          System.Type referencedType = parameters1[index].GetReferencedType();
          if (referencedType == typeof (PXResourceScheduleAttribute.DateTimeRestriction.startEdge))
            objArray[index] = (object) scheduleState.StartDate;
          if (referencedType == typeof (PXResourceScheduleAttribute.DateTimeRestriction.endEdge))
            objArray[index] = (object) scheduleState.EndDate;
        }
      }
      return base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }

    protected override List<object> GetResult(
      object[] parameters,
      PXFilterRow[] filters,
      bool reverseOrder,
      int topCount,
      PXView.PXSearchColumn[] sorts,
      ref bool overrideSort,
      ref bool extFilter)
    {
      List<object> result1 = base.GetResult(parameters, filters, reverseOrder, topCount, sorts, ref overrideSort, ref extFilter);
      PXResourceScheduleAttribute.PXScheduleState scheduleState = PXResourceScheduleAttribute.GetScheduleState(this._Graph.Views[this._stateViewName]);
      System.DateTime? nullable1;
      if (scheduleState != null)
      {
        nullable1 = scheduleState.StartDate;
        if (!nullable1.HasValue)
        {
          nullable1 = scheduleState.EndDate;
          if (!nullable1.HasValue)
            goto label_3;
        }
        result1.AddRange(this.GeAdditionalResult(scheduleState.StartDate, scheduleState.EndDate));
        PXCache cach = this._Graph.Caches[typeof (Table)];
        List<object> result2 = new List<object>();
        nullable1 = scheduleState.StartDate;
        System.DateTime dateTime1 = nullable1.Value;
        nullable1 = scheduleState.EndDate;
        System.DateTime dateTime2 = nullable1.Value;
        if (dateTime1 <= dateTime2)
        {
          foreach (object obj in result1)
          {
            switch (obj)
            {
              case PXResult _:
                pattern_0 = ((PXResult) obj)[typeof (Table)] as Table;
                break;
            }
            System.DateTime? nullable2 = (System.DateTime?) cach.GetValue((object) pattern_0, this._startFieldName);
            System.DateTime? nullable3 = (System.DateTime?) cach.GetValue((object) pattern_0, this._endFieldName);
            bool hasValue = nullable2.HasValue;
            nullable1 = scheduleState.StartDate;
            if (nullable1.HasValue)
            {
              nullable1 = nullable2;
              System.DateTime? nullable4 = scheduleState.StartDate;
              int num1;
              if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                nullable4 = nullable3;
                nullable1 = scheduleState.StartDate;
                num1 = nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0;
              }
              else
                num1 = 0;
              bool flag1 = num1 != 0;
              nullable1 = scheduleState.EndDate;
              bool flag2;
              if (nullable1.HasValue)
              {
                int num2 = flag1 ? 1 : 0;
                nullable1 = nullable2;
                nullable4 = scheduleState.StartDate;
                int num3;
                if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                {
                  nullable4 = nullable2;
                  nullable1 = scheduleState.EndDate;
                  num3 = nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0;
                }
                else
                  num3 = 0;
                flag2 = (num2 | num3) != 0;
              }
              else
              {
                int num4 = flag1 ? 1 : 0;
                nullable1 = nullable2;
                nullable4 = scheduleState.StartDate;
                int num5;
                if ((nullable1.HasValue == nullable4.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
                {
                  nullable4 = nullable3;
                  nullable1 = scheduleState.StartDate;
                  num5 = nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0;
                }
                else
                  num5 = 1;
                flag2 = (num4 | num5) != 0;
              }
              hasValue &= flag2;
            }
            if (hasValue | PXResourceScheduleAttribute.PXRestrictedSelectView<Table, StartField, EndField>.AreKeysNulls(cach, pattern_0))
              result2.Add(obj);
          }
        }
        return result2;
      }
label_3:
      List<object> objectList = result1;
      nullable1 = new System.DateTime?();
      System.DateTime? start = nullable1;
      nullable1 = new System.DateTime?();
      System.DateTime? end = nullable1;
      IEnumerable<object> collection = this.GeAdditionalResult(start, end);
      objectList.AddRange(collection);
      return result1;
    }

    private IEnumerable<object> GeAdditionalResult(System.DateTime? start, System.DateTime? end)
    {
      PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler[] getRecordsHandlerArray = this.AdditionalDataHandlers;
      for (int index = 0; index < getRecordsHandlerArray.Length; ++index)
      {
        foreach (object obj in getRecordsHandlerArray[index](start, end))
          yield return obj;
      }
      getRecordsHandlerArray = (PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler[]) null;
    }

    private PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler[] AdditionalDataHandlers
    {
      get
      {
        return this._additionalDataHandlers ?? (this._additionalDataHandlers = PXResourceScheduleAttribute.HandlerAttribute.GetAdditionalRecordsHandlers(this._Graph, this._viewName));
      }
    }

    private static BqlCommand AddJoinConditions(BqlCommand command, System.Type joinTable, System.Type condition)
    {
      return Activator.CreateInstance(BqlCommand.AddJoinConditions(command.GetType(), joinTable, condition)) as BqlCommand;
    }

    private static bool AreKeysNulls(PXCache cache, Table item)
    {
      foreach (string key in (IEnumerable<string>) cache.Keys)
      {
        if (cache.GetValue((object) item, key) != null)
          return false;
      }
      return true;
    }
  }

  /// <exclude />
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class HandlerAttribute : Attribute
  {
    private static readonly PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler _addRecordEmptyHandler = new PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler(PXResourceScheduleAttribute.HandlerAttribute.AddRecord);
    private static readonly PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler _removeRecordEmptyHandler = new PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler(PXResourceScheduleAttribute.HandlerAttribute.RemoveRecord);
    private static readonly PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler _viewDetailsEmptyHandler = new PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler(PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsRecord);
    private static readonly PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler GetWorkStartEmptyHandler = new PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler(PXResourceScheduleAttribute.HandlerAttribute.GetStartWorkTime);
    private static readonly PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler GetWorkEndEmptyHandler = new PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler(PXResourceScheduleAttribute.HandlerAttribute.GetEndWorkTime);
    public readonly string ViewName;
    public readonly PXResourceScheduleAttribute.HandlerAttribute.Types Type;

    public HandlerAttribute(
      string viewName,
      PXResourceScheduleAttribute.HandlerAttribute.Types type)
    {
      this.ViewName = viewName;
      this.Type = type;
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler GetAddRecordHandlers(
      PXGraph graph,
      string viewName)
    {
      PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler[] handlers = PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.AddRecordHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.AddRecord, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => returnType == typeof (void) && parameters.Length == 0));
      return handlers.Length == 0 ? PXResourceScheduleAttribute.HandlerAttribute._addRecordEmptyHandler : handlers[0];
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler GetRemoveRecordHandlers(
      PXGraph graph,
      string viewName)
    {
      PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler[] handlers = PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.RemoveRecordHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.DeleteRecord, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => returnType == typeof (void) && parameters.Length == 1 && parameters[0].ParameterType == typeof (object)));
      return handlers.Length == 0 ? PXResourceScheduleAttribute.HandlerAttribute._removeRecordEmptyHandler : handlers[0];
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler GetViewDetailsHandlers(
      PXGraph graph,
      string viewName)
    {
      PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler[] handlers = PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.ViewDetailsHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.ViewDetails, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => returnType == typeof (void) && parameters.Length == 1 && parameters[0].ParameterType == typeof (object)));
      return handlers.Length == 0 ? PXResourceScheduleAttribute.HandlerAttribute._viewDetailsEmptyHandler : handlers[0];
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler[] GetAdditionalRecordsHandlers(
      PXGraph graph,
      string viewName)
    {
      return PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.GetRecordsHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.GetAdditionalRecords, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => typeof (IEnumerable).IsAssignableFrom(returnType) && parameters.Length == 2 && parameters[0].ParameterType == typeof (System.DateTime?) && parameters[1].ParameterType == typeof (System.DateTime?)));
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler GetGetWorkStartTimeHandlers(
      PXGraph graph,
      string viewName)
    {
      PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler[] handlers = PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartTimeHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.GetWorkStartTime, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => returnType == typeof (System.DateTime?) && parameters.Length == 2 && parameters[0].ParameterType == typeof (object) && parameters[1].ParameterType == typeof (System.DateTime)));
      return handlers.Length == 0 ? PXResourceScheduleAttribute.HandlerAttribute.GetWorkStartEmptyHandler : handlers[0];
    }

    public static PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler GetGetWorkEndTimeHandlers(
      PXGraph graph,
      string viewName)
    {
      PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler[] handlers = PXResourceScheduleAttribute.HandlerAttribute.GetHandlers<PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndTimeHandler>(graph, viewName, PXResourceScheduleAttribute.HandlerAttribute.Types.GetWorkEndTime, (PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler) ((returnType, parameters) => returnType == typeof (System.DateTime?) && parameters.Length == 2 && parameters[0].ParameterType == typeof (object) && parameters[1].ParameterType == typeof (System.DateTime)));
      return handlers.Length == 0 ? PXResourceScheduleAttribute.HandlerAttribute.GetWorkEndEmptyHandler : handlers[0];
    }

    private static THandler[] GetHandlers<THandler>(
      PXGraph graph,
      string viewName,
      PXResourceScheduleAttribute.HandlerAttribute.Types type,
      PXResourceScheduleAttribute.HandlerAttribute.IsCorrectHandler criteria)
      where THandler : class
    {
      if (graph == null)
        throw new ArgumentNullException(nameof (graph));
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      List<THandler> handlerList = new List<THandler>();
      foreach (MethodInfo method in graph.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod))
      {
        if (Attribute.GetCustomAttribute((MemberInfo) method, typeof (PXResourceScheduleAttribute.HandlerAttribute), true) is PXResourceScheduleAttribute.HandlerAttribute customAttribute && customAttribute.Type == type && string.Compare(customAttribute.ViewName, viewName, true) == 0 && criteria(method.ReturnType, method.GetParameters()))
          handlerList.Add(Delegate.CreateDelegate(typeof (THandler), (object) graph, method.Name) as THandler);
      }
      return handlerList.ToArray();
    }

    private static void AddRecord()
    {
    }

    private static void RemoveRecord(object item)
    {
    }

    private static void ViewDetailsRecord(object item)
    {
    }

    private static System.DateTime? GetStartWorkTime(object item, System.DateTime date)
    {
      return new System.DateTime?();
    }

    private static System.DateTime? GetEndWorkTime(object item, System.DateTime date)
    {
      return new System.DateTime?();
    }

    /// <exclude />
    public delegate void AddRecordHandler();

    /// <exclude />
    public delegate void RemoveRecordHandler(object item);

    /// <exclude />
    public delegate void ViewDetailsHandler(object item);

    /// <exclude />
    public delegate IEnumerable GetRecordsHandler(System.DateTime? start, System.DateTime? end);

    /// <exclude />
    public delegate System.DateTime? GetWorkStartTimeHandler(object item, System.DateTime date);

    /// <exclude />
    public delegate System.DateTime? GetWorkEndTimeHandler(object item, System.DateTime date);

    /// <exclude />
    private delegate bool IsCorrectHandler(System.Type returnType, ParameterInfo[] paramters);

    /// <exclude />
    public enum Types
    {
      AddRecord,
      DeleteRecord,
      GetAdditionalRecords,
      ViewDetails,
      GetWorkStartTime,
      GetWorkEndTime,
    }
  }

  /// <exclude />
  public class Definition
  {
    public const string SIMPLE_CONFIRM = "simple";
    public const string PANEL_CONFIRM = "panel";
    public static readonly PXResourceScheduleAttribute.Definition Empty;
    private static readonly Dictionary<string, PXResourceScheduleAttribute.Definition> _collection = new Dictionary<string, PXResourceScheduleAttribute.Definition>();
    private static readonly object _syncObj = new object();
    private string _addItemAction;
    private string _deleteItemAction;
    private string _detailsItemAction;
    private string _previousAction;
    private string _nextAction;
    private string _navigateAction;
    private string _searchAction;
    private string _searchNextAction;
    private string _searchPreviousAction;
    private string _updateResourceAction;
    private string _resourceStartDateField;
    private string _resourceEndDateField;
    private string _resourceDurationField;
    private string _startDateField;
    private string _endDateField;
    private string _durationField;
    private string _simpleDurationField;
    private string _descriptionField;
    private string _resourceDescriptionField;
    private readonly string _stateViewName;
    private readonly System.Type _resourceTable;
    private readonly string _viewName;

    static Definition()
    {
      PXResourceScheduleAttribute.Definition.Empty = (PXResourceScheduleAttribute.Definition) new PXResourceScheduleAttribute.Definition.DefinitionEmpty();
    }

    protected Definition()
    {
    }

    public Definition(string stateViewName, System.Type resourceTable, string viewName)
    {
      this._stateViewName = stateViewName;
      this._resourceTable = resourceTable;
      this._viewName = viewName;
    }

    public string ResourceStartDateField
    {
      get => this._resourceStartDateField;
      internal set => this._resourceStartDateField = value;
    }

    public string ResourceEndDateField
    {
      get => this._resourceEndDateField;
      internal set => this._resourceEndDateField = value;
    }

    public string ResourceDurationField
    {
      get => this._resourceDurationField;
      internal set => this._resourceDurationField = value;
    }

    public string StartDateField
    {
      get => this._startDateField;
      internal set => this._startDateField = value;
    }

    public string EndDateField
    {
      get => this._endDateField;
      internal set => this._endDateField = value;
    }

    public string DurationField
    {
      get => this._durationField;
      internal set => this._durationField = value;
    }

    public string SimpleDurationField
    {
      get => this._simpleDurationField;
      internal set => this._simpleDurationField = value;
    }

    public string AddItemAction
    {
      get => this._addItemAction;
      internal set => this._addItemAction = value;
    }

    public string DeleteItemAction
    {
      get => this._deleteItemAction;
      internal set => this._deleteItemAction = value;
    }

    public string DetailsItemAction
    {
      get => this._detailsItemAction;
      internal set => this._detailsItemAction = value;
    }

    public string PreviousAction
    {
      get => this._previousAction;
      internal set => this._previousAction = value;
    }

    public string NextAction
    {
      get => this._nextAction;
      internal set => this._nextAction = value;
    }

    public string NavigateAction
    {
      get => this._navigateAction;
      internal set => this._navigateAction = value;
    }

    public string SearchAction
    {
      get => this._searchAction;
      internal set => this._searchAction = value;
    }

    public string SearchNextAction
    {
      get => this._searchNextAction;
      internal set => this._searchNextAction = value;
    }

    public string SearchPreviousAction
    {
      get => this._searchPreviousAction;
      internal set => this._searchPreviousAction = value;
    }

    public string UpdateResourceAction
    {
      get => this._updateResourceAction;
      set => this._updateResourceAction = value;
    }

    public string DescriptionField
    {
      get => this._descriptionField;
      internal set => this._descriptionField = value;
    }

    public string ResourceDescriptionField
    {
      get => this._resourceDescriptionField;
      internal set => this._resourceDescriptionField = value;
    }

    public string StateView => this._stateViewName;

    internal string ViewName => this._viewName;

    public static PXResourceScheduleAttribute.Definition GetDefinition(
      System.Type graphType,
      string dataMember)
    {
      lock (PXResourceScheduleAttribute.Definition._syncObj)
      {
        string key = PXResourceScheduleAttribute.GenerateKey(graphType, dataMember);
        return PXResourceScheduleAttribute.Definition._collection.ContainsKey(key) ? PXResourceScheduleAttribute.Definition._collection[key] : PXResourceScheduleAttribute.Definition.Empty;
      }
    }

    internal static void SaveDefinition(
      PXGraph graph,
      PXResourceScheduleAttribute.Definition definition)
    {
      lock (PXResourceScheduleAttribute.Definition._syncObj)
      {
        string key = PXResourceScheduleAttribute.GenerateKey(graph.GetType(), definition.ViewName);
        if (PXResourceScheduleAttribute.Definition._collection.ContainsKey(key))
          return;
        PXResourceScheduleAttribute.Definition._collection.Add(key, definition);
      }
    }

    public bool AllowUpdate(PXGraph graph)
    {
      bool? allowUpdate = this.CurrentState(graph).AllowUpdate;
      bool flag = false;
      return !(allowUpdate.GetValueOrDefault() == flag & allowUpdate.HasValue);
    }

    public System.DateTime? GetStartDate(PXGraph graph) => this.CurrentState(graph).StartDate;

    public System.DateTime? GetEndDate(PXGraph graph) => this.CurrentState(graph).EndDate;

    public System.DateTime? GetWorkStartTime(PXGraph graph, string[] itemKeys, System.DateTime date)
    {
      return PXResourceScheduleAttribute.GetStartWorkTime(graph, this._viewName, this.GetFromKeys(graph, itemKeys), date);
    }

    public System.DateTime? GetWorkEndTime(PXGraph graph, string[] itemKeys, System.DateTime date)
    {
      return PXResourceScheduleAttribute.GetEndWorkTime(graph, this._viewName, this.GetFromKeys(graph, itemKeys), date);
    }

    public System.DateTime? GetTargetStartDate(PXGraph graph)
    {
      return this.CurrentState(graph).TargetStartDate;
    }

    public System.DateTime? GetTargetEndDate(PXGraph graph)
    {
      return this.CurrentState(graph).TargetEndDate;
    }

    public int? GetRegionType(PXGraph graph) => this.CurrentState(graph).RegionType;

    public virtual string[] GetKeys(PXGraph graph, object data)
    {
      PXCache cache = graph.Views[this.ViewName].Cache;
      object obj = PXResourceScheduleAttribute.ExtractItem(data, cache.GetItemType());
      return obj == null ? (string[]) null : this.GetItemKeys(cache, obj);
    }

    public virtual object GetFromKeys(PXGraph graph, string[] keys)
    {
      return this.GetItemFromKeys(graph.Views[this.ViewName].Cache, keys);
    }

    public virtual string GetDescription(PXGraph graph, object data)
    {
      PXCache cache = graph.Views[this.ViewName].Cache;
      object data1 = PXResourceScheduleAttribute.ExtractItem(data, cache.GetItemType());
      return data1 == null ? (string) null : Convert.ToString(cache.GetValue(data1, this.DescriptionField));
    }

    public virtual bool EqualKeys(PXGraph graph, object data, string[] keys)
    {
      return PXResourceScheduleAttribute.Definition.EqualKeys(keys, this.GetKeys(graph, data));
    }

    public virtual string[] GetResKeys(PXGraph graph, object data)
    {
      object obj = PXResourceScheduleAttribute.ExtractItem(data, this._resourceTable);
      return obj == null ? (string[]) null : this.GetItemKeys(graph.Caches[this._resourceTable], obj);
    }

    public virtual object GetResFromKeys(PXGraph graph, string[] keys)
    {
      return this.GetItemFromKeys(graph.Caches[this._resourceTable], keys);
    }

    protected object GetItemFromKeys(PXCache cache, string[] keys)
    {
      if (cache.Keys.Count != keys.Length)
        throw new ApplicationException("Keys are incorrect.");
      object instance = cache.CreateInstance();
      IEnumerator enumerator = keys.GetEnumerator();
      foreach (string key in (IEnumerable<string>) cache.Keys)
      {
        enumerator.MoveNext();
        cache.SetValueExt(instance, key, enumerator.Current);
      }
      return cache.Locate(instance);
    }

    public virtual string GetResDescription(PXGraph graph, object data)
    {
      if (string.IsNullOrEmpty(this._resourceDescriptionField))
        return (string) null;
      object data1 = PXResourceScheduleAttribute.ExtractItem(data, this._resourceTable);
      return data1 == null ? (string) null : Convert.ToString(graph.Caches[this._resourceTable].GetValue(data1, this._resourceDescriptionField));
    }

    public virtual object GetResValue(PXGraph graph, object data, string fieldName)
    {
      object data1 = PXResourceScheduleAttribute.ExtractItem(data, this._resourceTable);
      return data1 == null ? (object) null : graph.Caches[this._resourceTable].GetValue(data1, fieldName);
    }

    public virtual void SetCurrentResource(PXGraph graph, string[] keys)
    {
      this.SetSelectedItem(graph.Caches[this._resourceTable], keys);
    }

    public virtual void SetCurrent(PXGraph graph, string[] keys)
    {
      this.SetSelectedItem(graph.Views[this.ViewName].Cache, keys);
    }

    public virtual void SetTargetStartEndDates(PXGraph graph, System.DateTime startDate, System.DateTime endDate)
    {
      PXResourceScheduleAttribute.SetTargetStartEndDates(graph, this._stateViewName, startDate, endDate);
    }

    public virtual void SetStartEndDates(PXGraph graph, System.DateTime startDate, System.DateTime endDate)
    {
      PXResourceScheduleAttribute.SetStartEndDates(graph, this._stateViewName, startDate, endDate);
    }

    public virtual void SetRegionType(PXGraph graph, int type)
    {
      this.CurrentState(graph).RegionType = new int?(type);
    }

    public virtual IEnumerable Select(PXGraph graph)
    {
      return PXResourceScheduleAttribute.Select(graph, this.ViewName, this._resourceTable.Name, this.StartDateField, this.EndDateField);
    }

    protected PXResourceScheduleAttribute.PXScheduleState CurrentState(PXGraph graph)
    {
      return PXResourceScheduleAttribute.CurrentState(graph, this._stateViewName);
    }

    protected static bool EqualKeys(string[] keys, string[] otherKeys)
    {
      if (otherKeys == null && keys == null)
        return true;
      for (int index = 0; index < otherKeys.Length; ++index)
      {
        if (string.Compare(otherKeys[index], keys[index], true) != 0)
          return false;
      }
      return true;
    }

    protected string[] GetItemKeys(PXCache cache, object item)
    {
      string[] itemKeys = new string[cache.Keys.Count];
      for (int index = 0; index < itemKeys.Length; ++index)
        itemKeys[index] = Convert.ToString(cache.GetValue(item, cache.Keys[index]));
      return itemKeys;
    }

    private void SetSelectedItem(PXCache cache, string[] keys)
    {
      System.Type itemType = cache.GetItemType();
      foreach (object data in this.Select(cache.Graph))
      {
        object obj = PXResourceScheduleAttribute.ExtractItem(data, itemType);
        if (PXResourceScheduleAttribute.Definition.EqualKeys(keys, this.GetItemKeys(cache, obj)))
        {
          this.CurrentState(cache.Graph).SelectedItem = obj;
          break;
        }
      }
    }

    /// <exclude />
    private sealed class DefinitionEmpty : PXResourceScheduleAttribute.Definition
    {
      public override string[] GetKeys(PXGraph graph, object data) => (string[]) null;

      public override string GetDescription(PXGraph graph, object data) => (string) null;

      public override bool EqualKeys(PXGraph graph, object data, string[] keys) => false;

      public override string[] GetResKeys(PXGraph graph, object data) => (string[]) null;

      public override string GetResDescription(PXGraph graph, object data) => (string) null;

      public override object GetResValue(PXGraph graph, object data, string fieldName)
      {
        return (object) null;
      }
    }
  }

  /// <exclude />
  private sealed class LongDescComparer : IComparer<long>
  {
    public int Compare(long x, long y) => -1 * x.CompareTo(y);
  }
}
