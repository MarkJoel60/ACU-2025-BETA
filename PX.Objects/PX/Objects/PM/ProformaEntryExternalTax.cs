// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaEntryExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProformaEntryExternalTax : ExternalTax<ProformaEntry, PMProforma>
{
  public PXAction<PMProforma> recalcExternalTax;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public override PMProforma CalculateExternalTax(PMProforma doc)
  {
    return this.IsExternalTax(doc.TaxZoneID) ? this.CalculateExternalTax(doc, false) : doc;
  }

  public virtual PMProforma CalculateExternalTax(PMProforma doc, bool forceRecalculate)
  {
    IAddressLocation toAddress = this.GetToAddress(doc);
    GetTaxRequest getTaxRequest = (GetTaxRequest) null;
    bool flag1 = true;
    if (!doc.IsTaxValid.GetValueOrDefault() | forceRecalculate && !this.IsNonTaxable((IAddressBase) toAddress))
    {
      getTaxRequest = this.BuildGetTaxRequest(doc);
      if (getTaxRequest.CartItems.Count > 0)
        flag1 = false;
      else
        getTaxRequest = (GetTaxRequest) null;
    }
    if (flag1)
    {
      doc.CuryTaxTotal = new Decimal?(0M);
      doc.IsTaxValid = new bool?(true);
      ((PXSelectBase<PMProforma>) this.Base.Document).Update(doc);
      foreach (PXResult<PMTaxTran> pxResult in ((PXSelectBase<PMTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
        ((PXSelectBase<PMTaxTran>) this.Base.Taxes).Delete(PXResult<PMTaxTran>.op_Implicit(pxResult));
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXGraph) this.Base).Persist(typeof (PMTaxTran), (PXDBOperation) 3);
        ((PXGraph) this.Base).Persist(typeof (PMProforma), (PXDBOperation) 1);
        PXTimeStampScope.PutPersisted(((PXSelectBase) this.Base.Document).Cache, (object) doc, new object[1]
        {
          (object) PXDatabase.SelectTimeStamp()
        });
        transactionScope.Complete();
      }
      return doc;
    }
    GetTaxResult result = (GetTaxResult) null;
    ITaxProvider itaxProvider = ExternalTaxBase<ProformaEntry>.TaxProviderFactory((PXGraph) this.Base, doc.TaxZoneID);
    bool flag2 = false;
    if (getTaxRequest != null)
    {
      result = itaxProvider.GetTax(getTaxRequest);
      if (!((ResultBase) result).IsSuccess)
        flag2 = true;
    }
    if (!flag2)
    {
      try
      {
        this.ApplyTax(doc, result);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          doc.IsTaxValid = new bool?(true);
          ((PXSelectBase<PMProforma>) this.Base.Document).Update(doc);
          ((PXGraph) this.Base).Persist(typeof (PMProforma), (PXDBOperation) 1);
          PXTimeStampScope.PutPersisted(((PXSelectBase) this.Base.Document).Cache, (object) doc, new object[1]
          {
            (object) PXDatabase.SelectTimeStamp()
          });
          transactionScope.Complete();
        }
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
      return doc;
    }
    this.LogMessages((ResultBase) result);
    throw new PXException("Failed to get taxes from the external tax provider. Check Trace Log for details.");
  }

  public virtual void RecalculateExternalTaxes()
  {
    if (((PXSelectBase<PMProforma>) this.Base.Document).Current == null || !this.IsExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current.TaxZoneID) || this.skipExternalTaxCalcOnSave || ((PXSelectBase<PMProforma>) this.Base.Document).Current.IsTaxValid.GetValueOrDefault())
      return;
    if (this.Base.RecalculateExternalTaxesSync)
    {
      this.CalculateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProformaEntryExternalTax.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new ProformaEntryExternalTax.\u003C\u003Ec__DisplayClass3_0()
      {
        doc = new PMProforma()
      };
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.doc.RefNbr = ((PXSelectBase<PMProforma>) this.Base.Document).Current.RefNbr;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.doc.RevisionID = ((PXSelectBase<PMProforma>) this.Base.Document).Current.RevisionID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass30, __methodptr(\u003CRecalculateExternalTaxes\u003Eb__0)));
    }
  }

  [PXOverride]
  public virtual void Persist(System.Action basePersist)
  {
    if (this.Base.RecalculateExternalTaxesSync && ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PMProforma>) this.Base.Document).Current) != 2)
    {
      this.RecalculateExternalTaxes();
      basePersist();
    }
    else
    {
      basePersist();
      this.RecalculateExternalTaxes();
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RecalcExternalTax(PXAdapter adapter)
  {
    ProformaEntryExternalTax entryExternalTax = this;
    if (((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current != null && entryExternalTax.IsExternalTax(((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current.TaxZoneID))
    {
      PMProforma current = ((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current;
      entryExternalTax.CalculateExternalTax(((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current, true);
      ((PXGraph) entryExternalTax.Base).Clear((PXClearOption) 3);
      ((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current = PXResultset<PMProforma>.op_Implicit(((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Search<PMProforma.refNbr>((object) current.RefNbr, Array.Empty<object>()));
      yield return (object) ((PXSelectBase<PMProforma>) entryExternalTax.Base.Document).Current;
    }
    else
    {
      foreach (object obj in adapter.Get())
        yield return obj;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMProforma> e)
  {
    if (!this.IsExternalTax(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMProforma>>) e).Cache.ObjectsEqual<PMProforma.invoiceDate, PMProforma.taxZoneID, PMProforma.customerID, PMProforma.locationID, PMProforma.externalTaxExemptionNumber, PMProforma.avalaraCustomerUsageType, PMProforma.shipAddressID, PMProforma.branchID>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProforma> e)
  {
    if (((PXSelectBase<PMProforma>) this.Base.Document).Current == null)
      return;
    bool flag1 = this.Base.CanEditDocument(e.Row);
    bool flag2 = this.IsExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current.TaxZoneID);
    PXCache cache1 = ((PXSelectBase) this.Base.Taxes).Cache;
    bool? nullable;
    int num1;
    if (flag1 && !flag2)
    {
      nullable = e.Row.Hold;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    cache1.AllowInsert = num1 != 0;
    PXCache cache2 = ((PXSelectBase) this.Base.Taxes).Cache;
    int num2;
    if (flag1 && !flag2)
    {
      nullable = e.Row.Hold;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    cache2.AllowUpdate = num2 != 0;
    PXCache cache3 = ((PXSelectBase) this.Base.Taxes).Cache;
    int num3;
    if (flag1 && !flag2)
    {
      nullable = e.Row.Hold;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    cache3.AllowDelete = num3 != 0;
    if (!flag2)
      return;
    nullable = e.Row.IsTaxValid;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<PMProforma.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProforma>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProformaTransactLine> e)
  {
    this.InvalidateTax((PMProformaLine) e.Row, (PMProformaLine) ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMProformaTransactLine>>) e).Cache.CreateInstance());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMProformaTransactLine> e)
  {
    this.InvalidateTax((PMProformaLine) e.Row, (PMProformaLine) e.OldRow);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProformaProgressLine> e)
  {
    this.InvalidateTax((PMProformaLine) e.Row, (PMProformaLine) ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMProformaProgressLine>>) e).Cache.CreateInstance());
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMProformaProgressLine> e)
  {
    this.InvalidateTax((PMProformaLine) e.Row, (PMProformaLine) e.OldRow);
  }

  public virtual void InvalidateTax(PMProformaLine row, PMProformaLine oldRow)
  {
    if (((PXSelectBase<PMProforma>) this.Base.Document).Current == null)
      return;
    int? nullable = row.AccountID;
    int? accountId = oldRow.AccountID;
    if (nullable.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable.HasValue == accountId.HasValue)
    {
      int? inventoryId = row.InventoryID;
      nullable = oldRow.InventoryID;
      if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
      {
        Decimal? lineTotal1 = row.LineTotal;
        Decimal? lineTotal2 = oldRow.LineTotal;
        if (lineTotal1.GetValueOrDefault() == lineTotal2.GetValueOrDefault() & lineTotal1.HasValue == lineTotal2.HasValue && !(row.TaxCategoryID != oldRow.TaxCategoryID) && !(row.Description != oldRow.Description))
          return;
      }
    }
    this.InvalidateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProforma>) this.Base.Document).Current == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMShippingAddress>>) e).Cache.ObjectsEqual<PMShippingAddress.postalCode, PMShippingAddress.countryID, PMShippingAddress.state>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProforma>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProforma>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<PMShippingAddress, PMShippingAddress.overrideAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProforma>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<PMProforma>) this.Base.Document).Current);
  }

  private void InvalidateExternalTax(PMProforma doc)
  {
    if (!this.IsExternalTax(doc.TaxZoneID))
      return;
    doc.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) doc, true);
  }

  public virtual GetTaxRequest BuildGetTaxRequest(PMProforma doc)
  {
    if (doc == null)
      throw new PXArgumentException(nameof (doc));
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) ((PXSelectBase) this.Base.Customer).View.SelectSingleBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) ((PXSelectBase) this.Base.Location).View.SelectSingleBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
    IAddressLocation fromAddress = this.GetFromAddress(doc);
    IAddressLocation toAddress = this.GetToAddress(doc);
    if (fromAddress == null)
      throw new PXException("The system has failed to obtain the From address from the pro forma invoice.");
    if (toAddress == null)
      throw new PXException("The system has failed to obtain the To address from the pro forma invoice.");
    GetTaxRequest taxRequest = new GetTaxRequest();
    taxRequest.CompanyCode = this.CompanyCodeFromBranch(doc.TaxZoneID, doc.BranchID);
    taxRequest.CurrencyCode = doc.CuryID;
    taxRequest.CustomerCode = customer.AcctCD;
    taxRequest.BAccountClassID = customer.CustomerClassID;
    taxRequest.TaxRegistrationID = location?.TaxRegistrationID;
    taxRequest.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    taxRequest.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
    taxRequest.DocCode = "PM." + doc.RefNbr;
    taxRequest.DocDate = doc.InvoiceDate.GetValueOrDefault();
    taxRequest.LocationCode = this.GetExternalTaxProviderLocationCode(doc);
    taxRequest.DocType = (TaxDocumentType) 1;
    taxRequest.CustomerUsageType = doc.AvalaraCustomerUsageType;
    taxRequest.APTaxType = taxZone.ExternalAPTaxType;
    taxRequest.TaxCalculationMode = this.GetTaxProviderTaxCalcMode(doc.TaxZoneID);
    if (!string.IsNullOrEmpty(doc.ExternalTaxExemptionNumber))
      taxRequest.ExemptionNo = doc.ExternalTaxExemptionNumber;
    foreach (PXResult<PMProformaProgressLine> pxResult in ((PXSelectBase<PMProformaProgressLine>) this.Base.ProgressiveLines).Select(Array.Empty<object>()))
    {
      PMProformaProgressLine proformaProgressLine = PXResult<PMProformaProgressLine>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PMProformaProgressLine.inventoryID>(((PXSelectBase) this.Base.ProgressiveLines).Cache, (object) proformaProgressLine);
      PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<PMProformaProgressLine.accountID>(((PXSelectBase) this.Base.ProgressiveLines).Cache, (object) proformaProgressLine);
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = proformaProgressLine.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Decimal? nullable = proformaProgressLine.CuryUnitPrice;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      taxCartItem2.UnitPrice = valueOrDefault1;
      TaxCartItem taxCartItem3 = taxCartItem1;
      nullable = proformaProgressLine.CuryLineTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      taxCartItem3.Amount = valueOrDefault2;
      taxCartItem1.Description = proformaProgressLine.Description;
      taxCartItem1.DestinationAddress = taxRequest.DestinationAddress;
      taxCartItem1.OriginAddress = taxRequest.OriginAddress;
      if (inventoryItem != null)
        taxCartItem1.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      nullable = proformaProgressLine.Qty;
      Decimal num = Math.Abs(nullable.GetValueOrDefault());
      taxCartItem4.Quantity = num;
      taxCartItem1.UOM = proformaProgressLine.UOM;
      taxCartItem1.Discounted = new bool?(taxRequest.Discount != 0M);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = proformaProgressLine.TaxCategoryID;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      taxRequest.CartItems.Add(taxCartItem1);
    }
    foreach (PXResult<PMProformaTransactLine> pxResult in ((PXSelectBase<PMProformaTransactLine>) this.Base.TransactionLines).Select(Array.Empty<object>()))
    {
      PMProformaTransactLine proformaTransactLine = PXResult<PMProformaTransactLine>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PMProformaTransactLine.inventoryID>(((PXSelectBase) this.Base.TransactionLines).Cache, (object) proformaTransactLine);
      PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<PMProformaTransactLine.accountID>(((PXSelectBase) this.Base.TransactionLines).Cache, (object) proformaTransactLine);
      TaxCartItem taxCartItem5 = new TaxCartItem();
      taxCartItem5.Index = proformaTransactLine.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem6 = taxCartItem5;
      Decimal? nullable = proformaTransactLine.CuryUnitPrice;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      taxCartItem6.UnitPrice = valueOrDefault3;
      TaxCartItem taxCartItem7 = taxCartItem5;
      nullable = proformaTransactLine.CuryLineTotal;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      taxCartItem7.Amount = valueOrDefault4;
      taxCartItem5.Description = proformaTransactLine.Description;
      taxCartItem5.DestinationAddress = taxRequest.DestinationAddress;
      taxCartItem5.OriginAddress = taxRequest.OriginAddress;
      if (inventoryItem != null)
        taxCartItem5.ItemCode = inventoryItem.InventoryCD;
      TaxCartItem taxCartItem8 = taxCartItem5;
      nullable = proformaTransactLine.Qty;
      Decimal num = Math.Abs(nullable.GetValueOrDefault());
      taxCartItem8.Quantity = num;
      taxCartItem5.UOM = proformaTransactLine.UOM;
      taxCartItem5.Discounted = new bool?(taxRequest.Discount != 0M);
      taxCartItem5.RevAcct = account.AccountCD;
      taxCartItem5.TaxCode = proformaTransactLine.TaxCategoryID;
      taxRequest.CartItems.Add(taxCartItem5);
    }
    return taxRequest;
  }

  public virtual void ApplyTax(PMProforma doc, GetTaxResult result)
  {
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) ((PXSelectBase) this.Base.taxzone).View.SelectSingleBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
    PX.Objects.AP.Vendor taxAgency = this.GetTaxAgency(this.Base, taxZone);
    if (result != null)
    {
      PXView view = ((PXSelectBase) new PXSelectJoin<PMTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<PMTaxTran.taxID>>>, Where<PMTaxTran.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTaxTran.revisionID, Equal<Current<PMProforma.revisionID>>>>>((PXGraph) this.Base)).View;
      object[] objArray1 = new object[1]{ (object) doc };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<PMTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
        ((PXSelectBase<PMTaxTran>) this.Base.Taxes).Delete(PXResult<PMTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
      ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.TX.Tax));
      List<TaxDetail> taxDetailList = new List<TaxDetail>();
      for (int index = 0; index < result.TaxSummary.Length; ++index)
        taxDetailList.Add(result.TaxSummary[index]);
      TaxCalc taxCalc1 = TaxBaseAttribute.GetTaxCalc<PMProformaProgressLine.taxCategoryID>(((PXSelectBase) this.Base.ProgressiveLines).Cache, (object) null);
      TaxCalc taxCalc2 = TaxBaseAttribute.GetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.Base.TransactionLines).Cache, (object) null);
      try
      {
        TaxBaseAttribute.SetTaxCalc<PMProformaProgressLine.taxCategoryID>(((PXSelectBase) this.Base.ProgressiveLines).Cache, (object) null, TaxCalc.ManualCalc);
        TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.Base.TransactionLines).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (TaxDetail taxDetail in taxDetailList)
        {
          taxDetail.TaxType = "S";
          PX.Objects.TX.Tax tax = this.CreateTax(this.Base, taxZone, taxAgency, taxDetail);
          if (tax != null)
          {
            PMTaxTran pmTaxTran = new PMTaxTran();
            pmTaxTran.RefNbr = doc.RefNbr;
            pmTaxTran.RevisionID = doc.RevisionID;
            pmTaxTran.TaxID = tax?.TaxID;
            pmTaxTran.CuryTaxAmt = new Decimal?(taxDetail.TaxAmount);
            pmTaxTran.CuryTaxableAmt = new Decimal?(taxDetail.TaxableAmount);
            pmTaxTran.TaxRate = new Decimal?(Convert.ToDecimal(taxDetail.Rate) * 100M);
            pmTaxTran.JurisType = taxDetail.JurisType;
            pmTaxTran.JurisName = taxDetail.JurisName;
            pmTaxTran.IsTaxInclusive = new bool?(taxDetail.TaxCalculationLevel == 1);
            ((PXSelectBase<PMTaxTran>) this.Base.Taxes).Insert(pmTaxTran);
          }
        }
      }
      finally
      {
        TaxBaseAttribute.SetTaxCalc<PMProformaProgressLine.taxCategoryID>(((PXSelectBase) this.Base.ProgressiveLines).Cache, (object) null, taxCalc1);
        TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.Base.TransactionLines).Cache, (object) null, taxCalc2);
      }
    }
    ((PXSelectBase<PMProforma>) this.Base.Document).Update(doc);
    this.SkipTaxCalcAndSave();
  }

  public virtual IAddressLocation GetFromAddress(PMProforma doc)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.Branch>> enumerator = ((PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) doc.BranchID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (IAddressLocation) PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Address>.op_Implicit((PXResult<PX.Objects.GL.Branch, BAccountR, PX.Objects.CR.Address>) enumerator.Current);
    }
    return (IAddressLocation) null;
  }

  public virtual IAddressLocation GetToAddress(PMProforma doc)
  {
    return (IAddressLocation) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>());
  }

  public virtual bool IsSame(GetTaxRequest x, GetTaxRequest y)
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
}
