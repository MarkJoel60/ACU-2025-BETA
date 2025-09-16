// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXPdfRenderContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Export.Pdf;
using PX.Export.Pdf.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

public class PXPdfRenderContext
{
  private PdfWriter writer;
  private PdfDocument document;
  private PdfPage currPage;
  private Size canvasSize;
  private Point currPos;
  private PXFontInfo fontInfo = new PXFontInfo();
  private PXCurrentMargins margin = new PXCurrentMargins();
  public PXWikiParserContext Settings;

  public PXPdfRenderContext(Stream stream)
  {
    this.writer = new PdfWriter(stream, this.CreatePdfSettings());
    this.document = new PdfDocument();
    ((PdfObject) this.document).Initialize(this.writer);
    this.NewPage();
  }

  public PXFontInfo FontInfo => this.fontInfo;

  public PdfWriter Writer => this.writer;

  public PdfDocument Document => this.document;

  public Size CanvasSize
  {
    get
    {
      return new Size(this.canvasSize.Width - this.margin.GetWidth(this.YPos), this.canvasSize.Height);
    }
  }

  public int XPos
  {
    get => this.currPos.X;
    set => this.currPos.X = value;
  }

  public int YPos
  {
    get => this.currPos.Y;
    set => this.currPos.Y = value;
  }

  public void SetMargin(PXMargin m)
  {
    if (m == null)
      return;
    this.margin.Add(m);
  }

  public void MoveToRight() => this.MoveToX(this.CanvasSize.Width);

  public void MoveToLeft() => this.MoveToX(0);

  public void MoveToCenter() => this.MoveToX(this.CanvasSize.Width / 2);

  public void MoveToEndOfMargin()
  {
    while (this.IsMargined.Count != 0)
      this.NewLine();
  }

  public void MoveToEndOfMargin(ElementFloat marginType)
  {
    while (this.IsMargined.Contains(marginType))
      this.NewLine();
  }

  public List<ElementFloat> IsMargined
  {
    get
    {
      List<ElementFloat> isMargined = new List<ElementFloat>();
      foreach (PXMargin pxMargin in (List<PXMargin>) this.margin)
      {
        if (pxMargin.Contains(this.YPos))
          isMargined.Add(pxMargin.Float);
      }
      return isMargined;
    }
  }

  public bool IsFit(int width)
  {
    PXMargin pxMargin = this.margin[ElementFloat.Left, this.YPos];
    return pxMargin != null ? this.XPos - pxMargin.Width + width < this.CanvasSize.Width : this.XPos + width < this.CanvasSize.Width;
  }

  public void NewLine()
  {
    this.YPos = this.YPos + (int) this.FontInfo.Size + 2;
    if (this.margin.Count == 0)
    {
      this.XPos = 0;
    }
    else
    {
      PXMargin pxMargin = this.margin[ElementFloat.Left, this.YPos];
      this.XPos = pxMargin == null ? 0 : pxMargin.Width;
    }
    if (this.YPos + (int) this.FontInfo.Size <= this.CanvasSize.Height && this.YPos != -1)
      return;
    this.NewPage();
  }

  public void NewPage()
  {
    if (this.currPage != null)
      ((PdfObject) this.currPage).Write();
    this.currPage = this.document.AddPage();
    this.currPage.Width = 595;
    this.currPage.Height = 842;
    this.currPage.MarginsLeft = 10;
    this.currPage.MarginsTop = 10;
    this.currPage.MarginsBottom = 10;
    this.currPage.MarginsRight = 10;
    ((PdfObject) this.currPage).Initialize(this.writer);
    this.canvasSize.Width = this.currPage.Width - this.currPage.MarginsLeft - this.currPage.MarginsRight;
    this.canvasSize.Height = this.currPage.Height - this.currPage.MarginsTop - this.currPage.MarginsBottom;
    this.currPos.X = this.currPos.Y = 0;
    this.margin.Clear();
  }

  public void Close()
  {
    ((PdfObject) this.currPage).Write();
    ((PdfObject) this.document).Write();
  }

  private void MoveToX(int x)
  {
    this.XPos = x;
    if (this.margin.Count == 0)
      return;
    PXMargin pxMargin = this.margin[ElementFloat.Left, this.YPos];
    if (pxMargin != null)
      this.XPos = x + pxMargin.Width;
    else
      this.XPos = x;
  }

  private PdfSettings CreatePdfSettings()
  {
    return new PdfSettings()
    {
      Creator = "Acumatica",
      IsCompressed = false,
      Producer = "Acumatica"
    };
  }
}
