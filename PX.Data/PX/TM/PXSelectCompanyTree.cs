// Decompiled with JetBrains decompiler
// Type: PX.TM.PXSelectCompanyTree
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.TM;

/// <exclude />
public class PXSelectCompanyTree : PXSelectBase<EPCompanyTree>
{
  public PXSelectCompanyTree(PXGraph graph)
  {
    this.View = PXSelectCompanyTree.CreateView(graph, (Delegate) new PXSelectDelegate<int?>(this.tree));
  }

  public PXSelectCompanyTree(PXGraph graph, Delegate handler)
  {
    this.View = PXSelectCompanyTree.CreateView(graph, handler);
  }

  private static PXView CreateView(PXGraph graph, Delegate handler)
  {
    return new PXView(graph, false, (BqlCommand) new PX.Data.Select<EPCompanyTree, Where<EPCompanyTree.parentWGID, Equal<Argument<int?>>>, OrderBy<Asc<EPCompanyTree.sortOrder>>>(), handler);
  }

  protected IEnumerable tree([PXInt] int? WorkGroupID)
  {
    if (!WorkGroupID.HasValue)
      WorkGroupID = new int?(0);
    return (IEnumerable) PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, Where<EPCompanyTree.parentWGID, Equal<Required<EPCompanyTree.workGroupID>>>>.Config>.Select(this.View.Graph, (object) WorkGroupID);
  }
}
