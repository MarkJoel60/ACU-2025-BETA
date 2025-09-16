// Decompiled with JetBrains decompiler
// Type: PX.Api.HtmlTableBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace PX.Api;

/// *
public class HtmlTableBuilder
{
  public Table Table;
  public TableRow Row;
  public TableCell Cell;
  public HyperLink Link;

  public void AddRow()
  {
    this.Row = new TableRow();
    this.Table.Rows.Add(this.Row);
  }

  public void AddCell(string text)
  {
    this.Cell = new TableCell()
    {
      Text = HttpUtility.HtmlEncode(text)
    };
    this.Row.Cells.Add(this.Cell);
  }

  public void AddCellHref(string text, string href)
  {
    this.AddCell((string) null);
    this.Link = new HyperLink()
    {
      Text = HttpUtility.HtmlEncode(text),
      NavigateUrl = href
    };
    this.Cell.Controls.Add((Control) this.Link);
  }
}
