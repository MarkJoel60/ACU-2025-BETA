// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistByPeriod
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

[PXCacheName("IN Item Site History by Period")]
[PXProjection(typeof (Select5<INItemSiteHist, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<INItemSiteHist.finPeriodID>>>, Aggregate<GroupBy<INItemSiteHist.inventoryID, GroupBy<INItemSiteHist.subItemID, GroupBy<INItemSiteHist.siteID, GroupBy<INItemSiteHist.locationID, Max<INItemSiteHist.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[Serializable]
public class INItemSiteHistByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [StockItem(IsKey = true, BqlField = typeof (INItemSiteHist.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INItemSiteHist.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(IsKey = true, BqlField = typeof (INItemSiteHist.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INItemSiteHistByPeriod.siteID), IsKey = true, BqlField = typeof (INItemSiteHist.locationID))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INItemSiteHist.finPeriodID))]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByPeriod.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByPeriod.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByPeriod.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByPeriod.locationID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteHistByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteHistByPeriod.finPeriodID>
  {
  }
}
