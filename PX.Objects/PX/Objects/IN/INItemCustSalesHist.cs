// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCustSalesHist
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
public class INItemCustSalesHist : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _BAccountID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _FinPtdCOGS;
  protected Decimal? _FinPtdCOGSCredits;
  protected Decimal? _FinPtdCOGSDropShips;
  protected Decimal? _FinPtdQtySales;
  protected Decimal? _FinPtdQtyCreditMemos;
  protected Decimal? _FinPtdQtyDropShipSales;
  protected Decimal? _FinPtdSales;
  protected Decimal? _FinPtdCreditMemos;
  protected Decimal? _FinPtdDropShipSales;
  protected Decimal? _FinYtdCOGS;
  protected Decimal? _FinYtdCOGSCredits;
  protected Decimal? _FinYtdCOGSDropShips;
  protected Decimal? _FinYtdQtySales;
  protected Decimal? _FinYtdQtyCreditMemos;
  protected Decimal? _FinYtdQtyDropShipSales;
  protected Decimal? _FinYtdSales;
  protected Decimal? _FinYtdCreditMemos;
  protected Decimal? _FinYtdDropShipSales;
  protected Decimal? _TranPtdCOGS;
  protected Decimal? _TranPtdCOGSCredits;
  protected Decimal? _TranPtdCOGSDropShips;
  protected Decimal? _TranPtdQtySales;
  protected Decimal? _TranPtdQtyCreditMemos;
  protected Decimal? _TranPtdQtyDropShipSales;
  protected Decimal? _TranPtdSales;
  protected Decimal? _TranPtdCreditMemos;
  protected Decimal? _TranPtdDropShipSales;
  protected Decimal? _TranYtdCOGS;
  protected Decimal? _TranYtdCOGSCredits;
  protected Decimal? _TranYtdCOGSDropShips;
  protected Decimal? _TranYtdQtySales;
  protected Decimal? _TranYtdQtyCreditMemos;
  protected Decimal? _TranYtdQtyDropShipSales;
  protected Decimal? _TranYtdSales;
  protected Decimal? _TranYtdCreditMemos;
  protected Decimal? _TranYtdDropShipSales;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCOGS
  {
    get => this._FinPtdCOGS;
    set => this._FinPtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCOGSCredits
  {
    get => this._FinPtdCOGSCredits;
    set => this._FinPtdCOGSCredits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCOGSDropShips
  {
    get => this._FinPtdCOGSDropShips;
    set => this._FinPtdCOGSDropShips = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdQtySales
  {
    get => this._FinPtdQtySales;
    set => this._FinPtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdQtyCreditMemos
  {
    get => this._FinPtdQtyCreditMemos;
    set => this._FinPtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdQtyDropShipSales
  {
    get => this._FinPtdQtyDropShipSales;
    set => this._FinPtdQtyDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdSales
  {
    get => this._FinPtdSales;
    set => this._FinPtdSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCreditMemos
  {
    get => this._FinPtdCreditMemos;
    set => this._FinPtdCreditMemos = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDropShipSales
  {
    get => this._FinPtdDropShipSales;
    set => this._FinPtdDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdCOGS
  {
    get => this._FinYtdCOGS;
    set => this._FinYtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdCOGSCredits
  {
    get => this._FinYtdCOGSCredits;
    set => this._FinYtdCOGSCredits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdCOGSDropShips
  {
    get => this._FinYtdCOGSDropShips;
    set => this._FinYtdCOGSDropShips = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdQtySales
  {
    get => this._FinYtdQtySales;
    set => this._FinYtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdQtyCreditMemos
  {
    get => this._FinYtdQtyCreditMemos;
    set => this._FinYtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdQtyDropShipSales
  {
    get => this._FinYtdQtyDropShipSales;
    set => this._FinYtdQtyDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdSales
  {
    get => this._FinYtdSales;
    set => this._FinYtdSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdCreditMemos
  {
    get => this._FinYtdCreditMemos;
    set => this._FinYtdCreditMemos = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdDropShipSales
  {
    get => this._FinYtdDropShipSales;
    set => this._FinYtdDropShipSales = value;
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
  public virtual Decimal? TranYtdCOGS
  {
    get => this._TranYtdCOGS;
    set => this._TranYtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdCOGSCredits
  {
    get => this._TranYtdCOGSCredits;
    set => this._TranYtdCOGSCredits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdCOGSDropShips
  {
    get => this._TranYtdCOGSDropShips;
    set => this._TranYtdCOGSDropShips = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdQtySales
  {
    get => this._TranYtdQtySales;
    set => this._TranYtdQtySales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdQtyCreditMemos
  {
    get => this._TranYtdQtyCreditMemos;
    set => this._TranYtdQtyCreditMemos = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdQtyDropShipSales
  {
    get => this._TranYtdQtyDropShipSales;
    set => this._TranYtdQtyDropShipSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdSales
  {
    get => this._TranYtdSales;
    set => this._TranYtdSales = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdCreditMemos
  {
    get => this._TranYtdCreditMemos;
    set => this._TranYtdCreditMemos = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdDropShipSales
  {
    get => this._TranYtdDropShipSales;
    set => this._TranYtdDropShipSales = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.inventoryID, INItemCustSalesHist.costSubItemID, INItemCustSalesHist.costSiteID, INItemCustSalesHist.bAccountID, INItemCustSalesHist.finPeriodID>
  {
    public static INItemCustSalesHist Find(
      PXGraph graph,
      int? inventoryID,
      int? costSubItemID,
      int? costSiteID,
      int? bAccountID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (INItemCustSalesHist) PrimaryKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.inventoryID, INItemCustSalesHist.costSubItemID, INItemCustSalesHist.costSiteID, INItemCustSalesHist.bAccountID, INItemCustSalesHist.finPeriodID>.FindBy(graph, (object) inventoryID, (object) costSubItemID, (object) costSiteID, (object) bAccountID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.costSiteID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<INItemCustSalesHist>.By<INItemCustSalesHist.bAccountID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesHist.inventoryID>
  {
  }

  public abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemCustSalesHist.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesHist.costSiteID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCustSalesHist.bAccountID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemCustSalesHist.finPeriodID>
  {
  }

  public abstract class finPtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdCOGS>
  {
  }

  public abstract class finPtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdCOGSCredits>
  {
  }

  public abstract class finPtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdCOGSDropShips>
  {
  }

  public abstract class finPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdQtySales>
  {
  }

  public abstract class finPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdQtyCreditMemos>
  {
  }

  public abstract class finPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdQtyDropShipSales>
  {
  }

  public abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdSales>
  {
  }

  public abstract class finPtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdCreditMemos>
  {
  }

  public abstract class finPtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finPtdDropShipSales>
  {
  }

  public abstract class finYtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdCOGS>
  {
  }

  public abstract class finYtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdCOGSCredits>
  {
  }

  public abstract class finYtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdCOGSDropShips>
  {
  }

  public abstract class finYtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdQtySales>
  {
  }

  public abstract class finYtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdQtyCreditMemos>
  {
  }

  public abstract class finYtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdQtyDropShipSales>
  {
  }

  public abstract class finYtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdSales>
  {
  }

  public abstract class finYtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdCreditMemos>
  {
  }

  public abstract class finYtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.finYtdDropShipSales>
  {
  }

  public abstract class tranPtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdCOGS>
  {
  }

  public abstract class tranPtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdCOGSCredits>
  {
  }

  public abstract class tranPtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdCOGSDropShips>
  {
  }

  public abstract class tranPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdQtySales>
  {
  }

  public abstract class tranPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdQtyCreditMemos>
  {
  }

  public abstract class tranPtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdQtyDropShipSales>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdSales>
  {
  }

  public abstract class tranPtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdCreditMemos>
  {
  }

  public abstract class tranPtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranPtdDropShipSales>
  {
  }

  public abstract class tranYtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdCOGS>
  {
  }

  public abstract class tranYtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdCOGSCredits>
  {
  }

  public abstract class tranYtdCOGSDropShips : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdCOGSDropShips>
  {
  }

  public abstract class tranYtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdQtySales>
  {
  }

  public abstract class tranYtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdQtyCreditMemos>
  {
  }

  public abstract class tranYtdQtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdQtyDropShipSales>
  {
  }

  public abstract class tranYtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdSales>
  {
  }

  public abstract class tranYtdCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdCreditMemos>
  {
  }

  public abstract class tranYtdDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemCustSalesHist.tranYtdDropShipSales>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemCustSalesHist.Tstamp>
  {
  }
}
