// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<InventoryItemMaintBase>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<INSite.baseCuryID, Equal<Current2<InventoryItemCurySettings.curyID>>, Or<INSite.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>>>), "The {0} branch specified for the {1} warehouse has other base currency than the {2} branch that is currently selected.", new Type[] {typeof (INSite.branchID), typeof (INSite.siteCD), typeof (Current<AccessInfo.branchID>)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<InventoryItemCurySettings.dfltSiteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>), "The {0} vendor has other base currency than the current branch.", new Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<POVendorInventory.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<INSite.baseCuryID, Equal<Current2<INItemRep.curyID>>, Or<INSite.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>>>), "The {0} branch specified for the {1} warehouse has other base currency than the {2} branch that is currently selected.", new Type[] {typeof (INSite.branchID), typeof (INSite.siteCD), typeof (Current<AccessInfo.branchID>)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INItemRep.replenishmentSourceSiteID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<InventoryItemCurySettings> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<InventoryItemCurySettings>>) e).Cache.VerifyFieldAndRaiseException<InventoryItemCurySettings.dfltSiteID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemRep> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INItemRep>>) e).Cache.VerifyFieldAndRaiseException<INItemRep.replenishmentSourceSiteID>((object) e.Row);
  }
}
