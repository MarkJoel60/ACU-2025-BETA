// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARCashSaleTaxAttribute : ARTaxAttribute
{
  private bool needTaxRecalculation = true;

  public ARCashSaleTaxAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type parentBranchIDField = null)
    : base(ParentType, TaxType, TaxSumType, parentBranchIDField)
  {
    this.DocDate = typeof (ARCashSale.adjDate);
    this.FinPeriodID = typeof (ARCashSale.adjFinPeriodID);
    this.CuryLineTotal = typeof (ARCashSale.curyLineTotal);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.discount>>, ARTran.curyTranAmt>, Minus<ARTran.curyTranAmt>>), typeof (SumCalc<ARCashSale.curyLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>, ARTran.curyDiscAmt>, decimal0>), typeof (SumCalc<ARCashSale.curyLineDiscTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, NotEqual<SOLineType.miscCharge>, And<ARTran.lineType, NotEqual<SOLineType.freight>, And<ARTran.lineType, NotEqual<SOLineType.discount>, And<ARTran.lineType, IsNotNull, And<ARTran.lineType, NotEqual<Empty>>>>>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<ARCashSale.curyGoodsExtPriceTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.miscCharge>, Or<ARTran.lineType, IsNull, Or<ARTran.lineType, Equal<Empty>>>>, ARTran.curyExtPrice>, decimal0>), typeof (SumCalc<ARCashSale.curyMiscExtPriceTotal>)));
  }

  protected override void ParentFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row is ARCashSale)
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
      ARCashSale arCashSale;
      if ((arCashSale = PXResultset<ARCashSale>.op_Implicit(PXSelectBase<ARCashSale, PXSelect<ARCashSale, Where<ARCashSale.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(graph, objArray))) == null || !(arCashSale.DocType != "RCS"))
        return;
      base.ParentFieldUpdated(sender, e);
    }
  }

  protected override void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(((ARRegister) e.Row).DocType != "RCS"))
      return;
    base.ZoneUpdated(sender, e);
  }

  protected override void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(((ARRegister) e.Row).DocType != "RCS"))
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
    Decimal num3 = 0M;
    if (!string.IsNullOrEmpty(this._CuryDocBal))
    {
      num3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
      this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) num2);
    }
    object obj = this.ParentRow(sender.Graph);
    if (obj == null)
      return;
    bool flag1;
    this.OrigDiscAmtExtCallDict.TryGetValue(obj, out flag1);
    if (flag1 || num2 == 0M || num3 == num2)
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
    Decimal num4 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryOrigDiscAmt) ?? (object) 0M);
    this.DiscPercentsDict[obj] = new Decimal?(100M * num4 / num2);
    PXFieldUpdatedEventArgs e = new PXFieldUpdatedEventArgs(obj, (object) num3, false);
    this.ParentFieldUpdated(sender, e);
    this.OrigDiscAmtExtCallDict.Remove(obj);
    this.DiscPercentsDict.Remove(obj);
  }
}
