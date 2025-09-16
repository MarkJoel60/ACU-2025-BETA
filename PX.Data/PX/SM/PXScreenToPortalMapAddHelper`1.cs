// Decompiled with JetBrains decompiler
// Type: PX.SM.PXScreenToPortalMapAddHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

internal class PXScreenToPortalMapAddHelper<DacType>(
  string screenIDPrefix,
  string urlPrefix,
  System.Type siteMapTitleField,
  System.Type defaultSiteMapTitleField,
  System.Type siteMapIDField,
  System.Type urlParamField,
  string placeholderID,
  bool generateNewScreenIDFromKeys,
  PXCache entityCache,
  PXCache siteMapTreeCache) : PXBaseScreenToSiteMapAddHelper<DacType, PortalMap>(screenIDPrefix, urlPrefix, siteMapTitleField, defaultSiteMapTitleField, siteMapIDField, urlParamField, placeholderID, generateNewScreenIDFromKeys, entityCache, siteMapTreeCache)
  where DacType : IBqlTable
{
}
