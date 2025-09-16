// Decompiled with JetBrains decompiler
// Type: PX.Data.PXInsert`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXInsert<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXInsert(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXInsert(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Insert", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXInsertButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXInsert<TNode> pxInsert = this;
    if (!adapter.View.Cache.AllowInsert)
      throw new PXException("The record cannot be inserted.");
    pxInsert._Graph.Clear();
    pxInsert._Graph.SelectTimeStamp();
    PXList.Provider.SetCurrentSearches(PXSiteMap.CurrentNode.ScreenID, (object[]) null);
    pxInsert.Insert(adapter);
    foreach (object obj in adapter.Get())
      yield return obj;
    adapter.View.Cache.IsDirty = false;
    pxInsert._Graph.ReuseRestricted = true;
  }
}
