// Decompiled with JetBrains decompiler
// Type: PX.Async.GraphLongOperationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Async;

internal class GraphLongOperationManager : IGraphLongOperationManager, ILongOperationManager
{
  private readonly ILongOperationManager _longOperationManager;
  private readonly PXGraph _graph;

  public GraphLongOperationManager(ILongOperationManager longOperationManager, PXGraph graph)
  {
    this._longOperationManager = longOperationManager;
    this._graph = graph;
  }

  private object Key => this._graph.UID;

  void IGraphLongOperationManager.StartOperation(System.Action<CancellationToken>? method)
  {
    this._longOperationManager.StartOperation(this._graph, method);
  }

  void IGraphLongOperationManager.StartAsyncOperation(Func<CancellationToken, Task> method)
  {
    this._longOperationManager.StartAsyncOperation(this._graph, method);
  }

  LongOperationDetails IGraphLongOperationManager.GetOperationDetails()
  {
    return this._longOperationManager.GetOperationDetails(this.Key);
  }

  bool IGraphLongOperationManager.WaitCompletion()
  {
    return this._longOperationManager.WaitCompletion(this.Key);
  }

  void IGraphLongOperationManager.ClearStatus() => this._longOperationManager.ClearStatus(this.Key);

  object? IGraphLongOperationManager.GetCustomInfoForThis(
    string? customInfoKey,
    out object[]? processingList)
  {
    return this._longOperationManager.GetCustomInfoFor(this.Key, customInfoKey, out processingList);
  }

  void ILongOperationManager.StartOperation(PXGraph graph, System.Action<CancellationToken>? method)
  {
    this._longOperationManager.StartOperation(graph, method);
  }

  void ILongOperationManager.StartOperation(object? key, System.Action<CancellationToken>? method)
  {
    this._longOperationManager.StartOperation(key, method);
  }

  void ILongOperationManager.StartAsyncOperation(
    PXGraph graph,
    Func<CancellationToken, Task> method)
  {
    this._longOperationManager.StartAsyncOperation(graph, method);
  }

  void ILongOperationManager.StartAsyncOperation(object? key, Func<CancellationToken, Task> method)
  {
    this._longOperationManager.StartAsyncOperation(key, method);
  }

  LongOperationDetails ILongOperationManager.GetOperationDetails(object key)
  {
    return this._longOperationManager.GetOperationDetails(key);
  }

  bool ILongOperationManager.WaitCompletion(object key)
  {
    return this._longOperationManager.WaitCompletion(key);
  }

  void ILongOperationManager.ClearStatus(object key) => this._longOperationManager.ClearStatus(key);

  object? ILongOperationManager.GetCustomInfo(string? customInfoKey)
  {
    return this._longOperationManager.GetCustomInfo(customInfoKey);
  }

  object? ILongOperationManager.GetCustomInfoFor(
    object? key,
    string? customInfoKey,
    out object[]? processingList)
  {
    return this._longOperationManager.GetCustomInfoFor(key, customInfoKey, out processingList);
  }

  void ILongOperationManager.SetCustomInfo(object? info, string? customInfoKey)
  {
    this._longOperationManager.SetCustomInfo(info, customInfoKey);
  }

  IEnumerable<RowTaskInfo> ILongOperationManager.GetTaskList()
  {
    return this._longOperationManager.GetTaskList();
  }

  bool ILongOperationManager.IsLongOperationContext()
  {
    return this._longOperationManager.IsLongOperationContext();
  }
}
