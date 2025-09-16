// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiScreenToPortalMapAddHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

internal class PXWikiScreenToPortalMapAddHelper(
  string screenIDPrefix,
  string urlPrefix,
  System.Type siteMapParentField,
  System.Type siteMapTitleField,
  System.Type defaultSiteMapTitleField,
  System.Type siteMapIDField,
  System.Type urlParamField,
  string placeholderID,
  bool generateNewScreenIDFromKeys,
  PXCache entityCache,
  PXCache siteMapTreeCache) : PXBaseWikiScreenToSiteMapAddHelper<PortalMap>(screenIDPrefix, urlPrefix, siteMapParentField, siteMapTitleField, defaultSiteMapTitleField, siteMapIDField, urlParamField, placeholderID, generateNewScreenIDFromKeys, entityCache, siteMapTreeCache)
{
}
