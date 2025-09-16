// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipCostProjectionBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("PM WIP Cost Projection Budget")]
[PXProjection(typeof (SelectFrom<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.LeftJoin<PMWipCostProjection>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectID, Equal<PMWipCostProjection.projectID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.costCodeID, Equal<PMWipCostProjection.costCodeID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.accountGroupID, Equal<PMWipCostProjection.accountGroupID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectTaskID, Equal<PMWipCostProjection.taskID>>>>>.And<BqlOperand<PMBudget.inventoryID, IBqlInt>.IsEqual<PMWipCostProjection.inventoryID>>>>>>))]
[Serializable]
public class PMWipCostProjectionBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMBudget.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(1, BqlField = typeof (PMBudget.type))]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyAmount))]
  public virtual Decimal? CuryAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.income>>, PMWipCostProjectionBudget.curyAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? OriginalContractAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>>, PMWipCostProjectionBudget.curyAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? OriginalCostAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyChangeOrderAmount))]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.income>>, PMWipCostProjectionBudget.curyChangeOrderAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ChangeOrderContractAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>>, PMWipCostProjectionBudget.curyChangeOrderAmount>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ChangeOrderCostAmount { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCostToComplete))]
  public virtual Decimal? CuryCostToComplete { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>>, PMWipCostProjectionBudget.curyCostToComplete>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CostToComplete { get; set; }

  [PXDBDecimal(1, BqlField = typeof (PMBudget.curyCostProjectionCostAtCompletion))]
  public virtual Decimal? CostProjectionCostAtCompletion { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>>, PMBudget.curyCostProjectionCostToComplete>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CostProjectionCostToComplete { get; set; }

  [PXDBDecimal(2, BqlField = typeof (PMBudget.curyCommittedOrigAmount))]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  [PXDecimal(2)]
  [PXDBCalced(typeof (Sub<PMBudget.curyCommittedAmount, PMBudget.curyCommittedOrigAmount>), typeof (Decimal))]
  public virtual Decimal? CuryCommittedCOAmount { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>, And<PMWipCostProjection.projectID, IsNotNull>>, PMBudget.curyCostProjectionCostAtCompletion, Case<Where<PMWipCostProjectionBudget.type, Equal<AccountType.expense>>, PMBudget.curyRevisedAmount>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? ProjectedCostAtCompletion { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCostProjectionBudget.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipCostProjectionBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipCostProjectionBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipCostProjectionBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipCostProjectionBudget.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipCostProjectionBudget.type>
  {
  }

  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.curyAmount>
  {
  }

  public abstract class originalContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.originalContractAmount>
  {
  }

  public abstract class originalCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.originalCostAmount>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderContractAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.changeOrderContractAmount>
  {
  }

  public abstract class changeOrderCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.changeOrderCostAmount>
  {
  }

  public abstract class curyCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.curyCostToComplete>
  {
  }

  public abstract class costToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.costToComplete>
  {
  }

  public abstract class costProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.costProjectionCostAtCompletion>
  {
  }

  public abstract class costProjectionCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.costProjectionCostToComplete>
  {
  }

  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.curyCommittedOrigAmount>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.curyCommittedCOAmount>
  {
  }

  public abstract class projectedCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipCostProjectionBudget.projectedCostAtCompletion>
  {
  }
}
