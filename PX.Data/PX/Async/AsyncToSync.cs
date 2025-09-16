// Decompiled with JetBrains decompiler
// Type: PX.Async.AsyncToSync
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Async;

internal static class AsyncToSync
{
  private static void WaitAndUnwrapException(
    Func<CancellationToken, Task> method,
    CancellationToken cancellationToken)
  {
    try
    {
      method(cancellationToken).Wait(cancellationToken);
    }
    catch (AggregateException ex) when (ex.InnerException != null)
    {
      throw AsyncToSync.PrepareForRethrow(ex.InnerException);
    }
  }

  private static Exception PrepareForRethrow(Exception exception)
  {
    ExceptionDispatchInfo.Capture(exception).Throw();
    return exception;
  }

  internal static Action<CancellationToken> Convert(Func<CancellationToken, Task> method)
  {
    return (Action<CancellationToken>) (cancellationToken => AsyncToSync.WaitAndUnwrapException(method, cancellationToken));
  }
}
