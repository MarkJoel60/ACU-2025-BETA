// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancel`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>The standard action that discards all the changes made to the data and reloads the data from the database.</summary>
public class PXCancel<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXCancel(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXCancel(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  [PXCancelButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    return PXCancel<TNode>.PerformCancel(adapter);
  }

  internal static IEnumerable PerformCancel(PXAdapter adapter)
  {
    PXGraph graph = adapter.View.Graph;
    adapter.Currents = new object[1]
    {
      adapter.View.Cache.Current
    };
    adapter.View.Cache.ClearQueryCache();
    graph.Clear();
    graph.SelectTimeStamp();
    bool flag = false;
    bool performSearch = true;
    bool performFilter = graph.IsImport && adapter.Filters != null && adapter.Filters.Length != 0 && string.Equals(adapter.Filters[0].DataField, "NoteID", StringComparison.OrdinalIgnoreCase);
    if (adapter.MaximumRows == 1)
    {
      performSearch = adapter.View.Cache.Keys.Count == 0;
      if (adapter.Searches != null)
      {
        for (int index = 0; index < adapter.Searches.Length; ++index)
        {
          if (adapter.Searches[index] != null)
          {
            performSearch = true;
            break;
          }
        }
      }
      if (!performSearch)
        performSearch = performFilter;
      else if (adapter.View.Cache.Keys.Count == 2 && adapter.View.Cache.IsAutoNumber(adapter.View.Cache.Keys[1]) && adapter.Currents[0] != null && adapter.SortColumns != null && adapter.SortColumns.Length != 0 && string.Equals(adapter.SortColumns[0], adapter.View.Cache.Keys[0], StringComparison.OrdinalIgnoreCase) && adapter.Searches != null && adapter.Searches.Length != 0 && adapter.Searches[0] != null)
      {
        object objB1 = PXFieldState.UnwrapValue(adapter.View.Cache.GetValueExt(adapter.Currents[0], adapter.View.Cache.Keys[0]));
        if (!object.Equals(adapter.Searches[0], objB1) && (!(objB1 is string) || !(adapter.Searches[0] is string) || PXLocalesProvider.CollationComparer.Compare(((string) objB1).TrimEnd(), ((string) adapter.Searches[0]).TrimEnd()) != 0))
        {
          object objB2 = PXFieldState.UnwrapValue(adapter.View.Cache.GetValueExt(adapter.Currents[0], adapter.View.Cache.Keys[1]));
          if (adapter.SortColumns.Length <= 1 || adapter.SortColumns.Length == 2 && string.Equals(adapter.SortColumns[1], adapter.View.Cache.Keys[1], StringComparison.OrdinalIgnoreCase) && (adapter.Searches.Length <= 1 || adapter.Searches.Length == 2 && (object.Equals(adapter.Searches[1], objB2) || adapter.Searches[1] is string && objB2 is string && PXLocalesProvider.CollationComparer.Compare(((string) adapter.Searches[1]).TrimEnd(), ((string) objB2).TrimEnd()) == 0)))
          {
            KeyValuePair<string, bool>[] sortColumns = adapter.View.GetSortColumns();
            if (adapter.SortColumns.Length <= 1 || sortColumns.Length <= 1 || string.Equals(sortColumns[1].Key, adapter.View.Cache.Keys[1], StringComparison.OrdinalIgnoreCase))
            {
              performSearch = performFilter;
              if (adapter.SortColumns.Length == 2)
                adapter.SortColumns = new string[1]
                {
                  adapter.SortColumns[0]
                };
              if (adapter.Searches.Length == 2)
                adapter.Searches = new object[1]
                {
                  adapter.Searches[0]
                };
            }
          }
        }
      }
    }
    if (performSearch)
    {
      foreach (object obj in adapter.Get())
      {
        yield return obj;
        flag = true;
      }
      if (!flag)
      {
        object[] searches = adapter.Searches;
        if ((searches != null ? (((IEnumerable<object>) searches).Any<object>() ? 1 : 0) : 0) != 0 && adapter.MaximumRows == 1 && !graph.IsContractBasedAPI)
        {
          using (new PXReadThroughArchivedScope(true))
          {
            foreach (object obj in adapter.Get())
            {
              yield return obj;
              flag = true;
            }
          }
        }
      }
    }
    if (!flag && adapter.MaximumRows == 1)
    {
      adapter.Currents = (object[]) null;
      if (adapter.View.Cache.AllowInsert)
      {
        Dictionary<string, object> values1 = new Dictionary<string, object>();
        if (adapter.SortColumns == null)
          adapter.SortColumns = adapter.View.Cache.Keys.ToArray();
        if (adapter.Searches != null)
        {
          for (int index = 0; index < adapter.Searches.Length && index < adapter.SortColumns.Length; ++index)
            values1[adapter.SortColumns[index]] = adapter.Searches[index];
        }
        if (adapter.View.Cache.Insert((IDictionary) values1) == 1)
        {
          if (performSearch)
          {
            if (!performFilter)
            {
              adapter.Currents = new object[1]
              {
                adapter.View.Cache.Current
              };
              graph.Clear();
              graph.SelectTimeStamp();
            }
            else
              adapter = new PXAdapter((PXView) new PXView.Dummy(graph, adapter.View.BqlSelect, new List<object>()
              {
                adapter.View.Cache.Current
              }));
            foreach (object obj in adapter.Get())
            {
              yield return obj;
              flag = true;
            }
          }
          if (!flag)
          {
            Dictionary<string, object> values2 = new Dictionary<string, object>();
            for (int index = 0; index < adapter.Searches.Length && index < adapter.SortColumns.Length; ++index)
              values2[adapter.SortColumns[index]] = adapter.Searches[index];
            if (!performSearch || adapter.View.Cache.Insert((IDictionary) values2) == 1)
            {
              adapter.View.Cache.IsDirty = false;
              adapter.Searches = new object[adapter.SortColumns.Length];
              for (int index = 0; index < adapter.SortColumns.Length; ++index)
              {
                object obj = values2[adapter.SortColumns[index]];
                adapter.Searches[index] = obj is PXFieldState ? ((PXFieldState) obj).Value : obj;
              }
              foreach (object obj in adapter.Get())
                yield return obj;
            }
          }
        }
      }
      else
      {
        if (graph.IsImport && !graph.IsExport)
          throw new PXException("Insert is disabled");
        adapter.StartRow = 0;
        object[] arr = adapter.Searches;
        while (true)
        {
          Dictionary<string, object> values3 = new Dictionary<string, object>();
          if (arr.Length == 1 && adapter.View.Cache.Keys.Count > 0 && !string.IsNullOrEmpty(adapter.View.Graph.PrimaryView) && adapter.View.Name == adapter.View.Graph.PrimaryView)
          {
            values3[adapter.SortColumns[0]] = arr[0];
            adapter.Currents = new object[1]
            {
              adapter.View.Cache.FillItem((IDictionary) values3)
            };
          }
          foreach (object obj in adapter.Get())
          {
            yield return obj;
            flag = true;
          }
          if (!flag && arr != null && arr.Length != 0 && adapter.SortColumns != null)
          {
            Array.Resize<object>(ref arr, arr.Length - 1);
            adapter.Searches = arr;
            Dictionary<string, object> values4 = new Dictionary<string, object>();
            for (int index = 0; index < arr.Length && index < adapter.SortColumns.Length; ++index)
              values4[adapter.SortColumns[index]] = arr[index];
            adapter.Currents = new object[1]
            {
              adapter.View.Cache.FillItem((IDictionary) values4)
            };
          }
          else
            break;
        }
        arr = (object[]) null;
      }
    }
  }
}
