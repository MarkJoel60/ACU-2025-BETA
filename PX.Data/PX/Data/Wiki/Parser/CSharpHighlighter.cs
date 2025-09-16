// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.CSharpHighlighter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class CSharpHighlighter : PXBlockParser, SourceTagProcessor.SourceHighlightingParser
{
  public static Dictionary<string, bool> Keywords = new Dictionary<string, bool>();
  private static TokenLexems[] localLexems = new TokenLexems[48 /*0x30*/]
  {
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "0"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "1"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "2"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "3"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "4"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "5"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "6"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "7"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "8"),
    new TokenLexems(CSharpHighlighter.LocalTokens.digit, "9"),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, "("),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, ")"),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, "{"),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, "}"),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, "["),
    new TokenLexems(CSharpHighlighter.LocalTokens.brace, "]"),
    new TokenLexems(CSharpHighlighter.LocalTokens.period, "."),
    new TokenLexems(CSharpHighlighter.LocalTokens.quote, "\""),
    new TokenLexems(CSharpHighlighter.LocalTokens.at, "@"),
    new TokenLexems(CSharpHighlighter.LocalTokens.singleQuote, "'"),
    new TokenLexems(CSharpHighlighter.LocalTokens.backslash, "\\"),
    new TokenLexems(CSharpHighlighter.LocalTokens.commentStart, "/*"),
    new TokenLexems(CSharpHighlighter.LocalTokens.commentEnd, "*/"),
    new TokenLexems(CSharpHighlighter.LocalTokens.commentLine, "//"),
    new TokenLexems(CSharpHighlighter.LocalTokens.plus, "+"),
    new TokenLexems(CSharpHighlighter.LocalTokens.minus, "-"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, " "),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, ","),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "\t"),
    new TokenLexems(CSharpHighlighter.LocalTokens.newline, "\r\n"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "*"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "/"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "="),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "!"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "~"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "%"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "^"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "?"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, ":"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "|"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "<"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, ">"),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, "="),
    new TokenLexems(CSharpHighlighter.LocalTokens.whitespace, ";"),
    new TokenLexems(Token.chars, "<b"),
    new TokenLexems(Token.chars, "<i"),
    new TokenLexems(Token.codebold, "<b>"),
    new TokenLexems(Token.codeitalic, "<i>")
  };

  static CSharpHighlighter()
  {
    CSharpHighlighter.Keywords.Add("#endregion", true);
    CSharpHighlighter.Keywords.Add("#region", true);
    CSharpHighlighter.Keywords.Add("abstract", true);
    CSharpHighlighter.Keywords.Add("event", true);
    CSharpHighlighter.Keywords.Add("new", true);
    CSharpHighlighter.Keywords.Add("struct", true);
    CSharpHighlighter.Keywords.Add("as", true);
    CSharpHighlighter.Keywords.Add("explicit", true);
    CSharpHighlighter.Keywords.Add("null", true);
    CSharpHighlighter.Keywords.Add("switch", true);
    CSharpHighlighter.Keywords.Add("base", true);
    CSharpHighlighter.Keywords.Add("extern", true);
    CSharpHighlighter.Keywords.Add("object", true);
    CSharpHighlighter.Keywords.Add("this", true);
    CSharpHighlighter.Keywords.Add("bool", true);
    CSharpHighlighter.Keywords.Add("false", true);
    CSharpHighlighter.Keywords.Add("operator", true);
    CSharpHighlighter.Keywords.Add("throw", true);
    CSharpHighlighter.Keywords.Add("break", true);
    CSharpHighlighter.Keywords.Add("finally", true);
    CSharpHighlighter.Keywords.Add("out", true);
    CSharpHighlighter.Keywords.Add("true", true);
    CSharpHighlighter.Keywords.Add("byte", true);
    CSharpHighlighter.Keywords.Add("fixed", true);
    CSharpHighlighter.Keywords.Add("override", true);
    CSharpHighlighter.Keywords.Add("try", true);
    CSharpHighlighter.Keywords.Add("case", true);
    CSharpHighlighter.Keywords.Add("float", true);
    CSharpHighlighter.Keywords.Add("params", true);
    CSharpHighlighter.Keywords.Add("typeof", true);
    CSharpHighlighter.Keywords.Add("catch", true);
    CSharpHighlighter.Keywords.Add("for", true);
    CSharpHighlighter.Keywords.Add("private", true);
    CSharpHighlighter.Keywords.Add("uint", true);
    CSharpHighlighter.Keywords.Add("char", true);
    CSharpHighlighter.Keywords.Add("foreach", true);
    CSharpHighlighter.Keywords.Add("protected", true);
    CSharpHighlighter.Keywords.Add("ulong", true);
    CSharpHighlighter.Keywords.Add("checked", true);
    CSharpHighlighter.Keywords.Add("goto", true);
    CSharpHighlighter.Keywords.Add("public", true);
    CSharpHighlighter.Keywords.Add("unchecked", true);
    CSharpHighlighter.Keywords.Add("class", true);
    CSharpHighlighter.Keywords.Add("if", true);
    CSharpHighlighter.Keywords.Add("readonly", true);
    CSharpHighlighter.Keywords.Add("unsafe", true);
    CSharpHighlighter.Keywords.Add("const", true);
    CSharpHighlighter.Keywords.Add("implicit", true);
    CSharpHighlighter.Keywords.Add("ref", true);
    CSharpHighlighter.Keywords.Add("ushort", true);
    CSharpHighlighter.Keywords.Add("continue", true);
    CSharpHighlighter.Keywords.Add("in", true);
    CSharpHighlighter.Keywords.Add("return", true);
    CSharpHighlighter.Keywords.Add("using", true);
    CSharpHighlighter.Keywords.Add("decimal", true);
    CSharpHighlighter.Keywords.Add("int", true);
    CSharpHighlighter.Keywords.Add("sbyte", true);
    CSharpHighlighter.Keywords.Add("virtual", true);
    CSharpHighlighter.Keywords.Add("default", true);
    CSharpHighlighter.Keywords.Add("interface", true);
    CSharpHighlighter.Keywords.Add("sealed", true);
    CSharpHighlighter.Keywords.Add("volatile", true);
    CSharpHighlighter.Keywords.Add("delegate", true);
    CSharpHighlighter.Keywords.Add("internal", true);
    CSharpHighlighter.Keywords.Add("short", true);
    CSharpHighlighter.Keywords.Add("void", true);
    CSharpHighlighter.Keywords.Add("do", true);
    CSharpHighlighter.Keywords.Add("is", true);
    CSharpHighlighter.Keywords.Add("sizeof", true);
    CSharpHighlighter.Keywords.Add("while", true);
    CSharpHighlighter.Keywords.Add("double", true);
    CSharpHighlighter.Keywords.Add("lock", true);
    CSharpHighlighter.Keywords.Add("stackalloc", true);
    CSharpHighlighter.Keywords.Add("else", true);
    CSharpHighlighter.Keywords.Add("long", true);
    CSharpHighlighter.Keywords.Add("static", true);
    CSharpHighlighter.Keywords.Add("enum", true);
    CSharpHighlighter.Keywords.Add("namespace", true);
    CSharpHighlighter.Keywords.Add("string", true);
  }

  public CSharpHighlighter()
    : base((IEnumerable<TokenLexems>) CSharpHighlighter.localLexems)
  {
  }

  private void ChangeSyntax(
    SourceElement.SyntaxType newType,
    ref SourceElement.SyntaxType curType,
    SourceElement.TextStyle curStyle,
    StringBuilder curValue,
    SourceElement.DiffState diffState,
    SourceElement ret)
  {
    if (newType == curType)
      return;
    ret.Source.Add(new SourceElement.SourcePart(curValue.ToString(), curType, diffState, curStyle));
    curType = newType;
    curValue.Length = 0;
  }

  private void SetStyleFlag(
    SourceElement.TextStyle styleFlag,
    ref SourceElement.TextStyle curStyle,
    SourceElement.SyntaxType curType,
    StringBuilder curValue,
    SourceElement.DiffState diffState,
    SourceElement ret)
  {
    ret.Source.Add(new SourceElement.SourcePart(curValue.ToString(), curType, diffState, curStyle));
    curValue.Length = 0;
    curStyle ^= styleFlag;
  }

  private void ReadQuotedString(
    PXBlockParser.ParseContext context,
    StringBuilder stringLiteral,
    Token delimiter)
  {
    bool flag = false;
    while (context.StartIndex <= context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == Token.endoftext)
        break;
      if (nextToken == CSharpHighlighter.LocalTokens.newline)
      {
        --context.StartIndex;
        break;
      }
      stringLiteral.Append(TokenValue);
      if (nextToken == delimiter && !flag)
        break;
      flag = nextToken == CSharpHighlighter.LocalTokens.backslash && !flag;
    }
  }

  private void ReadUpTo(
    PXBlockParser.ParseContext context,
    Token delimiter,
    ref SourceElement.DiffState diffState,
    SourceElement.SyntaxType curType,
    SourceElement.TextStyle curStyle,
    StringBuilder curValue,
    SourceElement ret)
  {
    bool flag = false;
    while (context.StartIndex <= context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (context.StartIndex == context.WikiText.Length && (nextToken == Token.emptytag || nextToken == Token.endoftext))
        break;
      if (flag)
        TokenValue = this.ProcessFirstLineToken(nextToken, ref diffState, curType, curStyle, curValue, ret, TokenValue);
      flag = nextToken == CSharpHighlighter.LocalTokens.newline;
      if (nextToken == delimiter & flag)
      {
        --context.StartIndex;
        break;
      }
      curValue.Append(TokenValue);
      if (nextToken == delimiter)
        break;
    }
  }

  private string ProcessFirstLineToken(
    Token t,
    ref SourceElement.DiffState diffState,
    SourceElement.SyntaxType curType,
    SourceElement.TextStyle curStyle,
    StringBuilder curValue,
    SourceElement ret,
    string tkValue)
  {
    if (curValue.Length > 0)
      ret.Source.Add(new SourceElement.SourcePart(curValue.ToString().TrimEnd('\r', '\n'), curType, diffState, curStyle));
    ret.Source.Add(new SourceElement.SourcePart(Environment.NewLine, SourceElement.SyntaxType.Text, SourceElement.DiffState.NoChange, curStyle));
    curValue.Length = 0;
    diffState = t != CSharpHighlighter.LocalTokens.plus ? (t != CSharpHighlighter.LocalTokens.minus ? SourceElement.DiffState.NoChange : SourceElement.DiffState.Removed) : SourceElement.DiffState.Added;
    return tkValue == "\n" ? "" : tkValue;
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    SourceElement sourceElement = new SourceElement();
    sourceElement.NeedClear = true;
    result.AddElement((PXElement) sourceElement);
    SourceElement.DiffState diffState = SourceElement.DiffState.NoChange;
    SourceElement.SyntaxType curType = SourceElement.SyntaxType.Text;
    SourceElement.TextStyle curStyle = (SourceElement.TextStyle) 0;
    StringBuilder stringBuilder = new StringBuilder();
    bool flag1 = true;
    bool flag2 = false;
    bool flag3 = false;
    int startIndex = context.StartIndex;
    string TokenValue;
    Token nextToken1 = this.GetNextToken(context, out TokenValue);
    while (TokenValue == "\r" || TokenValue == " " || TokenValue == "\t")
      nextToken1 = this.GetNextToken(context, out TokenValue);
    if (nextToken1 != CSharpHighlighter.LocalTokens.newline)
      context.StartIndex = startIndex;
    while (context.StartIndex < context.WikiText.Length)
    {
      Token nextToken2 = this.GetNextToken(context, out TokenValue);
      if (flag1)
        TokenValue = this.ProcessFirstLineToken(nextToken2, ref diffState, curType, curStyle, stringBuilder, sourceElement, TokenValue);
      if (nextToken2 == CSharpHighlighter.LocalTokens.digit && !flag2)
        this.ChangeSyntax(SourceElement.SyntaxType.Number, ref curType, curStyle, stringBuilder, diffState, sourceElement);
      else if (nextToken2 != CSharpHighlighter.LocalTokens.period || curType != SourceElement.SyntaxType.Number)
      {
        if (nextToken2 == CSharpHighlighter.LocalTokens.brace)
        {
          this.ChangeSyntax(SourceElement.SyntaxType.Bracket, ref curType, curStyle, stringBuilder, diffState, sourceElement);
        }
        else
        {
          if (nextToken2 == CSharpHighlighter.LocalTokens.quote || nextToken2 == CSharpHighlighter.LocalTokens.singleQuote)
          {
            this.ChangeSyntax(SourceElement.SyntaxType.StringLiteral, ref curType, curStyle, stringBuilder, diffState, sourceElement);
            stringBuilder.Append(TokenValue);
            if (nextToken2 == CSharpHighlighter.LocalTokens.quote & flag3)
            {
              this.ReadUpTo(context, nextToken2, ref diffState, curType, curStyle, stringBuilder, sourceElement);
              continue;
            }
            this.ReadQuotedString(context, stringBuilder, nextToken2);
            continue;
          }
          if (nextToken2 == CSharpHighlighter.LocalTokens.commentStart)
          {
            this.ChangeSyntax(SourceElement.SyntaxType.Comment, ref curType, curStyle, stringBuilder, diffState, sourceElement);
            stringBuilder.Append(TokenValue);
            this.ReadUpTo(context, CSharpHighlighter.LocalTokens.commentEnd, ref diffState, curType, curStyle, stringBuilder, sourceElement);
            continue;
          }
          if (nextToken2 == CSharpHighlighter.LocalTokens.commentLine)
          {
            this.ChangeSyntax(SourceElement.SyntaxType.Comment, ref curType, curStyle, stringBuilder, diffState, sourceElement);
            stringBuilder.Append(TokenValue);
            this.ReadUpTo(context, CSharpHighlighter.LocalTokens.newline, ref diffState, curType, curStyle, stringBuilder, sourceElement);
            continue;
          }
          if (nextToken2 == Token.codebold)
          {
            this.SetStyleFlag(SourceElement.TextStyle.Bold, ref curStyle, curType, stringBuilder, diffState, sourceElement);
            continue;
          }
          if (nextToken2 == Token.codeitalic)
          {
            this.SetStyleFlag(SourceElement.TextStyle.Italic, ref curStyle, curType, stringBuilder, diffState, sourceElement);
            continue;
          }
          if (nextToken2 == Token.chars && CSharpHighlighter.Keywords.ContainsKey(TokenValue))
            this.ChangeSyntax(SourceElement.SyntaxType.Keyword, ref curType, curStyle, stringBuilder, diffState, sourceElement);
          else
            this.ChangeSyntax(SourceElement.SyntaxType.Text, ref curType, curStyle, stringBuilder, diffState, sourceElement);
        }
      }
      flag2 = nextToken2 == Token.chars || nextToken2 == CSharpHighlighter.LocalTokens.digit & flag2;
      flag3 = nextToken2 == CSharpHighlighter.LocalTokens.at;
      flag1 = nextToken2 == CSharpHighlighter.LocalTokens.newline;
      stringBuilder.Append(TokenValue);
    }
    if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\n')
      --stringBuilder.Length;
    if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
      --stringBuilder.Length;
    sourceElement.Source.Add(new SourceElement.SourcePart(stringBuilder.ToString(), curType, diffState, curStyle));
  }

  SourceElement SourceTagProcessor.SourceHighlightingParser.Process(
    string content,
    List<PXHtmlAttribute> attributes)
  {
    return (SourceElement) this.Parse(content, new PXWikiParserContext()).GetAllElements()[0];
  }

  private class LocalTokens
  {
    public static Token keyword = new Token("identifier");
    public static Token digit = new Token(nameof (digit));
    public static Token brace = new Token(nameof (brace));
    public static Token at = new Token(nameof (at));
    public static Token plus = new Token(nameof (plus));
    public static Token minus = new Token(nameof (minus));
    public static Token newline = new Token(nameof (newline));
    public static Token whitespace = new Token(nameof (whitespace));
    public static Token period = new Token(nameof (period));
    public static Token quote = new Token(nameof (quote));
    public static Token singleQuote = new Token(nameof (singleQuote));
    public static Token backslash = new Token(nameof (backslash));
    public static Token commentStart = new Token(nameof (commentStart));
    public static Token commentEnd = new Token(nameof (commentEnd));
    public static Token commentLine = new Token(nameof (commentLine));
  }
}
