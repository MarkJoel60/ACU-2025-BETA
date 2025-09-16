// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXStyledTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXStyledTextElement HTML rendering.
/// </summary>
internal class PXStyledTextRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXStyledTextElement styledTextElement = (PXStyledTextElement) elem;
    resultHtml.Append(this.GetOpenTag(styledTextElement.Style));
    foreach (PXElement child in styledTextElement.Children)
      this.DoRender(child, resultHtml, settings);
    resultHtml.Append(this.GetCloseTag(styledTextElement.Style));
  }

  private string GetOpenTag(TextStyle style)
  {
    switch (style)
    {
      case TextStyle.Bold:
        return "<b>";
      case TextStyle.Italic:
        return "<i>";
      case TextStyle.Bold | TextStyle.Italic:
        return "<b><i>";
      case TextStyle.Underlined:
        return "<u>";
      case TextStyle.Striked:
        return "<strike>";
      case TextStyle.Monotype:
        return "<code>";
      default:
        return "";
    }
  }

  private string GetCloseTag(TextStyle style)
  {
    switch (style)
    {
      case TextStyle.Bold:
        return "</b>";
      case TextStyle.Italic:
        return "</i>";
      case TextStyle.Bold | TextStyle.Italic:
        return "</i></b>";
      case TextStyle.Underlined:
        return "</u>";
      case TextStyle.Striked:
        return "</strike>";
      case TextStyle.Monotype:
        return "</code>";
      default:
        return "";
    }
  }
}
