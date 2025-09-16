// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.CacheOperationsScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class CacheOperationsScope<TEntity> : IDisposable where TEntity : class, IBqlTable, new()
{
  protected PXGraph Graph { get; }

  protected CacheOperationsScope(PXGraph graph) => this.Graph = graph;

  public virtual void SubscribeCacheEvents()
  {
    PXGraph.RowInsertedEvents rowInserted = this.Graph.RowInserted;
    CacheOperationsScope<TEntity> cacheOperationsScope1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cacheOperationsScope1, __vmethodptr(cacheOperationsScope1, RowInserted));
    rowInserted.AddHandler<TEntity>(pxRowInserted);
    PXGraph.RowUpdatedEvents rowUpdated = this.Graph.RowUpdated;
    CacheOperationsScope<TEntity> cacheOperationsScope2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cacheOperationsScope2, __vmethodptr(cacheOperationsScope2, RowUpdated));
    rowUpdated.AddHandler<TEntity>(pxRowUpdated);
    PXGraph.RowDeletedEvents rowDeleted = this.Graph.RowDeleted;
    CacheOperationsScope<TEntity> cacheOperationsScope3 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cacheOperationsScope3, __vmethodptr(cacheOperationsScope3, RowDeleted));
    rowDeleted.AddHandler<TEntity>(pxRowDeleted);
  }

  public virtual void UnSubscribeCacheEvents()
  {
    PXGraph.RowInsertedEvents rowInserted = this.Graph.RowInserted;
    CacheOperationsScope<TEntity> cacheOperationsScope1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cacheOperationsScope1, __vmethodptr(cacheOperationsScope1, RowInserted));
    rowInserted.RemoveHandler<TEntity>(pxRowInserted);
    PXGraph.RowUpdatedEvents rowUpdated = this.Graph.RowUpdated;
    CacheOperationsScope<TEntity> cacheOperationsScope2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cacheOperationsScope2, __vmethodptr(cacheOperationsScope2, RowUpdated));
    rowUpdated.RemoveHandler<TEntity>(pxRowUpdated);
    PXGraph.RowDeletedEvents rowDeleted = this.Graph.RowDeleted;
    CacheOperationsScope<TEntity> cacheOperationsScope3 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cacheOperationsScope3, __vmethodptr(cacheOperationsScope3, RowDeleted));
    rowDeleted.RemoveHandler<TEntity>(pxRowDeleted);
  }

  protected abstract void RowInserted(PXCache cache, PXRowInsertedEventArgs e);

  protected abstract void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e);

  protected abstract void RowDeleted(PXCache cache, PXRowDeletedEventArgs e);

  public virtual void Dispose() => this.UnSubscribeCacheEvents();
}
