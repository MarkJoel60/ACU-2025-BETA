// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNext`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNext<TNode> : PXPrevNextBase<TNode> where TNode : class, IBqlTable, new()
{
  public PXNext(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXNext(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override int DefaultStartRow => 0;

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select)]
  [PXNextButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    adapter.StartRow += adapter.MaximumRows;
    return base.Handler(adapter);
  }
}
