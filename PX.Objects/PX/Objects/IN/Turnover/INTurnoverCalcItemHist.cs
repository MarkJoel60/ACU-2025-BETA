// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INTurnoverCalcItemHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;
using System;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXHidden]
[PXProjection(typeof (SelectFromMirror<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<INSite>>, FbqlJoins.Cross<MasterFinPeriod>>, FbqlJoins.Inner<INItemCostHistRange>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemCostHistRange.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>>.And<BqlOperand<INItemCostHistRange.siteID, IBqlInt>.IsEqual<INSite.siteID>>>>, FbqlJoins.Left<INItemCostHist>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemCostHist.inventoryID, Equal<INItemCostHistRange.inventoryID>>>>, And<BqlOperand<INItemCostHist.costSubItemID, IBqlInt>.IsEqual<INItemCostHistRange.costSubItemID>>>, And<BqlOperand<INItemCostHist.costSiteID, IBqlInt>.IsEqual<INItemCostHistRange.costSiteID>>>, And<BqlOperand<INItemCostHist.accountID, IBqlInt>.IsEqual<INItemCostHistRange.accountID>>>, And<BqlOperand<INItemCostHist.subID, IBqlInt>.IsEqual<INItemCostHistRange.subID>>>>.And<BqlOperand<INItemCostHist.finPeriodID, IBqlString>.IsEqual<MasterFinPeriod.finPeriodID>>>>, FbqlJoins.Left<INItemCostHistLastActivePeriod>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MasterFinPeriod.finPeriodID, Equal<BqlField<INTurnoverCalc.fromPeriodID, IBqlString>.FromCurrent.Value>>>>, And<BqlOperand<INItemCostHist.finPeriodID, IBqlString>.IsNull>>, And<BqlOperand<INItemCostHistLastActivePeriod.inventoryID, IBqlInt>.IsEqual<INItemCostHistRange.inventoryID>>>, And<BqlOperand<INItemCostHistLastActivePeriod.costSubItemID, IBqlInt>.IsEqual<INItemCostHistRange.costSubItemID>>>, And<BqlOperand<INItemCostHistLastActivePeriod.costSiteID, IBqlInt>.IsEqual<INItemCostHistRange.costSiteID>>>, And<BqlOperand<INItemCostHistLastActivePeriod.accountID, IBqlInt>.IsEqual<INItemCostHistRange.accountID>>>>.And<BqlOperand<INItemCostHistLastActivePeriod.subID, IBqlInt>.IsEqual<INItemCostHistRange.subID>>>>>.LeftJoin<ItemCostHist>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ItemCostHist.inventoryID, Equal<INItemCostHistRange.inventoryID>>>>, And<BqlOperand<ItemCostHist.costSubItemID, IBqlInt>.IsEqual<INItemCostHistRange.costSubItemID>>>, And<BqlOperand<ItemCostHist.costSiteID, IBqlInt>.IsEqual<INItemCostHistRange.costSiteID>>>, And<BqlOperand<ItemCostHist.accountID, IBqlInt>.IsEqual<INItemCostHistRange.accountID>>>, And<BqlOperand<ItemCostHist.subID, IBqlInt>.IsEqual<INItemCostHistRange.subID>>>>.And<BqlOperand<ItemCostHist.finPeriodID, IBqlString>.IsEqual<INItemCostHistLastActivePeriod.lastActiveFinPeriodID>>>), Persistent = false)]
public class INTurnoverCalcItemHist : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true, BqlField = typeof (INItemCostHistRange.costSubItemID))]
  public virtual int? CostSubItemID { get; set; }

  [Site(IsKey = true, BqlField = typeof (INItemCostHistRange.costSiteID))]
  public virtual int? CostSiteID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INItemCostHistRange.accountID))]
  public virtual int? AccountID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INItemCostHistRange.subID))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBInt(BqlField = typeof (INSite.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INSite.siteID))]
  public virtual int? SiteID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (INItemCostHist.finPeriodID))]
  public virtual string CostHistFinPeriodID { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finBegQty))]
  public virtual Decimal? FinBegQty { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finYtdQty))]
  public virtual Decimal? FinYtdQty { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finBegCost))]
  public virtual Decimal? FinBegCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finYtdCost))]
  public virtual Decimal? FinYtdCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCOGS))]
  public virtual Decimal? FinPtdCOGS { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtySales))]
  public virtual Decimal? FinPtdQtySales { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCOGSCredits))]
  public virtual Decimal? FinPtdCOGSCredits { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtyCreditMemos))]
  public virtual Decimal? FinPtdQtyCreditMemos { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCostAMAssemblyOut))]
  public virtual Decimal? FinPtdCostAMAssemblyOut { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtyAMAssemblyOut))]
  public virtual Decimal? FinPtdQtyAMAssemblyOut { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCostAssemblyOut))]
  public virtual Decimal? FinPtdCostAssemblyOut { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtyAssemblyOut))]
  public virtual Decimal? FinPtdQtyAssemblyOut { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCostIssued))]
  public virtual Decimal? FinPtdCostIssued { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtyIssued))]
  public virtual Decimal? FinPtdQtyIssued { get; set; }

  [PXDBDecimal(4, BqlField = typeof (INItemCostHist.finPtdCostTransferOut))]
  public virtual Decimal? FinPtdCostTransferOut { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCostHist.finPtdQtyTransferOut))]
  public virtual Decimal? FinPtdQtyTransferOut { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (ItemCostHist.finPeriodID))]
  public virtual string LastActiveFinPeriodID { get; set; }

  [PXDBQuantity(BqlField = typeof (ItemCostHist.finYtdQty))]
  public virtual Decimal? LastFinYtdQty { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ItemCostHist.finYtdCost))]
  public virtual Decimal? LastFinYtdCost { get; set; }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.inventoryID>
  {
  }

  public abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTurnoverCalcItemHist.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPeriodID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.branchID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalcItemHist.siteID>
  {
  }

  public abstract class costHistFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcItemHist.costHistFinPeriodID>
  {
  }

  public abstract class finBegQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finBegQty>
  {
  }

  public abstract class finYtdQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finYtdQty>
  {
  }

  public abstract class finBegCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finBegCost>
  {
  }

  public abstract class finYtdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finYtdCost>
  {
  }

  public abstract class finPtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCOGS>
  {
  }

  public abstract class finPtdQtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtySales>
  {
  }

  public abstract class finPtdCOGSCredits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCOGSCredits>
  {
  }

  public abstract class finPtdQtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtyCreditMemos>
  {
  }

  public abstract class finPtdCostAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCostAMAssemblyOut>
  {
  }

  public abstract class finPtdQtyAMAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtyAMAssemblyOut>
  {
  }

  public abstract class finPtdCostAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCostAssemblyOut>
  {
  }

  public abstract class finPtdQtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtyAssemblyOut>
  {
  }

  public abstract class finPtdCostIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCostIssued>
  {
  }

  public abstract class finPtdQtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtyIssued>
  {
  }

  public abstract class finPtdCostTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdCostTransferOut>
  {
  }

  public abstract class finPtdQtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.finPtdQtyTransferOut>
  {
  }

  public abstract class lastActiveFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalcItemHist.lastActiveFinPeriodID>
  {
  }

  public abstract class lastFinYtdQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.lastFinYtdQty>
  {
  }

  public abstract class lastFinYtdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTurnoverCalcItemHist.lastFinYtdCost>
  {
  }
}
