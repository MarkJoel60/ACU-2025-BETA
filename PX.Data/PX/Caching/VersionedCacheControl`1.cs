// Decompiled with JetBrains decompiler
// Type: PX.Caching.VersionedCacheControl`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// <para>A Cache Control implementation that utilizes cache versioning for cache invalidation.</para>
/// <para>Can be used as a base class for a type that can be used as a <typeparamref name="TCacheName" />
/// during the registration, e.g. <code lang="CS">class FooCacheControl : VersionedCacheControl&lt;FooCacheControl&gt;</code></para>
/// <para>Use <see cref="M:PX.Caching.ContainerBuilderExtensions.RegisterVersionedCacheControl``1(Autofac.ContainerBuilder)" /> extension method
/// on <see cref="T:Autofac.ContainerBuilder" /> to register your derived type in DI to be able to resolve it
/// either explicitly (i.e., as <tt>FooCacheControl</tt>) or as <see cref="T:PX.Caching.ICacheControl" />
/// or <see cref="T:PX.Caching.ICacheControl`1" />.</para>
/// </summary>
/// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
[PXInternalUseOnly]
public class VersionedCacheControl<TCacheName> : ICacheControlledBy<TCacheName>, ICacheControl
{
  private readonly ICacheVersion<TCacheName> _cacheVersion;

  public VersionedCacheControl(ICacheVersion<TCacheName> cacheVersion)
  {
    this._cacheVersion = cacheVersion;
  }

  public virtual void InvalidateCache() => this._cacheVersion.Increment();
}
