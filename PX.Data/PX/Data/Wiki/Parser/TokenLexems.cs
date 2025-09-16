// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.TokenLexems
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

public struct TokenLexems(Token Id, string Lexem, PXBlockParser parser)
{
  public Token tkId = Id;
  public string tkLexem = Lexem;
  public PXBlockParser parser = parser;

  public TokenLexems(Token Id, string Lexem)
    : this(Id, Lexem, (PXBlockParser) null)
  {
  }
}
