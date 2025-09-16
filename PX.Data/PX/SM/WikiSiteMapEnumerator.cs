// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSiteMapEnumerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <exclude />
public class WikiSiteMapEnumerator(bool isSecureAccess) : PXSiteMapFilteredPathEnumerator((isSecureAccess ? 1 : 0) != 0, new PXSiteMapNodeFilter((IEnumerable<Func<PXSiteMapNode, bool>>) new Func<PXSiteMapNode, bool>[2]
{
  WikiSiteMapEnumerator.\u003C\u003EO.\u003C0\u003E__IsDashboard ?? (WikiSiteMapEnumerator.\u003C\u003EO.\u003C0\u003E__IsDashboard = new Func<PXSiteMapNode, bool>(PXSiteMapFilterRuleStorage.IsDashboard)),
  (Func<PXSiteMapNode, bool>) (node => PXSiteMapProviderExtensions.FindSiteMapNodeFromKeyUnsecure(PXSiteMap.WikiProvider, node.NodeID) == null)
}))
{
  private PXSiteMapNode GetWikiNodeById(Guid nodeID)
  {
    return !this.IsSecure ? PXSiteMap.WikiProvider.FindSiteMapNodeFromKeyUnsecure(nodeID) : PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(nodeID);
  }

  private IList<PXSiteMapNode> GetWikiChildNodes(PXSiteMapNode node)
  {
    return !this.IsSecure ? PXSiteMap.WikiProvider.GetChildNodesUnsecure(node) : PXSiteMap.WikiProvider.GetChildNodes(node);
  }

  protected override PXSiteMapNode GetNodeById(Guid nodeID)
  {
    return this.GetWikiNodeById(nodeID) ?? base.GetNodeById(nodeID);
  }

  protected override IList<PXSiteMapNode> GetChildNodes(PXSiteMapNode node)
  {
    if (object.Equals((object) node, (object) PXSiteMap.WikiProvider.RootNode))
      return base.GetChildNodes(node);
    PXSiteMapNode wikiNodeById = this.GetWikiNodeById(node.NodeID);
    return wikiNodeById != null ? this.GetWikiChildNodes(wikiNodeById) : base.GetChildNodes(node);
  }
}
