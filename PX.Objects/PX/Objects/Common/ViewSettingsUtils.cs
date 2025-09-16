// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ViewSettingsUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class ViewSettingsUtils
{
  public static bool ApplySortingFrom<TEntity>(
    this ViewSettings viewSettings,
    PXSelectBase<TEntity> query)
    where TEntity : class, IBqlTable, new()
  {
    IBqlSortColumn[] sortColumns = ((PXSelectBase) query).View.BqlSelect.GetSortColumns();
    PXCache<TEntity> cache = GraphHelper.Caches<TEntity>(((PXSelectBase) query).View.Graph);
    return viewSettings.ApplySorting((PXCache) cache, sortColumns);
  }

  public static bool ApplySorting(
    this ViewSettings viewSettings,
    PXCache cache,
    IBqlSortColumn[] querySorting)
  {
    if (!viewSettings.SortColumns.Any<string>())
      return false;
    List<string> stringList = cache.Keys.Count == 0 || viewSettings.SortColumns.Count < cache.Keys.Count ? new List<string>((IEnumerable<string>) viewSettings.SortColumns) : viewSettings.SortColumns.Take<string>(viewSettings.SortColumns.Count - cache.Keys.Count).ToList<string>();
    List<\u003C\u003Ef__AnonymousType26<Type, string, bool>> queryFields = ((IEnumerable<IBqlSortColumn>) querySorting).Select(sort => new
    {
      Type = sort.GetReferencedType(),
      Name = cache.GetField(sort.GetReferencedType()),
      Descending = sort.IsDescending
    }).ToList();
    for (int index = 0; index < stringList.Count; ++index)
    {
      string str = stringList[index];
      removeQueryField(str);
      string name;
      if (viewSettings.FieldsMap != null && viewSettings.FieldsMap.TryGetValue(str, out name))
      {
        if (viewSettings.Searches[index] == null)
          viewSettings.SortColumns[index] = name;
        removeQueryField(name);
      }
    }
    int count = stringList.Count;
    foreach (var data in queryFields)
      insert(count++, data.Name, data.Descending);
    return true;

    void removeQueryField(string name)
    {
      int index = queryFields.FindIndex(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
      if (index < 0)
        return;
      queryFields.RemoveAt(index);
    }

    void insert(int index, string field, bool desc)
    {
      viewSettings.Searches.Insert(index, (object) null);
      viewSettings.SortColumns.Insert(index, field);
      viewSettings.Descendings.Insert(index, desc);
    }
  }

  public static List<object> Select(this PXView view, ViewSettings viewSettings)
  {
    int startRow = viewSettings.StartRow;
    int num = 0;
    return view.Select(viewSettings.Currents.ToArray(), viewSettings.Parameters.ToArray(), viewSettings.Searches.ToArray(), viewSettings.SortColumns.ToArray(), viewSettings.Descendings.ToArray(), PXView.PXFilterRowCollection.op_Implicit(viewSettings.Filters), ref startRow, viewSettings.MaximumRows, ref num);
  }

  public static List<TEntity> Select<TEntity>(this PXView view, ViewSettings viewSettings) where TEntity : class, IBqlTable, new()
  {
    int startRow = viewSettings.StartRow;
    int num = 0;
    return GraphHelper.RowCast<TEntity>((IEnumerable) view.Select(viewSettings.Currents.ToArray(), viewSettings.Parameters.ToArray(), viewSettings.Searches.ToArray(), viewSettings.SortColumns.ToArray(), viewSettings.Descendings.ToArray(), PXView.PXFilterRowCollection.op_Implicit(viewSettings.Filters), ref startRow, viewSettings.MaximumRows, ref num)).ToList<TEntity>();
  }
}
