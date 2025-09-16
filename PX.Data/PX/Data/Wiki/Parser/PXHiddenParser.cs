// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHiddenParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXHiddenParser : PXBlockParser
{
  protected override bool IsAllowedForParsing(Token tk)
  {
    return tk != Token.hiddenstart && tk != Token.h1 && tk != Token.h2 && tk != Token.h3 && tk != Token.h4;
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    WikiArticle result1 = new WikiArticle();
    StringBuilder stringBuilder = new StringBuilder();
    string TokenValue;
    while (context.StartIndex < context.WikiText.Length)
    {
      Token nextToken = this.GetNextToken(context, out TokenValue);
      stringBuilder.Append(TokenValue);
      if (nextToken == Token.chars && TokenValue.EndsWith(Environment.NewLine))
        break;
    }
    base.DoParse(new PXBlockParser.ParseContext(stringBuilder.ToString(), 0, context.Settings)
    {
      AllowParagraph = false
    }, result1);
    PXHiddenElement elem = new PXHiddenElement();
    bool allowParagraph = context.AllowParagraph;
    elem.Caption = result1.GetAllElements().ToArray();
    result.AddElement((PXElement) elem);
    context.AllowParagraph = false;
    while (context.StartIndex < context.WikiText.Length)
    {
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken != Token.hiddenend)
      {
        this.ProcessToken(nextToken, TokenValue, context, result);
        if (TokenValue.EndsWith(Environment.NewLine))
          context.AllowParagraph = allowParagraph;
      }
      else
        break;
    }
    context.AllowParagraph = allowParagraph;
    result.ReduceToContainer();
  }
}
