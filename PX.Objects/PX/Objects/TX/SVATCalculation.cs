// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATCalculation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public static class SVATCalculation
{
  public static void FillAmounts(
    this SVATConversionHist histSVAT,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    Decimal? curyTaxableAmt,
    Decimal? curyTaxAmount,
    Decimal mult)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = currencyInfo;
    Decimal? nullable = curyTaxableAmt;
    Decimal num1 = mult;
    Decimal valueOrDefault1 = (nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num1) : new Decimal?()).GetValueOrDefault();
    Decimal curyval1 = currencyInfo1.RoundCury(valueOrDefault1);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo;
    nullable = curyTaxAmount;
    Decimal num2 = mult;
    Decimal valueOrDefault2 = (nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?()).GetValueOrDefault();
    Decimal curyval2 = currencyInfo2.RoundCury(valueOrDefault2);
    histSVAT.CuryTaxableAmt = new Decimal?(curyval1);
    histSVAT.TaxableAmt = new Decimal?(currencyInfo.CuryConvBase(curyval1));
    histSVAT.CuryTaxAmt = new Decimal?(curyval2);
    histSVAT.TaxAmt = new Decimal?(currencyInfo.CuryConvBase(curyval2));
    histSVAT.CuryUnrecognizedTaxAmt = new Decimal?(curyval2);
    histSVAT.UnrecognizedTaxAmt = new Decimal?(currencyInfo.CuryConvBase(curyval2));
  }

  public static void FillBaseAmounts(this SVATConversionHist histSVAT, PXCache cache)
  {
    Decimal baseval1;
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvBase<SVATConversionHist.curyInfoID>(cache, (object) histSVAT, histSVAT.CuryTaxableAmt.GetValueOrDefault(), out baseval1);
    Decimal baseval2;
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvBase<SVATConversionHist.curyInfoID>(cache, (object) histSVAT, histSVAT.CuryTaxAmt.GetValueOrDefault(), out baseval2);
    Decimal baseval3;
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvBase<SVATConversionHist.curyInfoID>(cache, (object) histSVAT, histSVAT.CuryUnrecognizedTaxAmt.GetValueOrDefault(), out baseval3);
    histSVAT.TaxableAmt = new Decimal?(baseval1);
    histSVAT.TaxAmt = new Decimal?(baseval2);
    histSVAT.UnrecognizedTaxAmt = new Decimal?(baseval3);
  }
}
