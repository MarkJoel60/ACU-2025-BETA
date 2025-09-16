// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFirstLastBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXFirstLastBase<TNode> : PXAction<TNode>, IPXRecordNavigationAction where TNode : class, IBqlTable, new()
{
  protected PXFirstLastBase(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  protected PXFirstLastBase(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override IEnumerable InsertAndGet(PXAdapter adapter)
  {
    adapter.Currents = (object[]) null;
    this.Insert(adapter);
    IEnumerable enumerable = adapter.Get();
    adapter.View.Cache.IsDirty = false;
    return enumerable;
  }

  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXFirstLastBase<TNode> pxFirstLastBase = this;
    adapter.Currents = new object[1]
    {
      adapter.View.Cache.Current
    };
    pxFirstLastBase.Graph.Clear();
    pxFirstLastBase.Graph.SelectTimeStamp();
    adapter.Searches = (object[]) null;
    bool flag = false;
    foreach (object obj in adapter.Get())
    {
      yield return obj;
      flag = true;
    }
    if (!flag && adapter.MaximumRows == 1 && adapter.View.Cache.AllowInsert)
    {
      foreach (object obj in pxFirstLastBase.InsertAndGet(adapter))
        yield return obj;
    }
  }
}
