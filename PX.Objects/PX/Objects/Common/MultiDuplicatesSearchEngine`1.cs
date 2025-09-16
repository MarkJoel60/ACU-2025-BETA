// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.MultiDuplicatesSearchEngine`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public class MultiDuplicatesSearchEngine<TEntity> : DuplicatesSearchEngineBase<TEntity> where TEntity : class, IBqlTable, new()
{
  protected readonly Dictionary<TEntity, List<TEntity>> _items;

  public MultiDuplicatesSearchEngine(PXCache cache, IEnumerable<Type> keyFields)
    : base(cache, keyFields)
  {
    this._items = new Dictionary<TEntity, List<TEntity>>((IEqualityComparer<TEntity>) this._comparator);
  }

  public MultiDuplicatesSearchEngine(
    PXCache cache,
    IEnumerable<Type> keyFields,
    ICollection<TEntity> items)
    : this(cache, keyFields)
  {
    foreach (TEntity entity in (IEnumerable<TEntity>) items)
      this.AddItem(entity);
  }

  public override void AddItem(TEntity item)
  {
    List<TEntity> entityList;
    if (this._items.TryGetValue(item, out entityList))
      entityList.Add(item);
    else
      this._items.Add(item, new List<TEntity>() { item });
  }

  public override TEntity Find(TEntity item) => this[item].FirstOrDefault<TEntity>();

  public virtual bool RemoveItem(TEntity item)
  {
    List<TEntity> entityList;
    if (!this._items.TryGetValue(item, out entityList) || !entityList.Remove(item))
      return false;
    if (entityList.Count == 0)
      this._items.Remove(item);
    return true;
  }

  public IEnumerable<TEntity> this[TEntity item]
  {
    get
    {
      List<TEntity> entityList;
      return this._items.TryGetValue(item, out entityList) ? (IEnumerable<TEntity>) entityList : (IEnumerable<TEntity>) Array<TEntity>.Empty;
    }
  }

  public virtual IEnumerable<TEntity> this[IDictionary itemValues]
  {
    get => this[this.CreateEntity(itemValues)];
  }
}
