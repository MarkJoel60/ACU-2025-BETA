// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PaymentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

public static class PaymentEntry
{
  public static void CuryConvCury(
    Decimal? BaseAmt,
    out Decimal? CuryAmt,
    Decimal CuryRate,
    string CuryMultDiv,
    int CuryPrecision)
  {
    if (CuryMultDiv == "D" && BaseAmt.HasValue)
      CuryAmt = new Decimal?(Math.Round(BaseAmt.Value * CuryRate, CuryPrecision, MidpointRounding.AwayFromZero));
    else if (CuryRate != 0M && BaseAmt.HasValue)
      CuryAmt = new Decimal?(Math.Round(BaseAmt.Value / CuryRate, CuryPrecision, MidpointRounding.AwayFromZero));
    else
      CuryAmt = BaseAmt;
  }

  public static void CuryConvBase(
    Decimal? CuryAmt,
    out Decimal? BaseAmt,
    Decimal CuryRate,
    string CuryMultDiv,
    int BasePrecision)
  {
    if (CuryMultDiv == "M" && CuryAmt.HasValue)
      BaseAmt = new Decimal?(Math.Round(CuryAmt.Value * CuryRate, BasePrecision, MidpointRounding.AwayFromZero));
    else if (CuryRate != 0M && CuryAmt.HasValue)
      BaseAmt = new Decimal?(Math.Round(CuryAmt.Value / CuryRate, BasePrecision, MidpointRounding.AwayFromZero));
    else
      BaseAmt = CuryAmt;
  }

  public static Decimal? CalcBalances(
    Decimal? CuryDocBal,
    Decimal? DocBal,
    string PayCuryID,
    string DocCuryID,
    string BaseCuryID,
    Decimal PayCuryRate,
    string PayCuryMultDiv,
    Decimal DocCuryRate,
    string DocCuryMultDiv,
    int CuryPrecision,
    int BasePrecision)
  {
    Decimal? CuryAmt;
    if (object.Equals((object) PayCuryID, (object) DocCuryID))
      CuryAmt = CuryDocBal;
    else if (object.Equals((object) BaseCuryID, (object) DocCuryID))
    {
      PaymentEntry.CuryConvCury(DocBal, out CuryAmt, PayCuryRate, PayCuryMultDiv, CuryPrecision);
    }
    else
    {
      Decimal? BaseAmt;
      PaymentEntry.CuryConvBase(CuryDocBal, out BaseAmt, DocCuryRate, DocCuryMultDiv, BasePrecision);
      PaymentEntry.CuryConvCury(BaseAmt, out CuryAmt, PayCuryRate, PayCuryMultDiv, CuryPrecision);
    }
    return CuryAmt;
  }

  public static void CalcDiscount(DateTime? PayDate, IInvoice voucher, IAdjustment adj)
  {
    if (!PayDate.HasValue || !voucher.DiscDate.HasValue || PayDate.Value.CompareTo(voucher.DiscDate.Value) <= 0)
      return;
    adj.CuryDiscBal = new Decimal?(0M);
    adj.DiscBal = new Decimal?(0M);
  }

  public static void WarnDiscount<TInvoice, TAdjust>(
    PXGraph graph,
    DateTime? PayDate,
    TInvoice invoice,
    TAdjust adj)
    where TInvoice : IInvoice
    where TAdjust : class, IBqlTable, IAdjustment
  {
    if (adj.Released.GetValueOrDefault() || !invoice.DiscDate.HasValue || !adj.AdjgDocDate.HasValue || adj.AdjgDocDate.Value.CompareTo(invoice.DiscDate.Value) <= 0)
      return;
    Decimal? curyAdjgDiscAmt = adj.CuryAdjgDiscAmt;
    Decimal num = 0M;
    if (!(curyAdjgDiscAmt.GetValueOrDefault() > num & curyAdjgDiscAmt.HasValue))
      return;
    graph.Caches[typeof (TAdjust)].RaiseExceptionHandling("CuryAdjgDiscAmt", (object) adj, (object) adj.CuryAdjgDiscAmt, (Exception) new PXSetPropertyException("The application date is later than the cash discount date ({0}).", (PXErrorLevel) 2, new object[1]
    {
      (object) invoice.DiscDate
    }));
  }

  public static void WarnPPDiscount<TInvoice, TAdjust>(
    PXGraph graph,
    DateTime? PayDate,
    TInvoice invoice,
    TAdjust adj,
    Decimal? CuryAdjgPPDAmt)
    where TInvoice : IInvoice
    where TAdjust : class, IBqlTable, IAdjustment
  {
    if (adj.Released.GetValueOrDefault() || !invoice.DiscDate.HasValue || !adj.AdjgDocDate.HasValue || adj.AdjgDocDate.Value.CompareTo(invoice.DiscDate.Value) <= 0)
      return;
    Decimal? nullable = CuryAdjgPPDAmt;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    graph.Caches[typeof (TAdjust)].RaiseExceptionHandling(nameof (CuryAdjgPPDAmt), (object) adj, (object) CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("The application date is later than the cash discount date ({0}).", (PXErrorLevel) 2, new object[1]
    {
      (object) invoice.DiscDate
    }));
  }
}
