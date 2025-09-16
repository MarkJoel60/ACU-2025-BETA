// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Statistics.ItemCustomer.ItemCustSalesStats
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Statistics.ItemCustomer;

[PXHidden]
[ItemCustSalesStats.Accumulator]
[PXDisableCloneAttributes]
public class ItemCustSalesStats : INItemCustSalesStats
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true)]
  [PXDefault]
  public override int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesStats.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesStats.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesStats.siteID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesStats.bAccountID>
  {
  }

  public class AccumulatorAttribute : INItemCustSalesStatsAccumAttribute
  {
    protected override void PrepareInsertImpl(
      INItemCustSalesStats stats,
      PXAccumulatorCollection columns)
    {
      columns.Update<INItemCustSalesStats.lastDate>((PXDataFieldAssign.AssignBehavior) 2);
      columns.Update<INItemCustSalesStats.lastUnitPrice>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemCustSalesStats.lastQty>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Restrict<INItemCustSalesStats.lastDate>((PXComp) 11, (object) stats.LastDate);
    }
  }
}
