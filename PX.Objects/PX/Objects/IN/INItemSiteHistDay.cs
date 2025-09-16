// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistDay
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

[PXCacheName("IN Item Site History Day")]
[Serializable]
public class INItemSiteHistDay : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected DateTime? _SDate;
  protected Decimal? _QtyReceived;
  protected Decimal? _QtyIssued;
  protected Decimal? _QtySales;
  protected Decimal? _QtyCreditMemos;
  protected Decimal? _QtyDropShipSales;
  protected Decimal? _QtyTransferIn;
  protected Decimal? _QtyTransferOut;
  protected Decimal? _QtyAssemblyIn;
  protected Decimal? _QtyAssemblyOut;
  protected Decimal? _QtyAdjusted;
  protected Decimal? _BegQty;
  protected Decimal? _QtyIn;
  protected Decimal? _QtyOut;
  protected Decimal? _EndQty;
  protected 
  #nullable disable
  byte[] _tstamp;

  [StockItem(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(IsKey = true)]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INItemSiteHistDay.siteID), IsKey = true)]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate(IsKey = true)]
  public virtual DateTime? SDate
  {
    get => this._SDate;
    set => this._SDate = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? QtyReceived
  {
    get => this._QtyReceived;
    set => this._QtyReceived = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? QtyIssued
  {
    get => this._QtyIssued;
    set => this._QtyIssued = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? QtySales
  {
    get => this._QtySales;
    set => this._QtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? QtyCreditMemos
  {
    get => this._QtyCreditMemos;
    set => this._QtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop Ship Sales")]
  public virtual Decimal? QtyDropShipSales
  {
    get => this._QtyDropShipSales;
    set => this._QtyDropShipSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? QtyTransferIn
  {
    get => this._QtyTransferIn;
    set => this._QtyTransferIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Out")]
  public virtual Decimal? QtyTransferOut
  {
    get => this._QtyTransferOut;
    set => this._QtyTransferOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? QtyAssemblyIn
  {
    get => this._QtyAssemblyIn;
    set => this._QtyAssemblyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? QtyAssemblyOut
  {
    get => this._QtyAssemblyOut;
    set => this._QtyAssemblyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted")]
  public virtual Decimal? QtyAdjusted
  {
    get => this._QtyAdjusted;
    set => this._QtyAdjusted = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Debit")]
  public virtual Decimal? QtyDebit { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit")]
  public virtual Decimal? QtyCredit { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegQty
  {
    get => this._BegQty;
    set => this._BegQty = value;
  }

  [PXQuantity]
  [PXFormula(typeof (Add<INItemSiteHistDay.qtyReceived, Add<INItemSiteHistDay.qtyTransferIn, INItemSiteHistDay.qtyAssemblyIn>>))]
  [PXUIField]
  public virtual Decimal? QtyIn
  {
    get => this._QtyIn;
    set => this._QtyIn = value;
  }

  [PXQuantity]
  [PXFormula(typeof (Add<INItemSiteHistDay.qtyIssued, Add<INItemSiteHistDay.qtySales, Add<INItemSiteHistDay.qtyCreditMemos, Add<INItemSiteHistDay.qtyTransferOut, INItemSiteHistDay.qtyAssemblyOut>>>>))]
  [PXUIField]
  public virtual Decimal? QtyOut
  {
    get => this._QtyOut;
    set => this._QtyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndQty
  {
    get => this._EndQty;
    set => this._EndQty = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.inventoryID, INItemSiteHistDay.subItemID, INItemSiteHistDay.siteID, INItemSiteHistDay.locationID, INItemSiteHistDay.sDate>
  {
    public static INItemSiteHistDay Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      DateTime? sDate,
      PKFindOptions options = 0)
    {
      return (INItemSiteHistDay) PrimaryKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.inventoryID, INItemSiteHistDay.subItemID, INItemSiteHistDay.siteID, INItemSiteHistDay.locationID, INItemSiteHistDay.sDate>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) sDate, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.locationID>
    {
    }

    public class ItemSiteReplenishment : 
      PrimaryKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID, INItemSiteReplenishment.siteID, INItemSiteReplenishment.subItemID>.ForeignKeyOf<INItemSiteHistDay>.By<INItemSiteHistDay.inventoryID, INItemSiteHistDay.siteID, INItemSiteHistDay.subItemID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistDay.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistDay.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistDay.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistDay.locationID>
  {
  }

  public abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSiteHistDay.sDate>
  {
  }

  public abstract class qtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyReceived>
  {
  }

  public abstract class qtyIssued : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtyIssued>
  {
  }

  public abstract class qtySales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtySales>
  {
  }

  public abstract class qtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyCreditMemos>
  {
  }

  public abstract class qtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyDropShipSales>
  {
  }

  public abstract class qtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyTransferIn>
  {
  }

  public abstract class qtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyTransferOut>
  {
  }

  public abstract class qtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyAssemblyIn>
  {
  }

  public abstract class qtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyAssemblyOut>
  {
  }

  public abstract class qtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistDay.qtyAdjusted>
  {
  }

  public abstract class qtyDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtyDebit>
  {
  }

  public abstract class qtyCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtyCredit>
  {
  }

  public abstract class begQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.begQty>
  {
  }

  public abstract class qtyIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtyIn>
  {
  }

  public abstract class qtyOut : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.qtyOut>
  {
  }

  public abstract class endQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistDay.endQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSiteHistDay.Tstamp>
  {
  }
}
