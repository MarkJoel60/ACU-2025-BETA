// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSaveCancel`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSaveCancel<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXSaveCancel(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXSaveCancel(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    foreach (TNode node in new PXSave<TNode>(adapter.View.Graph, "Save").Press(adapter))
      ;
    foreach (TNode node in new PXCancel<TNode>(adapter.View.Graph, "Cancel").Press(adapter))
      yield return (object) node;
  }
}
