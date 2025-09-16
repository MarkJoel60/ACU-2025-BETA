// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXLexemsTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

public static class PXLexemsTable
{
  private static TokenLexems[] reservedLexems = new TokenLexems[48 /*0x30*/]
  {
    new TokenLexems(Token.h1, "==", (PXBlockParser) new PXHeaderParser(1)),
    new TokenLexems(Token.h2, "===", (PXBlockParser) new PXHeaderParser(2)),
    new TokenLexems(Token.h3, "====", (PXBlockParser) new PXHeaderParser(3)),
    new TokenLexems(Token.h4, "=====", (PXBlockParser) new PXHeaderParser(4)),
    new TokenLexems(Token.bold, "'''", (PXBlockParser) new PXBoldParser()),
    new TokenLexems(Token.italic, "''", (PXBlockParser) new PXItalicParser()),
    new TokenLexems(Token.bolditalic, "'''''", (PXBlockParser) new PXBoldItalicParser()),
    new TokenLexems(Token.linkstart, "[", (PXBlockParser) new PXLinkParser()),
    new TokenLexems(Token.linkend, "]"),
    new TokenLexems(Token.link2start, "[[", (PXBlockParser) new PXLinkParser()),
    new TokenLexems(Token.link2end, "]]"),
    new TokenLexems(Token.linkseparator, "|"),
    new TokenLexems(Token.image, "image:", (PXBlockParser) new PXImageParser()),
    new TokenLexems(Token.rss, "rss:", (PXBlockParser) new PXRssParser()),
    new TokenLexems(Token.rssart, "rssart:", (PXBlockParser) new PXRssArticleParser()),
    new TokenLexems(Token.underlined, "__", (PXBlockParser) new PXUnderlinedParser()),
    new TokenLexems(Token.striked, "--", (PXBlockParser) new PXStrikedParser()),
    new TokenLexems(Token.horline, "----", (PXBlockParser) new PXHorLineParser()),
    new TokenLexems(Token.codestart, "{{", (PXBlockParser) new PXCodeParser()),
    new TokenLexems(Token.codeend, "}}"),
    new TokenLexems(Token.codeboxstart, "{{{{", (PXBlockParser) new PXCodeBoxParser()),
    new TokenLexems(Token.codeboxend, "}}}}"),
    new TokenLexems(Token.boxstart, "(((", (PXBlockParser) new PXBoxParser()),
    new TokenLexems(Token.boxend, ")))"),
    new TokenLexems(Token.indent, ":", (PXBlockParser) PXIndentParser.Instance),
    new TokenLexems(Token.indent, ";", (PXBlockParser) PXIndentParser.Instance),
    new TokenLexems(Token.indent, "*", (PXBlockParser) PXIndentParser.Instance),
    new TokenLexems(Token.indent, "#", (PXBlockParser) PXIndentParser.Instance),
    new TokenLexems(Token.specialtagstart, "{", (PXBlockParser) new PXSpecialTagParser()),
    new TokenLexems(Token.specialtagend, "}"),
    new TokenLexems(Token.tablestart, "{|", (PXBlockParser) new PXTableParser()),
    new TokenLexems(Token.tableend, "|}"),
    new TokenLexems(Token.htmlstart, "<", (PXBlockParser) new PXHtmlParser(new string[6]
    {
      "source",
      "nowiki",
      "form",
      "script",
      "MasterPage",
      "html"
    })),
    new TokenLexems(Token.htmlend, ">"),
    new TokenLexems(Token.hiddenstart, "=^", (PXBlockParser) new PXHiddenParser()),
    new TokenLexems(Token.hiddenend, "^="),
    new TokenLexems(Token.emptytag, "{{{"),
    new TokenLexems(Token.emptytag, "}}}"),
    new TokenLexems(Token.emptytag, "''''"),
    new TokenLexems(Token.emptytag, "---"),
    new TokenLexems(Token.emptytag, "(("),
    new TokenLexems(Token.emptytag, "))"),
    new TokenLexems(Token.embedvideostart, "[[[", (PXBlockParser) new PXEmbeddedVideoParser()),
    new TokenLexems(Token.embedvideoend, "]]]"),
    new TokenLexems(Token.htmlstartpartial, "<i", (PXBlockParser) new PXHtmlParser(new string[6]
    {
      "source",
      "nowiki",
      "form",
      "script",
      "MasterPage",
      "html"
    }, -1)),
    new TokenLexems(Token.htmlstartpartial, "<b", (PXBlockParser) new PXHtmlParser(new string[6]
    {
      "source",
      "nowiki",
      "form",
      "script",
      "MasterPage",
      "html"
    }, -1)),
    new TokenLexems(Token.codeitalic, "<i>", (PXBlockParser) new PXCodeItalicParser()),
    new TokenLexems(Token.codebold, "<b>", (PXBlockParser) new PXCodeBoldParser())
  };

  public static TokenLexems[] ReservedLexems => PXLexemsTable.reservedLexems;
}
