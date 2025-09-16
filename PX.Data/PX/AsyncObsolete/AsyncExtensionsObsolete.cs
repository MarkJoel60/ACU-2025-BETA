// Decompiled with JetBrains decompiler
// Type: PX.AsyncObsolete.AsyncExtensionsObsolete
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.AsyncObsolete;

[Obsolete]
[PXInternalUseOnly]
public static class AsyncExtensionsObsolete
{
  [Obsolete]
  public static void SetAsyncProcessDelegate<TTable>(
    this PXProcessingBase<TTable> processing,
    Func<List<TTable>, CancellationToken, Task> handler)
    where TTable : class, IBqlTable, new()
  {
    processing.SetAsyncProcessDelegate(handler);
  }

  [Obsolete]
  public static void SetAsyncProcessDelegate<TTable>(
    this PXProcessingBase<TTable> processing,
    Func<TTable, CancellationToken, Task> handler)
    where TTable : class, IBqlTable, new()
  {
    processing.SetAsyncProcessDelegate(handler);
  }

  [Obsolete]
  public static void SetAsyncProcessDelegate<TTable, TGraph>(
    this PXProcessingBase<TTable> processing,
    Func<TGraph, TTable, CancellationToken, Task> handler)
    where TTable : class, IBqlTable, new()
    where TGraph : PXGraph, new()
  {
    processing.SetAsyncProcessDelegate<TGraph>(handler);
  }

  [Obsolete]
  public static void SetAsyncProcessDelegate<TTable, TGraph>(
    this PXProcessingBase<TTable> processing,
    Func<TGraph, TTable, CancellationToken, Task> handler,
    Func<TGraph, CancellationToken, Task> handlerFinally)
    where TTable : class, IBqlTable, new()
    where TGraph : PXGraph, new()
  {
    processing.SetAsyncProcessDelegate<TGraph>(handler, handlerFinally);
  }

  [Obsolete]
  public static void StartAsyncOperation(
    this ILongOperationManager manager,
    PXGraph graph,
    Func<CancellationToken, Task> method)
  {
    manager.StartAsyncOperation(graph, method);
  }

  [Obsolete]
  public static void StartAsyncOperation(
    this ILongOperationManager manager,
    object key,
    Func<CancellationToken, Task> method)
  {
    manager.StartAsyncOperation(key, method);
  }

  [Obsolete]
  public static void StartAsyncOperation(
    this IGraphLongOperationManager manager,
    Func<CancellationToken, Task> method)
  {
    manager.StartAsyncOperation(method);
  }
}
