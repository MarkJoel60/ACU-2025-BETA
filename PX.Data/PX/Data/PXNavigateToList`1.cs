// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNavigateToList`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <summary>
/// Navigates from the entry screen to the corresponding list.
/// </summary>
/// <typeparam name="TNode">Row t ype of a primary view.</typeparam>
public abstract class PXNavigateToList<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXNavigateToList(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXNavigateToList(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override IEnumerable Handler(PXAdapter adapter)
  {
    if (adapter.ExternalCall)
      PXList.Provider.TryRedirect(this.Graph, PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(PXContext.GetScreenID().Replace(".", "")).ScreenID);
    return adapter.Get();
  }
}
