// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CuryHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM;

public class CuryHelper : IPXCurrencyHelper
{
  private PXSelectBase<CurrencyInfo> Currencyinfoselect { get; }

  public CuryHelper(PXSelectBase<CurrencyInfo> currencyinfoselect)
  {
    this.Currencyinfoselect = currencyinfoselect;
  }

  public CurrencyInfo GetCurrencyInfo(long? currencyInfoID)
  {
    return CurrencyInfoCache.GetInfo(this.Currencyinfoselect, currencyInfoID);
  }

  PX.Objects.CM.Extensions.CurrencyInfo IPXCurrencyHelper.GetCurrencyInfo(long? key)
  {
    return PX.Objects.CM.Extensions.CurrencyInfo.GetEX(this.GetCurrencyInfo(key));
  }

  public PX.Objects.CM.Extensions.CurrencyInfo GetDefaultCurrencyInfo()
  {
    return PX.Objects.CM.Extensions.CurrencyInfo.GetEX(this.Currencyinfoselect.SelectSingle(Array.Empty<object>()));
  }
}
