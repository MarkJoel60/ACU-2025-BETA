// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Services.AmountDistribution.RemainderToBiggestLineService`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CM;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.Services.AmountDistribution;

public class RemainderToBiggestLineService<Item> : IAmountDistributionService<Item> where Item : class, IAmountItem
{
  protected DistributionParameter<Item> _context;
  protected Item _biggestLine;
  protected Decimal? _biggestLineWeight;

  public RemainderToBiggestLineService(DistributionParameter<Item> distributeParameter)
  {
    this._context = distributeParameter;
  }

  public virtual DistributionResult<Item> Distribute()
  {
    Decimal sumOfWeight = this.GetSumOfWeight();
    if (sumOfWeight == 0M)
      return new DistributionResult<Item>()
      {
        Successful = false
      };
    Decimal? sumOfAmt = new Decimal?(0M);
    Decimal? curySumOfAmt = new Decimal?(0M);
    this.Clear();
    foreach (Item obj in this._context.Items)
      this.ProcessItem(obj, new Decimal?(sumOfWeight), ref sumOfAmt, ref curySumOfAmt);
    Decimal? nullable1 = this._context.Amount;
    Decimal? nullable2 = sumOfAmt;
    Decimal? roundingDifference = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = this._context.CuryAmount;
    nullable1 = curySumOfAmt;
    Decimal? curyRoundingDifference = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = roundingDifference;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = curyRoundingDifference;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
        goto label_12;
    }
    this.DistributeRoundingDifference(this._biggestLine, roundingDifference, curyRoundingDifference);
label_12:
    return new DistributionResult<Item>()
    {
      Successful = true
    };
  }

  protected virtual void Clear()
  {
    this._biggestLine = default (Item);
    this._biggestLineWeight = new Decimal?();
  }

  protected virtual Decimal GetSumOfWeight()
  {
    return this._context.Items.Sum<Item>((Func<Item, Decimal>) (i => i.Weight));
  }

  protected virtual void ProcessItem(
    Item item,
    Decimal? sumOfWeight,
    ref Decimal? sumOfAmt,
    ref Decimal? curySumOfAmt)
  {
    this.SetBiggestLine(item);
    Decimal? currentAmount;
    Decimal? curyCurrentAmount;
    this.CalculateAmount(item.Weight, sumOfWeight, out currentAmount, out curyCurrentAmount);
    this.ReplaceAmount(item, ref currentAmount, ref curyCurrentAmount);
    this.RoundAmount(item, ref currentAmount, ref curyCurrentAmount);
    this.ValueCalculated(item, currentAmount, curyCurrentAmount);
    ref Decimal? local1 = ref sumOfAmt;
    Decimal? nullable1 = sumOfAmt;
    Decimal? nullable2 = currentAmount;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    local1 = nullable3;
    ref Decimal? local2 = ref curySumOfAmt;
    nullable2 = curySumOfAmt;
    Decimal? nullable4 = curyCurrentAmount;
    Decimal? nullable5 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    local2 = nullable5;
  }

  protected virtual void CalculateAmount(
    Decimal weight,
    Decimal? sumOfWeight,
    out Decimal? currentAmount,
    out Decimal? curyCurrentAmount)
  {
    ref Decimal? local1 = ref currentAmount;
    Decimal? nullable1 = this._context.Amount;
    Decimal num1 = weight;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
    Decimal? nullable3 = sumOfWeight;
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault());
    local1 = nullable4;
    ref Decimal? local2 = ref curyCurrentAmount;
    nullable1 = this._context.CuryAmount;
    Decimal num2 = weight;
    Decimal? nullable5 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
    nullable2 = sumOfWeight;
    Decimal? nullable6;
    if (!(nullable5.HasValue & nullable2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable6 = nullable1;
    }
    else
      nullable6 = new Decimal?(nullable5.GetValueOrDefault() / nullable2.GetValueOrDefault());
    local2 = nullable6;
  }

  protected virtual void ReplaceAmount(
    Item item,
    ref Decimal? currentAmount,
    ref Decimal? curyCurrentAmount)
  {
    if (this._context.ReplaceAmount == null)
      return;
    Tuple<Decimal?, Decimal?> tuple = this._context.ReplaceAmount(item, currentAmount, curyCurrentAmount);
    currentAmount = tuple.Item1;
    curyCurrentAmount = tuple.Item2;
  }

  protected virtual void RoundAmount(
    Item item,
    ref Decimal? currentAmount,
    ref Decimal? curyCurrentAmount)
  {
    currentAmount = this.RoundAmountDecimal(currentAmount, false);
    curyCurrentAmount = this.RoundAmountDecimal(curyCurrentAmount, true);
  }

  protected virtual void ValueCalculated(
    Item item,
    Decimal? currentAmount,
    Decimal? curyCurrentAmount)
  {
    item.Amount = currentAmount;
    item.CuryAmount = curyCurrentAmount;
    Func<Item, Decimal?, Decimal?, Item> onValueCalculated = this._context.OnValueCalculated;
    Item obj = onValueCalculated != null ? onValueCalculated(item, currentAmount, curyCurrentAmount) : default (Item);
    if ((object) obj == null || (object) item != (object) this._biggestLine)
      return;
    this._biggestLine = obj;
  }

  protected virtual void SetBiggestLine(Item item)
  {
    if ((object) this._biggestLine != null)
    {
      Decimal weight = item.Weight;
      Decimal? biggestLineWeight = this._biggestLineWeight;
      Decimal valueOrDefault = biggestLineWeight.GetValueOrDefault();
      if (!(weight > valueOrDefault & biggestLineWeight.HasValue))
        return;
    }
    this._biggestLine = item;
    this._biggestLineWeight = new Decimal?(item.Weight);
  }

  protected virtual void DistributeRoundingDifference(
    Item item,
    Decimal? roundingDifference,
    Decimal? curyRoundingDifference)
  {
    Decimal? amount1 = item.Amount;
    Decimal? amount2 = item.Amount;
    ref Item local1 = ref item;
    // ISSUE: variable of a boxed type
    __Boxed<Item> local2 = (object) local1;
    Decimal? amount3 = local1.Amount;
    Decimal? nullable1 = roundingDifference;
    Decimal? nullable2 = amount3.HasValue & nullable1.HasValue ? new Decimal?(amount3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    local2.Amount = nullable2;
    ref Item local3 = ref item;
    // ISSUE: variable of a boxed type
    __Boxed<Item> local4 = (object) local3;
    Decimal? curyAmount = local3.CuryAmount;
    Decimal? nullable3 = curyRoundingDifference;
    Decimal? nullable4 = curyAmount.HasValue & nullable3.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    local4.CuryAmount = nullable4;
    Action<Item, Decimal?, Decimal?, Decimal?, Decimal?> differenceApplied = this._context.OnRoundingDifferenceApplied;
    if (differenceApplied == null)
      return;
    differenceApplied(item, item.Amount, item.CuryAmount, amount1, amount2);
  }

  protected virtual Decimal? RoundAmountDecimal(Decimal? currentAmount, bool curyValue)
  {
    return curyValue ? (this._context.CacheOfCuryRow == null || this._context.CuryRow == null ? currentAmount : new Decimal?(PXDBCurrencyAttribute.RoundCury(this._context.CacheOfCuryRow, this._context.CuryRow, currentAmount.GetValueOrDefault()))) : (this._context.CacheOfCuryRow == null ? currentAmount : new Decimal?(PXDBCurrencyAttribute.BaseRound(this._context.CacheOfCuryRow.Graph, currentAmount.GetValueOrDefault())));
  }
}
