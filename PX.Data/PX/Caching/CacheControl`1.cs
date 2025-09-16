// Decompiled with JetBrains decompiler
// Type: PX.Caching.CacheControl`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// <para>A static helper to control cache invalidation for a particular cache marked by <typeparamref name="TCacheName" />.</para>
/// <para>Use DI via injecting <see cref="T:PX.Caching.ICacheControl`1" /> whenever possible.</para>
/// </summary>
/// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
[PXInternalUseOnly]
public static class CacheControl<TCacheName>
{
  public static void InvalidateCache()
  {
    ICacheControl<TCacheName> cacheControl;
    if (!ServiceLocator.IsLocationProviderSet || !ServiceLocatorExtensions.TryGetInstance<ICacheControl<TCacheName>>(ServiceLocator.Current, ref cacheControl))
      return;
    cacheControl.InvalidateCache();
  }
}
