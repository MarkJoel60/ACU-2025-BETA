// Decompiled with JetBrains decompiler
// Type: PX.Data.AsyncToSyncSessionStateStore
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNet.SessionState;
using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace PX.Data;

internal sealed class AsyncToSyncSessionStateStore : SessionStateStoreProviderBase
{
  private SessionStateStoreProviderAsyncBase _provider;

  public AsyncToSyncSessionStateStore()
  {
  }

  public AsyncToSyncSessionStateStore(SessionStateStoreProviderAsyncBase provider)
  {
    this._provider = provider;
  }

  public override void Initialize(string name, NameValueCollection config)
  {
    base.Initialize(name, config);
    ((ProviderBase) this._provider).Initialize(name, config);
  }

  public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
  {
    return this._provider.CreateNewStoreData((HttpContextBase) new HttpContextWrapper(context), timeout);
  }

  public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
  {
    this._provider.CreateUninitializedItemAsync((HttpContextBase) new HttpContextWrapper(context), id, timeout, CancellationToken.None).Wait();
  }

  public override void Dispose() => this._provider.Dispose();

  public override void EndRequest(HttpContext context)
  {
    this._provider.EndRequestAsync((HttpContextBase) new HttpContextWrapper(context)).Wait();
  }

  public override SessionStateStoreData GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    Task<GetItemResult> itemAsync = this._provider.GetItemAsync((HttpContextBase) new HttpContextWrapper(context), id, CancellationToken.None);
    itemAsync.Wait();
    GetItemResult result = itemAsync.Result;
    locked = result.Locked;
    lockAge = result.LockAge;
    lockId = result.LockId;
    actions = result.Actions;
    return result.Item;
  }

  public override SessionStateStoreData GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    Task<GetItemResult> itemExclusiveAsync = this._provider.GetItemExclusiveAsync((HttpContextBase) new HttpContextWrapper(context), id, CancellationToken.None);
    itemExclusiveAsync.Wait();
    GetItemResult result = itemExclusiveAsync.Result;
    locked = result.Locked;
    lockAge = result.LockAge;
    lockId = result.LockId;
    actions = result.Actions;
    return result.Item;
  }

  public override void InitializeRequest(HttpContext context)
  {
    this._provider.InitializeRequest((HttpContextBase) new HttpContextWrapper(context));
  }

  public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
  {
    this._provider.ReleaseItemExclusiveAsync((HttpContextBase) new HttpContextWrapper(context), id, lockId, CancellationToken.None).Wait();
  }

  public override void RemoveItem(
    HttpContext context,
    string id,
    object lockId,
    SessionStateStoreData item)
  {
    this._provider.RemoveItemAsync((HttpContextBase) new HttpContextWrapper(context), id, lockId, item, CancellationToken.None).Wait();
  }

  public override void ResetItemTimeout(HttpContext context, string id)
  {
    this._provider.ResetItemTimeoutAsync((HttpContextBase) new HttpContextWrapper(context), id, CancellationToken.None).Wait();
  }

  public override void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    SessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    this._provider.SetAndReleaseItemExclusiveAsync((HttpContextBase) new HttpContextWrapper(context), id, item, lockId, newItem, CancellationToken.None).Wait();
  }

  public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
  {
    return this._provider.SetItemExpireCallback(expireCallback);
  }
}
