// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.GraphExtensions.SOOrderEntryExt.AffectedRQRequisitionOrders
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Extensions;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ.GraphExtensions.SOOrderEntryExt;

public class AffectedRQRequisitionOrders : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedRQRequisitionOrders, SOOrderEntry, RQRequisition, RQRequisitionEntry>
{
  private readonly Dictionary<string, (RQRequisitionOrder Link, PX.Objects.SO.SOOrder Order)> orders = new Dictionary<string, (RQRequisitionOrder, PX.Objects.SO.SOOrder)>();

  private PXCache<RQRequisition> requisitions
  {
    get => GraphHelper.Caches<RQRequisition>((PXGraph) this.Base);
  }

  protected override bool PersistInSameTransaction => false;

  protected override bool EntityIsAffected(RQRequisition entity)
  {
    if (!object.Equals(((PXCache) this.requisitions).GetValueOriginal<RQRequisition.quoted>((object) entity), (object) entity.Quoted))
    {
      RQRequisitionOrder requisitionOrder = ((PXCache) GraphHelper.Caches<RQRequisitionOrder>((PXGraph) this.Base)).Deleted.Cast<RQRequisitionOrder>().FirstOrDefault<RQRequisitionOrder>((Func<RQRequisitionOrder, bool>) (o => o.ReqNbr == entity.ReqNbr && o.OrderCategory == "SO"));
      if (requisitionOrder != null)
      {
        PX.Objects.SO.SOOrder soOrder = PX.Objects.SO.SOOrder.PK.Find((PXGraph) this.Base, requisitionOrder.OrderType, requisitionOrder.OrderNbr);
        this.orders[entity.ReqNbr] = (requisitionOrder, soOrder);
        return true;
      }
    }
    return false;
  }

  protected override void ProcessAffectedEntity(
    RQRequisitionEntry primaryGraph,
    RQRequisition entity)
  {
    (RQRequisitionOrder Link, PX.Objects.SO.SOOrder Order) tuple;
    if (!this.orders.TryGetValue(entity.ReqNbr, out tuple) || tuple.Link == null || tuple.Order == null)
      return;
    ((SelectedEntityEvent<RQRequisitionOrder, PX.Objects.SO.SOOrder>) PXEntityEventBase<RQRequisitionOrder>.Container<RQRequisitionOrder.Events>.Select<PX.Objects.SO.SOOrder>((Expression<Func<RQRequisitionOrder.Events, PXEntityEvent<RQRequisitionOrder.Events, PX.Objects.SO.SOOrder>>>) (e => e.SOOrderUnlinked))).FireOn((PXGraph) primaryGraph, tuple.Link, tuple.Order);
  }

  protected override RQRequisition ActualizeEntity(
    RQRequisitionEntry primaryGraph,
    RQRequisition entity)
  {
    return PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) primaryGraph.Document).Search<RQRequisition.reqNbr>((object) entity.ReqNbr, Array.Empty<object>()));
  }
}
