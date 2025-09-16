// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSaveCloseToList`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <summary>
/// Navigates from the entry screen to the corresponding list.
/// </summary>
/// <typeparam name="TNode">Row t ype of a primary view.</typeparam>
public class PXSaveCloseToList<TNode> : PXNavigateToList<TNode>, ISaveCloseToList where TNode : class, IBqlTable, new()
{
  public PXSaveCloseToList(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXSaveCloseToList(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Save & Close", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveCloseButton(ClosePopup = true)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    if (!adapter.InternalCall && !adapter.View.Cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    this._Graph.Actions.PressSave((PXAction) this);
    return base.Handler(adapter);
  }
}
