// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCartContentByLotSerial
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Cart Content by Lot/Serial Nbr.")]
[PXProjection(typeof (SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INCartSplit.siteID>, GroupBy<INCartSplit.fromLocationID>, GroupBy<INCartSplit.inventoryID>, GroupBy<INCartSplit.subItemID>, GroupBy<INCartSplit.lotSerialNbr>, Sum<INCartSplit.baseQty>>), Persistent = false)]
public class INCartContentByLotSerial : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, BqlTable = typeof (INCartSplit))]
  public int? SiteID { get; set; }

  [Location(typeof (INCartContentByLotSerial.siteID), IsKey = true, BqlField = typeof (INCartSplit.fromLocationID))]
  public virtual int? LocationID { get; set; }

  [StockItem(IsKey = true, BqlTable = typeof (INCartSplit))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (INCartContentByLotSerial.inventoryID), IsKey = true, BqlTable = typeof (INCartSplit))]
  public virtual int? SubItemID { get; set; }

  [PX.Objects.IN.LotSerialNbr(IsKey = true, BqlTable = typeof (INCartSplit))]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (INCartSplit))]
  public virtual Decimal? BaseQty { get; set; }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.siteID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.subItemID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.inventoryID, INCartContentByLotSerial.subItemID, INCartContentByLotSerial.siteID, INCartContentByLotSerial.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<INCartContentByLotSerial>.By<INCartContentByLotSerial.inventoryID, INCartContentByLotSerial.subItemID, INCartContentByLotSerial.siteID, INCartContentByLotSerial.locationID, INCartContentByLotSerial.lotSerialNbr>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLotSerial.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLotSerial.locationID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCartContentByLotSerial.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLotSerial.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCartContentByLotSerial.lotSerialNbr>
  {
  }

  public abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCartContentByLotSerial.baseQty>
  {
  }
}
