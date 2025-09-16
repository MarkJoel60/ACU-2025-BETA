// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXHtmlTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXHtmlTagRenderer : PXditaRenderer
{
  private PXHtmlTagElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXHtmlTagElement) elem;
    string tagName = this._e.TagName;
    if (tagName == null)
      return;
    PXDitaElement pxDitaElement;
    switch (tagName.Length)
    {
      case 2:
        if (!(tagName == "tt"))
          return;
        pxDitaElement = (PXDitaElement) new PXttDitaElement();
        break;
      case 3:
        switch (tagName[2])
        {
          case 'b':
            if (!(tagName == "sub"))
              return;
            pxDitaElement = (PXDitaElement) new PXSubDitaElement();
            break;
          case 'g':
            if (!(tagName == "big"))
              return;
            using (List<PXElement>.Enumerator enumerator = this._e.TagValue.GetEnumerator())
            {
              while (enumerator.MoveNext())
                this.DoRender(enumerator.Current, resultTxt);
              return;
            }
          case 'p':
            if (!(tagName == "sup"))
              return;
            pxDitaElement = (PXDitaElement) new PXSupDitaElement();
            break;
          case 'v':
            if (!(tagName == "div"))
              return;
            using (List<PXElement>.Enumerator enumerator = this._e.TagValue.GetEnumerator())
            {
              while (enumerator.MoveNext())
                this.DoRender(enumerator.Current, resultTxt);
              return;
            }
          default:
            return;
        }
        break;
      case 4:
        if (!(tagName == "span"))
          return;
        using (List<PXElement>.Enumerator enumerator = this._e.TagValue.GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.DoRender(enumerator.Current, resultTxt);
          return;
        }
      case 5:
        if (!(tagName == "small"))
          return;
        using (List<PXElement>.Enumerator enumerator = this._e.TagValue.GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.DoRender(enumerator.Current, resultTxt);
          return;
        }
      default:
        return;
    }
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild(pxDitaElement);
    else
      resultTxt.CurrentTopic.AddElement(pxDitaElement);
    resultTxt.CurrentParent.Push(pxDitaElement);
    foreach (PXElement el in this._e.TagValue)
      this.DoRender(el, resultTxt);
    resultTxt.CurrentParent.Pop();
  }
}
