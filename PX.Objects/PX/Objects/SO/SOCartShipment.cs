// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCartShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOCartShipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, Visible = false)]
  [PXParent(typeof (SOCartShipment.FK.Site))]
  public int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INCart.cartID, Where<INCart.active, Equal<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  [PXParent(typeof (SOCartShipment.FK.Cart))]
  public int? CartID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXParent(typeof (SOCartShipment.FK.Shipment))]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<SOCartShipment>.By<SOCartShipment.siteID, SOCartShipment.cartID>
  {
    public static SOCartShipment Find(
      PXGraph graph,
      int? siteID,
      int? cartID,
      PKFindOptions options = 0)
    {
      return (SOCartShipment) PrimaryKeyOf<SOCartShipment>.By<SOCartShipment.siteID, SOCartShipment.cartID>.FindBy(graph, (object) siteID, (object) cartID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOCartShipment>.By<SOCartShipment.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<SOCartShipment>.By<SOCartShipment.siteID, SOCartShipment.cartID>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOCartShipment>.By<SOCartShipment.shipmentNbr>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOCartShipment.siteID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOCartShipment.cartID>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOCartShipment.shipmentNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOCartShipment.Tstamp>
  {
  }
}
