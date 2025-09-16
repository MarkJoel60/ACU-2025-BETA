// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.AffectedSOOrdersWithPrepaymentRequirementsBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class AffectedSOOrdersWithPrepaymentRequirementsBase<TSelf, TGraph> : 
  ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, SOOrder, SOOrderEntry>
  where TSelf : ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, SOOrder, SOOrderEntry>
  where TGraph : PXGraph
{
  protected HashSet<SOOrder> ordersChangedDuringPersist;

  private PXCache<SOOrder> orders => GraphHelper.Caches<SOOrder>((PXGraph) this.Base);

  protected override bool PersistInSameTransaction => false;

  public override void Persist(Action basePersist)
  {
    this.ordersChangedDuringPersist = new HashSet<SOOrder>(PXCacheEx.GetComparer<SOOrder>(GraphHelper.Caches<SOOrder>((PXGraph) this.Base)));
    base.Persist(basePersist);
  }

  protected virtual void _(Events.RowUpdated<SOOrder> args)
  {
    if (this.ordersChangedDuringPersist == null || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<SOOrder>>) args).Cache.ObjectsEqual<SOOrder.curyPrepaymentReqAmt, SOOrder.curyPaymentOverall>((object) args.Row, (object) args.OldRow))
      return;
    this.ordersChangedDuringPersist.Add(args.Row);
  }

  protected override bool EntityIsAffected(SOOrder order)
  {
    return !object.Equals(((PXCache) this.orders).GetValueOriginal<SOOrder.curyPrepaymentReqAmt>((object) order), (object) order.CuryPrepaymentReqAmt) || !object.Equals(((PXCache) this.orders).GetValueOriginal<SOOrder.curyPaymentOverall>((object) order), (object) order.CuryPaymentOverall) || !object.Equals(((PXCache) this.orders).GetValueOriginal<SOOrder.curyUnpaidBalance>((object) order), (object) order.CuryUnpaidBalance);
  }

  protected override void ProcessAffectedEntity(SOOrderEntry orderEntry, SOOrder order)
  {
    Decimal? curyPaymentOverall = order.CuryPaymentOverall;
    Decimal? prepaymentReqAmt = order.CuryPrepaymentReqAmt;
    if (!(curyPaymentOverall.GetValueOrDefault() >= prepaymentReqAmt.GetValueOrDefault() & curyPaymentOverall.HasValue & prepaymentReqAmt.HasValue))
    {
      Decimal? curyUnpaidBalance = order.CuryUnpaidBalance;
      Decimal num = 0M;
      if (!(curyUnpaidBalance.GetValueOrDefault() == num & curyUnpaidBalance.HasValue))
      {
        order.ViolatePrepaymentRequirements((PXGraph) orderEntry);
        return;
      }
    }
    order.SatisfyPrepaymentRequirements((PXGraph) orderEntry);
  }

  protected override IEnumerable<SOOrder> GetLatelyAffectedEntities()
  {
    return (IEnumerable<SOOrder>) this.ordersChangedDuringPersist;
  }

  protected override void OnProcessed(SOOrderEntry foreignGraph)
  {
    this.ordersChangedDuringPersist = (HashSet<SOOrder>) null;
  }

  protected override SOOrder ActualizeEntity(SOOrderEntry orderEntry, SOOrder order)
  {
    return PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) orderEntry.Document).Search<SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    }));
  }
}
