// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.CreatePaymentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class CreatePaymentExt : CreatePaymentExtBase<SOOrderEntry, PX.Objects.SO.SOOrder, SOAdjust>
{
  private bool isReqPrepaymentCalculationInProgress;
  public PXAction<PX.Objects.SO.SOOrder> createOrderPrepayment;
  public PXAction<PX.Objects.SO.SOOrder> deletePayment;
  public PXAction<PX.Objects.SO.SOOrder> deleteRefund;

  public override void Initialize()
  {
    base.Initialize();
    if (!((PXSelectBase) this.GetAdjustView()).Cache.Fields.Contains("CanDeletePayment"))
    {
      ((PXSelectBase) this.GetAdjustView()).Cache.Fields.Add("CanDeletePayment");
      ((PXGraph) this.Base).FieldSelecting.AddHandler(typeof (SOAdjust), "CanDeletePayment", this.CreateVoidCaptureFieldSelecting("CanDeletePayment", new Func<SOAdjust, PX.Objects.AR.ARPayment, bool>(this.CanDeletePayment)));
    }
    if (((PXSelectBase) this.GetAdjustView()).Cache.Fields.Contains("CanDeleteRefund"))
      return;
    ((PXSelectBase) this.GetAdjustView()).Cache.Fields.Add("CanDeleteRefund");
    ((PXGraph) this.Base).FieldSelecting.AddHandler(typeof (SOAdjust), "CanDeleteRefund", this.CreateVoidCaptureFieldSelecting("CanDeleteRefund", new Func<SOAdjust, PX.Objects.AR.ARPayment, bool>(this.CanDeleteRefund)));
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", Tooltip = "Create Payment", DisplayOnMainToolbar = false, PopupCommand = "syncPaymentTransaction")]
  protected override IEnumerable CreateDocumentPayment(PXAdapter adapter)
  {
    this.CanMakeAPayment();
    this.CheckTermsInstallmentType();
    return base.CreateDocumentPayment(adapter);
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", Tooltip = "Create Prepayment", DisplayOnMainToolbar = false, PopupCommand = "syncPaymentTransaction")]
  protected virtual IEnumerable CreateOrderPrepayment(PXAdapter adapter)
  {
    this.CanMakeAPayment();
    this.CheckTermsInstallmentType();
    if (this.AskCreatePaymentDialog("Create Prepayment") == 1)
      this.CreatePayment(((PXSelectBase<SOQuickPayment>) this.QuickPayment).Current, "PPM", false);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Delete Payment", DisplayOnMainToolbar = false)]
  protected virtual IEnumerable DeletePayment(PXAdapter adapter)
  {
    ((PXAction) this.Base.Save).Press();
    this.ProcessDeletePayment(((PXSelectBase<SOAdjust>) this.Base.Adjustments).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Delete Refund", DisplayOnMainToolbar = false)]
  protected virtual IEnumerable DeleteRefund(PXAdapter adapter)
  {
    ((PXAction) this.Base.Save).Press();
    this.ProcessDeletePayment(((PXSelectBase<SOAdjust>) this.Base.Adjustments).Current);
    return adapter.Get();
  }

  public virtual void CheckTermsInstallmentType()
  {
    PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.TermsID);
    if (terms != null && terms.InstallmentType != "S")
      throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
  }

  public virtual void CanMakeAPayment()
  {
    ARTran arTran = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXViewOf<ARTran>.BasedOn<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.sOOrderNbr, Equal<P.AsString>>>>, And<BqlOperand<ARTran.sOOrderType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<ARTran.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<ARTran.invtMult, IBqlShort>.IsNotEqual<short0>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr,
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType
    }));
    if (arTran != null)
      throw new PXSetPropertyException("A payment or prepayment cannot be created because there is a not released SO invoice which is linked directly to the sales order. To create a payment or prepayment, release the {0} invoice first.", new object[1]
      {
        (object) arTran.RefNbr
      });
  }

  public virtual SOOrderStateForPayments GetDocumentState(PXCache cache, PX.Objects.SO.SOOrder order)
  {
    if (order == null)
      throw new PXArgumentException(nameof (order));
    SOOrderStateForPayments stateForPayments1 = new SOOrderStateForPayments();
    stateForPayments1.IsMixedOrder = order.Behavior == "MO";
    stateForPayments1.Inserted = GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base).GetStatus(order) == 2;
    bool? nullable1 = (bool?) ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current?.CanHavePayments;
    stateForPayments1.PaymentsAllowed = nullable1.GetValueOrDefault();
    SOOrderType current1 = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current;
    bool? nullable2;
    if (current1 == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current1.CanHaveRefunds;
    nullable1 = nullable2;
    stateForPayments1.RefundsAllowed = nullable1.GetValueOrDefault();
    stateForPayments1.PaymentType = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this.Base, order.PaymentMethodID)?.PaymentType;
    SOOrderStateForPayments documentState = stateForPayments1;
    SOOrderStateForPayments stateForPayments2 = documentState;
    Decimal? curyOrderTotal;
    int num1;
    if (documentState.IsMixedOrder)
    {
      curyOrderTotal = order.CuryOrderTotal;
      Decimal num2 = 0M;
      num1 = curyOrderTotal.GetValueOrDefault() >= num2 & curyOrderTotal.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    stateForPayments2.PaymentsAllowedByAmount = num1 != 0;
    SOOrderStateForPayments stateForPayments3 = documentState;
    nullable1 = order.Completed;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = order.Cancelled;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = order.Approved;
        if (((int) nullable1 ?? (order.DontApprove.GetValueOrDefault() ? 1 : 0)) == 0)
        {
          nullable1 = order.Hold;
          if (!nullable1.GetValueOrDefault())
            goto label_14;
        }
        if (this.Base.IsAddingPaymentsAllowed(order, ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current))
        {
          num3 = !documentState.IsMixedOrder ? 1 : (EnumerableExtensions.IsIn<string>(order.Status, "N", "R") ? 1 : 0);
          goto label_15;
        }
      }
    }
label_14:
    num3 = 0;
label_15:
    stateForPayments3.DocStatusAllowsPayment = num3 != 0;
    documentState.CreatePaymentEnabled = documentState.PaymentsAllowed && !documentState.Inserted && documentState.DocStatusAllowsPayment && documentState.PaymentsAllowedByAmount;
    SOOrderStateForPayments stateForPayments4 = documentState;
    int num4;
    if (documentState.IsMixedOrder)
    {
      curyOrderTotal = order.CuryOrderTotal;
      Decimal num5 = 0M;
      num4 = curyOrderTotal.GetValueOrDefault() < num5 & curyOrderTotal.HasValue ? 1 : 0;
    }
    else
      num4 = 1;
    stateForPayments4.RefundsAllowedByAmount = num4 != 0;
    SOOrderStateForPayments stateForPayments5 = documentState;
    int num6;
    if (documentState.RefundsAllowed && !documentState.Inserted && order.Status == "N")
    {
      SOOrderType current2 = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current;
      int num7;
      if (current2 == null)
      {
        num7 = 0;
      }
      else
      {
        nullable1 = current2.AllowRefundBeforeReturn;
        num7 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num7 != 0)
      {
        num6 = documentState.RefundsAllowedByAmount ? 1 : 0;
        goto label_25;
      }
    }
    num6 = 0;
label_25:
    stateForPayments5.CreateRefundEnabled = num6 != 0;
    documentState.ImportPaymentEnabled = documentState.CreatePaymentEnabled && order.PaymentMethodID != null && (documentState.PaymentType == "CCD" || documentState.PaymentType == "EFT");
    documentState.IsReqPrepaymentVisible = this.GetRequiredPrepaymentEnabled(order);
    SOOrderStateForPayments stateForPayments6 = documentState;
    int? childLineCntr = order.ChildLineCntr;
    int num8 = 0;
    int num9 = childLineCntr.GetValueOrDefault() > num8 & childLineCntr.HasValue ? 1 : 0;
    stateForPayments6.ChildExists = num9 != 0;
    SOOrderStateForPayments stateForPayments7 = documentState;
    curyOrderTotal = order.CuryOrderTotal;
    Decimal num10 = 0M;
    int num11;
    if (curyOrderTotal.GetValueOrDefault() >= num10 & curyOrderTotal.HasValue)
    {
      nullable1 = order.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = order.DontApprove;
        num11 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num11 = 1;
    }
    else
      num11 = 0;
    stateForPayments7.IsOverridePrepaymentEnabled = num11 != 0;
    SOOrderStateForPayments stateForPayments8 = documentState;
    int num12;
    if (documentState.IsOverridePrepaymentEnabled)
    {
      nullable1 = order.OverridePrepayment;
      num12 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num12 = 0;
    stateForPayments8.IsPrepaymentReqEnabled = num12 != 0;
    documentState.IsIncreaseAuthorizedAmountEnabled = PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && (order.Behavior == "SO" || order.Behavior == "IN" || order.Behavior == "RM" || order.Behavior == "BL");
    return documentState;
  }

  protected override void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> eventArgs)
  {
    base._(eventArgs);
    if (eventArgs.Row == null)
      return;
    SOOrderStateForPayments docState = this.GetDocumentState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, eventArgs.Row);
    ((PXAction) this.createDocumentPayment).SetVisible(docState.PaymentsAllowed);
    ((PXAction) this.createDocumentPayment).SetEnabled(docState.CreatePaymentEnabled);
    ((PXAction) this.createDocumentRefund).SetVisible(docState.RefundsAllowed);
    ((PXAction) this.createDocumentRefund).SetEnabled(docState.CreateRefundEnabled);
    ((PXAction) this.createOrderPrepayment).SetVisible(docState.PaymentsAllowed && !docState.IsMixedOrder);
    ((PXAction) this.createOrderPrepayment).SetEnabled(docState.CreatePaymentEnabled && !docState.IsMixedOrder);
    ((PXAction) this.importDocumentPayment).SetVisible(docState.PaymentsAllowed && !docState.IsMixedOrder);
    ((PXAction) this.importDocumentPayment).SetEnabled(docState.ImportPaymentEnabled && !docState.IsMixedOrder);
    ((PXAction) this.captureDocumentPayment).SetVisible(docState.PaymentsAllowed && !docState.IsMixedOrder);
    ((PXAction) this.voidDocumentPayment).SetVisible(docState.PaymentsAllowed);
    ((PXAction) this.deletePayment).SetVisible(docState.IsMixedOrder);
    ((PXAction) this.deleteRefund).SetVisible(docState.IsMixedOrder);
    ((PXAction) this.increaseAuthorizedAmount).SetVisible(docState.PaymentsAllowed && docState.IsIncreaseAuthorizedAmountEnabled);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>((Action<PXUIFieldAttribute>) (a => a.Enabled = docState.IsPrepaymentReqEnabled)).SameFor<PX.Objects.SO.SOOrder.prepaymentReqPct>();
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    if (docState.IsReqPrepaymentVisible)
    {
      Decimal? curyOrderTotal = eventArgs.Row.CuryOrderTotal;
      Decimal num = 0M;
      if (curyOrderTotal.GetValueOrDefault() < num & curyOrderTotal.HasValue)
        propertyException1 = new PXSetPropertyException("The amount is set to 0 because the Order Total is negative.", (PXErrorLevel) 2);
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>((object) eventArgs.Row, (object) 0M, (Exception) propertyException1);
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.overridePrepayment>((Action<PXUIFieldAttribute>) (a => a.Enabled = docState.IsOverridePrepaymentEnabled));
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOOrder.prepaymentReqSatisfied>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row, false);
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<PX.Objects.SO.SOOrder.prepaymentReqPct>((Action<PXUIFieldAttribute>) (a => a.Visible = docState.IsReqPrepaymentVisible)).SameFor<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>();
    chained = chained.SameFor<PX.Objects.SO.SOOrder.overridePrepayment>();
    chained.SameFor<PX.Objects.SO.SOOrder.prepaymentReqSatisfied>();
    ARPaymentType.SOListAttribute.SetPaymentList<SOAdjust.adjgDocType>(((PXSelectBase) this.Base.Adjustments).Cache, docState.RefundsAllowed && docState.RefundsAllowedByAmount);
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyPaymentTotal>((object) eventArgs.Row, (object) eventArgs.Row.CuryPaymentTotal, (Exception) propertyException2);
    ((PXAction) this.createDocumentPayment).SetTooltip(docState.ChildExists ? "The payment cannot be created because one or multiple child orders are created for this sales order." : "Create Payment");
    ((PXAction) this.createOrderPrepayment).SetTooltip(docState.ChildExists ? "The prepayment cannot be created because one or multiple child orders are created for this sales order." : "Create Prepayment");
    ((PXAction) this.importDocumentPayment).SetTooltip(docState.ChildExists ? "The payment cannot be imported because one or multiple child orders are created for this sales order." : string.Empty);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled> eventArgs)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) eventArgs).NewValue).GetValueOrDefault())
      return;
    SOAdjust paymentLinkToOrder = this.GetPaymentLinkToOrder(eventArgs.Row);
    if (paymentLinkToOrder != null)
      throw new PXException(paymentLinkToOrder.AdjgDocType == "REF" ? "The sales order {0} cannot be canceled because it has the {1} refund applied. Reverse the refund application before you cancel the order." : "The sales order {0} cannot be canceled because it has the {1} payment applied. Reverse the payment application before you cancel the order.", new object[2]
      {
        (object) eventArgs.Row.OrderNbr,
        (object) paymentLinkToOrder.AdjgRefNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOOrder> eventArgs)
  {
    if (this.GetPaymentLinkToOrder(eventArgs.Row) != null)
      throw new PXException("Cannot delete the {0} sales order. There are one or more payments linked to this sales order. To delete the sales order, remove the payment application.", new object[1]
      {
        (object) eventArgs.Row.OrderNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.termsID> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.termsID>>) eventArgs).Cache.SetDefaultExt<PX.Objects.SO.SOOrder.overridePrepayment>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.overridePrepayment> eventArgs)
  {
    bool? overridePrepayment = eventArgs.Row.OverridePrepayment;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.overridePrepayment>, PX.Objects.SO.SOOrder, object>) eventArgs).OldValue;
    if (overridePrepayment.GetValueOrDefault() == oldValue.GetValueOrDefault() & overridePrepayment.HasValue == oldValue.HasValue)
      return;
    bool? nullable = eventArgs.Row.DontApprove;
    if (!nullable.GetValueOrDefault())
    {
      nullable = eventArgs.Row.Approved;
      if (nullable.GetValueOrDefault())
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.overridePrepayment>>) eventArgs).Cache.SetValueExt<PX.Objects.SO.SOOrder.approved>((object) eventArgs.Row, (object) false);
    }
    nullable = eventArgs.Row.OverridePrepayment;
    if (nullable.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.overridePrepayment>>) eventArgs).Cache.SetDefaultExt<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    Decimal? nullable = eventArgs.Row.CuryOrderTotal;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct>, PX.Objects.SO.SOOrder, object>) eventArgs).NewValue = (object) 0M;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct>, PX.Objects.SO.SOOrder, object>) eventArgs).NewValue;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    nullable = newValue;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
      propertyException = (PXSetPropertyException) new PXSetPropertyException<PX.Objects.SO.SOOrder.prepaymentReqPct>("The value of the Prepayment Percent box should be greater than zero.");
    nullable = newValue;
    num2 = 100M;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      propertyException = (PXSetPropertyException) new PXSetPropertyException<PX.Objects.SO.SOOrder.prepaymentReqPct>("The value of the Prepayment Percent box should be less than 100.");
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) eventArgs.Row, (object) newValue, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyPrepaymentReqAmt> eventArgs)
  {
    if (eventArgs.Row == null || !this.GetRequiredPrepaymentEnabled(eventArgs.Row))
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>, PX.Objects.SO.SOOrder, object>) eventArgs).NewValue;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    Decimal? nullable = newValue;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
    {
      propertyException = (PXSetPropertyException) new PXSetPropertyException<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>("The value of the Prepayment Amount box should be greater than zero.");
    }
    else
    {
      nullable = newValue;
      Decimal? curyOrderTotal = eventArgs.Row.CuryOrderTotal;
      if (nullable.GetValueOrDefault() > curyOrderTotal.GetValueOrDefault() & nullable.HasValue & curyOrderTotal.HasValue)
      {
        curyOrderTotal = eventArgs.Row.CuryOrderTotal;
        Decimal num2 = 0M;
        if (curyOrderTotal.GetValueOrDefault() > num2 & curyOrderTotal.HasValue)
          propertyException = (PXSetPropertyException) new PXSetPropertyException<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>("The value of the Prepayment Amount box should be less than or equal to the total amount of the {0} sales order.", new object[1]
          {
            (object) eventArgs.Row.CuryOrderTotal
          });
      }
    }
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>((object) eventArgs.Row, (object) newValue, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct> eventArgs)
  {
    if (eventArgs.Row == null || !eventArgs.Row.OverridePrepayment.GetValueOrDefault())
      return;
    this.SetAmountByPercent(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.prepaymentReqPct>>) eventArgs).Cache, eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PX.Objects.SO.SOOrder row = eventArgs.Row;
    Decimal? oldValue = (Decimal?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>, PX.Objects.SO.SOOrder, object>) eventArgs).OldValue;
    Decimal? nullable = row.CuryOrderTotal;
    Decimal num1 = 0M;
    bool? overridePrepayment;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
    {
      nullable = oldValue;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() >= num2 & nullable.HasValue)
      {
        overridePrepayment = row.OverridePrepayment;
        if (overridePrepayment.GetValueOrDefault())
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache.SetValueExt<PX.Objects.SO.SOOrder.prepaymentReqPctToRestore>((object) row, (object) row.PrepaymentReqPct);
      }
      nullable = row.PrepaymentReqPct;
      Decimal num3 = 0M;
      if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache.SetValueExt<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) row, (object) 0M);
    }
    else
    {
      nullable = oldValue;
      Decimal num4 = 0M;
      if (nullable.GetValueOrDefault() < num4 & nullable.HasValue)
      {
        nullable = row.PrepaymentReqPctToRestore;
        if (nullable.HasValue)
        {
          overridePrepayment = row.OverridePrepayment;
          if (overridePrepayment.GetValueOrDefault())
          {
            ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache.SetValueExt<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) row, (object) row.PrepaymentReqPctToRestore);
            goto label_13;
          }
        }
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache.SetDefaultExt<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) row);
label_13:
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache.SetValueExt<PX.Objects.SO.SOOrder.prepaymentReqPctToRestore>((object) row, (object) null);
      }
    }
    overridePrepayment = eventArgs.Row.OverridePrepayment;
    if (!overridePrepayment.GetValueOrDefault())
      return;
    this.SetAmountByPercent(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyOrderTotal>>) eventArgs).Cache, eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyPrepaymentReqAmt> eventArgs)
  {
    if (eventArgs.Row == null || !eventArgs.Row.OverridePrepayment.GetValueOrDefault() || ((PXGraph) this.Base).IsCopyPasteContext || this.Base.IsCopyOrder)
      return;
    this.SetPercentByAmount(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>>) eventArgs).Cache, eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> eventArgs)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) eventArgs).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt, PX.Objects.SO.SOOrder.curyPaymentOverall, PX.Objects.SO.SOOrder.completed, PX.Objects.SO.SOOrder.curyUnpaidBalance>((object) eventArgs.Row, (object) eventArgs.OldRow))
    {
      Decimal? curyPaymentOverall = eventArgs.Row.CuryPaymentOverall;
      Decimal? nullable = eventArgs.Row.CuryPrepaymentReqAmt;
      if (!(curyPaymentOverall.GetValueOrDefault() >= nullable.GetValueOrDefault() & curyPaymentOverall.HasValue & nullable.HasValue))
      {
        nullable = eventArgs.Row.CuryUnpaidBalance;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          eventArgs.Row.ViolatePrepaymentRequirements((PXGraph) this.Base);
          goto label_5;
        }
      }
      eventArgs.Row.SatisfyPrepaymentRequirements((PXGraph) this.Base);
    }
label_5:
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) eventArgs).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.paymentsNeedValidationCntr>((object) eventArgs.Row, (object) eventArgs.OldRow))
    {
      int? needValidationCntr1 = eventArgs.Row.PaymentsNeedValidationCntr;
      int num = 0;
      if (needValidationCntr1.GetValueOrDefault() == num & needValidationCntr1.HasValue && eventArgs.OldRow.PaymentsNeedValidationCntr.HasValue)
      {
        ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.LostLastPaymentInPendingProcessing))).FireOn((PXGraph) this.Base, eventArgs.Row);
      }
      else
      {
        int? needValidationCntr2 = eventArgs.OldRow.PaymentsNeedValidationCntr;
        int? needValidationCntr3 = eventArgs.Row.PaymentsNeedValidationCntr;
        if (needValidationCntr2.GetValueOrDefault() < needValidationCntr3.GetValueOrDefault() & needValidationCntr2.HasValue & needValidationCntr3.HasValue)
          ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.ObtainedPaymentInPendingProcessing))).FireOn((PXGraph) this.Base, eventArgs.Row);
      }
    }
    if (!eventArgs.Row.CreditHold.GetValueOrDefault() || !eventArgs.Row.IsFullyPaid.GetValueOrDefault())
      return;
    bool? isFullyPaid1 = eventArgs.OldRow.IsFullyPaid;
    bool? isFullyPaid2 = eventArgs.Row.IsFullyPaid;
    if (isFullyPaid1.GetValueOrDefault() == isFullyPaid2.GetValueOrDefault() & isFullyPaid1.HasValue == isFullyPaid2.HasValue)
      return;
    eventArgs.Row.SatisfyCreditLimitByPayment((PXGraph) this.Base);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOLine> eventArgs)
  {
    this.MarkRefundAdjUpdatedForValidation(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOLine> eventArgs)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine>>) eventArgs).Cache.ObjectsEqual<SOLine.orderQty>((object) eventArgs.OldRow, (object) eventArgs.Row))
      return;
    Decimal? orderQty = eventArgs.Row.OrderQty;
    Decimal num = 0M;
    if (!(orderQty.GetValueOrDefault() == num & orderQty.HasValue))
      return;
    this.MarkRefundAdjUpdatedForValidation(eventArgs.Row);
  }

  protected override void _(PX.Data.Events.RowSelected<SOAdjust> eventArgs)
  {
    if (ARPaymentType.DrCr(eventArgs.Row?.AdjgDocType) == "C")
    {
      PXSetPropertyException propertyException = (PXSetPropertyException) null;
      if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>())
      {
        bool? nullable = eventArgs.Row.IsCCPayment;
        if (nullable.GetValueOrDefault())
        {
          nullable = eventArgs.Row.IsCCAuthorized;
          if (!nullable.GetValueOrDefault())
          {
            nullable = eventArgs.Row.IsCCCaptured;
            if (!nullable.GetValueOrDefault())
            {
              nullable = eventArgs.Row.Voided;
              if (!nullable.GetValueOrDefault())
              {
                nullable = eventArgs.Row.Released;
                if (!nullable.GetValueOrDefault())
                {
                  nullable = eventArgs.Row.PaymentReleased;
                  if (!nullable.GetValueOrDefault())
                    propertyException = new PXSetPropertyException("The {0} customer refund has no active refund transaction. To process the {0} customer refund, open the Payments and Applications (AR302000) form.", (PXErrorLevel) 3, new object[1]
                    {
                      (object) eventArgs.Row.AdjgRefNbr
                    });
                }
              }
            }
          }
        }
      }
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) eventArgs).Cache.RaiseExceptionHandling(this.GetPaymentErrorFieldName(), (object) eventArgs.Row, (object) null, (Exception) propertyException);
    }
    else
      base._(eventArgs);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<SOAdjust> eventArgs)
  {
    SOAdjust row = eventArgs.Row;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      Decimal? curyAdjdBilledAmt = row.CuryAdjdBilledAmt;
      Decimal num2 = 0M;
      num1 = curyAdjdBilledAmt.GetValueOrDefault() > num2 & curyAdjdBilledAmt.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      throw new PXException("The {0} payment cannot be removed because it has the nonzero Transferred to Invoice amount.", new object[1]
      {
        (object) eventArgs.Row.AdjgRefNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<SOAdjust> eventArgs)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(eventArgs.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || eventArgs.Row.AdjgDocType != "REF")
      return;
    bool? refundOrigTransaction = eventArgs.Row.ValidateCCRefundOrigTransaction;
    bool flag = false;
    if (refundOrigTransaction.GetValueOrDefault() == flag & refundOrigTransaction.HasValue)
      return;
    PX.Objects.AR.ARPayment arPayment = PX.Objects.AR.ARPayment.PK.Find((PXGraph) this.Base, eventArgs.Row.AdjgDocType, eventArgs.Row.AdjgRefNbr);
    if (string.IsNullOrEmpty(arPayment?.RefTranExtNbr) || this.HasReturnLineForOrigTran(arPayment.ProcessingCenterID, arPayment.RefTranExtNbr))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) eventArgs).Cache.RaiseExceptionHandling<SOAdjust.adjgDocType>((object) eventArgs.Row, (object) eventArgs.Row.AdjgDocType, (Exception) new PXSetPropertyException("The original {0} transaction is not related to any of the documents with the items to be returned.", (PXErrorLevel) 5, new object[1]
    {
      (object) arPayment.RefTranExtNbr
    }));
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where2<Where<Exists<Select<SOOrderType, Where<SOOrderType.orderType, Equal<Current2<PX.Objects.SO.SOOrder.orderType>>, And<SOOrderType.validateCCRefundsOrigTransactions, Equal<False>>>>>>, Or<Where<Exists<Select2<PX.Objects.AR.ARAdjust, InnerJoin<SOLine, On<SOLine.invoiceType, Equal<PX.Objects.AR.ARAdjust.adjdDocType>, And<SOLine.invoiceNbr, Equal<PX.Objects.AR.ARAdjust.adjdRefNbr>>>>, Where<PX.Objects.AR.ARAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<SOLine.orderType, Equal<Current2<PX.Objects.SO.SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current2<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.AR.ARAdjust.voided, NotEqual<True>, And<PX.Objects.AR.ARAdjust.curyAdjdAmt, NotEqual<decimal0>, And<SOLine.curyLineAmt, NotEqual<decimal0>>>>>>>>>>>>>), "The original {0} transaction is not related to any of the documents with the items to be returned.", new Type[] {typeof (PX.Objects.AR.ExternalTransaction.tranNumber)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOQuickPayment.refTranExtNbr> eventArgs)
  {
  }

  protected override bool CanCreatePayment()
  {
    SOOrderStateForPayments documentState = this.GetDocumentState(((PXSelectBase) this.Base.Document).Cache, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
    return documentState != null && documentState.CreatePaymentEnabled;
  }

  protected override PXSelectBase<SOAdjust> GetAdjustView()
  {
    return (PXSelectBase<SOAdjust>) this.Base.Adjustments;
  }

  protected override PXSelectBase<SOAdjust> GetAdjustView(ARPaymentEntry paymentEntry)
  {
    return (PXSelectBase<SOAdjust>) paymentEntry.GetOrdersToApplyTabExtension(true).SOAdjustments;
  }

  protected override ARSetup GetARSetup() => ((PXSelectBase<ARSetup>) this.Base.arsetup).Current;

  protected override CustomerClass GetCustomerClass()
  {
    return ((PXSelectBase<CustomerClass>) this.Base.customerclass).SelectSingle(Array.Empty<object>());
  }

  protected override void SetCurrentDocument(PX.Objects.SO.SOOrder document)
  {
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) document.OrderNbr, new object[1]
    {
      (object) document.OrderType
    }));
  }

  protected override void AddAdjust(ARPaymentEntry paymentEntry, PX.Objects.SO.SOOrder document)
  {
    SOAdjust soAdjust = new SOAdjust()
    {
      AdjdOrderType = document.OrderType,
      AdjdOrderNbr = document.OrderNbr
    };
    ((PXSelectBase<SOAdjust>) paymentEntry.GetOrdersToApplyTabExtension(true).SOAdjustments).Insert(soAdjust);
  }

  protected override void VerifyAdjustments(ARPaymentEntry paymentEntry, string actionName)
  {
    PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PX.Objects.AR.ARPayment current2 = ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current;
    ARPaymentTotals paymentTotals;
    PX.Objects.SO.SOInvoice invoice;
    if (this.IsMultipleApplications(paymentEntry, out paymentTotals, out invoice))
    {
      if (actionName == "CaptureDocumentPayment")
      {
        if (current2.DocType == "PMT")
          throw new PXException("The {0} payment has multiple applications and cannot be captured for a separate document. To capture the payment, use the Actions > Capture action on the Payments and Applications (AR302000) form.", new object[1]
          {
            (object) current2.RefNbr
          });
        throw new PXException("The {0} prepayment has multiple applications and cannot be captured for a separate document. To capture the prepayment, use the Actions > Capture action on the Payments and Applications (AR302000) form.", new object[1]
        {
          (object) current2.RefNbr
        });
      }
      if (current2.DocType == "PMT")
        throw new PXException("The {0} payment has multiple applications and cannot be voided for a separate document. To void the payment, click Actions > Void Card Payment on the Payments and Applications (AR302000) form.", new object[1]
        {
          (object) current2.RefNbr
        });
      throw new PXException("The {0} prepayment has multiple applications and cannot be voided for a separate document. To void the prepayment, click Actions > Void Card Payment on the Payments and Applications (AR302000) form.", new object[1]
      {
        (object) current2.RefNbr
      });
    }
    if (!this.IsPaymentLinkedToInvoiceWithTheSameOrder(paymentTotals, invoice))
      return;
    if (actionName == "CaptureDocumentPayment")
    {
      if (current2.DocType == "PMT")
        throw new PXException("The invoice number {0} is prepared for the sales order {1}. Capture the payment {2} on the Invoices (SO303000) form or on the Payments and Applications (AR302000) form.", new object[3]
        {
          (object) invoice.RefNbr,
          (object) current1.OrderNbr,
          (object) current2.RefNbr
        });
      throw new PXException("The invoice number {0} is prepared for the sales order {1}. Capture the prepayment {2} on the Invoices (SO303000) form or on the Payments and Applications (AR302000) form.", new object[3]
      {
        (object) invoice.RefNbr,
        (object) current1.OrderNbr,
        (object) current2.RefNbr
      });
    }
    if (current2.DocType == "PMT")
      throw new PXException("The invoice number {0} is prepared for the sales order {1}. Void the payment {2} on the Invoices (SO303000) form or on the Payments and Applications (AR302000) form.", new object[3]
      {
        (object) invoice.RefNbr,
        (object) current1.OrderNbr,
        (object) current2.RefNbr
      });
    throw new PXException("The invoice number {0} is prepared for the sales order {1}. Void the prepayment {2} on the Invoices (SO303000) form or on the Payments and Applications (AR302000) form.", new object[3]
    {
      (object) invoice.RefNbr,
      (object) current1.OrderNbr,
      (object) current2.RefNbr
    });
  }

  protected override void PrepareForCreateCCPayment(PX.Objects.SO.SOOrder doc)
  {
    base.PrepareForCreateCCPayment(doc);
    this.CheckTermsInstallmentType();
  }

  protected override bool CanVoid(SOAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    bool flag1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior == "MO";
    if (base.CanVoid(adjust, payment))
    {
      PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.Completed;
        bool flag2 = false;
        num1 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
      }
      if (num1 != 0)
      {
        PX.Objects.SO.SOOrder current2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          nullable = current2.Cancelled;
          bool flag3 = false;
          num2 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
        }
        if (num2 != 0)
          return !flag1 || EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Status, "H", "N", "R");
      }
    }
    return false;
  }

  protected override bool CanCapture(SOAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    if (base.CanCapture(adjust, payment))
    {
      PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      bool? nullable;
      int num;
      if (current1 == null)
      {
        num = 0;
      }
      else
      {
        nullable = current1.Completed;
        bool flag = false;
        num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
      }
      if (num != 0)
      {
        PX.Objects.SO.SOOrder current2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
        if (current2 == null)
          return false;
        nullable = current2.Cancelled;
        bool flag = false;
        return nullable.GetValueOrDefault() == flag & nullable.HasValue;
      }
    }
    return false;
  }

  protected override bool CanIncreaseAuthorizedAmount(SOAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if (base.CanIncreaseAuthorizedAmount(adjust, payment) && current != null)
    {
      bool? nullable = current.Completed;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue && current != null)
      {
        nullable = current.Cancelled;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && current != null)
        {
          // ISSUE: explicit non-virtual call
          Decimal? unpaidBalance = __nonvirtual (current.UnpaidBalance);
          Decimal num = 0M;
          return unpaidBalance.GetValueOrDefault() > num & unpaidBalance.HasValue;
        }
      }
    }
    return false;
  }

  protected override string GetDocumentDescr(PX.Objects.SO.SOOrder document) => document.OrderDesc;

  protected override string GetDocumentType(PX.Objects.SO.SOOrder document) => document.OrderType;

  protected override string GetDocumentNbr(PX.Objects.SO.SOOrder document) => document.OrderNbr;

  protected override Type GetPaymentMethodField() => typeof (PX.Objects.SO.SOOrder.paymentMethodID);

  protected override bool IsCashSale()
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    return current != null && current.IsCashSaleOrder.GetValueOrDefault();
  }

  protected override string GetCCPaymentIsNotSupportedMessage()
  {
    return "The credit card payment method is not supported in sales orders of the Cash Sales and Cash Return type.";
  }

  protected override Type GetDocumentPMInstanceIDField() => typeof (PX.Objects.SO.SOOrder.pMInstanceID);

  protected override Decimal? GetDefaultPaymentAmount(SOQuickPayment qp)
  {
    PX.Objects.SO.SOOrder current = this.GetCurrent<PX.Objects.SO.SOOrder>();
    if ((qp == null || !qp.IsRefund.GetValueOrDefault() ? 0 : (current?.Behavior == "MO" ? 1 : 0)) == 0)
      return base.GetDefaultPaymentAmount(qp);
    Decimal? defaultPaymentAmount = new Decimal?();
    if (qp == null || current == null)
      return defaultPaymentAmount;
    if (qp.CuryID == current.CuryID)
      defaultPaymentAmount = current.CuryUnrefundedBalance;
    else if (qp.CuryID != null)
    {
      Decimal curyval;
      PXCurrencyAttribute.CuryConvCury(((PXSelectBase) this.QuickPayment).Cache, (object) qp, current.UnrefundedBalance.GetValueOrDefault(), out curyval);
      defaultPaymentAmount = new Decimal?(curyval);
    }
    return defaultPaymentAmount;
  }

  protected override bool AllowsAuthorization(SOQuickPayment qp)
  {
    if (!base.AllowsAuthorization(qp))
      return false;
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current;
    return current == null || !current.CanHaveRefunds.GetValueOrDefault();
  }

  protected override string GetExceedingAmountErrorMessage(SOQuickPayment qp)
  {
    return qp.IsRefund.GetValueOrDefault() && this.GetCurrent<PX.Objects.SO.SOOrder>()?.Behavior == "MO" ? "The amount must be less than or equal to the unrefunded balance." : base.GetExceedingAmountErrorMessage(qp);
  }

  protected virtual SOAdjust GetPaymentLinkToOrder(PX.Objects.SO.SOOrder order)
  {
    if (order != null)
    {
      Decimal? curyPaymentTotal = order.CuryPaymentTotal;
      Decimal num1 = 0M;
      if (curyPaymentTotal.GetValueOrDefault() > num1 & curyPaymentTotal.HasValue)
      {
        PXView view = ((PXSelectBase) this.Base.Adjustments_Raw).View;
        object[] objArray1 = new object[1]{ (object) order };
        object[] objArray2 = Array.Empty<object>();
        foreach (PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment> pxResult in view.SelectMultiBound(objArray1, objArray2))
        {
          SOAdjust paymentLinkToOrder = PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
          if (paymentLinkToOrder != null)
          {
            bool? voided = paymentLinkToOrder.Voided;
            bool flag = false;
            if (voided.GetValueOrDefault() == flag & voided.HasValue)
            {
              Decimal? curyAdjdAmt = paymentLinkToOrder.CuryAdjdAmt;
              Decimal num2 = 0M;
              if (curyAdjdAmt.GetValueOrDefault() > num2 & curyAdjdAmt.HasValue)
                return paymentLinkToOrder;
            }
          }
        }
      }
    }
    return (SOAdjust) null;
  }

  protected virtual bool IsPaymentLinkedToInvoiceWithTheSameOrder(
    ARPaymentTotals paymentTotals,
    PX.Objects.SO.SOInvoice invoice)
  {
    return paymentTotals != null && invoice != null && paymentTotals.AdjdOrderType == invoice.SOOrderType && paymentTotals.AdjdOrderNbr == invoice.SOOrderNbr;
  }

  protected virtual bool GetRequiredPrepaymentEnabled(PX.Objects.SO.SOOrder order)
  {
    if (order == null || !order.AllowsRequiredPrepayment.GetValueOrDefault() || order.IsTransferOrder.GetValueOrDefault() || order.TermsID == null)
      return false;
    PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) this.Base, order.TermsID);
    return terms != null && terms.PrepaymentRequired.GetValueOrDefault();
  }

  protected virtual void SetAmountByPercent(PXCache cache, PX.Objects.SO.SOOrder order)
  {
    if (this.isReqPrepaymentCalculationInProgress)
      return;
    try
    {
      this.isReqPrepaymentCalculationInProgress = true;
      Decimal? nullable1 = new Decimal?(0M);
      if (order.PrepaymentReqPct.HasValue)
      {
        Decimal? nullable2 = order.PrepaymentReqPct;
        Decimal num1 = 0M;
        if (!(nullable2.GetValueOrDefault() < num1 & nullable2.HasValue))
        {
          nullable2 = order.PrepaymentReqPct;
          Decimal num2 = 100M;
          if (!(nullable2.GetValueOrDefault() > num2 & nullable2.HasValue))
          {
            nullable2 = order.PrepaymentReqPct;
            Decimal num3 = 0M;
            if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
            {
              nullable2 = order.CuryOrderTotal;
              Decimal? prepaymentReqPct = order.PrepaymentReqPct;
              nullable1 = nullable2.HasValue & prepaymentReqPct.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * prepaymentReqPct.GetValueOrDefault() / 100.0M) : new Decimal?();
              nullable1 = new Decimal?(PXCurrencyAttribute.RoundCury(cache, (object) order, nullable1.Value));
              goto label_7;
            }
            goto label_7;
          }
        }
      }
      nullable1 = new Decimal?();
label_7:
      cache.SetValueExt<PX.Objects.SO.SOOrder.curyPrepaymentReqAmt>((object) order, (object) nullable1);
    }
    finally
    {
      this.isReqPrepaymentCalculationInProgress = false;
    }
  }

  protected virtual void SetPercentByAmount(PXCache cache, PX.Objects.SO.SOOrder order)
  {
    if (this.isReqPrepaymentCalculationInProgress)
      return;
    try
    {
      this.isReqPrepaymentCalculationInProgress = true;
      Decimal? nullable1 = new Decimal?(0M);
      if (order.CuryPrepaymentReqAmt.HasValue)
      {
        Decimal? nullable2 = order.CuryPrepaymentReqAmt;
        Decimal? nullable3 = order.CuryOrderTotal;
        if (!(nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue))
        {
          nullable3 = order.CuryPrepaymentReqAmt;
          Decimal num1 = 0M;
          if (!(nullable3.GetValueOrDefault() < num1 & nullable3.HasValue))
          {
            nullable3 = order.CuryPrepaymentReqAmt;
            Decimal num2 = 0M;
            if (!(nullable3.GetValueOrDefault() == num2 & nullable3.HasValue))
            {
              nullable3 = order.CuryOrderTotal;
              if (nullable3.GetValueOrDefault() != 0M)
              {
                Decimal? prepaymentReqAmt = order.CuryPrepaymentReqAmt;
                Decimal num3 = 100.0M;
                nullable3 = prepaymentReqAmt.HasValue ? new Decimal?(prepaymentReqAmt.GetValueOrDefault() * num3) : new Decimal?();
                nullable2 = order.CuryOrderTotal;
                nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
                nullable2 = nullable1;
                Decimal num4 = 100.0M;
                if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
                  nullable1 = new Decimal?(100.0M);
                nullable1 = new Decimal?(PXCurrencyAttribute.Round(cache, (object) order, nullable1.Value, CMPrecision.TRANCURY, 2));
                goto label_10;
              }
              goto label_10;
            }
            goto label_10;
          }
        }
      }
      nullable1 = new Decimal?();
label_10:
      cache.SetValueExt<PX.Objects.SO.SOOrder.prepaymentReqPct>((object) order, (object) nullable1);
    }
    finally
    {
      this.isReqPrepaymentCalculationInProgress = false;
    }
  }

  protected override bool HasReturnLineForOrigTran(string procCenterID, string tranNumber)
  {
    if (string.IsNullOrEmpty(procCenterID))
      throw new PXArgumentException(nameof (procCenterID));
    if (string.IsNullOrEmpty(tranNumber))
      throw new PXArgumentException(nameof (tranNumber));
    return PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjdDocType, Equal<SOLine.invoiceType>>>>>.And<BqlOperand<PX.Objects.AR.ARAdjust.adjdRefNbr, IBqlString>.IsEqual<SOLine.invoiceNbr>>>>, FbqlJoins.Inner<PX.Objects.AR.ExternalTransaction>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ExternalTransaction.docType, Equal<PX.Objects.AR.ARAdjust.adjgDocType>>>>>.And<BqlOperand<PX.Objects.AR.ExternalTransaction.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARAdjust.adjgRefNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ExternalTransaction.processingCenterID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.AR.ExternalTransaction.tranNumber, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.curyAdjdAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.And<BqlOperand<SOLine.curyLineAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) procCenterID,
      (object) tranNumber
    })) != null;
  }

  protected override bool ValidateCCRefundOrigTransaction()
  {
    SOOrderType soOrderType = PXResultset<SOOrderType>.op_Implicit(PXSetup<SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (soOrderType == null)
      return true;
    bool? origTransactions = soOrderType.ValidateCCRefundsOrigTransactions;
    bool flag = false;
    return !(origTransactions.GetValueOrDefault() == flag & origTransactions.HasValue);
  }

  protected virtual void MarkRefundAdjUpdatedForValidation(SOLine line)
  {
    if (!((bool?) ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current?.CanHaveRefunds).GetValueOrDefault())
      return;
    foreach (PXResult<SOAdjust> pxResult in ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Adjustments).Cache, (object) PXResult<SOAdjust>.op_Implicit(pxResult), true);
  }

  protected virtual bool CanDeletePayment(SOAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    bool flag = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior == "MO";
    return ((!EnumerableExtensions.IsIn<string>(adjust.AdjgDocType, "PMT", "PPM") || payment == null ? 0 : (payment.Status != "N" ? 1 : 0)) & (flag ? 1 : 0)) != 0 && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Status, "H", "N", "R");
  }

  protected virtual bool CanDeleteRefund(SOAdjust adjust, PX.Objects.AR.ARPayment payment)
  {
    bool flag = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior == "MO";
    return ((!(adjust.AdjgDocType == "REF") || payment == null ? 0 : (payment.Status != "N" ? 1 : 0)) & (flag ? 1 : 0)) != 0 && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Status, "H", "N", "R");
  }

  protected virtual void ProcessDeletePayment(SOAdjust adjust)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXGraph) instance).Clear();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) adjust.AdjgRefNbr, new object[1]
    {
      (object) adjust.AdjgDocType
    }));
    this.PressButtonIfEnabled((PXGraph) instance, "Delete");
    ((PXAction) this.Base.Cancel).Press();
  }

  [PXOverride]
  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    Action<bool, List<Command>, List<Container>> baseMethod)
  {
    string[] strArray = new string[3]
    {
      "OverridePrepayment",
      "PrepaymentReqPct",
      "CuryPrepaymentReqAmt"
    };
    foreach (string str in strArray)
    {
      string field = str;
      int index = script.FindIndex((Predicate<Command>) (command => field.Equals(command.FieldName, StringComparison.OrdinalIgnoreCase)));
      if (index != -1)
      {
        Command command = script[index];
        Container container = containers[index];
        script.Remove(command);
        containers.Remove(container);
        script.Add(command);
        containers.Add(container);
      }
    }
    if (baseMethod == null)
      return;
    baseMethod(isImportSimple, script, containers);
  }

  protected override long? GetOrigDocumentCurrencyInfoID()
  {
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.CuryInfoID;
  }

  protected override bool CanIncreaseAndCapture => false;
}
