// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class PXSiteMapProviderExtensions
{
  /// <summary>
  /// Finds a site map node by its URL regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="rawUrl">Url of the node. It has to be absolute (with application name) or virtual (~/) path to the node</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found</returns>
  [PXInternalUseOnly]
  public static PXSiteMapNode FindSiteMapNodeUnsecure(
    this PXSiteMapProvider provider,
    string rawUrl)
  {
    PXSiteMapProvider.Definition definitions = provider.Definitions;
    return definitions == null ? (PXSiteMapNode) null : definitions.FindSiteMapNode(rawUrl?.ToLower(), false);
  }

  /// <summary>Finds a site map node by its identifier</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="key">String representation of the node identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  /// <remarks>Node identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> field</remarks>
  [PXInternalUseOnly]
  public static PXSiteMapNode FindSiteMapNodeFromKey(this PXSiteMapProvider provider, string key)
  {
    return provider.TryParseKeyAsGuidAndFindSiteMapNode(key, true);
  }

  /// <summary>
  /// Finds a site map node by its identifier regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="key">String representation of the node identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found</returns>
  /// <remarks>Node identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> field</remarks>
  internal static PXSiteMapNode FindSiteMapNodeFromKeyUnsecure(
    this PXSiteMapProvider provider,
    string key)
  {
    return provider.TryParseKeyAsGuidAndFindSiteMapNode(key, false);
  }

  private static PXSiteMapNode TryParseKeyAsGuidAndFindSiteMapNode(
    this PXSiteMapProvider provider,
    string key,
    bool secure)
  {
    Guid key1;
    return !GUID.TryParse(key, ref key1) ? (PXSiteMapNode) null : provider.Definitions.FindSiteMapNodeFromKey(key1, secure);
  }

  /// <summary>
  /// Finds a site map node by its identifier regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="key">The node identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found</returns>
  /// <remarks>Node identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> field</remarks>
  internal static PXSiteMapNode FindSiteMapNodeFromKeyUnsecure(
    this PXSiteMapProvider provider,
    Guid key)
  {
    return provider.FindSiteMapNodeByGuidKey(key, false);
  }

  /// <summary>Finds a site map node by its identifier</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="key">The node identifier</param>
  /// <param name="secure">Indicates whether the current user permission to the node should be checked</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  /// <remarks>Node identifier is a value stored in the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> field</remarks>
  internal static PXSiteMapNode FindSiteMapNodeByGuidKey(
    this PXSiteMapProvider provider,
    Guid key,
    bool secure)
  {
    return provider.Definitions.FindSiteMapNodeFromKey(key, secure);
  }

  /// <summary>Finds a site map node by graph type</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Type of the graph</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  [PXInternalUseOnly]
  public static PXSiteMapNode FindSiteMapNode(this PXSiteMapProvider provider, System.Type graphType)
  {
    return provider.FindSiteMapNodeByGraphType(graphType, true);
  }

  /// <summary>
  /// Finds a site map node by graph type regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Type of the graph</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found</returns>
  [PXInternalUseOnly]
  public static PXSiteMapNode FindSiteMapNodeUnsecure(
    this PXSiteMapProvider provider,
    System.Type graphType)
  {
    return provider.FindSiteMapNodeByGraphType(graphType, false);
  }

  private static PXSiteMapNode FindSiteMapNodeByGraphType(
    this PXSiteMapProvider provider,
    System.Type graphType,
    bool secure)
  {
    if (provider.RootNode == null)
      return (PXSiteMapNode) null;
    string fullName = CustomizedTypeManager.GetTypeNotCustomized(graphType).FullName;
    return provider.Definitions.FindSiteMapNodesFromGraphType(fullName, secure).FirstOrDefault<PXSiteMapNode>();
  }

  /// <summary>
  /// Checks whether the specified node is a child of the hidden node
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="node">Node to be checked</param>
  /// <returns><see langword="true" /> if the node is a descendant of the hidden node, otherwise <see langword="false" /></returns>
  [Obsolete("Hidden node support is obsolete and will be removed")]
  internal static bool IsInHidden(this PXSiteMapProvider provider, PXSiteMapNode node)
  {
    if (node == null)
      return false;
    PXSiteMapNode mapNodeByScreenId = provider.FindSiteMapNodeByScreenID("HD000000", false);
    if (mapNodeByScreenId == null)
      return false;
    PXSiteMapNode objA = node;
    int num = 0;
    while (objA.NodeID != Guid.Empty && (objA = provider.FindSiteMapNodeFromKeyUnsecure(objA.ParentID)) != null && num++ < 1000)
    {
      if (object.Equals((object) objA, (object) mapNodeByScreenId))
        return true;
    }
    return false;
  }

  /// <summary>Finds a site map node by screen identifier</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="screenID">The screen identifier</param>
  /// <param name="securityTrimmingEnabled">Indicates whether the current user permission to the node should be checked</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  internal static PXSiteMapNode FindSiteMapNodeByScreenID(
    this PXSiteMapProvider provider,
    string screenID,
    bool securityTrimmingEnabled)
  {
    string[] strArray;
    if (screenID == null)
      strArray = (string[]) null;
    else
      strArray = screenID.Split('.');
    if (strArray == null)
      strArray = Array.Empty<string>();
    string[] source = strArray;
    if (((IEnumerable<string>) source).Count<string>() == 4 && ((IEnumerable<string>) source).All<string>((Func<string, bool>) (part => part.Length == 2)))
      screenID = screenID?.Replace(".", string.Empty);
    return string.Equals(screenID, "HD000000", StringComparison.OrdinalIgnoreCase) ? provider.Definitions.FindSiteMapNodesFromScreenID(screenID, securityTrimmingEnabled).FirstOrDefault<PXSiteMapNode>() : EnumerableExtensions.FirstOrAny<PXSiteMapNode>(provider.Definitions.FindSiteMapNodesFromScreenID(screenID, securityTrimmingEnabled), (Func<PXSiteMapNode, bool>) (n => !provider.IsInHidden(n)), (PXSiteMapNode) null);
  }

  /// <summary>
  /// Finds a site map node by screen identifier regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="screenID">The screen identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found</returns>
  internal static PXSiteMapNode FindSiteMapNodeByScreenIDUnsecure(
    this PXSiteMapProvider provider,
    string screenID)
  {
    return provider.FindSiteMapNodeByScreenID(screenID, false);
  }

  /// <summary>Finds site map nodes by screen identifier</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="screenID">The screen identifier</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the nodes</returns>
  [PXInternalUseOnly]
  public static IEnumerable<PXSiteMapNode> FindSiteMapNodesByScreenID(
    this PXSiteMapProvider provider,
    string screenID)
  {
    return provider.Definitions.FindSiteMapNodesFromScreenID(screenID, true);
  }

  /// <summary>
  /// Finds site map nodes by screen identifier regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="screenID">The screen identifier</param>
  /// <returns>The set of site map nodes or empty set if they are not found</returns>
  internal static IEnumerable<PXSiteMapNode> FindSiteMapNodesByScreenIDUnsecure(
    this PXSiteMapProvider provider,
    string screenID)
  {
    return provider.Definitions.FindSiteMapNodesFromScreenID(screenID, false);
  }

  private static PXSiteMapNode FindSiteMapNodeByGraphType(
    this PXSiteMapProvider provider,
    string graphType,
    bool securityTrimmingEnabled)
  {
    return EnumerableExtensions.FirstOrAny<PXSiteMapNode>(provider.Definitions.FindSiteMapNodesFromGraphType(graphType, securityTrimmingEnabled), (Func<PXSiteMapNode, bool>) (n => !provider.IsInHidden(n)), (PXSiteMapNode) null);
  }

  /// <summary>Finds a site map node by graph type</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Full name of the graph type</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  /// <remarks>Graph type name is a fully qualified name of the graph type (typeof(MyMaint).FullName)</remarks>
  [PXInternalUseOnly]
  public static PXSiteMapNode FindSiteMapNodeByGraphType(
    this PXSiteMapProvider provider,
    string graphType)
  {
    return provider.FindSiteMapNodeByGraphType(graphType, true);
  }

  /// <summary>
  /// Finds a site map node by graph type regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Full name of the graph type</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the current user doesn't have access to the node</returns>
  /// <remarks>Graph type name is a fully qualified name of the graph type (typeof(MyMaint).FullName)</remarks>
  internal static PXSiteMapNode FindSiteMapNodeByGraphTypeUnsecure(
    this PXSiteMapProvider provider,
    string graphType)
  {
    return provider.FindSiteMapNodeByGraphType(graphType, false);
  }

  /// <summary>Finds a site map nodes by graph type</summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Full name of the graph type</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  /// <remarks>Graph type name is a fully qualified name of the graph type (typeof(MyMaint).FullName)</remarks>
  [PXInternalUseOnly]
  public static IEnumerable<PXSiteMapNode> FindSiteMapNodesByGraphType(
    this PXSiteMapProvider provider,
    string graphType)
  {
    return provider.Definitions.FindSiteMapNodesFromGraphType(graphType, true);
  }

  /// <summary>
  /// Finds a site map nodes by graph type regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the node</param>
  /// <param name="graphType">Full name of the graph type</param>
  /// <returns>The set of site map nodes or empty set if they are not found</returns>
  /// <remarks>Graph type name is a fully qualified name of the graph type (typeof(MyMaint).FullName)</remarks>
  internal static IEnumerable<PXSiteMapNode> FindSiteMapNodesByGraphTypeUnsecure(
    this PXSiteMapProvider provider,
    string graphType)
  {
    return provider.Definitions.FindSiteMapNodesFromGraphType(graphType, false);
  }

  /// <summary>Checks whether the screen exists in the provider</summary>
  /// <param name="provider">Provider to be looked for the screen</param>
  /// <param name="screenID">The screen identifier</param>
  /// <returns><see langword="true" /> if the screen exists, otherwise <see langword="false" /></returns>
  internal static bool ScreenExists(this PXSiteMapProvider provider, string screenID)
  {
    return provider.FindSiteMapNodeByScreenIDUnsecure(screenID) != null;
  }

  /// <summary>
  /// Gets all descendant nodes of the node regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>All descendants of the node or an empty array</returns>
  internal static PXSiteMapNode[] GetDescendants(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    List<PXSiteMapNode> nodes = new List<PXSiteMapNode>();
    provider.enumSiteMap(node, nodes, false);
    return nodes.ToArray();
  }

  private static void enumSiteMap(
    this PXSiteMapProvider provider,
    PXSiteMapNode current,
    List<PXSiteMapNode> nodes,
    bool securityTrimmingEnabled)
  {
    if (current != null)
      nodes.Add(current);
    foreach (PXSiteMapNode childNode in provider.Definitions.GetChildNodes(current, securityTrimmingEnabled))
      provider.enumSiteMap(childNode, nodes, securityTrimmingEnabled);
  }

  /// <summary>
  /// Gets all nodes of the provider which are accessible to the current user
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <returns>The set of all nodes of the provider which are accessible to the current user</returns>
  [PXInternalUseOnly]
  public static IEnumerable<PXSiteMapNode> GetNodes(this PXSiteMapProvider provider)
  {
    return provider.Definitions.Nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (o => o.IsAccessibleToUser()));
  }

  /// <summary>Gets child nodes of the node</summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  internal static IList<PXSiteMapNode> GetChildNodes(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    return (IList<PXSiteMapNode>) provider.Definitions.GetChildNodes(node, provider.SecurityTrimmingEnabled).ToList<PXSiteMapNode>();
  }

  /// <summary>
  /// Gets child nodes of the node regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>The set of site map nodes or empty set if they are not found</returns>
  internal static IList<PXSiteMapNode> GetChildNodesUnsecure(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    return (IList<PXSiteMapNode>) provider.Definitions.GetChildNodes(node, false).ToList<PXSiteMapNode>();
  }

  /// <summary>Gets child nodes with non-empty screen identifier</summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>The set of site map nodes or empty set if they are not found or the user doesn't have access to the node</returns>
  /// <remarks>This method skips nodes where ScreenID is empty</remarks>
  internal static IEnumerable<PXSiteMapNode> GetChildNodesSimple(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    List<PXSiteMapNode> pxSiteMapNodeList = new List<PXSiteMapNode>();
    foreach (PXSiteMapNode childNode in provider.Definitions.GetChildNodes(node, provider.SecurityTrimmingEnabled))
    {
      if (string.IsNullOrWhiteSpace(childNode.ScreenID))
        pxSiteMapNodeList.AddRange(provider.GetChildNodesSimple(childNode));
      else
        pxSiteMapNodeList.Add(childNode);
    }
    return (IEnumerable<PXSiteMapNode>) pxSiteMapNodeList.AsReadOnly();
  }

  /// <summary>
  /// Gets child nodes with non-empty screen identifier regardless the current user's permissions
  /// </summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>The set of site map nodes or empty set if they are not found</returns>
  /// <remarks>This method skips nodes where ScreenID is empty</remarks>
  internal static IEnumerable<PXSiteMapNode> GetChildNodesUnsecureSimple(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    List<PXSiteMapNode> pxSiteMapNodeList = new List<PXSiteMapNode>();
    foreach (PXSiteMapNode childNode in provider.Definitions.GetChildNodes(node, false))
    {
      if (string.IsNullOrWhiteSpace(childNode.ScreenID))
        pxSiteMapNodeList.AddRange(provider.GetChildNodesUnsecureSimple(childNode));
      else
        pxSiteMapNodeList.Add(childNode);
    }
    return (IEnumerable<PXSiteMapNode>) pxSiteMapNodeList.AsReadOnly();
  }

  /// <summary>
  /// Returns child nodes of required node that can be automated and for which user has access
  /// </summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>Child nodes of the node that can be automated and for which user has access</returns>
  internal static bool HasChildNodesThatCanBeAutomated(
    this PXSiteMapProvider provider,
    PXSiteMapNode node)
  {
    foreach (PXSiteMapNode childNode in provider.Definitions.GetChildNodes(node, false))
    {
      bool flag1 = childNode.Url.StartsWith("~");
      bool flag2 = childNode.Url.Contains("wiki.aspx");
      bool flag3 = childNode.Url.Contains("reportlauncher.aspx") || childNode.Url.Contains("rmlauncher.aspx");
      if (childNode.HasChildNodes() || flag1 && !flag2 && !flag3)
        return true;
    }
    return false;
  }

  /// <summary>Gets parent node of the node</summary>
  /// <param name="provider">Provider to be looked for the nodes</param>
  /// <param name="node">Node to be analyzed</param>
  /// <returns>Parent node of the node or <see langword="null" /> if the parent node is not found</returns>
  internal static PXSiteMapNode GetParentNode(this PXSiteMapProvider provider, PXSiteMapNode node)
  {
    return provider.Definitions.GetParentNode(node);
  }

  /// <summary>
  /// Gets access rights of the current user to the specific site map node with the specific screen identifier
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <param name="screenID">The screen identifier</param>
  /// <param name="node">The site map node</param>
  /// <returns>Computed access rights</returns>
  internal static PXCacheRights GetAccessRights(
    this PXSiteMapProvider provider,
    string screenID,
    PXSiteMapNode node)
  {
    PXCacheRights accessRights = PXCacheRights.Denied;
    if (node != null)
    {
      if (PXList.Provider.IsList(node.ScreenID))
      {
        string entryScreenId = PXList.Provider.GetEntryScreenID(node.ScreenID);
        if (entryScreenId != null)
        {
          PXSiteMapNode screenIdUnsecure = provider.FindSiteMapNodeByScreenIDUnsecure(entryScreenId);
          if (screenIdUnsecure != null)
            return provider.GetAccessRights(entryScreenId, screenIdUnsecure);
        }
      }
      PXRoleList pxRoles;
      if (node != null && (pxRoles = node.PXRoles) != null)
      {
        if (pxRoles.Common != null)
        {
          bool flag = false;
          PXCacheRights pxCacheRights1 = PXCacheRights.Denied;
          foreach (string str in pxRoles.Common)
          {
            if (PXSiteMapProviderExtensions.IsUserInRole(str))
            {
              flag = true;
              PXCacheRights rights = PXAccess.Provider.GetRights(str, screenID);
              if (accessRights < rights)
                accessRights = rights;
            }
            else if (PXSiteMapProviderExtensions.IsUniversalRoleInERP(str))
              pxCacheRights1 = PXAccess.Provider.GetRights(str, screenID);
          }
          if (!flag && accessRights < pxCacheRights1)
            accessRights = pxCacheRights1;
          if (pxRoles.Prioritized != null)
          {
            PXCacheRights pxCacheRights2 = PXCacheRights.Denied;
            foreach (string str in pxRoles.Prioritized)
            {
              if (PXSiteMapProviderExtensions.IsUserInRole(str))
              {
                PXCacheRights rights = PXAccess.Provider.GetRights(str, screenID);
                if (pxCacheRights2 < rights)
                  pxCacheRights2 = rights;
              }
            }
            if (pxCacheRights2 < accessRights || !flag && PXAccess.Provider.UserManagedAssignmentToPrioritizedRoles)
              accessRights = pxCacheRights2;
          }
        }
        else if (pxRoles.Prioritized != null)
        {
          foreach (string str in pxRoles.Prioritized)
          {
            if (PXSiteMapProviderExtensions.IsUserInRole(str))
            {
              PXCacheRights rights = PXAccess.Provider.GetRights(str, screenID);
              if (accessRights < rights)
                accessRights = rights;
            }
          }
          if (pxRoles.CommonDenied != null)
          {
            foreach (string role in pxRoles.CommonDenied)
            {
              if (PXSiteMapProviderExtensions.IsUserInRole(role))
              {
                accessRights = PXCacheRights.Denied;
                break;
              }
            }
          }
        }
      }
      else if (node.Roles != null)
      {
        foreach (string role in (IEnumerable<string>) node.Roles)
        {
          if (PXSiteMapProviderExtensions.IsUserInRole(role))
          {
            PXCacheRights rights = PXAccess.Provider.GetRights(role, screenID);
            if (accessRights < rights)
              accessRights = rights;
          }
        }
      }
      else if (node.Roles == null)
        accessRights = PXCacheRights.Delete;
    }
    return accessRights;
  }

  private static bool IsUserInRole(string role)
  {
    IPrincipal authUser = PXContext.PXIdentity.AuthUser;
    return authUser != null && authUser.IsInRole(role);
  }

  private static bool IsUniversalRoleInERP(string role)
  {
    return role == "*" && !PortalHelper.IsPortalContext(PortalContexts.Modern);
  }

  /// <summary>
  /// Gets screen identifiers where the specific graph type is used
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <param name="graphType">Graph type</param>
  /// <returns>A set of screen identifiers</returns>
  [PXInternalUseOnly]
  public static IEnumerable<string> GetScreenIdsByGraphType(
    this PXSiteMapProvider provider,
    System.Type graphType)
  {
    if (graphType == (System.Type) null)
      throw new ArgumentNullException(nameof (graphType));
    return provider.FindSiteMapNodesByGraphTypeUnsecure(CustomizedTypeManager.GetTypeNotCustomized(graphType).FullName).Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (n => n.ScreenID)).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Gets first found screen identifier where the specific graph type is used
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <param name="graphType">Graph type</param>
  /// <returns>A first found screen identifiers</returns>
  [PXInternalUseOnly]
  public static string GetScreenIdByGraphType(this PXSiteMapProvider provider, System.Type graphType)
  {
    return provider.GetScreenIdsByGraphType(graphType).FirstOrDefault<string>();
  }

  /// <summary>
  /// Gets access rights of the current user to the specific screen
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <param name="screenID">The screen identifier</param>
  /// <returns>Computed access rights</returns>
  public static PXCacheRights AccessRights(this PXSiteMapProvider provider, string screenID)
  {
    PXSiteMapNode mapNodeByScreenId = provider.FindSiteMapNodeByScreenID(screenID);
    return provider.RootNode.Equals((object) mapNodeByScreenId) ? PXCacheRights.Select : provider.GetAccessRights(screenID, mapNodeByScreenId);
  }

  /// <summary>
  /// Gets access rights of the current user with the specific role to the specific screen
  /// </summary>
  /// <param name="provider">Provider to be used</param>
  /// <param name="screenID">The screen identifier</param>
  /// <param name="roles">Role to be checked</param>
  /// <returns>Computed access rights</returns>
  internal static PXCacheRights AccessRights(
    this PXSiteMapProvider provider,
    string screenID,
    string roles)
  {
    PXSiteMapNode mapNodeByScreenId = provider.FindSiteMapNodeByScreenID(screenID);
    return mapNodeByScreenId != null && (mapNodeByScreenId.Roles != null && mapNodeByScreenId.Roles.Contains(roles) || mapNodeByScreenId.Roles == null) ? PXCacheRights.Select : PXCacheRights.Denied;
  }
}
