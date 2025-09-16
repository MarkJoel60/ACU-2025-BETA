// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Services.AmountDistribution.RemainderToLastLineService`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PO.Services.AmountDistribution;

public class RemainderToLastLineService<Item>(DistributionParameter<Item> distributeParameter) : 
  RemainderToBiggestLineService<Item>(distributeParameter)
  where Item : class, IAmountItem
{
  protected Item _lastLine;

  protected override void Clear()
  {
    this._lastLine = default (Item);
    base.Clear();
  }

  protected override void ProcessItem(
    Item item,
    Decimal? sumOfWeight,
    ref Decimal? sumOfAmt,
    ref Decimal? curySumOfAmt)
  {
    base.ProcessItem(item, sumOfWeight, ref sumOfAmt, ref curySumOfAmt);
    this.SetLastLine(item);
  }

  protected virtual void SetLastLine(Item item)
  {
    if (!(item.Weight > 0M))
      return;
    this._lastLine = item;
  }

  protected override void DistributeRoundingDifference(
    Item item,
    Decimal? roundingDifference,
    Decimal? curyRoundingDifference)
  {
    base.DistributeRoundingDifference(this._lastLine, roundingDifference, curyRoundingDifference);
  }
}
