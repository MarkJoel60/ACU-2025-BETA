// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.ProjectAccounting;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The base class for the project budget line.
/// The records of this type are created and edited through the Project Budget (PM309000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectBalanceMaint" /> graph).
/// </summary>
[PXCacheName("Project Budget")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter, IQuantify
{
  protected bool? _Selected = new bool?(false);
  protected int? _ProjectID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _TaxCategoryID;
  protected Decimal? _ActualQty;
  protected Decimal? _ChangeOrderQty;
  protected Decimal? _CommittedQty;
  protected Decimal? _CommittedOpenQty;
  protected Decimal? _CommittedReceivedQty;
  protected Decimal? _CommittedInvoicedQty;
  protected bool? _IsProduction;
  protected int _SortOrder;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMBudget.projectID>>, And<PMBudget.type, Equal<Current<PMBudget.type>>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXForeignReference(typeof (Field<PMBudget.projectID>.IsRelatedTo<PMProject.contractID>))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see> associated with the budget line.
  /// </summary>
  public int? TaskID => this.ProjectTaskID;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">project task</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PMTaskCompleted]
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.taskID, Equal<Current<PMBudget.projectTaskID>>, And<PMBudget.type, Equal<Current<PMBudget.type>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<PMBudget.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMBudget.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMBudget.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, typeof (PMBudget.projectTaskID), null, typeof (PMBudget.accountGroupID), true, false, IsKey = true, Filterable = false, SkipVerification = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">Account Group</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXForeignReference(typeof (Field<PMBudget.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [PXDefault]
  [AccountGroup(IsKey = true)]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PMInventorySelector]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>))]
  [PXDefault]
  [PXForeignReference(typeof (Field<PMBudget.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the budget line.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description><c>A</c>: Asset</description></item>
  /// <item><description><c>L</c>: Liability</description></item>
  /// <item><description><c>I</c>: Income</description></item>
  /// <item><description><c>E</c>: Expense</description></item>
  /// <item><description><c>O</c>: Off-Balance</description></item>
  /// </list>
  /// </value>
  [PXDBString(1)]
  [PXDefault]
  [PMAccountType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// </summary>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>))]
  [PXUIEnabled(typeof (Where<PMBudget.type, NotEqual<AccountType.income>>))]
  [PXSelector(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMBudget.projectID>>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>), new System.Type[] {typeof (PMTask.taskCD), typeof (PMTask.description), typeof (PMTask.status)}, SubstituteKey = typeof (PMTask.taskCD), DirtyRead = true)]
  [PXUIField(DisplayName = "Revenue Task")]
  [PXDBInt]
  public virtual int? RevenueTaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">revenue project task</see> associated with the budget line.
  /// The field is used in the contract-based API endpoints for backward compatibility.
  /// </summary>
  [PXInt]
  public virtual int? RevenueTaskIDApiEndPoint
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.revenueTaskID)})] get
    {
      return this.RevenueTaskID;
    }
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem"> revenue inventory item</see> associated with the budget line.
  /// </summary>
  [Obsolete]
  [PXDBInt]
  public virtual int? RevenueInventoryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">currency info</see> object associated with the budget line.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo(typeof (PMProject.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The description of the budget line.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>The budgeted quantity of the budget line.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Quantity")]
  public virtual Decimal? Qty { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> of the budget line.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>))]
  [PMUnit(typeof (PMBudget.inventoryID))]
  public virtual string UOM { get; set; }

  /// <summary>
  /// The price or cost of the specified unit of the budget line in the project currency.
  /// </summary>
  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMBudget.rate))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Rate")]
  public virtual Decimal? CuryUnitRate { get; set; }

  /// <summary>
  /// The price or cost of the specified unit of the budget line in the base currency of the tenant.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  public virtual Decimal? Rate { get; set; }

  /// <summary>
  /// The price of the specified unit of the budget line in the project currency.
  /// </summary>
  [PXDBCurrencyPriceCost(typeof (PMBudget.curyInfoID), typeof (PMBudget.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  /// <summary>
  /// The price of the specified unit of the budget line in the base currency of the tenant.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price in Base Currency")]
  public virtual Decimal? UnitPrice { get; set; }

  /// <summary>
  /// The budgeted amount of the budget line in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.amount))]
  [PXFormula(typeof (Mult<PMBudget.qty, PMBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount")]
  public virtual Decimal? CuryAmount { get; set; }

  /// <summary>
  /// The budgeted amount of the budget line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Budgeted Amount in Base Currency")]
  public virtual Decimal? Amount { get; set; }

  /// <summary>The revised budgeted quantity.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Quantity")]
  public virtual Decimal? RevisedQty { get; set; }

  /// <summary>The revised budgeted amount in the project currency.</summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.revisedAmount))]
  [PXFormula(typeof (Mult<PMBudget.revisedQty, PMBudget.curyUnitRate>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount")]
  public virtual Decimal? CuryRevisedAmount { get; set; }

  /// <summary>
  /// The revised budgeted amount in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Budgeted Amount in Base Currency")]
  public virtual Decimal? RevisedAmount { get; set; }

  /// <summary>
  /// The total quantity of unreleased invoices that correspond to the revenue budget line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoice Quantity", Enabled = false)]
  public virtual Decimal? InvoicedQty { get; set; }

  /// <summary>
  /// The total amount of unreleased invoices that correspond to the revenue budget line. The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.invoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoice Amount", Enabled = false)]
  public virtual Decimal? CuryInvoicedAmount { get; set; }

  /// <summary>
  /// The total amount of unreleased invoices that correspond to the revenue budget line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Invoices Amount in Base Currency", Enabled = false)]
  public virtual Decimal? InvoicedAmount { get; set; }

  /// <summary>
  /// The total quantity of the lines of the released accounts receivable invoices that correspond to the budget line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Quantity", Enabled = false)]
  public virtual Decimal? ActualQty
  {
    get => this._ActualQty;
    set => this._ActualQty = value;
  }

  /// <summary>
  /// The total amount of the lines of the released accounts receivable invoices that correspond to the budget line.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.baseActualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <summary>The actual amount in the base currency of the tenant.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Hist. Actual Amount in Base Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? ActualAmount { get; set; }

  /// <summary>
  /// The total amount of the lines of the released accounts receivable invoices that correspond to the budget line in the base currency of the tenant.
  /// </summary>
  [PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount in Base Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? BaseActualAmount { get; set; }

  /// <summary>The inclusive tax amount in project currency.</summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.inclTaxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Amount", Visible = false, Enabled = false)]
  public virtual Decimal? CuryInclTaxAmount { get; set; }

  /// <summary>The inclusive tax amount in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Amount in Base Currency", Visible = false, Enabled = false)]
  public virtual Decimal? InclTaxAmount { get; set; }

  /// <summary>
  /// The total quantity of the estimation lines of open change requests plus
  /// the total quantity of the budget lines of non-closed change orders.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderQty { get; set; }

  /// <summary>
  /// The total amount of the estimation lines of open change requests plus
  /// the total amount of the budget lines of non-closed change orders.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.draftChangeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryDraftChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the estimation lines of open change requests plus the total amount
  /// of the budget lines of non-closed change orders in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Potential CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? DraftChangeOrderAmount { get; set; }

  /// <summary>
  /// The total quantity of the lines of released change orders.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderQty
  {
    get => this._ChangeOrderQty;
    set => this._ChangeOrderQty = value;
  }

  /// <summary>
  /// The total amount of the lines of released change orders.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.changeOrderAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryChangeOrderAmount { get; set; }

  /// <summary>
  /// The total amount of the lines of released change orders in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Budgeted CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? ChangeOrderAmount { get; set; }

  /// <summary>The total quantity of the commitments.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Quantity", Enabled = false)]
  public virtual Decimal? CommittedQty
  {
    get => this._CommittedQty;
    set => this._CommittedQty = value;
  }

  /// <summary>
  /// The total amount of the commitments in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedAmount { get; set; }

  /// <summary>
  /// The total amount of the commitments in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Revised Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedAmount { get; set; }

  /// <summary>
  /// The total quantity of the commitment lines of released change orders.
  /// </summary>
  /// <value>
  /// The quantity is the difference between the <see cref="P:PX.Objects.PM.PMBudget.CommittedQty">Revised Committed Quantity</see>
  /// and the <see cref="P:PX.Objects.PM.PMBudget.CommittedOrigQty">Original Committed Quantity</see>.
  /// </value>
  [PXQuantity]
  [PXUIField(DisplayName = "Committed CO Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.committedQty), typeof (PMBudget.committedOrigQty)})] get
    {
      Decimal? nullable = this.CommittedQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommittedOrigQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The total amount of the commitment lines of released change orders.
  /// The amount is shown in the project currency.
  /// </summary>
  /// <value>
  /// The amount is the difference between the <see cref="P:PX.Objects.PM.PMBudget.CuryCommittedAmount">Revised Committed Amount</see>
  /// and the <see cref="P:PX.Objects.PM.PMBudget.CuryCommittedOrigAmount">Original Committed Amount</see>.
  /// </value>
  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedCOAmount))]
  [PXUIField(DisplayName = "Committed CO Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryCommittedCOAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.curyCommittedAmount), typeof (PMBudget.curyCommittedOrigAmount)})] get
    {
      Decimal? nullable = this.CuryCommittedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryCommittedOrigAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>
  /// The total amount of the commitment lines of released change orders in the base currency of the tenant.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Committed CO Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedCOAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.committedAmount), typeof (PMBudget.committedOrigAmount)})] get
    {
      Decimal? nullable = this.CommittedAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CommittedOrigAmount;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
  }

  /// <summary>The total original quantity of the commitments.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Quantity", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedOrigQty { get; set; }

  /// <summary>
  /// The total original amount of the commitments in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedOrigAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Amount", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CuryCommittedOrigAmount { get; set; }

  /// <summary>
  /// The total original amount of the commitments in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Committed Amount in Base Currency", Enabled = false, FieldClass = "CHANGEORDER")]
  public virtual Decimal? CommittedOrigAmount { get; set; }

  /// <summary>The total open quantity of the commitments.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Quantity", Enabled = false)]
  public virtual Decimal? CommittedOpenQty
  {
    get => this._CommittedOpenQty;
    set => this._CommittedOpenQty = value;
  }

  /// <summary>
  /// The total open amount of the commitments in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedOpenAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedOpenAmount { get; set; }

  /// <summary>
  /// The total open amount of the commitments in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Open Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedOpenAmount { get; set; }

  /// <summary>The total received quantity of the commitments.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Received Quantity", Enabled = false)]
  public virtual Decimal? CommittedReceivedQty
  {
    get => this._CommittedReceivedQty;
    set => this._CommittedReceivedQty = value;
  }

  /// <summary>The total invoiced quantity of the commitments.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Quantity", Enabled = false)]
  public virtual Decimal? CommittedInvoicedQty
  {
    get => this._CommittedInvoicedQty;
    set => this._CommittedInvoicedQty = value;
  }

  /// <summary>
  /// The total invoiced amount of the commitments in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.committedInvoicedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Amount", Enabled = false)]
  public virtual Decimal? CuryCommittedInvoicedAmount { get; set; }

  /// <summary>
  /// The total invoiced amount of the commitments in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Committed Invoiced Amount in Base Currency", Enabled = false)]
  public virtual Decimal? CommittedInvoicedAmount { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.CuryActualAmount">Actual Amount</see> and <see cref="P:PX.Objects.PM.PMBudget.CuryCommittedOpenAmount">Committed Open Amount</see> values.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.actualPlusOpenCommittedAmount))]
  [PXUIField(DisplayName = "Actual + Open Committed Amount", Enabled = false)]
  public virtual Decimal? CuryActualPlusOpenCommittedAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.curyActualAmount), typeof (PMBudget.curyCommittedOpenAmount)})] get
    {
      Decimal? curyActualAmount = this.CuryActualAmount;
      Decimal? committedOpenAmount = this.CuryCommittedOpenAmount;
      return !(curyActualAmount.HasValue & committedOpenAmount.HasValue) ? new Decimal?() : new Decimal?(curyActualAmount.GetValueOrDefault() + committedOpenAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.PM.PMBudget.ActualAmount" /> and <see cref="P:PX.Objects.PM.PMBudget.CommittedOpenAmount" /> values.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Actual + Open Committed Amount in Base Currency", Enabled = false)]
  public virtual Decimal? ActualPlusOpenCommittedAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.actualAmount), typeof (PMBudget.committedOpenAmount)})] get
    {
      Decimal? actualAmount = this.ActualAmount;
      Decimal? committedOpenAmount = this.CommittedOpenAmount;
      return !(actualAmount.HasValue & committedOpenAmount.HasValue) ? new Decimal?() : new Decimal?(actualAmount.GetValueOrDefault() + committedOpenAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMBudget.CuryRevisedAmount">Revised Budgeted Amount</see> and
  /// <see cref="P:PX.Objects.PM.PMBudget.CuryActualPlusOpenCommittedAmount">Actual + Open Committed Amount</see> values.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.varianceAmount))]
  [PXUIField(DisplayName = "Variance Amount", Enabled = false)]
  public virtual Decimal? CuryVarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.curyRevisedAmount), typeof (PMBudget.curyActualPlusOpenCommittedAmount)})] get
    {
      Decimal? curyRevisedAmount = this.CuryRevisedAmount;
      Decimal? openCommittedAmount = this.CuryActualPlusOpenCommittedAmount;
      return !(curyRevisedAmount.HasValue & openCommittedAmount.HasValue) ? new Decimal?() : new Decimal?(curyRevisedAmount.GetValueOrDefault() - openCommittedAmount.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The difference between the <see cref="P:PX.Objects.PM.PMBudget.RevisedAmount" /> and <see cref="P:PX.Objects.PM.PMBudget.ActualPlusOpenCommittedAmount" /> values.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Variance Amount in Base Currency", Enabled = false)]
  public virtual Decimal? VarianceAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMBudget.revisedAmount), typeof (PMBudget.actualPlusOpenCommittedAmount)})] get
    {
      Decimal? revisedAmount = this.RevisedAmount;
      Decimal? openCommittedAmount = this.ActualPlusOpenCommittedAmount;
      return !(revisedAmount.HasValue & openCommittedAmount.HasValue) ? new Decimal?() : new Decimal?(revisedAmount.GetValueOrDefault() - openCommittedAmount.GetValueOrDefault());
    }
  }

  /// <summary>The task performance measure.</summary>
  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Performance (%)", Enabled = false)]
  public virtual Decimal? Performance
  {
    get
    {
      if (this.ProgressBillingBase == "A" || string.IsNullOrEmpty(this.ProgressBillingBase))
      {
        Decimal num1;
        if (this.CuryRevisedAmount.HasValue)
        {
          Decimal? nullable = this.CuryRevisedAmount;
          Decimal num2 = 0.0M;
          if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
          {
            nullable = this.CuryActualAmount;
            Decimal valueOrDefault1 = nullable.GetValueOrDefault();
            nullable = this.CuryInclTaxAmount;
            Decimal valueOrDefault2 = nullable.GetValueOrDefault();
            Decimal num3 = valueOrDefault1 + valueOrDefault2;
            nullable = this.CuryRevisedAmount;
            Decimal valueOrDefault3 = nullable.GetValueOrDefault();
            num1 = num3 / valueOrDefault3 * 100.0M;
            goto label_5;
          }
        }
        num1 = 0.0M;
label_5:
        return new Decimal?(num1);
      }
      if (!(this.ProgressBillingBase == "Q"))
        return new Decimal?(0.0M);
      Decimal num4;
      if (this.RevisedQty.HasValue)
      {
        Decimal? nullable = this.RevisedQty;
        Decimal num5 = 0.0M;
        if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
        {
          nullable = this.ActualQty;
          Decimal valueOrDefault4 = nullable.GetValueOrDefault();
          nullable = this.RevisedQty;
          Decimal valueOrDefault5 = nullable.GetValueOrDefault();
          num4 = valueOrDefault4 / valueOrDefault5 * 100.0M;
          goto label_11;
        }
      }
      num4 = 0.0M;
label_11:
      return new Decimal?(num4);
    }
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the <see cref="P:PX.Objects.PM.PMTask.CompletedPercent">Completed (%)</see>
  /// of the corresponding task is calculated automatically, based on the completion method of the task.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Auto Completed (%)")]
  public virtual bool? IsProduction
  {
    get => this._IsProduction;
    set => this._IsProduction = value;
  }

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
  public virtual string Mode { get; set; }

  /// <summary>
  /// The percentage of the work that has been completed on the revenue budget line.
  /// </summary>
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Completed (%)")]
  public virtual Decimal? CompletedPct { get; set; }

  /// <summary>
  /// The quantity for which the customer will be billed during the next billing.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Quantity")]
  public virtual Decimal? QtyToInvoice { get; set; }

  /// <summary>
  /// The amount for which the customer will be billed during the next billing.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.amountToInvoice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Amount")]
  public virtual Decimal? CuryAmountToInvoice { get; set; }

  /// <summary>
  /// The amount for which the customer will be billed during the next billing in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Invoice Amount in Base Currency")]
  public virtual Decimal? AmountToInvoice { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDecimal(2, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid (%)")]
  public virtual Decimal? PrepaymentPct { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Amount")]
  public virtual Decimal? CuryPrepaymentAmount { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Amount in Base Currency")]
  public virtual Decimal? PrepaymentAmount { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentAvailable))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Available", Enabled = false)]
  public virtual Decimal? CuryPrepaymentAvailable { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Available in Base Currency", Enabled = false)]
  public virtual Decimal? PrepaymentAvailable { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.prepaymentInvoiced))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Invoiced", Enabled = false)]
  public virtual Decimal? CuryPrepaymentInvoiced { get; set; }

  /// <summary>
  /// The field is reserved for a feature that is currently not supported.
  /// </summary>
  /// <exclude />
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Invoiced in Base Currency", Enabled = false)]
  public virtual Decimal? PrepaymentInvoiced { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system controls the quantity
  /// available to bill the customer based on the billing limit quantity.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limit Quantity")]
  public virtual bool? LimitQty { get; set; }

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that the system controls the amount
  /// available to bill the customer based on the billing limit amount.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limit Amount")]
  public virtual bool? LimitAmount { get; set; }

  /// <summary>
  /// The maximum billable quantity for the revenue budget line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Quantity")]
  public virtual Decimal? MaxQty { get; set; }

  /// <summary>
  /// The maximum billable amount for the revenue budget line in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.maxAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Amount")]
  public virtual Decimal? CuryMaxAmount { get; set; }

  /// <summary>
  /// The maximum billable amount for the revenue budget line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Amount in Base Currency")]
  public virtual Decimal? MaxAmount { get; set; }

  /// <summary>
  /// The percent of the invoice line amount to be retained by the customer.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage (%)", FieldClass = "Retainage")]
  public virtual Decimal? RetainagePct { get; set; }

  /// <summary>The retained amount in the project currency.</summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.retainedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainedAmount { get; set; }

  /// <summary>Retained Amount (In Base Currency)</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? RetainedAmount { get; set; }

  /// <summary>The draft retained amount in the project currency.</summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.draftRetainedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryDraftRetainedAmount { get; set; }

  /// <summary>Draft Retained Amount (in Base Currency)</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Draft Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? DraftRetainedAmount { get; set; }

  /// <summary>The total retained amount in the project currency.</summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.totalRetainedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Retained Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryTotalRetainedAmount { get; set; }

  /// <summary>Total Retained Amount in Base Currency</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Retained Amount in Base Currency", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? TotalRetainedAmount { get; set; }

  /// <summary>Retainage Cap %</summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cap (%)", FieldClass = "Retainage")]
  public virtual Decimal? RetainageMaxPct { get; set; }

  /// <summary>
  /// The value of the Cost to Complete column before the most recent change was made to it.
  /// The value is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.lastCostToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost to Complete", Enabled = false)]
  public virtual Decimal? CuryLastCostToComplete { get; set; }

  /// <summary>
  /// The value of the Cost to Complete column before the most recent change was made to it in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost to Complete in Base Currency", Enabled = false)]
  public virtual Decimal? LastCostToComplete { get; set; }

  /// <summary>
  /// The current projected amount that is required to complete the cost budget line.
  /// The amount is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.costToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost to Complete")]
  public virtual Decimal? CuryCostToComplete { get; set; }

  /// <summary>
  /// The current projected amount that is required to complete the cost budget line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost To Complete in Base Currency")]
  public virtual Decimal? CostToComplete { get; set; }

  /// <summary>
  /// The value of the Percentage of Completion column before the most recent change was made to it.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Percentage of Completion", Enabled = false)]
  public virtual Decimal? LastPercentCompleted { get; set; }

  /// <summary>
  /// The current approximate percentage of project completion that corresponds to the cost budget line.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Percentage of Completion")]
  public virtual Decimal? PercentCompleted { get; set; }

  /// <summary>
  /// The value of the Cost at Completion column before the most recent change was made to it.
  /// The value is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.lastCostAtCompletion))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost at Completion", Enabled = false)]
  public virtual Decimal? CuryLastCostAtCompletion { get; set; }

  /// <summary>
  /// The value of the Cost at Completion column before the most recent change was made to it in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost at Completion in Base Currency", Enabled = false)]
  public virtual Decimal? LastCostAtCompletion { get; set; }

  /// <summary>
  /// The current projected total cost amount of the cost budget line in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.costAtCompletion))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost at Completion")]
  public virtual Decimal? CuryCostAtCompletion { get; set; }

  /// <summary>
  /// The current projected total cost amount of the cost budget line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cost at Completion")]
  public virtual Decimal? CostAtCompletion { get; set; }

  /// <summary>The counter of budget lines.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// An internal field that is used to arrange the records on the Revenue Budget and Cost Budget tabs of the Projects (PM301000) form.
  /// </summary>
  [PXInt]
  public virtual int? SortOrder
  {
    get => new int?(this._SortOrder);
    set => this._SortOrder = value.GetValueOrDefault();
  }

  /// <summary>
  /// The projected percentage of completion for the cost budget line in the last
  /// released revision of the cost projection for this line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Completed (%)", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CostProjectionCompletedPct { get; set; }

  /// <summary>
  /// The remainder of the budgeted quantity for the cost budget line in the last
  /// released revision of the cost projection for this line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Quantity to Complete", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CostProjectionQtyToComplete { get; set; }

  /// <summary>
  /// The projected final quantity at project completion for the cost budget line
  /// in the last released revision of the cost projection for this line.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Quantity at Completion", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CostProjectionQtyAtCompletion { get; set; }

  /// <summary>
  /// The remainder of the budgeted cost for the cost budget line in the last
  /// released revision of the cost projection for this line.
  /// The value is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.costProjectionCostToComplete))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost to Complete", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CuryCostProjectionCostToComplete { get; set; }

  /// <summary>
  /// The remainder of the budgeted cost for the cost budget line in the last released
  /// revision of the cost projection for this line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost To Complete in Base Currency", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CostProjectionCostToComplete { get; set; }

  /// <summary>
  /// The projected final cost at project completion for the cost budget line
  /// in the last released revision of the cost projection for this line.
  /// The value is shown in the project currency.
  /// </summary>
  [PXDBCurrency(typeof (PMBudget.curyInfoID), typeof (PMBudget.costProjectionCostAtCompletion))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CuryCostProjectionCostAtCompletion { get; set; }

  /// <summary>
  /// The projected final cost at project completion for the cost budget line in the last released
  /// revision of the cost projection for this line in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Cost at Completion in Base Currency", Enabled = false, FieldClass = "Construction")]
  public virtual Decimal? CostProjectionCostAtCompletion { get; set; }

  /// <summary>
  /// The value that the system uses as the basis for progress billing of the project task.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"A"</c>: Amount,
  /// <c>"Q"</c>: Quantity
  /// </value>
  [PXDBString]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Progress Billing Basis")]
  [PX.Objects.PM.ProgressBillingBase.List]
  public virtual string ProgressBillingBase { get; set; }

  /// <summary>
  /// The type of use of the budget line in the <see cref="T:PX.Objects.PM.PMProgressWorksheet">progress worksheet document</see>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"N"</c>: Not Allowed,
  /// <c>"D"</c>: On Demand,
  /// <c>"T"</c>: Template
  /// </value>
  [PXDBString(1)]
  [PXDefault("N")]
  [PMProductivityTrackingType.List]
  [PXUIField(DisplayName = "Productivity Tracking", FieldClass = "Construction")]
  public virtual string ProductivityTracking { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : 
    PrimaryKeyOf<PMBudget>.By<PMBudget.projectID, PMBudget.projectTaskID, PMBudget.accountGroupID, PMBudget.costCodeID, PMBudget.inventoryID>
  {
    public static PMBudget Find(
      PXGraph graph,
      int? projectID,
      int? projectTaskID,
      int? accountGroupID,
      int? costCodeID,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (PMBudget) PrimaryKeyOf<PMBudget>.By<PMBudget.projectID, PMBudget.projectTaskID, PMBudget.accountGroupID, PMBudget.costCodeID, PMBudget.inventoryID>.FindBy(graph, (object) projectID, (object) projectTaskID, (object) accountGroupID, (object) costCodeID, (object) inventoryID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public static class FK
  {
    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMBudget>.By<PMBudget.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMBudget>.By<PMBudget.projectID, PMBudget.projectTaskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMBudget>.By<PMBudget.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMBudget>.By<PMBudget.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMBudget>.By<PMBudget.inventoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudget.selected>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.costCodeID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.inventoryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.type>
  {
  }

  public abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.revenueTaskID>
  {
  }

  public abstract class revenueTaskIDApiEndPoint : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMBudget.revenueTaskIDApiEndPoint>
  {
  }

  public abstract class revenueInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.revenueInventoryID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.taxCategoryID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMBudget.curyInfoID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.description>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.qty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.uOM>
  {
  }

  public abstract class curyUnitRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.curyUnitRate>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.rate>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.unitPrice>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.amount>
  {
  }

  public abstract class revisedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.revisedQty>
  {
  }

  public abstract class curyRevisedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyRevisedAmount>
  {
  }

  public abstract class revisedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.revisedAmount>
  {
  }

  public abstract class invoicedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.invoicedQty>
  {
  }

  public abstract class curyInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyInvoicedAmount>
  {
  }

  public abstract class invoicedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.invoicedAmount>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.actualQty>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyActualAmount>
  {
  }

  public abstract class actualAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.actualAmount>
  {
  }

  public abstract class baseActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.baseActualAmount>
  {
  }

  public abstract class curyInclTaxAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyInclTaxAmount>
  {
  }

  public abstract class inclTaxAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.inclTaxAmount>
  {
  }

  public abstract class draftChangeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.draftChangeOrderQty>
  {
  }

  public abstract class curyDraftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyDraftChangeOrderAmount>
  {
  }

  public abstract class draftChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.draftChangeOrderAmount>
  {
  }

  public abstract class changeOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.changeOrderQty>
  {
  }

  public abstract class curyChangeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyChangeOrderAmount>
  {
  }

  public abstract class changeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.changeOrderAmount>
  {
  }

  public abstract class committedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.committedQty>
  {
  }

  public abstract class curyCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedAmount>
  {
  }

  public abstract class committedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedAmount>
  {
  }

  public abstract class committedCOQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.committedCOQty>
  {
  }

  public abstract class curyCommittedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedCOAmount>
  {
  }

  public abstract class committedCOAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedCOAmount>
  {
  }

  public abstract class committedOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedOrigQty>
  {
  }

  public abstract class curyCommittedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedOrigAmount>
  {
  }

  public abstract class committedOrigAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedOrigAmount>
  {
  }

  public abstract class committedOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedOpenQty>
  {
  }

  public abstract class curyCommittedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedOpenAmount>
  {
  }

  public abstract class committedOpenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedOpenAmount>
  {
  }

  public abstract class committedReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedReceivedQty>
  {
  }

  public abstract class committedInvoicedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedInvoicedQty>
  {
  }

  public abstract class curyCommittedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCommittedInvoicedAmount>
  {
  }

  public abstract class committedInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.committedInvoicedAmount>
  {
  }

  public abstract class curyActualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyActualPlusOpenCommittedAmount>
  {
  }

  public abstract class actualPlusOpenCommittedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.actualPlusOpenCommittedAmount>
  {
  }

  public abstract class curyVarianceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyVarianceAmount>
  {
  }

  public abstract class varianceAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.varianceAmount>
  {
  }

  public abstract class performance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.performance>
  {
  }

  public abstract class isProduction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudget.isProduction>
  {
  }

  public abstract class mode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBudget.mode>
  {
  }

  public abstract class completedPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.completedPct>
  {
  }

  public abstract class qtyToInvoice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.qtyToInvoice>
  {
  }

  public abstract class curyAmountToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyAmountToInvoice>
  {
  }

  public abstract class amountToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.amountToInvoice>
  {
  }

  public abstract class prepaymentPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.prepaymentPct>
  {
  }

  public abstract class curyPrepaymentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyPrepaymentAmount>
  {
  }

  public abstract class prepaymentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.prepaymentAmount>
  {
  }

  public abstract class curyPrepaymentAvailable : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyPrepaymentAvailable>
  {
  }

  public abstract class prepaymentAvailable : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.prepaymentAvailable>
  {
  }

  public abstract class curyPrepaymentInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyPrepaymentInvoiced>
  {
  }

  public abstract class prepaymentInvoiced : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.prepaymentInvoiced>
  {
  }

  public abstract class limitQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudget.limitQty>
  {
  }

  public abstract class limitAmount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBudget.limitAmount>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.maxQty>
  {
  }

  public abstract class curyMaxAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.curyMaxAmount>
  {
  }

  public abstract class maxAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.maxAmount>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.retainagePct>
  {
  }

  /// <exclude />
  public abstract class curyRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class retainedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.retainedAmount>
  {
  }

  /// <exclude />
  public abstract class curyDraftRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyDraftRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class draftRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.draftRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class curyTotalRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyTotalRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class totalRetainedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.totalRetainedAmount>
  {
  }

  /// <exclude />
  public abstract class retainageMaxPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.retainageMaxPct>
  {
  }

  public abstract class curyLastCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyLastCostToComplete>
  {
  }

  public abstract class lastCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.lastCostToComplete>
  {
  }

  public abstract class curyCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCostToComplete>
  {
  }

  public abstract class costToComplete : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMBudget.costToComplete>
  {
  }

  public abstract class lastPercentCompleted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.lastPercentCompleted>
  {
  }

  public abstract class percentCompleted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.percentCompleted>
  {
  }

  public abstract class curyLastCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyLastCostAtCompletion>
  {
  }

  public abstract class lastCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.lastCostAtCompletion>
  {
  }

  public abstract class curyCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCostAtCompletion>
  {
  }

  public abstract class costAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costAtCompletion>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.lineCntr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBudget.sortOrder>
  {
  }

  public abstract class costProjectionCompletedPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costProjectionCompletedPct>
  {
  }

  public abstract class costProjectionQtyToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costProjectionQtyToComplete>
  {
  }

  public abstract class costProjectionQtyAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costProjectionQtyAtCompletion>
  {
  }

  public abstract class curyCostProjectionCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCostProjectionCostToComplete>
  {
  }

  public abstract class costProjectionCostToComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costProjectionCostToComplete>
  {
  }

  public abstract class curyCostProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.curyCostProjectionCostAtCompletion>
  {
  }

  public abstract class costProjectionCostAtCompletion : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMBudget.costProjectionCostAtCompletion>
  {
  }

  public abstract class progressBillingBase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudget.progressBillingBase>
  {
  }

  public abstract class productivityTracking : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudget.productivityTracking>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudget.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMBudget.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudget.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudget.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudget.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBudget.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBudget.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBudget.lastModifiedDateTime>
  {
  }
}
