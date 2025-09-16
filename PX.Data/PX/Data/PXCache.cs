// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Api.Export;
using PX.Data.PushNotifications;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.SQLTree;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

/// <summary>
/// Untyped interface to access <see cref="T:PX.Data.PXCache`1">PXCache&lt;TNode&gt;</see>
/// without knowledge of the <tt>TNode</tt> type.</summary>
/// <seealso cref="T:PX.Data.PXCache`1" />
public abstract class PXCache
{
  private static 
  #nullable disable
  Dictionary<string, List<KeyValuePair<string, List<string>>>> _ExtensionTables;
  /// <summary>
  /// The flag controlled by the
  /// <see cref="T:PX.Data.PXDisableCloneAttributesAttribute">PXDisableCloneAttributes</see>
  /// attribute.
  /// </summary>
  public bool DisableCloneAttributes;
  internal bool NeverCloneAttributes;
  internal bool DisableCacheClear;
  /// <exclude />
  public bool DisableReadItem;
  protected List<PXEventSubscriberAttribute> _CacheAttributes;
  protected Dictionary<object, PXCache.DirtyItemState> _ItemAttributes;
  protected PXEventSubscriberAttribute[] _CacheAttributesWithEmbedded;
  protected PXEventSubscriberAttribute[] _ReusableItemAttributes;
  protected int[] _UsableItemAttributes;
  protected ILookup<System.Type, PXEventSubscriberAttribute> _AttributesByType;
  protected ILookup<string, PXEventSubscriberAttribute> _AttributesByName;
  private Func<object, object, bool>[] _AttributeComparers;
  protected internal List<IPXInterfaceField> AttributesWithError = new List<IPXInterfaceField>();
  private static readonly ConcurrentDictionary<System.Type, PXCache.CompiledAttributeComparer> _Comparers = new ConcurrentDictionary<System.Type, PXCache.CompiledAttributeComparer>();
  private PXEventSubscriberAttribute[] _reusableContainer;
  internal Dictionary<string, int> _FieldsMap;
  protected HashSet<string> _BypassAuditFields;
  protected internal HashSet<string> _EncryptAuditFields;
  internal bool _SelectingForAuditExplore;
  protected HashSet<string> _AlteredFields;
  protected HashSet<string> _SecuredFields;
  protected HashSet<string> _SelectingFields;
  protected HashSet<int> _KeyValueStoredOrdinals;
  protected internal Dictionary<string, int> _KeyValueStoredNames;
  protected internal Dictionary<string, int> _DBLocalizableNames;
  protected KeyValuePair<string, int>? _FirstKeyValueStored;
  protected Dictionary<string, string> _InactiveFields;
  protected internal Dictionary<string, int> _KeyValueAttributeNames;
  protected internal IPXInterfaceField[] _KeyValueAttributeUIFields;
  internal Dictionary<string, StorageBehavior> _KeyValueAttributeTypes;
  protected internal int _KeyValueAttributeSlotPosition;
  protected KeyValuePair<string, int>? _FirstKeyValueAttribute;
  internal System.Type _KeyValueAttributeConfigurationSource;
  protected internal readonly HashSet<string> _ForcedKeyValueAttributes = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  protected internal bool _MeasuringUpdatability;
  protected internal const string _OriginalValue = "_OriginalValue";
  private static Dictionary<System.Type, Dictionary<System.Type, string[]>> _keys = new Dictionary<System.Type, Dictionary<System.Type, string[]>>();
  protected KeysCollection _Keys;
  protected internal string _Identity;
  protected internal string _Timestamp;
  internal bool _ForbidChangesAfterPersist;
  protected internal bool _PreventDeadlock;
  internal Comparison<object> _CustomDeadlockComparison;
  protected internal bool _AggregateSelecting;
  protected internal bool _SingleTableSelecting;
  protected internal string _RowId;
  protected internal int? _NoteIDOrdinal;
  protected internal string _NoteIDName;
  protected List<Tuple<Delegate, Delegate, Delegate>> _SlotDelegates;
  protected List<string> _Immutables;
  protected internal HashSet<string> _AutoNumberFields;
  protected PXFieldCollection _Fields;
  protected List<System.Type> _BqlFields;
  protected List<System.Type> _BqlKeys;
  protected List<System.Type> _BqlImmutables;
  internal bool BypassCalced;
  internal bool SingleExtended;
  protected PXGraph _Graph;
  protected internal object _CacheSecurity;
  protected bool _SelectRights = true;
  protected internal bool _AllowSelect = true;
  protected internal bool _AllowSelectChanged;
  internal bool AutomationHidden;
  internal bool AutomationInsertDisabled;
  internal bool AutomationUpdateDisabled;
  internal bool AutomationDeleteDisabled;
  protected bool _UpdateRights = true;
  protected internal bool _AllowUpdate = true;
  protected bool _InsertRights = true;
  protected internal bool _AllowInsert = true;
  protected bool _DeleteRights = true;
  protected internal bool _AllowDelete = true;
  internal bool _ReadonlyCreatedCache;
  protected bool _AutoSave;
  /// <exclude />
  public bool ForceExceptionHandling;
  private static readonly System.Type[] _AvailableExtensions;
  internal static readonly PXCache.Mapping _mapping;
  private static System.Type[] _DynamicExtensions;
  private static bool _initDynamicExtensionsWatcher;
  internal static System.Action ClearCacheExtensionsDelegate;
  public static readonly object NotSetValue = new object();
  public static readonly string IsNewRow = Guid.NewGuid().ToString();
  internal object _Current;
  internal object _CurrentVersionModified;
  /// <summary>
  /// Allow to pass active row from business logic back to UI
  /// Current can not be used for this because its value can be unpredictable changed
  /// </summary>
  public IBqlTable ActiveRow;
  /// <summary>
  /// Allows to pass data key of the row before which new row should be inserted.
  /// </summary>
  public Dictionary<string, object> InsertPosition;
  /// <summary>
  /// If true you should insert 'RowsToMove' after specified 'InsertPosition'.
  /// </summary>
  public bool InsertPositionMode;
  /// <summary>
  /// Allows to pass data keys of the rows that need to be moved.
  /// </summary>
  public List<Dictionary<string, object>> RowsToMove;
  internal readonly PXCacheOriginalCollection _Originals = PXCacheOriginalCollection.Instance;
  internal PXCacheRemovedOriginalsCollection _OriginalsRemoved;
  protected bool _IsDirty;
  /// <summary>
  /// Mobile only. Clarifies If cache in before insert state.
  /// </summary>
  protected bool _IsInInsertState;
  internal bool _Persisting;
  protected internal PXCache.EventsRow _EventsRow = new PXCache.EventsRow();
  protected internal PXCache.EventsRowAttr _EventsRowAttr = new PXCache.EventsRowAttr();
  protected PXCache.EventDictionary<PXCommandPreparing> _CommandPreparingEvents;
  protected Dictionary<string, IPXCommandPreparingSubscriber[]> _CommandPreparingEventsAttr = new Dictionary<string, IPXCommandPreparingSubscriber[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  protected PXCache.EventDictionary<PXFieldDefaulting> _FieldDefaultingEvents;
  protected Dictionary<string, IPXFieldDefaultingSubscriber[]> _FieldDefaultingEventsAttr = new Dictionary<string, IPXFieldDefaultingSubscriber[]>();
  internal PXCache.FieldDefaultingDelegate WorkflowStateFieldDefaulting;
  internal PXCache.FieldOfRowDefaultingDelegate WorkflowFieldDefaulting;
  internal PXCache.FieldDefaultingDelegate AutomationFieldDefaulting;
  protected PXCache.EventDictionary<PXFieldUpdating> _FieldUpdatingEvents;
  protected Dictionary<string, IPXFieldUpdatingSubscriber[]> _FieldUpdatingEventsAttr = new Dictionary<string, IPXFieldUpdatingSubscriber[]>();
  protected PXCache.EventDictionary<PXFieldVerifying> _FieldVerifyingEvents;
  protected Dictionary<string, IPXFieldVerifyingSubscriber[]> _FieldVerifyingEventsAttr = new Dictionary<string, IPXFieldVerifyingSubscriber[]>();
  protected PXCache.EventDictionary<PXFieldUpdated> _FieldUpdatedEvents;
  protected Dictionary<string, IPXFieldUpdatedSubscriber[]> _FieldUpdatedEventsAttr = new Dictionary<string, IPXFieldUpdatedSubscriber[]>();
  protected PXCache.EventDictionary<PXFieldSelecting> _FieldSelectingEvents;
  protected Dictionary<string, IPXFieldSelectingSubscriber[]> _FieldSelectingEventsAttr = new Dictionary<string, IPXFieldSelectingSubscriber[]>();
  internal PXCache.FieldSelectingDelegate WorkflowFieldSelecting;
  internal PXCache.FieldSelectingDelegate AutomationFieldSelecting;
  protected PXCache.EventDictionary<PXExceptionHandling> _ExceptionHandlingEvents;
  protected Dictionary<string, IPXExceptionHandlingSubscriber[]> _ExceptionHandlingEventsAttr = new Dictionary<string, IPXExceptionHandlingSubscriber[]>();

  public PXCache()
  {
    if (!(((IEnumerable<Attribute>) Attribute.GetCustomAttributes((MemberInfo) this.GetItemType(), typeof (PXCacheNameAttribute), true)).FirstOrDefault<Attribute>() is PXCacheNameAttribute cacheNameAttribute))
      return;
    this.DisplayName = cacheNameAttribute.Name;
  }

  static PXCache()
  {
    PXCache._ExtensionTables = new Dictionary<string, List<KeyValuePair<string, List<string>>>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXCache._AvailableExtensions = ((IEnumerable<System.Type>) PXCache.FindExtensionTypes(out PXCache._mapping, extensionTables: PXCache._ExtensionTables)).Where<System.Type>((Func<System.Type, bool>) (_ => !_.Assembly.FullName.StartsWith("RuntimeCode_"))).ToArray<System.Type>();
  }

  internal static System.Type[] FindExtensionTypes(
    List<Exception> errorLog = null,
    Dictionary<string, List<KeyValuePair<string, List<string>>>> extensionTables = null)
  {
    return PXCache.FindExtensionTypes(out PXCache.Mapping _, errorLog, extensionTables);
  }

  internal static System.Type[] FindExtensionTypes(
    out PXCache.Mapping mapping,
    List<Exception> errorLog = null,
    Dictionary<string, List<KeyValuePair<string, List<string>>>> extensionTables = null)
  {
    List<System.Type> typeList = new List<System.Type>();
    mapping = new PXCache.Mapping();
    foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (FindExtensionTypes), errorLog))
    {
      if (typeof (PXCacheExtension).IsAssignableFrom(enumTypesInAssembly))
      {
        PXSubstManager.AddTypeToNamedList(nameof (FindExtensionTypes), enumTypesInAssembly);
        typeList.Add(enumTypesInAssembly);
        PXCache.saveTables(enumTypesInAssembly, extensionTables);
      }
      else if (typeof (PXGraphExtension).IsAssignableFrom(enumTypesInAssembly))
      {
        PXSubstManager.AddTypeToNamedList(nameof (FindExtensionTypes), enumTypesInAssembly);
        if (PXExtensionManager.GetFirstExtensionParent(enumTypesInAssembly) != enumTypesInAssembly.BaseType)
        {
          foreach (MethodInfo method in enumTypesInAssembly.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
          {
            if (typeof (IBqlMapping).IsAssignableFrom(method.ReturnType))
            {
              if (!enumTypesInAssembly.ContainsGenericParameters)
              {
                try
                {
                  IBqlMapping mapping1 = (IBqlMapping) method.Invoke(Activator.CreateInstance(enumTypesInAssembly), new object[0]);
                  if (mapping1 != null)
                    mapping.AddMap(enumTypesInAssembly, mapping1);
                  else
                    mapping.AddDisabledGraphExtension(enumTypesInAssembly);
                }
                catch
                {
                }
              }
            }
          }
        }
      }
    }
    PXSubstManager.SaveTypeCache(nameof (FindExtensionTypes));
    System.Type[] typeArray = PXCache._DynamicExtensions ?? PXCodeDirectoryCompiler.GetCompiledTypes<PXCacheExtension>().ToArray();
    if (typeArray != null)
    {
      foreach (System.Type t in typeArray)
      {
        typeList.Add(t);
        PXCache.saveTables(t, extensionTables);
      }
    }
    typeList.Sort((Comparison<System.Type>) ((a, b) => string.Compare(a.FullName, b.FullName)));
    return typeList.ToArray();
  }

  private static void saveTables(
    System.Type t,
    Dictionary<string, List<KeyValuePair<string, List<string>>>> extensionTables)
  {
    if (extensionTables == null || !t.IsDefined(typeof (PXDBInterceptorAttribute), true) || !t.BaseType.IsGenericType)
      return;
    PXDBInterceptorAttribute customAttribute = (PXDBInterceptorAttribute) t.GetCustomAttributes(typeof (PXDBInterceptorAttribute), true)[0];
    for (System.Type c = t.BaseType.GetGenericArguments()[t.BaseType.GetGenericArguments().Length - 1]; c != typeof (object); c = c.BaseType)
    {
      if ((c.BaseType == typeof (object) || !typeof (IBqlTable).IsAssignableFrom(c.BaseType)) && typeof (IBqlTable).IsAssignableFrom(c) || c.IsDefined(typeof (PXTableAttribute), false))
      {
        List<KeyValuePair<string, List<string>>> keyValuePairList;
        if (!extensionTables.TryGetValue(c.Name, out keyValuePairList))
          extensionTables[c.Name] = keyValuePairList = new List<KeyValuePair<string, List<string>>>();
        keyValuePairList.Add(new KeyValuePair<string, List<string>>(t.Name, customAttribute.Keys));
      }
    }
  }

  internal static List<KeyValuePair<string, List<string>>> GetExtensionTables(string table)
  {
    Dictionary<string, List<KeyValuePair<string, List<string>>>> extensionTables1 = PXCache._ExtensionTables;
    if (extensionTables1 == null)
    {
      extensionTables1 = new Dictionary<string, List<KeyValuePair<string, List<string>>>>();
      PXCache.FindExtensionTypes(extensionTables: extensionTables1);
      PXCache._ExtensionTables = extensionTables1;
    }
    List<KeyValuePair<string, List<string>>> extensionTables2;
    extensionTables1.TryGetValue(table, out extensionTables2);
    return extensionTables2;
  }

  /// <summary>
  /// Returns the cach-level instances of attributes placed on the
  /// specified field and all item-level instances currently stored
  /// in the cache.
  /// </summary>
  /// <param name="name">The name of the field whose attributes are returned.
  /// If <tt>null</tt>, the method returns attributes from all fields.</param>
  public abstract List<PXEventSubscriberAttribute> GetAttributes(string name);

  /// <summary>Returns the cach-level instances of attributes placed on the specified field and all item-level instances currently stored in the cache as read-only instances.</summary>
  public abstract List<PXEventSubscriberAttribute> GetAttributesReadonly(string name);

  /// <exclude />
  public abstract List<PXEventSubscriberAttribute> GetAttributesReadonly(
    string name,
    bool extractEmmbeddedAttr);

  /// <summary>Returns a collection of row-specific attributes as read-only instances.</summary>
  public abstract IEnumerable<PXEventSubscriberAttribute> GetAttributesReadonly(
    object data,
    string name);

  /// <summary>Returns a collection of row specific attributes. This method creates a copy of all cache level attributes and stores these clones to internal collection that
  /// contains row specific attributes. To avoid cloning cache level attributes, use the GetAttributesReadonly method.</summary>
  public abstract IEnumerable<PXEventSubscriberAttribute> GetAttributes(object data, string name);

  /// <exclude />
  public virtual IEnumerable<T> GetAttributesOfType<T>(object data, string name) where T : PXEventSubscriberAttribute
  {
    return this.GetAttributes(data, name).OfType<T>();
  }

  /// <exclude />
  public abstract bool HasAttributes(object data);

  /// <exclude />
  public virtual bool HasAttribute<T>() where T : PXEventSubscriberAttribute
  {
    foreach (PXEventSubscriberAttribute cacheAttribute in this._CacheAttributes)
    {
      if (cacheAttribute is T)
        return true;
      if (cacheAttribute is PXAggregateAttribute aggregateAttribute)
      {
        foreach (PXEventSubscriberAttribute aggregatedAttribute in aggregateAttribute.GetAggregatedAttributes())
        {
          if (aggregatedAttribute is T)
            return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Returns the cach-level instances of attributes placed on the
  /// specified field and all item-level instances currently stored
  /// in the cache.
  /// </summary>
  /// <typeparam name="Field">The DAC field.</typeparam>
  public List<PXEventSubscriberAttribute> GetAttributes<Field>() where Field : IBqlField
  {
    return this.GetAttributes(typeof (Field).Name);
  }

  /// <summary>Returns the cache-level read-only instances of attributes placed on the specified field in the DAC. The field is specified as the type parameter.</summary>
  /// <typeparam name="Field">The DAC field.</typeparam>
  public List<PXEventSubscriberAttribute> GetAttributesReadonly<Field>() where Field : IBqlField
  {
    return this.GetAttributesReadonly(typeof (Field).Name);
  }

  /// <summary>Returns the read-only item-level instances of attributes placed on the specified field if such instances exist for the provided data record, or the cache-level
  /// instances otherwise. The field is specified as the type parameter.</summary>
  /// <typeparam name="Field">The DAC field.</typeparam>
  /// <param name="data">The data record.</param>
  public IEnumerable<PXEventSubscriberAttribute> GetAttributesReadonly<Field>(object data) where Field : IBqlField
  {
    return this.GetAttributesReadonly(data, typeof (Field).Name);
  }

  /// <summary>
  /// Returns the item-level instances of attributes placed on the
  /// specified field. If such instances are not exist for the
  /// provided data record, the method creates them by copying all
  /// cache-level attributes and storing them in the internal collection
  /// that contains the data record specific attributes.</summary>
  /// <remarks>
  /// To avoid cloning cache-level attributes, use the
  /// <see cref="M:PX.Data.PXCache`1.GetAttributesReadonly(System.Object,System.String)">GetAttributesReadonly(object, string)</see>
  /// method. The field is specified as the type parameter.
  /// </remarks>
  /// <typeparam name="Field">The DAC field.</typeparam>
  /// <param name="data">The data record.</param>
  /// <example>
  /// <code>
  /// foreach (PXEventSubscriberAttribute attr in sender.GetAttributes&lt;Field&gt;(data))
  /// {
  ///     if (attr is PXUIFieldAttribute)
  ///     {
  ///         // Doing something
  ///     }
  /// }
  /// </code>
  /// </example>
  public IEnumerable<PXEventSubscriberAttribute> GetAttributes<Field>(object data) where Field : IBqlField
  {
    return this.GetAttributes(data, typeof (Field).Name);
  }

  /// <exclude />
  public abstract List<System.Type> GetExtensionTables();

  internal abstract PXCacheExtension[] GetCacheExtensions(IBqlTable item);

  /// <summary>
  /// initializes internal collections: _ItemAttributes, _CacheAttributesWithEmbedded, _ReusableItemAttributes, _AttributesByType
  ///  </summary>
  /// <exclude />
  protected void InitItemAttributesCollection()
  {
    if (this._ItemAttributes != null)
      return;
    this._ItemAttributes = new Dictionary<object, PXCache.DirtyItemState>();
    if (this._CacheAttributesWithEmbedded != null)
      return;
    this._CacheAttributesWithEmbedded = this.GetAttributesReadonly((string) null, true).ToArray();
    for (int index = 0; index < this._CacheAttributesWithEmbedded.Length; ++index)
      this._CacheAttributesWithEmbedded[index].IndexInClonesArray = index;
    this._ReusableItemAttributes = new PXEventSubscriberAttribute[this._CacheAttributesWithEmbedded.Length];
    this._UsableItemAttributes = new int[this._CacheAttributesWithEmbedded.Length != 0 ? (this._CacheAttributesWithEmbedded.Length - 1) / 32 /*0x20*/ + 1 : 0];
    this._AttributesByType = ((IEnumerable<PXEventSubscriberAttribute>) this._CacheAttributesWithEmbedded).SelectMany((Func<PXEventSubscriberAttribute, IEnumerable<System.Type>>) (a => a.GetType().CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType))), (a, t) => new
    {
      a = a,
      t = t
    }).Where(_param1 => typeof (PXEventSubscriberAttribute).IsAssignableFrom(_param1.t) && _param1.t != typeof (PXEventSubscriberAttribute)).Select(_param1 => new
    {
      t = _param1.t,
      a = _param1.a
    }).ToLookup(_ => _.t, _ => _.a);
    this._AttributesByName = ((IEnumerable<PXEventSubscriberAttribute>) this._CacheAttributesWithEmbedded).ToLookup<PXEventSubscriberAttribute, string, PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, string>) (_ => _.FieldName), (Func<PXEventSubscriberAttribute, PXEventSubscriberAttribute>) (_ => _), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this._AttributeComparers = ((IEnumerable<PXEventSubscriberAttribute>) this._CacheAttributesWithEmbedded).Select<PXEventSubscriberAttribute, Func<object, object, bool>>(PXCache.\u003C\u003EO.\u003C0\u003E__GetAttributeComparer ?? (PXCache.\u003C\u003EO.\u003C0\u003E__GetAttributeComparer = new Func<PXEventSubscriberAttribute, Func<object, object, bool>>(PXCache.GetAttributeComparer))).ToArray<Func<object, object, bool>>();
  }

  /// <summary>Compares attributes by field values</summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  private bool AttributesEqual(PXEventSubscriberAttribute a, PXEventSubscriberAttribute b)
  {
    return a == b || this._AttributeComparers[a.IndexInClonesArray]((object) a, (object) b);
  }

  private static Func<object, object, bool> GetAttributeComparer(PXEventSubscriberAttribute a)
  {
    return PXCache._Comparers.GetOrAdd(a.GetType(), (Func<System.Type, PXCache.CompiledAttributeComparer>) (type => new PXCache.CompiledAttributeComparer(type))).CompiledMethod;
  }

  public static void TryDispose(object obj)
  {
    if (!(obj is IDisposable disposable))
      return;
    disposable.Dispose();
  }

  /// <summary>Clones attribute to per item collection</summary>
  /// <param name="state"></param>
  /// <param name="idx"></param>
  /// <returns></returns>
  private PXEventSubscriberAttribute CheckoutItemAttribute(PXCache.DirtyItemState state, int idx)
  {
    PXEventSubscriberAttribute a = state.DirtyAttributes[idx];
    if (a == null)
    {
      a = this._ReusableItemAttributes[idx];
      if (a != null)
      {
        this._ReusableItemAttributes[idx] = (PXEventSubscriberAttribute) null;
        if (!this.AttributesEqual(a, a.Prototype))
          throw new PXException("NewItemAttribute is dirty.");
        if (!this.AttributesEqual(a, this._CacheAttributesWithEmbedded[idx]))
          a = (PXEventSubscriberAttribute) null;
      }
      if (a == null)
      {
        a = this._CacheAttributesWithEmbedded[idx].Clone(PXAttributeLevel.Item);
        a.Prototype = this._CacheAttributesWithEmbedded[idx].Clone(PXAttributeLevel.Item);
      }
      state.DirtyAttributes[idx] = a;
    }
    if (a is PXAggregateAttribute aggregateAttribute)
    {
      List<PXEventSubscriberAttribute> attributesAccessor = aggregateAttribute.InternalAttributesAccessor;
      for (int index = 0; index < attributesAccessor.Count; ++index)
      {
        PXEventSubscriberAttribute subscriberAttribute = attributesAccessor[index];
        attributesAccessor[index] = this.CheckoutItemAttribute(state, subscriberAttribute.IndexInClonesArray);
      }
    }
    return a;
  }

  /// <exclude />
  protected PXCache.DirtyItemState GetItemState(object row)
  {
    if (row == null || this._ItemAttributes == null)
      return (PXCache.DirtyItemState) null;
    PXCache.DirtyItemState itemState;
    if (this._ItemAttributes.TryGetValue(row, out itemState))
    {
      ++itemState.RefCnt;
      if (itemState.DirtyAttributes == null)
      {
        if (this._reusableContainer != null)
        {
          itemState.DirtyAttributes = this._reusableContainer;
          this._reusableContainer = (PXEventSubscriberAttribute[]) null;
        }
        else
          itemState.DirtyAttributes = new PXEventSubscriberAttribute[this._ReusableItemAttributes.Length];
      }
    }
    return itemState;
  }

  /// <summary>Checks if a subscriber attribute has an item related dirty clone.</summary>
  /// <exclude />
  /// <typeparam name="T"></typeparam>
  /// <param name="buffer"></param>
  /// <param name="subscriber"></param>
  /// <returns></returns>
  [DebuggerStepThrough]
  protected T GetDirtyAttribute<T>(PXCache.DirtyItemState buffer, T subscriber) where T : class
  {
    PXEventSubscriberAttribute subscriberAttribute = (object) subscriber as PXEventSubscriberAttribute;
    if (buffer != null)
      return this.CheckoutItemAttribute(buffer, subscriberAttribute.IndexInClonesArray) as T;
    if (subscriberAttribute.AttributeLevel != PXAttributeLevel.Cache)
      throw new PXException("GetDirtyAttribute level");
    return subscriber;
  }

  /// <summary>Cleans up unmodified attributes from the item state.</summary>
  /// <exclude />
  /// <param name="buffer"></param>
  protected void CompressItemState(PXCache.DirtyItemState buffer)
  {
    if (buffer == null)
      return;
    --buffer.RefCnt;
    if (buffer.RefCnt > 0)
      return;
    if (buffer.RefCnt < 0)
      throw new PXException("ReleaseItemState RefCnt<0");
    bool flag1 = false;
    for (int index = 0; index < buffer.DirtyAttributes.Length; ++index)
    {
      PXEventSubscriberAttribute dirtyAttribute = buffer.DirtyAttributes[index];
      if (dirtyAttribute != null)
      {
        bool flag2 = dirtyAttribute.IsDirty;
        if (!flag2)
          flag2 = dirtyAttribute.IsDirty = !this.AttributesEqual(dirtyAttribute, dirtyAttribute.Prototype);
        if (flag2)
        {
          flag1 = true;
          this._UsableItemAttributes[index / 32 /*0x20*/] |= 1 << index % 32 /*0x20*/;
          dirtyAttribute.Prototype = (PXEventSubscriberAttribute) null;
        }
        else
        {
          this._ReusableItemAttributes[index] = dirtyAttribute;
          buffer.DirtyAttributes[index] = (PXEventSubscriberAttribute) null;
        }
      }
    }
    if (flag1)
      return;
    this._reusableContainer = buffer.DirtyAttributes;
    buffer.DirtyAttributes = (PXEventSubscriberAttribute[]) null;
  }

  /// <summary>
  /// Bypass collecting values for audit. May be used for passwords ans so on.
  /// </summary>
  /// <exclude />
  public virtual HashSet<string> BypassAuditFields
  {
    get
    {
      if (this._BypassAuditFields == null)
        this._BypassAuditFields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      return this._BypassAuditFields;
    }
  }

  /// <summary>
  /// Bypass collecting values for audit. May be used for passwords ans so on.
  /// </summary>
  /// <exclude />
  public virtual HashSet<string> EncryptAuditFields
  {
    get
    {
      if (this._EncryptAuditFields == null)
        this._EncryptAuditFields = new HashSet<string>();
      return this._EncryptAuditFields;
    }
  }

  internal virtual HashSet<string> SelectingFields
  {
    get
    {
      if (this._SelectingFields == null)
        this._SelectingFields = new HashSet<string>();
      return this._SelectingFields;
    }
  }

  internal virtual HashSet<string> SecuredFields
  {
    get
    {
      if (this._SecuredFields == null)
        this._SecuredFields = new HashSet<string>();
      return this._SecuredFields;
    }
  }

  protected internal object[] convertAttributesFromString(string[] alternatives)
  {
    object[] objArray = new object[alternatives.Length];
    foreach (KeyValuePair<string, int> valueAttributeName in this._KeyValueAttributeNames)
    {
      objArray[valueAttributeName.Value] = (object) alternatives[valueAttributeName.Value];
      StorageBehavior storageBehavior;
      if (this._KeyValueAttributeTypes.TryGetValue(valueAttributeName.Key, out storageBehavior))
      {
        switch (storageBehavior)
        {
          case StorageBehavior.KeyValueNumeric:
            Decimal result1;
            if (Decimal.TryParse(alternatives[valueAttributeName.Value], NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
            {
              objArray[valueAttributeName.Value] = (object) result1;
              continue;
            }
            continue;
          case StorageBehavior.KeyValueDate:
            System.DateTime result2;
            if (System.DateTime.TryParse(alternatives[valueAttributeName.Value], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result2))
            {
              objArray[valueAttributeName.Value] = (object) result2;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    return objArray;
  }

  /// <exclude />
  public ISet<string> GetFieldsWithChangingUpdatability()
  {
    HashSet<string> changingUpdatability = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXContext.SetSlot<HashSet<string>>("_MeasuringUpdatability", changingUpdatability);
    try
    {
      object instance = this.CreateInstance();
      this._MeasuringUpdatability = true;
      this.RaiseRowSelected(instance);
    }
    catch
    {
    }
    finally
    {
      this._MeasuringUpdatability = false;
    }
    return (ISet<string>) changingUpdatability;
  }

  /// <summary>Gets the collection of field names. Placing the field name in this collection forces calculation of the <tt>PXFieldState</tt> object in the <tt>GetValueExt</tt>, <tt>GetValueInt</tt>
  /// methods.</summary>
  /// <remarks>This property internally calls the OnFieldSelecting event with the IsAltered flag.</remarks>
  public virtual ISet<string> AlteredFields
  {
    get
    {
      if (this._AlteredFields == null)
        this._AlteredFields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      return (ISet<string>) this._AlteredFields;
    }
  }

  /// <summary>
  /// Adds or removes the specified field to/from the <tt>AlteredFields</tt> list.
  /// </summary>
  /// <typeparam name="Field">The DAC field.</typeparam>
  /// <param name="isAltered">The value indicating whether the field is added
  /// (<tt>true</tt>) or removed (<tt>false</tt>) from the <tt>AlteredFields</tt> list.</param>
  /// <example>
  /// <code>
  /// Items.Cache.SetAltered&lt;FlatPriceItem.inventoryID&gt;(true);
  /// </code>
  /// </example>
  public virtual void SetAltered<Field>(bool isAltered) where Field : IBqlField
  {
    this.SetAltered(typeof (Field).Name, isAltered);
  }

  /// <summary>
  /// Adds or removes the specified field to/from the <tt>AlteredFields</tt> list.
  /// </summary>
  /// <param name="field">The name of the DAC field.</param>
  /// <param name="isAltered">The value indicating whether the field is added
  /// (<tt>true</tt>) or removed (<tt>false</tt>) from the <tt>AlteredFields</tt> list.</param>
  public virtual void SetAltered(string field, bool isAltered)
  {
    if (string.IsNullOrEmpty(field))
      return;
    if (!isAltered)
    {
      if (this._AlteredFields == null)
        return;
      this._AlteredFields.Remove(field.ToLower());
    }
    else
      this.AlteredFields.Add(field.ToLower());
  }

  /// <summary>Sets value without any validation or event handling</summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <param name="ordinal"></param>
  /// <param name="value"></param>
  public abstract void SetValue(object data, int ordinal, object value);

  /// <exclude />
  public abstract object GetValue(object data, int ordinal);

  /// <summary>sets value without any validation or event handling</summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  public abstract void SetValue(object data, string fieldName, object value);

  /// <summary>Gets the field value by the field name without raising any events.</summary>
  /// <param name="data"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  public abstract object GetValue(object data, string fieldName);

  /// <summary>
  /// Set field value. <br />
  /// Raises events: OnFieldUpdating, OnFieldVerifying, OnFieldUpdated. <br />
  /// If exception - OnExceptionHandling. <br />
  /// </summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <param name="fieldName">The name of the field. The parameter is case-insensitive.</param>
  /// <param name="value"></param>
  public abstract void SetValueExt(object data, string fieldName, object value);

  internal abstract object GetCopy(object data);

  /// <summary>Raises OnFieldUpdating event to set field value</summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <param name="fieldName"></param>
  public abstract void SetDefaultExt(object data, string fieldName, object value = null);

  /// <summary>
  /// Sets the value of the field in the provided data record
  /// without raising events.
  /// </summary>
  /// <typeparam name="Field">The field that is set to the
  /// value.</typeparam>
  /// <param name="data">The data record.</param>
  /// <param name="value">The value to set to the field.</param>
  /// <example>
  /// The code below shows an event handler that sets values to
  /// three fields of the <tt>ARTran</tt> data record on update
  /// of the <tt>UOM</tt> field: two fields are assigned their
  /// default values and the <tt>UnitCost</tt> field is set
  /// directly.
  /// <code>
  /// protected virtual void APTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  /// {
  ///     APTran tran = (APTran)e.Row;
  ///     sender.SetDefaultExt&lt;APTran.unitCost&gt;(tran);
  ///     sender.SetDefaultExt&lt;APTran.curyUnitCost&gt;(tran);
  ///     sender.SetValue&lt;APTran.unitCost&gt;(tran, null);
  /// }
  /// </code>
  /// </example>
  public void SetValue<Field>(object data, object value) where Field : IBqlField
  {
    this.SetValue(data, typeof (Field).Name, value);
  }

  /// <summary>Sets the value of the field in the provided data
  /// record.</summary>
  /// <remarks>The method raises the <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, and <tt>FieldUpdated</tt> events. To set the
  /// value to the field without raising events, use the <see cref="M:PX.Data.PXCache`1.SetValue(System.Object,System.String,System.Object)">SetValue(object,
  /// string, object)</see> method.</remarks>
  /// <typeparam name="Field">The field that is set to the
  /// value.</typeparam>
  /// <param name="data">The data record.</param>
  /// <param name="value">The value to set to the field.</param>
  /// <example>
  /// <code>
  /// protected virtual void APInvoice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  /// {
  ///     APInvoice doc = e.Row as APInvoice;
  /// 
  ///     if (doc.Released != true &amp;&amp; doc.Prebooked != true)
  ///     {
  ///         if (doc.CuryDocBal != doc.CuryOrigDocAmt)
  ///         {
  ///             if (doc.CuryDocBal != null &amp;&amp; doc.CuryDocBal != 0)
  ///                 sender.SetValueExt&lt;APInvoice.curyOrigDocAmt&gt;(doc, doc.CuryDocBal);
  ///             else
  ///                 sender.SetValueExt&lt;APInvoice.curyOrigDocAmt&gt;(doc, 0m);
  ///         }
  ///         ...
  ///     }
  /// }
  /// </code>
  /// </example>
  public void SetValueExt<Field>(object data, object value) where Field : IBqlField
  {
    this.SetValueExt(data, typeof (Field).Name, value);
  }

  /// <summary>Sets the default value to the field in the provided data
  /// record.</summary>
  /// <remarks>The method raises <tt>FieldDefaulting</tt>,
  /// <tt>FieldUpdating</tt>, <tt>FieldVerifying</tt>, and
  /// <tt>FieldUpdated</tt>.</remarks>
  /// <typeparam name="Field">The field to set.</typeparam>
  /// <param name="data">The data record.</param>
  /// <example>
  /// The code below sets default values to the fields of a
  /// <tt>Location</tt> data record.
  /// <code>
  /// // The data view to select Location data records
  /// public PXSelect&lt;Location,
  ///     Where&lt;Location.bAccountID, Equal&lt;Current&lt;BAccount.bAccountID&gt;&gt;&gt;,
  ///     OrderBy&lt;Asc&lt;Location.locationID&gt;&gt;&gt; IntLocations;
  /// ...
  /// public virtual void InitVendorLocation(Location aLoc, string aLocationType)
  /// {
  ///     this.IntLocations.Cache.SetDefaultExt&lt;Location.vCarrierID&gt;(aLoc);
  ///     this.IntLocations.Cache.SetDefaultExt&lt;Location.vFOBPointID&gt;(aLoc);
  ///     this.IntLocations.Cache.SetDefaultExt&lt;Location.vLeadTime&gt;(aLoc);
  ///     this.IntLocations.Cache.SetDefaultExt&lt;Location.vShipTermsID&gt;(aLoc);
  ///     ...
  /// }
  /// </code>
  /// </example>
  public void SetDefaultExt<Field>(object data) where Field : IBqlField
  {
    this.SetDefaultExt(data, typeof (Field).Name);
  }

  /// <summary>Gets the value by the field name. The method raises the OnFieldSelecting event.</summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <param name="fieldName"></param>
  /// <returns>Returns the field value or PXFieldState (if the field is in the <see cref="P:PX.Data.PXCache.AlteredFields">AlteredFields</see> list).</returns>
  public abstract object GetValueExt(object data, string fieldName);

  /// <summary>The same as GetValueExt</summary>
  /// <param name="data"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  /// <exclude />
  public abstract object GetValueInt(object data, string fieldName);

  protected internal virtual object GetValueInt(
    object data,
    string fieldName,
    bool forceState,
    bool externalCall)
  {
    return (object) null;
  }

  /// <exclude />
  internal abstract object GetStateInt(object data, string fieldName);

  /// <summary>Gets the field state by the field name.</summary>
  /// <remarks>Raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <param name="fieldName"></param>
  /// <returns>A <tt>PXFieldState</tt> object generated in the <tt>FieldSelecting</tt> event</returns>
  public abstract object GetStateExt(object data, string fieldName);

  /// <summary>
  /// Gets the value of the specified field in the provided data record.
  /// </summary>
  /// <typeparam name="Field">The field.</typeparam>
  /// <param name="data">The data record.</param>
  /// <returns>The field value.</returns>
  /// <example>
  /// The code below shows a <tt>FieldDefaulting</tt> event handler which
  /// gets the default value for the <tt>LocationExtAddress.VPaymentByType</tt> field
  /// from the current <tt>VendorClass</tt> data record.
  /// <code>
  /// protected virtual void LocationExtAddress_VPaymentByType_FieldDefaulting(
  ///     PXCache sender, PXFieldDefaultingEventArgs e)
  /// {
  ///     if (VendorClass.Current != null)
  ///     {
  ///         e.NewValue = VendorClass.Cache.GetValue&lt;VendorClass.paymentByType&gt;(VendorClass.Current);
  ///         e.Cancel = true;
  ///     }
  /// }
  /// </code>
  /// </example>
  public object GetValue<Field>(object data) where Field : IBqlField
  {
    return this.GetValue(data, typeof (Field).Name);
  }

  /// <summary>Returns the value or the <tt>PXFieldState</tt> object of the
  /// specified field in the given data record. The <tt>PXFieldState</tt>
  /// object is returned if the field is in the <tt>AlteredFields</tt>
  /// collection.</summary>
  /// <param name="data">The data record.</param>
  /// <typeparam name="Field">The field whose value or
  /// <tt>PXFieldState</tt> object is returned.</typeparam>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <returns>Returns the field value or PXFieldState (if the field is in the <see cref="P:PX.Data.PXCache.AlteredFields">AlteredFields</see> list).</returns>
  /// <example>
  /// The code below shows a <tt>RowDeleted</tt> event handler in which you
  /// get the value (or field state) of the <tt>IsDefault</tt> field
  /// of the <tt>POVendorInventory</tt> data record.
  /// <code title="" description="" lang="CS">
  /// protected virtual void POVendorInventory_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  /// {
  ///     POVendorInventory vendor = e.Row as POVendorInventory;
  ///     object isdefault = cache.GetValueExt&lt;POVendorInventory.isDefault&gt;(e.Row);
  ///     if (isdefault is PXFieldState)
  ///     {
  ///         isdefault = ((PXFieldState)isdefault).Value;
  ///     }
  ///     if ((bool?)isdefault == true)
  ///         ...
  /// }</code></example>
  public object GetValueExt<Field>(object data) where Field : IBqlField
  {
    return this.GetValueExt(data, typeof (Field).Name);
  }

  /// <exclude />
  public object GetValueInt<Field>(object data) where Field : IBqlField
  {
    return this.GetValueInt(data, typeof (Field).Name);
  }

  /// <summary>
  /// Gets the <tt>PXFieldState</tt> object of the specified field
  /// in the given data record.
  /// </summary>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <typeparam name="Field">The field whose
  /// <tt>PXFieldState</tt> object is created.</typeparam>
  /// <param name="data">The data record.</param>
  /// <returns>The field state object.</returns>
  /// <example>
  /// The code below shows a part of the <tt>SOOrderEntry</tt> graph
  /// constructor. The field state is created for the
  /// <tt>SOLine.InventoryID</tt> field.
  /// <code>
  /// public SOOrderEntry()
  /// {
  ///     ...
  ///     PXFieldState state = (PXFieldState)this.Transactions.Cache.GetStateExt&lt;SOLine.inventoryID&gt;(null);
  ///     viewInventoryID = state != null ? state.ViewName : null;
  ///     ...
  /// }
  /// </code>
  /// </example>
  public object GetStateExt<Field>(object data) where Field : IBqlField
  {
    return this.GetStateExt(data, typeof (Field).Name);
  }

  /// <summary>
  /// Specified value stored in internal dictionary associated with data row,  <br />
  /// and can be retrived later by GetValuePending method.
  /// </summary>
  /// <param name="data"></param>
  /// <param name="fieldName"></param>
  /// <param name="value"></param>
  public abstract void SetValuePending(object data, string fieldName, object value);

  internal abstract bool HasPendingValues(object data);

  /// <summary>returns value stored by SetValuePending</summary>
  /// <exclude />
  /// <param name="data"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  public abstract object GetValuePending(object data, string fieldName);

  /// <exclude />
  public object GetValuePending<Field>(object data) where Field : IBqlField
  {
    string name = typeof (Field).Name;
    return this.GetValuePending(data, char.ToUpper(name[0]).ToString() + name.Substring(1));
  }

  public void SetValuePending<Field>(object data, object value) where Field : IBqlField
  {
    string name = typeof (Field).Name;
    this.SetValuePending(data, char.ToUpper(name[0]).ToString() + name.Substring(1), value);
  }

  /// <summary>returns value from instance copy stored in database</summary>
  /// <exclude />
  /// <param name="data"></param>
  /// <param name="fieldName"></param>
  /// <returns></returns>
  public abstract object GetValueOriginal(object data, string fieldName);

  /// <exclude />
  public object GetValueOriginal<Field>(object data) where Field : IBqlField
  {
    string name = typeof (Field).Name;
    return this.GetValueOriginal(data, char.ToUpper(name[0]).ToString() + name.Substring(1));
  }

  /// <exclude />
  public abstract object GetOriginal(object data);

  internal static string[] GetKeyNames(PXGraph graph, System.Type cacheType)
  {
    Dictionary<System.Type, string[]> dictionary;
    string[] array;
    lock (((ICollection) PXCache._keys).SyncRoot)
    {
      if (!PXCache._keys.TryGetValue(cacheType, out dictionary))
        PXCache._keys[cacheType] = dictionary = new Dictionary<System.Type, string[]>();
      dictionary.TryGetValue(graph.GetType(), out array);
    }
    if (array == null)
    {
      array = graph.Caches[cacheType].Keys.ToArray();
      lock (((ICollection) PXCache._keys).SyncRoot)
        dictionary[graph.GetType()] = array;
    }
    return array;
  }

  /// <summary>
  /// The collection of field names that form the primary key
  /// of the data record. The collection is usually composed of
  /// the fields that have attributes with the <tt>IsKey</tt> property
  /// set to <see langword="true" />.
  /// </summary>
  public virtual KeysCollection Keys
  {
    get
    {
      if (this._Keys == null)
        this._Keys = new KeysCollection();
      return this._Keys;
    }
  }

  /// <summary>
  /// When set to <c>true</c> makes the cache sort the accumulator records
  /// by keys before executing update/insert commands, which allows to
  /// avoid deadlocks in case of multithreaded update of the same accumulator table.
  /// </summary>
  [Obsolete("Use CustomDeadlockComparison instead.", false)]
  public bool PreventDeadlock
  {
    get => this._PreventDeadlock;
    set => this._PreventDeadlock = value;
  }

  /// <summary>
  /// When set, makes the cache sort the accumulator records
  /// by custom comparison before executing update/insert commands, which allows to
  /// avoid deadlocks in case of multithreaded update of the same accumulator table.
  /// </summary>
  public Comparison<object> CustomDeadlockComparison
  {
    get => this._CustomDeadlockComparison;
    set => this._CustomDeadlockComparison = value;
  }

  /// <summary>
  /// Gets the name of the identity field if the DAC defines it.
  /// </summary>
  public virtual string Identity => this._Identity;

  /// <summary>
  /// Gets the name of the Row ID (identity, guid or foreign identity) field if the DAC defines it.
  /// </summary>
  public virtual string RowId => this._RowId;

  public virtual int SetupSlot<TSlot>(
    Func<TSlot> create,
    Func<TSlot, TSlot, TSlot> update,
    Func<TSlot, TSlot> clone)
  {
    if (this._SlotDelegates == null)
      this._SlotDelegates = new List<Tuple<Delegate, Delegate, Delegate>>();
    this._SlotDelegates.Add(new Tuple<Delegate, Delegate, Delegate>((Delegate) (() => (object) create()), (Delegate) ((item, copy) => (object) update((TSlot) item, (TSlot) copy)), (Delegate) (item => (object) clone((TSlot) item))));
    return this._SlotDelegates.Count - 1;
  }

  internal virtual int SlotsCount
  {
    get
    {
      List<Tuple<Delegate, Delegate, Delegate>> slotDelegates = this._SlotDelegates;
      return slotDelegates == null ? 0 : __nonvirtual (slotDelegates.Count);
    }
  }

  protected internal abstract void _AdjustStorage(int i, PXDataFieldParam assign);

  protected internal abstract void _AdjustStorage(string name, PXDataFieldParam assign);

  protected internal virtual bool _HasKeyValueStored()
  {
    return this._KeyValueStoredNames != null || this._KeyValueAttributeNames != null;
  }

  protected internal virtual string _ReportGetFirstKeyValueStored(string fieldName)
  {
    return !this.IsKvExtField(fieldName) ? (string) null : this._FirstKeyValueStored.Value.Key;
  }

  protected internal virtual string _ReportGetFirstKeyValueAttribute(string fieldName)
  {
    return !this.IsKvExtAttribute(fieldName) ? (string) null : this._FirstKeyValueAttribute.Value.Key;
  }

  public bool IsKvExtField(string fieldName)
  {
    return this._KeyValueStoredNames != null && this._KeyValueStoredNames.ContainsKey(fieldName);
  }

  public virtual bool IsKvExtAttribute(string fieldName)
  {
    return this._KeyValueAttributeNames != null && this._KeyValueAttributeNames.ContainsKey(fieldName);
  }

  /// <summary>
  /// Gets the list of names of the fields that are considered as immutable.
  /// Immutable fields are the fields that have attributes with the
  /// <tt>IsImmutable</tt> property set to <tt>true</tt>.
  /// </summary>
  public virtual List<string> Immutables
  {
    get
    {
      if (this._Immutables == null)
        this._Immutables = new List<string>();
      return this._Immutables;
    }
  }

  /// <summary>
  /// Return true if the <paramref name="field" /> value is auto number
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  public bool IsAutoNumber(string field)
  {
    return !string.IsNullOrEmpty(field) && this._AutoNumberFields != null && this._AutoNumberFields.Contains(field);
  }

  /// <summary>
  /// Set the <paramref name="field" /> as auto number
  /// </summary>
  /// <param name="field"></param>
  public void SetAutoNumber(string field)
  {
    if (string.IsNullOrEmpty(field) || !this.Fields.Contains(field))
      return;
    if (this._AutoNumberFields == null)
      this._AutoNumberFields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._AutoNumberFields.Add(field);
  }

  /// <summary>Gets the collection of names of fields and virtual fields.</summary>
  /// <value>By default, the collection includes all public properties of the DAC that is associated with the cache. The collection may also include the virtual fields that
  /// are injected by attributes (such as the description field of the PXSelector attribute). The developer can add any field to the collection.</value>
  public virtual PXFieldCollection Fields
  {
    get
    {
      if (this._Fields == null)
        this._Fields = new PXFieldCollection((IEnumerable<string>) new string[0], new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
      return this._Fields;
    }
  }

  internal virtual PXResult _InvokeTailAppender(object data, Query query, object[] pars)
  {
    return (PXResult) null;
  }

  internal virtual object _InvokeSelectorGetter(
    object data,
    string field,
    PXView view,
    object[] pars,
    bool unwrap)
  {
    return (object) null;
  }

  internal virtual bool _InvokeMeetVerifier(object data, Query query, object[] pars) => true;

  /// <summary>Allows you to intercept the database operations (insertion, update and removal of a row in the database).</summary>
  public abstract PXDBInterceptorAttribute Interceptor { get; set; }

  public IPXSelectInterceptor SelectInterceptor { get; set; }

  /// <summary>Gets the list of classes that implement IBqlField and are nested in the DAC and its base type. These types represent DAC fields in BQL queries. This list
  /// differs from the list that the Fields property returns.</summary>
  public virtual List<System.Type> BqlFields
  {
    get
    {
      if (this._BqlFields == null)
        this._BqlFields = new List<System.Type>();
      return this._BqlFields;
    }
  }

  /// <summary>Searchs for the specified BQL field in the <see cref="P:PX.Data.PXCache.Fields">Fields</see> collection.</summary>
  /// <param name="bqlField">A BQL field.</param>
  /// <returns></returns>
  public string GetField(System.Type bqlField)
  {
    string name = bqlField.Name;
    foreach (string field in (List<string>) this.Fields)
    {
      if (string.Equals(field, name, StringComparison.OrdinalIgnoreCase))
        return field;
    }
    return (string) null;
  }

  /// <summary>
  /// Searches for the specified field in the <see cref="P:PX.Data.PXCache`1.BqlFields">BqlFields</see>
  /// collection.
  /// </summary>
  /// <param name="field">The field to find.</param>
  /// <returns>The abstract type implementing <tt>IBqlField</tt>.</returns>
  public virtual System.Type GetBqlField(string field)
  {
    foreach (System.Type bqlField in this.BqlFields)
    {
      if (string.Equals(field, bqlField.Name, StringComparison.OrdinalIgnoreCase))
        return bqlField;
    }
    return (System.Type) null;
  }

  internal virtual System.Type GetBaseBqlField(string field) => this.GetBqlField(field);

  /// <exclude />
  protected internal string GetFieldName(string name, bool needBql)
  {
    if (needBql)
    {
      for (int index = this.BqlFields.Count - 1; index >= 0; --index)
      {
        if (string.Compare(this.BqlFields[index].Name, name, StringComparison.OrdinalIgnoreCase) == 0)
          return this.BqlFields[index].Name;
      }
    }
    else
    {
      for (int index = 0; index < this.Fields.Count; ++index)
      {
        if (string.Compare(this.Fields[index], name, StringComparison.OrdinalIgnoreCase) == 0)
          return this.Fields[index];
      }
    }
    return (string) null;
  }

  /// <summary>Gets the collection of BQL types that correspond to the key fields that the DAC defines.</summary>
  public virtual List<System.Type> BqlKeys
  {
    get
    {
      if (this._BqlKeys == null)
        this._BqlKeys = new List<System.Type>();
      return this._BqlKeys;
    }
  }

  public virtual List<System.Type> BqlImmutables
  {
    get
    {
      if (this._BqlImmutables == null)
        this._BqlImmutables = new List<System.Type>();
      return this._BqlImmutables;
    }
  }

  /// <summary>Looks like obsolete property. The value of this property is not used.</summary>
  /// <exclude />
  public abstract BqlCommand BqlSelect { get; set; }

  /// <summary>Gets the DAC the cache is associated with. The DAC is specified through the type parameter when the cache is instantiated.</summary>
  public abstract System.Type BqlTable { get; }

  public virtual System.Type GenericParameter => (System.Type) null;

  /// <exclude />
  public static System.Type GetBqlTable(System.Type dac)
  {
    System.Type bqlTable = dac;
    while (bqlTable != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(bqlTable.BaseType) && !bqlTable.IsDefined(typeof (PXTableAttribute), false) && (!bqlTable.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) bqlTable.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive))
      bqlTable = bqlTable.BaseType;
    return bqlTable;
  }

  /// <summary>Save row field values to Dictionary</summary>
  /// <param name="data">IBqlTable</param>
  /// <returns></returns>
  public abstract Dictionary<string, object> ToDictionary(object data);

  public abstract string ToXml(object data);

  /// <exclude />
  public abstract object FromXml(string xml);

  /// <exclude />
  public abstract string ValueToString(string fieldName, object val);

  internal virtual string ValueToString(string fieldName, object val, object dbval)
  {
    return this.ValueToString(fieldName, val);
  }

  internal virtual string AttributeValueToString(string fieldName, object val)
  {
    if (val == null)
      return (string) null;
    if (val is string || this._KeyValueAttributeTypes[fieldName] == StorageBehavior.KeyValueString || this._KeyValueAttributeTypes[fieldName] == StorageBehavior.KeyValueText)
      return val.ToString();
    if (this._KeyValueAttributeTypes[fieldName] == StorageBehavior.KeyValueDate && val is System.DateTime dateTime)
      return dateTime.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    if (this._KeyValueAttributeTypes[fieldName] == StorageBehavior.KeyValueNumeric)
    {
      switch (val)
      {
        case int num1:
          return num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case bool flag:
          return flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case Decimal num2:
          return num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }
    return val.ToString();
  }

  /// <exclude />
  public abstract object ValueFromString(string fieldName, string val);

  /// <exclude />
  public abstract int GetFieldCount();

  /// <exclude />
  public abstract int GetFieldOrdinal(string field);

  /// <exclude />
  public abstract int GetFieldOrdinal<Field>() where Field : IBqlField;

  internal abstract System.Type GetFieldType(string fieldName);

  /// <summary>
  /// Repair internal hashtable, if user has changed the key values of stored data rows.
  /// </summary>
  public abstract void Normalize();

  /// <summary>Gets or sets the business logic controller the cache is related to.</summary>
  public virtual PXGraph Graph
  {
    get => this._Graph;
    set => this._Graph = value;
  }

  protected internal virtual bool SelectRights
  {
    get => this._SelectRights;
    set
    {
      this._SelectRights = value;
      if (value)
        return;
      this._AllowSelect = false;
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates whether the cache
  /// allows selection of data records from the user interface.
  /// </summary>
  public virtual bool AllowSelect
  {
    get => !this.AutomationHidden && this._AllowSelect;
    set
    {
      if (!this._SelectRights)
        return;
      if (this._AllowSelect != value)
        this._AllowSelectChanged = true;
      this._AllowSelect = value;
    }
  }

  protected internal virtual bool UpdateRights
  {
    get => this._UpdateRights;
    set
    {
      this._UpdateRights = value;
      if (value)
        return;
      this._AllowUpdate = false;
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates whether the cache allows
  /// update of data records from the user interface. This value does
  /// not affect the ability to update a data record via the methods.
  /// By default, the property equals true.
  /// </summary>
  public virtual bool AllowUpdate
  {
    get => !this.AutomationUpdateDisabled && this._AllowUpdate;
    set
    {
      if (!this._UpdateRights)
        return;
      this._AllowUpdate = value;
    }
  }

  protected internal virtual bool InsertRights
  {
    get => this._InsertRights;
    set
    {
      this._InsertRights = value;
      if (value)
        return;
      this._AllowInsert = false;
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates whether the cache allows insertion
  /// of data records from the user interface. This value does not affect the
  /// ability to insert a data record via the methods. By default, the property
  /// equals <tt>true</tt>.
  /// </summary>
  public virtual bool AllowInsert
  {
    get => !this.AutomationInsertDisabled && this._AllowInsert;
    set
    {
      if (!this._InsertRights)
        return;
      this._AllowInsert = value;
    }
  }

  protected internal virtual bool DeleteRights
  {
    get => this._DeleteRights;
    set
    {
      this._DeleteRights = value;
      if (value)
        return;
      this._AllowDelete = false;
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates whether the cache allows
  /// deletion of data records from the user interface. This value does
  /// not affect the ability to delete a data record via the methods.
  /// By default, the property equals <tt>true</tt>.
  /// </summary>
  public virtual bool AllowDelete
  {
    get => !this.AutomationDeleteDisabled && this._AllowDelete;
    set
    {
      if (!this._DeleteRights)
        return;
      this._AllowDelete = value;
    }
  }

  internal abstract object _Clone(object item);

  public virtual bool AutoSave
  {
    get => this._AutoSave;
    set => this._AutoSave = value;
  }

  /// <exclude />
  public static List<System.Type> getBqlTableAndParents(System.Type bqlTable)
  {
    List<System.Type> bqlTableAndParents = new List<System.Type>();
    for (; typeof (IBqlTable).IsAssignableFrom(bqlTable); bqlTable = bqlTable.BaseType)
      bqlTableAndParents.Add(bqlTable);
    return bqlTableAndParents;
  }

  /// <exclude />
  public abstract System.Type[] GetExtensionTypes();

  public static bool IsActiveExtension(System.Type extension)
  {
    foreach (System.Type allExtension in PXCache.GetAllExtensions())
    {
      if (allExtension == extension)
      {
        try
        {
          MethodInfo method = allExtension.GetMethod("IsActive", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
          object obj;
          return !(method != (MethodInfo) null) || !(method.ReturnType == typeof (bool)) || !((obj = method.Invoke((object) null, new object[0])) is bool) || (bool) obj;
        }
        catch
        {
          return false;
        }
      }
    }
    return false;
  }

  private static IEnumerable<System.Type> GetAllExtensions()
  {
    if (WebConfig.DisableExtensions)
      return (IEnumerable<System.Type>) new System.Type[0];
    if (PXCache._DynamicExtensions == null)
      PXCache._DynamicExtensions = PXCodeDirectoryCompiler.GetCompiledTypes<PXCacheExtension>().ToArray();
    if (!PXCache._initDynamicExtensionsWatcher)
    {
      PXCache._initDynamicExtensionsWatcher = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXCodeDirectoryCompiler.NotifyOnChange(PXCache.\u003C\u003EO.\u003C1\u003E__ClearCaches ?? (PXCache.\u003C\u003EO.\u003C1\u003E__ClearCaches = new System.Action(PXCache.ClearCaches)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXCache.ClearCacheExtensionsDelegate = PXCache.\u003C\u003EO.\u003C1\u003E__ClearCaches ?? (PXCache.\u003C\u003EO.\u003C1\u003E__ClearCaches = new System.Action(PXCache.ClearCaches));
    }
    return ((IEnumerable<System.Type>) PXCache._AvailableExtensions).Union<System.Type>((IEnumerable<System.Type>) PXCache._DynamicExtensions);
  }

  private static void ClearCaches()
  {
    PXCache._DynamicExtensions = (System.Type[]) null;
    PXCache._ExtensionTables = (Dictionary<string, List<KeyValuePair<string, List<string>>>>) null;
    PXDatabase.ResetSlotForAllCompanies(typeof (AuditSetup).FullName, typeof (AUAuditSetup), typeof (AUAuditTable), typeof (AUAuditField));
    PXDatabase.ResetSlotForAllCompanies("CacheStaticInfo", typeof (PXGraph.FeaturesSet));
    PXBuildManager.ClearTypeCache();
    lock (PXExtensionManager._StaticInfoLock)
      PXExtensionManager._CacheStaticInfo.Clear();
  }

  internal static List<System.Type> _GetExtensions(System.Type tnode, bool checkActive)
  {
    return PXCache._GetExtensions(tnode, checkActive, out Dictionary<string, string> _);
  }

  private static bool ExtensionIsActive(System.Type extensionType)
  {
    MethodInfo method = extensionType.GetMethod("IsActive", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
    object obj;
    return method == (MethodInfo) null || method.ReturnType != typeof (bool) || !((obj = method.Invoke((object) null, new object[0])) is bool) || (bool) obj;
  }

  protected internal static List<System.Type> _GetExtensions(
    System.Type tnode,
    bool checkActive,
    out Dictionary<string, string> inactiveFields)
  {
    inactiveFields = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<System.Type> bqlTableAndParents = PXCache.getBqlTableAndParents(tnode);
    List<System.Type> ret = new List<System.Type>();
    for (int index = bqlTableAndParents.Count - 1; index >= 0; --index)
    {
      System.Type extendedTable = bqlTableAndParents[index];
      List<System.Type> list = new List<System.Type>();
      foreach (System.Type allExtension in PXCache.GetAllExtensions())
      {
        System.Type[] genericArguments;
        if (typeof (PXCacheExtension).IsAssignableFrom(allExtension) && allExtension.BaseType.IsGenericType && (genericArguments = allExtension.BaseType.GetGenericArguments()).Length != 0 && genericArguments[genericArguments.Length - 1] == extendedTable)
        {
          if (checkActive)
          {
            if (!PXCache.ExtensionIsActive(allExtension))
            {
              try
              {
                foreach (PropertyInfo property in allExtension.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                  IPXInterfaceField pxInterfaceField = ((IEnumerable<object>) property.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (_ => _ is IPXInterfaceField)).FirstOrDefault<object>() as IPXInterfaceField;
                  string str = property.Name;
                  if (pxInterfaceField != null && !string.IsNullOrWhiteSpace(pxInterfaceField.DisplayName))
                    str = pxInterfaceField.DisplayName;
                  inactiveFields[property.Name] = str;
                }
                continue;
              }
              catch
              {
                continue;
              }
            }
          }
          list.Add(allExtension);
        }
      }
      foreach (IBqlMapping bqlMapping in PXCache._mapping.GetAllMaps().Where<IBqlMapping>((Func<IBqlMapping, bool>) (m => m.Table == extendedTable && !ret.Contains(m.Extension))))
        list.Add(bqlMapping.Extension);
      ret.AddRange((IEnumerable<System.Type>) PXExtensionManager.Sort(list));
    }
    return ret;
  }

  /// <summary>Gets an extension of appropriate type.</summary>
  /// <typeparam name="Extension">The type of extension requested</typeparam>
  /// <param name="item">Parent standard object</param>
  /// <returns>Object of type Extension</returns>
  public abstract Extension GetExtension<Extension>(object item) where Extension : PXCacheExtension;

  /// <exclude />
  public abstract object GetMain<Extension>(Extension item) where Extension : PXCacheExtension;

  /// <summary>Gets the type of data rows in the cache.</summary>
  /// <returns>Containing type.</returns>
  public abstract System.Type GetItemType();

  /// <summary>
  ///   <para>Gets or sets the current data record. This property can point to the last data record displayed in the user interface. If the user selects a data record in
  /// a grid, this property points to this data record. If the user or the application inserts, updates, or deletes a data record, the property points to this data
  /// record. Insertion and update of records through the API methods also assign this property.</para>
  /// </summary>
  /// <remarks>
  ///   <para>When this property is assigned, the RowSelected event is raised.</para>
  ///   <para>You can reference the Current data record and its fields in the PXSelect BQL statements by using the Current parameter.</para>
  /// </remarks>
  /// <value>Contains value of type IBqlTable.</value>
  public abstract object Current { get; set; }

  public abstract object InternalCurrent { get; }

  /// <summary>Compares the values of the key fields. The list of key fields is taken from the cache.</summary>
  /// <param name="a">IBqlTable or IDictionary</param>
  /// <param name="b">IBqlTable or IDictionary</param>
  /// <returns>Returns true if the values of the key fields are equal.</returns>
  public abstract bool ObjectsEqual(object a, object b);

  /// <summary>Compares the values of the specified fields.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The field to compare.</typeparam>
  /// <returns>Returns true if the values of the specified fields in the specified rows are equal.</returns>
  /// <example><para>To check whether a field has been changed, you use the ObjectsEqual&lt;&gt;() method of the cache as follows.</para>
  ///   <code title="Example" lang="CS">
  /// if (!sender.ObjectsEqual&lt;ShipmentLine.lineQty&gt;(newLine, oldLine)
  ///     ...</code>
  /// </example>
  public bool ObjectsEqual<Field1>(object a, object b) where Field1 : IBqlField
  {
    return object.Equals(this.GetValue<Field1>(a), this.GetValue<Field1>(b));
  }

  /// <summary>Compares the values of two fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  /// <example><para>You can specify up to eight fields as type parameters of the ObjectsEqual&lt;&gt;() method to compare two data records by using these fields.</para>
  ///   <code title="Example" lang="CS">
  /// if (!sender.ObjectsEqual&lt;ShipmentLine.lineQty,
  ///                          ShipmentLine.cancelled&gt;(newLine, oldLine)
  ///     ...</code>
  /// </example>
  public bool ObjectsEqual<Field1, Field2>(object a, object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2>(a, b);
  }

  /// <summary>Compares the values of three fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3>(object a, object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3>(a, b);
  }

  /// <summary>Compares the values of four fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field4">The fourth field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3, Field4>(object a, object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3, Field4>(a, b);
  }

  /// <summary>Compares the values of five fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field5">The fifth field to compare.</typeparam>
  /// <typeparam name="Field4">The fourth field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3, Field4, Field5>(object a, object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3, Field4, Field5>(a, b);
  }

  /// <summary>Compares the values of six fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field6">The sixth field to compare.</typeparam>
  /// <typeparam name="Field5">The fifth field to compare.</typeparam>
  /// <typeparam name="Field4">The fourth field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3, Field4, Field5, Field6>(object a, object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3, Field4, Field5, Field6>(a, b);
  }

  /// <summary>Compares the values of seven fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field7">The seventh field to compare.</typeparam>
  /// <typeparam name="Field6">The sixth field to compare.</typeparam>
  /// <typeparam name="Field5">The fifth field to compare.</typeparam>
  /// <typeparam name="Field4">The fourth field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3, Field4, Field5, Field6, Field7>(
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3, Field4, Field5, Field6, Field7>(a, b);
  }

  /// <summary>Compares the values of eight fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="Field1">The first field to compare.</typeparam>
  /// <typeparam name="Field2">The second field to compare.</typeparam>
  /// <typeparam name="Field3">The third field to compare.</typeparam>
  /// <typeparam name="Field4">The fourth field to compare.</typeparam>
  /// <typeparam name="Field5">The fifth field to compare.</typeparam>
  /// <typeparam name="Field6">The sixth field to compare.</typeparam>
  /// <typeparam name="Field7">The seventh field to compare.</typeparam>
  /// <typeparam name="Field8">The eighth field to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqual<Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8>(
    object a,
    object b)
    where Field1 : IBqlField
    where Field2 : IBqlField
    where Field3 : IBqlField
    where Field4 : IBqlField
    where Field5 : IBqlField
    where Field6 : IBqlField
    where Field7 : IBqlField
    where Field8 : IBqlField
  {
    return this.ObjectsEqual<Field1>(a, b) && this.ObjectsEqual<Field2, Field3, Field4, Field5, Field6, Field7, Field8>(a, b);
  }

  /// <summary>Compares the values of given fields in the specified records.</summary>
  /// <param name="a">The first row to compare.</param>
  /// <param name="b">The second row to compare.</param>
  /// <typeparam name="TFields">The set of fields to compare.</typeparam>
  /// <returns>If the values of any of the fields you specify differ, the data records are considered different and the method returns false.</returns>
  public bool ObjectsEqualBy<TFields>(object a, object b) where TFields : ITypeArrayOf<IBqlField>
  {
    return ((IEnumerable<System.Type>) TypeArray.CheckAndExtract<IBqlField>(typeof (TFields), (string) null)).All<System.Type>((Func<System.Type, bool>) (f => object.Equals(this.GetValue(a, f.Name), this.GetValue(b, f.Name))));
  }

  /// <summary>Returns the hash code generated from the key field values.</summary>
  /// <param name="data">IBqlTable or IDictionary</param>
  /// <returns></returns>
  public abstract int GetObjectHashCode(object data);

  /// <summary>Displays key fields in format {k1=v1, k2=v2}</summary>
  /// <param name="data">IBqlTable or PXResult</param>
  /// <returns></returns>
  public abstract string ObjectToString(object data);

  /// <summary>Looks for an object in the cache and returns the status of the object. The item is located by the values of the key fields.</summary>
  /// <param name="item">Cache item to test, IBqlTable</param>
  /// <returns></returns>
  public abstract PXEntryStatus GetStatus(object item);

  /// <summary>
  /// Looks for an item in the cache and sets the status of the item.
  /// If the item is not in the cache, the item is inserted.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="status"></param>
  public abstract void SetStatus(object item, PXEntryStatus status);

  /// <summary>
  /// Searches data row in the cache.<br />
  /// Returns located row.
  /// </summary>
  /// <param name="item">IBqlRow</param>
  /// <returns></returns>
  public abstract object Locate(object item);

  protected internal abstract bool IsPresent(object item);

  protected internal abstract bool IsGraphSpecificField(string fieldName);

  /// <summary>
  /// Searches data row in the cache. <br />
  /// If row not found in cache, <br />
  /// reads it from the database and places into the cache with status NotChanged.<br />
  /// </summary>
  public abstract int Locate(IDictionary keys);

  /// <summary>Remove entry from cache items hashtable.</summary>
  /// <param name="item"></param>
  public abstract void Remove(object item);

  /// <summary>
  /// Places row into the cache with status Updated.<br />
  /// If row does not exists in the cache, looks for it in database.<br />
  /// If row does not exists in database, inserts row with status Inserted.<br />
  /// Raise events OnRowUpdating, OnRowUpdated and other events.<br />
  /// This method is used to update row from user interface.<br />
  /// Flag AllowUpdate may cancel this method.<br />
  /// </summary>
  /// <param name="keys">Primary key of the item.</param>
  /// <param name="values">New field values to update the item with.</param>
  /// <returns>Returns 1 if updated successfully, otherwise 0.</returns>
  public abstract int Update(IDictionary keys, IDictionary values);

  /// <summary>
  /// Places a row into the cache with the Updated status.
  /// If the row does not exists in the cache, the method looks for it in the database.
  /// If the row does not exists in the database, the method inserts the row with the Inserted status.</summary>
  /// <remarks>The method raises the OnRowUpdating, OnRowUpdated and other events.
  /// The <see cref="P:PX.Data.PXCache.AllowUpdate" /> flag does not affect this method.
  /// </remarks>
  /// <param name="item">IBqlTable of the cache item type</param>
  /// <returns></returns>
  public abstract object Update(object item);

  /// <summary>
  /// Place row into the cache with status Updated.<br />
  /// If row does not exists in the cache, looks for it in database.<br />
  /// If row does not exists in database, inserts row with status Inserted.<br />
  /// Raise events OnRowUpdating, OnRowUpdated and other events.
  /// </summary>
  /// <param name="item">IBqlTable of type [CacheItemType]</param>
  /// <param name="bypassinterceptor">whether to ignore PXDBInterceptorAttribute</param>
  protected internal abstract object Update(object item, bool bypassinterceptor);

  /// <summary>
  /// Updates Last Modified state of the row into the cache. <br />
  /// Called at the end of RowUpdated, RowInserted event. <br />
  /// Last modified is used to track properties changes on RowUpdate. <br />
  /// Has influence on Accumulator Attributes <br />
  /// </summary>
  /// <param name="item">Row updated/inserted</param>
  /// <param name="inserted">Flag insert/update</param>
  protected internal abstract void UpdateLastModified(object item, bool inserted);

  /// <summary>
  /// Updates Last Modified state of the row into the cache. <br />
  /// Called at the end of <see cref="M:PX.Data.GraphHelper.MarkUpdated(PX.Data.PXCache,System.Object,System.Boolean)" />, <see cref="M:PX.Data.PXCache.SetStatus(System.Object,PX.Data.PXEntryStatus)" /> methods. <br />
  /// Last modified is used to track properties changes on RowUpdate. <br />
  /// Has influence on Accumulator Attributes <br />
  /// Also, executes <see cref="M:PX.Data.PXCache.SetSessionUnmodified(System.Object,System.Object)" /> method.
  /// </summary>
  /// <param name="item">Row updated</param>
  /// <param name="existingStatus">Current status</param>
  /// <param name="newStatus">New status</param>
  protected internal abstract void EnsureUnmodified(
    object item,
    PXEntryStatus? existingStatus,
    PXEntryStatus? newStatus);

  protected internal abstract void SetSessionUnmodified(object item, object unmodified);

  protected internal abstract void ClearSessionUnmodified();

  internal abstract void ReinitializeCollection();

  /// <summary>Create new Version of Graph on Action executed</summary>
  /// <param name="item"></param>
  protected internal abstract void CreateNewVersion(object item);

  /// <summary>
  /// Make DiscardChanges - return to previous stored version
  /// </summary>
  /// <param name="item"></param>
  protected internal abstract void DiscardCurrentVersion(object item);

  protected internal abstract void SaveCurrentVersion(object item);

  /// <summary>Check is Graph was modified</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected internal abstract bool IfModifiedVersion(object item);

  /// <summary>Reqrites VersionModified on Update()</summary>
  /// <param name="item"></param>
  protected internal abstract void SetVersionModified(object item);

  /// <summary>Inserts a new row into the cache.</summary>
  /// <remarks>The method raises the OnRowInserting, OnRowInserted and other field related events.
  /// It does not check the database for existing row.
  /// The <see cref="P:PX.Data.PXCache.AllowInsert" /> flag can cancel this method.</remarks>
  /// <param name="values">Field values to populate the item before inserting.
  /// The values of dictionary are not read-only and can be updated during method call.</param>
  /// <returns>Returns 1 if inserted successfully, otherwise 0.</returns>
  public abstract int Insert(IDictionary values);

  internal abstract object FillItem(IDictionary values);

  /// <summary>
  /// Inserts  new row into the cache. <br />
  /// Returns inserted row of type [CacheItemType] or null if row was not inserted.<br />
  /// Raises events OnRowInserting, OnRowInserted and other field related events.<br />
  /// Does not check the database for existing row.
  /// Flag AllowInsert does not affects this method.
  /// </summary>
  /// <param name="item">IBqlTable of type [CacheItemType]</param>
  public abstract object Insert(object item);

  /// <summary>
  /// Inserts new row into the cache. <br />
  /// Returns inserted row of type [CacheItemType] or null if row was not inserted.<br />
  /// Raises events OnRowInserting, OnRowInserted and other field related events.<br />
  /// Does not check the database for existing row.<br />
  /// </summary>
  /// <param name="item">IBqlTable of type [CacheItemType]</param>
  /// <param name="bypassinterceptor">whether to ignore PXDBInterceptorAttribute</param>
  /// <returns></returns>
  protected internal abstract object Insert(object item, bool bypassinterceptor);

  /// <summary>Place new row into the cache.</summary>
  /// <returns></returns>
  public abstract object Insert();

  /// <summary>Returns a new TNode();</summary>
  /// <exclude />
  public abstract object CreateInstance();

  internal abstract object ToChildEntity<Parent>(Parent item) where Parent : class, IBqlTable, new();

  /// <summary>Inserts a row into the cache if the row type distincts from the cache item type.</summary>
  /// <typeparam name="Parent"></typeparam>
  /// <param name="item"></param>
  /// <returns>Returns the inserted row.</returns>
  public abstract object Extend<Parent>(Parent item) where Parent : class, IBqlTable, new();

  /// <summary>Places the item into the cache with the Deleted status. The method raises the OnRowDeleting, OnRowDeleted events. This method is used to delete a row from
  /// the user interface. The <see cref="P:PX.Data.PXCache.AllowDelete">AllowDelete</see> property can cancel this method.</summary>
  /// <param name="keys">The primary key of the item.</param>
  /// <param name="values">The parameter is not used. The value can be null.</param>
  /// <returns>Returns 1 if deleted successfully, otherwise returns 0.</returns>
  public abstract int Delete(IDictionary keys, IDictionary values);

  /// <summary>Places the item into the cache with the Deleted status. The method raises the OnRowDeleting, OnRowDeleted events. The <see cref="P:PX.Data.PXCache.AllowDelete">AllowDelete</see> property
  /// does not affect this method.</summary>
  /// <param name="item">The item to be placed in the cache.</param>
  public abstract object Delete(object item);

  /// <exclude />
  protected internal abstract object Delete(object item, bool bypassinterceptor);

  /// <summary>
  /// Looks for a row in the cache collection. If the row has the deleted or insertedDeleted status, the method returns null.
  /// Returns the row inserted to the cache.
  /// </summary>
  protected internal abstract void PlaceNotChanged(object data);

  /// <summary>
  /// Looks for a row in the cache collection. If the row has the deleted or insertedDeleted status, the method returns null.
  /// If the status of the row is inserted or updated, the wasUpdated flag is true.
  /// Returns the row inserted to cache.
  /// </summary>
  protected internal abstract object PlaceNotChanged(object data, out bool wasUpdated);

  /// <summary>Creates a data record from the PXDataRecord object and places it into the cache with the NotChanged status if the data record isn't found among the modified
  /// data records in the cache.</summary>
  /// <param name="record">The PXDataRecord object to convert to the DAC type of the cache.</param>
  /// <param name="position">The index of the first field to read in the list of columns comprising the PXDataRecord object.</param>
  /// <param name="isReadOnly">The value that indicates (if true) that the data record with the same key fields should be located in the cache and updated.</param>
  /// <param name="wasUpdated">The value that indicates (if true) that the data record with the same keys existed in the cache among the modified data records.</param>
  /// <remarks>
  ///   <para>If isReadOnly is false then:</para>
  ///   <list type="bullet">
  ///     <item><description>If the cache already contains the data record with the same keys and the NotChanged status, the method returns this data record updated to the state of
  ///     PXDataRecord.</description></item>
  ///     <item><description>If the cache contains the same data record with the Updated or Inserted status, the method returns this data record.</description></item>
  ///   </list>
  ///   <para>In other cases and when isReadonly is true, the method returns the data record created from the PXDataRecord object.</para>
  ///   <para>If the AllowSelect property is false, the methods returns a new empty data record and the logic described above is not executed.</para>
  ///   <para>The method raises the RowSelecting event.</para>
  /// </remarks>
  public abstract object Select(
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated);

  internal abstract object CreateItem(PXDataRecord record, ref int position, bool isReadOnly);

  internal abstract object Select(
    PXDataRecord record,
    object row,
    bool isReadOnly,
    out bool wasUpdated);

  /// <summary>Initializes a new data record with the field values from the provided data record.</summary>
  /// <param name="item">The data record to copy.</param>
  /// <returns></returns>
  /// <example><para>The code below creates a copy of the Current data record of a data view.</para>
  ///   <code title="Example" lang="CS">
  /// public PXSelect&lt;APInvoice, ... &gt; Document;
  /// ...
  /// APInvoice newdoc = PXCache&lt;APInvoice&gt;.CreateCopy(Document.Current);</code>
  /// </example>
  public abstract object CreateCopy(object item);

  /// <summary>Copy field values from copy to item.</summary>
  /// <param name="item">IBqlTable</param>
  /// <param name="copy">IBqlTable</param>
  public abstract void RestoreCopy(object item, object copy);

  /// <summary>
  /// Loads data rows and other cache state objects from the session.
  /// </summary>
  public abstract void Load();

  internal abstract void LoadFromSession(bool force = false);

  internal abstract void ResetToUnmodified(SessionRollback.FakeState fakeState);

  internal abstract void SaveUnmodifiedState();

  internal abstract void ResetToInitialState();

  internal abstract void CreateNewVersion();

  internal abstract void SaveCurrentVersion();

  internal abstract void DiscardCurrentVersion();

  internal abstract void SaveInsertState();

  /// <summary>
  /// Saves dirty data rows and other cache state objects into the session.
  /// </summary>
  public abstract void Unload();

  /// <summary>Clears all information stored in the session previously.</summary>
  public abstract void Clear();

  /// <exclude />
  public abstract void ClearItemAttributes();

  /// <exclude />
  public abstract void TrimItemAttributes(object item);

  /// <summary>Clears all cached query result for a given table.</summary>
  public abstract void ClearQueryCacheObsolete();

  public abstract void ClearQueryCache();

  /// <summary>Gets the collection of updated, inserted, and deleted data records. The collection contains data records with the Updated, Inserted, or Deleted status.</summary>
  public abstract IEnumerable Dirty { get; }

  /// <summary>Gets the collection of deleted data records that exist in the database. The collection contains data records with the Deleted status.</summary>
  public abstract IEnumerable Deleted { get; }

  /// <summary>Gets the collection of updated data records that exist in the database. The collection contains data records with the Updated status.</summary>
  public abstract IEnumerable Updated { get; }

  /// <summary>Gets the collection of inserted data records that does not exist in the database. The collection contains data records with the Inserted status.</summary>
  public abstract IEnumerable Inserted { get; }

  /// <summary>Get the collection of all cached data records. The collection contains data records with any status. The developer should not rely on the presence of data
  /// records with statuses other than Updated, Inserted, and Deleted in this collection.</summary>
  /// <remarks>The collection contains data records with any status. The developer should not rely on the presence of data records with statuses other than Updated, Inserted,
  /// and Deleted in this collection.</remarks>
  public abstract IEnumerable Cached { get; }

  internal abstract int Version { get; set; }

  internal abstract BqlTablePair GetOriginalObjectContext(object item, bool readItemIfNotExists = false);

  internal Tuple<string, int?, int?, string> _GetOriginalCounts(object item)
  {
    BqlTablePair bqlTablePair;
    return item is IBqlTable && this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair) ? new Tuple<string, int?, int?, string>(bqlTablePair.NoteText, bqlTablePair.FilesCount, bqlTablePair.ActivitiesCount, bqlTablePair.NotePopupText) : new Tuple<string, int?, int?, string>((string) null, new int?(), new int?(), (string) null);
  }

  public virtual void SetSlot<TSlot>(object item, int idx, TSlot slot, bool isOriginal = false)
  {
    if (!(item is IBqlTable))
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
      this._Originals[(IBqlTable) item] = bqlTablePair = new BqlTablePair();
    if (bqlTablePair.Slots == null)
    {
      bqlTablePair.Slots = new List<object>(this._SlotDelegates.Count);
      bqlTablePair.SlotsOriginal = new List<object>(this._SlotDelegates.Count);
    }
    for (int count = bqlTablePair.Slots.Count; count <= idx; ++count)
    {
      bqlTablePair.Slots.Add((object) null);
      bqlTablePair.SlotsOriginal.Add((object) null);
    }
    bqlTablePair.Slots[idx] = (object) slot;
    if (!isOriginal || idx >= this._SlotDelegates.Count)
      return;
    bqlTablePair.SlotsOriginal[idx] = ((Func<object, object>) this._SlotDelegates[idx].Item3)((object) slot);
  }

  public virtual TSlot GetSlot<TSlot>(object item, int idx, bool isOriginal = false)
  {
    BqlTablePair bqlTablePair;
    if (item is IBqlTable && this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
    {
      if (!isOriginal)
      {
        if (bqlTablePair.Slots != null && idx < bqlTablePair.Slots.Count)
          return (TSlot) bqlTablePair.Slots[idx];
      }
      else if (bqlTablePair.SlotsOriginal != null && idx < bqlTablePair.SlotsOriginal.Count)
        return (TSlot) bqlTablePair.SlotsOriginal[idx];
    }
    return default (TSlot);
  }

  internal void _SetOriginalCounts(
    object item,
    string noteText,
    int? filesCount,
    int? activitiesCount,
    string notePopupText)
  {
    if (!(item is IBqlTable))
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
      this._Originals[(IBqlTable) item] = bqlTablePair = new BqlTablePair();
    if (noteText != null)
      bqlTablePair.NoteText = noteText;
    if (filesCount.HasValue)
      bqlTablePair.FilesCount = filesCount;
    if (activitiesCount.HasValue)
      bqlTablePair.ActivitiesCount = activitiesCount;
    if (notePopupText == null)
      return;
    bqlTablePair.NotePopupText = notePopupText;
  }

  internal void _ResetOriginalCounts(
    object item,
    bool resetText,
    bool resetFiles,
    bool resetActivities)
  {
    if (!(item is IBqlTable))
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
      this._Originals[(IBqlTable) item] = bqlTablePair = new BqlTablePair();
    if (resetText)
    {
      bqlTablePair.NoteText = (string) null;
      bqlTablePair.NotePopupText = (string) null;
    }
    if (resetFiles)
      bqlTablePair.FilesCount = new int?();
    if (!resetActivities)
      return;
    bqlTablePair.ActivitiesCount = new int?();
  }

  /// <summary>
  /// Indicates whether a given <paramref name="item" /> is archived or not.
  /// </summary>
  /// <param name="item">The entity to test against archivation.</param>
  /// <returns><c>true</c> if the <paramref name="item" /> is archived, <c>false</c> if not.</returns>
  public bool IsArchived(object item)
  {
    if (!(item is IBqlTable))
      return false;
    BqlTablePair bqlTablePair = (BqlTablePair) null;
    try
    {
      bqlTablePair = this.GetOriginalObjectContext(item, true);
    }
    catch
    {
    }
    if (bqlTablePair == null)
      return false;
    bool? isArchived = bqlTablePair.IsArchived;
    bool flag = true;
    return isArchived.GetValueOrDefault() == flag & isArchived.HasValue;
  }

  internal void SetArchived(object item, bool value)
  {
    if (!(item is IBqlTable))
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
      this._Originals[(IBqlTable) item] = bqlTablePair = new BqlTablePair();
    bqlTablePair.IsArchived = new bool?(value);
  }

  internal bool IsDeletedRecord(object item)
  {
    if (!(item is IBqlTable))
      return false;
    BqlTablePair bqlTablePair = (BqlTablePair) null;
    try
    {
      bqlTablePair = this.GetOriginalObjectContext(item, true);
    }
    catch
    {
    }
    if (bqlTablePair == null)
      return false;
    bool? isDeletedRecord = bqlTablePair.IsDeletedRecord;
    bool flag = true;
    return isDeletedRecord.GetValueOrDefault() == flag & isDeletedRecord.HasValue;
  }

  internal void SetDeletedRecord(object item, bool value)
  {
    if (!(item is IBqlTable))
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
      this._Originals[(IBqlTable) item] = bqlTablePair = new BqlTablePair();
    bqlTablePair.IsDeletedRecord = new bool?(value);
  }

  /// <summary>
  /// Saves changed data rows to the database.<br />
  /// Raise events OnRowPersisting, OnCommandPreparing, OnRowPersisted, OnExceptionHandling
  /// </summary>
  /// <returns>The first item saved.</returns>
  public abstract int Persist(PXDBOperation operation);

  /// <summary>
  /// executes PersistUpdated or PersistInserted or PersistDeleted depends on operation
  /// </summary>
  /// <param name="row"></param>
  /// <param name="operation"></param>
  public abstract void Persist(object row, PXDBOperation operation);

  /// <summary>Saves the updated data record to the database.</summary>
  /// <remarks>
  ///   <para>The method raises the OnRowPersisting, OnCommandPreparing, OnRowPersisted, OnExceptionHandling events.</para>
  ///   <para>The default behavior can be modified by PXDBInterceptorAttribute.</para>
  /// </remarks>
  public virtual bool PersistUpdated(object row) => this.PersistUpdated(row, false);

  protected internal abstract bool PersistUpdated(object row, bool bypassInterceptor);

  /// <summary>Inserts the provided data record into the database.<br /></summary>
  /// <remarks>
  ///   <para>The exception is thrown if a row with such keys exists in the database.</para>
  ///   <para>The method raises the OnRowPersisting, OnCommandPreparing, OnRowPersisted, OnExceptionHandling events.</para>
  ///   <para>The default behavior can be modified by PXDBInterceptorAttribute.</para>
  /// </remarks>
  public virtual bool PersistInserted(object row) => this.PersistInserted(row, false);

  protected internal abstract bool PersistInserted(object row, bool bypassInterceptor);

  /// <summary>Deletes the provided data record from the database by the key fields</summary>
  /// <remarks>The method raises the OnRowPersisting, OnCommandPreparing, OnRowPersisted, OnExceptionHandling events.<br />
  /// The default behavior can be modified by PXDBInterceptorAttribute.</remarks>
  public virtual bool PersistDeleted(object row) => this.PersistDeleted(row, false);

  protected internal abstract bool PersistDeleted(object row, bool bypassInterceptor);

  /// <summary>
  /// For each persisted row - raise OnRowPersisted, SetStatus(Notchanged)
  /// </summary>
  public abstract void Persisted(bool isAborted);

  /// <summary>
  /// For each persisted row - raise OnRowPersisted, SetStatus(Notchanged)
  /// </summary>
  protected internal abstract void Persisted(bool isAborted, Exception exception);

  /// <summary>remove row from list of persited items</summary>
  /// <param name="row"></param>
  public abstract void ResetPersisted(object row);

  protected internal abstract void ResetState();

  /// <summary>Gets or sets the value that indicates whether the cache contains the modified data records.</summary>
  public virtual bool IsDirty
  {
    get => !this.AutoSave && this._IsDirty;
    set => this._IsDirty = value;
  }

  /// <summary>Gets the value that indicates if the cache contains modified data records to be saved to database.</summary>
  public abstract bool IsInsertedUpdatedDeleted { get; }

  /// <summary>
  /// Gets or sets the user-friendly name set by the
  /// <see cref="T:PX.Data.PXCacheNameAttribute">PXCacheName</see> attribute.
  /// </summary>
  public string DisplayName { get; set; }

  internal abstract void ClearSession();

  internal virtual bool HasChangedKeys() => false;

  /// <exclude />
  public virtual bool IsKeysFilled(object item) => this.isKeysFilled(item);

  internal bool isUnexistingKey(string field, object value)
  {
    return this.Keys.variableLengthStrings.Contains(field) && value is string str && str.StartsWith(" <") || string.Equals(this.Identity, field, StringComparison.OrdinalIgnoreCase) && (value is int num1 && num1 < 0 || value is long num2 && num2 < 0L);
  }

  internal bool hasUnexistingKey(object data)
  {
    if (this.Identity != null && this.isUnexistingKey(this.Identity, this.GetValue(data, this.Identity)))
      return true;
    foreach (string key in (IEnumerable<string>) this.Keys)
    {
      if (this.isUnexistingKey(key, this.GetValue(data, key)))
        return true;
    }
    return false;
  }

  internal static bool IsOrigValueNewDate(
    PXCache sender,
    PXCommandPreparingEventArgs.FieldDescription origdescription)
  {
    if (origdescription.DataType != PXDbType.DirectExpression)
      return false;
    return object.Equals(origdescription.DataValue, (object) sender.Graph.SqlDialect.GetDate) || object.Equals(origdescription.DataValue, (object) sender.Graph.SqlDialect.GetUtcDate);
  }

  [Obsolete("This method has been renamed to IsKeysFilled")]
  internal bool isKeysFilled(object item)
  {
    foreach (string key in (IEnumerable<string>) this._Keys)
    {
      if (this.GetValue(item, key) == null)
        return false;
    }
    return true;
  }

  internal abstract int LocateByNoteID(Guid noteId, string noteIdField);

  public abstract int LocateByNoteID(Guid noteId);

  protected static (List<PXCache.fieldContainer> list, PXCache.fieldContainer tree) getAllNestedTypes(
    System.Type root)
  {
    List<PXCache.fieldContainer> fieldContainerList1 = new List<PXCache.fieldContainer>();
    List<PXCache.fieldContainer> fieldContainerList2 = new List<PXCache.fieldContainer>();
    PXCache.fieldContainer fieldContainer1 = new PXCache.fieldContainer()
    {
      Field = root
    };
    fieldContainerList2.Add(fieldContainer1);
    foreach (System.Type nestedType in root.GetNestedTypes())
    {
      PXCache.fieldContainer fieldContainer2 = new PXCache.fieldContainer()
      {
        Field = nestedType,
        Children = new PXCache.fieldContainer[0]
      };
      fieldContainerList2.Add(fieldContainer2);
      fieldContainerList1.Add(fieldContainer2);
    }
    fieldContainer1.Children = fieldContainerList1.ToArray();
    return (fieldContainerList2, fieldContainer1);
  }

  protected internal static PXCache.DACFieldDescriptor[] _GetProperties(
    System.Type type,
    List<System.Type> extensions,
    out Dictionary<string, List<PropertyInfo>> nameToFieldPropertyInfo,
    out int origLength)
  {
    PXCache.DACFieldDescriptor[] array1 = ((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Public)).OrderBy<PropertyInfo, MemberInfo>((Func<PropertyInfo, MemberInfo>) (prop => (MemberInfo) prop), (IComparer<MemberInfo>) new PXGraph.FieldInfoComparer()).Select<PropertyInfo, PXCache.DACFieldDescriptor>((Func<PropertyInfo, PXCache.DACFieldDescriptor>) (prop => new PXCache.DACFieldDescriptor()
    {
      Property = prop
    })).ToArray<PXCache.DACFieldDescriptor>();
    PXCache.DACFieldDescriptor[] array2 = new PXCache.DACFieldDescriptor[array1.Length];
    int sourceIndex;
    for (int destinationIndex = 0; destinationIndex < array2.Length; destinationIndex = array1.Length - sourceIndex)
    {
      sourceIndex = array1.Length - destinationIndex - 1;
      while (sourceIndex > 0 && array1[array1.Length - destinationIndex - 1].Property.DeclaringType == array1[sourceIndex - 1].Property.DeclaringType)
        --sourceIndex;
      Array.Copy((Array) array1, sourceIndex, (Array) array2, destinationIndex, array1.Length - destinationIndex - sourceIndex);
    }
    Dictionary<string, int> nametoposition = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    EnumerableExtensions.ForEach<IndexedValue<PXCache.DACFieldDescriptor>>(((IEnumerable<PXCache.DACFieldDescriptor>) array2).EnumWithIndex<PXCache.DACFieldDescriptor>(), (System.Action<IndexedValue<PXCache.DACFieldDescriptor>>) (_ => nametoposition[_.Value.Property.Name] = _.Index));
    nameToFieldPropertyInfo = (Dictionary<string, List<PropertyInfo>>) null;
    origLength = array2.Length;
    if (extensions.Count > 0)
    {
      HashSet<string> stringSet1 = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      List<PXCache.DACFieldDescriptor> source = new List<PXCache.DACFieldDescriptor>();
      foreach (PXCache.DACFieldDescriptor dacFieldDescriptor in array2)
      {
        if (dacFieldDescriptor.Property != (PropertyInfo) null)
          stringSet1.Add(dacFieldDescriptor.Property.Name);
      }
      nameToFieldPropertyInfo = new Dictionary<string, List<PropertyInfo>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      Dictionary<string, List<PXCache.DACFieldDescriptor>> dictionary = new Dictionary<string, List<PXCache.DACFieldDescriptor>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      List<PXCache.DACFieldDescriptor> dacFieldDescriptorList1 = new List<PXCache.DACFieldDescriptor>();
      foreach (System.Type extension in extensions)
      {
        foreach (PropertyInfo propertyInfo in (IEnumerable<PropertyInfo>) ((IEnumerable<PropertyInfo>) extension.GetProperties(BindingFlags.Instance | BindingFlags.Public)).OrderBy<PropertyInfo, MemberInfo>((Func<PropertyInfo, MemberInfo>) (p => (MemberInfo) p), (IComparer<MemberInfo>) new PXGraph.FieldInfoComparer()))
        {
          List<PropertyInfo> propertyInfoList;
          if (!nameToFieldPropertyInfo.TryGetValue(propertyInfo.Name, out propertyInfoList))
            nameToFieldPropertyInfo[propertyInfo.Name] = propertyInfoList = new List<PropertyInfo>();
          propertyInfoList.Insert(0, propertyInfo);
        }
      }
      foreach (IBqlMapping bqlMapping in PXCache._mapping.GetAllMaps().Where<IBqlMapping>((Func<IBqlMapping, bool>) (m => m.Table.IsAssignableFrom(type) && extensions.Contains(m.Extension))).OrderBy<IBqlMapping, string>((Func<IBqlMapping, string>) (_ => _.Extension.FullName)).ToList<IBqlMapping>())
      {
        foreach (System.Reflection.FieldInfo field in bqlMapping.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
          string name = ((System.Type) field.GetValue((object) bqlMapping)).Name;
          List<PropertyInfo> propertyInfoList1;
          if (field.FieldType == typeof (System.Type) && !string.Equals(name, field.Name, StringComparison.OrdinalIgnoreCase) && nameToFieldPropertyInfo.TryGetValue(field.Name, out propertyInfoList1))
          {
            PropertyInfo property = bqlMapping.Extension.GetProperty(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (property == (PropertyInfo) null)
              throw new PXException("Can't find mapping {0} field in extension {1}", new object[2]
              {
                (object) field.Name,
                (object) bqlMapping.Extension.FullName
              });
            List<PropertyInfo> propertyInfoList2;
            nameToFieldPropertyInfo.TryGetValue(name, out propertyInfoList2);
            if (!propertyInfoList1.Contains(property))
            {
              if (propertyInfoList2 == null || !propertyInfoList2.Contains(property))
                throw new PXException("Can't find mapping {0} field in extension {1}", new object[2]
                {
                  (object) field.Name,
                  (object) bqlMapping.Extension.FullName
                });
            }
            else
            {
              propertyInfoList1.Remove(property);
              if (propertyInfoList1.Count == 0)
                nameToFieldPropertyInfo.Remove(field.Name);
              if (propertyInfoList2 != null)
                propertyInfoList2.Add(property);
              else
                nameToFieldPropertyInfo[name] = new List<PropertyInfo>()
                {
                  property
                };
            }
          }
        }
      }
      foreach (List<PropertyInfo> propertyInfoList in nameToFieldPropertyInfo.Values)
      {
        List<PropertyInfo> extpi = propertyInfoList;
        if (!stringSet1.Contains(extpi[0].Name) && !source.Any<PXCache.DACFieldDescriptor>((Func<PXCache.DACFieldDescriptor, bool>) (_ => _.Name == extpi[0].Name)))
          source.Add(new PXCache.DACFieldDescriptor()
          {
            Property = extpi[0]
          });
      }
      if (source.Count > 0)
      {
        Array.Resize<PXCache.DACFieldDescriptor>(ref array2, array2.Length + source.Count);
        Array.Copy((Array) source.ToArray(), 0, (Array) array2, array2.Length - source.Count, source.Count);
      }
      EnumerableExtensions.ForEach<IndexedValue<PXCache.DACFieldDescriptor>>(((IEnumerable<PXCache.DACFieldDescriptor>) array2).EnumWithIndex<PXCache.DACFieldDescriptor>(), (System.Action<IndexedValue<PXCache.DACFieldDescriptor>>) (x => nametoposition[x.Value.Name] = x.Index));
      List<PXCache.DACFieldDescriptor> dacFieldDescriptorList2 = new List<PXCache.DACFieldDescriptor>();
      HashSet<string> stringSet2 = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (PXCache.DACFieldDescriptor dacFieldDescriptor in dacFieldDescriptorList1)
      {
        if (!stringSet2.Contains(dacFieldDescriptor.Name))
        {
          stringSet2.Add(dacFieldDescriptor.Name);
          int index;
          if (nametoposition.TryGetValue(dacFieldDescriptor.Name, out index))
          {
            if (array2[index].BqlField == (System.Type) null)
            {
              array2[index].BqlField = dacFieldDescriptor.BqlField;
              array2[index].TargetBqlField = dacFieldDescriptor.TargetBqlField;
              array2[index].IsSameView = dacFieldDescriptor.IsSameView;
              array2[index].ViewName = dacFieldDescriptor.ViewName;
            }
          }
          else
            dacFieldDescriptorList2.Add(dacFieldDescriptor);
        }
      }
      if (dacFieldDescriptorList2.Count > 0)
      {
        Array.Resize<PXCache.DACFieldDescriptor>(ref array2, array2.Length + dacFieldDescriptorList2.Count);
        Array.Copy((Array) dacFieldDescriptorList2.ToArray(), 0, (Array) array2, array2.Length - dacFieldDescriptorList2.Count, dacFieldDescriptorList2.Count);
      }
    }
    return array2;
  }

  /// <remarks>
  /// See <a href="https://wiki.acumatica.com/display/TND/Primary+and+Foreign+Key+API">Foreign Key API</a> for more details.
  /// </remarks>
  private protected static void CollectForeignKeys(System.Type type, IEnumerable<System.Type> extensions)
  {
    for (; type != (System.Type) null && type != typeof (object); type = type.BaseType)
      CollectForeignKeysImpl(type);
    foreach (System.Type extension in extensions)
      CollectForeignKeysImpl(extension);

    static void CollectForeignKeysImpl(System.Type t)
    {
      foreach (System.Type nestedType in t.GetNestedTypes())
      {
        if (nestedType.IsAbstract && nestedType.IsSealed)
          CollectForeignKeysImpl(nestedType);
        for (System.Type type = nestedType; type != (System.Type) null && type != typeof (object); type = type.BaseType)
        {
          if (type.IsGenericType && typeof (KeysRelation<,,>).IsAssignableFrom(type.GetGenericTypeDefinition()))
          {
            MethodInfo method = type.GetMethod("CollectReference", BindingFlags.Static | BindingFlags.NonPublic);
            if ((object) method != null)
            {
              // ISSUE: explicit non-virtual call
              __nonvirtual (method.Invoke((object) null, Array<object>.Empty));
              break;
            }
            break;
          }
        }
      }
    }
  }

  protected internal PXCache.EventDictionary<PXCommandPreparing> CommandPreparingEvents
  {
    get
    {
      return this._CommandPreparingEvents ?? (this._CommandPreparingEvents = new PXCache.EventDictionary<PXCommandPreparing>());
    }
  }

  internal Dictionary<string, IPXCommandPreparingSubscriber[]> CommandPreparingEventsAttr
  {
    get => this._CommandPreparingEventsAttr;
  }

  protected virtual bool OnCommandPreparing(
    string name,
    object row,
    object value,
    PXDBOperation operation,
    System.Type table,
    out PXCommandPreparingEventArgs.FieldDescription description)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    description = (PXCommandPreparingEventArgs.FieldDescription) null;
    bool flag1 = true;
    bool flag2 = operation.Command() == PXDBOperation.Select && (this.IsKvExtField(name) || this.IsKvExtAttribute(name));
    PXCommandPreparing commandPreparing1;
    PXCommandPreparing commandPreparing2 = this._CommandPreparingEvents == null || !this._CommandPreparingEvents.TryGetValue(name, out commandPreparing1) ? (PXCommandPreparing) null : commandPreparing1;
    IPXCommandPreparingSubscriber[] attributeHandlers;
    Action<PXCache, PXCommandPreparingEventArgs> action = this._CommandPreparingEventsAttr == null || !this._CommandPreparingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXCommandPreparingEventArgs>) null : this.SquashAttributeHandlers<IPXCommandPreparingSubscriber>(attributeHandlers).To<PXCommandPreparingEventArgs>((Action<IPXCommandPreparingSubscriber, PXCache, PXCommandPreparingEventArgs>) ((h, c, a) => h.CommandPreparing(c, a)), row);
    if (commandPreparing2 != null || action != null)
    {
      PXCommandPreparingEventArgs e = new PXCommandPreparingEventArgs(row, value, operation, table, this._Graph.SqlDialect);
      if (commandPreparing2 != null)
        commandPreparing2(this, e);
      SQLExpression expr = e.Expr;
      bool flag3 = e.Cancel & flag2 && !e.DataLength.HasValue;
      if (!e.Cancel | flag3)
      {
        try
        {
          if (action != null)
            action(this, e);
        }
        catch (Exception ex) when (flag3)
        {
        }
      }
      if (flag3)
        e.Expr = expr;
      description = e.GetFieldDescription();
      flag1 = !e.Cancel;
    }
    if (!(flag1 & flag2))
      return flag1;
    if (description?.Expr == null)
      this.OnCommandPreparing(this._NoteIDName, row, (object) null, operation, table, out description);
    return this.prepareKvExtField(name, value, table, operation, ref description);
  }

  protected bool prepareKvExtField(
    string name,
    object value,
    System.Type table,
    PXDBOperation operation,
    ref PXCommandPreparingEventArgs.FieldDescription description)
  {
    if (((operation.Option() & PXDBOperation.External) == PXDBOperation.External || (operation & PXDBOperation.WhereClause) == PXDBOperation.WhereClause || (operation & PXDBOperation.OrderByClause) == PXDBOperation.OrderByClause) && description != null)
    {
      PXDataFieldAssign assign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue);
      this._AdjustStorage(name, (PXDataFieldParam) assign);
      string name1 = (string) null;
      PXDbType type1 = description.DataType;
      switch (assign.Storage)
      {
        case StorageBehavior.KeyValueNumeric:
          name1 = "ValueNumeric";
          type1 = PXDbType.Decimal;
          break;
        case StorageBehavior.KeyValueDate:
          name1 = "ValueDate";
          type1 = PXDbType.DateTime;
          break;
        case StorageBehavior.KeyValueString:
          name1 = "ValueString";
          type1 = PXDbType.VarChar;
          break;
        case StorageBehavior.KeyValueText:
          name1 = "ValueText";
          type1 = PXDbType.VarChar;
          break;
      }
      string extTable = this.Graph.SqlDialect.GetKvExtTableName(BqlCommand.GetTableName(description.BqlTable));
      System.Type type2 = table;
      if ((object) type2 == null)
        type2 = description.BqlTable;
      System.Type type3 = type2;
      TableChangingScope.AddUnchangedRealName(extTable);
      Table sqlTable = TableChangingScope.GetSQLTable((Func<Table>) (() => (Table) new SimpleTable(extTable)), extTable);
      Query q = new Query();
      q.Field((SQLExpression) new Column(name1, sqlTable, type1)).From(sqlTable).Where(SQLExpressionExt.EQ(new Column("RecordID", sqlTable, PXDbType.UniqueIdentifier), (SQLExpression) new Column("NoteID", (Table) new SimpleTable(type3), PXDbType.UniqueIdentifier)).And(SQLExpressionExt.EQ(new Column("FieldName", sqlTable, PXDbType.VarChar), (SQLExpression) new SQLConst((object) name))));
      SQLExpression where = q.GetWhere();
      TableChangingScope.AppendRestrictionsOnIsNew(ref where, this.Graph, new TableChangingScope.DacTable(type3), new TableChangingScope.DacTable(extTable), type3 == description.BqlTable);
      q.Where(where);
      description.Expr = (SQLExpression) new SubQuery(q);
      if ((operation & PXDBOperation.WhereClause) == PXDBOperation.WhereClause && type1 == PXDbType.Decimal)
        description.Expr = description.Expr.Coalesce((SQLExpression) new SQLConst((object) 0));
      if (description.DataValue == null && description.DataType == PXDbType.UniqueIdentifier)
      {
        description.DataType = type1;
        description.DataValue = value;
        description.DataLength = new int?();
      }
    }
    else if (this._FirstKeyValueStored.HasValue && string.Equals(this._FirstKeyValueStored.Value.Key, name, StringComparison.OrdinalIgnoreCase) || !this._FirstKeyValueStored.HasValue && this._FirstKeyValueAttribute.HasValue && string.Equals(this._FirstKeyValueAttribute.Value.Key, name, StringComparison.InvariantCultureIgnoreCase) || !this._FirstKeyValueStored.HasValue && this._ForcedKeyValueAttributes.Contains(name))
    {
      System.Type type = (System.Type) null;
      System.Type b = description.BqlTable;
      if (description.BqlTable.IsAssignableFrom(this.BqlTable) || this.BqlSelect != null && (type = ((IEnumerable<System.Type>) this.BqlSelect.GetTables()).FirstOrDefault<System.Type>((Func<System.Type, bool>) (_ => b.IsAssignableFrom(PXCache.GetBqlTable(_))))) != (System.Type) null)
      {
        if ((operation & PXDBOperation.Place) == PXDBOperation.GroupByClause && this.BqlSelect == null)
        {
          description.Expr = SQLExpression.Null();
        }
        else
        {
          Query attributesJoined = BqlCommand.GetNoteAttributesJoined((System.Type) null, description?.BqlTable, table, operation);
          description = new PXCommandPreparingEventArgs.FieldDescription(type != (System.Type) null ? this.BqlTable : description.BqlTable, (SQLExpression) new SubQuery(attributesJoined), PXDbType.NVarChar, new int?(-1), (object) null, false);
        }
      }
    }
    else
      description = (PXCommandPreparingEventArgs.FieldDescription) null;
    return true;
  }

  /// <summary>
  /// Raises the CommandPreparing
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="value">The current field value.</param>
  /// <param name="operation">The current database operation.</param>
  /// <param name="table">The type of DAC objects placed in the cache.</param>
  /// <param name="description">The object containing the description of the current field.</param>
  public virtual bool RaiseCommandPreparing(
    string name,
    object row,
    object value,
    PXDBOperation operation,
    System.Type table,
    out PXCommandPreparingEventArgs.FieldDescription description)
  {
    return this.OnCommandPreparing(name, row, value, operation, table, out description);
  }

  /// <summary>
  /// Raises the CommandPreparing
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="value">The current field value.</param>
  /// <param name="operation">The current database operation.</param>
  /// <param name="table">The type of DAC objects placed in the cache.</param>
  /// <param name="description">The object containing the description of the current field.</param>
  public bool RaiseCommandPreparing<Field>(
    object row,
    object value,
    PXDBOperation operation,
    System.Type table,
    out PXCommandPreparingEventArgs.FieldDescription description)
    where Field : IBqlField
  {
    return this.OnCommandPreparing(typeof (Field).Name, row, value, operation, table, out description);
  }

  /// <summary>Allows you to work with the main data reader from a manually added event handler.</summary>
  /// <remarks>While querying database, Acumatica Framework executes RowSelecting events for every retrieved record.
  /// Graph-level RowSelecting events are executed after the iteration through rows of the main data reader
  /// has completed. You can manually add a RowSelectingWhileReading event handler to execute
  /// graph-level RowSelecting events in a separate connection scope while the main data reader is open.</remarks>
  internal event PXRowSelecting RowSelectingWhileReading
  {
    add
    {
      if (this._EventsRow._RowSelectingWhileReadingList != null)
        this._EventsRow._RowSelectingWhileReadingList.Add(value);
      else
        this._EventsRow.RowSelectingWhileReading += value;
    }
    remove
    {
      if (this._EventsRow._RowSelectingWhileReadingList != null)
        this._EventsRow._RowSelectingWhileReadingList.Remove(value);
      else
        this._EventsRow.RowSelectingWhileReading = (PXRowSelecting) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowSelectingWhileReading, (Delegate) value);
    }
  }

  protected internal event PXRowSelecting RowSelecting
  {
    add
    {
      if (this._EventsRow._RowSelectingList != null)
        this._EventsRow._RowSelectingList.Add(value);
      else
        this._EventsRow.RowSelecting += value;
    }
    remove
    {
      if (this._EventsRow._RowSelectingList != null)
        this._EventsRow._RowSelectingList.Remove(value);
      else
        this._EventsRow.RowSelecting = (PXRowSelecting) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowSelecting, (Delegate) value);
    }
  }

  protected internal bool OnRowSelecting(
    object item,
    PXDataRecord record,
    ref int position,
    bool isReadOnly)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmRowSelecting)))
    {
      Action<PXCache, PXRowSelectingEventArgs> rowSelecting = this.SquashAttributeHandlers(this._EventsRowAttr.RowSelecting).ToRowSelecting(item, record, isReadOnly);
      if (this._EventsRow.RowSelecting == null && this._EventsRow.RowSelectingWhileReading == null && rowSelecting == null)
        return true;
      PXRowSelectingEventArgs e = new PXRowSelectingEventArgs(item, record, position, isReadOnly);
      if (rowSelecting != null)
        rowSelecting(this, e);
      PXRowSelecting selectingWhileReading = this._EventsRow.RowSelectingWhileReading;
      if (selectingWhileReading != null)
        selectingWhileReading(this, e);
      if (this._EventsRow.RowSelecting != null)
      {
        if (PXStreamingQueryScope.IsScoped || OptimizedExportScope.IsScoped)
        {
          using (new PXConnectionScope())
            this._EventsRow.RowSelecting(this, e);
        }
        else if (record != null && e.Row != null)
          record.AddRowSelecting((System.Action) (() => this._EventsRow.RowSelecting(this, new PXRowSelectingEventArgs(item, (PXDataRecord) null, 0, isReadOnly))));
      }
      position = e.Position;
      return !e.Cancel;
    }
  }

  /// <summary>
  /// Raises the RowSelecting event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  /// <param name="record">The object wrapping the result set row.</param>
  /// <param name="position">The current index in the list of <tt>PXDataRecord</tt>
  /// columns.</param>
  /// <param name="isReadOnly">The value indicating if the data record is read-only.</param>
  public virtual bool RaiseRowSelecting(
    object item,
    PXDataRecord record,
    ref int position,
    bool isReadOnly)
  {
    return this.OnRowSelecting(item, record, ref position, isReadOnly);
  }

  protected internal event PXRowSelected RowSelected
  {
    add
    {
      if (this._EventsRow._RowSelectedList != null)
        this._EventsRow._RowSelectedList.Add(value);
      else
        this._EventsRow.RowSelected += value;
    }
    remove
    {
      if (this._EventsRow._RowSelectedList != null)
        this._EventsRow._RowSelectedList.Remove(value);
      else
        this._EventsRow.RowSelected = (PXRowSelected) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowSelected, (Delegate) value);
    }
  }

  protected virtual void OnRowSelected(object item)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmRowSelected)))
    {
      Func<PXRowSelected> func = (Func<PXRowSelected>) (() => this._EventsRow.RowSelected);
      Action<PXCache, PXRowSelectedEventArgs> action = this.SquashAttributeHandlers<IPXRowSelectedSubscriber>(this._EventsRowAttr.RowSelected).To<PXRowSelectedEventArgs>((Action<IPXRowSelectedSubscriber, PXCache, PXRowSelectedEventArgs>) ((h, c, a) => h.RowSelected(c, a)), item);
      if (func() == null && action == null)
        return;
      PXRowSelectedEventArgs e = new PXRowSelectedEventArgs(item);
      if (action != null)
        action(this, e);
      PXRowSelected pxRowSelected = func();
      if (pxRowSelected == null)
        return;
      pxRowSelected(this, e);
    }
  }

  /// <summary>
  /// Raises the RowSelected event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  public virtual void RaiseRowSelected(object item) => this.OnRowSelected(item);

  protected internal event PXRowInserting RowInserting
  {
    add
    {
      if (this._EventsRow._RowInsertingList != null)
        this._EventsRow._RowInsertingList.Insert(0, value);
      else
        this._EventsRow.RowInserting = value + this._EventsRow.RowInserting;
    }
    remove
    {
      if (this._EventsRow._RowInsertingList != null)
        this._EventsRow._RowInsertingList.Remove(value);
      else
        this._EventsRow.RowInserting = (PXRowInserting) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowInserting, (Delegate) value);
    }
  }

  protected bool OnRowInserting(object item, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    PXRowInserting rowInserting = this._EventsRow.RowInserting;
    Action<PXCache, PXRowInsertingEventArgs> action = this.SquashAttributeHandlers<IPXRowInsertingSubscriber>(this._EventsRowAttr.RowInserting).To<PXRowInsertingEventArgs>((Action<IPXRowInsertingSubscriber, PXCache, PXRowInsertingEventArgs>) ((h, c, a) => h.RowInserting(c, a)), item);
    if (rowInserting == null && action == null)
      return true;
    PXRowInsertingEventArgs e = new PXRowInsertingEventArgs(item, externalCall);
    if (rowInserting != null)
      rowInserting(this, e);
    if (!e.Cancel && action != null)
      action(this, e);
    return !e.Cancel;
  }

  /// <summary>
  /// Raises the RowInserting event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  public bool RaiseRowInserting(object item) => this.OnRowInserting(item, false);

  protected internal event PXRowInserted RowInserted
  {
    add
    {
      if (this._EventsRow._RowInsertedList != null)
        this._EventsRow._RowInsertedList.Add(value);
      else
        this._EventsRow.RowInserted += value;
    }
    remove
    {
      if (this._EventsRow._RowInsertedList != null)
        this._EventsRow._RowInsertedList.Remove(value);
      else
        this._EventsRow.RowInserted = (PXRowInserted) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowInserted, (Delegate) value);
    }
  }

  protected void OnRowInserted(object item, bool externalCall)
  {
    this.OnRowInserted(item, item, externalCall);
  }

  protected void OnRowInserted(object item, object pending, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    Func<PXRowInserted> func = (Func<PXRowInserted>) (() => this._EventsRow.RowInserted);
    Action<PXCache, PXRowInsertedEventArgs> action = this.SquashAttributeHandlers<IPXRowInsertedSubscriber>(this._EventsRowAttr.RowInserted).To<PXRowInsertedEventArgs>((Action<IPXRowInsertedSubscriber, PXCache, PXRowInsertedEventArgs>) ((h, c, a) => h.RowInserted(c, a)), item);
    if (func() != null || action != null)
    {
      PXRowInsertedEventArgs e = new PXRowInsertedEventArgs(item, externalCall)
      {
        PendingRow = pending
      };
      if (action != null)
        action(this, e);
      PXRowInserted pxRowInserted = func();
      if (pxRowInserted != null)
        pxRowInserted(this, e);
    }
    this.UpdateLastModified(item, true);
  }

  /// <summary>
  /// Raises the RowInserted event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  public virtual void RaiseRowInserted(object item) => this.OnRowInserted(item, false);

  protected internal event PXRowUpdating RowUpdating
  {
    add
    {
      if (this._EventsRow._RowUpdatingList != null)
        this._EventsRow._RowUpdatingList.Insert(0, value);
      else
        this._EventsRow.RowUpdating = value + this._EventsRow.RowUpdating;
    }
    remove
    {
      if (this._EventsRow._RowUpdatingList != null)
        this._EventsRow._RowUpdatingList.Remove(value);
      else
        this._EventsRow.RowUpdating = (PXRowUpdating) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowUpdating, (Delegate) value);
    }
  }

  protected bool OnRowUpdating(object item, object newitem, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    this.TryEnsureRowHasNotBeenPersistedYet(item);
    PXRowUpdating rowUpdating = this._EventsRow.RowUpdating;
    Action<PXCache, PXRowUpdatingEventArgs> action = this.SquashAttributeHandlers<IPXRowUpdatingSubscriber>(this._EventsRowAttr.RowUpdating).To<PXRowUpdatingEventArgs>((Action<IPXRowUpdatingSubscriber, PXCache, PXRowUpdatingEventArgs>) ((h, c, a) => h.RowUpdating(c, a)), item);
    if (rowUpdating == null && action == null)
      return true;
    PXRowUpdatingEventArgs e = new PXRowUpdatingEventArgs(item, newitem, externalCall);
    if (rowUpdating != null)
      rowUpdating(this, e);
    if (!e.Cancel && action != null)
      action(this, e);
    return !e.Cancel;
  }

  /// <summary>
  /// Raises the RowUpdating event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The version of the data record before update.</param>
  /// <param name="newItem">The updated version of the data record.</param>
  public virtual bool RaiseRowUpdating(object item, object newItem)
  {
    return this.OnRowUpdating(item, newItem, false);
  }

  protected internal event PXRowUpdated RowUpdated
  {
    add
    {
      if (this._EventsRow._RowUpdatedList != null)
        this._EventsRow._RowUpdatedList.Add(value);
      else
        this._EventsRow.RowUpdated += value;
    }
    remove
    {
      if (this._EventsRow._RowUpdatedList != null)
        this._EventsRow._RowUpdatedList.Remove(value);
      else
        this._EventsRow.RowUpdated = (PXRowUpdated) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowUpdated, (Delegate) value);
    }
  }

  protected virtual void OnRowUpdated(object newItem, object oldItem, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    this.TryEnsureRowHasNotBeenPersistedYet(newItem);
    this.TryEnsureRowHasNotBeenPersistedYet(oldItem);
    this.SetSessionUnmodified(newItem, oldItem);
    this.SetVersionModified(newItem);
    Func<PXRowUpdated> func = (Func<PXRowUpdated>) (() => this._EventsRow.RowUpdated);
    Action<PXCache, PXRowUpdatedEventArgs> action = this.SquashAttributeHandlers<IPXRowUpdatedSubscriber>(this._EventsRowAttr.RowUpdated).To<PXRowUpdatedEventArgs>((Action<IPXRowUpdatedSubscriber, PXCache, PXRowUpdatedEventArgs>) ((h, c, a) => h.RowUpdated(c, a)), newItem);
    if (func() != null || action != null)
    {
      PXRowUpdatedEventArgs e = new PXRowUpdatedEventArgs(newItem, oldItem, externalCall);
      if (action != null)
        action(this, e);
      PXRowUpdated pxRowUpdated = func();
      if (pxRowUpdated != null)
        pxRowUpdated(this, e);
    }
    this.UpdateLastModified(newItem, false);
  }

  /// <summary>
  /// Raises the RowUpdated event
  /// for the specified data record.
  /// </summary>
  /// <param name="newItem">The updated version of the data record.</param>
  /// <param name="oldItem">The version of the data record before update.</param>
  public virtual void RaiseRowUpdated(object newItem, object oldItem)
  {
    this.OnRowUpdated(newItem, oldItem, false);
  }

  protected internal event PXRowDeleting RowDeleting
  {
    add
    {
      if (this._EventsRow._RowDeletingList != null)
        this._EventsRow._RowDeletingList.Insert(0, value);
      else
        this._EventsRow.RowDeleting = value + this._EventsRow.RowDeleting;
    }
    remove
    {
      if (this._EventsRow._RowDeletingList != null)
        this._EventsRow._RowDeletingList.Remove(value);
      else
        this._EventsRow.RowDeleting = (PXRowDeleting) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowDeleting, (Delegate) value);
    }
  }

  protected bool OnRowDeleting(object item, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    this.TryEnsureRowHasNotBeenPersistedYet(item);
    foreach (System.Type key in new List<System.Type>((IEnumerable<System.Type>) this.Graph.Views.Caches))
    {
      PXCache cach = this.Graph.Caches[key];
    }
    PXRowDeleting rowDeleting = this._EventsRow.RowDeleting;
    Action<PXCache, PXRowDeletingEventArgs> action = this.SquashAttributeHandlers<IPXRowDeletingSubscriber>(this._EventsRowAttr.RowDeleting).To<PXRowDeletingEventArgs>((Action<IPXRowDeletingSubscriber, PXCache, PXRowDeletingEventArgs>) ((h, c, a) => h.RowDeleting(c, a)), item);
    if (rowDeleting == null && action == null)
      return true;
    PXRowDeletingEventArgs e = new PXRowDeletingEventArgs(item, externalCall);
    if (rowDeleting != null)
      rowDeleting(this, e);
    if (!e.Cancel && action != null)
      action(this, e);
    return !e.Cancel;
  }

  /// <summary>
  /// Raises the RowDeleting event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  public virtual bool RaiseRowDeleting(object item) => this.OnRowDeleting(item, false);

  protected internal event PXRowDeleted RowDeleted
  {
    add
    {
      if (this._EventsRow._RowDeletedList != null)
        this._EventsRow._RowDeletedList.Add(value);
      else
        this._EventsRow.RowDeleted += value;
    }
    remove
    {
      if (this._EventsRow._RowDeletedList != null)
        this._EventsRow._RowDeletedList.Remove(value);
      else
        this._EventsRow.RowDeleted = (PXRowDeleted) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowDeleted, (Delegate) value);
    }
  }

  protected void OnRowDeleted(object item, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    this.TryEnsureRowHasNotBeenPersistedYet(item);
    Func<PXRowDeleted> func = (Func<PXRowDeleted>) (() => this._EventsRow.RowDeleted);
    Action<PXCache, PXRowDeletedEventArgs> action = this.SquashAttributeHandlers<IPXRowDeletedSubscriber>(this._EventsRowAttr.RowDeleted).To<PXRowDeletedEventArgs>((Action<IPXRowDeletedSubscriber, PXCache, PXRowDeletedEventArgs>) ((h, c, a) => h.RowDeleted(c, a)), item);
    if (func() == null && action == null)
      return;
    PXRowDeletedEventArgs e = new PXRowDeletedEventArgs(item, externalCall);
    if (action != null)
      action(this, e);
    PXRowDeleted pxRowDeleted = func();
    if (pxRowDeleted == null)
      return;
    pxRowDeleted(this, e);
  }

  /// <summary>
  /// Raises the RowDeleted event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  public virtual void RaiseRowDeleted(object item) => this.OnRowDeleted(item, false);

  protected internal event PXRowPersisting RowPersisting
  {
    add
    {
      if (this._EventsRow._RowPersistingList != null)
        this._EventsRow._RowPersistingList.Insert(0, value);
      else
        this._EventsRow.RowPersisting = value + this._EventsRow.RowPersisting;
    }
    remove
    {
      if (this._EventsRow._RowPersistingList != null)
        this._EventsRow._RowPersistingList.Remove(value);
      else
        this._EventsRow.RowPersisting = (PXRowPersisting) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowPersisting, (Delegate) value);
    }
  }

  protected bool OnRowPersisting(object item, PXDBOperation operation)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    PXRowPersisting rowPersisting = this._EventsRow.RowPersisting;
    Action<PXCache, PXRowPersistingEventArgs> action = this.SquashAttributeHandlers<IPXRowPersistingSubscriber>(this._EventsRowAttr.RowPersisting).To<PXRowPersistingEventArgs>((Action<IPXRowPersistingSubscriber, PXCache, PXRowPersistingEventArgs>) ((h, c, a) => h.RowPersisting(c, a)), item);
    if (rowPersisting == null && action == null)
      return true;
    PXRowPersistingEventArgs e = new PXRowPersistingEventArgs(operation, item);
    if (rowPersisting != null)
      rowPersisting(this, e);
    if (!e.Cancel && action != null)
      action(this, e);
    return !e.Cancel;
  }

  /// <summary>
  /// Raises the RowPersisting event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  /// <param name="operation">The type of the current database operation.</param>
  public virtual bool RaiseRowPersisting(object item, PXDBOperation operation)
  {
    return this.OnRowPersisting(item, operation);
  }

  protected internal event PXRowPersisted RowPersisted
  {
    add
    {
      if (this._EventsRow._RowPersistedList != null)
        this._EventsRow._RowPersistedList.Add(value);
      else
        this._EventsRow.RowPersisted += value;
    }
    remove
    {
      if (this._EventsRow._RowPersistedList != null)
        this._EventsRow._RowPersistedList.Remove(value);
      else
        this._EventsRow.RowPersisted = (PXRowPersisted) PXCache.RemoveEventDelegate((Delegate) this._EventsRow.RowPersisted, (Delegate) value);
    }
  }

  protected void OnRowPersisted(
    object item,
    PXDBOperation operation,
    PXTranStatus tranStatus,
    Exception exception)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    Func<PXRowPersisted> func = (Func<PXRowPersisted>) (() => this._EventsRow.RowPersisted);
    Action<PXCache, PXRowPersistedEventArgs> action = this.SquashAttributeHandlers<IPXRowPersistedSubscriber>(this._EventsRowAttr.RowPersisted).To<PXRowPersistedEventArgs>((Action<IPXRowPersistedSubscriber, PXCache, PXRowPersistedEventArgs>) ((h, c, a) => h.RowPersisted(c, a)), item);
    if (func() == null && action == null)
      return;
    PXRowPersistedEventArgs e = new PXRowPersistedEventArgs(item, operation, tranStatus, exception);
    if (action != null)
      action(this, e);
    PXRowPersisted pxRowPersisted = func();
    if (pxRowPersisted == null)
      return;
    pxRowPersisted(this, e);
  }

  /// <summary>
  /// Raises the RowPersisted event
  /// for the specified data record.
  /// </summary>
  /// <param name="item">The data record for which the event is raised.</param>
  /// <param name="operation">The type of the current database operation.</param>
  /// <param name="tranStatus">The <see cref="T:PX.Data.PXTranStatus">PXTranStatus</see>
  /// value indicating the status of the transaction.</param>
  /// <param name="exception">The exception thrown while the database operation
  /// was executed.</param>
  public void RaiseRowPersisted(
    object item,
    PXDBOperation operation,
    PXTranStatus tranStatus,
    Exception exception)
  {
    this.OnRowPersisted(item, operation, tranStatus, exception);
  }

  protected internal PXCache.EventDictionary<PXFieldDefaulting> FieldDefaultingEvents
  {
    get
    {
      return this._FieldDefaultingEvents ?? (this._FieldDefaultingEvents = new PXCache.EventDictionary<PXFieldDefaulting>());
    }
  }

  /// <summary>
  /// Decorate FieldDefaultingDelegate for using as FieldOfRowDefaultingDelegate.
  /// </summary>
  private static PXCache.FieldOfRowDefaultingDelegate DecorateAsRowDelegate(
    PXCache.FieldDefaultingDelegate defaultingDelegate)
  {
    return defaultingDelegate == null ? (PXCache.FieldOfRowDefaultingDelegate) null : (PXCache.FieldOfRowDefaultingDelegate) ((PXCache sender, string name, object row, ref object defaultValue) => defaultingDelegate(sender, name, ref defaultValue, row != null));
  }

  protected bool OnFieldDefaulting(string name, object row, out object newValue)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = name.ToLower();
    bool flag = false;
    newValue = (object) null;
    List<PXCache.FieldOfRowDefaultingDelegate> defaultingDelegateList = new List<PXCache.FieldOfRowDefaultingDelegate>()
    {
      PXCache.DecorateAsRowDelegate(this.WorkflowStateFieldDefaulting),
      this.WorkflowFieldDefaulting,
      PXCache.DecorateAsRowDelegate(this.AutomationFieldDefaulting)
    };
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmFieldDefaulting)))
    {
      PXFieldDefaulting pxFieldDefaulting1;
      PXFieldDefaulting pxFieldDefaulting2 = this._FieldDefaultingEvents == null || !this._FieldDefaultingEvents.TryGetValue(name, out pxFieldDefaulting1) ? (PXFieldDefaulting) null : pxFieldDefaulting1;
      IPXFieldDefaultingSubscriber[] attributeHandlers;
      Action<PXCache, PXFieldDefaultingEventArgs> action = this._FieldDefaultingEventsAttr == null || !this._FieldDefaultingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXFieldDefaultingEventArgs>) null : this.SquashAttributeHandlers<IPXFieldDefaultingSubscriber>(attributeHandlers).To<PXFieldDefaultingEventArgs>((Action<IPXFieldDefaultingSubscriber, PXCache, PXFieldDefaultingEventArgs>) ((h, c, a) => h.FieldDefaulting(c, a)), row);
      if (pxFieldDefaulting2 != null || action != null)
      {
        PXFieldDefaultingEventArgs args = new PXFieldDefaultingEventArgs(row);
        if (pxFieldDefaulting2 != null)
          pxFieldDefaulting2(this, args);
        if (!args.Cancel && action != null)
          action(this, args);
        newValue = args.NewValue;
        flag = !args.Cancel;
      }
      foreach (PXCache.FieldOfRowDefaultingDelegate defaultingDelegate in defaultingDelegateList)
      {
        if (defaultingDelegate != null && defaultingDelegate(this, name, row, ref newValue))
          return true;
      }
      return flag;
    }
  }

  /// <summary>
  /// Raises the FieldDefaulting
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The default value for the current field.</param>
  public virtual bool RaiseFieldDefaulting(string name, object row, out object newValue)
  {
    return this.OnFieldDefaulting(name, row, out newValue);
  }

  /// <summary>
  /// Raises the FieldDefaulting
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The default value for the current field.</param>
  /// <example>
  /// The code below shows an example of raising the <tt>FieldDefaulting</tt>
  /// event.
  /// <code>
  /// CashAccount acct = null;
  /// 
  /// // Get the cache (the other way is to use Cache property of a data view)
  /// PXCache cache = this.Caches[typeof(ARPayment)].Cache;
  /// 
  /// // Initialize a new ARPayment data record
  /// ARPayment payment = new ARPayment();
  /// payment.CustomerID = aDoc.CustomerID;
  /// payment.CustomerLocationID = aDoc.CustomerLocationID;
  /// 
  /// // You could execute cache.Insert(payment) to insert the data record
  /// // in the cache and raise the events including FieldDefaulting.
  /// // However, we need to raise FieldDefaulting only on one field.
  /// 
  /// // Declare a variable for the value
  /// object newValue;
  /// 
  /// // Raise the FieldDefaulting event
  /// cache.RaiseFieldDefaulting&lt;ARPayment.cashAccountID&gt;(payment, out newValue);
  /// 
  /// // Convert the object to the data type of the field
  /// Int32? acctID = newValue as Int32?;
  /// 
  /// // Use the value to retrieve the CashAccount data record
  /// if (acctID.HasValue)
  /// {
  ///     acct = PXSelect&lt;CashAccount,
  ///         Where&lt;CashAccount.cashAccountID,
  ///             Equal&lt;Required&lt;CashAccount.cashAccountID&gt;&gt;&gt;&gt;.
  ///         Select(this, acctID);
  /// }
  /// </code>
  /// </example>
  public bool RaiseFieldDefaulting<Field>(object row, out object newValue) where Field : IBqlField
  {
    return this.OnFieldDefaulting(typeof (Field).Name, row, out newValue);
  }

  protected internal PXCache.EventDictionary<PXFieldUpdating> FieldUpdatingEvents
  {
    get
    {
      return this._FieldUpdatingEvents ?? (this._FieldUpdatingEvents = new PXCache.EventDictionary<PXFieldUpdating>());
    }
  }

  protected virtual bool OnFieldUpdating(string name, object row, ref object newValue)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = name.ToLower();
    PXFieldUpdating pxFieldUpdating1;
    PXFieldUpdating pxFieldUpdating2 = this._FieldUpdatingEvents == null || !this._FieldUpdatingEvents.TryGetValue(name, out pxFieldUpdating1) ? (PXFieldUpdating) null : pxFieldUpdating1;
    IPXFieldUpdatingSubscriber[] attributeHandlers;
    Action<PXCache, PXFieldUpdatingEventArgs> action = this._FieldUpdatingEventsAttr == null || !this._FieldUpdatingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXFieldUpdatingEventArgs>) null : this.SquashAttributeHandlers<IPXFieldUpdatingSubscriber>(attributeHandlers).To<PXFieldUpdatingEventArgs>((Action<IPXFieldUpdatingSubscriber, PXCache, PXFieldUpdatingEventArgs>) ((h, c, a) => h.FieldUpdating(c, a)), row);
    if (pxFieldUpdating2 == null && action == null)
      return true;
    PXFieldUpdatingEventArgs args = new PXFieldUpdatingEventArgs(row, newValue);
    try
    {
      if (pxFieldUpdating2 != null)
        pxFieldUpdating2(this, args);
      if (!args.Cancel)
      {
        if (action != null)
          action(this, args);
      }
    }
    finally
    {
      newValue = args.NewValue;
    }
    return !args.Cancel;
  }

  /// <summary>
  /// Raises the FieldUpdating
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The updated value of the current field.</param>
  public bool RaiseFieldUpdating(string name, object row, ref object newValue)
  {
    return this.OnFieldUpdating(name, row, ref newValue);
  }

  /// <summary>
  /// Raises the FieldUpdating
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The updated value of the current field.</param>
  public bool RaiseFieldUpdating<Field>(object row, ref object newValue) where Field : IBqlField
  {
    return this.OnFieldUpdating(typeof (Field).Name, row, ref newValue);
  }

  protected internal PXCache.EventDictionary<PXFieldVerifying> FieldVerifyingEvents
  {
    get
    {
      return this._FieldVerifyingEvents ?? (this._FieldVerifyingEvents = new PXCache.EventDictionary<PXFieldVerifying>());
    }
  }

  protected internal bool OnFieldVerifying(
    string name,
    object row,
    ref object newValue,
    bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = name.ToLower();
    PXFieldVerifying pxFieldVerifying1;
    PXFieldVerifying pxFieldVerifying2 = this._FieldVerifyingEvents == null || !this._FieldVerifyingEvents.TryGetValue(name, out pxFieldVerifying1) ? (PXFieldVerifying) null : pxFieldVerifying1;
    IPXFieldVerifyingSubscriber[] attributeHandlers;
    Action<PXCache, PXFieldVerifyingEventArgs> action = this._FieldVerifyingEventsAttr == null || !this._FieldVerifyingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXFieldVerifyingEventArgs>) null : this.SquashAttributeHandlers<IPXFieldVerifyingSubscriber>(attributeHandlers).To<PXFieldVerifyingEventArgs>((Action<IPXFieldVerifyingSubscriber, PXCache, PXFieldVerifyingEventArgs>) ((h, c, a) => h.FieldVerifying(c, a)), row);
    if (pxFieldVerifying2 == null && action == null)
      return true;
    PXFieldVerifyingEventArgs args = new PXFieldVerifyingEventArgs(row, newValue, externalCall);
    try
    {
      if (pxFieldVerifying2 != null)
        pxFieldVerifying2(this, args);
      if (!args.Cancel)
      {
        if (action != null)
          action(this, args);
      }
    }
    finally
    {
      newValue = args.NewValue;
    }
    return !args.Cancel;
  }

  /// <summary>
  /// Raises the FieldVerifying
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The updated value of the current field.</param>
  public virtual bool RaiseFieldVerifying(string name, object row, ref object newValue)
  {
    return this.OnFieldVerifying(name, row, ref newValue, false);
  }

  /// <summary>
  /// Raises the FieldVerifying
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The updated value of the current field.</param>
  public bool RaiseFieldVerifying<Field>(object row, ref object newValue) where Field : IBqlField
  {
    return this.OnFieldVerifying(typeof (Field).Name, row, ref newValue, false);
  }

  protected internal PXCache.EventDictionary<PXFieldUpdated> FieldUpdatedEvents
  {
    get
    {
      return this._FieldUpdatedEvents ?? (this._FieldUpdatedEvents = new PXCache.EventDictionary<PXFieldUpdated>());
    }
  }

  protected void OnFieldUpdated(string name, object row, object oldValue, bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = name.ToLower();
    PXFieldUpdated pxFieldUpdated1;
    PXFieldUpdated pxFieldUpdated2 = this._FieldUpdatedEvents == null || !this._FieldUpdatedEvents.TryGetValue(name, out pxFieldUpdated1) ? (PXFieldUpdated) null : pxFieldUpdated1;
    IPXFieldUpdatedSubscriber[] attributeHandlers;
    Action<PXCache, PXFieldUpdatedEventArgs> action = this._FieldUpdatedEventsAttr == null || !this._FieldUpdatedEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXFieldUpdatedEventArgs>) null : this.SquashAttributeHandlers<IPXFieldUpdatedSubscriber>(attributeHandlers).To<PXFieldUpdatedEventArgs>((Action<IPXFieldUpdatedSubscriber, PXCache, PXFieldUpdatedEventArgs>) ((h, c, a) => h.FieldUpdated(c, a)), row);
    if (pxFieldUpdated2 == null && action == null)
      return;
    PXFieldUpdatedEventArgs args = new PXFieldUpdatedEventArgs(row, oldValue, externalCall);
    if (action != null)
      action(this, args);
    if (pxFieldUpdated2 == null)
      return;
    pxFieldUpdated2(this, args);
  }

  /// <summary>
  /// Raises the FieldUpdated
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="oldValue">The value of the current field before update.</param>
  public virtual void RaiseFieldUpdated(string name, object row, object oldValue)
  {
    this.OnFieldUpdated(name, row, oldValue, false);
  }

  /// <summary>
  /// Raises the FieldUpdated
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="oldValue">The value of the current field before update.</param>
  public virtual void RaiseFieldUpdated<Field>(object row, object oldValue) where Field : IBqlField
  {
    this.OnFieldUpdated(typeof (Field).Name, row, oldValue, false);
  }

  protected internal PXCache.EventDictionary<PXFieldSelecting> FieldSelectingEvents
  {
    get
    {
      return this._FieldSelectingEvents ?? (this._FieldSelectingEvents = new PXCache.EventDictionary<PXFieldSelecting>());
    }
  }

  protected virtual bool OnFieldSelecting(
    string name,
    object row,
    ref object returnValue,
    bool forceState,
    bool externalCall)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = name.ToLower();
    PXFieldSelecting pxFieldSelecting1;
    PXFieldSelecting pxFieldSelecting2 = this._FieldSelectingEvents == null || !this._FieldSelectingEvents.TryGetValue(name, out pxFieldSelecting1) ? (PXFieldSelecting) null : pxFieldSelecting1;
    IPXFieldSelectingSubscriber[] attributeHandlers;
    Action<PXCache, PXFieldSelectingEventArgs> action = this._FieldSelectingEventsAttr == null || !this._FieldSelectingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXFieldSelectingEventArgs>) null : this.SquashAttributeHandlers<IPXFieldSelectingSubscriber>(attributeHandlers).To<PXFieldSelectingEventArgs>((Action<IPXFieldSelectingSubscriber, PXCache, PXFieldSelectingEventArgs>) ((h, c, a) => h.FieldSelecting(c, a)), row);
    if (pxFieldSelecting2 == null && action == null)
      return true;
    if (pxFieldSelecting2 != null)
    {
      if (!externalCall)
      {
        HashSet<string> selectingFields = this._SelectingFields;
        // ISSUE: explicit non-virtual call
        if ((selectingFields != null ? (__nonvirtual (selectingFields.Contains(name)) ? 1 : 0) : 0) == 0)
          goto label_5;
      }
      this.SetAltered(name, true);
      forceState = true;
    }
label_5:
    PXFieldSelectingEventArgs args = new PXFieldSelectingEventArgs(row, returnValue, forceState, externalCall);
    if (pxFieldSelecting2 != null)
      pxFieldSelecting2(this, args);
    if (!args.Cancel && action != null)
      action(this, args);
    returnValue = args.ReturnState;
    if (this.AutomationFieldSelecting != null && returnValue is PXFieldState)
      this.AutomationFieldSelecting(name, ref returnValue, row);
    if (this.WorkflowFieldSelecting != null && returnValue is PXFieldState)
      this.WorkflowFieldSelecting(name, ref returnValue, row);
    return !args.Cancel;
  }

  /// <summary>
  /// Raises the FieldSelecting
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="returnValue">The external presentation of the value of the current field.</param>
  /// <param name="forceState">The value indicating whether the
  /// <see cref="T:PX.Data.PXFieldState">PXFieldState</see> object should be generated.</param>
  public virtual bool RaiseFieldSelecting(
    string name,
    object row,
    ref object returnValue,
    bool forceState)
  {
    return this.OnFieldSelecting(name, row, ref returnValue, forceState, true);
  }

  /// <exclude />
  internal bool RaiseFieldSelectingInt(
    string name,
    object row,
    ref object returnValue,
    bool forceState)
  {
    return this.OnFieldSelecting(name, row, ref returnValue, forceState, false);
  }

  /// <summary>
  /// Raises the FieldSelecting
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="returnValue">The external presentation of the value of the current field.</param>
  /// <param name="forceState">The value indicating whether the
  /// <see cref="T:PX.Data.PXFieldState">PXFieldState</see> object should be generated.</param>
  public bool RaiseFieldSelecting<Field>(object row, ref object returnValue, bool forceState) where Field : IBqlField
  {
    return this.OnFieldSelecting(typeof (Field).Name, row, ref returnValue, forceState, true);
  }

  protected internal PXCache.EventDictionary<PXExceptionHandling> ExceptionHandlingEvents
  {
    get
    {
      return this._ExceptionHandlingEvents ?? (this._ExceptionHandlingEvents = new PXCache.EventDictionary<PXExceptionHandling>());
    }
  }

  protected bool OnExceptionHandling(
    string name,
    object row,
    object newValue,
    Exception exception)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    name = !(exception is PXOverridableException overridableException) || overridableException.MapErrorTo == null ? name.ToLower() : overridableException.MapErrorTo.ToLower();
    PXExceptionHandling exceptionHandling1;
    PXExceptionHandling exceptionHandling2 = this._ExceptionHandlingEvents == null || !this._ExceptionHandlingEvents.TryGetValue(name, out exceptionHandling1) ? (PXExceptionHandling) null : exceptionHandling1;
    IPXExceptionHandlingSubscriber[] attributeHandlers;
    Action<PXCache, PXExceptionHandlingEventArgs> action = this._ExceptionHandlingEventsAttr == null || !this._ExceptionHandlingEventsAttr.TryGetValue(name, out attributeHandlers) ? (Action<PXCache, PXExceptionHandlingEventArgs>) null : this.SquashAttributeHandlers<IPXExceptionHandlingSubscriber>(attributeHandlers).To<PXExceptionHandlingEventArgs>((Action<IPXExceptionHandlingSubscriber, PXCache, PXExceptionHandlingEventArgs>) ((h, c, a) => h.ExceptionHandling(c, a)), row);
    if (exceptionHandling2 == null && action == null)
      return true;
    PXExceptionHandlingEventArgs args = new PXExceptionHandlingEventArgs(row, newValue, exception);
    if (exceptionHandling2 != null)
      exceptionHandling2(this, args);
    if (!args.Cancel && action != null)
      action(this, args);
    newValue = args.NewValue;
    return !args.Cancel;
  }

  /// <summary>
  /// Raises the ExceptionHandling
  /// event for the specified field and data record.
  /// </summary>
  /// <param name="name">The name of the field for which the event is raised.
  /// The parameter is case-insensitive.</param>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The new value of the current field generated by the
  /// operation that causes the exception.</param>
  /// <param name="exception">The exception that causes the event.</param>
  /// <remarks>
  ///   <para>
  ///     The <see cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />,
  ///     which is used to prevent the saving of a record or to display an error or warning on the form,
  ///     cannot be invoked on a <see cref="T:PX.Data.PXCache" /> instance in the following event handlers:</para>
  ///   <list type="bullet">
  ///     <item><description>
  ///       <tt>FieldDefaulting</tt>: This event handler works with a record that has not yet been added
  ///       to <see cref="T:PX.Data.PXCache" /> or a record whose field is changed in the code. Neither situation involves
  ///       the display of errors or warning for a record.</description></item>
  ///     <item><description>
  ///       <tt>FieldSelecting</tt>: This event handler is used to configure a UI control of a field.
  ///       The invocation of <see cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />
  ///       in this event handler has no effect.</description></item>
  ///     <item><description>
  ///       <tt>RowSelecting</tt>: This event handler is called when the record is being read from the database.
  ///       These records are unavailable in <see cref="T:PX.Data.PXCache" /> yet. Invocation of
  ///       <see cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" /> in this event
  ///       handler has no effect.</description></item>
  ///     <item>
  ///       <description><tt>RowPersisted</tt>: This event handler is called when the record
  ///       has already been saved to the database. Therefore, it would
  ///       not make sense to display any warnings for this record.</description></item>
  ///   </list>
  ///   <para>
  ///     <see cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" /> usually is invoked
  ///     in the following event handlers:</para>
  ///   <list type="bullet">
  ///     <item><description>
  ///       <tt>RowPersisting</tt> to prevent saving of a record</description></item>
  ///     <item><description>
  ///       <tt>RowSelected</tt> to display an error or warning on the form</description></item>
  ///   </list>
  /// </remarks>
  public virtual bool RaiseExceptionHandling(
    string name,
    object row,
    object newValue,
    Exception exception)
  {
    if (exception is PXOverridableException overridableException && overridableException.MapErrorTo != null)
      name = overridableException.MapErrorTo;
    PXCache.TryDispose((object) this.GetAttributes(row, name));
    bool flag = this.OnExceptionHandling(name, row, newValue, exception);
    if (flag && exception != null && row != null)
    {
      if (exception is PXSetPropertyException propertyException)
      {
        switch (propertyException.ErrorLevel)
        {
          case PXErrorLevel.RowInfo:
          case PXErrorLevel.RowWarning:
          case PXErrorLevel.RowError:
            break;
          case PXErrorLevel.Warning:
            PXTrace.WriteWarning(exception);
            break;
          default:
            PXTrace.WriteError(exception);
            break;
        }
      }
      else
        PXTrace.WriteError(exception);
    }
    return flag;
  }

  /// <summary>
  /// Raises the ExceptionHandling
  /// event for the specified field and data record.
  /// </summary>
  /// <typeparam name="Field">The field for which the event is raised.</typeparam>
  /// <param name="row">The data record for which the event is raised.</param>
  /// <param name="newValue">The new value of the current field generated by the operation that causes the exception.</param>
  /// <param name="exception">The exception that causes the event.</param>
  /// <example>
  /// A typical use of the method is found in event handlers when the value of
  /// a field doesn't pass validation. If the value is validated in a
  /// <tt>RowUpdating</tt> event handler, you should pass an instance of
  /// <tt>PXSetPropertyException</tt> with the error message to the method.
  /// The code below gives an example for this case.
  /// <code>
  /// INComponent row = e.NewRow as INComponent;
  /// if (row != null &amp;&amp; row.Qty != null &amp;&amp;
  ///     row.MinQty != null &amp;&amp; row.Qty &lt;= row.MinQty)
  /// {
  ///     sender.RaiseExceptionHandling&lt;INComponent.qty&gt;(
  ///         row, row.Qty, new PXSetPropertyException(
  ///             "Quantity must be greater or equal to Min. Quantity."));
  /// }
  /// </code>
  /// </example>
  public bool RaiseExceptionHandling<Field>(object row, object newValue, Exception exception) where Field : IBqlField
  {
    return this.RaiseExceptionHandling(typeof (Field).Name, row, newValue, exception);
  }

  private static 
  #nullable enable
  Delegate? RemoveEventDelegate(Delegate? source, Delegate value)
  {
    foreach (Delegate @delegate in source?.GetInvocationList() ?? Array.Empty<Delegate>())
    {
      if (@delegate.Target is IEventInterceptorProxy target)
        target.InterceptedDelegate = PXCache.RemoveEventDelegate(target.InterceptedDelegate, value);
    }
    return Delegate.Remove(source, value);
  }

  protected virtual void TryEnsureRowHasNotBeenPersistedYet(
  #nullable disable
  object row)
  {
  }

  private PXCache.AttributeHandlersSquasher<TAttributeHandler> SquashAttributeHandlers<TAttributeHandler>(
    TAttributeHandler[] attributeHandlers)
    where TAttributeHandler : class
  {
    return new PXCache.AttributeHandlersSquasher<TAttributeHandler>(attributeHandlers);
  }

  private PXCache.AttributeRowSelectingHandlersSquasher SquashAttributeHandlers(
    IPXRowSelectingSubscriber[] attributes)
  {
    return new PXCache.AttributeRowSelectingHandlersSquasher(attributes);
  }

  /// <summary>
  /// Ensures cleanup of item attributes after foreach statement
  /// </summary>
  protected class DisposableAttributesList : 
    IPXAttributeList,
    IEnumerable<PXEventSubscriberAttribute>,
    IEnumerable,
    IDisposable
  {
    public string NameFilter;
    public PXCache cache;
    public PXCache.DirtyItemState Buffer;
    private bool _disposed;
    internal System.Type TypeFilter;

    public DisposableAttributesList(PXCache c, object data)
    {
      this.cache = c;
      this.Buffer = c.GetItemState(data);
      if (this.Buffer.RefCnt <= 1)
        return;
      --this.Buffer.RefCnt;
      this._disposed = true;
    }

    public void Dispose()
    {
      if (this._disposed)
        return;
      this._disposed = true;
      this.cache.CompressItemState(this.Buffer);
    }

    public IEnumerator<PXEventSubscriberAttribute> GetEnumerator()
    {
      return this.EnumItems().GetEnumerator();
    }

    private bool IsMatchFilters(PXEventSubscriberAttribute a)
    {
      return (this.NameFilter == null || this.NameFilter.Equals(a.FieldName, StringComparison.OrdinalIgnoreCase)) && (!(this.TypeFilter != (System.Type) null) || this.TypeFilter.IsInstanceOfType((object) a));
    }

    private IEnumerable<PXEventSubscriberAttribute> EnumItems()
    {
      PXCache.DisposableAttributesList disposableAttributesList = this;
      using (disposableAttributesList)
      {
        PXEventSubscriberAttribute[] d = disposableAttributesList.Buffer.DirtyAttributes;
        PXEventSubscriberAttribute[] attributesWithEmbedded = disposableAttributesList.cache._CacheAttributesWithEmbedded;
        if (disposableAttributesList.NameFilter != null)
        {
          if (disposableAttributesList.cache._AttributesByName.Contains(disposableAttributesList.NameFilter))
          {
            foreach (PXEventSubscriberAttribute a in disposableAttributesList.cache._AttributesByName[disposableAttributesList.NameFilter])
            {
              if (disposableAttributesList.IsMatchFilters(a))
              {
                int indexInClonesArray = a.IndexInClonesArray;
                yield return disposableAttributesList.cache.CheckoutItemAttribute(disposableAttributesList.Buffer, indexInClonesArray);
              }
            }
          }
        }
        else if (disposableAttributesList.TypeFilter != (System.Type) null)
        {
          if (disposableAttributesList.cache._AttributesByType.Contains(disposableAttributesList.TypeFilter))
          {
            foreach (PXEventSubscriberAttribute a in disposableAttributesList.cache._AttributesByType[disposableAttributesList.TypeFilter])
            {
              if (disposableAttributesList.IsMatchFilters(a))
              {
                int indexInClonesArray = a.IndexInClonesArray;
                yield return disposableAttributesList.cache.CheckoutItemAttribute(disposableAttributesList.Buffer, indexInClonesArray);
              }
            }
          }
        }
        else
        {
          for (int i = 0; i < d.Length; ++i)
            yield return disposableAttributesList.cache.CheckoutItemAttribute(disposableAttributesList.Buffer, i);
        }
        d = (PXEventSubscriberAttribute[]) null;
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.EnumItems().GetEnumerator();
  }

  private class CompiledAttributeComparer
  {
    private readonly System.Type T;
    private Func<object, object, bool> _CompiledMethod;

    public CompiledAttributeComparer(System.Type t) => this.T = t;

    public Func<object, object, bool> CompiledMethod
    {
      get
      {
        if (this._CompiledMethod == null)
        {
          lock (this)
          {
            if (this._CompiledMethod == null)
              this._CompiledMethod = this.CompileAttributeComparer();
          }
        }
        return this._CompiledMethod;
      }
    }

    private Func<object, object, bool> CompileAttributeComparer()
    {
      List<System.Reflection.FieldInfo> fieldInfoList = new List<System.Reflection.FieldInfo>();
      for (System.Type type = this.T; type != typeof (object) && !(type == typeof (PXEventSubscriberAttribute)); type = type.BaseType)
      {
        System.Reflection.FieldInfo[] array = ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).OrderBy<System.Reflection.FieldInfo, string>((Func<System.Reflection.FieldInfo, string>) (_ => _.Name)).ToArray<System.Reflection.FieldInfo>();
        if (type == typeof (PXAggregateAttribute))
          array = ((IEnumerable<System.Reflection.FieldInfo>) array).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (_ => _.Name != "_Attributes")).ToArray<System.Reflection.FieldInfo>();
        fieldInfoList.AddRange((IEnumerable<System.Reflection.FieldInfo>) array);
      }
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (object), "a");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (object), "b");
      Expression expression = (Expression) Expression.Constant((object) true);
      foreach (System.Reflection.FieldInfo field in fieldInfoList)
        expression = (Expression) Expression.And((Expression) Expression.Equal((Expression) Expression.Field((Expression) Expression.Convert((Expression) parameterExpression1, field.DeclaringType), field), (Expression) Expression.Field((Expression) Expression.Convert((Expression) parameterExpression2, field.DeclaringType), field)), expression);
      return Expression.Lambda<Func<object, object, bool>>(expression, parameterExpression1, parameterExpression2).Compile();
    }
  }

  /// <summary>
  /// Container for dirty attributes in _ItemAttributes collection
  /// </summary>
  protected class DirtyItemState
  {
    public PXEventSubscriberAttribute[] DirtyAttributes;
    public int RefCnt;
    public object BoundItem;
  }

  protected class MeasuringUIFieldAttribute : PXUIFieldAttribute
  {
    private PXCache _Cache;

    public MeasuringUIFieldAttribute(
      PXCache cache,
      string fieldName,
      int fieldOrdinal,
      System.Type bqlTable)
    {
      this._Cache = cache;
      this._FieldName = fieldName;
      this._FieldOrdinal = fieldOrdinal;
      this._BqlTable = bqlTable;
    }

    public override bool Enabled
    {
      get => base.Enabled;
      set
      {
        base.Enabled = value;
        PXContext.GetSlot<HashSet<string>>("_MeasuringUpdatability").Add(this._FieldName);
      }
    }
  }

  public class ExternalCallMarker
  {
    public readonly object Value;

    public bool IsInternalCall { get; set; }

    public ExternalCallMarker(object value) => this.Value = value;

    public static object Unwrap(object value)
    {
      return !(value is PXCache.ExternalCallMarker externalCallMarker) ? value : externalCallMarker.Value;
    }
  }

  internal class IndirectMappingScope : IDisposable
  {
    private System.Type _extendedGraph;

    internal static System.Type Get()
    {
      return PXContext.GetSlot<PXCache.IndirectMappingScope>()?._extendedGraph;
    }

    public IndirectMappingScope(System.Type graph)
    {
      this._extendedGraph = graph;
      PXContext.SetSlot<PXCache.IndirectMappingScope>(this);
    }

    public void Dispose()
    {
      PXContext.SetSlot<PXCache.IndirectMappingScope>((PXCache.IndirectMappingScope) null);
    }
  }

  internal class Mapping
  {
    private Dictionary<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>> _maps = new Dictionary<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>>();
    private Dictionary<Tuple<System.Type, System.Type>, Dictionary<string, string>> _remappingByTableAndExtension = new Dictionary<Tuple<System.Type, System.Type>, Dictionary<string, string>>();
    private HashSet<System.Type> _disabledGraphExtensions = new HashSet<System.Type>();

    private Tuple<System.Type, System.Type> getKey(System.Type extendedGraph, System.Type mappedCacheExtension)
    {
      return Tuple.Create<System.Type, System.Type>(CustomizedTypeManager.GetTypeNotCustomized(extendedGraph), PXCache.Mapping.GetFirstExtensionImplementation(mappedCacheExtension));
    }

    private List<IBqlMapping> FindMaps(System.Type extendedGraph, System.Type mappedCacheExtension)
    {
      Tuple<System.Type, List<IBqlMapping>> tuple;
      return this._maps.TryGetValue(this.getKey(extendedGraph, mappedCacheExtension), out tuple) && tuple.Item2.Count > 0 ? tuple.Item2 : (List<IBqlMapping>) null;
    }

    private List<IBqlMapping> FindMaps(
      System.Type extendedGraph,
      System.Type extendingGraph,
      string mappedCacheExtensionName)
    {
      extendedGraph = CustomizedTypeManager.GetTypeNotCustomized(extendedGraph);
      List<IBqlMapping> bqlMappingList = this._maps.Where<KeyValuePair<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>>>((Func<KeyValuePair<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>>, bool>) (_ => _.Key.Item1 == extendedGraph && (_.Value.Item1.IsAssignableFrom(extendingGraph) || (!_.Value.Item1.BaseType.IsGenericType || !string.Equals(MainTools.GetGenericSimpleName(_.Value.Item1.BaseType), typeof (PXGraphExtension).Name, StringComparison.OrdinalIgnoreCase)) && _.Value.Item1.BaseType.IsAssignableFrom(extendingGraph)) && string.Compare(MainTools.GetGenericSimpleName(_.Key.Item2), mappedCacheExtensionName, StringComparison.OrdinalIgnoreCase) == 0)).Select<KeyValuePair<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>>, List<IBqlMapping>>((Func<KeyValuePair<Tuple<System.Type, System.Type>, Tuple<System.Type, List<IBqlMapping>>>, List<IBqlMapping>>) (_ => _.Value.Item2)).SingleOrDefault<List<IBqlMapping>>();
      return bqlMappingList != null && bqlMappingList.Count > 0 ? bqlMappingList : (List<IBqlMapping>) null;
    }

    private List<IBqlMapping> FindMapsForInheritedGraph(
      System.Type extendedGraph,
      System.Type mappedCacheExtension)
    {
      extendedGraph = CustomizedTypeManager.GetTypeNotCustomized(extendedGraph);
      List<IBqlMapping> maps;
      do
      {
        maps = this.FindMaps(extendedGraph, mappedCacheExtension);
        extendedGraph = extendedGraph.BaseType;
      }
      while (maps == null && extendedGraph != typeof (PXGraph));
      return maps;
    }

    private List<IBqlMapping> FindMapsForInheritedGraph(
      System.Type extendedGraph,
      System.Type extendingGraph,
      string mappedCacheExtensionName)
    {
      extendedGraph = CustomizedTypeManager.GetTypeNotCustomized(extendedGraph);
      List<IBqlMapping> maps;
      do
      {
        maps = this.FindMaps(extendedGraph, extendingGraph, mappedCacheExtensionName);
        extendedGraph = extendedGraph.BaseType;
      }
      while (maps == null && extendedGraph != typeof (PXGraph));
      return maps;
    }

    private void AddMap(
      System.Type extendedGraph,
      System.Type extendingGraph,
      System.Type mappedCacheExtension,
      IBqlMapping mapping)
    {
      Tuple<System.Type, System.Type> key = this.getKey(extendedGraph, mappedCacheExtension);
      Tuple<System.Type, List<IBqlMapping>> tuple;
      if (!this._maps.TryGetValue(key, out tuple))
        this._maps[key] = tuple = Tuple.Create<System.Type, List<IBqlMapping>>(extendingGraph, new List<IBqlMapping>());
      tuple.Item2.Add(mapping);
    }

    private static System.Type GetFirstExtensionImplementation(System.Type extension)
    {
      System.Type extensionImplementation;
      for (extensionImplementation = extension; extensionImplementation.BaseType != (System.Type) null; extensionImplementation = extensionImplementation.BaseType)
      {
        string str = extensionImplementation.BaseType.Name;
        int length = str.IndexOf('`');
        if (length != -1)
          str = str.Substring(0, length);
        if (str == typeof (PXMappedCacheExtension).Name)
          break;
      }
      return extensionImplementation;
    }

    public void AddMap(System.Type extendingGraph, IBqlMapping mapping)
    {
      this.AddMap(PXExtensionManager.GetExtendedGraphType(extendingGraph, out bool _), extendingGraph, mapping.Extension, mapping);
      if (!mapping.Extension.IsDefined(typeof (PXProxyPropertiesAttribute)))
        return;
      Dictionary<string, string> dictionary;
      this._remappingByTableAndExtension[Tuple.Create<System.Type, System.Type>(mapping.Table, mapping.Extension)] = dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (System.Reflection.FieldInfo field in mapping.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
      {
        if (field.FieldType == typeof (System.Type))
        {
          string name1 = field.Name;
          string name2 = ((System.Type) field.GetValue((object) mapping)).Name;
          dictionary[name2] = name1;
        }
      }
      foreach (PropertyInfo property in mapping.Extension.GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        if (!string.Equals(property.Name, "Base", StringComparison.OrdinalIgnoreCase) && !dictionary.ContainsKey(property.Name))
          dictionary[property.Name] = property.Name;
      }
    }

    internal Dictionary<string, string> GetFieldsMapping(System.Type table, System.Type extension)
    {
      Dictionary<string, string> dictionary;
      return this._remappingByTableAndExtension.TryGetValue(Tuple.Create<System.Type, System.Type>(table, extension), out dictionary) ? dictionary : (Dictionary<string, string>) null;
    }

    public bool TryGetMap(System.Type extendedGraph, System.Type mappedCacheExtension, out IBqlMapping map)
    {
      ref IBqlMapping local = ref map;
      List<IBqlMapping> forInheritedGraph = this.FindMapsForInheritedGraph(extendedGraph, mappedCacheExtension);
      IBqlMapping bqlMapping = forInheritedGraph != null ? forInheritedGraph.FirstOrDefault<IBqlMapping>() : (IBqlMapping) null;
      local = bqlMapping;
      return map != null;
    }

    public bool TryGetMap(
      System.Type extendedGraph,
      System.Type mappedCacheExtension,
      System.Type extendedType,
      out IBqlMapping map)
    {
      ref IBqlMapping local = ref map;
      List<IBqlMapping> forInheritedGraph = this.FindMapsForInheritedGraph(extendedGraph, mappedCacheExtension);
      IBqlMapping bqlMapping = forInheritedGraph != null ? forInheritedGraph.FirstOrDefault<IBqlMapping>((Func<IBqlMapping, bool>) (_ => _.Table == extendedType)) : (IBqlMapping) null;
      local = bqlMapping;
      return map != null;
    }

    public bool TryGetMap(
      System.Type extendedGraph,
      System.Type extendingGraph,
      string mappedCacheExtensionName,
      out IBqlMapping map)
    {
      ref IBqlMapping local = ref map;
      List<IBqlMapping> forInheritedGraph = this.FindMapsForInheritedGraph(extendedGraph, extendingGraph, mappedCacheExtensionName);
      IBqlMapping bqlMapping = forInheritedGraph != null ? forInheritedGraph.FirstOrDefault<IBqlMapping>() : (IBqlMapping) null;
      local = bqlMapping;
      return map != null;
    }

    public IBqlMapping GetMap(System.Type extendedGraph, System.Type mappedCacheExtension)
    {
      IBqlMapping map;
      if (this.TryGetMap(extendedGraph, mappedCacheExtension, out map))
        return map;
      System.Type extendedGraph1 = PXCache.IndirectMappingScope.Get();
      if (extendedGraph1 != (System.Type) null && this.TryGetMap(extendedGraph1, mappedCacheExtension, out map))
        return map;
      System.Type baseType = PXCache.Mapping.GetFirstExtensionImplementation(mappedCacheExtension).BaseType;
      if (baseType.IsGenericType)
        return (IBqlMapping) new PXCache.Mapping.DefaultMapping(baseType.GenericTypeArguments[0], mappedCacheExtension);
      throw new KeyNotFoundException($"Requested unavailable mapping {mappedCacheExtension.FullName} for graph {extendedGraph.FullName}");
    }

    public IBqlMapping GetMap(System.Type extendedGraph, System.Type mappedCacheExtension, System.Type extendedType)
    {
      IBqlMapping map;
      if (this.TryGetMap(extendedGraph, mappedCacheExtension, extendedType, out map))
        return map;
      System.Type extendedGraph1 = PXCache.IndirectMappingScope.Get();
      if (extendedGraph1 != (System.Type) null && this.TryGetMap(extendedGraph1, mappedCacheExtension, out map))
        return map;
      System.Type baseType = PXCache.Mapping.GetFirstExtensionImplementation(mappedCacheExtension).BaseType;
      if (baseType.IsGenericType)
        return (IBqlMapping) new PXCache.Mapping.DefaultMapping(baseType.GenericTypeArguments[0], mappedCacheExtension);
      throw new KeyNotFoundException($"Requested unavailable mapping {mappedCacheExtension.FullName} for dac {extendedType.FullName} used in graph {extendedGraph.FullName}");
    }

    public List<IBqlMapping> GetAllMaps()
    {
      return this._maps.Values.SelectMany<Tuple<System.Type, List<IBqlMapping>>, IBqlMapping>((Func<Tuple<System.Type, List<IBqlMapping>>, IEnumerable<IBqlMapping>>) (_ => (IEnumerable<IBqlMapping>) _.Item2)).ToList<IBqlMapping>();
    }

    public PXCache CreateModelExtension(IBqlMapping map, PXGraph graph)
    {
      return (PXCache) Activator.CreateInstance(typeof (PXModelExtension<>).MakeGenericType(map.Extension), (object) graph, (object) map);
    }

    public PXCache CreateModelExtension(System.Type mce, PXGraph graph)
    {
      return this.CreateModelExtension(this.GetMap(graph.GetType(), mce), graph);
    }

    public PXCache CreateModelExtension(System.Type mce, System.Type et, PXGraph graph)
    {
      return this.CreateModelExtension(this.GetMap(graph.GetType(), mce, et), graph);
    }

    internal void AddDisabledGraphExtension(System.Type t)
    {
      if (!typeof (PXGraphExtension).IsAssignableFrom(t))
        return;
      this._disabledGraphExtensions.Add(t);
    }

    internal bool IsDisabled(System.Type extType)
    {
      return this._disabledGraphExtensions.Contains(extType);
    }

    internal class DefaultMapping : IBqlMapping
    {
      private System.Type _table;
      private System.Type _extension;

      public System.Type Table => this._table;

      public System.Type Extension => this._extension;

      public DefaultMapping(System.Type table, System.Type extension)
      {
        this._table = table;
        this._extension = extension;
      }
    }
  }

  internal class CacheStaticInfoDictionary : 
    Dictionary<System.Type, PXCache.CacheStaticInfoBase>,
    IPXCompanyDependent
  {
  }

  protected internal sealed class EventsRow
  {
    internal List<PXRowSelecting> _RowSelectingWhileReadingList = new List<PXRowSelecting>();
    private PXRowSelecting _RowSelectingWhileReadingDelegate;
    internal List<PXRowSelecting> _RowSelectingList = new List<PXRowSelecting>();
    private PXRowSelecting _RowSelectingDelegate;
    internal List<PXRowSelected> _RowSelectedList = new List<PXRowSelected>();
    private PXRowSelected _RowSelectedDelegate;
    internal List<PXRowInserting> _RowInsertingList = new List<PXRowInserting>();
    private PXRowInserting _RowInsertingDelegate;
    internal List<PXRowInserted> _RowInsertedList = new List<PXRowInserted>();
    private PXRowInserted _RowInsertedDelegate;
    internal List<PXRowUpdating> _RowUpdatingList = new List<PXRowUpdating>();
    private PXRowUpdating _RowUpdatingDelegate;
    internal List<PXRowUpdated> _RowUpdatedList = new List<PXRowUpdated>();
    private PXRowUpdated _RowUpdatedDelegate;
    internal List<PXRowDeleting> _RowDeletingList = new List<PXRowDeleting>();
    private PXRowDeleting _RowDeletingDelegate;
    internal List<PXRowDeleted> _RowDeletedList = new List<PXRowDeleted>();
    private PXRowDeleted _RowDeletedDelegate;
    internal List<PXRowPersisting> _RowPersistingList = new List<PXRowPersisting>();
    private PXRowPersisting _RowPersistingDelegate;
    internal List<PXRowPersisted> _RowPersistedList = new List<PXRowPersisted>();
    private PXRowPersisted _RowPersistedDelegate;

    public PXRowSelecting RowSelectingWhileReading
    {
      get
      {
        if (this._RowSelectingWhileReadingList != null && this._RowSelectingWhileReadingList.Count > 0)
        {
          this._RowSelectingWhileReadingDelegate = (PXRowSelecting) Delegate.Combine((Delegate[]) this._RowSelectingWhileReadingList.ToArray());
          this._RowSelectingWhileReadingList = (List<PXRowSelecting>) null;
        }
        return this._RowSelectingWhileReadingDelegate;
      }
      set => this._RowSelectingWhileReadingDelegate = value;
    }

    public PXRowSelecting RowSelecting
    {
      get
      {
        if (this._RowSelectingList != null && this._RowSelectingList.Count > 0)
        {
          this._RowSelectingDelegate = (PXRowSelecting) Delegate.Combine((Delegate[]) this._RowSelectingList.ToArray());
          this._RowSelectingList = (List<PXRowSelecting>) null;
        }
        return this._RowSelectingDelegate;
      }
      set => this._RowSelectingDelegate = value;
    }

    public PXRowSelected RowSelected
    {
      get
      {
        if (this._RowSelectedList != null && this._RowSelectedList.Count > 0)
        {
          this._RowSelectedDelegate = (PXRowSelected) Delegate.Combine((Delegate[]) this._RowSelectedList.ToArray());
          this._RowSelectedList = (List<PXRowSelected>) null;
        }
        return this._RowSelectedDelegate;
      }
      set => this._RowSelectedDelegate = value;
    }

    public PXRowInserting RowInserting
    {
      get
      {
        if (this._RowInsertingList != null && this._RowInsertingList.Count > 0)
        {
          this._RowInsertingDelegate = (PXRowInserting) Delegate.Combine((Delegate[]) this._RowInsertingList.ToArray());
          this._RowInsertingList = (List<PXRowInserting>) null;
        }
        return this._RowInsertingDelegate;
      }
      set => this._RowInsertingDelegate = value;
    }

    public PXRowInserted RowInserted
    {
      get
      {
        if (this._RowInsertedList != null && this._RowInsertedList.Count > 0)
        {
          this._RowInsertedDelegate = (PXRowInserted) Delegate.Combine((Delegate[]) this._RowInsertedList.ToArray());
          this._RowInsertedList = (List<PXRowInserted>) null;
        }
        return this._RowInsertedDelegate;
      }
      set => this._RowInsertedDelegate = value;
    }

    public PXRowUpdating RowUpdating
    {
      get
      {
        if (this._RowUpdatingList != null && this._RowUpdatingList.Count > 0)
        {
          this._RowUpdatingDelegate = (PXRowUpdating) Delegate.Combine((Delegate[]) this._RowUpdatingList.ToArray());
          this._RowUpdatingList = (List<PXRowUpdating>) null;
        }
        return this._RowUpdatingDelegate;
      }
      set => this._RowUpdatingDelegate = value;
    }

    public PXRowUpdated RowUpdated
    {
      get
      {
        if (this._RowUpdatedList != null && this._RowUpdatedList.Count > 0)
        {
          this._RowUpdatedDelegate = (PXRowUpdated) Delegate.Combine((Delegate[]) this._RowUpdatedList.ToArray());
          this._RowUpdatedList = (List<PXRowUpdated>) null;
        }
        return this._RowUpdatedDelegate;
      }
      set => this._RowUpdatedDelegate = value;
    }

    public PXRowDeleting RowDeleting
    {
      get
      {
        if (this._RowDeletingList != null && this._RowDeletingList.Count > 0)
        {
          this._RowDeletingDelegate = (PXRowDeleting) Delegate.Combine((Delegate[]) this._RowDeletingList.ToArray());
          this._RowDeletingList = (List<PXRowDeleting>) null;
        }
        return this._RowDeletingDelegate;
      }
      set => this._RowDeletingDelegate = value;
    }

    public PXRowDeleted RowDeleted
    {
      get
      {
        if (this._RowDeletedList != null && this._RowDeletedList.Count > 0)
        {
          this._RowDeletedDelegate = (PXRowDeleted) Delegate.Combine((Delegate[]) this._RowDeletedList.ToArray());
          this._RowDeletedList = (List<PXRowDeleted>) null;
        }
        return this._RowDeletedDelegate;
      }
      set => this._RowDeletedDelegate = value;
    }

    public PXRowPersisting RowPersisting
    {
      get
      {
        if (this._RowPersistingList != null && this._RowPersistingList.Count > 0)
        {
          this._RowPersistingDelegate = (PXRowPersisting) Delegate.Combine((Delegate[]) this._RowPersistingList.ToArray());
          this._RowPersistingList = (List<PXRowPersisting>) null;
        }
        return this._RowPersistingDelegate;
      }
      set => this._RowPersistingDelegate = value;
    }

    public PXRowPersisted RowPersisted
    {
      get
      {
        if (this._RowPersistedList != null && this._RowPersistedList.Count > 0)
        {
          this._RowPersistedDelegate = (PXRowPersisted) Delegate.Combine((Delegate[]) this._RowPersistedList.ToArray());
          this._RowPersistedList = (List<PXRowPersisted>) null;
        }
        return this._RowPersistedDelegate;
      }
      set => this._RowPersistedDelegate = value;
    }
  }

  protected internal sealed class EventsRowAttr
  {
    public IPXRowSelectingSubscriber[] RowSelecting;
    public IPXRowSelectedSubscriber[] RowSelected;
    public IPXRowInsertingSubscriber[] RowInserting;
    public IPXRowInsertedSubscriber[] RowInserted;
    public IPXRowUpdatingSubscriber[] RowUpdating;
    public IPXRowUpdatedSubscriber[] RowUpdated;
    public IPXRowDeletingSubscriber[] RowDeleting;
    public IPXRowDeletedSubscriber[] RowDeleted;
    public IPXRowPersistingSubscriber[] RowPersisting;
    public IPXRowPersistedSubscriber[] RowPersisted;
  }

  protected internal sealed class EventsFieldMap
  {
    public List<IPXCommandPreparingSubscriber> CommandPreparing = new List<IPXCommandPreparingSubscriber>();
    public List<IPXFieldDefaultingSubscriber> FieldDefaulting = new List<IPXFieldDefaultingSubscriber>();
    public List<IPXFieldUpdatingSubscriber> FieldUpdating = new List<IPXFieldUpdatingSubscriber>();
    public List<IPXFieldVerifyingSubscriber> FieldVerifying = new List<IPXFieldVerifyingSubscriber>();
    public List<IPXFieldUpdatedSubscriber> FieldUpdated = new List<IPXFieldUpdatedSubscriber>();
    public List<IPXFieldSelectingSubscriber> FieldSelecting = new List<IPXFieldSelectingSubscriber>();
    public List<IPXExceptionHandlingSubscriber> ExceptionHandling = new List<IPXExceptionHandlingSubscriber>();
  }

  internal class EventPosition
  {
    public int RowSelectingFirst = -1;
    public int RowSelectingLength;
    public int RowSelectedFirst = -1;
    public int RowSelectedLength;
    public int RowInsertingFirst = -1;
    public int RowInsertingLength;
    public int RowInsertedFirst = -1;
    public int RowInsertedLength;
    public int RowUpdatingFirst = -1;
    public int RowUpdatingLength;
    public int RowUpdatedFirst = -1;
    public int RowUpdatedLength;
    public int RowDeletingFirst = -1;
    public int RowDeletingLength;
    public int RowDeletedFirst = -1;
    public int RowDeletedLength;
    public int RowPersistingFirst = -1;
    public int RowPersistingLength;
    public int RowPersistedFirst = -1;
    public int RowPersistedLength;
    public int CommandPreparingFirst = -1;
    public int CommandPreparingLength;
  }

  internal delegate void _CacheAttachedDelegate(PXGraph graph, PXCache sender);

  public class DACFieldDescriptor
  {
    public PropertyInfo Property;
    public System.Type BqlField;
    public System.Type TargetBqlField;
    public string ViewName;
    public bool IsSameView;

    public System.Type DeclaringType
    {
      get
      {
        return this.Property != (PropertyInfo) null ? this.Property.DeclaringType : this.BqlField.DeclaringType;
      }
    }

    public string Name
    {
      get => this.Property != (PropertyInfo) null ? this.Property.Name : this.BqlField.Name;
    }
  }

  public class fieldContainer
  {
    public System.Type Field;
    public PXCache.fieldContainer[] Children;
  }

  /// <exclude />
  internal delegate bool FieldDefaultingDelegate(
    PXCache sender,
    string name,
    ref object defaultValue,
    bool rowSpecific);

  internal delegate bool FieldOfRowDefaultingDelegate(
    PXCache sender,
    string name,
    object row,
    ref object defaultValue);

  /// <exclude />
  internal delegate void FieldSelectingDelegate(string name, ref object returnState, object row);

  /// <exclude />
  [PXInternalUseOnly]
  public sealed class EventDictionary<T> : Dictionary<
  #nullable enable
  string, T>
  {
    public EventDictionary(int count)
      : base(count, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    public EventDictionary()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    public new T? this[string key]
    {
      get
      {
        if (!this.ContainsKey(key))
          base[key] = default (T);
        return base[key];
      }
      set
      {
        base[key] = value;
        if ((object) value != null)
          return;
        this.Remove(key);
      }
    }
  }

  private readonly struct AttributeHandlersSquasher<TAttributeHandler> where TAttributeHandler : 
  #nullable disable
  class
  {
    private readonly TAttributeHandler[] _attributeHandlers;

    internal AttributeHandlersSquasher(TAttributeHandler[] attributeHandlers)
    {
      this._attributeHandlers = attributeHandlers;
    }

    public Action<PXCache, TArgs> To<TArgs>(
      Action<TAttributeHandler, PXCache, TArgs> handlerInvoke,
      object row)
    {
      if (this._attributeHandlers == null)
        return (Action<PXCache, TArgs>) null;
      TAttributeHandler[] attributeHandlers = this._attributeHandlers;
      return (Action<PXCache, TArgs>) ((cache, args) =>
      {
        PXCache.DirtyItemState itemState = cache.GetItemState(row);
        for (int index = 0; index < attributeHandlers.Length; ++index)
          handlerInvoke(cache.GetDirtyAttribute<TAttributeHandler>(itemState, attributeHandlers[index]), cache, args);
        cache.CompressItemState(itemState);
      });
    }
  }

  private readonly struct AttributeRowSelectingHandlersSquasher
  {
    private readonly IPXRowSelectingSubscriber[] _attributeHandlers;

    internal AttributeRowSelectingHandlersSquasher(IPXRowSelectingSubscriber[] attributeHandlers)
    {
      this._attributeHandlers = attributeHandlers;
    }

    public Action<PXCache, PXRowSelectingEventArgs> ToRowSelecting(
      object item,
      PXDataRecord record,
      bool isReadOnly)
    {
      if (this._attributeHandlers == null)
        return (Action<PXCache, PXRowSelectingEventArgs>) null;
      IPXRowSelectingSubscriber[] attributeHandlers = this._attributeHandlers;
      return (Action<PXCache, PXRowSelectingEventArgs>) ((cache, args) =>
      {
        ISqlDialect sqlDialect = cache.Graph.SqlDialect;
        PXCache.DirtyItemState itemState = cache.GetItemState(item);
        PXRowSelectingEventArgs e = (PXRowSelectingEventArgs) null;
        for (int index = 0; index < attributeHandlers.Length; ++index)
        {
          try
          {
            HashSet<int> valueStoredOrdinals = cache._KeyValueStoredOrdinals;
            // ISSUE: explicit non-virtual call
            if ((valueStoredOrdinals != null ? (__nonvirtual (valueStoredOrdinals.Contains(index)) ? 1 : 0) : 0) != 0)
            {
              if (cache._FirstKeyValueStored.Value.Value == index)
              {
                string str = record.GetString(args.Position);
                ++args.Position;
                string[] attributes;
                if (sqlDialect.tryExtractAttributes(str, (IDictionary<string, int>) cache._KeyValueStoredNames, out attributes))
                {
                  e = new PXRowSelectingEventArgs(item, (PXDataRecord) new PXDummyDataRecord(attributes), 0, isReadOnly);
                  tryExtractAndStoreKvAttributes(str);
                }
              }
              if (e != null)
              {
                cache.GetDirtyAttribute<IPXRowSelectingSubscriber>(itemState, attributeHandlers[index]).RowSelecting(cache, e);
                e.Row = item;
              }
            }
            else
            {
              cache.GetDirtyAttribute<IPXRowSelectingSubscriber>(itemState, attributeHandlers[index]).RowSelecting(cache, args);
              if (cache._KeyValueStoredOrdinals == null)
              {
                if (cache._KeyValueAttributeNames != null)
                {
                  int? nullable1 = attributeHandlers[index] is PXEventSubscriberAttribute subscriberAttribute4 ? new int?(subscriberAttribute4.FieldOrdinal) : new int?();
                  int? noteIdOrdinal1 = cache._NoteIDOrdinal;
                  if (nullable1.GetValueOrDefault() == noteIdOrdinal1.GetValueOrDefault() & nullable1.HasValue == noteIdOrdinal1.HasValue)
                  {
                    if (index < attributeHandlers.Length - 1)
                    {
                      int? nullable2 = attributeHandlers[index + 1] is PXEventSubscriberAttribute subscriberAttribute5 ? new int?(subscriberAttribute5.FieldOrdinal) : new int?();
                      int? noteIdOrdinal2 = cache._NoteIDOrdinal;
                      if (nullable2.GetValueOrDefault() == noteIdOrdinal2.GetValueOrDefault() & nullable2.HasValue == noteIdOrdinal2.HasValue)
                        continue;
                    }
                    string attrs = record.GetString(args.Position);
                    ++args.Position;
                    tryExtractAndStoreKvAttributes(attrs);
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            if (attributeHandlers[index] is PXEventSubscriberAttribute subscriberAttribute6)
            {
              string fieldName = subscriberAttribute6.FieldName;
              string displayName = PXUIFieldAttribute.GetDisplayName(cache, fieldName);
              throw new PXException(ex, "An error occurred during processing of the field {0}: {1}.", new object[2]
              {
                (object) (displayName ?? fieldName),
                (object) ex.Message
              });
            }
            throw;
          }
        }
        cache.CompressItemState(itemState);

        void tryExtractAndStoreKvAttributes(string attrs)
        {
          string[] attributes;
          if (string.IsNullOrEmpty(attrs) || cache._KeyValueAttributeNames == null || !sqlDialect.tryExtractAttributes(attrs, (IDictionary<string, int>) cache._KeyValueAttributeNames, out attributes))
            return;
          cache.SetSlot<object[]>(item, cache._KeyValueAttributeSlotPosition, cache.convertAttributesFromString(attributes), true);
        }
      });
    }
  }

  /// <exclude />
  protected internal sealed class EventsRowMap
  {
    public List<IPXRowSelectingSubscriber> RowSelecting;
    public List<IPXRowSelectedSubscriber> RowSelected;
    public List<IPXRowInsertingSubscriber> RowInserting;
    public List<IPXRowInsertedSubscriber> RowInserted;
    public List<IPXRowUpdatingSubscriber> RowUpdating;
    public List<IPXRowUpdatedSubscriber> RowUpdated;
    public List<IPXRowDeletingSubscriber> RowDeleting;
    public List<IPXRowDeletedSubscriber> RowDeleted;
    public List<IPXRowPersistingSubscriber> RowPersisting;
    public List<IPXRowPersistedSubscriber> RowPersisted;

    public EventsRowMap()
    {
      this.RowSelecting = new List<IPXRowSelectingSubscriber>();
      this.RowSelected = new List<IPXRowSelectedSubscriber>();
      this.RowInserting = new List<IPXRowInsertingSubscriber>();
      this.RowInserted = new List<IPXRowInsertedSubscriber>();
      this.RowUpdating = new List<IPXRowUpdatingSubscriber>();
      this.RowUpdated = new List<IPXRowUpdatedSubscriber>();
      this.RowDeleting = new List<IPXRowDeletingSubscriber>();
      this.RowDeleted = new List<IPXRowDeletedSubscriber>();
      this.RowPersisting = new List<IPXRowPersistingSubscriber>();
      this.RowPersisted = new List<IPXRowPersistedSubscriber>();
    }

    public EventsRowMap(PXCache.EventsRowMap map)
    {
      this.RowSelecting = new List<IPXRowSelectingSubscriber>((IEnumerable<IPXRowSelectingSubscriber>) map.RowSelecting);
      this.RowSelected = new List<IPXRowSelectedSubscriber>((IEnumerable<IPXRowSelectedSubscriber>) map.RowSelected);
      this.RowInserting = new List<IPXRowInsertingSubscriber>((IEnumerable<IPXRowInsertingSubscriber>) map.RowInserting);
      this.RowInserted = new List<IPXRowInsertedSubscriber>((IEnumerable<IPXRowInsertedSubscriber>) map.RowInserted);
      this.RowUpdating = new List<IPXRowUpdatingSubscriber>((IEnumerable<IPXRowUpdatingSubscriber>) map.RowUpdating);
      this.RowUpdated = new List<IPXRowUpdatedSubscriber>((IEnumerable<IPXRowUpdatedSubscriber>) map.RowUpdated);
      this.RowDeleting = new List<IPXRowDeletingSubscriber>((IEnumerable<IPXRowDeletingSubscriber>) map.RowDeleting);
      this.RowDeleted = new List<IPXRowDeletedSubscriber>((IEnumerable<IPXRowDeletedSubscriber>) map.RowDeleted);
      this.RowPersisting = new List<IPXRowPersistingSubscriber>((IEnumerable<IPXRowPersistingSubscriber>) map.RowPersisting);
      this.RowPersisted = new List<IPXRowPersistedSubscriber>((IEnumerable<IPXRowPersistedSubscriber>) map.RowPersisted);
    }
  }

  /// <exclude />
  internal sealed class AlteredSource
  {
    public readonly PXEventSubscriberAttribute[] Attributes;
    public readonly PXCache._CacheAttachedDelegate Method;
    public readonly System.Type CacheType;
    public readonly HashSet<string> Fields;

    public AlteredSource(
      PXEventSubscriberAttribute[] attributes,
      HashSet<string> fields,
      PXCache._CacheAttachedDelegate method,
      System.Type cacheType)
    {
      this.Attributes = attributes;
      this.Method = method;
      this.CacheType = cacheType;
      this.Fields = fields;
    }
  }

  /// <exclude />
  internal sealed class AlteredDescriptor
  {
    public List<PXEventSubscriberAttribute> _FieldAttributes;
    public PXCache.EventsRowMap _EventsRowMap;
    public Dictionary<string, PXCache.EventsFieldMap> _EventsFieldMap;
    public KeyValuePair<string, int>? _FirstKeyValueStored;
    public HashSet<int> _KeyValueStoredOrdinals;
    public int[] _FieldAttributesFirst;
    public int[] _FieldAttributesLast;
    public PXCache._CacheAttachedDelegate _Method;
    public readonly HashSet<string> Fields;
    public readonly System.Type[] _ExtensionTypes;

    public AlteredDescriptor(
      PXEventSubscriberAttribute[] attributes,
      HashSet<string> fields,
      PXCache._CacheAttachedDelegate method,
      System.Type cacheType)
    {
      this.Fields = fields;
      this._Method = method;
      cacheType = typeof (PXCache<>).MakeGenericType(cacheType);
      PXCache.CacheStaticInfoBase cacheStaticInfoBase = (PXCache.CacheStaticInfoBase) cacheType.GetMethod("_Initialize", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, new object[1]
      {
        (object) false
      });
      Dictionary<string, int> fieldsMap = cacheStaticInfoBase._FieldsMap;
      this._FieldAttributes = new List<PXEventSubscriberAttribute>((IEnumerable<PXEventSubscriberAttribute>) cacheStaticInfoBase._FieldAttributes);
      this._EventsRowMap = new PXCache.EventsRowMap(cacheStaticInfoBase._EventsRowMap);
      this._EventsFieldMap = new Dictionary<string, PXCache.EventsFieldMap>((IDictionary<string, PXCache.EventsFieldMap>) cacheStaticInfoBase._EventsFieldMap);
      this._FieldAttributesFirst = (int[]) cacheStaticInfoBase._FieldAttributesFirst.Clone();
      this._FieldAttributesLast = (int[]) cacheStaticInfoBase._FieldAttributesLast.Clone();
      this._FirstKeyValueStored = cacheStaticInfoBase._FirstKeyValueStored;
      this._KeyValueStoredOrdinals = cacheStaticInfoBase._KeyValueStoredOrdinals;
      PXCache.EventPosition[] eventPositions = cacheStaticInfoBase._EventPositions;
      List<string> classFields = cacheStaticInfoBase._ClassFields;
      this._ExtensionTypes = cacheStaticInfoBase._ExtensionTypes;
      Dictionary<string, int> order = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      for (int index = 0; index < classFields.Count; ++index)
        order[classFields[index]] = index;
      string a1 = (string) null;
      int index1 = -1;
      int index2 = -1;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11;
      attributes = ((IEnumerable<PXEventSubscriberAttribute>) attributes).OrderBy<PXEventSubscriberAttribute, int>((Func<PXEventSubscriberAttribute, int>) (a => order.TryGetValue(a.FieldName, out num11) ? num11 : -1)).ToArray<PXEventSubscriberAttribute>();
      foreach (PXEventSubscriberAttribute attribute in attributes)
      {
        string lower = attribute.FieldName.ToLower();
        KeyValuePair<string, int> keyValuePair;
        if (!string.Equals(a1, attribute.FieldName, StringComparison.OrdinalIgnoreCase))
        {
          if (a1 != null)
          {
            int num12 = index1 - this._FieldAttributesLast[index2] - 1;
            if (num12 != 0)
            {
              for (int index3 = 0; index3 < this._FieldAttributesFirst.Length; ++index3)
              {
                if (index3 != index2 && this._FieldAttributesFirst[index3] > this._FieldAttributesFirst[index2])
                  this._FieldAttributesFirst[index3] += num12;
                if (this._FieldAttributesLast[index3] >= this._FieldAttributesFirst[index2])
                  this._FieldAttributesLast[index3] += num12;
              }
            }
          }
          if (fieldsMap.TryGetValue(attribute.FieldName, out index2))
          {
            if (attribute.FieldOrdinal == -1)
              attribute.FieldOrdinal = index2;
            this._FieldAttributes.RemoveRange(this._FieldAttributesFirst[index2], this._FieldAttributesLast[index2] - this._FieldAttributesFirst[index2] + 1);
            a1 = attribute.FieldName;
            index1 = this._FieldAttributesFirst[index2];
            this._EventsFieldMap[lower] = new PXCache.EventsFieldMap();
            PXCache.EventPosition position = eventPositions[index2];
            if (position.RowSelectingLength > 0)
            {
              this._EventsRowMap.RowSelecting.RemoveRange(position.RowSelectingFirst + num1, position.RowSelectingLength);
              if (this._FirstKeyValueStored.HasValue)
              {
                int num13 = position.RowSelectingFirst + num1;
                keyValuePair = this._FirstKeyValueStored.Value;
                int num14 = keyValuePair.Value;
                if (num13 <= num14)
                {
                  if (this._KeyValueStoredOrdinals != null)
                    this._KeyValueStoredOrdinals = new HashSet<int>(this._KeyValueStoredOrdinals.Select<int, int>((Func<int, int>) (_ => _ - position.RowSelectingLength)));
                  keyValuePair = this._FirstKeyValueStored.Value;
                  string key = keyValuePair.Key;
                  keyValuePair = this._FirstKeyValueStored.Value;
                  int num15 = keyValuePair.Value - position.RowSelectingLength;
                  this._FirstKeyValueStored = new KeyValuePair<string, int>?(new KeyValuePair<string, int>(key, num15));
                }
              }
              num1 -= position.RowSelectingLength;
            }
            if (position.RowSelectedLength > 0)
            {
              this._EventsRowMap.RowSelected.RemoveRange(position.RowSelectedFirst + num2, position.RowSelectedLength);
              num2 -= position.RowSelectedLength;
            }
            if (position.RowInsertingLength > 0)
            {
              this._EventsRowMap.RowInserting.RemoveRange(position.RowInsertingFirst + num3, position.RowInsertingLength);
              num3 -= position.RowInsertingLength;
            }
            if (position.RowInsertedLength > 0)
            {
              this._EventsRowMap.RowInserted.RemoveRange(position.RowInsertedFirst + num4, position.RowInsertedLength);
              num4 -= position.RowInsertedLength;
            }
            if (position.RowUpdatingLength > 0)
            {
              this._EventsRowMap.RowUpdating.RemoveRange(position.RowUpdatingFirst + num5, position.RowUpdatingLength);
              num5 -= position.RowUpdatingLength;
            }
            if (position.RowUpdatedLength > 0)
            {
              this._EventsRowMap.RowUpdated.RemoveRange(position.RowUpdatedFirst + num6, position.RowUpdatedLength);
              num6 -= position.RowUpdatedLength;
            }
            if (position.RowDeletingLength > 0)
            {
              this._EventsRowMap.RowDeleting.RemoveRange(position.RowDeletingFirst + num7, position.RowDeletingLength);
              num7 -= position.RowDeletingLength;
            }
            if (position.RowDeletedLength > 0)
            {
              this._EventsRowMap.RowDeleted.RemoveRange(position.RowDeletedFirst + num8, position.RowDeletedLength);
              num8 -= position.RowDeletedLength;
            }
            if (position.RowPersistingLength > 0)
            {
              this._EventsRowMap.RowPersisting.RemoveRange(position.RowPersistingFirst + num9, position.RowPersistingLength);
              num9 -= position.RowPersistingLength;
            }
            if (position.RowPersistedLength > 0)
            {
              this._EventsRowMap.RowPersisted.RemoveRange(position.RowPersistedFirst + num10, position.RowPersistedLength);
              num10 -= position.RowPersistedLength;
            }
          }
          else
          {
            a1 = (string) null;
            continue;
          }
        }
        else if (attribute.FieldOrdinal == -1)
          attribute.FieldOrdinal = index2;
        this._FieldAttributes.Insert(index1, attribute);
        PXCache.EventPosition eventPosition = eventPositions[index2];
        List<IPXRowSelectingSubscriber> rowSelecting = new List<IPXRowSelectingSubscriber>();
        attribute.GetSubscriber<IPXRowSelectingSubscriber>(rowSelecting);
        if (rowSelecting.Count > 0)
        {
          this._EventsRowMap.RowSelecting.InsertRange(eventPosition.RowSelectingFirst + num1 + eventPosition.RowSelectingLength, (IEnumerable<IPXRowSelectingSubscriber>) rowSelecting);
          if (this._FirstKeyValueStored.HasValue)
          {
            int num16 = eventPosition.RowSelectingFirst + num1;
            keyValuePair = this._FirstKeyValueStored.Value;
            int num17 = keyValuePair.Value;
            if (num16 <= num17)
            {
              if (this._KeyValueStoredOrdinals != null)
                this._KeyValueStoredOrdinals = new HashSet<int>(this._KeyValueStoredOrdinals.Select<int, int>((Func<int, int>) (_ => _ + rowSelecting.Count)));
              keyValuePair = this._FirstKeyValueStored.Value;
              string key = keyValuePair.Key;
              keyValuePair = this._FirstKeyValueStored.Value;
              int num18 = keyValuePair.Value + rowSelecting.Count;
              this._FirstKeyValueStored = new KeyValuePair<string, int>?(new KeyValuePair<string, int>(key, num18));
            }
          }
          num1 += rowSelecting.Count;
        }
        List<IPXRowSelectedSubscriber> selectedSubscriberList = new List<IPXRowSelectedSubscriber>();
        attribute.GetSubscriber<IPXRowSelectedSubscriber>(selectedSubscriberList);
        if (selectedSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowSelected.InsertRange(eventPosition.RowSelectedFirst + num2 + eventPosition.RowSelectedLength, (IEnumerable<IPXRowSelectedSubscriber>) selectedSubscriberList);
          num2 += selectedSubscriberList.Count;
        }
        List<IPXRowInsertingSubscriber> insertingSubscriberList = new List<IPXRowInsertingSubscriber>();
        attribute.GetSubscriber<IPXRowInsertingSubscriber>(insertingSubscriberList);
        if (insertingSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowInserting.InsertRange(eventPosition.RowInsertingFirst + num3 + eventPosition.RowInsertingLength, (IEnumerable<IPXRowInsertingSubscriber>) insertingSubscriberList);
          num3 += insertingSubscriberList.Count;
        }
        List<IPXRowInsertedSubscriber> insertedSubscriberList = new List<IPXRowInsertedSubscriber>();
        attribute.GetSubscriber<IPXRowInsertedSubscriber>(insertedSubscriberList);
        if (insertedSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowInserted.InsertRange(eventPosition.RowInsertedFirst + num4 + eventPosition.RowInsertedLength, (IEnumerable<IPXRowInsertedSubscriber>) insertedSubscriberList);
          num4 += insertedSubscriberList.Count;
        }
        List<IPXRowUpdatingSubscriber> updatingSubscriberList = new List<IPXRowUpdatingSubscriber>();
        attribute.GetSubscriber<IPXRowUpdatingSubscriber>(updatingSubscriberList);
        if (updatingSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowUpdating.InsertRange(eventPosition.RowUpdatingFirst + num5 + eventPosition.RowUpdatingLength, (IEnumerable<IPXRowUpdatingSubscriber>) updatingSubscriberList);
          num5 += updatingSubscriberList.Count;
        }
        List<IPXRowUpdatedSubscriber> updatedSubscriberList = new List<IPXRowUpdatedSubscriber>();
        attribute.GetSubscriber<IPXRowUpdatedSubscriber>(updatedSubscriberList);
        if (updatedSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowUpdated.InsertRange(eventPosition.RowUpdatedFirst + num6 + eventPosition.RowUpdatedLength, (IEnumerable<IPXRowUpdatedSubscriber>) updatedSubscriberList);
          num6 += updatedSubscriberList.Count;
        }
        List<IPXRowDeletingSubscriber> deletingSubscriberList = new List<IPXRowDeletingSubscriber>();
        attribute.GetSubscriber<IPXRowDeletingSubscriber>(deletingSubscriberList);
        if (deletingSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowDeleting.InsertRange(eventPosition.RowDeletingFirst + num7 + eventPosition.RowDeletingLength, (IEnumerable<IPXRowDeletingSubscriber>) deletingSubscriberList);
          num7 += deletingSubscriberList.Count;
        }
        List<IPXRowDeletedSubscriber> deletedSubscriberList = new List<IPXRowDeletedSubscriber>();
        attribute.GetSubscriber<IPXRowDeletedSubscriber>(deletedSubscriberList);
        if (deletedSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowDeleted.InsertRange(eventPosition.RowDeletedFirst + num8 + eventPosition.RowDeletedLength, (IEnumerable<IPXRowDeletedSubscriber>) deletedSubscriberList);
          num8 += deletedSubscriberList.Count;
        }
        List<IPXRowPersistingSubscriber> persistingSubscriberList = new List<IPXRowPersistingSubscriber>();
        attribute.GetSubscriber<IPXRowPersistingSubscriber>(persistingSubscriberList);
        if (persistingSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowPersisting.InsertRange(eventPosition.RowPersistingFirst + num9 + eventPosition.RowPersistingLength, (IEnumerable<IPXRowPersistingSubscriber>) persistingSubscriberList);
          num9 += persistingSubscriberList.Count;
        }
        List<IPXRowPersistedSubscriber> persistedSubscriberList = new List<IPXRowPersistedSubscriber>();
        attribute.GetSubscriber<IPXRowPersistedSubscriber>(persistedSubscriberList);
        if (persistedSubscriberList.Count > 0)
        {
          this._EventsRowMap.RowPersisted.InsertRange(eventPosition.RowPersistedFirst + num10 + eventPosition.RowPersistedLength, (IEnumerable<IPXRowPersistedSubscriber>) persistedSubscriberList);
          num10 += persistedSubscriberList.Count;
        }
        PXCache.EventsFieldMap eventsField = this._EventsFieldMap[lower];
        attribute.GetSubscriber<IPXCommandPreparingSubscriber>(eventsField.CommandPreparing);
        attribute.GetSubscriber<IPXFieldDefaultingSubscriber>(eventsField.FieldDefaulting);
        attribute.GetSubscriber<IPXFieldUpdatingSubscriber>(eventsField.FieldUpdating);
        attribute.GetSubscriber<IPXFieldVerifyingSubscriber>(eventsField.FieldVerifying);
        attribute.GetSubscriber<IPXFieldUpdatedSubscriber>(eventsField.FieldUpdated);
        attribute.GetSubscriber<IPXExceptionHandlingSubscriber>(eventsField.ExceptionHandling);
        attribute.GetSubscriber<IPXFieldSelectingSubscriber>(eventsField.FieldSelecting);
        ++index1;
      }
      if (a1 == null)
        return;
      int num19 = index1 - this._FieldAttributesLast[index2] - 1;
      if (num19 == 0)
        return;
      for (int index4 = 0; index4 < this._FieldAttributesFirst.Length; ++index4)
      {
        if (index4 != index2 && this._FieldAttributesFirst[index4] > this._FieldAttributesFirst[index2])
          this._FieldAttributesFirst[index4] += num19;
        if (this._FieldAttributesLast[index4] >= this._FieldAttributesFirst[index2])
          this._FieldAttributesLast[index4] += num19;
      }
    }
  }

  protected internal class OrdinalFieldInfo
  {
    public System.Type DeclaringType { get; set; }

    public string TargetFieldName { get; set; }

    public string ViewName { get; set; }

    public string BaseFieldName { get; set; }
  }

  /// <exclude />
  protected internal class CacheStaticInfoBase
  {
    public List<System.Type> _ExtensionTables;
    public System.Type[] _ExtensionTypes;
    public readonly Dictionary<string, int> _FieldsMap = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    public readonly List<PXEventSubscriberAttribute> _FieldAttributes = new List<PXEventSubscriberAttribute>();
    public readonly PXCache.EventsRowMap _EventsRowMap = new PXCache.EventsRowMap();
    public readonly Dictionary<string, PXCache.EventsFieldMap> _EventsFieldMap = new Dictionary<string, PXCache.EventsFieldMap>();
    public int[] _FieldAttributesFirst;
    public int[] _FieldAttributesLast;
    internal PXCache.EventPosition[] _EventPositions;
    public readonly List<string> _ClassFields = new List<string>();
    public readonly Dictionary<string, int> _ReverseMap = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    public readonly List<System.Type> _FieldTypes = new List<System.Type>();
    public readonly Dictionary<System.Type, int> _BqlFieldsMap = new Dictionary<System.Type, int>();
    public readonly Dictionary<int, PXCache.OrdinalFieldInfo> _CachesByOrdinal = new Dictionary<int, PXCache.OrdinalFieldInfo>();
    public readonly Dictionary<int, System.Type> _InverseBqlFieldsMap = new Dictionary<int, System.Type>();
    public int? _TimestampOrdinal;
    public HashSet<int> _KeyValueStoredOrdinals;
    public Dictionary<string, int> _KeyValueStoredNames;
    public Dictionary<string, int> _DBLocalizableNames;
    public KeyValuePair<string, int>? _FirstKeyValueStored;
    public System.Type _BreakInheritanceType;
    public object _TypeInterceptorAttribute;
    public object _ExtensionInterceptorAttribute;
    public object[] _ClassAttributes;
  }
}
