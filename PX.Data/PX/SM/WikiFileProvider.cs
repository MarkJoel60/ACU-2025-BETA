// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiFileProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Export.Excel.Core;
using System;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class WikiFileProvider
{
  public static string GetWikiPagePath(Guid pageID)
  {
    return WikiFileProvider.GetNodePath(PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(pageID));
  }

  public static string GetSiteMapPath(Guid nodeID)
  {
    return WikiFileProvider.GetNodePath(PXSiteMap.Provider.FindSiteMapNodeFromKey(nodeID));
  }

  private static string GetNodePath(PXSiteMapNode node)
  {
    string nodePath = string.Empty;
    for (PXSiteMapNode pxSiteMapNode = node; pxSiteMapNode != null; pxSiteMapNode = pxSiteMapNode.ParentNode)
      nodePath = Utils.CombinePaths(Utils.UrlEncode(pxSiteMapNode.Title), nodePath);
    return nodePath;
  }
}
