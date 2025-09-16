// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXPriceCostAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXPriceCostAttribute : PXDecimalAttribute
{
  public static Decimal Round(Decimal value)
  {
    return Math.Round(value, CommonSetupDecPl.PrcCst, MidpointRounding.AwayFromZero);
  }

  public static Decimal MinPrice(
    InventoryItem ii,
    INItemCost cost,
    InventoryItemCurySettings itemCurySettings)
  {
    if (!(ii.ValMethod != "T"))
      return ((Decimal?) itemCurySettings?.StdCost).GetValueOrDefault() + ((Decimal?) itemCurySettings?.StdCost).GetValueOrDefault() * 0.01M * ii.MinGrossProfitPct.GetValueOrDefault();
    Decimal? nullable = cost.AvgCost;
    Decimal num1 = 0M;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      nullable = cost.AvgCost;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = cost.AvgCost;
      Decimal num2 = nullable.GetValueOrDefault() * 0.01M;
      nullable = ii.MinGrossProfitPct;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num3 = num2 * valueOrDefault2;
      return valueOrDefault1 + num3;
    }
    nullable = cost.LastCost;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = cost.LastCost;
    Decimal num4 = nullable.GetValueOrDefault() * 0.01M;
    nullable = ii.MinGrossProfitPct;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal num5 = num4 * valueOrDefault4;
    return valueOrDefault3 + num5;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Precision = new int?(CommonSetupDecPl.PrcCst);
  }
}
