// Decompiled with JetBrains decompiler
// Type: PX.Data.EnumerableHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal static class EnumerableHelper
{
  /// <summary>
  /// Constructs an enumerable sequence that depends on a resource object, whose lifetime is tied to the resulting enumerable sequence's lifetime.
  /// </summary>
  /// <typeparam name="TSource">The type of the elements in the produced sequence.</typeparam>
  /// <typeparam name="TResource">The type of the resource used during the generation of the resulting sequence. Needs to implement <see cref="T:System.IDisposable" />.</typeparam>
  /// <param name="resourceFactory">Factory function to obtain a resource object.</param>
  /// <param name="enumerableFactory">Factory function to obtain an enumerable sequence that depends on the obtained resource.</param>
  /// <returns>An enumerable sequence whose lifetime controls the lifetime of the dependent resource object.</returns>
  /// <exception cref="T:System.ArgumentNullException"><paramref name="resourceFactory" /> or <paramref name="enumerableFactory" /> is null.</exception>
  public static IEnumerable<TSource> Using<TSource, TResource>(
    Func<TResource> resourceFactory,
    Func<TResource, IEnumerable<TSource>> enumerableFactory)
    where TResource : IDisposable
  {
    ExceptionExtensions.ThrowOnNull<Func<TResource>>(resourceFactory, nameof (resourceFactory), (string) null);
    ExceptionExtensions.ThrowOnNull<Func<TResource, IEnumerable<TSource>>>(enumerableFactory, nameof (enumerableFactory), (string) null);
    TResource resource = resourceFactory();
    try
    {
      foreach (TSource source in enumerableFactory(resource))
        yield return source;
    }
    finally
    {
      resource?.Dispose();
    }
    resource = default (TResource);
  }
}
