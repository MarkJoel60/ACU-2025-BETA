// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPrevNextBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXPrevNextBase<TNode> : PXAction<TNode>, IPXRecordNavigationAction where TNode : class, IBqlTable, new()
{
  protected bool _inserted;

  protected PXPrevNextBase(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  protected PXPrevNextBase(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected abstract int DefaultStartRow { get; }

  protected override IEnumerable InsertAndGet(PXAdapter adapter)
  {
    IEnumerable enumerable;
    if (!this._inserted && adapter.View.Cache.AllowInsert && adapter.MaximumRows == 1)
    {
      adapter.Currents = (object[]) null;
      this.Insert(adapter);
      enumerable = adapter.Get();
      adapter.View.Cache.IsDirty = false;
    }
    else
    {
      adapter.StartRow = this.DefaultStartRow;
      adapter.Searches = (object[]) null;
      enumerable = adapter.Get();
      if (!NonGenericIEnumerableExtensions.Any_(enumerable) && adapter.View.Cache.AllowInsert && adapter.MaximumRows == 1)
      {
        adapter.Currents = (object[]) null;
        this.Insert(adapter);
        enumerable = adapter.Get();
        adapter.View.Cache.IsDirty = false;
      }
    }
    return enumerable;
  }

  protected override IEnumerable Handler(PXAdapter adapter)
  {
    this._inserted = adapter.View.Cache.Current != null && adapter.View.Cache.GetStatus(adapter.View.Cache.Current) == PXEntryStatus.Inserted;
    this._Graph.SelectTimeStamp();
    IEnumerable enumerable = adapter.Get();
    adapter.Currents = new object[1]
    {
      adapter.View.Cache.Current
    };
    this._Graph.Clear(PXClearOption.PreserveTimeStamp);
    if (!NonGenericIEnumerableExtensions.Any_(enumerable))
      enumerable = this.InsertAndGet(adapter);
    return enumerable;
  }
}
