// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.RetainedAmountLineFields`11
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

/// <summary>
/// A specialized for retainage version of the <see cref="T:PX.Objects.Common.Discount.Mappers.AmountLineFields" /> class
/// with an additional CuryRetainageAmtField generic parameter.
/// Discount fields should not be recalculated after retainage fields changing
/// because they have a higher priority.
/// </summary>
public class RetainedAmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, CuryRetainageAmtField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>(
  PXCache cache,
  object row) : 
  AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>(cache, row)
  where QuantityField : IBqlField
  where CuryUnitPriceField : IBqlField
  where CuryExtPriceField : IBqlField
  where CuryLineAmountField : IBqlField
  where CuryRetainageAmtField : IBqlField
  where UOMField : IBqlField
  where OrigGroupDiscountRateField : IBqlField
  where OrigDocumentDiscountRateField : IBqlField
  where GroupDiscountRateField : IBqlField
  where DocumentDiscountRateField : IBqlField
  where FreezeManualDiscField : IBqlField
{
  public override Decimal? CuryLineAmount
  {
    get
    {
      Decimal? curyLineAmount = base.CuryLineAmount;
      Decimal num = (Decimal) (this.Cache.GetValue<CuryRetainageAmtField>(this.MappedLine) ?? (object) 0M);
      return !curyLineAmount.HasValue ? new Decimal?() : new Decimal?(curyLineAmount.GetValueOrDefault() + num);
    }
    set => base.CuryLineAmount = value;
  }
}
