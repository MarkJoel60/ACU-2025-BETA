// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXParagraphRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXParagraphRenderer : PXditaRenderer
{
  private PXParagraphElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXParagraphElement) elem;
    PXParagraphDitaElement paragraphDitaElement = new PXParagraphDitaElement();
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) paragraphDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) paragraphDitaElement);
    resultTxt.CurrentParent.Push((PXDitaElement) paragraphDitaElement);
    foreach (PXElement child in this._e.Children)
      this.DoRender(child, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
