// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTran
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
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a project transaction.
/// The transactions are grouped in <see cref="T:PX.Objects.PM.PMRegister">batches</see> and edited through the
/// Project Transactions (PM304000) form (which corresponds to the <see cref="T:PX.Objects.PM.RegisterEntry" /> graph).
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (RegisterEntry)}, new System.Type[] {typeof (Select<PMRegister, Where<PMRegister.refNbr, Equal<Current<PMTran.refNbr>>>>)})]
[PXCacheName("Project Transaction")]
[PXGroupMask(typeof (LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PMTran.offsetAccountID>>, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMTran.accountGroupID>>, LeftJoin<RegisterReleaseProcess.OffsetPMAccountGroup, On<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, Equal<PX.Objects.GL.Account.accountGroupID>>>>>), WhereRestriction = typeof (Where2<Where<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, IsNull, Or<Match<RegisterReleaseProcess.OffsetPMAccountGroup, Current<AccessInfo.userName>>>>, And<Where<PMAccountGroup.groupID, IsNull, Or<Match<PMAccountGroup, Current<AccessInfo.userName>>>>>>))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IProjectFilter, IQuantify
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected long? _TranID;
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected DateTime? _Date;
  protected string _FinPeriodID;
  protected DateTime? _TranDate;
  protected string _TranPeriodID;
  protected int? _ProjectID;
  protected bool? _NonProject;
  protected string _BaseType;
  protected int? _TaskID;
  protected int? _AccountGroupID;
  protected int? _CostCodeID;
  protected int? _ResourceID;
  protected int? _BAccountID;
  protected int? _LocationID;
  protected int? _InventoryID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected bool? _Billable;
  protected bool? _UseBillableQty;
  protected Decimal? _BillableQty;
  protected Decimal? _InvoicedQty;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _OffsetSubID;
  protected bool? _Allocated;
  protected bool? _Released;
  protected string _BatchNbr;
  protected string _OrigModule;
  protected string _OrigTranType;
  protected string _OrigRefNbr;
  protected int? _OrigLineNbr;
  protected string _BillingID;
  protected string _AllocationID;
  protected bool? _Billed;
  protected DateTime? _BilledDate;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Guid? _OrigRefID;
  protected bool? _IsNonGL;
  protected bool? _IsQtyOnly;
  protected string _Reverse;
  protected string _ARTranType;
  protected string _ARRefNbr;
  protected int? _RefLineNbr;
  protected int? _OrigProjectID;
  protected int? _OrigTaskID;
  protected int? _OrigAccountGroupID;
  protected string _ExtRefNbr;
  protected bool? _IsFree = new bool?(false);
  protected Decimal? _Proportion;
  protected bool? _Skip = new bool?(false);
  protected string _Prefix;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _CreatedByCurrentAllocation = new bool?(false);

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.GL.Branch" /> to which the transaction belongs.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The unique identifier of the project transaction.</summary>
  [PXUIField(DisplayName = "PM Tran.", Visible = false, Enabled = false)]
  [PXDBLongIdentity(IsKey = true)]
  public virtual long? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  /// <summary>The identifier of the functional area to which the transaction belongs.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.PM.PMRegister.Module">module of the parent batch</see>.
  /// </value>
  [PXDefault(typeof (PMRegister.module))]
  [PXDBString(2, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <summary>The number of the <see cref="T:PX.Objects.PM.PMRegister" /> to which the transaction belongs.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.PM.PMRegister.RefNbr" /> field.
  /// </value>
  [PXUIField(DisplayName = "Ref. Number")]
  [PXDBDefault(typeof (PMRegister.refNbr))]
  [PXDBString(15, IsUnicode = true)]
  [PXParent(typeof (Select<PMRegister, Where<PMRegister.module, Equal<Current<PMTran.tranType>>, And<PMRegister.refNbr, Equal<Current<PMTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The date of the transaction, which is specified by the user.</summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  /// <summary>An identifier of the company-specific financial period to which the transaction belongs.</summary>
  /// <value>Defaults to the period to which the <see cref="P:PX.Objects.PM.PMTran.Date" /> belongs. The value can be overriden by the user.</value>
  [OpenPeriod(null, typeof (PMTran.date), typeof (PMTran.branchID), null, null, null, null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (PMTran.tranPeriodID), true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>The date of the transaction.</summary>
  /// <value>Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.</value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  /// <summary>The financial period in the master calendar.</summary>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the transaction, or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see> indicating that the transaction is
  /// not related to any particular project.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMTran.projectID>.IsRelatedTo<PMProject.contractID>))]
  [ActiveProjectOrContractBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMProject.nonProject, Where<PMProject.contractID, Equal<Current<PMTran.projectID>>>>))]
  [PXFormula(typeof (Default<PMTran.projectID>))]
  public virtual bool? NonProject
  {
    get => this._NonProject;
    set => this._NonProject = value;
  }

  [PXDefault(typeof (Search<PMProject.baseType, Where<PMProject.contractID, Equal<Current<PMTran.projectID>>>>))]
  [PXFormula(typeof (Default<PMTran.projectID>))]
  public virtual string BaseType
  {
    get => this._BaseType;
    set => this._BaseType = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMTask.TaskID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<PMTran.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [BaseProjectTask(typeof (PMTran.projectID), typeof (PMTran.baseType), typeof (PMTran.nonProject), AllowInactive = false)]
  [PXForeignReference(typeof (CompositeKey<Field<PMTran.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMTran.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">Account Group</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXForeignReference(typeof (Field<PMTran.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [AccountGroup(typeof (Where<Match<PMAccountGroup, Current<AccessInfo.userName>>>))]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [Account(null, DisplayName = "Credit Account", AvoidControlAccounts = true)]
  public virtual int? OffsetAccountID { get; set; }

  [PXForeignReference(typeof (Field<PMTran.offsetAccountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [AccountGroup(typeof (Where<Match<PMAccountGroup, Current<AccessInfo.userName>>>), DisplayName = "Credit Account Group", Enabled = false)]
  public virtual int? OffsetAccountGroupID { get; set; }

  /// <summary>The identifier of the offset account group. Migrated from offset account group for backword compatibility.
  /// Eventually will be removed in future when no longer needed.
  /// </summary>
  [PXForeignReference(typeof (Field<PMTran.migrationOffsetAccountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  [PXDBInt]
  public virtual int? MigrationOffsetAccountGroupID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [CostCode(null, typeof (PMTran.taskID), "E", typeof (PMTran.accountGroupID), ReleasedField = typeof (PMTran.released), ProjectField = typeof (PMTran.projectID), InventoryField = typeof (PMTran.inventoryID), UseNewDefaulting = true, AllowNullValueIfReleased = true)]
  [PXForeignReference(typeof (Field<PMTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount">employee</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.BAccount.bAccountID" /> field.
  /// </value>
  [PXEPEmployeeSelector]
  [PXDBInt]
  [PXUIField(DisplayName = "Employee")]
  [PXForeignReference(typeof (Field<PMTran.resourceID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? ResourceID
  {
    get => this._ResourceID;
    set => this._ResourceID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount">vendor or customer</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.BAccount.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Customer/Vendor")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<MatchUser>, BAccountR>.SearchFor<BAccountR.bAccountID>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type), typeof (BAccountR.parentBAccountID), typeof (PX.Objects.CR.BAccount.ownerID), typeof (PX.Objects.CR.BAccount.acctReferenceNbr)}, SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [CustomerVendorRestrictor]
  [PXForeignReference(typeof (Field<PMTran.bAccountID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Location">location</see> of the customer or vendor associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.CR.BAccount.defLocationID, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PMTran.bAccountID>>>>))]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMTran.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">stock or non-stock item</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDBInt]
  [PMInventorySelector]
  [PXForeignReference(typeof (Field<PMTran.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The description provided for the transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>A description, which is generated during the billing based on the <see cref="P:PX.Objects.PM.PMBillingRule.DescriptionFormula">line description formula</see> specified in the billing rule.
  /// The value is used to generate the description for the corresponding pro forma invoice line.</summary>
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string InvoicedDescription { get; set; }

  /// <summary>The <see cref="T:PX.Objects.IN.INUnit">unit of measure</see> used to estimate the <see cref="P:PX.Objects.PM.PMTran.Qty">quantity</see> for the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INUnit.fromUnit" /> field.
  /// </value>
  [PMUnit(typeof (PMTran.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The quantity of the transaction.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  [PXFormula(null, typeof (SumCalc<PMRegister.qtyTotal>))]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the transaction is used in calculating the amount charged to the customer.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? Billable
  {
    get => this._Billable;
    set => this._Billable = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the system uses the <see cref="P:PX.Objects.PM.PMTran.BillableQty">billable quantity</see> instead of the <see cref="P:PX.Objects.PM.PMTran.Qty">overall quantity</see> of the transaction when
  /// calculating the amount of the transaction.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Billable Quantity in Amount Formula")]
  public virtual bool? UseBillableQty
  {
    get => this._UseBillableQty;
    set => this._UseBillableQty = value;
  }

  /// <summary>The quantity that is used for billing the customer.</summary>
  [PXDBQuantity]
  [PXDefault(typeof (PMTran.qty))]
  [PXUIField(DisplayName = "Billable Quantity")]
  [PXFormula(null, typeof (SumCalc<PMRegister.billableQtyTotal>))]
  public virtual Decimal? BillableQty
  {
    get => this._BillableQty;
    set => this._BillableQty = value;
  }

  /// <summary>The quantity to bill the customer. The quanity is provided by the billing rule.</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Quantity", Enabled = false)]
  public virtual Decimal? InvoicedQty
  {
    get => this._InvoicedQty;
    set => this._InvoicedQty = value;
  }

  /// <summary>
  /// The identifier of the transaction <see cref="T:PX.Objects.CM.Extensions.CurrencyList">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.CuryID" /> field.
  /// </value>
  [PXDefault]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency", FieldClass = "ProjectMultiCurrency")]
  public virtual string TranCuryID { get; set; }

  /// <summary>The project currency.</summary>
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Project Currency", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual string ProjectCuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record
  /// that stores exchange rate from the <see cref="P:PX.Objects.PM.PMTran.TranCuryID">transaction currency</see> to the base currency.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? BaseCuryInfoID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record that stores
  /// exchange rate from the <see cref="P:PX.Objects.PM.PMTran.TranCuryID">transaction currency</see> to the <see cref="P:PX.Objects.PM.PMTran.ProjectCuryID">project currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? ProjectCuryInfoID { get; set; }

  /// <summary>
  /// The exchange rate from the <see cref="P:PX.Objects.PM.PMTran.TranCuryID">transaction currency</see> to the base currency.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Base Currency Rate", Enabled = false, Visible = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? BaseCuryRate { get; set; }

  /// <summary>
  /// The exchange rate from the <see cref="P:PX.Objects.PM.PMTran.TranCuryID">transaction currency</see> to the <see cref="P:PX.Objects.PM.PMTran.ProjectCuryID">project currency</see>.
  /// </summary>
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Project Currency Rate", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? ProjectCuryRate { get; set; }

  /// <summary>The price of the item or the rate of the service in the transaction currency. For a labor item, the employee's hourly rate is used as the unit rate.</summary>
  [PXDBCurrencyPriceCost(typeof (PMTran.baseCuryInfoID), typeof (PMTran.unitRate))]
  [PXUIField(DisplayName = "Unit Rate")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranCuryUnitRate { get; set; }

  /// <summary>The price of the item or the rate of the service in the base currency of the tenant. For a labor item, the employee's hourly rate is used as the unit rate.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitRate { get; set; }

  /// <summary>
  /// The amount of the transaction in the transaction currency.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  [PXFormula(typeof (Mult<Switch<Case<Where<PMTran.useBillableQty, Equal<True>>, PMTran.billableQty>, PMTran.qty>, PMTran.tranCuryUnitRate>))]
  [PXDBCurrency(typeof (PMTran.baseCuryInfoID), typeof (PMTran.amount))]
  public virtual Decimal? TranCuryAmount { get; set; }

  /// <summary>
  /// The amount of the transaction in the base currency of the tenant.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<PMRegister.amtTotal>))]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The amount of the transaction in the transaction currency.
  /// </summary>
  /// <remarks>
  /// This is a technical field that is a copy of the TranCuryAmount field.
  /// Used to automatic conversion of amount from the transaction currency to the project currency.
  /// </remarks>
  [PXFormula(typeof (PMTran.tranCuryAmount))]
  [PXCurrency(typeof (PMTran.projectCuryInfoID), typeof (PMTran.projectCuryAmount))]
  public virtual Decimal? TranCuryAmountCopy { get; set; }

  /// <summary>
  /// The amount of the transaction in the project currency.
  /// </summary>
  [PXDBProjectCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Project Currency Amount", Enabled = false, FieldClass = "ProjectMultiCurrency")]
  public virtual Decimal? ProjectCuryAmount { get; set; }

  /// <summary>The amount to bill the customer in the project currency. The amount is provided by the billing rule.</summary>
  [PXDBProjectCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Amount", Enabled = false)]
  public virtual Decimal? ProjectCuryInvoicedAmount { get; set; }

  /// <summary>The amount to bill the customer (in the base currency of the tenant). The amount is provided by the billing rule.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? InvoicedAmount { get; set; }

  /// <summary>
  /// The identifier of the debit <see cref="T:PX.Objects.GL.Account" /> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, typeof (Search2<PX.Objects.GL.Account.accountID, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<Current<PMTran.accountGroupID>>>>, Where<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>, And<PX.Objects.GL.Account.accountGroupID, Equal<Current<PMTran.accountGroupID>>, Or<PMAccountGroup.type, Equal<PMAccountType.offBalance>, Or<PMAccountGroup.groupID, IsNull>>>>>), DisplayName = "Debit Account", AvoidControlAccounts = true)]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>The identifier of the debit <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (PMTran.accountID), typeof (PMTran.branchID), false, DisplayName = "Debit Subaccount")]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>The identifier of the credit <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (PMTran.offsetAccountID), typeof (PMTran.branchID), false, DisplayName = "Credit Subaccount")]
  public virtual int? OffsetSubID
  {
    get => this._OffsetSubID;
    set => this._OffsetSubID = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the transaction</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allocated", Enabled = false)]
  public virtual bool? Allocated
  {
    get => this._Allocated;
    set => this._Allocated = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the transaction is excluded from the allocation.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded from Allocation")]
  public virtual bool? ExcludedFromAllocation { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the transaction has been released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.GL.Batch">GL Batch</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "GL Batch Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<Current<PMTran.tranType>>>>))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <summary>
  /// The identifier of the functional area to which the GL batch that spawned the transaction belongs.
  /// </summary>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "OrigModule")]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>The type of the original document.</summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "OrigTranType")]
  public virtual string OrigTranType
  {
    get => this._OrigTranType;
    set => this._OrigTranType = value;
  }

  /// <summary>The reference number of the original document.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "OrigRefNbr")]
  public virtual string OrigRefNbr
  {
    get => this._OrigRefNbr;
    set => this._OrigRefNbr = value;
  }

  /// <summary>The line number in the original document.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "OrigLineNbr")]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  /// <summary>
  /// The identifier of the billing rule associated with the transaction.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string BillingID
  {
    get => this._BillingID;
    set => this._BillingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAllocation">allocation rule</see> associated with the transaction.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AllocationID")]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the transaction has been billed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billed", Enabled = false)]
  public virtual bool? Billed
  {
    get => this._Billed;
    set => this._Billed = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the transaction is excluded from the billing.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded from Billing", Visible = false, Enabled = false)]
  public virtual bool? ExcludedFromBilling { get; set; }

  /// <summary>The reason of exclusion from the billing.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Excluded from Billing Reason", Visible = false, Enabled = false)]
  [PXFieldDescription]
  public virtual string ExcludedFromBillingReason { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the transaction is excluded from the balance.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded from Balance", Visible = false, Enabled = false)]
  public virtual bool? ExcludedFromBalance { get; set; }

  /// <summary>The date on which the transaction was billed.</summary>
  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Billed Date")]
  public virtual DateTime? BilledDate
  {
    get => this._BilledDate;
    set => this._BilledDate = value;
  }

  /// <summary>The transaction start date.</summary>
  [PXDefault(typeof (PMTran.date))]
  [PXDBDate]
  [PXUIField(DisplayName = "Start Date", Visible = false)]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The transaction end date.</summary>
  [PXDefault(typeof (PMTran.date))]
  [PXDBDate]
  [PXUIField(DisplayName = "End Date", Visible = false)]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>The Case.NoteID for contracts and CRActivity.NoteID for time cards and time sheets.</summary>
  [PXDBGuid(false)]
  public virtual Guid? OrigRefID
  {
    get => this._OrigRefID;
    set => this._OrigRefID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsNonGL
  {
    get => this._IsNonGL;
    set => this._IsNonGL = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the transaction contains only quantity data and no price and amount data. For example, CRM records contain
  /// only usage data without price information. The price is determined later during the billing process.</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsQtyOnly
  {
    get => this._IsQtyOnly;
    set => this._IsQtyOnly = value;
  }

  /// <summary>
  /// An option that indicates when the allocation transaction should be reversed.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"I"</c>: On AR Invoice Release,
  /// <c>"B"</c>: On AR Invoice Generation,
  /// <c>"N"</c>: Never
  /// </value>
  [PMReverse.List]
  [PXDefault("N")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Reverse")]
  public virtual string Reverse
  {
    get => this._Reverse;
    set => this._Reverse = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.EP.EPEarningType">earning type</see>, which is specified for the transaction to calculate the labor cost.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.EP.EPEarningType.typeCD" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (EPEarningType.typeCD), DescriptionField = typeof (EPEarningType.description))]
  [PXUIField(DisplayName = "Earning Type", Enabled = false)]
  [PXForeignReference(typeof (Field<PMTran.earningType>.IsRelatedTo<EPEarningType.typeCD>))]
  public virtual string EarningType { get; set; }

  /// <summary>
  /// The multiplier by which the <see cref="P:PX.Objects.PM.PMTran.TranCuryUnitRate">unit rate</see> is multiplied when the labor cost is calculated.
  /// The multiplier can differ from 1 only for <see cref="P:PX.Objects.PM.PMTran.EarningType">earning types</see> marked as overtime.
  /// </summary>
  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Multiplier", Enabled = false)]
  public virtual Decimal? OvertimeMultiplier { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRCase">case</see> whose billing resulted in this transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.CRCase.CaseCD" /> field.
  /// </value>
  [PXSelector(typeof (Search<CRCase.caseCD>))]
  [PXDBString(15)]
  [PXUIField(DisplayName = "Case ID", Visible = false, Enabled = false)]
  public virtual string CaseCD { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMUnion">union local</see> associated with the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMUnion.UnionID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMTran.unionID>.IsRelatedTo<PMUnion.unionID>))]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Union Local", FieldClass = "Construction", Enabled = false)]
  public virtual string UnionID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMWorkCode">WCC code</see> associated with the transaction.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMWorkCode.WorkCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMTran.workCodeID>.IsRelatedTo<PMWorkCode.workCodeID>))]
  [PXDBString(15)]
  [PXUIField(DisplayName = "WCC Code", FieldClass = "Construction", Enabled = false)]
  public virtual string WorkCodeID { get; set; }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"INV"</c>: Invoice,
  /// <c>"DRM"</c>: Debit Memo,
  /// <c>"CRM"</c>: Credit Memo,
  /// <c>"FCH"</c>: Overdue Charge,
  /// <c>"SMC"</c>: Credit WO
  /// </value>
  [ARInvoiceType.List]
  [PXDBString(3, IsFixed = true)]
  public virtual string ARTranType
  {
    get => this._ARTranType;
    set => this._ARTranType = value;
  }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr>))]
  public virtual string ARRefNbr
  {
    get => this._ARRefNbr;
    set => this._ARRefNbr = value;
  }

  /// <summary>
  /// The line number in the corresponding accounts receivable document associated with the transaction.
  /// </summary>
  [PXDBInt]
  public virtual int? RefLineNbr
  {
    get => this._RefLineNbr;
    set => this._RefLineNbr = value;
  }

  /// <summary>The original project ID.</summary>
  [PXDBInt]
  public virtual int? OrigProjectID
  {
    get => this._OrigProjectID;
    set => this._OrigProjectID = value;
  }

  /// <summary>The original task ID.</summary>
  [PXDBInt]
  public virtual int? OrigTaskID
  {
    get => this._OrigTaskID;
    set => this._OrigTaskID = value;
  }

  /// <summary>The original account group ID.</summary>
  [PXDBInt]
  public virtual int? OrigAccountGroupID
  {
    get => this._OrigAccountGroupID;
    set => this._OrigAccountGroupID = value;
  }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see> associated with the transaction.
  /// </summary>
  [PXUIField(DisplayName = "Pro Forma Ref. Nbr.")]
  [PXDBString(15, IsUnicode = true)]
  public virtual string ProformaRefNbr { get; set; }

  /// <summary>
  /// The line number in the corresponding pro forma invoice associated with the transaction.
  /// </summary>
  [PXDBInt]
  public virtual int? ProformaLineNbr { get; set; }

  /// <summary>The reference number of the external document.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr.")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>The reference to the original transaction if a reversal is created.</summary>
  [PXDBLong]
  public virtual long? OrigTranID { get; set; }

  /// <summary>A remainder, which holds the reference to the original transaction when the remainder is created.</summary>
  [PXDBLong]
  public virtual long? RemainderOfTranID { get; set; }

  /// <summary>The identifier of the project transaction. When a credit memo is created as the result of project's invoice reversal, a copy of the original billable
  /// transaction is created so that it can be billed again.</summary>
  [Obsolete("Will be removed in 2020R2")]
  [PXDBLong]
  public virtual long? DuplicateOfTranID { get; set; }

  [Obsolete]
  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  [Obsolete]
  [PXDecimal]
  public virtual Decimal? Proportion
  {
    get => this._Proportion;
    set => this._Proportion = value;
  }

  [Obsolete]
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Skip
  {
    get => this._Skip;
    set => this._Skip = value;
  }

  [Obsolete]
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string Prefix
  {
    get => this._Prefix;
    set => this._Prefix = value;
  }

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

  [PXBool]
  public bool? IsInverted { get; set; }

  [PXBool]
  public bool? IsCreditPair { get; set; }

  /// <summary>The rate.</summary>
  public Decimal? Rate { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the transaction was created during the current allocation process.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? CreatedByCurrentAllocation
  {
    get => this._CreatedByCurrentAllocation;
    set => this._CreatedByCurrentAllocation = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : PrimaryKeyOf<PMTran>.By<PMTran.tranID>
  {
    public static PMTran Find(PXGraph graph, long? tranID, PKFindOptions options = 0)
    {
      return (PMTran) PrimaryKeyOf<PMTran>.By<PMTran.tranID>.FindBy(graph, (object) tranID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public static class FK
  {
    /// <summary>Project</summary>
    /// <exclude />
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMTran>.By<PMTran.projectID>
    {
    }

    /// <summary>Project Task</summary>
    /// <exclude />
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMTran>.By<PMTran.projectID, PMTran.taskID>
    {
    }

    /// <summary>Account Group</summary>
    /// <exclude />
    public class AccountGroup : 
      PrimaryKeyOf<PMAccountGroup>.By<PMAccountGroup.groupID>.ForeignKeyOf<PMTran>.By<PMTran.accountGroupID>
    {
    }

    /// <summary>Cost Code</summary>
    /// <exclude />
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMTran>.By<PMTran.costCodeID>
    {
    }

    /// <summary>Inventory Item</summary>
    /// <exclude />
    public class Item : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMTran>.By<PMTran.inventoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.branchID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.tranID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.refNbr>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTran.date>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.finPeriodID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTran.tranDate>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.tranPeriodID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.projectID>
  {
  }

  public abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.nonProject>
  {
  }

  public abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.baseType>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.taskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.accountGroupID>
  {
  }

  public abstract class offsetAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.offsetAccountID>
  {
  }

  public abstract class offsetAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTran.offsetAccountGroupID>
  {
  }

  public abstract class migrationOffsetAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTran.migrationOffsetAccountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.costCodeID>
  {
  }

  public abstract class resourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.resourceID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.inventoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.description>
  {
  }

  public abstract class invoicedDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTran.invoicedDescription>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.qty>
  {
  }

  public abstract class billable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.billable>
  {
  }

  public abstract class useBillableQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.useBillableQty>
  {
  }

  public abstract class billableQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.billableQty>
  {
  }

  public abstract class invoicedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.invoicedQty>
  {
  }

  public abstract class tranCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.tranCuryID>
  {
  }

  public abstract class projectCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.projectCuryID>
  {
  }

  public abstract class baseCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.baseCuryInfoID>
  {
  }

  public abstract class projectCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.projectCuryInfoID>
  {
  }

  public abstract class baseCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.baseCuryRate>
  {
  }

  public abstract class projectCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.projectCuryRate>
  {
  }

  public abstract class tranCuryUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTran.tranCuryUnitRate>
  {
  }

  public abstract class unitRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.unitRate>
  {
  }

  public abstract class tranCuryAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.tranCuryAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.amount>
  {
  }

  public abstract class tranCuryAmountCopy : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTran.tranCuryAmountCopy>
  {
  }

  public abstract class projectCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTran.projectCuryAmount>
  {
  }

  public abstract class projectCuryInvoicedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTran.projectCuryInvoicedAmount>
  {
  }

  public abstract class invoicedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.invoicedAmount>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.subID>
  {
  }

  public abstract class offsetSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.offsetSubID>
  {
  }

  public abstract class allocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.allocated>
  {
  }

  public abstract class excludedFromAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTran.excludedFromAllocation>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.released>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.batchNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.origModule>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.origTranType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.origRefNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.origLineNbr>
  {
  }

  public abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.billingID>
  {
  }

  public abstract class allocationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.allocationID>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.billed>
  {
  }

  public abstract class excludedFromBilling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTran.excludedFromBilling>
  {
  }

  public abstract class excludedFromBillingReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTran.excludedFromBillingReason>
  {
  }

  public abstract class excludedFromBalance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTran.excludedFromBalance>
  {
  }

  public abstract class billedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTran.billedDate>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTran.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMTran.endDate>
  {
  }

  public abstract class origRefID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTran.origRefID>
  {
  }

  public abstract class isNonGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.isNonGL>
  {
  }

  public abstract class isQtyOnly : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.isQtyOnly>
  {
  }

  public abstract class reverse : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.reverse>
  {
  }

  public abstract class earningType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.earningType>
  {
  }

  public abstract class overtimeMultiplier : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTran.overtimeMultiplier>
  {
  }

  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.caseCD>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.unionID>
  {
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.workCodeID>
  {
  }

  public abstract class aRTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.aRTranType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.aRRefNbr>
  {
  }

  public abstract class refLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.refLineNbr>
  {
  }

  public abstract class origProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.origProjectID>
  {
  }

  public abstract class origTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.origTaskID>
  {
  }

  public abstract class origAccountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.origAccountGroupID>
  {
  }

  public abstract class proformaRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.proformaRefNbr>
  {
  }

  public abstract class proformaLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTran.proformaLineNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.extRefNbr>
  {
  }

  public abstract class origTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.origTranID>
  {
  }

  public abstract class remainderOfTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.remainderOfTranID>
  {
  }

  public abstract class duplicateOfTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTran.duplicateOfTranID>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.isFree>
  {
  }

  public abstract class proportion : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTran.proportion>
  {
  }

  public abstract class skip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTran.skip>
  {
  }

  public abstract class prefix : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTran.prefix>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTran.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMTran.lastModifiedDateTime>
  {
  }

  public abstract class createdByCurrentAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTran.createdByCurrentAllocation>
  {
  }
}
