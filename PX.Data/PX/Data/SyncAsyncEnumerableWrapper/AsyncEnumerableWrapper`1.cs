// Decompiled with JetBrains decompiler
// Type: PX.Data.SyncAsyncEnumerableWrapper.AsyncEnumerableWrapper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SyncAsyncEnumerableWrapper;

internal class AsyncEnumerableWrapper<T> : IEnumerableLinqWrapper<T>
{
  private readonly IAsyncEnumerable<T> _source;

  public AsyncEnumerableWrapper(IAsyncEnumerable<T> source) => this._source = source;

  /// <inheritdoc />
  public IEnumerable<T> AsEnumerable() => AsyncEnumerable.ToEnumerable<T>(this._source);

  /// <inheritdoc />
  public IAsyncEnumerable<T> AsAsyncEnumerable() => this._source;

  /// <inheritdoc />
  public IEnumerableLinqWrapper<TResult> Select<TResult>(Func<T, TResult> selector)
  {
    return (IEnumerableLinqWrapper<TResult>) new AsyncEnumerableWrapper<TResult>(AsyncEnumerable.Select<T, TResult>(this._source, selector));
  }

  /// <inheritdoc />
  public IEnumerableLinqWrapper<T> Where(Func<T, bool> filter)
  {
    return (IEnumerableLinqWrapper<T>) new AsyncEnumerableWrapper<T>(AsyncEnumerable.Where<T>(this._source, filter));
  }
}
