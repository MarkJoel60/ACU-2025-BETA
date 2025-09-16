// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostProjectionByDateLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a line of a date-sensitive cost projection.
/// The records of this type are created and edited through the Cost Projection By Date (PM305500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectCostProjectionByDateEntry" /> graph).
/// </summary>
[PXCacheName("Cost Projection By Date Line")]
[PXPrimaryGraph(typeof (ProjectCostProjectionByDateEntry))]
[Serializable]
public class PMCostProjectionByDateLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IProjectFilter
{
  /// <summary>The projection document identifier.</summary>
  [PXParent(typeof (PMCostProjectionByDateLine.FK.Projection))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PMCostProjectionByDate.refNbr))]
  [PXUIField(DisplayName = "Number", Enabled = false)]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>The number of the cost projection line.</summary>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> for which the cost projection line is created.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PMCostProjectionByDate.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("PROTASK", typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCostProjectionByDateLine.projectID>>>>), typeof (PMTask.taskCD))]
  [PXUIField(DisplayName = "Project Task", Enabled = false)]
  public virtual int? ProjectTaskID { get; set; }

  /// <exclude />
  public int? TaskID => this.ProjectTaskID;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID>), typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Account Group", Enabled = false)]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [PXDBInt]
  [PXDimensionSelector("COSTCODE", typeof (Search<PMCostCode.costCodeID>), typeof (PMCostCode.costCodeCD))]
  [PXUIField(DisplayName = "Cost Code", Enabled = false, FieldClass = "COSTCODE")]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The projected percentage of completion for the cost budget line.
  /// </summary>
  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Completed (%)")]
  public virtual Decimal? CompletedPct { get; set; }

  /// <summary>
  /// The projected remainder of the budgeted cost for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyAmountToCompleteTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.amountToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost to Complete", Enabled = true)]
  public virtual Decimal? CuryAmountToComplete { get; set; }

  /// <summary>
  /// The projected remainder of the budgeted cost for the cost budget line in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.amountToCompleteTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmountToComplete { get; set; }

  /// <summary>
  /// The projected final cost at the moment of project completion for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyProjectedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.projectedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion", Enabled = true)]
  public virtual Decimal? CuryProjectedAmount { get; set; }

  /// <summary>
  /// The projected final cost at the moment of project completion for the cost budget line in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.projectedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProjectedAmount { get; set; }

  /// <summary>
  /// The revised budgeted cost, which consists of the original budgeted cost and
  /// the cost of all released change orders with the date before <see cref="!:ProjectionDate" /> that corresponds to the line.
  /// If change order workflow is not activated, the field contains the revised budget from the corresponding line of the project cost budget.
  /// The revised budget cost is in the project currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyBudgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.budgetedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Cost", Enabled = false)]
  public virtual Decimal? CuryBudgetedAmount { get; set; }

  /// <summary>
  /// The revised budgeted cost, which consists of the original budgeted cost and
  /// the cost of all released change orders with the date before <see cref="!:ProjectionDate" /> that corresponds to the line.
  /// If change order workflow is not activated, the field contains the revised budget from the corresponding line of the project cost budget.
  /// The revised budget cost is in the base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.budgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetedAmount { get; set; }

  /// <summary>
  /// The original budgeted cost of the corresponding cost budget line specified in the project.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyOriginalBudgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.originalBudgetedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOriginalBudgetedAmount { get; set; }

  /// <summary>
  /// The original budgeted cost of the corresponding cost budget line specified in the project in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.originalBudgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OriginalBudgetedAmount { get; set; }

  /// <summary>
  /// The revised budgeted cost of the corresponding cost budget line specified in the project.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyRevisedBudgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.revisedBudgetedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRevisedBudgetedAmount { get; set; }

  /// <summary>
  /// The revised budgeted cost of the corresponding cost budget line specified in the project in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.revisedBudgetedAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RevisedBudgetedAmount { get; set; }

  /// <summary>
  /// The total amount of the project cost in the lines of a change order for particular date.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyChangeOrderAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.changeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the project cost in the lines of a change order for particular date. The amount is in the base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.changeOrderAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the pending project cost in the lines of a change order for particular date.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyPendingChangeOrderAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.pendingChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending CO Cost", Enabled = false)]
  public virtual Decimal? CuryPendingChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the pending project cost in the lines of a change order for particular date. The amount is in the base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.pendingChangeOrderAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the released project cost transactions for particular date.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyActualAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Cost to Date", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>
  /// The total amount of the released project cost transactions for particular date in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.actualAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>
  /// The total amount of the project open commitments for particular date.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyCommitmentOpenAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.commitmentOpenAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Commitments", Enabled = false)]
  public virtual Decimal? CuryCommitmentOpenAmount { get; set; }

  /// <summary>
  /// The total amount of the project open commitments for particular date in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.commitmentOpenAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommitmentOpenAmount { get; set; }

  /// <summary>
  /// The total amount of the project pending commitments for particular date.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.curyPendingCommitmentAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.pendingCommitmentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending CO Commitments", Enabled = false)]
  public virtual Decimal? CuryPendingCommitmentAmount { get; set; }

  /// <summary>
  /// The total amount of the project pending commitments for particular date in base currency.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjectionByDate.pendingCommitmentAmountTotal>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PendingCommitmentAmount { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryActualAmount" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryCommitmentOpenAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.completedAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Anticipated Cost", Enabled = false)]
  public virtual Decimal? CuryCompletedAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.curyActualAmount), typeof (PMCostProjectionByDateLine.curyCommitmentOpenAmount), typeof (PMCostProjectionByDateLine.curyPendingCommitmentAmount)})] get
    {
      Decimal? nullable = this.CuryActualAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryCommitmentOpenAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryPendingCommitmentAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.ActualAmount" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CommitmentOpenAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CompletedAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.actualAmount), typeof (PMCostProjectionByDateLine.commitmentOpenAmount), typeof (PMCostProjectionByDateLine.pendingCommitmentAmount)})] get
    {
      Decimal? nullable = this.ActualAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommitmentOpenAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.PendingCommitmentAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryBudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryActualAmount" />.
  /// </summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMCostProjectionByDateLine.budgetBacklogAmount))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost Budget Backlog", Enabled = false)]
  public virtual Decimal? CuryBudgetBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.curyBudgetedAmount), typeof (PMCostProjectionByDateLine.curyActualAmount), typeof (PMCostProjectionByDateLine.curyPendingChangeOrderAmount)})] get
    {
      Decimal? nullable = this.CuryBudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryPendingChangeOrderAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.CuryActualAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.BudgetedAmount" /> and <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.ActualAmount" />.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BudgetBacklogAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.budgetedAmount), typeof (PMCostProjectionByDateLine.actualAmount), typeof (PMCostProjectionByDateLine.pendingChangeOrderAmount)})] get
    {
      Decimal? nullable = this.BudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.PendingChangeOrderAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.ActualAmount;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num - valueOrDefault3);
    }
  }

  /// <summary>
  /// The performance percent, which is the result of dividing <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryActualAmount" /> by <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryBudgetedAmount" />, multiplied by 100%.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Performance (%)", Enabled = false)]
  public virtual Decimal? Performance
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.curyActualAmount), typeof (PMCostProjectionByDateLine.curyBudgetedAmount), typeof (PMCostProjectionByDateLine.curyPendingChangeOrderAmount)})] get
    {
      Decimal? nullable1 = this.CuryBudgetedAmount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryPendingChangeOrderAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      if (!(valueOrDefault1 + valueOrDefault2 != 0M))
        return new Decimal?();
      Decimal? nullable2 = this.CuryActualAmount;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryBudgetedAmount;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPendingChangeOrderAmount;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal num = valueOrDefault4 + valueOrDefault5;
      return new Decimal?(valueOrDefault3 / num * 100M);
    }
  }

  /// <summary>
  /// The anticipated performance percent, which is the result of dividing <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryCompletedAmount" /> by <see cref="P:PX.Objects.PM.PMCostProjectionByDateLine.CuryBudgetedAmount" />, multiplied by 100%.
  /// </summary>
  [PXDecimal(2)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Anticipated Performance (%)", Enabled = false)]
  public virtual Decimal? AnticipatedPerformance
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionByDateLine.curyCompletedAmount), typeof (PMCostProjectionByDateLine.curyBudgetedAmount), typeof (PMCostProjectionByDateLine.curyPendingChangeOrderAmount)})] get
    {
      Decimal? nullable1 = this.CuryBudgetedAmount;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryPendingChangeOrderAmount;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      if (!(valueOrDefault1 + valueOrDefault2 != 0M))
        return new Decimal?();
      Decimal? nullable2 = this.CuryCompletedAmount;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryBudgetedAmount;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPendingChangeOrderAmount;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal num = valueOrDefault4 + valueOrDefault5;
      return new Decimal?(valueOrDefault3 / num * 100M);
    }
  }

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

  public class PK : 
    PrimaryKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.refNbr, PMCostProjectionByDateLine.lineNbr>
  {
    public static PMCostProjectionByDateLine Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMCostProjectionByDateLine) PrimaryKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.refNbr, PMCostProjectionByDateLine.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Projection : 
      PrimaryKeyOf<PMCostProjectionByDate>.By<PMCostProjectionByDate.refNbr>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.refNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.projectID, PMCostProjectionByDateLine.projectTaskID>
    {
    }

    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.accountGroupID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.inventoryID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMCostProjectionByDateLine>.By<PMCostProjectionByDateLine.costCodeID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionByDateLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionByDateLine.lineNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionByDateLine.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostProjectionByDateLine.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostProjectionByDateLine.accountGroupID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostProjectionByDateLine.inventoryID>
  {
  }

  public abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostProjectionByDateLine.costCodeID>
  {
  }

  public abstract class completedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.completedPct>
  {
    public const int Precision = 2;
  }

  public abstract class curyAmountToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyAmountToComplete>
  {
  }

  public abstract class amountToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.amountToComplete>
  {
  }

  public abstract class curyProjectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyProjectedAmount>
  {
  }

  public abstract class projectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.projectedAmount>
  {
  }

  public abstract class curyBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyBudgetedAmount>
  {
  }

  public abstract class budgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.budgetedAmount>
  {
  }

  public abstract class curyOriginalBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyOriginalBudgetedAmount>
  {
  }

  public abstract class originalBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.originalBudgetedAmount>
  {
  }

  public abstract class curyRevisedBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyRevisedBudgetedAmount>
  {
  }

  public abstract class revisedBudgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.revisedBudgetedAmount>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.changeOrderAmount>
  {
  }

  public abstract class curyPendingChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyPendingChangeOrderAmount>
  {
  }

  public abstract class pendingChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.pendingChangeOrderAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyActualAmount>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.actualAmount>
  {
  }

  public abstract class curyCommitmentOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyCommitmentOpenAmount>
  {
  }

  public abstract class commitmentOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.commitmentOpenAmount>
  {
  }

  public abstract class curyPendingCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyPendingCommitmentAmount>
  {
  }

  public abstract class pendingCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.pendingCommitmentAmount>
  {
  }

  public abstract class curyCompletedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyCompletedAmount>
  {
  }

  public abstract class completedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.completedAmount>
  {
  }

  public abstract class curyBudgetBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.curyBudgetBacklogAmount>
  {
  }

  public abstract class budgetBacklogAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.budgetBacklogAmount>
  {
  }

  public abstract class performance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.performance>
  {
  }

  public abstract class anticipatedPerformance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionByDateLine.anticipatedPerformance>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    PMCostProjectionByDateLine.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionByDateLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionByDateLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDateLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionByDateLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionByDateLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionByDateLine.lastModifiedDateTime>
  {
  }
}
