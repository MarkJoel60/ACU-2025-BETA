// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyAttributeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

internal static class CurrencyAttributeHelper
{
  internal static int? GetPrecision(
    this PXGraph graph,
    PXCache sender,
    object row,
    string fieldName,
    Dictionary<long, string> _Matches)
  {
    return new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph).CuryDecimalPlaces(graph.GetCuryID(sender, row, fieldName, _Matches)));
  }

  private static string GetCuryID(
    this PXGraph graph,
    PXCache sender,
    object row,
    string fieldName,
    Dictionary<long, string> _Matches)
  {
    if (_Matches == null)
    {
      long? key = (long?) sender.GetValue(row ?? sender.InternalCurrent, fieldName);
      IPXCurrencyHelper implementation = graph.FindImplementation<IPXCurrencyHelper>();
      CurrencyInfo currencyInfo = implementation?.GetCurrencyInfo(key) ?? implementation.GetDefaultCurrencyInfo();
      return graph.Accessinfo.CuryViewState ? currencyInfo?.BaseCuryID : currencyInfo?.CuryID;
    }
    long? nullable = (long?) sender.GetValue(row, fieldName);
    string str;
    return nullable.HasValue && _Matches.TryGetValue(nullable.Value, out str) ? str : string.Empty;
  }
}
