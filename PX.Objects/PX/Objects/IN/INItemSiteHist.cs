// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHist
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

[PXCacheName("IN Item Site History")]
[Serializable]
public class INItemSiteHist : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected string _LastActivityPeriod;
  protected Decimal? _FinPtdQtyIssued;
  protected Decimal? _FinPtdQtyReceived;
  protected Decimal? _FinBegQty;
  protected Decimal? _FinYtdQty;
  protected Decimal? _FinPtdQtySales;
  protected Decimal? _FinPtdQtyCreditMemos;
  protected Decimal? _FinPtdQtyDropShipSales;
  protected Decimal? _FinPtdQtyTransferIn;
  protected Decimal? _FinPtdQtyTransferOut;
  protected Decimal? _FinPtdQtyAssemblyIn;
  protected Decimal? _FinPtdQtyAssemblyOut;
  protected Decimal? _FinPtdQtyAdjusted;
  protected Decimal? _TranPtdQtyReceived;
  protected Decimal? _TranPtdQtyIssued;
  protected Decimal? _TranPtdQtySales;
  protected Decimal? _TranPtdQtyCreditMemos;
  protected Decimal? _TranPtdQtyDropShipSales;
  protected Decimal? _TranPtdQtyTransferIn;
  protected Decimal? _TranPtdQtyTransferOut;
  protected Decimal? _TranPtdQtyAssemblyIn;
  protected Decimal? _TranPtdQtyAssemblyOut;
  protected Decimal? _TranPtdQtyAdjusted;
  protected Decimal? _TranBegQty;
  protected Decimal? _TranYtdQty;
  protected byte[] _tstamp;

  [StockItem(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (INItemSiteHist.FK.InventoryItem))]
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
  [PXForeignReference(typeof (INItemSiteHist.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INItemSiteHist.siteID), IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (INItemSiteHist.FK.Location))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXString(6, IsFixed = true)]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? FinPtdQtyIssued
  {
    get => this._FinPtdQtyIssued;
    set => this._FinPtdQtyIssued = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? FinPtdQtyReceived
  {
    get => this._FinPtdQtyReceived;
    set => this._FinPtdQtyReceived = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Qty.")]
  public virtual Decimal? FinBegQty
  {
    get => this._FinBegQty;
    set => this._FinBegQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Qty.")]
  public virtual Decimal? FinYtdQty
  {
    get => this._FinYtdQty;
    set => this._FinYtdQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? FinPtdQtySales
  {
    get => this._FinPtdQtySales;
    set => this._FinPtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? FinPtdQtyCreditMemos
  {
    get => this._FinPtdQtyCreditMemos;
    set => this._FinPtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop Ship Sales")]
  public virtual Decimal? FinPtdQtyDropShipSales
  {
    get => this._FinPtdQtyDropShipSales;
    set => this._FinPtdQtyDropShipSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? FinPtdQtyTransferIn
  {
    get => this._FinPtdQtyTransferIn;
    set => this._FinPtdQtyTransferIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Out")]
  public virtual Decimal? FinPtdQtyTransferOut
  {
    get => this._FinPtdQtyTransferOut;
    set => this._FinPtdQtyTransferOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? FinPtdQtyAssemblyIn
  {
    get => this._FinPtdQtyAssemblyIn;
    set => this._FinPtdQtyAssemblyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? FinPtdQtyAssemblyOut
  {
    get => this._FinPtdQtyAssemblyOut;
    set => this._FinPtdQtyAssemblyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted")]
  public virtual Decimal? FinPtdQtyAdjusted
  {
    get => this._FinPtdQtyAdjusted;
    set => this._FinPtdQtyAdjusted = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? TranPtdQtyReceived
  {
    get => this._TranPtdQtyReceived;
    set => this._TranPtdQtyReceived = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? TranPtdQtyIssued
  {
    get => this._TranPtdQtyIssued;
    set => this._TranPtdQtyIssued = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? TranPtdQtySales
  {
    get => this._TranPtdQtySales;
    set => this._TranPtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? TranPtdQtyCreditMemos
  {
    get => this._TranPtdQtyCreditMemos;
    set => this._TranPtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop Ship Sales")]
  public virtual Decimal? TranPtdQtyDropShipSales
  {
    get => this._TranPtdQtyDropShipSales;
    set => this._TranPtdQtyDropShipSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? TranPtdQtyTransferIn
  {
    get => this._TranPtdQtyTransferIn;
    set => this._TranPtdQtyTransferIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Out")]
  public virtual Decimal? TranPtdQtyTransferOut
  {
    get => this._TranPtdQtyTransferOut;
    set => this._TranPtdQtyTransferOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? TranPtdQtyAssemblyIn
  {
    get => this._TranPtdQtyAssemblyIn;
    set => this._TranPtdQtyAssemblyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? TranPtdQtyAssemblyOut
  {
    get => this._TranPtdQtyAssemblyOut;
    set => this._TranPtdQtyAssemblyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted", Visible = false)]
  public virtual Decimal? TranPtdQtyAdjusted
  {
    get => this._TranPtdQtyAdjusted;
    set => this._TranPtdQtyAdjusted = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Qty.")]
  public virtual Decimal? TranBegQty
  {
    get => this._TranBegQty;
    set => this._TranBegQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Qty.")]
  public virtual Decimal? TranYtdQty
  {
    get => this._TranYtdQty;
    set => this._TranYtdQty = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemSiteHist>.By<INItemSiteHist.inventoryID, INItemSiteHist.siteID, INItemSiteHist.subItemID, INItemSiteHist.locationID, INItemSiteHist.finPeriodID>
  {
    public static INItemSiteHist Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      int? subItemID,
      int? locationID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (INItemSiteHist) PrimaryKeyOf<INItemSiteHist>.By<INItemSiteHist.inventoryID, INItemSiteHist.siteID, INItemSiteHist.subItemID, INItemSiteHist.locationID, INItemSiteHist.finPeriodID>.FindBy(graph, (object) inventoryID, (object) siteID, (object) subItemID, (object) locationID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSiteHist>.By<INItemSiteHist.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSiteHist>.By<INItemSiteHist.siteID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemSiteHist>.By<INItemSiteHist.subItemID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSiteHist>.By<INItemSiteHist.locationID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHist.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHist.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHist.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHist.locationID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSiteHist.finPeriodID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteHist.lastActivityPeriod>
  {
  }

  public abstract class finPtdQtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyIssued>
  {
  }

  public abstract class finPtdQtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyReceived>
  {
  }

  public abstract class finBegQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHist.finBegQty>
  {
  }

  public abstract class finYtdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHist.finYtdQty>
  {
  }

  public abstract class finPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtySales>
  {
  }

  public abstract class finPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyCreditMemos>
  {
  }

  public abstract class finPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyDropShipSales>
  {
  }

  public abstract class finPtdQtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyTransferIn>
  {
  }

  public abstract class finPtdQtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyTransferOut>
  {
  }

  public abstract class finPtdQtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyAssemblyIn>
  {
  }

  public abstract class finPtdQtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyAssemblyOut>
  {
  }

  public abstract class finPtdQtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.finPtdQtyAdjusted>
  {
  }

  public abstract class tranPtdQtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyReceived>
  {
  }

  public abstract class tranPtdQtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyIssued>
  {
  }

  public abstract class tranPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtySales>
  {
  }

  public abstract class tranPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyCreditMemos>
  {
  }

  public abstract class tranPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyDropShipSales>
  {
  }

  public abstract class tranPtdQtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyTransferIn>
  {
  }

  public abstract class tranPtdQtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyTransferOut>
  {
  }

  public abstract class tranPtdQtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyAssemblyIn>
  {
  }

  public abstract class tranPtdQtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyAssemblyOut>
  {
  }

  public abstract class tranPtdQtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHist.tranPtdQtyAdjusted>
  {
  }

  public abstract class tranBegQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHist.tranBegQty>
  {
  }

  public abstract class tranYtdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHist.tranYtdQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSiteHist.Tstamp>
  {
  }
}
