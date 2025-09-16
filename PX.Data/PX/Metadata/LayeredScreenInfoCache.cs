// Decompiled with JetBrains decompiler
// Type: PX.Metadata.LayeredScreenInfoCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// <para>Provides a persistent, performance-optimized implementation of <see cref="T:PX.Metadata.IScreenInfoStorage" />
/// and <see cref="T:PX.Metadata.IScreenInfoCacheControl" /> interfaces.</para>
/// <para>It attempts to retrieve <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> from the in-memory cache,
/// and if it's not found there, uses the distributed cache to get the item and duplicate it
/// to the in-memory cache.</para>
/// </summary>
/// <remarks>
/// Made <see langword="public" /> only to be consistent with <see cref="T:PX.Metadata.InMemoryScreenInfoCache" /> and <see cref="T:PX.Metadata.DistributedScreenInfoCache" />.
/// </remarks>
[PXInternalUseOnly]
public class LayeredScreenInfoCache : 
  IScreenInfoStorage,
  IScreenInfoCacheControl,
  ICacheControlledBy<PXSiteMap.ScreenInfo>,
  ICacheControl
{
  private readonly InMemoryScreenInfoCache _inMemoryCache;
  private readonly DistributedScreenInfoCache _distributedCache;
  private readonly ICacheControlledBy<ScreenUiTypeAbsence> _screenUiTypeAbsenceCache;

  public LayeredScreenInfoCache(
    InMemoryScreenInfoCache inMemoryCache,
    DistributedScreenInfoCache distributedCache,
    ICacheControlledBy<ScreenUiTypeAbsence> screenUiTypeAbsenceCache)
  {
    this._inMemoryCache = inMemoryCache ?? throw new ArgumentNullException(nameof (inMemoryCache));
    this._distributedCache = distributedCache ?? throw new ArgumentNullException(nameof (distributedCache));
    this._screenUiTypeAbsenceCache = screenUiTypeAbsenceCache;
  }

  public void Store(string screenId, string locale, PXSiteMap.ScreenInfo value)
  {
    this._inMemoryCache.Store(screenId, locale, value);
    this._distributedCache.Store(screenId, locale, value);
  }

  public PXSiteMap.ScreenInfo Get(string screenId, string locale)
  {
    PXSiteMap.ScreenInfo screenInfo = this._inMemoryCache.Get(screenId, locale);
    if (screenInfo == null)
    {
      screenInfo = this._distributedCache.Get(screenId, locale);
      if (screenInfo != null)
        this._inMemoryCache.Store(screenId, locale, screenInfo);
    }
    return screenInfo;
  }

  public void InvalidateCache()
  {
    this._inMemoryCache.InvalidateCache();
    this._distributedCache.InvalidateCache();
    this._screenUiTypeAbsenceCache.InvalidateCache();
  }

  public void InvalidateCache(string screenId)
  {
    this._inMemoryCache.InvalidateCache(screenId);
    this._distributedCache.InvalidateCache(screenId);
    this._screenUiTypeAbsenceCache.InvalidateCache();
  }

  public void InvalidateCache(string screenId, string locale)
  {
    this._inMemoryCache.InvalidateCache(screenId, locale);
    this._distributedCache.InvalidateCache(screenId, locale);
  }
}
