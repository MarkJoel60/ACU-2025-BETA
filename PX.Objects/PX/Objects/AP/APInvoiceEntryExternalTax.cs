// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceEntryExternalTax : ExternalTax<APInvoiceEntry, APInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.avalaraTax>();

  public virtual bool CalculateTaxesUsingExternalProvider(string taxZoneID)
  {
    bool flag = this.Base.Document.Current != null && this.Base.Document.Current.ExternalTaxesImportInProgress.GetValueOrDefault();
    return ExternalTaxBase<APInvoiceEntry>.IsExternalTax((PXGraph) this.Base, taxZoneID) && !flag;
  }

  public virtual bool IsExternalAPTaxTypeIsUseTax()
  {
    bool flag = false;
    PX.Objects.TX.TaxZone taxZone = this.Base.taxzone.SelectSingle();
    if (taxZone != null && taxZone.IsExternal.GetValueOrDefault() && taxZone.ExternalAPTaxType == "P")
      flag = true;
    return flag;
  }

  public override APInvoice CalculateExternalTax(APInvoice invoice)
  {
    try
    {
      if (this.CalculateTaxesUsingExternalProvider(invoice.TaxZoneID) && !invoice.InstallmentNbr.HasValue && !invoice.IsChildRetainageDocument())
      {
        if (this.IsNonTaxable((IAddressBase) this.GetToAddress(invoice)))
        {
          this.ApplyTax(invoice, GetTaxResult.Empty);
          invoice.IsTaxValid = new bool?(true);
          invoice.NonTaxable = new bool?(true);
          invoice.IsTaxSaved = new bool?(false);
          invoice = this.Base.Document.Update(invoice);
          this.SkipTaxCalcAndSave();
          return invoice;
        }
        if (invoice.NonTaxable.GetValueOrDefault())
          this.Base.Document.SetValueExt<APInvoice.nonTaxable>(invoice, (object) false);
        ITaxProvider itaxProvider = ExternalTaxBase<APInvoiceEntry>.TaxProviderFactory((PXGraph) this.Base, invoice.TaxZoneID);
        GetTaxRequest getTaxRequest = this.BuildTaxRequest(invoice);
        if (getTaxRequest.CartItems.Count == 0)
        {
          this.ApplyTax(invoice, GetTaxResult.Empty);
          invoice.IsTaxValid = new bool?(true);
          invoice.IsTaxSaved = new bool?(false);
          invoice = this.Base.Document.Update(invoice);
          this.SkipTaxCalcAndSave();
          return invoice;
        }
        GetTaxResult tax = itaxProvider.GetTax(getTaxRequest);
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
            string format = "The tax amount calculated by the external tax provider cannot be applied to the document.";
            foreach (string innerMessage in ex1.InnerMessages)
              format = format + Environment.NewLine + innerMessage;
            throw new PXException((Exception) ex1, format, Array.Empty<object>());
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
            string format = $"The tax amount calculated by the external tax provider cannot be applied to the document.{Environment.NewLine}{ex3.Message}";
            throw new PXException(ex3, format, Array.Empty<object>());
          }
          PostTaxRequest postTaxRequest = new PostTaxRequest();
          ((GetTaxRequest) postTaxRequest).CompanyCode = getTaxRequest.CompanyCode;
          ((GetTaxRequest) postTaxRequest).CustomerCode = getTaxRequest.CustomerCode;
          ((GetTaxRequest) postTaxRequest).BAccountClassID = getTaxRequest.BAccountClassID;
          ((GetTaxRequest) postTaxRequest).DocCode = getTaxRequest.DocCode;
          ((GetTaxRequest) postTaxRequest).DocDate = getTaxRequest.DocDate;
          ((GetTaxRequest) postTaxRequest).DocType = getTaxRequest.DocType;
          postTaxRequest.TotalAmount = tax.TotalAmount;
          postTaxRequest.TotalTaxAmount = tax.TotalTaxAmount;
          if (((ResultBase) itaxProvider.PostTax(postTaxRequest)).IsSuccess)
          {
            APInvoice copy = PXCache<APInvoice>.CreateCopy(invoice);
            copy.IsTaxValid = new bool?(true);
            invoice = this.Base.Document.Update(copy);
            this.SkipTaxCalcAndSave();
          }
        }
        else
        {
          PXTrace.WriteError(string.Join(", ", ((ResultBase) tax).Messages));
          throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
        }
      }
      return invoice;
    }
    finally
    {
      if (invoice.IsTaxSaved.GetValueOrDefault() && invoice.Scheduled.GetValueOrDefault())
      {
        this.VoidScheduledDocument(invoice);
        this.SkipTaxCalcAndSave();
      }
    }
  }

  [PXOverride]
  public virtual void Persist()
  {
    if (this.Base.Document.Current == null || !this.IsExternalTax(this.Base.Document.Current.TaxZoneID) || this.Base.Document.Current.InstallmentNbr.HasValue)
      return;
    bool? nullable = this.Base.Document.Current.IsTaxValid;
    if (nullable.GetValueOrDefault() || this.skipExternalTaxCalcOnSave)
      return;
    nullable = this.Base.Document.Current.Released;
    if (nullable.GetValueOrDefault())
      return;
    if (PXLongOperation.GetCurrentItem() == null)
    {
      APInvoice currentDoc = this.Base.Document.Current;
      PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        instance.Document.Current = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) instance, (object) currentDoc.DocType, (object) currentDoc.RefNbr);
        instance.CalculateExternalTax(instance.Document.Current);
      }));
    }
    else
      this.Base.CalculateExternalTax(this.Base.Document.Current);
  }

  [PXOverride]
  public virtual APRegister OnBeforeRelease(APRegister doc)
  {
    this.skipExternalTaxCalcOnSave = true;
    return doc;
  }

  protected virtual void _(PX.Data.Events.RowSelected<APInvoice> e)
  {
    if (e.Row == null)
      return;
    int num1 = this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) ? 1 : 0;
    bool? nullable;
    int num2;
    if (num1 == 0)
    {
      nullable = e.Row.IsRetainageReversing;
      if (!nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsRetainageDocument;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
        goto label_6;
      }
    }
    num2 = 0;
label_6:
    bool flag = num2 != 0;
    this.Base.Taxes.Cache.AllowDelete &= flag;
    this.Base.Taxes.Cache.AllowInsert &= flag;
    this.Base.Taxes.Cache.AllowUpdate &= flag;
    if (num1 == 0)
      return;
    nullable = e.Row.IsTaxValid;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<APInvoice.curyTaxTotal>(e.Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APInvoice> e)
  {
    if (e.Row.Released.GetValueOrDefault())
      return;
    if (e.Row.IsTaxSaved.GetValueOrDefault())
    {
      if (e.Operation.Command() == PXDBOperation.Delete)
        this.CancelTax(e.Row, (VoidReasonCode) 2);
      if (EnumerableExtensions.IsIn<PXDBOperation>(e.Operation.Command(), PXDBOperation.Insert, PXDBOperation.Update) && !this.Base.Transactions.Any<APTran>())
        this.CancelTax(e.Row, (VoidReasonCode) 2);
      if (EnumerableExtensions.IsIn<PXDBOperation>(e.Operation.Command(), PXDBOperation.Insert, PXDBOperation.Update) && (!ExternalTaxBase<APInvoiceEntry>.IsExternalTax((PXGraph) this.Base, e.Row.TaxZoneID) && !e.Row.ExternalTaxesImportInProgress.GetValueOrDefault() || this.IsNonTaxable((IAddressBase) this.GetToAddress(e.Row))))
        this.CancelTax(e.Row, (VoidReasonCode) 2);
    }
    if (this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) && e.Row.IsOriginalRetainageDocument())
    {
      APSetup current = this.Base.APSetup.Current;
      if ((current != null ? (current.RetainTaxes.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXException("Retained taxes calculated by an external tax provider are not supported.");
    }
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || !(e.Row.TaxCalcMode == "G") || !this.IsExternalAPTaxTypeIsUseTax())
      return;
    e.Cache.RaiseExceptionHandling<APInvoice.taxCalcMode>((object) e.Row, (object) e.Row.TaxCalcMode, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The Gross tax calculation mode cannot be used in a document for which use taxes are calculated by an external tax provider.", PXErrorLevel.Error));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APTran> e)
  {
    if (!this.IsDocumentExtTaxValid(this.Base.Document.Current) || e.Cache.ObjectsEqual<APTran.accountID, APTran.inventoryID, APTran.tranAmt, APTran.tranDate, APTran.taxCategoryID>((object) e.Row, (object) e.OldRow))
      return;
    this.Base.Document.Current.IsTaxValid = new bool?(false);
    this.Base.Document.Update(this.Base.Document.Current);
  }

  public virtual bool IsDocumentExtTaxValid(APInvoice doc)
  {
    return doc != null && this.CalculateTaxesUsingExternalProvider(doc.TaxZoneID) && !doc.InstallmentNbr.HasValue;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APTran> e)
  {
    this.Base.Document.Current.IsTaxValid = new bool?(!this.IsDocumentExtTaxValid(this.Base.Document.Current));
  }

  protected virtual void _(PX.Data.Events.RowInserted<APTran> e)
  {
    if (!this.IsDocumentExtTaxValid(this.Base.Document.Current))
      return;
    this.Base.Document.Current.IsTaxValid = new bool?(false);
    this.Base.Document.Cache.MarkUpdated((object) this.Base.Document.Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APInvoice> e)
  {
    if (e.Row.Released.GetValueOrDefault() || !this.IsDocumentExtTaxValid(e.Row) || e.Cache.ObjectsEqual<APRegister.curyDiscountedTaxableTotal, APInvoice.docDate, APInvoice.branchID, APInvoice.entityUsageType, APInvoice.externalTaxExemptionNumber, APInvoice.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected virtual GetTaxRequest BuildTaxRequest(APInvoice invoice)
  {
    return (GetTaxRequest) this.BuildCommitTaxRequest(invoice);
  }

  public virtual CommitTaxRequest BuildCommitTaxRequest(APInvoice invoice)
  {
    if (invoice == null)
      throw new PXArgumentException(nameof (invoice), "The argument cannot be null.");
    Vendor vendor = (Vendor) this.Base.vendor.View.SelectSingleBound(new object[1]
    {
      (object) invoice
    });
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) this.Base.taxzone.View.SelectSingleBound(new object[1]
    {
      (object) invoice
    });
    CommitTaxRequest commitTaxRequest1 = new CommitTaxRequest();
    ((GetTaxRequest) commitTaxRequest1).CompanyCode = this.CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
    ((GetTaxRequest) commitTaxRequest1).CurrencyCode = invoice.CuryID;
    ((GetTaxRequest) commitTaxRequest1).CustomerCode = vendor.AcctCD;
    ((GetTaxRequest) commitTaxRequest1).BAccountClassID = vendor.ClassID;
    IAddressLocation fromAddress = this.GetFromAddress(invoice);
    IAddressLocation toAddress = this.GetToAddress(invoice);
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address for the document.");
    if (toAddress == null)
      throw new PXException("The system failed to get the To address for the document.");
    ((GetTaxRequest) commitTaxRequest1).OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    ((GetTaxRequest) commitTaxRequest1).DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    ((GetTaxRequest) commitTaxRequest1).DocCode = $"AP.{invoice.DocType}.{invoice.RefNbr}";
    ((GetTaxRequest) commitTaxRequest1).DocDate = invoice.DocDate.GetValueOrDefault();
    ((GetTaxRequest) commitTaxRequest1).LocationCode = this.GetExternalTaxProviderLocationCode(invoice);
    ((GetTaxRequest) commitTaxRequest1).APTaxType = taxZone.ExternalAPTaxType;
    CommitTaxRequest commitTaxRequest2 = commitTaxRequest1;
    bool? nullable1 = invoice.IsTaxSaved;
    int num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((GetTaxRequest) commitTaxRequest2).IsTaxSaved = num1 != 0;
    ((GetTaxRequest) commitTaxRequest1).TaxCalculationMode = !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || !(invoice.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(invoice.TaxZoneID) : invoice.TaxCalcMode;
    ((GetTaxRequest) commitTaxRequest1).CustomerUsageType = invoice.EntityUsageType;
    if (!string.IsNullOrEmpty(invoice.ExternalTaxExemptionNumber))
      ((GetTaxRequest) commitTaxRequest1).ExemptionNo = invoice.ExternalTaxExemptionNumber;
    ((GetTaxRequest) commitTaxRequest1).DocType = this.GetTaxDocumentType(invoice);
    Sign documentSign = this.GetDocumentSign(invoice);
    PXSelectJoin<APTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<APTran.accountID>>>>, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<APTran.lineType, NotEqual<SOLineType.discount>>>>, OrderBy<Asc<APTran.tranType, Asc<APTran.refNbr, Asc<APTran.lineNbr>>>>> pxSelectJoin = new PXSelectJoin<APTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<APTran.accountID>>>>, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<APTran.lineType, NotEqual<SOLineType.discount>>>>, OrderBy<Asc<APTran.tranType, Asc<APTran.refNbr, Asc<APTran.lineNbr>>>>>((PXGraph) this.Base);
    ((GetTaxRequest) commitTaxRequest1).Discount = Sign.op_Multiply(documentSign, this.GetDocDiscount().GetValueOrDefault());
    APSetup current = this.Base.APSetup.Current;
    int num2;
    if (current == null)
    {
      num2 = 1;
    }
    else
    {
      nullable1 = current.RetainTaxes;
      num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = num2 != 0 && invoice.IsOriginalRetainageDocument();
    PXView view = pxSelectJoin.View;
    object[] currents = new object[1]{ (object) invoice };
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<APTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(currents, objArray))
    {
      APTran tran = (APTran) pxResult;
      PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) pxResult;
      PX.Objects.GL.Account account = (PX.Objects.GL.Account) pxResult;
      Decimal? nullable2;
      int num3;
      if (((GetTaxRequest) commitTaxRequest1).Discount != 0M)
      {
        nullable2 = tran.DocumentDiscountRate;
        if (!((nullable2 ?? 1M) != 1M))
        {
          nullable2 = tran.GroupDiscountRate;
          num3 = (nullable2 ?? 1M) != 1M ? 1 : 0;
        }
        else
          num3 = 1;
      }
      else
        num3 = 0;
      bool flag2 = num3 != 0;
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = tran.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Sign sign1 = documentSign;
      nullable2 = tran.CuryUnitCost;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal num4 = Sign.op_Multiply(sign1, valueOrDefault1);
      taxCartItem2.UnitPrice = num4;
      TaxCartItem taxCartItem3 = taxCartItem1;
      Sign sign2 = documentSign;
      nullable2 = tran.CuryTranAmt;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num5;
      if (!flag1)
      {
        num5 = 0M;
      }
      else
      {
        nullable2 = tran.CuryRetainageAmt;
        num5 = nullable2.GetValueOrDefault();
      }
      Decimal num6 = valueOrDefault2 + num5;
      Decimal num7 = Sign.op_Multiply(sign2, num6);
      taxCartItem3.Amount = num7;
      taxCartItem1.Description = tran.TranDesc;
      taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(invoice, tran));
      taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(invoice, tran));
      taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      nullable2 = tran.Qty;
      Decimal num8 = System.Math.Abs(nullable2.GetValueOrDefault());
      taxCartItem4.Quantity = num8;
      taxCartItem1.UOM = tran.UOM;
      taxCartItem1.Discounted = new bool?(flag2);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = tran.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      ((GetTaxRequest) commitTaxRequest1).CartItems.Add(taxCartItem1);
    }
    if (invoice.DocType == "ADR" && invoice.OrigDocDate.HasValue)
    {
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.Reason = "Debit Adjustment";
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.TaxDate = new System.DateTime?(invoice.OrigDocDate.Value);
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.TaxOverrideType = new TaxOverrideType?((TaxOverrideType) 3);
    }
    return commitTaxRequest1;
  }

  public virtual TaxDocumentType GetTaxDocumentType(APInvoice invoice)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) this.Base.taxzone.View.SelectSingleBound(new object[1]
    {
      (object) invoice
    });
    switch (invoice.DrCr)
    {
      case "D":
        return (TaxDocumentType) 4;
      case "C":
        return taxZone == null || !taxZone.ExternalAPTaxType.Equals("P") ? (TaxDocumentType) 6 : (TaxDocumentType) 4;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  public virtual Sign GetDocumentSign(APInvoice invoice)
  {
    switch (invoice.DrCr)
    {
      case "D":
        return Sign.Plus;
      case "C":
        return Sign.Minus;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  protected virtual void ApplyTax(APInvoice invoice, GetTaxResult result)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) null;
    Vendor vendor = (Vendor) null;
    invoice.CuryTaxTotal = new Decimal?(0M);
    if (result.TaxSummary.Length != 0)
    {
      taxZone = (PX.Objects.TX.TaxZone) this.Base.taxzone.View.SelectSingleBound(new object[1]
      {
        (object) invoice
      });
      vendor = this.GetTaxAgency(this.Base, taxZone, true);
    }
    PXView view1 = this.Base.Taxes.View;
    object[] currents1 = new object[1]{ (object) invoice };
    object[] objArray1 = Array.Empty<object>();
    foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in view1.SelectMultiBound(currents1, objArray1))
      this.Base.Taxes.Delete((APTaxTran) pxResult);
    this.Base.Views.Caches.Add(typeof (PX.Objects.TX.Tax));
    PXView view2 = this.Base.Tax_Rows.View;
    object[] currents2 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (APTax apTax in view2.SelectMultiBound(currents2, objArray2))
      this.Base.Tax_Rows.Delete(apTax);
    TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null);
    bool? nullable1 = this.Base.APSetup.Current.RequireControlTotal;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    try
    {
      nullable1 = invoice.Hold;
      if (!nullable1.GetValueOrDefault())
        this.Base.APSetup.Current.RequireControlTotal = new bool?(false);
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
      for (int index = 0; index < result.TaxSummary.Length; ++index)
      {
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, vendor, result.TaxSummary[index]);
        if (tax != null)
        {
          APTaxTran apTaxTran = new APTaxTran();
          apTaxTran.Module = "AP";
          apTaxTran.TranType = invoice.DocType;
          apTaxTran.RefNbr = invoice.RefNbr;
          apTaxTran.TaxID = tax?.TaxID;
          apTaxTran.CuryTaxAmt = new Decimal?(System.Math.Abs(result.TaxSummary[index].TaxAmount));
          apTaxTran.CuryTaxableAmt = new Decimal?(System.Math.Abs(result.TaxSummary[index].TaxableAmount));
          apTaxTran.CuryTaxAmtSumm = new Decimal?(System.Math.Abs(result.TaxSummary[index].TaxAmount));
          apTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          apTaxTran.CuryID = invoice.CuryID;
          apTaxTran.TaxType = result.TaxSummary[index].TaxType;
          apTaxTran.TaxBucketID = new int?(0);
          int? nullable2 = (int?) tax?.SalesTaxAcctID;
          apTaxTran.AccountID = nullable2 ?? vendor.SalesTaxAcctID;
          nullable2 = (int?) tax?.SalesTaxSubID;
          apTaxTran.SubID = nullable2 ?? vendor.SalesTaxSubID;
          apTaxTran.JurisType = result.TaxSummary[index].JurisType;
          apTaxTran.JurisName = result.TaxSummary[index].JurisName;
          apTaxTran.IsTaxInclusive = new bool?(result.TaxSummary[index].TaxCalculationLevel == 1);
          this.Base.Taxes.Insert(apTaxTran);
        }
      }
      this.InsertTaxDetails(invoice, result, taxZone, vendor);
    }
    finally
    {
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, taxCalc);
      this.Base.APSetup.Current.RequireControlTotal = new bool?(valueOrDefault);
    }
    this.Base.Document.Cache.SetValueExt<APInvoice.isTaxSaved>((object) invoice, (object) true);
  }

  private void InsertTaxDetails(
    APInvoice invoice,
    GetTaxResult result,
    PX.Objects.TX.TaxZone taxZone,
    Vendor vendor)
  {
    foreach (TaxLine taxLine in result.TaxLines)
    {
      foreach (TaxDetail taxDetail in taxLine.TaxDetails)
      {
        PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, vendor, taxDetail);
        if (tax != null)
        {
          PXSelect<APTax, Where<APTax.tranType, Equal<Current<APInvoice.docType>>, And<APTax.refNbr, Equal<Current<APInvoice.refNbr>>>>, OrderBy<Asc<APTax.tranType, Asc<APTax.refNbr, Asc<APTax.taxID>>>>> taxRows = this.Base.Tax_Rows;
          APTax apTax = new APTax();
          apTax.TranType = invoice.DocType;
          apTax.RefNbr = invoice.RefNbr;
          apTax.LineNbr = new int?(taxLine.Index);
          apTax.TaxID = tax.TaxID;
          apTax.CuryTaxAmt = new Decimal?(System.Math.Abs(taxDetail.TaxAmount));
          apTax.CuryTaxableAmt = new Decimal?(System.Math.Abs(taxDetail.TaxableAmount) == 0M ? System.Math.Abs(taxDetail.ExemptedAmount) : System.Math.Abs(taxDetail.TaxableAmount));
          apTax.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail.Rate) * 100M);
          taxRows.Insert(apTax);
        }
      }
    }
  }

  public void ApplyExternalTaxes(APInvoice invoice, GetTaxResult result)
  {
    try
    {
      this.ApplyTax(invoice, result);
    }
    catch (PXOuterException ex)
    {
      string format = "The tax amount calculated by the external tax provider cannot be applied to the document.";
      foreach (string innerMessage in ex.InnerMessages)
        format = format + Environment.NewLine + innerMessage;
      throw new PXException((Exception) ex, format, Array.Empty<object>());
    }
    catch (Exception ex)
    {
      string format = $"The tax amount calculated by the external tax provider cannot be applied to the document.{Environment.NewLine}{ex.Message}";
      throw new PXException(ex, format, Array.Empty<object>());
    }
  }

  protected virtual void CancelTax(APInvoice invoice, VoidReasonCode code)
  {
    string taxZoneID = PrimaryKeyOf<APInvoice>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<APInvoice.docType, APInvoice.refNbr>>.Find((PXGraph) this.Base, invoice)?.TaxZoneID ?? invoice.TaxZoneID;
    VoidTaxRequest voidTaxRequest = new VoidTaxRequest();
    voidTaxRequest.CompanyCode = this.CompanyCodeFromBranch(taxZoneID, invoice.BranchID);
    voidTaxRequest.Code = code;
    voidTaxRequest.DocCode = $"AP.{invoice.DocType}.{invoice.RefNbr}";
    voidTaxRequest.DocType = this.GetTaxDocumentType(invoice);
    ITaxProvider itaxProvider = ExternalTaxBase<APInvoiceEntry>.TaxProviderFactory((PXGraph) this.Base, taxZoneID);
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
    if (this.Base.Document.Cache.GetStatus((object) invoice) != PXEntryStatus.Notchanged)
      return;
    this.Base.Document.Cache.SetStatus((object) invoice, PXEntryStatus.Updated);
  }

  public virtual void VoidScheduledDocument(APInvoice invoice)
  {
    int num = invoice.IsTaxValid.GetValueOrDefault() ? 1 : 0;
    this.CancelTax(invoice, (VoidReasonCode) 2);
    if (num == 0)
      return;
    invoice.IsTaxValid = new bool?(true);
    this.Base.Document.Cache.MarkUpdated((object) invoice);
  }

  protected override string GetExternalTaxProviderLocationCode(APInvoice invoice)
  {
    return this.GetExternalTaxProviderLocationCode<APTran, KeysRelation<CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<APTran.tranType>.IsRelatedTo<APInvoice.docType>, PX.Data.ReferentialIntegrity.Attributes.Field<APTran.refNbr>.IsRelatedTo<APInvoice.refNbr>>.WithTablesOf<APInvoice, APTran>, APInvoice, APTran>.SameAsCurrent, APTran.siteID>(invoice);
  }

  protected virtual IAddressLocation GetToAddress(APInvoice invoice)
  {
    return (IAddressLocation) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, (object) invoice.BranchID).RowCast<PX.Objects.CR.Address>().FirstOrDefault<PX.Objects.CR.Address>();
  }

  protected virtual IAddressLocation GetToAddress(APInvoice invoice, APTran tran)
  {
    POShipAddress toAddress1 = (POShipAddress) PXSelectBase<POShipAddress, PXSelectJoin<POShipAddress, InnerJoin<PX.Objects.PO.POOrder, On<POShipAddress.addressID, Equal<PX.Objects.PO.POOrder.shipAddressID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<APTran.pOOrderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<APTran.pONbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APTran[1]
    {
      tran
    });
    if (EnumerableExtensions.IsIn<string>(tran.POOrderType, "DP", "PD"))
      return (IAddressLocation) toAddress1;
    PX.Objects.CR.Address toAddress2 = (PX.Objects.CR.Address) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<APTran.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<APTran.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Current<APTran.receiptLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APTran[1]
    {
      tran
    });
    if (toAddress2 != null)
      return (IAddressLocation) toAddress2;
    return (IAddressLocation) (PX.Objects.CR.Address) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Current<APTran.pOOrderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<APTran.pONbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Current<APTran.pOLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APTran[1]
    {
      tran
    }) ?? (IAddressLocation) toAddress1 ?? this.GetToAddress(invoice);
  }

  protected virtual PX.Objects.CR.Standalone.Location GetBranchLocation(APInvoice invoice)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base).Select((object) invoice.BranchID).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (PX.Objects.CR.Standalone.Location) (PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Standalone.Location>) enumerator.Current;
    }
    return (PX.Objects.CR.Standalone.Location) null;
  }

  protected virtual IAddressLocation GetFromAddress(APInvoice invoice)
  {
    return (IAddressLocation) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.CR.Standalone.Location.locationID, Equal<Required<PX.Objects.CR.Standalone.Location.locationID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, (object) (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>() ? invoice.SuppliedByVendorLocationID : invoice.VendorLocationID)).RowCast<PX.Objects.CR.Address>().FirstOrDefault<PX.Objects.CR.Address>();
  }

  protected virtual IAddressLocation GetFromAddress(APInvoice invoice, APTran tran)
  {
    IAddressLocation fromAddress1 = (IAddressLocation) PXSelectBase<PORemitAddress, PXSelectJoin<PORemitAddress, InnerJoin<PX.Objects.PO.POOrder, On<PORemitAddress.addressID, Equal<PX.Objects.PO.POOrder.remitAddressID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<APTran.pOOrderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Current<APTran.pONbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APTran[1]
    {
      tran
    }).RowCast<PORemitAddress>().FirstOrDefault<PORemitAddress>();
    if (fromAddress1 != null)
      return fromAddress1;
    PX.Objects.CR.Address fromAddress2 = PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceipt.vendorLocationID, Equal<PX.Objects.CR.Standalone.Location.locationID>>, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<APTran.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<APTran.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Current<APTran.receiptLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APTran[1]
    {
      tran
    }).RowCast<PX.Objects.CR.Address>().FirstOrDefault<PX.Objects.CR.Address>();
    if (fromAddress2 != null)
      return (IAddressLocation) fromAddress2;
    return (IAddressLocation) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<APInvoice.vendorLocationID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new APInvoice[1]
    {
      invoice
    }).RowCast<PX.Objects.CR.Address>().FirstOrDefault<PX.Objects.CR.Address>();
  }

  protected override Decimal? GetDocDiscount()
  {
    return new Decimal?(((Decimal?) this.Base.Document.Current?.CuryDiscTot).GetValueOrDefault());
  }
}
