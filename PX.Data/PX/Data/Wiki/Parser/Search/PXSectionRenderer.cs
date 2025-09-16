// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXSectionRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXSectionElement pxSectionElement = (PXSectionElement) elem;
    this.DoRender((PXElement) pxSectionElement.Header, resultHtml, settings);
    foreach (PXElement child in pxSectionElement.Children)
      this.DoRender(child, resultHtml, settings);
  }
}
