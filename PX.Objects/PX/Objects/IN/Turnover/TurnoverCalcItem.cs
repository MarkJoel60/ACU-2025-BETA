// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.TurnoverCalcItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXCacheName]
[PXProjection(typeof (SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INTurnoverCalcItem.FK.InventoryItem>>, FbqlJoins.Inner<INSite>.On<INTurnoverCalcItem.FK.Site>>>.Where<BqlOperand<INTurnoverCalcItem.isVirtual, IBqlBool>.IsEqual<False>>), Persistent = false)]
public class TurnoverCalcItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (INTurnoverCalcItem.branchID))]
  public virtual int? BranchID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (INTurnoverCalcItem.fromPeriodID))]
  public virtual 
  #nullable disable
  string FromPeriodID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (INTurnoverCalcItem.toPeriodID))]
  public virtual string ToPeriodID { get; set; }

  [StockItem(BqlField = typeof (INTurnoverCalcItem.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual string InventoryCD { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  public virtual int? ItemClassID { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [INUnit(BqlField = typeof (PX.Objects.IN.InventoryItem.baseUnit))]
  public virtual string UOM { get; set; }

  [Site(BqlField = typeof (INTurnoverCalcItem.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, BqlField = typeof (INSite.siteCD))]
  public virtual string SiteCD { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.begQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Inventory (Units)")]
  public virtual Decimal? BegQty { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTurnoverCalcItem.begCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beginning Inventory")]
  public virtual Decimal? BegCost { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.ytdQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Inventory (Units)")]
  public virtual Decimal? YtdQty { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTurnoverCalcItem.ytdCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Inventory")]
  public virtual Decimal? YtdCost { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.avgQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average  Inventory (Units)")]
  public virtual Decimal? AvgQty { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTurnoverCalcItem.avgCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average Inventory")]
  public virtual Decimal? AvgCost { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.soldQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. of Items Sold")]
  public virtual Decimal? SoldQty { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTurnoverCalcItem.soldCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost of Goods Sold")]
  public virtual Decimal? SoldCost { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.qtyRatio))]
  [PXUIField(DisplayName = "Turnover Ratio (Units)", Visible = false)]
  public virtual Decimal? QtyRatio { get; set; }

  [PXDBPriceCost(true, BqlField = typeof (INTurnoverCalcItem.costRatio))]
  [PXUIField(DisplayName = "Turnover Ratio")]
  public virtual Decimal? CostRatio { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.qtySellDays))]
  public virtual Decimal? QtySellDays { get; set; }

  [PXDBQuantity(BqlField = typeof (INTurnoverCalcItem.costSellDays))]
  [PXUIField(DisplayName = "Days Sales of Inventory")]
  public virtual Decimal? CostSellDays { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TurnoverCalcItem.branchID>
  {
  }

  public abstract class fromPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TurnoverCalcItem.fromPeriodID>
  {
  }

  public abstract class toPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TurnoverCalcItem.toPeriodID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TurnoverCalcItem.inventoryID>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TurnoverCalcItem.inventoryCD>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TurnoverCalcItem.itemClassID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TurnoverCalcItem.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TurnoverCalcItem.uOM>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TurnoverCalcItem.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TurnoverCalcItem.siteCD>
  {
  }

  public abstract class begQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.begQty>
  {
  }

  public abstract class begCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.begCost>
  {
  }

  public abstract class ytdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.ytdQty>
  {
  }

  public abstract class ytdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.ytdCost>
  {
  }

  public abstract class avgQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.avgQty>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.avgCost>
  {
  }

  public abstract class soldQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.soldQty>
  {
  }

  public abstract class soldCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.soldCost>
  {
  }

  public abstract class qtyRatio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.qtyRatio>
  {
  }

  public abstract class costRatio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TurnoverCalcItem.costRatio>
  {
  }

  public abstract class qtySellDays : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TurnoverCalcItem.qtySellDays>
  {
  }

  public abstract class costSellDays : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TurnoverCalcItem.costSellDays>
  {
  }
}
