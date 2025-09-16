// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXEmbeddedVideoParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXEmbeddedVideoParser : PXBlockParser
{
  protected override bool IsThisToken(Token tk)
  {
    return tk == Token.embedvideostart || tk == Token.embedvideoend;
  }

  protected override bool IsAllowedForParsing(Token tk) => false;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    int startIndex = context.StartIndex;
    int num = startIndex;
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      if (this.GetNextToken(context, out TokenValue) == Token.embedvideoend)
      {
        num = context.StartIndex - TokenValue.Length;
        break;
      }
    }
    PXEmbeddedVideoElement elem = new PXEmbeddedVideoElement()
    {
      VideoUrl = context.WikiText.Substring(startIndex, num - startIndex)
    };
    if (context.Settings.IsDesignMode)
      elem.WikiTag = "video";
    result.AddElement((PXElement) elem);
  }
}
