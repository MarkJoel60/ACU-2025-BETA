// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDelete`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDelete<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXDelete(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXDelete(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override object GetState(object row)
  {
    object state = base.GetState(row);
    if (!(state is PXButtonState pxButtonState) || string.IsNullOrEmpty(pxButtonState.ConfirmationMessage))
      return state;
    if (typeof (TNode).IsDefined(typeof (PXCacheNameAttribute), true))
    {
      PXCacheNameAttribute customAttribute = (PXCacheNameAttribute) typeof (TNode).GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0];
      pxButtonState.ConfirmationMessage = string.Format(pxButtonState.ConfirmationMessage, (object) customAttribute.GetName());
      return state;
    }
    pxButtonState.ConfirmationMessage = string.Format(pxButtonState.ConfirmationMessage, (object) typeof (TNode).Name);
    return state;
  }

  [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.")]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXDelete<TNode> caller = this;
    if (!adapter.View.Cache.AllowDelete)
      throw new PXException("The record cannot be deleted.");
    int startRow = adapter.StartRow;
    foreach (object obj in adapter.Get())
      adapter.View.Cache.Delete(obj);
    try
    {
      caller._Graph.Actions.PressSave((PXAction) caller);
    }
    catch
    {
      caller._Graph.Clear();
      throw;
    }
    caller._Graph.SelectTimeStamp();
    adapter.StartRow = startRow;
    bool flag = false;
    foreach (object obj in adapter.Get())
    {
      yield return obj;
      flag = true;
    }
    if (!flag && adapter.MaximumRows == 1)
    {
      if (adapter.View.Cache.AllowInsert)
      {
        caller.Insert(adapter);
        foreach (object obj in adapter.Get())
          yield return obj;
        adapter.View.Cache.IsDirty = false;
      }
      else
      {
        adapter.StartRow = 0;
        adapter.Searches = (object[]) null;
        foreach (object obj in adapter.Get())
          yield return obj;
      }
    }
  }
}
