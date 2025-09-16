// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXTableRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXTableRenderer : PXditaRenderer
{
  private PXTableElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXTableElement) elem;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    int num1 = 0;
    PXTableDitaElement tableDitaElement = new PXTableDitaElement();
    tableDitaElement.AddAttribute("frame", "all");
    tableDitaElement.AddAttribute("colsep", "1");
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) tableDitaElement);
    PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
    tgroupDitaElement.AddAttribute("colsep", "1");
    tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
    if (!string.IsNullOrEmpty(this._e.Props))
    {
      int startIndex = this._e.Props.IndexOf("class");
      if (startIndex > -1)
      {
        int num2 = this._e.Props.IndexOf("\"", startIndex);
        int num3 = this._e.Props.IndexOf("\"", num2 + 1);
        if (this._e.Props.Substring(num2 + 1, num3 - num2 - 1) == "checklist")
          tableDitaElement.AddAttribute("rowsep", "1");
        else
          tableDitaElement.AddAttribute("rowsep", "0");
      }
    }
    foreach (PXTableRow row in this._e.Rows)
    {
      if (num1 < row.Cells.Length)
      {
        num1 = row.Cells.Length;
        resultTxt.Settings.GetExchangedata()["CellsCount"] = (object) num1;
        tgroupDitaElement.AddAttribute("cols", num1.ToString());
      }
      if (row.Cells.Length != 0)
      {
        if (row.Cells[0].IsHeader && !flag1)
        {
          if (!flag3)
          {
            for (int index = 0; index < row.Cells.Length; ++index)
            {
              PXTableColSpecDitaElement colSpecDitaElement = new PXTableColSpecDitaElement();
              colSpecDitaElement.AddAttribute("colname", index.ToString());
              tgroupDitaElement.AddChild((PXDitaElement) colSpecDitaElement);
            }
            flag3 = true;
          }
          PXTableTHeadDitaElement theadDitaElement = new PXTableTHeadDitaElement();
          tgroupDitaElement.AddChild((PXDitaElement) theadDitaElement);
          resultTxt.CurrentParent.Push((PXDitaElement) theadDitaElement);
          this.DoRender((PXElement) row, resultTxt);
          resultTxt.CurrentParent.Pop();
          flag1 = true;
        }
        else
        {
          if (!flag2)
          {
            if (!flag3)
            {
              for (int index = 0; index < row.Cells.Length; ++index)
              {
                PXTableColSpecDitaElement colSpecDitaElement = new PXTableColSpecDitaElement();
                colSpecDitaElement.AddAttribute("colname", index.ToString());
                tgroupDitaElement.AddChild((PXDitaElement) colSpecDitaElement);
              }
              flag3 = true;
            }
            PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
            tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
            resultTxt.CurrentParent.Push((PXDitaElement) tbodyDitaElement);
            flag2 = true;
          }
          this.DoRender((PXElement) row, resultTxt);
        }
      }
    }
    if (flag2)
      resultTxt.CurrentParent.Pop();
    resultTxt.CurrentParent.Pop();
  }
}
