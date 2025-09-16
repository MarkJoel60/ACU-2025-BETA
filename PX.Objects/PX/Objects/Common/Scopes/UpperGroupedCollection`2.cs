// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.UpperGroupedCollection`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Scopes;

[PXInternalUseOnly]
public class UpperGroupedCollection<TEntity, TCollection> : GroupedCollection<TEntity, TCollection>
  where TEntity : class, IBqlTable, new()
  where TCollection : IGroupedCollection<TEntity>, new()
{
  protected TCollection ChildTemplate;

  public UpperGroupedCollection()
  {
  }

  public UpperGroupedCollection(
    IEqualityComparer<TEntity> comparer,
    GroupItemsLoadHandler<TEntity> itemsLoader,
    TCollection childTemplate)
    : base(childTemplate.Cache, comparer, itemsLoader)
  {
    this.ChildTemplate = childTemplate;
  }

  public override void Insert(TEntity data)
  {
    this.ThrowIfNoCollection(data);
    TCollection collection = this.PrepareCollection(data);
    collection.Insert(data);
    if (this.Groups.ContainsKey(data))
      return;
    this.Groups.Add(data, collection);
  }

  public override void InsertRange(TEntity group, IEnumerable<TEntity> range)
  {
    List<TEntity> list = range.Where<TEntity>((Func<TEntity, bool>) (x => this.Comparer.Equals(group, x))).ToList<TEntity>();
    TCollection collection = this.PrepareCollection(group);
    collection.InsertRange(group, (IEnumerable<TEntity>) list);
    this.Groups.Add(group, collection);
  }

  public override void Update(TEntity oldData, TEntity newData)
  {
    this.ThrowIfNoCollection(oldData);
    this.PrepareCollection(oldData).Update(oldData, newData);
  }

  public override void Delete(TEntity data)
  {
    this.ThrowIfNoCollection(data);
    this.PrepareCollection(data).Delete(data);
  }

  protected override IEnumerable<TEntity> GetItems(TEntity group, TCollection collection)
  {
    return collection.GetItems(group);
  }

  public override IGroupedCollection<TEntity> Clone()
  {
    return (IGroupedCollection<TEntity>) new UpperGroupedCollection<TEntity, TCollection>(this.Comparer, this.ItemsLoader, this.ChildTemplate);
  }

  protected override TCollection PrepareCollection(TEntity group)
  {
    TCollection collection;
    if (this.Groups != null && this.Groups.TryGetValue(group, out collection))
      return collection;
    return (object) this.ChildTemplate == null ? base.PrepareCollection(group) : (TCollection) this.ChildTemplate.Clone();
  }
}
