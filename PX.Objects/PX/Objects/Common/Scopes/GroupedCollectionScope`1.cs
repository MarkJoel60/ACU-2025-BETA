// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.GroupedCollectionScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Scopes;

public class GroupedCollectionScope<TEntity> : CacheOperationsScope<TEntity> where TEntity : class, IBqlTable, new()
{
  private readonly IGroupedCollection<TEntity> _collection;

  public GroupedCollectionScope(IGroupedCollection<TEntity> collection)
    : base(collection.Cache.Graph)
  {
    this._collection = collection;
  }

  protected override void RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    this._collection.Insert((TEntity) e.Row);
  }

  protected override void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this._collection.Update((TEntity) e.OldRow, (TEntity) e.Row);
  }

  protected override void RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this._collection.Delete((TEntity) e.Row);
  }
}
