// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXIndentRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXIndentRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    foreach (PXElement child1 in ((PXContainerElement) elem).Children)
    {
      switch (child1)
      {
        case PXIndentContainer _:
          this.DoRender(child1, resultHtml, settings);
          break;
        case PXIndentElement _:
          foreach (PXElement child2 in ((PXContainerElement) child1).Children)
            this.DoRender(child2, resultHtml, settings);
          if (resultHtml.Length > 0)
          {
            int index = this.lastsymvol(resultHtml);
            if (resultHtml[index] == '.' || resultHtml[index] == ':' || resultHtml[index] == ';')
            {
              resultHtml.Append("");
              break;
            }
            resultHtml.Append("; ");
            break;
          }
          break;
      }
    }
  }
}
