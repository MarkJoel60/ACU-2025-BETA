// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.IPXCurrencyService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

public interface IPXCurrencyService
{
  int BaseDecimalPlaces();

  int CuryDecimalPlaces(string curyID);

  int PriceCostDecimalPlaces();

  int QuantityDecimalPlaces();

  string DefaultRateTypeID(string moduleCode);

  IPXCurrencyRate GetRate(
    string fromCuryID,
    string toCuryID,
    string rateTypeID,
    DateTime? curyEffDate);

  int GetRateEffDays(string rateTypeID);

  /// <summary>Returns base currency of the tenant.</summary>
  string BaseCuryID();

  /// <summary>
  /// Returns base currency of the branch or base currency of the tenant if branchID is null.
  /// </summary>
  string BaseCuryID(int? branchID);

  IEnumerable<IPXCurrency> Currencies();

  IEnumerable<IPXCurrencyRateType> CurrencyRateTypes();

  void PopulatePrecision(PXCache cache, CurrencyInfo info);
}
