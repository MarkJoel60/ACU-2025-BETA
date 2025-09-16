// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.StandardCostPrecisionExtBase`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class StandardCostPrecisionExtBase<TGraph, TSettings, TSettingsInventoryID, TStdCost, TPendingStdCost> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TSettings : class, IBqlTable, new()
  where TSettingsInventoryID : class, IBqlField
  where TStdCost : class, IBqlField
  where TPendingStdCost : class, IBqlField
{
  protected virtual void _(Events.RowSelected<TSettings> e)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, (int?) ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TSettings>>) e).Cache.GetValue<TSettingsInventoryID>((object) e.Row));
    if (inventoryItem == null || inventoryItem.ValMethod != "T")
      return;
    CommonSetup commonSetup = PXResultset<CommonSetup>.op_Implicit(PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    PX.Objects.CM.Currency baseCurrency = this.GetBaseCurrency(e.Row);
    if (commonSetup == null || baseCurrency == null)
      return;
    short? nullable1 = commonSetup.DecPlPrcCst;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = baseCurrency.DecimalPlaces;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    if (nullable2.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
      return;
    PXCache cache1 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TSettings>>) e).Cache;
    TSettings row1 = e.Row;
    InventoryItem inventory1 = inventoryItem;
    nullable1 = baseCurrency.DecimalPlaces;
    int valueOrDefault1 = (int) nullable1.GetValueOrDefault();
    this.SetStandardCostExceedsBaseCurrencyPrecisionWarningIfNeeded<TStdCost>(cache1, row1, inventory1, valueOrDefault1);
    PXCache cache2 = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TSettings>>) e).Cache;
    TSettings row2 = e.Row;
    InventoryItem inventory2 = inventoryItem;
    nullable1 = baseCurrency.DecimalPlaces;
    int valueOrDefault2 = (int) nullable1.GetValueOrDefault();
    this.SetStandardCostExceedsBaseCurrencyPrecisionWarningIfNeeded<TPendingStdCost>(cache2, row2, inventory2, valueOrDefault2);
  }

  private void SetStandardCostExceedsBaseCurrencyPrecisionWarningIfNeeded<TField>(
    PXCache cache,
    TSettings row,
    InventoryItem inventory,
    int decimalPlaces)
    where TField : class, IBqlField
  {
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, "The decimal precision of the cost of the {0} item with the Standard valuation method exceeds the currency precision. Consider decreasing the Price/Cost Decimal Places on the Companies (CS101500) form.", (PXErrorLevel) 2, new object[1]
    {
      (object) inventory.InventoryCD
    });
    Decimal valueOrDefault = ((Decimal?) cache.GetValue<TField>((object) row)).GetValueOrDefault();
    if (Math.Round(valueOrDefault, decimalPlaces) != valueOrDefault)
    {
      cache.RaiseExceptionHandling<TField>((object) row, (object) valueOrDefault, (Exception) propertyException);
    }
    else
    {
      if (!(PXUIFieldAttribute.GetError(cache, (object) row, typeof (TField).Name) == string.Format(PXMessages.LocalizeNoPrefix("The decimal precision of the cost of the {0} item with the Standard valuation method exceeds the currency precision. Consider decreasing the Price/Cost Decimal Places on the Companies (CS101500) form."), (object) inventory.InventoryCD)))
        return;
      cache.RaiseExceptionHandling<TField>((object) row, (object) valueOrDefault, (Exception) null);
    }
  }

  protected abstract PX.Objects.CM.Currency GetBaseCurrency(TSettings settings);
}
