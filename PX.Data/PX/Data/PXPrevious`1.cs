// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPrevious`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXPrevious<TNode> : PXPrevNextBase<TNode> where TNode : class, IBqlTable, new()
{
  public PXPrevious(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXPrevious(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override int DefaultStartRow => -1;

  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select)]
  [PXPreviousButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    adapter.StartRow -= adapter.MaximumRows;
    return base.Handler(adapter);
  }
}
