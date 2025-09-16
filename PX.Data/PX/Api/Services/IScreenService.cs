// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.IScreenService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.Api.Mobile;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Services;

[PXInternalUseOnly]
public interface IScreenService
{
  Content GetSchema(string id, SchemaMode mode);

  void Set(string id, Content schemaContent);

  void Clear(string id);

  IEnumerable<Content> Submit(string id, IEnumerable<Command> commands, SchemaMode mode);

  IEnumerable<Content> Submit(
    string id,
    IEnumerable<Command> commands,
    SchemaMode mode,
    bool mobile,
    ref PXGraph forceGraph,
    ref string redirectContainerView,
    ref string redirectScreen,
    Dictionary<string, PXFilterRow[]> viewFilters,
    IGraphHelper graphHelper = null);

  IEnumerable<ImportResult> Import(
    string id,
    Command[] commands,
    PX.Api.Models.Filter[] filters,
    string[][] data,
    bool includeHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget);

  string[][] Export(
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
    bool disableOptimizedExport = false);

  ProcessResult GetProcessStatus(string ids);

  ProcessResult GetProcessStatus(string ids, PXGraph targetGraph);

  ProcessResult GetProcessStatus(string ids, PXGraph targetGraph, out Exception exception);

  void SetMode(SchemaMode mode);

  IEnumerable<Command> GetScenario(string id, string scenario);

  string ExtractId(string id);

  string ExtractViewName(string viewName);

  ProcessResult GetProcessStatus(object key, out Exception exception);

  void AbortProcess(string id);

  void AbortProcess(object key);

  void SetSerializationDelegate(
    Func<PXSYRow, IList<string>, string[]> serializationDelegate);

  Exception WaitLongOperation(string screenId, PXGraph targetGraph);

  void FillGraphFromOperationContext(PXGraph targetGraph, string screenId);
}
