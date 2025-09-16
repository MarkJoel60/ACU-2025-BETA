// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.ResultSet.PXAsyncQueryableResultset`1
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

internal class PXAsyncQueryableResultset<T0> : 
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
  private PXDelayedQuery _DelayedQueryPersisted;
  private PXDelayedQuery _DelayedQuery;
  private readonly CancellationToken _cancellationToken;
  private AsyncSQLQueryable<PXResult<T0>> _relinqHelper;

  internal AsyncSQLQueryable<PXResult<T0>> _RelinqHelper
  {
    get
    {
      if (this._relinqHelper == null)
        this._relinqHelper = new AsyncSQLQueryable<PXResult<T0>>(this._DelayedQueryPersisted?.View.Graph, (IPXResultsetBase) this);
      return this._relinqHelper;
    }
  }

  /// <inheritdoc />
  public PXDelayedQuery GetDelayedQuery() => this._DelayedQueryPersisted;

  /// <inheritdoc />
  public object GetCollection() => throw new NotImplementedException();

  /// <inheritdoc />
  public PXAsyncQueryableResultset(PXDelayedQuery src, CancellationToken cancellationToken)
  {
    this._DelayedQuery = src;
    this._cancellationToken = cancellationToken;
    if (!(src.View.GetType() == typeof (PXView)))
      return;
    this._DelayedQueryPersisted = src;
  }

  public IAsyncEnumerator<PXResult<T0>> GetAsyncEnumerator(CancellationToken cancellationToken)
  {
    return (IAsyncEnumerator<PXResult<T0>>) new PXAsyncResultsetEnumerator<T0>((IAsyncEnumerable<PXResult<T0>>) this._RelinqHelper, CancellationTokenSource.CreateLinkedTokenSource(this._cancellationToken, cancellationToken));
  }

  /// <inheritdoc />
  public Expression Expression => this._RelinqHelper.Expression;

  /// <inheritdoc />
  public System.Type ElementType => this._RelinqHelper.ElementType;

  /// <inheritdoc />
  IQueryProvider IQueryable.Provider => this._RelinqHelper.Provider;

  /// <inheritdoc />
  public IEnumerator<PXResult<T0>> GetEnumerator() => throw new NotImplementedException();

  /// <inheritdoc />
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  /// <inheritdoc />
  IAsyncQueryProvider IAsyncQueryable.Provider => this._RelinqHelper.AsyncProvider;
}
