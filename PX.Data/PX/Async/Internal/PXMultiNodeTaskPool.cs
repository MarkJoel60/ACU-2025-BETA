// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXMultiNodeTaskPool
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Session;
using PX.Data;
using PX.Distributed.Messaging;
using PX.Distributed.Stores;
using PX.SM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Async.Internal;

internal sealed class PXMultiNodeTaskPool(
  ILogger logger,
  IApplicationStore<PXLongOperationState> sharedStore,
  IPXSyncMessageBus messageBus) : PXTaskPool
{
  private const string TASK_TO_SESSION_BIND = "PendingSharedSessionKey";
  private Timer _sharedStoreUpdate;
  private static bool IsRecursive = false;
  private static readonly SessionKey<HashSet<string>> ActiveTasksSessionKey = new SessionKey<HashSet<string>>("PXTaskPool.ActiveTask");

  protected override void FreeLockObject(string key)
  {
    base.FreeLockObject(key);
    this.ScheduleSharedStoreUpdate(key);
  }

  internal override bool TryGetValue(string key, out PXLongOperationState state)
  {
    int num = HttpContext.Current != null ? 1 : 0;
    if (!this.TryGetLocal(key, out state))
      state = PXMultiNodeTaskPool.ContextStateSlot.Get(key) ?? sharedStore.Get(key);
    if (num != 0)
      PXMultiNodeTaskPool.ContextStateSlot.Set(key, state);
    return state != null;
  }

  internal override bool Exists(string key) => !this.IsNonExistingTask(key) && base.Exists(key);

  protected override void Add(string key, PXLongOperationState state)
  {
    base.Add(key, state);
    this.BindTaskToUserSession(key);
    this.ScheduleSharedStoreUpdate(key);
    using (PXAccess.GetAdminScope())
    {
      SMLongOperationMaint longOperationMaint = new SMLongOperationMaint();
      longOperationMaint.Operations.Update(new SMLongOperation()
      {
        OperationKey = key,
        WebsiteID = WebsiteID.Key,
        ScreenID = state.ScreenID
      });
      longOperationMaint.Persist();
    }
  }

  private (System.Action<HashSet<string>> setToSession, HashSet<string> tasks) GetTasksInUserSession()
  {
    IPXSessionState session = HttpContextPXSessionState.TryGet(HttpContext.Current);
    return session == null ? ((System.Action<HashSet<string>>) null, (HashSet<string>) null) : ((System.Action<HashSet<string>>) (tasks => session.Set<HashSet<string>>(PXMultiNodeTaskPool.ActiveTasksSessionKey, tasks)), session.Get<HashSet<string>>(PXMultiNodeTaskPool.ActiveTasksSessionKey));
  }

  internal override void BindTaskToUserSession(string key)
  {
    (System.Action<HashSet<string>> setToSession, HashSet<string> tasks) tasksInUserSession = this.GetTasksInUserSession();
    System.Action<HashSet<string>> setToSession = tasksInUserSession.setToSession;
    if (setToSession == null)
      return;
    HashSet<string> tasks = tasksInUserSession.tasks;
    if (tasks == null)
      setToSession(new HashSet<string>() { key });
    else
      tasks.Add(key);
  }

  private bool IsNonExistingTask(string key)
  {
    (System.Action<HashSet<string>> setToSession, HashSet<string> tasks) tasksInUserSession = this.GetTasksInUserSession();
    bool flag;
    if (tasksInUserSession.setToSession != null)
    {
      HashSet<string> tasks = tasksInUserSession.tasks;
      flag = tasks == null || !tasks.Contains(key);
    }
    else
      flag = false;
    return flag;
  }

  protected override void Remove(string key, Guid instanceId)
  {
    this.GetTasksInUserSession().tasks?.Remove(key);
    int num = this.RemoveLocalAndDeleteFromDb(key) ? 1 : 0;
    if (num != 0)
      logger.Verbose<string>("Long operation {LongOperationId} removed from local store", key);
    if (this.NoLocalItems)
      SMLongOperationMaint.ClearOperationsForThisWebsite();
    logger.Verbose<string>("Removing long operation {LongOperationId} from shared store", key);
    sharedStore.Remove(key);
    PXMultiNodeTaskPool.ContextStateSlot.Clear(key);
    if (num != 0)
      return;
    logger.Verbose<string, string>("Long operation {LongOperationId} not found locally, sending {Message}", key, typeof (RemoveLongOperationMessage).FullName);
    messageBus.Publish<RemoveLongOperationMessage>(new RemoveLongOperationMessage(key, instanceId));
  }

  private void UpdateDirtyItems()
  {
    foreach (string localTaskKey in this.GetLocalTaskKeys())
    {
      using (this.GetLock(localTaskKey))
      {
        PXLongOperationState state;
        if (this.TryGetLocal(localTaskKey, out state))
        {
          if (state.IsDirty)
            this.Flush(state);
        }
      }
    }
  }

  private void ScheduleSharedStoreUpdate(string key)
  {
    if (!this.TryGetLocal(key, out PXLongOperationState _))
      return;
    if (HttpContext.Current != null)
    {
      if (!(HttpContext.Current.Items[(object) "PendingSharedSessionKey"] is HashSet<string> stringSet))
        HttpContext.Current.Items[(object) "PendingSharedSessionKey"] = (object) (stringSet = new HashSet<string>());
      stringSet.Add(key);
    }
    else
    {
      if (this._sharedStoreUpdate != null)
        return;
      this._sharedStoreUpdate = new Timer((TimerCallback) (_param1 =>
      {
        if (PXMultiNodeTaskPool.IsRecursive)
          return;
        try
        {
          PXMultiNodeTaskPool.IsRecursive = true;
          this.UpdateDirtyItems();
        }
        catch
        {
        }
        finally
        {
          PXMultiNodeTaskPool.IsRecursive = false;
        }
      }), (object) null, new TimeSpan(0, 0, 2), new TimeSpan(0, 0, 2));
    }
  }

  internal override void Flush(PXLongOperationState item)
  {
    if (!item.IsDirty || item.IsNotStarted)
      return;
    sharedStore.Set(item.Key.String, item);
    item.WasFlushed = true;
    item.IsDirty = false;
  }

  internal override void Abort(string key)
  {
    if (this.AbortLocalOperation(key))
      return;
    logger.Verbose<string, string>("Long operation {LongOperationId} not found locally, sending {Message}", key, typeof (AbortLongOperationMessage).FullName);
    messageBus.Publish<AbortLongOperationMessage>(new AbortLongOperationMessage(key));
    PXMultiNodeTaskPool.ContextStateSlot.Clear(key);
  }

  internal override void FlushState()
  {
    if (!(HttpContext.Current.Items[(object) "PendingSharedSessionKey"] is HashSet<string> stringSet))
      return;
    foreach (string key in stringSet)
    {
      using (this.GetLock(key))
      {
        PXLongOperationState state;
        if (this.TryGetLocal(key, out state))
          this.Flush(state);
      }
    }
  }

  internal override T TryReadFrom<T>(string key, Func<PXLongOperationState, T> reader)
  {
    return this.IsNonExistingTask(key) ? default (T) : base.TryReadFrom<T>(key, reader);
  }

  internal override void TryWriteTo(string key, System.Action<PXLongOperationState> writer)
  {
    if (this.IsNonExistingTask(key))
      return;
    base.TryWriteTo(key, writer);
  }

  internal override IEnumerable<RowTaskInfo> GetTaskList(string key, string screenID)
  {
    IEnumerable<SMLongOperation> firstTableItems;
    using (PXAccess.GetAdminScope())
      firstTableItems = new SMLongOperationMaint().Operations.Select().FirstTableItems;
    foreach (SMLongOperation smLongOperation in firstTableItems)
    {
      if ((key == null || !(smLongOperation.OperationKey != key)) && (screenID == null || !(smLongOperation.ScreenID != screenID)))
      {
        PXLongOperationState longOperationState = (PXLongOperationState) null;
        try
        {
          longOperationState = sharedStore.Get(smLongOperation.OperationKey);
        }
        catch
        {
        }
        if (longOperationState != null && !longOperationState.IsCompleted)
        {
          TimeSpan timeSpan = System.DateTime.Now - longOperationState.StartStamp;
          PXProgress pxProgress = longOperationState.GetProgress() ?? new PXProgress();
          yield return new RowTaskInfo()
          {
            OperationKey = longOperationState.Key,
            Key = longOperationState.Key.String,
            WorkTime = timeSpan.ToString("hh\\:mm\\:ss"),
            User = longOperationState.UserName,
            Screen = longOperationState.ScreenID,
            Processed = pxProgress.Current,
            Total = pxProgress.Total,
            Errors = pxProgress.Errors
          };
        }
      }
    }
  }

  internal override void Complete(PXLongOperationState item)
  {
    this.RemoveLocalAndDeleteFromDb(item.Key.String);
  }

  private bool RemoveLocalAndDeleteFromDb(string key)
  {
    int num = this.TryRemoveLocal(key) ? 1 : 0;
    SMLongOperationMaint.DeleteOperationByKey((object) key);
    return num != 0;
  }

  protected override Task HandleAbortMessageAsync(AbortLongOperationMessage message)
  {
    logger.Verbose<string, string>("Got {Message} for long operation {LongOperationId}...", typeof (AbortLongOperationMessage).FullName, message.OperationKey);
    if (this.AbortLocalOperation(message.OperationKey))
      logger.Verbose<string>("Long operation {LongOperationId} aborted", message.OperationKey);
    else
      logger.Verbose<string>("Long operation {LongOperationId} not found locally", message.OperationKey);
    return Task.CompletedTask;
  }

  protected override Task HandleRemoveMessageAsync(RemoveLongOperationMessage message)
  {
    string operationKey = message.OperationKey;
    logger.Verbose<string, string, Guid>("Got {Message} for long operation {LongOperationId}({LongOperationInstanceId})...", typeof (RemoveLongOperationMessage).FullName, operationKey, message.OperationInstanceId);
    using (this.GetLock(operationKey))
    {
      PXLongOperationState state;
      if (!this.TryGetLocal(operationKey, out state))
      {
        logger.Verbose<string>("Long operation {LongOperationId} not found locally", operationKey);
        return Task.CompletedTask;
      }
      if (state.InstanceId != message.OperationInstanceId)
      {
        logger.Verbose<string, Guid>("Local long operation {LongOperationId} has different instance id {LongOperationInstanceId} (possible concurrency), skipping", operationKey, state.InstanceId);
        return Task.CompletedTask;
      }
      if (this.TryRemoveLocal(operationKey))
        logger.Verbose<string>("Long operation {LongOperationId} removed from local store", operationKey);
      else
        logger.Warning<string>("Long operation {LongOperationId} not found locally while removing, check for concurrency issues", operationKey);
      return Task.CompletedTask;
    }
  }

  private static class ContextStateSlot
  {
    private static string GetLongOpSlotKey(string key) => "LongOp+" + key;

    internal static PXLongOperationState Get(string key)
    {
      return PXContext.GetSlot<PXLongOperationState>(PXMultiNodeTaskPool.ContextStateSlot.GetLongOpSlotKey(key));
    }

    internal static void Set(string key, PXLongOperationState state)
    {
      PXContext.SetSlot<PXLongOperationState>(PXMultiNodeTaskPool.ContextStateSlot.GetLongOpSlotKey(key), state);
    }

    internal static void Clear(string key)
    {
      PXMultiNodeTaskPool.ContextStateSlot.Set(key, (PXLongOperationState) null);
    }
  }
}
