// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCostHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Item Cost History")]
[Serializable]
public class INItemCostHist : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _FinPtdCostIssued;
  protected Decimal? _FinPtdCostReceived;
  protected Decimal? _FinBegCost;
  protected Decimal? _FinYtdCost;
  protected Decimal? _FinPtdQtyIssued;
  protected Decimal? _FinPtdQtyReceived;
  protected Decimal? _FinBegQty;
  protected Decimal? _FinYtdQty;
  protected Decimal? _FinPtdCOGS;
  protected Decimal? _FinPtdCOGSCredits;
  protected Decimal? _FinPtdCOGSDropShips;
  protected Decimal? _FinPtdCostTransferIn;
  protected Decimal? _FinPtdCostTransferOut;
  protected Decimal? _FinPtdCostAssemblyIn;
  protected Decimal? _FinPtdCostAssemblyOut;
  protected Decimal? _FinPtdCostAdjusted;
  protected Decimal? _FinPtdQtySales;
  protected Decimal? _FinPtdQtyCreditMemos;
  protected Decimal? _FinPtdQtyDropShipSales;
  protected Decimal? _FinPtdQtyTransferIn;
  protected Decimal? _FinPtdQtyTransferOut;
  protected Decimal? _FinPtdQtyAssemblyIn;
  protected Decimal? _FinPtdQtyAssemblyOut;
  protected Decimal? _FinPtdQtyAdjusted;
  protected Decimal? _FinPtdSales;
  protected Decimal? _FinPtdCreditMemos;
  protected Decimal? _FinPtdDropShipSales;
  protected Decimal? _TranPtdCostReceived;
  protected Decimal? _TranPtdCostIssued;
  protected Decimal? _TranPtdQtyReceived;
  protected Decimal? _TranPtdQtyIssued;
  protected Decimal? _TranPtdCOGS;
  protected Decimal? _TranPtdCOGSCredits;
  protected Decimal? _TranPtdCOGSDropShips;
  protected Decimal? _TranPtdCostTransferIn;
  protected Decimal? _TranPtdCostTransferOut;
  protected Decimal? _TranPtdCostAssemblyIn;
  protected Decimal? _TranPtdCostAssemblyOut;
  protected Decimal? _TranPtdCostAdjusted;
  protected Decimal? _TranPtdQtySales;
  protected Decimal? _TranPtdQtyCreditMemos;
  protected Decimal? _TranPtdQtyDropShipSales;
  protected Decimal? _TranPtdQtyTransferIn;
  protected Decimal? _TranPtdQtyTransferOut;
  protected Decimal? _TranPtdQtyAssemblyIn;
  protected Decimal? _TranPtdQtyAssemblyOut;
  protected Decimal? _TranPtdQtyAdjusted;
  protected Decimal? _TranPtdSales;
  protected Decimal? _TranPtdCreditMemos;
  protected Decimal? _TranPtdDropShipSales;
  protected Decimal? _TranBegCost;
  protected Decimal? _TranYtdCost;
  protected Decimal? _TranBegQty;
  protected Decimal? _TranYtdQty;
  protected byte[] _tstamp;

  [StockItem(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (INItemCostHist.FK.InventoryItem))]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true)]
  [PXDefault]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [Site(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (INItemCostHist.FK.CostSite))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Site(true)]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? FinPtdCostIssued
  {
    get => this._FinPtdCostIssued;
    set => this._FinPtdCostIssued = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? FinPtdCostReceived
  {
    get => this._FinPtdCostReceived;
    set => this._FinPtdCostReceived = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Cost")]
  public virtual Decimal? FinBegCost
  {
    get => this._FinBegCost;
    set => this._FinBegCost = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Cost")]
  public virtual Decimal? FinYtdCost
  {
    get => this._FinYtdCost;
    set => this._FinYtdCost = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Issued")]
  public virtual Decimal? FinPtdQtyIssued
  {
    get => this._FinPtdQtyIssued;
    set => this._FinPtdQtyIssued = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Received")]
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

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "COGS")]
  public virtual Decimal? FinPtdCOGS
  {
    get => this._FinPtdCOGS;
    set => this._FinPtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? FinPtdCOGSCredits
  {
    get => this._FinPtdCOGSCredits;
    set => this._FinPtdCOGSCredits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop ships")]
  public virtual Decimal? FinPtdCOGSDropShips
  {
    get => this._FinPtdCOGSDropShips;
    set => this._FinPtdCOGSDropShips = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? FinPtdCostTransferIn
  {
    get => this._FinPtdCostTransferIn;
    set => this._FinPtdCostTransferIn = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer out")]
  public virtual Decimal? FinPtdCostTransferOut
  {
    get => this._FinPtdCostTransferOut;
    set => this._FinPtdCostTransferOut = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? FinPtdCostAssemblyIn
  {
    get => this._FinPtdCostAssemblyIn;
    set => this._FinPtdCostAssemblyIn = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? FinPtdCostAssemblyOut
  {
    get => this._FinPtdCostAssemblyOut;
    set => this._FinPtdCostAssemblyOut = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCostAMAssemblyIn { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCostAMAssemblyOut { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted")]
  public virtual Decimal? FinPtdCostAdjusted
  {
    get => this._FinPtdCostAdjusted;
    set => this._FinPtdCostAdjusted = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Sales")]
  public virtual Decimal? FinPtdQtySales
  {
    get => this._FinPtdQtySales;
    set => this._FinPtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Credit Memos")]
  public virtual Decimal? FinPtdQtyCreditMemos
  {
    get => this._FinPtdQtyCreditMemos;
    set => this._FinPtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Drop ship Sales")]
  public virtual Decimal? FinPtdQtyDropShipSales
  {
    get => this._FinPtdQtyDropShipSales;
    set => this._FinPtdQtyDropShipSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Transfer In")]
  public virtual Decimal? FinPtdQtyTransferIn
  {
    get => this._FinPtdQtyTransferIn;
    set => this._FinPtdQtyTransferIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Transfer Out")]
  public virtual Decimal? FinPtdQtyTransferOut
  {
    get => this._FinPtdQtyTransferOut;
    set => this._FinPtdQtyTransferOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Assembly In")]
  public virtual Decimal? FinPtdQtyAssemblyIn
  {
    get => this._FinPtdQtyAssemblyIn;
    set => this._FinPtdQtyAssemblyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Assembly Out")]
  public virtual Decimal? FinPtdQtyAssemblyOut
  {
    get => this._FinPtdQtyAssemblyOut;
    set => this._FinPtdQtyAssemblyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdQtyAMAssemblyIn { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdQtyAMAssemblyOut { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Adjusted")]
  public virtual Decimal? FinPtdQtyAdjusted
  {
    get => this._FinPtdQtyAdjusted;
    set => this._FinPtdQtyAdjusted = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? FinPtdSales
  {
    get => this._FinPtdSales;
    set => this._FinPtdSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? FinPtdCreditMemos
  {
    get => this._FinPtdCreditMemos;
    set => this._FinPtdCreditMemos = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop ship Sales")]
  public virtual Decimal? FinPtdDropShipSales
  {
    get => this._FinPtdDropShipSales;
    set => this._FinPtdDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostReceived
  {
    get => this._TranPtdCostReceived;
    set => this._TranPtdCostReceived = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostIssued
  {
    get => this._TranPtdCostIssued;
    set => this._TranPtdCostIssued = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyReceived
  {
    get => this._TranPtdQtyReceived;
    set => this._TranPtdQtyReceived = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyIssued
  {
    get => this._TranPtdQtyIssued;
    set => this._TranPtdQtyIssued = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCOGS
  {
    get => this._TranPtdCOGS;
    set => this._TranPtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCOGSCredits
  {
    get => this._TranPtdCOGSCredits;
    set => this._TranPtdCOGSCredits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCOGSDropShips
  {
    get => this._TranPtdCOGSDropShips;
    set => this._TranPtdCOGSDropShips = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostTransferIn
  {
    get => this._TranPtdCostTransferIn;
    set => this._TranPtdCostTransferIn = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostTransferOut
  {
    get => this._TranPtdCostTransferOut;
    set => this._TranPtdCostTransferOut = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostAssemblyIn
  {
    get => this._TranPtdCostAssemblyIn;
    set => this._TranPtdCostAssemblyIn = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostAssemblyOut
  {
    get => this._TranPtdCostAssemblyOut;
    set => this._TranPtdCostAssemblyOut = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostAMAssemblyIn { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostAMAssemblyOut { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCostAdjusted
  {
    get => this._TranPtdCostAdjusted;
    set => this._TranPtdCostAdjusted = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtySales
  {
    get => this._TranPtdQtySales;
    set => this._TranPtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyCreditMemos
  {
    get => this._TranPtdQtyCreditMemos;
    set => this._TranPtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyDropShipSales
  {
    get => this._TranPtdQtyDropShipSales;
    set => this._TranPtdQtyDropShipSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyTransferIn
  {
    get => this._TranPtdQtyTransferIn;
    set => this._TranPtdQtyTransferIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyTransferOut
  {
    get => this._TranPtdQtyTransferOut;
    set => this._TranPtdQtyTransferOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyAssemblyIn
  {
    get => this._TranPtdQtyAssemblyIn;
    set => this._TranPtdQtyAssemblyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyAssemblyOut
  {
    get => this._TranPtdQtyAssemblyOut;
    set => this._TranPtdQtyAssemblyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyAMAssemblyIn { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyAMAssemblyOut { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdQtyAdjusted
  {
    get => this._TranPtdQtyAdjusted;
    set => this._TranPtdQtyAdjusted = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdSales
  {
    get => this._TranPtdSales;
    set => this._TranPtdSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCreditMemos
  {
    get => this._TranPtdCreditMemos;
    set => this._TranPtdCreditMemos = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDropShipSales
  {
    get => this._TranPtdDropShipSales;
    set => this._TranPtdDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranBegCost
  {
    get => this._TranBegCost;
    set => this._TranBegCost = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdCost
  {
    get => this._TranYtdCost;
    set => this._TranYtdCost = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranBegQty
  {
    get => this._TranBegQty;
    set => this._TranBegQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
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
    PrimaryKeyOf<INItemCostHist>.By<INItemCostHist.inventoryID, INItemCostHist.costSubItemID, INItemCostHist.costSiteID, INItemCostHist.accountID, INItemCostHist.subID, INItemCostHist.finPeriodID>
  {
    public static INItemCostHist Find(
      PXGraph graph,
      int? inventoryID,
      int? costSubItemID,
      int? costSiteID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (INItemCostHist) PrimaryKeyOf<INItemCostHist>.By<INItemCostHist.inventoryID, INItemCostHist.costSubItemID, INItemCostHist.costSiteID, INItemCostHist.accountID, INItemCostHist.subID, INItemCostHist.finPeriodID>.FindBy(graph, (object) inventoryID, (object) costSubItemID, (object) costSiteID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.costSiteID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.subID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCostHist>.By<INItemCostHist.siteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.inventoryID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.subID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCostHist.siteID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemCostHist.finPeriodID>
  {
  }

  public abstract class finPtdCostIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostIssued>
  {
  }

  public abstract class finPtdCostReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostReceived>
  {
  }

  public abstract class finBegCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finBegCost>
  {
  }

  public abstract class finYtdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finYtdCost>
  {
  }

  public abstract class finPtdQtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyIssued>
  {
  }

  public abstract class finPtdQtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyReceived>
  {
  }

  public abstract class finBegQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finBegQty>
  {
  }

  public abstract class finYtdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finYtdQty>
  {
  }

  public abstract class finPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finPtdCOGS>
  {
  }

  public abstract class finPtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCOGSCredits>
  {
  }

  public abstract class finPtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCOGSDropShips>
  {
  }

  public abstract class finPtdCostTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostTransferIn>
  {
  }

  public abstract class finPtdCostTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostTransferOut>
  {
  }

  public abstract class finPtdCostAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostAssemblyIn>
  {
  }

  public abstract class finPtdCostAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostAssemblyOut>
  {
  }

  public abstract class finPtdCostAMAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostAMAssemblyIn>
  {
  }

  public abstract class finPtdCostAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostAMAssemblyOut>
  {
  }

  public abstract class finPtdCostAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCostAdjusted>
  {
  }

  public abstract class finPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtySales>
  {
  }

  public abstract class finPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyCreditMemos>
  {
  }

  public abstract class finPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyDropShipSales>
  {
  }

  public abstract class finPtdQtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyTransferIn>
  {
  }

  public abstract class finPtdQtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyTransferOut>
  {
  }

  public abstract class finPtdQtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyAssemblyIn>
  {
  }

  public abstract class finPtdQtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyAssemblyOut>
  {
  }

  public abstract class finPtdQtyAMAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyAMAssemblyIn>
  {
  }

  public abstract class finPtdQtyAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyAMAssemblyOut>
  {
  }

  public abstract class finPtdQtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdQtyAdjusted>
  {
  }

  public abstract class finPtdSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.finPtdSales>
  {
  }

  public abstract class finPtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdCreditMemos>
  {
  }

  public abstract class finPtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.finPtdDropShipSales>
  {
  }

  public abstract class tranPtdCostReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostReceived>
  {
  }

  public abstract class tranPtdCostIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostIssued>
  {
  }

  public abstract class tranPtdQtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyReceived>
  {
  }

  public abstract class tranPtdQtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyIssued>
  {
  }

  public abstract class tranPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.tranPtdCOGS>
  {
  }

  public abstract class tranPtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCOGSCredits>
  {
  }

  public abstract class tranPtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCOGSDropShips>
  {
  }

  public abstract class tranPtdCostTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostTransferIn>
  {
  }

  public abstract class tranPtdCostTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostTransferOut>
  {
  }

  public abstract class tranPtdCostAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostAssemblyIn>
  {
  }

  public abstract class tranPtdCostAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostAssemblyOut>
  {
  }

  public abstract class tranPtdCostAMAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostAMAssemblyIn>
  {
  }

  public abstract class tranPtdCostAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostAMAssemblyOut>
  {
  }

  public abstract class tranPtdCostAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCostAdjusted>
  {
  }

  public abstract class tranPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtySales>
  {
  }

  public abstract class tranPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyCreditMemos>
  {
  }

  public abstract class tranPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyDropShipSales>
  {
  }

  public abstract class tranPtdQtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyTransferIn>
  {
  }

  public abstract class tranPtdQtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyTransferOut>
  {
  }

  public abstract class tranPtdQtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyAssemblyIn>
  {
  }

  public abstract class tranPtdQtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyAssemblyOut>
  {
  }

  public abstract class tranPtdQtyAMAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyAMAssemblyIn>
  {
  }

  public abstract class tranPtdQtyAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyAMAssemblyOut>
  {
  }

  public abstract class tranPtdQtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdQtyAdjusted>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdSales>
  {
  }

  public abstract class tranPtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdCreditMemos>
  {
  }

  public abstract class tranPtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCostHist.tranPtdDropShipSales>
  {
  }

  public abstract class tranBegCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.tranBegCost>
  {
  }

  public abstract class tranYtdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.tranYtdCost>
  {
  }

  public abstract class tranBegQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.tranBegQty>
  {
  }

  public abstract class tranYtdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCostHist.tranYtdQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemCostHist.Tstamp>
  {
  }
}
