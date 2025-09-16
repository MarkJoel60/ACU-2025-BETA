// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARPaymentEntryPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARPaymentEntryPayLink : PXGraphExtension<ARPaymentEntry>
{
  public bool DoNotSetNeedSync { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  private void _(PX.Data.Events.RowPersisted<SOAdjust> e)
  {
    if (this.DoNotSetNeedSync || e.TranStatus != null || e.Operation != 2 && e.Operation != 1 && e.Operation != 3)
      return;
    SOAdjust adj = e.Row;
    PX.Objects.SO.SOOrder soOrder = GraphHelper.RowCast<PX.Objects.SO.SOOrder>(((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOOrder)].Cached).Where<PX.Objects.SO.SOOrder>((Func<PX.Objects.SO.SOOrder, bool>) (i => i.OrderNbr == adj.AdjdOrderNbr && i.OrderType == adj.AdjdOrderType)).FirstOrDefault<PX.Objects.SO.SOOrder>();
    if (soOrder == null)
      return;
    SOOrderPayLink extension = ((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOOrder)].GetExtension<SOOrderPayLink>((object) soOrder);
    if ((extension != null ? (!extension.PayLinkID.HasValue ? 1 : 0) : 1) != 0)
      return;
    CCPayLink payLink = PXResultset<CCPayLink>.op_Implicit(PXSelectBase<CCPayLink, PXSelect<CCPayLink, Where<CCPayLink.payLinkID, Equal<Required<CCPayLink.payLinkID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension.PayLinkID
    }));
    Decimal? amount = payLink.Amount;
    Decimal? curyUnpaidBalance = soOrder.CuryUnpaidBalance;
    if (amount.GetValueOrDefault() == curyUnpaidBalance.GetValueOrDefault() & amount.HasValue == curyUnpaidBalance.HasValue)
      return;
    bool? needSync = payLink.NeedSync;
    bool flag = false;
    if (!(needSync.GetValueOrDefault() == flag & needSync.HasValue))
      return;
    this.SetSyncFlagIfNeeded(((PXSelectBase<ARPayment>) this.Base.Document).Current, payLink);
    needSync = payLink.NeedSync;
    if (!needSync.GetValueOrDefault() || e.TranStatus != null)
      return;
    ((PXGraph) this.Base).Caches[typeof (CCPayLink)].PersistUpdated((object) payLink);
  }

  private void SetSyncFlagIfNeeded(ARPayment payment, CCPayLink payLink)
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
}
