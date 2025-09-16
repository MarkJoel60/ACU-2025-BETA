// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqView`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLinqView<T>(PXGraph graph, SQLinqBqlCommand select, SQLinqExecutor linqExecutor) : 
  SQLinqView(graph, select, linqExecutor)
{
  protected override void SortResult(
    List<object> list,
    PXView.PXSearchColumn[] sorts,
    bool reverseOrder)
  {
    bool changed;
    List<T> source = this._linqExecutor.SortResult<T>(this.UnwrapItems((IEnumerable<object>) list).Cast<T>(), out changed);
    if (!changed)
      return;
    list.Clear();
    list.AddRange(source.Cast<object>());
  }
}
