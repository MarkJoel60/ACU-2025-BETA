// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXSourceRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

internal class PXSourceRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    SourceElement sourceElement = (SourceElement) elem;
    SourceElement.DiffState diffState = SourceElement.DiffState.NoChange;
    PXRtfBuilder pxRtfBuilder = new PXRtfBuilder(rtf.Document.Settings);
    PXTable elem1 = new PXTable(rtf.Document);
    PXNestedTable elem2 = new PXNestedTable(rtf.Document);
    PXTableRow pxTableRow = new PXTableRow(rtf.Document);
    PXTableCell pxTableCell = new PXTableCell(rtf.Document);
    PXParagraph pxParagraph = new PXParagraph(rtf.Document);
    ++rtf.CurrentTableLevel;
    pxRtfBuilder.Settings = rtf.Settings;
    pxRtfBuilder.CurrentTableLevel = rtf.CurrentTableLevel;
    pxTableRow.Settings.Offset = rtf.CurrentIndent;
    pxTableCell.Settings.width = rtf.Document.ClientWidth;
    pxTableCell.Settings.left.type = pxTableCell.Settings.top.type = pxTableCell.Settings.right.type = pxTableCell.Settings.bottom.type = BorderType.Single | BorderType.Dashed;
    pxTableCell.Settings.left.color = pxTableCell.Settings.top.color = pxTableCell.Settings.right.color = pxTableCell.Settings.bottom.color = new Color?(Color.FromArgb(204, 204, 204));
    pxTableCell.Settings.background = new Color?(Color.FromArgb(240 /*0xF0*/, 240 /*0xF0*/, (int) byte.MaxValue));
    rtf.AddString(Environment.NewLine);
    rtf.SetTextFont("Courier New");
    foreach (SourceElement.SourcePart sourcePart in sourceElement.Source)
    {
      if (sourcePart.DiffState != diffState)
      {
        if (diffState != SourceElement.DiffState.NoChange)
          pxRtfBuilder.DisableHighlight();
        diffState = sourcePart.DiffState;
        if (diffState == SourceElement.DiffState.Added)
          pxRtfBuilder.SetHighlightColor(Color.FromArgb(152, 251, 152));
        else if (diffState == SourceElement.DiffState.Removed)
          pxRtfBuilder.SetHighlightColor(Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 203));
      }
      Color? styleOfSyntax = this.GetStyleOfSyntax(sourcePart.Syntax);
      if (styleOfSyntax.HasValue)
        pxRtfBuilder.SetTextColor(styleOfSyntax.Value);
      pxRtfBuilder.AddString(sourcePart.Value, true);
      if (styleOfSyntax.HasValue)
        pxRtfBuilder.DisableColor();
    }
    if (diffState != SourceElement.DiffState.NoChange)
      pxRtfBuilder.DisableHighlight();
    pxParagraph.Children.Add((PXRtfElement) new PXRawText(rtf.Document, pxRtfBuilder.Document.Content.ToString()));
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

  private Color? GetStyleOfSyntax(SourceElement.SyntaxType s)
  {
    switch (s)
    {
      case SourceElement.SyntaxType.Bracket:
        return new Color?(Color.FromArgb(0, 100, 0));
      case SourceElement.SyntaxType.Comment:
        return new Color?(Color.FromArgb(0, 128 /*0x80*/, 0));
      case SourceElement.SyntaxType.StringLiteral:
        return new Color?(Color.FromArgb(165, 42, 42));
      case SourceElement.SyntaxType.Number:
        return new Color?(Color.FromArgb(165, 42, 42));
      case SourceElement.SyntaxType.Keyword:
        return new Color?(Color.Blue);
      default:
        return new Color?();
    }
  }
}
