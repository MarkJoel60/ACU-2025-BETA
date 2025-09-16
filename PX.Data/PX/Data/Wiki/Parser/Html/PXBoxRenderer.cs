// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for rendering PXBoxElement to HTML.
/// </summary>
internal class PXBoxRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXBoxElement e = (PXBoxElement) elem;
    resultHtml.Append(Environment.NewLine);
    resultHtml.Append("<div ");
    resultHtml.Append(PXHtmlFormatter.GetWikiTag((PXElement) e));
    if (e.IsHintBox)
      resultHtml.Append(this.GetWikiClass("GrayBox HintBox", settings));
    else if (e.IsWarnBox)
      resultHtml.Append(this.GetWikiClass("GrayBox WarnBox", settings));
    else if (e.IsDangerBox)
      resultHtml.Append(this.GetWikiClass("GrayBox DangerBox", settings));
    else if (e.IsGoodPracticeBox)
      resultHtml.Append(this.GetWikiClass("GrayBox GoodPracticeBox", settings));
    resultHtml.AppendLine(">");
    if (e.IsHintBox || e.IsWarnBox || e.IsDangerBox || e.IsGoodPracticeBox)
    {
      resultHtml.Append("<table class=\"GrayBoxContent\">");
      resultHtml.AppendLine("<tr>");
      if (e.IsHintBox)
        resultHtml.AppendLine("<td class=\"hintcell\"><i class=\"ac ac-info\" /></td>");
      else if (e.IsWarnBox)
        resultHtml.AppendLine("<td class=\"warncell\"><div class=\"sprite-icon text-icon\"><div class=\"text-icon-img text-BoxWarn\"></div></div></td>");
      else if (e.IsDangerBox)
        resultHtml.AppendLine("<td class=\"dangercell\"><div class=\"sprite-icon text-icon\"><div class=\"text-icon-img text-BoxWarn\"></div></div></td>");
      else if (e.IsGoodPracticeBox)
        resultHtml.AppendLine("<td class=\"goodpracticecell\"><i class=\"ac ac-check_circle\" /></td>");
      resultHtml.AppendLine("<td class=\"boxcontent\">");
    }
    foreach (PXElement child in e.Children)
      this.DoRender(child, resultHtml, settings);
    if (e.IsHintBox || e.IsWarnBox || e.IsDangerBox || e.IsGoodPracticeBox)
      resultHtml.AppendLine("</td></tr></table>");
    resultHtml.AppendLine("</div>");
  }
}
