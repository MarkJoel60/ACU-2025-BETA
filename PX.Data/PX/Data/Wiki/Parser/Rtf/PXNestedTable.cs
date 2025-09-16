// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXNestedTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXNestedTable : PXTableCell
{
  private int level = 2;
  private List<PXTableRow> rows = new List<PXTableRow>();

  public PXNestedTable(PXDocument document)
    : base(document)
  {
  }

  public PXNestedTable(PXDocument document, int width)
    : base(document, width)
  {
  }

  public PXNestedTable(PXDocument document, PXCellSettings settings)
    : base(document, settings)
  {
  }

  public int Level
  {
    get => this.level;
    set => this.level = value;
  }

  public List<PXTableRow> Rows => this.rows;

  public override void Render(StringBuilder result)
  {
    for (int index1 = 0; index1 < this.Rows.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.Rows[index1].Cells.Count; ++index2)
      {
        this.Rows[index1].Cells[index2].Render(result);
        result.Append(Environment.NewLine);
        result.Append("\\intbl\\itap");
        result.Append(this.Level);
        result.Append("\\nestcell");
      }
      result.Append(Environment.NewLine);
      result.Append(Environment.NewLine);
      result.AppendLine("{{\\*\\nesttableprops");
      result.AppendLine(this.Rows[index1].GetSettings());
      foreach (PXTableCell cell in this.Rows[index1].Cells)
      {
        result.AppendLine(cell.GetSettings());
        result.Append("\\cellx");
        result.AppendLine(cell.Settings.width.ToString());
      }
      result.AppendLine("\\nestrow}}");
      result.Append(Environment.NewLine);
    }
  }
}
