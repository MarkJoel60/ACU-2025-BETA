// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.DiscountLineFields`13
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Mappers;

public class DiscountLineFields<SkipDiscField, CuryDiscAmtField, DiscPctField, DiscountIDField, DiscountSequenceIDField, DiscountsAppliedToLineField, ManualDiscField, ManualPriceField, LineTypeField, IsFreeField, CalculateDiscountsOnImportField, ExcludeFromAutomaticDiscountCalculationField, SkipLineDiscountsField>(
  PXCache cache,
  object row) : DiscountLineFields(cache, row)
  where SkipDiscField : IBqlField
  where CuryDiscAmtField : IBqlField
  where DiscPctField : IBqlField
  where DiscountIDField : IBqlField
  where DiscountSequenceIDField : IBqlField
  where DiscountsAppliedToLineField : IBqlField
  where ManualDiscField : IBqlField
  where ManualPriceField : IBqlField
  where LineTypeField : IBqlField
  where IsFreeField : IBqlField
  where CalculateDiscountsOnImportField : IBqlField
  where ExcludeFromAutomaticDiscountCalculationField : IBqlField
  where SkipLineDiscountsField : IBqlField
{
  public override Type GetField<T>()
  {
    if (typeof (T) == typeof (DiscountLineFields.skipDisc))
      return typeof (SkipDiscField);
    if (typeof (T) == typeof (DiscountLineFields.curyDiscAmt))
      return typeof (CuryDiscAmtField);
    if (typeof (T) == typeof (DiscountLineFields.discPct))
      return typeof (DiscPctField);
    if (typeof (T) == typeof (DiscountLineFields.discountID))
      return typeof (DiscountIDField);
    if (typeof (T) == typeof (DiscountLineFields.discountSequenceID))
      return typeof (DiscountSequenceIDField);
    if (typeof (T) == typeof (DiscountLineFields.discountsAppliedToLine))
      return typeof (DiscountsAppliedToLineField);
    if (typeof (T) == typeof (DiscountLineFields.manualDisc))
      return typeof (ManualDiscField);
    if (typeof (T) == typeof (DiscountLineFields.manualPrice))
      return typeof (ManualPriceField);
    if (typeof (T) == typeof (DiscountLineFields.lineType))
      return typeof (LineTypeField);
    if (typeof (T) == typeof (DiscountLineFields.isFree))
      return typeof (IsFreeField);
    if (typeof (T) == typeof (DiscountLineFields.calculateDiscountsOnImport))
      return typeof (CalculateDiscountsOnImportField);
    if (typeof (T) == typeof (DiscountLineFields.automaticDiscountsDisabled))
      return typeof (ExcludeFromAutomaticDiscountCalculationField);
    return typeof (T) == typeof (DiscountLineFields.skipLineDiscounts) ? typeof (SkipLineDiscountsField) : (Type) null;
  }

  public override bool SkipDisc
  {
    get => ((bool?) this.Cache.GetValue<SkipDiscField>(this.MappedLine)).GetValueOrDefault();
    set => this.Cache.SetValue<SkipDiscField>(this.MappedLine, (object) value);
  }

  public override Decimal? CuryDiscAmt
  {
    get => (Decimal?) this.Cache.GetValue<CuryDiscAmtField>(this.MappedLine);
    set => this.Cache.SetValue<CuryDiscAmtField>(this.MappedLine, (object) value);
  }

  public override Decimal? DiscPct
  {
    get => (Decimal?) this.Cache.GetValue<DiscPctField>(this.MappedLine);
    set => this.Cache.SetValue<DiscPctField>(this.MappedLine, (object) value);
  }

  public override string DiscountID
  {
    get => (string) this.Cache.GetValue<DiscountIDField>(this.MappedLine);
    set => this.Cache.SetValue<DiscountIDField>(this.MappedLine, (object) value);
  }

  public override string DiscountSequenceID
  {
    get => (string) this.Cache.GetValue<DiscountSequenceIDField>(this.MappedLine);
    set => this.Cache.SetValue<DiscountSequenceIDField>(this.MappedLine, (object) value);
  }

  public override ushort[] DiscountsAppliedToLine
  {
    get => (ushort[]) this.Cache.GetValue<DiscountsAppliedToLineField>(this.MappedLine);
    set => this.Cache.SetValue<DiscountsAppliedToLineField>(this.MappedLine, (object) value);
  }

  public override bool ManualDisc
  {
    get => ((bool?) this.Cache.GetValue<ManualDiscField>(this.MappedLine)).GetValueOrDefault();
    set => this.Cache.SetValue<ManualDiscField>(this.MappedLine, (object) value);
  }

  public override bool ManualPrice
  {
    get => ((bool?) this.Cache.GetValue<ManualPriceField>(this.MappedLine)).GetValueOrDefault();
    set => this.Cache.SetValue<ManualPriceField>(this.MappedLine, (object) value);
  }

  public override string LineType
  {
    get => (string) this.Cache.GetValue<LineTypeField>(this.MappedLine);
    set => this.Cache.SetValue<LineTypeField>(this.MappedLine, (object) value);
  }

  public override bool? IsFree
  {
    get
    {
      return new bool?(((bool?) this.Cache.GetValue<IsFreeField>(this.MappedLine)).GetValueOrDefault());
    }
    set => this.Cache.SetValue<IsFreeField>(this.MappedLine, (object) value);
  }

  public override bool? CalculateDiscountsOnImport
  {
    get
    {
      return new bool?(((bool?) this.Cache.GetValue<CalculateDiscountsOnImportField>(this.MappedLine)).GetValueOrDefault());
    }
    set => this.Cache.SetValue<CalculateDiscountsOnImportField>(this.MappedLine, (object) value);
  }

  public override bool? AutomaticDiscountsDisabled
  {
    get
    {
      return new bool?(((bool?) this.Cache.GetValue<ExcludeFromAutomaticDiscountCalculationField>(this.MappedLine)).GetValueOrDefault());
    }
    set
    {
      this.Cache.SetValue<ExcludeFromAutomaticDiscountCalculationField>(this.MappedLine, (object) value);
    }
  }

  public override bool? SkipLineDiscounts
  {
    get
    {
      return new bool?(((bool?) this.Cache.GetValue<SkipLineDiscountsField>(this.MappedLine)).GetValueOrDefault());
    }
    set => this.Cache.SetValue<SkipLineDiscountsField>(this.MappedLine, (object) value);
  }
}
