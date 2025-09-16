// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXRssRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXRssLink HTML rendering.</summary>
internal class PXRssRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXRssLink pxRssLink = (PXRssLink) elem;
    StringBuilder stringBuilder = new StringBuilder(settings.Settings.GetRSSUrl);
    stringBuilder.Append("?type=annoucements&wiki=");
    stringBuilder.Append((object) pxRssLink.WikiId);
    stringBuilder.Append("&folder=");
    stringBuilder.Append((object) pxRssLink.FolderId);
    stringBuilder.Append("&maxnews=");
    stringBuilder.Append(pxRssLink.MaxNewsCount);
    stringBuilder.Append("&category=");
    stringBuilder.Append((object) pxRssLink.CategoryId);
    stringBuilder.Append("&title=");
    stringBuilder.Append(pxRssLink.Title);
    stringBuilder.Append("&description=");
    stringBuilder.Append(pxRssLink.Description);
    resultHtml.Append("<a href=\"");
    resultHtml.Append(stringBuilder.ToString());
    resultHtml.Append("\">");
    resultHtml.Append("<img src=\"");
    resultHtml.Append(settings.Settings.RSSImageUrl);
    resultHtml.Append("\" />");
    resultHtml.AppendLine("</a>");
    if (HttpContext.Current == null || HttpContext.Current.CurrentHandler == null)
      return;
    ((Page) HttpContext.Current.CurrentHandler).Header.Controls.Add((Control) new WebControl(HtmlTextWriterTag.Link)
    {
      Attributes = {
        ["href"] = stringBuilder.ToString(),
        ["type"] = "application/rss+xml",
        ["rel"] = "alternate",
        ["title"] = pxRssLink.Title
      }
    });
  }
}
