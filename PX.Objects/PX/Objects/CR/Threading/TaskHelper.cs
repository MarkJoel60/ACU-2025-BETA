// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Threading.TaskHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Async;
using PX.Common;
using PX.Data;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.CR.Threading;

[PXInternalUseOnly]
public static class TaskHelper
{
  public static 
  #nullable disable
  T RunSynchronously<T>(
    this ILongOperationManager longOperationManager,
    Func<CancellationToken, Task<T>> factory,
    bool forceRethrow = false)
  {
    Guid guid = Guid.NewGuid();
    T result = default (T);
    Exception exception = (Exception) null;
    longOperationManager.StartAsyncOperation((object) guid, (Func<CancellationToken, Task>) (async ct =>
    {
      try
      {
        result = await factory(ct);
      }
      catch (Exception ex)
      {
        exception = ex;
        throw;
      }
    }));
    longOperationManager.WaitCompletion((object) guid);
    longOperationManager.ClearStatus((object) guid);
    if (exception != null & forceRethrow)
      ExceptionDispatchInfo.Capture(exception).Throw();
    return result;
  }

  public static void RunSynchronously(
    this ILongOperationManager longOperationManager,
    Func<CancellationToken, Task> factory,
    bool forceRethrow = false)
  {
    longOperationManager.RunSynchronously<bool>((Func<CancellationToken, Task<bool>>) (async ct =>
    {
      await factory(ct);
      return true;
    }), forceRethrow);
  }

  [Obsolete]
  public static T RunSynchronously<T>(Func<Task<T>> factory)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TaskHelper.\u003C\u003Ec__DisplayClass2_0<T> cDisplayClass20 = new TaskHelper.\u003C\u003Ec__DisplayClass2_0<T>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.factory = factory;
    try
    {
      if (PXLongOperation.IsLongOperationContext())
      {
        // ISSUE: reference to a compiler-generated field
        return cDisplayClass20.factory().Result;
      }
      string str = Guid.NewGuid().ToString();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.result = default (T);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.ex = (Exception) null;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((object) str, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CRunSynchronously\u003Eb__0)));
      PXLongOperation.WaitCompletion((object) str);
      PXLongOperation.ClearStatus((object) str);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass20.ex != null)
      {
        // ISSUE: reference to a compiler-generated field
        ExceptionDispatchInfo.Capture(cDisplayClass20.ex).Throw();
      }
      // ISSUE: reference to a compiler-generated field
      return cDisplayClass20.result;
    }
    catch (AggregateException ex)
    {
      TaskHelper.RethrowInnerPXException(ex);
      return default (T);
    }
  }

  [Obsolete]
  public static void RunSynchronously(Func<Task> factory)
  {
    TaskHelper.RunSynchronously<bool>((Func<Task<bool>>) (() => factory().ContinueWith<bool>((Func<Task, bool>) (t => true), TaskContinuationOptions.OnlyOnRanToCompletion)));
  }

  [Obsolete]
  private static void RethrowInnerPXException(AggregateException ae)
  {
    Exception source = (Exception) ae;
    if (ae.InnerException is PXException innerException)
      source = (Exception) innerException;
    ExceptionDispatchInfo.Capture(source).Throw();
  }
}
