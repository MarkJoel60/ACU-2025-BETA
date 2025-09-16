// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARPaymentEntryExt.AffectedSOOrdersWithCreditLimitExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;

public class AffectedSOOrdersWithCreditLimitExt : 
  AffectedSOOrdersWithCreditLimitExtBase<ARPaymentEntry>
{
  private HashSet<PX.Objects.SO.SOOrder> ordersChangedDuringPersist;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public override void Persist(Action basePersist)
  {
    this.ordersChangedDuringPersist = new HashSet<PX.Objects.SO.SOOrder>(PXCacheEx.GetComparer<PX.Objects.SO.SOOrder>(GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)));
    base.Persist(basePersist);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> args)
  {
    if (this.ordersChangedDuringPersist == null)
      return;
    bool? isFullyPaid1 = args.Row.IsFullyPaid;
    bool? isFullyPaid2 = args.OldRow.IsFullyPaid;
    if (isFullyPaid1.GetValueOrDefault() == isFullyPaid2.GetValueOrDefault() & isFullyPaid1.HasValue == isFullyPaid2.HasValue)
      return;
    this.ordersChangedDuringPersist.Add(args.Row);
  }

  protected override IEnumerable<PX.Objects.SO.SOOrder> GetLatelyAffectedEntities()
  {
    return (IEnumerable<PX.Objects.SO.SOOrder>) this.ordersChangedDuringPersist;
  }

  protected override void OnProcessed(SOOrderEntry foreignGraph)
  {
    this.ordersChangedDuringPersist = (HashSet<PX.Objects.SO.SOOrder>) null;
  }
}
