// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHeaderParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXHeaderParser : PXSectionParser
{
  private Token[] tokens = new Token[7]
  {
    Token.bold,
    Token.bolditalic,
    Token.italic,
    Token.link2start,
    Token.linkstart,
    Token.striked,
    Token.underlined
  };
  private List<Token> allowedTokens;

  public PXHeaderParser(int level)
    : base(level)
  {
    this.allowedTokens = new List<Token>((IEnumerable<Token>) this.tokens);
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    bool allowParagraph = context.AllowParagraph;
    context.AllowParagraph = false;
    WikiArticle wikiArticle = this.StartBlock(context);
    PXSectionElement elem = new PXSectionElement();
    result.AddElement((PXElement) elem);
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token tk = this.GetNextToken(context, out TokenValue);
      if (this.IsThisToken(tk))
      {
        this.AddHeader(result, wikiArticle);
        elem.StartIndex = context.StartIndex;
        result.TocItems.Add(this.CreateTOCItem(wikiArticle, this.Level));
        context.AllowParagraph = allowParagraph;
        base.DoParse(context, result);
        elem.EndIndex = context.StartIndex;
        return;
      }
      if (tk != Token.chars && !this.allowedTokens.Contains(tk))
        tk = Token.chars;
      this.ProcessToken(tk, TokenValue, context, wikiArticle);
    }
    this.AddHeader(result, wikiArticle);
    result.TocItems.Add(this.CreateTOCItem(wikiArticle, this.Level));
    wikiArticle.Current.IsError = true;
    context.AllowParagraph = allowParagraph;
  }

  private TOCItem CreateTOCItem(WikiArticle headerValue, int level)
  {
    string HeaderValue = "No Header";
    return !(headerValue.Current is PXHeaderElement) ? new TOCItem(HeaderValue, level) : new TOCItem(((PXHeaderElement) headerValue.Current).Value, level);
  }

  protected override bool IsThisToken(Token tk) => this.GetHeadingLevel(tk) == this.Level;

  /// <summary>
  /// Creates a new header element and puts it into new output which will contain all its children.
  /// </summary>
  /// <param name="context">Parsing context.</param>
  /// <returns>A WikiArticle object representing parsed header in memory.</returns>
  protected WikiArticle StartBlock(PXBlockParser.ParseContext context)
  {
    WikiArticle wikiArticle = new WikiArticle();
    wikiArticle.AddElement((PXElement) new PXHeaderElement((SectionLevel) this.Level)
    {
      SectionID = context.Settings.SectionCount
    });
    ++context.Settings.SectionCount;
    return wikiArticle;
  }

  /// <summary>
  /// Creates a new section element and adds a header to it.
  /// </summary>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  /// <param name="headerValue">A WikiArticle object representing parsed header in memory.</param>
  protected void AddHeader(WikiArticle result, WikiArticle headerValue)
  {
    headerValue.ReduceToContainer();
    PXSectionElement current1 = (PXSectionElement) result.Current;
    PXHeaderElement current2 = (PXHeaderElement) headerValue.Current;
    current2.ExtractExpandProps();
    current1.Header = current2;
    current1.IsCollapsed = current2.HasCollapsedTag;
    current1.IsCollapsable = current2.HasExpTag;
  }
}
