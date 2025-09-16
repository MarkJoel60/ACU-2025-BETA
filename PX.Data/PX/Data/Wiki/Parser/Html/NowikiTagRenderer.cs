// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.NowikiTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

internal class NowikiTagRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    NowikiElement nowikiElement = (NowikiElement) elem;
    if (settings.IsDesignMode)
      resultHtml.Append("<span wikitag=\"nowiki\">");
    resultHtml.Append(nowikiElement.Value);
    if (!settings.IsDesignMode)
      return;
    resultHtml.Append("</span>");
  }
}
