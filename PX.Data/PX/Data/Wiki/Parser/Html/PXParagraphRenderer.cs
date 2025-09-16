// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXParagraphRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

internal class PXParagraphRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXParagraphElement e = (PXParagraphElement) elem;
    PXElement[] children = e.Children;
    if (children.Length == 0)
      return;
    resultHtml.Append(Environment.NewLine);
    resultHtml.Append(this.PTagOpen(e, settings));
    resultHtml.Append(Environment.NewLine);
    foreach (PXElement el in children)
      this.DoRender(el, resultHtml, settings);
    resultHtml.Append(Environment.NewLine);
    resultHtml.Append(settings.IsSimpleRender ? "" : "</p>");
  }

  private string PTagOpen(PXParagraphElement e, PXWikiParserContext settings)
  {
    if (settings.IsDesignMode && e.HasNewLineInDesidnMode)
      return "<p newline=\"br\">";
    return !settings.IsSimpleRender ? "<p>" : "";
  }
}
