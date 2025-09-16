// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXSectionParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXSectionParser : PXBlockParser
{
  private int level;

  public PXSectionParser(int sectionLevel) => this.level = sectionLevel;

  protected override bool IsAllowedForParsing(Token tk) => base.IsAllowedForParsing(tk);

  public int GetHeadingLevel(Token tk)
  {
    if (tk == Token.h1)
      return 1;
    if (tk == Token.h2)
      return 2;
    if (tk == Token.h3)
      return 3;
    return tk == Token.h4 ? 4 : -1;
  }

  /// <summary>
  /// Determines whether given Token indicates beginning of next section (i.e. section of the same or less level).
  /// </summary>
  /// <param name="tk">H1, H2, H3 or H4 Token value.</param>
  /// <returns>True if Token indicates beginning of next section. Otherwise false.</returns>
  protected bool IsNextSection(Token tk)
  {
    int headingLevel = this.GetHeadingLevel(tk);
    return headingLevel != -1 && headingLevel <= this.Level;
  }

  /// <summary>Gets level of current section.</summary>
  protected int Level => this.level;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (this.IsNextSection(nextToken))
      {
        context.StartIndex -= TokenValue.Length;
        break;
      }
      this.ProcessToken(nextToken, TokenValue, context, result);
    }
    this.CloseBlock(result);
  }

  /// <summary>
  /// Reduces all section element children from output stack.
  /// </summary>
  /// <param name="result">A WikiArticle object representing a parsed article in memory.</param>
  protected void CloseBlock(WikiArticle result) => result.ReduceToContainer();
}
