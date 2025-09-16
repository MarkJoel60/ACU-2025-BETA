// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranTaxAttribute : TaxAttribute
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

  public CABankTranTaxAttribute(
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
    this.CuryDocBal = typeof (CABankTran.curyTranAmt);
    this.CuryLineTotal = typeof (CABankTran.curyApplAmtCA);
    this.DocDate = typeof (CABankTran.tranDate);
    this.CuryTaxTotal = typeof (CABankTran.curyTaxTotal);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxByCalculationLevelComparer", (string) null);
    Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
    object[] objArray = new object[2]
    {
      row,
      this.GetCurrent(graph)
    };
    foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<Current<CABankTran.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<Current<CABankTran.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.sales>, And2<Where<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>, Or<Current<CABankTran.drCr>, Equal<CADrCr.cADebit>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>>>>>>>>>>>>, And<Current<CABankTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
    {
      PX.Objects.TX.Tax tax = this.AdjustTaxLevel(graph, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
      dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = new PXResult<PX.Objects.TX.Tax, TaxRev>(tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
    }
    List<object> ret = new List<object>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        foreach (PXResult<CABankTax> pxResult in PXSelectBase<CABankTax, PXSelect<CABankTax, Where<CABankTax.bankTranType, Equal<Current<CABankTranDetail.bankTranType>>, And<CABankTax.bankTranID, Equal<Current<CABankTranDetail.bankTranID>>, And<CABankTax.lineNbr, Equal<Current<CABankTranDetail.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankTax caBankTax = PXResult<CABankTax>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (dictionary.TryGetValue(caBankTax.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankTax>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankTax, PX.Objects.TX.Tax, TaxRev>(caBankTax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      case PXTaxCheck.RecalcLine:
        foreach (PXResult<CABankTax> pxResult in PXSelectBase<CABankTax, PXSelect<CABankTax, Where<CABankTax.bankTranType, Equal<Current<CABankTran.tranType>>, And<CABankTax.bankTranID, Equal<Current<CABankTran.tranID>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankTax caBankTax = PXResult<CABankTax>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (dictionary.TryGetValue(caBankTax.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankTax>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankTax, PX.Objects.TX.Tax, TaxRev>(caBankTax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      case PXTaxCheck.RecalcTotals:
        foreach (PXResult<CABankTaxTran> pxResult in PXSelectBase<CABankTaxTran, PXSelect<CABankTaxTran, Where<CABankTaxTran.bankTranType, Equal<Current<CABankTran.tranType>>, And<CABankTaxTran.bankTranID, Equal<Current<CABankTran.tranID>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankTaxTran caBankTaxTran = PXResult<CABankTaxTran>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (caBankTaxTran.TaxID != null && dictionary.TryGetValue(caBankTaxTran.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankTaxTran>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankTaxTran, PX.Objects.TX.Tax, TaxRev>(caBankTaxTran, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      default:
        return ret;
    }
  }

  protected virtual object GetCurrent(PXGraph graph)
  {
    return (object) ((PXSelectBase<CABankTran>) ((CABankTransactionsMaint) graph).Details).Current;
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<CABankTranDetail>((IEnumerable) PXSelectBase<CABankTranDetail, PXSelect<CABankTranDetail, Where<CABankTranDetail.bankTranID, Equal<Current<CABankTran.tranID>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<CABankTranDetail, object>((Func<CABankTranDetail, object>) (_ => (object) _)).ToList<object>();
  }

  private int CalculateIndex<T>(
    List<object> ret,
    PXResult<PX.Objects.TX.Tax, TaxRev> line,
    IComparer<PX.Objects.TX.Tax> taxByCalculationLevelComparer)
    where T : class, IBqlTable, new()
  {
    int count = ret.Count;
    while (count > 0 && taxByCalculationLevelComparer.Compare(PXResult<T, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<T, PX.Objects.TX.Tax, TaxRev>) ret[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)) > 0)
      --count;
    return count;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (sender.Graph is CABankTransactionsMaint || sender.Graph is CABankMatchingProcess)
      base.CacheAttached(sender);
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    if (!(sender.Graph is CABankTransactionsMaint))
      return base.CalcLineTotal(sender, row);
    Decimal num = 0M;
    PXView view = ((PXSelectBase) ((CABankTransactionsMaint) sender.Graph).TranSplit).View;
    object[] objArray1 = new object[1]{ row };
    object[] objArray2 = Array.Empty<object>();
    foreach (CABankTranDetail caBankTranDetail in view.SelectMultiBound(objArray1, objArray2))
      num += caBankTranDetail.CuryTranAmt.GetValueOrDefault();
    return num;
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CABankTranDetail.curyTaxableAmt>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CABankTranDetail.curyTaxAmt>(row, (object) value);
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CABankTranDetail.curyTaxableAmt>(row);
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CABankTranDetail.curyTaxAmt>(row);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is CABankTranDetail caBankTranDetail))
      return;
    caBankTranDetail.CuryTranAmt = value;
    sender.Update((object) caBankTranDetail);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<CABankTranDetail.curyTranAmt>(row)).DisplayName;
  }

  protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    base.DefaultTaxes(sender, row, DefaultExisting);
    this.CalculateRawAmount(sender, row);
  }

  protected virtual void CalculateRawAmount(PXCache sender, object row)
  {
    if (sender.Current == null)
      return;
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = new Decimal?(0M);
    List<CABankTranTaxAttribute.RateWithMultiplier> taxRates1 = new List<CABankTranTaxAttribute.RateWithMultiplier>();
    List<CABankTranTaxAttribute.RateWithMultiplier> taxRates2 = new List<CABankTranTaxAttribute.RateWithMultiplier>();
    string str = this._isTaxCalcModeEnabled ? this.GetTaxCalcMode(sender.Graph) : "T";
    Decimal? curyTranAmt = ((CABankTranDetail) sender.Current).CuryTranAmt;
    foreach (object selectTax in this.SelectTaxes(sender, row, PXTaxCheck.Line))
    {
      PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax);
      TaxRev taxRev = PXResult.Unwrap<TaxRev>(selectTax);
      if ((!this._isTaxCalcModeEnabled || str == "T") && tax.TaxCalcLevel == "0" || str == "G")
      {
        Decimal? nullable3 = nullable1;
        Decimal valueOrDefault = taxRev.TaxRate.GetValueOrDefault();
        nullable1 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault) : new Decimal?();
        taxRates1.Add(new CABankTranTaxAttribute.RateWithMultiplier(taxRev.TaxRate, new Decimal?(tax.ReverseTax.GetValueOrDefault() ? -1M : 1M)));
      }
      if ((!this._isTaxCalcModeEnabled || str == "T") && tax.TaxCalcLevel != "0" || str == "N")
      {
        Decimal? nullable4 = nullable2;
        Decimal valueOrDefault = taxRev.TaxRate.GetValueOrDefault();
        nullable2 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault) : new Decimal?();
        taxRates2.Add(new CABankTranTaxAttribute.RateWithMultiplier(taxRev.TaxRate, new Decimal?(tax.ReverseTax.GetValueOrDefault() ? -1M : 1M)));
      }
    }
    Decimal? nullable5 = curyTranAmt;
    Decimal num1 = 1M;
    Decimal? nullable6 = nullable1;
    Decimal num2 = (Decimal) 100;
    Decimal? nullable7 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num2) : new Decimal?();
    Decimal? nullable8;
    if (!nullable7.HasValue)
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(num1 + nullable7.GetValueOrDefault());
    Decimal? nullable9 = nullable8;
    Decimal? nullable10 = nullable5.HasValue & nullable9.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
    Decimal num3 = 1M;
    nullable6 = nullable1;
    Decimal num4 = (Decimal) 100;
    Decimal? nullable11 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num4) : new Decimal?();
    Decimal? nullable12;
    if (!nullable11.HasValue)
    {
      nullable6 = new Decimal?();
      nullable12 = nullable6;
    }
    else
      nullable12 = new Decimal?(num3 + nullable11.GetValueOrDefault());
    nullable9 = nullable12;
    Decimal? nullable13 = nullable2;
    Decimal num5 = (Decimal) 100;
    Decimal? nullable14;
    if (!nullable13.HasValue)
    {
      nullable6 = new Decimal?();
      nullable14 = nullable6;
    }
    else
      nullable14 = new Decimal?(nullable13.GetValueOrDefault() / num5);
    Decimal? nullable15 = nullable14;
    Decimal? nullable16;
    if (!(nullable9.HasValue & nullable15.HasValue))
    {
      nullable13 = new Decimal?();
      nullable16 = nullable13;
    }
    else
      nullable16 = new Decimal?(nullable9.GetValueOrDefault() + nullable15.GetValueOrDefault());
    Decimal? nullable17 = nullable16;
    Decimal? nullable18;
    if (!(nullable10.HasValue & nullable17.HasValue))
    {
      nullable15 = new Decimal?();
      nullable18 = nullable15;
    }
    else
      nullable18 = new Decimal?(nullable10.GetValueOrDefault() / nullable17.GetValueOrDefault());
    Decimal? amount1 = nullable18;
    Decimal? totalAmtWithRates1 = this.CalculateTaxTotalAmtWithRates(sender, row, taxRates1, amount1);
    nullable17 = amount1;
    Decimal? nullable19 = totalAmtWithRates1;
    Decimal? nullable20;
    if (!(nullable17.HasValue & nullable19.HasValue))
    {
      nullable15 = new Decimal?();
      nullable20 = nullable15;
    }
    else
      nullable20 = new Decimal?(nullable17.GetValueOrDefault() - nullable19.GetValueOrDefault());
    Decimal? amount2 = nullable20;
    Decimal? totalAmtWithRates2 = this.CalculateTaxTotalAmtWithRates(sender, row, taxRates2, amount2);
    nullable19 = curyTranAmt;
    nullable17 = totalAmtWithRates2;
    Decimal? nullable21;
    if (!(nullable19.HasValue & nullable17.HasValue))
    {
      nullable15 = new Decimal?();
      nullable21 = nullable15;
    }
    else
      nullable21 = new Decimal?(nullable19.GetValueOrDefault() - nullable17.GetValueOrDefault());
    Decimal? nullable22 = nullable21;
    PXCache cach = sender.Graph.Caches[typeof (CABankTranDetail)];
    CABankTranDetail copy = sender.Graph.Caches[typeof (CABankTranDetail)].CreateCopy(row) as CABankTranDetail;
    copy.CuryTranAmt = nullable22;
    CABankTranDetail caBankTranDetail = copy;
    cach.Update((object) caBankTranDetail);
  }

  protected virtual Decimal? CalculateTaxTotalAmtWithRates(
    PXCache sender,
    object row,
    List<CABankTranTaxAttribute.RateWithMultiplier> taxRates,
    Decimal? amount)
  {
    Decimal? totalAmtWithRates = new Decimal?(0M);
    foreach (CABankTranTaxAttribute.RateWithMultiplier taxRate in taxRates)
    {
      Decimal? nullable1;
      ref Decimal? local = ref nullable1;
      Decimal? nullable2 = amount;
      Decimal? nullable3 = taxRate.TaxRate;
      Decimal valueOrDefault = (nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
      local = new Decimal?(valueOrDefault);
      Decimal num = MultiCurrencyCalculator.RoundCury(sender, row, nullable1.Value, this.Precision);
      nullable2 = taxRate.Multiplier;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(num * nullable2.GetValueOrDefault());
      nullable1 = nullable4;
      nullable2 = totalAmtWithRates;
      nullable3 = nullable1;
      totalAmtWithRates = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    }
    return totalAmtWithRates;
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal objA = this.CalcLineTotal(sender, row);
    Decimal num1 = objA + CuryTaxTotal - CuryInclTaxTotal;
    Decimal objB1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M);
    Decimal objB2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    Decimal num2 = Math.Abs((Decimal) (this.ParentGetValue<CABankTran.curyTranAmt>(sender.Graph) ?? (object) 0M));
    this.ParentSetValue<CABankTran.curyUnappliedBalCA>(sender.Graph, (object) (num2 - num1));
    this.ParentSetValue<CABankTran.curyApplAmtCA>(sender.Graph, (object) objA);
    if (object.Equals((object) objA, (object) objB1) && object.Equals((object) CuryTaxTotal, (object) objB2))
      return;
    this.ParentSetValue<CABankTran.curyTaxTotal>(sender.Graph, (object) CuryTaxTotal);
    this.ParentSetValue<CABankTran.curyDetailsWithTaxesTotal>(sender.Graph, (object) num1);
  }

  protected override void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    TaxCalc taxCalc = this.TaxCalc;
    try
    {
      if (this.IsExternalTax(sender.Graph, (string) e.OldValue))
        this.TaxCalc = TaxCalc.Calc;
      if (this.IsExternalTax(sender.Graph, (string) sender.GetValue(e.Row, this._TaxZoneID)) || ((bool?) sender.GetValue(e.Row, this._ExternalTaxesImportInProgress)).GetValueOrDefault())
        this.TaxCalc = TaxCalc.ManualCalc;
      if (e.OldValue != null || this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc)
        return;
      PXCache cach = sender.Graph.Caches[this._ChildType];
      if (this.CompareZone(sender.Graph, (string) e.OldValue, (string) sender.GetValue(e.Row, this._TaxZoneID)) && sender.GetValue(e.Row, this._TaxZoneID) != null)
        return;
      this.Preload(sender);
      List<object> details = this.ChildSelect(cach, e.Row);
      this.ReDefaultTaxes(cach, details);
      this._ParentRow = e.Row;
      this.CalcTaxes(cach, (object) null);
      this._ParentRow = (object) null;
    }
    finally
    {
      this.TaxCalc = taxCalc;
    }
  }

  protected class RateWithMultiplier
  {
    public Decimal? TaxRate { get; private set; }

    public Decimal? Multiplier { get; private set; }

    public RateWithMultiplier(Decimal? taxRate, Decimal? multiplier)
    {
      this.TaxRate = taxRate;
      this.Multiplier = multiplier;
    }
  }
}
