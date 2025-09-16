// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXRssParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXRssParser : PXBlockParser
{
  protected override bool IsAllowedForParsing(Token tk) => false;

  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    PXRssLinkBase linkElement = this.CreateLinkElement();
    StringBuilder stringBuilder = new StringBuilder(this.WikiPrefix);
    StringBuilder options = new StringBuilder();
    this.CollectOptions(context, options);
    this.SetProperties(linkElement, options.ToString().Split('|'));
    stringBuilder.Append((object) options);
    this.TryAddElementToParagraph((PXElement) linkElement, context, result);
    if (!context.Settings.IsDesignMode)
      return;
    linkElement.WikiTag = this.WikiTag;
    linkElement.WikiText = stringBuilder.ToString();
  }

  protected virtual string WikiPrefix => "RSS:";

  protected virtual string WikiTag => "rss";

  protected virtual PXRssLinkBase CreateLinkElement() => (PXRssLinkBase) new PXRssLink();

  private void CollectOptions(PXBlockParser.ParseContext context, StringBuilder options)
  {
    while (context.StartIndex < context.WikiText.Length)
    {
      string TokenValue;
      Token nextToken = this.GetNextToken(context, out TokenValue);
      if (nextToken == Token.linkend || nextToken == Token.link2end)
      {
        context.StartIndex -= TokenValue.Length;
        break;
      }
      options.Append(TokenValue);
    }
  }

  protected virtual void SetProperties(PXRssLinkBase element, string[] options)
  {
    if (!(element is PXRssLink pxRssLink) || options.Length < 1)
      return;
    pxRssLink.WikiId = this.GetWiki(options[0].Trim());
    if (options.Length > 1)
      pxRssLink.FolderId = this.GetFolder(options[0].Trim(), options[1].Trim());
    if (options.Length > 2)
      int.TryParse(options[2].Trim(), out pxRssLink.MaxNewsCount);
    int result;
    if (options.Length > 3 && int.TryParse(options[3].Trim(), out result))
      pxRssLink.CategoryId = new int?(result);
    if (options.Length > 4)
      pxRssLink.Title = options[4];
    if (options.Length <= 5)
      return;
    pxRssLink.Description = options[5];
  }

  private Guid? GetWiki(string wiki)
  {
    if (string.IsNullOrEmpty(wiki))
      wiki = PXNewslineDeclaration.DefaultWiki;
    Guid? guid = GUID.CreateGuid(wiki);
    if (guid.HasValue)
      return guid;
    return PXSiteMap.WikiProvider.FindWiki(wiki)?.NodeID;
  }

  private Guid? GetFolder(string wiki, string folder)
  {
    if (string.IsNullOrEmpty(wiki))
      wiki = PXNewslineDeclaration.DefaultWiki;
    Guid? guid = GUID.CreateGuid(folder);
    if (guid.HasValue)
      return guid;
    return PXSiteMap.WikiProvider.FindSiteMapNode(wiki, folder)?.NodeID;
  }
}
