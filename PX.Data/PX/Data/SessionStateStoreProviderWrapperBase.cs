// Decompiled with JetBrains decompiler
// Type: PX.Data.SessionStateStoreProviderWrapperBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Data;

internal abstract class SessionStateStoreProviderWrapperBase(SessionStateStoreProviderBase inner) : 
  SessionStateStoreProviderBase
{
  protected SessionStateStoreProviderBase Inner { get; } = inner;

  public override void Initialize(string name, NameValueCollection config)
  {
    base.Initialize(name, config);
    this.Inner.Initialize(name, config);
  }

  public override void Dispose() => this.Inner.Dispose();

  public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
  {
    return this.Inner.SetItemExpireCallback(expireCallback);
  }

  public override void InitializeRequest(HttpContext context)
  {
    this.Inner.InitializeRequest(context);
  }

  public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
  {
    return this.Inner.CreateNewStoreData(context, timeout);
  }

  public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
  {
    this.Inner.CreateUninitializedItem(context, id, timeout);
  }

  public override SessionStateStoreData? GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    return this.Inner.GetItem(context, id, out locked, out lockAge, out lockId, out actions);
  }

  public override SessionStateStoreData? GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    return this.Inner.GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actions);
  }

  public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
  {
    this.Inner.ReleaseItemExclusive(context, id, lockId);
  }

  public override void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    SessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    this.Inner.SetAndReleaseItemExclusive(context, id, item, lockId, newItem);
  }

  public override void EndRequest(HttpContext context) => this.Inner.EndRequest(context);

  public override void RemoveItem(
    HttpContext context,
    string id,
    object lockId,
    SessionStateStoreData item)
  {
    this.Inner.RemoveItem(context, id, lockId, item);
  }

  public override void ResetItemTimeout(HttpContext context, string id)
  {
    this.Inner.ResetItemTimeout(context, id);
  }
}
