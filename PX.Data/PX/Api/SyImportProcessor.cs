// Decompiled with JetBrains decompiler
// Type: PX.Api.SyImportProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api.Mobile;
using PX.Api.Services;
using PX.Common;
using PX.Common.Extensions;
using PX.Common.Parser;
using PX.Data;
using PX.Data.Api.Export.SyImport;
using PX.Data.Api.Mobile;
using PX.Data.Api.Mobile.SignManager;
using PX.Data.Automation;
using PX.Data.Descriptor.Action;
using PX.Data.Maintenance.GI;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Monads;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

internal static class SyImportProcessor
{
  private const string SaveActionName = "Save";

  internal static bool IsFilterView(this PXGraph graph, string viewName)
  {
    PXCache cache = graph.Views[viewName].Cache;
    return cache.Keys.Count == 0 && graph.Defaults != null && graph.Defaults.ContainsKey(cache.GetItemType());
  }

  internal static object GetValueOrStateExt(
    this PXGraph graph,
    string viewName,
    object data,
    string fieldName,
    bool forceState)
  {
    return forceState ? graph.GetStateExt(viewName, data, fieldName) : graph.GetValueExt(viewName, data, fieldName);
  }

  internal static bool TryGetValue(this OrderedDictionary dictionary, object key, out object value)
  {
    if (dictionary.Contains(key))
    {
      value = dictionary[key];
      return true;
    }
    value = (object) null;
    return false;
  }

  internal static bool RowKeysEqual(
    Dictionary<string, object> dict1,
    Dictionary<string, object> dict2)
  {
    if (dict1 == null || dict1.Count == 0 || dict2 == null || dict2.Count == 0)
      return false;
    if (dict1 == dict2)
      return true;
    foreach (KeyValuePair<string, object> keyValuePair in dict1)
    {
      object objB;
      if (!dict2.TryGetValue(keyValuePair.Key, out objB) || !object.Equals(keyValuePair.Value, objB))
        return false;
    }
    return true;
  }

  internal static PXSYTablePr ExportTable(
    SyExportContext context,
    bool submit,
    IGraphHelper graphHelper)
  {
    return SyImportProcessor.ExportTable(context, submit, graphHelper, CancellationToken.None);
  }

  internal static PXSYTablePr ExportTable(
    SyExportContext context,
    bool submit,
    IGraphHelper graphHelper,
    CancellationToken token)
  {
    return new SyImportProcessor.ExportTableHelper(context, submit)
    {
      GraphHelper = graphHelper
    }.ExportTable(token);
  }

  public static PXSYTablePr ExportTable(SyExportContext context, bool submit = false)
  {
    return SyImportProcessor.ExportTable(context, CancellationToken.None);
  }

  public static PXSYTablePr ExportTable(
    SyExportContext context,
    CancellationToken token,
    bool submit = false)
  {
    return new SyImportProcessor.ExportTableHelper(context, submit).ExportTable(token);
  }

  public static void FillPrepareResultKeys(SyExportContext context, PXSYTablePr prepareResults)
  {
    if (string.IsNullOrEmpty(context.PrimaryView))
      return;
    PXGraph graph = context.Graph ?? SyImportProcessor.CreateGraph(context.GraphName, context.ScreenID);
    bool isGI;
    PXCache keysSourceCache = SyMappingUtils.GetKeysSourceCache(context.PrimaryView, context.GridView, graph, out isGI);
    for (int index = 0; index < prepareResults.Rows.Count; ++index)
    {
      PXSYRow row = prepareResults.Rows[index];
      object primaryViewRow = prepareResults.PrimaryViewRows[index];
      if (row.Keys == null && keysSourceCache.Keys.Count > 0)
        row.Keys = new Dictionary<string, string>();
      foreach (string key in (IEnumerable<string>) keysSourceCache.Keys)
      {
        string keyField = key;
        object val = !isGI ? keysSourceCache.GetValue(primaryViewRow, keyField) : ((IEnumerable<PXSYItem>) row.Items).Where<PXSYItem>((Func<PXSYItem, bool>) (item => item != null && item.State != null && item.State.Name.OrdinalEquals(keyField))).Select<PXSYItem, object>((Func<PXSYItem, object>) (ri => ri.NativeValue)).FirstOrDefault<object>();
        string str = val as string;
        row.Keys[keyField] = str ?? keysSourceCache.ValueToString(keyField, val);
      }
    }
  }

  public static void FillImportResultKeys(SyImportContext context)
  {
    if (string.IsNullOrEmpty(context.PrimaryView))
      return;
    PXCache cache = (context.Graph ?? SyImportProcessor.CreateGraph(context.GraphName)).Views[context.PrimaryView].Cache;
    foreach (SyImportRowResult syImportRowResult in ((IEnumerable<SyImportRowResult>) context.ImportResult).Where<SyImportRowResult>((Func<SyImportRowResult, bool>) (r => r.IsFilled && r.IsPersisted && r.PersistedRow != null)))
    {
      foreach (string key in (IEnumerable<string>) cache.Keys)
      {
        if (syImportRowResult.Keys == null)
          syImportRowResult.Keys = new Dictionary<string, string>();
        object val = cache.GetValue(syImportRowResult.PersistedRow, key);
        syImportRowResult.Keys[key] = cache.ValueToString(key, val);
      }
    }
  }

  public static void ImportTable(SyImportContext context)
  {
    SyImportProcessor.ImportTable(context, CancellationToken.None);
  }

  public static void ImportTable(SyImportContext context, CancellationToken token)
  {
    SyImportProcessor.SyStep step = new SyImportProcessor.SyStep(context.Graph, new SyFormulaProcessor(), context.PrimaryView);
    step.PrimaryDataView = context.PrimaryDataView;
    object errorItem = (object) null;
    bool flag1 = false;
    List<PXErrorInfo> nonUIErrors = new List<PXErrorInfo>();
    PXFilterRow[] targetConditions = SyImportProcessor.SyStep.DemaskFilters(context.Graph, context.PrimaryView, context.TargetConditions);
    context.Graph.RowPersisted.AddHandler(context.PrimaryDataView, (PXRowPersisted) ((cache, e) => SyImportProcessor.OnPersist(context, step, errorItem, e)));
    PXGraph.InstanceCreatedDelegate del = (PXGraph.InstanceCreatedDelegate) (g => g.RowPersisted.AddHandler(context.PrimaryDataView, (PXRowPersisted) ((cache, e) => SyImportProcessor.OnPersist(context, step, errorItem, e))));
    System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(context.Graph);
    PXGraph.InstanceCreated.AddHandler(typeNotCustomized, del);
    int? branchId1 = PXContext.GetBranchID();
    try
    {
      int num = context.Commands.Length - 1;
      for (context.RowIndex = context.StartRow; context.RowIndex <= context.EndRow; ++context.RowIndex)
      {
        if (!token.IsCancellationRequested)
        {
          context.ClearAndShrinkCachesIfNeeded();
          SyImportProcessor.SyExternalValues externalValues = new SyImportProcessor.SyExternalValues(context);
          SyImportRowResult rowImportResult = context.ImportResult[context.RowIndex];
          SyImportProcessor.SaveLineNumber(context.RowIndex + 1);
          try
          {
            bool hasExceptions = false;
            bool flag2 = false;
            step = new SyImportProcessor.SyStep(step, externalValues, rowImportResult);
            for (int index = 0; index <= num; ++index)
            {
              SyCommand command1 = context.Commands[index];
              if (SyImportProcessor.ValidCurrentExecutionBehavior(context, command1.ExecutionBehavior))
              {
                bool flag3 = index == num;
                bool flag4 = false;
                if (flag3 && command1.CommandType == SyCommandType.Action && command1.Commit && "Save".Equals(command1.Field, StringComparison.Ordinal) && !context.BreakOnError)
                  flag4 = true;
                if (flag3 && context.IsLastRow())
                  command1.Commit = true;
                bool needCommit;
                step = step.ProcessCommand(command1, out needCommit);
                if (needCommit)
                {
                  step.CommitWithoutExceptions(ref hasExceptions, ref errorItem, targetConditions, context);
                  bool flag5 = false;
                  bool flag6 = false;
                  if (!flag3)
                  {
                    SyCommand command2 = context.Commands[index + 1];
                    flag5 = !command2.ViewAlias.OrdinalEquals(command1.ViewAlias);
                    flag6 = !command2.View.OrdinalEquals(command1.View);
                    if (flag6)
                      SyImportProcessor.ApplyWorkflow(context.Graph, command1.View);
                  }
                  else if (((!hasExceptions ? 0 : (errorItem != null ? 1 : 0)) & (flag4 ? 1 : 0)) != 0)
                  {
                    errorItem = (object) null;
                    flag1 = true;
                  }
                  step = new SyImportProcessor.SyStep(step, externalValues, rowImportResult);
                  if (hasExceptions && (context.BreakOnError || (flag2 = rowImportResult.Error != null) && (flag6 | flag5 || rowImportResult.Error is ExpressionException)))
                    break;
                }
              }
            }
            if (context.BatchSize > 0 && context.IsEndOfBatch())
            {
              SyImportProcessor.SyStep syStep = step;
              SyCommand cmd = new SyCommand();
              cmd.Commit = true;
              cmd.View = step.PrimaryView;
              cmd.CommandType = SyCommandType.Action;
              cmd.Field = "Save";
              bool flag7;
              ref bool local = ref flag7;
              step = syStep.ProcessCommand(cmd, out local);
              step.CommitWithoutExceptions(ref hasExceptions, ref errorItem, targetConditions, context);
            }
            if (hasExceptions)
            {
              if (!context.BreakOnError)
              {
                if (flag2)
                  context.Graph.Clear(PXClearOption.ClearAll);
              }
              else
                break;
            }
            else
            {
              if (!context.IsFilter || !context.IsPrimaryView(context.PrimaryDataView))
              {
                if (!rowImportResult.NoteChanged)
                  goto label_38;
              }
              rowImportResult.PersistedRow = step.PrimaryItem;
              rowImportResult.IsPersisted = true;
              rowImportResult.IsFilled = true;
            }
          }
          catch (SyImportProcessor.InvalidTargetException ex)
          {
            rowImportResult.Error = (Exception) ex;
            errorItem = (object) rowImportResult.ExternalKeys;
            if (!context.BreakOnTarget)
            {
              foreach (SyImportProcessor.SyStep.SyView syView in step.Views.Values)
                syView.Current = (object) null;
              step.HeldPrimaryItem();
            }
            else
              break;
          }
          catch (Exception ex)
          {
            if (!(ex is PXSetPropertyException))
              nonUIErrors.Add(new PXErrorInfo(ex.Message, ex.StackTrace));
            rowImportResult.Error = ex;
            errorItem = (object) rowImportResult.ExternalKeys;
            if (!context.BreakOnError)
            {
              step.HeldPrimaryItem();
              context.Graph.Clear(PXClearOption.ClearAll);
            }
            else
              break;
          }
label_38:
          if (context.IsSetup)
            step.ProcessThisAndPreviousSteps((System.Action<SyImportProcessor.SyStep>) (s => s.Action = (string) null));
        }
        else
          break;
      }
    }
    finally
    {
      PXGraph.InstanceCreated.RemoveHandler(del, typeNotCustomized);
      int? nullable = branchId1;
      int? branchId2 = PXContext.GetBranchID();
      if (!(nullable.GetValueOrDefault() == branchId2.GetValueOrDefault() & nullable.HasValue == branchId2.HasValue))
      {
        PXContext.SetBranchID(branchId1);
        context.Graph.Accessinfo.BranchID = branchId1;
      }
    }
    if (errorItem != null | flag1)
    {
      context.PropogateErrorsUp();
      ImportGraphNavigator.StoreGraph(context.Graph, nonUIErrors);
    }
    else
    {
      if (context.ValidationType != SYValidation.SaveIfValid)
        return;
      context.ValidationType = SYValidation.None;
      SyImportProcessor.ImportTable(context, token);
    }
  }

  private static bool ValidCurrentExecutionBehavior(
    SyImportContext context,
    ExecutionBehavior? behavior)
  {
    if (behavior.HasValue)
    {
      ExecutionBehavior? nullable = behavior;
      ExecutionBehavior executionBehavior1 = ExecutionBehavior.ForEachRecord;
      if (!(nullable.GetValueOrDefault() == executionBehavior1 & nullable.HasValue))
      {
        SyImportRowResult syImportRowResult1 = context.ImportResult[context.RowIndex];
        nullable = behavior;
        ExecutionBehavior executionBehavior2 = ExecutionBehavior.FirstRecordOnly;
        if (nullable.GetValueOrDefault() == executionBehavior2 & nullable.HasValue)
        {
          if (context.IsFirstRow())
            return true;
          SyImportRowResult syImportRowResult2 = context.ImportResult[context.RowIndex - 1];
          return !SyImportProcessor.RowKeysEqual(syImportRowResult1.ExternalKeys, syImportRowResult2.ExternalKeys);
        }
        if (context.IsLastRow())
          return true;
        SyImportRowResult syImportRowResult3 = context.ImportResult[context.RowIndex + 1];
        return !SyImportProcessor.RowKeysEqual(syImportRowResult1.ExternalKeys, syImportRowResult3.ExternalKeys);
      }
    }
    return true;
  }

  /// <summary>Saves current row index to slot (used on LineNbr function evaluation).</summary>
  /// <param name="lineNbr">Row index to store (the first row index is 1).</param>
  internal static void SaveLineNumber(int lineNbr) => PXContext.SetSlot<int>("LineNbr", lineNbr);

  private static void ApplyWorkflow(PXGraph graph, string viewName)
  {
    if (!graph.PrimaryView.OrdinalEquals(viewName))
      return;
    object current = graph.Views[viewName].Cache.Current;
    graph.ApplyWorkflowState(current);
  }

  public static PXGraph CreateGraph(
    string graphName,
    string screenID = null,
    bool export = false,
    bool contractBasedAPI = false)
  {
    return SyImportProcessor.CreateGraphWithPrefix(graphName, (string) null, screenID, export, contractBasedAPI);
  }

  public static PXGraph CreateGraphWithPrefix(
    string graphName,
    string prefix,
    string screenID = null,
    bool export = false,
    bool contractBasedAPI = false)
  {
    bool flag = false;
    PXGraph graphWithPrefix = (PXGraph) null;
    System.Type graphType = SyImportProcessor.GetGraphType(graphName);
    if (graphType != (System.Type) null)
    {
      using (new SyScope(export, contractBasedAPI))
      {
        flag = graphType == typeof (PXGenericInqGrph) && !string.IsNullOrEmpty(screenID);
        graphWithPrefix = flag ? (PXGraph) PXGenericInqGrph.CreateInstance(screenID, prefix) : PXGraph.CreateInstance(graphType, prefix ?? "");
      }
    }
    if (graphWithPrefix == null)
      throw new PXException("A graph cannot be created.");
    if (!flag)
      graphWithPrefix.Culture = Thread.CurrentThread.CurrentCulture;
    graphWithPrefix.UnattendedMode = false;
    return graphWithPrefix;
  }

  public static System.Type GetGraphType(string graphName)
  {
    System.Type type1 = PXBuildManager.GetType(graphName, false);
    if ((object) type1 == null)
      type1 = System.Type.GetType(graphName);
    System.Type t = type1;
    if (t != (System.Type) null)
    {
      System.Type type2 = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(t), false);
      if ((object) type2 == null)
        type2 = t;
      t = type2;
    }
    return t;
  }

  private static void OnPersist(
    SyImportContext context,
    SyImportProcessor.SyStep step,
    object errorItem,
    PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Open)
    {
      step.FillImportResults(context, (SyImportProcessor.SyStep.FillImportResult) ((_, result) =>
      {
        result.IsPersisted = e.TranStatus == PXTranStatus.Completed;
        result.PersistedRow = e.Row;
        result.PersistingError = e.Exception;
        result.IsFilled = true;
      }));
    }
    else
    {
      if (context.ValidationType != SYValidation.None)
        PXTransactionScope.ForceRollbackParentTransaction();
      if (step.CheckShouldBeBypassed(errorItem))
        throw new PXException("An attempt to commit a bypassed row");
      if (context.RowPersisting == null)
        return;
      step.FillImportResults(context, (SyImportProcessor.SyStep.FillImportResult) ((rowIndex, _) => context.RowPersisting(rowIndex)));
    }
  }

  internal class ExportTableHelper
  {
    private readonly SyExportContext Context;
    private readonly PXGraph _graph;
    private readonly IGrowingTable _growingTable;
    private readonly SyFormulaProcessor _formulaProcessor = new SyFormulaProcessor();
    private SyImportProcessor.SyStep _step;
    private readonly PXSYTableBuilder _exportedTable;
    private PXDummySYTableBuilder _dummyTable;
    private readonly Dictionary<IBqlTable, SyImportProcessor.ExportTableHelper.NameValueCollection> _documentRows;
    private readonly List<SyImportProcessor.ExportTableHelper.ExportedLine> _documentLines;
    private IBqlTable _lastPrimaryRow;
    private bool _forceFlush;
    private bool _filterAsPrimary;
    [Obsolete("Don't use, use GraphHelper instead")]
    private IGraphHelper _graphHelper;

    public IGraphHelper GraphHelper
    {
      set
      {
        this._graphHelper = this._graphHelper == null ? value : throw new InvalidOperationException("GraphHelper can be set only once");
      }
      private get
      {
        return this._graphHelper ?? (this._graphHelper = (IGraphHelper) new SyImportProcessor.ExportTableHelper.DefaultGraphHelper());
      }
    }

    public ExportTableHelper(SyExportContext context, bool submit = false)
    {
      this.Context = context;
      using (new PXPreserveScope())
        this._graph = context.Graph ?? SyImportProcessor.CreateGraph(this.Context.GraphName, this.Context.ScreenID, true);
      this._growingTable = GrowingTableBuilder.Create(this._graph, context);
      this._filterAsPrimary = this._graph.Views[context.PrimaryView].Cache.Keys.Count == 0;
      this._exportedTable = new PXSYTableBuilder(this.Context.ProviderFields, context.Locale);
      this._dummyTable = new PXDummySYTableBuilder();
      if (this._graph is PXGenericInqGrph)
      {
        PXCache cache = this._graph.Views[context.PrimaryView].Cache;
        if (cache.GetItemType() == typeof (GenericFilter) && cache.Current is GenericFilter current && !current.Values.Any<KeyValuePair<string, object>>())
        {
          foreach (string field in (List<string>) cache.Fields)
          {
            cache.SetDefaultExt((object) current, field);
            if (PXFieldState.UnwrapValue(cache.GetValueExt((object) current, field)) is string str && RelativeDatesManager.IsRelativeDatesString(str))
            {
              string asString = RelativeDatesManager.EvaluateAsString(str);
              cache.SetValueExt((object) current, field, (object) asString);
            }
          }
        }
        foreach (SyCommand command in context.Commands)
        {
          if (command.CommandType == SyCommandType.Action)
            command.View = command.ViewAlias = "Filter";
        }
      }
      this._documentRows = new Dictionary<IBqlTable, SyImportProcessor.ExportTableHelper.NameValueCollection>();
      this._documentLines = new List<SyImportProcessor.ExportTableHelper.ExportedLine>();
      this._step = new SyImportProcessor.SyStep(this._graph, this._formulaProcessor, context.PrimaryView, !submit);
      if (!this._graph.Defaults.ContainsKey(this._graph.GetItemType(context.PrimaryView)))
        return;
      this._graph.RowUpdated.AddHandler(context.PrimaryView, (PXRowUpdated) ((sender, e) => this._forceFlush = true));
    }

    protected void UnloadGraph(PXGraph graph) => this.GraphHelper.UnloadGraph(graph);

    protected PXGraph CreateGraphForRedirect(string screenId, string graphType)
    {
      return this.GraphHelper.CreateGraphForRedirect(screenId, graphType);
    }

    public PXSYTablePr ExportTable() => this.ExportTable(CancellationToken.None);

    public PXSYTablePr ExportTable(CancellationToken token)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      try
      {
        this._graph.IsExport = true;
        System.Type itemType = this._graph.Views[this.Context.PrimaryView].GetItemType();
        PXFilterRow[] array;
        foreach (string str in this.Context.ViewFilters.Keys.ToArray<string>())
        {
          PXFilterRow[] pxFilterRowArray;
          if (this.Context.ViewFilters.TryGetValue(str, out pxFilterRowArray))
          {
            this.Context.ViewFilters[str] = SyImportProcessor.SyStep.DemaskFilters(this._graph, str, pxFilterRowArray);
            if (this._graph.Views[ScreenUtils.NormalizeViewName(str)].GetItemType() == itemType && !this.Context.IsPrimaryView(str))
            {
              if (this.Context.ViewFilters.TryGetValue(this.Context.PrimaryView, out array))
              {
                Array.Resize<PXFilterRow>(ref array, array.Length + pxFilterRowArray.Length);
                Array.Copy((Array) pxFilterRowArray, 0, (Array) array, array.Length - pxFilterRowArray.Length, pxFilterRowArray.Length);
                this.Context.ViewFilters[this.Context.PrimaryView] = array;
              }
              else
                this.Context.ViewFilters[this.Context.PrimaryView] = this.Context.ViewFilters[str];
              this.Context.ViewFilters.Remove(str);
            }
          }
        }
        this.Context.ViewFilters.TryGetValue(this.Context.PrimaryView, out array);
        bool flag1 = false;
        string str1 = (string) null;
        if (this.Context.TimeRange.HasValue)
        {
          str1 = this.Context.NewOnly ? "CreatedDateTime" : "LastModifiedDateTime";
          if (this._graph.Views[this.Context.PrimaryView].Cache.Fields.Contains(str1))
          {
            if (array != null && array.Length != 0)
            {
              ++array[0].OpenBrackets;
              ++array[array.Length - 1].CloseBrackets;
              Array.Resize<PXFilterRow>(ref array, array.Length + 2);
            }
            else
              this.Context.ViewFilters[this.Context.PrimaryView] = array = new PXFilterRow[2];
            flag1 = this._graph.Views[this.Context.PrimaryView].Cache.GetAttributes((object) null, str1).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (_ => _ is PXDBCreatedDateTimeAttribute && ((PXDBDateAttribute) _).UseTimeZone));
            if (flag1 && this.Context.TimeRangeUtc.HasValue)
            {
              array[array.Length - 2] = new PXFilterRow(str1, PXCondition.GT, (object) this.Context.TimeRangeUtc.Value.Key);
              array[array.Length - 1] = new PXFilterRow(str1, PXCondition.LE, (object) this.Context.TimeRangeUtc.Value.Value);
            }
            else
            {
              array[array.Length - 2] = new PXFilterRow(str1, PXCondition.GT, (object) this.Context.TimeRange.Value.Key);
              array[array.Length - 1] = new PXFilterRow(str1, PXCondition.LE, (object) this.Context.TimeRange.Value.Value);
            }
            this.Context.ViewFilters[this.Context.PrimaryView] = array;
          }
        }
        object itemToBypass = (object) null;
        SyCommand syCommand = ((IEnumerable<SyCommand>) this.Context.Commands).LastOrDefault<SyCommand>((Func<SyCommand, bool>) (cmd => cmd.CommandType != SyCommandType.ExportField && cmd.CommandType != SyCommandType.ExportPath && cmd.Formula != "RowNumber_" + cmd.View));
        Dictionary<string, SyCommand> dictionary = new Dictionary<string, SyCommand>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        HashSet<string> stringSet = new HashSet<string>();
        bool? nullable1 = new bool?();
        bool keepNullValues = this.Context.NewApi || this._graph.IsMobile || this._graph.IsCopyPasteContext;
        foreach (SyCommand command in this.Context.Commands)
        {
          if ((command.CommandType != SyCommandType.RowNumber || command.Formula != "RowNumber_" + command.View) && (!dictionary.ContainsKey(command.ViewAlias) || !dictionary[command.ViewAlias].Commit && dictionary[command.ViewAlias].CommandType == SyCommandType.Field && command.CommandType != SyCommandType.Field))
            dictionary[command.ViewAlias] = command;
          if ((command.CommandType == SyCommandType.Path || command.CommandType == SyCommandType.ExportPath) && !string.IsNullOrEmpty(command.View) && !stringSet.Contains(command.View))
            stringSet.Add(command.View);
          if (command.CommandType == SyCommandType.ExportField && (command.Formula.OrdinalEquals("NoteText") || command.Formula.OrdinalEquals("NoteFiles") || command.Formula.OrdinalEquals("NotePopupText")) && this._graph.Views.ContainsKey(ScreenUtils.NormalizeViewName(command.View)))
          {
            this._graph.SetValueExt(ScreenUtils.NormalizeViewName(command.View), (object) null, "NoteText", (object) null);
            this._graph.SetValueExt(ScreenUtils.NormalizeViewName(command.View), (object) null, "NoteFiles", (object) null);
            this._graph.SetValueExt(ScreenUtils.NormalizeViewName(command.View), (object) null, "NotePopupText", (object) null);
          }
        }
        int lineNbr = 1;
        SyImportProcessor.SaveLineNumber(lineNbr);
        SyImportProcessor.ExportTableHelper.ExportedLine currentLine;
        while (true)
        {
          token.ThrowIfCancellationRequested();
          SyImportProcessor.SyExternalValues syExternalValues = new SyImportProcessor.SyExternalValues(this._graph);
          Dictionary<string, SyImportProcessor.ExportTableHelper.NameValueCollection> collectedViews = new Dictionary<string, SyImportProcessor.ExportTableHelper.NameValueCollection>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          currentLine = new SyImportProcessor.ExportTableHelper.ExportedLine();
          this._step = new SyImportProcessor.SyStep(this._step, syExternalValues);
          object obj1 = (object) null;
          foreach (SyCommand command in this.Context.Commands)
          {
            token.ThrowIfCancellationRequested();
            if ((!this.Context.NewApi || !SyImportProcessor.ExportTableHelper.IsUnavailableActionCommand(this._graph, command, "Save", true)) && !SyImportProcessor.ExportTableHelper.IsUnavailableActionCommand(this._graph, command, "Cancel", false))
            {
              bool isPrimaryView = this.Context.IsPrimaryView(command.View);
              if (!isPrimaryView && (string.IsNullOrEmpty(command.View) || !stringSet.Contains(command.View)))
              {
                if (itemType == typeof (GenericFilter) || this._graph.IsFilterView(this.Context.PrimaryView))
                  this.EnsurePrimaryViewRow(array);
                else
                  this.EnsurePrimaryViewRow(array, this.Context.StartRow);
                if (this._growingTable.IsEmptyViewResults(this.Context.PrimaryView))
                {
                  currentLine = (SyImportProcessor.ExportTableHelper.ExportedLine) null;
                  break;
                }
              }
              if (!nullable1.HasValue && (command.CommandType == SyCommandType.ExportPath || command.CommandType == SyCommandType.EnumFieldValues))
                nullable1 = new bool?(true);
              int? nullable2 = new int?();
              PXFilterRow[] pxFilterRowArray1 = new PXFilterRow[0];
              if (command.CommandType == SyCommandType.ExportField || command.CommandType == SyCommandType.ExportPath || command.CommandType == SyCommandType.EnumFieldValues || command.CommandType == SyCommandType.Field && dictionary.ContainsKey(command.ViewAlias) && dictionary[command.ViewAlias] == command)
              {
                this.Context.ViewFilters.TryGetValue(command.View, out pxFilterRowArray1);
                if (flag1)
                  PXDBDateAttribute.SetUseTimeZone(this._graph.Views[this.Context.PrimaryView].Cache, str1, false);
                nullable2 = this.EmitExportedRows(this._step, this._growingTable, command, pxFilterRowArray1, this.Context.TopCount);
                if (flag1)
                  PXDBDateAttribute.SetUseTimeZone(this._graph.Views[this.Context.PrimaryView].Cache, str1, true);
              }
              int? nullable3 = nullable2;
              int num1 = 0;
              int num2;
              if (!SyImportProcessor.ExportTableHelper.IsRowAborted(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue, isPrimaryView, command.CommandType))
              {
                nullable3 = nullable2;
                int num3 = 0;
                num2 = !(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue) || pxFilterRowArray1 == null ? 0 : (((IEnumerable<PXFilterRow>) pxFilterRowArray1).Any<PXFilterRow>((Func<PXFilterRow, bool>) (f => f.RequireRowForPrimary)) ? 1 : 0);
              }
              else
                num2 = 1;
              if (num2 != 0)
              {
                currentLine = (SyImportProcessor.ExportTableHelper.ExportedLine) null;
                PXCache cache = this._graph.Views[this.Context.PrimaryView].Cache;
                if (cache.Keys.Count > 0 && command.CommandType == SyCommandType.EnumFieldValues && string.Equals(cache.Keys.LastOrDefault<string>(), command.Field, StringComparison.InvariantCultureIgnoreCase) && cache.Current != null && cache.GetStatus(cache.Current) == PXEntryStatus.Inserted)
                {
                  this._graph.Clear();
                  break;
                }
                break;
              }
              nullable3 = this._growingTable.CountResultsForView(command.ViewAlias);
              if (nullable3.HasValue)
              {
                object obj2 = this._growingTable.GetNativeRow(command.ViewAlias).Result;
                if (this.Context.IsPrimaryView(command.ViewAlias) && obj1 != null)
                  obj2 = obj1;
                if (currentLine.Currents.ContainsKey(command.View) && currentLine.Currents[command.View] != obj2)
                  currentLine.Currents = new Dictionary<string, object>((IDictionary<string, object>) currentLine.Currents, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
                currentLine.Currents[command.View] = obj2;
              }
              if (command.CommandType == SyCommandType.ExportField || command.CommandType == SyCommandType.ExportPath)
              {
                this.ProcessExportFieldCommand(this._step, command, currentLine, collectedViews);
              }
              else
              {
                this._growingTable.ExportValues(syExternalValues);
                syExternalValues.Results = currentLine.Currents;
                try
                {
                  syExternalValues.Results = currentLine.Currents;
                  if (command == syCommand)
                    command.Commit = true;
                  bool? nullable4 = nullable1;
                  bool flag2 = false;
                  if (nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue && command.Commit)
                  {
                    if (command.CommandType == SyCommandType.Action)
                      continue;
                  }
                  bool needCommit;
                  this._step = this._step.ProcessCommand(command, out needCommit);
                  if (command.CommandType == SyCommandType.CustomDelegate && command.View == this.Context.PrimaryView)
                    this.EmitExportedRows(this._step, this._growingTable, command, pxFilterRowArray1, this.Context.TopCount);
                  if (needCommit)
                  {
                    PXFilterRow[] pxFilterRowArray2 = (PXFilterRow[]) null;
                    if (command.CommandType == SyCommandType.Action && !this.Context.ViewFilters.TryGetValue(command.View, out pxFilterRowArray2))
                    {
                      PXGraph graph = this.Context.Graph;
                      if ((graph != null ? (graph.IsMobile ? 1 : 0) : 0) != 0)
                      {
                        foreach (string key in this.Context.ViewFilters.Keys)
                        {
                          string processAllActionKey;
                          string processingFilterViewName;
                          if (this.IsFilteredProcessingView(this.Context.Graph, this.Context.GraphName, key, out processAllActionKey, out processingFilterViewName) && processingFilterViewName.OrdinalEquals(command.View) && processAllActionKey.OrdinalEquals(command.Field))
                            this.Context.ViewFilters.TryGetValue(key, out pxFilterRowArray2);
                        }
                      }
                    }
                    this._step.CommitChanges(ref itemToBypass, (PXFilterRow[]) null, pxFilterRowArray2);
                    if (pxFilterRowArray2 != null)
                      this.VerifyFiltersValues(command.View, pxFilterRowArray2);
                    if (SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) && this._step.PrimaryItem != null)
                      itemToBypass = this._step.PrimaryItem;
                    if (this._step.PrimaryResult != null)
                      obj1 = this._step.PrimaryResult;
                    this._step = new SyImportProcessor.SyStep(this._step, syExternalValues);
                  }
                }
                catch (Exception ex1)
                {
                  Exception innerException = ex1;
                  if (innerException is PXRedirectToUrlException redirectToUrlException)
                  {
                    if (!redirectToUrlException.Url.StartsWith("~/pages"))
                      throw innerException;
                    PXSiteMapNode siteMapNode = PXSiteMap.Provider.FindSiteMapNode(redirectToUrlException.Url);
                    string graphType = siteMapNode != null ? siteMapNode.GraphType : throw innerException;
                    PXGraph graphForRedirect;
                    using (new PXPreserveScope())
                    {
                      graphForRedirect = this.CreateGraphForRedirect(siteMapNode.ScreenID, graphType);
                      graphForRedirect.Load();
                    }
                    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(graphForRedirect, string.Empty);
                    requiredException.ScreenId = siteMapNode.ScreenID;
                    graphForRedirect.Unload();
                    this.Context.Error = (Exception) requiredException;
                    this._graph.IsExport = false;
                    return this._exportedTable.GetTable(keepNullValues);
                  }
                  if (innerException is PXRedirectRequiredException)
                  {
                    PXGraph graph = ((PXRedirectRequiredException) innerException).Graph;
                    graph.UnattendedMode = false;
                    graph.FullTrust = false;
                    this.UnloadGraph(graph);
                    this.Context.Error = innerException;
                    this._graph.IsExport = false;
                    return this._exportedTable.GetTable(keepNullValues);
                  }
                  if (innerException is PXPopupRedirectException)
                  {
                    PXGraph graph = ((PXPopupRedirectException) innerException).Graph;
                    graph.UnattendedMode = false;
                    graph.FullTrust = false;
                    this.UnloadGraph(graph);
                    this.Context.Error = innerException;
                    this._graph.IsExport = false;
                    return this._exportedTable.GetTable(keepNullValues);
                  }
                  if (innerException is PXDialogRequiredException ex2)
                  {
                    if (this._graph.IsMobile && ex2.UseAskDialog())
                      throw;
                    if (ex2.Buttons == MessageButtons.None)
                    {
                      this.Context.Error = innerException;
                      this._graph.IsExport = false;
                      return this._exportedTable.GetTable(keepNullValues);
                    }
                    innerException = (Exception) new PXException(innerException.Message);
                  }
                  if (innerException is PXSignatureRequiredException && this._graph.IsMobile)
                    throw;
                  if (innerException is EntitySetPropertyException)
                    throw;
                  itemToBypass = this._step.PrimaryItem ?? SyImportProcessor.SyStep.anyBypass;
                  if (this.Context.BreakOnError)
                  {
                    this.Context.Error = !(innerException is PXOuterException exception) ? innerException : (Exception) new PXException(exception.GetFullMessage(" "), innerException);
                    this._graph.IsExport = false;
                    return this._exportedTable.GetTable(keepNullValues);
                  }
                  if (this.Context.NewApi)
                    this.Context.Error = innerException;
                  if (this.Context.NewApi)
                  {
                    if (command.CommandType != SyCommandType.Action)
                      break;
                  }
                  else
                    break;
                }
              }
            }
          }
          if (!nullable1.HasValue)
            nullable1 = new bool?(false);
          if (currentLine != null)
          {
            this._documentLines.Add(currentLine);
            ++lineNbr;
            SyImportProcessor.SaveLineNumber(lineNbr);
          }
          if (!this._growingTable.IsLastRow() && !this.Context.OneRun)
            this._growingTable.MoveNextRow();
          else
            break;
        }
        this.FlushDocument((IEnumerable<SyImportProcessor.ExportTableHelper.ExportedLine>) this._documentLines, currentLine != null);
        this._graph.IsExport = false;
        return this._exportedTable.GetTable(keepNullValues);
      }
      finally
      {
        stopwatch.Stop();
        PXTrace.WriteVerbose("Export elapsed {Elapsed}", (object) stopwatch.ElapsedMilliseconds);
      }
    }

    private void VerifyFiltersValues(string viewName, PXFilterRow[] viewFilters)
    {
      foreach (PXFilterRow viewFilter in viewFilters)
      {
        if (this._graph.IsContractBasedAPI && viewFilter.OrigValue is string origValue && viewFilter.Value is string str)
        {
          if (!str.OrdinalStartsWith(origValue))
          {
            PXStringState stateExt = this._graph.GetStateExt(viewName, (object) null, viewFilter.DataField) as PXStringState;
            if (!string.IsNullOrEmpty(stateExt.InputMask) || !str.OrdinalIgnoreCaseEquals(origValue.TrimEnd()))
              throw new EntitySetPropertyException(viewName, viewFilter.DataField, "The provided value '{0}' does not match the required input mask '{1}'.", new object[2]
              {
                (object) origValue,
                (object) stateExt.InputMask
              });
          }
        }
      }
    }

    private bool IsFilteredProcessingView(
      PXGraph graph,
      string graphName,
      string viewName,
      out string processAllActionKey,
      out string processingFilterViewName)
    {
      processingFilterViewName = (string) null;
      processAllActionKey = (string) null;
      System.Type graphType = SyImportProcessor.GetGraphType(graphName);
      if (graphType == (System.Type) null)
        return false;
      System.Reflection.FieldInfo field = graphType.GetField(viewName);
      if (field == (System.Reflection.FieldInfo) null || !typeof (IPXFilteredProcessing).IsAssignableFrom(field.FieldType))
        return false;
      PropertyInfo property1 = field.FieldType.GetProperty("ProcessAllActionKey");
      if (property1 == (PropertyInfo) null)
        return false;
      PropertyInfo property2 = field.FieldType.GetProperty("ViewName");
      if (property2 == (PropertyInfo) null)
        return false;
      object obj = field.GetValue((object) graph);
      if (obj == null)
        return false;
      processAllActionKey = property1.GetValue(obj).ToString();
      processingFilterViewName = property2.GetValue(obj).ToString();
      return true;
    }

    private void EnsurePrimaryViewRow(PXFilterRow[] filters, int startRow = 0)
    {
      string primaryView = this.Context.PrimaryView;
      if (this._growingTable.CountResultsForView(primaryView).HasValue)
        return;
      SyImportProcessor.SyStep step = this._step;
      IEnumerable enumerable = this._step.SelectRows(primaryView, filters, this.Context.TopCount, this._growingTable.HasEnumValues, startRow, this.Context);
      ViewSelectResults selectResults = new ViewSelectResults(primaryView);
      int v = 0;
      foreach (object result in enumerable)
      {
        selectResults.AddRow(result);
        selectResults.AddCell(SyExportContext.GetFieldID(primaryView), (object) v);
        ++v;
      }
      this._growingTable.AddSelectResults(selectResults);
    }

    private void ProcessExportFieldCommand(
      SyImportProcessor.SyStep step,
      SyCommand cmd,
      SyImportProcessor.ExportTableHelper.ExportedLine currentLine,
      Dictionary<string, SyImportProcessor.ExportTableHelper.NameValueCollection> collectedViews)
    {
      if (!currentLine.Views.ContainsKey(cmd.View))
      {
        bool isPrimary = this.Context.IsPrimaryView(cmd.View);
        NativeRowWrapper nativeRow = this._growingTable.GetNativeRow(cmd.ViewAlias);
        IBqlTable bqlTable = nativeRow?.BqlTable;
        if (bqlTable == null)
        {
          currentLine.Views.Add(cmd.View, new SyImportProcessor.ExportTableHelper.ViewExportedValues(cmd.View, nativeRow, isPrimary, new SyImportProcessor.ExportTableHelper.NameValueCollection()));
          return;
        }
        System.Type type = bqlTable.GetType();
        if (!isPrimary && (this._graph.PrimaryItemType != type || this._graph.Caches[type].Current != bqlTable))
        {
          this._graph.Caches[type].Current = (object) bqlTable;
          this._graph.Caches[type].TrimItemAttributes((object) bqlTable);
        }
        int num = !this._documentRows.ContainsKey(bqlTable) ? 1 : 0;
        SyImportProcessor.ExportTableHelper.NameValueCollection values = num != 0 ? new SyImportProcessor.ExportTableHelper.NameValueCollection() : this._documentRows[bqlTable];
        currentLine.Views.Add(cmd.View, new SyImportProcessor.ExportTableHelper.ViewExportedValues(cmd.View, nativeRow, isPrimary, values));
        if ((num & (isPrimary ? 1 : 0)) != 0 || this._forceFlush)
        {
          this.FlushDocument((IEnumerable<SyImportProcessor.ExportTableHelper.ExportedLine>) this._documentLines, false);
          this._documentRows.Clear();
          this._documentLines.Clear();
        }
        if (num != 0)
          this._documentRows.Add(bqlTable, values);
        if (!collectedViews.ContainsKey(cmd.View))
          collectedViews.Add(cmd.View, values);
      }
      if (!collectedViews.ContainsKey(cmd.View))
        return;
      SyImportProcessor.ExportTableHelper.CommandState cmdState = new SyImportProcessor.ExportTableHelper.CommandState()
      {
        Currents = currentLine.Currents
      };
      if (cmd.CommandType == SyCommandType.ExportField)
      {
        this.WriteExportedValueAndState(cmd, this._formulaProcessor, (ISYTableAddGet) this._dummyTable, cmdState);
        this._dummyTable.AddValue(cmd.Field, cmdState.Value, cmdState.State, cmdState.Translations);
      }
      collectedViews[cmd.View][cmd] = cmdState;
    }

    private void FlushDocument(
      IEnumerable<SyImportProcessor.ExportTableHelper.ExportedLine> lines,
      bool refreshValues)
    {
      bool slot = PXContext.GetSlot<bool>("ExportRefreshingValues");
      try
      {
        PXContext.SetSlot<bool>("ExportRefreshingValues", refreshValues);
        this._forceFlush = false;
        HashSet<SyImportProcessor.ExportTableHelper.ViewExportedValues> viewExportedValuesSet = new HashSet<SyImportProcessor.ExportTableHelper.ViewExportedValues>();
        HashSet<IBqlTable> bqlTableSet = new HashSet<IBqlTable>();
        foreach (SyImportProcessor.ExportTableHelper.ExportedLine line in lines)
        {
          foreach (SyImportProcessor.ExportTableHelper.ViewExportedValues viewExportedValues1 in line.Views.Values)
          {
            if (viewExportedValues1.NativeRow.BqlTable != null)
            {
              bool flag = true;
              if (bqlTableSet.Contains(viewExportedValues1.NativeRow.BqlTable))
              {
                foreach (SyImportProcessor.ExportTableHelper.ViewExportedValues viewExportedValues2 in viewExportedValuesSet)
                {
                  if (viewExportedValues2.ViewName == viewExportedValues1.ViewName && viewExportedValues2.NativeRow.BqlTable == viewExportedValues1.NativeRow.BqlTable)
                  {
                    if (viewExportedValues2.NativeRow.Select != viewExportedValues1.NativeRow.Select)
                    {
                      viewExportedValuesSet.Remove(viewExportedValues2);
                      break;
                    }
                    flag = false;
                    break;
                  }
                }
              }
              if (flag)
              {
                viewExportedValuesSet.Add(viewExportedValues1);
                bqlTableSet.Add(viewExportedValues1.NativeRow.BqlTable);
              }
            }
          }
        }
        foreach (SyImportProcessor.ExportTableHelper.ExportedLine line in lines)
        {
          NativeRowWrapper nativeRow = line.Views.Values.FirstOrDefault<SyImportProcessor.ExportTableHelper.ViewExportedValues>((Func<SyImportProcessor.ExportTableHelper.ViewExportedValues, bool>) (v => v.IsPrimaryView))?.NativeRow;
          IBqlTable bqlTable = nativeRow?.BqlTable;
          bool flag = line.Views.Count == 1 || bqlTable != null && bqlTable != this._lastPrimaryRow;
          this._exportedTable.AddRow(nativeRow?.Result);
          foreach (SyCommand command in this.Context.Commands)
          {
            SyImportProcessor.ExportTableHelper.ViewExportedValues viewExportedValues;
            SyImportProcessor.ExportTableHelper.CommandState cmdState;
            if ((command.CommandType == SyCommandType.ExportField || command.CommandType == SyCommandType.ExportPath) && line.Views.TryGetValue(command.View, out viewExportedValues) && viewExportedValues.Values.TryGetValue(command, out cmdState) && (this.Context.RepeatingData != SYMapping.RepeatingOption.None && viewExportedValues.IsPrimaryView || viewExportedValuesSet.Contains(viewExportedValues) || this.Context.RepeatingData == SYMapping.RepeatingOption.All))
            {
              if (command.CommandType == SyCommandType.ExportField)
              {
                if (refreshValues)
                  this.WriteExportedValueAndState(command, this._formulaProcessor, (ISYTableAddGet) this._exportedTable, cmdState);
                this._exportedTable.AddValue(command.Field, cmdState.Value, cmdState.State, cmdState.Translations);
              }
              else
                this._exportedTable.AddValue(command.Field, (object) viewExportedValues.NativeRow.Path, (PXFieldState) null, (PXDBLocalizableStringAttribute.Translations) null);
              flag = flag || !viewExportedValues.IsPrimaryView;
            }
          }
          this._lastPrimaryRow = bqlTable;
          if (!flag)
            this._exportedTable.RemoveRow();
        }
      }
      finally
      {
        PXContext.SetSlot<bool>(slot);
      }
    }

    private void WriteExportedValueAndState(
      SyCommand command,
      SyFormulaProcessor formulaProcessor,
      ISYTableAddGet exportedTable,
      SyImportProcessor.ExportTableHelper.CommandState cmdState)
    {
      PXFieldState state = (PXFieldState) null;
      PXDBLocalizableStringAttribute.Translations translations = (PXDBLocalizableStringAttribute.Translations) null;
      if (command.Formula.StartsWith("="))
      {
        SyFormulaFinalDelegate getter = (SyFormulaFinalDelegate) (names =>
        {
          if (names.Length == 1)
            return exportedTable.GetValue(names[0]);
          object row;
          if (!cmdState.Currents.TryGetValue(names[0], out row))
            throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Formula runtime error: {0}", (object) command.Formula));
          return row != null ? SyImportProcessor.ExportTableHelper.GetValueExt(this._graph, row, names[0], names[1], this.Context.ForceState, this.Context.AddTranslations, out state, out translations) : (object) null;
        });
        cmdState.Value = formulaProcessor.Evaluate(command.Formula, getter);
      }
      else
      {
        object row;
        if (!cmdState.Currents.TryGetValue(command.View, out row))
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Invalid field exported: {0}", (object) command.Formula));
        cmdState.Value = row == null ? (object) null : SyImportProcessor.ExportTableHelper.GetValueExt(this._graph, row, command.View, command.Formula, this.Context.ForceState, this.Context.AddTranslations, out state, out translations);
      }
      cmdState.State = state;
      cmdState.Translations = translations;
    }

    internal static object GetValueExt(
      PXGraph graph,
      object row,
      string viewName,
      string fieldName,
      bool forceState,
      bool addTranslations,
      out PXFieldState state,
      out PXDBLocalizableStringAttribute.Translations translations)
    {
      state = (PXFieldState) null;
      translations = (PXDBLocalizableStringAttribute.Translations) null;
      object valueExt1 = graph.GetValueOrStateExt(viewName, row, fieldName, forceState);
      if (valueExt1 != null)
      {
        state = valueExt1 as PXFieldState;
        if (state != null)
          valueExt1 = state.Value;
        switch (valueExt1)
        {
          case null:
            return (object) null;
          case string str:
            if (state == null)
              state = graph.GetStateExt(viewName, row, fieldName) as PXFieldState;
            string result1;
            if (state is PXStringState state1 && state1.HasFixedValuesList() && !state1.MultiSelect && state1.TryGetListLabel(str, out result1))
              valueExt1 = (object) result1;
            if (addTranslations)
            {
              PXCache cache = graph.Views[viewName].Cache;
              PXDBLocalizableStringAttribute localizableStringAttribute = cache.GetAttributesOfType<PXDBLocalizableStringAttribute>(PXResult.UnwrapFirst(row), fieldName).FirstOrDefault<PXDBLocalizableStringAttribute>();
              if (localizableStringAttribute != null)
              {
                translations = localizableStringAttribute.GetTranslationsWithLanguage(cache, PXResult.UnwrapFirst(row));
                break;
              }
              break;
            }
            break;
          case int num1:
label_13:
            if (state == null)
              state = graph.GetStateExt(viewName, row, fieldName) as PXFieldState;
            string result2;
            if (state is PXIntState state2 && state2.HasFixedValuesList() && state2.TryGetListLabel(num1, out result2))
            {
              valueExt1 = (object) result2;
              break;
            }
            break;
          case short num2:
            if ((num1 = (int) num2) != num1)
              break;
            goto label_13;
        }
        return valueExt1;
      }
      if (fieldName.Contains<char>('!'))
      {
        string outerField;
        string innerField;
        if (PXSelectorAttribute.SplitFieldNames(fieldName, out outerField, out innerField) && graph.GetStateExt(viewName, row, outerField) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.ViewName))
        {
          if (graph.Views[stateExt.ViewName].GetItemType() != graph.Views[viewName].GetItemType())
          {
            object data = PXSelectorAttribute.SelectSingle(graph.Views[viewName].Cache, PXResult.UnwrapFirst(row), outerField);
            if (data != null)
            {
              object valueExt2 = graph.GetValueExt(stateExt.ViewName, data, innerField);
              if (valueExt2 is PXFieldState)
              {
                state = valueExt2 as PXFieldState;
                valueExt2 = state.Value;
              }
              return valueExt2;
            }
          }
          else
          {
            object valueOrStateExt = graph.GetValueOrStateExt(viewName, row, innerField, forceState);
            state = valueOrStateExt as PXFieldState;
            if (state != null)
              valueOrStateExt = state.Value;
            return valueOrStateExt;
          }
        }
      }
      else if (SyImportProcessor.SyStep.IsFileName(fieldName))
      {
        PXCache cache = graph.Views[viewName].Cache;
        string filePrefix = SyImportProcessor.SyStep.GetFilePrefix(cache, row);
        fieldName = SyImportProcessor.SyStep.GetFileName(fieldName);
        foreach (UploadFile uploadFile in SyImportProcessor.SyStep.GetUploadFiles(graph, cache, row))
        {
          if (uploadFile.Name == fieldName || !string.IsNullOrEmpty(filePrefix) && uploadFile.Name.StartsWith(filePrefix) && uploadFile.Name.Substring(filePrefix.Length) == fieldName)
          {
            FileInfo file = PXGraph.CreateInstance<UploadFileMaintenance>().GetFile(uploadFile.FileID.Value);
            if (file != null && file.BinData != null)
              return (object) Convert.ToBase64String(file.BinData);
          }
        }
      }
      return valueExt1;
    }

    private static bool IsRowAborted(bool isEmptySelect, bool isPrimaryView, SyCommandType cmdType)
    {
      switch (cmdType)
      {
        case SyCommandType.EnumFieldValues:
          return isEmptySelect;
        case SyCommandType.ExportField:
        case SyCommandType.ExportPath:
          return isEmptySelect & isPrimaryView;
        default:
          return false;
      }
    }

    private static bool IsUnavailableActionCommand(
      PXGraph graph,
      SyCommand cmd,
      string name,
      bool checkDirty)
    {
      if (cmd.CommandType != SyCommandType.Action || !cmd.Field.OrdinalEquals(name))
        return false;
      if (graph.Actions[name] == null || (graph.Actions[name].GetState((object) null) is PXButtonState state ? (!state.Enabled ? 1 : 0) : 1) != 0)
        return true;
      return checkDirty && !graph.IsDirty;
    }

    private int? EmitExportedRows(
      SyImportProcessor.SyStep step,
      IGrowingTable table,
      SyCommand command,
      PXFilterRow[] filters,
      int topCount)
    {
      string view1 = command.View;
      string viewId = SyExportContext.GetViewId(command);
      int? nullable = table.CountResultsForView(viewId);
      if (nullable.HasValue)
        return nullable;
      bool flag1 = step.IsPrimaryView(view1);
      if (command.CommandType == SyCommandType.EnumFieldValues)
      {
        if (flag1)
          table.HasEnumValues = true;
        string fieldId = SyExportContext.GetFieldID(command);
        IEnumerable<object> source = (IEnumerable<object>) new object[0];
        if (command.CheckRowNumberCondition && this._graph.IsContractBasedAPI)
        {
          int num1 = -1;
          int num2 = -1;
          SyImportProcessor.SyStep.SyView view2 = step.Views[view1];
          if (view2.TotalRow != null)
            num1 = step.GetFormulaValueInt(view2.TotalRow);
          if (view2.StartRow != null && !view2.StartRow.StartsWith("RowNumber_"))
            num2 = step.GetFormulaValueInt(view2.StartRow);
          if (num2 != -1 && num1 != -1)
            num1 += num2;
          if (num2 == -1 && num1 == -1)
            source = step.EnumFieldValues(this._graph, view1, command.Field, filters, topCount, step.PrimaryView, true, this.Context.ShouldReadArchive);
          else if (command.Enumerated < num1 || command.Skipped < num2 || num1 == -1)
          {
            source = step.EnumFieldValues(this._graph, view1, command.Field, filters, topCount, step.PrimaryView, true, this.Context.ShouldReadArchive);
            int num3 = source.Count<object>();
            int count = num2 - command.Skipped;
            if (count > num3)
              count = num3;
            if (num2 <= command.Skipped)
              count = 0;
            if (num2 == -1)
              count = 0;
            int num4 = num1 - command.Enumerated;
            if (num3 < num4)
              num4 = num3;
            if (num1 == -1)
              num4 = num3;
            if (num2 != -1 || num1 != -1)
              source = source.Skip<object>(count).Take<object>(num4 - count);
            if (num2 != -1)
              command.Skipped += count;
            if (num1 != -1)
              command.Enumerated += num4;
          }
        }
        else
          source = step.EnumFieldValues(this._graph, view1, command.Field, filters, topCount, step.PrimaryView, shouldReadArchived: this.Context.ShouldReadArchive);
        ViewSelectResults selectResults = new ViewSelectResults(viewId);
        foreach (object v in source)
        {
          selectResults.AddRow((object) null);
          selectResults.AddCell(fieldId, v);
          selectResults.AddCell(SyExportContext.GetFieldID(command.View), (object) 0);
        }
        table.AddSelectResults(selectResults);
        return new int?(selectResults.Count);
      }
      if (command.CommandType == SyCommandType.ExportPath)
      {
        IEnumerable<KeyValuePair<string, object>> keyValuePairs = step.SelectPaths(view1, command.Formula, this.Context.Sorts, filters);
        ViewSelectResults selectResults = new ViewSelectResults(viewId);
        foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
        {
          selectResults.AddRow(keyValuePair.Value);
          selectResults[selectResults.Count - 1].Path = keyValuePair.Key;
        }
        table.AddSelectResults(selectResults);
        return new int?(selectResults.Count);
      }
      if (command.CommandType == SyCommandType.CustomDelegate)
      {
        ViewSelectResults selectResults = new ViewSelectResults(viewId);
        if (this._step.PrimaryResult != null)
        {
          selectResults.AddRow(this._step.PrimaryResult);
          selectResults.AddCell(SyExportContext.GetFieldID(command.View), (object) 0);
          table.AddSelectResults(selectResults);
        }
        return new int?(selectResults.Count);
      }
      bool flag2 = false;
      IEnumerable enumerable = (IEnumerable) null;
      if (this.Context.LinkedSelectorViews.ContainsView(view1))
      {
        string str = this.Context.LinkedSelectorViews.GetFields(view1).FirstOrDefault<string>((Func<string, bool>) (v => step.ExternalValues.Results.ContainsKey(StringExtensions.FirstSegment(StringExtensions.FirstSegment(v, '%'), ':'))));
        if (str != null)
        {
          flag2 = true;
          string[] strArray = str.Split('%');
          string key = StringExtensions.FirstSegment(strArray[0], ':');
          PXCache cache = this._graph.Views[key].Cache;
          object data = PXResult.UnwrapFirst(step.ExternalValues.Results[key]);
          object obj1;
          enumerable = (IEnumerable) new object[1]
          {
            obj1 = PXSelectorAttribute.SelectSingle(cache, data, strArray[1])
          };
          if (obj1 == null && cache.GetValue(cache.Current, strArray[1]) == null)
          {
            object obj2 = PXFieldState.UnwrapValue(cache.GetValueExt(cache.Current, strArray[1]));
            if (obj2 != null)
              enumerable = (IEnumerable) new object[1]
              {
                PXSelectorAttribute.Select(cache, data, strArray[1], obj2)
              };
          }
          if (enumerable == null)
            throw new PXException($"Invalid linked selector view {view1}.");
        }
      }
      if (!flag2)
      {
        if (command.UseCurrent)
          enumerable = (IEnumerable) new object[1]
          {
            step.Graph.Views[view1].Cache.Current
          };
        else
          enumerable = step.SelectRows(view1, filters, flag1 || this._filterAsPrimary ? topCount : 0, flag1 && table.HasEnumValues, 0, this.Context);
      }
      ViewSelectResults selectResults1 = new ViewSelectResults(viewId);
      int v1 = 0;
      foreach (object result in enumerable)
      {
        selectResults1.AddRow(result);
        selectResults1.AddCell(SyExportContext.GetFieldID(command.View), (object) v1);
        ++v1;
      }
      table.AddSelectResults(selectResults1);
      return new int?(selectResults1.Count);
    }

    private class ViewExportedValues
    {
      public readonly string ViewName;
      public readonly bool IsPrimaryView;
      public readonly SyImportProcessor.ExportTableHelper.NameValueCollection Values;
      public readonly NativeRowWrapper NativeRow;

      public ViewExportedValues(
        string viewName,
        NativeRowWrapper nativeRow,
        bool isPrimary,
        SyImportProcessor.ExportTableHelper.NameValueCollection values)
      {
        this.ViewName = viewName;
        this.NativeRow = nativeRow;
        this.IsPrimaryView = isPrimary;
        this.Values = values;
      }
    }

    private class CommandState
    {
      public Dictionary<string, object> Currents;
      public object Value;
      public PXFieldState State;
      public PXDBLocalizableStringAttribute.Translations Translations;
    }

    private class NameValueCollection : 
      Dictionary<SyCommand, SyImportProcessor.ExportTableHelper.CommandState>
    {
    }

    private class ExportedLine
    {
      public readonly Dictionary<string, SyImportProcessor.ExportTableHelper.ViewExportedValues> Views = new Dictionary<string, SyImportProcessor.ExportTableHelper.ViewExportedValues>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      public Dictionary<string, object> Currents = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    private class DefaultGraphHelper : IGraphHelper
    {
      public void UnloadGraph(PXGraph graph) => graph.Unload();

      public PXGraph CreateGraphForRedirect(string screenId, string graphType)
      {
        return SyImportProcessor.CreateGraph(graphType);
      }
    }
  }

  internal sealed class SyExternalValues
  {
    public System.DateTime PrepareTime = System.DateTime.UtcNow;
    private readonly PXGraph Graph;
    public Dictionary<string, object> Results;
    private readonly Dictionary<string, string> Values;

    public string this[string key]
    {
      set => this.Values[key] = value;
      get => this.Values[key];
    }

    public bool TryGetValue(string key, out string value)
    {
      return this.Values.TryGetValue(key, out value);
    }

    public bool TryGetValue(string view, string field, out object value)
    {
      PXView pxView;
      object row;
      if (this.Graph != null && this.Graph.Views.TryGetValue(view, out pxView) && this.Results != null && this.Results.TryGetValue(view, out row) && row != null)
      {
        value = PXFieldState.UnwrapValue(pxView.Cache.GetValueExt(PXResult.UnwrapFirst(row), field));
        return true;
      }
      string str;
      if (this.Values.TryGetValue($"{view}.{field}", out str))
      {
        value = (object) str;
        return true;
      }
      value = (object) null;
      return false;
    }

    public SyExternalValues(SyImportContext context)
      : this(context.FieldNamesComparer)
    {
      this.PrepareTime = context.PrepareTime;
      for (int columnIndex = 0; columnIndex < context.SourceFields.Length; ++columnIndex)
        this[context.SourceFields[columnIndex]] = context.SourceTable.GetValue(columnIndex, context.RowIndex);
    }

    public SyExternalValues(PXGraph graph)
      : this(StringComparer.OrdinalIgnoreCase)
    {
      this.Graph = graph;
    }

    private SyExternalValues(StringComparer fieldNamesComparer)
    {
      this.Values = new Dictionary<string, string>((IEqualityComparer<string>) fieldNamesComparer);
    }
  }

  internal sealed class SyStep
  {
    private const string ActionMenuName = "Action";
    private const string ActionsMenuName = "Actions";
    private const string InquiryMenuName = "Inquiry";
    private const string InquiriesMenuName = "Inquiries";
    private const string ReportMenuName = "Report";
    private const string ReportsMenuName = "Reports";
    private readonly string[] StandardActionsSuffix = new string[6]
    {
      nameof (Action),
      "Actions",
      "Inquiry",
      "Inquiries",
      "Report",
      "Reports"
    };
    internal readonly PXGraph Graph;
    public object PrimaryResult;
    internal readonly Dictionary<string, SyImportProcessor.SyStep.SyView> Views;
    internal readonly SyImportProcessor.SyExternalValues ExternalValues;
    internal readonly SyFormulaProcessor FormulaProcessor;
    internal readonly SyFormulaFinalDelegate GetExternal;
    internal readonly SyFormulaFinalDelegate GetInternal;
    internal readonly SyFormulaFinalDelegate GetSearch;
    internal readonly SyImportRowResult RowImportResult;
    internal string Action;
    private string WorkflowAction;
    private string ActionTriggerField;
    private SyImportProcessor.SyStep PreviousStep;
    private readonly int RowNumberLocal;
    private bool UsePreviousFieldValueInSearch = true;
    private bool IsActionRemovalPending;
    private bool IsPending;
    public readonly string PrimaryView;
    private string _primaryDataView;
    private readonly IWorkflowService _workflow;
    private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
    private readonly SyImportProcessor.SyStep.GlobalStep GlobalState;
    internal static readonly object anyBypass = new object();

    public static string RemovePrompt(PXStringState state, string val)
    {
      PXSegment[] segments;
      if (state is PXSegmentedState && (segments = ((PXSegmentedState) state).Segments) != null && ((IEnumerable<PXSegment>) segments).Any<PXSegment>((Func<PXSegment, bool>) (_ => _.PromptCharacter != '_')))
      {
        if (((IEnumerable<PXSegment>) segments).GroupBy<PXSegment, char>((Func<PXSegment, char>) (_ => _.PromptCharacter)).Count<IGrouping<char, PXSegment>>() == 1)
        {
          val = val.Replace(segments[0].PromptCharacter, ' ');
        }
        else
        {
          List<string> stringList = new List<string>();
          for (int index = 0; index < segments.Length; ++index)
          {
            if ((int) segments[index].Length + 1 >= val.Length)
            {
              stringList.Add(val);
              break;
            }
            stringList.Add(val.Substring(0, (int) segments[index].Length + 1));
            val = val.Substring((int) segments[index].Length + 1);
          }
          for (int index = 0; index < stringList.Count; ++index)
            stringList[index] = stringList[index].Replace(segments[index].PromptCharacter, ' ');
          val = string.Concat(stringList.ToArray());
        }
      }
      else
      {
        if (state != null)
        {
          PXStringState pxStringState = state;
          char? promptChar = pxStringState.PromptChar;
          if (promptChar.HasValue)
          {
            string str = val;
            promptChar = pxStringState.PromptChar;
            int oldChar = (int) promptChar.Value;
            val = str.Replace((char) oldChar, ' ');
            goto label_16;
          }
        }
        val = val.Replace('_', ' ');
      }
label_16:
      return val;
    }

    public static PXFilterRow[] DemaskFilters(
      PXGraph graph,
      string viewName,
      PXFilterRow[] filters)
    {
      return filters == null || filters.Length == 0 ? filters : ((IEnumerable<PXFilterRow>) filters).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (f =>
      {
        PXFilterRow filter = new PXFilterRow(f);
        if (f.Condition == PXCondition.NestedSelector)
        {
          filter.Value2 = (object) new PXFilterRow((PXFilterRow) filter.Value2);
          SyImportProcessor.SyStep.DemaskFilter(graph, viewName, (PXFilterRow) filter.Value2);
        }
        else
          SyImportProcessor.SyStep.DemaskFilter(graph, viewName, filter);
        return filter;
      })).ToArray<PXFilterRow>();
    }

    private static void DemaskFilter(PXGraph graph, string viewName, PXFilterRow filter)
    {
      if (string.IsNullOrEmpty(filter.DataField) || !(filter.Value is string) && !(filter.Value2 is string) || !(graph.GetStateExt(viewName, (object) null, filter.DataField) is PXFieldState stateExt))
        return;
      switch (stateExt)
      {
        case PXStringState state1:
          if (!string.IsNullOrEmpty(state1.InputMask))
          {
            if (filter.Value is string str1)
              filter.Value = (object) SyImportProcessor.SyStep.DemaskString(str1, state1, false);
            if (filter.Value2 is string str2)
              filter.Value2 = (object) SyImportProcessor.SyStep.DemaskString(str2, state1, false);
          }
          if (!state1.HasFixedValuesList())
            break;
          string result1;
          if (filter.Value is string label1 && state1.TryGetListValue(label1, out result1))
            filter.Value = (object) result1;
          string result2;
          if (!(filter.Value2 is string label2) || !state1.TryGetListValue(label2, out result2))
            break;
          filter.Value2 = (object) result2;
          break;
        case PXIntState state2:
          if (!state2.HasFixedValuesList())
            break;
          object result3;
          if (filter.Value is string label3 && state2.TryGetListValue(label3, out result3))
            filter.Value = result3;
          object result4;
          if (!(filter.Value2 is string label4) || !state2.TryGetListValue(label4, out result4))
            break;
          filter.Value2 = result4;
          break;
        case PXDateState _:
          if (string.IsNullOrEmpty(filter.LocaleOverride))
            break;
          CultureInfo provider = new CultureInfo(filter.LocaleOverride);
          if (filter.Value is string s1)
            filter.Value = (object) System.DateTime.Parse(s1, (IFormatProvider) provider);
          if (!(filter.Value2 is string s2))
            break;
          filter.Value2 = (object) System.DateTime.Parse(s2, (IFormatProvider) provider);
          break;
      }
    }

    internal static string DemaskString(string value, PXStringState state, bool encode)
    {
      value = SyImportProcessor.SyStep.RemovePrompt(state, value);
      if (Mask.IsMasked(value, state.InputMask, false))
        value = Mask.Parse(state.InputMask, value);
      else if (encode && state.InputMask != "*")
      {
        StringBuilder stringBuilder = Mask.EncodeMask(state.InputMask);
        int length = value.Length;
        string str = value;
        value = Mask.Format(stringBuilder, str, ' ');
        value = Mask.Parse(state.InputMask, value);
        if (value != null && length < value.Length)
          value = value.Substring(0, length);
      }
      if (state.InputMask.StartsWith(">"))
        value = value.ToUpper();
      else if (state.InputMask.StartsWith("<"))
        value = value.ToLower();
      return value;
    }

    public static string GetFilePrefix(PXCache cache, object row)
    {
      string filePrefix = (string) null;
      string screenId = PXContext.GetScreenID();
      PXSiteMapNode screenIdUnsecure;
      if (!string.IsNullOrEmpty(screenId) && (screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId.Replace(".", ""))) != null && !string.IsNullOrEmpty(screenIdUnsecure.Title))
      {
        StringBuilder stringBuilder = new StringBuilder(screenIdUnsecure.Title);
        stringBuilder.Append(" (");
        for (int index = 0; index < cache.Keys.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(cache.GetValue(row, cache.Keys[index]));
        }
        stringBuilder.Append(")\\");
        filePrefix = stringBuilder.ToString();
      }
      return filePrefix;
    }

    public string PrimaryDataView
    {
      get => this._primaryDataView ?? this.PrimaryView;
      set => this._primaryDataView = value;
    }

    public int RowNumber
    {
      get
      {
        int rowNumberLocal = this.RowNumberLocal;
        SyImportProcessor.SyStep syStep = this;
        while ((syStep = syStep.PreviousStep) != null)
          rowNumberLocal = syStep.RowNumberLocal;
        return rowNumberLocal;
      }
    }

    public object PrimaryItem
    {
      get
      {
        SyImportProcessor.SyStep.SyView syView;
        return !string.IsNullOrEmpty(this.PrimaryDataView) && this.Views.TryGetValue(this.PrimaryDataView, out syView) ? syView.Current : (object) null;
      }
    }

    public int MinRowNumber => this.GlobalState.MinRowNumber;

    public SyStep(
      SyImportProcessor.SyStep previousStep,
      SyImportProcessor.SyExternalValues externalValues,
      SyImportRowResult rowImportResult = null)
      : this(previousStep.FormulaProcessor, externalValues)
    {
      this.PreviousStep = previousStep;
      this.Graph = previousStep.Graph;
      this.PrimaryView = previousStep.PrimaryView;
      this.PrimaryDataView = previousStep.PrimaryDataView;
      this.RowImportResult = rowImportResult ?? previousStep.RowImportResult;
      this.GlobalState = previousStep.GlobalState;
      this.IsExport = previousStep.IsExport;
      this._workflow = previousStep._workflow;
      this._currentUserInformationProvider = this.PreviousStep._currentUserInformationProvider;
      this.RowNumberLocal = previousStep.RowNumberLocal;
      this.UsePreviousFieldValueInSearch = previousStep.UsePreviousFieldValueInSearch;
      if (previousStep.ExternalValues != null && previousStep.ExternalValues != externalValues)
      {
        ++this.RowNumberLocal;
        if (previousStep.RowImportResult != null && this.RowImportResult != null)
          this.UsePreviousFieldValueInSearch = SyImportProcessor.RowKeysEqual(previousStep.RowImportResult.ExternalKeys, this.RowImportResult.ExternalKeys);
      }
      foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyView> view in previousStep.Views)
        this.Views[view.Key] = new SyImportProcessor.SyStep.SyView(this, view.Key, view.Value);
    }

    public SyStep(
      PXGraph graph,
      SyFormulaProcessor formulaProcessor,
      string primaryView,
      bool export = false,
      SyImportProcessor.SyExternalValues externalValues = null,
      SyImportRowResult rowImportResult = null)
      : this(formulaProcessor, externalValues)
    {
      this.Graph = graph;
      this.PrimaryView = primaryView;
      this.RowImportResult = rowImportResult;
      this.GlobalState = new SyImportProcessor.SyStep.GlobalStep();
      this.IsExport = export;
      this._workflow = ServiceLocator.Current.GetInstance<IWorkflowService>();
      this._currentUserInformationProvider = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>();
    }

    private SyStep(
      SyFormulaProcessor formulaProcessor,
      SyImportProcessor.SyExternalValues externalValues)
    {
      this.Views = new Dictionary<string, SyImportProcessor.SyStep.SyView>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.FormulaProcessor = formulaProcessor;
      this.ExternalValues = externalValues;
      this.GetExternal = (SyFormulaFinalDelegate) (names => this.GetFieldValue(true, false, names));
      this.GetInternal = (SyFormulaFinalDelegate) (names => this.GetFieldValue(false, false, names));
      this.GetSearch = (SyFormulaFinalDelegate) (names => this.GetFieldValue(false, this.UsePreviousFieldValueInSearch, names));
    }

    private string GetSimplifiedKey()
    {
      return this.ExternalValues.PrepareTime.ToString() + this.FormulaProcessor.Evaluate("=LineNbr()", this.GetExternal)?.ToString();
    }

    private object GetFieldValue(bool externalOnly, bool usePrevious, params string[] names)
    {
      if (names.Length > 2)
        names = new string[2]
        {
          string.Join(".", ((IEnumerable<string>) names).Take<string>(names.Length - 1)),
          ((IEnumerable<string>) names).Last<string>()
        };
      if (names.Length == 1)
      {
        string name = names[0];
        if (name == null)
          return (object) null;
        string fieldValue;
        if (this.ExternalValues.TryGetValue(name, out fieldValue))
        {
          if (!string.IsNullOrEmpty(fieldValue))
            fieldValue = fieldValue.TrimEnd();
          if (fieldValue != string.Empty)
            return (object) fieldValue;
        }
        return (object) null;
      }
      string str = names.Length == 2 ? names[0] : throw new PXArgumentException(nameof (names), "The argument is out of range.");
      if (str == null)
        return (object) null;
      string name1 = names[1];
      if (name1 == null)
        return (object) null;
      if (name1.IndexOf("__") > -1)
        usePrevious = false;
      SyImportProcessor.SyStep.SyView syView;
      SyImportProcessor.SyStep.SyField syField;
      if (this.Views.TryGetValue(str, out syView) && syView.Fields.TryGetValue(name1, out syField))
      {
        if (syField.CommittedRevision >= syView.MinRevision)
        {
          if (externalOnly)
            return syField.GetValue(this.FormulaProcessor, this.GetExternal);
          if (!syField.IsCalced)
          {
            if (!syField.Calculating)
            {
              syField.CalculateExternal(this);
            }
            else
            {
              syField.ExternalValue = syField.InternalValue;
              syField.LoopDetected = true;
              return this.Graph.Views[str].Cache.ObjectsEqual(syField.Current, syView.Current) && object.Equals(syField.CommittedExternalValue, syField.ExternalValue) ? syField.CommittedInternalValue : syField.InternalValue;
            }
          }
          return this.Graph.Views[str].Cache.ObjectsEqual(syField.Current, syView.Current) && (object.Equals(syField.CommittedExternalValue, syField.ExternalValue) || usePrevious && syField.ExternalValue == null) ? syField.CommittedInternalValue : syField.GetValue(this.FormulaProcessor, usePrevious ? this.GetSearch : this.GetInternal);
        }
        if (usePrevious && this.IsPrimaryView(str) && this.Graph.Views[str].Cache.ObjectsEqual(syField.Current, syView.Current) && object.Equals(syField.CommittedExternalValue, syField.ExternalValue))
          return syField.CommittedInternalValue;
      }
      object obj;
      return this.ExternalValues.TryGetValue(str, name1, out obj) ? obj : (object) null;
    }

    internal int GetFormulaValueInt(string formula)
    {
      object obj = this.FormulaProcessor.Evaluate(formula, this.GetExternal);
      return !(obj is string s) ? Convert.ToInt32(obj) : int.Parse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    public SyImportProcessor.SyStep ProcessCommand(SyCommand cmd, out bool needCommit)
    {
      try
      {
        SyImportProcessor.SyStep previousStep1 = this;
        needCommit = false;
        if (this.IsPending && cmd.CommandType == SyCommandType.Action)
          return new SyImportProcessor.SyStep(previousStep1, this.ExternalValues).ProcessCommand(cmd, out needCommit);
        string str1 = cmd.View;
        bool flag = false;
        if (str1.OrdinalEquals("FilterPreview"))
        {
          str1 = this.PrimaryView;
          flag = true;
        }
        SyImportProcessor.SyStep.SyView view;
        this.GetViewOrAddNew(str1, out view);
        switch (cmd.CommandType)
        {
          case SyCommandType.Field:
          case SyCommandType.EnumFieldValues:
            if (!flag)
            {
              string fieldName = cmd.Field;
              string str2 = (string) null;
              if (!string.IsNullOrEmpty(fieldName))
              {
                int length = fieldName.IndexOf('!');
                if (length > 0 && length < fieldName.Length - 1)
                {
                  str2 = fieldName.Substring(length + 1).Trim();
                  fieldName = fieldName.Substring(0, length).Trim();
                }
              }
              SyImportProcessor.SyStep.SyField field1;
              view.GetFieldOrAddNew(fieldName, out field1);
              field1.ForeignField = str2;
              field1.Formula = cmd.Formula;
              field1.IsCalced = false;
              field1.IsPending = true;
              field1.FromPreviousSearch = false;
              field1.IgnoreError = cmd.IgnoreError;
              field1.Revision = ++view.MaxRevision;
              field1.CheckUpdatability = cmd.CheckUpdatability;
              if (cmd.Commit && this.Action != null)
              {
                SyImportProcessor.SyStep.SyView syView;
                SyImportProcessor.SyStep.SyField field2;
                if (this.PreviousStep != null && this.PreviousStep.Action.OrdinalEquals(this.Action) && this.PreviousStep.ExternalValues == this.ExternalValues && this.PreviousStep.Views.TryGetValue(str1, out syView) && !syView.GetFieldOrAddNew(fieldName, out field2))
                {
                  field2.ForeignField = field1.ForeignField;
                  field2.Formula = field1.Formula;
                  field2.IsCalced = field1.IsCalced;
                  field2.IsPending = field1.IsPending;
                  field2.IgnoreError = field1.IgnoreError;
                  field2.Revision = ++syView.MaxRevision;
                  field2.CheckUpdatability = field1.CheckUpdatability;
                }
                if ((view.Current == null || view.Current == field1.Current) && object.Equals(field1.CommittedExternalValue, this.FormulaProcessor.Evaluate(field1.Formula, this.GetExternal)))
                {
                  this.IsActionRemovalPending = true;
                  needCommit = false;
                  return new SyImportProcessor.SyStep(previousStep1, this.ExternalValues);
                }
                this.ActionTriggerField = fieldName;
              }
              else
                this.ActionTriggerField = (string) null;
              view.IsPending = true;
              previousStep1.IsPending = true;
              break;
            }
            view.WorkflowParameters[cmd.Field] = cmd.Formula;
            break;
          case SyCommandType.Parameter:
            view.Parameters[cmd.Field] = cmd.Formula;
            break;
          case SyCommandType.Search:
            view.Searches[cmd.Field] = cmd.Formula;
            if (this.Graph.IsContractBasedAPI && !view.IsPending)
              EnumerableExtensions.ForEach<KeyValuePair<string, SyImportProcessor.SyStep.SyField>>((IEnumerable<KeyValuePair<string, SyImportProcessor.SyStep.SyField>>) view.Fields, (System.Action<KeyValuePair<string, SyImportProcessor.SyStep.SyField>>) (f => f.Value.FromPreviousSearch = true));
            if (!this.Graph.IsContractBasedAPI && this.IsPrimaryView(str1))
            {
              PXCache cache = this.Graph.Views[this.PrimaryView].Cache;
              object obj = this.FormulaProcessor.Evaluate(cmd.Formula, this.GetExternal);
              this.TryUpdateExternalKey(cache, cmd.Field, obj, ref this.GlobalState.PendingExternalKey);
              break;
            }
            break;
          case SyCommandType.Action:
            this.Action = cmd.Field;
            if (this.Action.OrdinalEquals("WorkflowTransition"))
              this.WorkflowAction = cmd.Formula;
            if (cmd.Commit)
            {
              previousStep1.PreviousStep?.ProcessThisAndPreviousSteps((System.Action<SyImportProcessor.SyStep>) (s => s.Action = (string) null));
            }
            else
            {
              SyImportProcessor.SyStep.SyView syView;
              if (this.PreviousStep != null && this.PreviousStep.Action.OrdinalEquals(this.Action) && this.PreviousStep.ExternalValues == this.ExternalValues && this.PreviousStep.Views.TryGetValue(str1, out syView))
              {
                foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field in syView.Fields)
                  view.Fields[field.Key].IsPending = field.Value.IsPending;
              }
              if (this.GlobalState.PendingExternalKey != null)
              {
                if (this.GlobalState.CommittedExternalKey.OrdinalEquals(this.GlobalState.PendingExternalKey))
                {
                  if (this.GlobalState.CancelAction.OrdinalEquals(this.Action))
                  {
                    this.IsActionRemovalPending = true;
                    needCommit = false;
                    return new SyImportProcessor.SyStep(previousStep1, this.ExternalValues);
                  }
                }
                else
                {
                  needCommit = true;
                  if (this.GlobalState.CancelAction == null)
                    this.GlobalState.CancelAction = this.Action;
                }
              }
            }
            view.IsPending = true;
            previousStep1.IsPending = true;
            break;
          case SyCommandType.NewRow:
            if (view.StartRow == null || view.StartRow.StartsWith("RowNumber_"))
            {
              view.NewRow = true;
              view.DeleteRow = false;
              view.StartRow = (string) null;
              break;
            }
            break;
          case SyCommandType.DeleteRow:
            view.DeleteRow = true;
            view.NewRow = false;
            view.IsPending = true;
            previousStep1.IsPending = true;
            break;
          case SyCommandType.RowNumber:
            view.StartRow = cmd.Formula;
            view.NewRow = false;
            view.RowFilterField = cmd.Field;
            break;
          case SyCommandType.RowCount:
            view.TotalRow = cmd.Formula;
            view.NewRow = false;
            view.RowFilterField = cmd.Field;
            break;
          case SyCommandType.Path:
            view.Path = cmd.Formula;
            view.PathField = cmd.Field;
            if (cmd.Commit)
            {
              view.IsPending = true;
              previousStep1.IsPending = true;
            }
            if (previousStep1.PreviousStep != null)
            {
              previousStep1.PreviousStep.IsActionRemovalPending = true;
              break;
            }
            break;
          case SyCommandType.Answer:
            view.AnswerFormula = cmd.Formula;
            if (cmd.Commit)
            {
              view.IsPending = true;
              previousStep1.IsPending = true;
              break;
            }
            break;
          case SyCommandType.CustomDelegate:
            this.ProcessLocateByNoteIdCommand(cmd, view, true);
            needCommit = false;
            break;
          case SyCommandType.LocateByNoteId:
            this.ProcessLocateByNoteIdCommand(cmd, view, false);
            needCommit = false;
            break;
          case SyCommandType.PossibleNewRow:
            this.ProcessPossibleNewRow(cmd, view);
            needCommit = false;
            break;
          case SyCommandType.SetBranch:
            view.BranchFormula = cmd.Formula;
            view.IsPending = true;
            previousStep1.IsPending = true;
            break;
        }
        if (cmd.Commit)
        {
          needCommit = true;
          string str3 = (string) null;
          SyImportProcessor.SyStep previousStep2;
          for (previousStep2 = previousStep1.PreviousStep; previousStep2 != null && !previousStep2.IsActionRemovalPending; previousStep2 = previousStep2.PreviousStep)
          {
            if (str3 == null && previousStep2.Action != null)
              str3 = previousStep2.Action;
          }
          if (previousStep2 != null && previousStep2.IsActionRemovalPending)
          {
            if (str3 != null || !previousStep2.Action.OrdinalEquals(this.Action))
              previousStep2.ProcessThisAndPreviousSteps((System.Action<SyImportProcessor.SyStep>) (s => s.Action = (string) null));
            else
              previousStep2.ProcessThisAndPreviousSteps((System.Action<SyImportProcessor.SyStep>) (s => s.IsActionRemovalPending = false));
          }
        }
        return previousStep1;
      }
      catch
      {
        this.PreviousStep = (SyImportProcessor.SyStep) null;
        throw;
      }
    }

    private void ProcessLocateByNoteIdCommand(
      SyCommand cmd,
      SyImportProcessor.SyStep.SyView view,
      bool invokeCommand)
    {
      SyCommandCustomDelegate command = (SyCommandCustomDelegate) cmd;
      object currentRow;
      if (invokeCommand)
      {
        this.LocateRecord(command);
        command.Invoke(this.Graph);
        currentRow = this.Graph.Views[cmd.View].Cache.Current;
      }
      else
        currentRow = this.LocateRecord(command);
      if (currentRow == null && cmd.CommandType == SyCommandType.LocateByNoteId)
        throw new PXException($"The {command.Decriptor.Source} detail entity with the {command.Decriptor.NoteId} ID cannot be found. Probably the ID from another session is used.");
      if (this.IsPrimaryView(cmd.View))
        this.PrimaryResult = currentRow;
      this.CreateSearchesForCurrentRow(cmd.View, currentRow, view);
    }

    private void ProcessPossibleNewRow(SyCommand cmd, SyImportProcessor.SyStep.SyView view)
    {
      object currentRow = this.LocateRecordByNoteId((SyCommandCustomDelegate) cmd);
      if (currentRow == null)
      {
        if (view.StartRow != null && !view.StartRow.StartsWith("RowNumber_"))
          return;
        view.NewRow = true;
        view.DeleteRow = false;
        view.StartRow = (string) null;
        cmd.Commit = false;
      }
      else
        this.CreateSearchesForCurrentRow(cmd.View, currentRow, view);
    }

    private void CreateSearchesForCurrentRow(
      string viewName,
      object currentRow,
      SyImportProcessor.SyStep.SyView view)
    {
      foreach (string keyName in this.Graph.GetKeyNames(viewName))
      {
        object valueExt = SyImportProcessor.ExportTableHelper.GetValueExt(this.Graph, currentRow, viewName, keyName, false, false, out PXFieldState _, out PXDBLocalizableStringAttribute.Translations _);
        view.Searches[keyName] = valueExt == null ? "=Null" : $"='{valueExt.ToString().Replace("'", "\\'")}'";
      }
    }

    private object LocateRecord(SyCommandCustomDelegate command)
    {
      object obj1 = (object) null;
      if (command.isNewEntity)
        return (object) null;
      if (command.Decriptor.Items != null && command.Decriptor.Items.Length != 0)
      {
        foreach (IGrouping<string, KeyValuePair<string, string>> grouping in ((IEnumerable<KeyValuePair<string, string>>) command.Decriptor.Items).GroupBy<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (i => EntityToGuidBindService.ParseKey(i.Key).Key)))
        {
          try
          {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            PXView view = this.Graph.Views[grouping.Key];
            foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) grouping)
            {
              string str = StringExtensions.FirstSegment(EntityToGuidBindService.ParseKey(keyValuePair.Key).Value, '_');
              bool? isListBox = new bool?();
              object obj2 = SyImportProcessor.SyStep.SyView.AdjustExternal(view.Cache, (object) null, false, str, (object) keyValuePair.Value, ref isListBox, (SyImportProcessor.SyStep.SyField) null, view.Name);
              dictionary.Add(str, obj2);
            }
            PXFilterRow[] array = dictionary.Select<KeyValuePair<string, object>, PXFilterRow>((Func<KeyValuePair<string, object>, PXFilterRow>) (c => new PXFilterRow(c.Key, PXCondition.EQ, c.Value)
            {
              UseExt = true
            })).ToArray<PXFilterRow>();
            int startRow = 0;
            int totalRows = 0;
            view.prepareFilters(ref array);
            object obj3 = PXResult.UnwrapFirst(view.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, array, ref startRow, 1, ref totalRows).FirstOrDefault<object>());
            object obj4;
            if (obj3 != null)
            {
              obj4 = view.Cache.Current = view.Cache.Locate(obj3);
            }
            else
            {
              view.Cache.Locate((IDictionary) dictionary);
              obj4 = view.Cache.Current;
            }
            if (grouping.Key.OrdinalEquals(command.View))
              obj1 = obj4;
          }
          catch
          {
            return (object) null;
          }
        }
      }
      else
        obj1 = this.LocateRecordByNoteId(command);
      return obj1;
    }

    private object LocateRecordByNoteId(SyCommandCustomDelegate command)
    {
      object obj = (object) null;
      if (command.Decriptor.NoteId.HasValue)
      {
        if (!string.IsNullOrEmpty(command.Decriptor.NoteView))
        {
          try
          {
            this.Graph.Views[command.Decriptor.NoteView].Cache.LocateByNoteID(command.Decriptor.NoteId.Value);
            obj = this.Graph.Views[command.Decriptor.NoteView].Cache.Current;
          }
          catch
          {
            return obj;
          }
        }
      }
      return obj;
    }

    private void UpdateViewsRevision()
    {
      foreach (SyImportProcessor.SyStep.SyView syView in this.Views.Values)
        syView.UpdateRevision();
    }

    internal void ProcessThisAndPreviousSteps(System.Action<SyImportProcessor.SyStep> action)
    {
      for (SyImportProcessor.SyStep syStep = this; syStep != null; syStep = syStep.PreviousStep)
        action(syStep);
    }

    private List<SyImportProcessor.SyStep> GetPreviousSteps()
    {
      List<SyImportProcessor.SyStep> steps = new List<SyImportProcessor.SyStep>();
      this.PreviousStep?.ProcessThisAndPreviousSteps((System.Action<SyImportProcessor.SyStep>) (step => steps.Add(step)));
      return steps;
    }

    private void CommitChangesInt(
      object itemToBypass,
      PXFilterRow[] targetConditions,
      PXFilterRow[] filtersForAction,
      SyImportRowResult importResult)
    {
      foreach (SyImportProcessor.SyStep.SyView syView in this.Views.Values)
      {
        if (!syView.IsPending && syView.Previous != null && syView.Current == null)
          syView.Current = syView.Previous;
      }
      bool flag1;
      if (SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) && this.PrimaryItem != null)
      {
        itemToBypass = this.PrimaryItem;
        flag1 = true;
      }
      else
        flag1 = this.CheckShouldBeBypassed(itemToBypass);
      if (!this.IsPending)
      {
        if (flag1)
        {
          this.UpdateViewsRevision();
          this.GlobalState.MinRowNumber = this.RowNumberLocal + 1;
          throw new SyImportProcessor.DetailBypassedException("The record was not processed because of an error during processing of the previous record");
        }
      }
      else
      {
        List<string> list = this.Graph.GetViewNames().ToList<string>();
        int index1 = list.FindIndex((Predicate<string>) (viewName => viewName.OrdinalEquals(this.PrimaryView)));
        if (index1 != -1)
        {
          try
          {
            for (int index2 = 0; index2 < list.Count; ++index2)
            {
              string str1 = list[(index1 + index2) % list.Count];
              SyImportProcessor.SyStep.SyView syView;
              if (this.Views.TryGetValue(str1, out syView))
              {
                syView.UpdateRevision();
                syView.CalculateExternal();
                WebDialogResult? answer = syView.Answer;
                if (answer.HasValue)
                {
                  PXView view = this.Graph.Views[str1];
                  answer = syView.Answer;
                  int num = (int) answer.Value;
                  view.Answer = (WebDialogResult) num;
                  if (index2 == 0)
                  {
                    answer = syView.Answer;
                    this.InitDetailViewsAnswer(answer.Value);
                  }
                }
                if (syView.IsPending)
                {
                  PXCache cache = this.Graph.Views[str1].Cache;
                  string[] parameterNames = this.Graph.GetParameterNames(str1);
                  string externalKey = (string) null;
                  List<object> objectList = new List<object>();
                  List<string> sorts = new List<string>();
                  bool flag2 = false;
                  int startRow = 0;
                  if (syView.Path != null)
                  {
                    string[] strArray1;
                    if (!(this.FormulaProcessor.Evaluate(syView.Path, this.GetExternal) is string str2))
                    {
                      strArray1 = new string[0];
                    }
                    else
                    {
                      char[] chArray = new char[1]{ '/' };
                      strArray1 = str2.Split(chArray);
                    }
                    string[] strArray2 = strArray1;
                    if (strArray2.Length != 0)
                    {
                      object[] objArray = new object[parameterNames.Length];
                      object[] searches = new object[1];
                      string[] sortcolumns = new string[1]
                      {
                        syView.PathField
                      };
                      bool evaluateParameters = false;
                      syView.Current = (object) null;
                      foreach (string str3 in strArray2)
                      {
                        searches[0] = string.IsNullOrEmpty(str3) ? (object) (string) null : (object) str3;
                        this.EvaluateFields(syView, cache, parameterNames, objArray, evaluateParameters);
                        evaluateParameters = true;
                        object foundRow;
                        if (this.SelectDataRecordWithoutTail(syView, str1, objArray, searches, sortcolumns, (PXFilterRow[]) null, ref startRow, out foundRow))
                          syView.Current = PXResult.UnwrapFirst(foundRow);
                        if (syView.Current != null)
                          syView.RecordInternal(cache, true, false, importResult);
                        else
                          break;
                      }
                      continue;
                    }
                  }
                  if (syView.BranchFormula != null)
                  {
                    this.SetBranch(syView.BranchFormula);
                  }
                  else
                  {
                    object[] parameters = this.EvaluateParameters(syView, cache, parameterNames);
                    if (syView.InsertCalled)
                    {
                      if (syView.Searches.Count == 0)
                        syView.KeysFromCurrent = syView.MinRevision;
                      syView.InsertCalled = false;
                    }
                    if (syView.KeysFromCurrent < syView.MinRevision)
                    {
                      object data = (object) null;
                      foreach (KeyValuePair<string, string> search in syView.Searches)
                      {
                        if (cache.Current == null && data == null)
                        {
                          data = cache.CreateInstance();
                          syView.DummyCurrent = data;
                        }
                        object val = this.FormulaProcessor.Evaluate(search.Value, this.GetSearch);
                        bool? isListBox;
                        object obj = syView.AdjustExternal(cache, data ?? cache.Current, false, search.Key, val, out isListBox);
                        if (!flag2)
                        {
                          if (!isListBox.HasValue)
                          {
                            ref bool? local = ref isListBox;
                            int num;
                            if (cache.GetStateExt((object) null, search.Key) is PXFieldState stateExt)
                            {
                              switch (stateExt)
                              {
                                case PXStringState state1 when state1.HasFixedValuesList():
                                  num = 1;
                                  break;
                                case PXIntState state2:
                                  num = state2.HasFixedValuesList() ? 1 : 0;
                                  break;
                                default:
                                  num = 0;
                                  break;
                              }
                            }
                            else
                              num = 0;
                            local = new bool?(num != 0);
                          }
                          bool? nullable = isListBox;
                          bool flag3 = true;
                          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue || !cache.Keys.Contains(search.Key))
                            flag2 = true;
                        }
                        if (!this.TryUpdateExternalKey(cache, search.Key, obj, ref externalKey))
                        {
                          sorts.Add(search.Key);
                          if (data != null)
                          {
                            try
                            {
                              cache.SetValueExt(data, search.Key, obj);
                            }
                            catch (PXSetPropertyException ex)
                            {
                            }
                          }
                          objectList.Add(obj);
                        }
                      }
                    }
                    else if (syView.Current != null && (syView.StartRow == null || this.FormulaProcessor.Evaluate(syView.StartRow, this.GetExternal) == null))
                    {
                      foreach (string key in (IEnumerable<string>) cache.Keys)
                      {
                        object obj = PXFieldState.UnwrapValue(cache.GetValueExt(syView.Current, key));
                        sorts.Add(key);
                        objectList.Add(obj);
                      }
                    }
                    if (syView.StartRow != null && !this.IsExport)
                      startRow = this.GetFormulaValueInt(syView.StartRow);
                    object current1 = syView.Current;
                    string str4 = (string) null;
                    bool flag4 = this.IsPrimaryView(str1);
                    if (flag4 && this.Action.OrdinalEquals("WorkflowTransition"))
                    {
                      str4 = this.FormulaProcessor.Evaluate(this.WorkflowAction, this.GetExternal) as string;
                      if (string.IsNullOrWhiteSpace(str4))
                        this.Action = (string) null;
                    }
                    object[] currents = (object[]) null;
                    if (flag4 && !cache.Graph.IsExport && !cache.Graph.IsMobile && cache.Current == null)
                      currents = new object[1]
                      {
                        syView.DummyCurrent
                      };
                    if (flag4 && this.Action != null)
                    {
                      object current2 = syView.Current;
                      syView.Current = (object) null;
                      syView.NewRow = false;
                      syView.DeleteRow = false;
                      string str5 = (string) null;
                      string str6;
                      if (this.Action.Contains("@"))
                      {
                        string[] strArray = this.Action.Split('@');
                        str5 = strArray[0];
                        str6 = strArray[1];
                      }
                      else
                        str6 = !string.IsNullOrEmpty(str4) ? str4 : this.Action;
                      PXAction action = this.Graph.Actions[str6];
                      if (action == null)
                      {
                        foreach (string suffix in this.StandardActionsSuffix)
                        {
                          if (str6.EndsWith(suffix))
                          {
                            str6 = str6.RemoveFromEnd(suffix);
                            action = this.Graph.Actions[str6];
                            break;
                          }
                        }
                      }
                      if (syView.WorkflowParameters.Count > 0)
                      {
                        this._workflow.FillFormValuesByAction(this.Graph, str6, EnumerableExtensions.ToDictionary<string, object>(syView.WorkflowParameters.Select<KeyValuePair<string, string>, KeyValuePair<string, object>>((Func<KeyValuePair<string, string>, KeyValuePair<string, object>>) (_ => new KeyValuePair<string, object>(_.Key, this.FormulaProcessor.Evaluate(_.Value, this.GetExternal))))));
                        syView.WorkflowParameters.Clear();
                      }
                      else if (!this.Graph.IsMobile)
                        this._workflow.ClearFormData(this.Graph);
                      PXAdapter adapter1 = new PXAdapter(this.Graph.Views[this.PrimaryView])
                      {
                        Parameters = parameters,
                        SortColumns = sorts.ToArray(),
                        Descendings = new bool[objectList.Count],
                        Searches = objectList.ToArray(),
                        StartRow = startRow,
                        MaximumRows = 1,
                        ExternalCall = true,
                        ImportFlag = true,
                        Menu = str5
                      };
                      if (filtersForAction != null)
                        adapter1.Filters = filtersForAction;
                      if (externalKey != null)
                      {
                        if (adapter1.Filters == null)
                          adapter1.Filters = new PXFilterRow[1]
                          {
                            SyImportProcessor.SyStep.GetExternalKeyFilter(externalKey)
                          };
                        else
                          adapter1.Filters = ((IEnumerable<PXFilterRow>) EnumerableExtensions.Append<PXFilterRow>(adapter1.Filters, SyImportProcessor.SyStep.GetExternalKeyFilter(externalKey))).ToArray<PXFilterRow>();
                      }
                      if (!flag1)
                      {
                        if (SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) && this.PrimaryItem != null)
                          itemToBypass = this.PrimaryItem;
                        if (!SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) && itemToBypass != null && !(itemToBypass is IDictionary<string, object>) && cache.GetItemType().IsAssignableFrom(itemToBypass.GetType()) && cache.Locate(itemToBypass) != null)
                          cache.Hold(itemToBypass);
                        if (action == null)
                          throw new PXException("The {0} action cannot be found.", new object[1]
                          {
                            (object) this.Action
                          });
                        try
                        {
                          if (!(this.Graph.IsExport | flag2) && !SyImportProcessor.SyStep.IsActionCancel(action))
                          {
                            if (!((IEnumerable<object>) adapter1.Searches).All<object>((Func<object, bool>) (_ => _ != null)))
                              continue;
                          }
                          bool flag5 = externalKey == null;
                          try
                          {
                            try
                            {
                              HashSet<object> objectSet = new HashSet<object>();
                              foreach (object obj in cache.Inserted)
                                objectSet.Add(obj);
                              if (this.PressButton(action, adapter1, syView, str6, currents))
                                flag5 = true;
                              if (flag5)
                              {
                                if (syView.Searches.Count == 0)
                                {
                                  if (syView.Current != null)
                                  {
                                    if (cache.GetStatus(syView.Current) == PXEntryStatus.Inserted)
                                    {
                                      if (!objectSet.Contains(syView.Current))
                                        syView.InsertCalled = true;
                                    }
                                  }
                                }
                              }
                            }
                            catch (PXActionDisabledException ex)
                            {
                              SyImportProcessor.SyStep.SyField syField;
                              if (this.ActionTriggerField != null && syView.Fields.TryGetValue(this.ActionTriggerField, out syField))
                              {
                                syView.RecordState(cache, (object) null);
                                if (syField.Enabled)
                                  throw;
                                flag5 = true;
                              }
                              else
                                throw;
                            }
                            if (flag5 && adapter1.Searches != null && objectList.Zip<object, object, KeyValuePair<object, object>>((IEnumerable<object>) adapter1.Searches, (Func<object, object, KeyValuePair<object, object>>) ((k, v) => new KeyValuePair<object, object>(k, v))).Any<KeyValuePair<object, object>>((Func<KeyValuePair<object, object>, bool>) (_ => !object.Equals(_.Key, _.Value))))
                            {
                              syView.EnsureFields(adapter1.SortColumns, adapter1.Searches, syView.Current);
                              syView.RecordInternal(cache, false, false, importResult);
                            }
                            if (flag5)
                            {
                              if (adapter1.SortColumns != null)
                              {
                                if (sorts != null)
                                {
                                  if (adapter1.SortColumns.Length == sorts.Count)
                                  {
                                    if (((IEnumerable<string>) adapter1.SortColumns).Except<string>((IEnumerable<string>) sorts).Count<string>() != 0)
                                      sorts = ((IEnumerable<string>) adapter1.SortColumns).ToList<string>();
                                  }
                                }
                              }
                            }
                          }
                          catch (FormatException ex)
                          {
                            this.TryThrowConversionException(cache, adapter1.SortColumns, adapter1.Searches);
                          }
                          if (!flag5)
                          {
                            object rowInserted;
                            if (this.GlobalState.TryGetInsertedExternalRow(cache, externalKey, out rowInserted))
                            {
                              PXAdapter adapter2 = new PXAdapter((PXView) new PXView.Dummy(cache.Graph, adapter1.View.BqlSelect, new List<object>()
                              {
                                rowInserted
                              }));
                              if (filtersForAction != null)
                                adapter2.Filters = filtersForAction;
                              this.PressButton(action, adapter2, syView, str6, currents);
                            }
                          }
                          else if (externalKey != null)
                          {
                            if (cache.GetStatus(syView.Current) == PXEntryStatus.Inserted)
                            {
                              if (PXNoteAttribute.ImportEnsureNewNoteID(cache, cache.Current, externalKey))
                                this.GlobalState.AddInsertedExternalRow(cache, externalKey, cache.Current);
                            }
                          }
                        }
                        catch
                        {
                          if (current2 != itemToBypass)
                            syView.Current = current2;
                          throw;
                        }
                      }
                      else
                      {
                        try
                        {
                          bool flag6 = externalKey == null;
                          if (this.PressButton(action, adapter1, cache, syView, objectList, itemToBypass, str6, currents))
                            flag6 = true;
                          if (!flag6)
                          {
                            object rowInserted;
                            if (this.GlobalState.TryGetInsertedExternalRow(cache, externalKey, out rowInserted))
                            {
                              PXAdapter adapter3 = new PXAdapter((PXView) new PXView.Dummy(cache.Graph, adapter1.View.BqlSelect, new List<object>()
                              {
                                rowInserted
                              }));
                              if (filtersForAction != null)
                                adapter3.Filters = filtersForAction;
                              this.PressButton(action, adapter3, cache, syView, objectList, itemToBypass, str6, currents);
                            }
                          }
                          else if (externalKey != null)
                          {
                            if (cache.GetStatus(syView.Current) == PXEntryStatus.Inserted)
                            {
                              if (PXNoteAttribute.ImportEnsureNewNoteID(cache, cache.Current, externalKey))
                                this.GlobalState.AddInsertedExternalRow(cache, externalKey, cache.Current);
                            }
                          }
                        }
                        catch
                        {
                          if (!SyImportProcessor.SyStep.IsAnyBypass(itemToBypass))
                          {
                            if (!(itemToBypass is IDictionary))
                              syView.Current = itemToBypass;
                          }
                        }
                      }
                    }
                    else if (syView.NewRow)
                    {
                      syView.Previous = syView.Current;
                      syView.Current = (object) null;
                      syView.KeysFromCurrent = syView.MaxRevision;
                    }
                    else if (syView.KeysFromCurrent < syView.MinRevision || syView.Current != null)
                    {
                      object current3 = syView.Current;
                      try
                      {
                        bool flag7 = externalKey == null;
                        syView.Current = (object) null;
                        try
                        {
                          object obj = (object) null;
                          if (cache.Keys.All<string>((Func<string, bool>) (k => sorts.Contains(k))))
                          {
                            try
                            {
                              object instance = cache.CreateInstance();
                              for (int index3 = 0; index3 < sorts.Count; ++index3)
                                cache.SetValue(instance, sorts[index3], objectList[index3]);
                              object data = cache.Locate(instance);
                              bool flag8 = true;
                              for (int index4 = 0; index4 < sorts.Count; ++index4)
                              {
                                if (!object.Equals(cache.GetValue(instance, sorts[index4]), cache.GetValue(data, sorts[index4])))
                                {
                                  flag8 = false;
                                  break;
                                }
                              }
                              if (flag8)
                              {
                                obj = data;
                                if (str1 == this.Graph.PrimaryView)
                                  this.Graph.ApplyWorkflowState(obj);
                              }
                            }
                            catch
                            {
                            }
                          }
                          if (obj != null)
                          {
                            flag7 = true;
                            cache.RaiseRowSelected(obj);
                          }
                          else
                          {
                            PXFilterRow[] pxFilterRowArray;
                            if (externalKey != null)
                              pxFilterRowArray = new PXFilterRow[1]
                              {
                                SyImportProcessor.SyStep.GetExternalKeyFilter(externalKey)
                              };
                            else
                              pxFilterRowArray = (PXFilterRow[]) null;
                            PXFilterRow[] filters = pxFilterRowArray;
                            object foundRow;
                            if (this.SelectDataRecordWithoutTail(syView, str1, parameters, objectList.ToArray(), sorts.ToArray(), filters, ref startRow, out foundRow))
                            {
                              flag7 = true;
                              obj = foundRow;
                            }
                          }
                          if (flag7)
                          {
                            if (obj != null)
                            {
                              syView.Current = PXResult.UnwrapFirst(obj);
                              if (!SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) & flag1)
                              {
                                if (cache.ObjectsEqual(syView.Current, itemToBypass))
                                  syView.Current = itemToBypass;
                              }
                            }
                          }
                        }
                        catch (FormatException ex)
                        {
                          this.TryThrowConversionException(cache, sorts.ToArray(), objectList.ToArray());
                        }
                        if (!flag7)
                        {
                          object rowInserted;
                          if (this.GlobalState.TryGetInsertedExternalRow(cache, externalKey, out rowInserted))
                          {
                            if (cache.GetStatus(rowInserted) == PXEntryStatus.Inserted)
                            {
                              syView.Current = rowInserted;
                              if (!SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) & flag1)
                              {
                                if (cache.ObjectsEqual(syView.Current, itemToBypass))
                                  syView.Current = itemToBypass;
                              }
                            }
                          }
                        }
                      }
                      catch
                      {
                        syView.Current = current3;
                        if (!this.CheckShouldBeBypassed(itemToBypass))
                          throw;
                      }
                    }
                    if (this.CheckShouldBeBypassed(itemToBypass))
                    {
                      this.GlobalState.MinRowNumber = this.RowNumberLocal + 1;
                      this.IsPending = false;
                      if (!this.Views.Any<KeyValuePair<string, SyImportProcessor.SyStep.SyView>>())
                        return;
                      syView.UpdateRevision();
                      return;
                    }
                    if (syView.Current != null && (!flag2 || objectList.All<object>((Func<object, bool>) (c => c != null))))
                    {
                      if (flag4 && targetConditions != null && targetConditions.Length != 0 && syView.Current != current1 && (cache.GetStatus(syView.Current) == PXEntryStatus.Notchanged || cache.GetStatus(syView.Current) == PXEntryStatus.Held))
                      {
                        PXFilterRow[] filters = externalKey == null ? targetConditions : ((IEnumerable<PXFilterRow>) EnumerableExtensions.Append<PXFilterRow>(targetConditions, SyImportProcessor.SyStep.GetExternalKeyFilter(externalKey))).ToArray<PXFilterRow>();
                        bool flag9 = this.SelectDataRecordWithoutTail(syView, this.PrimaryView, parameters, objectList.ToArray(), sorts.ToArray(), filters, ref startRow, out object _);
                        if (!flag9)
                        {
                          object rowInserted;
                          if (externalKey != null && this.GlobalState.TryGetInsertedExternalRow(cache, externalKey, out rowInserted) && cache.GetStatus(rowInserted) == PXEntryStatus.Inserted)
                            flag9 = true;
                          if (!flag9)
                          {
                            syView.RecordInternal(cache, true, true, importResult);
                            if (externalKey != null)
                            {
                              this.GlobalState.CommittedExternalKey = externalKey;
                              this.GlobalState.PendingExternalKey = (string) null;
                            }
                            this.IsPending = false;
                            throw new SyImportProcessor.InvalidTargetException();
                          }
                        }
                      }
                      syView.RecordInternal(cache, false, false, importResult);
                      syView.CalculateExternal();
                    }
                    else
                      syView.RecordState(cache, syView.Current);
                    if (syView.CalculatePending())
                    {
                      bool hasKeys;
                      OrderedDictionary keys = syView.PrepareKeys(cache, out hasKeys);
                      bool hasValues;
                      OrderedDictionary orderedDictionary = syView.PrepareValues(cache, out hasValues);
                      if (hasKeys | hasValues)
                      {
                        int num = 0;
                        if (syView.DeleteRow)
                        {
                          if (this.Graph.ExecuteDelete(str1, (IDictionary) keys, (IDictionary) orderedDictionary) == 0 && !syView.IsPartial(cache, orderedDictionary))
                            throw new PXException("The system failed to commit the {0} row.", new object[1]
                            {
                              (object) str1
                            });
                          syView.Current = (object) null;
                        }
                        else
                        {
                          if (!syView.AnyUpdate(cache, keys, orderedDictionary))
                          {
                            num = cache.Locate((IDictionary) keys);
                            if (num != 0 && cache.GetStatus(cache.Current) == PXEntryStatus.Notchanged)
                              cache.SetStatus(cache.Current, syView.NewRow ? PXEntryStatus.Inserted : PXEntryStatus.Held);
                          }
                          if (num == 0)
                          {
                            HashSet<object> objectSet = new HashSet<object>(cache.Inserted.Cast<object>());
                            if (objectSet.Count > 0 || !flag4 || SyImportProcessor.SyStep.HasRealValues(orderedDictionary))
                            {
                              if (this.Graph.AllowUpdate(str1))
                                num = this.Graph.ExecuteUpdate(str1, (IDictionary) keys, (IDictionary) orderedDictionary);
                              if (num == 0)
                              {
                                bool flag10 = syView.IsPartial(cache, orderedDictionary);
                                if (!flag10 & flag4 && (orderedDictionary.Count > 2 || !orderedDictionary.Contains((object) "CuryViewState")))
                                  throw new PXException("The system failed to commit the {0} row.", new object[1]
                                  {
                                    (object) str1
                                  });
                                if ((keys.Count != 0 ? 0 : (((IEnumerable<string>) this.Graph.GetKeyNames(str1)).Any<string>() ? 1 : 0)) != 0)
                                {
                                  if (this.Graph.AllowInsert(str1))
                                    throw new PXException("The system failed to commit the {0} row.", new object[1]
                                    {
                                      (object) str1
                                    });
                                }
                                else if (!flag10 && this.Graph.AllowUpdate(str1))
                                  throw new PXException("The system failed to commit the {0} row.", new object[1]
                                  {
                                    (object) str1
                                  });
                              }
                              else
                              {
                                if (cache.GetStatus(cache.Current) == PXEntryStatus.Inserted)
                                {
                                  if (!objectSet.Contains(cache.Current))
                                    syView.KeysFromCurrent = syView.MaxRevision;
                                  if (externalKey != null && PXNoteAttribute.ImportEnsureNewNoteID(cache, cache.Current, externalKey))
                                    this.GlobalState.AddInsertedExternalRow(cache, externalKey, cache.Current);
                                }
                                syView.CommitFiles(cache, orderedDictionary, false);
                                OrderedDictionary values = new OrderedDictionary();
                                foreach (DictionaryEntry dictionaryEntry in orderedDictionary)
                                {
                                  SyImportProcessor.SyStep.SyField syField;
                                  object returnValue;
                                  if (dictionaryEntry.Value is PXFieldState pxFieldState && !pxFieldState.Enabled && syView.Fields.TryGetValue((string) dictionaryEntry.Key, out syField) && syField.CheckUpdatability && syField.Visible && !syField.Enabled && orderedDictionary.TryGetValue((object) (dictionaryEntry.Key?.ToString() + "_OriginalValue"), out returnValue) && !object.Equals(returnValue, cache.GetValue(cache.Current, (string) dictionaryEntry.Key)))
                                  {
                                    if (returnValue != null)
                                    {
                                      cache.RaiseFieldSelecting((string) dictionaryEntry.Key, cache.Current, ref returnValue, false);
                                      PXFieldState.UnwrapValue(returnValue);
                                    }
                                    if (!object.Equals(pxFieldState.Value, returnValue))
                                      values[dictionaryEntry.Key] = returnValue;
                                  }
                                }
                                if (values.Count > 0)
                                  num = this.Graph.ExecuteUpdate(str1, (IDictionary) keys, (IDictionary) values);
                              }
                            }
                            else
                            {
                              syView.CommitNotes(cache, orderedDictionary, importResult);
                              syView.CommitFiles(cache, orderedDictionary, true);
                            }
                          }
                          syView.Current = cache.Current;
                          syView.RecordInternal(cache, num == 1, false, importResult);
                          if (externalKey != null & flag4)
                          {
                            this.GlobalState.CommittedExternalKey = externalKey;
                            this.GlobalState.PendingExternalKey = (string) null;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          catch
          {
            this.UpdateViewsRevision();
            throw;
          }
        }
        this.IsPending = false;
      }
    }

    private void SetBranch(string branchFormula)
    {
      string branch = this.FormulaProcessor.Evaluate(branchFormula, this.GetExternal) as string;
      this.Graph.Accessinfo.BranchID = new int?((this._currentUserInformationProvider.GetActiveBranches().FirstOrDefault<BranchInfo>((Func<BranchInfo, bool>) (b => b.Cd.Trim().OrdinalEquals(branch) || b.Name.Trim().OrdinalEquals(branch))) ?? throw new PXException("The {0} branch cannot be found in the system.", new object[1]
      {
        (object) branch
      })).Id);
      PXContext.SetBranchID(this.Graph.Accessinfo.BranchID);
    }

    private void EvaluateFields(
      SyImportProcessor.SyStep.SyView view,
      PXCache cache,
      string[] paramNames,
      object[] pars,
      bool evaluateParameters)
    {
      int length = paramNames.Length;
      for (int index = 0; index < length; ++index)
      {
        string paramName = paramNames[index];
        string parameter;
        if (view.Parameters.TryGetValue(paramName, out parameter))
        {
          SyImportProcessor.SyStep.SyField syField;
          if (evaluateParameters && view.Fields.TryGetValue(paramName, out syField))
            pars[index] = this.EvaluateParameter(view, cache, parameter);
          else
            view.Fields[paramName] = syField = new SyImportProcessor.SyStep.SyField();
          syField.Formula = parameter;
          syField.IsPending = true;
        }
      }
    }

    private object[] EvaluateParameters(
      SyImportProcessor.SyStep.SyView view,
      PXCache cache,
      string[] paramNames)
    {
      object[] parameters = new object[paramNames.Length];
      for (int index = 0; index < parameters.Length; ++index)
      {
        string parameter;
        if (view.Parameters.TryGetValue(paramNames[index], out parameter))
          parameters[index] = this.EvaluateParameter(view, cache, parameter);
      }
      return parameters;
    }

    private object EvaluateParameter(
      SyImportProcessor.SyStep.SyView view,
      PXCache cache,
      string parameter)
    {
      return this.FormulaProcessor.Evaluate(parameter, (SyFormulaFinalDelegate) (names =>
      {
        object val = this.GetSearch(names);
        if (val == null || val is string && string.IsNullOrWhiteSpace((string) val))
          return val;
        string name;
        SyImportProcessor.SyStep.SyView syView;
        PXCache cache1;
        switch (names.Length)
        {
          case 1:
            name = names[0];
            syView = view;
            cache1 = cache;
            break;
          case 2:
            if (!this.Views.TryGetValue(names[0], out syView))
              return val;
            cache1 = this.Graph.Views[names[0]].Cache;
            name = names[1];
            break;
          default:
            return val;
        }
        return string.IsNullOrWhiteSpace(name) || !syView.Fields.ContainsKey(name) ? val : view.AdjustExternal(cache1, cache1.Current, false, name, val);
      }));
    }

    private bool PressButton(
      PXAction button,
      PXAdapter adapter,
      SyImportProcessor.SyStep.SyView view,
      string actionName,
      object[] currents)
    {
      bool flag = false;
      using (SyImportProcessor.PXInsertRightsScope insertRightsScope = new SyImportProcessor.PXInsertRightsScope(adapter.View.Cache))
      {
        using (new PXCustomizedActionScope(actionName))
        {
          if (SyImportProcessor.SyStep.GrantInsertRightsToAction(button))
          {
            adapter.Currents = currents;
            if (this.IsExport)
              insertRightsScope.SetRightsOnCacheAndKeyFields();
          }
          else if (this.IsExport)
            insertRightsScope.CheckRights(button, actionName);
          foreach (object row in button.Press(adapter))
          {
            view.Current = PXResult.UnwrapFirst(row);
            this.PrimaryResult = row;
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool PressButton(
      PXAction button,
      PXAdapter adapter,
      PXCache cache,
      SyImportProcessor.SyStep.SyView view,
      List<object> searches,
      object itemToBypass,
      string actionName,
      object[] currents)
    {
      bool flag1 = false;
      using (SyImportProcessor.PXInsertRightsScope insertRightsScope = new SyImportProcessor.PXInsertRightsScope(adapter.View.Cache))
      {
        using (new PXCustomizedActionScope(actionName))
        {
          if (SyImportProcessor.SyStep.GrantInsertRightsToAction(button) && this.IsExport)
            insertRightsScope.SetRightsOnCacheAndKeyFields();
          else if (this.IsExport)
            insertRightsScope.CheckRights(button, actionName);
          bool flag2 = SyImportProcessor.SyStep.IsActionCancel(button);
          foreach (object row in button.Press(adapter))
          {
            flag1 = true;
            view.Current = PXResult.UnwrapFirst(row);
            if (!SyImportProcessor.SyStep.IsAnyBypass(itemToBypass) && cache.ObjectsEqual(itemToBypass, view.Current))
            {
              bool flag3 = true;
              if (flag2)
              {
                cache._Current = currents?[0] ?? cache.Current;
                adapter.Currents = currents;
              }
              if (flag2 && adapter.SortColumns != null && searches != null)
              {
                int length = adapter.SortColumns.Length;
                for (int index = 0; index < length; ++index)
                {
                  string sortColumn = adapter.SortColumns[index];
                  SyImportProcessor.SyStep.SyField syField;
                  if (sortColumn != null && searches.Count > index && searches[index] != null && view.Fields.TryGetValue(sortColumn, out syField) && object.Equals(syField.ExternalValue, searches[index]) && syField.CommittedExternalValue != null && !object.Equals(syField.ExternalValue, syField.CommittedExternalValue))
                  {
                    flag3 = false;
                    break;
                  }
                }
              }
              if (flag3)
                view.Current = itemToBypass;
            }
          }
        }
      }
      return flag1;
    }

    private bool SelectDataRecordWithoutTail(
      SyImportProcessor.SyStep.SyView syView,
      string viewName,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      PXFilterRow[] filters,
      ref int startRow,
      out object foundRow)
    {
      PXView view = this.Graph.Views[viewName];
      bool supressTailSelect = view.SupressTailSelect;
      bool flag = false;
      System.Type[] tables = view.BqlSelect?.GetTables();
      if (tables != null && tables.Length > 1)
      {
        foreach (string key in syView.Fields.Keys)
        {
          int length = key.IndexOf("__");
          if (length != -1)
          {
            string b = key.Substring(0, length);
            for (int index = 1; index < tables.Length; ++index)
            {
              if (tables[index].Name.OrdinalEquals(b))
              {
                flag = true;
                break;
              }
            }
            if (flag)
              break;
          }
        }
      }
      if (!flag)
        view.SupressTailSelect = true;
      try
      {
        int totalRows = 0;
        IEnumerator enumerator = this.Graph.ExecuteSelect(viewName, parameters, searches, sortcolumns, (bool[]) null, filters, ref startRow, 1, ref totalRows).GetEnumerator();
        try
        {
          if (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            foundRow = current;
            return true;
          }
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
        foundRow = (object) null;
        return false;
      }
      finally
      {
        view.SupressTailSelect = supressTailSelect;
      }
    }

    internal static bool SelectDataRecord(
      PXGraph graph,
      string viewName,
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      out object foundRow)
    {
      int totalRows = 0;
      IEnumerator enumerator = graph.ExecuteSelect(viewName, currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, 1, ref totalRows).GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          object current = enumerator.Current;
          foundRow = current;
          return true;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      foundRow = (object) null;
      return false;
    }

    private void InitDetailViewsAnswer(WebDialogResult answer)
    {
      if (this.GlobalState.IsAnswersInitialized)
        return;
      this.GlobalState.IsAnswersInitialized = true;
      foreach (PXView pxView in this.Graph.Views.Values.Where<PXView>((Func<PXView, bool>) (v => v.Name != null && !v.Name.StartsWith("_") && !v.Name.StartsWith("$") && !v.Name.Equals(this.PrimaryView, StringComparison.Ordinal))).ToArray<PXView>())
        pxView.Answer = answer;
      foreach (PXAction pxAction in (IEnumerable) this.Graph.Actions.Values)
        pxAction.ClearAnswerAfterPress = false;
    }

    private void TryThrowConversionException(
      PXCache cache,
      string[] sortColumns,
      object[] searches)
    {
      for (int index = 0; index < sortColumns.Length; ++index)
      {
        if (cache.GetStateExt((object) null, sortColumns[index]) is PXFieldState stateExt)
        {
          object newValue = searches[index];
          if (newValue != null)
          {
            string str = newValue.ToString();
            try
            {
              cache.RaiseFieldUpdating(sortColumns[index], cache.CreateInstance(), ref newValue);
            }
            catch
            {
              newValue = (object) null;
            }
            if (newValue == null)
              throw new PXException("Field {0}: Failed to convert the value {1} to the type {2}.", new object[3]
              {
                (object) stateExt.DisplayName,
                (object) str,
                (object) stateExt.DataType.Name
              });
          }
        }
      }
    }

    private bool TryUpdateExternalKey(
      PXCache cache,
      string field,
      object value,
      ref string externalKey)
    {
      if (!SyImportProcessor.SyStep.IsNoteIdKey(field) || cache.Keys.Count == 1 && SyImportProcessor.SyStep.IsNoteIdKey(cache.Keys[0]) && SyImportProcessor.SyStep.IsGuid(value))
        return false;
      if (externalKey != null)
        externalKey += SYData.FIELD_SEPARATOR.ToString();
      externalKey += value != null ? value.ToString() : this.GetSimplifiedKey();
      return true;
    }

    public bool IsPrimaryView(string viewName) => viewName.OrdinalEquals(this.PrimaryView);

    internal static bool IsNoteIdKey(string fieldName) => fieldName.OrdinalEquals("NoteID");

    internal static bool IsGuid(object value)
    {
      switch (value)
      {
        case Guid _:
          return true;
        case string input:
          return Guid.TryParse(input, out Guid _);
        default:
          return false;
      }
    }

    internal static bool GrantInsertRightsToAction(PXAction action)
    {
      return action.GetState((object) null) is PXButtonState state && (state.SpecialType == PXSpecialButtonType.Cancel || (state.SpecialType == PXSpecialButtonType.Process || state.SpecialType == PXSpecialButtonType.ProcessAll ? 1 : (action.Attributes.Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => typeof (PXProcessButtonAttribute).IsAssignableFrom(a.GetType()))) ? 1 : 0)) != 0);
    }

    internal static bool IsActionCancel(PXAction action)
    {
      return SyImportProcessor.SyStep.IsActionCancel(action, out string _);
    }

    internal static bool IsActionCancel(PXAction action, out string name)
    {
      if (action.GetState((object) null) is PXButtonState state)
      {
        name = state.Name;
        return state.SpecialType == PXSpecialButtonType.Cancel;
      }
      name = (string) null;
      return false;
    }

    internal static IEnumerable<UploadFile> GetUploadFiles(
      PXGraph graph,
      PXCache cache,
      object row)
    {
      Guid? noteId = PXNoteAttribute.GetNoteID(cache, row, (string) null);
      if (noteId.HasValue && noteId.Value != Guid.Empty)
      {
        PXGraph graph1 = graph;
        object[] objArray = new object[1]{ (object) noteId };
        foreach (PXResult<NoteDoc, UploadFile> uploadFile in PXSelectBase<NoteDoc, PXSelectJoin<NoteDoc, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select(graph1, objArray))
          yield return (UploadFile) uploadFile;
      }
    }

    public static string WithUpperFirstLetter(string name)
    {
      return name.Substring(0, 1).ToUpper() + name.Substring(1);
    }

    public static string GetFileName(string fieldName) => fieldName.Substring(1);

    public static bool IsFileName(string fieldName) => fieldName.StartsWith("&");

    /// <summary>Gets the view with the specified name or creates new one and adds it to the views collection of this step.</summary>
    /// <returns>True if view with the specified name was found; False if new view was created and added to the collection.</returns>
    internal bool GetViewOrAddNew(string viewName, out SyImportProcessor.SyStep.SyView view)
    {
      int num = this.Views.TryGetValue(viewName, out view) ? 1 : 0;
      if (num != 0)
        return num != 0;
      this.Views.Add(viewName, view = new SyImportProcessor.SyStep.SyView(this, viewName));
      return num != 0;
    }

    internal void FillSearches(
      string viewName,
      SyImportProcessor.SyStep.SyView view,
      List<object> srchs,
      List<string> sorts,
      List<bool> descs)
    {
      PXView view1 = this.Graph.Views[viewName];
      KeyValuePair<string, bool>[] array = ((IEnumerable<KeyValuePair<string, bool>>) view1.GetSortColumns()).ToArray<KeyValuePair<string, bool>>();
      int count = sorts.Count;
      bool flag1 = false;
      foreach (KeyValuePair<string, string> search in view.Searches)
      {
        KeyValuePair<string, string> pair = search;
        if (!sorts.Any<string>((Func<string, bool>) (s => s.OrdinalEquals(pair.Key))))
        {
          sorts.Add(pair.Key);
          srchs.Add(view.AdjustExternal(view1.Cache, view1.Cache.Current, false, pair.Key, this.FormulaProcessor.Evaluate(pair.Value, this.GetInternal)));
          KeyValuePair<string, bool> keyValuePair = ((IEnumerable<KeyValuePair<string, bool>>) array).SingleOrDefault<KeyValuePair<string, bool>>((Func<KeyValuePair<string, bool>, bool>) (c => c.Key.OrdinalEquals(pair.Key)));
          descs.Add(keyValuePair.Value);
          flag1 = flag1 || srchs[srchs.Count - 1] != null;
        }
      }
      bool flag2 = flag1 || count == sorts.Count;
      foreach (KeyValuePair<string, bool> keyValuePair in array)
      {
        KeyValuePair<string, bool> pair = keyValuePair;
        if (!sorts.Any<string>((Func<string, bool>) (s => s.OrdinalEquals(pair.Key))))
        {
          if (flag2)
          {
            sorts.Add(pair.Key);
            descs.Add(pair.Value);
          }
          else
          {
            sorts.Insert(count, pair.Key);
            descs.Insert(count, pair.Value);
            srchs.Insert(count, (object) null);
            ++count;
          }
        }
      }
    }

    public void FillSearchesWithExternalSorts(
      string viewName,
      SyImportProcessor.SyStep.SyView view,
      List<object> srchs,
      List<string> sorts,
      List<bool> descs,
      Dictionary<string, KeyValuePair<string, bool>[]> externalSorts)
    {
      PXView view1 = this.Graph.Views[viewName];
      foreach (KeyValuePair<string, bool> keyValuePair1 in externalSorts[viewName])
      {
        string name = SyImportProcessor.SyStep.WithUpperFirstLetter(keyValuePair1.Key);
        if (!sorts.Any<string>((Func<string, bool>) (s => s.OrdinalEquals(name))))
        {
          sorts.Add(name);
          if (view.Searches.Keys.Any<string>((Func<string, bool>) (k => k.OrdinalEquals(name))))
          {
            KeyValuePair<string, string> keyValuePair2 = ((IEnumerable<KeyValuePair<string, string>>) view.Searches.ToArray<KeyValuePair<string, string>>()).Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key.OrdinalEquals(name)));
            srchs.Add(view.AdjustExternal(view1.Cache, view1.Cache.Current, false, keyValuePair2.Key, this.FormulaProcessor.Evaluate(keyValuePair2.Value, this.GetInternal)));
          }
          else
            srchs.Add((object) null);
          descs.Add(keyValuePair1.Value);
        }
      }
    }

    internal IEnumerable SelectRows(
      string viewName,
      PXFilterRow[] filters,
      int topCount,
      bool bypassInserted,
      int startRow,
      SyExportContext exportContext)
    {
      SyImportProcessor.SyStep.SyView view;
      this.GetViewOrAddNew(viewName, out view);
      List<object> source1 = new List<object>();
      if (this.Graph._InactiveViews.ContainsKey(viewName))
        return (IEnumerable) source1;
      List<object> objectList = new List<object>();
      List<string> stringList1 = new List<string>();
      List<bool> boolList = new List<bool>();
      Dictionary<string, KeyValuePair<string, bool>[]> sorts = exportContext.Sorts;
      if (sorts != null && sorts.Any<KeyValuePair<string, KeyValuePair<string, bool>[]>>() && sorts.ContainsKey(viewName))
        this.FillSearchesWithExternalSorts(viewName, view, objectList, stringList1, boolList, sorts);
      List<string> stringList2 = new List<string>((IEnumerable<string>) stringList1);
      List<bool> descs = new List<bool>((IEnumerable<bool>) boolList);
      this.FillSearches(viewName, view, objectList, stringList2, descs);
      object[] pars = (object[]) null;
      if (view.Parameters.Any<KeyValuePair<string, string>>())
        pars = this.EvaluateParameters(view, this.Graph.Views[viewName].Cache, this.Graph.GetParameterNames(viewName));
      bool flag1 = objectList.Count > 0 && objectList.Any<object>((Func<object, bool>) (s => s != null));
      int total = 0;
      int start = 0;
      int maximumRows = 1;
      if (!flag1)
      {
        start = startRow;
        if (topCount < 0)
        {
          start = -1;
          maximumRows = -topCount;
        }
        else
        {
          int num;
          if (view.StartRow != null && !view.StartRow.StartsWith("RowNumber_"))
          {
            start = this.GetFormulaValueInt(view.StartRow);
            num = 1;
          }
          else
            num = -1;
          maximumRows = view.TotalRow == null ? topCount : this.GetFormulaValueInt(view.TotalRow);
        }
      }
      else if (exportContext.SkipFiltersWhenSearchPresent)
        filters = (PXFilterRow[]) null;
      using (new SortAsImplicitScope(stringList2.Except<string>((IEnumerable<string>) stringList1).ToArray<string>()))
      {
        foreach (object obj in this.ExecuteSelect(viewName, pars, objectList.ToArray(), stringList2.ToArray(), descs.ToArray(), filters, ref start, maximumRows, ref total, exportContext.ShouldReadArchive))
        {
          if (!flag1 || !bypassInserted || this.Graph.GetStatus(viewName) != PXEntryStatus.Inserted)
            source1.Add(obj);
        }
      }
      if (exportContext.Sorts == null || NonGenericIEnumerableExtensions.Empty_((IEnumerable) exportContext.Sorts))
      {
        PXGenericInqGrph inqGraph = this.Graph as PXGenericInqGrph;
        if (inqGraph != null && source1.FirstOrDefault<object>() is GenericResult)
        {
          IEnumerable<GenericResult> source2 = source1.Cast<GenericResult>();
          foreach (GISort giSort in inqGraph.Description.Sorts.Reverse<GISort>())
          {
            bool? isActive = giSort.IsActive;
            bool flag2 = true;
            if (isActive.GetValueOrDefault() == flag2 & isActive.HasValue)
            {
              string[] strArray = giSort.DataFieldName.Split('.');
              string fieldName = $"{strArray[0]}_{strArray[1]}";
              source2 = giSort.SortOrder == "D" ? (IEnumerable<GenericResult>) source2.OrderByDescending<GenericResult, object>(new Func<GenericResult, object>(SortKeySelector)) : (IEnumerable<GenericResult>) source2.OrderBy<GenericResult, object>(new Func<GenericResult, object>(SortKeySelector));

              object SortKeySelector(GenericResult result)
              {
                return SyImportProcessor.ExportTableHelper.GetValueExt((PXGraph) inqGraph, (object) result, "Results", fieldName, true, false, out PXFieldState _, out PXDBLocalizableStringAttribute.Translations _);
              }
            }
          }
          return (IEnumerable) source2;
        }
      }
      return (IEnumerable) source1;
    }

    private IEnumerable ExecuteSelect(
      string viewName,
      object[] pars,
      object[] searches,
      string[] sorts,
      bool[] descs,
      PXFilterRow[] filters,
      ref int start,
      int maximumRows,
      ref int total,
      bool shouldReadArchive)
    {
      if (!shouldReadArchive)
        return this.Graph.ExecuteSelect(viewName, pars, searches, sorts, descs, filters, ref start, maximumRows, ref total);
      PXView view = this.Graph.Views[viewName];
      bool forceReadArchived = view.ForceReadArchived;
      try
      {
        view.ForceReadArchived = true;
        return this.Graph.ExecuteSelect(viewName, pars, searches, sorts, descs, filters, ref start, maximumRows, ref total);
      }
      finally
      {
        view.ForceReadArchived = forceReadArchived;
      }
    }

    internal IEnumerable<KeyValuePair<string, object>> SelectPaths(
      string viewName,
      string fieldName,
      Dictionary<string, KeyValuePair<string, bool>[]> externalSorts,
      PXFilterRow[] filters)
    {
      SyImportProcessor.SyStep.SyView view;
      this.GetViewOrAddNew(viewName, out view);
      SyImportProcessor.SyStep.SyField field;
      if (!view.GetFieldOrAddNew(fieldName, out field))
        field.Formula = $"=[{viewName}.{fieldName}]";
      PXCache cache = this.Graph.Views[viewName].Cache;
      string[] parameterNames = this.Graph.GetParameterNames(viewName);
      List<KeyValuePair<string, object>> ret = new List<KeyValuePair<string, object>>();
      object current = view.Current;
      this.DrillPath(viewName, fieldName, (string) null, view, field, parameterNames, cache, ret, externalSorts, filters);
      view.Current = cache.Current = current;
      view.IsPending = true;
      view.RecordInternal(cache, true, false, (SyImportRowResult) null);
      return (IEnumerable<KeyValuePair<string, object>>) ret;
    }

    private void DrillPath(
      string viewName,
      string fieldName,
      string prefix,
      SyImportProcessor.SyStep.SyView view,
      SyImportProcessor.SyStep.SyField field,
      string[] paramNames,
      PXCache cache,
      List<KeyValuePair<string, object>> ret,
      Dictionary<string, KeyValuePair<string, bool>[]> externalSorts,
      PXFilterRow[] filters)
    {
      field.IsPending = true;
      object[] objArray = new object[paramNames.Length];
      this.EvaluateFields(view, cache, paramNames, objArray, true);
      List<object> objectList = new List<object>();
      List<string> sorts = new List<string>();
      List<bool> descs = new List<bool>();
      if (externalSorts != null && externalSorts.Any<KeyValuePair<string, KeyValuePair<string, bool>[]>>() && externalSorts.ContainsKey(viewName))
        this.FillSearchesWithExternalSorts(viewName, view, objectList, sorts, descs, externalSorts);
      this.FillSearches(viewName, view, objectList, sorts, descs);
      int startRow = 0;
      int totalRows1 = 0;
      List<object> source = new List<object>();
      if (objectList.Any<object>((Func<object, bool>) (s => s != null)) || filters != null && filters.Length != 0)
        source.AddRange(this.Graph.ExecuteSelect(viewName, objArray, objectList.ToArray(), sorts.ToArray(), descs.ToArray(), filters, ref startRow, objectList.Any<object>((Func<object, bool>) (s => s != null)) ? 1 : 0, ref totalRows1).Cast<object>());
      startRow = 0;
      int totalRows2 = 0;
      List<KeyValuePair<string, object>> keyValuePairList = new List<KeyValuePair<string, object>>();
      foreach (object obj in this.Graph.ExecuteSelect(viewName, objArray, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows2))
      {
        object row = obj;
        view.Current = PXResult.UnwrapFirst(row);
        if (view.Current == null)
          break;
        if (keyValuePairList.Count > 0)
        {
          KeyValuePair<string, object> keyValuePair = keyValuePairList[keyValuePairList.Count - 1];
          object a;
          if ((a = keyValuePair.Value) == row || cache.ObjectsEqual(a, row) || a is PXResult pxResult1 && row is PXResult pxResult2 && cache.ObjectsEqual(pxResult1[0], pxResult2[0]))
            break;
        }
        view.IsPending = true;
        view.RecordInternal(cache, true, false, (SyImportRowResult) null);
        if (!string.IsNullOrEmpty(field.CommittedInternalValue as string))
        {
          string str = prefix == null ? (string) field.CommittedInternalValue : $"{prefix}/{(string) field.CommittedInternalValue}";
          keyValuePairList.Add(new KeyValuePair<string, object>(str, row));
          if (!objectList.Any<object>((Func<object, bool>) (s => s != null)) && (filters == null || filters.Length == 0) || source.Any<object>((Func<object, bool>) (r => cache.ObjectsEqual(PXResult.UnwrapFirst(row), PXResult.UnwrapFirst(r)))))
            ret.Add(new KeyValuePair<string, object>(str, row));
          this.DrillPath(viewName, fieldName, str, view, field, paramNames, cache, ret, externalSorts, filters);
        }
      }
    }

    internal IEnumerable<object> EnumFieldValues(
      PXGraph graph,
      string viewName,
      string fieldName,
      PXFilterRow[] filters,
      int topCount,
      string primaryViewName,
      bool ignoreStart = false,
      bool shouldReadArchived = false)
    {
      SyImportProcessor.SyStep.SyView view1 = this.Views[viewName];
      PXView view2 = graph.Views[viewName];
      PXFieldState stateExt = (PXFieldState) view2.Cache.GetStateExt(view2.Cache.Current, fieldName);
      switch (stateExt)
      {
        case null:
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The EveryValue option is not allowed for non-selector fields. ViewName:{0}, fieldName:{1}", (object) viewName, (object) fieldName));
        case PXStringState pxStringState:
          if (pxStringState.AllowedValues != null && ((IEnumerable<string>) pxStringState.AllowedValues).Any<string>())
            return (IEnumerable<object>) pxStringState.AllowedValues;
          break;
        case PXIntState pxIntState:
          if (pxIntState.AllowedValues != null && ((IEnumerable<int>) pxIntState.AllowedValues).Any<int>())
            return pxIntState.AllowedValues.Cast<object>();
          break;
      }
      string str = stateExt.ViewName;
      bool flag1 = false;
      if (string.IsNullOrEmpty(str))
      {
        flag1 = true;
        str = viewName;
      }
      string fieldName1 = flag1 ? fieldName : (graph.Views[str].BqlSelect is IBqlSearch bqlSelect ? bqlSelect.GetField().Name : (string) null);
      if (string.IsNullOrEmpty(fieldName1))
        return (IEnumerable<object>) new object[0];
      bool flag2 = view2.Cache.GetAttributesReadonly(fieldName, true).OfType<PXSelectorAttribute>().Any<PXSelectorAttribute>((Func<PXSelectorAttribute, bool>) (c => c.IsPrimaryViewCompatible));
      int total = 0;
      int start = 0;
      string[] sorts = (string[]) null;
      bool[] descs = (bool[]) null;
      System.Type itemType1;
      System.Type itemType2;
      if ((flag2 || (itemType1 = graph.Views[str].GetItemType()) == (itemType2 = graph.Views[primaryViewName].GetItemType()) || itemType2.IsAssignableFrom(itemType1) ? 1 : (itemType1.IsAssignableFrom(itemType2) ? 1 : 0)) == 0)
      {
        filters = SyImportProcessor.SyStep.TryGetEnumerationFilters(fieldName, filters, stateExt);
        if (filters == null)
          topCount = -1;
      }
      else
      {
        if (filters != null && filters.Length != 0)
          filters = ((IEnumerable<PXFilterRow>) filters).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (_ => _.Clone() as PXFilterRow)).ToArray<PXFilterRow>();
        if (topCount < 0)
        {
          start = -1;
          topCount = -topCount;
        }
        KeyValuePair<string, bool>[] keyValuePairArray = graph.IsMobile ? graph.Views[str].GetSortColumns() : view2.GetSortColumns();
        if (keyValuePairArray.Length != 0)
        {
          sorts = new string[keyValuePairArray.Length];
          descs = new bool[keyValuePairArray.Length];
          for (int index = 0; index < keyValuePairArray.Length; ++index)
          {
            sorts[index] = keyValuePairArray[index].Key;
            descs[index] = keyValuePairArray[index].Value;
          }
        }
        if (flag2 && !graph.IsMobile && filters != null && filters.Length != 0)
        {
          HashSet<string> selectorfields = new HashSet<string>(((IEnumerable<PXFieldState>) PXFieldState.GetFields(graph, graph.Views[str].BqlSelect.GetTables(), false)).Select<PXFieldState, string>((Func<PXFieldState, string>) (_ => _.Name)));
          if (((IEnumerable<PXFilterRow>) filters).Any<PXFilterRow>((Func<PXFilterRow, bool>) (_ => !string.IsNullOrEmpty(_.DataField) && !selectorfields.Contains(_.DataField))))
          {
            HashSet<string> viewfields = new HashSet<string>(((IEnumerable<PXFieldState>) PXFieldState.GetFields(graph, graph.Views[primaryViewName].BqlSelect.GetTables(), false)).Select<PXFieldState, string>((Func<PXFieldState, string>) (_ => _.Name)));
            if (!((IEnumerable<PXFilterRow>) filters).Any<PXFilterRow>((Func<PXFilterRow, bool>) (_ => !string.IsNullOrEmpty(_.DataField) && !viewfields.Contains(_.DataField))))
              str = primaryViewName;
          }
        }
      }
      if (string.Equals(fieldName, view1.RowFilterField))
      {
        if (view1.StartRow != null && !view1.StartRow.StartsWith("RowNumber_"))
        {
          start = this.GetFormulaValueInt(view1.StartRow);
          topCount = 1;
        }
        if (view1.TotalRow != null)
          topCount = this.GetFormulaValueInt(view1.TotalRow);
      }
      if (ignoreStart)
      {
        start = 0;
        topCount = 0;
      }
      List<object> objectList = new List<object>();
      foreach (object obj in this.ExecuteSelect(str, (object[]) null, (object[]) null, sorts, descs, filters, ref start, topCount, ref total, shouldReadArchived))
      {
        object returnValue = graph.GetValue(str, obj, fieldName1);
        view2.Cache.RaiseFieldSelecting(fieldName, obj, ref returnValue, false);
        objectList.Add(PXFieldState.UnwrapValue(returnValue));
      }
      return (IEnumerable<object>) objectList;
    }

    private static PXFilterRow[] TryGetEnumerationFilters(
      string fieldName,
      PXFilterRow[] filters,
      PXFieldState state)
    {
      if (string.IsNullOrWhiteSpace(state?.ValueField) || filters == null)
        return (PXFilterRow[]) null;
      if (filters.Length != 1 && ((IEnumerable<PXFilterRow>) filters).Any<PXFilterRow>((Func<PXFilterRow, bool>) (f => f.OrOperator)))
        return (PXFilterRow[]) null;
      PXFilterRow pxFilterRow = MaybeObjects.If<PXFilterRow[]>(((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (f => f.DataField.OrdinalEquals(fieldName))).Take<PXFilterRow>(2).ToArray<PXFilterRow>(), (Func<PXFilterRow[], bool>) (ff => ff.Length == 1))?[0];
      if (pxFilterRow == null || pxFilterRow.Condition != PXCondition.EQ || pxFilterRow.Value == null)
        return (PXFilterRow[]) null;
      return new PXFilterRow[1]
      {
        new PXFilterRow()
        {
          DataField = state.ValueField,
          Condition = PXCondition.EQ,
          Value = pxFilterRow.Value
        }
      };
    }

    private static PXFilterRow GetExternalKeyFilter(string externalKey)
    {
      return new PXFilterRow("NoteID", PXCondition.ER, (object) externalKey);
    }

    private static bool HasRealValues(OrderedDictionary vals)
    {
      return vals.Keys.OfType<string>().Any<string>((Func<string, bool>) (name => !SyImportProcessor.SyStep.IsFileName(name) && !name.OrdinalEquals("NoteText") && !Guid.TryParse(name, out Guid _) && vals[(object) name] != PXCache.NotSetValue));
    }

    public bool IsExport { get; private set; }

    public void CommitChanges(
      ref object itemToBypass,
      PXFilterRow[] targetConditions,
      PXFilterRow[] filtersForAction)
    {
      this.Graph.IsImport = true;
      if (this.PreviousStep != null)
      {
        List<SyImportProcessor.SyStep> previousSteps = this.GetPreviousSteps();
        for (int index = previousSteps.Count - 1; index >= 0; --index)
        {
          previousSteps[index].PreviousStep = (SyImportProcessor.SyStep) null;
          previousSteps[index].CommitChangesInt(itemToBypass, targetConditions, filtersForAction, (SyImportRowResult) null);
        }
        this.PreviousStep = (SyImportProcessor.SyStep) null;
      }
      this.CommitChangesInt(itemToBypass, targetConditions, filtersForAction, (SyImportRowResult) null);
    }

    public void CommitWithoutExceptions(
      ref bool hasExceptions,
      ref object itemToBypass,
      PXFilterRow[] targetConditions,
      SyImportContext context,
      int depth = 0)
    {
      this.Graph.IsImport = true;
      if (this.PreviousStep != null)
      {
        int rowNumberLocal = this.RowNumberLocal;
        List<SyImportProcessor.SyStep> previousSteps = this.GetPreviousSteps();
        for (int index = previousSteps.Count - 1; index >= 0; --index)
        {
          SyImportProcessor.SyStep syStep = previousSteps[index];
          syStep.PreviousStep = (SyImportProcessor.SyStep) null;
          syStep.CommitWithoutExceptions(ref hasExceptions, ref itemToBypass, targetConditions, context, syStep.RowNumberLocal - rowNumberLocal);
        }
        this.PreviousStep = (SyImportProcessor.SyStep) null;
      }
      SyImportRowResult importResult = context.ImportResult[context.RowIndex + depth];
      try
      {
        this.CommitChangesInt(itemToBypass, targetConditions, (PXFilterRow[]) null, importResult);
      }
      catch (SyImportProcessor.InvalidTargetException ex)
      {
        throw;
      }
      catch (PXRedirectRequiredException ex)
      {
        ex.Graph.Unload();
        throw;
      }
      catch (PXPopupRedirectException ex)
      {
        ex.Graph.Unload();
        throw;
      }
      catch (PXException ex)
      {
        if (ex is SyImportProcessor.DetailBypassedException)
        {
          if (importResult.Error == null)
            importResult.Error = (Exception) ex;
        }
        else
        {
          importResult.Error = (Exception) ex;
          hasExceptions = true;
        }
        itemToBypass = (object) importResult.ExternalKeys;
        this.HeldPrimaryItem();
      }
      catch (ExpressionException ex)
      {
        hasExceptions = true;
        importResult.Error = (Exception) ex;
        importResult.AddFieldExceptions(this);
        itemToBypass = (object) importResult.ExternalKeys;
        this.HeldPrimaryItem();
      }
    }

    public Dictionary<string, object> GetKeyExternalValues(string[] keyExternalNames)
    {
      if (keyExternalNames.Length == 0)
        return (Dictionary<string, object>) null;
      Dictionary<string, object> keyExternalValues = new Dictionary<string, object>();
      foreach (string keyExternalName in keyExternalNames)
      {
        object obj = this.FormulaProcessor.Evaluate(keyExternalName, this.GetExternal);
        keyExternalValues.Add(keyExternalName, obj);
      }
      return keyExternalValues;
    }

    internal static bool IsAnyBypass(object errorItem)
    {
      return errorItem == SyImportProcessor.SyStep.anyBypass;
    }

    public bool CheckShouldBeBypassed(object errorItem)
    {
      if (errorItem == null)
        return false;
      if (SyImportProcessor.SyStep.IsAnyBypass(errorItem))
        return true;
      if (!(errorItem is Dictionary<string, object> dict1))
        return errorItem == this.PrimaryItem;
      Dictionary<string, object> externalKeys = this.RowImportResult?.ExternalKeys;
      return SyImportProcessor.RowKeysEqual(dict1, externalKeys);
    }

    internal void HeldPrimaryItem()
    {
      if (this.PrimaryItem == null)
        return;
      PXCache cach = this.Graph.Caches[this.PrimaryItem.GetType()];
      if (cach.GetStatus(this.PrimaryItem) != PXEntryStatus.Notchanged)
        return;
      cach.SetStatus(this.PrimaryItem, PXEntryStatus.Held);
    }

    internal void FillImportResults(
      SyImportContext context,
      SyImportProcessor.SyStep.FillImportResult handler)
    {
      SyImportRowResult[] importResult = context.ImportResult;
      int startRow = context.StartRow;
      int rowIndex = this.RowNumber + startRow;
      for (int index = this.MinRowNumber + startRow; rowIndex >= index && rowIndex >= startRow; --rowIndex)
      {
        SyImportRowResult result = importResult[rowIndex];
        if (result.IsFilled)
          break;
        handler(rowIndex, result);
      }
    }

    private class GlobalStep
    {
      public int MinRowNumber;
      public string CommittedExternalKey;
      public string PendingExternalKey;
      public string CancelAction;
      public bool IsAnswersInitialized;
      private readonly Dictionary<PXCache, Dictionary<string, object>> InsertedExternalKeys = new Dictionary<PXCache, Dictionary<string, object>>();

      public bool TryGetInsertedExternalRow(
        PXCache cache,
        string externalKey,
        out object rowInserted)
      {
        rowInserted = (object) null;
        Dictionary<string, object> dictionary;
        return this.InsertedExternalKeys.TryGetValue(cache, out dictionary) && dictionary.TryGetValue(externalKey, out rowInserted);
      }

      public void AddInsertedExternalRow(PXCache cache, string externalKey, object rowInserted)
      {
        Dictionary<string, object> dictionary;
        if (!this.InsertedExternalKeys.TryGetValue(cache, out dictionary))
          this.InsertedExternalKeys[cache] = dictionary = new Dictionary<string, object>();
        dictionary[externalKey] = rowInserted;
      }
    }

    public sealed class SyField
    {
      public string Formula;
      public object ExternalValue;
      public bool IsCalced;
      public bool IsCommitted;
      public bool IsPending;
      public bool FromPreviousSearch;
      public bool Calculating;
      public bool LoopDetected;
      public object InternalValue;
      public string InputMask;
      public int Length;
      public bool Enabled = true;
      public bool IgnoreError;
      internal bool CanCleanError = true;
      public long Revision;
      public string ForeignField;
      private readonly SyImportProcessor.SyStep.SyField.SyFieldGlobal GlobalState;
      public bool CheckUpdatability;
      public bool Visible = true;

      internal bool IsExternalValueNullResult { get; private set; }

      public object CommittedExternalValue
      {
        get => this.GlobalState.ExternalValue;
        set => this.GlobalState.ExternalValue = value;
      }

      public object CommittedInternalValue
      {
        get => this.GlobalState.InternalValue;
        set => this.GlobalState.InternalValue = value;
      }

      public object Current
      {
        get => this.GlobalState.Current;
        set => this.GlobalState.Current = value;
      }

      public ExpressionException Exception
      {
        get => this.GlobalState.Exception;
        set => this.GlobalState.Exception = value;
      }

      public PXErrorLevel ErrorLevel
      {
        get => this.GlobalState.ErrorLevel;
        set => this.GlobalState.ErrorLevel = value;
      }

      public string Error
      {
        get => this.GlobalState.Error;
        set => this.GlobalState.Error = value;
      }

      public string DisplayName
      {
        get => this.GlobalState.DisplayName;
        set => this.GlobalState.DisplayName = value;
      }

      public long CommittedRevision
      {
        get => this.GlobalState.Revision;
        set => this.GlobalState.Revision = value;
      }

      public SyField() => this.GlobalState = new SyImportProcessor.SyStep.SyField.SyFieldGlobal();

      public SyField(SyImportProcessor.SyStep.SyField previousLocal)
      {
        this.Formula = previousLocal.Formula;
        this.GlobalState = previousLocal.GlobalState;
        this.InternalValue = this.CommittedInternalValue;
      }

      internal void CalculateExternal(SyImportProcessor.SyStep step)
      {
        try
        {
          this.Calculating = true;
          this.IsExternalValueNullResult = false;
          bool isNullOperatorResult;
          this.ExternalValue = step.FormulaProcessor.Evaluate(this.Formula, step.GetExternal, out isNullOperatorResult);
          this.IsExternalValueNullResult = isNullOperatorResult;
        }
        catch (ExpressionException ex)
        {
          this.Exception = ex;
          throw;
        }
        finally
        {
          this.IsCalced = !this.LoopDetected;
          this.Calculating = false;
        }
      }

      internal object GetValue(SyFormulaProcessor processor, SyFormulaFinalDelegate getter)
      {
        if (!this.Calculating)
        {
          try
          {
            this.Calculating = true;
            return processor.Evaluate(this.Formula, getter);
          }
          finally
          {
            this.Calculating = false;
          }
        }
        else
        {
          this.LoopDetected = true;
          return this.InternalValue;
        }
      }

      internal string GetExternalName(string[] sourceFields)
      {
        if (this.Formula == null || !this.Formula.StartsWith("="))
          return this.Formula;
        if (SyExpressionParser.Parse(this.Formula.Substring(1)) is FunctionNode functionNode && functionNode.Arguments != null)
        {
          foreach (ExpressionNode expressionNode in functionNode.Arguments)
          {
            if (expressionNode is NameNode nameNode && ((IEnumerable<string>) sourceFields).Contains<string>(nameNode.Name, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
              return nameNode.Name;
          }
        }
        return (string) null;
      }

      private sealed class SyFieldGlobal
      {
        public object ExternalValue;
        public object InternalValue;
        public object Current;
        public ExpressionException Exception;
        public PXErrorLevel ErrorLevel;
        public string Error;
        public string DisplayName;
        public long Revision;
      }
    }

    internal sealed class SyView
    {
      private readonly SyImportProcessor.SyStep Step;
      public readonly Dictionary<string, SyImportProcessor.SyStep.SyField> Fields;
      public readonly Dictionary<string, string> Parameters;
      public readonly Dictionary<string, string> WorkflowParameters;
      public Dictionary<string, string> Searches;
      public string StartRow;
      public string TotalRow;
      public bool NewRow;
      public bool DeleteRow;
      public string AnswerFormula;
      public string BranchFormula;
      public string Path;
      public string PathField;
      private readonly SyImportProcessor.SyStep.SyView.SyViewGlobal GlobalState;
      public bool IsPending;
      public long MaxRevision;
      private bool revisionUpdated;
      public string RowFilterField;
      private string ViewName;

      internal object DummyCurrent { get; set; }

      public object Current
      {
        get => this.GlobalState.Current;
        set => this.GlobalState.Current = value;
      }

      public object Previous
      {
        get => this.GlobalState.Previous;
        set => this.GlobalState.Previous = value;
      }

      public long KeysFromCurrent
      {
        get => this.GlobalState.KeysFromCurrent;
        set => this.GlobalState.KeysFromCurrent = value;
      }

      public bool InsertCalled
      {
        get => this.GlobalState.InsertCalled;
        set => this.GlobalState.InsertCalled = value;
      }

      public WebDialogResult? Answer
      {
        get => this.GlobalState.Answer;
        private set => this.GlobalState.Answer = value;
      }

      public long MinRevision
      {
        get => this.GlobalState.MinRevision;
        set => this.GlobalState.MinRevision = value;
      }

      public SyView(SyImportProcessor.SyStep step, string viewName)
      {
        this.Step = step;
        this.Fields = new Dictionary<string, SyImportProcessor.SyStep.SyField>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.Parameters = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.WorkflowParameters = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.Searches = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.GlobalState = new SyImportProcessor.SyStep.SyView.SyViewGlobal();
        this.ViewName = viewName;
      }

      public SyView(
        SyImportProcessor.SyStep step,
        string viewName,
        SyImportProcessor.SyStep.SyView previousView)
      {
        this.Step = step;
        this.Parameters = new Dictionary<string, string>((IDictionary<string, string>) previousView.Parameters, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.WorkflowParameters = new Dictionary<string, string>((IDictionary<string, string>) previousView.WorkflowParameters, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.Searches = new Dictionary<string, string>((IDictionary<string, string>) previousView.Searches, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this.Fields = new Dictionary<string, SyImportProcessor.SyStep.SyField>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field in previousView.Fields)
          this.Fields[field.Key] = new SyImportProcessor.SyStep.SyField(field.Value);
        this.StartRow = previousView.StartRow;
        this.TotalRow = previousView.TotalRow;
        this.RowFilterField = previousView.RowFilterField;
        this.Path = previousView.Path;
        this.PathField = previousView.PathField;
        this.GlobalState = previousView.GlobalState;
        this.MaxRevision = previousView.MaxRevision;
        this.ViewName = viewName;
      }

      public void CalculateExternal()
      {
        foreach (SyImportProcessor.SyStep.SyField syField in this.Fields.Values)
        {
          if (!syField.IsCalced)
            syField.CalculateExternal(this.Step);
        }
        if (string.IsNullOrEmpty(this.AnswerFormula))
          return;
        string str = this.Step.FormulaProcessor.Evaluate(this.AnswerFormula, this.Step.GetExternal) as string;
        if (string.IsNullOrEmpty(str))
          return;
        this.Answer = new WebDialogResult?((WebDialogResult) Enum.Parse(typeof (WebDialogResult), str));
      }

      public bool CalculatePending()
      {
        bool flag1 = false;
        bool flag2 = this.Current == null;
        foreach (SyImportProcessor.SyStep.SyField syField in this.Fields.Values)
        {
          syField.IsCommitted = object.Equals(syField.CommittedExternalValue, syField.ExternalValue) && syField.Current != null && syField.Current == this.Current;
          flag1 = flag1 || syField.IsPending && !syField.IsCommitted;
          flag2 = flag2 || syField.ExternalValue != null || syField.IsExternalValueNullResult;
        }
        return this.DeleteRow || flag1 & flag2;
      }

      public void UpdateRevision()
      {
        if (this.revisionUpdated)
          return;
        long num = long.MaxValue;
        long lastrev = -1;
        foreach (SyImportProcessor.SyStep.SyField syField in this.Fields.Values)
        {
          if (syField.Revision > 0L)
          {
            if (syField.Revision < num)
            {
              num = syField.Revision;
              lastrev = syField.CommittedRevision;
            }
            syField.CommittedRevision = syField.Revision;
          }
        }
        if (lastrev >= this.MinRevision || this.NewRow || this.IsUpdateAfterCommit(lastrev))
          this.MinRevision = num;
        this.revisionUpdated = true;
      }

      private bool IsUpdateAfterCommit(long lastrev)
      {
        return this.IsPending && lastrev == 0L && this.MinRevision > 0L && this.Fields.Values.Any<SyImportProcessor.SyStep.SyField>((Func<SyImportProcessor.SyStep.SyField, bool>) (f => !f.IsPending && f.FromPreviousSearch));
      }

      public object AdjustExternal(
        PXCache cache,
        object current,
        bool checkLength,
        string fieldName,
        object val)
      {
        return this.AdjustExternal(cache, current, checkLength, fieldName, val, out bool? _);
      }

      public object AdjustExternal(
        PXCache cache,
        object current,
        bool checkLength,
        string fieldName,
        object val,
        out bool? isListBox)
      {
        isListBox = new bool?();
        if (val != null)
        {
          SyImportProcessor.SyStep.SyField field;
          if (this.Fields.TryGetValue(fieldName, out field) && field.ForeignField != null)
          {
            isListBox = new bool?(false);
            if (cache.GetStateExt(current, fieldName) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.ViewName) && cache.Graph.Views[stateExt.ViewName].BqlSelect is IBqlSearch bqlSelect)
            {
              int startRow = 0;
              object[] currents = new object[1]{ current };
              object[] searches = new object[1]{ val };
              string[] sortcolumns = new string[1]
              {
                field.ForeignField
              };
              bool[] descendings = new bool[1];
              int totalRows = 0;
              object data = (object) null;
              bool flag = false;
              IEnumerator enumerator = cache.Graph.ExecuteSelect(stateExt.ViewName, currents, (object[]) null, searches, sortcolumns, descendings, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).GetEnumerator();
              try
              {
                if (enumerator.MoveNext())
                {
                  data = enumerator.Current;
                  flag = true;
                }
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
              if (flag)
              {
                val = cache.Graph.GetValue(stateExt.ViewName, data, bqlSelect.GetField().Name);
                cache.RaiseFieldSelecting(fieldName, current, ref val, false);
                val = PXFieldState.UnwrapValue(val);
              }
            }
          }
          else
            val = SyImportProcessor.SyStep.SyView.AdjustExternal(cache, current, checkLength, fieldName, val, ref isListBox, field, this.ViewName);
        }
        return val;
      }

      internal static object AdjustExternal(
        PXCache cache,
        object current,
        bool checkLength,
        string fieldName,
        object val,
        ref bool? isListBox,
        SyImportProcessor.SyStep.SyField field,
        string viewName)
      {
        string stringVal = val as string;
        if (stringVal != null)
        {
          PXFieldState stateExt = cache.GetStateExt(current, fieldName) as PXFieldState;
          string result1;
          switch (stateExt)
          {
            case PXStringState state1:
              bool flag = false;
              if (state1.HasFixedValuesList())
              {
                isListBox = new bool?(true);
                string result2;
                if (flag = state1.TryGetListValue(stringVal, out result2))
                  val = (object) (stringVal = result2);
                else if (state1.MultiSelect)
                {
                  foreach (string multiSelectValue in PXStringListAttribute.SplitMultiSelectValues(stringVal))
                  {
                    if (!state1.TryGetListLabel(multiSelectValue, out result1))
                    {
                      SyImportProcessor.SyStep.SyView.RaiseUnallowedListValueException(cache, current, val, field, stateExt, state1.AllowedValues);
                      return (object) null;
                    }
                  }
                }
                else if (state1.ExclusiveValues && !(flag = state1.TryGetListLabel(stringVal, out result1)))
                {
                  SyImportProcessor.SyStep.SyView.RaiseUnallowedListValueException(cache, current, val, field, stateExt, state1.AllowedLabels);
                  return (object) null;
                }
              }
              if (!flag && !string.IsNullOrEmpty(state1.InputMask) && !stringVal.Contains<char>('?'))
              {
                string a = SyImportProcessor.SyStep.DemaskString(stringVal, state1, true);
                if (cache.Graph.IsContractBasedAPI && !Mask.IsMasked(stringVal, state1.InputMask, true) && !a.OrdinalIgnoreCaseEquals(stringVal))
                  throw new EntitySetPropertyException(viewName, fieldName, "The provided value '{0}' does not match the required input mask '{1}'.", new object[2]
                  {
                    (object) stringVal,
                    (object) state1.InputMask
                  });
                val = (object) (stringVal = a);
              }
              if (!checkLength || field == null || field.Length <= 0 || stringVal.Length <= field.Length)
                return val;
              object newValue = val;
              try
              {
                cache.RaiseFieldUpdating(fieldName, current, ref newValue);
              }
              catch
              {
              }
              if (field.IgnoreError || newValue is string str && str.Length <= field.Length && !stringVal.StartsWithCollation(str))
                return val;
              throw new PXSetPropertyException("The string value provided exceeds the field '{0}' length.", new object[1]
              {
                (object) (stateExt.DisplayName ?? fieldName)
              });
            case PXIntState state2:
              if (state2.HasFixedValuesList())
              {
                isListBox = new bool?(true);
                object result3;
                if (state2.TryGetListValue(stringVal, out result3))
                {
                  val = result3;
                }
                else
                {
                  int result4;
                  if (state2.ExclusiveValues && (!int.TryParse(stringVal, out result4) || !state2.TryGetListLabel(result4, out result1)))
                  {
                    SyImportProcessor.SyStep.SyView.RaiseUnallowedListValueException(cache, current, val, field, stateExt, state2.AllowedLabels);
                    return (object) null;
                  }
                }
              }
              return val;
            case PXDecimalState _:
              if (stringVal == string.Empty)
                return (object) null;
              val = (object) Convert.ToDecimal(val);
              return val;
            case PXDateState _:
              if (stringVal == string.Empty)
                return (object) null;
              if (((IEnumerable<string>) RelativeDatesManager.AllVariables).Any<string>((Func<string, bool>) (c => stringVal.Contains(c))))
                return val;
              val = (object) Convert.ToDateTime(val);
              break;
          }
        }
        return val;
      }

      private static void RaiseUnallowedListValueException(
        PXCache cache,
        object row,
        object value,
        SyImportProcessor.SyStep.SyField field,
        PXFieldState state,
        string[] allowedLabels)
      {
        cache.RaiseExceptionHandling(state.Name, row, value, (Exception) new PXSetPropertyException("The '{0}' list value is not allowed for the {1} field. The allowed values are: {2}.", new object[3]
        {
          value,
          (object) (state.DisplayName ?? state.Name),
          (object) string.Join(", ", allowedLabels)
        }));
        if (field == null)
          return;
        field.CanCleanError = false;
        if (cache.Graph.IsContractBasedAPI)
          return;
        field.IgnoreError = false;
      }

      public OrderedDictionary PrepareKeys(PXCache cache, out bool hasKeys)
      {
        hasKeys = false;
        OrderedDictionary orderedDictionary = (OrderedDictionary) new OrderedDictionaryD();
        foreach (string key in (IEnumerable<string>) cache.Keys)
        {
          if (this.Current != null)
          {
            object obj = PXFieldState.UnwrapValue(cache.GetValueExt(this.Current, key));
            if (obj != null)
              hasKeys = true;
            orderedDictionary[(object) key] = obj;
          }
          else
          {
            SyImportProcessor.SyStep.SyField syField;
            if (this.Fields.TryGetValue(key, out syField) && syField.CommittedRevision >= this.MinRevision)
            {
              object obj = !syField.IsCommitted ? this.AdjustExternal(cache, this.DummyCurrent, false, key, syField.ExternalValue) : syField.CommittedInternalValue;
              if (obj != null)
                hasKeys = true;
              orderedDictionary[(object) key] = obj;
            }
          }
        }
        return orderedDictionary;
      }

      public OrderedDictionary PrepareValues(PXCache cache, out bool hasValues)
      {
        hasValues = false;
        OrderedDictionary orderedDictionary = (OrderedDictionary) new OrderedDictionaryD();
        foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field in this.Fields)
        {
          if (field.Value.IsPending && (field.Value.Enabled || field.Value.Visible && field.Value.CheckUpdatability) && !SyImportProcessor.SyStep.IsNoteIdKey(field.Key))
          {
            object obj1;
            if (field.Value.IsCommitted)
            {
              obj1 = field.Value.CommittedInternalValue;
            }
            else
            {
              object obj2 = this.Current ?? this.DummyCurrent;
              obj1 = this.AdjustExternal(cache, obj2, true, field.Key, field.Value.ExternalValue);
              if (field.Value.IgnoreError && field.Value.CanCleanError && !PopupNoteManager.PreserveErrors(cache, field.Key) && obj2 != null)
                PXUIFieldAttribute.SetError(cache, obj2, field.Key, (string) null);
            }
            if (obj1 != null)
              hasValues = true;
            orderedDictionary[(object) field.Key] = obj1;
          }
        }
        orderedDictionary.Add((object) PXImportAttribute.ImportFlag, PXCache.NotSetValue);
        return orderedDictionary;
      }

      public void EnsureFields(string[] names, object[] values, object current)
      {
        for (int index = 0; index < names.Length && index < values.Length; ++index)
        {
          string name = names[index];
          object obj = values[index];
          if (obj != null && !string.IsNullOrWhiteSpace(name) && !this.Fields.ContainsKey(name))
            this.Fields[name] = new SyImportProcessor.SyStep.SyField()
            {
              ExternalValue = obj,
              Current = current
            };
        }
      }

      private void updateFieldFromState(
        SyImportProcessor.SyStep.SyField field,
        PXFieldState state,
        bool isPrimaryView)
      {
        field.Enabled = state.Enabled || state.PrimaryKey & isPrimaryView || state.Name.OrdinalEquals("NoteText");
        field.Visible = state.Visible;
        if (!(state is PXStringState))
          return;
        field.InputMask = ((PXStringState) state).InputMask;
        if (state is PXSegmentedState)
        {
          field.Length = 0;
          foreach (PXSegment segment in ((PXSegmentedState) state).Segments)
            field.Length += (int) segment.Length;
        }
        else
        {
          if (!string.IsNullOrEmpty(state.ViewName))
            return;
          field.Length = state.Length;
        }
      }

      public void RecordState(PXCache cache, object stateHolder)
      {
        bool isPrimaryView = object.Equals((object) cache, (object) cache.Graph.Views[cache.Graph.PrimaryView].Cache);
        foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field in this.Fields)
        {
          if (cache.GetStateExt(stateHolder, field.Key) is PXFieldState stateExt)
            this.updateFieldFromState(field.Value, stateExt, isPrimaryView);
        }
      }

      public void RecordInternal(
        PXCache cache,
        bool justUpdated,
        bool clearPending,
        SyImportRowResult importResult)
      {
        bool isPrimaryView = object.Equals((object) cache, (object) cache.Graph.Views[cache.Graph.PrimaryView].Cache);
        foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field1 in this.Fields)
        {
          SyImportProcessor.SyStep.SyField field2 = field1.Value;
          string key = field1.Key;
          if (justUpdated && field2.IsPending || field2.Current == this.Current)
          {
            object stateExt = cache.GetStateExt(this.Current, key);
            if (stateExt is PXFieldState state)
            {
              field2.CommittedInternalValue = state.Value;
              field2.DisplayName = state.DisplayName;
              field2.ErrorLevel = state.ErrorLevel;
              field2.Error = state.Error;
              int num = !string.IsNullOrEmpty(state.Error) ? 1 : 0;
              bool isError = state.ErrorLevel == PXErrorLevel.Error || state.ErrorLevel == PXErrorLevel.RowError;
              if (num != 0 && importResult != null)
              {
                importResult.AddFieldError(field2, isError);
                importResult.AddUnlinkedError(field2, isError);
              }
              if ((num & (isError ? 1 : 0)) != 0)
              {
                field2.Current = (object) null;
                if (justUpdated && !field2.IgnoreError)
                {
                  if (state.Value == null)
                    throw new SyImportProcessor.PXFieldErrorException("An error occurred during processing of the field {0}: {1}.", new object[2]
                    {
                      (object) state.DisplayName,
                      (object) state.Error
                    });
                  throw new SyImportProcessor.PXFieldErrorException("An error occurred during processing of the field {0} value {1} {2}.", new object[3]
                  {
                    (object) state.DisplayName,
                    state.Value,
                    (object) state.Error
                  });
                }
              }
              else
              {
                field2.Current = this.Current;
                field2.InternalValue = state.Value;
              }
              this.updateFieldFromState(field2, state, isPrimaryView);
            }
            else
            {
              field2.CommittedInternalValue = stateExt;
              field2.InternalValue = stateExt;
              field2.Error = (string) null;
              field2.Current = this.Current;
            }
            if (justUpdated && field2.IsPending && key.IndexOf("__") == -1)
              field2.CommittedExternalValue = field2.ExternalValue;
          }
          else
          {
            object stateExt = cache.GetStateExt(this.Current, key);
            if (stateExt is PXFieldState state)
            {
              if (string.IsNullOrEmpty(state.Error))
                field2.InternalValue = state.Value;
              this.updateFieldFromState(field2, state, isPrimaryView);
            }
            else
              field2.InternalValue = stateExt;
          }
          if (clearPending)
            field2.IsPending = false;
        }
      }

      public bool IsPartial(PXCache cache, OrderedDictionary vals)
      {
        if (cache.Current == null)
          return false;
        foreach (string key in (IEnumerable<string>) cache.Keys)
        {
          if (!this.Fields.ContainsKey(key))
            return true;
        }
        bool flag = false;
        foreach (string key in (IEnumerable) vals.Keys)
        {
          if (key != PXImportAttribute.ImportFlag && !cache.Keys.Contains(key))
          {
            flag = true;
            break;
          }
        }
        return !flag;
      }

      public bool AnyUpdate(PXCache cache, OrderedDictionary keys, OrderedDictionary vals)
      {
        foreach (string key1 in (IEnumerable) vals.Keys)
        {
          if (!(key1 == PXImportAttribute.ImportFlag))
          {
            if (!cache.Keys.Contains(key1) || !keys.Contains((object) key1))
              return true;
            object val = vals[(object) key1];
            object key2 = keys[(object) key1];
            if (val is string str1 && key2 is string str2)
            {
              if (!PXLocalesProvider.CollationComparer.Equals(str1.TrimEnd(), str2.TrimEnd()))
                return true;
            }
            else if (!object.Equals(val, key2))
              return true;
          }
        }
        return false;
      }

      public void CommitFiles(PXCache cache, OrderedDictionary vals, bool passThrough)
      {
        Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
        foreach (DictionaryEntry val in vals)
        {
          if (val.Key is string key && key.Length > 1 && SyImportProcessor.SyStep.IsFileName(key) && val.Value is string str && str.Length > 0)
          {
            if (dictionary == null)
              dictionary = new Dictionary<string, string>();
            dictionary[SyImportProcessor.SyStep.GetFileName(key)] = str;
          }
        }
        if (dictionary == null)
          return;
        List<Guid> guidList1 = new List<Guid>();
        string filePrefix = SyImportProcessor.SyStep.GetFilePrefix(cache, cache.Current);
        foreach (UploadFile uploadFile in SyImportProcessor.SyStep.GetUploadFiles(cache.Graph, cache, cache.Current))
        {
          string key;
          string s;
          Guid? fileId;
          if (dictionary.TryGetValue(key = uploadFile.Name, out s) || !string.IsNullOrEmpty(filePrefix) && uploadFile.Name.StartsWith(filePrefix) && dictionary.TryGetValue(key = uploadFile.Name.Substring(filePrefix.Length), out s))
          {
            UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
            fileId = uploadFile.FileID;
            FileInfo finfo = new FileInfo(fileId.Value, uploadFile.Name, (string) null, Convert.FromBase64String(s));
            instance.SaveFile(finfo, FileExistsAction.CreateVersion);
            dictionary.Remove(key);
          }
          List<Guid> guidList2 = guidList1;
          fileId = uploadFile.FileID;
          Guid guid = fileId.Value;
          guidList2.Add(guid);
        }
        if (dictionary.Count <= 0)
          return;
        bool flag = false;
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
        {
          UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
          string key = keyValuePair.Key;
          int num = key.IndexOf(")\\");
          FileInfo fileInfo = new FileInfo(num <= key.IndexOf(" (") ? filePrefix + key : filePrefix + key.Substring(num + 1), (string) null, Convert.FromBase64String(keyValuePair.Value));
          fileInfo.Comment = "";
          FileInfo finfo = fileInfo;
          if (instance.SaveFile(finfo, FileExistsAction.CreateVersion))
          {
            guidList1.Add(fileInfo.UID.Value);
            flag = true;
          }
        }
        if (!flag)
          return;
        if (passThrough)
          PXNoteAttribute.ForcePassThrow(cache, (string) null);
        PXNoteAttribute.SetFileNotes(cache, cache.Current, guidList1.ToArray());
      }

      public void CommitNotes(
        PXCache cache,
        OrderedDictionary vals,
        SyImportRowResult importResult)
      {
        foreach (DictionaryEntry val in vals)
        {
          if (val.Value is string note && val.Key is string key && key.OrdinalEquals("NoteText"))
          {
            PXNoteAttribute.SetNote(cache, cache.Current, note);
            if (importResult != null)
              importResult.NoteChanged = true;
          }
        }
      }

      /// <summary>Gets the field with the specified name or creates new one and adds it to the fields collection of this view.</summary>
      /// <returns>True if field with the specified name was found; False if new field was created and added to the collection.</returns>
      internal bool GetFieldOrAddNew(string fieldName, out SyImportProcessor.SyStep.SyField field)
      {
        int num = this.Fields.TryGetValue(fieldName, out field) ? 1 : 0;
        if (num != 0)
          return num != 0;
        this.Fields.Add(fieldName, field = new SyImportProcessor.SyStep.SyField());
        return num != 0;
      }

      private sealed class SyViewGlobal
      {
        public object Current;
        public object Previous;
        public long KeysFromCurrent = -1;
        public WebDialogResult? Answer;
        public long MinRevision;
        public bool InsertCalled;
      }
    }

    internal delegate void FillImportResult(int rowIndex, SyImportRowResult result);
  }

  internal class DetailBypassedException(string message) : PXException(message)
  {
  }

  internal class InvalidTargetException : PXException
  {
    public InvalidTargetException()
      : base("The target found violates the constraints.")
    {
    }

    public InvalidTargetException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      ReflectionSerializer.RestoreObjectProps<SyImportProcessor.InvalidTargetException>(this, info);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ReflectionSerializer.GetObjectData<SyImportProcessor.InvalidTargetException>(this, info);
      base.GetObjectData(info, context);
    }
  }

  internal class PXFieldErrorException(string format, params object[] args) : PXSetPropertyException(format, args)
  {
  }

  private class PXInsertRightsScope : IDisposable
  {
    private readonly PXCache _cache;
    private readonly bool _prevAllowInsert;
    private readonly bool _prevInsertRights;
    private readonly List<PXUIFieldAttribute> _uiFieldsWithoutEnabledRights;

    public PXInsertRightsScope(PXCache cache)
    {
      this._cache = cache;
      this._prevAllowInsert = cache.AllowInsert;
      this._prevInsertRights = cache.InsertRights;
      this._uiFieldsWithoutEnabledRights = new List<PXUIFieldAttribute>();
    }

    public void SetRightsOnCacheAndKeyFields()
    {
      if (!this._cache.AllowInsert)
      {
        if (!this._cache.InsertRights)
          this._cache.InsertRights = true;
        this._cache.AllowInsert = true;
      }
      foreach (string key in (IEnumerable<string>) this._cache.Keys)
      {
        PXUIFieldAttribute pxuiFieldAttribute = this._cache.GetAttributes(key).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (f => !f.EnableRights));
        if (pxuiFieldAttribute != null)
        {
          pxuiFieldAttribute.EnableRights = true;
          this._uiFieldsWithoutEnabledRights.Add(pxuiFieldAttribute);
        }
      }
    }

    /// <summary>Throws exception if there is any inserted records and no insert rights.</summary>
    /// <exception cref="T:PX.Data.PXException" />
    public void CheckRights(PXAction action, string actionName)
    {
      if (!this._cache.AllowInsert && NonGenericIEnumerableExtensions.Any_(this._cache.Inserted))
        throw new PXException("Cannot invoke the {0} action. You have no access rights for the INSERT operation.", new object[1]
        {
          (object) (action.GetCaption() ?? actionName)
        });
    }

    public void Dispose()
    {
      this._cache.InsertRights = this._prevInsertRights;
      this._cache.AllowInsert = this._prevAllowInsert;
      foreach (PXUIFieldAttribute withoutEnabledRight in this._uiFieldsWithoutEnabledRights)
        withoutEnabledRight.EnableRights = false;
    }
  }
}
