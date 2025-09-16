// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemCostHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;

[PXHidden]
[ItemCostHist.Accumulator]
[PXDisableCloneAttributes]
public class ItemCostHist : INItemCostHist
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
  public override int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true)]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt]
  [PXDefault]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.inventoryID>
  {
  }

  public new abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.costSiteID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.subID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCostHist.siteID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemCostHist.finPeriodID>
  {
  }

  public new abstract class finYtdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ItemCostHist.finYtdCost>
  {
  }

  public new abstract class finYtdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ItemCostHist.finYtdQty>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
    {
      PXAccumulatorAttribute.RunningTotalRule[] runningTotalRuleArray = new PXAccumulatorAttribute.RunningTotalRule[8];
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer1 = PXAccumulatorAttribute.Run<INItemCostHist.finBegQty>();
      runningTotalRuleArray[0] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer1).WithValueOf<ItemCostHist.finYtdQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer2 = PXAccumulatorAttribute.Run<ItemCostHist.finYtdQty>();
      runningTotalRuleArray[1] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer2).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer3 = PXAccumulatorAttribute.Run<INItemCostHist.finBegCost>();
      runningTotalRuleArray[2] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer3).WithValueOf<ItemCostHist.finYtdCost>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer4 = PXAccumulatorAttribute.Run<ItemCostHist.finYtdCost>();
      runningTotalRuleArray[3] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer4).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer5 = PXAccumulatorAttribute.Run<INItemCostHist.tranBegQty>();
      runningTotalRuleArray[4] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer5).WithValueOf<INItemCostHist.tranYtdQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer6 = PXAccumulatorAttribute.Run<INItemCostHist.tranYtdQty>();
      runningTotalRuleArray[5] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer6).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer7 = PXAccumulatorAttribute.Run<INItemCostHist.tranBegCost>();
      runningTotalRuleArray[6] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer7).WithValueOf<INItemCostHist.tranYtdCost>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer8 = PXAccumulatorAttribute.Run<INItemCostHist.tranYtdCost>();
      runningTotalRuleArray[7] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer8).WithOwnValue();
      // ISSUE: explicit constructor call
      base.\u002Ector(runningTotalRuleArray);
    }
  }
}
