// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXHtmlTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXHtmlTagElement HTML rendering.
/// </summary>
internal class PXHtmlTagRenderer : PXHtmlRenderer
{
  private static List<string> selfCloseTags = new List<string>((IEnumerable<string>) new string[9]
  {
    "area",
    "base",
    "basefont",
    "br",
    "hr",
    "input",
    "img",
    "link",
    "meta"
  });

  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXHtmlTagElement pxHtmlTagElement = (PXHtmlTagElement) elem;
    resultHtml.Append("<" + pxHtmlTagElement.TagName);
    foreach (PXHtmlAttribute attribute in pxHtmlTagElement.Attributes)
    {
      resultHtml.Append(" ");
      resultHtml.Append(attribute.name);
      resultHtml.Append("=\"");
      resultHtml.Append(attribute.value);
      resultHtml.Append("\"");
    }
    if (pxHtmlTagElement.TagValue == null || pxHtmlTagElement.TagValue.Count == 0)
    {
      if (PXHtmlTagRenderer.selfCloseTags.Contains(pxHtmlTagElement.TagName.ToLower()))
        resultHtml.Append(" />");
      else
        resultHtml.Append($"></{pxHtmlTagElement.TagName}>");
    }
    else
    {
      resultHtml.Append(">");
      foreach (PXElement el in pxHtmlTagElement.TagValue)
        this.DoRender(el, resultHtml, settings);
      resultHtml.Append($"</{pxHtmlTagElement.TagName}>");
    }
  }
}
