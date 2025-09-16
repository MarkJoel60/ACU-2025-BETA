// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXLinkParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXLinkParser : PXBlockParser
{
  protected override bool IsAllowedForParsing(Token tk)
  {
    return tk == Token.specialtagstart || tk == Token.bold || tk == Token.bolditalic || tk == Token.italic || tk == Token.striked || tk == Token.underlined || tk == Token.image || tk == Token.rss || tk == Token.rssart;
  }

  protected override bool IsThisToken(Token tk) => tk == Token.linkstart || tk == Token.link2start;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    int spos = 0;
    PXLinkParser.LinkType linkType = PXLinkParser.LinkType.Link;
    WikiArticle wikiArticle1 = new WikiArticle();
    WikiArticle wikiArticle2 = new WikiArticle();
    WikiArticle wikiArticle3 = new WikiArticle();
    PXLinkElement pxLinkElement = new PXLinkElement();
    bool allowParagraph = context.AllowParagraph;
    int startIndex = context.StartIndex;
    context.AllowParagraph = false;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken != Token.linkend && nextToken != Token.link2end)
      {
        if (TokenValue.Trim() == "^")
          pxLinkElement.IsInNewWindow = true;
        else if (nextToken == Token.linkseparator)
        {
          this.DetermineProperty(pxLinkElement, wikiArticle3, spos, context);
          wikiArticle3 = new WikiArticle();
          ++spos;
        }
        else
          linkType = this.ProcessToken(nextToken, TokenValue, pxLinkElement, context, wikiArticle3, result, spos, allowParagraph);
      }
      else
        break;
    }
    context.AllowParagraph = allowParagraph;
    if (linkType != PXLinkParser.LinkType.Link)
      return;
    this.DetermineProperty(pxLinkElement, wikiArticle3, spos, context);
    this.TryAddElementToParagraph((PXElement) pxLinkElement, context, result);
    if (!context.Settings.IsDesignMode)
      return;
    pxLinkElement.WikiTag = "link";
    pxLinkElement.WikiText = context.WikiText.Substring(startIndex, context.StartIndex - startIndex - 1);
  }

  private PXLinkParser.LinkType ProcessToken(
    Token tk,
    string tkValue,
    PXLinkElement elem,
    PXBlockParser.ParseContext context,
    WikiArticle curattr,
    WikiArticle result,
    int spos,
    bool allowParagraph)
  {
    if (!(curattr.Current is PXTextElement))
    {
      this.ProcessToken(tk, tkValue, context, curattr);
      if (curattr.Current is PXSpecialTagElement && string.Compare(((PXSpecialTagElement) curattr.Current).TagValue, "{up}", true) == 0)
        elem.IsFileLink = true;
      return PXLinkParser.LinkType.Link;
    }
    string strA = (curattr.Current as PXTextElement).Value.Trim() + tkValue.Trim();
    int count = curattr.GetAllElements().Count;
    if (spos == 0 && string.Compare(strA, "image:", true) == 0 && count == 1)
    {
      context.AllowParagraph = allowParagraph;
      this.ProcessToken(Token.image, "image:", context, result);
      context.AllowParagraph = false;
      return PXLinkParser.LinkType.Image;
    }
    if (spos == 0 && string.Compare(strA, "rss:", true) == 0 && count == 1)
    {
      context.AllowParagraph = allowParagraph;
      this.ProcessToken(Token.rss, "rss:", context, result);
      context.AllowParagraph = false;
      return PXLinkParser.LinkType.Rss;
    }
    if (spos == 0 && string.Compare(strA, "rssart:", true) == 0 && count == 1)
    {
      context.AllowParagraph = allowParagraph;
      this.ProcessToken(Token.rssart, "rssart:", context, result);
      context.AllowParagraph = false;
      return PXLinkParser.LinkType.Rss;
    }
    switch (spos)
    {
      case 0:
        tk = tk == Token.specialtagstart ? Token.specialtagstart : Token.chars;
        break;
      case 1:
        tk = this.IsAllowedForParsing(tk) ? tk : Token.chars;
        break;
      default:
        tk = Token.chars;
        break;
    }
    this.ProcessToken(tk, tkValue, context, curattr);
    return PXLinkParser.LinkType.Link;
  }

  private void DetermineProperty(
    PXLinkElement e,
    WikiArticle prop,
    int spos,
    PXBlockParser.ParseContext context)
  {
    if (spos == 0)
      this.AddToLink(e, prop);
    else if (spos == 1)
    {
      e.AddToCaption((IEnumerable<PXElement>) prop.GetAllElements());
    }
    else
    {
      if (!(prop.Current is PXTextElement current))
        return;
      if (string.Compare(current.Value.Trim().ToLower(), "noicon", true) == 0)
        e.NoIcon = true;
      else if (string.Compare(current.Value.Trim().ToLower(), "popup", true) == 0)
      {
        e.IsPopup = true;
      }
      else
      {
        if (PXImageParser.TryParseSize((IWidthHeightSettable) e, current.Value.Trim().ToLower()))
          return;
        e.Props = this.SanitizeAttributes(current.Value, context.Settings);
      }
    }
  }

  private void AddToLink(PXLinkElement link, WikiArticle prop)
  {
    List<PXElement> allElements = prop.GetAllElements();
    if (allElements.Count == 0 || !link.IsFileLink)
    {
      link.AddToLink((IEnumerable<PXElement>) allElements);
    }
    else
    {
      for (int index = 0; index < allElements.Count - 1; ++index)
        link.AddToLink(allElements[index]);
      PXElement e = allElements[allElements.Count - 1];
      if (e is PXTextElement)
      {
        PXTextElement pxTextElement = e as PXTextElement;
        string[] strArray = pxTextElement.Value.Split(':');
        int result;
        if (strArray.Length == 2 && int.TryParse(strArray[1], out result))
        {
          pxTextElement.Value = strArray[0];
          link.FileRevision = new int?(result);
        }
      }
      link.AddToLink(e);
    }
  }

  private enum LinkType
  {
    Link,
    Image,
    Rss,
  }
}
