// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXSectionElement RTF rendering.
/// </summary>
internal class PXSectionRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXSectionElement e = (PXSectionElement) elem;
    PXRtfBuilder rtf1 = new PXRtfBuilder(rtf.Document.Settings);
    PXParagraph elem1 = new PXParagraph(rtf.Document);
    rtf1.Settings = rtf.Settings;
    rtf1.CurrentIndent = rtf.CurrentIndent;
    rtf1.CurrentTableLevel = rtf.CurrentTableLevel;
    this.DoRender((PXElement) e.Header, rtf1);
    this.RenderChildren(e, rtf1);
    elem1.Children.Add((PXRtfElement) new PXRawText(rtf.Document, rtf1.Document.Content.ToString()));
    rtf.AddRtfElement((PXRtfElement) elem1);
  }

  private void RenderChildren(PXSectionElement e, PXRtfBuilder rtf)
  {
    foreach (PXElement child in e.Children)
      this.DoRender(child, rtf);
  }
}
