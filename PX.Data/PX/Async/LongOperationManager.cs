// Decompiled with JetBrains decompiler
// Type: PX.Async.LongOperationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Async.Internal;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Async;

internal abstract class LongOperationManager : ILongOperationManager, ILongOperationTaskManager
{
  private static LongOperationManager _instance;
  protected readonly PXTaskPool _Pool;
  private static System.DateTime _lastCheckTime = System.DateTime.Now;
  private static object _lastCheckTimeLock = new object();
  private const string IS_LONG_OPERATION_RUNNING = "IsLongOperationRunning";

  internal static LongOperationManager Instance
  {
    get
    {
      if (LongOperationManager._instance != null)
        return LongOperationManager._instance;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("LongOperationManager should have been initialized by now");
      return ServiceLocator.Current.GetInstance<LongOperationManager>();
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      LongOperationManager._instance = LongOperationManager._instance == null ? value : throw new InvalidOperationException("LongOperationManager has already been initialized");
    }
  }

  protected LongOperationManager(PXTaskPool taskPool) => this._Pool = taskPool;

  void ILongOperationManager.StartOperation(PXGraph graph, System.Action<CancellationToken> method)
  {
    this.StartOperation(graph, method);
  }

  void ILongOperationManager.StartAsyncOperation(
    PXGraph graph,
    Func<CancellationToken, Task> method)
  {
    this.StartOperation(graph, AsyncToSync.Convert(method));
  }

  private void StartOperation(PXGraph graph, System.Action<CancellationToken> method)
  {
    graph.WorkflowService?.BeforeStartOperation(graph);
    this.StartOperation(OperationKey.For(graph), graph.TimeStamp, method);
  }

  void ILongOperationManager.StartOperation(object key, System.Action<CancellationToken> method)
  {
    this.StartOperation(key, method);
  }

  void ILongOperationManager.StartAsyncOperation(object key, Func<CancellationToken, Task> method)
  {
    this.StartOperation(key, AsyncToSync.Convert(method));
  }

  private void StartOperation(object key, System.Action<CancellationToken> method)
  {
    if (!(key is PXGraph graph))
    {
      if (key == null)
        this.StartOperation(OperationKey.New(), method);
      else
        this.StartOperation(new OperationKey(key), method);
    }
    else
      this.StartOperation(graph, method);
  }

  internal void StartOperationWithForceAsync(Guid key, System.Action<CancellationToken> method)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    this.StartOperation(new OperationKey(key), (byte[]) null, method, true);
  }

  internal LongOperationManager ClearOperationResultsOnCompletion()
  {
    PXContext.SetSlot<bool>("PXLongOperationState.ClearOperationResultsOnCompletion", true);
    return this;
  }

  private void StartOperation(OperationKey key, System.Action<CancellationToken> method)
  {
    this.StartOperation(key, (byte[]) null, method);
  }

  private void StartOperation(
    OperationKey key,
    byte[] timeStamp,
    System.Action<CancellationToken> method,
    bool forceAsync = false)
  {
    this.CleanupExpiredItems();
    LongOperationManager.WriteLog("Start Operation " + key.FormattedDisplay);
    this.StartOperationImpl(key, timeStamp, method, forceAsync);
  }

  protected abstract void StartOperationImpl(
    OperationKey key,
    byte[] timeStamp,
    System.Action<CancellationToken> method,
    bool forceAsync);

  protected static void RunSync(
    byte[] timeStamp,
    System.Action<CancellationToken> method,
    CancellationToken cancellationToken)
  {
    using (new WorkflowSyncScope())
    {
      if (timeStamp == null)
      {
        method(cancellationToken);
      }
      else
      {
        using (new PXReadBranchRestrictedScope())
        {
          using (new PXTimeStampScope(timeStamp))
            method(cancellationToken);
        }
      }
    }
  }

  internal bool IsLongOperationInPendingState(object key)
  {
    return this._Pool.TryReadFrom<bool>(OperationKey.IfNotNull(key), (Func<PXLongOperationState, bool>) (state => state.IsPendingTask));
  }

  private void CleanupExpiredItems()
  {
    if (!WebConfig.CleanupLongOperationInfo)
      return;
    bool flag = false;
    System.DateTime now = System.DateTime.Now;
    if (now.Subtract(LongOperationManager._lastCheckTime).TotalMinutes >= (double) WebConfig.LongOperationInfoTimeout)
    {
      lock (LongOperationManager._lastCheckTimeLock)
      {
        now = System.DateTime.Now;
        if (now.Subtract(LongOperationManager._lastCheckTime).TotalMinutes >= (double) WebConfig.LongOperationInfoTimeout)
        {
          LongOperationManager._lastCheckTime = System.DateTime.Now;
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    this._Pool.CleanupExpiredLocal(WebConfig.LongOperationInfoTimeout);
  }

  protected static void WriteLog(string s)
  {
  }

  void ILongOperationTaskManager.AbortTask(RowTaskInfo taskInfo)
  {
    this._Pool.Abort(taskInfo.OperationKey);
  }

  internal void AsyncAbort(object key)
  {
    if (key == null)
      return;
    this._Pool.Abort(new OperationKey(key));
  }

  /// <summary>Simulates operation for current thread</summary>
  internal abstract IDisposable SimulateOperation();

  PXLongRunStatus ILongOperationTaskManager.GetSharedTaskStatus(RowTaskInfo taskInfo)
  {
    OperationKey operationKey = taskInfo.OperationKey;
    this._Pool.BindTaskToUserSession(operationKey);
    return this.GetOperationDetails(operationKey).Status;
  }

  LongOperationDetails ILongOperationManager.GetOperationDetails(object key)
  {
    return key != null ? this.GetOperationDetails(new OperationKey(key)) : throw new ArgumentNullException(nameof (key));
  }

  private LongOperationDetails GetOperationDetails(OperationKey key)
  {
    LongOperationDetails operationDetails = this._Pool.TryReadFrom<LongOperationDetails>(key, (Func<PXLongOperationState, LongOperationDetails>) (state =>
    {
      if (!state.IsCompleted)
        return new LongOperationDetails(PXLongRunStatus.InProcess, state.Elapsed, (Exception) new PXOverridableException("Executing"), state.IsRedirected);
      Exception message = state.Message;
      if (!state.IsRedirected && message != null && message.InnerException is PXBaseRedirectException)
        message = message.InnerException;
      return new LongOperationDetails(state.IsAborted ? PXLongRunStatus.Aborted : PXLongRunStatus.Completed, state.Elapsed, message, state.IsRedirected);
    }));
    if (operationDetails != null)
      return operationDetails;
    PXNothingInProgressException message1 = new PXNothingInProgressException("Executing");
    message1.SetMessage("Nothing in progress");
    return new LongOperationDetails(PXLongRunStatus.NotExists, TimeSpan.Zero, (Exception) message1, false);
  }

  void ILongOperationManager.ClearStatus(object key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    this._Pool.TryWriteTo(new OperationKey(key), (System.Action<PXLongOperationState>) (state =>
    {
      if (!state.IsCompleted)
        return;
      if (!state.IsRedirected && (state.Message is PXBaseRedirectException || state.Message != null && state.Message.InnerException is PXBaseRedirectException))
      {
        state.IsRedirected = true;
        if (state.Message is PXBaseRedirectException)
          state.IsAborted = false;
        this._Pool.Flush(state);
      }
      else
        this._Pool.Remove(state);
    }));
  }

  internal void ForceClearStatus(object key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    this.ForceClearStatus(new OperationKey(key));
  }

  internal void ForceClearStatus(OperationKey key)
  {
    this._Pool.TryWriteTo(key, (System.Action<PXLongOperationState>) (state => this._Pool.Remove(state)));
  }

  internal object GetCurrentItem()
  {
    return this._Pool.TryReadFromUncompletedCurrent<object>((Func<PXLongOperationState, object>) (state => state.CurrentItem));
  }

  internal void SetCurrentItem(object currentItem)
  {
    this._Pool.TryWriteToUncompletedCurrent((System.Action<PXLongOperationState>) (state =>
    {
      state.CurrentItem = currentItem;
      state.CurrentIndex = state.ProcessingList != null ? Array.IndexOf<object>(state.ProcessingList, currentItem) : 0;
    }));
  }

  internal void SetOperationStateModified()
  {
    this._Pool.TryWriteToUncompletedCurrent((System.Action<PXLongOperationState>) (state => state.IsDirty = true));
  }

  internal int GetCurrentIndex()
  {
    return this._Pool.TryReadFromUncompletedCurrent<int>((Func<PXLongOperationState, int>) (state => state.CurrentIndex));
  }

  internal void SetCurrentIndex(int CurrentIndex)
  {
    this._Pool.TryWriteToUncompletedCurrent((System.Action<PXLongOperationState>) (state => state.CurrentIndex = CurrentIndex));
  }

  object ILongOperationManager.GetCustomInfo(string customInfoKey)
  {
    return this._Pool.TryReadFromUncompletedCurrent<object>((Func<PXLongOperationState, object>) (state => !string.IsNullOrEmpty(customInfoKey) ? state.GetCustomInfo(customInfoKey) : state.GetCustomInfo()));
  }

  object ILongOperationManager.GetCustomInfoFor(
    object key,
    string customInfoKey,
    out object[] processingList)
  {
    (object[], object) tuple = this._Pool.TryReadFrom<(object[], object)>(OperationKey.IfNotNull(key), (Func<PXLongOperationState, (object[], object)>) (state => (state.ProcessingList, string.IsNullOrEmpty(customInfoKey) ? state.GetCustomInfo() : state.GetCustomInfo(customInfoKey))));
    processingList = tuple.Item1;
    return tuple.Item2;
  }

  internal Dictionary<string, object> GetAllCustomInfo(object key)
  {
    return this._Pool.TryReadFrom<Dictionary<string, object>>(OperationKey.IfNotNull(key), (Func<PXLongOperationState, Dictionary<string, object>>) (state => state.GetAllCustomInfo()));
  }

  internal bool Exists(object key) => key != null && this._Pool.Exists(new OperationKey(key));

  void ILongOperationManager.SetCustomInfo(object info, string customInfoKey)
  {
    this.SetCustomInfo(info, customInfoKey, (object[]) null);
  }

  internal void SetCustomInfo(object info, string key, object[] processingList)
  {
    this._Pool.TryWriteToUncompletedCurrent((System.Action<PXLongOperationState>) (state =>
    {
      if (string.IsNullOrEmpty(key))
        state.SetCustomInfo(info);
      else
        state.SetCustomInfo(info, key);
      if (processingList == null)
        return;
      state.ProcessingList = processingList;
    }));
  }

  internal void SetCustomInfoInternal(object key, string customInfoKey, object info)
  {
    if (key == null)
      return;
    OperationKey operationKey = new OperationKey(key);
    using (this._Pool.GetLock(operationKey))
    {
      PXLongOperationState state;
      if (!this._Pool.TryGetValue(operationKey, out state))
        return;
      if (string.IsNullOrEmpty(customInfoKey))
        state.SetCustomInfo(info);
      else
        state.SetCustomInfo(info, customInfoKey);
      this._Pool.Flush(state);
    }
  }

  bool ILongOperationManager.IsLongOperationContext() => this.GetOperationKey() != null;

  internal object GetOperationKey()
  {
    return this._Pool.TryReadFromUncompletedCurrent<object>((Func<PXLongOperationState, object>) (state => state.Key.Original));
  }

  internal bool IsLongRunOperation
  {
    get
    {
      ISlotStore instance = SlotStore.Instance;
      bool? nullable = instance.Get<bool?>("IsLongOperationRunning");
      if (nullable.HasValue)
        return nullable.Value;
      bool longRunOperation = this.GetOperationKey() != null;
      LongOperationManager.SetIsLongRunOperation(instance, longRunOperation);
      return longRunOperation;
    }
  }

  protected static void SetIsLongRunOperation(ISlotStore slots, bool value)
  {
    slots.Set("IsLongOperationRunning", (object) value);
  }

  bool ILongOperationManager.WaitCompletion(object key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    Task task = (Task) null;
    OperationKey operationKey = new OperationKey(key);
    using (this._Pool.GetLock(operationKey))
    {
      PXLongOperationState state;
      if (this._Pool.TryGetLocal(operationKey, out state))
      {
        if (state.IsCompleted)
          return true;
        task = state.Task;
      }
    }
    if (task != null)
    {
      task.Wait();
      return true;
    }
    PXLongOperationState state1;
    while (this._Pool.TryGetValue(operationKey, out state1))
    {
      if (state1.IsCompleted)
        return true;
      Thread.Sleep(5000);
    }
    return false;
  }

  IEnumerable<RowTaskInfo> ILongOperationManager.GetTaskList()
  {
    return this.GetTaskList((string) null, (string) null);
  }

  IEnumerable<RowTaskInfo> ILongOperationTaskManager.GetTasks(string key, string screenID)
  {
    return this.GetTaskList(key, screenID);
  }

  private IEnumerable<RowTaskInfo> GetTaskList(string key, string screenID)
  {
    return this._Pool.GetTaskList(key, screenID);
  }

  internal void FlushState() => this._Pool.FlushState();
}
