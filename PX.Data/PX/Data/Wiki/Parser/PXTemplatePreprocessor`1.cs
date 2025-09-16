// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTemplatePreprocessor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXTemplatePreprocessor<T> : PXBlockParser where T : PXTemplatePreprocessor<T>, new()
{
  protected static TokenLexems[] _localLexems = new TokenLexems[15]
  {
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.linkStart, "[["),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.linkStart, "["),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.linkEnd, "]"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.linkEnd, "]]"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.tableStart, "{|"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.tableEnd, "|}"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.templateStart, "{{{"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.templateEnd, "}}}"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.templateNameDelimiter, "|"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.paramDelimiter, "|"),
    new TokenLexems(PXTemplatePreprocessor<T>.LocalTokens.equals, "="),
    new TokenLexems(Token.chars, "{{{{"),
    new TokenLexems(Token.chars, "}}}}"),
    new TokenLexems(Token.chars, "{{"),
    new TokenLexems(Token.chars, "}}")
  };
  public static readonly T Instance = new T();
  private readonly string _templateNameSeparatorString;
  private readonly string _templateStartString;
  private readonly string _templateEndString;

  protected PXTemplatePreprocessor(params TokenLexems[] lexems)
    : base((IEnumerable<TokenLexems>) lexems)
  {
    this._templateNameSeparatorString = PXTemplatePreprocessor<T>.GetFirstElement(this.GetTokenReservedLexems(PXTemplatePreprocessor<T>.LocalTokens.templateNameDelimiter));
    this._templateStartString = PXTemplatePreprocessor<T>.GetFirstElement(this.GetTokenReservedLexems(PXTemplatePreprocessor<T>.LocalTokens.templateStart));
    this._templateEndString = PXTemplatePreprocessor<T>.GetFirstElement(this.GetTokenReservedLexems(PXTemplatePreprocessor<T>.LocalTokens.templateEnd));
  }

  protected PXTemplatePreprocessor()
    : this(PXTemplatePreprocessor<T>._localLexems)
  {
  }

  public static string Process(string wikiText, PXWikiParserContext context)
  {
    return PXTemplatePreprocessor<T>.Process(wikiText, context, 0);
  }

  private static string Process(string wikiText, PXWikiParserContext context, int level)
  {
    if (context == null || wikiText == null || level > 100)
      return wikiText;
    WikiArticle result = new WikiArticle();
    result.ParseContext["TemplateContext"] = (object) context;
    result.ParseContext["TemplateLevel"] = (object) level;
    PXTemplatePreprocessor<T>.Instance.Parse(new PXBlockParser.ParseContext(wikiText, 0, new PXWikiParserContext()), result);
    return ((StringBuilder) result.ParseContext["PreprocessorOutput"]).ToString();
  }

  private void InsertTemplate(
    WikiArticle result,
    string templateName,
    Dictionary<string, string> parameters)
  {
    PXWikiParserContext context = (PXWikiParserContext) result.ParseContext["TemplateContext"];
    StringBuilder stringBuilder = (StringBuilder) result.ParseContext["PreprocessorOutput"];
    int num = (int) result.ParseContext["TemplateLevel"];
    string str = PXTemplatePreprocessor<T>.Process(this.GetTemplate(templateName, context).GetContent(parameters), context, num + 1);
    stringBuilder.Append(str);
  }

  private static string GetFirstElement(IEnumerable<string> source)
  {
    IEnumerator<string> enumerator = source.GetEnumerator();
    return enumerator == null || !enumerator.MoveNext() ? (string) null : enumerator.Current;
  }

  protected virtual PXCustomTemplateDeclaration GetTemplate(
    string templateName,
    PXWikiParserContext context)
  {
    return string.Compare(templateName, "newsline", true) == 0 ? (PXCustomTemplateDeclaration) new PXNewslineDeclaration(context) : (PXCustomTemplateDeclaration) new PXTemplateDeclaration(templateName, context);
  }

  protected virtual Dictionary<string, string> ReadParameterList(
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    bool flag1 = false;
    bool flag2 = false;
    int num = 0;
    string key = (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == PXTemplatePreprocessor<T>.LocalTokens.templateStart)
        this.ProcessTemplateDeclaration(context, result);
      else if (!flag2 && nextToken == PXTemplatePreprocessor<T>.LocalTokens.equals)
      {
        key = stringBuilder.ToString().Trim();
        stringBuilder.Length = 0;
        flag2 = true;
      }
      else
      {
        if (flag1 && nextToken == PXTemplatePreprocessor<T>.LocalTokens.linkEnd)
          flag1 = false;
        else if (!flag1)
        {
          if (num > 0 && nextToken == PXTemplatePreprocessor<T>.LocalTokens.tableEnd)
            --num;
          else if (nextToken == PXTemplatePreprocessor<T>.LocalTokens.tableStart)
            ++num;
          else if (num <= 0)
          {
            if (nextToken == PXTemplatePreprocessor<T>.LocalTokens.linkStart)
              flag1 = true;
            else if (nextToken == PXTemplatePreprocessor<T>.LocalTokens.paramDelimiter || nextToken == PXTemplatePreprocessor<T>.LocalTokens.templateEnd)
            {
              if (!string.IsNullOrEmpty(key))
                dictionary[key] = stringBuilder.ToString().Trim();
              stringBuilder.Length = 0;
              flag2 = false;
              if (nextToken != PXTemplatePreprocessor<T>.LocalTokens.templateEnd)
                continue;
              break;
            }
          }
        }
        stringBuilder.Append(TokenValue);
      }
    }
    return dictionary;
  }

  protected void ProcessTemplateDeclaration(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder stringBuilder = (StringBuilder) result.ParseContext["PreprocessorOutput"];
    PXWikiParserContext wikiParserContext = (PXWikiParserContext) result.ParseContext["TemplateContext"];
    int num1 = context.WikiText.IndexOf(this._templateNameSeparatorString, context.StartIndex);
    int num2 = context.WikiText.IndexOf(this._templateEndString, context.StartIndex);
    int startIndex = context.StartIndex;
    if (num2 != -1 && (num1 == -1 || num1 > num2))
      num1 = num2;
    if (num1 == -1 || num2 == -1)
      return;
    string templateName = context.WikiText.Substring(context.StartIndex, num1 - context.StartIndex).Trim();
    Dictionary<string, string> parameters;
    if (num1 == num2)
    {
      context.StartIndex = num1 + 3;
      parameters = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
    else
    {
      context.StartIndex = num1 + 1;
      parameters = this.ReadParameterList(context, result);
    }
    if (wikiParserContext.IsDesignMode)
    {
      stringBuilder.Append("<span wikitag=\"template\" wikitext=\"");
      stringBuilder.Append(this._templateStartString);
      string s = context.WikiText.Substring(startIndex, num2 - startIndex);
      stringBuilder.Append(HttpUtility.HtmlEncode(s));
      stringBuilder.Append(this._templateEndString);
      stringBuilder.Append("\">");
    }
    this.InsertTemplate(result, templateName, parameters);
    if (!wikiParserContext.IsDesignMode)
      return;
    stringBuilder.Append("</span>");
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder stringBuilder = new StringBuilder();
    result.ParseContext["PreprocessorOutput"] = (object) stringBuilder;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      if (this.GetNextToken(context, out TokenValue) == PXTemplatePreprocessor<T>.LocalTokens.templateStart)
        this.ProcessTemplateDeclaration(context, result);
      else
        stringBuilder.Append(TokenValue);
    }
  }

  protected static class LocalTokens
  {
    public static Token linkStart = new Token(nameof (linkStart));
    public static Token linkEnd = new Token(nameof (linkEnd));
    public static Token templateStart = new Token(nameof (templateStart));
    public static Token templateEnd = new Token(nameof (templateEnd));
    public static Token templateNameDelimiter = new Token("templatenameDelimiter");
    public static Token tableStart = new Token(nameof (tableStart));
    public static Token tableEnd = new Token(nameof (tableEnd));
    public static Token paramDelimiter = new Token(nameof (paramDelimiter));
    public static Token equals = new Token(nameof (equals));
  }
}
