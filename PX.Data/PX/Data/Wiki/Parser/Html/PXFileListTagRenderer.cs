// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXFileListTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXSpecialTagElement HTML rendering.
/// </summary>
internal class PXFileListTagRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    if (!(elem is PXFileListTagElement e))
      return;
    resultHtml.Append($"<div id=\"FileList\" name=\"FileList\" {PXHtmlFormatter.GetWikiTag((PXElement) e)}></div>");
    settings.RenderFileList = true;
  }
}
