// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.AffectedSpecialOrderLinesByPOLines
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.Extensions;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class AffectedSpecialOrderLinesByPOLines : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedSpecialOrderLinesByPOLines, POOrderEntry, PX.Objects.SO.SOOrder, SOOrderEntry>
{
  private Dictionary<PX.Objects.SO.SOOrder, IEnumerable<POOrderEntry.SOLine5>> _affectedOrders;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(PX.Objects.SO.SOOrder entity) => false;

  protected override IEnumerable<PX.Objects.SO.SOOrder> GetAffectedEntities()
  {
    foreach (IGrouping<\u003C\u003Ef__AnonymousType82<string, string>, POOrderEntry.SOLine5> source in ((PXSelectBase) this.Base.FixedDemandOrigSOLine).Cache.Updated.Cast<POOrderEntry.SOLine5>().Where<POOrderEntry.SOLine5>((Func<POOrderEntry.SOLine5, bool>) (l => l.IsCostUpdatedOnPO.GetValueOrDefault() && l.CuryUnitCostUpdated.HasValue)).GroupBy(l => new
    {
      OrderType = l.OrderType,
      OrderNbr = l.OrderNbr
    }))
    {
      if (this._affectedOrders == null)
        this._affectedOrders = new Dictionary<PX.Objects.SO.SOOrder, IEnumerable<POOrderEntry.SOLine5>>(PXCacheEx.GetComparer<PX.Objects.SO.SOOrder>(GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)));
      this._affectedOrders.Add(PX.Objects.SO.SOOrder.PK.Find((PXGraph) this.Base, source.First<POOrderEntry.SOLine5>().OrderType, source.First<POOrderEntry.SOLine5>().OrderNbr), (IEnumerable<POOrderEntry.SOLine5>) source);
    }
    return (IEnumerable<PX.Objects.SO.SOOrder>) this._affectedOrders?.Keys ?? Enumerable.Empty<PX.Objects.SO.SOOrder>();
  }

  protected override PX.Objects.SO.SOOrder ActualizeEntity(
    SOOrderEntry primaryGraph,
    PX.Objects.SO.SOOrder entity)
  {
    return PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) primaryGraph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) entity.OrderNbr, new object[1]
    {
      (object) entity.OrderType
    }));
  }

  protected override void ProcessAffectedEntity(SOOrderEntry primaryGraph, PX.Objects.SO.SOOrder entity)
  {
    foreach (POOrderEntry.SOLine5 soLine5 in this._affectedOrders[entity])
    {
      PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) primaryGraph.Transactions).Search<PX.Objects.SO.SOLine.lineNbr>((object) soLine5.LineNbr, new object[2]
      {
        (object) soLine5.OrderType,
        (object) soLine5.OrderNbr
      }) ?? throw new RowNotFoundException(((PXSelectBase) primaryGraph.Transactions).Cache, new object[3]
      {
        (object) soLine5.OrderType,
        (object) soLine5.OrderNbr,
        (object) soLine5.LineNbr
      }));
      soLine.CuryUnitCost = soLine5.CuryUnitCostUpdated;
      using (((PXGraph) primaryGraph).FindImplementation<SOLineCost>()?.CostsRecalculationScope())
        ((PXSelectBase<PX.Objects.SO.SOLine>) primaryGraph.Transactions).Update(soLine);
    }
  }

  protected override void OnProcessed(SOOrderEntry foreignGraph)
  {
    base.OnProcessed(foreignGraph);
    this._affectedOrders = (Dictionary<PX.Objects.SO.SOOrder, IEnumerable<POOrderEntry.SOLine5>>) null;
  }

  protected override void ClearCaches(PXGraph graph)
  {
    base.ClearCaches(graph);
    ((PXCache) GraphHelper.Caches<POOrderEntry.SOLine5>(graph)).Clear();
    ((PXCache) GraphHelper.Caches<POOrderEntry.SOLine5>(graph)).ClearQueryCache();
  }
}
