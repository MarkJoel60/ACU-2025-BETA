// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBiddingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.RQ;

[TableAndChartDashboardType]
public class RQBiddingProcess : PXGraph<RQBiddingProcess>
{
  public PXSave<RQRequisition> Save;
  public PXCancel<RQRequisition> Cancel;
  public PXFirst<RQRequisition> First;
  public PXPrevious<RQRequisition> Previous;
  public PXNext<RQRequisition> Next;
  public PXLast<RQRequisition> Last;
  public PXFilter<RQBiddingState> State;
  public PXSelectJoin<RQRequisition, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<RQRequisition.customerID>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequisition.vendorID>>>>, Where2<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>, And2<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, And<Where<RQRequisition.status, Equal<RQRequisitionStatus.bidding>, Or<RQRequisition.status, Equal<RQRequisitionStatus.closed>, Or<RQRequisition.status, Equal<RQRequisitionStatus.open>, Or<RQRequisition.status, Equal<RQRequisitionStatus.pendingQuotation>, Or<RQRequisition.status, Equal<RQRequisitionStatus.released>>>>>>>>>> Document;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>> Vendor;
  public PXSelectJoin<RQBiddingVendor, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQBiddingVendor.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<RQBiddingVendor.vendorLocationID>>>>, Where<RQBiddingVendor.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> Vendors;
  public PXSelect<RQBiddingVendor, Where<RQBiddingVendor.reqNbr, Equal<Required<RQRequisition.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Required<RQBiddingVendor.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Required<RQBiddingVendor.vendorLocationID>>>>>> ChoosenVendor;
  public PXSelect<PX.Objects.PO.PORemitAddress, Where<PX.Objects.PO.PORemitAddress.addressID, Equal<Current<RQBiddingVendor.remitAddressID>>>> Remit_Address;
  public PXSelect<PX.Objects.PO.PORemitContact, Where<PX.Objects.PO.PORemitContact.contactID, Equal<Current<RQBiddingVendor.remitContactID>>>> Remit_Contact;
  public PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> Lines;
  public PXSelectJoin<RQBidding, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<RQBidding.reqNbr>, And<RQBiddingVendor.vendorID, Equal<RQBidding.vendorID>, And<RQBiddingVendor.vendorLocationID, Equal<RQBidding.vendorLocationID>>>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQBidding.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<RQBidding.vendorLocationID>>>>>, Where<RQBidding.reqNbr, Equal<Argument<string>>, And<RQBidding.lineNbr, Equal<Argument<int?>>>>> Bidding;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Optional<RQBiddingVendor.curyInfoID>>>> currencyinfo;
  public ToggleCurrency<RQRequisition> CurrencyView;
  public CMSetupSelect cmsetup;
  public PXAction<RQRequisition> ChooseVendor;
  public PXAction<RQRequisition> VendorInfo;
  public PXAction<RQRequisition> Process;
  public PXAction<RQRequisition> UpdateResult;
  public PXAction<RQRequisition> ClearResult;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (RQSetup.requisitionNumberingID), typeof (RQRequisition.orderDate))]
  [PXSelector(typeof (Search<RQRequisition.reqNbr, Where<RQRequisition.status, Equal<RQRequisitionStatus.bidding>, Or<RQRequisition.status, Equal<RQRequisitionStatus.open>, Or<RQRequisition.status, Equal<RQRequisitionStatus.closed>, Or<RQRequisition.status, Equal<RQRequisitionStatus.pendingQuotation>, Or<RQRequisition.status, Equal<RQRequisitionStatus.released>>>>>>>), new System.Type[] {typeof (RQRequisition.status), typeof (RQRequisition.priority), typeof (RQRequisition.orderDate)}, Filterable = true)]
  protected virtual void RQRequisition_ReqNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField]
  protected virtual void RQBiddingVendor_EntryDate_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable bidding([PXDBString] string reqNbr, [PXDBInt] int? lineNbr)
  {
    RQBiddingProcess rqBiddingProcess1 = this;
    ((PXSelectBase<RQBiddingState>) rqBiddingProcess1.State).Current.ReqNbr = reqNbr ?? (((PXSelectBase<RQRequisitionLine>) rqBiddingProcess1.Lines).Current != null ? ((PXSelectBase<RQRequisitionLine>) rqBiddingProcess1.Lines).Current.ReqNbr : (string) null);
    RQBiddingState current = ((PXSelectBase<RQBiddingState>) rqBiddingProcess1.State).Current;
    int? nullable1 = lineNbr;
    int? nullable2 = nullable1 ?? (((PXSelectBase<RQRequisitionLine>) rqBiddingProcess1.Lines).Current != null ? ((PXSelectBase<RQRequisitionLine>) rqBiddingProcess1.Lines).Current.LineNbr : new int?());
    current.LineNbr = nullable2;
    RQBiddingProcess rqBiddingProcess2 = rqBiddingProcess1;
    object[] objArray = new object[2]
    {
      (object) ((PXSelectBase<RQBiddingState>) rqBiddingProcess1.State).Current.ReqNbr,
      (object) ((PXSelectBase<RQBiddingState>) rqBiddingProcess1.State).Current.LineNbr
    };
    foreach (PXResult<RQBidding, RQBiddingVendor, PX.Objects.CR.Location> pxResult in PXSelectBase<RQBidding, PXSelectJoin<RQBidding, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<RQBidding.reqNbr>, And<RQBiddingVendor.vendorID, Equal<RQBidding.vendorID>, And<RQBiddingVendor.vendorLocationID, Equal<RQBidding.vendorLocationID>>>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQBidding.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<RQBidding.vendorLocationID>>>>>, Where<RQBidding.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) rqBiddingProcess2, objArray))
    {
      RQBidding rqBidding1 = PXResult<RQBidding, RQBiddingVendor, PX.Objects.CR.Location>.op_Implicit(pxResult);
      RQBidding rqBidding2 = rqBidding1;
      Decimal? orderQty = rqBidding1.OrderQty;
      Decimal num1 = 0M;
      int num2;
      if (!(orderQty.GetValueOrDefault() > num1 & orderQty.HasValue))
      {
        nullable1 = rqBidding1.VendorID;
        int? nullable3 = ((PXSelectBase<RQRequisition>) rqBiddingProcess1.Document).Current.VendorID;
        if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
        {
          nullable3 = rqBidding1.VendorLocationID;
          nullable1 = ((PXSelectBase<RQRequisition>) rqBiddingProcess1.Document).Current.VendorLocationID;
          num2 = nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
      }
      else
        num2 = 1;
      bool? nullable4 = new bool?(num2 != 0);
      rqBidding2.Selected = nullable4;
      yield return (object) pxResult;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Select Vendor")]
  public virtual IEnumerable chooseVendor(PXAdapter adapter)
  {
    RQBiddingProcess rqBiddingProcess = this;
    RQBiddingVendor vendor = PXCache<RQBiddingVendor>.CreateCopy(((PXSelectBase<RQBiddingVendor>) rqBiddingProcess.Vendors).Current);
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (vendor != null)
      {
        bool flag = false;
        PXView view = ((PXSelectBase) rqBiddingProcess.Lines).View;
        object[] objArray1 = new object[1]
        {
          (object) rqRequisition
        };
        object[] objArray2 = Array.Empty<object>();
        foreach (RQRequisitionLine rqRequisitionLine in view.SelectMultiBound(objArray1, objArray2))
        {
          RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) rqBiddingProcess.Lines).Cache.CreateCopy((object) rqRequisitionLine);
          RQBidding rqBidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) rqBiddingProcess, new object[2]
          {
            (object) copy,
            (object) vendor
          }, Array.Empty<object>()));
          if (rqBidding == null)
          {
            ((PXSelectBase) rqBiddingProcess.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.orderQty>((object) rqRequisitionLine, (object) rqRequisitionLine.OrderQty, (Exception) new PXException("Bidding result is empty."));
            flag = true;
          }
          else
          {
            Decimal? nullable1 = rqBidding.MinQty;
            Decimal num1 = 0M;
            if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
            {
              nullable1 = rqBidding.QuoteQty;
              Decimal num2 = 0M;
              if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
                continue;
            }
            nullable1 = rqBidding.MinQty;
            Decimal? nullable2 = rqRequisitionLine.OrderQty;
            if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            {
              ((PXSelectBase) rqBiddingProcess.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.orderQty>((object) rqRequisitionLine, (object) rqRequisitionLine.OrderQty, (Exception) new PXException("Order qty less than minimal qty specified by the vendor"));
              flag = true;
            }
            nullable2 = rqBidding.QuoteQty;
            nullable1 = rqRequisitionLine.OrderQty;
            if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
            {
              ((PXSelectBase) rqBiddingProcess.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.orderQty>((object) rqRequisitionLine, (object) rqRequisitionLine.OrderQty, (Exception) new PXException("Order qty more than quote qty specified by the vendor"));
              flag = true;
            }
          }
        }
        if (flag)
          throw new PXException("Unable to process operation, some lines doesn't contain valid quotation information.");
        foreach (PXResult<RQBidding> pxResult in PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.orderQty, Greater<Required<RQBidding.orderQty>>>>>.Config>.Select((PXGraph) rqBiddingProcess, new object[2]
        {
          (object) rqRequisition.ReqNbr,
          (object) 0M
        }))
        {
          RQBidding rqBidding = PXResult<RQBidding>.op_Implicit(pxResult);
          RQBidding copy = (RQBidding) ((PXSelectBase) rqBiddingProcess.Bidding).Cache.CreateCopy((object) rqBidding);
          copy.OrderQty = new Decimal?(0M);
          ((PXSelectBase<RQBidding>) rqBiddingProcess.Bidding).Update(copy);
        }
        yield return (object) rqBiddingProcess.DoChooseVendor(rqRequisition, vendor);
      }
      else
        yield return (object) rqRequisition;
    }
  }

  protected virtual void CopyUnitCost(RQRequisitionLine line, RQBidding bidding)
  {
    Decimal? minQty1 = bidding.MinQty;
    Decimal num1 = 0M;
    if (minQty1.GetValueOrDefault() == num1 & minQty1.HasValue)
    {
      Decimal? quoteQty = bidding.QuoteQty;
      Decimal num2 = 0M;
      if (quoteQty.GetValueOrDefault() == num2 & quoteQty.HasValue)
        goto label_4;
    }
    Decimal? minQty2 = bidding.MinQty;
    Decimal? orderQty1 = line.OrderQty;
    if (!(minQty2.GetValueOrDefault() <= orderQty1.GetValueOrDefault() & minQty2.HasValue & orderQty1.HasValue))
      return;
    Decimal? quoteQty1 = bidding.QuoteQty;
    Decimal? orderQty2 = line.OrderQty;
    if (!(quoteQty1.GetValueOrDefault() >= orderQty2.GetValueOrDefault() & quoteQty1.HasValue & orderQty2.HasValue))
      return;
label_4:
    RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) line);
    Decimal curyval;
    PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(((PXSelectBase) this.Lines).Cache, (object) copy, bidding.QuoteUnitCost.GetValueOrDefault(), out curyval, true);
    copy.CuryEstUnitCost = new Decimal?(curyval);
    ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
  }

  private RQRequisition DoChooseVendor(RQRequisition item, RQBiddingVendor vendor)
  {
    if (!vendor.RemitContactID.HasValue)
      SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(((PXGraph) this).Caches[typeof (RQBiddingVendor)], (object) vendor);
    if (!vendor.RemitAddressID.HasValue)
      SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(((PXGraph) this).Caches[typeof (RQBiddingVendor)], (object) vendor);
    RQRequisition copy = (RQRequisition) ((PXSelectBase) this.Document).Cache.CreateCopy((object) item);
    copy.VendorID = vendor.VendorID;
    copy.VendorLocationID = vendor.VendorLocationID;
    copy.RemitAddressID = vendor.RemitAddressID;
    copy.RemitContactID = vendor.RemitContactID;
    copy.ShipVia = vendor.ShipVia;
    copy.FOBPoint = vendor.FOBPoint;
    RQRequisition rqRequisition = PXCache<RQRequisition>.CreateCopy(((PXSelectBase<RQRequisition>) this.Document).Update(copy));
    if (!rqRequisition.CustomerID.HasValue && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).SelectWindowed(0, 1, new object[1]
      {
        (object) vendor.CuryInfoID
      }));
      PX.Objects.CM.CurrencyInfo currencyInfo2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).SelectWindowed(0, 1, new object[1]
      {
        (object) rqRequisition.CuryInfoID
      })));
      if (!((PXSelectBase) this.currencyinfo).Cache.ObjectsEqual<PX.Objects.CM.CurrencyInfo.curyID, PX.Objects.CM.CurrencyInfo.curyRateTypeID, PX.Objects.CM.CurrencyInfo.curyRate, PX.Objects.CM.CurrencyInfo.curyEffDate>((object) currencyInfo2, (object) currencyInfo1))
      {
        long? curyInfoId1 = currencyInfo2.CuryInfoID;
        PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo2, currencyInfo1);
        currencyInfo2.CuryInfoID = curyInfoId1;
        try
        {
          PX.Objects.CM.PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, false);
          long? curyInfoId2 = (long?) CurrencyCollection.GetCurrency(currencyInfo2.BaseCuryID)?.CuryInfoID;
          long? curyInfoId3 = currencyInfo2.CuryInfoID;
          if (!(curyInfoId2.GetValueOrDefault() == curyInfoId3.GetValueOrDefault() & curyInfoId2.HasValue == curyInfoId3.HasValue))
          {
            long? curyInfoId4 = (long?) CurrencyCollection.GetBaseCurrency()?.CuryInfoID;
            long? curyInfoId5 = currencyInfo2.CuryInfoID;
            if (!(curyInfoId4.GetValueOrDefault() == curyInfoId5.GetValueOrDefault() & curyInfoId4.HasValue == curyInfoId5.HasValue))
            {
              ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Update(currencyInfo2);
              goto label_12;
            }
          }
          currencyInfo2.CuryInfoID = new long?();
          currencyInfo2 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(currencyInfo2);
          rqRequisition.CuryInfoID = currencyInfo2.CuryInfoID;
          rqRequisition = ((PXSelectBase<RQRequisition>) this.Document).Update(rqRequisition);
        }
        finally
        {
          PX.Objects.CM.PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, true);
        }
label_12:
        string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo2);
        if (!string.IsNullOrEmpty(error))
          ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<RQRequisition.orderDate>((object) item, (object) item.OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
        rqRequisition.CuryID = currencyInfo2.CuryID;
        rqRequisition = ((PXSelectBase<RQRequisition>) this.Document).Update(rqRequisition);
      }
    }
    PXView view = ((PXSelectBase) this.Lines).View;
    object[] objArray1 = new object[1]
    {
      (object) rqRequisition
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (RQRequisitionLine line in view.SelectMultiBound(objArray1, objArray2))
    {
      RQBidding bidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
      {
        (object) line,
        (object) vendor
      }, Array.Empty<object>()));
      if (bidding != null)
        this.CopyUnitCost(line, bidding);
    }
    return PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) this.Document).Search<RQRequisition.reqNbr>((object) rqRequisition.ReqNbr, Array.Empty<object>()));
  }

  [PXButton]
  [PXUIField(DisplayName = "Vendor Info")]
  public virtual IEnumerable vendorInfo(PXAdapter adapter)
  {
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (((PXSelectBase<RQBiddingVendor>) this.Vendors).Current != null)
        ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).AskExt();
      yield return (object) rqRequisition;
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Complete Bidding")]
  public virtual IEnumerable process(PXAdapter adapter)
  {
    RQBiddingProcess rqBiddingProcess = this;
    bool hasChanges = false;
    Lazy<RQRequisitionEntry> reqEntry = PXGraph.CreateLazyInstance<RQRequisitionEntry>();
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      ((PXSelectBase<RQRequisition>) rqBiddingProcess.Document).Current = rqRequisition;
      bool flag = false;
      if (rqRequisition.Splittable.GetValueOrDefault())
      {
        if (!rqRequisition.VendorID.HasValue || !rqRequisition.VendorLocationID.HasValue)
        {
          int? nullable1 = new int?();
          int? nullable2 = new int?();
          Dictionary<int?, List<RQBidding>> dictionary = new Dictionary<int?, List<RQBidding>>();
          foreach (PXResult<RQBidding> pxResult in PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.orderQty, Greater<decimal0>>>>.Config>.Select((PXGraph) rqBiddingProcess, new object[1]
          {
            (object) rqRequisition.ReqNbr
          }))
          {
            RQBidding rqBidding = PXResult<RQBidding>.op_Implicit(pxResult);
            List<RQBidding> rqBiddingList;
            if (!dictionary.TryGetValue(rqBidding.LineNbr, out rqBiddingList))
              dictionary[rqBidding.LineNbr] = rqBiddingList = new List<RQBidding>();
            rqBiddingList.Add(rqBidding);
          }
          foreach (PXResult<RQRequisitionLine> pxResult in ((PXSelectBase<RQRequisitionLine>) rqBiddingProcess.Lines).Select(new object[1]
          {
            (object) rqRequisition.ReqNbr
          }))
          {
            RQRequisitionLine line = PXResult<RQRequisitionLine>.op_Implicit(pxResult);
            Decimal? orderQty1 = line.OrderQty;
            Decimal? biddingQty = line.BiddingQty;
            if (orderQty1.GetValueOrDefault() > biddingQty.GetValueOrDefault() & orderQty1.HasValue & biddingQty.HasValue)
            {
              ((PXSelectBase) rqBiddingProcess.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.biddingQty>((object) line, (object) line.BiddingQty, (Exception) new PXSetPropertyException("Bidding Qty cannot be lower than Order Qty"));
              flag = true;
              nullable2 = new int?(-1);
              break;
            }
            List<RQBidding> source;
            if (dictionary.TryGetValue(line.LineNbr, out source))
            {
              if (source.Count == 1)
                rqBiddingProcess.CopyUnitCost(line, source[0]);
              foreach (RQBidding rqBidding in source.Where<RQBidding>((Func<RQBidding, bool>) (bidding =>
              {
                Decimal? orderQty2 = bidding.OrderQty;
                Decimal num = 0M;
                return orderQty2.GetValueOrDefault() > num & orderQty2.HasValue;
              })))
              {
                if (!nullable2.HasValue)
                {
                  nullable1 = rqBidding.VendorID;
                  nullable2 = rqBidding.VendorLocationID;
                }
                int? nullable3 = nullable2;
                int num = 0;
                if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
                {
                  int? nullable4 = nullable2;
                  int? vendorLocationId = rqBidding.VendorLocationID;
                  if (!(nullable4.GetValueOrDefault() == vendorLocationId.GetValueOrDefault() & nullable4.HasValue == vendorLocationId.HasValue))
                  {
                    nullable2 = new int?(-1);
                    break;
                  }
                }
              }
            }
          }
          int? nullable5 = nullable2;
          int num1 = 0;
          if (nullable5.GetValueOrDefault() > num1 & nullable5.HasValue)
          {
            RQBiddingVendor vendor = PXResultset<RQBiddingVendor>.op_Implicit(((PXSelectBase<RQBiddingVendor>) rqBiddingProcess.ChoosenVendor).SelectWindowed(0, 1, new object[3]
            {
              (object) rqRequisition.ReqNbr,
              (object) nullable1,
              (object) nullable2
            }));
            if (vendor != null)
              rqBiddingProcess.DoChooseVendor(rqRequisition, vendor);
          }
        }
      }
      else if (!rqRequisition.VendorID.HasValue)
      {
        ((PXSelectBase) rqBiddingProcess.Document).Cache.RaiseExceptionHandling<RQRequisition.vendorID>((object) rqRequisition, (object) null, (Exception) new PXSetPropertyException("Select a vendor for requisition."));
        flag = true;
      }
      else if (!rqRequisition.VendorLocationID.HasValue)
      {
        ((PXSelectBase) rqBiddingProcess.Document).Cache.RaiseExceptionHandling<RQRequisition.vendorLocationID>((object) rqRequisition, (object) null, (Exception) new PXSetPropertyException("Select a vendor location for requisition."));
        flag = true;
      }
      if (!flag)
      {
        RQRequisition requisition = PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) reqEntry.Value.Document).Search<RQRequisition.reqNbr>((object) rqRequisition.ReqNbr, Array.Empty<object>()));
        RQRequisition copy1 = PXCache<RQRequisition>.CreateCopy(requisition);
        requisition.CompleteBidding((PXGraph) reqEntry.Value);
        RQRequisition copy2 = PXCache<RQRequisition>.CreateCopy(rqRequisition);
        foreach (KeyValuePair<string, (object, object)> keyValuePair in (IEnumerable<KeyValuePair<string, (object, object)>>) PXCacheEx.GetDifference(((PXSelectBase) reqEntry.Value.Document).Cache, (IBqlTable) copy1, (IBqlTable) requisition, false))
          ((PXSelectBase) rqBiddingProcess.Document).Cache.SetValue((object) copy2, keyValuePair.Key, keyValuePair.Value.Item2);
        hasChanges = true;
        yield return ((PXSelectBase) rqBiddingProcess.Document).Cache.Update((object) copy2);
      }
      else
        yield return (object) rqRequisition;
    }
    if (hasChanges)
      ((PXAction) rqBiddingProcess.Save).Press();
  }

  [PXButton]
  [PXUIField(DisplayName = "Update Result")]
  public virtual IEnumerable updateResult(PXAdapter adapter)
  {
    RQBiddingProcess rqBiddingProcess = this;
    foreach (RQRequisition rqRequisition1 in adapter.Get<RQRequisition>())
    {
      RQRequisition rqRequisition2 = rqRequisition1;
      RQBiddingVendor vendor = (RQBiddingVendor) null;
      if (rqRequisition1.Splittable.GetValueOrDefault())
      {
        if (!rqRequisition1.VendorID.HasValue || !rqRequisition1.VendorLocationID.HasValue)
        {
          foreach (PXResult<RQRequisitionLine> pxResult1 in ((PXSelectBase<RQRequisitionLine>) rqBiddingProcess.Lines).Select(new object[1]
          {
            (object) rqRequisition1.ReqNbr
          }))
          {
            RQRequisitionLine line = PXResult<RQRequisitionLine>.op_Implicit(pxResult1);
            Decimal? nullable1 = line.OrderQty;
            Decimal? nullable2 = line.BiddingQty;
            if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            {
              nullable2 = line.OrderQty;
              Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
              nullable2 = line.BiddingQty;
              Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
              Decimal num1 = valueOrDefault1 - valueOrDefault2;
              List<RQBidding> rqBiddingList = new List<RQBidding>();
              foreach (PXResult<RQBidding> pxResult2 in PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.lineNbr, Equal<Required<RQBidding.lineNbr>>, And<RQBidding.quoteUnitCost, Greater<decimal0>>>>, OrderBy<Asc<RQBidding.quoteUnitCost>>>.Config>.Select((PXGraph) rqBiddingProcess, new object[2]
              {
                (object) line.ReqNbr,
                (object) line.LineNbr
              }))
              {
                RQBidding rqBidding1 = PXResult<RQBidding>.op_Implicit(pxResult2);
                nullable2 = rqBidding1.QuoteQty;
                Decimal num2 = 0M;
                Decimal num3;
                if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
                {
                  nullable2 = rqBidding1.QuoteQty;
                  Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
                  nullable2 = rqBidding1.OrderQty;
                  Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
                  num3 = valueOrDefault3 - valueOrDefault4;
                }
                else
                  num3 = 0M;
                Decimal num4 = num3;
                if (num4 > 0M)
                {
                  Decimal num5 = num4 > num1 ? num1 : num4;
                  Decimal num6 = num5;
                  nullable2 = rqBidding1.MinQty;
                  Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
                  if (num6 >= valueOrDefault5 & nullable2.HasValue)
                  {
                    if (vendor == null)
                    {
                      vendor = new RQBiddingVendor()
                      {
                        VendorID = rqBidding1.VendorID,
                        VendorLocationID = rqBidding1.VendorLocationID
                      };
                    }
                    else
                    {
                      int? vendorId = vendor.VendorID;
                      int? nullable3 = rqBidding1.VendorID;
                      int? nullable4;
                      if (vendorId.GetValueOrDefault() == nullable3.GetValueOrDefault() & vendorId.HasValue == nullable3.HasValue)
                      {
                        nullable3 = vendor.VendorLocationID;
                        nullable4 = rqBidding1.VendorLocationID;
                        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                          goto label_21;
                      }
                      RQBiddingVendor rqBiddingVendor1 = vendor;
                      nullable4 = new int?();
                      int? nullable5 = nullable4;
                      rqBiddingVendor1.VendorID = nullable5;
                      RQBiddingVendor rqBiddingVendor2 = vendor;
                      nullable4 = new int?();
                      int? nullable6 = nullable4;
                      rqBiddingVendor2.VendorLocationID = nullable6;
                    }
label_21:
                    RQBidding copy = (RQBidding) ((PXSelectBase) rqBiddingProcess.Bidding).Cache.CreateCopy((object) rqBidding1);
                    RQBidding rqBidding2 = copy;
                    nullable2 = rqBidding2.OrderQty;
                    Decimal num7 = num5;
                    Decimal? nullable7;
                    if (!nullable2.HasValue)
                    {
                      nullable1 = new Decimal?();
                      nullable7 = nullable1;
                    }
                    else
                      nullable7 = new Decimal?(nullable2.GetValueOrDefault() + num7);
                    rqBidding2.OrderQty = nullable7;
                    ((PXSelectBase<RQBidding>) rqBiddingProcess.Bidding).Update(copy);
                    rqBiddingList.Add(rqBidding1);
                    num1 -= num5;
                  }
                }
                if (num1 <= 0M)
                  break;
              }
              if (rqBiddingList.Count == 1)
                rqBiddingProcess.CopyUnitCost(line, rqBiddingList[0]);
              if (num1 > 0M)
                ((PXSelectBase) rqBiddingProcess.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.biddingQty>((object) line, (object) line.BiddingQty, (Exception) new PXSetPropertyException("Bidding Qty cannot be lower than Order Qty", (PXErrorLevel) 2));
            }
          }
        }
        if (vendor != null && !vendor.VendorID.HasValue)
          vendor = (RQBiddingVendor) null;
      }
      else
      {
        Decimal maxValue = Decimal.MaxValue;
        foreach (PXResult<RQBiddingRequisitionLine, RQBiddingVendor, RQBidding> pxResult in PXSelectBase<RQBiddingRequisitionLine, PXSelectJoinGroupBy<RQBiddingRequisitionLine, InnerJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<RQRequisitionLine.reqNbr>>, LeftJoin<RQBidding, On<RQBidding.reqNbr, Equal<RQRequisitionLine.reqNbr>, And<RQBidding.lineNbr, Equal<RQRequisitionLine.lineNbr>, And<RQBidding.vendorID, Equal<RQBiddingVendor.vendorID>, And<RQBidding.vendorLocationID, Equal<RQBiddingVendor.vendorLocationID>, And<RQBidding.minQty, LessEqual<RQRequisitionLine.orderQty>, And<Where<RQBidding.quoteQty, Equal<decimal0>, Or<RQBidding.quoteQty, GreaterEqual<RQRequisitionLine.orderQty>>>>>>>>>>>, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>>, Aggregate<GroupBy<RQBiddingVendor.vendorID, GroupBy<RQBiddingVendor.vendorLocationID, Sum<RQBiddingRequisitionLine.quoteCost, Sum<RQRequisitionLine.lineNbr, Sum<RQBidding.lineNbr>>>>>>>.Config>.Select((PXGraph) rqBiddingProcess, new object[1]
        {
          (object) rqRequisition1.ReqNbr
        }))
        {
          RQBiddingRequisitionLine biddingRequisitionLine = PXResult<RQBiddingRequisitionLine, RQBiddingVendor, RQBidding>.op_Implicit(pxResult);
          RQBiddingVendor rqBiddingVendor = PXResult<RQBiddingRequisitionLine, RQBiddingVendor, RQBidding>.op_Implicit(pxResult);
          RQBidding rqBidding = PXResult<RQBiddingRequisitionLine, RQBiddingVendor, RQBidding>.op_Implicit(pxResult);
          Decimal? quoteCost = biddingRequisitionLine.QuoteCost;
          if (quoteCost.HasValue)
          {
            int? lineNbr1 = biddingRequisitionLine.LineNbr;
            int? lineNbr2 = rqBidding.LineNbr;
            if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            {
              quoteCost = biddingRequisitionLine.QuoteCost;
              Decimal num = maxValue;
              if (!(quoteCost.GetValueOrDefault() > num & quoteCost.HasValue))
              {
                quoteCost = biddingRequisitionLine.QuoteCost;
                maxValue = quoteCost.Value;
                vendor = rqBiddingVendor;
              }
            }
          }
        }
      }
      if (vendor != null)
        rqRequisition2 = rqBiddingProcess.DoChooseVendor(rqRequisition2, vendor);
      yield return (object) rqRequisition2;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Clear Result")]
  public virtual IEnumerable clearResult(PXAdapter adapter)
  {
    RQBiddingProcess rqBiddingProcess = this;
    foreach (RQRequisition rqRequisition1 in adapter.Get<RQRequisition>())
    {
      RQRequisition rqRequisition2 = rqRequisition1;
      foreach (PXResult<RQBidding> pxResult in PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.orderQty, Greater<Required<RQBidding.orderQty>>>>>.Config>.Select((PXGraph) rqBiddingProcess, new object[2]
      {
        (object) rqRequisition1.ReqNbr,
        (object) 0M
      }))
      {
        RQBidding rqBidding = PXResult<RQBidding>.op_Implicit(pxResult);
        RQBidding copy = (RQBidding) ((PXSelectBase) rqBiddingProcess.Bidding).Cache.CreateCopy((object) rqBidding);
        copy.OrderQty = new Decimal?(0M);
        ((PXSelectBase<RQBidding>) rqBiddingProcess.Bidding).Update(copy);
      }
      if (rqRequisition1.VendorLocationID.HasValue)
      {
        RQRequisition copy = (RQRequisition) ((PXSelectBase) rqBiddingProcess.Document).Cache.CreateCopy((object) rqRequisition1);
        copy.VendorID = new int?();
        copy.VendorLocationID = new int?();
        copy.RemitAddressID = new int?();
        copy.RemitContactID = new int?();
        copy.ShipVia = (string) null;
        copy.FOBPoint = (string) null;
        rqRequisition2 = ((PXSelectBase<RQRequisition>) rqBiddingProcess.Document).Update(copy);
      }
      yield return (object) rqRequisition2;
    }
  }

  public RQBiddingProcess()
  {
    ((PXSelectBase) this.Document).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Document).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RQRequisition.reqNbr>(((PXSelectBase) this.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisition.vendorID>(((PXSelectBase) this.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisition.vendorLocationID>(((PXSelectBase) this.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisition.vendorRefNbr>(((PXSelectBase) this.Document).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequisition.splittable>(((PXSelectBase) this.Document).Cache, (object) null, true);
    ((PXSelectBase) this.Lines).Cache.AllowInsert = false;
    ((PXSelectBase) this.Lines).Cache.AllowDelete = false;
  }

  protected virtual void RQRequisition_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null)
      return;
    bool? nullable = row.Splittable;
    bool valueOrDefault = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<RQRequisition.curyID>(sender, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<RQRequisitionLine.biddingQty>(((PXSelectBase) this.Lines).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<RQBidding.orderQty>(((PXSelectBase) this.Bidding).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<RQBidding.selected>(((PXSelectBase) this.Bidding).Cache, (object) null, valueOrDefault);
    PXCache pxCache = sender;
    nullable = ((PXSelectBase<RQBiddingState>) this.State).Current.SingleMode;
    int num = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequisition.reqNbr>(pxCache, (object) null, num != 0);
    PXUIFieldAttribute.SetEnabled<RQRequisition.vendorID>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<RQRequisition.vendorLocationID>(sender, (object) row, false);
    if (row.Status == "B")
    {
      PXUIFieldAttribute.SetEnabled<RQRequisition.vendorRefNbr>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequisition.splittable>(sender, (object) row, true);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<RQRequisition.reqNbr>(sender, (object) row, true);
    }
    ((PXAction) this.Process).SetEnabled(row.Status == "B");
    ((PXAction) this.ChooseVendor).SetEnabled(row.Status == "B");
    ((PXAction) this.UpdateResult).SetEnabled(row.Status == "B");
    ((PXAction) this.ClearResult).SetEnabled(row.Status == "B");
    ((PXSelectBase) this.Document).Cache.AllowInsert = ((PXSelectBase) this.Document).Cache.AllowDelete = false;
    ((PXSelectBase) this.Document).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Bidding).Cache.AllowInsert = ((PXSelectBase) this.Bidding).Cache.AllowUpdate = ((PXSelectBase) this.Bidding).Cache.AllowDelete = ((PXSelectBase) this.Vendors).Cache.AllowInsert = ((PXSelectBase) this.Vendors).Cache.AllowUpdate = ((PXSelectBase) this.Vendors).Cache.AllowDelete = row.Status == "B";
  }

  protected virtual void RQBidding_Selected_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    if (row.Selected.GetValueOrDefault())
    {
      Decimal? nullable = row.OrderQty;
      if (nullable.GetValueOrDefault() == 0M)
      {
        RQRequisitionLine rqRequisitionLine = PXResultset<RQRequisitionLine>.op_Implicit(PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.ReqNbr,
          (object) row.LineNbr
        }));
        nullable = rqRequisitionLine.OrderQty;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = rqRequisitionLine.BiddingQty;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        Decimal num = valueOrDefault1 - valueOrDefault2;
        if (num > 0M)
          sender.SetValueExt<RQBidding.orderQty>((object) row, (object) num);
      }
    }
    bool? selected = row.Selected;
    bool flag = false;
    if (!(selected.GetValueOrDefault() == flag & selected.HasValue))
      return;
    row.OrderQty = new Decimal?(0M);
  }

  protected virtual void RQBidding_OrderQty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    if (row == null)
      return;
    RQRequisitionLine rqRequisitionLine = PXResultset<RQRequisitionLine>.op_Implicit(PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.ReqNbr,
      (object) row.LineNbr
    }));
    if (rqRequisitionLine == null)
      return;
    Decimal valueOrDefault1 = ((Decimal?) e.NewValue).GetValueOrDefault();
    Decimal? nullable1 = row.OrderQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 - valueOrDefault2;
    Decimal? nullable2;
    if (num1 > 0M)
    {
      Decimal? orderQty1 = rqRequisitionLine.OrderQty;
      nullable2 = rqRequisitionLine.BiddingQty;
      nullable1 = orderQty1.HasValue & nullable2.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = num1;
      if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
      {
        sender.RaiseExceptionHandling<RQBidding.orderQty>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Insufficient quantity available. Order quantity was changed to match.", (PXErrorLevel) 2));
        PXFieldVerifyingEventArgs verifyingEventArgs = e;
        orderQty1 = row.OrderQty;
        Decimal? orderQty2 = rqRequisitionLine.OrderQty;
        nullable1 = orderQty1.HasValue & orderQty2.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() + orderQty2.GetValueOrDefault()) : new Decimal?();
        nullable2 = rqRequisitionLine.BiddingQty;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?());
        verifyingEventArgs.NewValue = (object) local;
      }
    }
    nullable2 = (Decimal?) e.NewValue;
    Decimal num3 = 0M;
    if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
      row.Selected = new bool?(true);
    nullable2 = (Decimal?) e.NewValue;
    Decimal num4 = 0M;
    if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
      return;
    nullable2 = row.MinQty;
    Decimal num5 = 0M;
    if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
    {
      nullable2 = row.QuoteQty;
      Decimal num6 = 0M;
      if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
        return;
    }
    nullable2 = row.MinQty;
    nullable1 = (Decimal?) e.NewValue;
    if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
      sender.RaiseExceptionHandling<RQBidding.orderQty>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Order qty less than minimal qty specified by the vendor", (PXErrorLevel) 2));
    nullable1 = row.QuoteQty;
    nullable2 = (Decimal?) e.NewValue;
    if (!(nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
      return;
    sender.RaiseExceptionHandling<RQBidding.orderQty>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Order qty more than quote qty specified by the vendor", (PXErrorLevel) 2));
  }

  protected virtual void RQBidding_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQBidding.quoteUnitCost>(e.Row);
  }

  protected virtual void RQBidding_CuryQuoteUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is RQBidding row))
      return;
    object[] objArray = new object[2]
    {
      (object) (row.ReqNbr ?? ((PXSelectBase<RQBiddingState>) this.State).Current.ReqNbr),
      null
    };
    int? nullable = row.LineNbr;
    objArray[1] = (object) (nullable ?? ((PXSelectBase<RQBiddingState>) this.State).Current.LineNbr);
    RQRequisitionLine inventoryRow = PXResultset<RQRequisitionLine>.op_Implicit(PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Required<RQRequisitionLine.reqNbr>>>>>.Config>.Select((PXGraph) this, objArray));
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.VendorID
    }));
    if (vendor == null || inventoryRow == null)
      return;
    nullable = inventoryRow.InventoryID;
    if (!nullable.HasValue || !row.CuryInfoID.HasValue)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = POItemCostManager.FetchCuryInfo<RQBidding.curyInfoID>((PXGraph) this, (object) row);
    int? vendorId = row.VendorID;
    int? vendorLocationId = row.VendorLocationID;
    DateTime? docDate = new DateTime?();
    string curyId = vendor.CuryID;
    string baseCuryId = currencyInfo.BaseCuryID;
    int? inventoryId = inventoryRow.InventoryID;
    int? subItemId = inventoryRow.SubItemID;
    nullable = new int?();
    int? siteID = nullable;
    string uom = inventoryRow.UOM;
    POItemCostManager.ItemCost itemCost = POItemCostManager.Fetch((PXGraph) this, vendorId, vendorLocationId, docDate, curyId, baseCuryId, inventoryId, subItemId, siteID, uom);
    e.NewValue = (object) itemCost.Convert<RQRequisitionLine.inventoryID>(sender.Graph, (object) inventoryRow, currencyInfo, inventoryRow.UOM);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQBidding_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    if (row == null)
      return;
    if (((PXSelectBase<RQRequisition>) this.Document).Current == null || !((PXSelectBase<RQRequisition>) this.Document).Current.Splittable.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled<RQBidding.orderQty>(sender, (object) null, false);
      row.OrderQty = new Decimal?(0M);
    }
    else
      PXUIFieldAttribute.SetEnabled<RQBidding.orderQty>(sender, (object) null, true);
  }

  protected virtual void RQBidding_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    if (row == null)
      return;
    row.LineNbr = ((PXSelectBase<RQBiddingState>) this.State).Current.LineNbr;
    row.ReqNbr = ((PXSelectBase<RQBiddingState>) this.State).Current.ReqNbr;
    ((CancelEventArgs) e).Cancel = !this.ValidateBiddingDuplicates(sender, row, (RQBidding) null);
    this.UpdateVendor(sender, row);
    RQBiddingVendor rqBiddingVendor = PXResultset<RQBiddingVendor>.op_Implicit(((PXSelectBase<RQBiddingVendor>) this.ChoosenVendor).Select(new object[3]
    {
      (object) row.ReqNbr,
      (object) row.VendorID,
      (object) row.VendorLocationID
    }));
    if (rqBiddingVendor == null)
      return;
    row.CuryInfoID = rqBiddingVendor.CuryInfoID;
  }

  protected virtual void RQBidding_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    if (row == null)
      return;
    RQBidding copy = PXCache<RQBidding>.CreateCopy(row);
    sender.SetDefaultExt<RQBidding.curyQuoteUnitCost>((object) copy);
    ((PXSelectBase<RQBidding>) this.Bidding).Update(copy);
  }

  protected virtual void RQBidding_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQBidding row = (RQBidding) e.Row;
    RQBidding newRow = (RQBidding) e.NewRow;
    int? nullable1;
    int? nullable2;
    if (row != null && newRow != null && row != newRow)
    {
      nullable1 = row.VendorID;
      int? vendorId = newRow.VendorID;
      if (nullable1.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable1.HasValue == vendorId.HasValue)
      {
        nullable2 = row.VendorLocationID;
        nullable1 = newRow.VendorLocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          goto label_4;
      }
      ((CancelEventArgs) e).Cancel = !this.ValidateBiddingDuplicates(sender, newRow, row);
    }
label_4:
    if (((CancelEventArgs) e).Cancel)
      return;
    nullable1 = newRow.VendorID;
    if (nullable1.HasValue)
    {
      nullable1 = newRow.VendorLocationID;
      if (!nullable1.HasValue && !PXAccess.FeatureInstalled<FeaturesSet.accountLocations>())
        sender.SetDefaultExt<RQBidding.vendorLocationID>((object) newRow);
    }
    this.UpdateVendor(sender, newRow);
    nullable1 = row.VendorID;
    nullable2 = newRow.VendorID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = row.VendorLocationID;
      nullable1 = newRow.VendorLocationID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        return;
    }
    RQBiddingVendor rqBiddingVendor = PXResultset<RQBiddingVendor>.op_Implicit(((PXSelectBase<RQBiddingVendor>) this.ChoosenVendor).Select(new object[3]
    {
      (object) row.ReqNbr,
      (object) newRow.VendorID,
      (object) newRow.VendorLocationID
    }));
    if (rqBiddingVendor == null)
      return;
    newRow.CuryInfoID = rqBiddingVendor.CuryInfoID;
    sender.SetDefaultExt<RQBidding.curyQuoteUnitCost>((object) newRow);
  }

  protected virtual void RQBiddingVendor_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null)
      return;
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(sender, e.Row);
    ((CancelEventArgs) e).Cancel = !this.ValidateBiddingVendorDuplicates(sender, row, (RQBiddingVendor) null);
  }

  protected virtual void RQBiddingVendor_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null)
      return;
    PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
  }

  protected virtual void RQBiddingVendor_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    RQBiddingVendor newRow = (RQBiddingVendor) e.NewRow;
    if (row == null || newRow == null || row == newRow)
      return;
    int? nullable = row.VendorID;
    int? vendorId = newRow.VendorID;
    if (nullable.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable.HasValue == vendorId.HasValue)
    {
      int? vendorLocationId = row.VendorLocationID;
      nullable = newRow.VendorLocationID;
      if (vendorLocationId.GetValueOrDefault() == nullable.GetValueOrDefault() & vendorLocationId.HasValue == nullable.HasValue)
        return;
    }
    ((CancelEventArgs) e).Cancel = !this.ValidateBiddingVendorDuplicates(sender, newRow, row);
  }

  protected virtual void RQBiddingVendor_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null)
      return;
    RQBidding rqBidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.vendorID>(sender, (object) row, rqBidding == null);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.vendorLocationID>(sender, (object) row, rqBidding == null);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyID>(sender, (object) row, rqBidding == null);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyInfoID>(sender, (object) row, rqBidding == null);
  }

  protected virtual void RQRequisition_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequisition.vendorLocationID>(e.Row);
  }

  protected virtual void RQBiddingVendor_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || row == null)
      return;
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current = (PX.Objects.AP.Vendor) ((PXSelectBase) this.Vendor).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    PX.Objects.CM.CurrencyInfoAttribute.SetDefaults<RQBiddingVendor.curyInfoID>(sender, e.Row);
    sender.SetDefaultExt<RQBiddingVendor.curyID>(e.Row);
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

  protected virtual void RQBiddingVendor_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(sender, e.Row);
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryID))
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current == null)
      return;
    e.NewValue = (object) (((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Document).Cache.Current == null)
      return;
    e.NewValue = (object) ((RQRequisition) ((PXSelectBase) this.Document).Cache.Current).OrderDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool? nullable = row.IsReadOnly;
    bool flag1 = !nullable.GetValueOrDefault() && ((PXSelectBase) this.Bidding).Cache.AllowUpdate;
    bool flag2 = row.AllowUpdate(((PXSelectBase) this.Bidding).Cache);
    if (((PXSelectBase<RQRequisition>) this.Document).Current != null)
    {
      PXResult<RQBiddingVendor, PX.Objects.AP.Vendor> pxResult = (PXResult<RQBiddingVendor, PX.Objects.AP.Vendor>) PXResultset<RQBiddingVendor>.op_Implicit(PXSelectBase<RQBiddingVendor, PXSelectJoin<RQBiddingVendor, InnerJoin<PX.Objects.AP.Vendor, On<RQBiddingVendor.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<RQBiddingVendor.curyInfoID, Equal<Required<RQBiddingVendor.curyInfoID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.CuryInfoID
      }));
      PX.Objects.AP.Vendor vendor = pxResult != null ? PXResult<RQBiddingVendor, PX.Objects.AP.Vendor>.op_Implicit(pxResult) : (PX.Objects.AP.Vendor) null;
      if (flag2)
      {
        int num;
        if (vendor != null)
        {
          nullable = vendor.AllowOverrideCury;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 0;
        flag1 = num != 0;
      }
      if (flag2)
      {
        int num;
        if (vendor != null)
        {
          nullable = vendor.AllowOverrideRate;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 0;
        flag2 = num != 0;
      }
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag2);
  }

  private bool ValidateBiddingVendorDuplicates(
    PXCache sender,
    RQBiddingVendor row,
    RQBiddingVendor oldRow)
  {
    if (row.VendorLocationID.HasValue)
    {
      foreach (PXResult<RQBiddingVendor> pxResult in ((PXSelectBase<RQBiddingVendor>) this.Vendors).Select(new object[1]
      {
        (object) (row.ReqNbr ?? ((PXSelectBase<RQBiddingState>) this.State).Current.ReqNbr)
      }))
      {
        RQBiddingVendor rqBiddingVendor = PXResult<RQBiddingVendor>.op_Implicit(pxResult);
        int? nullable1 = rqBiddingVendor.VendorID;
        int? nullable2 = row.VendorID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = rqBiddingVendor.VendorLocationID;
          nullable1 = row.VendorLocationID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = row.LineID;
            nullable2 = rqBiddingVendor.LineID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            {
              if (oldRow != null)
              {
                nullable2 = oldRow.VendorID;
                nullable1 = row.VendorID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  goto label_9;
              }
              sender.RaiseExceptionHandling<RQBiddingVendor.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_9:
              if (oldRow != null)
              {
                nullable1 = oldRow.VendorLocationID;
                nullable2 = row.VendorLocationID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                  goto label_12;
              }
              sender.RaiseExceptionHandling<RQBiddingVendor.vendorLocationID>((object) row, (object) row.VendorLocationID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_12:
              return false;
            }
          }
        }
      }
    }
    PXUIFieldAttribute.SetError<RQBiddingVendor.vendorID>(sender, (object) row, (string) null);
    PXUIFieldAttribute.SetError<RQBiddingVendor.vendorLocationID>(sender, (object) row, (string) null);
    return true;
  }

  private bool ValidateBiddingDuplicates(PXCache sender, RQBidding row, RQBidding oldRow)
  {
    if (row.VendorLocationID.HasValue)
    {
      PXSelectJoin<RQBidding, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<RQBidding.reqNbr>, And<RQBiddingVendor.vendorID, Equal<RQBidding.vendorID>, And<RQBiddingVendor.vendorLocationID, Equal<RQBidding.vendorLocationID>>>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQBidding.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<RQBidding.vendorLocationID>>>>>, Where<RQBidding.reqNbr, Equal<Argument<string>>, And<RQBidding.lineNbr, Equal<Argument<int?>>>>> bidding = this.Bidding;
      object[] objArray = new object[2]
      {
        (object) (row.ReqNbr ?? ((PXSelectBase<RQBiddingState>) this.State).Current.ReqNbr),
        null
      };
      int? nullable1 = row.LineNbr;
      objArray[1] = (object) (nullable1 ?? ((PXSelectBase<RQBiddingState>) this.State).Current.LineNbr);
      foreach (PXResult<RQBidding> pxResult in ((PXSelectBase<RQBidding>) bidding).Select(objArray))
      {
        RQBidding rqBidding = PXResult<RQBidding>.op_Implicit(pxResult);
        nullable1 = rqBidding.VendorID;
        int? nullable2 = row.VendorID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = rqBidding.VendorLocationID;
          nullable1 = row.VendorLocationID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = row.LineID;
            nullable2 = rqBidding.LineID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            {
              if (oldRow != null)
              {
                nullable2 = oldRow.VendorID;
                nullable1 = row.VendorID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  goto label_9;
              }
              sender.RaiseExceptionHandling<RQBidding.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_9:
              if (oldRow != null)
              {
                nullable1 = oldRow.VendorLocationID;
                nullable2 = row.VendorLocationID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                  goto label_12;
              }
              sender.RaiseExceptionHandling<RQBidding.vendorLocationID>((object) row, (object) row.VendorLocationID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_12:
              return false;
            }
          }
        }
      }
    }
    PXUIFieldAttribute.SetError<RQBidding.vendorID>(sender, (object) row, (string) null);
    PXUIFieldAttribute.SetError<RQBidding.vendorLocationID>(sender, (object) row, (string) null);
    return true;
  }

  private void UpdateVendor(PXCache sender, RQBidding row)
  {
    if (!row.VendorID.HasValue || !row.VendorLocationID.HasValue)
      return;
    RQBiddingVendor rqBiddingVendor = PXResultset<RQBiddingVendor>.op_Implicit(PXSelectBase<RQBiddingVendor, PXSelect<RQBiddingVendor, Where<RQBiddingVendor.reqNbr, Equal<Required<RQBiddingVendor.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Required<RQBiddingVendor.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Required<RQBiddingVendor.vendorLocationID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) row.ReqNbr,
      (object) row.VendorID,
      (object) row.VendorLocationID
    }));
    if (rqBiddingVendor == null)
    {
      RQBiddingVendor copy = PXCache<RQBiddingVendor>.CreateCopy(((PXSelectBase<RQBiddingVendor>) this.Vendors).Insert(new RQBiddingVendor()
      {
        ReqNbr = row.ReqNbr
      }));
      copy.VendorID = row.VendorID;
      copy.VendorLocationID = row.VendorLocationID;
      copy.EntryDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<RQBiddingVendor>) this.Vendors).Update(copy);
    }
    else
    {
      if (rqBiddingVendor.EntryDate.HasValue)
        return;
      rqBiddingVendor.EntryDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<RQBiddingVendor>) this.Vendors).Update(rqBiddingVendor);
    }
  }
}
