// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.Standalone;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
///  Specialized for <see cref="T:PX.Objects.AP.Standalone.APQuickCheck" /> version of the <see cref="T:PX.Objects.AP.APTaxAttribute" />(override).<br />
///  Provides Tax calculation for <see cref="T:PX.Objects.AP.APTran" />, by default is attached to <see cref="T:PX.Objects.AP.APTran" /> (details) and <see cref="T:PX.Objects.AP.Standalone.APQuickCheck" /> (header) <br />
///  Normally, should be placed on the TaxCategoryID field. <br />
///  Internally, it uses <see cref="T:PX.Objects.AP.APQuickCheckEntry" /> graph, otherwise taxes are not calculated (tax calc Level is set to NoCalc).<br />
///  As a result of this attribute work a set of <see cref="T:PX.Objects.AP.APTax" /> tran related to each AP Tran and to their parent will created <br />
///  May be combined with other attrbibutes with similar type - for example, APTaxAttribute <br />
///  <example>
/// [APQuickCheckTax(typeof(Standalone.APQuickCheck), typeof(APTax), typeof(APTaxTran))]
///  </example>
///  </summary>
public class APQuickCheckTaxAttribute : APTaxAttribute
{
  private bool needTaxRecalculation = true;

  /// <summary>
  /// <param name="ParentType">Type of parent(main) document. Must Be IBqlTable </param>
  /// <param name="TaxType">Type of the TaxTran records for the row(details). Must be IBqlTable</param>
  /// <param name="TaxSumType">Type of the TaxTran recorde for the main documect (summary). Must be iBqlTable</param>
  /// </summary>
  public APQuickCheckTaxAttribute(
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
    this.DocDate = typeof (APQuickCheck.adjDate);
    this.FinPeriodID = typeof (APQuickCheck.adjFinPeriodID);
    this.CuryLineTotal = typeof (APQuickCheck.curyLineTotal);
    this.CuryTranAmt = typeof (APTran.curyTranAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (APTran.curyTranAmt), typeof (SumCalc<APQuickCheck.curyLineTotal>)));
  }

  protected override void ParentFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row is APQuickCheck && ((APRegister) e.Row).DocType != "VQC")
    {
      base.ParentFieldUpdated(sender, e);
    }
    else
    {
      if (!(e.Row is PX.Objects.CM.CurrencyInfo))
        return;
      PXGraph graph = sender.Graph;
      object[] objArray = new object[1]
      {
        (object) ((PX.Objects.CM.CurrencyInfo) e.Row).CuryInfoID
      };
      APQuickCheck apQuickCheck;
      if ((apQuickCheck = (APQuickCheck) PXSelectBase<APQuickCheck, PXSelect<APQuickCheck, Where<APQuickCheck.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(graph, objArray)) == null || !(apQuickCheck.DocType != "VQC"))
        return;
      base.ParentFieldUpdated(sender, e);
    }
  }

  protected override void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(((APRegister) e.Row).DocType != "VQC"))
      return;
    base.ZoneUpdated(sender, e);
  }

  protected override void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(((APRegister) e.Row).DocType != "VQC"))
      return;
    base.DateUpdated(sender, e);
  }

  protected override bool IsRetainedTaxes(PXGraph graph) => false;

  protected override bool ConsiderEarlyPaymentDiscount(PXCache sender, object parent, PX.Objects.TX.Tax tax)
  {
    return (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2") && tax.TaxApplyTermsDisc == "P";
  }

  protected override bool ConsiderInclusiveDiscount(PXCache sender, object parent, PX.Objects.TX.Tax tax)
  {
    return tax.TaxCalcLevel == "0" && tax.TaxApplyTermsDisc == "P";
  }

  protected override void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal num1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDiscTot) ?? (object) 0M);
    Decimal num2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M) + CuryTaxTotal + CuryTaxDiscountTotal - CuryInclTaxTotal - num1;
    Decimal objB = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    if (!object.Equals((object) CuryTaxTotal, (object) objB))
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) CuryTaxTotal);
    if (!string.IsNullOrEmpty(this._CuryTaxDiscountTotal))
      this.ParentSetValue(sender.Graph, this._CuryTaxDiscountTotal, (object) CuryTaxDiscountTotal);
    Decimal oldValue = 0M;
    if (!string.IsNullOrEmpty(this._CuryDocBal))
    {
      oldValue = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
      this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) num2);
    }
    object obj = this.ParentRow(sender.Graph);
    if (obj == null)
      return;
    bool flag1;
    this.OrigDiscAmtExtCallDict.TryGetValue(obj, out flag1);
    if (flag1 || num2 == 0M || oldValue == num2)
      return;
    bool flag2 = false;
    foreach (object selectTax in this.SelectTaxes(sender, obj, PXTaxCheck.RecalcTotals))
    {
      PX.Objects.TX.Tax tax = PXResult.Unwrap<PX.Objects.TX.Tax>(selectTax);
      if (this.ConsiderEarlyPaymentDiscount(sender, obj, tax) || this.ConsiderInclusiveDiscount(sender, obj, tax))
      {
        flag2 = true;
        break;
      }
    }
    if (!flag2 || !this.needTaxRecalculation)
      return;
    this.needTaxRecalculation = false;
    Decimal num3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryOrigDiscAmt) ?? (object) 0M);
    this.DiscPercentsDict[obj] = new Decimal?(100M * num3 / num2);
    PXFieldUpdatedEventArgs e = new PXFieldUpdatedEventArgs(obj, (object) oldValue, false);
    this.ParentFieldUpdated(sender, e);
    this.OrigDiscAmtExtCallDict.Remove(obj);
    this.DiscPercentsDict.Remove(obj);
  }
}
