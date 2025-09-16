// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXNewslineDeclaration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents newsline declaration.</summary>
internal class PXNewslineDeclaration(PXWikiParserContext context) : PXTemplateDeclaration("Newsline", context)
{
  private PXTemplateDeclaration template;
  private Guid? wiki;
  private Guid? folder;
  private int maxNewsCount;
  private bool hasRssLink;
  private string rssTitle;
  private string rssDescription;
  private string dateFormat;
  private bool paging;
  private int? maxPagerItems;
  public static string DefaultWiki = "Announcements";

  protected override void InitParamDeclarations()
  {
  }

  public override string GetContent(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("template") || !(this.context is PXDBContext))
      return (string) null;
    StringBuilder result1 = new StringBuilder();
    PXDBContext context = this.context as PXDBContext;
    Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    int total = 0;
    int result2 = 0;
    this.template = new PXTemplateDeclaration(pars.ContainsKey("template") ? pars["template"] : "", this.context);
    this.wiki = this.GetWiki(pars);
    this.folder = this.GetFolder(pars);
    this.maxNewsCount = this.GetMaxNewsCount(pars);
    this.hasRssLink = this.GetHasRSS(pars);
    this.rssTitle = pars.ContainsKey("rsstitle") ? pars["rsstitle"] : "";
    this.rssDescription = pars.ContainsKey("rssdescription") ? pars["rssdescription"] : "";
    this.dateFormat = pars.ContainsKey("dateformat") ? pars["dateformat"] : "";
    this.paging = this.GetHasPaging(pars);
    this.maxPagerItems = this.GetMaxPagerItems(pars);
    this.CreateRSSLink(result1);
    if (this.paging && HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request["nlpage"]))
      int.TryParse(HttpContext.Current.Request["nlpage"], out result2);
    foreach (WikiAnnouncement readAnnouncement in this.ReadAnnouncements(context))
    {
      if ((!this.folder.HasValue || !(readAnnouncement.ParentUID.GetValueOrDefault() != this.folder.Value)) && readAnnouncement.StartDate.HasValue)
      {
        if (this.maxNewsCount > 0 && (total < result2 * this.maxNewsCount || total >= this.maxNewsCount * (result2 + 1)))
        {
          ++total;
        }
        else
        {
          this.CollectProps(dictionary, readAnnouncement, context);
          result1.AppendLine(this.template.GetContent(dictionary));
          ++total;
        }
      }
    }
    this.CreatePaging(result1, result2, total);
    return result1.ToString();
  }

  private Guid? GetWiki(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("wiki"))
      pars["wiki"] = PXNewslineDeclaration.DefaultWiki;
    return PXSiteMap.WikiProvider.FindWiki(pars["wiki"])?.NodeID;
  }

  private Guid? GetFolder(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("folder"))
      return new Guid?();
    return PXSiteMap.WikiProvider.FindSiteMapNode(pars["wiki"], pars["folder"])?.NodeID;
  }

  private int GetMaxNewsCount(Dictionary<string, string> pars)
  {
    int result = 0;
    if (!pars.ContainsKey("maxnewscount"))
      return result;
    int.TryParse(pars["maxnewscount"], out result);
    return result;
  }

  private bool GetHasRSS(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("hasrsslink"))
      return false;
    string lower = pars["hasrsslink"].ToLower();
    return !(lower == "false") && !(lower == "0");
  }

  private void CreateRSSLink(StringBuilder result)
  {
    if (!this.hasRssLink)
      return;
    result.AppendFormat("[RSS:{0}|{1}|{2}|{3}|{4}]", (object) this.wiki, (object) this.folder, (object) (this.paging ? 0 : this.maxNewsCount), (object) this.rssTitle, (object) this.rssDescription);
    result.AppendLine("{br}");
  }

  private bool GetHasPaging(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("paging"))
      return false;
    string lower = pars["paging"].ToLower();
    return !(lower == "false") && !(lower == "0");
  }

  private int? GetMaxPagerItems(Dictionary<string, string> pars)
  {
    if (!pars.ContainsKey("maxpageritems"))
      return new int?();
    int result;
    return int.TryParse(pars["maxpageritems"], out result) ? new int?(result) : new int?();
  }

  private List<WikiAnnouncement> ReadAnnouncements(PXDBContext context)
  {
    List<WikiAnnouncement> wikiAnnouncementList = new List<WikiAnnouncement>();
    if (context.AnnouncementReader == null)
      return wikiAnnouncementList;
    if (this.wiki.HasValue)
      context.AnnouncementReader.Filter.Current.WikiID = this.wiki;
    foreach (PXResult<WikiAnnouncement> pxResult in context.AnnouncementReader.Announcements.Select())
    {
      WikiAnnouncement wikiAnnouncement = (WikiAnnouncement) pxResult;
      wikiAnnouncementList.Add(wikiAnnouncement);
    }
    wikiAnnouncementList.Sort(new Comparison<WikiAnnouncement>(this.CompareAnnouncements));
    return wikiAnnouncementList;
  }

  private int CompareAnnouncements(WikiAnnouncement a1, WikiAnnouncement a2)
  {
    if (a1 == null && a2 == null)
      return 0;
    if (a1 == null)
      return 1;
    if (a2 == null)
      return -1;
    System.DateTime? startDate1 = a1.StartDate;
    System.DateTime? startDate2 = a2.StartDate;
    if ((startDate1.HasValue == startDate2.HasValue ? (startDate1.HasValue ? (startDate1.GetValueOrDefault() == startDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      return 0;
    System.DateTime? startDate3 = a1.StartDate;
    if (!startDate3.HasValue)
      return 1;
    startDate3 = a2.StartDate;
    if (!startDate3.HasValue)
      return -1;
    startDate3 = a1.StartDate;
    System.DateTime? startDate4 = a2.StartDate;
    if ((startDate3.HasValue & startDate4.HasValue ? (startDate3.GetValueOrDefault() < startDate4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      return 1;
    System.DateTime? startDate5 = a1.StartDate;
    startDate3 = a2.StartDate;
    return (startDate5.HasValue & startDate3.HasValue ? (startDate5.GetValueOrDefault() > startDate3.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? -1 : 0;
  }

  private void CollectProps(
    Dictionary<string, string> props,
    WikiAnnouncement ann,
    PXDBContext context)
  {
    props.Clear();
    foreach (System.Type bqlField in context.AnnouncementReader.Announcements.Cache.BqlFields)
    {
      if (context.AnnouncementReader.Announcements.Cache.GetStateExt((object) ann, bqlField.Name) is PXFieldState stateExt)
      {
        foreach (PXEventSubscriberAttribute attribute in context.AnnouncementReader.Announcements.Cache.GetAttributes(bqlField.Name))
        {
          if (attribute is PXUIFieldAttribute && (((PXUIFieldAttribute) attribute).Visibility & PXUIVisibility.Visible) == PXUIVisibility.Visible && stateExt.Value != null)
          {
            if (stateExt.DataType == typeof (System.DateTime) && !string.IsNullOrEmpty(this.dateFormat))
              props[bqlField.Name] = ((System.DateTime) stateExt.Value).ToString(this.dateFormat);
            else
              props[bqlField.Name] = stateExt.Value.ToString();
          }
        }
      }
    }
    props.Add("rss", $"GetRss.ashx?type=annoucements&wiki={this.wiki}&folder={this.folder}&maxnews={this.maxNewsCount}");
  }

  private void CreatePaging(StringBuilder result, int currpage, int total)
  {
    if (!this.paging || this.maxNewsCount == 0)
      return;
    int num1 = total % this.maxNewsCount == 0 ? total / this.maxNewsCount : total / this.maxNewsCount + 1;
    int num2 = !this.maxPagerItems.HasValue ? num1 : this.maxPagerItems.Value;
    int num3 = num2 == 0 ? 0 : currpage / num2;
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    if (num1 < 2)
      return;
    result.AppendLine("{br}");
    if (this.maxPagerItems.HasValue)
    {
      if (currpage == 0)
      {
        result.Append("&lt;&lt; &lt; ");
      }
      else
      {
        this.AppendPageLink(result, 0, "&lt;&lt;");
        this.AppendPageLink(result, currpage - 1, "&lt;");
      }
    }
    if (num2 > 0)
    {
      for (int num4 = num3 * num2; num4 < (num3 + 1) * num2 && num4 < num1; ++num4)
      {
        if (num4 == currpage)
        {
          result.Append(num4 + 1);
          result.Append(" ");
        }
        else
          this.AppendPageLink(result, num4, Convert.ToString(num4 + 1));
      }
    }
    if (this.maxPagerItems.HasValue)
    {
      if (currpage == num1 - 1)
      {
        result.Append("&gt; &gt;&gt;");
      }
      else
      {
        this.AppendPageLink(result, currpage + 1, "&gt;");
        this.AppendPageLink(result, num1 - 1, "&gt;&gt;");
      }
    }
    result.AppendLine("{br}");
  }

  private void AppendPageLink(StringBuilder str, int num, string caption)
  {
    str.Append("<a href=\"");
    str.Append(this.AddQueryToSiteUrl("nlpage", num.ToString()));
    str.Append("\" class=\"wikilink pagelink\">");
    str.Append(caption);
    str.Append("</a> ");
  }

  private string AddQueryToSiteUrl(string parName, string parValue)
  {
    if (HttpContext.Current == null || HttpContext.Current.Request == null)
      return "";
    Uri externalUrl = HttpContext.Current.Request.GetExternalUrl();
    bool flag = false;
    List<string> stringList = new List<string>();
    if (HttpContext.Current.Request.QueryString.Count > 0)
    {
      foreach (string allKey in HttpContext.Current.Request.QueryString.AllKeys)
      {
        if (string.Compare(allKey, parName, true) == 0)
        {
          stringList.Add($"{allKey}={parValue}");
          flag = true;
        }
        else
          stringList.Add($"{allKey}={HttpContext.Current.Request.QueryString[allKey]}");
      }
    }
    if (!flag)
      stringList.Add($"{parName}={parValue}");
    return $"{externalUrl.GetLeftPart(UriPartial.Path)}?{string.Join("&", stringList.ToArray())}";
  }
}
