// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.SingleCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CM.Extensions;
using System;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

/// <summary>
/// An implementation of IPXCurrencyHelper for a screen on which the same currency is always expected
/// and CurrencyInfo is never persisted.
/// </summary>
public abstract class SingleCurrencyGraph<TGraph, TPrimary> : 
  PXGraphExtension<TGraph>,
  IPXCurrencyHelper
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>> currencyinfobykey;

  public CurrencyInfo GetCurrencyInfo(long? key)
  {
    return (CurrencyInfo) this.currencyinfobykey.Select((object) key);
  }

  public CurrencyInfo GetDefaultCurrencyInfo()
  {
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base);
    string curyID = pxCurrencyService.BaseCuryID();
    short int16 = Convert.ToInt16(pxCurrencyService.CuryDecimalPlaces(curyID));
    return new CurrencyInfo()
    {
      CuryID = curyID,
      BaseCuryID = curyID,
      CuryRate = new Decimal?(1M),
      RecipRate = new Decimal?(1M),
      CuryPrecision = new short?(int16),
      BasePrecision = new short?(int16)
    };
  }
}
