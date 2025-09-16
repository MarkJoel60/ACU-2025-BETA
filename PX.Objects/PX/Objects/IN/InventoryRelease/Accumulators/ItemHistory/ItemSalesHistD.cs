// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemSalesHistD
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
[ItemSalesHistD.Accumulator]
[PXDisableCloneAttributes]
public class ItemSalesHistD : INItemSalesHistD
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

  [PXDBInt(IsKey = true)]
  public override int? SYear
  {
    get => this._SYear;
    set => this._SYear = value;
  }

  [PXDBInt(IsKey = true)]
  public override int? SMonth
  {
    get => this._SMonth;
    set => this._SMonth = value;
  }

  [PXDBInt(IsKey = true)]
  public override int? SDay
  {
    get => this._SDay;
    set => this._SDay = value;
  }

  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.siteID>
  {
  }

  public new abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ItemSalesHistD.sDate>
  {
  }

  public new abstract class sYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.sYear>
  {
  }

  public new abstract class sMonth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.sMonth>
  {
  }

  public new abstract class sDay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSalesHistD.sDay>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<INItemSalesHistD.qtyExcluded>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSalesHistD.qtyIssues>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSalesHistD.qtyLostSales>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSalesHistD.qtyPlanSales>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INItemSalesHistD.demandType1>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSalesHistD.demandType2>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSalesHistD.sQuater>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INItemSalesHistD.sDayOfWeek>((PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
