// Decompiled with JetBrains decompiler
// Type: PX.Api.ScreenUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api.Helpers;
using PX.Api.Mobile;
using PX.Api.Models;
using PX.Api.Reports;
using PX.Api.Services;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.Api.Export;
using PX.Data.Api.Helpers;
using PX.Data.Api.Mobile;
using PX.Data.Api.Mobile.SignManager;
using PX.Data.Description;
using PX.Metadata;
using PX.Reports;
using PX.Reports.Controls;
using Serilog.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Api;

public static class ScreenUtils
{
  public const string SubmitFieldKeys = "SubmitFieldKeys";
  public const string SubmitFieldCommands = "SubmitFieldCommands";
  public const string SubmitFieldErrors = "SubmitFieldErrors";
  public const string SubmitReportKeys = "SubmitReportParameterKeys";
  public const string SubmitReportKeyPreProcessInstance = "SubmitReportKeyPreProcessInstance";
  public const string SubmitReportKeyPreProcessIsActive = "SubmitReportKeyPreProcessIsActive";
  public const string SubmitReportTemplateKeys = "SubmitReportTemplateKeys";
  public const char ScreenViewNameSeparator = ':';
  private static bool _isReport;
  private static string _filters = "Filters";
  private static string _dataField = "DataField";
  private static string _condition = "Condition";
  private static readonly Dictionary<string, string> _specialNameSubstitutionDict = new Dictionary<string, string>()
  {
    {
      "%",
      "PERCENT"
    },
    {
      "#",
      "NUMBER"
    }
  };

  /// <summary>
  /// <para>Returns the current ScreenInfo provider, if any, or the dummy provider if <see cref="T:CommonServiceLocator.ServiceLocator" /> is not set up.</para>
  /// <para>Use DI via injecting <see cref="T:PX.Metadata.IScreenInfoProvider" /> whenever possible.</para>
  /// </summary>
  public static IScreenInfoProvider ScreenInfo
  {
    get
    {
      return ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<IScreenInfoProvider>() : DummyScreenInfoProvider.Instance;
    }
  }

  [PXInternalUseOnly]
  public static Content GetScreenInfoWithServiceCommands(
    bool appendDescriptors,
    bool includeHiddenFields,
    string screenID,
    bool forceDefaultLocale = false)
  {
    return ScreenUtils.ScreenInfo.GetScreenInfoWithServiceCommands(appendDescriptors, includeHiddenFields, screenID, forceDefaultLocale);
  }

  [PXInternalUseOnly]
  public static string GetScreenTitle(string screenId)
  {
    return PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId)?.Title;
  }

  [PXInternalUseOnly]
  public static string GetScreenGraphType(string screenId)
  {
    return PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId)?.GraphType;
  }

  [PXInternalUseOnly]
  public static bool ScreenExists(string screenId)
  {
    return PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId) != null;
  }

  [PXInternalUseOnly]
  public static bool CheckIfScreenAccessibleToUser(string screenId)
  {
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId);
    return mapNodeByScreenId != null && mapNodeByScreenId.IsAccessibleToUser();
  }

  [PXInternalUseOnly]
  internal static Content GetScreenInfoWithServiceCommands(
    this PXSiteMap.ScreenInfo info,
    string screenID,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false)
  {
    if (info == null)
      return (Content) null;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID);
    ContainerSearchStrategy containerSearchStrategy = mapNodeByScreenId?.SelectedUI == "T" || info.IsNewUI ? ContainerSearchStrategy.ViewNameFirst : ContainerSearchStrategy.ContainerNameFirst;
    Content screenInfo = info.GetScreenInfo(screenID, appendDescriptors, includeHiddenFields, forceDefaultLocale, containerSearchStrategy);
    ScreenUtils._isReport = mapNodeByScreenId != null && mapNodeByScreenId.Url.Contains(".rpx");
    foreach (Container container in screenInfo.Containers)
    {
      ScreenUtils.AddServiceCommands(screenID, (IReadOnlyList<PX.Api.Models.Field>) container.Fields, info);
      if (container.ServiceCommands != null && container.ServiceCommands.Length != 0)
      {
        foreach (Command serviceCommand in container.ServiceCommands)
        {
          if (serviceCommand is EveryValue)
          {
            foreach (Command field in container.Fields)
            {
              if (serviceCommand.FieldName == field.FieldName)
              {
                serviceCommand.Commit = field.Commit;
                serviceCommand.LinkedCommand = field.LinkedCommand;
                break;
              }
            }
          }
        }
      }
    }
    return screenInfo;
  }

  internal static IEnumerable<(string Name, string DisplayName)> GetGraphScreenSingleEntitySections(
    PXGraph graph,
    string screenId)
  {
    return ScreenUtils.GetGraphScreenSingleEntityContainers(graph, screenId).Select<KeyValuePair<string, PXViewDescription>, (string, string)>((Func<KeyValuePair<string, PXViewDescription>, (string, string)>) (container => (container.Key, container.Value.DisplayName)));
  }

  internal static IEnumerable<string> GetGraphScreenSingleEntityAndWorkflowViewNames(
    PXGraph graph,
    string screenId)
  {
    List<string> list = ScreenUtils.GetGraphScreenSingleEntityContainers(graph, screenId).Select<KeyValuePair<string, PXViewDescription>, string>((Func<KeyValuePair<string, PXViewDescription>, string>) (container => ScreenUtils.NormalizeViewName(container.Key))).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToList<string>();
    if (graph.Views.ContainsKey("FilterPreview"))
      list.Add("FilterPreview");
    return (IEnumerable<string>) list;
  }

  internal static IEnumerable<KeyValuePair<string, PXViewDescription>> GetGraphScreenSingleEntityContainers(
    PXGraph graph,
    string screenId,
    bool withPrimaryView = false)
  {
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenId);
    if (screenInfo == null)
      return (IEnumerable<KeyValuePair<string, PXViewDescription>>) new Dictionary<string, PXViewDescription>();
    List<string> viewNames = GraphHelper.GetGraphViews(graph.GetType(), false, false).Select<PXViewInfo, string>((Func<PXViewInfo, string>) (view => view.Name)).Where<string>((Func<string, bool>) (viewName => (withPrimaryView || viewName != graph.PrimaryView) && graph.Views.ContainsKey(viewName))).ToList<string>();
    return screenInfo.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (container => !container.Value.IsGrid && viewNames.Contains(ScreenUtils.NormalizeViewName(container.Key))));
  }

  internal static ICollection<Content> GetSchema(
    string id,
    SchemaMode schemaMode,
    ScreenUtils.WebReportSettings savedSettings)
  {
    return (ICollection<Content>) new List<Content>()
    {
      ServiceLocator.Current.GetInstance<IScreenService>().GetSchema(id, schemaMode)
    };
  }

  private static PXSiteMap.ScreenInfo GetScreenInfoWithoutHttpContext(
    string screenId,
    bool forceDefaultLocale,
    out Exception error)
  {
    PXSiteMap.ScreenInfo withoutHttpContext = (PXSiteMap.ScreenInfo) null;
    error = (Exception) null;
    if (ServiceLocator.IsLocationProviderSet)
    {
      IScreenInfoProvider instance = ServiceLocator.Current.GetInstance<IScreenInfoProvider>();
      withoutHttpContext = forceDefaultLocale ? instance.TryGetWithInvariantLocale(screenId, out error) : instance.TryGet(screenId, out error);
    }
    return withoutHttpContext;
  }

  [PXInternalUseOnly]
  public static Content GetScreenInfo(
    string screenId,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false)
  {
    return ScreenUtils.ScreenInfo.GetScreenInfo(screenId, appendDescriptors, includeHiddenFields, forceDefaultLocale);
  }

  internal static Content GetScreenInfo(
    this PXSiteMap.ScreenInfo info,
    string screenId,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false,
    ContainerSearchStrategy containerSearchStrategy = ContainerSearchStrategy.ContainerNameFirst)
  {
    if (info == null)
      return (Content) null;
    HashSet<string> prohibited = new HashSet<string>((IEnumerable<string>) new string[22]
    {
      "Actions",
      "Filter",
      "Command",
      "ProcessResult",
      "ProcessStatus",
      "EveryValue",
      "Key",
      "Action",
      "Field",
      "Value",
      "Answer",
      "RowNumber",
      "NewRow",
      "DeleteRow",
      "Parameter",
      "Content",
      "ImportResult",
      "PrimaryKey",
      "SchemaMode",
      "ElementTypes",
      "ElementDescriptor",
      "DisplayName"
    });
    List<PX.Api.Models.Action> list = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) info.Actions).Select<PXSiteMap.ScreenInfo.Action, PX.Api.Models.Action>((Func<PXSiteMap.ScreenInfo.Action, PX.Api.Models.Action>) (a => ScreenUtils.GetAction(a, info.PrimaryView, appendDescriptors))).ToList<PX.Api.Models.Action>();
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      int startIndex;
      if (list[index1].FieldName != null && (startIndex = list[index1].FieldName.IndexOf('@') + 1) > 0 && startIndex < list[index1].FieldName.Length)
      {
        string b = list[index1].FieldName.Substring(startIndex);
        int index2 = 0;
        while (index2 < list.Count)
        {
          if (string.Equals(list[index2].FieldName, b, StringComparison.OrdinalIgnoreCase))
          {
            list.RemoveAt(index2);
            if (index2 <= index1)
              --index1;
          }
          else
            ++index2;
        }
      }
    }
    return new Content()
    {
      Actions = list.ToArray(),
      PrimaryView = info.PrimaryView,
      Containers = info.Containers.Values.Select<PXViewDescription, Container>((Func<PXViewDescription, Container>) (c => ScreenUtils.GetContainer(c, prohibited, appendDescriptors, includeHiddenFields))).ToArray<Container>(),
      ContainerSearchStrategy = containerSearchStrategy
    };
  }

  internal static void AddServiceCommands(
    string screenID,
    IReadOnlyList<PX.Api.Models.Field> commands,
    PXSiteMap.ScreenInfo info)
  {
    bool flag = false;
    for (int index1 = 0; index1 < commands.Count; ++index1)
    {
      PX.Api.Models.Field command1 = commands[index1];
      if (!flag && (index1 == commands.Count - 1 && command1.FieldName != "NoteText" || index1 == commands.Count - 2 && commands[commands.Count - 1].FieldName == "NoteText"))
        command1.Commit = true;
      else if (command1.Commit && command1.FieldName != null && string.Equals(command1.FieldName, "BranchID", StringComparison.OrdinalIgnoreCase))
        command1.Commit = false;
      if (command1.Commit || command1.FieldName != null && command1.FieldName.StartsWith("//"))
      {
        Command command2 = (Command) command1;
        while (command2.LinkedCommand != null)
          command2 = command2.LinkedCommand;
        PX.Data.Description.FieldInfo fieldInfo = info.Containers[command1.ObjectName][command1.FieldName];
        if (fieldInfo != null && fieldInfo.Callback != null && !string.IsNullOrEmpty(fieldInfo.Callback.dsCommandName))
        {
          Command command3 = command2;
          PX.Api.Models.Action action = new PX.Api.Models.Action();
          action.ObjectName = info.PrimaryView;
          action.FieldName = fieldInfo.Callback.dsCommandName;
          command3.LinkedCommand = (Command) action;
          command2 = command2.LinkedCommand;
        }
        string str1 = info.Containers[command1.ObjectName].ViewName;
        if (!string.IsNullOrEmpty(str1))
        {
          int length = str1.IndexOf(": ");
          if (length != -1)
            str1 = str1.Substring(0, length);
        }
        if (info.Containers[command1.ObjectName].Parameters != null)
        {
          for (int index2 = info.Containers[command1.ObjectName].Parameters.Length - 1; index2 >= 0; --index2)
          {
            ParsInfo parameter1 = info.Containers[command1.ObjectName].Parameters[index2];
            if (parameter1.Type == ParType.Parameters)
            {
              Command command4 = command2;
              Parameter parameter2 = new Parameter();
              parameter2.ObjectName = command1.ObjectName;
              parameter2.FieldName = parameter1.Name;
              Parameter parameter3 = parameter2;
              string str2;
              if (parameter1.Field == null || !parameter1.Field.Contains<char>('.'))
                str2 = $"=[{str1}.{parameter1.Field}]";
              else
                str2 = $"=[{parameter1.Field}]";
              parameter3.Value = str2;
              Parameter parameter4 = parameter2;
              command4.LinkedCommand = (Command) parameter4;
            }
            else if (parameter1.Type == ParType.Searches)
            {
              Command command5 = command2;
              Key key1 = new Key();
              key1.ObjectName = command1.ObjectName;
              key1.FieldName = parameter1.Name;
              Key key2 = key1;
              string str3;
              if (parameter1.Field == null || !parameter1.Field.Contains<char>('.'))
                str3 = $"=[{str1}.{parameter1.Field}]";
              else
                str3 = $"=[{parameter1.Field}]";
              key2.Value = str3;
              Key key3 = key1;
              command5.LinkedCommand = (Command) key3;
            }
            else
              continue;
            command2 = command2.LinkedCommand;
          }
        }
        if (ScreenUtils._isReport && string.Equals(command1.ObjectName, ScreenUtils._filters) && (string.Equals(command1.FieldName, ScreenUtils._condition) || string.Equals(command1.FieldName, ScreenUtils._dataField)))
        {
          if (string.Equals(command1.FieldName, ScreenUtils._dataField))
          {
            Command command6 = command2;
            NewRow newRow = new NewRow();
            newRow.ObjectName = info.Containers[command1.ObjectName].ViewName;
            command6.LinkedCommand = (Command) newRow;
          }
        }
        else if (!flag && info.Containers[command1.ObjectName].HasLineNumber && !info.Containers[command1.ObjectName].HasSearchesByKey)
        {
          Command command7 = command2;
          NewRow newRow = new NewRow();
          newRow.ObjectName = info.Containers[command1.ObjectName].ViewName;
          command7.LinkedCommand = (Command) newRow;
        }
        flag = true;
      }
    }
  }

  internal static string ExtractMessage(Exception e)
  {
    return e is PXOuterException exception ? exception.GetFullMessage(" ") : e.Message;
  }

  public static string ExtractId(string id)
  {
    if (id.Contains("$"))
      id = id.Split('$')[0];
    if (id.Contains("_"))
      id = id.Split('_')[0];
    return id;
  }

  public static string ExtractId(
    string id,
    out string expandSelectorValue,
    out Guid? folderFilterId)
  {
    folderFilterId = new Guid?();
    expandSelectorValue = (string) null;
    if (id.Contains("$"))
    {
      string[] strArray = id.Split('$');
      id = strArray[0];
      folderFilterId = new Guid?(Guid.Parse(strArray[1]));
    }
    if (id.Contains("_"))
    {
      string[] strArray = id.Split('_');
      id = strArray[0];
      expandSelectorValue = strArray[1];
    }
    return id;
  }

  private static IEnumerable<Command> EnumFieldCommands(Command cmd)
  {
    List<Command> commandList = new List<Command>();
    commandList.Add(cmd);
    while (commandList[0].LinkedCommand != null)
      commandList.Insert(0, commandList[0].LinkedCommand);
    for (int index = commandList.Count - 1; index > 0; --index)
    {
      if (commandList[index] is PX.Api.Models.Value && string.IsNullOrEmpty(commandList[index].ObjectName) && (string.IsNullOrEmpty(commandList[index].FieldName) || commandList[index].LinkedCommand is Attachment) && commandList[index].LinkedCommand == commandList[index - 1])
      {
        if (string.IsNullOrEmpty(commandList[index].FieldName))
        {
          commandList[index - 1].Value = commandList[index].Value;
        }
        else
        {
          commandList[index - 1].FieldName = commandList[index].FieldName;
          if (commandList[index].Value != null)
            commandList[index - 1].Value = commandList[index].Value;
        }
        if (commandList[index].Commit)
          commandList[index - 1].Commit = true;
        if (commandList[index].IgnoreError)
          commandList[index - 1].IgnoreError = true;
        commandList.RemoveAt(index);
      }
      else
        commandList[index].LinkedCommand = (Command) null;
    }
    return (IEnumerable<Command>) commandList;
  }

  private static SYMappingField ConvertCommand(Command cmd)
  {
    string str1 = cmd.FieldName;
    string str2 = cmd.Value;
    switch (cmd)
    {
      case Key _:
        str1 = "@@" + str1;
        break;
      case Parameter _:
        str1 = "@" + str1;
        break;
      case PX.Api.Models.Action _:
        str1 = $"<{str1}>";
        break;
      case NewRow _:
        str1 = "##";
        str2 = "=-1";
        break;
      case DeleteRow _:
        str1 = "##";
        str2 = "=-2";
        break;
      case RowNumber _:
        str1 = "##";
        break;
      case EveryValue _:
        str2 = "=Every";
        break;
      case Answer _:
        str1 = "??";
        break;
      case Attachment _:
        str1 = "&" + str1;
        break;
    }
    return new SYMappingField()
    {
      FieldName = str1,
      ObjectName = cmd.ObjectName,
      IsActive = new bool?(true),
      NeedCommit = new bool?(cmd.Commit),
      IgnoreError = new bool?(cmd.IgnoreError),
      Value = str2,
      UseCurrent = cmd.UseCurrent
    };
  }

  public static SYMapping GetMapping(string screenId)
  {
    return ScreenUtils.GetMapping(ScreenUtils.ScreenInfo.Get(screenId), screenId);
  }

  private static (PXSiteMap.ScreenInfo screenInfo, string errorMessage) GetScreenInfo(
    string screenId)
  {
    return (ScreenUtils.ScreenInfo.Get(screenId), (string) null);
  }

  internal static SYMapping GetMapping(PXSiteMap.ScreenInfo screenInfo, string screenId)
  {
    return new SYMapping()
    {
      GraphName = screenInfo.GraphName,
      SyncType = "F",
      ViewName = screenInfo.PrimaryView,
      ScreenID = screenId
    };
  }

  public static PXFilterRow ConvertFilter(PX.Api.Models.Filter f, PXGraph graph)
  {
    if (f.ParentField == null)
      return new PXFilterRow()
      {
        DataField = f.Field.FieldName,
        CloseBrackets = f.CloseBrackets,
        Condition = (PXCondition) f.Condition,
        OpenBrackets = f.OpenBrackets,
        Value2 = ScreenUtils.ConvertIfDateTime(f.Value2, f.Field, graph),
        Value = ScreenUtils.ConvertIfDateTime(f.Value, f.Field, graph),
        OrOperator = f.Operator == FilterOperator.Or
      };
    return new PXFilterRow()
    {
      DataField = f.ParentField.FieldName,
      OpenBrackets = f.OpenBrackets,
      CloseBrackets = f.CloseBrackets,
      Condition = PXCondition.NestedSelector,
      Value = (object) f.Field.ObjectName,
      Value2 = (object) new PXFilterRow()
      {
        OpenBrackets = f.OpenBrackets,
        CloseBrackets = f.CloseBrackets,
        DataField = f.Field.FieldName,
        Value = ScreenUtils.ConvertIfDateTime(f.Value, f.Field, graph),
        Value2 = ScreenUtils.ConvertIfDateTime(f.Value2, f.Field, graph),
        Condition = (PXCondition) f.Condition,
        OrOperator = false
      },
      OrOperator = f.Operator == FilterOperator.Or
    };
  }

  private static object ConvertIfDateTime(object p, PX.Api.Models.Field field, PXGraph graph)
  {
    string s = p as string;
    if (!string.IsNullOrEmpty(s))
    {
      string key = field.ObjectName ?? graph.PrimaryView;
      if (graph.Views[key].Cache.Fields.Contains(field.FieldName))
      {
        System.Type fieldType = graph.Views[key].Cache.GetFieldType(field.FieldName);
        System.DateTime result;
        if ((fieldType == typeof (System.DateTime?) || fieldType == typeof (System.DateTime)) && System.DateTime.TryParse(s, (IFormatProvider) graph.Culture.DateTimeFormat, DateTimeStyles.None, out result))
          return (object) result;
      }
    }
    return p;
  }

  public static string CSname(string str)
  {
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = true;
    if (str == null)
      return string.Empty;
    string str1;
    if (ScreenUtils._specialNameSubstitutionDict.TryGetValue(str, out str1))
      return str1;
    foreach (char c in str)
    {
      if (!char.IsLetterOrDigit(c))
      {
        flag = true;
      }
      else
      {
        stringBuilder.Append(flag ? char.ToUpper(c) : c);
        flag = false;
      }
    }
    return stringBuilder.ToString();
  }

  private static PX.Api.Models.Action GetAction(
    PXSiteMap.ScreenInfo.Action a,
    string primaryView,
    bool appendDescriptors)
  {
    PX.Api.Models.Action action1 = new PX.Api.Models.Action();
    action1.Name = ScreenUtils.CSname(a.Name);
    action1.FieldName = a.Name;
    action1.ObjectName = primaryView;
    action1.ViewTypeName = a.ViewTypeName;
    PX.Api.Models.Action action2 = action1;
    if (appendDescriptors)
      action2.Descriptor = new ElementDescriptor()
      {
        ElementType = ElementTypes.Action,
        DisplayName = a.DisplayName,
        IsDisabled = !a.Enabled,
        ButtonType = a.ButtonType,
        DependsOn = a.DependsOn,
        StateColumn = a.StateColumn
      };
    return action2;
  }

  internal static string GetContainerDisplayName(string viewDisplayName)
  {
    return !viewDisplayName.Contains(" -> ") ? viewDisplayName : viewDisplayName.Substring(viewDisplayName.LastIndexOf(" -> ") + " -> ".Length);
  }

  public static string NormalizeViewName(string viewname)
  {
    int? nullable1 = viewname?.IndexOf(':');
    int? nullable2 = nullable1;
    int num = 0;
    return !(nullable2.GetValueOrDefault() >= num & nullable2.HasValue) ? viewname : viewname.Substring(0, nullable1.Value);
  }

  private static Container GetContainer(
    PXViewDescription viewDescription,
    HashSet<string> prohibited,
    bool appendDescriptors,
    bool includeHiddenFields)
  {
    HashSet<string> existing = new HashSet<string>((IEnumerable<string>) new string[2]
    {
      "ServiceCommands",
      "DisplayName"
    });
    PX.Data.Description.FieldInfo[] source = includeHiddenFields ? viewDescription.AllFields : viewDescription.Fields;
    ParsInfo[] parsInfoArray = includeHiddenFields ? viewDescription.AllParameters : viewDescription.Parameters;
    Container container = new Container()
    {
      Name = ScreenUtils.CSname(string.IsNullOrEmpty(viewDescription.DisplayName) || string.IsNullOrEmpty(ScreenUtils.CSname(viewDescription.DisplayName)) ? (viewDescription.ViewName == null || !viewDescription.ViewName.Contains<char>(':') ? viewDescription.ViewName : viewDescription.ViewName.Substring(0, viewDescription.ViewName.IndexOf(':'))) : viewDescription.DisplayName),
      Fields = ((IEnumerable<PX.Data.Description.FieldInfo>) source).Select<PX.Data.Description.FieldInfo, PX.Api.Models.Field>((Func<PX.Data.Description.FieldInfo, PX.Api.Models.Field>) (f => ScreenUtils.GetField(f, viewDescription, existing, appendDescriptors))).ToArray<PX.Api.Models.Field>(),
      ViewDescription = viewDescription,
      DisplayName = ScreenUtils.GetContainerDisplayName(viewDescription.DisplayName)
    };
    if (container.Name.Length > 0 && char.IsDigit(container.Name, 0))
      container.Name = "_" + container.Name;
    while (prohibited.Contains(container.Name))
      container.Name += "_";
    prohibited.Add(container.Name);
    List<Command> commandList1 = new List<Command>();
    HashSet<string> stringSet = viewDescription.AppendEveryField ? new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : (HashSet<string>) null;
    if (parsInfoArray != null && parsInfoArray.Length != 0)
    {
      string str1 = viewDescription.ViewName;
      if (!string.IsNullOrEmpty(str1))
      {
        int length = str1.IndexOf(": ");
        if (length != -1)
          str1 = str1.Substring(0, length);
      }
      foreach (ParsInfo parsInfo in parsInfoArray)
      {
        if (parsInfo.Type == ParType.Searches)
        {
          string str2 = parsInfo.Name;
          bool flag = false;
          foreach (PX.Data.Description.FieldInfo fieldInfo in source)
          {
            if (fieldInfo.FieldName == parsInfo.Field)
            {
              str2 = fieldInfo.DisplayName;
              flag = fieldInfo.AllowedLabels != null;
              break;
            }
          }
          List<Command> commandList2 = commandList1;
          Key key1 = new Key();
          key1.Name = ScreenUtils.CSname("Key" + str2);
          key1.ObjectName = viewDescription.ViewName;
          key1.FieldName = parsInfo.Name;
          key1.Value = $"=[{str1}.{parsInfo.Field}]";
          Key key2 = key1;
          commandList2.Add((Command) key2);
          if (flag && !viewDescription.HasLineNumber)
          {
            List<Command> commandList3 = commandList1;
            EveryValue everyValue = new EveryValue();
            everyValue.Name = ScreenUtils.CSname("Every" + str2);
            everyValue.Commit = true;
            everyValue.ObjectName = viewDescription.ViewName;
            everyValue.FieldName = parsInfo.Name;
            commandList3.Add((Command) everyValue);
            if (viewDescription.AppendEveryField)
              stringSet.Add(parsInfo.Name);
          }
        }
        else if (parsInfo.Type == ParType.Parameters)
        {
          List<Command> commandList4 = commandList1;
          Parameter parameter = new Parameter();
          parameter.Name = ScreenUtils.CSname("Parameter" + parsInfo.Name);
          parameter.ObjectName = viewDescription.ViewName;
          parameter.FieldName = parsInfo.Name;
          parameter.Value = $"=[{(parsInfo.Field == null || !parsInfo.Field.Contains<char>('.') ? $"{str1}.{parsInfo.Field}" : parsInfo.Field)}]";
          commandList4.Add((Command) parameter);
        }
        else
        {
          List<Command> commandList5 = commandList1;
          PX.Api.Models.Field field = new PX.Api.Models.Field();
          field.Name = ScreenUtils.CSname("Filter" + parsInfo.Name);
          field.ObjectName = viewDescription.ViewName;
          field.FieldName = parsInfo.Field;
          commandList5.Add((Command) field);
        }
      }
    }
    if (viewDescription.AppendEveryField)
    {
      foreach (PX.Data.Description.FieldInfo fieldInfo in source)
      {
        if (fieldInfo.AllowedLabels != null && !stringSet.Contains(fieldInfo.FieldName))
        {
          List<Command> commandList6 = commandList1;
          EveryValue everyValue = new EveryValue();
          everyValue.Name = ScreenUtils.CSname("Every" + (!string.IsNullOrEmpty(fieldInfo.DisplayName) ? fieldInfo.DisplayName : fieldInfo.FieldName));
          everyValue.Commit = true;
          everyValue.ObjectName = viewDescription.ViewName;
          everyValue.FieldName = fieldInfo.FieldName;
          commandList6.Add((Command) everyValue);
          stringSet.Add(fieldInfo.FieldName);
        }
      }
    }
    if (viewDescription.HasLineNumber)
    {
      List<Command> commandList7 = commandList1;
      NewRow newRow = new NewRow();
      newRow.Name = ScreenUtils.CSname("NewRow");
      newRow.ObjectName = viewDescription.ViewName;
      commandList7.Add((Command) newRow);
      List<Command> commandList8 = commandList1;
      RowNumber rowNumber = new RowNumber();
      rowNumber.Name = ScreenUtils.CSname("RowNumber");
      rowNumber.ObjectName = viewDescription.ViewName;
      rowNumber.Value = "LineNbr";
      commandList8.Add((Command) rowNumber);
    }
    List<Command> commandList9 = commandList1;
    DeleteRow deleteRow = new DeleteRow();
    deleteRow.Name = ScreenUtils.CSname("DeleteRow");
    deleteRow.ObjectName = viewDescription.ViewName;
    deleteRow.Commit = true;
    commandList9.Add((Command) deleteRow);
    List<Command> commandList10 = commandList1;
    Answer answer = new Answer();
    answer.Name = ScreenUtils.CSname("DialogAnswer");
    answer.ObjectName = viewDescription.ViewName;
    answer.Value = "='Yes'";
    commandList10.Add((Command) answer);
    if (viewDescription.HasFileIndicator)
    {
      List<Command> commandList11 = commandList1;
      Attachment attachment = new Attachment();
      attachment.Name = ScreenUtils.CSname("Attachment");
      attachment.ObjectName = viewDescription.ViewName;
      attachment.Value = "Attachment";
      commandList11.Add((Command) attachment);
    }
    container.ServiceCommands = commandList1.ToArray();
    return container;
  }

  public static PX.Api.Models.Field GetField(
    string displayName,
    string viewName,
    string fieldName,
    bool commit,
    HashSet<string> existing)
  {
    PX.Api.Models.Field field1 = new PX.Api.Models.Field();
    field1.Name = ScreenUtils.CSname(!string.IsNullOrEmpty(displayName) ? displayName : fieldName);
    field1.ObjectName = viewName;
    field1.FieldName = fieldName;
    field1.Commit = commit;
    field1.Value = ScreenUtils.CSname(displayName);
    PX.Api.Models.Field field2 = field1;
    if (field2.Name.Length > 0 && char.IsDigit(field2.Name, 0))
      field2.Name = "_" + field2.Name;
    for (; existing.Contains(field2.Name); field2.Name += "_")
    {
      if (!string.IsNullOrEmpty(field2.Value))
        field2.Value += "_";
    }
    existing.Add(field2.Name);
    return field2;
  }

  private static PX.Api.Models.Field GetField(
    PX.Data.Description.FieldInfo fieldInfo,
    PXViewDescription viewDescription,
    HashSet<string> existing,
    bool appendDescriptors)
  {
    PX.Api.Models.Field field1 = ScreenUtils.GetField(fieldInfo.DisplayName, viewDescription.ViewName, fieldInfo.FieldName, fieldInfo.Callback != null, existing);
    if (appendDescriptors)
    {
      PX.Api.Models.Field field2 = field1;
      ElementDescriptor elementDescriptor1 = new ElementDescriptor();
      elementDescriptor1.ElementType = fieldInfo.IsSelector ? (!string.IsNullOrWhiteSpace(fieldInfo.TextField) ? ElementTypes.ExplicitSelector : ElementTypes.StringSelector) : (fieldInfo.AllowedLabels != null || fieldInfo.FieldType == typeof (bool) ? ElementTypes.Option : (fieldInfo.FieldType == typeof (string) ? (fieldInfo.IsUnicode ? ElementTypes.String : ElementTypes.AsciiString) : (fieldInfo.FieldType == typeof (Guid?) ? ElementTypes.String : (fieldInfo.FieldType == typeof (System.DateTime) ? ElementTypes.Calendar : (fieldInfo.FieldType == typeof (object) ? ElementTypes.String : ElementTypes.Number)))));
      elementDescriptor1.DisplayName = fieldInfo.DisplayName;
      elementDescriptor1.LengthLimit = fieldInfo.Length > 0 ? fieldInfo.Length : 0;
      elementDescriptor1.IsDisabled = !fieldInfo.IsEnabled;
      elementDescriptor1.IsRequired = fieldInfo.IsRequired;
      elementDescriptor1.InputMask = fieldInfo.FieldType == typeof (Decimal) || fieldInfo.FieldType == typeof (double) || fieldInfo.FieldType == typeof (float) ? (fieldInfo.Precision > 0 ? "." + new string('9', fieldInfo.Precision) : ".") : fieldInfo.InputMask;
      elementDescriptor1.DisplayRules = fieldInfo.DisplayMask;
      ElementDescriptor elementDescriptor2 = elementDescriptor1;
      string[] strArray;
      if (fieldInfo.AllowedLabels == null || fieldInfo.IsSelector)
      {
        if (!(fieldInfo.FieldType == typeof (bool)))
        {
          if (fieldInfo.MinValue == null || fieldInfo.MaxValue == null)
          {
            if (fieldInfo.MinValue == null)
            {
              if (fieldInfo.MaxValue == null)
                strArray = (string[]) null;
              else
                strArray = new string[2]
                {
                  null,
                  fieldInfo.MaxValue.ToString()
                };
            }
            else
              strArray = new string[2]
              {
                fieldInfo.MinValue.ToString(),
                null
              };
          }
          else
            strArray = new string[3]
            {
              fieldInfo.MinValue.ToString(),
              null,
              fieldInfo.MaxValue.ToString()
            };
        }
        else
          strArray = new string[2]{ "True", "False" };
      }
      else
        strArray = fieldInfo.AllowedLabels;
      elementDescriptor2.AllowedValues = strArray;
      elementDescriptor1.LinkCommand = fieldInfo.LinkCommand;
      elementDescriptor1.IsTimeList = fieldInfo.IsTimeList;
      elementDescriptor1.PreserveTime = fieldInfo.PreserveTime;
      elementDescriptor1.AutoPostback = fieldInfo.IsCommit;
      elementDescriptor1.FieldType = ScreenUtils.ConvertFieldType(fieldInfo.FieldType);
      ElementDescriptor elementDescriptor3 = elementDescriptor1;
      field2.Descriptor = elementDescriptor3;
      if (fieldInfo.SelectorViewDescription != null)
        field1.Descriptor.Container = ScreenUtils.GetContainer(fieldInfo.SelectorViewDescription, new HashSet<string>(), true, true);
    }
    return field1;
  }

  [PXInternalUseOnly]
  public static FieldTypes ConvertFieldType(System.Type fieldType)
  {
    System.Type type = fieldType;
    if (type == typeof (string))
      return FieldTypes.String;
    if (type == typeof (Decimal) || type == typeof (Decimal?))
      return FieldTypes.Decimal;
    if (type == typeof (System.DateTime) || type == typeof (System.DateTime?))
      return FieldTypes.DateTime;
    if (type == typeof (bool) || type == typeof (bool?))
      return FieldTypes.Boolean;
    if (type == typeof (int) || type == typeof (int?))
      return FieldTypes.Int;
    if (type == typeof (long) || type == typeof (long?))
      return FieldTypes.Long;
    if (type == typeof (short) || type == typeof (short?))
      return FieldTypes.Short;
    if (type == typeof (byte) || type == typeof (byte?))
      return FieldTypes.Byte;
    if (type == typeof (double) || type == typeof (double?))
      return FieldTypes.Double;
    if (type == typeof (Guid) || type == typeof (Guid?))
      return FieldTypes.Guid;
    if (type == typeof (float) || type == typeof (float?))
      return FieldTypes.Float;
    if (type == typeof (float) || type == typeof (float?))
      return FieldTypes.Single;
    return type == typeof (byte[]) ? FieldTypes.Text : FieldTypes.Unknown;
  }

  internal static string GetPrefix()
  {
    string prefix = string.Empty;
    if (HttpContext.Current != null && HttpContext.Current.User.Identity is FormsIdentity identity)
      prefix = identity.GetPrefix();
    return prefix;
  }

  [PXInternalUseOnly]
  public static PXLoginScope EnsureLogin(string prefix = "")
  {
    PXLoginScope pxLoginScope = (PXLoginScope) null;
    if (string.IsNullOrEmpty(prefix))
      prefix = ScreenUtils.GetPrefix();
    if (!ServiceLocator.IsLocationProviderSet || !ServiceLocator.Current.GetInstance<ILoginService>().IsUserAuthenticated(false, prefix))
    {
      string userName = "admin";
      if (PXDatabase.Companies.Length != 0)
      {
        string str = PXAccess.GetCompanyName();
        if (string.IsNullOrEmpty(str))
          str = PXDatabase.Companies[0];
        userName = $"{userName}@{str}";
      }
      pxLoginScope = new PXLoginScope(userName, PXAccess.GetAdministratorRoles());
    }
    return pxLoginScope;
  }

  public static Command ConvertCommand(SYMappingField cmd)
  {
    SyCommand command = SyImportContext.ParseCommand(cmd);
    switch (command.CommandType)
    {
      case SyCommandType.Field:
        PX.Api.Models.Field field1 = new PX.Api.Models.Field();
        field1.ObjectName = command.View;
        field1.FieldName = command.Field;
        field1.Commit = command.Commit;
        field1.IgnoreError = command.IgnoreError;
        field1.Value = cmd.Value;
        return (Command) field1;
      case SyCommandType.Parameter:
        Parameter parameter = new Parameter();
        parameter.ObjectName = command.View;
        parameter.FieldName = command.Field;
        parameter.Value = cmd.Value;
        return (Command) parameter;
      case SyCommandType.Search:
        Key key = new Key();
        key.ObjectName = command.View;
        key.FieldName = command.Field;
        key.Value = cmd.Value;
        return (Command) key;
      case SyCommandType.Action:
        PX.Api.Models.Action action = new PX.Api.Models.Action();
        action.ObjectName = command.View;
        action.FieldName = command.Field;
        action.Commit = command.Commit;
        action.IgnoreError = command.IgnoreError;
        return (Command) action;
      case SyCommandType.NewRow:
        NewRow newRow = new NewRow();
        newRow.ObjectName = command.View;
        return (Command) newRow;
      case SyCommandType.DeleteRow:
        DeleteRow deleteRow = new DeleteRow();
        deleteRow.ObjectName = command.View;
        deleteRow.Commit = command.Commit;
        return (Command) deleteRow;
      case SyCommandType.RowNumber:
        RowNumber rowNumber = new RowNumber();
        rowNumber.ObjectName = command.View;
        rowNumber.Value = cmd.Value;
        return (Command) rowNumber;
      case SyCommandType.EnumFieldValues:
        EveryValue everyValue = new EveryValue();
        everyValue.ObjectName = command.View;
        everyValue.FieldName = command.Field;
        everyValue.Commit = command.Commit;
        everyValue.IgnoreError = command.IgnoreError;
        return (Command) everyValue;
      case SyCommandType.ExportField:
      case SyCommandType.ExportPath:
        PX.Api.Models.Field field2 = new PX.Api.Models.Field();
        field2.ObjectName = command.View;
        field2.FieldName = command.Field;
        field2.Value = cmd.Value;
        return (Command) field2;
      default:
        Answer answer = new Answer();
        answer.ObjectName = command.View;
        answer.Value = cmd.Value;
        return (Command) answer;
    }
  }

  internal static string[][] ExportInternal(
    string screenId,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    int startRow,
    int topCount,
    bool includeHeaders,
    bool breakOnError,
    PXGraph graph,
    bool bindGuids = false,
    bool mobile = false,
    bool isSelector = false,
    string forcePrimaryView = null,
    string bindContainer = "undefined",
    Dictionary<string, KeyValuePair<string, bool>[]> sorts = null,
    string guidViewName = "undefined",
    OptimizedExportProviderBuilderForScreenBasedApi buildOptimizedExportProviderDelegate = null,
    Func<PXSYRow, IList<string>, string[]> serializationDelegate = null)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ScreenUtils.ExportInternal(screenId, ScreenUtils.\u003C\u003EO.\u003C0\u003E__GetScreenInfo ?? (ScreenUtils.\u003C\u003EO.\u003C0\u003E__GetScreenInfo = new Func<string, (PXSiteMap.ScreenInfo, string)>(ScreenUtils.GetScreenInfo)), commands, filters, startRow, topCount, includeHeaders, breakOnError, graph, bindGuids, mobile, isSelector, forcePrimaryView, bindContainer, sorts, guidViewName, buildOptimizedExportProviderDelegate, serializationDelegate);
  }

  internal static string[][] ExportInternal(
    string screenId,
    Func<string, (PXSiteMap.ScreenInfo screenInfo, string errorMessage)> getScreenInfoFunc,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    int startRow,
    int topCount,
    bool includeHeaders,
    bool breakOnError,
    PXGraph graph,
    bool bindGuids = false,
    bool mobile = false,
    bool isSelector = false,
    string forcePrimaryView = null,
    string bindContainer = "undefined",
    Dictionary<string, KeyValuePair<string, bool>[]> sorts = null,
    string guidViewName = "undefined",
    OptimizedExportProviderBuilderForScreenBasedApi buildOptimizedExportProviderDelegate = null,
    Func<PXSYRow, IList<string>, string[]> serializationDelegate = null)
  {
    using (new ScreenUtils.LocaleScope())
    {
      PXSiteMap.ScreenInfo screenInfo = (PXSiteMap.ScreenInfo) null;
      try
      {
        string str1;
        (screenInfo, str1) = getScreenInfoFunc(screenId);
        SYMapping mapping = ScreenUtils.GetMapping(screenInfo, screenId);
        if (!string.IsNullOrEmpty(forcePrimaryView))
          mapping.ViewName = StringExtensions.FirstSegment(forcePrimaryView, ':');
        try
        {
          foreach (Command command in commands)
          {
            if (command is PX.Api.Models.Value)
            {
              if (command.Value != null && !command.Value.StartsWith("="))
                command.Value = !(command.Value == "") ? (!command.Value.StartsWith("'") || !command.Value.EndsWith("'") || command.Value.Length <= 1 ? $"='{command.Value.Replace("'", "''")}'" : "=" + command.Value) : (string) null;
            }
            else if (command is PX.Api.Models.Field && command.Value == null)
              command.Value = command.FieldName;
            if (command.Value != null && !command.Value.StartsWith("=") && (!(command is PX.Api.Models.Value) || command.LinkedCommand == null))
            {
              if (command.FieldName == null || !command.FieldName.StartsWith("//"))
                command.LinkedCommand = (Command) null;
              command.Commit = false;
            }
          }
        }
        catch (Exception ex)
        {
          using (LogContext.PushProperty(nameof (commands), (object) new
          {
            commands = commands
          }, false))
            PXTrace.WriteError(ex, "#commands failed");
          throw;
        }
        List<Command> commandList1 = new List<Command>();
        try
        {
          commandList1.AddRange((IEnumerable<Command>) commands);
          commands = ((IEnumerable<Command>) commands).SelectMany<Command, Command, Command>((Func<Command, IEnumerable<Command>>) (field => ScreenUtils.EnumFieldCommands(field)), (Func<Command, Command, Command>) ((field, cmd) => cmd)).ToArray<Command>();
        }
        catch (Exception ex)
        {
          using (LogContext.PushProperty(nameof (commands), (object) new
          {
            temp_commands = commandList1
          }, false))
            PXTrace.WriteError(ex, "#EnumFieldCommands commands failed");
          throw;
        }
        Dictionary<string, string> dictionary1 = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        List<string> exportFields = new List<string>();
        for (int index = 0; index < commands.Length; ++index)
        {
          Command command = commands[index];
          if (command == null)
          {
            using (LogContext.PushProperty(nameof (commands), (object) new
            {
              temp_commands = commandList1
            }, false))
              PXTrace.WriteError("#commands contains nulls");
          }
          if (command.Value != null && !command.Value.StartsWith("="))
          {
            if (dictionary1.ContainsKey(command.Value))
            {
              string str2 = command.Value;
              command.Value = Guid.NewGuid().ToString();
              dictionary1[command.Value] = str2;
            }
            else
              dictionary1[command.Value] = command.Value;
            exportFields.Add(command.Value);
          }
        }
        if (mobile && !bindGuids && !guidViewName.OrdinalEquals("undefined") && !((IEnumerable<Command>) commands).Any<Command>((Func<Command, bool>) (c => c.Name.OrdinalEquals("NoteID"))))
        {
          List<Command> first = new List<Command>((IEnumerable<Command>) commands);
          Command[] second = new Command[1];
          PX.Api.Models.Field field = new PX.Api.Models.Field();
          field.Commit = false;
          field.FieldName = "NoteID";
          field.Name = "NoteID";
          field.Value = "NoteID";
          field.IgnoreError = false;
          field.ObjectName = guidViewName;
          second[0] = (Command) field;
          commands = first.Concat<Command>((IEnumerable<Command>) second).ToArray<Command>();
          exportFields.Add("NoteID");
          dictionary1["NoteID"] = "NoteID";
        }
        if (!string.IsNullOrEmpty(str1))
          PXTrace.WriteError("#GetScreenInfo returned errorMessage " + str1);
        HashSet<string> views = new HashSet<string>(((IEnumerable<Command>) commands).Select<Command, string>((Func<Command, string>) (c => c.ObjectName)).Distinct<string>(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        PXViewDescription[] array1 = screenInfo.Containers.Values.Where<PXViewDescription>((Func<PXViewDescription, bool>) (v => ((IEnumerable<Command>) commands).Any<Command>((Func<Command, bool>) (c => c.ObjectName.OrdinalEquals(v.ViewName))))).ToArray<PXViewDescription>();
        bool isForcedGraph = true;
        if (graph == null)
        {
          using (new PXPreserveScope())
          {
            graph = SyImportProcessor.CreateGraph(mapping.GraphName, screenId);
            if (graph == null)
              PXTrace.WriteError("#Graph load: graph is null GraphName = " + mapping.GraphName);
            graph.Load();
            graph.Clear();
          }
          isForcedGraph = false;
        }
        LinkedSelectorViews selectorViews = isSelector ? new LinkedSelectorViews() : ViewHelper.CollectLinkedSelectorViews(views, (IEnumerable<PXViewDescription>) array1, graph.PrimaryView);
        Command command1 = ((IEnumerable<Command>) commands).LastOrDefault<Command>((Func<Command, bool>) (c => c is Key && c.ObjectName.OrdinalEquals(mapping.ViewName)));
        Dictionary<string, PXFilterRow[]> dictionary2 = new Dictionary<string, PXFilterRow[]>();
        if (filters != null)
        {
          try
          {
            foreach (PX.Api.Models.Filter filter in filters)
            {
              PXFilterRow pxFilterRow = ScreenUtils.ConvertFilter(filter, graph);
              string key = mapping.ViewName;
              string str3 = filter.ParentField == null ? filter.Field.ObjectName : filter.ParentField.ObjectName;
              if (!string.IsNullOrEmpty(str3))
              {
                int length = str3.IndexOf(':');
                if (length != -1)
                {
                  if (length > 0)
                    key = str3.Substring(0, length);
                }
                else
                  key = str3;
              }
              PXFilterRow[] array2;
              if (!dictionary2.TryGetValue(key, out array2))
              {
                dictionary2[key] = new PXFilterRow[1]
                {
                  pxFilterRow
                };
              }
              else
              {
                Array.Resize<PXFilterRow>(ref array2, array2.Length + 1);
                array2[array2.Length - 1] = pxFilterRow;
                dictionary2[key] = array2;
              }
            }
          }
          catch (Exception ex)
          {
            object[] objArray = Array.Empty<object>();
            PXTrace.WriteError(ex, "#filters", objArray);
            throw;
          }
        }
        SYMappingField[] array3 = ((IEnumerable<Command>) commands).Select<Command, SYMappingField>((Func<Command, SYMappingField>) (cmd => ScreenUtils.ConvertCommand(cmd))).ToArray<SYMappingField>();
        SyExportContext context = new SyExportContext(mapping, (IEnumerable<SYMappingField>) array3, exportFields.ToArray(), dictionary2, breakOnError, startRow, topCount, selectorViews, command1?.FieldName);
        context.Sorts = sorts ?? new Dictionary<string, KeyValuePair<string, bool>[]>();
        context.Graph = graph;
        if (mobile)
          context.Locale = PXCultureInfo.InvariantCulture.Name;
        IEnumerable<PXSYRow> exportResult = ScreenUtils.GetExportResult(screenId, commands, startRow, topCount, bindGuids, mobile, sorts, buildOptimizedExportProviderDelegate, isForcedGraph, array1, screenInfo, views, dictionary2, context);
        List<string[]> strArrayList = new List<string[]>();
        Command[] array4 = ((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (c => c is Key || c is RowNumber)).ToArray<Command>();
        List<Command> commandList2 = new List<Command>();
        List<string> source = new List<string>();
        Dictionary<string, List<string>> keyColumnsByView = new Dictionary<string, List<string>>();
        foreach (Command command2 in array4)
        {
          Command key = command2;
          if (key.ObjectName == null)
            PXTrace.WriteError("#key.ObjectName is null " + key?.Name);
          if ((key.FieldName ?? key.Value) == null)
            PXTrace.WriteError("#key.FieldName and key.Value is null " + key?.Name);
          SyCommand syCommand = ((IEnumerable<SyCommand>) context.Commands).FirstOrDefault<SyCommand>((Func<SyCommand, bool>) (c => c.CommandType == SyCommandType.ExportField && (key.FieldName ?? key.Value).OrdinalEquals(c.Formula) && key.ObjectName.OrdinalEquals(c.View)));
          if (syCommand != null)
          {
            commandList2.Add(key);
            source.Add(syCommand.Field);
            if (!keyColumnsByView.ContainsKey(key.ObjectName))
              keyColumnsByView.Add(key.ObjectName, new List<string>());
            keyColumnsByView[key.ObjectName].Add(syCommand.Field);
          }
        }
        string[] keys = EntityToGuidBindService.FormatKeys(commandList2.ToArray());
        for (int index = 0; index < exportFields.Count; ++index)
          exportFields[index] = dictionary1[exportFields[index]];
        foreach (PXSYRow pxsyRow in exportResult)
        {
          PXSYRow row = pxsyRow;
          if (row == null)
          {
            using (LogContext.PushProperty("exportResult", (object) new
            {
              exportResult = exportResult
            }, false))
              PXTrace.WriteError("row is null");
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if ((!row.All<string>(ScreenUtils.\u003C\u003EO.\u003C1\u003E__IsNullOrEmpty ?? (ScreenUtils.\u003C\u003EO.\u003C1\u003E__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty))) || !source.Any<string>()) && !ScreenUtils.FilterTrashRecord(keyColumnsByView, row))
          {
            if (mobile && serializationDelegate == null)
            {
              PXTrace.WriteError("serilization delegate is null");
              throw new PXException("Serilization delegate was not set. ScreenID:" + screenId);
            }
            string[] resultRow = mobile ? serializationDelegate(row, (IList<string>) exportFields) : ((IEnumerable<string>) row.ToArray()).Take<string>(commands.Length).ToArray<string>();
            if (resultRow != null)
            {
              string[] values = mobile ? source.Distinct<string>().Select<string, string>((Func<string, string>) (c =>
              {
                PXSYItem pxsyItem = row.GetItem(c);
                return pxsyItem.NativeValue is System.DateTime ? resultRow[exportFields.FindIndex((Predicate<string>) (_ => _ == c))] : pxsyItem.Value;
              })).ToArray<string>() : source.Distinct<string>().Select<string, string>((Func<string, string>) (c => row[c])).ToArray<string>();
              if (bindGuids)
              {
                Guid guid = EntityToGuidBindService.Instance.GetGuid(graph.GetType().FullName, keys, values, bindContainer);
                strArrayList.Add(((IEnumerable<string>) new string[1]
                {
                  guid.ToString()
                }).Concat<string>((IEnumerable<string>) resultRow).ToArray<string>());
              }
              else
              {
                if (mobile)
                {
                  if (!string.IsNullOrEmpty(row["NoteID"]))
                    EntityToGuidBindService.Instance.BindGuid(Guid.Parse(row["NoteID"]), graph.GetType().FullName, keys, values, bindContainer);
                  else
                    continue;
                }
                strArrayList.Add(resultRow);
              }
            }
          }
        }
        string[][] array5 = strArrayList.ToArray();
        if (!includeHeaders)
          return array5;
        string[] array6;
        if (!bindGuids)
          array6 = exportFields.ToArray();
        else
          array6 = ((IEnumerable<string>) new string[1]
          {
            "GUID"
          }).Concat<string>((IEnumerable<string>) exportFields).ToArray<string>();
        return ((IEnumerable<string[]>) new string[1][]
        {
          array6
        }).Concat<string[]>((IEnumerable<string[]>) array5).ToArray<string[]>();
      }
      catch (Exception ex)
      {
        if (screenInfo == null)
          PXTrace.WriteError(ex, "#screenInfo is null");
        else if (screenInfo.Containers == null)
          PXTrace.WriteError(ex, "#screenInfo.Containers is null");
        else if (screenInfo.Containers.Any<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => c.Value == null)))
        {
          using (LogContext.PushProperty("ScreenInfo.Containers", (object) new
          {
            screenId = screenId,
            commands = commands,
            filters = filters,
            startRow = startRow,
            topCount = topCount,
            includeHeaders = includeHeaders,
            breakOnError = breakOnError,
            graph = graph,
            bindGuids = bindGuids,
            mobile = mobile,
            isSelector = isSelector,
            forcePrimaryView = forcePrimaryView,
            bindContainer = bindContainer,
            sorts = sorts,
            guidViewName = guidViewName,
            buildOptimizedExportProviderDelegate = buildOptimizedExportProviderDelegate,
            Values = screenInfo.Containers.Values
          }, false))
            PXTrace.WriteError(ex, "#screenInfo.Containers contains nulls");
        }
        using (LogContext.PushProperty("descriptors", (object) new
        {
          screenId = screenId,
          commands = commands,
          filters = filters,
          startRow = startRow,
          topCount = topCount,
          includeHeaders = includeHeaders,
          breakOnError = breakOnError,
          graph = graph,
          bindGuids = bindGuids,
          mobile = mobile,
          isSelector = isSelector,
          forcePrimaryView = forcePrimaryView,
          bindContainer = bindContainer,
          sorts = sorts,
          guidViewName = guidViewName,
          buildOptimizedExportProviderDelegate = buildOptimizedExportProviderDelegate
        }, false))
          PXTrace.WriteError(ex);
        throw;
      }
    }
  }

  private static IEnumerable<PXSYRow> GetExportResult(
    string screenId,
    Command[] commands,
    int startRow,
    int topCount,
    bool bindGuids,
    bool mobile,
    Dictionary<string, KeyValuePair<string, bool>[]> sorts,
    OptimizedExportProviderBuilderForScreenBasedApi buildOptimizedExportProvider,
    bool isForcedGraph,
    PXViewDescription[] containers,
    PXSiteMap.ScreenInfo screenInfo,
    HashSet<string> views,
    Dictionary<string, PXFilterRow[]> dict,
    SyExportContext context)
  {
    if (bindGuids | mobile)
      context.RepeatingData = SYMapping.RepeatingOption.All;
    if (buildOptimizedExportProvider != null)
    {
      if (!isForcedGraph | mobile)
      {
        try
        {
          Command[] commands1 = mobile ? ((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (c => c is PX.Api.Models.Field)).ToArray<Command>() : ((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (c => !(c is EveryValue))).ToArray<Command>();
          using (IOptimizedExportProvider optimizedExportProvider = buildOptimizedExportProvider(screenInfo, views, containers, commands1, screenId, dict, context.Locale, mobile))
          {
            if (optimizedExportProvider != null)
            {
              if (optimizedExportProvider.CanOptimize)
              {
                optimizedExportProvider.SetRepeatingOption(context.RepeatingData);
                return (IEnumerable<PXSYRow>) optimizedExportProvider.DoSelect((long) startRow, (long) topCount, sorts, context.AddTranslations, out PXSYTable _).ToArray<PXSYRow>();
              }
            }
          }
        }
        catch (Exception ex)
        {
          PXTrace.WriteWarning((Exception) new InvalidOperationException("Cannot optimize the data export.", ex));
        }
      }
    }
    using (PXLongOperation.SimulateOperation())
    {
      PXSYTablePr exportResult = SyImportProcessor.ExportTable(context);
      if (context.Error != null)
        throw PXException.PreserveStack(context.Error);
      return (IEnumerable<PXSYRow>) exportResult;
    }
  }

  private static bool FilterTrashRecord(
    Dictionary<string, List<string>> keyColumnsByView,
    PXSYRow row)
  {
    foreach (KeyValuePair<string, List<string>> keyValuePair in keyColumnsByView)
    {
      if (keyValuePair.Value.Any<string>())
      {
        string[] array = keyValuePair.Value.Select<string, string>((Func<string, string>) (c => row[c])).ToArray<string>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (((IEnumerable<string>) array).Any<string>() && ((IEnumerable<string>) array).All<string>(ScreenUtils.\u003C\u003EO.\u003C1\u003E__IsNullOrEmpty ?? (ScreenUtils.\u003C\u003EO.\u003C1\u003E__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty))))
          return true;
      }
    }
    return false;
  }

  internal static ImportResult[] Import(
    string screenId,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    string[][] data,
    bool includedHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget,
    PXGraph graph)
  {
    using (new ScreenUtils.LocaleScope())
    {
      SYMapping mapping = ScreenUtils.GetMapping(screenId);
      foreach (Command command in commands)
      {
        if (command is PX.Api.Models.Value && command.Value != null && !command.Value.StartsWith("="))
          command.Value = !(command.Value == "") ? (!command.Value.StartsWith("'") || !command.Value.EndsWith("'") || command.Value.Length <= 1 ? $"='{command.Value.Replace("'", "''")}'" : "=" + command.Value) : (string) null;
      }
      commands = ((IEnumerable<Command>) commands).SelectMany<Command, Command, Command>((Func<Command, IEnumerable<Command>>) (field => ScreenUtils.EnumFieldCommands(field)), (Func<Command, Command, Command>) ((field, cmd) => cmd)).ToArray<Command>();
      string[] strArray1 = !includedHeaders || data.Length == 0 ? ((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (cmd => cmd.Value != null && !cmd.Value.StartsWith("="))).Select<Command, string>((Func<Command, string>) (cmd => cmd.Value)).ToArray<string>() : data[0];
      HashSet<string> stringSet = new HashSet<string>();
      for (int index = 0; index < strArray1.Length; ++index)
      {
        string str = strArray1[index];
        if (stringSet.Contains(str))
        {
          bool flag = false;
          foreach (Command command in commands)
          {
            if (command.Value != null && (command.Value == str || command.Value.Contains($"[{str}]")))
            {
              if (flag)
              {
                strArray1[index] = Guid.NewGuid().ToString();
                if (command.Value == str)
                {
                  command.Value = strArray1[index];
                  break;
                }
                command.Value.Replace($"[{str}]", $"[{strArray1[index]}]");
                break;
              }
              flag = true;
            }
          }
        }
        else
          stringSet.Add(str);
      }
      SYMappingField[] array = ((IEnumerable<Command>) commands).Select<Command, SYMappingField>((Func<Command, SYMappingField>) (cmd => ScreenUtils.ConvertCommand(cmd))).ToArray<SYMappingField>();
      PXSYTableBuilder pxsyTableBuilder = new PXSYTableBuilder(strArray1);
      for (int index = includedHeaders ? 1 : 0; index < data.Length; ++index)
      {
        string[] strArray2 = data[index];
        pxsyTableBuilder.AddRow();
        foreach (IndexedValue<string> indexedValue in ((IEnumerable<string>) strArray1).EnumWithIndex<string>())
        {
          string v = strArray2[indexedValue.Index];
          pxsyTableBuilder.AddValue(indexedValue.Value, (object) v, (PXFieldState) null, (PXDBLocalizableStringAttribute.Translations) null);
        }
      }
      if (graph == null)
      {
        using (new PXPreserveScope())
        {
          graph = SyImportProcessor.CreateGraph(mapping.GraphName, screenId);
          graph.Load();
          graph.Clear();
        }
      }
      PXFilterRow[] filters1 = (PXFilterRow[]) null;
      if (filters != null)
        filters1 = ((IEnumerable<PX.Api.Models.Filter>) filters).Select<PX.Api.Models.Filter, PXFilterRow>((Func<PX.Api.Models.Filter, PXFilterRow>) (f => ScreenUtils.ConvertFilter(f, graph))).ToArray<PXFilterRow>();
      SyImportContext context = new SyImportContext(mapping, ((IEnumerable<SYMappingField>) array).ToArray<SYMappingField>(), (PXSYTable) pxsyTableBuilder.GetTable(), breakOnError, breakOnIncorrectTarget, SYValidation.None, filters1, StringComparer.OrdinalIgnoreCase);
      context.Graph = graph;
      using (PXLongOperation.SimulateOperation())
      {
        context.FillImportResultExternalKeys();
        SyImportProcessor.ImportTable(context);
      }
      List<Command> commandList = new List<Command>();
      Content schema = ServiceLocator.Current.GetInstance<IScreenService>().GetSchema(screenId, SchemaMode.Basic);
      if (schema.Containers.Length != 0 && schema.Containers[0].ServiceCommands != null && schema.Containers[0].ServiceCommands.Length != 0)
      {
        foreach (PX.Api.Models.Field field in schema.Containers[0].Fields)
        {
          foreach (Command serviceCommand in schema.Containers[0].ServiceCommands)
          {
            if (serviceCommand is Key && serviceCommand.FieldName == field.FieldName)
            {
              commandList.Add((Command) field);
              break;
            }
          }
        }
      }
      ImportResult[] importResultArray = new ImportResult[context.ImportResult.Length];
      for (int index1 = 0; index1 < context.ImportResult.Length; ++index1)
      {
        SyImportRowResult syImportRowResult = context.ImportResult[index1];
        importResultArray[index1] = new ImportResult()
        {
          Processed = syImportRowResult.IsFilled && syImportRowResult.Error == null && syImportRowResult.PersistingError == null,
          Error = syImportRowResult.GetErrorMessage(false, false)
        };
        if (importResultArray[index1].Processed && context.ImportResult[index1].PersistedRow != null && commandList.Count > 0)
        {
          importResultArray[index1].Keys = new PX.Api.Models.Value[commandList.Count];
          for (int index2 = 0; index2 < commandList.Count; ++index2)
          {
            object valueExt = context.Graph.GetValueExt(context.PrimaryView, context.ImportResult[index1].PersistedRow, commandList[index2].FieldName);
            if (valueExt is PXFieldState)
              valueExt = ((PXFieldState) valueExt).Value;
            PX.Api.Models.Value[] keys = importResultArray[index1].Keys;
            int index3 = index2;
            PX.Api.Models.Value obj = new PX.Api.Models.Value();
            obj.ObjectName = commandList[index2].ObjectName;
            obj.FieldName = commandList[index2].FieldName;
            obj.Name = commandList[index2].Name;
            obj.Commit = commandList[index2].Commit;
            obj.LinkedCommand = commandList[index2].LinkedCommand;
            obj.Value = valueExt?.ToString();
            keys[index3] = obj;
          }
        }
      }
      return importResultArray;
    }
  }

  internal static Content[] ReportSubmit(
    string screenId,
    PXSiteMapNode reportNode,
    SchemaMode schemaMode,
    IEnumerable<Command> commands)
  {
    string screenId1 = reportNode.ScreenID;
    Command[] array1 = commands.Where<Command>((Func<Command, bool>) (cmd => cmd.Value != null && cmd.Value.StartsWith("="))).ToArray<Command>();
    Command[] array2 = commands.Where<Command>((Func<Command, bool>) (cmd => cmd.Value != null && !cmd.Value.StartsWith("="))).ToArray<Command>();
    Command[] array3 = commands.Where<Command>((Func<Command, bool>) (cmd => cmd.Value == null || cmd.Value.StartsWith("="))).ToArray<Command>();
    IReportLoaderService instance1 = ServiceLocator.Current.GetInstance<IReportLoaderService>();
    Report report = instance1.LoadReportAndProcessParameters(screenId1);
    ScreenUtils.WebReportSettings savedReportSettings = ScreenUtils.GetSavedReportSettings(report, screenId1);
    ScreenUtils.Template savedTemplate = ScreenUtils.GetSavedTemplate(array1, screenId1);
    ICollection<Content> schema = ScreenUtils.GetSchema(screenId, schemaMode, savedReportSettings);
    Content content = schema.First<Content>();
    instance1.InitReportParameters(report, screenId1, savedReportSettings, (FilterExpCollection) null);
    ScreenUtils.ReportCommandsProcessChain commandsProcessChain1 = new ScreenUtils.ReportCommandsProcessChain((IEnumerable<ICommandProcessor>) new ICommandProcessor[3]
    {
      (ICommandProcessor) new ReportParameterAssigmentProcessor(report.Parameters),
      (ICommandProcessor) new ReportAssigmentProcessor(report),
      (ICommandProcessor) new PreProcessAssigmentProcessor()
    });
    IReportRunner instance2 = ServiceLocator.Current.GetInstance<IReportRunner>();
    IReportRenderer instance3 = ServiceLocator.Current.GetInstance<IReportRenderer>();
    ScreenUtils.ReportCommandsProcessChain commandsProcessChain2 = new ScreenUtils.ReportCommandsProcessChain((IEnumerable<ICommandProcessor>) new ICommandProcessor[3]
    {
      (ICommandProcessor) new ReportPdfResultExportProcessor(content, report, screenId1, instance2, instance3),
      (ICommandProcessor) new ReportParametersFieldExportProcessor(content, report.Parameters),
      (ICommandProcessor) new ReportMailAndCommonSettingsExportProcessor(content, report)
    });
    SettingsProvider instance4 = ServiceLocator.Current.GetInstance<SettingsProvider>();
    TemplateAssigmentProcessor assigmentProcessor1 = new TemplateAssigmentProcessor(savedTemplate, report, screenId1, instance4);
    ReportCollectionInsertProcessor<SortExp> collectionInsertProcessor1 = new ReportCollectionInsertProcessor<SortExp>((ICollection<SortExp>) report.DynamicSorting, "Sorting");
    ReportCollectionItemAssignProcessor<SortExp> itemAssignProcessor = new ReportCollectionItemAssignProcessor<SortExp>(new Func<SortExp>(collectionInsertProcessor1.GetItem));
    ReportCollectionInsertProcessor<FilterExp> collectionInsertProcessor2 = new ReportCollectionInsertProcessor<FilterExp>((ICollection<FilterExp>) report.DynamicFilters, ScreenUtils._filters);
    ReportFiltersAssigmentProcessor assigmentProcessor2 = new ReportFiltersAssigmentProcessor(new Func<FilterExp>(collectionInsertProcessor2.GetItem));
    new ScreenUtils.ReportCommandsProcessChain((IEnumerable<ICommandProcessor>) new ICommandProcessor[7]
    {
      (ICommandProcessor) assigmentProcessor1,
      (ICommandProcessor) collectionInsertProcessor1,
      (ICommandProcessor) itemAssignProcessor,
      (ICommandProcessor) collectionInsertProcessor2,
      (ICommandProcessor) assigmentProcessor2,
      (ICommandProcessor) new ReportCollectionExportProcessor<FilterExp>((IEnumerable<FilterExp>) report.DynamicFilters, ScreenUtils._filters, schema, content),
      (ICommandProcessor) new ReportCollectionExportProcessor<SortExp>((IEnumerable<SortExp>) report.DynamicSorting, "Sorting", schema, content)
    }).Process((IEnumerable<Command>) array3);
    ScreenUtils.RemoveStaticFiltersIfDynamicFilterOnSameFieldExists(report);
    commandsProcessChain1.Process((IEnumerable<Command>) array1);
    commandsProcessChain2.Process((IEnumerable<Command>) array2);
    ScreenUtils.SaveReportSettings(screenId1, report);
    ScreenUtils.SaveReportTemplate(savedTemplate, screenId1);
    ScreenUtils.ClearContent(array2, schema.ToArray<Content>());
    return schema.ToArray<Content>();
  }

  private static void RemoveStaticFiltersIfDynamicFilterOnSameFieldExists(Report report)
  {
    HashSet<string> hashSet = ((IEnumerable<FilterExp>) report.DynamicFilters).Where<FilterExp>((Func<FilterExp, bool>) (c => c.Condition == 0)).Select<FilterExp, string>((Func<FilterExp, string>) (c => c.DataField)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (FilterExp filter in (List<FilterExp>) report.Filters)
    {
      if (hashSet.Contains(filter.DataField))
      {
        filter.Condition = (FilterCondition) 12;
        filter.Value = filter.Value2 = "";
      }
    }
  }

  internal static void ClearContent(Command[] commands, Content[] resultContent)
  {
    foreach (Content content in resultContent)
    {
      Content schema = content;
      schema.Actions = (PX.Api.Models.Action[]) null;
      for (int i = 0; i < ((IEnumerable<Container>) schema.Containers).Count<Container>(); i++)
      {
        if (schema.Containers[i] != null)
        {
          schema.Containers[i].ServiceCommands = (Command[]) null;
          if (((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (c => string.Equals(c.ObjectName, schema.Containers[i].Name))).Count<Command>() == 0)
          {
            schema.Containers[i] = (Container) null;
          }
          else
          {
            for (int j = 0; j < ((IEnumerable<PX.Api.Models.Field>) schema.Containers[i].Fields).Count<PX.Api.Models.Field>(); j++)
            {
              if (((IEnumerable<Command>) commands).Where<Command>((Func<Command, bool>) (c => string.Equals(c.FieldName, schema.Containers[i].Fields[j].FieldName))).Count<Command>() == 0)
                schema.Containers[i].Fields[j] = (PX.Api.Models.Field) null;
            }
          }
        }
      }
    }
  }

  [PXInternalUseOnly]
  public static void SaveReportSettings(string screenId, Report report)
  {
    string key = "SubmitReportParameterKeys$" + screenId;
    ScreenUtils.WebReportSettings webReportSettings = new ScreenUtils.WebReportSettings();
    webReportSettings.ReportMailSettings = new ReportMailSettings()
    {
      Bcc = report.MailSettings.Bcc,
      Cc = report.MailSettings.Cc,
      EMail = report.MailSettings.To,
      Format = report.MailSettings.Format,
      Subject = report.MailSettings.Subject
    };
    webReportSettings.Filters = report.DynamicFilters;
    webReportSettings.Sorting = report.DynamicSorting;
    webReportSettings.Parameters = report.Parameters;
    webReportSettings.CommonSettings = report.CommonSettings;
    webReportSettings.BaseFilters = report.Filters;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
      PXSharedUserSession.CurrentUser[key] = (object) webReportSettings;
    else
      PXSharedUserSession.CurrentUser.Add(key, (object) webReportSettings);
  }

  private static string GetValue(Command[] SetCommands, string ObjectName, string FieldName)
  {
    string empty = string.Empty;
    foreach (Command setCommand in SetCommands)
    {
      if (string.Equals(setCommand.ObjectName, ObjectName) && string.Equals(setCommand.FieldName, FieldName))
        empty = setCommand.Value;
    }
    return empty.Replace("=", "").Replace("'", "");
  }

  [PXInternalUseOnly]
  public static ScreenUtils.Template GetSavedTemplate(Command[] setCommands, string screenId)
  {
    ScreenUtils.Template savedTemplate = (ScreenUtils.Template) null;
    string key = "SubmitReportTemplateKeys$" + screenId;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
      savedTemplate = (ScreenUtils.Template) PXSharedUserSession.CurrentUser[key];
    if (savedTemplate == null)
      savedTemplate = new ScreenUtils.Template();
    PropertyInfo[] properties = savedTemplate.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
    for (int index = 0; index < properties.Length; ++index)
    {
      PropertyInfo propertyInfo = (PropertyInfo) properties.GetValue(index);
      if (propertyInfo != (PropertyInfo) null)
      {
        string str = ScreenUtils.GetValue(setCommands, "Template", propertyInfo.Name);
        if (!string.IsNullOrEmpty(str))
          propertyInfo.SetValue((object) savedTemplate, Convert.ChangeType((object) str, propertyInfo.PropertyType), (object[]) null);
      }
    }
    return savedTemplate;
  }

  [PXInternalUseOnly]
  public static void SaveReportTemplate(ScreenUtils.Template template, string screenId)
  {
    string key = "SubmitReportTemplateKeys$" + screenId;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
      PXSharedUserSession.CurrentUser[key] = (object) template;
    else
      PXSharedUserSession.CurrentUser.Add(key, (object) template);
  }

  [PXInternalUseOnly]
  public static ScreenUtils.WebReportSettings GetSavedReportSettings(Report report, string screenId)
  {
    string key = "SubmitReportParameterKeys$" + screenId;
    ScreenUtils.WebReportSettings savedReportSettings;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
    {
      savedReportSettings = (ScreenUtils.WebReportSettings) PXSharedUserSession.CurrentUser[key];
    }
    else
    {
      savedReportSettings = new ScreenUtils.WebReportSettings()
      {
        CommonSettings = report.CommonSettings ?? new ReportCommonSettings(),
        Filters = report.DynamicFilters,
        BaseFilters = report.Filters,
        ReportMailSettings = ScreenUtils.ConvertMailSettings(report.MailSettings) ?? new ReportMailSettings(),
        Parameters = report.Parameters ?? new ReportParameterCollection(),
        Sorting = ((IEnumerable<SortExp>) report.DynamicSorting).Any<SortExp>() ? report.DynamicSorting : report.Sorting
      };
      PXSharedUserSession.CurrentUser.Add(key, (object) savedReportSettings);
    }
    return savedReportSettings;
  }

  [PXInternalUseOnly]
  public static ReportMailSettings ConvertMailSettings(MailSettings mailSettings)
  {
    return new ReportMailSettings()
    {
      Bcc = mailSettings.Bcc,
      Cc = mailSettings.Cc,
      EMail = mailSettings.To,
      Format = mailSettings.Format,
      Subject = mailSettings.Subject
    };
  }

  internal static Content[] Submit(
    string screenId,
    IReadOnlyList<Command> commands,
    SchemaMode schemaMode,
    ref PXGraph graph,
    ref string redirectContainerView,
    ref string redirectScreen,
    bool mobile = false,
    Dictionary<string, PXFilterRow[]> viewFilters = null,
    IGraphHelper graphHelper = null)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ScreenUtils.Submit(screenId, ScreenUtils.\u003C\u003EO.\u003C0\u003E__GetScreenInfo ?? (ScreenUtils.\u003C\u003EO.\u003C0\u003E__GetScreenInfo = new Func<string, (PXSiteMap.ScreenInfo, string)>(ScreenUtils.GetScreenInfo)), commands, schemaMode, ref graph, ref redirectContainerView, ref redirectScreen, mobile, viewFilters, graphHelper);
  }

  internal static Content[] Submit(
    string screenId,
    Func<string, (PXSiteMap.ScreenInfo screenInfo, string errorMessage)> getScreenInfoFunc,
    IReadOnlyList<Command> commands,
    SchemaMode schemaMode,
    ref PXGraph graph,
    ref string redirectContainerView,
    ref string redirectScreen,
    bool mobile = false,
    Dictionary<string, PXFilterRow[]> viewFilters = null,
    IGraphHelper graphHelper = null)
  {
    using (new ScreenUtils.LocaleScope())
    {
      PXSiteMap.ScreenInfo screenInfo = getScreenInfoFunc(screenId).Item1;
      SYMapping mapping = ScreenUtils.GetMapping(screenInfo, screenId);
      if (commands == null || commands.Count == 0)
      {
        string fullName = SyImportProcessor.CreateGraph(mapping.GraphName, screenId).GetType().FullName;
        return (Content[]) PXContext.Session.SubmitFieldErrors["SubmitFieldErrors$" + fullName];
      }
      foreach (Command command in (IEnumerable<Command>) commands)
      {
        if (command is PX.Api.Models.Value)
        {
          if (command.Value != null && !command.Value.StartsWith("="))
          {
            if (command.Value == "" || command.Value == "''")
            {
              command.Value = (string) null;
            }
            else
            {
              string str = command.Value;
              if (str.StartsWith("'") && str.EndsWith("'") && str.Length > 1)
                str = str.Substring(1, str.Length - 2);
              command.Value = $"='{str.Replace("'", "''")}'";
            }
          }
        }
        else if (command is PX.Api.Models.Field && command.Value == null)
          command.Value = Guid.NewGuid().ToString();
        if (command.Value != null && !command.Value.StartsWith("="))
        {
          if (command.FieldName == null || !command.FieldName.StartsWith("//"))
            command.LinkedCommand = (Command) null;
          command.Commit = false;
        }
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      commands = (IReadOnlyList<Command>) commands.SelectMany<Command, Command>(ScreenUtils.\u003C\u003EO.\u003C2\u003E__EnumFieldCommands ?? (ScreenUtils.\u003C\u003EO.\u003C2\u003E__EnumFieldCommands = new Func<Command, IEnumerable<Command>>(ScreenUtils.EnumFieldCommands))).ToArray<Command>();
      Content schema = ScreenService.GetSchema(screenId, schemaMode, screenInfo);
      List<Command> keys1 = ScreenUtils.GetKeys(screenInfo, schema);
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      foreach (Command command1 in (IEnumerable<Command>) commands)
      {
        if (command1.ObjectName == mapping.ViewName)
        {
          switch (command1)
          {
            case Key _:
              bool flag4 = false;
              foreach (Command command2 in keys1)
              {
                if (command2.FieldName == command1.FieldName)
                {
                  if (command1.Value == $"=[{command2.ObjectName}.{command2.FieldName}]")
                  {
                    flag4 = true;
                    break;
                  }
                }
              }
              if (!flag4)
              {
                flag1 = true;
                goto label_47;
              }
              flag2 = true;
              continue;
            case PX.Api.Models.Value _:
            case EveryValue _:
label_37:
              using (List<Command>.Enumerator enumerator = keys1.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  if (enumerator.Current.FieldName == command1.FieldName)
                  {
                    flag3 = true;
                    break;
                  }
                }
                continue;
              }
            default:
              if (command1.Value == null || !command1.Value.StartsWith("="))
                continue;
              goto label_37;
          }
        }
      }
label_47:
      if (!flag1 & flag3 & flag2)
        flag1 = true;
      Command command3 = (Command) null;
      for (int index1 = 0; index1 < commands.Count; ++index1)
      {
        Command command4 = commands[index1];
        if ((command3 == null || command3.ObjectName != command4.ObjectName) && !string.IsNullOrEmpty(command4.FieldName))
          command3 = command4;
        switch (command4)
        {
          case NewRow _:
          case RowNumber _:
          case Key _:
            Command command5 = (Command) null;
            for (int index2 = index1 + 1; index2 < commands.Count; ++index2)
            {
              Command command6 = commands[index2];
              if (command3 == null || !(command6.ObjectName != command3.ObjectName))
              {
                if (command3 != null && command3.FieldName == command6.FieldName)
                {
                  if (command5 != null)
                    command5.Commit = true;
                  index1 = index2 - 1;
                  break;
                }
                if (command6 is PX.Api.Models.Field && (command6.Value == null || command6.Value.StartsWith("=")))
                  command5 = command6;
              }
              else
                break;
            }
            break;
        }
      }
      Command[] array1 = commands.Where<Command>((Func<Command, bool>) (cmd => cmd.Value != null && !cmd.Value.StartsWith("="))).ToArray<Command>();
      if (mobile)
        array1 = EnumerableExtensions.Distinct<Command, string>((IEnumerable<Command>) array1, (Func<Command, string>) (cmd => cmd.Value)).ToArray<Command>();
      string[] array2 = ((IEnumerable<Command>) array1).Select<Command, string>((Func<Command, string>) (cmd => cmd.Value)).ToArray<string>();
      if (!mobile)
      {
        for (int index3 = 0; index3 < array2.Length; ++index3)
        {
          int num = index3;
          for (int index4 = 0; index4 < index3; ++index4)
          {
            if (string.Equals(array2[index4], array1[index3].Value, StringComparison.OrdinalIgnoreCase))
            {
              num = index4;
              break;
            }
          }
          if (num < index3)
          {
            Command command7 = array1[index3];
            string[] strArray = array2;
            int index5 = index3;
            Guid guid = Guid.NewGuid();
            string str1;
            string str2 = str1 = guid.ToString();
            strArray[index5] = str1;
            string str3 = str2;
            command7.Value = str3;
          }
        }
      }
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId);
      if (mapNodeByScreenId != null && mapNodeByScreenId.Url.Contains(".rpx"))
        return ScreenUtils.ReportSubmit(screenId, mapNodeByScreenId, schemaMode, (IEnumerable<Command>) commands);
      if (graph == null)
      {
        using (new PXPreserveScope())
        {
          graph = SyImportProcessor.CreateGraph(mapping.GraphName, screenId);
          graph.Load();
        }
      }
      PXView view = graph.Views[mapping.ViewName];
      if (!flag1)
      {
        if (keys1.Count > 0)
        {
          if (view.Cache.Current == null && view.Cache.AllowInsert)
          {
            graph.Clear();
            graph.ExecuteInsert(mapping.ViewName, (IDictionary) new Dictionary<string, object>());
          }
          if (view.Cache.Current != null)
          {
            int index6 = 0;
            while (index6 < keys1.Count)
            {
              Command command8 = keys1[index6];
              object valueExt = view.Cache.GetValueExt(view.Cache.Current, command8.FieldName);
              if (valueExt is PXFieldState)
                valueExt = ((PXFieldState) valueExt).Value;
              if (valueExt != null)
              {
                command8.Value = $"='{valueExt.ToString().Replace("'", "''")}'";
                ++index6;
              }
              else
                keys1.RemoveAt(index6);
            }
            int count = keys1.Count;
            for (int index7 = 0; index7 < count; ++index7)
            {
              string str = keys1[index7].ObjectName;
              if (!string.IsNullOrEmpty(str))
              {
                int length = str.IndexOf(": ");
                if (length != -1)
                  str = str.Substring(0, length);
              }
              List<Command> commandList = keys1;
              PX.Api.Models.Field field = new PX.Api.Models.Field();
              field.ObjectName = keys1[index7].ObjectName;
              field.FieldName = keys1[index7].FieldName;
              field.Value = keys1[index7].Value;
              field.Commit = true;
              commandList.Add((Command) field);
              keys1[index7].Value = $"=[{str}.{keys1[index7].FieldName}]";
            }
            keys1.AddRange((IEnumerable<Command>) commands);
            commands = (IReadOnlyList<Command>) keys1.ToArray();
          }
        }
        else if (view.Cache.Current == null)
        {
          int startRow = 0;
          int totalRows = -1;
          graph.ExecuteSelect(mapping.ViewName, new object[0], new object[0], new string[0], new bool[0], (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
          if (view.Cache.Current == null)
          {
            graph.Clear();
            graph.ExecuteInsert(mapping.ViewName, (IDictionary) new Dictionary<string, object>());
          }
          else if (view.Cache.Keys.Count > 0 && schema.Containers.Length != 0)
          {
            bool flag5 = false;
            foreach (Command serviceCommand in schema.Containers[0].ServiceCommands)
            {
              if (serviceCommand is RowNumber)
              {
                flag5 = true;
                break;
              }
            }
            if (!flag5)
            {
              graph.Clear();
              graph.ExecuteInsert(mapping.ViewName, (IDictionary) new Dictionary<string, object>());
            }
          }
        }
      }
      SYMappingField[] array3 = commands.Select<Command, SYMappingField>((Func<Command, SYMappingField>) (cmd => ScreenUtils.ConvertCommand(cmd))).ToArray<SYMappingField>();
      PXContext.Session.Remove(graph.GetType().FullName + "$SubmitFieldErrors");
      List<Command> commandList1;
      HashSet<string> stringSet;
      if ((commandList1 = PXContext.SessionTyped<PXSessionStatePXData>().SubmitFieldCommands[graph.GetType().FullName + "$SubmitFieldCommands"]) == null || (stringSet = PXContext.Session.SubmitFieldKeys[graph.GetType().FullName + "$SubmitFieldKeys"]) == null)
      {
        PXContext.SessionTyped<PXSessionStatePXData>().SubmitFieldKeys["SubmitFieldKeys$" + graph.GetType().FullName] = stringSet = new HashSet<string>();
        PXContext.SessionTyped<PXSessionStatePXData>().SubmitFieldCommands["SubmitFieldCommands$" + graph.GetType().FullName] = commandList1 = new List<Command>();
      }
      foreach (SYMappingField syMappingField in array3)
      {
        if (!string.IsNullOrEmpty(syMappingField.ObjectName) && !string.IsNullOrEmpty(syMappingField.FieldName) && char.IsLetter(syMappingField.FieldName[0]))
        {
          string str = $"{syMappingField.ObjectName}${syMappingField.FieldName}";
          if (!stringSet.Contains(str))
          {
            stringSet.Add(str);
            commandList1.Add(new Command()
            {
              ObjectName = syMappingField.ObjectName,
              FieldName = syMappingField.FieldName,
              Value = syMappingField.Value
            });
          }
        }
      }
      LinkedSelectorViews selectorViews = ViewHelper.CollectLinkedSelectorViews(new HashSet<string>(commands.Select<Command, string>((Func<Command, string>) (c => c.ObjectName)).Distinct<string>(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase), (IEnumerable<PXViewDescription>) screenInfo.Containers.Values, graph.PrimaryView);
      mapping.RepeatingData = new byte?((byte) 2);
      SyExportContext context1 = new SyExportContext(mapping, (IEnumerable<SYMappingField>) array3, ((IEnumerable<string>) array2).ToArray<string>(), viewFilters, true, count: graph.IsCopyPasteContext ? 1 : 0, selectorViews: selectorViews);
      if (mobile)
      {
        context1.RepeatingData = SYMapping.RepeatingOption.All;
        context1.Locale = PXCultureInfo.InvariantCulture.Name;
      }
      context1.Graph = graph;
      context1.ForceState = schemaMode != 0;
      PXSYTablePr table1 = SyImportProcessor.ExportTable(context1, !mobile, graphHelper);
      context1.Graph.Unload();
      if (context1.Error != null && !(context1.Error is PXRedirectRequiredException) && !(context1.Error is PXPopupRedirectException) && !(context1.Error is PXDialogRequiredException) && !(context1.Error is PXSignatureRequiredException))
      {
        if (commandList1.Count > 0)
        {
          try
          {
            string[] providerFields = new string[commandList1.Count];
            List<SYMappingField> syMappingFieldList = new List<SYMappingField>(commandList1.Count);
            for (int index = 0; index < commandList1.Count; ++index)
            {
              providerFields[index] = Guid.NewGuid().ToString();
              syMappingFieldList.Add(new SYMappingField()
              {
                ObjectName = commandList1[index].ObjectName,
                FieldName = commandList1[index].FieldName,
                Value = providerFields[index]
              });
            }
            if (syMappingFieldList.Count > 0)
            {
              if (view.Cache.Current != null)
              {
                List<Command> keys2 = ScreenUtils.GetKeys(screenInfo, schema);
                int index8 = 0;
                while (index8 < keys2.Count)
                {
                  Command command9 = keys2[index8];
                  object valueExt = view.Cache.GetValueExt(view.Cache.Current, command9.FieldName);
                  if (valueExt is PXFieldState)
                    valueExt = ((PXFieldState) valueExt).Value;
                  if (valueExt != null)
                  {
                    command9.Value = $"='{valueExt.ToString().Replace("'", "''")}'";
                    ++index8;
                  }
                  else
                    keys2.RemoveAt(index8);
                }
                int count = keys2.Count;
                for (int index9 = 0; index9 < count; ++index9)
                {
                  string str = keys2[index9].ObjectName;
                  if (!string.IsNullOrEmpty(str))
                  {
                    int length = str.IndexOf(": ");
                    if (length != -1)
                      str = str.Substring(0, length);
                  }
                  List<Command> commandList2 = keys2;
                  PX.Api.Models.Field field = new PX.Api.Models.Field();
                  field.ObjectName = keys2[index9].ObjectName;
                  field.FieldName = keys2[index9].FieldName;
                  field.Value = keys2[index9].Value;
                  field.Commit = true;
                  commandList2.Add((Command) field);
                  keys2[index9].Value = $"=[{str}.{keys2[index9].FieldName}]";
                  keys2[index9].FieldName = "@@" + keys2[index9].FieldName;
                }
                for (int index10 = 0; index10 < keys2.Count; ++index10)
                  syMappingFieldList.Insert(index10, new SYMappingField()
                  {
                    ObjectName = keys2[index10].ObjectName,
                    FieldName = keys2[index10].FieldName,
                    Value = keys2[index10].Value,
                    NeedCommit = new bool?(keys2[index10].Commit)
                  });
              }
              mapping.RepeatingData = new byte?((byte) 2);
              SyExportContext context2 = new SyExportContext(mapping, (IEnumerable<SYMappingField>) syMappingFieldList.ToArray(), providerFields, (Dictionary<string, PXFilterRow[]>) null, false, selectorViews: context1.LinkedSelectorViews);
              context2.TopCount = 1;
              context2.Graph = graph;
              context1.ForceState = schemaMode != 0;
              PXSYTablePr table2 = SyImportProcessor.ExportTable(context2);
              PXContext.Session.SubmitFieldErrors["SubmitFieldErrors$" + graph.GetType().FullName] = (object[]) ScreenUtils.ExtractContent((PXSYTable) table2, schema, commandList1.ToArray());
            }
            else
              PXContext.Session.SubmitFieldErrors.Remove("SubmitFieldErrors$" + graph.GetType().FullName);
          }
          catch
          {
          }
        }
        throw PXException.PreserveStack(context1.Error);
      }
      if (context1.Error != null && context1.Error is PXBaseRedirectException)
      {
        switch (context1.Error)
        {
          case PXRedirectRequiredException requiredException:
            graph = requiredException.Graph;
            graph.EnsureIfArchived();
            redirectScreen = requiredException.ScreenId;
            break;
          case PXPopupRedirectException redirectException:
            graph = redirectException.Graph;
            break;
          case PXDialogRequiredException ex:
            if (mobile && ex.UseAskDialog())
              throw ex;
            break;
        }
      }
      graph.InitViewDataCache(redirectScreen ?? screenId);
      return ScreenUtils.ExtractContent((PXSYTable) table1, schema, array1);
    }
  }

  internal static Content[] ExtractContent(
    PXSYTable table,
    Content info,
    Command[] exportCommands,
    bool packSelectorValues = false)
  {
    List<Content> contentList = new List<Content>();
    foreach (PXSYRow pxsyRow in table)
    {
      bool flag1 = false;
      Content content = new Content();
      content.Containers = new Container[info.Containers.Length];
      for (int index1 = 0; index1 < pxsyRow.Count; ++index1)
      {
        PXSYItem pxsyItem = pxsyRow.GetItem(index1);
        bool flag2 = false;
        Command exportCommand = exportCommands[index1];
        if (packSelectorValues && !((IEnumerable<Container>) info.Containers).Any<Container>((Func<Container, bool>) (c => c.ViewDescription.ViewName.OrdinalEquals(exportCommand.ObjectName))))
        {
          flag2 = true;
          exportCommand = (Command) ((IEnumerable<Container>) info.Containers).SelectMany<Container, PX.Api.Models.Field>((Func<Container, IEnumerable<PX.Api.Models.Field>>) (c => (IEnumerable<PX.Api.Models.Field>) c.Fields)).First<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (f => f.Descriptor.Container != null && ((IEnumerable<PX.Api.Models.Field>) f.Descriptor.Container.Fields).Any<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (sf => sf.FieldName.OrdinalEquals(exportCommand.FieldName) && sf.ObjectName.OrdinalEquals(exportCommand.ObjectName)))));
        }
        if (exportCommand.Value.Contains("%"))
          flag2 = true;
        PX.Api.Models.Value obj1 = new PX.Api.Models.Value();
        obj1.ObjectName = exportCommand.ObjectName;
        obj1.FieldName = exportCommand.FieldName;
        obj1.Value = packSelectorValues & flag2 ? $"{StringExtensions.Segment(exportCommands[index1].Value, '%', (ushort) 1)}\u001F{pxsyItem.Value}" : pxsyItem.Value;
        obj1.Message = pxsyItem.State != null ? pxsyItem.State.Error : (string) null;
        obj1.IsError = pxsyItem.State != null && (pxsyItem.State.ErrorLevel == PXErrorLevel.Error || pxsyItem.State.ErrorLevel == PXErrorLevel.RowError);
        obj1.ErrorLevel = pxsyItem.State == null ? (string) null : pxsyItem.State.ErrorLevel.ToString();
        PX.Api.Models.Value obj2 = obj1;
        for (int index2 = 0; index2 < info.Containers.Length; ++index2)
        {
          for (int index3 = 0; index3 < info.Containers[index2].Fields.Length; ++index3)
          {
            Command field1 = (Command) info.Containers[index2].Fields[index3];
            if (field1 is PX.Api.Models.Field && field1.ObjectName == obj2.ObjectName && field1.FieldName == obj2.FieldName)
            {
              if (content.Containers[index2] == null && !pxsyItem.IsAbsent)
                content.Containers[index2] = new Container()
                {
                  Name = info.Containers[index2].Name,
                  DisplayName = info.Containers[index2].DisplayName,
                  Fields = new PX.Api.Models.Field[info.Containers[index2].Fields.Length]
                };
              if (content.Containers[index2] != null)
              {
                if (content.Containers[index2].Fields[index3] != null & packSelectorValues)
                {
                  PX.Api.Models.Field field2 = content.Containers[index2].Fields[index3];
                  field2.Value = $"{field2.Value}\u001E{obj2.Value}";
                }
                else
                {
                  obj2.Name = field1.Name;
                  obj2.Commit = field1.Commit;
                  obj2.IgnoreError = field1.IgnoreError;
                  obj2.LinkedCommand = field1.LinkedCommand;
                  content.Containers[index2].Fields[index3] = (PX.Api.Models.Field) obj2;
                  if (field1.Descriptor != null)
                  {
                    obj2.Descriptor = new ElementDescriptor()
                    {
                      ElementType = field1.Descriptor.ElementType,
                      DisplayName = field1.Descriptor.DisplayName,
                      LengthLimit = field1.Descriptor.LengthLimit,
                      IsDisabled = field1.Descriptor.IsDisabled,
                      IsRequired = field1.Descriptor.IsRequired,
                      InputMask = field1.Descriptor.InputMask,
                      DisplayRules = field1.Descriptor.DisplayRules,
                      AllowedValues = field1.Descriptor.AllowedValues,
                      Visible = true
                    };
                    if (pxsyItem.State != null)
                    {
                      obj2.Descriptor.FieldType = ScreenUtils.ConvertFieldType(pxsyItem.State.DataType);
                      obj2.Descriptor.Visible = pxsyItem.State.Visible;
                      if (!string.IsNullOrEmpty(pxsyItem.State.DisplayName))
                        obj2.Descriptor.DisplayName = pxsyItem.State.DisplayName;
                      if (pxsyItem.State.Length > 0)
                        obj2.Descriptor.LengthLimit = pxsyItem.State.Length;
                      obj2.Descriptor.IsDisabled = !pxsyItem.State.Enabled;
                      obj2.Descriptor.IsRequired = pxsyItem.State.Required.GetValueOrDefault();
                      if (pxsyItem.State.DataType == typeof (Decimal) || pxsyItem.State.DataType == typeof (double) || pxsyItem.State.DataType == typeof (float))
                      {
                        if (pxsyItem.State.Precision > 0)
                          obj2.Descriptor.InputMask = "." + new string('9', pxsyItem.State.Precision);
                        else
                          obj2.Descriptor.InputMask = ".";
                      }
                      else if (pxsyItem.State is PXStringState && !string.IsNullOrEmpty(((PXStringState) pxsyItem.State).InputMask))
                        obj2.Descriptor.InputMask = ((PXStringState) pxsyItem.State).InputMask;
                      else if (pxsyItem.State is PXDateState && !string.IsNullOrEmpty(((PXDateState) pxsyItem.State).InputMask))
                        obj2.Descriptor.InputMask = ((PXDateState) pxsyItem.State).InputMask;
                      if (!string.IsNullOrEmpty(pxsyItem.State.ViewName))
                      {
                        if (obj2.Descriptor.ElementType != ElementTypes.StringSelector && obj2.Descriptor.ElementType != ElementTypes.ExplicitSelector)
                          obj2.Descriptor.ElementType = ElementTypes.StringSelector;
                      }
                      else if (pxsyItem.State.DataType == typeof (bool))
                      {
                        obj2.Descriptor.ElementType = ElementTypes.Option;
                        obj2.Descriptor.AllowedValues = new string[2]
                        {
                          "True",
                          "False"
                        };
                      }
                      else if (pxsyItem.State is PXStringState && ((PXStringState) pxsyItem.State).AllowedValues != null)
                      {
                        obj2.Descriptor.ElementType = ((PXStringState) pxsyItem.State).ExclusiveValues ? ElementTypes.Option : ElementTypes.WideOption;
                        obj2.Descriptor.AllowedValues = ((PXStringState) pxsyItem.State).AllowedLabels;
                      }
                      else if (pxsyItem.State is PXIntState && ((PXIntState) pxsyItem.State).AllowedValues != null)
                      {
                        obj2.Descriptor.ElementType = ((PXIntState) pxsyItem.State).ExclusiveValues ? ElementTypes.Option : ElementTypes.WideOption;
                        obj2.Descriptor.AllowedValues = ((PXIntState) pxsyItem.State).AllowedLabels;
                      }
                      else if (pxsyItem.State.DataType == typeof (string))
                      {
                        if (pxsyItem.State is PXStringState && ((PXStringState) pxsyItem.State).IsUnicode)
                          obj2.Descriptor.ElementType = ElementTypes.String;
                        else
                          obj2.Descriptor.ElementType = ElementTypes.AsciiString;
                      }
                      else if (pxsyItem.State.DataType == typeof (System.DateTime))
                      {
                        obj2.Descriptor.ElementType = ElementTypes.Calendar;
                        if (pxsyItem.State is PXDateState)
                        {
                          if (!string.IsNullOrEmpty(((PXDateState) pxsyItem.State).DisplayMask))
                            obj2.Descriptor.DisplayRules = ((PXDateState) pxsyItem.State).DisplayMask;
                          System.DateTime dateTime;
                          if (((PXDateState) pxsyItem.State).MinValue != System.DateTime.MinValue && ((PXDateState) pxsyItem.State).MaxValue != System.DateTime.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            dateTime = ((PXDateState) pxsyItem.State).MinValue;
                            strArray[0] = dateTime.ToString();
                            dateTime = ((PXDateState) pxsyItem.State).MaxValue;
                            strArray[1] = dateTime.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDateState) pxsyItem.State).MinValue != System.DateTime.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            dateTime = ((PXDateState) pxsyItem.State).MinValue;
                            strArray[0] = dateTime.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDateState) pxsyItem.State).MaxValue != System.DateTime.MaxValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            dateTime = ((PXDateState) pxsyItem.State).MaxValue;
                            strArray[1] = dateTime.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                      }
                      else
                      {
                        obj2.Descriptor.ElementType = ElementTypes.Number;
                        int num1;
                        long num2;
                        double num3;
                        Decimal num4;
                        float num5;
                        if (pxsyItem.State is PXIntState)
                        {
                          if (((PXIntState) pxsyItem.State).MinValue != int.MinValue && ((PXIntState) pxsyItem.State).MaxValue != int.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num1 = ((PXIntState) pxsyItem.State).MinValue;
                            strArray[0] = num1.ToString();
                            num1 = ((PXIntState) pxsyItem.State).MaxValue;
                            strArray[1] = num1.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXIntState) pxsyItem.State).MinValue != int.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num1 = ((PXIntState) pxsyItem.State).MinValue;
                            strArray[0] = num1.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXIntState) pxsyItem.State).MaxValue != int.MaxValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num1 = ((PXIntState) pxsyItem.State).MaxValue;
                            strArray[1] = num1.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                        else if (pxsyItem.State is PXLongState)
                        {
                          if (((PXLongState) pxsyItem.State).MinValue != long.MinValue && ((PXLongState) pxsyItem.State).MaxValue != long.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num2 = ((PXLongState) pxsyItem.State).MinValue;
                            strArray[0] = num2.ToString();
                            num2 = ((PXLongState) pxsyItem.State).MaxValue;
                            strArray[1] = num2.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXLongState) pxsyItem.State).MinValue != long.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num2 = ((PXLongState) pxsyItem.State).MinValue;
                            strArray[0] = num2.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXLongState) pxsyItem.State).MaxValue != long.MaxValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num2 = ((PXLongState) pxsyItem.State).MaxValue;
                            strArray[1] = num2.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                        else if (pxsyItem.State is PXDoubleState)
                        {
                          if (((PXDoubleState) pxsyItem.State).MinValue != double.MinValue && ((PXDoubleState) pxsyItem.State).MaxValue != double.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num3 = ((PXDoubleState) pxsyItem.State).MinValue;
                            strArray[0] = num3.ToString();
                            num3 = ((PXDoubleState) pxsyItem.State).MaxValue;
                            strArray[1] = num3.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDoubleState) pxsyItem.State).MinValue != double.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num3 = ((PXDoubleState) pxsyItem.State).MinValue;
                            strArray[0] = num3.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDoubleState) pxsyItem.State).MaxValue != double.MaxValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num3 = ((PXDoubleState) pxsyItem.State).MaxValue;
                            strArray[1] = num3.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                        else if (pxsyItem.State is PXDecimalState)
                        {
                          if (((PXDecimalState) pxsyItem.State).MinValue != Decimal.MinValue && ((PXDecimalState) pxsyItem.State).MaxValue != Decimal.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num4 = ((PXDecimalState) pxsyItem.State).MinValue;
                            strArray[0] = num4.ToString();
                            num4 = ((PXDecimalState) pxsyItem.State).MaxValue;
                            strArray[1] = num4.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDecimalState) pxsyItem.State).MinValue != Decimal.MinValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num4 = ((PXDecimalState) pxsyItem.State).MinValue;
                            strArray[0] = num4.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if (((PXDecimalState) pxsyItem.State).MaxValue != Decimal.MaxValue)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num4 = ((PXDecimalState) pxsyItem.State).MaxValue;
                            strArray[1] = num4.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                        else if (pxsyItem.State is PXFloatState)
                        {
                          if ((double) ((PXFloatState) pxsyItem.State).MinValue != -3.4028234663852886E+38 && (double) ((PXFloatState) pxsyItem.State).MaxValue != -3.4028234663852886E+38)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num5 = ((PXFloatState) pxsyItem.State).MinValue;
                            strArray[0] = num5.ToString();
                            num5 = ((PXFloatState) pxsyItem.State).MaxValue;
                            strArray[1] = num5.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if ((double) ((PXFloatState) pxsyItem.State).MinValue != -3.4028234663852886E+38)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num5 = ((PXFloatState) pxsyItem.State).MinValue;
                            strArray[0] = num5.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else if ((double) ((PXFloatState) pxsyItem.State).MaxValue != 3.4028234663852886E+38)
                          {
                            ElementDescriptor descriptor = obj2.Descriptor;
                            string[] strArray = new string[2];
                            num5 = ((PXFloatState) pxsyItem.State).MaxValue;
                            strArray[1] = num5.ToString();
                            descriptor.AllowedValues = strArray;
                          }
                          else
                            obj2.Descriptor.AllowedValues = (string[]) null;
                        }
                      }
                    }
                  }
                  flag1 = true;
                }
              }
            }
          }
        }
      }
      if (flag1)
        contentList.Add(content);
    }
    return contentList.ToArray();
  }

  internal static List<Command> GetKeys(PXSiteMap.ScreenInfo screenInfo, Content info)
  {
    List<Command> keys = new List<Command>();
    string primaryView = screenInfo.PrimaryView;
    Container container1 = (Container) null;
    if (!string.IsNullOrEmpty(primaryView))
      container1 = ((IEnumerable<Container>) info.Containers).FirstOrDefault<Container>((Func<Container, bool>) (c => primaryView.Equals(c.ViewDescription?.ViewName, StringComparison.OrdinalIgnoreCase)));
    else if (info.Containers.Length != 0)
      container1 = info.Containers[0];
    if (container1 != null)
    {
      foreach (Command serviceCommand in container1.ServiceCommands)
      {
        if (serviceCommand is Key)
        {
          bool flag1 = false;
          if (string.IsNullOrEmpty(primaryView) && !string.IsNullOrEmpty(serviceCommand.ObjectName))
          {
            int length = serviceCommand.ObjectName.IndexOf(':');
            primaryView = length > 0 ? serviceCommand.ObjectName.Substring(0, length) : serviceCommand.ObjectName;
          }
          foreach (Command field in container1.Fields)
          {
            if (field.FieldName == serviceCommand.FieldName)
            {
              flag1 = true;
              bool flag2 = false;
              for (Command linkedCommand = field.LinkedCommand; linkedCommand != null && !flag2; linkedCommand = linkedCommand.LinkedCommand)
                flag2 = linkedCommand is PX.Api.Models.Action;
              if (flag2)
              {
                List<Command> commandList = keys;
                Key key = new Key();
                key.ObjectName = primaryView;
                key.FieldName = serviceCommand.FieldName;
                commandList.Add((Command) key);
                break;
              }
            }
          }
          if (!flag1)
          {
            foreach (Container container2 in info.Containers)
            {
              foreach (Command field in container2.Fields)
              {
                if (!string.IsNullOrEmpty(field.ObjectName) && field.ObjectName.StartsWith(primaryView + ":"))
                {
                  if (field.FieldName == serviceCommand.Name)
                  {
                    flag1 = true;
                    bool flag3 = false;
                    for (Command linkedCommand = field.LinkedCommand; linkedCommand != null && !flag3; linkedCommand = linkedCommand.LinkedCommand)
                      flag3 = linkedCommand is PX.Api.Models.Action;
                    if (flag3)
                    {
                      List<Command> commandList = keys;
                      Key key = new Key();
                      key.ObjectName = primaryView;
                      key.FieldName = serviceCommand.FieldName;
                      commandList.Add((Command) key);
                      break;
                    }
                  }
                }
                else
                  break;
              }
              if (flag1)
                break;
            }
          }
        }
      }
    }
    return keys;
  }

  [PXInternalUseOnly]
  public class Template
  {
    private string _name;
    private bool _isDefault;
    private bool _isShared;

    public string Name
    {
      get => this._name;
      set => this._name = value;
    }

    public bool IsDefault
    {
      get => this._isDefault;
      set => this._isDefault = value;
    }

    public bool IsShared
    {
      get => this._isShared;
      set => this._isShared = value;
    }
  }

  private sealed class LocaleScope : IDisposable
  {
    public string ForceLocale;
    private CultureInfo previousCulture;

    public LocaleScope()
    {
      this.previousCulture = Thread.CurrentThread.CurrentCulture;
      string forceLocale = PXContext.Session["LocaleName"] as string;
      if (!string.IsNullOrEmpty(this.ForceLocale))
        forceLocale = this.ForceLocale;
      if (string.IsNullOrEmpty(forceLocale))
        return;
      LocaleInfo.SetAllCulture(new CultureInfo(forceLocale));
    }

    public LocaleScope(string locale)
      : this()
    {
      this.ForceLocale = locale;
    }

    void IDisposable.Dispose() => LocaleInfo.SetAllCulture(this.previousCulture);
  }

  private class ReportCommandsProcessChain
  {
    private readonly IEnumerable<ICommandProcessor> _processChain;

    public ReportCommandsProcessChain(IEnumerable<ICommandProcessor> processChain)
    {
      this._processChain = processChain;
    }

    public void Process(IEnumerable<Command> commands)
    {
      foreach (Command command in commands)
      {
        foreach (ICommandProcessor commandProcessor in this._processChain)
        {
          if (commandProcessor.CanExecute(command))
            commandProcessor.Execute(command);
        }
      }
    }
  }

  [PXInternalUseOnly]
  [Serializable]
  public class WebReportSettings
  {
    public ReportCommonSettings CommonSettings;
    public ReportMailSettings ReportMailSettings;
    public ReportParameterCollection Parameters;
    public SortExpCollection Sorting;
    public FilterExpCollection Filters;
    public FilterExpCollection BaseFilters;
  }
}
