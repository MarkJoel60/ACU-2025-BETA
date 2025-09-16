// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Async;
using PX.Common;
using PX.Data.Automation;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

/// <summary>A base class for processing data views.</summary>
/// <typeparam name="Table">A DAC.</typeparam>
public abstract class PXProcessingBase<Table> : 
  PXSelectBase<
  #nullable disable
  Table>,
  IPXProcessing,
  IPXProcessingWithCustomDelegate,
  IPXProcessingWithOuterViewCache
  where Table : class, IBqlTable, new()
{
  protected PXProcessingBase<Table>.ParametersDelegate _ParametersDelegate;
  protected object[] _Parameters;
  protected PXFilterRow[] _Filters;
  private PXFilterRow[] _AdditionalFilters;
  protected bool _SelectFromUI;
  protected PXView _OuterView;
  protected internal string _SelectedField = "selected";
  protected string _NoteIDField = "NoteID";
  protected PXResultset<Table> _InProc;
  protected PXProcessingInfo<Table> _Info;
  protected bool _AutoPersist;
  protected bool _IsInstance;
  protected Dictionary<object, PXProcessingMessage> _SelectedInfo = new Dictionary<object, PXProcessingMessage>();
  /// <summary>The parallel processing options.</summary>
  /// <remarks>
  ///   <para>You should not add multiple delegates that modify
  ///   the <see cref="F:PX.Data.PXProcessingBase`1.ParallelProcessingOptions" /> delegate field and rely on .Net delegates chaining because
  /// the logic of their addition and execution can be random.</para>
  /// </remarks>
  /// <inheritdoc cref="T:PX.Data.PXParallelProcessingOptions" path="/remarks,/example" />
  public System.Action<PXParallelProcessingOptions> ParallelProcessingOptions;
  private PXAction ActionCloseProcessing;
  private PXAction ActionCancelProcessing;
  private PXFilter<PXProcessingBase<Table>.ProcessingResults> ViewProcessingResults;

  [InjectDependency]
  private ILogger Logger { get; set; }

  protected PXProcessingBase()
  {
  }

  protected PXProcessingBase(PXGraph graph)
    : this(graph, (Delegate) null)
  {
  }

  /// <summary>Initializes a new instance of a data view that is bound to
  /// the specified graph and uses the provided method to retrieve
  /// data.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  /// <param name="handler">The delegate of the method that is used to
  /// retrieve the data from the database (or other source).</param>
  protected PXProcessingBase(PXGraph graph, Delegate handler)
  {
    this._Graph = graph;
    this.View = (PXView) new PXProcessingBase<Table>.ParametrizedView(graph, this.GetCommand(), this, new PXSelectDelegate(this._List));
    this.SetOuterViewDelegate(handler);
    this._PrepareGraph<Table>();
  }

  [PXInternalUseOnly]
  [Obsolete("_ProcessDelegate will become private in future versions. Use ProcessDelegate property instead")]
  protected Action<List<Table>, CancellationToken> _ProcessDelegate { get; set; }

  /// <summary>Returns the delegate of the processing method, which is set
  /// by one of the <see cref="!:PXProcessing&lt;T&gt;.SetProcessDelegate()">SetProcessDelegate()</see>
  /// methods.</summary>
  protected Action<List<Table>, CancellationToken> ProcessDelegate
  {
    get
    {
      return this._ProcessDelegate ?? (Action<List<Table>, CancellationToken>) ((list, cancellationToken) =>
      {
        throw new PXInvalidOperationException("The process delegate has not been defined.");
      });
    }
  }

  public virtual bool SuppressMerge { get; set; }

  public virtual bool SuppressUpdate { get; set; }

  public PXCache OuterViewCache => this._OuterView.Cache;

  /// <summary>The delegate of the method that retrieves the
  /// data, which is the optional method of the data view.</summary>
  public virtual Delegate CustomViewDelegate
  {
    get => this._OuterView.BqlDelegate;
    set
    {
      if (!(this._OuterView.BqlDelegate != value))
        return;
      this.SetOuterViewDelegate(value);
    }
  }

  /// <summary>Sets additional filters to the base processing view.</summary>
  /// <param name="addFilters">The collection of additional filters.</param>
  [Obsolete("This method is obsolete and will be removed in a future version of Acumatica ERP.")]
  public virtual void SetAdditionalFilters(PXFilterRow[] addFilters)
  {
    this._AdditionalFilters = addFilters;
  }

  /// <summary>Sets the value that indicates (if set to <see langword="true" />) that the changes in the
  /// graph should be automatically saved in the database before the data
  /// records are processed. By default, the changes are not saved
  /// automatically.</summary>
  /// <param name="autoPersist">The value that indicates whether to save the
  /// changes.</param>
  /// <remarks>The method can be used in the graph constructor and in the RowSelected event handler.</remarks>
  public virtual void SetAutoPersist(bool autoPersist) => this._AutoPersist = autoPersist;

  /// <summary>Sets the DAC field by which the user can mark data records
  /// that should be processed. The method makes available this field and makes unavailable
  /// all other fields.</summary>
  /// <typeparam name="Field">The field type.</typeparam>
  public virtual void SetSelected<Field>() where Field : IBqlField
  {
    this._SelectedField = typeof (Field).Name;
    PXCache cache = this._OuterView.Cache;
    PXUIFieldAttribute.SetEnabled(cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, true);
  }

  /// <summary>Provides conditional running of the processing method. If the method set by <tt>SetParametersDelegate</tt> returns <tt>true</tt>, the system runs the
  /// processing method that is set in <tt>SetProcessDelegate</tt>.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  /// <remarks>
  /// <para>You can use the view's SetParametersDelegate() method if you need to ask for user's input before processing.
  /// You implement the logic inside the parameters delegate, set the processing delegate, and return a Boolean result.</para>
  /// <para>The list of items passed to <see cref="T:PX.Data.PXProcessingBase`1.ParametrizedView" /> is not filtered by external filters
  /// (such as grid column filters).</para>
  /// </remarks>
  /// <example>
  ///   <code title="Example">Items.SetParametersDelegate(delegate(List&lt;Contact&gt; list)
  /// {
  ///     bool result = AskProcess(list);
  /// 
  ///     CampaignMemberMassProcess process = this.Clone();
  ///     Items.SetProcessDelegate(process.Process);
  /// 
  ///     return result;
  /// });</code>
  /// </example>
  public void SetParametersDelegate(PXProcessingBase<Table>.ParametersDelegate handler)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetParametersDelegate(handler))))
      return;
    this._ParametersDelegate = handler;
  }

  /// <summary>Replaces the sorting expression in the underlying BQL
  /// command.</summary>
  public override void OrderByNew<newOrderBy>()
  {
    base.OrderByNew<newOrderBy>();
    this._OuterView.OrderByNew<newOrderBy>();
  }

  /// <summary>Appends the join clause to the underlying BQL
  /// command.</summary>
  public override void Join<join>()
  {
    base.Join<join>();
    this._OuterView.Join<join>();
  }

  protected abstract BqlCommand GetCommand();

  protected void SetOuterViewDelegate(Delegate handler)
  {
    this._OuterView = (object) handler != null ? (PXView) new PXProcessingBase<Table>.OuterView(this.View, this.GetCommand(), handler) : (PXView) new PXProcessingBase<Table>.OuterView(this.View, this.GetCommand());
  }

  protected static void AddAction(PXGraph graph, string name, PXAction handler)
  {
    if (graph.Actions.Contains((object) name))
      graph.Actions[name] = handler;
    else
      graph.Actions.Add((object) name, (object) handler);
  }

  protected virtual void _PrepareGraph<Primary>() where Primary : class, IBqlTable, new()
  {
    PXCache cache = this._OuterView.Cache;
    PXGraph graph = cache.Graph;
    cache.AllowDelete = false;
    cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXNoteAttribute)
      {
        this._NoteIDField = attribute.FieldName;
        break;
      }
    }
    graph.FieldSelecting.AddHandler(typeof (Table), this._SelectedField, new PXFieldSelecting(this.SelectedFieldSelecting));
    this.AttachBaseActions<Primary>();
  }

  protected virtual void AttachBaseActions<Primary>() where Primary : class, IBqlTable, new()
  {
    PXGraph graph = this._OuterView.Cache.Graph;
    this.ActionCloseProcessing = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.actionCloseProcessing));
    PXProcessingBase<Table>.AddAction(graph, "ActionCloseProcessing", this.ActionCloseProcessing);
    this.ActionCancelProcessing = (PXAction) new PXAction<Primary>(graph, (Delegate) new System.Action(this.actionCancelProcessing));
    PXProcessingBase<Table>.AddAction(graph, "ActionCancelProcessing", this.ActionCancelProcessing);
    this.ViewProcessingResults = new PXFilter<PXProcessingBase<Table>.ProcessingResults>(graph, (Delegate) new PXSelectDelegate(this.viewProcessingResults));
    graph.Views["ViewProcessingResults"] = this.ViewProcessingResults.View;
  }

  protected void SelectedFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || this._Graph.IsProcessing || this._Graph.IsMobile)
      return;
    PXProcessingMessage processingMessage;
    this._SelectedInfo.TryGetValue(e.Row, out processingMessage);
    if (processingMessage == null)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    string selectedField = this._SelectedField;
    string fieldName1 = sender.GetFieldName(this._SelectedField, false);
    string str = PXMessages.LocalizeNoPrefix(processingMessage.Message);
    PXErrorLevel errorLevel1 = processingMessage.ErrorLevel;
    bool? isKey = new bool?();
    bool? nullable = new bool?();
    int? required = new int?();
    int? precision = new int?();
    int? length = new int?();
    string fieldName2 = selectedField;
    string displayName = fieldName1;
    string error = str;
    int errorLevel2 = (int) errorLevel1;
    bool? enabled = new bool?();
    bool? visible = new bool?();
    bool? readOnly = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, isKey, nullable, required, precision, length, fieldName: fieldName2, displayName: displayName, error: error, errorLevel: (PXErrorLevel) errorLevel2, enabled: enabled, visible: visible, readOnly: readOnly);
    selectingEventArgs.ReturnState = (object) instance;
  }

  /// <summary>Selects actual records from view.</summary>
  protected virtual IEnumerable _SelectRecords()
  {
    return this._SelectRecords(PXView.StartRow, PXView.MaximumRows);
  }

  protected virtual IEnumerable _SelectRecords(int startRow, int maxRows)
  {
    int totalRows = 0;
    PXView.PXSearchColumn[] array1 = ((IEnumerable<PXView.PXSearchColumn>) (PXView.PXSearchColumn[]) PXView.Sorts).Where<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (s => s.IsExternalSort)).ToArray<PXView.PXSearchColumn>();
    string[] array2 = ((IEnumerable<PXView.PXSearchColumn>) array1).Select<PXView.PXSearchColumn, string>((Func<PXView.PXSearchColumn, string>) (s => s.Column)).ToArray<string>();
    bool[] array3 = ((IEnumerable<PXView.PXSearchColumn>) array1).Select<PXView.PXSearchColumn, bool>((Func<PXView.PXSearchColumn, bool>) (s => s.Descending)).ToArray<bool>();
    if (PXView.RetrieveTotalRowCount)
    {
      int startRow1 = 0;
      totalRows = -1;
      if (this._OuterView.Select((object[]) null, this._Parameters, (object[]) null, array2, array3, this._AlterFilters(this._Filters), ref startRow1, 0, ref totalRows).Count == 0)
      {
        PXResultset<Table> pxResultset = new PXResultset<Table>();
        PXResult<Table> pxResult = new PXResult<Table>(new Table());
        pxResult.RowCount = new int?(totalRows);
        pxResultset.Add(pxResult);
        return (IEnumerable) pxResultset;
      }
    }
    if (maxRows == 0)
    {
      int startRow2 = 0;
      return (IEnumerable) this._OuterView.Select((object[]) null, this._Parameters, (object[]) null, array2, array3, this._AlterFilters(this._Filters), ref startRow2, 0, ref totalRows);
    }
    int startRow3 = startRow;
    PXView.StartRow = 0;
    return (IEnumerable) this._OuterView.Select(PXView.Currents, this._Parameters, ((IEnumerable<PXView.PXSearchColumn>) array1).Select<PXView.PXSearchColumn, object>((Func<PXView.PXSearchColumn, object>) (s => s.OrigSearchValue)).ToArray<object>(), array2, array3, this._AlterFilters(this._Filters), ref startRow3, maxRows, ref totalRows);
  }

  protected virtual IEnumerable _List()
  {
    if (PXLongOperation.Exists(this._Graph.UID))
    {
      object[] processingList;
      this._Info = PXProcessing.GetProcessingInfo(this._Graph.UID, out processingList) as PXProcessingInfo<Table>;
      if (processingList != null)
        this.FillInProcWithProcessingList((IEnumerable) processingList);
      else if (this._InProc == null)
        this.FillInProcWithSelectedItemsOfOuterView();
    }
    if (this._InProc != null && this._InProc.Count > 0 && PXLongOperation.Exists(this._Graph.UID))
    {
      Exception message;
      if (PXLongOperation.GetStatus(this._Graph.UID, out TimeSpan _, out message) == PXLongRunStatus.Aborted && message is PXBaseRedirectException)
        throw message;
      PXUIFieldAttribute.SetEnabled(this._OuterView.Cache, this._SelectedField, false);
      if (this._Info == null)
        this._Info = PXProcessing.GetProcessingInfo(this._Graph.UID) as PXProcessingInfo<Table>;
      for (int index = 0; index < this._InProc.Count; ++index)
      {
        if (this._Info != null && this._Info.Messages != null && index < this._Info.Messages.Length && this._Info.Messages[index] != null)
          this._SelectedInfo[this._InProc[index][0]] = this._Info.Messages[index];
      }
      if (PXLongOperation.GetStatus(this._Graph.UID) == PXLongRunStatus.Completed && this._Info != null && !this._Info.ProcessingCompleted)
      {
        this._Info.ProcessingCompleted = true;
        PXProcessing.SetProcessingInfoInternal(this._Graph.UID, (object) this._Info);
        this.View.RequestFiltersReset();
      }
      return (IEnumerable) this._InProc;
    }
    if (!PXLongOperation.Exists(this._Graph.UID))
      PXUIFieldAttribute.SetEnabled(this.Cache, this._SelectedField, true);
    return this._SelectRecords();
  }

  /// <summary>
  /// Returns all records as pending records (sets to true 'Selected' property)
  /// </summary>
  /// <returns></returns>
  protected virtual List<Table> _PendingList(
    object[] parameters,
    string[] sorts,
    bool[] descendings,
    PXFilterRow[] filters)
  {
    PXCache<Table> cache = (PXCache<Table>) this._OuterView.Cache;
    int startRow = 0;
    int totalRows = 0;
    List<Table> ableList1 = new List<Table>();
    List<Table> ableList2 = (List<Table>) null;
    this._OuterView.SupressTailSelect = true;
    try
    {
      if (this.SuppressMerge)
        this._OuterView.IsReadOnly = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ableList2 = this._OuterView.Select((object[]) null, parameters, (object[]) null, sorts, descendings, this._AlterFilters(filters), ref startRow, 0, ref totalRows).Select<object, Table>(PXProcessingBase<Table>.\u003C\u003EO.\u003C0\u003E__Unwrap ?? (PXProcessingBase<Table>.\u003C\u003EO.\u003C0\u003E__Unwrap = new Func<object, Table>(PXResult.Unwrap<Table>))).ToList<Table>();
    }
    finally
    {
      this._OuterView.SupressTailSelect = false;
    }
    foreach (Table able in ableList2)
    {
      Table data = able;
      if ((object) cache.Locate(data) != null)
      {
        cache.SetValue((object) data, this._SelectedField, (object) true);
        if (this.SuppressUpdate)
          cache.SetStatus((object) data, PXEntryStatus.Updated);
        else
          data = cache.Update(data);
      }
      else
      {
        cache.PlaceNotChangedWithOriginals(data);
        if (this.SuppressUpdate)
        {
          cache.SetValue((object) data, this._SelectedField, (object) true);
          cache.SetStatus((object) data, PXEntryStatus.Updated);
        }
        else
        {
          OrderedDictionary keys = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
          foreach (string key in (IEnumerable<string>) cache.Keys)
            keys[(object) key] = PXFieldState.UnwrapValue(cache.GetValueExt((object) data, key));
          cache.Update((IDictionary) keys, (IDictionary) new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
          {
            [(object) this._SelectedField] = (object) true
          });
          data = (Table) cache.Current;
        }
      }
      if ((object) data != null)
      {
        ableList1.Add(data);
        if (cache.GetStateExt((object) data, this._SelectedField) is PXFieldState stateExt && !stateExt.Enabled)
        {
          cache.SetValue((object) data, this._SelectedField, (object) false);
          if (this.SuppressUpdate)
            cache.SetStatus((object) data, PXEntryStatus.Updated);
          else
            cache.Update(data);
        }
      }
    }
    return ableList1;
  }

  protected PXFilterRow[] _AlterFilters(PXFilterRow[] orfilters)
  {
    List<PXFilterRow> pxFilterRowList = orfilters == null ? new List<PXFilterRow>() : ((IEnumerable<PXFilterRow>) orfilters).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (f => (PXFilterRow) f.Clone())).ToList<PXFilterRow>();
    pxFilterRowList.RemoveAll((Predicate<PXFilterRow>) (_ => _.DataField == "ProcessingStatus"));
    if (pxFilterRowList.Count > 0)
      pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = false;
    if (this._AdditionalFilters != null && this._AdditionalFilters.Length != 0)
    {
      if (pxFilterRowList.Count > 1)
      {
        ++pxFilterRowList[0].OpenBrackets;
        ++pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets;
      }
      if (pxFilterRowList.Count > 0)
        pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = false;
      if (this._AdditionalFilters.Length > 1)
      {
        ++this._AdditionalFilters[0].OpenBrackets;
        ++this._AdditionalFilters[this._AdditionalFilters.Length - 1].CloseBrackets;
      }
      pxFilterRowList.AddRange((IEnumerable<PXFilterRow>) this._AdditionalFilters);
    }
    return pxFilterRowList.ToArray();
  }

  /// <summary>
  /// Iterates over items and returns only items with "Selected" field set to true.
  /// </summary>
  protected List<Table> GetSelectedItems(PXCache cache, IEnumerable items)
  {
    List<Table> selected = new List<Table>();
    this.ProcessSelectedItems(cache, items, (System.Action<Table>) (item => selected.Add(item)));
    return selected;
  }

  /// <summary>
  /// Fills processing resultset with items of an outer view whose "Selected" field set to true.
  /// </summary>
  protected void FillInProcWithSelectedItemsOfOuterView()
  {
    this._InProc = new PXResultset<Table>();
    PXCache cache = this._OuterView.Cache;
    this.ProcessSelectedItems(cache, cache.Cached, (System.Action<Table>) (item => this._InProc.Add(new PXResult<Table>(item))));
  }

  /// <summary>
  /// Fills processing resultset with items of provided collection.
  /// </summary>
  protected void FillInProcWithProcessingList(IEnumerable processingList)
  {
    this._InProc = new PXResultset<Table>();
    foreach (Table processing in processingList)
      this._InProc.Add(new PXResult<Table>(processing));
  }

  /// <summary>
  /// Tries to get an index of the current item of PXLongOperation in a provided list.
  /// </summary>
  protected static bool TryGetCurrentItemIndex(IList list, out int index)
  {
    if (PXLongOperation.GetCurrentItem() is Table currentItem)
    {
      index = list.IndexOf((object) currentItem);
      return index != -1;
    }
    index = -1;
    return false;
  }

  /// <summary>
  /// Iterates over items and performs an action on items with "Selected" field set to true.
  /// </summary>
  private void ProcessSelectedItems(PXCache cache, IEnumerable items, System.Action<Table> action)
  {
    foreach (Table data in items)
    {
      if (Convert.ToBoolean(cache.GetValue((object) data, this._SelectedField)) && EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus((object) data), PXEntryStatus.Inserted, PXEntryStatus.Updated))
        action(data);
    }
  }

  private static Action<T, CancellationToken> ToSync<T>(Func<T, CancellationToken, Task> handler)
  {
    return (Action<T, CancellationToken>) ((arg, outerCancellationToken) => AsyncToSync.Convert((Func<CancellationToken, Task>) (cancellationToken => handler(arg, cancellationToken)))(outerCancellationToken));
  }

  private static Action<T1, T2, CancellationToken> ToSync<T1, T2>(
    Func<T1, T2, CancellationToken, Task> handler)
  {
    return (Action<T1, T2, CancellationToken>) ((arg1, arg2, outerCancellationToken) => AsyncToSync.Convert((Func<CancellationToken, Task>) (cancellationToken => handler(arg1, arg2, cancellationToken)))(outerCancellationToken));
  }

  protected bool TryWithExternal(System.Action<PXProcessingBase<Table>> action)
  {
    if (!(this._Graph.Views.GetExternalMember(this.View) is PXProcessingBase<Table> externalMember) || this == externalMember)
      return false;
    action(externalMember);
    return true;
  }

  private static void InvokeWorkflowActionInternal(
    PXGraph graph,
    string action,
    object record,
    IEnumerable parameters,
    CancellationToken cancellationToken)
  {
    object[] parameters1 = parameters as object[];
    Dictionary<string, object> parameters2 = parameters as Dictionary<string, object>;
    List<object> recordsList = record as List<object>;
    if (string.IsNullOrEmpty(action) || !action.Contains("$"))
      throw new PXInvalidOperationException();
    if (recordsList == null && record != null)
      recordsList = new List<object>() { record };
    PXProcessingBase<Table>.PressActionDelegate pressAction = parameters1 != null ? PXProcessingBase<Table>.CaptureParameters(parameters1) : (parameters2 != null ? PXProcessingBase<Table>.CaptureParameters(parameters2) : PXProcessingBase<Table>.CaptureParameters(Array.Empty<object>()));
    PXProcessingBase<Table>.InvokeWorkflowDrivenAction(graph, action, (IReadOnlyList<object>) recordsList, pressAction, cancellationToken);
  }

  private static void InvokeWorkflowDrivenAction(
    PXGraph graph,
    string action,
    IReadOnlyList<object> recordsList,
    PXProcessingBase<Table>.PressActionDelegate pressAction,
    CancellationToken cancellationToken)
  {
    string str1;
    string str2;
    ArrayDeconstruct.Deconstruct<string>(action.Split('$'), ref str1, ref str2);
    string str3 = str1;
    string actionName = str2;
    string typeName = graph.WorkflowService?.GetGraphTypeByScreenID(str3) ?? PXSiteMap.Provider.FindSiteMapNodeByScreenID(str3)?.GraphType;
    PXGraph pxGraph = (PXGraph) null;
    System.Type type = PXBuildManager.GetType(typeName, false);
    if (type != (System.Type) null)
      pxGraph = PXGraph.CreateInstance(type);
    if (pxGraph == null)
      return;
    PXWorkflowService workflowService = pxGraph.WorkflowService;
    if (workflowService == null)
      return;
    AUScreenActionBaseState screenActionBaseState = workflowService.GetActionDefinitions(str3).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
    Dictionary<string, object> values = (Dictionary<string, object>) null;
    if (screenActionBaseState != null && screenActionBaseState.Form != null)
    {
      IReadOnlyDictionary<string, object> providedFormValues = workflowService.GetOnlyProvidedFormValues(graph, str3, screenActionBaseState.Form);
      values = providedFormValues != null ? providedFormValues.ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (it => it.Key), (Func<KeyValuePair<string, object>, object>) (it => it.Value)) : (Dictionary<string, object>) null;
    }
    Exception exception = PXSystemWorkflows.CheckErrors(pxGraph);
    if (exception != null)
      throw new PXOperationCompletedWithErrorException(exception.Message);
    pxGraph.UID = graph.UID;
    int num;
    if (screenActionBaseState == null)
    {
      num = 1;
    }
    else
    {
      bool? batchMode = screenActionBaseState.BatchMode;
      bool flag = true;
      num = !(batchMode.GetValueOrDefault() == flag & batchMode.HasValue) ? 1 : 0;
    }
    if (num != 0 || workflowService.HasAnyActionSequences(str3, actionName))
    {
      PXContext.SetSlot<bool>("Workflow.NonBatchModeProcessing", true);
      PXReportRequiredException requiredException1 = (PXReportRequiredException) null;
      bool flag = false;
      for (int index = 0; index < recordsList.Count; ++index)
      {
        cancellationToken.ThrowIfCancellationRequested();
        PXContext.SetSlot<bool>("Workflow.OperationCompletedInTransaction", false);
        pxGraph.Clear();
        pxGraph.TimeStamp = pxGraph.SqlDialect.GetLatestTimestamp(graph.TimeStamp, PXTimeStampScope.GetValue() ?? pxGraph._VeryFirstTimeStamp);
        object records = recordsList[index];
        if (records is PXResult pxResult)
          records = pxResult[0];
        List<object> processingList = new List<object>()
        {
          records
        };
        PXCache cache = pxGraph.Views[pxGraph.PrimaryView].Cache;
        cache.Current = records;
        if (cache.Current != cache.Locate(records))
        {
          cache.Remove(records);
          cache.Hold(records);
        }
        if (screenActionBaseState != null && screenActionBaseState.Form != null)
          workflowService.SetFormValues(pxGraph, screenActionBaseState.Form, (IDictionary<string, object>) values);
        PXProcessing<Table>.SetCurrentItem(records);
        List<object> objectList = new List<object>();
        try
        {
          foreach (object obj in pressAction(pxGraph, actionName, processingList))
            objectList.Add(obj);
          if (PXProcessing<Table>.GetItemMessage() == null)
          {
            string processingMessage = ActionSequencesLog.GetMassProcessingMessage();
            if (processingMessage == null)
              PXProcessing<Table>.SetInfo("The record has been processed successfully.");
            else if (ActionSequencesLog.HasErrors())
              PXProcessing<Table>.SetError(processingMessage);
            else
              PXProcessing<Table>.SetInfo(processingMessage);
          }
        }
        catch (PXReportRequiredException ex)
        {
          if (requiredException1 == null)
            requiredException1 = ex;
          else
            requiredException1.AddSibling(ex.ReportID, ex.Parameters);
        }
        catch (PXRedirectRequiredException ex)
        {
          PXGraph graph1 = ex.Graph;
          if ((graph1 != null ? (graph1.IsDirty ? 1 : 0) : 0) != 0)
            ex.Graph.Actions.PressSave();
        }
        catch (PXBaseRedirectException ex)
        {
          throw;
        }
        catch (PXOuterException ex)
        {
          if (PXLongOperation.GetCurrentItem() is Table currentItem && processingList.IndexOf((object) currentItem) != -1)
            PXProcessing<Table>.SetError(ex.GetFullMessage(" "));
          flag = true;
        }
        catch (PXSetPropertyException ex)
        {
          if (PXLongOperation.GetCurrentItem() is Table currentItem && processingList.IndexOf((object) currentItem) != -1)
          {
            switch (ex.ErrorLevel)
            {
              case PXErrorLevel.Warning:
                PXProcessing<Table>.SetWarning(ex.Message);
                break;
              case PXErrorLevel.Error:
                PXProcessing<Table>.SetError(ex.Message);
                break;
              default:
                PXProcessing<Table>.SetInfo(ex.MessageNoPrefix);
                break;
            }
          }
          flag = true;
        }
        catch (PXOperationCompletedSingleErrorException ex)
        {
          throw;
        }
        catch (PXOperationCompletedException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          if (PXLongOperation.GetCurrentItem() is Table currentItem && processingList.IndexOf((object) currentItem) != -1)
          {
            PXProcessingMessage itemMessage = PXProcessing<Table>.GetItemMessage();
            if (itemMessage == null || itemMessage.ErrorLevel != PXErrorLevel.RowError)
              PXProcessing<Table>.SetError(ex.Message);
          }
          flag = true;
        }
        IEnumerable<PXReportRequiredException> reportRedirects = ActionSequencesLog.GetReportRedirects();
        PXReportRequiredException requiredException2 = reportRedirects != null ? reportRedirects.FirstOrDefault<PXReportRequiredException>() : (PXReportRequiredException) null;
        if (requiredException2 != null)
        {
          if (requiredException1 == null)
            requiredException1 = requiredException2;
          else
            requiredException1.AddSibling(requiredException2.ReportID, requiredException2.Parameters);
        }
      }
      if (requiredException1 != null)
        throw requiredException1;
      if (flag)
        throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    }
    else
    {
      pxGraph.Clear();
      pxGraph.TimeStamp = pxGraph.SqlDialect.GetLatestTimestamp(graph.TimeStamp, PXTimeStampScope.GetValue() ?? pxGraph._VeryFirstTimeStamp);
      List<object> list = recordsList.Select<object, object>((Func<object, object>) (row => !(row is PXResult pxResult) ? row : pxResult[0])).ToList<object>();
      if (screenActionBaseState.Form != null)
        workflowService.SetFormValues(pxGraph, screenActionBaseState?.Form, (IDictionary<string, object>) values, true);
      List<object> objectList = new List<object>();
      foreach (object obj in pressAction(pxGraph, actionName, list))
      {
        cancellationToken.ThrowIfCancellationRequested();
        objectList.Add(obj);
      }
      workflowService.ClearMassProcessingWorkflowObjectKeys();
    }
  }

  private static PXProcessingBase<Table>.PressActionDelegate CaptureParameters(
    Dictionary<string, object> parameters)
  {
    return new PXProcessingBase<Table>.PressActionDelegate(Impl);

    IEnumerable Impl(PXGraph processor, string actionName, List<object> processingList)
    {
      return processor.Actions[actionName].Press(new PXAdapter((PXView) new PXView.Dummy(processor, processor.Views[processor.PrimaryView].BqlSelect, processingList))
      {
        MassProcess = true,
        Arguments = parameters
      });
    }
  }

  private static PXProcessingBase<Table>.PressActionDelegate CaptureParameters(object[] parameters)
  {
    return new PXProcessingBase<Table>.PressActionDelegate(Impl);

    IEnumerable Impl(PXGraph processor, string actionName, List<object> processingList)
    {
      PXAction action = processor.Actions[actionName];
      PXAdapter adapter = new PXAdapter((PXView) new PXView.Dummy(processor, processor.Views[processor.PrimaryView].BqlSelect, processingList))
      {
        MassProcess = true
      };
      if (parameters != null)
      {
        string[] parameterNames = action.GetParameterNames();
        for (int index = 0; index < parameterNames.Length && index < parameters.Length; ++index)
        {
          if (parameters[index] != null)
            adapter.Arguments[parameterNames[index]] = parameters[index];
        }
      }
      return action.Press(adapter);
    }
  }

  protected virtual void _SetProcessTargetInternal(string action, IEnumerable parameters)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e._SetProcessTargetInternal(action, parameters))))
      return;
    this._IsInstance = false;
    if (string.IsNullOrEmpty(action) || !action.Contains("$"))
      return;
    PXWorkflowService workflowService = this._Graph.WorkflowService;
    if (workflowService == null)
      return;
    string[] strArray = action.Split('$');
    string screenId = strArray[0];
    string actionName = strArray[1];
    this._ProcessDelegate = (Action<List<Table>, CancellationToken>) ((list, cancellationToken) =>
    {
      PXProcessingInfo<Table> info = new PXProcessingInfo<Table>()
      {
        Messages = new PXProcessingMessagesCollection<Table>(list.Count)
      };
      object[] array = list.Cast<object>().ToArray<object>();
      PXProcessing.SetProcessingInfo((PXProcessingInfo) info, array);
      PXParallelProcessingOptions parallelOpt = new PXParallelProcessingOptions();
      System.Action<PXParallelProcessingOptions> processingOptions = this.ParallelProcessingOptions;
      if (processingOptions != null)
        processingOptions(parallelOpt);
      if (WebConfig.ParallelProcessingDisabled || !parallelOpt.IsEnabled)
      {
        List<object> src = new List<object>(list.Count);
        for (int index = 0; index < list.Count; ++index)
          src.Add((object) list[index]);
        PXProcessingBase<Table>.ProcessMultipleItems<Table>(this._Graph, action, parameters, src, info.Messages, cancellationToken);
      }
      else
        PXBatchList.SplitAndProcessAll((IList) list, parallelOpt, (Func<(int, int), System.Action<CancellationToken>>) (range =>
        {
          (int start2, int end2) = range;
          return (System.Action<CancellationToken>) (batchCancellationToken =>
          {
            List<object> src = new List<object>();
            for (int index = start2; index <= end2; ++index)
              src.Add((object) list[index]);
            PXProcessing.SetProcessingInfo((PXProcessingInfo) info, src.ToArray());
            if (start2 > 0)
              PXContext.SetSlot<int?>("PXParallelProcessingOffset", new int?(start2));
            PXProcessingBase<Table>.ProcessMultipleItems<Table>(this._Graph, action, parameters, src, info.Messages, batchCancellationToken);
          });
        }), cancellationToken);
    });
    AUScreenActionBaseState actionDefinition = workflowService.GetActionDefinitions(screenId).FirstOrDefault<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (it => it.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
    if (actionDefinition?.Form != null)
    {
      if (PXContext.GetSlot<AUSchedule>() != null || PXContext.GetSlot<bool>("ScheduleIsRunning"))
      {
        workflowService.PrepareFormDataForMassProcessing(this._Graph, actionDefinition.Form, screenId);
        this._ParametersDelegate = (PXProcessingBase<Table>.ParametersDelegate) null;
      }
      else
        this._ParametersDelegate = (PXProcessingBase<Table>.ParametersDelegate) (_param1 => workflowService.AskMassUpdateExt(this._Graph, actionDefinition.Form, screenId, workflowService.GetScreen(screenId), action));
    }
    else
      this._ParametersDelegate = (PXProcessingBase<Table>.ParametersDelegate) null;
  }

  private static void ProcessMultipleItems<Table>(
    PXGraph graph,
    string action,
    IEnumerable parameters,
    List<object> src,
    PXProcessingMessagesCollection<Table> perRowMessage,
    CancellationToken cancellationToken)
    where Table : class, IBqlTable
  {
    try
    {
      PXProcessingBase<Table>.InvokeWorkflowActionInternal(graph, action, (object) src, parameters, cancellationToken);
      for (int index = 0; index < src.Count; ++index)
      {
        if (perRowMessage[index] == null)
          perRowMessage[index] = new PXProcessingMessage(PXErrorLevel.RowInfo, "The record has been processed successfully.");
      }
    }
    catch (PXOuterException ex)
    {
      int index;
      if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) src, out index))
        perRowMessage[index] = new PXProcessingMessage(PXErrorLevel.RowError, ex.GetFullMessage(" "));
      throw;
    }
    catch (PXSetPropertyException ex)
    {
      int index;
      if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) src, out index))
      {
        PXErrorLevel errorLevel = ex.ErrorLevel;
        switch (errorLevel)
        {
          case PXErrorLevel.Warning:
            errorLevel = PXErrorLevel.RowWarning;
            break;
          case PXErrorLevel.Error:
            errorLevel = PXErrorLevel.RowError;
            break;
        }
        perRowMessage[index] = new PXProcessingMessage(errorLevel, errorLevel == PXErrorLevel.RowInfo ? ex.MessageNoPrefix : ex.Message);
      }
      throw new PXException("At least one item has not been processed.");
    }
    catch (PXBaseRedirectException ex)
    {
      throw;
    }
    catch (PXOperationCompletedSingleErrorException ex)
    {
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    }
    catch (PXOperationCompletedException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      int index;
      if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) src, out index) && (perRowMessage[index] == null || perRowMessage[index].ErrorLevel != PXErrorLevel.RowError))
        perRowMessage[index] = new PXProcessingMessage(PXErrorLevel.RowError, ex.Message);
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    }
  }

  [Obsolete]
  public virtual void SetProcessTarget(
    string graphType,
    string stepID,
    string action,
    string menu,
    params object[] parameters)
  {
    this._SetProcessTargetInternal(action, (IEnumerable) parameters);
  }

  [Obsolete]
  public virtual void SetProcessTarget(
    string graphType,
    string stepID,
    string action,
    string menu,
    Dictionary<string, object> parameters)
  {
    this._SetProcessTargetInternal(action, (IEnumerable) parameters);
  }

  /// <summary>Sets the workflow action that is invoked to process multiple data records.</summary>
  /// <param name="action">The name of the action.</param>
  /// <remarks>We recommend that you use this method after the graph has been already initialized.
  /// For example, the method can be used in a RowSelected event handler.</remarks>
  public virtual void SetProcessWorkflowAction(string action)
  {
    this._SetProcessTargetInternal(action, (IEnumerable) null);
  }

  /// <summary>Sets the workflow action that is invoked to process multiple data records
  /// and specifies the parameters of the action.</summary>
  /// <param name="action">The name of the graph action.</param>
  /// <param name="parameters">The array of values that is used in the processing action.</param>
  /// <remarks>We recommend that you use this method after the graph has been already initialized.
  /// For example, the method can be used in a RowSelected event handler.</remarks>
  public virtual void SetProcessWorkflowAction(string action, params object[] parameters)
  {
    this._SetProcessTargetInternal(action, (IEnumerable) parameters);
  }

  /// <summary>Sets the workflow action that is invoked to process multiple data records
  /// and specifies the parameters of the action as a dictionary.</summary>
  /// <param name="action">The name of the graph action.</param>
  /// <param name="parameters">The key-value pairs of parameters and their values that are used in the processing action.</param>
  /// <remarks>We recommend that you use this method after the graph has been already initialized.
  /// For example, the method can be used in a RowSelected event handler.</remarks>
  public virtual void SetProcessWorkflowAction(string action, Dictionary<string, object> parameters)
  {
    this._SetProcessTargetInternal(action, (IEnumerable) parameters);
  }

  /// <summary>Sets the workflow action (by its name) that is invoked to process multiple data records.</summary>
  /// <typeparam name="TGraph">The type of the graph.</typeparam>
  /// <param name="actionName">The name of the graph action.</param>
  /// <remarks>We recommend that you use this method after the graph has been already initialized.
  /// For example, the method can be used in a RowSelected event handler.</remarks>
  public virtual void SetProcessWorkflowAction<TGraph>(string actionName) where TGraph : PXGraph
  {
    string screenId = PXSiteMap.Provider.FindSiteMapNodeByGraphType(CustomizedTypeManager.GetTypeNotCustomized(typeof (TGraph)).FullName)?.ScreenID;
    if (string.IsNullOrEmpty(screenId))
      return;
    this._SetProcessTargetInternal($"{screenId}${actionName}", (IEnumerable) null);
  }

  /// <summary>By using a function expression sets the workflow action
  /// that is invoked to process multiple data records.</summary>
  /// <typeparam name="TGraph">The type of the graph.</typeparam>
  /// <param name="actionSelector">A function expression that returns the graph action.</param>
  /// <remarks>We recommend that you use this method after the graph has been already initialized.
  /// For example, the method can be used in a RowSelected event handler.</remarks>
  /// <example>
  /// In the RowSelected event handler, you specify the workflow action that the processing form should use for processing.
  /// <code>protected virtual void _(Events.RowSelected&lt;RSSVWorkOrder&gt; e)
  /// {
  ///     WorkOrders.SetProcessWorkflowAction&lt;RSSVWorkOrderEntry&gt;(
  ///         g =&gt; g.Assign);
  /// }</code></example>
  public virtual void SetProcessWorkflowAction<TGraph>(
    Expression<Func<TGraph, PXAction>> actionSelector)
    where TGraph : PXGraph
  {
    string screenId = PXSiteMap.Provider.FindSiteMapNodeByGraphType(CustomizedTypeManager.GetTypeNotCustomized(typeof (TGraph)).FullName)?.ScreenID;
    if (string.IsNullOrEmpty(screenId))
      return;
    this._SetProcessTargetInternal($"{screenId}${((MemberExpression) actionSelector.Body).Member.Name}", (IEnumerable) null);
  }

  /// <summary>Sets the method that is invoked to process multiple data
  /// records. The delegate works with the list of records and supports the cancellation token.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  /// <remarks>The method receives the list of the data records to process in
  /// the parameter. Depending on the button the user clicked to start
  /// processing, the data records are either the data records selected by
  /// the user in the grid or all data records selected by the data view.</remarks>
  public virtual void SetProcessDelegate(Action<List<Table>, CancellationToken> handler)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate(handler))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore(handler);
  }

  public void SetAsyncProcessDelegate(Func<List<Table>, CancellationToken, Task> handler)
  {
    this.SetProcessDelegate(PXProcessingBase<Table>.ToSync<List<Table>>(handler));
  }

  private void SetProcessDelegateCore(Action<List<Table>, CancellationToken> handler)
  {
    bool isImport = this._Graph.IsImport;
    this._ProcessDelegate = (Action<List<Table>, CancellationToken>) ((list, cancellationToken) =>
    {
      PXProcessingInfo<Table> info = new PXProcessingInfo<Table>()
      {
        Messages = new PXProcessingMessagesCollection<Table>(list.Count)
      };
      object[] castList = (object[]) null;
      if (!isImport || this._Graph.IsMobile)
      {
        castList = list.Cast<object>().ToArray<object>();
        try
        {
          PXProcessing.SetProcessingInfo((PXProcessingInfo) info, castList);
        }
        catch (PXException ex)
        {
        }
      }
      PXParallelProcessingOptions parallelOpt = new PXParallelProcessingOptions();
      System.Action<PXParallelProcessingOptions> processingOptions = this.ParallelProcessingOptions;
      if (processingOptions != null)
        processingOptions(parallelOpt);
      if (WebConfig.ParallelProcessingDisabled || !parallelOpt.IsEnabled)
        ProcessList(list, info.Messages, cancellationToken);
      else
        PXBatchList.SplitAndProcessAll((IList) list, parallelOpt, (Func<(int, int), System.Action<CancellationToken>>) (range =>
        {
          (int start2, int end2) = range;
          return (System.Action<CancellationToken>) (batchCancellationToken =>
          {
            List<Table> ableList = new List<Table>();
            for (int index = start2; index <= end2; ++index)
              ableList.Add(list[index]);
            if (castList != null)
              PXProcessing.SetProcessingInfo((PXProcessingInfo) info, ableList.Cast<object>().ToArray<object>());
            if (start2 > 0)
              PXContext.SetSlot<int?>("PXParallelProcessingOffset", new int?(start2));
            ProcessList(ableList, info.Messages, batchCancellationToken);
          });
        }), cancellationToken);
    });

    void ProcessList(
      List<Table> list,
      PXProcessingMessagesCollection<Table> perrowmessage,
      CancellationToken cancellationToken)
    {
      try
      {
        handler(list, cancellationToken);
        for (int index = 0; index < list.Count; ++index)
        {
          if (perrowmessage[index] == null)
            perrowmessage[index] = new PXProcessingMessage(PXErrorLevel.RowInfo, "The record has been processed successfully.");
        }
      }
      catch (PXOuterException ex)
      {
        int index;
        if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) list, out index))
          perrowmessage[index] = new PXProcessingMessage(PXErrorLevel.RowError, ex.GetFullMessage(" "));
        throw;
      }
      catch (PXSetPropertyException ex)
      {
        int index;
        if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) list, out index))
        {
          PXErrorLevel errorLevel = ex.ErrorLevel;
          switch (errorLevel)
          {
            case PXErrorLevel.Warning:
              errorLevel = PXErrorLevel.RowWarning;
              break;
            case PXErrorLevel.Error:
              errorLevel = PXErrorLevel.RowError;
              break;
          }
          perrowmessage[index] = new PXProcessingMessage(errorLevel, errorLevel == PXErrorLevel.RowInfo ? ex.MessageNoPrefix : ex.Message);
        }
        throw;
      }
      catch (PXBaseRedirectException ex)
      {
        throw;
      }
      catch (PXOperationCompletedSingleErrorException ex)
      {
        throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
      }
      catch (PXOperationCompletedException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        int index;
        if (PXProcessingBase<Table>.TryGetCurrentItemIndex((IList) list, out index))
          perrowmessage[index] = new PXProcessingMessage(PXErrorLevel.RowError, ex.Message);
        throw;
      }
    }
  }

  /// <summary>Sets the method that is invoked to process multiple data
  /// records. The delegate of the <see cref="T:PX.Data.PXProcessingBase`1.ProcessListDelegate" /> type does not support the cancellation token.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  /// <inheritdoc cref="M:PX.Data.PXProcessingBase`1.SetProcessDelegate(System.Action{System.Collections.Generic.List{`0},System.Threading.CancellationToken})" path="/remarks" />
  /// <example><para>The code below sets the processing method for a processing data view in a graph.</para>
  /// <code title="Example" lang="CS">
  /// // Definition of the processing data view
  /// public PXProcessingJoin&lt;BalancedAPDocument, ... &gt; APDocumentList;
  /// ...
  /// // The constructor of the graph
  /// public APDocumentRelease()
  /// {
  ///     ...
  ///     // Setting the delegate of a processing method and defining the
  ///     // processing method in place
  ///     APDocumentList.SetProcessDelegate(
  ///         delegate(List&lt;BalancedAPDocument&gt; list)
  ///         {
  ///             List&lt;APRegister&gt; newlist = new List&lt;APRegister&gt;(list.Count);
  ///             foreach (BalancedAPDocument doc in list)
  ///             {
  ///                 newlist.Add(doc);
  ///             }
  ///             ReleaseDoc(newlist, true);
  ///         }
  ///     );
  /// }
  /// // Definition of the method that does actual processing
  /// public static void ReleaseDoc(List&lt;APRegister&gt; list, bool isMassProcess)
  /// {
  ///     ...
  /// }</code>
  /// </example>
  public void SetProcessDelegate(
    PXProcessingBase<Table>.ProcessListDelegate handler)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate(handler))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore((Action<List<Table>, CancellationToken>) ((list, cancellationToken) => CancellationIgnorantExtensions.RunWithCancellationViaThreadAbort((System.Action) (() => handler(list)), cancellationToken)));
  }

  /// <summary>Sets the method that is invoked to process each data
  /// record. The delegate of the processing method works with a single record
  /// and supports the cancellation token.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  public virtual void SetProcessDelegate(Action<Table, CancellationToken> handler)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate(handler))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore(handler);
  }

  public void SetAsyncProcessDelegate(Func<Table, CancellationToken, Task> handler)
  {
    this.SetProcessDelegate(PXProcessingBase<Table>.ToSync<Table>(handler));
  }

  private void SetProcessDelegateCore(Action<Table, CancellationToken> handler)
  {
    this._ProcessDelegate = (Action<List<Table>, CancellationToken>) ((list, cancellationToken) =>
    {
      if (PXProcessing.ProcessItems<Table>(list, handler, cancellationToken) != list.Count)
        throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    });
  }

  /// <summary>Sets the method that is invoked to process each data
  /// record. The delegate of the <see cref="T:PX.Data.PXProcessingBase`1.ProcessItemDelegate" /> type works with a single record
  /// and does not support the cancellation token.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  public void SetProcessDelegate(
    PXProcessingBase<Table>.ProcessItemDelegate handler)
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate(handler))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore((Action<Table, CancellationToken>) ((item, _) => handler(item)));
  }

  /// <summary>Sets the method that is invoked to process each data
  /// record. The delegate method has three parameters, which are the graph, the data
  /// record, and the cancellation token.</summary>
  /// <param name="handler">The delegate of the processing method.</param>
  /// <remarks>When the user initiates processing, the data view initializes
  /// the instance of the specified graph type and passes it to the
  /// processing method while it is invoked for each data record.</remarks>
  public void SetProcessDelegate<Graph>(Action<Graph, Table, CancellationToken> handler) where Graph : PXGraph, new()
  {
    this.SetProcessDelegate<Graph>(handler, (Action<Graph, CancellationToken>) null);
  }

  /// <summary>Sets the method that is invoked to process each data
  /// record. The delegate method has two parameters, which are the graph and the data
  /// record, and does not support the cancellation topic.</summary>
  /// <inheritdoc cref="M:PX.Data.PXProcessingBase`1.SetProcessDelegate``1(System.Action{``0,`0,System.Threading.CancellationToken})" path="/remarks" />
  /// <example><para>The code below sets the processing method, which will process each data record,
  /// for a processing data view in a graph.</para>
  /// <code title="Example" lang="CS">
  /// // Definition of the processing data view
  /// public PXFilteredProcessing&lt;ARPaymentInfo&gt; ARDocumentList;
  /// ...
  /// ARDocumentList.SetProcessDelegate&lt;ARPaymentCCProcessing&gt;(
  ///     delegate(ARPaymentCCProcessing aGraph,ARPaymentInfo doc)
  ///     {
  ///         ProcessPayment(aGraph, doc);
  ///     }
  /// );</code>
  /// </example>
  public void SetProcessDelegate<Graph>(
    PXProcessingBase<Table>.ProcessItemDelegate<Graph> handler)
    where Graph : PXGraph, new()
  {
    this.SetProcessDelegate<Graph>(handler, (PXProcessingBase<Table>.FinallyProcessDelegate<Graph>) null);
  }

  /// <summary>Sets the method that is invoked to process each data record
  /// and the method that is invoked after all data records are
  /// processed. The methods support cancellation tokens.</summary>
  /// <remarks>
  /// <para>When the user initiates processing, the data view
  /// initializes the instance of the specified graph type and passes it to
  /// the processing method while it is invoked for each data record.</para>
  /// <para>The second method
  /// is invoked once when all data record are processed. </para>
  /// </remarks>
  /// <param name="handler">The delegate of the processing method. The method should have three parameters, which are the graph,
  /// the data record, and the cancellation token.</param>
  /// <param name="handlerFinally">The delegate of the method invoked when
  /// all data records are processed. The method has two parameters, which are the graph and the cancellation token.
  /// The graph parameter of the method is set to the graph that was passed to the processing
  /// method for each data record.</param>
  public virtual void SetProcessDelegate<Graph>(
    Action<Graph, Table, CancellationToken> handler,
    Action<Graph, CancellationToken> handlerFinally)
    where Graph : PXGraph, new()
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate<Graph>(handler, handlerFinally))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore<Graph>(handler, handlerFinally);
  }

  public void SetAsyncProcessDelegate<Graph>(
    Func<Graph, Table, CancellationToken, Task> handler,
    Func<Graph, CancellationToken, Task> handlerFinally = null)
    where Graph : PXGraph, new()
  {
    this.SetProcessDelegate<Graph>(PXProcessingBase<Table>.ToSync<Graph, Table>(handler), handlerFinally != null ? PXProcessingBase<Table>.ToSync<Graph>(handlerFinally) : (Action<Graph, CancellationToken>) null);
  }

  private void SetProcessDelegateCore<Graph>(
    Action<Graph, Table, CancellationToken> handler,
    Action<Graph, CancellationToken> handlerFinally)
    where Graph : PXGraph, new()
  {
    this._ProcessDelegate = (Action<List<Table>, CancellationToken>) ((list, cancellationToken) =>
    {
      bool flag;
      if (handlerFinally == null)
      {
        PXParallelProcessingOptions parallelOpt = new PXParallelProcessingOptions();
        System.Action<PXParallelProcessingOptions> processingOptions = this.ParallelProcessingOptions;
        if (processingOptions != null)
          processingOptions(parallelOpt);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        flag = PXProcessing.ProcessItemsParallel<Graph, Table>(list, handler, PXProcessingBase<Table>.\u003CSetProcessDelegateCore\u003EO__99_0<Graph>.\u003C0\u003E__CreateInstance ?? (PXProcessingBase<Table>.\u003CSetProcessDelegateCore\u003EO__99_0<Graph>.\u003C0\u003E__CreateInstance = new Func<Graph>(PXGraph.CreateInstance<Graph>)), parallelOpt, cancellationToken);
      }
      else
      {
        Graph graph = PXGraph.CreateInstance<Graph>();
        List<Table> list1 = list;
        Action<Graph, Table, CancellationToken> action = handler;
        Func<Graph> factory = (Func<Graph>) (() => graph);
        PXParallelProcessingOptions parallelOpt = new PXParallelProcessingOptions();
        parallelOpt.IsEnabled = false;
        CancellationToken cancellationToken1 = cancellationToken;
        flag = PXProcessing.ProcessItemsParallel<Graph, Table>(list1, action, factory, parallelOpt, cancellationToken1);
        handlerFinally(graph, cancellationToken);
      }
      if (flag)
        throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
    });
  }

  /// <summary>Sets the method that is invoked to process each data record
  /// and the method that is invoked after all data records are
  /// processed. The methods do not support cancellation tokens.</summary>
  /// <remarks>
  /// <para>When the user initiates processing, the data view
  /// initializes the instance of the specified graph type and passes it to
  /// the processing method while it is invoked for each data record.</para>
  /// <para>The second method
  /// is invoked once when all data record are processed. </para>
  /// </remarks>
  /// <param name="handler">The delegate of the processing method. The method should have two parameters, which are the graph and
  /// the data record.</param>
  /// <param name="handlerFinally">The delegate of the method invoked when
  /// all data records are processed. The method has one parameter, which is the graph.
  /// The parameter of the method is set to the graph that was passed to the processing
  /// method for each data record.</param>
  public void SetProcessDelegate<Graph>(
    PXProcessingBase<Table>.ProcessItemDelegate<Graph> handler,
    PXProcessingBase<Table>.FinallyProcessDelegate<Graph> handlerFinally)
    where Graph : PXGraph, new()
  {
    if (this.TryWithExternal((System.Action<PXProcessingBase<Table>>) (e => e.SetProcessDelegate<Graph>(handler, handlerFinally))))
      return;
    this._IsInstance = handler.Target == this._Graph;
    this.SetProcessDelegateCore<Graph>((Action<Graph, Table, CancellationToken>) ((g, r, _) => handler(g, r)), handlerFinally != null ? (Action<Graph, CancellationToken>) ((g, _) => handlerFinally(g)) : (Action<Graph, CancellationToken>) null);
  }

  [PXUIField(DisplayName = "Close")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable actionCloseProcessing(PXAdapter adapter)
  {
    this.ViewProcessingResults.Current.WasAborted = false;
    IEnumerable enumerable = this._Graph.Actions.PressCancel((PXAction) null, adapter);
    if (enumerable != null)
      return enumerable;
    this._Graph.Clear();
    return (IEnumerable) new object[0];
  }

  [PXUIField(DisplayName = "Cancel Processing", Visible = false)]
  [PXButton(DisplayOnMainToolbar = false)]
  protected void actionCancelProcessing()
  {
    if (this.ViewProcessingResults.Ask("Cancel Processing", "Are you sure you want to cancel the processing?", MessageButtons.YesNo) != WebDialogResult.Yes)
      return;
    this.ActionCancelProcessing.SetEnabled(false);
    this.ViewProcessingResults.Current.WasAborted = true;
    PXLongOperation.AsyncAbort(this._Graph.UID);
  }

  protected virtual IEnumerable viewProcessingResults()
  {
    PXProcessingBase<Table> pxProcessingBase = this;
    bool isMobile = pxProcessingBase._Graph.IsMobile;
    PXProcessingBase<Table>.ProcessingResults current = pxProcessingBase.ViewProcessingResults.Current;
    object[] processingList;
    PXProcessingInfo processingInfo = PXProcessing.GetProcessingInfo(pxProcessingBase._Graph.UID, out processingList);
    Exception message;
    bool isRedirected;
    PXLongRunStatus status = PXLongOperation.GetStatus(pxProcessingBase._Graph.UID, out TimeSpan _, out message, out isRedirected);
    if (status == PXLongRunStatus.NotExists || processingInfo == null)
    {
      current.ElapsedTime = "";
      current.Errors = new int?();
      current.Skipped = new int?();
      current.Percentage = new int?();
      current.ProcessedItems = new int?();
      yield return (object) current;
    }
    else
    {
      TimeSpan timeSpan1 = System.DateTime.UtcNow - processingInfo.StarTime;
      current.ElapsedTime = string.Format(PXMessages.LocalizeNoPrefix("{0} Elapsed"), (object) timeSpan1.ToString("hh\\:mm\\:ss"));
      int length = processingList.Length;
      current.Errors = new int?(processingInfo.Errors);
      current.ProcessedItems = new int?(processingInfo.Processed - processingInfo.Errors - processingInfo.Warnings);
      current.Skipped = new int?(processingInfo.Warnings);
      current.Total = new int?(length);
      current.Remains = new int?(length - processingInfo.Processed);
      double num1 = 100.0 * (double) processingInfo.Processed / (double) length;
      current.Percentage = new int?((int) num1);
      if (processingInfo.Processed > 0 && processingInfo.Processed < length && status == PXLongRunStatus.InProcess)
      {
        TimeSpan timeSpan2 = new TimeSpan(0, 0, (int) (timeSpan1.TotalSeconds / (double) processingInfo.Processed * (double) (length - processingInfo.Processed)));
        current.ElapsedTime = string.Format(PXMessages.LocalizeNoPrefix("{0}% Completed &emsp;{1} Remaining &emsp;{2}"), (object) current.Percentage, (object) timeSpan2.ToString("hh\\:mm\\:ss"), (object) current.ElapsedTime);
      }
      else if (processingInfo.Processed == 0 && status == PXLongRunStatus.InProcess)
        current.ElapsedTime = $"{PXMessages.LocalizeNoPrefix("Initializing...")} &emsp;{current.ElapsedTime}";
      else if (processingInfo.Processed == length && status == PXLongRunStatus.InProcess)
        current.ElapsedTime = $"{PXMessages.LocalizeNoPrefix("Post-processing... Please wait for the process to complete.")} &emsp;{current.ElapsedTime}";
      if (status != PXLongRunStatus.InProcess)
      {
        pxProcessingBase.ActionCancelProcessing.SetVisible(false);
        pxProcessingBase.ActionCancelProcessing.SetEnabled(true);
        pxProcessingBase.ActionCloseProcessing.SetVisible(true);
        current.Result = processingInfo.Errors > 0 ? PXMessages.LocalizeNoPrefix("Processing completed with errors") : (processingInfo.Warnings > 0 ? PXMessages.LocalizeNoPrefix("Processing completed with warnings") : PXMessages.LocalizeNoPrefix("Processing completed"));
        if (!isRedirected)
        {
          switch (message)
          {
            case null:
            case PXBaseRedirectException _:
              goto label_19;
            case PXOperationCompletedException completedException:
              if (completedException is PXOperationCompletedWithErrorException || completedException is PXOperationCompletedSingleErrorException)
                break;
              goto label_19;
          }
          string str = "Processing stopped on error";
          current.ProcessingErrorMessage = message is PXException pxException ? pxException.MessageNoPrefix : message.Message;
          int? total = current.Total;
          int? nullable = current.Remains;
          if (total.GetValueOrDefault() == nullable.GetValueOrDefault() & total.HasValue == nullable.HasValue)
          {
            str = "An error occurred while initializing processing";
            current.Result = isMobile ? string.Format(PXMessages.LocalizeNoPrefix("An error occurred while initializing processing. Error message: {0}"), (object) current.ProcessingErrorMessage) : string.Format(PXMessages.LocalizeNoPrefix("An error occurred while initializing processing. {0}Show error message{1}."), (object) $"<span title=\"{current.ProcessingErrorMessage}\" class=\"LabelAbbrError\" error=\"2\">", (object) "</span>");
          }
          else
          {
            nullable = current.Total;
            int processed = processingInfo.Processed;
            if (nullable.GetValueOrDefault() == processed & nullable.HasValue)
            {
              nullable = current.Errors;
              int num2 = 0;
              if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
              {
                str = "Processing completed with errors";
                current.Result = isMobile ? string.Format(PXMessages.LocalizeNoPrefix("Processing completed with errors. Error message: {0}"), (object) current.ProcessingErrorMessage) : string.Format(PXMessages.LocalizeNoPrefix("Processing completed with errors. {0}Show error message{1}"), (object) $"<span title=\"{current.ProcessingErrorMessage}\" class=\"LabelAbbrError\" error=\"2\">", (object) "</span>");
              }
            }
            else
            {
              nullable = current.Errors;
              int num3 = 0;
              current.Result = !(nullable.GetValueOrDefault() == num3 & nullable.HasValue) ? PXMessages.LocalizeNoPrefix("Processing stopped on error") : (isMobile ? string.Format(PXMessages.LocalizeNoPrefix("Processing stopped on error. Error message: {0}"), (object) current.ProcessingErrorMessage) : string.Format(PXMessages.LocalizeNoPrefix("Processing stopped on error. {0}Show error message{1}"), (object) $"<span title=\"{current.ProcessingErrorMessage}\" class=\"LabelAbbrError\" error=\"2\">", (object) "</span>"));
            }
          }
          pxProcessingBase.Logger.ForTelemetry("Processing", "ProcessingError").Error(message.InnerException, str);
          goto label_22;
        }
label_19:
        if (!isRedirected && message is PXOperationCompletedWithWarningException warningException)
        {
          current.ProcessingErrorMessage = warningException.MessageNoPrefix;
          int? skipped = current.Skipped;
          int num4 = 0;
          if (skipped.GetValueOrDefault() == num4 & skipped.HasValue)
            current.Result = isMobile ? string.Format(PXMessages.LocalizeNoPrefix("Processing completed with warnings. Warning message: {0}"), (object) current.ProcessingErrorMessage) : string.Format(PXMessages.LocalizeNoPrefix("Processing completed with warnings. {0}Show warning message{1}"), (object) $"<span title=\"{current.ProcessingErrorMessage}\" class=\"LabelAbbrWarning\" error=\"1\">", (object) "</span>");
        }
label_22:
        if (current.WasAborted || status == PXLongRunStatus.Aborted && message.Message.Equals("The operation has been aborted.", StringComparison.OrdinalIgnoreCase))
          current.Result = PXMessages.LocalizeNoPrefix("Processing cancelled");
        current.Percentage = new int?(100);
        current.ElapsedTime = $"{current.Result}, {current.ElapsedTime}";
      }
      else
      {
        pxProcessingBase.ActionCancelProcessing.SetVisible(true);
        pxProcessingBase.ActionCloseProcessing.SetVisible(false);
      }
      yield return (object) current;
    }
  }

  /// <summary>The delegate for processing a list of data records.</summary>
  public delegate bool ParametersDelegate(List<Table> list) where Table : class, IBqlTable, new();

  /// <summary>The delegate for processing a list of data records.</summary>
  /// <param name="list">The data records to process.</param>
  public delegate void ProcessListDelegate(List<Table> list) where Table : class, IBqlTable, new();

  /// <summary>
  /// The delegate of the method for processing a single data record.
  /// </summary>
  /// <param name="item">The data record to process.</param>
  public delegate void ProcessItemDelegate(Table item) where Table : class, IBqlTable, new();

  /// <summary>
  /// The delegate of the method for processing a single data record.
  /// The delegate allows you to receive the same instance of the
  /// provided graph type to each invocation of the processing method
  /// during the processing operation.
  /// </summary>
  /// <typeparam name="Graph">The graph type.</typeparam>
  /// <param name="graph">The graph instance shared between invocations
  /// of the method for different data records.</param>
  /// <param name="item">The data record to process.</param>
  public delegate void ProcessItemDelegate<Graph>(Graph graph, Table item)
    where Table : class, IBqlTable, new()
    where Graph : PXGraph, new();

  /// <summary>
  /// The delegate of the method that is executed after all data records
  /// are processed. In the parameter, the method receives the graph that
  /// was passed to each invocation of the data record processing method
  /// during the processing operation.
  /// </summary>
  /// <typeparam name="Graph">The graph type.</typeparam>
  /// <param name="graph">The graph instance passed to invocations of the
  /// <tt>ProcessItemDelegate&lt;Graph&gt;(Graph, Table)</tt> delegate.</param>
  public delegate void FinallyProcessDelegate<Graph>(Graph graph)
    where Table : class, IBqlTable, new()
    where Graph : PXGraph, new();

  [Obsolete("Use parameterless ParametrizedView class instead")]
  protected class ParametrizedView<TableP>(
    PXGraph graph,
    BqlCommand select,
    PXProcessingBase<Table> processing,
    PXSelectDelegate handler) : PXProcessingBase<Table>.ParametrizedView(graph, select, processing, handler)
    where TableP : class, IBqlTable, new()
  {
  }

  /// <exclude />
  protected internal class ParametrizedView : PXView, IPXProcessingView
  {
    private readonly PXProcessingBase<Table> _processing;

    public string ViewName => this._Graph.ViewNames[this._processing.View];

    public string FilterName
    {
      get
      {
        string viewName = this._Graph.ViewNames[(PXView) this];
        return viewName == this.ViewName ? (string) null : viewName;
      }
    }

    public ParametrizedView(
      PXGraph graph,
      BqlCommand select,
      PXProcessingBase<Table> processing,
      PXSelectDelegate handler)
      : base(graph, false, select, (Delegate) handler)
    {
      this._processing = processing;
    }

    public override string[] GetParameterNames() => this._processing._OuterView.GetParameterNames();

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
      this._processing._SelectFromUI = PXView._Executing.Count == 0;
      if (parameters != null && parameters.Length != 0)
        this._processing._Parameters = parameters;
      if (filters != null && filters.Length != 0)
      {
        this._processing._Filters = new PXFilterRow[filters.Length];
        for (int index = 0; index < filters.Length; ++index)
          this._processing._Filters[index] = new PXFilterRow(filters[index]);
      }
      else
        this._processing._Filters = new PXFilterRow[0];
      return base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }
  }

  /// <exclude />
  protected class OuterView : PXView
  {
    public OuterView(PXView view, BqlCommand select)
      : base(view.Graph, false, select)
    {
    }

    public OuterView(PXView view, BqlCommand select, Delegate handler)
      : base(view.Graph, false, view.BqlSelect, !(handler is PXSelectDelegate) ? handler : (Delegate) (() =>
      {
        IEnumerable enumerable = ((PXSelectDelegate) handler)();
        IBqlJoinedSelect bqlSelect = view.BqlSelect as IBqlJoinedSelect;
        IBqlJoinedSelect bqlJoinedSelect = select as IBqlJoinedSelect;
        if (bqlSelect?.GetTail() == null || bqlJoinedSelect?.GetTail() != null && bqlJoinedSelect.GetTail().GetType() == bqlSelect.GetTail().GetType())
          return enumerable;
        List<object> list = new List<object>();
        foreach (object obj in enumerable)
          view.AppendTail((object) (obj is PXResult ? (Table) (PXResult<Table>) obj : (Table) obj), list, PXView.Parameters);
        return (IEnumerable) list;
      }))
    {
    }
  }

  private delegate IEnumerable PressActionDelegate(
    PXGraph processor,
    string actionName,
    List<object> processingList)
    where Table : class, IBqlTable, new();

  [PXHidden]
  public class ProcessingResults : 
    PXBqlTable,
    IBqlTable,
    IBqlTableSystemDataStorage,
    IPXProcessingResults
  {
    public bool WasAborted;

    [PXInt]
    [PXUIField(DisplayName = "Processed", Visible = false)]
    public int? ProcessedItems { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Errors", Visible = false)]
    public int? Errors { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Skipped", Visible = false)]
    public int? Skipped { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Total", Visible = false)]
    public int? Total { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Remaining", Visible = false)]
    public int? Remains { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Progress", Visible = false)]
    public string ElapsedTime { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Remaining", Visible = false)]
    public string RemainsTime { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Percentage", Visible = false)]
    public int? Percentage { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Result", Visible = false)]
    public string Result { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Processing Error Message", Visible = false)]
    public string ProcessingErrorMessage { get; set; }
  }
}
