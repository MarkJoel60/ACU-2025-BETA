// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXSearchRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

public class PXSearchRenderer : PXRenderer<PXSearchRenderer>
{
  private StringBuilder resultHtml = new StringBuilder();
  protected WikiArticle model;

  public PXSearchRenderer()
  {
  }

  public PXSearchRenderer(PXWikiParserContext settings) => this.Context = settings;

  public StringBuilder ResultHtml => this.resultHtml;

  public override void Render(WikiArticle output)
  {
    List<PXElement> allElements = output.GetAllElements();
    this.RegisterCustomRenderers();
    this.resultHtml.Length = 0;
    foreach (PXElement el in allElements)
      this.DoRender(el, this.resultHtml, this.Context);
  }

  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXEmptySpace), (PXSearchRenderer) new PXEmptyRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXTextElement), (PXSearchRenderer) new PXTextRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXSearchRenderer) new PXStyledTextRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXSectionElement), (PXSearchRenderer) new PXSectionRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXHeaderElement), (PXSearchRenderer) new PXHeaderRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXBoxElement), (PXSearchRenderer) new PXBoxRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXCodeBoxElement), (PXSearchRenderer) new PXCodeBoxRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXHorLineElement), (PXSearchRenderer) new PXHorLineRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXHtmlTagElement), (PXSearchRenderer) new PXHtmlTagRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXIndentContainer), (PXSearchRenderer) new PXIndentRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXCommonSpecialTagElement), (PXSearchRenderer) new PXSpecialTagRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXFileListTagElement), (PXSearchRenderer) new PXFileListTagRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXLinkElement), (PXSearchRenderer) new PXLinkRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXImageElement), (PXSearchRenderer) new PXImageRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXTableElement), (PXSearchRenderer) new PXTableRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXTableRow), (PXSearchRenderer) new PXTableRowRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXTableCell), (PXSearchRenderer) new PXTableCellRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (SourceElement), (PXSearchRenderer) new PXSourceRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (NowikiElement), (PXSearchRenderer) new PXNowikiTagRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXSearchRenderer) new PXParagraphRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXHiddenElement), (PXSearchRenderer) new PXHiddenRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXRssLink), (PXSearchRenderer) new PXRssRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXRssArticleLink), (PXSearchRenderer) new PXRssArticleRenderer());
    PXRenderer<PXSearchRenderer>.RegisterRenderer(typeof (PXHtmlContentElement), (PXSearchRenderer) new PXHtmlTagRenderer());
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return parseContext.Settings.IsSimpleRender ? this.resultHtml.ToString() : PXHtmlFormatter.GetResultingPage(this.ResultHtml.ToString(), this.Context, parseContext);
  }

  protected void DoRender(PXElement el, StringBuilder resultHtml, PXWikiParserContext settings)
  {
    this.GetRenderer(el)?.Render(el, resultHtml, settings);
  }

  public virtual void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
  }

  public virtual int lastsymvol(StringBuilder resultHtml)
  {
    int index = resultHtml.Length - 1;
    while (index > 0 && resultHtml[index] == ' ')
      --index;
    return index;
  }
}
