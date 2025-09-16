// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.ForeachTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Wiki.Parser.BlockParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class ForeachTagProcessor : PXBlockParser, PXHtmlParser.TagProcessor
{
  private static TokenLexems[] localLexems = ((IEnumerable<TokenLexems>) new TokenLexems[3]
  {
    new TokenLexems(ForeachTagProcessor.LocalTokens.ifStart, "((@"),
    new TokenLexems(ForeachTagProcessor.LocalTokens.parameterStart, "(("),
    new TokenLexems(ForeachTagProcessor.LocalTokens.parameterEnd, "))")
  }).Concat<TokenLexems>((IEnumerable<TokenLexems>) PreviousValueHelper.PrevLexems).ToArray<TokenLexems>();
  private static readonly ForeachTagProcessor instance = new ForeachTagProcessor();

  public static ForeachTagProcessor Instance => ForeachTagProcessor.instance;

  protected ForeachTagProcessor()
    : base((IEnumerable<TokenLexems>) ForeachTagProcessor.localLexems)
  {
  }

  protected virtual bool IsForeachViewAttribute(string attributeName)
  {
    return attributeName.Equals("view", StringComparison.OrdinalIgnoreCase);
  }

  PXElement PXHtmlParser.TagProcessor.Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    Dictionary<string, string> parameters = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    string viewName = (string) null;
    string sortby = (string) null;
    IDictionary<string, string> queryParams = ForeachTagProcessor.GetHttpQueryParameters();
    foreach (PXHtmlAttribute attribute in attributes)
    {
      if (this.IsForeachViewAttribute(attribute.name))
        viewName = attribute.value;
      else if (attribute.name.Equals("sortby", StringComparison.OrdinalIgnoreCase))
      {
        attribute.value.With<string, bool>((Func<string, bool>) (c => queryParams.TryGetValue(c, out sortby)));
      }
      else
      {
        string str;
        if (queryParams.TryGetValue(attribute.value, out str))
          parameters.Add(attribute.name, str);
      }
    }
    if ((result.TypedContext.TemplateGraph == null || viewName == null) && result.TypedContext.MultilineTemplateParameters == null)
      return (PXElement) null;
    StringBuilder ret = (StringBuilder) result.ParseContext["TemplateOutput"];
    IForeachIIterator iterator = result.TypedContext.SetForeachIterator(viewName, (IDictionary<string, string>) parameters, sortby);
    string namePrefix = string.IsNullOrWhiteSpace(viewName) ? string.Empty : viewName + ".";
    foreach (object row in (IEnumerable<object>) iterator)
    {
      PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(content, 0, new PXWikiParserContext());
      while (context.StartIndex < context.WikiText.Length)
      {
        string TokenValue;
        Token nextToken1 = this.GetNextToken(context, out TokenValue);
        switch (nextToken1.Value)
        {
          case "ifStart":
            string str = "";
            Token nextToken2;
            while ((nextToken2 = this.GetNextToken(context, out TokenValue)) != ForeachTagProcessor.LocalTokens.parameterEnd && nextToken2 != Token.endoftext)
              str += TokenValue;
            ForeachTagProcessor.ProcessIfLexam(queryParams, str, ret);
            continue;
          case "previousValue":
            result.TypedContext.Previous = true;
            this.ProcessParameter(result, ret, iterator, namePrefix, row, context, nextToken1);
            continue;
          case "parameterStart":
            this.ProcessParameter(result, ret, iterator, namePrefix, row, context, nextToken1);
            continue;
          default:
            ret.Append(TokenValue);
            continue;
        }
      }
    }
    return (PXElement) null;
  }

  private void ProcessParameter(
    WikiArticle result,
    StringBuilder ret,
    IForeachIIterator iterator,
    string namePrefix,
    object row,
    PXBlockParser.ParseContext context,
    Token t)
  {
    string paramName = "";
    string TokenValue;
    for (Token nextToken = this.GetNextToken(context, out TokenValue); nextToken != ForeachTagProcessor.LocalTokens.parameterEnd && nextToken != Token.endoftext; nextToken = this.GetNextToken(context, out TokenValue))
      paramName += TokenValue;
    if (paramName.StartsWith(namePrefix))
    {
      string fieldName = paramName.Substring(namePrefix.Length);
      ret.Append(iterator.GetValue(fieldName, row));
    }
    else
      PXTemplateContentParser.Instance.AddParameterValue(result, paramName);
  }

  internal static void ProcessIfLexam(
    IDictionary<string, string> queryParams,
    string param,
    StringBuilder ret)
  {
    string[] strArray = param.Split('|');
    if (strArray.Length <= 2)
      return;
    string str1 = strArray[0];
    string str2 = strArray[1];
    string str3 = strArray[2];
    bool flag = true;
    if (!string.IsNullOrEmpty(str1))
    {
      string str4 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string str5 in str4.Split(chArray))
      {
        int length = str5.IndexOf('=');
        if (length > -1)
        {
          string key = str5.Substring(0, length);
          string str6 = length < str5.Length - 1 ? str5.Substring(length + 1) : (string) null;
          string str7 = queryParams.ContainsKey(key) ? queryParams[key] : (string) null;
          if (str7 != null && str7.Length == 0)
            str7 = (string) null;
          flag &= str6 == str7;
        }
      }
    }
    ret.Append(flag ? str2 : str3);
  }

  internal static IDictionary<string, string> GetHttpQueryParameters()
  {
    Dictionary<string, string> httpQueryParameters = new Dictionary<string, string>();
    string str1 = HttpContext.Current.With<HttpContext, HttpRequest>((Func<HttpContext, HttpRequest>) (c => c.Request)).With<HttpRequest, string>((Func<HttpRequest, string>) (r => r.RawUrl));
    if (str1 != null)
    {
      string[] strArray = str1.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
      string str2 = strArray[strArray.Length - 1];
      if (!string.IsNullOrEmpty(str2))
      {
        int num = str2.IndexOf('?');
        if (num > -1 && num + 1 < str2.Length)
        {
          string str3 = str2.Substring(num + 1);
          char[] chArray = new char[1]{ '&' };
          foreach (string str4 in str3.Split(chArray))
          {
            int length = str4.IndexOf('=');
            if (length > -1)
            {
              string key = str4.Substring(0, length);
              string str5 = length + 1 < str4.Length ? str4.Substring(length + 1) : (string) null;
              httpQueryParameters.Add(key, str5);
            }
          }
        }
      }
    }
    return (IDictionary<string, string>) httpQueryParameters;
  }

  private static class LocalTokens
  {
    internal const string IfStart = "ifStart";
    internal const string ParameterStart = "parameterStart";
    internal const string ParameterEnd = "parameterEnd";
    public static readonly Token ifStart = new Token(nameof (ifStart));
    public static readonly Token parameterStart = new Token(nameof (parameterStart));
    public static readonly Token parameterEnd = new Token(nameof (parameterEnd));
  }
}
