// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ExtensionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public static class ExtensionHelper
{
  public static PX.Objects.CM.Extensions.CurrencyInfo SelectCurrencyInfo(
    PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> currencyInfoView,
    long? curyInfoID)
  {
    if (!curyInfoID.HasValue)
      return (PX.Objects.CM.Extensions.CurrencyInfo) null;
    PX.Objects.CM.Extensions.CurrencyInfo current = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) currencyInfoView).Cache.Current;
    if (current != null)
    {
      long? nullable = curyInfoID;
      long? curyInfoId = current.CuryInfoID;
      if (nullable.GetValueOrDefault() == curyInfoId.GetValueOrDefault() & nullable.HasValue == curyInfoId.HasValue)
        return current;
    }
    return currencyInfoView.SelectSingle(Array.Empty<object>());
  }
}
