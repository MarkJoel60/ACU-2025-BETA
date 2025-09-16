// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryExternalTax
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
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARInvoiceEntryExternalTax : ExternalTax<
#nullable disable
ARInvoiceEntry, ARInvoice>
{
  public FbqlSelect<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ARTran.sOOrderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOOrder.orderType>>>>>.And<BqlOperand<
  #nullable enable
  ARTran.sOOrderNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrder.orderNbr>>>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<BqlOperand<
  #nullable enable
  PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrder.shipVia>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ARTran.refNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ARInvoice.refNbr, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  ARTran.tranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARInvoice.docType, IBqlString>.FromCurrent>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CS.Carrier.carrierID, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.CS.Carrier.isCommonCarrier, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>, ARTran>.View WillcallOrders;
  public bool forceTaxCalcOnHold;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public virtual bool CalculateTaxesUsingExternalProvider(string taxZoneID)
  {
    bool? nullable;
    int num1;
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current != null)
    {
      nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ExternalTaxesImportInProgress;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    int num2;
    if (current == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = current.DisableAutomaticTaxCalculation;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num2 != 0;
    return ((!ExternalTaxBase<ARInvoiceEntry>.IsExternalTax((PXGraph) this.Base, taxZoneID) ? 0 : (!flag1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
  }

  public override ARInvoice CalculateExternalTax(ARInvoice invoice)
  {
    try
    {
      if (this.CalculateTaxesUsingExternalProvider(invoice.TaxZoneID) && !invoice.InstallmentNbr.HasValue && !invoice.IsRetainageDocument.GetValueOrDefault())
      {
        if (this.IsNonTaxable(!((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.WillcallOrders).Select(Array.Empty<object>())).Any<PXResult<ARTran>>() ? (IAddressBase) this.GetToAddress(invoice) : (IAddressBase) this.GetFromAddress(invoice)))
        {
          this.ApplyTax(invoice, GetTaxResult.Empty);
          invoice.IsTaxValid = new bool?(true);
          invoice.NonTaxable = new bool?(true);
          invoice.IsTaxSaved = new bool?(false);
          invoice = ((PXSelectBase<ARInvoice>) this.Base.Document).Update(invoice);
          this.SkipTaxCalcAndSave();
          return invoice;
        }
        if (invoice.NonTaxable.GetValueOrDefault())
          ((PXSelectBase<ARInvoice>) this.Base.Document).SetValueExt<ARInvoice.nonTaxable>(invoice, (object) false);
        ITaxProvider itaxProvider = ExternalTaxBase<ARInvoiceEntry>.TaxProviderFactory((PXGraph) this.Base, invoice.TaxZoneID);
        GetTaxRequest taxRequest = this.BuildGetTaxRequest(invoice);
        if (taxRequest.CartItems.Count == 0)
        {
          this.ApplyTax(invoice, GetTaxResult.Empty);
          invoice.IsTaxValid = new bool?(true);
          invoice.IsTaxSaved = new bool?(false);
          invoice = ((PXSelectBase<ARInvoice>) this.Base.Document).Update(invoice);
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
            invoice = ((PXSelectBase<ARInvoice>) this.Base.Document).Update(invoice);
            this.SkipTaxCalcAndSave();
          }
        }
        else
        {
          this.LogMessages((ResultBase) tax);
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
  public virtual void Persist(System.Action persist)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARInvoiceEntryExternalTax.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new ARInvoiceEntryExternalTax.\u003C\u003Ec__DisplayClass5_0();
    bool? nullable;
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current != null && this.CalculateTaxesUsingExternalProvider(((PXSelectBase<ARInvoice>) this.Base.Document).Current.TaxZoneID))
    {
      nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        if (!((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.WillcallOrders).Select(Array.Empty<object>())).Any<PXResult<ARTran>>())
          this.GetToAddress(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
        if (((PXSelectBase<ARInvoice>) this.Base.Document).Current.IsOriginalRetainageDocument())
        {
          ARSetup current = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current;
          int num;
          if (current == null)
          {
            num = 0;
          }
          else
          {
            nullable = current.RetainTaxes;
            num = nullable.GetValueOrDefault() ? 1 : 0;
          }
          if (num != 0)
            throw new PXException("Retained taxes calculated by an external tax provider are not supported.");
        }
      }
    }
    persist();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.currentDoc = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass50.currentDoc == null)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.fromSOModule = cDisplayClass50.currentDoc.OrigModule == "SO";
    int num1;
    if (!this.skipExternalTaxCalcOnSave)
    {
      // ISSUE: reference to a compiler-generated field
      nullable = cDisplayClass50.currentDoc.Released;
      if (!nullable.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass50.fromSOModule)
        {
          // ISSUE: reference to a compiler-generated field
          nullable = cDisplayClass50.currentDoc.Hold;
          if (nullable.GetValueOrDefault())
          {
            num1 = !this.forceTaxCalcOnHold ? 1 : 0;
            goto label_19;
          }
        }
        num1 = 0;
        goto label_19;
      }
    }
    num1 = 1;
label_19:
    bool flag = num1 != 0;
    // ISSUE: reference to a compiler-generated field
    if (!this.IsDocumentExtTaxValid(cDisplayClass50.currentDoc))
      return;
    // ISSUE: reference to a compiler-generated field
    nullable = cDisplayClass50.currentDoc.IsTaxValid;
    if (nullable.GetValueOrDefault() || flag)
      return;
    if (!PXLongOperation.IsLongOperationContext())
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass50, __methodptr(\u003CPersist\u003Eb__0)));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      this.Base.RecalculateExternalTax(cDisplayClass50.currentDoc);
    }
  }

  [PXOverride]
  public virtual ARInvoice RecalculateExternalTax(ARInvoice invoice)
  {
    if (invoice != null && this.CalculateTaxesUsingExternalProvider(invoice.TaxZoneID))
    {
      bool flag = invoice.OrigModule == "SO";
      if (((PXSelectBase<ARInvoice>) this.Base.Document).Current == null)
      {
        ((PXSelectBase<ARInvoice>) this.Base.Document).Current = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXViewOf<ARInvoice>.BasedOn<SelectFromBase<ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ARInvoice.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) invoice.DocType,
          (object) invoice.RefNbr
        }));
        if (flag)
        {
          ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ApplyPaymentWhenTaxAvailable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ApplyPaymentWhenTaxAvailable;
          ((PXSelectBase<PX.Objects.SO.SOInvoice>) ((SOInvoiceEntry) this.Base).SODocument).Current = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(PXSelectBase<PX.Objects.SO.SOInvoice, PXViewOf<PX.Objects.SO.SOInvoice>.BasedOn<SelectFromBase<PX.Objects.SO.SOInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOInvoice.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<PX.Objects.SO.SOInvoice.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) invoice.DocType,
            (object) invoice.RefNbr
          }));
        }
      }
      bool valueOrDefault = ((bool?) invoice?.IsTaxSaved).GetValueOrDefault();
      invoice = this.Base.CalculateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
      if (((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)).IsInsertedUpdatedDeleted)
        ((PXAction) this.Base.Save).Press();
      try
      {
        this.Base.RecalcUnbilledTax();
      }
      catch (Exception ex1)
      {
        invoice.IsTaxValid = new bool?(false);
        ((PXSelectBase<ARInvoice>) this.Base.Document).Update(invoice);
        this.SkipTaxCalcAndSave();
        try
        {
          if (!valueOrDefault)
          {
            if (invoice.IsTaxSaved.GetValueOrDefault())
            {
              if (PXTransactionScope.IsScoped)
                this.CancelTax(invoice, (VoidReasonCode) 2);
            }
          }
        }
        catch (Exception ex2)
        {
          object[] objArray = Array.Empty<object>();
          throw new PXException(ex2, "Failed to cancel the tax application in the external tax provider during the rollback. Check details in the Trace Log.", objArray);
        }
        throw ex1;
      }
    }
    return invoice;
  }

  [PXOverride]
  public virtual ARRegister OnBeforeRelease(ARRegister doc)
  {
    this.skipExternalTaxCalcOnSave = true;
    return doc;
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARInvoice> e)
  {
    if (e.Row == null)
      return;
    int num1 = this.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) ? 1 : 0;
    bool? nullable;
    int num2;
    if (num1 == 0)
    {
      nullable = e.Row.ProformaExists;
      if (!nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsRetainageReversing;
        if (!nullable.GetValueOrDefault())
        {
          nullable = e.Row.IsRetainageDocument;
          num2 = !nullable.GetValueOrDefault() ? 1 : 0;
          goto label_7;
        }
      }
    }
    num2 = 0;
label_7:
    bool flag = num2 != 0;
    ((PXSelectBase) this.Base.Taxes).Cache.AllowDelete = ((PXSelectBase) this.Base.Taxes).Cache.AllowDelete & flag;
    ((PXSelectBase) this.Base.Taxes).Cache.AllowInsert = ((PXSelectBase) this.Base.Taxes).Cache.AllowInsert & flag;
    ((PXSelectBase) this.Base.Taxes).Cache.AllowUpdate = ((PXSelectBase) this.Base.Taxes).Cache.AllowUpdate & flag;
    if (num1 == 0)
      return;
    nullable = e.Row.IsTaxValid;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<ARInvoice.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache, (object) e.Row, "Tax is not up-to-date.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARInvoice> e)
  {
    if (!this.IsDocumentExtTaxValid(e.Row) || e.Row.Released.GetValueOrDefault() || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARInvoice>>) e).Cache.ObjectsEqual<ARInvoice.externalTaxExemptionNumber, ARInvoice.avalaraCustomerUsageType, ARInvoice.curyDiscTot, ARInvoice.customerLocationID, ARInvoice.docDate, ARInvoice.taxZoneID, ARInvoice.branchID, ARInvoice.taxCalcMode>((object) e.Row, (object) e.OldRow))
      return;
    e.Row.IsTaxValid = new bool?(false);
  }

  public virtual bool IsDocumentExtTaxValid(ARInvoice doc)
  {
    return doc != null && this.CalculateTaxesUsingExternalProvider(doc.TaxZoneID) && !doc.InstallmentNbr.HasValue;
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARInvoice>) this.Base.Document).Current))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARInvoice>) this.Base.Document).Current) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARTran>>) e).Cache.ObjectsEqual<ARTran.avalaraCustomerUsageType, ARTran.accountID, ARTran.inventoryID, ARTran.tranDesc, ARTran.tranAmt, ARTran.tranDate, ARTran.taxCategoryID>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARTran> e)
  {
    if (!this.IsDocumentExtTaxValid(((PXSelectBase<ARInvoice>) this.Base.Document).Current))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARInvoice>) this.Base.Document).Current == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARShippingAddress>>) e).Cache.ObjectsEqual<ARShippingAddress.postalCode, ARShippingAddress.countryID, ARShippingAddress.state, ARShippingAddress.latitude, ARShippingAddress.longitude>((object) e.Row, (object) e.OldRow))
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARInvoice>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARShippingAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARInvoice>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<ARShippingAddress, ARShippingAddress.overrideAddress> e)
  {
    if (e.Row == null || ((PXSelectBase<ARInvoice>) this.Base.Document).Current == null)
      return;
    this.InvalidateExternalTax(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
  }

  private void InvalidateExternalTax(ARInvoice doc)
  {
    if (!this.CalculateTaxesUsingExternalProvider(doc.TaxZoneID))
      return;
    doc.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) doc);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARInvoice> e)
  {
    if (!e.Row.IsTaxSaved.GetValueOrDefault() || e.Row.Released.GetValueOrDefault())
      return;
    if (PXDBOperationExt.Command(e.Operation) == 3)
      this.CancelTax(e.Row, (VoidReasonCode) 2);
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && !((PXSelectBase<ARTran>) this.Base.Transactions).Any<ARTran>())
      this.CancelTax(e.Row, (VoidReasonCode) 2);
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    IAddressLocation address = !((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.WillcallOrders).Select(Array.Empty<object>())).Any<PXResult<ARTran>>() ? this.GetToAddress(e.Row) : this.GetFromAddress(e.Row);
    if ((ExternalTaxBase<ARInvoiceEntry>.IsExternalTax((PXGraph) this.Base, e.Row.TaxZoneID) || e.Row.ExternalTaxesImportInProgress.GetValueOrDefault()) && !this.IsNonTaxable((IAddressBase) address))
      return;
    this.CancelTax(e.Row, (VoidReasonCode) 2);
  }

  protected virtual GetTaxRequest BuildGetTaxRequest(ARInvoice invoice)
  {
    return (GetTaxRequest) this.BuildCommitTaxRequest(invoice);
  }

  public virtual CommitTaxRequest BuildCommitTaxRequest(ARInvoice invoice)
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
    CommitTaxRequest commitTaxRequest1 = new CommitTaxRequest();
    ((GetTaxRequest) commitTaxRequest1).CompanyCode = this.CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
    ((GetTaxRequest) commitTaxRequest1).CurrencyCode = invoice.CuryID;
    ((GetTaxRequest) commitTaxRequest1).CustomerCode = customer.AcctCD;
    ((GetTaxRequest) commitTaxRequest1).BAccountClassID = customer.ClassID;
    ((GetTaxRequest) commitTaxRequest1).TaxRegistrationID = location?.TaxRegistrationID;
    ((GetTaxRequest) commitTaxRequest1).APTaxType = taxZone.ExternalAPTaxType;
    IAddressLocation fromAddress = this.GetFromAddress(invoice);
    IAddressLocation address = !((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) this.WillcallOrders).Select(Array.Empty<object>())).Any<PXResult<ARTran>>() ? this.GetToAddress(invoice) : fromAddress;
    if (fromAddress == null)
      throw new PXException("The system failed to get the From address for the document.");
    if (address == null)
      throw new PXException("The system failed to get the To address for the document.");
    ((GetTaxRequest) commitTaxRequest1).OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
    ((GetTaxRequest) commitTaxRequest1).DestinationAddress = AddressConverter.ConvertTaxAddress(address);
    ((GetTaxRequest) commitTaxRequest1).DocCode = $"AR.{invoice.DocType}.{invoice.RefNbr}";
    ((GetTaxRequest) commitTaxRequest1).DocDate = invoice.DocDate.GetValueOrDefault();
    ((GetTaxRequest) commitTaxRequest1).LocationCode = this.GetExternalTaxProviderLocationCode(invoice);
    ((GetTaxRequest) commitTaxRequest1).CustomerUsageType = invoice.AvalaraCustomerUsageType;
    CommitTaxRequest commitTaxRequest2 = commitTaxRequest1;
    bool? nullable1 = invoice.IsTaxSaved;
    int num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((GetTaxRequest) commitTaxRequest2).IsTaxSaved = num1 != 0;
    ((GetTaxRequest) commitTaxRequest1).TaxCalculationMode = !PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() || !(invoice.TaxCalcMode != "T") ? this.GetTaxProviderTaxCalcMode(invoice.TaxZoneID) : invoice.TaxCalcMode;
    if (!string.IsNullOrEmpty(invoice.ExternalTaxExemptionNumber))
      ((GetTaxRequest) commitTaxRequest1).ExemptionNo = invoice.ExternalTaxExemptionNumber;
    ((GetTaxRequest) commitTaxRequest1).DocType = this.GetTaxDocumentType(invoice);
    Sign documentSign = this.GetDocumentSign(invoice);
    PXSelectJoin<ARTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>>, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, Or<ARTran.lineType, IsNull>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> pxSelectJoin = new PXSelectJoin<ARTran, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARTran.inventoryID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>>, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, Or<ARTran.lineType, IsNull>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>>((PXGraph) this.Base);
    ((GetTaxRequest) commitTaxRequest1).Discount = Sign.op_Multiply(documentSign, this.GetDocDiscount().GetValueOrDefault());
    DateTime? nullable2 = invoice.OrigDocDate;
    ARSetup current = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current;
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
    PXView view = ((PXSelectBase) pxSelectJoin).View;
    object[] objArray1 = new object[1]{ (object) invoice };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      ARTran tran = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PXResult<ARTran, PX.Objects.IN.InventoryItem, PX.Objects.GL.Account>.op_Implicit(pxResult);
      Decimal? nullable3;
      if (tran.LineType == "FR")
      {
        nullable3 = tran.CuryTranAmt;
        Decimal num3 = 0M;
        if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
          continue;
      }
      int num4;
      if (tran.LineType != "FR" && ((GetTaxRequest) commitTaxRequest1).Discount != 0M)
      {
        nullable3 = tran.OrigDocumentDiscountRate;
        if (!((nullable3 ?? 1M) != 1M))
        {
          nullable3 = tran.OrigGroupDiscountRate;
          if (!((nullable3 ?? 1M) != 1M))
          {
            nullable3 = tran.DocumentDiscountRate;
            if (!((nullable3 ?? 1M) != 1M))
            {
              nullable3 = tran.GroupDiscountRate;
              num4 = (nullable3 ?? 1M) != 1M ? 1 : 0;
              goto label_22;
            }
          }
        }
        num4 = 1;
      }
      else
        num4 = 0;
label_22:
      bool flag2 = num4 != 0;
      TaxCartItem taxCartItem1 = new TaxCartItem();
      taxCartItem1.Index = tran.LineNbr.GetValueOrDefault();
      TaxCartItem taxCartItem2 = taxCartItem1;
      Sign sign1 = documentSign;
      nullable3 = tran.CuryUnitPrice;
      Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
      Decimal num5 = Sign.op_Multiply(sign1, valueOrDefault1);
      taxCartItem2.UnitPrice = num5;
      TaxCartItem taxCartItem3 = taxCartItem1;
      Sign sign2 = documentSign;
      nullable3 = tran.CuryTranAmt;
      Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
      Decimal num6;
      if (!flag1)
      {
        num6 = 0M;
      }
      else
      {
        nullable3 = tran.CuryRetainageAmt;
        num6 = nullable3.GetValueOrDefault();
      }
      Decimal num7 = valueOrDefault2 + num6;
      Decimal num8 = Sign.op_Multiply(sign2, num7);
      taxCartItem3.Amount = num8;
      taxCartItem1.Description = tran.TranDesc;
      taxCartItem1.DestinationAddress = AddressConverter.ConvertTaxAddress(this.GetToAddress(invoice, tran));
      taxCartItem1.OriginAddress = AddressConverter.ConvertTaxAddress(this.GetFromAddress(invoice, tran));
      taxCartItem1.ItemCode = tran.LineType == "FR" ? "N/A" : inventoryItem.InventoryCD;
      TaxCartItem taxCartItem4 = taxCartItem1;
      Decimal num9;
      if (!(tran.LineType == "FR"))
      {
        nullable3 = tran.Qty;
        num9 = Math.Abs(nullable3.GetValueOrDefault());
      }
      else
        num9 = 1M;
      taxCartItem4.Quantity = num9;
      taxCartItem1.UOM = tran.UOM;
      taxCartItem1.Discounted = new bool?(flag2);
      taxCartItem1.RevAcct = account.AccountCD;
      taxCartItem1.TaxCode = tran.TaxCategoryID;
      taxCartItem1.CustomerUsageType = tran.AvalaraCustomerUsageType;
      if (!string.IsNullOrEmpty(inventoryItem.HSTariffCode))
        taxCartItem1.CommodityCode = new CommodityCode(inventoryItem.CommodityCodeType, inventoryItem.HSTariffCode);
      if (tran.OrigInvoiceDate.HasValue)
        nullable2 = tran.OrigInvoiceDate;
      if (invoice.DocType == "CRM" && invoice.OrigRefNbr != null)
      {
        nullable1 = invoice.IsCancellation;
        if (nullable1.GetValueOrDefault())
        {
          ARTax[] array = GraphHelper.RowCast<ARTax>((IEnumerable) PXSelectBase<ARTax, PXViewOf<ARTax>.BasedOn<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.tranType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARTax.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ARTax.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[3]
          {
            (object) tran?.OrigInvoiceType,
            (object) tran?.OrigInvoiceNbr,
            (object) (int?) tran?.OrigInvoiceLineNbr
          })).ToArray<ARTax>();
          if (((IEnumerable<ARTax>) array).Any<ARTax>())
          {
            taxCartItem1.TaxOverride.Reason = "Return";
            taxCartItem1.TaxOverride.TaxDate = new DateTime?(nullable2.Value);
            taxCartItem1.TaxOverride.TaxOverrideType = new TaxOverrideType?((TaxOverrideType) 1);
            TaxOverride taxOverride = taxCartItem1.TaxOverride;
            Sign minus = Sign.Minus;
            nullable3 = ((IEnumerable<ARTax>) array).Sum<ARTax>((Func<ARTax, Decimal?>) (x => x?.CuryTaxAmt));
            Decimal num10 = nullable3.Value;
            Decimal? nullable4 = new Decimal?(Sign.op_Multiply(minus, num10));
            taxOverride.TaxAmount = nullable4;
          }
        }
      }
      ((GetTaxRequest) commitTaxRequest1).CartItems.Add(taxCartItem1);
    }
    if ((invoice.DocType == "CRM" || invoice.DocType == "RCS") && invoice.OrigDocDate.HasValue)
    {
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.Reason = "Return";
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.TaxDate = new DateTime?(nullable2.Value);
      ((GetTaxRequest) commitTaxRequest1).TaxOverride.TaxOverrideType = new TaxOverrideType?((TaxOverrideType) 3);
      Sign minus = Sign.Minus;
    }
    return commitTaxRequest1;
  }

  public virtual TaxDocumentType GetTaxDocumentType(ARInvoice invoice)
  {
    switch (invoice.DrCr)
    {
      case "C":
        return (TaxDocumentType) 2;
      case "D":
        return (TaxDocumentType) 6;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  public virtual Sign GetDocumentSign(ARInvoice invoice)
  {
    switch (invoice.DrCr)
    {
      case "C":
        return Sign.Plus;
      case "D":
        return Sign.Minus;
      default:
        throw new PXException("The document type is not supported or implemented.");
    }
  }

  public virtual void ApplyTax(ARInvoice invoice, GetTaxResult result)
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
    Sign documentSign = this.GetDocumentSign(invoice);
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
          arTaxTran1.CuryID = invoice.CuryID;
          arTaxTran1.CuryTaxAmt = new Decimal?(Sign.op_Multiply(documentSign, result.TaxSummary[index].TaxAmount));
          arTaxTran1.CuryTaxableAmt = new Decimal?(Sign.op_Multiply(documentSign, result.TaxSummary[index].TaxableAmount));
          arTaxTran1.CuryTaxAmtSumm = new Decimal?(Sign.op_Multiply(documentSign, result.TaxSummary[index].TaxAmount));
          arTaxTran1.TaxRate = new Decimal?(Convert.ToDecimal(result.TaxSummary[index].Rate) * 100M);
          arTaxTran1.JurisType = result.TaxSummary[index].JurisType;
          arTaxTran1.JurisName = result.TaxSummary[index].JurisName;
          arTaxTran1.TaxType = result.TaxSummary[index].TaxType;
          arTaxTran1.TaxBucketID = new int?(0);
          int? nullable = (int?) tax?.SalesTaxAcctID;
          arTaxTran1.AccountID = nullable ?? vendor.SalesTaxAcctID;
          nullable = (int?) tax?.SalesTaxSubID;
          arTaxTran1.SubID = nullable ?? vendor.SalesTaxSubID;
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
    bool valueOrDefault1 = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal.GetValueOrDefault();
    if (!invoice.Hold.GetValueOrDefault())
      ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal = new bool?(false);
    try
    {
      ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoice.isTaxSaved>((object) invoice, (object) true);
    }
    finally
    {
      ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal = new bool?(valueOrDefault1);
    }
    PXSelectJoin<ARAdjust2, InnerJoin<ARPayment, On<ARAdjust2.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARPayment.refNbr>>>>, Where<ARAdjust2.adjdDocType, Equal<Required<ARInvoice.docType>>, And<ARAdjust2.adjdRefNbr, Equal<Required<ARInvoice.refNbr>>>>, OrderBy<Desc<ARAdjust2.recalculatable>>> pxSelectJoin = new PXSelectJoin<ARAdjust2, InnerJoin<ARPayment, On<ARAdjust2.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARPayment.refNbr>>>>, Where<ARAdjust2.adjdDocType, Equal<Required<ARInvoice.docType>>, And<ARAdjust2.adjdRefNbr, Equal<Required<ARInvoice.refNbr>>>>, OrderBy<Desc<ARAdjust2.recalculatable>>>((PXGraph) this.Base);
    Decimal num1 = 0M;
    object[] objArray5 = new object[2]
    {
      (object) invoice.DocType,
      (object) invoice.RefNbr
    };
    foreach (PXResult<ARAdjust2, ARPayment> pxResult in ((PXSelectBase<ARAdjust2>) pxSelectJoin).Select(objArray5))
    {
      ARAdjust2 arAdjust2_1 = PXResult<ARAdjust2, ARPayment>.op_Implicit(pxResult);
      PXResult<ARAdjust2, ARPayment>.op_Implicit(pxResult);
      ARAdjust2 copy = PXCache<ARAdjust2>.CreateCopy(arAdjust2_1);
      Decimal? nullable1;
      if (copy.Recalculatable.GetValueOrDefault())
      {
        ARAdjust2 arAdjust2_2 = copy;
        nullable1 = copy.CuryAdjdOrigAmt;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        nullable1 = copy.CuryAdjdAmt;
        Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = valueOrDefault2 > valueOrDefault3 ? copy.CuryAdjdOrigAmt : copy.CuryAdjdAmt;
        arAdjust2_2.CuryAdjdAmt = nullable2;
        copy.Recalculatable = new bool?(false);
      }
      Decimal num2 = num1;
      nullable1 = copy.CuryAdjdAmt;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      num1 = num2 + valueOrDefault4;
      Decimal num3 = num1;
      nullable1 = invoice.CuryDocBal;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      if (num3 > valueOrDefault5)
      {
        nullable1 = copy.CuryAdjdAmt;
        Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
        Decimal num4 = num1;
        nullable1 = invoice.CuryDocBal;
        Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
        Decimal num5 = num4 - valueOrDefault7;
        Decimal num6 = valueOrDefault6 - num5;
        copy.CuryAdjdAmt = new Decimal?(num6 > 0M ? num6 : 0M);
      }
      ((PXSelectBase<ARAdjust2>) this.Base.Adjustments).Update(copy);
    }
  }

  protected virtual void UpdateTransactionTaxableAmount(ARInvoice invoice, bool hasInclTax)
  {
    foreach (ARTran arTran1 in ((PXSelectBase) this.Base.AllTransactions).View.SelectMultiBound(new object[1]
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
      ((PXSelectBase<ARTran>) this.Base.AllTransactions).Update(arTran1);
    }
  }

  public virtual void CancelTax(ARInvoice invoice, VoidReasonCode code)
  {
    string taxZoneID = ((ARInvoice) PrimaryKeyOf<ARInvoice>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<ARInvoice.docType, ARInvoice.refNbr>>.Find((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<ARInvoice.docType, ARInvoice.refNbr>) invoice, (PKFindOptions) 0))?.TaxZoneID ?? invoice.TaxZoneID;
    VoidTaxRequest voidTaxRequest = new VoidTaxRequest();
    voidTaxRequest.CompanyCode = this.CompanyCodeFromBranch(taxZoneID, invoice.BranchID);
    voidTaxRequest.Code = code;
    voidTaxRequest.DocCode = $"AR.{invoice.DocType}.{invoice.RefNbr}";
    voidTaxRequest.DocType = this.GetTaxDocumentType(invoice);
    ITaxProvider itaxProvider = ExternalTaxBase<PXGraph>.TaxProviderFactory((PXGraph) this.Base, taxZoneID);
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

  public virtual void VoidScheduledDocument(ARInvoice invoice)
  {
    int num = invoice.IsTaxValid.GetValueOrDefault() ? 1 : 0;
    this.CancelTax(invoice, (VoidReasonCode) 2);
    if (num == 0)
      return;
    invoice.IsTaxValid = new bool?(true);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) invoice);
  }

  protected override string GetExternalTaxProviderLocationCode(ARInvoice invoice)
  {
    return this.GetExternalTaxProviderLocationCode<ARTran, KeysRelation<CompositeKey<Field<ARTran.tranType>.IsRelatedTo<ARInvoice.docType>, Field<ARTran.refNbr>.IsRelatedTo<ARInvoice.refNbr>>.WithTablesOf<ARInvoice, ARTran>, ARInvoice, ARTran>.SameAsCurrent, ARTran.siteID>(invoice);
  }

  public virtual IAddressLocation GetFromAddress(ARInvoice invoice)
  {
    return GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) invoice.BranchID
    })).FirstOrDefault<PX.Objects.CR.Address>().With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<BAccountR.defAddressID>));
  }

  public virtual IAddressLocation GetFromAddress(ARInvoice invoice, ARTran tran)
  {
    if (!(invoice.OrigModule == "SO"))
      return this.GetFromAddress(invoice);
    PXResult<PX.Objects.CR.Address, PX.Objects.IN.INSite, PX.Objects.SO.SOLine> pxResult = ((IEnumerable<PXResult<PX.Objects.CR.Address>>) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<ARTran.sOOrderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<ARTran.sOOrderNbr>>, And<PX.Objects.SO.SOLine.lineNbr, Equal<Current<ARTran.sOOrderLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.CR.Address>>().Cast<PXResult<PX.Objects.CR.Address, PX.Objects.IN.INSite, PX.Objects.SO.SOLine>>().FirstOrDefault<PXResult<PX.Objects.CR.Address, PX.Objects.IN.INSite, PX.Objects.SO.SOLine>>();
    PX.Objects.CR.Address o = GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<ARTran.siteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>())).FirstOrDefault<PX.Objects.CR.Address>();
    IAddressLocation shipOriginAddress = this.GetDropShipOriginAddress(tran, PXResult<PX.Objects.CR.Address, PX.Objects.IN.INSite, PX.Objects.SO.SOLine>.op_Implicit(pxResult), invoice);
    if (shipOriginAddress != null)
      return shipOriginAddress;
    IAddressLocation fromAddress = GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.CR.Address>>) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<SOShipLine, On<SOShipLine.siteID, Equal<PX.Objects.IN.INSite.siteID>, And<SOShipLine.shipmentType, Equal<Current<ARTran.sOShipmentType>>>>>>, Where<SOShipLine.shipmentNbr, Equal<Current<ARTran.sOShipmentNbr>>, And<SOShipLine.lineNbr, Equal<Current<ARTran.sOShipmentLineNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.CR.Address>>()).FirstOrDefault<PX.Objects.CR.Address>().With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<PX.Objects.IN.INSite.addressID>));
    if (fromAddress != null)
      return fromAddress;
    IAddressLocation iaddressLocation;
    if (!(tran.LineType == "FR"))
      iaddressLocation = (IAddressLocation) null;
    else
      iaddressLocation = GraphHelper.RowCast<PX.Objects.CR.Address>((IEnumerable) PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.SO.SOShipment, On<PX.Objects.SO.SOShipment.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>, Where<PX.Objects.SO.SOShipment.shipmentType, Equal<Current<ARTran.sOShipmentType>>, And<PX.Objects.SO.SOShipment.shipmentNbr, Equal<Current<ARTran.sOShipmentNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
      {
        tran
      }, Array.Empty<object>())).FirstOrDefault<PX.Objects.CR.Address>().With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<PX.Objects.IN.INSite.addressID>));
    return iaddressLocation ?? PXResult<PX.Objects.CR.Address, PX.Objects.IN.INSite, PX.Objects.SO.SOLine>.op_Implicit(pxResult).With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<PX.Objects.IN.INSite.addressID>)) ?? o.With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<PX.Objects.IN.INSite.addressID>)) ?? this.GetFromAddress(invoice);
  }

  public virtual IAddressLocation GetToAddress(ARInvoice invoice)
  {
    return ((ARShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).View.SelectSingleBound(new object[1]
    {
      (object) invoice
    }, Array.Empty<object>())).With<ARShippingAddress, IAddressLocation>(new Func<ARShippingAddress, IAddressLocation>(this.ValidAddressFrom<ARInvoice.shipAddressID>));
  }

  public virtual IAddressLocation GetToAddress(ARInvoice invoice, ARTran tran)
  {
    if (!(invoice.OrigModule == "SO"))
      return this.GetToAddress(invoice);
    PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier> pxResult1 = ((IEnumerable<PXResult<SOAddress>>) PXSelectBase<SOAddress, PXSelectJoin<SOAddress, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.shipAddressID, Equal<SOAddress.addressID>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>>>, LeftJoin<PX.Objects.CS.Carrier, On<PX.Objects.CS.Carrier.carrierID, Equal<PX.Objects.SO.SOOrder.shipVia>>>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<ARTran.sOOrderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<ARTran.sOOrderNbr>>, And<Where<Current2<ARTran.sOOrderLineNbr>, IsNull, Or<Current2<ARTran.sOOrderLineNbr>, Equal<PX.Objects.SO.SOLine.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOAddress>>().Cast<PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier>>().FirstOrDefault<PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier>>();
    IAddressLocation destinationAddress = this.GetDropShipDestinationAddress(tran, PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier>.op_Implicit(pxResult1));
    if (destinationAddress != null)
      return destinationAddress;
    PXResult<SOAddress, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.CS.Carrier> pxResult2 = ((IEnumerable<PXResult<SOAddress>>) PXSelectBase<SOAddress, PXSelectJoin<SOAddress, InnerJoin<PX.Objects.SO.SOShipment, On<PX.Objects.SO.SOShipment.shipAddressID, Equal<SOAddress.addressID>>, LeftJoin<SOShipLine, On<SOShipLine.shipmentType, Equal<PX.Objects.SO.SOShipment.shipmentType>, And<SOShipLine.shipmentNbr, Equal<PX.Objects.SO.SOShipment.shipmentNbr>>>, LeftJoin<PX.Objects.CS.Carrier, On<PX.Objects.CS.Carrier.carrierID, Equal<PX.Objects.SO.SOShipment.shipVia>>>>>, Where<PX.Objects.SO.SOShipment.shipmentType, Equal<Current<ARTran.sOShipmentType>>, And<PX.Objects.SO.SOShipment.shipmentNbr, Equal<Current<ARTran.sOShipmentNbr>>, And<Where<Current2<ARTran.sOShipmentLineNbr>, IsNull, Or<Current2<ARTran.sOShipmentLineNbr>, Equal<SOShipLine.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOAddress>>().Cast<PXResult<SOAddress, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.CS.Carrier>>().FirstOrDefault<PXResult<SOAddress, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.CS.Carrier>>();
    int num1 = pxResult1 != null ? 0 : (pxResult2 == null ? 1 : 0);
    int num2;
    if (pxResult2 != null)
    {
      PX.Objects.CS.Carrier carrier = PXResult<SOAddress, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.CS.Carrier>.op_Implicit(pxResult2);
      num2 = carrier != null ? (carrier.IsCommonCarrier.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
    {
      PX.Objects.CS.Carrier carrier = PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier>.op_Implicit(pxResult1);
      num2 = carrier != null ? (carrier.IsCommonCarrier.GetValueOrDefault() ? 1 : 0) : 0;
    }
    bool flag = num2 != 0;
    return (num1 != 0 ? 0 : (!flag ? 1 : 0)) != 0 ? this.GetFromAddress(invoice, tran) : (tran.LineType == "FR" || tran.SOShipmentLineNbr.HasValue ? PXResult<SOAddress, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.CS.Carrier>.op_Implicit(pxResult2) : (SOAddress) null).With<SOAddress, IAddressLocation>(new Func<SOAddress, IAddressLocation>(this.ValidAddressFrom<PX.Objects.SO.SOShipment.shipAddressID>)) ?? (tran.LineType == "FR" || tran.SOOrderLineNbr.HasValue ? PXResult<SOAddress, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, PX.Objects.CS.Carrier>.op_Implicit(pxResult1) : (SOAddress) null).With<SOAddress, IAddressLocation>(new Func<SOAddress, IAddressLocation>(this.ValidAddressFrom<PX.Objects.SO.SOOrder.shipAddressID>)) ?? this.GetToAddress(invoice);
  }

  protected virtual IAddressLocation GetDropShipOriginAddress(
    ARTran tran,
    PX.Objects.SO.SOLine soLine,
    ARInvoice invoice)
  {
    if (soLine == null || !tran.SOOrderLineNbr.HasValue || !soLine.POCreate.GetValueOrDefault() || !(soLine.POSource == "D"))
      return (IAddressLocation) null;
    PX.Objects.CR.Address o = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelectJoin<PX.Objects.CR.Address, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.addressID, Equal<PX.Objects.CR.Address.addressID>>, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceipt.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>, And<PX.Objects.PO.POReceiptLine.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) (tran.SOOrderLineOperation == "R" ? "RN" : "RT"),
      (object) tran.SOShipmentNbr,
      (object) tran.SOShipmentLineNbr
    }));
    return (o != null ? o.With<PX.Objects.CR.Address, IAddressLocation>(new Func<PX.Objects.CR.Address, IAddressLocation>(this.ValidAddressFrom<PX.Objects.PO.POReceiptLine.siteID>)) : (IAddressLocation) null) ?? this.GetFromAddress(invoice);
  }

  protected virtual IAddressLocation GetDropShipDestinationAddress(ARTran tran, PX.Objects.SO.SOLine soLine)
  {
    if (soLine != null && tran.SOOrderLineNbr.HasValue && soLine.POCreate.GetValueOrDefault() && soLine.POSource == "D")
    {
      string str = tran.SOOrderLineOperation == "R" ? "RN" : "RT";
      string soShipmentNbr = tran.SOShipmentNbr;
      POAddress o = PXResultset<POAddress>.op_Implicit(PXSelectBase<POAddress, PXSelectJoin<POAddress, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.shipAddressID, Equal<POAddress.addressID>>, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.pOType, Equal<PX.Objects.PO.POOrder.orderType>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) str,
        (object) tran.SOShipmentNbr,
        (object) tran.SOShipmentLineNbr
      }));
      if (o != null)
        return o.With<POAddress, IAddressLocation>(new Func<POAddress, IAddressLocation>(this.ValidAddressFrom<PX.Objects.PO.POOrder.shipAddressID>));
    }
    return (IAddressLocation) null;
  }

  private IAddressLocation ValidAddressFrom<TFieldSource>(IAddressLocation address) where TFieldSource : IBqlField
  {
    return !ExternalTaxBase<ARInvoiceEntry>.IsEmptyAddress(address) ? address : throw new PXException(this.PickAddressError<TFieldSource>((IAddressBase) address));
  }

  private string PickAddressError<TFieldSource>(IAddressBase address) where TFieldSource : IBqlField
  {
    if (typeof (TFieldSource) == typeof (PX.Objects.PO.POOrder.shipAddressID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.PO.POOrder>>) PXSelectBase<PX.Objects.PO.POOrder, PXSelectReadonly<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.shipAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((POAddress) address).AddressID
      })).First<PXResult<PX.Objects.PO.POOrder>>()).GetItem<PX.Objects.PO.POOrder>().With<PX.Objects.PO.POOrder, string>((Func<PX.Objects.PO.POOrder, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.PO.POOrder>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.PO.POOrder>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.PO.POReceipt.vendorLocationID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.PO.POReceipt>>) PXSelectBase<PX.Objects.PO.POReceipt, PXSelectReadonly2<PX.Objects.PO.POReceipt, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.PO.POReceipt.vendorLocationID>>>, Where<PX.Objects.CR.Standalone.Location.defAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<PX.Objects.PO.POReceipt>>()).GetItem<PX.Objects.PO.POReceipt>().With<PX.Objects.PO.POReceipt, string>((Func<PX.Objects.PO.POReceipt, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.PO.POReceipt>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.PO.POReceipt>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.SO.SOShipment.shipAddressID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.SO.SOShipment>>) PXSelectBase<PX.Objects.SO.SOShipment, PXSelectReadonly<PX.Objects.SO.SOShipment, Where<PX.Objects.SO.SOShipment.shipAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((SOAddress) address).AddressID
      })).First<PXResult<PX.Objects.SO.SOShipment>>()).GetItem<PX.Objects.SO.SOShipment>().With<PX.Objects.SO.SOShipment, string>((Func<PX.Objects.SO.SOShipment, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.SO.SOShipment>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.SO.SOShipment>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.SO.SOOrder.shipAddressID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) PXSelectBase<PX.Objects.SO.SOOrder, PXSelectReadonly<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.shipAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((SOAddress) address).AddressID
      })).First<PXResult<PX.Objects.SO.SOOrder>>()).GetItem<PX.Objects.SO.SOOrder>().With<PX.Objects.SO.SOOrder, string>((Func<PX.Objects.SO.SOOrder, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.SO.SOOrder>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.SO.SOOrder>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (ARInvoice.shipAddressID))
      return ((PXResult) ((IQueryable<PXResult<ARInvoice>>) PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.shipAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((ARAddress) address).AddressID
      })).First<PXResult<ARInvoice>>()).GetItem<ARInvoice>().With<ARInvoice, string>((Func<ARInvoice, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<ARInvoice>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<ARInvoice>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (PX.Objects.IN.INSite.addressID))
      return ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.INSite>>) PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<PX.Objects.IN.INSite>>()).GetItem<PX.Objects.IN.INSite>().With<PX.Objects.IN.INSite, string>((Func<PX.Objects.IN.INSite, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<PX.Objects.IN.INSite>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.IN.INSite>(e, ", ")
      })));
    if (typeof (TFieldSource) == typeof (BAccountR.defAddressID))
      return ((PXResult) ((IQueryable<PXResult<BAccountR>>) PXSelectBase<BAccountR, PXSelectReadonly<BAccountR, Where<BAccountR.defAddressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) ((PX.Objects.CR.Address) address).AddressID
      })).First<PXResult<BAccountR>>()).GetItem<BAccountR>().With<BAccountR, string>((Func<BAccountR, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
      {
        (object) EntityHelper.GetFriendlyEntityName<BAccountR>(),
        (object) new EntityHelper((PXGraph) this.Base).GetRowID<BAccountR>(e, ", ")
      })));
    if (!(typeof (TFieldSource) == typeof (PX.Objects.PO.POReceiptLine.siteID)))
      throw new ArgumentOutOfRangeException("Unknown address source used");
    return ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.INSite>>) PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.addressID, Equal<Required<PX.Objects.PO.POReceiptLine.siteID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) ((PX.Objects.CR.Address) address).AddressID
    })).First<PXResult<PX.Objects.IN.INSite>>()).GetItem<PX.Objects.IN.INSite>().With<PX.Objects.IN.INSite, string>((Func<PX.Objects.IN.INSite, string>) (e => PXMessages.LocalizeFormat("Taxes cannot be calculated via the external tax provider because the address is missing for {0} {1}.", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName<PX.Objects.IN.INSite>(),
      (object) new EntityHelper((PXGraph) this.Base).GetRowID<PX.Objects.IN.INSite>(e, ", ")
    })));
  }

  private void InsertTaxDetails(
    ARInvoice invoice,
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
          PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARInvoice.docType>>, And<ARTax.refNbr, Equal<Current<ARInvoice.refNbr>>>>, OrderBy<Asc<ARTax.tranType, Asc<ARTax.refNbr, Asc<ARTax.taxID>>>>> taxRows = this.Base.Tax_Rows;
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

  protected override Decimal? GetDocDiscount()
  {
    return ((PXSelectBase<ARInvoice>) this.Base.Document).Current.CuryDiscTot;
  }
}
