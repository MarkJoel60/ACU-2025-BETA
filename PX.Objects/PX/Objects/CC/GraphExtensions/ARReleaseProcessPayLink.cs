// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARReleaseProcessPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARReleaseProcessPayLink : PXGraphExtension<ARReleaseProcess>
{
  private const string slotKey = "ARReleaseProcessDoNotSyncFlag";

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  public static void ActivateDoNotSyncFlag()
  {
    PXContext.SetSlot<bool>("ARReleaseProcessDoNotSyncFlag", true);
  }

  public static void ClearDoNotSyncFlag() => PXContext.ClearSlot("ARReleaseProcessDoNotSyncFlag");

  [PXOverride]
  public virtual Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> ProcessAdjustments(
    JournalEntry je,
    PXResultset<ARAdjust> adjustments,
    PX.Objects.AR.ARRegister paymentRegister,
    PX.Objects.AR.ARPayment payment,
    PX.Objects.AR.Customer paymentCustomer,
    PX.Objects.CM.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury,
    ARReleaseProcessPayLink.ProcessAdjustmentsDelegate baseMethod)
  {
    Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> tuple = baseMethod(je, adjustments, paymentRegister, payment, paymentCustomer, new_info, paycury);
    foreach (PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AR.ARRegister, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARPayment, ARTran> adjustment in adjustments)
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AR.ARRegister, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARPayment, ARTran>.op_Implicit(adjustment);
      ARInvoicePayLink extension = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARInvoice)].GetExtension<ARInvoicePayLink>((object) arInvoice);
      if (extension.PayLinkID.HasValue)
      {
        CCPayLink payLink = PXResultset<CCPayLink>.op_Implicit(PXSelectBase<CCPayLink, PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Required<CCPayLink.payLinkID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) extension.PayLinkID
        }));
        Decimal? amount = payLink.Amount;
        Decimal? curyDocBal = arInvoice.CuryDocBal;
        if (!(amount.GetValueOrDefault() == curyDocBal.GetValueOrDefault() & amount.HasValue == curyDocBal.HasValue))
        {
          bool? needSync = payLink.NeedSync;
          bool flag = false;
          if (needSync.GetValueOrDefault() == flag & needSync.HasValue)
          {
            ARAdjust adjust = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AR.ARRegister, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARPayment, ARTran>.op_Implicit(adjustment);
            this.SetSyncFlagIfNeeded(payment, adjust, payLink);
          }
        }
      }
    }
    return tuple;
  }

  [PXOverride]
  public virtual void Persist(ARReleaseProcessPayLink.PersistDelegate baseMethod)
  {
    PX.Objects.AR.ARRegister current1 = ((PXSelectBase<PX.Objects.AR.ARRegister>) this.Base.ARDocument).Current;
    SOAdjust current2 = ((PXSelectBase<SOAdjust>) this.Base.soAdjust).Current;
    PX.Objects.SO.SOOrder current3 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.soOrder).Current;
    if (current2 != null && current1 != null && current3 != null && current2.AdjgRefNbr == current1.RefNbr && current2.AdjdOrderNbr == current3.OrderNbr)
    {
      SOOrderPayLink extension = ((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOOrder)].GetExtension<SOOrderPayLink>((object) current3);
      if (extension.PayLinkID.HasValue)
      {
        CCPayLink payLink = PXResultset<CCPayLink>.op_Implicit(PXSelectBase<CCPayLink, PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Required<CCPayLink.payLinkID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) extension.PayLinkID
        }));
        Decimal? amount = payLink.Amount;
        Decimal? curyUnpaidBalance = current3.CuryUnpaidBalance;
        if (!(amount.GetValueOrDefault() == curyUnpaidBalance.GetValueOrDefault() & amount.HasValue == curyUnpaidBalance.HasValue))
        {
          bool? needSync = payLink.NeedSync;
          bool flag = false;
          if (needSync.GetValueOrDefault() == flag & needSync.HasValue)
            this.SetSyncFlagIfNeeded(PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) this.Base.ARPayment_DocType_RefNbr).Select(new object[3]
            {
              (object) current1.DocType,
              (object) current1.RefNbr,
              (object) current1.CustomerID
            })), payLink);
        }
      }
    }
    ((PXGraph) this.Base).Caches[typeof (CCPayLink)].Persist((PXDBOperation) 1);
    baseMethod();
  }

  private void SetSyncFlagIfNeeded(PX.Objects.AR.ARPayment payment, CCPayLink payLink)
  {
    if (!PayLinkHelper.PayLinkOpen(payLink))
      return;
    if (payment != null && payment.CCActualExternalTransactionID.HasValue)
    {
      if (((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.transactionID, Equal<Required<ExternalTransaction.transactionID>>, And<ExternalTransaction.payLinkID, Equal<Required<ExternalTransaction.payLinkID>>>>>((PXGraph) this.Base)).Any<ExternalTransaction>((object) payment.CCActualExternalTransactionID, (object) payLink.PayLinkID))
        return;
      payLink.NeedSync = new bool?(true);
      ((PXGraph) this.Base).Caches[typeof (CCPayLink)].Update((object) payLink);
    }
    else
    {
      payLink.NeedSync = new bool?(true);
      ((PXGraph) this.Base).Caches[typeof (CCPayLink)].Update((object) payLink);
    }
  }

  private void SetSyncFlagIfNeeded(PX.Objects.AR.ARPayment payment, ARAdjust adjust, CCPayLink payLink)
  {
    if (ARReleaseProcessPayLink.GetDoNotSyncFlag() || !PayLinkHelper.PayLinkOpen(payLink))
      return;
    Decimal? curyAdjdPpdAmt = adjust.CuryAdjdPPDAmt;
    Decimal? curyAdjdWoAmt = adjust.CuryAdjdWOAmt;
    Decimal? nullable = curyAdjdPpdAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      nullable = curyAdjdWoAmt;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      {
        this.SetSyncFlagIfNeeded(payment, payLink);
        return;
      }
    }
    payLink.NeedSync = new bool?(true);
    ((PXGraph) this.Base).Caches[typeof (CCPayLink)].Update((object) payLink);
  }

  private static bool GetDoNotSyncFlag()
  {
    return PXContext.GetSlot<bool>("ARReleaseProcessDoNotSyncFlag");
  }

  public delegate Tuple<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> ProcessAdjustmentsDelegate(
    JournalEntry je,
    PXResultset<ARAdjust> adjustments,
    PX.Objects.AR.ARRegister paymentRegister,
    PX.Objects.AR.ARPayment payment,
    PX.Objects.AR.Customer paymentCustomer,
    PX.Objects.CM.CurrencyInfo new_info,
    PX.Objects.CM.Extensions.Currency paycury);

  public delegate void PersistDelegate();
}
