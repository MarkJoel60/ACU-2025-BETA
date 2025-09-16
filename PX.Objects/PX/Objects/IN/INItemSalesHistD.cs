// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSalesHistD
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
public class INItemSalesHistD : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _DemandType1;
  protected int? _DemandType2;
  protected DateTime? _SDate;
  protected int? _SYear;
  protected int? _SMonth;
  protected int? _SQuater;
  protected int? _SDay;
  protected int? _SDayOfWeek;
  protected Decimal? _QtyIssues;
  protected Decimal? _QtyExcluded;
  protected Decimal? _QtyLostSales;
  protected Decimal? _QtyPlanSales;
  protected 
  #nullable disable
  byte[] _tstamp;

  [Inventory(IsKey = true)]
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

  [PXDBInt]
  [PXUIField(DisplayName = "Working Day")]
  public virtual int? DemandType1
  {
    get => this._DemandType1;
    set => this._DemandType1 = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Nont Working Day")]
  public virtual int? DemandType2
  {
    get => this._DemandType2;
    set => this._DemandType2 = value;
  }

  [PXDBDate(IsKey = true)]
  public virtual DateTime? SDate
  {
    get => this._SDate;
    set => this._SDate = value;
  }

  [PXDBInt]
  public virtual int? SYear
  {
    get => this._SYear;
    set => this._SYear = value;
  }

  [PXDBInt]
  public virtual int? SMonth
  {
    get => this._SMonth;
    set => this._SMonth = value;
  }

  [PXDBInt]
  public virtual int? SQuater
  {
    get => this._SQuater;
    set => this._SQuater = value;
  }

  [PXDBInt]
  public virtual int? SDay
  {
    get => this._SDay;
    set => this._SDay = value;
  }

  [PXDBInt]
  public virtual int? SDayOfWeek
  {
    get => this._SDayOfWeek;
    set => this._SDayOfWeek = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Issues")]
  public virtual Decimal? QtyIssues
  {
    get => this._QtyIssues;
    set => this._QtyIssues = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Excluded")]
  public virtual Decimal? QtyExcluded
  {
    get => this._QtyExcluded;
    set => this._QtyExcluded = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Lost Sales")]
  public virtual Decimal? QtyLostSales
  {
    get => this._QtyLostSales;
    set => this._QtyLostSales = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Plan Sales")]
  public virtual Decimal? QtyPlanSales
  {
    get => this._QtyPlanSales;
    set => this._QtyPlanSales = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemSalesHistD>.By<INItemSalesHistD.inventoryID, INItemSalesHistD.siteID, INItemSalesHistD.subItemID, INItemSalesHistD.sDate>
  {
    public static INItemSalesHistD Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      int? subItemID,
      DateTime? sDate,
      PKFindOptions options = 0)
    {
      return (INItemSalesHistD) PrimaryKeyOf<INItemSalesHistD>.By<INItemSalesHistD.inventoryID, INItemSalesHistD.siteID, INItemSalesHistD.subItemID, INItemSalesHistD.sDate>.FindBy(graph, (object) inventoryID, (object) siteID, (object) subItemID, (object) sDate, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSalesHistD>.By<INItemSalesHistD.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemSalesHistD>.By<INItemSalesHistD.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSalesHistD>.By<INItemSalesHistD.siteID>
    {
    }

    public class ItemSiteReplenishment : 
      PrimaryKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID, INItemSiteReplenishment.siteID, INItemSiteReplenishment.subItemID>.ForeignKeyOf<INItemSalesHistD>.By<INItemSalesHistD.inventoryID, INItemSalesHistD.siteID, INItemSalesHistD.subItemID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.siteID>
  {
  }

  public abstract class demandType1 : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.demandType1>
  {
  }

  public abstract class demandType2 : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.demandType2>
  {
  }

  public abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSalesHistD.sDate>
  {
  }

  public abstract class sYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.sYear>
  {
  }

  public abstract class sMonth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.sMonth>
  {
  }

  public abstract class sQuater : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.sQuater>
  {
  }

  public abstract class sDay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.sDay>
  {
  }

  public abstract class sDayOfWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSalesHistD.sDayOfWeek>
  {
  }

  public abstract class qtyIssues : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSalesHistD.qtyIssues>
  {
  }

  public abstract class qtyExcluded : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSalesHistD.qtyExcluded>
  {
  }

  public abstract class qtyLostSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSalesHistD.qtyLostSales>
  {
  }

  public abstract class qtyPlanSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSalesHistD.qtyPlanSales>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSalesHistD.Tstamp>
  {
  }
}
