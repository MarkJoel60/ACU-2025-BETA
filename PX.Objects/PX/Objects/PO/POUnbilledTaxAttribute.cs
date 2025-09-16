// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POUnbilledTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

public class POUnbilledTaxAttribute : POTaxAttribute
{
  protected override short SortOrder => 1;

  public POUnbilledTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (POTax.curyUnbilledTaxableAmt).Name;
    this._CuryTaxAmt = typeof (POTax.curyUnbilledTaxAmt).Name;
    this.CuryDocBal = typeof (POOrder.curyUnbilledOrderTotal);
    this.CuryLineTotal = typeof (POOrder.curyUnbilledLineTotal);
    this.CuryTaxTotal = typeof (POOrder.curyUnbilledTaxTotal);
    this.DocDate = typeof (POOrder.orderDate);
    this.CuryDiscTot = typeof (POOrder.curyDiscTot);
    this.CuryTranAmt = typeof (POLine.curyUnbilledAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (POLine.curyUnbilledAmt), typeof (SumCalc<POOrder.curyUnbilledLineTotal>)));
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Current<POLine.lineNbr>>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }

  protected override string TaxableQtyFieldNameForTaxDetail => "UnbilledTaxableQty";

  protected override void ReDefaultTaxes(PXCache cache, List<object> details)
  {
  }

  protected override void ReDefaultTaxes(
    PXCache cache,
    object clearDet,
    object defaultDet,
    bool defaultExisting = true)
  {
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal valueOrDefault = ((Decimal?) sender.GetValue(row, this._CuryTranAmt)).GetValueOrDefault();
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(valueOrDefault * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
    nullable3 = nullable4;
    Decimal num = 0M;
    return !(nullable3.GetValueOrDefault() == num & nullable3.HasValue) ? new Decimal?(sender.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(nullable4.Value)) : new Decimal?(0M);
  }

  protected override bool IsRetainedTaxes(PXGraph graph) => false;

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal curyOpenTaxTotal,
    Decimal curyOpenInclTaxTotal,
    Decimal curyOpenWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal curyDiscTotal = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    Decimal curyUnbilledLineTotal = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M);
    Decimal valueOrDefault = ((Decimal?) this.ParentGetValue<POOrder.curyLineTotal>(sender.Graph)).GetValueOrDefault();
    Decimal num1 = this.CalcUnbilledDiscTotal(sender, row, valueOrDefault, curyDiscTotal, curyUnbilledLineTotal);
    Decimal num2 = curyUnbilledLineTotal + curyOpenTaxTotal - curyOpenInclTaxTotal - num1;
    Decimal num3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    Decimal num4 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
    if (curyOpenTaxTotal != num3)
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) curyOpenTaxTotal);
    if (string.IsNullOrEmpty(this._CuryDocBal) || !(num2 != num4))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) num2);
  }

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
    Decimal valueOrDefault1 = ((Decimal?) this.ParentGetValue<POOrder.curyUnbilledLineTotal>(sender.Graph)).GetValueOrDefault();
    if (!(valueOrDefault1 != 0M) || !(CuryTaxableAmt != 0M) || !(valueOrDefault1 == CuryTaxableAmt))
      return;
    Decimal valueOrDefault2 = ((Decimal?) this.ParentGetValue<POOrder.curyLineTotal>(sender.Graph)).GetValueOrDefault();
    Decimal valueOrDefault3 = ((Decimal?) this.ParentGetValue<POOrder.curyDiscTot>(sender.Graph)).GetValueOrDefault();
    CuryTaxableAmt -= this.CalcUnbilledDiscTotal(sender, row, valueOrDefault2, valueOrDefault3, valueOrDefault1);
  }

  protected virtual Decimal CalcUnbilledDiscTotal(
    PXCache sender,
    object row,
    Decimal curyLineTotal,
    Decimal curyDiscTotal,
    Decimal curyUnbilledLineTotal)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = sender.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    if (Math.Abs(curyLineTotal - curyUnbilledLineTotal) < 0.00005M)
      return curyDiscTotal;
    return !(curyLineTotal == 0M) ? defaultCurrencyInfo.RoundCury(curyUnbilledLineTotal * curyDiscTotal / curyLineTotal) : 0M;
  }

  protected override void SetTaxDetailCuryExpenseAmt(
    PXCache cache,
    TaxDetail taxdet,
    Decimal CuryExpenseAmt)
  {
  }

  public override object Insert(PXCache cache, object item) => this.InsertCached(cache, item);

  public override object Update(PXCache sender, object item) => this.UpdateCached(sender, item);

  public override object Delete(PXCache cache, object item) => this.DeleteCached(cache, item);

  protected virtual object InsertCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (POOrder)];
    string OrderType = (string) cach.GetValue<POOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<POOrder.orderNbr>(cach.Current);
    List<PXRowInserted> pxRowInsertedList = this.storeCachedInsertList(sender, recordsList, OrderType, OrderNbr);
    List<PXRowDeleted> pxRowDeletedList = this.storeCachedDeleteList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Insert(item);
    }
    finally
    {
      foreach (PXRowInserted pxRowInserted in pxRowInsertedList)
        sender.Graph.RowInserted.RemoveHandler<POTax>(pxRowInserted);
      foreach (PXRowDeleted pxRowDeleted in pxRowDeletedList)
        sender.Graph.RowDeleted.RemoveHandler<POTax>(pxRowDeleted);
    }
  }

  protected virtual object UpdateCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (POOrder)];
    string OrderType = (string) cach.GetValue<POOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<POOrder.orderNbr>(cach.Current);
    List<PXRowUpdated> pxRowUpdatedList = this.storeCachedUpdateList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Update(item);
    }
    finally
    {
      foreach (PXRowUpdated pxRowUpdated in pxRowUpdatedList)
        sender.Graph.RowUpdated.RemoveHandler<POTax>(pxRowUpdated);
    }
  }

  protected virtual object DeleteCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (POOrder)];
    string OrderType = (string) cach.GetValue<POOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<POOrder.orderNbr>(cach.Current);
    List<PXRowInserted> pxRowInsertedList = this.storeCachedInsertList(sender, recordsList, OrderType, OrderNbr);
    List<PXRowDeleted> pxRowDeletedList = this.storeCachedDeleteList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Delete(item);
    }
    finally
    {
      foreach (PXRowInserted pxRowInserted in pxRowInsertedList)
        sender.Graph.RowInserted.RemoveHandler<POTax>(pxRowInserted);
      foreach (PXRowDeleted pxRowDeleted in pxRowDeletedList)
        sender.Graph.RowDeleted.RemoveHandler<POTax>(pxRowDeleted);
    }
  }

  protected List<object> getRecordsList(PXCache sender)
  {
    return new List<object>((IEnumerable<object>) GraphHelper.RowCast<POTax>((IEnumerable) PXSelectBase<POTax, PXSelect<POTax, Where<POTax.orderType, Equal<Current<POOrder.orderType>>, And<POTax.orderNbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectMultiBound(sender.Graph, new object[1]
    {
      sender.Graph.Caches[typeof (POOrder).Name].Current
    }, Array.Empty<object>())));
  }

  protected List<PXRowInserted> storeCachedInsertList(
    PXCache sender,
    List<object> recordsList,
    string OrderType,
    string OrderNbr)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.OrderNbr = OrderNbr;
    List<PXRowInserted> pxRowInsertedList = new List<PXRowInserted>();
    // ISSUE: method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cDisplayClass210, __methodptr(\u003CstoreCachedInsertList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.sender.Graph.RowInserted.AddHandler<POTax>(pxRowInserted);
    pxRowInsertedList.Add(pxRowInserted);
    return pxRowInsertedList;
  }

  protected List<PXRowUpdated> storeCachedUpdateList(
    PXCache sender,
    List<object> recordsList,
    string OrderType,
    string OrderNbr)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass22_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.OrderNbr = OrderNbr;
    List<PXRowUpdated> pxRowUpdatedList = new List<PXRowUpdated>();
    // ISSUE: method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cDisplayClass220, __methodptr(\u003CstoreCachedUpdateList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220.sender.Graph.RowUpdated.AddHandler<POTax>(pxRowUpdated);
    pxRowUpdatedList.Add(pxRowUpdated);
    return pxRowUpdatedList;
  }

  protected List<PXRowDeleted> storeCachedDeleteList(
    PXCache sender,
    List<object> recordsList,
    string OrderType,
    string OrderNbr)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass23_0 cDisplayClass230 = new POUnbilledTaxAttribute.\u003C\u003Ec__DisplayClass23_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.OrderNbr = OrderNbr;
    List<PXRowDeleted> pxRowDeletedList = new List<PXRowDeleted>();
    // ISSUE: method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cDisplayClass230, __methodptr(\u003CstoreCachedDeleteList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass230.sender.Graph.RowDeleted.AddHandler<POTax>(pxRowDeleted);
    pxRowDeletedList.Add(pxRowDeleted);
    return pxRowDeletedList;
  }
}
