// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory.ItemSiteHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;

[PXHidden]
[ItemSiteHist.Accumulator]
[PXDisableCloneAttributes]
public class ItemSiteHist : INItemSiteHist
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
  ItemSiteHist.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHist.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHist.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemSiteHist.locationID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemSiteHist.finPeriodID>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute()
    {
      PXAccumulatorAttribute.RunningTotalRule[] runningTotalRuleArray = new PXAccumulatorAttribute.RunningTotalRule[4];
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer1 = PXAccumulatorAttribute.Run<INItemSiteHist.finBegQty>();
      runningTotalRuleArray[0] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer1).WithValueOf<INItemSiteHist.finYtdQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer2 = PXAccumulatorAttribute.Run<INItemSiteHist.finYtdQty>();
      runningTotalRuleArray[1] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer2).WithOwnValue();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer3 = PXAccumulatorAttribute.Run<INItemSiteHist.tranBegQty>();
      runningTotalRuleArray[2] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer3).WithValueOf<INItemSiteHist.tranYtdQty>();
      PXAccumulatorAttribute.RunningTotalPairer runningTotalPairer4 = PXAccumulatorAttribute.Run<INItemSiteHist.tranYtdQty>();
      runningTotalRuleArray[3] = ((PXAccumulatorAttribute.RunningTotalPairer) ref runningTotalPairer4).WithOwnValue();
      // ISSUE: explicit constructor call
      base.\u002Ector(runningTotalRuleArray);
    }
  }
}
