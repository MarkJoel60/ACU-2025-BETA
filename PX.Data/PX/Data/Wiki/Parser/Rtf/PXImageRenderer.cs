// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXImageRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXImageElement RTF rendering.</summary>
internal class PXImageRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXImageElement e = (PXImageElement) elem;
    LinkInfo imageLink = rtf.Settings.CreateImageLink(e, true);
    if (!imageLink.IsExisting || imageLink.BinData == null)
      return;
    MemoryStream memoryStream = new MemoryStream(imageLink.BinData);
    Image img;
    try
    {
      img = Image.FromStream((Stream) memoryStream);
    }
    catch
    {
      return;
    }
    PXPicture pxPicture = new PXPicture(rtf.Document, img);
    if (e.Width != -1 && e.Width < img.Width)
    {
      pxPicture.ScaleX = pxPicture.ScaleY = e.Width * 100 / img.Width;
      if (e.Height != -1)
        pxPicture.ScaleY = e.Height * 100 / img.Height;
    }
    else if (e.Type == ImageType.Thumb)
      pxPicture.ScaleX = pxPicture.ScaleY = 18000 / img.Width;
    if (e.Location == ImageLocation.None && e.Type == ImageType.None)
    {
      rtf.AddRtfElement((PXRtfElement) pxPicture);
      img.Dispose();
    }
    else
    {
      int num1 = pxPicture.ScaleX > 0 ? pxPicture.ScaleX : 100;
      int num2 = pxPicture.ScaleY > 0 ? pxPicture.ScaleY : 100;
      PXShape pxShape = new PXShape(rtf.Document, pxPicture.Width * num1 / 100, pxPicture.Height * num2 / 100);
      if (e.Type != ImageType.None && e.Location == ImageLocation.Left || e.Location == ImageLocation.Right)
      {
        this.MakeTextBoxShape(pxShape, rtf);
        pxShape.InnerRTF = this.EmbedPicture(e, rtf, pxPicture, pxShape.Width, e.Location, false);
        this.PositionPicture(e.Location, pxShape);
        rtf.AddRtfElement((PXRtfElement) pxShape);
      }
      else if (e.Type == ImageType.None && e.Location == ImageLocation.Left || e.Location == ImageLocation.Right)
      {
        pxShape.Type = ShapeType.PictureFrame;
        pxShape.Properties["pib"] = (object) pxPicture;
        this.PositionPicture(e.Location, pxShape);
        rtf.AddRtfElement((PXRtfElement) pxShape);
      }
      else
        rtf.AddRtfElement(this.EmbedPicture(e, rtf, pxPicture, pxShape.Width, e.Location, true));
      img.Dispose();
    }
  }

  private PXRtfElement EmbedPicture(
    PXImageElement e,
    PXRtfBuilder rtf,
    PXPicture pict,
    int width,
    ImageLocation location,
    bool canBeNested)
  {
    PXRtfElement pxRtfElement = (PXRtfElement) null;
    PXTable pxTable = new PXTable(rtf.Document);
    PXTableRow pxTableRow1 = new PXTableRow(rtf.Document);
    PXTableRow pxTableRow2 = new PXTableRow(rtf.Document);
    PXTableCell pxTableCell1 = new PXTableCell(rtf.Document, width + rtf.CurrentIndent);
    PXTableCell pxTableCell2 = new PXTableCell(rtf.Document, width + rtf.CurrentIndent);
    PXNestedTable pxNestedTable = new PXNestedTable(rtf.Document, width + rtf.CurrentIndent);
    int num1 = pict.ScaleX > 0 ? pict.ScaleX : 100;
    ++rtf.CurrentTableLevel;
    pxTableCell1.Settings.align = pxTableCell2.Settings.align = TextAlign.Center;
    pxTableCell1.Settings.background = new Color?(Color.FromArgb(249, 249, 249));
    pxTableCell1.Settings.left.type = pxTableCell1.Settings.top.type = pxTableCell1.Settings.right.type = BorderType.Single;
    pxTableCell2.Settings.background = new Color?(Color.FromArgb(249, 249, 249));
    pxTableCell2.Settings.left.type = pxTableCell2.Settings.right.type = pxTableCell2.Settings.bottom.type = BorderType.Single;
    pxTableRow1.Settings.CellSpacing = 0;
    pxTableRow1.Settings.Offset = rtf.CurrentIndent;
    pxTableRow1.Settings.Align = location == ImageLocation.Center ? TextAlign.Center : TextAlign.Left;
    pxTableRow1.Settings.paddingLeft = pxTableRow1.Settings.paddingTop = pxTableRow1.Settings.paddingRight = 80 /*0x50*/;
    pxTableRow1.Settings.KeepRow = pxTableRow1.Settings.KeepFollowingRow = true;
    pxTableRow2.Settings.CellSpacing = 0;
    pxTableRow2.Settings.Offset = rtf.CurrentIndent;
    pxTableRow2.Settings.Align = this.GetAlign(location);
    pxTableRow2.Settings.paddingLeft = pxTableRow2.Settings.paddingTop = pxTableRow2.Settings.paddingRight = 80 /*0x50*/;
    pxTableRow2.Settings.KeepRow = pxTableRow2.Settings.KeepFollowingRow = true;
    pxNestedTable.Settings.align = this.GetAlign(e.Location);
    pxTable.Align = this.GetAlign(e.Location);
    int num2 = num1 - (pxTableRow1.Settings.paddingLeft + pxTableRow1.Settings.paddingRight * 2) * 100 / pict.Width;
    switch (e.Type)
    {
      case ImageType.None:
        pxTableCell1.Settings.left.type = pxTableCell1.Settings.top.type = pxTableCell1.Settings.right.type = BorderType.None;
        pxTableCell2.Settings.left.type = pxTableCell2.Settings.right.type = pxTableCell2.Settings.bottom.type = BorderType.None;
        pxTableRow1.Settings.paddingLeft = pxTableRow1.Settings.paddingTop = pxTableRow1.Settings.paddingRight = 0;
        pxTableRow1.Cells.Add(pxTableCell1);
        if (rtf.CurrentTableLevel > 0 & canBeNested)
        {
          pxTableCell1.Children.Add(this.WrapByPar((PXRtfElement) pict, rtf, pxTableCell1.Settings.align));
          pxNestedTable.Level = rtf.CurrentTableLevel;
          pxNestedTable.Rows.Add(pxTableRow1);
          pxRtfElement = (PXRtfElement) pxNestedTable;
          break;
        }
        pxTableCell1.Children.Add((PXRtfElement) pict);
        pxTable.Rows.Add(pxTableRow1);
        pxRtfElement = (PXRtfElement) pxTable;
        break;
      case ImageType.Border:
        pict.ScaleX = num2;
        pict.ScaleY = num2;
        pxTableRow1.Cells.Add(pxTableCell1);
        if (rtf.CurrentTableLevel > 1 & canBeNested)
        {
          pxTableCell1.Children.Add(this.WrapByPar((PXRtfElement) pict, rtf, pxTableCell1.Settings.align));
          pxNestedTable.Level = rtf.CurrentTableLevel;
          pxNestedTable.Rows.Add(pxTableRow1);
          pxRtfElement = (PXRtfElement) pxNestedTable;
          break;
        }
        pxTableCell1.Children.Add((PXRtfElement) pict);
        pxTable.Rows.Add(pxTableRow1);
        pxRtfElement = (PXRtfElement) pxTable;
        break;
      case ImageType.Frame:
      case ImageType.Thumb:
      case ImageType.Popup:
        pict.ScaleX = num2;
        pict.ScaleY = num2;
        pxTableRow1.Cells.Add(pxTableCell1);
        pxTableRow2.Cells.Add(pxTableCell2);
        if (rtf.CurrentTableLevel > 1 & canBeNested)
        {
          pxTableCell1.Children.Add(this.WrapByPar((PXRtfElement) pict, rtf, pxTableCell1.Settings.align));
          pxTableCell2.Children.Add(this.WrapByPar((PXRtfElement) this.GetCaption(e, rtf), rtf, pxTableCell2.Settings.align));
          pxNestedTable.Level = rtf.CurrentTableLevel;
          pxNestedTable.Rows.Add(pxTableRow1);
          pxNestedTable.Rows.Add(pxTableRow2);
          pxRtfElement = (PXRtfElement) pxNestedTable;
          break;
        }
        pxTableCell1.Children.Add((PXRtfElement) pict);
        pxTableCell2.Children.Add(this.WrapByPar((PXRtfElement) this.GetCaption(e, rtf), rtf, pxTableCell2.Settings.align));
        pxTable.Rows.Add(pxTableRow1);
        pxTable.Rows.Add(pxTableRow2);
        pxRtfElement = (PXRtfElement) pxTable;
        break;
    }
    --rtf.CurrentTableLevel;
    return pxRtfElement;
  }

  private void PositionPicture(ImageLocation location, PXShape shape)
  {
    shape.PosV = VerticalAlign.Inside;
    switch (location)
    {
      case ImageLocation.Left:
        shape.PosH = HorizontalAlgn.Left;
        break;
      case ImageLocation.Right:
        shape.PosH = HorizontalAlgn.Right;
        break;
      default:
        shape.Wrap = ShapeWrapType.TopBottom;
        shape.PosH = HorizontalAlgn.Center;
        break;
    }
  }

  private void MakeTextBoxShape(PXShape shape, PXRtfBuilder rtf)
  {
    shape.Type = ShapeType.TextBox;
    shape.PosV = VerticalAlign.Column;
    shape.Properties["fFitShapeToText"] = (object) 1;
    shape.Properties["fAutoTextMargin"] = (object) 1;
    shape.Properties["fLine"] = (object) 0;
    shape.Properties["WrapText"] = (object) 2;
    shape.Properties["fLayoutInCell"] = (object) 1;
    if (shape.Width < rtf.Document.ClientWidth)
      return;
    shape.Properties["dxTextLeft"] = (object) 0;
  }

  private PXRawText GetCaption(PXImageElement e, PXRtfBuilder rtf)
  {
    PXRtfBuilder pxRtfBuilder = new PXRtfBuilder();
    pxRtfBuilder.SetTextSize(pxRtfBuilder.Document.Settings.FontSize);
    pxRtfBuilder.SetTextStyle(TextStyle.Italic);
    pxRtfBuilder.AddString(e.Caption);
    pxRtfBuilder.DisableTextStyle(TextStyle.Italic);
    return new PXRawText(rtf.Document, pxRtfBuilder.Document.Content.ToString());
  }

  private TextAlign GetAlign(ImageLocation location)
  {
    switch (location)
    {
      case ImageLocation.None:
      case ImageLocation.Left:
        return TextAlign.Left;
      case ImageLocation.Right:
        return TextAlign.Right;
      case ImageLocation.Center:
        return TextAlign.Center;
      default:
        return TextAlign.Center;
    }
  }

  private PXRtfElement WrapByPar(PXRtfElement el, PXRtfBuilder rtf, TextAlign align)
  {
    return (PXRtfElement) new PXParagraph(rtf.Document)
    {
      TableLevel = rtf.CurrentTableLevel,
      Align = align,
      EndsWithNewLine = (rtf.CurrentTableLevel <= 1),
      Children = {
        el
      }
    };
  }
}
