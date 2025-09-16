// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.DummyTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class DummyTagProcessor : PXHtmlParser.TagProcessor
{
  private static readonly DummyTagProcessor instance = new DummyTagProcessor();

  public static DummyTagProcessor Instance => DummyTagProcessor.instance;

  protected DummyTagProcessor()
  {
  }

  public PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("<{0}", (object) tagName);
    foreach (PXHtmlAttribute attribute in attributes)
      stringBuilder.AppendFormat(" {0}=\"{1}\"", (object) attribute.name, (object) attribute.value.Replace("\"", "\\\""));
    stringBuilder.AppendFormat(">{0}</{1}>", (object) content, (object) tagName);
    return (PXElement) new SourceElement(stringBuilder.ToString());
  }
}
