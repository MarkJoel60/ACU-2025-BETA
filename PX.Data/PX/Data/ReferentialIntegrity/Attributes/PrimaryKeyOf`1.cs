// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Begins a primary key declaration by defining a <typeparamref name="TTable" /> to which the PK is related.
/// </summary>
/// <typeparam name="TTable">The type of a table to which the PK is related.</typeparam>
public static class PrimaryKeyOf<TTable> where TTable : 
#nullable disable
class, IBqlTable, new()
{
  private static long? DirtyCacheAge;

  internal static TTable FindImpl(
    PXGraph graph,
    PKFindOptions options,
    Func<PXGraph, object, bool, TTable> entitySelector,
    object key)
  {
    if (key == null)
      return default (TTable);
    bool flag1 = (options & PKFindOptions.SkipGlobalCache) == PKFindOptions.SkipGlobalCache;
    bool flag2 = (options & PKFindOptions.IncludeDirty) == PKFindOptions.IncludeDirty;
    return PrimaryKeyOf<TTable>.UseGlobalCache && !flag1 && !flag2 ? PrimaryKeyOf<TTable>.GetFromGlobalCache(graph, key, (byte) 1, (Func<TTable>) (() => entitySelector(graph, key, true))) : entitySelector(graph, key, !flag2);
  }

  internal static TTable FindImpl(
    PXGraph graph,
    PKFindOptions options,
    Func<PXGraph, object[], bool, TTable> entitySelector,
    object[] keys)
  {
    if (keys == null || ((IEnumerable<object>) keys).Contains<object>((object) null))
      return default (TTable);
    bool flag1 = (options & PKFindOptions.SkipGlobalCache) == PKFindOptions.SkipGlobalCache;
    bool flag2 = (options & PKFindOptions.IncludeDirty) == PKFindOptions.IncludeDirty;
    if (!PrimaryKeyOf<TTable>.UseGlobalCache || flag1 || flag2)
      return entitySelector(graph, keys, !flag2);
    object itemKey = keys.Length > 1 ? (object) Composite.Create(keys) : keys[0];
    return PrimaryKeyOf<TTable>.GetFromGlobalCache(graph, itemKey, (byte) keys.Length, (Func<TTable>) (() => entitySelector(graph, keys, true)));
  }

  private static bool UseGlobalCache => PXCacheNameAttribute.UseGlobalCache(typeof (TTable));

  private static bool CanCacheItem(PXGraph graph, TTable item)
  {
    return typeof (TTable) == item.GetType() && graph.Caches<TTable>().GetStatus(item) == PXEntryStatus.Notchanged;
  }

  private static TTable GetFromGlobalCache(
    PXGraph graph,
    object itemKey,
    byte keysCount,
    Func<TTable> entitySelector)
  {
    if (PrimaryKeyOf<TTable>.DirtyCacheAge.HasValue)
    {
      if (PrimaryKeyOf<TTable>.DirtyCacheAge.Value == PXDatabase.Provider.AgeGlobal)
        return entitySelector();
      PrimaryKeyOf<TTable>.DirtyCacheAge = new long?();
    }
    PXSelectorAttribute.GlobalDictionary globalDictionary = PXSelectorAttribute.GlobalDictionary.GetOrCreate(typeof (TTable), typeof (TTable), keysCount);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    graph.RowPersisted.RemoveHandler<TTable>(PrimaryKeyOf<TTable>.\u003C\u003EO.\u003C0\u003E__RefreshAgeOnRowPersisted ?? (PrimaryKeyOf<TTable>.\u003C\u003EO.\u003C0\u003E__RefreshAgeOnRowPersisted = new PXRowPersisted(PrimaryKeyOf<TTable>.RefreshAgeOnRowPersisted)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    graph.RowPersisted.AddHandler<TTable>(PrimaryKeyOf<TTable>.\u003C\u003EO.\u003C0\u003E__RefreshAgeOnRowPersisted ?? (PrimaryKeyOf<TTable>.\u003C\u003EO.\u003C0\u003E__RefreshAgeOnRowPersisted = new PXRowPersisted(PrimaryKeyOf<TTable>.RefreshAgeOnRowPersisted)));
    lock (globalDictionary.SyncRoot)
    {
      PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
      if (globalDictionary.TryGetValue(itemKey, out cacheValue))
      {
        if (!cacheValue.IsDeleted)
        {
          if (cacheValue.Item != null)
          {
            if (cacheValue.Item is TTable fromGlobalCache)
              return fromGlobalCache;
          }
        }
      }
    }
    TTable row = entitySelector();
    if ((object) row != null && !PXDatabase.ReadDeleted && PrimaryKeyOf<TTable>.CanCacheItem(graph, row))
    {
      lock (globalDictionary.SyncRoot)
        globalDictionary.Set(itemKey, (object) row, false);
    }
    return row;
  }

  internal static void PutToGlobalCache(TTable item, object key)
  {
    PrimaryKeyOf<TTable>.PutToGlobalCache(item, key, (byte) 1);
  }

  internal static void PutToGlobalCache(TTable item, object[] keys)
  {
    PrimaryKeyOf<TTable>.PutToGlobalCache(item, (object) Composite.Create(keys), (byte) keys.Length);
  }

  private static void PutToGlobalCache(TTable item, object itemKey, byte keysCount)
  {
    if ((object) item == null || itemKey == null || PXDatabase.ReadDeleted || !PrimaryKeyOf<TTable>.UseGlobalCache)
      return;
    PXSelectorAttribute.GlobalDictionary globalDictionary = PXSelectorAttribute.GlobalDictionary.GetOrCreate(typeof (TTable), typeof (TTable), keysCount);
    lock (globalDictionary.SyncRoot)
      globalDictionary.Set(itemKey, (object) item, false);
  }

  private static void RefreshAgeOnRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    PrimaryKeyOf<TTable>.DirtyCacheAge = new long?(PXDatabase.Provider.AgeGlobal);
  }

  /// <summary>
  /// Finishes the primary key declaration by defining a single key field of the table.
  /// </summary>
  /// <typeparam name="TKeyComponent">A single key field of the table.</typeparam>
  public class By<TKeyComponent> : 
    TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent>,
    ITypeArrayOf<IBqlField>,
    TypeArray.IsNotEmpty,
    IPrimaryKeyOf<TTable>,
    IPrimaryKey
    where TKeyComponent : IBqlField
  {
    private static readonly BqlCommand Command = (BqlCommand) new Select<TTable, Where<TKeyComponent, Equal<Required<TKeyComponent>>>>();

    /// <summary>Finds an entity by the item's key.</summary>
    public static TTable Find(PXGraph graph, TTable item, PKFindOptions options = PKFindOptions.None)
    {
      return (object) item == null ? default (TTable) : PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, graph.Caches<TTable>().GetValue<TKeyComponent>((object) item), options);
    }

    /// <summary>Finds an entity by its key.</summary>
    protected static TTable FindBy(PXGraph graph, object key, PKFindOptions options = PKFindOptions.None)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return PrimaryKeyOf<TTable>.FindImpl(graph, options, PrimaryKeyOf<TTable>.By<TKeyComponent>.\u003C\u003EO.\u003C0\u003E__SelectEntity ?? (PrimaryKeyOf<TTable>.By<TKeyComponent>.\u003C\u003EO.\u003C0\u003E__SelectEntity = new Func<PXGraph, object, bool, TTable>(PrimaryKeyOf<TTable>.By<TKeyComponent>.SelectEntity)), key);
    }

    [Obsolete("The method is depricated, instead use the FindBy(PXGraph, object, PKFindOptions) method.")]
    [PXInternalUseOnly]
    protected static TTable FindBy(PXGraph graph, object key, bool isDirtyKey)
    {
      return PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, key, isDirtyKey ? PKFindOptions.SkipGlobalCache : PKFindOptions.None);
    }

    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.StoreResult(PX.Data.PXGraph,PX.Data.IBqlTable,System.Boolean)" />
    public static void StoreResult(PXGraph graph, TTable item, bool forDirtySelect = false)
    {
      if ((object) item == null || graph.Caches<TTable>().GetValue<TKeyComponent>((object) item) == null)
        return;
      PrimaryKeyOf<TTable>.By<TKeyComponent>.GetView(graph, !forDirtySelect).StoreResult((IBqlTable) item);
    }

    private static TTable SelectEntity(PXGraph graph, object key, bool isReadOnly)
    {
      object obj = (object) (isReadOnly ? default (TTable) : PrimaryKeyOf<TTable>.By<TKeyComponent>.EntityFromCache(graph, key));
      if (obj == null)
        obj = PrimaryKeyOf<TTable>.By<TKeyComponent>.GetView(graph, isReadOnly).SelectSingle(key);
      return (TTable) obj;
    }

    private static PXView GetView(PXGraph graph, bool isReadOnly)
    {
      return graph.TypedViews.GetView(PrimaryKeyOf<TTable>.By<TKeyComponent>.Command, isReadOnly);
    }

    private static TTable EntityFromCache(PXGraph graph, object key)
    {
      PXCache<TTable> pxCache = graph.Caches<TTable>();
      object instance = pxCache.CreateInstance();
      pxCache.SetValue(instance, typeof (TKeyComponent).Name, key);
      return (TTable) pxCache.Locate(instance);
    }

    IBqlTable IPrimaryKey.Find(PXGraph graph, IBqlTable item, PKFindOptions options)
    {
      return (IBqlTable) PrimaryKeyOf<TTable>.By<TKeyComponent>.Find(graph, (TTable) item, options);
    }

    TTable IPrimaryKeyOf<TTable>.Find(PXGraph graph, TTable item, PKFindOptions options)
    {
      return PrimaryKeyOf<TTable>.By<TKeyComponent>.Find(graph, item, options);
    }

    IBqlTable IPrimaryKey.Find(PXGraph graph, PKFindOptions options, params object[] keys)
    {
      return (IBqlTable) PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, keys[0], options);
    }

    TTable IPrimaryKeyOf<TTable>.Find(PXGraph graph, PKFindOptions options, params object[] keys)
    {
      return PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, keys[0], options);
    }

    void IPrimaryKey.StoreResult(PXGraph graph, IBqlTable item, bool forDirtySelect)
    {
      PrimaryKeyOf<TTable>.By<TKeyComponent>.StoreResult(graph, (TTable) item, forDirtySelect);
    }

    void IPrimaryKeyOf<TTable>.StoreResult(PXGraph graph, TTable item, bool forDirtySelect)
    {
      PrimaryKeyOf<TTable>.By<TKeyComponent>.StoreResult(graph, item, forDirtySelect);
    }

    /// <summary>
    /// Begins a declaration of a foreign key (FK) based on an existing primary key by defining a <typeparamref name="TChildTable" /> table to which the FK is related.
    /// </summary>
    /// <typeparam name="TChildTable">The type of the table to which the FK is related.</typeparam>
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining a single foreign key field of the child table.
      /// </summary>
      /// <typeparam name="TChildKeyComponent">A single foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent> : 
        Field<TChildKeyComponent>.IsRelatedTo<TKeyComponent>.AsSimpleKey.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent : IBqlField
      {
        /// <summary>Finds the parent entity that the child entity refers.</summary>
        public new static TTable FindParent(
          PXGraph graph,
          TChildTable child,
          PKFindOptions options = PKFindOptions.None)
        {
          if ((object) child == null)
            return default (TTable);
          object key = graph.Caches[KeysRelation<Field<TChildKeyComponent>.IsRelatedTo<TKeyComponent>.AsSimpleKey.WithTablesOf<TTable, TChildTable>, TTable, TChildTable>.Ref.Child.Table].GetValue((object) child, KeysRelation<Field<TChildKeyComponent>.IsRelatedTo<TKeyComponent>.AsSimpleKey.WithTablesOf<TTable, TChildTable>, TTable, TChildTable>.Ref.Child.KeyFields.First<System.Type>().Name);
          return PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, key, options);
        }
      }
    }

    public class Dirty : PrimaryKeyOf<TTable>.By<TKeyComponent>
    {
      /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.Find(PX.Data.PXGraph,`0,PX.Data.PKFindOptions)" />
      /// <remarks>Forced to include dirty records.</remarks>
      public new static TTable Find(PXGraph graph, TTable item, PKFindOptions options = PKFindOptions.None)
      {
        return PrimaryKeyOf<TTable>.By<TKeyComponent>.Find(graph, item, options | PKFindOptions.IncludeDirty);
      }

      /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.FindBy(PX.Data.PXGraph,System.Object,PX.Data.PKFindOptions)" />
      /// <remarks>Forced to include dirty records.</remarks>
      protected new static TTable FindBy(PXGraph graph, object key, PKFindOptions options = PKFindOptions.None)
      {
        return PrimaryKeyOf<TTable>.By<TKeyComponent>.FindBy(graph, key, options | PKFindOptions.IncludeDirty);
      }

      /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.StoreResult(PX.Data.PXGraph,`0,System.Boolean)" />
      /// <remarks>Forced to affect non readonly select.</remarks>
      public new static void StoreResult(PXGraph graph, TTable item, bool forDirtySelect = false)
      {
        PrimaryKeyOf<TTable>.By<TKeyComponent>.StoreResult(graph, item, ((forDirtySelect ? 1 : 0) | 1) != 0);
      }

      [Obsolete("The method is depricated, instead use the FindBy(PXGraph, object, PKFindOptions) method.")]
      [PXInternalUseOnly]
      protected new static TTable FindBy(PXGraph graph, object key, bool isDirtyKey)
      {
        return PrimaryKeyOf<TTable>.By<TKeyComponent>.Dirty.FindBy(graph, key, (PKFindOptions) (1 | (isDirtyKey ? 2 : 0)));
      }

      public static void PutToGlobalCache(PXGraph graph, TTable item)
      {
        object key = graph.Caches<TTable>().GetValue<TKeyComponent>((object) item);
        if (key == null)
          return;
        PrimaryKeyOf<TTable>.PutToGlobalCache(item, key);
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining two key fields of the table.
  /// </summary>
  /// <typeparam name="TKeyComponent1">The first key field of the table.</typeparam>
  /// <typeparam name="TKeyComponent2">The second key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2>>.FindImpl(graph, options, keyComponent1, keyComponent2);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining two foreign key fields of the child table.
      /// </summary>
      /// <typeparam name="TChildKeyComponent1">The first foreign key field of the child table.</typeparam>
      /// <typeparam name="TChildKeyComponent2">The second foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining three key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`2" />
  /// <typeparam name="TKeyComponent3">The third key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining three foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`2.ForeignKeyOf`1.By`2" />
      /// <typeparam name="TChildKeyComponent3">The third foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining four key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`3" />
  /// <typeparam name="TKeyComponent4">The fourth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining four foreign key field of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`3.ForeignKeyOf`1.By`3" />
      /// <typeparam name="TChildKeyComponent4">The fourth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining five key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`4" />
  /// <typeparam name="TKeyComponent5">The fifth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining five foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`4.ForeignKeyOf`1.By`4" />
      /// <typeparam name="TChildKeyComponent5">The fifth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining six key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`5" />
  /// <typeparam name="TKeyComponent6">The sixth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining six foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`5.ForeignKeyOf`1.By`5" />
      /// <typeparam name="TChildKeyComponent6">The sixth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining seven key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`6" />
  /// <typeparam name="TKeyComponent7">The seventh key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining seven foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`6.ForeignKeyOf`1.By`6" />
      /// <typeparam name="TChildKeyComponent7">The seventh foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining eight key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`7" />
  /// <typeparam name="TKeyComponent8">The eighth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
    where TKeyComponent8 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      object keyComponent8,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7, keyComponent8);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining eigth foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`7.ForeignKeyOf`1.By`7" />
      /// <typeparam name="TChildKeyComponent8">The eighth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7, TChildKeyComponent8> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>, Field<TChildKeyComponent8>.IsRelatedTo<TKeyComponent8>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
        where TChildKeyComponent8 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining ninth key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`8" />
  /// <typeparam name="TKeyComponent9">The ninth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
    where TKeyComponent8 : IBqlField
    where TKeyComponent9 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      object keyComponent8,
      object keyComponent9,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7, keyComponent8, keyComponent9);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining ninth foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`8.ForeignKeyOf`1.By`8" />
      /// <typeparam name="TChildKeyComponent9">The ninth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7, TChildKeyComponent8, TChildKeyComponent9> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>, Field<TChildKeyComponent8>.IsRelatedTo<TKeyComponent8>, Field<TChildKeyComponent9>.IsRelatedTo<TKeyComponent9>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
        where TChildKeyComponent8 : IBqlField
        where TChildKeyComponent9 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining tenth key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`9" />
  /// <typeparam name="TKeyComponent10">The tenth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
    where TKeyComponent8 : IBqlField
    where TKeyComponent9 : IBqlField
    where TKeyComponent10 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      object keyComponent8,
      object keyComponent9,
      object keyComponent10,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7, keyComponent8, keyComponent9, keyComponent10);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining tenth foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`9.ForeignKeyOf`1.By`9" />
      /// <typeparam name="TChildKeyComponent10">The tenth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7, TChildKeyComponent8, TChildKeyComponent9, TChildKeyComponent10> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>, Field<TChildKeyComponent8>.IsRelatedTo<TKeyComponent8>, Field<TChildKeyComponent9>.IsRelatedTo<TKeyComponent9>, Field<TChildKeyComponent10>.IsRelatedTo<TKeyComponent10>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
        where TChildKeyComponent8 : IBqlField
        where TChildKeyComponent9 : IBqlField
        where TChildKeyComponent10 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining eleventh key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`10" />
  /// <typeparam name="TKeyComponent11">The eleventh key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
    where TKeyComponent8 : IBqlField
    where TKeyComponent9 : IBqlField
    where TKeyComponent10 : IBqlField
    where TKeyComponent11 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      object keyComponent8,
      object keyComponent9,
      object keyComponent10,
      object keyComponent11,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7, keyComponent8, keyComponent9, keyComponent10, keyComponent11);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining eleventh foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`10.ForeignKeyOf`1.By`10" />
      /// <typeparam name="TChildKeyComponent11">The eleventh foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7, TChildKeyComponent8, TChildKeyComponent9, TChildKeyComponent10, TChildKeyComponent11> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>, Field<TChildKeyComponent8>.IsRelatedTo<TKeyComponent8>, Field<TChildKeyComponent9>.IsRelatedTo<TKeyComponent9>, Field<TChildKeyComponent10>.IsRelatedTo<TKeyComponent10>, Field<TChildKeyComponent11>.IsRelatedTo<TKeyComponent11>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
        where TChildKeyComponent8 : IBqlField
        where TChildKeyComponent9 : IBqlField
        where TChildKeyComponent10 : IBqlField
        where TChildKeyComponent11 : IBqlField
      {
      }
    }
  }

  /// <summary>
  /// Finishes the primary key declaration by defining twelfth key fields of the table.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`11" />
  /// <typeparam name="TKeyComponent12">The twelfth key field of the table.</typeparam>
  public class By<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11, TKeyComponent12> : 
    PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11, TKeyComponent12>>
    where TKeyComponent1 : IBqlField
    where TKeyComponent2 : IBqlField
    where TKeyComponent3 : IBqlField
    where TKeyComponent4 : IBqlField
    where TKeyComponent5 : IBqlField
    where TKeyComponent6 : IBqlField
    where TKeyComponent7 : IBqlField
    where TKeyComponent8 : IBqlField
    where TKeyComponent9 : IBqlField
    where TKeyComponent10 : IBqlField
    where TKeyComponent11 : IBqlField
    where TKeyComponent12 : IBqlField
  {
    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
    protected static TTable FindBy(
      PXGraph graph,
      object keyComponent1,
      object keyComponent2,
      object keyComponent3,
      object keyComponent4,
      object keyComponent5,
      object keyComponent6,
      object keyComponent7,
      object keyComponent8,
      object keyComponent9,
      object keyComponent10,
      object keyComponent11,
      object keyComponent12,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TKeyComponent1, TKeyComponent2, TKeyComponent3, TKeyComponent4, TKeyComponent5, TKeyComponent6, TKeyComponent7, TKeyComponent8, TKeyComponent9, TKeyComponent10, TKeyComponent11, TKeyComponent12>>.FindImpl(graph, options, keyComponent1, keyComponent2, keyComponent3, keyComponent4, keyComponent5, keyComponent6, keyComponent7, keyComponent8, keyComponent9, keyComponent10, keyComponent11, keyComponent12);
    }

    /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`1.ForeignKeyOf`1" />
    public static class ForeignKeyOf<TChildTable> where TChildTable : class, IBqlTable, new()
    {
      /// <summary>
      /// Finishes the foreign key declaration by defining eleventh foreign key fields of the child table.
      /// </summary>
      /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.PrimaryKeyOf`1.By`11.ForeignKeyOf`1.By`11" />
      /// <typeparam name="TChildKeyComponent12">The twelfth foreign key field of the child table.</typeparam>
      public class By<TChildKeyComponent1, TChildKeyComponent2, TChildKeyComponent3, TChildKeyComponent4, TChildKeyComponent5, TChildKeyComponent6, TChildKeyComponent7, TChildKeyComponent8, TChildKeyComponent9, TChildKeyComponent10, TChildKeyComponent11, TChildKeyComponent12> : 
        CompositeKey<Field<TChildKeyComponent1>.IsRelatedTo<TKeyComponent1>, Field<TChildKeyComponent2>.IsRelatedTo<TKeyComponent2>, Field<TChildKeyComponent3>.IsRelatedTo<TKeyComponent3>, Field<TChildKeyComponent4>.IsRelatedTo<TKeyComponent4>, Field<TChildKeyComponent5>.IsRelatedTo<TKeyComponent5>, Field<TChildKeyComponent6>.IsRelatedTo<TKeyComponent6>, Field<TChildKeyComponent7>.IsRelatedTo<TKeyComponent7>, Field<TChildKeyComponent8>.IsRelatedTo<TKeyComponent8>, Field<TChildKeyComponent9>.IsRelatedTo<TKeyComponent9>, Field<TChildKeyComponent10>.IsRelatedTo<TKeyComponent10>, Field<TChildKeyComponent11>.IsRelatedTo<TKeyComponent11>, Field<TChildKeyComponent12>.IsRelatedTo<TKeyComponent12>>.WithTablesOf<TTable, TChildTable>
        where TChildKeyComponent1 : IBqlField
        where TChildKeyComponent2 : IBqlField
        where TChildKeyComponent3 : IBqlField
        where TChildKeyComponent4 : IBqlField
        where TChildKeyComponent5 : IBqlField
        where TChildKeyComponent6 : IBqlField
        where TChildKeyComponent7 : IBqlField
        where TChildKeyComponent8 : IBqlField
        where TChildKeyComponent9 : IBqlField
        where TChildKeyComponent10 : IBqlField
        where TChildKeyComponent11 : IBqlField
        where TChildKeyComponent12 : IBqlField
      {
      }
    }
  }

  public abstract class MultiBy<TKeyComponents> : 
    TypeArrayOf<IBqlField>.Concat<TypeArrayOf<IBqlField>.Empty, TKeyComponents>,
    IPrimaryKeyOf<TTable>,
    IPrimaryKey
    where TKeyComponents : ITypeArrayOf<IBqlField>, TypeArray.IsNotEmpty
  {
    private static readonly Lazy<IEnumerable<System.Type>> KeyComponents = Lazy.By<IEnumerable<System.Type>>((Func<IEnumerable<System.Type>>) (() => (IEnumerable<System.Type>) TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TKeyComponents), (string) null)));
    private static readonly Lazy<BqlCommand> Command = Lazy.By<BqlCommand>((Func<BqlCommand>) (() => BqlCommand.CreateInstance(typeof (Select<,>), typeof (TTable), PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.KeyComponents.Value.Select<System.Type, FieldAndParameter>((Func<System.Type, FieldAndParameter>) (f => new FieldAndParameter(f, f, typeof (Required<>)))).ToArray<FieldAndParameter>().ToWhere())));

    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.IBqlTable,PX.Data.PKFindOptions)" />
    public static TTable Find(PXGraph graph, TTable item, PKFindOptions options = PKFindOptions.None)
    {
      return (object) item == null ? default (TTable) : PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.FindImpl(graph, options, PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.GetKeyValues(graph, item).ToArray<object>());
    }

    internal static TTable FindImpl(PXGraph graph, PKFindOptions options, params object[] keys)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return PrimaryKeyOf<TTable>.FindImpl(graph, options, PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.\u003C\u003EO.\u003C0\u003E__SelectEntity ?? (PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.\u003C\u003EO.\u003C0\u003E__SelectEntity = new Func<PXGraph, object[], bool, TTable>(PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.SelectEntity)), keys);
    }

    /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.StoreResult(PX.Data.PXGraph,PX.Data.IBqlTable,System.Boolean)" />
    public static void StoreResult(PXGraph graph, TTable item, bool forDirtySelect = false)
    {
      if ((object) item == null || !PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.GetKeyValues(graph, item).All<object>((Func<object, bool>) (kv => kv != null)))
        return;
      PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.GetView(graph, !forDirtySelect).StoreResult((IBqlTable) item);
    }

    private protected static TTable SelectEntity(PXGraph graph, object[] keys, bool isReadOnly)
    {
      return (TTable) ((object) (isReadOnly ? default (TTable) : PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.EntityFromCache(graph, (IEnumerable<object>) keys)) ?? PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.GetView(graph, isReadOnly).SelectSingle(keys));
    }

    private static PXView GetView(PXGraph graph, bool isReadOnly)
    {
      return graph.TypedViews.GetView(PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.Command.Value, isReadOnly);
    }

    private static IEnumerable<object> GetKeyValues(PXGraph graph, TTable item)
    {
      if ((object) item != null)
      {
        PXCache<TTable> cache = graph.Caches<TTable>();
        foreach (MemberInfo memberInfo in PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.KeyComponents.Value)
          yield return cache.GetValue((object) item, memberInfo.Name);
      }
    }

    private static TTable EntityFromCache(PXGraph graph, IEnumerable<object> keys)
    {
      PXCache<TTable> pxCache = graph.Caches<TTable>();
      object instance = pxCache.CreateInstance();
      foreach (Tuple<System.Type, object> tuple in EnumerableExtensions.Zip<System.Type, object>(PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.KeyComponents.Value, keys))
      {
        System.Type type1;
        object obj1;
        tuple.Deconstruct<System.Type, object>(out type1, out obj1);
        System.Type type2 = type1;
        object obj2 = obj1;
        pxCache.SetValue(instance, type2.Name, obj2);
      }
      return (TTable) pxCache.Locate(instance);
    }

    IBqlTable IPrimaryKey.Find(PXGraph graph, IBqlTable item, PKFindOptions options)
    {
      return (IBqlTable) PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.Find(graph, (TTable) item, options);
    }

    TTable IPrimaryKeyOf<TTable>.Find(PXGraph graph, TTable item, PKFindOptions options)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.Find(graph, item, options);
    }

    IBqlTable IPrimaryKey.Find(PXGraph graph, PKFindOptions options, params object[] keys)
    {
      return (IBqlTable) PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.FindImpl(graph, options, keys);
    }

    TTable IPrimaryKeyOf<TTable>.Find(PXGraph graph, PKFindOptions options, params object[] keys)
    {
      return PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.FindImpl(graph, options, keys);
    }

    void IPrimaryKey.StoreResult(PXGraph graph, IBqlTable item, bool forDirtySelect)
    {
      PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.StoreResult(graph, (TTable) item, forDirtySelect);
    }

    void IPrimaryKeyOf<TTable>.StoreResult(PXGraph graph, TTable item, bool forDirtySelect)
    {
      PrimaryKeyOf<TTable>.MultiBy<TKeyComponents>.StoreResult(graph, item, forDirtySelect);
    }
  }
}
