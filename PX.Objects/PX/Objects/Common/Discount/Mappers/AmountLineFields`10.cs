// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.AmountLineFields`10
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public class AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>(
  PXCache cache,
  object row) : 
  AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>(cache, row)
  where QuantityField : IBqlField
  where CuryUnitPriceField : IBqlField
  where CuryExtPriceField : IBqlField
  where CuryLineAmountField : IBqlField
  where UOMField : IBqlField
  where OrigGroupDiscountRateField : IBqlField
  where OrigDocumentDiscountRateField : IBqlField
  where GroupDiscountRateField : IBqlField
  where DocumentDiscountRateField : IBqlField
  where FreezeManualDiscField : IBqlField
{
  public override Type GetField<T>()
  {
    return typeof (T) == typeof (AmountLineFields.freezeManualDisc) ? typeof (FreezeManualDiscField) : base.GetField<T>();
  }

  public override bool? FreezeManualDisc
  {
    get => (bool?) this.Cache.GetValue<FreezeManualDiscField>(this.MappedLine);
    set => this.Cache.SetValue<FreezeManualDiscField>(this.MappedLine, (object) value);
  }
}
