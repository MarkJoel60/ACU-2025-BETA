// Decompiled with JetBrains decompiler
// Type: PX.Common.ThreadCop
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;

#nullable disable
namespace PX.Common;

[Obsolete("This won't work in .net core. Try to rewrite your code for cooperative cancellation.")]
public class ThreadCop
{
  private int \u0002 = 60000;
  private static ThreadCop \u000E;

  private ThreadCop()
  {
  }

  public static T PerformWithTimeout<T>(
    TimeSpan timeout,
    Func<T> action,
    Func<TimeSpan, Exception> exceptionForTimeout)
  {
    ThreadCop.\u000E<T> obj = new ThreadCop.\u000E<T>();
    obj.\u000E = action;
    if (obj.\u000E == null)
      throw new ArgumentNullException(nameof (action));
    if (exceptionForTimeout == null)
      throw new ArgumentNullException(nameof (exceptionForTimeout));
    obj.\u0002 = default (T);
    return new ThreadCop.\u0006(new Action(obj.\u0002)).\u0002(timeout) ? obj.\u0002 : throw exceptionForTimeout(timeout);
  }

  public static void PerformWithTimeout(
    TimeSpan timeout,
    Action action,
    Func<TimeSpan, Exception> exceptionForTimeout)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    if (exceptionForTimeout == null)
      throw new ArgumentNullException(nameof (exceptionForTimeout));
    if (!new ThreadCop.\u0006(action).\u0002(timeout))
      throw exceptionForTimeout(timeout);
  }

  public static bool PerformWithTimeout(TimeSpan timeout, Action action)
  {
    return action != null ? new ThreadCop.\u0006(action).\u0002(timeout) : throw new ArgumentNullException(nameof (action));
  }

  [Obsolete("Use PerformWithTimeout.")]
  public static int TimeOut
  {
    get => ThreadCop.\u0002().\u0002;
    set
    {
      ThreadCop.\u0002().\u0002 = value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof (value), "Timeout cannot be less than one millisecond.");
    }
  }

  private static ThreadCop \u0002() => ThreadCop.\u000E ?? (ThreadCop.\u000E = new ThreadCop());

  [Obsolete("Use PerformWithTimeout.")]
  public static bool PerformWork(Action del)
  {
    if (del == null)
      throw new ArgumentNullException(nameof (del));
    int num = ThreadCop.\u0002().\u0002;
    return new ThreadCop.\u0006(del).\u0002(num);
  }

  [Obsolete("Use PerformWithTimeout.")]
  public static void AddWorkToPerform(Action del)
  {
    ThreadCop.\u0002 obj = new ThreadCop.\u0002();
    obj.\u0002 = del;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (del));
    ThreadPool.QueueUserWorkItem(new WaitCallback(obj.\u0002));
  }

  private sealed class \u0002
  {
    public Action \u0002;

    internal void \u0002(object _param1)
    {
      try
      {
        ThreadCop.PerformWork(this.\u0002);
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch
      {
      }
    }
  }

  private sealed class \u0006
  {
    private readonly Action \u0002;
    private readonly EventWaitHandle \u000E = (EventWaitHandle) new AutoResetEvent(false);
    private Thread \u0006;
    private bool \u0008;
    private Exception \u0003;
    private readonly PXSessionContext \u000F;

    public \u0006(Action _param1)
    {
      this.\u0002 = _param1 ?? throw new ArgumentNullException("del");
      this.\u000F = PXContext.PXIdentity.Clone();
    }

    public bool \u0002(int _param1) => this.\u0002(TimeSpan.FromMilliseconds((double) _param1));

    public bool \u0002(TimeSpan _param1)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.\u0002));
      bool flag = this.\u000E.WaitOne(_param1);
      this.\u0006?.Abort();
      if (this.\u0003 != null)
        ExceptionDispatchInfo.Capture(this.\u0003).Throw();
      return !this.\u0008 & flag;
    }

    private void \u0002(object _param1)
    {
      using (IDisposableSlotStorageProvider slotStorageProvider = SlotStore.AsyncLocal())
      {
        PXSessionContextFactory.InitializeContext((ISlotStore) slotStorageProvider, this.\u000F);
        this.\u0006 = Thread.CurrentThread;
        try
        {
          this.\u0002();
          this.\u0006 = (Thread) null;
        }
        catch (OutOfMemoryException ex)
        {
          throw;
        }
        catch (ThreadAbortException ex)
        {
          this.\u0008 = true;
          Thread.ResetAbort();
        }
        catch (Exception ex)
        {
          this.\u0003 = ex;
        }
        finally
        {
          this.\u0006 = (Thread) null;
          this.\u000E.Set();
        }
      }
    }
  }

  private sealed class \u000E<\u0002>
  {
    public \u0002 \u0002;
    public Func<\u0002> \u000E;

    internal void \u0002() => this.\u0002 = this.\u000E();
  }
}
