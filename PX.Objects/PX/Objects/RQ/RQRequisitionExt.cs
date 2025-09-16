// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.Extensions;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ;

public static class RQRequisitionExt
{
  public static void CompleteBidding(this RQRequisition requisition, PXGraph graph)
  {
    if (requisition == null || requisition.BiddingComplete.GetValueOrDefault())
      return;
    graph.LiteUpdate<RQRequisition>(requisition, (Action<ValueSetter<RQRequisition>>) (_ => _.Set<bool?>((Expression<Func<RQRequisition, bool?>>) (o => o.BiddingComplete), new bool?(true))));
    ((SelectedEntityEvent<RQRequisition>) PXEntityEventBase<RQRequisition>.Container<RQRequisition.Events>.Select((Expression<Func<RQRequisition.Events, PXEntityEvent<RQRequisition.Events>>>) (e => e.BiddingCompleted))).FireOn(graph, requisition);
  }
}
