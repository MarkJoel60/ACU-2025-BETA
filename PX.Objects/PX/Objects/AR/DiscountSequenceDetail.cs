// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountSequenceDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents one of the two discount breakpoint records created
/// for each <see cref="T:PX.Objects.AR.DiscountDetail" /> records. The record stores
/// (depending on the value of the <see cref="P:PX.Objects.AR.DiscountSequenceDetail.IsLast" /> flag) either
/// the current or the pending break quantity or amount, and either
/// the current or the pending discount percentage or amount.
/// The entities of this type cannot be edited directly, but are
/// aggregated into <see cref="T:PX.Objects.AR.DiscountDetail">discount breakpoint</see>
/// records on the Discounts (AR209500) form, which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXCacheName("Discount Sequence Detail")]
[Serializable]
public class DiscountSequenceDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DiscountDetailsID;
  protected int? _LineNbr;
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected bool? _IsLast;
  protected bool? _IsActive;
  protected Decimal? _Amount;
  protected Decimal? _AmountTo;
  protected Decimal? _PendingAmount;
  protected Decimal? _Quantity;
  protected Decimal? _QuantityTo;
  protected Decimal? _PendingQuantity;
  protected Decimal? _Discount;
  protected Decimal? _PendingDiscount;
  protected Decimal? _FreeItemQty;
  protected Decimal? _PendingFreeItemQty;
  protected DateTime? _PendingDate;
  protected DateTime? _LastDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? DiscountDetailsID
  {
    get => this._DiscountDetailsID;
    set => this._DiscountDetailsID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXLineNbr(typeof (DiscountSequence.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (DiscountSequence.discountID))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountSequenceDetail.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountSequenceDetail.discountID>>>>>), LeaveChildren = true)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBBool(IsKey = true)]
  [PXDefault(false)]
  public virtual bool? IsLast
  {
    get => this._IsLast;
    set => this._IsLast = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBPriceCost(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXDBDecimal]
  public virtual Decimal? AmountTo
  {
    get => this._AmountTo;
    set => this._AmountTo = value;
  }

  [PXDBPriceCost(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? PendingAmount
  {
    get => this._PendingAmount;
    set => this._PendingAmount = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  [PXDBDecimal]
  public virtual Decimal? QuantityTo
  {
    get => this._QuantityTo;
    set => this._QuantityTo = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? PendingQuantity
  {
    get => this._PendingQuantity;
    set => this._PendingQuantity = value;
  }

  [PXDBPriceCost]
  [PXUIField]
  public virtual Decimal? Discount
  {
    get => this._Discount;
    set => this._Discount = value;
  }

  [PXDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField]
  public virtual Decimal? DiscountPercent
  {
    get => this.Discount;
    set => this.Discount = value;
  }

  [PXDBPriceCost]
  [PXUIField]
  public virtual Decimal? PendingDiscount
  {
    get => this._PendingDiscount;
    set => this._PendingDiscount = value;
  }

  [PXDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField]
  public virtual Decimal? PendingDiscountPercent
  {
    get => this.PendingDiscount;
    set => this.PendingDiscount = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? PendingFreeItemQty
  {
    get => this._PendingFreeItemQty;
    set => this._PendingFreeItemQty = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PendingDate
  {
    get => this._PendingDate;
    set => this._PendingDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastDate
  {
    get => this._LastDate;
    set => this._LastDate = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<DiscountSequenceDetail>.By<DiscountSequenceDetail.discountDetailsID>
  {
    public static DiscountSequenceDetail Find(
      PXGraph graph,
      int? discountDetailsID,
      PKFindOptions options = 0)
    {
      return (DiscountSequenceDetail) PrimaryKeyOf<DiscountSequenceDetail>.By<DiscountSequenceDetail.discountDetailsID>.FindBy(graph, (object) discountDetailsID, options);
    }
  }

  public abstract class discountDetailsID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DiscountSequenceDetail.discountDetailsID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountSequenceDetail.lineNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail.discountSequenceID>
  {
  }

  public abstract class isLast : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequenceDetail.isLast>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequenceDetail.isActive>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountSequenceDetail.amount>
  {
  }

  public abstract class amountTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.amountTo>
  {
  }

  public abstract class pendingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingAmount>
  {
  }

  public abstract class quantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.quantity>
  {
  }

  public abstract class quantityTo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.quantityTo>
  {
  }

  public abstract class pendingQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingQuantity>
  {
  }

  public abstract class discount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.discount>
  {
  }

  public abstract class discountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.discountPercent>
  {
  }

  public abstract class pendingDiscount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingDiscount>
  {
  }

  public abstract class pendingDiscountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingDiscountPercent>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.freeItemQty>
  {
  }

  public abstract class pendingFreeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingFreeItemQty>
  {
  }

  public abstract class pendingDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail.pendingDate>
  {
  }

  public abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail.lastDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DiscountSequenceDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountSequenceDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountSequenceDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequenceDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequenceDetail.lastModifiedDateTime>
  {
  }
}
