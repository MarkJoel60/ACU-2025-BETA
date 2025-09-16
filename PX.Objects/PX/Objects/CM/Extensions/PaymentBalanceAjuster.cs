// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PaymentBalanceAjuster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

internal class PaymentBalanceAjuster
{
  private readonly IPXCurrencyHelper curyHelper;

  public PaymentBalanceAjuster(IPXCurrencyHelper curyHelper) => this.curyHelper = curyHelper;

  public void AdjustBalance(IAdjustment adj)
  {
    this.AdjustBalance(adj, adj.AdjgCuryInfoID, adj.CuryAdjgAmt, adj.CuryAdjgDiscAmt, adj.CuryAdjgWhTaxAmt, true);
  }

  public void AdjustBalance(
    IAdjustment adj,
    long? toCuryInfoID,
    Decimal? curyAdjAmt,
    Decimal? curyAdjDiscAmt,
    Decimal? curyAdjWhTaxAmt,
    bool adjustBalanceByExtraAmounts)
  {
    if (adj.Released.GetValueOrDefault())
      return;
    CurrencyInfo currencyInfo = this.curyHelper.GetCurrencyInfo(toCuryInfoID);
    Decimal? nullable1;
    if (curyAdjAmt.HasValue)
    {
      Decimal curyval = curyAdjAmt.Value;
      Decimal num1 = currencyInfo.CuryConvBase(curyval);
      adj.AdjAmt = new Decimal?(num1);
      IAdjustment adjustment1 = adj;
      nullable1 = adjustment1.CuryDocBal;
      Decimal num2 = curyval;
      adjustment1.CuryDocBal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?();
      nullable1 = adj.CuryDocBal;
      Decimal num3 = 0M;
      if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
      {
        adj.AdjAmt = adj.DocBal;
        adj.DocBal = new Decimal?(0M);
      }
      else
      {
        IAdjustment adjustment2 = adj;
        IAdjustment adj1 = adj;
        Decimal? curyDocBal = adj.CuryDocBal;
        nullable1 = adj.DocBal;
        Decimal num4 = num1;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num4) : new Decimal?();
        Decimal? nullable3 = this.AdjustWhenTheSameCuryAndRate(adj1, curyDocBal, nullable2);
        adjustment2.DocBal = nullable3;
      }
    }
    if (curyAdjWhTaxAmt.HasValue)
    {
      Decimal curyval = curyAdjWhTaxAmt.Value;
      Decimal num5 = currencyInfo.CuryConvBase(curyval);
      adj.AdjWhTaxAmt = new Decimal?(num5);
      IAdjustment adjustment3 = adj;
      nullable1 = adjustment3.CuryWhTaxBal;
      Decimal num6 = curyval;
      adjustment3.CuryWhTaxBal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num6) : new Decimal?();
      IAdjustment adjustment4 = adj;
      nullable1 = adj.CuryWhTaxBal;
      Decimal num7 = 0M;
      Decimal? nullable4;
      if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
      {
        nullable1 = adj.WhTaxBal;
        Decimal num8 = num5;
        nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num8) : new Decimal?();
      }
      else
        nullable4 = new Decimal?(0M);
      adjustment4.WhTaxBal = nullable4;
      if (adjustBalanceByExtraAmounts)
      {
        IAdjustment adjustment5 = adj;
        nullable1 = adjustment5.CuryDocBal;
        Decimal num9 = curyval;
        adjustment5.CuryDocBal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num9) : new Decimal?();
        nullable1 = adj.CuryDocBal;
        Decimal num10 = 0M;
        if (nullable1.GetValueOrDefault() == num10 & nullable1.HasValue)
        {
          adj.DocBal = new Decimal?(0M);
        }
        else
        {
          IAdjustment adjustment6 = adj;
          IAdjustment adj2 = adj;
          Decimal? curyDocBal = adj.CuryDocBal;
          nullable1 = adj.DocBal;
          Decimal num11 = num5;
          Decimal? nullable5 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num11) : new Decimal?();
          Decimal? nullable6 = this.AdjustWhenTheSameCuryAndRate(adj2, curyDocBal, nullable5);
          adjustment6.DocBal = nullable6;
        }
      }
    }
    if (!curyAdjDiscAmt.HasValue)
      return;
    Decimal curyval1 = curyAdjDiscAmt.Value;
    Decimal num12 = currencyInfo.CuryConvBase(curyval1);
    adj.AdjDiscAmt = new Decimal?(num12);
    IAdjustment adjustment7 = adj;
    nullable1 = adjustment7.CuryDiscBal;
    Decimal num13 = curyval1;
    adjustment7.CuryDiscBal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num13) : new Decimal?();
    IAdjustment adjustment8 = adj;
    nullable1 = adj.CuryDiscBal;
    Decimal num14 = 0M;
    Decimal? nullable7;
    if (!(nullable1.GetValueOrDefault() == num14 & nullable1.HasValue))
    {
      nullable1 = adj.DiscBal;
      Decimal num15 = num12;
      nullable7 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num15) : new Decimal?();
    }
    else
      nullable7 = new Decimal?(0M);
    adjustment8.DiscBal = nullable7;
    if (!adjustBalanceByExtraAmounts)
      return;
    IAdjustment adjustment9 = adj;
    nullable1 = adjustment9.CuryDocBal;
    Decimal num16 = curyval1;
    adjustment9.CuryDocBal = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num16) : new Decimal?();
    nullable1 = adj.CuryDocBal;
    Decimal num17 = 0M;
    if (nullable1.GetValueOrDefault() == num17 & nullable1.HasValue)
    {
      adj.DocBal = new Decimal?(0M);
    }
    else
    {
      IAdjustment adjustment10 = adj;
      IAdjustment adj3 = adj;
      Decimal? curyDocBal = adj.CuryDocBal;
      nullable1 = adj.DocBal;
      Decimal num18 = num12;
      Decimal? nullable8 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num18) : new Decimal?();
      Decimal? nullable9 = this.AdjustWhenTheSameCuryAndRate(adj3, curyDocBal, nullable8);
      adjustment10.DocBal = nullable9;
    }
  }

  /// <summary>
  /// prevent discrepancy due rounding (unexpected for user in that case)
  /// </summary>
  public Decimal? AdjustWhenTheSameCuryAndRate(IAdjustment adj, Decimal? curyValue, Decimal? value)
  {
    CurrencyInfo currencyInfo1 = this.curyHelper.GetCurrencyInfo(adj.AdjdOrigCuryInfoID);
    CurrencyInfo currencyInfo2 = this.curyHelper.GetCurrencyInfo(adj.AdjgCuryInfoID);
    int num;
    if (currencyInfo1.CuryID == currencyInfo2.CuryID)
    {
      Decimal? nullable = currencyInfo1.CuryRate;
      Decimal? curyRate = currencyInfo2.CuryRate;
      if (nullable.GetValueOrDefault() == curyRate.GetValueOrDefault() & nullable.HasValue == curyRate.HasValue)
      {
        Decimal? recipRate = currencyInfo1.RecipRate;
        nullable = currencyInfo2.RecipRate;
        num = recipRate.GetValueOrDefault() == nullable.GetValueOrDefault() & recipRate.HasValue == nullable.HasValue ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    return num != 0 ? new Decimal?(currencyInfo1.CuryConvBase(curyValue.Value)) : value;
  }
}
