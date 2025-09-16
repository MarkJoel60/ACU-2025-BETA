// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.SourceTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class SourceTagProcessor : PXHtmlParser.TagProcessor
{
  private static readonly SourceTagProcessor instance = new SourceTagProcessor();
  private static SourceTagProcessor.DummyParser dummyParser = new SourceTagProcessor.DummyParser();
  private static CSharpHighlighter csharpParser = new CSharpHighlighter();

  public static SourceTagProcessor Instance => SourceTagProcessor.instance;

  protected SourceTagProcessor()
  {
  }

  private SourceTagProcessor.SourceHighlightingParser GetParser(
    string tagName,
    List<PXHtmlAttribute> attributes)
  {
    foreach (PXHtmlAttribute attribute in attributes)
    {
      if (attribute.name == "lang" && attribute.value.ToLowerInvariant() == "csharp")
        return (SourceTagProcessor.SourceHighlightingParser) SourceTagProcessor.csharpParser;
    }
    return (SourceTagProcessor.SourceHighlightingParser) SourceTagProcessor.dummyParser;
  }

  public PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    return (PXElement) this.GetParser(tagName, attributes).Process(SourceTagProcessor.UnescapeSlashes(content), attributes);
  }

  private static string UnescapeSlashes(string content) => content.Replace("&#47;", "/");

  public interface SourceHighlightingParser
  {
    SourceElement Process(string content, List<PXHtmlAttribute> attributes);
  }

  private class DummyParser : SourceTagProcessor.SourceHighlightingParser
  {
    public SourceElement Process(string content, List<PXHtmlAttribute> attributes)
    {
      return new SourceElement()
      {
        Source = {
          new SourceElement.SourcePart(content, SourceElement.SyntaxType.Text, SourceElement.DiffState.NoChange)
        }
      };
    }
  }
}
