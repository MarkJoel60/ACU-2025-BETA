// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXSpecialTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>
/// Represents a class for PXSpecialTagElement RTF rendering.
/// </summary>
internal class PXSpecialTagRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXSpecialTagElement specialTagElement = (PXSpecialTagElement) elem;
    switch (specialTagElement.TagValue.ToLower())
    {
      case "{wikititle}":
        rtf.AddString(rtf.Settings.WikiTitle);
        break;
      case "{toc}":
        break;
      case "{up}":
        break;
      case "{themepath}":
        rtf.AddString(rtf.Settings.ThemePath);
        break;
      case "{br}":
        rtf.AddString(Environment.NewLine, true);
        break;
      case "{pagebreak}":
        rtf.PageBreak();
        break;
      default:
        rtf.AddString(specialTagElement.TagValue);
        break;
    }
  }
}
