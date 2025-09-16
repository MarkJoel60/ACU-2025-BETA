// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXCodeBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXCodeBoxElement rendering.</summary>
internal class PXCodeBoxRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXCodeBoxElement e = (PXCodeBoxElement) elem;
    resultHtml.Append($"{Environment.NewLine}<pre {PXHtmlFormatter.GetWikiTag((PXElement) e)}>");
    foreach (PXElement child in e.Children)
    {
      switch (child)
      {
        case PXTextElement _:
          resultHtml.Append(((PXTextElement) child).Value);
          break;
        case PXStyledTextElement _:
          this.DoRender(child, resultHtml, settings);
          break;
      }
    }
    resultHtml.Append("</pre>" + Environment.NewLine);
  }
}
