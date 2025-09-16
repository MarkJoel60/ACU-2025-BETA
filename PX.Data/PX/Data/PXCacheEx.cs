// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheEx
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

public static class PXCacheEx
{
  private const string AttributesFieldNameSuffix = "_Attributes";

  public static IEqualityComparer<object> GetComparer(this PXCache cache)
  {
    return (IEqualityComparer<object>) new PXCacheEx.CacheItemComparer<object>(cache);
  }

  public static IEqualityComparer<TNode> GetComparer<TNode>(this PXCache<TNode> cache) where TNode : class, IBqlTable, new()
  {
    return (IEqualityComparer<TNode>) new PXCacheEx.CacheItemComparer<TNode>((PXCache) cache);
  }

  public static TExtension GetExtension<TExtension>(this IBqlTable item) where TExtension : PXCacheExtension
  {
    if (item == null)
      return default (TExtension);
    PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    lock (((ICollection) extensionCollection).SyncRoot)
    {
      PXCacheExtension[] source;
      if (extensionCollection.TryGetValue(item, out source))
        return (TExtension) ((IEnumerable<PXCacheExtension>) source).FirstOrDefault<PXCacheExtension>((Func<PXCacheExtension, bool>) (x => x is TExtension && x.GetType() == typeof (TExtension)));
    }
    throw new PXException("GetItemExtension failed.");
  }

  public static TExtension GetExtension<TBqlTable, TExtension>(this TBqlTable item)
    where TBqlTable : class, IBqlTable, new()
    where TExtension : PXCacheExtension<TBqlTable>
  {
    return PXCache<TBqlTable>.GetExtension<TExtension>(item);
  }

  public static PXCacheExtension[] GetExtensions(this IBqlTable item)
  {
    if (item == null)
      return (PXCacheExtension[]) null;
    PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    lock (((ICollection) extensionCollection).SyncRoot)
    {
      PXCacheExtension[] extensions;
      if (extensionCollection.TryGetValue(item, out extensions))
        return extensions;
    }
    return (PXCacheExtension[]) null;
  }

  /// <summary>
  /// Provides an object that allows to adjust the state of a <see cref="F:PX.Data.PXAttributeLevel.Item" />-attribute of the <see cref="T:PX.Data.PXUIFieldAttribute" /> type, that is attached to the cache, in runtime.
  /// </summary>
  /// <param name="cache">Cache which the attribute is attached to.</param>
  /// <param name="row">Row which the attribute should be adjusted for.</param>
  public static PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> AdjustUI(
    this PXCache cache,
    object row = null)
  {
    return new PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>(cache, row, false);
  }

  /// <summary>
  /// Provides an object that allows to adjust the state of a <see cref="F:PX.Data.PXAttributeLevel.Item" />-attribute of a specific type, that is attached to the cache, in runtime.
  /// </summary>
  /// <typeparam name="TAttribute">Type of the adjusted attribute.</typeparam>
  /// <param name="cache">Cache which the attribute is attached to.</param>
  /// <param name="row">Row which the attribute should be adjusted for.</param>
  public static PXCacheEx.AttributeAdjuster<TAttribute> Adjust<TAttribute>(
    this PXCache cache,
    object row = null)
    where TAttribute : PXEventSubscriberAttribute
  {
    return new PXCacheEx.AttributeAdjuster<TAttribute>(cache, row, false);
  }

  /// <summary>
  /// Provides an object that allows to adjust the state of a <see cref="F:PX.Data.PXAttributeLevel.Cache" />-attribute of the <see cref="T:PX.Data.PXUIFieldAttribute" /> type, that is attached to the cache, in runtime.
  /// </summary>
  /// <param name="cache">Cache which the attribute is attached to.</param>
  /// <param name="row">Row which the attribute should be adjusted for.</param>
  public static PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> AdjustUIReadonly(
    this PXCache cache,
    object row = null)
  {
    return new PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>(cache, row, true);
  }

  /// <summary>
  /// Provides an object that allows to adjust the state of a <see cref="F:PX.Data.PXAttributeLevel.Cache" />-attribute of a specific type, that is attached to the cache, in runtime.
  /// </summary>
  /// <typeparam name="TAttribute">Adjusted attribute type.</typeparam>
  /// <param name="cache">Cache which the attribute is attached to.</param>
  /// <param name="row">Row which the attribute should be adjusted for.</param>
  public static PXCacheEx.AttributeAdjuster<TAttribute> AdjustReadonly<TAttribute>(
    this PXCache cache,
    object row = null)
    where TAttribute : PXEventSubscriberAttribute
  {
    return new PXCacheEx.AttributeAdjuster<TAttribute>(cache, row, true);
  }

  public static ValueSetter<TEntity> GetSetterFor<TEntity>(this PXGraph graph, TEntity entity) where TEntity : class, IBqlTable, new()
  {
    return (graph != null ? (PXCache) graph.Caches<TEntity>() : (PXCache) null).GetSetterFor<TEntity>(entity);
  }

  public static ValueSetter<TEntity> GetSetterFor<TEntity>(
    this PXSelectBase<TEntity> select,
    TEntity entity)
    where TEntity : class, IBqlTable, new()
  {
    return select?.Cache.GetSetterFor<TEntity>(entity);
  }

  public static ValueSetter<TEntity> GetSetterForCurrent<TEntity>(this PXSelectBase<TEntity> select) where TEntity : class, IBqlTable, new()
  {
    return select?.Cache.GetSetterFor<TEntity>(select != null ? select.Current : default (TEntity));
  }

  public static ValueSetter<TEntity> GetSetterFor<TEntity>(this PXCache cache, TEntity entity) where TEntity : class, IBqlTable, new()
  {
    PXCacheEx.ValidateParameters<TEntity>(cache, entity);
    return new ValueSetter<TEntity>(cache, entity, false);
  }

  private static void ValidateParameters<TEntity>(PXCache cache, TEntity entity) where TEntity : class, IBqlTable, new()
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    ExceptionExtensions.ThrowOnNull<TEntity>(entity, nameof (entity), (string) null);
  }

  public static IDictionary<string, (object LeftValue, object RightValue)> GetDifference(
    this PXCache cache,
    IBqlTable left,
    IBqlTable right,
    bool allowDifferentTypes = false)
  {
    Dictionary<string, (object, object)> difference = new Dictionary<string, (object, object)>();
    if (cache == null || left == null || right == null)
      return (IDictionary<string, (object, object)>) null;
    if (!allowDifferentTypes && left.GetType() != right.GetType())
    {
      difference[typeof (System.Type).FullName] = ((object) left.GetType().FullName, (object) right.GetType().FullName);
      return (IDictionary<string, (object, object)>) difference;
    }
    foreach (string field in (List<string>) cache.Fields)
    {
      if (!(field == "Base"))
      {
        object objA = cache.GetValue((object) left, field);
        object objB = cache.GetValue((object) right, field);
        if ((objA != null || objB != null) && !object.Equals(objA, objB))
          difference[field] = (objA, objB);
      }
    }
    return (IDictionary<string, (object, object)>) difference;
  }

  /// <summary>
  /// Checks if a given <paramref name="cache" /> has a corresponding table in the DB.
  /// Cases like inheritance via <see cref="T:PX.Data.PXTableAttribute">PXTable</see>, projections and so on are also handled by this method.
  /// </summary>
  /// <param name="cache">Cache that the table belongs to</param>
  /// <returns />
  [PXInternalUseOnly]
  public static bool TableExistsInDb(this PXCache cache)
  {
    return cache.GetDbTableSchemas().Any<TableHeader>();
  }

  /// <summary>
  /// Get the DB table schemas for DAC if the table (or tables from projection) exists in the DB.
  /// Cases like inheritance via <see cref="T:PX.Data.PXTableAttribute">PXTable</see>, projections and so on are also handled by this method.
  /// </summary>
  /// <param name="cache">Cache that the table belongs to</param>
  /// <returns></returns>
  [PXInternalUseOnly]
  public static IEnumerable<TableHeader> GetDbTableSchemas(this PXCache cache)
  {
    TableHeader tableStructure1 = PXDatabase.Provider.GetTableStructure(cache.BqlTable.Name);
    if (tableStructure1 == null && cache.BqlSelect != null)
    {
      System.Type[] typeArray = cache.BqlSelect.GetTables();
      for (int index = 0; index < typeArray.Length; ++index)
      {
        string name = PXCache.GetBqlTable(typeArray[index])?.Name;
        if (!string.IsNullOrEmpty(name))
        {
          TableHeader tableStructure2 = PXDatabase.Provider.GetTableStructure(name);
          if (tableStructure2 != null)
            yield return tableStructure2;
        }
      }
      typeArray = (System.Type[]) null;
    }
    else if (tableStructure1 != null)
      yield return tableStructure1;
  }

  /// <summary>
  /// Returns non-mapped DAC extension types that contain the DAC field with the specified <paramref name="fieldName" /> and
  /// have <paramref name="dacType" /> as their last generic argument.
  /// </summary>
  /// <param name="extentionTypes">Types of DAC extensions to check</param>
  /// <param name="dacType">Type of original DAC which DAC extensions belong to</param>
  /// <param name="fieldName">Name of DAC field that should be searched among DAC extensions</param>
  /// <returns>Types of DAC extensions containing DAC field with the name <paramref name="fieldName" /></returns>
  /// <remarks>This method doesn't return <see cref="T:PX.Data.PXMappedCacheExtension">PXMappedCacheExtension</see>-derived types</remarks>
  [PXInternalUseOnly]
  public static IEnumerable<System.Type> GetDacExtensionsContainingDacField(
    System.Type[] extensionTypes,
    System.Type dacType,
    string fieldName)
  {
    ExceptionExtensions.ThrowOnNull<System.Type[]>(extensionTypes, nameof (extensionTypes), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(dacType, nameof (dacType), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(fieldName, nameof (fieldName), (string) null);
    foreach (System.Type type in ((IEnumerable<System.Type>) extensionTypes).Where<System.Type>((Func<System.Type, bool>) (extType => extType.IsSubclassOf(typeof (PXCacheExtension)) && !typeof (PXMappedCacheExtension).IsAssignableFrom(extType))))
    {
      PropertyInfo property = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
      System.Type[] genericArguments = type.BaseType.GetGenericArguments();
      if (property != (PropertyInfo) null && genericArguments.Length != 0 && genericArguments[genericArguments.Length - 1].IsAssignableFrom(dacType))
        yield return type;
    }
  }

  /// <summary>
  /// Checks if the <paramref name="fieldName" /> is a regular (not UDF) attribute field.
  /// </summary>
  internal static bool IsAttributesField(this PXCache cache, string fieldName)
  {
    return fieldName != null && fieldName.EndsWith("_Attributes", StringComparison.OrdinalIgnoreCase);
  }

  internal class CacheItemComparer<T> : IEqualityComparer<T>
  {
    private readonly PXCache _cache;

    public CacheItemComparer(PXCache cache) => this._cache = cache;

    int IEqualityComparer<T>.GetHashCode(T item) => this._cache.GetObjectHashCode((object) item);

    bool IEqualityComparer<T>.Equals(T x, T y) => this._cache.ObjectsEqual((object) x, (object) y);
  }

  [ImmutableObject(true)]
  public readonly struct AttributeAdjuster<TAttribute> where TAttribute : PXEventSubscriberAttribute
  {
    private readonly bool _readonly;
    private readonly PXCache _cache;
    private readonly object _row;

    internal AttributeAdjuster(PXCache cache, object row, bool @readonly)
    {
      this._cache = cache != null ? cache : throw new ArgumentNullException(nameof (cache));
      this._readonly = @readonly;
      this._row = row;
    }

    /// <summary>
    /// Adjust attributes attached to any fields by applying the <paramref name="adjustment" /> action.
    /// </summary>
    /// <param name="adjustment">An attribute adjustment action</param>
    public PXCacheEx.AttributeAdjuster<TAttribute> ForAllFields(System.Action<TAttribute> adjustment)
    {
      this.Adjust((string) null, adjustment);
      return this;
    }

    /// <summary>
    /// Adjust attributes attached to <typeparamref name="TField" /> by applying the <paramref name="adjustment" /> action,
    /// and memorize <paramref name="adjustment" /> action for further chaining.
    /// </summary>
    /// <param name="adjustment">An attribute adjustment action</param>
    public PXCacheEx.AttributeAdjuster<TAttribute>.Chained For<TField>(System.Action<TAttribute> adjustment) where TField : IBqlField
    {
      return this.For(typeof (TField).Name, adjustment);
    }

    /// <summary>
    /// Adjust attributes attached to <paramref name="fieldName" /> by applying the <paramref name="adjustment" /> action,
    /// and memorize <paramref name="adjustment" /> action for further chaining.
    /// </summary>
    /// <param name="adjustment">An attribute adjustment action</param>
    public PXCacheEx.AttributeAdjuster<TAttribute>.Chained For(
      string fieldName,
      System.Action<TAttribute> adjustment)
    {
      this.Adjust(fieldName, adjustment);
      return new PXCacheEx.AttributeAdjuster<TAttribute>.Chained(this, adjustment);
    }

    private void Adjust(string fieldName, System.Action<TAttribute> adjustment)
    {
      if (adjustment == null)
        throw new ArgumentNullException(nameof (adjustment));
      if (this._row == null)
        this._cache.SetAltered(fieldName, true);
      foreach (TAttribute attribute in (this._readonly ? (IEnumerable) this._cache.GetAttributesReadonly(this._row, fieldName) : (IEnumerable) this._cache.GetAttributes(this._row, fieldName)).OfType<TAttribute>())
        adjustment(attribute);
    }

    [ImmutableObject(true)]
    public readonly struct Chained
    {
      private readonly PXCacheEx.AttributeAdjuster<TAttribute> _previousAdjuster;
      private readonly System.Action<TAttribute> _previousAction;

      internal Chained(
        PXCacheEx.AttributeAdjuster<TAttribute> previousAdjuster,
        System.Action<TAttribute> previousAction)
      {
        this._previousAdjuster = previousAdjuster;
        this._previousAction = previousAction;
      }

      /// <summary>
      /// Adjust attributes attached to any fields by applying the <paramref name="adjustment" /> action.
      /// </summary>
      /// <param name="adjustment">An attribute adjustment action</param>
      public PXCacheEx.AttributeAdjuster<TAttribute> ForAllFields(System.Action<TAttribute> adjustment)
      {
        return this._previousAdjuster.ForAllFields(adjustment);
      }

      /// <summary>
      /// Adjust attributes attached to <typeparamref name="TField" /> by applying the <paramref name="adjustment" /> action,
      /// and override previous memorized <paramref name="adjustment" /> action.
      /// </summary>
      /// <param name="adjustment">An attribute adjustment action</param>
      public PXCacheEx.AttributeAdjuster<TAttribute>.Chained For<TField>(
        System.Action<TAttribute> adjustment)
        where TField : IBqlField
      {
        return this._previousAdjuster.For<TField>(adjustment);
      }

      /// <summary>
      /// Adjust attributes attached to <paramref name="fieldName" /> by applying the <paramref name="adjustment" /> action,
      /// and override previous memorized <paramref name="adjustment" /> action.
      /// </summary>
      /// <param name="adjustment">An attribute adjustment action</param>
      public PXCacheEx.AttributeAdjuster<TAttribute>.Chained For(
        string fieldName,
        System.Action<TAttribute> adjustment)
      {
        return this._previousAdjuster.For(fieldName, adjustment);
      }

      /// <summary>
      /// Adjust attributes attached to <typeparamref name="TField" /> by applying the previously memorized adjustment action.
      /// </summary>
      public PXCacheEx.AttributeAdjuster<TAttribute>.Chained SameFor<TField>() where TField : IBqlField
      {
        return this.SameFor(typeof (TField).Name);
      }

      /// <summary>
      /// Adjust attributes attached to <paramref name="fieldName" /> by applying the previously memorized adjustment action.
      /// </summary>
      public PXCacheEx.AttributeAdjuster<TAttribute>.Chained SameFor(string fieldName)
      {
        return this._previousAdjuster.For(fieldName, this._previousAction);
      }
    }
  }
}
