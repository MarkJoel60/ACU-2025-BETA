// Decompiled with JetBrains decompiler
// Type: PX.Async.Internal.PXLongOperationState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Data.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Async.Internal;

[Serializable]
internal class PXLongOperationState
{
  [NonSerialized]
  public bool IsDirty = true;
  [NonSerialized]
  public bool WasFlushed;
  private bool _IsNotStarted;
  private const string DEFAULT_CUSTOM_INFO_KEY = "";
  public const string CLEAR_OPERATION_RESULTS_ON_COMPLETION_SLOT = "PXLongOperationState.ClearOperationResultsOnCompletion";
  private readonly System.DateTime _StartStamp;
  private System.DateTime? _EndStamp;
  private bool _IsAborted;
  private bool _IsRedirected;
  private Dictionary<string, object> _CustomInfo;
  private object _CurrentItem;
  private int _CurrentIndex;
  private IEnumerable<PXTrace.Event> _TraceSource;
  private object[] _processingList;
  [NonSerialized]
  private readonly TaskCompletionSource<object> _taskCompletionSource = new TaskCompletionSource<object>();
  [NonSerialized]
  private CancellationTokenSource _cancellationTokenSource;
  [NonSerialized]
  private bool _removed;

  public bool IsNotStarted
  {
    get => this._IsNotStarted;
    set
    {
      this.IsDirty = true;
      this._IsNotStarted = value;
    }
  }

  internal string UserName { get; }

  internal string ScreenID { get; }

  internal int DatabaseID { get; }

  internal PX.Async.Internal.OperationKey Key { get; }

  [Obsolete("Use Key.Original instead")]
  public object OperationKey => this.Key.Original;

  public bool IsPendingTask { get; set; }

  internal Guid InstanceId { get; } = Guid.NewGuid();

  public PXLongOperationState(PX.Async.Internal.OperationKey key)
  {
    this.Key = key;
    this._StartStamp = System.DateTime.Now;
    if (HttpContext.Current != null && HttpContext.Current.User != null)
      this.UserName = PXContext.PXIdentity.User.Identity.Name;
    this.ScreenID = PXContext.GetScreenID();
    this.ClearOperationResultsOnCompletion = PXContext.GetSlot<bool>("PXLongOperationState.ClearOperationResultsOnCompletion");
    PXContext.ClearSlot("PXLongOperationState.ClearOperationResultsOnCompletion");
    if (!PXAccess.IsMultiDbMode)
      return;
    this.DatabaseID = PXInstanceHelper.DatabaseId;
  }

  internal void MarkAsRemoved() => this._removed = true;

  internal void CompleteRequest(
    PXTaskPool taskPool,
    bool isAborted,
    Exception message,
    ISlotStore slots)
  {
    this.IsDirty = true;
    this.IsCompleted = true;
    this._EndStamp = new System.DateTime?(System.DateTime.Now);
    this._IsAborted = isAborted;
    this.Message = message?.GetType().Name == "MySqlException" ? new Exception(message.Message) : message;
    taskPool.UnbindAmbientContext(slots);
    this.Flush(taskPool);
    this._taskCompletionSource.SetResult((object) null);
  }

  private void Flush(PXTaskPool taskPool)
  {
    if (this._removed)
      return;
    this.IsDirty = true;
    taskPool.Flush(this);
  }

  internal bool IsCompleted { get; private set; }

  internal Task Task => (Task) this._taskCompletionSource.Task;

  internal IDisposable InitCancellation()
  {
    CancellationTokenSource cancellationTokenSource1 = new CancellationTokenSource();
    CancellationTokenSource cancellationTokenSource2 = Interlocked.Exchange<CancellationTokenSource>(ref this._cancellationTokenSource, cancellationTokenSource1);
    if (cancellationTokenSource2 == null)
      return (IDisposable) cancellationTokenSource1;
    cancellationTokenSource2.Dispose();
    cancellationTokenSource1.Dispose();
    throw new InvalidOperationException("InitCancellation should not be called twice");
  }

  private CancellationTokenSource CancellationTokenSource
  {
    get
    {
      return this._cancellationTokenSource ?? throw new InvalidOperationException("CancellationTokenSource not set");
    }
  }

  internal CancellationToken CancellationToken => this.CancellationTokenSource.Token;

  internal void Cancel() => this.CancellationTokenSource.Cancel();

  public TimeSpan Elapsed => (this._EndStamp ?? System.DateTime.Now) - this._StartStamp;

  public System.DateTime StartStamp => this._StartStamp;

  internal System.DateTime? EndStamp => this._EndStamp;

  public bool IsAborted
  {
    get => this._IsAborted;
    set
    {
      this.IsDirty = true;
      this._IsAborted = value;
    }
  }

  public bool IsRedirected
  {
    get => this._IsRedirected;
    set
    {
      this.IsDirty = true;
      this._IsRedirected = value;
    }
  }

  public Exception Message { get; private set; }

  public object CurrentItem
  {
    get => this._CurrentItem;
    set
    {
      this.IsDirty = true;
      this._CurrentItem = value;
    }
  }

  public int CurrentIndex
  {
    get => this._CurrentIndex;
    set
    {
      this.IsDirty = true;
      this._CurrentIndex = value;
    }
  }

  public object[] ProcessingList
  {
    get => this._processingList;
    set
    {
      this.IsDirty = true;
      this._processingList = value;
    }
  }

  internal bool ClearOperationResultsOnCompletion { get; private set; }

  public PXProgress GetProgress()
  {
    if (!(this.GetCustomInfo("PXProcessingState") is PXProcessingInfo customInfo))
      return (PXProgress) null;
    return new PXProgress()
    {
      Total = new int?(customInfo.Messages.Length),
      Current = new int?(customInfo.Processed),
      Errors = new int?(customInfo.Errors),
      Warnings = new int?(customInfo.Warnings)
    };
  }

  private string CustomInfoGraphKey(string key, PXGraph graph)
  {
    return $"{key}|{graph.GetType().FullName}";
  }

  private System.Type GetCacheFromCustomInfoKey(
    string customInfoGraphKey,
    string customInfoCacheKey)
  {
    return PXBuildManager.GetType(customInfoCacheKey.Substring(customInfoGraphKey.Length), false);
  }

  private void SetCustomInfoInt(object info, string key)
  {
    if (this._CustomInfo == null)
      this._CustomInfo = new Dictionary<string, object>();
    this._CustomInfo[key] = info;
    if (info is PXGraph graph && graph.IsMobile)
      this._CustomInfo[this.CustomInfoGraphKey(key, graph)] = (object) new PXLongOperationState.CustomInfoGraphState(graph);
    this.IsDirty = true;
  }

  private bool ValidCustomInfoKey(string key)
  {
    if (string.IsNullOrEmpty(key))
      throw new PXArgumentException(nameof (key), "Cannot be null or empty.");
    return true;
  }

  public void SetCustomInfo(object info) => this.SetCustomInfoInt(info, "");

  public void SetCustomInfo(object info, string key)
  {
    if (!this.ValidCustomInfoKey(key))
      return;
    this.SetCustomInfoInt(info, key);
  }

  private object GetCustomInfoInt(string key)
  {
    object customInfoInt = (object) null;
    if (this._CustomInfo != null && this._CustomInfo.ContainsKey(key))
      customInfoInt = this._CustomInfo[key];
    if (customInfoInt == null || !(customInfoInt is PXGraph graph) || !graph.IsMobile)
      return customInfoInt;
    object obj;
    return this._CustomInfo.TryGetValue(this.CustomInfoGraphKey(key, graph), out obj) && obj is PXLongOperationState.CustomInfoGraphState customInfoGraphState ? (object) customInfoGraphState.FillFromState(graph) : (object) graph;
  }

  public object GetCustomInfo() => this.GetCustomInfoInt("");

  public object GetCustomInfo(string key)
  {
    object customInfo = (object) null;
    if (this.ValidCustomInfoKey(key))
      customInfo = this.GetCustomInfoInt(key);
    return customInfo;
  }

  public Dictionary<string, object> GetAllCustomInfo() => this._CustomInfo;

  public static bool IsDefaultInfo(string key) => string.Equals(key, "");

  internal class CustomInfoGraphState
  {
    public Dictionary<string, PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState> Caches;

    public CustomInfoGraphState(PXGraph graph)
    {
      this.Caches = new Dictionary<string, PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState>();
      foreach (System.Type key in graph.Caches.Keys.ToList<System.Type>())
        this.Caches[key.FullName] = new PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState(graph.Caches[key]);
    }

    public PXGraph FillFromState(PXGraph graph)
    {
      foreach (PXView pxView in graph.Views.Values.ToArray<PXView>())
        pxView?.DetachCache();
      foreach (KeyValuePair<string, PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState> cach1 in this.Caches)
      {
        System.Type type = PXBuildManager.GetType(cach1.Key, false);
        if (type != (System.Type) null)
        {
          PXCache cach2 = graph.Caches[type];
          if (cach2 != null)
          {
            cach2.Clear();
            cach2.ClearQueryCache();
            foreach (PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState cachedState in cach1.Value.Cached)
            {
              cach2.SetStatus(cachedState.Row, cachedState.Status);
              foreach (PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState.ErrorState error in cachedState.Errors)
                PXUIFieldAttribute.SetError(cach2, cachedState.Row, error.FieldName, error.ErrorText, error.ErrorValue.ToString());
            }
            cach2._Current = cach1.Value.Current;
            cach2.IsDirty = cach1.Value.IsDirty;
          }
        }
      }
      return graph;
    }

    internal class CustomInfoCacheState
    {
      public object Current;
      public List<PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState> Cached;
      public bool IsDirty;

      public CustomInfoCacheState(PXCache cache)
      {
        this.Current = cache._Current;
        this.Cached = new List<PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState>();
        foreach (object row in cache.Cached)
          this.Cached.Add(new PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState(cache, row));
        this.IsDirty = cache.IsDirty;
      }

      public class CachedState
      {
        public PXEntryStatus Status;
        public object Row;
        public List<PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState.ErrorState> Errors;

        public CachedState(PXCache cache, object row)
        {
          this.Status = cache.GetStatus(row);
          this.Row = row;
          this.Errors = this.CollectErrors(cache, row).ToList<PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState.ErrorState>();
        }

        private IEnumerable<PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState.ErrorState> CollectErrors(
          PXCache cache,
          object row)
        {
          foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(row, (string) null))
          {
            if (subscriberAttribute is IPXInterfaceField pxInterfaceField && pxInterfaceField.ErrorLevel == PXErrorLevel.Error && pxInterfaceField.ErrorValue != null)
              yield return new PXLongOperationState.CustomInfoGraphState.CustomInfoCacheState.CachedState.ErrorState()
              {
                ErrorText = pxInterfaceField.ErrorText,
                ErrorValue = pxInterfaceField.ErrorValue.ToString(),
                FieldName = subscriberAttribute.FieldName
              };
          }
        }

        public class ErrorState
        {
          public string FieldName;
          public string ErrorText;
          public string ErrorValue;
        }
      }
    }
  }
}
