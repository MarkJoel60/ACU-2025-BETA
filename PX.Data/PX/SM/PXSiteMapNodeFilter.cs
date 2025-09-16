// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSiteMapNodeFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXSiteMapNodeFilter
{
  private readonly Func<PXSiteMapNode, bool>[] filterRules;

  public PXSiteMapNodeFilter(
    IEnumerable<Func<PXSiteMapNode, bool>> nodeFilterRules)
  {
    if (nodeFilterRules == null)
      return;
    this.filterRules = nodeFilterRules.ToArray<Func<PXSiteMapNode, bool>>();
  }

  public IEnumerable<PXSiteMapNode> Filter(IEnumerable<PXSiteMapNode> nodes)
  {
    return this.filterRules != null && this.filterRules.Length != 0 ? nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => ((IEnumerable<Func<PXSiteMapNode, bool>>) this.filterRules).All<Func<PXSiteMapNode, bool>>((Func<Func<PXSiteMapNode, bool>, bool>) (filter => !filter(node))))) : nodes;
  }

  /// <summary>
  /// Returns true if defined node meets conditions for at least one filter rule.
  /// </summary>
  public bool MeetsFilter(PXSiteMapNode node)
  {
    foreach (Func<PXSiteMapNode, bool> filterRule in this.filterRules)
    {
      if (filterRule(node))
        return true;
    }
    return false;
  }
}
