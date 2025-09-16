// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SiteMapHelpers.RMReportScreenToSiteMapAddHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.CS;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Maintenance.SM.SiteMapHelpers;

internal class RMReportScreenToSiteMapAddHelper(
  PXGraph graph,
  IScreenInfoCacheControl screenInfoCacheControl) : PXFieldScreenToSiteMapAddHelper<RMReport>(graph, screenInfoCacheControl, "RM", (IEnumerable<string>) new \u003C\u003Ez__ReadOnlyArray<string>(new string[2]
{
  "~/Frames/RmLauncher.aspx",
  "~/Scripts/Screens/ReportScreen.html"
}), typeof (RMReport.reportCode), typeof (RMReport.sitemapTitle), typeof (RMReport.reportUID), (PXFieldScreenToSiteMapAddHelper<RMReport>.Field[]) new PXFieldScreenToSiteMapAddHelper<RMReport>.Field<RMReport.reportCode>[1]
{
  new PXFieldScreenToSiteMapAddHelper<RMReport>.Field<RMReport.reportCode>("ID")
})
{
  protected override string BuildUrl(
    RMReport row,
    PXFieldScreenToSiteMapAddHelper<RMReport>.Field[] fields,
    string urlPrefix)
  {
    string str = base.BuildUrl(row, fields, urlPrefix);
    if (urlPrefix == "~/Scripts/Screens/ReportScreen.html")
      str += "&IsARm=true";
    return str;
  }

  internal override bool IsVisible(RMReport row)
  {
    return base.IsVisible(row) || this.Cache.GetValue((object) row, this.TitleField) != null;
  }

  internal string GetSiteMapScreenID(RMReport row)
  {
    return this.FindNodes(row).FirstOrDefault<PXSiteMapNode>()?.ScreenID;
  }

  internal string GenerateSiteMapScreenID(RMReport row)
  {
    Guid nodeID = (Guid) this.Cache.GetValue((object) row, this.ScreenIDField);
    PX.SM.SiteMap node = this.SiteMapCache.Inserted.Cast<PX.SM.SiteMap>().FirstOrDefault<PX.SM.SiteMap>((Func<PX.SM.SiteMap, bool>) (sm =>
    {
      Guid? nodeId = sm.NodeID;
      Guid guid = nodeID;
      if (!nodeId.HasValue)
        return false;
      return !nodeId.HasValue || nodeId.GetValueOrDefault() == guid;
    }));
    if (node == null)
      return (string) null;
    node.ScreenID = this.GenerateNewScreenID(node);
    this.SiteMapCache.Update((object) node);
    return node.ScreenID;
  }
}
