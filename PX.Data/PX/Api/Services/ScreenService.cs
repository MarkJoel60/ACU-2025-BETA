// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.ScreenService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Async;
using PX.Common;
using PX.Data;
using PX.Data.Api.Mobile;
using PX.Data.Api.Services;
using PX.Metadata;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Api.Services;

internal class ScreenService : IScreenService
{
  private readonly OptimizedExportProviderBuilderForScreenBasedApi _optimizedExportProviderBuilder;
  private readonly IScreenInfoProvider _screenInfoProvider;
  private readonly IDialogService _dialogService;
  private readonly ILongOperationManager _longOperationManager;
  private static readonly System.Type[] _schemaCacheSlotTables = ((IEnumerable<System.Type>) PXGenericInqGrph.Definition.UsedTables).Concat<System.Type>((IEnumerable<System.Type>) new System.Type[12]
  {
    typeof (PX.SM.SiteMap),
    typeof (MobileSiteMap),
    typeof (PX.SM.RolesInGraph),
    typeof (PX.SM.RolesInCache),
    typeof (PX.SM.RolesInMember),
    typeof (FilterHeader),
    typeof (LocalizationResource),
    typeof (LocalizationTranslationSet),
    typeof (LocalizationResourceByScreen),
    typeof (LocalizationTranslation),
    typeof (LocalizationTranslationSetItem),
    typeof (LocalizationValue)
  }).ToArray<System.Type>();
  private static object _sync = new object();
  internal Func<PXSYRow, IList<string>, string[]> _serializationDelegate;

  public ScreenService(
    OptimizedExportProviderBuilderForScreenBasedApi optimizedExportProviderBuilder,
    IScreenInfoProvider screenInfoProvider,
    IDialogService dialogService,
    ILongOperationManager longOperationManager)
  {
    this._optimizedExportProviderBuilder = optimizedExportProviderBuilder;
    this._screenInfoProvider = screenInfoProvider;
    this._dialogService = dialogService;
    this._longOperationManager = longOperationManager;
  }

  public string ExtractViewName(string viewName)
  {
    return !viewName.Contains(":") ? viewName : viewName.Substring(0, viewName.IndexOf(":", StringComparison.OrdinalIgnoreCase));
  }

  public Content GetSchema(string id, SchemaMode mode)
  {
    return ScreenService.GetSchema(id, mode, (PXSiteMap.ScreenInfo) null);
  }

  internal static Content GetSchema(
    string screenId,
    SchemaMode mode,
    PXSiteMap.ScreenInfo screenInfo)
  {
    ScreenService.AssertApiEnabled(screenId);
    bool appendDescriptors = mode != 0;
    Content schema = PXContext.SessionTyped<PXSessionStatePXData>().ScreenGateSchema["ScreenGateSchema$" + screenId];
    if (schema != null)
      return schema;
    return screenInfo == null ? ScreenUtils.GetScreenInfoWithServiceCommands(appendDescriptors, mode == SchemaMode.DetailedWithHidden, screenId, true) : screenInfo.GetScreenInfoWithServiceCommands(screenId, appendDescriptors, mode == SchemaMode.DetailedWithHidden, true);
  }

  private static void AssertApiEnabled(string id)
  {
    if (!PXAccess.IsScreenApiEnabled(id))
      throw new PXNotEnoughRightsException(PXCacheRights.Denied);
  }

  public void Set(string id, Content schemaContent)
  {
    id = this.ExtractId(id);
    ScreenService.AssertApiEnabled(id);
    int? schemaMode = PXContext.Session.SchemaMode;
    int num = 2;
    Content withServiceCommands = ScreenUtils.GetScreenInfoWithServiceCommands(false, (schemaMode.GetValueOrDefault() == num & schemaMode.HasValue ? 1 : 0) != 0, id, true);
    if (withServiceCommands != null)
      this.AdjustSchema(withServiceCommands, schemaContent);
    PXContext.SessionTyped<PXSessionStatePXData>().ScreenGateSchema["ScreenGateSchema$" + id] = schemaContent;
  }

  private void AdjustSchema(Content actual, Content stored)
  {
    Dictionary<string, Tuple<string, string, string>> dictionary = new Dictionary<string, Tuple<string, string, string>>();
    foreach (PX.Api.Models.Action action in actual.Actions)
    {
      if (!string.IsNullOrEmpty(action.Name))
      {
        string key = "Actions$" + action.Name;
        dictionary[key] = new Tuple<string, string, string>(action.ObjectName, action.FieldName, action.Value);
        for (Command linkedCommand = action.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
        {
          key += "$LinkedCommand";
          dictionary[key] = new Tuple<string, string, string>(linkedCommand.ObjectName, linkedCommand.FieldName, linkedCommand.Value);
        }
      }
    }
    foreach (Container container in actual.Containers)
    {
      if (!string.IsNullOrEmpty(container.Name))
      {
        foreach (PX.Api.Models.Field field in container.Fields)
        {
          if (!string.IsNullOrEmpty(field.Name))
          {
            string key = $"Containers${container.Name}${field.Name}";
            dictionary[key] = new Tuple<string, string, string>(field.ObjectName, field.FieldName, field.Value);
            for (Command linkedCommand = field.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
            {
              key += "$LinkedCommand";
              dictionary[key] = new Tuple<string, string, string>(linkedCommand.ObjectName, linkedCommand.FieldName, linkedCommand.Value);
            }
          }
        }
        foreach (Command serviceCommand in container.ServiceCommands)
        {
          if (!string.IsNullOrEmpty(serviceCommand.Name))
          {
            string key = $"{container.Name}$ServiceCommands${serviceCommand.Name}";
            dictionary[key] = new Tuple<string, string, string>(serviceCommand.ObjectName, serviceCommand.FieldName, serviceCommand.Value);
            for (Command linkedCommand = serviceCommand.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
            {
              key += "$LinkedCommand";
              dictionary[key] = new Tuple<string, string, string>(linkedCommand.ObjectName, linkedCommand.FieldName, linkedCommand.Value);
            }
          }
        }
      }
    }
    Tuple<string, string, string> tuple;
    foreach (PX.Api.Models.Action action in stored.Actions)
    {
      if (!string.IsNullOrEmpty(action.Name))
      {
        string key = "Actions$" + action.Name;
        if (dictionary.TryGetValue(key, out tuple))
        {
          action.ObjectName = tuple.Item1;
          action.FieldName = tuple.Item2;
          action.Value = tuple.Item3;
        }
        for (Command linkedCommand = action.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
        {
          key += "$LinkedCommand";
          if (dictionary.TryGetValue(key, out tuple))
          {
            linkedCommand.ObjectName = tuple.Item1;
            linkedCommand.FieldName = tuple.Item2;
            linkedCommand.Value = tuple.Item3;
          }
        }
      }
    }
    foreach (Container container in stored.Containers)
    {
      if (!string.IsNullOrEmpty(container.Name))
      {
        foreach (PX.Api.Models.Field field in container.Fields)
        {
          if (!string.IsNullOrEmpty(field.Name))
          {
            string key = $"Containers${container.Name}${field.Name}";
            if (dictionary.TryGetValue(key, out tuple))
            {
              field.ObjectName = tuple.Item1;
              field.FieldName = tuple.Item2;
              field.Value = tuple.Item3;
            }
            for (Command linkedCommand = field.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
            {
              key += "$LinkedCommand";
              if (dictionary.TryGetValue(key, out tuple))
              {
                linkedCommand.ObjectName = tuple.Item1;
                linkedCommand.FieldName = tuple.Item2;
                linkedCommand.Value = tuple.Item3;
              }
            }
          }
        }
        foreach (Command serviceCommand in container.ServiceCommands)
        {
          if (!string.IsNullOrEmpty(serviceCommand.Name))
          {
            string key = $"{container.Name}$ServiceCommands${serviceCommand.Name}";
            if (dictionary.TryGetValue(key, out tuple))
            {
              serviceCommand.ObjectName = tuple.Item1;
              serviceCommand.FieldName = tuple.Item2;
              serviceCommand.Value = tuple.Item3;
            }
            for (Command linkedCommand = serviceCommand.LinkedCommand; linkedCommand != null; linkedCommand = linkedCommand.LinkedCommand)
            {
              key += "$LinkedCommand";
              if (dictionary.TryGetValue(key, out tuple))
              {
                linkedCommand.ObjectName = tuple.Item1;
                linkedCommand.FieldName = tuple.Item2;
                linkedCommand.Value = tuple.Item3;
              }
            }
          }
        }
      }
    }
  }

  public void Clear(string id)
  {
    id = this.ExtractId(id);
    ScreenService.AssertApiEnabled(id);
    try
    {
      SYMapping mapping = ScreenUtils.GetMapping(id);
      using (new PXPreserveScope())
      {
        PXGraph graph = SyImportProcessor.CreateGraph(mapping.GraphName, id);
        graph.Load();
        graph.Clear();
        PXContext.SessionTyped<PXSessionStatePXData>().SubmitFieldKeys.Remove("SubmitFieldKeys$" + graph.GetType().FullName);
        PXContext.SessionTyped<PXSessionStatePXData>().SubmitFieldCommands.Remove("SubmitFieldCommands$" + graph.GetType().FullName);
        PXContext.Session.SubmitFieldErrors.Remove("SubmitFieldErrors$" + graph.GetType().FullName);
        string key1 = "SubmitReportKeyPreProcessInstance$" + id;
        string key2 = "SubmitReportKeyPreProcessIsActive$" + id;
        if (PXSharedUserSession.CurrentUser.ContainsKey(key1))
        {
          PXLongOperation.ClearStatus(PXSharedUserSession.CurrentUser[key1]);
          PXSharedUserSession.CurrentUser.Remove(key1);
        }
        PXSharedUserSession.CurrentUser.Remove("SubmitReportParameterKeys$" + id);
        PXSharedUserSession.CurrentUser.Remove(key2);
        PXSharedUserSession.CurrentUser.Remove("SubmitReportTemplateKeys$" + id);
      }
    }
    catch (PXForceLogOutException ex)
    {
      throw new Exception(ex.Title);
    }
  }

  public IEnumerable<Content> Submit(
    string id,
    IEnumerable<Command> commands,
    SchemaMode schemaMode)
  {
    PXGraph forceGraph = (PXGraph) null;
    string redirectContainerView = (string) null;
    string redirectScreen = (string) null;
    return this.Submit(id, commands, schemaMode, false, ref forceGraph, ref redirectContainerView, ref redirectScreen, (Dictionary<string, PXFilterRow[]>) null, (IGraphHelper) null);
  }

  public IEnumerable<Content> Submit(
    string id,
    IEnumerable<Command> commands,
    SchemaMode schemaMode,
    bool mobile,
    ref PXGraph forceGraph,
    ref string redirectContainerView,
    ref string redirectScreen,
    Dictionary<string, PXFilterRow[]> viewFilters,
    IGraphHelper graphHelper = null)
  {
    this._dialogService.SetAnswers(forceGraph);
    id = this.ExtractId(id);
    ScreenService.AssertApiEnabled(id);
    if (commands == null)
    {
      try
      {
        return (IEnumerable<Content>) ScreenUtils.Submit(id, this.GetScreenInfoFunc(mobile), (IReadOnlyList<Command>) null, schemaMode, ref forceGraph, ref redirectContainerView, ref redirectScreen, mobile, viewFilters);
      }
      catch (PXForceLogOutException ex)
      {
        throw new Exception(ex.Title);
      }
    }
    else
    {
      List<List<Command>> source = new List<List<Command>>();
      source.Add(new List<Command>());
      foreach (Command command in commands)
      {
        if (command == null)
          source.Add(new List<Command>());
        else
          source.Last<List<Command>>().Add(command);
      }
      List<Content> contentList = new List<Content>();
      for (int index = 0; index < source.Count; ++index)
      {
        Content[] collection = ScreenUtils.Submit(id, this.GetScreenInfoFunc(mobile), (IReadOnlyList<Command>) source[index], schemaMode, ref forceGraph, ref redirectContainerView, ref redirectScreen, mobile, viewFilters, graphHelper);
        if (collection != null)
          contentList.AddRange((IEnumerable<Content>) collection);
        if (index < source.Count - 1)
          contentList.Add((Content) null);
      }
      return (IEnumerable<Content>) contentList.ToArray();
    }
  }

  public IEnumerable<ImportResult> Import(
    string id,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    string[][] data,
    bool includeHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget)
  {
    ScreenService.AssertApiEnabled(id);
    try
    {
      return (IEnumerable<ImportResult>) ScreenUtils.Import(id, commands, filters, data, includeHeaders, breakOnError, breakOnIncorrectTarget, (PXGraph) null);
    }
    catch (PXForceLogOutException ex)
    {
      throw new Exception(ex.Title);
    }
  }

  public string[][] Export(
    string id,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    int startRow,
    int topCount,
    bool includeHeaders,
    bool breakOnError,
    bool bindGuids = false,
    bool mobile = false,
    bool isSelector = false,
    string forcePrimaryView = null,
    PXGraph forceGraph = null,
    string bindContainer = "undefined",
    Dictionary<string, KeyValuePair<string, bool>[]> sorts = null,
    string guidViewName = "undefined",
    bool disableOptimizedExport = false)
  {
    ScreenService.AssertApiEnabled(id);
    try
    {
      return ScreenUtils.ExportInternal(id, this.GetScreenInfoFunc(mobile), commands, filters, startRow, topCount, includeHeaders, breakOnError, forceGraph, bindGuids, mobile, isSelector, forcePrimaryView, bindContainer, sorts, guidViewName, disableOptimizedExport ? (OptimizedExportProviderBuilderForScreenBasedApi) null : this._optimizedExportProviderBuilder, this._serializationDelegate);
    }
    catch (PXForceLogOutException ex)
    {
      throw new Exception(ex.Title);
    }
  }

  public ProcessResult GetProcessStatus(string id)
  {
    Exception exception = (Exception) null;
    return this.GetProcessStatus(id, (PXGraph) null, out exception);
  }

  public ProcessResult GetProcessStatus(string id, PXGraph targetGraph)
  {
    Exception exception = (Exception) null;
    return this.GetProcessStatus(id, targetGraph, out exception);
  }

  public void AbortProcess(string id)
  {
    string key = "SubmitReportKeyPreProcessInstance$" + id;
    if (!PXSharedUserSession.CurrentUser.ContainsKey(key))
      return;
    PXLongOperation.AsyncAbort(PXSharedUserSession.CurrentUser[key]);
  }

  public void AbortProcess(object longOperationKey) => PXLongOperation.AsyncAbort(longOperationKey);

  public ProcessResult GetProcessStatus(string id, PXGraph targetGraph, out Exception exception)
  {
    id = this.ExtractId(id);
    ScreenService.AssertApiEnabled(id);
    string key = "SubmitReportKeyPreProcessInstance$" + id;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
    {
      ProcessResult processStatus = new ProcessResult();
      LongOperationDetails operationDetails = this._longOperationManager.GetOperationDetails(PXSharedUserSession.CurrentUser[key]);
      exception = operationDetails.Message;
      switch (operationDetails.Status)
      {
        case PXLongRunStatus.NotExists:
          processStatus.Status = ProcessStatus.NotExists;
          break;
        case PXLongRunStatus.InProcess:
          processStatus.Status = ProcessStatus.InProcess;
          break;
        case PXLongRunStatus.Completed:
          processStatus.Status = ProcessStatus.Completed;
          break;
        case PXLongRunStatus.Aborted:
          processStatus.Status = ProcessStatus.Aborted;
          throw exception;
      }
      if (exception != null)
        processStatus.Message = ScreenUtils.ExtractMessage(exception);
      processStatus.Seconds = Convert.ToInt32(operationDetails.Duration.TotalSeconds);
      return processStatus;
    }
    PXGraph pxGraph;
    if (targetGraph == null)
    {
      PXSiteMap.ScreenInfo screenInfo = this.GetScreenInfo(id);
      using (new PXPreserveScope())
      {
        pxGraph = SyImportProcessor.CreateGraph(screenInfo.GraphName, id);
        pxGraph.Load();
        pxGraph.IsMobile = true;
      }
    }
    else
      pxGraph = targetGraph;
    return this.GetProcessStatus(pxGraph.UID, out exception);
  }

  public Exception WaitLongOperation(string screenId, PXGraph targetGraph)
  {
    Exception exception;
    ProcessResult processStatus;
    for (processStatus = this.GetProcessStatus(screenId, targetGraph, out exception); processStatus.Status == ProcessStatus.InProcess; processStatus = this.GetProcessStatus(screenId, targetGraph, out exception))
      Thread.Sleep(100);
    if (processStatus.Status == ProcessStatus.Aborted)
      throw new PXException(processStatus.Message);
    PXLongOperation.ForceClearStatus(targetGraph);
    return exception;
  }

  public void FillGraphFromOperationContext(PXGraph targetGraph, string screenId)
  {
    object customInfo = PXLongOperation.GetCustomInfo(targetGraph.UID);
    ProcessResult processStatus = this.GetProcessStatus(screenId, targetGraph);
    PXGraph pxGraph = customInfo as PXGraph;
    PXLongOperation.ForceClearStatus(targetGraph);
    if (pxGraph == null)
      return;
    if (processStatus.Status != ProcessStatus.NotExists)
      targetGraph.Clear(PXClearOption.ClearQueriesOnly);
    foreach (PXView pxView in targetGraph.Views.Values.ToArray<PXView>())
      pxView.DetachCache();
    foreach (PXCache cach1 in pxGraph.Caches.Caches)
    {
      PXCache cach2 = targetGraph.Caches[cach1.GetItemType()];
      cach2.Clear();
      cach2.ClearQueryCache();
      foreach (object data in cach1.Cached)
      {
        PXEntryStatus status = cach1.GetStatus(data);
        cach2.SetStatus(data, status);
        if ((status == PXEntryStatus.Updated || status == PXEntryStatus.Inserted) && cach1.HasAttributes(data))
        {
          foreach (PXEventSubscriberAttribute attribute in cach1.GetAttributes(data, (string) null))
          {
            if (attribute is IPXInterfaceField)
            {
              IPXInterfaceField pxInterfaceField = (IPXInterfaceField) attribute;
              if (pxInterfaceField.ErrorLevel == PXErrorLevel.Error && pxInterfaceField.ErrorValue != null)
                PXUIFieldAttribute.SetError(cach2, data, attribute.FieldName, pxInterfaceField.ErrorText, pxInterfaceField.ErrorValue.ToString());
            }
          }
        }
      }
      cach2.IsDirty = cach1.IsDirty;
      cach2._Current = cach1._Current;
    }
    targetGraph.TypedViews.Clear();
    foreach (KeyValuePair<System.Type, PXView> typedView in (Dictionary<System.Type, PXView>) pxGraph.TypedViews)
      targetGraph.TypedViews.Add(typedView.Key, typedView.Value);
    targetGraph.Unload();
  }

  public ProcessResult GetProcessStatus(object key, out Exception exception)
  {
    LongOperationDetails operationDetails = this._longOperationManager.GetOperationDetails(key);
    exception = operationDetails.Message;
    ProcessResult processStatus = new ProcessResult();
    switch (operationDetails.Status)
    {
      case PXLongRunStatus.NotExists:
        processStatus.Status = ProcessStatus.NotExists;
        return processStatus;
      case PXLongRunStatus.InProcess:
        processStatus.Status = ProcessStatus.InProcess;
        break;
      case PXLongRunStatus.Aborted:
        processStatus.Status = ProcessStatus.Aborted;
        break;
      default:
        processStatus.Status = ProcessStatus.Completed;
        break;
    }
    if (exception != null)
      processStatus.Message = ScreenUtils.ExtractMessage(exception);
    processStatus.Seconds = Convert.ToInt32(operationDetails.Duration.TotalSeconds);
    return processStatus;
  }

  public string ExtractId(string id) => ScreenUtils.ExtractId(id);

  public void SetMode(SchemaMode mode) => PXContext.Session.SchemaMode = new int?((int) mode);

  public IEnumerable<Command> GetScenario(string id, string scenario)
  {
    if (!string.IsNullOrWhiteSpace(id))
      ScreenService.AssertApiEnabled(id);
    if (!string.IsNullOrEmpty(id))
      id = id.Replace(".", "");
    SYMapping syMapping;
    if (!string.IsNullOrEmpty(id))
      syMapping = (SYMapping) PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.name, Equal<Required<SYMapping.name>>, And<SYMapping.screenID, Equal<Required<SYMapping.screenID>>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, (object) scenario, (object) id);
    else
      syMapping = (SYMapping) PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.name, Equal<Required<SYMapping.name>>>>.Config>.SelectWindowed(new PXGraph(), 0, 1, (object) scenario);
    List<PXResult<SYMappingField>> source = syMapping != null ? PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMappingField.mappingID>>, And<SYMappingField.isActive, Equal<PX.Data.True>>>, OrderBy<Asc<SYMappingField.orderNumber>>>.Config>.Select(new PXGraph(), (object) syMapping.MappingID).ToList<PXResult<SYMappingField>>() : throw new PXArgumentException("name", "An invalid argument has been specified.");
    List<Command> list = source.Select<PXResult<SYMappingField>, Command>((Func<PXResult<SYMappingField>, Command>) (c => ScreenUtils.ConvertCommand((SYMappingField) c))).ToList<Command>();
    int num = 0;
    int index = 0;
    while (index < list.Count - 1)
    {
      short? lineNbr = ((SYMappingField) source[index + num + 1]).LineNbr;
      int? nullable1 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
      short? parentLineNbr1 = ((SYMappingField) source[index + num]).ParentLineNbr;
      int? nullable2 = parentLineNbr1.HasValue ? new int?((int) parentLineNbr1.GetValueOrDefault()) : new int?();
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        if (((SYMappingField) source[index + num + 1]).ParentLineNbr.HasValue)
        {
          short? parentLineNbr2 = ((SYMappingField) source[index + num + 1]).ParentLineNbr;
          int? nullable3 = parentLineNbr2.HasValue ? new int?((int) parentLineNbr2.GetValueOrDefault()) : new int?();
          short? parentLineNbr3 = ((SYMappingField) source[index + num]).ParentLineNbr;
          int? nullable4 = parentLineNbr3.HasValue ? new int?((int) parentLineNbr3.GetValueOrDefault()) : new int?();
          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
            goto label_13;
        }
        ++index;
        continue;
      }
label_13:
      list[index + 1].LinkedCommand = list[index];
      list.RemoveAt(index);
      ++num;
    }
    return (IEnumerable<Command>) list;
  }

  public void SetSerializationDelegate(
    Func<PXSYRow, IList<string>, string[]> serializationDelegate)
  {
    this._serializationDelegate = serializationDelegate;
  }

  protected virtual Func<string, (PXSiteMap.ScreenInfo screenInfo, string errorMessage)> GetScreenInfoFunc(
    bool isMobileApp)
  {
    Exception error;
    return (Func<string, (PXSiteMap.ScreenInfo, string)>) (screenId => (isMobileApp ? this._screenInfoProvider.TryGetWithInvariantLocale(screenId, out error) : this._screenInfoProvider.TryGet(screenId, out error), error?.Message));
  }

  private PXSiteMap.ScreenInfo GetScreenInfo(string screenId)
  {
    return this.GetScreenInfoFunc(true)(screenId).Item1;
  }
}
