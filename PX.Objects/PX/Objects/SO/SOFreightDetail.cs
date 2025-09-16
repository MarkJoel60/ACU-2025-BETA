// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOFreightDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Freight Detail")]
[Serializable]
public class SOFreightDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFreightBase
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected string _OrderType;
  protected string _OrderNbr;
  protected string _ShipmentType;
  protected string _ShipmentNbr;
  protected string _ShipTermsID;
  protected string _ShipZoneID;
  protected string _ShipVia;
  protected Decimal? _Weight;
  protected Decimal? _Volume;
  protected long? _CuryInfoID;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryFreightCost;
  protected Decimal? _FreightCost;
  protected Decimal? _CuryFreightAmt;
  protected Decimal? _FreightAmt;
  protected Decimal? _CuryPremiumFreightAmt;
  protected Decimal? _PremiumFreightAmt;
  protected Decimal? _CuryTotalFreightAmt;
  protected Decimal? _TotalFreightAmt;
  protected string _TaxCategoryID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (PX.Objects.AR.ARInvoice.docType))]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXParent(typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<SOFreightDetail.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<SOFreightDetail.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOFreightDetail.orderType>>>>))]
  [PXParent(typeof (SOFreightDetail.FK.Order), LeaveChildren = true)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDefault]
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Shipment Type", Enabled = false)]
  [SOShipmentType.List]
  public virtual string ShipmentType
  {
    get => this._ShipmentType;
    set => this._ShipmentType = value;
  }

  [PXDefault]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.Navigate.SOOrderShipment.shipmentNbr, Where<PX.Objects.SO.Navigate.SOOrderShipment.orderType, Equal<Current<SOFreightDetail.orderType>>, And<PX.Objects.SO.Navigate.SOOrderShipment.orderNbr, Equal<Current<SOFreightDetail.orderNbr>>, And<PX.Objects.SO.Navigate.SOOrderShipment.shipmentType, Equal<Current<SOFreightDetail.shipmentType>>>>>>))]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CS.ShipTerms.shipTermsID), CacheGlobal = true)]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone ID", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), DescriptionField = typeof (PX.Objects.CS.ShippingZone.description), CacheGlobal = true)]
  public virtual string ShipZoneID
  {
    get => this._ShipZoneID;
    set => this._ShipZoneID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Weight", Enabled = false)]
  public virtual Decimal? Weight
  {
    get => this._Weight;
    set => this._Weight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Volume", Enabled = false)]
  public virtual Decimal? Volume
  {
    get => this._Volume;
    set => this._Volume = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (SOFreightDetail.curyInfoID), typeof (SOFreightDetail.lineTotal))]
  [PXUIField(DisplayName = "Line Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDBCurrency(typeof (SOFreightDetail.curyInfoID), typeof (SOFreightDetail.freightCost))]
  [PXFormula(null, typeof (SumCalc<PX.Objects.AR.ARInvoice.curyFreightCost>))]
  [PXUIField(DisplayName = "Freight Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightCost
  {
    get => this._CuryFreightCost;
    set => this._CuryFreightCost = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? FreightCost
  {
    get => this._FreightCost;
    set => this._FreightCost = value;
  }

  [PXDBCurrency(typeof (SOFreightDetail.curyInfoID), typeof (SOFreightDetail.freightAmt))]
  [PXFormula(null, typeof (SumCalc<PX.Objects.AR.ARInvoice.curyFreightAmt>))]
  [PXUIField(DisplayName = "Freight Price")]
  [PXUIVerify]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightAmt
  {
    get => this._CuryFreightAmt;
    set => this._CuryFreightAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  public virtual Decimal? FreightAmt
  {
    get => this._FreightAmt;
    set => this._FreightAmt = value;
  }

  [PXDBCurrency(typeof (SOFreightDetail.curyInfoID), typeof (SOFreightDetail.premiumFreightAmt))]
  [PXFormula(null, typeof (SumCalc<PX.Objects.AR.ARInvoice.curyPremiumFreightAmt>))]
  [PXUIField(DisplayName = "Premium Freight Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPremiumFreightAmt
  {
    get => this._CuryPremiumFreightAmt;
    set => this._CuryPremiumFreightAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  public virtual Decimal? PremiumFreightAmt
  {
    get => this._PremiumFreightAmt;
    set => this._PremiumFreightAmt = value;
  }

  [PXDBCurrency(typeof (SOFreightDetail.curyInfoID), typeof (SOFreightDetail.totalFreightAmt))]
  [PXFormula(typeof (Add<SOFreightDetail.curyPremiumFreightAmt, SOFreightDetail.curyFreightAmt>), typeof (SumCalc<SOOrder.curyBilledFreightTot>))]
  [PXUIField(DisplayName = "Total Freight Price", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTotalFreightAmt
  {
    get => this._CuryTotalFreightAmt;
    set => this._CuryTotalFreightAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  public virtual Decimal? TotalFreightAmt
  {
    get => this._TotalFreightAmt;
    set => this._TotalFreightAmt = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [Account]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (SOFreightDetail.accountID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (PX.Objects.AR.ARInvoice.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMAccountTask.taskID, Where<PMAccountTask.projectID, Equal<Current<SOFreightDetail.projectID>>, And<PMAccountTask.accountID, Equal<Current<SOFreightDetail.accountID>>>>>))]
  [SOFreightDetailTask(typeof (SOFreightDetail.projectID), typeof (SOFreightDetail.curyTotalFreightAmt), DisplayName = "Project Task")]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  public Decimal? OrderWeight => this.Weight;

  public Decimal? PackageWeight => this.Weight;

  public Decimal? OrderVolume => this.Volume;

  public class PK : 
    PrimaryKeyOf<SOFreightDetail>.By<SOFreightDetail.docType, SOFreightDetail.refNbr, SOFreightDetail.orderType, SOFreightDetail.orderNbr, SOFreightDetail.shipmentType, SOFreightDetail.shipmentNbr>
  {
    public static SOFreightDetail Find(
      PXGraph graph,
      string docType,
      string refNbr,
      string orderType,
      string orderNbr,
      string shipmentType,
      string shipmentNbr,
      PKFindOptions options = 0)
    {
      return (SOFreightDetail) PrimaryKeyOf<SOFreightDetail>.By<SOFreightDetail.docType, SOFreightDetail.refNbr, SOFreightDetail.orderType, SOFreightDetail.orderNbr, SOFreightDetail.shipmentType, SOFreightDetail.shipmentNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) orderType, (object) orderNbr, (object) shipmentType, (object) shipmentNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentType, SOShipment.shipmentNbr>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.shipmentType, SOFreightDetail.shipmentNbr>
    {
    }

    public class OrderShipment : 
      PrimaryKeyOf<SOOrderShipment>.By<SOOrderShipment.shipmentType, SOOrderShipment.shipmentNbr, SOOrderShipment.orderType, SOOrderShipment.orderNbr>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.shipmentType, SOFreightDetail.shipmentNbr, SOFreightDetail.orderType, SOFreightDetail.orderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.orderType, SOFreightDetail.orderNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.docType, SOFreightDetail.refNbr>
    {
    }

    public class ShipTerms : 
      PrimaryKeyOf<PX.Objects.CS.ShipTerms>.By<PX.Objects.CS.ShipTerms.shipTermsID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.shipTermsID>
    {
    }

    public class ShipZone : 
      PrimaryKeyOf<PX.Objects.CS.ShippingZone>.By<PX.Objects.CS.ShippingZone.zoneID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.shipZoneID>
    {
    }

    public class Carrier : 
      PrimaryKeyOf<PX.Objects.CS.Carrier>.By<PX.Objects.CS.Carrier.carrierID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.shipVia>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.taxCategoryID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.subID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<SOFreightDetail>.By<SOFreightDetail.projectID, SOFreightDetail.taskID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.refNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.orderNbr>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOFreightDetail.shipmentType>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.shipmentNbr>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.shipTermsID>
  {
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.shipZoneID>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOFreightDetail.shipVia>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOFreightDetail.weight>
  {
  }

  public abstract class volume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOFreightDetail.volume>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOFreightDetail.curyInfoID>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOFreightDetail.lineTotal>
  {
  }

  public abstract class curyFreightCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.curyFreightCost>
  {
  }

  public abstract class freightCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.freightCost>
  {
  }

  public abstract class curyFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.curyFreightAmt>
  {
  }

  public abstract class freightAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOFreightDetail.freightAmt>
  {
  }

  public abstract class curyPremiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.curyPremiumFreightAmt>
  {
  }

  public abstract class premiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.premiumFreightAmt>
  {
  }

  public abstract class curyTotalFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.curyTotalFreightAmt>
  {
  }

  public abstract class totalFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOFreightDetail.totalFreightAmt>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOFreightDetail.taxCategoryID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOFreightDetail.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOFreightDetail.subID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOFreightDetail.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOFreightDetail.taskID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOFreightDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOFreightDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOFreightDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOFreightDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOFreightDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOFreightDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOFreightDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOFreightDetail.lastModifiedDateTime>
  {
  }
}
