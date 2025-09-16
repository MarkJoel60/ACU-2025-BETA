// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemSiteHistDay
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
[ItemSiteHistDay.Accumulator]
[PXDisableCloneAttributes]
public class ItemSiteHistDay : INItemSiteHistDay
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
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
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate(IsKey = true)]
  public override DateTime? SDate
  {
    get => this._SDate;
    set => this._SDate = value;
  }

  public new abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistDay.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistDay.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistDay.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHistDay.locationID>
  {
  }

  public new abstract class sDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ItemSiteHistDay.sDate>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
    {
      PXAccumulatorAttribute.RunningTotalRule[] runningTotalRuleArray = new PXAccumulatorAttribute.RunningTotalRule[2];
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer1 = PXAccumulatorAttribute.Run<INItemSiteHistDay.begQty>();
      runningTotalRuleArray[0] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer1).WithValueOf<INItemSiteHistDay.endQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer2 = PXAccumulatorAttribute.Run<INItemSiteHistDay.endQty>();
      runningTotalRuleArray[1] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer2).WithOwnValue();
      // ISSUE: explicit constructor call
      base.\u002Ector(runningTotalRuleArray);
    }
  }
}
