// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBaseGenScreenToSiteMapAddHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public abstract class PXBaseGenScreenToSiteMapAddHelper<DacType> : PXScreenToSiteMapBaseHelper where DacType : IBqlTable
{
  public PXBaseGenScreenToSiteMapAddHelper(
    string screenIDPrefix,
    string urlPrefix,
    System.Type siteMapTitleField,
    System.Type defaultSiteMapTitleField,
    System.Type siteMapIDField,
    System.Type urlParamField,
    string placeholderID,
    bool generateNewScreenIDFromKeys,
    PXCache entityCache,
    PXCache siteMapTreeCache)
    : base(entityCache)
  {
  }

  public abstract bool Persist(DacType row);

  public abstract bool DeleteObsoleteSiteMapNode(DacType row, PXDBOperation operation);

  public abstract void InitializeSiteMapFields(DacType row);

  public abstract void UpdateSiteMapTitle(DacType row);

  public abstract void UpdateSiteMapParent(DacType row);
}
