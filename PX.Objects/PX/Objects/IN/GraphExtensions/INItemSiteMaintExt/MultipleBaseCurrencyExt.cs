// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemSiteMaintExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INItemSiteMaintExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<INItemSiteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<INSite.baseCuryID, EqualSiteBaseCuryID<Current<INItemSite.siteID>>>), "The base currency of the {0} branch differs from the base currency of the {1} branch specified for the {2} warehouse.", new Type[] {typeof (Selector<INItemSite.siteID, INSite.branchID>), typeof (INSite.branchID), typeof (INSite.siteCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemSite.replenishmentSourceSiteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.baseCuryID, EqualSiteBaseCuryID<Current2<INItemSite.siteID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>), "The {0} vendor has other base currency than the branch of the currently selected warehouse.", new Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemSite.preferredVendorID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemSite> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INItemSite>>) e).Cache.VerifyFieldAndRaiseException<INItemSite.replenishmentSourceSiteID>((object) e.Row);
  }
}
