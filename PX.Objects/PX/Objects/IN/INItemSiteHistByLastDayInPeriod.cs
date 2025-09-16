// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistByLastDayInPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Item Site History by Last Day In Period")]
[PXProjection(typeof (Select5<INItemSiteHistDay, InnerJoin<MasterFinPeriod, On<INItemSiteHistDay.sDate, Less<MasterFinPeriod.endDate>, And<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>>>>, Aggregate<GroupBy<INItemSiteHistDay.inventoryID, GroupBy<INItemSiteHistDay.subItemID, GroupBy<INItemSiteHistDay.siteID, GroupBy<INItemSiteHistDay.locationID, Max<INItemSiteHistDay.sDate, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[Serializable]
public class INItemSiteHistByLastDayInPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [StockItem(IsKey = true, BqlField = typeof (INItemSiteHistDay.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true, BqlField = typeof (INItemSiteHistDay.subItemID))]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INItemSiteHistDay.siteID))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [Location(typeof (INItemSiteHistByLastDayInPeriod.siteID), IsKey = true, BqlField = typeof (INItemSiteHistDay.locationID))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [PXDBDate(BqlField = typeof (INItemSiteHistDay.sDate))]
  public virtual DateTime? LastActivityDate { get; set; }

  [PXDBString(6, IsKey = true, IsFixed = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  [PXUIField]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLastDayInPeriod.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLastDayInPeriod.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByLastDayInPeriod.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByLastDayInPeriod.locationID>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteHistByLastDayInPeriod.lastActivityDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteHistByLastDayInPeriod.finPeriodID>
  {
  }
}
