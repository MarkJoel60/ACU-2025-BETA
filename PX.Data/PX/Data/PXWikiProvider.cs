// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Security;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Represents wiki site map provider</summary>
[method: PXInternalUseOnly]
public class PXWikiProvider(
  IOptions<PXWikiSiteMapOptions> options,
  IHttpContextAccessor httpContextAccessor,
  IRoleManagementService roleManagementService) : PXSiteMapProvider((IOptions<PXSiteMapOptions>) options, httpContextAccessor, roleManagementService)
{
  /// <inheritdoc />
  [PXInternalUseOnly]
  public override bool IsAccessibleToUser(PXSiteMapNode node)
  {
    return ((PXWikiProvider.WikiDefinition) this.Definitions).GetAccessRights((PXWikiMapNode) node) >= PXWikiRights.Select;
  }

  /// <summary>Gets access rights by the page identifier</summary>
  /// <param name="pageID">The page identifier</param>
  [PXInternalUseOnly]
  public PXWikiRights GetAccessRights(Guid pageID)
  {
    PXWikiMapNode siteMapNodeFromKey = (PXWikiMapNode) this.FindSiteMapNodeFromKey(pageID);
    return siteMapNodeFromKey != null ? ((PXWikiProvider.WikiDefinition) this.Definitions).GetAccessRights(siteMapNodeFromKey) : PXWikiRights.Denied;
  }

  protected override PXSiteMapProvider.Definition GetSlot(string slotName)
  {
    return (PXSiteMapProvider.Definition) PXDatabase.GetSlot<PXWikiProvider.WikiDefinition, PXWikiProvider>(slotName + Thread.CurrentThread.CurrentUICulture.Name, this, typeof (WikiPage), typeof (WikiPageLanguage), typeof (WikiAccessRights));
  }

  /// <inheritdoc />
  protected override void ResetSlot(string slotName)
  {
    PXDatabase.ResetSlot<PXWikiProvider.WikiDefinition>(slotName + Thread.CurrentThread.CurrentUICulture.Name, typeof (WikiPage), typeof (WikiPageLanguage), typeof (WikiAccessRights));
  }

  /// <inheritdoc />
  [PXInternalUseOnly]
  public override PXSiteMapNode FindSiteMapNode(string rawUrl)
  {
    if (string.IsNullOrEmpty(rawUrl))
      return (PXSiteMapNode) null;
    Guid? guid1 = GUID.CreateGuid(rawUrl);
    if (guid1.HasValue)
      return this.FindSiteMapNodeFromKey(guid1.Value);
    int startIndex = rawUrl.IndexOf('?');
    string input = startIndex <= -1 || startIndex >= rawUrl.Length ? (string) null : rawUrl.Substring(startIndex);
    if (input != null)
    {
      string wiki = (string) null;
      string art = (string) null;
      foreach (Match match in Wiki.RegexQueryString.Matches(input))
      {
        string strA = match.Groups["Name"].Value;
        string str = WebUtility.UrlDecode(match.Groups["Value"].Value);
        if (string.Compare(strA, "pageID", true) == 0)
        {
          Guid? guid2 = GUID.CreateGuid(str);
          if (guid2.HasValue)
            return this.FindSiteMapNodeFromKey(guid2.Value);
        }
        if (string.Compare(strA, "wiki", true) == 0)
          wiki = str;
        if (string.Compare(strA, "art", true) == 0)
          art = str;
        if (!string.IsNullOrEmpty(wiki) && !string.IsNullOrEmpty(art))
          return (PXSiteMapNode) this.FindSiteMapNode(wiki, art);
      }
    }
    return base.FindSiteMapNode(rawUrl);
  }

  /// <summary>Gets the wiki page identifier by its name</summary>
  /// <param name="name">The name of the wiki page</param>
  public Guid GetWikiPageIDByPageName(string name)
  {
    PXWikiMapNode wikiByPageName = this.FindWikiByPageName(name);
    return wikiByPageName == null ? Guid.Empty : wikiByPageName.NodeID;
  }

  [PXInternalUseOnly]
  public class WikiDefinition : 
    PXSiteMapProvider.Definition,
    IPrefetchable<PXWikiProvider>,
    IPXCompanyDependent
  {
    private readonly Dictionary<string, PXWikiMapNode> _nodesByName = new Dictionary<string, PXWikiMapNode>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private readonly List<Guid> _published = new List<Guid>();
    private readonly List<Guid> _publishedOlderVersion = new List<Guid>();
    private readonly Dictionary<Guid, List<PXWikiRights?>> _wikiRights = new Dictionary<Guid, List<PXWikiRights?>>();
    private readonly List<string> _wikiRoles = new List<string>();
    private bool _stateLoaded;

    void IPrefetchable<PXWikiProvider>.Prefetch(PXWikiProvider provider)
    {
      PXGraph graph = new PXGraph();
      PXSiteMapNode rootNode = PXSiteMap.RootNode;
      this.AddNode((PXSiteMapNode) this.CreateNode(provider, Guid.Empty, rootNode.Url, rootNode.Title, true), Guid.Empty);
      foreach (WikiPageWithCurrentLanguage page in this.GetSourcePrefetch(provider, graph))
      {
        string str1 = (string) null;
        string str2 = (string) null;
        Guid? nullable = page.WikiID;
        Guid empty = Guid.Empty;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0)
        {
          PXSiteMapProvider provider1 = PXSiteMap.Provider;
          nullable = page.PageID;
          Guid valueOrDefault = nullable.GetValueOrDefault();
          PXSiteMapNode siteMapNodeFromKey = provider1.FindSiteMapNodeFromKey(valueOrDefault);
          if (siteMapNodeFromKey != null)
            str1 = siteMapNodeFromKey.Url;
        }
        if (str1 == null)
        {
          nullable = page.PageID;
          Guid pageID = nullable.Value;
          nullable = page.WikiID;
          Guid wikiID = nullable.Value;
          str1 = Wiki.Url(pageID, wikiID);
        }
        if (str2 == null)
          str2 = !string.IsNullOrEmpty(page.TitleLoc) ? page.TitleLoc : (string.IsNullOrEmpty(page.Title) ? page.Name : page.Title);
        string str3 = page.SummaryLoc ?? page.Summary;
        PXWikiProvider provider2 = provider;
        nullable = page.PageID;
        Guid id = nullable.Value;
        string url = str1;
        string title = str2;
        int num = page.Folder.Value ? 1 : 0;
        string summary = str3;
        PXWikiMapNode node1 = this.CreateNode(provider2, id, url, title, num != 0, summary);
        nullable = page.WikiID;
        PXWikiMapNode siteMapNodeFromKey1 = this.FindSiteMapNodeFromKey(nullable.Value, false) as PXWikiMapNode;
        this.UpdateNode(node1, page, siteMapNodeFromKey1?.Name);
        PXWikiMapNode node2 = node1;
        nullable = page.ParentUID;
        Guid parentID = nullable.Value;
        this.AddNode((PXSiteMapNode) node2, parentID);
      }
      this.UpdateMainSiteMap();
    }

    protected override void AddNode(PXSiteMapNode node, Guid parentID)
    {
      base.AddNode(node, parentID);
      PXWikiMapNode node1 = (PXWikiMapNode) node;
      PXSiteMapNode pxSiteMapNode;
      if (parentID != node.NodeID && this.KeyTable.TryGetValue(parentID, out pxSiteMapNode) && ((PXWikiMapNode) pxSiteMapNode).Rights != null && node1.Rights != null)
      {
        for (int index = 0; index < node1.Rights.Length; ++index)
        {
          if (((PXWikiMapNode) pxSiteMapNode).Rights[index].HasValue && !node1.Rights[index].HasValue)
            node1.Rights[index] = ((PXWikiMapNode) pxSiteMapNode).Rights[index];
        }
      }
      this.updateChildren(node1);
      if (this._nodesByName.ContainsKey(((PXWikiMapNode) node).StringID))
        return;
      this._nodesByName.Add(((PXWikiMapNode) node).StringID, (PXWikiMapNode) node);
    }

    private void updateChildren(PXWikiMapNode node)
    {
      foreach (PXWikiMapNode childNode in this.GetChildNodes((PXSiteMapNode) node, false))
      {
        bool flag = false;
        if (node.Rights != null && childNode.Rights != null)
        {
          for (int index = 0; index < node.Rights.Length; ++index)
          {
            if (node.Rights[index].HasValue && !childNode.Rights[index].HasValue)
            {
              childNode.Rights[index] = node.Rights[index];
              flag = true;
            }
          }
        }
        if (flag)
          this.updateChildren(childNode);
      }
    }

    private void UpdateMainSiteMap()
    {
      if (!(PXSiteMap.Provider is PXDatabaseSiteMapProvider))
        return;
      List<PXSiteMapNode> pxSiteMapNodeList = new List<PXSiteMapNode>();
      foreach (PXSiteMapNode childNode in PXSiteMap.Provider.Definitions.GetChildNodes(PXSiteMap.RootNode, false))
        pxSiteMapNodeList.Add(childNode);
      foreach (PXSiteMapNode pxSiteMapNode in pxSiteMapNodeList)
      {
        foreach (PXWikiMapNode childNode in this.GetChildNodes(this.RootNode, false))
        {
          if (pxSiteMapNode.Url.Contains(childNode.NodeID.ToString()))
          {
            if (childNode.Rights == null)
            {
              pxSiteMapNode.Roles = (IList<string>) null;
            }
            else
            {
              List<string> stringList = new List<string>();
              for (int index = 0; index < this._wikiRoles.Count; ++index)
              {
                PXWikiRights? right = childNode.Rights[index];
                PXWikiRights pxWikiRights = PXWikiRights.Select;
                if (right.GetValueOrDefault() >= pxWikiRights & right.HasValue)
                  stringList.Add(this._wikiRoles[index]);
              }
              pxSiteMapNode.Roles = (IList<string>) stringList.ToArray();
            }
          }
        }
      }
    }

    private void EnsureState()
    {
      if (this._stateLoaded)
        return;
      foreach (PXDataRecord rec in PXDatabase.SelectMulti<WikiPageLanguage>(new PXDataField("PageID"), new PXDataField("LastRevisionID"), new PXDataField("LastPublishedID"), (PXDataField) new PXDataFieldValue("Language", PXDbType.VarChar, new int?(50), (object) "en-US"), (PXDataField) new PXDataFieldValue("LastPublishedID", PXDbType.Int, new int?(4), (object) 0, PXComp.GT)))
        this.AddToPublished(rec);
      if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
      {
        foreach (PXDataRecord rec in PXDatabase.SelectMulti<WikiPageLanguage>(new PXDataField("PageID"), new PXDataField("LastRevisionID"), new PXDataField("LastPublishedID"), (PXDataField) new PXDataFieldValue("Language", PXDbType.VarChar, new int?(50), (object) Thread.CurrentThread.CurrentCulture.Name), (PXDataField) new PXDataFieldValue("LastPublishedID", PXDbType.Int, new int?(4), (object) 0, PXComp.GT)))
          this.AddToPublished(rec);
      }
      List<PXWikiRights?> collection = new List<PXWikiRights?>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<WikiAccessRights>(new PXDataField("PageID"), new PXDataField("RoleName"), new PXDataField("AccessRights"), (PXDataField) new PXDataFieldValue("ApplicationName", (object) PXAccess.Provider.ApplicationName)))
      {
        string str = pxDataRecord.GetString(1);
        int index = this._wikiRoles.IndexOf(str);
        if (index == -1)
        {
          this._wikiRoles.Add(str);
          collection.Add(new PXWikiRights?());
          index = this._wikiRoles.Count - 1;
        }
        Guid key = pxDataRecord.GetGuid(0).Value;
        PXWikiRights pxWikiRights = (PXWikiRights) pxDataRecord.GetInt16(2).Value;
        List<PXWikiRights?> nullableList;
        if (!this._wikiRights.TryGetValue(key, out nullableList))
        {
          this._wikiRights[key] = nullableList = new List<PXWikiRights?>((IEnumerable<PXWikiRights?>) collection);
        }
        else
        {
          for (int count = nullableList.Count; count < collection.Count; ++count)
            nullableList.Add(new PXWikiRights?());
        }
        nullableList[index] = new PXWikiRights?(pxWikiRights);
      }
      this._stateLoaded = true;
    }

    private void AddToPublished(PXDataRecord rec)
    {
      Guid guid = rec.GetGuid(0).Value;
      int? int32 = rec.GetInt32(1);
      int num1 = int32.Value;
      int32 = rec.GetInt32(2);
      int num2 = int32.Value;
      this._published.Add(guid);
      int num3 = num2;
      if (num1 == num3)
        return;
      this._publishedOlderVersion.Add(guid);
    }

    protected PXWikiRights?[] GetExplicitRights(Guid pageID)
    {
      this.EnsureState();
      List<PXWikiRights?> nullableList;
      PXWikiRights?[] array;
      if (this._wikiRights.TryGetValue(pageID, out nullableList))
      {
        array = nullableList.ToArray();
        if (array.Length < this._wikiRoles.Count)
          Array.Resize<PXWikiRights?>(ref array, this._wikiRoles.Count);
      }
      else
        array = new PXWikiRights?[this._wikiRoles.Count];
      return array;
    }

    private PXWikiMapNode CreateNode(
      PXWikiProvider provider,
      Guid id,
      string url,
      string title,
      bool isFolder,
      string summary = null)
    {
      if (id == Guid.Empty)
        title = (string) null;
      PXWikiMapNode node = new PXWikiMapNode((PXSiteMapProvider) provider, id, url, title, (PXRoleList) null, new bool?(false), new bool?(false), (string) null, new bool?(isFolder), (string) null);
      if (summary != null)
        node.Summary = summary;
      return node;
    }

    protected void UpdateNode(
      PXWikiMapNode node,
      WikiPageWithCurrentLanguage page,
      string wikiName)
    {
      node.Rights = this.GetExplicitRights(page.PageID.Value);
      PXWikiMapNode pxWikiMapNode1 = node;
      Guid? nullable = page.WikiID;
      Guid empty = Guid.Empty;
      int num1 = nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0;
      pxWikiMapNode1.IsRootWikiNode = num1 != 0;
      PXWikiMapNode pxWikiMapNode2 = node;
      List<Guid> published = this._published;
      nullable = page.PageID;
      Guid guid1 = nullable.Value;
      int num2 = published.Contains(guid1) ? 1 : 0;
      pxWikiMapNode2.IsPublished = num2 != 0;
      PXWikiMapNode pxWikiMapNode3 = node;
      List<Guid> publishedOlderVersion = this._publishedOlderVersion;
      nullable = page.PageID;
      Guid guid2 = nullable.Value;
      int num3 = !publishedOlderVersion.Contains(guid2) ? 1 : 0;
      pxWikiMapNode3.IsLastRevPublished = num3 != 0;
      PXWikiMapNode pxWikiMapNode4 = node;
      int? articleType = page.ArticleType;
      int num4 = 1;
      int num5 = articleType.GetValueOrDefault() == num4 & articleType.HasValue ? 1 : 0;
      pxWikiMapNode4.IsRootDeletedItems = num5 != 0;
      node.Wiki = wikiName;
      node.Name = page.Name;
    }

    private IEnumerable<WikiPageWithCurrentLanguage> GetSourcePrefetch(
      PXWikiProvider provider,
      PXGraph graph)
    {
      PXSelectDelegate pxSelectDelegate = new PXSelectDelegate(this.SelectSource);
      PXGraph graph1 = graph;
      BqlCommand instance = BqlCommand.CreateInstance(typeof (Select3<WikiPageWithCurrentLanguage, OrderBy<Asc<WikiPage.wikiID, Asc<WikiPage.parentUID, Desc<WikiPage.articleType, Asc<WikiPage.number>>>>>>));
      PXSelectDelegate handler = pxSelectDelegate;
      foreach (WikiPageWithCurrentLanguage withCurrentLanguage in new PXView(graph1, true, instance, (Delegate) handler).SelectMulti())
        yield return withCurrentLanguage;
    }

    private IEnumerable SelectSource()
    {
      for (int i = 0; i < 16 /*0x10*/; ++i)
      {
        int repeatCount = Array.IndexOf<int>(PXDatabase.Provider.SqlDialect.GetGuidByteOrder(), 15) * 2;
        StringBuilder stringBuilder1 = new StringBuilder().Append('0', repeatCount).AppendFormat("{0:X}", (object) i).Append('0', 31 /*0x1F*/ - repeatCount);
        StringBuilder stringBuilder2 = new StringBuilder().Append('F', repeatCount).AppendFormat("{0:X}", (object) i).Append('F', 31 /*0x1F*/ - repeatCount);
        PXGraph currentGraph = PXView.CurrentGraph;
        object[] objArray = new object[2]
        {
          (object) Guid.Parse(stringBuilder1.ToString()),
          (object) Guid.Parse(stringBuilder2.ToString())
        };
        foreach (PXResult<WikiPageWithCurrentLanguage> pxResult in PXSelectBase<WikiPageWithCurrentLanguage, PXSelectReadonly<WikiPageWithCurrentLanguage, Where<WikiPageWithCurrentLanguage.pageID, Between<Required<WikiPageWithCurrentLanguage.pageID>, Required<WikiPageWithCurrentLanguage.pageID>>>>.Config>.Select(currentGraph, objArray))
        {
          WikiPageWithCurrentLanguage withCurrentLanguage = (WikiPageWithCurrentLanguage) pxResult;
          withCurrentLanguage.Title = withCurrentLanguage.TitleLoc ?? withCurrentLanguage.Title;
          withCurrentLanguage.Summary = withCurrentLanguage.Summary ?? withCurrentLanguage.SummaryLoc;
          yield return (object) withCurrentLanguage;
        }
      }
    }

    /// <summary>Tries to find the node by its name</summary>
    /// <param name="name">The node name</param>
    /// <param name="node">The found node or <see langword="null" /></param>
    /// <returns><see langword="true" /> if the node is found, otherwise <see langword="false" /></returns>
    internal bool TryGetNodeByName(string name, out PXWikiMapNode node)
    {
      return this._nodesByName.TryGetValue(name, out node);
    }

    /// <summary>Gets role index in the storage by its name</summary>
    /// <param name="roleName">The role name</param>
    /// <returns>The role index or -1 if the role is not found</returns>
    internal int GetRoleIndexByName(string roleName) => this._wikiRoles.IndexOf(roleName);

    /// <summary>Gets the role name by its index in the storage</summary>
    /// <param name="index">The role index</param>
    /// <returns>The role name</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Occurs when the provided index is out of the storage range</exception>
    internal string GetRoleNameByIndex(int index)
    {
      if (index < 0 && index >= this._wikiRoles.Count)
        throw new ArgumentOutOfRangeException(nameof (index));
      return this._wikiRoles[index];
    }
  }
}
