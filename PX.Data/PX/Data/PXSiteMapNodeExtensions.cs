// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapNodeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

public static class PXSiteMapNodeExtensions
{
  /// <summary>
  /// Checks if the current user has access rights to view the specific <see cref="T:PX.Data.PXSiteMapNode" />
  /// </summary>
  /// <param name="siteMapNode">Node to be checked</param>
  /// <returns><see langword="true" /> if the user can view the node, otherwise <see langword="false" /></returns>
  [PXInternalUseOnly]
  public static bool IsAccessibleToUser(this PXSiteMapNode siteMapNode)
  {
    return siteMapNode.Provider.IsAccessibleToUser(siteMapNode);
  }

  /// <summary>
  /// Checks if the specific <see cref="T:PX.Data.PXSiteMapNode" /> is a dashboard node
  /// </summary>
  /// <param name="node">Node to be checked</param>
  /// <returns><see langword="true" /> if the node is a dashboard node, otherwise <see langword="false" /></returns>
  public static bool IsDashboard(this PXSiteMapNode node) => PXSiteMap.IsDashboard(node?.Url);

  /// <summary>
  /// Checks if the specific <see cref="T:PX.Data.PXSiteMapNode" /> is the hidden node
  /// </summary>
  /// <param name="node">Node to be checked</param>
  /// <returns><see langword="true" /> if the node is the hidden node, otherwise <see langword="false" /></returns>
  [Obsolete("Hidden node support is obsolete and will be removed")]
  internal static bool IsHidden(this PXSiteMapNode node)
  {
    return node != null && string.Equals(node.ScreenID, "HD000000", StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Checks if the specific <see cref="T:PX.Data.PXSiteMapNode" /> has child nodes
  /// </summary>
  /// <param name="node">Node to be checked</param>
  /// <returns><see langword="true" /> if the node has child nodes, otherwise <see langword="false" /></returns>
  internal static bool HasChildNodes(this PXSiteMapNode node)
  {
    return node != null && node.ChildNodes.Any<PXSiteMapNode>();
  }
}
