// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.PXCurrencyHelperExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CM.Extensions;
using System;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

public static class PXCurrencyHelperExtension
{
  public static Decimal RoundCury(this IPXCurrencyHelper pXCurrencyHelper, Decimal val)
  {
    CurrencyInfo defaultCurrencyInfo = pXCurrencyHelper.GetDefaultCurrencyInfo();
    return defaultCurrencyInfo == null ? val : defaultCurrencyInfo.RoundCury(val);
  }
}
