// Decompiled with JetBrains decompiler
// Type: PX.Data.GIScreenHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.Maintenance.GI;
using PX.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
[Obsolete("This class is not intended for public use and will be made internal in the next release")]
public static class GIScreenHelper
{
  public static PXSiteMapNode TryGetSiteMapNode(string screenID)
  {
    return screenID == null ? (PXSiteMapNode) null : PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
  }

  /// <exception cref="T:PX.Data.PXException">Throws if node can't be found or it doesn't have graph type.</exception>
  public static PXSiteMapNode GetSiteMapNode(string screenID)
  {
    PXSiteMapNode siteMapNode = GIScreenHelper.TryGetSiteMapNode(screenID);
    if (siteMapNode == null || !PXSiteMap.IsReport(siteMapNode.Url) && string.IsNullOrEmpty(siteMapNode.GraphType))
      throw new PXException("A site map node with the screen ID {0} does not exist. Maybe it was moved or removed.", new object[1]
      {
        (object) screenID
      });
    return siteMapNode;
  }

  public static System.Type GetCacheType(string screenID)
  {
    PXSiteMapNode siteMapNode = GIScreenHelper.GetSiteMapNode(screenID);
    if (siteMapNode.GraphType == typeof (PXGenericInqGrph).FullName)
      return typeof (GenericResult);
    string primaryView = PXPageIndexingService.GetPrimaryView(siteMapNode.GraphType);
    return GraphHelper.GetGraphView(siteMapNode.GraphType, primaryView).Cache.CacheType;
  }

  public static string GetCacheName(string screenID)
  {
    PXSiteMapNode siteMapNode = GIScreenHelper.GetSiteMapNode(screenID);
    if (siteMapNode.GraphType == typeof (PXGenericInqGrph).FullName)
      return string.Empty;
    string primaryView = PXPageIndexingService.GetPrimaryView(siteMapNode.GraphType);
    return GraphHelper.GetGraphView(siteMapNode.GraphType, primaryView).Cache.Name;
  }

  internal static System.Type GetGraphType(string screenId)
  {
    return PXBuildManager.GetType(GIScreenHelper.GetSiteMapNode(screenId).GraphType, false);
  }

  public static PXGraph InstantiateGraph(string screenId)
  {
    return GIScreenHelper.InstantiateGraph(screenId, true);
  }

  public static PXGraph InstantiateGraph(string screenId, bool clearNewInstance)
  {
    if (!ServiceLocator.IsLocationProviderSet)
      return new PXGraph();
    using (new PXPreserveScope())
    {
      PXGraph pxGraph = ServiceLocator.Current.GetInstance<PXGraphFactory>()(screenId);
      if (clearNewInstance)
        pxGraph.Clear();
      return pxGraph;
    }
  }

  public static PXGraph InstantiateGraph(string screenId, out PXView primaryView)
  {
    PXGraph pxGraph = GIScreenHelper.InstantiateGraph(screenId);
    string primaryView1 = PXPageIndexingService.GetPrimaryView(pxGraph.GetType().FullName);
    if (string.IsNullOrEmpty(primaryView1))
      primaryView1 = pxGraph.PrimaryView;
    primaryView = pxGraph.Views[primaryView1];
    return pxGraph;
  }

  internal static Guid? GetGenericInquiryIDByScreenID(string screenID)
  {
    if (string.IsNullOrEmpty(screenID))
      return new Guid?();
    PXSiteMapNode pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNodesByScreenIDUnsecure(screenID).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => PXSiteMap.IsGenericInquiry(n.Url)));
    string parameter1 = PXUrl.GetParameter(pxSiteMapNode.Url, "id");
    Guid result;
    if (!string.IsNullOrEmpty(parameter1) && Guid.TryParse(parameter1, out result))
      return new Guid?(result);
    string parameter2 = PXUrl.GetParameter(pxSiteMapNode.Url, "name");
    if (string.IsNullOrEmpty(parameter2))
      return new Guid?();
    return new PXSelect<GIDesign, Where<GIDesign.name, Equal<Required<GIDesign.name>>>>(GIScreenHelper.InstantiateGraph(screenID)).SelectSingle((object) parameter2)?.DesignID;
  }

  internal static PXSiteMapNode TryGetSiteMapNode(GIDesign design)
  {
    if (design == null)
      return (PXSiteMapNode) null;
    return ((IEnumerable<string>) new string[4]
    {
      BuildUrl("~/GenericInquiry/GenericInquiry.aspx", "id", design.DesignID.ToString()),
      BuildUrl("~/Scripts/Screens/GenericInquiry.html", "id", design.DesignID.ToString()),
      BuildUrl("~/GenericInquiry/GenericInquiry.aspx", "name", design.Name),
      BuildUrl("~/Scripts/Screens/GenericInquiry.html", "name", design.Name)
    }).Select<string, PXSiteMapNode>((Func<string, PXSiteMapNode>) (x => PXSiteMap.Provider.FindSiteMapNodeUnsecure(x))).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => x != null));

    static string BuildUrl(string baseUrl, string paramName, string paramValue)
    {
      return $"{baseUrl}?{paramName}={WebUtility.UrlEncode(paramValue)}";
    }
  }
}
