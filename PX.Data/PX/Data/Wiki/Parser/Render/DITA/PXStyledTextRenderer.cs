// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXStyledTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXStyledTextRenderer : PXditaRenderer
{
  private PXStyledTextElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXStyledTextElement) elem;
    switch (this._e.Style)
    {
      case TextStyle.Bold:
        PXBoldDitaElement pxBoldDitaElement = new PXBoldDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxBoldDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxBoldDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) pxBoldDitaElement);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
      case TextStyle.Italic:
        PXItalicDitaElement italicDitaElement1 = new PXItalicDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) italicDitaElement1);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) italicDitaElement1);
        resultTxt.CurrentParent.Push((PXDitaElement) italicDitaElement1);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
      case TextStyle.Bold | TextStyle.Italic:
        PXBoldAndItalicDitaElement italicDitaElement2 = new PXBoldAndItalicDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) italicDitaElement2);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) italicDitaElement2);
        resultTxt.CurrentParent.Push((PXDitaElement) italicDitaElement2);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
      case TextStyle.Underlined:
        PXUnderlinedDitaElement underlinedDitaElement = new PXUnderlinedDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) underlinedDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) underlinedDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) underlinedDitaElement);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
      case TextStyle.Striked:
        PXStrikedDitaElement strikedDitaElement = new PXStrikedDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) strikedDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) strikedDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) strikedDitaElement);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
      case TextStyle.Monotype:
        PXttDitaElement pxttDitaElement = new PXttDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxttDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxttDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) pxttDitaElement);
        foreach (PXElement child in this._e.Children)
          this.DoRender(child, resultTxt);
        resultTxt.CurrentParent.Pop();
        break;
    }
  }
}
