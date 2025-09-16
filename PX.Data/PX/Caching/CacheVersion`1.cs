// Decompiled with JetBrains decompiler
// Type: PX.Caching.CacheVersion`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// <para>A static helper to work with a particular cache version.</para>
/// <para>Use DI via injecting <see cref="T:PX.Caching.ICacheVersion`1" /> whenever possible.</para>
/// </summary>
/// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
[PXInternalUseOnly]
public static class CacheVersion<TCacheName>
{
  private static readonly ICacheVersion<TCacheName> _service;

  public static int Current => CacheVersion<TCacheName>._service.Current;

  public static void Increment() => CacheVersion<TCacheName>._service.Increment();

  static CacheVersion()
  {
    ICacheVersion<TCacheName> cacheVersion;
    CacheVersion<TCacheName>._service = !ServiceLocator.IsLocationProviderSet || !ServiceLocatorExtensions.TryGetInstance<ICacheVersion<TCacheName>>(ServiceLocator.Current, ref cacheVersion) ? (ICacheVersion<TCacheName>) new CacheVersion<TCacheName>.DummyCacheVersion() : cacheVersion;
  }

  private class DummyCacheVersion : ICacheVersion<TCacheName>
  {
    public int Current => 0;

    public void Increment()
    {
    }
  }
}
