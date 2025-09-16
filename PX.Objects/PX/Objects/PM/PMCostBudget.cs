// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a project budget line with the <see cref="F:PX.Objects.GL.AccountType.Expense">Expense</see> type. The records of this type are created and edited through the <b>Cost Budget</b>
/// tab of the Projects (PM301000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph) and through the <b>Cost Budget</b> tab of the Projects Templates (PM208000)
/// form (which corresponds to the <see cref="T:PX.Objects.PM.TemplateMaint" /> graph). The DAC is based on the <see cref="T:PX.Objects.PM.PMBudget" /> DAC.</summary>
[PXCacheName("Project Cost Budget")]
[PXBreakInheritance]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMCostBudget : PMBudget
{
  /// <inheritdoc />
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMCostBudget.projectID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXForeignReference(typeof (Field<PMCostBudget.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <inheritdoc />
  [PMTaskCompleted]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMCostBudget.projectTaskID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>>>))]
  [ProjectTask(typeof (PMCostBudget.projectID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMCostBudget.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMCostBudget.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public override int? ProjectTaskID { get; set; }

  /// <inheritdoc />
  [CostCode(null, typeof (PMCostBudget.projectTaskID), "E", typeof (PMCostBudget.accountGroupID), true, false, IsKey = true, Filterable = false, SkipVerification = true)]
  [PXForeignReference(typeof (Field<PMCostBudget.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <inheritdoc />
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [AccountGroup(typeof (Where<PMAccountGroup.isExpense, Equal<True>>), IsKey = true)]
  [PXForeignReference(typeof (Field<PMCostBudget.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public override int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <inheritdoc />
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDefault]
  [PMInventorySelector]
  [PXForeignReference(typeof (Field<PMCostBudget.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the budget line.</summary>
  /// <value>
  /// By default, its value is <see cref="F:PX.Objects.GL.AccountType.Expense">Expense</see>.
  /// </value>
  [PXDBString(1)]
  [PXDefault("E")]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
  public override 
  #nullable disable
  string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// </summary>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  [PXUIEnabled(typeof (Where<PMCostBudget.type, NotEqual<AccountType.income>>))]
  [PXSelector(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.description), typeof (PMTask.status)}, SubstituteKey = typeof (PMTask.taskCD), DirtyRead = true)]
  [PXUIField(DisplayName = "Revenue Task")]
  [PXDBInt]
  public override int? RevenueTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem"> revenue inventory item</see> associated with the budget line.
  /// </summary>
  [Obsolete]
  [PXUIField(DisplayName = "Revenue Item")]
  [PXDBInt]
  [PMRevenueBudgetLineSelector]
  public override int? RevenueInventoryID { get; set; }

  /// <inheritdoc />
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public override string Description { get; set; }

  /// <inheritdoc />
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public override Decimal? Qty { get; set; }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMCostBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMCostBudget.inventoryID))]
  public override string UOM { get; set; }

  /// <summary>
  /// The cost of the specified unit of the cost budget line in the project currency.
  /// </summary>
  [PXDBCurrencyPriceCost(typeof (PMCostBudget.curyInfoID), typeof (PMCostBudget.rate))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public override Decimal? CuryUnitRate { get; set; }

  /// <summary>
  /// The cost of the specified unit of the cost budget line in the base currency of the tenant.
  /// </summary>
  [PXDBPriceCost]
  public override Decimal? Rate { get; set; }

  /// <inheritdoc />
  [PXDBCurrencyPriceCost(typeof (PMCostBudget.curyInfoID), typeof (PMCostBudget.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public override Decimal? CuryUnitPrice { get; set; }

  /// <inheritdoc />
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price in Base Currency")]
  public override Decimal? UnitPrice { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMCostBudget.curyInfoID), typeof (PMCostBudget.amount))]
  [PXFormula(typeof (Mult<PMCostBudget.qty, PMCostBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public override Decimal? CuryAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public override Decimal? Amount { get; set; }

  /// <inheritdoc />
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public override Decimal? RevisedQty { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMCostBudget.curyInfoID), typeof (PMCostBudget.revisedAmount))]
  [PXFormula(typeof (Mult<PMCostBudget.revisedQty, PMCostBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public override Decimal? CuryRevisedAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency")]
  public override Decimal? RevisedAmount { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMCostBudget.curyInfoID), typeof (PMCostBudget.invoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount", Enabled = false)]
  public override Decimal? CuryInvoicedAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount in Base Currency", Enabled = false)]
  public override Decimal? InvoicedAmount { get; set; }

  /// <inheritdoc />
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(typeof (Search<PMProject.retainagePct, Where<PMProject.contractID, Equal<Current<PMCostBudget.projectID>>>>))]
  [PXUIField(DisplayName = "Retainage (%)", FieldClass = "Retainage")]
  public override Decimal? RetainagePct { get; set; }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public new class PK : 
    PrimaryKeyOf<PMCostBudget>.By<PMCostBudget.projectID, PMCostBudget.projectTaskID, PMCostBudget.accountGroupID, PMCostBudget.costCodeID, PMCostBudget.inventoryID>
  {
    public static PMCostBudget Find(
      PXGraph graph,
      int? projectID,
      int? projectTaskID,
      int? accountGroupID,
      int? costCodeID,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (PMCostBudget) PrimaryKeyOf<PMCostBudget>.By<PMCostBudget.projectID, PMCostBudget.projectTaskID, PMCostBudget.accountGroupID, PMCostBudget.costCodeID, PMCostBudget.inventoryID>.FindBy(graph, (object) projectID, (object) projectTaskID, (object) accountGroupID, (object) costCodeID, (object) inventoryID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public new static class FK
  {
    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMCostBudget>.By<PMCostBudget.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMCostBudget>.By<PMCostBudget.projectID, PMCostBudget.projectTaskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMCostBudget>.By<PMCostBudget.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMCostBudget>.By<PMCostBudget.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMCostBudget>.By<PMCostBudget.inventoryID>
    {
    }
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.projectID>
  {
  }

  public new abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.projectTaskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.costCodeID>
  {
  }

  public new abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.accountGroupID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.inventoryID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostBudget.type>
  {
  }

  public new abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.revenueTaskID>
  {
  }

  public new abstract class revenueInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMCostBudget.revenueInventoryID>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostBudget.description>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.qty>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostBudget.uOM>
  {
  }

  public new abstract class curyUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyUnitRate>
  {
  }

  public new abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.rate>
  {
  }

  public new abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyUnitPrice>
  {
  }

  public new abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.unitPrice>
  {
  }

  public new abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.curyAmount>
  {
  }

  public new abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.amount>
  {
  }

  public new abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.revisedQty>
  {
  }

  public new abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyRevisedAmount>
  {
  }

  public new abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.revisedAmount>
  {
  }

  public new abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyInvoicedAmount>
  {
  }

  public new abstract class invoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.invoicedAmount>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostBudget.taxCategoryID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMCostBudget.curyInfoID>
  {
  }

  public new abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMCostBudget.actualQty>
  {
  }

  public new abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyActualAmount>
  {
  }

  public new abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.draftChangeOrderQty>
  {
  }

  public new abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyDraftChangeOrderAmount>
  {
  }

  public new abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.changeOrderQty>
  {
  }

  public new abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyChangeOrderAmount>
  {
  }

  public new abstract class committedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedQty>
  {
  }

  public new abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedAmount>
  {
  }

  public new abstract class committedOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedOpenQty>
  {
  }

  public new abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedOpenAmount>
  {
  }

  public new abstract class committedReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedReceivedQty>
  {
  }

  public new abstract class committedInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedInvoicedQty>
  {
  }

  public new abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.committedInvoicedAmount>
  {
  }

  public new abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyActualPlusOpenCommittedAmount>
  {
  }

  public new abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.curyVarianceAmount>
  {
  }

  public new abstract class performance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.performance>
  {
  }

  public new abstract class isProduction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostBudget.isProduction>
  {
  }

  public new abstract class productivityTracking : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostBudget.productivityTracking>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMCostBudget.lineCntr>
  {
  }

  public new abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMCostBudget.retainagePct>
  {
  }
}
