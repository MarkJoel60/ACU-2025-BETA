// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.PercentageDistribution
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.DR.Descriptor;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

internal class PercentageDistribution
{
  protected IInventoryItemProvider _inventoryItemProvider;
  protected IEnumerable<InventoryItemComponentInfo> _percentageComponents;
  protected List<Tuple<InventoryItemComponentInfo, Decimal>> _amounts;
  protected Func<Decimal, Decimal> _roundingFunction;

  public PercentageDistribution(
    IInventoryItemProvider inventoryItemProvider,
    IEnumerable<InventoryItemComponentInfo> percentageComponents,
    Func<Decimal, Decimal> roundingFunction)
  {
    this._inventoryItemProvider = inventoryItemProvider;
    this._percentageComponents = percentageComponents;
    this._roundingFunction = roundingFunction;
  }

  public virtual IEnumerable<Tuple<InventoryItemComponentInfo, Decimal>> Distribute(
    Decimal transactionAmount,
    Decimal amountToDistribute)
  {
    this._amounts = new List<Tuple<InventoryItemComponentInfo, Decimal>>();
    foreach (InventoryItemComponentInfo percentageComponent in this._percentageComponents)
    {
      INComponent component = percentageComponent.Component;
      Decimal amountForComponent = this.GetAmountForComponent(percentageComponent.Component, amountToDistribute);
      if (amountForComponent < 0M && transactionAmount >= 0M || amountForComponent > 0M && transactionAmount <= 0M)
        throw new PXException("Splitting deferred amount between components results in negative amount for component '{0}'. Can't create deferral schedule. Please correct Fixed Amount components.", new object[1]
        {
          (object) this._inventoryItemProvider.GetComponentName(component)
        });
      this._amounts.Add(new Tuple<InventoryItemComponentInfo, Decimal>(percentageComponent, amountForComponent));
    }
    return (IEnumerable<Tuple<InventoryItemComponentInfo, Decimal>>) this._amounts;
  }

  protected virtual Decimal GetAmountForComponent(
    INComponent inventoryItemComponent,
    Decimal amountToDistribute)
  {
    return inventoryItemComponent != this._percentageComponents.Last<InventoryItemComponentInfo>().Component ? this._roundingFunction(amountToDistribute * inventoryItemComponent.Percentage.Value / this._percentageComponents.Sum<InventoryItemComponentInfo>((Func<InventoryItemComponentInfo, Decimal>) (componentInfo => componentInfo.Component.Percentage.Value))) : amountToDistribute - this._amounts.Sum<Tuple<InventoryItemComponentInfo, Decimal>>((Func<Tuple<InventoryItemComponentInfo, Decimal>, Decimal>) (ca => ca.Item2));
  }
}
