// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Order Discount Detail")]
[Serializable]
public class POOrderDiscountDetail : 
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
  string _OrderType;
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
  [PXLineNbr(typeof (POOrder))]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<POOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<POOrderDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POOrder.orderNbr))]
  [PXParent(typeof (POOrderDiscountDetail.FK.Order))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<POOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Discount Code")]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<POOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXSelector(typeof (Search<VendorDiscountSequence.discountSequenceID, Where<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.discountID, Equal<Current<POOrderDiscountDetail.discountID>>>>>))]
  [PXForeignReference(typeof (POOrderDiscountDetail.FK.DiscountSequence))]
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
  [CurrencyInfo(typeof (POOrder.curyInfoID))]
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

  [PXDBCurrency(typeof (POOrderDiscountDetail.curyInfoID), typeof (POOrderDiscountDetail.discountableAmt))]
  [PXUIField(DisplayName = "Discountable Amt.", Enabled = false)]
  public virtual Decimal? CuryDiscountableAmt
  {
    get => this._CuryDiscountableAmt;
    set => this._CuryDiscountableAmt = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
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

  [PXDBCurrency(typeof (POOrderDiscountDetail.curyInfoID), typeof (POOrderDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<POOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<POOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  [PXDBCurrency(typeof (POOrderDiscountDetail.curyInfoID), typeof (POOrderDiscountDetail.retainedDiscountAmt))]
  [PXUIField(DisplayName = "Retained Discount", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedDiscountAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedDiscountAmt { get; set; }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<POOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<POOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (POOrderDiscountDetail.FK.FreeInventoryItem))]
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
  [PXDefault(typeof (Search<VendorDiscountSequence.description, Where<VendorDiscountSequence.discountID, Equal<Current<POOrderDiscountDetail.discountID>>, And<VendorDiscountSequence.discountSequenceID, Equal<Current<POOrderDiscountDetail.discountSequenceID>>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
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

  public class PK : 
    PrimaryKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.recordID, POOrderDiscountDetail.orderType, POOrderDiscountDetail.orderNbr>
  {
    public static POOrderDiscountDetail Find(
      PXGraph graph,
      int? recordID,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (POOrderDiscountDetail) PrimaryKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.recordID, POOrderDiscountDetail.orderType, POOrderDiscountDetail.orderNbr>.FindBy(graph, (object) recordID, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.orderType, POOrderDiscountDetail.orderNbr>
    {
    }

    public class FreeInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.freeItemID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.discountID, POOrderDiscountDetail.discountSequenceID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POOrderDiscountDetail>.By<POOrderDiscountDetail.curyInfoID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderDiscountDetail.skipDiscount>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderDiscountDetail.orderNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderDiscountDetail.type>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POOrderDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class curyRetainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.curyRetainedDiscountAmt>
  {
  }

  public abstract class retainedDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.retainedDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrderDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.description>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POOrderDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrderDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrderDiscountDetail.lastModifiedDateTime>
  {
  }
}
