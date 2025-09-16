// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXTableCell
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXTableCell : PXRtfElement
{
  private PXCellSettings settings;
  private List<PXRtfElement> children = new List<PXRtfElement>();
  public bool FirstVMerge;
  public bool VMerge;

  public PXTableCell(PXDocument document)
    : base(document)
  {
    this.settings = new PXCellSettings();
  }

  public PXTableCell(PXDocument document, int width)
    : base(document)
  {
    this.settings = new PXCellSettings();
    this.settings.width = width;
    this.settings.align = TextAlign.Left;
    this.settings.valign = CellVerticalAlign.Top;
    this.settings.left.type = this.settings.top.type = BorderType.None;
    this.settings.right.type = this.settings.bottom.type = BorderType.None;
  }

  public PXTableCell(PXDocument document, PXCellSettings settings)
    : base(document)
  {
    this.settings = settings;
  }

  public PXCellSettings Settings => this.settings;

  public List<PXRtfElement> Children => this.children;

  public override void Render(StringBuilder result)
  {
    foreach (PXRtfElement child in this.children)
      child.Render(result);
  }

  public string GetSettings()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(this.DecryptVerticalCellAlign(this.settings.valign));
    stringBuilder.Append(this.DecryptCellAlign(this.settings.align));
    if (this.FirstVMerge)
      stringBuilder.Append("\\clvmgf");
    else if (this.VMerge)
      stringBuilder.Append("\\clvmrg");
    if (this.settings.background.HasValue)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\clcbpat");
      stringBuilder.Append(this.document.GetColorCode(this.settings.background.Value));
    }
    if (this.settings.top.type != BorderType.None)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\clbrdrt");
      stringBuilder.Append(this.settings.top.ToString());
      if (this.settings.top.color.HasValue)
      {
        stringBuilder.Append("\\brdrcf");
        stringBuilder.Append(this.document.GetColorCode(this.settings.top.color.Value));
      }
    }
    if (this.settings.left.type != BorderType.None)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\clbrdrl");
      stringBuilder.Append(this.settings.left.ToString());
      if (this.settings.left.color.HasValue)
      {
        stringBuilder.Append("\\brdrcf");
        stringBuilder.Append(this.document.GetColorCode(this.settings.left.color.Value));
      }
    }
    if (this.settings.right.type != BorderType.None)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\clbrdrr");
      stringBuilder.Append(this.settings.right.ToString());
      if (this.settings.right.color.HasValue)
      {
        stringBuilder.Append("\\brdrcf");
        stringBuilder.Append(this.document.GetColorCode(this.settings.right.color.Value));
      }
    }
    if (this.settings.bottom.type != BorderType.None)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\clbrdrb");
      stringBuilder.Append(this.settings.bottom.ToString());
      if (this.settings.bottom.color.HasValue)
      {
        stringBuilder.Append("\\brdrcf");
        stringBuilder.Append(this.document.GetColorCode(this.settings.bottom.color.Value));
      }
    }
    return stringBuilder.ToString();
  }

  private string DecryptVerticalCellAlign(CellVerticalAlign align)
  {
    switch (align)
    {
      case CellVerticalAlign.Top:
        return "\\clvertalt";
      case CellVerticalAlign.Center:
        return "\\clvertalc";
      case CellVerticalAlign.Bottom:
        return "\\clvertalb";
      default:
        return "";
    }
  }

  private string DecryptCellAlign(TextAlign align)
  {
    switch (align)
    {
      case TextAlign.Left:
        return "\\ql";
      case TextAlign.Center:
        return "\\qc";
      case TextAlign.Right:
        return "\\qr";
      case TextAlign.Justify:
        return "\\qj";
      default:
        return "";
    }
  }
}
