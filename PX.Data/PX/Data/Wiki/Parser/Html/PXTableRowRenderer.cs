// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXTableRowRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXTableRow HTML rendering.</summary>
internal class PXTableRowRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXTableRow row = (PXTableRow) elem;
    resultHtml.Append(this.TRTag(row, settings));
    resultHtml.Append(Environment.NewLine);
    foreach (PXElement cell in row.Cells)
      this.DoRender(cell, resultHtml, settings);
    resultHtml.Append("</tr>" + Environment.NewLine);
  }

  private string TRTag(PXTableRow row, PXWikiParserContext settings)
  {
    string str = "<tr";
    if (settings.IsDesignMode && row.IsSingleLine)
      str += " singleline=\"1\"";
    if (!string.IsNullOrEmpty(row.Props))
      str = $"{str} {row.Props}";
    return str + ">";
  }
}
