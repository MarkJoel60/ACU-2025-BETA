// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.CreatePaymentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using PX.Objects.SO.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class CreatePaymentExt : 
  CreatePaymentExtBase<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust>
{
  public override void Initialize()
  {
    base.Initialize();
    PXAction action = ((PXGraph) this.Base).Actions["action"];
    if (action == null)
      return;
    action.AddMenuAction((PXAction) this.createAndAuthorizePayment);
    action.SetVisible("CreateAndAuthorizePayment", false);
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> eventArgs)
  {
    base._(eventArgs);
    if (eventArgs.Row == null)
      return;
    bool flag1 = EnumerableExtensions.IsIn<string>(eventArgs.Row.DocType, "INV", "DRM");
    bool flag2 = eventArgs.Row.DocType == "INV" && PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
    bool flag3 = eventArgs.Row.DocType == "CRM";
    bool flag4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) eventArgs).Cache.GetStatus((object) eventArgs.Row) == 2;
    bool flag5 = EnumerableExtensions.IsIn<string>(eventArgs.Row.Status, "H", "W", "R", "B");
    bool flag6 = flag5 || EnumerableExtensions.IsIn<string>(eventArgs.Row.Status, "E", "P");
    Decimal? curyUnpaidBalance;
    int num1;
    if (((!flag1 ? 0 : (!flag4 ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
    {
      curyUnpaidBalance = eventArgs.Row.CuryUnpaidBalance;
      Decimal num2 = 0M;
      num1 = curyUnpaidBalance.GetValueOrDefault() > num2 & curyUnpaidBalance.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag7 = num1 != 0;
    int num3;
    if (((!flag3 ? 0 : (!flag4 ? 1 : 0)) & (flag6 ? 1 : 0)) != 0)
    {
      curyUnpaidBalance = eventArgs.Row.CuryUnpaidBalance;
      Decimal num4 = 0M;
      num3 = curyUnpaidBalance.GetValueOrDefault() > num4 & curyUnpaidBalance.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag8 = num3 != 0;
    string paymentType = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, eventArgs.Row.PaymentMethodID)?.PaymentType;
    bool flag9 = flag7 && eventArgs.Row.PaymentMethodID != null && (paymentType == "CCD" || paymentType == "EFT");
    ((PXAction) this.createDocumentPayment).SetVisible(flag1);
    ((PXAction) this.createDocumentPayment).SetEnabled(flag7);
    ((PXAction) this.createDocumentRefund).SetVisible(flag3);
    ((PXAction) this.createDocumentRefund).SetEnabled(flag8);
    ((PXAction) this.importDocumentPayment).SetVisible(flag1);
    ((PXAction) this.importDocumentPayment).SetEnabled(flag9);
    ((PXAction) this.increaseAuthorizedAmount).SetVisible(flag2);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOInvoice> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool flag1 = ((PXSelectBase) this.Base.Document).Cache.AllowUpdate && EnumerableExtensions.IsIn<string>(eventArgs.Row.DocType, "CSL", "RCS", "INV", "CRM");
    bool flag2 = false;
    if (flag1 && !string.IsNullOrEmpty(eventArgs.Row.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, eventArgs.Row.PaymentMethodID);
      flag2 = paymentMethod != null && paymentMethod.IsAccountNumberRequired.GetValueOrDefault();
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.paymentMethodID>(((PXSelectBase) this.Base.SODocument).Cache, (object) eventArgs.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.pMInstanceID>(((PXSelectBase) this.Base.SODocument).Cache, (object) eventArgs.Row, flag1 & flag2);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARTran> eventArgs)
  {
    this.MarkRefundAdjUpdatedForValidation(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARTran> eventArgs)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARTran>>) eventArgs).Cache.ObjectsEqual<ARTran.qty>((object) eventArgs.OldRow, (object) eventArgs.Row))
      return;
    Decimal? qty = eventArgs.Row.Qty;
    Decimal num = 0M;
    if (!(qty.GetValueOrDefault() == num & qty.HasValue))
      return;
    this.MarkRefundAdjUpdatedForValidation(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARAdjust> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    if (!(eventArgs.Row.AdjdDocType == "CRM") || !(eventArgs.Row.AdjgDocType == "REF") || !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() || !eventArgs.Row.IsCCPayment.GetValueOrDefault() || eventArgs.Row.IsCCAuthorized.GetValueOrDefault() || eventArgs.Row.IsCCCaptured.GetValueOrDefault() || eventArgs.Row.Voided.GetValueOrDefault() || eventArgs.Row.Released.GetValueOrDefault() || eventArgs.Row.PaymentReleased.GetValueOrDefault() || !eventArgs.Row.PaymentPendingProcessing.GetValueOrDefault())
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The {0} customer refund has no active refund transaction. To process the {0} customer refund, open the Payments and Applications (AR302000) form.", (PXErrorLevel) 3, new object[1]
    {
      (object) eventArgs.Row.AdjgRefNbr
    });
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust>>) eventArgs).Cache.RaiseExceptionHandling<ARAdjust.displayRefNbr>((object) eventArgs.Row, (object) null, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARAdjust> eventArgs)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(eventArgs.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || eventArgs.Row.AdjgDocType != "REF" || !string.IsNullOrEmpty(eventArgs.Row.AdjdOrderNbr))
      return;
    PX.Objects.AR.ARPayment arPayment = PX.Objects.AR.ARPayment.PK.Find((PXGraph) this.Base, eventArgs.Row.AdjgDocType, eventArgs.Row.AdjgRefNbr);
    if (string.IsNullOrEmpty(arPayment?.RefTranExtNbr) || this.HasReturnLineForOrigTran(arPayment.ProcessingCenterID, arPayment.RefTranExtNbr))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<ARAdjust>>) eventArgs).Cache.RaiseExceptionHandling<ARAdjust.displayDocType>((object) eventArgs.Row, (object) eventArgs.Row.AdjgDocType, (Exception) new PXSetPropertyException("The original {0} transaction is not related to any of the documents with the items to be returned.", (PXErrorLevel) 5, new object[1]
    {
      (object) arPayment.RefTranExtNbr
    }));
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.curyOrigAdjgAmt, SOAdjust.curyAdjgBilledAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARPayment.curySOApplAmt>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOAdjust.curyAdjgAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.origAdjAmt, SOAdjust.adjBilledAmt>, decimal0>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOAdjust.adjAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.curyOrigAdjdAmt, SOAdjust.curyAdjdBilledAmt>, decimal0>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOAdjust.curyAdjdAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Switch<Case<Where<Current<PX.Objects.AR.ARInvoice.docType>, Equal<ARDocType.creditMemo>>, True>, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.isRefund> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<PX.Objects.AR.ExternalTransaction.tranNumber, InnerJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ExternalTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>, And<PX.Objects.AR.ExternalTransaction.refNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>>, Where<PX.Objects.AR.ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And<PX.Objects.AR.ARPayment.customerID, Equal<Current2<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.ARPayment.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.tranNumber>>>), new Type[] {typeof (PX.Objects.AR.ExternalTransaction.refNbr), typeof (PX.Objects.AR.ARPayment.docDate), typeof (PX.Objects.AR.ExternalTransaction.amount), typeof (PX.Objects.AR.ExternalTransaction.tranNumber)})]
  [PXRestrictor(typeof (Where<Exists<Select2<ARAdjust, InnerJoin<ARTran, On<ARTran.origInvoiceType, Equal<ARAdjust.adjdDocType>, And<ARTran.origInvoiceNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where<ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<ARAdjust.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<ARTran.tranType, Equal<Current2<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current2<PX.Objects.AR.ARInvoice.refNbr>>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.curyAdjdAmt, NotEqual<decimal0>, And<ARTran.curyTranAmt, NotEqual<decimal0>>>>>>>>>>>), "The original {0} transaction is not related to any of the documents with the items to be returned.", new Type[] {typeof (PX.Objects.AR.ExternalTransaction.tranNumber)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.refTranExtNbr> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>, Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<PX.Objects.AR.ARInvoice.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>, And<Where<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, Or<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<SOQuickPayment.pMInstanceID>>>>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.pMInstanceID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOQuickPayment.pMInstanceID>>>>, Coalesce<Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>, InnerJoin<CCProcessingCenterPmntMethodBranch, On<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethodBranch.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And2<Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.posTerminal>, And<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<PX.Objects.AR.ARInvoice.branchID>>, Or<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<AccessInfo.branchID>>>>>, And<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>, Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.processingCenterID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOQuickPayment.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<PX.Objects.AR.ARInvoice.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (PX.Objects.AR.ARInvoice.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>), SuppressCurrencyValidation = false, Required = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.cashAccountID> eventArgs)
  {
  }

  protected override bool CanIncreaseAuthorizedAmount(ARAdjust2 adjust, PX.Objects.AR.ARPayment payment)
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    if (!base.CanIncreaseAuthorizedAmount(adjust, payment) || !(current.DocType == "INV") || current == null)
      return false;
    // ISSUE: explicit non-virtual call
    Decimal? unpaidBalance = __nonvirtual (current.UnpaidBalance);
    Decimal num = 0M;
    return unpaidBalance.GetValueOrDefault() > num & unpaidBalance.HasValue;
  }

  public override void AuthorizePayment(
    ARAdjust2 adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    paymentEntry.ForcePaymentApp = true;
    using (new ForcePaymentAppScope())
      base.AuthorizePayment(adjustment, paymentEntry, paymentTransaction);
  }

  public override void VoidPayment(
    ARAdjust2 adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    paymentEntry.IgnoreNegativeOrderBal = true;
    if (adjustment.IsCCPayment.GetValueOrDefault())
    {
      ARAdjust arAdjust = ((PXSelectBase<ARAdjust>) paymentEntry.Adjustments).Locate((ARAdjust) adjustment);
      if (arAdjust != null)
      {
        if (arAdjust.Hold.GetValueOrDefault())
        {
          arAdjust.Hold = new bool?(false);
          arAdjust = ((PXSelectBase<ARAdjust>) paymentEntry.Adjustments).Update(arAdjust);
          ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).UpdateCurrent();
        }
        PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
        if ((current != null ? (current.IsCCAuthorized.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          arAdjust.CuryAdjgAmt = new Decimal?(0M);
          ((PXSelectBase<ARAdjust>) paymentEntry.Adjustments).Update(arAdjust);
        }
      }
    }
    base.VoidPayment(adjustment, paymentEntry, paymentTransaction);
  }

  public override void CapturePayment(
    ARAdjust2 adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction)
  {
    paymentEntry.IgnoreNegativeOrderBal = true;
    paymentEntry.ForcePaymentApp = true;
    using (new ForcePaymentAppScope())
      base.CapturePayment(adjustment, paymentEntry, paymentTransaction);
  }

  public override void IncreasePayment(
    ARAdjust2 adjustment,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransaction,
    bool capture)
  {
    paymentEntry.ForcePaymentApp = true;
    using (new ForcePaymentAppScope())
      base.IncreasePayment(adjustment, paymentEntry, paymentTransaction, capture);
  }

  protected override void RemoveUnappliedBalance(ARPaymentEntry paymentEntry)
  {
    OrdersToApplyTab applyTabExtension = paymentEntry.GetOrdersToApplyTabExtension(true);
    SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(((IEnumerable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOAdjust>>().Where<PXResult<SOAdjust>>((Func<PXResult<SOAdjust>, bool>) (a =>
    {
      Decimal? curyAdjgAmt = ((PXResult) a).GetItem<SOAdjust>().CuryAdjgAmt;
      Decimal num = 0M;
      return curyAdjgAmt.GetValueOrDefault() > num & curyAdjgAmt.HasValue;
    })).SingleOrDefault<PXResult<SOAdjust>>());
    if (soAdjust != null)
    {
      ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).SetValueExt<SOAdjust.curyAdjgAmt>(soAdjust, (object) 0M);
      ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).Update(soAdjust);
    }
    PXFormulaAttribute.CalcAggregate<SOAdjust.curyAdjgAmt>(((PXSelectBase) applyTabExtension.SOAdjustments).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current, false);
    base.RemoveUnappliedBalance(paymentEntry);
  }

  protected override void CheckUnappliedBalance(
    ARPaymentEntry paymentEntry,
    ARAdjust2 arAdjust,
    PX.Objects.AR.ARInvoice arInvoice)
  {
    base.CheckUnappliedBalance(paymentEntry, arAdjust, arInvoice);
    if (arAdjust.AdjdOrderType == null || arAdjust.AdjdOrderNbr == null)
      return;
    OrdersToApplyTab applyTabExtension = paymentEntry.GetOrdersToApplyTabExtension(true);
    SOAdjust soAdjust = (applyTabExtension != null ? GraphHelper.RowCast<SOAdjust>((IEnumerable) ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).Select(Array.Empty<object>())) : (IEnumerable<SOAdjust>) null).Where<SOAdjust>((Func<SOAdjust, bool>) (a => a.AdjdOrderType == arAdjust.AdjdOrderType && a.AdjdOrderNbr == arAdjust.AdjdOrderNbr)).SingleOrDefault<SOAdjust>();
    if (soAdjust == null)
      return;
    Decimal? curyAdjdAmt = soAdjust.CuryAdjdAmt;
    Decimal num1 = 0M;
    if (curyAdjdAmt.GetValueOrDefault() > num1 & curyAdjdAmt.HasValue)
    {
      PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
      string displayName1 = ARDocType.GetDisplayName(current.DocType ?? "PMT");
      string displayName2 = ARDocType.GetDisplayName(arInvoice.DocType ?? "INV");
      Decimal num2 = PXCurrencyAttribute.Round(((PXGraph) paymentEntry).Caches[typeof (PX.Objects.AR.ARInvoice)], (object) arInvoice, soAdjust.CuryAdjgAmt.GetValueOrDefault(), CMPrecision.TRANCURY);
      throw new PXException("The {0} document with the {1} ref. number has the balance of {2} {3} that is applied to the related {4} document with the {5} ref. number. You must transfer the application amount from the related sales order to the current {6} document with the {7} ref. number before increasing the authorized amount of the {8} document with the {9} ref. number.", new object[10]
      {
        (object) displayName1,
        (object) current.RefNbr,
        (object) num2,
        (object) current.CuryID,
        (object) arAdjust.AdjdOrderType,
        (object) arAdjust.AdjdOrderNbr,
        (object) displayName2,
        (object) arInvoice.RefNbr,
        (object) displayName1,
        (object) current.RefNbr
      });
    }
  }

  protected override PXSelectBase<ARAdjust2> GetAdjustView()
  {
    return (PXSelectBase<ARAdjust2>) this.Base.Adjustments;
  }

  protected override PXSelectBase<ARAdjust> GetAdjustView(ARPaymentEntry paymentEntry)
  {
    return (PXSelectBase<ARAdjust>) paymentEntry.Adjustments;
  }

  protected override ARSetup GetARSetup() => ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;

  protected override CustomerClass GetCustomerClass()
  {
    return ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
  }

  protected override void SetCurrentDocument(PX.Objects.AR.ARInvoice document)
  {
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) document.RefNbr, new object[1]
    {
      (object) document.DocType
    }));
  }

  protected override void AddAdjust(ARPaymentEntry paymentEntry, PX.Objects.AR.ARInvoice document)
  {
    ARAdjust2 arAdjust2_1 = new ARAdjust2();
    arAdjust2_1.AdjdRefNbr = document.RefNbr;
    arAdjust2_1.AdjdDocType = document.DocType;
    arAdjust2_1.Hold = new bool?(true);
    ARAdjust2 arAdjust2_2 = arAdjust2_1;
    ((PXSelectBase<ARAdjust>) paymentEntry.Adjustments).Insert((ARAdjust) arAdjust2_2);
    PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) ((PXGraph) paymentEntry).Caches[typeof (PX.Objects.AR.ARInvoice)].Locate((object) document);
    PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.FireOnPropertyChanged<PX.Objects.AR.ARInvoice.pendingProcessing>((PXGraph) paymentEntry, (PX.Objects.AR.ARInvoice.Events) arInvoice);
  }

  protected override void VerifyAdjustments(ARPaymentEntry paymentEntry, string actionName)
  {
    PX.Objects.AR.ARPayment current = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    if (!this.IsMultipleApplications(paymentEntry))
      return;
    switch (actionName)
    {
      case "CaptureDocumentPayment":
        if (current.DocType == "PMT")
          throw new PXException("The {0} payment has multiple applications and cannot be captured for a separate document. To capture the payment, use the Actions > Capture action on the Payments and Applications (AR302000) form.", new object[1]
          {
            (object) current.RefNbr
          });
        throw new PXException("The {0} prepayment has multiple applications and cannot be captured for a separate document. To capture the prepayment, use the Actions > Capture action on the Payments and Applications (AR302000) form.", new object[1]
        {
          (object) current.RefNbr
        });
      case "IncreaseAuthorizedAmount":
        throw new PXException("The {0} document with the {1} ref. number has multiple applications and cannot be captured for a separate document. To capture the payment, click Capture on the More menu of the Payments and Applications (AR302000) form. To increase the authorized amount without capture, click Increase.", new object[2]
        {
          (object) ARDocType.GetDisplayName(current.DocType ?? "PMT"),
          (object) current.RefNbr
        });
      default:
        if (current.DocType == "PMT")
          throw new PXException("The {0} payment has multiple applications and cannot be voided for a separate document. To void the payment, click Actions > Void Card Payment on the Payments and Applications (AR302000) form.", new object[1]
          {
            (object) current.RefNbr
          });
        throw new PXException("The {0} prepayment has multiple applications and cannot be voided for a separate document. To void the prepayment, click Actions > Void Card Payment on the Payments and Applications (AR302000) form.", new object[1]
        {
          (object) current.RefNbr
        });
    }
  }

  protected override string GetDocumentDescr(PX.Objects.AR.ARInvoice document) => document.DocDesc;

  protected override string GetDocumentType(PX.Objects.AR.ARInvoice document) => document.DocType;

  protected override string GetDocumentNbr(PX.Objects.AR.ARInvoice document) => document.RefNbr;

  protected override bool CanVoid(ARAdjust2 adjust, PX.Objects.AR.ARPayment payment)
  {
    if (!base.CanVoid(adjust, payment))
      return false;
    return !payment.Released.GetValueOrDefault() || adjust.AdjdOrderType == null;
  }

  protected override Type GetPaymentMethodField() => typeof (PX.Objects.SO.SOInvoice.paymentMethodID);

  protected override bool IsCashSale()
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    return current != null && EnumerableExtensions.IsIn<string>(current.DocType, "CSL", "RCS");
  }

  protected override string GetCCPaymentIsNotSupportedMessage()
  {
    return "The credit card payment method is not supported in invoices of the Cash Sales and Cash Return type.";
  }

  protected override Type GetDocumentPMInstanceIDField() => typeof (PX.Objects.AR.ARInvoice.pMInstanceID);

  public override void CopyError(ARAdjust2 errorAdjustment, Exception exception)
  {
    if (this.GetCurrent<PX.Objects.AR.ARInvoice>()?.DocType == "CRM")
      this.CopyError<ARAdjust>("DisplayDocType", (ICreatePaymentAdjust) errorAdjustment, exception);
    else
      base.CopyError(errorAdjustment, exception);
  }

  protected override bool HasReturnLineForOrigTran(string procCenterID, string tranNumber)
  {
    if (string.IsNullOrEmpty(procCenterID))
      throw new PXArgumentException(nameof (procCenterID));
    if (string.IsNullOrEmpty(tranNumber))
      throw new PXArgumentException(nameof (tranNumber));
    return PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXViewOf<ARTran>.BasedOn<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<ARTran.origInvoiceType>>>>>.And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<ARTran.origInvoiceNbr>>>>, FbqlJoins.Inner<PX.Objects.AR.ExternalTransaction>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ExternalTransaction.docType, Equal<ARAdjust.adjgDocType>>>>>.And<BqlOperand<PX.Objects.AR.ExternalTransaction.refNbr, IBqlString>.IsEqual<ARAdjust.adjgRefNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, Equal<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<ARTran.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ExternalTransaction.processingCenterID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.AR.ExternalTransaction.tranNumber, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<ARAdjust.curyAdjdAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.And<BqlOperand<ARTran.curyTranAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) procCenterID,
      (object) tranNumber
    })) != null;
  }

  protected override bool ValidateCCRefundOrigTransaction() => true;

  protected virtual void MarkRefundAdjUpdatedForValidation(ARTran line)
  {
    if (!(line.TranType == "CRM") || string.IsNullOrEmpty(line.OrigInvoiceNbr))
      return;
    foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Base.Adjustments_1).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Adjustments_1).Cache, (object) PXResult<ARAdjust>.op_Implicit(pxResult), true);
  }

  protected override long? GetOrigDocumentCurrencyInfoID()
  {
    return ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current?.CuryInfoID;
  }

  protected override bool CanIncreaseAndCapture => true;

  protected override PX.Objects.SO.SOOrder GetDocIfAuthorizeRemainderRequired(
    ARPaymentEntry paymentEntry,
    ARAdjust2 arAdjust)
  {
    if (arAdjust.AdjdOrderType == null || arAdjust.AdjdOrderNbr == null)
      return (PX.Objects.SO.SOOrder) null;
    OrdersToApplyTab applyTabExtension = paymentEntry.GetOrdersToApplyTabExtension(true);
    IEnumerable<SOAdjust> source = applyTabExtension != null ? GraphHelper.RowCast<SOAdjust>((IEnumerable) ((PXSelectBase<SOAdjust>) applyTabExtension.SOAdjustments).Select(Array.Empty<object>())) : (IEnumerable<SOAdjust>) null;
    if (source == null)
      return (PX.Objects.SO.SOOrder) null;
    SOAdjust soAdjust = source.Where<SOAdjust>((Func<SOAdjust, bool>) (a => a.AdjdOrderType == arAdjust.AdjdOrderType && a.AdjdOrderNbr == arAdjust.AdjdOrderNbr)).SingleOrDefault<SOAdjust>();
    if (soAdjust == null)
      return (PX.Objects.SO.SOOrder) null;
    PX.Objects.SO.SOOrder remainderRequired = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoin<SOOrderType, On<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>, And<SOOrderType.authorizeRemainderAfterPartialCapture, Equal<True>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) soAdjust.AdjdOrderType,
      (object) soAdjust.AdjdOrderNbr
    }));
    if (remainderRequired != null && remainderRequired.Status != "C" && remainderRequired.OpenDoc.GetValueOrDefault())
    {
      Decimal? unbilledOrderTotal = remainderRequired.CuryUnbilledOrderTotal;
      Decimal num = 0M;
      if (unbilledOrderTotal.GetValueOrDefault() > num & unbilledOrderTotal.HasValue)
        return remainderRequired;
    }
    return (PX.Objects.SO.SOOrder) null;
  }
}
