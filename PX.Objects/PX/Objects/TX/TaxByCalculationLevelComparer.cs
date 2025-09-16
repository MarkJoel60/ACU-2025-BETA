// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxByCalculationLevelComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

/// <summary>A comparer of taxes by tax calculation level</summary>
public class TaxByCalculationLevelComparer : IComparer<Tax>
{
  public static readonly TaxByCalculationLevelComparer Instance = new TaxByCalculationLevelComparer();

  protected TaxByCalculationLevelComparer()
  {
  }

  public int Compare(Tax taxX, Tax taxY)
  {
    if (taxX == null && taxY == null)
      return 0;
    if (taxX == null)
      return -1;
    return taxY == null ? 1 : this.CompareTaxesWithPerUnitTaxSupport(taxX, taxY);
  }

  protected virtual int CompareTaxesWithPerUnitTaxSupport(Tax taxX, Tax taxY)
  {
    int num1 = this.CompareTaxesForPerUnitOrdering(taxX, taxY);
    if (num1 != 0)
      return num1;
    int num2 = this.CompareTaxesByCalculationLevels(taxX, taxY);
    return num2 == 0 ? this.CompareTaxesWithTheSameCalculationLevel(taxX, taxY) : num2;
  }

  /// <summary>
  /// Tax comparison for per unit taxes ordering. Places Per Unit taxes first, consider other tax types to be equal.
  /// </summary>
  /// <param name="taxX">The tax x.</param>
  /// <param name="taxY">The tax y.</param>
  /// <returns />
  protected virtual int CompareTaxesForPerUnitOrdering(Tax taxX, Tax taxY)
  {
    bool flag1 = taxX.TaxType == "Q";
    bool flag2 = taxY.TaxType == "Q";
    if (flag1 & flag2)
      return 0;
    if (flag1)
      return -1;
    return flag2 ? 1 : 0;
  }

  protected virtual int CompareTaxesByCalculationLevels(Tax taxX, Tax taxY)
  {
    return string.Compare(taxX.TaxCalcLevel, taxY.TaxCalcLevel);
  }

  protected virtual int CompareTaxesWithTheSameCalculationLevel(Tax taxX, Tax taxY)
  {
    return this.CompareTaxesByTaxTypes(taxX, taxY);
  }

  /// <summary>
  /// Tax comparison by tax type. Default implementation considers taxes of all tax types to be equal.
  /// </summary>
  /// <param name="taxX">The tax x.</param>
  /// <param name="taxY">The tax y.</param>
  /// <returns />
  protected virtual int CompareTaxesByTaxTypes(Tax taxX, Tax taxY) => 0;
}
