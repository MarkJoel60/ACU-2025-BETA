// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountSequenceDetail2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An alias DAC for <see cref="T:PX.Objects.AR.DiscountSequenceDetail" />.
/// </summary>
[Serializable]
public class DiscountSequenceDetail2 : DiscountSequenceDetail
{
  public new abstract class discountDetailsID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    DiscountSequenceDetail2.discountDetailsID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountSequenceDetail2.lineNbr>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail2.discountID>
  {
  }

  public new abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail2.discountSequenceID>
  {
  }

  public new abstract class isActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscountSequenceDetail2.isActive>
  {
  }

  public new abstract class isLast : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequenceDetail2.isLast>
  {
  }

  public new abstract class amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.amount>
  {
  }

  public new abstract class amountTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.amountTo>
  {
  }

  public new abstract class pendingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingAmount>
  {
  }

  public new abstract class quantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.quantity>
  {
  }

  public new abstract class quantityTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.quantityTo>
  {
  }

  public new abstract class pendingQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingQuantity>
  {
  }

  public new abstract class discount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.discount>
  {
  }

  public new abstract class discountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.discountPercent>
  {
  }

  public new abstract class pendingDiscount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingDiscount>
  {
  }

  public new abstract class pendingDiscountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingDiscountPercent>
  {
  }

  public new abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.freeItemQty>
  {
  }

  public new abstract class pendingFreeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingFreeItemQty>
  {
  }

  public new abstract class pendingDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail2.pendingDate>
  {
  }

  public new abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail2.lastDate>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    DiscountSequenceDetail2.Tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountSequenceDetail2.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail2.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail2.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountSequenceDetail2.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail2.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail2.lastModifiedDateTime>
  {
  }
}
