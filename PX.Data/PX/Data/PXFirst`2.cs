// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFirst`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXFirst<TNode, TName> : PXFirst<TNode>
  where TNode : class, IBqlTable, new()
  where TName : class, IBqlField
{
  public PXFirst(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXFirst(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "First", MapEnableRights = PXCacheRights.Select)]
  [PXFirstButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    return PXNavigationHelper.Press<TName>(adapter, (PXAction) new PXFirst<TNode>(this._Graph, "First"));
  }
}
