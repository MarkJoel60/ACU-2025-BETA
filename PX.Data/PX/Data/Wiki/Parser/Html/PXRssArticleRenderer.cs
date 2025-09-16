// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXRssArticleRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXRssArticleLink HTML rendering.
/// </summary>
internal class PXRssArticleRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXRssArticleLink pxRssArticleLink = (PXRssArticleLink) elem;
    resultHtml.Append("<a href=\"");
    resultHtml.Append(settings.Settings.GetRSSUrl);
    resultHtml.Append("?type=article&pageId=");
    resultHtml.Append((object) pxRssArticleLink.PageId);
    resultHtml.Append("&title=");
    resultHtml.Append(pxRssArticleLink.Description);
    if (!string.IsNullOrEmpty(pxRssArticleLink.Language))
    {
      resultHtml.Append("&lang=");
      resultHtml.Append(pxRssArticleLink.Language);
    }
    resultHtml.Append("\">");
    resultHtml.Append(pxRssArticleLink.Title);
    resultHtml.AppendLine("</a>");
  }
}
