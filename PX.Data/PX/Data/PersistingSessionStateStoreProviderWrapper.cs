// Decompiled with JetBrains decompiler
// Type: PX.Data.PersistingSessionStateStoreProviderWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Data;

internal sealed class PersistingSessionStateStoreProviderWrapper(SessionStateStoreProviderBase inner) : 
  SessionStateStoreProviderWrapperBase(inner)
{
  public override SessionStateStoreData? GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    SessionStateStoreData sessionStateStoreData = base.GetItem(context, id, out locked, out lockAge, out lockId, out actions);
    return this.UnWrapItems(context, sessionStateStoreData);
  }

  public override SessionStateStoreData? GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actions)
  {
    SessionStateStoreData itemExclusive = base.GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actions);
    try
    {
      return this.UnWrapItems(context, itemExclusive);
    }
    catch
    {
      this.ReleaseItemExclusive(context, id, lockId);
      throw;
    }
  }

  public override void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    SessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    try
    {
      PersistingSessionStateStoreProviderWrapper.WrapItems(item);
    }
    catch
    {
      this.ReleaseItemExclusive(context, id, lockId);
      throw;
    }
    base.SetAndReleaseItemExclusive(context, id, item, lockId, newItem);
  }

  private SessionStateStoreData? UnWrapItems(HttpContext context, SessionStateStoreData? item)
  {
    if (item == null || item.Items?["Wrapper"] == null)
      return item;
    SessionStateStoreData newStoreData = this.CreateNewStoreData(context, item.Timeout);
    for (int index = 0; index < item.Items.Count; ++index)
      newStoreData.Items[item.Items.Keys[index]] = item.Items[index];
    try
    {
      PersistingWrapperFS.UnWrap(newStoreData);
    }
    catch (PXSerializationException ex)
    {
      PXTrace.Logger.ForContext<PersistingSessionStateStoreProviderWrapper>().Error<string>((Exception) ex, "Error {Message} during session deserialization, session will be cleared", ((Exception) ex).Message);
      newStoreData.Items.Clear();
    }
    return newStoreData;
  }

  private static void WrapItems(SessionStateStoreData item)
  {
    if (item.Items == null)
      return;
    PersistingWrapperFS persistingWrapperFs = new PersistingWrapperFS();
    persistingWrapperFs.Wrap(item);
    PXPerformanceInfo currentSample = PXPerformanceMonitor.GetCurrentSample();
    if (currentSample == null)
      return;
    currentSample.SessionSaveSize = (double) persistingWrapperFs.GetBufferLength().GetValueOrDefault();
  }
}
