// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXHiddenRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXHiddenRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXHiddenElement pxHiddenElement = (PXHiddenElement) elem;
    foreach (PXElement el in pxHiddenElement.Caption)
      this.DoRender(el, resultHtml, settings);
    if (resultHtml.Length > 0)
    {
      int index = this.lastsymvol(resultHtml);
      if (resultHtml[index] == '.' || resultHtml[index] == ':' || resultHtml[index] == ';')
        resultHtml.Append("");
      else
        resultHtml.Append(": ");
    }
    foreach (PXElement child in pxHiddenElement.Children)
      this.DoRender(child, resultHtml, settings);
  }
}
