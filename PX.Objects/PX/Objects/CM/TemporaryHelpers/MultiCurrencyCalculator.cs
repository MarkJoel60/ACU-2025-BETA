// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TemporaryHelpers.MultiCurrencyCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.TemporaryHelpers;

/// <summary>
/// Povides SOME operations for currency-related calculations. Uses IPXCurrencyHelper (MultiCurrency Extension) if available, otherwise uses PXCurrencyAttribute
/// </summary>
internal static class MultiCurrencyCalculator
{
  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
  {
    IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
    if (implementation == null)
      PX.Objects.CM.PXCurrencyAttribute.CuryConvBase(sender, row, curyval, out baseval);
    else
      baseval = implementation.GetDefaultCurrencyInfo().CuryConvBase(curyval);
  }

  public static void CuryConvBaseSkipRounding(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
  {
    IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
    if (implementation == null)
    {
      PX.Objects.CM.PXCurrencyAttribute.CuryConvBase(sender, row, curyval, out baseval, true);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = implementation.GetDefaultCurrencyInfo();
      baseval = defaultCurrencyInfo != null ? defaultCurrencyInfo.CuryConvBaseRaw(curyval) : curyval;
    }
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval)
  {
    IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
    if (implementation == null)
    {
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(sender, row, baseval, out curyval);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = implementation.GetDefaultCurrencyInfo();
      curyval = defaultCurrencyInfo != null ? defaultCurrencyInfo.CuryConvCury(baseval) : baseval;
    }
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
  {
    IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
    if (implementation == null)
    {
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(sender, row, baseval, out curyval, precision);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = implementation.GetDefaultCurrencyInfo();
      curyval = defaultCurrencyInfo != null ? defaultCurrencyInfo.CuryConvCury(baseval, new int?(precision)) : baseval;
    }
  }

  public static Decimal RoundCury(PXCache sender, object row, Decimal val)
  {
    IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
    return implementation == null ? PX.Objects.CM.PXCurrencyAttribute.RoundCury(sender, row, val) : implementation.RoundCury(val);
  }

  public static Decimal RoundCury(PXCache sender, object row, Decimal val, int? customPrecision)
  {
    if (!customPrecision.HasValue)
      return MultiCurrencyCalculator.RoundCury(sender, row, val);
    return sender.Graph.FindImplementation<IPXCurrencyHelper>() == null ? PX.Objects.CM.PXDBCurrencyAttribute.RoundCury(sender, row, val, customPrecision) : Math.Round(val, customPrecision.Value, MidpointRounding.AwayFromZero);
  }

  public static string GetCurrencyID(PXGraph graph)
  {
    IPXCurrencyHelper implementation = graph.FindImplementation<IPXCurrencyHelper>();
    return implementation == null ? (!(((PXCache) GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>(graph)).Current is PX.Objects.CM.CurrencyInfo current) ? (string) null : current.CuryID) : implementation.GetDefaultCurrencyInfo()?.CuryID;
  }

  public static PX.Objects.CM.Currency GetCurrentCurrency(PXGraph graph)
  {
    string currencyId = MultiCurrencyCalculator.GetCurrencyID(graph);
    return currencyId == null ? (PX.Objects.CM.Currency) null : CurrencyCollection.GetCurrency(currencyId);
  }

  public static PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo<CuryInfoIDField>(
    PXGraph graph,
    object row)
    where CuryInfoIDField : IBqlField
  {
    PXCache cach = graph.Caches[row.GetType()];
    IPXCurrencyHelper implementation = graph.FindImplementation<IPXCurrencyHelper>();
    if (implementation != null)
      return implementation.GetCurrencyInfo(cach.GetValue<CuryInfoIDField>(row) as long?);
    PX.Objects.CM.CurrencyInfo currencyInfo = PX.Objects.CM.CurrencyInfoAttribute.GetCurrencyInfo<CuryInfoIDField>(cach, row);
    if (currencyInfo == null)
      currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select(graph, new object[1]
      {
        cach.GetValue<CuryInfoIDField>(row)
      }));
    PX.Objects.CM.CurrencyInfo info = currencyInfo;
    return info == null ? (PX.Objects.CM.Extensions.CurrencyInfo) null : PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info);
  }
}
