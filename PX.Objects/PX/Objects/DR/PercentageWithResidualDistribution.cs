// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.PercentageWithResidualDistribution
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.DR.Descriptor;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

internal class PercentageWithResidualDistribution : PercentageDistribution
{
  private readonly Decimal _fairUnitPrice;
  private readonly Decimal _fixedPerUnit;
  private readonly Decimal _compoundDiscountRate;
  private readonly Decimal _qtyInBaseUnit;

  public PercentageWithResidualDistribution(
    IInventoryItemProvider inventoryItemProvider,
    IEnumerable<InventoryItemComponentInfo> percentageComponents,
    Decimal fairUnitPrice,
    Decimal fixedPerUnit,
    Decimal compoundDiscountRate,
    Decimal qtyInBaseUnit,
    Func<Decimal, Decimal> roundingFunction)
    : base(inventoryItemProvider, percentageComponents, roundingFunction)
  {
    this._fairUnitPrice = fairUnitPrice;
    this._fixedPerUnit = fixedPerUnit;
    this._compoundDiscountRate = compoundDiscountRate;
    this._qtyInBaseUnit = qtyInBaseUnit;
  }

  public override IEnumerable<Tuple<InventoryItemComponentInfo, Decimal>> Distribute(
    Decimal tranAmt,
    Decimal amountToDistribute)
  {
    base.Distribute(tranAmt, amountToDistribute);
    if (amountToDistribute - this._amounts.Sum<Tuple<InventoryItemComponentInfo, Decimal>>((Func<Tuple<InventoryItemComponentInfo, Decimal>, Decimal>) (componentAmount => componentAmount.Item2)) < 0M)
      this._amounts = new PercentageDistribution(this._inventoryItemProvider, this._percentageComponents, this._roundingFunction).Distribute(tranAmt, amountToDistribute).ToList<Tuple<InventoryItemComponentInfo, Decimal>>();
    return (IEnumerable<Tuple<InventoryItemComponentInfo, Decimal>>) this._amounts;
  }

  protected override Decimal GetAmountForComponent(
    INComponent inventoryItemComponent,
    Decimal amountToDistribute)
  {
    return amountToDistribute == 0M ? 0M : this._roundingFunction((this._fairUnitPrice - this._fixedPerUnit) * this._compoundDiscountRate * this._qtyInBaseUnit * inventoryItemComponent.Percentage.Value * 0.01M);
  }
}
