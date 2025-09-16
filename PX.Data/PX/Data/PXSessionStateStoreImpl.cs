// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStateStoreImpl
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Data;

internal sealed class PXSessionStateStoreImpl(SessionStateStoreProviderBase sessionStore) : 
  IDisposable
{
  public void Dispose() => sessionStore.Dispose();

  internal void InitializeRequest(HttpContext context) => sessionStore.InitializeRequest(context);

  internal void CreateUninitializedItem(HttpContext context, string id, int timeout)
  {
    sessionStore.CreateUninitializedItem(context, id, timeout);
  }

  internal PXSessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
  {
    return new PXSessionStateStoreData(sessionStore.CreateNewStoreData(context, timeout));
  }

  internal PXSessionStateStoreData? GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    return PXSessionStateStoreImpl.Convert(sessionStore.GetItem(context, id, out locked, out lockAge, out lockId, out actions));
  }

  internal PXSessionStateStoreData? GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    return PXSessionStateStoreImpl.Convert(sessionStore.GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actions));
  }

  internal void ReleaseItemExclusive(HttpContext context, string id, object lockId)
  {
    sessionStore.ReleaseItemExclusive(context, id, lockId);
  }

  internal void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    PXSessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    sessionStore.SetAndReleaseItemExclusive(context, id, item.PrepareForSet(), lockId, newItem);
  }

  internal void EndRequest(HttpContext context) => sessionStore.EndRequest(context);

  internal void RemoveItem(
    HttpContext context,
    string id,
    object lockId,
    PXSessionStateStoreData item)
  {
    sessionStore.RemoveItem(context, id, lockId, item.PrepareForSet());
  }

  internal void Clear(HttpSessionStateBase session) => PXSessionStateStoreData.Clear(session);

  internal bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
  {
    return sessionStore.SetItemExpireCallback(expireCallback);
  }

  internal void ResetItemTimeout(HttpContext context, string id)
  {
    sessionStore.ResetItemTimeout(context, id);
  }

  private static PXSessionStateStoreData? Convert(SessionStateStoreData? item)
  {
    return item == null ? (PXSessionStateStoreData) null : new PXSessionStateStoreData(item);
  }
}
