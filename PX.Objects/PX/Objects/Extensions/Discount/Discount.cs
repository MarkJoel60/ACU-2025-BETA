// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.Discount.Discount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.Extensions.Discount;

/// <summary>A mapped cache extension that provides information about the discount.</summary>
public class Discount : PXMappedCacheExtension, IDiscountDetail
{
  /// <exclude />
  protected int? _RecordID;
  protected ushort? _LineNbr;
  /// <exclude />
  protected bool? _SkipDiscount;
  /// <exclude />
  protected 
  #nullable disable
  string _DiscountID;
  /// <exclude />
  protected string _DiscountSequenceID;
  /// <exclude />
  protected string _Type;
  /// <exclude />
  protected long? _CuryInfoID;
  /// <exclude />
  protected Decimal? _DiscountableAmt;
  /// <exclude />
  protected Decimal? _CuryDiscountableAmt;
  /// <exclude />
  protected Decimal? _DiscountableQty;
  /// <exclude />
  protected Decimal? _DiscountAmt;
  /// <exclude />
  protected Decimal? _CuryDiscountAmt;
  /// <exclude />
  protected Decimal? _DiscountPct;
  /// <exclude />
  protected int? _FreeItemID;
  /// <exclude />
  protected Decimal? _FreeItemQty;
  /// <exclude />
  protected bool? _IsManual;
  protected bool? _IsOrigDocDiscount;
  protected string _ExtDiscCode;
  protected string _Description;
  /// <exclude />
  protected byte[] _tstamp;
  /// <exclude />
  protected Guid? _CreatedByID;
  /// <exclude />
  protected string _CreatedByScreenID;
  /// <exclude />
  protected System.DateTime? _CreatedDateTime;
  /// <exclude />
  protected Guid? _LastModifiedByID;
  /// <exclude />
  protected string _LastModifiedByScreenID;
  /// <exclude />
  protected System.DateTime? _LastModifiedDateTime;

  /// <summary>The number of the detail line to which this discount applies.</summary>
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBUShort]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>If set to true, cancels the Group- and Document-level discounts for the document.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  /// <summary>The identifier (code) of the discount applied to the document.</summary>
  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code")]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  /// <summary>The ID of the sequence defined for the discount.</summary>
  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<PX.Objects.Extensions.Discount.Discount.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  /// <summary>The type of discount whose sequence was applied to the document (<i>Group</i>, or <i>Document</i>).</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo" /> object associated with the discount.</summary>
  [PXDBLong]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The amount used as a base for discount calculation if the discount is based on the amount. The amount is in the base currency of the company,</summary>
  [PXDBDecimal(4)]
  public virtual Decimal? DiscountableAmt
  {
    get => this._DiscountableAmt;
    set => this._DiscountableAmt = value;
  }

  /// <summary>The amount used as a base for discount calculation if the discount is based on the amount. The amount is in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  [PXDBCurrency(typeof (PX.Objects.Extensions.Discount.Discount.curyInfoID), typeof (PX.Objects.Extensions.Discount.Discount.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public virtual Decimal? CuryDiscountableAmt
  {
    get => this._CuryDiscountableAmt;
    set => this._CuryDiscountableAmt = value;
  }

  /// <summary>The quantity used as a base for discount calculation if the discount is based on the item quantity.</summary>
  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
  public virtual Decimal? DiscountableQty
  {
    get => this._DiscountableQty;
    set => this._DiscountableQty = value;
  }

  /// <summary>The amount of the discount, in the base currency of the company.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountAmt
  {
    get => this._DiscountAmt;
    set => this._DiscountAmt = value;
  }

  /// <summary>The amount of the discount, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  [PXDBCurrency(typeof (PX.Objects.Extensions.Discount.Discount.curyInfoID), typeof (PX.Objects.Extensions.Discount.Discount.discountAmt))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  /// <summary>The discount percent if by definition the discount is calculated as a percentage.</summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  /// <summary>The identifier of the free item, if one is specified by the discount applied to the document.</summary>
  [Inventory(DisplayName = "Free Item", Enabled = false)]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  /// <summary>The quantity of the free item to be added as a discount.</summary>
  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that the discount applicable to this detail row was changed manually.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? IsOrigDocDiscount
  {
    get => this._IsOrigDocDiscount;
    set => this._IsOrigDocDiscount = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Discount Code")]
  public virtual string ExtDiscCode
  {
    get => this._ExtDiscCode;
    set => this._ExtDiscCode = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<DiscountSequence.description, Where<DiscountSequence.discountID, Equal<Current<PX.Objects.Extensions.Discount.Discount.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<PX.Objects.Extensions.Discount.Discount.discountSequenceID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  /// <exclude />
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  /// <exclude />
  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <exclude />
  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.lineNbr>
  {
  }

  /// <exclude />
  public abstract class skipDiscount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.skipDiscount>
  {
  }

  /// <exclude />
  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.discountID>
  {
  }

  /// <exclude />
  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.discountSequenceID>
  {
  }

  /// <exclude />
  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.type>
  {
  }

  /// <exclude />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.curyInfoID>
  {
  }

  /// <exclude />
  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.discountableAmt>
  {
  }

  /// <exclude />
  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.curyDiscountableAmt>
  {
  }

  /// <exclude />
  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.discountableQty>
  {
  }

  /// <exclude />
  public abstract class discountAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.discountAmt>
  {
  }

  /// <exclude />
  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.curyDiscountAmt>
  {
  }

  /// <exclude />
  public abstract class discountPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.discountPct>
  {
  }

  /// <exclude />
  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.freeItemID>
  {
  }

  /// <exclude />
  public abstract class freeItemQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.freeItemQty>
  {
  }

  /// <exclude />
  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.isManual>
  {
  }

  public abstract class isOrigDocDiscount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.extDiscCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.description>
  {
  }

  /// <exclude />
  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.Tstamp>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PX.Objects.Extensions.Discount.Discount.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PX.Objects.Extensions.Discount.Discount.lastModifiedDateTime>
  {
  }
}
