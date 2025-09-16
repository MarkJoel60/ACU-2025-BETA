// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARCashSaleEntryExternalTax : ExternalTax<ARCashSaleEntry, ARCashSale>
{
  private bool asynchronousProcess = true;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  protected virtual void _(PX.Data.Events.RowUpdated<ARCashSale> e)
  {
    if (e.Row.Released.GetValueOrDefault() || !this.IsDocumentExtTaxValid(e.Row) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARCashSale>>) e).Cache.ObjectsEqual<ARCashSale.externalTaxExemptionNumber, ARCashSale.avalaraCustomerUsageType, ARRegister.curyDiscountedTaxableTotal, ARCashSale.adjDate, ARCashSale.taxZoneID, ARCashSale.branchID, ARCashSale.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARCashSale> e)
  {
    if (e.Row == null || !this.IsDocumentExtTaxValid(e.Row) || e.Row.IsTaxValid.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<ARCashSale.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARCashSale>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARCashSale> e)
  {
    if (!e.Row.IsTaxSaved.GetValueOrDefault() || e.Row.Released.GetValueOrDefault())
      return;
    if (PXDBOperationExt.Command(e.Operation) == 3)
      this.CancelTax(e.Row, (VoidReasonCode) 2);
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && !((PXSelectBase<ARTran>) this.Base.Transactions).Any<ARTran>())
      this.CancelTax(e.Row, (VoidReasonCode) 2);
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || this.IsExternalTax(e.Row.TaxZoneID) && !this.IsNonTaxable((IAddressBase) this.GetToAddress(e.Row)))
      return;
    this.CancelTax(e.Row, (VoidReasonCode) 2);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARCashSale>) this.Base.Document).Current) || e.Row.Released.GetValueOrDefault())
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARCashSale>) this.Base.Document).Current) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARTran>>) e).Cache.ObjectsEqual<ARTran.accountID, ARTran.inventoryID, ARTran.tranDesc, ARTran.tranAmt, ARTran.tranDate, ARTran.taxCategoryID>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARCashSale>) this.Base.Document).Current) || e.Row.Released.GetValueOrDefault())
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARCashSale>) this.Base.Document).Current == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARShippingAddress>>) e).Cache.ObjectsEqual<ARShippingAddress.postalCode, ARShippingAddress.countryID, ARShippingAddress.state, ARShippingAddress.latitude, ARShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARCashSale>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARCashSale>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<ARShippingAddress, ARShippingAddress.overrideAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARCashSale>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  private void InvalidateExternalTax(ARCashSale doc)
  {
    if (!this.IsExternalTax(doc.TaxZoneID))
      return;
    doc.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) doc);
  }

  public override ARCashSale CalculateExternalTax(ARCashSale invoice)
  {
    if (this.IsNonTaxable((IAddressBase) this.GetToAddress(invoice)))
    {
      this.ApplyTax(invoice, GetTaxResult.Empty);
      invoice.IsTaxValid = new bool?(true);
      invoice.NonTaxable = new bool?(true);
      invoice.IsTaxSaved = new bool?(false);
      invoice = ((PXSelectBase<ARCashSale>) this.Base.Document).Update(invoice);
      this.SkipTaxCalcAndSave();
      return invoice;
    }
    if (invoice.NonTaxable.GetValueOrDefault())
      ((PXSelectBase<ARCashSale>) this.Base.Document).SetValueExt<ARRegister.nonTaxable>(invoice, (object) false);
    ITaxProvider itaxProvider = ExternalTaxBase<ARCashSaleEntry>.TaxProviderFactory((PXGraph) this.Base, invoice.TaxZoneID);
    GetTaxRequest taxRequest = this.BuildGetTaxRequest(invoice);
    if (taxRequest.CartItems.Count == 0)
    {
      this.ApplyTax(invoice, GetTaxResult.Empty);
      invoice.IsTaxValid = new bool?(true);
      invoice.IsTaxSaved = new bool?(false);
      invoice = ((PXSelectBase<ARCashSale>) this.Base.Document).Update(invoice);
      this.SkipTaxCalcAndSave();
      return invoice;
    }
    GetTaxResult tax = itaxProvider.GetTax(taxRequest);
    if (((ResultBase) tax).IsSuccess)
    {
      try
      {
        this.ApplyTax(invoice, tax);
        this.SkipTaxCalcAndSave();
      }
      catch (PXOuterException ex1)
      {
        try
        {
          this.CancelTax(invoice, (VoidReasonCode) 0);
        }
        catch (Exception ex2)
        {
          throw new PXException((Exception) new PXException((Exception) ex1, "The tax amount calculated by the external tax provider cannot be applied to the document.", Array.Empty<object>()), "Failed to cancel the tax application in the external tax provider during the rollback. Check details in the Trace Log.", Array.Empty<object>());
        }
        string str = "The tax amount calculated by the external tax provider cannot be applied to the document.";
        foreach (string innerMessage in ex1.InnerMessages)
          str = str + Environment.NewLine + innerMessage;
        throw new PXException((Exception) ex1, str, Array.Empty<object>());
      }
      catch (Exception ex3)
      {
        try
        {
          this.CancelTax(invoice, (VoidReasonCode) 0);
        }
        catch (Exception ex4)
        {
          throw new PXException((Exception) new PXException(ex3, "The tax amount calculated by the external tax provider cannot be applied to the document.", Array.Empty<object>()), "Failed to cancel the tax application in the external tax provider during the rollback. Check details in the Trace Log.", Array.Empty<object>());
        }
        string str = $"The tax amount calculated by the external tax provider cannot be applied to the document.{Environment.NewLine}{ex3.Message}";
        throw new PXException(ex3, str, Array.Empty<object>());
      }
      PostTaxRequest postTaxRequest = new PostTaxRequest();
      ((GetTaxRequest) postTaxRequest).CompanyCode = taxRequest.CompanyCode;
      ((GetTaxRequest) postTaxRequest).CustomerCode = taxRequest.CustomerCode;
      ((GetTaxRequest) postTaxRequest).BAccountClassID = taxRequest.BAccountClassID;
      ((GetTaxRequest) postTaxRequest).DocCode = taxRequest.DocCode;
      ((GetTaxRequest) postTaxRequest).DocDate = taxRequest.DocDate;
      ((GetTaxRequest) postTaxRequest).DocType = taxRequest.DocType;
      postTaxRequest.TotalAmount = tax.TotalAmount;
      postTaxRequest.TotalTaxAmount = tax.TotalTaxAmount;
      if (((ResultBase) itaxProvider.PostTax(postTaxRequest)).IsSuccess)
      {
        ARCashSale copy = PXCache<ARCashSale>.CreateCopy(invoice);
        copy.IsTaxValid = new bool?(true);
        invoice = ((PXSelectBase<ARCashSale>) this.Base.Document).Update(copy);
        this.SkipTaxCalcAndSave();
      }
      return invoice;
    }
    this.LogMessages((ResultBase) tax);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  [PXOverride]
  public virtual void Persist()
  {
    if (((PXSelectBase<ARCashSale>) this.Base.Document).Current == null || !this.IsExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current.TaxZoneID) || ((PXSelectBase<ARCashSale>) this.Base.Document).Current.IsTaxValid.GetValueOrDefault() || this.skipExternalTaxCalcOnSave)
      return;
    if (PXLongOperation.GetCurrentItem() == null && this.asynchronousProcess)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new ARCashSaleEntryExternalTax.\u003C\u003Ec__DisplayClass14_0()
      {
        currentDoc = ((PXSelectBase<ARCashSale>) this.Base.Document).Current
      }, __methodptr(\u003CPersist\u003Eb__0)));
    }
    else
      this.Base.CalculateExternalTax(((PXSelectBase<ARCashSale>) this.Base.Document).Current);
  }

  [PXOverride]
  public virtual IEnumerable Release(
    PXAdapter adapter,
    ARCashSaleEntryExternalTax.ReleaseDelegate baseRelease)
  {
    List<object> objectList = new List<object>();
    foreach (ARCashSale arCashSale in adapter.Get<ARCashSale>())
      objectList.Add((object) arCashSale);
    this.asynchronousProcess = false;
    ((PXAction) this.Base.Save).Press();
    this.asynchronousProcess = true;
    return baseRelease(new PXAdapter((PXView) new PXView.Dummy((PXGraph) this.Base, adapter.View.BqlSelect, objectList)));
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(ARCashSale invoice)
  {
    if (invoice == null)
      throw new PXArgumentException(nameof (invoice), "The argument cannot be null.");
    Customer customer = (Customer) ((PXSelectBase) this.Base.customer).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>());
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXSelectBase) this.Base.location).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>());
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
    taxRequest.CurrencyCode = invoice.CuryID;
    taxRequest.CustomerCode = customer.AcctCD;
    taxRequest.BAccountClassID = customer.ClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    IAddressLocation fromAddress = this.GetFromAddress(invoice);
    IAddressLocation toAddress = this.GetToAddress(invoice);
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address for the document.");
    if (toAddress == null)
      throw new PXException("The system failed to get the To address for the document.");
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = $"AR.{invoice.DocType}.{invoice.RefNbr}";
    taxRequest.DocDate = invoice.DocDate.GetValueOrDefault();
    taxRequest.LocationCode = this.GetExternalTaxProviderLocationCode(invoice);
    taxRequest.CustomerUsageType = invoice.AvalaraCustomerUsageType;
    taxRequest.IsTaxSaved = invoice.IsTaxSaved.GetValueOrDefault();
    taxRequest.TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(invoice.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(invoice.TaxZoneID) : invoice.TaxCalcMode;
    if (!string.IsNullOrEmpty(invoice.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = invoice.ExternalTaxExemptionNumber;
    taxRequest.DocType = this.GetTaxDocumentType(invoice);
    Sign documentSign = this.GetDocumentSign(invoice);
    PXSelectJoin<ARTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>>, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, Or<ARTran.lineType, IsNull>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> pxSelectJoin = new PXSelectJoin<ARTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>>, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, Or<ARTran.lineType, IsNull>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>>((PXGraph) this.Base);
    taxRequest.Discount = Sign.op_Multiply(documentSign, this.GetDocDiscount().GetValueOrDefault());
    DateTime? nullable = invoice.OrigDocDate;
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      ARTran arTran = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      TaxCartItem taxCartItem = new TaxCartItem();
      taxCartItem.Index = arTran.LineNbr.GetValueOrDefault();
      taxCartItem.UnitPrice = Sign.op_Multiply(documentSign, arTran.CuryUnitPrice.GetValueOrDefault());
      taxCartItem.Amount = Sign.op_Multiply(documentSign, arTran.CuryTranAmt.GetValueOrDefault());
      taxCartItem.Description = arTran.TranDesc;
      taxCartItem.DestinationAddress = taxRequest.DestinationAddress;
      taxCartItem.OriginAddress = taxRequest.OriginAddress;
      taxCartItem.ItemCode = inventoryItem.InventoryCD;
      taxCartItem.Quantity = Math.Abs(arTran.Qty.GetValueOrDefault());
      taxCartItem.UOM = arTran.UOM;
      taxCartItem.Discounted = new bool?(taxRequest.Discount != 0M);
      taxCartItem.RevAcct = account.AccountCD;
      taxCartItem.TaxCode = arTran.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      if (arTran.OrigInvoiceDate.HasValue)
        nullable = arTran.OrigInvoiceDate;
      taxRequest.CartItems.Add(taxCartItem);
    }
    if (invoice.DocType == "RCS" && invoice.OrigDocDate.HasValue)
    {
      taxRequest.TaxOverride.Reason = "Return";
      taxRequest.TaxOverride.TaxDate = new DateTime?(nullable.Value);
      taxRequest.TaxOverride.TaxOverrideType = new TaxOverrideType?((TaxOverrideType) 3);
    }
    return taxRequest;
  }

  public virtual TaxDocumentType GetTaxDocumentType(ARCashSale invoice)
  {
    switch (ARInvoiceType.DrCr(invoice.DocType))
    {
      case "C":
        return (TaxDocumentType) 2;
      case "D":
        return (TaxDocumentType) 6;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  public virtual Sign GetDocumentSign(ARCashSale invoice)
  {
    switch (ARInvoiceType.DrCr(invoice.DocType))
    {
      case "C":
        return Sign.Plus;
      case "D":
        return Sign.Minus;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  protected virtual void ApplyTax(ARCashSale invoice, GetTaxResult result)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) null;
    PX.Objects.AP.Vendor vendor = (PX.Objects.AP.Vendor) null;
    invoice.CuryTaxTotal = new Decimal?(0M);
    bool hasInclTax = false;
    if (result.TaxSummary.Length != 0)
    {
      taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
      {
        (object) invoice
      }, Array.Empty<object>());
      vendor = this.GetTaxAgency(this.Base, taxZone, true);
    }
    PXView view1 = ((PXSelectBase) this.Base.Taxes).View;
    object[] objArray1 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult in view1.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Delete(PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    PXView view2 = ((PXSelectBase) this.Base.Tax_Rows).View;
    object[] objArray3 = new object[1]{ (object) invoice };
    object[] objArray4 = Array.Empty<object>();
    foreach (ARTax arTax in view2.SelectMultiBound(objArray3, objArray4))
      ((PXSelectBase<ARTax>) this.Base.Tax_Rows).Delete(arTax);
    TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
    try
    {
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        result.TaxSummary[index].TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, vendor, result.TaxSummary[index]);
        if (tax != null)
        {
          ARTaxTran arTaxTran1 = new ARTaxTran();
          arTaxTran1.Module = "AR";
          arTaxTran1.TranType = invoice.DocType;
          arTaxTran1.RefNbr = invoice.RefNbr;
          arTaxTran1.TaxID = tax?.TaxID;
          arTaxTran1.CuryTaxAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxAmount));
          arTaxTran1.CuryTaxableAmt = new Decimal?(Math.Abs(result.TaxSummary[index].TaxableAmount));
          arTaxTran1.CuryTaxAmtSumm = new Decimal?(Math.Abs(result.TaxSummary[index].TaxAmount));
          arTaxTran1.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          arTaxTran1.TaxType = result.TaxSummary[index].TaxType;
          arTaxTran1.TaxBucketID = new int?(0);
          int? nullable = (int?) tax?.SalesTaxAcctID;
          arTaxTran1.AccountID = nullable ?? vendor.SalesTaxAcctID;
          nullable = (int?) tax?.SalesTaxSubID;
          arTaxTran1.SubID = nullable ?? vendor.SalesTaxSubID;
          arTaxTran1.JurisType = result.TaxSummary[index].JurisType;
          arTaxTran1.JurisName = result.TaxSummary[index].JurisName;
          arTaxTran1.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          ARTaxTran arTaxTran2 = arTaxTran1;
          hasInclTax = result.TaxSummary[index].TaxCalculationLevel == 1;
          ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Insert(arTaxTran2);
        }
      }
      this.InsertTaxDetails(invoice, result, taxZone, vendor);
      this.UpdateTransactionTaxableAmount(invoice, hasInclTax);
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, taxCalc);
    }
    bool valueOrDefault = ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.RequireControlTotal.GetValueOrDefault();
    if (!invoice.Hold.GetValueOrDefault())
      ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.RequireControlTotal = new bool?(false);
    try
    {
      ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegister.isTaxSaved>((object) invoice, (object) true);
    }
    finally
    {
      ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.RequireControlTotal = new bool?(valueOrDefault);
    }
  }

  protected virtual void InsertTaxDetails(
    ARCashSale invoice,
    GetTaxResult result,
    PX.Objects.TX.TaxZone taxZone,
    PX.Objects.AP.Vendor vendor)
  {
    foreach (TaxLine taxLine in result.TaxLines)
    {
      foreach (TaxDetail taxDetail in taxLine.TaxDetails)
      {
        taxDetail.TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, vendor, taxDetail);
        if (tax != null)
        {
          PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARCashSale.docType>>, And<ARTax.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTax.tranType, Asc<ARTax.refNbr, Asc<ARTax.taxID>>>>> taxRows = this.Base.Tax_Rows;
          ARTax arTax = new ARTax();
          arTax.TranType = invoice.DocType;
          arTax.RefNbr = invoice.RefNbr;
          arTax.LineNbr = new int?(taxLine.Index);
          arTax.TaxID = tax.TaxID;
          arTax.CuryTaxAmt = new Decimal?(Math.Abs(taxDetail.TaxAmount));
          arTax.CuryTaxableAmt = new Decimal?(Math.Abs(taxDetail.TaxableAmount) == 0M ? Math.Abs(taxLine.TaxableAmount) : Math.Abs(taxDetail.TaxableAmount));
          arTax.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail.Rate) * 100M);
          arTax.CuryExemptedAmt = new Decimal?(Math.Abs(taxDetail.ExemptedAmount));
          ((PXSelectBase<ARTax>) taxRows).Insert(arTax);
        }
      }
    }
  }

  protected virtual void UpdateTransactionTaxableAmount(ARCashSale invoice, bool hasInclTax)
  {
    foreach (ARTran arTran1 in ((PXSelectBase) this.Base.Transactions).View.SelectMultiBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>()))
    {
      arTran1.CuryTaxableAmt = new Decimal?(0M);
      arTran1.CuryTaxAmt = new Decimal?(0M);
      if (hasInclTax)
      {
        foreach (PXResult<ARTax> pxResult in PXSelectBase<ARTax, PXViewOf<ARTax>.BasedOn<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.tranType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARTax.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ARTax.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) arTran1.TranType,
          (object) arTran1.RefNbr,
          (object) arTran1.LineNbr
        }))
        {
          ARTax arTax = PXResult<ARTax>.op_Implicit(pxResult);
          ARTran arTran2 = arTran1;
          Decimal? nullable1 = arTran2.CuryTaxAmt;
          Decimal valueOrDefault = arTax.CuryTaxAmt.GetValueOrDefault();
          arTran2.CuryTaxAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault) : new Decimal?();
          ARTran arTran3 = arTran1;
          nullable1 = arTax.CuryTaxableAmt;
          Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
          arTran3.CuryTaxableAmt = nullable2;
        }
      }
      ((PXSelectBase<ARTran>) this.Base.Transactions).Update(arTran1);
    }
  }

  protected virtual void CancelTax(ARCashSale invoice, VoidReasonCode code)
  {
    string taxZoneID = ((ARCashSale) PrimaryKeyOf<ARCashSale>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<ARCashSale.docType, ARCashSale.refNbr>>.Find((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<ARCashSale.docType, ARCashSale.refNbr>) invoice, (PKFindOptions) 0))?.TaxZoneID ?? invoice.TaxZoneID;
    VoidTaxRequest voidTaxRequest = new VoidTaxRequest();
    voidTaxRequest.CompanyCode = this.CompanyCodeFromBranch(taxZoneID, invoice.BranchID);
    voidTaxRequest.Code = code;
    voidTaxRequest.DocCode = $"AR.{invoice.DocType}.{invoice.RefNbr}";
    voidTaxRequest.DocType = this.GetTaxDocumentType(invoice);
    ITaxProvider itaxProvider = ExternalTaxBase<ARCashSaleEntry>.TaxProviderFactory((PXGraph) this.Base, taxZoneID);
    if (itaxProvider == null)
      return;
    VoidTaxResult result = itaxProvider.VoidTax(voidTaxRequest);
    if (!((ResultBase) result).IsSuccess)
    {
      this.LogMessages((ResultBase) result);
      throw new PXException("Failed to delete taxes in the external tax provider. Check details in the Trace Log.");
    }
    invoice.IsTaxSaved = new bool?(false);
    invoice.IsTaxValid = new bool?(false);
    if (((PXSelectBase) this.Base.Document).Cache.GetStatus((object) invoice) != null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetStatus((object) invoice, (PXEntryStatus) 1);
  }

  protected override string GetExternalTaxProviderLocationCode(ARCashSale invoice)
  {
    return this.GetExternalTaxProviderLocationCode<ARTran, KeysRelation<CompositeKey<Field<ARTran.tranType>.IsRelatedTo<ARCashSale.docType>, Field<ARTran.refNbr>.IsRelatedTo<ARCashSale.refNbr>>.WithTablesOf<ARCashSale, ARTran>, ARCashSale, ARTran>.SameAsCurrent, ARTran.siteID>(invoice);
  }

  protected virtual IAddressLocation GetFromAddress(ARCashSale invoice)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) invoice.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (IAddressLocation) PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Address>.op_Implicit((PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Address>) enumerator.Current);
    }
    return (IAddressLocation) null;
  }

  protected virtual IAddressLocation GetToAddress(ARCashSale invoice)
  {
    return (IAddressLocation) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>());
  }

  public virtual bool IsDocumentExtTaxValid(ARCashSale doc)
  {
    return doc != null && this.IsExternalTax(doc.TaxZoneID) && !doc.InstallmentNbr.HasValue;
  }

  public delegate IEnumerable ReleaseDelegate(PXAdapter adapter);
}
