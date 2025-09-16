// Decompiled with JetBrains decompiler
// Type: PX.Metadata.InMemoryScreenInfoCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using PX.Caching;
using PX.Common;
using PX.Data;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Monads;
using System.Threading;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// <para>Provides in-memory <see cref="T:Microsoft.Extensions.Caching.Memory.IMemoryCache" />-based implementation of
/// <see cref="T:PX.Metadata.IScreenInfoStorage" /> and <see cref="T:PX.Metadata.IScreenInfoCacheControl" /> interfaces.</para>
/// </summary>
/// <remarks>
/// Made <see langword="public" /> for unit test purposes only.
/// </remarks>
[PXInternalUseOnly]
public class InMemoryScreenInfoCache : 
  IScreenInfoStorage,
  IScreenInfoCacheControl,
  ICacheControlledBy<PXSiteMap.ScreenInfo>,
  ICacheControl,
  IDisposable
{
  private readonly IMemoryCache _cache;
  private readonly IPXIdentityAccessor _identityAccessor;
  private readonly ILogger _logger;
  private readonly object _id = new object();
  private readonly ConcurrentDictionary<(string TenantId, string ScreenId), CancellationTokenSource> _ctsByScreenId = new ConcurrentDictionary<(string, string), CancellationTokenSource>();

  public InMemoryScreenInfoCache(
    IMemoryCache cache,
    IPXIdentityAccessor identityAccessor,
    ILogger logger)
  {
    this._cache = cache ?? throw new ArgumentNullException(nameof (cache));
    this._identityAccessor = identityAccessor ?? throw new ArgumentNullException(nameof (identityAccessor));
    this._logger = logger ?? throw new ArgumentNullException(nameof (logger));
    this._logger = this._logger.WithStack();
  }

  public void Store(string screenId, string locale, PXSiteMap.ScreenInfo value)
  {
    if (screenId == null)
      throw new ArgumentNullException(nameof (screenId));
    if (locale == null)
      throw new ArgumentNullException(nameof (locale));
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    CancellationTokenSource orAdd1 = this._ctsByScreenId.GetOrAdd((this._identityAccessor.Identity?.TenantId, screenId), (Func<(string, string), CancellationTokenSource>) (_ => new CancellationTokenSource()));
    CancellationTokenSource orAdd2 = this._ctsByScreenId.GetOrAdd((this._identityAccessor.Identity?.TenantId, (string) null), (Func<(string, string), CancellationTokenSource>) (_ => new CancellationTokenSource()));
    using (ICacheEntry entry = this._cache.CreateEntry(this.CreateKey(screenId, locale)))
    {
      entry.Value = (object) value;
      CacheEntryExtensions.AddExpirationToken(CacheEntryExtensions.AddExpirationToken(entry, (IChangeToken) new CancellationChangeToken(orAdd2.Token)), (IChangeToken) new CancellationChangeToken(orAdd1.Token));
    }
  }

  public PXSiteMap.ScreenInfo Get(string screenId, string locale)
  {
    if (screenId == null)
      throw new ArgumentNullException(nameof (screenId));
    if (locale == null)
      throw new ArgumentNullException(nameof (locale));
    PXSiteMap.ScreenInfo screenInfo = CacheExtensions.Get<PXSiteMap.ScreenInfo>(this._cache, this.CreateKey(screenId, locale));
    if (screenInfo != null)
      return screenInfo;
    this._logger.WithEventID("ScreenInfo", "CacheMiss").Debug<string, string>("Cache miss in the in-memory cache for {ScreenID} and {Locale}", screenId, locale);
    return screenInfo;
  }

  public void InvalidateCache()
  {
    CancellationTokenSource cancellationTokenSource;
    if (this._ctsByScreenId.TryRemove((this._identityAccessor.Identity?.TenantId, (string) null), out cancellationTokenSource))
    {
      cancellationTokenSource.Cancel();
      cancellationTokenSource.Dispose();
    }
    this._logger.WithEventID("ScreenInfo", nameof (InvalidateCache)).Debug("ScreenInfo in-memory cache invalidated");
  }

  public void InvalidateCache(string screenId)
  {
    if (screenId == null)
      throw new ArgumentNullException(nameof (screenId));
    CancellationTokenSource cancellationTokenSource;
    if (this._ctsByScreenId.TryRemove((this._identityAccessor.Identity?.TenantId, screenId), out cancellationTokenSource))
    {
      cancellationTokenSource.Cancel();
      cancellationTokenSource.Dispose();
    }
    this._logger.WithEventID("ScreenInfo", "InvalidateCacheForScreenID").Debug<string>("ScreenInfo in-memory cache invalidated for {ScreenID}", screenId);
  }

  public void InvalidateCache(string screenId, string locale)
  {
    if (screenId == null)
      throw new ArgumentNullException(nameof (screenId));
    if (locale == null)
      throw new ArgumentNullException(nameof (locale));
    this._cache.Remove(this.CreateKey(screenId, locale));
    this._logger.WithEventID("ScreenInfo", "InvalidateCacheForScreenIDAndLocale").Debug<string, string>("ScreenInfo in-memory cache invalidated for {ScreenID} and {Locale}", screenId, locale);
  }

  public void Dispose()
  {
    foreach (CancellationTokenSource cancellationTokenSource in (IEnumerable<CancellationTokenSource>) this._ctsByScreenId.Values)
    {
      MaybeObjects.TryDo<CancellationTokenSource>(cancellationTokenSource, (System.Action<CancellationTokenSource>) (c => c.Cancel()));
      cancellationTokenSource.Dispose();
    }
    this._ctsByScreenId.Clear();
  }

  private object CreateKey(string screenId, string locale)
  {
    return (object) (this._id, this._identityAccessor.Identity?.TenantId, screenId, locale, CachingKeys.UIType());
  }
}
