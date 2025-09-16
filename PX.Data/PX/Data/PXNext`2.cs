// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNext`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNext<TNode, TName> : PXNext<TNode>
  where TNode : class, IBqlTable, new()
  where TName : class, IBqlField
{
  public PXNext(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXNext(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select)]
  [PXNextButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    return PXNavigationHelper.Press<TName>(adapter, (PXAction) new PXNext<TNode>(this._Graph, "Next"));
  }
}
