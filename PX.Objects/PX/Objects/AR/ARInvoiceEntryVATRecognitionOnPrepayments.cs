// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARInvoiceEntryVATRecognitionOnPrepayments : 
  PXGraphExtension<
  #nullable disable
  ARInvoiceEntry.MultiCurrency, ARInvoiceEntry>
{
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.AR.Standalone2.ARPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AR.Standalone2.ARPayment.docType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ARRegister.docType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.AR.Standalone2.ARPayment.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARRegister.refNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.AR.Standalone2.ARPayment>.View ARPayment_DocType_RefNbr;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<SOAdjust, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<SOAdjust.adjdOrderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<SOAdjust.adjdOrderNbr>>>>, Where<SOAdjust.adjgDocType, Equal<Current<ARInvoice.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARInvoice.refNbr>>>>> SOAdjustments;
  public PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.customerID, Equal<Required<PX.Objects.SO.SOOrder.customerID>>, And<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>> SOOrder_CustomerID_OrderType_RefNbr;
  [PXHidden]
  public PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<Where<ARTran.sOOrderNbr, IsNotNull>>>>> LinesWithSOLinks;
  public FbqlSelect<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<
  #nullable enable
  ARTax.taxID, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.TX.Tax.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ARTax.tranType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ARInvoice.docType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ARTax.refNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ARInvoice.refNbr, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.TX.Tax.taxCalcLevel, IBqlString>.IsNotEqual<
  #nullable disable
  CSTaxCalcLevel.inclusive>>>>, ARTax>.View NonInclusiveTaxes;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<ARTaxTran>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.TaxesList).WhereAnd<Where<ARTaxTran.tranType, NotEqual<ARDocType.prepaymentInvoice>, Or<TaxTran.taxInvoiceNbr, IsNull>>>();
    SOAdjust current = ((PXSelectBase<SOAdjust>) this.SOAdjustments).Current;
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current<ARRegister.docType>, NotEqual<ARDocType.prepaymentInvoice>, Or<Current<ARRegister.docType>, Equal<ARDocType.prepaymentInvoice>, And<PX.Objects.CS.Terms.installmentType, Equal<TermsInstallmentType.single>>>>), "A prepayment invoice cannot have terms with the Multiple installment type.", new Type[] {})]
  [PXRestrictor(typeof (Where<Current<ARRegister.docType>, NotEqual<ARDocType.prepaymentInvoice>, Or<Current<ARRegister.docType>, Equal<ARDocType.prepaymentInvoice>, And<PX.Objects.CS.Terms.discPercent, Equal<decimal0>>>>), "A prepayment invoice cannot have terms with a cash discount.", new Type[] {})]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXFormula(typeof (ARInvoiceEntryVATRecognitionOnPrepayments.TermsByCustomer<ARInvoice.customerID, ARInvoice.termsID, ARInvoice.docType>))]
  protected virtual void ARInvoice_TermsID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (AccountAttribute))]
  [Account(typeof (ARInvoice.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "Prepayment Account", ControlAccountForModule = "AR")]
  [PXUIRequired(typeof (Where<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>))]
  protected virtual void ARInvoice_PrepaymentAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (Where<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>))]
  protected virtual void ARInvoice_PrepaymentSubID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXFormula(typeof (Switch<Case<Where<ARTran.tranType, NotEqual<ARDocType.prepaymentInvoice>>, Sub<ARTran.curyExtPrice, Add<ARTran.curyDiscAmt, ARTran.curyRetainageAmt>>>, ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt>))]
  [PXFormula(null, typeof (CountCalc<ARSalesPerTran.refCntr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARTran.curyTranAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (ManualDiscountMode))]
  [ManualDiscountModeVATRecognitionOnPrepayments(typeof (ARTran.curyDiscAmt), typeof (ARTran.curyTranAmt), typeof (ARTran.discPct), typeof (ARTran.freezeManualDisc), DiscountFeatureType.CustomerDiscount)]
  protected virtual void _(PX.Data.Events.CacheAttached<ARTran.manualDisc> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (DenormalizedFromAttribute))]
  [DenormalizedFrom(new Type[] {typeof (ARRegister.voided), typeof (ARRegister.hold), typeof (ARRegister.docDesc), typeof (ARRegister.pendingPayment)}, new Type[] {typeof (SOAdjust.voided), typeof (SOAdjust.hold), typeof (SOAdjust.docDesc), typeof (SOAdjust.pendingPayment)}, null, null)]
  [DenormalizedFrom(new Type[] {typeof (ARInvoice.paymentMethodID), typeof (ARInvoice.cashAccountID)}, new Type[] {typeof (SOAdjust.paymentMethodID), typeof (SOAdjust.cashAccountID)}, null, null)]
  protected virtual void _(PX.Data.Events.CacheAttached<SOAdjust.isCCPayment> e)
  {
  }

  protected DiscountEngine<ARTran, ARInvoiceDiscountDetail> ARDiscountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<ARTran, ARInvoiceDiscountDetail>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARInvoiceEntry.MultiCurrency.AllowOverrideCury" />
  /// </summary>
  [PXOverride]
  public virtual bool AllowOverrideCury(Func<bool> baseAllowOverrideCury)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current;
    return (current == null || !current.IsPrepaymentInvoiceDocumentReverse() && (!current.IsPrepaymentInvoiceDocument() || ((PXSelectBase<ARTran>) this.LinesWithSOLinks).SelectSingle(Array.Empty<object>()) == null)) && baseAllowOverrideCury();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARInvoice, ARRegister.prepaymentAccountID> e)
  {
    ARInvoice row = e.Row;
    if (row == null || !(row.DocType == "PPI") || ((PXSelectBase<Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARInvoice, ARRegister.prepaymentAccountID>, ARInvoice, object>) e).NewValue = ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetAcctSub<Customer.prepaymentAcctID>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Cache, (object) ((PXSelectBase<Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARInvoice, ARRegister.prepaymentSubID> e)
  {
    ARInvoice row = e.Row;
    if (row == null || !(row.DocType == "PPI") || ((PXSelectBase<Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARInvoice, ARRegister.prepaymentSubID>, ARInvoice, object>) e).NewValue = ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetAcctSub<Customer.prepaymentSubID>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Cache, (object) ((PXSelectBase<Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current);
  }

  public virtual void ARInvoice_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARRegister.prepaymentAccountID>(e.Row);
    sender.SetDefaultExt<ARRegister.prepaymentSubID>(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARTran> e)
  {
    ARTran row = e.Row;
    if (row == null || !(row.TranType == "PPI"))
      return;
    row.Commissionable = new bool?(false);
  }

  protected virtual void ARSalesPerTran_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e,
    PXRowInserting baseDelegate)
  {
    if (((ARSalesPerTran) e.Row).DocType == "PPI")
      ((CancelEventArgs) e).Cancel = true;
    else
      baseDelegate.Invoke(sender, e);
  }

  public virtual void ARTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current == null || e.Operation != 2 && e.Operation != 1 || !((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.IsPrepaymentInvoiceDocument())
      return;
    ARTaxTran row = (ARTaxTran) e.Row;
    row.TaxType = "A";
    row.CuryAdjustedTaxableAmt = row.CuryTaxableAmt;
    row.AdjustedTaxableAmt = row.TaxableAmt;
    row.CuryAdjustedTaxAmt = row.CuryTaxAmt;
    row.AdjustedTaxAmt = row.TaxAmt;
  }

  public virtual void ARInvoiceDiscountDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e,
    PXRowPersisting baseDelegate)
  {
    baseDelegate.Invoke(sender, e);
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current == null || e.Operation != 2 && e.Operation != 1)
      return;
    bool flag = row.OrderNbr != null;
    if (!(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.IsPrepaymentInvoiceDocument() & flag))
      return;
    PXDefaultAttribute.SetPersistingCheck<ARInvoiceDiscountDetail.discountID>(sender, (object) row, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<ARInvoiceDiscountDetail.discountSequenceID>(sender, (object) row, (PXPersistingCheck) 2);
  }

  public virtual void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    ARInvoiceState documentState = ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetDocumentState(cache, row);
    if (documentState.IsDocumentPrepaymentInvoice || documentState.IsPrepaymentInvoiceReversing)
    {
      PXUIFieldAttribute.SetEnabled<ARTran.defScheduleID>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<ARTran.defScheduleID>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<ARTran.deferredCode>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<ARTran.deferredCode>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<ARTran.dRTermStartDate>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<ARTran.dRTermStartDate>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<ARTran.dRTermEndDate>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<ARTran.dRTermEndDate>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
    }
    PXUIFieldAttribute.SetVisible<TaxTran.curyAdjustedTaxableAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Taxes).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetVisible<TaxTran.curyAdjustedTaxAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Taxes).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetVisible<ARRegister.prepaymentAccountID>(cache, (object) null, documentState.IsDocumentPrepaymentInvoice || documentState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetVisible<ARRegister.prepaymentSubID>(cache, (object) null, documentState.IsDocumentPrepaymentInvoice || documentState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<ARRegister.prepaymentAccountID>(cache, (object) null, documentState.IsDocumentPrepaymentInvoice && !documentState.ShouldDisableHeader);
    PXUIFieldAttribute.SetEnabled<ARRegister.prepaymentSubID>(cache, (object) null, documentState.IsDocumentPrepaymentInvoice && !documentState.ShouldDisableHeader);
    PXUIFieldAttribute.SetDisplayName<ARTaxTran.curyTaxableAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Taxes).Cache, documentState.IsDocumentPrepaymentInvoice ? "Orig. Taxable Amount" : "Taxable Amount");
    PXUIFieldAttribute.SetDisplayName<ARTaxTran.curyTaxAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Taxes).Cache, documentState.IsDocumentPrepaymentInvoice ? "Orig. Tax Amount" : "Tax Amount");
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderNbr>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderType>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetEnabled<ARTran.sOOrderNbr>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.sOOrderType>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<ARTranVATRecognitionOnPrepayments.prepaymentPct>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetVisible<ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetEnabled<ARTranVATRecognitionOnPrepayments.prepaymentPct>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    PXUIFieldAttribute.SetEnabled<ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, (object) null, documentState.IsDocumentPrepaymentInvoice);
    if (((PXSelectBase<ARTran>) this.LinesWithSOLinks).Any<ARTran>() || ((PXSelectBase<SOAdjust>) this.SOAdjustments).Any<SOAdjust>())
      PXUIFieldAttribute.SetEnabled<ARInvoice.customerID>(cache, (object) row, false);
    ARInvoiceVATRecognitionOnPrepayments extension = ((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base).Caches[typeof (ARInvoice)].GetExtension<ARInvoiceVATRecognitionOnPrepayments>((object) row);
    Decimal? nullable1 = extension.CuryPrepaymentAmt;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.CuryDocBal;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
    nullable1 = extension.CuryPrepaymentAmt;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = row.CuryDocBal;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num2 = valueOrDefault1 - valueOrDefault2;
    if (!(num2 != 0M))
      return;
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    Decimal num3 = 1M;
    int num4 = 0;
    while (true)
    {
      int num5 = num4;
      short? curyPrecision = defaultCurrencyInfo.CuryPrecision;
      int? nullable2 = curyPrecision.HasValue ? new int?((int) curyPrecision.GetValueOrDefault()) : new int?();
      int valueOrDefault3 = nullable2.GetValueOrDefault();
      if (num5 < valueOrDefault3 & nullable2.HasValue)
      {
        num3 /= 10M;
        ++num4;
      }
      else
        break;
    }
    Decimal? nullable3 = new Decimal?(num3 * (Decimal) (((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>())).Count<PXResult<ARTran>>() + 2));
    if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() && row.TaxCalcMode == "G")
    {
      Decimal num6 = Math.Abs(num2);
      nullable1 = nullable3;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      if (!(num6 > valueOrDefault4 & nullable1.HasValue))
        return;
      ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Cache.RaiseExceptionHandling<ARRegister.curyDocBal>((object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current, (object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CuryDocBal, (Exception) new PXSetPropertyException((IBqlTable) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current, "The amount of the prepayment invoice ({0}) cannot be adjusted automatically to match the amount specified in the Create Prepayment Invoice dialog box ({1}). Review the document and amend it, if needed. Add group or document discounts manually if they are applicable.", (PXErrorLevel) 2, new object[2]
      {
        (object) row.CuryDocBal,
        (object) extension.CuryPrepaymentAmt
      }));
    }
    else
    {
      nullable1 = nullable3;
      Decimal num7 = num3 * (Decimal) ((IQueryable<PXResult<ARTax>>) ((PXSelectBase<ARTax>) this.NonInclusiveTaxes).Select(Array.Empty<object>())).Count<PXResult<ARTax>>();
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num7) : new Decimal?();
      Decimal num8 = Math.Abs(num2);
      nullable1 = nullable4;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      if (!(num8 > valueOrDefault5 & nullable1.HasValue))
        return;
      ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Cache.RaiseExceptionHandling<ARInvoice.curyDocBal>((object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current, (object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CuryDocBal, (Exception) new PXSetPropertyException((IBqlTable) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current, "The amount of the prepayment invoice ({0}) cannot be adjusted automatically to match the amount specified in the Create Prepayment Invoice dialog box ({1}). Review the document and amend it, if needed. Add group or document discounts manually if they are applicable.", (PXErrorLevel) 2, new object[2]
      {
        (object) row.CuryDocBal,
        (object) extension.CuryPrepaymentAmt
      }));
    }
  }

  public string CheckPrepaymentAmount(ARTran tran, ARTranVATRecognitionOnPrepayments tranExt)
  {
    Decimal? curyExtPrice = tran.CuryExtPrice;
    Decimal? nullable1 = tran.CuryDiscAmt;
    Decimal? nullable2 = curyExtPrice.HasValue & nullable1.HasValue ? new Decimal?(curyExtPrice.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = tranExt.CuryPrepaymentAmt;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      nullable1 = nullable2;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
        goto label_4;
    }
    nullable1 = tranExt.CuryPrepaymentAmt;
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
    {
      nullable1 = nullable2;
      Decimal num4 = 0M;
      if (nullable1.GetValueOrDefault() > num4 & nullable1.HasValue)
        goto label_4;
    }
    nullable1 = tranExt.CuryPrepaymentAmt;
    return Math.Abs(nullable1.Value) > Math.Abs(nullable2.Value) ? "The absolute value of the prepayment amount must be less than or equal to the absolute value of the extended price minus the amount of the line discount (if any)." : (string) null;
label_4:
    return "The prepayment amount and the extended price minus the line discount amount (if any) must both be either negative or positive.";
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARTran> e)
  {
    ARTran row = e.Row;
    if ((row != null ? (!row.CuryExtPrice.HasValue ? 1 : 0) : 1) != 0 || (row != null ? (!row.CuryDiscAmt.HasValue ? 1 : 0) : 1) != 0 || row?.TranType != "PPI")
      return;
    ARTranVATRecognitionOnPrepayments extension = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARTran>>) e).Cache.GetExtension<ARTranVATRecognitionOnPrepayments>((object) row);
    if ((extension != null ? (!extension.CuryPrepaymentAmt.HasValue ? 1 : 0) : 1) != 0)
      return;
    bool flag = row.SOOrderType == null && row.SOOrderNbr == null;
    PXUIFieldAttribute.SetEnabled<ARTran.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARTran>>) e).Cache, (object) null, flag);
    string str = this.CheckPrepaymentAmount(row, extension);
    PXSetPropertyException propertyException = str != null ? new PXSetPropertyException((IBqlTable) row, str) : (PXSetPropertyException) null;
    ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache.RaiseExceptionHandling<ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt>((object) e.Row, (object) extension.CuryPrepaymentAmt, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARTran> e)
  {
    if (e.Row?.TranType != "PPI")
      return;
    TaxBaseAttribute.Calculate<ARTran.taxCategoryID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARTran>>) e).Cache, new PXRowUpdatedEventArgs((object) e.Row, (object) e.OldRow, e.ExternalCall));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARTran> e)
  {
    ARTran row = e.Row;
    if ((row != null ? (!row.CuryExtPrice.HasValue ? 1 : 0) : 1) != 0 || (row != null ? (!row.CuryDiscAmt.HasValue ? 1 : 0) : 1) != 0 || row?.TranType != "PPI")
      return;
    ARTranVATRecognitionOnPrepayments extension = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARTran>>) e).Cache.GetExtension<ARTranVATRecognitionOnPrepayments>((object) row);
    if ((extension != null ? (!extension.CuryPrepaymentAmt.HasValue ? 1 : 0) : 1) != 0)
      return;
    string str = this.CheckPrepaymentAmount(row, extension);
    if (str != null)
      throw new PXRowPersistingException(typeof (ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt).Name, (object) extension.CuryPrepaymentAmt, str);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.taxZoneID> e)
  {
    if (e.Row?.DocType != "PPI")
      return;
    foreach (ARTran arTran in ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache.Updated)
      TaxBaseAttribute.Calculate<ARTran.taxCategoryID>(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, new PXRowUpdatedEventArgs((object) arTran, (object) arTran, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.taxZoneID>>) e).ExternalCall));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARInvoice> e)
  {
    ARInvoice row = e.Row;
    if (row == null)
      return;
    ARSetup current = ((PXSelectBase<ARSetup>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARSetup).Current;
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
    bool flag = num1 != 0;
    if (row.DocType == "PPI" & flag)
      throw new PXException("The document of the Prepmt. Invoice type is not supported when migration mode is activated.");
    if (row.DocType == "PPI")
    {
      nullable = row.RetainageApply;
      if (nullable.GetValueOrDefault())
        throw new PXException("A prepayment invoice cannot have retainage.");
    }
    if (PXDBOperationExt.Command(e.Operation) == 3)
      PXDatabase.Delete<PX.Objects.AR.Standalone2.ARPayment>(new PXDataFieldRestrict[2]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.Standalone2.ARPayment.docType>((PXDbType) 3, new int?(3), (object) row.DocType, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.Standalone2.ARPayment.refNbr>((PXDbType) 22, new int?(15), (object) row.RefNbr, (PXComp) 0)
      });
    if (!row.IsPrepaymentInvoiceDocument() || PXDBOperationExt.Command(e.Operation) == 3)
      return;
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXViewOf<PX.Objects.TX.TaxZone>.BasedOn<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[1]
    {
      (object) row.TaxZoneID
    }));
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
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARInvoice>>) e).Cache.RaiseExceptionHandling<ARInvoice.taxZoneID>((object) row, (object) row.TaxZoneID, (Exception) new PXSetPropertyException((IBqlTable) row, "Tax zones with the External Tax Provider check box selected are not supported for prepayment invoices."));
  }

  public virtual void _(PX.Data.Events.RowDeleting<ARTax> e)
  {
    if (!((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.IsPrepaymentInvoiceDocument() || e.Row.TaxUOM == null)
      return;
    ARTran arTran1 = ((PXSelectBase<ARTran>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Locate(new ARTran()
    {
      TranType = e.Row.TranType,
      RefNbr = e.Row.RefNbr,
      LineNbr = e.Row.LineNbr
    });
    PXCache cache = ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache;
    ARTran arTran2 = arTran1;
    Decimal? curyTranAmt = arTran1.CuryTranAmt;
    Decimal? curyTaxAmt = e.Row.CuryTaxAmt;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyTranAmt.HasValue & curyTaxAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() - curyTaxAmt.GetValueOrDefault()) : new Decimal?());
    cache.SetValueExt<ARTran.curyTranAmt>((object) arTran2, (object) local);
  }

  [PXOverride]
  public void Persist(System.Action basePersist)
  {
    foreach (ARInvoice doc in ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Cache.Cached)
    {
      PXEntryStatus status = ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Cache.GetStatus((object) doc);
      if (doc.IsPrepaymentInvoiceDocument() && status == 3)
        ((PXCache) GraphHelper.Caches<ARPayment>((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base)).Clear();
    }
    basePersist();
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARInvoice> e, PXRowPersisted baseMethod)
  {
    ARInvoice row1 = e.Row;
    ARRegister row2 = (ARRegister) e.Row;
    if (row1.IsPrepaymentInvoiceDocument() && PXDBOperationExt.Command(e.Operation) != 3 && e.TranStatus != 2)
    {
      PX.Objects.AR.Standalone2.ARPayment arPayment = new PX.Objects.AR.Standalone2.ARPayment();
      arPayment.DocType = row1.DocType;
      arPayment.RefNbr = row1.RefNbr;
      arPayment.CashAccountID = row1.CashAccountID;
      arPayment.PaymentMethodID = row1.PaymentMethodID;
      arPayment.AdjDate = row2.DocDate;
      arPayment.AdjFinPeriodID = row2.FinPeriodID;
      arPayment.AdjTranPeriodID = row2.TranPeriodID;
      arPayment.CuryOrigTaxDiscAmt = new Decimal?(0M);
      arPayment.OrigTaxDiscAmt = new Decimal?(0M);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.projectID>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.taskID>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.chargeCntr>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCPayment>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCAuthorized>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCCaptured>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCCaptureFailed>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCRefunded>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.isCCUserAttention>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.cCActualExternalTransactionID>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.cCPaymentStateDescr>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.cCReauthTriesLeft>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.cCTransactionRefund>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.consolidateChargeTotal>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.curyConsolidateChargeTotal>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.cleared>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.saveCard>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.depositAsBatch>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.deposited>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.SetDefaultExt<ARPayment.settled>((object) arPayment);
      ((PXSelectBase) this.ARPayment_DocType_RefNbr).Cache.Update((object) arPayment);
      ((PXSelectBase) this.SOAdjustments).Cache.SetValueExt<SOAdjust.curyOrigDocAmt>((object) ((PXSelectBase<SOAdjust>) this.SOAdjustments).Current, (object) row1.CuryOrigDocAmt);
    }
    baseMethod.Invoke(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<ARInvoice>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<ARInvoice>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.curyDocBal> e)
  {
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.curyDocBal>, ARInvoice, object>) e).OldValue)
      return;
    foreach (PXResult<SOAdjust, PX.Objects.SO.SOOrder> pxResult in ((PXSelectBase<SOAdjust>) this.SOAdjustments).Select(Array.Empty<object>()))
    {
      SOAdjust adj = PXResult<SOAdjust, PX.Objects.SO.SOOrder>.op_Implicit(pxResult);
      PX.Objects.SO.SOOrder soOrder = PXResult<SOAdjust, PX.Objects.SO.SOOrder>.op_Implicit(pxResult);
      Decimal? nullable1 = (Decimal?) ((PXSelectBase) this.SOAdjustments).Cache.GetValueOriginal<SOAdjust.curyAdjdAmt>((object) adj);
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      if (adj.AdjdCuryInfoID.HasValue && ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.curyDocBal>>) e).Cache.GetStatus((object) e.Row) != 3)
      {
        nullable1 = (Decimal?) e.NewValue;
        Decimal num1 = valueOrDefault;
        Decimal? curyUnpaidBalance = soOrder.CuryUnpaidBalance;
        Decimal? nullable2 = curyUnpaidBalance.HasValue ? new Decimal?(num1 + curyUnpaidBalance.GetValueOrDefault()) : new Decimal?();
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        {
          adj.CuryAdjdAmt = (Decimal?) e.NewValue;
        }
        else
        {
          SOAdjust soAdjust = adj;
          Decimal num2 = valueOrDefault;
          nullable2 = soOrder.CuryUnpaidBalance;
          Decimal? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new Decimal?(num2 + nullable2.GetValueOrDefault());
          soAdjust.CuryAdjdAmt = nullable3;
        }
        adj.CuryOrigDocAmt = (Decimal?) e.NewValue;
        this.CalcBalances(adj, false, false);
        ((PXSelectBase) this.SOAdjustments).Cache.Update((object) adj);
      }
    }
  }

  public void _(PX.Data.Events.FieldDefaulting<ARTran.taxCategoryID> e)
  {
    if (e.Row == null || string.IsNullOrEmpty(((ARTran) e.Row).SOOrderNbr))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARTran.taxCategoryID>, object, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARTran.taxCategoryID>>) e).Cancel = true;
  }

  public void _(PX.Data.Events.FieldDefaulting<ARTaxTran.accountID> e)
  {
    ARTaxTran row = (ARTaxTran) e.Row;
    if (row == null || !(row.TranType == "PPI"))
      return;
    SalesTax salesTax = PXResultset<SalesTax>.op_Implicit(PXSelectBase<SalesTax, PXViewOf<SalesTax>.BasedOn<SelectFromBase<SalesTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SalesTax.taxID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[1]
    {
      (object) row.TaxID
    }));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARTaxTran.accountID>, object, object>) e).NewValue = (object) (int?) salesTax?.OnARPrepaymentTaxAcctID;
  }

  public void _(PX.Data.Events.FieldDefaulting<ARTaxTran.subID> e)
  {
    ARTaxTran row = (ARTaxTran) e.Row;
    if (row == null || !(row.TranType == "PPI"))
      return;
    SalesTax salesTax = PXResultset<SalesTax>.op_Implicit(PXSelectBase<SalesTax, PXViewOf<SalesTax>.BasedOn<SelectFromBase<SalesTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SalesTax.taxID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[1]
    {
      (object) row.TaxID
    }));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARTaxTran.subID>, object, object>) e).NewValue = (object) (int?) salesTax?.OnARPrepaymentTaxSubID;
  }

  public void _(PX.Data.Events.RowSelected<ARTaxTran> e)
  {
    ARTaxTran row = e.Row;
    if (row == null)
      return;
    PX.Objects.TX.Tax tax = PXSelectorAttribute.Select<PX.Objects.TX.Tax.taxID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARTaxTran>>) e).Cache, (object) row) as PX.Objects.TX.Tax;
    if (!(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current?.DocType == "PPI") || tax == null || !(tax.TaxType == "S") && !(tax.TaxType == "P") && !(tax.TaxType == "W"))
      return;
    string taxTypeLabel = CSTaxType.GetTaxTypeLabel(tax.TaxType);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARTaxTran>>) e).Cache.RaiseExceptionHandling<ARTaxTran.taxID>((object) e.Row, (object) row.TaxID, (Exception) new PXSetPropertyException((IBqlTable) row, "{0} taxes are not supported for prepayment invoices.", (PXErrorLevel) 5, new object[1]
    {
      (object) taxTypeLabel
    }));
  }

  public void CalcBalances(SOAdjust adj, PX.Objects.SO.SOOrder invoice, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    ARInvoiceEntry.MultiCurrency extension = ((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base).GetExtension<ARInvoiceEntry.MultiCurrency>();
    new PaymentBalanceCalculator((IPXCurrencyHelper) extension).CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) invoice, (IAdjustment) adj);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
    new PaymentBalanceAjuster((IPXCurrencyHelper) extension).AdjustBalance((IAdjustment) adj);
    if (!isCalcRGOL || adj.Voided.GetValueOrDefault())
      return;
    new PaymentRGOLCalculator((IPXCurrencyHelper) extension, (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) invoice);
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PX.Objects.CM.PXCurrencyAttribute))]
  [PXCurrency(typeof (PX.Objects.SO.SOOrder.curyInfoID), typeof (PX.Objects.SO.SOOrder.docBal))]
  protected virtual void SOOrder_CuryDocBal_CacheAttached(PXCache sender)
  {
  }

  public void CalcBalances(SOAdjust adj, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    if (PXTransactionScope.IsConnectionOutOfScope)
      return;
    foreach (PXResult<PX.Objects.SO.SOOrder> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrder>) this.SOOrder_CustomerID_OrderType_RefNbr).Select(new object[3]
    {
      (object) adj.CustomerID,
      (object) adj.AdjdOrderType,
      (object) adj.AdjdOrderNbr
    }))
    {
      PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(PXResult<PX.Objects.SO.SOOrder>.op_Implicit(pxResult));
      SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.voided, Equal<False>, And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>, Aggregate<GroupBy<SOAdjust.adjdOrderType, GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[4]
      {
        (object) adj.AdjdOrderType,
        (object) adj.AdjdOrderNbr,
        (object) adj.AdjgDocType,
        (object) adj.AdjgRefNbr
      }));
      if (soAdjust != null && soAdjust.AdjdOrderNbr != null)
      {
        PX.Objects.SO.SOOrder soOrder1 = copy;
        Decimal? nullable1 = soOrder1.CuryDocBal;
        Decimal? nullable2 = soAdjust.CuryAdjdAmt;
        soOrder1.CuryDocBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        PX.Objects.SO.SOOrder soOrder2 = copy;
        nullable2 = soOrder2.DocBal;
        nullable1 = soAdjust.AdjAmt;
        soOrder2.DocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      }
      this.CalcBalances(adj, copy, isCalcRGOL, DiscOnDiscDate);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderNbr> e)
  {
    try
    {
      if (e.Row.AdjdCuryInfoID.HasValue)
        return;
      using (IEnumerator<PXResult<PX.Objects.SO.SOOrder>> enumerator = PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.SO.SOOrder.curyInfoID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[2]
      {
        (object) e.Row.AdjdOrderType,
        (object) e.Row.AdjdOrderNbr
      }).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.UpdateAppliedToOrderAmount(PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), e.Row);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  protected virtual void UpdateAppliedToOrderAmount(
    PX.Objects.SO.SOOrder order,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    SOAdjust adj)
  {
    PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(order);
    adj.CustomerID = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CustomerID;
    adj.AdjgDocDate = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocDate;
    adj.AdjgCuryInfoID = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CuryInfoID;
    adj.AdjdCuryInfoID = curyInfo.CuryInfoID;
    adj.AdjdOrigCuryInfoID = copy.CuryInfoID;
    SOAdjust soAdjust1 = adj;
    DateTime? orderDate = copy.OrderDate;
    DateTime? docDate = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocDate;
    DateTime? nullable1 = (orderDate.HasValue & docDate.HasValue ? (orderDate.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocDate : copy.OrderDate;
    soAdjust1.AdjdOrderDate = nullable1;
    adj.Released = new bool?(false);
    SOAdjust soAdjust2 = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.voided, Equal<False>, And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>, Aggregate<GroupBy<SOAdjust.adjdOrderType, GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[4]
    {
      (object) adj.AdjdOrderType,
      (object) adj.AdjdOrderNbr,
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }));
    Decimal? nullable2;
    Decimal? nullable3;
    if (soAdjust2 != null && soAdjust2.AdjdOrderNbr != null)
    {
      PX.Objects.SO.SOOrder soOrder1 = copy;
      Decimal? curyDocBal = soOrder1.CuryDocBal;
      nullable2 = soAdjust2.CuryAdjdAmt;
      soOrder1.CuryDocBal = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      PX.Objects.SO.SOOrder soOrder2 = copy;
      nullable2 = soOrder2.DocBal;
      nullable3 = soAdjust2.AdjAmt;
      soOrder2.DocBal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    this.CalcBalances(adj, copy, false, true);
    nullable3 = adj.CuryDocBal;
    nullable2 = adj.CuryDiscBal;
    Decimal? nullable4 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = adj.CuryDiscBal;
    Decimal? curyDocBal1 = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CuryDocBal;
    nullable2 = adj.CuryDiscBal;
    Decimal num1 = 0M;
    Decimal? nullable6;
    if (nullable2.GetValueOrDefault() >= num1 & nullable2.HasValue)
    {
      nullable3 = adj.CuryDocBal;
      nullable6 = adj.CuryDiscBal;
      nullable2 = nullable3.HasValue & nullable6.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
        return;
    }
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current != null && string.IsNullOrEmpty(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocDesc))
      ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocDesc = copy.OrderDesc;
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current != null)
    {
      nullable2 = curyDocBal1;
      Decimal num3 = 0M;
      if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
      {
        nullable4 = new Decimal?(Math.Min(nullable4.Value, curyDocBal1.Value));
        nullable3 = nullable4;
        Decimal? nullable7 = nullable5;
        nullable2 = nullable3.HasValue & nullable7.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        nullable6 = adj.CuryDocBal;
        if (nullable2.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable2.HasValue & nullable6.HasValue)
        {
          nullable5 = new Decimal?(0M);
          goto label_14;
        }
        goto label_14;
      }
    }
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current != null)
    {
      nullable6 = curyDocBal1;
      Decimal num4 = 0M;
      if (nullable6.GetValueOrDefault() <= num4 & nullable6.HasValue)
      {
        nullable6 = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.CuryOrigDocAmt;
        Decimal num5 = 0M;
        if (nullable6.GetValueOrDefault() > num5 & nullable6.HasValue)
        {
          nullable4 = new Decimal?(0M);
          nullable5 = new Decimal?(0M);
        }
      }
    }
label_14:
    SOAdjust soAdjust3 = (SOAdjust) ((PXSelectBase) this.SOAdjustments).Cache.Locate((object) adj);
    if (soAdjust3 == null)
      soAdjust3 = (SOAdjust) PrimaryKeyOf<SOAdjust>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>>.Find((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (TypeArrayOf<IBqlField>.IFilledWith<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>) adj, (PKFindOptions) 0);
    else if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.SOAdjustments).Cache.GetStatus((object) soAdjust3), (PXEntryStatus) 3, (PXEntryStatus) 4))
      soAdjust3 = (SOAdjust) null;
    PXCache cache1 = ((PXSelectBase) this.SOAdjustments).Cache;
    SOAdjust soAdjust4 = adj;
    Decimal? nullable8;
    if (soAdjust3 == null)
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = soAdjust3.CuryAdjgAmt;
    nullable6 = nullable8;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> valueOrDefault1 = (ValueType) nullable6.GetValueOrDefault();
    cache1.SetValue<SOAdjust.curyAdjgAmt>((object) soAdjust4, (object) valueOrDefault1);
    PXCache cache2 = ((PXSelectBase) this.SOAdjustments).Cache;
    SOAdjust soAdjust5 = adj;
    Decimal? nullable9;
    if (soAdjust3 == null)
    {
      nullable6 = new Decimal?();
      nullable9 = nullable6;
    }
    else
    {
      // ISSUE: explicit non-virtual call
      nullable9 = __nonvirtual (soAdjust3.CuryAdjgDiscAmt);
    }
    nullable6 = nullable9;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> valueOrDefault2 = (ValueType) nullable6.GetValueOrDefault();
    cache2.SetValue<SOAdjust.curyAdjgDiscAmt>((object) soAdjust5, (object) valueOrDefault2);
    ((PXSelectBase) this.SOAdjustments).Cache.SetValueExt<SOAdjust.curyAdjgAmt>((object) adj, (object) nullable4);
    ((PXSelectBase) this.SOAdjustments).Cache.SetValueExt<SOAdjust.curyAdjgDiscAmt>((object) adj, (object) nullable5);
    this.CalcBalances(adj, copy, true, true);
  }

  [PXOverride]
  public virtual void RecalculateDiscountsProc(
    bool redirect,
    ARInvoiceEntryVATRecognitionOnPrepayments.PerformBasicReleaseChecksDelegate baseMethod)
  {
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current?.DocType == "PPI")
      this.RecalculateOrigDiscountsProc();
    baseMethod(redirect);
  }

  [PXOverride]
  public virtual void RecalculateDiscounts(
    PXCache sender,
    ARTran line,
    ARInvoiceEntryVATRecognitionOnPrepayments.RecalculateDiscountsDelegate baseMethod)
  {
    if (line.TranType == "PPI" && FlaggedModeScopeBase<ARInvoiceEntry.SuppressRecalculateDiscountScope>.IsActive)
      return;
    baseMethod(sender, line);
  }

  /// <summary>
  /// This method recalculates original discounts - discounts on AR transactions that came from sales orders
  /// </summary>
  protected virtual void RecalculateOrigDiscountsProc()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
      return;
    PXResultset<ARRegister> pxResultset = PXSelectBase<ARRegister, PXViewOf<ARRegister>.BasedOn<SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARTran.tranType>>>>>.And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<ARTran.refNbr>>>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<ARTran.sOOrderType>>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<ARTran.sOOrderNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<BqlField<ARRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<BqlField<ARRegister.refNbr, IBqlString>.FromCurrent>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, Array.Empty<object>());
    List<PX.Objects.SO.SOOrder> source = new List<PX.Objects.SO.SOOrder>();
    foreach (PXResult<ARRegister, ARTran, PX.Objects.SO.SOOrder> pxResult1 in pxResultset)
    {
      ARTran arTran = PXResult<ARRegister, ARTran, PX.Objects.SO.SOOrder>.op_Implicit(pxResult1);
      PX.Objects.SO.SOOrder order = PXResult<ARRegister, ARTran, PX.Objects.SO.SOOrder>.op_Implicit(pxResult1);
      if (!source.Select<PX.Objects.SO.SOOrder, bool>((Func<PX.Objects.SO.SOOrder, bool>) (x => x.OrderType == order.OrderType && x.OrderNbr == order.OrderNbr)).Any<bool>())
        source.Add(order);
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult2 in ((PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Select(Array.Empty<object>()))
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult2);
        if (invoiceDiscountDetail.DiscountID != null && ((IEnumerable<ushort>) arTran.DiscountsAppliedToLine).Contains<ushort>(invoiceDiscountDetail.LineNbr.GetValueOrDefault()))
          ((PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Delete(invoiceDiscountDetail);
      }
    }
    foreach (PX.Objects.SO.SOOrder soOrder in source)
      this.CreateGroupAndDocumentDiscountDetails(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current, soOrder);
  }

  /// <summary>
  /// This method prepares group and document discounts for SO invoice. Discounts are either prorated from the originating SO to the AR document, or recalculated on the AR level.
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  public virtual void CreateGroupAndDocumentDiscountDetails(ARInvoice newdoc, PX.Objects.SO.SOOrder soOrder)
  {
    bool discountsFeatureEnabled = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    ARInvoiceDiscountDetail draftDiscountDetail1 = this.CreateDraftDiscountDetail((ARRegister) newdoc, soOrder, "G", discountsFeatureEnabled, "I");
    ARInvoiceDiscountDetail draftDiscountDetail2 = this.CreateDraftDiscountDetail((ARRegister) newdoc, soOrder, "D", discountsFeatureEnabled, "I");
    ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer discountDetailComparer = new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer();
    ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparerNoID detailComparerNoId = new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparerNoID();
    TwoWayLookup<ARInvoiceDiscountDetail, ARTran> twoWayLookup = new TwoWayLookup<ARInvoiceDiscountDetail, ARTran>((IEqualityComparer<ARInvoiceDiscountDetail>) discountDetailComparer, (IEqualityComparer<ARTran>) null, (Func<ARInvoiceDiscountDetail, ARTran, bool>) null);
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      ARTran tran = PXResult<ARTran>.op_Implicit(pxResult);
      if ((!discountsFeatureEnabled ? 1 : (!(soOrder.OrderType == tran.SOOrderType) ? 0 : (soOrder.OrderNbr == tran.SOOrderNbr ? 1 : 0))) != 0)
        ARInvoiceEntryVATRecognitionOnPrepayments.SetDiscountAmounts(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache, draftDiscountDetail1, draftDiscountDetail2, twoWayLookup, tran, discountsFeatureEnabled);
    }
    foreach (ARInvoiceDiscountDetail leftValue1 in twoWayLookup.LeftValues)
    {
      ARInvoiceDiscountDetail invoiceDiscountDetail1 = (ARInvoiceDiscountDetail) null;
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Select(Array.Empty<object>()))
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail2 = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
        if (invoiceDiscountDetail2.DiscountID == leftValue1.DiscountID && invoiceDiscountDetail2.DiscountSequenceID == leftValue1.DiscountSequenceID && (!discountsFeatureEnabled || invoiceDiscountDetail2.OrderType == leftValue1.OrderType && invoiceDiscountDetail2.OrderNbr == leftValue1.OrderNbr) && invoiceDiscountDetail2.RefNbr == leftValue1.RefNbr && invoiceDiscountDetail2.Type == leftValue1.Type)
        {
          invoiceDiscountDetail1 = invoiceDiscountDetail2;
          break;
        }
      }
      if (leftValue1.CuryDiscountAmt.IsNullOrZero() || leftValue1.CuryDiscountableAmt.IsNullOrZero())
      {
        leftValue1.DiscountPct = new Decimal?(0M);
      }
      else
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail3 = leftValue1;
        Decimal? nullable1 = leftValue1.CuryDiscountAmt;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        nullable1 = leftValue1.CuryDiscountableAmt;
        Decimal num = nullable1 ?? 1M;
        Decimal? nullable2 = new Decimal?(valueOrDefault / num * 100M);
        invoiceDiscountDetail3.DiscountPct = nullable2;
      }
      ARInvoiceDiscountDetail discountDetail2;
      if (invoiceDiscountDetail1 != null)
      {
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountAmt>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountAmt);
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail1, (object) leftValue1.CuryDiscountAmt);
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountableAmt>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountableAmt);
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.curyDiscountableAmt>((object) invoiceDiscountDetail1, (object) leftValue1.CuryDiscountableAmt);
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountableQty>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountableQty);
        ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountPct>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountPct);
        discountDetail2 = ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache.GetStatus((object) invoiceDiscountDetail1) != 3 ? this.ARDiscountEngine.UpdateDiscountDetail(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails, invoiceDiscountDetail1) : this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails, invoiceDiscountDetail1);
      }
      else
        discountDetail2 = this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.ARDiscountDetails, leftValue1);
      if (discountDetail2 != null)
      {
        foreach (ARInvoiceDiscountDetail leftValue2 in twoWayLookup.LeftValues)
        {
          if (detailComparerNoId.Equals(leftValue2, discountDetail2))
            leftValue2.LineNbr = discountDetail2.LineNbr;
        }
      }
    }
    this.ARDiscountEngine.UpdateListOfDiscountsAppliedToLines(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache, twoWayLookup, ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current));
    ((PXGraphExtension<ARInvoiceEntry>) this).Base.RecalculateTotalDiscount();
  }

  private static void SetDiscountAmounts(
    PXCache cache,
    ARInvoiceDiscountDetail draftGroupDiscountDetail,
    ARInvoiceDiscountDetail draftDocumentDiscountDetail,
    TwoWayLookup<ARInvoiceDiscountDetail, ARTran> discountCodesWithApplicableARLines,
    ARTran tran,
    bool discountsFeatureEnabled)
  {
    Decimal? nullable1 = discountsFeatureEnabled ? tran.OrigGroupDiscountRate : tran.GroupDiscountRate;
    Decimal? nullable2 = discountsFeatureEnabled ? tran.OrigDocumentDiscountRate : tran.DocumentDiscountRate;
    Decimal? nullable3 = nullable1;
    Decimal num1 = (Decimal) 1;
    Decimal? nullable4;
    if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue))
    {
      nullable3 = tran.CuryTranAmt;
      Decimal num2 = nullable3.GetValueOrDefault() * (1M - nullable1.GetValueOrDefault());
      ARInvoiceDiscountDetail invoiceDiscountDetail1 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail1.CuryDiscountAmt;
      Decimal num3 = num2;
      invoiceDiscountDetail1.CuryDiscountAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num3) : new Decimal?();
      ARInvoiceDiscountDetail invoiceDiscountDetail2 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail2.CuryDiscountableAmt;
      PXCache sender = cache;
      ARInvoiceDiscountDetail row = draftGroupDiscountDetail;
      nullable4 = tran.CuryTranAmt;
      Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
      Decimal num4 = PX.Objects.CM.PXCurrencyAttribute.RoundCury(sender, (object) row, valueOrDefault1);
      Decimal? nullable5;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable5 = nullable4;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() + num4);
      invoiceDiscountDetail2.CuryDiscountableAmt = nullable5;
      nullable3 = tran.TranAmt;
      Decimal num5 = nullable3.GetValueOrDefault() * (1M - nullable1.GetValueOrDefault());
      ARInvoiceDiscountDetail invoiceDiscountDetail3 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail3.DiscountAmt;
      Decimal num6 = num5;
      Decimal? nullable6;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable3.GetValueOrDefault() + num6);
      invoiceDiscountDetail3.DiscountAmt = nullable6;
      ARInvoiceDiscountDetail invoiceDiscountDetail4 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail4.DiscountableAmt;
      PXGraph graph = cache.Graph;
      nullable4 = tran.TranAmt;
      Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
      Decimal num7 = PX.Objects.CM.PXCurrencyAttribute.BaseRound(graph, valueOrDefault2);
      Decimal? nullable7;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(nullable3.GetValueOrDefault() + num7);
      invoiceDiscountDetail4.DiscountableAmt = nullable7;
      ARInvoiceDiscountDetail invoiceDiscountDetail5 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail5.DiscountableQty;
      nullable4 = tran.Qty;
      invoiceDiscountDetail5.DiscountableQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      discountCodesWithApplicableARLines.Link(draftGroupDiscountDetail, tran);
    }
    nullable4 = nullable2;
    Decimal num8 = (Decimal) 1;
    if (nullable4.GetValueOrDefault() == num8 & nullable4.HasValue)
      return;
    nullable4 = tran.CuryTranAmt;
    Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
    nullable4 = tran.CuryTranAmt;
    Decimal num9 = nullable4.GetValueOrDefault() * (1M - (nullable1 ?? 1M));
    Decimal val = valueOrDefault3 - num9;
    nullable4 = tran.TranAmt;
    Decimal valueOrDefault4 = nullable4.GetValueOrDefault();
    nullable4 = tran.TranAmt;
    Decimal num10 = nullable4.GetValueOrDefault() * (1M - (nullable1 ?? 1M));
    Decimal num11 = valueOrDefault4 - num10;
    Decimal num12 = val * (1M - nullable2.GetValueOrDefault());
    ARInvoiceDiscountDetail invoiceDiscountDetail6 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail6.CuryDiscountAmt;
    Decimal num13 = num12;
    Decimal? nullable8;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable8 = nullable3;
    }
    else
      nullable8 = new Decimal?(nullable4.GetValueOrDefault() + num13);
    invoiceDiscountDetail6.CuryDiscountAmt = nullable8;
    ARInvoiceDiscountDetail invoiceDiscountDetail7 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail7.CuryDiscountableAmt;
    Decimal num14 = PX.Objects.CM.PXCurrencyAttribute.RoundCury(cache, (object) draftGroupDiscountDetail, val);
    Decimal? nullable9;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable9 = nullable3;
    }
    else
      nullable9 = new Decimal?(nullable4.GetValueOrDefault() + num14);
    invoiceDiscountDetail7.CuryDiscountableAmt = nullable9;
    Decimal num15 = num11 * (1M - nullable2.GetValueOrDefault());
    ARInvoiceDiscountDetail invoiceDiscountDetail8 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail8.DiscountAmt;
    Decimal num16 = num15;
    Decimal? nullable10;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable10 = nullable3;
    }
    else
      nullable10 = new Decimal?(nullable4.GetValueOrDefault() + num16);
    invoiceDiscountDetail8.DiscountAmt = nullable10;
    ARInvoiceDiscountDetail invoiceDiscountDetail9 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail9.DiscountableAmt;
    Decimal num17 = PX.Objects.CM.PXCurrencyAttribute.BaseRound(cache.Graph, num11);
    Decimal? nullable11;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable11 = nullable3;
    }
    else
      nullable11 = new Decimal?(nullable4.GetValueOrDefault() + num17);
    invoiceDiscountDetail9.DiscountableAmt = nullable11;
    ARInvoiceDiscountDetail invoiceDiscountDetail10 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail10.DiscountableQty;
    nullable3 = tran.Qty;
    invoiceDiscountDetail10.DiscountableQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    discountCodesWithApplicableARLines.Link(draftDocumentDiscountDetail, tran);
  }

  private ARInvoiceDiscountDetail CreateDraftDiscountDetail(
    ARRegister invoice,
    PX.Objects.SO.SOOrder soOrder,
    string discountType,
    bool discountsFeatureEnabled,
    string operation = null)
  {
    return new ARInvoiceDiscountDetail()
    {
      Type = discountsFeatureEnabled ? discountType : "B",
      DocType = invoice.DocType,
      RefNbr = invoice.RefNbr,
      OrderType = discountsFeatureEnabled ? soOrder?.OrderType : (string) null,
      OrderNbr = discountsFeatureEnabled ? soOrder?.OrderNbr : (string) null,
      CuryInfoID = invoice.CuryInfoID,
      CuryDiscountAmt = new Decimal?(0M),
      DiscountAmt = new Decimal?(0M),
      CuryDiscountableAmt = new Decimal?(0M),
      DiscountableAmt = new Decimal?(0M),
      DiscountableQty = new Decimal?(0M),
      IsOrigDocDiscount = new bool?(discountsFeatureEnabled),
      Description = discountsFeatureEnabled ? (discountType == "G" ? "Group discounts from the related sales order" : "Document discounts from the related sales order") : "Discount Total Adjustment"
    };
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    if (!(((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.CurrentDocument).Current?.DocType == "PPI"))
      return;
    (string, string)[] valueTupleArray = new (string, string)[7]
    {
      ("Document", "RetainageApply"),
      ("CurrentDocument", "RetainageAcctID"),
      ("CurrentDocument", "RetainageSubID"),
      ("CurrentDocument", "DefRetainagePct"),
      ("Transactions", "RetainagePct"),
      ("Transactions", "CuryRetainageAmt"),
      ("Transactions", "CuryRetainageBal")
    };
    foreach ((string str1, string str2) in valueTupleArray)
    {
      int index = script.FindIndex((Predicate<Command>) (cmd => cmd.ObjectName.StartsWith(str1) && cmd.FieldName == str2));
      if (index != -1)
      {
        script.RemoveAt(index);
        containers.RemoveAt(index);
      }
    }
  }

  public delegate void PerformBasicReleaseChecksDelegate(bool redirect);

  public delegate void RecalculateDiscountsDelegate(PXCache sender, ARTran line);

  public class TermsByCustomer<CustomerID, TermsID, DocType> : 
    BqlFormulaEvaluator<CustomerID, TermsID, DocType>
    where CustomerID : IBqlOperand
    where TermsID : IBqlOperand
    where DocType : IBqlOperand
  {
    public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
    {
      int? par1 = (int?) pars[typeof (CustomerID)];
      string par2 = (string) pars[typeof (TermsID)];
      string par3 = (string) pars[typeof (DocType)];
      if (!par1.HasValue)
        return (object) null;
      PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) null;
      object valuePending = cache.GetValuePending<ARInvoice.termsID>(item);
      if (((BqlFormulaEvaluator) this).IsExternalCall || par2 == null || valuePending == null || valuePending == PXCache.NotSetValue)
      {
        Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select(cache.Graph, new object[1]
        {
          (object) par1
        }));
        ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(cache.Graph, Array.Empty<object>()));
        if (par3 == "PPI")
          terms = PXResultset<PX.Objects.CS.Terms>.op_Implicit(PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Required<PX.Objects.CS.Terms.termsID>>, And<PX.Objects.CS.Terms.discPercent, Equal<decimal0>, And<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>>>.Config>.Select(cache.Graph, new object[1]
          {
            (object) customer.TermsID
          }));
        else if (par3 != "CRM" || arSetup != null && arSetup.TermsInCreditMemos.GetValueOrDefault())
          terms = PXResultset<PX.Objects.CS.Terms>.op_Implicit(PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Required<PX.Objects.CS.Terms.termsID>>>>.Config>.Select(cache.Graph, new object[1]
          {
            (object) customer.TermsID
          }));
      }
      return terms != null ? (object) terms.TermsID : (object) null;
    }
  }
}
