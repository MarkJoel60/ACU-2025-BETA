// Decompiled with JetBrains decompiler
// Type: PX.Objects.Unit.CurrencyServiceMock
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Unit;

public class CurrencyServiceMock : IPXCurrencyService
{
  public const string CurrencyUSD = "USD";
  public const string CurrencyEUR = "EUR";
  public const string CurrencyJPY = "JPY";
  public const Decimal DefaultCuryRate = 1M;
  public const short DefaultCuryPrecision = 2;
  public const short JPYPrecision = 0;

  public int BaseDecimalPlaces() => 2;

  public int CuryDecimalPlaces(string curyID) => curyID == "JPY" ? 0 : this.BaseDecimalPlaces();

  public int PriceCostDecimalPlaces() => 4;

  public int QuantityDecimalPlaces() => 6;

  public string DefaultRateTypeID(string moduleCode) => "SPOT";

  public IPXCurrencyRate GetRate(
    string fromCuryID,
    string toCuryID,
    string rateTypeID,
    DateTime? curyEffDate)
  {
    if (fromCuryID != toCuryID)
      return (IPXCurrencyRate) new CurrencyRate()
      {
        FromCuryID = fromCuryID,
        CuryEffDate = new DateTime?(DateTime.Today),
        CuryMultDiv = "M",
        CuryRate = new Decimal?(1.28M),
        RateReciprocal = new Decimal?(0.78125M),
        ToCuryID = toCuryID
      };
    return (IPXCurrencyRate) new CurrencyRate()
    {
      FromCuryID = fromCuryID,
      CuryEffDate = new DateTime?(DateTime.Today),
      CuryMultDiv = "M",
      CuryRate = new Decimal?(1M),
      RateReciprocal = new Decimal?(1M),
      ToCuryID = toCuryID
    };
  }

  public int GetRateEffDays(string rateTypeID) => 3;

  public string BaseCuryID() => "USD";

  public string BaseCuryID(int? branchID) => "USD";

  public IEnumerable<IPXCurrency> Currencies()
  {
    return (IEnumerable<IPXCurrency>) new IPXCurrency[3]
    {
      (IPXCurrency) new PX.Objects.CM.Extensions.Currency()
      {
        CuryID = "USD",
        Description = "Dollar"
      },
      (IPXCurrency) new PX.Objects.CM.Extensions.Currency()
      {
        CuryID = "EUR",
        Description = "Euro"
      },
      (IPXCurrency) new PX.Objects.CM.Extensions.Currency()
      {
        CuryID = "JPY",
        Description = "Yen"
      }
    };
  }

  public IEnumerable<IPXCurrencyRateType> CurrencyRateTypes()
  {
    return (IEnumerable<IPXCurrencyRateType>) new IPXCurrencyRateType[2]
    {
      (IPXCurrencyRateType) new CurrencyRateType()
      {
        CuryRateTypeID = "SPOT",
        Descr = "Spot"
      },
      (IPXCurrencyRateType) new CurrencyRateType()
      {
        CuryRateTypeID = "BANK",
        Descr = "Bank"
      }
    };
  }

  public void PopulatePrecision(PXCache cache, CurrencyInfo info)
  {
    if (info == null || info.CuryPrecision.HasValue && info.BasePrecision.HasValue)
      return;
    if (!info.CuryPrecision.HasValue)
      info.CuryPrecision = new short?(Convert.ToInt16(this.CuryDecimalPlaces(info.CuryID)));
    if (!info.BasePrecision.HasValue)
      info.BasePrecision = new short?(Convert.ToInt16(this.CuryDecimalPlaces(info.BaseCuryID)));
    if (cache.GetStatus((object) info) != null)
      return;
    cache.SetStatus((object) info, (PXEntryStatus) 5);
  }
}
