// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.RuntimeLongOperationManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Async.Internal;

internal sealed class RuntimeLongOperationManager : LongOperationManager
{
  private readonly ILongOperationWorkflowAdapter _workflow;
  private readonly ILifetimeScope _lifetimeScope;
  private readonly PXThreadPool _threads;

  public RuntimeLongOperationManager(
    ILongOperationWorkflowAdapter workflow,
    PXTaskPool taskPool,
    IOptions<ThreadPoolOptions> options,
    ILifetimeScope lifetimeScope)
    : base(taskPool)
  {
    this._workflow = workflow;
    this._lifetimeScope = lifetimeScope;
    this._threads = new PXThreadPool(options);
  }

  protected override void StartOperationImpl(
    OperationKey key,
    byte[] timeStamp,
    System.Action<CancellationToken> method,
    bool forceAsync)
  {
    ISlotStorageProvider provider = SlotStore.Provider;
    PXLongOperationState longOperationState = (PXLongOperationState) null;
    if (!forceAsync)
    {
      using (this._Pool.GetLock(key))
        longOperationState = this._Pool.GetContextTask((ISlotStore) provider);
    }
    if (longOperationState != null)
    {
      if (method == null)
        throw new ArgumentNullException(nameof (method), "The delegate is null in context of the long operation.");
      if (longOperationState.IsCompleted)
        throw new InvalidOperationException("Long operation has been completed and cannot start a new operation.");
      LongOperationManager.RunSync(timeStamp, method, longOperationState.CancellationToken);
    }
    else
    {
      PXLongOperationState state;
      using (this._Pool.GetLock(key))
      {
        if ((!this._Pool.TryGetValue(key, out state) ? 1 : (state.IsCompleted ? 1 : 0)) != 0)
        {
          state = new PXLongOperationState(key);
          if (method == null)
          {
            state.IsNotStarted = true;
            state.IsPendingTask = true;
          }
          this._Pool.Add(state);
          if (method == null)
          {
            LongOperationManager.WriteLog("Start Pending Operation " + key.FormattedDisplay);
            return;
          }
        }
        else
        {
          if (!this._Pool.TryGetLocal(key, out state))
            throw new ArgumentOutOfRangeException(nameof (key), (object) key, "Remote call");
          if (method == null)
            throw new ArgumentNullException(nameof (method), "The method is null in the second call.");
          if (!state.IsNotStarted)
          {
            if (state.IsPendingTask)
              throw new InvalidOperationException("Pending task");
            throw new PXException("The previous operation has not been completed yet.");
          }
          LongOperationManager.WriteLog("Set Pending delegate " + key.FormattedDisplay);
          state.IsNotStarted = false;
        }
      }
      if (method == null)
        throw new ArgumentNullException(nameof (method), "The method is null in the long operation.");
      HttpContext current = HttpContext.Current;
      PXLongOperationPars longOperationPars = PXLongOperationPars.Capture(provider, current, method, timeStamp, state);
      longOperationPars.AddExtension<LongOperationPerformanceParameters>(new LongOperationPerformanceParameters((ISlotStore) provider, current));
      this._workflow.StartOperation(longOperationPars);
      PXTelemetryInvoker.OnLongOperationStarting(key.Original);
      this._threads.QueueUserWorkItem(new System.Action<PXLongOperationPars>(this.PerformOperation), longOperationPars);
    }
  }

  private static IDisposable SetIsLongRunOperation(ISlotStore slots)
  {
    LongOperationManager.SetIsLongRunOperation(slots, true);
    return Disposable.Create<ISlotStore>(slots, (System.Action<ISlotStore>) (s => LongOperationManager.SetIsLongRunOperation(s, false)));
  }

  private void PerformOperation(PXLongOperationPars p)
  {
    PXLongOperationState state = p.State;
    Activity previous;
    using (ActivityHelper.ClearRootActivity(out previous))
    {
      ActivitySource activitySource = ActivityHelper.ActivitySource;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("acumatica.longoperation.key", (object) state.Key.String);
      ActivityLink[] activityLinkArray;
      if (previous != null)
        activityLinkArray = new ActivityLink[1]
        {
          new ActivityLink(previous.Context, (ActivityTagsCollection) null)
        };
      else
        activityLinkArray = (ActivityLink[]) null;
      DateTimeOffset dateTimeOffset = new DateTimeOffset();
      using (activitySource.StartActivity("LongOperation", (ActivityKind) 0, (string) null, (IEnumerable<KeyValuePair<string, object>>) dictionary, (IEnumerable<ActivityLink>) activityLinkArray, dateTimeOffset))
      {
        using (IDisposableSlotStorageProvider slots = SlotStore.AsyncLocal())
        {
          using (SingleAssignmentDisposable assignmentDisposable1 = new SingleAssignmentDisposable())
          {
            using (SingleAssignmentDisposable assignmentDisposable2 = new SingleAssignmentDisposable())
            {
              using (RuntimeLongOperationManager.SetIsLongRunOperation((ISlotStore) slots))
              {
                using (RuntimeLongOperationManager.PerformanceMonitoring performanceMonitoring = new RuntimeLongOperationManager.PerformanceMonitoring((ISlotStore) slots, state.Key.Original))
                {
                  try
                  {
                    using (state.InitCancellation())
                    {
                      assignmentDisposable1.Disposable = (IDisposable) ((ISlotStore) slots).BeginLifetimeScope(this._lifetimeScope);
                      p.Restore((ISlotStorageProvider) slots);
                      this._workflow.RestoreWorkflowParameters(p);
                      Exception message1;
                      using (performanceMonitoring.LongOperation(p))
                      {
                        try
                        {
                          this._Pool.BindAmbientContext((ISlotStore) slots, state);
                          using (new PXReadBranchRestrictedScope())
                          {
                            assignmentDisposable2.Disposable = (IDisposable) new PXTimeStampScope(p.TimeStamp);
                            p.PopAndRunDelegate(state.CancellationToken);
                            if (state.CancellationToken.IsCancellationRequested)
                              throw new PXOperationCompletedException("The operation has been aborted.");
                          }
                          PXConnectionList.CheckConnections();
                          message1 = (Exception) new PXOperationCompletedException("The operation has completed.");
                        }
                        catch (Exception ex)
                        {
                          PXTrace.WriteError(ex);
                          PXOperationCompletedException message2 = new PXOperationCompletedException("The operation has completed.");
                          CancellationToken cancellationToken = state.CancellationToken;
                          message1 = RuntimeLongOperationManager.HandleException(ex, (Exception) message2, cancellationToken);
                        }
                      }
                      bool isAborted;
                      try
                      {
                        ChainEventArgs<Exception> exceptionArgs = ChainEventArgs.From<Exception>(message1);
                        switch (message1)
                        {
                          case PXOperationCompletedWithErrorException _:
                          case PXOperationCompletedSingleErrorException _:
                            isAborted = true;
                            this._workflow.CompleteOperation(p, OperationCompletion.WorkflowAndEventsAndActionSequences, exceptionArgs);
                            break;
                          case PXOperationCompletedException _:
                            isAborted = false;
                            this._workflow.CompleteOperation(p, OperationCompletion.WorkflowAndEventsAndActionSequences, exceptionArgs);
                            break;
                          case PXBaseRedirectException _:
                            isAborted = false;
                            this._workflow.CompleteOperation(p, OperationCompletion.WorkflowAndEventsAndActionSequences, exceptionArgs);
                            break;
                          default:
                            isAborted = true;
                            this._workflow.CompleteOperation(p, OperationCompletion.OnlyActionSequences, exceptionArgs);
                            break;
                        }
                        if (exceptionArgs.Handled)
                        {
                          isAborted = false;
                          message1 = (Exception) new PXOperationCompletedException("The operation has completed.");
                        }
                      }
                      catch (Exception ex)
                      {
                        isAborted = true;
                        Exception message3 = message1;
                        CancellationToken cancellationToken = state.CancellationToken;
                        message1 = RuntimeLongOperationManager.HandleException(ex, message3, cancellationToken);
                      }
                      using (this._Pool.GetLock(state.Key))
                      {
                        state.CompleteRequest(this._Pool, isAborted, message1, (ISlotStore) slots);
                        this._Pool.Complete(state);
                        if (!state.ClearOperationResultsOnCompletion)
                          return;
                        this._Pool.Remove(state);
                      }
                    }
                  }
                  catch (Exception ex)
                  {
                    using (this._Pool.GetLock(state.Key))
                      state.CompleteRequest(this._Pool, true, ex, (ISlotStore) slots);
                    PXTrace.WriteError(ex, "A long operation failed with the following message: {0}", (object) ex.Message);
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  private static Exception HandleException(
    Exception exception,
    Exception message,
    CancellationToken operationCancellationToken)
  {
    Exception e = exception;
    if (!(e is ThreadAbortException))
    {
      if (e == null)
        throw new ArgumentNullException(nameof (exception));
      if (!RuntimeLongOperationManager.HasInnerThreadAbortException(e))
      {
        switch (e)
        {
          case OperationCanceledException _:
            if (operationCancellationToken.IsCancellationRequested)
            {
              if (message is PXOverridableException overridableException)
                overridableException.SetMessage("The operation has been aborted.");
              return message;
            }
            break;
          case TargetInvocationException invocationException:
            if (invocationException.InnerException != null)
              return invocationException.InnerException;
            break;
        }
        return e;
      }
      if (message is PXOverridableException overridableException1)
        overridableException1.SetMessage("The operation has been aborted.");
      Thread.ResetAbort();
      return message;
    }
    ((PXOverridableException) message).SetMessage("The operation has been aborted.");
    Thread.ResetAbort();
    return message;
  }

  private static bool HasInnerThreadAbortException(Exception e)
  {
    Exception innerException = e.InnerException;
    if (innerException is ThreadAbortException)
      return true;
    return innerException != null && RuntimeLongOperationManager.HasInnerThreadAbortException(innerException);
  }

  /// <summary>Simulates operation for current thread</summary>
  internal override IDisposable SimulateOperation()
  {
    ISlotStore slots = SlotStore.Instance;
    if (this._Pool.HasContextTask(slots))
      return Disposable.Empty;
    OperationKey key = OperationKey.New();
    PXLongOperationState state = new PXLongOperationState(key);
    this._Pool.BindAmbientContext(slots, state);
    using (this._Pool.GetLock(key))
      this._Pool.Add(state);
    IDisposable cancellationScope = state.InitCancellation();
    return Disposable.Create((System.Action) (() =>
    {
      cancellationScope.Dispose();
      using (this._Pool.GetLock(key))
        this._Pool.Remove(state);
      this._Pool.UnbindAmbientContext(slots);
    }));
  }

  private class PerformanceMonitoring : IDisposable
  {
    private readonly ISlotStore _slots;
    private readonly object _key;
    private PXPerformanceInfo _performanceInfo;

    public PerformanceMonitoring(ISlotStore slots, object key)
    {
      this._key = key;
      this._slots = slots;
    }

    public IDisposable LongOperation(PXLongOperationPars p)
    {
      if (!PXPerformanceMonitor.IsEnabled && !PXPerformanceMonitor.IsDebuggerEnabled && !WebConfig.ProfilerMonitorThreads)
        return Disposable.Empty;
      if (PXPerformanceMonitor.IsLongOperationCollectMemory)
        GCHelper.ForcedCollect(true);
      string username;
      string company;
      LegacyCompanyService.ParseLogin(p.UserContext.With<PXSessionContext, string>((Func<PXSessionContext, string>) (c => c.UserName ?? c.IdentityName)), out username, out company, out string _);
      LongOperationPerformanceParameters extension = p.GetExtension<LongOperationPerformanceParameters>();
      PXPerformanceInfo pxPerformanceInfo = new PXPerformanceInfo();
      pxPerformanceInfo.ScreenId = extension.ScreenUrl;
      pxPerformanceInfo.InternalScreenId = PXContext.GetScreenID()?.Replace(".", "");
      pxPerformanceInfo.CommandTarget = "LongRun";
      pxPerformanceInfo.CommandName = extension.CommandName;
      pxPerformanceInfo.UserId = username;
      string str = company;
      if (str == null)
      {
        PXDatabaseProvider provider = PXDatabase.Provider;
        if (provider == null)
        {
          str = (string) null;
        }
        else
        {
          string[] dbCompanies = provider.DbCompanies;
          str = dbCompanies != null ? ((IEnumerable<string>) dbCompanies).FirstOrDefault<string>() : (string) null;
        }
      }
      pxPerformanceInfo.CompanyName = str;
      pxPerformanceInfo.TenantId = company;
      this._performanceInfo = pxPerformanceInfo;
      TypeKeyedOperationExtensions.Set<PXPerformanceInfo>(this._slots, this._performanceInfo);
      PXPerformanceMonitor.SetScreenIdInternal(true);
      this._performanceInfo.MemBefore = GC.GetTotalMemory(PXPerformanceMonitor.IsLongOperationCollectMemory);
      this._performanceInfo.MemWorkingSet = PXPerformanceMonitor.GetWorkingSet();
      this._performanceInfo.Timer.Start();
      this._performanceInfo.ThreadTime = PXPerformanceMonitor.CurrentThreadTime();
      PXPerformanceMonitor.RegisterSampleInProgress(this._performanceInfo);
      PXTelemetryInvoker.OnLongOperationStarted(p.State.Key.Original);
      return Disposable.Create((System.Action) (() =>
      {
        if (TypeKeyedOperationExtensions.Get<PXPerformanceInfo>(this._slots) != null)
          return;
        this._performanceInfo.Timer.Stop();
        PXPerformanceMonitor.RemoveSampleInProgress(this._performanceInfo);
        this._performanceInfo = (PXPerformanceInfo) null;
      }));
    }

    void IDisposable.Dispose()
    {
      if (this._performanceInfo == null)
        return;
      this._performanceInfo.Timer.Stop();
      this._performanceInfo.ThreadTime = PXPerformanceMonitor.CurrentThreadTime() - this._performanceInfo.ThreadTime;
      PXTelemetryInvoker.OnLongOperationEnded();
      PXTelemetryInvoker.OnLongOperationCompleted(this._key);
      if (PXPerformanceMonitor.IsLongOperationCollectMemory)
        GCHelper.ForcedCollect(true);
      this._performanceInfo.MemDelta = GC.GetTotalMemory(PXPerformanceMonitor.IsLongOperationCollectMemory) - this._performanceInfo.MemBefore;
      PXPerformanceMonitor.AddSample(this._performanceInfo);
    }
  }
}
