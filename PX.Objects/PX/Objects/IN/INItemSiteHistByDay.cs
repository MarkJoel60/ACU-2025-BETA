// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistByDay
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("Do not use this DAC to calculate beginning qty - it causes performance issues due to join with DateInfo. Use INItemSiteHistByLatestSDate instead.")]
[PXCacheName("IN Item Site History by Day")]
[PXProjection(typeof (Select5<INItemSiteHistDay, InnerJoin<DateInfo, On<DateInfo.date, GreaterEqual<INItemSiteHistDay.sDate>>>, Aggregate<GroupBy<INItemSiteHistDay.inventoryID, GroupBy<INItemSiteHistDay.subItemID, GroupBy<INItemSiteHistDay.siteID, GroupBy<INItemSiteHistDay.locationID, Max<INItemSiteHistDay.sDate, GroupBy<DateInfo.date>>>>>>>>))]
[Serializable]
public class INItemSiteHistByDay : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected DateTime? _LastActivityDate;
  protected DateTime? _date;

  [StockItem(IsKey = true, BqlField = typeof (INItemSiteHistDay.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INItemSiteHistDay.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(IsKey = true, BqlField = typeof (INItemSiteHistDay.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INItemSiteHistByDay.siteID), IsKey = true, BqlField = typeof (INItemSiteHistDay.locationID))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate(BqlField = typeof (INItemSiteHistDay.sDate))]
  public virtual DateTime? LastActivityDate
  {
    get => this._LastActivityDate;
    set => this._LastActivityDate = value;
  }

  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? Date
  {
    get => this._date;
    set => this._date = value;
  }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByDay.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByDay.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByDay.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByDay.locationID>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteHistByDay.lastActivityDate>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSiteHistByDay.date>
  {
  }
}
