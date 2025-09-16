// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXCodeBoxParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXCodeBoxParser : PXBlockParser
{
  private Token[] tokens = new Token[2]
  {
    Token.codebold,
    Token.codeitalic
  };

  protected override bool IsAllowedForParsing(Token tk)
  {
    return ((IEnumerable<Token>) this.tokens).Contains<Token>(tk);
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    bool allowParagraph = context.AllowParagraph;
    PXCodeBoxElement elem = new PXCodeBoxElement();
    elem.NeedClear = true;
    elem.WikiTag = context.Settings.IsDesignMode ? "codebox" : (string) null;
    result.AddElement((PXElement) elem);
    context.AllowParagraph = false;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken != Token.codeboxend)
        this.ProcessToken(nextToken, TokenValue, context, result);
      else
        break;
    }
    result.ReduceToContainer();
    context.AllowParagraph = allowParagraph;
  }
}
