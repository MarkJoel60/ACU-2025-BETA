// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PaymentBalanceCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class PaymentBalanceCalculator
{
  private readonly IPXCurrencyHelper curyHelper;

  public PaymentBalanceCalculator(IPXCurrencyHelper curyHelper) => this.curyHelper = curyHelper;

  /// <summary>
  /// The method to initialize application
  /// balances in Payment currency.
  /// </summary>
  public void CalcBalances(
    long? PaymentCuryInfoID,
    long? VoucherPayCuryInfoID,
    IInvoice voucher,
    IAdjustment adj,
    IDocumentTran tran = null)
  {
    long? toCuryInfoID1 = PaymentCuryInfoID;
    long? fromCuryInfoID1 = VoucherPayCuryInfoID;
    long? curyInfoId = (long?) tran?.CuryInfoID;
    long? fromOrigInfoID1 = curyInfoId ?? voucher.CuryInfoID;
    Decimal? nullable = (Decimal?) tran?.CuryTranBal;
    Decimal? fromCuryDocBal1 = nullable ?? voucher.CuryDocBal;
    nullable = (Decimal?) tran?.TranBal;
    Decimal? fromDocBal1 = nullable ?? voucher.DocBal;
    CalculatedBalance calculatedBalance1 = this.CalcBalance(toCuryInfoID1, fromCuryInfoID1, fromOrigInfoID1, fromCuryDocBal1, fromDocBal1);
    adj.CuryDocBal = new Decimal?(calculatedBalance1.CuryBalance);
    adj.DocBal = new Decimal?(calculatedBalance1.Balance);
    long? toCuryInfoID2 = PaymentCuryInfoID;
    long? fromCuryInfoID2 = VoucherPayCuryInfoID;
    curyInfoId = (long?) tran?.CuryInfoID;
    long? fromOrigInfoID2 = curyInfoId ?? voucher.CuryInfoID;
    nullable = (Decimal?) tran?.CuryCashDiscBal;
    Decimal? fromCuryDocBal2 = nullable ?? voucher.CuryDiscBal;
    nullable = (Decimal?) tran?.CashDiscBal;
    Decimal? fromDocBal2 = nullable ?? voucher.DiscBal;
    CalculatedBalance calculatedBalance2 = this.CalcBalance(toCuryInfoID2, fromCuryInfoID2, fromOrigInfoID2, fromCuryDocBal2, fromDocBal2);
    adj.CuryDiscBal = new Decimal?(calculatedBalance2.CuryBalance);
    adj.DiscBal = new Decimal?(calculatedBalance2.Balance);
    CalculatedBalance calculatedBalance3 = this.CalcBalance(PaymentCuryInfoID, VoucherPayCuryInfoID, voucher.CuryInfoID, voucher.CuryWhTaxBal, voucher.WhTaxBal);
    adj.CuryWhTaxBal = new Decimal?(calculatedBalance3.CuryBalance);
    adj.WhTaxBal = new Decimal?(calculatedBalance3.Balance);
  }

  public CalculatedBalance CalcBalance(
    long? toCuryInfoID,
    long? fromCuryInfoID,
    long? fromOrigInfoID,
    Decimal? fromCuryDocBal,
    Decimal? fromDocBal)
  {
    CurrencyInfo currencyInfo1 = this.curyHelper.GetCurrencyInfo(toCuryInfoID);
    CurrencyInfo currencyInfo2 = this.curyHelper.GetCurrencyInfo(fromOrigInfoID);
    if (object.Equals((object) currencyInfo1.CuryID, (object) currencyInfo2.CuryID))
      return new CalculatedBalance()
      {
        CuryBalance = fromCuryDocBal.Value,
        Balance = currencyInfo1.CuryConvBase(fromCuryDocBal.Value)
      };
    if (object.Equals((object) currencyInfo2.CuryID, (object) currencyInfo2.BaseCuryID))
      return new CalculatedBalance()
      {
        CuryBalance = currencyInfo1.CuryConvCury(fromDocBal.Value),
        Balance = fromDocBal.Value
      };
    Decimal baseval = this.curyHelper.GetCurrencyInfo(fromCuryInfoID).CuryConvBaseRaw(fromCuryDocBal.Value);
    Decimal curyval = currencyInfo1.CuryConvCury(baseval);
    return new CalculatedBalance()
    {
      CuryBalance = curyval,
      Balance = currencyInfo1.CuryConvBase(curyval)
    };
  }

  public Decimal GetAdjdCuryRate(IAdjustment adj)
  {
    CurrencyInfo currencyInfo1 = this.curyHelper.GetCurrencyInfo(adj.AdjgCuryInfoID);
    CurrencyInfo currencyInfo2 = this.curyHelper.GetCurrencyInfo(adj.AdjdCuryInfoID);
    return currencyInfo2 != null && !string.Equals(currencyInfo1.CuryID, currencyInfo2.CuryID) ? Math.Round((currencyInfo2.CuryMultDiv == "M" ? currencyInfo2.CuryRate.Value : 1M / currencyInfo2.CuryRate.Value) * (currencyInfo1.CuryMultDiv == "M" ? 1M / currencyInfo1.CuryRate.Value : currencyInfo1.CuryRate.Value), 8, MidpointRounding.AwayFromZero) : 1M;
  }
}
