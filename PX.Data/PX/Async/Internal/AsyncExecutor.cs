// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.AsyncExecutor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Reports;
using System;

#nullable enable
namespace PX.Async.Internal;

internal sealed class AsyncExecutor(LongOperationManager managerImplementation) : IAsyncExecutor
{
  private readonly ILongOperationManager _manager = (ILongOperationManager) managerImplementation;

  TResult? IAsyncExecutor.Execute<TResult>(
    string uniqueKey,
    Func<TResult> method,
    long waitTimeout)
  {
    return managerImplementation.AsyncExecute<TResult>(uniqueKey, method, waitTimeout);
  }

  void IAsyncExecutor.Abort(string key) => managerImplementation.AsyncAbort((object) key);

  void IAsyncExecutor.ClearStatus(string key)
  {
    managerImplementation.ForceClearStatus((object) key);
  }

  object? IAsyncExecutor.GetCustomInfo(string key) => this._manager.GetCustomInfoFor((object) key);
}
