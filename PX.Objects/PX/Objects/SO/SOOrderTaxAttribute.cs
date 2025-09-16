// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTaxAttribute
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
/// Extends <see cref="T:PX.Objects.SO.SOTaxAttribute" /> and calculates CuryOrderTotal and CuryTaxTotal for the Parent(Header) SOOrder.
/// This Attribute overrides some of functionality of <see cref="T:PX.Objects.SO.SOTaxAttribute" />.
/// This Attribute is applied to the TaxCategoryField of SOOrder instead of SO Line.
/// </summary>
/// <example>
/// [SOOrderTax(typeof(SOOrder), typeof(SOTax), typeof(SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc)]
/// </example>
public class SOOrderTaxAttribute : SOTaxAttribute
{
  protected override short SortOrder => 0;

  public SOOrderTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : this(ParentType, TaxType, TaxSumType, (Type) null)
  {
  }

  public SOOrderTaxAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type TaxCalculationMode)
    : base(ParentType, TaxType, TaxSumType, TaxCalculationMode)
  {
    this.CuryTranAmt = typeof (SOOrder.curyFreightTot);
    this.TaxCategoryID = typeof (SOOrder.freightTaxCategoryID);
    this._Attributes.Clear();
  }

  protected override object InitializeTaxDet(object data)
  {
    object obj = base.InitializeTaxDet(data);
    if (obj.GetType() == this._TaxType)
      ((SOTax) obj).LineNbr = new int?(32000);
    return obj;
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType)
  {
    return (Decimal?) sender.GetValue(row, this._CuryTranAmt);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, int32000>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal num1 = (Decimal) (this.ParentGetValue<SOOrder.curyLineTotal>(sender.Graph) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue<SOOrder.curyMiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num3 = (Decimal) (this.ParentGetValue<SOOrder.curyFreightTot>(sender.Graph) ?? (object) 0M);
    Decimal num4 = (Decimal) (this.ParentGetValue<SOOrder.curyDiscTot>(sender.Graph) ?? (object) 0M);
    Decimal num5 = num2;
    Decimal objA = num1 + num5 + num3 + CuryTaxTotal - CuryInclTaxTotal - num4;
    if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<SOOrder.curyOrderTotal>(sender.Graph) ?? (object) 0M)) && object.Equals((object) CuryTaxTotal, (object) (Decimal) (this.ParentGetValue<SOOrder.curyTaxTotal>(sender.Graph) ?? (object) 0M)))
      return;
    this.ParentSetValue<SOOrder.curyOrderTotal>(sender.Graph, (object) objA);
    this.ParentSetValue<SOOrder.curyTaxTotal>(sender.Graph, (object) CuryTaxTotal);
  }

  protected override void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
  }

  protected override void Tax_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrderTaxAttribute.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new SOOrderTaxAttribute.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.e = e;
    // ISSUE: reference to a compiler-generated field
    if ((this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc || !cDisplayClass90.e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    object obj = this.ParentRow(sender.Graph);
    // ISSUE: reference to a compiler-generated field
    ITaxDetail taxitem = this.MatchesCategory(cach1, obj, (ITaxDetail) cDisplayClass90.e.Row);
    this.AddOneTax(cach2, obj, taxitem);
    // ISSUE: reference to a compiler-generated field
    this._NoSumTotals = this._TaxCalc == TaxCalc.ManualCalc && !cDisplayClass90.e.ExternalCall;
    // ISSUE: method pointer
    PXRowDeleting pxRowDeleting = new PXRowDeleting((object) cDisplayClass90, __methodptr(\u003CTax_RowInserted\u003Eb__0));
    sender.Graph.RowDeleting.AddHandler(this._TaxSumType, pxRowDeleting);
    try
    {
      this.CalcTaxes(cach1, (object) null);
    }
    finally
    {
      sender.Graph.RowDeleting.RemoveHandler(this._TaxSumType, pxRowDeleting);
    }
    this._NoSumTotals = false;
  }

  protected override void Tax_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if ((this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach = sender.Graph.Caches[this._ChildType];
    this.DelOneTax(sender.Graph.Caches[this._TaxType], this.ParentRow(sender.Graph), e.Row);
    this.CalcTaxes(cach, (object) null);
  }

  protected override void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc || this.CompareZone(sender.Graph, (string) e.OldValue, (string) sender.GetValue(e.Row, this._TaxZoneID)))
      return;
    this.Preload(sender);
    this.ReDefaultTaxes(sender, e.Row, e.Row, false);
    this._ParentRow = e.Row;
    this.CalcTaxes(sender, e.Row);
    this._ParentRow = (object) null;
  }

  protected override void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc)
      return;
    this.Preload(sender);
    this.ReDefaultTaxes(sender, e.Row, e.Row, false);
    this._NoSumTaxable = true;
    this._ParentRow = e.Row;
    try
    {
      this.CalcTaxes(sender, e.Row);
    }
    finally
    {
      this._ParentRow = (object) null;
      this._NoSumTaxable = false;
    }
  }

  protected override bool ShouldUpdateFinPeriodID(PXCache sender, object oldRow, object newRow)
  {
    return false;
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this._ParentRow = e.Row;
    base.RowUpdated(sender, e);
    this._ParentRow = (object) null;
  }

  /// <summary>
  /// Fill tax details for line for per unit taxes. Do nothing for retained tax.
  /// </summary>
  protected override void TaxSetLineDefaultForPerUnitTaxes(
    PXCache rowCache,
    object row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail)
  {
  }

  /// <summary>
  /// Fill aggregated tax detail for per unit tax. Do nothing for retained tax.
  /// </summary>
  protected override TaxDetail FillAggregatedTaxDetailForPerUnitTax(
    PXCache rowCache,
    object row,
    PX.Objects.TX.Tax tax,
    TaxRev taxRevision,
    TaxDetail aggrTaxDetail,
    List<object> taxItems)
  {
    return aggrTaxDetail;
  }
}
