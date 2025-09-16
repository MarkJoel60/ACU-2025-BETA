// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRevenueBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.ProjectAccounting;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a project budget line with the <see cref="F:PX.Objects.GL.AccountType.Income">Income</see> type. The records of this type are created and edited through the <strong>Revenue
/// Budget</strong> tab of the Projects (PM301000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph) and through the <strong>Revenue Budget</strong> tab of the
/// Projects Templates (PM208000) form (which corresponds to the <see cref="T:PX.Objects.PM.TemplateMaint" /> graph). The DAC is based on the <see cref="T:PX.Objects.PM.PMBudget" /> DAC and extends it with the fields
/// relevant to the lines of this type.</summary>
[PXCacheName("Project Revenue Budget")]
[PXBreakInheritance]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMRevenueBudget : PMBudget
{
  /// <inheritdoc />
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMRevenueBudget.projectID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXForeignReference(typeof (Field<PMRevenueBudget.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <inheritdoc />
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMRevenueBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMRevenueBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMRevenueBudget.projectTaskID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>>))]
  [ProjectTask(typeof (PMRevenueBudget.projectID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMRevenueBudget.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMRevenueBudget.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public override int? ProjectTaskID { get; set; }

  /// <inheritdoc />
  [CostCode(null, typeof (PMRevenueBudget.projectTaskID), "I", typeof (PMRevenueBudget.accountGroupID), true, false, IsKey = true, Filterable = false, SkipVerification = true)]
  [PXForeignReference(typeof (Field<PMRevenueBudget.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <inheritdoc />
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDefault]
  [AccountGroup(typeof (Where<PMAccountGroup.type, Equal<AccountType.income>>), IsKey = true)]
  [PXForeignReference(typeof (Field<PMRevenueBudget.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
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
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the budget line.</summary>
  /// <value>
  /// By default, its value is <see cref="F:PX.Objects.GL.AccountType.Income">Income</see>.
  /// </value>
  [PXDBString(1)]
  [PXDefault("I")]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
  public override 
  #nullable disable
  string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// </summary>
  [PXDBInt]
  public override int? RevenueTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem"> revenue inventory item</see> associated with the budget line.
  /// </summary>
  [Obsolete]
  [PXDBInt]
  public override int? RevenueInventoryID { get; set; }

  /// <inheritdoc />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public override string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

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
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMRevenueBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMRevenueBudget.inventoryID))]
  public override string UOM { get; set; }

  /// <summary>
  /// The price of the specified unit of the revenue budget line in the project currency.
  /// </summary>
  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.rate))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public override Decimal? CuryUnitRate { get; set; }

  /// <summary>
  /// The price of the specified unit of the revenue budget line in the base currency of the tenant.
  /// </summary>
  [PXDBPriceCost]
  public override Decimal? Rate { get; set; }

  /// <inheritdoc />
  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public override Decimal? CuryUnitPrice { get; set; }

  /// <inheritdoc />
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price in Base Currency")]
  public override Decimal? UnitPrice { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.amount))]
  [PXFormula(typeof (Mult<PMRevenueBudget.qty, PMRevenueBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public override Decimal? CuryAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public override Decimal? Amount { get; set; }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMRevenueBudget.CuryRevisedAmount">Revised Budgeted Amount</see> and
  /// <see cref="P:PX.Objects.PM.PMBudget.CuryActualAmount">Actual</see> values.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.varianceAmount))]
  [PXUIField(DisplayName = "Variance Amount", Enabled = false)]
  public override Decimal? CuryVarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMRevenueBudget.curyRevisedAmount), typeof (PMRevenueBudget.curyActualAmount)})] get
    {
      Decimal? curyRevisedAmount = this.CuryRevisedAmount;
      Decimal? curyActualAmount = this.CuryActualAmount;
      return !(curyRevisedAmount.HasValue & curyActualAmount.HasValue) ? new Decimal?() : new Decimal?(curyRevisedAmount.GetValueOrDefault() - curyActualAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMRevenueBudget.RevisedAmount" /> and <see cref="P:PX.Objects.PM.PMBudget.ActualAmount" /> values.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Variance Amount in Base Currency", Enabled = false)]
  public override Decimal? VarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMRevenueBudget.revisedAmount), typeof (PMBudget.actualAmount)})] get
    {
      Decimal? revisedAmount = this.RevisedAmount;
      Decimal? actualAmount = this.ActualAmount;
      return !(revisedAmount.HasValue & actualAmount.HasValue) ? new Decimal?() : new Decimal?(revisedAmount.GetValueOrDefault() - actualAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The inclusive tax amount in project currency calculated from the data of <see cref="P:PX.Objects.AR.ARTran.CuryTaxAmt" />
  /// and from the data of <see cref="P:PX.Objects.AR.ARTax.CuryRetainedTaxAmt" />.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.inclTaxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Amount", Visible = false, Enabled = false)]
  public override Decimal? CuryInclTaxAmount { get; set; }

  /// <summary>
  /// The inclusive tax amount in base currency calculated from the data of <see cref="P:PX.Objects.AR.ARTran.TaxAmt" />
  /// and from the data of <see cref="P:PX.Objects.AR.ARTax.RetainedTaxAmt" />.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Amount in Base Currency", Visible = false, Enabled = false)]
  public override Decimal? InclTaxAmount { get; set; }

  /// <inheritdoc />
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public override Decimal? RevisedQty { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.revisedAmount))]
  [PXFormula(typeof (Mult<PMRevenueBudget.revisedQty, PMRevenueBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public override Decimal? CuryRevisedAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency")]
  public override Decimal? RevisedAmount { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.invoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoice Amount", Enabled = false)]
  public override Decimal? CuryInvoicedAmount { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount in Base Currency", Enabled = false)]
  public override Decimal? InvoicedAmount { get; set; }

  [PXDBString]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Progress Billing Basis")]
  [PX.Objects.PM.ProgressBillingBase.List]
  public override string ProgressBillingBase { get; set; }

  /// <summary>
  ///  An internal field that is used to track the calculation source (whether auto or manual) of the completed percentage value of a revenue budget.
  /// </summary>
  /// <remarks>
  ///  This field is used internally by the system while a user is working with the revenue budget of a project.The system uses this field to track the completed percentage value for the revenue budget in real-time and determine whether this percentage was calculated automatically or manually. Based on the result of this determination, the system either runs the calculation again or keeps the user’s input. This field is constantly updated while the user processes documents. This is not a field that dictates a setting, but this is an additional internal field that the system uses to support calculations.
  /// </remarks>
  [PXDBString(1, IsFixed = true)]
  [ProjectionMode.ShortList]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Mode")]
  public override string Mode { get; set; }

  /// <inheritdoc />
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)")]
  public override Decimal? CompletedPct { get; set; }

  /// <summary>
  /// The quantity for which the customer will be billed during the next billing.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Quantity")]
  public override Decimal? QtyToInvoice { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.amountToInvoice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Amount")]
  public override Decimal? CuryAmountToInvoice { get; set; }

  /// <inheritdoc />
  [PXDecimal(2, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid (%)")]
  public override Decimal? PrepaymentPct { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Amount")]
  public override Decimal? CuryPrepaymentAmount { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentAvailable))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Available", Enabled = false)]
  public override Decimal? CuryPrepaymentAvailable { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentInvoiced))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Invoiced", Enabled = false)]
  public override Decimal? CuryPrepaymentInvoiced { get; set; }

  /// <inheritdoc />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limit Quantity")]
  public override bool? LimitQty { get; set; }

  /// <inheritdoc />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limit Amount")]
  public override bool? LimitAmount { get; set; }

  /// <inheritdoc />
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Quantity")]
  public override Decimal? MaxQty { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.maxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Amount")]
  public override Decimal? CuryMaxAmount { get; set; }

  /// <inheritdoc />
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(typeof (Search<PMProject.retainagePct, Where<PMProject.contractID, Equal<Current<PMRevenueBudget.projectID>>>>))]
  [PXUIField(DisplayName = "Retainage (%)", FieldClass = "Retainage")]
  public override Decimal? RetainagePct { get; set; }

  /// <summary>Retainage Cap Amount in the project currency.</summary>
  [PXFormula(typeof (Switch<Case<Where<Current<PMProject.includeCO>, Equal<True>>, Mult<PMRevenueBudget.curyRevisedAmount, Mult<Div<PMRevenueBudget.retainagePct, decimal100>, Div<PMBudget.retainageMaxPct, decimal100>>>>, Case<Where<Current<PMProject.includeCO>, NotEqual<True>>, Mult<PMRevenueBudget.curyAmount, Mult<Div<PMRevenueBudget.retainagePct, decimal100>, Div<PMBudget.retainageMaxPct, decimal100>>>>>))]
  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMRevenueBudget.capAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Cap Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryCapAmount { get; set; }

  /// <summary>Retainage Cap Amount (in Base Currency)</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CapAmount { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.lastCostToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryLastCostToComplete { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.costToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryCostToComplete { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? LastPercentCompleted { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? PercentCompleted { get; set; }

  /// <inheritdoc />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.lastCostAtCompletion))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryLastCostAtCompletion { get; set; }

  /// <inheritdoc />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryCostAtCompletion { get; set; }

  /// <inheritdoc />
  [PXDefault(false)]
  [PXDBBool]
  public override bool? IsProduction
  {
    get => this._IsProduction;
    set => this._IsProduction = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public new class PK : 
    PrimaryKeyOf<PMRevenueBudget>.By<PMRevenueBudget.projectID, PMRevenueBudget.projectTaskID, PMRevenueBudget.accountGroupID, PMRevenueBudget.costCodeID, PMRevenueBudget.inventoryID>
  {
    public static PMRevenueBudget Find(
      PXGraph graph,
      int? projectID,
      int? projectTaskID,
      int? accountGroupID,
      int? costCodeID,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (PMRevenueBudget) PrimaryKeyOf<PMRevenueBudget>.By<PMRevenueBudget.projectID, PMRevenueBudget.projectTaskID, PMRevenueBudget.accountGroupID, PMRevenueBudget.costCodeID, PMRevenueBudget.inventoryID>.FindBy(graph, (object) projectID, (object) projectTaskID, (object) accountGroupID, (object) costCodeID, (object) inventoryID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public new static class FK
  {
    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMRevenueBudget>.By<PMRevenueBudget.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMRevenueBudget>.By<PMRevenueBudget.projectID, PMRevenueBudget.projectTaskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMRevenueBudget>.By<PMRevenueBudget.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMRevenueBudget>.By<PMRevenueBudget.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMRevenueBudget>.By<PMRevenueBudget.inventoryID>
    {
    }
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueBudget.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRevenueBudget.projectTaskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueBudget.costCodeID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRevenueBudget.accountGroupID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueBudget.inventoryID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRevenueBudget.type>
  {
  }

  public new abstract class revenueTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRevenueBudget.revenueTaskID>
  {
  }

  public new abstract class revenueInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRevenueBudget.revenueInventoryID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRevenueBudget.taxCategoryID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRevenueBudget.description>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRevenueBudget.qty>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRevenueBudget.uOM>
  {
  }

  public new abstract class curyUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyUnitRate>
  {
  }

  public new abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRevenueBudget.rate>
  {
  }

  public new abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyUnitPrice>
  {
  }

  public new abstract class unitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.unitPrice>
  {
  }

  public new abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyAmount>
  {
  }

  public new abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRevenueBudget.amount>
  {
  }

  public new abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyVarianceAmount>
  {
  }

  public new abstract class varianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.varianceAmount>
  {
  }

  public new abstract class curyInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyInclTaxAmount>
  {
  }

  public new abstract class inclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.inclTaxAmount>
  {
  }

  public new abstract class revisedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.revisedQty>
  {
  }

  public new abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyRevisedAmount>
  {
  }

  public new abstract class revisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.revisedAmount>
  {
  }

  public new abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyInvoicedAmount>
  {
  }

  public new abstract class invoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.invoicedAmount>
  {
  }

  public new abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.draftChangeOrderQty>
  {
  }

  public new abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyDraftChangeOrderAmount>
  {
  }

  public new abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.changeOrderQty>
  {
  }

  public new abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyChangeOrderAmount>
  {
  }

  public new abstract class actualQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.actualQty>
  {
  }

  public new abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyActualAmount>
  {
  }

  public new abstract class committedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedQty>
  {
  }

  public new abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedAmount>
  {
  }

  public new abstract class committedOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedOpenQty>
  {
  }

  public new abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedOpenAmount>
  {
  }

  public new abstract class committedReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedReceivedQty>
  {
  }

  public new abstract class committedInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedInvoicedQty>
  {
  }

  public new abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.committedInvoicedAmount>
  {
  }

  public new abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyActualPlusOpenCommittedAmount>
  {
  }

  public new abstract class performance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.performance>
  {
  }

  public new abstract class invoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.invoicedQty>
  {
  }

  public new abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMRevenueBudget.progressBillingBase>
  {
  }

  public new abstract class mode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMRevenueBudget.mode>
  {
  }

  public new abstract class completedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.completedPct>
  {
    public const int Precision = 2;
  }

  public new abstract class qtyToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.qtyToInvoice>
  {
  }

  public new abstract class curyAmountToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyAmountToInvoice>
  {
  }

  public new abstract class prepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.prepaymentPct>
  {
  }

  public new abstract class curyPrepaymentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyPrepaymentAmount>
  {
  }

  public new abstract class curyPrepaymentAvailable : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyPrepaymentAvailable>
  {
  }

  public new abstract class curyPrepaymentInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyPrepaymentInvoiced>
  {
  }

  public new abstract class limitQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRevenueBudget.limitQty>
  {
  }

  public new abstract class limitAmount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMRevenueBudget.limitAmount>
  {
  }

  public new abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRevenueBudget.maxQty>
  {
  }

  public new abstract class curyMaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyMaxAmount>
  {
  }

  public new abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.retainagePct>
  {
  }

  /// <exclude />
  public abstract class curyCapAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyCapAmount>
  {
  }

  /// <exclude />
  public abstract class capAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRevenueBudget.capAmount>
  {
  }

  public new abstract class curyLastCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyLastCostToComplete>
  {
  }

  public new abstract class curyCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyCostToComplete>
  {
  }

  public new abstract class lastPercentCompleted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.lastPercentCompleted>
  {
  }

  public new abstract class percentCompleted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.percentCompleted>
  {
  }

  public new abstract class curyLastCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyLastCostAtCompletion>
  {
  }

  public new abstract class curyCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRevenueBudget.curyCostAtCompletion>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRevenueBudget.lineCntr>
  {
  }

  public new abstract class isProduction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMRevenueBudget.isProduction>
  {
  }
}
