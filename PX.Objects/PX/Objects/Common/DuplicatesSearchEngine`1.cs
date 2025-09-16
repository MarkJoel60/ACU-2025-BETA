// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DuplicatesSearchEngine`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public class DuplicatesSearchEngine<TEntity> : DuplicatesSearchEngineBase<TEntity> where TEntity : class, IBqlTable, new()
{
  protected readonly Dictionary<TEntity, TEntity> _items;

  public DuplicatesSearchEngine(
    PXCache cache,
    IEnumerable<Type> keyFields,
    ICollection<TEntity> items)
    : base(cache, keyFields)
  {
    this._items = new Dictionary<TEntity, TEntity>((IEqualityComparer<TEntity>) this._comparator);
    foreach (TEntity entity in (IEnumerable<TEntity>) items)
      this.AddItem(entity);
  }

  public override TEntity Find(TEntity item)
  {
    TEntity entity;
    return !this._items.TryGetValue(item, out entity) ? default (TEntity) : entity;
  }

  public override void AddItem(TEntity item)
  {
    if (this._items.ContainsKey(item))
      return;
    this._items.Add(item, item);
  }
}
