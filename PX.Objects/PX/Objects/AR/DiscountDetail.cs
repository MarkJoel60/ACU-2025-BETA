// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<DiscountSequenceDetail, LeftJoin<DiscountSequenceDetail2, On<DiscountSequenceDetail.discountID, Equal<DiscountSequenceDetail2.discountID>, And<DiscountSequenceDetail.discountSequenceID, Equal<DiscountSequenceDetail2.discountSequenceID>, And<DiscountSequenceDetail.lineNbr, Equal<DiscountSequenceDetail2.lineNbr>, And<DiscountSequenceDetail.discountDetailsID, NotEqual<DiscountSequenceDetail2.discountDetailsID>>>>>>, Where<DiscountSequenceDetail.isLast, Equal<False>>>), new Type[] {typeof (DiscountSequenceDetail), typeof (DiscountSequenceDetail2)})]
[PXCacheName("Discount Breakpoint")]
[Serializable]
public class DiscountDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DiscountDetailsID;
  protected int? _LineNbr;
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected bool? _IsActive;
  protected Decimal? _Amount;
  protected Decimal? _AmountTo;
  protected Decimal? _LastAmount;
  protected Decimal? _LastAmountTo;
  protected Decimal? _PendingAmount;
  protected Decimal? _Quantity;
  protected Decimal? _QuantityTo;
  protected Decimal? _LastQuantity;
  protected Decimal? _LastQuantityTo;
  protected Decimal? _PendingQuantity;
  protected Decimal? _Discount;
  protected Decimal? _LastDiscount;
  protected Decimal? _PendingDiscount;
  protected Decimal? _FreeItemQty;
  protected Decimal? _LastFreeItemQty;
  protected Decimal? _PendingFreeItemQty;
  protected DateTime? _StartDate;
  protected DateTime? _LastDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true, BqlField = typeof (DiscountSequenceDetail.discountDetailsID))]
  public virtual int? DiscountDetailsID
  {
    get => this._DiscountDetailsID;
    set => this._DiscountDetailsID = value;
  }

  [PXDBInt(BqlField = typeof (DiscountSequenceDetail.lineNbr))]
  [PXDefault(0)]
  [PXLineNbr(typeof (DiscountSequence.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (DiscountSequenceDetail.discountID))]
  [PXDBDefault(typeof (DiscountSequence.discountID))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (DiscountSequenceDetail.discountSequenceID))]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountDetail.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountDetail.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBBool(BqlField = typeof (DiscountSequenceDetail.isActive))]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBPriceCost(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.amount))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXDBDecimal(BqlField = typeof (DiscountSequenceDetail.amountTo))]
  public virtual Decimal? AmountTo
  {
    get => this._AmountTo;
    set => this._AmountTo = value;
  }

  [PXDBPriceCost(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail2.amount))]
  [PXUIField]
  public virtual Decimal? LastAmount
  {
    get => this._LastAmount;
    set => this._LastAmount = value;
  }

  [PXDBDecimal(BqlField = typeof (DiscountSequenceDetail2.amountTo))]
  public virtual Decimal? LastAmountTo
  {
    get => this._LastAmountTo;
    set => this._LastAmountTo = value;
  }

  [PXDBPriceCost(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.pendingAmount))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingAmount
  {
    get => this._PendingAmount;
    set => this._PendingAmount = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.quantity))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  [PXDBDecimal(BqlField = typeof (DiscountSequenceDetail.quantityTo))]
  public virtual Decimal? QuantityTo
  {
    get => this._QuantityTo;
    set => this._QuantityTo = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail2.quantity))]
  [PXUIField]
  public virtual Decimal? LastQuantity
  {
    get => this._LastQuantity;
    set => this._LastQuantity = value;
  }

  [PXDBDecimal(BqlField = typeof (DiscountSequenceDetail2.quantityTo))]
  public virtual Decimal? LastQuantityTo
  {
    get => this._LastQuantityTo;
    set => this._LastQuantityTo = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.pendingQuantity))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingQuantity
  {
    get => this._PendingQuantity;
    set => this._PendingQuantity = value;
  }

  [PXDBPriceCost(BqlField = typeof (DiscountSequenceDetail.discount))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Discount
  {
    get => this._Discount;
    set => this._Discount = value;
  }

  [PXDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPercent
  {
    get => this.Discount;
    set => this.Discount = value;
  }

  [PXDBPriceCost(BqlField = typeof (DiscountSequenceDetail2.discount))]
  [PXUIField]
  public virtual Decimal? LastDiscount
  {
    get => this._LastDiscount;
    set => this._LastDiscount = value;
  }

  [PXDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField]
  public virtual Decimal? LastDiscountPercent
  {
    get => this.LastDiscount;
    set => this.LastDiscount = value;
  }

  [PXDBPriceCost(BqlField = typeof (DiscountSequenceDetail.pendingDiscount))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingDiscount
  {
    get => this._PendingDiscount;
    set => this._PendingDiscount = value;
  }

  [PXDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingDiscountPercent
  {
    get => this.PendingDiscount;
    set => this.PendingDiscount = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.freeItemQty))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail2.freeItemQty))]
  [PXUIField]
  public virtual Decimal? LastFreeItemQty
  {
    get => this._LastFreeItemQty;
    set => this._LastFreeItemQty = value;
  }

  [PXDBQuantity(MinValue = 0.0, BqlField = typeof (DiscountSequenceDetail.pendingFreeItemQty))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingFreeItemQty
  {
    get => this._PendingFreeItemQty;
    set => this._PendingFreeItemQty = value;
  }

  [PXDBDate(BqlField = typeof (DiscountSequenceDetail.pendingDate))]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate(BqlField = typeof (DiscountSequenceDetail.lastDate))]
  [PXUIField]
  public virtual DateTime? LastDate
  {
    get => this._LastDate;
    set => this._LastDate = value;
  }

  [PXDBTimestamp(BqlField = typeof (DiscountSequenceDetail.Tstamp))]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID(BqlField = typeof (DiscountSequenceDetail.createdByID))]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (DiscountSequenceDetail.createdByScreenID))]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (DiscountSequenceDetail.createdDateTime))]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (DiscountSequenceDetail.lastModifiedByID))]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (DiscountSequenceDetail.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (DiscountSequenceDetail.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class discountDetailsID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DiscountDetail.discountDetailsID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountDetail.lineNbr>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountDetail.discountSequenceID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountDetail.isActive>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.amount>
  {
  }

  public abstract class amountTo : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.amountTo>
  {
  }

  public abstract class lastAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.lastAmount>
  {
  }

  public abstract class lastAmountTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastAmountTo>
  {
  }

  public abstract class pendingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.pendingAmount>
  {
  }

  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.quantity>
  {
  }

  public abstract class quantityTo : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.quantityTo>
  {
  }

  public abstract class lastQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastQuantity>
  {
  }

  public abstract class lastQuantityTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastQuantityTo>
  {
  }

  public abstract class pendingQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.pendingQuantity>
  {
  }

  public abstract class discount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.discount>
  {
  }

  public abstract class discountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.discountPercent>
  {
  }

  public abstract class lastDiscount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastDiscount>
  {
  }

  public abstract class lastDiscountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastDiscountPercent>
  {
  }

  public abstract class pendingDiscount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.pendingDiscount>
  {
  }

  public abstract class pendingDiscountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.pendingDiscountPercent>
  {
  }

  public abstract class freeItemQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountDetail.freeItemQty>
  {
  }

  public abstract class lastFreeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.lastFreeItemQty>
  {
  }

  public abstract class pendingFreeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountDetail.pendingFreeItemQty>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DiscountDetail.startDate>
  {
  }

  public abstract class lastDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DiscountDetail.lastDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountDetail.lastModifiedDateTime>
  {
  }
}
