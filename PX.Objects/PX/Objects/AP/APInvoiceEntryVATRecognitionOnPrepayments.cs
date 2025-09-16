// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP;

public class APInvoiceEntryVATRecognitionOnPrepayments : 
  PXGraphExtension<
  #nullable disable
  APInvoiceEntry.MultiCurrency, APInvoiceEntry>
{
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.AP.Standalone2.APPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  PX.Objects.AP.Standalone2.APPayment.docType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  APRegister.docType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.AP.Standalone2.APPayment.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APRegister.refNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.AP.Standalone2.APPayment>.View APPayment_DocType_RefNbr;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.Base.TaxesList.WhereAnd<Where<APTaxTran.tranType, NotEqual<APDocType.prepaymentInvoice>, Or<TaxTran.taxInvoiceNbr, PX.Data.IsNull>>>();
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APRegister.docType>, NotEqual<APDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APRegister.docType>, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<PX.Objects.CS.Terms.installmentType, IBqlString>.IsEqual<TermsInstallmentType.single>>>>), "A prepayment invoice cannot have terms with the Multiple installment type.", new System.Type[] {})]
  [PXRestrictor(typeof (PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APRegister.docType>, NotEqual<APDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APRegister.docType>, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<PX.Objects.CS.Terms.discPercent, IBqlDecimal>.IsEqual<decimal0>>>>), "A prepayment invoice cannot have terms with a cash discount.", new System.Type[] {})]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXFormula(typeof (APInvoiceEntryVATRecognitionOnPrepayments.TermsByVendor<APInvoice.vendorID, APInvoice.termsID, APInvoice.docType>))]
  public void _(PX.Data.Events.CacheAttached<APInvoice.termsID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIRequired(typeof (Where<APInvoice.docType, Equal<APDocType.prepaymentInvoice>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APInvoice.prepaymentAccountID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIRequired(typeof (Where<APInvoice.docType, Equal<APDocType.prepaymentInvoice>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APInvoice.prepaymentSubID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (CalculatePrepaymentPercent<APTran.prepaymentPct, APTran.curyPrepaymentAmt, APTran.curyLineAmt, APTran.curyDiscAmt>))]
  public void _(PX.Data.Events.CacheAttached<APTran.prepaymentPct> e)
  {
  }

  public void _(PX.Data.Events.FieldDefaulting<APTaxTran.accountID> e)
  {
    APTaxTran row = (APTaxTran) e.Row;
    if (row == null || !(row.TranType == "PPI"))
      return;
    PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) PXSelectBase<PX.Objects.TX.Tax, PXViewOf<PX.Objects.TX.Tax>.BasedOn<SelectFromBase<PX.Objects.TX.Tax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, (object) row.TaxID);
    e.NewValue = (object) (int?) tax?.OnAPPrepaymentTaxAcctID;
  }

  public void _(PX.Data.Events.FieldDefaulting<APTaxTran.subID> e)
  {
    APTaxTran row = (APTaxTran) e.Row;
    if (row == null || !(row.TranType == "PPI"))
      return;
    PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) PXSelectBase<PX.Objects.TX.Tax, PXViewOf<PX.Objects.TX.Tax>.BasedOn<SelectFromBase<PX.Objects.TX.Tax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, (object) row.TaxID);
    e.NewValue = (object) (int?) tax?.OnAPPrepaymentTaxSubID;
  }

  public void _(PX.Data.Events.RowSelected<APTaxTran> e)
  {
    APTaxTran row = e.Row;
    if (row == null)
      return;
    PX.Objects.TX.Tax tax = PXSelectorAttribute.Select<PX.Objects.TX.Tax.taxID>(e.Cache, (object) row) as PX.Objects.TX.Tax;
    if (!(this.Base.Document.Current?.DocType == "PPI") || tax == null)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (tax.TaxType == "S" || tax.TaxType == "P" || tax.TaxType == "W")
      propertyException = new PXSetPropertyException((IBqlTable) row, "{0} taxes are not supported for prepayment invoices.", PXErrorLevel.RowError, new object[1]
      {
        (object) CSTaxType.GetTaxTypeLabel(tax.TaxType)
      });
    else if (tax.DirectTax.GetValueOrDefault())
      propertyException = new PXSetPropertyException((IBqlTable) row, "Direct-entry taxes are not supported for prepayment invoices.", PXErrorLevel.RowError);
    if (propertyException == null)
      return;
    e.Cache.RaiseExceptionHandling<APTaxTran.taxID>((object) e.Row, (object) row.TaxID, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APInvoice, APInvoice.prepaymentAccountID> e)
  {
    APInvoice row = e.Row;
    if (row == null || !(row.DocType == "PPI") || this.Base.vendor.Current == null)
      return;
    e.NewValue = this.Base.GetAcctSub<Vendor.prepaymentAcctID>(this.Base.vendor.Cache, (object) this.Base.vendor.Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APInvoice, APInvoice.prepaymentSubID> e)
  {
    APInvoice row = e.Row;
    if (row == null || !(row.DocType == "PPI") || this.Base.vendor.Current == null)
      return;
    e.NewValue = this.Base.GetAcctSub<Vendor.prepaymentSubID>(this.Base.vendor.Cache, (object) this.Base.vendor.Current);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<APInvoice, APInvoice.vendorLocationID> e)
  {
    e.Cache.SetDefaultExt<APInvoice.prepaymentAccountID>((object) e.Row);
    e.Cache.SetDefaultExt<APInvoice.prepaymentSubID>((object) e.Row);
  }

  public virtual void APTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.Base.Document.Current == null || e.Operation != PXDBOperation.Insert && e.Operation != PXDBOperation.Update || !this.Base.Document.Current.IsPrepaymentInvoiceDocument())
      return;
    APTaxTran row = (APTaxTran) e.Row;
    row.TaxType = "B";
    row.CuryAdjustedTaxableAmt = row.CuryTaxableAmt;
    row.AdjustedTaxableAmt = row.TaxableAmt;
    row.CuryAdjustedTaxAmt = row.CuryTaxAmt;
    row.AdjustedTaxAmt = row.TaxAmt;
  }

  public virtual void _(PX.Data.Events.RowSelected<APInvoice> e)
  {
    APInvoice row = e.Row;
    if (row == null)
      return;
    APInvoiceState documentState = this.Base.GetDocumentState(e.Cache, row);
    PXUIFieldAttribute.SetVisible<TaxTran.curyAdjustedTaxableAmt>(this.Base.Taxes.Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetVisible<TaxTran.curyAdjustedTaxAmt>(this.Base.Taxes.Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetDisplayName<APTaxTran.curyTaxableAmt>(this.Base.Taxes.Cache, documentState.IsDocumentPrepaymentInvoice ? "Orig. Taxable Amount" : "Taxable Amount");
    PXUIFieldAttribute.SetDisplayName<APTaxTran.curyTaxAmt>(this.Base.Taxes.Cache, documentState.IsDocumentPrepaymentInvoice ? "Orig. Tax Amount" : "Tax Amount");
    if (documentState.IsDocumentPrepaymentInvoice || documentState.IsPrepaymentInvoiceReversing)
    {
      PXUIFieldAttribute.SetVisible<APInvoice.prebookAcctID>(e.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APInvoice.prebookSubID>(e.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APTran.defScheduleID>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APTran.deferredCode>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APTran.dRTermStartDate>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(this.Base.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APTran.dRTermEndDate>(this.Base.Transactions.Cache, (object) null, false);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<APInvoice.prepaymentAccountID>(e.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<APInvoice.prepaymentSubID>(e.Cache, (object) null, false);
    }
    if (documentState.IsPrepaymentInvoiceReversing)
      PXUIFieldAttribute.SetEnabled<APInvoice.projectID>(e.Cache, (object) null, false);
    if (documentState.IsDocumentPrepaymentInvoice && !row.Released.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<APInvoice.prepaymentAccountID>(e.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APInvoice.prepaymentSubID>(e.Cache, (object) null, false);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APInvoice> e)
  {
    APInvoice row1 = e.Row;
    if (row1 == null)
      return;
    APRegister row2 = (APRegister) e.Row;
    APSetup current = this.Base.APSetup.Current;
    bool? nullable;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current.MigrationMode;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0 && row1.DocType == "PPI")
      throw new PXException("The document of the Prepmt. Invoice type is not supported when migration mode is activated.");
    if (e.Operation.Command() == PXDBOperation.Delete)
      PXDatabase.Delete<PX.Objects.AP.Standalone2.APPayment>((PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.Standalone2.APPayment.docType>(PXDbType.Char, new int?(3), (object) row1.DocType, PXComp.EQ), (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.Standalone2.APPayment.refNbr>(PXDbType.VarChar, new int?(15), (object) row1.RefNbr, PXComp.EQ));
    if (!row1.IsPrepaymentInvoiceDocument() || e.Operation.Command() == PXDBOperation.Delete)
      return;
    if (APPayment.PK.Find((PXGraph) this.Base, row1.DocType, row1.RefNbr) == null)
    {
      PX.Objects.AP.Standalone2.APPayment data = new PX.Objects.AP.Standalone2.APPayment();
      data.DocType = row1.DocType;
      data.ExtRefNbr = row1.InvoiceNbr;
      data.CashAccountID = row1.PayAccountID;
      data.PaymentMethodID = row1.PayTypeID;
      data.AdjDate = row2.DocDate;
      data.AdjFinPeriodID = row2.FinPeriodID;
      data.AdjTranPeriodID = row2.TranPeriodID;
      data.PrintCheck = new bool?(false);
      data.CuryOrigTaxDiscAmt = new Decimal?(0M);
      data.OrigTaxDiscAmt = new Decimal?(0M);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.stubCntr>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.billCntr>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.chargeCntr>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.cleared>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.depositAsBatch>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetDefaultExt<APPayment.deposited>((object) data);
      this.APPayment_DocType_RefNbr.Cache.SetValueExt<APPayment.printed>((object) data, (object) true);
      SharedRecordAttribute.DefaultRecord<PX.Objects.AP.Standalone2.APPayment.remitAddressID>(this.APPayment_DocType_RefNbr.Cache, (object) data);
      SharedRecordAttribute.DefaultRecord<PX.Objects.AP.Standalone2.APPayment.remitContactID>(this.APPayment_DocType_RefNbr.Cache, (object) data);
      this.APPayment_DocType_RefNbr.Cache.Update((object) data);
    }
    PX.Objects.TX.TaxZone taxZone = (PX.Objects.TX.TaxZone) PXSelectBase<PX.Objects.TX.TaxZone, PXViewOf<PX.Objects.TX.TaxZone>.BasedOn<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, (object) row1.TaxZoneID);
    int num2;
    if (taxZone == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = taxZone.IsExternal;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 == 0)
      return;
    e.Cache.RaiseExceptionHandling<APInvoice.taxZoneID>((object) row1, (object) row1.TaxZoneID, (Exception) new PXSetPropertyException((IBqlTable) row1, "Tax zones with the External Tax Provider check box selected are not supported for prepayment invoices."));
  }

  [PXOverride]
  public void Persist(System.Action basePersist)
  {
    foreach (APInvoice doc in this.Base.Document.Cache.Cached)
    {
      PXEntryStatus status = this.Base.Document.Cache.GetStatus((object) doc);
      if (doc.IsPrepaymentInvoiceDocument() && status == PXEntryStatus.Deleted)
        this.Base.Caches<APPayment>().Clear();
    }
    basePersist();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APTran.taxCategoryID> e,
    PXFieldDefaulting baseMethod)
  {
    APRegister current = (APRegister) this.Base.Document.Current;
    if (current != null && current.IsPrepaymentInvoiceDocumentReverse())
      e.NewValue = (object) null;
    else
      baseMethod(e.Cache, e.Args);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APTran> e)
  {
    APTran row = e.Row;
    if (row == null)
      return;
    string message = this.CheckPrepaymentAmount(row);
    PXSetPropertyException propertyException = message != null ? new PXSetPropertyException((IBqlTable) row, message) : (PXSetPropertyException) null;
    e.Cache.RaiseExceptionHandling<APTran.curyPrepaymentAmt>((object) row, (object) row.CuryPrepaymentAmt, (Exception) propertyException);
  }

  public string CheckPrepaymentAmount(APTran tran)
  {
    if (tran == null || !tran.CuryPrepaymentAmt.HasValue || !tran.CuryLineAmt.HasValue || !tran.CuryDiscAmt.HasValue)
      return (string) null;
    Decimal? curyLineAmt = tran.CuryLineAmt;
    Decimal? curyDiscAmt = tran.CuryDiscAmt;
    Decimal? nullable1 = curyLineAmt.HasValue & curyDiscAmt.HasValue ? new Decimal?(curyLineAmt.GetValueOrDefault() - curyDiscAmt.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = tran.CuryPrepaymentAmt;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
    {
      nullable2 = nullable1;
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
        goto label_6;
    }
    nullable2 = tran.CuryPrepaymentAmt;
    Decimal num3 = 0M;
    if (nullable2.GetValueOrDefault() < num3 & nullable2.HasValue)
    {
      nullable2 = nullable1;
      Decimal num4 = 0M;
      if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
        goto label_6;
    }
    nullable2 = tran.CuryPrepaymentAmt;
    return System.Math.Abs(nullable2.Value) > System.Math.Abs(nullable1.Value) ? "The absolute value of the prepayment amount must be less than or equal to the absolute value of the extended price minus the amount of the line discount (if any)." : (string) null;
label_6:
    return "The prepayment amount and the extended price minus the line discount amount (if any) must both be either negative or positive.";
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APTran> e)
  {
    APTran row = e.Row;
    if (row == null)
      return;
    string message = this.CheckPrepaymentAmount(row);
    if (message != null)
      throw new PXRowPersistingException(typeof (APTran.curyPrepaymentAmt).Name, (object) row.CuryPrepaymentAmt, message);
  }

  public class TermsByVendor<VendorID, TermsID, DocType> : 
    BqlFormulaEvaluator<VendorID, TermsID, DocType>
    where VendorID : IBqlOperand
    where TermsID : IBqlOperand
    where DocType : IBqlOperand
  {
    public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
    {
      int? par1 = (int?) pars[typeof (VendorID)];
      string par2 = (string) pars[typeof (TermsID)];
      string par3 = (string) pars[typeof (DocType)];
      if (!par1.HasValue)
        return (object) null;
      cache.GetValuePending<APInvoice.termsID>(item);
      Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.Select(cache.Graph, (object) par1);
      APSetup apSetup = (APSetup) PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select(cache.Graph);
      PX.Objects.CS.Terms terms;
      switch (par3)
      {
        case "PPI":
          terms = (PX.Objects.CS.Terms) PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Required<PX.Objects.CS.Terms.termsID>>, And<PX.Objects.CS.Terms.discPercent, Equal<decimal0>, And<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>>>.Config>.Select(cache.Graph, (object) vendor.TermsID);
          break;
        case "PPM":
          terms = (PX.Objects.CS.Terms) null;
          break;
        case "ADR":
          if ((apSetup != null ? (!apSetup.TermsInDebitAdjustments.GetValueOrDefault() ? 1 : 0) : 1) != 0)
          {
            terms = (PX.Objects.CS.Terms) null;
            break;
          }
          goto default;
        default:
          terms = (PX.Objects.CS.Terms) PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Required<PX.Objects.CS.Terms.termsID>>>>.Config>.Select(cache.Graph, (object) vendor.TermsID);
          break;
      }
      return terms != null ? (object) terms.TermsID : (object) null;
    }
  }
}
