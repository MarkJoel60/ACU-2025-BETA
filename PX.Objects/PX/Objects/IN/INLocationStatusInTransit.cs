// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLocationStatusInTransit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class INLocationStatusInTransit : INLocationStatusByCostCenter
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    INLocationStatusInTransit>.By<INLocationStatusInTransit.inventoryID, INLocationStatusInTransit.subItemID, INLocationStatusInTransit.siteID, INLocationStatusInTransit.locationID, INLocationStatusInTransit.costCenterID>
  {
    public static INLocationStatusInTransit Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      int? costCenterID)
    {
      return (INLocationStatusInTransit) PrimaryKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.inventoryID, INLocationStatusInTransit.subItemID, INLocationStatusInTransit.siteID, INLocationStatusInTransit.locationID, INLocationStatusInTransit.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) costCenterID, (PKFindOptions) 0);
    }
  }

  public new static class FK
  {
    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.siteID>
    {
    }

    public class ItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INLocationStatusInTransit>.By<INLocationStatusInTransit.inventoryID, INLocationStatusInTransit.siteID>
    {
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusInTransit.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusInTransit.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLocationStatusInTransit.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusInTransit.locationID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLocationStatusInTransit.costCenterID>
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLocationStatusInTransit.active>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyOnHand>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyAvail>
  {
  }

  public new abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyNotAvail>
  {
  }

  public new abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyExpired>
  {
  }

  public new abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyHardAvail>
  {
  }

  public new abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyInTransit>
  {
  }

  public new abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyInTransitToSO>
  {
  }

  public new abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOPrepared>
  {
  }

  public new abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOOrders>
  {
  }

  public new abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOReceipts>
  {
  }

  public new abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOBackOrdered>
  {
  }

  public new abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOPrepared>
  {
  }

  public new abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOBooked>
  {
  }

  public new abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOShipped>
  {
  }

  public new abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOShipping>
  {
  }

  public new abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyINIssues>
  {
  }

  public new abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyINReceipts>
  {
  }

  public new abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyINAssemblyDemand>
  {
  }

  public new abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyINAssemblySupply>
  {
  }

  public new abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySOFixed>
  {
  }

  public new abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOFixedOrders>
  {
  }

  public new abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOFixedPrepared>
  {
  }

  public new abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPOFixedReceipts>
  {
  }

  public new abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtySODropShip>
  {
  }

  public new abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPODropShipOrders>
  {
  }

  public new abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPODropShipPrepared>
  {
  }

  public new abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLocationStatusInTransit.qtyPODropShipReceipts>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INLocationStatusInTransit.Tstamp>
  {
  }
}
