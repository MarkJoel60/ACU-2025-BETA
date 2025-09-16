// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CATaxAttribute : TaxAttribute
{
  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  protected override bool AskRecalculationOnCalcModeChange
  {
    get => true;
    set => base.AskRecalculationOnCalcModeChange = value;
  }

  public CATaxAttribute(
    Type parentType,
    Type taxType,
    Type taxSumType,
    Type calcMode = null,
    Type parentBranchIDField = null)
    : base(parentType, taxType, taxSumType, calcMode, parentBranchIDField)
  {
    this.Init();
  }

  private void Init()
  {
    this.CuryDocBal = typeof (CAAdj.curyTranAmt);
    this.CuryLineTotal = typeof (CAAdj.curySplitTotal);
    this.DocDate = typeof (CAAdj.tranDate);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(this.GetTaxByCalculationLevelComparer(), "taxByCalculationLevelComparer", (string) null);
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<Current<CAAdj.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<Current<CAAdj.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.sales>, And2<Where<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>, Or<Current<CAAdj.drCr>, Equal<CADrCr.cADebit>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>>>>>>>>>>>>, And<Current<CAAdj.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    object[] currents = new object[2]
    {
      row,
      (object) ((PXSelectBase<CAAdj>) ((CATranEntry) graph).CAAdjRecords).Current
    };
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<CATax>((IEnumerable) PXSelectBase<CATax, PXSelect<CATax, Where<CATax.adjTranType, Equal<Current<CASplit.adjTranType>>, And<CATax.adjRefNbr, Equal<Current<CASplit.adjRefNbr>>, And<CATax.lineNbr, Equal<Current<CASplit.lineNbr>>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters);
        foreach (CATax record in list1)
          this.InsertTax<CATax>(graph, taxchk, record, tails1, taxList);
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<CATax>((IEnumerable) PXSelectBase<CATax, PXSelect<CATax, Where<CATax.adjTranType, Equal<Current<CAAdj.adjTranType>>, And<CATax.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters);
        foreach (CATax record in list2)
          this.InsertTax<CATax>(graph, taxchk, record, tails2, taxList);
        return taxList;
      case PXTaxCheck.RecalcTotals:
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<CATaxTran>((IEnumerable) PXSelectBase<CATaxTran, PXSelect<CATaxTran, Where<CATaxTran.module, Equal<BatchModule.moduleCA>, And<CATaxTran.tranType, Equal<Current<CAAdj.adjTranType>>, And<CATaxTran.refNbr, Equal<Current<CAAdj.adjRefNbr>>>>>>.Config>.SelectMultiBound(graph, currents, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list3, select, currents, parameters);
        foreach (CATaxTran record in list3)
          this.InsertTax<CATaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<CASplit>((IEnumerable) PXSelectBase<CASplit, PXSelect<CASplit, Where<CASplit.adjTranType, Equal<Current<CAAdj.adjTranType>>, And<CASplit.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<CASplit, object>((Func<CASplit, object>) (_ => (object) _)).ToList<object>();
  }

  public override void CacheAttached(PXCache sender)
  {
    if (sender.Graph is CATranEntry)
      base.CacheAttached(sender);
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    if (!(sender.Graph is CATranEntry))
      return base.CalcLineTotal(sender, row);
    Decimal num = 0M;
    PXView view = ((PXSelectBase) ((CATranEntry) sender.Graph).CASplitRecords).View;
    object[] objArray1 = new object[1]{ row };
    object[] objArray2 = Array.Empty<object>();
    foreach (CASplit caSplit in view.SelectMultiBound(objArray1, objArray2))
      num += caSplit.CuryTranAmt.GetValueOrDefault();
    return num;
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CASplit.curyTaxableAmt>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CASplit.curyTaxAmt>(row, (object) value);
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CASplit.curyTaxableAmt>(row);
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CASplit.curyTaxAmt>(row);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is CASplit caSplit))
      return;
    caSplit.CuryTranAmt = value;
    sender.Update((object) caSplit);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<CASplit.curyTranAmt>(row)).DisplayName;
  }
}
