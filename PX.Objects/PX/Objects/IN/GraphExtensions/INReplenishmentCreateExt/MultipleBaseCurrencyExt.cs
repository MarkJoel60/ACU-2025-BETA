// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReplenishmentCreateExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReplenishmentCreateExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<INReplenishmentCreate>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<INSite.baseCuryID, EqualSiteBaseCuryID<Current2<INReplenishmentFilter.replenishmentSiteID>>>), "The {0} branch specified for the {1} warehouse has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (INSite.branchID), typeof (INSite.siteCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INReplenishmentItem.replenishmentSourceSiteID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.baseCuryID, EqualSiteBaseCuryID<Current2<INReplenishmentFilter.replenishmentSiteID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>), "The {0} vendor has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INReplenishmentItem.preferredVendorID> eventArgs)
  {
  }
}
