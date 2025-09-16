// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Services.AmountDistribution.AccumulateRemainderToNonZeroLineService`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO.Services.AmountDistribution;

public class AccumulateRemainderToNonZeroLineService<Item>(
  DistributionParameter<Item> distributeParameter) : RemainderToBiggestLineService<Item>(distributeParameter)
  where Item : class, IAmountItem
{
  protected Decimal? _accumulatedAmount;
  protected Decimal? _curyAccumulatedAmount;

  protected override void Clear()
  {
    this._accumulatedAmount = new Decimal?(0M);
    this._curyAccumulatedAmount = new Decimal?(0M);
    base.Clear();
  }

  protected override void RoundAmount(
    Item item,
    ref Decimal? currentAmount,
    ref Decimal? curyCurrentAmount)
  {
    ref Decimal? local1 = ref currentAmount;
    Decimal? nullable1 = currentAmount;
    Decimal? accumulatedAmount = this._accumulatedAmount;
    Decimal? nullable2 = nullable1.HasValue & accumulatedAmount.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + accumulatedAmount.GetValueOrDefault()) : new Decimal?();
    local1 = nullable2;
    ref Decimal? local2 = ref curyCurrentAmount;
    Decimal? nullable3 = curyCurrentAmount;
    nullable1 = this._curyAccumulatedAmount;
    Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local2 = nullable4;
    Decimal? nullable5 = currentAmount;
    Decimal? nullable6 = curyCurrentAmount;
    base.RoundAmount(item, ref currentAmount, ref curyCurrentAmount);
    nullable1 = nullable5;
    nullable3 = currentAmount;
    this._accumulatedAmount = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    nullable3 = nullable6;
    nullable1 = curyCurrentAmount;
    this._curyAccumulatedAmount = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
  }
}
