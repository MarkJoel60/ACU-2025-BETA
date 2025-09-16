// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXIndentParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXIndentParser : PXBlockParser
{
  private static PXIndentParser instance = new PXIndentParser();

  public static PXIndentParser Instance => PXIndentParser.instance;

  protected PXIndentParser()
  {
  }

  protected override bool IsAllowedForParsing(Token tk)
  {
    return tk != Token.h1 && tk != Token.h2 && tk != Token.h3 && tk != Token.h4;
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    PXIndentContainer elem = new PXIndentContainer(context.WikiText[context.StartIndex - 1]);
    Token tk = Token.indent;
    string TokenValue = context.WikiText[context.StartIndex - 1].ToString();
    bool allowParagraph = context.AllowParagraph;
    string str1 = "";
    string str2 = "";
    context.AllowParagraph = false;
    if (this.IsRedirect(context, result))
      context.AllowParagraph = allowParagraph;
    else if (context.StartIndex > 1 && context.WikiText[context.StartIndex - 2] != '\n')
    {
      context.AllowParagraph = allowParagraph;
      this.AddTextNoParagraph(TokenValue, result, true);
    }
    else
    {
      for (; tk == Token.indent; tk = this.GetNextToken(context, out TokenValue))
      {
        PXIndentContainer pxIndentContainer = elem;
        for (; tk == Token.indent; tk = this.GetNextToken(context, out TokenValue))
        {
          if (str1.StartsWith(str2) && str2.Length > 0)
          {
            if (pxIndentContainer.Children.Length == 0 || !(pxIndentContainer.Children[pxIndentContainer.Children.Length - 1] is PXIndentContainer) || (int) ((PXIndentContainer) pxIndentContainer.Children[pxIndentContainer.Children.Length - 1]).Type != (int) TokenValue[0])
              pxIndentContainer.AddChild((PXElement) new PXIndentContainer(TokenValue[0]));
            pxIndentContainer = (PXIndentContainer) pxIndentContainer.Children[pxIndentContainer.Children.Length - 1];
          }
          str2 += TokenValue;
        }
        str1 = str2;
        str2 = "";
        WikiArticle result1 = new WikiArticle();
        this.ProcessIndentedLine(context, result1, tk, TokenValue);
        pxIndentContainer.AddChildren(result1.GetAllElements());
      }
      elem.IsReduced = true;
      result.AddElement((PXElement) elem);
      context.AllowParagraph = allowParagraph;
      context.StartIndex -= TokenValue.Length;
    }
  }

  private string SubtractIndent(string curIndent, string indent)
  {
    return curIndent.Length <= indent.Length ? "" : curIndent.Substring(indent.Length);
  }

  private void ProcessIndentedLine(
    PXBlockParser.ParseContext context,
    WikiArticle result,
    Token tk,
    string tkValue)
  {
    PXIndentElement elem = new PXIndentElement();
    result.AddElement((PXElement) elem);
    this.ProcessToken(tk, tkValue, context, result);
    while (context.StartIndex < context.WikiText.Length && (tk != Token.chars || tkValue.Length < Environment.NewLine.Length || !tkValue.EndsWith(Environment.NewLine)))
    {
      tk = this.GetNextToken(context, out tkValue);
      this.ProcessToken(tk, tkValue, context, result);
    }
    result.ReduceToContainer();
  }

  private bool IsRedirect(PXBlockParser.ParseContext context, WikiArticle result)
  {
    Token chars = Token.chars;
    string str = "";
    int startIndex = context.StartIndex;
    if (context.StartIndex > 1 && context.WikiText[context.StartIndex - 2] != '\n')
      return false;
    string TokenValue;
    Token nextToken;
    for (nextToken = this.GetNextToken(context, out TokenValue); nextToken == Token.chars && context.StartIndex < context.WikiText.Length; nextToken = this.GetNextToken(context, out TokenValue))
      str += TokenValue;
    if (str.Trim().ToLower() != "redirect" || nextToken != Token.linkstart)
    {
      context.StartIndex = startIndex;
      return false;
    }
    string wikiText = "[";
    for (; context.StartIndex < context.WikiText.Length; ++context.StartIndex)
    {
      wikiText += context.WikiText[context.StartIndex].ToString();
      if (context.WikiText[context.StartIndex] == ']' || context.WikiText[context.StartIndex] == '\n')
        break;
    }
    WikiArticle wikiArticle = new WikiArticle();
    PXBlockParser.ParseContext parseContext = new PXBlockParser.ParseContext(wikiText, 0, context.Settings);
    PXBlockParser pxBlockParser = new PXBlockParser();
    parseContext.AllowParagraph = false;
    PXBlockParser.ParseContext context1 = parseContext;
    WikiArticle result1 = wikiArticle;
    pxBlockParser.Parse(context1, result1);
    if (wikiArticle.GetAllElements().Count != 1 || !(wikiArticle.Current is PXLinkElement))
    {
      context.StartIndex = startIndex;
      return false;
    }
    PXRedirectElement elem = new PXRedirectElement(wikiArticle.Current as PXLinkElement);
    result.AddElement((PXElement) elem);
    ++context.StartIndex;
    return true;
  }
}
