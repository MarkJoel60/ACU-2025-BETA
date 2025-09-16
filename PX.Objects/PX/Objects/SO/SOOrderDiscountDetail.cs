// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Sales Order Discount Detail")]
[Serializable]
public class SOOrderDiscountDetail : 
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
  protected short? _ManualOrder;
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
  [PXUIField(DisplayName = "Line Nbr.")]
  [PXLineNbr(typeof (SOOrder))]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<SOOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<SOOrderDiscountDetail.discountID, IsNotNull>>))]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (SOOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (SOOrderDiscountDetail.FK.Order))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code")]
  [PXUIEnabled(typeof (Where<SOOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  [PXForeignReference(typeof (SOOrderDiscountDetail.FK.DiscountSequence))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  [PXUIEnabled(typeof (Where<SOOrderDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.DiscountSequence.discountSequenceID, Where<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<SOOrderDiscountDetail.discountID>>>>>))]
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

  [PXDBShort]
  [PXLineNbr(typeof (SOOrder))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual short? ManualOrder
  {
    get => this._ManualOrder;
    set => this._ManualOrder = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
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

  [PXDBCurrency(typeof (SOOrderDiscountDetail.curyInfoID), typeof (SOOrderDiscountDetail.discountableAmt))]
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

  [PXDBCurrency(typeof (SOOrderDiscountDetail.curyInfoID), typeof (SOOrderDiscountDetail.discountAmt))]
  [PXUIEnabled(typeof (Where<SOOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<SOOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscountAmt
  {
    get => this._CuryDiscountAmt;
    set => this._CuryDiscountAmt = value;
  }

  [PXDBDecimal(6)]
  [PXUIEnabled(typeof (Where<SOOrderDiscountDetail.type, Equal<DiscountType.DocumentDiscount>, Or<SOOrderDiscountDetail.type, Equal<DiscountType.ExternalDocumentDiscount>>>))]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountPct
  {
    get => this._DiscountPct;
    set => this._DiscountPct = value;
  }

  [Inventory(DisplayName = "Free Item", Enabled = false)]
  [PXForeignReference(typeof (SOOrderDiscountDetail.FK.FreeInventoryItem))]
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
  [PXDefault(typeof (Search<PX.Objects.AR.DiscountSequence.description, Where<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<SOOrderDiscountDetail.discountID>>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, Equal<Current<SOOrderDiscountDetail.discountSequenceID>>>>>))]
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
    PrimaryKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.orderType, SOOrderDiscountDetail.orderNbr, SOOrderDiscountDetail.recordID>
  {
    public static SOOrderDiscountDetail Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (SOOrderDiscountDetail) PrimaryKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.orderType, SOOrderDiscountDetail.orderNbr, SOOrderDiscountDetail.recordID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.orderType, SOOrderDiscountDetail.orderNbr>
    {
    }

    public class FreeInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.freeItemID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.discountID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<PX.Objects.AR.DiscountSequence>.By<PX.Objects.AR.DiscountSequence.discountID, PX.Objects.AR.DiscountSequence.discountSequenceID>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.discountID, SOOrderDiscountDetail.discountSequenceID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOOrderDiscountDetail>.By<SOOrderDiscountDetail.curyInfoID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderDiscountDetail.skipDiscount>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderDiscountDetail.orderNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderDiscountDetail.type>
  {
  }

  public abstract class manualOrder : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOOrderDiscountDetail.manualOrder>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOOrderDiscountDetail.curyInfoID>
  {
  }

  public abstract class discountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountableAmt>
  {
  }

  public abstract class curyDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.curyDiscountableAmt>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountableQty>
  {
  }

  public abstract class discountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountAmt>
  {
  }

  public abstract class curyDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.curyDiscountAmt>
  {
  }

  public abstract class discountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.discountPct>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.description>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrderDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderDiscountDetail.lastModifiedDateTime>
  {
  }
}
