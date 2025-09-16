// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CAMultiCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

/// <summary>The generic graph extension that defines the multi-currency functionality.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">A DAC (a <see cref="T:PX.Data.IBqlTable" /> type).</typeparam>
public abstract class CAMultiCurrencyGraph<TGraph, TPrimary> : MultiCurrencyGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  internal virtual void UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(
    PXCache tranDetailsCache,
    ICATranDetail oldTranDetail,
    ICATranDetail newTranDetail)
  {
    if (tranDetailsCache == null || newTranDetail == null)
      return;
    Decimal? nullable1 = (Decimal?) oldTranDetail?.CuryUnitPrice;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.CuryUnitPrice;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    bool flag1 = valueOrDefault1 != valueOrDefault2;
    Decimal? nullable2;
    if (oldTranDetail == null)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = oldTranDetail.CuryTranAmt;
    nullable1 = nullable2;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.CuryTranAmt;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    int num1 = valueOrDefault3 != valueOrDefault4 ? 1 : 0;
    Decimal? nullable3;
    if (oldTranDetail == null)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = oldTranDetail.Qty;
    nullable1 = nullable3;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    nullable1 = newTranDetail.Qty;
    Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
    bool flag2 = valueOrDefault5 != valueOrDefault6;
    if (num1 != 0)
    {
      nullable1 = newTranDetail.Qty;
      if (nullable1.HasValue)
      {
        nullable1 = newTranDetail.Qty;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          nullable1 = newTranDetail.CuryTranAmt;
          Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
          nullable1 = newTranDetail.Qty;
          Decimal num3 = nullable1.Value;
          Decimal val = valueOrDefault7 / num3;
          PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.GetDefaultCurrencyInfo();
          newTranDetail.CuryUnitPrice = new Decimal?(defaultCurrencyInfo.RoundCury(val));
          return;
        }
      }
      newTranDetail.CuryUnitPrice = newTranDetail.CuryTranAmt;
      newTranDetail.Qty = new Decimal?(1.0M);
    }
    else
    {
      if (!(flag1 | flag2))
        return;
      ICATranDetail caTranDetail = newTranDetail;
      nullable1 = newTranDetail.Qty;
      Decimal? curyUnitPrice = newTranDetail.CuryUnitPrice;
      Decimal? nullable4 = nullable1.HasValue & curyUnitPrice.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * curyUnitPrice.GetValueOrDefault()) : new Decimal?();
      caTranDetail.CuryTranAmt = nullable4;
    }
  }
}
