// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Specialized for SO version of the TaxAttribute. <br />
/// Provides Tax calculation for SOLine, by default is attached to SOLine (details) and SOOrder (header) <br />
/// Normally, should be placed on the TaxCategoryID field. <br />
/// Internally, it uses SOOrder graphs, otherwise taxes are not calculated (tax calc Level is set to NoCalc).<br />
/// As a result of this attribute work a set of SOTax tran related to each SOLine  and to their parent will created <br />
/// May be combined with other attrbibutes with similar type - for example, SOOpenTaxAttribute <br />
/// </summary>
/// <example>
/// [SOTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
/// </example>
public class SOTaxAttribute : TaxAttribute
{
  protected string _DisableAutomaticTaxCalculation = "DisableAutomaticTaxCalculation";

  public Type CuryTaxableAmtField { get; set; }

  protected virtual short SortOrder => 0;

  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  public SOTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : this(ParentType, TaxType, TaxSumType, (Type) null)
  {
  }

  public SOTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType, Type TaxCalculationMode)
    : base(ParentType, TaxType, TaxSumType, TaxCalculationMode)
  {
    this.CuryDocBal = (Type) null;
    this.CuryLineTotal = typeof (SOOrder.curyLineTotal);
    this.CuryTaxTotal = typeof (SOOrder.curyTaxTotal);
    this.DocDate = typeof (SOOrder.orderDate);
    this.CuryTranAmt = typeof (SOLine.curyLineAmt);
    this.GroupDiscountRate = typeof (SOLine.groupDiscountRate);
    this.TaxCalcMode = typeof (SOOrder.taxCalcMode);
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    PXUnboundFormulaAttribute formulaAttribute = new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, SOLine.curyLineAmt>, decimal0>), typeof (SumCalc<SOOrder.curyLineTotal>));
    ((PXFormulaAttribute) formulaAttribute).ValidateAggregateCalculation = true;
    attributes.Add((PXEventSubscriberAttribute) formulaAttribute);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, SOLine.curyExtPrice>, decimal0>), typeof (SumCalc<SOOrder.curyGoodsExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine.lineType, Equal<SOLineType.miscCharge>>, SOLine.curyLineAmt>, decimal0>), typeof (SumCalc<SOOrder.curyMiscTot>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine.lineType, Equal<SOLineType.miscCharge>>, SOLine.curyExtPrice>, decimal0>), typeof (SumCalc<SOOrder.curyMiscExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (SOLine.curyDiscAmt), typeof (SumCalc<SOOrder.curyLineDiscTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine.commissionable, Equal<True>>, Mult<IIf<BqlOperand<SOLine.defaultOperation, IBqlString>.IsEqual<SOOperation.receipt>, decimal_1, decimal1>, Mult<Mult<SOLine.curyLineAmt, SOLine.groupDiscountRate>, SOLine.documentDiscountRate>>>, decimal0>), typeof (SumCalc<SOSalesPerTran.curyCommnblAmt>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Row<SOLine.curyNetSales>), typeof (SumCalc<SOOrder.curyNetSalesTotal>)));
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((SOTaxAttribute) other).SortOrder);
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
  }

  protected override string GetTaxZone(PXCache sender, object row)
  {
    return row != null ? (string) sender.GetValue<SOLine.taxZoneID>(row) : base.GetTaxZone(sender, row);
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal? curyTranAmt = base.GetCuryTranAmt(sender, row);
    Decimal valueOrDefault = curyTranAmt.GetValueOrDefault();
    curyTranAmt = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal num1 = curyTranAmt ?? 1M;
    Decimal num2 = valueOrDefault * num1;
    curyTranAmt = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal num3 = curyTranAmt ?? 1M;
    Decimal val = num2 * num3;
    return new Decimal?(PXDBCurrencyAttribute.Round(sender, row, val, CMPrecision.TRANCURY));
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    base.CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    Decimal num1 = (Decimal) (this.ParentGetValue<SOOrder.curyLineTotal>(sender.Graph) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue<SOOrder.curyMiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num3 = (Decimal) (this.ParentGetValue<SOOrder.curyFreightTot>(sender.Graph) ?? (object) 0M);
    Decimal num4 = (Decimal) (this.ParentGetValue<SOOrder.curyDiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num5 = num2;
    Decimal objA = num1 + num5 + num3 + CuryTaxTotal - CuryInclTaxTotal - num4;
    if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<SOOrder.curyOrderTotal>(sender.Graph) ?? (object) 0M)))
      return;
    this.ParentSetValue<SOOrder.curyOrderTotal>(sender.Graph, (object) objA);
  }

  protected virtual bool IsFreightTaxable(PXCache sender, List<object> taxitems)
  {
    for (int index = 0; index < taxitems.Count; ++index)
    {
      if (PXResult<SOTax>.op_Implicit((PXResult<SOTax>) taxitems[index]).LineNbr.GetValueOrDefault() == 32000)
        return true;
    }
    return false;
  }

  protected override object SelectParent(PXCache sender, object row)
  {
    if (!(row.GetType() == typeof (SOTax)) || ((SOTax) row).LineNbr.GetValueOrDefault() != 32000)
      return base.SelectParent(sender, row);
    if (this._ChildType != typeof (SOOrder))
      return (object) null;
    return (object) PXResultset<SOOrder>.op_Implicit(PXSelectBase<SOOrder, PXSelect<SOOrder, Where<SOOrder.orderType, Equal<Current<SOTax.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOTax.orderNbr>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      row
    }));
  }

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
    Decimal valueOrDefault = ((Decimal?) this.ParentGetValue<SOOrder.curyLineTotal>(sender.Graph)).GetValueOrDefault();
    Decimal num1 = (Decimal) (this.ParentGetValue<SOOrder.curyFreightTot>(sender.Graph) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue<SOOrder.curyDiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num3 = this.IsFreightTaxable(sender, taxitems) ? num1 : 0M;
    if (!(valueOrDefault != 0M) || !(CuryTaxableAmt != 0M))
      return;
    if (Math.Abs(CuryTaxableAmt - num3 - valueOrDefault) < 0.00005M)
    {
      CuryTaxableAmt -= num2;
    }
    else
    {
      if (!(Math.Abs(valueOrDefault + num3 - num2 - CuryTaxableAmt) < this.GetPrecisionBasedNegligibleDifference(sender.Graph, row)))
        return;
      CuryTaxableAmt = valueOrDefault + num3 - num2;
    }
  }

  protected override List<object> SelectTaxesToCalculateTaxSum(
    PXCache sender,
    object row,
    TaxDetail taxdet)
  {
    SOTaxTran soTaxTran = (SOTaxTran) taxdet;
    return this.SelectTaxes<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>, Where<SOTax.taxID, Equal<Required<SOTaxTran.taxID>>, And<SOTax.taxZoneID, Equal<Required<SOTaxTran.taxZoneID>>>>, Current<SOLine.lineNbr>>(sender.Graph, new object[2]
    {
      row,
      sender.Graph.Caches[this._ParentType].Current
    }, PXTaxCheck.RecalcLine, (object) soTaxTran.TaxID, (object) soTaxTran.TaxZoneID);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, Current<SOLine.lineNbr>>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }

  public override object Insert(PXCache sender, object item) => this.InsertCached(sender, item);

  public override object Update(PXCache sender, object item) => this.UpdateCached(sender, item);

  public override object Delete(PXCache sender, object item) => this.DeleteCached(sender, item);

  protected virtual object InsertCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (SOOrder)];
    string OrderType = (string) cach.GetValue<SOOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<SOOrder.orderNbr>(cach.Current);
    List<PXRowInserted> pxRowInsertedList = this.storeCachedInsertList(sender, recordsList, OrderType, OrderNbr);
    List<PXRowDeleted> pxRowDeletedList = this.storeCachedDeleteList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Insert(item);
    }
    finally
    {
      foreach (PXRowInserted pxRowInserted in pxRowInsertedList)
        sender.Graph.RowInserted.RemoveHandler<SOTax>(pxRowInserted);
      foreach (PXRowDeleted pxRowDeleted in pxRowDeletedList)
        sender.Graph.RowDeleted.RemoveHandler<SOTax>(pxRowDeleted);
    }
  }

  protected virtual object UpdateCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (SOOrder)];
    string OrderType = (string) cach.GetValue<SOOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<SOOrder.orderNbr>(cach.Current);
    List<PXRowUpdated> pxRowUpdatedList = this.storeCachedUpdateList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Update(item);
    }
    finally
    {
      foreach (PXRowUpdated pxRowUpdated in pxRowUpdatedList)
        sender.Graph.RowUpdated.RemoveHandler<SOTax>(pxRowUpdated);
    }
  }

  protected virtual object DeleteCached(PXCache sender, object item)
  {
    List<object> recordsList = this.getRecordsList(sender);
    PXCache cach = sender.Graph.Caches[typeof (SOOrder)];
    string OrderType = (string) cach.GetValue<SOOrder.orderType>(cach.Current);
    string OrderNbr = (string) cach.GetValue<SOOrder.orderNbr>(cach.Current);
    List<PXRowInserted> pxRowInsertedList = this.storeCachedInsertList(sender, recordsList, OrderType, OrderNbr);
    List<PXRowDeleted> pxRowDeletedList = this.storeCachedDeleteList(sender, recordsList, OrderType, OrderNbr);
    try
    {
      return sender.Delete(item);
    }
    finally
    {
      foreach (PXRowInserted pxRowInserted in pxRowInsertedList)
        sender.Graph.RowInserted.RemoveHandler<SOTax>(pxRowInserted);
      foreach (PXRowDeleted pxRowDeleted in pxRowDeletedList)
        sender.Graph.RowDeleted.RemoveHandler<SOTax>(pxRowDeleted);
    }
  }

  protected List<object> getRecordsList(PXCache sender)
  {
    return new List<object>((IEnumerable<object>) GraphHelper.RowCast<SOTax>((IEnumerable) PXSelectBase<SOTax, PXSelect<SOTax, Where<SOTax.orderType, Equal<Current<SOOrder.orderType>>, And<SOTax.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectMultiBound(sender.Graph, new object[1]
    {
      sender.Graph.Caches[typeof (SOOrder).Name].Current
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
    SOTaxAttribute.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new SOTaxAttribute.\u003C\u003Ec__DisplayClass28_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.OrderNbr = OrderNbr;
    List<PXRowInserted> pxRowInsertedList = new List<PXRowInserted>();
    // ISSUE: method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) cDisplayClass280, __methodptr(\u003CstoreCachedInsertList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.sender.Graph.RowInserted.AddHandler<SOTax>(pxRowInserted);
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
    SOTaxAttribute.\u003C\u003Ec__DisplayClass29_0 cDisplayClass290 = new SOTaxAttribute.\u003C\u003Ec__DisplayClass29_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.OrderNbr = OrderNbr;
    List<PXRowUpdated> pxRowUpdatedList = new List<PXRowUpdated>();
    // ISSUE: method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) cDisplayClass290, __methodptr(\u003CstoreCachedUpdateList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.sender.Graph.RowUpdated.AddHandler<SOTax>(pxRowUpdated);
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
    SOTaxAttribute.\u003C\u003Ec__DisplayClass30_0 cDisplayClass300 = new SOTaxAttribute.\u003C\u003Ec__DisplayClass30_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.recordsList = recordsList;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.OrderType = OrderType;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.OrderNbr = OrderNbr;
    List<PXRowDeleted> pxRowDeletedList = new List<PXRowDeleted>();
    // ISSUE: method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) cDisplayClass300, __methodptr(\u003CstoreCachedDeleteList\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass300.sender.Graph.RowDeleted.AddHandler<SOTax>(pxRowDeleted);
    pxRowDeletedList.Add(pxRowDeleted);
    return pxRowDeletedList;
  }

  protected List<object> SelectTaxes<Where, Where2, LineNbr>(
    PXGraph graph,
    object[] currents,
    PXTaxCheck taxchk,
    params object[] parameters)
    where Where : IBqlWhere, new()
    where Where2 : IBqlWhere, new()
    where LineNbr : IBqlOperand
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Current<SOOrder.orderDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<SOTax>((IEnumerable) PXSelectBase<SOTax, PXSelect<SOTax, Where<SOTax.orderType, Equal<Current<SOOrder.orderType>>, And<SOTax.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters);
        int? nullable1 = new int?(int.MinValue);
        Type genericArgument;
        if (currents.Length != 0 && currents[0] != null && typeof (LineNbr).IsGenericType && typeof (LineNbr).GetGenericTypeDefinition() == typeof (Current<>) && (genericArgument = typeof (LineNbr).GetGenericArguments()[0]).IsNested && currents[0].GetType() == BqlCommand.GetItemType(genericArgument))
          nullable1 = (int?) graph.Caches[BqlCommand.GetItemType(genericArgument)].GetValue(currents[0], genericArgument.Name);
        if (typeof (IConstant<int>).IsAssignableFrom(typeof (LineNbr)))
          nullable1 = new int?(((IConstant<int>) Activator.CreateInstance(typeof (LineNbr))).Value);
        foreach (SOTax record in list1)
        {
          int? lineNbr = record.LineNbr;
          int? nullable2 = nullable1;
          if (lineNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & lineNbr.HasValue == nullable2.HasValue)
            this.InsertTax<SOTax>(graph, taxchk, record, tails1, taxList);
        }
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<SOTax>((IEnumerable) PXSelectBase<SOTax, PXSelect<SOTax, Where<SOTax.orderType, Equal<Current<SOOrder.orderType>>, And<SOTax.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>())).OrderBy<SOTax, int?>((Func<SOTax, int?>) (_ => _.LineNbr)).ThenBy<SOTax, string>((Func<SOTax, string>) (_ => _.TaxID))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters);
        foreach (SOTax record in list2)
        {
          if (this.Meet<Where2>(graph, record, ((IEnumerable<object>) parameters).ToList<object>()))
          {
            if (record.LineNbr.GetValueOrDefault() != int.MaxValue)
              this.InsertTax<SOTax>(graph, taxchk, record, tails2, taxList);
            else
              break;
          }
        }
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<SOTaxTran>((IEnumerable) PXSelectBase<SOTaxTran, PXSelect<SOTaxTran, Where<SOTaxTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters);
        foreach (SOTaxTran record in list3)
          this.InsertTax<SOTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected bool Meet<Where2>(PXGraph graph, SOTax record, List<object> parameters) where Where2 : IBqlWhere, new()
  {
    IBqlWhere instance = (IBqlWhere) Activator.CreateInstance(typeof (Where2));
    object obj = (object) null;
    bool? nullable = new bool?();
    PXCache cach = graph.Caches[this._TaxType];
    SOTax soTax = record;
    List<object> objectList = parameters;
    ref bool? local1 = ref nullable;
    ref object local2 = ref obj;
    ((IBqlUnary) instance).Verify(cach, (object) soTax, objectList, ref local1, ref local2);
    return nullable.GetValueOrDefault();
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    base.RowInserted(sender, e);
    if (this._TaxCalc != TaxCalc.ManualCalc || (this._TaxFlags & TaxCalc.RedefaultAlways) != TaxCalc.RedefaultAlways)
      return;
    this.Preload(sender);
    this.DefaultTaxes(sender, e.Row);
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    base.RowUpdated(sender, e);
    if (this._TaxCalc == TaxCalc.Calc && (this._TaxFlags & TaxCalc.RecalculateAlways) == TaxCalc.RecalculateAlways && !this.ShouldRecalculateTaxesOnRowUpdate(sender, e.Row, e.OldRow))
    {
      this.CalcTaxes(sender, e.Row, PXTaxCheck.Line);
      this.CalcTotals(sender, e.Row, false);
    }
    if (this._TaxCalc != TaxCalc.ManualCalc || (this._TaxFlags & TaxCalc.RedefaultAlways) != TaxCalc.RedefaultAlways || object.Equals((object) this.GetTaxCategory(sender, e.OldRow), (object) this.GetTaxCategory(sender, e.Row)))
      return;
    this.Preload(sender);
    this.ReDefaultTaxes(sender, e.OldRow, e.Row);
  }

  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    base.RowDeleted(sender, e);
    if (this._TaxCalc != TaxCalc.ManualCalc || (this._TaxFlags & TaxCalc.RedefaultAlways) != TaxCalc.RedefaultAlways)
      return;
    this.ClearTaxes(sender, e.Row);
  }

  protected override void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    base.ZoneUpdated(sender, e);
    if (this._TaxCalc != TaxCalc.ManualCalc || !(sender.Graph is SOOrderEntry graph) || graph.IsCopyOrder)
      return;
    PXCache cach = sender.Graph.Caches[this._ChildType];
    if (this.CompareZone(sender.Graph, (string) e.OldValue, (string) sender.GetValue(e.Row, this._TaxZoneID)) && sender.GetValue(e.Row, this._TaxZoneID) != null)
      return;
    this.Preload(sender);
    List<object> details = this.ChildSelect(cach, e.Row);
    this.ReDefaultTaxes(cach, details);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return this.SelectDocumentLines<SOLine, SOLine.orderType, SOLine.orderNbr>(graph, row).Union<SOLine>(SOTaxAttribute.FreightToSOLine<SOLine>(graph)).Cast<object>().ToList<object>();
  }

  protected IEnumerable<TSOLine> SelectDocumentLines<TSOLine, TSOLineOrderType, TSOLineOrderNbr>(
    PXGraph graph,
    object row)
    where TSOLine : class, IBqlTable, new()
    where TSOLineOrderType : IBqlOperand
    where TSOLineOrderNbr : IBqlOperand
  {
    return GraphHelper.RowCast<TSOLine>((IEnumerable) PXSelectBase<TSOLine, PXSelect<TSOLine, Where<TSOLineOrderType, Equal<Current<SOOrder.orderType>>, And<TSOLineOrderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>()));
  }

  protected static IEnumerable<TSOLine> FreightToSOLine<TSOLine>(PXGraph graph) where TSOLine : class, IBqlTable, new()
  {
    if (graph is SOOrderEntry soOrderEntry)
    {
      SOOrder current = ((PXSelectBase<SOOrder>) soOrderEntry.Document).Current;
      if (current != null)
      {
        PXCache<TSOLine> pxCache = GraphHelper.Caches<TSOLine>(graph);
        TSOLine instance = (TSOLine) ((PXCache) pxCache).CreateInstance();
        ((PXCache) pxCache).SetValue<SOLine.orderType>((object) instance, (object) current.OrderType);
        ((PXCache) pxCache).SetValue<SOLine.orderNbr>((object) instance, (object) current.OrderNbr);
        ((PXCache) pxCache).SetValue<SOLine.lineNbr>((object) instance, (object) 32000);
        ((PXCache) pxCache).SetValue<SOLine.taxCategoryID>((object) instance, (object) current.FreightTaxCategoryID);
        ((PXCache) pxCache).SetValue<SOLine.curyExtPrice>((object) instance, (object) current.CuryFreightTot);
        ((PXCache) pxCache).SetValue<SOLine.extPrice>((object) instance, (object) current.FreightTot);
        ((PXCache) pxCache).SetValue<SOLine.curyDiscAmt>((object) instance, (object) 0M);
        ((PXCache) pxCache).SetValue<SOLine.discAmt>((object) instance, (object) 0M);
        ((PXCache) pxCache).SetValue<SOLine.groupDiscountRate>((object) instance, (object) 1M);
        ((PXCache) pxCache).SetValue<SOLine.documentDiscountRate>((object) instance, (object) 1M);
        yield return instance;
      }
    }
  }

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType = "I")
  {
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, typeof (SOLine.curyExtPrice).Name);
    Decimal? nullable2 = (Decimal?) sender.GetValue(row, typeof (SOLine.curyDiscAmt).Name);
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, typeof (SOLine.documentDiscountRate).Name);
    Decimal? nullable4 = (Decimal?) sender.GetValue(row, typeof (SOLine.groupDiscountRate).Name);
    Decimal? nullable5 = nullable1;
    Decimal? nullable6 = nullable2;
    Decimal? nullable7 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable8 = nullable3;
    Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable10 = nullable4;
    return !(nullable9.HasValue & nullable10.HasValue) ? new Decimal?() : new Decimal?(nullable9.GetValueOrDefault() * nullable10.GetValueOrDefault());
  }

  protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    if (this.SortOrder != (short) 0)
      return;
    base.DefaultTaxes(sender, row, DefaultExisting);
  }

  protected override void ClearTaxes(PXCache sender, object row)
  {
    if (this.SortOrder != (short) 0)
      return;
    base.ClearTaxes(sender, row);
  }

  public override void CacheAttached(PXCache sender)
  {
    this._ChildType = sender.GetItemType();
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
    if (sender.Graph is SOOrderEntry)
    {
      base.CacheAttached(sender);
      PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
      Type type1 = typeof (SOOrder);
      SOTaxAttribute soTaxAttribute1 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated = new PXRowUpdated((object) soTaxAttribute1, __vmethodptr(soTaxAttribute1, SOOrder_RowUpdated));
      rowUpdated.AddHandler(type1, pxRowUpdated);
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type type2 = typeof (SOOrder);
      string curyTaxTotal = this._CuryTaxTotal;
      SOTaxAttribute soTaxAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) soTaxAttribute2, __vmethodptr(soTaxAttribute2, SOOrder_CuryTaxTot_FieldUpdated));
      fieldUpdated.AddHandler(type2, curyTaxTotal, pxFieldUpdated);
    }
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected override void OnZoneUpdated(TaxBaseAttribute.ZoneUpdatedArgs e)
  {
    foreach (object detail in e.Details)
    {
      if ((string) e.Cache.GetValue<SOLine.behavior>(detail) != "BL")
      {
        e.Cache.SetValue<SOLine.taxZoneID>(detail, (object) e.NewValue);
        GraphHelper.MarkUpdated(e.Cache, detail, true);
      }
    }
    base.OnZoneUpdated(e);
  }

  protected override bool CompareZone(PXGraph graph, string zoneA, string zoneB)
  {
    return string.Equals(zoneA, zoneB, StringComparison.OrdinalIgnoreCase);
  }

  protected override void AddOneTax(PXCache sender, object detrow, ITaxDetail taxitem)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOTaxAttribute.\u003C\u003Ec__DisplayClass46_0 cDisplayClass460 = new SOTaxAttribute.\u003C\u003Ec__DisplayClass46_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass460.detrow = detrow;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass460.childcache = sender.Graph.Caches[this._ChildType];
    // ISSUE: method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) cDisplayClass460, __methodptr(\u003CAddOneTax\u003Eb__0));
    sender.Graph.RowInserting.AddHandler(this._TaxType, pxRowInserting);
    try
    {
      // ISSUE: reference to a compiler-generated field
      base.AddOneTax(sender, cDisplayClass460.detrow, taxitem);
    }
    finally
    {
      sender.Graph.RowInserting.RemoveHandler(this._TaxType, pxRowInserting);
    }
  }

  protected override void AddTaxTotals(PXCache sender, string taxID, object row)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOTaxAttribute.\u003C\u003Ec__DisplayClass47_0 cDisplayClass470 = new SOTaxAttribute.\u003C\u003Ec__DisplayClass47_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass470.row = row;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass470.childcache = sender.Graph.Caches[this._ChildType];
    // ISSUE: method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) cDisplayClass470, __methodptr(\u003CAddTaxTotals\u003Eb__0));
    sender.Graph.RowInserting.AddHandler(this._TaxSumType, pxRowInserting);
    try
    {
      // ISSUE: reference to a compiler-generated field
      base.AddTaxTotals(sender, taxID, cDisplayClass470.row);
    }
    finally
    {
      sender.Graph.RowInserting.RemoveHandler(this._TaxSumType, pxRowInserting);
    }
  }

  protected override IEnumerable<ITaxDetail> ManualTaxes(PXCache sender, object row)
  {
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    foreach (PXResult selectTax in this.SelectTaxes(sender, row, PXTaxCheck.RecalcTotals))
    {
      if (row is SOLine soLine && selectTax[0] is SOTaxTran soTaxTran && soLine.TaxZoneID != null && soLine.TaxZoneID == soTaxTran.TaxZoneID)
        taxDetailList.Add((ITaxDetail) selectTax[0]);
    }
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  protected virtual void SOOrder_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<SOOrder.curyDiscTot, SOOrder.curyFreightTot>(e.Row, e.OldRow))
      return;
    bool CalcTaxes = true;
    if (this.SkipInternalTaxCalculation(sender, e.Row))
      CalcTaxes = false;
    this._ParentRow = e.Row;
    this.CalcTotals(sender, e.Row, CalcTaxes);
    this._ParentRow = (object) null;
  }

  protected virtual void SOOrder_CuryTaxTot_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!this.SkipInternalTaxCalculation(sender, e.Row))
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    Decimal? nullable3 = nullable1;
    Decimal? oldValue = (Decimal?) e.OldValue;
    if (nullable3.GetValueOrDefault() == oldValue.GetValueOrDefault() & nullable3.HasValue == oldValue.HasValue)
      return;
    this._ParentRow = e.Row;
    this.CalcDocTotals(sender, e.Row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
    this._ParentRow = (object) null;
  }

  private bool SkipInternalTaxCalculation(PXCache sender, object row)
  {
    string taxZoneID = (string) sender.GetValue(row, this._TaxZoneID);
    if (this.IsExternalTax(sender.Graph, taxZoneID))
      return true;
    if (row == null)
      return false;
    return this.IsManualExternalTax(sender.Graph, taxZoneID) && ((SOOrder) row).ExternalTaxesImportInProgress.GetValueOrDefault() || ((SOOrder) row).DisableAutomaticTaxCalculation.GetValueOrDefault();
  }

  public virtual bool IsManualExternalTax(PXGraph graph, string taxZoneID)
  {
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelect<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<PX.Objects.TX.TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    }));
    return taxZone != null && taxZone.IsExternal.GetValueOrDefault() && string.IsNullOrEmpty(taxZone.TaxPluginID);
  }

  public override bool IsTaxCalculationNeeded(PXCache sender, object row)
  {
    int num1 = row == null ? 1 : (!((bool?) sender.GetValue(row, this._DisableAutomaticTaxCalculation)).GetValueOrDefault() ? 1 : 0);
    bool? nullable;
    int num2;
    if (sender.Current != null)
    {
      nullable = (bool?) sender.GetValue(sender.Current, this._DisableAutomaticTaxCalculation);
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 1;
    bool flag1 = num2 != 0;
    PXCache pxCache = this.ParentCache(sender.Graph);
    int num3;
    if (pxCache.Current != null)
    {
      nullable = (bool?) pxCache.GetValue(pxCache.Current, this._DisableAutomaticTaxCalculation);
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    bool flag2 = num3 != 0;
    int num4 = flag1 ? 1 : 0;
    return (num1 & num4 & (flag2 ? 1 : 0)) != 0;
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    if (!(this.CuryTaxableAmtField != (Type) null))
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this.CuryTaxableAmtField.Name);
    Decimal? nullable2 = value;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    object copy = sender.CreateCopy(row);
    sender.SetValueExt(row, this.CuryTaxableAmtField.Name, (object) value);
    if (this._TaxCalc != TaxCalc.ManualLineCalc)
      return;
    ((IEnumerable) this._Attributes).OfType<PXFormulaAttribute>().FirstOrDefault<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (x => x.ParentField == typeof (SOOrder.curyNetSalesTotal)))?.RowUpdated(sender, new PXRowUpdatedEventArgs(row, copy, false));
  }
}
