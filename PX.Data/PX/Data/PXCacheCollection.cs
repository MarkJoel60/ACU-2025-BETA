// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

#nullable disable
namespace PX.Data;

/// <summary>A collection of <see cref="T:PX.Data.PXCache" /> objects.</summary>
public class PXCacheCollection : Dictionary<System.Type, PXCache>
{
  protected readonly PXGraph _Parent;
  private Dictionary<System.Type, System.Type> _CacheMapping;
  private Dictionary<System.Type, PXCache> _mappedCaches = new Dictionary<System.Type, PXCache>();
  internal CacheLogger _cacheLogger = new CacheLogger();
  private Dictionary<System.Type, System.Action> _OnCacheCreated;

  /// <summary>Adds the cache mapping.</summary>
  /// <param name="itemType">The DAC type.</param>
  /// <param name="cacheType">The cache item type.</param>
  /// <example>
  /// The following example explicitly specifies cache mappings for the VendorShipmentEntry graph
  /// in the override <see cref="M:PX.Data.PXGraph.InitCacheMapping(System.Collections.Generic.Dictionary{System.Type,System.Type})" /> method of the graph.
  /// By specifying cache mapping from INLotSerialStatus to INLotSerialStatus, you ensure
  /// that cache mapping for the DAC is stable and is not affected by new graph views.
  /// <code>public override void InitCacheMapping(Dictionary&lt;Type, Type&gt; map)
  /// {
  ///     base.InitCacheMapping(map);
  /// 
  ///     this.Caches.AddCacheMapping(typeof(INLotSerialStatus), typeof(INLotSerialStatus));
  /// }</code></example>
  /// <inheritdoc cref="M:PX.Data.PXGraph.InitCacheMapping(System.Collections.Generic.Dictionary{System.Type,System.Type})" path="/remarks" />
  public void AddCacheMapping(System.Type itemType, System.Type cacheType)
  {
    if (this._CacheMapping.ContainsKey(itemType))
      return;
    this._CacheMapping.Add(itemType, cacheType);
  }

  public void InitCacheMapping(PXGraph graph)
  {
    if (this._CacheMapping != null)
      return;
    this._CacheMapping = new Dictionary<System.Type, System.Type>();
    graph.InitCacheMapping(this._CacheMapping);
  }

  public bool HasCacheMapping(System.Type cacheType, System.Type itemType)
  {
    return this._CacheMapping == null || !this._CacheMapping.ContainsKey(cacheType) || !(this._CacheMapping[cacheType] != itemType);
  }

  public bool CanInitLazyCache()
  {
    return WebConfig.LazyCachesEnabled && HttpContext.Current != null && !PXLongOperation.IsLongRunOperation;
  }

  public void ProcessCacheMapping(PXGraph graph, System.Type cacheType)
  {
    if (!this.CanInitLazyCache())
      return;
    this.InitCacheMapping(graph);
    this.AddCacheMappingsWithInheritance(graph, cacheType);
  }

  /// <summary>
  /// Maps a DAC type and all its base DAC types to a single cache item type.
  /// This method is useful when you have a derived DAC and you want to map
  /// both the derived DAC and all its base types to a single cache item type.
  /// </summary>
  /// <param name="graph">A graph.</param>
  /// <param name="cacheType">The cache item type.</param>
  /// <remarks>
  /// <para>Instead of using AddCacheMappingsWithInheritance, you could add
  /// multiple cache mappings manually for each base type. However, we recommend that you use
  /// AddCacheMappingsWithInheritance because this method is not affected by the changes
  /// in the DAC hierarchy and it takes into account multiple platform mechanisms
  /// related to graph caches and class inheritance (such as the <see cref="T:PX.Data.PXBreakInheritanceAttribute" /> attribute).</para>
  ///   <para>For details about cache mapping,
  ///   see <a href="https://help.acumatica.com/Help?ScreenId=ShowWiki&amp;pageid=a5fb7e46-c6e8-438a-b640-1fa9f56b9525" target="_blank">Cache Mapping</a>.</para>
  /// </remarks>
  /// <example>
  /// In the following example, Caches.AddCacheMappingsWithInheritance(this,typeof(VendorR)) adds three cache mappings. It maps VendorR, Vendor, and BAccount DACs to PXCache&lt;VendorR&gt;.
  /// <code>public override void InitCacheMapping(Dictionary&lt;Type, Type&gt; map)
  /// {
  ///     base.InitCacheMapping(map);
  /// 
  ///     Caches.AddCacheMappingsWithInheritance(this,typeof(VendorR));
  ///     Caches.AddCacheMappingsWithInheritance(this,typeof(CRLocation));
  /// }</code></example>
  public void AddCacheMappingsWithInheritance(PXGraph graph, System.Type cacheType)
  {
    System.Type type1 = PXSubstManager.Substitute(cacheType, this._Parent.GetType());
    for (System.Type type2 = type1; PXCacheCollection.IsCacheType(type2) && !this._CacheMapping.ContainsKey(type2); type2 = type2.BaseType)
    {
      this._CacheMapping[type2] = type1;
      if ((type2 == type1 || type1.IsSubclassOf(type2)) && Attribute.IsDefined((MemberInfo) type2, typeof (PXBreakInheritanceAttribute), false) || this._Parent.GetType() == typeof (PXGraph) || this._Parent.GetType() == typeof (PXGenericInqGrph) || this._Parent.GetType() == typeof (GenericInquiryDesigner) || !this.IsSameCache(type2.BaseType) || graph._ReadonlyCacheCreation)
        break;
    }
  }

  public PXCacheCollection(PXGraph parent) => this._Parent = parent;

  public PXCacheCollection(PXGraph parent, int capacity)
    : base(capacity)
  {
    this._Parent = parent;
  }

  public new PXCache this[System.Type key]
  {
    get
    {
      key = !(key == (System.Type) null) ? this.ExtractCacheExtensionType(key) : throw new PXArgumentException(nameof (key), "The argument cannot be null.");
      if (!this.ContainsKey(key))
      {
        System.Type type = this.GetCacheTypeWithSubstitution(key);
        this._cacheLogger.CacheRequested(type);
        PXCache cache;
        if (!this.TryGetValue(type, out cache))
        {
          try
          {
            if (typeof (PXMappedCacheExtension).IsAssignableFrom(type))
            {
              if (!this._mappedCaches.TryGetValue(type, out cache))
              {
                cache = PXCache._mapping.CreateModelExtension(type, this._Parent);
                this._mappedCaches.Add(type, cache);
              }
              return cache;
            }
            cache = this.CreateCacheInstance(type);
          }
          catch (TypeInitializationException ex)
          {
            throw PXException.ExtractInner((Exception) ex);
          }
          catch (TargetInvocationException ex)
          {
            throw PXException.ExtractInner((Exception) ex);
          }
          cache.Load();
        }
        System.Type itemType = type;
        if (Attribute.IsDefined((MemberInfo) key, typeof (PXAutoSaveAttribute), false))
          cache.AutoSave = true;
        cache.AutomationHidden = this._Parent.AutomationHidden;
        cache.AutomationInsertDisabled = this._Parent.AutomationInsertDisabled;
        cache.AutomationUpdateDisabled = this._Parent.AutomationUpdateDisabled;
        cache.AutomationDeleteDisabled = this._Parent.AutomationDeleteDisabled;
        for (; PXCacheCollection.IsCacheType(type); type = type.BaseType)
        {
          if (!this.ContainsKey(type) && this.HasCacheMapping(type, itemType))
          {
            base[type] = cache;
            this.RaiseCacheCreated(this._Parent, cache);
          }
          if ((!(type == key) && !key.IsSubclassOf(type) || !Attribute.IsDefined((MemberInfo) type, typeof (PXBreakInheritanceAttribute), false)) && !(this._Parent.GetType() == typeof (PXGraph)) && !(this._Parent.GetType() == typeof (PXGenericInqGrph)) && !(this._Parent.GetType() == typeof (GenericInquiryDesigner)) && this.IsSameCache(type.BaseType))
          {
            if (type == key && this._Parent._ReadonlyCacheCreation)
            {
              cache._ReadonlyCreatedCache = true;
              break;
            }
          }
          else
            break;
        }
        System.Action action;
        if (this._OnCacheCreated != null && this._OnCacheCreated.TryGetValue(cache.GetItemType(), out action))
          action();
        System.Action<PXCache> everyCacheCreated = PXCacheCollection.OnEveryCacheCreated;
        if (everyCacheCreated != null)
          everyCacheCreated(cache);
      }
      if (this.ContainsKey(key))
      {
        this._cacheLogger.CacheRequested(key);
        if (this._Parent._ReadonlyCacheCreation)
          return base[key];
        PXCache cache = base[key];
        if (cache._ReadonlyCreatedCache)
        {
          System.Type baseType = cache.GetItemType().BaseType;
          cache._ReadonlyCreatedCache = false;
          for (; PXCacheCollection.IsCacheType(baseType) && !this.ContainsKey(baseType) && this.HasCacheMapping(baseType, cache.GetItemType()); baseType = baseType.BaseType)
          {
            base[baseType] = cache;
            this.AttachHandlers(baseType, cache);
            this.RaiseCacheCreated(this._Parent, cache);
          }
        }
        return cache;
      }
      throw new PXException("A cache instance cannot be created for the type {0}.", new object[1]
      {
        (object) key.Name
      });
    }
    set
    {
      string fullName = key.FullName;
      if (!this.ContainsKey(key) && this._Parent == value.Graph)
      {
        value.AutomationHidden = this._Parent.AutomationHidden;
        value.AutomationInsertDisabled = this._Parent.AutomationInsertDisabled;
        value.AutomationUpdateDisabled = this._Parent.AutomationUpdateDisabled;
        value.AutomationDeleteDisabled = this._Parent.AutomationDeleteDisabled;
        base[key] = value;
        this.AttachHandlers(key, value);
      }
      else
        base[key] = value;
      PXCacheCollection.RaiseCacheChanged(this._Parent, value);
    }
  }

  private System.Type GetCacheTypeWithSubstitution(System.Type key)
  {
    string str = !CustomizedTypeManager.IsCustomizedType(key) ? key.FullName : throw new PXException("Invalid argument for graph.Caches[] : {0}", new object[1]
    {
      (object) key
    });
    System.Type key1 = PXSubstManager.Substitute(key, this._Parent.GetType());
    if (this._CacheMapping.ContainsKey(key1) && this._CacheMapping[key1] != key1)
      key1 = this._CacheMapping[key1];
    return key1;
  }

  private System.Type ExtractCacheExtensionType(System.Type key)
  {
    this.InitCacheMapping(this._Parent);
    if (typeof (PXCacheExtension).IsAssignableFrom(key) && !typeof (PXMappedCacheExtension).IsAssignableFrom(key) && key.BaseType.IsGenericType)
      key = key.BaseType.GetGenericArguments()[key.BaseType.GetGenericArguments().Length - 1];
    return key;
  }

  internal System.Type GetRealCacheType(System.Type key)
  {
    return this.ContainsKey(key) ? base[key].GetItemType() : this.GetCacheTypeWithSubstitution(this.ExtractCacheExtensionType(key));
  }

  public void AttachHandlers(System.Type type, PXCache cache)
  {
    this._Parent.RowDeleted.CacheAttached(type, cache);
    this._Parent.RowDeleting.CacheAttached(type, cache);
    this._Parent.RowInserted.CacheAttached(type, cache);
    this._Parent.RowInserting.CacheAttached(type, cache);
    this._Parent.RowPersisted.CacheAttached(type, cache);
    this._Parent.RowPersisting.CacheAttached(type, cache);
    this._Parent.RowSelected.CacheAttached(type, cache);
    this._Parent.RowSelecting.CacheAttached(type, cache);
    this._Parent.RowUpdated.CacheAttached(type, cache);
    this._Parent.RowUpdating.CacheAttached(type, cache);
    this._Parent.CommandPreparing.CacheAttached(type, cache);
    this._Parent.ExceptionHandling.CacheAttached(type, cache);
    this._Parent.FieldDefaulting.CacheAttached(type, cache);
    this._Parent.FieldUpdating.CacheAttached(type, cache);
    this._Parent.FieldVerifying.CacheAttached(type, cache);
    this._Parent.FieldUpdated.CacheAttached(type, cache);
    this._Parent.FieldSelecting.CacheAttached(type, cache);
  }

  public PXCache this[string key]
  {
    get
    {
      foreach (KeyValuePair<System.Type, PXCache> keyValuePair in (Dictionary<System.Type, PXCache>) this)
      {
        if (keyValuePair.Key.Name == key)
        {
          this._cacheLogger.CacheRequested(keyValuePair.Key);
          return keyValuePair.Value;
        }
      }
      if (this._CacheMapping != null)
      {
        foreach (System.Type type in this._CacheMapping.Keys.ToArray<System.Type>())
        {
          if (type.Name == key)
          {
            PXCache pxCache = this[type];
            this._cacheLogger.CacheRequested(type);
            return pxCache;
          }
        }
      }
      return (PXCache) null;
    }
  }

  internal PXCache GetCache(System.Type key)
  {
    PXCache cache = (PXCache) null;
    this.TryGetValue(key, out cache);
    return cache;
  }

  internal System.Type GetCacheType(System.Type key)
  {
    PXCache pxCache;
    if (this.TryGetValue(key, out pxCache))
      return pxCache.GetItemType();
    System.Type type;
    return this._CacheMapping.TryGetValue(key, out type) ? type : (System.Type) null;
  }

  protected virtual PXCache CreateCacheInstance(System.Type itemType)
  {
    return (PXCache) Activator.CreateInstance(typeof (PXCache<>).MakeGenericType(itemType), (object) this._Parent);
  }

  internal virtual bool IsSameCache(System.Type type) => true;

  protected internal void RaiseCacheCreated(PXGraph graph, PXCache cache)
  {
    if (PXCacheCollection.OnCacheCreated == null)
      return;
    PXCacheCollection.OnCacheCreated(graph, cache);
  }

  protected static void RaiseCacheChanged(PXGraph graph, PXCache cache)
  {
    if (PXCacheCollection.OnCacheChanged == null)
      return;
    PXCacheCollection.OnCacheChanged(graph, cache);
  }

  public static event Action<PXGraph, PXCache> OnCacheCreated;

  public static event Action<PXGraph, PXCache> OnCacheChanged;

  internal static event System.Action<PXCache> OnEveryCacheCreated;

  public void SubscribeCacheCreated(System.Type t, System.Action a)
  {
    if (this.ContainsKey(t))
    {
      a();
    }
    else
    {
      if (this._OnCacheCreated == null)
        this._OnCacheCreated = new Dictionary<System.Type, System.Action>();
      System.Action action;
      if (this._OnCacheCreated.TryGetValue(t, out action))
      {
        action += a;
        this._OnCacheCreated[t] = action;
      }
      else
        this._OnCacheCreated.Add(t, a);
    }
  }

  public void SubscribeCacheCreated<T>(System.Action a)
  {
    this.SubscribeCacheCreated(typeof (T), a);
  }

  public PXCache[] Caches => this.Values.Distinct<PXCache>().ToArray<PXCache>();

  public IBqlTable[] Currents
  {
    get
    {
      return ((IEnumerable<PXCache>) this.Caches).Select<PXCache, object>((Func<PXCache, object>) (_ => _.Current)).OfType<IBqlTable>().ToArray<IBqlTable>();
    }
  }

  public void AttachAllHandlers<TNode>(PXCache<TNode> cache) where TNode : class, IBqlTable, new()
  {
    System.Type type = typeof (TNode);
    System.Type itemType = type;
    for (; PXCacheCollection.IsCacheType(type); type = type.BaseType)
    {
      if (this.HasCacheMapping(type, itemType))
        this.AttachHandlers(type, (PXCache) cache);
      if ((type == itemType || itemType.IsSubclassOf(type)) && Attribute.IsDefined((MemberInfo) type, typeof (PXBreakInheritanceAttribute), false) || this._Parent.GetType() == typeof (PXGraph) || this._Parent.GetType() == typeof (PXGenericInqGrph) || this._Parent.GetType() == typeof (GenericInquiryDesigner) || !this.IsSameCache(type.BaseType))
        break;
      if (type == itemType && this._Parent._ReadonlyCacheCreation)
      {
        cache._ReadonlyCreatedCache = true;
        break;
      }
    }
  }

  public static bool IsCacheType(System.Type type) => !type.IsAssignableFrom(typeof (PXBqlTable));
}
