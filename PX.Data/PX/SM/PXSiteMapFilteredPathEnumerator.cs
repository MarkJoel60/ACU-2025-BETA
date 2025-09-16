// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSiteMapFilteredPathEnumerator
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

/// <summary>
/// Returns only nodes that matches defined filter rules, and path to them from the root node.
/// </summary>
public class PXSiteMapFilteredPathEnumerator(
  bool isSecureAccess,
  PXSiteMapNodeFilter siteMapNodeFilter) : PXSiteMapEnumeratorBase(isSecureAccess, siteMapNodeFilter)
{
  private Dictionary<Guid, PXSiteMapNode> _preprocessedNodes = new Dictionary<Guid, PXSiteMapNode>();

  public override IEnumerable<SiteMap> SiteMapNodes(Guid? nodeID)
  {
    PXSiteMapFilteredPathEnumerator filteredPathEnumerator = this;
    if (!nodeID.HasValue)
    {
      PXSiteMapNode nodeById = filteredPathEnumerator.GetNodeById(Guid.Empty);
      filteredPathEnumerator._preprocessedNodes.Clear();
      filteredPathEnumerator.ProcessChilds(nodeById);
      if (filteredPathEnumerator._preprocessedNodes.Count != 0)
        yield return PXSiteMapEnumeratorBase.CreateSiteMap(nodeById);
    }
    else
    {
      PXSiteMapNode node = filteredPathEnumerator.FindNode(nodeID.Value);
      if (node != null)
      {
        IList<PXSiteMapNode> childNodes = filteredPathEnumerator.GetChildNodes(node);
        if (childNodes != null && childNodes.Count > 0)
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          foreach (SiteMap siteMap in childNodes.Where<PXSiteMapNode>(new Func<PXSiteMapNode, bool>(filteredPathEnumerator.\u003CSiteMapNodes\u003Eb__2_0)).Select<PXSiteMapNode, SiteMap>(PXSiteMapFilteredPathEnumerator.\u003C\u003EO.\u003C0\u003E__CreateSiteMap ?? (PXSiteMapFilteredPathEnumerator.\u003C\u003EO.\u003C0\u003E__CreateSiteMap = new Func<PXSiteMapNode, SiteMap>(PXSiteMapEnumeratorBase.CreateSiteMap))))
            yield return siteMap;
        }
      }
    }
  }

  private PXSiteMapNode FindNode(Guid nodeID)
  {
    if (this._preprocessedNodes.Count == 0)
    {
      PXSiteMapNode nodeById = this.GetNodeById(nodeID);
      if (nodeById != null)
      {
        this.ProcessChilds(nodeById);
        return nodeById;
      }
    }
    PXSiteMapNode pxSiteMapNode;
    return !this._preprocessedNodes.TryGetValue(nodeID, out pxSiteMapNode) ? (PXSiteMapNode) null : pxSiteMapNode;
  }

  private void ProcessChilds(PXSiteMapNode root)
  {
    IList<PXSiteMapNode> childNodes = this.GetChildNodes(root);
    if (childNodes.Count > 0)
    {
      foreach (PXSiteMapNode root1 in (IEnumerable<PXSiteMapNode>) childNodes)
      {
        root1.ParentNode = root;
        this.ProcessChilds(root1);
      }
    }
    if (this.NodeFilter.MeetsFilter(root))
      return;
    this._preprocessedNodes[root.NodeID] = root;
    PXSiteMapNode objA = root;
    while (!object.Equals((object) objA, (object) root.Provider.RootNode))
    {
      objA = objA.ParentNode;
      this._preprocessedNodes[objA.NodeID] = objA;
    }
  }
}
