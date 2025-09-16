// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXTableCellRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXTableCell HTML rendering.</summary>
internal class PXTableCellRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXTableCell e = (PXTableCell) elem;
    string tagValue = this.GetTagValue(e);
    if (string.IsNullOrEmpty(e.Props))
      resultHtml.Append($"<{tagValue}>");
    else
      resultHtml.Append($"<{tagValue} {e.Props}>");
    resultHtml.Append(Environment.NewLine);
    foreach (PXElement child in e.Children)
      this.DoRender(child, resultHtml, settings);
    resultHtml.Append($"</{tagValue}>{Environment.NewLine}");
  }

  private string GetTagValue(PXTableCell e) => e.IsHeader ? "th" : "td";
}
