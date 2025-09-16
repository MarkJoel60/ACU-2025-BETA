// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXRssArticleParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXRssArticleParser : PXRssParser
{
  protected override string WikiPrefix => "RSSART:";

  protected override string WikiTag => "rssart";

  protected override PXRssLinkBase CreateLinkElement() => (PXRssLinkBase) new PXRssArticleLink();

  protected override void SetProperties(PXRssLinkBase element, string[] options)
  {
    if (!(element is PXRssArticleLink pxRssArticleLink) || options.Length < 1)
      return;
    pxRssArticleLink.PageId = GUID.CreateGuid(options[0].Trim());
    if (options.Length > 1)
      pxRssArticleLink.Title = options[1].Trim();
    if (options.Length > 2)
      pxRssArticleLink.Description = options[2].Trim();
    if (options.Length <= 3)
      return;
    pxRssArticleLink.Language = options[3].Trim();
  }
}
