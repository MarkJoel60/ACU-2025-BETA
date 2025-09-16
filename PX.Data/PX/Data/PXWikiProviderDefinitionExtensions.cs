// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiProviderDefinitionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Security.Principal;

#nullable disable
namespace PX.Data;

internal static class PXWikiProviderDefinitionExtensions
{
  /// <summary>Finds wiki node by the wiki name and the article name</summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="wiki">The wiki name</param>
  /// <param name="art">The article name</param>
  /// <returns>The wiki node or <see langword="null" /> if the node is not found</returns>
  internal static PXWikiMapNode FindSiteMapNode(
    this PXWikiProvider.WikiDefinition definition,
    string wiki,
    string art)
  {
    PXWikiMapNode node;
    return !definition.TryGetNodeByName(PXWikiMapNode.GetStringID(wiki, art), out node) ? (PXWikiMapNode) null : node;
  }

  /// <summary>Gets access rights of the specific wiki node</summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="node">The wiki node</param>
  /// <returns>Computed access rights</returns>
  internal static PXWikiRights GetAccessRights(
    this PXWikiProvider.WikiDefinition definition,
    PXWikiMapNode node)
  {
    if (node.IsRootWikiNode)
    {
      if (PXSiteMap.Provider.FindSiteMapNodeFromKey(node.Key) == null)
        return PXWikiRights.Denied;
    }
    else
    {
      PXWikiMapNode siteMapNode = definition.FindSiteMapNode("", node.Wiki);
      if (siteMapNode != null && PXSiteMap.Provider.FindSiteMapNodeFromKey(siteMapNode.Key) == null)
        return PXWikiRights.Denied;
    }
    IPrincipal authUser = PXContext.PXIdentity.AuthUser;
    if (node.Rights == null || authUser == null)
      return PXWikiRights.Select;
    PXWikiRights accessRights = PXWikiRights.Denied;
    if (authUser == null)
      return accessRights;
    for (int index = 0; index < node.Rights.Length; ++index)
    {
      PXWikiRights? right = node.Rights[index];
      PXWikiRights pxWikiRights = accessRights;
      if (right.GetValueOrDefault() > pxWikiRights & right.HasValue && authUser.IsInRole(definition.GetRoleNameByIndex(index)))
        accessRights = node.Rights[index].Value;
    }
    if (!node.IsPublished && !node.IsRootWikiNode && accessRights == PXWikiRights.Select)
      accessRights = PXWikiRights.Denied;
    return accessRights;
  }

  /// <summary>
  /// Gets access rights of the specific wiki node with the specific role
  /// without checking if the current user has access to the node
  /// </summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns>Computed access rights</returns>
  internal static PXWikiRights GetAccessRightsUnsecure(
    this PXWikiProvider.WikiDefinition definition,
    Guid pageID,
    string roleName)
  {
    PXWikiMapNode siteMapNodeFromKey = (PXWikiMapNode) definition.FindSiteMapNodeFromKey(pageID, false);
    if (siteMapNodeFromKey != null)
    {
      if (siteMapNodeFromKey.IsRootWikiNode)
      {
        if (PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(siteMapNodeFromKey.Key) == null)
          return PXWikiRights.Denied;
      }
      else
      {
        PXWikiMapNode siteMapNode = definition.FindSiteMapNode("", siteMapNodeFromKey.Wiki);
        if (siteMapNode != null && PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(siteMapNode.Key) == null)
          return PXWikiRights.Denied;
      }
    }
    if (siteMapNodeFromKey == null || siteMapNodeFromKey.Rights == null)
      return PXWikiRights.Select;
    int roleIndexByName = definition.GetRoleIndexByName(roleName);
    return roleIndexByName != -1 && siteMapNodeFromKey.Rights[roleIndexByName].HasValue ? siteMapNodeFromKey.Rights[roleIndexByName].Value : PXWikiRights.Denied;
  }

  /// <summary>
  /// Gets access rights of the specific wiki node with the specific role
  /// with checking if the current user has access to the node
  /// </summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns>Computed access rights</returns>
  internal static PXWikiRights GetAccessRights(
    this PXWikiProvider.WikiDefinition definition,
    Guid pageID,
    string roleName)
  {
    PXWikiMapNode siteMapNodeFromKey = (PXWikiMapNode) definition.FindSiteMapNodeFromKey(pageID, true);
    if (siteMapNodeFromKey != null)
    {
      if (siteMapNodeFromKey.IsRootWikiNode)
      {
        if (PXSiteMap.Provider.FindSiteMapNodeFromKey(siteMapNodeFromKey.Key) == null)
          return PXWikiRights.Denied;
      }
      else
      {
        PXWikiMapNode siteMapNode = definition.FindSiteMapNode("", siteMapNodeFromKey.Wiki);
        if (siteMapNode != null && PXSiteMap.Provider.FindSiteMapNodeFromKey(siteMapNode.Key) == null)
          return PXWikiRights.Denied;
      }
    }
    if (siteMapNodeFromKey == null || siteMapNodeFromKey.Rights == null)
      return PXWikiRights.Select;
    int roleIndexByName = definition.GetRoleIndexByName(roleName);
    return roleIndexByName != -1 && siteMapNodeFromKey.Rights[roleIndexByName].HasValue ? siteMapNodeFromKey.Rights[roleIndexByName].Value : PXWikiRights.Denied;
  }

  /// <summary>
  /// Checks if the current user has access to the specific wiki page
  /// </summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <returns><see langword="true" /> if the current user has access to the page, otherwise <see langword="false" /></returns>
  internal static bool HasAccessRights(this PXWikiProvider.WikiDefinition definition, Guid pageID)
  {
    PXWikiMapNode siteMapNodeFromKey;
    if ((siteMapNodeFromKey = (PXWikiMapNode) definition.FindSiteMapNodeFromKey(pageID, true)) == null || siteMapNodeFromKey.Rights == null)
      return false;
    for (int index = 0; index < siteMapNodeFromKey.Rights.Length; ++index)
    {
      if (siteMapNodeFromKey.Rights[index].HasValue)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Checks if the current user has access to the specific wiki page with the specific role
  /// </summary>
  /// <param name="definition">The Wiki provider definition</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns><see langword="true" /> if the current user has access to the page, otherwise <see langword="false" /></returns>
  internal static bool HasAccessRights(
    this PXWikiProvider.WikiDefinition definition,
    Guid pageID,
    string roleName)
  {
    PXWikiMapNode siteMapNodeFromKey;
    if ((siteMapNodeFromKey = (PXWikiMapNode) definition.FindSiteMapNodeFromKey(pageID, true)) == null || siteMapNodeFromKey.Rights == null)
      return false;
    int roleIndexByName = definition.GetRoleIndexByName(roleName);
    return roleIndexByName != -1 && siteMapNodeFromKey.Rights[roleIndexByName].HasValue;
  }
}
