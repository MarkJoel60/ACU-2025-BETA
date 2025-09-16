// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXContainerTemplatePreprocessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXContainerTemplatePreprocessor : 
  PXTemplatePreprocessor<PXContainerTemplatePreprocessor>
{
  private const string _PREPROCESSOR_OUT_KEY = "PreprocessorOutput";
  protected static readonly Token LineEnd = new Token("lineEnd");
  protected static readonly TokenLexems[] GenericLocalLexems = new TokenLexems[11]
  {
    new TokenLexems(PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.templateStart, "{{+"),
    new TokenLexems(PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.templateEnd, "+}}"),
    new TokenLexems(PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.templateNameDelimiter, "|"),
    new TokenLexems(PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.paramDelimiter, "///"),
    new TokenLexems(PXContainerTemplatePreprocessor.LineEnd, "\n"),
    new TokenLexems(Token.chars, "{"),
    new TokenLexems(Token.chars, "{{"),
    new TokenLexems(Token.chars, "+"),
    new TokenLexems(Token.chars, "+}"),
    new TokenLexems(Token.chars, "/"),
    new TokenLexems(Token.chars, "//")
  };

  public PXContainerTemplatePreprocessor()
    : base(PXContainerTemplatePreprocessor.GenericLocalLexems)
  {
  }

  protected override PXCustomTemplateDeclaration GetTemplate(
    string templateName,
    PXWikiParserContext context)
  {
    return (PXCustomTemplateDeclaration) new PXContainerTemplatePreprocessor.PXContainerTemplateDeclaration(templateName, context);
  }

  protected override Dictionary<string, string> ReadParameterList(
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    bool flag1 = true;
    bool flag2 = false;
    string key = (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (!flag2 && nextToken == PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.templateStart)
      {
        object obj = result.ParseContext["PreprocessorOutput"];
        result.ParseContext["PreprocessorOutput"] = (object) stringBuilder;
        this.ProcessTemplateDeclaration(context, result);
        result.ParseContext["PreprocessorOutput"] = obj;
      }
      else if (!flag1 && nextToken == PXContainerTemplatePreprocessor.LineEnd)
      {
        if (flag2)
        {
          key = stringBuilder.ToString().Trim();
          stringBuilder.Length = 0;
          flag2 = false;
        }
        flag1 = true;
      }
      else if (flag1 && nextToken == PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.paramDelimiter)
      {
        if (!string.IsNullOrEmpty(key))
          dictionary[key] = stringBuilder.ToString().Trim();
        stringBuilder.Length = 0;
        flag1 = false;
        flag2 = true;
      }
      else
      {
        if (nextToken == PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.LocalTokens.templateEnd)
        {
          if (!string.IsNullOrEmpty(key))
            dictionary[key] = stringBuilder.ToString().Trim();
          stringBuilder.Length = 0;
          break;
        }
        if (nextToken == Token.chars && TokenValue.ToLower().StartsWith("<source lang=\"csharp\">"))
        {
          int num = context.WikiText.IndexOf("</source>", context.StartIndex);
          int length = num == -1 ? context.WikiText.Length - context.StartIndex : num - context.StartIndex;
          stringBuilder.Append(TokenValue);
          stringBuilder.Append(context.WikiText.Substring(context.StartIndex, length));
          stringBuilder.Append("</source>");
          context.StartIndex = context.StartIndex + length + "</source>".Length;
        }
        else
          stringBuilder.Append(TokenValue);
      }
    }
    return dictionary;
  }

  protected class PXContainerTemplateDeclaration(string templateName, PXWikiParserContext context) : 
    PXSimpleTemplateDeclaration("ContainerTemplate:" + templateName, context)
  {
    private const string _TEMPLATE_NAME_PREFIX = "ContainerTemplate:";
  }
}
