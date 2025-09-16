// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select2<InventoryItem, CrossJoin<PX.Objects.CM.Currency, LeftJoin<INItemCost, On2<INItemCost.FK.InventoryItem, And<INItemCost.FK.Currency>>, LeftJoin<InventoryItemCurySettings, On2<InventoryItemCurySettings.FK.Inventory, And<InventoryItemCurySettings.FK.Currency>>, LeftJoin<INSite, On<InventoryItemCurySettings.FK.DefaultSite>>>>>>), Persistent = false)]
[PXCacheName("Item Cost Statistics")]
public class INItemCost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (InventoryItem.inventoryID))]
  [PXDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(5, IsKey = true, IsUnicode = true, BqlField = typeof (PX.Objects.CM.Currency.curyID))]
  [PXDefault(typeof (AccessInfo.baseCuryID))]
  public 
  #nullable disable
  string CuryID { get; set; }

  [PXDBPriceCost(BqlField = typeof (INItemCost.lastCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  public virtual Decimal? LastCost { get; set; }

  [PXDBLastChangeDateTime(typeof (INItemCost.lastCost), BqlField = typeof (INItemCost.lastCostDate))]
  [PXDefault]
  public virtual DateTime? LastCostDate { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INItemCost.totalCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalCost { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCost.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average Cost", Enabled = false)]
  [PXDBPriceCostCalced(typeof (Switch<Case<Where<INItemCost.qtyOnHand, Equal<decimal0>>, decimal0>, Div<INItemCost.totalCost, INItemCost.qtyOnHand>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  public virtual Decimal? AvgCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  public virtual Decimal? MinCost { get; set; }

  [PXDBPriceCost(BqlField = typeof (INItemCost.maxCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  public virtual Decimal? MaxCost { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.valMethod, Equal<INValMethod.standard>>, InventoryItemCurySettings.stdCost, Case<Where<InventoryItem.valMethod, Equal<INValMethod.average>, And<INSite.avgDefaultCost, Equal<INSite.avgDefaultCost.lastCost>, Or<InventoryItem.valMethod, Equal<INValMethod.fIFO>, And<INSite.fIFODefaultCost, Equal<INSite.avgDefaultCost.lastCost>, Or<InventoryItem.valMethod, Equal<INValMethod.specific>>>>>>, INItemCost.lastCost>>, Switch<Case<Where<INItemCost.qtyOnHand, Equal<decimal0>>, decimal0, Case<Where<Div<INItemCost.totalCost, INItemCost.qtyOnHand>, Less<decimal0>>, INItemCost.lastCost>>, Div<INItemCost.totalCost, INItemCost.qtyOnHand>>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  public virtual Decimal? TranUnitCost { get; set; }

  public class PK : PrimaryKeyOf<INItemCost>.By<INItemCost.inventoryID, INItemCost.curyID>
  {
    public static INItemCost Find(
      PXGraph graph,
      int? inventoryID,
      string baseCuryID,
      PKFindOptions options = 0)
    {
      return (INItemCost) PrimaryKeyOf<INItemCost>.By<INItemCost.inventoryID, INItemCost.curyID>.FindBy(graph, (object) inventoryID, (object) baseCuryID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemCost>.By<INItemCost.inventoryID>
    {
    }

    public class InventoryItemCurySettings : 
      PrimaryKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.inventoryID, InventoryItemCurySettings.curyID>.ForeignKeyOf<INItemCost>.By<INItemCost.inventoryID, INItemCost.curyID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<INItemCost>.By<INItemCost.curyID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemCost.inventoryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemCost.curyID>
  {
  }

  public abstract class lastCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.lastCost>
  {
  }

  public abstract class lastCostDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemCost.lastCostDate>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.totalCost>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.qtyOnHand>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.avgCost>
  {
  }

  public abstract class minCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.minCost>
  {
  }

  public abstract class maxCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.maxCost>
  {
  }

  public abstract class tranUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemCost.tranUnitCost>
  {
  }
}
