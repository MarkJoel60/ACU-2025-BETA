// Decompiled with JetBrains decompiler
// Type: PX.Api.SyImportContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api;

internal class SyImportContext
{
  public SyImportContext.RowPersistingDelegate RowPersisting;
  public readonly string GraphName;
  public readonly string PrimaryView;
  internal string PrimaryDataView;
  internal int RowIndex;
  public PXGraph Graph;
  public SyCommand[] Commands;
  public readonly PXSYTable SourceTable;
  public readonly string[] SourceFields;
  public readonly SyImportRowResult[] ImportResult;
  public readonly PXFilterRow[] TargetConditions;
  public readonly bool BreakOnError;
  public readonly bool BreakOnTarget;
  public readonly bool IsSimpleMapping;
  public readonly System.DateTime PrepareTime;
  public bool ProcessInParallel;

  public SYValidation ValidationType { get; internal set; }

  internal bool IsSetup { get; set; }

  internal bool IsFilter { get; set; }

  public int StartRow { get; set; }

  public int EndRow { get; set; }

  public StringComparer FieldNamesComparer { get; }

  public int BatchSize { get; set; }

  public SyImportContext(
    SYMapping mapping,
    SYMappingField[] fields,
    PXSYTable sourceTable,
    bool breakOnError,
    bool breakOnTarget,
    SYValidation validationType,
    PXFilterRow[] filters,
    StringComparer fieldNamesComparer,
    int batchSize = 0)
    : this(sourceTable, 0, sourceTable.Count - 1)
  {
    int num;
    if (mapping != null)
    {
      bool? isSimpleMapping = mapping.IsSimpleMapping;
      bool flag = true;
      num = isSimpleMapping.GetValueOrDefault() == flag & isSimpleMapping.HasValue ? 1 : 0;
    }
    else
      num = 0;
    this.IsSimpleMapping = num != 0;
    System.DateTime utcNow;
    if (mapping != null)
    {
      System.DateTime? preparedOn = mapping.PreparedOn;
      if (preparedOn.HasValue)
      {
        preparedOn = mapping.PreparedOn;
        utcNow = preparedOn.Value;
        goto label_7;
      }
    }
    utcNow = System.DateTime.UtcNow;
label_7:
    this.PrepareTime = utcNow;
    this.GraphName = mapping.GraphName;
    this.PrimaryView = mapping.ViewName;
    this.BreakOnError = breakOnError;
    this.BreakOnTarget = breakOnTarget;
    this.ValidationType = validationType;
    this.FieldNamesComparer = fieldNamesComparer ?? StringComparer.Ordinal;
    this.Commands = ((IEnumerable<SYMappingField>) fields).Select<SYMappingField, SyCommand>((Func<SYMappingField, SyCommand>) (f => SyImportContext.ParseCommand(f))).ToArray<SyCommand>();
    this.TargetConditions = filters;
    this.BatchSize = batchSize;
  }

  public SyImportContext(SyImportContext context, int startRow, int endRow)
    : this(context.SourceTable, startRow, endRow)
  {
    this.ProcessInParallel = true;
    this.RowPersisting = context.RowPersisting;
    this.IsSimpleMapping = context.IsSimpleMapping;
    this.PrepareTime = context.PrepareTime;
    this.GraphName = context.GraphName;
    this.PrimaryView = context.PrimaryView;
    this.BreakOnError = context.BreakOnError;
    this.BreakOnTarget = context.BreakOnTarget;
    this.ValidationType = context.ValidationType;
    this.FieldNamesComparer = context.FieldNamesComparer;
    this.Commands = ((IEnumerable<SyCommand>) context.Commands).Select<SyCommand, SyCommand>((Func<SyCommand, SyCommand>) (command => (SyCommand) command.Clone())).ToArray<SyCommand>();
    this.TargetConditions = ((IEnumerable<PXFilterRow>) context.TargetConditions).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (filter => (PXFilterRow) filter.Clone())).ToArray<PXFilterRow>();
    this.BatchSize = context.BatchSize;
  }

  private SyImportContext(PXSYTable sourceTable, int startRow, int endRow)
  {
    this.SourceTable = sourceTable;
    this.SourceFields = sourceTable.Columns.Select<string, string>((Func<string, string>) (name => name?.Trim())).ToArray<string>();
    this.StartRow = startRow;
    this.EndRow = endRow;
    this.ImportResult = new SyImportRowResult[sourceTable.Count];
    for (int startRow1 = this.StartRow; startRow1 <= this.EndRow; ++startRow1)
      this.ImportResult[startRow1] = new SyImportRowResult(this.SourceFields);
  }

  internal void Prepare()
  {
    if (this.Graph == null || this.ProcessInParallel)
    {
      using (new PXPreserveScope())
        this.Graph = SyImportProcessor.CreateGraph(this.GraphName);
    }
    PXCache cache = this.Graph.Views[this.PrimaryView].Cache;
    this.IsSetup = cache.Keys.Count == 0;
    if (this.IsSimpleMapping)
    {
      foreach (string viewName in ((IEnumerable<SyCommand>) this.Commands).Where<SyCommand>((Func<SyCommand, bool>) (command => !Str.IsNullOrEmpty(command.View))).Select<SyCommand, string>((Func<SyCommand, string>) (command => command.View)).Distinct<string>())
        DialogManager.SetAnswer(this.Graph, viewName, (string) null, WebDialogResult.Yes);
      if (!this.IsSetup && !this.GetKeyFieldCommands().Any<SyCommand>())
      {
        foreach (PXAction action in (IEnumerable) this.Graph.Actions.Values)
        {
          string name;
          if (SyImportProcessor.SyStep.IsActionCancel(action, out name))
          {
            List<SyCommand> syCommandList = new List<SyCommand>((IEnumerable<SyCommand>) this.Commands);
            syCommandList.Insert(0, new SyCommand()
            {
              CommandType = SyCommandType.Action,
              View = this.PrimaryView,
              ViewAlias = this.PrimaryView,
              Field = name
            });
            syCommandList.Insert(0, new SyCommand()
            {
              CommandType = SyCommandType.Search,
              View = this.PrimaryView,
              ViewAlias = this.PrimaryView,
              Field = "NoteID"
            });
            this.Commands = syCommandList.ToArray();
          }
        }
      }
    }
    this.IsFilter = this.IsSetup && this.Graph.Defaults != null && this.Graph.Defaults.ContainsKey(cache.GetItemType());
    this.PrimaryDataView = this.IsFilter ? this.GetPrimaryDataView() : this.PrimaryView;
  }

  private string GetPrimaryDataView()
  {
    foreach (string key in ((IEnumerable<SyCommand>) this.Commands).Select<SyCommand, string>((Func<SyCommand, string>) (c => c.View)).Distinct<string>())
    {
      PXViewExtensionAttribute[] attributes = this.Graph.Views[key].Attributes;
      if ((attributes != null ? (((IEnumerable<PXViewExtensionAttribute>) attributes).Any<PXViewExtensionAttribute>((Func<PXViewExtensionAttribute, bool>) (c => c is PXImportAttribute)) ? 1 : 0) : 0) != 0)
        return key;
    }
    return this.Graph.PrimaryView;
  }

  private static string ExtractKeyFieldName(SyCommand c)
  {
    return StringExtensions.FirstSegment(c.Field, '!');
  }

  public static SyCommand ParseCommand(SYMappingField field)
  {
    SyCommand command = SyMappingUtils.ParseCommand(field);
    SyImportContext.ParseCommand(command);
    return command;
  }

  internal static void ParseCommand(SyCommand cmd)
  {
    if (cmd.Field.StartsWith("@@"))
    {
      cmd.Field = cmd.Field.Substring(2);
      cmd.CommandType = SyCommandType.Search;
    }
    else if (cmd.Field.StartsWith("@"))
    {
      cmd.Field = cmd.Field.Substring(1);
      cmd.CommandType = SyCommandType.Parameter;
    }
    else if (cmd.Field.StartsWith("##"))
    {
      cmd.Field = (string) null;
      string a = cmd.Formula == null ? (string) null : cmd.Formula.Replace(" ", "");
      if (a.OrdinalEquals("=-1"))
      {
        cmd.Formula = (string) null;
        cmd.CommandType = SyCommandType.NewRow;
      }
      else if (a.OrdinalEquals("=-2"))
      {
        cmd.Formula = (string) null;
        cmd.CommandType = SyCommandType.DeleteRow;
      }
      else
        cmd.CommandType = SyCommandType.RowNumber;
    }
    else if (cmd.Field.StartsWith("//"))
    {
      cmd.Field = cmd.Field.Substring(2);
      cmd.CommandType = SyCommandType.Path;
    }
    else if (cmd.Field.Equals("<Set: Branch>"))
    {
      cmd.Field = (string) null;
      cmd.CommandType = SyCommandType.SetBranch;
    }
    else if (cmd.Field.StartsWith("<") && cmd.Field.EndsWith(">"))
    {
      cmd.Field = cmd.Field.Substring(1, cmd.Field.Length - 2);
      cmd.CommandType = SyCommandType.Action;
      SyCommand syCommand = cmd;
      ExecutionBehavior? executionBehavior1 = syCommand.ExecutionBehavior;
      executionBehavior1.GetValueOrDefault();
      if (executionBehavior1.HasValue)
        return;
      ExecutionBehavior executionBehavior2 = ExecutionBehavior.ForEachRecord;
      syCommand.ExecutionBehavior = new ExecutionBehavior?(executionBehavior2);
    }
    else if (cmd.Field.StartsWith("??"))
    {
      cmd.Field = (string) null;
      cmd.CommandType = SyCommandType.Answer;
    }
    else
      cmd.CommandType = SyCommandType.Field;
  }

  internal IEnumerable<SyCommand> GetKeyFieldCommands()
  {
    return ((IEnumerable<SyCommand>) this.Commands).Where<SyCommand>((Func<SyCommand, bool>) (cmd => cmd.CommandType == SyCommandType.Search && this.IsPrimaryView(cmd.View)));
  }

  internal string[] GetKeyExternalNames()
  {
    string[] keyFieldNames = this.GetKeyFieldCommands().Select<SyCommand, string>((Func<SyCommand, string>) (c => c.Field)).ToArray<string>();
    return ((IEnumerable<SyCommand>) this.Commands).Where<SyCommand>((Func<SyCommand, bool>) (c => c.CommandType == SyCommandType.Field)).Where<SyCommand>((Func<SyCommand, bool>) (c => ((IEnumerable<string>) keyFieldNames).Contains<string>(SyImportContext.ExtractKeyFieldName(c)))).Select<SyCommand, string>((Func<SyCommand, string>) (c => c.Formula)).Distinct<string>().ToArray<string>();
  }

  internal void ClearAndShrinkCachesIfNeeded()
  {
    if (this.RowIndex <= 0 || this.RowIndex % 100 != 0)
      return;
    this.Graph.Clear(PXClearOption.ClearQueriesOnly);
  }

  internal void FillImportResultExternalKeys()
  {
    this.Prepare();
    SyFormulaProcessor formulaProcessor = new SyFormulaProcessor();
    string[] keyExternalNames = this.GetKeyExternalNames();
    for (this.RowIndex = this.StartRow; this.RowIndex <= this.EndRow; ++this.RowIndex)
    {
      SyImportProcessor.SaveLineNumber(this.RowIndex + 1);
      SyImportRowResult rowImportResult = this.ImportResult[this.RowIndex];
      SyImportProcessor.SyExternalValues externalValues = new SyImportProcessor.SyExternalValues(this);
      SyImportProcessor.SyStep syStep = new SyImportProcessor.SyStep(this.Graph, formulaProcessor, this.PrimaryView, externalValues: externalValues, rowImportResult: rowImportResult);
      rowImportResult.ExternalKeys = syStep.GetKeyExternalValues(keyExternalNames);
    }
  }

  internal void FillImportResultExternalKeys(SyImportContext context)
  {
    this.Prepare();
    for (this.RowIndex = this.StartRow; this.RowIndex <= this.EndRow; ++this.RowIndex)
      this.ImportResult[this.RowIndex].ExternalKeys = context.ImportResult[this.RowIndex].ExternalKeys;
  }

  public bool IsPrimaryView(string viewName) => viewName.OrdinalEquals(this.PrimaryView);

  public void PropogateErrorsUp()
  {
    Dictionary<string, object> dict2 = (Dictionary<string, object>) null;
    for (int endRow = this.EndRow; endRow >= this.StartRow; --endRow)
    {
      SyImportRowResult syImportRowResult = this.ImportResult[endRow];
      if (syImportRowResult.Error != null)
        dict2 = syImportRowResult.ExternalKeys;
      else if (!syImportRowResult.IsPersisted && SyImportProcessor.RowKeysEqual(syImportRowResult.ExternalKeys, dict2))
        syImportRowResult.Error = (Exception) new SyImportProcessor.DetailBypassedException("The record was not processed because of an error during processing of the next record.");
    }
  }

  public IEnumerable<SyImportContext> SplitToBatches(PXParallelProcessingOptions parallelOpt)
  {
    SyImportContext context = this;
    context.FillImportResultExternalKeys();
    if (!parallelOpt.IsEnabled)
    {
      yield return context;
    }
    else
    {
      if (parallelOpt.BatchSize <= 0)
        parallelOpt.BatchSize = 100;
      int startRow = 0;
      int endRow = 0;
      if (context.IsSimpleMapping && !context.IsSetup && !context.GetKeyFieldCommands().Any<SyCommand>())
      {
        for (; startRow <= context.EndRow; startRow = endRow + 1)
        {
          endRow = System.Math.Min(context.EndRow, startRow + parallelOpt.BatchSize);
          yield return new SyImportContext(context, startRow, endRow);
        }
      }
      else
      {
        Dictionary<string, object> dict1 = new Dictionary<string, object>();
        int count = 0;
        SyImportRowResult[] syImportRowResultArray = context.ImportResult;
        for (int index = 0; index < syImportRowResultArray.Length; ++index)
        {
          SyImportRowResult result = syImportRowResultArray[index];
          if (count >= parallelOpt.BatchSize && !SyImportProcessor.RowKeysEqual(dict1, result.ExternalKeys))
          {
            yield return new SyImportContext(context, startRow, startRow + count - 1);
            startRow += count;
            count = 0;
          }
          ++count;
          dict1 = result.ExternalKeys;
          result = (SyImportRowResult) null;
        }
        syImportRowResultArray = (SyImportRowResult[]) null;
        yield return new SyImportContext(context, startRow, startRow + count - 1);
      }
    }
  }

  public bool IsEndOfBatch() => this.BatchSize > 0 && (this.RowIndex + 1) % this.BatchSize == 0;

  public bool IsFirstRow() => this.RowIndex == this.StartRow;

  public bool IsLastRow() => this.RowIndex == this.EndRow;

  public delegate void RowPersistingDelegate(int rowIndex);
}
