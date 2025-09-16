// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXCodeParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXCodeParser : PXBaseTextParser
{
  private Token[] tokens = new Token[2]
  {
    Token.codebold,
    Token.codeitalic
  };

  public PXCodeParser() => this.allowedTokens = new List<Token>((IEnumerable<Token>) this.tokens);

  protected override bool IsThisToken(Token tk) => tk == Token.codeend;

  protected override TextStyle GetStyle() => TextStyle.Monotype;

  protected override string EscapeIfNeeded(string tkValue) => HttpUtility.HtmlEncode(tkValue);
}
