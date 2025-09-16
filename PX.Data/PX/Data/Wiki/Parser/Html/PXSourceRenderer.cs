// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXSourceRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

internal class PXSourceRenderer : PXHtmlRenderer
{
  private string GetStyleOfSyntax(SourceElement.SyntaxType s)
  {
    switch (s)
    {
      case SourceElement.SyntaxType.Bracket:
        return "br";
      case SourceElement.SyntaxType.Comment:
        return "cmt";
      case SourceElement.SyntaxType.StringLiteral:
        return "sl";
      case SourceElement.SyntaxType.Number:
        return "nu";
      case SourceElement.SyntaxType.Keyword:
        return "kw";
      default:
        return "";
    }
  }

  public static string EscapeHtml(string input)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (input == null || input.Length == 0)
      return input;
    input = input.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
    int startIndex1 = 0;
    while (true)
    {
      string str = input;
      char[] anyOf = new char[3]{ '<', '>', '&' };
      int startIndex2 = startIndex1;
      int index;
      if ((index = str.IndexOfAny(anyOf, startIndex2)) != -1)
      {
        char ch = input[index];
        stringBuilder.Append(input, startIndex1, index - startIndex1);
        switch (ch)
        {
          case '&':
            stringBuilder.Append("&amp;");
            break;
          case '<':
            stringBuilder.Append("&lt;");
            break;
          case '>':
            stringBuilder.Append("&gt;");
            break;
        }
        startIndex1 = index + 1;
      }
      else
        break;
    }
    stringBuilder.Append(input, startIndex1, input.Length - startIndex1);
    return stringBuilder.ToString();
  }

  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    SourceElement e = (SourceElement) elem;
    SourceElement.DiffState diffState = SourceElement.DiffState.NoChange;
    resultHtml.Append($"<pre {this.GetWikiClass("source-highlighted", settings)}{this.GetWikiTag(e, settings)}>");
    foreach (SourceElement.SourcePart sourcePart in e.Source)
    {
      if (!string.IsNullOrEmpty(sourcePart.Value))
      {
        if (sourcePart.DiffState != diffState)
        {
          if (diffState != SourceElement.DiffState.NoChange)
            resultHtml.Append("</span>");
          diffState = sourcePart.DiffState;
          if (diffState == SourceElement.DiffState.Added)
            resultHtml.Append($"<span {this.GetWikiClass("add", settings)}>");
          else if (diffState == SourceElement.DiffState.Removed)
            resultHtml.Append($"<span {this.GetWikiClass("del", settings)}>");
        }
        string styleOfSyntax = this.GetStyleOfSyntax(sourcePart.Syntax);
        if (styleOfSyntax != "" || sourcePart.TextStyle != (SourceElement.TextStyle) 0)
        {
          resultHtml.Append("<span");
          if (styleOfSyntax != "")
            resultHtml.Append(" " + this.GetWikiClass(styleOfSyntax, settings));
          if (sourcePart.TextStyle != (SourceElement.TextStyle) 0)
            resultHtml.Append(" " + this.GetFontStyle(sourcePart.TextStyle));
          resultHtml.Append(">");
        }
        resultHtml.Append(PXSourceRenderer.EscapeHtml(sourcePart.Value));
        if (styleOfSyntax != "" || sourcePart.TextStyle != (SourceElement.TextStyle) 0)
          resultHtml.Append("</span>");
      }
    }
    if (diffState != SourceElement.DiffState.NoChange)
      resultHtml.Append("</span>");
    resultHtml.Append("</pre>");
  }

  private string GetFontStyle(SourceElement.TextStyle style)
  {
    StringBuilder stringBuilder = new StringBuilder("style=\"");
    if (style.HasFlag((Enum) SourceElement.TextStyle.Bold))
      stringBuilder.Append("font-weight:bold;");
    if (style.HasFlag((Enum) SourceElement.TextStyle.Italic))
      stringBuilder.Append("font-style:italic;");
    return stringBuilder.Append("\"").ToString();
  }

  private string GetWikiTag(SourceElement e, PXWikiParserContext settings)
  {
    return settings.IsDesignMode ? " wikitag=\"csharp\"" : "";
  }
}
