// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXThreadPool
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace PX.Async.Internal;

internal sealed class PXThreadPool : IDisposable
{
  private readonly List<PXThreadPool.RequestItem> _requests;
  private readonly List<Thread> _workers;
  private List<Thread> _threads;
  private readonly object _lockQueues;
  private int _availThreads;

  public PXThreadPool(IOptions<ThreadPoolOptions> options)
    : this(options.Value.ThreadPoolSize)
  {
  }

  public PXThreadPool(int numThreads)
  {
    if (numThreads <= 0)
      throw new PXArgumentException(nameof (numThreads), "The argument is out of range.");
    this._requests = new List<PXThreadPool.RequestItem>();
    this._workers = new List<Thread>();
    this._threads = new List<Thread>();
    this._availThreads = numThreads;
    this._lockQueues = new object();
  }

  public void QueueUserWorkItem(System.Action<PXLongOperationPars> callback, PXLongOperationPars state)
  {
    if (this._threads == null)
      throw new PXObjectDisposedException(this.GetType().Name);
    PXThreadPool.RequestItem requestItem = callback != null ? new PXThreadPool.RequestItem(callback, state) : throw new PXArgumentException(nameof (callback), "The argument cannot be null.");
    bool flag = false;
    lock (this._lockQueues)
    {
      this._requests.Add(requestItem);
      if (this._workers.Count > 0)
      {
        this._workers[0].Interrupt();
        this._workers.RemoveAt(0);
      }
      else
      {
        flag = this._availThreads > 0;
        if (flag)
          --this._availThreads;
      }
    }
    if (!flag)
      return;
    Thread thread = new Thread(new ThreadStart(this.Run));
    thread.IsBackground = true;
    PXThreadPool.SetThreadPriority(thread);
    lock (this._lockQueues)
      this._threads.Add(thread);
    thread.Start();
  }

  private static void SetThreadPriority(Thread thread)
  {
    if (!WebConfig.LongOperationLowPriority)
      return;
    thread.Priority = ThreadPriority.BelowNormal;
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  private void RunItem(PXThreadPool.RequestItem item)
  {
    PXThreadPool.SetThreadPriority(Thread.CurrentThread);
    ExecutionContext.Run(item.Context, new ContextCallback(item.Callback), (object) null);
    item.Close();
  }

  private void Run()
  {
    try
    {
      try
      {
        while (true)
        {
          PXThreadPool.RequestItem requestItem;
          lock (this._lockQueues)
          {
            if (this._requests.Count > 0)
            {
              requestItem = this._requests[0];
              this._requests.RemoveAt(0);
            }
            else
            {
              requestItem = (PXThreadPool.RequestItem) null;
              this._workers.Insert(0, Thread.CurrentThread);
            }
          }
          if (requestItem == null)
          {
            try
            {
              Thread.Sleep(300000);
            }
            catch (ThreadInterruptedException ex)
            {
            }
            lock (this._lockQueues)
            {
              this._workers.Remove(Thread.CurrentThread);
              if (this._requests.Count <= 0)
                break;
              requestItem = this._requests[0];
              this._requests.RemoveAt(0);
            }
          }
          this.RunItem(requestItem);
        }
      }
      finally
      {
        lock (this._lockQueues)
        {
          int index = this._threads.IndexOf(Thread.CurrentThread);
          if (index >= 0)
            this._threads.RemoveAt(index);
          this._workers.Remove(Thread.CurrentThread);
          ++this._availThreads;
        }
      }
    }
    catch
    {
    }
  }

  public void Dispose()
  {
    if (this._threads == null)
      return;
    lock (this._lockQueues)
    {
      this._threads.ForEach((System.Action<Thread>) (t => t.Interrupt()));
      this._threads = (List<Thread>) null;
    }
  }

  private class RequestItem
  {
    private System.Action<PXLongOperationPars> _callback;
    private PXLongOperationPars _state;
    private ExecutionContext _context;

    public RequestItem(System.Action<PXLongOperationPars> callback, PXLongOperationPars state)
    {
      this._callback = callback;
      this._state = state;
      this._context = ExecutionContext.Capture();
    }

    public ExecutionContext Context => this._context;

    public void Callback(object _) => this._callback(this._state);

    public void Close()
    {
      this._callback = (System.Action<PXLongOperationPars>) null;
      this._state = (PXLongOperationPars) null;
      this._context = (ExecutionContext) null;
    }
  }
}
