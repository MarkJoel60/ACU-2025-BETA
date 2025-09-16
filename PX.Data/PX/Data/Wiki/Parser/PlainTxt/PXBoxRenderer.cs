// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

/// <summary>Represents a class for rendering PXBoxElement to txt.</summary>
internal class PXBoxRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    if (elem is SourceElement sourceElement)
    {
      foreach (SourceElement.SourcePart sourcePart in sourceElement.Source)
        resultTxt.Append(sourcePart.Value);
    }
    else
    {
      foreach (PXElement child in ((PXContainerElement) elem).Children)
        this.DoRender(child, resultTxt);
    }
  }
}
