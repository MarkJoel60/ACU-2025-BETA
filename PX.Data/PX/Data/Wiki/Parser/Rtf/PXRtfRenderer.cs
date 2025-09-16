// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXRtfRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a base class for all RTF renderers of wiki elements.
/// </summary>
public class PXRtfRenderer : PXRenderer<PXRtfRenderer>
{
  private PXRtfBuilder rtf;

  /// <summary>Initializes a new instance of PXRtfRenderer class.</summary>
  public PXRtfRenderer()
  {
  }

  /// <summary>Initializes a new instance of PXRtfRenderer class.</summary>
  /// <param name="settings">Formatting settings.</param>
  public PXRtfRenderer(PXWikiParserContext settings) => this.Context = settings;

  /// <summary>
  /// Registers custom RTF renderers for PXElement-derived objects.
  /// Override this method to provide rendering of your custom objects.
  /// </summary>
  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXTextElement), (PXRtfRenderer) new PXTextRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXRtfRenderer) new PXStyledTextRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXRtfRenderer) new PXParagraphRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXEmptySpace), (PXRtfRenderer) new PXEmptyRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXHeaderElement), (PXRtfRenderer) new PXHeaderRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXSectionElement), (PXRtfRenderer) new PXSectionRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXBoxElement), (PXRtfRenderer) new PXBoxRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXCodeBoxElement), (PXRtfRenderer) new PXCodeBoxRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXHiddenElement), (PXRtfRenderer) new PXHiddenRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXHorLineElement), (PXRtfRenderer) new PXHorLineRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXHtmlTagElement), (PXRtfRenderer) new PXHtmlTagRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXImageElement), (PXRtfRenderer) new PXImageRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXIndentContainer), (PXRtfRenderer) new PXIndentRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXLinkElement), (PXRtfRenderer) new PXLinkRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (NowikiElement), (PXRtfRenderer) new NowikiTagRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (SourceElement), (PXRtfRenderer) new PXSourceRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXCommonSpecialTagElement), (PXRtfRenderer) new PXSpecialTagRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXTableElement), (PXRtfRenderer) new PXTableRenderer());
    PXRenderer<PXRtfRenderer>.RegisterRenderer(typeof (PXEmbeddedVideoElement), (PXRtfRenderer) new PXEmbeddedVideoRenderer());
  }

  /// <summary>
  /// Performs rendering of the given element if its renderer is available.
  /// </summary>
  /// <param name="el">An element to render.</param>
  /// <param name="resultHtml">A StringBuilder object which will contain rendered result.</param>
  protected void DoRender(PXElement el, PXRtfBuilder rtf) => this.GetRenderer(el)?.Render(el, rtf);

  /// <summary>
  /// Renders specified element. The default implementation does nothing.
  /// Override this method and provide rendering logic in your custom renderers.
  /// </summary>
  /// <param name="elem">An element to render.</param>
  /// <param name="resultHtml">A StringBuilder object which will contain rendered result.</param>
  public virtual void Render(PXElement elem, PXRtfBuilder rtf)
  {
  }

  public override void Render(WikiArticle output)
  {
    List<PXElement> allElements = output.GetAllElements();
    this.RegisterCustomRenderers();
    this.rtf = new PXRtfBuilder();
    this.rtf.Settings = this.Context;
    foreach (PXElement el in allElements)
      this.DoRender(el, this.rtf);
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return this.rtf.Result;
  }
}
