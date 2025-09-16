// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.SiteMapNodeFactoryExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System;

#nullable disable
namespace PX.Data.Access;

internal static class SiteMapNodeFactoryExtension
{
  internal static PXSiteMapNode SwitchUi(
    this ISiteMapNodeFactory siteMapNodeFactory,
    string ui,
    PXSiteMapNode node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    return siteMapNodeFactory.CreateNode(ui, node.Provider, node.NodeID, node.Url, node.Title, node.PXRoles, node.GraphType, node.ScreenID, PXCompanyHelper.IsTestTenant());
  }

  internal static string TransformUrlAccordingToUi(
    this ISiteMapNodeFactory siteMapNodeFactory,
    string url,
    string selectedUi,
    string screenId)
  {
    return siteMapNodeFactory.CreateNode(selectedUi, PXSiteMap.Provider, Guid.Empty, url, (string) null, (PXRoleList) null, (string) null, screenId, PXCompanyHelper.IsTestTenant()).OriginalUrl;
  }
}
