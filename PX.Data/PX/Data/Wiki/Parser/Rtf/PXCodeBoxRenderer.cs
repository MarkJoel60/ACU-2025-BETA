// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXCodeBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXCodeBoxElement rendering.</summary>
internal class PXCodeBoxRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXCodeBoxElement pxCodeBoxElement = (PXCodeBoxElement) elem;
    PXTable elem1 = new PXTable(rtf.Document);
    PXNestedTable elem2 = new PXNestedTable(rtf.Document);
    PXTableRow pxTableRow = new PXTableRow(rtf.Document);
    PXTableCell pxTableCell = new PXTableCell(rtf.Document);
    PXParagraph pxParagraph = new PXParagraph(rtf.Document);
    ++rtf.CurrentTableLevel;
    pxParagraph.TableLevel = rtf.CurrentTableLevel;
    pxTableRow.Settings.Offset = rtf.CurrentIndent;
    pxTableCell.Settings.width = rtf.Document.ClientWidth - rtf.CurrentIndent;
    pxTableCell.Settings.left.type = pxTableCell.Settings.top.type = pxTableCell.Settings.right.type = pxTableCell.Settings.bottom.type = BorderType.Single | BorderType.Dashed;
    pxTableCell.Settings.left.color = pxTableCell.Settings.top.color = pxTableCell.Settings.right.color = pxTableCell.Settings.bottom.color = new Color?(Color.FromArgb(153, 153, 153));
    pxTableCell.Settings.background = new Color?(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 240 /*0xF0*/));
    rtf.AddString(Environment.NewLine);
    rtf.SetTextFont("Courier New");
    foreach (PXElement child in pxCodeBoxElement.Children)
    {
      if (child is PXTextElement)
        pxParagraph.Children.Add((PXRtfElement) new PXText(rtf.Document, ((PXTextElement) child).Value.Trim(), true));
    }
    pxTableCell.Children.Add((PXRtfElement) pxParagraph);
    pxTableRow.Cells.Add(pxTableCell);
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
    rtf.DisableTextFont();
    --rtf.CurrentTableLevel;
  }
}
