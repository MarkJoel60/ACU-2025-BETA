// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyServiceHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public static class CurrencyServiceHelper
{
  public static IPXCurrencyRate SearchForNewRate(this CurrencyInfo info, PXGraph graph)
  {
    return ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph).GetRate(info.CuryID, info.BaseCuryID, info.CuryRateTypeID, info.CuryEffDate);
  }

  public static void Populate(this IPXCurrencyRate rate, CurrencyInfo info)
  {
    info.CuryEffDate = rate.CuryEffDate;
    info.CuryRate = new Decimal?(Math.Round(rate.CuryRate.Value, 8));
    info.CuryMultDiv = rate.CuryMultDiv;
    info.RecipRate = new Decimal?(Math.Round(rate.RateReciprocal.Value, 8));
  }
}
