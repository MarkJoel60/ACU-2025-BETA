// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXHtmlTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>
/// Represents a class for PXHtmlTagElement txt rendering.
/// </summary>
internal class PXHtmlTagRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXHtmlTagElement pxHtmlTagElement = (PXHtmlTagElement) elem;
    if (string.Compare(pxHtmlTagElement.TagName, "br", true) == 0)
    {
      resultTxt.NewLine();
    }
    else
    {
      foreach (PXElement el in pxHtmlTagElement.TagValue)
        this.DoRender(el, resultTxt);
    }
  }
}
