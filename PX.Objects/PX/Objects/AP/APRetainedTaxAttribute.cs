// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRetainedTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Specialized for <see cref="P:PX.Objects.CS.FeaturesSet.Retainage" /> feature version of the APTaxAttribute. <br />
/// Provides Tax calculation for <see cref="P:PX.Objects.AP.APTran.CuryRetainageAmt" /> amount in the line, by default is attached
/// to <see cref="T:PX.Objects.AP.APTran" /> (details) and <see cref="T:PX.Objects.AP.APInvoice" /> (header). <br />
/// Normally, should be placed on the <see cref="P:PX.Objects.AP.APTran.TaxCategoryID" /> field. <br />
/// Internally, it uses <see cref="T:PX.Objects.AP.APInvoiceEntry" /> graph, otherwise taxes will not be calculated
/// (<see cref="P:PX.Objects.TX.Tax.TaxCalcLevel" /> is set to <see cref="F:PX.Objects.TX.TaxCalc.NoCalc" />).<br />
/// As a result of this attribute work a set of <see cref="T:PX.Objects.AP.APTax" /> tran related to each line and to their parent will be created <br />
/// <example>
/// [APRetainedTaxAttribute(typeof(APRegister), typeof(APTax), typeof(APTaxTran))]
/// </example>
/// </summary>
public class APRetainedTaxAttribute : APTaxAttribute
{
  private readonly HashSet<string> allowedParentFields = new HashSet<string>();

  protected override short SortOrder => 1;

  public APRetainedTaxAttribute(
    System.Type ParentType,
    System.Type TaxType,
    System.Type TaxSumType,
    System.Type CalcMode = null,
    System.Type parentBranchIDField = null)
    : base(ParentType, TaxType, TaxSumType, CalcMode, parentBranchIDField)
  {
    this.Init();
  }

  private void Init()
  {
    this.CuryTranAmt = typeof (APTran.curyRetainageAmt);
    this.GroupDiscountRate = typeof (APTran.groupDiscountRate);
    this.CuryLineTotal = typeof (APRegister.curyLineRetainageTotal);
    this.CuryTaxTotal = typeof (APRegister.curyRetainedTaxTotal);
    this.CuryDiscTot = typeof (APRegister.curyRetainedDiscTotal);
    this.CuryDocBal = typeof (APRegister.curyRetainageTotal);
    this.allowedParentFields.Add(this._CuryLineTotal);
    this.allowedParentFields.Add(this._CuryTaxTotal);
    this.allowedParentFields.Add(this._CuryDiscTot);
    this.allowedParentFields.Add(this._CuryDocBal);
    this._CuryOrigTaxableAmt = string.Empty;
    this._CuryTaxableAmt = typeof (APTax.curyRetainedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (APTax.curyRetainedTaxAmt).Name;
    this._CuryTaxAmtSumm = typeof (TaxTran.curyRetainedTaxAmtSumm).Name;
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<APTran.lineType, NotEqual<SOLineType.discount>>, APTran.curyRetainageAmt>, decimal0>), typeof (SumCalc<APRegister.curyLineRetainageTotal>)));
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
    return base.IsTaxRowAmountUpdated(sender, e) || !sender.ObjectsEqual<APTax.curyRetainedTaxAmt>(e.Row, e.OldRow);
  }

  protected override bool IsDeductibleVATTax(PX.Objects.TX.Tax tax) => false;

  protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    if (!this.allowedParentFields.Contains(fieldname))
      return;
    base.ParentSetValue(graph, fieldname, value);
  }

  protected override bool IsRoundingNeeded(PXGraph graph) => false;

  protected override void ResetRoundingDiff(PXGraph graph)
  {
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

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
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
