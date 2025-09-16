// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXSectionRenderer : PXditaRenderer
{
  private PXSectionElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXSectionElement) elem;
    PXSectionDitaElement sectionDitaElement = new PXSectionDitaElement();
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) sectionDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) sectionDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) sectionDitaElement);
    this.DoRender((PXElement) this._e.Header, resultTxt);
    foreach (PXElement child in this._e.Children)
      this.DoRender(child, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
