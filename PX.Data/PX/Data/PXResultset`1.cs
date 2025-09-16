// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResultset`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXResultset<T0> : 
  IList<PXResult<T0>>,
  ICollection<PXResult<T0>>,
  IEnumerable<PXResult<T0>>,
  IEnumerable,
  ICollection,
  IPXResultset,
  IPXResultsetBase,
  IList,
  IOrderedQueryable<PXResult<T0>>,
  IQueryable<PXResult<T0>>,
  IQueryable,
  IOrderedQueryable
  where T0 : class, IBqlTable, new()
{
  private SQLQueryable<PXResult<T0>> _relinqHelper;
  private PXDelayedQuery _DelayedQuery;
  private PXDelayedQuery _DelayedQueryPersisted;
  private List<PXResult<T0>> _ListResult = new List<PXResult<T0>>();
  private PXResult<T0> _SingleRowResult;
  private bool _singleRowRequested;

  internal SQLQueryable<PXResult<T0>> _RelinqHelper
  {
    get
    {
      if (this._relinqHelper == null)
        this._relinqHelper = new SQLQueryable<PXResult<T0>>(this._DelayedQueryPersisted?.View.Graph, (IPXResultsetBase) this);
      return this._relinqHelper;
    }
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreResult(List<object> selectResult)
  {
    PXDelayedQuery delayedQuery = this.GetDelayedQuery();
    PXView view = delayedQuery.View;
    object[] objArray = view.PrepareParameters(delayedQuery.currents, delayedQuery.arguments);
    view.StoreResult(selectResult, PXQueryParameters.ExplicitParameters(objArray));
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreResult(IBqlTable selectResult)
  {
    PXDelayedQuery delayedQuery = this.GetDelayedQuery();
    PXView view = delayedQuery.View;
    object[] objArray = view.PrepareParameters(delayedQuery.currents, delayedQuery.arguments);
    view.StoreResult(selectResult, PXQueryParameters.ExplicitParameters(objArray));
  }

  /// <summary>
  /// Useful when real type of PXResult have more type parameters than PXResultset declaration
  /// (for example PXSelectJoin.Select() produces such case) and you need missed types in your LINQ expression.
  /// </summary>
  /// <example>
  /// <code>
  /// var sel = PXSelectJoin&lt;SupplierProductTest,
  /// 	InnerJoin&lt;SupplierTest, On&lt;SupplierTest.supplierID, Equal&lt;SupplierProductTest.supplierID&gt;&gt;,
  /// 		InnerJoin&lt;ProductTest, On&lt;ProductTest.productID, Equal&lt;SupplierProductTest.productID&gt;&gt;&gt;&gt;&gt;(graph);
  /// 
  /// var query = sel.Select().ToQueryable&lt;PXResult&lt;SupplierProductTest, SupplierTest, ProductTest&gt;&gt;().Where(...)
  /// </code>
  /// </example>
  public IQueryable<TPXResult> ToQueryable<TPXResult>() where TPXResult : PXResult<T0>
  {
    PXDelayedQuery delayedQueryPersisted = this._DelayedQueryPersisted;
    return delayedQueryPersisted == null ? (IQueryable<TPXResult>) null : (IQueryable<TPXResult>) new SQLQueryable<TPXResult>(delayedQueryPersisted.View.Graph, (IPXResultsetBase) this);
  }

  public IQueryable<TResult> Cast<TResult>()
  {
    System.Type c = typeof (TResult);
    System.Type type = typeof (PXResult);
    if (type.IsAssignableFrom(c) && c.IsGenericType)
      return (IQueryable<TResult>) typeof (SQLinqExtensions).GetMethod("PXCast").MakeGenericMethod(c).Invoke((object) null, new object[1]
      {
        (object) this
      });
    if (!typeof (IBqlTable).IsAssignableFrom(c))
      return this.AsEnumerable<PXResult<T0>>().Cast<TResult>().AsQueryable<TResult>();
    MethodInfo methodInfo = typeof (SQLinqExtensions).GetMethod("PXCast").MakeGenericMethod(typeof (PXResult<>).MakeGenericType(c));
    ParameterExpression instance = Expression.Parameter(type);
    Expression<Func<PXResult, TResult>> selector = Expression.Lambda<Func<PXResult, TResult>>((Expression) Expression.Call((Expression) instance, "GetItem", new System.Type[1]
    {
      c
    }), instance);
    return ((IQueryable<PXResult>) methodInfo.Invoke((object) null, new object[1]
    {
      (object) this
    })).Select<PXResult, TResult>(selector);
  }

  public Expression Expression => this._RelinqHelper?.Expression;

  public IQueryProvider Provider => this._RelinqHelper?.Provider;

  System.Type IQueryable.ElementType => this._RelinqHelper?.ElementType;

  public object GetCollection() => (object) this._List;

  public System.Type GetCollectionType() => typeof (List<PXResult<T0>>);

  public PXDelayedQuery GetDelayedQuery() => this._DelayedQueryPersisted;

  bool IList.IsFixedSize => false;

  bool IList.IsReadOnly => false;

  int IList.Add(object item)
  {
    if (!this.IsCompatibleObject(item))
      throw new PXArgumentException(nameof (item));
    this.Add((PXResult<T0>) item);
    return this.Count - 1;
  }

  private bool IsCompatibleObject(object value)
  {
    switch (value)
    {
      case PXResult<T0> _:
      case null:
        return true;
      default:
        return false;
    }
  }

  bool IList.Contains(object item)
  {
    return this.IsCompatibleObject(item) && this.Contains((PXResult<T0>) item);
  }

  int IList.IndexOf(object item)
  {
    return this.IsCompatibleObject(item) ? this.IndexOf((PXResult<T0>) item) : -1;
  }

  void IList.Insert(int index, object item)
  {
    if (!this.IsCompatibleObject(item))
      throw new PXArgumentException(nameof (item));
    this.Insert(index, (PXResult<T0>) item);
  }

  void IList.Remove(object item)
  {
    if (!this.IsCompatibleObject(item))
      return;
    this.Remove((PXResult<T0>) item);
  }

  object IList.this[int index]
  {
    get => (object) this[index];
    set
    {
      this[index] = this.IsCompatibleObject(value) ? (PXResult<T0>) value : throw new PXArgumentException("item");
    }
  }

  internal void SetDelayedQuery(PXDelayedQuery src)
  {
    this._DelayedQuery = src;
    if (src.View.GetType() == typeof (PXView))
      this._DelayedQueryPersisted = src;
    this._ListResult = (List<PXResult<T0>>) null;
  }

  private static PXResult<T0> Convert(object item)
  {
    switch (item)
    {
      case T0 i0:
        return new PXResult<T0>(i0);
      default:
        return (PXResult<T0>) item;
    }
  }

  protected List<PXResult<T0>> _List
  {
    get
    {
      if (this._singleRowRequested)
        throw new PXException("Single row was requested");
      if (this._ListResult == null)
      {
        this._ListResult = this._DelayedQuery.GetRows(false).Select<object, PXResult<T0>>(PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert ?? (PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert = new Func<object, PXResult<T0>>(PXResultset<T0>.Convert))).ToList<PXResult<T0>>();
        this._DelayedQuery = (PXDelayedQuery) null;
      }
      return this._ListResult;
    }
  }

  public int IndexOf(PXResult<T0> item) => this._List.IndexOf(item);

  public void Insert(int index, PXResult<T0> item) => this._List.Insert(index, item);

  public void RemoveAt(int index) => this._List.RemoveAt(index);

  public int Count => this._List.Count;

  public void Sort(Comparison<PXResult<T0>> comparison) => this._List.Sort(comparison);

  public void Sort(IComparer<PXResult<T0>> comparer) => this._List.Sort(comparer);

  bool ICollection<PXResult<T0>>.IsReadOnly => ((ICollection<PXResult<T0>>) this._List).IsReadOnly;

  public void Add(PXResult<T0> item) => this._List.Add(item);

  public void Clear() => this._List.Clear();

  public bool Contains(PXResult<T0> item) => this._List.Contains(item);

  public void CopyTo(PXResult<T0>[] array, int arrayIndex) => this._List.CopyTo(array, arrayIndex);

  public void AddRange(IEnumerable<PXResult<T0>> collection) => this._List.AddRange(collection);

  public void Reverse() => this._List.Reverse();

  void ICollection.CopyTo(Array array, int arrayIndex)
  {
    ((ICollection) this._List).CopyTo(array, arrayIndex);
  }

  bool ICollection.IsSynchronized => ((ICollection) this._List).IsSynchronized;

  object ICollection.SyncRoot => ((ICollection) this._List).SyncRoot;

  public bool Remove(PXResult<T0> item) => this._List.Remove(item);

  public int? RowCount => this._List.Count > 0 ? this[0].RowCount : new int?(0);

  public PXResult<T0> this[int index]
  {
    get
    {
      PXResult<T0> pxResult = this._List[index];
      (PXContext.GetSlot<Dictionary<System.Type, PXResult>>() ?? PXContext.SetSlot<Dictionary<System.Type, PXResult>>(new Dictionary<System.Type, PXResult>()))[typeof (PXResult<T0>)] = (PXResult) pxResult;
      return pxResult;
    }
    set
    {
      (PXContext.GetSlot<Dictionary<System.Type, PXResult>>() ?? PXContext.SetSlot<Dictionary<System.Type, PXResult>>(new Dictionary<System.Type, PXResult>()))[typeof (PXResult<T0>)] = (PXResult) value;
      this._List[index] = value;
    }
  }

  public IEnumerator<PXResult<T0>> GetEnumerator()
  {
    return (IEnumerator<PXResult<T0>>) new PXResultset<T0>.Enumerator(this._List);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new PXResultset<T0>.Enumerator(this._List);
  }

  public static implicit operator T0(PXResultset<T0> l)
  {
    if (l == null)
      return (T0) (PXResult<T0>) null;
    if (l._singleRowRequested)
      return (T0) l._SingleRowResult;
    if (l._DelayedQuery != null)
    {
      l._singleRowRequested = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      l._SingleRowResult = l._DelayedQuery.GetRows(true).Select<object, PXResult<T0>>(PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert ?? (PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert = new Func<object, PXResult<T0>>(PXResultset<T0>.Convert))).FirstOrDefault<PXResult<T0>>();
      return (T0) l._SingleRowResult;
    }
    return l != null && l.Count > 0 ? (T0) l[0] : default (T0);
  }

  public static implicit operator PXResult<T0>(PXResultset<T0> l)
  {
    if (l == null)
      return (PXResult<T0>) null;
    if (l._singleRowRequested)
      return l._SingleRowResult;
    if (l._DelayedQuery != null)
    {
      l._singleRowRequested = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      l._SingleRowResult = l._DelayedQuery.GetRows(true).Select<object, PXResult<T0>>(PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert ?? (PXResultset<T0>.\u003C\u003EO.\u003C0\u003E__Convert = new Func<object, PXResult<T0>>(PXResultset<T0>.Convert))).FirstOrDefault<PXResult<T0>>();
      return l._SingleRowResult;
    }
    return l != null && l.Count > 0 ? l[0] : (PXResult<T0>) null;
  }

  System.Type IPXResultset.GetItemType(int i) => i == 0 ? typeof (T0) : (System.Type) null;

  object IPXResultset.GetItem(int rowNbr, int i)
  {
    return rowNbr < 0 || rowNbr >= this.Count || i != 0 ? (object) null : this[rowNbr][i];
  }

  int IPXResultset.GetTableCount() => 1;

  int IPXResultset.GetRowCount() => this.Count;

  public IEnumerable<T0> FirstTableItems
  {
    get
    {
      return this.AsEnumerable<PXResult<T0>>().Select<PXResult<T0>, T0>((Func<PXResult<T0>, T0>) (r => (T0) r[0]));
    }
  }

  public T0 TopFirst => (T0) this;

  /// <exclude />
  [Serializable]
  public struct Enumerator : IEnumerator<PXResult<T0>>, IDisposable, IEnumerator
  {
    private List<PXResult<T0>> _List;
    private int _Index;
    private PXResult<T0> _Current;
    private System.Type _Key;

    internal Enumerator(List<PXResult<T0>> list)
    {
      System.Type type = list.Count <= 0 || list[0] == null ? typeof (PXResult<T0>) : list[0].GetType();
      while (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof (PXResult<>) && type.GetGenericTypeDefinition() != typeof (PXResult<,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,>) && type.GetGenericTypeDefinition() != typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,,>))
        type = type.BaseType;
      this._Key = type;
      this._List = list;
      this._Current = (PXResult<T0>) null;
      this._Index = 0;
    }

    public void Dispose() => PXContext.GetSlot<Dictionary<System.Type, PXResult>>()?.Remove(this._Key);

    public bool MoveNext()
    {
      Dictionary<System.Type, PXResult> dictionary = PXContext.GetSlot<Dictionary<System.Type, PXResult>>();
      if (this._Index < this._List.Count)
      {
        this._Current = this._List[this._Index];
        ++this._Index;
        if (dictionary == null)
          dictionary = PXContext.SetSlot<Dictionary<System.Type, PXResult>>(new Dictionary<System.Type, PXResult>());
        dictionary[this._Key] = (PXResult) this._Current;
        return true;
      }
      this._Current = (PXResult<T0>) null;
      this._Index = this._List.Count + 1;
      dictionary?.Remove(this._Key);
      return false;
    }

    public PXResult<T0> Current => this._Current;

    object IEnumerator.Current
    {
      get
      {
        if (this._Index == 0 || this._Index == this._List.Count + 1)
          throw new PXException("Index is outside bounds of the array");
        return (object) this.Current;
      }
    }

    void IEnumerator.Reset()
    {
      this._Index = 0;
      this._Current = (PXResult<T0>) null;
    }
  }
}
