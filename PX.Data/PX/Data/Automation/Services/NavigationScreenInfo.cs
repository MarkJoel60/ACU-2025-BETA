// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.NavigationScreenInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Automation.Services;

internal class NavigationScreenInfo : INavigationScreenInfo
{
  private readonly PXSiteMapNode _siteMapNode;

  internal static INavigationScreenInfo GetScreenInfo(string screenId)
  {
    return (INavigationScreenInfo) new NavigationScreenInfo(screenId);
  }

  internal NavigationScreenInfo(string screenId)
  {
    this._siteMapNode = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenId);
    if (this._siteMapNode == null)
      throw new PXException("A site map node with the screen ID {0} does not exist. Maybe it was moved or removed.", new object[1]
      {
        (object) screenId
      });
    this.IsReport = PXSiteMap.IsReport(this._siteMapNode.Url) || PXSiteMap.IsARmReport(this._siteMapNode.Url);
    if (this.IsReport)
      return;
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(this._siteMapNode.ScreenID);
    if (screenInfo == null)
      throw new PXException("This form cannot be automated.", new object[1]
      {
        (object) $"{this._siteMapNode.ScreenID} - {this._siteMapNode.Title}"
      });
    PXViewDescription pxViewDescription1;
    bool flag = screenInfo.Containers.TryGetValue(screenInfo.PrimaryView, out pxViewDescription1);
    PXViewDescription pxViewDescription2 = screenInfo.Containers.Values.FirstOrDefault<PXViewDescription>((Func<PXViewDescription, bool>) (v => v.IsGrid));
    this.IsInquiry = !flag || pxViewDescription1.IsGrid;
    this.IsFilteredInquiry = flag && ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription1.AllFields).All<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => !f.IsKey)) && pxViewDescription2 != null;
    this.DataViewName = pxViewDescription2?.ViewName;
  }

  public string ScreenId => this._siteMapNode.ScreenID;

  public string DataViewName { get; }

  public System.Type GraphType => PXBuildManager.GetType(this._siteMapNode.GraphType, false);

  public string Url => this._siteMapNode.Url;

  public bool IsReport { get; }

  public bool IsInquiry { get; }

  public bool IsFilteredInquiry { get; }
}
