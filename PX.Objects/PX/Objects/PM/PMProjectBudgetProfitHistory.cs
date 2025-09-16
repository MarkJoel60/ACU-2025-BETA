// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectBudgetProfitHistory
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
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A projection DAC with formulas for profit calculation in the project budget.
/// </summary>
[PXCacheName("PMProjectBudgetProfitHistory")]
[PXProjection(typeof (SelectFrom<PMProjectBudgetHistory, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<PMAccountGroup>.On<BqlOperand<PMProjectBudgetHistory.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>))]
[Serializable]
public class PMProjectBudgetProfitHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.ProjectID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMProjectBudgetHistory.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.TaskID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMProjectBudgetHistory.taskID))]
  public virtual int? TaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.AccountGroupID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMProjectBudgetHistory.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.Type" />
  [PXDBString(BqlField = typeof (PMAccountGroup.type))]
  public virtual 
  #nullable disable
  string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.ReportGroup" />
  [PXDBString(BqlField = typeof (PMAccountGroup.reportGroup))]
  public virtual string ReportGroup { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.ChangeOrderRefNbr" />
  [PXDBString(BqlField = typeof (PMProjectBudgetHistory.changeOrderRefNbr))]
  public virtual string ChangeOrderRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.CuryRevisedBudgetAmt" />
  [PXDBDecimal(BqlField = typeof (PMProjectBudgetHistory.curyRevisedBudgetAmt))]
  public virtual Decimal? CuryRevisedBudgetAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMProjectBudgetHistory.Date" />
  [PXDBDate(BqlField = typeof (PMProjectBudgetHistory.date))]
  public virtual DateTime? Date { get; set; }

  /// <summary>The original contract.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMProjectBudgetHistory.curyRevisedBudgetAmt, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProjectBudgetHistory.changeOrderRefNbr, Equal<ProjectBudgetHistoryChangeOrderRef.empty>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.income>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, Equal<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Original Contract Amount")]
  public virtual Decimal? CuryOriginalContract { get; set; }

  /// <summary>The original budget.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMProjectBudgetHistory.curyRevisedBudgetAmt, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProjectBudgetHistory.changeOrderRefNbr, Equal<ProjectBudgetHistoryChangeOrderRef.empty>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, NotEqual<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Original Cost Amount")]
  public virtual Decimal? CuryOriginalBudget { get; set; }

  /// <summary>The original profit.</summary>
  /// <value>
  /// The value is calculated by the formula: <see cref="P:PX.Objects.PM.PMProjectBudgetProfitHistory.CuryOriginalContract" /> - <see cref="P:PX.Objects.PM.PMProjectBudgetProfitHistory.CuryOriginalBudget" />.
  /// </value>
  [PXDecimal]
  [PXUIField(DisplayName = "Original Profit")]
  [PXDBCalced(typeof (Sub<PMProjectBudgetProfitHistory.curyOriginalContract, PMProjectBudgetProfitHistory.curyOriginalBudget>), typeof (Decimal))]
  public virtual Decimal? CuryOriginalProfit { get; set; }

  /// <summary>The revised contract.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMProjectBudgetHistory.curyRevisedBudgetAmt, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.income>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, Equal<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Revised Contract Amount")]
  public virtual Decimal? CuryRevisedContract { get; set; }

  /// <summary>The revised budget.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMProjectBudgetHistory.curyRevisedBudgetAmt, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, NotEqual<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Revised Cost Amount")]
  public virtual Decimal? CuryRevisedBudget { get; set; }

  /// <summary>The revised profit.</summary>
  /// <value>
  /// The value is calculated by the formula: <see cref="P:PX.Objects.PM.PMProjectBudgetProfitHistory.CuryRevisedContract" /> - <see cref="P:PX.Objects.PM.PMProjectBudgetProfitHistory.CuryRevisedBudget" />.
  /// </value>
  [PXDecimal]
  [PXUIField(DisplayName = "Revised Profit")]
  [PXDBCalced(typeof (Sub<PMProjectBudgetProfitHistory.curyRevisedContract, PMProjectBudgetProfitHistory.curyRevisedBudget>), typeof (Decimal))]
  public virtual Decimal? CuryRevisedProfit { get; set; }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProjectBudgetProfitHistory.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.accountGroupID>
  {
  }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.accountGroupType>
  {
  }

  public abstract class reportGroup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.reportGroup>
  {
  }

  public abstract class changeOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.changeOrderRefNbr>
  {
  }

  public abstract class curyRevisedBudgetAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyRevisedBudgetAmt>
  {
  }

  public abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.date>
  {
  }

  public abstract class curyOriginalContract : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyOriginalContract>
  {
  }

  public abstract class curyOriginalBudget : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyOriginalBudget>
  {
  }

  public abstract class curyOriginalProfit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyOriginalProfit>
  {
  }

  public abstract class curyRevisedContract : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyRevisedContract>
  {
  }

  public abstract class curyRevisedBudget : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyRevisedBudget>
  {
  }

  public abstract class curyRevisedProfit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProjectBudgetProfitHistory.curyOriginalProfit>
  {
  }
}
