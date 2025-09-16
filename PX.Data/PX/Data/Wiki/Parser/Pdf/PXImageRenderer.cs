// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXImageRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

/// <summary>Initializes a new instance of PXImageRenderer class.</summary>
internal class PXImageRenderer(Stream stream) : PXPdfRenderer(stream)
{
  private Size imSize;
  private int x;
  private int y;

  protected override void Render(PXElement elem, PXPdfRenderContext resultPdf)
  {
    PXImageElement e = (PXImageElement) elem;
    LinkInfo imageLink = resultPdf.Settings.CreateImageLink(e, true);
    this.x = this.y = -1;
    this.imSize = new Size(0, 0);
    if (imageLink.IsInvalid || imageLink.BinData == null)
      return;
    Image imageFromArray = PX.Common.Drawing.CreateImageFromArray(imageLink.BinData);
    if (imageFromArray == null)
      return;
    this.imSize = this.ScaleImage(e, resultPdf, imageFromArray);
    this.x = resultPdf.XPos;
    this.y = resultPdf.YPos;
    if (e.Location != ImageLocation.None)
      resultPdf.NewLine();
    while ((this.imSize.Width > resultPdf.CanvasSize.Width || this.imSize.Height > resultPdf.CanvasSize.Height) && resultPdf.IsMargined.Count != 0)
      resultPdf.NewLine();
    if (resultPdf.IsMargined.Contains(ElementFloat.None))
      resultPdf.MoveToEndOfMargin(ElementFloat.None);
    if (e.Location == ImageLocation.Right)
    {
      if (resultPdf.IsMargined.Contains(ElementFloat.Right) || resultPdf.IsMargined.Contains(ElementFloat.None))
        resultPdf.MoveToEndOfMargin(ElementFloat.Right);
      resultPdf.MoveToRight();
      resultPdf.XPos -= this.imSize.Width;
    }
    else if (e.Location == ImageLocation.Center)
    {
      resultPdf.MoveToCenter();
      resultPdf.XPos -= this.imSize.Width / 2;
      this.x = this.y = -1;
    }
    else if (e.Location == ImageLocation.Left)
    {
      if (resultPdf.IsMargined.Contains(ElementFloat.Left) || resultPdf.IsMargined.Contains(ElementFloat.None))
        resultPdf.MoveToEndOfMargin(ElementFloat.Left);
      resultPdf.MoveToLeft();
    }
    resultPdf.Writer.CurrentStream.DrawImage(resultPdf.XPos, resultPdf.YPos, this.imSize.Width, this.imSize.Height, imageLink.Url, imageFromArray, Color.White);
  }

  protected override PXMargin GetFloat(PXElement elem, PXPdfRenderContext resultPdf)
  {
    PXImageElement pxImageElement = (PXImageElement) elem;
    PXMargin pxMargin = (PXMargin) null;
    switch (pxImageElement.Location)
    {
      case ImageLocation.Left:
        ElementFloat floating1 = ElementFloat.Left;
        pxMargin = new PXMargin(resultPdf.YPos, this.imSize.Width, this.imSize.Height, floating1);
        break;
      case ImageLocation.Right:
        ElementFloat floating2 = ElementFloat.Right;
        pxMargin = new PXMargin(resultPdf.YPos, this.imSize.Width, this.imSize.Height, floating2);
        break;
      case ImageLocation.Center:
        ElementFloat floating3 = ElementFloat.None;
        pxMargin = new PXMargin(resultPdf.YPos, resultPdf.CanvasSize.Width - resultPdf.XPos, this.imSize.Height, floating3);
        resultPdf.YPos += this.imSize.Height;
        resultPdf.MoveToRight();
        break;
    }
    if (this.x != -1 && this.y != -1 && pxImageElement.Location == ImageLocation.Left)
    {
      if (this.x < resultPdf.XPos + this.imSize.Width)
      {
        this.x = resultPdf.XPos + this.imSize.Width;
        this.y = resultPdf.YPos;
      }
      resultPdf.XPos = this.x;
      resultPdf.YPos = this.y;
    }
    else if (this.x != -1 && this.y != -1 && pxImageElement.Location == ImageLocation.Right)
    {
      resultPdf.XPos = this.x;
      resultPdf.YPos = this.y;
    }
    else if (pxImageElement.Location == ImageLocation.None)
      resultPdf.XPos += this.imSize.Width;
    return pxMargin;
  }

  private Size ScaleImage(PXImageElement e, PXPdfRenderContext resultPdf, Image im)
  {
    if (e.Width > 0)
      im = PX.Common.Drawing.ScaleImage(im, e.Width, e.Height);
    return new Size(im.Width, im.Height);
  }
}
