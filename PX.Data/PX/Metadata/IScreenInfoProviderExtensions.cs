// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IScreenInfoProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using System;
using System.Globalization;

#nullable disable
namespace PX.Metadata;

public static class IScreenInfoProviderExtensions
{
  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using the current locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen (in the current locale),
  /// or <see langword="null" /> if the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo Get(this IScreenInfoProvider provider, string screenId)
  {
    return provider.Get(screenId, IScreenInfoProviderExtensions.GetCurrentLocale());
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen data id using the current locale.
  /// </summary>
  /// <param name="id">Data ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen data id (in the current locale),
  /// or <see langword="null" /> if the screen does not exist.</returns>
  internal static PXSiteMap.ScreenInfo Get<T>(this IScreenInfoCollector<T> collector, T id)
  {
    return collector.Collect(id, IScreenInfoProviderExtensions.GetCurrentLocale());
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using invariant locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen (in invariant locale),
  /// or <see langword="null" /> if the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo GetWithInvariantLocale(
    this IScreenInfoProvider provider,
    string screenId)
  {
    return provider.Get(screenId, CultureInfo.InvariantCulture.Name);
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen data id using invariant locale.
  /// </summary>
  /// <param name="screenId">Data ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen data id (in invariant locale),
  /// or <see langword="null" /> if the screen does not exist.</returns>
  internal static PXSiteMap.ScreenInfo GetWithInvariantLocale<T>(
    this IScreenInfoCollector<T> collector,
    T id)
  {
    using (new PXInvariantCultureScope())
      return collector.Get<T>(id);
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using the current locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen (in the current locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo TryGet(this IScreenInfoProvider provider, string screenId)
  {
    return provider.TryGet(screenId, out Exception _);
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using the current locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="error">Exception that was thrown during the <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection (if any).</param>
  /// <returns>Screen metadata for the specific screen (in the current locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo TryGet(
    this IScreenInfoProvider provider,
    string screenId,
    out Exception error)
  {
    try
    {
      error = (Exception) null;
      return provider.Get(screenId);
    }
    catch (Exception ex)
    {
      error = ex;
      return (PXSiteMap.ScreenInfo) null;
    }
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using invariant locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <returns>Screen metadata for the specific screen (in invariant locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo TryGetWithInvariantLocale(
    this IScreenInfoProvider provider,
    string screenId)
  {
    return provider.TryGetWithInvariantLocale(screenId, out Exception _);
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using invariant locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="error">Exception that was thrown during the <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection (if any).</param>
  /// <returns>Screen metadata for the specific screen (in invariant locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo TryGetWithInvariantLocale(
    this IScreenInfoProvider provider,
    string screenId,
    out Exception error)
  {
    try
    {
      error = (Exception) null;
      return provider.Get(screenId, CultureInfo.InvariantCulture.Name);
    }
    catch (Exception ex)
    {
      error = ex;
      return (PXSiteMap.ScreenInfo) null;
    }
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using invariant locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="error">Exception that was thrown during the <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection (if any).</param>
  /// <returns>Screen metadata for the specific screen (in invariant locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  public static PXSiteMap.ScreenInfo TryGetWithInvariantLocale<T>(
    this IScreenInfoCollector<T> screenInfoCollector,
    T id,
    out Exception error)
  {
    try
    {
      error = (Exception) null;
      return screenInfoCollector.Collect(id, CultureInfo.InvariantCulture.Name);
    }
    catch (Exception ex)
    {
      error = ex;
      return (PXSiteMap.ScreenInfo) null;
    }
  }

  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen using the current locale.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="error">Exception that was thrown during the <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection (if any).</param>
  /// <returns>Screen metadata for the specific screen (in invariant locale);
  /// <see langword="null" />, if there were any errors during <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> collection
  /// or the screen does not exist.</returns>
  internal static PXSiteMap.ScreenInfo TryGet<T>(
    this IScreenInfoCollector<T> screenInfoCollector,
    T id,
    out Exception error)
  {
    try
    {
      error = (Exception) null;
      return screenInfoCollector.Collect(id, IScreenInfoProviderExtensions.GetCurrentLocale());
    }
    catch (Exception ex)
    {
      error = ex;
      return (PXSiteMap.ScreenInfo) null;
    }
  }

  [PXInternalUseOnly]
  public static Content GetScreenInfo(
    this IScreenInfoProvider screenInfoProvider,
    string screenId,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false)
  {
    return (forceDefaultLocale ? screenInfoProvider.GetWithInvariantLocale(screenId) : screenInfoProvider.Get(screenId)).GetScreenInfo(screenId, appendDescriptors, includeHiddenFields, forceDefaultLocale);
  }

  internal static Content GetScreenInfo<T>(
    this IScreenInfoCollector<T> screenInfoCollector,
    T id,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false)
  {
    return (forceDefaultLocale ? screenInfoCollector.GetWithInvariantLocale<T>(id) : screenInfoCollector.Get<T>(id)).GetScreenInfo((string) null, appendDescriptors, includeHiddenFields, forceDefaultLocale);
  }

  internal static Content GetScreenInfoWithServiceCommands<T>(
    this IScreenInfoCollector<T> screenInfoCollector,
    T id,
    bool appendDescriptors,
    bool includeHiddenFields,
    bool forceDefaultLocale = false)
  {
    return (forceDefaultLocale ? screenInfoCollector.GetWithInvariantLocale<T>(id) : screenInfoCollector.Get<T>(id)).GetScreenInfoWithServiceCommands((string) null, appendDescriptors, includeHiddenFields, forceDefaultLocale);
  }

  [PXInternalUseOnly]
  public static Content GetScreenInfoWithServiceCommands(
    this IScreenInfoProvider screenInfoProvider,
    bool appendDescriptors,
    bool includeHiddenFields,
    string screenID,
    bool forceDefaultLocale = false)
  {
    return (forceDefaultLocale ? screenInfoProvider.GetWithInvariantLocale(screenID) : screenInfoProvider.Get(screenID)).GetScreenInfoWithServiceCommands(screenID, appendDescriptors, includeHiddenFields, forceDefaultLocale);
  }

  private static string GetCurrentLocale()
  {
    string currentLocale = (string) null;
    if (ServiceLocator.IsLocationProviderSet)
      currentLocale = ServiceLocator.Current.GetInstance<IPXIdentityAccessor>().Identity?.Culture.Name;
    if (string.IsNullOrEmpty(currentLocale))
      currentLocale = PXLocalesProvider.GetCurrentLocale();
    return currentLocale;
  }
}
