// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXTableRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXTableElement RTF rendering.</summary>
internal class PXTableRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXTableElement pxTableElement = (PXTableElement) elem;
    PXTable elem1 = new PXTable(rtf.Document);
    PXNestedTable elem2 = new PXNestedTable(rtf.Document);
    PXTableRenderer.TableContext context = new PXTableRenderer.TableContext()
    {
      Rtf = rtf
    };
    ++rtf.CurrentTableLevel;
    elem2.Level = rtf.CurrentTableLevel;
    if (!string.IsNullOrEmpty(pxTableElement.Caption))
      rtf.AddString(Environment.NewLine + pxTableElement.Caption, true);
    rtf.AddString(Environment.NewLine);
    for (int index = 0; index < pxTableElement.Rows.Length; ++index)
    {
      context.CurRow = index;
      PXTableRow pxTableRow = this.RenderRow(pxTableElement.Rows[index], context);
      if (rtf.CurrentTableLevel > 1)
        elem2.Rows.Add(pxTableRow);
      else
        elem1.Rows.Add(pxTableRow);
    }
    if (rtf.CurrentTableLevel > 1)
      rtf.AddRtfElement((PXRtfElement) elem2);
    else
      rtf.AddRtfElement((PXRtfElement) elem1);
    --rtf.CurrentTableLevel;
  }

  private PXTableRow RenderRow(PX.Data.Wiki.Parser.PXTableRow row, PXTableRenderer.TableContext context)
  {
    PXTableRow row1 = new PXTableRow(context.Rtf.Document);
    PX.Data.Wiki.Parser.PXTableCell[] cells = row.Cells;
    for (int i = 0; i < cells.Length; ++i)
    {
      PXTableCell cell = this.RenderCell(cells[i], context);
      cell.Settings.width = (context.Rtf.Document.ClientWidth - context.Rtf.CurrentIndent) / cells.Length;
      this.HandleRowSpan(cells, i, cell, context, row1);
      row1.Cells.Add(cell);
    }
    row1.Settings.Offset = context.Rtf.CurrentIndent;
    return row1;
  }

  private PXTableCell RenderCell(PX.Data.Wiki.Parser.PXTableCell cell, PXTableRenderer.TableContext context)
  {
    PXTableCell pxTableCell = new PXTableCell(context.Rtf.Document);
    PXRtfBuilder rtf = new PXRtfBuilder(context.Rtf.Document.Settings);
    PXElement[] fromParagraphs = PXBoxRenderer.ExtractFromParagraphs(cell.Children);
    PXParagraphElement el = (PXParagraphElement) null;
    rtf.Settings = context.Rtf.Settings;
    rtf.CurrentTableLevel = context.Rtf.CurrentTableLevel;
    for (int index = 0; index < fromParagraphs.Length; ++index)
    {
      if (fromParagraphs[index] is PXTextElement || fromParagraphs[index] is PXStyledTextElement || fromParagraphs[index] is PXLinkElement)
      {
        el = el == null ? new PXParagraphElement() : el;
        el.AddChild(fromParagraphs[index]);
      }
      else if (el != null)
      {
        this.DoRender((PXElement) el, rtf);
        el = (PXParagraphElement) null;
        --index;
      }
      else
        this.DoRender(fromParagraphs[index], rtf);
    }
    if (el != null)
      this.DoRender((PXElement) el, rtf);
    pxTableCell.Children.Add((PXRtfElement) new PXRawText(context.Rtf.Document, rtf.Document.Content.ToString()));
    return pxTableCell;
  }

  private void HandleRowSpan(
    PX.Data.Wiki.Parser.PXTableCell[] cells,
    int i,
    PXTableCell cell,
    PXTableRenderer.TableContext context,
    PXTableRow row)
  {
    if (context.RowSpans.ContainsKey(i) && context.RowSpans[i].Spacing >= context.CurRow - context.RowSpans[i].RowNum + 1)
    {
      PXTableCell pxTableCell = new PXTableCell(context.Rtf.Document);
      pxTableCell.Settings.width = (context.Rtf.Document.ClientWidth - context.Rtf.CurrentIndent) / (cells.Length + context.RowSpans.Count);
      pxTableCell.VMerge = true;
      row.Cells.Add(pxTableCell);
      cell.Settings.width = pxTableCell.Settings.width;
      if (context.RowSpans[i].Spacing != context.CurRow - context.RowSpans[i].RowNum + 1)
        return;
      context.RowSpans.Remove(i);
    }
    else
    {
      int rowSpan = this.GetRowSpan(cells[i]);
      if (rowSpan <= 1)
        return;
      cell.FirstVMerge = true;
      context.RowSpans.Add(i, new PXTableRenderer.RowSpan()
      {
        CellNum = i,
        RowNum = context.CurRow,
        Spacing = rowSpan
      });
    }
  }

  private int GetRowSpan(PX.Data.Wiki.Parser.PXTableCell c)
  {
    if (c.Attributes == null || !c.Attributes.ContainsKey("rowspan"))
      return 0;
    int result;
    int.TryParse(c.Attributes["rowspan"], out result);
    return result;
  }

  private class TableContext
  {
    public PXRtfBuilder Rtf;
    public int CurRow;
    public Dictionary<int, PXTableRenderer.RowSpan> RowSpans = new Dictionary<int, PXTableRenderer.RowSpan>();
  }

  private class RowSpan
  {
    public int CellNum;
    public int RowNum;
    public int Spacing;
  }
}
