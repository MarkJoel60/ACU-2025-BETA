// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.AmountLineFields`9
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public class AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>(
  PXCache cache,
  object row) : AmountLineFields(cache, row)
  where QuantityField : IBqlField
  where CuryUnitPriceField : IBqlField
  where CuryExtPriceField : IBqlField
  where CuryLineAmountField : IBqlField
  where UOMField : IBqlField
  where OrigGroupDiscountRateField : IBqlField
  where OrigDocumentDiscountRateField : IBqlField
  where GroupDiscountRateField : IBqlField
  where DocumentDiscountRateField : IBqlField
{
  public override Type GetField<T>()
  {
    if (typeof (T) == typeof (AmountLineFields.quantity))
      return typeof (QuantityField);
    if (typeof (T) == typeof (AmountLineFields.curyUnitPrice))
      return typeof (CuryUnitPriceField);
    if (typeof (T) == typeof (AmountLineFields.curyExtPrice))
      return typeof (CuryExtPriceField);
    if (typeof (T) == typeof (AmountLineFields.curyLineAmount))
      return typeof (CuryLineAmountField);
    if (typeof (T) == typeof (AmountLineFields.uOM))
      return typeof (UOMField);
    if (typeof (T) == typeof (AmountLineFields.origGroupDiscountRate))
      return typeof (GroupDiscountRateField);
    if (typeof (T) == typeof (AmountLineFields.origDocumentDiscountRate))
      return typeof (DocumentDiscountRateField);
    if (typeof (T) == typeof (AmountLineFields.groupDiscountRate))
      return typeof (GroupDiscountRateField);
    return typeof (T) == typeof (AmountLineFields.documentDiscountRate) ? typeof (DocumentDiscountRateField) : (Type) null;
  }

  public override Decimal? Quantity
  {
    get => (Decimal?) this.Cache.GetValue<QuantityField>(this.MappedLine);
    set => this.Cache.SetValue<QuantityField>(this.MappedLine, (object) value);
  }

  public override Decimal? CuryUnitPrice
  {
    get => (Decimal?) this.Cache.GetValue<CuryUnitPriceField>(this.MappedLine);
    set => this.Cache.SetValue<CuryUnitPriceField>(this.MappedLine, (object) value);
  }

  public override Decimal? CuryExtPrice
  {
    get => (Decimal?) this.Cache.GetValue<CuryExtPriceField>(this.MappedLine);
    set => this.Cache.SetValue<CuryExtPriceField>(this.MappedLine, (object) value);
  }

  public override Decimal? CuryLineAmount
  {
    get => (Decimal?) this.Cache.GetValue<CuryLineAmountField>(this.MappedLine);
    set => this.Cache.SetValue<CuryLineAmountField>(this.MappedLine, (object) value);
  }

  public override string UOM
  {
    get => (string) this.Cache.GetValue<UOMField>(this.MappedLine);
    set => this.Cache.SetValue<UOMField>(this.MappedLine, (object) value);
  }

  public override Decimal? OrigGroupDiscountRate
  {
    get => (Decimal?) this.Cache.GetValue<OrigGroupDiscountRateField>(this.MappedLine);
    set => this.Cache.SetValue<OrigGroupDiscountRateField>(this.MappedLine, (object) value);
  }

  public override Decimal? OrigDocumentDiscountRate
  {
    get => (Decimal?) this.Cache.GetValue<OrigDocumentDiscountRateField>(this.MappedLine);
    set => this.Cache.SetValue<OrigDocumentDiscountRateField>(this.MappedLine, (object) value);
  }

  public override Decimal? GroupDiscountRate
  {
    get => (Decimal?) this.Cache.GetValue<GroupDiscountRateField>(this.MappedLine);
    set => this.Cache.SetValue<GroupDiscountRateField>(this.MappedLine, (object) value);
  }

  public override Decimal? DocumentDiscountRate
  {
    get => (Decimal?) this.Cache.GetValue<DocumentDiscountRateField>(this.MappedLine);
    set => this.Cache.SetValue<DocumentDiscountRateField>(this.MappedLine, (object) value);
  }
}
