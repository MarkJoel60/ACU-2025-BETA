// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXSiteMapRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.LocalizationKeyGenerators;

#nullable disable
namespace PX.Translation;

/// <exclude />
internal class PXSiteMapRipper : PXRipperWithGraph
{
  public override void CollectResources(ResourceCollection result)
  {
    foreach (PXResult<PX.SM.SiteMap> pxResult in PXSelectBase<PX.SM.SiteMap, PXSelect<PX.SM.SiteMap>.Config>.Select(this.graph))
    {
      PX.SM.SiteMap sm = (PX.SM.SiteMap) pxResult;
      string localizationKey = PXSiteMapKeyGenerator.GetLocalizationKey(sm);
      result.AddResource(new LocalizationResourceLite(localizationKey, LocalizationResourceType.SiteMapNode, sm.Title));
    }
    foreach (PXResult<PX.SM.PortalMap> pxResult in PXSelectBase<PX.SM.PortalMap, PXSelect<PX.SM.PortalMap>.Config>.Select(this.graph))
    {
      PX.SM.PortalMap sm = (PX.SM.PortalMap) pxResult;
      string localizationKey = PXSiteMapKeyGenerator.GetLocalizationKey((PX.SM.SiteMap) sm);
      result.AddResource(new LocalizationResourceLite(localizationKey, LocalizationResourceType.SiteMapNode, sm.Title));
    }
  }
}
