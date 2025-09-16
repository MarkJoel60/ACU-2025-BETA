// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO;

public class POTaxAttribute : TaxAttribute
{
  protected 
  #nullable disable
  Type CuryRetainageAmt = typeof (POTaxAttribute.curyRetainageAmt);

  protected string _CuryRetainageAmt => this.CuryRetainageAmt.Name;

  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  protected override bool AskRecalculationOnCalcModeChange
  {
    get => false;
    set => base.AskRecalculationOnCalcModeChange = value;
  }

  protected virtual short SortOrder => 0;

  public POTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryDocBal = typeof (POOrder.curyOrderTotal);
    this.CuryLineTotal = typeof (POOrder.curyLineTotal);
    this.DocDate = typeof (POOrder.orderDate);
    this.CuryTranAmt = typeof (POLine.curyExtCost);
    this.GroupDiscountRate = typeof (POLine.groupDiscountRate);
    this.CuryTaxTotal = typeof (POOrder.curyTaxTotal);
    this.CuryDiscTot = typeof (POOrder.curyDiscTot);
    this.TaxCalcMode = typeof (POOrder.taxCalcMode);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<POLine.lineType, NotIn3<POLineType.freight, POLineType.description, POLineType.service>>, POLine.curyLineAmt>, decimal0>), typeof (SumCalc<POOrder.curyGoodsExtCostTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<POLine.lineType, Equal<POLineType.service>>, POLine.curyLineAmt>, decimal0>), typeof (SumCalc<POOrder.curyServiceExtCostTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<POLine.lineType, Equal<POLineType.freight>>, POLine.curyLineAmt>, decimal0>), typeof (SumCalc<POOrder.curyFreightTot>)));
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    PXUnboundFormulaAttribute formulaAttribute = new PXUnboundFormulaAttribute(typeof (POLine.curyExtCost), typeof (SumCalc<POOrder.curyLineTotal>));
    ((PXFormulaAttribute) formulaAttribute).ValidateAggregateCalculation = true;
    attributes.Add((PXEventSubscriberAttribute) formulaAttribute);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (POLine.curyDiscAmt), typeof (SumCalc<POOrder.curyLineDiscTotal>)));
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((POTaxAttribute) other).SortOrder);
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal num1 = this.IsRetainedTaxes(sender.Graph) ? 0M : (Decimal) (sender.GetValue(row, this._CuryRetainageAmt) ?? (object) 0M);
    Decimal num2 = base.GetCuryTranAmt(sender, row).GetValueOrDefault() + num1;
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    Decimal? nullable6 = nullable5;
    Decimal num3 = 0M;
    return !(nullable6.GetValueOrDefault() == num3 & nullable6.HasValue) ? new Decimal?(sender.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(nullable5.Value)) : new Decimal?(0M);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    PXCache pxCache = sender;
    object obj = row;
    int num1;
    if (value.HasValue)
    {
      Decimal? nullable = value;
      Decimal num2 = 0M;
      num1 = !(nullable.GetValueOrDefault() == num2 & nullable.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    // ISSUE: variable of a boxed type
    __Boxed<bool> local = (ValueType) (bool) num1;
    pxCache.SetValue<POLine.hasInclusiveTaxes>(obj, (object) local);
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
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
      (object) ((PXSelectBase<POOrder>) ((POOrderEntry) graph).Document).Current
    }, taxchk, parameters);
  }

  protected List<object> SelectTaxes<Where, LineNbr>(
    PXGraph graph,
    object[] currents,
    PXTaxCheck taxchk,
    params object[] parameters)
    where Where : IBqlWhere, new()
    where LineNbr : IBqlOperand
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Where<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.perUnit>>>>>>>>>, And<Current<POOrder.orderDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POTax>((IEnumerable) PXSelectBase<POTax, PXSelect<POTax, Where<POTax.orderType, Equal<Current<POOrder.orderType>>, And<POTax.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POTax.lineNbr, Equal<LineNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters);
        foreach (POTax record in list1)
          this.InsertTax<POTax>(graph, taxchk, record, tails1, taxList);
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POTax>((IEnumerable) PXSelectBase<POTax, PXSelect<POTax, Where<POTax.orderType, Equal<Current<POOrder.orderType>>, And<POTax.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POTax.lineNbr, Less<intMax>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>())).OrderBy<POTax, int?>((Func<POTax, int?>) (_ => _.LineNbr)).ThenBy<POTax, string>((Func<POTax, string>) (_ => _.TaxID))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters);
        foreach (POTax record in list2)
          this.InsertTax<POTax>(graph, taxchk, record, tails2, taxList);
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POTaxTran>((IEnumerable) PXSelectBase<POTaxTran, PXSelect<POTaxTran, Where<POTaxTran.orderType, Equal<Current<POOrder.orderType>>, And<POTaxTran.orderNbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters);
        foreach (POTaxTran record in list3)
          this.InsertTax<POTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<POLine>((IEnumerable) PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<POLine, object>((Func<POLine, object>) (_ => (object) _)).ToList<object>();
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

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType)
  {
    Decimal valueOrDefault = base.GetCuryTranAmt(sender, row).GetValueOrDefault();
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, this._GroupDiscountRate);
    Decimal? nullable2 = (Decimal?) sender.GetValue(row, this._DocumentDiscountRate);
    Decimal num = valueOrDefault;
    Decimal? nullable3 = nullable1;
    Decimal? nullable4 = nullable3.HasValue ? new Decimal?(num * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable2;
    return new Decimal?((nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?()).Value);
  }

  public override void CacheAttached(PXCache sender)
  {
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
    if (this.EnableTaxCalcOn(sender.Graph))
    {
      base.CacheAttached(sender);
      PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
      Type type1 = typeof (POOrder);
      string curyTaxTotal = this._CuryTaxTotal;
      POTaxAttribute poTaxAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) poTaxAttribute1, __vmethodptr(poTaxAttribute1, POOrder_CuryTaxTot_FieldUpdated));
      fieldUpdated1.AddHandler(type1, curyTaxTotal, pxFieldUpdated1);
      PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
      Type type2 = typeof (POOrder);
      string name = typeof (POOrder.curyDiscTot).Name;
      POTaxAttribute poTaxAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) poTaxAttribute2, __vmethodptr(poTaxAttribute2, POOrder_CuryDiscTot_FieldUpdated));
      fieldUpdated2.AddHandler(type2, name, pxFieldUpdated2);
    }
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected virtual void POOrder_CuryDiscTot_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool CalcTaxes = true;
    if (this.IsExternalTax(sender.Graph, (string) sender.GetValue(e.Row, this._TaxZoneID)) || e.Row != null && ((POOrder) e.Row).ExternalTaxesImportInProgress.GetValueOrDefault())
      CalcTaxes = false;
    this._ParentRow = e.Row;
    this.CalcTotals(sender, e.Row, CalcTaxes);
    this._ParentRow = (object) null;
  }

  protected virtual bool EnableTaxCalcOn(PXGraph aGraph) => aGraph is POOrderEntry;

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    base.CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    if (this.ParentGetStatus(sender.Graph) == 3)
      return;
    Decimal objB = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryWhTaxTotal) ?? (object) 0M);
    if (object.Equals((object) CuryWhTaxTotal, (object) objB))
      return;
    this.ParentSetValue(sender.Graph, this._CuryWhTaxTotal, (object) CuryWhTaxTotal);
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal curyTaxTotal,
    Decimal curyInclTaxTotal,
    Decimal curyWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal num1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    Decimal num2 = this.CalcLineTotal(sender, row) + curyTaxTotal - curyInclTaxTotal - num1;
    Decimal num3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    Decimal num4 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
    if (curyTaxTotal != num3)
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) curyTaxTotal);
    if (string.IsNullOrEmpty(this._CuryDocBal) || !(num2 != num4))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) num2);
  }

  protected virtual void POOrder_CuryTaxTot_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!this.IsExternalTax(sender.Graph, (string) sender.GetValue(e.Row, this._TaxZoneID)) && (e.Row == null || !((POOrder) e.Row).ExternalTaxesImportInProgress.GetValueOrDefault()))
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    if (!(((Decimal?) sender.GetValue(e.Row, this._CuryLineTotal)).GetValueOrDefault() == 0M))
      return;
    this.CalcDocTotals(sender, e.Row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
  }

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
    Decimal valueOrDefault = ((Decimal?) this.ParentGetValue<POOrder.curyLineTotal>(sender.Graph)).GetValueOrDefault();
    Decimal num = (Decimal) (this.ParentGetValue<POOrder.curyDiscTot>(sender.Graph) ?? (object) 0M);
    if (!(valueOrDefault != 0M) || !(CuryTaxableAmt != 0M))
      return;
    if (Math.Abs(CuryTaxableAmt - valueOrDefault) < 0.00005M)
    {
      CuryTaxableAmt -= num;
    }
    else
    {
      if (!(Math.Abs(valueOrDefault - num - CuryTaxableAmt) < this.GetPrecisionBasedNegligibleDifference(sender.Graph, row)))
        return;
      CuryTaxableAmt = valueOrDefault - num;
    }
  }

  protected override bool IsRetainedTaxes(PXGraph graph)
  {
    APSetup current = graph.Caches[typeof (APSetup)].Current as APSetup;
    POOrder poOrder = this.ParentRow(graph) as POOrder;
    return PXAccess.FeatureInstalled<FeaturesSet.retainage>() && poOrder != null && poOrder.RetainageApply.GetValueOrDefault() && current != null && current.RetainTaxes.GetValueOrDefault();
  }

  protected abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxAttribute.curyRetainageAmt>
  {
  }
}
