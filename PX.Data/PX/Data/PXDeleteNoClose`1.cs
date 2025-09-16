// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDeleteNoClose`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDeleteNoClose<TNode> : PXDelete<TNode> where TNode : class, IBqlTable, new()
{
  public PXDeleteNoClose(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXDeleteNoClose(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.", ClosePopup = false)]
  protected override IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);
}
