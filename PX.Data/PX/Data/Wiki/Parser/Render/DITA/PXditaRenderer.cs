// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXditaRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

public class PXditaRenderer : PXRenderer<PXditaRenderer>
{
  private readonly PXditaRenderContext _resultTxt;

  public PXditaRenderer() => this._resultTxt = new PXditaRenderContext();

  public PXditaRenderer(PXWikiParserContext settings)
    : this()
  {
    this.Context = settings;
  }

  public override void Render(WikiArticle output)
  {
    List<PXElement> allElements = output.GetAllElements();
    this._resultTxt.Settings = this.Context;
    this.RegisterCustomRenderers();
    foreach (PXElement el in allElements)
      this.DoRender(el, this._resultTxt);
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return (string) null;
  }

  protected virtual void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
  }

  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXTextElement), (PXditaRenderer) new PXTextRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXditaRenderer) new PXStyledTextRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXditaRenderer) new PXParagraphRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXHeaderElement), (PXditaRenderer) new PXHeaderRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXSectionElement), (PXditaRenderer) new PXSectionRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXTableElement), (PXditaRenderer) new PXTableRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXTableRow), (PXditaRenderer) new PXTableRowRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXTableCell), (PXditaRenderer) new PXTableCellRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXHtmlTagElement), (PXditaRenderer) new PXHtmlTagRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXBoxElement), (PXditaRenderer) new PXBoxRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXIndentContainer), (PXditaRenderer) new PXIndentContainerRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXCodeBoxElement), (PXditaRenderer) new PXCodeBoxRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (NowikiElement), (PXditaRenderer) new PXNowikiTagRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXHorLineElement), (PXditaRenderer) new PXHorLineRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXHiddenElement), (PXditaRenderer) new PXHiddenRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXIndentElement), (PXditaRenderer) new PXIndentElementRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXLinkElement), (PXditaRenderer) new PXLinkRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXImageElement), (PXditaRenderer) new PXImageRenderer());
    PXRenderer<PXditaRenderer>.RegisterRenderer(typeof (PXIndentElement), (PXditaRenderer) new PXIndentElementRenderer());
  }

  protected void DoRender(PXElement el, PXditaRenderContext resultTxt)
  {
    this.GetRenderer(el)?.Render(el, resultTxt);
  }
}
