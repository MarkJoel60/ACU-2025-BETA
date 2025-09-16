// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrder_ExtensionMethods
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
namespace PX.Objects.SO;

public static class SOOrder_ExtensionMethods
{
  public static void MarkOpen(this SOOrder order)
  {
    if (order == null)
      return;
    order.Completed = new bool?(false);
    order.Status = "N";
  }

  public static void MarkCompleted(this SOOrder order)
  {
    if (order == null)
      return;
    order.Completed = new bool?(true);
    order.Status = "C";
  }

  public static void SatisfyPrepaymentRequirements(this SOOrder order, PXGraph graph)
  {
    if (order == null)
      return;
    bool? completed = order.Completed;
    bool flag1 = false;
    if (!(completed.GetValueOrDefault() == flag1 & completed.HasValue) || !order.AllowsRequiredPrepayment.GetValueOrDefault())
      return;
    bool? prepaymentReqSatisfied = order.PrepaymentReqSatisfied;
    bool flag2 = false;
    if (!(prepaymentReqSatisfied.GetValueOrDefault() == flag2 & prepaymentReqSatisfied.HasValue))
      return;
    graph.LiteUpdate<SOOrder>(order, (Action<ValueSetter<SOOrder>>) (_ => _.Set<bool?>((Expression<Func<SOOrder, bool?>>) (o => o.PrepaymentReqSatisfied), new bool?(true))));
    ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.PaymentRequirementsSatisfied))).FireOn(graph, order);
    if (!(PX.Objects.AR.Customer.PK.Find(graph, order.CustomerID)?.Status == "C"))
      return;
    ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.CreditLimitViolated))).FireOn(graph, order);
  }

  public static void ViolatePrepaymentRequirements(this SOOrder order, PXGraph graph)
  {
    if (order == null)
      return;
    bool? completed = order.Completed;
    bool flag = false;
    if (!(completed.GetValueOrDefault() == flag & completed.HasValue) || !order.AllowsRequiredPrepayment.GetValueOrDefault() || !order.PrepaymentReqSatisfied.GetValueOrDefault())
      return;
    graph.LiteUpdate<SOOrder>(order, (Action<ValueSetter<SOOrder>>) (_ => _.Set<bool?>((Expression<Func<SOOrder, bool?>>) (o => o.PrepaymentReqSatisfied), new bool?(false))));
    ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.PaymentRequirementsViolated))).FireOn(graph, order);
  }

  public static void SatisfyCreditLimitByPayment(this SOOrder order, PXGraph graph)
  {
    SOOrderType soOrderType = SOOrderType.PK.Find(graph, order.OrderType);
    bool? nullable;
    int num;
    if (soOrderType == null)
    {
      num = 0;
    }
    else
    {
      nullable = soOrderType.RemoveCreditHoldByPayment;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
    {
      graph.Caches[typeof (SOOrder)].RaiseExceptionHandling<SOOrder.status>((object) order, (object) null, (Exception) null);
      graph.LiteUpdate<SOOrder>(order, (Action<ValueSetter<SOOrder>>) (_ => _.Set<bool?>((Expression<Func<SOOrder, bool?>>) (o => o.ApprovedCreditByPayment), new bool?(true))));
      nullable = order.CreditHold;
      if (nullable.GetValueOrDefault())
        ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (ev => ev.CreditLimitSatisfied))).FireOn(graph, order);
    }
    if (!(PX.Objects.AR.Customer.PK.Find(graph, order.CustomerID)?.Status == "C"))
      return;
    ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.CreditLimitViolated))).FireOn(graph, order);
  }
}
