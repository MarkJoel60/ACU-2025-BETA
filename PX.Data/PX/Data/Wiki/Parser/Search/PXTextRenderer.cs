// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXTextRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXTextElement pxTextElement = (PXTextElement) elem;
    pxTextElement.Value = pxTextElement.Value.Replace("&lt;", "<");
    pxTextElement.Value = pxTextElement.Value.Replace("&gt;", ">");
    pxTextElement.Value = pxTextElement.Value.Replace("&nbsp;", " ");
    pxTextElement.Value = pxTextElement.Value.Replace("&nbsp", " ");
    pxTextElement.Value = pxTextElement.Value.Replace("&#38;", "& ");
    pxTextElement.Value = pxTextElement.Value.Replace("&#8212;", "—");
    string str = settings.IsDesignMode ? this.ReplaceHTMLCodes(pxTextElement.Value) : pxTextElement.Value;
    int num1 = str.IndexOf(Environment.NewLine);
    int num2 = 0;
    int startIndex = 0;
    for (; num1 > -1 && num1 < pxTextElement.Value.Length; num1 = str.IndexOf(Environment.NewLine, startIndex))
    {
      if (num1 != startIndex + Environment.NewLine.Length)
        num2 = 0;
      if (settings.IsDesignMode)
      {
        resultHtml.Append(str.Replace(Environment.NewLine, "<span newline=\"br\"></span>"));
        startIndex = str.Length;
        break;
      }
      resultHtml.Append(str.Substring(startIndex, num1 - startIndex));
      startIndex = num1 + Environment.NewLine.Length;
      if (++num2 % 2 == 0)
        resultHtml.Append("<br />" + Environment.NewLine);
    }
    if (startIndex >= str.Length)
      return;
    resultHtml.Append(str.Substring(startIndex));
  }

  private string ReplaceHTMLCodes(string val)
  {
    return new Regex("&#[0-9a-zA-Z]{2,4};").Replace(val, new MatchEvaluator(this.MatchReplacer));
  }

  private string MatchReplacer(Match m)
  {
    return $"<span wikitext=\"{HttpUtility.HtmlEncode(m.Value)}\">{m.Value}</span>";
  }

  public static void ReplaceNewLines(PXTextElement e, StringBuilder resultHtml)
  {
    int startIndex = 0;
    for (int index = e.Value.IndexOf(Environment.NewLine, startIndex); index == startIndex; index = e.Value.IndexOf(Environment.NewLine, startIndex))
    {
      startIndex = index + Environment.NewLine.Length;
      resultHtml.Append("<br />");
    }
    if (startIndex >= e.Value.Length)
      return;
    e.Value = e.Value.Substring(startIndex);
  }

  public static void ReplaceSiblingNewLines(string val, StringBuilder resultHtml)
  {
    int num = 0;
    int startIndex = 0;
    for (int index1 = val.IndexOf(Environment.NewLine); index1 != -1; index1 = val.IndexOf(Environment.NewLine, startIndex))
    {
      resultHtml.Append(val.Substring(startIndex, index1 - startIndex));
      while (index1 + 2 < val.Length && val.IndexOf(Environment.NewLine, index1 + 2) == index1 + 2)
      {
        index1 += 2;
        ++num;
      }
      if (num % 2 != 0)
        ++num;
      for (int index2 = 0; index2 < num / 2; ++index2)
        resultHtml.Append("<br />" + Environment.NewLine);
      if ((startIndex = index1 + 2) >= val.Length)
        break;
    }
    resultHtml.Append(val.Substring(startIndex));
  }
}
