// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXStyledTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXStyledTextElement RTF rendering.
/// </summary>
internal class PXStyledTextRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXStyledTextElement styledTextElement = (PXStyledTextElement) elem;
    rtf.AddString(Environment.NewLine);
    rtf.SetTextStyle(styledTextElement.Style);
    foreach (PXElement child in styledTextElement.Children)
      this.DoRender(child, rtf);
    rtf.DisableTextStyle(styledTextElement.Style);
  }
}
