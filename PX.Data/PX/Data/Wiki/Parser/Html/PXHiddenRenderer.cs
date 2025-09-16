// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXHiddenRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXHiddenElement HTML rendering.
/// </summary>
public class PXHiddenRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXHiddenElement e = (PXHiddenElement) elem;
    if (settings.IsSimpleRender)
      return;
    if (settings.IsDesignMode)
    {
      resultHtml.Append("<span wikitag=\"hiddentext\">");
      this.Render(e, resultHtml, settings, "", "");
      resultHtml.Append("</span>");
    }
    else if (settings.EnableScript)
      this.Render(e, resultHtml, settings, "<img src=\"\" style=\"margin-right: 5px;\" />", " style=\"display: none;\"");
    else
      this.Render(e, resultHtml, settings, "", "");
  }

  private void Render(
    PXHiddenElement e,
    StringBuilder bld,
    PXWikiParserContext settings,
    string img,
    string contentStyle)
  {
    bld.Append(Environment.NewLine);
    bld.Append("<div>" + Environment.NewLine);
    bld.Append($"<span id=\"hiddenText{settings.IDSuffix}\" name=\"hiddenText{settings.IDSuffix}\">");
    bld.Append(img);
    bld.Append($"<a href=\"javascript:void 0;\" {this.GetWikiClass("wikilink", settings)}>");
    foreach (PXElement el in e.Caption)
      this.DoRender(el, bld, settings);
    bld.Append("</a>");
    bld.Append("</span>");
    bld.Append(Environment.NewLine);
    bld.Append($"<div{contentStyle} class=\"wikihiddencontent\">");
    bld.Append(Environment.NewLine);
    foreach (PXElement child in e.Children)
      this.DoRender(child, bld, settings);
    bld.Append(Environment.NewLine);
    bld.Append("</div>");
    bld.Append(Environment.NewLine + "</div>");
  }
}
