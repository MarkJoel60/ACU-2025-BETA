// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXBoxRenderer : PXditaRenderer
{
  private PXBoxElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXBoxElement) elem;
    if (this._e.IsHintBox)
    {
      PXTableDitaElement tableDitaElement = new PXTableDitaElement();
      tableDitaElement.AddAttribute("frame", "all");
      tableDitaElement.AddAttribute("rowsep", "1");
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
      PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
      tgroupDitaElement.AddAttribute("cols", "1");
      tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
      PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
      tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
      PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
      tableRawDitaElement.AddAttribute("rowsep", "1");
      tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
      PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
      tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
      PXNoteDitaElement pxNoteDitaElement = new PXNoteDitaElement();
      pxNoteDitaElement.AddAttribute("type", "tip");
      tableCellDitaElement.AddChild((PXDitaElement) pxNoteDitaElement);
      resultTxt.CurrentParent.Push((PXDitaElement) pxNoteDitaElement);
      foreach (PXElement child in this._e.Children)
        this.DoRender(child, resultTxt);
      resultTxt.CurrentParent.Pop();
    }
    else if (this._e.IsWarnBox)
    {
      PXTableDitaElement tableDitaElement = new PXTableDitaElement();
      tableDitaElement.AddAttribute("frame", "all");
      tableDitaElement.AddAttribute("rowsep", "1");
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
      PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
      tgroupDitaElement.AddAttribute("cols", "1");
      tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
      PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
      tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
      PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
      tableRawDitaElement.AddAttribute("rowsep", "1");
      tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
      PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
      tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
      PXNoteDitaElement pxNoteDitaElement = new PXNoteDitaElement();
      pxNoteDitaElement.AddAttribute("type", "attention");
      tableCellDitaElement.AddChild((PXDitaElement) pxNoteDitaElement);
      resultTxt.CurrentParent.Push((PXDitaElement) pxNoteDitaElement);
      foreach (PXElement child in this._e.Children)
        this.DoRender(child, resultTxt);
      resultTxt.CurrentParent.Pop();
    }
    else if (this._e.IsDangerBox)
    {
      PXTableDitaElement tableDitaElement = new PXTableDitaElement();
      tableDitaElement.AddAttribute("frame", "all");
      tableDitaElement.AddAttribute("rowsep", "1");
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
      PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
      tgroupDitaElement.AddAttribute("cols", "1");
      tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
      PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
      tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
      PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
      tableRawDitaElement.AddAttribute("rowsep", "1");
      tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
      PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
      tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
      PXNoteDitaElement pxNoteDitaElement = new PXNoteDitaElement();
      pxNoteDitaElement.AddAttribute("type", "danger");
      tableCellDitaElement.AddChild((PXDitaElement) pxNoteDitaElement);
      resultTxt.CurrentParent.Push((PXDitaElement) pxNoteDitaElement);
      foreach (PXElement child in this._e.Children)
        this.DoRender(child, resultTxt);
      resultTxt.CurrentParent.Pop();
    }
    else if (this._e.IsGoodPracticeBox)
    {
      PXTableDitaElement tableDitaElement = new PXTableDitaElement();
      tableDitaElement.AddAttribute("frame", "all");
      tableDitaElement.AddAttribute("rowsep", "1");
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
      PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
      tgroupDitaElement.AddAttribute("cols", "1");
      tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
      PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
      tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
      PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
      tableRawDitaElement.AddAttribute("rowsep", "1");
      tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
      PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
      tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
      PXNoteDitaElement pxNoteDitaElement = new PXNoteDitaElement();
      pxNoteDitaElement.AddAttribute("type", "goodpractice");
      tableCellDitaElement.AddChild((PXDitaElement) pxNoteDitaElement);
      resultTxt.CurrentParent.Push((PXDitaElement) pxNoteDitaElement);
      foreach (PXElement child in this._e.Children)
        this.DoRender(child, resultTxt);
      resultTxt.CurrentParent.Pop();
    }
    else
    {
      PXTableDitaElement tableDitaElement = new PXTableDitaElement();
      tableDitaElement.AddAttribute("frame", "all");
      tableDitaElement.AddAttribute("rowsep", "1");
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
      PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
      tgroupDitaElement.AddAttribute("cols", "1");
      tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
      PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
      tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
      PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
      tableRawDitaElement.AddAttribute("rowsep", "1");
      tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
      PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
      tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
      resultTxt.CurrentParent.Push((PXDitaElement) tableCellDitaElement);
      foreach (PXElement child in this._e.Children)
        this.DoRender(child, resultTxt);
      resultTxt.CurrentParent.Pop();
    }
  }
}
