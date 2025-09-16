// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSiteMapEnumeratorBase
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
public abstract class PXSiteMapEnumeratorBase
{
  protected bool IsSecure { get; private set; }

  protected PXSiteMapNodeFilter NodeFilter { get; private set; }

  public PXSiteMapEnumeratorBase(bool isSecureAccess, PXSiteMapNodeFilter siteMapNodeFilter)
  {
    this.IsSecure = isSecureAccess;
    this.NodeFilter = siteMapNodeFilter;
  }

  public abstract IEnumerable<SiteMap> SiteMapNodes(Guid? nodeID);

  protected virtual PXSiteMapNode GetNodeById(Guid nodeID)
  {
    return !this.IsSecure ? PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nodeID) : PXSiteMap.Provider.FindSiteMapNodeFromKey(nodeID);
  }

  protected virtual IList<PXSiteMapNode> GetChildNodes(PXSiteMapNode node)
  {
    return !this.IsSecure ? PXSiteMap.Provider.GetChildNodesUnsecure(node) : PXSiteMap.Provider.GetChildNodes(node);
  }

  /// <summary>Converts PXSiteMapNode to SiteMap object.</summary>
  public static SiteMap CreateSiteMap(PXSiteMapNode node)
  {
    if (PXSiteMap.IsPortal)
    {
      PortalMap siteMap = new PortalMap();
      siteMap.NodeID = new Guid?(node.NodeID);
      siteMap.ParentID = new Guid?(node.ParentID);
      siteMap.ScreenID = node.ScreenID;
      siteMap.Title = node.Title;
      siteMap.Url = node.Url;
      return (SiteMap) siteMap;
    }
    return new SiteMap()
    {
      NodeID = new Guid?(node.NodeID),
      ParentID = new Guid?(node.ParentID),
      ScreenID = node.ScreenID,
      Title = node.Title,
      Url = node.Url
    };
  }
}
