// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents a base class for rendering of parsed wiki text.
/// </summary>
public abstract class PXRenderer
{
  private PXWikiParserContext context;

  public PXWikiParserContext Context
  {
    get
    {
      if (this.context == null)
        this.context = new PXWikiParserContext();
      return this.context;
    }
    set => this.context = value;
  }

  public abstract void Render(WikiArticle output);

  public abstract string GetResultingString(PXBlockParser.ParseContext parseContext);
}
