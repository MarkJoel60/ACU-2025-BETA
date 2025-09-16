// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXHtmlTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXHtmlTagElement RTF rendering.
/// </summary>
internal class PXHtmlTagRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXHtmlTagElement e = (PXHtmlTagElement) elem;
    if (string.Compare(e.TagName, "sup", true) == 0)
    {
      rtf.SetTextStyle(TextStyle.Superscript);
      this.RenderInner(e, rtf);
      rtf.DisableTextStyle(TextStyle.Superscript);
    }
    else if (string.Compare(e.TagName, "sub", true) == 0)
    {
      rtf.SetTextStyle(TextStyle.Subscript);
      this.RenderInner(e, rtf);
      rtf.DisableTextStyle(TextStyle.Subscript);
    }
    else if (string.Compare(e.TagName, "br", true) == 0)
    {
      rtf.NewLine();
    }
    else
    {
      rtf.AddString($"<{e.TagName}>");
      this.RenderInner(e, rtf);
      rtf.AddString($"</{e.TagName}>");
    }
  }

  private void RenderInner(PXHtmlTagElement e, PXRtfBuilder rtf)
  {
    foreach (PXElement el in e.TagValue)
      this.DoRender(el, rtf);
  }
}
