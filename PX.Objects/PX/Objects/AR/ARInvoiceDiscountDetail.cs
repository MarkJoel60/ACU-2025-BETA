// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A document-level or group-level discount that has been applied
/// to an accounts receivable invoice or memo. The records of this type
/// can be edited on the Invoices and Memos (AR301000) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph.
/// </summary>
/// <remarks>
/// Line-level discounts are specified in the document line itself
/// in <see cref="P:PX.Objects.AR.ARTran.DiscPct" />.
/// </remarks>
[PXCacheName("AR Invoice Discount Detail")]
[Serializable]
public class ARInvoiceDiscountDetail : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IDiscountDetail
{
  protected int? _RecordID;
  protected ushort? _LineNbr;
  protected bool? _SkipDiscount;
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected string _OrderType;
  protected string _OrderNbr;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected string _Type;
  protected long? _CuryInfoID;
  protected Decimal? _DiscountableAmt;
  protected Decimal? _CuryDiscountableAmt;
  protected Decimal? _DiscountableQty;
  protected Decimal? _DiscountAmt;
  protected Decimal? _CuryDiscountAmt;
  protected Decimal? _DiscountPct;
  protected int? _FreeItemID;
  protected Decimal? _FreeItemQty;
  protected bool? _IsManual;
  protected bool? _IsOrigDocDiscount;
  protected string _ExtDiscCode;
  protected string _Description;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBUShort]
  [PXLineNbr(typeof (ARRegister))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<ARInvoiceDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount")]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARRegister.docType))]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARInvoiceDiscountDetail.docType>>, And<ARRegister.refNbr, Equal<Current<ARInvoiceDiscountDetail.refNbr>>>>>))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<ARInvoiceDiscountDetail.orderType>>>>), ValidateValue = false)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.orderNbr, IsNull, And<ARInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Required = false)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.orderNbr, IsNull, And<ARInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<ARInvoiceDiscountDetail.discountID>>>>>))]
  [PXForeignReference(typeof (ARInvoiceDiscountDetail.FK.DiscountSequence))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARInvoice.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? DiscountableAmt
  {
    get => this._DiscountableAmt;
    set => this._DiscountableAmt = value;
  }

  [PXDBCurrency(typeof (ARInvoiceDiscountDetail.curyInfoID), typeof (ARInvoiceDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public virtual Decimal? CuryDiscountableAmt
  {
    get => this._CuryDiscountableAmt;
    set => this._CuryDiscountableAmt = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Discountable Qty.", Enabled = false)]
  public virtual Decimal? DiscountableQty
  {
    get => this._DiscountableQty;
    set => this._DiscountableQty = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountAmt
  {
    get => this._DiscountAmt;
    set => this._DiscountAmt = value;
  }

  [PXDBCurrency(typeof (ARInvoiceDiscountDetail.curyInfoID), typeof (ARInvoiceDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.orderNbr, IsNull, And<Where<ARInvoiceDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<ARInvoiceDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.orderNbr, IsNull, And<Where<ARInvoiceDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<ARInvoiceDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (Field<ARInvoiceDiscountDetail.freeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.", Enabled = false)]
  public virtual Decimal? FreeItemQty
  {
    get => this._FreeItemQty;
    set => this._FreeItemQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Discount", Enabled = false)]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<ARInvoiceDiscountDetail.orderNbr, IsNotNull>, True>, False>))]
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
  [PXDefault(typeof (Search<DiscountSequence.description, Where<DiscountSequence.discountID, Equal<Current<ARInvoiceDiscountDetail.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<ARInvoiceDiscountDetail.discountSequenceID>>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBCurrency(typeof (ARInvoiceDiscountDetail.curyInfoID), typeof (ARInvoiceDiscountDetail.retainedDiscountAmt))]
  [PXUIField(DisplayName = "Retained Discount", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedDiscountAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedDiscountAmt { get; set; }

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

  public class PK : 
    PrimaryKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.docType, ARInvoiceDiscountDetail.refNbr, ARInvoiceDiscountDetail.recordID>
  {
    public static ARInvoiceDiscountDetail Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (ARInvoiceDiscountDetail) PrimaryKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.docType, ARInvoiceDiscountDetail.refNbr, ARInvoiceDiscountDetail.recordID>.FindBy(graph, (object) docType, (object) refNbr, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.discountID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.discountID, ARInvoiceDiscountDetail.discountSequenceID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.curyInfoID>
    {
    }

    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ARInvoiceDiscountDetail>.By<ARInvoiceDiscountDetail.freeItemID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.skipDiscount>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.refNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.orderNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.type>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.description>
  {
  }

  public abstract class curyRetainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.curyRetainedDiscountAmt>
  {
  }

  public abstract class retainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.retainedDiscountAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARInvoiceDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoiceDiscountDetail.lastModifiedDateTime>
  {
  }

  public class ARInvoiceDiscountDetailComparer : IEqualityComparer<ARInvoiceDiscountDetail>
  {
    public bool Equals(
      ARInvoiceDiscountDetail discountDetail1,
      ARInvoiceDiscountDetail discountDetail2)
    {
      return discountDetail1.DiscountID == discountDetail2.DiscountID && discountDetail1.DiscountSequenceID == discountDetail2.DiscountSequenceID && discountDetail1.Type == discountDetail2.Type && discountDetail1.DocType == discountDetail2.DocType && discountDetail1.RefNbr == discountDetail2.RefNbr && discountDetail1.OrderType == discountDetail2.OrderType && discountDetail1.OrderNbr == discountDetail2.OrderNbr;
    }

    public int GetHashCode(ARInvoiceDiscountDetail discountDetail)
    {
      int num1 = 17 * 11;
      int? hashCode1 = discountDetail.DiscountID?.GetHashCode();
      int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode2 = discountDetail.DiscountSequenceID?.GetHashCode();
      int num3 = (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode3 = discountDetail.Type?.GetHashCode();
      int num4 = (hashCode3.HasValue ? new int?(num3 + hashCode3.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode4 = discountDetail.DocType?.GetHashCode();
      int num5 = (hashCode4.HasValue ? new int?(num4 + hashCode4.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode5 = discountDetail.RefNbr?.GetHashCode();
      int num6 = (hashCode5.HasValue ? new int?(num5 + hashCode5.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode6 = discountDetail.OrderType?.GetHashCode();
      int num7 = (hashCode6.HasValue ? new int?(num6 + hashCode6.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode7 = discountDetail.OrderNbr?.GetHashCode();
      return (hashCode7.HasValue ? new int?(num7 + hashCode7.GetValueOrDefault()) : new int?()).GetValueOrDefault();
    }
  }

  public class ARInvoiceDiscountDetailComparerNoID : IEqualityComparer<ARInvoiceDiscountDetail>
  {
    public bool Equals(
      ARInvoiceDiscountDetail discountDetail1,
      ARInvoiceDiscountDetail discountDetail2)
    {
      return discountDetail1.Type == discountDetail2.Type && discountDetail1.DocType == discountDetail2.DocType && discountDetail1.RefNbr == discountDetail2.RefNbr && discountDetail1.OrderType == discountDetail2.OrderType && discountDetail1.OrderNbr == discountDetail2.OrderNbr;
    }

    public int GetHashCode(ARInvoiceDiscountDetail discountDetail)
    {
      int num1 = 17 * 11;
      int? hashCode1 = discountDetail.Type?.GetHashCode();
      int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode2 = discountDetail.DocType?.GetHashCode();
      int num3 = (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode3 = discountDetail.RefNbr?.GetHashCode();
      int num4 = (hashCode3.HasValue ? new int?(num3 + hashCode3.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode4 = discountDetail.OrderType?.GetHashCode();
      int num5 = (hashCode4.HasValue ? new int?(num4 + hashCode4.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode5 = discountDetail.OrderNbr?.GetHashCode();
      return (hashCode5.HasValue ? new int?(num5 + hashCode5.GetValueOrDefault()) : new int?()).GetValueOrDefault();
    }
  }
}
