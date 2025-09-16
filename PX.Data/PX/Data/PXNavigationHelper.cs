// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNavigationHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class PXNavigationHelper
{
  internal static IEnumerable Press<TName>(PXAdapter adapter, PXAction action) where TName : class, IBqlField
  {
    string str = (string) null;
    if (adapter.View.Cache.Current != null)
      str = adapter.View.Cache.GetValue<TName>(adapter.View.Cache.Current) as string;
    List<string> stringList = new List<string>((IEnumerable<string>) adapter.SortColumns);
    List<object> objectList = new List<object>((IEnumerable<object>) adapter.Searches);
    string name = typeof (TName).Name;
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (string.Equals(stringList[index], name, StringComparison.OrdinalIgnoreCase))
      {
        objectList.RemoveAt(index);
        stringList.RemoveAt(index);
      }
    }
    objectList.Insert(0, (object) str);
    stringList.Insert(0, name);
    adapter.Searches = objectList.ToArray();
    adapter.SortColumns = stringList.ToArray();
    return action.Press(adapter);
  }

  internal static IEnumerable PressOrderBy<TOrderBy>(PXAdapter adapter, PXAction action) where TOrderBy : class, IBqlOrderBy, new()
  {
    IBqlOrderBy instance = (IBqlOrderBy) Activator.CreateInstance(typeof (TOrderBy));
    List<IBqlSortColumn> bqlSortColumnList = new List<IBqlSortColumn>();
    SQLExpression sqlExpression = SQLExpression.None();
    ref SQLExpression local = ref sqlExpression;
    BqlCommandInfo info = new BqlCommandInfo(false);
    info.SortColumns = bqlSortColumnList;
    info.BuildExpression = false;
    BqlCommand.Selection selection = new BqlCommand.Selection();
    instance.AppendExpression(ref local, (PXGraph) null, info, selection);
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    List<object> objectList = new List<object>();
    if (adapter.View.Cache.Current != null)
    {
      foreach (IBqlSortColumn bqlSortColumn in bqlSortColumnList)
      {
        System.Type referencedType = bqlSortColumn.GetReferencedType();
        System.Type declaringType = referencedType.DeclaringType;
        System.Type itemType = adapter.View.GetItemType();
        PXCache pxCache = adapter.View.Cache;
        System.Type c = declaringType;
        if (!itemType.IsAssignableFrom(c))
          pxCache = adapter.View.Cache.Graph.Caches[declaringType];
        string name = referencedType.Name;
        object obj = pxCache.GetValue(pxCache.Current, name);
        stringList.Add(name);
        boolList.Add(bqlSortColumn.IsDescending);
        objectList.Add(obj);
      }
    }
    adapter.Searches = objectList.ToArray();
    adapter.SortColumns = stringList.ToArray();
    adapter.Descendings = boolList.ToArray();
    return action.Press(adapter);
  }
}
