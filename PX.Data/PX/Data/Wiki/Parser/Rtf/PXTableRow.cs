// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXTableRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXTableRow : PXRtfElement
{
  private PXRowSettings settings;
  private List<PXTableCell> cells = new List<PXTableCell>();

  public PXTableRow(PXDocument document)
    : base(document)
  {
    this.settings = new PXRowSettings();
  }

  public PXTableRow(PXRowSettings settings)
    : base((PXDocument) null)
  {
    this.settings = settings;
  }

  public PXRowSettings Settings => this.settings;

  public List<PXTableCell> Cells => this.cells;

  public override void Render(StringBuilder result)
  {
    List<string> stringList = new List<string>();
    string settings1 = this.GetSettings();
    int num = 0;
    result.AppendLine(settings1);
    result.Append(Environment.NewLine);
    foreach (PXTableCell cell in this.cells)
    {
      string settings2 = cell.GetSettings();
      num += cell.Settings.width;
      string str = $"{settings2}{Environment.NewLine}\\cellx{num.ToString()}";
      stringList.Add(str);
      result.AppendLine(str);
      result.Append(Environment.NewLine);
      result.AppendLine("\\intbl");
      cell.Render(result);
      result.Append(Environment.NewLine);
      result.Append(Environment.NewLine);
      result.AppendLine(settings1);
      result.Append(Environment.NewLine);
      result.Append(str);
      result.AppendLine("\\pard");
      result.AppendLine("\\cell\\pard");
      result.Append(Environment.NewLine);
    }
    result.Append("{");
    result.AppendLine(settings1);
    foreach (string str in stringList)
    {
      result.Append(str);
      result.AppendLine("\\pard");
    }
    result.AppendLine("\\row}");
    result.Append(Environment.NewLine);
  }

  public string GetSettings()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("\\trowd\\trql\\trgaph");
    stringBuilder.Append(this.settings.CellSpacing);
    stringBuilder.Append("\\trrh");
    stringBuilder.Append(this.settings.MinRowHeight);
    stringBuilder.Append("\\trleft");
    stringBuilder.Append(this.settings.Offset);
    stringBuilder.Append("\\tr");
    stringBuilder.Append(PXParagraph.DecryptAlign(this.Settings.Align));
    stringBuilder.Append("\\trautofit1");
    if (this.Settings.KeepRow)
      stringBuilder.Append("\\trkeep");
    if (this.Settings.KeepFollowingRow)
      stringBuilder.Append("\\trkeepfollow");
    if (this.settings.paddingLeft != 0)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\trpaddl");
      stringBuilder.Append(this.settings.paddingLeft);
      stringBuilder.Append("\\trpaddfl3");
    }
    if (this.settings.paddingTop != 0)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\trpaddt");
      stringBuilder.Append(this.settings.paddingTop);
      stringBuilder.Append("\\trpaddft3");
    }
    if (this.settings.paddingRight != 0)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\trpaddr");
      stringBuilder.Append(this.settings.paddingRight);
      stringBuilder.Append("\\trpaddfr3");
    }
    if (this.settings.paddingBottom != 0)
    {
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append("\\trpaddb");
      stringBuilder.Append(this.settings.paddingBottom);
      stringBuilder.Append("\\trpaddfb3");
    }
    stringBuilder.Append(Environment.NewLine);
    return stringBuilder.ToString();
  }
}
