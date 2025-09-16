// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteReplenishment
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

[PXCacheName("SubItem Replenishment Info")]
[Serializable]
public class INItemSiteReplenishment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _SubItemID;
  protected Decimal? _SafetyStock;
  protected Decimal? _MinQty;
  protected Decimal? _MaxQty;
  protected Decimal? _TransferERQ;
  protected 
  #nullable disable
  string _ItemStatus;
  protected Decimal? _SafetyStockSuggested;
  protected Decimal? _MinQtySuggested;
  protected Decimal? _MaxQtySuggested;
  protected Decimal? _DemandPerDayAverage;
  protected Decimal? _DemandPerDayMSE;
  protected Decimal? _DemandPerDayMAD;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID")]
  [PXParent(typeof (INItemSiteReplenishment.FK.InventoryItem))]
  [PXDefault(typeof (INItemSite.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(IsKey = true)]
  [PXDefault(typeof (INItemSite.siteID))]
  [PXParent(typeof (INItemSiteReplenishment.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SubItem(typeof (INItemSiteReplenishment.inventoryID), DisplayName = "Subitem", IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemSite, Where<INItemSite.inventoryID, Equal<Current<INItemSiteReplenishment.inventoryID>>, And<INItemSite.siteID, Equal<Current<INItemSiteReplenishment.siteID>>>>>), SourceField = typeof (INItemSite.safetyStock))]
  public virtual Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemSite, Where<INItemSite.inventoryID, Equal<Current<INItemSiteReplenishment.inventoryID>>, And<INItemSite.siteID, Equal<Current<INItemSiteReplenishment.siteID>>>>>), SourceField = typeof (INItemSite.minQty))]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemSite, Where<INItemSite.inventoryID, Equal<Current<INItemSiteReplenishment.inventoryID>>, And<INItemSite.siteID, Equal<Current<INItemSiteReplenishment.siteID>>>>>), SourceField = typeof (INItemSite.maxQty))]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemSite, Where<INItemSite.inventoryID, Equal<Current<INItemSiteReplenishment.inventoryID>>, And<INItemSite.siteID, Equal<Current<INItemSiteReplenishment.siteID>>>>>))]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("AC")]
  [PXUIField]
  [InventoryItemStatus.SubItemList]
  public virtual string ItemStatus
  {
    get => this._ItemStatus;
    set => this._ItemStatus = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock Suggested", Enabled = false)]
  public virtual Decimal? SafetyStockSuggested
  {
    get => this._SafetyStockSuggested;
    set => this._SafetyStockSuggested = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point Suggested", Enabled = false)]
  public virtual Decimal? MinQtySuggested
  {
    get => this._MinQtySuggested;
    set => this._MinQtySuggested = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty Suggested", Enabled = false)]
  public virtual Decimal? MaxQtySuggested
  {
    get => this._MaxQtySuggested;
    set => this._MaxQtySuggested = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast", Enabled = false)]
  public virtual Decimal? DemandPerDayAverage
  {
    get => this._DemandPerDayAverage;
    set => this._DemandPerDayAverage = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast Error(MSE)", Enabled = false)]
  public virtual Decimal? DemandPerDayMSE
  {
    get => this._DemandPerDayMSE;
    set => this._DemandPerDayMSE = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Forecast Error MAD", Enabled = false)]
  public virtual Decimal? DemandPerDayMAD
  {
    get => this._DemandPerDayMAD;
    set => this._DemandPerDayMAD = value;
  }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast Error(STDEV)", Enabled = false)]
  public virtual Decimal? DemandPerDaySTDEV
  {
    [PXDependsOnFields(new Type[] {typeof (INItemSiteReplenishment.demandPerDayMSE)})] get
    {
      return !this._DemandPerDayMSE.HasValue ? this._DemandPerDayMSE : new Decimal?((Decimal) Math.Sqrt((double) this._DemandPerDayMSE.Value));
    }
    set
    {
    }
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID, INItemSiteReplenishment.siteID, INItemSiteReplenishment.subItemID>
  {
    public static INItemSiteReplenishment Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      int? subItemID,
      PKFindOptions options = 0)
    {
      return (INItemSiteReplenishment) PrimaryKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID, INItemSiteReplenishment.siteID, INItemSiteReplenishment.subItemID>.FindBy(graph, (object) inventoryID, (object) siteID, (object) subItemID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.siteID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.subItemID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteReplenishment.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteReplenishment.siteID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteReplenishment.subItemID>
  {
  }

  public abstract class safetyStock : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.safetyStock>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteReplenishment.minQty>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteReplenishment.maxQty>
  {
  }

  public abstract class transferERQ : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.transferERQ>
  {
  }

  public abstract class itemStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteReplenishment.itemStatus>
  {
  }

  public abstract class safetyStockSuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.safetyStockSuggested>
  {
  }

  public abstract class minQtySuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.minQtySuggested>
  {
  }

  public abstract class maxQtySuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.maxQtySuggested>
  {
  }

  public abstract class demandPerDayAverage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.demandPerDayAverage>
  {
  }

  public abstract class demandPerDayMSE : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.demandPerDayMSE>
  {
  }

  public abstract class demandPerDayMAD : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.demandPerDayMAD>
  {
  }

  public abstract class demandPerDaySTDEV : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteReplenishment.demandPerDaySTDEV>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemSiteReplenishment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteReplenishment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteReplenishment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemSiteReplenishment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteReplenishment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteReplenishment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSiteReplenishment.Tstamp>
  {
  }
}
