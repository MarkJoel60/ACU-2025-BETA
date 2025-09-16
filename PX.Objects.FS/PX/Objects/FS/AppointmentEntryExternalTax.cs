// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentEntryExternalTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class AppointmentEntryExternalTax : ExternalTax<AppointmentEntry, FSAppointment>
{
  public PXAction<FSAppointment> recalcExternalTax;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public override bool IsExternalTax(string taxZoneID)
  {
    return (((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current.PostTo == "PM")) && ExternalTaxBase<AppointmentEntry>.IsExternalTax((PXGraph) this.Base, taxZoneID);
  }

  public bool SkipExternalTaxCalcOnSave
  {
    get => this.skipExternalTaxCalcOnSave;
    set => this.skipExternalTaxCalcOnSave = value;
  }

  public override FSAppointment CalculateExternalTax(FSAppointment fsAppointment)
  {
    return this.IsExternalTax(fsAppointment.TaxZoneID) && !this.skipExternalTaxCalcOnSave ? this.CalculateExternalTax(fsAppointment, ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRelated).Current, false) : fsAppointment;
  }

  public virtual FSAppointment CalculateExternalTax(
    FSAppointment fsAppointment,
    FSServiceOrder order,
    bool forceRecalculate)
  {
    IAddressLocation toAddress = this.GetToAddress(order);
    ITaxProvider itaxProvider = ExternalTaxBase<AppointmentEntry>.TaxProviderFactory((PXGraph) this.Base, fsAppointment.TaxZoneID);
    GetTaxRequest x = (GetTaxRequest) null;
    GetTaxRequest y1 = (GetTaxRequest) null;
    GetTaxRequest y2 = (GetTaxRequest) null;
    GetTaxRequest getTaxRequest = (GetTaxRequest) null;
    bool flag1 = false;
    PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSAppointment.srvOrdType>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) fsAppointment.SrvOrdType
    }));
    if (toAddress != null && !this.IsNonTaxable((IAddressBase) toAddress) && !fsAppointment.IsTaxValid.GetValueOrDefault() | forceRecalculate)
    {
      x = this.BuildGetTaxRequest(fsAppointment, order);
      if (x.CartItems.Count > 0)
        flag1 = false;
      else
        x = (GetTaxRequest) null;
    }
    if (flag1)
    {
      fsAppointment.CuryTaxTotal = new Decimal?(0M);
      fsAppointment.IsTaxValid = new bool?(true);
      ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Update(fsAppointment);
      foreach (PXResult<FSAppointmentTaxTran> pxResult in ((PXSelectBase<FSAppointmentTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
        ((PXSelectBase<FSAppointmentTaxTran>) this.Base.Taxes).Delete(PXResult<FSAppointmentTaxTran>.op_Implicit(pxResult));
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXGraph) this.Base).Persist(typeof (FSAppointmentTaxTran), (PXDBOperation) 3);
        ((PXGraph) this.Base).Persist(typeof (FSAppointment), (PXDBOperation) 1);
        PXTimeStampScope.PutPersisted(((PXSelectBase) this.Base.AppointmentRecords).Cache, (object) fsAppointment, new object[1]
        {
          (object) PXDatabase.SelectTimeStamp()
        });
        transactionScope.Complete();
      }
      return fsAppointment;
    }
    GetTaxResult result1 = (GetTaxResult) null;
    GetTaxResult resultOpen = (GetTaxResult) null;
    GetTaxResult resultUnbilled = (GetTaxResult) null;
    GetTaxResult resultFreight = (GetTaxResult) null;
    bool flag2 = false;
    if (x != null)
    {
      result1 = itaxProvider.GetTax(x);
      if (!((ResultBase) result1).IsSuccess)
        flag2 = true;
    }
    if (y1 != null)
    {
      if (x != null && this.IsSame(x, y1))
      {
        resultOpen = result1;
      }
      else
      {
        resultOpen = itaxProvider.GetTax(y1);
        if (!((ResultBase) resultOpen).IsSuccess)
          flag2 = true;
      }
    }
    if (y2 != null)
    {
      if (x != null && this.IsSame(x, y2))
      {
        resultUnbilled = result1;
      }
      else
      {
        resultUnbilled = itaxProvider.GetTax(y2);
        if (!((ResultBase) resultUnbilled).IsSuccess)
          flag2 = true;
      }
    }
    if (getTaxRequest != null)
    {
      resultFreight = itaxProvider.GetTax(getTaxRequest);
      if (!((ResultBase) resultFreight).IsSuccess)
        flag2 = true;
    }
    if (!flag2)
    {
      try
      {
        this.ApplyTax(fsAppointment, result1, resultOpen, resultUnbilled, resultFreight);
      }
      catch (PXOuterException ex)
      {
        string str = "The tax amount calculated by the external tax provider cannot be applied to the document.";
        foreach (string innerMessage in ex.InnerMessages)
          str = str + Environment.NewLine + innerMessage;
        throw new PXException((Exception) ex, str, Array.Empty<object>());
      }
      catch (Exception ex)
      {
        string str = $"The tax amount calculated by the external tax provider cannot be applied to the document.{Environment.NewLine}{ex.Message}";
        throw new PXException(ex, str, Array.Empty<object>());
      }
      return fsAppointment;
    }
    ResultBase result2 = (ResultBase) (result1 ?? resultOpen ?? resultUnbilled ?? resultFreight);
    if (result2 != null)
      this.LogMessages(result2);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  [PXOverride]
  public virtual void RecalculateExternalTaxes()
  {
    if (((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current == null || !this.IsExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave || ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.IsTaxValid.GetValueOrDefault())
      return;
    if (this.Base.RecalculateExternalTaxesSync)
    {
      AppointmentExternalTaxCalc.Process(new FSAppointment()
      {
        SrvOrdType = ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.SrvOrdType,
        RefNbr = ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.RefNbr
      });
    }
    else
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRecalculateExternalTaxes\u003Eb__7_0)));
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RecalcExternalTax(PXAdapter adapter)
  {
    AppointmentEntryExternalTax entryExternalTax = this;
    if (((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current != null && entryExternalTax.IsExternalTax(((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current.TaxZoneID))
    {
      FSAppointment current = ((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current;
      entryExternalTax.CalculateExternalTax(((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current, ((PXSelectBase<FSServiceOrder>) entryExternalTax.Base.ServiceOrderRelated).Current, true);
      ((PXGraph) entryExternalTax.Base).Clear((PXClearOption) 3);
      ((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Search<FSAppointment.refNbr>((object) current.RefNbr, new object[1]
      {
        (object) current.SrvOrdType
      }));
      yield return (object) ((PXSelectBase<FSAppointment>) entryExternalTax.Base.AppointmentRecords).Current;
    }
    else
    {
      foreach (object obj in adapter.Get())
        yield return obj;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = this.IsExternalTax(e.Row.TaxZoneID);
    bool allowEdit = this.Base.CanUpdateAppointment(e.Row, ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current) && (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointment>>) e).Cache.Graph.IsContractBasedAPI || !flag1);
    bool flag2 = ((PXGraph) this.Base).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300000");
    if (flag1 && !e.Row.IsTaxValid.GetValueOrDefault() && !flag2)
      PXUIFieldAttribute.SetWarning<FSAppointment.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointment>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
    ((PXSelectBase) this.Base.Taxes).Cache.SetAllEditPermissions(allowEdit);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointment> e)
  {
    if (e.Row == null || !this.IsExternalTax(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointment>>) e).Cache.ObjectsEqual<FSAppointment.scheduledDateTimeBegin, FSAppointment.taxZoneID, FSAppointment.billCustomerID, FSAppointment.branchID, FSAppointment.curyDiscTot, FSAppointment.entityUsageType, FSAppointment.externalTaxExemptionNumber, FSAppointment.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceOrder> e)
  {
    if (!(this.IsExternalTax(e.Row.TaxZoneID) & !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSServiceOrder>>) e).Cache.ObjectsEqual<FSServiceOrder.branchLocationID, FSServiceOrder.billCustomerID, FSServiceOrder.serviceOrderAddressID, FSServiceOrder.billLocationID>((object) e.Row, (object) e.OldRow)))
      return;
    ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.IsTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointmentDet> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointmentDet> e)
  {
    if (((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current == null || !this.IsExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache.ObjectsEqual<FSAppointmentDet.acctID, FSAppointmentDet.inventoryID, FSAppointmentDet.tranDesc, FSAppointmentDet.curyBillableTranAmt, FSAppointmentDet.tranDate, FSAppointmentDet.taxCategoryID, FSAppointmentDet.siteID>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointmentDet> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAddress> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAddress>>) e).Cache.ObjectsEqual<FSAddress.postalCode, FSAddress.countryID, FSAddress.state>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAddress, FSAddress.overrideAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current);
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(
    FSAppointment fsAppointment,
    FSServiceOrder order)
  {
    if (fsAppointment == null)
      throw new PXArgumentException(nameof (fsAppointment));
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) ((PXSelectBase) this.Base.TaxCustomer).View.SelectSingleBound(new object[1]
    {
      (object) fsAppointment
    }, Array.Empty<object>());
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXSelectBase) this.Base.TaxLocation).View.SelectSingleBound(new object[1]
    {
      (object) fsAppointment
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.TaxZone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    IAddressLocation fromAddress = this.GetFromAddress(order);
    IAddressLocation toAddress = this.GetToAddress(order);
    if (fromAddress == null)
      throw new PXException("The system cannot obtain the From address from the document.");
    if (toAddress == null)
      throw new PXException("The system cannot obtain the To address from the document.");
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(fsAppointment.TaxZoneID, fsAppointment.BranchID);
    taxRequest.CurrencyCode = fsAppointment.CuryID;
    taxRequest.CustomerCode = customer?.AcctCD;
    taxRequest.BAccountClassID = customer?.ClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = $"SO.{fsAppointment.SrvOrdType}.{fsAppointment.RefNbr}";
    taxRequest.DocDate = fsAppointment.ScheduledDateTimeBegin.GetValueOrDefault();
    taxRequest.LocationCode = this.GetExternalTaxProviderLocationCode(fsAppointment);
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(fsAppointment.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(fsAppointment.TaxZoneID) : fsAppointment.TaxCalcMode;
    Sign plus = Sign.Plus;
    taxRequest.CustomerUsageType = fsAppointment.EntityUsageType;
    if (!string.IsNullOrEmpty(fsAppointment.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = fsAppointment.ExternalTaxExemptionNumber;
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) ((PXSelectBase) this.Base.ServiceOrderTypeSelected).View.SelectSingleBound(new object[1]
    {
      (object) fsAppointment
    }, Array.Empty<object>());
    taxRequest.DocType = (TaxDocumentType) 1;
    PXSelectJoin<FSAppointmentDet, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<FSAppointmentDet.acctID>>>>, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentDet.srvOrdType, Asc<FSAppointmentDet.refNbr, Asc<FSAppointmentDet.lineNbr>>>>> pxSelectJoin = new PXSelectJoin<FSAppointmentDet, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<FSAppointmentDet.acctID>>>>, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentDet.srvOrdType, Asc<FSAppointmentDet.refNbr, Asc<FSAppointmentDet.lineNbr>>>>>((PXGraph) this.Base);
    taxRequest.Discount = Sign.op_Multiply(plus, fsAppointment.CuryDiscTot.GetValueOrDefault());
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[1]
    {
      (object) fsAppointment
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<FSAppointmentDet, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      FSAppointmentDet line = PXResult<FSAppointmentDet, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<FSAppointmentDet, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<FSAppointmentDet, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = line.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Sign sign1 = plus;
      Decimal? nullable = line.CuryUnitPrice;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      Decimal num1 = Sign.op_Multiply(sign1, valueOrDefault1);
      taxCartItem2.UnitPrice = num1;
      TaxCartItem taxCartItem3 = taxCartItem1;
      nullable = line.BillableQty;
      Decimal num2;
      if (!(nullable.GetValueOrDefault() > 0M))
      {
        num2 = 0M;
      }
      else
      {
        Sign sign2 = plus;
        nullable = line.CuryBillableTranAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num2 = Sign.op_Multiply(sign2, valueOrDefault2);
      }
      taxCartItem3.Amount = num2;
      taxCartItem1.Description = line.TranDesc;
      taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(order, line));
      taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(order, line));
      taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      nullable = line.BillableQty;
      Decimal num3 = Math.Abs(nullable.GetValueOrDefault());
      taxCartItem4.Quantity = num3;
      taxCartItem1.UOM = line.UOM;
      taxCartItem1.Discounted = new bool?(taxRequest.Discount != 0M);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = line.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      taxRequest.CartItems.Add(taxCartItem1);
    }
    return taxRequest;
  }

  protected virtual void ApplyTax(
    FSAppointment order,
    GetTaxResult result,
    GetTaxResult resultOpen,
    GetTaxResult resultUnbilled,
    GetTaxResult resultFreight)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.TaxZone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.AP.Vendor taxAgency = this.GetTaxAgency(this.Base, taxZone);
    Sign plus = Sign.Plus;
    if (result != null)
    {
      PXView view = ((PXSelectBase) this.Base.Taxes).View;
      object[] objArray1 = new object[1]{ (object) order };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
        ((PXSelectBase<FSAppointmentTaxTran>) this.Base.Taxes).Delete(PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
      ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
      Decimal num1 = 0M;
      if (resultFreight != null)
        num1 = Sign.op_Multiply(plus, resultFreight.TotalTaxAmount);
      List<TaxDetail> taxDetailList = new List<TaxDetail>();
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        string str = result.TaxSummary[index].TaxName;
        if (string.IsNullOrEmpty(str))
          str = result.TaxSummary[index].JurisCode;
        if (string.IsNullOrEmpty(str))
          PXTrace.WriteInformation("Taxes returned by external tax provider has no tax code and tax zone specified. Please check settings configured in external tax provider.");
        else
          taxDetailList.Add(result.TaxSummary[index]);
      }
      if (resultFreight != null)
      {
        foreach (TaxDetail taxDetail in (IEnumerable<TaxDetail>) ((IEnumerable<TaxDetail>) resultFreight.TaxSummary).OrderByDescending<TaxDetail, Decimal>((Func<TaxDetail, Decimal>) (e => e.TaxAmount)))
        {
          TaxDetail tax = taxDetail;
          if (tax.TaxAmount != 0M || taxDetailList.Find((Predicate<TaxDetail>) (e => e.TaxName == tax.TaxName)) == null)
            taxDetailList.Add(tax);
        }
      }
      Decimal num2 = 0M;
      foreach (TaxDetail taxDetail in taxDetailList)
      {
        taxDetail.TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, taxDetail);
        if (tax != null)
        {
          FSAppointmentTaxTran instance = (FSAppointmentTaxTran) ((PXSelectBase) this.Base.Taxes).Cache.CreateInstance();
          instance.TaxID = tax?.TaxID;
          instance.CuryTaxAmt = new Decimal?(Math.Abs(taxDetail.TaxAmount));
          instance.CuryTaxableAmt = new Decimal?(Math.Abs(taxDetail.TaxableAmount));
          instance.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail.Rate) * 100M);
          instance.JurisType = taxDetail.JurisType;
          instance.JurisName = taxDetail.JurisName;
          instance.TaxZoneID = taxZone.TaxZoneID;
          instance.IsTaxInclusive = new bool?(taxDetail.TaxCalculationLevel == 1);
          ((PXSelectBase<FSAppointmentTaxTran>) this.Base.Taxes).Insert(instance);
          num2 += instance.IsTaxInclusive.GetValueOrDefault() ? instance.CuryTaxAmt.GetValueOrDefault() : 0M;
        }
      }
      ((PXSelectBase<FSAppointment>) this.Base.AppointmentSelected).SetValueExt<FSAppointment.curyTaxTotal>(order, (object) (Sign.op_Multiply(plus, result.TotalTaxAmount) + num1));
      Decimal? nullable = new Decimal?(AppointmentEntry.GetCuryDocTotal(order.CuryBillableLineTotal, order.CuryLogBillableTranAmountTotal, order.CuryDiscTot, order.CuryTaxTotal, new Decimal?(num2)));
      ((PXSelectBase<FSAppointment>) this.Base.AppointmentSelected).SetValueExt<FSAppointment.curyDocTotal>(order, (object) nullable.GetValueOrDefault());
    }
    order = (FSAppointment) ((PXSelectBase) this.Base.AppointmentSelected).Cache.CreateCopy((object) order);
    order.IsTaxValid = new bool?(true);
    ((PXSelectBase) this.Base.AppointmentSelected).Cache.Update((object) order);
    if (((PXGraph) this.Base).TimeStamp == null)
      ((PXGraph) this.Base).SelectTimeStamp();
    this.SkipTaxCalcAndSave();
  }

  protected virtual bool IsSame(GetTaxRequest x, GetTaxRequest y)
  {
    if (x.CartItems.Count != y.CartItems.Count)
      return false;
    for (int index = 0; index < x.CartItems.Count; ++index)
    {
      if (x.CartItems[index].Amount != y.CartItems[index].Amount)
        return false;
    }
    return true;
  }

  protected override string GetExternalTaxProviderLocationCode(FSAppointment fsAppointment)
  {
    return this.GetExternalTaxProviderLocationCode<FSAppointmentDet, KeysRelation<CompositeKey<Field<FSAppointmentDet.srvOrdType>.IsRelatedTo<FSAppointment.srvOrdType>, Field<FSAppointmentDet.refNbr>.IsRelatedTo<FSAppointment.refNbr>>.WithTablesOf<FSAppointment, FSAppointmentDet>, FSAppointment, FSAppointmentDet>.SameAsCurrent, FSAppointmentDet.siteID>(fsAppointment);
  }

  protected virtual IAddressLocation GetFromAddress(FSServiceOrder order)
  {
    return (IAddressLocation) GraphHelper.RowCast<FSAddress>((IEnumerable) PXSelectBase<FSAddress, PXSelectJoin<FSAddress, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationAddressID, Equal<FSAddress.addressID>>>, Where<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.BranchLocationID
    })).FirstOrDefault<FSAddress>();
  }

  protected virtual IAddressLocation GetFromAddress(FSServiceOrder order, FSAppointmentDet line)
  {
    IAddressLocation fromAddress = (IAddressLocation) null;
    if (line.SiteID.HasValue)
      fromAddress = (IAddressLocation) GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<INSite, On<PX.Objects.CR.Address.addressID, Equal<INSite.addressID>>>, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) line.SiteID
      })).FirstOrDefault<PX.Objects.CR.Address>();
    if (fromAddress == null)
      fromAddress = this.GetFromAddress(order);
    return fromAddress;
  }

  protected virtual IAddressLocation GetToAddress(FSServiceOrder order)
  {
    return FSAddress.PK.Find((PXGraph) this.Base, order.ServiceOrderAddressID).With<FSAddress, IAddressLocation>(new Func<FSAddress, IAddressLocation>(this.ValidAddressFrom<FSServiceOrder.serviceOrderAddressID>));
  }

  protected virtual IAddressLocation GetToAddress(FSServiceOrder order, FSAppointmentDet line)
  {
    return this.GetToAddress(order);
  }

  private IAddressLocation ValidAddressFrom<TFieldSource>(IAddressLocation address) where TFieldSource : IBqlField
  {
    return !ExternalTaxBase<AppointmentEntry>.IsEmptyAddress(address) ? address : throw new PXException(this.PickAddressError<TFieldSource>((IAddressBase) address));
  }

  private string PickAddressError<TFieldSource>(IAddressBase address) where TFieldSource : IBqlField
  {
    if (typeof (TFieldSource) == typeof (FSServiceOrder.serviceOrderAddressID))
      return ((PXResult) ((IQueryable<PXResult<FSServiceOrder>>) PXSelectBase<FSServiceOrder, PXSelectReadonly<FSServiceOrder, Where<FSServiceOrder.serviceOrderAddressID, Equal<Required<FSAddress.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((FSAddress) address).AddressID
      })).First<PXResult<FSServiceOrder>>()).GetItem<FSAppointment>().With<FSAppointment, string>((Func<FSAppointment, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<FSServiceOrder>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<FSAppointment>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.AP.Vendor.defLocationID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.AP.Vendor>>) PXSelectBase<PX.Objects.AP.Vendor, PXSelectReadonly<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.defLocationID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<PX.Objects.AP.Vendor>>()).GetItem<PX.Objects.AP.Vendor>().With<PX.Objects.AP.Vendor, string>((Func<PX.Objects.AP.Vendor, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.AP.Vendor>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.AP.Vendor>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (INSite.addressID))
      return ((PXResult) ((IQueryable<PXResult<INSite>>) PXSelectBase<INSite, PXSelectReadonly<INSite, Where<INSite.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<INSite>>()).GetItem<INSite>().With<INSite, string>((Func<INSite, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<INSite>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<INSite>(e, ", ")
      })));
    if (!(typeof (TFieldSource) == typeof (BAccountR.defAddressID)))
      throw new ArgumentOutOfRangeException("Unknown address source used");
    return ((PXResult) ((IQueryable<PXResult<BAccountR>>) PXSelectBase<BAccountR, PXSelectReadonly<BAccountR, Where<BAccountR.defAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((PX.Objects.CR.Address) address).AddressID
    })).First<PXResult<BAccountR>>()).GetItem<BAccountR>().With<BAccountR, string>((Func<BAccountR, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName<BAccountR>(),
      (object) new EntityHelper((PXGraph) this.Base).GetRowID<BAccountR>(e, ", ")
    })));
  }

  protected virtual bool IsCommonCarrier(string carrierID)
  {
    if (string.IsNullOrEmpty(carrierID))
      return false;
    Carrier carrier = PXResultset<Carrier>.op_Implicit(PXSelectBase<Carrier, PXSelect<Carrier, Where<Carrier.carrierID, Equal<Required<Carrier.carrierID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) carrierID
    }));
    return carrier != null && carrier.IsCommonCarrier.GetValueOrDefault();
  }

  private void InvalidateExternalTax(FSAppointment order, bool keepFreight = false)
  {
    if (order == null || !this.IsExternalTax(order.TaxZoneID))
      return;
    order.IsTaxValid = new bool?(false);
  }
}
