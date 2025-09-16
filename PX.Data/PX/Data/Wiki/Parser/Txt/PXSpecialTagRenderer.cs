// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXSpecialTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>
/// Represents a class for PXSpecialTagElement txt rendering.
/// </summary>
internal class PXSpecialTagRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXSpecialTagElement specialTagElement = (PXSpecialTagElement) elem;
    switch (specialTagElement.TagValue.ToLower())
    {
      case "{wikititle}":
        resultTxt.Append(resultTxt.Settings.WikiTitle);
        break;
      case "{toc}":
        break;
      case "{up}":
        break;
      case "{themepath}":
        resultTxt.Append(resultTxt.Settings.ThemePath);
        break;
      case "{br}":
        resultTxt.NewLine();
        break;
      case "{pagebreak}":
        break;
      default:
        resultTxt.Append(specialTagElement.TagValue);
        break;
    }
  }
}
