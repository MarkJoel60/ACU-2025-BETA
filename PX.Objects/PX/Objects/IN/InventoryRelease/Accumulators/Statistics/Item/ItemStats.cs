// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item;

[PXHidden]
[ItemStatsAccumulator(LastCostDateField = typeof (INItemStats.lastCostDate), LastCostField = typeof (INItemStats.lastCost), MinCostField = typeof (INItemStats.minCost), MaxCostField = typeof (INItemStats.maxCost), QtyOnHandField = typeof (INItemStats.qtyOnHand), TotalCostField = typeof (INItemStats.totalCost), LastPurchasedDateField = typeof (INItemStats.lastPurchaseDate))]
[PXDisableCloneAttributes]
public class ItemStats : INItemStats
{
  [StockItem(IsKey = true, DirtyRead = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SearchFor<InventoryItem.valMethod>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<ItemStats.inventoryID, IBqlInt>.FromCurrent>>))]
  public override 
  #nullable disable
  string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemStats.inventoryID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemStats.siteID>
  {
  }

  public new abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemStats.valMethod>
  {
  }
}
