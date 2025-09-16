// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXBaseTextParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Base class for parsing plain and styled text.</summary>
internal class PXBaseTextParser : PXBlockParser
{
  protected List<Token> allowedTokens;

  protected override bool IsAllowedForParsing(Token tk) => this.allowedTokens.Contains(tk);

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    WikiArticle result1 = new WikiArticle();
    PXStyledTextElement elem = new PXStyledTextElement(this.GetStyle());
    bool allowParagraph = context.AllowParagraph;
    result1.AddElement((PXElement) elem);
    context.AllowParagraph = false;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (!this.IsThisToken(nextToken))
      {
        TokenValue = this.EscapeIfNeeded(TokenValue);
        this.ProcessToken(nextToken, TokenValue, context, result1);
      }
      else
        break;
    }
    result1.ReduceToContainer();
    context.AllowParagraph = allowParagraph;
    this.TryAddElementToParagraph((PXElement) elem, context, result);
  }

  protected virtual TextStyle GetStyle() => TextStyle.None;

  protected virtual bool IsSpaceAfterBlockRequired => false;

  protected virtual string EscapeIfNeeded(string tkValue) => tkValue;
}
