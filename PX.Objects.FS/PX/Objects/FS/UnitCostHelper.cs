// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.UnitCostHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

internal static class UnitCostHelper
{
  public static UnitCostHelper.UnitCostPair CalculateCuryUnitCost<unitCostField, inventoryIDField, uomField>(
    PXCache cache,
    object row,
    bool raiseUnitCostDefaulting,
    Decimal? unitCost)
    where unitCostField : IBqlField
    where inventoryIDField : IBqlField
    where uomField : IBqlField
  {
    Decimal num1 = 0M;
    if (raiseUnitCostDefaulting)
    {
      object obj;
      cache.RaiseFieldDefaulting<unitCostField>(row, ref obj);
      unitCost = (Decimal?) obj;
    }
    if (unitCost.HasValue)
    {
      Decimal? nullable = unitCost;
      Decimal num2 = 0M;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        Decimal curyval = INUnitAttribute.ConvertToBase<inventoryIDField, uomField>(cache, row, unitCost.Value, INPrecision.NOROUND);
        IPXCurrencyHelper implementation = cache.Graph.FindImplementation<IPXCurrencyHelper>();
        if (implementation != null)
          curyval = implementation.GetDefaultCurrencyInfo().CuryConvCury(unitCost.Value);
        else
          PXDBCurrencyAttribute.CuryConvCury(cache, row, curyval, out curyval, true);
        num1 = Math.Round(curyval, CommonSetupDecPl.PrcCst, MidpointRounding.AwayFromZero);
      }
    }
    return new UnitCostHelper.UnitCostPair(new Decimal?(num1), unitCost);
  }

  public class UnitCostPair
  {
    public Decimal? unitCost;
    public Decimal? curyUnitCost;

    public UnitCostPair(Decimal? unitCost, Decimal? curyUnitCost)
    {
      this.unitCost = unitCost;
      this.curyUnitCost = curyUnitCost;
    }
  }
}
