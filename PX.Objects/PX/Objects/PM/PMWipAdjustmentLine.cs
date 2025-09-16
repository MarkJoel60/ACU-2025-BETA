// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWipAdjustmentLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CT;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a line of a project WIP adjustment transaction.
/// The records of this type are created and edited on the WIP Adjustment (PM305600) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectWipAdjustmentEntry" /> graph).
/// </summary>
[PXCacheName("Project WIP Adjustment Line")]
[PXPrimaryGraph(typeof (ProjectWipAdjustmentEntry))]
[Serializable]
public class PMWipAdjustmentLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, INotable
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>The transaction identifier.</summary>
  [PXParent(typeof (PMWipAdjustmentLine.FK.Document))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PMWipAdjustment.refNbr))]
  [PXUIField]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>The number of the transaction line.</summary>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMWipAdjustment.lineCntr))]
  [PXUIField]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> for which the WIP adjustment line is created.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault]
  [Project(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>), WarnIfCompleted = false, Required = true)]
  [PXUIField]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMTask">task</see> of the <see cref="T:PX.Objects.PM.PMProject">project</see> which will be linked to the GL transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMWipAdjustmentLine.projectID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMCostProjectionByDate">project cost projection</see> that the WIP adjustment line is based on.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RefNbr" /> field.
  /// </value>
  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.released, Equal<True>>>>, And<BqlOperand<PMCostProjectionByDate.projectID, IBqlInt>.IsEqual<BqlField<PMWipAdjustmentLine.projectID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PMCostProjectionByDate.projectionDate, IBqlDateTime>.IsLessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>, PMCostProjectionByDate>.SearchFor<PMCostProjectionByDate.refNbr>), new Type[] {typeof (PMCostProjectionByDate.refNbr), typeof (PMCostProjectionByDate.status), typeof (PMCostProjectionByDate.projectionDate)}, DescriptionField = typeof (PMCostProjectionByDate.description))]
  [PXUIField]
  public virtual string ProjectionRefNbr { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryAmount">Original Budgeted Amount</see> of the project revenue budget.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.originalRevenueAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Contract Amount", Enabled = false, Visible = false)]
  public virtual Decimal? CuryOriginalRevenueAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryAmount">Original Budgeted Amount</see> of the project revenue budget in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OriginalRevenueAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingRevenueChangeOrderAmountTotal">Pending CO Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.pendingRevenueChangeOrderAmount))]
  [PXUIField(DisplayName = "Pending CO Revenue", Enabled = false, Visible = false)]
  public virtual Decimal? CuryPendingRevenueChangeOrderAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingRevenueChangeOrderAmountTotal">Pending CO Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingRevenueChangeOrderAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal">Revised Budget Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.revisedRevenueBudgetedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Contract Amount", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRevisedRevenueBudgetedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal">Revised Budget Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedRevenueBudgetedAmount { get; set; }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryRevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOriginalRevenueAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.budgetedRevenueChangeOrderAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Revenue", Enabled = false, Visible = false)]
  public virtual Decimal? CuryBudgetedRevenueChangeOrderAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.projectionRefNbr), typeof (PMWipAdjustmentLine.curyOriginalRevenueAmount), typeof (PMWipAdjustmentLine.curyRevisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.curyPendingRevenueChangeOrderAmount)})] get
    {
      return new Decimal?(this.ProjectionRefNbr == null ? this.CuryRevisedRevenueBudgetedAmount.GetValueOrDefault() - this.CuryOriginalRevenueAmount.GetValueOrDefault() - this.CuryPendingRevenueChangeOrderAmount.GetValueOrDefault() : this.CuryRevisedRevenueBudgetedAmount.GetValueOrDefault() - this.CuryOriginalRevenueAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.RevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OriginalRevenueAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetedRevenueChangeOrderAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.projectionRefNbr), typeof (PMWipAdjustmentLine.originalRevenueAmount), typeof (PMWipAdjustmentLine.revisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.pendingRevenueChangeOrderAmount)})] get
    {
      return new Decimal?(this.ProjectionRefNbr == null ? this.RevisedRevenueBudgetedAmount.GetValueOrDefault() - this.OriginalRevenueAmount.GetValueOrDefault() - this.PendingRevenueChangeOrderAmount.GetValueOrDefault() : this.RevisedRevenueBudgetedAmount.GetValueOrDefault() - this.OriginalRevenueAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryAmount">Original Budgeted Amount</see> of the project cost budget.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.originalCostAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryOriginalCostAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryAmount">Original Budgeted Amount</see> of the project cost budget in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OriginalCostAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingChangeOrderAmountTotal">Pending CO Cost</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.pendingCostChangeOrderAmount))]
  [PXUIField(DisplayName = "Pending CO Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryPendingCostChangeOrderAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingChangeOrderAmountTotal">Pending CO Cost</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingCostChangeOrderAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBudgetedAmountTotal">Revised Budgeted Cost</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.revisedCostBudgetedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRevisedCostBudgetedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBudgetedAmountTotal">Revised Budgeted Cost</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedCostBudgetedAmount { get; set; }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryRevisedCostBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOriginalCostAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.budgetedCostChangeOrderAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryBudgetedCostChangeOrderAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.projectionRefNbr), typeof (PMWipAdjustmentLine.curyOriginalCostAmount), typeof (PMWipAdjustmentLine.curyRevisedCostBudgetedAmount), typeof (PMWipAdjustmentLine.curyPendingCostChangeOrderAmount)})] get
    {
      return new Decimal?(this.ProjectionRefNbr == null ? this.CuryRevisedCostBudgetedAmount.GetValueOrDefault() - this.CuryOriginalCostAmount.GetValueOrDefault() - this.CuryPendingCostChangeOrderAmount.GetValueOrDefault() : this.CuryRevisedCostBudgetedAmount.GetValueOrDefault() - this.CuryOriginalCostAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.RevisedCostBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OriginalCostAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetedCostChangeOrderAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.projectionRefNbr), typeof (PMWipAdjustmentLine.originalCostAmount), typeof (PMWipAdjustmentLine.revisedCostBudgetedAmount), typeof (PMWipAdjustmentLine.pendingCostChangeOrderAmount)})] get
    {
      return new Decimal?(this.ProjectionRefNbr == null ? this.RevisedCostBudgetedAmount.GetValueOrDefault() - this.OriginalCostAmount.GetValueOrDefault() - this.PendingCostChangeOrderAmount.GetValueOrDefault() : this.RevisedCostBudgetedAmount.GetValueOrDefault() - this.OriginalCostAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The sum of all project commitments with the date the same as or earlier than the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate">transaction's projected date</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.originalCommitmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Commitment Total", Enabled = false, Visible = false)]
  public virtual Decimal? CuryOriginalCommitmentAmount { get; set; }

  /// <summary>
  /// The sum of all project commitments with the date the same as or earlier than the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate">transaction's projected date</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OriginalCommitmentAmount { get; set; }

  /// <summary>
  /// The sum of all approved project commitments with approval date the same as or earlier than the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate">transaction's projected date</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.approvedCommitmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Approved Commitment Change Total", Enabled = false, Visible = false)]
  public virtual Decimal? CuryApprovedCommitmentAmount { get; set; }

  /// <summary>
  /// The sum of all approved project commitments with approval date the same as or earlier than the <see cref="P:PX.Objects.PM.PMWipAdjustment.ProjectionDate">transaction's projected date</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ApprovedCommitmentAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingCommitmentAmountTotal">Pending CO Commitments</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.pendingCommitmentAmount))]
  [PXUIField(DisplayName = "Pending Commitment Change Total", Enabled = false, Visible = false)]
  public virtual Decimal? CuryPendingCommitmentAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryPendingCommitmentAmountTotal">Pending CO Commitments</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingCommitmentAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryOriginalCommitmentAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryApprovedCommitmentAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.revisedCommitmentAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Commitment Total", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRevisedCommitmentAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyOriginalCommitmentAmount), typeof (PMWipAdjustmentLine.curyApprovedCommitmentAmount), typeof (PMWipAdjustmentLine.curyPendingCommitmentAmount)})] get
    {
      Decimal? commitmentAmount = this.CuryOriginalCommitmentAmount;
      Decimal valueOrDefault1 = commitmentAmount.GetValueOrDefault();
      commitmentAmount = this.CuryApprovedCommitmentAmount;
      Decimal valueOrDefault2 = commitmentAmount.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      commitmentAmount = this.CuryPendingCommitmentAmount;
      Decimal valueOrDefault3 = commitmentAmount.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.OriginalCommitmentAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.ApprovedCommitmentAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedCommitmentAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.originalCommitmentAmount), typeof (PMWipAdjustmentLine.approvedCommitmentAmount), typeof (PMWipAdjustmentLine.pendingCommitmentAmount)})] get
    {
      Decimal? commitmentAmount = this.OriginalCommitmentAmount;
      Decimal valueOrDefault1 = commitmentAmount.GetValueOrDefault();
      commitmentAmount = this.ApprovedCommitmentAmount;
      Decimal valueOrDefault2 = commitmentAmount.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      commitmentAmount = this.PendingCommitmentAmount;
      Decimal valueOrDefault3 = commitmentAmount.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedAmountTotal">Projected Cost at Completion</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.projectedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion", Enabled = false, Visible = false)]
  public virtual Decimal? CuryProjectedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedAmountTotal">Projected Cost at Completion</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProjectedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedMarginTotal">Projected Margin</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.projectedMarginAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Est. Gross Profit", Enabled = false, Visible = false)]
  public virtual Decimal? CuryProjectedMarginAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedMarginTotal">Projected Margin</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProjectedMarginAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectedMarginPctTotal">Projected Margin (%)</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Est. Margin (%)", Enabled = false, Visible = false)]
  public virtual Decimal? ProjectedMarginPct { get; set; }

  /// <summary>
  /// The sum of the expense <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount">project transaction amount</see> for the <see cref="P:PX.Objects.PM.PMWipAdjustment.FinPeriodID">transaction's financial period</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.periodCostAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Period Costs", Enabled = false, Visible = false)]
  public virtual Decimal? CuryPeriodCostAmount { get; set; }

  /// <summary>Period costs in the transaction's base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PeriodCostAmount { get; set; }

  /// <summary>The percentage of the budget used.</summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "% Budget Used", Enabled = false, Visible = false)]
  public virtual Decimal? BudgetUsedPct
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyPeriodCostAmount), typeof (PMWipAdjustmentLine.curyProjectedAmount)})] get
    {
      if (!(this.CuryProjectedAmount.GetValueOrDefault() != 0M))
        return new Decimal?();
      Decimal? nullable = this.CuryPeriodCostAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryProjectedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 / valueOrDefault2 * 100M);
    }
  }

  /// <summary>
  /// The sum of all <see cref="P:PX.Objects.PM.PMTran.ProjectCuryAmount">project transaction amounts</see> of the AR functional area for the <see cref="P:PX.Objects.PM.PMWipAdjustment.FinPeriodID">transaction's financial period</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.periodBillingAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Period Billings", Enabled = false, Visible = false)]
  public virtual Decimal? CuryPeriodBillingAmount { get; set; }

  /// <summary>Period billings in the transaction's base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PeriodBillingAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryActualAmountTotal">Actual Cost to Date</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Costs to Period", Enabled = false, Visible = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryActualAmountTotal">Actual Cost to Date</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CompletedPctTotal">Completed (%)</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)", Enabled = false, Visible = false)]
  public virtual Decimal? CompletedPct { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevenueExpectedAmountTotal">Expected Current Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.revenueExpectedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Earned Revenue", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRevenueExpectedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevenueExpectedAmountTotal">Expected Current Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevenueExpectedAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBilledRevenueAmountTotal">Billed Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see>.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.billedRevenueAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billings to Period", Enabled = false, Visible = false)]
  public virtual Decimal? CuryBilledRevenueAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBilledRevenueAmountTotal">Billed Revenue</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledRevenueAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryOverbillingAmountTotal">Overbilling or Underbilling</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> if it is greater than 0; otherwise, 0.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.overbillingAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overbilling Amount", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<PMWipAdjustment.curyOverbillingAmount>), ForceAggregateRecalculation = false)]
  public virtual Decimal? CuryOverbillingAmount { get; set; }

  /// <summary>Overbilling in the transaction's base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OverbillingAmount { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryOverbillingAmountTotal">Overbilling or Underbilling</see> of the corresponding <see cref="T:PX.Objects.PM.PMCostProjectionByDate">cost projection</see> multiplied by –1, if it is less than 0; otherwise, 0.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.underbillingAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Underbilling Amount", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<PMWipAdjustment.curyUnderbillingAmount>), ForceAggregateRecalculation = false)]
  public virtual Decimal? CuryUnderbillingAmount { get; set; }

  /// <summary>Underbilling in the transaction's base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnderbillingAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryBilledRevenueAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryActualAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.grossProfitAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Profit", Enabled = false, Visible = false)]
  public virtual Decimal? CuryGrossProfitAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyRevenueExpectedAmount), typeof (PMWipAdjustmentLine.curyActualAmount)})] get
    {
      Decimal? nullable = this.CuryRevenueExpectedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryActualAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.BilledRevenueAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.ActualAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GrossProfitAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.revenueExpectedAmount), typeof (PMWipAdjustmentLine.actualAmount)})] get
    {
      Decimal? nullable = this.RevenueExpectedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.ActualAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>The percentage of margin.</summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Margin (%)", Enabled = false, Visible = false)]
  public virtual Decimal? MarginPct
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyGrossProfitAmount), typeof (PMWipAdjustmentLine.curyRevenueExpectedAmount)})] get
    {
      if (!(this.CuryRevenueExpectedAmount.GetValueOrDefault() != 0M))
        return new Decimal?();
      Decimal? nullable = this.CuryGrossProfitAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryRevenueExpectedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 / valueOrDefault2 * 100M);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryRevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryRevenueExpectedAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.revenueBacklogAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revenue Backlog", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRevenueBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyRevisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.curyRevenueExpectedAmount)})] get
    {
      Decimal? nullable = this.CuryRevisedRevenueBudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryRevenueExpectedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.RevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.RevenueExpectedAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevenueBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.revisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.revenueExpectedAmount)})] get
    {
      Decimal? nullable = this.RevisedRevenueBudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RevenueExpectedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryProjectedMarginAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryGrossProfitAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.grossProfitBacklogAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Profit Backlog", Enabled = false, Visible = false)]
  public virtual Decimal? CuryGrossProfitBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyProjectedMarginAmount), typeof (PMWipAdjustmentLine.curyGrossProfitAmount)})] get
    {
      Decimal? nullable = this.CuryProjectedMarginAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryGrossProfitAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.ProjectedMarginAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.GrossProfitAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GrossProfitBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.projectedMarginAmount), typeof (PMWipAdjustmentLine.grossProfitAmount)})] get
    {
      Decimal? nullable = this.ProjectedMarginAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.GrossProfitAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryRevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.CuryBilledRevenueAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.remainingContractgAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Remaining Contract", Enabled = false, Visible = false)]
  public virtual Decimal? CuryRemainingContractgAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.curyRevisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.curyBilledRevenueAmount)})] get
    {
      Decimal? nullable = this.CuryRevisedRevenueBudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryBilledRevenueAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.RevisedRevenueBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMWipAdjustmentLine.BilledRevenueAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RemainingContractgAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMWipAdjustmentLine.revisedRevenueBudgetedAmount), typeof (PMWipAdjustmentLine.billedRevenueAmount)})] get
    {
      Decimal? nullable = this.RevisedRevenueBudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.BilledRevenueAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The resulting overbilling adjustment that is used in the GL adjusting transactions.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.overbillingAdjustmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overbilling Adjustment")]
  [PXFormula(null, typeof (SumCalc<PMWipAdjustment.curyOverbillingAdjustmentAmount>), ForceAggregateRecalculation = false)]
  public virtual Decimal? CuryOverbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The overbilling adjustment in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OverbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The resulting underbilling adjustment that is used in the GL adjusting transactions.
  /// </summary>
  [PXDBCurrency(typeof (PMWipAdjustment.curyInfoID), typeof (PMWipAdjustmentLine.underbillingAdjustmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Underbilling Adjustment")]
  [PXFormula(null, typeof (SumCalc<PMWipAdjustment.curyUnderbillingAdjustmentAmount>), ForceAggregateRecalculation = false)]
  public virtual Decimal? CuryUnderbillingAdjustmentAmount { get; set; }

  /// <summary>
  /// The underbilling adjustment in the transaction's base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnderbillingAdjustmentAmount { get; set; }

  /// <summary>The overbilling account.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.liability>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  public virtual int? OverbillingAccountID { get; set; }

  /// <summary>The overbilling subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustmentLine.overbillingAccountID), typeof (PMWipAdjustment.branchID), true)]
  public virtual int? OverbillingSubID { get; set; }

  /// <summary>The underbilling account.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.asset>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  public virtual int? UnderbillingAccountID { get; set; }

  /// <summary>The underbilling subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustmentLine.underbillingAccountID), typeof (PMWipAdjustment.branchID), true)]
  public virtual int? UnderbillingSubID { get; set; }

  /// <summary>The revenue account.</summary>
  [Account(typeof (PMWipAdjustment.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.type, Equal<AccountType.income>, And<PX.Objects.GL.Account.isCashAccount, NotEqual<True>>>>>>))]
  public virtual int? RevenueAccountID { get; set; }

  /// <summary>The revenue subaccount.</summary>
  [SubAccount(typeof (PMWipAdjustmentLine.revenueAccountID), typeof (PMWipAdjustment.branchID), true)]
  public virtual int? RevenueSubID { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that change orders with the Pending Approval status are included in calculations.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (PMWipAdjustment.includePendingChangeOrders))]
  [PXUIField(DisplayName = "Pending CO Included")]
  public virtual bool? IncludePendingChangeOrders { get; set; }

  /// <exclude />
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  /// <exclude />
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<PMWipAdjustmentLine>.By<PMWipAdjustmentLine.refNbr, PMWipAdjustmentLine.lineNbr>
  {
    public static PMWipAdjustmentLine Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMWipAdjustmentLine) PrimaryKeyOf<PMWipAdjustmentLine>.By<PMWipAdjustmentLine.refNbr, PMWipAdjustmentLine.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<PMWipAdjustment>.By<PMWipAdjustment.refNbr>.ForeignKeyOf<PMWipAdjustmentLine>.By<PMWipAdjustmentLine.refNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMWipAdjustmentLine>.By<PMWipAdjustmentLine.projectID>
    {
    }

    public class Projection : 
      PrimaryKeyOf<PMCostProjectionByDate>.By<PMCostProjectionByDate.refNbr>.ForeignKeyOf<PMWipAdjustmentLine>.By<PMWipAdjustmentLine.projectionRefNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipAdjustmentLine.selected>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipAdjustmentLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustmentLine.lineNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustmentLine.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.projectTaskID>
  {
  }

  public abstract class projectionRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustmentLine.projectionRefNbr>
  {
  }

  public abstract class curyOriginalRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyOriginalRevenueAmount>
  {
  }

  public abstract class originalRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.originalRevenueAmount>
  {
  }

  public abstract class curyPendingRevenueChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyPendingRevenueChangeOrderAmount>
  {
  }

  public abstract class pendingRevenueChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.pendingRevenueChangeOrderAmount>
  {
  }

  public abstract class curyRevisedRevenueBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRevisedRevenueBudgetedAmount>
  {
  }

  public abstract class revisedRevenueBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.revisedRevenueBudgetedAmount>
  {
  }

  public abstract class curyBudgetedRevenueChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyBudgetedRevenueChangeOrderAmount>
  {
  }

  public abstract class budgetedRevenueChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.budgetedRevenueChangeOrderAmount>
  {
  }

  public abstract class curyOriginalCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyOriginalCostAmount>
  {
  }

  public abstract class originalCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.originalCostAmount>
  {
  }

  public abstract class curyPendingCostChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyPendingCostChangeOrderAmount>
  {
  }

  public abstract class pendingCostChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.pendingCostChangeOrderAmount>
  {
  }

  public abstract class curyRevisedCostBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRevisedCostBudgetedAmount>
  {
  }

  public abstract class revisedCostBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.revisedCostBudgetedAmount>
  {
  }

  public abstract class curyBudgetedCostChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyBudgetedCostChangeOrderAmount>
  {
  }

  public abstract class budgetedCostChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.budgetedCostChangeOrderAmount>
  {
  }

  public abstract class curyOriginalCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyOriginalCommitmentAmount>
  {
  }

  public abstract class originalCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.originalCommitmentAmount>
  {
  }

  public abstract class curyApprovedCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyApprovedCommitmentAmount>
  {
  }

  public abstract class approvedCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.approvedCommitmentAmount>
  {
  }

  public abstract class curyPendingCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyPendingCommitmentAmount>
  {
  }

  public abstract class pendingCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.pendingCommitmentAmount>
  {
  }

  public abstract class curyRevisedCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRevisedCommitmentAmount>
  {
  }

  public abstract class revisedCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.revisedCommitmentAmount>
  {
  }

  public abstract class curyProjectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyProjectedAmount>
  {
  }

  public abstract class projectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.projectedAmount>
  {
  }

  public abstract class curyProjectedMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyProjectedMarginAmount>
  {
  }

  public abstract class projectedMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.projectedMarginAmount>
  {
  }

  public abstract class projectedMarginPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.projectedMarginPct>
  {
  }

  public abstract class curyPeriodCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyPeriodCostAmount>
  {
  }

  public abstract class periodCostAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.periodCostAmount>
  {
  }

  public abstract class budgetUsedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.budgetUsedPct>
  {
  }

  public abstract class curyPeriodBillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyPeriodBillingAmount>
  {
  }

  public abstract class periodBillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.periodBillingAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.actualAmount>
  {
  }

  public abstract class completedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.completedPct>
  {
  }

  public abstract class curyRevenueExpectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRevenueExpectedAmount>
  {
  }

  public abstract class revenueExpectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.revenueExpectedAmount>
  {
  }

  public abstract class curyBilledRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyBilledRevenueAmount>
  {
  }

  public abstract class billedRevenueAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.billedRevenueAmount>
  {
  }

  public abstract class curyOverbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyOverbillingAmount>
  {
  }

  public abstract class overbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.overbillingAmount>
  {
  }

  public abstract class curyUnderbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyUnderbillingAmount>
  {
  }

  public abstract class underbillingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.underbillingAmount>
  {
  }

  public abstract class curyGrossProfitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyGrossProfitAmount>
  {
  }

  public abstract class grossProfitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.grossProfitAmount>
  {
  }

  public abstract class marginPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.marginPct>
  {
  }

  public abstract class curyRevenueBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRevenueBacklogAmount>
  {
  }

  public abstract class revenueBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.revenueBacklogAmount>
  {
  }

  public abstract class curyGrossProfitBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyGrossProfitBacklogAmount>
  {
  }

  public abstract class grossProfitBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.grossProfitBacklogAmount>
  {
  }

  public abstract class curyRemainingContractgAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyRemainingContractgAmount>
  {
  }

  public abstract class remainingContractgAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.remainingContractgAmount>
  {
  }

  public abstract class curyOverbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyOverbillingAdjustmentAmount>
  {
  }

  public abstract class overbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.overbillingAdjustmentAmount>
  {
  }

  public abstract class curyUnderbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.curyUnderbillingAdjustmentAmount>
  {
  }

  public abstract class underbillingAdjustmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipAdjustmentLine.underbillingAdjustmentAmount>
  {
  }

  public abstract class overbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.overbillingAccountID>
  {
  }

  public abstract class overbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.overbillingSubID>
  {
  }

  public abstract class underbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.underbillingAccountID>
  {
  }

  public abstract class underbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.underbillingSubID>
  {
  }

  public abstract class revenueAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWipAdjustmentLine.revenueAccountID>
  {
  }

  public abstract class revenueSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipAdjustmentLine.revenueSubID>
  {
  }

  public abstract class includePendingChangeOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMWipAdjustmentLine.includePendingChangeOrders>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWipAdjustmentLine.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMWipAdjustmentLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustmentLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipAdjustmentLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWipAdjustmentLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWipAdjustmentLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipAdjustmentLine.lastModifiedDateTime>
  {
  }
}
