// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.SearchService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using HtmlAgilityPack;
using PX.SP;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class SearchService
{
  public static IList<SearchService.SearchLookupItem> BuildComboList(Guid? activeModule)
  {
    List<SearchService.SearchLookupItem> searchLookupItemList = new List<SearchService.SearchLookupItem>();
    bool flag = false;
    string module = (string) null;
    string name = (string) null;
    if (activeModule.HasValue)
    {
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(activeModule.Value);
      if (siteMapNodeFromKey != null)
      {
        if (siteMapNodeFromKey.Url.StartsWith("~/Search/Wiki.aspx", StringComparison.InvariantCultureIgnoreCase))
          flag = true;
        if (!string.IsNullOrEmpty(siteMapNodeFromKey.Url) && siteMapNodeFromKey.Url.Length > 10)
        {
          module = siteMapNodeFromKey.Url.Substring(siteMapNodeFromKey.Url.Length - 8, 2).ToUpper();
          name = siteMapNodeFromKey.Title;
        }
      }
    }
    if (PortalHelper.IsPortalContext())
    {
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, PX.SM.Messages.GetLocal("Help")));
      if (name != null)
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveWiki, name));
    }
    else if (flag)
    {
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, PX.SM.Messages.GetLocal("Help")));
      if (name != null)
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveWiki, name));
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.Files, PX.SM.Messages.GetLocal("Files")));
    }
    else
    {
      if (name != null)
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveModule, name, module));
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllEntities, PX.SM.Messages.GetLocal("All Entities"), module));
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, PX.SM.Messages.GetLocal("Help"), module));
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.Files, PX.SM.Messages.GetLocal("Files")));
    }
    return (IList<SearchService.SearchLookupItem>) searchLookupItemList;
  }

  public static IList<SearchService.SearchLookupItem> BuildComboList2(
    Guid? activeModule,
    string query)
  {
    List<SearchService.SearchLookupItem> searchLookupItemList = new List<SearchService.SearchLookupItem>();
    bool flag = false;
    string module = (string) null;
    string name = (string) null;
    if (activeModule.HasValue)
    {
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(activeModule.Value);
      if (siteMapNodeFromKey != null)
      {
        if (siteMapNodeFromKey.Url.StartsWith("~/Search/Wiki.aspx", StringComparison.InvariantCultureIgnoreCase))
          flag = true;
        if (!string.IsNullOrEmpty(siteMapNodeFromKey.Url) && siteMapNodeFromKey.Url.Length > 10)
        {
          module = siteMapNodeFromKey.Url.Substring(siteMapNodeFromKey.Url.Length - 8, 2).ToUpper();
          name = siteMapNodeFromKey.Title;
        }
      }
    }
    UriBuilder uriBuilder = new UriBuilder(new Uri(HttpContext.Current.Request.GetExternalUrl(), ((Control) HttpContext.Current.Handler).ResolveUrl("~/Search/Search.aspx")));
    if (PXSiteMap.IsPortal)
    {
      if (flag)
      {
        SearchService.SearchLookupItem searchLookupItem = new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Help"), (object) query));
        searchLookupItem.Url = uriBuilder.Path + $"?query={query}&am={activeModule}&st={searchLookupItem.Type}";
        searchLookupItemList.Add(searchLookupItem);
        if (name != null)
          searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveWiki, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in {1}"), (object) query, (object) name)));
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllEntities, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in All Entities"), (object) query), module));
      }
      else
      {
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllEntities, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in All Entities"), (object) query), module));
        if (name != null)
          searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveModule, name, module));
        searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Help"), (object) query), module));
      }
    }
    else if (flag)
    {
      SearchService.SearchLookupItem searchLookupItem1 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Help"), (object) query));
      searchLookupItem1.Url = uriBuilder.Path + $"?query={query}&st={searchLookupItem1.Type}";
      searchLookupItemList.Add(searchLookupItem1);
      if (name != null)
      {
        SearchService.SearchLookupItem searchLookupItem2 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveWiki, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in {1}"), (object) query, (object) name));
        searchLookupItem2.Url = uriBuilder.Path + $"?query={query}&am={activeModule}&st={searchLookupItem2.Type}";
        searchLookupItemList.Add(searchLookupItem2);
      }
      SearchService.SearchLookupItem searchLookupItem3 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllEntities, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in All Entities"), (object) query), module);
      searchLookupItem3.Url = uriBuilder.Path + $"?query={query}&&st={searchLookupItem3.Type}";
      searchLookupItemList.Add(searchLookupItem3);
      SearchService.SearchLookupItem searchLookupItem4 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.Files, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Files"), (object) query));
      searchLookupItem4.Url = uriBuilder.Path + $"?query={query}&st={searchLookupItem4.Type}";
      searchLookupItemList.Add(searchLookupItem4);
    }
    else
    {
      if (name != null)
      {
        SearchService.SearchLookupItem searchLookupItem = new SearchService.SearchLookupItem(SearchService.SearchLookupType.ActiveModule, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in {1}"), (object) query, (object) name), module);
        searchLookupItem.Url = uriBuilder.Path + $"?query={query}&am={activeModule}&st={searchLookupItem.Type}";
        searchLookupItemList.Add(searchLookupItem);
      }
      SearchService.SearchLookupItem searchLookupItem5 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllEntities, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in All Entities"), (object) query), module);
      searchLookupItem5.Url = uriBuilder.Path + $"?query={query}&&st={searchLookupItem5.Type}";
      searchLookupItemList.Add(searchLookupItem5);
      SearchService.SearchLookupItem searchLookupItem6 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.AllHelp, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Help"), (object) query));
      searchLookupItem6.Url = uriBuilder.Path + $"?query={query}&st={searchLookupItem6.Type}";
      searchLookupItemList.Add(searchLookupItem6);
      SearchService.SearchLookupItem searchLookupItem7 = new SearchService.SearchLookupItem(SearchService.SearchLookupType.Files, string.Format(PX.SM.Messages.GetLocal("Search for \"{0}\" in Files"), (object) query));
      searchLookupItem7.Url = uriBuilder.Path + $"?query={query}&st={searchLookupItem7.Type}";
      searchLookupItemList.Add(searchLookupItem7);
    }
    searchLookupItemList.AddRange((IEnumerable<SearchService.SearchLookupItem>) SearchService.SearchInSitemap(query, 10, 5));
    return (IList<SearchService.SearchLookupItem>) searchLookupItemList;
  }

  public static IList<SearchService.SearchLookupItem> SearchInSitemap(
    string query,
    int topScreens,
    int topReports)
  {
    string str = query.Replace(".", "");
    int capacity = topScreens + topReports;
    List<PXSiteMapNode> pxSiteMapNodeList1 = new List<PXSiteMapNode>(capacity);
    List<PXSiteMapNode> pxSiteMapNodeList2 = new List<PXSiteMapNode>(capacity);
    List<PXSiteMapNode> collection1 = new List<PXSiteMapNode>(capacity);
    List<PXSiteMapNode> collection2 = new List<PXSiteMapNode>(capacity);
    foreach (PXSiteMapNode node in PXSiteMap.Provider.Definitions.Nodes)
    {
      if (!node.Url.ToLowerInvariant().Contains("wiki") && !string.IsNullOrWhiteSpace(node.Url) && !node.Url.ToLowerInvariant().Contains("default.aspx") && !PXList.Provider.HasList(node.ScreenID) && (!PXSiteMap.Provider.IsInHidden(node) || PXList.Provider.IsList(node.ScreenID)))
      {
        if (node.Title.ToLowerInvariant().StartsWith(query.ToLowerInvariant()) || !string.IsNullOrWhiteSpace(node.ScreenID) && node.ScreenID.ToLowerInvariant().StartsWith(str.ToLowerInvariant()))
        {
          if (node.Url.ToLowerInvariant().Contains("reportlauncher.aspx"))
          {
            if (pxSiteMapNodeList2.Count < capacity && node.IsAccessibleToUser())
              pxSiteMapNodeList2.Add(node);
          }
          else if (pxSiteMapNodeList1.Count < capacity && node.IsAccessibleToUser())
            pxSiteMapNodeList1.Add(node);
        }
        else if (node.Title.ToLowerInvariant().Contains($" {query}".ToLowerInvariant()))
        {
          if (node.Url.ToLowerInvariant().Contains("reportlauncher.aspx"))
          {
            if (pxSiteMapNodeList2.Count + collection2.Count < capacity && node.IsAccessibleToUser())
              collection2.Add(node);
          }
          else if (pxSiteMapNodeList1.Count + collection1.Count < capacity && node.IsAccessibleToUser())
            collection1.Add(node);
        }
        if (pxSiteMapNodeList1.Count == topScreens)
        {
          if (pxSiteMapNodeList2.Count == topReports)
            break;
        }
      }
    }
    if (pxSiteMapNodeList1.Count < topScreens)
      pxSiteMapNodeList1.AddRange((IEnumerable<PXSiteMapNode>) collection1);
    if (pxSiteMapNodeList2.Count < topReports)
      pxSiteMapNodeList2.AddRange((IEnumerable<PXSiteMapNode>) collection2);
    int num1 = System.Math.Min(topScreens, pxSiteMapNodeList1.Count);
    int num2 = System.Math.Min(topReports, pxSiteMapNodeList2.Count);
    if (num1 < topScreens)
      num2 = System.Math.Min(topReports + (topScreens - num1), pxSiteMapNodeList2.Count);
    if (num2 < topReports)
      num1 = System.Math.Min(topScreens + (topReports - num2), pxSiteMapNodeList1.Count);
    List<SearchService.SearchLookupItem> searchLookupItemList = new List<SearchService.SearchLookupItem>();
    for (int index = 0; index < num1; ++index)
    {
      string fullpath = pxSiteMapNodeList1[index].Title;
      PXSiteMapNode pxSiteMapNode = pxSiteMapNodeList1[index];
      string entryScreenId = PXList.Provider.GetEntryScreenID(pxSiteMapNode.ScreenID);
      if (entryScreenId != null)
      {
        PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(entryScreenId);
        if (screenIdUnsecure != null && !PXSiteMap.Provider.IsInHidden(screenIdUnsecure))
          pxSiteMapNode = screenIdUnsecure;
      }
      for (; pxSiteMapNode.ParentNode != null && pxSiteMapNode.ParentNode.Key != pxSiteMapNode.Provider.RootNode.Key; pxSiteMapNode = pxSiteMapNode.ParentNode)
        fullpath = $"{pxSiteMapNode.ParentNode.Title} > {fullpath}";
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.Screen, pxSiteMapNodeList1[index].Title)
      {
        Url = ((Control) HttpContext.Current.Handler).ResolveUrl(pxSiteMapNodeList1[index].Url),
        Path = SearchService.TrimToSecondLevel(fullpath),
        Image = Sprite.Main.GetFullUrl("DataEntry")
      });
    }
    for (int index = 0; index < num2; ++index)
    {
      string fullpath = pxSiteMapNodeList2[index].Title;
      for (PXSiteMapNode parentNode = pxSiteMapNodeList2[index]; parentNode.ParentNode != null && parentNode.ParentNode.Key != parentNode.Provider.RootNode.Key; parentNode = parentNode.ParentNode)
        fullpath = $"{parentNode.ParentNode.Title} > {fullpath}";
      searchLookupItemList.Add(new SearchService.SearchLookupItem(SearchService.SearchLookupType.Report, pxSiteMapNodeList2[index].Title)
      {
        Url = ((Control) HttpContext.Current.Handler).ResolveUrl(pxSiteMapNodeList2[index].Url),
        Path = SearchService.TrimToSecondLevel(fullpath),
        Image = Sprite.Main.GetFullUrl("Report")
      });
    }
    if (searchLookupItemList.Count > 0)
      searchLookupItemList[0].NewGroup = true;
    return (IList<SearchService.SearchLookupItem>) searchLookupItemList;
  }

  public static string Html2PlainText(string html)
  {
    if (string.IsNullOrEmpty(html))
      return html;
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);
    StringWriter outText = new StringWriter();
    SearchService.ConvertTo(htmlDocument.DocumentNode, (TextWriter) outText);
    outText.Flush();
    return outText.ToString();
  }

  private static string TrimToSecondLevel(string fullpath)
  {
    string secondLevel = fullpath;
    int num = fullpath.IndexOf('>');
    if (num > 0)
    {
      int length = fullpath.IndexOf('>', num + 1);
      if (length > 0)
        secondLevel = fullpath.Substring(0, length);
    }
    return secondLevel;
  }

  private static void ConvertContentTo(HtmlNode node, TextWriter outText)
  {
    foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) node.ChildNodes)
      SearchService.ConvertTo(childNode, outText);
  }

  private static void ConvertTo(HtmlNode node, TextWriter outText)
  {
    switch ((int) node.NodeType)
    {
      case 0:
        SearchService.ConvertContentTo(node, outText);
        break;
      case 1:
        string name = node.Name;
        if (name == "p" || name == "br")
          outText.Write("\r\n");
        if (!node.HasChildNodes)
          break;
        SearchService.ConvertContentTo(node, outText);
        break;
      case 3:
        switch (node.ParentNode.Name)
        {
          case "script":
            return;
          case "style":
            return;
          default:
            string text = ((HtmlTextNode) node).Text;
            if (HtmlNode.IsOverlappedClosingElement(text) || text.Trim().Length <= 0)
              return;
            outText.Write(HtmlEntity.DeEntitize(text));
            return;
        }
    }
  }

  /// <exclude />
  [DebuggerDisplay("{Id}: {Name} {Type}")]
  public class SearchLookupItem
  {
    public string Name { get; private set; }

    public string Module { get; private set; }

    public SearchService.SearchLookupType Type { get; private set; }

    public string Image { get; set; }

    public string Path { get; set; }

    public string Url { get; set; }

    public bool NewGroup { get; set; }

    public SearchLookupItem(SearchService.SearchLookupType type, string name)
      : this(type, name, (string) null)
    {
    }

    public SearchLookupItem(SearchService.SearchLookupType type, string name, string module)
    {
      this.Type = type;
      this.Name = name;
      this.Module = module;
    }
  }

  public enum SearchLookupType
  {
    AllEntities,
    ActiveModule,
    AllHelp,
    ActiveWiki,
    Screen,
    Files,
    Report,
  }
}
