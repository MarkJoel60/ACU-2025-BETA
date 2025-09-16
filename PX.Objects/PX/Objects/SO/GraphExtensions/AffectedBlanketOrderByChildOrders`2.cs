// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.AffectedBlanketOrderByChildOrders`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.Exceptions;
using PX.Objects.Extensions;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class AffectedBlanketOrderByChildOrders<TSelf, TGraph> : 
  ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, BlanketSOOrder, SOOrderEntry>
  where TSelf : AffectedBlanketOrderByChildOrders<TSelf, TGraph>
  where TGraph : PXGraph
{
  private Dictionary<BlanketSOOrder, AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo> _affectedBlanketOrders;

  /// <summary>
  /// Indicates that the logic of SOOrder.MinSchedOrderDate recalculation is suppressed.
  /// </summary>
  public bool SuppressedMode { get; private set; }

  /// <summary>
  /// Create a scope for suppressing recalculation of SOOrder.MinSchedOrderDate
  /// </summary>
  public IDisposable SuppressedModeScope(SOOrder order)
  {
    return (IDisposable) new AffectedBlanketOrderByChildOrders<TSelf, TGraph>.SuppressionScope(this, order);
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    GraphHelper.EnsureCachePersistence<BlanketSOOrder>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOOrderSite>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOLine>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<BlanketSOLineSplit>((PXGraph) this.Base);
    GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base);
  }

  protected override bool ClearAffectedCaches => false;

  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(BlanketSOOrder entity) => false;

  protected override IEnumerable<BlanketSOOrder> GetLatelyAffectedEntities()
  {
    PXCache<BlanketSOOrder> pxCache1 = GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base);
    this._affectedBlanketOrders = new Dictionary<BlanketSOOrder, AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo>(PXCacheEx.GetComparer<BlanketSOOrder>(pxCache1));
    foreach (BlanketSOOrder key in ((PXCache) pxCache1).Updated)
    {
      int? nullable = key.OpenLineCntr;
      int num1 = 0;
      int num2 = nullable.GetValueOrDefault() == num1 & nullable.HasValue ? 1 : 0;
      nullable = key.OrigOpenLineCntr;
      int num3 = 0;
      int num4 = nullable.GetValueOrDefault() == num3 & nullable.HasValue ? 1 : 0;
      if (num2 != num4)
        this._affectedBlanketOrders.Add(key, new AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo()
        {
          SwitchStatus = true
        });
    }
    PXCache<BlanketSOLine> pxCache2 = GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base);
    foreach (IGrouping<\u003C\u003Ef__AnonymousType82<string, string>, BlanketSOLine> source in GraphHelper.RowCast<BlanketSOLine>(((PXCache) pxCache2).Updated).Where<BlanketSOLine>((Func<BlanketSOLine, bool>) (l =>
    {
      Decimal? curyOpenAmt = l.CuryOpenAmt;
      Decimal? origCuryOpenAmt = l.OrigCuryOpenAmt;
      return !(curyOpenAmt.GetValueOrDefault() == origCuryOpenAmt.GetValueOrDefault() & curyOpenAmt.HasValue == origCuryOpenAmt.HasValue);
    })).GroupBy(line => new
    {
      OrderType = line.OrderType,
      OrderNbr = line.OrderNbr
    }))
    {
      BlanketSOOrder key = PXParentAttribute.SelectParent<BlanketSOOrder>((PXCache) pxCache2, (object) source.First<BlanketSOLine>());
      AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo affectedOrderInfo;
      if (!this._affectedBlanketOrders.TryGetValue(key, out affectedOrderInfo))
        this._affectedBlanketOrders.Add(key, affectedOrderInfo = new AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo());
      HashSet<int> recalcOpenTaxesLineNbrs = source.Select<BlanketSOLine, int>((Func<BlanketSOLine, int>) (line => line.LineNbr.Value)).ToHashSet<int>();
      affectedOrderInfo.OriginalLines = GraphHelper.RowCast<SOLine>((IEnumerable) ((IEnumerable<PXResult<SOLine>>) PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) key.OrderType,
        (object) key.OrderNbr
      })).AsEnumerable<PXResult<SOLine>>()).Where<SOLine>((Func<SOLine, bool>) (l => recalcOpenTaxesLineNbrs.Contains(l.LineNbr.Value))).ToDictionary<SOLine, int>((Func<SOLine, int>) (l => l.LineNbr.Value));
    }
    return !this._affectedBlanketOrders.Any<KeyValuePair<BlanketSOOrder, AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo>>() ? (IEnumerable<BlanketSOOrder>) null : (IEnumerable<BlanketSOOrder>) this._affectedBlanketOrders.Keys;
  }

  protected override void ProcessAffectedEntity(SOOrderEntry primaryGraph, BlanketSOOrder entity)
  {
    ((PXSelectBase<SOOrder>) primaryGraph.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) primaryGraph.Document).Search<SOOrder.orderNbr>((object) entity.OrderNbr, new object[1]
    {
      (object) entity.OrderType
    }));
    AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo info = (AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo) null;
    this._affectedBlanketOrders?.TryGetValue(entity, out info);
    AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo affectedOrderInfo1 = info;
    int num1;
    if (affectedOrderInfo1 == null)
    {
      num1 = 0;
    }
    else
    {
      Dictionary<int, SOLine> originalLines = affectedOrderInfo1.OriginalLines;
      num1 = (originalLines != null ? new bool?(originalLines.Any<KeyValuePair<int, SOLine>>()) : new bool?()).GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      foreach (SOLine soLine in GraphHelper.RowCast<SOLine>((IEnumerable) ((IEnumerable<PXResult<SOLine>>) ((PXSelectBase<SOLine>) primaryGraph.Transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOLine>>()).Where<SOLine>((Func<SOLine, bool>) (line => info.OriginalLines.ContainsKey(line.LineNbr.Value))))
        TaxBaseAttribute.Calculate<SOLine.taxCategoryID, SOOpenTaxAttribute>(((PXSelectBase) primaryGraph.Transactions).Cache, new PXRowUpdatedEventArgs((object) soLine, (object) info.OriginalLines[soLine.LineNbr.Value], true));
    }
    AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo affectedOrderInfo2 = info;
    if ((affectedOrderInfo2 != null ? (affectedOrderInfo2.SwitchStatus ? 1 : 0) : 0) == 0)
      return;
    int? openLineCntr = entity.OpenLineCntr;
    int num2 = 0;
    if (openLineCntr.GetValueOrDefault() == num2 & openLineCntr.HasValue)
      ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.BlanketCompleted))).FireOn((PXGraph) primaryGraph, ((PXSelectBase<SOOrder>) primaryGraph.Document).Current);
    else
      ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (e => e.BlanketReopened))).FireOn((PXGraph) primaryGraph, ((PXSelectBase<SOOrder>) primaryGraph.Document).Current);
  }

  protected override void OnProcessed(SOOrderEntry foreignGraph)
  {
    base.OnProcessed(foreignGraph);
    this._affectedBlanketOrders = (Dictionary<BlanketSOOrder, AffectedBlanketOrderByChildOrders<TSelf, TGraph>.AffectedOrderInfo>) null;
  }

  private DateTime? CalcSchedOrderDate(BlanketSOLineSplit s)
  {
    bool? completed = s.Completed;
    bool flag = false;
    if (completed.GetValueOrDefault() == flag & completed.HasValue)
    {
      Decimal? qty = s.Qty;
      Decimal? qtyOnOrders = s.QtyOnOrders;
      Decimal? receivedQty = s.ReceivedQty;
      Decimal? nullable = qtyOnOrders.HasValue & receivedQty.HasValue ? new Decimal?(qtyOnOrders.GetValueOrDefault() + receivedQty.GetValueOrDefault()) : new Decimal?();
      if (qty.GetValueOrDefault() > nullable.GetValueOrDefault() & qty.HasValue & nullable.HasValue)
        return s.SchedOrderDate;
    }
    return new DateTime?();
  }

  protected virtual void _(Events.RowInserted<BlanketSOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(e.Row, new DateTime?(), this.CalcSchedOrderDate(e.Row));
  }

  protected virtual void _(Events.RowUpdated<BlanketSOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(e.Row, this.CalcSchedOrderDate(e.OldRow), this.CalcSchedOrderDate(e.Row));
  }

  protected virtual void _(Events.RowDeleted<BlanketSOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(e.Row, this.CalcSchedOrderDate(e.Row), new DateTime?());
  }

  private BlanketSOOrder GetBlanketOrder(string orderType, string orderNbr)
  {
    return PXResultset<BlanketSOOrder>.op_Implicit(PXSelectBase<BlanketSOOrder, PXViewOf<BlanketSOOrder>.BasedOn<SelectFromBase<BlanketSOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOOrder.orderType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<BlanketSOOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) orderType,
      (object) orderNbr
    })) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base), new object[2]
    {
      (object) orderType,
      (object) orderNbr
    });
  }

  protected virtual void OnSchedOrderDateUpdated(
    BlanketSOLineSplit split,
    DateTime? oldDate,
    DateTime? newDate)
  {
    DateTime? nullable1 = oldDate;
    DateTime? nullable2 = newDate;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 || this.SuppressedMode)
      return;
    BlanketSOOrder blanketOrder = this.GetBlanketOrder(split.OrderType, split.OrderNbr);
    int num;
    if (newDate.HasValue)
    {
      if (oldDate.HasValue)
      {
        DateTime? nullable3 = newDate;
        DateTime? nullable4 = oldDate;
        num = nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0;
      }
      else
        num = 1;
    }
    else
      num = 0;
    if (num != 0)
    {
      DateTime? minSchedOrderDate = blanketOrder.MinSchedOrderDate;
      if (minSchedOrderDate.HasValue)
      {
        minSchedOrderDate = blanketOrder.MinSchedOrderDate;
        DateTime? nullable5 = newDate;
        if ((minSchedOrderDate.HasValue & nullable5.HasValue ? (minSchedOrderDate.GetValueOrDefault() > nullable5.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      blanketOrder.MinSchedOrderDate = newDate;
      GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base).Update(blanketOrder);
    }
    else
    {
      DateTime? minSchedOrderDate = blanketOrder.MinSchedOrderDate;
      if (minSchedOrderDate.HasValue)
      {
        minSchedOrderDate = blanketOrder.MinSchedOrderDate;
        DateTime? nullable6 = oldDate;
        if ((minSchedOrderDate.HasValue & nullable6.HasValue ? (minSchedOrderDate.GetValueOrDefault() >= nullable6.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      this.RecalculateMinSchedOrderDate(blanketOrder);
    }
  }

  protected void RecalculateMinSchedOrderDate(BlanketSOOrder blanketOrder)
  {
    blanketOrder.MinSchedOrderDate = GraphHelper.RowCast<BlanketSOLineSplit>((IEnumerable) PXSelectBase<BlanketSOLineSplit, PXViewOf<BlanketSOLineSplit>.BasedOn<SelectFromBase<BlanketSOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<BlanketSOLineSplit.orderType>.IsRelatedTo<BlanketSOOrder.orderType>, Field<BlanketSOLineSplit.orderNbr>.IsRelatedTo<BlanketSOOrder.orderNbr>>.WithTablesOf<BlanketSOOrder, BlanketSOLineSplit>, BlanketSOOrder, BlanketSOLineSplit>.SameAsCurrent>, And<BqlOperand<BlanketSOLineSplit.completed, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<BlanketSOLineSplit.qty, IBqlDecimal>.IsGreater<BqlOperand<BlanketSOLineSplit.qtyOnOrders, IBqlDecimal>.Add<BlanketSOLineSplit.receivedQty>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new BlanketSOOrder[1]
    {
      blanketOrder
    }, Array.Empty<object>())).Min<BlanketSOLineSplit, DateTime?>((Func<BlanketSOLineSplit, DateTime?>) (s => s.SchedOrderDate));
    blanketOrder = GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base).Update(blanketOrder);
  }

  private class SuppressionScope : IDisposable
  {
    private readonly AffectedBlanketOrderByChildOrders<TSelf, TGraph> _ext;
    private readonly SOOrder _order;

    public SuppressionScope(
      AffectedBlanketOrderByChildOrders<TSelf, TGraph> ext,
      SOOrder order)
    {
      this._ext = ext;
      this._ext.SuppressedMode = true;
      this._order = order;
    }

    void IDisposable.Dispose()
    {
      this._ext.SuppressedMode = false;
      this._ext.RecalculateMinSchedOrderDate(this._ext.GetBlanketOrder(this._order.OrderType, this._order.OrderNbr));
    }
  }

  private class AffectedOrderInfo
  {
    internal bool SwitchStatus { get; set; }

    internal Dictionary<int, SOLine> OriginalLines { get; set; }
  }
}
