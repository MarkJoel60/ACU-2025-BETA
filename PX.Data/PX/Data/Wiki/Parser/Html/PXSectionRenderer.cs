// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXSectionElement HTML rendering.
/// </summary>
internal class PXSectionRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXSectionElement e = (PXSectionElement) elem;
    if (e.parent != null)
      e.Header.parent = e.parent is PXSectionElement parent ? (PXElement) parent.Header : (PXElement) null;
    this.DoRender((PXElement) e.Header, resultHtml, settings);
    if (e.Header.IsCollapsable)
    {
      if (e.Header.IsDefaultExpanded)
        resultHtml.Append($"<div CollapseRange=\"Sec{e.Header.SectionID.ToString()}\">");
      else
        resultHtml.Append($"<div CollapseRange=\"Sec{e.Header.SectionID.ToString()}\" class='folded'>");
    }
    this.RenderChildren(e, resultHtml, settings);
    if (!e.Header.IsCollapsable)
      return;
    resultHtml.Append($"{Environment.NewLine}</div>{Environment.NewLine}");
  }

  private void RenderChildren(
    PXSectionElement e,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    if (!settings.IsSimpleRender)
    {
      resultHtml.Append($"{Environment.NewLine}<div id=\"Sec{e.Header.SectionID.ToString()}\" ");
      if (e.IsCollapsed)
        resultHtml.Append("style=\"display: none\"");
      string wkclass = e.Header.IsNestedTopics ? "section chapter s" + e.Header.Name : "section s" + e.Header.Name;
      resultHtml.Append(this.GetWikiClass(wkclass, settings));
      resultHtml.Append(">");
    }
    resultHtml.Append(Environment.NewLine);
    foreach (PXElement child in e.Children)
      this.DoRender(child, resultHtml, settings);
    if (settings.IsSimpleRender)
      return;
    resultHtml.Append($"{Environment.NewLine}</div>{Environment.NewLine}");
  }
}
