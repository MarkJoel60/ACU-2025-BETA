// Decompiled with JetBrains decompiler
// Type: PX.Caching.ContainerBuilderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Common;

#nullable disable
namespace PX.Caching;

[PXInternalUseOnly]
public static class ContainerBuilderExtensions
{
  /// <summary>
  /// Registers a new Cache Version implementation that
  /// can be used in <see cref="T:PX.Caching.CacheVersion`1" /> static helper
  /// or directly as <see cref="T:PX.Caching.ICacheVersion`1" /> after the registration.
  /// </summary>
  /// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
  [PXInternalUseOnly]
  public static IRegistrationBuilder<ICacheVersion<TCacheName>, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterDbCacheVersion<TCacheName>(
    this ContainerBuilder builder)
  {
    return (IRegistrationBuilder<ICacheVersion<TCacheName>, ConcreteReflectionActivatorData, SingleRegistrationStyle>) RegistrationExtensions.RegisterType<DbCacheVersion<TCacheName>>(builder).As<ICacheVersion<TCacheName>>().SingleInstance();
  }

  /// <summary>
  /// <para>Registers a new Cache Control implementation that utilizes cache versioning for cache invalidation.</para>
  /// <para>After the registration, it can be resolved either as <see cref="T:PX.Caching.ICacheControl" /> or <see cref="T:PX.Caching.ICacheControl`1" />.
  /// Additionally, if <typeparamref name="TCacheName" /> is derived from <see cref="T:PX.Caching.VersionedCacheControl`1" />,
  /// it can also be resolved as <typeparamref name="TCacheName" /> (but be aware that it always returns a single instance).</para>
  /// <para>Make sure that your cache utilizes <see cref="T:PX.Caching.ICacheVersion`1" /> either directly
  /// or by using <see cref="M:PX.Caching.CachingKeys.CacheVersion``1(PX.Caching.ICacheVersion{``0})" /> as a part of the key.</para>
  /// </summary>
  /// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
  /// <example><para>In the example below, the versioned cache control is registered using a marker type</para>
  /// <code title="Example" lang="CS">
  /// builder.RegisterVersionedCacheControl&lt;PXSiteMap.ScreenInfo&gt;();
  /// </code>
  /// <code title="Example2" description="In the example below, the versioned cache control is registered using a VersionedCacheControl&lt;TCacheName&gt; inheritor that can be explicitly resolved later" lang="CS">
  /// public class DashboardsCacheControl : VersionedCacheControl&lt;DashboardsCacheControl&gt; { }
  /// ...
  /// builder.RegisterVersionedCacheControl&lt;DashboardsCacheControl&gt;();
  /// </code>
  /// </example>
  [PXInternalUseOnly]
  public static IRegistrationBuilder<VersionedCacheControl<TCacheName>, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterVersionedCacheControl<TCacheName>(
    this ContainerBuilder builder)
  {
    return (!typeof (VersionedCacheControl<TCacheName>).IsAssignableFrom(typeof (TCacheName)) ? RegistrationExtensions.RegisterType<VersionedCacheControl<TCacheName>>(builder) : (IRegistrationBuilder<VersionedCacheControl<TCacheName>, ConcreteReflectionActivatorData, SingleRegistrationStyle>) RegistrationExtensions.AsSelf<TCacheName, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<TCacheName>(builder))).As<ICacheControlledBy<TCacheName>>().As<ICacheControl>().SingleInstance();
  }
}
