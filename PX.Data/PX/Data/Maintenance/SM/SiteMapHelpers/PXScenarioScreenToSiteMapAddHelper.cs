// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.SM.SiteMapHelpers.PXScenarioScreenToSiteMapAddHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Metadata;
using System;
using System.Linq;

#nullable disable
namespace PX.Data.Maintenance.SM.SiteMapHelpers;

internal class PXScenarioScreenToSiteMapAddHelper : PXFieldScreenToSiteMapAddHelper<SYMapping>
{
  public PXScenarioScreenToSiteMapAddHelper(
    PXGraph graph,
    IScreenInfoCacheControl screenInfoCacheControl,
    string screenIdPrefix,
    string urlPrefix)
    : base(graph, screenInfoCacheControl, screenIdPrefix, urlPrefix, typeof (SYMapping.name), typeof (SYMapping.sitemapTitle), typeof (SYMapping.sitemapScreenId))
  {
    graph.FieldUpdated.AddHandler(typeof (SYMapping), "isActive", new PXFieldUpdated(this.T_IsActive_FieldUpdated));
  }

  internal override bool IsVisible(SYMapping row)
  {
    bool? isActive = row.IsActive;
    bool flag = true;
    return isActive.GetValueOrDefault() == flag & isActive.HasValue && base.IsVisible(row);
  }

  protected void T_IsActive_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SYMapping row = (SYMapping) e.Row;
    if (row == null)
      return;
    bool? isActive = row.IsActive;
    bool flag = true;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      return;
    PXSiteMapNode pxSiteMapNode = this.FindNodes(row).FirstOrDefault<PXSiteMapNode>();
    PXScreenToSiteMapAddHelperBase<SYMapping>.SiteMapInternal siteMapInternal1;
    if (pxSiteMapNode == null)
      siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<SYMapping>.SiteMapInternal) null;
    else
      siteMapInternal1 = (PXScreenToSiteMapAddHelperBase<SYMapping>.SiteMapInternal) this.SiteMap.SelectSingle((object) pxSiteMapNode.NodeID);
    PXScreenToSiteMapAddHelperBase<SYMapping>.SiteMapInternal siteMapInternal2 = siteMapInternal1;
    string b = pxSiteMapNode?.Url ?? this.BuildUrl(row);
    foreach (PXScreenToSiteMapAddHelperBase<SYMapping>.SiteMapInternal siteMapInternal3 in this.SiteMapCache.Inserted)
    {
      if (string.Equals(siteMapInternal3.Url, b, StringComparison.OrdinalIgnoreCase))
        this.SiteMapCache.Delete((object) siteMapInternal3);
    }
    sender.SetValue(e.Row, this.TitleField, (object) null);
    if (!string.IsNullOrEmpty(this.ScreenIDField) && !this.IsScreenFieldGuid)
      sender.SetValue(e.Row, this.ScreenIDField, (object) null);
    if (siteMapInternal2 == null)
      return;
    this.SiteMapCache.Delete((object) siteMapInternal2);
  }
}
