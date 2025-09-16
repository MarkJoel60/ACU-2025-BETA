// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXTextElement RTF rendering.</summary>
internal class PXTextRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXTextElement pxTextElement = (PXTextElement) elem;
    rtf.AddString(pxTextElement.Value);
  }
}
