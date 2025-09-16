// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for rendering PXBoxElement to RTF.</summary>
internal class PXBoxRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXBoxElement pxBoxElement = (PXBoxElement) elem;
    PXTable elem1 = new PXTable(rtf.Document);
    PXNestedTable elem2 = new PXNestedTable(rtf.Document);
    PXTableRow pxTableRow = new PXTableRow(rtf.Document);
    PXTableCell pxTableCell1 = new PXTableCell(rtf.Document);
    rtf.AddString(Environment.NewLine);
    ++rtf.CurrentTableLevel;
    pxTableRow.Settings.Offset = rtf.CurrentIndent;
    if (!pxBoxElement.IsHintBox && !pxBoxElement.IsWarnBox && !pxBoxElement.IsDangerBox && !pxBoxElement.IsGoodPracticeBox)
    {
      pxTableCell1.Settings.width = rtf.Document.ClientWidth - rtf.CurrentIndent;
      pxTableCell1.Settings.left.type = pxTableCell1.Settings.top.type = pxTableCell1.Settings.right.type = pxTableCell1.Settings.bottom.type = BorderType.Single;
      pxTableCell1.Settings.left.color = pxTableCell1.Settings.top.color = pxTableCell1.Settings.right.color = pxTableCell1.Settings.bottom.color = new Color?(Color.FromArgb(204, 204, 204));
      pxTableCell1.Settings.background = new Color?(Color.FromArgb(249, 249, 249));
    }
    else
    {
      Image icon = PXLinkRenderer.GetIcon(rtf.Settings.Settings.WarnImageUrl);
      PXTableCell pxTableCell2 = new PXTableCell(rtf.Document);
      PXPicture pxPicture = icon != null ? new PXPicture(rtf.Document, icon) : (PXPicture) null;
      pxTableCell2.Settings.width = 667 + pxTableRow.Settings.Offset;
      pxTableCell2.Settings.left.type = pxTableCell2.Settings.top.type = pxTableCell2.Settings.bottom.type = BorderType.Single;
      pxTableCell2.Settings.left.color = pxTableCell2.Settings.top.color = pxTableCell2.Settings.right.color = pxTableCell2.Settings.bottom.color = new Color?(Color.FromArgb(204, 204, 204));
      pxTableCell2.Settings.background = new Color?(Color.FromArgb(249, 249, 249));
      pxTableCell2.Settings.valign = CellVerticalAlign.Center;
      pxTableCell2.Settings.align = TextAlign.Center;
      pxTableCell1.Settings.width = rtf.Document.ClientWidth - rtf.CurrentIndent;
      pxTableCell1.Settings.top.type = pxTableCell1.Settings.right.type = pxTableCell1.Settings.bottom.type = BorderType.Single;
      pxTableCell1.Settings.top.color = pxTableCell1.Settings.right.color = pxTableCell1.Settings.bottom.color = new Color?(Color.FromArgb(204, 204, 204));
      pxTableCell1.Settings.background = new Color?(Color.FromArgb(249, 249, 249));
      if (pxPicture != null)
        pxTableCell2.Children.Add((PXRtfElement) pxPicture);
      pxTableRow.Cells.Add(pxTableCell2);
    }
    PXRtfBuilder rtf1 = new PXRtfBuilder(rtf.Document.Settings);
    PXElement[] fromParagraphs = PXBoxRenderer.ExtractFromParagraphs(pxBoxElement.Children);
    PXParagraphElement el = (PXParagraphElement) null;
    rtf1.Settings = rtf.Settings;
    rtf1.CurrentTableLevel = rtf.CurrentTableLevel;
    for (int index = 0; index < fromParagraphs.Length; ++index)
    {
      if (fromParagraphs[index] is PXTextElement || fromParagraphs[index] is PXStyledTextElement || fromParagraphs[index] is PXLinkElement)
      {
        el = el == null ? new PXParagraphElement() : el;
        el.AddChild(fromParagraphs[index]);
      }
      else if (el != null)
      {
        this.DoRender((PXElement) el, rtf1);
        el = (PXParagraphElement) null;
        --index;
      }
      else
        this.DoRender(fromParagraphs[index], rtf1);
    }
    if (el != null)
      this.DoRender((PXElement) el, rtf1);
    pxTableCell1.Children.Add((PXRtfElement) new PXRawText(rtf1.Document, rtf1.Document.Content.ToString()));
    pxTableRow.Cells.Add(pxTableCell1);
    if (rtf.CurrentTableLevel > 1)
    {
      elem2.Level = rtf.CurrentTableLevel;
      elem2.Rows.Add(pxTableRow);
      rtf.AddRtfElement((PXRtfElement) elem2);
    }
    else
    {
      elem1.Rows.Add(pxTableRow);
      rtf.AddRtfElement((PXRtfElement) elem1);
    }
    --rtf.CurrentTableLevel;
  }

  public static PXElement[] ExtractFromParagraphs(PXElement[] elems)
  {
    List<PXElement> pxElementList = new List<PXElement>();
    foreach (PXElement elem in elems)
    {
      if (elem is PXParagraphElement)
      {
        foreach (PXElement child in ((PXContainerElement) elem).Children)
          pxElementList.Add(child);
        PXParagraphElement paragraphElement = new PXParagraphElement();
        paragraphElement.AddChild((PXElement) new PXTextElement()
        {
          Value = " "
        });
        pxElementList.Add((PXElement) paragraphElement);
      }
      else
        pxElementList.Add(elem);
    }
    return pxElementList.ToArray();
  }
}
