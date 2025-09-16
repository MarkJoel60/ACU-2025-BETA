// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.SQLinqAsyncViewOfT`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Linq.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree.Async;

/// <inheritdoc />
internal class SQLinqAsyncViewOfT<T>(
  PXGraph graph,
  SQLinqBqlCommand select,
  SQLinqExecutor linqExecutor) : SQLinqView<T>(graph, select, linqExecutor)
{
  public IAsyncEnumerable<object> SelectAsync(
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
    return this.UnwrapItems(AsyncEnumerable.ToAsyncEnumerable<object>((IEnumerable<object>) this.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows)));
  }

  protected IAsyncEnumerable<object> UnwrapItems(IAsyncEnumerable<object> list)
  {
    IAsyncEnumerable<object> asyncEnumerable = AsyncEnumerable.Select<object, object>(list, (Func<object, object>) (item => this.UnwrapItem(item)));
    return this._select.Query.Projection() is ProjectionEnumerableAny ? AsyncEnumerable.Select<object, object>(AsyncEnumerable.Take<object>(asyncEnumerable, 1), (Func<object, object>) (r => (object) true)) : asyncEnumerable;
  }

  private object UnwrapItem(object item)
  {
    MergedItemWrapper wrapper = item as MergedItemWrapper;
    if (wrapper != null)
    {
      System.Type type = wrapper.Items[0]?.GetType();
      if ((object) type == null)
        type = wrapper.Dac.GetType();
      System.Type dacType = type;
      if (wrapper.Items[0] != wrapper.Dac)
      {
        wrapper.DataItem = this._select.Query.Projection().Transform(wrapper.DataItem, (Func<System.Type, object, bool>) ((t, d) => t == dacType), (Func<object, object>) (d => wrapper.Items[0]));
        wrapper.Dac = wrapper.DataItem;
      }
      return wrapper.DataItem is IEnumerable dataItem ? AsyncEnumerableQuery.Create(dacType, dataItem) : wrapper.DataItem;
    }
    return item is IQueryable sequence ? AsyncEnumerableQuery.Create(sequence.ElementType, (IEnumerable) sequence) : item;
  }
}
