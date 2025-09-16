// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostProjectionLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.ProjectAccounting;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a cost projection revision line.
/// The records of this type are created and edited through the Cost Projection (PM305000) form
/// (which corresponds to the <see cref="T:PX.Objects.CN.ProjectAccounting.CostProjectionEntry" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Cost Projection Line")]
[PXPrimaryGraph(typeof (CostProjectionEntry))]
[Serializable]
public class PMCostProjectionLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> for which the cost projection revision line is created.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault(typeof (PMCostProjection.projectID))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  /// <summary>The revision identifier of the cost projection.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostProjection.revisionID" /> field.
  /// </value>
  [PXDBString(30, IsKey = true)]
  [PXDefault(typeof (PMCostProjection.revisionID))]
  [PXUIField]
  public virtual 
  #nullable disable
  string RevisionID { get; set; }

  /// <summary>The number of the cost projection line.</summary>
  [PXParent(typeof (Select<PMCostProjection, Where<PMCostProjection.projectID, Equal<Current<PMCostProjectionLine.projectID>>, And<PMCostProjection.revisionID, Equal<Current<PMCostProjectionLine.revisionID>>>>>))]
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMCostProjection.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMCostProjectionLine.projectID), AlwaysEnabled = true, DisplayName = "Cost Task", AllowNull = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMCostProjectionLine.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMCostProjectionLine.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">project account group</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(typeof (Where<PMAccountGroup.isExpense, Equal<True>>))]
  [PXForeignReference(typeof (Field<PMCostProjectionLine.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">project cost code</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMCostCode.CostCodeID" /> field.
  /// </value>
  [CostCode(null, typeof (PMCostProjectionLine.taskID), "E", typeof (PMCostProjectionLine.accountGroupID), SkipVerification = true, AllowNullValue = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PMInventorySelector]
  public virtual int? InventoryID { get; set; }

  /// <summary>The description of the cost budget line.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  /// <summary>The unit of measure of the cost budget line.</summary>
  [PXDBString(6, IsUnicode = true)]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  public virtual string UOM { get; set; }

  /// <summary>
  /// The budgeted quantity of the cost budget line specified in the project.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalBudgetedQuantity>), ForceAggregateRecalculation = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted Quantity", Enabled = false)]
  public virtual Decimal? BudgetedQuantity { get; set; }

  /// <summary>
  /// The budgeted cost of the cost budget line specified in the project.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalBudgetedAmount>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted Cost", Enabled = false)]
  public virtual Decimal? BudgetedAmount { get; set; }

  /// <summary>
  /// The total quantity of the released project transactions that correspond to the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalActualQuantity>), ForceAggregateRecalculation = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false, Visible = false)]
  public virtual Decimal? ActualQuantity { get; set; }

  /// <summary>
  /// The total amount of the released project transactions that correspond to the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalActualAmount>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Cost", Enabled = false, Visible = false)]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>The committed open quantity of the cost budget line.</summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalUnbilledQuantity>), ForceAggregateRecalculation = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Quantity", Enabled = false, Visible = false)]
  public virtual Decimal? UnbilledQuantity { get; set; }

  /// <summary>The committed open cost of the cost budget line.</summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalUnbilledAmount>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Cost", Enabled = false, Visible = false)]
  public virtual Decimal? UnbilledAmount { get; set; }

  /// <summary>
  /// The sum of the actual quantity and committed open quantity of the cost budget line specified in the project.
  /// </summary>
  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual + Committed Open Quantity", Enabled = false)]
  public virtual Decimal? CompletedQuantity
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionLine.actualQuantity), typeof (PMCostProjectionLine.unbilledQuantity)})] get
    {
      Decimal? nullable = this.ActualQuantity;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.UnbilledQuantity;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 + valueOrDefault2);
    }
  }

  /// <summary>
  /// The sum of the actual cost and committed open cost of the cost budget line specified in the project.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual + Committed Open Cost", Enabled = false)]
  public virtual Decimal? CompletedAmount
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionLine.actualAmount), typeof (PMCostProjectionLine.unbilledAmount)})] get
    {
      Decimal? nullable = this.ActualAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.UnbilledAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 + valueOrDefault2);
    }
  }

  /// <summary>
  /// The remainder of the budgeted quantity of the cost budget line that is currently available for completion.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalQuantityToComplete>), ForceAggregateRecalculation = true)]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity to Complete", Enabled = false)]
  public virtual Decimal? QuantityToComplete
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionLine.budgetedQuantity), typeof (PMCostProjectionLine.completedQuantity)})] get
    {
      Decimal? nullable = this.BudgetedQuantity;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CompletedQuantity;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(Math.Max(0M, valueOrDefault1 - valueOrDefault2));
    }
  }

  /// <summary>
  /// The remainder of the budgeted cost of the cost budget line that is currently available for completion.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalAmountToComplete>), ForceAggregateRecalculation = true)]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost to Complete", Enabled = false)]
  public virtual Decimal? AmountToComplete
  {
    [PXDependsOnFields(new Type[] {typeof (PMCostProjectionLine.budgetedAmount), typeof (PMCostProjectionLine.completedAmount)})] get
    {
      Decimal? nullable = this.BudgetedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CompletedAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(Math.Max(0M, valueOrDefault1 - valueOrDefault2));
    }
  }

  /// <summary>
  /// The mode that specifies how the amounts and quantities are recalculated for the cost budget line.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.CN.ProjectAccounting.ProjectionMode.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ProjectionMode.List]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Mode")]
  public virtual string Mode { get; set; }

  /// <summary>
  /// The projected remainder of the budgeted quantity for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalQuantity>), ForceAggregateRecalculation = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Quantity to Complete")]
  public virtual Decimal? Quantity { get; set; }

  /// <summary>
  /// The projected remainder of the budgeted cost for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalAmount>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost to Complete")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The projected final quantity at the moment of project completion for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalProjectedQuantity>), ForceAggregateRecalculation = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Quantity at Completion")]
  public virtual Decimal? ProjectedQuantity { get; set; }

  /// <summary>
  /// The projected final cost at the moment of project completion for the cost budget line.
  /// </summary>
  [PXFormula(null, typeof (SumCalc<PMCostProjection.totalProjectedAmount>), ForceAggregateRecalculation = true)]
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion")]
  public virtual Decimal? ProjectedAmount { get; set; }

  /// <summary>
  /// The difference between <see cref="P:PX.Objects.PM.PMCostProjectionLine.ProjectedQuantity" /> and <see cref="P:PX.Objects.PM.PMCostProjectionLine.BudgetedQuantity" />.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Variance Quantity")]
  public virtual Decimal? VarianceQuantity { get; set; }

  /// <summary>
  /// The difference between the <see cref="T:PX.Objects.PM.PMCostProjectionLine.amount">projected cost</see> at completion and the <see cref="T:PX.Objects.PM.PMCostProjectionLine.budgetedAmount" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Variance Cost")]
  public virtual Decimal? VarianceAmount { get; set; }

  /// <summary>
  /// The projected percentage of completion for the cost budget line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Completed (%)")]
  public virtual Decimal? CompletedPct { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<PMCostProjectionLine>.By<PMCostProjectionLine.projectID, PMCostProjectionLine.revisionID, PMCostProjectionLine.lineNbr>
  {
    public static PMCostProjectionLine Find(
      PXGraph graph,
      int? projectID,
      string revisionID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMCostProjectionLine) PrimaryKeyOf<PMCostProjectionLine>.By<PMCostProjectionLine.projectID, PMCostProjectionLine.revisionID, PMCostProjectionLine.lineNbr>.FindBy(graph, (object) projectID, (object) revisionID, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Cost Projection</summary>
    public class CostProjection : 
      PrimaryKeyOf<PMCostProjection>.By<PMCostProjection.projectID, PMCostProjection.revisionID>.ForeignKeyOf<PMCostProjectionLine>.By<PMCostProjectionLine.projectID, PMCostProjectionLine.revisionID>
    {
    }

    /// <summary>Project</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjectionLine.projectID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjectionLine.projectID, PMCostProjectionLine.taskID>
    {
    }

    /// <summary>Account Group</summary>
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjectionLine.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjectionLine.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMCostProjection>.By<PMCostProjectionLine.inventoryID>
    {
    }
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionLine.projectID>
  {
  }

  public abstract class revisionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionLine.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionLine.lineNbr>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionLine.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostProjectionLine.accountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionLine.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostProjectionLine.inventoryID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionLine.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionLine.uOM>
  {
  }

  public abstract class budgetedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.budgetedQuantity>
  {
  }

  public abstract class budgetedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.budgetedAmount>
  {
  }

  public abstract class actualQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.actualQuantity>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.actualAmount>
  {
  }

  public abstract class unbilledQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.unbilledQuantity>
  {
  }

  public abstract class unbilledAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.unbilledAmount>
  {
  }

  public abstract class completedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.completedQuantity>
  {
  }

  public abstract class completedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.completedAmount>
  {
  }

  public abstract class quantityToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.quantityToComplete>
  {
  }

  public abstract class amountToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.amountToComplete>
  {
  }

  public abstract class mode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionLine.mode>
  {
  }

  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostProjectionLine.quantity>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostProjectionLine.amount>
  {
  }

  public abstract class projectedQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.projectedQuantity>
  {
  }

  public abstract class projectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.projectedAmount>
  {
  }

  public abstract class varianceQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.varianceQuantity>
  {
  }

  public abstract class varianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.varianceAmount>
  {
  }

  public abstract class completedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostProjectionLine.completedPct>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjectionLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCostProjectionLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjectionLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionLine.lastModifiedDateTime>
  {
  }
}
