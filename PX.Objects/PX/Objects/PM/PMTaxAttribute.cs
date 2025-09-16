// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMTaxAttribute : TaxAttribute
{
  protected int SortOrder;
  protected bool forceRetainedTaxesOff;

  public PMTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryLineTotal = typeof (PMProformaLine.curyLineTotal);
    this.CuryTranAmt = typeof (PMProformaLine.curyLineTotal);
    this.CuryDocBal = typeof (PMProforma.curyDocTotal);
    this.CuryTaxTotal = typeof (PMProforma.curyTaxTotal);
    this.CuryTaxInclTotal = typeof (PMProforma.curyTaxInclTotal);
    this.DocDate = typeof (PMProforma.invoiceDate);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    List<object> objectList = new List<object>();
    if (row == null)
      return objectList;
    object[] progressiveDetails;
    object[] transactionDetails;
    this.SelectDetails(graph, row, out progressiveDetails, out transactionDetails);
    if (progressiveDetails != null)
      objectList.AddRange((IEnumerable<object>) progressiveDetails);
    if (transactionDetails != null)
      objectList.AddRange((IEnumerable<object>) transactionDetails);
    return objectList;
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    List<object> ret = new List<object>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        int? nullable = new int?(int.MinValue);
        if (row != null && row.GetType() == typeof (PMProformaProgressLine))
          nullable = (int?) graph.Caches[typeof (PMProformaProgressLine)].GetValue<PMProformaProgressLine.lineNbr>(row);
        if (row != null && row.GetType() == typeof (PMProformaTransactLine))
          nullable = (int?) graph.Caches[typeof (PMProformaTransactLine)].GetValue<PMProformaTransactLine.lineNbr>(row);
        if (!nullable.HasValue)
          return ret;
        foreach (PXResult<PMTax> pxResult in PXSelectBase<PMTax, PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTax.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMTax.lineNbr, Equal<Required<PMTax.lineNbr>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, new object[1]{ (object) nullable }))
        {
          PMTax record = PXResult<PMTax>.op_Implicit(pxResult);
          this.AppendTail<PMTax, Where>(graph, ret, record, row, parameters);
        }
        return ret;
      case PXTaxCheck.RecalcLine:
        foreach (PXResult<PMTax> pxResult in PXSelectBase<PMTax, PXSelect<PMTax, Where<PMTax.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTax.revisionID, Equal<Current<PMProforma.revisionID>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))
        {
          PMTax record = PXResult<PMTax>.op_Implicit(pxResult);
          this.AppendTail<PMTax, Where>(graph, ret, record, row, parameters);
        }
        return ret;
      case PXTaxCheck.RecalcTotals:
        foreach (PXResult<PMTaxTran> pxResult in PXSelectBase<PMTaxTran, PXSelect<PMTaxTran, Where<PMTaxTran.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMTaxTran.revisionID, Equal<Current<PMProforma.revisionID>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))
        {
          PMTaxTran record = PXResult<PMTaxTran>.op_Implicit(pxResult);
          this.AppendTail<PMTaxTran, Where>(graph, ret, record, row, parameters);
        }
        return ret;
      default:
        return ret;
    }
  }

  protected virtual void AppendTail<T, W>(
    PXGraph graph,
    List<object> ret,
    T record,
    object row,
    params object[] parameters)
    where T : class, ITaxDetail, IBqlTable, new()
    where W : IBqlWhere, new()
  {
    IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxByCalculationLevelComparer", (string) null);
    PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where2<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>, And<W>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where2<Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>, And<W>>>(graph);
    List<object> objectList = new List<object>();
    objectList.Add((object) this.GetDocDate(this.ParentCache(graph), row));
    objectList.Add((object) record.TaxID);
    if (parameters != null)
      objectList.AddRange((IEnumerable<object>) parameters);
    foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in ((PXSelectBase) pxSelectReadonly2).View.SelectMultiBound(new object[1]
    {
      row
    }, objectList.ToArray()))
    {
      int count = ret.Count;
      while (count > 0 && calculationLevelComparer.Compare(PXResult<T, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<T, PX.Objects.TX.Tax, TaxRev>) ret[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)) > 0)
        --count;
      ret.Insert(count, (object) new PXResult<T, PX.Objects.TX.Tax, TaxRev>(record, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)));
    }
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    Decimal num = 0M;
    object[] progressiveDetails;
    object[] transactionDetails;
    this.SelectDetails(sender, row, out progressiveDetails, out transactionDetails);
    if (progressiveDetails != null)
    {
      foreach (object obj in progressiveDetails)
        num += this.GetCuryTranAmt(sender.Graph.Caches[typeof (PMProformaProgressLine)], sender.Graph.Caches[typeof (PMProformaProgressLine)].ObjectsEqual(obj, row) ? row : obj, "I").GetValueOrDefault();
    }
    if (transactionDetails != null)
    {
      foreach (object obj in transactionDetails)
        num += this.GetCuryTranAmt(sender.Graph.Caches[typeof (PMProformaTransactLine)], sender.Graph.Caches[typeof (PMProformaTransactLine)].ObjectsEqual(obj, row) ? row : obj, "I").GetValueOrDefault();
    }
    return num;
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    try
    {
      this.forceRetainedTaxesOff = true;
      base.CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    }
    finally
    {
      this.forceRetainedTaxesOff = false;
    }
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType = "I")
  {
    PMProformaLine pmProformaLine = (PMProformaLine) row;
    if (!this.IsRetainedTaxes(sender.Graph))
      return pmProformaLine.CuryLineTotal;
    Decimal? curyLineTotal = pmProformaLine.CuryLineTotal;
    Decimal valueOrDefault = pmProformaLine.CuryRetainage.GetValueOrDefault();
    return !curyLineTotal.HasValue ? new Decimal?() : new Decimal?(curyLineTotal.GetValueOrDefault() - valueOrDefault);
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PMProformaLine row = e.Row as PMProformaLine;
    PMProformaLine oldRow = e.OldRow as PMProformaLine;
    if (this._TaxCalc == TaxCalc.Calc && row != null && oldRow != null)
    {
      bool? merged = row.Merged;
      if (!merged.GetValueOrDefault())
      {
        merged = oldRow.Merged;
        if (merged.GetValueOrDefault() && !string.IsNullOrEmpty(oldRow.TaxCategoryID))
          oldRow.TaxCategoryID = (string) null;
      }
    }
    base.RowUpdated(sender, e);
    if (this._TaxCalc != TaxCalc.Calc || row == null || oldRow == null || !(row.TaxCategoryID == oldRow.TaxCategoryID))
      return;
    Decimal? nullable1 = row.CuryLineTotal;
    Decimal? nullable2 = oldRow.CuryLineTotal;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return;
    nullable2 = row.CuryRetainage;
    nullable1 = oldRow.CuryRetainage;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      return;
    this.CalcTaxes(sender, e.Row, PXTaxCheck.Line);
  }

  protected override bool IsRetainedTaxes(PXGraph graph)
  {
    if (this.forceRetainedTaxesOff)
      return false;
    ARSetup current = graph.Caches[typeof (ARSetup)].Current as ARSetup;
    return PXAccess.FeatureInstalled<FeaturesSet.retainage>() && current != null && current.RetainTaxes.GetValueOrDefault();
  }

  public override int CompareTo(object other)
  {
    return this.SortOrder.CompareTo(((PMTaxAttribute) other).SortOrder);
  }

  private void SelectDetails(
    PXGraph graph,
    object row,
    out object[] progressiveDetails,
    out object[] transactionDetails)
  {
    this.SelectDetails(graph.Caches[row.GetType()], row, out progressiveDetails, out transactionDetails);
  }

  private void SelectDetails(
    PXCache sender,
    object row,
    out object[] progressiveDetails,
    out object[] transactionDetails)
  {
    object obj = PXParentAttribute.SelectParent(sender, row, this._ParentType);
    progressiveDetails = PXParentAttribute.SelectChildren(sender.Graph.Caches[typeof (PMProformaProgressLine)], obj, this._ParentType);
    transactionDetails = PXParentAttribute.SelectChildren(sender.Graph.Caches[typeof (PMProformaTransactLine)], obj, this._ParentType);
  }
}
