// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Order Warehouse")]
[Serializable]
public class SOOrderSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _SiteID;
  protected int? _LineCntr;
  protected int? _OpenLineCntr;
  protected int? _ShipmentCntr;
  protected int? _OpenShipmentCntr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (SOOrder.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (SOOrderSite.FK.Order))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [Site(DisplayName = "Warehouse ID", IsKey = true, DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXDefault]
  [PXForeignReference(typeof (SOOrderSite.FK.Site))]
  [PXFormula(null, typeof (CountCalc<SOOrder.siteCntr>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenLineCntr
  {
    get => this._OpenLineCntr;
    set => this._OpenLineCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ShipmentCntr
  {
    get => this._ShipmentCntr;
    set => this._ShipmentCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUnboundFormula(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderSite.openShipmentCntr, Equal<int0>>>>>.And<BqlOperand<SOOrderSite.openLineCntr, IBqlInt>.IsGreater<int0>>, int1, int0>), typeof (SumCalc<SOOrder.openSiteCntr>), ValidateAggregateCalculation = true)]
  public virtual int? OpenShipmentCntr
  {
    get => this._OpenShipmentCntr;
    set => this._OpenShipmentCntr = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<SOOrderSite>.By<SOOrderSite.orderType, SOOrderSite.orderNbr, SOOrderSite.siteID>
  {
    public static SOOrderSite Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (SOOrderSite) PrimaryKeyOf<SOOrderSite>.By<SOOrderSite.orderType, SOOrderSite.orderNbr, SOOrderSite.siteID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrderSite>.By<SOOrderSite.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOOrderSite>.By<SOOrderSite.orderType, SOOrderSite.orderNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrderSite>.By<SOOrderSite.siteID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderSite.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderSite.orderNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSite.siteID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSite.lineCntr>
  {
  }

  public abstract class openLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSite.openLineCntr>
  {
  }

  public abstract class shipmentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSite.shipmentCntr>
  {
  }

  public abstract class openShipmentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSite.openShipmentCntr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderSite.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSite.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderSite.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderSite.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSite.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderSite.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrderSite.Tstamp>
  {
  }
}
