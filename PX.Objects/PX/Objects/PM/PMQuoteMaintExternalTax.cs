// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaintExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteMaintExternalTax : ExternalTaxBase<PMQuoteMaint, PMQuote>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  [PXOverride]
  public void Persist()
  {
    if (((PXSelectBase<PMQuote>) this.Base.Quote).Current == null || !this.IsExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave || ((PXSelectBase<PMQuote>) this.Base.Quote).Current.IsTaxValid.GetValueOrDefault())
      return;
    if (PXLongOperation.GetCurrentItem() == null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new PMQuoteMaintExternalTax.\u003C\u003Ec__DisplayClass1_0()
      {
        currentDoc = ((PXSelectBase<PMQuote>) this.Base.Quote).Current
      }, __methodptr(\u003CPersist\u003Eb__0)));
    }
    else
      this.CalculateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  public override void SkipTaxCalcAndSave()
  {
    try
    {
      this.skipExternalTaxCalcOnSave = true;
      ((PXAction) this.Base.Save).Press();
    }
    finally
    {
      this.skipExternalTaxCalcOnSave = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMQuote> e)
  {
    if (e.Row == null || !this.IsExternalTax(e.Row.TaxZoneID) || e.Row.IsTaxValid.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<PMQuote.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMQuote>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMQuote> e)
  {
    if (!this.IsExternalTax(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMQuote>>) e).Cache.ObjectsEqual<PMQuote.contactID, PMQuote.taxZoneID, PMQuote.branchID, PMQuote.locationID, PMQuote.curyAmount, PMQuote.shipAddressID, PMQuote.avalaraCustomerUsageType, PMQuote.externalTaxExemptionNumber>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunityProducts> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CROpportunityDiscountDetail> e)
  {
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CRShippingAddress> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CRShippingAddress>>) e).Cache.ObjectsEqual<CRShippingAddress.postalCode, CRShippingAddress.countryID, CRShippingAddress.state, CRShippingAddress.latitude, CRShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRShippingAddress> e)
  {
    if (e.Row == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMQuote>) this.Base.Quote).Current);
  }

  private void InvalidateExternalTax(PMQuote quote)
  {
    if (!this.IsExternalTax(quote.TaxZoneID))
      return;
    quote.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Quote).Cache, (object) quote, true);
  }

  public override PMQuote CalculateExternalTax(PMQuote quote)
  {
    if (this.IsNonTaxable((IAddressBase) this.GetToAddress(quote)))
    {
      this.ApplyTax(quote, GetTaxResult.Empty);
      quote.IsTaxValid = new bool?(true);
      quote = ((PXSelectBase<PMQuote>) this.Base.Quote).Update(quote);
      this.SkipTaxCalcAndSave();
      return quote;
    }
    ITaxProvider itaxProvider = ExternalTaxBase<PMQuoteMaint>.TaxProviderFactory((PXGraph) this.Base, quote.TaxZoneID);
    GetTaxRequest getTaxRequest = (GetTaxRequest) null;
    bool flag = true;
    if (!quote.IsTaxValid.GetValueOrDefault())
    {
      getTaxRequest = this.BuildGetTaxRequest(quote);
      if (getTaxRequest.CartItems.Count > 0)
        flag = false;
      else
        getTaxRequest = (GetTaxRequest) null;
    }
    if (flag)
    {
      quote.IsTaxValid = new bool?(true);
      quote = ((PXSelectBase<PMQuote>) this.Base.Quote).Update(quote);
      this.SkipTaxCalcAndSave();
      return quote;
    }
    GetTaxResult tax = itaxProvider.GetTax(getTaxRequest);
    if (((ResultBase) tax).IsSuccess)
    {
      try
      {
        this.ApplyTax(quote, tax);
        quote.IsTaxValid = new bool?(true);
        quote = ((PXSelectBase<PMQuote>) this.Base.Quote).Update(quote);
        this.SkipTaxCalcAndSave();
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
        object[] objArray = Array.Empty<object>();
        throw new PXException(ex, "The tax amount calculated by the external tax provider cannot be applied to the document.", objArray);
      }
      return quote;
    }
    this.LogMessages((ResultBase) tax);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(PMQuote quote)
  {
    if (quote == null)
      throw new PXArgumentException(nameof (quote));
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) quote.BAccountID
    }));
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) quote.BAccountID,
      (object) quote.LocationID
    }));
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelectReadonly<PX.Objects.TX.TaxZone, Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) quote.TaxZoneID
    }));
    IAddressLocation fromAddress = this.GetFromAddress(quote);
    IAddressLocation toAddress = this.GetToAddress(quote);
    if (fromAddress == null)
      throw new PXException("The system cannot obtain the From address from the document.");
    if (toAddress == null)
      throw new PXException("The system cannot obtain the To address from the document.");
    int num1 = 1;
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(quote.TaxZoneID, ((PXGraph) this.Base).Accessinfo.BranchID);
    taxRequest.CurrencyCode = quote.CuryID;
    taxRequest.CustomerCode = baccount?.AcctCD;
    taxRequest.BAccountClassID = baccount?.ClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = $"CR.{quote.OpportunityID}";
    taxRequest.DocDate = quote.DocumentDate.GetValueOrDefault();
    taxRequest.Discount = (Decimal) num1 * quote.CuryLineDocDiscountTotal.GetValueOrDefault();
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(quote.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(quote.TaxZoneID) : quote.TaxCalcMode;
    taxRequest.CustomerUsageType = quote.AvalaraCustomerUsageType;
    if (!string.IsNullOrEmpty(quote.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = quote.ExternalTaxExemptionNumber;
    taxRequest.DocType = (TaxDocumentType) 1;
    foreach (PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in PXSelectBase<CROpportunityProducts, PXSelectJoin<CROpportunityProducts, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.salesAcctID>>>>, Where<CROpportunityProducts.quoteID, Equal<Required<PMQuote.quoteID>>>, OrderBy<Asc<CROpportunityProducts.lineNbr>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) quote.QuoteID
    }))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = opportunityProducts.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Decimal num2 = (Decimal) num1;
      Decimal? nullable = opportunityProducts.CuryUnitPrice;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      Decimal num3 = num2 * valueOrDefault1;
      taxCartItem2.UnitPrice = num3;
      TaxCartItem taxCartItem3 = taxCartItem1;
      Decimal num4 = (Decimal) num1;
      nullable = opportunityProducts.CuryAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num5 = num4 * valueOrDefault2;
      taxCartItem3.Amount = num5;
      taxCartItem1.Description = opportunityProducts.Descr;
      taxCartItem1.DestinationAddress = taxRequest.DestinationAddress;
      taxCartItem1.OriginAddress = taxRequest.OriginAddress;
      taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      nullable = opportunityProducts.Qty;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      taxCartItem4.Quantity = valueOrDefault3;
      taxCartItem1.UOM = opportunityProducts.UOM;
      taxCartItem1.Discounted = new bool?(taxRequest.Discount != 0M);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = opportunityProducts.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      taxRequest.CartItems.Add(taxCartItem1);
    }
    return taxRequest;
  }

  protected void ApplyTax(PMQuote quote, GetTaxResult result)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) null;
    PX.Objects.AP.Vendor taxAgency = (PX.Objects.AP.Vendor) null;
    if (result.TaxSummary.Length != 0)
    {
      taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelectReadonly<PX.Objects.TX.TaxZone, Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) quote.TaxZoneID
      }));
      taxAgency = this.GetTaxAgency(this.Base, taxZone);
    }
    PXView view = ((PXSelectBase) this.Base.Taxes).View;
    object[] objArray1 = new object[1]{ (object) quote };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<CRTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<CRTaxTran>) this.Base.Taxes).Delete(PXResult<CRTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    TaxCalc taxCalc1 = TaxBaseAttribute.GetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null);
    PX.Objects.Extensions.SalesTax.Document extension = ((PXSelectBase) this.Base.QuoteCurrent).Cache.GetExtension<PX.Objects.Extensions.SalesTax.Document>((object) ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current);
    TaxCalc taxCalc2 = extension.TaxCalc.Value;
    try
    {
      TaxBaseAttribute.SetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null, TaxCalc.ManualCalc);
      extension.TaxCalc = new TaxCalc?(TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        result.TaxSummary[index].TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, result.TaxSummary[index]);
        if (tax != null)
        {
          CRTaxTran crTaxTran = new CRTaxTran();
          crTaxTran.QuoteID = quote.QuoteID;
          crTaxTran.TaxID = tax?.TaxID;
          crTaxTran.LineNbr = new int?(index + 1);
          crTaxTran.CuryTaxAmt = new Decimal?(result.TaxSummary[index].TaxAmount);
          crTaxTran.CuryTaxableAmt = new Decimal?(result.TaxSummary[index].TaxableAmount);
          crTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          crTaxTran.JurisType = result.TaxSummary[index].JurisType;
          crTaxTran.JurisName = result.TaxSummary[index].JurisName;
          crTaxTran.TaxZoneID = taxZone.TaxZoneID;
          crTaxTran.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          ((PXSelectBase<CRTaxTran>) this.Base.Taxes).Insert(crTaxTran);
        }
      }
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<CROpportunityProducts.taxCategoryID>(((PXSelectBase) this.Base.Products).Cache, (object) null, taxCalc1);
      extension.TaxCalc = new TaxCalc?(taxCalc2);
    }
  }

  protected IAddressLocation GetFromAddress(PMQuote quote)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.BAccount.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) quote.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (IAddressLocation) PXResult<PX.Objects.GL.Branch, PX.Objects.CR.BAccount, PX.Objects.CR.Address>.op_Implicit((PXResult<PX.Objects.GL.Branch, PX.Objects.CR.BAccount, PX.Objects.CR.Address>) enumerator.Current);
    }
    return (IAddressLocation) null;
  }

  protected IAddressLocation GetToAddress(PMQuote quote)
  {
    CRShippingAddress toAddress1 = (CRShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
    {
      (object) quote
    }, Array.Empty<object>());
    if (toAddress1 != null)
      return (IAddressLocation) toAddress1;
    PX.Objects.CR.Address toAddress2 = (PX.Objects.CR.Address) null;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) quote.BAccountID,
      (object) quote.LocationID
    }));
    if (location != null)
      toAddress2 = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) location.DefAddressID
      }));
    return (IAddressLocation) toAddress2;
  }
}
