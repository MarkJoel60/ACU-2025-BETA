// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.TaxExtensionHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.Extensions.SalesTax;

public static class TaxExtensionHelper
{
  public static void AdjustMinMaxTaxableAmt(
    this PXGraph graph,
    TaxRev taxrev,
    ref Decimal curyTaxableAmt,
    ref Decimal taxableAmt)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    taxableAmt = defaultCurrencyInfo.CuryConvBase(curyTaxableAmt);
    Decimal? taxableMin1 = taxrev.TaxableMin;
    Decimal num1 = 0.0M;
    if (!(taxableMin1.GetValueOrDefault() == num1 & taxableMin1.HasValue))
    {
      Decimal num2 = taxableAmt;
      Decimal? taxableMin2 = taxrev.TaxableMin;
      Decimal valueOrDefault = taxableMin2.GetValueOrDefault();
      if (num2 < valueOrDefault & taxableMin2.HasValue)
      {
        curyTaxableAmt = 0.0M;
        taxableAmt = 0.0M;
      }
    }
    Decimal? taxableMax = taxrev.TaxableMax;
    num1 = 0.0M;
    if (taxableMax.GetValueOrDefault() == num1 & taxableMax.HasValue)
      return;
    Decimal num3 = taxableAmt;
    taxableMax = taxrev.TaxableMax;
    Decimal valueOrDefault1 = taxableMax.GetValueOrDefault();
    if (!(num3 > valueOrDefault1 & taxableMax.HasValue))
      return;
    ref Decimal local1 = ref curyTaxableAmt;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
    taxableMax = taxrev.TaxableMax;
    Decimal baseval = taxableMax.Value;
    Decimal num4 = currencyInfo.CuryConvCury(baseval);
    local1 = num4;
    ref Decimal local2 = ref taxableAmt;
    taxableMax = taxrev.TaxableMax;
    Decimal num5 = taxableMax.Value;
    local2 = num5;
  }

  public static void SetExpenseAmountsForDeductibleVAT(
    this TaxDetail taxdet,
    TaxRev taxrev,
    Decimal CuryTaxAmt,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo)
  {
    taxdet.CuryExpenseAmt = new Decimal?(currencyInfo.RoundCury(CuryTaxAmt * (1M - taxrev.NonDeductibleTaxRate.GetValueOrDefault() / 100M)));
    taxdet.ExpenseAmt = new Decimal?(currencyInfo.CuryConvBase(taxdet.CuryExpenseAmt.Value));
  }
}
