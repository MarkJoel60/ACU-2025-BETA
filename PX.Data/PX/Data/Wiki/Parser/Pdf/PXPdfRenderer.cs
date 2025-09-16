// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXPdfRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

public class PXPdfRenderer : PXRenderer<PXPdfRenderer>
{
  private Stream resultStream;
  private PXPdfRenderContext resultPdf;

  public PXPdfRenderer(Stream stream)
  {
    this.resultStream = stream;
    this.resultPdf = new PXPdfRenderContext(stream);
  }

  public PXPdfRenderer(Stream stream, PXWikiParserContext settings)
  {
    this.resultStream = stream;
    this.Context = settings;
    this.resultPdf = new PXPdfRenderContext(stream);
    this.resultPdf.Settings = settings;
  }

  protected PXPdfRenderer()
  {
  }

  public Stream ResultPDFStream => this.resultStream;

  public override void Render(WikiArticle output)
  {
    List<PXElement> allElements = output.GetAllElements();
    this.RegisterCustomRenderers();
    foreach (PXElement el in allElements)
      this.DoRender(el, this.resultPdf);
    this.resultPdf.Close();
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return "Use a MemoryStream object passed to the constructor to get a rendering result.";
  }

  protected virtual void Render(PXElement elem, PXPdfRenderContext resultPdf)
  {
  }

  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXPdfRenderer>.RegisterRenderer(typeof (PXEmptySpace), (PXPdfRenderer) new PXEmptyRenderer());
    PXRenderer<PXPdfRenderer>.RegisterRenderer(typeof (PXTextElement), (PXPdfRenderer) new PXTextRenderer());
    PXRenderer<PXPdfRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXPdfRenderer) new PXStyledTextRenderer());
    PXRenderer<PXPdfRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXPdfRenderer) new PXParagraphRenderer());
    PXRenderer<PXPdfRenderer>.RegisterRenderer(typeof (PXImageElement), (PXPdfRenderer) new PXImageRenderer(this.ResultPDFStream));
  }

  protected void DoRender(PXElement el, PXPdfRenderContext resultPdf)
  {
    PXPdfRenderer renderer = this.GetRenderer(el);
    if (renderer == null)
      return;
    renderer.Render(el, resultPdf);
    resultPdf.SetMargin(renderer.GetFloat(el, resultPdf));
  }

  protected virtual PXMargin GetFloat(PXElement elem, PXPdfRenderContext resultPdf)
  {
    return (PXMargin) null;
  }
}
