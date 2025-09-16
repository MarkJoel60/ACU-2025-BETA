// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PaymentRGOLCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class PaymentRGOLCalculator
{
  private readonly RGOLCalculator rGOLCalculator;
  private readonly IAdjustment adj;
  private readonly bool? reverseGainLoss;

  public PaymentRGOLCalculator(
    IPXCurrencyHelper curyHelper,
    IAdjustment adj,
    bool? reverseGainLoss)
  {
    this.adj = adj;
    this.reverseGainLoss = reverseGainLoss;
    this.rGOLCalculator = new RGOLCalculator(curyHelper.GetCurrencyInfo(adj.AdjgCuryInfoID), curyHelper.GetCurrencyInfo(adj.AdjdCuryInfoID), curyHelper.GetCurrencyInfo(adj.AdjdOrigCuryInfoID));
  }

  public void Calculate(IInvoice voucher, IDocumentTran tran = null)
  {
    Decimal? nullable1 = (Decimal?) tran?.CuryCashDiscBal;
    Decimal? documentCuryBal1 = nullable1 ?? voucher.CuryDiscBal;
    nullable1 = (Decimal?) tran?.CashDiscBal;
    Decimal? documentBaseBal1 = nullable1 ?? voucher.DiscBal;
    RGOLCalculationResult calculationResult1 = this.rGOLCalculator.CalcRGOL(this.adj.CuryDiscBal, documentCuryBal1, documentBaseBal1, this.adj.CuryAdjgDiscAmt, this.adj.AdjDiscAmt);
    this.adj.CuryAdjdDiscAmt = calculationResult1.ToCuryAdjAmt;
    RGOLCalculationResult calculationResult2 = this.rGOLCalculator.CalcRGOL(this.adj.CuryAdjgWhTaxAmt, this.adj.AdjWhTaxAmt);
    this.adj.CuryAdjdWhTaxAmt = calculationResult2.ToCuryAdjAmt;
    nullable1 = (Decimal?) tran?.CuryTranBal;
    Decimal? documentCuryBal2 = nullable1 ?? voucher.CuryDocBal;
    nullable1 = (Decimal?) tran?.TranBal;
    Decimal? documentBaseBal2 = nullable1 ?? voucher.DocBal;
    nullable1 = this.adj.CuryDocBal;
    Decimal num1 = 0M;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    RGOLCalculationResult calculationResult3;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      RGOLCalculationResult calculationResult4 = new RGOLCalculationResult();
      nullable2 = documentCuryBal2;
      Decimal? curyAdjdDiscAmt = this.adj.CuryAdjdDiscAmt;
      nullable1 = nullable2.HasValue & curyAdjdDiscAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
      Decimal? curyAdjdWhTaxAmt = this.adj.CuryAdjdWhTaxAmt;
      calculationResult4.ToCuryAdjAmt = nullable1.HasValue & curyAdjdWhTaxAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyAdjdWhTaxAmt.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = documentBaseBal2.Value;
      nullable2 = this.adj.AdjDiscAmt;
      Decimal num3 = nullable2.Value;
      Decimal num4 = num2 - num3;
      nullable2 = this.adj.AdjWhTaxAmt;
      Decimal num5 = nullable2.Value;
      Decimal num6 = num4 - num5;
      nullable2 = this.adj.AdjAmt;
      Decimal num7 = nullable2.Value;
      Decimal num8 = num6 - num7;
      nullable3 = calculationResult1.RgolAmt;
      Decimal? nullable5;
      if (!nullable3.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(num8 - nullable3.GetValueOrDefault());
      nullable4 = nullable5;
      nullable1 = calculationResult2.RgolAmt;
      Decimal? nullable6;
      if (!(nullable4.HasValue & nullable1.HasValue))
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault());
      calculationResult4.RgolAmt = nullable6;
      calculationResult3 = calculationResult4;
    }
    else
      calculationResult3 = this.rGOLCalculator.CalcRGOL(this.adj.CuryDocBal, documentCuryBal2, documentBaseBal2, this.adj.CuryAdjgAmt, this.adj.AdjAmt);
    this.adj.CuryAdjdAmt = calculationResult3.ToCuryAdjAmt;
    IAdjustment adj1 = this.adj;
    nullable3 = calculationResult1.RgolAmt;
    nullable2 = calculationResult2.RgolAmt;
    nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable4 = calculationResult3.RgolAmt;
    Decimal? nullable7;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    adj1.RGOLAmt = nullable7;
    if (!this.reverseGainLoss.GetValueOrDefault())
      return;
    IAdjustment adj2 = this.adj;
    nullable4 = adj2.RGOLAmt;
    Decimal num9 = -1M;
    Decimal? nullable8;
    if (!nullable4.HasValue)
    {
      nullable1 = new Decimal?();
      nullable8 = nullable1;
    }
    else
      nullable8 = new Decimal?(nullable4.GetValueOrDefault() * num9);
    adj2.RGOLAmt = nullable8;
  }
}
