// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.NowikiTagProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class NowikiTagProcessor : PXHtmlParser.TagProcessor
{
  private static readonly NowikiTagProcessor instance = new NowikiTagProcessor();

  public static NowikiTagProcessor Instance => NowikiTagProcessor.instance;

  protected NowikiTagProcessor()
  {
  }

  public PXElement Process(
    string tagName,
    string content,
    List<PXHtmlAttribute> attributes,
    WikiArticle result,
    PXWikiParserContext settings)
  {
    return (PXElement) new NowikiElement(content);
  }
}
