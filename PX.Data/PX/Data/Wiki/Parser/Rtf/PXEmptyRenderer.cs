// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXEmptyRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXEmptySpaceElement HTML rendering.
/// </summary>
internal class PXEmptyRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf) => rtf.NewLine();
}
