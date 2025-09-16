// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLastOrderBy`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXLastOrderBy<TNode, TOrderBy> : PXLast<TNode>
  where TNode : class, IBqlTable, new()
  where TOrderBy : class, IBqlOrderBy, new()
{
  public PXLastOrderBy(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXLastOrderBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Last", MapEnableRights = PXCacheRights.Select)]
  [PXLastButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    return PXNavigationHelper.PressOrderBy<TOrderBy>(adapter, (PXAction) new PXLast<TNode>(this._Graph, "Last"));
  }
}
