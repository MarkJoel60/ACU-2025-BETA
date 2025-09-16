// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class SOInvoiceTaxAttribute : ARTaxAttribute
{
  public SOInvoiceTaxAttribute()
    : base(typeof (PX.Objects.AR.ARInvoice), typeof (ARTax), typeof (ARTaxTran))
  {
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.miscCharge>, And<ARTran.lineType, NotEqual<SOLineType.freight>, And<ARTran.lineType, NotEqual<SOLineType.discount>>>>, ARTran.curyTranAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyGoodsTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.miscCharge>, And<ARTran.lineType, NotEqual<SOLineType.freight>, And<ARTran.lineType, NotEqual<SOLineType.discount>>>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyGoodsExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.miscCharge>>, ARTran.curyTranAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyMiscTot>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.miscCharge>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyMiscExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>, ARTran.curyDiscAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyLineDiscTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.freight>>, ARTran.curyTranAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyFreightTot>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.commissionable, Equal<True>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>, And<Where<ARTran.tranType, Equal<ARDocType.creditMemo>, Or<ARTran.tranType, Equal<ARDocType.cashReturn>>>>>>>, Minus<Sub<Sub<ARTran.curyTranAmt, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.origGroupDiscountRate>, ARTran.origDocumentDiscountRate>>>, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.groupDiscountRate>, ARTran.documentDiscountRate>>>>, Case<Where<ARTran.commissionable, Equal<True>, And<Where<ARTran.lineType, NotEqual<SOLineType.discount>>>>, Sub<Sub<ARTran.curyTranAmt, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.origGroupDiscountRate>, ARTran.origDocumentDiscountRate>>>, Sub<ARTran.curyTranAmt, Mult<Mult<ARTran.curyTranAmt, ARTran.groupDiscountRate>, ARTran.documentDiscountRate>>>>>, decimal0>), typeof (SumCalc<ARSalesPerTran.curyCommnblAmt>)));
  }

  protected override void Tax_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.NoCalc && e.ExternalCall)
      base.Tax_RowDeleted(sender, e);
    if (this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    this.SelectTaxes(sender, (object) null, PXTaxCheck.RecalcLine);
  }

  protected override bool ConsiderEarlyPaymentDiscount(PXCache sender, object parent, PX.Objects.TX.Tax tax)
  {
    PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) parent;
    if (arInvoice.DocType != "CSL" && arInvoice.DocType != "RCS")
      return base.ConsiderEarlyPaymentDiscount(sender, parent, tax);
    return (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2") && tax.TaxApplyTermsDisc == "P";
  }

  protected override bool ConsiderInclusiveDiscount(PXCache sender, object parent, PX.Objects.TX.Tax tax)
  {
    PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) parent;
    if (arInvoice.DocType != "CSL" && arInvoice.DocType != "RCS")
      return base.ConsiderInclusiveDiscount(sender, parent, tax);
    return tax.TaxCalcLevel == "0" && tax.TaxApplyTermsDisc == "P";
  }

  protected override object SelectParent(PXCache cache, object row)
  {
    if (this._TaxCalc != TaxCalc.ManualCalc)
      return base.SelectParent(cache, row);
    object detrow = PXParentAttribute.LocateParent(cache, row, this._ChildType);
    return this.FilterParent(cache, detrow) ? (object) null : detrow;
  }

  protected virtual bool FilterParent(PXCache cache, object detrow)
  {
    if (detrow == null || cache.Graph.Caches[this._ChildType].GetStatus(detrow) == null)
      return true;
    if (!(this._ChildType == typeof (ARTran)))
      return false;
    ARTran arTran = (ARTran) detrow;
    SOOrderShipment current = (SOOrderShipment) cache.Graph.Caches[typeof (SOOrderShipment)]?.Current;
    if (current == null)
      return false;
    return current.OrderType != arTran.SOOrderType || current.OrderNbr != arTran.SOOrderNbr;
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
  }

  protected override void AdjustTaxableAmount(
    PXCache sender,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
    PXCache cach = sender.Graph.Caches[typeof (PX.Objects.AR.ARInvoice)];
    PX.Objects.AR.ARInvoice arInvoice = PXParentAttribute.SelectParent<PX.Objects.AR.ARInvoice>(sender, row);
    Decimal valueOrDefault1 = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyGoodsTotal>((object) arInvoice)).GetValueOrDefault();
    Decimal valueOrDefault2 = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyMiscTot>((object) arInvoice)).GetValueOrDefault();
    Decimal valueOrDefault3 = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyFreightTot>((object) arInvoice)).GetValueOrDefault();
    Decimal valueOrDefault4 = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyDiscTot>((object) arInvoice)).GetValueOrDefault();
    if (!(valueOrDefault1 + valueOrDefault2 + valueOrDefault3 != 0M) || !(CuryTaxableAmt != 0M))
      return;
    if (Math.Abs(CuryTaxableAmt - valueOrDefault1 - valueOrDefault2) < 0.00005M)
    {
      CuryTaxableAmt -= valueOrDefault4;
    }
    else
    {
      if (!(Math.Abs(valueOrDefault1 + valueOrDefault3 + valueOrDefault2 - valueOrDefault4 - CuryTaxableAmt) < this.GetPrecisionBasedNegligibleDifference(sender.Graph, row)))
        return;
      CuryTaxableAmt = valueOrDefault1 + valueOrDefault3 + valueOrDefault2 - valueOrDefault4;
    }
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
    if (this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach = sender.Graph.Caches[this._ChildType];
    if (this.CompareZone(sender.Graph, (string) e.OldValue, (string) sender.GetValue(e.Row, this._TaxZoneID)) && sender.GetValue(e.Row, this._TaxZoneID) != null)
      return;
    this.Preload(sender);
    List<object> details = this.ChildSelect(cach, e.Row);
    this.ReDefaultTaxes(cach, details);
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    PXCache cach = sender.Graph.Caches[typeof (PX.Objects.AR.ARInvoice)];
    PX.Objects.AR.ARInvoice arInvoice = !(row is PX.Objects.AR.ARInvoice) ? (PX.Objects.AR.ARInvoice) PXParentAttribute.SelectParent(sender, row, typeof (PX.Objects.AR.ARInvoice)) : (PX.Objects.AR.ARInvoice) row;
    Decimal objA1 = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyGoodsTotal>((object) arInvoice)).GetValueOrDefault() + ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyMiscTot>((object) arInvoice)).GetValueOrDefault() + ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyFreightTot>((object) arInvoice)).GetValueOrDefault();
    Decimal valueOrDefault = ((Decimal?) cach.GetValue<PX.Objects.AR.ARInvoice.curyDiscTot>((object) arInvoice)).GetValueOrDefault();
    Decimal objA2 = objA1 + CuryTaxTotal + CuryTaxDiscountTotal - CuryInclTaxTotal - valueOrDefault;
    Decimal objB1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M);
    Decimal objB2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    Decimal objB3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    if (!object.Equals((object) objA1, (object) objB1) || !object.Equals((object) CuryTaxTotal, (object) objB2) || !object.Equals((object) valueOrDefault, (object) objB3))
    {
      this.ParentSetValue(sender.Graph, this._CuryLineTotal, (object) objA1);
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) CuryTaxTotal);
      this.ParentSetValue(sender.Graph, this._CuryDiscTot, (object) valueOrDefault);
      if (!string.IsNullOrEmpty(this._CuryDocBal))
      {
        this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
        return;
      }
    }
    if (!string.IsNullOrEmpty(this._CuryTaxDiscountTotal))
      this.ParentSetValue(sender.Graph, this._CuryTaxDiscountTotal, (object) CuryTaxDiscountTotal);
    if (string.IsNullOrEmpty(this._CuryDocBal))
      return;
    Decimal objB4 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
    if (object.Equals((object) objA2, (object) objB4))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
  }

  protected override void IsTaxSavedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.ARInvoice row))
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    Decimal? nullable3 = (Decimal?) sender.GetValue(e.Row, this._CuryLineTotal);
    bool flag = this.DocHasInclusiveTax(sender, row);
    if (!(nullable3.GetValueOrDefault() == 0M) && flag)
      return;
    this.CalcDocTotals(sender, (object) row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
  }

  protected override DateTime? GetDocDate(PXCache sender, object row)
  {
    int num = ((bool?) this.ParentGetValue<PX.Objects.AR.ARRegister.isCancellation>(sender.Graph)).GetValueOrDefault() ? 1 : 0;
    DateTime? nullable = (DateTime?) this.ParentGetValue<PX.Objects.AR.ARRegister.origDocDate>(sender.Graph);
    return num != 0 && nullable.HasValue ? nullable : base.GetDocDate(sender, row);
  }
}
