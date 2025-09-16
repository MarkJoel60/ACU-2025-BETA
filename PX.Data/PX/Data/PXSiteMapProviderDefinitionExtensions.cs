// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapProviderDefinitionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.Access;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class PXSiteMapProviderDefinitionExtensions
{
  private static readonly string[] _specialParams = new string[3]
  {
    "id",
    "unum",
    "scrid"
  };
  private static readonly string[] _wasteParams = new string[4]
  {
    "HideScript",
    "HidePageTitle",
    "fileupload",
    "ui"
  };

  /// <summary>Finds a site map node by its URL</summary>
  /// <param name="rawUrl">Url of the node. It has to be absolute (with application name) or virtual (~/) path to the node</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user's permissions to the node should be checked</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the user doesn't have access to the node</returns>
  internal static PXSiteMapNode FindSiteMapNode(
    this PXSiteMapProvider.Definition definition,
    string rawUrl,
    bool securityTrimmingEnabled)
  {
    // ISSUE: unable to decompile the method.
  }

  /// <summary>Finds a site map node by its identifier</summary>
  /// <param name="key">Identifier of the node.</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user's permissions to the node should be checked</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the user doesn't have access to the node</returns>
  /// <remarks>Node identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> field</remarks>
  internal static PXSiteMapNode FindSiteMapNodeFromKey(
    this PXSiteMapProvider.Definition definition,
    Guid key,
    bool securityTrimmingEnabled)
  {
    PXSiteMapNode node;
    return !definition.TryGetNodeByKey(key, out node) || securityTrimmingEnabled && !node.IsAccessibleToUser() ? (PXSiteMapNode) null : node;
  }

  /// <summary>Finds site map nodes by screen identifier</summary>
  /// <param name="screenID">Key of the node.</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user's permissions to the node should be checked</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  /// <remarks>Screen identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.ScreenID" /> field</remarks>
  [PXInternalUseOnly]
  public static IEnumerable<PXSiteMapNode> FindSiteMapNodesFromScreenID(
    this PXSiteMapProvider.Definition definition,
    string screenID,
    bool securityTrimmingEnabled)
  {
    IReadOnlyCollection<PXSiteMapNode> nodes;
    if (!string.IsNullOrEmpty(screenID) && definition.TryGetNodesByScreenId(screenID, out nodes))
    {
      if (!string.IsNullOrEmpty(PXContext.GetSlot<string>("ForceUI")) && ServiceLocator.IsLocationProviderSet)
      {
        string uiType = PXContext.GetSlot<string>("ForceUI");
        ISiteMapNodeFactory siteMapNodeFactory = ServiceLocator.Current.GetInstance<ISiteMapNodeFactory>();
        foreach (PXSiteMapNode pxSiteMapNode in (IEnumerable<PXSiteMapNode>) nodes)
        {
          if (!securityTrimmingEnabled || pxSiteMapNode.IsAccessibleToUser())
            yield return siteMapNodeFactory.SwitchUi(uiType, pxSiteMapNode);
        }
        uiType = (string) null;
        siteMapNodeFactory = (ISiteMapNodeFactory) null;
      }
      else
      {
        foreach (PXSiteMapNode siteMapNode in (IEnumerable<PXSiteMapNode>) nodes)
        {
          if (!securityTrimmingEnabled || siteMapNode.IsAccessibleToUser())
            yield return siteMapNode;
        }
      }
    }
  }

  /// <summary>Finds site map nodes by graph type</summary>
  /// <param name="graphType">Full name of the graph type.</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user's permissions to the node should be checked</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  /// <remarks>Graph type name is a fully qualified name of the graph type (typeof(MyMaint).FullName)</remarks>
  [PXInternalUseOnly]
  public static IEnumerable<PXSiteMapNode> FindSiteMapNodesFromGraphType(
    this PXSiteMapProvider.Definition definition,
    string graphType,
    bool securityTrimmingEnabled)
  {
    IReadOnlyCollection<PXSiteMapNode> nodes;
    if (!string.IsNullOrEmpty(graphType) && definition.TryGetNodesByGraphType(graphType, out nodes))
    {
      foreach (PXSiteMapNode siteMapNode in (IEnumerable<PXSiteMapNode>) nodes)
      {
        if (!securityTrimmingEnabled || siteMapNode.IsAccessibleToUser())
          yield return siteMapNode;
      }
    }
  }

  /// <summary>Finds child nodes of the node</summary>
  /// <param name="node">Node to be analyzed</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user permissions to the node should be checked</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  internal static IEnumerable<PXSiteMapNode> GetChildNodes(
    this PXSiteMapProvider.Definition definition,
    PXSiteMapNode node,
    bool securityTrimmingEnabled)
  {
    if (node == null)
      throw new PXArgumentException(nameof (node), "The argument cannot be null.");
    IList<PXSiteMapNode> nodes;
    if (!definition.TryGetChildNodesByNodeId(node.NodeID, out nodes))
      return Enumerable.Empty<PXSiteMapNode>();
    return securityTrimmingEnabled ? (IEnumerable<PXSiteMapNode>) nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => x.IsAccessibleToUser())).ToList<PXSiteMapNode>().AsReadOnly() : (IEnumerable<PXSiteMapNode>) nodes;
  }

  /// <summary>Finds a parent node of the node</summary>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>The site map node or <see langword="null" /> if the provided node doesn't have parent node or the user doesn't have access to the parent node </returns>
  internal static PXSiteMapNode GetParentNode(
    this PXSiteMapProvider.Definition definition,
    PXSiteMapNode node)
  {
    if (node == null)
      throw new PXArgumentException(nameof (node), "The argument cannot be null.");
    if (node.ParentID == Guid.Empty)
      return (PXSiteMapNode) null;
    PXSiteMapNode node1;
    return definition.TryGetNodeByKey(node.ParentID, out node1) && node1.IsAccessibleToUser() ? node1 : (PXSiteMapNode) null;
  }
}
