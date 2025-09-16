// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXTaskPool
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Distributed.Messaging;
using PX.SM;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Async.Internal;

internal abstract class PXTaskPool : 
  IPXMessageHandler<AbortLongOperationMessage>,
  IPXMessageHandler<RemoveLongOperationMessage>
{
  private readonly ConcurrentDictionary<string, PXLongOperationState> LocalItems = new ConcurrentDictionary<string, PXLongOperationState>();
  private readonly Dictionary<string, PXTaskPool.TaskLock> TaskLocks = new Dictionary<string, PXTaskPool.TaskLock>();
  private const string LongOperationKey = "PXLongOperation.Key";

  protected bool NoLocalItems => this.LocalItems.IsEmpty;

  protected virtual void FreeLockObject(string key)
  {
    lock (this.TaskLocks)
      this.TaskLocks.Remove(key);
  }

  internal IDisposable GetLock(string key)
  {
    PXTaskPool.TaskLock taskLock;
    lock (this.TaskLocks)
    {
      if (!this.TaskLocks.TryGetValue(key, out taskLock))
      {
        taskLock = new PXTaskPool.TaskLock(this, key);
        this.TaskLocks.Add(key, taskLock);
      }
      taskLock.AddRef();
    }
    Monitor.Enter((object) taskLock);
    return (IDisposable) taskLock;
  }

  internal abstract bool TryGetValue(string key, out PXLongOperationState state);

  internal virtual bool Exists(string key) => this.TryGetValue(key, out PXLongOperationState _);

  internal void Add(PXLongOperationState state)
  {
    if (state == null)
      throw new ArgumentNullException(nameof (state));
    this.Add(state.Key.String, state);
  }

  protected virtual void Add(string key, PXLongOperationState state)
  {
    PXLongOperationState state1;
    if (this.TryGetLocal(key, out state1) && !state1.IsCompleted)
      throw new ArgumentOutOfRangeException(nameof (key), (object) key, "Operation with the key already exists.");
    this.LocalItems[key] = state;
  }

  internal abstract void BindTaskToUserSession(string key);

  internal PXLongOperationState GetContextTask(ISlotStore slots)
  {
    string key = slots.Get<string>("PXLongOperation.Key");
    PXLongOperationState state;
    return key == null || !this.TryGetLocal(key, out state) ? (PXLongOperationState) null : state;
  }

  internal abstract void Abort(string key);

  internal void Remove(PXLongOperationState item) => this.Remove(item.Key.String, item.InstanceId);

  protected abstract void Remove(string key, Guid instanceId);

  internal void BindAmbientContext(ISlotStore slots, PXLongOperationState item)
  {
    slots.Set("PXLongOperation.Key", (object) item.Key.String);
  }

  internal void UnbindAmbientContext(ISlotStore slots) => slots.Remove("PXLongOperation.Key");

  internal abstract void FlushState();

  protected bool AbortLocalOperation(string key)
  {
    PXLongOperationState state;
    using (this.GetLock(key))
    {
      if (!this.TryGetLocal(key, out state))
        return false;
      if (state.IsCompleted)
        return true;
    }
    state.Cancel();
    return true;
  }

  protected IEnumerable<string> GetLocalTaskKeys() => (IEnumerable<string>) this.LocalItems.Keys;

  internal IEnumerable<PXLongOperationState> GetLocalTasks()
  {
    return ConcurrentDictionaryExtensions.ValuesExt<string, PXLongOperationState>(this.LocalItems);
  }

  internal bool TryGetLocal(string key, out PXLongOperationState state)
  {
    return this.LocalItems.TryGetValue(key, out state);
  }

  protected bool TryRemoveLocal(string key)
  {
    PXLongOperationState longOperationState;
    if (!this.LocalItems.TryRemove(key, out longOperationState))
      return false;
    longOperationState.MarkAsRemoved();
    return true;
  }

  internal void CleanupExpiredLocal(int timeoutMinutes)
  {
    foreach (string localTaskKey in this.GetLocalTaskKeys())
    {
      using (this.GetLock(localTaskKey))
      {
        PXLongOperationState state;
        if (this.TryGetLocal(localTaskKey, out state))
        {
          if (state.IsCompleted)
          {
            DateTime? endStamp = state.EndStamp;
            if (endStamp.HasValue)
            {
              DateTime now = DateTime.Now;
              ref DateTime local = ref now;
              endStamp = state.EndStamp;
              DateTime dateTime = endStamp.Value;
              if (local.Subtract(dateTime).TotalMinutes >= (double) timeoutMinutes)
                this.TryRemoveLocal(localTaskKey);
            }
          }
        }
      }
    }
  }

  internal abstract void Flush(PXLongOperationState value);

  internal virtual T TryReadFrom<T>(string key, Func<PXLongOperationState, T> reader)
  {
    using (this.GetLock(key))
    {
      PXLongOperationState state;
      return !this.TryGetValue(key, out state) ? default (T) : reader(state);
    }
  }

  internal virtual void TryWriteTo(string key, Action<PXLongOperationState> writer)
  {
    using (this.GetLock(key))
    {
      PXLongOperationState state;
      if (!this.TryGetValue(key, out state))
        return;
      writer(state);
    }
  }

  internal abstract IEnumerable<RowTaskInfo> GetTaskList(string key, string screenID);

  internal abstract void Complete(PXLongOperationState item);

  Task IPXMessageHandler<AbortLongOperationMessage>.HandleAsync(AbortLongOperationMessage message)
  {
    return this.HandleAbortMessageAsync(message);
  }

  Task IPXMessageHandler<RemoveLongOperationMessage>.HandleAsync(RemoveLongOperationMessage message)
  {
    return this.HandleRemoveMessageAsync(message);
  }

  protected abstract Task HandleAbortMessageAsync(AbortLongOperationMessage message);

  protected abstract Task HandleRemoveMessageAsync(RemoveLongOperationMessage message);

  private class TaskLock(PXTaskPool owner, string key) : IDisposable
  {
    private int refCnt;

    public void AddRef() => ++this.refCnt;

    public void Dispose()
    {
      Monitor.Exit((object) this);
      --this.refCnt;
      if (this.refCnt != 0)
        return;
      owner.FreeLockObject(key);
    }
  }
}
