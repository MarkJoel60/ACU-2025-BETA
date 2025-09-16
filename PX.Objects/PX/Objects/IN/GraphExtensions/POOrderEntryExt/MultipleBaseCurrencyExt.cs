// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POOrderEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.PO;
using PX.Objects.PO.DAC.Unbound;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.POOrderEntryExt;

public class MultipleBaseCurrencyExt : 
  MultipleBaseCurrencyExtBase<POOrderEntry, PX.Objects.PO.POOrder, PX.Objects.PO.POLine, PX.Objects.PO.POOrder.branchID, PX.Objects.PO.POLine.branchID, PX.Objects.PO.POLine.siteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.siteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<EntryHeader.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<PX.Objects.PO.POOrder.branchID>, IsNull, Or<Customer.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POOrder.branchID>>, Or<Customer.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (Customer.cOrgBAccountID), typeof (Customer.acctCD)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CreateSOOrderFilter.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<PX.Objects.PO.POOrder.branchID>, IsNull, Or<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POOrder.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POOrder.branchID>>, Or<PX.Objects.AP.Vendor.baseCuryID, IsNull>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (PX.Objects.AP.Vendor.vOrgBAccountID), typeof (PX.Objects.AP.Vendor.acctCD)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.payToVendorID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PX.Objects.PO.POOrder.branchID), null)]
  protected virtual void _(PX.Data.Events.CacheAttached<POSiteStatusFilter.siteID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POOrder> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POOrder>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.PO.POOrder.branchID>((object) e.Row);
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POOrder>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.PO.POOrder.payToVendorID>((object) e.Row);
  }

  protected override void OnLineBaseCuryChanged(PXCache cache, PX.Objects.PO.POLine row)
  {
    base.OnLineBaseCuryChanged(cache, row);
    cache.SetDefaultExt<PX.Objects.PO.POLine.curyUnitCost>((object) row);
  }

  protected override PXSelectBase<PX.Objects.PO.POLine> GetTransactionView()
  {
    return (PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions;
  }

  public sealed class POOrderMultipleBaseCurrenciesRestriction : 
    PXCacheExtension<POOrderVisibilityRestriction, PX.Objects.PO.POOrder>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    }

    [PXMergeAttributes]
    [PXRestrictor(typeof (Where<Current2<PX.Objects.PO.POOrder.branchID>, IsNull, Or<Customer.baseCuryID, EqualBaseCuryID<Current2<PX.Objects.PO.POOrder.branchID>>, Or<Customer.baseCuryID, IsNull, Or<Current<PX.Objects.PO.POOrder.shipDestType>, NotEqual<POShippingDestination.customer>, Or<Current<PX.Objects.PO.POOrder.orderType>, NotEqual<POOrderType.dropShip>>>>>>), "The branch base currency differs from the base currency of the {0} entity associated with the {1} business account.", new Type[] {typeof (Customer.cOrgBAccountID), typeof (Customer.acctCD)})]
    public int? ShipToBAccountID { get; set; }
  }
}
