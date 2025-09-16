// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentSplitToCartSplitLink
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
namespace PX.Objects.SO;

[PXCacheName]
public class SOShipmentSplitToCartSplitLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ShipmentLineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (SOShipmentSplitToCartSplitLink.FK.ShipmentSplitLine))]
  public virtual int? ShipmentSplitLineNbr { get; set; }

  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (SOShipmentSplitToCartSplitLink.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (SOShipmentSplitToCartSplitLink.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (SOShipmentSplitToCartSplitLink.FK.CartSplit))]
  public virtual int? CartSplitLineNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.shipmentNbr, SOShipmentSplitToCartSplitLink.shipmentLineNbr, SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr, SOShipmentSplitToCartSplitLink.siteID, SOShipmentSplitToCartSplitLink.cartID, SOShipmentSplitToCartSplitLink.cartSplitLineNbr>
  {
    public static SOShipmentSplitToCartSplitLink Find(
      PXGraph graph,
      string shipmentNbr,
      int? shipmentLineNbr,
      int? shipmentSplitLineNbr,
      int? siteID,
      int? cartID,
      int? cartSplitLineNbr,
      PKFindOptions options = 0)
    {
      return (SOShipmentSplitToCartSplitLink) PrimaryKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.shipmentNbr, SOShipmentSplitToCartSplitLink.shipmentLineNbr, SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr, SOShipmentSplitToCartSplitLink.siteID, SOShipmentSplitToCartSplitLink.cartID, SOShipmentSplitToCartSplitLink.cartSplitLineNbr>.FindBy(graph, (object) shipmentNbr, (object) shipmentLineNbr, (object) shipmentSplitLineNbr, (object) siteID, (object) cartID, (object) cartSplitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.shipmentNbr>
    {
    }

    public class ShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.shipmentNbr, SOShipmentSplitToCartSplitLink.shipmentLineNbr>
    {
    }

    public class ShipmentSplitLine : 
      PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.shipmentNbr, SOShipmentSplitToCartSplitLink.shipmentLineNbr, SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.siteID, SOShipmentSplitToCartSplitLink.cartID>
    {
    }

    public class CartSplit : 
      PrimaryKeyOf<INCartSplit>.By<INCartSplit.siteID, INCartSplit.cartID, INCartSplit.splitLineNbr>.ForeignKeyOf<SOShipmentSplitToCartSplitLink>.By<SOShipmentSplitToCartSplitLink.siteID, SOShipmentSplitToCartSplitLink.cartID, SOShipmentSplitToCartSplitLink.cartSplitLineNbr>
    {
    }
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.shipmentNbr>
  {
  }

  public abstract class shipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.shipmentLineNbr>
  {
  }

  public abstract class shipmentSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentSplitToCartSplitLink.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentSplitToCartSplitLink.cartID>
  {
  }

  public abstract class cartSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.cartSplitLineNbr>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipmentSplitToCartSplitLink.qty>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOShipmentSplitToCartSplitLink.Tstamp>
  {
  }
}
