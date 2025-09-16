// Decompiled with JetBrains decompiler
// Type: PX.SM.Wiki
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.SM;

/// <exclude />
public static class Wiki
{
  public const string WikiPageUrl = "~/Wiki/Show.aspx";
  public const string WikiSearchPortalUrl = "~/Search/WikiSP.aspx";
  public const string WikiSearchUrl = "~/Search/Wiki.aspx";
  public const string WikiComparisonUrl = "~/Wiki/Comparison.aspx";
  public const string WikiPageIDParName = "pageID";
  public const string WikiPageParName = "art";
  public const string WikiParName = "wiki";
  public const string WikiSearchParName = "WikiID";
  public const string WikiRevision = "PageRevisionID";
  public static readonly Regex RegexQueryString = new Regex("(\\?|\\&)(?<Name>.+?)=(?<Value>[^&]+)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

  public static string WikiUrl(string wiki)
  {
    if (!string.IsNullOrEmpty(wiki))
      wiki = wiki.Replace(' ', '+');
    return "~/Wiki/Show.aspx?WikiID=" + wiki;
  }

  public static string Url(string wiki, string article)
  {
    if (!string.IsNullOrEmpty(article))
      article = article.Replace(' ', '+');
    return $"~/Wiki/Show.aspx?wiki={wiki}&art={article}";
  }

  public static string Link(string wiki, string article) => $"{wiki}\\{article}";

  public static string Url(Guid? pageID) => Wiki.Url("~/Wiki/Show.aspx", pageID);

  public static string Url(Guid? pageID, int revision)
  {
    return $"{Wiki.Url("~/Wiki/Show.aspx", pageID)}&{"PageRevisionID"}={revision}";
  }

  public static string Url(string urlPage, Guid? pageID) => $"{urlPage}?{nameof (pageID)}={pageID}";

  public static string Url(Guid pageID, Guid wikiID)
  {
    return !(wikiID == Guid.Empty) ? Wiki.Url(new Guid?(pageID)) : Wiki.SearchUrl(new Guid?(pageID));
  }

  public static string SearchUrl(Guid? pageID) => $"{"~/Search/Wiki.aspx"}?{"WikiID"}={pageID}";

  public static string SearchPortalUrl(Guid? pageID)
  {
    return $"{"~/Search/WikiSP.aspx"}?{"WikiID"}={pageID}";
  }

  public static string GetSiteMapIcon(int wikiArticleType)
  {
    switch (wikiArticleType)
    {
      case 10:
        return "main@Help|main@Help";
      case 11:
        return "main@Rss|main@Rss";
      case 12:
        return "main@Templates|Main@Templates";
      case 13:
        return "main@WebSite|main@WebSite";
      default:
        return (string) null;
    }
  }

  public static System.Type GraphType(int? artType)
  {
    if (artType.HasValue)
    {
      switch (artType.GetValueOrDefault())
      {
        case 0:
          return typeof (WikiMaintenance);
        case 10:
          return typeof (KBArticleMaint);
        case 11:
          return typeof (WikiAnnouncementMaintenance);
        case 12:
          return typeof (WikiNotificationTemplateMaintenance);
        case 13:
          return typeof (WikiSitePageMaintenance);
      }
    }
    return (System.Type) null;
  }

  internal static System.Type PageType(int? artType)
  {
    if (artType.HasValue)
    {
      switch (artType.GetValueOrDefault())
      {
        case 10:
          return typeof (WikiArticle);
        case 11:
          return typeof (WikiAnnouncement);
        case 12:
          return typeof (WikiNotificationTemplate);
        case 13:
          return typeof (WikiSitePage);
      }
    }
    return (System.Type) null;
  }

  internal static PXCacheRights Convert(PXWikiRights rights)
  {
    switch (rights)
    {
      case PXWikiRights.Denied:
        return PXCacheRights.Denied;
      case PXWikiRights.Select:
        return PXCacheRights.Select;
      case PXWikiRights.Update:
      case PXWikiRights.Published:
        return PXCacheRights.Update;
      case PXWikiRights.Insert:
        return PXCacheRights.Insert;
      case PXWikiRights.Delete:
        return PXCacheRights.Delete;
      default:
        return PXCacheRights.Select;
    }
  }

  internal static PXWikiRights Convert(PXCacheRights rights)
  {
    switch (rights)
    {
      case PXCacheRights.Denied:
        return PXWikiRights.Denied;
      case PXCacheRights.Select:
        return PXWikiRights.Select;
      case PXCacheRights.Update:
        return PXWikiRights.Update;
      case PXCacheRights.Insert:
        return PXWikiRights.Insert;
      case PXCacheRights.Delete:
        return PXWikiRights.Delete;
      default:
        return PXWikiRights.Select;
    }
  }

  public static WikiCss GetWikiCss(PXGraph graph, string wikiName)
  {
    return (WikiCss) PXSelectBase<WikiCss, PXSelectJoin<WikiCss, InnerJoin<WikiDescriptor, On<WikiCss.cssID, Equal<WikiDescriptor.cssID>>, InnerJoin<WikiPage, On<WikiDescriptor.pageID, Equal<WikiPage.pageID>>>>, Where<WikiPage.name, Equal<Required<WikiPage.name>>>>.Config>.SelectWindowed(graph, 0, 1, (object) wikiName);
  }

  public static WikiCss GetWikiCss(PXGraph graph, Guid wikiID)
  {
    return (WikiCss) PXSelectBase<WikiCss, PXSelectJoin<WikiCss, InnerJoin<WikiDescriptor, On<WikiCss.cssID, Equal<WikiDescriptor.cssID>>>, Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>>.Config>.SelectWindowed(graph, 0, 1, (object) wikiID);
  }

  public static WikiCss GetCss(PXGraph graph, Guid styleID)
  {
    return (WikiCss) PXSelectBase<WikiCss, PXSelect<WikiCss, Where<WikiCss.cssID, Equal<Required<WikiCss.cssID>>>>.Config>.SelectWindowed(graph, 0, 1, (object) styleID);
  }

  public static SiteMap GetSiteMapRecord(PXGraph graph, Guid? pageID)
  {
    return (SiteMap) PXSelectBase<SiteMap, PXSelect<SiteMap, Where<SiteMap.nodeID, Equal<Required<SiteMap.nodeID>>>>.Config>.SelectWindowed(graph, 0, 1, (object) pageID);
  }

  [PXInternalUseOnly]
  public static void BlockIfOnlineHelpIsOn()
  {
    if (SitePolicy.IsOnlineHelpOn() && !SitePolicy.IsHelpPortal())
      throw new PXSetupNotEnteredException<PreferencesGeneral>("The form is not available because the Use Online Help System check box is selected on the Site Preferences (SM200505) form.", Array.Empty<object>());
  }
}
