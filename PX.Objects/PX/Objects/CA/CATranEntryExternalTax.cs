// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class CATranEntryExternalTax : ExternalTax<CATranEntry, CAAdj>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public override CAAdj CalculateExternalTax(CAAdj invoice)
  {
    if (this.IsNonTaxable((IAddressBase) this.GetToAddress(invoice)))
    {
      this.ApplyTax(invoice, GetTaxResult.Empty);
      invoice.IsTaxValid = new bool?(true);
      invoice.NonTaxable = new bool?(true);
      invoice.IsTaxSaved = new bool?(false);
      invoice = ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Update(invoice);
      this.SkipTaxCalcAndSave();
      return invoice;
    }
    if (invoice.NonTaxable.GetValueOrDefault())
      ((PXSelectBase<CAAdj>) this.Base.CurrentDocument).SetValueExt<CAAdj.nonTaxable>(invoice, (object) false);
    ITaxProvider itaxProvider = ExternalTaxBase<CATranEntry>.TaxProviderFactory((PXGraph) this.Base, invoice.TaxZoneID);
    GetTaxRequest taxRequest = this.BuildGetTaxRequest(invoice);
    if (taxRequest.CartItems.Count == 0)
    {
      this.ApplyTax(invoice, GetTaxResult.Empty);
      invoice.IsTaxValid = new bool?(true);
      invoice.IsTaxSaved = new bool?(false);
      invoice = ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Update(invoice);
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
        invoice.IsTaxValid = new bool?(true);
        invoice = ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Update(invoice);
        this.SkipTaxCalcAndSave();
      }
      return invoice;
    }
    this.LogMessages((ResultBase) tax);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  [PXOverride]
  public void Persist()
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current) || ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.IsTaxValid.GetValueOrDefault() || this.skipExternalTaxCalcOnSave)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new CATranEntryExternalTax.\u003C\u003Ec__DisplayClass2_0()
    {
      doc = new CAAdj()
      {
        AdjTranType = ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.DocType,
        AdjRefNbr = ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.RefNbr
      }
    }, __methodptr(\u003CPersist\u003Eb__0)));
  }

  protected void _(PX.Data.Events.RowUpdated<CASplit> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CASplit>>) e).Cache.ObjectsEqual<CASplit.accountID, CASplit.inventoryID, CASplit.tranDesc, CASplit.tranAmt, CASplit.taxCategoryID, CASplit.qty, CASplit.taxCategoryID, CASplit.curyUnitPrice>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.IsTaxValid = new bool?(false);
    ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Update(((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current);
  }

  public bool IsDocumentExtTaxValid(CAAdj doc) => doc != null && this.IsExternalTax(doc.TaxZoneID);

  protected void _(PX.Data.Events.RowInserted<CASplit> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current))
      return;
    ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.CAAdjRecords).Cache, (object) ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current);
  }

  protected void _(PX.Data.Events.RowDeleted<CASplit> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current))
      return;
    ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.CAAdjRecords).Cache, (object) ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Current);
  }

  protected void _(PX.Data.Events.RowUpdated<CAAdj> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CAAdj>>) e).Cache.ObjectsEqual<CAAdj.tranDate, CAAdj.taxZoneID, CAAdj.branchID, CAAdj.entityUsageType, CAAdj.externalTaxExemptionNumber, CAAdj.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected void _(PX.Data.Events.RowSelected<CAAdj> e)
  {
    if (e.Row == null || !this.IsExternalTax(e.Row.TaxZoneID) || e.Row.IsTaxValid.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<CAAdj.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CAAdj>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected GetTaxRequest BuildGetTaxRequest(CAAdj invoice)
  {
    return (GetTaxRequest) this.BuildCommitTaxRequest(invoice);
  }

  public virtual CommitTaxRequest BuildCommitTaxRequest(CAAdj invoice)
  {
    if (invoice == null)
      throw new PXArgumentException(nameof (invoice), "The argument cannot be null.");
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>());
    CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
    ((GetTaxRequest) commitTaxRequest).CompanyCode = this.CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
    ((GetTaxRequest) commitTaxRequest).CurrencyCode = invoice.CuryID;
    ((GetTaxRequest) commitTaxRequest).CustomerCode = "N/A";
    IAddressLocation toAddress = this.GetToAddress(invoice);
    IAddressLocation address = toAddress;
    if (toAddress == null)
      throw new PXException("The system failed to get the From address for the document.");
    if (address == null)
      throw new PXException("The system failed to get the To address for the document.");
    ((GetTaxRequest) commitTaxRequest).APTaxType = "S";
    ((GetTaxRequest) commitTaxRequest).OriginAddress = AddressConverter.ConvertTaxAddress(toAddress);
    ((GetTaxRequest) commitTaxRequest).DestinationAddress = AddressConverter.ConvertTaxAddress(address);
    ((GetTaxRequest) commitTaxRequest).DocCode = "CA." + invoice.AdjRefNbr;
    ((GetTaxRequest) commitTaxRequest).DocDate = invoice.TranDate.GetValueOrDefault();
    ((GetTaxRequest) commitTaxRequest).LocationCode = this.GetExternalTaxProviderLocationCode(invoice);
    ((GetTaxRequest) commitTaxRequest).IsTaxSaved = invoice.IsTaxSaved.GetValueOrDefault();
    ((GetTaxRequest) commitTaxRequest).TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(invoice.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(invoice.TaxZoneID) : invoice.TaxCalcMode;
    ((GetTaxRequest) commitTaxRequest).CustomerUsageType = invoice.EntityUsageType;
    if (!string.IsNullOrEmpty(invoice.ExternalTaxExemptionNumber))
      ((GetTaxRequest) commitTaxRequest).ExemptionNo = invoice.ExternalTaxExemptionNumber;
    ((GetTaxRequest) commitTaxRequest).DocType = (TaxDocumentType) 4;
    int num = 1;
    if (invoice.DrCr == "D")
      ((GetTaxRequest) commitTaxRequest).DocType = (TaxDocumentType) 2;
    else
      ((GetTaxRequest) commitTaxRequest).DocType = (TaxDocumentType) 4;
    PXView view = ((PXSelectBase) new PXSelectJoin<CASplit, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CASplit.inventoryID>>>, Where<CASplit.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>, OrderBy<Asc<CASplit.adjRefNbr, Asc<CASplit.lineNbr>>>>((PXGraph) this.Base)).View;
    object[] objArray1 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<CASplit, PX.Objects.IN.InventoryItem> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      CASplit caSplit = PXResult<CASplit, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<CASplit, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      TaxCartItem taxCartItem = new TaxCartItem()
      {
        Index = caSplit.LineNbr.GetValueOrDefault(),
        UnitPrice = (Decimal) num * caSplit.CuryUnitPrice.GetValueOrDefault(),
        Amount = (Decimal) num * caSplit.CuryTranAmt.GetValueOrDefault(),
        Description = caSplit.TranDesc,
        DestinationAddress = ((GetTaxRequest) commitTaxRequest).DestinationAddress,
        OriginAddress = ((GetTaxRequest) commitTaxRequest).OriginAddress,
        ItemCode = inventoryItem.InventoryCD,
        Quantity = Math.Abs(caSplit.Qty.GetValueOrDefault()),
        UOM = caSplit.UOM,
        Discounted = new bool?(((GetTaxRequest) commitTaxRequest).Discount > 0M),
        TaxCode = caSplit.TaxCategoryID
      };
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      ((GetTaxRequest) commitTaxRequest).CartItems.Add(taxCartItem);
    }
    return commitTaxRequest;
  }

  protected void ApplyTax(CAAdj invoice, GetTaxResult result)
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
      vendor = this.GetTaxAgency(this.Base, taxZone);
    }
    PXView view1 = ((PXSelectBase) this.Base.Taxes).View;
    object[] objArray1 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<CATaxTran, PX.Objects.TX.Tax> pxResult in view1.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<CATaxTran>) this.Base.Taxes).Delete(PXResult<CATaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    PXView view2 = ((PXSelectBase) this.Base.Tax_Rows).View;
    object[] objArray3 = new object[1]{ (object) invoice };
    object[] objArray4 = Array.Empty<object>();
    foreach (CATax caTax in view2.SelectMultiBound(objArray3, objArray4))
      ((PXSelectBase<CATax>) this.Base.Tax_Rows).Delete(caTax);
    TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) this.Base.CASplitRecords).Cache, (object) null);
    try
    {
      TaxBaseAttribute.SetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) this.Base.CASplitRecords).Cache, (object) null, TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        result.TaxSummary[index].TaxType = "S";
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, vendor, result.TaxSummary[index]);
        if (tax != null)
        {
          CATaxTran caTaxTran1 = new CATaxTran();
          caTaxTran1.Module = "CA";
          caTaxTran1.TranType = invoice.DocType;
          caTaxTran1.RefNbr = invoice.RefNbr;
          caTaxTran1.TaxID = tax?.TaxID;
          caTaxTran1.CuryTaxAmt = new Decimal?(result.TaxSummary[index].TaxAmount);
          caTaxTran1.CuryTaxableAmt = new Decimal?(result.TaxSummary[index].TaxableAmount);
          caTaxTran1.CuryTaxAmtSumm = new Decimal?(result.TaxSummary[index].TaxAmount);
          caTaxTran1.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          caTaxTran1.TaxType = result.TaxSummary[index].TaxType;
          caTaxTran1.TaxBucketID = new int?(0);
          int? nullable = (int?) tax?.SalesTaxAcctID;
          caTaxTran1.AccountID = nullable ?? vendor.SalesTaxAcctID;
          nullable = (int?) tax?.SalesTaxSubID;
          caTaxTran1.SubID = nullable ?? vendor.SalesTaxSubID;
          caTaxTran1.JurisType = result.TaxSummary[index].JurisType;
          caTaxTran1.JurisName = result.TaxSummary[index].JurisName;
          caTaxTran1.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          caTaxTran1.NonDeductibleTaxRate = new Decimal?(100.0M);
          CATaxTran caTaxTran2 = caTaxTran1;
          hasInclTax = result.TaxSummary[index].TaxCalculationLevel == 1;
          ((PXSelectBase<CATaxTran>) this.Base.Taxes).Insert(caTaxTran2);
        }
      }
      this.InsertTaxDetails(invoice, result, taxZone, vendor);
      this.UpdateTransactionTaxableAmount(invoice, hasInclTax);
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<CASplit.taxCategoryID>(((PXSelectBase) this.Base.CASplitRecords).Cache, (object) null, taxCalc);
    }
    bool valueOrDefault = ((PXSelectBase<CASetup>) this.Base.casetup).Current.RequireControlTotal.GetValueOrDefault();
    if (!invoice.Hold.GetValueOrDefault())
      ((PXSelectBase<CASetup>) this.Base.casetup).Current.RequireControlTotal = new bool?(false);
    try
    {
      ((PXSelectBase) this.Base.CAAdjRecords).Cache.SetValueExt<CAAdj.isTaxSaved>((object) invoice, (object) true);
      ((PXSelectBase<CAAdj>) this.Base.CAAdjRecords).Update(invoice);
    }
    finally
    {
      ((PXSelectBase<CASetup>) this.Base.casetup).Current.RequireControlTotal = new bool?(valueOrDefault);
    }
  }

  protected virtual void UpdateTransactionTaxableAmount(CAAdj invoice, bool hasInclTax)
  {
    foreach (CASplit caSplit1 in ((PXSelectBase) this.Base.CASplitRecords).View.SelectMultiBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>()))
    {
      caSplit1.CuryTaxableAmt = new Decimal?(0M);
      caSplit1.CuryTaxAmt = new Decimal?(0M);
      if (hasInclTax)
      {
        foreach (PXResult<CATax> pxResult in PXSelectBase<CATax, PXViewOf<CATax>.BasedOn<SelectFromBase<CATax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATax.adjTranType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<CATax.adjRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<CATax.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) caSplit1.TranType,
          (object) caSplit1.RefNbr,
          (object) caSplit1.LineNbr
        }))
        {
          CATax caTax = PXResult<CATax>.op_Implicit(pxResult);
          CASplit caSplit2 = caSplit1;
          Decimal? nullable1 = caSplit2.CuryTaxAmt;
          Decimal valueOrDefault = caTax.CuryTaxAmt.GetValueOrDefault();
          caSplit2.CuryTaxAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault) : new Decimal?();
          CASplit caSplit3 = caSplit1;
          nullable1 = caTax.CuryTaxableAmt;
          Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
          caSplit3.CuryTaxableAmt = nullable2;
        }
      }
      ((PXSelectBase<CASplit>) this.Base.CASplitRecords).Update(caSplit1);
    }
  }

  protected virtual void InsertTaxDetails(
    CAAdj invoice,
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
          PXSelect<CATax, Where<CATax.adjTranType, Equal<Current<CAAdj.adjTranType>>, And<CATax.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>>, OrderBy<Asc<CATax.adjTranType, Asc<CATax.adjRefNbr, Asc<CATax.taxID>>>>> taxRows = this.Base.Tax_Rows;
          CATax caTax = new CATax();
          caTax.TranType = invoice.DocType;
          caTax.RefNbr = invoice.RefNbr;
          caTax.LineNbr = new int?(taxLine.Index);
          caTax.TaxID = tax.TaxID;
          caTax.CuryTaxAmt = new Decimal?(taxDetail.TaxAmount);
          caTax.CuryTaxableAmt = new Decimal?(taxDetail.TaxableAmount);
          caTax.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail.Rate) * 100M);
          caTax.CuryExemptedAmt = new Decimal?(taxDetail.ExemptedAmount);
          ((PXSelectBase<CATax>) taxRows).Insert(caTax);
        }
      }
    }
  }

  protected void CancelTax(CAAdj invoice, VoidReasonCode code)
  {
    VoidTaxRequest voidTaxRequest = new VoidTaxRequest();
    voidTaxRequest.CompanyCode = this.CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
    voidTaxRequest.Code = code;
    voidTaxRequest.DocCode = "CA." + invoice.AdjRefNbr;
    voidTaxRequest.DocType = !(invoice.DrCr == "D") ? (TaxDocumentType) 4 : (TaxDocumentType) 2;
    ITaxProvider itaxProvider = ExternalTaxBase<CATranEntry>.TaxProviderFactory((PXGraph) this.Base, invoice.TaxZoneID);
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
    if (((PXSelectBase) this.Base.CAAdjRecords).Cache.GetStatus((object) invoice) != null)
      return;
    ((PXSelectBase) this.Base.CAAdjRecords).Cache.SetStatus((object) invoice, (PXEntryStatus) 1);
  }

  protected IAddressLocation GetToAddress(CAAdj invoice)
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

  protected PX.Objects.CR.Location GetBranchLocation(CAAdj invoice)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) invoice.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Location>.op_Implicit((PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Location>) enumerator.Current);
    }
    return (PX.Objects.CR.Location) null;
  }
}
