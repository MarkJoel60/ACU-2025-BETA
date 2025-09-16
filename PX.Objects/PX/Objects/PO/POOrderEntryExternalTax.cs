// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Objects.PO;

public class POOrderEntryExternalTax : ExternalTax<POOrderEntry, POOrder>
{
  private int _nesting;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public virtual bool CalculateTaxesUsingExternalProvider(string taxZoneID)
  {
    bool flag = ((PXSelectBase<POOrder>) this.Base.Document).Current != null && ((PXSelectBase<POOrder>) this.Base.Document).Current.ExternalTaxesImportInProgress.GetValueOrDefault();
    return ExternalTaxBase<POOrderEntry>.IsExternalTax((PXGraph) this.Base, taxZoneID) && !flag;
  }

  public override POOrder CalculateExternalTax(POOrder order)
  {
    if (order != null && this.CalculateTaxesUsingExternalProvider(order.TaxZoneID))
    {
      if (this.IsNonTaxable((IAddressBase) this.GetToAddress(order)))
      {
        this.ApplyTax(order, GetTaxResult.Empty, GetTaxResult.Empty);
        return order;
      }
      ITaxProvider itaxProvider = ExternalTaxBase<POOrderEntry>.TaxProviderFactory((PXGraph) this.Base, order.TaxZoneID);
      GetTaxRequest getTaxRequest1 = (GetTaxRequest) null;
      GetTaxRequest getTaxRequest2 = (GetTaxRequest) null;
      bool flag1 = true;
      bool? nullable = order.IsTaxValid;
      if (!nullable.GetValueOrDefault())
      {
        getTaxRequest1 = this.BuildGetTaxRequest(order);
        if (getTaxRequest1.CartItems.Count > 0)
          flag1 = false;
        else
          getTaxRequest1 = (GetTaxRequest) null;
      }
      nullable = order.IsUnbilledTaxValid;
      if (!nullable.GetValueOrDefault())
      {
        getTaxRequest2 = this.BuildGetTaxRequestUnbilled(order);
        if (getTaxRequest2.CartItems.Count > 0)
          flag1 = false;
        else
          getTaxRequest2 = (GetTaxRequest) null;
      }
      if (flag1)
      {
        nullable = order.Hold;
        using (this.SuppressRequireControlTotalScope(nullable.GetValueOrDefault()))
        {
          order.IsTaxValid = new bool?(true);
          order.IsUnbilledTaxValid = new bool?(true);
          order = ((PXSelectBase<POOrder>) this.Base.Document).Update(order);
          this.SkipTaxCalcAndSave();
          return order;
        }
      }
      GetTaxResult result1 = (GetTaxResult) null;
      GetTaxResult resultUnbilled = (GetTaxResult) null;
      bool flag2 = false;
      if (getTaxRequest1 != null)
      {
        result1 = itaxProvider.GetTax(getTaxRequest1);
        if (!((ResultBase) result1).IsSuccess)
          flag2 = true;
      }
      if (getTaxRequest2 != null)
      {
        resultUnbilled = itaxProvider.GetTax(getTaxRequest2);
        if (!((ResultBase) resultUnbilled).IsSuccess)
          flag2 = true;
      }
      if (!flag2)
      {
        this.ApplyExternalTaxes(order, result1, resultUnbilled);
      }
      else
      {
        ResultBase result2 = (ResultBase) (result1 ?? resultUnbilled);
        if (result2 != null)
          this.LogMessages(result2);
        throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
      }
    }
    return order;
  }

  public virtual void ApplyExternalTaxes(
    POOrder order,
    GetTaxResult result,
    GetTaxResult resultUnbilled)
  {
    try
    {
      this.ApplyTax(order, result, resultUnbilled);
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
  }

  [PXOverride]
  public virtual void Persist(System.Action baseImpl)
  {
    if (((PXSelectBase<POOrder>) this.Base.Document).Current?.OrderType == "DP" && this.CalculateTaxesUsingExternalProvider(PX.Objects.SO.SOOrder.PK.Find((PXGraph) this.Base, ((PXSelectBase<POOrder>) this.Base.Document).Current.SOOrderType, ((PXSelectBase<POOrder>) this.Base.Document).Current.SOOrderNbr)?.TaxZoneID))
      this.GetToAddress(((PXSelectBase<POOrder>) this.Base.Document).Current).With<IAddressLocation, IAddressBase>(new Func<IAddressLocation, IAddressBase>(this.ValidAddressFrom<POOrder.shipAddressID>));
    try
    {
      ++this._nesting;
      baseImpl();
      if (((PXSelectBase<POOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<POOrder>) this.Base.Document).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave || ((PXSelectBase<POOrder>) this.Base.Document).Current.IsTaxValid.GetValueOrDefault())
        return;
      if (!PXLongOperation.IsLongOperationContext())
      {
        if (this._nesting != 1)
          return;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new POOrderEntryExternalTax.\u003C\u003Ec__DisplayClass5_0()
        {
          doc = new POOrder()
          {
            OrderType = ((PXSelectBase<POOrder>) this.Base.Document).Current.OrderType,
            OrderNbr = ((PXSelectBase<POOrder>) this.Base.Document).Current.OrderNbr
          }
        }, __methodptr(\u003CPersist\u003Eb__0)));
      }
      else
        this.CalculateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
    }
    finally
    {
      --this._nesting;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<POOrder> e)
  {
    if (e.Row == null || !this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) || e.Row.IsTaxValid.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<POOrder.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POOrder>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POOrder> e)
  {
    if (e.Row == null || !this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POOrder>>) e).Cache.ObjectsEqual<POOrder.shipDestType, POOrder.shipToBAccountID, POOrder.shipToLocationID, POOrder.branchID, POOrder.entityUsageType, POOrder.externalTaxExemptionNumber, POOrder.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POOrder> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(e.Row.OrderType != "RS") || !(e.Row.TaxCalcMode == "G") || !this.IsExternalAPTaxTypeIsUseTax())
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POOrder>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APInvoice.taxCalcMode>((object) e.Row, (object) e.Row.TaxCalcMode, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The Gross tax calculation mode cannot be used in a document for which use taxes are calculated by an external tax provider.", (PXErrorLevel) 4));
  }

  public virtual bool IsExternalAPTaxTypeIsUseTax()
  {
    bool flag = false;
    PX.Objects.TX.TaxZone taxZone = ((PXSelectBase<PX.Objects.TX.TaxZone>) this.Base.taxzone).SelectSingle(Array.Empty<object>());
    if (taxZone != null && taxZone.IsExternal.GetValueOrDefault() && taxZone.ExternalAPTaxType == "P")
      flag = true;
    return flag;
  }

  protected virtual void _(PX.Data.Events.RowInserted<POLine> e)
  {
    if (((PXSelectBase<POOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<POOrder>) this.Base.Document).Current.TaxZoneID))
      return;
    ((PXSelectBase<POOrder>) this.Base.Document).Current.IsTaxValid = new bool?(false);
    ((PXSelectBase<POOrder>) this.Base.Document).Update(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POLine> e)
  {
    if (((PXSelectBase<POOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<POOrder>) this.Base.Document).Current.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POLine>>) e).Cache.ObjectsEqual<POLine.inventoryID, POLine.tranDesc, POLine.extCost, POLine.promisedDate, POLine.taxCategoryID>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase<POOrder>) this.Base.Document).Current.IsTaxValid = new bool?(false);
    ((PXSelectBase<POOrder>) this.Base.Document).Update(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POLine> e)
  {
    if (((PXSelectBase<POOrder>) this.Base.Document).Current == null || !this.CalculateTaxesUsingExternalProvider(((PXSelectBase<POOrder>) this.Base.Document).Current.TaxZoneID))
      return;
    this.InvalidateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POShipAddress> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POShipAddress>>) e).Cache.ObjectsEqual<POShipAddress.postalCode, POShipAddress.countryID, POShipAddress.state>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<POShipAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POShipAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<POShipAddress, POShipAddress.overrideAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<POOrder>) this.Base.Document).Current);
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(POOrder order)
  {
    if (order == null)
      throw new PXArgumentException(nameof (order));
    PX.Objects.AP.Vendor vendor = (PX.Objects.AP.Vendor) ((PXSelectBase) this.Base.vendor).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    IAddressLocation fromAddress = this.GetFromAddress(order);
    IAddressLocation toAddress = this.GetToAddress(order);
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address from the sales order.");
    if (toAddress == null)
      throw new PXException("The system failed to get the To address from the sales order.");
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
    taxRequest.CurrencyCode = order.CuryID;
    taxRequest.CustomerCode = vendor.AcctCD;
    taxRequest.BAccountClassID = vendor.ClassID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = $"PO.{order.OrderType}.{order.OrderNbr}";
    taxRequest.DocDate = order.ExpectedDate.GetValueOrDefault();
    taxRequest.LocationCode = this.GetExternalTaxProviderLocationCode(order);
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(order.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(order.TaxZoneID) : order.TaxCalcMode;
    int num = 1;
    taxRequest.CustomerUsageType = order.EntityUsageType;
    if (!string.IsNullOrEmpty(order.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = order.ExternalTaxExemptionNumber;
    taxRequest.DocType = (TaxDocumentType) 3;
    PXSelectJoin<POLine, LeftJoin<PX.Objects.IN.InventoryItem, On<POLine.FK.InventoryItem>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.salesAcctID>>>>, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POLine.lineNbr>>> pxSelectJoin = new PXSelectJoin<POLine, LeftJoin<PX.Objects.IN.InventoryItem, On<POLine.FK.InventoryItem>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.salesAcctID>>>>, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POLine.lineNbr>>>((PXGraph) this.Base);
    taxRequest.Discount = (Decimal) num * order.CuryDiscTot.GetValueOrDefault();
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      POLine line = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      bool flag = taxRequest.Discount != 0M && ((line.DocumentDiscountRate ?? 1M) != 1M || (line.GroupDiscountRate ?? 1M) != 1M);
      TaxCartItem taxCartItem = new TaxCartItem();
      taxCartItem.Index = line.LineNbr.GetValueOrDefault();
      taxCartItem.UnitPrice = (Decimal) num * line.CuryUnitCost.GetValueOrDefault();
      taxCartItem.Amount = (Decimal) num * line.CuryExtCost.GetValueOrDefault();
      taxCartItem.Description = line.TranDesc;
      taxCartItem.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(order, line));
      taxCartItem.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(order, line));
      taxCartItem.ItemCode = inventoryItem.InventoryCD;
      taxCartItem.Quantity = Math.Abs(line.OrderQty.GetValueOrDefault());
      taxCartItem.UOM = line.UOM;
      taxCartItem.Discounted = new bool?(flag);
      taxCartItem.RevAcct = account.AccountCD;
      taxCartItem.TaxCode = line.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      taxRequest.CartItems.Add(taxCartItem);
    }
    return taxRequest;
  }

  protected virtual GetTaxRequest BuildGetTaxRequestUnbilled(POOrder order)
  {
    if (order == null)
      throw new PXArgumentException(nameof (order));
    PX.Objects.AP.Vendor vendor = (PX.Objects.AP.Vendor) ((PXSelectBase) this.Base.vendor).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) order
    }, Array.Empty<object>());
    IAddressLocation fromAddress = this.GetFromAddress(order);
    IAddressLocation toAddress = this.GetToAddress(order);
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address from the sales order.");
    if (toAddress == null)
      throw new PXException("The system failed to get the To address from the sales order.");
    GetTaxRequest taxRequestUnbilled = new GetTaxRequest();
    taxRequestUnbilled.CompanyCode = this.CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
    taxRequestUnbilled.CurrencyCode = order.CuryID;
    taxRequestUnbilled.CustomerCode = vendor.AcctCD;
    taxRequestUnbilled.BAccountClassID = vendor.ClassID;
    taxRequestUnbilled.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequestUnbilled.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequestUnbilled.DocCode = $"PO.{order.OrderType}.{order.OrderNbr}";
    taxRequestUnbilled.DocDate = order.OrderDate.GetValueOrDefault();
    taxRequestUnbilled.LocationCode = this.GetExternalTaxProviderLocationCode(order);
    taxRequestUnbilled.Discount = 0M;
    taxRequestUnbilled.APTaxType = taxZone.ExternalAPTaxType;
    taxRequestUnbilled.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(order.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(order.TaxZoneID) : order.TaxCalcMode;
    int num1 = 1;
    taxRequestUnbilled.CustomerUsageType = order.EntityUsageType;
    if (!string.IsNullOrEmpty(order.ExternalTaxExemptionNumber))
      taxRequestUnbilled.ExemptionNo = order.ExternalTaxExemptionNumber;
    taxRequestUnbilled.DocType = (TaxDocumentType) 3;
    PXView view = ((PXSelectBase) new PXSelectJoin<POLine, LeftJoin<PX.Objects.IN.InventoryItem, On<POLine.FK.InventoryItem>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.salesAcctID>>>>, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POLine.lineNbr>>>((PXGraph) this.Base)).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      POLine line = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<POLine, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      Decimal? nullable = line.UnbilledAmt;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      {
        TaxCartItem taxCartItem1 = new TaxCartItem();
        taxCartItem1.Index = line.LineNbr.GetValueOrDefault();
        TaxCartItem taxCartItem2 = taxCartItem1;
        Decimal num3 = (Decimal) num1;
        nullable = line.CuryUnitCost;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        Decimal num4 = num3 * valueOrDefault1;
        taxCartItem2.UnitPrice = num4;
        TaxCartItem taxCartItem3 = taxCartItem1;
        Decimal num5 = (Decimal) num1;
        nullable = line.CuryUnbilledAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        Decimal num6 = num5 * valueOrDefault2;
        taxCartItem3.Amount = num6;
        taxCartItem1.Description = line.TranDesc;
        taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(order, line));
        taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(order, line));
        taxCartItem1.ItemCode = inventoryItem.InventoryCD;
        TaxCartItem taxCartItem4 = taxCartItem1;
        nullable = line.BaseUnbilledQty;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        taxCartItem4.Quantity = valueOrDefault3;
        taxCartItem1.UOM = line.UOM;
        taxCartItem1.Discounted = new bool?(false);
        taxCartItem1.RevAcct = account.AccountCD;
        taxCartItem1.TaxCode = line.TaxCategoryID;
        if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
          taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
        taxRequestUnbilled.CartItems.Add(taxCartItem1);
      }
    }
    return taxRequestUnbilled;
  }

  protected virtual void ApplyTax(POOrder order, GetTaxResult result, GetTaxResult resultUnbilled)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) null;
    PX.Objects.AP.Vendor taxAgency = (PX.Objects.AP.Vendor) null;
    if (result.TaxSummary.Length != 0)
    {
      taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
      {
        (object) order
      }, Array.Empty<object>());
      taxAgency = this.GetTaxAgency(this.Base, taxZone);
    }
    PXView view = ((PXSelectBase) this.Base.Taxes).View;
    object[] objArray1 = new object[1]{ (object) order };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<POTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<POTaxTran>) this.Base.Taxes).Delete(PXResult<POTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
    try
    {
      TaxBaseAttribute.SetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, result.TaxSummary[index]);
        if (tax != null)
        {
          POTaxTran poTaxTran = new POTaxTran();
          poTaxTran.OrderType = order.OrderType;
          poTaxTran.OrderNbr = order.OrderNbr;
          poTaxTran.TaxID = tax?.TaxID;
          poTaxTran.CuryTaxAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxAmount));
          poTaxTran.CuryTaxableAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxableAmount));
          poTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          poTaxTran.JurisType = result.TaxSummary[index].JurisType;
          poTaxTran.JurisName = result.TaxSummary[index].JurisName;
          poTaxTran.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          ((PXSelectBase<POTaxTran>) this.Base.Taxes).Insert(poTaxTran);
        }
      }
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, taxCalc);
    }
    using (this.SuppressRequireControlTotalScope(order.Hold.GetValueOrDefault()))
    {
      if (resultUnbilled != null)
        ((PXSelectBase<POOrder>) this.Base.Document).SetValueExt<POOrder.curyUnbilledTaxTotal>(order, (object) Math.Abs(resultUnbilled.TotalTaxAmount));
      order.IsTaxValid = new bool?(true);
      order.IsUnbilledTaxValid = new bool?(true);
      order = ((PXSelectBase<POOrder>) this.Base.Document).Update(order);
    }
    this.SkipTaxCalcAndSave();
  }

  protected override string GetExternalTaxProviderLocationCode(POOrder order)
  {
    return this.GetExternalTaxProviderLocationCode<POLine, KeysRelation<CompositeKey<Field<POLine.orderType>.IsRelatedTo<POOrder.orderType>, Field<POLine.orderNbr>.IsRelatedTo<POOrder.orderNbr>>.WithTablesOf<POOrder, POLine>, POOrder, POLine>.SameAsCurrent, POLine.siteID>(order);
  }

  protected virtual IAddressLocation GetToAddress(POOrder order)
  {
    return EnumerableExtensions.IsIn<string>(order.OrderType, "RO", "BL", "SB", "DP", "PD", Array.Empty<string>()) ? (IAddressLocation) PXResultset<POShipAddress>.op_Implicit(PXSelectBase<POShipAddress, PXSelect<POShipAddress, Where<POShipAddress.addressID, Equal<Required<POOrder.shipAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.ShipAddressID
    })) : (IAddressLocation) GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.BranchID
    })).FirstOrDefault<PX.Objects.CR.Address>();
  }

  protected virtual IAddressLocation GetToAddress(POOrder order, POLine line)
  {
    if (!EnumerableExtensions.IsIn<string>(order.OrderType, "RO", "BL", "SB"))
      return this.GetToAddress(order);
    return (IAddressLocation) GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.FK.Address>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<POLine.siteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new POLine[1]
    {
      line
    }, Array.Empty<object>())).FirstOrDefault<PX.Objects.CR.Address>() ?? this.GetToAddress(order);
  }

  protected virtual PX.Objects.CR.Standalone.Location GetBranchLocation(POOrder order)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) order.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Standalone.Location>.op_Implicit((PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Standalone.Location>) enumerator.Current);
    }
    return (PX.Objects.CR.Standalone.Location) null;
  }

  protected virtual IAddressLocation GetFromAddress(POOrder order)
  {
    return EnumerableExtensions.IsIn<string>(order.OrderType, "RO", "BL", "SB", "DP", "PD", Array.Empty<string>()) ? (IAddressLocation) PXResultset<PORemitAddress>.op_Implicit(PXSelectBase<PORemitAddress, PXSelect<PORemitAddress, Where<PORemitAddress.addressID, Equal<Required<POOrder.remitAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) order.RemitAddressID
    })) : (IAddressLocation) PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.DefAddressID
    }));
  }

  protected virtual IAddressLocation GetFromAddress(POOrder order, POLine line)
  {
    return this.GetFromAddress(order);
  }

  private void InvalidateExternalTax(POOrder order)
  {
    if (order == null || !this.CalculateTaxesUsingExternalProvider(order.TaxZoneID))
      return;
    order.IsTaxValid = new bool?(false);
    order.IsUnbilledTaxValid = new bool?(false);
  }

  private IAddressBase ValidAddressFrom<TFieldSource>(IAddressBase address) where TFieldSource : IBqlField
  {
    return !ExternalTaxBase<POOrderEntry>.IsEmptyAddress(address as IAddressLocation) ? address : throw new PXException(this.PickAddressError<TFieldSource>(address));
  }

  private string PickAddressError<TFieldSource>(IAddressBase address) where TFieldSource : IBqlField
  {
    if (!(typeof (TFieldSource) == typeof (POOrder.shipAddressID)))
      throw new ArgumentOutOfRangeException("Unknown address source used");
    return ((PXResult) ((IQueryable<PXResult<POOrder>>) PXSelectBase<POOrder, PXSelect<POOrder, Where<POOrder.shipAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((POAddress) address).AddressID
    })).First<PXResult<POOrder>>()).GetItem<POOrder>().With<POOrder, string>((Func<POOrder, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName<POOrder>(),
      (object) new EntityHelper((PXGraph) this.Base).GetRowID<POOrder>(e, ", ")
    })));
  }

  private IDisposable SuppressRequireControlTotalScope(bool hold)
  {
    bool requireBlanketControlTotal = ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireBlanketControlTotal.GetValueOrDefault();
    bool requireDropShipControlTotal = ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireDropShipControlTotal.GetValueOrDefault();
    bool requireProjectDropShipControlTotal = ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireProjectDropShipControlTotal.GetValueOrDefault();
    bool requireOrderControlTotal = ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireOrderControlTotal.GetValueOrDefault();
    if (!hold)
    {
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireBlanketControlTotal = new bool?(false);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireDropShipControlTotal = new bool?(false);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireProjectDropShipControlTotal = new bool?(false);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireOrderControlTotal = new bool?(false);
    }
    return Disposable.Create((System.Action) (() =>
    {
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireBlanketControlTotal = new bool?(requireBlanketControlTotal);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireDropShipControlTotal = new bool?(requireDropShipControlTotal);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireProjectDropShipControlTotal = new bool?(requireProjectDropShipControlTotal);
      ((PXSelectBase<POSetup>) this.Base.POSetup).Current.RequireOrderControlTotal = new bool?(requireOrderControlTotal);
    }));
  }
}
