// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemSiteHistD
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
[ItemSiteHistD.Accumulator]
public class ItemSiteHistD : INItemSiteHistD
{
  [Inventory(IsKey = true)]
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

  [Site(IsKey = true)]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBDate(IsKey = true)]
  public override DateTime? SDate
  {
    get => this._SDate;
    set => this._SDate = value;
  }

  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistD.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistD.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistD.siteID>
  {
  }

  public new abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ItemSiteHistD.sDate>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
    {
      PXAccumulatorAttribute.RunningTotalRule[] runningTotalRuleArray = new PXAccumulatorAttribute.RunningTotalRule[2];
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer1 = PXAccumulatorAttribute.Run<INItemSiteHistD.begQty>();
      runningTotalRuleArray[0] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer1).WithValueOf<INItemSiteHistD.endQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer2 = PXAccumulatorAttribute.Run<INItemSiteHistD.endQty>();
      runningTotalRuleArray[1] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer2).WithOwnValue();
      // ISSUE: explicit constructor call
      base.\u002Ector(runningTotalRuleArray);
    }

    protected virtual bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<INItemSiteHistD.qtyReceived>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyIssued>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtySales>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyCreditMemos>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyDropShipSales>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyTransferIn>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyTransferOut>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyAssemblyIn>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyAssemblyOut>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.qtyAdjusted>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSiteHistD.sDay>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistD.sMonth>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistD.sYear>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistD.sQuater>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSiteHistD.sDayOfWeek>((PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
