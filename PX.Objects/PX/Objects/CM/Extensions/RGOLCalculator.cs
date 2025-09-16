// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.RGOLCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class RGOLCalculator
{
  private readonly CurrencyInfo from_info;
  private readonly CurrencyInfo to_info;
  private readonly CurrencyInfo to_originfo;

  public RGOLCalculator(CurrencyInfo from_info, CurrencyInfo to_info, CurrencyInfo to_originfo)
  {
    this.from_info = from_info ?? throw new ArgumentNullException(nameof (from_info));
    this.to_info = to_info ?? throw new ArgumentNullException(nameof (to_info));
    this.to_originfo = to_originfo ?? throw new ArgumentNullException(nameof (to_originfo));
  }

  public RGOLCalculationResult CalcRGOL(Decimal? fromCuryAdjAmt, Decimal? fromAdjAmt)
  {
    Decimal? nullable1 = object.Equals((object) this.from_info.CuryID, (object) this.to_info.CuryID) ? fromCuryAdjAmt : new Decimal?(this.to_info.CuryConvCury(fromAdjAmt.Value));
    Decimal? nullable2 = !object.Equals((object) this.from_info.CuryID, (object) this.to_originfo.CuryID) || !object.Equals((object) this.from_info.CuryRate, (object) this.to_originfo.CuryRate) || !object.Equals((object) this.from_info.CuryMultDiv, (object) this.to_originfo.CuryMultDiv) ? new Decimal?(this.to_originfo.CuryConvBase(nullable1.Value)) : fromAdjAmt;
    RGOLCalculationResult calculationResult = new RGOLCalculationResult();
    calculationResult.ToCuryAdjAmt = nullable1;
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = fromAdjAmt;
    calculationResult.RgolAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    return calculationResult;
  }

  public RGOLCalculationResult CalcRGOL(
    Decimal? applicationCuryBal,
    Decimal? documentCuryBal,
    Decimal? documentBaseBal,
    Decimal? fromCuryAdjAmt,
    Decimal? fromAdjAmt)
  {
    Decimal? nullable1 = applicationCuryBal;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      Decimal? nullable2 = fromCuryAdjAmt;
      Decimal num2 = 0M;
      if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
        return new RGOLCalculationResult()
        {
          ToCuryAdjAmt = new Decimal?(documentCuryBal.Value),
          RgolAmt = new Decimal?(documentBaseBal.Value - fromAdjAmt.Value)
        };
    }
    return this.CalcRGOL(fromCuryAdjAmt, fromAdjAmt);
  }
}
