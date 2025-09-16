// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXTableRowRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXTableRowRenderer : PXditaRenderer
{
  private PXTableRow _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXTableRow) elem;
    if (this._e.Cells.Length == 0)
      return;
    PXTableRawDitaElement tableRawDitaElement = new PXTableRawDitaElement();
    resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) tableRawDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) tableRawDitaElement);
    foreach (PXElement cell in this._e.Cells)
      this.DoRender(cell, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
