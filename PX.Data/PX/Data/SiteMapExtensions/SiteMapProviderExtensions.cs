// Decompiled with JetBrains decompiler
// Type: PX.Data.SiteMapExtensions.SiteMapProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.SiteMapExtensions;

[PXInternalUseOnly]
public static class SiteMapProviderExtensions
{
  public static string GetScreenUrl(this PXSiteMapProvider siteMapProvider, string screenId)
  {
    PXSiteMapNode mapNodeByScreenId = siteMapProvider.FindSiteMapNodeByScreenID(screenId);
    if (mapNodeByScreenId == null)
      throw new PXException("The form with the {0} screen ID has not been found or you do not have sufficient rights to access this form.", new object[1]
      {
        (object) screenId
      });
    return mapNodeByScreenId?.Url;
  }

  public static string GetScreenUrl(this PXSiteMapProvider siteMapProvider, System.Type graphType)
  {
    return PXUrl.TrimUrl(SiteMapProviderExtensions.GetSiteMapNode(siteMapProvider, graphType).Url);
  }

  public static string GetScreenTitle(this PXSiteMapProvider siteMapProvider, System.Type graphType)
  {
    return SiteMapProviderExtensions.GetSiteMapNode(siteMapProvider, graphType)?.Title;
  }

  private static PXSiteMapNode GetSiteMapNode(PXSiteMapProvider siteMapProvider, System.Type graphType)
  {
    return siteMapProvider.FindSiteMapNode(graphType) ?? throw new PXException("You have insufficient rights to access the object ({0}).", new object[1]
    {
      (object) graphType.Name
    });
  }
}
