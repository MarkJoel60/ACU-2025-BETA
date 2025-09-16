// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCostHistByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Item Cost History by Period")]
[PXProjection(typeof (Select5<INItemCostHist, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<INItemCostHist.finPeriodID>>>, Aggregate<GroupBy<INItemCostHist.inventoryID, GroupBy<INItemCostHist.costSubItemID, GroupBy<INItemCostHist.costSiteID, GroupBy<INItemCostHist.accountID, GroupBy<INItemCostHist.subID, Max<INItemCostHist.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>))]
[Serializable]
public class INItemCostHistByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [StockItem(IsKey = true, BqlField = typeof (INItemCostHist.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INItemCostHist.costSubItemID))]
  [PXDefault]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [Site(IsKey = true, BqlField = typeof (INItemCostHist.costSiteID))]
  [PXDefault]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [Account(IsKey = true, BqlField = typeof (INItemCostHist.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, BqlField = typeof (INItemCostHist.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Site(true, BqlField = typeof (INItemCostHist.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INItemCostHist.finPeriodID))]
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

  public class PK : 
    PrimaryKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.inventoryID, INItemCostHistByPeriod.costSubItemID, INItemCostHistByPeriod.costSiteID, INItemCostHistByPeriod.accountID, INItemCostHistByPeriod.subID, INItemCostHistByPeriod.finPeriodID>
  {
    public static INItemCostHistByPeriod Find(
      PXGraph graph,
      int? inventoryID,
      int? costSubItemID,
      int? costSiteID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (INItemCostHistByPeriod) PrimaryKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.inventoryID, INItemCostHistByPeriod.costSubItemID, INItemCostHistByPeriod.costSiteID, INItemCostHistByPeriod.accountID, INItemCostHistByPeriod.subID, INItemCostHistByPeriod.finPeriodID>.FindBy(graph, (object) inventoryID, (object) costSubItemID, (object) costSiteID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.costSiteID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.subID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCostHistByPeriod>.By<INItemCostHistByPeriod.siteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistByPeriod.inventoryID>
  {
  }

  public abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCostHistByPeriod.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistByPeriod.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistByPeriod.subID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHistByPeriod.siteID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemCostHistByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemCostHistByPeriod.finPeriodID>
  {
  }
}
