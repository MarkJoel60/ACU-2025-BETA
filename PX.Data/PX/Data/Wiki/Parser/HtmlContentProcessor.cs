// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.HtmlContentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Used for processing of any content which is inside of &lt;HTML&gt;&lt;/HTML&gt; tag.
/// </summary>
internal class HtmlContentProcessor : PXBlockParser, PXHtmlParser.TagProcessor
{
  private static HtmlContentProcessor instance = new HtmlContentProcessor();

  public static HtmlContentProcessor Instance => HtmlContentProcessor.instance;

  protected HtmlContentProcessor()
  {
  }

  protected override bool IsAllowedForParsing(Token tk)
  {
    return tk == Token.linkstart || tk == Token.link2start || tk == Token.image;
  }

  public PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    PXHtmlContentElement htmlContentElement = new PXHtmlContentElement();
    PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(content, 0, settings);
    WikiArticle result1 = new WikiArticle();
    context.AllowParagraph = false;
    this.Parse(context, result1);
    htmlContentElement.TagName = tagName;
    htmlContentElement.Attributes = attributes;
    htmlContentElement.TagValue = result1.GetAllElements();
    return (PXElement) htmlContentElement;
  }
}
