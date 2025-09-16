// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.ICurrencyHelperEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CM.Extensions;
using System;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

public interface ICurrencyHelperEx : IPXCurrencyHelper
{
  CurrencyInfo CloneCurrencyInfo(CurrencyInfo currencyInfo);

  CurrencyInfo CloneCurrencyInfo(CurrencyInfo currencyInfo, DateTime? currencyEffectiveDate);
}
