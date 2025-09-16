// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.ResultSet.PXAsyncResultset`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Async;
using PX.Data.SQLTree.Linq.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable disable
namespace PX.Data.Database.ResultSet;

/// <exclude />
internal class PXAsyncResultset<T0> : 
  IPXAsyncResultset<PXResult<T0>>,
  IPXResultsetBase,
  IAsyncQueryable<PXResult<T0>>,
  IAsyncEnumerable<PXResult<T0>>,
  IAsyncQueryable,
  IQueryable<PXResult<T0>>,
  IEnumerable<PXResult<T0>>,
  IEnumerable,
  IQueryable
  where T0 : class, IBqlTable, new()
{
  private readonly IAsyncEnumerable<PXResult<T0>> _data;
  private readonly CancellationToken _cancellationToken;

  public PXAsyncResultset(IAsyncEnumerable<PXResult<T0>> data, CancellationToken cancellationToken)
  {
    this._data = data ?? throw new ArgumentNullException(nameof (data));
    this._cancellationToken = cancellationToken;
  }

  /// <inheritdoc />
  public virtual IAsyncEnumerator<PXResult<T0>> GetAsyncEnumerator(
    CancellationToken cancellationToken)
  {
    return (IAsyncEnumerator<PXResult<T0>>) new PXAsyncResultsetEnumerator<T0>(this._data, CancellationTokenSource.CreateLinkedTokenSource(this._cancellationToken, cancellationToken));
  }

  /// <inheritdoc />
  public PXDelayedQuery GetDelayedQuery() => (PXDelayedQuery) null;

  /// <inheritdoc />
  public object GetCollection() => (object) AsyncEnumerable.ToEnumerable<PXResult<T0>>(this._data);

  /// <inheritdoc />
  IEnumerator<PXResult<T0>> IEnumerable<PXResult<T0>>.GetEnumerator()
  {
    return this.GetEnumeratorInternal();
  }

  /// <inheritdoc />
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumeratorInternal();

  protected virtual IEnumerator<PXResult<T0>> GetEnumeratorInternal()
  {
    return AsyncEnumerable.ToEnumerable<PXResult<T0>>(this._data).GetEnumerator();
  }

  /// <inheritdoc />
  public Expression Expression => throw new NotImplementedException();

  /// <inheritdoc />
  public System.Type ElementType { get; } = typeof (PXResult<T0>);

  /// <inheritdoc />
  IQueryProvider IQueryable.Provider => throw new NotImplementedException();

  /// <inheritdoc />
  IAsyncQueryProvider IAsyncQueryable.Provider => throw new NotImplementedException();
}
