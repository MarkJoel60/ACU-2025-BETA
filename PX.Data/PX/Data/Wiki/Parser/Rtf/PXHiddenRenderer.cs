// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXHiddenRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXHiddenElement RTF rendering.</summary>
internal class PXHiddenRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXHiddenElement pxHiddenElement = (PXHiddenElement) elem;
    rtf.AddString(Environment.NewLine);
    foreach (PXElement el in pxHiddenElement.Caption)
      this.DoRender(el, rtf);
    rtf.Paragraph();
    rtf.AddString(Environment.NewLine);
    foreach (PXElement child in pxHiddenElement.Children)
      this.DoRender(child, rtf);
  }
}
