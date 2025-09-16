// Decompiled with JetBrains decompiler
// Type: PX.Api.SyExportContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Helpers;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Api;

internal class SyExportContext
{
  public readonly string GraphName;
  public readonly string PrimaryView;
  public readonly string GridView;
  public readonly SyCommand[] Commands;
  public readonly string ScreenID;
  public readonly Dictionary<string, PXFilterRow[]> ViewFilters;
  public readonly bool BreakOnError;
  public readonly bool BreakOnTarget;
  public readonly string[] ProviderFields;
  public readonly LinkedSelectorViews LinkedSelectorViews;
  public Exception Error;
  public PXGraph Graph;
  public SYMapping.RepeatingOption RepeatingData;
  public int StartRow;
  public int TopCount;
  public bool ForceState;
  public KeyValuePair<System.DateTime, System.DateTime>? TimeRange;
  public KeyValuePair<System.DateTime, System.DateTime>? TimeRangeUtc;
  public bool NewOnly;
  public bool NewApi;
  internal bool ShouldReadArchive;
  internal bool AddTranslations;
  public bool OneRun;
  public Dictionary<string, KeyValuePair<string, bool>[]> Sorts;

  public string Locale { get; set; }

  /// <remarks>
  /// For some (yet unknown) reason export engine ignores view filters when a Search on key value is present.
  /// This behavior is wrong for (at least) Contract-based API, so we're adding an option to turn it off.
  /// We're not turning it off at all because of backwards compatibility. Maybe we will later.
  /// </remarks>
  public bool SkipFiltersWhenSearchPresent => !this.NewApi;

  public SyExportContext(
    SYMapping mapping,
    IEnumerable<SYMappingField> fields,
    string[] providerFields,
    Dictionary<string, PXFilterRow[]> viewFilters,
    bool breakOnError,
    int start = 0,
    int count = 0,
    LinkedSelectorViews selectorViews = null,
    string rowFilterField = null)
  {
    this.ProviderFields = providerFields;
    this.GraphName = mapping.GraphName;
    this.PrimaryView = mapping.ViewName;
    this.GridView = mapping.GridViewName;
    this.ScreenID = mapping.ScreenID;
    this.RepeatingData = mapping.RepeatingData.HasValue ? (SYMapping.RepeatingOption) mapping.RepeatingData.Value : SYMapping.RepeatingOption.Primary;
    this.Locale = mapping.FormatLocale;
    this.StartRow = start;
    this.TopCount = count;
    List<SyCommand> list = fields.Select<SYMappingField, SyCommand>(new Func<SYMappingField, SyCommand>(this.ParseCommandWithErrors)).ToList<SyCommand>();
    SyExportContext.SetViewAlias(list);
    SyExportContext.AppendRowNumbers(list, this.StartRow, this.TopCount, rowFilterField);
    this.Commands = list.ToArray();
    this.ViewFilters = viewFilters ?? new Dictionary<string, PXFilterRow[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.BreakOnError = breakOnError;
    this.LinkedSelectorViews = selectorViews ?? new LinkedSelectorViews();
  }

  public SyExportContext(
    string screenId,
    string graphName,
    string viewName,
    SyCommand[] commands,
    string[] providerFields,
    Dictionary<string, PXFilterRow[]> viewFilters,
    SYMapping.RepeatingOption repeatingData,
    bool breakOnError,
    LinkedSelectorViews selectorViews)
  {
    this.GraphName = graphName;
    this.PrimaryView = viewName;
    this.GridView = (string) null;
    this.ScreenID = screenId;
    this.RepeatingData = repeatingData;
    this.StartRow = 0;
    this.TopCount = 0;
    List<SyCommand> list = ((IEnumerable<SyCommand>) commands).ToList<SyCommand>();
    SyExportContext.SetViewAlias(list);
    SyExportContext.AppendRowNumbers(list);
    this.Commands = list.ToArray();
    this.ProviderFields = providerFields;
    this.ViewFilters = viewFilters ?? new Dictionary<string, PXFilterRow[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.BreakOnError = breakOnError;
    this.LinkedSelectorViews = selectorViews;
    this.Locale = Thread.CurrentThread.CurrentCulture.Name;
  }

  private static void AppendRowNumbers(
    List<SyCommand> commands,
    int startRow = 0,
    int count = 0,
    string rowFilterField = null)
  {
    HashSet<string> stringSet = new HashSet<string>(commands.Where<SyCommand>((Func<SyCommand, bool>) (c => c.CommandType == SyCommandType.RowNumber)).Select<SyCommand, string>((Func<SyCommand, string>) (c => c.View)));
    for (int index = 0; index < commands.Count; ++index)
    {
      if (!stringSet.Contains(commands[index].View))
      {
        if (count != 0)
          commands.Insert(index, new SyCommand()
          {
            CommandType = SyCommandType.RowCount,
            View = commands[index].View,
            ViewAlias = commands[index].View,
            Formula = $"={count}",
            Field = startRow == 0 ? (string) null : rowFilterField
          });
        commands.Insert(index, new SyCommand()
        {
          CommandType = SyCommandType.RowNumber,
          View = commands[index].View,
          ViewAlias = commands[index].View,
          Formula = startRow == 0 ? SyExportContext.GetFieldID(commands[index].View) : $"={startRow}",
          Field = startRow == 0 ? (string) null : rowFilterField
        });
        stringSet.Add(commands[index].View);
        ++index;
      }
    }
  }

  private static void SetViewAlias(List<SyCommand> commands)
  {
    ILookup<string, SyCommand> lookup = commands.Where<SyCommand>((Func<SyCommand, bool>) (c => c.ViewAlias != c.View)).ToLookup<SyCommand, string>((Func<SyCommand, string>) (c => c.View));
    foreach (IndexedValue<SyCommand> indexedValue in commands.EnumWithIndex<SyCommand>())
    {
      SyCommand syCommand = indexedValue.Value;
      if (!lookup.Contains(syCommand.View))
        syCommand.ViewAlias = SyExportContext.GetViewAlias(commands, indexedValue.Index);
    }
  }

  private static bool isAlterViewCmd(SyCommand cmd)
  {
    if (cmd.CommandType == SyCommandType.NewRow)
      return true;
    return (cmd.CommandType == SyCommandType.Parameter || cmd.CommandType == SyCommandType.Search) && !string.IsNullOrEmpty(cmd.Formula) && !cmd.Formula.StartsWith("=[");
  }

  private static string GetViewAlias(List<SyCommand> commands, int index)
  {
    string view = commands[index].View;
    SyCommand cmd = (SyCommand) null;
    int num = 0;
    for (int index1 = 0; index1 <= index; ++index1)
    {
      SyCommand command = commands[index1];
      if (!(command.View != view))
      {
        if (cmd != null)
        {
          bool flag = SyExportContext.isAlterViewCmd(command);
          if (!SyExportContext.isAlterViewCmd(cmd) && !cmd.Field.OrdinalEquals("BranchID") && flag)
            ++num;
        }
        cmd = command;
      }
    }
    return num != 0 ? $"{view}:{num.ToString()}" : view;
  }

  public string FindViewAlias(SyCommand currentCommand, string viewName)
  {
    return currentCommand.View == viewName ? currentCommand.ViewAlias : ((IEnumerable<SyCommand>) this.Commands).TakeWhile<SyCommand>((Func<SyCommand, bool>) (c => c != currentCommand)).Last<SyCommand>((Func<SyCommand, bool>) (c => c.View == viewName)).ViewAlias;
  }

  private SyCommand ParseCommandWithErrors(SYMappingField field)
  {
    SyCommand command = SyMappingUtils.ParseCommand(field);
    SyExportContext.ParseCommand(command, this);
    return command;
  }

  internal static SyCommand ParseCommand(SYMappingField field)
  {
    SyCommand command = SyMappingUtils.ParseCommand(field);
    SyExportContext.ParseCommand(command, (SyExportContext) null);
    return command;
  }

  private static void ParseCommand(SyCommand cmd, SyExportContext context)
  {
    if (cmd.Formula == "=Every")
    {
      cmd.CommandType = SyCommandType.EnumFieldValues;
      cmd.Formula = $"=[{SyExportContext.GetFieldID(cmd)}]";
    }
    else if (cmd.Formula != null && !cmd.Formula.StartsWith("="))
    {
      string str = cmd.Formula.TrimStart('$');
      if ((context == null ? 1 : (((IEnumerable<string>) context.ProviderFields).Contains<string>(str) ? 1 : 0)) == 0)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The field is not supported by the provider: {0}", (object) str));
      if (cmd.Field != null && cmd.Field.StartsWith("//"))
      {
        cmd.Formula = cmd.Field.Substring(2);
        cmd.CommandType = SyCommandType.ExportPath;
      }
      else
      {
        cmd.Formula = cmd.Field;
        cmd.CommandType = SyCommandType.ExportField;
      }
      cmd.Field = str;
    }
    else
      SyImportContext.ParseCommand(cmd);
  }

  public static string GetViewId(SyCommand cmd)
  {
    return cmd.CommandType == SyCommandType.EnumFieldValues ? $"{cmd.ViewAlias}_{cmd.Field}" : cmd.ViewAlias;
  }

  public static string GetFieldID(SyCommand cmd)
  {
    if (cmd.CommandType == SyCommandType.EnumFieldValues)
      return $"Selector_{cmd.ViewAlias}_{cmd.Field}";
    throw new PXException("GetFieldGroup failed");
  }

  public static string GetFieldID(string viewAlias, string field) => $"{viewAlias}_{field}";

  public static string GetFieldID(string viewName) => "RowNumber_" + viewName;

  public bool IsPrimaryView(string viewName) => viewName.OrdinalEquals(this.PrimaryView);

  public void CleanCommands()
  {
    foreach (SyCommand command in this.Commands)
      command.ViewAlias = SyMappingUtils.CleanViewName(command.ViewAlias);
  }
}
