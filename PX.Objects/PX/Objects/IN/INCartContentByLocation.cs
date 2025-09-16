// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCartContentByLocation
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

[PXCacheName("IN Cart Content by Location")]
[PXProjection(typeof (SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INCartSplit.siteID>, GroupBy<INCartSplit.fromLocationID>, GroupBy<INCartSplit.inventoryID>, GroupBy<INCartSplit.subItemID>, Sum<INCartSplit.baseQty>>), Persistent = false)]
public class INCartContentByLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true, BqlTable = typeof (INCartSplit))]
  public int? SiteID { get; set; }

  [Location(typeof (INCartContentByLocation.siteID), IsKey = true, BqlField = typeof (INCartSplit.fromLocationID))]
  public virtual int? LocationID { get; set; }

  [StockItem(IsKey = true, BqlTable = typeof (INCartSplit))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (INCartContentByLocation.inventoryID), IsKey = true, BqlTable = typeof (INCartSplit))]
  public virtual int? SubItemID { get; set; }

  [PXDBDecimal(6, BqlTable = typeof (INCartSplit))]
  public virtual Decimal? BaseQty { get; set; }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<
      #nullable disable
      INSite>.By<INSite.siteID>.ForeignKeyOf<INCartContentByLocation>.By<INCartContentByLocation.siteID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INCartContentByLocation>.By<INCartContentByLocation.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCartContentByLocation>.By<INCartContentByLocation.subItemID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INCartContentByLocation>.By<INCartContentByLocation.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<INCartContentByLocation>.By<INCartContentByLocation.inventoryID, INCartContentByLocation.subItemID, INCartContentByLocation.siteID, INCartContentByLocation.locationID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLocation.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLocation.locationID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCartContentByLocation.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCartContentByLocation.subItemID>
  {
  }

  public abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCartContentByLocation.baseQty>
  {
  }
}
