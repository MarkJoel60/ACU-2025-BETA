// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXEmbeddedVideoRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXEmbeddedVideoElement HTML rendering.
/// </summary>
public class PXEmbeddedVideoRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    string str1 = $"<iframe src=\"{((PXEmbeddedVideoElement) elem).VideoUrl}\" allowfullscreen></iframe>";
    string str2 = $"<div {this.GetWikiClass("wk-video-container", settings)}>{str1}</div>";
    resultHtml.Append(str2);
  }
}
