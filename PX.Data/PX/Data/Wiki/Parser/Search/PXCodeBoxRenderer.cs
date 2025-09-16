// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Search.PXCodeBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Search;

internal class PXCodeBoxRenderer : PXSearchRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    foreach (PXElement child in ((PXContainerElement) elem).Children)
    {
      if (child is PXTextElement)
      {
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&lt;", "<");
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&gt;", ">");
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&nbsp;", " ");
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&nbsp", " ");
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&#38;", "& ");
        ((PXTextElement) child).Value = ((PXTextElement) child).Value.Replace("&#8212;", "—");
        resultHtml.Append(((PXTextElement) child).Value);
      }
    }
  }
}
