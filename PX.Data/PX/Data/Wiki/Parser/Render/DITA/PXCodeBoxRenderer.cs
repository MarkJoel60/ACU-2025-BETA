// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXCodeBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXCodeBoxRenderer : PXditaRenderer
{
  private PXCodeBoxElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXCodeBoxElement) elem;
    PXPreDitaElement pxPreDitaElement = new PXPreDitaElement();
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxPreDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) pxPreDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) pxPreDitaElement);
    foreach (PXElement child in this._e.Children)
      this.DoRender(child, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
