// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistD
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[INItemSiteHistDProjection]
[PXHidden]
public class INItemSiteHistD : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected DateTime? _SDate;
  protected int? _SYear;
  protected int? _SMonth;
  protected int? _SQuater;
  protected int? _SDay;
  protected int? _SDayOfWeek;
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

  [Site(IsKey = true, BqlField = typeof (INItemSiteHistByCostCenterD.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [StockItem(IsKey = true, BqlField = typeof (INItemSiteHistByCostCenterD.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INItemSiteHistByCostCenterD.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBDate(IsKey = true, BqlField = typeof (INItemSiteHistByCostCenterD.sDate))]
  public virtual DateTime? SDate
  {
    get => this._SDate;
    set => this._SDate = value;
  }

  [PXDBInt(BqlField = typeof (INItemSiteHistByCostCenterD.sYear))]
  public virtual int? SYear
  {
    get => this._SYear;
    set => this._SYear = value;
  }

  [PXDBInt(BqlField = typeof (INItemSiteHistByCostCenterD.sMonth))]
  public virtual int? SMonth
  {
    get => this._SMonth;
    set => this._SMonth = value;
  }

  [PXDBInt(BqlField = typeof (INItemSiteHistByCostCenterD.sQuater))]
  public virtual int? SQuater
  {
    get => this._SQuater;
    set => this._SQuater = value;
  }

  [PXDBInt(BqlField = typeof (INItemSiteHistByCostCenterD.sDay))]
  public virtual int? SDay
  {
    get => this._SDay;
    set => this._SDay = value;
  }

  [PXDBInt(BqlField = typeof (INItemSiteHistByCostCenterD.sDayOfWeek))]
  public virtual int? SDayOfWeek
  {
    get => this._SDayOfWeek;
    set => this._SDayOfWeek = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyReceived))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? QtyReceived
  {
    get => this._QtyReceived;
    set => this._QtyReceived = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyIssued))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? QtyIssued
  {
    get => this._QtyIssued;
    set => this._QtyIssued = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtySales))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? QtySales
  {
    get => this._QtySales;
    set => this._QtySales = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyCreditMemos))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? QtyCreditMemos
  {
    get => this._QtyCreditMemos;
    set => this._QtyCreditMemos = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyDropShipSales))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop Ship Sales")]
  public virtual Decimal? QtyDropShipSales
  {
    get => this._QtyDropShipSales;
    set => this._QtyDropShipSales = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyTransferIn))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? QtyTransferIn
  {
    get => this._QtyTransferIn;
    set => this._QtyTransferIn = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyTransferOut))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Out")]
  public virtual Decimal? QtyTransferOut
  {
    get => this._QtyTransferOut;
    set => this._QtyTransferOut = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyAssemblyIn))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? QtyAssemblyIn
  {
    get => this._QtyAssemblyIn;
    set => this._QtyAssemblyIn = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyAssemblyOut))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? QtyAssemblyOut
  {
    get => this._QtyAssemblyOut;
    set => this._QtyAssemblyOut = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyAdjusted))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted")]
  public virtual Decimal? QtyAdjusted
  {
    get => this._QtyAdjusted;
    set => this._QtyAdjusted = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.begQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegQty { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.endQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndQty { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyDebit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyDebit { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemSiteHistByCostCenterD.qtyCredit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyCredit { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INItemSiteHistByCostCenterD.costDebit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CostDebit { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INItemSiteHistByCostCenterD.costCredit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CostCredit { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    INItemSiteHistD>.By<INItemSiteHistD.siteID, INItemSiteHistD.inventoryID, INItemSiteHistD.subItemID, INItemSiteHistD.sDate>
  {
    public static INItemSiteHistD Find(
      PXGraph graph,
      int? siteID,
      int? inventoryID,
      int? subItemID,
      DateTime? sDate,
      PKFindOptions options = 0)
    {
      return (INItemSiteHistD) PrimaryKeyOf<INItemSiteHistD>.By<INItemSiteHistD.siteID, INItemSiteHistD.inventoryID, INItemSiteHistD.subItemID, INItemSiteHistD.sDate>.FindBy(graph, (object) siteID, (object) inventoryID, (object) subItemID, (object) sDate, options);
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.subItemID>
  {
  }

  public abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSiteHistD.sDate>
  {
  }

  public abstract class sYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.sYear>
  {
  }

  public abstract class sMonth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.sMonth>
  {
  }

  public abstract class sQuater : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.sQuater>
  {
  }

  public abstract class sDay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.sDay>
  {
  }

  public abstract class sDayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistD.sDayOfWeek>
  {
  }

  public abstract class qtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyReceived>
  {
  }

  public abstract class qtyIssued : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.qtyIssued>
  {
  }

  public abstract class qtySales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.qtySales>
  {
  }

  public abstract class qtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyCreditMemos>
  {
  }

  public abstract class qtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyDropShipSales>
  {
  }

  public abstract class qtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyTransferIn>
  {
  }

  public abstract class qtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyTransferOut>
  {
  }

  public abstract class qtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyAssemblyIn>
  {
  }

  public abstract class qtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyAssemblyOut>
  {
  }

  public abstract class qtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistD.qtyAdjusted>
  {
  }

  public abstract class begQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.begQty>
  {
  }

  public abstract class endQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.endQty>
  {
  }

  public abstract class qtyDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.qtyDebit>
  {
  }

  public abstract class qtyCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.qtyCredit>
  {
  }

  public abstract class costDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.costDebit>
  {
  }

  public abstract class costCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSiteHistD.costCredit>
  {
  }
}
