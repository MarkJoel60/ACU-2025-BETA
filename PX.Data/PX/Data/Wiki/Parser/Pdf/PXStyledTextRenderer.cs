// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXStyledTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

internal class PXStyledTextRenderer : PXPdfRenderer
{
  protected override void Render(PXElement elem, PXPdfRenderContext resultPdf)
  {
    PXStyledTextElement e = (PXStyledTextElement) elem;
    string name = resultPdf.FontInfo.Name;
    resultPdf.FontInfo.Name = e.Style == TextStyle.Monotype ? "Monotype" : "Arial";
    resultPdf.FontInfo.AppliedStyles.Add(this.GetStyle(e));
    foreach (PXElement child in e.Children)
      this.DoRender(child, resultPdf);
    resultPdf.FontInfo.Name = name;
    resultPdf.FontInfo.AppliedStyles.RemoveAt(resultPdf.FontInfo.AppliedStyles.Count - 1);
  }

  private FontStyle GetStyle(PXStyledTextElement e)
  {
    return e.Style != TextStyle.Monotype ? (FontStyle) e.Style : FontStyle.Regular;
  }
}
