// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXSourceRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXSourceRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    foreach (SourceElement.SourcePart sourcePart in ((SourceElement) elem).Source)
    {
      sourcePart.Value = sourcePart.Value.Replace("&lt;", "<");
      sourcePart.Value = sourcePart.Value.Replace("&gt;", ">");
      sourcePart.Value = sourcePart.Value.Replace("&#38;", "& ");
      sourcePart.Value = sourcePart.Value.Replace("&nbsp;", "");
      sourcePart.Value = sourcePart.Value.Replace("&#8212;;", "—");
      resultHtml.Append(sourcePart.Value);
    }
  }
}
