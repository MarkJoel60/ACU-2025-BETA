// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStateProviderWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace PX.Data;

internal sealed class PXSessionStateProviderWrapper(SessionStateStoreProviderBase inner) : 
  SessionStateStoreProviderWrapperBase(inner)
{
  public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
  {
    SessionStateStoreData newStoreData = base.CreateNewStoreData(context, timeout);
    return new SessionStateStoreData((ISessionStateItemCollection) new PXSessionStateItemCollection(newStoreData.Items), newStoreData.StaticObjects, timeout);
  }

  public override SessionStateStoreData GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    SessionStateStoreData sessionStateStoreData = base.GetItem(context, id, out locked, out lockAge, out lockId, out actions);
    return sessionStateStoreData != null ? new SessionStateStoreData((ISessionStateItemCollection) new PXSessionStateItemCollection(sessionStateStoreData.Items, PXSessionStateStore.CopyStateItemsRequired(context)), sessionStateStoreData.StaticObjects, sessionStateStoreData.Timeout) : (SessionStateStoreData) null;
  }

  public override SessionStateStoreData GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    SessionStateStoreData itemExclusive = base.GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actions);
    return itemExclusive != null ? new SessionStateStoreData((ISessionStateItemCollection) new PXSessionStateItemCollection(itemExclusive.Items), itemExclusive.StaticObjects, itemExclusive.Timeout) : (SessionStateStoreData) null;
  }

  public override void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    SessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    SessionStateStoreData sessionStateStoreData = new SessionStateStoreData(((PXSessionStateItemCollection) item.Items).innerCollection, item.StaticObjects, item.Timeout);
    base.SetAndReleaseItemExclusive(context, id, sessionStateStoreData, lockId, newItem);
  }
}
