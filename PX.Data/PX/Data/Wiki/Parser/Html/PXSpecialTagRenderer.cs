// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXSpecialTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXSpecialTagElement HTML rendering.
/// </summary>
internal class PXSpecialTagRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXSpecialTagElement e = (PXSpecialTagElement) elem;
    string lower = e.TagValue.ToLower();
    if (lower != null)
    {
      switch (lower.Length)
      {
        case 4:
          switch (lower[1])
          {
            case 'b':
              if (lower == "{br}")
              {
                resultHtml.Append($"<br {PXHtmlFormatter.GetWikiTag((PXElement) e)}/>{Environment.NewLine}");
                return;
              }
              break;
            case 'u':
              if (lower == "{up}")
                return;
              break;
          }
          break;
        case 5:
          if (lower == "{toc}")
          {
            resultHtml.Append(PXHtmlFormatter.GetTOC(settings, this.model));
            return;
          }
          break;
        case 7:
          if (lower == "{qgtoc}")
          {
            resultHtml.Append(PXHtmlFormatter.GetTOC(settings, this.model, true));
            return;
          }
          break;
        case 9:
          if (lower == "{siteurl}")
          {
            resultHtml.Append(PXUrl.SiteUrlWithPath());
            return;
          }
          break;
        case 11:
          switch (lower[1])
          {
            case 'c':
              if (lower == "{copyright}")
              {
                resultHtml.Append(PXVersionInfo.Copyright);
                return;
              }
              break;
            case 'p':
              if (lower == "{pagebreak}")
              {
                resultHtml.Append($"<div {PXHtmlFormatter.GetWikiTag((PXElement) e)}style=\"page-break-after:always;\"></div>");
                return;
              }
              break;
            case 't':
              if (lower == "{themepath}")
              {
                resultHtml.Append(settings.ThemePath);
                return;
              }
              break;
            case 'w':
              if (lower == "{wikititle}")
              {
                resultHtml.Append(settings.WikiTitle);
                return;
              }
              break;
          }
          break;
      }
    }
    resultHtml.Append(e.TagValue);
  }
}
