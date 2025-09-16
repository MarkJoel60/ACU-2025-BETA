// Decompiled with JetBrains decompiler
// Type: PX.Metadata.DistributedScreenInfoCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// <para>Provides persistent <see cref="T:Microsoft.Extensions.Caching.Distributed.IDistributedCache" />-based implementation of
/// <see cref="T:PX.Metadata.IScreenInfoStorage" /> and <see cref="T:PX.Metadata.IScreenInfoCacheControl" /> interfaces.</para>
/// </summary>
/// <remarks>
/// Made <see langword="public" /> for unit test purposes only.
/// </remarks>
[PXInternalUseOnly]
public class DistributedScreenInfoCache : 
  VersionedCacheControl<PXSiteMap.ScreenInfo>,
  IScreenInfoCacheControl,
  ICacheControlledBy<PXSiteMap.ScreenInfo>,
  ICacheControl,
  IScreenInfoStorage
{
  private const string CachePrefix = "DistributedScreenInfoCache";
  private readonly IDistributedCache _distributedCache;
  private readonly DistributedCacheEntryOptions _cacheEntryOptions;
  private readonly IAppInstanceInfo _appInstanceInfo;
  private readonly IPXIdentityAccessor _identityAccessor;
  private readonly ICacheVersion<PXSiteMap.ScreenInfo> _cacheVersion;
  private readonly Func<IEnumerable<string>> _getSupportedLocales;
  private readonly IScreenInfoVersion _screenInfoVersion;
  private readonly ILogger _logger;
  private readonly ICacheControlledBy<ScreenUiTypeAbsence> _screenUiTypeAbsenceCache;

  public DistributedScreenInfoCache(
    IDistributedCache distributedCache,
    IAppInstanceInfo appInstanceInfo,
    IPXIdentityAccessor identityAccessor,
    ICacheVersion<PXSiteMap.ScreenInfo> cacheVersion,
    Func<IEnumerable<string>> getSupportedLocales,
    IScreenInfoVersion screenInfoVersion,
    ILogger logger,
    IOptions<DistributedScreenInfoCacheOptions> options,
    ICacheControlledBy<ScreenUiTypeAbsence> screenUiTypeAbsenceCache)
    : base(cacheVersion)
  {
    this._distributedCache = distributedCache ?? throw new ArgumentNullException(nameof (distributedCache));
    this._appInstanceInfo = appInstanceInfo ?? throw new ArgumentNullException(nameof (appInstanceInfo));
    this._identityAccessor = identityAccessor ?? throw new ArgumentNullException(nameof (identityAccessor));
    this._cacheVersion = cacheVersion ?? throw new ArgumentNullException(nameof (cacheVersion));
    this._getSupportedLocales = getSupportedLocales ?? throw new ArgumentNullException(nameof (getSupportedLocales));
    this._screenInfoVersion = screenInfoVersion;
    this._logger = logger ?? throw new ArgumentNullException(nameof (logger));
    this._screenUiTypeAbsenceCache = screenUiTypeAbsenceCache;
    this._logger = this._logger.WithStack();
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this._cacheEntryOptions = new DistributedCacheEntryOptions()
    {
      SlidingExpiration = new TimeSpan?(options.Value.SlidingExpiration)
    };
  }

  private IEnumerable<Func<object>> GetKeys(string screenId, string locale)
  {
    return (IEnumerable<Func<object>>) new Func<object>[9]
    {
      CachingKeys.InstallationID(this._appInstanceInfo),
      CachingKeys.AppVersion(this._appInstanceInfo),
      CachingKeys.IsPortal(this._appInstanceInfo),
      CachingKeys.Tenant(this._identityAccessor),
      CachingKeys.CacheVersion<PXSiteMap.ScreenInfo>(this._cacheVersion),
      (Func<object>) (() => (object) this._screenInfoVersion.GetCurrent(screenId)),
      (Func<object>) (() => (object) screenId),
      (Func<object>) (() => (object) locale),
      CachingKeys.UITypeKey()
    };
  }

  public void Store(string screenId, string locale, PXSiteMap.ScreenInfo value)
  {
    this.VerifyLocale(locale);
    this._distributedCache.Set<PXSiteMap.ScreenInfo>(value, this._cacheEntryOptions, nameof (DistributedScreenInfoCache), this.GetKeys(screenId, locale));
  }

  public PXSiteMap.ScreenInfo Get(string screenId, string locale)
  {
    this.VerifyLocale(locale);
    PXSiteMap.ScreenInfo screenInfo = this._distributedCache.Get<PXSiteMap.ScreenInfo>(nameof (DistributedScreenInfoCache), this.GetKeys(screenId, locale));
    if (screenInfo != null)
      return screenInfo;
    this._logger.WithEventID("ScreenInfo", "CacheMiss").Debug<string, string>("Cache miss in the distributed cache for {ScreenID} and {Locale}", screenId, locale);
    return screenInfo;
  }

  public override void InvalidateCache()
  {
    base.InvalidateCache();
    this._screenUiTypeAbsenceCache.InvalidateCache();
    this._logger.WithEventID("ScreenInfo", nameof (InvalidateCache)).Debug("ScreenInfo distributed cache invalidated");
  }

  public void InvalidateCache(string screenId)
  {
    foreach (string locale in this._getSupportedLocales())
      this._distributedCache.Remove(nameof (DistributedScreenInfoCache), this.GetKeys(screenId, locale));
    this._distributedCache.Remove(nameof (DistributedScreenInfoCache), this.GetKeys(screenId, CultureInfo.InvariantCulture.Name));
    this._screenInfoVersion.Increment(screenId);
    this._screenUiTypeAbsenceCache.InvalidateCache();
    this._logger.WithEventID("ScreenInfo", "InvalidateCacheForScreenID").Debug<string>("ScreenInfo distributed cache invalidated for {ScreenID}", screenId);
  }

  public void InvalidateCache(string screenId, string locale)
  {
    this.VerifyLocale(locale);
    this._distributedCache.Remove(nameof (DistributedScreenInfoCache), this.GetKeys(screenId, locale));
    this._screenInfoVersion.Increment(screenId);
    this._logger.WithEventID("ScreenInfo", "InvalidateCacheForScreenIDAndLocale").Debug<string, string>("ScreenInfo distributed cache invalidated for {ScreenID} and {Locale}", screenId, locale);
  }

  private void VerifyLocale(string locale)
  {
    if (locale == null)
      throw new ArgumentNullException(nameof (locale));
    if (!(locale == CultureInfo.InvariantCulture.Name) && !this._getSupportedLocales().Contains<string>(locale))
      throw new NotSupportedException($"Locale \"{locale}\" does not exist in the system or is not marked as active.");
  }
}
