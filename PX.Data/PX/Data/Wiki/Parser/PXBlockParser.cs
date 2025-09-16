// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXBlockParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Main parsing class.</summary>
public class PXBlockParser
{
  protected Dictionary<Token, PXBlockParser> allowedBlocks = new Dictionary<Token, PXBlockParser>();
  protected Dictionary<string, TokenLexems> ReservedLexems;
  private Dictionary<Token, ICollection<string>> _reservedTokenLexems;
  protected string ReservedSymbols;
  protected char[] ReservedSymbolsWithNewline;
  private bool newlineReserved;
  private object ReservedLexemsLock = new object();

  public PXBlockParser()
  {
    this.ReservedLexems = (Dictionary<string, TokenLexems>) null;
    this.ReservedSymbols = (string) null;
  }

  protected internal PXBlockParser(IEnumerable<TokenLexems> reservedLexems)
  {
    this.SetReservedLexems(reservedLexems);
    this.SetReservedTokenLexems(reservedLexems);
    this.CalculateReservedSymbols();
  }

  private void SetReservedLexems(IEnumerable<TokenLexems> reservedLexems)
  {
    this.ReservedLexems = PXBlockParser.CalculateReservedLexems(reservedLexems);
  }

  private void SetReservedTokenLexems(IEnumerable<TokenLexems> reservedLexems)
  {
    this._reservedTokenLexems = PXBlockParser.CalculateReservedTokenLexems(reservedLexems);
  }

  private static Dictionary<string, TokenLexems> CalculateReservedLexems(
    IEnumerable<TokenLexems> reservedLexems)
  {
    Dictionary<string, TokenLexems> reservedLexems1 = new Dictionary<string, TokenLexems>();
    if (reservedLexems != null)
    {
      foreach (TokenLexems reservedLexem in reservedLexems)
        reservedLexems1[reservedLexem.tkLexem] = reservedLexem;
    }
    return reservedLexems1;
  }

  private static Dictionary<Token, ICollection<string>> CalculateReservedTokenLexems(
    IEnumerable<TokenLexems> reservedLexems)
  {
    Dictionary<Token, ICollection<string>> reservedTokenLexems = new Dictionary<Token, ICollection<string>>();
    if (reservedLexems != null)
    {
      foreach (TokenLexems reservedLexem in reservedLexems)
      {
        if (!reservedTokenLexems.ContainsKey(reservedLexem.tkId))
          reservedTokenLexems.Add(reservedLexem.tkId, (ICollection<string>) new List<string>());
        reservedTokenLexems[reservedLexem.tkId].Add(reservedLexem.tkLexem);
      }
    }
    return reservedTokenLexems;
  }

  private void CalculateReservedSymbols()
  {
    this.ReservedSymbols = PXBlockParser.CalculateReservedSymbols((IDictionary<string, TokenLexems>) this.ReservedLexems);
    this.newlineReserved = this.ReservedSymbols.Contains("\n");
    this.ReservedSymbolsWithNewline = (this.ReservedSymbols + (this.newlineReserved ? "" : "\n")).ToCharArray();
  }

  private static string CalculateReservedSymbols(IDictionary<string, TokenLexems> lexems)
  {
    string reservedSymbols = "";
    foreach (TokenLexems tokenLexems in (IEnumerable<TokenLexems>) lexems.Values)
    {
      char ch = tokenLexems.tkLexem[0];
      if (reservedSymbols.IndexOf(ch) == -1)
        reservedSymbols += ch.ToString();
    }
    return reservedSymbols;
  }

  protected IEnumerable<string> GetTokenReservedLexems(Token tk)
  {
    if (this._reservedTokenLexems.ContainsKey(tk))
    {
      foreach (string tokenReservedLexem in (IEnumerable<string>) this._reservedTokenLexems[tk])
        yield return tokenReservedLexem;
    }
  }

  /// <summary>Parses given wiki-text.</summary>
  /// <param name="wikiText">Wiki-text to parse.</param>
  /// <param name="settings">Parsing settings.</param>
  /// <returns>A WikiArticle object representing a parsed article in memory.</returns>
  public WikiArticle Parse(string wikiText, PXWikiParserContext settings)
  {
    PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(wikiText, 0, settings);
    WikiArticle result = new WikiArticle();
    this.Parse(context, result);
    return result;
  }

  /// <summary>
  /// Parses wiki-text depending on context and puts result inside of WikiArticle object.
  /// </summary>
  /// <param name="context">Parsing context.</param>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  public void Parse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    this.InitDictionary();
    this.DoParse(context, result);
  }

  /// <summary>
  /// Parses a block of wiki-text and puts result inside of WikiArticle object.
  /// Override this method in your derived classes to provide parsing of your custom tags.
  /// </summary>
  /// <param name="context">Parsing context.</param>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  protected virtual void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    if (string.IsNullOrEmpty(context.WikiText))
      return;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      this.ProcessToken(this.GetNextToken(context, out TokenValue), TokenValue, context, result);
    }
  }

  protected string SanitizeAttributes(string props, PXWikiParserContext settings)
  {
    if (string.IsNullOrWhiteSpace(props))
      return string.Empty;
    props = props.Replace("<", "&lt;").Replace(">", "&gt;");
    PXBlockParser.ParseContext parseContext = new PXBlockParser.ParseContext(props, 0, settings);
    PXHtmlParser pxHtmlParser = new PXHtmlParser(new string[0]);
    StringBuilder stringBuilder = new StringBuilder();
    PXBlockParser.ParseContext context = parseContext;
    foreach (PXHtmlAttribute readTagAttribute in pxHtmlParser.ReadTagAttributes(context))
      stringBuilder.Append($" {readTagAttribute.name}=\"{readTagAttribute.value}\"");
    return stringBuilder.ToString();
  }

  /// <summary>
  /// Inits dictionary which determines which PXBlockParser-derived object to use to parse given wiki-tag.
  /// Override this method and fill the allowedBlocks dictionary if you want your custom
  /// PXBlockParser-derived object to be called to parse a specific or custom tag.
  /// </summary>
  protected virtual void InitDictionary()
  {
    lock (this.ReservedLexemsLock)
    {
      if (this.ReservedLexems == null)
      {
        this.SetReservedLexems((IEnumerable<TokenLexems>) this.GetReservedLexems());
        this.CalculateReservedSymbols();
      }
      if (this.allowedBlocks.Count != 0)
        return;
      foreach (TokenLexems tokenLexems in this.ReservedLexems.Values)
      {
        if (tokenLexems.parser != null && this.IsAllowedForParsing(tokenLexems.tkId))
        {
          if (!this.allowedBlocks.ContainsKey(tokenLexems.tkId))
            this.allowedBlocks.Add(tokenLexems.tkId, (PXBlockParser) null);
          this.allowedBlocks[tokenLexems.tkId] = tokenLexems.parser;
        }
      }
    }
  }

  protected virtual TokenLexems[] GetReservedLexems() => PXLexemsTable.ReservedLexems;

  /// <summary>
  /// Determines whether a parser object whould be called for parsing a specific tag or it
  /// should be treated as plain text. Override this method in your custom block parsers if
  /// you want to exclude any tags from parsing and simply send them to output.
  /// </summary>
  /// <param name="tk">Token representing wiki-tag.</param>
  /// <returns>True. Override this method to provide custom logic.</returns>
  protected virtual bool IsAllowedForParsing(Token tk) => true;

  /// <summary>
  /// Determines whether given wiki-tag matches to the one being parsed.
  /// Override this method to determine when your custom parser should terminate.
  /// </summary>
  /// <param name="tk">Token representing wiki-tag.</param>
  /// <returns>True. Override this method to provide custom logic.</returns>
  protected virtual bool IsThisToken(Token tk) => true;

  /// <summary>
  /// Processes next token by calling an appropriate block parser for it or by sending it to output as text.
  /// </summary>
  /// <param name="tk">A Token to process.</param>
  /// <param name="tkValue">String containing token value.</param>
  /// <param name="context">Parsing context.</param>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  protected void ProcessToken(
    Token tk,
    string tkValue,
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    if (this.allowedBlocks.ContainsKey(tk))
      this.allowedBlocks[tk].Parse(context, result);
    else
      this.AddText(tkValue, context, result);
  }

  /// <summary>Returns next Token from non-parsed text.</summary>
  /// <param name="context">Parsing context.</param>
  /// <param name="TokenValue">[out] String containing token value.</param>
  /// <returns></returns>
  protected internal Token GetNextToken(PXBlockParser.ParseContext context, out string TokenValue)
  {
    return PXBlockParser.GetNextTokenInternal(context, this.ReservedSymbols, this.ReservedSymbolsWithNewline, this.newlineReserved, (IDictionary<string, TokenLexems>) this.ReservedLexems, out TokenValue);
  }

  public static IEnumerable<KeyValuePair<Token, string>> GetTokens(
    PXBlockParser.ParseContext context,
    params TokenLexems[] lexems)
  {
    Dictionary<string, TokenLexems> reservedLexems = PXBlockParser.CalculateReservedLexems((IEnumerable<TokenLexems>) lexems);
    string reservedSymbols = PXBlockParser.CalculateReservedSymbols((IDictionary<string, TokenLexems>) reservedLexems);
    bool isNewlineReserved = reservedSymbols.Contains("\n");
    char[] reservedSymbolsWithNewline = (reservedSymbols + (isNewlineReserved ? "" : "\n")).ToCharArray();
    if (context.WikiText != null)
    {
      while (context.StartIndex < context.WikiText.Length)
      {
        string tokenValue;
        yield return new KeyValuePair<Token, string>(PXBlockParser.GetNextTokenInternal(context, reservedSymbols, reservedSymbolsWithNewline, isNewlineReserved, (IDictionary<string, TokenLexems>) reservedLexems, out tokenValue), tokenValue);
      }
    }
  }

  private static Token GetNextTokenInternal(
    PXBlockParser.ParseContext context,
    string reservedSymbols,
    char[] reservedSymbolsWithNewline,
    bool isNewlineReserved,
    IDictionary<string, TokenLexems> lexems,
    out string tokenValue)
  {
    if (context.StartIndex >= context.WikiText.Length)
    {
      tokenValue = "";
      return Token.endoftext;
    }
    if (PXBlockParser.IsReservedSymbol(context.WikiText[context.StartIndex], reservedSymbols))
      return PXBlockParser.TryGetSpecialToken(context, lexems, out tokenValue);
    int index = context.WikiText.IndexOfAny(reservedSymbolsWithNewline, context.StartIndex);
    if (index == -1)
    {
      tokenValue = context.WikiText.Substring(context.StartIndex);
      context.StartIndex = context.WikiText.Length;
      return Token.chars;
    }
    if (!isNewlineReserved && context.WikiText[index] == '\n')
      ++index;
    tokenValue = context.WikiText.Substring(context.StartIndex, index - context.StartIndex);
    context.StartIndex = index;
    return Token.chars;
  }

  /// <summary>
  /// Determines whether given character is one of reserved symbols.
  /// </summary>
  /// <param name="symbol">A character to check.</param>
  /// <returns>True if 'symbol' is a reserved character. Otherwise false.</returns>
  protected bool IsReservedSymbol(char symbol)
  {
    return PXBlockParser.IsReservedSymbol(symbol, this.ReservedSymbols);
  }

  private static bool IsReservedSymbol(char symbol, string symbols)
  {
    return symbols.IndexOf(symbol) != -1;
  }

  private Token TryGetSpecialToken(PXBlockParser.ParseContext context, out string tokenVal)
  {
    return PXBlockParser.TryGetSpecialToken(context, (IDictionary<string, TokenLexems>) this.ReservedLexems, out tokenVal);
  }

  private static Token TryGetSpecialToken(
    PXBlockParser.ParseContext context,
    IDictionary<string, TokenLexems> lexems,
    out string tokenVal)
  {
    Token chars = Token.chars;
    int startIndex = context.StartIndex;
    string str1 = context.WikiText[startIndex].ToString() ?? "";
    ++context.StartIndex;
    PXBlockParser.IsKeyword(str1, lexems, ref chars);
    tokenVal = str1;
    for (++context.StartIndex; context.StartIndex <= context.WikiText.Length; ++context.StartIndex)
    {
      string str2 = context.WikiText.Substring(startIndex, context.StartIndex - startIndex);
      if (PXBlockParser.IsKeyword(str2, lexems, ref chars))
        tokenVal = str2;
      else
        break;
    }
    --context.StartIndex;
    return chars;
  }

  private bool IsKeyword(string value, ref Token tk)
  {
    return PXBlockParser.IsKeyword(value, (IDictionary<string, TokenLexems>) this.ReservedLexems, ref tk);
  }

  private static bool IsKeyword(
    string value,
    IDictionary<string, TokenLexems> lexems,
    ref Token tk)
  {
    TokenLexems tokenLexems;
    if (!lexems.TryGetValue(value, out tokenLexems))
      return false;
    tk = tokenLexems.tkId;
    return true;
  }

  /// <summary>Appends plain text to output.</summary>
  /// <param name="text">A string of text to append.</param>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  protected virtual void AddText(
    string text,
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    if (context.AllowParagraph)
      this.AddTextToParagraph(text, result);
    else
      this.AddTextNoParagraph(text, result);
  }

  protected void AddTextToParagraph(string text, WikiArticle result)
  {
    if (!(result.Current is PXParagraphElement))
    {
      result.AddElement((PXElement) new PXParagraphElement()
      {
        HasNewLineInDesidnMode = false
      });
      if (text == Environment.NewLine)
        return;
    }
    PXParagraphElement current = (PXParagraphElement) result.Current;
    if (text == Environment.NewLine && current.Children.Length != 0 && current.Children[current.Children.Length - 1] is PXTextElement child && child.Value.Length >= 2 && child.Value[child.Value.Length - 2] == '\r' && child.Value[child.Value.Length - 1] == '\n')
    {
      PXParagraphElement elem = new PXParagraphElement();
      result.AddElement((PXElement) elem);
    }
    else
    {
      if (current.Children.Length == 0 || !(current.Children[current.Children.Length - 1] is PXTextElement))
        current.AddChild((PXElement) new PXTextElement());
      ((PXTextElement) current.Children[current.Children.Length - 1]).Value += text;
    }
  }

  protected void AddTextNoParagraph(string text, WikiArticle result)
  {
    this.AddTextNoParagraph(text, result, false);
  }

  protected void AddTextNoParagraph(string text, WikiArticle result, bool tryAddToParagraph)
  {
    if (tryAddToParagraph)
    {
      if (text == Environment.NewLine && result.Current is PXTextElement current1 && current1.Value.Length >= 2 && current1.Value[current1.Value.Length - 1] == '\n' && current1.Value[current1.Value.Length - 2] == '\r')
      {
        current1.Value = current1.Value.Substring(0, current1.Value.Length - 2);
        result.AddElement((PXElement) new PXParagraphElement()
        {
          HasNewLineInDesidnMode = false
        });
        text = string.Empty;
      }
      if (result.Current is PXParagraphElement current2)
      {
        if (text == Environment.NewLine && current2.Children.Length != 0 && current2.Children[current2.Children.Length - 1] is PXTextElement child && child.Value.Length >= 2 && child.Value[child.Value.Length - 2] == '\r' && child.Value[child.Value.Length - 1] == '\n')
        {
          PXParagraphElement elem = new PXParagraphElement();
          result.AddElement((PXElement) elem);
        }
        else
        {
          if (current2.Children.Length == 0 || !(current2.Children[current2.Children.Length - 1] is PXTextElement))
            current2.AddChild((PXElement) new PXTextElement());
          ((PXTextElement) current2.Children[current2.Children.Length - 1]).Value += text;
        }
      }
      else
        this.AddTextNoParagraph(text, result, false);
    }
    else
    {
      if (!(result.Current is PXTextElement))
        result.AddElement((PXElement) new PXTextElement());
      ((PXTextElement) result.Current).Value += text;
    }
  }

  protected void AddElementToParagraph(
    PXElement elem,
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    if (!context.AllowParagraph)
    {
      result.AddElement(elem);
    }
    else
    {
      if (!(result.Current is PXParagraphElement))
        result.AddElement((PXElement) new PXParagraphElement()
        {
          HasNewLineInDesidnMode = false
        });
      ((PXContainerElement) result.Current).AddChild(elem);
    }
  }

  protected void TryAddElementToParagraph(
    PXElement elem,
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    if (!context.AllowParagraph)
      result.AddElement(elem);
    else if (result.Current is PXParagraphElement current)
      current.AddChild(elem);
    else
      result.AddElement(elem);
  }

  public static string EncodeSpecialChars(string text)
  {
    PXBlockParser parser = new PXBlockParser((IEnumerable<TokenLexems>) PXLexemsTable.ReservedLexems);
    return PXBlockParser.EncodeSpecialChars(text, parser);
  }

  public static string EncodeSpecialChars(string text, PXBlockParser parser)
  {
    PXBlockParser.EncodeContext context = new PXBlockParser.EncodeContext(text, 0, new PXWikiParserContext());
    PXBlockParser.DoEncodeSpecialChars(context, parser);
    return context.ToString();
  }

  public static string DecodeSpecialCharsSimple(string text)
  {
    text = text.Replace("&#0123;", "{").Replace("&#0125;", "}").Replace("&#123;", "{").Replace("&#125;", "}").Replace("&#91;", "[").Replace("&#93;", "]").Replace("&#35;", "#").Replace("&#59;", ";").Replace("&#95;", "_").Replace("&#58;", ":");
    return text;
  }

  private static void DoEncodeSpecialChars(
    PXBlockParser.EncodeContext context,
    PXBlockParser parser)
  {
    if (string.IsNullOrEmpty(context.WikiText))
      return;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = parser.GetNextToken((PXBlockParser.ParseContext) context, out TokenValue);
      string str = TokenValue;
      Token chars = Token.chars;
      if (nextToken != chars)
        str = TextUtils.HtmlEncode(TokenValue);
      context.Out.Append(str);
    }
  }

  /// <summary>
  /// Parsing context which is passed to every parser during parsing.
  /// </summary>
  public class ParseContext
  {
    public readonly string WikiText;
    public int StartIndex;
    public readonly PXWikiParserContext Settings;
    public bool AllowParagraph = true;

    public ParseContext(string wikiText, int startIndex, PXWikiParserContext settings)
    {
      this.WikiText = wikiText;
      this.StartIndex = startIndex;
      this.Settings = settings;
    }
  }

  private class EncodeContext : PXBlockParser.ParseContext
  {
    private readonly StringBuilder _out;

    public EncodeContext(string wikiText, int startIndex, PXWikiParserContext settings)
      : base(wikiText, startIndex, settings)
    {
      this._out = new StringBuilder();
    }

    public StringBuilder Out => this._out;

    public override string ToString() => this._out.ToString();
  }
}
