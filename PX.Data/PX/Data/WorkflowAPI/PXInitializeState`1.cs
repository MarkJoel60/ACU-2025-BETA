// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXInitializeState`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data.WorkflowAPI;

public class PXInitializeState<TNode> : PXAutoAction<TNode> where TNode : class, IBqlTable, new()
{
  protected PXInitializeState(PXGraph graph)
    : base(graph)
  {
  }

  public PXInitializeState(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXInitializeState(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this._Handler = (Delegate) new PXButtonDelegate(((PXAction<TNode>) this).Handler);
  }

  [PXButton]
  [PXUIField(DisplayName = "Initialize State", Visible = false, MapEnableRights = PXCacheRights.Select)]
  protected override IEnumerable Handler(PXAdapter adapter) => adapter.Get();
}
