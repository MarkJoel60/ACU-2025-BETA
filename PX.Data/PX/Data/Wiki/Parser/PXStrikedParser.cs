// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXStrikedParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXStrikedParser : PXBaseTextParser
{
  private Token[] tokens = new Token[10]
  {
    Token.bold,
    Token.italic,
    Token.bolditalic,
    Token.underlined,
    Token.linkstart,
    Token.link2start,
    Token.codestart,
    Token.horline,
    Token.specialtagstart,
    Token.htmlstart
  };

  public PXStrikedParser()
  {
    this.allowedTokens = new List<Token>((IEnumerable<Token>) this.tokens);
  }

  protected override bool IsThisToken(Token tk) => tk == Token.striked;

  protected override TextStyle GetStyle() => TextStyle.Striked;
}
