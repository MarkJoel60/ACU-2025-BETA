// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Shipment Discount Detail")]
[Serializable]
public class SOShipmentDiscountDetail : 
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
  string _ShipmentNbr;
  protected string _OrderType;
  protected string _OrderNbr;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected string _Type;
  protected Decimal? _DiscountableQty;
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
  [PXLineNbr(typeof (SOShipment))]
  public virtual ushort? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Discount", Enabled = true)]
  public virtual bool? SkipDiscount
  {
    get => this._SkipDiscount;
    set => this._SkipDiscount = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (SOShipment.shipmentNbr))]
  [PXParent(typeof (SOShipmentDiscountDetail.FK.Shipment))]
  [PXUIField(DisplayName = "ShipmentNbr")]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Discount ID")]
  [PXForeignReference(typeof (SOShipmentDiscountDetail.FK.DiscountSequence))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Sequence ID")]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBString(1, IsKey = true)]
  [PXDefault]
  [DiscountType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Discountable Qty.")]
  public virtual Decimal? DiscountableQty
  {
    get => this._DiscountableQty;
    set => this._DiscountableQty = value;
  }

  [Inventory(DisplayName = "Free Item")]
  [PXForeignReference(typeof (SOShipmentDiscountDetail.FK.FreeItem))]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Free Item Qty.")]
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
  [PXDefault(typeof (Search<PX.Objects.AR.DiscountSequence.description, Where<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<SOShipmentDiscountDetail.discountID>>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, Equal<Current<SOShipmentDiscountDetail.discountSequenceID>>>>>))]
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

  public Decimal? CuryDiscountableAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  public Decimal? CuryDiscountAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  public Decimal? DiscountPct
  {
    get => new Decimal?();
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.recordID, SOShipmentDiscountDetail.orderType, SOShipmentDiscountDetail.orderNbr, SOShipmentDiscountDetail.shipmentNbr, SOShipmentDiscountDetail.type>
  {
    public static SOShipmentDiscountDetail Find(
      PXGraph graph,
      int? recordID,
      string orderType,
      string orderNbr,
      string shipmentNbr,
      string type,
      PKFindOptions options = 0)
    {
      return (SOShipmentDiscountDetail) PrimaryKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.recordID, SOShipmentDiscountDetail.orderType, SOShipmentDiscountDetail.orderNbr, SOShipmentDiscountDetail.shipmentNbr, SOShipmentDiscountDetail.type>.FindBy(graph, (object) recordID, (object) orderType, (object) orderNbr, (object) shipmentNbr, (object) type, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.shipmentNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.orderType, SOShipmentDiscountDetail.orderNbr>
    {
    }

    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.freeItemID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.discountID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<PX.Objects.AR.DiscountSequence>.By<PX.Objects.AR.DiscountSequence.discountID, PX.Objects.AR.DiscountSequence.discountSequenceID>.ForeignKeyOf<SOShipmentDiscountDetail>.By<SOShipmentDiscountDetail.discountID, SOShipmentDiscountDetail.discountSequenceID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentDiscountDetail.recordID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentDiscountDetail.lineNbr>
  {
  }

  public abstract class skipDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentDiscountDetail.skipDiscount>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.shipmentNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.orderNbr>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.discountSequenceID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentDiscountDetail.type>
  {
  }

  public abstract class discountableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipmentDiscountDetail.discountableQty>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentDiscountDetail.freeItemID>
  {
  }

  public abstract class freeItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipmentDiscountDetail.freeItemQty>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipmentDiscountDetail.isManual>
  {
  }

  public abstract class isOrigDocDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentDiscountDetail.isOrigDocDiscount>
  {
  }

  public abstract class extDiscCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.extDiscCode>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.description>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOShipmentDiscountDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipmentDiscountDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentDiscountDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipmentDiscountDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentDiscountDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentDiscountDetail.lastModifiedDateTime>
  {
  }
}
