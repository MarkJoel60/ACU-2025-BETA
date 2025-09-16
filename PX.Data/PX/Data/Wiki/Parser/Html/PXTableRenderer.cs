// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXTableRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXTableElement HTML rendering.</summary>
internal class PXTableRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXTableElement pxTableElement = (PXTableElement) elem;
    resultHtml.Append(Environment.NewLine);
    if (string.IsNullOrEmpty(pxTableElement.Props))
      resultHtml.Append("<table>");
    else
      resultHtml.Append($"<table {StyleBuilder.MergeCssClasses(pxTableElement.Props)}>");
    if (!string.IsNullOrEmpty(pxTableElement.Caption))
      resultHtml.Append($"<caption>{pxTableElement.Caption}</caption>");
    resultHtml.Append(Environment.NewLine);
    foreach (PXElement row in pxTableElement.Rows)
      this.DoRender(row, resultHtml, settings);
    resultHtml.Append("</table>" + Environment.NewLine);
  }
}
