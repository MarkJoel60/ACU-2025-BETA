// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.GroupedCollectionUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Scopes;

[PXInternalUseOnly]
public static class GroupedCollectionUtils
{
  public static GroupedCollection<TEntity, TCollection> SplitBy<TEntity, TCollection>(
    this TCollection collection,
    PXCache<TEntity> cache,
    Type[] byFields)
    where TEntity : class, IBqlTable, new()
    where TCollection : ICollection<TEntity>, new()
  {
    KeyValuesComparer<TEntity> comparer = new KeyValuesComparer<TEntity>((PXCache) cache, (IEnumerable<Type>) byFields);
    KeyValuesComparer<TEntity> keyValuesComparer = new KeyValuesComparer<TEntity>((PXCache) cache, (IEnumerable<Type>) ((PXCache) cache).BqlKeys);
    return new GroupedCollection<TEntity, TCollection>((PXCache) cache, (IEqualityComparer<TEntity>) comparer)
    {
      UniqueKeyComparer = (IEqualityComparer<TEntity>) keyValuesComparer
    };
  }

  public static GroupedCollection<TEntity, GroupedCollection<TEntity, TCollection>> SplitBy<TEntity, TCollection>(
    this GroupedCollection<TEntity, TCollection> collection,
    Type[] byFields,
    GroupItemsLoadHandler<TEntity> itemLoader)
    where TEntity : class, IBqlTable, new()
    where TCollection : ICollection<TEntity>, new()
  {
    return (GroupedCollection<TEntity, GroupedCollection<TEntity, TCollection>>) new UpperGroupedCollection<TEntity, GroupedCollection<TEntity, TCollection>>((IEqualityComparer<TEntity>) new KeyValuesComparer<TEntity>(collection.Cache, (IEnumerable<Type>) byFields), itemLoader, collection);
  }
}
