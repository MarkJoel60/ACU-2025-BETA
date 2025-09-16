// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.Attributes.POLandedCostTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts.Attributes;

public class POLandedCostTaxAttribute : TaxAttribute
{
  private Dictionary<object, Dictionary<string, object>> totals;

  protected virtual short SortOrder => 0;

  public POLandedCostTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryDocBal = typeof (POLandedCostDoc.curyDocTotal);
    this.CuryLineTotal = typeof (POLandedCostDoc.curyLineTotal);
    this.DocDate = typeof (POLandedCostDoc.docDate);
    this.CuryTaxTotal = typeof (POLandedCostDoc.curyTaxTotal);
    this.CuryOrigDiscAmt = typeof (POLandedCostDoc.curyDiscAmt);
    this.CuryTranAmt = typeof (POLandedCostDetail.curyLineAmt);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (POLandedCostDetail.curyLineAmt), typeof (SumCalc<POLandedCostDoc.curyLineTotal>)));
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((POLandedCostTaxAttribute) other).SortOrder);
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    Decimal? curyTranAmt = base.GetCuryTranAmt(sender, row);
    Decimal? nullable = curyTranAmt;
    Decimal num = 0M;
    return !(nullable.GetValueOrDefault() == num & nullable.HasValue) ? new Decimal?(sender.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().RoundCury(curyTranAmt.Value)) : curyTranAmt;
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
  }

  protected override List<object> SelectTaxes<TWhere>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<TWhere, Current<POLandedCostDetail.lineNbr>>(graph, new object[2]
    {
      row,
      (object) ((PXSelectBase<POLandedCostDoc>) ((POLandedCostDocEntry) graph).Document).Current
    }, taxchk, parameters);
  }

  protected List<object> SelectTaxes<TWhere, TLineNbr>(
    PXGraph graph,
    object[] currents,
    PXTaxCheck taxchk,
    params object[] parameters)
    where TWhere : IBqlWhere, new()
    where TLineNbr : IBqlOperand
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Where<PX.Objects.TX.Tax.reverseTax, Equal<True>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>>>>, And<Current<POLandedCostDoc.docDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POLandedCostTax>((IEnumerable) PXSelectBase<POLandedCostTax, PXSelect<POLandedCostTax, Where<POLandedCostTax.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostTax.refNbr, Equal<Current<POLandedCostDoc.refNbr>>, And<POLandedCostTax.lineNbr, Equal<TLineNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<TWhere>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters);
        foreach (POLandedCostTax record in list1)
          this.InsertTax<POLandedCostTax>(graph, taxchk, record, tails1, taxList);
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POLandedCostTax>((IEnumerable) PXSelectBase<POLandedCostTax, PXSelect<POLandedCostTax, Where<POLandedCostTax.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostTax.refNbr, Equal<Current<POLandedCostDoc.refNbr>>, And<POLandedCostTax.lineNbr, Less<intMax>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<TWhere>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters);
        foreach (POLandedCostTax record in list2)
          this.InsertTax<POLandedCostTax>(graph, taxchk, record, tails2, taxList);
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<POLandedCostTaxTran>((IEnumerable) PXSelectBase<POLandedCostTaxTran, PXSelect<POLandedCostTaxTran, Where<POLandedCostTaxTran.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostTaxTran.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<TWhere>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters);
        foreach (POLandedCostTaxTran record in list3)
          this.InsertTax<POLandedCostTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  /// <summary>
  /// The purpose of this handler is to workaround situations where exception was thrown in RowUpdated
  /// event chain and formulas that are embedded inside SOTaxAttribute will fail to calculate totals.
  /// Resetting totals to null will force formulas to calculate on all the details rather then calculate
  /// on the difference from previous incorrect result.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.GetItemType() != typeof (POLandedCostDetail))
    {
      base.RowUpdated(sender, e);
    }
    else
    {
      IEnumerable<Type> source = ((IEnumerable) this._Attributes).OfType<PXUnboundFormulaAttribute>().Where<PXUnboundFormulaAttribute>((Func<PXUnboundFormulaAttribute, bool>) (a => ((PXFormulaAttribute) a).ParentField.DeclaringType == typeof (POLandedCostDoc))).Select<PXUnboundFormulaAttribute, Type>((Func<PXUnboundFormulaAttribute, Type>) (a => ((PXFormulaAttribute) a).ParentField));
      Dictionary<string, object> header;
      if (this._TaxCalc == TaxCalc.ManualLineCalc && !this.totals.TryGetValue(e.Row, out header))
      {
        this.totals[e.Row] = header = new Dictionary<string, object>();
        EnumerableExtensions.ForEach<Type>(source, (Action<Type>) (a =>
        {
          PXCache pxCache = this.ParentCache(sender.Graph);
          object obj = pxCache.GetValue(pxCache.Current, a.Name);
          if (obj == null)
            return;
          header[a.Name] = obj;
          pxCache.SetValue(pxCache.Current, a.Name, (object) null);
        }));
      }
      if (this._TaxCalc == TaxCalc.Calc && this.totals.TryGetValue(e.Row, out header))
      {
        EnumerableExtensions.ForEach<Type>(source.Where<Type>((Func<Type, bool>) (a => header.ContainsKey(a.Name))), (Action<Type>) (a =>
        {
          PXCache pxCache = this.ParentCache(sender.Graph);
          pxCache.SetValue(pxCache.Current, a.Name, header[a.Name]);
        }));
        this.totals.Remove(e.Row);
      }
      base.RowUpdated(sender, e);
    }
  }

  public override void CacheAttached(PXCache sender)
  {
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
    this.totals = new Dictionary<object, Dictionary<string, object>>();
    if (this.EnableTaxCalcOn(sender.Graph))
    {
      base.CacheAttached(sender);
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type type = typeof (POLandedCostDoc);
      string curyTaxTotal = this._CuryTaxTotal;
      POLandedCostTaxAttribute costTaxAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) costTaxAttribute, __vmethodptr(costTaxAttribute, POLandedCostDoc_CuryTaxTot_FieldUpdated));
      fieldUpdated.AddHandler(type, curyTaxTotal, pxFieldUpdated);
    }
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected virtual void POLandedCostDoc_CuryDiscTot_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    bool CalcTaxes = true;
    if (this.IsExternalTax(sender.Graph, (string) sender.GetValue(e.Row, this._TaxZoneID)))
      CalcTaxes = false;
    this._ParentRow = e.Row;
    if (this._TaxCalc != TaxCalc.ManualLineCalc || !this.totals.Any<KeyValuePair<object, Dictionary<string, object>>>())
      this.CalcTotals(sender, e.Row, CalcTaxes);
    this._ParentRow = (object) null;
  }

  protected virtual bool EnableTaxCalcOn(PXGraph aGraph) => aGraph is POLandedCostDocEntry;

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
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal num = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    Decimal objA1 = this.CalcLineTotal(sender, row);
    Decimal objA2 = objA1 + CuryTaxTotal - CuryInclTaxTotal - num;
    Decimal objB1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M);
    Decimal objB2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    if (!object.Equals((object) objA1, (object) objB1) || !object.Equals((object) CuryTaxTotal, (object) objB2))
    {
      this.ParentSetValue(sender.Graph, this._CuryLineTotal, (object) objA1);
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) CuryTaxTotal);
      if (!string.IsNullOrEmpty(this._CuryDocBal))
      {
        this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
        return;
      }
    }
    if (string.IsNullOrEmpty(this._CuryDocBal))
      return;
    Decimal objB3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
    if (object.Equals((object) objA2, (object) objB3))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
  }

  protected virtual void POLandedCostDoc_CuryTaxTot_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    this.CalcDocTotals(sender, e.Row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
  }

  protected override bool SkipDirectTax(PXCache sender, object row, string applicableDirectTaxId)
  {
    if (!(row is POLandedCostDetail landedCostDetail) || !(landedCostDetail.AllocationMethod != "N"))
      return false;
    sender.RaiseExceptionHandling<POLandedCostDetail.taxCategoryID>((object) landedCostDetail, (object) landedCostDetail.TaxCategoryID, (Exception) new PXSetPropertyException("The {0} direct-entry tax can be applied only to a landed cost line with the None allocation method.", (PXErrorLevel) 2, new object[1]
    {
      (object) applicableDirectTaxId
    }));
    return true;
  }
}
