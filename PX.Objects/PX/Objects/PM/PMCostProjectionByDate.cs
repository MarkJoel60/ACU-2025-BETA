// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostProjectionByDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Contains the main properties of a date-sensitive cost projection.
/// Cost projections are used for tracking the project costs during project completion
/// in comparison to the initially estimated costs.
/// The records of this type are created and edited through the Cost Projection By Date (PM305500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectCostProjectionByDateEntry" /> graph).
/// </summary>
[PXCacheName("Cost Projection By Date")]
[PXPrimaryGraph(typeof (ProjectCostProjectionByDateEntry))]
[Serializable]
public class PMCostProjectionByDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  /// <summary>The projection identifier.</summary>
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMCostProjectionByDate.projectID>>>>.Where<MatchUserFor<PMProject>>, PMCostProjectionByDate>.SearchFor<PMCostProjectionByDate.refNbr>), new Type[] {typeof (PMCostProjectionByDate.refNbr), typeof (PMCostProjectionByDate.status), typeof (PMCostProjectionByDate.projectID), typeof (PMCostProjectionByDate.projectionDate), typeof (PMCostProjectionByDate.groupByProjectTaskID), typeof (PMCostProjectionByDate.groupByAccountGroupID), typeof (PMCostProjectionByDate.groupByInventoryID), typeof (PMCostProjectionByDate.groupByCostCodeID)})]
  [AutoNumber(typeof (Search<PMSetup.costProjectionNumbering>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> for which the cost projection revision is created.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault]
  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false, Required = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>The status of the cost projection revision.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.ProjectCostProjectionByDateStatus.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ProjectCostProjectionByDateStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status { get; set; }

  /// <summary>
  /// The date when the cost projection document was created.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", FieldClass = "Construction")]
  public virtual DateTime? Date { get; set; }

  /// <summary>
  /// The date the cost projection document was projected to.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Projection Date")]
  public virtual DateTime? ProjectionDate { get; set; }

  /// <summary>The date the cost projection document is actual till.</summary>
  [PXDBDate]
  public virtual DateTime? ActualTillDate { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the document is approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the document is rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the document is released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  /// <summary>The description of the document.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the project budget data should be grouped by a project task.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Project Task")]
  public virtual bool? GroupByProjectTaskID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the project budget data should be grouped by an account group.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Account Group")]
  public virtual bool? GroupByAccountGroupID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the project budget data should be grouped by an inventory item.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual bool? GroupByInventoryID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the project budget data should be grouped by cost code.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public virtual bool? GroupByCostCodeID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether pending change orders are included in calculations.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (PMSetup.includePendingChangeOrdersInCostProjections))]
  [PXUIField(DisplayName = "Include Pending CO in Calculations")]
  public virtual bool? IncludePendingChangeOrders { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the project budget will be updated when the document is released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update Project Budget")]
  public virtual bool? UpdateProjectBudget { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryAmountToComplete" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.amountToCompleteTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost to Complete", Enabled = false)]
  public virtual Decimal? CuryAmountToCompleteTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.AmountToComplete" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmountToCompleteTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryProjectedAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.projectedAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CuryProjectedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.ProjectedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProjectedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryBudgetedAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.budgetedAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Cost", Enabled = false)]
  public virtual Decimal? CuryBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.BudgetedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryOriginalBudgetedAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.originalBudgetedAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOriginalBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.OriginalBudgetedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OriginalBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryRevisedBudgetedAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.revisedBudgetedAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRevisedBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.RevisedBudgetedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryChangeOrderAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.changeOrderAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.ChangeOrderAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryPendingChangeOrderAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.pendingChangeOrderAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending CO Cost", Enabled = false)]
  public virtual Decimal? CuryPendingChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.PendingChangeOrderAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The total amount of the pending project revenue of change orders.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.pendingRevenueChangeOrderAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending CO Revenue", Enabled = false)]
  public virtual Decimal? CuryPendingRevenueChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The total amount of the pending project revenue of change orders. The amount is in base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingRevenueChangeOrderAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryActualAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.actualAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Cost to Date", Enabled = false)]
  public virtual Decimal? CuryActualAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.ActualAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryCommitmentOpenAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.commitmentOpenAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Cost", Enabled = false)]
  public virtual Decimal? CuryCommitmentOpenAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CommitmentOpenAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommitmentOpenAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryPendingCommitmentAmount" />.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.pendingCommitmentAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending CO Commitments", Enabled = false)]
  public virtual Decimal? CuryPendingCommitmentAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.PendingCommitmentAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingCommitmentAmountTotal { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryActualAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryCommitmentOpenAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.completedAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Anticipated Cost", Enabled = false)]
  public virtual Decimal? CuryCompletedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyActualAmountTotal), typeof (PMCostProjectionByDate.curyCommitmentOpenAmountTotal), typeof (PMCostProjectionByDate.curyPendingCommitmentAmountTotal)})] get
    {
      Decimal? nullable = this.CuryActualAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryCommitmentOpenAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryPendingCommitmentAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ActualAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CommitmentOpenAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CompletedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.actualAmountTotal), typeof (PMCostProjectionByDate.commitmentOpenAmountTotal)})] get
    {
      Decimal? nullable = this.ActualAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommitmentOpenAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 + valueOrDefault2);
    }
  }

  /// <summary>
  /// The revised budget revenue, which consists of the original budgeted revenue and
  /// the revenue of all released change orders with the date before or equal to <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectionDate" />.
  /// If change order workflow is not activated, the field contains the revised budget from the project revenue budget.
  /// The revised budget revenue is in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.revisedRevenueBudgetedAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budget Revenue", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CuryRevisedRevenueBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The revised budget revenue, which consists of the original budgeted revenue and
  /// the revenue of all released change orders with the date before or equal to <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectionDate" />.
  /// If change order workflow is not activated, the field contains the revised budget from the project revenue budget.
  /// The revised budget revenue is in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedRevenueBudgetedAmountTotal { get; set; }

  /// <summary>
  /// The total amount of released project transactions with AR module and the date before or equal to <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectionDate" />.
  /// The amount is in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.billedRevenueAmountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Revenue", Enabled = false)]
  public virtual Decimal? CuryBilledRevenueAmountTotal { get; set; }

  /// <summary>
  /// The total amount of released project transactions with AR module and the date before or equal to <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectionDate" />.
  /// The amount is in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledRevenueAmountTotal { get; set; }

  /// <summary>The percent of project completion.</summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)", Enabled = false)]
  public virtual Decimal? CompletedPctTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyProjectedAmountTotal), typeof (PMCostProjectionByDate.curyActualAmountTotal)})] get
    {
      if (!(this.CuryProjectedAmountTotal.GetValueOrDefault() != 0M))
        return new Decimal?();
      Decimal? nullable = this.CuryActualAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryProjectedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 / valueOrDefault2 * 100M);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryActualAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.budgetBacklogAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Budget Backlog", Enabled = false)]
  public virtual Decimal? CuryBudgetBacklogAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyActualAmountTotal), typeof (PMCostProjectionByDate.curyPendingChangeOrderAmountTotal)})] get
    {
      Decimal? nullable = this.CuryBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryPendingChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryActualAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.BudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ActualAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetBacklogAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.budgetedAmountTotal), typeof (PMCostProjectionByDate.actualAmountTotal)})] get
    {
      Decimal? nullable = this.BudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.ActualAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBilledRevenueAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.revenueBudgetBacklogAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revenue Budget Backlog", Enabled = false)]
  public virtual Decimal? CuryRevenueBudgetBacklogAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyRevisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal), typeof (PMCostProjectionByDate.curyBilledRevenueAmountTotal)})] get
    {
      Decimal? nullable = this.CuryRevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryPendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryBilledRevenueAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RevisedRevenueBudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.BilledRevenueAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevenueBudgetBacklogAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.revisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.pendingRevenueChangeOrderAmountTotal), typeof (PMCostProjectionByDate.billedRevenueAmountTotal)})] get
    {
      Decimal? nullable = this.RevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.PendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.BilledRevenueAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The product of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CompletedPctTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedBudgetedAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.expectedAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExpectedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.completedPctTotal), typeof (PMCostProjectionByDate.curyRevisedBudgetedAmountTotal)})] get
    {
      Decimal? nullable = this.CompletedPctTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryRevisedBudgetedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 * valueOrDefault2 / 100M);
    }
  }

  /// <summary>
  /// The product of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CompletedPctTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RevisedBudgetedAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExpectedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.completedPctTotal), typeof (PMCostProjectionByDate.revisedBudgetedAmountTotal)})] get
    {
      Decimal? nullable = this.CompletedPctTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RevisedBudgetedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 * valueOrDefault2 / 100M);
    }
  }

  /// <summary>
  /// The product of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CompletedPctTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.revenueExpectedAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Expected Current Revenue", Enabled = false)]
  public virtual Decimal? CuryRevenueExpectedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.completedPctTotal), typeof (PMCostProjectionByDate.curyRevisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal)})] get
    {
      Decimal? nullable = this.CompletedPctTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryRevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      nullable = this.CuryPendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault2 + valueOrDefault3;
      return new Decimal?(valueOrDefault1 * num / 100M);
    }
  }

  /// <summary>
  /// The product of <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CompletedPctTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RevisedRevenueBudgetedAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevenueExpectedAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.completedPctTotal), typeof (PMCostProjectionByDate.revisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.pendingRevenueChangeOrderAmountTotal)})] get
    {
      Decimal? nullable = this.CompletedPctTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      nullable = this.PendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault2 + valueOrDefault3;
      return new Decimal?(valueOrDefault1 * num / 100M);
    }
  }

  /// <summary>
  /// The performance percent, which is the result of dividing <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryActualAmountTotal" /> by <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBudgetedAmountTotal" />, multiplied by 100%.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Performance (%)", Enabled = false)]
  public virtual Decimal? PerformanceTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyActualAmountTotal), typeof (PMCostProjectionByDate.curyBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyPendingChangeOrderAmountTotal)})] get
    {
      Decimal? nullable1 = this.CuryBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryPendingChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      if (!(valueOrDefault1 + valueOrDefault2 != 0M))
        return new Decimal?();
      Decimal? nullable2 = this.CuryActualAmountTotal;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryBudgetedAmountTotal;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPendingChangeOrderAmountTotal;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal num = valueOrDefault4 + valueOrDefault5;
      return new Decimal?(valueOrDefault3 / num * 100M);
    }
  }

  /// <summary>
  /// The anticipated performance percent, which is the result of dividing <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryCompletedAmountTotal" /> by <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBudgetedAmountTotal" />, multiplied by 100%.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Anticipated Performance (%)", Enabled = false)]
  public virtual Decimal? AnticipatedPerformanceTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyCompletedAmountTotal), typeof (PMCostProjectionByDate.curyBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyPendingChangeOrderAmountTotal)})] get
    {
      Decimal? nullable1 = this.CuryBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryPendingChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      if (!(valueOrDefault1 + valueOrDefault2 != 0M))
        return new Decimal?();
      Decimal? nullable2 = this.CuryCompletedAmountTotal;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryBudgetedAmountTotal;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPendingChangeOrderAmountTotal;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal num = valueOrDefault4 + valueOrDefault5;
      return new Decimal?(valueOrDefault3 / num * 100M);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryBilledRevenueAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevenueExpectedAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.overbillingAmountTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overbilling or Underbilling", Enabled = false)]
  public virtual Decimal? CuryOverbillingAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyBilledRevenueAmountTotal), typeof (PMCostProjectionByDate.curyRevenueExpectedAmountTotal)})] get
    {
      Decimal? nullable = this.CuryBilledRevenueAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryRevenueExpectedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.BilledRevenueAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RevenueExpectedAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OverbillingAmountTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.billedRevenueAmountTotal), typeof (PMCostProjectionByDate.revenueExpectedAmountTotal)})] get
    {
      Decimal? nullable = this.BilledRevenueAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RevenueExpectedAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedAmountTotal" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDate.projectedMarginTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Margin", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CuryProjectedMarginTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyRevisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyProjectedAmountTotal), typeof (PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal)})] get
    {
      Decimal? nullable = this.CuryRevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryPendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryProjectedAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDate.RevisedRevenueBudgetedAmountTotal" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDate.ProjectedAmountTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProjectedMarginTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.revisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.projectedAmountTotal), typeof (PMCostProjectionByDate.pendingRevenueChangeOrderAmountTotal)})] get
    {
      Decimal? nullable = this.RevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.PendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.ProjectedAmountTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The projected margin percent, which is the result of dividing <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryProjectedMarginTotal" /> by <see cref="P:PX.Objects.PM.PMCostProjectionByDate.CuryRevisedRevenueBudgetedAmountTotal" />, multiplied by 100%.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Margin (%)", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? ProjectedMarginPctTotal
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDate.curyProjectedMarginTotal), typeof (PMCostProjectionByDate.curyRevisedRevenueBudgetedAmountTotal), typeof (PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal)})] get
    {
      Decimal? nullable1 = this.CuryRevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryPendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      if (!(valueOrDefault1 + valueOrDefault2 != 0M))
        return new Decimal?();
      Decimal? nullable2 = this.CuryProjectedMarginTotal;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryRevisedRevenueBudgetedAmountTotal;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPendingRevenueChangeOrderAmountTotal;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal num = valueOrDefault4 + valueOrDefault5;
      return new Decimal?(valueOrDefault3 / num * 100M);
    }
  }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PMProject.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.EP.EPEmployee">employee</see> responsible for the document.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDefault(typeof (PMProject.ownerID))]
  [Owner(typeof (PMCostProjectionByDate.workgroupID))]
  public virtual int? OwnerID { get; set; }

  /// <exclude />
  [PXNote(DescriptionField = typeof (PMCostProjectionByDate.description))]
  public virtual Guid? NoteID { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : PrimaryKeyOf<PMCostProjectionByDate>.By<PMCostProjectionByDate.refNbr>
  {
    public static PMCostProjectionByDate Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (PMCostProjectionByDate) PrimaryKeyOf<PMCostProjectionByDate>.By<PMCostProjectionByDate.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMCostProjectionByDate>.By<PMCostProjectionByDate.projectID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionByDate.refNbr>
  {
    public const int Length = 30;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionByDate.projectID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionByDate.status>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMCostProjectionByDate.date>
  {
  }

  public abstract class projectionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDate.projectionDate>
  {
  }

  public abstract class actualTillDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDate.actualTillDate>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionByDate.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionByDate.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionByDate.rejected>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionByDate.released>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionByDate.description>
  {
  }

  public abstract class groupByProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.groupByProjectTaskID>
  {
  }

  public abstract class groupByAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.groupByAccountGroupID>
  {
  }

  public abstract class groupByInventoryID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.groupByInventoryID>
  {
  }

  public abstract class groupByCostCodeID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.groupByCostCodeID>
  {
  }

  public abstract class includePendingChangeOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.includePendingChangeOrders>
  {
  }

  public abstract class updateProjectBudget : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionByDate.updateProjectBudget>
  {
  }

  public abstract class curyAmountToCompleteTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyAmountToCompleteTotal>
  {
  }

  public abstract class amountToCompleteTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.amountToCompleteTotal>
  {
  }

  public abstract class curyProjectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyProjectedAmountTotal>
  {
  }

  public abstract class projectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.projectedAmountTotal>
  {
  }

  public abstract class curyBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyBudgetedAmountTotal>
  {
  }

  public abstract class budgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.budgetedAmountTotal>
  {
  }

  public abstract class curyOriginalBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyOriginalBudgetedAmountTotal>
  {
  }

  public abstract class originalBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.originalBudgetedAmountTotal>
  {
  }

  public abstract class curyRevisedBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyRevisedBudgetedAmountTotal>
  {
  }

  public abstract class revisedBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.revisedBudgetedAmountTotal>
  {
  }

  public abstract class curyChangeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyChangeOrderAmountTotal>
  {
  }

  public abstract class changeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.changeOrderAmountTotal>
  {
  }

  public abstract class curyPendingChangeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyPendingChangeOrderAmountTotal>
  {
  }

  public abstract class pendingChangeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.pendingChangeOrderAmountTotal>
  {
  }

  public abstract class curyPendingRevenueChangeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyPendingRevenueChangeOrderAmountTotal>
  {
  }

  public abstract class pendingRevenueChangeOrderAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.pendingRevenueChangeOrderAmountTotal>
  {
  }

  public abstract class curyActualAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyActualAmountTotal>
  {
  }

  public abstract class actualAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.actualAmountTotal>
  {
  }

  public abstract class curyCommitmentOpenAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyCommitmentOpenAmountTotal>
  {
  }

  public abstract class commitmentOpenAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.commitmentOpenAmountTotal>
  {
  }

  public abstract class curyPendingCommitmentAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyPendingCommitmentAmountTotal>
  {
  }

  public abstract class pendingCommitmentAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.pendingCommitmentAmountTotal>
  {
  }

  public abstract class curyCompletedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyCompletedAmountTotal>
  {
  }

  public abstract class completedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.completedAmountTotal>
  {
  }

  public abstract class curyRevisedRevenueBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyRevisedRevenueBudgetedAmountTotal>
  {
  }

  public abstract class revisedRevenueBudgetedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.revisedRevenueBudgetedAmountTotal>
  {
  }

  public abstract class curyBilledRevenueAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyBilledRevenueAmountTotal>
  {
  }

  public abstract class billedRevenueAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.billedRevenueAmountTotal>
  {
  }

  public abstract class completedPctTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.completedPctTotal>
  {
    public const int Precision = 2;
  }

  public abstract class curyBudgetBacklogAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyBudgetBacklogAmountTotal>
  {
  }

  public abstract class budgetBacklogAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.budgetBacklogAmountTotal>
  {
  }

  public abstract class curyRevenueBudgetBacklogAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyRevenueBudgetBacklogAmountTotal>
  {
  }

  public abstract class revenueBudgetBacklogAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.revenueBudgetBacklogAmountTotal>
  {
  }

  public abstract class curyExpectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyExpectedAmountTotal>
  {
  }

  public abstract class expectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.expectedAmountTotal>
  {
  }

  public abstract class curyRevenueExpectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyRevenueExpectedAmountTotal>
  {
  }

  public abstract class revenueExpectedAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.revenueExpectedAmountTotal>
  {
  }

  public abstract class performanceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.performanceTotal>
  {
  }

  public abstract class anticipatedPerformanceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.anticipatedPerformanceTotal>
  {
  }

  public abstract class curyOverbillingAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyOverbillingAmountTotal>
  {
  }

  public abstract class overbillingAmountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.overbillingAmountTotal>
  {
  }

  public abstract class curyProjectedMarginTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.curyProjectedMarginTotal>
  {
  }

  public abstract class projectedMarginTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.projectedMarginTotal>
  {
  }

  public abstract class projectedMarginPctTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDate.projectedMarginPctTotal>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionByDate.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionByDate.ownerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjectionByDate.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCostProjectionByDate.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionByDate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionByDate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionByDate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionByDate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDate.lastModifiedDateTime>
  {
  }
}
