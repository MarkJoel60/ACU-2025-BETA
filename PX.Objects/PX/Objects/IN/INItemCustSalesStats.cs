// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCustSalesStats
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
[Serializable]
public class INItemCustSalesStats : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _BAccountID;
  protected DateTime? _LastDate;
  protected Decimal? _LastQty;
  protected Decimal? _LastUnitPrice;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBDate]
  public virtual DateTime? LastDate
  {
    get => this._LastDate;
    set => this._LastDate = value;
  }

  [PXDBQuantity]
  public virtual Decimal? LastQty
  {
    get => this._LastQty;
    set => this._LastQty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? LastUnitPrice
  {
    get => this._LastUnitPrice;
    set => this._LastUnitPrice = value;
  }

  [PXDBDate]
  public virtual DateTime? DropShipLastDate { get; set; }

  [PXDBQuantity]
  public virtual Decimal? DropShipLastQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? DropShipLastUnitPrice { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.inventoryID, INItemCustSalesStats.subItemID, INItemCustSalesStats.siteID, INItemCustSalesStats.bAccountID>
  {
    public static INItemCustSalesStats Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? bAccountID,
      PKFindOptions options = 0)
    {
      return (INItemCustSalesStats) PrimaryKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.inventoryID, INItemCustSalesStats.subItemID, INItemCustSalesStats.siteID, INItemCustSalesStats.bAccountID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) bAccountID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.siteID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<INItemCustSalesStats>.By<INItemCustSalesStats.bAccountID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesStats.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesStats.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesStats.siteID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesStats.bAccountID>
  {
  }

  public abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemCustSalesStats.lastDate>
  {
  }

  public abstract class lastQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCustSalesStats.lastQty>
  {
  }

  public abstract class lastUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesStats.lastUnitPrice>
  {
  }

  public abstract class dropShipLastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemCustSalesStats.dropShipLastDate>
  {
  }

  public abstract class dropShipLastQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesStats.dropShipLastQty>
  {
  }

  public abstract class dropShipLastUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesStats.dropShipLastUnitPrice>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemCustSalesStats.Tstamp>
  {
  }
}
