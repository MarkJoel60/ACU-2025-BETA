// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.HtmlTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class HtmlTagProcessor : PXBlockParser, PXHtmlParser.TagProcessor
{
  private static readonly HtmlTagProcessor instance = new HtmlTagProcessor();

  public static HtmlTagProcessor Instance => HtmlTagProcessor.instance;

  protected HtmlTagProcessor()
  {
  }

  public virtual PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    PXHtmlTagElement pxHtmlTagElement = new PXHtmlTagElement();
    PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(content, 0, settings);
    WikiArticle result1 = new WikiArticle();
    this.Parse(context, result1);
    pxHtmlTagElement.TagName = tagName;
    pxHtmlTagElement.Attributes = attributes;
    pxHtmlTagElement.TagValue = result1.GetAllElements();
    result.TocItems.AddRange((IEnumerable<TOCItem>) result1.TocItems);
    return (PXElement) pxHtmlTagElement;
  }

  protected override void AddText(
    string text,
    PXBlockParser.ParseContext context,
    WikiArticle result)
  {
    this.AddTextNoParagraph(text, result, true);
  }
}
