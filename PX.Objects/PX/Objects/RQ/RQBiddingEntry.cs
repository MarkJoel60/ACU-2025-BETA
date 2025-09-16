// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBiddingEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.RQ;

public class RQBiddingEntry : PXGraph<RQBiddingEntry>
{
  public PXSave<RQBiddingVendor> Save;
  public PXAction<RQBiddingVendor> cancel;
  public RQBiddingEntry.PXBiddingInsert<RQBiddingVendor> Insert;
  public PXDelete<RQBiddingVendor> Delete;
  public PXFirst<RQBiddingVendor> First;
  public PXAction<RQBiddingVendor> previous;
  public PXAction<RQBiddingVendor> next;
  public PXLast<RQBiddingVendor> Last;
  public PXAction<RQRequest> validateAddresses;
  public PXSelectJoin<RQBiddingVendor, InnerJoin<RQRequisition, On<RQRequisition.reqNbr, Equal<RQBiddingVendor.reqNbr>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQBiddingVendor.vendorID>>, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<RQRequisition.customerID>>>>>, Where<RQRequisition.status, Equal<RQRequisitionStatus.bidding>, And2<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> Vendor;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>> BVendor;
  public PXSelect<RQBiddingVendor, Where<RQBiddingVendor.lineID, Equal<Current<RQBiddingVendor.lineID>>>> CurrentDocument;
  public PXSelect<RQRequisitionLineBidding, Where<RQRequisitionLineBidding.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>>> Lines;
  public PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>> Bidding;
  public PXSelect<PX.Objects.PO.PORemitAddress, Where<PX.Objects.PO.PORemitAddress.addressID, Equal<Current<RQBiddingVendor.remitAddressID>>>> Remit_Address;
  public PXSelect<PX.Objects.PO.PORemitContact, Where<PX.Objects.PO.PORemitContact.contactID, Equal<Current<RQBiddingVendor.remitContactID>>>> Remit_Contact;
  public PXSelect<RQRequisitionLine> rqline;
  public PXSelect<RQRequisition> rq;
  public PXSelect<RQRequestLine> reqline;
  public PXSelect<RQRequest> req;
  public CMSetupSelect cmsetup;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<RQBiddingVendor.curyInfoID>>>> currencyinfo;
  public ToggleCurrency<RQBiddingVendor> CurrencyView;

  [PXUIField]
  [PXPreviousButton]
  protected virtual IEnumerable Previous(PXAdapter adapter)
  {
    RQBiddingEntry rqBiddingEntry = this;
    foreach (PXResult<RQBiddingVendor, RQRequisition> pxResult1 in ((PXAction) new PXPrevious<RQBiddingVendor>((PXGraph) rqBiddingEntry, nameof (Previous))).Press(adapter))
    {
      if (((PXSelectBase) rqBiddingEntry.Vendor).Cache.GetStatus((object) PXResult<RQBiddingVendor, RQRequisition>.op_Implicit(pxResult1)) == 2 && adapter.Searches != null)
      {
        adapter.Searches = (object[]) null;
        foreach (PXResult<RQBiddingVendor, RQRequisition> pxResult2 in ((PXAction) rqBiddingEntry.Insert).Press(adapter))
          yield return (object) pxResult2;
      }
      else
        yield return (object) pxResult1;
    }
  }

  [PXUIField]
  [PXNextButton]
  protected virtual IEnumerable Next(PXAdapter adapter)
  {
    RQBiddingEntry rqBiddingEntry = this;
    foreach (PXResult<RQBiddingVendor, RQRequisition> pxResult1 in ((PXAction) new PXNext<RQBiddingVendor>((PXGraph) rqBiddingEntry, nameof (Next))).Press(adapter))
    {
      if (((PXSelectBase) rqBiddingEntry.Vendor).Cache.GetStatus((object) PXResult<RQBiddingVendor, RQRequisition>.op_Implicit(pxResult1)) == 2 && adapter.Searches != null)
      {
        adapter.Searches = (object[]) null;
        foreach (PXResult<RQBiddingVendor, RQRequisition> pxResult2 in ((PXAction) rqBiddingEntry.Insert).Press(adapter))
          yield return (object) pxResult2;
      }
      else
        yield return (object) pxResult1;
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    RQBiddingEntry rqBiddingEntry = this;
    foreach (RQRequest rqRequest in adapter.Get<RQRequest>())
    {
      if (rqRequest != null)
        ((PXGraph) rqBiddingEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) rqRequest;
    }
  }

  [PXDBIdentity]
  protected virtual void RQBiddingVendor_LineID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDefault("")]
  [PXSelector(typeof (Search<RQRequisition.reqNbr, Where<RQRequisition.status, Equal<RQRequisitionStatus.bidding>>>), new System.Type[] {typeof (RQRequisition.status), typeof (RQRequisition.employeeID), typeof (RQRequisition.vendorID)}, Filterable = true)]
  [PXUIField(DisplayName = "Requisition")]
  protected virtual void RQBiddingVendor_ReqNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [VendorNonEmployeeActive(typeof (Search2<BAccountR.bAccountID, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<BAccountR.bAccountID>>>>>))]
  [PXRestrictor(typeof (Where<RQBiddingVendor.reqNbr, IsNotNull>), "Vendor didn't take part in bidding.", new System.Type[] {})]
  protected virtual void RQBiddingVendor_VendorID_CacheAttached(PXCache sender)
  {
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>))]
  protected virtual void RQBiddingVendor_VendorLocationID_CacheAttached(PXCache sender)
  {
  }

  public RQBiddingEntry()
  {
    ((PXSelectBase) this.Lines).Cache.AllowInsert = ((PXSelectBase) this.Lines).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Lines).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLineBidding.minQty>(((PXSelectBase) this.Lines).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLineBidding.quoteNumber>(((PXSelectBase) this.Lines).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLineBidding.quoteQty>(((PXSelectBase) this.Lines).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLineBidding.curyQuoteUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, true);
  }

  protected virtual IEnumerable lines()
  {
    RQBiddingEntry rqBiddingEntry = this;
    if (((PXSelectBase<RQBiddingVendor>) rqBiddingEntry.Vendor).Current != null && ((PXSelectBase<RQBiddingVendor>) rqBiddingEntry.Vendor).Current.VendorLocationID.HasValue)
    {
      using (new ReadOnlyScope(new PXCache[4]
      {
        ((PXSelectBase) rqBiddingEntry.Lines).Cache,
        ((PXSelectBase) rqBiddingEntry.Bidding).Cache,
        ((PXSelectBase) rqBiddingEntry.rqline).Cache,
        ((PXSelectBase) rqBiddingEntry.rq).Cache
      }))
      {
        foreach (PXResult<RQRequisitionLineBidding, RQBidding> pxResult in PXSelectBase<RQRequisitionLineBidding, PXSelectJoin<RQRequisitionLineBidding, LeftJoin<RQBidding, On<RQBidding.reqNbr, Equal<RQRequisitionLineBidding.reqNbr>, And<RQBidding.lineNbr, Equal<RQRequisitionLineBidding.lineNbr>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>>, Where<RQRequisitionLineBidding.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>>>.Config>.Select((PXGraph) rqBiddingEntry, Array.Empty<object>()))
          yield return (object) rqBiddingEntry.PrepareRQRequisitionLineBiddingInViewDelegate(pxResult);
      }
    }
  }

  protected virtual RQRequisitionLineBidding PrepareRQRequisitionLineBiddingInViewDelegate(
    PXResult<RQRequisitionLineBidding, RQBidding> item)
  {
    RQRequisitionLineBidding rqLineBidding = PXResult<RQRequisitionLineBidding, RQBidding>.op_Implicit(item);
    if (((PXSelectBase) this.Lines).Cache.GetStatus((object) rqLineBidding) != 1)
    {
      RQBidding bidding = PXResult<RQRequisitionLineBidding, RQBidding>.op_Implicit(item);
      this.FillRequisitionLineBiddingPropertiesInViewDelegate(rqLineBidding, bidding);
      if (!bidding.LineID.HasValue)
        ((PXSelectBase) this.Lines).Cache.Update((object) rqLineBidding);
      else
        GraphHelper.MarkUpdated(((PXSelectBase) this.Lines).Cache, (object) rqLineBidding, true);
    }
    return rqLineBidding;
  }

  /// <summary>
  /// Fill <see cref="T:PX.Objects.RQ.RQRequisitionLineBidding" /> properties from <see cref="T:PX.Objects.RQ.RQBidding" /> in view delegate. This is an extension point used by Lexware PriceUnit customization.
  /// </summary>
  /// <param name="rqLineBidding">The line bidding.</param>
  /// <param name="bidding">The bidding.</param>
  protected virtual void FillRequisitionLineBiddingPropertiesInViewDelegate(
    RQRequisitionLineBidding rqLineBidding,
    RQBidding bidding)
  {
    rqLineBidding.QuoteNumber = bidding.QuoteNumber;
    rqLineBidding.QuoteQty = new Decimal?(bidding.QuoteQty.GetValueOrDefault());
    rqLineBidding.CuryInfoID = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.CuryInfoID;
    rqLineBidding.CuryQuoteUnitCost = new Decimal?(bidding.CuryQuoteUnitCost.GetValueOrDefault());
    rqLineBidding.CuryQuoteExtCost = new Decimal?(bidding.CuryQuoteExtCost.GetValueOrDefault());
    rqLineBidding.QuoteExtCost = new Decimal?(bidding.QuoteExtCost.GetValueOrDefault());
    rqLineBidding.MinQty = new Decimal?(bidding.MinQty.GetValueOrDefault());
    if (!bidding.CuryQuoteUnitCost.HasValue)
    {
      int? nullable = rqLineBidding.InventoryID;
      if (nullable.HasValue)
      {
        string valueExt = (string) ((PXSelectBase<RQBiddingVendor>) this.Vendor).GetValueExt<RQBiddingVendor.curyID>(((PXSelectBase<RQBiddingVendor>) this.Vendor).Current);
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = POItemCostManager.FetchCuryInfo<RQRequisitionLineBidding.curyInfoID>((PXGraph) this, (object) rqLineBidding);
        int? vendorId = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorID;
        int? vendorLocationId = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorLocationID;
        DateTime? docDate = new DateTime?();
        string curyID = valueExt;
        string baseCuryId = currencyInfo?.BaseCuryID;
        int? inventoryId = rqLineBidding.InventoryID;
        int? subItemId = rqLineBidding.SubItemID;
        nullable = new int?();
        int? siteID = nullable;
        string uom = rqLineBidding.UOM;
        POItemCostManager.ItemCost itemCost = POItemCostManager.Fetch((PXGraph) this, vendorId, vendorLocationId, docDate, curyID, baseCuryId, inventoryId, subItemId, siteID, uom);
        rqLineBidding.CuryQuoteUnitCost = new Decimal?(itemCost.Convert<RQRequisitionLineBidding.inventoryID>((PXGraph) this, (object) rqLineBidding, currencyInfo, rqLineBidding.UOM));
        goto label_4;
      }
    }
    RQRequisitionLineBidding requisitionLineBidding = rqLineBidding;
    Decimal? nullable1 = bidding.QuoteUnitCost;
    Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
    requisitionLineBidding.QuoteUnitCost = nullable2;
label_4:
    nullable1 = rqLineBidding.CuryQuoteUnitCost;
    if (nullable1.HasValue)
      return;
    rqLineBidding.CuryQuoteUnitCost = new Decimal?(0M);
  }

  protected virtual void RQRequisitionLineBidding_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<RQBiddingVendor> e)
  {
    RQBiddingVendor row = e.Row;
    if (row == null || string.IsNullOrEmpty(row.ShipVia))
      return;
    PX.Objects.CS.Carrier carrier = (PX.Objects.CS.Carrier) PXSelectorAttribute.Select<RQBiddingVendor.shipVia>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<RQBiddingVendor>>) e).Cache, (object) row);
    int num;
    if (carrier == null)
    {
      num = 0;
    }
    else
    {
      bool? isActive = carrier.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<RQBiddingVendor>>) e).Cache.RaiseExceptionHandling<RQBiddingVendor.shipVia>((object) row, (object) row.ShipVia, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", (PXErrorLevel) 2));
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Lines).Cache.Clear();
  }

  protected virtual void RQRequisitionLineBidding_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    GraphHelper.MarkUpdated(((PXSelectBase) this.Vendor).Cache, (object) ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current, true);
    RQRequisitionLineBidding row = (RQRequisitionLineBidding) e.Row;
    RQRequisitionLineBidding oldRow = (RQRequisitionLineBidding) e.OldRow;
    Decimal? minQty1 = row.MinQty;
    Decimal? minQty2 = oldRow.MinQty;
    if (minQty1.GetValueOrDefault() == minQty2.GetValueOrDefault() & minQty1.HasValue == minQty2.HasValue)
    {
      Decimal? nullable = row.QuoteUnitCost;
      Decimal? quoteUnitCost = oldRow.QuoteUnitCost;
      if (nullable.GetValueOrDefault() == quoteUnitCost.GetValueOrDefault() & nullable.HasValue == quoteUnitCost.HasValue)
      {
        Decimal? quoteQty = row.QuoteQty;
        nullable = oldRow.QuoteQty;
        if (quoteQty.GetValueOrDefault() == nullable.GetValueOrDefault() & quoteQty.HasValue == nullable.HasValue && !(row.QuoteNumber != oldRow.QuoteNumber))
          return;
      }
    }
    ((PXSelectBase<RQBidding>) this.Bidding).Update(this.GetRQBiddingOnRequisitionLineBiddingRowUpdatedEvent(row));
  }

  protected virtual RQBidding GetRQBiddingOnRequisitionLineBiddingRowUpdatedEvent(
    RQRequisitionLineBidding updatedRQLineBidding)
  {
    RQBidding rqBidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.lineNbr, Equal<Required<RQBidding.lineNbr>>, And<RQBidding.vendorID, Equal<Required<RQBidding.vendorID>>, And<RQBidding.vendorLocationID, Equal<Required<RQBidding.vendorLocationID>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.ReqNbr,
      (object) updatedRQLineBidding.LineNbr,
      (object) ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorID,
      (object) ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorLocationID
    }));
    if (rqBidding == null)
      rqBidding = ((PXSelectBase<RQBidding>) this.Bidding).Insert(new RQBidding()
      {
        VendorID = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorID,
        VendorLocationID = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.VendorLocationID,
        ReqNbr = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.ReqNbr,
        CuryInfoID = ((PXSelectBase<RQBiddingVendor>) this.Vendor).Current.CuryInfoID,
        LineNbr = updatedRQLineBidding.LineNbr
      });
    RQBidding copy = (RQBidding) ((PXSelectBase) this.Bidding).Cache.CreateCopy((object) rqBidding);
    copy.QuoteQty = updatedRQLineBidding.QuoteQty;
    copy.QuoteNumber = updatedRQLineBidding.QuoteNumber;
    copy.QuoteUnitCost = updatedRQLineBidding.QuoteUnitCost;
    copy.CuryQuoteUnitCost = updatedRQLineBidding.CuryQuoteUnitCost;
    copy.MinQty = updatedRQLineBidding.MinQty;
    return copy;
  }

  protected virtual void RQBiddingVendor_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null)
      return;
    if (!row.EntryDate.HasValue)
      row.EntryDate = sender.Graph.Accessinfo.BusinessDate;
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current != null && ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current.AllowOverrideCury.GetValueOrDefault())
    {
      RQBidding rqBidding = (RQBidding) ((PXSelectBase) this.Bidding).View.SelectSingleBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>());
      PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyID>(sender, e.Row, rqBidding == null);
    }
    else
      PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyID>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyTotalQuoteExtCost>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.totalQuoteExtCost>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.totalQuoteQty>(sender, e.Row, false);
    ((PXAction) this.validateAddresses).SetEnabled(((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation());
  }

  protected virtual void RQBiddingVendor_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if ((RQBiddingVendor) e.Row != null)
    {
      SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(sender, e.Row);
    }
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current = (PX.Objects.AP.Vendor) ((PXSelectBase) this.BVendor).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
  }

  protected virtual void RQBiddingVendor_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || row.ReqNbr == null || !row.VendorID.HasValue)
      return;
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current = (PX.Objects.AP.Vendor) ((PXSelectBase) this.BVendor).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    PX.Objects.CM.CurrencyInfoAttribute.SetDefaults<RQBiddingVendor.curyInfoID>(sender, e.Row);
    sender.SetDefaultExt<RQBiddingVendor.curyID>(e.Row);
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current;
    if (current == null || string.IsNullOrEmpty(current.CuryID))
      return;
    e.NewValue = (object) current.CuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current;
    if (current == null)
      return;
    e.NewValue = (object) (current.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Vendor).Cache.Current == null)
      return;
    e.NewValue = (object) ((RQBiddingVendor) ((PXSelectBase) this.Vendor).Cache.Current).EntryDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool? nullable = row.IsReadOnly;
    bool flag1 = !nullable.GetValueOrDefault();
    bool flag2 = row.AllowUpdate(((PXSelectBase) this.Bidding).Cache);
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current != null)
    {
      nullable = ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current.AllowOverrideCury;
      if (!nullable.GetValueOrDefault())
        flag1 = false;
      nullable = ((PXSelectBase<PX.Objects.AP.Vendor>) this.BVendor).Current.AllowOverrideRate;
      if (!nullable.GetValueOrDefault())
        flag2 = false;
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag2);
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    RQBiddingEntry rqBiddingEntry = this;
    if (a.Searches != null && a.Searches.Length == 3)
    {
      if (((PXSelectBase<RQBiddingVendor>) rqBiddingEntry.Vendor).Current != null && ((PXSelectBase<RQBiddingVendor>) rqBiddingEntry.Vendor).Current.ReqNbr != (string) a.Searches[0])
      {
        PXResult<RQBiddingVendor, PX.Objects.AP.Vendor, PX.Objects.CR.Location> pxResult = (PXResult<RQBiddingVendor, PX.Objects.AP.Vendor, PX.Objects.CR.Location>) PXResultset<RQBiddingVendor>.op_Implicit(PXSelectBase<RQBiddingVendor, PXSelectJoin<RQBiddingVendor, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQBiddingVendor.vendorID>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>>>>>, Where<RQBiddingVendor.reqNbr, Equal<Required<RQBiddingVendor.reqNbr>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>.Config>.Select((PXGraph) rqBiddingEntry, new object[1]
        {
          (object) (string) a.Searches[0]
        }));
        PX.Objects.AP.Vendor vendor = (PX.Objects.AP.Vendor) null;
        PX.Objects.CR.Location location = (PX.Objects.CR.Location) null;
        if (pxResult != null)
        {
          vendor = PXResult<RQBiddingVendor, PX.Objects.AP.Vendor, PX.Objects.CR.Location>.op_Implicit(pxResult);
          location = PXResult<RQBiddingVendor, PX.Objects.AP.Vendor, PX.Objects.CR.Location>.op_Implicit(pxResult);
        }
        if (vendor == null || !vendor.BAccountID.HasValue)
        {
          a.Searches[1] = (object) null;
          a.Searches[2] = (object) null;
        }
        else
        {
          a.Searches[1] = (object) vendor.AcctCD;
          a.Searches[2] = (object) location.LocationCD;
        }
      }
      else
      {
        if (a.Searches[1] != null && a.Searches[2] != null)
        {
          if (PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>, Where<PX.Objects.AP.Vendor.acctCD, Equal<Required<PX.Objects.AP.Vendor.acctCD>>, And<PX.Objects.CR.Location.locationCD, Equal<Required<PX.Objects.CR.Location.locationCD>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>.Config>.SelectWindowed((PXGraph) rqBiddingEntry, 0, 1, new object[2]
          {
            a.Searches[1],
            a.Searches[2]
          })) == null)
            a.Searches[2] = (object) null;
        }
        if (a.Searches[1] != null && a.Searches[2] == null)
        {
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.AP.Vendor.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.AP.Vendor.acctCD, Equal<Required<PX.Objects.AP.Vendor.acctCD>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>.Config>.SelectWindowed((PXGraph) rqBiddingEntry, 0, 1, new object[1]
          {
            a.Searches[1]
          }));
          if (location != null)
            a.Searches[2] = (object) location.LocationCD;
        }
      }
    }
    foreach (object obj in ((PXAction) new PXCancel<RQBiddingVendor>((PXGraph) rqBiddingEntry, nameof (Cancel))).Press(a))
      yield return obj;
  }

  public class RQBiddingEntryRemitAddressCachingHelper : 
    AddressValidationExtension<RQBiddingEntry, PX.Objects.PO.PORemitAddress>
  {
    protected override IEnumerable<PXSelectBase<PX.Objects.PO.PORemitAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RQBiddingEntry.RQBiddingEntryRemitAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<PX.Objects.PO.PORemitAddress>) addressCachingHelper.Base.Remit_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class PXBiddingInsert<TNode>(PXGraph graph, string name) : PXInsert<TNode>(graph, name) where TNode : RQBiddingVendor, new()
  {
    [PXInsertButton]
    [PXUIField]
    protected virtual IEnumerable Handler(PXAdapter adapter)
    {
      List<object> objectList = new List<object>();
      foreach (object obj in base.Handler(adapter))
      {
        RQBiddingVendor rqBiddingVendor = obj is PXResult ? (RQBiddingVendor) ((PXResult) obj)[0] : (RQBiddingVendor) obj;
        rqBiddingVendor.VendorID = new int?();
        rqBiddingVendor.VendorLocationID = new int?();
        objectList.Add(obj);
        foreach (System.Type key in ((Dictionary<System.Type, PXCache>) ((PXAction) this)._Graph.Caches).Keys)
          ((PXAction) this)._Graph.Caches[key].IsDirty = false;
      }
      return (IEnumerable) objectList;
    }
  }
}
