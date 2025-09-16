// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTemplateContentParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Wiki.Parser.BlockParsers;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXTemplateContentParser : PXBlockParser
{
  private static readonly TokenLexems[] localLexems = ((IEnumerable<TokenLexems>) new TokenLexems[11]
  {
    new TokenLexems(Token.htmlstart, "<", (PXBlockParser) new PXHtmlParser(new string[6]
    {
      "foreach",
      "action",
      "tr",
      "td",
      "div",
      "li"
    })),
    new TokenLexems(Token.htmlend, ">"),
    new TokenLexems(PXTemplateContentParser.LocalTokens.ifStart, "((@"),
    new TokenLexems(PXTemplateContentParser.LocalTokens.parameterStart, "(("),
    new TokenLexems(PXTemplateContentParser.LocalTokens.parameterEnd, "))"),
    new TokenLexems(Token.chars, "EM"),
    new TokenLexems(Token.chars, "EMA"),
    new TokenLexems(Token.chars, "EMAI"),
    new TokenLexems(Token.chars, "EMAIL"),
    new TokenLexems(Token.chars, "EMAIL("),
    new TokenLexems(PXTemplateContentParser.LocalTokens.userEmail, "EMAIL((")
  }).Concat<TokenLexems>((IEnumerable<TokenLexems>) PreviousValueHelper.PrevLexems).ToArray<TokenLexems>();
  private static readonly PXTemplateContentParser instance = new PXTemplateContentParser(false);
  private static readonly PXTemplateContentParser scriptInstance = new PXTemplateContentParser(true);
  private static readonly PXTemplateContentParser templateInstance = new PXTemplateContentParser(true)
  {
    _useSpecialBrackets = true
  };
  private static readonly PXTemplateContentParser nullableInstance = new PXTemplateContentParser(false)
  {
    _returnNullParameterValue = true
  };
  private bool _useSpecialBrackets;
  private readonly bool _processScript;
  private bool _returnNullParameterValue;
  private Func<PXCache, object, string, string> _getFieldValueFunc;
  private static readonly Regex tagRegex = new Regex("<([^>]*)>");

  protected PXTemplateContentParser(bool processScript)
    : base((IEnumerable<TokenLexems>) PXTemplateContentParser.localLexems)
  {
    this._processScript = processScript;
  }

  public static PXTemplateContentParser Instance => PXTemplateContentParser.instance;

  public static PXTemplateContentParser ScriptInstance => PXTemplateContentParser.scriptInstance;

  public static PXTemplateContentParser TemplateInstance
  {
    get => PXTemplateContentParser.templateInstance;
  }

  public static PXTemplateContentParser NullableInstance
  {
    get => PXTemplateContentParser.nullableInstance;
  }

  public string Process(
    string templateText,
    Tuple<IDictionary<string, object>, IDictionary<string, object>>[] parameters,
    PXGraph graph,
    string primaryView,
    Dictionary<string, AUWorkflowFormField[]> forms)
  {
    return this.Process(templateText, parameters, graph, primaryView, false, forms);
  }

  public string Process(
    string templateText,
    Tuple<IDictionary<string, object>, IDictionary<string, object>>[] parameters,
    PXGraph graph,
    string primaryView)
  {
    return this.Process(templateText, parameters, graph, primaryView, false, new Dictionary<string, AUWorkflowFormField[]>());
  }

  public string Process(
    string templateText,
    Tuple<IDictionary<string, object>, IDictionary<string, object>>[] parameters,
    PXGraph graph,
    string primaryView,
    bool useInternalParameterValue)
  {
    return this.Process(templateText, parameters, graph, primaryView, useInternalParameterValue, new Dictionary<string, AUWorkflowFormField[]>());
  }

  public string Process(
    string templateText,
    Tuple<IDictionary<string, object>, IDictionary<string, object>>[] parameters,
    PXGraph graph,
    string primaryView,
    bool useInternalParameterValue,
    Dictionary<string, AUWorkflowFormField[]> forms)
  {
    WikiArticle result = new WikiArticle();
    result.TypedContext.MultilineTemplateParameters = parameters;
    result.TypedContext.TemplateGraph = graph;
    result.TypedContext.UseInternalParameterValue = useInternalParameterValue;
    if (!string.IsNullOrEmpty(primaryView))
      result.TypedContext.CurrentRow = graph.Views[primaryView].Cache.Current;
    result.TypedContext.Forms = forms;
    this.Parse(new PXBlockParser.ParseContext(templateText, 0, new PXWikiParserContext()), result);
    return result.ParseContext["TemplateOutput"].ToString();
  }

  public string Process(string templateText, Dictionary<string, string> parameters)
  {
    WikiArticle result = new WikiArticle();
    result.TypedContext.TemplateParameters = (IDictionary<string, string>) parameters;
    this.Parse(new PXBlockParser.ParseContext(templateText, 0, new PXWikiParserContext()), result);
    return result.ParseContext["TemplateOutput"].ToString();
  }

  public string Process(string templateText, PXGraph graph, System.Type entityType, object[] keys)
  {
    WikiArticle result = new WikiArticle();
    result.TypedContext.TemplateGraph = graph;
    if (keys != null)
    {
      EntityHelper entityHelper = new EntityHelper(graph);
      graph.Caches[entityType].Current = entityHelper.GetEntityRow(entityType, keys);
    }
    result.TypedContext.CurrentRow = graph.Caches[entityType].Current;
    this.Parse(new PXBlockParser.ParseContext(templateText, 0, new PXWikiParserContext()), result);
    return result.ParseContext["TemplateOutput"].ToString();
  }

  public string Process(
    string templateText,
    PXGraph graph,
    System.Type entityType,
    object[] keys,
    Func<PXCache, object, string, string> getFieldValueFunc)
  {
    this._getFieldValueFunc = getFieldValueFunc;
    string str = this.Process(templateText, graph, entityType, keys);
    this._getFieldValueFunc = (Func<PXCache, object, string, string>) null;
    return str;
  }

  public void AddParameterValue(WikiArticle result, string paramName)
  {
    paramName = Tools.ConvertHtmlFragmentToSimpleText(paramName);
    StringBuilder stringBuilder = (StringBuilder) result.ParseContext["TemplateOutput"];
    try
    {
      string str = !this._returnNullParameterValue ? this.WrapValue(result.TypedContext.GetValue(paramName, this._getFieldValueFunc)) ?? paramName : this.WrapValue(result.TypedContext.GetValue(paramName, this._getFieldValueFunc));
      stringBuilder.Append(string.IsNullOrEmpty(str) ? str : str.Trim());
    }
    catch (Exception ex)
    {
      stringBuilder.Append($"<span style=\"color:red\">{paramName}</span>");
    }
  }

  private string WrapValue(string value)
  {
    return !this._useSpecialBrackets ? value : PXWikiParser.EncodeSpecialChars(string.IsNullOrEmpty(value) ? value : PXTemplateContentParser.tagRegex.Replace(value, string.Empty));
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder ret = new StringBuilder();
    result.ParseContext["TemplateOutput"] = (object) ret;
    if (context.WikiText == null)
      return;
    IDictionary<string, string> httpQueryParameters = ForeachTagProcessor.GetHttpQueryParameters();
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken1 = this.GetNextToken(context, out TokenValue);
      switch (nextToken1.Value)
      {
        case "ifStart":
          string str = "";
          Token nextToken2;
          while ((nextToken2 = this.GetNextToken(context, out TokenValue)) != PXTemplateContentParser.LocalTokens.parameterEnd && nextToken2 != Token.endoftext)
            str += TokenValue;
          ForeachTagProcessor.ProcessIfLexam(httpQueryParameters, str, ret);
          continue;
        case "previousValue":
          result.TypedContext.Previous = true;
          this.ProcessParameter(context, result);
          continue;
        case "parameterStart":
          this.ProcessParameter(context, result);
          continue;
        case "useremail":
          this.ProcessUserEmail(context, result);
          continue;
        case "htmlstart":
          if (PXTemplateContentParser.IsScriptTag(context, this._processScript))
          {
            this.ProcessToken(nextToken1, TokenValue, context, result);
            continue;
          }
          ret.Append(TokenValue);
          continue;
        default:
          ret.Append(TokenValue);
          continue;
      }
    }
  }

  private void ProcessUserEmail(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string TokenValue;
    for (Token nextToken = this.GetNextToken(context, out TokenValue); nextToken != PXTemplateContentParser.LocalTokens.parameterEnd && nextToken != Token.endoftext; nextToken = this.GetNextToken(context, out TokenValue))
      stringBuilder.Append(TokenValue);
    ((StringBuilder) result.ParseContext["TemplateOutput"]).Append(result.TypedContext.ContactProvider?.GetEmail(stringBuilder.ToString()));
  }

  private void ProcessParameter(PXBlockParser.ParseContext context, WikiArticle result)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string TokenValue;
    for (Token nextToken = this.GetNextToken(context, out TokenValue); nextToken != PXTemplateContentParser.LocalTokens.parameterEnd && nextToken != Token.endoftext; nextToken = this.GetNextToken(context, out TokenValue))
      stringBuilder.Append(TokenValue);
    this.AddParameterValue(result, stringBuilder.ToString());
  }

  private static bool IsScriptTag(PXBlockParser.ParseContext context, bool processScript)
  {
    int num1 = context.WikiText.IndexOf('>', context.StartIndex);
    if (num1 <= -1)
      return false;
    int num2 = context.WikiText.IndexOf(' ', context.StartIndex, num1 - context.StartIndex);
    string b = num2 > -1 ? context.WikiText.Substring(context.StartIndex, num2 - context.StartIndex) : context.WikiText.Substring(context.StartIndex, num1 - context.StartIndex);
    bool flag = false;
    if ((string.Equals("tr", b, StringComparison.OrdinalIgnoreCase) || string.Equals("td", b, StringComparison.OrdinalIgnoreCase) || string.Equals("div", b, StringComparison.OrdinalIgnoreCase) || string.Equals("li", b, StringComparison.OrdinalIgnoreCase)) && context.WikiText.Substring(context.StartIndex, num1 - context.StartIndex).Contains("data-foreach-view"))
      flag = true;
    return ((string.Equals("foreach", b, StringComparison.OrdinalIgnoreCase) ? 1 : (string.Equals("action", b, StringComparison.OrdinalIgnoreCase) & processScript ? 1 : 0)) | (flag ? 1 : 0)) != 0;
  }

  private static class LocalTokens
  {
    internal const string IfStart = "ifStart";
    internal const string ParameterStart = "parameterStart";
    internal const string ParameterEnd = "parameterEnd";
    internal const string UserEMail = "useremail";
    public static readonly Token ifStart = new Token(nameof (ifStart));
    public static readonly Token parameterStart = new Token(nameof (parameterStart));
    public static readonly Token parameterEnd = new Token(nameof (parameterEnd));
    public static readonly Token userEmail = new Token("useremail");
  }
}
