// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.PXSectionRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.ScreenRelatedInfo;

internal sealed class PXSectionRenderer
{
  private const string AUTO_GENERATED_LINK_HEADER = "Help Dashboard";
  public const string AcumaticaNewsToken = "{News}";
  public const string AcumaticaEducationalResourcesToken = "{EducationalResources}";
  private static readonly IEnumerable<(string Title, string Url)> _dashboardLinks = (IEnumerable<(string, string)>) new (string, string)[2]
  {
    ("{EducationalResources}", "~/Help"),
    ("{News}", "~/Main?ScreenId=00000000&welcomePage=true&HideScript=On")
  };

  public PXScreenRelatedInfoSection RenderWithSettings(
    PXSectionElement section,
    PXWikiParserContext settings)
  {
    string str = section.Header.Value;
    if (str.Equals("Help Dashboard"))
      return this.GenerateHelpDashboardContent(settings);
    PXLinkRenderer pxLinkRenderer = new PXLinkRenderer();
    PXScreenRelatedInfoSection relatedInfoSection = new PXScreenRelatedInfoSection()
    {
      Header = str
    };
    foreach (PXLinkElement link in this.FindAllDescendantsOfType<PXLinkElement>((PXElement) section))
    {
      PXScreenRelatedInfoLink screenRelatedInfoLink = pxLinkRenderer.RenderWithSettings(link, settings);
      if (screenRelatedInfoLink != null)
      {
        relatedInfoSection.Links.Add(screenRelatedInfoLink);
        if (link.parent is PXContainerElement parent && parent.Children.OfType<PXImageElement>().Any<PXImageElement>((Func<PXImageElement, bool>) (i => i.Name.EndsWith("icon_video_NAV.png", StringComparison.OrdinalIgnoreCase))))
          screenRelatedInfoLink.HasVideo = new bool?(true);
      }
    }
    return relatedInfoSection;
  }

  private PXScreenRelatedInfoSection GenerateHelpDashboardContent(PXWikiParserContext settings)
  {
    PXScreenRelatedInfoSection dashboardContent = new PXScreenRelatedInfoSection()
    {
      Header = "Help Dashboard"
    };
    foreach ((string Title, string Url) dashboardLink in PXSectionRenderer._dashboardLinks)
    {
      string title = dashboardLink.Title;
      string str = PXLinkRenderer.FormatLink(dashboardLink.Url, settings);
      dashboardContent.Links.Add(new PXScreenRelatedInfoLink()
      {
        Text = title,
        Link = str
      });
    }
    return dashboardContent;
  }

  private IEnumerable<TElem> FindAllDescendantsOfType<TElem>(PXElement element) where TElem : PXElement
  {
    LinkedList<TElem> descendantsOfType = new LinkedList<TElem>();
    if (element is TElem elem1)
      descendantsOfType.AddLast(elem1);
    if (!(element is PXContainerElement containerElement))
      return (IEnumerable<TElem>) descendantsOfType;
    foreach (TElem elem2 in ((IEnumerable<PXElement>) containerElement.Children).SelectMany<PXElement, TElem>((Func<PXElement, IEnumerable<TElem>>) (c => this.FindAllDescendantsOfType<TElem>(c))))
      descendantsOfType.AddLast(elem2);
    return (IEnumerable<TElem>) descendantsOfType;
  }
}
