// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOCreateExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.SOCreateExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<SOCreate>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<SOCreate.SOCreateFilter.sourceSiteID>, IsNull, Or<PX.Objects.IN.INSite.baseCuryID, EqualSiteBaseCuryID<Current2<SOCreate.SOCreateFilter.sourceSiteID>>>>), "The {0} branch specified for the {1} warehouse has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (PX.Objects.IN.INSite.branchID), typeof (PX.Objects.IN.INSite.siteCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOCreate.SOCreateFilter.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<SOCreate.SOCreateFilter.siteID>, IsNull, Or<PX.Objects.IN.INSite.baseCuryID, EqualSiteBaseCuryID<Current2<SOCreate.SOCreateFilter.siteID>>>>), "The {0} branch specified for the {1} warehouse has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (PX.Objects.IN.INSite.branchID), typeof (PX.Objects.IN.INSite.siteCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOCreate.SOCreateFilter.sourceSiteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<SOCreate.SOFixedDemand.siteID>, IsNull, Or<PX.Objects.IN.INSite.baseCuryID, EqualSiteBaseCuryID<Current2<SOCreate.SOFixedDemand.siteID>>>>), "The {0} branch specified for the {1} warehouse has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (PX.Objects.IN.INSite.branchID), typeof (PX.Objects.IN.INSite.siteCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOCreate.SOFixedDemand.sourceSiteID> e)
  {
  }
}
