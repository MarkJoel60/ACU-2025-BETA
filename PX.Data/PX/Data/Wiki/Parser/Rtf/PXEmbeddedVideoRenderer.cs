// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXEmbeddedVideoRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXEmbeddedVideoElement RTF rendering.
/// </summary>
internal class PXEmbeddedVideoRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXEmbeddedVideoElement embeddedVideoElement = (PXEmbeddedVideoElement) elem;
    PXLinkElement elem1 = new PXLinkElement();
    elem1.AddToCaption((PXElement) new PXTextElement()
    {
      Value = "Open the video"
    });
    elem1.AddToLink((PXElement) new PXTextElement()
    {
      Value = embeddedVideoElement.VideoUrl
    });
    PXParagraphElement el = new PXParagraphElement();
    el.AddChild((PXElement) elem1);
    this.DoRender((PXElement) el, rtf);
  }
}
