// Decompiled with JetBrains decompiler
// Type: PX.Data.PXView
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Export;
using PX.Common;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>
/// A controller that executes the BQL command and implements interfaces for sorting, searching, merging data with the cached changes, and caching the result set.
/// </summary>
/// <summary>A controller that executes the BQL command and implements
/// interfaces for sorting, searching, merging data with the cached
/// changes, and caching the result set.</summary>
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay, nq}")]
public class PXView
{
  internal RestrictedFieldsSet RestrictedFields = new RestrictedFieldsSet();
  internal bool MainTableCanBeRestricted = true;
  internal PXGraphExtension[] Extensions;
  private Func<PXFilterTuple> _ExternalFilterDelegate = (Func<PXFilterTuple>) (() => new PXFilterTuple());
  private Func<PXSortColumnsTuple> _ExternalSortsDelegate = (Func<PXSortColumnsTuple>) (() => new PXSortColumnsTuple((string[]) null, (bool[]) null, true));
  private System.Type[] _dependToCacheTypes;
  protected PXGraph _Graph;
  /// <exclude />
  public PXViewExtensionAttribute[] Attributes;
  protected bool _IsReadOnly;
  private BqlCommand _Select;
  protected Delegate _Delegate;
  protected System.Type _CacheType;
  private PXCache _cache;
  private System.Action _init;
  protected string[] _ParameterNames;
  private protected bool? _isNonStandardView;
  private PXViewQueryCollection _SelectQueries;
  protected bool _AllowSelect = true;
  protected bool _AllowInsert = true;
  protected bool _AllowUpdate = true;
  protected bool _AllowDelete = true;
  protected BqlCommand.Selection _Selection;
  protected System.Type _PrimaryTableType;
  private System.Type[] _InvertedParameters;
  protected PXView _TailSelect;
  private bool? keyReferenceOnly;
  private bool? fullKeyReferenced;
  /// <summary>
  ///  This flag keeps joined DACs (tail) during cache merge of updated main DAC instead of retrieving the tail
  /// </summary>
  internal bool SupressTailSelect;
  private static readonly ConcurrentDictionary<object, IBqlParameter[]> CommandParamsCache = new ConcurrentDictionary<object, IBqlParameter[]>();
  private System.Type _NewOrder;
  private PXView._InvokeDelegate _CustomMethod;
  private PXView._InstantiateDelegate _CreateInstance;
  private static ReaderWriterLock _CreateInstanceLock = new ReaderWriterLock();
  private static Dictionary<PXCreateInstanceKey, PXView._InstantiateDelegate> _CreateInstanceDict = new Dictionary<PXCreateInstanceKey, PXView._InstantiateDelegate>();
  internal bool _AlwaysReturnPXResult;
  private static ReaderWriterLock _InvokeLock = new ReaderWriterLock();
  private static Dictionary<System.Type, PXView._InvokeDelegate> _InvokeDict = new Dictionary<System.Type, PXView._InvokeDelegate>();
  protected bool _IsCommandMutable;
  /// <summary>
  /// The event handler for the <tt>RefreshRequested</tt> event.
  /// </summary>
  /// <exclude />
  public EventHandler RefreshRequested;

  protected static Stack<PXView.Context> _Executing
  {
    get
    {
      return PXContext.GetSlot<Stack<PXView.Context>>() ?? PXContext.SetSlot<Stack<PXView.Context>>(new Stack<PXView.Context>());
    }
  }

  protected static bool TryPeekExecutingContext(out PXView.Context context)
  {
    Stack<PXView.Context> executing = PXView._Executing;
    if (executing == null)
    {
      context = (PXView.Context) null;
      return false;
    }
    if (executing.Any<PXView.Context>())
    {
      context = executing.Peek();
      return context != null;
    }
    context = (PXView.Context) null;
    return false;
  }

  public static RestrictedFieldsSet CurrentRestrictedFields
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) && context.RestrictedFields != null ? new RestrictedFieldsSet(context.RestrictedFields) : new RestrictedFieldsSet();
    }
  }

  /// <summary>Gets the names of the fields passed to the <tt>Select(...)</tt> method to filter and sort the data set.</summary>
  public static string[] SortColumns
  {
    get
    {
      List<string> stringList = new List<string>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Sorts != null)
      {
        foreach (PXView.PXSearchColumn sort in context.Sorts)
          stringList.Add(sort.Column);
      }
      return stringList.ToArray();
    }
  }

  /// <summary>Gets the values passed to the <tt>Select(...)</tt> method to indicate whether ordering by the sort columns should be descending or ascending.</summary>
  public static bool[] Descendings
  {
    get
    {
      List<bool> boolList = new List<bool>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Sorts != null)
      {
        foreach (PXView.PXSearchColumn sort in context.Sorts)
          boolList.Add(sort.Descending);
      }
      return boolList.ToArray();
    }
  }

  /// <summary>Gets the values passed to the <tt>Select(...)</tt> method to filter the data set by them.</summary>
  public static object[] Searches
  {
    get
    {
      List<object> objectList = new List<object>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Sorts != null)
      {
        foreach (PXView.PXSearchColumn sort in context.Sorts)
          objectList.Add(sort.OrigSearchValue);
      }
      return objectList.ToArray();
    }
    internal set
    {
      if (value == null)
        value = new object[0];
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context) || context.Sorts == null || context.Sorts.Length != value.Length)
        return;
      for (int index = 0; index < context.Sorts.Length; ++index)
      {
        context.Sorts[index].SearchValue = value[index];
        context.Sorts[index].OrigSearchValue = value[index];
      }
    }
  }

  /// <summary>Gets the graph within which the <tt>Select(...)</tt> method was invoked.</summary>
  public static PXGraph CurrentGraph
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) ? context.View.Graph : (PXGraph) null;
    }
  }

  /// <summary>Gets the filtering conditions originated on the user interface and passed to the <tt>Select(...)</tt> method.</summary>
  public static PXView.PXFilterRowCollection Filters
  {
    get
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Filters != null)
      {
        foreach (PXFilterRow filter in context.Filters)
        {
          pxFilterRowList.Add(new PXFilterRow(filter.DataField, filter.Condition, filter.OrigValue ?? filter.Value, filter.OrigValue2 ?? filter.Value2, filter.Variable));
          pxFilterRowList[pxFilterRowList.Count - 1].OpenBrackets = filter.OpenBrackets;
          pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets = filter.CloseBrackets;
          pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = filter.OrOperator;
        }
      }
      return new PXView.PXFilterRowCollection(pxFilterRowList.ToArray());
    }
  }

  protected internal static PXView.PXSortColumnCollection Sorts
  {
    get
    {
      List<PXView.PXSearchColumn> pxSearchColumnList = new List<PXView.PXSearchColumn>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Sorts != null)
      {
        foreach (PXView.PXSearchColumn sort in context.Sorts)
          pxSearchColumnList.Add((PXView.PXSearchColumn) sort.Clone());
      }
      return new PXView.PXSortColumnCollection(pxSearchColumnList.ToArray());
    }
  }

  /// <exclude />
  public static IEnumerable<PXView.PXSearchColumn> SearchColumns
  {
    get => PXView.Sorts.Cast<PXView.PXSearchColumn>();
  }

  /// <summary>Gets the current data records passed to the <tt>Select(...)</tt> method to process the <tt>Current</tt> and <tt>Optional</tt> parameters.</summary>
  public static object[] Currents
  {
    get
    {
      List<object> objectList = new List<object>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Currents != null)
      {
        foreach (object current in context.Currents)
          objectList.Add(current);
      }
      return objectList.ToArray();
    }
  }

  /// <summary>Gets the values passed to the <tt>Select(...)</tt> method to process such parameters as <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>, and
  /// pre-processed by the <tt>Select(...)</tt> method.</summary>
  public static object[] Parameters
  {
    get
    {
      List<object> objectList = new List<object>();
      PXView.Context context;
      if (PXView.TryPeekExecutingContext(out context) && context.Parameters != null)
      {
        foreach (object parameter in context.Parameters)
          objectList.Add(parameter);
      }
      return objectList.ToArray();
    }
  }

  /// <summary>Gets or sets the value passed to the <tt>Select(...)</tt> method as the index of the first data record to retrieve.</summary>
  public static int StartRow
  {
    get
    {
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return 0;
      return context.ReverseOrder && context.MaximumRows > 0 ? -1 - context.StartRow : context.StartRow;
    }
    set
    {
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return;
      context.StartRow = value;
    }
  }

  /// <summary>Gets the value passed to the <tt>Select(...)</tt> method as the number of data records to retrieve.</summary>
  public static int MaximumRows
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) ? context.MaximumRows : 0;
    }
  }

  public static bool NeedDefaultPrimaryViewObject
  {
    get
    {
      if (PXView.MaximumRows != 1)
        return false;
      return !((IEnumerable<object>) PXView.Searches).Any<object>() || ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (key => key == null));
    }
  }

  /// <summary>Gets the value indicating whether a negative value was passed as the index of the first data record to retrieve.</summary>
  public static bool ReverseOrder
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) && context.ReverseOrder;
    }
    set
    {
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return;
      context.ReverseOrder = value;
    }
  }

  /// <summary>Gets the value indicating whether a view delegate should retrieve total row count.</summary>
  public static bool RetrieveTotalRowCount
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) && context.RetrieveTotalRowCount;
    }
  }

  /// <summary>Sort the provided collection of <tt>PXResult&lt;&gt;</tt> instances by the conditions currently stored in the <tt>PXView</tt> context. This context exists only
  /// during execution of the <see cref="M:PX.Data.PXView.Select(System.Object[],System.Object[],System.Object[],System.String[],System.Boolean[],PX.Data.PXFilterRow[],System.Int32@,System.Int32,System.Int32@)">Select(...)</see> method. The <tt>Sort(IEnumerable)</tt> method may be called in the optional method of the data view to sort
  /// by the conditions that were provided to the <tt>Select(...)</tt> method, which invoked the optional method.</summary>
  /// <param name="list">The collection of <tt>PXResult&lt;&gt;</tt>
  /// instances to sort.</param>
  public static IEnumerable Sort(IEnumerable list)
  {
    PXView.Context context;
    if (!PXView.TryPeekExecutingContext(out context))
      return list;
    List<object> objectList;
    switch (list)
    {
      case List<object> _:
        context.View.SortResult((List<object>) list, context.Sorts, context.ReverseOrder);
        return list;
      case PXView.VersionedList _:
        objectList = (List<object>) new PXView.VersionedList();
        break;
      default:
        objectList = new List<object>();
        break;
    }
    List<object> list1 = objectList;
    foreach (object obj in list)
      list1.Add(obj);
    context.View.SortResult(list1, context.Sorts, context.ReverseOrder);
    if (list1 is PXView.VersionedList)
      ((PXView.VersionedList) list1).Version = ((PXView.VersionedList) list).Version;
    return (IEnumerable) list1;
  }

  /// <exclude />
  public static void SortClear() => PXView.Sorts.Clear();

  /// <exclude />
  public static IEnumerable Filter(IEnumerable list)
  {
    PXView.Context context;
    if (!PXView.TryPeekExecutingContext(out context))
      return list;
    List<object> objectList;
    switch (list)
    {
      case List<object> _:
        context.View.FilterResult((List<object>) list, context.Filters);
        return list;
      case PXView.VersionedList _:
        objectList = (List<object>) new PXView.VersionedList();
        break;
      default:
        objectList = new List<object>();
        break;
    }
    List<object> list1 = objectList;
    foreach (object obj in list)
      list1.Add(obj);
    context.View.FilterResult(list1, context.Filters);
    if (list1 is PXView.VersionedList)
      ((PXView.VersionedList) list1).Version = ((PXView.VersionedList) list).Version;
    return (IEnumerable) list1;
  }

  public static PXView View
  {
    get
    {
      PXView.Context context;
      return PXView.TryPeekExecutingContext(out context) ? context.View : (PXView) null;
    }
  }

  internal string DebuggerDisplay => this._Select.ToString()?.Replace("/n", " ");

  protected PXGraphExtension[] GraphExtensions => this.Extensions;

  internal void StoreExternalFilters(Func<PXFilterTuple> del) => this._ExternalFilterDelegate = del;

  /// <exclude />
  public PXFilterRow[] GetExternalFilters() => this._ExternalFilterDelegate().FilterRows;

  internal Guid? GetExternalFilterID() => this._ExternalFilterDelegate().FilterID;

  internal void StoreExternalSorts(Func<PXSortColumnsTuple> del)
  {
    this._ExternalSortsDelegate = del;
  }

  /// <exclude />
  internal PXSortColumnsTuple GetExternalSortsWithDefs() => this._ExternalSortsDelegate();

  /// <exclude />
  public string[] GetExternalSorts() => this._ExternalSortsDelegate().SortColumns;

  /// <exclude />
  public bool[] GetExternalDescendings() => this._ExternalSortsDelegate().Descendings;

  /// <summary>Resets applied filters for all views in the data graph.</summary>
  public virtual void RequestFiltersReset() => this.FiltersResetRequired = true;

  internal bool FiltersResetRequired { get; private set; }

  internal void SetDependToCacheTypes(IEnumerable<System.Type> types)
  {
    this._dependToCacheTypes = types.ToArray<System.Type>();
  }

  internal IEnumerable<System.Type> GetDependToCacheTypes()
  {
    if (this._dependToCacheTypes != null)
      return (IEnumerable<System.Type>) this._dependToCacheTypes;
    if ((object) this.BqlDelegate != null)
    {
      MethodInfo method1 = this.BqlDelegate.Method;
      int num;
      if ((object) method1 == null)
      {
        num = 1;
      }
      else
      {
        bool? ignoreBqlDelegate = method1.GetCustomAttribute<PXOptimizationBehaviorAttribute>()?.IgnoreBqlDelegate;
        bool flag = true;
        num = !(ignoreBqlDelegate.GetValueOrDefault() == flag & ignoreBqlDelegate.HasValue) ? 1 : 0;
      }
      if (num != 0)
      {
        MethodInfo method2 = this.BqlDelegate.Method;
        this._dependToCacheTypes = ((object) method2 != null ? method2.GetCustomAttribute<PXDependToCacheAttribute>() : (PXDependToCacheAttribute) null)?.ViewTypes;
        return (IEnumerable<System.Type>) this._dependToCacheTypes;
      }
    }
    System.Type type1 = (System.Type) null;
    List<System.Type> typeList = new List<System.Type>();
    foreach (System.Type type2 in BqlCommand.Decompose(this.BqlSelect.GetType()))
    {
      if (type1 == typeof (Current<>) || type1 == typeof (Current2<>) || type1 == typeof (Optional<>) || type1 == typeof (Optional2<>))
      {
        System.Type declaringType = type2.DeclaringType;
        if (declaringType != (System.Type) null)
          typeList.Add(this.Graph.Caches[declaringType].GenericParameter);
      }
      type1 = type2;
    }
    this._dependToCacheTypes = typeList.ToArray();
    return (IEnumerable<System.Type>) this._dependToCacheTypes;
  }

  /// <summary>Gets or sets the parent business object.</summary>
  public virtual PXGraph Graph
  {
    get => this._Graph;
    set => this._Graph = value;
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that placing retrieved data records into the cache and merging them with the cache are allowed.</summary>
  public virtual bool IsReadOnly
  {
    get => this._IsReadOnly;
    set
    {
      this._IsReadOnly = value;
      this._SelectQueries = (PXViewQueryCollection) null;
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates (if set to <tt>true</tt>) that the view also retrieves data records without default query hints.
  /// </summary>
  internal bool SkipQueryHints { get; set; }

  /// <summary>
  /// Gets or sets the value that indicates (if set to <tt>true</tt>) that the view also retrieves archived data records.
  /// </summary>
  internal bool ForceReadArchived { get; set; }

  private bool ShouldReadArchived
  {
    get
    {
      PXGraph graph = this.Graph;
      int num;
      if (graph == null)
      {
        num = 0;
      }
      else
      {
        bool? isArchiveContext = graph.IsArchiveContext;
        bool flag = true;
        num = isArchiveContext.GetValueOrDefault() == flag & isArchiveContext.HasValue ? 1 : 0;
      }
      return num != 0 || this.ForceReadArchived;
    }
  }

  /// <summary>Returns the string with the SQL query corresponding to the underlying BQL command.</summary>
  public override string ToString()
  {
    return this._Select.Context != null && this._Select.Context.LastCommandText != null ? this._Select.Context.LastCommandText : this._Select.GetQuery(this._Graph, this.RestrictedFields.Any() ? this : (PXView) null).ToString();
  }

  /// <summary>Gets the delegate representing the method (called <i>optional method</i> in this reference) which is invoked by the <tt>Select(...)</tt> method to retrieve the
  /// data. If this method is provided to the <tt>PXView</tt> object, the Select(...) method doesn't retrieve data from the database and returns the result returned
  /// by the optional method.</summary>
  public Delegate BqlDelegate => this._Delegate;

  protected internal System.Type CacheType
  {
    get
    {
      if (this._CacheType == (System.Type) null)
        this._CacheType = this._Select.GetFirstTable();
      return this._CacheType;
    }
    set => this._CacheType = value;
  }

  protected internal PXCache _Cache
  {
    get
    {
      if (this._init != null)
      {
        this._init();
        this._init = (System.Action) null;
      }
      return this._cache;
    }
    set
    {
      if (this._init != null)
        this._init = (System.Action) null;
      this._cache = value;
    }
  }

  /// <summary>Gets the cache corresponding to the first DAC mentioned in the BQL command.</summary>
  public virtual PXCache Cache
  {
    get
    {
      if (this._Cache == null || this._Graph.stateLoading)
        this._Cache = this.Graph.Caches[this.CacheType];
      return this._Cache;
    }
  }

  /// <summary>Returns the DAC type of the primary cache; that is, the first DAC referenced in the BQL command.</summary>
  public virtual System.Type GetItemType() => this.CacheType;

  public System.Type CacheGetItemType()
  {
    return this._Cache != null ? this._Cache.GetItemType() : this.Graph.Caches.GetRealCacheType(this.CacheType);
  }

  /// <summary>Gets the underlying BQL command. If the current <tt>PXView</tt> object is associated with a variant of <tt>PXSelect&lt;&gt;</tt> object, the BQL command type
  /// has the the same type parameters as the type of this object, so it represents the same SQL query.</summary>
  public virtual BqlCommand BqlSelect => this._Select;

  /// <summary>Gets the class that defines the optional method of a data view. Typically, this class is the graph that defines both the data view and its optional method. The
  /// optional method is the method represented by <tt>BqlDelegate</tt> when a data view is defined as a member of a graph.</summary>
  public virtual System.Type BqlTarget
  {
    get => (object) this._Delegate != null ? this._Delegate.Method.DeclaringType : (System.Type) null;
  }

  /// <summary>Returns all DAC types referenced in the BQL command.</summary>
  public virtual System.Type[] GetItemTypes() => this._Select.GetTables();

  /// <summary>Returns the names of the fields referenced by BQL parameters and the names of parameters of the optional method, if it is defined.</summary>
  public virtual string[] GetParameterNames()
  {
    if (this._ParameterNames == null)
    {
      List<PXViewParameter> pxViewParameterList = this.EnumParameters();
      this._ParameterNames = new string[pxViewParameterList.Count];
      foreach (PXViewParameter pxViewParameter in pxViewParameterList)
        this._ParameterNames[pxViewParameter.Ordinal] = pxViewParameter.Name;
    }
    return this._ParameterNames;
  }

  /// <summary>Returns the information on the fields referenced by BQL parameters and parameters of the optional method, if it is defined for the data view.</summary>
  public virtual List<PXViewParameter> EnumParameters()
  {
    List<PXViewParameter> pxViewParameterList = new List<PXViewParameter>();
    IBqlParameter[] parameters1 = this._Select.GetParameters();
    ParameterInfo[] parameters2 = (object) this._Delegate == null ? (ParameterInfo[]) null : this._Delegate.Method.GetParameters();
    int index1 = 0;
    for (int index2 = 0; index2 < parameters1.Length; ++index2)
    {
      if (parameters1[index2].IsVisible)
      {
        System.Type referencedType = parameters1[index2].GetReferencedType();
        if (typeof (IBqlField).IsAssignableFrom(referencedType) && referencedType.IsNested)
        {
          string str = $"{BqlCommand.GetItemType(referencedType).Name}.{referencedType.Name}";
          pxViewParameterList.Add(new PXViewParameter()
          {
            Name = str,
            Bql = parameters1[index2],
            Ordinal = pxViewParameterList.Count
          });
        }
        else if (parameters1[index2].IsArgument)
        {
          if (parameters2 == null || index1 >= parameters2.Length || referencedType != parameters2[index1].ParameterType && referencedType != parameters2[index1].ParameterType.GetElementType())
            throw new PXException("Delegate arguments do not meet the selection command parameters.");
          string name = parameters2[index1].Name;
          pxViewParameterList.Add(new PXViewParameter()
          {
            Name = name,
            Bql = parameters1[index2],
            Argument = parameters2[index1],
            Ordinal = pxViewParameterList.Count
          });
          ++index1;
        }
      }
    }
    if (parameters2 != null)
    {
      for (; index1 < parameters2.Length; ++index1)
      {
        string name = parameters2[index1].Name;
        pxViewParameterList.Add(new PXViewParameter()
        {
          Name = name,
          Argument = parameters2[index1],
          Ordinal = pxViewParameterList.Count
        });
      }
    }
    return pxViewParameterList;
  }

  protected bool IsNonStandardView
  {
    get
    {
      bool? isNonStandardView = this._isNonStandardView;
      if (isNonStandardView.HasValue)
        return isNonStandardView.GetValueOrDefault();
      return this.GetType() != typeof (PXView) || this._Delegate != null;
    }
  }

  protected PXViewQueryCollection SelectQueries
  {
    get
    {
      if (this._SelectQueries != null)
        return this._SelectQueries;
      bool isViewReadonly = this._IsReadOnly || this.RestrictedFields.Any();
      if (this.IsNonStandardView && (object) this._Delegate == null)
      {
        this._SelectQueries = new PXViewQueryCollection()
        {
          CacheType = this.CacheType,
          IsViewReadonly = isViewReadonly
        };
        this._Graph.TypedViews._NonstandardViews.Add(this._SelectQueries);
        return this._SelectQueries;
      }
      this._Graph.LoadQueryCache();
      ViewKey key = new ViewKey(this._Select.GetSelectType(), isViewReadonly);
      PXViewQueryCollection viewQueryCollection;
      if (!this._Graph.QueryCache.TryGetValue(key, out viewQueryCollection))
      {
        this._SelectQueries = new PXViewQueryCollection()
        {
          CacheType = this.CacheType,
          IsViewReadonly = isViewReadonly
        };
        this._Graph.QueryCache[key] = this._SelectQueries;
        return this._SelectQueries;
      }
      this._SelectQueries = viewQueryCollection;
      if (!this.IsReadOnly)
      {
        PXCache pxCache = (PXCache) null;
        foreach (PXQueryResult pxQueryResult in viewQueryCollection.Values)
        {
          if (!pxQueryResult.HasPlacedNotChanged)
          {
            if (pxCache == null)
            {
              pxCache = this.Cache;
              pxCache.Normalize();
            }
            if (!pxCache.GetItemType().IsAssignableFrom(viewQueryCollection.CacheType))
            {
              this._SelectQueries = new PXViewQueryCollection()
              {
                CacheType = this.CacheType,
                IsViewReadonly = false
              };
              this._Graph.QueryCache[key] = this._SelectQueries;
              break;
            }
            try
            {
              bool sortReq = false;
              List<object> items = pxQueryResult.Items;
              this.InternCache(ref items, ref sortReq);
            }
            catch (InvalidCastException ex)
            {
              this._SelectQueries = new PXViewQueryCollection()
              {
                CacheType = this.CacheType,
                IsViewReadonly = false
              };
              this._Graph.QueryCache[key] = this._SelectQueries;
              break;
            }
            pxQueryResult.HasPlacedNotChanged = true;
          }
        }
      }
      return this._SelectQueries;
    }
  }

  /// <summary>Clears the results of BQL statement execution.</summary>
  public virtual void Clear() => this.SelectQueries.Clear();

  /// <summary>Initialize a new cache for storing the results of BQL statement execution.</summary>
  public void DetachCache() => this._SelectQueries = new PXViewQueryCollection();

  internal void ReinitializeSelectQueries() => this._SelectQueries = (PXViewQueryCollection) null;

  /// <summary>Initializes an instance for executing the BQL command.</summary>
  /// <param name="graph">The graph with which the instance is associated.</param>
  /// <param name="isReadOnly">The value that indicates if updating the cache and merging data with the cache are allowed.</param>
  /// <param name="select">The BQL command as an instance of the type derived from the <tt>BqlCommand</tt> class.</param>
  public PXView(PXGraph graph, bool isReadOnly, BqlCommand select)
  {
    this.Answers = new PXView.AnswerIndexer(this);
    this._Graph = graph;
    this._IsReadOnly = isReadOnly;
    this._Select = select is BqlCommandDecorator commandDecorator ? commandDecorator.Unwrap() : select;
    if (!this._IsReadOnly)
    {
      if (!graph.Caches.CanInitLazyCache())
      {
        this._Cache = graph.Caches[this.CacheType];
      }
      else
      {
        this._init = new System.Action(this.CacheLazyLoad);
        graph.Caches.ProcessCacheMapping(graph, this.CacheType);
      }
    }
    PXExtensionManager.InitExtensions((object) this);
  }

  private void CacheLazyLoad() => this._Cache = this.Graph.Caches[this.CacheType];

  /// <summary>Initializes an instance for executing the BQL command using the provided method to retrieve data.</summary>
  /// <param name="graph">The graph with which the instance is associated.</param>
  /// <param name="isReadOnly">The value that indicates if updating the cache and merging data with the cache are allowed.</param>
  /// <param name="select">The BQL command as an instance of the type derived from the <tt>BqlCommand</tt> class.</param>
  /// <param name="handler">Either PXPrepareDelegate or PXSelectDelegate.</param>
  public PXView(PXGraph graph, bool isReadOnly, BqlCommand select, Delegate handler)
    : this(graph, isReadOnly, select)
  {
    System.Type type = (object) handler != null ? handler.GetType() : throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
    if (!type.IsGenericType)
    {
      if (!(handler is PXSelectDelegate) && type != typeof (PXPrepareDelegate))
        throw new PXException("An invalid delegate is supplied.");
    }
    else
    {
      System.Type genericTypeDefinition = type.GetGenericTypeDefinition();
      if (genericTypeDefinition != typeof (PXSelectDelegate<>) && genericTypeDefinition != typeof (PXPrepareDelegate<>) && genericTypeDefinition != typeof (PXSelectDelegate<,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,,,,>) && genericTypeDefinition != typeof (PXSelectDelegate<,,,,,,,,,,>) && genericTypeDefinition != typeof (PXPrepareDelegate<,,,,,,,,,,>))
        throw new PXException("An invalid delegate is supplied.");
    }
    this._Delegate = handler;
    PXExtensionManager.InitExtensions((object) this);
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that selecting of data records of the view's main DAC type is allowed in the UI.</summary>
  public bool AllowSelect
  {
    get => this._AllowSelect && this.Cache.AllowSelect;
    set
    {
      if (!value && PXGraph.GeneratorIsActive)
        return;
      this._AllowSelect = value;
    }
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that insertion of data records of the view's main DAC type is allowed in the UI.</summary>
  public bool AllowInsert
  {
    get => this._AllowInsert && !this.IsReadOnly && this.Cache.AllowInsert;
    set => this._AllowInsert = value;
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that the update of data records of the view's main DAC type is allowed in the UI.</summary>
  public bool AllowUpdate
  {
    get => this._AllowUpdate && !this.IsReadOnly && this.Cache.AllowUpdate;
    set => this._AllowUpdate = value;
  }

  /// <summary>Gets or sets the value that indicates (if set to <tt>true</tt>) that deletion of data records of the view's main DAC type is allowed in the UI.</summary>
  public bool AllowDelete
  {
    get => this._AllowDelete && !this.IsReadOnly && this.Cache.AllowDelete;
    set => this._AllowDelete = value;
  }

  internal System.Type ConstructSort(
    PXView.PXSearchColumn[] sortColumns,
    List<PXDataValue> pars,
    bool reverseOrder)
  {
    PXView.PXSearchColumn[] pxSearchColumnArray = this.DistinctSearches(sortColumns);
    System.Type type1 = (System.Type) null;
    List<System.Type> bqlFields = this.Cache.BqlFields;
    int count1 = pars.Count;
    for (int index1 = pxSearchColumnArray.Length - 1; index1 >= 0; --index1)
    {
      System.Type field1 = (System.Type) null;
      PXCache cache;
      string field2;
      if (this.CorrectCacheAndField(pxSearchColumnArray[index1].Column, out cache, out field2))
      {
        System.Type type2 = pxSearchColumnArray[index1].SelSort;
        if ((object) type2 == null)
          type2 = cache.GetBqlField(field2);
        field1 = type2;
      }
      else
      {
        for (int index2 = bqlFields.Count - 1; index2 >= 0; --index2)
        {
          if (string.Equals(bqlFields[index2].Name, pxSearchColumnArray[index1].Column, StringComparison.OrdinalIgnoreCase))
          {
            field1 = bqlFields[index2];
            if (!pxSearchColumnArray[index1].UseExt && pxSearchColumnArray[index1].Description?.BqlTable != (System.Type) null && !(pxSearchColumnArray[index1].Description.Expr is Column) && pxSearchColumnArray[index1].Description != null && pxSearchColumnArray[index1].Description.Expr != null && !bqlFields[index2].DeclaringType.Name.OrdinalEquals(pxSearchColumnArray[index1].Description?.BqlTable?.Name) && pxSearchColumnArray[index1].CouldUseExt)
            {
              pxSearchColumnArray[index1].UseExt = true;
              break;
            }
            break;
          }
        }
      }
      if (!pxSearchColumnArray[index1].UseExt)
      {
        if (field1 != (System.Type) null)
        {
          if (reverseOrder ^ pxSearchColumnArray[index1].Descending)
          {
            if (type1 == (System.Type) null)
              type1 = typeof (Desc<>).MakeGenericType(field1);
            else
              type1 = typeof (Desc<,>).MakeGenericType(field1, type1);
          }
          else if (type1 == (System.Type) null)
            type1 = typeof (Asc<>).MakeGenericType(field1);
          else
            type1 = typeof (Asc<,>).MakeGenericType(field1, type1);
        }
      }
      else if (pxSearchColumnArray[index1].Description != null && pxSearchColumnArray[index1].Description.Expr != null)
      {
        PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
        if (field1 != (System.Type) null && field1.IsNested && this.CacheType != this.Cache.GetItemType() && (BqlCommand.GetItemType(field1) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(field1))) && pxSearchColumnArray[index1].Description.Expr.Oper() != SQLExpression.Operation.NULL)
          this.Cache.RaiseCommandPreparing(pxSearchColumnArray[index1].Column, (object) null, pxSearchColumnArray[index1].SearchValue, PXDBOperation.External, this.CacheType, out description);
        if (description == null || description.Expr == null)
          description = pxSearchColumnArray[index1].Description;
        if (reverseOrder ^ pxSearchColumnArray[index1].Descending)
        {
          if (type1 == (System.Type) null)
            type1 = typeof (FieldNameDesc);
          else
            type1 = typeof (FieldNameDesc<>).MakeGenericType(type1);
        }
        else if (type1 == (System.Type) null)
          type1 = typeof (FieldNameAsc);
        else
          type1 = typeof (FieldNameAsc<>).MakeGenericType(type1);
        string str = description.Expr?.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
        string origField = description.Expr?.ToString();
        SQLExpression expr1 = description.Expr?.Duplicate();
        int startIndex1;
        int length1;
        if ((startIndex1 = str.IndexOf('.')) != -1 && str.IndexOf("CASE ") == -1 && str.IndexOf('(') == -1 && (length1 = pxSearchColumnArray[index1].Column.IndexOf("__")) != -1)
          str = pxSearchColumnArray[index1].Column.Substring(0, length1) + str.Substring(startIndex1);
        if (this._Select is IBqlAggregate && !string.IsNullOrEmpty(str))
        {
          if (this._Selection == null)
          {
            this._Selection = new BqlCommand.Selection();
            BqlCommand select = this._Select;
            PXGraph graph = this._Graph;
            BqlCommandInfo info = new BqlCommandInfo(false);
            info.Tables = new List<System.Type>();
            BqlCommand.Selection selection = this._Selection;
            select.GetQueryInternal(graph, info, selection);
          }
          SQLExpression expr2 = this._Selection.GetExpr(origField);
          string a1 = expr2?.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
          if (!string.IsNullOrEmpty(a1) && !string.Equals(a1, str, StringComparison.OrdinalIgnoreCase))
          {
            str = a1;
            if (expr2 != null)
              expr1 = expr2;
          }
          else
          {
            this.Cache.RaiseCommandPreparing(pxSearchColumnArray[index1].Column, (object) null, (object) null, PXDBOperation.Select, this.Cache.GetItemType(), out description);
            if (description == null || description.Expr == null)
              this.Cache.RaiseCommandPreparing(pxSearchColumnArray[index1].Column.Substring(0, pxSearchColumnArray[index1].Column.IndexOf('_')), (object) null, (object) null, PXDBOperation.Select, this.Cache.GetItemType(), out description);
            if (description != null && description.Expr != null)
            {
              string b = description.Expr.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
              SQLExpression expr3 = this._Selection.GetExpr(description.Expr?.ToString());
              string a2 = expr3?.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
              expr1.substituteNode(description.Expr, expr3);
              if (!string.IsNullOrEmpty(a2) && !string.Equals(a2, b, StringComparison.OrdinalIgnoreCase))
              {
                int length2 = str.IndexOf(b);
                while (true)
                {
                  int startIndex2;
                  switch (length2)
                  {
                    case -1:
                      goto label_52;
                    case 0:
                      if (length2 + b.Length == str.Length || !char.IsLetterOrDigit(str[length2 + b.Length]))
                      {
                        str = str.Substring(0, length2) + a2 + (length2 + b.Length == str.Length ? "" : str.Substring(length2 + b.Length));
                        startIndex2 = length2 + a2.Length + 1;
                        goto label_49;
                      }
                      break;
                    default:
                      if (char.IsLetterOrDigit(str[length2 - 1]))
                        break;
                      goto case 0;
                  }
                  startIndex2 = length2 + b.Length + 1;
label_49:
                  length2 = str.IndexOf(b, startIndex2);
                }
              }
            }
            else
            {
              int count2 = this._Selection.GrouppedBy.Count;
            }
          }
        }
label_52:
        pars.Insert(count1, (PXDataValue) new PXFieldName(str, expr1));
      }
    }
    if (!(type1 != (System.Type) null))
      return (System.Type) null;
    return typeof (OrderBy<>).MakeGenericType(type1);
  }

  internal KeyValuePair<string, bool>[] GetSortColumns(bool externalCall, bool withoutExtSort = false)
  {
    bool resetTopCount = false;
    PXView.PXSearchColumn[] pxSearchColumnArray = this.prepareSorts((string[]) null, (bool[]) null, (object[]) null, 1, out bool _, out bool _, ref resetTopCount, externalCall);
    KeyValuePair<string, bool>[] sortColumns = new KeyValuePair<string, bool>[pxSearchColumnArray.Length];
    for (int index = 0; index < pxSearchColumnArray.Length; ++index)
    {
      if (!withoutExtSort || !pxSearchColumnArray[index].CouldUseExt || pxSearchColumnArray[index].IsExternalSort)
      {
        string key = pxSearchColumnArray[index].Column;
        if (!string.IsNullOrEmpty(key))
          key = char.ToUpper(pxSearchColumnArray[index].Column[0]).ToString() + pxSearchColumnArray[index].Column.Substring(1);
        sortColumns[index] = new KeyValuePair<string, bool>(key, pxSearchColumnArray[index].Descending);
      }
    }
    return sortColumns;
  }

  /// <summary>Returns pairs of the names of the fields by which the data view result will be sorted and values indicating if the sort by the field is descending.</summary>
  internal KeyValuePair<string, bool>[] GetSortColumnsWithUpdateSelect()
  {
    return this.GetSortColumns(true);
  }

  internal KeyValuePair<string, bool>[] GetSortColumnsWithUpdateSelectWithoutExtSort()
  {
    return this.GetSortColumns(true, true);
  }

  /// <summary>Returns pairs of the names of the fields by which the data view result will be sorted and values indicating if the sort by the field is descending.</summary>
  public virtual KeyValuePair<string, bool>[] GetSortColumns() => this.GetSortColumns(true);

  protected bool HasUnboundSort(PXView.PXSearchColumn[] sorts)
  {
    for (int index = 0; index < sorts.Length; ++index)
    {
      if (sorts[index].IsUnboundSort)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Generates sort columns. Analyze Bql command sort columns, override with external sort order, append primary key.
  /// as side effect can override _Select with OrderByNew() method.
  /// </summary>
  /// <param name="sortcolumns">External sort columns</param>
  /// <param name="needOverrideSort">Output if the Bql command need to be composed with the new sort expression</param>
  /// <returns>Sort columns</returns>
  internal PXView.PXSearchColumn[] prepareSorts(
    string[] sortcolumns,
    bool[] descendings,
    object[] searches,
    int topCount,
    out bool needOverrideSort,
    out bool anySearch,
    ref bool resetTopCount,
    bool externalCall = false,
    string[] sortAsImplicitColumns = null)
  {
    if (sortcolumns != null && ((IEnumerable<string>) sortcolumns).Any<string>((Func<string, bool>) (item => item == null)))
      sortcolumns = Array.FindAll<string>(sortcolumns, (Predicate<string>) (item => item != null));
    string[] source = (string[]) null;
    bool flag1 = false;
    if (!externalCall && this.Cache.BqlTable != (System.Type) null && !string.IsNullOrEmpty(this.Cache.Identity) && !this.Cache.Keys.Contains(this.Cache.Identity))
    {
      TableIndex primaryKey = PXDatabase.Provider.SchemaCache.GetTableHeader(this.Cache.BqlTable.Name)?.GetPrimaryKey();
      if (primaryKey != null && primaryKey.Columns.Count == 2 && (string.Equals(((TableEntityBase) primaryKey.Columns[0]).Name, "CompanyID", StringComparison.OrdinalIgnoreCase) && string.Equals(((TableEntityBase) primaryKey.Columns[1]).Name, this.Cache.Identity, StringComparison.OrdinalIgnoreCase) || string.Equals(((TableEntityBase) primaryKey.Columns[1]).Name, "CompanyID", StringComparison.OrdinalIgnoreCase) && string.Equals(((TableEntityBase) primaryKey.Columns[0]).Name, this.Cache.Identity, StringComparison.OrdinalIgnoreCase)))
        source = new string[1]{ this.Cache.Identity };
    }
    if (source == null)
      source = this.Cache.Keys.ToArray();
    else
      flag1 = true;
    object row = (object) null;
    IBqlSortColumn[] sortColumns = this._Select.GetSortColumns();
    bool flag2 = false;
    anySearch = false;
    bool flag3 = false;
    HashSet<string> explicitSortColumns = this.GetExplicitSortColumns(sortcolumns, this._Select.GetExplicitSortColumns(), ((IEnumerable<string>) SortAsImplicitScope.GetSortColumns()).Union<string>((IEnumerable<string>) (sortAsImplicitColumns ?? new string[0])));
    List<System.Type> typeList;
    if (sortColumns.Length == 0)
    {
      needOverrideSort = true;
      if (sortcolumns == null || sortcolumns.Length == 0)
      {
        sortcolumns = source;
        flag2 = source.Length != 0;
      }
      else if (!CompareIgnoreCase.IsListEndingWithSublist(((IEnumerable<string>) sortcolumns).ToList<string>(), ((IEnumerable<string>) source).ToList<string>()))
      {
        List<string> list = new List<string>((IEnumerable<string>) sortcolumns);
        foreach (string str in source)
        {
          if (!flag1 || this.Cache.Keys.Count != 1 || !CompareIgnoreCase.IsInList(list, this.Cache.Keys[0]))
            list.Add(str);
        }
        sortcolumns = list.ToArray();
        if (searches != null)
        {
          List<object> objectList = new List<object>();
          List<string> stringList = new List<string>();
          int num = 0;
          int length = searches.Length;
          for (int index1 = 0; index1 < sortcolumns.Length; ++index1)
          {
            string sortcolumn = sortcolumns[index1];
            if (!stringList.Contains(sortcolumn))
            {
              objectList.Add(num < length ? searches[num++] : (object) null);
              stringList.Add(sortcolumn);
            }
            else
            {
              int index2 = list.IndexOf(sortcolumn);
              objectList.Add(objectList[index2]);
            }
          }
          searches = objectList.ToArray();
        }
      }
      typeList = new List<System.Type>((IEnumerable<System.Type>) new System.Type[sortcolumns.Length]);
    }
    else
    {
      List<string> list;
      List<bool> boolList;
      if (sortcolumns != null && sortcolumns.Length != 0)
      {
        list = new List<string>((IEnumerable<string>) sortcolumns);
        boolList = descendings == null ? new List<bool>(sortcolumns.Length) : new List<bool>((IEnumerable<bool>) descendings);
        typeList = new List<System.Type>((IEnumerable<System.Type>) new System.Type[sortcolumns.Length]);
        for (int count = boolList.Count; count < list.Count; ++count)
          boolList.Add(false);
        for (int index3 = 0; index3 < sortColumns.Length; ++index3)
        {
          System.Type referencedType = sortColumns[index3].GetReferencedType();
          if (referencedType != (System.Type) null && typeof (IBqlField).IsAssignableFrom(referencedType))
          {
            string b = !(this._Select is IBqlJoinedSelect) || BqlCommand.GetItemType(referencedType) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(referencedType)) ? referencedType.Name : $"{BqlCommand.GetItemType(referencedType).Name}__{referencedType.Name}";
            bool flag4 = false;
            for (int index4 = 0; index4 < list.Count; ++index4)
            {
              if (string.Equals(list[index4], b, StringComparison.OrdinalIgnoreCase))
              {
                flag4 = true;
                if (typeList[index4] == (System.Type) null)
                {
                  typeList[index4] = referencedType;
                  break;
                }
                break;
              }
            }
            if (!flag4)
            {
              list.Add(b);
              boolList.Add(sortColumns[index3].IsDescending);
              typeList.Add(referencedType);
            }
          }
          else
            flag3 = true;
        }
        needOverrideSort = true;
      }
      else
      {
        list = new List<string>();
        boolList = new List<bool>();
        typeList = new List<System.Type>();
        for (int index = 0; index < sortColumns.Length; ++index)
        {
          System.Type referencedType = sortColumns[index].GetReferencedType();
          if (referencedType != (System.Type) null && typeof (IBqlField).IsAssignableFrom(referencedType))
          {
            string str = !(this._Select is IBqlJoinedSelect) || BqlCommand.GetItemType(referencedType) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(referencedType)) ? referencedType.Name : $"{BqlCommand.GetItemType(referencedType).Name}__{referencedType.Name}";
            list.Add(str);
            boolList.Add(sortColumns[index].IsDescending);
            typeList.Add(referencedType);
          }
          else
            flag3 = true;
        }
        needOverrideSort = false;
      }
      foreach (string str in source)
      {
        if (!CompareIgnoreCase.IsInList(list, str) && !CompareIgnoreCase.IsInList(list, $"{this.Cache.BqlTable.Name}__{str}") && (!flag1 || this.Cache.Keys.Count != 1 || !CompareIgnoreCase.IsInList(list, this.Cache.Keys[0]) && !CompareIgnoreCase.IsInList(list, $"{this.Cache.BqlTable.Name}__{this.Cache.Keys[0]}")))
        {
          list.Add(str);
          typeList.Add((System.Type) null);
          if (!flag3)
            needOverrideSort = true;
        }
      }
      sortcolumns = list.ToArray();
      descendings = boolList.ToArray();
    }
    PXView.PXSearchColumn[] pxSearchColumnArray = new PXView.PXSearchColumn[sortcolumns.Length];
    for (int index = 0; index < sortcolumns.Length; ++index)
    {
      object search = searches == null || index >= searches.Length ? (object) null : searches[index];
      bool descending = descendings != null && index < descendings.Length && descendings[index];
      string sortcolumn = sortcolumns[index];
      PXCache cache;
      string field;
      bool corrected = this.CorrectCacheAndField(sortcolumns[index], out cache, out field);
      flag2 |= corrected;
      PXView.PXSearchColumn searchColumn = pxSearchColumnArray[index] = new PXView.PXSearchColumn(sortcolumn, descending, search);
      searchColumn.SelSort = typeList[index];
      try
      {
        if (search != null)
        {
          anySearch = true;
          row = this.RaiseFieldUpdatingForSearchColumn(cache, searchColumn, topCount, field, row, corrected, ref search);
        }
      }
      catch (Exception ex)
      {
        searchColumn.UseExt = true;
        if (search is string)
        {
          if (searchColumn.SearchValue is string)
          {
            if (((string) search).Length > ((string) searchColumn.SearchValue).Length)
            {
              if (((string) search).StartsWith((string) searchColumn.SearchValue))
                searchColumn.SearchValue = searchColumn.OrigSearchValue = search;
            }
          }
        }
      }
      bool resetTopCount1 = false;
      try
      {
        PXView.RaiseCommandPreparingForSearchColumn(cache, searchColumn, topCount, field, row, new Func<System.Type>(this.GetItemType), new Func<System.Type[]>(this.GetItemTypes), out resetTopCount1, explicitSortColumns);
      }
      catch (Exception ex)
      {
      }
      if (resetTopCount1)
        resetTopCount = true;
    }
    needOverrideSort = (needOverrideSort || ((IEnumerable<PXView.PXSearchColumn>) pxSearchColumnArray).Any<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (s => s.UseExt && s.SelSort != (System.Type) null))) && !flag3;
    if (flag2 && !flag3)
    {
      List<PXDataValue> pars = new List<PXDataValue>();
      System.Type newOrderBy = this.ConstructSort(pxSearchColumnArray, pars, false);
      if (pars.Count == 0)
      {
        this._NewOrder = newOrderBy;
        this._Select = this._Select.OrderByNew(newOrderBy);
      }
    }
    return pxSearchColumnArray;
  }

  private void prepareUnboundSorts(
    PXView.PXSearchColumn[] sortcolumns,
    int topCount,
    ref bool resetTopCount)
  {
    if (sortcolumns == null)
      return;
    object row = (object) null;
    foreach (PXView.PXSearchColumn searchColumn in ((IEnumerable<PXView.PXSearchColumn>) sortcolumns).Where<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (s => s != null && s.IsUnboundSort)))
    {
      bool resetTopCount1 = false;
      PXCache cache;
      string field;
      this.CorrectCacheAndField(searchColumn.Column, out cache, out field);
      try
      {
        PXView.RaiseCommandPreparingForSearchColumn(cache, searchColumn, topCount, field, row, new Func<System.Type>(this.GetItemType), new Func<System.Type[]>(this.GetItemTypes), out resetTopCount1);
      }
      catch (Exception ex)
      {
      }
      if (resetTopCount1)
        resetTopCount = true;
    }
  }

  private HashSet<string> GetExplicitSortColumns(
    string[] sortcolumns,
    IBqlSortColumn[] selsort,
    IEnumerable<string> sortAsImplicitColumns)
  {
    HashSet<string> explicitSortColumns = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    EnumerableExtensions.AddRange<string>((ISet<string>) explicitSortColumns, ((IEnumerable<IBqlSortColumn>) selsort).Select<IBqlSortColumn, string>((Func<IBqlSortColumn, string>) (s =>
    {
      System.Type referencedType = s.GetReferencedType();
      if (!(referencedType != (System.Type) null) || !typeof (IBqlField).IsAssignableFrom(referencedType))
        return (string) null;
      return !(this._Select is IBqlJoinedSelect) || BqlCommand.GetItemType(referencedType) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(referencedType)) ? referencedType.Name : $"{BqlCommand.GetItemType(referencedType).Name}__{referencedType.Name}";
    })).Where<string>((Func<string, bool>) (s => s != null)).Union<string>((IEnumerable<string>) (sortcolumns ?? new string[0])).Except<string>(sortAsImplicitColumns));
    return explicitSortColumns;
  }

  private object RaiseFieldUpdatingForSearchColumn(
    PXCache cache,
    PXView.PXSearchColumn searchColumn,
    int topCount,
    string columnName,
    object row,
    bool corrected,
    ref object val)
  {
    cache.RaiseFieldUpdating(columnName, row, ref val);
    if (val == null)
    {
      searchColumn.UseExt = true;
      return row;
    }
    try
    {
      if (!corrected)
      {
        if (row == null)
          row = this.Cache.CreateInstance();
        this.Cache.SetValue(row, columnName, val);
      }
    }
    catch
    {
    }
    if (topCount == 1)
      searchColumn.SearchValue = val;
    else if (this.Cache.GetStateExt(row, columnName) is PXFieldState stateExt && stateExt.DataType == val.GetType())
      searchColumn.SearchValue = !searchColumn.Descending || !(val is string) || ((string) val).Length >= stateExt.Length ? val : (object) (val?.ToString() + new string('\uF8FF', stateExt.Length - ((string) val).Length));
    else
      searchColumn.UseExt = true;
    return row;
  }

  internal static void RaiseCommandPreparingForSearchColumn(
    PXCache cache,
    PXView.PXSearchColumn searchColumn,
    int topCount,
    string columnName,
    object row,
    Func<System.Type> getItemType,
    Func<System.Type[]> getItemTypes,
    out bool resetTopCount,
    HashSet<string> explicitSortColumns = null)
  {
    resetTopCount = false;
    System.Type table = searchColumn == null || !searchColumn.Column.Contains("__") ? getItemType() : cache.GetItemType();
    PXCommandPreparingEventArgs.FieldDescription description1;
    if (topCount == 1 && searchColumn.SearchValue == null)
    {
      cache.RaiseCommandPreparing(columnName, (object) null, (object) null, PXDBOperation.Select, table, out description1);
    }
    else
    {
      cache.RaiseCommandPreparing(columnName, (object) null, searchColumn.SearchValue, PXDBOperation.External, table, out description1);
      if (description1 != null && description1.Expr != null)
      {
        PXCommandPreparingEventArgs.FieldDescription description2;
        cache.RaiseCommandPreparing(columnName, (object) null, (object) null, PXDBOperation.External, table, out description2);
        if (description2 == null || description2.Expr == null)
          description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
      }
    }
    if (description1 != null && description1.Expr != null)
    {
      PXCommandPreparingEventArgs.FieldDescription description3;
      cache.RaiseCommandPreparing(columnName, (object) null, (object) null, PXDBOperation.External, table, out description3);
      if (description3 == null || description3.Expr == null)
        description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
    }
    if (description1 != null && description1.Expr != null && description1.Expr.Oper() != SQLExpression.Operation.NULL)
    {
      searchColumn.Description = description1;
      if (description1.Expr is SubQuery || searchColumn.SearchValue == null && description1.Expr is SQLSwitch expr && !expr.IsEmbraced() && PXView.ItCanBeExt(expr, getItemTypes))
      {
        searchColumn.CouldUseExt = true;
        // ISSUE: explicit non-virtual call
        if (explicitSortColumns != null && __nonvirtual (explicitSortColumns.Contains(searchColumn.Column)))
        {
          searchColumn.UseExt = true;
          searchColumn.SearchValue = searchColumn.OrigSearchValue;
        }
      }
    }
    else
    {
      if (!(cache.GetStateExt(row, searchColumn.Column) is PXFieldState stateExt) || searchColumn.OrigSearchValue == null || stateExt.DataType == searchColumn.OrigSearchValue.GetType())
        searchColumn.SearchValue = searchColumn.OrigSearchValue;
      searchColumn.UseExt = true;
      resetTopCount = true;
    }
    // ISSUE: explicit non-virtual call
    if (explicitSortColumns == null || !__nonvirtual (explicitSortColumns.Contains(searchColumn.Column)))
      return;
    searchColumn.IsExternalSort = true;
    explicitSortColumns.Remove(searchColumn.Column);
  }

  private static bool ItCanBeExt(SQLSwitch expression, Func<System.Type[]> getItemTypes)
  {
    List<string> alowedTables = ((IEnumerable<System.Type>) getItemTypes()).Select<System.Type, string>((Func<System.Type, string>) (t => t.Name)).ToList<string>();
    foreach (Tuple<SQLExpression, SQLExpression> tuple in expression.GetCases())
    {
      if (!IsUnboundExpression(tuple.Item1) || !IsUnboundExpression(tuple.Item2))
        return false;
    }
    return IsUnboundExpression(expression.GetDefault());

    bool IsUnboundExpression(SQLExpression expr)
    {
      if (expr == null)
        return true;
      IEnumerable<string> first = expression.GetExpressionsOfType<SubQuery>().SelectMany<SubQuery, Table>((Func<SubQuery, IEnumerable<Table>>) (e => GetTablesFromQueryFrom(e.Query()))).Select<Table, string>((Func<Table, string>) (t => t.AliasOrName()));
      return !expr.GetExpressionsOfType<Column>().Select<Column, string>((Func<Column, string>) (c => c.Table().AliasOrName())).Except<string>(first.Union<string>((IEnumerable<string>) alowedTables)).Any<string>();
    }

    static IEnumerable<Table> GetTablesFromQueryFrom(Query q)
    {
      return q.GetFrom().SelectMany<Joiner, Table>((Func<Joiner, IEnumerable<Table>>) (f =>
      {
        if (f.Table() is Query q2)
          return GetTablesFromQueryFrom(q2);
        return (IEnumerable<Table>) new Table[1]
        {
          f.Table()
        };
      }));
    }
  }

  /// <summary>Prepares parameters, formats input values, gets default values for the hidden and not supplied parameters. The method returns the values that will replace the
  /// parameters including and the parameters of the custom selection method if it is defined.</summary>
  /// <param name="currents">The objects to use as current data records when
  /// processing <tt>Current</tt> and <tt>Optional</tt> parameters.</param>
  /// <param name="parameters">The explicit values for such parameters as
  /// <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  public object[] PrepareParameters(object[] currents, object[] parameters)
  {
    IBqlParameter[] parameters1 = this._Select.GetParameters();
    return this.PrepareParametersInternal(currents, parameters, parameters1);
  }

  protected virtual PXCache _GetCacheCheckTail(System.Type ft, out System.Type ct)
  {
    ct = BqlCommand.GetItemType(ft);
    if (this._PrimaryTableType != (System.Type) null && this._PrimaryTableType.IsSubclassOf(ct))
      ct = this._PrimaryTableType;
    return this._Graph.Caches[ct];
  }

  internal object[] PrepareParametersInternal(
    object[] currents,
    object[] parameters,
    IBqlParameter[] selpars)
  {
    List<object> objectList = new List<object>();
    int index1 = 0;
    int index2 = 0;
    ParameterInfo[] parameterInfoArray = (ParameterInfo[]) null;
    for (int index3 = 0; index3 < selpars.Length; ++index3)
    {
      object newValue = (object) null;
      if ((selpars[index3].IsVisible || selpars[index3] is FieldNameParam) && parameters != null && index1 < parameters.Length)
      {
        newValue = parameters[index1];
        ++index1;
      }
      if (newValue == null)
      {
        if (selpars[index3].HasDefault)
        {
          System.Type referencedType = selpars[index3].GetReferencedType();
          if (referencedType.IsNested)
          {
            System.Type ct;
            PXCache cacheCheckTail = this._GetCacheCheckTail(referencedType, out ct);
            bool flag = false;
            if (currents != null)
            {
              for (int index4 = 0; index4 < currents.Length; ++index4)
              {
                if (currents[index4] != null && (currents[index4].GetType() == ct || currents[index4].GetType().IsSubclassOf(ct)))
                {
                  newValue = cacheCheckTail.GetValue(currents[index4], referencedType.Name);
                  flag = true;
                  break;
                }
              }
            }
            if (!flag && newValue == null && (cacheCheckTail._Current ?? cacheCheckTail.Current) != null)
              newValue = cacheCheckTail.GetValue(cacheCheckTail._Current ?? cacheCheckTail.Current, referencedType.Name);
            if (newValue == null && selpars[index3].TryDefault && cacheCheckTail.RaiseFieldDefaulting(referencedType.Name, (object) null, out newValue))
              cacheCheckTail.RaiseFieldUpdating(referencedType.Name, (object) null, ref newValue);
            if (selpars[index3].MaskedType != (System.Type) null && !selpars[index3].IsArgument)
            {
              object data = cacheCheckTail._Current ?? cacheCheckTail.Current;
              if (currents != null)
              {
                for (int index5 = 0; index5 < currents.Length; ++index5)
                {
                  if (currents[index5] != null && (currents[index5].GetType() == ct || currents[index5].GetType().IsSubclassOf(ct)))
                  {
                    data = currents[index5];
                    break;
                  }
                }
              }
              newValue = GroupHelper.GetReferencedValue(cacheCheckTail, data, referencedType.Name, newValue, this._Graph._ForceUnattended);
            }
          }
        }
        else if (selpars[index3].IsArgument && (object) this._Delegate != null)
        {
          if (parameterInfoArray == null)
            parameterInfoArray = this._Delegate.Method.GetParameters();
          if (index2 < parameterInfoArray.Length)
          {
            object[] customAttributes = parameterInfoArray[index2].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false);
            foreach (PXEventSubscriberAttribute subscriberAttribute in customAttributes)
            {
              List<IPXFieldDefaultingSubscriber> defaultingSubscriberList = new List<IPXFieldDefaultingSubscriber>();
              List<IPXFieldDefaultingSubscriber> subscribers = defaultingSubscriberList;
              subscriberAttribute.GetSubscriber<IPXFieldDefaultingSubscriber>(subscribers);
              if (defaultingSubscriberList.Count > 0)
              {
                PXFieldDefaultingEventArgs e = new PXFieldDefaultingEventArgs((object) null);
                for (int index6 = 0; index6 < defaultingSubscriberList.Count; ++index6)
                  defaultingSubscriberList[index6].FieldDefaulting(this.Cache, e);
                newValue = e.NewValue;
                break;
              }
            }
            foreach (PXEventSubscriberAttribute subscriberAttribute in customAttributes)
            {
              List<IPXFieldUpdatingSubscriber> updatingSubscriberList = new List<IPXFieldUpdatingSubscriber>();
              List<IPXFieldUpdatingSubscriber> subscribers = updatingSubscriberList;
              subscriberAttribute.GetSubscriber<IPXFieldUpdatingSubscriber>(subscribers);
              if (updatingSubscriberList.Count > 0)
              {
                PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, newValue);
                for (int index7 = 0; index7 < updatingSubscriberList.Count; ++index7)
                  updatingSubscriberList[index7].FieldUpdating(this.Cache, e);
                newValue = e.NewValue;
              }
            }
          }
          ++index2;
        }
      }
      else if (selpars[index3].HasDefault)
      {
        System.Type referencedType = selpars[index3].GetReferencedType();
        if (referencedType.IsNested)
        {
          System.Type ct;
          PXCache cacheCheckTail = this._GetCacheCheckTail(referencedType, out ct);
          object obj = cacheCheckTail._Current ?? cacheCheckTail.Current;
          if (currents != null)
          {
            for (int index8 = 0; index8 < currents.Length; ++index8)
            {
              if (currents[index8] != null && (currents[index8].GetType() == ct || currents[index8].GetType().IsSubclassOf(ct)))
              {
                obj = currents[index8];
                break;
              }
            }
          }
          using (new PXReadDeletedScope())
            cacheCheckTail.RaiseFieldUpdating(referencedType.Name, obj, ref newValue);
          if (selpars[index3].MaskedType != (System.Type) null && !selpars[index3].IsArgument)
            newValue = GroupHelper.GetReferencedValue(cacheCheckTail, obj, referencedType.Name, newValue, this._Graph._ForceUnattended);
        }
      }
      else if (selpars[index3].IsArgument && (object) this._Delegate != null)
      {
        if (parameterInfoArray == null)
          parameterInfoArray = this._Delegate.Method.GetParameters();
        if (index2 < parameterInfoArray.Length)
        {
          foreach (PXEventSubscriberAttribute customAttribute in parameterInfoArray[index2].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
          {
            List<IPXFieldUpdatingSubscriber> updatingSubscriberList = new List<IPXFieldUpdatingSubscriber>();
            List<IPXFieldUpdatingSubscriber> subscribers = updatingSubscriberList;
            customAttribute.GetSubscriber<IPXFieldUpdatingSubscriber>(subscribers);
            if (updatingSubscriberList.Count > 0)
            {
              PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, newValue);
              for (int index9 = 0; index9 < updatingSubscriberList.Count; ++index9)
                updatingSubscriberList[index9].FieldUpdating(this.Cache, e);
              newValue = e.NewValue;
            }
          }
        }
        ++index2;
      }
      objectList.Add(newValue);
    }
    if (parameters != null)
    {
      for (; index1 < parameters.Length; ++index1)
      {
        object newValue = parameters[index1];
        if ((object) this._Delegate != null)
        {
          if (parameterInfoArray == null)
            parameterInfoArray = this._Delegate.Method.GetParameters();
          if (index2 < parameterInfoArray.Length)
          {
            foreach (PXEventSubscriberAttribute customAttribute in parameterInfoArray[index2].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
            {
              List<IPXFieldUpdatingSubscriber> updatingSubscriberList = new List<IPXFieldUpdatingSubscriber>();
              List<IPXFieldUpdatingSubscriber> subscribers = updatingSubscriberList;
              customAttribute.GetSubscriber<IPXFieldUpdatingSubscriber>(subscribers);
              if (updatingSubscriberList.Count > 0)
              {
                PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, newValue);
                for (int index10 = 0; index10 < updatingSubscriberList.Count; ++index10)
                  updatingSubscriberList[index10].FieldUpdating(this.Cache, e);
                newValue = e.NewValue;
              }
            }
          }
          ++index2;
        }
        objectList.Add(newValue);
      }
    }
    return objectList.ToArray();
  }

  private bool CorrectCacheAndField(string originField, out PXCache cache, out string field)
  {
    cache = this.Cache;
    return PXView.CorrectCacheAndField(this._Graph, this._Select, originField, ref cache, out field);
  }

  internal static bool CorrectCacheAndField(
    PXGraph graph,
    BqlCommand bqlCommand,
    string originField,
    ref PXCache cache,
    out string field)
  {
    field = originField;
    bool flag = false;
    int length = originField.IndexOf("__", StringComparison.Ordinal);
    if (length > -1)
    {
      string str = originField.Substring(length + 2);
      System.Type[] tables = bqlCommand.GetTables();
      string tableName = originField.Substring(0, length);
      Func<System.Type, bool> predicate = (Func<System.Type, bool>) (t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
      PXCache pxCache = ((IEnumerable<System.Type>) tables).Where<System.Type>(predicate).Select<System.Type, PXCache>((Func<System.Type, PXCache>) (t => graph.Caches[t])).FirstOrDefault<PXCache>();
      if (pxCache != null)
      {
        cache = pxCache;
        field = str;
        flag = true;
      }
    }
    return flag;
  }

  private object GetStateFromCorrectCache(string dataField)
  {
    return PXView.GetStateFromCorrectCache(this.Graph, this._Select, this.Cache, dataField);
  }

  private static object GetStateFromCorrectCache(
    PXGraph graph,
    BqlCommand bqlCommand,
    PXCache cache,
    string dataField)
  {
    PXCache cache1 = cache;
    string field;
    PXView.CorrectCacheAndField(graph, bqlCommand, dataField, ref cache1, out field);
    return cache1.GetStateExt((object) null, field);
  }

  private IEnumerable<PXEventSubscriberAttribute> GetAttributesFromCorrectCache(string dataField)
  {
    return PXView.GetAttributesFromCorrectCache(this.Graph, this._Select, this.Cache, dataField);
  }

  private static IEnumerable<PXEventSubscriberAttribute> GetAttributesFromCorrectCache(
    PXGraph graph,
    BqlCommand bqlCommand,
    PXCache cache,
    string dataField)
  {
    PXCache cache1 = cache;
    string field;
    PXView.CorrectCacheAndField(graph, bqlCommand, dataField, ref cache1, out field);
    return (IEnumerable<PXEventSubscriberAttribute>) cache1.GetAttributesReadonly(field, true);
  }

  protected internal bool prepareFilters(ref PXFilterRow[] filters, string forceView = null)
  {
    return PXView.prepareFilters(this.Graph, this.Cache, this._Select, ref filters, forceView, isNonStandardView: this.IsNonStandardView);
  }

  internal static void ExpandINNIFilters(
    PXGraph graph,
    PXCache originalCache,
    BqlCommand bqlCommand,
    ref PXFilterRow[] filters)
  {
    if (filters == null)
      return;
    List<PXFilterRow> list = ((IEnumerable<PXFilterRow>) filters).ToList<PXFilterRow>();
    bool flag = false;
    foreach (PXFilterRow originalFilter in filters)
    {
      if ((originalFilter.Condition == PXCondition.IN || originalFilter.Condition == PXCondition.NI) && !originalFilter.Variable.HasValue && (object) (originalFilter.Value as System.Type) == null)
      {
        string b = "<null>";
        string[] strArray = originalFilter.Value.ToString().Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
        int num;
        int index = num = list.IndexOf(originalFilter);
        list.Remove(originalFilter);
        flag = true;
        bool isNegative = originalFilter.Condition == PXCondition.NI;
        foreach (string str in strArray)
        {
          if (string.Equals(str, b, StringComparison.OrdinalIgnoreCase))
          {
            PXCondition condition = isNegative ? PXCondition.ISNOTNULL : PXCondition.ISNULL;
            PXFilterRow subfilter = PXView.CreateSubfilter(originalFilter, condition, isNegative: isNegative);
            list.Insert(num++, subfilter);
          }
          else
          {
            FilterVariableType? variableType = FilterVariable.GetVariableType(str);
            PXCondition condition = variableType.HasValue ? originalFilter.Condition : (isNegative ? PXCondition.NE : PXCondition.EQ);
            PXFilterRow subfilter = PXView.CreateSubfilter(originalFilter, condition, str, isNegative);
            subfilter.Variable = variableType;
            list.Insert(num++, subfilter);
          }
        }
        PXFilterRow pxFilterRow = list.ElementAt<PXFilterRow>(num - 1);
        list.ElementAt<PXFilterRow>(index).OpenBrackets = originalFilter.OpenBrackets + 1;
        pxFilterRow.OrOperator = originalFilter.OrOperator;
        pxFilterRow.CloseBrackets = originalFilter.CloseBrackets + 1;
      }
    }
    if (!flag)
      return;
    filters = list.ToArray();
  }

  internal static bool prepareFilters(
    PXGraph graph,
    PXCache originalCache,
    BqlCommand bqlCommand,
    ref PXFilterRow[] filters,
    string forceView = null,
    bool cleanDescription = true,
    bool isNonStandardView = false)
  {
    PXView.ExpandINNIFilters(graph, originalCache, bqlCommand, ref filters);
    bool flag1 = false;
    if (filters != null)
    {
      PXCache cache = originalCache;
      int num1 = 0;
      List<PXFilterRow> list = ((IEnumerable<PXFilterRow>) filters).ToList<PXFilterRow>();
      bool flag2 = false;
      foreach (PXFilterRow originalFilter in filters)
      {
        if (PXView.GetStateFromCorrectCache(graph, bqlCommand, originalCache, originalFilter.DataField) is PXFieldState fromCorrectCache)
        {
          if (fromCorrectCache is PXStringState pxStringState && pxStringState.MultiSelect && originalFilter.Value != null)
          {
            string b = "<null>";
            string[] strArray = originalFilter.Value.ToString().Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries);
            if (originalFilter.Condition == PXCondition.LIKE)
            {
              int num2;
              int index1 = num2 = list.IndexOf(originalFilter);
              list.Remove(originalFilter);
              flag2 = true;
              originalFilter.UseExt = true;
              foreach (string a in strArray)
              {
                if (string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
                {
                  PXCondition condition = PXCondition.ISNULL;
                  PXFilterRow subfilter = PXView.CreateSubfilter(originalFilter, condition);
                  list.Insert(num2++, subfilter);
                }
                else
                {
                  PXCondition condition1 = PXCondition.EQ;
                  PXFilterRow subfilter1 = PXView.CreateSubfilter(originalFilter, condition1, a);
                  List<PXFilterRow> pxFilterRowList1 = list;
                  int index2 = num2;
                  int num3 = index2 + 1;
                  PXFilterRow pxFilterRow1 = subfilter1;
                  pxFilterRowList1.Insert(index2, pxFilterRow1);
                  string str1 = $",{a},";
                  PXCondition condition2 = PXCondition.LIKE;
                  PXFilterRow subfilter2 = PXView.CreateSubfilter(originalFilter, condition2, str1);
                  List<PXFilterRow> pxFilterRowList2 = list;
                  int index3 = num3;
                  int num4 = index3 + 1;
                  PXFilterRow pxFilterRow2 = subfilter2;
                  pxFilterRowList2.Insert(index3, pxFilterRow2);
                  string str2 = a + ",";
                  PXCondition condition3 = PXCondition.RLIKE;
                  PXFilterRow subfilter3 = PXView.CreateSubfilter(originalFilter, condition3, str2);
                  List<PXFilterRow> pxFilterRowList3 = list;
                  int index4 = num4;
                  int num5 = index4 + 1;
                  PXFilterRow pxFilterRow3 = subfilter3;
                  pxFilterRowList3.Insert(index4, pxFilterRow3);
                  string str3 = "," + a;
                  PXCondition condition4 = PXCondition.LLIKE;
                  PXFilterRow subfilter4 = PXView.CreateSubfilter(originalFilter, condition4, str3);
                  List<PXFilterRow> pxFilterRowList4 = list;
                  int index5 = num5;
                  num2 = index5 + 1;
                  PXFilterRow pxFilterRow4 = subfilter4;
                  pxFilterRowList4.Insert(index5, pxFilterRow4);
                }
              }
              PXFilterRow pxFilterRow = list.ElementAt<PXFilterRow>(num2 - 1);
              list.ElementAt<PXFilterRow>(index1).OpenBrackets = originalFilter.OpenBrackets + 1;
              pxFilterRow.OrOperator = originalFilter.OrOperator;
              pxFilterRow.CloseBrackets = originalFilter.CloseBrackets + 1;
            }
          }
          if (fromCorrectCache is PXDateState)
          {
            PXDBDateAttribute pxdbDateAttribute = (PXDBDateAttribute) PXView.GetAttributesFromCorrectCache(graph, bqlCommand, originalCache, originalFilter.DataField).FirstOrDefault<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBDateAttribute));
            if (pxdbDateAttribute != null && pxdbDateAttribute.PreserveTime)
            {
              System.DateTime result;
              System.DateTime? nullable1 = System.DateTime.TryParse(originalFilter.Value as string, (IFormatProvider) cache.Graph.Culture, DateTimeStyles.None, out result) ? new System.DateTime?(result) : originalFilter.Value as System.DateTime?;
              System.DateTime? nullable2 = System.DateTime.TryParse(originalFilter.Value2 as string, (IFormatProvider) cache.Graph.Culture, DateTimeStyles.None, out result) ? new System.DateTime?(result) : originalFilter.Value2 as System.DateTime?;
              if (nullable1.HasValue)
              {
                System.DateTime dateTime = nullable1.Value;
                if (dateTime.TimeOfDay == TimeSpan.Zero)
                {
                  switch (originalFilter.Condition)
                  {
                    case PXCondition.EQ:
                      int index6 = list.IndexOf(originalFilter);
                      PXFilterRow pxFilterRow5 = (PXFilterRow) originalFilter.Clone();
                      PXFilterRow pxFilterRow6 = (PXFilterRow) originalFilter.Clone();
                      pxFilterRow5.OrOperator = false;
                      pxFilterRow5.Condition = PXCondition.GE;
                      pxFilterRow5.CloseBrackets = 0;
                      pxFilterRow6.Condition = PXCondition.LT;
                      pxFilterRow6.OpenBrackets = 0;
                      PXFilterRow pxFilterRow7 = pxFilterRow6;
                      dateTime = nullable1.Value;
                      // ISSUE: variable of a boxed type
                      __Boxed<System.DateTime> local1 = (ValueType) dateTime.AddDays(1.0);
                      pxFilterRow7.Value = (object) local1;
                      list.Remove(originalFilter);
                      list.Insert(index6, pxFilterRow6);
                      list.Insert(index6, pxFilterRow5);
                      flag2 = true;
                      continue;
                    case PXCondition.NE:
                      int index7 = list.IndexOf(originalFilter);
                      PXFilterRow pxFilterRow8 = (PXFilterRow) originalFilter.Clone();
                      PXFilterRow pxFilterRow9 = (PXFilterRow) originalFilter.Clone();
                      pxFilterRow8.OrOperator = false;
                      pxFilterRow8.Condition = PXCondition.LT;
                      pxFilterRow8.CloseBrackets = 0;
                      pxFilterRow9.Condition = PXCondition.GE;
                      pxFilterRow9.OpenBrackets = 0;
                      PXFilterRow pxFilterRow10 = pxFilterRow9;
                      dateTime = nullable1.Value;
                      // ISSUE: variable of a boxed type
                      __Boxed<System.DateTime> local2 = (ValueType) dateTime.AddDays(1.0);
                      pxFilterRow10.Value = (object) local2;
                      list.Remove(originalFilter);
                      list.Insert(index7, pxFilterRow9);
                      list.Insert(index7, pxFilterRow8);
                      flag2 = true;
                      continue;
                    case PXCondition.BETWEEN:
                      if (nullable2.HasValue)
                      {
                        dateTime = nullable2.Value;
                        if (dateTime.TimeOfDay == TimeSpan.Zero)
                        {
                          int index8 = list.IndexOf(originalFilter);
                          PXFilterRow pxFilterRow11 = (PXFilterRow) originalFilter.Clone();
                          PXFilterRow pxFilterRow12 = (PXFilterRow) originalFilter.Clone();
                          pxFilterRow11.OrOperator = false;
                          pxFilterRow11.Condition = PXCondition.GE;
                          pxFilterRow11.CloseBrackets = 0;
                          pxFilterRow11.Value2 = (object) null;
                          pxFilterRow12.Condition = PXCondition.LT;
                          pxFilterRow12.OpenBrackets = 0;
                          PXFilterRow pxFilterRow13 = pxFilterRow12;
                          dateTime = nullable2.Value;
                          // ISSUE: variable of a boxed type
                          __Boxed<System.DateTime> local3 = (ValueType) dateTime.AddDays(1.0);
                          pxFilterRow13.Value = (object) local3;
                          pxFilterRow12.Value2 = (object) null;
                          list.Remove(originalFilter);
                          list.Insert(index8, pxFilterRow12);
                          list.Insert(index8, pxFilterRow11);
                          flag2 = true;
                          continue;
                        }
                        continue;
                      }
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
          }
        }
      }
      if (flag2)
        filters = list.ToArray();
      foreach (PXFilterRow pxFilterRow14 in filters)
      {
        PXFilterRow fr = pxFilterRow14;
        if (!string.IsNullOrEmpty(fr.DataField))
        {
          fr.OrigValue = fr.Value;
          fr.OrigValue2 = fr.Value2;
          if (num1 > 0)
          {
            num1 = num1 + fr.OpenBrackets - fr.CloseBrackets;
            if (cache == originalCache)
            {
              fr.Value = (object) null;
              fr.Value2 = (object) null;
              continue;
            }
          }
          else
            cache = forceView == null ? originalCache : graph.Views[forceView].Cache;
          string str4 = fr.DataField;
          PXCache cache1 = originalCache;
          string field;
          bool flag3 = PXView.CorrectCacheAndField(graph, bqlCommand, fr.DataField, ref cache1, out field);
          if (flag3)
          {
            cache = cache1;
            str4 = field;
          }
          PXFieldState state = cache.GetStateExt((object) null, str4) as PXFieldState;
          if (fr.Condition == PXCondition.EQ && fr.Value is bool && !(bool) fr.Value)
          {
            fr.Condition = PXCondition.NE;
            fr.OrigValue = fr.Value = (object) true;
          }
          if (fr.Value is string && (fr.Condition == PXCondition.EQ && string.Equals((string) fr.Value, "False", StringComparison.OrdinalIgnoreCase) || fr.Condition == PXCondition.NE && string.Equals((string) fr.Value, "True", StringComparison.OrdinalIgnoreCase)) && state != null && state.DataType == typeof (bool))
          {
            fr.Condition = PXCondition.NE;
            fr.OrigValue = fr.Value = (object) true;
          }
          if (fr.Value == null && (fr.Condition == PXCondition.EQ || fr.Condition == PXCondition.LIKE || fr.Condition == PXCondition.LLIKE || fr.Condition == PXCondition.RLIKE))
            fr.Condition = PXCondition.ISNULL;
          if (fr.Value == null && (fr.Condition == PXCondition.NE || fr.Condition == PXCondition.NOTLIKE))
            fr.Condition = PXCondition.ISNOTNULL;
          if (!fr.Variable.HasValue)
            fr.Variable = FilterVariable.GetVariableType(fr.Value as string);
          if (!(fr.Value is string) || FilterVariable.GetConditionViolationMessage(fr.Value as string, fr.Condition) == null)
          {
            if (fr.Variable.HasValue)
            {
              fr.DataField = str4 = fr.DataField.RemoveFromEnd("_description", StringComparison.OrdinalIgnoreCase);
              state = cache.GetStateExt((object) null, str4) as PXFieldState;
            }
            if (RelativeDatesManager.IsRelativeDatesString(fr.Value as string))
              fr.Value = (object) RelativeDatesManager.EvaluateAsDateTime(fr.Value as string);
            if (RelativeDatesManager.IsRelativeDatesString(fr.Value2 as string))
              fr.Value2 = (object) RelativeDatesManager.EvaluateAsDateTime(fr.Value2 as string);
            PXCommandPreparingEventArgs.FieldDescription descr;
            switch (fr.Condition)
            {
              case PXCondition.EQ:
              case PXCondition.NE:
                PXDBOperation operation = PXDBOperation.Select;
                if (PXDBLocalizableStringAttribute.IsEnabled && cache.GetAttributes(str4).OfType<PXDBLocalizableStringAttribute>().Any<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (l => l.MultiLingual)) || cache._ReportGetFirstKeyValueStored(str4) != null || cache._ReportGetFirstKeyValueAttribute(str4) != null)
                {
                  operation |= PXDBOperation.External;
                  fr.UseExt = true;
                }
                if (!flag3)
                {
                  FilterVariableType? variable1 = fr.Variable;
                  FilterVariableType filterVariableType1 = FilterVariableType.CurrentUser;
                  if (variable1.GetValueOrDefault() == filterVariableType1 & variable1.HasValue)
                  {
                    PXCommandPreparingEventArgs.FieldDescription description;
                    cache.RaiseCommandPreparing(str4, (object) null, (object) null, operation, cache.GetItemType(), out description);
                    if (description != null && description.Expr != null)
                    {
                      PXFilterRow pxFilterRow15 = fr;
                      object obj1;
                      switch (description.DataType)
                      {
                        case PXDbType.Int:
                          obj1 = (object) PXAccess.GetContactID();
                          break;
                        case PXDbType.NVarChar:
                          obj1 = (object) PXAccess.GetUserName();
                          break;
                        case PXDbType.UniqueIdentifier:
                          obj1 = (object) PXAccess.GetUserID();
                          break;
                        case PXDbType.VarChar:
                          obj1 = (object) PXAccess.GetUserName();
                          break;
                        default:
                          obj1 = fr.Value;
                          break;
                      }
                      pxFilterRow15.Value = obj1;
                      if (fr.Value != null)
                      {
                        if (state != null && state.DataType != fr.Value.GetType())
                        {
                          object returnValue = fr.Value;
                          cache.RaiseFieldSelecting(str4, (object) null, ref returnValue, false);
                          object obj2 = PXFieldState.UnwrapValue(returnValue);
                          fr.UseExt = fr.UseExt | obj2 != null && obj2 != fr.Value;
                          fr.Value = obj2 ?? fr.Value;
                        }
                      }
                      else
                        continue;
                    }
                    else
                      continue;
                  }
                  else
                  {
                    FilterVariableType? variable2 = fr.Variable;
                    FilterVariableType filterVariableType2 = FilterVariableType.CurrentBranch;
                    if (variable2.GetValueOrDefault() == filterVariableType2 & variable2.HasValue)
                    {
                      PXCommandPreparingEventArgs.FieldDescription description;
                      cache.RaiseCommandPreparing(str4, (object) null, (object) null, operation, cache.GetItemType(), out description);
                      if (description != null && description.Expr != null && description.DataType == PXDbType.Int)
                      {
                        fr.Value = (object) PXAccess.GetBranchID();
                        if (fr.Value != null)
                        {
                          if (state != null && state.DataType != fr.Value.GetType())
                          {
                            object returnValue = fr.Value;
                            cache.RaiseFieldSelecting(str4, (object) null, ref returnValue, false);
                            object obj = PXFieldState.UnwrapValue(returnValue);
                            fr.UseExt = fr.UseExt | obj != null && obj != fr.Value;
                            fr.Value = obj ?? (object) PXAccess.GetBranchID();
                          }
                        }
                        else
                          continue;
                      }
                      else
                        continue;
                    }
                  }
                }
                if (fr.Value != null)
                {
                  object obj = fr.Value;
                  bool flag4 = false;
                  try
                  {
                    if (fr.Value is string b)
                    {
                      object newValue = !(state is PXStringState pxStringState) || string.IsNullOrEmpty(pxStringState.InputMask) || !Mask.IsMasked(b, pxStringState.InputMask, true) ? (object) b : (object) Mask.Parse(pxStringState.InputMask, b);
                      cache.RaiseFieldUpdating(str4, (object) null, ref newValue);
                      if (newValue is string)
                        fr.Value = (object) (string) newValue;
                      else if (fr.Tag != null && string.Equals(fr.Tag.ToString(), "Export", StringComparison.InvariantCultureIgnoreCase) || string.Equals(newValue.ToString(), b, StringComparison.CurrentCultureIgnoreCase) || newValue is System.DateTime)
                      {
                        fr.Value = newValue;
                      }
                      else
                      {
                        operation |= PXDBOperation.External;
                        fr.UseExt = true;
                      }
                    }
                    else
                    {
                      object newValue = fr.Value;
                      cache.RaiseFieldUpdating(str4, (object) null, ref newValue);
                      if (newValue != null)
                        fr.Value = newValue;
                    }
                  }
                  catch (Exception ex)
                  {
                    fr.UseExt = true;
                    flag4 = true;
                    if (ex is PXSetPropertyException)
                    {
                      if (((PXSetPropertyException) ex).ErrorValue is string)
                      {
                        if (fr.Value is string)
                          fr.Value = ((PXSetPropertyException) ex).ErrorValue;
                      }
                    }
                  }
                  if (fr.Value != null)
                  {
                    try
                    {
                      descr = (PXCommandPreparingEventArgs.FieldDescription) null;
                      if (!flag4)
                        PXView.RaiseCommandPreparingForFilterValue(cache, str4, fr.Value, operation, state, out descr);
                      if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                      {
                        fr.Description = descr;
                        fr.UseExt = UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, true);
                      }
                      else
                      {
                        PXView.RaiseCommandPreparingForFilterValue(cache, str4, fr.Value, PXDBOperation.External, state, out descr);
                        bool? nullable3 = new bool?();
                        if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                        {
                          if (flag4)
                          {
                            nullable3 = new bool?(UseExtShouldBeSetForFilterBasedOnStateOrExpression(true, false));
                            bool? nullable4 = nullable3;
                            bool flag5 = true;
                            if (!(nullable4.GetValueOrDefault() == flag5 & nullable4.HasValue))
                              goto label_121;
                          }
                          fr.Description = descr;
                          fr.UseExt = ((int) nullable3 ?? (UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false) ? 1 : 0)) != 0;
                          goto label_123;
                        }
label_121:
                        flag1 = true;
                        fr.UseExt = fr.UseExt || str4.IndexOf("_", StringComparison.Ordinal) != -1 || originalCache.GetBqlField(fr.DataField) == (System.Type) null;
                      }
                    }
                    catch
                    {
                      flag1 = true;
                    }
label_123:
                    FilterVariableType? variable3 = fr.Variable;
                    FilterVariableType filterVariableType3 = FilterVariableType.CurrentUser;
                    if (!(variable3.GetValueOrDefault() == filterVariableType3 & variable3.HasValue))
                    {
                      FilterVariableType? variable4 = fr.Variable;
                      FilterVariableType filterVariableType4 = FilterVariableType.CurrentBranch;
                      if (!(variable4.GetValueOrDefault() == filterVariableType4 & variable4.HasValue))
                        break;
                    }
                    fr.Value = obj;
                    break;
                  }
                  break;
                }
                break;
              case PXCondition.GT:
              case PXCondition.GE:
              case PXCondition.LT:
              case PXCondition.LE:
                if (fr.Value != null)
                {
                  object obj = fr.Value;
                  try
                  {
                    try
                    {
                      cache.RaiseFieldUpdating(str4, (object) null, ref obj);
                    }
                    catch
                    {
                      fr.Value = obj;
                      throw;
                    }
                    if (obj != null)
                    {
                      if (state != null && state.DataType == obj.GetType())
                      {
                        fr.Value = obj;
                      }
                      else
                      {
                        fr.UseExt = true;
                        if (state != null)
                        {
                          cache.RaiseFieldSelecting(str4, (object) null, ref obj, false);
                          if (obj is PXFieldState)
                            obj = ((PXFieldState) obj).Value;
                          if (obj != null)
                          {
                            if (obj.GetType() == state.DataType)
                              fr.Value = obj;
                          }
                        }
                      }
                    }
                    else
                      fr.UseExt = true;
                  }
                  catch
                  {
                    fr.UseExt = true;
                  }
                  try
                  {
                    PXView.RaiseCommandPreparingForFilterValue(cache, str4, fr.Value, PXDBOperation.External, state, out descr);
                    if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                    {
                      fr.Description = descr;
                      fr.UseExt = UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false);
                      break;
                    }
                    fr.UseExt = true;
                    flag1 = true;
                    break;
                  }
                  catch
                  {
                    flag1 = true;
                    break;
                  }
                }
                else
                  break;
              case PXCondition.LIKE:
              case PXCondition.RLIKE:
              case PXCondition.LLIKE:
              case PXCondition.NOTLIKE:
                if (fr.Value is string str6)
                {
                  bool flag6 = false;
                  try
                  {
                    object newValue = (object) str6;
                    if (state is PXStringState pxStringState && !string.IsNullOrEmpty(pxStringState.InputMask))
                    {
                      if (Mask.IsMasked(str6, pxStringState.InputMask, true))
                      {
                        newValue = (object) Mask.Parse(pxStringState.InputMask, str6);
                      }
                      else
                      {
                        for (int startIndex = 0; startIndex < pxStringState.InputMask.Length - str6.Length + 1; ++startIndex)
                        {
                          string str5 = pxStringState.InputMask.Substring(startIndex, str6.Length);
                          if (Mask.IsMasked(str6, str5, true))
                          {
                            newValue = (object) Mask.Parse(str5, str6);
                            break;
                          }
                        }
                      }
                    }
                    cache.RaiseFieldUpdating(str4, (object) null, ref newValue);
                    if (newValue is string)
                    {
                      string[] allowedLabels1 = pxStringState.AllowedLabels;
                      if ((allowedLabels1 != null ? (allowedLabels1.Length != 0 ? 1 : 0) : 0) == 0)
                      {
                        if (state is PXIntState pxIntState)
                        {
                          string[] allowedLabels2 = pxIntState.AllowedLabels;
                          if ((allowedLabels2 != null ? (allowedLabels2.Length != 0 ? 1 : 0) : 0) != 0)
                            goto label_160;
                        }
                        if (pxStringState != null && pxStringState.Length > 0 && str6.Length > pxStringState.Length && ((string) newValue).Length < str6.Length)
                        {
                          if (str6.StartsWith((string) newValue))
                            goto label_166;
                        }
                        str6 = (string) newValue;
                        goto label_166;
                      }
label_160:
                      flag6 = true;
                      fr.UseExt = true;
                    }
                    else
                      fr.UseExt = true;
                  }
                  catch
                  {
                    fr.UseExt = true;
                  }
label_166:
                  string searchText = str6.TrimEnd();
                  fr.Value = (object) searchText;
                  string filterValue = PXView.PrepareLikeCondition(fr.Condition, graph.SqlDialect, searchText);
                  try
                  {
                    PXView.RaiseCommandPreparingForFilterValue(cache, str4, (object) filterValue, PXDBOperation.External, state, out descr);
                    if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                    {
                      if (descr.DataLength.HasValue)
                      {
                        int num6 = descr.DataLength.Value + (fr.Condition == PXCondition.LIKE || fr.Condition == PXCondition.NOTLIKE ? 2 : 1);
                        switch (fr.Condition)
                        {
                          case PXCondition.LIKE:
                          case PXCondition.RLIKE:
                          case PXCondition.LLIKE:
                          case PXCondition.NOTLIKE:
                            switch (descr.DataType)
                            {
                              case PXDbType.BigInt:
                              case PXDbType.Int:
                              case PXDbType.SmallInt:
                              case PXDbType.TinyInt:
                                descr.DataType = PXDbType.VarChar;
                                break;
                            }
                            break;
                        }
                        PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
                        if (flag6)
                          cache.RaiseCommandPreparing(str4, (object) null, (object) null, PXDBOperation.External, cache.GetItemType(), out description);
                        fr.Description = new PXCommandPreparingEventArgs.FieldDescription(descr.BqlTable, description?.Expr ?? descr.Expr, descr.DataType, new int?(num6), descr.DataValue, descr.IsRestriction);
                      }
                      else
                        fr.Description = descr;
                      fr.UseExt = UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false);
                      break;
                    }
                    flag1 = true;
                    fr.UseExt = fr.UseExt || str4.IndexOf("_") != -1;
                    break;
                  }
                  catch
                  {
                    fr.UseExt = true;
                    flag1 = true;
                    break;
                  }
                }
                else
                {
                  if (!(fr.Value is int))
                  {
                    fr.Value = (object) null;
                    break;
                  }
                  break;
                }
              case PXCondition.BETWEEN:
              case PXCondition.TODAY:
              case PXCondition.OVERDUE:
              case PXCondition.TODAY_OVERDUE:
              case PXCondition.TOMMOROW:
              case PXCondition.THIS_WEEK:
              case PXCondition.NEXT_WEEK:
              case PXCondition.THIS_MONTH:
              case PXCondition.NEXT_MONTH:
                if (fr.Condition != PXCondition.BETWEEN)
                {
                  System.DateTime dateTime = new System.DateTime(1900, 1, 1);
                  fr.Value = (object) dateTime;
                  fr.Value2 = (object) dateTime;
                  object dateRange = (object) PXView.DateTimeFactory.GetDateRange(fr.Condition, graph.Accessinfo.BusinessDate);
                  if ((dateRange as System.DateTime?[])[0].HasValue)
                    fr.Value = (object) (dateRange as System.DateTime?[])[0].Value;
                  if ((dateRange as System.DateTime?[])[1].HasValue)
                    fr.Value2 = (object) (dateRange as System.DateTime?[])[1].Value;
                }
                if (fr.Value != null && fr.Value2 != null)
                {
                  object obj3 = fr.Value;
                  object obj4 = fr.Value2;
                  try
                  {
                    Exception exception = (Exception) null;
                    try
                    {
                      cache.RaiseFieldUpdating(str4, (object) null, ref obj3);
                    }
                    catch (Exception ex)
                    {
                      fr.Value = obj3;
                      exception = ex;
                    }
                    try
                    {
                      cache.RaiseFieldUpdating(str4, (object) null, ref obj4);
                    }
                    catch
                    {
                      fr.Value2 = obj4;
                      throw;
                    }
                    if (exception != null)
                      throw exception;
                    if (obj3 != null && obj4 != null)
                    {
                      if (state != null && state.DataType == obj3.GetType() && state.DataType == obj4.GetType())
                      {
                        fr.Value = obj3;
                        fr.Value2 = obj4;
                      }
                      else
                      {
                        fr.UseExt = true;
                        if (state != null)
                        {
                          cache.RaiseFieldSelecting(str4, (object) null, ref obj3, false);
                          if (obj3 is PXFieldState)
                            obj3 = ((PXFieldState) obj3).Value;
                          if (obj3 != null && obj3.GetType() == state.DataType)
                            fr.Value = obj3;
                          cache.RaiseFieldSelecting(str4, (object) null, ref obj4, false);
                          if (obj4 is PXFieldState)
                            obj4 = ((PXFieldState) obj4).Value;
                          if (obj4 != null)
                          {
                            if (obj4.GetType() == state.DataType)
                              fr.Value2 = obj4;
                          }
                        }
                      }
                    }
                    else
                      fr.UseExt = true;
                  }
                  catch
                  {
                    fr.UseExt = true;
                  }
                  try
                  {
                    PXCommandPreparingEventArgs.FieldDescription fieldDescription;
                    PXView.RaiseCommandPreparingForFilterValue(cache, str4, fr.Value2, PXDBOperation.External, state, out fieldDescription);
                    if (fieldDescription != null && fieldDescription.Expr != null && fieldDescription.Expr.Oper() != SQLExpression.Operation.NULL)
                    {
                      PXView.RaiseCommandPreparingForFilterValue(cache, str4, fr.Value, PXDBOperation.External, state, out descr);
                      if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                      {
                        fr.Description = descr;
                        fr.Description2 = fieldDescription;
                        fr.UseExt = UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false);
                        break;
                      }
                      flag1 = true;
                      fr.UseExt = fr.UseExt || str4.IndexOf("_") != -1;
                      break;
                    }
                    fr.UseExt = true;
                    flag1 = true;
                    break;
                  }
                  catch
                  {
                    flag1 = true;
                    break;
                  }
                }
                else
                  break;
              case PXCondition.ISNULL:
              case PXCondition.ISNOTNULL:
                try
                {
                  cache.RaiseCommandPreparing(str4, (object) null, (object) null, PXDBOperation.Select, cache.GetItemType(), out descr);
                  if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                  {
                    if (CanApplyNullIf(descr.Expr))
                    {
                      descr.Expr = descr.Expr.NullIf((SQLExpression) new SQLConst((object) string.Empty));
                      fr.UseExt = true;
                    }
                    fr.Description = descr;
                    fr.UseExt = UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false);
                    break;
                  }
                  bool flag7 = false;
                  try
                  {
                    object newValue = (object) null;
                    cache.RaiseFieldUpdating(str4, (object) null, ref newValue);
                  }
                  catch (Exception ex)
                  {
                    flag7 = true;
                  }
                  cache.RaiseCommandPreparing(str4, (object) null, (object) null, PXDBOperation.External, cache.GetItemType(), out descr);
                  bool? nullable5 = new bool?();
                  if (descr != null && descr.Expr != null && descr.Expr.Oper() != SQLExpression.Operation.NULL)
                  {
                    if (flag7)
                    {
                      nullable5 = new bool?(UseExtShouldBeSetForFilterBasedOnStateOrExpression(true, false));
                      bool? nullable6 = nullable5;
                      bool flag8 = true;
                      if (!(nullable6.GetValueOrDefault() == flag8 & nullable6.HasValue))
                        goto label_195;
                    }
                    if (CanApplyNullIf(descr.Expr))
                      descr.Expr = descr.Expr.NullIf((SQLExpression) new SQLConst((object) string.Empty));
                    fr.Description = descr;
                    fr.UseExt = ((int) nullable5 ?? (UseExtShouldBeSetForFilterBasedOnStateOrExpression(false, false) ? 1 : 0)) != 0;
                    break;
                  }
label_195:
                  flag1 = true;
                  fr.UseExt = fr.UseExt || str4.IndexOf("_") != -1;
                  break;
                }
                catch
                {
                  flag1 = true;
                  break;
                }
              case PXCondition.IN:
              case PXCondition.NI:
                if ((object) (fr.Value as System.Type) != null)
                  fr.Value = (object) ((System.Type) fr.Value).FullName;
                if (cache != originalCache && str4 == fr.DataField)
                {
                  cache = originalCache;
                  fr.Value = (object) null;
                  fr.Value2 = (object) null;
                  break;
                }
                if (fr.Value is string)
                {
                  FilterVariableType? variable5 = fr.Variable;
                  FilterVariableType filterVariableType5 = FilterVariableType.CurrentUserGroups;
                  if (!(variable5.GetValueOrDefault() == filterVariableType5 & variable5.HasValue))
                  {
                    FilterVariableType? variable6 = fr.Variable;
                    FilterVariableType filterVariableType6 = FilterVariableType.CurrentUserGroupsTree;
                    if (!(variable6.GetValueOrDefault() == filterVariableType6 & variable6.HasValue))
                    {
                      FilterVariableType? variable7 = fr.Variable;
                      FilterVariableType filterVariableType7 = FilterVariableType.CurrentOrganization;
                      if (!(variable7.GetValueOrDefault() == filterVariableType7 & variable7.HasValue))
                      {
                        if (!fr.Variable.HasValue)
                        {
                          System.Type type = PXBuildManager.GetType((string) fr.Value, false);
                          if (type != (System.Type) null && typeof (IBqlField).IsAssignableFrom(type) && type.IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(type)))
                          {
                            fr.Value = (object) type;
                            cache = graph.Caches[BqlCommand.GetItemType(type)];
                          }
                          else
                          {
                            fr.Value = (object) null;
                            fr.Value2 = (object) null;
                          }
                          num1 = fr.OpenBrackets - fr.CloseBrackets;
                          break;
                        }
                        break;
                      }
                    }
                  }
                  if (cache.GetBqlField(fr.DataField) != (System.Type) null && cache.GetFieldType(fr.DataField) == typeof (int))
                  {
                    FilterVariableType? variable8 = fr.Variable;
                    if (variable8.HasValue)
                    {
                      object obj;
                      switch (variable8.GetValueOrDefault())
                      {
                        case FilterVariableType.CurrentUserGroups:
                          obj = (object) UserGroupLazyCache.Current.GetUserGroupIds(PXAccess.GetUserID()).ToArray<int>();
                          break;
                        case FilterVariableType.CurrentUserGroupsTree:
                          obj = (object) UserGroupLazyCache.Current.GetUserWorkTreeIds(PXAccess.GetUserID()).ToArray<int>();
                          break;
                        case FilterVariableType.CurrentOrganization:
                          obj = (object) PXAccess.GetBranchIDsForCurrentOrganization();
                          break;
                        default:
                          goto label_67;
                      }
                      cache.RaiseCommandPreparing(fr.DataField, (object) null, obj, PXDBOperation.Select, cache.GetItemType(), out descr);
                      fr.Description = descr;
                      break;
                    }
label_67:
                    throw new NotSupportedException();
                  }
                  flag1 = true;
                  if (fr.DataField.Contains<char>('_'))
                  {
                    fr.UseExt = true;
                    break;
                  }
                  break;
                }
                cache = originalCache;
                fr.Value = (object) null;
                fr.Value2 = (object) null;
                num1 = fr.OpenBrackets - fr.CloseBrackets;
                break;
              case PXCondition.ER:
                fr.DataField = typeof (Note.noteID).Name;
                PXView.RaiseCommandPreparingForFilterValue(new PXGraph().Caches[typeof (Note)], typeof (Note.externalKey).Name, fr.Value, PXDBOperation.Select, typeof (Note), state, out descr);
                if (descr != null)
                {
                  fr.Description = descr;
                  break;
                }
                break;
              case PXCondition.NestedSelector:
                PXFilterRow[] filters1 = new PXFilterRow[1]
                {
                  (PXFilterRow) fr.Value2
                };
                PXView.prepareFilters(graph, originalCache, bqlCommand, ref filters1, (string) fr.Value);
                break;
            }
            if (fr.Condition != PXCondition.NestedSelector)
            {
              if (fr.Description != null & cleanDescription)
              {
                cache.RaiseCommandPreparing(str4, (object) null, (object) null, PXDBOperation.External, cache.GetItemType(), out descr);
                if (descr == null || descr.Expr == null)
                {
                  fr.Description = (PXCommandPreparingEventArgs.FieldDescription) null;
                  fr.Description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
                  if (fr.UseExt)
                  {
                    FilterVariableType? variable9 = fr.Variable;
                    FilterVariableType filterVariableType8 = FilterVariableType.CurrentUser;
                    if (!(variable9.GetValueOrDefault() == filterVariableType8 & variable9.HasValue))
                    {
                      FilterVariableType? variable10 = fr.Variable;
                      FilterVariableType filterVariableType9 = FilterVariableType.CurrentBranch;
                      if (!(variable10.GetValueOrDefault() == filterVariableType9 & variable10.HasValue))
                        goto label_254;
                    }
                  }
                  object returnValue1 = fr.Value;
                  if (state != null)
                  {
                    if (returnValue1 != null && state.DataType != returnValue1.GetType())
                    {
                      cache.RaiseFieldSelecting(str4, (object) null, ref returnValue1, false);
                      if (returnValue1 is PXFieldState)
                        returnValue1 = ((PXFieldState) returnValue1).Value;
                      if (returnValue1 != null && returnValue1.GetType() == state.DataType)
                        fr.Value = returnValue1;
                      object returnValue2 = fr.Value2;
                      if (returnValue2 != null && state.DataType != returnValue2.GetType())
                      {
                        cache.RaiseFieldSelecting(str4, (object) null, ref returnValue2, false);
                        if (returnValue2 is PXFieldState)
                          returnValue2 = ((PXFieldState) returnValue2).Value;
                        if (returnValue2 != null && returnValue2.GetType() == state.DataType)
                          fr.Value2 = returnValue2;
                      }
                    }
                    else
                    {
                      FilterVariableType? variable11 = fr.Variable;
                      FilterVariableType filterVariableType10 = FilterVariableType.CurrentUser;
                      if (!(variable11.GetValueOrDefault() == filterVariableType10 & variable11.HasValue))
                      {
                        FilterVariableType? variable12 = fr.Variable;
                        FilterVariableType filterVariableType11 = FilterVariableType.CurrentBranch;
                        if (!(variable12.GetValueOrDefault() == filterVariableType11 & variable12.HasValue) && fr.OrigValue != null && state.DataType == fr.OrigValue.GetType())
                        {
                          fr.Value = fr.OrigValue;
                          fr.Value2 = fr.OrigValue2;
                        }
                      }
                    }
                  }
label_254:
                  fr.UseExt = true;
                  flag1 = true;
                }
              }
              else if (fr.UseExt && fr.OrigValue != null && state != null && state.DataType == fr.OrigValue.GetType())
              {
                fr.Value = fr.OrigValue;
                fr.Value2 = fr.OrigValue2;
              }
            }
            if (cache.BqlSelect != null && !fr.UseExt)
            {
              if (fr.Description?.Expr != null)
                fr.Description.Expr = (SQLExpression) new Column(str4, cache.GetItemType());
              if (fr.Description2?.Expr != null)
                fr.Description2.Expr = (SQLExpression) new Column(str4, cache.GetItemType());
            }

            bool UseExtShouldBeSetForFilterBasedOnStateOrExpression(
              bool skipSelfCheck = false,
              bool ignoreSelector = false)
            {
              SQLExpression sqlExpression = descr.Expr.Duplicate();
              List<SubQuery> expressionsOfType = sqlExpression.GetExpressionsOfType<SubQuery>();
              bool flag1 = expressionsOfType.Any<SubQuery>();
              foreach (SubQuery from in expressionsOfType)
                sqlExpression = sqlExpression.substituteNode((SQLExpression) from, SQLExpression.Null());
              bool flag2 = sqlExpression.GetExpressionsOfType<Column>().All<Column>((Func<Column, bool>) (c => c.Table().AliasOrName().OrdinalIgnoreCaseEquals(cache.GetItemType().Name)));
              if (flag1 & flag2 || ((skipSelfCheck ? 0 : (fr.UseExt ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
                return true;
              if (!(flag2 | isNonStandardView) || !(cache.GetBqlField(fr.DataField) == (System.Type) null))
                return false;
              if (!(descr.Expr is Column))
                return true;
              return !ignoreSelector && state.SelectorMode != 0;
            }
          }
        }
      }
    }
    return flag1;

    bool CanApplyNullIf(SQLExpression v)
    {
      if (graph.IsContractBasedAPI)
        return false;
      switch (v.GetDBType())
      {
        case PXDbType.NChar:
        case PXDbType.NText:
        case PXDbType.NVarChar:
        case PXDbType.Text:
        case PXDbType.VarChar:
        case PXDbType.Variant:
        case PXDbType.Xml:
          return true;
        default:
          return false;
      }
    }
  }

  private static string PrepareLikeCondition(
    PXCondition condition,
    ISqlDialect dialect,
    string searchText)
  {
    string str = dialect.PrepareLikeCondition(searchText);
    string wildcardAnything = dialect.WildcardAnything;
    if (condition == PXCondition.RLIKE)
      return str + wildcardAnything;
    return condition == PXCondition.LLIKE ? wildcardAnything + str : wildcardAnything + str + wildcardAnything;
  }

  private static PXFilterRow CreateSubfilter(
    PXFilterRow originalFilter,
    PXCondition condition,
    string value = null,
    bool isNegative = false)
  {
    PXFilterRow subfilter = (PXFilterRow) originalFilter.Clone();
    subfilter.Value = (object) value;
    subfilter.OrOperator = !isNegative;
    subfilter.Condition = condition;
    subfilter.OpenBrackets = 0;
    subfilter.CloseBrackets = 0;
    return subfilter;
  }

  private static void RaiseCommandPreparingForFilterValue(
    PXCache cache,
    string fieldName,
    object filterValue,
    PXDBOperation operation,
    PXFieldState state,
    out PXCommandPreparingEventArgs.FieldDescription fieldDescription)
  {
    System.Type itemType = cache.GetItemType();
    PXView.RaiseCommandPreparingForFilterValue(cache, fieldName, filterValue, operation, itemType, state, out fieldDescription);
  }

  /// <summary>
  /// Raises the cache command preparing field event for filter value with a special treatment for the <see cref="T:PX.Data.PXDBDateAttribute" />. The <see cref="T:PX.Data.PXDBDateAttribute" /> may convert the date filter
  /// value from local time zone to UTC if its flags <see cref="P:PX.Data.PXDBDateAttribute.UseTimeZone" /> and
  /// <see cref="P:PX.Data.PXDBDateAttribute.PreserveTime" /> both are set. This results in a shift in the filter value and a value 01.09.2020 00:00 corresponding to the 1st of September 2020
  /// will be shifted to a 01.09.2020 05:00 which will return incorrect filtered results. Depending on the time zone it can even result in a different date. This is unwanted therefore for filter
  /// values these flags on  <see cref="T:PX.Data.PXDBDateAttribute" /> attribute are temporarily cleared to prevent time zone conversions.
  /// </summary>
  /// <param name="cache">The cache.</param>
  /// <param name="fieldName">Name of the field on which  cache command preparing event should be raised.</param>
  /// <param name="filterValue">The filter value.</param>
  /// <param name="operation">The operation.</param>
  /// <param name="table">The table to use for the command preparing event.</param>
  /// <param name="state">The field state.</param>
  /// <param name="fieldDescription">[out] Field description.</param>
  private static void RaiseCommandPreparingForFilterValue(
    PXCache cache,
    string fieldName,
    object filterValue,
    PXDBOperation operation,
    System.Type table,
    PXFieldState state,
    out PXCommandPreparingEventArgs.FieldDescription fieldDescription)
  {
    if ((filterValue is System.DateTime || filterValue is System.DateTime? || state?.DataType == typeof (System.DateTime) ? 1 : (state?.DataType == typeof (System.DateTime?) ? 1 : 0)) == 0)
    {
      cache.RaiseCommandPreparing(fieldName, (object) null, filterValue, operation, table, out fieldDescription);
    }
    else
    {
      List<PXDBDateAttribute> list = cache.GetAttributesReadonly(fieldName).OfType<PXDBDateAttribute>().Where<PXDBDateAttribute>((Func<PXDBDateAttribute, bool>) (a => a.UseTimeZone)).ToList<PXDBDateAttribute>();
      try
      {
        list.ForEach((System.Action<PXDBDateAttribute>) (a => a.UseTimeZone = false));
        cache.RaiseCommandPreparing(fieldName, (object) null, filterValue, operation, table, out fieldDescription);
      }
      finally
      {
        list.ForEach((System.Action<PXDBDateAttribute>) (a => a.UseTimeZone = true));
      }
    }
  }

  private static void CreateInConditionForIntArray(
    object dataValue,
    System.Type field,
    bool inCondition,
    out System.Type expression,
    out PXDataValue parameter)
  {
    parameter = (PXDataValue) null;
    if (dataValue is int[] numArray && numArray.Length != 0)
    {
      parameter = new PXDataValue(PXDbType.DirectExpression, dataValue);
      if (inCondition)
        expression = typeof (Where<,>).MakeGenericType(field, typeof (In<>).MakeGenericType(typeof (Required<>).MakeGenericType(field)));
      else
        expression = typeof (Where<,>).MakeGenericType(field, typeof (NotIn<>).MakeGenericType(typeof (Required<>).MakeGenericType(field)));
    }
    else
      expression = typeof (Where<True, Equal<False>>);
  }

  private BqlCommand appendFilters(
    PXFilterRow[] filters,
    List<PXDataValue> pars,
    BqlCommand cmd,
    string forceView = null)
  {
    PXCache cache = string.IsNullOrEmpty(forceView) ? this.Cache : this.Graph.Views[forceView].Cache;
    return PXView.AppendFiltersToCommand(cmd, filters, pars, cache, this.Graph);
  }

  internal static BqlCommand AppendFiltersToCommand(
    BqlCommand cmd,
    PXFilterRow[] filters,
    List<PXDataValue> pars,
    PXCache cache,
    PXGraph graph)
  {
    return PXView.AppendFiltersToCommand(cmd, filters, pars, cache.BqlFields, graph, cache);
  }

  internal static BqlCommand AppendFiltersToCommand(
    BqlCommand cmd,
    PXFilterRow[] filters,
    List<PXDataValue> pars,
    List<System.Type> bqlFields,
    PXGraph graph,
    PXCache cache = null)
  {
    if (filters != null)
    {
      int num1 = 0;
      foreach (PXFilterRow filter in filters)
        num1 = num1 + filter.OpenBrackets - filter.CloseBrackets;
      if (num1 > 0)
        filters[filters.Length - 1].CloseBrackets += num1;
      else if (num1 < 0)
        filters[0].OpenBrackets -= num1;
      System.Type[] typeArray = new System.Type[filters.Length];
      int num2 = 0;
      int num3 = 0;
      PXCache pxCache = (PXCache) null;
      FilterVariableType? variable;
      for (int index1 = 0; index1 < filters.Length; ++index1)
      {
        PXFilterRow filter = filters[index1];
        System.Type field1 = (System.Type) null;
        if (num3 > 0)
        {
          field1 = pxCache.GetBqlField(filter.DataField);
          num3 = num3 + filter.OpenBrackets - filter.CloseBrackets;
        }
        else
        {
          for (int index2 = bqlFields.Count - 1; index2 >= 0; --index2)
          {
            System.Type bqlField = bqlFields[index2];
            if (string.Compare(bqlField.Name, filter.DataField, StringComparison.OrdinalIgnoreCase) == 0)
            {
              field1 = bqlField;
              break;
            }
          }
        }
        System.Type type1 = (System.Type) null;
        if (field1 != (System.Type) null && !filter.UseExt)
          type1 = typeof (Required<>).MakeGenericType(field1);
        else if (filter.Description != null && filter.Description.Expr != null)
        {
          field1 = typeof (FieldNameParam);
          type1 = typeof (Argument<object>);
        }
        bool flag = filter.Value is string && FilterVariable.GetConditionViolationMessage(filter.Value as string, filter.Condition) != null;
        if (field1 != (System.Type) null && !flag)
        {
          int count = pars.Count;
          switch (filter.Condition)
          {
            case PXCondition.EQ:
              if (filter.Description != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Equal<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.NE:
              if (filter.Description != null)
              {
                if (filter.OrigValue is bool && (bool) filter.OrigValue)
                  typeArray[index1] = typeof (Where<,,>).MakeGenericType(field1, typeof (NotEqual<>).MakeGenericType(type1), typeof (Or<,>).MakeGenericType(field1, typeof (IsNull)));
                else
                  typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (NotEqual<>).MakeGenericType(type1));
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                break;
              }
              break;
            case PXCondition.GT:
              if (filter.Description != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Greater<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.GE:
              if (filter.Description != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (GreaterEqual<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.LT:
              if (filter.Description != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Less<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.LE:
              if (filter.Description != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (LessEqual<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.LIKE:
            case PXCondition.RLIKE:
            case PXCondition.LLIKE:
              if (filter.Description != null)
              {
                List<PXDataValue> pxDataValueList = pars;
                int dataType = (int) filter.Description.DataType;
                int? dataLength = filter.Description.DataLength;
                int length = ((string) filter.Description.DataValue).Length;
                int? valueLength = dataLength.GetValueOrDefault() <= length & dataLength.HasValue ? filter.Description.DataLength : new int?(((string) filter.Description.DataValue).Length);
                object dataValue = filter.Description.DataValue;
                PXDataValue pxDataValue = new PXDataValue((PXDbType) dataType, valueLength, dataValue);
                pxDataValueList.Add(pxDataValue);
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Like<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.NOTLIKE:
              if (filter.Description != null)
              {
                List<PXDataValue> pxDataValueList = pars;
                int dataType = (int) filter.Description.DataType;
                int? dataLength = filter.Description.DataLength;
                int length = ((string) filter.Description.DataValue).Length;
                int? valueLength = dataLength.GetValueOrDefault() <= length & dataLength.HasValue ? filter.Description.DataLength : new int?(((string) filter.Description.DataValue).Length);
                object dataValue = filter.Description.DataValue;
                PXDataValue pxDataValue = new PXDataValue((PXDbType) dataType, valueLength, dataValue);
                pxDataValueList.Add(pxDataValue);
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (NotLike<>).MakeGenericType(type1));
                break;
              }
              break;
            case PXCondition.BETWEEN:
            case PXCondition.TODAY:
            case PXCondition.OVERDUE:
            case PXCondition.TOMMOROW:
            case PXCondition.THIS_WEEK:
            case PXCondition.THIS_MONTH:
              if (filter.Description != null && filter.Description2 != null)
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                pars.Add(new PXDataValue(filter.Description2.DataType, filter.Description2.DataLength, filter.Description2.DataValue));
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Between<,>).MakeGenericType(type1, type1));
                break;
              }
              break;
            case PXCondition.ISNULL:
              if (filter.Description != null)
              {
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (IsNull));
                break;
              }
              break;
            case PXCondition.ISNOTNULL:
              if (filter.Description != null)
              {
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (IsNotNull));
                break;
              }
              break;
            case PXCondition.TODAY_OVERDUE:
            case PXCondition.NEXT_WEEK:
            case PXCondition.NEXT_MONTH:
              if (filter.Description != null)
              {
                System.DateTime?[] dateRange = PXView.DateTimeFactory.GetDateRange(filter.Condition, new System.DateTime?());
                System.DateTime? nullable1 = dateRange[0];
                System.DateTime? nullable2 = dateRange[1];
                System.Type type2 = (System.Type) null;
                if (nullable1.HasValue)
                {
                  pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, (object) nullable1));
                  type2 = typeof (GreaterEqual<>).MakeGenericType(type1);
                }
                if (nullable2.HasValue)
                {
                  pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, (object) nullable2));
                  type2 = typeof (LessEqual<>).MakeGenericType(type1);
                }
                if (nullable1.HasValue && nullable2.HasValue)
                  type2 = typeof (Between<,>).MakeGenericType(type1, type1);
                if (type2 != (System.Type) null)
                {
                  typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, type2);
                  break;
                }
                break;
              }
              break;
            case PXCondition.IN:
            case PXCondition.NI:
              if ((object) (filter.Value as System.Type) != null)
              {
                typeArray[index1] = typeof (Where<,>).MakeGenericType(field1, typeof (Equal<>).MakeGenericType((System.Type) filter.Value));
                pxCache = graph.Caches[BqlCommand.GetItemType((System.Type) filter.Value)];
                num3 = filter.OpenBrackets - filter.CloseBrackets;
                break;
              }
              variable = filter.Variable;
              FilterVariableType filterVariableType1 = FilterVariableType.CurrentUserGroups;
              if (!(variable.GetValueOrDefault() == filterVariableType1 & variable.HasValue))
              {
                variable = filter.Variable;
                FilterVariableType filterVariableType2 = FilterVariableType.CurrentUserGroupsTree;
                if (!(variable.GetValueOrDefault() == filterVariableType2 & variable.HasValue))
                {
                  variable = filter.Variable;
                  FilterVariableType filterVariableType3 = FilterVariableType.CurrentOrganization;
                  if (!(variable.GetValueOrDefault() == filterVariableType3 & variable.HasValue))
                    break;
                }
              }
              if (filter.Description != null)
              {
                System.Type expression;
                PXDataValue parameter;
                PXView.CreateInConditionForIntArray(filter.Description.DataValue, field1, filter.Condition == PXCondition.IN, out expression, out parameter);
                if (expression != (System.Type) null)
                  typeArray[index1] = expression;
                if (parameter != null)
                {
                  pars.Add(parameter);
                  break;
                }
                break;
              }
              break;
            case PXCondition.ER:
              if (string.Compare(field1.Name, "noteID", StringComparison.InvariantCultureIgnoreCase) == 0 && !string.IsNullOrEmpty(filter.Value as string))
              {
                pars.Add(new PXDataValue(filter.Description.DataType, filter.Description.DataLength, filter.Description.DataValue));
                typeArray[index1] = typeof (Where<,,>).MakeGenericType(field1, typeof (Equal<>).MakeGenericType(typeof (Note.noteID)), typeof (And<,>).MakeGenericType(typeof (Note.externalKey), typeof (Like<>).MakeGenericType(typeof (Required<>).MakeGenericType(typeof (Note.externalKey)))));
                break;
              }
              break;
            case PXCondition.NestedSelector:
              typeArray[index1] = PXView.AppendNestedSelectorFilters(field1, filter, pars, cmd, graph);
              break;
          }
          if (field1 == typeof (FieldNameParam) && typeArray[index1] != (System.Type) null)
          {
            SQLExpression expr = filter.Description.Expr.Duplicate();
            int length = filters[index1].DataField.IndexOf("__");
            if (expr is Column column1 && column1.Table() != null && column1.Name.IndexOf("__") == -1 && length != -1)
            {
              SimpleTable t = new SimpleTable(filters[index1].DataField.Substring(0, length));
              t.Alias = PXView.GetAliasForFilterTable(column1.Table() as SimpleTable);
              expr = (SQLExpression) new Column(column1.Name, (Table) t);
            }
            if (length != -1 && expr.LExpr() is Column column2 && column2.Table()?.Alias != null && column2.Name.IndexOf("__") == -1)
              column2.substituteTableName(column2.Table().Alias, filters[index1].DataField.Substring(0, length));
            if (length == -1 && filter.UseExt && expr.Oper() == SQLExpression.Operation.NULL_IF && !expr.LExpr().GetExpressionsOfType<SubQuery>().Any<SubQuery>())
            {
              PXCommandPreparingEventArgs.FieldDescription description;
              cache.RaiseCommandPreparing(filter.DataField, (object) null, (object) null, PXDBOperation.External, cache.GetItemType(), out description);
              if (!(description?.Expr is Column) && !(description?.Expr is SubQuery))
                expr.substituteNode(description?.Expr, (SQLExpression) new Column(filter.DataField, description.BqlTable.Name));
            }
            string field2 = expr.SQLQuery(graph.SqlDialect.GetConnection()).ToString();
            pars.Insert(count, (PXDataValue) new PXFieldName(field2, expr));
            if (filter.Condition == PXCondition.NE && filter.OrigValue is bool && (bool) filter.OrigValue)
              pars.Add((PXDataValue) new PXFieldName(field2, expr));
          }
        }
        if (typeArray[index1] == (System.Type) null)
          typeArray[index1] = typeof (Where<True, Equal<True>>);
        if (typeArray[index1] != (System.Type) null)
          ++num2;
      }
      System.Type where = (System.Type) null;
      if (num2 > 0)
      {
        List<System.Type> typeList = new List<System.Type>();
        List<int> intList = new List<int>();
        intList.Add(typeList.Count);
        typeList.Add(typeof (Where<>));
        for (int index = 0; index < filters[0].OpenBrackets - filters[0].CloseBrackets; ++index)
        {
          intList.Add(typeList.Count);
          typeList.Add(typeof (Where<>));
        }
        for (int index3 = 0; index3 < filters.Length; ++index3)
        {
          if (typeArray[index3] != (System.Type) null)
          {
            if (index3 > 0)
            {
              System.Type c = typeList[intList[intList.Count - 1]];
              if (typeof (Where<>).IsAssignableFrom(c))
                typeList[intList[intList.Count - 1]] = typeof (Where2<,>);
              else if (typeof (And<>).IsAssignableFrom(c))
                typeList[intList[intList.Count - 1]] = typeof (And2<,>);
              else if (typeof (Or<>).IsAssignableFrom(c))
                typeList[intList[intList.Count - 1]] = typeof (Or2<,>);
              intList[intList.Count - 1] = typeList.Count;
              if (filters[index3 - 1].OrOperator)
                typeList.Add(typeof (Or<>));
              else
                typeList.Add(typeof (And<>));
              for (int index4 = 0; index4 < filters[index3].OpenBrackets - filters[index3].CloseBrackets; ++index4)
              {
                intList.Add(typeList.Count);
                typeList.Add(typeof (Where<>));
              }
              for (int index5 = 0; index5 < filters[index3].CloseBrackets - filters[index3].OpenBrackets; ++index5)
                intList.RemoveAt(intList.Count - 1);
            }
            variable = filters[index3].Variable;
            bool flag = !variable.HasValue && (!(filters[index3].Value is string) || FilterVariable.GetConditionViolationMessage(filters[index3].Value as string, filters[index3].Condition) == null);
            if (filters[index3].Condition == PXCondition.IN & flag)
            {
              typeList.Add(typeof (Exists<>));
              typeList.Add(typeof (PX.Data.Select<,>));
              typeList.Add(BqlCommand.GetItemType((System.Type) filters[index3].Value));
            }
            else if (filters[index3].Condition == PXCondition.NI & flag)
            {
              typeList.Add(typeof (NotExists<>));
              typeList.Add(typeof (PX.Data.Select<,>));
              typeList.Add(BqlCommand.GetItemType((System.Type) filters[index3].Value));
            }
            else if (filters[index3].Condition == PXCondition.ER)
            {
              typeList.Add(typeof (Exists<>));
              typeList.Add(typeof (PX.Data.Select<,>));
              typeList.Add(typeof (Note));
            }
            typeList.Add(typeArray[index3]);
          }
        }
        where = BqlCommand.Compose(typeList.ToArray());
      }
      if (where != (System.Type) null)
        cmd = cmd.WhereAnd(where);
    }
    return cmd;
  }

  private static string GetAliasForFilterTable(SimpleTable table)
  {
    return table != null && table.Alias != null && !table.Alias.OrdinalIgnoreCaseEquals(table.Name) && table.Alias.IndexOf("_", StringComparison.Ordinal) > 0 ? table.Alias : (string) null;
  }

  private static System.Type AppendNestedSelectorFilters(
    System.Type field,
    PXFilterRow filter,
    List<PXDataValue> pars,
    BqlCommand cmd,
    PXGraph graph)
  {
    string key = (string) filter.Value;
    PXView view = graph.Views[key];
    PXFilterRow pxFilterRow = (PXFilterRow) filter.Value2;
    List<System.Type> source = new List<System.Type>();
    System.Type[] array = BqlCommand.KillAllWheres((IEnumerable<System.Type>) BqlCommand.Decompose(view.BqlSelect.GetSelectType())).ToArray<System.Type>();
    for (int index = 0; index < array.Length; ++index)
    {
      if (array[index] == typeof (Current<>))
        source.Add(array[index + 1].DeclaringType);
    }
    BqlCommand instance = BqlCommand.CreateInstance(BqlCommand.Compose(array));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    BqlCommand cmd1 = source.Aggregate<System.Type, BqlCommand>(instance, PXView.\u003C\u003EO.\u003C0\u003E__Deparametrize ?? (PXView.\u003C\u003EO.\u003C0\u003E__Deparametrize = new Func<BqlCommand, System.Type, BqlCommand>(BqlCommand.Deparametrize)));
    pars.AddRange(((IEnumerable<object>) view.PrepareParametersInternal((object[]) null, (object[]) null, cmd1.GetParameters())).Select<object, PXDataValue>((Func<object, PXDataValue>) (o => new PXDataValue(o))));
    System.Type field1 = ((IBqlSearch) view.BqlSelect).GetField();
    return typeof (Where<>).MakeGenericType(typeof (Exists<>).MakeGenericType(PXView.AppendFiltersToCommand(cmd1, new PXFilterRow[1]
    {
      pxFilterRow
    }, pars, view.Cache, graph).WhereAnd(typeof (Where<,>).MakeGenericType(field1, typeof (Equal<>).MakeGenericType(field))).GetType()));
  }

  private BqlCommand appendSearches(
    PXView.PXSearchColumn[] sortColumns,
    List<PXDataValue> pars,
    BqlCommand cmd,
    ref int topCount,
    bool reverseOrder,
    out PXDataValue firstsearch)
  {
    PXView.PXSearchColumn[] pxSearchColumnArray = this.DistinctSearches(sortColumns);
    firstsearch = (PXDataValue) null;
    if (pxSearchColumnArray != null && topCount > 0)
    {
      IBqlSortColumn[] sortColumns1 = cmd.GetSortColumns();
      System.Type where = (System.Type) null;
      int count = pars.Count;
      for (int index1 = pxSearchColumnArray.Length - 1; index1 >= 0; --index1)
      {
        if (index1 >= sortColumns1.Length)
          topCount = 0;
        else if (pxSearchColumnArray[index1].SearchValue != null && pxSearchColumnArray[index1].Description != null)
        {
          PXFieldName pxFieldName = (PXFieldName) null;
          System.Type field1 = (System.Type) null;
          if (sortColumns1[index1].GetReferencedType() == (System.Type) null)
          {
            for (int index2 = this.Cache.BqlFields.Count - 1; index2 >= 0; --index2)
            {
              if (string.Compare(this.Cache.BqlFields[index2].Name, pxSearchColumnArray[index1].Column, StringComparison.OrdinalIgnoreCase) == 0)
              {
                field1 = this.Cache.BqlFields[index2];
                break;
              }
            }
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            if (field1 != (System.Type) null && field1.IsNested && this.CacheType != this.Cache.GetItemType() && (BqlCommand.GetItemType(field1) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(field1))) && pxSearchColumnArray[index1].Description.Expr.Oper() != SQLExpression.Operation.NULL)
              this.Cache.RaiseCommandPreparing(pxSearchColumnArray[index1].Column, (object) null, pxSearchColumnArray[index1].SearchValue, PXDBOperation.External, this.CacheType, out description);
            if (description == null || description.Expr.Oper() != SQLExpression.Operation.NULL)
              description = pxSearchColumnArray[index1].Description;
            string field2 = description.Expr.SQLQuery(this.Graph.SqlDialect.GetConnection()).ToString();
            int startIndex;
            int length;
            if ((startIndex = field2.IndexOf('.')) != -1 && field2.IndexOf('(') == -1 && (length = pxSearchColumnArray[index1].Column.IndexOf("__")) != -1)
              field2 = pxSearchColumnArray[index1].Column.Substring(0, length) + field2.Substring(startIndex);
            pxFieldName = new PXFieldName(field2, description.Expr);
          }
          PXDataValue pxDataValue = new PXDataValue(pxSearchColumnArray[index1].Description.DataType, pxSearchColumnArray[index1].Description.DataLength, pxSearchColumnArray[index1].Description.DataValue);
          if (index1 == 0)
            firstsearch = pxDataValue;
          if (count != pars.Count && topCount != 1 | reverseOrder)
          {
            pars.Insert(count, pxDataValue);
            if (pxFieldName != null)
              pars.Insert(count, (PXDataValue) pxFieldName);
          }
          pars.Insert(count, pxDataValue);
          if (pxFieldName != null)
            pars.Insert(count, (PXDataValue) pxFieldName);
          System.Type type1 = sortColumns1[index1].GetReferencedType();
          System.Type type2;
          if (type1 == (System.Type) null)
          {
            type1 = typeof (FieldNameParam);
            System.Type type3 = typeof (Required<>);
            System.Type[] typeArray = new System.Type[1];
            System.Type type4 = field1;
            if ((object) type4 == null)
              type4 = typeof (FieldNameParam.PXRequiredField);
            typeArray[0] = type4;
            type2 = type3.MakeGenericType(typeArray);
          }
          else
            type2 = typeof (Required<>).MakeGenericType(type1);
          if (where == (System.Type) null)
          {
            System.Type type5 = topCount != 1 || reverseOrder ? (!sortColumns1[index1].IsDescending ? (reverseOrder ? typeof (Greater<>) : typeof (GreaterEqual<>)) : (reverseOrder ? typeof (Less<>) : typeof (LessEqual<>))) : typeof (Equal<>);
            where = typeof (Where<,>).MakeGenericType(type1, type5.MakeGenericType(type2));
          }
          else if (topCount == 1 && !reverseOrder)
            where = typeof (Where2<,>).MakeGenericType(typeof (Where<,>).MakeGenericType(type1, typeof (Equal<>).MakeGenericType(type2)), typeof (And<>).MakeGenericType(where));
          else if (sortColumns1[index1].IsDescending)
            where = typeof (Where2<,>).MakeGenericType(typeof (Where<,>).MakeGenericType(type1, typeof (Less<>).MakeGenericType(type2)), typeof (Or2<,>).MakeGenericType(typeof (Where<,>).MakeGenericType(type1, typeof (Equal<>).MakeGenericType(type2)), typeof (And<>).MakeGenericType(where)));
          else
            where = typeof (Where2<,>).MakeGenericType(typeof (Where<,>).MakeGenericType(type1, typeof (Greater<>).MakeGenericType(type2)), typeof (Or2<,>).MakeGenericType(typeof (Where<,>).MakeGenericType(type1, typeof (Equal<>).MakeGenericType(type2)), typeof (And<>).MakeGenericType(where)));
        }
      }
      if (where != (System.Type) null)
        cmd = cmd.WhereAnd(where);
    }
    return cmd;
  }

  /// <summary>Retrieves resultset out of the database</summary>
  /// <param name="parameters">Parameters for the command</param>
  /// <param name="searches">Search values</param>
  /// <param name="reverseOrder">If reversing of the sort expression is required</param>
  /// <param name="topCount">Number of rows required</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="needOverrideSort">If the Bql command needs to be updated with the new sort expression</param>
  /// <returns>Resultset, if there is no empty parameters, otherwise empty list</returns>
  protected virtual List<object> GetResult(
    object[] parameters,
    PXFilterRow[] filters,
    bool reverseOrder,
    int topCount,
    PXView.PXSearchColumn[] sorts,
    ref bool overrideSort,
    ref bool extFilter)
  {
    PXSelectOperationContext operationContext = new PXSelectOperationContext()
    {
      ReadArchived = this.ShouldReadArchived,
      SkipDefaultHints = this.SkipQueryHints
    };
    this._Select.Context = operationContext;
    PXView.VersionedList result1 = new PXView.VersionedList();
    IBqlParameter[] cmdpars = this._Select.GetParameters();
    if (parameters == null && cmdpars.Length != 0)
    {
      operationContext.BadParametersQueryNotExecuted = true;
      return (List<object>) result1;
    }
    if (parameters != null)
    {
      if (cmdpars.Length > parameters.Length)
      {
        operationContext.BadParametersQueryNotExecuted = true;
        return (List<object>) result1;
      }
      for (int index1 = 0; index1 < parameters.Length && index1 < cmdpars.Length; ++index1)
      {
        if (!cmdpars[index1].NullAllowed && parameters[index1] == null)
        {
          operationContext.BadParametersQueryNotExecuted = true;
          List<KeyValuePair<System.Type, System.Type>> parameterPairs = this._Select.GetParameterPairs();
          int num = 0;
          for (int index2 = index1; index2 < parameters.Length && index2 < cmdpars.Length; ++index2)
          {
            System.Type referencedType = cmdpars[index2].GetReferencedType();
            bool flag = false;
            foreach (KeyValuePair<System.Type, System.Type> keyValuePair in parameterPairs)
            {
              if (keyValuePair.Value == referencedType)
              {
                flag = true;
                System.Type key = keyValuePair.Key;
                PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(key)];
                if (!cmdpars[index2].NullAllowed)
                {
                  if (parameters[index2] == null)
                  {
                    if (cach.Keys.Contains(char.ToUpper(key.Name[0]).ToString() + key.Name.Substring(1)))
                      return (List<object>) result1;
                    ++num;
                    break;
                  }
                  break;
                }
                break;
              }
            }
            if (!flag)
              return (List<object>) result1;
          }
          if (num > 0)
            operationContext.BadParametersSkipMergeCache = true;
          return (List<object>) result1;
        }
      }
    }
    BqlCommand cmd1 = this._Select;
    List<PXDataValue> pxDataValueList = new List<PXDataValue>();
    if (overrideSort)
    {
      System.Type newOrderBy = this.ConstructSort(sorts, pxDataValueList, reverseOrder);
      if (newOrderBy != (System.Type) null)
        cmd1 = cmd1.OrderByNew(newOrderBy);
      overrideSort = false;
    }
    List<PXDataValue> pars = new List<PXDataValue>();
    ISqlDialect sqlDialect = this._Graph.SqlDialect;
    if (parameters != null)
    {
      int index3 = 0;
      for (int i = 0; i < cmdpars.Length && i < parameters.Length; i++)
      {
        if (cmdpars[i] is FieldNameParam && parameters[i] is PXFieldName)
        {
          pars.Add((PXDataValue) parameters[i]);
        }
        else
        {
          PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
          bool flag1 = this._IsReadOnly || cmdpars[i].NullAllowed;
          System.Type restrition = (System.Type) null;
          if (!cmdpars[i].IsArgument)
          {
            System.Type referencedType = cmdpars[i].GetReferencedType();
            if (!referencedType.IsNested || BqlCommand.GetItemType(referencedType) == this.CacheType)
            {
              if (this.Cache.isUnexistingKey(referencedType.Name, parameters[i]) && !cmdpars[i].NullAllowed && !this._Select.HasNotEqualParameter(cmdpars[i].GetReferencedType()))
              {
                operationContext.BadParametersQueryNotExecuted = true;
                return (List<object>) result1;
              }
              this.Cache.RaiseCommandPreparing(referencedType.Name, (object) null, parameters[i], (PXDBOperation) (0 | (flag1 ? 64 /*0x40*/ : 0)), (System.Type) null, out description1);
              if (cmdpars[i].MaskedType != (System.Type) null)
                restrition = GroupHelper.GetReferencedType(this.Cache, referencedType.Name);
            }
            else
            {
              PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(referencedType)];
              if (cach.isUnexistingKey(referencedType.Name, parameters[i]) && !cmdpars[i].NullAllowed && !this._Select.HasNotEqualParameter(cmdpars[i].GetReferencedType()))
              {
                operationContext.BadParametersQueryNotExecuted = true;
                return (List<object>) result1;
              }
              cach.RaiseCommandPreparing(referencedType.Name, (object) null, parameters[i], (PXDBOperation) (0 | (flag1 ? 64 /*0x40*/ : 0)), (System.Type) null, out description1);
              if (cmdpars[i].MaskedType != (System.Type) null)
                restrition = GroupHelper.GetReferencedType(cach, referencedType.Name);
            }
          }
          else if ((object) this._Delegate != null)
          {
            foreach (PXEventSubscriberAttribute customAttribute in this._Delegate.Method.GetParameters()[index3].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
            {
              List<IPXCommandPreparingSubscriber> preparingSubscriberList = new List<IPXCommandPreparingSubscriber>();
              List<IPXCommandPreparingSubscriber> subscribers = preparingSubscriberList;
              customAttribute.GetSubscriber<IPXCommandPreparingSubscriber>(subscribers);
              if (preparingSubscriberList.Count > 0)
              {
                PXCommandPreparingEventArgs e = new PXCommandPreparingEventArgs((object) null, parameters[i], PXDBOperation.Select, (System.Type) null, sqlDialect);
                for (int index4 = 0; index4 < preparingSubscriberList.Count; ++index4)
                  preparingSubscriberList[index4].CommandPreparing(this.Cache, e);
                description1 = e.GetFieldDescription();
                ++index3;
                break;
              }
            }
          }
          if (description1 == null || description1.DataValue == null && !cmdpars[i].NullAllowed)
          {
            PXCommandPreparingEventArgs.FieldDescription description2 = flag1 ? description1 : (PXCommandPreparingEventArgs.FieldDescription) null;
            operationContext.BadParametersQueryNotExecuted = true;
            int num = 0;
            List<KeyValuePair<System.Type, System.Type>> parameterPairs = this._Select.GetParameterPairs();
            for (int index5 = i; index5 < parameters.Length && index5 < cmdpars.Length; ++index5)
            {
              System.Type referencedType = cmdpars[index5].GetReferencedType();
              bool flag2 = false;
              foreach (KeyValuePair<System.Type, System.Type> keyValuePair in parameterPairs)
              {
                if (keyValuePair.Value == referencedType)
                {
                  flag2 = true;
                  System.Type key = keyValuePair.Key;
                  PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(key)];
                  if (description2 == null)
                    cach.RaiseCommandPreparing(key.Name, (object) null, parameters[index5], PXDBOperation.ReadOnly, (System.Type) null, out description2);
                  if (description2 == null || description2.DataValue == null && !cmdpars[index5].NullAllowed)
                  {
                    if (cach.Keys.Contains(char.ToUpper(key.Name[0]).ToString() + key.Name.Substring(1)))
                      return (List<object>) result1;
                    ++num;
                  }
                  description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
                  break;
                }
              }
              if (!flag2)
                return (List<object>) result1;
            }
            if (num > 0)
              operationContext.BadParametersSkipMergeCache = true;
            return (List<object>) result1;
          }
          if (cmdpars[i].MaskedType == (System.Type) null)
            pars.Add(new PXDataValue(description1.DataType, description1.DataLength, description1.DataValue));
          else if (cmdpars[i].MaskedType == typeof (Array))
            pars.Add(new PXDataValue(PXDbType.DirectExpression, description1.DataValue));
          else if (this._Graph.Caches[cmdpars[i].MaskedType].Fields.Contains("GroupMask"))
          {
            byte[] dataValue = description1.DataValue as byte[];
            GroupHelper.ParamsPair[] paramsPairArray = GroupHelper.GetParams(restrition, cmdpars[i].MaskedType, dataValue);
            foreach (GroupHelper.ParamsPair paramsPair in paramsPairArray)
            {
              pars.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.First));
              pars.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.Second));
            }
            if (paramsPairArray.Length != 0 && ((IEnumerable<IBqlParameter>) cmdpars).Skip<IBqlParameter>(i).Take<IBqlParameter>(paramsPairArray.Length * 2).All<IBqlParameter>((Func<IBqlParameter, bool>) (c => c == cmdpars[i])))
              i += paramsPairArray.Length * 2 - 1;
          }
        }
      }
    }
    PXDataValue firstsearch;
    BqlCommand cmd2 = this.appendSearches(sorts, pars, cmd1, ref topCount, reverseOrder, out firstsearch);
    if (firstsearch != null && this.Cache.Keys.Count > 1 && sorts.Length > 1 && string.Equals(this.Cache.Keys[0], sorts[0].Column, StringComparison.OrdinalIgnoreCase) && sorts[0].SearchValue != null && cmdpars.Length != 0 && !cmdpars[0].NullAllowed && string.Equals(this.Cache.Keys[0], cmdpars[0].GetReferencedType().Name, StringComparison.OrdinalIgnoreCase) && this._Select is IHasBqlWhere select)
    {
      System.Type type = select.GetWhere().GetType();
      if (type.IsGenericType)
      {
        System.Type[] genericArguments = type.GetGenericArguments();
        if (genericArguments.Length > 1 && string.Equals(this.Cache.Keys[0], genericArguments[0].Name, StringComparison.OrdinalIgnoreCase) && genericArguments[1].IsGenericType && genericArguments[1].GetGenericTypeDefinition() == typeof (Equal<>))
        {
          if (pars[0].Value is string str2)
          {
            if (firstsearch.Value is string str1 && PXLocalesProvider.CollationComparer.Compare(str2.Trim(), str1.Trim()) != 0)
              return (List<object>) result1;
          }
          else if (!(firstsearch.Value is string) && !object.Equals(pars[0].Value, firstsearch.Value))
            return (List<object>) result1;
        }
      }
    }
    BqlCommand bqlCommand = this.appendFilters(filters, pars, cmd2);
    pars.AddRange((IEnumerable<PXDataValue>) pxDataValueList);
    System.Type[] tables = bqlCommand.GetTables();
    bool hascount = !typeof (IBqlTable).IsAssignableFrom(tables[tables.Length - 1]);
    PXCache[] caches = new PXCache[hascount ? tables.Length - 1 : tables.Length];
    caches[0] = this.Cache;
    for (int index = 1; index < caches.Length; ++index)
      caches[index] = this._Graph.Caches[tables[index]];
    bool flag3 = PXView.IsAggregateCommand(this._Select);
    if (flag3)
      PXView.MarkCachesAsAggregate(caches, true);
    this.EnsureCreateInstance(tables);
    bqlCommand.Context = operationContext;
    if (this.Cache.SelectInterceptor != null)
    {
      foreach (object obj in this.Cache.SelectInterceptor.Select(this._Graph, bqlCommand, topCount, this, pars.ToArray()))
      {
        if (this._CreateInstance == null)
        {
          result1.Add(obj);
        }
        else
        {
          PXResult pxResult = (PXResult) this._CreateInstance(new object[1]
          {
            obj
          });
          pxResult.RowCount = new int?(1);
          result1.Add((object) pxResult);
        }
      }
      return (List<object>) result1;
    }
    using (new PerformanceMonitorSqlSampleScope(bqlCommand, this))
    {
      IEnumerable<PXDataRecord> pxDataRecords = this._Graph.ProviderSelect(bqlCommand, topCount, this.RestrictedFields.Any() ? this : (PXView) null, pars.ToArray());
      PXDataRecordMap pxDataRecordMap = (PXDataRecordMap) null;
      if (bqlCommand.RecordMapEntries.Any<PXDataRecordMap.FieldEntry>())
        pxDataRecordMap = new PXDataRecordMap(bqlCommand.RecordMapEntries);
      foreach (PXDataRecord row in pxDataRecords)
      {
        PXDataRecord rec = row;
        if (pxDataRecordMap != null)
        {
          pxDataRecordMap.SetRow(row);
          rec = (PXDataRecord) pxDataRecordMap;
        }
        object result2 = this.CreateResult(caches, rec, hascount, ref overrideSort, ref extFilter);
        if (result2 != null)
          result1.Add(result2);
      }
    }
    if (flag3)
      PXView.MarkCachesAsAggregate(caches, false);
    return (List<object>) result1;
  }

  private static bool IsAggregateCommand(BqlCommand bqlCommand)
  {
    return typeof (IBqlAggregate).IsAssignableFrom(bqlCommand.GetSelectType());
  }

  private static void MarkCachesAsAggregate(PXCache[] caches, bool aggregateSelecting)
  {
    foreach (PXCache cach in caches)
      cach._AggregateSelecting = aggregateSelecting;
  }

  private protected virtual object CreateResult(
    PXCache[] caches,
    PXDataRecord rec,
    bool hascount,
    ref bool overrideSort,
    ref bool extFilter)
  {
    int position = 0;
    object[] parameters = new object[caches.Length];
    for (int index = 0; index < caches.Length; ++index)
    {
      bool isReadOnly = this._IsReadOnly || index > 0 || index == 0 && this.RestrictedFields.Any() && this.MainTableCanBeRestricted;
      bool wasUpdated;
      parameters[index] = this.CreateItemReadDeleted(caches[index], rec, ref position, isReadOnly, out wasUpdated);
      if (wasUpdated)
      {
        overrideSort = true;
        extFilter = true;
      }
    }
    if (parameters[0] == null)
      return (object) null;
    if (!this._IsReadOnly && this.RestrictedFields.Any())
    {
      object obj = caches[0].Locate(parameters[0]);
      if (obj != null)
      {
        switch (caches[0].GetStatus(obj))
        {
          case PXEntryStatus.Updated:
          case PXEntryStatus.Inserted:
          case PXEntryStatus.Modified:
            overrideSort = true;
            extFilter = true;
            break;
        }
        parameters[0] = obj;
      }
    }
    if (this._CreateInstance == null)
      return parameters[0];
    PXResult result = (PXResult) this._CreateInstance(parameters);
    if (hascount)
      result.RowCount = rec.GetInt32(position);
    return (object) result;
  }

  private protected object CreateItem(
    PXCache cache,
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated)
  {
    wasUpdated = false;
    return cache.Select(record, ref position, isReadOnly, out wasUpdated);
  }

  private protected object CreateItemReadDeleted(
    PXCache cache,
    PXDataRecord record,
    ref int position,
    bool isReadOnly,
    out bool wasUpdated)
  {
    wasUpdated = false;
    if (PXDatabase.ReadDeleted)
      return cache.Select(record, ref position, isReadOnly, out wasUpdated);
    object row = cache.CreateItem(record, ref position, isReadOnly);
    return cache.Select(record, row, isReadOnly, out wasUpdated);
  }

  private List<object> GetResultWindowedRO(
    object[] parameters,
    PXView.PXSearchColumn[] sorts,
    int skip = 0,
    int take = 0)
  {
    PXSelectOperationContext operationContext = new PXSelectOperationContext()
    {
      ReadArchived = this.ShouldReadArchived,
      SkipDefaultHints = this.SkipQueryHints
    };
    this._Select.Context = operationContext;
    PXView.VersionedList resultWindowedRo = new PXView.VersionedList();
    IBqlParameter[] parameters1 = this._Select.GetParameters();
    if (parameters == null && parameters1.Length != 0)
    {
      operationContext.BadParametersQueryNotExecuted = true;
      return (List<object>) resultWindowedRo;
    }
    if (parameters != null)
    {
      if (parameters1.Length > parameters.Length)
      {
        operationContext.BadParametersQueryNotExecuted = true;
        return (List<object>) resultWindowedRo;
      }
      for (int index1 = 0; index1 < parameters.Length && index1 < parameters1.Length; ++index1)
      {
        if (!parameters1[index1].NullAllowed && parameters[index1] == null)
        {
          operationContext.BadParametersQueryNotExecuted = true;
          List<KeyValuePair<System.Type, System.Type>> parameterPairs = this._Select.GetParameterPairs();
          int num = 0;
          for (int index2 = index1; index2 < parameters.Length && index2 < parameters1.Length; ++index2)
          {
            System.Type referencedType = parameters1[index2].GetReferencedType();
            bool flag = false;
            foreach (KeyValuePair<System.Type, System.Type> keyValuePair in parameterPairs)
            {
              if (keyValuePair.Value == referencedType)
              {
                flag = true;
                System.Type key = keyValuePair.Key;
                PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(key)];
                if (!parameters1[index2].NullAllowed)
                {
                  if (parameters[index2] == null)
                  {
                    if (cach.Keys.Contains(char.ToUpper(key.Name[0]).ToString() + key.Name.Substring(1)))
                      return (List<object>) resultWindowedRo;
                    ++num;
                    break;
                  }
                  break;
                }
                break;
              }
            }
            if (!flag)
              return (List<object>) resultWindowedRo;
          }
          if (num > 0)
            operationContext.BadParametersSkipMergeCache = true;
          return (List<object>) resultWindowedRo;
        }
      }
    }
    BqlCommand command = this._Select;
    List<PXDataValue> pxDataValueList1 = new List<PXDataValue>();
    System.Type newOrderBy = this.ConstructSort(sorts, pxDataValueList1, false);
    if (newOrderBy != (System.Type) null)
      command = command.OrderByNew(newOrderBy);
    List<PXDataValue> pxDataValueList2 = new List<PXDataValue>();
    ISqlDialect sqlDialect = this._Graph.SqlDialect;
    if (parameters != null)
    {
      int index3 = 0;
      for (int index4 = 0; index4 < parameters1.Length && index4 < parameters.Length; ++index4)
      {
        if (parameters1[index4] is FieldNameParam && parameters[index4] is PXFieldName)
        {
          pxDataValueList2.Add((PXDataValue) parameters[index4]);
        }
        else
        {
          PXCommandPreparingEventArgs.FieldDescription description1 = (PXCommandPreparingEventArgs.FieldDescription) null;
          bool flag1 = this._IsReadOnly || parameters1[index4].NullAllowed;
          System.Type restrition = (System.Type) null;
          if (!parameters1[index4].IsArgument)
          {
            System.Type referencedType = parameters1[index4].GetReferencedType();
            if (!referencedType.IsNested || BqlCommand.GetItemType(referencedType) == this.CacheType)
            {
              this.Cache.RaiseCommandPreparing(referencedType.Name, (object) null, parameters[index4], (PXDBOperation) (0 | (flag1 ? 64 /*0x40*/ : 0)), (System.Type) null, out description1);
              if (parameters1[index4].MaskedType != (System.Type) null)
                restrition = GroupHelper.GetReferencedType(this.Cache, referencedType.Name);
            }
            else
            {
              PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(referencedType)];
              cach.RaiseCommandPreparing(referencedType.Name, (object) null, parameters[index4], (PXDBOperation) (0 | (flag1 ? 64 /*0x40*/ : 0)), (System.Type) null, out description1);
              if (parameters1[index4].MaskedType != (System.Type) null)
                restrition = GroupHelper.GetReferencedType(cach, referencedType.Name);
            }
          }
          else if ((object) this._Delegate != null)
          {
            foreach (PXEventSubscriberAttribute customAttribute in this._Delegate.Method.GetParameters()[index3].GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
            {
              List<IPXCommandPreparingSubscriber> preparingSubscriberList = new List<IPXCommandPreparingSubscriber>();
              List<IPXCommandPreparingSubscriber> subscribers = preparingSubscriberList;
              customAttribute.GetSubscriber<IPXCommandPreparingSubscriber>(subscribers);
              if (preparingSubscriberList.Count > 0)
              {
                PXCommandPreparingEventArgs e = new PXCommandPreparingEventArgs((object) null, parameters[index4], PXDBOperation.Select, (System.Type) null, sqlDialect);
                for (int index5 = 0; index5 < preparingSubscriberList.Count; ++index5)
                  preparingSubscriberList[index5].CommandPreparing(this.Cache, e);
                description1 = e.GetFieldDescription();
                ++index3;
                break;
              }
            }
          }
          if (description1 == null || description1.DataValue == null && !parameters1[index4].NullAllowed)
          {
            PXCommandPreparingEventArgs.FieldDescription description2 = flag1 ? description1 : (PXCommandPreparingEventArgs.FieldDescription) null;
            operationContext.BadParametersQueryNotExecuted = true;
            int num = 0;
            List<KeyValuePair<System.Type, System.Type>> parameterPairs = this._Select.GetParameterPairs();
            for (int index6 = index4; index6 < parameters.Length && index6 < parameters1.Length; ++index6)
            {
              System.Type referencedType = parameters1[index6].GetReferencedType();
              bool flag2 = false;
              foreach (KeyValuePair<System.Type, System.Type> keyValuePair in parameterPairs)
              {
                if (keyValuePair.Value == referencedType)
                {
                  flag2 = true;
                  System.Type key = keyValuePair.Key;
                  PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(key)];
                  if (description2 == null)
                    this.Cache.RaiseCommandPreparing(key.Name, (object) null, parameters[index6], PXDBOperation.ReadOnly, (System.Type) null, out description2);
                  if (description2 == null || description2.DataValue == null && !parameters1[index6].NullAllowed)
                  {
                    if (cach.Keys.Contains(char.ToUpper(key.Name[0]).ToString() + key.Name.Substring(1)))
                      return (List<object>) resultWindowedRo;
                    ++num;
                  }
                  description2 = (PXCommandPreparingEventArgs.FieldDescription) null;
                  break;
                }
              }
              if (!flag2)
                return (List<object>) resultWindowedRo;
            }
            if (num > 0)
              operationContext.BadParametersSkipMergeCache = true;
            return (List<object>) resultWindowedRo;
          }
          if (parameters1[index4].MaskedType == (System.Type) null)
            pxDataValueList2.Add(new PXDataValue(description1.DataType, description1.DataLength, description1.DataValue));
          else if (parameters1[index4].MaskedType == typeof (Array))
            pxDataValueList2.Add(new PXDataValue(PXDbType.DirectExpression, description1.DataValue));
          else if (this._Graph.Caches[parameters1[index4].MaskedType].Fields.Contains("GroupMask"))
          {
            byte[] dataValue = description1.DataValue as byte[];
            foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(restrition, parameters1[index4].MaskedType, dataValue))
            {
              pxDataValueList2.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.First));
              pxDataValueList2.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.Second));
            }
          }
        }
      }
    }
    pxDataValueList2.AddRange((IEnumerable<PXDataValue>) pxDataValueList1);
    System.Type[] tables = command.GetTables();
    bool flag3 = !typeof (IBqlTable).IsAssignableFrom(tables[tables.Length - 1]);
    PXCache[] caches = new PXCache[flag3 ? tables.Length - 1 : tables.Length];
    caches[0] = this.Cache;
    for (int index = 1; index < caches.Length; ++index)
      caches[index] = this._Graph.Caches[tables[index]];
    if (PXView.IsAggregateCommand(this._Select))
      PXView.MarkCachesAsAggregate(caches, true);
    this.EnsureCreateInstance(tables);
    command.Context = operationContext;
    IEnumerable<PXDataRecord> pxDataRecords = PXDatabase.Provider.Select(command.GetQuery(this._Graph, this, (long) take, (long) skip), (IEnumerable<PXDataValue>) pxDataValueList2.ToArray());
    PXDataRecordMap pxDataRecordMap = (PXDataRecordMap) null;
    if (command.RecordMapEntries.Any<PXDataRecordMap.FieldEntry>())
      pxDataRecordMap = new PXDataRecordMap(command.RecordMapEntries);
    foreach (PXDataRecord row in pxDataRecords)
    {
      PXDataRecord record = row;
      if (pxDataRecordMap != null)
      {
        pxDataRecordMap.SetRow(row);
        record = (PXDataRecord) pxDataRecordMap;
      }
      int position = 0;
      object[] parameters2 = new object[caches.Length];
      for (int index = 0; index < caches.Length; ++index)
        parameters2[index] = caches[index].Select(record, ref position, this._IsReadOnly || index > 0 || this.RestrictedFields.Any(), out bool _);
      if (parameters2[0] != null)
      {
        if (this._CreateInstance == null)
        {
          resultWindowedRo.Add(parameters2[0]);
        }
        else
        {
          PXResult pxResult = (PXResult) this._CreateInstance(parameters2);
          if (flag3)
            pxResult.RowCount = record.GetInt32(position);
          resultWindowedRo.Add((object) pxResult);
        }
      }
    }
    return (List<object>) resultWindowedRo;
  }

  private protected virtual PXCommandKey GetParametersCacheKey(
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    int maximumRows,
    PXFilterRow[] filters)
  {
    return new PXCommandKey((object[]) null, searches, sortcolumns, descendings, new int?(0), new int?(maximumRows), filters, PXDatabase.ReadBranchRestricted)
    {
      Select = this._Select.GetSelectType(),
      CacheType = this.CacheGetItemType()
    };
  }

  internal virtual void StoreCached(PXCommandKey queryKey, List<object> records)
  {
    this.StoreCached(queryKey, records, (PXSelectOperationContext) null);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// </summary>
  public void StoreResult(IBqlTable selectResult, PXQueryParameters queryParameters)
  {
    List<object> objectList = new List<object>(1)
    {
      (object) selectResult
    };
    if (WebConfig.StoreCachedValidation)
    {
      object[] parameters = queryParameters.GetParameters(this);
      this.ValidateParametersInfer(objectList, parameters);
    }
    this.StoreResult(objectList, queryParameters);
  }

  /// <summary>
  /// Intercepts PXView.Select calls, instead of querying the database, PXView.Select will immediately return synthetic result provided by this method
  /// The interceptor works in context of the current graph.
  /// For a new graph instance create a new interceptor
  /// Query parameters determined automatically from the provided selectResult
  /// </summary>
  public void StoreResult(IBqlTable selectResult)
  {
    object[] parameterValues = this.GetParameterValues(selectResult);
    this.StoreResult(new List<object>(1)
    {
      (object) selectResult
    }, PXQueryParameters.ExplicitParameters(parameterValues));
  }

  public void StoreResult(PXResult selectResult)
  {
    object[] parameterValues = this.GetParameterValues(selectResult);
    this.StoreResult(new List<object>(1)
    {
      (object) selectResult
    }, PXQueryParameters.ExplicitParameters(parameterValues));
  }

  internal object[] GetParameterValues(IBqlTable row)
  {
    int num = this._InvertedParameters == null ? 1 : 0;
    System.Type[] source = this._InvertedParameters ?? (this._InvertedParameters = this.GetParametersMapping());
    if (source.Length == 0)
      throw new PXException("Cannot invert parameters");
    object[] array = ((IEnumerable<System.Type>) source).Select<System.Type, object>((Func<System.Type, object>) (_ => this.Cache.GetValue((object) row, _.Name))).ToArray<object>();
    if (num != 0 && !this._Select.Meet(this.Cache, (object) row, array))
    {
      this._InvertedParameters = new System.Type[0];
      throw new PXException("Provided row does not meet select conditions");
    }
    return array;
  }

  internal object[] GetParameterValues(PXResult row)
  {
    System.Type[] typeArray = this._InvertedParameters ?? (this._InvertedParameters = this.GetParametersMapping());
    if (typeArray.Length == 0)
      throw new PXException("Cannot invert parameters");
    List<object> objectList = new List<object>();
    foreach (System.Type type in typeArray)
    {
      object data = row[type.DeclaringType];
      PXCache cach = this.Graph.Caches[type.DeclaringType];
      if (cach != null)
        objectList.Add(cach.GetValue(data, type.Name));
    }
    return objectList.ToArray();
  }

  private List<System.Type> GetParametersFromBQL()
  {
    System.Type[] typeArray = BqlCommand.Decompose(this._Select.GetSelectType());
    List<System.Type> parametersFromBql = new List<System.Type>();
    for (int index = 0; index < typeArray.Length - 1; ++index)
    {
      if (typeof (IBqlParameter).IsAssignableFrom(typeArray[index]))
      {
        System.Type c = typeArray[index + 1];
        if (typeof (IBqlField).IsAssignableFrom(c) && !parametersFromBql.Contains(c))
          parametersFromBql.Add(c);
      }
    }
    return parametersFromBql;
  }

  private System.Type[] GetParametersMapping()
  {
    System.Type[] typeArray = BqlCommand.Decompose(this._Select.GetSelectType());
    List<System.Type> typeList = new List<System.Type>();
    for (int index = 2; index < typeArray.Length; ++index)
    {
      if (typeof (IBqlParameter).IsAssignableFrom(typeArray[index]))
      {
        if (!typeof (Equal<>).IsAssignableFrom(typeArray[index - 1]))
          return new System.Type[0];
        System.Type c = typeArray[index - 2];
        if (!typeof (IBqlField).IsAssignableFrom(c))
          return new System.Type[0];
        typeList.Add(c);
      }
    }
    return typeList.ToArray();
  }

  public void StoreResult(List<object> selectResult, PXQueryParameters queryParameters)
  {
    object[] parameters = queryParameters.GetParameters(this);
    if (!this.SelectQueries.HasStoredResults())
      this.ValidateRecords(selectResult, parameters);
    List<object> items = this.IsReadOnly ? selectResult : (List<object>) new PXView.VersionedList((IEnumerable<object>) this.CloneResult(selectResult), this.Cache.Version);
    this.SelectQueries.SetResult(this, this.SelectQueries.GetAltCommandKey(parameters), items);
  }

  private void ValidateRecords(List<object> records, object[] prs)
  {
    try
    {
      string[] parameterNames = this.GetParameterNames();
      if (parameterNames.Length != prs.Length)
      {
        List<System.Type> parametersFromBql = this.GetParametersFromBQL();
        if (parametersFromBql.Count == prs.Length)
          return;
        PXTrace.Logger.ForTelemetry("StoreCached", "ParametersCount").WithStack().Warning("{Names};{Params};{Query};{BQL};{BQLNames}", new object[5]
        {
          (object) ((IEnumerable<string>) parameterNames).JoinToString<string>(","),
          (object) ((IEnumerable<object>) prs).JoinToString<object>(","),
          (object) this.ToString(),
          (object) this.BqlSelect.ToString(),
          (object) parametersFromBql.JoinToString<System.Type>(",")
        });
      }
      else
      {
        foreach (object record in records)
        {
          if (!this._Select.Meet(this.Cache, record, prs))
          {
            PXTrace.Logger.ForTelemetry("StoreCached", "Mismatch").WithStack().Warning("{Names};{Params};{Query};{BQL}", new object[4]
            {
              (object) ((IEnumerable<string>) parameterNames).JoinToString<string>(","),
              (object) ((IEnumerable<object>) prs).JoinToString<object>(","),
              (object) this.ToString(),
              (object) this.BqlSelect.ToString()
            });
            break;
          }
        }
      }
    }
    catch
    {
    }
  }

  internal virtual void StoreCached(
    PXCommandKey queryKey,
    List<object> records,
    PXSelectOperationContext context)
  {
    if (WebConfig.StoreCachedValidation)
    {
      object[] prs = queryKey.GetParameters() ?? new object[0];
      this.ValidateParametersInfer(records, prs);
      this.ValidateRecords(records, prs);
    }
    List<object> items = this.IsReadOnly ? records : (List<object>) new PXView.VersionedList((IEnumerable<object>) this.CloneResult(records), this.Cache.Version);
    this.SelectQueries.StoreCached(this, queryKey, items, context);
  }

  private void ValidateParametersInfer(List<object> records, object[] prs)
  {
    try
    {
      if (records == null || !records.Any<object>() || !(records[0] is IBqlTable record))
        return;
      object[] parameterValues = this.GetParameterValues(record);
      if (((IEnumerable<object>) parameterValues).SequenceEqual<object>((IEnumerable<object>) prs))
        return;
      PXTrace.Logger.ForTelemetry("StoreCached", "ValidateParameters").WithStack().Warning("{Params};{Params2};{Query};{BQL}", new object[4]
      {
        (object) ((IEnumerable<object>) prs).JoinToString<object>(","),
        (object) ((IEnumerable<object>) parameterValues).JoinToString<object>(","),
        (object) this.ToString(),
        (object) this.BqlSelect.ToString()
      });
    }
    catch (Exception ex)
    {
      PXTrace.Logger.ForTelemetry("StoreCached", "ValidateParameters").WithStack().Warning("{Params};{Error};{Query};{BQL}", new object[4]
      {
        (object) ((IEnumerable<object>) prs).JoinToString<object>(","),
        (object) ex.ToString(),
        (object) this.ToString(),
        (object) this.BqlSelect.ToString()
      });
    }
  }

  public List<object> CloneResult(List<object> list)
  {
    List<object> objectList;
    if (list is PXView.VersionedList versionedList)
      objectList = (List<object>) new PXView.VersionedList((IEnumerable<object>) list, versionedList.Version)
      {
        AnyMerged = versionedList.AnyMerged
      };
    else
      objectList = new List<object>((IEnumerable<object>) list);
    if (list.Count > 0 && list[0] is PXResult)
      this.EnsureCreateInstance(this._Select.GetTables());
    for (int index = 0; index < list.Count; ++index)
    {
      object obj = list[index];
      if (obj != null)
        objectList[index] = this.CloneItem(obj);
    }
    return objectList;
  }

  private protected virtual object CloneItem(object item, PXCache[] caches = null)
  {
    if (item is PXResult pxResult1 && this._CreateInstance != null)
    {
      int? rowCount = pxResult1.RowCount;
      PXResult pxResult = (PXResult) this._CreateInstance(pxResult1.Items);
      pxResult.RowCount = rowCount;
      object copy = this.Cache.CreateCopy(pxResult.Items[0]);
      this.CloneOriginals(this.Cache, pxResult.Items[0], copy);
      pxResult.Items[0] = copy;
      return (object) pxResult;
    }
    if (pxResult1 != null)
    {
      object copy = this.Cache.CreateCopy(pxResult1.Items[0]);
      this.CloneOriginals(this.Cache, pxResult1.Items[0], copy);
      return copy;
    }
    object copy1 = this.Cache.CreateCopy(item);
    this.CloneOriginals(this.Cache, item, copy1);
    return copy1;
  }

  /// <summary>Removes a result set from the cache of the results of BQL statement execution.</summary>
  /// <param name="queryKey">Key to search</param>
  /// <returns>true if resultset is found and removed successfully</returns>
  public bool RemoveCached(PXCommandKey queryKey)
  {
    return this.SelectQueries.TryRemove(queryKey, out PXQueryResult _);
  }

  private void CloneOriginals(PXCache cache, object oldItem, object newItem)
  {
    BqlTablePair bqlTablePair;
    if (!(oldItem is IBqlTable) || !(newItem is IBqlTable) || !cache._Originals.TryGetValue((IBqlTable) oldItem, out bqlTablePair))
      return;
    cache._Originals[(IBqlTable) newItem] = bqlTablePair;
  }

  /// <summary>Looks for a resultset inside the internal cache, merges updated and deleted items, adjusts top count</summary>
  /// <param name="queryKey">Key to search</param>
  /// <param name="topCount">Number of rows required</param>
  /// <returns>Resultset if found in the cache, otherwise null</returns>
  protected virtual List<object> LookupCache(
    PXCommandKey queryKey,
    ref int topCount,
    ref bool overrideSort,
    ref bool needFilterResults)
  {
    List<object> objectList = (List<object>) null;
    if (this._IsCommandMutable)
      this.SelectQueries.IsCommandMutable = this._IsCommandMutable;
    if (this.SelectQueries.IsCommandMutable && queryKey.CommandText == null && !queryKey.BadParamsQueryNotExecuted)
      queryKey.CommandText = this.ToString();
    PXQueryResult v = (PXQueryResult) null;
    if (this.SelectQueries.TryGetValue(queryKey, out v) && !v.IsExpired(PXDatabase.Provider))
      objectList = v.Items;
    else if (v != null)
      this.SelectQueries.TryRemove(queryKey, out PXQueryResult _);
    return objectList;
  }

  private protected virtual object LookupItem(
    PXDataRecord record,
    object item,
    PXCache[] caches,
    ref bool overrideSort,
    ref bool needFilterResults)
  {
    bool wasUpdated = false;
    object obj1;
    if (item is PXResult pxResult1)
    {
      int? rowCount = pxResult1.RowCount;
      PXResult pxResult = (PXResult) this._CreateInstance(pxResult1.Items);
      pxResult.RowCount = rowCount;
      obj1 = (object) pxResult;
      for (int i = 0; i < pxResult.Items.Length; ++i)
      {
        if (pxResult.Items[i] != null)
        {
          object obj2 = caches[i].Locate(pxResult.Items[i]);
          if (obj2 != null && !this._IsReadOnly && (this.Cache.Interceptor == null || this.Cache.Interceptor.CacheSelected))
          {
            pxResult.Items[i] = obj2;
          }
          else
          {
            object copy = caches[i].CreateCopy(pxResult.Items[i]);
            this.CloneOriginals(caches[i], pxResult.Items[i], copy);
            pxResult.Items[i] = copy;
            pxResult[i] = caches[i].Select(record, pxResult[i], this._IsReadOnly || i > 0 || this.RestrictedFields.Any(), out wasUpdated);
            if (wasUpdated)
            {
              overrideSort = true;
              needFilterResults = true;
            }
          }
        }
      }
    }
    else
    {
      object obj3 = caches[0].Locate(item);
      if (obj3 != null && !this._IsReadOnly && (this.Cache.Interceptor == null || this.Cache.Interceptor.CacheSelected))
      {
        obj1 = obj3;
      }
      else
      {
        object copy = caches[0].CreateCopy(item);
        this.CloneOriginals(caches[0], item, copy);
        object row = copy;
        obj1 = caches[0].Select(record, row, this._IsReadOnly || this.RestrictedFields.Any(), out wasUpdated);
        if (wasUpdated)
        {
          overrideSort = true;
          needFilterResults = true;
        }
      }
    }
    return obj1;
  }

  /// <summary>Selects the data records joined with the provided data record by the underlying BQL command.</summary>
  /// <param name="item">First data item.</param>
  /// <param name="parameters">Parameters.</param>
  /// <returns>The first item plus joined rows.</returns>
  public virtual void AppendTail(object item, List<object> list, params object[] parameters)
  {
    if (!(this._Select is IBqlJoinedSelect))
      return;
    if (this._TailSelect == null)
    {
      BqlCommand tail = ((IBqlJoinedSelect) this._Select).GetTail();
      if (tail != null)
        this._TailSelect = this._Graph.TypedViews.GetView(tail, true);
    }
    List<object> objectList;
    if (this._TailSelect != null)
    {
      this._TailSelect._PrimaryTableType = this._CacheType;
      objectList = this._TailSelect.SelectMultiBound(new object[1]
      {
        item
      }, parameters);
      this._TailSelect._PrimaryTableType = (System.Type) null;
    }
    else
      objectList = new List<object>();
    System.Type[] tables = this._Select.GetTables();
    this.EnsureCreateInstance(tables);
    if (objectList.Count == 0)
    {
      if (((IBqlJoinedSelect) this._Select).IsInner && ((IBqlJoinedSelect) this._Select).GetTail() != null)
        return;
      this.AppendEmptyTail(item, list, tables);
    }
    else if (!(objectList[0] is PXResult))
    {
      for (int index = 0; index < objectList.Count; ++index)
        list.Add(this._CreateInstance(new object[2]
        {
          item,
          objectList[index]
        }));
    }
    else
    {
      for (int index1 = 0; index1 < objectList.Count; ++index1)
      {
        object[] parameters1 = new object[((PXResult) objectList[index1]).TableCount + 1];
        parameters1[0] = item;
        for (int index2 = 1; index2 < parameters1.Length; ++index2)
          parameters1[index2] = ((PXResult) objectList[index1])[index2 - 1];
        list.Add(this._CreateInstance(parameters1));
      }
    }
  }

  private protected virtual void AppendEmptyTail(object item, List<object> list)
  {
    System.Type[] tables = this._Select.GetTables();
    this.EnsureCreateInstance(tables);
    this.AppendEmptyTail(item, list, tables);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void AppendEmptyTail(object item, List<object> list, System.Type[] tables)
  {
    object[] parameters = new object[tables.Length];
    parameters[0] = item;
    for (int index = 1; index < tables.Length; ++index)
    {
      if (typeof (IBqlTable).IsAssignableFrom(tables[index]))
        parameters[index] = this._Graph.Caches[tables[index]].CreateInstance();
    }
    list.Add(this._CreateInstance(parameters));
  }

  protected virtual bool emulation => this.Cache.Interceptor is PXUIEmulatorAttribute;

  /// <summary>Merges the result set with the updated/inserted/deleted items, stores the result set in the internal cache.</summary>
  /// <param name="list">Resultset</param>
  /// <param name="parameters">Query parameters</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="reverseOrder">If reverse of the sort expression is required</param>
  /// <param name="queryKey">Key to store the result in the cache</param>
  private protected virtual bool MergeCache(
    List<object> list,
    object[] parameters,
    bool filterExists,
    ref bool sortReq)
  {
    bool flag1 = false;
    Query query = (Query) null;
    if (this._IsReadOnly && !this.emulation || !this.Graph.Caches.ContainsKey(this.CacheType))
      return false;
    int version = this.Cache.Version;
    if (this._Select is IBqlJoinedSelect || !(list is PXView.VersionedList) || ((PXView.VersionedList) list).Version != version)
    {
      if (!this.fullKeyReferenced.HasValue)
      {
        if (this.Cache.BqlSelect != null || this._Select is IBqlJoinedSelect)
        {
          this.fullKeyReferenced = new bool?(false);
        }
        else
        {
          HashSet<System.Type> typeSet = new HashSet<System.Type>((IEnumerable<System.Type>) this.Cache.BqlImmutables);
          foreach (System.Type referencedField in this._Select.GetReferencedFields(true))
          {
            if (!typeSet.Remove(referencedField))
            {
              this.fullKeyReferenced = new bool?(false);
              break;
            }
          }
          if (!this.fullKeyReferenced.HasValue)
            this.fullKeyReferenced = typeSet.Count != 0 ? new bool?(false) : new bool?(true);
        }
      }
      bool? fullKeyReferenced = this.fullKeyReferenced;
      bool flag2 = true;
      if (!(fullKeyReferenced.GetValueOrDefault() == flag2 & fullKeyReferenced.HasValue) || list.Count != 1 || list[0] == null || this.LocateInCache(list[0]) != list[0])
      {
        HashSet<object> objectSet = (HashSet<object>) null;
        foreach (object obj in this.Cache.Inserted)
        {
          object item = obj;
          bool flag3 = this._Select.Meet(this.Cache, item, parameters);
          if (flag3 && this.emulation)
          {
            if (query == null)
              query = this._Select.GetQuery(this.Cache.Graph, this);
            flag3 = flag3 && this.Cache._InvokeMeetVerifier(item, query, parameters);
          }
          if (flag3)
          {
            if (this._Select is IBqlJoinedSelect && this.SupressTailSelect)
            {
              if (objectSet == null)
                objectSet = new HashSet<object>(list.Select<object, object>((Func<object, object>) (i => !(i is PXResult pxResult) ? i : pxResult[0])));
              if (!objectSet.Contains(item))
                this.AppendEmptyTail(item, list);
            }
            else if (this._Select is IBqlJoinedSelect)
            {
              bool flag4 = true;
              int index = 0;
              while (index < list.Count)
              {
                if (list[index] is PXResult pxResult1 && this.Cache.ObjectsEqual(item, pxResult1[0]))
                {
                  if (this.emulation)
                  {
                    flag4 = false;
                    break;
                  }
                  list.RemoveAt(index);
                }
                else
                  ++index;
              }
              if (flag4)
              {
                if (this.emulation)
                {
                  if (query == null)
                    query = this._Select.GetQuery(this.Cache.Graph, this);
                  PXResult pxResult2 = this.Cache._InvokeTailAppender(item, query, parameters);
                  if (pxResult2 != null)
                    list.Add((object) pxResult2);
                  else
                    this.AppendTail(item, list, parameters);
                }
                else
                  this.AppendTail(item, list, parameters);
              }
            }
            else
            {
              if (objectSet == null)
                objectSet = new HashSet<object>((IEnumerable<object>) list);
              if (!objectSet.Contains(item))
                list.Add(item);
            }
            sortReq = true;
            flag1 = true;
            fullKeyReferenced = this.fullKeyReferenced;
            bool flag5 = true;
            if (fullKeyReferenced.GetValueOrDefault() == flag5 & fullKeyReferenced.HasValue)
              break;
          }
          else if (list.Count > 0 && !this.emulation)
          {
            if (this._Select is IBqlJoinedSelect)
            {
              if (objectSet == null)
                objectSet = new HashSet<object>();
              for (int index = 0; index < list.Count; ++index)
              {
                if (list[index] is PXResult pxResult)
                  objectSet.Add(pxResult[0]);
              }
            }
            else if (objectSet == null)
              objectSet = new HashSet<object>((IEnumerable<object>) list);
            if (objectSet.Contains(item))
              list.RemoveAll((Predicate<object>) (_ => _ == item));
          }
        }
      }
    }
    if (this.Cache.BqlSelect != null || this.Cache.HasChangedKeys())
      this.keyReferenceOnly = new bool?(false);
    bool? keyReferenceOnly = this.keyReferenceOnly;
    bool flag6 = true;
    if (!(keyReferenceOnly.GetValueOrDefault() == flag6 & keyReferenceOnly.HasValue) | filterExists)
    {
      foreach (object a in this.Cache.Updated)
      {
        if (!this.keyReferenceOnly.HasValue)
        {
          List<System.Type> bqlImmutables = this.Cache.BqlImmutables;
          this.keyReferenceOnly = new bool?(true);
          foreach (System.Type referencedField in this._Select.GetReferencedFields(false))
          {
            if ((BqlCommand.GetItemType(referencedField) == this.CacheType || this.CacheType.IsSubclassOf(BqlCommand.GetItemType(referencedField))) && !bqlImmutables.Contains(referencedField))
            {
              this.keyReferenceOnly = new bool?(false);
              break;
            }
          }
        }
        keyReferenceOnly = this.keyReferenceOnly;
        bool flag7 = true;
        if (keyReferenceOnly.GetValueOrDefault() == flag7 & keyReferenceOnly.HasValue)
        {
          if (!filterExists)
            break;
        }
        if (this._Select.Meet(this.Cache, a, parameters))
        {
          if (this._Select is IBqlJoinedSelect)
          {
            if (this.SupressTailSelect)
            {
              for (int index = 0; index < list.Count; ++index)
              {
                if (list[index] is PXResult pxResult && this.Cache.ObjectsEqual(a, pxResult[0]))
                  pxResult[0] = a;
              }
            }
            else
            {
              int index = 0;
              while (index < list.Count)
              {
                if (list[index] is PXResult pxResult && this.Cache.ObjectsEqual(a, pxResult[0]))
                  list.RemoveAt(index);
                else
                  ++index;
              }
              this.AppendTail(a, list, parameters);
            }
          }
          else if (!list.Contains(a))
            list.Add(a);
          sortReq = true;
          flag1 = true;
        }
        else if (this._Select is IBqlJoinedSelect)
        {
          int index = 0;
          while (index < list.Count)
          {
            if (list[index] is PXResult pxResult && this.Cache.ObjectsEqual(a, pxResult[0]))
              list.RemoveAt(index);
            else
              ++index;
          }
        }
        else if (list.Contains(a))
          list.Remove(a);
      }
    }
    if (list is PXView.VersionedList)
    {
      ((PXView.VersionedList) list).Version = version;
      ((PXView.VersionedList) list).AnyMerged |= flag1;
    }
    return flag1;
  }

  private bool InternCache(ref List<object> list, ref bool sortReq)
  {
    bool flag1 = false;
    if (!this._IsReadOnly && (this.Cache.Interceptor == null || this.Cache.Interceptor.CacheSelected))
    {
      bool wasUpdated = false;
      bool flag2 = false;
      if (list is PXView.VersionedList versionedList)
      {
        if (versionedList.MergedList == null || versionedList.MergedList.Version != this.Cache.Version)
        {
          versionedList.MergedList = new PXView.VersionedList((IEnumerable<object>) list, versionedList.Version);
          versionedList.MergedList.AnyMerged = versionedList.AnyMerged;
          flag2 = true;
        }
        list = (List<object>) versionedList.MergedList;
      }
      else
        list = new List<object>((IEnumerable<object>) list);
      if (flag2 && list.Count > 0 && list[0] is PXResult)
      {
        System.Type[] tables = this._Select.GetTables();
        this.EnsureCreateInstance(tables);
        if (this._CreateInstance != null)
        {
          for (int index = 0; index < list.Count; ++index)
          {
            object obj = list[index];
            if (obj != null && obj is PXResult pxResult1 && tables.Length <= pxResult1.Items.Length)
            {
              int? rowCount = pxResult1.RowCount;
              PXResult pxResult = (PXResult) this._CreateInstance(pxResult1.Items);
              pxResult.RowCount = rowCount;
              list[index] = (object) pxResult;
            }
          }
        }
      }
      Dictionary<object, object> dictionary = (Dictionary<object, object>) null;
      if (this.Cache._Persisting && this.Cache.Inserted.Count() > 0L)
      {
        dictionary = new Dictionary<object, object>(this.Cache.GetComparer());
        foreach (object key in this.Cache.Inserted)
          dictionary.Add(key, key);
      }
      int index1 = 0;
      while (index1 < list.Count)
      {
        object obj1 = list[index1];
        switch (obj1)
        {
          case null:
            ++index1;
            continue;
          case PXResult pxResult:
            if (pxResult.Items[0] == null)
            {
              ++index1;
              continue;
            }
            object obj2 = this.Cache.Locate(pxResult.Items[0]);
            if (obj2 == null && dictionary != null)
              dictionary.TryGetValue(pxResult.Items[0], out obj2);
            if (obj2 != null)
            {
              PXEntryStatus status = this.Cache.GetStatus(pxResult.Items[0]);
              if (status == PXEntryStatus.Deleted || !PXDatabase.ReadInsertedDeleted && status == PXEntryStatus.InsertedDeleted)
              {
                list.RemoveAt(index1);
                continue;
              }
              if (obj2 != pxResult.Items[0])
              {
                pxResult.Items[0] = obj2;
                flag1 = true;
                break;
              }
              break;
            }
            object copy = pxResult.Items[0];
            if (flag2)
            {
              copy = this.Cache.CreateCopy(pxResult.Items[0]);
              this.CloneOriginals(this.Cache, pxResult.Items[0], copy);
              pxResult.Items[0] = copy;
            }
            this.Cache.PlaceNotChanged(copy, out wasUpdated);
            if (wasUpdated)
            {
              sortReq = true;
              break;
            }
            break;
          case IBqlTable _:
            object obj3 = this.Cache.Locate(obj1);
            if (obj3 == null && dictionary != null)
              dictionary.TryGetValue(obj1, out obj3);
            if (obj3 != null)
            {
              PXEntryStatus status = this.Cache.GetStatus(obj1);
              if (status == PXEntryStatus.Deleted || !PXDatabase.ReadInsertedDeleted && status == PXEntryStatus.InsertedDeleted)
              {
                list.RemoveAt(index1);
                continue;
              }
              if (obj3 != obj1)
              {
                list[index1] = obj3;
                flag1 = true;
                break;
              }
              break;
            }
            object obj4 = obj1;
            if (flag2)
            {
              obj4 = this.Cache.CreateCopy(obj1);
              this.CloneOriginals(this.Cache, obj1, obj4);
              list[index1] = obj4;
            }
            this.Cache.PlaceNotChanged(obj4, out wasUpdated);
            if (wasUpdated)
            {
              sortReq = true;
              break;
            }
            break;
          default:
            ++index1;
            continue;
        }
        ++index1;
      }
    }
    return flag1;
  }

  private void RemoveDeleted(List<object> list)
  {
    if (this._IsReadOnly || !this.Graph.Caches.ContainsKey(this.CacheType))
      return;
    if (this.Cache.Deleted.Count() <= 0L)
      return;
    Dictionary<object, object> dictionary = new Dictionary<object, object>(this.Cache.GetComparer());
    foreach (object key in this.Cache.Deleted)
      dictionary.Add(key, key);
    int index = 0;
    while (index < list.Count)
    {
      object key = list[index];
      if (key == null)
      {
        ++index;
      }
      else
      {
        if (key is PXResult pxResult)
        {
          if (pxResult.Items[0] == null)
          {
            ++index;
            continue;
          }
          if (dictionary.ContainsKey(pxResult.Items[0]))
          {
            list.RemoveAt(index);
            continue;
          }
        }
        else if (dictionary.ContainsKey(key))
        {
          list.RemoveAt(index);
          continue;
        }
        ++index;
      }
    }
  }

  private protected virtual object LocateInCache(object item) => this.Cache.Locate(item);

  /// <summary>Sorts the result set.</summary>
  /// <param name="list">Resultset to sort</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="reverseOrder">If reversing of the sort order is required</param>
  protected virtual void SortResult(
    List<object> list,
    PXView.PXSearchColumn[] sorts,
    bool reverseOrder)
  {
    if (list.Count < 2 || sorts.Length == 0)
      return;
    ISqlDialect dialect = (ISqlDialect) null;
    PXView.compareDelegate[] comparisons = new PXView.compareDelegate[sorts.Length];
    for (int index = 0; index < sorts.Length; ++index)
    {
      string fieldName = sorts[index].Column;
      System.Type tableType = (System.Type) null;
      PXCache cache = this.Cache;
      bool descending = sorts[index].Descending;
      bool useExt = sorts[index].UseExt;
      int length = fieldName.IndexOf("__");
      if (length != -1)
      {
        tableType = list[0] is PXResult ? ((PXResult) list[0]).GetItemType(fieldName.Substring(0, length)) : (System.Type) null;
        fieldName = fieldName.Substring(length + 2);
        if (tableType != (System.Type) null)
          cache = this._Graph.Caches[tableType];
      }
      else if (list[0] is PXResult)
      {
        PXResult pxResult = (PXResult) list[0];
        tableType = pxResult.GetItemType(0);
        if (useExt && !cache.Fields.Contains(fieldName))
        {
          bool flag = false;
          int i = 1;
          while (i < pxResult.Items.Length && !(flag = (cache = this._Graph.Caches[tableType = pxResult.GetItemType(i)]).Fields.Contains(fieldName)))
            ++i;
          if (!flag)
          {
            cache = this.Cache;
            tableType = pxResult.GetItemType(0);
          }
        }
      }
      PXCollationComparer collationCmp = PXLocalesProvider.CollationComparer;
      PXStringState stateExt = cache.GetStateExt((object) null, fieldName) as PXStringState;
      Dictionary<string, string> dict = (Dictionary<string, string>) null;
      if (stateExt != null && stateExt.AllowedValues != null && stateExt.AllowedLabels != null)
        dict = stateExt.ValueLabelDic;
      comparisons[index] = reverseOrder ? (PXView.compareDelegate) ((a, b) => -this.CompareMethod(a, b, cache, fieldName, descending, useExt, tableType, collationCmp, dict, ref dialect)) : (PXView.compareDelegate) ((a, b) => this.CompareMethod(a, b, cache, fieldName, descending, useExt, tableType, collationCmp, dict, ref dialect));
    }
    list.Sort((Comparison<object>) ((a, b) => this.Compare(a, b, comparisons)));
  }

  internal static int compareGuid(Guid a, Guid b, ISqlDialect dialect)
  {
    int[] guidByteOrder = dialect.GetGuidByteOrder();
    byte[] byteArray1 = a.ToByteArray();
    byte[] byteArray2 = b.ToByteArray();
    for (int index = guidByteOrder.Length - 1; index >= 0; --index)
    {
      int num = byteArray1[guidByteOrder[index]].CompareTo(byteArray2[guidByteOrder[index]]);
      if (num != 0)
        return num;
    }
    return 0;
  }

  private void ConvertFilterValues(object value, PXFilterRow fr)
  {
    System.DateTime result;
    if (fr.Value is string && value is System.DateTime && System.DateTime.TryParse((string) fr.Value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
      fr.Value = (object) result;
    if (!(fr.Value2 is string) || !(value is System.DateTime) || !System.DateTime.TryParse((string) fr.Value2, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
      return;
    fr.Value2 = (object) result;
  }

  protected internal virtual void FilterResult(List<object> list, PXFilterRow[] filters)
  {
    if (filters == null || filters.Length == 0 || list.Count == 0)
      return;
    List<KeyValuePair<PXView.HashList, bool>> keyValuePairList1 = new List<KeyValuePair<PXView.HashList, bool>>();
    int index1 = 0;
    KeyValuePair<PXView.HashList, bool> keyValuePair1;
    for (int index2 = 0; index2 < filters.Length; ++index2)
    {
      PXFilterRow fr = filters[index2];
      if ((fr.Condition == PXCondition.ISNULL || fr.Condition == PXCondition.ISNOTNULL || fr.Condition == PXCondition.TODAY || fr.Condition == PXCondition.OVERDUE || fr.Condition == PXCondition.TODAY_OVERDUE || fr.Condition == PXCondition.TOMMOROW || fr.Condition == PXCondition.THIS_WEEK || fr.Condition == PXCondition.NEXT_WEEK || fr.Condition == PXCondition.THIS_MONTH || fr.Condition == PXCondition.NEXT_MONTH || fr.Value != null) && (fr.Condition != PXCondition.BETWEEN || fr.Value2 != null) && !string.IsNullOrEmpty(fr.DataField))
      {
        int length = fr.DataField.IndexOf("__");
        System.Type t = list[0] is PXResult ? (length != -1 ? ((PXResult) list[0]).GetItemType(fr.DataField.Substring(0, length)) : ((PXResult) list[0]).GetItemType(0)) : this.Cache.GetItemType();
        string dataField = length != -1 ? fr.DataField.Substring(length + 2) : fr.DataField;
        if (length == -1 || !(t == (System.Type) null))
        {
          PXCache cache = length != -1 ? this._Graph.Caches[t] : this.Cache;
          if (!string.IsNullOrEmpty(dataField) && cache.Fields.Contains(dataField))
          {
            PXView.getPXResultValue getValue = !fr.UseExt ? (list[0] is PXResult ? (PXView.getPXResultValue) (item =>
            {
              object obj = cache.GetValue(((PXResult) item)[t], dataField);
              this.ConvertFilterValues(obj, fr);
              return obj;
            }) : (PXView.getPXResultValue) (item =>
            {
              object obj = cache.GetValue(item, dataField);
              this.ConvertFilterValues(obj, fr);
              return obj;
            })) : (list[0] is PXResult ? (PXView.getPXResultValue) (item =>
            {
              object obj = PXFieldState.UnwrapValue(cache.GetValueExt(((PXResult) item)[t], dataField));
              this.ConvertFilterValues(obj, fr);
              return obj;
            }) : (PXView.getPXResultValue) (item =>
            {
              object obj = PXFieldState.UnwrapValue(cache.GetValueExt(item, dataField));
              this.ConvertFilterValues(obj, fr);
              return obj;
            }));
            PXFieldState fromCorrectCache = this.GetStateFromCorrectCache(fr.DataField) as PXFieldState;
            if (fromCorrectCache is PXStringState pxStringState && pxStringState.MultiSelect && fr.Value != null)
              fr.Value = (object) fr.Value.ToString().Trim(',');
            Predicate<object> match = (Predicate<object>) null;
            Predicate<object> isNullAsEmpty = (Predicate<object>) (item => getValue(item) is string str1 && string.IsNullOrWhiteSpace(str1) && !this.Graph.IsContractBasedAPI);
            switch (fr.Condition)
            {
              case PXCondition.EQ:
                if (fr.Value != null)
                {
                  if (fr.Value is string)
                  {
                    string sval = ((string) fr.Value).Trim();
                    match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) == 0);
                    break;
                  }
                  match = !fr.UseExt || !(fr.Value is bool) ? (Predicate<object>) (item => object.Equals(getValue(item), fr.Value)) : (Predicate<object>) (item =>
                  {
                    try
                    {
                      return object.Equals((object) Convert.ToBoolean(getValue(item)), fr.Value);
                    }
                    catch (FormatException ex)
                    {
                      return false;
                    }
                  });
                  break;
                }
                list.Clear();
                return;
              case PXCondition.NE:
                if (fr.Value != null)
                {
                  if (fr.Value is string)
                  {
                    string sval = ((string) fr.Value).Trim();
                    match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) != 0);
                    break;
                  }
                  match = (Predicate<object>) (item => !object.Equals(getValue(item), fr.Value));
                  break;
                }
                break;
              case PXCondition.GT:
                if (fr.Value is string)
                {
                  string sval = ((string) fr.Value).Trim();
                  match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) > 0);
                  break;
                }
                if (fr.Value is IComparable)
                {
                  match = (Predicate<object>) (item => ((IComparable) fr.Value).CompareTo(getValue(item)) < 0);
                  break;
                }
                break;
              case PXCondition.GE:
                if (fr.Value is string)
                {
                  string sval = ((string) fr.Value).Trim();
                  match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) >= 0);
                  break;
                }
                if (fr.Value is IComparable)
                {
                  match = (Predicate<object>) (item => ((IComparable) fr.Value).CompareTo(getValue(item)) <= 0);
                  break;
                }
                break;
              case PXCondition.LT:
                if (fr.Value is string)
                {
                  string sval = ((string) fr.Value).Trim();
                  match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) < 0);
                  break;
                }
                if (fr.Value is IComparable)
                {
                  match = (Predicate<object>) (item => ((IComparable) fr.Value).CompareTo(getValue(item)) > 0);
                  break;
                }
                list.Clear();
                return;
              case PXCondition.LE:
                if (fr.Value is string)
                {
                  string sval = ((string) fr.Value).Trim();
                  match = (Predicate<object>) (item => this.CompareValueWithFilter(getValue(item), sval) <= 0);
                  break;
                }
                if (fr.Value is IComparable)
                {
                  match = (Predicate<object>) (item => ((IComparable) fr.Value).CompareTo(getValue(item)) >= 0);
                  break;
                }
                list.Clear();
                return;
              case PXCondition.LIKE:
              case PXCondition.RLIKE:
              case PXCondition.LLIKE:
              case PXCondition.NOTLIKE:
                if (fr.Value is int num1)
                  fr.Value = (object) num1.ToString();
                if (fr.Value is string)
                {
                  bool likeInvertResult = false;
                  Func<string, bool> matcher = (Func<string, bool>) null;
                  string fValue = (string) fr.Value;
                  PXCondition fastFilterCondition = SitePolicy.GridFastFilterCondition;
                  bool isFastFilter = filters.Length > 1 && EnumerableExtensions.Except<PXFilterRow>((IEnumerable<PXFilterRow>) filters, fr).All<PXFilterRow>((Func<PXFilterRow, bool>) (f => f.Condition == fastFilterCondition && f.Value is string && string.Equals(((string) f.Value).Trim(), fValue?.Trim(), StringComparison.OrdinalIgnoreCase)));
                  if (fValue.Contains(cache.Graph.SqlDialect.WildcardAnySingle) || fValue.Contains(cache.Graph.SqlDialect.WildcardAnything))
                  {
                    string pattern = fValue.Replace(cache.Graph.SqlDialect.WildcardAnything, ".*").Replace(cache.Graph.SqlDialect.WildcardAnySingle, ".{1}");
                    switch (fr.Condition)
                    {
                      case PXCondition.LIKE:
                        pattern = $".*{pattern}.*";
                        likeInvertResult = false;
                        break;
                      case PXCondition.RLIKE:
                        pattern += ".*";
                        likeInvertResult = false;
                        break;
                      case PXCondition.LLIKE:
                        pattern = ".*" + pattern;
                        likeInvertResult = false;
                        break;
                      case PXCondition.NOTLIKE:
                        pattern = $".*{pattern}.*";
                        likeInvertResult = true;
                        break;
                    }
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
                    matcher = (Func<string, bool>) (v => regex.IsMatch(v));
                  }
                  else
                  {
                    string lower = fValue.ToLower();
                    switch (fr.Condition)
                    {
                      case PXCondition.LIKE:
                        matcher = (Func<string, bool>) (v => v.ToLower().Contains(lower));
                        likeInvertResult = false;
                        break;
                      case PXCondition.RLIKE:
                        matcher = (Func<string, bool>) (v => v.StartsWith(fValue, StringComparison.CurrentCultureIgnoreCase));
                        likeInvertResult = false;
                        break;
                      case PXCondition.LLIKE:
                        matcher = fromCorrectCache is PXSegmentedState ? (Func<string, bool>) (v => v.TrimEnd().EndsWith(fValue.TrimEnd(), StringComparison.CurrentCultureIgnoreCase)) : (Func<string, bool>) (v => v.EndsWith(fValue, StringComparison.CurrentCultureIgnoreCase));
                        likeInvertResult = false;
                        break;
                      case PXCondition.NOTLIKE:
                        matcher = (Func<string, bool>) (v => v.ToLower().Contains(lower));
                        likeInvertResult = true;
                        break;
                    }
                  }
                  PXStringState stateExt = cache.GetStateExt((object) null, dataField) as PXStringState;
                  Dictionary<string, string> valueLabelDic = (Dictionary<string, string>) null;
                  if (stateExt != null && stateExt.ValueLabelDic != null && stateExt.ValueLabelDic.Count > 0)
                    valueLabelDic = stateExt.ValueLabelDic;
                  match = (Predicate<object>) (item =>
                  {
                    string key = getValue(item)?.ToString();
                    if (key != null & isFastFilter && valueLabelDic != null && valueLabelDic.ContainsKey(key))
                      key = valueLabelDic[key];
                    return key != null && likeInvertResult ^ matcher(key);
                  });
                  break;
                }
                break;
              case PXCondition.BETWEEN:
              case PXCondition.TODAY:
              case PXCondition.OVERDUE:
              case PXCondition.TOMMOROW:
              case PXCondition.THIS_WEEK:
              case PXCondition.THIS_MONTH:
                Predicate<object> leftCondition = (Predicate<object>) null;
                Predicate<object> rightCondition = (Predicate<object>) null;
                string startFilterValueString = fr.Value as string;
                if (startFilterValueString != null)
                {
                  startFilterValueString = startFilterValueString.Trim();
                  leftCondition = (Predicate<object>) (value => PXLocalesProvider.CollationComparer.Compare(startFilterValueString, value is string str2 ? str2.Trim() : (string) null) <= 0);
                }
                else
                {
                  IComparable startFilterValue = fr.Value as IComparable;
                  if (startFilterValue != null)
                    leftCondition = (Predicate<object>) (value => startFilterValue.CompareTo(value) <= 0);
                }
                string endFilterValueString = fr.Value2 as string;
                if (endFilterValueString != null)
                {
                  endFilterValueString = endFilterValueString.Trim();
                  rightCondition = (Predicate<object>) (value => PXLocalesProvider.CollationComparer.Compare(endFilterValueString, value is string str3 ? str3.Trim() : (string) null) >= 0);
                }
                else
                {
                  IComparable endFilterValue = fr.Value2 as IComparable;
                  if (endFilterValue != null)
                    rightCondition = (Predicate<object>) (value => endFilterValue.CompareTo(value) >= 0);
                }
                if (leftCondition == null || rightCondition == null)
                {
                  list.Clear();
                  return;
                }
                match = (Predicate<object>) (item =>
                {
                  object obj = getValue(item);
                  return leftCondition(obj) && rightCondition(obj);
                });
                break;
              case PXCondition.ISNULL:
                match = (Predicate<object>) (item => getValue(item) == null || isNullAsEmpty(item));
                break;
              case PXCondition.ISNOTNULL:
                match = (Predicate<object>) (item => !isNullAsEmpty(item) && getValue(item) != null);
                break;
              case PXCondition.TODAY_OVERDUE:
              case PXCondition.NEXT_WEEK:
              case PXCondition.NEXT_MONTH:
                System.DateTime?[] dateRange = PXView.DateTimeFactory.GetDateRange(fr.Condition, new System.DateTime?());
                System.DateTime? startDate = dateRange[0];
                System.DateTime? endDate = dateRange[1];
                match = (Predicate<object>) (item =>
                {
                  System.DateTime? nullable = (System.DateTime?) getValue(item);
                  if (!nullable.HasValue || startDate.HasValue && startDate.Value.CompareTo((object) nullable) > 0)
                    return false;
                  return !endDate.HasValue || endDate.Value.CompareTo((object) nullable) >= 0;
                });
                break;
              case PXCondition.IN:
              case PXCondition.NI:
                FilterVariableType? variable = fr.Variable;
                FilterVariableType filterVariableType1 = FilterVariableType.CurrentUserGroups;
                if (variable.GetValueOrDefault() == filterVariableType1 & variable.HasValue)
                {
                  match = !fr.UseExt ? (Predicate<object>) (item =>
                  {
                    object obj = getValue(item);
                    if (!(obj is int))
                      return false;
                    HashSet<int> userGroupIds = UserGroupLazyCache.Current.GetUserGroupIds(PXAccess.GetUserID());
                    return fr.Condition != PXCondition.IN ? !userGroupIds.Contains((int) obj) : userGroupIds.Contains((int) obj);
                  }) : (Predicate<object>) (item =>
                  {
                    string str4 = getValue(item) as string;
                    if (string.IsNullOrEmpty(str4))
                      return false;
                    HashSet<string> groupDescriptions = UserGroupLazyCache.Current.GetUserGroupDescriptions(PXAccess.GetUserID());
                    return fr.Condition != PXCondition.IN ? !groupDescriptions.Contains(str4) : groupDescriptions.Contains(str4);
                  });
                  break;
                }
                variable = fr.Variable;
                FilterVariableType filterVariableType2 = FilterVariableType.CurrentUserGroupsTree;
                if (variable.GetValueOrDefault() == filterVariableType2 & variable.HasValue)
                {
                  match = !fr.UseExt ? (Predicate<object>) (item =>
                  {
                    object obj = getValue(item);
                    if (!(obj is int))
                      return false;
                    HashSet<int> userWorkTreeIds = UserGroupLazyCache.Current.GetUserWorkTreeIds(PXAccess.GetUserID());
                    return fr.Condition != PXCondition.IN ? !userWorkTreeIds.Contains((int) obj) : userWorkTreeIds.Contains((int) obj);
                  }) : (Predicate<object>) (item =>
                  {
                    string str5 = getValue(item) as string;
                    if (string.IsNullOrEmpty(str5))
                      return false;
                    HashSet<string> treeDescriptions = UserGroupLazyCache.Current.GetUserWorkTreeDescriptions(PXAccess.GetUserID());
                    return fr.Condition != PXCondition.IN ? !treeDescriptions.Contains(str5) : treeDescriptions.Contains(str5);
                  });
                  break;
                }
                variable = fr.Variable;
                FilterVariableType filterVariableType3 = FilterVariableType.CurrentOrganization;
                if (variable.GetValueOrDefault() == filterVariableType3 & variable.HasValue)
                {
                  if (fr.UseExt)
                  {
                    HashSet<string> branches = ((IEnumerable<string>) PXAccess.GetBranchCDsForCurrentOrganization()).ToHashSet<string>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
                    match = (Predicate<object>) (item =>
                    {
                      string str6 = getValue(item) as string;
                      if (string.IsNullOrEmpty(str6))
                        return false;
                      string str7 = str6.TrimEnd();
                      return fr.Condition != PXCondition.IN ? !branches.Contains(str7) : branches.Contains(str7);
                    });
                    break;
                  }
                  HashSet<int> branches1 = ((IEnumerable<int>) PXAccess.GetBranchIDsForCurrentOrganization()).ToHashSet<int>();
                  match = (Predicate<object>) (item =>
                  {
                    if (!(getValue(item) is int num3))
                      return false;
                    return fr.Condition != PXCondition.IN ? !branches1.Contains(num3) : branches1.Contains(num3);
                  });
                  break;
                }
                if ((object) (fr.Value as System.Type) != null)
                {
                  PXCache subcache = this._Graph.Caches[BqlCommand.GetItemType((System.Type) fr.Value)];
                  List<PXDataField> pars = new List<PXDataField>();
                  for (int index3 = fr.OpenBrackets; index3 > 0 && index2 + 1 < filters.Length; ++index2)
                  {
                    index3 = index3 + filters[index2 + 1].OpenBrackets - filters[index2 + 1].CloseBrackets;
                    if (filters[index2 + 1].Description != null)
                    {
                      PXComp comp;
                      switch (filters[index2 + 1].Condition)
                      {
                        case PXCondition.EQ:
                          comp = PXComp.EQ;
                          break;
                        case PXCondition.NE:
                          comp = PXComp.NE;
                          break;
                        case PXCondition.GT:
                          comp = PXComp.GT;
                          break;
                        case PXCondition.GE:
                          comp = PXComp.GE;
                          break;
                        case PXCondition.LT:
                          comp = PXComp.LT;
                          break;
                        case PXCondition.LE:
                          comp = PXComp.LE;
                          break;
                        case PXCondition.LIKE:
                          comp = PXComp.LIKE;
                          break;
                        case PXCondition.ISNULL:
                          comp = PXComp.ISNULL;
                          break;
                        case PXCondition.ISNOTNULL:
                          comp = PXComp.ISNOTNULL;
                          break;
                        default:
                          continue;
                      }
                      SQLExpression fieldName = filters[index2 + 1].Description.Expr.Duplicate().substituteTableName(subcache.GetItemType().Name, subcache.BqlTable.Name);
                      pars.Add((PXDataField) new PXDataFieldValue(fieldName, filters[index2 + 1].Description.DataType, filters[index2 + 1].Description.DataLength, filters[index2 + 1].Description.DataValue, comp));
                    }
                  }
                  match = (Predicate<object>) (item =>
                  {
                    PXCommandPreparingEventArgs.FieldDescription description;
                    subcache.RaiseCommandPreparing(((System.Type) fr.Value).Name, (object) null, getValue(item), PXDBOperation.Select, (System.Type) null, out description);
                    if (description == null || description.Expr == null || description.Expr.Oper() == SQLExpression.Operation.NULL)
                      return true;
                    pars.Add((PXDataField) new PXDataFieldValue(description.Expr, description.DataType, description.DataLength, description.DataValue));
                    pars.Add(new PXDataField(description.Expr));
                    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(subcache.BqlTable, pars.ToArray()))
                      return fr.Condition == PXCondition.IN && pxDataRecord != null || fr.Condition == PXCondition.NI && pxDataRecord == null;
                  });
                  break;
                }
                break;
              case PXCondition.ER:
                match = (Predicate<object>) (item =>
                {
                  object obj = getValue(item);
                  if (!(obj is long) || !(fr.Value is string))
                    return false;
                  OrderedDictionary keys = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
                  keys[(object) "NoteID"] = obj;
                  PXCache cach = this._Graph.Caches[typeof (Note)];
                  return cach.Locate((IDictionary) keys) > 0 && cach.Current != null && string.Equals(((Note) cach.Current).ExternalKey, (string) fr.Value, StringComparison.InvariantCultureIgnoreCase);
                });
                break;
            }
            if (match != null)
            {
              index1 += fr.OpenBrackets;
              if (index1 < keyValuePairList1.Count)
              {
                keyValuePair1 = keyValuePairList1[index1];
                if (!keyValuePair1.Value)
                {
                  List<KeyValuePair<PXView.HashList, bool>> keyValuePairList2 = keyValuePairList1;
                  int index4 = index1;
                  keyValuePair1 = keyValuePairList1[index1];
                  KeyValuePair<PXView.HashList, bool> keyValuePair2 = new KeyValuePair<PXView.HashList, bool>(new PXView.HashList((IEnumerable<object>) keyValuePair1.Key.FindAll(match)), fr.OrOperator);
                  keyValuePairList2[index4] = keyValuePair2;
                }
                else
                {
                  foreach (object obj in list.FindAll(match))
                  {
                    keyValuePair1 = keyValuePairList1[index1];
                    if (!keyValuePair1.Key.Contains(obj))
                    {
                      keyValuePair1 = keyValuePairList1[index1];
                      keyValuePair1.Key.Add(obj);
                    }
                  }
                  keyValuePairList1[index1] = new KeyValuePair<PXView.HashList, bool>(keyValuePairList1[index1].Key, fr.OrOperator);
                }
              }
              else
              {
                for (int count = keyValuePairList1.Count; count <= index1; ++count)
                  keyValuePairList1.Add(new KeyValuePair<PXView.HashList, bool>(new PXView.HashList(), true));
                keyValuePairList1[index1] = new KeyValuePair<PXView.HashList, bool>(new PXView.HashList((IEnumerable<object>) list.FindAll(match)), fr.OrOperator);
              }
              for (int index5 = 0; index5 < fr.CloseBrackets && index1 != 0; ++index5)
              {
                keyValuePair1 = keyValuePairList1[index1 - 1];
                if (keyValuePair1.Value)
                {
                  keyValuePair1 = keyValuePairList1[index1];
                  foreach (object obj in (List<object>) keyValuePair1.Key)
                  {
                    keyValuePair1 = keyValuePairList1[index1 - 1];
                    if (!keyValuePair1.Key.Contains(obj))
                    {
                      keyValuePair1 = keyValuePairList1[index1 - 1];
                      keyValuePair1.Key.Add(obj);
                    }
                  }
                }
                else
                {
                  int index6 = 0;
                  while (true)
                  {
                    int num4 = index6;
                    keyValuePair1 = keyValuePairList1[index1 - 1];
                    int count = keyValuePair1.Key.Count;
                    if (num4 < count)
                    {
                      keyValuePair1 = keyValuePairList1[index1];
                      PXView.HashList key = keyValuePair1.Key;
                      keyValuePair1 = keyValuePairList1[index1 - 1];
                      object obj = keyValuePair1.Key[index6];
                      if (!key.Contains(obj))
                      {
                        keyValuePair1 = keyValuePairList1[index1 - 1];
                        keyValuePair1.Key.RemoveAt(index6);
                      }
                      else
                        ++index6;
                    }
                    else
                      break;
                  }
                }
                List<KeyValuePair<PXView.HashList, bool>> keyValuePairList3 = keyValuePairList1;
                int index7 = index1 - 1;
                keyValuePair1 = keyValuePairList1[index1 - 1];
                KeyValuePair<PXView.HashList, bool> keyValuePair3 = new KeyValuePair<PXView.HashList, bool>(keyValuePair1.Key, fr.OrOperator);
                keyValuePairList3[index7] = keyValuePair3;
                keyValuePairList1.RemoveAt(index1);
                --index1;
              }
            }
          }
        }
      }
    }
label_141:
    for (; index1 > 0; --index1)
    {
      keyValuePair1 = keyValuePairList1[index1 - 1];
      if (keyValuePair1.Value)
      {
        keyValuePair1 = keyValuePairList1[index1];
        foreach (object obj in (List<object>) keyValuePair1.Key)
        {
          keyValuePair1 = keyValuePairList1[index1 - 1];
          if (!keyValuePair1.Key.Contains(obj))
          {
            keyValuePair1 = keyValuePairList1[index1 - 1];
            keyValuePair1.Key.Add(obj);
          }
        }
      }
      else
      {
        int index8 = 0;
        while (true)
        {
          int num = index8;
          keyValuePair1 = keyValuePairList1[index1 - 1];
          int count = keyValuePair1.Key.Count;
          if (num < count)
          {
            keyValuePair1 = keyValuePairList1[index1];
            PXView.HashList key = keyValuePair1.Key;
            keyValuePair1 = keyValuePairList1[index1 - 1];
            object obj = keyValuePair1.Key[index8];
            if (!key.Contains(obj))
            {
              keyValuePair1 = keyValuePairList1[index1 - 1];
              keyValuePair1.Key.RemoveAt(index8);
            }
            else
              ++index8;
          }
          else
            goto label_141;
        }
      }
    }
    if (keyValuePairList1.Count <= 0)
      return;
    int index9 = 0;
    while (index9 < list.Count)
    {
      keyValuePair1 = keyValuePairList1[0];
      if (keyValuePair1.Key.Contains(list[index9]))
        ++index9;
      else
        list.RemoveAt(index9);
    }
  }

  private int CompareValueWithFilter(object value, string filterValue)
  {
    return value != null && TypeHelper.IsNumericOrDate(value) ? TypeHelper.CompareValueWithString(value, filterValue, (IFormatProvider) CultureInfo.CurrentUICulture) : PXLocalesProvider.CollationComparer.Compare(value is string str ? str.Trim() : (string) null, filterValue);
  }

  /// <summary>Cuts the resultset by search values, starting row and number of rows required</summary>
  /// <param name="list">The result set.</param>
  /// <param name="searches">Values to search</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="reverseOrder">If reversing of the sort expression is required</param>
  /// <param name="startRow">Index of row to start with</param>
  /// <param name="maximumRows">Maximum number of rows to return</param>
  /// <param name="totalRows">Total number of rows fetched</param>
  /// <param name="searchFound">If there is an item that meets search values</param>
  /// <returns>Filtered resultset</returns>
  protected virtual List<object> SearchResult(
    List<object> list,
    PXView.PXSearchColumn[] sorts,
    bool reverseOrder,
    bool findAll,
    ref int startRow,
    int maximumRows,
    ref int totalRows,
    out bool searchFound)
  {
    if (list.Count == 0)
    {
      searchFound = false;
      return list;
    }
    ISqlDialect dialect = (ISqlDialect) null;
    searchFound = false;
    int num1 = 0;
    List<object> objectList;
    switch (list)
    {
      case PXView.VersionedList _:
        objectList = (List<object>) new PXView.VersionedList();
        break;
      case IPXDelegateResult pxDelegateResult:
        objectList = (List<object>) new PXDelegateResult()
        {
          IsResultSorted = pxDelegateResult.IsResultSorted,
          IsResultFiltered = pxDelegateResult.IsResultFiltered,
          IsResultTruncated = pxDelegateResult.IsResultTruncated
        };
        break;
      default:
        objectList = new List<object>();
        break;
    }
    List<PXView.searchDelegate> searchDelegateList = (List<PXView.searchDelegate>) null;
    for (int index = 0; index < sorts.Length; ++index)
    {
      if (sorts[index].SearchValue != null)
      {
        if (searchDelegateList == null)
          searchDelegateList = new List<PXView.searchDelegate>();
        string fieldName = sorts[index].Column;
        System.Type tableType = (System.Type) null;
        PXCache cache = this.Cache;
        bool descending = sorts[index].Descending;
        bool useExt = sorts[index].UseExt;
        int length = fieldName.IndexOf("__");
        object searchValue = sorts[index].SearchValue;
        if (length != -1)
        {
          tableType = list[0] is PXResult ? ((PXResult) list[0]).GetItemType(fieldName.Substring(0, length)) : (System.Type) null;
          if (tableType != (System.Type) null)
            cache = this._Graph.Caches[tableType];
          fieldName = fieldName.Substring(length + 2);
        }
        else if (list[0] is PXResult)
          tableType = ((PXResult) list[0]).GetItemType(0);
        PXStringState stateExt = cache.GetStateExt((object) null, fieldName) as PXStringState;
        Dictionary<string, string> dict = (Dictionary<string, string>) null;
        if (stateExt != null && stateExt.AllowedValues != null && stateExt.AllowedLabels != null && stateExt.ValueLabelDic.Count > 0)
          dict = stateExt.ValueLabelDic;
        searchDelegateList.Add((PXView.searchDelegate) (a => this.SearchMethod(a, searchValue, cache, fieldName, descending, useExt, tableType, dict, ref dialect)));
      }
    }
    if (searchDelegateList != null)
    {
      PXView.searchDelegate[] array = searchDelegateList.ToArray();
      int index1 = 0;
      if (searchDelegateList.Count > 0)
      {
        while (index1 < list.Count && (!reverseOrder && this.Search(list[index1], array) < 0 || reverseOrder && this.Search(list[index1], array) >= 0))
          ++index1;
      }
      searchFound = !reverseOrder && index1 < list.Count && this.Search(list[index1], array) == 0;
      if (totalRows == -1 && maximumRows > 2)
      {
        if (!reverseOrder)
        {
          index1 = index1 / maximumRows * maximumRows;
        }
        else
        {
          int num2 = ((list.Count - index1 - 1) / maximumRows + 1) * maximumRows;
          index1 = list.Count - num2;
          if (index1 < 0)
          {
            maximumRows += index1;
            index1 = 0;
          }
        }
      }
      int index2 = list.Count;
      if (((!searchFound ? 0 : (!reverseOrder ? 1 : 0)) & (findAll ? 1 : 0)) != 0)
      {
        index2 = index1 + 1;
        while (index2 < list.Count && this.Search(list[index2], array) == 0)
          ++index2;
      }
      for (int index3 = index1; index3 < index2; ++index3)
        objectList.Add(list[index3]);
      if (totalRows == -1)
        num1 = !reverseOrder ? index1 : list.Count - index1 - 1;
    }
    else
    {
      for (int index = 0; index < list.Count; ++index)
        objectList.Add(list[index]);
      if (totalRows == -1 && reverseOrder)
      {
        if (maximumRows > 2)
        {
          int num3 = ((list.Count - startRow - 1) / maximumRows + 1) * maximumRows;
          if (num3 > 0 && num3 < list.Count && list.Count - num3 < startRow)
          {
            maximumRows = list.Count - num3;
            startRow = maximumRows - 1;
          }
        }
        num1 = list.Count;
      }
    }
    totalRows = list.Count;
    if (!reverseOrder)
    {
      if (startRow > 0)
      {
        if (startRow <= objectList.Count)
          objectList.RemoveRange(0, startRow);
        else
          objectList.Clear();
      }
    }
    else
    {
      if (maximumRows > 0 && startRow >= objectList.Count)
      {
        maximumRows -= startRow - objectList.Count + 1;
        if (maximumRows < 0)
          maximumRows = 0;
      }
      if (startRow + 1 < objectList.Count)
        objectList.RemoveRange(startRow + 1, objectList.Count - startRow - 1);
      objectList.Reverse();
    }
    if (maximumRows >= 0 && maximumRows < objectList.Count)
      objectList.RemoveRange(maximumRows, objectList.Count - maximumRows);
    if (!reverseOrder)
      startRow += num1;
    else
      startRow = num1 - startRow - 1;
    if (objectList is PXView.VersionedList)
      ((PXView.VersionedList) objectList).Version = ((PXView.VersionedList) list).Version;
    return objectList;
  }

  private PXView.PXSearchColumn[] DistinctSearches(PXView.PXSearchColumn[] searches)
  {
    return !PXDatabase.Provider.OrderByColumnsMustBeUnique || !PXDatabase.Provider.TablesOrderedByClusteredIndexByDefault ? searches : ((IEnumerable<PXView.PXSearchColumn>) searches).Distinct<PXView.PXSearchColumn>((IEqualityComparer<PXView.PXSearchColumn>) PXView.PXSearchColumnEqualityComparer.Instance).ToArray<PXView.PXSearchColumn>();
  }

  /// <summary>The main selection procedure.</summary>
  /// <param name="currents">Items to replace current values when retrieving Current and Optional parameters</param>
  /// <param name="parameters">Query parameters</param>
  /// <param name="searches">Search values</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="startRow">Index of row to start with</param>
  /// <param name="maximumRows">Maximum rows to return</param>
  /// <param name="totalRows">Total rows fetched</param>
  /// <returns>Resultset requested</returns>
  public virtual List<object> Select(
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return this.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows, (string[]) null);
  }

  /// <summary>The main selection procedure.</summary>
  /// <param name="currents">Items to replace current values when retrieving Current and Optional parameters</param>
  /// <param name="parameters">Query parameters</param>
  /// <param name="searches">Search values</param>
  /// <param name="sortcolumns">Sort columns</param>
  /// <param name="descendings">Descending flags</param>
  /// <param name="startRow">Index of row to start with</param>
  /// <param name="maximumRows">Maximum rows to return</param>
  /// <param name="totalRows">Total rows fetched</param>
  /// <returns>Resultset requested</returns>
  private List<object> Select(
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows,
    string[] sortAsImplicitColumns)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    BqlCommand select = this._Select;
    PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
    if (currentSample != null)
    {
      ++currentSample.SelectCount;
      currentSample.SelectTimer.Start();
    }
    int num1 = totalRows;
    if (totalRows == -2)
      totalRows = 0;
    bool resetTopCount = false;
    if (this.FiltersResetRequired)
      filters = (PXFilterRow[]) null;
    PXCommandKey parametersCacheKey = this.GetParametersCacheKey(searches, sortcolumns, descendings, maximumRows, filters);
    bool flag1 = false;
    SelectCacheEntry selectCacheEntry1;
    PXView.PXSearchColumn[] pxSearchColumnArray;
    bool needOverrideSort;
    bool anySearch;
    if (this.SelectQueries.ParametersCache.TryGetValue(parametersCacheKey, out selectCacheEntry1))
    {
      pxSearchColumnArray = selectCacheEntry1.Sorts;
      needOverrideSort = selectCacheEntry1.overrideSort;
      anySearch = selectCacheEntry1.anySearch;
      resetTopCount = selectCacheEntry1.resetTopCount;
      if (filters != null && filters.Length != 0)
      {
        filters = selectCacheEntry1.filters;
        flag1 = selectCacheEntry1.extFilter;
      }
      if (selectCacheEntry1.NewOrder != (System.Type) null)
        this._Select = this._Select.OrderByNew(selectCacheEntry1.NewOrder);
    }
    else
    {
      this._NewOrder = (System.Type) null;
      pxSearchColumnArray = this.prepareSorts(sortcolumns, descendings, searches, maximumRows, out needOverrideSort, out anySearch, ref resetTopCount, sortAsImplicitColumns: sortAsImplicitColumns);
      if (filters != null && filters.Length != 0)
        flag1 = this.prepareFilters(ref filters);
      SelectCacheEntry selectCacheEntry2 = new SelectCacheEntry()
      {
        KeyDigest = parametersCacheKey,
        NewOrder = this._NewOrder,
        overrideSort = needOverrideSort,
        anySearch = anySearch,
        resetTopCount = resetTopCount,
        extFilter = flag1
      };
      if (filters != null)
        selectCacheEntry2.filters = (PXFilterRow[]) filters.Clone();
      if (pxSearchColumnArray != null)
        selectCacheEntry2.Sorts = (PXView.PXSearchColumn[]) pxSearchColumnArray.Clone();
      this._NewOrder = (System.Type) null;
      this.SelectQueries.ParametersCache.TryAdd(parametersCacheKey, selectCacheEntry2);
    }
    IBqlParameter[] orAdd = PXView.CommandParamsCache.GetOrAdd(this._Select.GetUniqueKey(), (Func<object, IBqlParameter[]>) (type => this._Select.GetParameters()));
    parameters = this.PrepareParametersInternal(currents, parameters, orAdd);
    if (flag1)
      resetTopCount = true;
    int topCount = 0;
    bool reverseOrder = false;
    if (startRow < 0)
    {
      reverseOrder = true;
      needOverrideSort = true;
      startRow = -1 - startRow;
      if (maximumRows > 0 && totalRows != -1)
        topCount = startRow + 1;
    }
    else if (maximumRows > 0 && totalRows != -1)
      topCount = startRow + maximumRows;
    if (maximumRows == 0)
      maximumRows = -1;
    if (resetTopCount)
      topCount = 0;
    bool flag2 = totalRows != -1 && maximumRows > 0;
    int num2 = topCount;
    bool flag3 = false;
    List<object> list = (List<object>) null;
    IPXDelegateResult pxDelegateResult = (IPXDelegateResult) null;
    try
    {
      PXView._Executing.Push(new PXView.Context(this, pxSearchColumnArray, filters, currents, parameters, reverseOrder, flag2 ? startRow : 0, flag2 ? maximumRows : 0, totalRows == -1, this.RestrictedFields));
      list = this.InvokeDelegate(parameters);
      pxDelegateResult = list as IPXDelegateResult;
      if (list is IPXDelegateCacheResult delegateCacheResult)
      {
        if (delegateCacheResult.IsResultCachable)
        {
          PXCommandKey queryKey = new PXCommandKey(delegateCacheResult.CacheKeys);
          List<object> objectList = list;
          list = this.LookupCache(queryKey, ref topCount, ref needOverrideSort, ref flag1);
          if (list == null)
          {
            delegateCacheResult.EmitRows();
            list = objectList;
            this.StoreCached(queryKey, list, new PXSelectOperationContext()
            {
              LastSqlTables = new List<string>((IEnumerable<string>) delegateCacheResult.SqlTables)
            });
          }
        }
      }
    }
    finally
    {
      Stack<PXView.Context> executing = PXView._Executing;
      if (executing.Any<PXView.Context>())
      {
        PXView.Context context = executing.Pop();
        flag3 = context.RetrieveTotalRowCount;
        if (list != null & flag2)
          startRow = !reverseOrder || context.StartRow != 0 || startRow == 0 ? context.StartRow : maximumRows - 1;
        if (context.Filters != filters)
        {
          filters = context.Filters;
          flag1 = this.prepareFilters(ref filters);
        }
        if (context.Sorts != pxSearchColumnArray)
        {
          pxSearchColumnArray = context.Sorts;
          this.prepareUnboundSorts(pxSearchColumnArray, topCount, ref resetTopCount);
        }
      }
    }
    if (flag3 && list != null && list.Count == 1 && (list[0] is PXResult pxResult ? (pxResult.RowCount.HasValue ? 1 : 0) : 0) != 0)
    {
      totalRows = ((PXResult) list[0]).RowCount.Value;
      list.Clear();
    }
    else
    {
      if (list != null)
      {
        flag1 = true;
        if ((pxDelegateResult != null ? (!pxDelegateResult.IsResultSorted ? 1 : 0) : 1) != 0)
          this.SortResult(list, pxSearchColumnArray, reverseOrder);
      }
      if (list == null)
      {
        bool flag4 = false;
        PXCommandKey pxCommandKey = (PXCommandKey) null;
        if (!PXDatabase.ReadDeleted)
        {
          string[] array = this.RestrictedFields.Any() ? this.RestrictedFields.Select<RestrictedField, string>((Func<RestrictedField, string>) (f => f.Field)).ToArray<string>() : (string[]) null;
          int num3 = this.AdjustTopCountByChangedRecords(maximumRows, searches, sortcolumns);
          pxCommandKey = topCount == 0 ? new PXCommandKey(parameters, searches, sortcolumns, descendings, new int?(), new int?(), filters, PXDatabase.ReadBranchRestricted, this.ShouldReadArchived, array) : new PXCommandKey(parameters, searches, sortcolumns, descendings, new int?(reverseOrder ? -1 - startRow : startRow), new int?(num3), filters, PXDatabase.ReadBranchRestricted, this.ShouldReadArchived, array);
          list = this.LookupCache(pxCommandKey, ref topCount, ref needOverrideSort, ref flag1);
          flag4 = list != null;
          if (flag4 && currentSample != null && PXPerformanceMonitor.SaveSqlToDb && PXPerformanceMonitor.SqlProfilerIncludeQueryCache)
          {
            PXProfilerSqlSample profilerSqlSample = currentSample.AddSqlSample(pxCommandKey.CommandText ?? this.ToString(), pxCommandKey.ToString(), true);
            if (profilerSqlSample != null)
              profilerSqlSample.RowCount = new int?(list.Count);
          }
        }
        if (list == null)
        {
          topCount = this.AdjustTopCountByChangedRecords(topCount, searches, sortcolumns);
          list = this.GetResult(parameters, filters, reverseOrder, topCount, pxSearchColumnArray, ref needOverrideSort, ref flag1);
        }
        else
        {
          needOverrideSort = false;
          flag1 = ((flag1 ? 1 : 0) | (!(list is PXView.VersionedList) ? 1 : (((PXView.VersionedList) list).AnyMerged ? 1 : 0))) != 0;
        }
        bool filterExists = filters != null && filters.Length != 0;
        bool sortReq = needOverrideSort || this.HasUnboundSort(pxSearchColumnArray);
        bool flag5 = false;
        PXQueryResult v = (PXQueryResult) null;
        if (pxCommandKey != null)
          this.SelectQueries.TryGetValue(pxCommandKey, out v);
        if (pxCommandKey != null && !flag4)
        {
          List<object> items = this.IsReadOnly ? list : (List<object>) new PXView.VersionedList((IEnumerable<object>) this.CloneResult(list), 0);
          this.SelectQueries.StoreCached(this, pxCommandKey, items, this.BqlSelect.Context);
          PXView.VersionedList versionedList1 = items as PXView.VersionedList;
          PXView.VersionedList versionedList2 = list as PXView.VersionedList;
          if (versionedList1 != null && versionedList2 != null)
            versionedList1.MergedList = versionedList2;
          this.RemoveDeleted(list);
        }
        if (pxCommandKey != null & flag4)
          flag5 |= this.InternCache(ref list, ref sortReq);
        if ((this.BqlSelect.Context == null || !this.BqlSelect.Context.BadParametersSkipMergeCache) && (v == null || !v.BadParamsSkipMergeCache))
          flag5 |= this.MergeCache(list, parameters, filterExists, ref sortReq);
        flag1 |= flag5;
        if (sortReq && list.Count > 1 && pxSearchColumnArray.Length != 0)
          this.SortResult(list, pxSearchColumnArray, reverseOrder);
      }
      else if (list.Count > 0 && !(list[0] is PXResult) && this._AlwaysReturnPXResult)
      {
        for (int index = 0; index < list.Count; ++index)
          list[index] = this.CreateResult(new object[1]
          {
            list[index]
          });
      }
      if (flag1 && !this.FiltersResetRequired && (pxDelegateResult != null ? (!pxDelegateResult.IsResultFiltered ? 1 : 0) : 1) != 0)
        this.FilterResult(list, filters);
      if ((pxDelegateResult != null ? (!pxDelegateResult.IsResultTruncated ? 1 : 0) : 1) != 0)
      {
        bool searchFound;
        list = this.SearchResult(list, pxSearchColumnArray, reverseOrder, num1 == -2, ref startRow, maximumRows, ref totalRows, out searchFound);
        int num4;
        if (!reverseOrder)
        {
          if (num1 != -2)
          {
            switch (num2)
            {
              case 0:
                num4 = maximumRows == 1 ? 1 : 0;
                goto label_81;
              case 1:
                break;
              default:
                num4 = 0;
                goto label_81;
            }
          }
          num4 = 1;
        }
        else
          num4 = 0;
label_81:
        int num5 = anySearch ? 1 : 0;
        if (((num4 & num5) == 0 || searchFound ? 0 : (list.Count > 0 ? 1 : 0)) != 0)
          list.Clear();
      }
      else if (pxDelegateResult != null && pxDelegateResult.IsResultTruncated)
        totalRows = list.Count;
      string str;
      if ((!this._Graph.ViewNames.TryGetValue(this, out str) ? 0 : (str == this._Graph.PrimaryView ? 1 : 0)) != 0)
        this.ApplyWorkflow(list);
    }
    currentSample?.SelectTimer.Stop();
    this._Select = select;
    return list;
  }

  private int AdjustTopCountByChangedRecords(int topCount, object[] searches, string[] sortcolumns)
  {
    if (searches != null && sortcolumns != null && this.Cache.Keys.All<string>((Func<string, bool>) (c => ((IEnumerable<string>) sortcolumns).Contains<string>(c, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))) && ((IEnumerable<object>) searches).All<object>((Func<object, bool>) (c => c != null)) || this._IsReadOnly || topCount <= 0 || !this.Graph.Caches.ContainsKey(this.CacheType))
      return topCount;
    foreach (object obj in this.Cache.Deleted)
      ++topCount;
    foreach (object obj in this.Cache.Updated)
      ++topCount;
    return topCount;
  }

  public virtual List<object> SelectWindowed(
    object[] currents,
    object[] parameters,
    string[] sortcolumns,
    bool[] descendings,
    int startRow,
    int maximumRows)
  {
    BqlCommand select = this._Select;
    PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
    if (currentSample != null)
    {
      ++currentSample.SelectCount;
      currentSample.SelectTimer.Start();
    }
    bool resetTopCount = false;
    PXView.PXSearchColumn[] sorts = this.prepareSorts(sortcolumns, descendings, (object[]) null, maximumRows, out bool _, out bool _, ref resetTopCount);
    parameters = this.PrepareParameters(currents, parameters);
    List<object> resultWindowedRo = this.GetResultWindowedRO(parameters, sorts, startRow, maximumRows);
    string str;
    if ((!this._Graph.ViewNames.TryGetValue(this, out str) ? 0 : (str == this._Graph.PrimaryView ? 1 : 0)) != 0)
      this.ApplyWorkflow(resultWindowedRo);
    currentSample?.SelectTimer.Stop();
    this._Select = select;
    return resultWindowedRo;
  }

  private void ApplyWorkflow(List<object> list)
  {
    if (!this._Graph.IsWorkflowServiceEnabled())
      return;
    foreach (object row in list)
      this.Cache.Current = PXResult.UnwrapFirst(row);
    this._Graph.EnsureIfArchived(this);
    if (list.Count > 0)
    {
      object record = PXResult.UnwrapFirst(list.Last<object>());
      this.Cache.Current = record;
      this._Graph.ApplyWorkflowState(record);
    }
    this._Graph.EnsureIfArchived(this);
  }

  private void EnsureCreateInstance(System.Type[] tables)
  {
    if (tables.Length <= 1 && (!this._AlwaysReturnPXResult || tables.Length == 0) || this._CreateInstance != null)
      return;
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXView._CreateInstanceLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      PXCreateInstanceKey key = new PXCreateInstanceKey(tables);
      if (PXView._CreateInstanceDict.TryGetValue(key, out this._CreateInstance))
        return;
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if (PXView._CreateInstanceDict.TryGetValue(key, out this._CreateInstance))
        return;
      System.Type type = (System.Type) null;
      if (!typeof (IBqlTable).IsAssignableFrom(tables[tables.Length - 1]))
      {
        System.Type[] destinationArray = new System.Type[tables.Length - 1];
        Array.Copy((Array) tables, (Array) destinationArray, destinationArray.Length);
        tables = destinationArray;
      }
      switch (tables.Length)
      {
        case 1:
          type = typeof (PXResult<>).MakeGenericType(tables);
          break;
        case 2:
          type = typeof (PXResult<,>).MakeGenericType(tables);
          break;
        case 3:
          type = typeof (PXResult<,,>).MakeGenericType(tables);
          break;
        case 4:
          type = typeof (PXResult<,,,>).MakeGenericType(tables);
          break;
        case 5:
          type = typeof (PXResult<,,,,>).MakeGenericType(tables);
          break;
        case 6:
          type = typeof (PXResult<,,,,,>).MakeGenericType(tables);
          break;
        case 7:
          type = typeof (PXResult<,,,,,,>).MakeGenericType(tables);
          break;
        case 8:
          type = typeof (PXResult<,,,,,,,>).MakeGenericType(tables);
          break;
        case 9:
          type = typeof (PXResult<,,,,,,,,>).MakeGenericType(tables);
          break;
        case 10:
          type = typeof (PXResult<,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 11:
          type = typeof (PXResult<,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 12:
          type = typeof (PXResult<,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 13:
          type = typeof (PXResult<,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 14:
          type = typeof (PXResult<,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 15:
          type = typeof (PXResult<,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 16 /*0x10*/:
          type = typeof (PXResult<,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 17:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 18:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 19:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 20:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 21:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 22:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 23:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 24:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
        case 25:
          type = typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,,>).MakeGenericType(tables);
          break;
      }
      DynamicMethod dynamicMethod;
      if (!PXGraph.IsRestricted)
        dynamicMethod = new DynamicMethod("_CreateInstance", typeof (object), new System.Type[1]
        {
          typeof (object[])
        }, typeof (PXView));
      else
        dynamicMethod = new DynamicMethod("_CreateInstance", typeof (object), new System.Type[1]
        {
          typeof (object[])
        });
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      int[] numArray = new int[tables.Length];
      for (int index = 0; index < tables.Length; ++index)
      {
        numArray[index] = ilGenerator.DeclareLocal(tables[index]).LocalIndex;
        ilGenerator.Emit(OpCodes.Ldarg_0);
        ilGenerator.Emit(OpCodes.Ldc_I4, index);
        ilGenerator.Emit(OpCodes.Ldelem_Ref);
        if (tables[index].IsValueType)
          ilGenerator.Emit(OpCodes.Unbox_Any, tables[index]);
        else
          ilGenerator.Emit(OpCodes.Castclass, tables[index]);
        ilGenerator.Emit(OpCodes.Stloc, numArray[index]);
      }
      for (int index = 0; index < tables.Length; ++index)
        ilGenerator.Emit(OpCodes.Ldloc, numArray[index]);
      ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(tables));
      ilGenerator.Emit(OpCodes.Ret);
      PXView._CreateInstanceDict[key] = this._CreateInstance = (PXView._InstantiateDelegate) dynamicMethod.CreateDelegate(typeof (PXView._InstantiateDelegate));
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  private void EnsureDelegate()
  {
    if (this._CustomMethod != null)
      return;
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(PXView._InvokeLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      System.Type type1 = this._Delegate.GetType();
      if (PXView._InvokeDict.TryGetValue(type1, out this._CustomMethod))
        return;
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if (PXView._InvokeDict.TryGetValue(type1, out this._CustomMethod))
        return;
      DynamicMethod dynamicMethod;
      if (!PXGraph.IsRestricted)
        dynamicMethod = new DynamicMethod("_CustomMethod", typeof (List<object>), new System.Type[2]
        {
          typeof (object),
          typeof (object[])
        }, typeof (PXView), true);
      else
        dynamicMethod = new DynamicMethod("_CustomMethod", typeof (List<object>), new System.Type[2]
        {
          typeof (object),
          typeof (object[])
        }, true);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ParameterInfo[] parameters = this._Delegate.Method.GetParameters();
      int[] numArray = new int[parameters.Length];
      bool flag = this._Delegate.Method.ReturnType == typeof (void);
      for (int index = 0; index < parameters.Length; ++index)
      {
        System.Type type2 = flag ? parameters[index].ParameterType.GetElementType() : parameters[index].ParameterType;
        numArray[index] = ilGenerator.DeclareLocal(type2).LocalIndex;
        ilGenerator.Emit(OpCodes.Ldarg_1);
        ilGenerator.Emit(OpCodes.Ldc_I4, index);
        ilGenerator.Emit(OpCodes.Ldelem_Ref);
        if (type2.IsValueType)
          ilGenerator.Emit(OpCodes.Unbox_Any, type2);
        else
          ilGenerator.Emit(OpCodes.Castclass, type2);
        ilGenerator.Emit(OpCodes.Stloc, numArray[index]);
      }
      LocalBuilder localBuilder = ilGenerator.DeclareLocal(typeof (List<object>));
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, type1);
      for (int index = 0; index < parameters.Length; ++index)
      {
        if (flag)
          ilGenerator.Emit(OpCodes.Ldloca_S, numArray[index]);
        else
          ilGenerator.Emit(OpCodes.Ldloc_S, numArray[index]);
      }
      ilGenerator.Emit(OpCodes.Callvirt, type1.GetMethod("Invoke"));
      if (flag)
      {
        ilGenerator.Emit(OpCodes.Ldnull);
        ilGenerator.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
      }
      else
      {
        ilGenerator.Emit(OpCodes.Call, typeof (PXView).GetMethod("ToList", BindingFlags.Static | BindingFlags.NonPublic));
        ilGenerator.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
      }
      if (flag)
      {
        for (int index = 0; index < parameters.Length; ++index)
        {
          System.Type elementType = parameters[index].ParameterType.GetElementType();
          ilGenerator.Emit(OpCodes.Ldarg_1);
          ilGenerator.Emit(OpCodes.Ldc_I4, index);
          ilGenerator.Emit(OpCodes.Ldloc, numArray[index]);
          if (elementType.IsValueType)
            ilGenerator.Emit(OpCodes.Box, elementType);
          ilGenerator.Emit(OpCodes.Stelem_Ref);
        }
      }
      ilGenerator.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
      ilGenerator.Emit(OpCodes.Ret);
      PXView._InvokeDict[type1] = this._CustomMethod = (PXView._InvokeDelegate) dynamicMethod.CreateDelegate(typeof (PXView._InvokeDelegate));
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  protected static List<object> ToList(IEnumerable src)
  {
    switch (src)
    {
      case null:
        return (List<object>) null;
      case PXDelegateResult collection1:
        PXDelegateResult list1 = new PXDelegateResult();
        list1.Capacity = collection1.Capacity;
        list1.AddRange((IEnumerable<object>) collection1);
        list1.IsResultFiltered = collection1.IsResultFiltered;
        list1.IsResultSorted = collection1.IsResultSorted;
        list1.IsResultTruncated = collection1.IsResultTruncated;
        return (List<object>) list1;
      case PXDelegateCacheResult list2:
        return (List<object>) list2;
      case List<object> collection2:
        return new List<object>((IEnumerable<object>) collection2);
      default:
        return src.OfType<object>().ToList<object>();
    }
  }

  /// <summary>Invokes the manual method if provided in the constructor</summary>
  /// <param name="parameters">Query parameters</param>
  /// <returns>Either resultset or null, if the method is intended to override parameters only</returns>
  protected virtual List<object> InvokeDelegate(object[] parameters)
  {
    List<object> objectList = (List<object>) null;
    if ((object) this._Delegate != null)
    {
      ParameterInfo[] parameters1 = this._Delegate.Method.GetParameters();
      IBqlParameter[] parameters2 = this._Select.GetParameters();
      object[] parameters3 = new object[parameters1.Length];
      if (parameters != null)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < parameters2.Length && index1 < parameters1.Length; ++index2)
        {
          if (parameters2[index2].IsArgument)
          {
            parameters3[index1] = index2 >= parameters.Length ? (object) null : parameters[index2];
            ++index1;
          }
        }
        int length = parameters2.Length;
        for (; index1 < parameters3.Length; ++index1)
        {
          if (length < parameters.Length)
          {
            parameters3[index1] = parameters[length];
            ++length;
          }
          else
            parameters3[index1] = (object) null;
        }
      }
      this.EnsureDelegate();
      objectList = this._CustomMethod(this._Delegate, parameters3);
      if (parameters != null)
      {
        int index3 = 0;
        for (int index4 = 0; index4 < parameters2.Length && index3 < parameters1.Length; ++index4)
        {
          if (parameters2[index4].IsArgument)
          {
            if (index4 < parameters.Length)
              parameters[index4] = parameters3[index3];
            ++index3;
          }
        }
        for (int length = parameters2.Length; index3 < parameters3.Length && length < parameters.Length; ++index3)
        {
          parameters[length] = parameters3[index3];
          ++length;
        }
      }
    }
    return objectList;
  }

  protected int CompareMethod(
    object a,
    object b,
    PXCache cache,
    string fieldName,
    bool descending,
    bool useExt,
    System.Type tableType,
    PXCollationComparer collationComparer,
    Dictionary<string, string> valueLabelDic,
    ref ISqlDialect dialect)
  {
    if (tableType != (System.Type) null)
    {
      a = ((PXResult) a)[tableType];
      b = ((PXResult) b)[tableType];
    }
    object obj1;
    if (!useExt)
    {
      obj1 = cache.GetValue(a, fieldName);
    }
    else
    {
      obj1 = cache.GetValueExt(a, fieldName);
      if (obj1 is PXFieldState)
        obj1 = ((PXFieldState) obj1).Value;
      if (valueLabelDic != null && valueLabelDic.Count > 0 && obj1 is string)
      {
        string str;
        obj1 = !valueLabelDic.TryGetValue((string) obj1, out str) ? (object) null : (object) str;
      }
    }
    if (!(obj1 is IComparable) && obj1 != null)
      return descending ? 1 : -1;
    object obj2;
    if (!useExt)
    {
      obj2 = cache.GetValue(b, fieldName);
    }
    else
    {
      obj2 = cache.GetValueExt(b, fieldName);
      if (obj2 is PXFieldState)
        obj2 = ((PXFieldState) obj2).Value;
      if (valueLabelDic != null && valueLabelDic.Count > 0 && obj2 is string)
      {
        string str;
        obj2 = !valueLabelDic.TryGetValue((string) obj2, out str) ? (object) null : (object) str;
      }
    }
    if (obj1 == null && obj2 == null)
      return 0;
    int num;
    if (obj1 == null)
      num = -1;
    else if (obj2 == null)
    {
      num = 1;
    }
    else
    {
      switch (obj1)
      {
        case string _ when obj2 is string:
          num = collationComparer.Compare((string) obj1, (string) obj2);
          break;
        case Guid _ when obj2 is Guid:
          if (dialect == null)
            dialect = cache.Graph.SqlDialect;
          num = PXView.compareGuid((Guid) obj1, (Guid) obj2, dialect);
          break;
        default:
          num = ((IComparable) obj1).CompareTo(obj2);
          break;
      }
    }
    return !descending ? num : -num;
  }

  protected int SearchMethod(
    object a,
    object bVal,
    PXCache cache,
    string fieldName,
    bool descending,
    bool useExt,
    System.Type tableType,
    Dictionary<string, string> valueLabelDic,
    ref ISqlDialect dialect)
  {
    if (tableType != (System.Type) null)
      a = ((PXResult) a)[tableType];
    object obj;
    if (!useExt)
    {
      obj = cache.GetValue(a, fieldName);
    }
    else
    {
      obj = cache.GetValueExt(a, fieldName);
      if (obj is PXFieldState)
        obj = ((PXFieldState) obj).Value;
    }
    if (!(obj is IComparable))
      return descending ? 1 : -1;
    int num;
    if (obj is string && bVal is string)
    {
      if (useExt && valueLabelDic != null)
      {
        string str;
        if (valueLabelDic.TryGetValue((string) obj, out str))
          obj = (object) str;
        if (valueLabelDic.TryGetValue((string) bVal, out str))
          bVal = (object) str;
      }
      num = PXLocalesProvider.CollationComparer.Compare((string) obj, (string) bVal);
    }
    else if (obj is Guid && bVal is Guid)
    {
      if (dialect == null)
        dialect = cache.Graph.SqlDialect;
      num = PXView.compareGuid((Guid) obj, (Guid) bVal, dialect);
    }
    else
      num = ((IComparable) obj).CompareTo(bVal);
    if (descending && num != 0)
      num = -num;
    return num;
  }

  /// <summary>Compare two items</summary>
  /// <param name="a">First item, might be a dictionary</param>
  /// <param name="b">Second item, might be a dictionary</param>
  /// <param name="columns">Sort columns</param>
  /// <param name="descendings">Sort descendings</param>
  /// <returns>-1, 0, 1</returns>
  protected virtual int Compare(object a, object b, PXView.compareDelegate[] comparisons)
  {
    if (a == b)
      return 0;
    for (int index = 0; index < comparisons.Length; ++index)
    {
      int num = comparisons[index](a, b);
      if (num != 0)
        return num;
    }
    return 0;
  }

  protected virtual int Search(object a, PXView.searchDelegate[] comparisons)
  {
    for (int index = 0; index < comparisons.Length; ++index)
    {
      int num = comparisons[index](a);
      if (num != 0)
        return num;
    }
    return 0;
  }

  /// <summary>Retrieves the whole data set corresponding to the BQL command.</summary>
  /// <param name="parameters">The explicit values for such parameters as <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  public virtual List<object> SelectMulti(params object[] parameters)
  {
    int startRow = 0;
    int totalRows = 0;
    return this.Select((object[]) null, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows);
  }

  /// <summary>Retrieves the top data record from the data set corresponding to the BQL command.</summary>
  /// <param name="parameters">The explicit values for such parameters as
  /// <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  public virtual object SelectSingle(params object[] parameters)
  {
    int startRow = 0;
    int totalRows = 0;
    List<object> objectList = this.Select((object[]) null, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
    return objectList.Count == 0 ? (object) null : objectList[0];
  }

  /// <summary>Retrieves the top data record from the data set corresponding to the BQL command.</summary>
  /// <param name="currents">The objects to use as current data records when
  /// processing <tt>Current</tt> and <tt>Optional</tt> parameters.</param>
  /// <param name="parameters">The explicit values for such parameters as
  /// <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  /// <returns>The resultset.</returns>
  public virtual object SelectSingleBound(object[] currents, params object[] parameters)
  {
    int startRow = 0;
    int totalRows = 0;
    List<object> objectList = this.Select(currents, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows);
    return objectList.Count == 0 ? (object) null : objectList[0];
  }

  /// <summary>Retrieves the whole data set corresponding to the BQL command.</summary>
  /// <param name="currents">The objects to use as current data records when
  /// processing <tt>Current</tt> and <tt>Optional</tt> parameters.</param>
  /// <param name="parameters">The explicit values for such parameters as
  /// <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  public virtual List<object> SelectMultiBound(object[] currents, params object[] parameters)
  {
    int startRow = 0;
    int totalRows = 0;
    return this.Select(currents, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows);
  }

  /// <summary>Retrieves the whole data set corresponding to the BQL command.</summary>
  /// <param name="currents">The objects to use as current data records when
  /// processing <tt>Current</tt> and <tt>Optional</tt> parameters.</param>
  /// <param name="parameters">The explicit values for such parameters as
  /// <tt>Required</tt>, <tt>Optional</tt>, and <tt>Argument</tt>.</param>
  [PXInternalUseOnly]
  public List<object> SelectMultiBoundSortAsImplicit(
    object[] currents,
    string[] sortAsImplicitColumns,
    params object[] parameters)
  {
    int startRow = 0;
    int totalRows = 0;
    return this.Select(currents, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows, sortAsImplicitColumns);
  }

  /// <summary>Appends a filtering expression to the underlying BQL command
  /// via the logical "and". The additional filtering expression is provided
  /// in the type parameter.</summary>
  public void WhereAnd<TWhere>() where TWhere : IBqlWhere, new()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereAnd<TWhere>();
  }

  /// <summary>Appends a filtering expression to the underlying BQL command
  /// via the logical "and". The additional filtering expression is provided
  /// in the type parameter.</summary>
  /// <param name="where">The additional filtering expression as the type
  /// derived from <tt>IBqlWhere</tt>.</param>
  public void WhereAnd(System.Type where)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereAnd(where);
  }

  /// <summary>Appends a filtering expression to the BQL statement via the
  /// logical "or". The additional filtering expression is provided in the
  /// type parameter.</summary>
  public void WhereOr<TWhere>() where TWhere : IBqlWhere, new()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereOr<TWhere>();
  }

  /// <summary>Appends a filtering expression to the BQL statement via the
  /// logical "or".</summary>
  /// <param name="where">The additional filtering expression as the type
  /// derived from <tt>IBqlWhere</tt>.</param>
  public void WhereOr(System.Type where)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereOr(where);
  }

  /// <summary>Replaces the sorting expression with the new sorting
  /// expression. The sorting expressio is specified in the type
  /// parameter.</summary>
  public void OrderByNew<newOrderBy>() where newOrderBy : IBqlOrderBy, new()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._Select = this._Select.OrderByNew<newOrderBy>();
    this.FixSortsForOrderByNew();
  }

  /// <summary>Replaces the sorting expression with the new sorting
  /// expression.</summary>
  /// <param name="newOrderBy">The sorting expression as a type derived from
  /// <tt>IBqlOrderBy</tt>, such as <tt>OrderBy&lt;&gt;</tt>.</param>
  public void OrderByNew(System.Type newOrderBy)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._Select = this._Select.OrderByNew(newOrderBy);
    this.FixSortsForOrderByNew();
  }

  private void FixSortsForOrderByNew()
  {
    bool resetTopCount = false;
    PXView.PXSearchColumn[] pxSearchColumnArray = this.prepareSorts((string[]) null, (bool[]) null, (object[]) null, 0, out bool _, out bool _, ref resetTopCount);
    if (PXView.View != this)
      return;
    PXView.Sorts.Clear();
    PXView.Sorts.Add(pxSearchColumnArray);
  }

  /// <summary>Appends the provided join clause to the BQL command. The join
  /// clause is specified in the type parameter.</summary>
  public void Join<join>() where join : IBqlJoin, new()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = BqlCommand.AppendJoin<join>(this._Select);
  }

  /// <summary>Appends the provided join clause to the BQL
  /// command.</summary>
  /// <param name="join">The join clause as a type derived from
  /// <tt>IBqlJoin</tt>.</param>
  public void Join(System.Type join)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = (BqlCommand) Activator.CreateInstance(BqlCommand.AppendJoin(this._Select.GetSelectType(), join));
  }

  /// <summary>Appends or sets the provided join clause to the BQL command.</summary>
  /// <param name="join">The join clause as a type derived from
  /// <tt>IBqlJoin</tt>.</param>
  public void JoinNew(System.Type join)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._CreateInstance = (PXView._InstantiateDelegate) null;
    this._Select = (BqlCommand) Activator.CreateInstance(BqlCommand.NewJoin(this._Select.GetSelectType(), join));
  }

  /// <summary>Adds logical "not" to the whole <tt>Where</tt> clause of the BQL statement, reversing the condition to the opposite.</summary>
  public void WhereNot()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereNot();
  }

  /// <summary>Replaces the filtering expression in the BQL statement. The
  /// new filtering expression is provided in the type parameter.</summary>
  public void WhereNew<newWhere>() where newWhere : IBqlWhere, new()
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereNew<newWhere>();
  }

  /// <summary>Replaces the filtering expression in the BQL
  /// statement.</summary>
  /// <param name="newWhere">The new filtering expression as the type
  /// derived from <tt>IBqlWhere</tt>.</param>
  public void WhereNew(System.Type newWhere)
  {
    if (this.IsNonStandardView)
      this.Clear();
    else
      this._SelectQueries = (PXViewQueryCollection) null;
    this._IsCommandMutable = true;
    this._ParameterNames = (string[]) null;
    this._Select = this._Select.WhereNew(newWhere);
  }

  protected void OnRefreshRequested(object sender, EventArgs e)
  {
    if (this.RefreshRequested == null)
      return;
    this.RefreshRequested(sender, e);
  }

  /// <summary>Raises the <tt>RequestRefresh</tt> event defined within the
  /// <tt>PXView</tt> object.
  /// The method refreshes only grid control and may be called from RowSelected, RowInserted, RowUpdated and RowDeleted events.
  /// </summary>
  public void RequestRefresh() => this.OnRefreshRequested((object) this, new EventArgs());

  /// <summary>Gets or sets the value indicating user's choice in the dialog window displayed through one of the <tt>Ask()</tt> methods.</summary>
  public WebDialogResult Answer
  {
    get => this.GetAnswer(string.Empty);
    set => this.SetAnswer(string.Empty, value);
  }

  /// <summary>Returns the result of the dialog window that was opened through one of the <tt>Ask()</tt> methods and saved in the <tt>PXView</tt> object.</summary>
  /// <param name="key">The identifier of the dialog window that was
  /// provided to the <tt>Ask()</tt> method or the name of the data
  /// view.</param>
  public WebDialogResult GetAnswer(string key) => DialogManager.GetAnswer(this, key);

  /// <summary>Saves the result of the dialog window.</summary>
  /// <param name="key">The identifier of the dialog window.</param>
  /// <param name="answer">The result value.</param>
  public void SetAnswer(string key, WebDialogResult answer)
  {
    DialogManager.SetAnswer(this, key, answer);
  }

  /// <summary>Allows to get or set the values indicating user's choice in the dialog window displayed through one of the <tt>Ask()</tt> methods.</summary>
  public PXView.AnswerIndexer Answers { get; }

  /// <summary>Saves the result of the dialog window.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  /// <param name="viewName">The name of the data view with which the dialog
  /// window is associated.</param>
  /// <param name="key">The identifier of the dialog window.</param>
  /// <param name="answer">The result value.</param>
  public static void SetAnswer(PXGraph graph, string viewName, string key, WebDialogResult answer)
  {
    DialogManager.SetAnswer(graph, viewName, key, answer);
  }

  /// <summary>Clears the dialog information saved by the graph on last invocation of the <tt>Ask()</tt> method.</summary>
  public void ClearDialog() => DialogManager.Clear(this.Graph);

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return DialogManager.Ask(this, key, row, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.Ask(key, row, header, message, buttons, icon, false);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool refreshRequired)
  {
    return this.Ask((string) null, row, header, message, buttons, icon, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="header">Dialog box caption.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="icon">Dialog box icon.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon)
  {
    return this.Ask((string) null, row, header, message, buttons, icon);
  }

  public WebDialogResult Ask(
    object row,
    string header,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames,
    MessageIcon icon)
  {
    return DialogManager.Ask(this, (string) null, row, header, message, buttons, buttonNames, icon, false);
  }

  public WebDialogResult Ask(
    object row,
    string header,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames,
    MessageIcon icon,
    bool repaintControls)
  {
    return DialogManager.Ask(this, (string) null, row, header, message, buttons, buttonNames, icon, repaintControls);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.Ask(key, (object) null, string.Empty, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string key, string message, MessageButtons buttons)
  {
    return this.Ask(key, (object) null, string.Empty, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string message, MessageButtons buttons, bool refreshRequired)
  {
    return this.Ask((string) null, (object) null, string.Empty, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string message, MessageButtons buttons)
  {
    return this.Ask((string) null, (object) null, string.Empty, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    string key,
    object row,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.Ask(key, row, string.Empty, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="key">The identifier of the dialog box (<tt>PXSmartPanel</tt>).</param>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(string key, object row, string message, MessageButtons buttons)
  {
    return this.Ask(key, row, string.Empty, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.
  /// The data for the dialog box can be read from the server or the cache.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <param name="refreshRequired">If <c>true</c>, the data for the dialog box is read from the server, and some or all controls of the form are refreshed.
  /// If <c>false</c>, the data for the dialog box is first searched in the cache.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(
    object row,
    string message,
    MessageButtons buttons,
    bool refreshRequired)
  {
    return this.Ask((string) null, row, string.Empty, message, buttons, MessageIcon.None, refreshRequired);
  }

  /// <summary>Displays the dialog box (<tt>PXSmartPanel</tt>) with one or more choices for the user.</summary>
  /// <param name="row">The parameter is never used.</param>
  /// <param name="message">The message to display in the dialog box.</param>
  /// <param name="buttons">A <see cref="T:PX.Data.MessageButtons">MessageButtons</see> value indicating
  /// the set of buttons to display in the dialog box.</param>
  /// <returns>The <see cref="T:PX.Data.WebDialogResult">WebDialogResult</see> value that
  /// indicates the clicked button.</returns>
  public WebDialogResult Ask(object row, string message, MessageButtons buttons)
  {
    return this.Ask((string) null, row, string.Empty, message, buttons, MessageIcon.None);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(string key, bool refreshRequired)
  {
    return DialogManager.AskExt(this, key, (DialogManager.InitializePanel) null, refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. The method requests repainting of the
  /// panel.</summary>
  /// <param name="key">The identifier of the panel.</param>
  public WebDialogResult AskExt(string key) => this.AskExt(key, false);

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement.</summary>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(bool refreshRequired)
  {
    return DialogManager.AskExt(this, (string) null, (DialogManager.InitializePanel) null, refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement. The method requests
  /// repainting of the panel.</summary>
  public WebDialogResult AskExt() => this.AskExt(false);

  public WebDialogResult AskExtWithHeader(string header, params string[] commitFields)
  {
    return this.AskExtWithHeader(header, ((IEnumerable<string>) commitFields).ToList<string>());
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. Header is displayed as a panel title</summary>
  public WebDialogResult AskExtWithHeader(string header, List<string> commitFields = null)
  {
    return DialogManager.AskExt(this, header, commitFields);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. Header is displayed as a panel title</summary>
  public WebDialogResult AskExtWithHeader(
    string header,
    PXView.InitializePanel initializeHandler,
    List<string> commitFields = null,
    bool repaintControls = false)
  {
    return DialogManager.AskExt(this, header, new DialogManager.InitializePanel(initializeHandler.Invoke), commitFields, repaintControls);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control. As a key, the method uses the name of
  /// the variable that holds the BQL statement. The method requests
  /// repainting of the panel.</summary>
  public WebDialogResult AskExt(MessageButtons buttons) => DialogManager.AskExt(this, buttons);

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(
    string key,
    PXView.InitializePanel initializeHandler,
    bool refreshRequired)
  {
    return DialogManager.AskExt(this, key, new DialogManager.InitializePanel(initializeHandler.Invoke), refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  public WebDialogResult AskExt(string key, PXView.InitializePanel initializeHandler)
  {
    return this.AskExt(key, initializeHandler, false);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  /// <param name="refreshRequired">The value that indicates whether the
  /// dialog should be repainted or displayed as it was cached. If
  /// <tt>true</tt>, the dialog is repainted.</param>
  public WebDialogResult AskExt(PXView.InitializePanel initializeHandler, bool refreshRequired)
  {
    return DialogManager.AskExt(this, (string) null, new DialogManager.InitializePanel(initializeHandler.Invoke), refreshRequired);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  public WebDialogResult AskExt(PXView.InitializePanel initializeHandler)
  {
    return this.AskExt(initializeHandler, false);
  }

  /// <summary>Displays the dialog window configured by the
  /// <tt>PXSmartPanel</tt> control.</summary>
  /// <param name="graph">The graph where the data view is defined.</param>
  /// <param name="viewName">The name of the data view with which the dialog
  /// is associated.</param>
  /// <param name="key">The identifier of the panel to display.</param>
  /// <param name="initializeHandler">The delegate of the method that is
  /// called before the dialog is displayed.</param>
  public static WebDialogResult AskExt(
    PXGraph graph,
    string viewName,
    string key,
    PXView.InitializePanel initializeHandler)
  {
    return DialogManager.AskExt(graph, viewName, key, new DialogManager.InitializePanel(initializeHandler.Invoke), false);
  }

  /// <summary>Gets the value of the specified field in the data record from the cache.</summary>
  /// <param name="sender">The cache object.</param>
  /// <param name="data">The data record.</param>
  /// <param name="sourceType">The DAC of the data record. The cache of this
  /// DAC type is obtained through the cache object provided in the
  /// parameter.</param>
  /// <param name="sourceField">The name of the field which value is
  /// returned.</param>
  /// <remarks>The method may raise the <tt>FieldDefaulting</tt> and
  /// <tt>FieldUpdating</tt> events.</remarks>
  public static object FieldGetValue(
    PXCache sender,
    object data,
    System.Type sourceType,
    string sourceField)
  {
    PXCache cach = sender.Graph.Caches[sourceType];
    object newValue;
    if (PXView.InheritsType(sender.GetItemType(), sourceType))
    {
      newValue = sender.GetValue(data, sourceField);
      if (newValue == null && sender.RaiseFieldDefaulting(sourceField, data, out newValue))
        sender.RaiseFieldUpdating(sourceField, data, ref newValue);
    }
    else if ((cach._Current ?? cach.Current) == null)
    {
      object instance = Activator.CreateInstance(sourceType);
      if (cach.RaiseFieldDefaulting(sourceField, instance, out newValue))
        cach.RaiseFieldUpdating(sourceField, instance, ref newValue);
    }
    else
      newValue = cach.GetValue(cach._Current ?? cach.Current, sourceField);
    return newValue;
  }

  private static bool InheritsType(System.Type ChildType, System.Type BaseType)
  {
    while (ChildType != (System.Type) null && ChildType != BaseType)
      ChildType = ChildType.BaseType;
    return ChildType == BaseType;
  }

  /// <summary>View name in the graph.</summary>
  public string Name
  {
    get
    {
      string str;
      return this.Graph != null && this.Graph.ViewNames.TryGetValue(this, out str) ? str : (string) null;
    }
  }

  /// <exclude />
  public object CreateResult(object[] items)
  {
    if (this._CreateInstance == null)
      this.EnsureCreateInstance(this._Select.GetTables());
    return this._CreateInstance == null ? (object) null : (object) (PXResult) this._CreateInstance(items);
  }

  /// <summary>A <see cref="T:PX.Data.PXView" /> that always returns only a predefined set of rows</summary>
  /// <exclude />
  public sealed class Dummy : PXView
  {
    private readonly List<object> _records;

    public Dummy(PXGraph graph, BqlCommand command, List<object> records)
      : base(graph, true, command)
    {
      this._records = records;
    }

    public override List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      return this._records;
    }

    public static PXView.Dummy For<TTable>(PXGraph graph) where TTable : class, IBqlTable, new()
    {
      return PXView.Dummy.For<TTable>(graph, EnumerableExtensions.AsSingleEnumerable<TTable>((TTable) graph.Caches<TTable>().Current));
    }

    public static PXView.Dummy For<TTable>(PXGraph graph, IEnumerable<TTable> items) where TTable : class, IBqlTable, new()
    {
      return new PXView.Dummy(graph, (BqlCommand) new PX.Data.Select<TTable>(), ((IEnumerable<object>) items).ToList<object>());
    }
  }

  [Serializable]
  protected internal sealed class VersionedList : List<object>
  {
    public int Version = -1;
    public bool AnyMerged;
    protected internal PXView.VersionedList MergedList;

    public VersionedList()
    {
    }

    public VersionedList(IEnumerable<object> collection, int Version)
      : base(collection)
    {
      this.Version = Version;
    }
  }

  protected sealed class Context
  {
    public readonly PXView View;
    public PXView.PXSearchColumn[] Sorts;
    public PXFilterRow[] Filters;
    public readonly object[] Currents;
    public readonly object[] Parameters;
    public bool ReverseOrder;
    public int StartRow;
    public readonly int MaximumRows;
    public readonly RestrictedFieldsSet RestrictedFields;
    public readonly bool RetrieveTotalRowCount;

    public Context(
      PXView view,
      PXView.PXSearchColumn[] sorts,
      PXFilterRow[] filters,
      object[] currents,
      object[] parameters,
      bool reverseOrder,
      int startRow,
      int maximumRows,
      bool retrieveTotalRowCount,
      RestrictedFieldsSet restrictedFields)
    {
      this.View = view;
      this.Sorts = sorts;
      this.Filters = filters;
      this.Currents = currents;
      this.Parameters = parameters;
      this.ReverseOrder = reverseOrder;
      this.StartRow = startRow;
      this.MaximumRows = maximumRows;
      this.RetrieveTotalRowCount = retrieveTotalRowCount;
      this.RestrictedFields = restrictedFields;
    }
  }

  public sealed class PXFilterRowCollection : IEnumerable
  {
    private PXFilterRow[] _Array;

    public IEnumerator GetEnumerator() => this._Array.GetEnumerator();

    public int Length => this._Array.Length;

    public static implicit operator PXFilterRow[](PXView.PXFilterRowCollection collection)
    {
      return collection._Array;
    }

    public PXFilterRowCollection(PXFilterRow[] source) => this._Array = source;

    public PXFilterRow this[int index] => this._Array[index];

    public void Add(params PXFilterRow[] filters)
    {
      PXView.Context context;
      if (!((IEnumerable<PXFilterRow>) filters).Any<PXFilterRow>() || !PXView.TryPeekExecutingContext(out context))
        return;
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      if (context.Filters != null && context.Filters.Length != 0)
      {
        foreach (PXFilterRow filter in context.Filters)
        {
          pxFilterRowList.Add(new PXFilterRow(filter.DataField, filter.Condition, filter.OrigValue ?? filter.Value, filter.OrigValue2 ?? filter.Value2, filter.Variable));
          pxFilterRowList[pxFilterRowList.Count - 1].OpenBrackets = filter.OpenBrackets;
          pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets = filter.CloseBrackets;
          pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = filter.OrOperator;
        }
        if (pxFilterRowList.Count > 1)
        {
          ++pxFilterRowList[0].OpenBrackets;
          ++pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets;
        }
        pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = false;
      }
      foreach (PXFilterRow filter in filters)
      {
        pxFilterRowList.Add(new PXFilterRow(filter.DataField, filter.Condition, filter.OrigValue ?? filter.Value, filter.OrigValue2 ?? filter.Value2, filter.Variable));
        pxFilterRowList[pxFilterRowList.Count - 1].OpenBrackets = filter.OpenBrackets;
        pxFilterRowList[pxFilterRowList.Count - 1].CloseBrackets = filter.CloseBrackets;
        pxFilterRowList[pxFilterRowList.Count - 1].OrOperator = filter.OrOperator;
      }
      context.Filters = pxFilterRowList.ToArray();
    }

    public void Clear()
    {
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return;
      context.Filters = new PXFilterRow[0];
    }

    internal bool PrepareFilters() => this.PrepareFilters(out bool _);

    internal bool PrepareFilters(out bool skipped)
    {
      skipped = true;
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return false;
      skipped = false;
      return context.View.prepareFilters(ref this._Array);
    }
  }

  protected internal sealed class PXSortColumnCollection : IEnumerable
  {
    private PXView.PXSearchColumn[] _Array;

    public IEnumerator GetEnumerator() => this._Array.GetEnumerator();

    public int Length => this._Array.Length;

    public static implicit operator PXView.PXSearchColumn[](PXView.PXSortColumnCollection collection)
    {
      return collection._Array;
    }

    public PXSortColumnCollection(PXView.PXSearchColumn[] source) => this._Array = source;

    public PXView.PXSearchColumn this[int index] => this._Array[index];

    public void Add(params PXView.PXSearchColumn[] sorts)
    {
      PXView.Context context;
      if (!((IEnumerable<PXView.PXSearchColumn>) sorts).Any<PXView.PXSearchColumn>() || !PXView.TryPeekExecutingContext(out context))
        return;
      List<PXView.PXSearchColumn> pxSearchColumnList = new List<PXView.PXSearchColumn>();
      if (context.Filters != null && context.Filters.Length != 0)
      {
        foreach (PXView.PXSearchColumn sort in context.Sorts)
        {
          PXView.PXSearchColumn pxSearchColumn = new PXView.PXSearchColumn(sort.Column, sort.Descending, sort.SearchValue);
          pxSearchColumn.Description = sort.Description;
          pxSearchColumn.OrigSearchValue = sort.OrigSearchValue;
          pxSearchColumn.UseExt = sort.UseExt;
          pxSearchColumnList.Add(pxSearchColumn);
        }
      }
      foreach (PXView.PXSearchColumn sort in sorts)
      {
        PXView.PXSearchColumn pxSearchColumn1 = (PXView.PXSearchColumn) null;
        foreach (PXView.PXSearchColumn pxSearchColumn2 in pxSearchColumnList)
        {
          if (pxSearchColumn2.Column == sort.Column)
          {
            pxSearchColumn1 = pxSearchColumn2;
            break;
          }
        }
        if (pxSearchColumn1 == null)
        {
          pxSearchColumn1 = new PXView.PXSearchColumn(sort.Column, sort.Descending, (object) null);
          pxSearchColumnList.Add(pxSearchColumn1);
        }
        else
          pxSearchColumn1.Descending = sort.Descending;
        pxSearchColumn1.UseExt = sort.UseExt;
      }
      context.Sorts = pxSearchColumnList.ToArray();
    }

    [Obsolete("Use overload with PXSearchColumn[] argument instead.")]
    public void Add(params PXView.PXSortColumn[] sorts)
    {
      this.Add(((IEnumerable<PXView.PXSortColumn>) sorts).Select<PXView.PXSortColumn, PXView.PXSearchColumn>((Func<PXView.PXSortColumn, PXView.PXSearchColumn>) (s =>
      {
        return new PXView.PXSearchColumn(s.Column, s.Descending, (object) null)
        {
          UseExt = s.UseExt
        };
      })).ToArray<PXView.PXSearchColumn>());
    }

    internal void Clear()
    {
      PXView.Context context;
      if (!PXView.TryPeekExecutingContext(out context))
        return;
      context.Sorts = new PXView.PXSearchColumn[0];
    }
  }

  internal static class DateTimeFactory
  {
    public static double GetDayOfWeek(DayOfWeek day)
    {
      switch (day)
      {
        case DayOfWeek.Sunday:
          return 6.0;
        case DayOfWeek.Monday:
          return 0.0;
        case DayOfWeek.Tuesday:
          return 1.0;
        case DayOfWeek.Wednesday:
          return 2.0;
        case DayOfWeek.Thursday:
          return 3.0;
        case DayOfWeek.Friday:
          return 4.0;
        case DayOfWeek.Saturday:
          return 5.0;
        default:
          return 0.0;
      }
    }

    public static System.DateTime?[] GetDateRange(PXCondition condition, System.DateTime? businessDate)
    {
      businessDate = new System.DateTime?(businessDate ?? System.DateTime.Today);
      System.DateTime? nullable1;
      System.DateTime? nullable2;
      System.DateTime dateTime1;
      switch (condition)
      {
        case PXCondition.TODAY:
          nullable1 = businessDate;
          nullable2 = new System.DateTime?(nullable1.Value.AddDays(1.0));
          break;
        case PXCondition.OVERDUE:
          nullable1 = new System.DateTime?();
          nullable2 = businessDate;
          break;
        case PXCondition.TODAY_OVERDUE:
          nullable1 = new System.DateTime?();
          nullable2 = new System.DateTime?(businessDate.Value.AddDays(1.0));
          break;
        case PXCondition.TOMMOROW:
          nullable1 = new System.DateTime?(businessDate.Value.AddDays(1.0));
          nullable2 = new System.DateTime?(nullable1.Value.AddDays(1.0));
          break;
        case PXCondition.THIS_WEEK:
          nullable1 = businessDate;
          nullable1 = new System.DateTime?(nullable1.Value.AddDays(-PXView.DateTimeFactory.GetDayOfWeek(nullable1.Value.DayOfWeek)));
          nullable2 = new System.DateTime?(nullable1.Value.AddDays(7.0));
          break;
        case PXCondition.NEXT_WEEK:
          nullable1 = businessDate;
          nullable1 = new System.DateTime?(nullable1.Value.AddDays(7.0 - PXView.DateTimeFactory.GetDayOfWeek(nullable1.Value.DayOfWeek)));
          nullable2 = new System.DateTime?(nullable1.Value.AddDays(7.0));
          break;
        case PXCondition.THIS_MONTH:
          nullable1 = businessDate;
          ref System.DateTime? local1 = ref nullable1;
          System.DateTime dateTime2 = nullable1.Value;
          int year1 = dateTime2.Year;
          dateTime2 = nullable1.Value;
          int month1 = dateTime2.Month;
          System.DateTime dateTime3 = new System.DateTime(year1, month1, 1);
          local1 = new System.DateTime?(dateTime3);
          nullable2 = new System.DateTime?(nullable1.Value.AddMonths(1));
          break;
        case PXCondition.NEXT_MONTH:
          nullable1 = businessDate;
          ref System.DateTime? local2 = ref nullable1;
          System.DateTime dateTime4 = nullable1.Value;
          int year2 = dateTime4.Year;
          dateTime4 = nullable1.Value;
          int month2 = dateTime4.Month;
          System.DateTime dateTime5 = new System.DateTime(year2, month2, 1);
          local2 = new System.DateTime?(dateTime5);
          nullable1 = new System.DateTime?(nullable1.Value.AddMonths(1));
          ref System.DateTime? local3 = ref nullable2;
          dateTime1 = nullable1.Value;
          System.DateTime dateTime6 = dateTime1.AddMonths(1);
          local3 = new System.DateTime?(dateTime6);
          break;
        default:
          return (System.DateTime?[]) null;
      }
      if (nullable2.HasValue)
      {
        ref System.DateTime? local4 = ref nullable2;
        dateTime1 = nullable2.Value;
        System.DateTime dateTime7 = dateTime1.AddSeconds(-1.0);
        local4 = new System.DateTime?(dateTime7);
      }
      return new System.DateTime?[2]{ nullable1, nullable2 };
    }
  }

  protected delegate object getPXResultValue(object item);

  protected sealed class HashList : List<object>
  {
    private HashSet<object> _hashset;

    public HashList() => this._hashset = new HashSet<object>();

    public HashList(IEnumerable<object> collection)
      : base(collection)
    {
      this._hashset = new HashSet<object>(collection);
    }

    public new bool Contains(object item) => this._hashset.Contains(item);

    public new void Add(object item)
    {
      base.Add(item);
      this._hashset.Add(item);
    }

    public new void RemoveAt(int index)
    {
      this._hashset.Remove(this[index]);
      base.RemoveAt(index);
    }
  }

  public class PXSortColumn
  {
    public string Column;
    public bool Descending;
    public bool UseExt;
    internal bool CouldUseExt;
    internal bool IsExternalSort;

    public PXSortColumn(string column, bool descending)
    {
      this.Column = column;
      this.Descending = descending;
    }

    public override string ToString()
    {
      return $"{this.GetType().Name}: {this.Column} {(this.Descending ? (object) "DESC" : (object) "ASC")} {(this.UseExt ? (object) "[EXT]" : (object) string.Empty)}";
    }
  }

  public sealed class PXSearchColumn : PXView.PXSortColumn, ICloneable
  {
    public object SearchValue;
    public object OrigSearchValue;
    public PXCommandPreparingEventArgs.FieldDescription Description;
    public System.Type SelSort;

    public PXSearchColumn(string column, bool descending, object searchValue)
      : base(column, descending)
    {
      this.OrigSearchValue = this.SearchValue = searchValue;
    }

    public object Clone()
    {
      PXView.PXSearchColumn pxSearchColumn = new PXView.PXSearchColumn(this.Column, this.Descending, this.SearchValue);
      pxSearchColumn.OrigSearchValue = this.OrigSearchValue;
      pxSearchColumn.SelSort = this.SelSort;
      pxSearchColumn.UseExt = this.UseExt;
      pxSearchColumn.Description = this.Description?.Clone() as PXCommandPreparingEventArgs.FieldDescription;
      pxSearchColumn.IsExternalSort = this.IsExternalSort;
      return (object) pxSearchColumn;
    }

    internal bool IsUnboundSort => this.Description == null;
  }

  /// <summary>
  /// Looks not for absolute equality, but for logical equality. Those columns that are converted to the same SQL
  /// </summary>
  private class PXSearchColumnEqualityComparer : IEqualityComparer<PXView.PXSearchColumn>
  {
    public static PXView.PXSearchColumnEqualityComparer Instance = new PXView.PXSearchColumnEqualityComparer();

    /// <inheritdoc />
    public bool Equals(PXView.PXSearchColumn x, PXView.PXSearchColumn y)
    {
      if (x == null || y == null || x.UseExt != y.UseExt)
        return false;
      return x.Column.Equals(y.Column, StringComparison.OrdinalIgnoreCase) || PXView.PXSearchColumnEqualityComparer.IsSameNotAliasedTableFields(x, y);
    }

    private static bool IsAliasedTableField(PXView.PXSearchColumn x) => x.Column.Contains("_");

    private static bool IsSameNotAliasedTableFields(
      PXView.PXSearchColumn x,
      PXView.PXSearchColumn y)
    {
      return !PXView.PXSearchColumnEqualityComparer.IsAliasedTableField(x) && !PXView.PXSearchColumnEqualityComparer.IsAliasedTableField(y) && PXView.PXSearchColumnEqualityComparer.IsSameTableFields(x, y);
    }

    private static bool IsSameTableFields(PXView.PXSearchColumn x, PXView.PXSearchColumn y)
    {
      PXCommandPreparingEventArgs.FieldDescription description = x.Description;
      if ((description != null ? (description.BqlTable.Equals(y.Description?.BqlTable) ? 1 : 0) : 0) == 0)
        return false;
      if (x.Description.Expr.Equals(y.Description.Expr) || x.Description.Expr is Column expr1 && expr1.Name.Equals(y.Column, StringComparison.OrdinalIgnoreCase) && expr1.Table() is SimpleTable simpleTable1 && simpleTable1.Name == y.Description.BqlTable.Name)
        return true;
      return y.Description.Expr is Column expr2 && expr2.Name.Equals(x.Column, StringComparison.OrdinalIgnoreCase) && expr2.Table() is SimpleTable simpleTable2 && simpleTable2.Name == x.Description.BqlTable.Name;
    }

    /// <inheritdoc />
    public int GetHashCode(PXView.PXSearchColumn col)
    {
      int num1 = 13 * 31 /*0x1F*/ + col.UseExt.GetHashCode();
      if (col.Description?.BqlTable == (System.Type) null)
        return num1 * 31 /*0x1F*/ + col.Column.ToLower().GetHashCode();
      int num2 = num1 * 31 /*0x1F*/ + col.Description.BqlTable.GetHashCode();
      return !(col.Description.Expr is Column expr) ? num2 * 31 /*0x1F*/ + col.Column.ToLower().GetHashCode() : num2 * 31 /*0x1F*/ + expr.Name.ToLower().GetHashCode();
    }
  }

  private delegate List<object> _InvokeDelegate(Delegate method, object[] parameters);

  private delegate object _InstantiateDelegate(object[] parameters);

  protected delegate int compareDelegate(object a, object b);

  protected delegate int searchDelegate(object a);

  public class AnswerIndexer
  {
    private readonly PXView _view;

    internal AnswerIndexer(PXView view) => this._view = view;

    /// <summary>The result of the dialog window that was opened
    /// through one of the <tt>Ask()</tt> methods and saved in the
    /// <tt>PXView</tt> object.</summary>
    /// <param name="key">The identifier of the dialog window that was
    /// provided to the <tt>Ask()</tt> method or the name of the data
    /// view.</param>
    public WebDialogResult this[string key]
    {
      get => this._view.GetAnswer(key);
      set => this._view.SetAnswer(key, value);
    }
  }

  public delegate void InitializePanel(PXGraph graph, string viewName);
}
