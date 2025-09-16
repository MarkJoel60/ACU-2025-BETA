// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.GroupedCollection`2
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
namespace PX.Objects.Common.Scopes;

[PXInternalUseOnly]
public class GroupedCollection<TEntity, TCollection> : 
  IGroupedCollection<TEntity>,
  ICollection<TEntity>,
  IEnumerable<TEntity>,
  IEnumerable
  where TEntity : class, IBqlTable, new()
  where TCollection : ICollection<TEntity>, new()
{
  protected Dictionary<TEntity, TCollection> Groups;
  protected IEqualityComparer<TEntity> Comparer;
  protected GroupItemsLoadHandler<TEntity> ItemsLoader;

  public IEqualityComparer<TEntity> UniqueKeyComparer { get; set; }

  public PXCache Cache { get; private set; }

  public int Count
  {
    get
    {
      Dictionary<TEntity, TCollection> groups = this.Groups;
      return groups == null ? 0 : __nonvirtual (groups.Count);
    }
  }

  public bool IsReadOnly => false;

  public GroupedCollection()
  {
  }

  public GroupedCollection(PXCache cache, IEqualityComparer<TEntity> comparer)
  {
    this.Cache = cache;
    this.Comparer = comparer;
  }

  protected GroupedCollection(
    PXCache cache,
    IEqualityComparer<TEntity> comparer,
    GroupItemsLoadHandler<TEntity> itemsLoader)
    : this(cache, comparer)
  {
    this.ItemsLoader = itemsLoader;
  }

  public void Clear() => this.Groups?.Clear();

  public virtual void Insert(TEntity data)
  {
    this.PrepareGroup();
    TCollection collection = this.PrepareCollection(data);
    collection.Add(data);
    if (this.Groups.ContainsKey(data))
      return;
    this.Groups.Add(data, collection);
  }

  protected void PrepareGroup()
  {
    this.Groups = this.Groups ?? new Dictionary<TEntity, TCollection>(this.Comparer);
  }

  public virtual void InsertRange(TEntity rangeKey, IEnumerable<TEntity> range)
  {
    this.PrepareGroup();
    foreach (IGrouping<TEntity, TEntity> grouping in (IEnumerable<IGrouping<TEntity, TEntity>>) range.ToLookup<TEntity, TEntity>((Func<TEntity, TEntity>) (x => x), this.Comparer))
    {
      TCollection collection;
      if (this.Groups.TryGetValue(grouping.Key, out collection))
        throw new KeyNotFoundException();
      collection = this.PrepareCollection(grouping.Key);
      foreach (TEntity entity in (IEnumerable<TEntity>) grouping)
        collection.Add(entity);
      this.Groups.Add(grouping.Key, collection);
    }
  }

  public virtual void Update(TEntity oldData, TEntity newData)
  {
    this.ThrowIfNoCollection(oldData);
    TCollection source = this.PrepareCollection(oldData);
    if (this.UniqueKeyComparer != null)
      oldData = source.FirstOrDefault<TEntity>((Func<TEntity, bool>) (x => this.UniqueKeyComparer.Equals(x, oldData)));
    if ((object) oldData != null)
      source.Remove(oldData);
    source.Add(newData);
  }

  public virtual void Delete(TEntity data)
  {
    this.ThrowIfNoCollection(data);
    TCollection source = this.PrepareCollection(data);
    if (this.UniqueKeyComparer != null)
      data = source.FirstOrDefault<TEntity>((Func<TEntity, bool>) (x => this.UniqueKeyComparer.Equals(x, data)));
    if ((object) data == null)
      return;
    source.Remove(data);
  }

  public IEnumerable<TEntity> GetItems(TEntity group)
  {
    this.PrepareGroup();
    TCollection collection;
    if (!this.Groups.TryGetValue(group, out collection))
    {
      IEnumerable<TEntity> range = this.LoadItems(group);
      if (range == null)
        return (IEnumerable<TEntity>) Array<TEntity>.Empty;
      this.InsertRange(group, range);
      if (!this.Groups.TryGetValue(group, out collection))
        return (IEnumerable<TEntity>) Array<TEntity>.Empty;
    }
    return this.GetItems(group, collection);
  }

  protected virtual IEnumerable<TEntity> GetItems(TEntity group, TCollection collection)
  {
    return (IEnumerable<TEntity>) collection.ToArray<TEntity>();
  }

  public IEnumerable<TEntity> LoadItems(TEntity group)
  {
    return this.ItemsLoader == null ? (IEnumerable<TEntity>) null : this.ItemsLoader(group);
  }

  protected virtual TCollection PrepareCollection(TEntity group)
  {
    TCollection collection;
    return this.Groups != null && this.Groups.TryGetValue(group, out collection) ? collection : new TCollection();
  }

  public void Add(TEntity item) => this.Insert(item);

  public bool Contains(TEntity item)
  {
    TCollection collection;
    return this.Groups != null && this.Groups.TryGetValue(item, out collection) && collection.Contains(item);
  }

  public void CopyTo(TEntity[] array, int arrayIndex) => throw new NotImplementedException();

  public virtual IGroupedCollection<TEntity> Clone()
  {
    return (IGroupedCollection<TEntity>) new GroupedCollection<TEntity, TCollection>(this.Cache, this.Comparer, this.ItemsLoader)
    {
      UniqueKeyComparer = this.UniqueKeyComparer
    };
  }

  public bool Remove(TEntity item)
  {
    this.Delete(item);
    return true;
  }

  public IEnumerator<TEntity> GetEnumerator()
  {
    Dictionary<TEntity, TCollection> groups = this.Groups;
    return groups == null ? (IEnumerator<TEntity>) null : groups.Values.SelectMany<TCollection, TEntity>((Func<TCollection, IEnumerable<TEntity>>) (x => (IEnumerable<TEntity>) x)).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  protected void ThrowIfNoCollection(TEntity group)
  {
    if (this.Groups == null || !this.Groups.ContainsKey(group))
      throw new NullReferenceException();
  }
}
