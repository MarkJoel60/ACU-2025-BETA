// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistByLatestSDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <exclude />
[PXCacheName("IN Item Site History by Latest SDate")]
[PXProjection(typeof (Select4<INItemSiteHistDay, Where<INItemSiteHistDay.sDate, LessEqual<CurrentValue<InventoryTranHistEnqFilter.startDate>>>, Aggregate<GroupBy<INItemSiteHistDay.inventoryID, GroupBy<INItemSiteHistDay.subItemID, GroupBy<INItemSiteHistDay.siteID, GroupBy<INItemSiteHistDay.locationID, Max<INItemSiteHistDay.sDate>>>>>>>))]
[Serializable]
public class INItemSiteHistByLatestSDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected DateTime? _LastActivityDate;

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

  [Location(typeof (INItemSiteHistByLatestSDate.siteID), IsKey = true, BqlField = typeof (INItemSiteHistDay.locationID))]
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

  public abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLatestSDate.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLatestSDate.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByLatestSDate.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLatestSDate.locationID>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteHistByLatestSDate.lastActivityDate>
  {
  }
}
