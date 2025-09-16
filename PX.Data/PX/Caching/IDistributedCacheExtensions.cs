// Decompiled with JetBrains decompiler
// Type: PX.Caching.IDistributedCacheExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Caching.Distributed;
using PX.Common;
using PX.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Caching;

[PXInternalUseOnly]
public static class IDistributedCacheExtensions
{
  private static readonly 
  #nullable disable
  IDistributedObjectSerializer _serializer = (IDistributedObjectSerializer) new PXReflectionDistributedObjectSerializer();

  public static bool TryGet<T>(
    this IDistributedCache cache,
    out T value,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
  {
    return cache.TryGet<T>(out value, prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static bool TryGet<T>(
    this IDistributedCache cache,
    out T value,
    string prefix,
    params Func<object>[] additionalKeys)
  {
    return cache.TryGet<T>(out value, prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static bool TryGet<T>(
    this IDistributedCache cache,
    out T value,
    string prefix,
    params object[] additionalKeys)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    string str = prefix != null ? CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)) : throw new ArgumentNullException(nameof (prefix));
    return DistributedCacheSerializationExtensions.TryGet<T>(cache, IDistributedCacheExtensions._serializer, str, ref value);
  }

  public static T Get<T>(
    this IDistributedCache cache,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
    where T : class
  {
    return cache.Get<T>(prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task<T> GetAsync<T>(
    this IDistributedCache cache,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
    where T : class
  {
    return cache.GetAsync<T>(prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static T Get<T>(
    this IDistributedCache cache,
    string prefix,
    params Func<object>[] additionalKeys)
    where T : class
  {
    return cache.Get<T>(prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task<T> GetAsync<T>(
    this IDistributedCache cache,
    string prefix,
    params Func<object>[] additionalKeys)
    where T : class
  {
    return cache.GetAsync<T>(prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static T Get<T>(
    this IDistributedCache cache,
    string prefix,
    params object[] additionalKeys)
    where T : class
  {
    T obj;
    return !cache.TryGet<T>(out obj, prefix, additionalKeys) ? default (T) : obj;
  }

  public static async Task<T> GetAsync<T>(
    this IDistributedCache cache,
    string prefix,
    params object[] additionalKeys)
    where T : class
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    string str = prefix != null ? CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)) : throw new ArgumentNullException(nameof (prefix));
    return await DistributedCacheSerializationExtensions.GetAsync<T>(cache, IDistributedCacheExtensions._serializer, str, new CancellationToken());
  }

  public static void Set<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
  {
    cache.Set<T>(value, options, prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task SetAsync<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
  {
    return cache.SetAsync<T>(value, options, prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static void Set<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    params Func<object>[] additionalKeys)
  {
    cache.Set<T>(value, options, prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task SetAsync<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    params Func<object>[] additionalKeys)
  {
    return cache.SetAsync<T>(value, options, prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static void Set<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    params object[] additionalKeys)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if ((object) value == null)
      throw new ArgumentNullException(nameof (value));
    string str = prefix != null ? CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)) : throw new ArgumentNullException(nameof (prefix));
    DistributedCacheSerializationExtensions.Set<T>(cache, IDistributedCacheExtensions._serializer, str, value, options);
  }

  public static async Task SetAsync<T>(
    this IDistributedCache cache,
    T value,
    DistributedCacheEntryOptions options,
    string prefix,
    params object[] additionalKeys)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if ((object) (T) value == null)
      throw new ArgumentNullException(nameof (value));
    string str = prefix != null ? CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)) : throw new ArgumentNullException(nameof (prefix));
    await DistributedCacheSerializationExtensions.SetAsync<T>(cache, IDistributedCacheExtensions._serializer, str, value, options, new CancellationToken());
  }

  public static void Remove(
    this IDistributedCache cache,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
  {
    cache.Remove(prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task RemoveAsync(
    this IDistributedCache cache,
    string prefix,
    IEnumerable<Func<object>> additionalKeys)
  {
    return cache.RemoveAsync(prefix, additionalKeys.Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static void Remove(
    this IDistributedCache cache,
    string prefix,
    params Func<object>[] additionalKeys)
  {
    cache.Remove(prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static Task RemoveAsync(
    this IDistributedCache cache,
    string prefix,
    params Func<object>[] additionalKeys)
  {
    return cache.RemoveAsync(prefix, ((IEnumerable<Func<object>>) additionalKeys).Select<Func<object>, object>((Func<Func<object>, object>) (k => k())).ToArray<object>());
  }

  public static void Remove(
    this IDistributedCache cache,
    string prefix,
    params object[] additionalKeys)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    string str = prefix != null ? CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)) : throw new ArgumentNullException(nameof (prefix));
    cache.Remove(str);
  }

  public static async Task RemoveAsync(
    this IDistributedCache cache,
    string prefix,
    params object[] additionalKeys)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (prefix == null)
      throw new ArgumentNullException(nameof (prefix));
    await cache.RemoveAsync(CachingKeys.ToString(EnumerableExtensions.Prepend<object>(additionalKeys, (object) prefix)), new CancellationToken());
  }

  private sealed class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      RegistrationExtensions.RegisterInstance<IDistributedObjectSerializer>(builder, IDistributedCacheExtensions._serializer).As<IDistributedObjectSerializer>();
    }
  }
}
