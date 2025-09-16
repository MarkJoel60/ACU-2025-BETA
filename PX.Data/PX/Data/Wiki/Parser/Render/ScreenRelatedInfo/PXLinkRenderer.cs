// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.PXLinkRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.ScreenRelatedInfo;

internal sealed class PXLinkRenderer
{
  private const string WikiTopicLinkTemplate = "{0}/Help?ScreenId=ShowWiki&pageid={1}";

  public PXScreenRelatedInfoLink RenderWithSettings(
    PXLinkElement link,
    PXWikiParserContext settings)
  {
    string str1 = PXLinkRenderer.JoinElementValues((IEnumerable<PXElement>) link.GetCaptionElements());
    string str2 = PXLinkRenderer.FormatLink(PXLinkRenderer.JoinElementValues((IEnumerable<PXElement>) link.GetLinkElements()), settings);
    if (str2 == null)
      return (PXScreenRelatedInfoLink) null;
    return new PXScreenRelatedInfoLink()
    {
      Text = str1,
      Link = str2
    };
  }

  private static string JoinElementValues(IEnumerable<PXElement> elements)
  {
    return elements.Aggregate<PXElement, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, PXElement, StringBuilder>) ((a, e) => a.Append((e is PXTextElement pxTextElement ? pxTextElement.Value : (string) null) ?? string.Empty)), (Func<StringBuilder, string>) (a => a.ToString()));
  }

  public static string FormatLink(string internalLink, PXWikiParserContext settings)
  {
    if (internalLink.Equals("anchor", StringComparison.OrdinalIgnoreCase))
      return (string) null;
    if (internalLink.StartsWith("~") && !string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
      return settings.Settings.RootUrl + internalLink.Substring(1);
    if (internalLink.Contains("://") || internalLink.StartsWith("www."))
      return internalLink;
    Guid? pageId = settings.CreateArticleLink(internalLink).PageID;
    return $"{settings.Settings.RootUrl}/Help?ScreenId=ShowWiki&pageid={pageId}";
  }
}
