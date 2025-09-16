// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.INDeadStockEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

[PXCacheName("IN Dead Stock Enquiry Result")]
public class INDeadStockEnqResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(DescriptionField = typeof (INSite.descr), DisplayName = "Warehouse", IsKey = true)]
  public virtual int? SiteID { get; set; }

  [AnyInventory(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.stkItem, NotEqual<boolFalse>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [SubItem]
  public virtual int? SubItemID { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In Stock Qty.")]
  public virtual Decimal? InStockQty { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Dead Stock Qty.")]
  public virtual Decimal? DeadStockQty { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In Dead Stock (days)")]
  public virtual Decimal? InDeadStockDays { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Last Purchase Date")]
  public virtual DateTime? LastPurchaseDate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Last Sale Date")]
  public virtual DateTime? LastSaleDate { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost")]
  public virtual Decimal? LastCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Dead Stock Cost")]
  public virtual Decimal? TotalDeadStockCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Dead Stock Item Average Cost")]
  public virtual Decimal? AverageItemCost { get; set; }

  /// <summary>
  /// The base <see cref="T:System.Currency" />.
  /// </summary>
  [PXString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXUIField(DisplayName = "Currency")]
  public virtual 
  #nullable disable
  string BaseCuryID { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqResult.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqResult.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqResult.subItemID>
  {
  }

  public abstract class inStockQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INDeadStockEnqResult.inStockQty>
  {
  }

  public abstract class deadStockQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INDeadStockEnqResult.deadStockQty>
  {
  }

  public abstract class inDeadStockDays : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INDeadStockEnqResult.inDeadStockDays>
  {
  }

  public abstract class lastPurchaseDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INDeadStockEnqResult.lastPurchaseDate>
  {
  }

  public abstract class lastSaleDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INDeadStockEnqResult.lastSaleDate>
  {
  }

  public abstract class lastCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INDeadStockEnqResult.lastCost>
  {
  }

  public abstract class totalDeadStockCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INDeadStockEnqResult.totalDeadStockCost>
  {
  }

  public abstract class averageItemCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INDeadStockEnqResult.averageItemCost>
  {
  }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INDeadStockEnqResult.baseCuryID>
  {
  }
}
