// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemCustSalesHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;

[PXHidden]
[ItemCustSalesHist.Accumulator]
[PXDisableCloneAttributes]
public class ItemCustSalesHist : INItemCustSalesHist
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
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
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
  ItemCustSalesHist.inventoryID>
  {
  }

  public new abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ItemCustSalesHist.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesHist.costSiteID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemCustSalesHist.bAccountID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemCustSalesHist.finPeriodID>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
    {
      PXAccumulatorAttribute.RunningTotalRule[] runningTotalRuleArray = new PXAccumulatorAttribute.RunningTotalRule[18];
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer1 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdSales>();
      runningTotalRuleArray[0] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer1).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer2 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdCreditMemos>();
      runningTotalRuleArray[1] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer2).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer3 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdDropShipSales>();
      runningTotalRuleArray[2] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer3).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer4 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdCOGS>();
      runningTotalRuleArray[3] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer4).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer5 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdCOGSCredits>();
      runningTotalRuleArray[4] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer5).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer6 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdCOGSDropShips>();
      runningTotalRuleArray[5] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer6).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer7 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdQtySales>();
      runningTotalRuleArray[6] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer7).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer8 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdQtyCreditMemos>();
      runningTotalRuleArray[7] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer8).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer9 = PXAccumulatorAttribute.Run<INItemCustSalesHist.finYtdQtyDropShipSales>();
      runningTotalRuleArray[8] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer9).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer10 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdSales>();
      runningTotalRuleArray[9] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer10).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer11 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdCreditMemos>();
      runningTotalRuleArray[10] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer11).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer12 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdDropShipSales>();
      runningTotalRuleArray[11] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer12).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer13 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdCOGS>();
      runningTotalRuleArray[12] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer13).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer14 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdCOGSCredits>();
      runningTotalRuleArray[13] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer14).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer15 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdCOGSDropShips>();
      runningTotalRuleArray[14] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer15).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer16 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdQtySales>();
      runningTotalRuleArray[15] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer16).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer17 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdQtyCreditMemos>();
      runningTotalRuleArray[16 /*0x10*/] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer17).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer18 = PXAccumulatorAttribute.Run<INItemCustSalesHist.tranYtdQtyDropShipSales>();
      runningTotalRuleArray[17] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer18).WithOwnValue();
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
      ItemCustSalesHist itemCustSalesHist = (ItemCustSalesHist) row;
      columns.RestrictPast<ItemCustSalesHist.finPeriodID>((PXComp) 3, (object) (itemCustSalesHist.FinPeriodID.Substring(0, 4) + "01"));
      columns.RestrictFuture<ItemCustSalesHist.finPeriodID>((PXComp) 5, (object) (itemCustSalesHist.FinPeriodID.Substring(0, 4) + "99"));
      return true;
    }
  }
}
