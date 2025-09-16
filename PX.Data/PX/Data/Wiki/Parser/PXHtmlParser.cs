// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHtmlParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXHtmlParser : PXBlockParser
{
  private static Token equals = new Token(nameof (equals));
  private static Token quote = new Token(nameof (quote));
  private static Token backslash = new Token(nameof (backslash));
  private static Token slash = new Token(nameof (slash));
  private static Token whitespace = new Token(nameof (whitespace));
  private int? _tagNameShift;
  private static TokenLexems[] localLexems = new TokenLexems[8]
  {
    new TokenLexems(PXHtmlParser.equals, "="),
    new TokenLexems(PXHtmlParser.quote, "\""),
    new TokenLexems(Token.htmlstart, "<"),
    new TokenLexems(Token.htmlend, ">"),
    new TokenLexems(PXHtmlParser.slash, "/"),
    new TokenLexems(PXHtmlParser.backslash, "\\"),
    new TokenLexems(PXHtmlParser.whitespace, " "),
    new TokenLexems(PXHtmlParser.whitespace, "\t")
  };
  private static readonly Dictionary<string, PXHtmlParser.TagProcessor> AllTags = new Dictionary<string, PXHtmlParser.TagProcessor>();
  private Dictionary<string, PXHtmlParser.TagProcessor> Tags = new Dictionary<string, PXHtmlParser.TagProcessor>(PXHtmlParser.AllTags.Count);

  static PXHtmlParser()
  {
    PXHtmlParser.AllTags.Add("source", (PXHtmlParser.TagProcessor) SourceTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("foreach", (PXHtmlParser.TagProcessor) ForeachTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("tr", (PXHtmlParser.TagProcessor) HtmlForeachTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("td", (PXHtmlParser.TagProcessor) HtmlForeachTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("div", (PXHtmlParser.TagProcessor) HtmlForeachTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("li", (PXHtmlParser.TagProcessor) HtmlForeachTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("nowiki", (PXHtmlParser.TagProcessor) NowikiTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("form", (PXHtmlParser.TagProcessor) DummyTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("script", (PXHtmlParser.TagProcessor) DummyTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("action", (PXHtmlParser.TagProcessor) ActionTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("MasterPage", (PXHtmlParser.TagProcessor) DummyTagProcessor.Instance);
    PXHtmlParser.AllTags.Add("html", (PXHtmlParser.TagProcessor) HtmlContentProcessor.Instance);
  }

  public PXHtmlParser(string[] allowedTags)
    : base((IEnumerable<TokenLexems>) PXHtmlParser.localLexems)
  {
    foreach (string allowedTag in allowedTags)
    {
      PXHtmlParser.TagProcessor tagProcessor;
      if (PXHtmlParser.AllTags.TryGetValue(allowedTag, out tagProcessor))
        this.Tags.Add(allowedTag, tagProcessor);
    }
    this.Tags.Add("sup", (PXHtmlParser.TagProcessor) HtmlTagProcessor.Instance);
    this.Tags.Add("sub", (PXHtmlParser.TagProcessor) HtmlTagProcessor.Instance);
    this.Tags.Add("br", (PXHtmlParser.TagProcessor) HtmlTagProcessor.Instance);
    this.Tags.Add("small", (PXHtmlParser.TagProcessor) HtmlTagProcessor.Instance);
  }

  public PXHtmlParser(string[] allowedTags, int shiftTagNameStart)
    : this(allowedTags)
  {
    this._tagNameShift = new int?(shiftTagNameStart);
  }

  private string ReadQuotedString(PXBlockParser.ParseContext context)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int startIndex1 = context.StartIndex;
    string str;
    if (this.GetNextTokenNoWhitespace(context, out str) != PXHtmlParser.quote)
    {
      context.StartIndex = startIndex1;
      return "";
    }
    int startIndex2 = context.StartIndex;
    Token nextToken;
    while (context.StartIndex < context.WikiText.Length && (nextToken = this.GetNextToken(context, out str)) != PXHtmlParser.quote)
    {
      if (nextToken == Token.htmlend)
      {
        context.StartIndex = startIndex2;
        return stringBuilder.ToString();
      }
      stringBuilder.Append(str);
      startIndex2 = context.StartIndex;
    }
    return stringBuilder.ToString();
  }

  public List<PXHtmlAttribute> ReadTagAttributes(PXBlockParser.ParseContext context)
  {
    List<PXHtmlAttribute> attributes = new List<PXHtmlAttribute>();
    Action<string, string> action = (Action<string, string>) ((name, value) =>
    {
      if (name.ToLower().StartsWith("on"))
        return;
      attributes.Add(new PXHtmlAttribute(name, value));
    });
    Token token = Token.chars;
    bool flag = false;
    string str1 = (string) null;
    string tkValue = (string) null;
    while (flag || (token = this.GetNextTokenNoWhitespace(context, out tkValue)) != Token.htmlend)
    {
      flag = false;
      if (str1 == null)
      {
        if (token == Token.chars)
          str1 = tkValue;
      }
      else if (token == PXHtmlParser.equals)
      {
        string str2 = this.ReadQuotedString(context);
        action(str1, str2);
        str1 = (string) null;
      }
      else
      {
        str1 = (string) null;
        flag = true;
      }
    }
    if (str1 != null)
      action(str1, "");
    if (context.Settings.IsDesignMode)
      attributes.Add(new PXHtmlAttribute("wikitag", "html"));
    return attributes;
  }

  private Token GetNextTokenNoWhitespace(PXBlockParser.ParseContext context, out string tkValue)
  {
    Token nextToken;
    do
      ;
    while ((nextToken = this.GetNextToken(context, out tkValue)) == PXHtmlParser.whitespace);
    return context.StartIndex >= context.WikiText.Length ? Token.htmlend : nextToken;
  }

  private bool ProcessCommentTag(PXBlockParser.ParseContext context, WikiArticle result)
  {
    if (context.StartIndex + 2 >= context.WikiText.Length || context.WikiText[context.StartIndex] != '!' || context.WikiText[context.StartIndex + 1] != '-' || context.WikiText[context.StartIndex + 2] != '-')
      return false;
    int num = context.WikiText.IndexOf("-->", context.StartIndex);
    if (num == -1)
      return false;
    int startIndex = context.StartIndex - 1;
    int length = System.Math.Max(num + "-->".Length - startIndex, 0);
    this.AddText(context.WikiText.Substring(startIndex, length), context, result);
    context.StartIndex = num + "-->".Length;
    return true;
  }

  protected override bool IsAllowedForParsing(Token tk) => false;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    if (this.ProcessCommentTag(context, result))
      return;
    if (this._tagNameShift.HasValue)
      context.StartIndex += this._tagNameShift.Value;
    int startIndex1 = context.StartIndex;
    string TokenValue;
    if (this.GetNextToken(context, out TokenValue) != Token.chars)
    {
      object obj;
      if (result.ParseContext.TryGetValue("TemplateOutput", out obj))
        ((StringBuilder) obj).Append(!context.Settings.IsDesignMode || !(TokenValue != "/") ? "<" + TokenValue : $"<{TokenValue} wikitag=\"html\"");
      else
        this.AddElementToParagraph((PXElement) new PXTextElement()
        {
          Value = (!context.Settings.IsDesignMode || !(TokenValue != "/") ? "<" + TokenValue : $"<{TokenValue} wikitag=\"html\"")
        }, context, result);
    }
    else
    {
      PXHtmlParser.TagProcessor instance;
      if (!this.Tags.TryGetValue(TokenValue, out instance))
        instance = (PXHtmlParser.TagProcessor) HtmlTagProcessor.Instance;
      List<PXHtmlAttribute> attributes = this.ReadTagAttributes(context);
      int startIndex2 = startIndex1 - 1;
      int startIndex3 = context.StartIndex;
      int num1 = context.StartIndex;
      string content;
      if (context.WikiText[context.StartIndex - 2] != '/')
      {
        int startIndex4 = context.StartIndex;
        int endIndex = startIndex4 - 1;
        string str1 = "</" + TokenValue;
        string str2 = "<" + TokenValue;
        int num2 = 1;
        int num3 = endIndex;
        while ((endIndex = context.WikiText.IndexOf(str1, endIndex + 1)) != -1)
        {
          if (!PXHtmlParser.tagInsideCodeBlock(context.WikiText, endIndex + str1.Length))
          {
            int tagEndPosition = PXHtmlParser.GetTagEndPosition(context.WikiText, endIndex + str1.Length, -1);
            if (tagEndPosition == -1)
            {
              endIndex = -1;
              break;
            }
            --num2;
            while ((num3 = context.WikiText.IndexOf(str2, num3 + 1, endIndex - num3)) != -1)
            {
              if (!PXHtmlParser.tagInsideCodeBlock(context.WikiText, num3 + str2.Length))
              {
                if ((num3 = PXHtmlParser.GetTagEndPosition(context.WikiText, num3 + str2.Length, endIndex)) != -1)
                  ++num2;
                else
                  break;
              }
            }
            num3 = endIndex;
            if (num2 <= 0)
            {
              num1 = tagEndPosition + 1;
              break;
            }
          }
        }
        if (endIndex == -1)
          endIndex = startIndex4;
        content = !(instance is HtmlForeachTagProcessor) ? context.WikiText.Substring(startIndex4, endIndex - startIndex4) : context.WikiText.Substring(startIndex2, num1 - startIndex2);
      }
      else
        content = "";
      context.StartIndex = num1;
      PXElement elem = instance.Process(TokenValue, content, attributes, result, context.Settings);
      if (elem == null)
        return;
      this.TryAddElementToParagraph(elem, context, result);
    }
  }

  private static int GetTagEndPosition(string text, int startIndex, int endIndex)
  {
    bool flag1 = false;
    bool flag2 = false;
    for (; startIndex < text.Length && (endIndex < 0 || startIndex < endIndex); ++startIndex)
    {
      char ch = text[startIndex];
      if (!flag1 && ch == '>')
        return startIndex;
      if (!flag2 && ch == '"')
        flag1 = !flag1;
      flag2 = ch == '\\' && !flag2;
    }
    return -1;
  }

  /// <summary>
  /// Determines if target index range is inside a CodePh block
  /// </summary>
  /// <param name="text">Text to search inside of</param>
  /// <param name="startIndex">First index in the range</param>
  /// <returns></returns>
  private static bool tagInsideCodeBlock(string text, int startIndex)
  {
    int num1 = text.LastIndexOf("{{", startIndex, StringComparison.Ordinal);
    if (num1 == -1)
      return false;
    int num2 = text.LastIndexOf("}}", startIndex, startIndex - num1, StringComparison.Ordinal);
    return num1 > num2;
  }

  public interface TagProcessor
  {
    PXElement Process(
      string tagName,
      string content,
      List<PXHtmlAttribute> attributes,
      WikiArticle result,
      PXWikiParserContext settings);
  }
}
