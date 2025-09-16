// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXHeaderRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXHeaderElement RTF rendering.</summary>
internal class PXHeaderRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXHeaderElement e = (PXHeaderElement) elem;
    int ptSize = 20 - 3 * (int) e.Level;
    if (e.IsError)
    {
      this.RenderError(e, rtf);
    }
    else
    {
      rtf.SetTextSize(ptSize);
      foreach (PXElement child in e.Children)
      {
        switch (child)
        {
          case PXTextElement _:
          case PXLinkElement _:
          case PXImageElement _:
            this.DoRender(child, rtf);
            break;
        }
      }
      rtf.SetTextSize(rtf.Document.Settings.FontSize);
      rtf.Paragraph();
    }
  }

  private void RenderError(PXHeaderElement e, PXRtfBuilder rtf)
  {
    rtf.SetTextColor(Color.Red);
    rtf.AddString("Unclosed header: ");
    rtf.AddString(e.Name);
    rtf.AddString(", cannot render section!");
    rtf.DisableColor();
  }
}
