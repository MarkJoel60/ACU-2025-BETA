// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

/// <summary>
/// Represents a class for PXSectionElement txt rendering.
/// </summary>
internal class PXSectionRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXSectionElement pxSectionElement = (PXSectionElement) elem;
    this.DoRender((PXElement) pxSectionElement.Header, resultTxt);
    foreach (PXElement child in pxSectionElement.Children)
      this.DoRender(child, resultTxt);
  }
}
