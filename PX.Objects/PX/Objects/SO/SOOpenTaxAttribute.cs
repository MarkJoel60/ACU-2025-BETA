// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOpenTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Extends <see cref="T:PX.Objects.SO.SOTaxAttribute" /> and calculates CuryOpenOrderTotal for the Parent(Header) SOOrder.
/// </summary>
/// <example>
/// [SOOpenTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
/// </example>
public class SOOpenTaxAttribute : SOTaxAttribute
{
  protected override short SortOrder => 1;

  public SOOpenTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnshippedTaxableAmt).Name;
    this._CuryExemptedAmt = typeof (SOTax.curyUnshippedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnshippedTaxAmt).Name;
    this.CuryDocBal = typeof (SOOrder.curyOpenOrderTotal);
    this.CuryLineTotal = typeof (SOOrder.curyOpenLineTotal);
    this.CuryTaxTotal = typeof (SOOrder.curyOpenTaxTotal);
    this.DocDate = typeof (SOOrder.orderDate);
    this.CuryTranAmt = typeof (SOLine.curyOpenAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (SOLine.curyOpenAmt), typeof (SumCalc<SOOrder.curyOpenLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Mult<SOLine.curyOpenAmt, Sub<decimal1, Mult<SOLine.groupDiscountRate, SOLine.documentDiscountRate>>>), typeof (SumCalc<SOOrder.curyOpenDiscTotal>)));
  }

  protected override string TaxableQtyFieldNameForTaxDetail => "UnshippedTaxableQty";

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

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType = "I")
  {
    Decimal? nullable1 = (Decimal?) sender.GetValue(row, typeof (SOLine.curyOpenAmt).Name);
    Decimal? nullable2 = (Decimal?) sender.GetValue(row, typeof (SOLine.documentDiscountRate).Name);
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, typeof (SOLine.groupDiscountRate).Name);
    Decimal? nullable4 = nullable1;
    Decimal? nullable5 = nullable2;
    Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = nullable3;
    return !(nullable6.HasValue & nullable7.HasValue) ? new Decimal?() : new Decimal?(nullable6.GetValueOrDefault() * nullable7.GetValueOrDefault());
  }

  protected override TaxDetail GetTaxDetail(
    PXCache sender,
    object taxrow,
    object row,
    out bool NeedUpdate)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cach = sender.Graph.Caches[this._TaxSumType];
    TaxDetail copy = (TaxDetail) ((PXResult) taxrow)[0];
    if (NeedUpdate = !this.IsTaxCalculationNeeded(sender, row))
    {
      copy = (TaxDetail) cach.CreateCopy((object) copy);
      Decimal? nullable1 = (Decimal?) this.ParentGetValue<SOOrder.curyOpenLineTotal>(sender.Graph);
      int? nullable2 = (int?) this.ParentGetValue<SOOrder.billedCntr>(sender.Graph);
      int? nullable3 = (int?) this.ParentGetValue<SOOrder.releasedCntr>(sender.Graph);
      bool? nullable4 = (bool?) this.ParentGetValue<SOOrder.orderTaxAllocated>(sender.Graph);
      Decimal? nullable5 = nullable1;
      Decimal num1 = 0M;
      if (!(nullable5.GetValueOrDefault() == num1 & nullable5.HasValue))
      {
        int? nullable6 = nullable2;
        int? nullable7 = nullable3;
        int? nullable8 = nullable6.HasValue & nullable7.HasValue ? new int?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new int?();
        int num2 = 0;
        if (!(nullable8.GetValueOrDefault() > num2 & nullable8.HasValue) || !nullable4.GetValueOrDefault())
        {
          cach.SetValue((object) copy, this._CuryTaxableAmt, cach.GetValue<SOTaxTran.curyTaxableAmt>((object) copy));
          cach.SetValue((object) copy, this._CuryTaxAmt, cach.GetValue<SOTaxTran.curyTaxAmt>((object) copy));
          goto label_7;
        }
      }
      cach.SetValue((object) copy, this._CuryTaxableAmt, (object) 0M);
      cach.SetValue((object) copy, this._CuryTaxAmt, (object) 0M);
    }
label_7:
    return copy;
  }

  protected override bool IsTaxRowAmountUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Decimal? nullable1 = sender.GetValue<SOTaxTran.curyTaxAmt>(e.OldRow) as Decimal?;
    Decimal? nullable2 = sender.GetValue<SOTaxTran.curyTaxAmt>(e.Row) as Decimal?;
    Decimal? nullable3 = sender.GetValue<SOTaxTran.curyExpenseAmt>(e.OldRow) as Decimal?;
    Decimal? nullable4 = sender.GetValue<SOTaxTran.curyExpenseAmt>(e.Row) as Decimal?;
    Decimal? nullable5 = nullable1;
    Decimal? nullable6 = nullable2;
    if (!(nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue))
      return true;
    Decimal? nullable7 = nullable3;
    Decimal? nullable8 = nullable4;
    return !(nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue);
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    this._CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    Decimal num = (Decimal) (this.ParentGetValue<SOOrder.curyOpenDiscTotal>(sender.Graph) ?? (object) 0M);
    Decimal objA = (Decimal) (this.ParentGetValue<SOOrder.curyOpenLineTotal>(sender.Graph) ?? (object) 0M) + CuryTaxTotal - CuryInclTaxTotal - num;
    if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<SOOrder.curyOpenOrderTotal>(sender.Graph) ?? (object) 0M)))
      return;
    this.ParentSetValue<SOOrder.curyOpenOrderTotal>(sender.Graph, (object) objA);
  }

  protected override bool IsFreightTaxable(PXCache sender, List<object> taxitems)
  {
    if (taxitems.Count <= 0)
      return false;
    List<object> taxitems1 = base.SelectTaxes<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>(sender.Graph, (object) null, PXTaxCheck.RecalcLine, (object) PXResult<SOTax>.op_Implicit((PXResult<SOTax>) taxitems[0]).TaxID);
    return base.IsFreightTaxable(sender, taxitems1);
  }

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
}
