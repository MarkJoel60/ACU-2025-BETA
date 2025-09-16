// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXTxtRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

public class PXTxtRenderer : PXRenderer<PXTxtRenderer>
{
  private PXTxtRenderContext resultTxt;

  public PXTxtRenderer()
  {
    this.resultTxt = new PXTxtRenderContext();
    this.ColsCount = 80 /*0x50*/;
  }

  public PXTxtRenderer(PXWikiParserContext settings)
    : this()
  {
    this.Context = settings;
  }

  public int ColsCount
  {
    get => this.resultTxt.ColsCount;
    set => this.resultTxt.ColsCount = value;
  }

  public string Text => this.resultTxt.Result.ToString();

  public override void Render(WikiArticle output)
  {
    List<PXElement> allElements = output.GetAllElements();
    this.resultTxt.Settings = this.Context;
    this.RegisterCustomRenderers();
    foreach (PXElement el in allElements)
      this.DoRender(el, this.resultTxt);
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return this.resultTxt.Result.ToString();
  }

  protected virtual void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
  }

  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXEmptySpace), (PXTxtRenderer) new PXEmptyRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXTxtRenderer) new PXStyledTextRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXTextElement), (PXTxtRenderer) new PXTextRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXTxtRenderer) new PXParagraphRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXHeaderElement), (PXTxtRenderer) new PXHeaderRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXSectionElement), (PXTxtRenderer) new PXSectionRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXHorLineElement), (PXTxtRenderer) new PXEmptyRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXBoxElement), (PXTxtRenderer) new PXBoxRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXCodeBoxElement), (PXTxtRenderer) new PXBoxRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXIndentContainer), (PXTxtRenderer) new PXIndentRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXLinkElement), (PXTxtRenderer) new PXTxtLinkRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (SourceElement), (PXTxtRenderer) new PXBoxRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXCommonSpecialTagElement), (PXTxtRenderer) new PXSpecialTagRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (PXHtmlTagElement), (PXTxtRenderer) new PXHtmlTagRenderer());
    PXRenderer<PXTxtRenderer>.RegisterRenderer(typeof (NowikiElement), (PXTxtRenderer) new PXNowikiTagRenderer());
  }

  protected void DoRender(PXElement el, PXTxtRenderContext resultTxt)
  {
    this.GetRenderer(el)?.Render(el, resultTxt);
  }
}
