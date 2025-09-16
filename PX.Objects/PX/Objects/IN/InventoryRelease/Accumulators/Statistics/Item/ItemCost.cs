// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item;

[PXHidden]
[ItemStatsAccumulator(LastCostDateField = typeof (INItemCost.lastCostDate), LastCostField = typeof (INItemCost.lastCost), MinCostField = typeof (INItemCost.minCost), MaxCostField = typeof (INItemCost.maxCost), QtyOnHandField = typeof (INItemCost.qtyOnHand), TotalCostField = typeof (INItemCost.totalCost))]
[PXDisableCloneAttributes]
[PXBreakInheritance]
public class ItemCost : INItemCost
{
  [StockItem(IsKey = true, DirtyRead = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => base.InventoryID;
    set => base.InventoryID = value;
  }

  [PXInt]
  public virtual int? LastCostSiteID { get; set; }

  [PXDecimal]
  public override Decimal? TranUnitCost
  {
    get => base.TranUnitCost;
    set => base.TranUnitCost = value;
  }

  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemCost.inventoryID>
  {
  }

  public abstract class lastCostSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCost.lastCostSiteID>
  {
  }

  public new abstract class tranUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ItemCost.tranUnitCost>
  {
  }
}
