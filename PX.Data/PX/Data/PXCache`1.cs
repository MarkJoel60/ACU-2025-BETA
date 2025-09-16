// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCache`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Session;
using PX.CS;
using PX.Data.BQL;
using PX.Data.DacDescriptorGeneration;
using PX.Data.Maintenance.GI;
using PX.Data.SQLTree;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web.Compilation;
using System.Xml;

#nullable enable
namespace PX.Data;

/// <summary>
/// Represents a cache that contains modified data records relating to a particular table
/// along with the controller that allows you to perform basic operations
/// on such records. A type parameter is set to a data access class (DAC) that represents this table.
/// </summary>
/// <typeparam name="TNode">The DAC type of data records stored in the cache object.</typeparam>
/// <remarks>
///   <para>Cache objects consist of two parts:</para>
///   <list type="bullet">
///     <item><description>Data records collection that were modified but have not been saved to the database, such
///     as <see cref="P:PX.Data.PXCache`1.Updated">Updated</see>, <see cref="P:PX.Data.PXCache`1.Inserted">Inserted</see>, <see cref="P:PX.Data.PXCache`1.Deleted">Deleted</see>, and <see cref="P:PX.Data.PXCache`1.Dirty">
///     Dirty</see>.</description></item>
///     <item><description>A controller that executes basic data-related operations through the use of the methods, such
///     as <see cref="M:PX.Data.PXCache`1.Update(PX.Data.PXGraph,`0)">Update()</see>, <see cref="M:PX.Data.PXCache`1.Insert(PX.Data.PXGraph,`0)">Insert()</see>, <see cref="M:PX.Data.PXCache`1.Delete(PX.Data.PXGraph,`0)">Delete()</see>, and <see cref="M:PX.Data.PXCache`1.Persist(PX.Data.PXDBOperation)">Persist()</see>.
///     </description></item>
///   </list>
///   <para>During execution of these methods, the cache object raises events. The graph as well as attributes can subscribe to these events to implement a business
/// logic. Each method is applied to a previously unchanged data record result in placing of the data record into the cache.</para>
///   <para>The system creates and destroys PXCache instances (caches) on each request. If the user or the code modifies a data record, it is placed into the cache.
/// When request execution is completed, the system serializes the modified records from the caches to the session. At run time, the cache may also include the
/// unchanged data records retrieved during request execution. These data records are discarded once the request is served.</para>
///   <para>On the next round trip, the modified data records are loaded from the session to the caches. The cache merges the data retrieved from the database with the
/// modified data, and the application accesses the data as if the entire data set has been preserved from the time of previous request.</para>
///   <para>The cache maintains the modified data until the changes are discarded or saved to the database.</para>
///   <para>The cache is the issuer of all data-related events, which can be handled by the graph and attributes.</para>
/// </remarks>
/// <example><para>The cache object that initiated the event is passed to every event handler as the first argument.</para>
/// <code title="Example" lang="CS">
/// protected virtual void Vendor_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
/// {
///     Vendor row = e.Row as Vendor;
///     ...
/// }</code>
/// </example>
[DebuggerTypeProxy(typeof (PXCache<>.PXCacheDebugView))]
public class PXCache<TNode> : PXCache, IPXDumpObjectState, PXCache<
#nullable disable
TNode>.IStronglyTypedRows where TNode : class, IBqlTable, new()
{
  private static PXCache<TNode>.memberwiseCloneDelegate memberwiseClone;
  private PXCache<TNode>.memberwiseCloneExtensionsDelegate _CloneExtensions;
  private HashSet<string> _setDefaultExtQueue;
  private Stack<HashSet<string>> _setDefaultExtQueueStack = new Stack<HashSet<string>>();
  private readonly PXCache<TNode>.CreateExtensionsDelegate _CreateExtensions;
  private List<System.Type> _ExtensionTables;
  private System.Type[] _ExtensionTypes;
  private PXCache<TNode>.SetValueByOrdinalDelegate _SetValueByOrdinal;
  private TNode _LastAccessedNode;
  private PXCacheExtension[] _LastAccessedExtensions;
  private PXCache<TNode>.GetValueByOrdinalDelegate _GetValueByOrdinal;
  protected Dictionary<TNode, object> _PendingValues;
  protected Dictionary<TNode, List<Exception>> _PendingExceptions;
  private BqlCommand _BqlSelect;
  private ReaderWriterLock _DelegatesLock;
  private PXCache<TNode>.EqualsDelegate _Equals;
  private Dictionary<string, PXCache<TNode>.EqualsDelegate> _DelegatesEquals;
  private PXCache<TNode>.GetHashCodeDelegate _GetHashCode;
  private Dictionary<string, PXCache<TNode>.GetHashCodeDelegate> _DelegatesGetHashCode;
  internal List<string> _ClassFields;
  private Dictionary<string, int> _ReverseMap = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  internal List<System.Type> _FieldTypes;
  private Dictionary<System.Type, int> _BqlFieldsMap;
  private Dictionary<int, System.Type> _InverseBqlFieldsMap;
  private static System.Type _BqlTable;
  private PXDBInterceptorAttribute _Interceptor;
  internal Func<TNode, Query, object[], PXResult<TNode>> _TailAppender;
  internal Func<TNode, Query, object[], bool> _MeetVerifier;
  internal Dictionary<string, Func<TNode, Query, object[], PXResult>> _SelectorGetter;
  internal List<PXEventSubscriberAttribute> _FieldAttributes;
  internal PXCache.EventsRowMap _EventsRowMap;
  internal Dictionary<string, PXCache.EventsFieldMap> _EventsFieldMap;
  internal int[] _FieldAttributesFirst;
  internal int[] _FieldAttributesLast;
  internal PXCache.EventPosition[] _EventPositions;
  private List<TNode> _PendingItems;
  private int[] _AttributesFirst;
  private int[] _AttributesLast;
  private PXCollection<TNode> _Items;
  private TNode _CurrentPlacedIntoCache;
  private bool _ItemsDenormalized;
  private readonly PXCacheExtensionCollection _Extensions;
  private int _OriginalsRequested;
  private int _OriginalsReadAhead;
  protected HashSet<string> _GraphSpecificFields;
  protected int? _TimestampOrdinal;
  protected bool? _NonDBTable;
  private Dictionary<TNode, bool> persistedItems;
  protected Dictionary<TNode, TNode> _ChangedKeys;
  private bool stateLoaded;

  internal override object _Clone(object item)
  {
    if (!(item is TNode))
      return (object) null;
    return PXCache<TNode>.memberwiseClone != null && this._CreateExtensions == null ? PXCache<TNode>.memberwiseClone((TNode) item) : (object) PXCache<TNode>.CreateCopy((TNode) item);
  }

  private void cloneRowSubscribers(Dictionary<object, object> clones, PXCache.EventsRowAttr list)
  {
    if (this._EventsRowAttr.RowSelecting != null)
    {
      list.RowSelecting = new IPXRowSelectingSubscriber[this._EventsRowAttr.RowSelecting.Length];
      for (int index = 0; index < this._EventsRowAttr.RowSelecting.Length; ++index)
        list.RowSelecting[index] = (IPXRowSelectingSubscriber) clones[(object) this._EventsRowAttr.RowSelecting[index]];
    }
    if (this._EventsRowAttr.RowSelected != null)
    {
      list.RowSelected = new IPXRowSelectedSubscriber[this._EventsRowAttr.RowSelected.Length];
      for (int index = 0; index < this._EventsRowAttr.RowSelected.Length; ++index)
        list.RowSelected[index] = (IPXRowSelectedSubscriber) clones[(object) this._EventsRowAttr.RowSelected[index]];
    }
    if (this._EventsRowAttr.RowInserting != null)
    {
      list.RowInserting = new IPXRowInsertingSubscriber[this._EventsRowAttr.RowInserting.Length];
      for (int index = 0; index < this._EventsRowAttr.RowInserting.Length; ++index)
        list.RowInserting[index] = (IPXRowInsertingSubscriber) clones[(object) this._EventsRowAttr.RowInserting[index]];
    }
    if (this._EventsRowAttr.RowInserted != null)
    {
      list.RowInserted = new IPXRowInsertedSubscriber[this._EventsRowAttr.RowInserted.Length];
      for (int index = 0; index < this._EventsRowAttr.RowInserted.Length; ++index)
        list.RowInserted[index] = (IPXRowInsertedSubscriber) clones[(object) this._EventsRowAttr.RowInserted[index]];
    }
    if (this._EventsRowAttr.RowUpdating != null)
    {
      list.RowUpdating = new IPXRowUpdatingSubscriber[this._EventsRowAttr.RowUpdating.Length];
      for (int index = 0; index < this._EventsRowAttr.RowUpdating.Length; ++index)
        list.RowUpdating[index] = (IPXRowUpdatingSubscriber) clones[(object) this._EventsRowAttr.RowUpdating[index]];
    }
    if (this._EventsRowAttr.RowUpdated != null)
    {
      list.RowUpdated = new IPXRowUpdatedSubscriber[this._EventsRowAttr.RowUpdated.Length];
      for (int index = 0; index < this._EventsRowAttr.RowUpdated.Length; ++index)
        list.RowUpdated[index] = (IPXRowUpdatedSubscriber) clones[(object) this._EventsRowAttr.RowUpdated[index]];
    }
    if (this._EventsRowAttr.RowDeleting != null)
    {
      list.RowDeleting = new IPXRowDeletingSubscriber[this._EventsRowAttr.RowDeleting.Length];
      for (int index = 0; index < this._EventsRowAttr.RowDeleting.Length; ++index)
        list.RowDeleting[index] = (IPXRowDeletingSubscriber) clones[(object) this._EventsRowAttr.RowDeleting[index]];
    }
    if (this._EventsRowAttr.RowDeleted != null)
    {
      list.RowDeleted = new IPXRowDeletedSubscriber[this._EventsRowAttr.RowDeleted.Length];
      for (int index = 0; index < this._EventsRowAttr.RowDeleted.Length; ++index)
        list.RowDeleted[index] = (IPXRowDeletedSubscriber) clones[(object) this._EventsRowAttr.RowDeleted[index]];
    }
    if (this._EventsRowAttr.RowPersisting != null)
    {
      list.RowPersisting = new IPXRowPersistingSubscriber[this._EventsRowAttr.RowPersisting.Length];
      for (int index = 0; index < this._EventsRowAttr.RowPersisting.Length; ++index)
        list.RowPersisting[index] = (IPXRowPersistingSubscriber) clones[(object) this._EventsRowAttr.RowPersisting[index]];
    }
    if (this._EventsRowAttr.RowPersisted == null)
      return;
    list.RowPersisted = new IPXRowPersistedSubscriber[this._EventsRowAttr.RowPersisted.Length];
    for (int index = 0; index < this._EventsRowAttr.RowPersisted.Length; ++index)
      list.RowPersisted[index] = (IPXRowPersistedSubscriber) clones[(object) this._EventsRowAttr.RowPersisted[index]];
  }

  private Dictionary<string, Subscriber[]> cloneFieldSubscribers<Subscriber>(
    Dictionary<object, object> clones,
    Dictionary<string, Subscriber[]> list)
    where Subscriber : class
  {
    Dictionary<string, Subscriber[]> dictionary = new Dictionary<string, Subscriber[]>(list.Count);
    foreach (KeyValuePair<string, Subscriber[]> keyValuePair in list)
    {
      List<Subscriber> subscriberList = new List<Subscriber>(keyValuePair.Value.Length);
      for (int index = 0; index < keyValuePair.Value.Length; ++index)
      {
        Subscriber key = keyValuePair.Value[index];
        subscriberList.Add((Subscriber) clones[(object) key]);
      }
      dictionary[keyValuePair.Key] = subscriberList.ToArray();
    }
    return dictionary;
  }

  private void AddAggregatedAttributes(
    ref Dictionary<object, object> clones,
    PXEventSubscriberAttribute attr,
    PXEventSubscriberAttribute clone)
  {
    clones.Add((object) attr, (object) clone);
    if (!(clone is PXAggregateAttribute))
      return;
    PXEventSubscriberAttribute[] aggregatedAttributes1 = ((PXAggregateAttribute) attr).GetAggregatedAttributes();
    PXEventSubscriberAttribute[] aggregatedAttributes2 = ((PXAggregateAttribute) clone).GetAggregatedAttributes();
    for (int index = 0; index < aggregatedAttributes1.Length; ++index)
      clones.Add((object) aggregatedAttributes1[index], (object) aggregatedAttributes2[index]);
  }

  /// <summary>Checks if the provided data record has any attributes
  /// attached to its fields.</summary>
  /// <param name="data">The data record.</param>
  public override bool HasAttributes(object data)
  {
    return this._ItemAttributes != null && data is TNode key && this._ItemAttributes.ContainsKey((object) key);
  }

  /// <summary>Returns the cache-level instances of attributes for the specified field.</summary>
  /// <param name="name">
  /// A field name, the attributes of which were returned.
  /// The method will return attributes from the entire field collection if <tt>null</tt>.
  /// </param>
  /// <remarks>The system maintains instances of attributes on three different
  /// levels. On its instantiation, a cache object copies appropriate
  /// attributes from the global level to the cache level and stores them in
  /// an internal collection. When an attribute needs to be modified for a
  /// particular data record, the cache creates item-level copies of all
  /// attributes and stores them associated with the data record.</remarks>
  public override List<PXEventSubscriberAttribute> GetAttributesReadonly(string name)
  {
    return this.GetAttributesReadonly(name, true);
  }

  /// <summary>Returns the cache-level instances of attributes for the specified field.</summary>
  /// <param name="name">The data record.</param>
  /// <param name="extractEmmbeddedAttr">The value that indicates whether
  /// the attributes embedded into an aggregate attribute are included into
  /// the list. If <tt>true</tt>, both the aggregate attribute and the
  /// attributes embedded into it are included in the list. Otherwise, only
  /// the aggregate attribute is included. An aggregate attribute is an
  /// attribute that derives from the <tt>PXAggregateAttribute</tt> class.
  /// This class allows combining multiple different attributes in a single
  /// one.</param>
  /// <remarks>Using this method, you can prevent expanding the aggregate
  /// attributes by setting the second parameter to <tt>false</tt>. Other
  /// overloads of this method always include both the aggregate attributes
  /// and the attributes that comprise such attributes.</remarks>
  public override List<PXEventSubscriberAttribute> GetAttributesReadonly(
    string name,
    bool extractEmmbeddedAttr)
  {
    List<PXEventSubscriberAttribute> list = new List<PXEventSubscriberAttribute>();
    if (name == null)
    {
      foreach (PXEventSubscriberAttribute cacheAttribute in this._CacheAttributes)
      {
        list.Add(cacheAttribute);
        if (extractEmmbeddedAttr)
          PXCache<TNode>.extractEmbeded(list, cacheAttribute);
      }
    }
    else
    {
      int index1;
      if (this._FieldsMap.TryGetValue(name, out index1))
      {
        for (int index2 = this._AttributesFirst[index1]; index2 <= this._AttributesLast[index1]; ++index2)
        {
          list.Add(this._CacheAttributes[index2]);
          if (extractEmmbeddedAttr)
            PXCache<TNode>.extractEmbeded(list, this._CacheAttributes[index2]);
        }
      }
    }
    if (this._MeasuringUpdatability)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        PXEventSubscriberAttribute subscriberAttribute = list[index];
        if (subscriberAttribute is PXUIFieldAttribute)
          list[index] = (PXEventSubscriberAttribute) new PXCache.MeasuringUIFieldAttribute((PXCache) this, subscriberAttribute.FieldName, subscriberAttribute.FieldOrdinal, subscriberAttribute.BqlTable);
      }
    }
    return list;
  }

  /// <summary>
  /// Returns the cache-level instances of attributes for the specified field.
  /// If there are no instances to be found, the cache-level instances will be returned.
  /// </summary>
  /// <param name="data">The data record.</param>
  /// <param name="name">
  /// A field name, the attributes of which were returned.
  /// The method will return attributes from the entire field collection if <tt>null</tt>.
  /// </param>
  /// <example>The following code snippet gets the attributes and places them into a list collection.
  /// <code title="Example" lang="CS">
  /// protected virtual void InventoryItem_ValMethod_FieldVerifying(
  ///     PXCache sender, PXFieldVerifyingEventArgs e)
  /// {
  ///     List&lt;PXEventSubscriberAttribute&gt; attrlist =
  ///         sender.GetAttributesReadonly(e.Row, "ValMethod");
  ///     ...
  /// }</code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override IEnumerable<PXEventSubscriberAttribute> GetAttributesReadonly(
    object data,
    string name)
  {
    if (data == null || this._ItemAttributes == null || !this._ItemAttributes.ContainsKey((object) (TNode) data))
      return (IEnumerable<PXEventSubscriberAttribute>) this.GetAttributesReadonly(name);
    return (IEnumerable<PXEventSubscriberAttribute>) new PXCache.DisposableAttributesList((PXCache) this, data)
    {
      NameFilter = name
    };
  }

  /// <summary>Returns item-level instances of attributes for the specified field.
  /// In case there are no instances for the provided data record(s), this method will create
  /// them simply by copying all cache-level attributes along with having them placed
  /// into the internal collection that contains specific data record attributes. To
  /// avoid cloning cache-level attributes, use the <see cref="M:PX.Data.PXCache`1.GetAttributesReadonly(System.Object,System.String)" /> method.</summary>
  /// <param name="data">The data record.</param>
  /// <param name="name">
  /// A field name, the attributes of which are used. The method will return attributes
  /// from the entire field collection if <tt>null</tt>.
  /// </param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override IEnumerable<PXEventSubscriberAttribute> GetAttributes(object data, string name)
  {
    return this.GetAttributes(data, name, false);
  }

  public override IEnumerable<T> GetAttributesOfType<T>(object data, string name)
  {
    return this.GetAttributes(data, name, false, typeof (T)).OfType<T>();
  }

  protected internal virtual IEnumerable<PXEventSubscriberAttribute> GetAttributes(
    object data,
    string name,
    bool forceNotCached,
    System.Type typeFilter = null)
  {
    if (data == null)
      return (IEnumerable<PXEventSubscriberAttribute>) this.GetAttributes(name);
    if (this.NeverCloneAttributes || this.Graph.UnattendedMode && this.Locate(data) == null && !forceNotCached && this._Current != data)
      return this.GetAttributesReadonly(data, name);
    if (this._ItemAttributes == null || !this._ItemAttributes.ContainsKey((object) (TNode) data))
    {
      if (this._ItemAttributes == null)
        this.InitItemAttributesCollection();
      if (this._PendingItems != null)
        this._PendingItems.Add((TNode) data);
      PXCache.DirtyItemState dirtyItemState = new PXCache.DirtyItemState()
      {
        BoundItem = data
      };
      this._ItemAttributes.Add((object) (TNode) data, dirtyItemState);
    }
    if (typeFilter == typeof (PXEventSubscriberAttribute))
      typeFilter = (System.Type) null;
    return (IEnumerable<PXEventSubscriberAttribute>) new PXCache.DisposableAttributesList((PXCache) this, data)
    {
      NameFilter = name,
      TypeFilter = typeFilter
    };
  }

  public override List<System.Type> GetExtensionTables() => this._ExtensionTables;

  public static System.Type[] GetExtensionTypesStatic()
  {
    return PXCache<TNode>._Initialize(false)._ExtensionTypes;
  }

  internal static IEnumerable<PXEventSubscriberAttribute> GetAttributesStatic()
  {
    List<PXEventSubscriberAttribute> list = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute fieldAttribute in PXCache<TNode>._Initialize(false)._FieldAttributes)
    {
      list.Add(fieldAttribute);
      PXCache<TNode>.extractEmbeded(list, fieldAttribute);
    }
    return (IEnumerable<PXEventSubscriberAttribute>) list;
  }

  private static void extractEmbeded(
    List<PXEventSubscriberAttribute> list,
    PXEventSubscriberAttribute attr)
  {
    if (!(attr is PXAggregateAttribute aggregateAttribute))
      return;
    list.AddRange((IEnumerable<PXEventSubscriberAttribute>) aggregateAttribute.GetAggregatedAttributes());
  }

  /// <summary>
  /// Returns the cach-level instances of attributes for the specified field
  /// along with the entire collection of item-level instances currently stored in cache.
  /// </summary>
  /// <param name="name">
  /// A field name, the attributes of which are used. The method will return attributes
  /// from the entire field collection if <tt>null</tt>.
  /// </param>
  public override List<PXEventSubscriberAttribute> GetAttributes(string name)
  {
    List<PXEventSubscriberAttribute> list = new List<PXEventSubscriberAttribute>();
    int index1 = -1;
    if (name == null)
    {
      foreach (PXEventSubscriberAttribute cacheAttribute in this._CacheAttributes)
      {
        list.Add(cacheAttribute);
        PXCache<TNode>.extractEmbeded(list, cacheAttribute);
      }
    }
    else if (this._FieldsMap.TryGetValue(name, out index1))
    {
      for (int index2 = this._AttributesFirst[index1]; index2 <= this._AttributesLast[index1]; ++index2)
      {
        list.Add(this._CacheAttributes[index2]);
        PXCache<TNode>.extractEmbeded(list, this._CacheAttributes[index2]);
      }
    }
    if (this._ItemAttributes != null)
    {
      int[] numArray = (int[]) null;
      int num = 0;
      if (name != null && this._FieldsMap.TryGetValue(name, out index1) && this._AttributesByName.Contains(name))
      {
        numArray = this._AttributesByName[name].Select<PXEventSubscriberAttribute, int>((Func<PXEventSubscriberAttribute, int>) (_ => _.IndexInClonesArray)).Where<int>((Func<int, bool>) (index => (this._UsableItemAttributes[index / 32 /*0x20*/] & 1 << index % 32 /*0x20*/) != 0)).ToArray<int>();
        num = numArray.Length;
        if (num == 0)
          return list;
      }
      foreach (PXCache.DirtyItemState dirtyItemState in this._ItemAttributes.Values)
      {
        if (dirtyItemState.DirtyAttributes != null)
        {
          if (name == null)
            list.AddRange(((IEnumerable<PXEventSubscriberAttribute>) dirtyItemState.DirtyAttributes).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (_ => _ != null)));
          else if (index1 >= 0)
          {
            if (numArray != null)
            {
              for (int index3 = 0; index3 < num; ++index3)
              {
                PXEventSubscriberAttribute dirtyAttribute = dirtyItemState.DirtyAttributes[numArray[index3]];
                if (dirtyAttribute != null)
                  list.Add(dirtyAttribute);
              }
            }
            else
              list.AddRange(((IEnumerable<PXEventSubscriberAttribute>) dirtyItemState.DirtyAttributes).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (_ => _ != null && _.FieldName.OrdinalEquals(name))));
          }
        }
      }
    }
    return list;
  }

  /// <summary>Sets the value of the field in the provided data record
  /// without raising events. The field is specified by its index in the
  /// field map.</summary>
  /// <remarks>To set the value, raising the field-related events, use the <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)">SetValueExt(object,
  /// string, object)</see> method.</remarks>
  /// <param name="data">The data record.</param>
  /// <param name="ordinal">The index of the field in the internally stored
  /// field map. To get the index of a specific field, use the <see cref="M:PX.Data.PXCache`1.GetFieldOrdinal(System.String)">GetFieldOrdinal(string)</see>
  /// method.</param>
  /// <param name="value">The value to set to the field.</param>
  public override void SetValue(object data, int ordinal, object value)
  {
    if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
      this.SetValueByOrdinal(this._LastAccessedNode, ordinal, value, this._LastAccessedExtensions);
    else if (data is TNode node)
    {
      this._LastAccessedNode = node;
      if (this._Extensions == null)
      {
        this.SetValueByOrdinal(node, ordinal, value, (PXCacheExtension[]) null);
      }
      else
      {
        PXCacheExtension[] extensions;
        lock (((ICollection) this._Extensions).SyncRoot)
        {
          if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
            this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
        }
        this._LastAccessedExtensions = extensions;
        this.SetValueByOrdinal(node, ordinal, value, extensions);
      }
    }
    else
    {
      if (ordinal >= this._ClassFields.Count || !(data is IDictionary dictionary))
        return;
      dictionary[(object) this._ClassFields[ordinal]] = value;
    }
  }

  /// <summary>Sets the value of the field in the provided data record
  /// without raising events.</summary>
  /// <remarks>
  /// To set the value, raising the field-related events, use the
  /// <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)" /> method.
  /// </remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field that is set to the value.
  /// The parameter is case-insensitive.</param>
  /// <param name="value">The value to set to the field.</param>
  /// <remarks>To set the value, raising the field-related events, use the <see cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)">SetValueExt(object, string, object)</see> method.</remarks>
  /// <example>
  /// <code title="" description="" lang="CS">
  /// public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  /// {
  ///     bool freeze = ((bool?)sender.GetValue(e.Row, sender.GetField(freezeDisc))) == true;
  /// 
  ///     if (!freeze &amp;&amp; !sender.Graph.IsImport)
  ///     {
  ///         IDiscountable row = (IDiscountable)e.Row;
  ///         if (row.CuryDiscAmt != null &amp;&amp; row.CuryDiscAmt != 0 &amp;&amp; row.CuryExtPrice != 0)
  ///         {
  ///             row.DiscPct = 100 * row.CuryDiscAmt / row.CuryExtPrice;
  ///             sender.SetValue(row, sender.GetField(curyTranAmt), row.CuryExtPrice - row.CuryDiscAmt);
  ///             sender.SetValue(e.Row, this.FieldName, true);
  ///         }
  ///         ...
  ///     }
  /// }</code>
  /// </example>
  public override void SetValue(object data, string fieldName, object value)
  {
    if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
    {
      if (this._FieldsMap.ContainsKey(fieldName))
        this.SetValueByOrdinal(this._LastAccessedNode, this._FieldsMap[fieldName], value, this._LastAccessedExtensions);
      else
        SetAdditionalFields();
    }
    else if (data is TNode node)
    {
      if (this._FieldsMap.ContainsKey(fieldName))
      {
        this._LastAccessedNode = node;
        if (this._Extensions == null)
        {
          this.SetValueByOrdinal(node, this._FieldsMap[fieldName], value, (PXCacheExtension[]) null);
        }
        else
        {
          PXCacheExtension[] extensions;
          lock (((ICollection) this._Extensions).SyncRoot)
          {
            if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
              this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
          }
          this._LastAccessedExtensions = extensions;
          this.SetValueByOrdinal(node, this._FieldsMap[fieldName], value, extensions);
        }
      }
      else
        SetAdditionalFields();
    }
    else
    {
      if (!(data is IDictionary dictionary))
        return;
      if (!dictionary.Contains((object) fieldName))
      {
        string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
        if (collectionKey != null)
          fieldName = collectionKey;
      }
      dictionary[(object) fieldName] = value;
    }

    void SetAdditionalFields()
    {
      if (string.Equals(fieldName, "DatabaseRecordStatus"))
      {
        bool flag1;
        switch (this.GetStatus(data))
        {
          case PXEntryStatus.Inserted:
          case PXEntryStatus.InsertedDeleted:
            flag1 = true;
            break;
          default:
            flag1 = false;
            break;
        }
        if (flag1)
          return;
        object data = data;
        bool flag2;
        int num1;
        if (value is bool)
        {
          flag2 = (bool) value;
          num1 = 1;
        }
        else
          num1 = 0;
        int num2 = flag2 ? 1 : 0;
        int num3 = num1 & num2;
        this.SetArchived(data, num3 != 0);
      }
      else
      {
        if (!string.Equals(fieldName, "DeletedDatabaseRecord"))
          return;
        bool flag3;
        switch (this.GetStatus(data))
        {
          case PXEntryStatus.Inserted:
          case PXEntryStatus.InsertedDeleted:
            flag3 = true;
            break;
          default:
            flag3 = false;
            break;
        }
        if (flag3)
          return;
        object data = data;
        bool flag4;
        int num4;
        if (value is bool)
        {
          flag4 = (bool) value;
          num4 = 1;
        }
        else
          num4 = 0;
        int num5 = flag4 ? 1 : 0;
        int num6 = num4 & num5;
        this.SetDeletedRecord(data, num6 != 0);
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected virtual object GetValueByOrdinal(
    TNode data,
    int ordinal,
    PXCacheExtension[] extensions)
  {
    return this._GetValueByOrdinal(data, ordinal, extensions);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected virtual void SetValueByOrdinal(
    TNode data,
    int ordinal,
    object value,
    PXCacheExtension[] extensions)
  {
    this._SetValueByOrdinal(data, ordinal, value, extensions);
  }

  internal override object GetCopy(object data) => this.GetCopy(data, out object _);

  protected virtual object GetCopy(object data, out object pending)
  {
    object copy = (object) null;
    pending = (object) null;
    if (this._PendingValues != null && data is TNode key1 && this._PendingValues.TryGetValue(key1, out pending))
    {
      foreach (TNode key in this._PendingValues.Keys)
      {
        if ((object) key != data && this._PendingValues[key] == pending)
        {
          copy = (object) key;
          break;
        }
      }
    }
    return copy;
  }

  /// <summary>Sets the default value to the field in the provided data
  /// record.</summary>
  /// <remarks>The method raises <tt>FieldDefaulting</tt>,
  /// <tt>FieldUpdating</tt>, <tt>FieldVerifying</tt>, and
  /// <tt>FieldUpdated</tt>.</remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field to set.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void SetDefaultExt(object data, string fieldName, object value = null)
  {
    if (!this.Fields.Contains(fieldName))
      return;
    bool flag = this._PendingValues != null && data is TNode key && this._PendingValues.ContainsKey(key) && this._PendingValues[(TNode) data] is IDictionary;
    bool externalCall = flag;
    if (value is PXCache.ExternalCallMarker externalCallMarker)
    {
      externalCall = !externalCallMarker.IsInternalCall;
      value = PXCache.ExternalCallMarker.Unwrap(value);
    }
    object valuePending = this.GetValuePending(data, fieldName);
    if ((valuePending == null ? 0 : (valuePending != PXCache.NotSetValue ? 1 : 0)) != 0)
    {
      object pending;
      object copy = this.GetCopy(data, out pending);
      if ((flag ? 1 : (copy == null || !this.OnFieldUpdating(fieldName, pending, ref valuePending) ? 0 : (object.Equals(this.GetValue(copy, fieldName), valuePending) ? 1 : 0))) == 0)
        return;
    }
    if (WebConfig.SetDefaultExtQueue && value == null && this._setDefaultExtQueue != null)
      this._setDefaultExtQueue.Add(fieldName);
    try
    {
      if (data is PXResult)
        data = ((PXResult) data)[0];
      if (value != null || this.OnFieldDefaulting(fieldName, data, out value))
        this.OnFieldUpdating(fieldName, data, ref value);
      this.OnFieldVerifying(fieldName, data, ref value, externalCall);
      object oldValue = (object) null;
      if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
      {
        int ordinal;
        if (!this._FieldsMap.TryGetValue(fieldName, out ordinal))
          return;
        oldValue = this.GetValueByOrdinal(this._LastAccessedNode, ordinal, this._LastAccessedExtensions);
        this.SetValueByOrdinal(this._LastAccessedNode, ordinal, value, this._LastAccessedExtensions);
      }
      else
      {
        switch (data)
        {
          case TNode node:
            this._LastAccessedNode = node;
            int ordinal1;
            if (!this._FieldsMap.TryGetValue(fieldName, out ordinal1))
              return;
            if (this._Extensions == null)
            {
              oldValue = this.GetValueByOrdinal(node, ordinal1, (PXCacheExtension[]) null);
              this.SetValueByOrdinal(node, ordinal1, value, (PXCacheExtension[]) null);
              break;
            }
            PXCacheExtension[] extensions;
            lock (((ICollection) this._Extensions).SyncRoot)
            {
              if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
                this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
            }
            this._LastAccessedExtensions = extensions;
            oldValue = this.GetValueByOrdinal(node, ordinal1, extensions);
            this.SetValueByOrdinal(node, ordinal1, value, extensions);
            break;
          case IDictionary dictionary:
            if (dictionary.Contains((object) fieldName))
            {
              oldValue = dictionary[(object) fieldName];
            }
            else
            {
              string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
              if (collectionKey != null)
              {
                oldValue = dictionary[(object) collectionKey];
                fieldName = collectionKey;
              }
            }
            dictionary[(object) fieldName] = value;
            break;
        }
      }
      this.OnFieldUpdated(fieldName, data, oldValue, externalCall);
    }
    catch (PXSetPropertyException ex)
    {
      if (this.OnExceptionHandling(fieldName, data, value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
    }
  }

  /// <summary>
  /// Sets the value of the field in the provided data record.
  /// </summary>
  /// <remarks>
  /// The method raises the <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, and <tt>FieldUpdated</tt> events. To set the
  /// value to the field without raising events, use the
  /// <see cref="M:PX.Data.PXCache`1.SetValue(System.Object,System.String,System.Object)" /> method.
  /// </remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field that is set to the value.
  /// The parameter is case-insensitive.</param>
  /// <param name="value">The value to set to the field.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void SetValueExt(object data, string fieldName, object value)
  {
    if (!this.Fields.Contains(fieldName))
      return;
    try
    {
      if (data is PXResult)
        data = ((PXResult) data)[0];
      bool externalCall;
      if (value is PXCache.ExternalCallMarker externalCallMarker)
      {
        externalCall = !externalCallMarker.IsInternalCall;
        value = PXCache.ExternalCallMarker.Unwrap(value);
      }
      else
        externalCall = this._PendingValues != null && data is TNode key && this._PendingValues.ContainsKey(key) && this._PendingValues[(TNode) data] is IDictionary;
      this.OnFieldUpdating(fieldName, data, ref value);
      this.OnFieldVerifying(fieldName, data, ref value, externalCall);
      object oldValue = (object) null;
      if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
      {
        int ordinal;
        if (!this._FieldsMap.TryGetValue(fieldName, out ordinal))
          return;
        oldValue = this.GetValueByOrdinal(this._LastAccessedNode, ordinal, this._LastAccessedExtensions);
        this.SetValueByOrdinal(this._LastAccessedNode, ordinal, value, this._LastAccessedExtensions);
      }
      else
      {
        switch (data)
        {
          case TNode node:
            this._LastAccessedNode = node;
            if (this._Extensions != null)
            {
              PXCacheExtension[] pxCacheExtensionArray;
              lock (((ICollection) this._Extensions).SyncRoot)
              {
                if (!this._Extensions.TryGetValue((IBqlTable) node, out pxCacheExtensionArray))
                  this._Extensions[(IBqlTable) node] = pxCacheExtensionArray = this._CreateExtensions(node);
              }
              this._LastAccessedExtensions = pxCacheExtensionArray;
            }
            else
              this._LastAccessedExtensions = (PXCacheExtension[]) null;
            int ordinal1;
            if (!this._FieldsMap.TryGetValue(fieldName, out ordinal1))
              return;
            oldValue = this.GetValueByOrdinal(node, ordinal1, this._LastAccessedExtensions);
            this.SetValueByOrdinal(node, ordinal1, value, this._LastAccessedExtensions);
            break;
          case IDictionary dictionary:
            if (dictionary.Contains((object) fieldName))
            {
              oldValue = dictionary[(object) fieldName];
            }
            else
            {
              string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
              if (collectionKey != null)
              {
                oldValue = dictionary[(object) collectionKey];
                fieldName = collectionKey;
              }
            }
            dictionary[(object) fieldName] = value;
            break;
        }
      }
      this.OnFieldUpdated(fieldName, data, oldValue, externalCall);
    }
    catch (PXSetPropertyException ex)
    {
      if (this.OnExceptionHandling(fieldName, data, value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
    }
  }

  private object NormalizeData(object data)
  {
    return !(data is PXResult) ? data : (object) PXResult.Unwrap<TNode>(data);
  }

  /// <summary>Returns the value of the specified field in the given data
  /// record without raising any events. The field is specified by its
  /// index—see the <see cref="M:PX.Data.PXCache`1.GetFieldOrdinal(System.String)">GetFieldOrdinal(string)</see>
  /// method.</summary>
  /// <param name="data">The data record.</param>
  /// <param name="ordinal">A field index, value of which is returned.</param>
  /// <example>
  /// <code title="Example" description="" lang="CS">
  /// if (tran == null || tran.InventoryID == null || !string.IsNullOrEmpty(tran.PONbr))
  /// {
  ///     e.NewValue = sender.GetValue&lt;APTran.curyUnitCost&gt;(e.Row);
  ///     e.Cancel = e.NewValue != null;
  ///     return;
  /// }</code>
  /// </example>
  public override object GetValue(object data, int ordinal)
  {
    data = this.NormalizeData(data);
    if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
      return this.GetValueByOrdinal(this._LastAccessedNode, ordinal, this._LastAccessedExtensions);
    if (data is TNode node)
    {
      this._LastAccessedNode = node;
      if (this._Extensions == null)
        return this.GetValueByOrdinal(node, ordinal, (PXCacheExtension[]) null);
      PXCacheExtension[] extensions;
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
          this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
      }
      this._LastAccessedExtensions = extensions;
      return this.GetValueByOrdinal(node, ordinal, extensions);
    }
    if (ordinal < this._ClassFields.Count && data is IDictionary dictionary)
    {
      string classField = this._ClassFields[ordinal];
      if (dictionary.Contains((object) classField))
        return dictionary[(object) classField];
      string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, classField);
      if (collectionKey != null)
        return dictionary[(object) collectionKey];
    }
    return (object) null;
  }

  /// <summary>Returns the value of the specified field in the given data
  /// record without raising any events.</summary>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field whose value is
  /// returned.</param>
  /// <remarks>
  /// <para>To get a known DAC type data record field, you can use DAC properties.
  /// If a type of a data record is unknown (such as when it is available as
  /// <tt>object</tt>), you can use the <tt>GetValue()</tt> methods to get a value of a field.
  /// These methods can also be used to get values of fields defined in
  /// extensions (another way is to get the extension data record through
  /// the <see cref="M:PX.Data.PXCache`1.GetExtension``1(`0)">GetExtension&lt;&gt;()</see> method).</para>
  /// <para>The <see cref="M:PX.Data.PXCache`1.GetValueExt(System.Object,System.String)">GetValueExt()</see> methods
  /// are used to get the value or the field state object and raise events.</para>
  /// </remarks>
  /// <example><para>The code snippet below iterates throughout the entire collection of fields in a specific DAC (including those fields defined in extensions) and checks whether the value is null.</para>
  /// <code title="Example" lang="CS">
  /// foreach (string field in sender.Fields)
  /// {
  ///     if (sender.GetValue(row, field) == null)
  ///         ...
  /// }</code>
  /// </example>
  public override object GetValue(object data, string fieldName)
  {
    int ordinal;
    if (this._FieldsMap.TryGetValue(fieldName, out ordinal))
    {
      if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
        return this.GetValueByOrdinal(this._LastAccessedNode, ordinal, this._LastAccessedExtensions);
      switch (data)
      {
        case TNode node:
          this._LastAccessedNode = node;
          if (this._Extensions == null)
            return this.GetValueByOrdinal(node, ordinal, (PXCacheExtension[]) null);
          PXCacheExtension[] extensions;
          lock (((ICollection) this._Extensions).SyncRoot)
          {
            if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
              this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
          }
          this._LastAccessedExtensions = extensions;
          return this.GetValueByOrdinal(node, ordinal, extensions);
        case IDictionary dictionary:
          if (dictionary.Contains((object) fieldName))
            return dictionary[(object) fieldName];
          string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
          if (collectionKey != null)
            return dictionary[(object) collectionKey];
          break;
      }
    }
    else
    {
      if (string.Equals(fieldName, "DatabaseRecordStatus"))
      {
        bool flag;
        switch (this.GetStatus(data))
        {
          case PXEntryStatus.Inserted:
          case PXEntryStatus.InsertedDeleted:
            flag = true;
            break;
          default:
            flag = false;
            break;
        }
        return flag ? (object) 0 : (object) (this.IsArchived(data) ? 1 : 0);
      }
      if (string.Equals(fieldName, "DeletedDatabaseRecord"))
      {
        bool flag;
        switch (this.GetStatus(data))
        {
          case PXEntryStatus.Inserted:
          case PXEntryStatus.InsertedDeleted:
            flag = true;
            break;
          default:
            flag = false;
            break;
        }
        return flag ? (object) false : (object) this.IsDeletedRecord(data);
      }
      if (fieldName != null)
      {
        int num = fieldName.IndexOf('.');
        if (num > 0 && num < fieldName.Length - 1)
        {
          fieldName = fieldName.Substring(num + 1);
          if (this._FieldsMap.ContainsKey(fieldName))
          {
            if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
              return this.GetValueByOrdinal(this._LastAccessedNode, this._FieldsMap[fieldName], this._LastAccessedExtensions);
            switch (data)
            {
              case TNode node:
                this._LastAccessedNode = node;
                if (this._Extensions == null)
                  return this.GetValueByOrdinal(node, this._FieldsMap[fieldName], (PXCacheExtension[]) null);
                PXCacheExtension[] extensions;
                lock (((ICollection) this._Extensions).SyncRoot)
                {
                  if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
                    this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
                }
                this._LastAccessedExtensions = extensions;
                return this.GetValueByOrdinal(node, this._FieldsMap[fieldName], extensions);
              case IDictionary dictionary:
                if (dictionary.Contains((object) fieldName))
                  return dictionary[(object) fieldName];
                string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
                if (collectionKey != null)
                  return dictionary[(object) collectionKey];
                break;
            }
          }
        }
      }
    }
    return (object) null;
  }

  /// <exclude />
  public override object GetValueInt(object data, string fieldName)
  {
    return this.GetValueInt(data, fieldName, this._AlteredFields != null && this._AlteredFields.Contains(fieldName.ToLower()), false);
  }

  /// <exclude />
  internal override object GetStateInt(object data, string fieldName)
  {
    return this.GetValueInt(data, fieldName, true, false);
  }

  /// <summary>Returns external UI field representation. The <tt>PXFieldState</tt> object is returned if the field is in the <tt>AlteredFields</tt> collection.</summary>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field whose value or
  /// <tt>PXFieldState</tt> object is returned.</param>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <returns>
  /// <tt>PXFieldState</tt> object field value.</returns>
  /// <example>
  /// <code title="Example" description="" lang="CS">
  /// string name = _MapErrorTo.Name;
  /// name = char.ToUpper(name[0]) + name.Substring(1);
  /// object val = sender.GetValueExt(e.Row, name);
  ///     if (val is PXFieldState)
  ///     {
  ///         val = ((PXFieldState)val).Value;
  ///     }</code>
  /// <code title="Example2" description="" groupname="Example" lang="CS">
  /// protected virtual void APPayment_CashAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  /// {
  ///     APPayment payment = (APPayment)e.Row;
  ///     if (payment == null || e.NewValue == null) return;
  /// 
  ///     CashAccount cashAccount = PXSelect&lt;CashAccount, Where&lt;CashAccount.cashAccountID, Equal&lt;Required&lt;CashAccount.cashAccountID&gt;&gt;&gt;&gt;.SelectSingleBound(this, null, e.NewValue);
  ///     if (cashAccount != null)
  ///     {
  ///         foreach (PXResult&lt;APAdjust, APInvoice, Standalone.APRegisterAlias&gt; res in Adjustments_Invoices.Select())
  ///         {
  ///             APAdjust adj = res;
  ///             APInvoice invoice = res;
  /// 
  ///             PXCache&lt;APRegister&gt;.RestoreCopy(invoice, (Standalone.APRegisterAlias)res);
  /// 
  ///             if(adj.AdjdDocType == APDocType.Prepayment
  ///                 &amp;&amp; (adj.AdjgDocType == APDocType.Check || adj.AdjgDocType == APDocType.VoidCheck)
  ///                 &amp;&amp; invoice.CuryID != cashAccount.CuryID)
  ///             {
  ///                 e.NewValue = sender.GetValueExt&lt;APPayment.cashAccountID&gt;(payment);
  ///                 throw new PXSetPropertyException(Messages.CashCuryNotPPCury);
  ///             }
  ///         }
  ///     }
  /// }</code>
  /// </example>
  public override object GetValueExt(object data, string fieldName)
  {
    return this.GetValueInt(data, fieldName, this._AlteredFields != null && this._AlteredFields.Contains(fieldName.ToLower()), true);
  }

  /// <summary>Gets the <tt>PXFieldState</tt> object of the specified field
  /// in the given data record.</summary>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field whose
  /// <tt>PXFieldState</tt> object is created.</param>
  /// <returns>The field state object.</returns>
  public override object GetStateExt(object data, string fieldName)
  {
    return this.GetValueInt(data, fieldName, true, true);
  }

  /// <summary>Returns the value of the specified field for the data record
  /// as it is stored in the database.</summary>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field whose original value is
  /// returned.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object GetValueOriginal(object data, string fieldName)
  {
    object original = this.GetOriginal(data);
    return original != null ? this.GetValue(original, fieldName) : (object) null;
  }

  /// <exclude />
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object GetOriginal(object data)
  {
    BqlTablePair bqlTablePair = (BqlTablePair) null;
    try
    {
      bqlTablePair = this.GetOriginalObjectContext(data, true);
    }
    catch
    {
    }
    return bqlTablePair != null && bqlTablePair.Unchanged != null ? (object) bqlTablePair.Unchanged : (object) null;
  }

  /// <summary>Returns the value of the field from the provided data record
  /// when the data record's update or insertion is in progress.</summary>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event.</remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The field name.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object GetValuePending(object data, string fieldName)
  {
    object data1;
    if (this._PendingValues == null || !(data is TNode key) || !this._PendingValues.TryGetValue(key, out data1))
      return (object) null;
    object valuePending = this.GetValueExt(data1, fieldName);
    if (valuePending is PXFieldState)
      valuePending = ((PXFieldState) valuePending).Value;
    else if (valuePending == null && data1 is IDictionary)
    {
      IDictionary dictionary = (IDictionary) data1;
      valuePending = !string.Equals(PXImportAttribute.ImportFlag, fieldName, StringComparison.OrdinalIgnoreCase) ? (dictionary.Contains((object) fieldName) ? dictionary[(object) fieldName] : PXCache.NotSetValue) : dictionary[(object) fieldName];
    }
    return valuePending;
  }

  /// <summary>Sets the value of the field in the provided data record when
  /// the data record's update or insertion is in process and the field
  /// possibly hasn't been updated in the cache yet. The field is specified
  /// in the type parameter.</summary>
  /// <remarks>The method raises the <tt>FieldUpdating</tt> event.</remarks>
  /// <param name="data">The data record.</param>
  /// <param name="fieldName">The name of the field that is set to the
  /// value.</param>
  /// <param name="value">The value to set to the field.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void SetValuePending(object data, string fieldName, object value)
  {
    object data1;
    if (this._PendingValues == null || !(data is TNode key) || !this._PendingValues.TryGetValue(key, out data1))
      return;
    switch (data1)
    {
      case TNode _:
        if (value == PXCache.NotSetValue)
          return;
        this.OnFieldUpdating(fieldName, data, ref value);
        this.SetValue(data1, fieldName, value);
        return;
      case IDictionary dictionary:
        if (dictionary.Contains((object) PXImportAttribute.ImportFlag) && value == PXCache.NotSetValue)
          return;
        break;
    }
    this.SetValue(data1, fieldName, value);
  }

  internal override bool HasPendingValues(object data)
  {
    if (!(data is TNode key))
      return false;
    Dictionary<TNode, object> pendingValues = this._PendingValues;
    // ISSUE: explicit non-virtual call
    return pendingValues != null && __nonvirtual (pendingValues.ContainsKey(key));
  }

  protected internal override object GetValueInt(
    object data,
    string fieldName,
    bool forceState,
    bool externalCall)
  {
    data = this.NormalizeData(data);
    object returnValue = (object) null;
    if (data == null)
    {
      this.OnFieldSelecting(fieldName, (object) null, ref returnValue, forceState, true);
      string displayName;
      return returnValue == null && this._InactiveFields != null && this._InactiveFields.TryGetValue(fieldName, out displayName) ? (object) PXInactiveFieldState.CreateInstance(fieldName, displayName) : returnValue;
    }
    int ordinal;
    if (this._FieldsMap.TryGetValue(fieldName, out ordinal))
    {
      if ((object) this._LastAccessedNode != null && (object) this._LastAccessedNode == data)
      {
        object valueByOrdinal = this.GetValueByOrdinal(this._LastAccessedNode, ordinal, this._LastAccessedExtensions);
        this.OnFieldSelecting(fieldName, (object) this._LastAccessedNode, ref valueByOrdinal, forceState, externalCall);
        return valueByOrdinal;
      }
      if (data is IBqlTable)
      {
        TNode node = (TNode) data;
        this._LastAccessedNode = node;
        PXCacheExtension[] extensions = (PXCacheExtension[]) null;
        if (this._Extensions != null)
        {
          lock (((ICollection) this._Extensions).SyncRoot)
          {
            if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
              this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
          }
          this._LastAccessedExtensions = extensions;
        }
        object valueByOrdinal = this.GetValueByOrdinal(node, ordinal, extensions);
        this.OnFieldSelecting(fieldName, (object) (data as IBqlTable), ref valueByOrdinal, forceState, externalCall);
        return valueByOrdinal;
      }
      IDictionary dictionary = (IDictionary) data;
      if (dictionary.Contains((object) fieldName))
        return dictionary[(object) fieldName];
      string collectionKey = CompareIgnoreCase.GetCollectionKey(dictionary.Keys, fieldName);
      return collectionKey != null ? dictionary[(object) collectionKey] : (object) null;
    }
    if (this.Fields.Contains(fieldName) && data is IBqlTable)
    {
      this.OnFieldSelecting(fieldName, data, ref returnValue, forceState, true);
      return returnValue;
    }
    if (this._InactiveFields != null)
    {
      string displayName;
      if (this._InactiveFields.TryGetValue(fieldName, out displayName))
        return (object) PXInactiveFieldState.CreateInstance(fieldName, displayName);
      if (fieldName.EndsWith("_Date") || fieldName.EndsWith("_Time") && this._InactiveFields.TryGetValue(fieldName.Substring(0, fieldName.Length - 5), out displayName))
        return (object) PXInactiveFieldState.CreateInstance(fieldName, displayName);
    }
    return (object) null;
  }

  /// <summary>
  /// Copy values from dictionary to item with event handling.<br />
  /// for insert operation raise OnFieldDefaulting, OnFieldUpdating, OnFieldVerifying, OnFieldUpdated<br />
  /// for update operation raise OnFieldUpdating, OnFieldVerifying, OnFieldUpdated<br />
  /// for delete operation events raised for key fields, OnFieldUpdating, OnFieldUpdated
  /// returns key updated flag<br />
  /// </summary>
  /// <param name="item"></param>
  /// <param name="copy"></param>
  /// <param name="values"></param>
  /// <param name="operation"></param>
  /// <returns></returns>
  private bool FillWithValues(
    TNode item,
    TNode copy,
    IDictionary values,
    PXCacheOperation operation)
  {
    return this.FillWithValues(item, copy, values, operation, true);
  }

  private bool FillWithValues(
    TNode item,
    TNode copy,
    IDictionary values,
    PXCacheOperation operation,
    bool externalCall)
  {
    bool flag1 = false;
    bool flag2 = (object) copy == null && operation == PXCacheOperation.Update;
    if (operation == PXCacheOperation.Insert && (object) copy == null)
      copy = new TNode();
    if (this._PendingValues == null)
      this._PendingValues = new Dictionary<TNode, object>();
    this._PendingValues[item] = (object) values;
    PXCacheExtension[] extensions1 = (PXCacheExtension[]) null;
    PXCacheExtension[] extensions2 = (PXCacheExtension[]) null;
    if (this._Extensions != null)
    {
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        if (!this._Extensions.TryGetValue((IBqlTable) item, out extensions2))
          this._Extensions[(IBqlTable) item] = extensions2 = this._CreateExtensions(item);
      }
    }
    if ((object) copy != null)
    {
      this._PendingValues[copy] = (object) values;
      if (this._Extensions != null)
      {
        lock (((ICollection) this._Extensions).SyncRoot)
        {
          if (!this._Extensions.TryGetValue((IBqlTable) copy, out extensions1))
            this._Extensions[(IBqlTable) copy] = extensions1 = this._CreateExtensions(copy);
        }
      }
    }
    else
      extensions1 = (PXCacheExtension[]) null;
    bool flag3 = this.Graph.IsContractBasedAPI || values.Contains((object) PXImportAttribute.ImportFlag) && !this.Graph.IsImport && !this.Graph.IsExport;
    string str1 = (string) null;
    if (WebConfig.SetDefaultExtQueue && operation == PXCacheOperation.Insert)
    {
      this._setDefaultExtQueue = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._setDefaultExtQueueStack.Push(this._setDefaultExtQueue);
    }
    try
    {
      for (int index = 0; index < this.Fields.Count; ++index)
      {
        str1 = this.Fields[index];
        int ordinal;
        bool flag4 = this._FieldsMap.TryGetValue(str1, out ordinal);
        bool flag5 = this.IsKvExtAttribute(str1);
        bool flag6 = values.Contains((object) str1);
        object newValue1 = (object) null;
        if (flag6)
        {
          newValue1 = values[(object) str1];
          if (newValue1 is PXFieldState)
            newValue1 = ((PXFieldState) newValue1).Value;
        }
        if (flag4 | flag5 && operation == PXCacheOperation.Insert)
        {
          if (operation != PXCacheOperation.Insert)
            this.SetValueByOrdinal(item, ordinal, (object) null, extensions2);
          object newValue2;
          if (this.OnFieldDefaulting(str1, (object) item, out newValue2))
            this.OnFieldUpdating(str1, (object) item, ref newValue2);
          HashSet<string> setDefaultExtQueue = this._setDefaultExtQueue;
          // ISSUE: explicit non-virtual call
          if ((setDefaultExtQueue != null ? (__nonvirtual (setDefaultExtQueue.Contains(str1)) ? 1 : 0) : 0) != 0)
          {
            try
            {
              object valueByOrdinal = this.GetValueByOrdinal(item, ordinal, extensions2);
              this.OnFieldVerifying(str1, (object) item, ref newValue2, externalCall);
              this.SetValueByOrdinal(item, ordinal, newValue2, extensions2);
              if (!object.Equals(newValue2, valueByOrdinal))
                this.OnFieldUpdated(str1, (object) item, valueByOrdinal, externalCall);
            }
            catch (PXSetPropertyException ex)
            {
              if (this.OnExceptionHandling(str1, (object) item, newValue2, (Exception) ex))
                throw;
              PXTrace.WriteWarning((Exception) ex);
            }
          }
          else if (this.GetValueByOrdinal(item, ordinal, extensions2) == null)
            this.SetValueByOrdinal(item, ordinal, newValue2, extensions2);
          if ((object) copy != null)
          {
            object newValue3;
            if (this.OnFieldDefaulting(str1, (object) copy, out newValue3))
              this.OnFieldUpdating(str1, (object) copy, ref newValue3);
            this.SetValueByOrdinal(copy, ordinal, newValue3, extensions1);
          }
        }
        if (flag6 && (newValue1 != null || operation != PXCacheOperation.Insert))
        {
          if (newValue1 != PXCache.NotSetValue)
          {
            try
            {
              bool flag7 = false;
              object objB = (object) null;
              if (flag4 && (object) copy != null)
              {
                objB = this.GetValueByOrdinal(copy, ordinal, extensions1);
                if (this._Keys.Contains(str1) && newValue1 != null)
                  flag7 = true;
              }
              Exception exception = (Exception) null;
              bool flag8 = false;
              try
              {
                this.OnFieldUpdating(str1, (object) item, ref newValue1);
              }
              catch (Exception ex1)
              {
                flag8 = true;
                if ((object) copy != null)
                {
                  exception = ex1;
                  try
                  {
                    this.OnFieldUpdating(str1, (object) copy, ref newValue1);
                  }
                  catch (Exception ex2)
                  {
                    throw ex1;
                  }
                }
              }
              if ((object) copy != null && object.Equals(newValue1, objB))
              {
                if (flag3)
                {
                  if (object.Equals(newValue1, this.GetValueByOrdinal(item, ordinal, extensions2)))
                    continue;
                }
                else
                  continue;
              }
              if (flag7)
                flag1 = true;
              if (exception != null)
                throw exception;
              if (operation != PXCacheOperation.Delete)
                this.OnFieldVerifying(str1, (object) item, ref newValue1, externalCall);
              if (flag4)
              {
                if ((object) copy != null && object.Equals(newValue1, objB))
                {
                  if (flag3)
                  {
                    if (object.Equals(newValue1, this.GetValueByOrdinal(item, ordinal, extensions2)))
                      continue;
                  }
                  else
                    continue;
                }
                if (!flag8)
                {
                  object valueByOrdinal = this.GetValueByOrdinal(item, ordinal, extensions2);
                  this.SetValueByOrdinal(item, ordinal, newValue1, extensions2);
                  if (this.Graph.IsContractBasedAPI)
                    values[(object) (str1 + "_OriginalValue")] = valueByOrdinal;
                  if (!flag2)
                    this.OnFieldUpdated(str1, (object) item, valueByOrdinal, externalCall);
                }
              }
            }
            catch (PXSetPropertyException ex)
            {
              if (operation == PXCacheOperation.Insert && this._Keys.Contains(str1) || this.OnExceptionHandling(str1, (object) item, newValue1, (Exception) ex))
                throw;
              PXTrace.WriteWarning((Exception) ex);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      if (string.IsNullOrEmpty(str1))
        throw;
      string str2 = (string) null;
      switch (ex)
      {
        case PXSetupNotEnteredException _:
        case PXDialogRequiredException _:
          throw;
        case PXSetPropertyException _:
          (string str3, string errorText) = this.GetErrorText(ex, str1);
          DacDescriptor? emptyDacDescriptor = DacDescriptorUtils.GetNonEmptyDacDescriptor(this.Graph, (IBqlTable) item);
          throw new PXFieldProcessingException(str1, emptyDacDescriptor, ex, ((PXSetPropertyException) ex).ErrorLevel, str3, errorText);
        default:
          PXTrace.WriteWarning(ex);
          throw this.CreateWrappingFieldProcessingException(item, str2 ?? str1, ex);
      }
    }
    finally
    {
      if (WebConfig.SetDefaultExtQueue && operation == PXCacheOperation.Insert)
      {
        this._setDefaultExtQueueStack.Pop();
        this._setDefaultExtQueue = this._setDefaultExtQueueStack.Any<HashSet<string>>() ? this._setDefaultExtQueueStack.Peek() : (HashSet<string>) null;
      }
      this._PendingValues.Remove(item);
      if ((object) copy != null)
        this._PendingValues.Remove(copy);
    }
    return flag1;
  }

  /// <summary>
  /// Create new node, then assigns all default values, then copy non empty fields from item.<br />
  /// If no exceptions, replace item with copy.
  /// </summary>
  /// <param name="item"></param>
  private void FillWithValues(TNode copy, ref TNode item)
  {
    if (this._PendingValues == null)
      this._PendingValues = new Dictionary<TNode, object>();
    PXCacheExtension[] extensions1 = (PXCacheExtension[]) null;
    PXCacheExtension[] extensions2 = (PXCacheExtension[]) null;
    if (this._Extensions != null)
    {
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        if (!this._Extensions.TryGetValue((IBqlTable) item, out extensions1))
          this._Extensions[(IBqlTable) item] = extensions1 = this._CreateExtensions(item);
        this._Extensions[(IBqlTable) copy] = extensions2 = this._CreateExtensions(copy);
      }
    }
    this._PendingValues[copy] = (object) item;
    if (WebConfig.SetDefaultExtQueue)
    {
      this._setDefaultExtQueue = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._setDefaultExtQueueStack.Push(this._setDefaultExtQueue);
    }
    try
    {
      foreach (string classField in this._ClassFields)
      {
        object newValue1 = (object) null;
        string str1 = (string) null;
        try
        {
          int fields = this._FieldsMap[classField];
          object newValue2;
          if (this.OnFieldDefaulting(classField, (object) copy, out newValue2))
            this.OnFieldUpdating(classField, (object) copy, ref newValue2);
          object obj = this.GetValueByOrdinal(copy, fields, extensions2);
          HashSet<string> setDefaultExtQueue = this._setDefaultExtQueue;
          // ISSUE: explicit non-virtual call
          if ((setDefaultExtQueue != null ? (__nonvirtual (setDefaultExtQueue.Contains(classField)) ? 1 : 0) : 0) != 0)
          {
            this.OnFieldVerifying(classField, (object) copy, ref newValue2, false);
            this.SetValueByOrdinal(copy, fields, newValue2, extensions2);
            if (!object.Equals(newValue2, obj))
              this.OnFieldUpdated(classField, (object) copy, obj, false);
            obj = newValue2;
          }
          else if (obj == null && newValue2 != null)
          {
            this.SetValueByOrdinal(copy, fields, newValue2, extensions2);
            obj = newValue2;
          }
          newValue1 = this.GetValueByOrdinal(item, fields, extensions1);
          if (newValue1 != null)
          {
            this.OnFieldVerifying(classField, (object) copy, ref newValue1, false);
            this.SetValueByOrdinal(copy, fields, newValue1, extensions2);
            this.OnFieldUpdated(classField, (object) copy, obj, false);
          }
        }
        catch (PXSetupNotEnteredException ex)
        {
          throw;
        }
        catch (PXSetPropertyException ex)
        {
          bool flag = ex.Row != null && !this.ObjectsEqual((object) item, (object) ex.Row) && !this.ObjectsEqual((object) copy, (object) ex.Row);
          if (!flag && this.ForceExceptionHandling)
          {
            if (!this.OnExceptionHandling(classField, (object) copy, newValue1, (Exception) ex))
              continue;
          }
          (string str2, string errorText) = this.GetErrorText((Exception) ex, classField);
          DacDescriptor? emptyDacDescriptor = DacDescriptorUtils.GetNonEmptyDacDescriptor(this.Graph, (IBqlTable) item);
          PXSetPropertyException propertyException;
          if (newValue1 == null)
          {
            propertyException = (PXSetPropertyException) new PXFieldProcessingException(classField, emptyDacDescriptor, (Exception) ex, ex.ErrorLevel, str2, errorText);
          }
          else
          {
            object stateExt1 = this.GetStateExt((object) null, classField);
            object fieldValue = (this.GetStateExt((object) item, classField) is PXFieldState stateExt2 ? stateExt2.Value : (object) null) ?? newValue1;
            if (fieldValue is string && stateExt1 is PXStringState && !string.IsNullOrEmpty(((PXStringState) stateExt1).InputMask))
              fieldValue = (object) Mask.Format(((PXStringState) stateExt1).InputMask, (string) fieldValue);
            propertyException = (PXSetPropertyException) new PXFieldValueProcessingException(classField, emptyDacDescriptor, (Exception) ex, ex.ErrorLevel, str2, fieldValue, errorText);
          }
          if (this._Keys.Contains(classField) | flag)
            throw propertyException;
          List<Exception> exceptionList;
          if (!this._PendingExceptions.TryGetValue(copy, out exceptionList))
            this._PendingExceptions[copy] = exceptionList = new List<Exception>();
          exceptionList.Add((Exception) propertyException);
        }
        catch (PXDialogRequiredException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          PXTrace.WriteWarning(ex);
          throw this.CreateWrappingFieldProcessingException(item, str1 ?? classField, ex);
        }
      }
      BqlTablePair bqlTablePair1;
      if (this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair1) && bqlTablePair1.Slots != null && bqlTablePair1.Slots.Count > 0)
      {
        BqlTablePair bqlTablePair2;
        if (!this._Originals.TryGetValue((IBqlTable) copy, out bqlTablePair2))
          this._Originals[(IBqlTable) copy] = bqlTablePair2 = new BqlTablePair();
        bqlTablePair2.Slots = new List<object>(bqlTablePair1.Slots.Count);
        bqlTablePair2.SlotsOriginal = new List<object>(bqlTablePair1.Slots.Count);
        for (int index = 0; index < bqlTablePair1.Slots.Count; ++index)
        {
          if (bqlTablePair1.Slots[index] != null && index < this._SlotDelegates.Count)
            bqlTablePair2.Slots.Add(((Func<object, object>) this._SlotDelegates[index].Item3)(bqlTablePair1.Slots[index]));
          else
            bqlTablePair2.Slots.Add((object) null);
          if (bqlTablePair1.SlotsOriginal[index] != null && index < this._SlotDelegates.Count)
            bqlTablePair2.SlotsOriginal.Add(((Func<object, object>) this._SlotDelegates[index].Item3)(bqlTablePair1.SlotsOriginal[index]));
          else
            bqlTablePair2.SlotsOriginal.Add((object) null);
        }
      }
      item = copy;
    }
    finally
    {
      if (WebConfig.SetDefaultExtQueue)
      {
        this._setDefaultExtQueueStack.Pop();
        this._setDefaultExtQueue = this._setDefaultExtQueueStack.Any<HashSet<string>>() ? this._setDefaultExtQueueStack.Peek() : (HashSet<string>) null;
      }
      this._PendingValues.Remove(copy);
    }
  }

  private (string displayName, string errorText) GetErrorText(Exception ex, string currentFieldName)
  {
    string str1 = PXUIFieldAttribute.GetDisplayName((PXCache) this, currentFieldName);
    string str2 = ex.Message;
    if (str1 != null)
      str2 = PXUIFieldAttribute.FormatFieldName(str2, currentFieldName, str1);
    else
      str1 = currentFieldName;
    foreach (string fieldName in PXUIFieldAttribute.FindFieldNames(str2))
    {
      string otherFieldName = fieldName;
      if (this.Fields.Any<string>((Func<string, bool>) (x => x.Equals(otherFieldName, StringComparison.OrdinalIgnoreCase))))
      {
        string displayName = PXUIFieldAttribute.GetDisplayName((PXCache) this, otherFieldName);
        if (displayName != null)
          str2 = PXUIFieldAttribute.FormatFieldName(str2, otherFieldName, displayName);
      }
    }
    return (str1, str2);
  }

  private void FillWithValues(TNode item, TNode copy, TNode newitem)
  {
    if (this._PendingValues == null)
      this._PendingValues = new Dictionary<TNode, object>();
    this._PendingValues[item] = (object) newitem;
    if ((object) copy != null)
      this._PendingValues[copy] = (object) newitem;
    PXCacheExtension[] extensions1 = (PXCacheExtension[]) null;
    PXCacheExtension[] extensions2 = (PXCacheExtension[]) null;
    PXCacheExtension[] extensions3 = (PXCacheExtension[]) null;
    if (this._Extensions != null)
    {
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        if (!this._Extensions.TryGetValue((IBqlTable) item, out extensions1))
          this._Extensions[(IBqlTable) item] = extensions1 = this._CreateExtensions(item);
        if (!this._Extensions.TryGetValue((IBqlTable) newitem, out extensions2))
          this._Extensions[(IBqlTable) newitem] = extensions2 = this._CreateExtensions(newitem);
        if (!this._Extensions.TryGetValue((IBqlTable) copy, out extensions3))
          this._Extensions[(IBqlTable) copy] = extensions3 = this._CreateExtensions(copy);
      }
    }
    try
    {
      foreach (string classField in this._ClassFields)
      {
        object newValue = (object) null;
        string str1 = (string) null;
        try
        {
          int fields = this._FieldsMap[classField];
          object valueByOrdinal = this.GetValueByOrdinal(copy, fields, extensions3);
          newValue = this.GetValueByOrdinal(newitem, fields, extensions2);
          if (!object.Equals(valueByOrdinal, newValue))
          {
            this.OnFieldVerifying(classField, (object) item, ref newValue, false);
            if (!object.Equals(valueByOrdinal, newValue))
            {
              this.SetValueByOrdinal(item, fields, newValue, extensions1);
              this.OnFieldUpdated(classField, (object) item, valueByOrdinal, false);
            }
          }
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case PXDialogRequiredException _:
              throw;
            case PXSetPropertyException _:
              if (this.ForceExceptionHandling)
              {
                if (!this.OnExceptionHandling(classField, (object) item, newValue, ex))
                  continue;
              }
              (string str2, string errorText) = this.GetErrorText(ex, classField);
              DacDescriptor? emptyDacDescriptor = DacDescriptorUtils.GetNonEmptyDacDescriptor(this.Graph, (IBqlTable) item);
              if (newValue == null)
                throw new PXFieldProcessingException(classField, emptyDacDescriptor, ex, ((PXSetPropertyException) ex).ErrorLevel, str2, errorText);
              object stateExt = this.GetStateExt((object) null, classField);
              if (newValue is string && stateExt is PXStringState && !string.IsNullOrEmpty(((PXStringState) stateExt).InputMask))
                newValue = (object) Mask.Format(((PXStringState) stateExt).InputMask, (string) newValue);
              throw new PXFieldValueProcessingException(classField, emptyDacDescriptor, ex, ((PXSetPropertyException) ex).ErrorLevel, str2, newValue, errorText);
            default:
              PXTrace.WriteWarning(ex);
              throw this.CreateWrappingFieldProcessingException(item, str1 ?? classField, ex);
          }
        }
      }
    }
    finally
    {
      this._PendingValues.Remove(item);
      if ((object) copy != null)
        this._PendingValues.Remove(copy);
    }
  }

  private PXException CreateWrappingFieldProcessingException(
    TNode item,
    string fieldName,
    Exception innerException)
  {
    DacDescriptor dacDescriptor = this.Graph.GetDacDescriptor((IBqlTable) item);
    return !dacDescriptor.IsNonTrivial ? new PXException(innerException, "An error occurred during processing of the field {0}: {1}.", new object[2]
    {
      (object) fieldName,
      (object) innerException.Message
    }) : new PXException(new DacDescriptor?(dacDescriptor), innerException, "An error occurred during processing of the field {0}: {1}.", new object[2]
    {
      (object) fieldName,
      (object) innerException.Message
    });
  }

  /// <summary>Inserts a new row into the cache. Returns inserted row of type <tt>CacheItemType</tt> or null if row was not inserted. Raises events <tt>OnRowInserting</tt>,
  /// <tt>OnRowInserted</tt> and other field related events. Does not check the database for existing row. Flag <tt>AllowInsert</tt> does not affects this method.</summary>
  /// <param name="graph">A Graph object</param>
  /// <param name="item">IBqlTable of type <tt>CacheItemType</tt></param>
  public static TNode Insert(PXGraph graph, TNode item)
  {
    return (TNode) graph.Caches[typeof (TNode)].Insert((object) item);
  }

  /// <summary>Places a row into the cache with the Updated status. If the row does not exist in the cache, the method looks for it in the database. If the row does not
  /// exist in the database, the method inserts the row with the Inserted status.The method raises the OnRowUpdating, OnRowUpdated, and other events. The
  /// PXCache.AllowUpdate flag does not affect this method.</summary>
  /// <param name="graph">graph object</param>
  /// <param name="item">IBqlTable of type <tt>CacheItemType</tt></param>
  /// <returns></returns>
  public static TNode Update(PXGraph graph, TNode item)
  {
    return (TNode) graph.Caches[typeof (TNode)].Update((object) item);
  }

  public static TNode Delete(PXGraph graph, TNode item)
  {
    return (TNode) graph.Caches[typeof (TNode)].Delete((object) item);
  }

  /// <summary>Creates a new node along with cloning field values.</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  public static TNode CreateCopy(TNode item)
  {
    if ((object) item != null && item.GetType() != typeof (TNode))
      return (TNode) typeof (PXCache<>).MakeGenericType(item.GetType()).GetMethod(nameof (CreateCopy), BindingFlags.Static | BindingFlags.Public).Invoke((object) null, new object[1]
      {
        (object) item
      });
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    return PXCache<TNode>.CreateCopy(item, cacheStaticInfo._CreateExtensions, cacheStaticInfo._CloneExtensions, cacheStaticInfo._ClassFields, cacheStaticInfo._GetValueByOrdinal, cacheStaticInfo._SetValueByOrdinal);
  }

  public static void StoreOriginal(PXGraph graph, TNode item)
  {
    if (!(graph.Caches[typeof (TNode)] is PXCache<TNode> cach) || (object) item == null)
      return;
    if (cach.GetStatus(item) == PXEntryStatus.Notchanged)
      cach.Remove(item);
    cach.PlaceNotChangedWithOriginals(item);
  }

  protected static TNode CreateCopy(
    TNode item,
    PXCache<TNode>.CreateExtensionsDelegate _CreateExtensions,
    PXCache<TNode>.memberwiseCloneExtensionsDelegate _CloneExtensions,
    List<string> _ClassFields,
    PXCache<TNode>.GetValueByOrdinalDelegate _GetValueByOrdinal,
    PXCache<TNode>.SetValueByOrdinalDelegate _SetValueByOrdinal)
  {
    if ((object) item == null)
      return default (TNode);
    TNode node = default (TNode);
    PXCacheExtension[] extensions1 = (PXCacheExtension[]) null;
    PXCacheExtension[] extensions2 = (PXCacheExtension[]) null;
    PXCacheExtensionCollection extensionCollection = (PXCacheExtensionCollection) null;
    if (_CreateExtensions != null)
    {
      extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
      extensions2 = PXCache<TNode>.GetCacheExtensions(item, _CreateExtensions, extensionCollection);
    }
    TNode copy;
    if (PXCache<TNode>.memberwiseClone != null)
    {
      copy = (TNode) PXCache<TNode>.memberwiseClone(item);
      if (_CreateExtensions != null)
      {
        lock (((ICollection) extensionCollection).SyncRoot)
        {
          PXCacheExtension[] pxCacheExtensionArray;
          extensionCollection[(IBqlTable) copy] = pxCacheExtensionArray = _CloneExtensions(copy, extensions2);
        }
      }
    }
    else
    {
      copy = new TNode();
      if (_CreateExtensions != null)
      {
        lock (((ICollection) extensionCollection).SyncRoot)
          extensionCollection[(IBqlTable) copy] = extensions1 = _CreateExtensions(copy);
        for (int ordinal = 0; ordinal < _ClassFields.Count; ++ordinal)
        {
          object obj = _GetValueByOrdinal(item, ordinal, extensions2);
          _SetValueByOrdinal(copy, ordinal, obj, extensions1);
        }
      }
      else
      {
        for (int ordinal = 0; ordinal < _ClassFields.Count; ++ordinal)
          _SetValueByOrdinal(copy, ordinal, _GetValueByOrdinal(item, ordinal, (PXCacheExtension[]) null), (PXCacheExtension[]) null);
      }
    }
    return copy;
  }

  /// <summary>Copies values of all fields from the second data record to
  /// the first data record.</summary>
  /// <remarks>The data records should have the DAC type of the cache, or the
  /// method does nothing.</remarks>
  /// <param name="item">The data record whose field values are
  /// updated.</param>
  /// <param name="copy">The data record whose field values are
  /// copied.</param>
  public override void RestoreCopy(object item, object copy)
  {
    if (!(item is TNode) || !(copy is TNode copy1))
      return;
    PXCache<TNode>.RestoreCopy((TNode) item, copy1);
  }

  /// <summary>
  /// Creates a clone of the provided data record by initializing a new data record
  /// with the field values that are obtained from the provided data record.
  /// </summary>
  /// <param name="item">The data record to copy.</param>
  /// <example>The following example creates a copy of the Address data record, modifies it,
  /// and then inserts modified data straight into the cache.
  /// <code title="Example" lang="CS">
  /// Address addr = PXCache&lt;Address&gt;.CreateCopy(defAddress);
  /// addr.AddressID = null;
  /// addr.BAccountID = owner.BAccountID;
  /// addr = this.RemitAddress.Insert(addr);</code>
  /// </example>
  public override object CreateCopy(object item)
  {
    return item is TNode node ? (object) PXCache<TNode>.CreateCopy(node) : (object) null;
  }

  /// <summary>Converts the provided data record to the dictionary of field
  /// names and field values. Returns the resulting dictionary
  /// object.</summary>
  /// <remarks>The method raises the <tt>FieldSelecting</tt> event for each
  /// field.</remarks>
  /// <param name="data">The data record to convert to a dictionary.</param>
  public override Dictionary<string, object> ToDictionary(object data)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    foreach (string classField in this._ClassFields)
    {
      object valueExt;
      dictionary.Add(classField, (valueExt = this.GetValueExt(data, classField)) is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt);
    }
    return dictionary;
  }

  /// <summary>Returns the XML string representing the provided data
  /// record.</summary>
  /// <remarks>
  /// <para>The data record is represented in the XML by the
  /// <i>&lt;Row&gt;</i> element with the <i>type</i> attribute set to the
  /// DAC name. Each field is represented by the <i>&lt;Field&gt;</i>
  /// element with the <i>name</i> attribute holding the field name and the
  /// <i>value</i> attribute holding the field value.</para>
  /// </remarks>
  /// <param name="data">The data record to convert to XML.</param>
  public override string ToXml(object data)
  {
    if (!(data is TNode node))
      return (string) null;
    PXCacheExtension[] extensions = (PXCacheExtension[]) null;
    if (this._Extensions != null)
    {
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        if (!this._Extensions.TryGetValue((IBqlTable) node, out extensions))
          this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
      }
    }
    StringBuilder sb = new StringBuilder();
    using (XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) new StringWriter(sb)))
    {
      xmlTextWriter.Formatting = Formatting.Indented;
      xmlTextWriter.Indentation = 2;
      xmlTextWriter.WriteStartElement("Row");
      xmlTextWriter.WriteAttributeString("type", typeof (TNode).FullName);
      for (int index = 0; index < this._ClassFields.Count; ++index)
      {
        object valueByOrdinal = this.GetValueByOrdinal(node, this._FieldsMap[this._ClassFields[index]], extensions);
        if (valueByOrdinal != null)
        {
          xmlTextWriter.WriteStartElement("Field");
          xmlTextWriter.WriteAttributeString("name", this._ClassFields[index]);
          switch (System.Type.GetTypeCode(this._FieldTypes[index]))
          {
            case TypeCode.Object:
              if (this._FieldTypes[index] == typeof (Guid))
              {
                xmlTextWriter.WriteAttributeString("value", ((Guid) valueByOrdinal).ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture));
                break;
              }
              if (this._FieldTypes[index] == typeof (byte[]))
              {
                xmlTextWriter.WriteAttributeString("value", Convert.ToBase64String((byte[]) valueByOrdinal));
                break;
              }
              break;
            case TypeCode.Boolean:
              xmlTextWriter.WriteAttributeString("value", ((bool) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Int16:
              xmlTextWriter.WriteAttributeString("value", ((short) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Int32:
              xmlTextWriter.WriteAttributeString("value", ((int) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Int64:
              xmlTextWriter.WriteAttributeString("value", ((long) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Single:
              xmlTextWriter.WriteAttributeString("value", ((float) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Double:
              xmlTextWriter.WriteAttributeString("value", ((double) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.Decimal:
              xmlTextWriter.WriteAttributeString("value", ((Decimal) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.DateTime:
              xmlTextWriter.WriteAttributeString("value", ((System.DateTime) valueByOrdinal).ToString((IFormatProvider) CultureInfo.InvariantCulture));
              break;
            case TypeCode.String:
              xmlTextWriter.WriteAttributeString("value", (string) valueByOrdinal);
              break;
          }
          xmlTextWriter.WriteEndElement();
        }
      }
      xmlTextWriter.WriteEndElement();
    }
    return sb.ToString();
  }

  /// <summary>Initializes a data record with the provided XML string.</summary>
  /// <param name="xml">The XML string to parse.</param>
  /// <remarks>A data record is represented in the XML as the <i>&lt;Row&gt;</i> element with the <i>type</i> attribute that is set to the DAC name. Each field is
  /// represented as the <i>&lt;Field&gt;</i> element with the <i>name</i> attribute that holds the field name and the <i>value</i> attribute that holds the
  /// field value.</remarks>
  public override object FromXml(string xml)
  {
    if (xml != null)
    {
      using (TextReader input = (TextReader) new StringReader(xml))
      {
        using (XmlReader xmlReader = XmlReader.Create(input))
        {
          string str = (string) null;
          if (xmlReader.ReadToDescendant("Row"))
            str = xmlReader.GetAttribute("type");
          if (str == typeof (TNode).FullName)
          {
            TNode node = new TNode();
            PXCacheExtension[] extensions = (PXCacheExtension[]) null;
            if (this._Extensions != null)
            {
              lock (((ICollection) this._Extensions).SyncRoot)
                this._Extensions[(IBqlTable) node] = extensions = this._CreateExtensions(node);
            }
            while (xmlReader.Read())
            {
              if (xmlReader.Name == "Field")
              {
                string attribute1 = xmlReader.GetAttribute("name");
                if (!string.IsNullOrEmpty(attribute1))
                {
                  string attribute2 = xmlReader.GetAttribute("value");
                  int ordinal;
                  if (attribute2 != null && this._FieldsMap.TryGetValue(attribute1, out ordinal))
                  {
                    int reverse = this._ReverseMap[attribute1];
                    switch (System.Type.GetTypeCode(this._FieldTypes[reverse]))
                    {
                      case TypeCode.Object:
                        if (this._FieldTypes[reverse] == typeof (Guid))
                        {
                          Guid guid;
                          if (GUID.TryParse(attribute2, ref guid))
                          {
                            this.SetValueByOrdinal(node, ordinal, (object) guid, extensions);
                            continue;
                          }
                          continue;
                        }
                        if (this._FieldTypes[reverse] == typeof (byte[]))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) Convert.FromBase64String(attribute2), extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Boolean:
                        bool result1;
                        if (bool.TryParse(attribute2, out result1))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result1, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Int16:
                        short result2;
                        if (short.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result2, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Int32:
                        int result3;
                        if (int.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result3, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Int64:
                        long result4;
                        if (long.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result4))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result4, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Single:
                        float result5;
                        if (float.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result5, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Double:
                        double result6;
                        if (double.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result6))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result6, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.Decimal:
                        Decimal result7;
                        if (Decimal.TryParse(attribute2, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result7))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result7, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.DateTime:
                        System.DateTime result8;
                        if (System.DateTime.TryParse(attribute2, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result8))
                        {
                          this.SetValueByOrdinal(node, ordinal, (object) result8, extensions);
                          continue;
                        }
                        continue;
                      case TypeCode.String:
                        this.SetValueByOrdinal(node, ordinal, (object) attribute2, extensions);
                        continue;
                      default:
                        continue;
                    }
                  }
                }
              }
            }
            return (object) node;
          }
        }
      }
    }
    return (object) null;
  }

  internal override string ValueToString(string fieldName, object val, object dbval)
  {
    return this._EncryptAuditFields != null && this._EncryptAuditFields.Contains(fieldName) && dbval is string ? (string) dbval : base.ValueToString(fieldName, val, dbval);
  }

  /// <summary>Converts the provided value of the field to string and
  /// returns the resulting value. No events are raised.</summary>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="val">The field value.</param>
  public override string ValueToString(string fieldName, object val)
  {
    if (val == null)
      return (string) null;
    if (this._BypassAuditFields != null && this._BypassAuditFields.Contains(fieldName))
      return PXDBUserPasswordAttribute.DefaultVeil;
    if (!this.IsKvExtAttribute(fieldName))
    {
      switch (System.Type.GetTypeCode(this._FieldTypes[this._ReverseMap[fieldName]]))
      {
        case TypeCode.Object:
          if (this._FieldTypes[this._ReverseMap[fieldName]] == typeof (Guid))
            return ((Guid) val).ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
          return this._FieldTypes[this._ReverseMap[fieldName]] == typeof (byte[]) ? Convert.ToBase64String((byte[]) val) : (string) null;
        case TypeCode.Boolean:
          return ((bool) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Int16:
          return ((short) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Int32:
          return ((int) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Int64:
          return ((long) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Single:
          return ((float) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Double:
          return ((double) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.Decimal:
          return ((Decimal) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.DateTime:
          return ((System.DateTime) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.String:
          return (string) val;
        default:
          return (string) null;
      }
    }
    else
    {
      switch (this._KeyValueAttributeTypes[fieldName])
      {
        case StorageBehavior.KeyValueNumeric:
          switch (val)
          {
            case int num1:
              return num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
            case bool flag:
              return flag.ToString((IFormatProvider) CultureInfo.InvariantCulture);
            case Decimal num2:
              return num2.ToString((IFormatProvider) CultureInfo.InvariantCulture);
            default:
              throw new ArgumentOutOfRangeException(nameof (val));
          }
        case StorageBehavior.KeyValueDate:
          return ((System.DateTime) val).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        case StorageBehavior.KeyValueString:
        case StorageBehavior.KeyValueText:
          return (string) val;
        default:
          return (string) null;
      }
    }
  }

  /// <summary>Converts the provided value of the field from a string to the
  /// appropriate type and returns the resulting value. No events are
  /// raised.</summary>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="val">The string representation of the field
  /// value.</param>
  public override object ValueFromString(string fieldName, string val)
  {
    if (FilterVariable.GetVariableType(val).HasValue || RelativeDatesManager.IsRelativeDatesString(val))
      return (object) val;
    int index = -1;
    System.Type type = (System.Type) null;
    if (this._ReverseMap.TryGetValue(fieldName, out index))
      type = this._FieldTypes[index];
    else if (this.GetStateExt((object) null, fieldName) is PXFieldState stateExt)
      type = stateExt.DataType;
    if (type == (System.Type) null)
      return (object) null;
    switch (System.Type.GetTypeCode(type))
    {
      case TypeCode.Object:
        if (this._FieldTypes[index] == typeof (Guid))
        {
          Guid guid;
          if (GUID.TryParse(val, ref guid))
            return (object) guid;
          break;
        }
        if (this._FieldTypes[index] == typeof (byte[]))
          return (object) Convert.FromBase64String(val);
        break;
      case TypeCode.Boolean:
        bool result1;
        if (bool.TryParse(val, out result1))
          return (object) result1;
        break;
      case TypeCode.Int16:
        short result2;
        if (short.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          return (object) result2;
        break;
      case TypeCode.Int32:
        int result3;
        if (int.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
          return (object) result3;
        break;
      case TypeCode.Int64:
        long result4;
        if (long.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result4))
          return (object) result4;
        break;
      case TypeCode.Single:
        float result5;
        if (float.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
          return (object) result5;
        break;
      case TypeCode.Double:
        double result6;
        if (double.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result6))
          return (object) result6;
        break;
      case TypeCode.Decimal:
        Decimal result7;
        if (Decimal.TryParse(val, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result7))
          return (object) result7;
        break;
      case TypeCode.DateTime:
        System.DateTime result8;
        if (System.DateTime.TryParse(val, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result8))
          return (object) result8;
        break;
      case TypeCode.String:
        return (object) val;
    }
    return (object) null;
  }

  /// <summary>
  /// Copies values of all fields from the second data record to the
  /// first data record.
  /// </summary>
  /// <param name="item">The data record whose field values are updated.</param>
  /// <param name="copy">The data record whose field values are copied.</param>
  /// <example>
  /// The code below modifies an <tt>APRegister</tt> data record and copies the
  /// values of all its fields to an <tt>APInvoice</tt> data record.
  /// <code>
  /// APInvoice apdoc = ...
  /// ...
  /// // Modifying the doc data record
  /// doc.OpenDoc = true;
  /// doc.ClosedFinPeriodID = null;
  /// ...
  /// // Copying all fields of doc to apdoc (APInvoince derives from APRegister)
  /// PXCache&lt;APRegister&gt;.RestoreCopy(apdoc, doc);
  /// </code>
  /// </example>
  public static void RestoreCopy(TNode item, TNode copy)
  {
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    if (cacheStaticInfo._CreateExtensions != null)
    {
      PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
      PXCacheExtension[] extensions1;
      PXCacheExtension[] extensions2;
      lock (((ICollection) extensionCollection).SyncRoot)
      {
        if (!extensionCollection.TryGetValue((IBqlTable) copy, out extensions1))
          extensionCollection[(IBqlTable) copy] = extensions1 = cacheStaticInfo._CreateExtensions(copy);
        if (!extensionCollection.TryGetValue((IBqlTable) item, out extensions2))
          extensionCollection[(IBqlTable) item] = extensions2 = cacheStaticInfo._CreateExtensions(item);
      }
      for (int ordinal = 0; ordinal < cacheStaticInfo._ClassFields.Count; ++ordinal)
        cacheStaticInfo._SetValueByOrdinal(item, ordinal, cacheStaticInfo._GetValueByOrdinal(copy, ordinal, extensions1), extensions2);
    }
    else
    {
      for (int ordinal = 0; ordinal < cacheStaticInfo._ClassFields.Count; ++ordinal)
        cacheStaticInfo._SetValueByOrdinal(item, ordinal, cacheStaticInfo._GetValueByOrdinal(copy, ordinal, (PXCacheExtension[]) null), (PXCacheExtension[]) null);
    }
  }

  /// <summary>Used to sync extension objects.</summary>
  /// <param name="item">The data record whose field values should be synced.</param>
  public static void SyncModel(TNode item)
  {
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    if (cacheStaticInfo._CreateExtensions == null)
      return;
    PXCacheExtension[] cacheExtensions = PXCache<TNode>.GetCacheExtensions(item, cacheStaticInfo._CreateExtensions);
    for (int ordinal = 0; ordinal < cacheStaticInfo._ClassFields.Count; ++ordinal)
      cacheStaticInfo._SetValueByOrdinal(item, ordinal, cacheStaticInfo._GetValueByOrdinal(item, ordinal, cacheExtensions), cacheExtensions);
  }

  internal static System.Type GetItemTypeInternal() => typeof (TNode);

  /// <summary>Gets the collection of names of fields and virtual fields. By
  /// default, the collection includes all public properties of the DAC that
  /// is associated with the cache. The collection may also include the
  /// virtual fields that are injected by attributes (such as the
  /// description field of the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> attribute). The
  /// developer can add any field to the collection.</summary>
  public override PXFieldCollection Fields
  {
    get
    {
      if (this._Fields == null)
        this._Fields = new PXFieldCollection((IEnumerable<string>) this._ClassFields, this._FieldsMap);
      return this._Fields;
    }
  }

  /// <summary>Gets the list of classes that implement <tt>IBqlField</tt>
  /// and are nested in the DAC and its base type. These types represent DAC
  /// fields in BQL queries. This list differs from the list that the
  /// <tt>Fields</tt> property returns.</summary>
  public override List<System.Type> BqlFields
  {
    get
    {
      if (this._BqlFields == null)
        this._BqlFields = new List<System.Type>((IEnumerable<System.Type>) this._BqlFieldsMap.Keys);
      return this._BqlFields;
    }
  }

  /// <summary>Gets the collection of BQL types that correspond to the key
  /// fields which the DAC defines.</summary>
  public override List<System.Type> BqlKeys
  {
    get
    {
      if (this._BqlKeys == null)
      {
        this._BqlKeys = new List<System.Type>();
        foreach (string key in (IEnumerable<string>) this.Keys)
        {
          foreach (System.Type bqlField in this.BqlFields)
          {
            if (string.Compare(bqlField.Name, key, StringComparison.OrdinalIgnoreCase) == 0)
            {
              this._BqlKeys.Add(bqlField);
              break;
            }
          }
        }
      }
      return this._BqlKeys;
    }
  }

  /// <exclude />
  public override List<System.Type> BqlImmutables
  {
    get
    {
      if (this._BqlImmutables == null)
      {
        this._BqlImmutables = new List<System.Type>();
        foreach (string immutable in this.Immutables)
        {
          foreach (System.Type bqlField in this.BqlFields)
          {
            if (string.Compare(bqlField.Name, immutable, StringComparison.OrdinalIgnoreCase) == 0)
            {
              this._BqlImmutables.Add(bqlField);
              break;
            }
          }
        }
      }
      return this._BqlImmutables;
    }
  }

  /// <exclude />
  public override BqlCommand BqlSelect
  {
    get => this._BqlSelect;
    set => this._BqlSelect = value;
  }

  /// <summary>Gets the DAC the cache is associated with. The DAC is
  /// specified through the type parameter when the cache is
  /// instantiated.</summary>
  public override System.Type BqlTable => PXCache<TNode>._BqlTable;

  public override System.Type GenericParameter => typeof (TNode);

  internal override System.Type GetFieldType(string fieldName)
  {
    int index;
    if (this._ReverseMap.TryGetValue(fieldName, out index))
      return this._FieldTypes[index];
    StorageBehavior storageBehavior;
    if (this._KeyValueAttributeTypes == null || !this._KeyValueAttributeTypes.TryGetValue(fieldName, out storageBehavior))
      return (System.Type) null;
    switch (storageBehavior)
    {
      case StorageBehavior.KeyValueNumeric:
        return typeof (Decimal);
      case StorageBehavior.KeyValueDate:
        return typeof (System.DateTime);
      case StorageBehavior.KeyValueString:
      case StorageBehavior.KeyValueText:
        return typeof (string);
      default:
        return (System.Type) null;
    }
  }

  internal static Dictionary<string, System.Type> GetClassFieldTypes()
  {
    PXCache<TNode>.CacheStaticInfo result = PXCache<TNode>._Initialize(false);
    return result._ClassFields.ToDictionary<string, string, System.Type>((Func<string, string>) (field => field), (Func<string, System.Type>) (field => result._FieldTypes[result._ReverseMap[field]]), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  internal static TNode FillClassFieldValues(Dictionary<string, object> values)
  {
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    PXCacheExtension[] extensions = (PXCacheExtension[]) null;
    TNode node = new TNode();
    if (cacheStaticInfo._CreateExtensions != null)
    {
      PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
      lock (((ICollection) extensionCollection).SyncRoot)
        extensionCollection[(IBqlTable) node] = extensions = cacheStaticInfo._CreateExtensions(node);
    }
    foreach (KeyValuePair<string, object> keyValuePair in values)
      cacheStaticInfo._SetValueByOrdinal(node, cacheStaticInfo._FieldsMap[keyValuePair.Key], keyValuePair.Value, extensions);
    return node;
  }

  internal override System.Type GetBaseBqlField(string field)
  {
    return this._BqlFieldsMap != null ? this._BqlFieldsMap.Keys.Where<System.Type>((Func<System.Type, bool>) (f => f.DeclaringType == this.BqlTable && f.Name.Equals(field, StringComparison.OrdinalIgnoreCase))).FirstOrDefault<System.Type>() : base.GetBaseBqlField(field);
  }

  /// <exclude />
  public override System.Type[] GetExtensionTypes() => this._ExtensionTypes;

  /// <summary>Compares two data records by the key fields. Returns
  /// <tt>true</tt> if the values of all key fields in the data records are
  /// equal. Otherwise, returns <tt>false</tt>.</summary>
  /// <param name="a">The first data record to compare.</param>
  /// <param name="b">The second data record to compare.</param>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override bool ObjectsEqual(object a, object b)
  {
    TNode node1 = a as TNode;
    TNode node2 = b as TNode;
    if ((object) node1 != null && (object) node2 != null)
      return this._Equals((TNode) a, (TNode) b, PXLocalesProvider.CollationComparer ?? PXCollationComparer.DefaultCollationComparer);
    IDictionary dictionary1 = a as IDictionary;
    IDictionary dictionary2 = b as IDictionary;
    if (dictionary1 == null || dictionary2 == null)
      return false;
    using (IEnumerator<string> enumerator = ((IEnumerable<string>) this._Keys).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        return dictionary1.Contains((object) current) && dictionary2.Contains((object) current) && object.Equals(dictionary1[(object) current], dictionary2[(object) current]);
      }
    }
    return false;
  }

  /// <summary>Returns the hash code generated from key field
  /// values.</summary>
  /// <param name="data">The data record.</param>
  public override int GetObjectHashCode(object data)
  {
    if ((object) (data as TNode) != null)
      return this._GetHashCode((TNode) data, PXLocalesProvider.CollationComparer ?? PXCollationComparer.DefaultCollationComparer);
    if (!(data is IDictionary dictionary))
      return 0;
    int objectHashCode = 13;
    foreach (string key in (IEnumerable<string>) this._Keys)
    {
      if (!dictionary.Contains((object) key) || dictionary[(object) key] == null)
        return 0;
      objectHashCode = objectHashCode * 37 + dictionary[(object) key].GetHashCode();
    }
    return objectHashCode;
  }

  /// <summary>Returns a string of key fields and their values in the
  /// <i>{key1=value1, key2=value2}</i> format.</summary>
  /// <param name="data">The data record which key fields are written to a
  /// string.</param>
  public override string ObjectToString(object data)
  {
    if (data == null)
      return string.Empty;
    if (data is PXResult)
      data = ((PXResult) data)[0];
    if ((object) (data as TNode) == null)
      return data.ToString();
    StringBuilder stringBuilder = new StringBuilder(typeof (TNode).Name);
    stringBuilder.Append("{");
    bool flag = true;
    foreach (string key in (IEnumerable<string>) this.Keys)
    {
      if (flag)
        flag = false;
      else
        stringBuilder.Append(", ");
      stringBuilder.Append(key);
      stringBuilder.Append(" = ");
      stringBuilder.Append(this.GetValue(data, key));
    }
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }

  /// <summary>Gets a DAC extention instance of the specified type. The extension type is specified as the type parameter.</summary>
  /// <param name="item">A data record whose extension is returned.</param>
  /// <example><para>The code below gets an extension data record corresponding to the given instance of the base data record.</para>
  /// <code title="Example" lang="CS">
  /// InventoryItem item = cache.Current as InventoryItem;
  /// InventoryItemExtension itemExt =
  ///     cache.GetExtension&lt;InventoryItemExtension&gt;(item);</code>
  /// </example>
  public override Extension GetExtension<Extension>(object item)
  {
    if (item == null)
      return default (Extension);
    int index = Array.IndexOf<System.Type>(this._ExtensionTypes, typeof (Extension));
    if (index == -1 || this._Extensions == null)
      throw new PXException("An incorrect type of extension has been requested.");
    PXCacheExtension[] pxCacheExtensionArray;
    lock (((ICollection) this._Extensions).SyncRoot)
    {
      if (!this._Extensions.TryGetValue((IBqlTable) item, out pxCacheExtensionArray))
        this._Extensions[(IBqlTable) item] = pxCacheExtensionArray = this._CreateExtensions((TNode) item);
    }
    return (Extension) pxCacheExtensionArray[index];
  }

  public override object GetMain<Extension>(Extension item) => throw new NotImplementedException();

  /// <summary>Gets an extension of appropriate type</summary>
  /// <typeparam name="Extension">The type of extension requested</typeparam>
  /// <param name="item">Parent standard object</param>
  /// <returns>Object of type Extension</returns>
  public static Extension GetExtension<Extension>(TNode item) where Extension : PXCacheExtension<TNode>
  {
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    int index = Array.IndexOf<System.Type>(cacheStaticInfo._ExtensionTypes, typeof (Extension));
    if (index == -1)
      throw new PXException("An incorrect type of extension has been requested.");
    PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    PXCacheExtension[] pxCacheExtensionArray;
    lock (((ICollection) extensionCollection).SyncRoot)
    {
      if (extensionCollection.TryGetValue((IBqlTable) item, out pxCacheExtensionArray))
      {
        if (pxCacheExtensionArray != null)
          goto label_9;
      }
      extensionCollection[(IBqlTable) item] = pxCacheExtensionArray = cacheStaticInfo._CreateExtensions(item);
    }
label_9:
    return (Extension) pxCacheExtensionArray[index];
  }

  internal override PXCacheExtension[] GetCacheExtensions(IBqlTable item)
  {
    PXCacheExtensionCollection extensionCollection = PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    PXCacheExtension[] cacheExtensions;
    lock (((ICollection) extensionCollection).SyncRoot)
    {
      if (extensionCollection.TryGetValue(item, out cacheExtensions))
      {
        if (cacheExtensions != null)
          goto label_7;
      }
      PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
      extensionCollection[item] = cacheExtensions = cacheStaticInfo._CreateExtensions((TNode) item);
    }
label_7:
    return cacheExtensions;
  }

  private static PXCacheExtension[] GetCacheExtensions(
    TNode item,
    PXCache<TNode>.CreateExtensionsDelegate createExtensions,
    PXCacheExtensionCollection extensionCollection = null)
  {
    PXCacheExtensionCollection extensionCollection1 = extensionCollection ?? PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    PXCacheExtension[] cacheExtensions;
    lock (((ICollection) extensionCollection1).SyncRoot)
    {
      if (!extensionCollection1.TryGetValue((IBqlTable) item, out cacheExtensions))
        extensionCollection1[(IBqlTable) item] = cacheExtensions = createExtensions(item);
    }
    return cacheExtensions;
  }

  /// <summary>
  /// Returns a DAC type data records that are stored in the cache.
  /// </summary>
  public override System.Type GetItemType() => typeof (TNode);

  internal static string[] GetKeyNames(PXGraph graph) => PXCache.GetKeyNames(graph, typeof (TNode));

  internal override PXResult _InvokeTailAppender(object data, Query query, object[] pars)
  {
    Func<TNode, Query, object[], PXResult<TNode>> tailAppender = this._TailAppender;
    return tailAppender == null ? (PXResult) null : (PXResult) tailAppender((TNode) data, query, pars);
  }

  internal override object _InvokeSelectorGetter(
    object data,
    string field,
    PXView view,
    object[] pars,
    bool unwrap)
  {
    Func<TNode, Query, object[], PXResult> func;
    if (!(this._Interceptor is PXUIEmulatorAttribute) || this._SelectorGetter == null || !this._SelectorGetter.TryGetValue(field, out func))
      return (object) null;
    object row = (object) func((TNode) data, view.BqlSelect.GetQuery(this.Graph, view), pars);
    if (row != null & unwrap)
      row = (object) PXResult.UnwrapMain(row);
    return row;
  }

  internal override bool _InvokeMeetVerifier(object data, Query query, object[] pars)
  {
    Func<TNode, Query, object[], bool> meetVerifier = this._MeetVerifier;
    return meetVerifier == null || meetVerifier((TNode) data, query, pars);
  }

  /// <exclude />
  public override PXDBInterceptorAttribute Interceptor
  {
    get => this._Interceptor;
    set
    {
      this._Interceptor = value;
      this._Interceptor?.CacheAttached((PXCache) this);
    }
  }

  public override System.Type GetBqlField(string field)
  {
    int key;
    System.Type type;
    return this._FieldsMap.TryGetValue(field, out key) && this._InverseBqlFieldsMap.TryGetValue(key, out type) ? type : (System.Type) null;
  }

  /// <summary>Returns the number of fields and virtual fields which
  /// comprise the <tt>Fields</tt> collection.</summary>
  public override int GetFieldCount() => this.Fields.Count;

  /// <summary>Returns the index of the specified field in the internally kept fields map.</summary>
  public override int GetFieldOrdinal<Field>()
  {
    int num;
    return this._FieldsMap.TryGetValue(typeof (Field).Name, out num) ? num : -1;
  }

  /// <summary>Returns the index of the specified field in the internally
  /// kept fields map.</summary>
  /// <param name="field">The name of the field whose index is
  /// returned.</param>
  public override int GetFieldOrdinal(string field)
  {
    int num;
    return this._FieldsMap.TryGetValue(field, out num) ? num : -1;
  }

  /// <summary>Recalculates internally stored hash codes. The method should
  /// be called after a key field is modified in a data record from the
  /// cache.</summary>
  public override void Normalize() => this._Items.Normalize(default (TNode));

  protected PXCollection<TNode> Items => this._Items;

  /// <summary>Gets or sets the current data record. This property points to
  /// the last data record displayed in the user interface. If the user
  /// selects a data record in a grid, this property points to this data
  /// record. If the user or the application inserts, updates, or deletes a
  /// data record, the property points to this data record. Assigning this
  /// property raises the <tt>RowSelected</tt> event.</summary>
  /// <remarks>You can reference the <tt>Current</tt> data record and its
  /// fields in the <tt>PXSelect</tt> BQL statements by using the
  /// <tt>Current</tt> parameter.</remarks>
  public override object Current
  {
    get
    {
      if (this._Current == null)
      {
        if (!this.stateLoaded)
        {
          this.Load();
          if (this._Current != null)
            return this._Current;
        }
        this._Current = (object) this._Graph.GetDefault<TNode>();
      }
      this._CurrentPlacedIntoCache = default (TNode);
      return this._Current;
    }
    set
    {
      this._Current = (object) (value as TNode);
      this.OnRowSelected(this._Current);
    }
  }

  /// <exclude />
  public override object InternalCurrent => this._Current;

  protected static PXCache<TNode>.CacheStaticInfo _Initialize(bool ignoredResult)
  {
    PXCache.CacheStaticInfoBase cacheStaticInfoBase1 = (PXCache.CacheStaticInfoBase) null;
    Dictionary<System.Type, PXCache.CacheStaticInfoBase> dictionary1 = (Dictionary<System.Type, PXCache.CacheStaticInfoBase>) PXContext.GetSlot<PXCache.CacheStaticInfoDictionary>();
    if (dictionary1 == null)
    {
      if (!ignoredResult)
      {
        try
        {
          dictionary1 = (Dictionary<System.Type, PXCache.CacheStaticInfoBase>) PXContext.SetSlot<PXCache.CacheStaticInfoDictionary>(PXDatabase.GetSlot<PXCache.CacheStaticInfoDictionary>("CacheStaticInfo", typeof (PXGraph.FeaturesSet)));
        }
        catch
        {
        }
      }
    }
    if (dictionary1 != null)
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXExtensionManager._StaticInfoLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        if (dictionary1.TryGetValue(typeof (TNode), out cacheStaticInfoBase1))
          return (PXCache<TNode>.CacheStaticInfo) cacheStaticInfoBase1;
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }
    Dictionary<string, string> inactiveFields;
    PXExtensionManager.ListOfTypes listOfTypes = new PXExtensionManager.ListOfTypes(PXCache._GetExtensions(typeof (TNode), dictionary1 != null, out inactiveFields));
    PXReaderWriterScope readerWriterScope1;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope1).\u002Ector(PXExtensionManager._StaticInfoLock);
    Dictionary<PXExtensionManager.ListOfTypes, PXCache.CacheStaticInfoBase> dictionary2;
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope1).AcquireReaderLock();
      if (PXExtensionManager._CacheStaticInfo.TryGetValue(typeof (TNode), out dictionary2))
      {
        if (dictionary2.TryGetValue(listOfTypes, out cacheStaticInfoBase1))
        {
          ((PXReaderWriterScope) ref readerWriterScope1).UpgradeToWriterLock();
          if (dictionary1 != null)
            dictionary1[typeof (TNode)] = cacheStaticInfoBase1;
          return (PXCache<TNode>.CacheStaticInfo) cacheStaticInfoBase1;
        }
      }
    }
    finally
    {
      readerWriterScope1.Dispose();
    }
    PXCache.CacheStaticInfoBase cacheStaticInfoBase2 = PXCache<TNode>.buildCacheStaticInfo(typeof (TNode), ignoredResult, listOfTypes);
    if (ignoredResult)
      return (PXCache<TNode>.CacheStaticInfo) null;
    ((PXCache<TNode>.CacheStaticInfo) cacheStaticInfoBase2)._InactiveFields = inactiveFields;
    PXReaderWriterScope readerWriterScope2;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope2).\u002Ector(PXExtensionManager._StaticInfoLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope2).AcquireWriterLock();
      if (dictionary1 != null)
        dictionary1[typeof (TNode)] = cacheStaticInfoBase2;
      if (!PXExtensionManager._CacheStaticInfo.TryGetValue(typeof (TNode), out dictionary2))
        PXExtensionManager._CacheStaticInfo[typeof (TNode)] = dictionary2 = new Dictionary<PXExtensionManager.ListOfTypes, PXCache.CacheStaticInfoBase>();
      dictionary2[listOfTypes] = cacheStaticInfoBase2;
    }
    finally
    {
      readerWriterScope2.Dispose();
    }
    return (PXCache<TNode>.CacheStaticInfo) cacheStaticInfoBase2;
  }

  static PXCache()
  {
    try
    {
      PXCache<TNode>._Initialize(true);
    }
    catch
    {
    }
  }

  protected void SetAutomationFieldDefaulting(System.Type cacheType)
  {
    PXCache.FieldDefaultingDelegate defaultingDelegate = PXAccess.GetDefaultingDelegate(cacheType);
    if (defaultingDelegate == null)
      return;
    this.AutomationFieldDefaulting = defaultingDelegate;
  }

  /// <summary>The application does not need to instantiate PXCache directly, as the system creates caches automatically whenever they are needed. A cache instance is always bound to an instance of the business logic controller (graph). The application typically accesses a cache instance through the Cache property of a data view. The property always returns the valid cache instance, even if it didn’t exist before the property was accessed. A cache instance is also available through the Caches property of the graph to which the cache instance is bound.</summary>
  public PXCache(PXGraph graph)
  {
    PXCache<TNode> pxCache = this;
    Stopwatch stopwatch = Stopwatch.StartNew();
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    this._CreateExtensions = cacheStaticInfo._CreateExtensions;
    this._CloneExtensions = cacheStaticInfo._CloneExtensions;
    this._ExtensionTables = cacheStaticInfo._ExtensionTables;
    this._ExtensionTypes = cacheStaticInfo._ExtensionTypes;
    this._SetValueByOrdinal = cacheStaticInfo._SetValueByOrdinal;
    this._GetValueByOrdinal = cacheStaticInfo._GetValueByOrdinal;
    this._DelegatesLock = cacheStaticInfo._DelegatesLock;
    this._DelegatesGetHashCode = cacheStaticInfo._DelegatesGetHashCode;
    this._DelegatesEquals = cacheStaticInfo._DelegatesEquals;
    this._FieldsMap = cacheStaticInfo._FieldsMap;
    this._FieldAttributes = cacheStaticInfo._FieldAttributes;
    this._EventsRowMap = cacheStaticInfo._EventsRowMap;
    this._EventsFieldMap = cacheStaticInfo._EventsFieldMap;
    this._FieldAttributesFirst = cacheStaticInfo._FieldAttributesFirst;
    this._FieldAttributesLast = cacheStaticInfo._FieldAttributesLast;
    this._EventPositions = cacheStaticInfo._EventPositions;
    this._ClassFields = cacheStaticInfo._ClassFields;
    this._ReverseMap = cacheStaticInfo._ReverseMap;
    this._FieldTypes = cacheStaticInfo._FieldTypes;
    this._BqlFieldsMap = cacheStaticInfo._BqlFieldsMap;
    this._InverseBqlFieldsMap = cacheStaticInfo._InverseBqlFieldsMap;
    this._TimestampOrdinal = cacheStaticInfo._TimestampOrdinal;
    this._KeyValueStoredOrdinals = cacheStaticInfo._KeyValueStoredOrdinals;
    this._KeyValueStoredNames = cacheStaticInfo._KeyValueStoredNames;
    this._DBLocalizableNames = cacheStaticInfo._DBLocalizableNames;
    this._FirstKeyValueStored = cacheStaticInfo._FirstKeyValueStored;
    this._InactiveFields = cacheStaticInfo._InactiveFields;
    this.SetAutomationFieldDefaulting(typeof (TNode));
    this._Graph = graph;
    if (this._CreateExtensions != null)
    {
      this._Extensions = PXContext.GetSlot<PXCacheExtensionCollection>();
      if (this._Extensions == null)
        this._Extensions = PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    }
    PXCache.AlteredDescriptor alteredAttributes = graph.GetAlteredAttributes(typeof (TNode));
    if (alteredAttributes != null && alteredAttributes.Fields != null && alteredAttributes.Fields.Count > 0)
      this._GraphSpecificFields = alteredAttributes.Fields;
    this._Items = new PXCollection<TNode>((PXCache) this);
    this.FillEventsRowAttr(alteredAttributes);
    this._Graph.Caches.InitCacheMapping(this._Graph);
    for (System.Type type = typeof (TNode); type.IsBqlTable(); type = type.BaseType)
    {
      if (!this._Graph.Caches.ContainsKey(type) && this._Graph.Caches.HasCacheMapping(type, typeof (TNode)))
      {
        if (this._Graph.IsInitializing || !this._Graph.Caches.CanInitLazyCache())
        {
          this.AutomationHidden = this._Graph.AutomationHidden;
          this.AutomationInsertDisabled = this._Graph.AutomationInsertDisabled;
          this.AutomationUpdateDisabled = this._Graph.AutomationUpdateDisabled;
          this.AutomationDeleteDisabled = this._Graph.AutomationDeleteDisabled;
          this._Graph.Caches.Add(type, (PXCache) this);
          this._Graph.Caches.AttachHandlers(type, (PXCache) this);
        }
        else
          this._Graph.Caches.Add(type, (PXCache) this);
        this._Graph.Caches.RaiseCacheCreated(this._Graph, (PXCache) this);
      }
      if ((!(type == typeof (TNode)) && !typeof (TNode).IsSubclassOf(type) || !(type == cacheStaticInfo._BreakInheritanceType)) && this._Graph.Caches.IsSameCache(type.BaseType) && !(this._Graph.GetType() == typeof (PXGraph)) && !(this._Graph.GetType() == typeof (PXGenericInqGrph)) && !(this._Graph.GetType() == typeof (GenericInquiryDesigner)))
      {
        if (this._Graph._ReadonlyCacheCreation)
        {
          this._ReadonlyCreatedCache = true;
          break;
        }
      }
      else
        break;
    }
    int index1 = -1;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute cacheAttribute in this._CacheAttributes)
    {
      if (cacheAttribute is PXDBAttributeAttribute)
      {
        int num = string.IsNullOrEmpty(this._NoteIDName) ? (this._Fields != null ? this._Fields.Count : this._ClassFields.Count) : -1;
        cacheAttribute.InvokeCacheAttached((PXCache) this);
        if (num >= 0 && !string.IsNullOrEmpty(this._NoteIDName))
          index1 = this.Fields.IndexOf(this._NoteIDName) + (this._Fields != null ? this._Fields.Count : this._ClassFields.Count) - num + 1;
      }
      else
        subscriberAttributeList.Add(cacheAttribute);
    }
    foreach (PXEventSubscriberAttribute subscriberAttribute in subscriberAttributeList)
    {
      int num = string.IsNullOrEmpty(this._NoteIDName) ? (this._Fields != null ? this._Fields.Count : this._ClassFields.Count) : -1;
      subscriberAttribute.InvokeCacheAttached((PXCache) this);
      if (num >= 0 && !string.IsNullOrEmpty(this._NoteIDName))
        index1 = this.Fields.IndexOf(this._NoteIDName) + (this._Fields != null ? this._Fields.Count : this._ClassFields.Count) - num + 1;
    }
    if (alteredAttributes != null && alteredAttributes._Method != null)
      alteredAttributes._Method(this._Graph, (PXCache) this);
    if (cacheStaticInfo._TypeInterceptorAttribute != null)
    {
      this._Interceptor = ((PXDBInterceptorAttribute) cacheStaticInfo._TypeInterceptorAttribute).Clone();
      if (this._ExtensionTables != null && this._ExtensionTables.Count > 0 && this._ExtensionTables.Last<System.Type>().BaseType.IsGenericType && (((IEnumerable<System.Type>) this._ExtensionTables.Last<System.Type>().BaseType.GetGenericArguments()).Last<System.Type>() == typeof (TNode) || ((IEnumerable<System.Type>) this._ExtensionTables.Last<System.Type>().BaseType.GetGenericArguments()).Last<System.Type>().IsAssignableFrom(typeof (TNode))) && cacheStaticInfo._ExtensionInterceptorAttribute != null)
        this._Interceptor.Child = ((PXDBInterceptorAttribute) cacheStaticInfo._ExtensionInterceptorAttribute).Clone();
      this._Interceptor.CacheAttached((PXCache) this);
      this._BqlSelect = this._Interceptor.GetTableCommand();
    }
    else if (this._ExtensionTables != null && this._ExtensionTables.Count > 0 && cacheStaticInfo._ExtensionInterceptorAttribute != null)
    {
      this._Interceptor = ((PXDBInterceptorAttribute) cacheStaticInfo._ExtensionInterceptorAttribute).Clone();
      this._Interceptor.CacheAttached((PXCache) this);
      this._BqlSelect = this._Interceptor.GetTableCommand();
    }
    if (cacheStaticInfo._ClassAttributes != null)
    {
      for (int index2 = 0; index2 < cacheStaticInfo._ClassAttributes.Length; ++index2)
        ((PXClassAttribute) cacheStaticInfo._ClassAttributes[index2]).Clone().CacheAttached((PXCache) this);
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string key in (IEnumerable<string>) this.Keys)
      stringBuilder.Append(key);
    string key1 = stringBuilder.ToString();
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._DelegatesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      if (this._DelegatesEquals.TryGetValue(key1, out this._Equals))
      {
        if (this._DelegatesGetHashCode.TryGetValue(key1, out this._GetHashCode))
          goto label_85;
      }
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if (this._DelegatesEquals.TryGetValue(key1, out this._Equals))
      {
        if (this._DelegatesGetHashCode.TryGetValue(key1, out this._GetHashCode))
          goto label_85;
      }
      DynamicMethod dynamicMethod1;
      DynamicMethod dynamicMethod2;
      if (!PXGraph.IsRestricted)
      {
        dynamicMethod1 = new DynamicMethod(nameof (_Equals), typeof (bool), new System.Type[3]
        {
          typeof (TNode),
          typeof (TNode),
          typeof (PXCollationComparer)
        }, this.GetType());
        dynamicMethod2 = new DynamicMethod(nameof (_GetHashCode), typeof (int), new System.Type[2]
        {
          typeof (TNode),
          typeof (PXCollationComparer)
        }, this.GetType());
      }
      else
      {
        dynamicMethod1 = new DynamicMethod(nameof (_Equals), typeof (bool), new System.Type[3]
        {
          typeof (TNode),
          typeof (TNode),
          typeof (PXCollationComparer)
        });
        dynamicMethod2 = new DynamicMethod(nameof (_GetHashCode), typeof (int), new System.Type[2]
        {
          typeof (TNode),
          typeof (PXCollationComparer)
        });
      }
      ILGenerator ilGenerator1 = dynamicMethod1.GetILGenerator();
      Label label1 = ilGenerator1.DefineLabel();
      Label label2 = ilGenerator1.DefineLabel();
      ILGenerator ilGenerator2 = dynamicMethod2.GetILGenerator();
      ilGenerator2.DeclareLocal(typeof (int));
      ilGenerator2.Emit(OpCodes.Ldc_I4_S, 13);
      ilGenerator2.Emit(OpCodes.Stloc_0);
      ilGenerator1.DeclareLocal(typeof (bool));
      MethodInfo method1 = typeof (object).GetMethod("Equals", BindingFlags.Static | BindingFlags.Public, (Binder) null, new System.Type[2]
      {
        typeof (object),
        typeof (object)
      }, (ParameterModifier[]) null);
      MethodInfo method2 = typeof (PXCollationComparer).GetMethod("Equals", BindingFlags.Instance | BindingFlags.Public, (Binder) null, new System.Type[2]
      {
        typeof (string),
        typeof (string)
      }, (ParameterModifier[]) null);
      MethodInfo method3 = typeof (PXCollationComparer).GetMethod("GetHashCode", BindingFlags.Instance | BindingFlags.Public, (Binder) null, new System.Type[1]
      {
        typeof (string)
      }, (ParameterModifier[]) null);
      foreach (string key2 in (IEnumerable<string>) this.Keys)
      {
        PropertyInfo property = typeof (TNode).GetProperty(key2);
        System.Type propertyType = property.PropertyType;
        if (property.CanRead)
        {
          MethodInfo getMethod = property.GetGetMethod();
          if (propertyType == typeof (string))
            ilGenerator1.Emit(OpCodes.Ldarg_2);
          ilGenerator1.Emit(OpCodes.Ldarg_0);
          ilGenerator1.Emit(OpCodes.Callvirt, getMethod);
          if (propertyType.IsValueType)
            ilGenerator1.Emit(OpCodes.Box, propertyType);
          ilGenerator1.Emit(OpCodes.Ldarg_1);
          ilGenerator1.Emit(OpCodes.Callvirt, getMethod);
          if (propertyType.IsValueType)
            ilGenerator1.Emit(OpCodes.Box, propertyType);
          if (propertyType == typeof (string))
            ilGenerator1.Emit(OpCodes.Call, method2);
          else
            ilGenerator1.Emit(OpCodes.Call, method1);
          ilGenerator1.Emit(OpCodes.Brfalse, label1);
          LocalBuilder localBuilder = ilGenerator2.DeclareLocal(propertyType);
          ilGenerator2.Emit(OpCodes.Ldloc_0);
          ilGenerator2.Emit(OpCodes.Ldc_I4_S, 37);
          ilGenerator2.Emit(OpCodes.Mul);
          ilGenerator2.Emit(OpCodes.Stloc_0);
          ilGenerator2.Emit(OpCodes.Ldarg_0);
          ilGenerator2.Emit(OpCodes.Callvirt, getMethod);
          ilGenerator2.Emit(OpCodes.Stloc_S, localBuilder.LocalIndex);
          if (!propertyType.IsValueType)
          {
            Label label3 = ilGenerator2.DefineLabel();
            ilGenerator2.Emit(OpCodes.Ldnull);
            ilGenerator2.Emit(OpCodes.Ldloc_S, localBuilder.LocalIndex);
            ilGenerator2.Emit(OpCodes.Ceq);
            ilGenerator2.Emit(OpCodes.Brtrue_S, label3);
            if (propertyType == typeof (string))
              ilGenerator2.Emit(OpCodes.Ldarg_1);
            ilGenerator2.Emit(OpCodes.Ldloc_S, localBuilder.LocalIndex);
            if (propertyType == typeof (string))
              ilGenerator2.Emit(OpCodes.Call, method3);
            else
              ilGenerator2.Emit(OpCodes.Callvirt, propertyType.GetMethod("GetHashCode", new System.Type[0]));
            ilGenerator2.Emit(OpCodes.Ldloc_0);
            ilGenerator2.Emit(OpCodes.Add);
            ilGenerator2.Emit(OpCodes.Stloc_0);
            ilGenerator2.MarkLabel(label3);
          }
          else
          {
            ilGenerator2.Emit(OpCodes.Ldloca_S, localBuilder.LocalIndex);
            ilGenerator2.Emit(OpCodes.Call, propertyType.GetMethod("GetHashCode", new System.Type[0]));
            ilGenerator2.Emit(OpCodes.Ldloc_0);
            ilGenerator2.Emit(OpCodes.Add);
            ilGenerator2.Emit(OpCodes.Stloc_0);
          }
        }
      }
      ilGenerator1.Emit(OpCodes.Ldc_I4_1);
      ilGenerator1.Emit(OpCodes.Stloc_0);
      ilGenerator1.Emit(OpCodes.Br, label2);
      ilGenerator1.MarkLabel(label1);
      ilGenerator1.Emit(OpCodes.Ldc_I4_0);
      ilGenerator1.Emit(OpCodes.Stloc_0);
      ilGenerator1.MarkLabel(label2);
      ilGenerator1.Emit(OpCodes.Ldloc_0);
      ilGenerator1.Emit(OpCodes.Ret);
      ilGenerator2.Emit(OpCodes.Ldloc_0);
      ilGenerator2.Emit(OpCodes.Ret);
      this._DelegatesEquals[key1] = this._Equals = (PXCache<TNode>.EqualsDelegate) dynamicMethod1.CreateDelegate(typeof (PXCache<TNode>.EqualsDelegate));
      this._DelegatesGetHashCode[key1] = this._GetHashCode = (PXCache<TNode>.GetHashCodeDelegate) dynamicMethod2.CreateDelegate(typeof (PXCache<TNode>.GetHashCodeDelegate));
    }
    finally
    {
      readerWriterScope.Dispose();
    }
label_85:
    graph.Caches._cacheLogger.CacheCreated(this.GetItemType(), (double) stopwatch.ElapsedMilliseconds);
    KeyValueHelper.Definition def = KeyValueHelper.Def;
    KeyValueHelper.TableAttribute[] tableAttributeArray;
    if (def == null)
    {
      tableAttributeArray = (KeyValueHelper.TableAttribute[]) null;
    }
    else
    {
      System.Type table = this._KeyValueAttributeConfigurationSource;
      if ((object) table == null)
        table = typeof (TNode);
      tableAttributeArray = def.GetAttributes(table, graph);
    }
    if (tableAttributeArray == null)
      tableAttributeArray = Array.Empty<KeyValueHelper.TableAttribute>();
    KeyValueHelper.TableAttribute[] kvattributes = tableAttributeArray;
    int kvlength = kvattributes.Length;
    string graphType = CustomizedTypeManager.GetTypeNotCustomized(this._Graph).FullName;
    string udfTypeField = PXPageIndexingService.GetUDFTypeField(graphType);
    if (kvlength <= 0 || index1 < 0)
      return;
    this._KeyValueAttributeUIFields = (IPXInterfaceField[]) ((IEnumerable<KeyValueHelper.TableAttribute>) kvattributes).Select<KeyValueHelper.TableAttribute, PXUIFieldAttribute>((Func<KeyValueHelper.TableAttribute, PXUIFieldAttribute>) (_ => _.UIField)).ToArray<PXUIFieldAttribute>();
    this._KeyValueAttributeNames = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    this._KeyValueAttributeTypes = new Dictionary<string, StorageBehavior>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    this._KeyValueAttributeSlotPosition = this.SetupSlot<object[]>((Func<object[]>) (() => new object[kvlength]), (Func<object[], object[], object[]>) ((item, copy) =>
    {
      for (int index3 = 0; index3 < kvlength && index3 < item.Length && index3 < copy.Length; ++index3)
        item[index3] = copy[index3];
      return item;
    }), (Func<object[], object[]>) (item => (object[]) item.Clone()));
    this.RowSelecting += (PXRowSelecting) ((c, e) =>
    {
      if (e.Row == null || e.IsReadOnly)
        return;
      foreach (KeyValueHelper.TableAttribute tableAttribute in kvattributes)
      {
        PXUIFieldAttribute uiField = tableAttribute.UIField;
        ((IPXInterfaceField) uiField).ErrorLevel = PXErrorLevel.Undefined;
        ((IPXInterfaceField) uiField).ErrorValue = (object) null;
        ((IPXInterfaceField) uiField).ErrorText = (string) null;
      }
    });
    this.RowInserting += (PXRowInserting) ((c, e) =>
    {
      if (e.Row == null)
        return;
      SetDefaultUdfValues(c, e.Row);
    });
    this.RowPersisting += (PXRowPersisting) ((c, e) =>
    {
      if (e.Row == null)
        return;
      if (e.Operation == PXDBOperation.Insert && !pxCache.Graph.IsImport)
        SetDefaultUdfValues(c, e.Row);
      string screenID = PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(graphType)?.ScreenID ?? PXContext.GetScreenID()?.Replace(".", "");
      if (string.IsNullOrWhiteSpace(screenID))
        throw new InvalidOperationException($"Unable to persist the record of type '{c.GetItemType().FullName}'. No matching ScreenID exists for graph type '{graphType}' to determine what UDF fields are required. Please ensure that you use a typed maintenance/entry PXGraph instance or provide an explicit ScreenID via PXContext when operating on data access classes with UDF fields attached.");
      string str = string.Empty;
      if (udfTypeField != null)
        str = c.GetValue(e.Row, udfTypeField)?.ToString().TrimEnd();
      foreach (KeyValueHelper.TableAttribute tableAttribute in kvattributes)
      {
        Dictionary<string, object> dictionary = tableAttribute.ScreensRequires.FirstOrDefault<KeyValuePair<string, Dictionary<string, object>>>((Func<KeyValuePair<string, Dictionary<string, object>>, bool>) (kv => kv.Key == screenID)).Value;
        if (dictionary != null && (c.GetValueExt(e.Row, tableAttribute.FieldName) is PXFieldState valueExt2 ? valueExt2.Value : (object) null) == null)
        {
          object obj;
          dictionary.TryGetValue(str ?? string.Empty, out obj);
          if (obj == null)
            dictionary.TryGetValue(string.Empty, out obj);
          if (obj != null && (bool) obj)
          {
            if (pxCache.GetSlot<object[]>(e.Row, pxCache._KeyValueAttributeSlotPosition) == null)
              pxCache.SetSlot<object[]>(e.Row, pxCache._KeyValueAttributeSlotPosition, new object[kvlength]);
            if (pxCache.OnExceptionHandling(tableAttribute.FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) $"[{tableAttribute.FieldName}]"))))
              throw new PXRowPersistingException(tableAttribute.FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
              {
                (object) tableAttribute.FieldName
              });
          }
        }
      }
    });
    for (int index4 = 0; index4 < kvattributes.Length; ++index4)
    {
      string fieldName = kvattributes[index4].FieldName;
      this.Fields.Insert(index1, fieldName);
      ++index1;
      this._KeyValueAttributeNames[fieldName] = index4;
      this._KeyValueAttributeTypes[fieldName] = kvattributes[index4].Storage;
      string lower = fieldName.ToLower();
      int idx = index4;
      if (index4 == 0)
        this._FirstKeyValueAttribute = new KeyValuePair<string, int>?(new KeyValuePair<string, int>(kvattributes[index4].FieldName, 0));
      System.Action<PXCache> cacheAttached = kvattributes[idx].CacheAttached;
      if (cacheAttached != null)
        cacheAttached((PXCache) this);
      this.FieldUpdatingEvents[lower] += (PXFieldUpdating) ((c, e) =>
      {
        PXFieldUpdating fieldUpdating = kvattributes[idx].FieldUpdating;
        if (fieldUpdating != null)
          fieldUpdating(c, e);
        if (e.Row == null)
          return;
        object[] slot1 = closure_0.GetSlot<object[]>(e.Row, closure_0._KeyValueAttributeSlotPosition);
        PXEntryStatus status = closure_0.GetStatus(e.Row);
        if (slot1 == null && status != PXEntryStatus.Inserted)
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          closure_0.OnCommandPreparing(closure_0._NoteIDName, e.Row, (object) null, PXDBOperation.Select, (System.Type) null, out description);
          string[] alternatives;
          if (OnDemandCommand.GetKeyValues((PXCache) closure_0, e.Row, description.BqlTable, closure_0._KeyValueAttributeNames, out alternatives))
          {
            slot1 = closure_0.convertAttributesFromString(alternatives);
            closure_0.SetSlot<object[]>(e.Row, closure_0._KeyValueAttributeSlotPosition, slot1, true);
          }
        }
        if (slot1 == null)
        {
          object[] slot2 = new object[kvlength];
          slot2[idx] = e.NewValue;
          closure_0.SetSlot<object[]>(e.Row, closure_0._KeyValueAttributeSlotPosition, slot2);
        }
        else
          slot1[idx] = e.NewValue;
        if (status != PXEntryStatus.Updated)
          return;
        c.IsDirty = true;
      });
      this.FieldSelectingEvents[lower] += (PXFieldSelecting) ((c, e) =>
      {
        if (e.Row != null)
        {
          object[] slot = closure_0.GetSlot<object[]>(e.Row, closure_0._KeyValueAttributeSlotPosition);
          if (slot == null && closure_0.GetStatus(e.Row) != PXEntryStatus.Inserted)
          {
            PXCommandPreparingEventArgs.FieldDescription description;
            closure_0.OnCommandPreparing(closure_0._NoteIDName, e.Row, (object) null, PXDBOperation.Select, (System.Type) null, out description);
            string[] alternatives;
            if (OnDemandCommand.GetKeyValues((PXCache) closure_0, e.Row, description.BqlTable, closure_0._KeyValueAttributeNames, out alternatives))
            {
              slot = closure_0.convertAttributesFromString(alternatives);
              closure_0.SetSlot<object[]>(e.Row, closure_0._KeyValueAttributeSlotPosition, slot, true);
            }
          }
          if (slot != null)
            e.ReturnValue = slot[idx];
        }
        PXFieldSelecting fieldSelecting = kvattributes[idx].FieldSelecting;
        if (fieldSelecting != null)
          fieldSelecting(c, e);
        if (e.Row == null)
          return;
        string key3 = string.Empty;
        if (udfTypeField != null)
          key3 = c.GetValue(e.Row, udfTypeField)?.ToString().TrimEnd();
        string key4 = PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(graphType)?.ScreenID ?? PXContext.GetScreenID()?.Replace(".", "");
        KeyValueHelper.TableAttribute tableAttribute = kvattributes[idx];
        Dictionary<string, bool> dictionary;
        bool flag;
        if (string.IsNullOrEmpty(key4) || !tableAttribute.ScreensHidden.TryGetValue(key4, out dictionary) || ((string.IsNullOrEmpty(key3) || !dictionary.TryGetValue(key3, out flag) ? (dictionary.TryGetValue(string.Empty, out flag) ? 1 : 0) : 1) & (flag ? 1 : 0)) == 0 || !(e.ReturnState is PXFieldState returnState2))
          return;
        returnState2.Visible = false;
      });
      this.FieldVerifyingEvents[lower] += (PXFieldVerifying) ((c, e) =>
      {
        PXFieldVerifying fieldVerifying = kvattributes[idx].FieldVerifying;
        if (fieldVerifying == null)
          return;
        fieldVerifying(c, e);
      });
      this.ExceptionHandlingEvents[lower] += (PXExceptionHandling) ((c, e) =>
      {
        PXExceptionHandling exceptionHandling = kvattributes[idx].ExceptionHandling;
        if (exceptionHandling == null)
          return;
        exceptionHandling(c, e);
      });
      this.CommandPreparingEvents[lower] += (PXCommandPreparing) ((c, e) =>
      {
        PXCommandPreparing commandPreparing = kvattributes[idx].CommandPreparing;
        if (commandPreparing == null)
          return;
        commandPreparing(c, e);
      });
    }

    void SetDefaultUdfValues(PXCache c, object row)
    {
      string str1 = string.Empty;
      if (udfTypeField != null)
        str1 = c.GetValue(row, udfTypeField)?.ToString().TrimEnd();
      foreach (KeyValueHelper.TableAttribute tableAttribute in kvattributes)
      {
        if ((pxCache.GetValueExt(row, tableAttribute.FieldName) is PXFieldState valueExt3 ? valueExt3.Value : (object) null) == null)
        {
          string str2;
          tableAttribute.DefaulValues.TryGetValue(str1 ?? string.Empty, out str2);
          if (str2 == null)
            tableAttribute.DefaulValues.TryGetValue(string.Empty, out str2);
          if (!string.IsNullOrEmpty(str2))
          {
            object newValue = (object) str2;
            pxCache.RaiseFieldUpdating(tableAttribute.FieldName, row, ref newValue);
          }
        }
      }
    }
  }

  private void FillEventsRowAttr(PXCache.AlteredDescriptor altered)
  {
    Dictionary<object, object> clones;
    Dictionary<string, PXCache.EventsFieldMap> eventsFieldMap;
    List<IPXRowSelectingSubscriber> rowSelecting;
    List<IPXRowSelectedSubscriber> rowSelected;
    List<IPXRowInsertingSubscriber> rowInserting;
    List<IPXRowInsertedSubscriber> rowInserted;
    List<IPXRowUpdatingSubscriber> rowUpdating;
    List<IPXRowUpdatedSubscriber> rowUpdated;
    List<IPXRowDeletingSubscriber> rowDeleting;
    List<IPXRowDeletedSubscriber> rowDeleted;
    List<IPXRowPersistingSubscriber> rowPersisting;
    List<IPXRowPersistedSubscriber> rowPersisted;
    if (altered != null)
    {
      clones = new Dictionary<object, object>(altered._FieldAttributes.Count);
      this._AttributesFirst = altered._FieldAttributesFirst;
      this._AttributesLast = altered._FieldAttributesLast;
      this._FirstKeyValueStored = altered._FirstKeyValueStored;
      this._KeyValueStoredOrdinals = altered._KeyValueStoredOrdinals;
      this._CacheAttributes = new List<PXEventSubscriberAttribute>(altered._FieldAttributes.Count);
      foreach (PXEventSubscriberAttribute fieldAttribute in altered._FieldAttributes)
      {
        PXEventSubscriberAttribute clone = fieldAttribute.Clone(PXAttributeLevel.Cache);
        this._CacheAttributes.Add(clone);
        this.AddAggregatedAttributes(ref clones, fieldAttribute, clone);
      }
      eventsFieldMap = altered._EventsFieldMap;
      rowSelecting = altered._EventsRowMap.RowSelecting;
      rowSelected = altered._EventsRowMap.RowSelected;
      rowInserting = altered._EventsRowMap.RowInserting;
      rowInserted = altered._EventsRowMap.RowInserted;
      rowUpdating = altered._EventsRowMap.RowUpdating;
      rowUpdated = altered._EventsRowMap.RowUpdated;
      rowDeleting = altered._EventsRowMap.RowDeleting;
      rowDeleted = altered._EventsRowMap.RowDeleted;
      rowPersisting = altered._EventsRowMap.RowPersisting;
      rowPersisted = altered._EventsRowMap.RowPersisted;
    }
    else
    {
      clones = new Dictionary<object, object>(this._FieldAttributes.Count);
      this._AttributesFirst = this._FieldAttributesFirst;
      this._AttributesLast = this._FieldAttributesLast;
      this._CacheAttributes = new List<PXEventSubscriberAttribute>(this._FieldAttributes.Count);
      foreach (PXEventSubscriberAttribute fieldAttribute in this._FieldAttributes)
      {
        PXEventSubscriberAttribute clone = fieldAttribute.Clone(PXAttributeLevel.Cache);
        this._CacheAttributes.Add(clone);
        this.AddAggregatedAttributes(ref clones, fieldAttribute, clone);
      }
      eventsFieldMap = this._EventsFieldMap;
      rowSelecting = this._EventsRowMap.RowSelecting;
      rowSelected = this._EventsRowMap.RowSelected;
      rowInserting = this._EventsRowMap.RowInserting;
      rowInserted = this._EventsRowMap.RowInserted;
      rowUpdating = this._EventsRowMap.RowUpdating;
      rowUpdated = this._EventsRowMap.RowUpdated;
      rowDeleting = this._EventsRowMap.RowDeleting;
      rowDeleted = this._EventsRowMap.RowDeleted;
      rowPersisting = this._EventsRowMap.RowPersisting;
      rowPersisted = this._EventsRowMap.RowPersisted;
    }
    foreach (KeyValuePair<string, PXCache.EventsFieldMap> keyValuePair in eventsFieldMap)
    {
      if (keyValuePair.Value.CommandPreparing.Count > 0)
      {
        IPXCommandPreparingSubscriber[] preparingSubscriberArray = new IPXCommandPreparingSubscriber[keyValuePair.Value.CommandPreparing.Count];
        for (int index = 0; index < preparingSubscriberArray.Length; ++index)
          preparingSubscriberArray[index] = (IPXCommandPreparingSubscriber) clones[(object) keyValuePair.Value.CommandPreparing[index]];
        this._CommandPreparingEventsAttr[keyValuePair.Key] = preparingSubscriberArray;
      }
      if (keyValuePair.Value.FieldDefaulting.Count > 0)
      {
        IPXFieldDefaultingSubscriber[] defaultingSubscriberArray = new IPXFieldDefaultingSubscriber[keyValuePair.Value.FieldDefaulting.Count];
        for (int index = 0; index < defaultingSubscriberArray.Length; ++index)
          defaultingSubscriberArray[index] = (IPXFieldDefaultingSubscriber) clones[(object) keyValuePair.Value.FieldDefaulting[index]];
        this._FieldDefaultingEventsAttr[keyValuePair.Key] = defaultingSubscriberArray;
      }
      if (keyValuePair.Value.FieldUpdating.Count > 0)
      {
        IPXFieldUpdatingSubscriber[] updatingSubscriberArray = new IPXFieldUpdatingSubscriber[keyValuePair.Value.FieldUpdating.Count];
        for (int index = 0; index < updatingSubscriberArray.Length; ++index)
          updatingSubscriberArray[index] = (IPXFieldUpdatingSubscriber) clones[(object) keyValuePair.Value.FieldUpdating[index]];
        this._FieldUpdatingEventsAttr[keyValuePair.Key] = updatingSubscriberArray;
      }
      if (keyValuePair.Value.FieldVerifying.Count > 0)
      {
        IPXFieldVerifyingSubscriber[] verifyingSubscriberArray = new IPXFieldVerifyingSubscriber[keyValuePair.Value.FieldVerifying.Count];
        for (int index = 0; index < verifyingSubscriberArray.Length; ++index)
          verifyingSubscriberArray[index] = (IPXFieldVerifyingSubscriber) clones[(object) keyValuePair.Value.FieldVerifying[index]];
        this._FieldVerifyingEventsAttr[keyValuePair.Key] = verifyingSubscriberArray;
      }
      if (keyValuePair.Value.FieldUpdated.Count > 0)
      {
        IPXFieldUpdatedSubscriber[] updatedSubscriberArray = new IPXFieldUpdatedSubscriber[keyValuePair.Value.FieldUpdated.Count];
        for (int index = 0; index < updatedSubscriberArray.Length; ++index)
          updatedSubscriberArray[index] = (IPXFieldUpdatedSubscriber) clones[(object) keyValuePair.Value.FieldUpdated[index]];
        this._FieldUpdatedEventsAttr[keyValuePair.Key] = updatedSubscriberArray;
      }
      if (keyValuePair.Value.FieldSelecting.Count > 0)
      {
        IPXFieldSelectingSubscriber[] attributes = new IPXFieldSelectingSubscriber[keyValuePair.Value.FieldSelecting.Count];
        for (int index = 0; index < attributes.Length; ++index)
          attributes[index] = (IPXFieldSelectingSubscriber) clones[(object) keyValuePair.Value.FieldSelecting[index]];
        IPXFieldSelectingSubscriber[] selectingSubscriberArray = this.SortPXAttributes(attributes);
        this._FieldSelectingEventsAttr[keyValuePair.Key] = selectingSubscriberArray;
      }
      if (keyValuePair.Value.ExceptionHandling.Count > 0)
      {
        IPXExceptionHandlingSubscriber[] handlingSubscriberArray = new IPXExceptionHandlingSubscriber[keyValuePair.Value.ExceptionHandling.Count];
        for (int index = 0; index < handlingSubscriberArray.Length; ++index)
          handlingSubscriberArray[index] = (IPXExceptionHandlingSubscriber) clones[(object) keyValuePair.Value.ExceptionHandling[index]];
        this._ExceptionHandlingEventsAttr[keyValuePair.Key] = handlingSubscriberArray;
      }
    }
    if (rowSelecting.Count > 0)
    {
      IPXRowSelectingSubscriber[] selectingSubscriberArray = new IPXRowSelectingSubscriber[rowSelecting.Count];
      for (int index = 0; index < selectingSubscriberArray.Length; ++index)
        selectingSubscriberArray[index] = (IPXRowSelectingSubscriber) clones[(object) rowSelecting[index]];
      this._EventsRowAttr.RowSelecting = selectingSubscriberArray;
    }
    if (rowSelected.Count > 0)
    {
      IPXRowSelectedSubscriber[] selectedSubscriberArray = new IPXRowSelectedSubscriber[rowSelected.Count];
      for (int index = 0; index < selectedSubscriberArray.Length; ++index)
        selectedSubscriberArray[index] = (IPXRowSelectedSubscriber) clones[(object) rowSelected[index]];
      this._EventsRowAttr.RowSelected = selectedSubscriberArray;
    }
    if (rowInserting.Count > 0)
    {
      IPXRowInsertingSubscriber[] insertingSubscriberArray = new IPXRowInsertingSubscriber[rowInserting.Count];
      for (int index = 0; index < insertingSubscriberArray.Length; ++index)
        insertingSubscriberArray[index] = (IPXRowInsertingSubscriber) clones[(object) rowInserting[index]];
      this._EventsRowAttr.RowInserting = insertingSubscriberArray;
    }
    if (rowInserted.Count > 0)
    {
      IPXRowInsertedSubscriber[] insertedSubscriberArray = new IPXRowInsertedSubscriber[rowInserted.Count];
      for (int index = 0; index < insertedSubscriberArray.Length; ++index)
        insertedSubscriberArray[index] = (IPXRowInsertedSubscriber) clones[(object) rowInserted[index]];
      this._EventsRowAttr.RowInserted = insertedSubscriberArray;
    }
    if (rowUpdating.Count > 0)
    {
      IPXRowUpdatingSubscriber[] updatingSubscriberArray = new IPXRowUpdatingSubscriber[rowUpdating.Count];
      for (int index = 0; index < updatingSubscriberArray.Length; ++index)
        updatingSubscriberArray[index] = (IPXRowUpdatingSubscriber) clones[(object) rowUpdating[index]];
      this._EventsRowAttr.RowUpdating = updatingSubscriberArray;
    }
    if (rowUpdated.Count > 0)
    {
      IPXRowUpdatedSubscriber[] updatedSubscriberArray = new IPXRowUpdatedSubscriber[rowUpdated.Count];
      for (int index = 0; index < updatedSubscriberArray.Length; ++index)
        updatedSubscriberArray[index] = (IPXRowUpdatedSubscriber) clones[(object) rowUpdated[index]];
      this._EventsRowAttr.RowUpdated = updatedSubscriberArray;
    }
    if (rowDeleting.Count > 0)
    {
      IPXRowDeletingSubscriber[] deletingSubscriberArray = new IPXRowDeletingSubscriber[rowDeleting.Count];
      for (int index = 0; index < deletingSubscriberArray.Length; ++index)
        deletingSubscriberArray[index] = (IPXRowDeletingSubscriber) clones[(object) rowDeleting[index]];
      this._EventsRowAttr.RowDeleting = deletingSubscriberArray;
    }
    if (rowDeleted.Count > 0)
    {
      IPXRowDeletedSubscriber[] deletedSubscriberArray = new IPXRowDeletedSubscriber[rowDeleted.Count];
      for (int index = 0; index < deletedSubscriberArray.Length; ++index)
        deletedSubscriberArray[index] = (IPXRowDeletedSubscriber) clones[(object) rowDeleted[index]];
      this._EventsRowAttr.RowDeleted = deletedSubscriberArray;
    }
    if (rowPersisting.Count > 0)
    {
      IPXRowPersistingSubscriber[] persistingSubscriberArray = new IPXRowPersistingSubscriber[rowPersisting.Count];
      for (int index = 0; index < persistingSubscriberArray.Length; ++index)
        persistingSubscriberArray[index] = (IPXRowPersistingSubscriber) clones[(object) rowPersisting[index]];
      this._EventsRowAttr.RowPersisting = persistingSubscriberArray;
    }
    if (rowPersisted.Count <= 0)
      return;
    IPXRowPersistedSubscriber[] persistedSubscriberArray = new IPXRowPersistedSubscriber[rowPersisted.Count];
    for (int index = 0; index < persistedSubscriberArray.Length; ++index)
      persistedSubscriberArray[index] = (IPXRowPersistedSubscriber) clones[(object) rowPersisted[index]];
    this._EventsRowAttr.RowPersisted = persistedSubscriberArray;
  }

  private IPXFieldSelectingSubscriber[] SortPXAttributes(IPXFieldSelectingSubscriber[] attributes)
  {
    if (attributes == null)
      return (IPXFieldSelectingSubscriber[]) null;
    if (((IEnumerable<IPXFieldSelectingSubscriber>) attributes).All<IPXFieldSelectingSubscriber>((Func<IPXFieldSelectingSubscriber, bool>) (attr => !(attr is IPXUIControlFieldAttribute))))
      return attributes;
    List<IPXFieldSelectingSubscriber> selectingSubscriberList = new List<IPXFieldSelectingSubscriber>(attributes.Length);
    foreach (IPXFieldSelectingSubscriber attribute in attributes)
    {
      if (!(attribute is IPXUIControlFieldAttribute))
        selectingSubscriberList.Add(attribute);
    }
    foreach (IPXFieldSelectingSubscriber attribute in attributes)
    {
      if (attribute is IPXUIControlFieldAttribute)
        selectingSubscriberList.Add(attribute);
    }
    return selectingSubscriberList.ToArray();
  }

  /// <summary>Creates a data record from the <tt>PXDataRecord</tt> object
  /// and places it into the cache with the <tt>NotChanged</tt> status if
  /// the data record isn't found among the modified data records in the
  /// cache.</summary>
  /// <remarks>
  /// <para>If <tt>isReadOnly</tt> is <tt>false</tt> then:</para>
  /// <list type="bullet">
  /// <item><description>If the cache already contains
  /// the data record with the same keys and the <tt>NotChanged</tt> status,
  /// the method returns this data record updated to the state of
  /// <tt>PXDataRecord</tt>.</description></item>
  /// <item><description>If the
  /// cache contains the same data record with the <tt>Updated</tt> or
  /// <tt>Inserted</tt> status, the method returns this data
  /// record.</description></item>
  /// </list>
  /// <para>In other cases and when <tt>isReadonly</tt> is <tt>true</tt>,
  /// the method returns the data record created from the
  /// <tt>PXDataRecord</tt> object.</para>
  /// <para>If the <tt>AllowSelect</tt> property is <tt>false</tt>, the
  /// methods returns a new empty data record and the logic described above
  /// is not executed.</para>
  /// <para>The method raises the <tt>RowSelecting</tt> event.</para>
  /// </remarks>
  /// <param name="record">The <tt>PXDataRecord</tt> object to convert to
  /// the DAC type of the cache.</param>
  /// <param name="position">The index of the first field to read in
  /// the list of columns comprising the <tt>PXDataRecord</tt>
  /// object.</param>
  /// <param name="isReadOnly">The value indicating if the data record with
  /// the same key fields should be located in the cache and
  /// updated.</param>
  /// <param name="bool">The value indicating whether the data record
  /// with the same keys existed in the cache among the modified data
  /// records.</param>
  public override object Select(
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated)
  {
    wasUpdated = false;
    TNode item = new TNode();
    this.OnRowSelecting((object) item, record, ref position, isReadOnly || this._Interceptor != null && !this._Interceptor.CacheSelected);
    if (isReadOnly || this._Interceptor != null && !this._Interceptor.CacheSelected)
      return (object) item;
    if (this._ItemsDenormalized)
    {
      this._Items.Normalize(default (TNode));
      this._ItemsDenormalized = false;
    }
    object placed = this._ChangedKeys == null || !this._ChangedKeys.ContainsKey(item) ? (object) this._Items.PlaceNotChanged(item, true, out wasUpdated) : (object) this._Items.PlaceNotChanged(this._ChangedKeys[item], true, out wasUpdated);
    if ((object) this._CurrentPlacedIntoCache != null && !wasUpdated && placed == (object) this._CurrentPlacedIntoCache)
    {
      bool flag = true;
      if (this._TimestampOrdinal.HasValue)
      {
        byte[] second = this.GetValue(placed, this._TimestampOrdinal.Value) as byte[];
        if (this.GetValue((object) item, this._TimestampOrdinal.Value) is byte[] first)
        {
          if (second != null)
            flag = PXDatabase.Provider.SqlDialect.CompareTimestamps(first, second) > 0;
        }
        else
          flag = false;
      }
      if (flag)
      {
        record.AddRowSelecting((System.Action) (() => this.RestoreCopy(placed, (object) item)));
        this._CurrentPlacedIntoCache = default (TNode);
      }
    }
    return placed;
  }

  internal override object CreateItem(PXDataRecord record, ref int position, bool isReadOnly)
  {
    TNode node = new TNode();
    this.OnRowSelecting((object) node, record, ref position, isReadOnly || this._Interceptor != null && !this._Interceptor.CacheSelected);
    return (object) node;
  }

  internal override object Select(
    PXDataRecord record,
    object row,
    bool isReadOnly,
    out bool wasUpdated)
  {
    wasUpdated = false;
    TNode item = (TNode) row;
    if (isReadOnly || this._Interceptor != null && !this._Interceptor.CacheSelected)
      return (object) item;
    if (this._ItemsDenormalized)
    {
      this._Items.Normalize(default (TNode));
      this._ItemsDenormalized = false;
    }
    object placed = this._ChangedKeys == null || !this._ChangedKeys.ContainsKey(item) ? (object) this._Items.PlaceNotChanged(item, true, out wasUpdated) : (object) this._Items.PlaceNotChanged(this._ChangedKeys[item], true, out wasUpdated);
    if (placed == null)
      placed = (object) this._Items.Locate(item);
    if ((object) this._CurrentPlacedIntoCache != null && !wasUpdated && placed == (object) this._CurrentPlacedIntoCache)
    {
      bool flag = true;
      if (this._TimestampOrdinal.HasValue)
      {
        byte[] second = this.GetValue(placed, this._TimestampOrdinal.Value) as byte[];
        if (this.GetValue((object) item, this._TimestampOrdinal.Value) is byte[] first)
        {
          if (second != null)
            flag = PXDatabase.Provider.SqlDialect.CompareTimestamps(first, second) > 0;
        }
        else
          flag = false;
      }
      if (flag)
      {
        record.AddRowSelecting((System.Action) (() => this.RestoreCopy(placed, (object) item)));
        this._CurrentPlacedIntoCache = default (TNode);
      }
    }
    return placed;
  }

  /// <summary>Returns the provided data record status. The <see cref="T:PX.Data.PXEntryStatus">PXEntryStatus</see> enumeration defines the possible status values. For example, the status can indicate
  /// whether the data record has been inserted, updated, or deleted.</summary>
  /// <param name="item">The data record whose status is requested.</param>
  /// <example><para>The code snippet below shows how to check a data record status in an event handler.</para>
  /// <code title="Example" lang="CS">
  /// protected virtual void Vendor_RowSelected(PXCache sender,
  ///                                           PXRowSelectedEventArgs e)
  /// {
  ///     Vendor vend = e.Row as Vendor;
  ///     if (vend != null &amp;&amp; sender.GetStatus(vend) == PXEntryStatus.Notchanged)
  ///     {
  ///         ...
  ///     }
  /// }</code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override PXEntryStatus GetStatus(object item) => this._Items.GetStatus((TNode) item);

  /// <summary>Sets the status to the provided data record. The <see cref="T:PX.Data.PXEntryStatus">PXEntryStatus</see> enumeration
  /// defines the possible status values.</summary>
  /// <param name="item">The data record to set status to.</param>
  /// <param name="status">The new status.</param>
  /// <example>
  /// The code below checks the status of a data record and sets the status
  /// to <tt>Updated</tt> if the status is <tt>Notchanged</tt>.
  /// <code>
  /// if (Transactions.Cache.GetStatus(tran) == PXEntryStatus.Notchanged)
  /// {
  ///     Transactions.Cache.SetStatus(tran, PXEntryStatus.Updated);
  /// }</code>
  /// </example>
  public override void SetStatus(object item, PXEntryStatus status)
  {
    if (item == null)
      return;
    PXEntryStatus? existingStatus = this._Items.SetStatus((TNode) item, status);
    if (status != PXEntryStatus.Updated)
      return;
    this.EnsureUnmodified(item, existingStatus, new PXEntryStatus?(status));
  }

  protected internal override void EnsureUnmodified(
    object item,
    PXEntryStatus? existingStatus,
    PXEntryStatus? newStatus)
  {
    if (EnumerableExtensions.IsNotIn<PXEntryStatus?>(existingStatus, new PXEntryStatus?(PXEntryStatus.Inserted), new PXEntryStatus?(PXEntryStatus.InsertedDeleted)))
    {
      TNode node = (TNode) item;
      TNode placed = (TNode) item;
      PXEntryStatus? nullable = existingStatus;
      PXEntryStatus pxEntryStatus = PXEntryStatus.Notchanged;
      int num = nullable.GetValueOrDefault() == pxEntryStatus & nullable.HasValue ? 1 : 0;
      this._FetchOriginals(node, placed, num != 0);
      object original = this.GetOriginal(item);
      if (original != null)
      {
        try
        {
          this.SetSessionUnmodified(item, original, newStatus);
        }
        catch
        {
        }
      }
    }
    this.UpdateLastModified(item, false);
  }

  /// <summary>Searches the cache for a data record that has the same key
  /// fields as the provided data record. If the data record is not found in
  /// the cache, the method retrieves the data record from the database and
  /// places it into the cache with the <tt>NotChanged</tt> status. The
  /// method returns the located or retrieved data record.</summary>
  /// <remarks>The <tt>AllowSelect</tt> property does not affect this method
  /// unlike the <see cref="M:PX.Data.PXCache`1.Locate(System.Collections.IDictionary)">Locate(IDictionary)</see>
  /// method.</remarks>
  /// <param name="item">The data record to locate in the cache.</param>
  /// <example>
  /// <code>
  /// public PXSelectJoin&lt;SOAdjust,
  ///     InnerJoin&lt;ARPayment, On&lt;ARPayment.docType, Equal&lt;SOAdjust.adjgDocType&gt;,
  ///         And&lt;ARPayment.refNbr, Equal&lt;SOAdjust.adjgRefNbr&gt;&gt;&gt;&gt;&gt; Adjustments;
  /// ...
  /// // The optional delegate of the Adjustment data view to replace the
  /// // output of the Select() method
  /// public virtual IEnumerable adjustments()
  /// {
  ///     ...
  ///     SOAdjust adj = new SOAdjust();
  /// 
  ///     // Setting the key fields
  ///     adj.CustomerID = Document.Current.CustomerID;
  ///     adj.AdjdOrderType = Document.Current.OrderType;
  ///     adj.AdjdOrderNbr = Document.Current.OrderNbr;
  ///     adj.AdjgDocType = payment.DocType;
  ///     adj.AdjgRefNbr = payment.RefNbr;
  /// 
  ///     // Searching the cache for the Adjustment data record with
  ///     // the same key fields
  ///     if (Adjustments.Cache.Locate(adj) == null)
  ///     {
  ///         yield return new PXResult&lt;SOAdjust, ARPayment&gt;(Adjustments.Insert(adj), payment);
  ///     }
  ///     ...
  /// }
  /// </code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object Locate(object item) => (object) this._Items.Locate((TNode) item);

  protected internal override bool IsPresent(object item)
  {
    TNode node;
    if (!this.IsKeysFilled((TNode) item) || (object) (node = this._Items.Locate((TNode) item)) == null)
      return false;
    return (object) this._CurrentPlacedIntoCache == null || (object) node != (object) this._CurrentPlacedIntoCache;
  }

  protected internal override bool IsGraphSpecificField(string fieldName)
  {
    return this._GraphSpecificFields != null && fieldName != null && this._GraphSpecificFields.Contains(fieldName);
  }

  /// <summary>Searches the cache for a data record that has the same key
  /// fields as in the provided dictionary. If the data record is not found
  /// in the cache, the method initializes a new data record with the
  /// provided values and places it into the cache with the
  /// <tt>NotChanged</tt> status.</summary>
  /// <remarks>Returns 1 if a data record is successfully located or placed
  /// into the cache, and returns 0 if placing into the cache fails or the
  /// <tt>AllowSelect</tt> property is <tt>false</tt>.</remarks>
  /// <param name="keys">The dictionary with values to initialize the data
  /// record fields. The dictionary keys are field names.</param>
  public override int Locate(IDictionary keys)
  {
    if (!this._AllowSelect)
      return 0;
    TNode node1 = new TNode();
    this.FillWithValues(node1, default (TNode), keys, PXCacheOperation.Update);
    TNode node2 = this.Locate(node1);
    if ((object) node2 == null && (object) this.readItem(node1) != null)
      node2 = this._Items.PlaceNotChanged(node1, out bool _);
    if ((object) node2 == null)
      return 0;
    this.Current = (object) node2;
    return 1;
  }

  protected bool NonDBTable
  {
    get
    {
      if (!this._NonDBTable.HasValue)
        this._NonDBTable = new bool?(PXDatabase.Provider.SchemaCache.GetTableHeader(PXCache<TNode>._BqlTable.Name) == null && (this.BqlSelect == null || PXDatabase.Provider.SchemaCache.GetTableHeader(PXCache.GetBqlTable(((IEnumerable<System.Type>) this.BqlSelect.GetTables()).FirstOrDefault<System.Type>())?.Name) == null));
      return this._NonDBTable.Value;
    }
  }

  public override int LocateByNoteID(Guid noteId) => this.LocateByNoteID(noteId, "NoteID");

  internal override int LocateByNoteID(Guid noteId, string noteIdField)
  {
    if (!this._AllowSelect)
      return 0;
    Guid result1;
    if (this.Current != null && Guid.TryParse(this.GetValueExt(this.Current, noteIdField).ToString(), out result1) && result1 == noteId)
      return 1;
    foreach (object data in this._Items)
    {
      Guid result2;
      if (Guid.TryParse(this.GetValueExt(data, noteIdField).ToString(), out result2) && result2 == noteId)
      {
        this.Current = data;
        return 1;
      }
    }
    TNode node1 = new TNode();
    this.FillWithValues(node1, default (TNode), (IDictionary) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      {
        noteIdField,
        noteId.ToString()
      }
    }, PXCacheOperation.Update);
    TNode node2 = this.readItemByNoteID(node1, noteIdField);
    if ((object) node2 != null)
      node2 = this._Items.PlaceNotChanged(node2, out bool _);
    if ((object) node2 == null)
      return 0;
    this.Current = (object) node2;
    return 1;
  }

  /// <summary>Completely removes provided data recors from the cache object without raising any events.</summary>
  /// <param name="item">The data record to remove from the cache.</param>
  /// <remarks>Please, keep in mind that this method will not remove any records from the database itself but only from the cache.</remarks>
  /// <example><para>The code below locates a data record in the cache and, if the data record has not been changed, silently removes it from the cache. (The Held status indicates that a data record has not been changed but needs to the preserved in the session.</para>
  /// <code title="Example" lang="CS">
  /// // Searching the data record by its key fields in the cache
  /// object cached = sender.Locate(item);
  /// // Checking the status
  /// if (cached != null &amp;&amp; (sender.GetStatus(cached) == PXEntryStatus.Held ||
  ///                        sender.GetStatus(cached) == PXEntryStatus.Notchanged))
  /// {
  ///     // Removing without events
  ///     sender.Remove(cached);
  /// }</code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void Remove(object item) => this._Items.Remove((TNode) item);

  /// <summary>
  /// Reads a row from the Database. Raises the <tt>OnRowSelecting</tt> event.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected virtual TNode readItem(TNode item) => this.readItem(item, false);

  protected virtual TNode readItemByNoteID(TNode item) => this.readItemByNoteID(item, "NoteID");

  protected virtual TNode readItemByNoteID(TNode item, string noteIdField)
  {
    return this.readItem(item, false, noteIdField, true);
  }

  protected virtual TNode readItem(TNode item, bool donotplace, bool byNoteID = false)
  {
    return this.readItem(item, donotplace, "NoteID", byNoteID);
  }

  protected virtual TNode readItem(TNode item, bool donotplace, string noteIdField, bool byNoteID = false)
  {
    if (this.DisableReadItem)
      return !this.IsKeysFilled(item) ? default (TNode) : item;
    if (this.IsKeysFilled(item) || byNoteID && this.GetValueExt((object) item, noteIdField) != null)
    {
      if (this.hasUnexistingKey((object) item))
        return default (TNode);
      if (this._Interceptor == null)
      {
        List<PXDataField> pxDataFieldList = new List<PXDataField>();
        foreach (string classField in this._ClassFields)
        {
          object obj = (object) null;
          bool flag1 = this._Keys.Contains(classField);
          bool flag2 = string.Equals(classField, noteIdField, StringComparison.OrdinalIgnoreCase);
          if (flag1 || flag2 & byNoteID)
            obj = this.GetValue((object) item, classField);
          if (this._KeyValueStoredNames != null || this._KeyValueAttributeNames == null)
            flag2 = false;
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          this.OnCommandPreparing(classField, (object) item, obj, PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
          if (description != null)
          {
            if (flag1)
            {
              if (!byNoteID && description.DataValue == null)
                return default (TNode);
            }
            else if (description.IsRestriction)
            {
              obj = this.GetValue((object) item, classField);
              this.OnCommandPreparing(classField, (object) item, obj, PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
            }
            if (description != null && description.Expr != null)
            {
              pxDataFieldList.Add(new PXDataField(description.Expr));
              if (obj != null)
              {
                SQLExpression fieldName = description.Expr.Duplicate();
                foreach (NoteIdExpression noteIdExpression in fieldName.GetExpressionsOfType<NoteIdExpression>())
                  noteIdExpression.IgnoreNulls = true;
                pxDataFieldList.Add((PXDataField) new PXDataFieldValue(fieldName, description.DataType, description.DataLength, description.DataValue));
              }
              if (flag2 && this.prepareKvExtField(this._FirstKeyValueAttribute.Value.Key, (object) null, description.BqlTable, PXDBOperation.Select, ref description))
                pxDataFieldList.Add(new PXDataField(description.Expr));
            }
          }
        }
        string recordStatusFieldName;
        if (PXDatabaseRecordStatusHelper.IsDatabaseRecordStatusNeeded(PXCache<TNode>._BqlTable, (PXCache) this, out recordStatusFieldName))
        {
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          this.OnCommandPreparing(recordStatusFieldName, (object) item, (object) null, PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
          if (description != null && description.Expr != null)
            pxDataFieldList.Add(new PXDataField(description.Expr));
        }
        string deletedDatabaseRecordFieldName;
        if (PXDeletedDatabaseRecordHelper.IsDeletedDatabaseRecordNeeded(PXCache<TNode>._BqlTable, (PXCache) this, out deletedDatabaseRecordFieldName))
        {
          PXCommandPreparingEventArgs.FieldDescription description;
          this.OnCommandPreparing(deletedDatabaseRecordFieldName, (object) item, (object) null, PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
          if (description != null && description.Expr != null)
            pxDataFieldList.Add(new PXDataField(description.Expr));
        }
        using (PXDataRecord record = this._Graph.ProviderSelectSingle(PXCache<TNode>._BqlTable, pxDataFieldList.ToArray()))
        {
          if (record != null)
          {
            TNode node = new TNode();
            int position = 0;
            this.OnRowSelecting((object) node, record, ref position, donotplace);
            return donotplace ? node : this._Items.PlaceNotChanged(node, out bool _);
          }
        }
      }
      else
      {
        List<PXDataValue> pxDataValueList = new List<PXDataValue>();
        BqlCommand command;
        if (byNoteID)
        {
          if (!this._Interceptor.CanSelectByNoteId)
            return default (TNode);
          command = this._Interceptor.GetRowByNoteIdCommand();
          string lowerInvariant = noteIdField.ToLowerInvariant();
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          this.OnCommandPreparing(lowerInvariant, (object) item, this.GetValue((object) item, lowerInvariant), PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
          pxDataValueList.Add(new PXDataValue(description.DataType, description.DataValue));
        }
        else
        {
          foreach (string classField in this._ClassFields)
          {
            if (this._Keys.Contains(classField))
            {
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              this.OnCommandPreparing(classField, (object) item, this.GetValue((object) item, classField), PXDBOperation.Internal | PXDBOperation.ReadItem, (System.Type) null, out description);
              if (description != null && description.DataValue == null)
                return default (TNode);
              pxDataValueList.Add(new PXDataValue(description.DataType, description.DataLength, description.DataValue));
            }
          }
          command = this._Interceptor.GetRowCommand();
        }
        using (IEnumerator<PXDataRecord> enumerator = this._Graph.ProviderSelect(command, 1, pxDataValueList.ToArray()).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            PXDataRecord current = enumerator.Current;
            TNode node = new TNode();
            int position = 0;
            this.OnRowSelecting((object) node, current, ref position, donotplace);
            return donotplace ? node : this._Items.PlaceNotChanged(node, out bool _);
          }
        }
      }
    }
    return default (TNode);
  }

  /// <summary>Updates the data record in the cache with the provided
  /// values.</summary>
  /// <remarks>
  /// <para>The method initalizes a data record with the provided key
  /// fields. If the data record with such keys does not exist in the cache,
  /// the method tries to retrieve it from the database. If the data record
  /// exists in the cache or database, it gets the <tt>Updated</tt> status.
  /// If the data record does not exist in the database, the method inserts
  /// a new data record into the cache with the <tt>Inserted</tt>
  /// status.</para>
  /// <para>The method raises the following events: <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, <tt>FieldUpdated</tt>, <tt>RowUpdating</tt>,
  /// and <tt>RowUpdated</tt>. See <a href="Update.html">Updating a Data Record</a> for
  /// the events flowchart. If the data record does not exist in the
  /// database, the method also causes the events of the <see cref="M:PX.Data.PXCache`1.Insert(System.Object)">Insert(object)</see>
  /// method.</para>
  /// <para>If the <tt>AllowUpdate</tt> property is <tt>false</tt>, the data
  /// record is not updated and the methods returns 0. The method returns 1
  /// if the data record is successfully updated or inserted.</para>
  /// </remarks>
  /// <param name="keys">The values of the key fields of the data record to
  /// update.</param>
  /// <param name="values">The new values with which the data record fields
  /// are updated.</param>
  public override int Update(IDictionary keys, IDictionary values)
  {
    if (!this._AllowUpdate)
      return 0;
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmUpdate)))
    {
      TNode key1 = new TNode();
      this.FillWithValues(key1, default (TNode), keys, PXCacheOperation.Update);
      if (this.Graph.IsContractBasedAPI)
      {
        foreach (string key2 in keys.Keys.ToArray<object>())
        {
          if (key2.EndsWith("_OriginalValue", StringComparison.OrdinalIgnoreCase))
            keys.Remove((object) key2);
        }
      }
      if (this._ChangedKeys != null && this._ChangedKeys.ContainsKey(key1))
        throw new PXBadDictinaryException();
      if (this.Graph.ShouldSaveVersionModified && this.Graph.IsMobile)
      {
        this.Graph.VersionedState.CreateNewVersion();
        this.Graph.ShouldSaveVersionModified = false;
      }
      TNode node1 = this._Items.PlaceUpdated(key1, false);
      BqlTablePair bqlTablePair1 = (BqlTablePair) null;
      if ((object) node1 == null && this.Keys.Count == 0)
        node1 = this.Current as TNode;
      if ((object) node1 == null)
      {
        TNode node2;
        if ((object) (node2 = this.readItem(key1)) != null)
        {
          node1 = this._Items.PlaceUpdated(key1, true);
          try
          {
            if ((object) node1 != null)
            {
              List<object> objectList1 = (List<object>) null;
              List<object> objectList2 = (List<object>) null;
              VersionedModifiedPair versionedModifiedPair = (VersionedModifiedPair) null;
              if (this._Originals.TryGetValue((IBqlTable) node1, out bqlTablePair1))
              {
                objectList1 = bqlTablePair1.Slots;
                objectList2 = bqlTablePair1.SlotsOriginal;
                versionedModifiedPair = bqlTablePair1.VersionedModified;
              }
              PXCacheOriginalCollection originals = this._Originals;
              // ISSUE: variable of a boxed type
              __Boxed<TNode> key3 = (object) node1;
              BqlTablePair bqlTablePair2 = new BqlTablePair();
              bqlTablePair2.Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(node2, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
              bqlTablePair2.Slots = objectList1;
              bqlTablePair2.SlotsOriginal = objectList2;
              bqlTablePair2.VersionedModified = versionedModifiedPair;
              bqlTablePair1 = bqlTablePair2;
              originals[(IBqlTable) key3] = bqlTablePair2;
            }
          }
          catch
          {
          }
        }
        else
        {
          if (!this._AllowInsert)
            return 0;
          Dictionary<string, object> values1 = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          bool flag1 = true;
          if (keys != null)
          {
            foreach (DictionaryEntry key4 in keys)
            {
              string key5 = (string) key4.Key;
              if (!this.Graph.IsImport || this.IsFieldEnabled(key5))
              {
                values1[key5] = key4.Value;
                if (key4.Value != null)
                  flag1 = false;
              }
            }
          }
          if (values != null)
          {
            foreach (DictionaryEntry dictionaryEntry in values)
            {
              if (!values1.ContainsKey((string) dictionaryEntry.Key) || dictionaryEntry.Value != PXCache.NotSetValue)
                values1[(string) dictionaryEntry.Key] = dictionaryEntry.Value;
            }
          }
          bool flag2 = false;
          bool isDirty = this._IsDirty;
          if (flag1 && !this._AllowInsert)
          {
            flag2 = true;
            this._AllowInsert = true;
          }
          int num;
          try
          {
            num = this.Insert((IDictionary) values1);
          }
          finally
          {
            if (flag2)
              this._AllowInsert = false;
          }
          if (flag2 && num > 0)
          {
            bool flag3 = false;
            foreach (string key6 in (IEnumerable<string>) this.Keys)
            {
              if (!values1.ContainsKey(key6) || PXFieldState.UnwrapValue(values1[key6]) == null)
                flag3 = true;
            }
            if (flag3)
            {
              this.Delete(this.Current);
              this._IsDirty = isDirty;
              return 0;
            }
          }
          if (values != null)
          {
            foreach (string key7 in values.Keys.ToArray<object>())
            {
              if (num > 0)
              {
                values[(object) key7] = values1[key7];
                if (values1.ContainsKey(key7 + "_OriginalValue"))
                  values[(object) (key7 + "_OriginalValue")] = values1[key7 + "_OriginalValue"];
              }
              else if (values1[key7] is PXFieldState pxFieldState && !string.IsNullOrEmpty(pxFieldState.Error))
                values[(object) key7] = (object) pxFieldState;
              else if (key7.EndsWith("_description", StringComparison.OrdinalIgnoreCase))
                values[(object) key7] = values1[key7];
            }
          }
          if (keys != null)
          {
            object[] array = keys.Keys.ToArray<object>();
            if (this.Graph.IsContractBasedAPI && array.Length == 0)
              array = (object[]) this.Keys.ToArray();
            foreach (string key8 in array)
            {
              if (num > 0)
              {
                object obj = values1[key8];
                if (obj is PXFieldState pxFieldState)
                  obj = pxFieldState.Value;
                keys[(object) key8] = obj;
              }
            }
          }
          return num;
        }
      }
      else if (!this.NonDBTable)
        bqlTablePair1 = this._FetchOriginals(key1, node1, true);
      if ((object) node1 == null)
        return 0;
      this.Current = (object) node1;
      TNode copy = PXCache<TNode>.CreateCopy(node1);
      if (bqlTablePair1 != null)
        bqlTablePair1.LastModified = (IBqlTable) copy;
      this.SetSessionUnmodified((object) node1, (object) copy);
      this.SetVersionModified((object) node1);
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      if (values != null)
      {
        if (!this._AllowUpdate)
          return 0;
        if (!this.DisableCloneAttributes)
          PXCache.TryDispose((object) this.GetAttributes((object) node1, (string) null));
        try
        {
          if (this.FillWithValues(node1, copy, values, PXCacheOperation.Update))
          {
            try
            {
              this._Items.Normalize(node1);
              flag5 = true;
            }
            catch (PXBadDictinaryException ex)
            {
              PXCache<TNode>.RestoreCopy(node1, copy);
              throw;
            }
            flag6 = true;
          }
        }
        catch (Exception ex)
        {
          flag4 = true;
          throw;
        }
        finally
        {
          if (flag4)
            PXCache<TNode>.RestoreCopy(node1, copy);
        }
      }
      try
      {
        flag4 = !this.OnRowUpdating((object) copy, (object) node1, true);
      }
      catch (Exception ex)
      {
        flag4 = true;
        throw;
      }
      finally
      {
        if (flag4)
        {
          PXCache<TNode>.RestoreCopy(node1, copy);
          if (flag5)
            this._Items.Normalize(default (TNode));
        }
      }
      if (flag4)
      {
        if (values != null)
        {
          foreach (string str in values.Keys.ToArray<object>())
          {
            if (this.Fields.Contains(str))
              values[(object) str] = this.GetValueExt((object) node1, str);
          }
        }
        if (keys != null)
        {
          foreach (string str in keys.Keys.ToArray<object>())
          {
            object valueExt = this.GetValueExt((object) node1, str);
            if (valueExt is PXFieldState pxFieldState)
              valueExt = pxFieldState.Value;
            keys[(object) str] = valueExt;
          }
        }
        return 0;
      }
      this._IsDirty = true;
      this.Current = (object) node1;
      this.OnRowUpdated((object) node1, (object) copy, true);
      if (values != null)
      {
        foreach (string str in values.Keys.ToArray<object>())
        {
          if (this.Fields.Contains(str))
            values[(object) str] = this.GetValueExt((object) node1, str);
        }
      }
      if (keys != null)
      {
        object[] array = keys.Keys.ToArray<object>();
        if (this.Graph.IsContractBasedAPI && array.Length == 0)
          array = (object[]) this.Keys.ToArray();
        foreach (string str in array)
        {
          object valueExt = this.GetValueExt((object) node1, str);
          if (valueExt is PXFieldState pxFieldState)
            valueExt = pxFieldState.Value;
          keys[(object) str] = valueExt;
        }
      }
      if (bqlTablePair1 != null)
      {
        try
        {
          bqlTablePair1.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node1);
          if (flag6)
          {
            if (bqlTablePair1.Unchanged is TNode)
            {
              if ((object) node1 != bqlTablePair1.Unchanged)
              {
                if (!this.ObjectsEqual((object) node1, (object) bqlTablePair1.Unchanged))
                {
                  if (this._ChangedKeys == null)
                    this._ChangedKeys = new Dictionary<TNode, TNode>((IEqualityComparer<TNode>) new PXCache<TNode>.ItemComparer((PXCache) this));
                  this._ChangedKeys[(TNode) bqlTablePair1.Unchanged] = node1;
                  this.ClearQueryCache();
                }
              }
            }
          }
        }
        catch
        {
        }
      }
      return 1;
    }
  }

  private bool IsFieldEnabled(string keyField)
  {
    return !(this.GetStateExt((object) null, keyField) is PXFieldState stateExt) || stateExt.Enabled;
  }

  internal bool PlaceNotChangedWithOriginals(TNode item)
  {
    if ((object) this.Locate(item) != null)
      return false;
    this.PlaceNotChanged(item);
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair))
    {
      this._Originals[(IBqlTable) item] = new BqlTablePair()
      {
        Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(item, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
        LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(item, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal))
      };
    }
    else
    {
      bqlTablePair.Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(item, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
      bqlTablePair.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(item, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
    }
    return true;
  }

  private BqlTablePair _FetchOriginals(TNode item, TNode placed, bool ahead)
  {
    BqlTablePair bqlTablePair1 = (BqlTablePair) null;
    try
    {
      TNode node = default (TNode);
      int num1 = !this._Originals.TryGetValue((IBqlTable) placed, out bqlTablePair1) ? 0 : (bqlTablePair1.Unchanged != null ? 1 : 0);
      if (num1 == 0 && this.GetStatus(placed) != PXEntryStatus.Inserted)
        node = this.readItem(item, true);
      if (num1 == 0)
      {
        if (this._Originals.TryGetValue((IBqlTable) placed, out bqlTablePair1))
        {
          if (bqlTablePair1.Unchanged != null)
            goto label_30;
        }
        if (ahead)
          ++this._OriginalsRequested;
        if ((object) node != null)
        {
          if ((object) node == (object) placed)
            node = PXCache<TNode>.CreateCopy(node, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
          if (bqlTablePair1 == null)
          {
            PXCacheOriginalCollection originals = this._Originals;
            // ISSUE: variable of a boxed type
            __Boxed<TNode> key = (object) placed;
            BqlTablePair bqlTablePair2 = new BqlTablePair();
            bqlTablePair2.Unchanged = (IBqlTable) node;
            bqlTablePair2.VersionedModified = new VersionedModifiedPair()
            {
              CopyOfItem = this.MakeCopyOfItem(node),
              WasChanged = false,
              Status = new PXEntryStatus?(PXEntryStatus.Notchanged)
            };
            bqlTablePair1 = bqlTablePair2;
            originals[(IBqlTable) key] = bqlTablePair2;
          }
          else
            bqlTablePair1.Unchanged = (IBqlTable) node;
        }
        if (ahead)
        {
          if (this._OriginalsRequested >= 5)
          {
            this._OriginalsRequested = 0;
            ++this._OriginalsReadAhead;
            int num2 = 0;
            foreach (TNode key in this._Items.NotChanged)
            {
              ++num2;
              if (this._OriginalsReadAhead < 100)
              {
                if (num2 >= 50)
                  break;
              }
              BqlTablePair bqlTablePair3;
              if (!this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair3))
              {
                this._Originals[(IBqlTable) key] = new BqlTablePair()
                {
                  Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
                  LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
                  VersionedModified = new VersionedModifiedPair()
                  {
                    CopyOfItem = this.MakeCopyOfItem(key),
                    WasChanged = false,
                    Status = new PXEntryStatus?(PXEntryStatus.Notchanged)
                  }
                };
              }
              else
              {
                bqlTablePair3.Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
                bqlTablePair3.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
                bqlTablePair3.VersionedModified = new VersionedModifiedPair()
                {
                  CopyOfItem = this.MakeCopyOfItem(key),
                  Status = new PXEntryStatus?(PXEntryStatus.Notchanged),
                  WasChanged = false
                };
              }
            }
            if (this._OriginalsReadAhead >= 100)
              this._OriginalsReadAhead = 0;
          }
        }
      }
    }
    catch
    {
    }
label_30:
    return bqlTablePair1;
  }

  /// <summary>Updates the provided data record in the cache.</summary>
  /// <remarks>
  /// <para>If the data record does not exist in the cache, the method tries
  /// to retrieve it from the database. If the data record exists in the
  /// cache or database, it gets the <tt>Updated</tt> status. If the data
  /// record does not exist in the database, the method inserts a new data
  /// record into the cache with the <tt>Inserted</tt> status.</para>
  /// <para>The method raises the following events: <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, <tt>FieldUpdated</tt>, <tt>RowUpdating</tt>,
  /// and <tt>RowUpdated</tt>. See <a href="Update.html">Updating a Data Record</a> for
  /// the events flowchart. If the data record does not exist in the
  /// database, the method also causes the events of the <see cref="M:PX.Data.PXCache`1.Insert(System.Object)">Insert(object)</see>
  /// method.</para>
  /// <para>The <tt>AllowUpdate</tt> property does not affect the method
  /// unlike the <see cref="M:PX.Data.PXCache`1.Update(System.Collections.IDictionary,System.Collections.IDictionary)">Update(IDictionary,
  /// IDictionary)</see> method.</para>
  /// </remarks>
  /// <param name="data">The data record to update in the cache.</param>
  /// <example>
  /// The code below modifies an <tt>APRegister</tt> data record and places
  /// it in the cache with the <tt>Updated</tt> status or updates it in the
  /// cache if the data record is already there.
  /// <code>
  /// // Declaring a data view in a graph
  /// public PXSelect&lt;APRegister&gt; APDocument;
  /// ...
  /// 
  /// APRegister apdoc = ...
  /// // Modifying the data record
  /// apdoc.Voided = true;
  /// apdoc.OpenDoc = false;
  /// apdoc.CuryDocBal = 0m;
  /// apdoc.DocBal = 0m;
  /// 
  /// // Updating the data record in the cache
  /// APDocument.Cache.Update(apdoc);</code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object Update(object data) => this.Update(data, false);

  protected internal override object Update(object data, bool bypassinterceptor)
  {
    PXPerformanceInfoTimerScope performanceInfoTimerScope = (PXPerformanceInfoTimerScope) null;
    bool flag1 = this._PendingItems == null;
    try
    {
      performanceInfoTimerScope = new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmUpdate));
      if (flag1)
        this._PendingItems = new List<TNode>();
      if (!bypassinterceptor && this._Interceptor != null)
        return this._Interceptor.Update((PXCache) this, data);
      if (data is PXResult)
        data = ((PXResult) data)[0];
      if (!(data is TNode node1))
        return (object) null;
      TNode node2 = this._Items.PlaceUpdated(node1, false);
      BqlTablePair bqlTablePair1 = (BqlTablePair) null;
      if ((object) node2 == null)
      {
        TNode node3;
        if ((object) (node3 = this.readItem(node1, true)) == null)
          return this.Insert(data);
        if (this.Graph.ShouldSaveVersionModified && this.Graph.IsMobile)
        {
          this.Graph.VersionedState.CreateNewVersion();
          this.Graph.ShouldSaveVersionModified = false;
        }
        node2 = this._Items.PlaceUpdated(node1, true);
        try
        {
          if ((object) node2 != null)
          {
            List<object> objectList1 = (List<object>) null;
            List<object> objectList2 = (List<object>) null;
            VersionedModifiedPair versionedModifiedPair = (VersionedModifiedPair) null;
            if (this._Originals.TryGetValue((IBqlTable) node2, out bqlTablePair1))
            {
              objectList1 = bqlTablePair1.Slots;
              objectList2 = bqlTablePair1.SlotsOriginal;
              versionedModifiedPair = bqlTablePair1.VersionedModified;
            }
            PXCacheOriginalCollection originals = this._Originals;
            // ISSUE: variable of a boxed type
            __Boxed<TNode> key = (object) node2;
            BqlTablePair bqlTablePair2 = new BqlTablePair();
            bqlTablePair2.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node3, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
            bqlTablePair2.Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(node3, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
            bqlTablePair2.Slots = objectList1;
            bqlTablePair2.SlotsOriginal = objectList2;
            bqlTablePair2.VersionedModified = versionedModifiedPair;
            bqlTablePair1 = bqlTablePair2;
            originals[(IBqlTable) key] = bqlTablePair2;
          }
        }
        catch
        {
        }
      }
      else if (!this.NonDBTable)
      {
        try
        {
          if (this.Graph.ShouldSaveVersionModified && this.Graph.IsMobile)
          {
            this.Graph.VersionedState.CreateNewVersion();
            this.Graph.ShouldSaveVersionModified = false;
          }
          TNode node4 = default (TNode);
          if ((!this._Originals.TryGetValue((IBqlTable) node2, out bqlTablePair1) ? 0 : (bqlTablePair1.Unchanged != null ? 1 : 0)) == 0)
            node4 = this.readItem(node1, true);
          if (!this._Originals.TryGetValue((IBqlTable) node2, out bqlTablePair1) || bqlTablePair1.Unchanged == null)
          {
            ++this._OriginalsRequested;
            if ((object) node4 != null)
            {
              if ((object) node4 == (object) node2)
                node4 = PXCache<TNode>.CreateCopy(node4, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
              if (bqlTablePair1 == null)
              {
                PXCacheOriginalCollection originals = this._Originals;
                // ISSUE: variable of a boxed type
                __Boxed<TNode> key = (object) node2;
                BqlTablePair bqlTablePair3 = new BqlTablePair();
                bqlTablePair3.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node4, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
                bqlTablePair3.Unchanged = (IBqlTable) node4;
                bqlTablePair1 = bqlTablePair3;
                originals[(IBqlTable) key] = bqlTablePair3;
              }
              else
              {
                if (bqlTablePair1.LastModified == null)
                  bqlTablePair1.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node4, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
                bqlTablePair1.Unchanged = (IBqlTable) node4;
              }
            }
            if (this._OriginalsRequested >= 5)
            {
              this._OriginalsRequested = 0;
              ++this._OriginalsReadAhead;
              int num = 0;
              foreach (TNode key in this._Items.NotChanged)
              {
                ++num;
                if (this._OriginalsReadAhead < 100)
                {
                  if (num >= 50)
                    break;
                }
                BqlTablePair bqlTablePair4;
                if (!this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair4))
                {
                  this._Originals[(IBqlTable) key] = new BqlTablePair()
                  {
                    Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
                    LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal))
                  };
                }
                else
                {
                  bqlTablePair4.Unchanged = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
                  bqlTablePair4.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
                }
              }
              if (this._OriginalsReadAhead >= 100)
                this._OriginalsReadAhead = 0;
            }
          }
          if (bqlTablePair1 != null)
          {
            if (bqlTablePair1.LastModified == null)
              bqlTablePair1.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy((TNode) bqlTablePair1.Unchanged, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
          }
        }
        catch
        {
        }
      }
      if ((object) node2 == null)
        return (object) null;
      bool flag2 = (object) node2 == (object) node1;
      bool flag3 = false;
      if (flag2 && bqlTablePair1 != null && bqlTablePair1.LastModified is TNode && bqlTablePair1.LastModified != (object) node1)
      {
        node1 = PXCache<TNode>.CreateCopy(node1);
        TNode lastModified = (TNode) bqlTablePair1.LastModified;
        foreach (string key in (IEnumerable<string>) this.Keys)
        {
          if (flag3 = flag3 || !object.Equals(this.GetValue((object) node1, key), this.GetValue((object) lastModified, key)))
            break;
        }
        this.RestoreCopy((object) node2, (object) bqlTablePair1.LastModified);
        this.Current = (object) node2;
        if (flag3)
          this.Normalize();
        flag2 = false;
      }
      TNode copy = PXCache<TNode>.CreateCopy(node2);
      if (this.ForceExceptionHandling && !this.DisableCloneAttributes)
        PXCache.TryDispose((object) this.GetAttributes((object) node2, (string) null));
      if (!flag2)
      {
        this.SetSessionUnmodified((object) node2, (object) copy);
        this.SetVersionModified((object) node2);
        this.FillWithValues(node2, copy, node1);
        if (flag3)
          this.Normalize();
      }
      bool flag4 = false;
      try
      {
        flag4 = !this.OnRowUpdating((object) copy, (object) node2, false);
      }
      catch (Exception ex)
      {
        flag4 = true;
        throw;
      }
      finally
      {
        if (flag4)
          PXCache<TNode>.RestoreCopy(node2, copy);
      }
      if (flag4)
        return (object) null;
      this._IsDirty = true;
      this.Current = (object) node2;
      this.OnRowUpdated((object) node2, (object) (flag2 ? node2 : copy), false);
      return (object) node2;
    }
    finally
    {
      performanceInfoTimerScope?.Dispose();
      if (flag1)
      {
        for (int index = 0; index < this._PendingItems.Count; ++index)
        {
          if ((object) this._Items.Locate(this._PendingItems[index]) == null)
            this._ItemAttributes.Remove((object) this._PendingItems[index]);
        }
        this._PendingItems = (List<TNode>) null;
      }
    }
  }

  protected internal override void UpdateLastModified(object item, bool inserted)
  {
    BqlTablePair bqlTablePair = (BqlTablePair) null;
    if (!(item is TNode node))
      return;
    TNode key = this._Items.Locate(node);
    if ((object) key == null)
      return;
    if (inserted)
    {
      if (this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) && bqlTablePair != null && bqlTablePair.Slots != null)
      {
        List<object> objectList1 = new List<object>(bqlTablePair.Slots.Count);
        List<object> objectList2 = new List<object>(bqlTablePair.Slots.Count);
        for (int index = 0; index < bqlTablePair.Slots.Count; ++index)
        {
          if (bqlTablePair.Slots[index] != null && index < this._SlotDelegates.Count)
            objectList1.Add(((Func<object, object>) this._SlotDelegates[index].Item3)(bqlTablePair.Slots[index]));
          else
            objectList1.Add((object) null);
          if (bqlTablePair.SlotsOriginal[index] != null && index < this._SlotDelegates.Count)
            objectList2.Add(((Func<object, object>) this._SlotDelegates[index].Item3)(bqlTablePair.SlotsOriginal[index]));
          else
            objectList2.Add((object) null);
        }
        this._Originals[(IBqlTable) key] = new BqlTablePair()
        {
          LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
          Slots = objectList1,
          SlotsOriginal = objectList2,
          SessionUnmodified = bqlTablePair.SessionUnmodified,
          VersionedModified = bqlTablePair.VersionedModified,
          Unchanged = bqlTablePair.Unchanged
        };
      }
      else
        this._Originals[(IBqlTable) key] = new BqlTablePair()
        {
          LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(key, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal)),
          SessionUnmodified = bqlTablePair?.SessionUnmodified,
          VersionedModified = bqlTablePair?.VersionedModified,
          Unchanged = bqlTablePair?.Unchanged
        };
    }
    else
    {
      if (!this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null)
        return;
      bqlTablePair.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node);
    }
  }

  protected internal override void SetSessionUnmodified(object item, object unmodified)
  {
    this.SetSessionUnmodified(item, unmodified, new PXEntryStatus?(this.GetStatus(unmodified)));
  }

  protected internal void SetSessionUnmodified(
    object item,
    object unmodified,
    PXEntryStatus? status)
  {
    if (!(item is TNode node))
      return;
    TNode key = this._Items.Locate(node);
    if ((object) key == null)
      return;
    BqlTablePair originalPair;
    if (!this._Originals.TryGetValue((IBqlTable) key, out originalPair) || originalPair == null)
      this._Originals[(IBqlTable) key] = originalPair = new BqlTablePair();
    if (originalPair.SessionUnmodified != null)
      return;
    BqlTablePair bqlTablePair = originalPair;
    SessionUnmodifiedPair sessionUnmodifiedPair;
    if (unmodified == null)
    {
      sessionUnmodifiedPair = new SessionUnmodifiedPair()
      {
        Item = (IBqlTable) null,
        Status = new PXEntryStatus?()
      };
    }
    else
    {
      sessionUnmodifiedPair = new SessionUnmodifiedPair();
      sessionUnmodifiedPair.Item = (IBqlTable) this.CreateUnmodifiedCopy((TNode) unmodified, originalPair);
      sessionUnmodifiedPair.Status = status;
    }
    bqlTablePair.SessionUnmodified = sessionUnmodifiedPair;
  }

  private TNode CreateUnmodifiedCopy(TNode unmodified, BqlTablePair originalPair)
  {
    TNode copy = (TNode) this.CreateCopy((object) unmodified);
    BqlTablePair bqlTablePair = originalPair.Clone();
    this._Originals[(IBqlTable) copy] = bqlTablePair;
    return copy;
  }

  protected internal override void CreateNewVersion(object item)
  {
    if (!this.Graph.IsMobile || !(item is TNode node))
      return;
    TNode key = this._Items.Locate(node);
    if ((object) key == null)
      return;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null)
      this._Originals[(IBqlTable) key] = bqlTablePair = new BqlTablePair();
    PXEntryStatus status = this._Items.GetStatus(key);
    if (bqlTablePair.VersionedModified != null)
      return;
    bqlTablePair.VersionedModified = new VersionedModifiedPair()
    {
      CopyOfItem = this.Graph.IsInVersionModifiedState ? (Dictionary<string, object>) null : this.MakeCopyOfItem(key),
      Status = new PXEntryStatus?(status),
      WasChanged = false
    };
  }

  protected internal override void SaveCurrentVersion(object item)
  {
    if (!this.Graph.IsMobile || !(item is TNode node))
      return;
    TNode key = this._Items.Locate(node);
    BqlTablePair bqlTablePair;
    if ((object) key == null || !this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null)
      return;
    bqlTablePair.VersionedModified = (VersionedModifiedPair) null;
    this._IsInInsertState = false;
  }

  protected internal override void SetVersionModified(object item)
  {
    if (!this.Graph.IsMobile || !(item is TNode node))
      return;
    TNode key = this._Items.Locate(node);
    BqlTablePair bqlTablePair;
    if ((object) key == null || !this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null || bqlTablePair.VersionedModified == null)
      return;
    bqlTablePair.VersionedModified.WasChanged = true;
  }

  protected internal override void DiscardCurrentVersion(object item)
  {
    if (!this.Graph.IsMobile || !(item is TNode node1))
      return;
    TNode key = this._Items.Locate(node1);
    if ((object) key == null)
      return;
    this._Items.Locate(this._Current as TNode);
    bool isDirty = this._IsDirty;
    BqlTablePair bqlTablePair;
    if (!this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null)
      return;
    if (bqlTablePair.VersionedModified != null)
    {
      if (bqlTablePair.VersionedModified.CopyOfItem == null)
        this._Items.Remove(key);
      else if (bqlTablePair.VersionedModified.WasChanged)
      {
        this.Items.Remove(bqlTablePair.LastModified as TNode);
        PXEntryStatus? status = bqlTablePair.VersionedModified.Status;
        TNode node2 = this.UpdateItemFromCopy(bqlTablePair.LastModified as TNode, bqlTablePair.VersionedModified.CopyOfItem);
        this.InsertToItems(status.HasValue ? status.Value : PXEntryStatus.Notchanged, node2);
      }
    }
    bqlTablePair.VersionedModified = (VersionedModifiedPair) null;
    this._IsDirty = isDirty && this.Dirty.Count() > 0L;
    this._IsInInsertState = false;
  }

  protected internal override bool IfModifiedVersion(object item)
  {
    if (!this.Graph.IsMobile)
      return false;
    if (this._IsInInsertState)
      return true;
    if (!(item is TNode node))
      return false;
    TNode key = this._Items.Locate(node);
    BqlTablePair bqlTablePair;
    if ((object) key == null || !this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair) || bqlTablePair == null || bqlTablePair.VersionedModified == null)
      return false;
    if (this._IsInInsertState || bqlTablePair.VersionedModified.WasChanged)
      return true;
    return bqlTablePair.VersionedModified.Status.HasValue && bqlTablePair.VersionedModified.WasChanged;
  }

  internal override void SaveInsertState() => this._IsInInsertState = true;

  private Dictionary<string, object> MakeCopyOfItem(TNode item)
  {
    Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    PXCacheExtension[] cacheExtensions = cacheStaticInfo._CreateExtensions != null ? PXCache<TNode>.GetCacheExtensions(item, cacheStaticInfo._CreateExtensions) : (PXCacheExtension[]) null;
    for (int index = 0; index < cacheStaticInfo._ClassFields.Count; ++index)
    {
      object obj = PXResult.UnwrapFirst(cacheStaticInfo._GetValueByOrdinal(item, index, cacheExtensions));
      if (obj is Dictionary<string, object> dictionary3)
      {
        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
        foreach (string key in dictionary3.Keys)
          dictionary2[key] = dictionary3[key];
        dictionary1[cacheStaticInfo._ClassFields[index]] = (object) dictionary2;
      }
      else
        dictionary1[cacheStaticInfo._ClassFields[index]] = obj;
    }
    return dictionary1;
  }

  private TNode UpdateItemFromCopy(TNode item, Dictionary<string, object> copy)
  {
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    PXCacheExtension[] cacheExtensions = cacheStaticInfo._CreateExtensions != null ? PXCache<TNode>.GetCacheExtensions(item, cacheStaticInfo._CreateExtensions) : (PXCacheExtension[]) null;
    for (int index = 0; index < cacheStaticInfo._ClassFields.Count; ++index)
    {
      object obj = copy[cacheStaticInfo._ClassFields[index]];
      if (obj is Dictionary<string, object> dictionary1)
      {
        Dictionary<string, object> dictionary = cacheStaticInfo._GetValueByOrdinal(item, index, cacheExtensions) as Dictionary<string, object>;
        dictionary.Clear();
        foreach (string key in dictionary1.Keys)
          dictionary[key] = dictionary1[key];
      }
      else
        cacheStaticInfo._SetValueByOrdinal(item, index, obj, cacheExtensions);
    }
    return item;
  }

  /// <summary>Initializes a new data record using the provided field values and inserts the data record into the cache. Returns 1 in case of successful insertion, 0
  /// otherwise.</summary>
  /// <param name="values">The dictionary with values to initialize the data
  /// record fields. The dictionary keys are field names.</param>
  /// <remarks>
  /// <para>The method raises the following events:
  /// <tt>FieldDefaulting</tt>, <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, <tt>FieldUpdated</tt>, <tt>RowInserting</tt>,
  /// and <tt>RowInserted</tt>. See <a href="Insert.html">Inserting a Data Record</a> for
  /// the events chart.</para>
  /// <para>The method does not check if the data record exists in the
  /// database. The values provided in the dictionary are not readonly and
  /// can be updated during execution of the method. The method is typically
  /// used by the system when the values are received from the user
  /// interface. If the <tt>AllowInsert</tt> property is <tt>false</tt>, the
  /// data record is not inserted and the method returns 0.</para>
  /// <para>In case of successful insertion, the method marks the data
  /// record as <tt>Inserted</tt>, and it becomes accessible through the
  /// <tt>Inserted</tt> collection.</para>
  /// </remarks>
  public override int Insert(IDictionary values)
  {
    if (!this._AllowInsert)
      return 0;
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmInsert)))
    {
      TNode node = new TNode();
      if (!this.DisableCloneAttributes)
        PXCache.TryDispose((object) this.GetAttributes((object) node, (string) null, true));
      this.FillWithValues(node, default (TNode), values, PXCacheOperation.Insert);
      TNode unmodified = this._ChangedKeys == null || !this._ChangedKeys.ContainsKey(node) ? this._Items.Locate(node) : throw new PXBadDictinaryException();
      PXEntryStatus status = this._Items.GetStatus(node);
      string[] strArray = new string[values.Keys.Count];
      values.Keys.CopyTo((Array) strArray, 0);
      if (!this.OnRowInserting((object) node, true))
      {
        foreach (string str in strArray)
        {
          if (this.Fields.Contains(str))
            values[(object) str] = this.GetValueExt((object) node, str);
        }
        return 0;
      }
      if (this.Graph.ShouldSaveVersionModified && this.Graph.IsMobile)
      {
        this.Graph.VersionedState.CreateNewVersion();
        this.Graph.ShouldSaveVersionModified = false;
      }
      bool wasDeleted;
      TNode data = this._Items.PlaceInserted(node, out wasDeleted);
      if (this.Graph.IsInVersionModifiedState)
        this.CreateNewVersion((object) data);
      if (wasDeleted)
        this.ClearQueryCache();
      if ((object) data == null)
        return 0;
      this._IsDirty = true;
      this.Current = (object) data;
      this.SetSessionUnmodified((object) data, (object) unmodified, new PXEntryStatus?(this.GetSessionUnmodifiedStatusOnInsert(status)));
      this.OnRowInserted((object) data, true);
      foreach (string str in strArray)
      {
        if (this.Fields.Contains(str))
          values[(object) str] = this.GetValueExt((object) data, str);
      }
      foreach (string key in (IEnumerable<string>) this._Keys)
        values[(object) key] = this.GetValueExt((object) data, key);
      return 1;
    }
  }

  internal override object FillItem(IDictionary values)
  {
    TNode data = new TNode();
    if (!this.DisableCloneAttributes)
      PXCache.TryDispose((object) this.GetAttributes((object) data, (string) null, true));
    this.FillWithValues(data, default (TNode), values, PXCacheOperation.Insert, false);
    return (object) data;
  }

  /// <summary>Inserts the provided data record into the cache. Returns the
  /// inserted data record or <tt>null</tt> if the data record wasn't
  /// inserted.</summary>
  /// <param name="data">The data record to insert into the cache.</param>
  /// <remarks>
  /// <para>The method raises the following events:
  /// <tt>FieldDefaulting</tt>, <tt>FieldUpdating</tt>,
  /// <tt>FieldVerifying</tt>, <tt>FieldUpdated</tt>, <tt>RowInserting</tt>,
  /// and <tt>RowInserted</tt>. See <a href="Insert.html">Inserting a Data Record</a> for
  /// the events chart.</para>
  /// <para>The method does not check if the data record exists in the
  /// database. The AllowInsert property does not affect this method unlike
  /// the <see cref="M:PX.Data.PXCache`1.Insert(System.Collections.IDictionary)">Insert(IDictionary)</see>
  /// method.</para>
  /// <para>In case of successful insertion, the method marks the data
  /// record as <tt>Inserted</tt>, and it becomes accessible through the
  /// <tt>Inserted</tt> collection.</para>
  /// </remarks>
  /// <example><para>The code below initializes a new instance of the APInvoice data record and inserts it into the cache.</para>
  /// <code title="Example" lang="CS">
  /// APInvoice newDoc = new APInvoice();
  /// newDoc.VendorID = Document.Current.VendorID;
  /// Document.Insert(newDoc);</code>
  /// </example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object Insert(object data) => this.Insert(data, false);

  protected internal override object Insert(object data, bool bypassinterceptor)
  {
    PXPerformanceInfoTimerScope performanceInfoTimerScope = (PXPerformanceInfoTimerScope) null;
    bool flag = this._PendingItems == null;
    try
    {
      performanceInfoTimerScope = new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmInsert));
      if (flag)
        this._PendingItems = new List<TNode>();
      if (this._PendingExceptions == null)
        this._PendingExceptions = new Dictionary<TNode, List<Exception>>();
      if (!bypassinterceptor && this._Interceptor != null)
        return this._Interceptor.Insert((PXCache) this, data);
      if (data is PXResult)
        data = ((PXResult) data)[0];
      if (!(data is TNode key))
        return (object) null;
      TNode node1 = new TNode();
      if (this.ForceExceptionHandling && !this.DisableCloneAttributes)
        PXCache.TryDispose((object) this.GetAttributes((object) node1, (string) null));
      this.FillWithValues(node1, ref key);
      if (!this.OnRowInserting((object) key, false))
        return (object) null;
      List<Exception> exceptionList;
      if (this._PendingExceptions.TryGetValue(key, out exceptionList) && exceptionList.Count > 0)
        throw exceptionList[0];
      TNode unmodified = this._Items.Locate(key);
      PXEntryStatus status = this._Items.GetStatus(key);
      if (this.Graph.ShouldSaveVersionModified && this.Graph.IsMobile)
      {
        this.Graph.VersionedState.CreateNewVersion();
        this.Graph.ShouldSaveVersionModified = false;
      }
      bool wasDeleted;
      TNode node2 = this._Items.PlaceInserted(key, out wasDeleted);
      if (wasDeleted)
        this.ClearQueryCache();
      if ((object) node2 == null)
        return (object) null;
      this._IsDirty = true;
      if (this.Graph.IsInVersionModifiedState)
        this.CreateNewVersion((object) node2);
      this.SetSessionUnmodified((object) node2, (object) unmodified, new PXEntryStatus?(this.GetSessionUnmodifiedStatusOnInsert(status)));
      this.Current = (object) node2;
      this.OnRowInserted((object) node2, (object) (data as TNode), false);
      return (object) node2;
    }
    finally
    {
      performanceInfoTimerScope?.Dispose();
      if (flag)
      {
        if (this._ItemAttributes != null)
        {
          for (int index = 0; index < this._PendingItems.Count; ++index)
          {
            if ((object) this._Items.Locate(this._PendingItems[index]) == null)
              this._ItemAttributes.Remove((object) this._PendingItems[index]);
          }
        }
        this._PendingItems = (List<TNode>) null;
      }
    }
  }

  private PXEntryStatus GetSessionUnmodifiedStatusOnInsert(PXEntryStatus incomingStatus)
  {
    return incomingStatus != PXEntryStatus.InsertedDeleted ? incomingStatus : PXEntryStatus.Inserted;
  }

  private static PXCache<TNode>.CacheStaticInfo _GetInitializer()
  {
    return PXCache<TNode>._Initialize(false);
  }

  internal override object ToChildEntity<Parent>(Parent item)
  {
    if (!typeof (TNode).IsSubclassOf(typeof (Parent)))
      throw new PXArgumentException(nameof (Parent), "The argument is out of range.");
    TNode data = new TNode();
    PXCacheExtension[] extensions = (PXCacheExtension[]) null;
    if (PXCache<Parent>._GetInitializer()._CreateExtensions != null)
    {
      PXCacheExtensionCollection extensionCollection = this._Extensions ?? PXContext.GetSlot<PXCacheExtensionCollection>() ?? PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
      lock (((ICollection) extensionCollection).SyncRoot)
      {
        if (!extensionCollection.TryGetValue((IBqlTable) item, out extensions))
          extensionCollection[(IBqlTable) item] = extensions = PXCache<Parent>._GetInitializer()._CreateExtensions(item);
      }
    }
    foreach (string classField in PXCache<Parent>._GetInitializer()._ClassFields)
      this.SetValue((object) data, classField, PXCache<Parent>._GetInitializer()._GetValueByOrdinal(item, PXCache<Parent>._GetInitializer()._FieldsMap[classField], extensions));
    return (object) data;
  }

  /// <summary>Initializes a data record of the DAC type of the cache from
  /// the provided data record of the base DAC type and inserts the new data
  /// record into the cache. Returns the inserted data record.</summary>
  /// <param name="item">The data record of the base DAC type, the field values of which are used to initialize the data record.</param>
  /// <example>
  /// See the <see cref="M:PX.Data.PXSelectBase`1.Extend``1(``0)">Extend&lt;Parent&gt;(Parent)</see>
  /// method of the <tt>PXSelectBase&lt;&gt;</tt> class.
  /// <code title="" description="" lang="neutral"></code></example>
  public override object Extend<Parent>(Parent item)
  {
    TNode childEntity = (TNode) this.ToChildEntity<Parent>(item);
    TNode node1 = this.Locate(childEntity);
    if ((object) node1 != null && this.GetStatus(node1) != PXEntryStatus.Deleted && this.GetStatus(node1) != PXEntryStatus.InsertedDeleted)
    {
      if (this._Extensions == null)
      {
        foreach (string classField in this._ClassFields)
        {
          if (!PXCache<Parent>._GetInitializer()._ClassFields.Contains(classField))
          {
            int fields = this._FieldsMap[classField];
            this.SetValueByOrdinal(childEntity, fields, this.GetValueByOrdinal(node1, fields, (PXCacheExtension[]) null), (PXCacheExtension[]) null);
          }
        }
      }
      else
      {
        PXCacheExtension[] extensions1;
        PXCacheExtension[] extensions2;
        lock (((ICollection) this._Extensions).SyncRoot)
        {
          if (!this._Extensions.TryGetValue((IBqlTable) childEntity, out extensions1))
            this._Extensions[(IBqlTable) childEntity] = extensions1 = this._CreateExtensions(childEntity);
          if (!this._Extensions.TryGetValue((IBqlTable) node1, out extensions2))
            this._Extensions[(IBqlTable) node1] = extensions2 = this._CreateExtensions(node1);
        }
        foreach (string classField in this._ClassFields)
        {
          if (!PXCache<Parent>._GetInitializer()._ClassFields.Contains(classField))
          {
            int fields = this._FieldsMap[classField];
            this.SetValueByOrdinal(childEntity, fields, this.GetValueByOrdinal(node1, fields, extensions2), extensions1);
          }
        }
      }
      return (object) this.Update(childEntity);
    }
    TNode node2 = this.Insert(childEntity);
    if ((object) node2 != null)
    {
      TNode node3 = ((Parent) this.Graph.Caches[typeof (Parent)].GetOriginal((object) item)).With<Parent, TNode>((Func<Parent, TNode>) (c => (TNode) this.ToChildEntity<Parent>(c)));
      if (!this.ObjectsEqual(node2, childEntity))
      {
        TNode data = default (TNode);
        BqlTablePair bqlTablePair;
        if (this._Originals.TryGetValue((IBqlTable) node2, out bqlTablePair))
        {
          data = (TNode) bqlTablePair.LastModified;
          bqlTablePair.Unchanged = (IBqlTable) node3;
        }
        foreach (string key in (IEnumerable<string>) this.Keys)
        {
          object obj;
          this.SetValue((object) node2, key, obj = this.GetValue((object) childEntity, key));
          if ((object) data != null)
            this.SetValue((object) data, key, obj);
        }
        this.Normalize();
      }
      this.SetStatus((object) node2, PXEntryStatus.Updated);
    }
    return (object) node2;
  }

  /// <summary>Initializes a new data record with default values and inserts
  /// it into the cache by invoking the <see cref="M:PX.Data.PXCache`1.Insert(System.Object)">Insert(object)</see>
  /// method. Returns the new data record inserted into the cache.</summary>
  public override object Insert() => (object) this.Insert(new TNode());

  /// <summary>
  /// Returns a new <tt>DAC type</tt> data record. This method must be used
  /// to initialize a data record that is of an appropriate type for the <tt>PXCache</tt>
  /// instance when its DAC type is unknown.
  /// </summary>
  public override object CreateInstance()
  {
    TNode instance = new TNode();
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = PXCache<TNode>._Initialize(false);
    if (cacheStaticInfo._CreateExtensions != null)
      PXCache<TNode>.GetCacheExtensions(instance, cacheStaticInfo._CreateExtensions);
    return (object) instance;
  }

  /// <summary>Clears the internal cache of database query
  /// results.</summary>
  public override void ClearQueryCacheObsolete()
  {
    foreach (PXViewQueryCollection viewQueryCollection in this._Graph.QueryCache.Values.Concat<PXViewQueryCollection>((IEnumerable<PXViewQueryCollection>) this._Graph.TypedViews._NonstandardViews))
    {
      if (viewQueryCollection.CacheType == typeof (TNode) || viewQueryCollection.CacheType.IsAssignableFrom(typeof (TNode)) && !System.Attribute.IsDefined((MemberInfo) typeof (TNode), typeof (PXBreakInheritanceAttribute), false))
        viewQueryCollection.Clear();
    }
  }

  /// <summary>Clears the internal cache of database query
  /// results.</summary>
  public override void ClearQueryCache()
  {
    foreach (PXViewQueryCollection viewQueryCollection in this._Graph.QueryCache.Values.Concat<PXViewQueryCollection>((IEnumerable<PXViewQueryCollection>) this._Graph.TypedViews._NonstandardViews))
    {
      if (!(viewQueryCollection.CacheType == typeof (TNode)) && (!viewQueryCollection.CacheType.IsAssignableFrom(typeof (TNode)) || System.Attribute.IsDefined((MemberInfo) typeof (TNode), typeof (PXBreakInheritanceAttribute), false)))
      {
        if (!this._Graph.IsImport)
        {
          System.Type[] cacheTypes = viewQueryCollection.CacheTypes;
          if ((cacheTypes != null ? (((IEnumerable<System.Type>) cacheTypes).Any<System.Type>((Func<System.Type, bool>) (_ =>
          {
            if (_ == typeof (TNode))
              return true;
            return _.IsAssignableFrom(typeof (TNode)) && !System.Attribute.IsDefined((MemberInfo) typeof (TNode), typeof (PXBreakInheritanceAttribute), false);
          })) ? 1 : 0) : 0) == 0)
            continue;
        }
        else
          continue;
      }
      viewQueryCollection.Clear();
    }
  }

  /// <summary>Initializes the data record with the provided key values and places it into the cache with the Deleted or InsertedDeleted status. The method assigns the
  /// InsertedDeleted status to the data record if it has the Inserted status when the method is invoked.</summary>
  /// <param name="keys">The values of key fields.</param>
  /// <param name="values">The values of all fields. The parameter is not
  /// used in the method.</param>
  /// <remarks>
  /// <para>The method raises the following events: <tt>FieldUpdating</tt>,
  /// <tt>FieldUpdated</tt>, <tt>RowDeleting</tt>, and <tt>RowDeleted</tt>
  /// events.</para>
  /// <para>This method is typically used to process deletion initiated from
  /// the user interface. If the <tt>AllowDelete</tt> property is
  /// <tt>false</tt>, the data record is not marked deleted and the method
  /// returns 0. The method returns 1 if the data record is successfully
  /// marked deleted.</para>
  /// </remarks>
  public override int Delete(IDictionary keys, IDictionary values)
  {
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmDelete)))
    {
      TNode node1 = new TNode();
      this.FillWithValues(node1, default (TNode), keys, PXCacheOperation.Delete);
      TNode unmodified = this._Items.Locate(node1);
      PXEntryStatus status = this._Items.GetStatus(node1);
      TNode node2 = this._Items.PlaceDeleted(node1, false);
      if ((object) node2 == null && (object) this.readItem(node1) != null)
        node2 = this._Items.PlaceDeleted(node1, true);
      if ((object) node2 == null)
        return 0;
      this.ClearQueryCache();
      bool flag = false;
      try
      {
        flag = !this.OnRowDeleting((object) node2, true);
      }
      catch (Exception ex)
      {
        flag = true;
        throw;
      }
      finally
      {
        if (flag)
          this._Items.PlaceInserted(node2, out bool _);
      }
      if (flag)
        return 0;
      this.Current = (object) node2;
      if (!this._AllowDelete && this._Items.GetStatus(node2) != PXEntryStatus.InsertedDeleted)
      {
        this._Items.PlaceInserted(node2, out bool _);
        return 0;
      }
      this.SetSessionUnmodified((object) node2, (object) unmodified, new PXEntryStatus?(status));
      this._IsDirty = true;
      this.OnRowDeleted((object) node2, true);
      this._Current = (object) null;
      return 1;
    }
  }

  /// <summary>Places the data record into the cache with the Deleted or InsertedDeleted status. The method assigns the InsertedDeleted status to the data record if it has
  /// the Inserted status when the method is invoked.</summary>
  /// <param name="data">The data record to delete.</param>
  /// <remarks>
  ///   <para>The method raises the RowDeleting and RowDeleted events.</para>
  ///   <para>The AllowDelete property does not affect this method.</para>
  /// </remarks>
  /// <example>
  /// The code below deletes the current data records through the
  /// <tt>Address</tt> and <tt>Contact</tt> data views on deletion of an
  /// <tt>INSite</tt> data record.
  /// <code title="" description="" lang="CS">
  /// public PXSelect&lt;Address, Where&lt;Address.bAccountID, Equal&lt;Current&lt;Branch.bAccountID&gt;&gt;,
  ///     And&lt;Address.addressID, Equal&lt;Current&lt;INSite.addressID&gt;&gt;&gt;&gt;&gt; Address;
  /// public PXSelect&lt;Contact, Where&lt;Contact.bAccountID, Equal&lt;Current&lt;Branch.bAccountID&gt;&gt;,
  ///     And&lt;Contact.contactID, Equal&lt;Current&lt;INSite.contactID&gt;&gt;&gt;&gt;&gt; Contact;
  /// ...
  /// protected virtual void INSite_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  /// {
  ///     INSite row = (INSite)e.Row;
  ///     Address.Cache.Delete(Address.Current);
  ///     Contact.Cache.Delete(Contact.Current);
  /// }</code></example>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override object Delete(object data) => this.Delete(data, false);

  protected internal override object Delete(object data, bool bypassinterceptor)
  {
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmDelete)))
    {
      if (!bypassinterceptor && this._Interceptor != null)
        return this._Interceptor.Delete((PXCache) this, data);
      if (data is PXResult)
        data = ((PXResult) data)[0];
      if (!(data is TNode node1))
        return (object) null;
      TNode unmodified = this._Items.Locate(node1);
      PXEntryStatus status = this._Items.GetStatus(node1);
      TNode node2 = this._Items.PlaceDeleted(node1, false);
      if ((object) node2 == null && (object) this.readItem(node1) != null)
        node2 = this._Items.PlaceDeleted(node1, true);
      if ((object) node2 == null)
        return (object) null;
      this.ClearQueryCache();
      bool flag = false;
      try
      {
        flag = !this.OnRowDeleting((object) node2, false);
      }
      catch (Exception ex)
      {
        flag = true;
        throw;
      }
      finally
      {
        if (flag)
          this._Items.PlaceInserted(node2, out bool _);
      }
      if (flag)
        return (object) null;
      this.SetSessionUnmodified((object) node2, (object) unmodified, new PXEntryStatus?(status));
      this._IsDirty = true;
      this.OnRowDeleted((object) node2, false);
      this._Current = (object) null;
      return (object) node2;
    }
  }

  protected internal override void PlaceNotChanged(object data)
  {
    this.PlaceNotChanged((TNode) data);
  }

  protected internal override object PlaceNotChanged(object data, out bool wasUpdated)
  {
    return (object) this._Items.PlaceNotChanged((TNode) data, out wasUpdated);
  }

  protected void PlaceNotChanged(TNode data) => this._Items.PlaceNotChanged(data, out bool _);

  protected void PlaceInserted(TNode data) => this._Items.PlaceInserted(data, out bool _);

  protected void PlaceUpdated(TNode data) => this._Items.PlaceUpdated(data, false);

  protected void PlaceDeleted(TNode data) => this._Items.PlaceDeleted(data, false);

  /// <summary>Gets the collection of updated, inserted, and deleted data
  /// records. The collection contains data records with the
  /// <tt>Updated</tt>, <tt>Inserted</tt>, or <tt>Deleted</tt>
  /// status.</summary>
  public override IEnumerable Dirty => (IEnumerable) this._Items.Dirty;

  /// <summary>Gets the collection of updated data records that exist in the
  /// database. The collection contains data records with the
  /// <tt>Updated</tt> status.</summary>
  /// <example>The following example shows how to iterate over all updated data records.
  /// <code title="Example" lang="CS">
  /// // The defition of a data view
  /// public PXProcessing&lt;POReceipt&gt; Orders;
  /// ...
  /// // The optional delegate for the Orders data view
  /// public virtual IEnumerable orders()
  /// {
  ///     // Iterating over all updated POReceipt data records
  ///     foreach (POReceipt order in Orders.Cache.Updated)
  ///     {
  ///         yield return order;
  ///     }
  ///     ...
  /// }</code>
  /// </example>
  public override IEnumerable Updated => (IEnumerable) this._Items.Updated;

  /// <summary>Gets the collection of inserted data records that does not
  /// exist in the database. The collection contains data records with the
  /// <tt>Inserted</tt> status.</summary>
  /// <example>
  /// The code below modifies inserted <tt>Address</tt> data records on
  /// update of an <tt>EPEmployee</tt> data record.
  /// <code>
  /// protected virtual void EPEmployee_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  /// {
  ///     // Checking whether the ParentBAccountID has changed
  ///     if (!sender.ObjectsEqual&lt;EPEmployee.parentBAccountID&gt;(e.Row, e.OldRow))
  ///     {
  ///         // Iterating over all inserted and not saved Address data records
  ///         foreach (Address addr in Address.Cache.Inserted)
  ///         {
  ///             addr.BAccountID = ((EPEmployee)e.Row).ParentBAccountID;
  ///             addr.CountryID = company.Current.CountryID;
  ///         }
  ///         ...
  ///     }
  /// }
  /// </code>
  /// </example>
  public override IEnumerable Inserted => (IEnumerable) this._Items.Inserted;

  /// <summary>Get the collection of all cached data records. The collection
  /// contains data records with any status. The developer should not rely
  /// on the presense of data records with statuses other than
  /// <tt>Updated</tt>, <tt>Inserted</tt>, and <tt>Deleted</tt> in this
  /// collection.</summary>
  public override IEnumerable Cached => (IEnumerable) this._Items.Cached;

  /// <summary>Gets the collection of deleted data records that exist in the
  /// database. The collection contains data records with the
  /// <tt>Deleted</tt> status.</summary>
  /// <example>The code below deletes EPActivity data records through a different graph.
  /// <code title="Example" lang="CS">
  /// public override void Persist()
  /// {
  ///     CREmailActivityMaint graph = CreateInstance&lt;CREmailActivityMaint&gt;();
  ///     // Iterating over all deleted EPActivity data records
  ///     foreach (EPActivity item in Emails.Cache.Deleted)
  ///     {
  ///         // Setting the current data record for the CREmailActivityMaint graph
  ///         graph.Message.Current = graph.Message.Search&lt;EPActivity.taskID&gt;(item.TaskID);
  ///         // Invoking the Delete action in the CREmailActivityMaint graph
  ///         graph.Delete.Press();
  ///         Emails.Cache.SetStatus(item, PXEntryStatus.Notchanged);
  ///     }
  ///     base.Persist();
  /// }</code>
  /// </example>
  public override IEnumerable Deleted => (IEnumerable) this._Items.Deleted;

  internal override int Version
  {
    get => this._Items.Version;
    set => this._Items.Version = value;
  }

  internal override BqlTablePair GetOriginalObjectContext(object data, bool readItemIfNotExists = false)
  {
    if (!(data is TNode key1) || this.NonDBTable)
      return (BqlTablePair) null;
    BqlTablePair originalObjectContext = (BqlTablePair) null;
    TNode node = default (TNode);
    bool flag1 = this._Originals.TryGetValue((IBqlTable) key1, out originalObjectContext) && originalObjectContext.Unchanged != null;
    bool flag2 = false;
    if (this._Extensions != null & flag1)
    {
      lock (((ICollection) this._Extensions).SyncRoot)
      {
        PXCacheExtension[] pxCacheExtensionArray1;
        PXCacheExtension[] pxCacheExtensionArray2;
        flag2 = this._Extensions != null && (!this._Extensions.TryGetValue(originalObjectContext.Unchanged, out pxCacheExtensionArray1) || this._Extensions.TryGetValue(originalObjectContext.Unchanged, out pxCacheExtensionArray2) && pxCacheExtensionArray2.Length > pxCacheExtensionArray1.Length);
      }
    }
    if (!flag1 | flag2)
      node = readItemIfNotExists ? this.readItem(key1, true) : key1;
    if ((object) node != null && ((!this._Originals.TryGetValue((IBqlTable) key1, out originalObjectContext) ? 1 : (originalObjectContext.Unchanged == null ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
    {
      if ((object) node == (object) key1)
        node = PXCache<TNode>.CreateCopy(node, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
      if (originalObjectContext == null)
      {
        PXCacheOriginalCollection originals = this._Originals;
        // ISSUE: variable of a boxed type
        __Boxed<TNode> key2 = (object) key1;
        BqlTablePair bqlTablePair = new BqlTablePair();
        bqlTablePair.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, new PXCache<TNode>.SetValueByOrdinalDelegate(this.SetValueByOrdinal));
        bqlTablePair.Unchanged = (IBqlTable) node;
        originalObjectContext = bqlTablePair;
        originals[(IBqlTable) key2] = bqlTablePair;
      }
      else
      {
        if (originalObjectContext.LastModified == null)
          originalObjectContext.LastModified = (IBqlTable) PXCache<TNode>.CreateCopy(node, this._CreateExtensions, this._CloneExtensions, this._ClassFields, this._GetValueByOrdinal, this._SetValueByOrdinal);
        originalObjectContext.Unchanged = (IBqlTable) node;
      }
    }
    return originalObjectContext;
  }

  /// <summary>Gets the value that indicates if the cache contains modified
  /// data records to be saved to database.</summary>
  public override bool IsInsertedUpdatedDeleted => this._Items.IsDirty;

  private object[] getKeys(TNode node)
  {
    object[] keys = new object[this.Keys.Count];
    for (int index = 0; index < keys.Length; ++index)
      keys[index] = this.GetValue((object) node, this.Keys[index]);
    return keys;
  }

  /// <summary>Saves the modifications of a particular type from the cache
  /// to the database. Returns the number of saved data records.</summary>
  /// <remarks>
  /// <para>Using this method, you can update, delete, or insert all data
  /// records kept by the cache. You can also perform different operations
  /// at once by passing a combination of <tt>PXDBOperation</tt> values,
  /// such as <tt>PXDBOperation.Insert | PXDBOperation.Update</tt>.</para>
  /// <para>The method raises the following events: <tt>RowPersisting</tt>,
  /// <tt>CommandPreparing</tt>, <tt>RowPersisted</tt>,
  /// <tt>ExceptionHandling</tt>.</para>
  /// </remarks>
  /// <param name="operation">The value that indicates the types of database
  /// operations to execute, either one of <tt>PXDBOperation.Insert</tt>,
  /// <tt>PXDBOperation.Update</tt>, and <tt>PXDBOperation.Delete</tt>
  /// values or their bitwise "or" (<tt>|</tt>) combination.</param>
  /// <example>
  /// The code below modifies a <tt>Vendor</tt> data record, updates it in
  /// the cache, saves changes to update <tt>Vendor</tt> data records to the
  /// database, and causes raising of the <tt>RowPersisted</tt> event with
  /// indication that the operation has completed successfully.
  /// <code>
  /// vendor.VStatus = VendorStatus.Inactive;
  /// Caches[typeof(Vendor)].Update(vendor);
  /// Caches[typeof(Vendor)].Persist(PXDBOperation.Update);
  /// Caches[typeof(Vendor)].Persisted(false);</code>
  /// </example>
  public override int Persist(PXDBOperation operation)
  {
    int num1 = 0;
    if (this.persistedItems == null)
      this.persistedItems = new Dictionary<TNode, bool>();
    this._Persisting = true;
    IEnumerable<TNode> collection = (IEnumerable<TNode>) null;
    switch (operation)
    {
      case PXDBOperation.Update:
        collection = this._Items.Updated;
        break;
      case PXDBOperation.Insert:
        collection = this._Items.Inserted;
        break;
      case PXDBOperation.Delete:
        collection = this._Items.Deleted;
        break;
    }
    if (this._PreventDeadlock && this.Keys.Count > 0)
    {
      collection = (IEnumerable<TNode>) new List<TNode>(collection);
      int[] keys = this.Keys.Select<string, int>((Func<string, int>) (_ => this._FieldsMap[_])).ToArray<int>();
      Comparison<TNode> comparison = (Comparison<TNode>) ((a, b) =>
      {
        for (int index = 0; index < keys.Length; ++index)
        {
          object obj1 = this.GetValue((object) a, keys[index]);
          object obj2 = this.GetValue((object) b, keys[index]);
          if (obj1 is IComparable && obj2 is IComparable)
          {
            int num2 = ((IComparable) obj1).CompareTo(obj2);
            if (num2 != 0)
              return num2;
          }
          else if (obj1 == null)
          {
            if (obj2 != null)
              return -1;
          }
          else if (obj2 == null)
            return 1;
        }
        return 0;
      });
      if (this._CustomDeadlockComparison == null)
        ((List<TNode>) collection).Sort(comparison);
      else
        ((List<TNode>) collection).Sort((Comparison<TNode>) this._CustomDeadlockComparison);
    }
    switch (operation)
    {
      case PXDBOperation.Update:
        using (IEnumerator<TNode> enumerator = collection.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            if (this.PersistUpdated((object) enumerator.Current))
              ++num1;
          }
          break;
        }
      case PXDBOperation.Insert:
        foreach (TNode row in collection)
        {
          if (this.PersistInserted((object) row))
            ++num1;
          this._ItemsDenormalized = true;
        }
        this._Items.Normalize(default (TNode));
        this._ItemsDenormalized = false;
        break;
      case PXDBOperation.Delete:
        using (IEnumerator<TNode> enumerator = collection.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            if (this.PersistDeleted((object) enumerator.Current))
              ++num1;
          }
          break;
        }
    }
    this._Persisting = false;
    return num1;
  }

  /// <summary>Saves the modification of the specified type from the cache
  /// to the database for a particular data record.</summary>
  /// <param name="row">The data record to save to the database.</param>
  /// <param name="operation">The database operation to perform for the data
  /// record, either one of <tt>PXDBOperation.Insert</tt>,
  /// <tt>PXDBOperation.Update</tt>, and <tt>PXDBOperation.Delete</tt>
  /// values or their bitwise "or" (<tt>|</tt>) combination.</param>
  public override void Persist(object row, PXDBOperation operation)
  {
    switch (operation)
    {
      case PXDBOperation.Update:
        this.PersistUpdated(row);
        break;
      case PXDBOperation.Insert:
        this.PersistInserted(row);
        break;
      case PXDBOperation.Delete:
        this.PersistDeleted(row);
        break;
    }
  }

  protected internal override void _AdjustStorage(string name, PXDataFieldParam assign)
  {
    if (this._FieldsMap.TryGetValue(name, out int _))
    {
      this._AdjustStorage(this._ReverseMap[name], assign);
    }
    else
    {
      StorageBehavior storageBehavior;
      if (!this._KeyValueAttributeTypes.TryGetValue(name, out storageBehavior))
        return;
      assign.Storage = storageBehavior;
    }
  }

  protected internal override void _AdjustStorage(int i, PXDataFieldParam assign)
  {
    if (!this.IsKvExtField(this._ClassFields[i]))
      return;
    assign.Column = new Column(this._ClassFields[i]);
    switch (System.Type.GetTypeCode(this._FieldTypes[i]))
    {
      case TypeCode.Object:
        if (this._FieldTypes[i] == typeof (Guid))
        {
          assign.Storage = StorageBehavior.KeyValueString;
          if (assign.Value == null)
            break;
          assign.Value = (object) ((Guid) assign.Value).ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        }
        if (!(this._FieldTypes[i] == typeof (byte[])))
          break;
        assign.Storage = StorageBehavior.KeyValueText;
        if (assign.Value == null)
          break;
        assign.Value = (object) Convert.ToBase64String((byte[]) assign.Value);
        break;
      case TypeCode.Boolean:
      case TypeCode.Int16:
      case TypeCode.Int32:
      case TypeCode.Int64:
      case TypeCode.Single:
      case TypeCode.Double:
      case TypeCode.Decimal:
        assign.Storage = StorageBehavior.KeyValueNumeric;
        if (assign.Value == null)
          break;
        assign.Value = (object) Convert.ToDecimal(assign.Value);
        break;
      case TypeCode.DateTime:
        assign.Storage = StorageBehavior.KeyValueDate;
        break;
      case TypeCode.String:
        if (assign.ValueType != PXDbType.Text && assign.ValueType != PXDbType.NText && assign.ValueLength.HasValue)
        {
          int? valueLength = assign.ValueLength;
          int num = 256 /*0x0100*/;
          if (!(valueLength.GetValueOrDefault() > num & valueLength.HasValue))
          {
            assign.Storage = StorageBehavior.KeyValueString;
            break;
          }
        }
        assign.Storage = StorageBehavior.KeyValueText;
        break;
    }
  }

  /// <summary>Updates the provided data record in the database. Returns
  /// <tt>true</tt> if the data record has been updated sucessfully, or
  /// <tt>false</tt> otherwise.</summary>
  /// <remarks>
  /// <para>The method raises the following events: <tt>RowPersisting</tt>,
  /// <tt>CommandPreparing</tt>, <tt>RowPersisted</tt>,
  /// <tt>ExceptionHandling</tt>.</para>
  /// <para>The default behavior can be modified by the
  /// <tt>PXDBInterceptor</tt> attribute.</para>
  /// </remarks>
  /// <param name="row">The data record to update in the database.</param>
  protected internal override bool PersistUpdated(object row, bool bypassInterceptor)
  {
    if (this.persistedItems == null)
      this.persistedItems = new Dictionary<TNode, bool>();
    bool flag1;
    if (this.persistedItems.TryGetValue((TNode) row, out flag1))
      return !flag1;
    if (!this.DisableCloneAttributes)
      PXCache.TryDispose((object) this.GetAttributes(row, (string) null));
    flag1 = true;
    try
    {
      flag1 = !this.OnRowPersisting(row, PXDBOperation.Update);
      if (!flag1)
      {
        if (this._Interceptor != null)
        {
          if (!bypassInterceptor)
          {
            flag1 = true;
            flag1 = !this._Interceptor.PersistUpdated((PXCache) this, row);
            return !flag1;
          }
        }
      }
    }
    catch (PXCommandPreparingException ex)
    {
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXRowPersistingException ex)
    {
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
        throw this.GetLockViolationException((TNode) row, PXDBOperation.Insert, ex, ex.Table);
      if (ex is PXDataWouldBeTruncatedException truncatedException)
      {
        truncatedException.Operation = new PXDBOperation?(PXDBOperation.Update);
        if (!string.IsNullOrEmpty(truncatedException.CommandText))
          PXTrace.WriteError("{0}{1}SQL query:{1}{2}", (object) truncatedException.Message, (object) Environment.NewLine, (object) truncatedException.CommandText);
      }
      ex.Keys = this.getKeys((TNode) row);
      throw;
    }
    finally
    {
      if (!bypassInterceptor)
        this.persistedItems.Add((TNode) row, flag1);
    }
    if (!flag1)
    {
      object unchanged = this.GetOriginal(row);
      List<PXDataFieldParam> pars = new List<PXDataFieldParam>();
      KeysVerifyer keysVerifyer = new KeysVerifyer((PXCache) this);
      bool flag2 = this._KeyValueStoredNames != null || this._KeyValueAttributeNames != null;
      try
      {
        for (int index = 0; index < this._ClassFields.Count; ++index)
        {
          string descr = this._ClassFields[index];
          PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
          object obj1 = this.GetValue(row, descr);
          this.OnCommandPreparing(descr, row, obj1, PXDBOperation.Update, (System.Type) null, out description1);
          if (description1 != null && description1.Expr != null)
          {
            object obj2 = unchanged.With<object, object>((Func<object, object>) (c => this.GetValue(unchanged, descr)));
            PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
            PXDataFieldAssign assign = (PXDataFieldAssign) null;
            Column column = (Column) null;
            if (obj2 != null)
              this.OnCommandPreparing(descr, unchanged, obj2, PXDBOperation.Update, (System.Type) null, out description2);
            if (description1.IsRestriction)
            {
              if (description1.DataValue != null)
                keysVerifyer.ExcludeField(descr);
              if (this.Keys.Contains(descr))
              {
                column = (Column) description1.Expr;
                assign = new PXDataFieldAssign(column, description1.DataType, description1.DataLength, description1.DataValue, this.ValueToString(descr, obj1, description1.DataValue))
                {
                  OldValue = description2?.DataValue ?? obj2
                };
              }
              if (obj2 != null && description1.DataType != PXDbType.Timestamp && this.Keys.Contains(descr) && !object.Equals(obj2, obj1))
              {
                if (description2 != null && description2.Expr != null)
                {
                  pars.Add((PXDataFieldParam) assign);
                  pars.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description2.Expr, description2.DataType, description2.DataLength, description2.DataValue));
                }
                else
                  pars.Add((PXDataFieldParam) new PXDataFieldRestrict(column, description1.DataType, description1.DataLength, description1.DataValue));
              }
              else
                pars.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description1.Expr, description1.DataType, description1.DataLength, description1.DataValue));
            }
            else
            {
              Column expr = (Column) description1.Expr;
              assign = new PXDataFieldAssign(expr, description1.DataType, description1.DataLength, description1.DataValue, (string) null);
              if (description1.IsExcludedFromUpdate)
              {
                PXDummyDataFieldRestrict dataFieldRestrict = new PXDummyDataFieldRestrict(expr, description1.DataType, description1.DataLength, description2?.DataValue ?? obj2);
                pars.Add((PXDataFieldParam) dataFieldRestrict);
              }
              else
              {
                if (unchanged != null)
                {
                  if (assign.IsChanged = !object.Equals(this.GetValue(row, descr), obj2))
                  {
                    assign.NewValue = this.ValueToString(descr, obj1, description1.DataValue);
                    assign.OldValue = description2 == null || PXCache.IsOrigValueNewDate((PXCache) this, description2) ? obj2 : description2.DataValue;
                  }
                }
                else
                  assign.IsChanged = false;
                this._AdjustStorage(index, (PXDataFieldParam) assign);
                pars.Add((PXDataFieldParam) assign);
              }
            }
            if (flag2 && string.Equals(descr, this._NoteIDName, StringComparison.OrdinalIgnoreCase) && assign != null)
            {
              this.ProcessUpdatedKeyValueAttributes(assign, row, pars);
              flag2 = false;
            }
          }
          else if (description1 != null)
            keysVerifyer.ExcludeField(descr);
        }
      }
      catch (PXCommandPreparingException ex)
      {
        if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
          throw;
        PXTrace.WriteWarning((Exception) ex);
        return false;
      }
      try
      {
        pars.Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        keysVerifyer.Check(PXCache<TNode>._BqlTable);
        if (unchanged == null)
          pars.Add((PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues);
        if (!this._Graph.ProviderUpdate(PXCache<TNode>._BqlTable, pars.ToArray()))
          throw this.GetLockViolationException(pars.ToArray(), PXDBOperation.Update, row);
        try
        {
          this.OnRowPersisted(row, PXDBOperation.Update, PXTranStatus.Open, (Exception) null);
          BqlTablePair bqlTablePair;
          if (this._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
          {
            if (this._OriginalsRemoved == null)
              this._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
            this._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
          }
          this._Originals.Remove((IBqlTable) row);
        }
        catch (PXRowPersistedException ex)
        {
          this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
          throw;
        }
      }
      catch (PXDbOperationSwitchRequiredException ex1)
      {
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
        try
        {
          foreach (string classField in this._ClassFields)
          {
            string descr = classField;
            object objB = unchanged.With<object, object>((Func<object, object>) (c => this.GetValue(unchanged, descr)));
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            this.OnCommandPreparing(descr, row, this.GetValue(row, descr), PXDBOperation.Insert, (System.Type) null, out description);
            if (description?.Expr != null && !description.IsExcludedFromUpdate)
            {
              PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue);
              pxDataFieldAssignList.Add(pxDataFieldAssign);
              if (pxDataFieldAssign.IsChanged = !object.Equals(description.DataValue, objB))
                pxDataFieldAssign.OldValue = objB;
            }
          }
        }
        catch (PXCommandPreparingException ex2)
        {
          if (this.OnExceptionHandling(ex2.Name, row, ex2.Value, (Exception) ex2))
            throw;
          PXTrace.WriteWarning((Exception) ex1);
          return false;
        }
        try
        {
          this._Graph.ProviderInsert(PXCache<TNode>._BqlTable, pxDataFieldAssignList.ToArray());
          try
          {
            this.OnRowPersisted(row, PXDBOperation.Update, PXTranStatus.Open, (Exception) null);
            BqlTablePair bqlTablePair;
            if (this._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
            {
              if (this._OriginalsRemoved == null)
                this._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
              this._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
            }
            this._Originals.Remove((IBqlTable) row);
          }
          catch (PXRowPersistedException ex3)
          {
            this.OnExceptionHandling(ex3.Name, row, ex3.Value, (Exception) ex3);
            throw;
          }
        }
        catch (PXDatabaseException ex4)
        {
          if (ex4.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
            throw this.GetLockViolationException((PXDataFieldParam[]) pxDataFieldAssignList.ToArray(), PXDBOperation.Insert, ex4, ex4.Table);
          ex4.Keys = this.getKeys((TNode) row);
          throw;
        }
      }
      catch (PXDatabaseException ex)
      {
        if (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation && PXDatabase.IsReadDeletedSupported(PXCache<TNode>._BqlTable))
          throw this.GetLockViolationException((TNode) row, PXDBOperation.Update, ex, ex.Table);
        ex.Keys = this.getKeys((TNode) row);
        throw;
      }
    }
    return !flag1;
  }

  private void ProcessUpdatedKeyValueAttributes(
    PXDataFieldAssign assign,
    object row,
    List<PXDataFieldParam> pars)
  {
    if (assign.Value == null)
    {
      assign.Value = (object) SequentialGuid.Generate();
      this.SetValue(row, this._NoteIDOrdinal.Value, assign.Value);
    }
    PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(this._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
    pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
    pars.Add((PXDataFieldParam) pxDataFieldAssign1);
    if (this._KeyValueAttributeNames == null)
      return;
    object[] slot1 = this.GetSlot<object[]>(row, this._KeyValueAttributeSlotPosition);
    object[] slot2 = this.GetSlot<object[]>(row, this._KeyValueAttributeSlotPosition, true);
    if (slot1 == null)
      return;
    foreach (KeyValuePair<string, int> valueAttributeName in this._KeyValueAttributeNames)
    {
      if (valueAttributeName.Value < slot1.Length)
      {
        this.OnCommandPreparing(valueAttributeName.Key, row, slot1[valueAttributeName.Value], PXDBOperation.Update, (System.Type) null, out PXCommandPreparingEventArgs.FieldDescription _);
        PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(valueAttributeName.Key, this._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueDate ? PXDbType.DateTime : (this._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueNumeric ? PXDbType.Bit : PXDbType.NVarChar), slot1[valueAttributeName.Value]);
        pxDataFieldAssign2.Storage = this._KeyValueAttributeTypes[valueAttributeName.Key];
        if (pxDataFieldAssign2.IsChanged = slot2 != null && valueAttributeName.Value < slot2.Length && !object.Equals(pxDataFieldAssign2.Value, slot2[valueAttributeName.Value]))
        {
          pxDataFieldAssign2.NewValue = this.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign2.Value);
          pxDataFieldAssign2.OldValue = slot2[valueAttributeName.Value];
        }
        pars.Add((PXDataFieldParam) pxDataFieldAssign2);
      }
    }
  }

  /// <summary>Inserts the provided data record into the database. Returns
  /// <tt>true</tt> if the data record has been inserted sucessfully, or
  /// <tt>false</tt> otherwise.</summary>
  /// <remarks>
  /// <para>The method throws an exception if the data record with such keys
  /// exists in the database.</para>
  /// <para>The method raises the following events: <tt>RowPersisting</tt>,
  /// <tt>CommandPreparing</tt>, <tt>RowPersisted</tt>,
  /// <tt>ExceptionHandling</tt>.</para>
  /// <para>The default behavior can be modified by the
  /// <tt>PXDBInterceptor</tt> attribute.</para>
  /// </remarks>
  /// <param name="row">The data record to insert into the database.</param>
  protected internal override bool PersistInserted(object row, bool bypassInterceptor)
  {
    if (this.persistedItems == null)
      this.persistedItems = new Dictionary<TNode, bool>();
    bool flag1;
    if (this.persistedItems.TryGetValue((TNode) row, out flag1))
      return !flag1;
    if (!this.DisableCloneAttributes)
      PXCache.TryDispose((object) this.GetAttributes(row, (string) null));
    flag1 = true;
    try
    {
      flag1 = !this.OnRowPersisting(row, PXDBOperation.Insert);
      if (!flag1)
      {
        if (this._Interceptor != null)
        {
          if (!bypassInterceptor)
          {
            flag1 = true;
            flag1 = !this._Interceptor.PersistInserted((PXCache) this, row);
            return !flag1;
          }
        }
      }
    }
    catch (PXCommandPreparingException ex)
    {
      this._Items.Normalize(default (TNode));
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXRowPersistingException ex)
    {
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
        throw this.GetLockViolationException((TNode) row, PXDBOperation.Insert, ex, ex.Table);
      if (ex is PXDataWouldBeTruncatedException truncatedException)
      {
        truncatedException.Operation = new PXDBOperation?(PXDBOperation.Insert);
        if (!string.IsNullOrEmpty(truncatedException.CommandText))
          PXTrace.WriteError("{0}{1}SQL query:{1}{2}", (object) truncatedException.Message, (object) Environment.NewLine, (object) truncatedException.CommandText);
      }
      ex.Keys = this.getKeys((TNode) row);
      throw;
    }
    finally
    {
      if (!bypassInterceptor)
        this.persistedItems.Add((TNode) row, flag1);
    }
    if (!flag1)
    {
      System.Type table = this.BqlTable;
      bool flag2;
      while (!(flag2 = PXDatabase.AuditRequired(table)) && table.BaseType != typeof (object))
        table = table.BaseType;
      List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
      bool flag3 = this._KeyValueStoredNames != null || this._KeyValueAttributeNames != null;
      try
      {
        for (int index = 0; index < this._ClassFields.Count; ++index)
        {
          string classField = this._ClassFields[index];
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          object val = this.GetValue(row, classField);
          this.OnCommandPreparing(classField, row, this.GetValue(row, classField), PXDBOperation.Insert, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate)
          {
            PXDataFieldAssign assign;
            pxDataFieldAssignList.Add(assign = new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue, (string) null));
            if (flag2 && val != null)
            {
              assign.IsChanged = true;
              assign.NewValue = this.ValueToString(classField, val, description.DataValue);
            }
            else
              assign.IsChanged = false;
            if (flag3 && string.Equals(classField, this._NoteIDName, StringComparison.OrdinalIgnoreCase))
            {
              if (assign.Value == null)
              {
                assign.Value = (object) SequentialGuid.Generate();
                this.SetValue(row, this._NoteIDOrdinal.Value, assign.Value);
              }
              PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign(this._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
              pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
              pxDataFieldAssignList.Add(pxDataFieldAssign1);
              flag3 = false;
              if (this._KeyValueAttributeNames != null)
              {
                object[] slot = this.GetSlot<object[]>(row, this._KeyValueAttributeSlotPosition);
                if (slot != null)
                {
                  foreach (KeyValuePair<string, int> valueAttributeName in this._KeyValueAttributeNames)
                  {
                    if (valueAttributeName.Value < slot.Length)
                    {
                      this.OnCommandPreparing(valueAttributeName.Key, row, slot[valueAttributeName.Value], PXDBOperation.Insert, (System.Type) null, out PXCommandPreparingEventArgs.FieldDescription _);
                      PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(valueAttributeName.Key, this._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueDate ? PXDbType.DateTime : (this._KeyValueAttributeTypes[valueAttributeName.Key] == StorageBehavior.KeyValueNumeric ? PXDbType.Bit : PXDbType.NVarChar), slot[valueAttributeName.Value]);
                      pxDataFieldAssign2.Storage = this._KeyValueAttributeTypes[valueAttributeName.Key];
                      if (pxDataFieldAssign2.IsChanged = flag2 && pxDataFieldAssign2.Value != null)
                        pxDataFieldAssign2.NewValue = this.AttributeValueToString(valueAttributeName.Key, pxDataFieldAssign2.Value);
                      pxDataFieldAssignList.Add(pxDataFieldAssign2);
                    }
                  }
                }
              }
            }
            this._AdjustStorage(index, (PXDataFieldParam) assign);
          }
        }
      }
      catch (PXCommandPreparingException ex)
      {
        this._Items.Normalize(default (TNode));
        if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
          throw;
        PXTrace.WriteWarning((Exception) ex);
        return false;
      }
      try
      {
        pxDataFieldAssignList.Add(PXDataFieldAssign.OperationSwitchAllowed);
        this._Graph.ProviderInsert(PXCache<TNode>._BqlTable, pxDataFieldAssignList.ToArray());
        try
        {
          this.OnRowPersisted(row, PXDBOperation.Insert, PXTranStatus.Open, (Exception) null);
          BqlTablePair bqlTablePair;
          if (this._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
          {
            if (this._OriginalsRemoved == null)
              this._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
            this._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
          }
          this._Originals.Remove((IBqlTable) row);
        }
        catch (PXRowPersistedException ex)
        {
          this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
          throw;
        }
      }
      catch (PXDbOperationSwitchRequiredException ex1)
      {
        TNode unchanged = default (TNode);
        try
        {
          BqlTablePair bqlTablePair;
          if (this._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
            unchanged = bqlTablePair.Unchanged as TNode;
        }
        catch
        {
        }
        List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
        KeysVerifyer keysVerifyer = new KeysVerifyer((PXCache) this);
        try
        {
          foreach (string classField in this._ClassFields)
          {
            string descr = classField;
            IPXIdentityColumn pxIdentityColumn = (IPXIdentityColumn) null;
            int index1;
            if (descr.Equals(this.Identity, StringComparison.Ordinal) && this._FieldsMap.TryGetValue(descr, out index1))
            {
              for (int index2 = this._FieldAttributesFirst[index1]; index2 <= this._FieldAttributesLast[index1] && pxIdentityColumn == null; ++index2)
                pxIdentityColumn = this._FieldAttributes[index2] as IPXIdentityColumn;
            }
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            object val = this.GetValue(row, descr);
            this.OnCommandPreparing(descr, row, val, PXDBOperation.Update | PXDBOperation.Second, (System.Type) null, out description);
            if (description != null)
            {
              if (description.Expr == null)
                keysVerifyer.ExcludeField(descr);
              else if (description.IsRestriction)
              {
                if (description.DataValue != null)
                  keysVerifyer.ExcludeField(descr);
                object obj = pxIdentityColumn == null ? description.DataValue : pxIdentityColumn.GetLastInsertedIdentity(description.DataValue);
                pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, obj));
              }
              else
              {
                object objB = unchanged.With<TNode, object>((Func<TNode, object>) (c => this.GetValue((object) unchanged, descr)));
                Column expr = (Column) description.Expr;
                if (description.IsExcludedFromUpdate)
                {
                  if ((object) unchanged != null)
                  {
                    PXDummyDataFieldRestrict dataFieldRestrict = new PXDummyDataFieldRestrict(expr, description.DataType, description.DataLength, objB);
                    pxDataFieldParamList.Add((PXDataFieldParam) dataFieldRestrict);
                  }
                }
                else
                {
                  PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(expr, description.DataType, description.DataLength, description.DataValue, (string) null);
                  if ((object) unchanged != null)
                  {
                    if (pxDataFieldAssign.IsChanged = !object.Equals(this.GetValue(row, descr), objB))
                      pxDataFieldAssign.NewValue = this.ValueToString(descr, val, description.DataValue);
                  }
                  else
                    pxDataFieldAssign.IsChanged = false;
                  pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign);
                }
              }
            }
          }
        }
        catch (PXCommandPreparingException ex2)
        {
          if (this.OnExceptionHandling(ex2.Name, row, ex2.Value, (Exception) ex2))
            throw;
          PXTrace.WriteWarning((Exception) ex1);
          return false;
        }
        try
        {
          keysVerifyer.Check(PXCache<TNode>._BqlTable);
          if ((object) unchanged == null)
            pxDataFieldParamList.Add((PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues);
          if (!this._Graph.ProviderUpdate(PXCache<TNode>._BqlTable, pxDataFieldParamList.ToArray()))
            throw this.GetLockViolationException(pxDataFieldParamList.ToArray(), PXDBOperation.Update);
          try
          {
            this.OnRowPersisted(row, PXDBOperation.Insert, PXTranStatus.Open, (Exception) null);
            BqlTablePair bqlTablePair;
            if (this._Originals.TryGetValue((IBqlTable) row, out bqlTablePair))
            {
              if (this._OriginalsRemoved == null)
                this._OriginalsRemoved = new PXCacheRemovedOriginalsCollection();
              this._OriginalsRemoved[(IBqlTable) row] = bqlTablePair;
            }
            this._Originals.Remove((IBqlTable) row);
          }
          catch (PXRowPersistedException ex3)
          {
            this.OnExceptionHandling(ex3.Name, row, ex3.Value, (Exception) ex3);
            throw;
          }
        }
        catch (PXDatabaseException ex4)
        {
          if (ex4.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
            throw this.GetLockViolationException(pxDataFieldParamList.ToArray(), PXDBOperation.Insert, ex4, ex4.Table);
          ex4.Keys = this.getKeys((TNode) row);
          throw;
        }
      }
      catch (PXDatabaseException ex)
      {
        if (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
          throw this.GetLockViolationException((PXDataFieldParam[]) pxDataFieldAssignList.ToArray(), PXDBOperation.Insert, ex, ex.Table);
        ex.Keys = this.getKeys((TNode) row);
        throw;
      }
    }
    return !flag1;
  }

  /// <summary>Deletes the provided data record from the database by the key
  /// fields. Returns <tt>true</tt> if the data record has been deleted
  /// sucessfully, or <tt>false</tt> otherwise.</summary>
  /// <remarks>
  /// <para>The method raises the following events: <tt>RowPersisting</tt>,
  /// <tt>CommandPreparing</tt>, <tt>RowPersisted</tt>,
  /// <tt>ExceptionHandling</tt>.</para>
  /// <para>The default behavior can be modified by the
  /// <tt>PXDBInterceptor</tt> attribute.</para>
  /// </remarks>
  /// <param name="row">The data record to deleted from the
  /// database.</param>
  protected internal override bool PersistDeleted(object row, bool bypassInterceptor)
  {
    if (this.persistedItems == null)
      this.persistedItems = new Dictionary<TNode, bool>();
    bool flag1;
    if (this.persistedItems.TryGetValue((TNode) row, out flag1))
      return !flag1;
    if (!this.DisableCloneAttributes)
      PXCache.TryDispose((object) this.GetAttributes(row, (string) null));
    flag1 = true;
    try
    {
      flag1 = !this.OnRowPersisting(row, PXDBOperation.Delete);
      if (!flag1)
      {
        if (this._Interceptor != null)
        {
          if (!bypassInterceptor)
          {
            flag1 = true;
            flag1 = !this._Interceptor.PersistDeleted((PXCache) this, row);
            return !flag1;
          }
        }
      }
    }
    catch (PXCommandPreparingException ex)
    {
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXRowPersistingException ex)
    {
      if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        throw;
      PXTrace.WriteWarning((Exception) ex);
      return false;
    }
    catch (PXDatabaseException ex)
    {
      object[] keys = this.getKeys((TNode) row);
      ex.Keys = keys;
      throw;
    }
    finally
    {
      if (!bypassInterceptor)
        this.persistedItems.Add((TNode) row, flag1);
    }
    if (!flag1)
    {
      TNode unchanged = this.GetOriginal(row) as TNode;
      List<PXDataFieldRestrict> dataFieldRestrictList = new List<PXDataFieldRestrict>();
      bool flag2 = this._KeyValueStoredNames != null || this._DBLocalizableNames != null || this._KeyValueAttributeNames != null;
      try
      {
        for (int index = 0; index < this._ClassFields.Count; ++index)
        {
          string descr = this._ClassFields[index];
          PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
          this.OnCommandPreparing(descr, row, this.GetValue(row, descr), PXDBOperation.Delete, (System.Type) null, out description1);
          if (description1 != null && description1.Expr != null)
          {
            Column expr = (Column) description1.Expr;
            if (description1.IsRestriction)
            {
              dataFieldRestrictList.Add(new PXDataFieldRestrict(expr, description1.DataType, description1.DataLength, description1.DataValue));
            }
            else
            {
              object obj = unchanged.With<TNode, object>((Func<TNode, object>) (c => this.GetValue((object) unchanged, descr)));
              PXCommandPreparingEventArgs.FieldDescription description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
              if (obj != null)
                this.OnCommandPreparing(descr, (object) unchanged, obj, PXDBOperation.Update, (System.Type) null, out description2);
              PXDataFieldRestrict assign = this.IsKvExtField(this._ClassFields[index]) ? new PXDataFieldRestrict(expr, description1.DataType, description1.DataLength, description1.DataValue) : (PXDataFieldRestrict) new PXDummyDataFieldRestrict(expr, description1.DataType, description1.DataLength, (object) unchanged != null ? description2?.DataValue ?? obj : description1.DataValue);
              if (flag2 && string.Equals(descr, this._NoteIDName, StringComparison.OrdinalIgnoreCase))
              {
                if (assign.Value == null)
                {
                  assign.Value = (object) SequentialGuid.Generate();
                  this.SetValue(row, this._NoteIDOrdinal.Value, assign.Value);
                }
                PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict(this._NoteIDName, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), assign.Value);
                dataFieldRestrict.Storage = StorageBehavior.KeyValueKey;
                dataFieldRestrictList.Add(dataFieldRestrict);
                flag2 = false;
              }
              this._AdjustStorage(index, (PXDataFieldParam) assign);
              dataFieldRestrictList.Add(assign);
            }
          }
        }
      }
      catch (PXCommandPreparingException ex)
      {
        if (this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
          throw;
        PXTrace.WriteWarning((Exception) ex);
        return false;
      }
      try
      {
        if ((object) unchanged == null)
          dataFieldRestrictList.Add(PXSelectOriginalsRestrict.SelectAllOriginalValues);
        if (!this._Graph.ProviderDelete(PXCache<TNode>._BqlTable, dataFieldRestrictList.ToArray()))
          throw this.GetLockViolationException((PXDataFieldParam[]) dataFieldRestrictList.ToArray(), PXDBOperation.Delete);
        try
        {
          this.OnRowPersisted(row, PXDBOperation.Delete, PXTranStatus.Open, (Exception) null);
        }
        catch (PXRowPersistedException ex)
        {
          this.OnExceptionHandling(ex.Name, row, ex.Value, (Exception) ex);
          throw;
        }
      }
      catch (PXDatabaseException ex1)
      {
        if (ex1 is PXDbOperationSwitchRequiredException)
        {
          List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
          try
          {
            foreach (string classField in this._ClassFields)
            {
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              this.OnCommandPreparing(classField, row, this.GetValue(row, classField), PXDBOperation.Insert, (System.Type) null, out description);
              if (description?.Expr != null && !description.IsExcludedFromUpdate)
                pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
            }
          }
          catch (PXCommandPreparingException ex2)
          {
            if (this.OnExceptionHandling(ex2.Name, row, ex2.Value, (Exception) ex2))
              throw;
            PXTrace.WriteWarning((Exception) ex1);
            return false;
          }
          try
          {
            this._Graph.ProviderInsert(PXCache<TNode>._BqlTable, pxDataFieldAssignList.ToArray());
            try
            {
              this.OnRowPersisted(row, PXDBOperation.Delete, PXTranStatus.Open, (Exception) null);
            }
            catch (PXRowPersistedException ex3)
            {
              this.OnExceptionHandling(ex3.Name, row, ex3.Value, (Exception) ex3);
              throw;
            }
          }
          catch (PXDatabaseException ex4)
          {
            if (ex4.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
              throw this.GetLockViolationException((PXDataFieldParam[]) pxDataFieldAssignList.ToArray(), PXDBOperation.Insert, ex4, ex4.Table);
            ex4.Keys = this.getKeys((TNode) row);
            throw;
          }
        }
        else
        {
          ex1.Keys = this.getKeys((TNode) row);
          throw;
        }
      }
    }
    return !flag1;
  }

  /// <exclude />
  public override void ResetPersisted(object row)
  {
    if (this.persistedItems == null || !(row is TNode key) || !this.persistedItems.ContainsKey(key))
      return;
    this.persistedItems.Remove((TNode) row);
  }

  protected internal override void ResetState()
  {
    this.persistedItems = (Dictionary<TNode, bool>) null;
    this.AllowDelete = true;
    this.AllowInsert = true;
    this.AllowSelect = true;
    this.AllowUpdate = true;
    foreach (IPXInterfaceField pxInterfaceField in this.AttributesWithError)
    {
      pxInterfaceField.ErrorText = (string) null;
      pxInterfaceField.ErrorLevel = PXErrorLevel.Undefined;
      pxInterfaceField.ErrorValue = (object) null;
    }
    this.AttributesWithError = new List<IPXInterfaceField>();
  }

  /// <summary>Completes saving changes to the database by raising the
  /// <tt>RowPersisted</tt> event for all persisted data records.</summary>
  /// <param name="isAborted">The value indicating whether the database
  /// operation has been aborted or completed.</param>
  /// <example><para>You need to call this method in the application only when you call the Persist(), PersistInserted(), PersistUpdated(), or PersistDeleted() method, as the following example shows.</para>
  /// <code title="Example" lang="CS">
  /// // Opening a transaction and saving changes to the provided
  /// // new data record
  /// using (PXTransactionScope ts = new PXTransactionScope())
  /// {
  ///     cache.PersistInserted(item);
  ///     ts.Complete(this);
  /// }
  /// // Indicating successful completion of saving changes to the database
  /// cache.Persisted(false);</code>
  /// </example>
  public override void Persisted(bool isAborted) => this.Persisted(isAborted, (Exception) null);

  protected internal override void Persisted(bool isAborted, Exception exception)
  {
    if (this.persistedItems == null)
      return;
    if (this._OriginalsRemoved != null)
    {
      if (isAborted)
        this._OriginalsRemoved.Restore(this._Originals);
      this._OriginalsRemoved = (PXCacheRemovedOriginalsCollection) null;
    }
    List<object> objectList = new List<object>();
    foreach (TNode key in this._Items.Updated)
    {
      if (this.persistedItems.ContainsKey(key))
      {
        this.OnRowPersisted((object) key, PXDBOperation.Update, isAborted ? PXTranStatus.Aborted : PXTranStatus.Completed, exception);
        if (!isAborted && !this.persistedItems[key])
        {
          this.SetStatus((object) key, PXEntryStatus.Notchanged);
          objectList.Add((object) key);
        }
      }
    }
    foreach (TNode key in this._Items.Inserted)
    {
      if (this.persistedItems.ContainsKey(key))
      {
        this.OnRowPersisted((object) key, PXDBOperation.Insert, isAborted ? PXTranStatus.Aborted : PXTranStatus.Completed, exception);
        if (!isAborted && !this.persistedItems[key])
        {
          this.SetStatus((object) key, PXEntryStatus.Notchanged);
          objectList.Add((object) key);
        }
      }
    }
    if (isAborted)
    {
      this._Items.Normalize(default (TNode));
      if (this.persistedItems.Count > 0)
        this.ClearQueryCache();
    }
    foreach (TNode key in this._Items.Deleted)
    {
      if (this.persistedItems.ContainsKey(key))
      {
        this.OnRowPersisted((object) key, PXDBOperation.Delete, isAborted ? PXTranStatus.Aborted : PXTranStatus.Completed, exception);
        if (!isAborted && !this.persistedItems[key])
          this.SetStatus((object) key, PXEntryStatus.InsertedDeleted);
      }
    }
    if (!isAborted)
      this._IsDirty = false;
    this.persistedItems = (Dictionary<TNode, bool>) null;
    this._Graph.WorkflowService?.CheckPrimaryWorkflowItemPersisted(this._Graph, PXCache<TNode>._BqlTable, objectList);
    PXQuickProcess.Engine.Instance?.StorePersisted(PXCache<TNode>._BqlTable, (IEnumerable<object>) objectList);
  }

  protected override void TryEnsureRowHasNotBeenPersistedYet(object row)
  {
    if (!this._ForbidChangesAfterPersist || this.persistedItems == null || !(row is TNode node) || !this.IsKeysFilled(node))
      return;
    bool flag;
    int num;
    if (!this.persistedItems.TryGetValue(node, out flag))
    {
      TNode key = this.Locate(node);
      num = (object) key == null ? 0 : (this.persistedItems.TryGetValue(key, out flag) ? 1 : 0);
    }
    else
      num = 1;
    if (num != 0 && !flag)
      throw new PXInvalidOperationException(DacDescriptorUtils.GetNonEmptyDacDescriptor(this.Graph, (IBqlTable) node), "The system detected an attempt to modify a record that had already been saved to the application database.");
  }

  protected internal override void ClearSessionUnmodified()
  {
    this.ClearSessionUnmodified((IEnumerable<TNode>) this._Items);
  }

  private void ClearSessionUnmodified(IEnumerable<TNode> items)
  {
    foreach (TNode key in items)
    {
      BqlTablePair bqlTablePair;
      if ((object) key != null && this._Originals.TryGetValue((IBqlTable) key, out bqlTablePair))
        bqlTablePair.SessionUnmodified = (SessionUnmodifiedPair) null;
    }
  }

  internal override void ReinitializeCollection()
  {
    PXCollection<TNode> items = this._Items;
    this._Items = new PXCollection<TNode>((PXCache) this);
    EnumerableExtensions.ForEach<TNode>(items.Updated, (System.Action<TNode>) (item => this._Items.PlaceUpdated(item, true)));
    EnumerableExtensions.ForEach<TNode>(items.Inserted, (System.Action<TNode>) (item => this._Items.PlaceInserted(item, out bool _)));
    EnumerableExtensions.ForEach<TNode>(items.Deleted, (System.Action<TNode>) (item => this._Items.PlaceDeleted(item, true)));
    EnumerableExtensions.ForEach<TNode>(items.Held, (System.Action<TNode>) (item =>
    {
      this._Items.PlaceNotChanged(item, out bool _);
      this._Items.SetStatus(item, PXEntryStatus.Held);
    }));
    if (!(this._Current is TNode) || (object) items.Locate((TNode) this._Current) == null || items.GetStatus((TNode) this._Current) == PXEntryStatus.InsertedDeleted || (object) this._Items.Locate((TNode) this._Current) != null)
      return;
    this._Items.PlaceNotChanged((TNode) this._Current, out bool _);
    this._CurrentPlacedIntoCache = (TNode) this._Current;
  }

  /// <summary>Loads dirty items and other cache state objects from the
  /// session. The application does not typically use this method.</summary>
  public override void Load()
  {
    if (this.stateLoaded)
    {
      if (!this._Graph.stateLoading)
        return;
      if (this._Current == null)
        return;
      try
      {
        this.OnRowSelected((object) (TNode) this._Current);
      }
      catch
      {
      }
    }
    else if (!PXContext.Session.IsSessionEnabled)
    {
      this._Graph.Caches.AttachAllHandlers<TNode>(this);
    }
    else
    {
      this.stateLoaded = true;
      if (this.Graph.UnattendedMode)
      {
        this._Graph.Caches.AttachAllHandlers<TNode>(this);
      }
      else
      {
        this.LoadFromSession(true);
        this._Graph.Caches.AttachAllHandlers<TNode>(this);
      }
    }
  }

  internal override void LoadFromSession(bool force)
  {
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner == null || this.stateLoaded && !force)
      return;
    object[] cacheInfo = inner.GetCacheInfo(this._Graph, typeof (TNode));
    if (cacheInfo == null || cacheInfo.Length == 0)
      return;
    if (cacheInfo[0] is TNode[] nodeArray1)
    {
      for (int index = 0; index < nodeArray1.Length; ++index)
      {
        TNode node = this._Items.PlaceUpdated(nodeArray1[index], true);
        BqlTablePair bqlTablePair;
        if ((object) node != null && this._Originals.TryGetValue((IBqlTable) node, out bqlTablePair) && bqlTablePair.Unchanged is TNode && (object) node != bqlTablePair.Unchanged && !this.ObjectsEqual((object) node, (object) bqlTablePair.Unchanged))
        {
          if (this._ChangedKeys == null)
            this._ChangedKeys = new Dictionary<TNode, TNode>((IEqualityComparer<TNode>) new PXCache<TNode>.ItemComparer((PXCache) this));
          this._ChangedKeys[(TNode) bqlTablePair.Unchanged] = node;
        }
      }
      if (this._ChangedKeys != null)
        this.ClearQueryCache();
    }
    if (cacheInfo.Length > 1 && cacheInfo[1] is TNode[] nodeArray2)
    {
      for (int index = 0; index < nodeArray2.Length; ++index)
        this._Items.PlaceInserted(nodeArray2[index], out bool _);
    }
    if (cacheInfo.Length > 2 && cacheInfo[2] is TNode[] nodeArray3)
    {
      for (int index = 0; index < nodeArray3.Length; ++index)
        this._Items.PlaceDeleted(nodeArray3[index], true);
    }
    if (cacheInfo.Length > 3 && cacheInfo[3] is TNode[] nodeArray4)
    {
      for (int index = 0; index < nodeArray4.Length; ++index)
      {
        this._Items.PlaceNotChanged(nodeArray4[index], out bool _);
        this._Items.SetStatus(nodeArray4[index], PXEntryStatus.Held);
      }
    }
    if (cacheInfo.Length > 4)
    {
      if (cacheInfo[4] is TNode node3)
      {
        try
        {
          TNode node1;
          TNode node2 = node1 = this._Items.Locate(node3) ?? node3;
          if (force)
            this.Current = (object) node2;
          else
            this._Current = (object) node2;
          if ((object) node1 == null)
          {
            if (cacheInfo.Length > 6)
            {
              if ((bool) cacheInfo[6])
              {
                this._Items.PlaceNotChanged(node2, out bool _);
                this._CurrentPlacedIntoCache = node2;
              }
            }
          }
        }
        catch
        {
        }
      }
    }
    if (cacheInfo.Length > 5)
      this._IsDirty = (bool) cacheInfo[5];
    if (cacheInfo.Length > 7)
      this._Items.Version = (int) cacheInfo[7];
    if (cacheInfo.Length > 8)
      this.LoadErrors(cacheInfo[8] as PXUIErrorInfo[]);
    if (cacheInfo.Length > 9)
      this._IsInInsertState = (bool) cacheInfo[9];
    if (cacheInfo.Length > 10)
      this._CurrentVersionModified = (object) (cacheInfo[10] as TNode);
    this.ClearSessionUnmodified((IEnumerable<TNode>) this._Items);
  }

  private BqlTablePair OriginalGetter(TNode item)
  {
    BqlTablePair bqlTablePair;
    return this._Originals == null || !this._Originals.TryGetValue((IBqlTable) item, out bqlTablePair) ? (BqlTablePair) null : bqlTablePair;
  }

  private BqlTablePair RemovedOriginalGetter(TNode item)
  {
    BqlTablePair bqlTablePair;
    return this._OriginalsRemoved == null || !this._OriginalsRemoved.TryGetValue((IBqlTable) item, out bqlTablePair) ? (BqlTablePair) null : bqlTablePair;
  }

  private static bool TryGetUnmodified(
    Func<TNode, BqlTablePair> getter,
    TNode item,
    out TNode outItem,
    out PXEntryStatus? status)
  {
    outItem = default (TNode);
    status = new PXEntryStatus?();
    if ((object) item != null)
    {
      BqlTablePair bqlTablePair = getter(item);
      if (bqlTablePair != null && bqlTablePair.SessionUnmodified != null)
      {
        if (bqlTablePair.SessionUnmodified.Item != null)
        {
          outItem = (TNode) bqlTablePair.SessionUnmodified.Item;
          status = bqlTablePair.SessionUnmodified.Status;
        }
        return true;
      }
    }
    return false;
  }

  private void TryGetUnmodified(TNode item, out TNode outItem, out PXEntryStatus? status)
  {
    if (PXCache<TNode>.TryGetUnmodified(new Func<TNode, BqlTablePair>(this.OriginalGetter), item, out outItem, out status) || PXCache<TNode>.TryGetUnmodified(new Func<TNode, BqlTablePair>(this.RemovedOriginalGetter), item, out outItem, out status))
      return;
    outItem = item;
    status = new PXEntryStatus?(this.GetStatus(outItem));
  }

  private IEnumerable<(TNode item, TNode unmodified, PXEntryStatus status)> GetUnmodified(
    IEnumerable<TNode> items,
    bool ignoreNotChanged = true)
  {
    foreach (TNode node in items)
    {
      TNode outItem;
      PXEntryStatus? status;
      this.TryGetUnmodified(node, out outItem, out status);
      if ((object) outItem != null)
      {
        PXEntryStatus? nullable = new PXEntryStatus?();
        switch (status.Value)
        {
          case PXEntryStatus.Notchanged:
            if (!ignoreNotChanged)
            {
              nullable = new PXEntryStatus?(PXEntryStatus.Notchanged);
              break;
            }
            break;
          case PXEntryStatus.Updated:
          case PXEntryStatus.Modified:
            nullable = new PXEntryStatus?(PXEntryStatus.Updated);
            break;
          case PXEntryStatus.Inserted:
            nullable = new PXEntryStatus?(PXEntryStatus.Inserted);
            break;
          case PXEntryStatus.Deleted:
            nullable = new PXEntryStatus?(PXEntryStatus.Deleted);
            break;
          case PXEntryStatus.Held:
            nullable = new PXEntryStatus?(PXEntryStatus.Held);
            break;
        }
        if (nullable.HasValue)
          yield return (node, outItem, nullable.Value);
      }
    }
  }

  private TNode GetInitial(TNode item) => this.readItem(item, true) ?? item;

  private IEnumerable<(TNode item, TNode unmodified)> GetInitial(IEnumerable<TNode> items)
  {
    foreach (TNode node1 in items)
    {
      TNode node2 = this.readItem(node1, true);
      if ((object) node2 != null)
        yield return (node1, node2);
    }
  }

  private void InsertToItems(PXEntryStatus status, TNode item)
  {
    switch (status)
    {
      case PXEntryStatus.Notchanged:
        this._Items.PlaceNotChanged(item, out bool _);
        break;
      case PXEntryStatus.Updated:
        this._Items.PlaceUpdated(item, true);
        break;
      case PXEntryStatus.Inserted:
        this._Items.PlaceInserted(item, out bool _);
        break;
      case PXEntryStatus.Deleted:
        this._Items.PlaceDeleted(item, true);
        break;
      case PXEntryStatus.Held:
        this._Items.PlaceNotChanged(item, out bool _);
        this._Items.SetStatus(item, PXEntryStatus.Held);
        break;
    }
  }

  internal override void ResetToUnmodified(SessionRollback.FakeState fakeState)
  {
    bool flag = fakeState.HasFlag((Enum) SessionRollback.FakeState.UI);
    List<(TNode item, TNode unmodified, PXEntryStatus status)> list = this.GetUnmodified(this._Items.Cached, !flag).ToList<(TNode, TNode, PXEntryStatus)>();
    TNode current = (TNode) this._Current;
    TNode outItem = default (TNode);
    if ((object) current != null)
      this.TryGetUnmodified(current, out outItem, out PXEntryStatus? _);
    Dictionary<object, PXCache.DirtyItemState> itemAttributes = this._ItemAttributes;
    bool isDirty = this._IsDirty;
    this.Clear();
    this.ClearQueryCache();
    if (flag && itemAttributes != null && itemAttributes.Count > 0)
      this.InitItemAttributesCollection();
    foreach ((TNode item, TNode unmodified, PXEntryStatus status) tuple in list)
    {
      this.InsertToItems(tuple.status, tuple.unmodified);
      PXCache.DirtyItemState dirtyItemState;
      if (flag && this._ItemAttributes != null && itemAttributes.TryGetValue((object) tuple.item, out dirtyItemState))
        this._ItemAttributes[(object) tuple.unmodified] = dirtyItemState;
    }
    this._Current = (object) outItem;
    this._IsDirty = isDirty && this.Dirty.Count() > 0L;
    PXCache.DirtyItemState dirtyItemState1;
    if (!flag || (object) current == null || (object) outItem == null || this._ItemAttributes == null || !itemAttributes.TryGetValue((object) current, out dirtyItemState1))
      return;
    this._ItemAttributes[(object) outItem] = dirtyItemState1;
  }

  internal override void SaveUnmodifiedState()
  {
    foreach (TNode unmodified in this._Items.Cached)
      this.SetSessionUnmodified((object) unmodified, (object) unmodified);
    this.SetSessionUnmodified(this._Current, this._Current);
  }

  internal override void ResetToInitialState()
  {
    if (this.NonDBTable)
      return;
    List<(TNode item, TNode unmodified)> list = this.GetInitial(this._Items.Cached).ToList<(TNode, TNode)>();
    TNode current = (TNode) this._Current;
    TNode key = default (TNode);
    if ((object) current != null)
      key = list.Where<(TNode, TNode)>((Func<(TNode, TNode), bool>) (u => u.item.Equals((object) current))).FirstOrDefault<(TNode, TNode)>().Item2 ?? this.GetInitial(current);
    Dictionary<object, PXCache.DirtyItemState> itemAttributes = this._ItemAttributes;
    bool isDirty = this._IsDirty;
    this.Clear();
    this.ClearQueryCache();
    if (itemAttributes != null && itemAttributes.Count > 0)
      this.InitItemAttributesCollection();
    foreach ((TNode item, TNode unmodified) tuple in list)
    {
      this.InsertToItems(PXEntryStatus.Notchanged, tuple.unmodified);
      PXCache.DirtyItemState dirtyItemState;
      if (this._ItemAttributes != null && itemAttributes.TryGetValue((object) tuple.item, out dirtyItemState))
        this._ItemAttributes[(object) tuple.unmodified] = dirtyItemState;
    }
    this._Current = (object) key;
    this._IsDirty = isDirty && this.Dirty.Count() > 0L;
    PXCache.DirtyItemState dirtyItemState1;
    if ((object) current == null || (object) key == null || this._ItemAttributes == null || !itemAttributes.TryGetValue((object) current, out dirtyItemState1))
      return;
    this._ItemAttributes[(object) key] = dirtyItemState1;
  }

  internal override void CreateNewVersion()
  {
    if (!this.Graph.IsMobile)
      return;
    foreach (TNode node in this._Items.Cached)
      this.CreateNewVersion((object) node);
    if (this._Current == null)
      return;
    this._CurrentVersionModified = this.CreateCopy(this._Current);
  }

  internal override void DiscardCurrentVersion()
  {
    if (!this.Graph.IsMobile)
      return;
    foreach (TNode node in this._Items.Cached)
      this.DiscardCurrentVersion((object) node);
    if (this.Graph.ShouldSaveVersionModified || this._CurrentVersionModified == this._Current)
      return;
    this._Current = this._CurrentVersionModified;
  }

  internal override void SaveCurrentVersion()
  {
    if (!this.Graph.IsMobile)
      return;
    foreach (TNode node in this._Items.Cached)
      this.SaveCurrentVersion((object) node);
    if (this.Graph.ShouldSaveVersionModified)
      return;
    this._CurrentVersionModified = this._Current;
  }

  private void LoadErrors(PXUIErrorInfo[] errors)
  {
    if (errors == null || errors.Length == 0)
      return;
    foreach (PXUIErrorInfo error in errors)
    {
      PXErrorLevel? errorLevel = error.ErrorLevel;
      PXErrorLevel pxErrorLevel = PXErrorLevel.Error;
      if (errorLevel.GetValueOrDefault() == pxErrorLevel & errorLevel.HasValue)
        PXUIFieldAttribute.SetError((PXCache) this, error.CacheItem, error.FieldName, error.ErrorText, error.ErrorValue);
      else
        PXUIFieldAttribute.SetWarning((PXCache) this, error.CacheItem, error.FieldName, error.ErrorText);
    }
  }

  /// <summary>Clears the cache completely.</summary>
  /// <example>The following example demonstrates how to clear all POReceipt data records from the cache.
  /// <code title="Example" lang="CS">
  /// // Declaration of a data view in a graph
  /// public PXSelect&lt;POReceipt&gt; poreceiptslist;
  /// ...
  /// // Clearing the cache of POReceipt data records
  /// poreceiptslist.Cache.Clear();</code>
  /// </example>
  public override void Clear()
  {
    if (this.DisableCacheClear)
      return;
    this._Current = (object) null;
    this._CurrentPlacedIntoCache = default (TNode);
    this._Items = new PXCollection<TNode>((PXCache) this);
    this._ItemAttributes = (Dictionary<object, PXCache.DirtyItemState>) null;
    this._IsDirty = false;
    this._ChangedKeys = (Dictionary<TNode, TNode>) null;
  }

  /// <exclude />
  public override void ClearItemAttributes()
  {
    this._ItemAttributes = (Dictionary<object, PXCache.DirtyItemState>) null;
  }

  /// <exclude />
  public override void TrimItemAttributes(object item)
  {
    if (item == null || this._ItemAttributes == null || !this._ItemAttributes.ContainsKey(item) || this._Items != null && this._Items.Contains((TNode) item))
      return;
    this._ItemAttributes.Remove(item);
  }

  internal override bool HasChangedKeys()
  {
    return this._ChangedKeys != null && this._ChangedKeys.Count > 0;
  }

  internal override void ClearSession()
  {
    IPXSessionState inner = PXContext.Session.Inner;
    if (inner == null)
      return;
    inner.RemoveCacheInfo(this._Graph, typeof (TNode));
  }

  /// <summary>Serializes cache to the session.</summary>
  public override void Unload()
  {
    if (!this.AutoSave)
    {
      IPXSessionState inner = PXContext.Session.Inner;
      if (inner != null)
      {
        IEnumerable<TNode> source1 = (IEnumerable<TNode>) new List<TNode>(this._Items.Updated);
        IEnumerable<TNode> source2 = (IEnumerable<TNode>) new List<TNode>(this._Items.Inserted);
        IEnumerable<TNode> source3 = (IEnumerable<TNode>) new List<TNode>(this._Items.Deleted);
        IEnumerable<TNode> source4 = (IEnumerable<TNode>) new List<TNode>(this._Items.Held);
        List<PXUIErrorInfo> pxuiErrorInfoList = new List<PXUIErrorInfo>();
        if (this.Graph.PreserveErrorInfo || this.Graph.IsMobile && (source1.Count<TNode>() > 0 || source2.Count<TNode>() > 0) && this.Graph.PrimaryItemType != (System.Type) null && this.Graph.Caches[this.Graph.PrimaryItemType] == this)
        {
          this.HoldNotChangedRows();
          pxuiErrorInfoList = this.CollectErrorInfo(this.Graph.PreserveErrorInfo);
        }
        if (source1.Count<TNode>() > 0 || source2.Count<TNode>() > 0 || source3.Count<TNode>() > 0 || source4.Count<TNode>() > 0 || this._Current != null || this._IsDirty || this._Items.Version > 0 || this._IsInInsertState || this._CurrentVersionModified != null)
          inner.SetCacheInfo(this._Graph, typeof (TNode), new object[11]
          {
            (object) source1.ToArray<TNode>(),
            (object) source2.ToArray<TNode>(),
            (object) source3.ToArray<TNode>(),
            (object) source4.ToArray<TNode>(),
            (object) (this._Current as TNode),
            (object) this._IsDirty,
            (object) (bool) (!(this._Current is TNode) || (object) this._Items.Locate((TNode) this._Current) == null ? 0 : (this._Items.GetStatus((TNode) this._Current) != PXEntryStatus.InsertedDeleted ? 1 : 0)),
            (object) this._Items.Version,
            (object) pxuiErrorInfoList.ToArray(),
            (object) this._IsInInsertState,
            this._CurrentVersionModified
          });
        else
          inner.RemoveCacheInfo(this._Graph, typeof (TNode));
      }
    }
    else
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.Persist(PXDBOperation.Insert);
        this.Persist(PXDBOperation.Update);
        this.Persist(PXDBOperation.Delete);
        transactionScope.Complete();
      }
    }
    this._CurrentPlacedIntoCache = default (TNode);
  }

  private List<PXUIErrorInfo> CollectErrorInfo(bool collectall)
  {
    List<PXUIErrorInfo> pxuiErrorInfoList = new List<PXUIErrorInfo>();
    if (collectall)
    {
      foreach (string field in (List<string>) this.Fields)
      {
        foreach (object obj in this.Cached)
        {
          IPXInterfaceField pxInterfaceField = this.GetAttributesReadonly(obj, field).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>();
          if (pxInterfaceField != null && !string.IsNullOrEmpty(pxInterfaceField.ErrorText))
            pxuiErrorInfoList.Add(new PXUIErrorInfo(field, obj, pxInterfaceField.ErrorText, new PXErrorLevel?(pxInterfaceField.ErrorLevel), pxInterfaceField.ErrorValue));
        }
      }
    }
    else
    {
      Dictionary<object, PXCache.DirtyItemState> itemAttributes = this._ItemAttributes;
      // ISSUE: explicit non-virtual call
      if ((itemAttributes != null ? (__nonvirtual (itemAttributes.Count) > 0 ? 1 : 0) : 0) != 0)
      {
        foreach (KeyValuePair<object, PXCache.DirtyItemState> itemAttribute in this._ItemAttributes)
        {
          PXEntryStatus status;
          if (itemAttribute.Value.DirtyAttributes != null && ((status = this.GetStatus(itemAttribute.Key)) == PXEntryStatus.Inserted || status == PXEntryStatus.Updated))
          {
            foreach (PXEventSubscriberAttribute dirtyAttribute in itemAttribute.Value.DirtyAttributes)
            {
              if (dirtyAttribute is IPXInterfaceField && !string.IsNullOrEmpty(((IPXInterfaceField) dirtyAttribute).ErrorText))
                pxuiErrorInfoList.Add(new PXUIErrorInfo(dirtyAttribute.FieldName, itemAttribute.Key, ((IPXInterfaceField) dirtyAttribute).ErrorText, new PXErrorLevel?(((IPXInterfaceField) dirtyAttribute).ErrorLevel), ((IPXInterfaceField) dirtyAttribute).ErrorValue));
            }
          }
        }
      }
    }
    return pxuiErrorInfoList;
  }

  private void HoldNotChangedRows()
  {
    foreach (object row in this.Cached)
      this.Hold(row);
  }

  private System.Type GetTableFromSelect(string tableName)
  {
    return string.IsNullOrEmpty(tableName) || this.BqlSelect == null ? (System.Type) null : ((IEnumerable<System.Type>) this.BqlSelect.GetTables()).FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)));
  }

  private PXLockViolationException GetLockViolationException(
    TNode row,
    PXDBOperation operation,
    PXDatabaseException original,
    string tableName = null)
  {
    System.Type table = this.GetTableFromSelect(tableName);
    if ((object) table == null)
      table = PXCache<TNode>._BqlTable;
    object[] keys;
    bool deletedDatabaseRecord = PXDatabase.IsDeletedDatabaseRecord(table, this.Keys.ToArray(), this.getKeys(row), out keys);
    return new PXLockViolationException(PXCache<TNode>._BqlTable, operation, (object) row, keys, deletedDatabaseRecord, (Exception) original);
  }

  private PXLockViolationException GetLockViolationException(
    PXDataFieldParam[] parameters,
    PXDBOperation operation,
    PXDatabaseException original,
    string tableName,
    object instanceRow = null)
  {
    System.Type table = this.GetTableFromSelect(tableName);
    if ((object) table == null)
      table = PXCache<TNode>._BqlTable;
    object[] keys;
    bool deletedDatabaseRecord = PXDatabase.IsDeletedDatabaseRecord(table, parameters, out keys);
    return new PXLockViolationException(PXCache<TNode>._BqlTable, operation, instanceRow, keys, deletedDatabaseRecord, (Exception) original);
  }

  private PXLockViolationException GetLockViolationException(
    PXDataFieldParam[] parameters,
    PXDBOperation operation,
    object instanceRow = null)
  {
    return this.GetLockViolationException(parameters, operation, (PXDatabaseException) null, (string) null, instanceRow);
  }

  /// <summary>Returns the string representing the current cache
  /// object.</summary>
  public override string ToString()
  {
    return $"PXCache<{typeof (TNode).FullName}>({this._Items.CachedCount})";
  }

  void IPXDumpObjectState.DumpObjectState(StringBuilder b, int maxSize)
  {
    b.AppendLine("Cache:" + typeof (TNode).FullName);
    foreach (TNode data in this.Cached)
    {
      if (b.Length > maxSize)
      {
        b.AppendLine("*********Truncated********");
        break;
      }
      PXEntryStatus status = this.GetStatus(data);
      b.AppendLine(typeof (TNode).Name).Append(" ").Append((object) status).AppendLine();
      TNode original = this.GetOriginal(data);
      TNode outItem;
      this.TryGetUnmodified(data, out outItem, out PXEntryStatus? _);
      foreach (MemberInfo bqlField in this.BqlFields)
      {
        string name = bqlField.Name;
        object b1 = this.GetValue((object) data, name);
        object a = this.GetValue((object) original, name);
        object obj = this.GetValue((object) outItem, name);
        b.Append('\t').Append(name).Append(" = ");
        if ((object) original != null)
          b.Append(" DB:").Append(Convert.ToString(a));
        if ((object) outItem != null && !IsEqual(a, obj))
          b.Append(" Session:").Append(Convert.ToString(obj));
        if ((object) outItem != null && !IsEqual(obj, b1) || (object) outItem == null && (object) original != null && !IsEqual(a, b1) || (object) outItem == null && (object) original == null)
          b.Append(" Current:").Append(Convert.ToString(b1));
        b.AppendLine();
      }
    }

    static bool IsEqual(object a, object b) => a == null ? b == null : a.Equals(b);
  }

  private static void enumBaseTypes(
    System.Type typenode,
    IEnumerable<System.Type> extensions,
    Dictionary<System.Type, PXCache.CacheStaticInfoBase> baseinfos,
    bool ignoredResult)
  {
    // ISSUE: variable of a compiler-generated type
    PXCache<TNode>.\u003C\u003Ec__DisplayClass297_0 displayClass2970;
    // ISSUE: reference to a compiler-generated field
    displayClass2970.baseinfos = baseinfos;
    // ISSUE: reference to a compiler-generated field
    displayClass2970.ignoredResult = ignoredResult;
  }

  private static System.Type GetRealDACType(System.Type type)
  {
    for (System.Type baseType = type.BaseType; baseType != (System.Type) null; baseType = baseType.BaseType)
    {
      if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof (PXCacheExtension<>))
        return baseType.GetGenericArguments()[0];
    }
    return typeof (IBqlTable).IsAssignableFrom(type) ? type : (System.Type) null;
  }

  private static unsafe PXCache.CacheStaticInfoBase buildCacheStaticInfo(
    System.Type typenode,
    bool ignoredResult,
    PXExtensionManager.ListOfTypes lot)
  {
    Dictionary<System.Type, PXCache.CacheStaticInfoBase> baseinfos = new Dictionary<System.Type, PXCache.CacheStaticInfoBase>();
    List<System.Type> types = lot._Types;
    PXCache<TNode>.enumBaseTypes(typenode, (IEnumerable<System.Type>) types, baseinfos, ignoredResult);
    PXCache<TNode>.CacheStaticInfo cacheStaticInfo = new PXCache<TNode>.CacheStaticInfo();
    bool flag = !ignoredResult;
    Dictionary<string, List<PropertyInfo>> nameToFieldPropertyInfo;
    int origLength;
    PXCache.DACFieldDescriptor[] properties = PXCache._GetProperties(typenode, types, out nameToFieldPropertyInfo, out origLength);
    if (typenode.IsDefined(typeof (PXDBInterceptorAttribute), true))
    {
      object[] customAttributes = typenode.GetCustomAttributes(typeof (PXDBInterceptorAttribute), true);
      cacheStaticInfo._TypeInterceptorAttribute = customAttributes[0];
    }
    for (System.Type element = typenode; element != typeof (object); element = element.BaseType)
    {
      if (System.Attribute.IsDefined((MemberInfo) element, typeof (PXBreakInheritanceAttribute), false))
      {
        cacheStaticInfo._BreakInheritanceType = element;
        break;
      }
    }
    if (typenode.IsDefined(typeof (PXClassAttribute), true))
      cacheStaticInfo._ClassAttributes = typenode.GetCustomAttributes(typeof (PXClassAttribute), true);
    PXCache.CollectForeignKeys(typenode, (IEnumerable<System.Type>) types);
    if (types.Count > 0)
    {
      if (flag)
        cacheStaticInfo._ExtensionTypes = types.ToArray();
      for (int index1 = 0; index1 < types.Count; ++index1)
      {
        List<System.Type> typeList = types;
        int index2 = index1;
        System.Type extensionWrapper = PXCache<TNode>.CreateExtensionWrapper(typenode, types[index1], types);
        if ((object) extensionWrapper == null)
          extensionWrapper = types[index1];
        typeList[index2] = extensionWrapper;
      }
      if (flag)
      {
        DynamicMethod dynamicMethod;
        if (PXGraph.IsRestricted)
          dynamicMethod = new DynamicMethod("_CreateExtensions", typeof (PXCacheExtension[]), new System.Type[1]
          {
            typeof (TNode)
          });
        else
          dynamicMethod = new DynamicMethod("_CreateExtensions", typeof (PXCacheExtension[]), new System.Type[1]
          {
            typeof (TNode)
          }, typeof (PXCache<TNode>));
        DynamicMethod dm_ext = dynamicMethod;
        PXCache<TNode>.emitCreateExtensionsCode(dm_ext, types);
        cacheStaticInfo._CreateExtensions = (PXCache<TNode>.CreateExtensionsDelegate) dm_ext.CreateDelegate(typeof (PXCache<TNode>.CreateExtensionsDelegate));
      }
    }
    else if (flag)
      cacheStaticInfo._ExtensionTypes = new System.Type[0];
    DynamicMethod dynamicMethod1 = (DynamicMethod) null;
    DynamicMethod dynamicMethod2 = (DynamicMethod) null;
    ILGenerator il_set = (ILGenerator) null;
    ILGenerator il_get = (ILGenerator) null;
    Label? nullable1 = new Label?();
    Label? nullable2 = new Label?();
    int length = properties.Length;
    Label[] labels1 = new Label[length];
    Label[] labels2 = new Label[length];
    if (flag)
    {
      System.Type[] parameterTypes1 = new System.Type[4]
      {
        typenode,
        typeof (int),
        typeof (object),
        typeof (PXCacheExtension[])
      };
      System.Type[] parameterTypes2 = new System.Type[3]
      {
        typenode,
        typeof (int),
        typeof (PXCacheExtension[])
      };
      if (!PXGraph.IsRestricted)
      {
        dynamicMethod1 = new DynamicMethod("_SetValueByOrdinal", (System.Type) null, parameterTypes1, typeof (PXCache<TNode>));
        dynamicMethod2 = new DynamicMethod("_GetValueByOrdinal", typeof (object), parameterTypes2, typeof (PXCache<TNode>));
      }
      else
      {
        dynamicMethod1 = new DynamicMethod("_SetValueByOrdinal", (System.Type) null, parameterTypes1);
        dynamicMethod2 = new DynamicMethod("_GetValueByOrdinal", typeof (object), parameterTypes2);
      }
      il_set = dynamicMethod1.GetILGenerator();
      il_get = dynamicMethod2.GetILGenerator();
      for (int index = 0; index < length; ++index)
      {
        labels1[index] = il_set.DefineLabel();
        labels2[index] = il_get.DefineLabel();
      }
      nullable1 = new Label?(il_get.DefineLabel());
      nullable2 = new Label?(il_set.DefineLabel());
      il_set.Emit(OpCodes.Ldarg_1);
      il_set.Emit(OpCodes.Switch, labels1);
      il_set.Emit(OpCodes.Br, nullable2.Value);
      il_get.Emit(OpCodes.Ldarg_1);
      il_get.Emit(OpCodes.Switch, labels2);
      il_get.Emit(OpCodes.Ldnull);
      il_get.Emit(OpCodes.Br, nullable1.Value);
    }
    List<KeyValuePair<string, int>> keyValuePairList = new List<KeyValuePair<string, int>>();
    Dictionary<string, PropertyInfo> cstInjectedProps = (Dictionary<string, PropertyInfo>) null;
    foreach (System.Type t in typenode.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)))
    {
      if (t != typeof (object))
      {
        System.Type type = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(t), false);
        if (type != (System.Type) null)
        {
          if (cstInjectedProps == null)
            cstInjectedProps = new Dictionary<string, PropertyInfo>();
          foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (_ => _.Name.StartsWith("Append_") || _.Name.StartsWith("Replace_"))))
            cstInjectedProps[propertyInfo.Name] = propertyInfo;
        }
      }
    }
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    for (int index3 = 0; index3 < properties.Length; ++index3)
    {
      bool isExtensionField = index3 >= origLength;
      PXCache.DACFieldDescriptor prop = properties[index3];
      if ((!isExtensionField || !(prop.Property != (PropertyInfo) null) ? 0 : (System.Attribute.IsDefined((MemberInfo) prop.Property.DeclaringType, typeof (PXKeyValueStorageAttribute)) ? 1 : 0)) != 0)
      {
        stringSet.Add(prop.Property.Name);
        if (cacheStaticInfo._KeyValueStoredOrdinals == null)
        {
          cacheStaticInfo._KeyValueStoredOrdinals = new HashSet<int>();
          cacheStaticInfo._KeyValueStoredNames = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        }
      }
      PXEventSubscriberAttribute[] subcriberAttributes = PXCache<TNode>.getEventSubcriberAttributes(typenode, nameToFieldPropertyInfo, prop, cstInjectedProps, baseinfos);
      PXAttributeFamilyAttribute.CheckAttributes(prop, subcriberAttributes);
      System.Type itemType = (System.Type) null;
      System.Type extensionType = (System.Type) null;
      if (typeof (PXCacheExtension).IsAssignableFrom(prop.DeclaringType) && nameToFieldPropertyInfo != null && nameToFieldPropertyInfo.ContainsKey(prop.Name))
      {
        extensionType = prop.DeclaringType;
        if (System.Attribute.IsDefined((MemberInfo) prop.DeclaringType, typeof (PXDBInterceptorAttribute)))
        {
          itemType = prop.DeclaringType;
        }
        else
        {
          foreach (PropertyInfo propertyInfo in nameToFieldPropertyInfo[prop.Name])
          {
            if (System.Attribute.IsDefined((MemberInfo) propertyInfo.DeclaringType, typeof (PXDBInterceptorAttribute)))
              itemType = propertyInfo.DeclaringType;
          }
        }
        if (itemType == (System.Type) null)
        {
          if (prop.DeclaringType.BaseType.IsGenericType)
          {
            System.Type[] genericArguments = prop.DeclaringType.BaseType.GetGenericArguments();
            itemType = genericArguments[genericArguments.Length - 1];
          }
          else
            itemType = typenode;
        }
        else if (flag)
        {
          if (cacheStaticInfo._ExtensionTables == null)
            cacheStaticInfo._ExtensionTables = new List<System.Type>();
          if (!cacheStaticInfo._ExtensionTables.Contains(itemType))
            cacheStaticInfo._ExtensionTables.Add(itemType);
        }
      }
      else
        itemType = typenode;
      string name = prop.Name;
      for (int index4 = 0; index4 < subcriberAttributes.Length; ++index4)
      {
        PXEventSubscriberAttribute attr = subcriberAttributes[index4];
        attr.InjectAttributeDependencies((PXCache) null);
        if (extensionType == (System.Type) null)
          attr.prepare(name, index3, itemType);
        else
          attr.prepare(name, index3, itemType, extensionType);
        if (flag)
        {
          cacheStaticInfo._FieldAttributes.Add(attr);
          if (subcriberAttributes[index4] is IPXInterfaceField pxInterfaceField && pxInterfaceField.TabOrder != index3)
            keyValuePairList.Add(new KeyValuePair<string, int>(name, pxInterfaceField.TabOrder));
          if (attr is PXDBTimestampAttribute)
            cacheStaticInfo._TimestampOrdinal = new int?(index3);
        }
      }
      if (flag)
      {
        if (!typeof (IBqlTable).IsAssignableFrom(prop.DeclaringType) && CustomizedTypeManager.IsCustomizedType(prop.DeclaringType) | isExtensionField && (keyValuePairList.Count == 0 || keyValuePairList[keyValuePairList.Count - 1].Key != name))
          keyValuePairList.Add(new KeyValuePair<string, int>(name, int.MaxValue));
        il_get.MarkLabel(labels2[index3]);
        PXCache<TNode>.emitReadProperty(prop, isExtensionField, il_get, types);
        il_get.Emit(OpCodes.Br, nullable1.Value);
        il_set.MarkLabel(labels1[index3]);
        PXCache<TNode>.emitWriteProperty(prop, isExtensionField, il_set, types, nameToFieldPropertyInfo);
        il_set.Emit(OpCodes.Br, nullable2.Value);
        System.Type type;
        if (prop.Property != (PropertyInfo) null)
        {
          type = !prop.Property.PropertyType.IsGenericType || !(prop.Property.PropertyType.GetGenericTypeDefinition() == typeof (Nullable<>)) ? prop.Property.PropertyType : prop.Property.PropertyType.GetGenericArguments()[0];
        }
        else
        {
          PXCache.CacheStaticInfoBase cacheStaticInfoBase;
          if (prop.TargetBqlField != (System.Type) null && baseinfos.TryGetValue(prop.TargetBqlField.DeclaringType, out cacheStaticInfoBase))
          {
            string str = (string) null;
            type = cacheStaticInfoBase._FieldTypes[cacheStaticInfoBase._FieldsMap[prop.TargetBqlField.Name]];
            cacheStaticInfo._CachesByOrdinal[index3] = new PXCache.OrdinalFieldInfo()
            {
              DeclaringType = PXCache<TNode>.GetRealDACType(prop.TargetBqlField.DeclaringType),
              TargetFieldName = prop.TargetBqlField.Name,
              ViewName = str,
              BaseFieldName = prop.BqlField.BaseType.Name
            };
          }
          else
            type = typeof (object);
        }
        cacheStaticInfo._ClassFields.Add(name);
        cacheStaticInfo._FieldTypes.Add(type);
        cacheStaticInfo._FieldsMap[name] = index3;
      }
    }
    if (!flag)
      return (PXCache.CacheStaticInfoBase) cacheStaticInfo;
    if (cacheStaticInfo._ExtensionTables != null && cacheStaticInfo._ExtensionTables.Count > 0)
    {
      System.Type extensionTable = cacheStaticInfo._ExtensionTables[cacheStaticInfo._ExtensionTables.Count - 1];
      if (extensionTable.IsDefined(typeof (PXDBInterceptorAttribute), true))
      {
        object[] customAttributes = extensionTable.GetCustomAttributes(typeof (PXDBInterceptorAttribute), true);
        cacheStaticInfo._ExtensionInterceptorAttribute = customAttributes[0];
      }
    }
    il_get.MarkLabel(nullable1.Value);
    il_get.Emit(OpCodes.Ret);
    il_set.MarkLabel(nullable2.Value);
    il_set.Emit(OpCodes.Ret);
    cacheStaticInfo._SetValueByOrdinal = (PXCache<TNode>.SetValueByOrdinalDelegate) dynamicMethod1.CreateDelegate(typeof (PXCache<TNode>.SetValueByOrdinalDelegate));
    cacheStaticInfo._GetValueByOrdinal = (PXCache<TNode>.GetValueByOrdinalDelegate) dynamicMethod2.CreateDelegate(typeof (PXCache<TNode>.GetValueByOrdinalDelegate));
    foreach (KeyValuePair<string, int> keyValuePair in keyValuePairList)
    {
      int index5 = keyValuePair.Value < 0 ? 0 : (keyValuePair.Value >= length ? length - 1 : keyValuePair.Value);
      int index6 = cacheStaticInfo._ClassFields.IndexOf(keyValuePair.Key);
      System.Type fieldType = cacheStaticInfo._FieldTypes[index6];
      cacheStaticInfo._ClassFields.RemoveAt(index6);
      cacheStaticInfo._FieldTypes.RemoveAt(index6);
      cacheStaticInfo._ClassFields.Insert(index5, keyValuePair.Key);
      cacheStaticInfo._FieldTypes.Insert(index5, fieldType);
      int index7 = 0;
      for (int index8 = 0; index8 < cacheStaticInfo._FieldAttributes.Count; ++index8)
      {
        if (cacheStaticInfo._ClassFields.IndexOf(cacheStaticInfo._FieldAttributes[index8].FieldName) < index5)
          index7 = index8 + 1;
      }
      PXEventSubscriberAttribute subscriberAttribute = (PXEventSubscriberAttribute) null;
      for (int index9 = 0; index9 < cacheStaticInfo._FieldAttributes.Count; ++index9)
      {
        if (cacheStaticInfo._FieldAttributes[index9].FieldName == keyValuePair.Key)
        {
          if (subscriberAttribute == null)
            subscriberAttribute = cacheStaticInfo._FieldAttributes[index9];
          else if (subscriberAttribute == cacheStaticInfo._FieldAttributes[index9])
            break;
          cacheStaticInfo._FieldAttributes.Insert(index7, cacheStaticInfo._FieldAttributes[index9]);
          cacheStaticInfo._FieldAttributes.RemoveAt(index9 < index7 ? index9 : index9 + 1);
          if (index9 >= index7)
            ++index7;
          else
            --index9;
        }
      }
    }
    cacheStaticInfo._FieldAttributesFirst = new int[cacheStaticInfo._ClassFields.Count];
    cacheStaticInfo._FieldAttributesLast = new int[cacheStaticInfo._ClassFields.Count];
    for (int index = 0; index < cacheStaticInfo._FieldAttributesLast.Length; ++index)
      cacheStaticInfo._FieldAttributesLast[index] = -1;
    for (int index = 0; index < cacheStaticInfo._FieldAttributes.Count; ++index)
    {
      if (index == 0 || cacheStaticInfo._FieldAttributes[index - 1].FieldOrdinal != cacheStaticInfo._FieldAttributes[index].FieldOrdinal)
        cacheStaticInfo._FieldAttributesFirst[cacheStaticInfo._FieldAttributes[index].FieldOrdinal] = cacheStaticInfo._FieldAttributesLast[cacheStaticInfo._FieldAttributes[index].FieldOrdinal] = index;
      else
        cacheStaticInfo._FieldAttributesLast[cacheStaticInfo._FieldAttributes[index].FieldOrdinal] = index;
    }
    cacheStaticInfo._EventPositions = new PXCache.EventPosition[cacheStaticInfo._ClassFields.Count];
    for (int index = 0; index < cacheStaticInfo._ClassFields.Count; ++index)
      cacheStaticInfo._EventPositions[index] = new PXCache.EventPosition();
    for (int index10 = 0; index10 < cacheStaticInfo._FieldAttributes.Count; ++index10)
    {
      PXEventSubscriberAttribute fieldAttribute = cacheStaticInfo._FieldAttributes[index10];
      PXCache.EventPosition eventPosition = cacheStaticInfo._EventPositions[cacheStaticInfo._FieldsMap[fieldAttribute.FieldName]];
      if (eventPosition.RowSelectingFirst == -1)
        eventPosition.RowSelectingFirst = cacheStaticInfo._EventsRowMap.RowSelecting.Count;
      fieldAttribute.GetSubscriber<IPXRowSelectingSubscriber>(cacheStaticInfo._EventsRowMap.RowSelecting);
      if ((eventPosition.RowSelectingLength = cacheStaticInfo._EventsRowMap.RowSelecting.Count - eventPosition.RowSelectingFirst) > 0 && stringSet.Contains(fieldAttribute.FieldName))
      {
        if (!cacheStaticInfo._FirstKeyValueStored.HasValue)
          cacheStaticInfo._FirstKeyValueStored = new KeyValuePair<string, int>?(new KeyValuePair<string, int>(fieldAttribute.FieldName, eventPosition.RowSelectingFirst));
        if (!cacheStaticInfo._KeyValueStoredNames.ContainsKey(fieldAttribute.FieldName))
          cacheStaticInfo._KeyValueStoredNames.Add(fieldAttribute.FieldName, cacheStaticInfo._KeyValueStoredNames.Count);
        for (int index11 = 0; index11 < eventPosition.RowSelectingLength; ++index11)
          cacheStaticInfo._KeyValueStoredOrdinals.Add(index11 + eventPosition.RowSelectingFirst);
      }
      if (fieldAttribute is PXDBLocalizableStringAttribute)
      {
        if (cacheStaticInfo._DBLocalizableNames == null)
          cacheStaticInfo._DBLocalizableNames = new Dictionary<string, int>();
        if (!cacheStaticInfo._DBLocalizableNames.ContainsKey(fieldAttribute.FieldName))
          cacheStaticInfo._DBLocalizableNames.Add(fieldAttribute.FieldName, cacheStaticInfo._DBLocalizableNames.Count);
      }
      if (eventPosition.RowSelectedFirst == -1)
        eventPosition.RowSelectedFirst = cacheStaticInfo._EventsRowMap.RowSelected.Count;
      fieldAttribute.GetSubscriber<IPXRowSelectedSubscriber>(cacheStaticInfo._EventsRowMap.RowSelected);
      eventPosition.RowSelectedLength = cacheStaticInfo._EventsRowMap.RowSelected.Count - eventPosition.RowSelectedFirst;
      if (eventPosition.RowInsertingFirst == -1)
        eventPosition.RowInsertingFirst = cacheStaticInfo._EventsRowMap.RowInserting.Count;
      fieldAttribute.GetSubscriber<IPXRowInsertingSubscriber>(cacheStaticInfo._EventsRowMap.RowInserting);
      eventPosition.RowInsertingLength = cacheStaticInfo._EventsRowMap.RowInserting.Count - eventPosition.RowInsertingFirst;
      if (eventPosition.RowInsertedFirst == -1)
        eventPosition.RowInsertedFirst = cacheStaticInfo._EventsRowMap.RowInserted.Count;
      fieldAttribute.GetSubscriber<IPXRowInsertedSubscriber>(cacheStaticInfo._EventsRowMap.RowInserted);
      eventPosition.RowInsertedLength = cacheStaticInfo._EventsRowMap.RowInserted.Count - eventPosition.RowInsertedFirst;
      if (eventPosition.RowUpdatingFirst == -1)
        eventPosition.RowUpdatingFirst = cacheStaticInfo._EventsRowMap.RowUpdating.Count;
      fieldAttribute.GetSubscriber<IPXRowUpdatingSubscriber>(cacheStaticInfo._EventsRowMap.RowUpdating);
      eventPosition.RowUpdatingLength = cacheStaticInfo._EventsRowMap.RowUpdating.Count - eventPosition.RowUpdatingFirst;
      if (eventPosition.RowUpdatedFirst == -1)
        eventPosition.RowUpdatedFirst = cacheStaticInfo._EventsRowMap.RowUpdated.Count;
      fieldAttribute.GetSubscriber<IPXRowUpdatedSubscriber>(cacheStaticInfo._EventsRowMap.RowUpdated);
      eventPosition.RowUpdatedLength = cacheStaticInfo._EventsRowMap.RowUpdated.Count - eventPosition.RowUpdatedFirst;
      if (eventPosition.RowDeletingFirst == -1)
        eventPosition.RowDeletingFirst = cacheStaticInfo._EventsRowMap.RowDeleting.Count;
      fieldAttribute.GetSubscriber<IPXRowDeletingSubscriber>(cacheStaticInfo._EventsRowMap.RowDeleting);
      eventPosition.RowDeletingLength = cacheStaticInfo._EventsRowMap.RowDeleting.Count - eventPosition.RowDeletingFirst;
      if (eventPosition.RowDeletedFirst == -1)
        eventPosition.RowDeletedFirst = cacheStaticInfo._EventsRowMap.RowDeleted.Count;
      fieldAttribute.GetSubscriber<IPXRowDeletedSubscriber>(cacheStaticInfo._EventsRowMap.RowDeleted);
      eventPosition.RowDeletedLength = cacheStaticInfo._EventsRowMap.RowDeleted.Count - eventPosition.RowDeletedFirst;
      if (eventPosition.RowPersistingFirst == -1)
        eventPosition.RowPersistingFirst = cacheStaticInfo._EventsRowMap.RowPersisting.Count;
      fieldAttribute.GetSubscriber<IPXRowPersistingSubscriber>(cacheStaticInfo._EventsRowMap.RowPersisting);
      eventPosition.RowPersistingLength = cacheStaticInfo._EventsRowMap.RowPersisting.Count - eventPosition.RowPersistingFirst;
      if (eventPosition.RowPersistedFirst == -1)
        eventPosition.RowPersistedFirst = cacheStaticInfo._EventsRowMap.RowPersisted.Count;
      fieldAttribute.GetSubscriber<IPXRowPersistedSubscriber>(cacheStaticInfo._EventsRowMap.RowPersisted);
      eventPosition.RowPersistedLength = cacheStaticInfo._EventsRowMap.RowPersisted.Count - eventPosition.RowPersistedFirst;
      string lower = fieldAttribute.FieldName.ToLower();
      PXCache.EventsFieldMap eventsFieldMap;
      if (!cacheStaticInfo._EventsFieldMap.TryGetValue(lower, out eventsFieldMap))
        cacheStaticInfo._EventsFieldMap[lower] = eventsFieldMap = new PXCache.EventsFieldMap();
      fieldAttribute.GetSubscriber<IPXCommandPreparingSubscriber>(eventsFieldMap.CommandPreparing);
      fieldAttribute.GetSubscriber<IPXFieldDefaultingSubscriber>(eventsFieldMap.FieldDefaulting);
      fieldAttribute.GetSubscriber<IPXFieldUpdatingSubscriber>(eventsFieldMap.FieldUpdating);
      fieldAttribute.GetSubscriber<IPXFieldVerifyingSubscriber>(eventsFieldMap.FieldVerifying);
      fieldAttribute.GetSubscriber<IPXFieldUpdatedSubscriber>(eventsFieldMap.FieldUpdated);
      fieldAttribute.GetSubscriber<IPXFieldSelectingSubscriber>(eventsFieldMap.FieldSelecting);
      fieldAttribute.GetSubscriber<IPXExceptionHandlingSubscriber>(eventsFieldMap.ExceptionHandling);
    }
    PXCache<TNode>._BqlTable = PXCache.GetBqlTable(typenode);
    for (System.Type root = typenode; root != typeof (object); root = root.BaseType)
    {
      foreach (System.Type type in PXCache.getAllNestedTypes(root).list.Select<PXCache.fieldContainer, System.Type>((Func<PXCache.fieldContainer, System.Type>) (_ => _.Field)))
      {
        int key;
        if (typeof (IBqlField).IsAssignableFrom(type) && cacheStaticInfo._FieldsMap.TryGetValue(type.Name, out key))
        {
          cacheStaticInfo._BqlFieldsMap[type] = key;
          if (!cacheStaticInfo._InverseBqlFieldsMap.ContainsKey(key))
            cacheStaticInfo._InverseBqlFieldsMap[key] = type;
        }
      }
    }
    List<System.Type> list = types.Where<System.Type>((Func<System.Type, bool>) (_ => !typeof (PXMappedCacheExtension).IsAssignableFrom(_) && _.BaseType.IsGenericType)).ToList<System.Type>();
    Dictionary<System.Type, System.Type> dictionary = list.ToDictionary<System.Type, System.Type, System.Type>((Func<System.Type, System.Type>) (_ => _), (Func<System.Type, System.Type>) (_ => ((IEnumerable<System.Type>) _.BaseType.GetGenericArguments()).Last<System.Type>()));
    for (int index12 = 0; index12 < list.Count; ++index12)
    {
      for (int index13 = index12 + 1; index13 < list.Count; ++index13)
      {
        if (dictionary[list[index13]].IsSubclassOf(dictionary[list[index12]]))
        {
          System.Type type = list[index12];
          list[index12] = list[index13];
          list[index13] = type;
        }
      }
    }
    for (int index = 0; index < list.Count; ++index)
    {
      foreach (System.Type nestedType in list[index].GetNestedTypes())
      {
        int key;
        if (typeof (IBqlField).IsAssignableFrom(nestedType) && cacheStaticInfo._FieldsMap.TryGetValue(nestedType.Name, out key))
        {
          cacheStaticInfo._BqlFieldsMap[nestedType] = key;
          if (!cacheStaticInfo._InverseBqlFieldsMap.ContainsKey(key))
            cacheStaticInfo._InverseBqlFieldsMap[key] = nestedType;
        }
      }
    }
    if (!PXGraph.IsRestricted)
    {
      DynamicMethod dynamicMethod3 = new DynamicMethod("memberwiseClone", typeof (object), new System.Type[1]
      {
        typenode
      }, typeof (PXCache<TNode>), true);
      ILGenerator ilGenerator = dynamicMethod3.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Callvirt, typenode.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic));
      ilGenerator.Emit(OpCodes.Ret);
      PXCache<TNode>.memberwiseCloneDelegate memberwiseCloneDynamic = (PXCache<TNode>.memberwiseCloneDelegate) dynamicMethod3.CreateDelegate(typeof (PXCache<TNode>.memberwiseCloneDelegate));
      PXCache<TNode>.memberwiseClone = (PXCache<TNode>.memberwiseCloneDelegate) (item =>
      {
        IBqlTableSystemDataStorage systemDataStorage = (IBqlTableSystemDataStorage) memberwiseCloneDynamic(item);
        *(PXBqlTableSystemData*) ref systemDataStorage.GetBqlTableSystemData() = new PXBqlTableSystemData();
        return (object) systemDataStorage;
      });
      DynamicMethod dm_ext = new DynamicMethod("memberwiseCloneExtensions", typeof (PXCacheExtension[]), new System.Type[2]
      {
        typenode,
        typeof (PXCacheExtension[])
      }, typeof (PXCache<TNode>), true);
      PXCache<TNode>.emitCreateCloneExtensionsCode(dm_ext, types);
      cacheStaticInfo._CloneExtensions = (PXCache<TNode>.memberwiseCloneExtensionsDelegate) dm_ext.CreateDelegate(typeof (PXCache<TNode>.memberwiseCloneExtensionsDelegate));
    }
    cacheStaticInfo._DelegatesLock = new ReaderWriterLock();
    cacheStaticInfo._DelegatesEquals = new Dictionary<string, PXCache<TNode>.EqualsDelegate>(1, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    cacheStaticInfo._DelegatesGetHashCode = new Dictionary<string, PXCache<TNode>.GetHashCodeDelegate>(1, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    for (int index = 0; index < cacheStaticInfo._ClassFields.Count; ++index)
      cacheStaticInfo._ReverseMap[cacheStaticInfo._ClassFields[index]] = index;
    return (PXCache.CacheStaticInfoBase) cacheStaticInfo;
  }

  private static System.Type CreateExtensionWrapper(
    System.Type ttable,
    System.Type textension,
    List<System.Type> extensions)
  {
    if (textension.IsSealed || !textension.IsDefined(typeof (PXProxyPropertiesAttribute)))
      return (System.Type) null;
    lock (PXExtensionManager.Wrappers)
    {
      PXExtensionManager.ListOfTypes key = new PXExtensionManager.ListOfTypes(extensions);
      Dictionary<PXExtensionManager.ListOfTypes, System.Type> dictionary;
      if (!PXExtensionManager.Wrappers.TryGetValue(textension, out dictionary))
        PXExtensionManager.Wrappers[textension] = dictionary = new Dictionary<PXExtensionManager.ListOfTypes, System.Type>();
      System.Type extensionWrapper;
      if (dictionary.TryGetValue(key, out extensionWrapper))
        return extensionWrapper;
      System.Type wrapperInternal = PXCache<TNode>.CreateWrapperInternal(ttable, textension);
      dictionary.Add(key, wrapperInternal);
      return wrapperInternal;
    }
  }

  private static System.Type CreateWrapperInternal(System.Type ttable, System.Type textension)
  {
    Dictionary<string, string> fieldsMapping;
    if ((fieldsMapping = PXCache._mapping.GetFieldsMapping(ttable, textension)) == null)
      return (System.Type) null;
    AssemblyName name = new AssemblyName()
    {
      Name = $"{ttable.Name}_{textension.Name}_Container"
    };
    TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(name.Name).DefineType("Wrapper." + CustomizedTypeManager.GetCustomizedTypeFullName(textension), TypeAttributes.Public | TypeAttributes.Sealed, textension);
    foreach (ConstructorInfo constructor in textension.GetConstructors())
    {
      System.Type[] array = ((IEnumerable<ParameterInfo>) constructor.GetParameters()).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (_ => _.ParameterType)).ToArray<System.Type>();
      ILGenerator ilGenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, array).GetILGenerator();
      for (int index = 0; index <= array.Length; ++index)
        ilGenerator.Emit(OpCodes.Ldarg, index);
      ilGenerator.Emit(OpCodes.Call, constructor);
      ilGenerator.Emit(OpCodes.Ret);
    }
    PropertyInfo property1 = textension.GetProperty("Base", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    foreach (KeyValuePair<string, string> keyValuePair in fieldsMapping)
    {
      PropertyInfo property2 = textension.GetProperty(keyValuePair.Key, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      PropertyInfo property3 = ttable.GetProperty(keyValuePair.Value, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      if (property2 != (PropertyInfo) null && property3 != (PropertyInfo) null)
      {
        PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(keyValuePair.Key, PropertyAttributes.None, property2.PropertyType, System.Type.EmptyTypes);
        MethodBuilder mdBuilder1 = typeBuilder.DefineMethod("get_" + property2.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, property2.PropertyType, System.Type.EmptyTypes);
        MethodBuilder mdBuilder2 = typeBuilder.DefineMethod("set_" + property2.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, (System.Type) null, new System.Type[1]
        {
          property2.PropertyType
        });
        ILGenerator ilGenerator1 = mdBuilder1.GetILGenerator();
        ILGenerator ilGenerator2 = mdBuilder2.GetILGenerator();
        ilGenerator1.Emit(OpCodes.Ldarg_0);
        ilGenerator1.Emit(OpCodes.Call, property1.GetGetMethod());
        ilGenerator1.Emit(OpCodes.Castclass, ttable);
        ilGenerator1.Emit(OpCodes.Callvirt, property3.GetGetMethod());
        ilGenerator1.Emit(OpCodes.Ret);
        ilGenerator2.Emit(OpCodes.Ldarg_0);
        ilGenerator2.Emit(OpCodes.Call, property1.GetGetMethod());
        ilGenerator2.Emit(OpCodes.Castclass, ttable);
        ilGenerator2.Emit(OpCodes.Ldarg_1);
        ilGenerator2.Emit(OpCodes.Callvirt, property3.GetSetMethod());
        ilGenerator2.Emit(OpCodes.Ret);
        propertyBuilder.SetGetMethod(mdBuilder1);
        propertyBuilder.SetSetMethod(mdBuilder2);
      }
    }
    return typeBuilder.CreateType();
  }

  private static void emitWriteProperty(
    PXCache.DACFieldDescriptor prop,
    bool isExtensionField,
    ILGenerator il_set,
    List<System.Type> extensions,
    Dictionary<string, List<PropertyInfo>> extproperties)
  {
    System.Type propertyType = prop.Property?.PropertyType;
    if (!isExtensionField && prop.Property != (PropertyInfo) null && prop.Property.CanWrite)
    {
      il_set.Emit(OpCodes.Ldarg_0);
      il_set.Emit(OpCodes.Ldarg_2);
      il_set.Emit(propertyType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, propertyType);
      il_set.Emit(OpCodes.Callvirt, prop.Property.GetSetMethod());
    }
    if (extproperties == null || !extproperties.ContainsKey(prop.Name))
      return;
    foreach (PropertyInfo propertyInfo in extproperties[prop.Name])
    {
      PropertyInfo extpi = propertyInfo;
      if (extpi.CanWrite && extpi.PropertyType == propertyType)
      {
        il_set.Emit(OpCodes.Ldarg_3);
        il_set.Emit(OpCodes.Ldc_I4, extensions.FindIndex((Predicate<System.Type>) (e => extpi.DeclaringType.IsAssignableFrom(e))));
        il_set.Emit(OpCodes.Ldelem_Ref);
        il_set.Emit(OpCodes.Castclass, extpi.DeclaringType);
        il_set.Emit(OpCodes.Ldarg_2);
        il_set.Emit(propertyType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, propertyType);
        il_set.Emit(OpCodes.Callvirt, extpi.GetSetMethod());
      }
    }
  }

  private static void emitReadProperty(
    PXCache.DACFieldDescriptor prop,
    bool isExtensionField,
    ILGenerator il_get,
    List<System.Type> extensions)
  {
    if (prop.Property != (PropertyInfo) null && prop.Property.CanRead)
    {
      System.Type propertyType = prop.Property.PropertyType;
      if (!isExtensionField)
      {
        il_get.Emit(OpCodes.Ldarg_0);
      }
      else
      {
        il_get.Emit(OpCodes.Ldarg_2);
        il_get.Emit(OpCodes.Ldc_I4, extensions.FindIndex((Predicate<System.Type>) (e => prop.DeclaringType.IsAssignableFrom(e))));
        il_get.Emit(OpCodes.Ldelem_Ref);
        il_get.Emit(OpCodes.Castclass, prop.DeclaringType);
      }
      il_get.Emit(OpCodes.Callvirt, prop.Property.GetGetMethod());
      if (!propertyType.IsValueType)
        return;
      il_get.Emit(OpCodes.Box, propertyType);
    }
    else
      il_get.Emit(OpCodes.Ldnull);
  }

  private static PXEventSubscriberAttribute[] getEventSubcriberAttributes(
    System.Type typenode,
    Dictionary<string, List<PropertyInfo>> extproperties,
    PXCache.DACFieldDescriptor prop,
    Dictionary<string, PropertyInfo> cstInjectedProps,
    Dictionary<System.Type, PXCache.CacheStaticInfoBase> baseinfos)
  {
    List<PropertyInfo> memberList = new List<PropertyInfo>();
    if (extproperties != null && extproperties.ContainsKey(prop.Name) && !System.Attribute.IsDefined((MemberInfo) extproperties[prop.Name][0].DeclaringType, typeof (PXDBInterceptorAttribute)))
      memberList = extproperties[prop.Name].ToList<PropertyInfo>();
    System.Type[] array = typenode.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)).Skip<System.Type>(1).TakeWhile<System.Type>((Func<System.Type, bool>) (_ => _ != typeof (object))).ToArray<System.Type>();
    PXEventSubscriberAttribute[] first;
    if (memberList.Count == 0 && array.Length == 0)
      first = !(prop.Property != (PropertyInfo) null) ? (!(prop.BqlField.BaseType == typeof (object)) ? (!baseinfos.TryGetValue(prop.BqlField.BaseType.DeclaringType, out PXCache.CacheStaticInfoBase _) ? new PXEventSubscriberAttribute[0] : prop.BqlField.GetCustomAttributes<PXEventSubscriberAttribute>().ToArray<PXEventSubscriberAttribute>()) : prop.BqlField.GetCustomAttributes<PXEventSubscriberAttribute>().ToArray<PXEventSubscriberAttribute>()) : (PXEventSubscriberAttribute[]) prop.Property.GetCustomAttributesEx(typeof (PXEventSubscriberAttribute), false);
    else if (prop.Property != (PropertyInfo) null)
    {
      memberList.Add(prop.Property);
      foreach (System.Type type in array)
      {
        PropertyInfo property = type.GetProperty(prop.Name, BindingFlags.Instance | BindingFlags.Public);
        if (property != (PropertyInfo) null)
          memberList.Add(property);
      }
      memberList.Reverse();
      first = PXExtensionManager.MergeExtensionAttributes((IEnumerable<MemberInfo>) memberList);
    }
    else
      first = new PXEventSubscriberAttribute[0];
    if (prop.Property != (PropertyInfo) null && cstInjectedProps != null && cstInjectedProps.Count > 0)
    {
      System.Type declaringType = prop.DeclaringType;
      foreach (System.Type type in typenode.CreateList<System.Type>((Func<System.Type, System.Type>) (_ => _.BaseType)).TakeWhile<System.Type>(new Func<System.Type, bool>(declaringType.IsAssignableFrom)).Reverse<System.Type>().ToArray<System.Type>())
      {
        string str = $"{type.FullName.Replace('.', '_').Replace('+', '_')}_{prop.Name}";
        PropertyInfo t;
        if (cstInjectedProps.TryGetValue("Replace_" + str, out t))
          first = (PXEventSubscriberAttribute[]) t.GetCustomAttributesEx(typeof (PXEventSubscriberAttribute), false);
        if (cstInjectedProps.TryGetValue("Append_" + str, out t))
        {
          PXEventSubscriberAttribute[] customAttributesEx = (PXEventSubscriberAttribute[]) t.GetCustomAttributesEx(typeof (PXEventSubscriberAttribute), false);
          if (((IEnumerable<PXEventSubscriberAttribute>) customAttributesEx).Any<PXEventSubscriberAttribute>())
            first = ((IEnumerable<PXEventSubscriberAttribute>) first).Concat<PXEventSubscriberAttribute>((IEnumerable<PXEventSubscriberAttribute>) customAttributesEx).ToArray<PXEventSubscriberAttribute>();
        }
      }
    }
    return first;
  }

  private static void emitCreateExtensionsCode(DynamicMethod dm_ext, List<System.Type> extensions)
  {
    ILGenerator ilGenerator = dm_ext.GetILGenerator();
    LocalBuilder local1 = ilGenerator.DeclareLocal(typeof (PXCacheExtension[]));
    ilGenerator.Emit(OpCodes.Ldc_I4, extensions.Count);
    ilGenerator.Emit(OpCodes.Newarr, typeof (PXCacheExtension));
    ilGenerator.Emit(OpCodes.Stloc, local1);
    LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (PXCacheExtension));
    LocalBuilder local3 = ilGenerator.DeclareLocal(typeof (WeakReference));
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Newobj, typeof (WeakReference).GetConstructor(new System.Type[1]
    {
      typeof (object)
    }));
    ilGenerator.Emit(OpCodes.Castclass, typeof (WeakReference));
    ilGenerator.Emit(OpCodes.Stloc, local3);
    for (int index1 = 0; index1 < extensions.Count; ++index1)
    {
      System.Type extension = extensions[index1];
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4, index1);
      ilGenerator.Emit(OpCodes.Newobj, extension.GetConstructor(new System.Type[0]));
      ilGenerator.Emit(OpCodes.Dup);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Stelem_Ref);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Castclass, extension);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Stfld, extension.GetField("_Base", BindingFlags.Instance | BindingFlags.NonPublic));
      System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(extension);
      int num1;
      for (int index2 = 0; index2 < (num1 = typeNotCustomized.BaseType.GetGenericArguments().Length - 1); ++index2)
      {
        System.Type genericArgument = typeNotCustomized.BaseType.GetGenericArguments()[index2];
        int num2;
        if ((num2 = extensions.IndexOf(genericArgument)) == -1)
          throw new PXException("The dependent extension does not belong to the same cache.");
        ilGenerator.Emit(OpCodes.Ldloc, local2);
        ilGenerator.Emit(OpCodes.Castclass, extension);
        ilGenerator.Emit(OpCodes.Ldloc, local1);
        ilGenerator.Emit(OpCodes.Ldc_I4, num2);
        ilGenerator.Emit(OpCodes.Ldelem_Ref);
        ilGenerator.Emit(OpCodes.Castclass, genericArgument);
        ilGenerator.Emit(OpCodes.Stfld, extension.GetField("_Base" + (num1 - index2).ToString(), BindingFlags.Instance | BindingFlags.NonPublic));
      }
    }
    ilGenerator.Emit(OpCodes.Ldloc, local1);
    ilGenerator.Emit(OpCodes.Ret);
  }

  private static void emitCreateCloneExtensionsCode(DynamicMethod dm_ext, List<System.Type> extensions)
  {
    ILGenerator ilGenerator = dm_ext.GetILGenerator();
    LocalBuilder local1 = ilGenerator.DeclareLocal(typeof (PXCacheExtension[]));
    ilGenerator.Emit(OpCodes.Ldc_I4, extensions.Count);
    ilGenerator.Emit(OpCodes.Newarr, typeof (PXCacheExtension));
    ilGenerator.Emit(OpCodes.Stloc, local1);
    LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (PXCacheExtension));
    LocalBuilder local3 = ilGenerator.DeclareLocal(typeof (PXCacheExtension));
    LocalBuilder local4 = ilGenerator.DeclareLocal(typeof (WeakReference));
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Newobj, typeof (WeakReference).GetConstructor(new System.Type[1]
    {
      typeof (object)
    }));
    ilGenerator.Emit(OpCodes.Castclass, typeof (WeakReference));
    ilGenerator.Emit(OpCodes.Stloc, local4);
    for (int index1 = 0; index1 < extensions.Count; ++index1)
    {
      System.Type extension = extensions[index1];
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4, index1);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(OpCodes.Ldc_I4, index1);
      ilGenerator.Emit(OpCodes.Ldelem_Ref, local3);
      ilGenerator.Emit(OpCodes.Callvirt, extension.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic));
      ilGenerator.Emit(OpCodes.Dup);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Stelem_Ref);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Castclass, extension);
      ilGenerator.Emit(OpCodes.Ldloc, local4);
      ilGenerator.Emit(OpCodes.Stfld, extension.GetField("_Base", BindingFlags.Instance | BindingFlags.NonPublic));
      System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(extension);
      int num1;
      for (int index2 = 0; index2 < (num1 = typeNotCustomized.BaseType.GetGenericArguments().Length - 1); ++index2)
      {
        System.Type genericArgument = typeNotCustomized.BaseType.GetGenericArguments()[index2];
        int num2;
        if ((num2 = extensions.IndexOf(genericArgument)) == -1)
          throw new PXException("The dependent extension does not belong to the same cache.");
        ilGenerator.Emit(OpCodes.Ldloc, local2);
        ilGenerator.Emit(OpCodes.Castclass, extension);
        ilGenerator.Emit(OpCodes.Ldloc, local1);
        ilGenerator.Emit(OpCodes.Ldc_I4, num2);
        ilGenerator.Emit(OpCodes.Ldelem_Ref);
        ilGenerator.Emit(OpCodes.Castclass, genericArgument);
        ilGenerator.Emit(OpCodes.Stfld, extension.GetField("_Base" + (num1 - index2).ToString(), BindingFlags.Instance | BindingFlags.NonPublic));
      }
    }
    ilGenerator.Emit(OpCodes.Ldloc, local1);
    ilGenerator.Emit(OpCodes.Ret);
  }

  TNode PXCache<TNode>.IStronglyTypedRows.Current
  {
    [DebuggerStepThrough] get => (TNode) this.Current;
    [DebuggerStepThrough] set => this.Current = (object) value;
  }

  TNode PXCache<TNode>.IStronglyTypedRows.ActiveRow
  {
    [DebuggerStepThrough] get => (TNode) this.ActiveRow;
    [DebuggerStepThrough] set => this.ActiveRow = (IBqlTable) value;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Held
  {
    [DebuggerStepThrough] get => this.Items.Held;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Dirty
  {
    [DebuggerStepThrough] get => this.Items.Dirty;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Cached
  {
    [DebuggerStepThrough] get => this.Items.Cached;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Inserted
  {
    [DebuggerStepThrough] get => this.Items.Inserted;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Updated
  {
    [DebuggerStepThrough] get => this.Items.Updated;
  }

  IEnumerable<TNode> PXCache<TNode>.IStronglyTypedRows.Deleted
  {
    [DebuggerStepThrough] get => this.Items.Deleted;
  }

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.Insert() => (TNode) this.Insert();

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.CreateInstance() => (TNode) this.CreateInstance();

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.CreateCopy(TNode item) => PXCache<TNode>.CreateCopy(item);

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.RestoreCopy(TNode item, TNode copy)
  {
    PXCache<TNode>.RestoreCopy(item, copy);
    return item;
  }

  [DebuggerStepThrough]
  TExtension PXCache<TNode>.IStronglyTypedRows.GetExtension<TExtension>(TNode item)
  {
    return this.GetExtension<TExtension>((object) item);
  }

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.GetMain<TExtension>(TExtension extension)
  {
    return (TNode) this.GetMain<TExtension>(extension);
  }

  [DebuggerStepThrough]
  TNode PXCache<TNode>.IStronglyTypedRows.Extend<TParent>(TParent item)
  {
    return (TNode) this.Extend<TParent>(item);
  }

  /// <inheritdoc cref="T:PX.Data.PXCache`1.IStronglyTypedRows" />
  public PXCache<TNode>.IStronglyTypedRows Rows
  {
    [DebuggerStepThrough] get => (PXCache<TNode>.IStronglyTypedRows) this;
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetStatus(System.Object)" />
  [DebuggerStepThrough]
  public PXEntryStatus GetStatus(TNode item) => this.GetStatus((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.IsArchived(System.Object)" />
  [DebuggerStepThrough]
  public bool IsArchived(TNode item) => this.IsArchived((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.IsKeysFilled(System.Object)" />
  [DebuggerStepThrough]
  public bool IsKeysFilled(TNode item) => this.IsKeysFilled((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.ObjectsEqual(System.Object,System.Object)" />
  [DebuggerStepThrough]
  public bool ObjectsEqual(TNode a, TNode b) => this.ObjectsEqual((object) a, (object) b);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.Insert" />
  [DebuggerStepThrough]
  public TNode Insert(TNode data) => (TNode) this.Insert((object) data);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.Update(System.Object)" />
  [DebuggerStepThrough]
  public TNode Update(TNode data) => (TNode) this.Update((object) data);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.Delete(System.Object)" />
  [DebuggerStepThrough]
  public TNode Delete(TNode data) => (TNode) this.Delete((object) data);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.Locate(System.Object)" />
  [DebuggerStepThrough]
  public TNode Locate(TNode item) => (TNode) this.Locate((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetOriginal(System.Object)" />
  [DebuggerStepThrough]
  public TNode GetOriginal(TNode data) => (TNode) this.GetOriginal((object) data);

  /// <inheritdoc cref="M:PX.Data.PXCache`1.Remove(System.Object)" />
  [DebuggerStepThrough]
  public void Remove(TNode item) => this.Remove((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowSelected(System.Object)" />
  [DebuggerStepThrough]
  public void RaiseRowSelected(TNode item) => this.RaiseRowSelected((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowInserting(System.Object)" />
  [DebuggerStepThrough]
  public bool RaiseRowInserting(TNode item) => this.RaiseRowInserting((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowInserted(System.Object)" />
  [DebuggerStepThrough]
  public void RaiseRowInserted(TNode item) => this.RaiseRowInserted((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowUpdating(System.Object,System.Object)" />
  [DebuggerStepThrough]
  public bool RaiseRowUpdating(TNode item, TNode newItem)
  {
    return this.RaiseRowUpdating((object) item, (object) newItem);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowUpdated(System.Object,System.Object)" />
  [DebuggerStepThrough]
  public void RaiseRowUpdated(TNode newItem, TNode oldItem)
  {
    this.RaiseRowUpdated((object) newItem, (object) oldItem);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowDeleting(System.Object)" />
  [DebuggerStepThrough]
  public bool RaiseRowDeleting(TNode item) => this.RaiseRowDeleting((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowDeleted(System.Object)" />
  [DebuggerStepThrough]
  public void RaiseRowDeleted(TNode item) => this.RaiseRowDeleted((object) item);

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowPersisting(System.Object,PX.Data.PXDBOperation)" />
  [DebuggerStepThrough]
  public bool RaiseRowPersisting(TNode item, PXDBOperation operation)
  {
    return this.RaiseRowPersisting((object) item, operation);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseRowPersisted(System.Object,PX.Data.PXDBOperation,PX.Data.PXTranStatus,System.Exception)" />
  [DebuggerStepThrough]
  public void RaiseRowPersisted(
    TNode item,
    PXDBOperation operation,
    PXTranStatus tranStatus,
    Exception exception)
  {
    this.RaiseRowPersisted((object) item, operation, tranStatus, exception);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseCommandPreparing(System.String,System.Object,System.Object,PX.Data.PXDBOperation,System.Type,PX.Data.PXCommandPreparingEventArgs.FieldDescription@)" />
  [DebuggerStepThrough]
  public bool RaiseCommandPreparing<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    TValue value,
    PXDBOperation operation,
    out PXCommandPreparingEventArgs.FieldDescription description,
    System.Type table)
    where TValue : class
  {
    string name1 = PXCache<TNode>.GetName((LambdaExpression) name);
    // ISSUE: variable of a boxed type
    __Boxed<TNode> row1 = (object) row;
    // ISSUE: variable of a boxed type
    __Boxed<TValue> local1 = (object) value;
    int operation1 = (int) operation;
    System.Type table1 = table;
    if ((object) table1 == null)
      table1 = typeof (TNode);
    ref PXCommandPreparingEventArgs.FieldDescription local2 = ref description;
    return this.RaiseCommandPreparing(name1, (object) row1, (object) local1, (PXDBOperation) operation1, table1, out local2);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseCommandPreparing(System.String,System.Object,System.Object,PX.Data.PXDBOperation,System.Type,PX.Data.PXCommandPreparingEventArgs.FieldDescription@)" />
  [DebuggerStepThrough]
  public bool RaiseCommandPreparing<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    TValue value,
    PXDBOperation operation,
    out PXCommandPreparingEventArgs.FieldDescription description,
    System.Type table)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    string name1 = PXCache<TNode>.GetName((LambdaExpression) name);
    // ISSUE: variable of a boxed type
    __Boxed<TNode> row1 = (object) row;
    // ISSUE: variable of a boxed type
    __Boxed<TValue> local1 = (object) value;
    int operation1 = (int) operation;
    System.Type table1 = table;
    if ((object) table1 == null)
      table1 = typeof (TNode);
    ref PXCommandPreparingEventArgs.FieldDescription local2 = ref description;
    return this.RaiseCommandPreparing(name1, (object) row1, (object) local1, (PXDBOperation) operation1, table1, out local2);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseCommandPreparing(System.String,System.Object,System.Object,PX.Data.PXDBOperation,System.Type,PX.Data.PXCommandPreparingEventArgs.FieldDescription@)" />
  [DebuggerStepThrough]
  public bool RaiseCommandPreparing<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    TValue? value,
    PXDBOperation operation,
    out PXCommandPreparingEventArgs.FieldDescription description,
    System.Type table)
    where TValue : struct
  {
    string name1 = PXCache<TNode>.GetName((LambdaExpression) name);
    // ISSUE: variable of a boxed type
    __Boxed<TNode> row1 = (object) row;
    // ISSUE: variable of a boxed type
    __Boxed<TValue?> local1 = (ValueType) value;
    int operation1 = (int) operation;
    System.Type table1 = table;
    if ((object) table1 == null)
      table1 = typeof (TNode);
    ref PXCommandPreparingEventArgs.FieldDescription local2 = ref description;
    return this.RaiseCommandPreparing(name1, (object) row1, (object) local1, (PXDBOperation) operation1, table1, out local2);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseCommandPreparing(System.String,System.Object,System.Object,PX.Data.PXDBOperation,System.Type,PX.Data.PXCommandPreparingEventArgs.FieldDescription@)" />
  [DebuggerStepThrough]
  public bool RaiseCommandPreparing<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    TValue? value,
    PXDBOperation operation,
    out PXCommandPreparingEventArgs.FieldDescription description,
    System.Type table)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    string name1 = PXCache<TNode>.GetName((LambdaExpression) name);
    // ISSUE: variable of a boxed type
    __Boxed<TNode> row1 = (object) row;
    // ISSUE: variable of a boxed type
    __Boxed<TValue?> local1 = (ValueType) value;
    int operation1 = (int) operation;
    System.Type table1 = table;
    if ((object) table1 == null)
      table1 = typeof (TNode);
    ref PXCommandPreparingEventArgs.FieldDescription local2 = ref description;
    return this.RaiseCommandPreparing(name1, (object) row1, (object) local1, (PXDBOperation) operation1, table1, out local2);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldDefaulting(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldDefaulting<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    out TValue newValue)
    where TValue : class
  {
    return this.RaiseFieldDefaulting<TValue>(row, (LambdaExpression) name, out newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldDefaulting(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldDefaulting<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    out TValue newValue)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldDefaulting<TValue>(row, (LambdaExpression) name, out newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldDefaulting(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldDefaulting<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    out TValue? newValue)
    where TValue : struct
  {
    return this.RaiseFieldDefaulting<TValue?>(row, (LambdaExpression) name, out newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldDefaulting(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldDefaulting<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    out TValue? newValue)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldDefaulting<TValue?>(row, (LambdaExpression) name, out newValue);
  }

  [DebuggerStepThrough]
  private bool RaiseFieldDefaulting<TValue>(TNode row, LambdaExpression field, out TValue newValue)
  {
    object newValue1;
    int num = this.RaiseFieldDefaulting(PXCache<TNode>.GetName(field), (object) row, out newValue1) ? 1 : 0;
    newValue = (TValue) newValue1;
    return num != 0;
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdating(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldUpdating<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    ref TValue newValue)
    where TValue : class
  {
    return this.RaiseFieldUpdating<TValue>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdating(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldUpdating<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    ref TValue newValue)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldUpdating<TValue>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdating(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldUpdating<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    ref TValue? newValue)
    where TValue : struct
  {
    return this.RaiseFieldUpdating<TValue?>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdating(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldUpdating<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    ref TValue? newValue)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldUpdating<TValue?>(row, (LambdaExpression) name, ref newValue);
  }

  [DebuggerStepThrough]
  private bool RaiseFieldUpdating<TValue>(TNode row, LambdaExpression field, ref TValue newValue)
  {
    object newValue1 = (object) newValue;
    int num = this.RaiseFieldUpdating(PXCache<TNode>.GetName(field), (object) row, ref newValue1) ? 1 : 0;
    newValue = (TValue) newValue1;
    return num != 0;
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldVerifying(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldVerifying<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    ref TValue newValue)
    where TValue : class
  {
    return this.RaiseFieldVerifying<TValue>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldVerifying(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldVerifying<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    ref TValue newValue)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldVerifying<TValue>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldVerifying(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldVerifying<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    ref TValue? newValue)
    where TValue : struct
  {
    return this.RaiseFieldVerifying<TValue?>(row, (LambdaExpression) name, ref newValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldVerifying(System.String,System.Object,System.Object@)" />
  [DebuggerStepThrough]
  public bool RaiseFieldVerifying<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    ref TValue? newValue)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseFieldVerifying<TValue?>(row, (LambdaExpression) name, ref newValue);
  }

  [DebuggerStepThrough]
  private bool RaiseFieldVerifying<TValue>(TNode row, LambdaExpression field, ref TValue newValue)
  {
    object newValue1 = (object) newValue;
    int num = this.RaiseFieldVerifying(PXCache<TNode>.GetName(field), (object) row, ref newValue1) ? 1 : 0;
    newValue = (TValue) newValue1;
    return num != 0;
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdated(System.String,System.Object,System.Object)" />
  [DebuggerStepThrough]
  public void RaiseFieldUpdated<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    TValue oldValue)
    where TValue : class
  {
    this.RaiseFieldUpdated(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) oldValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdated(System.String,System.Object,System.Object)" />
  [DebuggerStepThrough]
  public void RaiseFieldUpdated<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    TValue oldValue)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.RaiseFieldUpdated(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) oldValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdated(System.String,System.Object,System.Object)" />
  [DebuggerStepThrough]
  public void RaiseFieldUpdated<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    TValue? oldValue)
    where TValue : struct
  {
    this.RaiseFieldUpdated(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) oldValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseFieldUpdated(System.String,System.Object,System.Object)" />
  [DebuggerStepThrough]
  public void RaiseFieldUpdated<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    TValue? oldValue)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.RaiseFieldUpdated(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) oldValue);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />
  [DebuggerStepThrough]
  public bool RaiseExceptionHandling<TValue>(
    Expression<Func<TNode, TValue>> name,
    TNode row,
    TValue newValue,
    Exception exception)
    where TValue : class
  {
    return this.RaiseExceptionHandling(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) newValue, exception);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />
  [DebuggerStepThrough]
  public bool RaiseExceptionHandling<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> name,
    TNode row,
    TValue newValue,
    Exception exception)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseExceptionHandling(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) newValue, exception);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />
  [DebuggerStepThrough]
  public bool RaiseExceptionHandling<TValue>(
    Expression<Func<TNode, TValue?>> name,
    TNode row,
    TValue? newValue,
    Exception exception)
    where TValue : struct
  {
    return this.RaiseExceptionHandling(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) newValue, exception);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache.RaiseExceptionHandling(System.String,System.Object,System.Object,System.Exception)" />
  [DebuggerStepThrough]
  public bool RaiseExceptionHandling<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> name,
    TNode row,
    TValue? newValue,
    Exception exception)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return this.RaiseExceptionHandling(PXCache<TNode>.GetName((LambdaExpression) name), (object) row, (object) newValue, exception);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetDefaultExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetDefaultExt<TValue>(
    Expression<Func<TNode, TValue>> fieldName,
    TNode data,
    TValue value = null)
    where TValue : class
  {
    this.SetDefaultExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetDefaultExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetDefaultExt<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> fieldName,
    TNode data,
    TValue value = null)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetDefaultExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetDefaultExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetDefaultExt<TValue>(
    Expression<Func<TNode, TValue?>> fieldName,
    TNode data,
    TValue? value = null)
    where TValue : struct
  {
    this.SetDefaultExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetDefaultExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetDefaultExt<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> fieldName,
    TNode data,
    TValue? value = null)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetDefaultExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValueExt<TValue>(
    Expression<Func<TNode, TValue>> fieldName,
    TNode data,
    TValue value)
    where TValue : class
  {
    this.SetValueExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValueExt<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> fieldName,
    TNode data,
    TValue value)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetValueExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValueExt<TValue>(
    Expression<Func<TNode, TValue?>> fieldName,
    TNode data,
    TValue? value)
    where TValue : struct
  {
    this.SetValueExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValueExt(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValueExt<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> fieldName,
    TNode data,
    TValue? value)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetValueExt((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValuePending(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValuePending<TValue>(
    Expression<Func<TNode, TValue>> fieldName,
    TNode data,
    TValue value)
    where TValue : class
  {
    this.SetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValuePending(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValuePending<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> fieldName,
    TNode data,
    TValue value)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValuePending(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValuePending<TValue>(
    Expression<Func<TNode, TValue?>> fieldName,
    TNode data,
    TValue? value)
    where TValue : struct
  {
    this.SetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.SetValuePending(System.Object,System.String,System.Object)" />
  [DebuggerStepThrough]
  public void SetValuePending<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> fieldName,
    TNode data,
    TValue? value)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    this.SetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName), (object) value);
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValuePending(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue GetValuePending<TValue>(Expression<Func<TNode, TValue>> fieldName, TNode data) where TValue : class
  {
    return (TValue) this.GetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValuePending(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue GetValuePending<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> fieldName,
    TNode data)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return (TValue) this.GetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValuePending(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue? GetValuePending<TValue>(Expression<Func<TNode, TValue?>> fieldName, TNode data) where TValue : struct
  {
    return new TValue?((TValue) this.GetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName)));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValuePending(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue? GetValuePending<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> fieldName,
    TNode data)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return new TValue?((TValue) this.GetValuePending((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName)));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValueOriginal(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue GetValueOriginal<TValue>(Expression<Func<TNode, TValue>> fieldName, TNode data) where TValue : class
  {
    return (TValue) this.GetValueOriginal((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValueOriginal(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue GetValueOriginal<TValue, TExtension>(
    Expression<Func<TExtension, TValue>> fieldName,
    TNode data)
    where TValue : class
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return (TValue) this.GetValueOriginal((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValueOriginal(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue? GetValueOriginal<TValue>(Expression<Func<TNode, TValue?>> fieldName, TNode data) where TValue : struct
  {
    return (TValue?) this.GetValueOriginal((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetValueOriginal(System.Object,System.String)" />
  [DebuggerStepThrough]
  public TValue? GetValueOriginal<TValue, TExtension>(
    Expression<Func<TExtension, TValue?>> fieldName,
    TNode data)
    where TValue : struct
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return (TValue?) this.GetValueOriginal((object) data, PXCache<TNode>.GetName((LambdaExpression) fieldName));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetAttributes(System.Object,System.String)" />
  [DebuggerStepThrough]
  public IEnumerable<PXEventSubscriberAttribute> GetAttributesOf(
    Expression<Func<TNode, object>> name,
    TNode data = null,
    bool readOnly = false)
  {
    return !readOnly ? this.GetAttributes((object) data, PXCache<TNode>.GetName((LambdaExpression) name)) : this.GetAttributesReadonly((object) data, PXCache<TNode>.GetName((LambdaExpression) name));
  }

  /// <inheritdoc cref="M:PX.Data.PXCache`1.GetAttributes(System.Object,System.String)" />
  [DebuggerStepThrough]
  public IEnumerable<PXEventSubscriberAttribute> GetAttributesOf<TExtension>(
    Expression<Func<TExtension, object>> name,
    TNode data = null,
    bool readOnly = false)
    where TExtension : PXCacheExtension, IExtends<TNode>
  {
    return !readOnly ? this.GetAttributes((object) data, PXCache<TNode>.GetName((LambdaExpression) name)) : this.GetAttributesReadonly((object) data, PXCache<TNode>.GetName((LambdaExpression) name));
  }

  [DebuggerStepThrough]
  private static string GetName(LambdaExpression exp)
  {
    if (exp.Body is MemberExpression body1)
      return body1.Member.Name;
    if (exp.Body is UnaryExpression body2 && body2.NodeType == ExpressionType.Convert && body2.Operand is MemberExpression operand)
      return operand.Member.Name;
    throw new InvalidOperationException($"The \"{exp}\" expression does not represent a field of a DAC.");
  }

  /// <exclude />
  private delegate object memberwiseCloneDelegate(TNode item) where TNode : class, IBqlTable, new();

  protected delegate PXCacheExtension[] memberwiseCloneExtensionsDelegate(
    TNode item,
    PXCacheExtension[] extensions)
    where TNode : class, IBqlTable, new();

  /// <exclude />
  protected sealed class ItemComparer : IEqualityComparer<TNode>
  {
    private PXCache _Cache;

    public ItemComparer(PXCache cache) => this._Cache = cache;

    public int GetHashCode(TNode item) => this._Cache.GetObjectHashCode((object) item);

    public bool Equals(TNode item1, TNode item2)
    {
      return this._Cache.ObjectsEqual((object) item1, (object) item2);
    }
  }

  /// <exclude />
  internal class PXCacheDebugView
  {
    private readonly PXCache<TNode> Src;

    public PXCacheDebugView(PXCache<TNode> src) => this.Src = src;

    public TNode Current => (TNode) this.Src.Current;

    public TNode[] Inserted => this.Src.Inserted.OfType<TNode>().ToArray<TNode>();

    public TNode[] Updated => this.Src.Updated.OfType<TNode>().ToArray<TNode>();

    public TNode[] Deleted => this.Src.Deleted.OfType<TNode>().ToArray<TNode>();

    public string[] Fields => this.Src.Fields.ToArray();

    public string[] BqlFields
    {
      get
      {
        return this.Src.BqlFields.Select<System.Type, string>((Func<System.Type, string>) (_ => _.Name)).ToArray<string>();
      }
    }
  }

  /// <exclude />
  protected delegate PXCacheExtension[] CreateExtensionsDelegate(TNode data) where TNode : class, IBqlTable, new();

  /// <exclude />
  protected delegate object GetValueByOrdinalDelegate(
    TNode data,
    int ordinal,
    PXCacheExtension[] extensions)
    where TNode : class, IBqlTable, new();

  /// <exclude />
  protected delegate void SetValueByOrdinalDelegate(
    TNode data,
    int ordinal,
    object value,
    PXCacheExtension[] extensions)
    where TNode : class, IBqlTable, new();

  /// <exclude />
  protected delegate int GetHashCodeDelegate(TNode a, PXCollationComparer comparer) where TNode : class, IBqlTable, new();

  /// <exclude />
  protected delegate bool EqualsDelegate(TNode a, TNode b, PXCollationComparer comparer) where TNode : class, IBqlTable, new();

  /// <exclude />
  protected class CacheStaticInfo : PXCache.CacheStaticInfoBase
  {
    public PXCache<TNode>.CreateExtensionsDelegate _CreateExtensions;
    public PXCache<TNode>.memberwiseCloneExtensionsDelegate _CloneExtensions;
    public PXCache<TNode>.SetValueByOrdinalDelegate _SetValueByOrdinal;
    public PXCache<TNode>.GetValueByOrdinalDelegate _GetValueByOrdinal;
    public ReaderWriterLock _DelegatesLock;
    public Dictionary<string, PXCache<TNode>.GetHashCodeDelegate> _DelegatesGetHashCode;
    public Dictionary<string, PXCache<TNode>.EqualsDelegate> _DelegatesEquals;
    public Dictionary<string, string> _InactiveFields;
  }

  /// <summary>
  /// Provides access to a set of strongly typed versions of elements of the <see cref="T:PX.Data.PXCache" /> class.
  /// </summary>
  public interface IStronglyTypedRows
  {
    /// <inheritdoc cref="P:PX.Data.PXCache`1.Current" />
    TNode Current { get; set; }

    /// <inheritdoc cref="!:PXCache&lt;TNode&gt;.ActiveRow" />
    TNode ActiveRow { get; set; }

    /// <inheritdoc cref="M:PX.Data.PXCache`1.Insert" />
    TNode Insert();

    /// <inheritdoc cref="M:PX.Data.PXCache`1.CreateInstance" />
    TNode CreateInstance();

    /// <inheritdoc cref="M:PX.Data.PXCache`1.CreateCopy(System.Object)" />
    TNode CreateCopy(TNode item);

    /// <inheritdoc cref="M:PX.Data.PXCache`1.RestoreCopy(System.Object,System.Object)" />
    TNode RestoreCopy(TNode item, TNode copy);

    /// <summary>
    /// Gets the collection of unchanged data records that have been marked as <see cref="F:PX.Data.PXEntryStatus.Held" />
    /// within their <see cref="T:PX.Data.PXCache" /> object to avoid being collected during memory cleanup.
    /// </summary>
    IEnumerable<TNode> Held { get; }

    /// <inheritdoc cref="P:PX.Data.PXCache`1.Dirty" />
    IEnumerable<TNode> Dirty { get; }

    /// <inheritdoc cref="P:PX.Data.PXCache`1.Cached" />
    IEnumerable<TNode> Cached { get; }

    /// <inheritdoc cref="P:PX.Data.PXCache`1.Inserted" />
    IEnumerable<TNode> Inserted { get; }

    /// <inheritdoc cref="P:PX.Data.PXCache`1.Updated" />
    IEnumerable<TNode> Updated { get; }

    /// <inheritdoc cref="P:PX.Data.PXCache`1.Deleted" />
    IEnumerable<TNode> Deleted { get; }

    /// <inheritdoc cref="M:PX.Data.PXCache`1.GetExtension``1(System.Object)" />
    TExtension GetExtension<TExtension>(TNode item) where TExtension : PXCacheExtension, IExtends<TNode>;

    /// <inheritdoc cref="M:PX.Data.PXCache`1.GetMain``1(``0)" />
    TNode GetMain<TExtension>(TExtension extension) where TExtension : PXCacheExtension, IExtends<TNode>;

    /// <inheritdoc cref="M:PX.Data.PXCache`1.Extend``1(``0)" />
    TNode Extend<TParent>(TParent item) where TParent : class, IBqlTable, new();
  }
}
