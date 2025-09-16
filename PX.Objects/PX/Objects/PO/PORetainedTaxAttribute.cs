// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PORetainedTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized for <see cref="P:PX.Objects.CS.FeaturesSet.Retainage" /> feature version of the POTaxAttribute. <br />
/// Provides Tax calculation for <see cref="P:PX.Objects.PO.POLine.CuryRetainageAmt" /> amount in the line, by default is attached
/// to <see cref="T:PX.Objects.PO.POLine" /> (details) and <see cref="T:PX.Objects.PO.POOrder" /> (header). <br />
/// Normally, should be placed on the <see cref="P:PX.Objects.PO.POLine.TaxCategoryID" /> field. <br />
/// As a result of this attribute work a set of <see cref="T:PX.Objects.PO.POTax" /> tran related to each line and to their parent will be created <br />
/// </summary>
public class PORetainedTaxAttribute : POTaxAttribute
{
  protected Type CuryRetainageTotal = typeof (POOrder.curyRetainageTotal);
  protected Type CuryOrderTotal = typeof (POOrder.curyOrderTotal);
  private readonly HashSet<string> allowedParentFields = new HashSet<string>();

  protected override short SortOrder => 2;

  public PORetainedTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.Init();
  }

  private void Init()
  {
    this.CuryTranAmt = typeof (POLine.curyRetainageAmt);
    this.GroupDiscountRate = typeof (POLine.groupDiscountRate);
    this.CuryLineTotal = typeof (POOrder.curyLineRetainageTotal);
    this.CuryTaxTotal = typeof (POOrder.curyRetainedTaxTotal);
    this.CuryDiscTot = typeof (POOrder.curyRetainedDiscTotal);
    this.CuryDocBal = typeof (POOrder.curyRetainageTotal);
    this.allowedParentFields.Add(this._CuryLineTotal);
    this.allowedParentFields.Add(this._CuryTaxTotal);
    this.allowedParentFields.Add(this._CuryDocBal);
    this._CuryOrigTaxableAmt = string.Empty;
    this._CuryTaxableAmt = typeof (POTax.curyRetainedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (POTax.curyRetainedTaxAmt).Name;
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (POLine.curyRetainageAmt), typeof (SumCalc<POOrder.curyLineRetainageTotal>)));
  }

  protected override List<object> SelectTaxes<WhereType>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return !this.IsRetainedTaxes(graph) ? new List<object>() : base.SelectTaxes<WhereType>(graph, row, taxchk, parameters);
  }

  protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    if (!this.IsRetainedTaxes(sender.Graph))
      return;
    base.DefaultTaxes(sender, row, DefaultExisting);
  }

  protected override void Tax_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!this.IsRetainedTaxes(sender.Graph))
      return;
    base.Tax_RowInserted(sender, e);
  }

  protected override void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!this.IsRetainedTaxes(sender.Graph))
      return;
    base.Tax_RowUpdated(sender, e);
  }

  protected override bool IsTaxRowAmountUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    return base.IsTaxRowAmountUpdated(sender, e) || !sender.ObjectsEqual<POTax.curyRetainedTaxAmt>(e.Row, e.OldRow);
  }

  protected override bool IsDeductibleVATTax(PX.Objects.TX.Tax tax) => false;

  protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    if (!this.allowedParentFields.Contains(fieldname))
      return;
    base.ParentSetValue(graph, fieldname, value);
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

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row) => new Decimal?(0M);

  protected override Decimal? GetTaxAmt(PXCache sender, object row) => new Decimal?(0M);

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal curyTaxTotal,
    Decimal curyInclTaxTotal,
    Decimal curyWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    base._CalcDocTotals(sender, row, curyTaxTotal, curyInclTaxTotal, curyWhTaxTotal, CuryTaxDiscountTotal);
    Decimal num1 = (Decimal) (this.ParentGetValue(sender.Graph, this.CuryRetainageTotal.Name) ?? (object) 0M);
    if (!(num1 != 0M))
      return;
    Decimal num2 = (Decimal) (this.ParentGetValue(sender.Graph, this.CuryOrderTotal.Name) ?? (object) 0M);
    base.ParentSetValue(sender.Graph, this.CuryOrderTotal.Name, (object) (num2 + num1));
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
