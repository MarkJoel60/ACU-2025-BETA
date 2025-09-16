// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.AffectedSOOrdersWithCreditLimitExtBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public class AffectedSOOrdersWithCreditLimitExtBase<TGraph> : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedSOOrdersWithCreditLimitExtBase<TGraph>, TGraph, SOOrder, SOOrderEntry>
  where TGraph : PXGraph
{
  private PXCache<SOOrder> orders => GraphHelper.Caches<SOOrder>((PXGraph) this.Base);

  protected override bool PersistInSameTransaction => false;

  protected override bool EntityIsAffected(SOOrder order)
  {
    bool? valueOriginal = (bool?) ((PXCache) this.orders).GetValueOriginal<SOOrder.isFullyPaid>((object) order);
    bool? isFullyPaid = order.IsFullyPaid;
    return !(valueOriginal.GetValueOrDefault() == isFullyPaid.GetValueOrDefault() & valueOriginal.HasValue == isFullyPaid.HasValue);
  }

  protected override void ProcessAffectedEntity(SOOrderEntry orderEntry, SOOrder order)
  {
    if (order.CreditHold.GetValueOrDefault() && order.IsFullyPaid.GetValueOrDefault())
    {
      order.SatisfyCreditLimitByPayment((PXGraph) orderEntry);
    }
    else
    {
      if (order.CreditHold.GetValueOrDefault() || order.IsFullyPaid.GetValueOrDefault())
        return;
      this.RunCreditLimitVerification(orderEntry, order);
    }
  }

  protected virtual void RunCreditLimitVerification(SOOrderEntry orderEntry, SOOrder order)
  {
    ((PXSelectBase<SOOrder>) orderEntry.Document).Update(order);
  }

  protected override SOOrder ActualizeEntity(SOOrderEntry orderEntry, SOOrder order)
  {
    return PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) orderEntry.Document).Search<SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    }));
  }
}
