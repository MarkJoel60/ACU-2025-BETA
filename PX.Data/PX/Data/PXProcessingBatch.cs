// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingBatch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

internal static class PXProcessingBatch
{
  internal static Task<T> Queue<T>(
    Action<System.Action<CancellationToken>, CancellationToken> runner,
    Func<CancellationToken, T> processor,
    CancellationToken externalCancellationToken)
  {
    TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
    runner((System.Action<CancellationToken>) (cancellationToken =>
    {
      try
      {
        taskCompletionSource.SetResult(processor(cancellationToken));
      }
      catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
      {
        taskCompletionSource.TrySetCanceled(cancellationToken);
        throw;
      }
      catch (ThreadAbortException ex)
      {
        taskCompletionSource.SetCanceled();
        throw;
      }
      catch (Exception ex)
      {
        taskCompletionSource.SetException(ex);
      }
    }), externalCancellationToken);
    return taskCompletionSource.Task;
  }
}
