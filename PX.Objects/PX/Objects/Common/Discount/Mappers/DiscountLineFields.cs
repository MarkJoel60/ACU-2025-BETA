// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Mappers.DiscountLineFields
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.Discount.Mappers;

public abstract class DiscountLineFields(
#nullable disable
PXCache cache, object row) : DiscountedLineMapperBase(cache, row)
{
  public virtual bool SkipDisc { get; set; }

  public virtual Decimal? CuryDiscAmt { get; set; }

  public virtual Decimal? DiscPct { get; set; }

  public virtual string DiscountID { get; set; }

  public virtual string DiscountSequenceID { get; set; }

  public virtual ushort[] DiscountsAppliedToLine { get; set; }

  public virtual bool ManualDisc { get; set; }

  public virtual bool ManualPrice { get; set; }

  public virtual string LineType { get; set; }

  public virtual bool? IsFree { get; set; }

  public virtual bool? CalculateDiscountsOnImport { get; set; }

  public virtual bool? AutomaticDiscountsDisabled { get; set; }

  public virtual bool? SkipLineDiscounts { get; set; }

  public static DiscountLineFields GetMapFor<TLine>(TLine line, PXCache cache) where TLine : class, IBqlTable
  {
    return (DiscountLineFields) new DiscountLineFields<DiscountLineFields.skipDisc, DiscountLineFields.curyDiscAmt, DiscountLineFields.discPct, DiscountLineFields.discountID, DiscountLineFields.discountSequenceID, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts>(cache, (object) line);
  }

  public abstract class skipDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountLineFields.skipDisc>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountLineFields.curyDiscAmt>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountLineFields.discPct>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountLineFields.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountLineFields.discountSequenceID>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    DiscountLineFields.discountsAppliedToLine>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountLineFields.manualDisc>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountLineFields.manualPrice>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountLineFields.lineType>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountLineFields.isFree>
  {
  }

  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscountLineFields.calculateDiscountsOnImport>
  {
  }

  public abstract class automaticDiscountsDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscountLineFields.automaticDiscountsDisabled>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscountLineFields.skipLineDiscounts>
  {
  }
}
