// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancelClose`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCancelClose<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXCancelClose(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXCancelClose(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Close", MapEnableRights = PXCacheRights.Select)]
  [PXCancelCloseButton(ClosePopup = true)]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    PXCancelClose<TNode> caller = this;
    foreach (object obj in adapter.Get())
      yield return obj;
    caller._Graph.Actions.PressCancel((PXAction) caller);
  }
}
