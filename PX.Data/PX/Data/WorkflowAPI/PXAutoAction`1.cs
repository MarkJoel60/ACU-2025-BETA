// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXAutoAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data.WorkflowAPI;

public class PXAutoAction<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  protected PXAutoAction(PXGraph graph)
    : base(graph)
  {
  }

  public PXAutoAction(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXAutoAction(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this._Handler = (Delegate) new PXButtonDelegate(((PXAction<TNode>) this).Handler);
  }

  [PXButton]
  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected override IEnumerable Handler(PXAdapter adapter) => adapter.Get();
}
