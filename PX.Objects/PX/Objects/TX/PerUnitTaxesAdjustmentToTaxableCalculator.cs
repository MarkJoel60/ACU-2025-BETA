// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.PerUnitTaxesAdjustmentToTaxableCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

/// <summary>A per unit taxes adjustment to taxable calculator.</summary>
public class PerUnitTaxesAdjustmentToTaxableCalculator
{
  public static readonly PerUnitTaxesAdjustmentToTaxableCalculator Instance = new PerUnitTaxesAdjustmentToTaxableCalculator();

  protected PerUnitTaxesAdjustmentToTaxableCalculator()
  {
  }

  public virtual Decimal GetPerUnitTaxAmountForTaxableAdjustmentCalculation(
    Tax taxForTaxableAdustment,
    PXCache taxDetailCache,
    object row,
    PXCache rowCache,
    string curyTaxAmtFieldName,
    Func<List<object>> perUnitTaxSelector)
  {
    ExceptionExtensions.ThrowOnNull<Tax>(taxForTaxableAdustment, nameof (taxForTaxableAdustment), (string) null);
    ExceptionExtensions.ThrowOnNull<PXCache>(taxDetailCache, nameof (taxDetailCache), (string) null);
    ExceptionExtensions.ThrowOnNull<object>(row, nameof (row), (string) null);
    ExceptionExtensions.ThrowOnNull<PXCache>(rowCache, nameof (rowCache), (string) null);
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(curyTaxAmtFieldName, nameof (curyTaxAmtFieldName), (string) null);
    ExceptionExtensions.ThrowOnNull<Func<List<object>>>(perUnitTaxSelector, nameof (perUnitTaxSelector), (string) null);
    if (taxForTaxableAdustment.TaxType == "Q")
      return 0M;
    Type bqlField = taxDetailCache.GetBqlField(curyTaxAmtFieldName);
    List<object> allPerUnitTaxes = perUnitTaxSelector != null ? perUnitTaxSelector() : (List<object>) null;
    if (allPerUnitTaxes == null || allPerUnitTaxes.Count == 0)
      return 0M;
    (List<object> objectList1, List<object> objectList2) = this.GetNonExcludedPerUnitTaxesByCalculationLevel(allPerUnitTaxes);
    if (objectList1.Count == 0 && objectList2.Count == 0 || objectList1.Count != 0 && (row is ARTran arTran ? arTran.TranType : (string) null) == "PPI")
      return 0M;
    switch (taxForTaxableAdustment.TaxCalcLevel)
    {
      case "0":
        if (objectList2.Count > 0)
        {
          PXTrace.WriteInformation("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.");
          throw new PXSetPropertyException("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.", (PXErrorLevel) 4);
        }
        return this.SumTaxAmountsWithReverseAdjustment(taxDetailCache.Graph, (IEnumerable<object>) objectList1, bqlField);
      case "1":
        IEnumerable<object> perUnitTaxes = objectList1.Concat<object>((IEnumerable<object>) objectList2);
        return this.SumTaxAmountsWithReverseAdjustment(taxDetailCache.Graph, perUnitTaxes, bqlField);
      case "2":
        return this.SumTaxAmountsWithReverseAdjustment(taxDetailCache.Graph, (IEnumerable<object>) objectList1, bqlField);
      default:
        return 0M;
    }
  }

  private (List<object> PerUnitInclusiveTaxes, List<object> PerUnitLevel1Taxes) GetNonExcludedPerUnitTaxesByCalculationLevel(
    List<object> allPerUnitTaxes)
  {
    List<object> objectList1 = new List<object>(allPerUnitTaxes.Count);
    List<object> objectList2 = new List<object>(allPerUnitTaxes.Count);
    foreach (object allPerUnitTax in allPerUnitTaxes)
    {
      Tax tax = PXResult.Unwrap<Tax>(allPerUnitTax);
      bool? calcLevel2Exclude = tax.TaxCalcLevel2Exclude;
      bool flag = false;
      if (calcLevel2Exclude.GetValueOrDefault() == flag & calcLevel2Exclude.HasValue)
      {
        switch (tax.TaxCalcLevel)
        {
          case "0":
            objectList2.Add(allPerUnitTax);
            continue;
          case "1":
            objectList1.Add(allPerUnitTax);
            continue;
          default:
            continue;
        }
      }
    }
    return (objectList2, objectList1);
  }

  private Decimal SumTaxAmountsWithReverseAdjustment(
    PXGraph graph,
    IEnumerable<object> perUnitTaxes,
    Type taxAmountField)
  {
    Decimal num1 = 0.0M;
    Type itemType = BqlCommand.GetItemType(taxAmountField);
    PXCache cach = graph.Caches[itemType];
    foreach (PXResult perUnitTax in perUnitTaxes)
    {
      Decimal? nullable = cach.GetValue(perUnitTax[itemType], taxAmountField.Name) as Decimal?;
      Decimal num2 = ((Tax) perUnitTax[typeof (Tax)]).ReverseTax.GetValueOrDefault() ? -1M : 1M;
      num1 += nullable.GetValueOrDefault() * num2;
    }
    return num1;
  }
}
