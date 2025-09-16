// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXBoxParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXBoxParser : PXBlockParser
{
  protected override bool IsAllowedForParsing(Token tk)
  {
    return tk != Token.boxstart && tk != Token.h1 && tk != Token.h2 && tk != Token.h3 && tk != Token.h4;
  }

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    string specialBox = this.CheckForSpecBox(context);
    this.StartBlock(result, context, specialBox);
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken != Token.boxend)
        this.ProcessToken(nextToken, TokenValue, context, result);
      else
        break;
    }
    this.CloseBlock(result);
  }

  /// <summary>Determines whether box is a hint or warning box.</summary>
  /// <param name="context">Parsing context.</param>
  /// <returns>{s:hint} or {s:warn} if box is a special box. Empty string otherwise.</returns>
  protected string CheckForSpecBox(PXBlockParser.ParseContext context)
  {
    string[] strArray = new string[4]
    {
      "{s:hint}",
      "{s:warn}",
      "{s:danger}",
      "{s:goodpractice}"
    };
    int startIndex = context.StartIndex;
    while (startIndex < context.WikiText.Length && char.IsWhiteSpace(context.WikiText[startIndex]))
      ++startIndex;
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (context.WikiText.IndexOf(strArray[index], startIndex, StringComparison.OrdinalIgnoreCase) == startIndex)
      {
        context.StartIndex = startIndex + strArray[index].Length;
        return strArray[index];
      }
    }
    return string.Empty;
  }

  /// <summary>Adds a new box element to output.</summary>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  /// <param name="context">Parsing context.</param>
  /// <param name="specialBox">{s:hint}, {s:warn}, {s:danger} or {s:goodpractice} if box is a special box.</param>
  protected void StartBlock(
    WikiArticle result,
    PXBlockParser.ParseContext context,
    string specialBox)
  {
    PXBoxElement elem = new PXBoxElement();
    string str;
    switch (specialBox)
    {
      case "{s:hint}":
        elem.IsHintBox = true;
        str = "info";
        break;
      case "{s:warn}":
        elem.IsWarnBox = true;
        str = "warn";
        break;
      case "{s:danger}":
        elem.IsDangerBox = true;
        str = "danger";
        break;
      case "{s:goodpractice}":
        elem.IsGoodPracticeBox = true;
        str = "goodpractice";
        break;
      default:
        str = "box";
        break;
    }
    if (context.Settings.IsDesignMode)
      elem.WikiTag = str;
    result.AddElement((PXElement) elem);
  }

  /// <summary>Reduces all box element children from output stack.</summary>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  protected void CloseBlock(WikiArticle result) => result.ReduceToContainer();
}
