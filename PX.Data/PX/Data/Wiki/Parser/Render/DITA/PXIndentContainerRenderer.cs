// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXIndentContainerRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXIndentContainerRenderer : PXditaRenderer
{
  private PXIndentContainer _e;
  private int first;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXIndentContainer) elem;
    this.first = 0;
    switch (this._e.Type)
    {
      case '#':
        PXOlDitaElement pxOlDitaElement = new PXOlDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxOlDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxOlDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) pxOlDitaElement);
        foreach (PXElement child in this._e.Children)
        {
          if (child is PXIndentElement)
          {
            if (this.first != 0)
            {
              resultTxt.CurrentParent.Pop();
              --this.first;
            }
            PXLiDitaElement pxLiDitaElement = new PXLiDitaElement();
            if (resultTxt.CurrentParent.Count != 0)
              resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxLiDitaElement);
            else
              resultTxt.CurrentTopic.AddElement((PXDitaElement) pxLiDitaElement);
            resultTxt.CurrentParent.Push((PXDitaElement) pxLiDitaElement);
            this.DoRender(child, resultTxt);
            ++this.first;
          }
          if (child is PXIndentContainer)
          {
            this.DoRender(child, resultTxt);
            ++this.first;
          }
        }
        for (; this.first != 0; --this.first)
          resultTxt.CurrentParent.Pop();
        resultTxt.CurrentParent.Pop();
        break;
      case '*':
        PXUlDitaElement pxUlDitaElement = new PXUlDitaElement();
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxUlDitaElement);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxUlDitaElement);
        resultTxt.CurrentParent.Push((PXDitaElement) pxUlDitaElement);
        foreach (PXElement child in this._e.Children)
        {
          if (child is PXIndentElement)
          {
            if (this.first != 0)
            {
              resultTxt.CurrentParent.Pop();
              --this.first;
            }
            PXLiDitaElement pxLiDitaElement = new PXLiDitaElement();
            if (resultTxt.CurrentParent.Count != 0)
              resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxLiDitaElement);
            else
              resultTxt.CurrentTopic.AddElement((PXDitaElement) pxLiDitaElement);
            resultTxt.CurrentParent.Push((PXDitaElement) pxLiDitaElement);
            this.DoRender(child, resultTxt);
            ++this.first;
          }
          if (child is PXIndentContainer)
          {
            this.DoRender(child, resultTxt);
            ++this.first;
          }
        }
        for (; this.first != 0; --this.first)
          resultTxt.CurrentParent.Pop();
        resultTxt.CurrentParent.Pop();
        break;
      case ':':
        foreach (PXElement child in this._e.Children)
        {
          PXParagraphDitaElement paragraphDitaElement = new PXParagraphDitaElement();
          if (resultTxt.CurrentParent.Count != 0)
            resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) paragraphDitaElement);
          else
            resultTxt.CurrentTopic.AddElement((PXDitaElement) paragraphDitaElement);
          resultTxt.CurrentParent.Push((PXDitaElement) paragraphDitaElement);
          this.DoRender(child, resultTxt);
          resultTxt.CurrentParent.Pop();
        }
        break;
    }
  }
}
