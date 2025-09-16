// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderShipmentProcess
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
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO;

public class SOOrderShipmentProcess : PXGraph<
#nullable disable
SOOrderShipmentProcess, SOOrderShipment>
{
  public PXSelect<SOOrder> Orders;
  public PXSelect<SOShipment> Shipments;
  public PXSelect<SOMiscLine2, Where<SOMiscLine2.orderType, Equal<Required<SOOrder.orderType>>, And<SOMiscLine2.orderNbr, Equal<Required<SOOrder.orderNbr>>, And<SOMiscLine2.completed, NotEqual<True>>>>> MiscLines;
  public PXSelect<ARBalances> Arbalances;
  public PXAction<SOShipment> flow;
  public PXSelectJoin<SOOrderShipment, InnerJoin<SOOrder, On<SOOrder.orderType, Equal<SOOrderShipment.orderType>, And<SOOrder.orderNbr, Equal<SOOrderShipment.orderNbr>>>, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<SOOrderShipment.invoiceType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<SOOrderShipment.invoiceNbr>, And<PX.Objects.AR.ARInvoice.released, Equal<boolTrue>>>>>>, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>> Items;
  public PXSelect<SOAdjust, Where<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>>>> Adjustments;
  public PXSelect<SOTaxTran, Where<SOTaxTran.orderType, Equal<Required<SOTaxTran.orderType>>, And<SOTaxTran.orderNbr, Equal<Required<SOTaxTran.orderNbr>>>>> Taxes;

  [PXUIField(DisplayName = "Flow")]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Flow(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  protected virtual void SOOrder_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (e.Operation == 1)
    {
      int? shipmentCntr1 = row.ShipmentCntr;
      int num1 = 0;
      if (!(shipmentCntr1.GetValueOrDefault() < num1 & shipmentCntr1.HasValue))
      {
        int? openShipmentCntr = row.OpenShipmentCntr;
        int num2 = 0;
        if (!(openShipmentCntr.GetValueOrDefault() < num2 & openShipmentCntr.HasValue))
        {
          int? shipmentCntr2 = row.ShipmentCntr;
          int? billedCntr = row.BilledCntr;
          int? releasedCntr = row.ReleasedCntr;
          int? nullable = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          if (!(shipmentCntr2.GetValueOrDefault() < nullable.GetValueOrDefault() & shipmentCntr2.HasValue & nullable.HasValue) || !(row.Behavior == "SO"))
            return;
        }
      }
      throw new InvalidShipmentCountersException();
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOOrderShipment> e)
  {
    if (!e.Row.OrderTaxAllocated.GetValueOrDefault() && !e.OldRow.OrderTaxAllocated.GetValueOrDefault())
      return;
    bool? orderTaxAllocated1 = e.Row.OrderTaxAllocated;
    bool? orderTaxAllocated2 = e.OldRow.OrderTaxAllocated;
    if (orderTaxAllocated1.GetValueOrDefault() == orderTaxAllocated2.GetValueOrDefault() & orderTaxAllocated1.HasValue == orderTaxAllocated2.HasValue)
      return;
    SOOrder soOrder1 = PXParentAttribute.SelectParent<SOOrder>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrderShipment>>) e).Cache, (object) e.Row);
    if (soOrder1 == null)
      return;
    SOOrder soOrder2 = soOrder1;
    orderTaxAllocated2 = e.Row.OrderTaxAllocated;
    bool? nullable = new bool?(orderTaxAllocated2.GetValueOrDefault());
    soOrder2.OrderTaxAllocated = nullable;
    GraphHelper.Caches<SOOrder>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrderShipment>>) e).Cache.Graph).Update(soOrder1);
  }

  public virtual void CompleteSOLinesAndSplits(
    PX.Objects.AR.ARRegister ardoc,
    List<PXResult<SOOrderShipment, SOOrder>> orderShipments)
  {
    if (ardoc.IsCancellation.GetValueOrDefault() || ardoc.IsCorrection.GetValueOrDefault())
      return;
    foreach (PXResult<SOOrderShipment, SOOrder> orderShipment in orderShipments)
    {
      SOOrder soOrder = PXResult<SOOrderShipment, SOOrder>.op_Implicit(orderShipment);
      bool? requireShipping = SOOrderType.PK.Find((PXGraph) this, soOrder.OrderType).RequireShipping;
      bool flag = false;
      if (requireShipping.GetValueOrDefault() == flag & requireShipping.HasValue)
      {
        PXDatabase.Update<SOLine>(new PXDataFieldParam[4]
        {
          (PXDataFieldParam) new PXDataFieldAssign<SOLine.completed>((object) true),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLine.completed>((object) false),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLine.orderType>((PXDbType) 22, new int?(2), (object) soOrder.OrderType, (PXComp) 0),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLine.orderNbr>((PXDbType) 12, new int?(15), (object) soOrder.OrderNbr, (PXComp) 0)
        });
        PXDatabase.Update<SOLineSplit>(new PXDataFieldParam[4]
        {
          (PXDataFieldParam) new PXDataFieldAssign<SOLineSplit.completed>((object) true),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLineSplit.completed>((object) false),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLineSplit.orderType>((PXDbType) 22, new int?(2), (object) soOrder.OrderType, (PXComp) 0),
          (PXDataFieldParam) new PXDataFieldRestrict<SOLineSplit.orderNbr>((PXDbType) 12, new int?(15), (object) soOrder.OrderNbr, (PXComp) 0)
        });
      }
    }
  }

  public virtual List<PXResult<SOOrderShipment, SOOrder>> UpdateOrderShipments(
    PX.Objects.AR.ARRegister arDoc,
    HashSet<object> processed)
  {
    bool valueOrDefault = arDoc.IsCancellation.GetValueOrDefault();
    arDoc.IsCorrection.GetValueOrDefault();
    PX.Objects.AR.ARRegister arRegister = valueOrDefault ? (PX.Objects.AR.ARRegister) PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, arDoc.OrigDocType, arDoc.OrigRefNbr) : arDoc;
    List<PXResult<SOOrderShipment, SOOrder>> list1 = ((PXSelectBase) this.Items).View.SelectMultiBound(new object[1]
    {
      (object) arRegister
    }, Array.Empty<object>()).Cast<PXResult<SOOrderShipment, SOOrder>>().ToList<PXResult<SOOrderShipment, SOOrder>>();
    (List<SOOrderShipment> source1, List<SOOrder> source2) = EnumerableExtensions.UnZip<PXResult<SOOrderShipment, SOOrder>, SOOrderShipment, SOOrder, (List<SOOrderShipment>, List<SOOrder>)>((IEnumerable<PXResult<SOOrderShipment, SOOrder>>) list1, (Func<PXResult<SOOrderShipment, SOOrder>, SOOrderShipment>) (r => PXCache<SOOrderShipment>.CreateCopy(PXResult<SOOrderShipment, SOOrder>.op_Implicit(r))), (Func<PXResult<SOOrderShipment, SOOrder>, SOOrder>) (r => ((PXResult) r).GetItem<SOOrder>()), (Func<IEnumerable<SOOrderShipment>, IEnumerable<SOOrder>, (List<SOOrderShipment>, List<SOOrder>)>) ((ls, rs) => (ls.ToList<SOOrderShipment>(), rs.ToList<SOOrder>())));
    List<SOOrderShipment> list2;
    if (valueOrDefault)
    {
      List<SOOrderShipment> list3 = source1.Select<SOOrderShipment, SOOrderShipment>((Func<SOOrderShipment, SOOrderShipment>) (r => ChangeReleased(r, false))).ToList<SOOrderShipment>();
      SOInvoice soInvoice = SOInvoice.PK.Find((PXGraph) this, arRegister.DocType, arRegister.RefNbr);
      ((SelectedEntityEvent<SOInvoice>) PXEntityEventBase<SOInvoice>.Container<SOInvoice.Events>.Select((Expression<Func<SOInvoice.Events, PXEntityEvent<SOInvoice.Events>>>) (e => e.InvoiceCancelled))).FireOn((PXGraph) this, soInvoice);
      List<SOOrderShipment> list4 = list3.Where<SOOrderShipment>((Func<SOOrderShipment, bool>) (r => r.OrderTaxAllocated.GetValueOrDefault())).ToList<SOOrderShipment>();
      List<SOOrderShipment> list5 = list3.Select<SOOrderShipment, SOOrderShipment>((Func<SOOrderShipment, SOOrderShipment>) (r => r.UnlinkInvoice((PXGraph) this))).ToList<SOOrderShipment>();
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origDocType, Equal<BqlField<PX.Objects.AR.ARInvoice.origDocType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.isCorrection, IBqlBool>.IsEqual<True>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new PX.Objects.AR.ARRegister[1]
      {
        arDoc
      }, Array.Empty<object>()));
      if (arInvoice != null)
      {
        SOInvoice correctionInvoice = SOInvoice.PK.Find((PXGraph) this, arInvoice.DocType, arInvoice.RefNbr);
        list2 = list5.Select<SOOrderShipment, SOOrderShipment>((Func<SOOrderShipment, SOOrderShipment>) (r => r.LinkInvoice(correctionInvoice, (PXGraph) this))).ToList<SOOrderShipment>();
      }
      else
      {
        foreach (IGrouping<(string, string), SOOrder> source3 in source2.GroupBy<SOOrder, (string, string)>((Func<SOOrder, (string, string)>) (order => (order.OrderType, order.OrderNbr))))
        {
          SOOrder order = source3.First<SOOrder>();
          if (list4.Exists((Predicate<SOOrderShipment>) (_ => _.OrderType == order.OrderType && _.OrderNbr == order.OrderNbr)))
            this.ResetUnbilledTaxes(order);
        }
      }
    }
    else
      list2 = source1.Select<SOOrderShipment, SOOrderShipment>((Func<SOOrderShipment, SOOrderShipment>) (r => ChangeReleased(r, true))).ToList<SOOrderShipment>();
    if (list1.Any<PXResult<SOOrderShipment, SOOrder>>())
      processed.Add((object) arDoc);
    return list1.ToList<PXResult<SOOrderShipment, SOOrder>>();

    SOOrderShipment ChangeReleased(SOOrderShipment sosh, bool isReleased)
    {
      sosh.InvoiceReleased = new bool?(isReleased);
      return ((PXSelectBase<SOOrderShipment>) this.Items).Update(sosh);
    }
  }

  public void UpdateApplications(PX.Objects.AR.ARRegister arDoc, IEnumerable<SOOrderShipment> orderShipments)
  {
    bool? nullable1 = arDoc.IsCancellation;
    int num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    nullable1 = arDoc.IsCorrection;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    if (num1 != 0 || valueOrDefault)
      return;
    foreach (IGrouping<(string, string), SOOrderShipment> grouping in orderShipments.GroupBy<SOOrderShipment, (string, string)>((Func<SOOrderShipment, (string, string)>) (os => (os.OrderType, os.OrderNbr))))
    {
      SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, grouping.Key.Item1);
      SOOrder soOrder = (SOOrder) ((PXGraph) this).Caches[typeof (SOOrder)].Locate((object) new SOOrder()
      {
        OrderType = grouping.Key.Item1,
        OrderNbr = grouping.Key.Item2
      });
      if (soOrder == null)
        soOrder = PXResultset<SOOrder>.op_Implicit(PXSelectBase<SOOrder, PXViewOf<SOOrder>.BasedOn<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.orderNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOOrder.orderType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
        {
          (object) grouping.Key.Item2,
          (object) grouping.Key.Item1
        }));
      bool? nullable2 = soOrder.Completed;
      if (!nullable2.GetValueOrDefault())
      {
        nullable2 = soOrderType.RequireShipping;
        bool flag = false;
        if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
          continue;
      }
      int? shipmentCntr = soOrder.ShipmentCntr;
      int? releasedCntr = soOrder.ReleasedCntr;
      if (shipmentCntr.GetValueOrDefault() <= releasedCntr.GetValueOrDefault() & shipmentCntr.HasValue & releasedCntr.HasValue)
      {
        Decimal? unbilledMiscTot = soOrder.UnbilledMiscTot;
        Decimal num2 = 0M;
        if (unbilledMiscTot.GetValueOrDefault() == num2 & unbilledMiscTot.HasValue)
        {
          foreach (PXResult<SOAdjust> pxResult in ((PXSelectBase<SOAdjust>) this.Adjustments).Select(new object[2]
          {
            (object) soOrder.OrderType,
            (object) soOrder.OrderNbr
          }))
          {
            SOAdjust copy = PXCache<SOAdjust>.CreateCopy(PXResult<SOAdjust>.op_Implicit(pxResult));
            copy.CuryAdjdAmt = new Decimal?(0M);
            copy.CuryAdjgAmt = new Decimal?(0M);
            copy.AdjAmt = new Decimal?(0M);
            ((PXSelectBase<SOAdjust>) this.Adjustments).Update(copy);
          }
        }
      }
    }
  }

  private void UpdateBalance(PXCache s, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrder row))
      return;
    if (e.OldRow is SOOrder oldRow)
    {
      SOOrder order = oldRow;
      Decimal? nullable = oldRow.UnbilledOrderTotal;
      Decimal? UnbilledAmount = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
      nullable = oldRow.OpenOrderTotal;
      Decimal? UnshippedAmount = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
      ARReleaseProcess.UpdateARBalances((PXGraph) this, order, UnbilledAmount, UnshippedAmount);
    }
    ARReleaseProcess.UpdateARBalances((PXGraph) this, row, row.UnbilledOrderTotal, row.OpenOrderTotal);
  }

  protected virtual void ResetUnbilledTaxes(SOOrder order)
  {
    // ISSUE: method pointer
    ((PXGraph) this).RowUpdated.AddHandler<SOOrder>(new PXRowUpdated((object) this, __methodptr(UpdateBalance)));
    try
    {
      order = GraphHelper.Caches<SOOrder>((PXGraph) this).Locate(order);
      SOOrder copy = PXCache<SOOrder>.CreateCopy(order);
      order.CuryOpenTaxTotal = new Decimal?(0M);
      order.CuryUnbilledTaxTotal = new Decimal?(0M);
      foreach (PXResult<SOTaxTran> pxResult in ((PXSelectBase<SOTaxTran>) this.Taxes).Select(new object[2]
      {
        (object) order.OrderType,
        (object) order.OrderNbr
      }))
      {
        SOTaxTran soTaxTran1 = PXResult<SOTaxTran>.op_Implicit(pxResult);
        SOTaxTran soTaxTran2 = soTaxTran1;
        Decimal? nullable1 = order.CuryUnbilledLineTotal;
        Decimal num1 = 0M;
        Decimal? nullable2;
        if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
        {
          int? billedCntr = order.BilledCntr;
          int? releasedCntr = order.ReleasedCntr;
          int? nullable3 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          int num2 = 0;
          if (!(nullable3.GetValueOrDefault() > num2 & nullable3.HasValue) || !order.OrderTaxAllocated.GetValueOrDefault())
          {
            nullable2 = soTaxTran1.CuryTaxableAmt;
            goto label_7;
          }
        }
        nullable2 = new Decimal?(0M);
label_7:
        soTaxTran2.CuryUnbilledTaxableAmt = nullable2;
        SOOrder soOrder1 = order;
        nullable1 = soOrder1.CuryUnbilledTaxTotal;
        SOTaxTran soTaxTran3 = soTaxTran1;
        Decimal? unbilledLineTotal = order.CuryUnbilledLineTotal;
        Decimal num3 = 0M;
        Decimal? nullable4;
        if (!(unbilledLineTotal.GetValueOrDefault() == num3 & unbilledLineTotal.HasValue))
        {
          int? billedCntr = order.BilledCntr;
          int? releasedCntr = order.ReleasedCntr;
          int? nullable5 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          int num4 = 0;
          if (!(nullable5.GetValueOrDefault() > num4 & nullable5.HasValue) || !order.OrderTaxAllocated.GetValueOrDefault())
          {
            nullable4 = soTaxTran1.CuryTaxAmt;
            goto label_11;
          }
        }
        nullable4 = new Decimal?(0M);
label_11:
        Decimal? nullable6 = nullable4;
        soTaxTran3.CuryUnbilledTaxAmt = nullable4;
        Decimal? nullable7 = nullable6;
        soOrder1.CuryUnbilledTaxTotal = nullable1.HasValue & nullable7.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        SOTaxTran soTaxTran4 = soTaxTran1;
        nullable7 = order.CuryOpenLineTotal;
        Decimal num5 = 0M;
        Decimal? nullable8;
        if (!(nullable7.GetValueOrDefault() == num5 & nullable7.HasValue))
        {
          int? billedCntr = order.BilledCntr;
          int? releasedCntr = order.ReleasedCntr;
          int? nullable9 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          int num6 = 0;
          if (!(nullable9.GetValueOrDefault() > num6 & nullable9.HasValue) || !order.OrderTaxAllocated.GetValueOrDefault())
          {
            nullable8 = soTaxTran1.CuryTaxableAmt;
            goto label_15;
          }
        }
        nullable8 = new Decimal?(0M);
label_15:
        soTaxTran4.CuryUnshippedTaxableAmt = nullable8;
        SOOrder soOrder2 = order;
        nullable7 = soOrder2.CuryOpenTaxTotal;
        SOTaxTran soTaxTran5 = soTaxTran1;
        Decimal? curyOpenLineTotal = order.CuryOpenLineTotal;
        Decimal num7 = 0M;
        Decimal? nullable10;
        if (!(curyOpenLineTotal.GetValueOrDefault() == num7 & curyOpenLineTotal.HasValue))
        {
          int? billedCntr = order.BilledCntr;
          int? releasedCntr = order.ReleasedCntr;
          int? nullable11 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          int num8 = 0;
          if (!(nullable11.GetValueOrDefault() > num8 & nullable11.HasValue) || !order.OrderTaxAllocated.GetValueOrDefault())
          {
            nullable10 = soTaxTran1.CuryTaxAmt;
            goto label_19;
          }
        }
        nullable10 = new Decimal?(0M);
label_19:
        Decimal? nullable12 = nullable10;
        soTaxTran5.CuryUnshippedTaxAmt = nullable10;
        nullable1 = nullable12;
        soOrder2.CuryOpenTaxTotal = nullable7.HasValue & nullable1.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        ((PXSelectBase<SOTaxTran>) this.Taxes).Update(soTaxTran1);
      }
      SOOrder soOrder3 = order;
      Decimal? curyOpenOrderTotal = soOrder3.CuryOpenOrderTotal;
      Decimal? nullable13 = order.CuryOpenTaxTotal;
      Decimal? curyOpenTaxTotal = copy.CuryOpenTaxTotal;
      Decimal? nullable14 = nullable13.HasValue & curyOpenTaxTotal.HasValue ? new Decimal?(nullable13.GetValueOrDefault() - curyOpenTaxTotal.GetValueOrDefault()) : new Decimal?();
      soOrder3.CuryOpenOrderTotal = curyOpenOrderTotal.HasValue & nullable14.HasValue ? new Decimal?(curyOpenOrderTotal.GetValueOrDefault() + nullable14.GetValueOrDefault()) : new Decimal?();
      SOOrder soOrder4 = order;
      Decimal? unbilledOrderTotal = soOrder4.CuryUnbilledOrderTotal;
      Decimal? unbilledTaxTotal = order.CuryUnbilledTaxTotal;
      nullable13 = copy.CuryUnbilledTaxTotal;
      Decimal? nullable15 = unbilledTaxTotal.HasValue & nullable13.HasValue ? new Decimal?(unbilledTaxTotal.GetValueOrDefault() - nullable13.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable16;
      if (!(unbilledOrderTotal.HasValue & nullable15.HasValue))
      {
        nullable13 = new Decimal?();
        nullable16 = nullable13;
      }
      else
        nullable16 = new Decimal?(unbilledOrderTotal.GetValueOrDefault() + nullable15.GetValueOrDefault());
      soOrder4.CuryUnbilledOrderTotal = nullable16;
      GraphHelper.Caches<SOOrder>((PXGraph) this).Update(order);
    }
    finally
    {
      // ISSUE: method pointer
      ((PXGraph) this).RowUpdated.RemoveHandler<SOOrder>(new PXRowUpdated((object) this, __methodptr(UpdateBalance)));
    }
  }

  public virtual void CompleteMiscLines(
    PX.Objects.AR.ARRegister ardoc,
    List<SOOrderShipment> directShipmentsToCreate)
  {
    // ISSUE: method pointer
    ((PXGraph) this).RowUpdated.AddHandler<SOOrder>(new PXRowUpdated((object) this, __methodptr(UpdateBalance)));
    try
    {
      foreach (IGrouping<(string, string), ARTran> source in GraphHelper.RowCast<ARTran>((IEnumerable) PXSelectBase<ARTran, PXViewOf<ARTran>.BasedOn<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<ARTran.tranType>.IsRelatedTo<PX.Objects.AR.ARInvoice.docType>, Field<ARTran.refNbr>.IsRelatedTo<PX.Objects.AR.ARInvoice.refNbr>>.WithTablesOf<PX.Objects.AR.ARInvoice, ARTran>, PX.Objects.AR.ARInvoice, ARTran>.SameAsCurrent>, And<BqlOperand<ARTran.sOOrderNbr, IBqlString>.IsNotNull>>>.And<BqlOperand<ARTran.lineType, IBqlString>.IsEqual<SOLineType.miscCharge>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this, (object[]) new PX.Objects.AR.ARRegister[1]
      {
        ardoc
      }, Array.Empty<object>())).ToList<ARTran>().GroupBy<ARTran, (string, string)>((Func<ARTran, (string, string)>) (t => (t.SOOrderType, t.SOOrderNbr))))
      {
        (string, string) key = source.Key;
        foreach (PXResult<SOMiscLine2> pxResult in ((PXSelectBase<SOMiscLine2>) this.MiscLines).Select(new object[2]
        {
          (object) key.Item1,
          (object) key.Item2
        }))
        {
          SOMiscLine2 line = PXResult<SOMiscLine2>.op_Implicit(pxResult);
          if (!source.All<ARTran>((Func<ARTran, bool>) (tran =>
          {
            int? soOrderLineNbr = tran.SOOrderLineNbr;
            int? lineNbr = line.LineNbr;
            return !(soOrderLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & soOrderLineNbr.HasValue == lineNbr.HasValue);
          })))
          {
            line.CuryUnbilledAmt = new Decimal?(0M);
            line.UnbilledQty = new Decimal?(0M);
            line.Completed = new bool?(true);
            ((PXSelectBase<SOMiscLine2>) this.MiscLines).Update(line);
          }
        }
        if (source.Any<ARTran>((Func<ARTran, bool>) (tran =>
        {
          short? invtMult = tran.InvtMult;
          int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num = 0;
          return !(nullable.GetValueOrDefault() == num & nullable.HasValue);
        })))
          directShipmentsToCreate.Add(SOOrderShipment.FromDirectInvoice(ardoc, key.Item1, key.Item2));
      }
    }
    finally
    {
      // ISSUE: method pointer
      ((PXGraph) this).RowUpdated.RemoveHandler<SOOrder>(new PXRowUpdated((object) this, __methodptr(UpdateBalance)));
    }
  }

  public virtual void OnInvoiceReleased(
    PX.Objects.AR.ARRegister ardoc,
    List<PXResult<SOOrderShipment, SOOrder>> orderShipments)
  {
    this.CompleteSOLinesAndSplits(ardoc, orderShipments);
  }
}
