// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXTable(PXDocument document) : PXRtfElement(document)
{
  private List<PXTableRow> rows = new List<PXTableRow>();
  public TextAlign Align;

  public List<PXTableRow> Rows => this.rows;

  public override void Render(StringBuilder result)
  {
    result.AppendLine("{\\par\\pard");
    foreach (PXRtfElement row in this.rows)
    {
      row.Render(result);
      result.Append(Environment.NewLine);
    }
    result.AppendLine("\\par}");
  }
}
