// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqView
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal abstract class SQLinqView : PXView
{
  internal static bool? LegacyQueryCacheModeIsOn;
  protected readonly PXGraph _graph;
  protected readonly SQLinqBqlCommand _select;
  protected readonly SQLinqExecutor _linqExecutor;

  protected override List<object> GetResult(
    object[] parameters,
    PXFilterRow[] filters,
    bool reverseOrder,
    int topCount,
    PXView.PXSearchColumn[] sorts,
    ref bool overrideSort,
    ref bool extFilter)
  {
    List<object> result = base.GetResult(parameters, filters, reverseOrder, topCount, sorts, ref overrideSort, ref extFilter);
    foreach (MergedItemWrapper mergedItemWrapper in result.OfType<MergedItemWrapper>())
    {
      if (mergedItemWrapper.DataItem is IGroupingResult dataItem)
        dataItem.MaterializeGroup();
    }
    return result;
  }

  protected SQLinqView(PXGraph graph, SQLinqBqlCommand select, SQLinqExecutor linqExecutor)
    : base(graph, linqExecutor.GetDacTypeToMerge() == (System.Type) null, (BqlCommand) select)
  {
    this._graph = graph;
    this._select = select;
    this._linqExecutor = linqExecutor;
    this._isNonStandardView = new bool?(false);
    this._IsCommandMutable = true;
  }

  public override PXCache Cache
  {
    get
    {
      System.Type dacTypeToMerge = this._linqExecutor.GetDacTypeToMerge();
      return !(dacTypeToMerge != (System.Type) null) ? base.Cache : this._graph.Caches[dacTypeToMerge];
    }
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
    return this.UnwrapItems((IEnumerable<object>) base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows)).ToList<object>();
  }

  protected IEnumerable<object> UnwrapItems(IEnumerable<object> list)
  {
    IEnumerable<object> source = list.Select<object, object>((Func<object, object>) (item => this.UnwrapItem(item)));
    return this._select.Query.Projection() is ProjectionEnumerableAny ? source.Take<object>(1).Select<object, object>((Func<object, object>) (r => (object) true)) : source;
  }

  private object UnwrapItem(object item)
  {
    MergedItemWrapper wrapper = item as MergedItemWrapper;
    if (wrapper == null)
      return item;
    if (wrapper.Items[0] != wrapper.Dac)
    {
      System.Type type = wrapper.Items[0]?.GetType();
      if ((object) type == null)
        type = wrapper.Dac.GetType();
      System.Type dacType = type;
      wrapper.DataItem = this._select.Query.Projection().Transform(wrapper.DataItem, (Func<System.Type, object, bool>) ((t, d) => t == dacType), (Func<object, object>) (d => wrapper.Items[0]));
      wrapper.Dac = wrapper.DataItem;
    }
    return wrapper.DataItem;
  }

  private protected override object CreateResult(
    PXCache[] caches,
    PXDataRecord rec,
    bool hascount,
    ref bool overrideSort,
    ref bool extFilter)
  {
    int position = 0;
    MergeCacheContext context = new MergeCacheContext(this._graph, this._linqExecutor, createItemDelegate: new MergeCacheContext.CreateItemDelegate(((PXView) this).CreateItem));
    object dataItem = this._select.Query.Projection().GetValue(rec, ref position, context);
    if (context.Status == MergeCacheStatus.Skip)
      return (object) null;
    if (dataItem != null && this.CanBeHandledByBase(dataItem))
      return dataItem;
    object mergedDac = context.MergedDac;
    return mergedDac == null ? (object) new MergedItemWrapper(dataItem, this.Cache.GetItemType()) : (object) new MergedItemWrapper(dataItem, mergedDac);
  }

  private protected override bool MergeCache(
    List<object> list,
    object[] parameters,
    bool filterExists,
    ref bool sortReq)
  {
    if (this._linqExecutor.GetDacTypeToMerge() == (System.Type) null && !this.emulation)
      return false;
    bool supressTailSelect = this.SupressTailSelect;
    try
    {
      this.SupressTailSelect = true;
      bool flag1 = base.MergeCache(list, parameters, filterExists, ref sortReq);
      if (flag1)
      {
        foreach (object a in this.Cache.Updated)
        {
          if (this._select.Meet(this.Cache, a, parameters) && this._select is IBqlJoinedSelect)
          {
            bool flag2 = false;
            for (int index = 0; index < list.Count; ++index)
            {
              if (list[index] is PXResult pxResult && this.Cache.ObjectsEqual(a, pxResult[0]))
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              this.AppendTail(a, list, parameters);
          }
        }
      }
      return flag1;
    }
    finally
    {
      this.SupressTailSelect = supressTailSelect;
    }
  }

  public override void AppendTail(object item, List<object> list, params object[] parameters)
  {
    int position = 0;
    MergeCacheContext context = new MergeCacheContext(this._graph, this._linqExecutor, true)
    {
      MergedDac = item
    };
    object obj = this._select.Query.Projection().GetValue((PXDataRecord) null, ref position, context);
    list.Add(obj);
  }

  private protected override void AppendEmptyTail(object item, List<object> list)
  {
    this.AppendTail(item, list);
  }

  private protected override object CloneItem(object item, PXCache[] caches)
  {
    System.Type dacTypeToMerge = this._linqExecutor.GetDacTypeToMerge();
    object newDac = (object) null;
    if (!(item is MergedItemWrapper mergedItemWrapper))
      return base.CloneItem(item, caches);
    object dataItem = this._select.Query.Projection().CloneValue(mergedItemWrapper.DataItem, new CloneContext(new CloneContext.CustomHandlerDelegate(DacCloneHandler)));
    return newDac == null ? (object) new MergedItemWrapper(dataItem, this.Cache.GetItemType()) : (object) new MergedItemWrapper(dataItem, newDac);

    bool DacCloneHandler(object value, out object clone)
    {
      clone = (object) null;
      if (!this.CanBeHandledByBase(value))
        return false;
      clone = base.CloneItem(value, caches);
      if (dacTypeToMerge != (System.Type) null && value?.GetType() == dacTypeToMerge)
        newDac = clone;
      return true;
    }
  }

  private protected override object LocateInCache(object item)
  {
    return base.LocateInCache(this.UnwrapItem(item));
  }

  private protected override object LookupItem(
    PXDataRecord record,
    object item,
    PXCache[] caches,
    ref bool overrideSort,
    ref bool needFilterResults)
  {
    bool localOverrideSort = overrideSort;
    bool localNeedFilterResults = needFilterResults;
    object obj = !(item is MergedItemWrapper mergedItemWrapper) ? base.LookupItem(record, item, caches, ref overrideSort, ref needFilterResults) : this._select.Query.Projection().CloneValue(mergedItemWrapper.DataItem, new CloneContext(new CloneContext.CustomHandlerDelegate(DacCloneHandler)));
    overrideSort = localOverrideSort;
    needFilterResults = localNeedFilterResults;
    return obj;

    bool DacCloneHandler(object value, out object clone)
    {
      clone = (object) null;
      if (!this.CanBeHandledByBase(value))
        return false;
      clone = base.LookupItem(record, value, caches, ref localOverrideSort, ref localNeedFilterResults);
      return true;
    }
  }

  private protected override PXCommandKey GetParametersCacheKey(
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    int maximumRows,
    PXFilterRow[] filters)
  {
    PXCommandKey parametersCacheKey = base.GetParametersCacheKey(searches, sortcolumns, descendings, maximumRows, filters);
    parametersCacheKey.CommandText = this.ToString();
    return parametersCacheKey;
  }

  private bool CanBeHandledByBase(object item)
  {
    switch (item)
    {
      case IBqlTable _:
        return true;
      case PXResult pxResult:
        return pxResult.TableCount > 1;
      default:
        return false;
    }
  }

  protected abstract override void SortResult(
    List<object> list,
    PXView.PXSearchColumn[] sorts,
    bool reverseOrder);
}
