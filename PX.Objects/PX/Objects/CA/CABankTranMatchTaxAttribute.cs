// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranMatchTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranMatchTaxAttribute : TaxAttribute
{
  public CABankTranMatchTaxAttribute(
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
    this.CuryDocBal = typeof (CABankTran.curyChargeAmt);
    this.CuryLineTotal = typeof (CABankTran.curyChargeAmt);
    this.DocDate = typeof (CABankTran.tranDate);
    this.CuryTaxTotal = typeof (CABankTran.curyChargeTaxAmt);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxByCalculationLevelComparer", (string) null);
    List<object> ret = new List<object>();
    CABankTranMatch caBankTranMatch = row as CABankTranMatch;
    CABankTran current = graph.Caches[typeof (CABankTran)].Current as CABankTran;
    if (caBankTranMatch?.MatchType == "M" && caBankTranMatch != null && caBankTranMatch.CATranID.HasValue || string.IsNullOrEmpty(current?.ChargeTypeID))
      return ret;
    Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
    object[] objArray = new object[2]
    {
      row,
      graph.Caches[typeof (CABankTran)].Current
    };
    foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<Current<CABankTran.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<Current<CABankTran.drCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.sales>, And2<Where<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>, Or<Current<CABankTran.drCr>, Equal<CADrCr.cADebit>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>>>>>>>>>>>>, And<Current<CABankTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
    {
      PX.Objects.TX.Tax tax = this.AdjustTaxLevel(graph, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
      dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = new PXResult<PX.Objects.TX.Tax, TaxRev>(tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
    }
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        foreach (PXResult<CABankChargeTax> pxResult in PXSelectBase<CABankChargeTax, PXSelect<CABankChargeTax, Where<CABankChargeTax.matchType, Equal<Current<CABankTranMatch.matchType>>, And<CABankChargeTax.bankTranID, Equal<Current<CABankTranMatch.tranID>>, And<CABankChargeTax.lineNbr, Equal<Current<CABankTranMatch.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankChargeTax caBankChargeTax = PXResult<CABankChargeTax>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (dictionary.TryGetValue(caBankChargeTax.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankChargeTax>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankChargeTax, PX.Objects.TX.Tax, TaxRev>(caBankChargeTax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      case PXTaxCheck.RecalcLine:
        foreach (PXResult<CABankChargeTax> pxResult in PXSelectBase<CABankChargeTax, PXSelect<CABankChargeTax, Where<CABankChargeTax.bankTranID, Equal<Current<CABankTran.tranID>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankChargeTax caBankChargeTax = PXResult<CABankChargeTax>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (dictionary.TryGetValue(caBankChargeTax.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankChargeTax>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankChargeTax, PX.Objects.TX.Tax, TaxRev>(caBankChargeTax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      case PXTaxCheck.RecalcTotals:
        foreach (PXResult<CABankTaxTranMatch> pxResult in PXSelectBase<CABankTaxTranMatch, PXSelect<CABankTaxTranMatch, Where<CABankTaxTranMatch.bankTranType, Equal<Current<CABankTran.tranType>>, And<CABankTaxTranMatch.bankTranID, Equal<Current<CABankTran.tranID>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
        {
          CABankTaxTranMatch bankTaxTranMatch = PXResult<CABankTaxTranMatch>.op_Implicit(pxResult);
          PXResult<PX.Objects.TX.Tax, TaxRev> line;
          if (bankTaxTranMatch.TaxID != null && dictionary.TryGetValue(bankTaxTranMatch.TaxID, out line))
          {
            int index = this.CalculateIndex<CABankTaxTranMatch>(ret, line, calculationLevelComparer);
            ret.Insert(index, (object) new PXResult<CABankTaxTranMatch, PX.Objects.TX.Tax, TaxRev>(bankTaxTranMatch, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(line)));
          }
        }
        return ret;
      default:
        return ret;
    }
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<CABankTranMatch>((IEnumerable) PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Current<CABankTran.tranID>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<CABankTranMatch, object>((Func<CABankTranMatch, object>) (_ => (object) _)).ToList<object>();
  }

  protected int CalculateIndex<T>(
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

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    if (!(sender.Graph is CABankTransactionsMaint))
      return base.CalcLineTotal(sender, row);
    Decimal num = 0M;
    PXView view = ((PXSelectBase) ((CABankTransactionsMaint) sender.Graph).TranSplit).View;
    object[] objArray1 = new object[1]{ row };
    object[] objArray2 = Array.Empty<object>();
    foreach (CABankTranMatch caBankTranMatch in view.SelectMultiBound(objArray1, objArray2))
      num += caBankTranMatch.CuryApplAmt.GetValueOrDefault();
    return num;
  }

  protected override void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CABankTranMatch.curyApplTaxableAmt>(row, (object) value);
  }

  protected override void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
    sender.SetValue<CABankTranMatch.curyApplTaxAmt>(row, (object) value);
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CABankTranMatch.curyApplTaxableAmt>(row);
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CABankTranMatch.curyApplTaxAmt>(row);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is CABankTranMatch caBankTranMatch))
      return;
    caBankTranMatch.CuryApplAmt = value;
    sender.Update((object) caBankTranMatch);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<CABankTranMatch.curyApplAmt>(row)).DisplayName;
  }

  protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    base.DefaultTaxes(sender, row, DefaultExisting);
    this.CalculateRawAmount(sender, row);
  }

  protected virtual void CalculateRawAmount(PXCache sender, object row)
  {
    CABankTranMatch row1 = (CABankTranMatch) row;
    if (row1.MatchType != "C")
      return;
    this.CalculateRawAmount<CABankTranMatch>(sender, (object) row1);
  }

  public virtual void CalculateRawAmount<T>(PXCache sender, object row) where T : IBqlTable
  {
    if (sender.Current == null)
      return;
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = new Decimal?(0M);
    List<CABankTranMatchTaxAttribute.RateWithMultiplier> taxRates1 = new List<CABankTranMatchTaxAttribute.RateWithMultiplier>();
    List<CABankTranMatchTaxAttribute.RateWithMultiplier> taxRates2 = new List<CABankTranMatchTaxAttribute.RateWithMultiplier>();
    string str = this._isTaxCalcModeEnabled ? this.GetTaxCalcMode(sender.Graph) : nameof (T);
    Decimal? curyTranAmount = this.GetCuryTranAmount(sender);
    foreach (object selectTax in this.SelectTaxes(sender, row, PXTaxCheck.Line))
    {
      PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax);
      TaxRev taxRev = PXResult.Unwrap<TaxRev>(selectTax);
      Decimal? nullable3;
      Decimal? nullable4;
      if ((!this._isTaxCalcModeEnabled || str == nameof (T)) && tax.TaxCalcLevel == "0" || str == "G")
      {
        nullable3 = nullable1;
        nullable4 = taxRev.TaxRate;
        nullable1 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        taxRates1.Add(new CABankTranMatchTaxAttribute.RateWithMultiplier(taxRev.TaxRate, new Decimal?(tax.ReverseTax.GetValueOrDefault() ? -1M : 1M)));
      }
      if ((!this._isTaxCalcModeEnabled || str == nameof (T)) && tax.TaxCalcLevel != "0" || str == "N")
      {
        nullable4 = nullable2;
        nullable3 = taxRev.TaxRate;
        nullable2 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        taxRates2.Add(new CABankTranMatchTaxAttribute.RateWithMultiplier(taxRev.TaxRate, new Decimal?(tax.ReverseTax.GetValueOrDefault() ? -1M : 1M)));
      }
    }
    Decimal? nullable5 = curyTranAmount;
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
    nullable19 = curyTranAmount;
    nullable17 = totalAmtWithRates2;
    Decimal? nullable21;
    if (!(nullable19.HasValue & nullable17.HasValue))
    {
      nullable15 = new Decimal?();
      nullable21 = nullable15;
    }
    else
      nullable21 = new Decimal?(nullable19.GetValueOrDefault() - nullable17.GetValueOrDefault());
    Decimal? amount3 = nullable21;
    sender.Graph.Caches[typeof (T)].Update((object) (T) this.SetCuryTranAmount((object) (T) sender.Graph.Caches[typeof (T)].CreateCopy(row), amount3));
  }

  protected virtual Decimal? CalculateTaxTotalAmtWithRates(
    PXCache sender,
    object row,
    List<CABankTranMatchTaxAttribute.RateWithMultiplier> taxRates,
    Decimal? amount)
  {
    Decimal? totalAmtWithRates = new Decimal?(0M);
    foreach (CABankTranMatchTaxAttribute.RateWithMultiplier taxRate in taxRates)
    {
      Decimal? nullable1;
      ref Decimal? local = ref nullable1;
      Decimal? nullable2 = amount;
      Decimal? nullable3 = taxRate.TaxRate;
      Decimal valueOrDefault = (nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
      local = new Decimal?(valueOrDefault);
      Decimal num = this.ApplyRounding(sender, row, nullable1.Value);
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

  protected virtual Decimal? GetCuryTranAmount(PXCache sender)
  {
    return ((CABankTranMatch) sender.Current).CuryApplAmt;
  }

  protected virtual object SetCuryTranAmount(object row, Decimal? amount)
  {
    CABankTranMatch caBankTranMatch = (CABankTranMatch) row;
    caBankTranMatch.CuryApplAmt = amount;
    return (object) caBankTranMatch;
  }

  protected virtual Decimal ApplyRounding(PXCache sender, object row, Decimal taxAmount)
  {
    int curyPrecision = this.GetCuryPrecision(sender);
    return Math.Round(taxAmount, curyPrecision, MidpointRounding.AwayFromZero);
  }

  protected virtual int GetCuryPrecision(PXCache sender)
  {
    return (int) PXResultset<CurrencyList>.op_Implicit(PXSelectBase<CurrencyList, PXViewOf<CurrencyList>.BasedOn<SelectFromBase<CurrencyList, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CurrencyList.curyID, IBqlString>.IsEqual<BqlField<CABankTran.curyID, IBqlString>.FromCurrent>>>.Config>.Select(sender.Graph, Array.Empty<object>()))?.DecimalPlaces ?? this.Precision.GetValueOrDefault();
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType = "I")
  {
    return new Decimal?(((Decimal?) sender.GetValue<CABankTranMatch.curyApplAmt>(row)).GetValueOrDefault());
  }

  protected override Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType = "I")
  {
    return new Decimal?(((Decimal?) sender.GetValue<CABankTranMatch.curyApplAmt>(row)).GetValueOrDefault());
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    if (!(row is CABankTranMatch caBankTranMatch) || caBankTranMatch?.MatchType != "C")
      return;
    Decimal objB = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    Decimal curyChargeAmt = (Decimal) (this.ParentGetValue<CABankTran.curyChargeAmt>(sender.Graph) ?? (object) 0M);
    int num = curyChargeAmt > 0M ? 1 : -1;
    Decimal discrepancy = CABankTranMatchTaxAttribute.CalculateDiscrepancy(row, CuryTaxTotal, curyChargeAmt, CuryInclTaxTotal);
    if (object.Equals((object) CuryTaxTotal, (object) objB))
      return;
    this.ParentSetValue<CABankTran.curyChargeTaxAmt>(sender.Graph, (object) ((Decimal) num * Math.Abs(CuryTaxTotal) + discrepancy));
  }

  private static Decimal CalculateDiscrepancy(
    object row,
    Decimal CuryTaxTotal,
    Decimal curyChargeAmt,
    Decimal curyInclTaxTotal)
  {
    CABankTranMatch caBankTranMatch = (CABankTranMatch) row;
    return Math.Abs(curyChargeAmt) - Math.Min(Math.Abs(((Decimal?) caBankTranMatch?.CuryApplTaxableAmt).GetValueOrDefault()) + Math.Abs(curyInclTaxTotal), Math.Abs(((Decimal?) caBankTranMatch?.CuryApplAmt).GetValueOrDefault())) - (Math.Abs(CuryTaxTotal) - Math.Abs(curyInclTaxTotal));
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
