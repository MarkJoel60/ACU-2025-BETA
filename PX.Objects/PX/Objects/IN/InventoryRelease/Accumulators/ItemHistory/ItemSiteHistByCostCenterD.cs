// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemSiteHistByCostCenterD
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;

[PXHidden]
[ItemSiteHistByCostCenterD.Accumulator]
public class ItemSiteHistByCostCenterD : INItemSiteHistByCostCenterD
{
  [Site(IsKey = true)]
  [PXDefault]
  public override int? SiteID { get; set; }

  [StockItem(IsKey = true)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [SubItem(IsKey = true)]
  [PXDefault]
  public override int? SubItemID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCenterID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDefault]
  public override DateTime? SDate { get; set; }

  public new abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistByCostCenterD.siteID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ItemSiteHistByCostCenterD.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ItemSiteHistByCostCenterD.subItemID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ItemSiteHistByCostCenterD.costCenterID>
  {
  }

  public new abstract class sDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ItemSiteHistByCostCenterD.sDate>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
      : base(new Type[3]
      {
        typeof (INItemSiteHistByCostCenterD.endQty),
        typeof (INItemSiteHistByCostCenterD.endQty),
        typeof (INItemSiteHistByCostCenterD.endCost)
      }, new Type[3]
      {
        typeof (INItemSiteHistByCostCenterD.begQty),
        typeof (INItemSiteHistByCostCenterD.endQty),
        typeof (INItemSiteHistByCostCenterD.endCost)
      })
    {
    }

    protected virtual bool PrepareInsert(
      PXCache sender,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(sender, row, columns))
        return false;
      ItemSiteHistByCostCenterD histByCostCenterD = (ItemSiteHistByCostCenterD) row;
      columns.Update<INItemSiteHistByCostCenterD.qtyReceived>((object) histByCostCenterD.QtyReceived, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyIssued>((object) histByCostCenterD.QtyIssued, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtySales>((object) histByCostCenterD.QtySales, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyCreditMemos>((object) histByCostCenterD.QtyCreditMemos, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyDropShipSales>((object) histByCostCenterD.QtyDropShipSales, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyTransferIn>((object) histByCostCenterD.QtyTransferIn, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyTransferOut>((object) histByCostCenterD.QtyTransferOut, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyAssemblyIn>((object) histByCostCenterD.QtyAssemblyIn, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyAssemblyOut>((object) histByCostCenterD.QtyAssemblyOut, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.qtyAdjusted>((object) histByCostCenterD.QtyAdjusted, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistByCostCenterD.sDay>((object) histByCostCenterD.SDay, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistByCostCenterD.sMonth>((object) histByCostCenterD.SMonth, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistByCostCenterD.sYear>((object) histByCostCenterD.SYear, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistByCostCenterD.sQuater>((object) histByCostCenterD.SQuater, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistByCostCenterD.sDayOfWeek>((object) histByCostCenterD.SDayOfWeek, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
