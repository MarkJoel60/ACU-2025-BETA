// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.MobileCreatePaymentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class MobileCreatePaymentExt : PXGraphExtension<CreatePaymentExt, SOOrderEntry>
{
  public PXAction<PX.Objects.SO.SOOrder> mobileCreatePayment;
  public PXAction<PX.Objects.SO.SOOrder> mobileCreatePrepayment;

  [PXUIField]
  [PXButton]
  protected virtual void MobileCreatePayment()
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current != null)
    {
      this.Base1.CheckTermsInstallmentType();
      ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Save).Press();
      PXGraph target;
      this.MobileCreatePaymentProc(((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current, out target);
      throw new PXPopupRedirectException(target, "New Payment", true);
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void MobileCreatePrepayment()
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current != null)
    {
      this.Base1.CheckTermsInstallmentType();
      ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Save).Press();
      PXGraph target;
      this.MobileCreatePaymentProc(((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current, out target, "PPM");
      throw new PXPopupRedirectException(target, "New Payment", true);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool flag = ((PXSelectBase<SOOrderType>) ((PXGraphExtension<SOOrderEntry>) this).Base.soordertype).Current.CanHavePayments.GetValueOrDefault() && ((PXGraphExtension<SOOrderEntry>) this).Base.IsAddingPaymentsAllowed(eventArgs.Row, ((PXSelectBase<SOOrderType>) ((PXGraphExtension<SOOrderEntry>) this).Base.soordertype).Current);
    ((PXAction) this.mobileCreatePayment).SetEnabled(flag && ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.GetStatus((object) eventArgs.Row) != 2);
    ((PXAction) this.mobileCreatePrepayment).SetEnabled(flag && ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.GetStatus((object) eventArgs.Row) != 2);
  }

  public virtual void MobileCreatePaymentProc(
    PX.Objects.SO.SOOrder order,
    out PXGraph target,
    string paymentType = "PMT")
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    target = (PXGraph) instance;
    ((PXGraph) instance).Clear();
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    arPayment1.DocType = paymentType;
    PX.Objects.AR.ARPayment arPayment2 = arPayment1;
    ((PXSelectBase<ARSetup>) instance.arsetup).Current.HoldEntry = new bool?(false);
    OpenPeriodAttribute.SetThrowErrorExternal<PX.Objects.AR.ARPayment.adjFinPeriodID>(((PXSelectBase) instance.Document).Cache, true);
    PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment2));
    OpenPeriodAttribute.SetThrowErrorExternal<PX.Objects.AR.ARPayment.adjFinPeriodID>(((PXSelectBase) instance.Document).Cache, false);
    copy.CustomerID = order.CustomerID;
    copy.CustomerLocationID = order.CustomerLocationID;
    copy.PaymentMethodID = order.PaymentMethodID;
    copy.PMInstanceID = order.PMInstanceID;
    copy.CuryOrigDocAmt = new Decimal?(0M);
    copy.DocDesc = order.OrderDesc;
    copy.CashAccountID = order.CashAccountID;
    PX.Objects.AR.ARPayment payment = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(copy);
    this.MobileInsertSOAdjustments(order, instance, payment);
    Decimal? curyOrigDocAmt = payment.CuryOrigDocAmt;
    Decimal num = 0M;
    if (!(curyOrigDocAmt.GetValueOrDefault() == num & curyOrigDocAmt.HasValue))
      return;
    payment.CuryOrigDocAmt = payment.CurySOApplAmt;
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(payment);
  }

  protected virtual void MobileInsertSOAdjustments(
    PX.Objects.SO.SOOrder order,
    ARPaymentEntry docgraph,
    PX.Objects.AR.ARPayment payment)
  {
    SOAdjust soAdjust = new SOAdjust()
    {
      AdjdOrderType = order.OrderType,
      AdjdOrderNbr = order.OrderNbr
    };
    try
    {
      ((PXSelectBase<SOAdjust>) docgraph.GetOrdersToApplyTabExtension(true).SOAdjustments).Insert(soAdjust);
    }
    catch (PXSetPropertyException ex)
    {
      payment.CuryOrigDocAmt = new Decimal?(0M);
    }
  }
}
