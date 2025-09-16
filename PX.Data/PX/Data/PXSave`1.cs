// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSave`1
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

/// <summary>
/// The standard action that commits the changes made to the data to the database and then reloads the committed data.
/// </summary>
public class PXSave<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXSave(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXSave(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected override IEnumerable Handler(PXAdapter adapter) => this.HandlerInternal(adapter, false);

  internal IEnumerable HandlerInternal(PXAdapter adapter, bool isPersistBeforeAdapterGet)
  {
    PXSave<TNode> pxSave = this;
    if (!adapter.InternalCall && !adapter.View.Cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    object first = (object) null;
    int cnt = 0;
    bool noTimeStamp = pxSave._Graph.TimeStamp == null;
    if (isPersistBeforeAdapterGet)
      Persist();
    foreach (object obj in adapter.Get())
    {
      ++cnt;
      first = obj;
      yield return obj;
    }
    if (noTimeStamp)
      pxSave._Graph.TimeStamp = (byte[]) null;
    if (!isPersistBeforeAdapterGet)
      Persist();
    if (cnt == 1 && first != null && adapter.Searches != null && adapter.SortColumns != null && adapter.Searches.Length == adapter.SortColumns.Length && ((IEnumerable<object>) adapter.Searches).All<object>((Func<object, bool>) (_ => _ != null)) && ((IEnumerable<string>) adapter.SortColumns).All<string>((Func<string, bool>) (_ => _ != null)))
    {
      if (first is PXResult)
        first = ((PXResult) first)[0];
      for (int index = 0; index < adapter.SortColumns.Length; ++index)
        adapter.Searches[index] = PXFieldState.UnwrapValue(adapter.View.Cache.GetValueExt(first, adapter.SortColumns[index]));
    }

    void Persist()
    {
      if (!PXProcessing<TNode>.TryPersistPerRow(this._Graph))
        this._Graph.Persist();
      this._Graph.ReuseRestricted = true;
    }
  }
}
