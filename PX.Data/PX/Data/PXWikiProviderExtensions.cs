// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class PXWikiProviderExtensions
{
  /// <summary>Finds wiki node by the wiki name and the article name</summary>
  /// <param name="definition">The Wiki provider</param>
  /// <param name="wiki">The wiki name</param>
  /// <param name="art">The article name</param>
  /// <returns>The wiki node or <see langword="null" /> if the node is not found</returns>
  internal static PXWikiMapNode FindSiteMapNode(
    this PXWikiProvider provider,
    string wiki,
    string art)
  {
    return ((PXWikiProvider.WikiDefinition) provider.Definitions).FindSiteMapNode(wiki, art);
  }

  /// <summary>
  /// Gets access rights of the specific wiki node with the specific role
  /// with checking if the current user has access to the node
  /// </summary>
  /// <param name="definition">The Wiki provider</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns>Computed access rights</returns>
  internal static PXWikiRights GetAccessRights(
    this PXWikiProvider provider,
    Guid pageID,
    string roleName)
  {
    return ((PXWikiProvider.WikiDefinition) provider.Definitions).GetAccessRights(pageID, roleName);
  }

  /// <summary>
  /// Gets access rights of the specific wiki node with the specific role
  /// without checking if the current user has access to the node
  /// </summary>
  /// <param name="definition">The Wiki provider</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns>Computed access rights</returns>
  internal static PXWikiRights GetAccessRightsUnsecure(
    this PXWikiProvider provider,
    Guid pageID,
    string roleName)
  {
    return ((PXWikiProvider.WikiDefinition) provider.Definitions).GetAccessRightsUnsecure(pageID, roleName);
  }

  /// <summary>
  /// Checks if the current user has access to the specific wiki page
  /// </summary>
  /// <param name="definition">The Wiki provider</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <returns><see langword="true" /> if the current user has access to the page, otherwise <see langword="false" /></returns>
  internal static bool HasAccessRights(this PXWikiProvider provider, Guid pageID)
  {
    return ((PXWikiProvider.WikiDefinition) provider.Definitions).HasAccessRights(pageID);
  }

  /// <summary>
  /// Checks if the current user has access to the specific wiki page with the specific role
  /// </summary>
  /// <param name="definition">The Wiki provider</param>
  /// <param name="pageID">The wiki page identifier</param>
  /// <param name="roleName">The role name</param>
  /// <returns><see langword="true" /> if the current user has access to the page, otherwise <see langword="false" /></returns>
  internal static bool HasAccessRights(this PXWikiProvider provider, Guid pageID, string roleName)
  {
    return ((PXWikiProvider.WikiDefinition) provider.Definitions).HasAccessRights(pageID, roleName);
  }

  /// <summary>Gets wiki node identifier by its title</summary>
  /// <param name="provider">The Wiki provider</param>
  /// <param name="wikiTitle">Title of the wiki node</param>
  /// <returns>The node identifier or <see cref="F:System.Guid.Empty" /> if the node is not found</returns>
  internal static Guid GetWikiID(this PXWikiProvider provider, string wikiTitle)
  {
    PXWikiMapNode wiki = provider.FindWiki(wikiTitle);
    return wiki == null ? Guid.Empty : wiki.NodeID;
  }

  /// <summary>Gets wiki node identifier by its url</summary>
  /// <param name="provider">The Wiki provider</param>
  /// <param name="wikiTitle">URL of the wiki node</param>
  /// <returns>The node identifier or <see cref="F:System.Guid.Empty" /> if the node is not found</returns>
  internal static Guid GetWikiIDFromUrl(this PXWikiProvider provider, string url)
  {
    if (string.IsNullOrEmpty(url))
      return Guid.Empty;
    string[] strArray1 = url.Split('?');
    if (strArray1.Length != 2)
      return Guid.Empty;
    string str1 = strArray1[1];
    char[] chArray1 = new char[1]{ '&' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '=' };
      string[] strArray2 = str2.Split(chArray2);
      if (strArray2.Length == 2 && string.Compare(strArray2[0], "wikiid", true) == 0)
      {
        Guid wikiID = GUID.CreateGuid(strArray2[1].Split('#')[0]) ?? Guid.Empty;
        return provider.FindWiki(wikiID) != null ? wikiID : Guid.Empty;
      }
    }
    return Guid.Empty;
  }

  /// <summary>Gets wiki node by its title</summary>
  /// <param name="provider">The Wiki provider</param>
  /// <param name="wikiTitle">Title of the wiki node</param>
  /// <returns>The wiki node or <see cref="F:System.Guid.Empty" /> if the node is not found</returns>
  internal static PXWikiMapNode FindWiki(this PXWikiProvider provider, string wikiTitle)
  {
    PXSiteMapNode rootNode = provider.Definitions.RootNode;
    return rootNode != null ? (PXWikiMapNode) rootNode.ChildNodes.FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => n.Title == wikiTitle)) : (PXWikiMapNode) null;
  }

  /// <summary>Gets wiki node by its name</summary>
  /// <param name="provider">The Wiki provider</param>
  /// <param name="name">Name of the wiki node</param>
  /// <returns>The wiki node or <see cref="F:System.Guid.Empty" /> if the node is not found</returns>
  internal static PXWikiMapNode FindWikiByPageName(this PXWikiProvider provider, string name)
  {
    return (PXWikiMapNode) provider.Definitions.Nodes.FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => ((PXWikiMapNode) node).Name == name));
  }

  /// <summary>Gets wiki node by its identifier</summary>
  /// <param name="provider">The Wiki provider</param>
  /// <param name="wikiID">Identifier of the wiki node</param>
  /// <returns>The wiki node or <see cref="F:System.Guid.Empty" /> if the node is not found</returns>
  internal static PXWikiMapNode FindWiki(this PXWikiProvider provider, Guid wikiID)
  {
    PXSiteMapNode rootNode = provider.Definitions.RootNode;
    return rootNode != null ? (PXWikiMapNode) rootNode.ChildNodes.FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => n.NodeID == wikiID)) : (PXWikiMapNode) null;
  }
}
