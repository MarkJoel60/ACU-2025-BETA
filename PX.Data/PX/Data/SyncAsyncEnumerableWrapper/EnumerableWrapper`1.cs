// Decompiled with JetBrains decompiler
// Type: PX.Data.SyncAsyncEnumerableWrapper.EnumerableWrapper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SyncAsyncEnumerableWrapper;

internal class EnumerableWrapper<T> : IEnumerableLinqWrapper<T>
{
  private readonly IEnumerable<T> _source;

  public EnumerableWrapper(IEnumerable<T> source) => this._source = source;

  /// <inheritdoc />
  public IEnumerable<T> AsEnumerable() => this._source;

  /// <inheritdoc />
  public IAsyncEnumerable<T> AsAsyncEnumerable()
  {
    return AsyncEnumerable.ToAsyncEnumerable<T>(this._source);
  }

  /// <inheritdoc />
  public IEnumerableLinqWrapper<TResult> Select<TResult>(Func<T, TResult> selector)
  {
    return (IEnumerableLinqWrapper<TResult>) new EnumerableWrapper<TResult>(this._source.Select<T, TResult>(selector));
  }

  /// <inheritdoc />
  public IEnumerableLinqWrapper<T> Where(Func<T, bool> filter)
  {
    return (IEnumerableLinqWrapper<T>) new EnumerableWrapper<T>(this._source.Where<T>(filter));
  }
}
