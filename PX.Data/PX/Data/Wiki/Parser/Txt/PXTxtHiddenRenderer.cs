// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXTxtHiddenRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>
/// Represents a class for rendering PXHiddenElement to txt.
/// </summary>
internal class PXTxtHiddenRenderer : PXBoxRenderer
{
  protected override string GetCaption(PXElement elem)
  {
    PXHiddenElement pxHiddenElement = (PXHiddenElement) elem;
    PXTxtRenderContext resultTxt = new PXTxtRenderContext();
    foreach (PXElement el in pxHiddenElement.Caption)
      this.DoRender(el, resultTxt);
    return "[+] " + resultTxt.Result;
  }
}
