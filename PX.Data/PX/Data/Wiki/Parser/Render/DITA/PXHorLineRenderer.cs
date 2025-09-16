// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXHorLineRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXHorLineRenderer : PXditaRenderer
{
  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    PXTableDitaElement tableDitaElement = new PXTableDitaElement();
    tableDitaElement.AddAttribute("frame", "bottom");
    tableDitaElement.AddAttribute("rowsep", "1");
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) tableDitaElement);
    PXTableTGroupDitaElement tgroupDitaElement = new PXTableTGroupDitaElement();
    tgroupDitaElement.AddAttribute("cols", "0");
    tableDitaElement.AddChild((PXDitaElement) tgroupDitaElement);
    PXTableTBodyDitaElement tbodyDitaElement = new PXTableTBodyDitaElement();
    tgroupDitaElement.AddChild((PXDitaElement) tbodyDitaElement);
    PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
    tableRawDitaElement.AddAttribute("rowsep", "1");
    tbodyDitaElement.AddChild((PXDitaElement) tableRawDitaElement);
    PXTableCellDitaElement tableCellDitaElement = new PXTableCellDitaElement();
    tableRawDitaElement.AddChild((PXDitaElement) tableCellDitaElement);
  }
}
