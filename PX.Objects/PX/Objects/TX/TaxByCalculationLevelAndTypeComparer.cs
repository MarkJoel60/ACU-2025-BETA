// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxByCalculationLevelAndTypeComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.TX;

/// <summary>
/// A comparer of taxes by tax calculation level and by tax type for taxes with the same calculation level
/// </summary>
public class TaxByCalculationLevelAndTypeComparer : TaxByCalculationLevelComparer
{
  public static readonly TaxByCalculationLevelAndTypeComparer Instance = new TaxByCalculationLevelAndTypeComparer();

  protected TaxByCalculationLevelAndTypeComparer()
  {
  }

  /// <summary>
  /// Tax comparison by tax type. Compares tax types via <see cref="M:System.String.Compare(System.String,System.String)" />.
  /// </summary>
  /// <param name="taxX">The tax x.</param>
  /// <param name="taxY">The tax y.</param>
  /// <returns />
  protected override int CompareTaxesByTaxTypes(Tax taxX, Tax taxY)
  {
    return string.Compare(taxX.TaxType, taxY.TaxType);
  }
}
