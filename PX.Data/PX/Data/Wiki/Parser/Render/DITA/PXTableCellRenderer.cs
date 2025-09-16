// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXTableCellRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXTableCellRenderer : PXditaRenderer
{
  private PXTableCell _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXTableCell) elem;
    PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
    resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableCellDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) tableCellDitaElement);
    if (!string.IsNullOrEmpty(this._e.Props))
    {
      if (this._e.Props.IndexOf("colspan") > -1)
      {
        tableCellDitaElement.AddAttribute("namest", "0");
        int num = (int) resultTxt.Settings.GetExchangedata()["CellsCount"] - 1;
        tableCellDitaElement.AddAttribute("nameend", num.ToString());
      }
      int startIndex = this._e.Props.IndexOf("rowspan");
      if (startIndex > -1)
      {
        int num1 = this._e.Props.IndexOf("\"", startIndex);
        int num2 = this._e.Props.IndexOf("\"", num1 + 1);
        int num3 = (int) Convert.ToInt16(this._e.Props.Substring(num1 + 1, num2 - num1 - 1)) - 1;
        tableCellDitaElement.AddAttribute("morerows", num3.ToString());
      }
    }
    foreach (PXElement child in this._e.Children)
      this.DoRender(child, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
