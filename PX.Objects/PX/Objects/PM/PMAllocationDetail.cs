// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocationDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a step of the <see cref="T:PX.Objects.PM.PMAllocation">allocation rule</see>
/// that defines the calculation rules and allocation settings.
/// The records of this type are created and edited through the Allocation Rules (PM207500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.AllocationMaint" /> graph).
/// </summary>
[PXCacheName("Allocation Rule Step")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAllocationDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AllocationID;
  protected int? _StepID;
  protected string _Description;
  protected string _SelectOption;
  protected bool? _Post;
  protected string _DescriptionFormula;
  protected int? _RangeStart;
  protected int? _RangeEnd;
  protected string _RateTypeID;
  protected int? _AccountGroupFrom;
  protected int? _AccountGroupTo;
  protected string _Method;
  protected bool? _UpdateGL;
  protected int? _SourceBranchID;
  protected string _OffsetBranchOrigin;
  protected int? _TargetBranchID;
  protected string _ProjectOrigin;
  protected int? _ProjectID;
  protected string _TaskOrigin;
  protected int? _TaskID;
  protected string _TaskCD;
  protected string _AccountGroupOrigin;
  protected int? _AccountGroupID;
  protected string _AccountOrigin;
  protected int? _AccountID;
  protected string _SubMask;
  protected int? _SubID;
  protected string _OffsetProjectOrigin;
  protected int? _OffsetProjectID;
  protected string _OffsetTaskOrigin;
  protected int? _OffsetTaskID;
  protected string _OffsetTaskCD;
  protected string _OffsetAccountGroupOrigin;
  protected int? _OffsetAccountGroupID;
  protected string _OffsetAccountOrigin;
  protected int? _OffsetAccountID;
  protected string _OffsetSubMask;
  protected int? _OffsetSubID;
  protected string _Reverse;
  protected string _NoRateOption;
  protected string _DateSource;
  protected bool? _GroupByItem;
  protected bool? _GroupByEmployee;
  protected bool? _GroupByDate;
  protected bool? _GroupByVendor;
  protected int? _Allocation;
  protected string _AllocationText;
  protected bool? _AllocateZeroAmount;
  protected bool? _AllocateZeroQty;
  protected bool? _AllocateNonBillable;
  protected bool? _MarkAsNotAllocated;
  protected bool? _CopyNotes;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAllocation">allocation</see> to which this allocation step belongs.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAllocation.allocationID" /> field.
  /// </value>
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault(typeof (PMAllocation.allocationID))]
  [PXParent(typeof (Select<PMAllocation, Where<PMAllocation.allocationID, Equal<Current<PMAllocationDetail.allocationID>>>>))]
  public virtual string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  /// <summary>The unique identifier of the allocation rule.</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Step ID")]
  public virtual int? StepID
  {
    get => this._StepID;
    set => this._StepID = value;
  }

  /// <summary>The description of the step.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// The way the system should select the transactions for allocation.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMSelectOption.ListAttribute" />.
  /// </value>
  [PMSelectOption.List]
  [PXDefault("T")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Select Transactions")]
  public virtual string SelectOption
  {
    get => this._SelectOption;
    set => this._SelectOption = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system creates the allocation transactions.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Allocation Transaction")]
  public virtual bool? Post
  {
    get => this._Post;
    set => this._Post = value;
  }

  /// <summary>
  /// The formula to be used for calculating the quantity for allocation transactions.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Quantity Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string QtyFormula { get; set; }

  /// <summary>
  /// The formula for calculating the billable quantity for allocation transactions.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Billable Qty. Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string BillableQtyFormula { get; set; }

  /// <summary>
  /// The formula for calculating the amount of allocation transactions.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Amount Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string AmountFormula { get; set; }

  /// <summary>
  /// The formula to be used to generate the descriptions for allocation transactions.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Description Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string DescriptionFormula { get; set; }

  /// <summary>The first step of the range.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Range Start")]
  public virtual int? RangeStart
  {
    get => this._RangeStart;
    set => this._RangeStart = value;
  }

  /// <summary>The last step of the range.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Range End")]
  public virtual int? RangeEnd
  {
    get => this._RangeEnd;
    set => this._RangeEnd = value;
  }

  /// <summary>The rate type used in the allocation rule step.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (PMRateType.rateTypeID), DescriptionField = typeof (PMRateType.description))]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  /// <summary>
  /// The account group that starts the range of account groups whose transactions are involved in this allocation step.
  /// </summary>
  [AccountGroup(DisplayName = "Account Group From")]
  public virtual int? AccountGroupFrom
  {
    get => this._AccountGroupFrom;
    set => this._AccountGroupFrom = value;
  }

  /// <summary>
  /// The account group that ends the range of account groups whose transactions are involved in this allocation step.
  /// </summary>
  [AccountGroup(DisplayName = "Account Group To")]
  public virtual int? AccountGroupTo
  {
    get => this._AccountGroupTo;
    set => this._AccountGroupTo = value;
  }

  /// <summary>The method of the allocation.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMMethod.ListAttribute" />.
  /// </value>
  [PMMethod.List]
  [PXDefault("T")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Allocation Method")]
  public virtual string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the allocation transactions should be posted to the general ledger.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Post Transaction to GL")]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Branch">branch</see> of project transactions to be allocated.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? SourceBranchID
  {
    get => this._SourceBranchID;
    set => this._SourceBranchID = value;
  }

  /// <summary>
  /// The source of the <see cref="T:PX.Objects.GL.Branch">branch</see> associated with the project allocation transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Branch", FieldClass = "COMPANYBRANCH")]
  public virtual string OffsetBranchOrigin
  {
    get => this._OffsetBranchOrigin;
    set => this._OffsetBranchOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to be used in project allocation transactions.
  /// </summary>
  [Branch(null, null, true, true, true)]
  public virtual int? TargetBranchID
  {
    get => this._TargetBranchID;
    set => this._TargetBranchID = value;
  }

  /// <summary>
  /// The source of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Project")]
  public virtual string ProjectOrigin
  {
    get => this._ProjectOrigin;
    set => this._ProjectOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [ProjectBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The source of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Project Task")]
  public virtual string TaskOrigin
  {
    get => this._TaskOrigin;
    set => this._TaskOrigin = value;
  }

  /// <summary>
  /// The source of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public virtual string CostCodeOrigin { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMAllocationDetail.projectID), AllowNull = true)]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.PM.PMAllocationDetail.TaskID">project task identifier</see> displayed on the form.
  /// </summary>
  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMAllocationDetail.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, null, null, null, true, false, Filterable = false, SkipVerification = true)]
  public virtual int? CostCodeID { get; set; }

  /// <summary>
  /// The source of <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Account Group")]
  public virtual string AccountGroupOrigin
  {
    get => this._AccountGroupOrigin;
    set => this._AccountGroupOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAccountGroup.accountID" /> field.
  /// </value>
  [AccountGroup(typeof (Where<Current<PMAllocationDetail.updateGL>, Equal<True>, And<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>, Or<Current<PMAllocationDetail.updateGL>, Equal<False>>>>), DisplayName = "Account Group")]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>The account of the allocation.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.DebitAccountListAttribute" />.
  /// </value>
  [PMOrigin.DebitAccountList]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Account Origin")]
  public virtual string AccountOrigin
  {
    get => this._AccountOrigin;
    set => this._AccountOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [Account(null, typeof (Search<Account.accountID, Where<Account.accountGroupID, IsNotNull>>), AvoidControlAccounts = true)]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// The subaccount associated with the allocation's credit transactions.
  /// </summary>
  [PMSubAccountMask]
  public virtual string SubMask
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the allocation's debit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Sub.subID" /> field.
  /// </value>
  [SubAccount(typeof (PMAllocationDetail.accountID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// The source of <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Project")]
  public virtual string OffsetProjectOrigin
  {
    get => this._OffsetProjectOrigin;
    set => this._OffsetProjectOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [ProjectBase]
  public virtual int? OffsetProjectID
  {
    get => this._OffsetProjectID;
    set => this._OffsetProjectID = value;
  }

  /// <summary>.
  /// The source of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Project Task")]
  public virtual string OffsetTaskOrigin
  {
    get => this._OffsetTaskOrigin;
    set => this._OffsetTaskOrigin = value;
  }

  /// <summary>.
  /// The source of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PMOrigin.List]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public virtual string OffsetCostCodeOrigin { get; set; }

  /// <summary>
  /// The identifier of the <see cref="P:PX.Objects.PM.PMAllocationDetail.OffsetTaskOrigin">origin of the offset task</see> associated with the record.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [ProjectTask(typeof (PMAllocationDetail.offsetProjectID), AllowNull = true, DisplayName = "Project Task")]
  public virtual int? OffsetTaskID
  {
    get => this._OffsetTaskID;
    set => this._OffsetTaskID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMTask.taskID" /> field.
  /// </value>
  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string OffsetTaskCD
  {
    get => this._OffsetTaskCD;
    set => this._OffsetTaskCD = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the budget line.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [PXForeignReference(typeof (Field<PMAllocationDetail.offsetCostCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  [CostCode(null, null, null, null, true, false, Filterable = false, SkipVerification = true)]
  public virtual int? OffsetCostCodeID { get; set; }

  /// <summary>
  /// The source of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.ListAttribute" />.
  /// </value>
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Account Group")]
  public virtual string OffsetAccountGroupOrigin
  {
    get => this._OffsetAccountGroupOrigin;
    set => this._OffsetAccountGroupOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAccountGroup.accountID" /> field.
  /// </value>
  [AccountGroup(typeof (Where<Current<PMAllocationDetail.updateGL>, Equal<True>, And<PMAccountGroup.type, NotEqual<PMAccountType.offBalance>, Or<Current<PMAllocationDetail.updateGL>, Equal<False>>>>), DisplayName = "Account Group")]
  public virtual int? OffsetAccountGroupID
  {
    get => this._OffsetAccountGroupID;
    set => this._OffsetAccountGroupID = value;
  }

  /// <summary>The account for the allocation's debit transactions.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMOrigin.CreditAccountListAttribute" />.
  /// </value>
  [PMOrigin.CreditAccountList]
  [PXDefault("S")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Account Origin")]
  public virtual string OffsetAccountOrigin
  {
    get => this._OffsetAccountOrigin;
    set => this._OffsetAccountOrigin = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [Account(null, DisplayName = "Account", AvoidControlAccounts = true)]
  public virtual int? OffsetAccountID
  {
    get => this._OffsetAccountID;
    set => this._OffsetAccountID = value;
  }

  /// <summary>
  /// The subaccount for the allocation's credit transactions.
  /// </summary>
  [PMOffsetSubAccountMask]
  public virtual string OffsetSubMask
  {
    get => this._OffsetSubMask;
    set => this._OffsetSubMask = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the allocation's credit transactions.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (PMAllocationDetail.offsetAccountID), DisplayName = "Subaccount")]
  public virtual int? OffsetSubID
  {
    get => this._OffsetSubID;
    set => this._OffsetSubID = value;
  }

  /// <summary>
  /// The reverse allocation for the allocation transaction.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description><c>I</c>: On AR Invoice Release</description></item>
  /// <item><description><c>B</c>: On AR Invoice Generation</description></item>
  /// <item><description><c>N</c>: Never</description></item>
  /// </list>
  /// </value>
  [PMReverse.List]
  [PXDefault("I")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Reverse Allocation")]
  public virtual string Reverse
  {
    get => this._Reverse;
    set => this._Reverse = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will specify the date and the financial period of the allocation transaction being reversed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Reversal Date from Original Transaction")]
  public virtual bool? UseReversalDateFromOriginal { get; set; }

  /// <summary>
  /// The action that is performed if the value for @Rate has not been defined.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMNoRateOption.AllocationListAttribute" />.
  /// </value>
  [PMNoRateOption.AllocationList]
  [PXDefault("1")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "If @Rate Is Not Defined")]
  public virtual string NoRateOption
  {
    get => this._NoRateOption;
    set => this._NoRateOption = value;
  }

  /// <summary>
  /// The date source, which defines how the date for the allocation transactions is defined.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMDateSource.ListAttribute" />.
  /// </value>
  [PMDateSource.List]
  [PXDefault("T")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Date Source")]
  public virtual string DateSource
  {
    get => this._DateSource;
    set => this._DateSource = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the underlying transactions should be groupped by inventory items.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Item")]
  public virtual bool? GroupByItem
  {
    get => this._GroupByItem;
    set => this._GroupByItem = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the underlying transactions should be groupped by employees.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Employee")]
  public virtual bool? GroupByEmployee
  {
    get => this._GroupByEmployee;
    set => this._GroupByEmployee = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the underlying transactions should be groupped by dates.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Date")]
  public virtual bool? GroupByDate
  {
    get => this._GroupByDate;
    set => this._GroupByDate = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the underlying transactions should be groupped by vendors or customers.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Vendor")]
  public virtual bool? GroupByVendor
  {
    get => this._GroupByVendor;
    set => this._GroupByVendor = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the underlying transactions should be groupped by all of the following: inventory items, employees, dates, and vendors or customers.
  /// </summary>
  [PXBool]
  public virtual bool? FullDetail
  {
    get
    {
      return new bool?(!this.GroupByItem.GetValueOrDefault() && !this.GroupByEmployee.GetValueOrDefault() && !this.GroupByDate.GetValueOrDefault() && !this.GroupByVendor.GetValueOrDefault());
    }
  }

  /// <summary>The step ID of the allocation rule.</summary>
  [PXInt]
  [PXUIField(DisplayName = "Allocation")]
  public virtual int? Allocation
  {
    get => this.StepID;
    set
    {
    }
  }

  /// <summary>The allocation text of the allocation rule.</summary>
  [PXString(10)]
  public virtual string AllocationText
  {
    get => this._AllocationText;
    set => this._AllocationText = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will create the allocation transaction even if it has an amount of zero.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Create Transaction with Zero Amount")]
  public virtual bool? AllocateZeroAmount
  {
    get => this._AllocateZeroAmount;
    set => this._AllocateZeroAmount = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will create the allocation transaction even if it has a quantity of zero.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Create Transaction with Zero Qty.")]
  public virtual bool? AllocateZeroQty
  {
    get => this._AllocateZeroQty;
    set => this._AllocateZeroQty = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will create the allocation transaction even if it is non-billable.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allocate Non-Billable Transactions")]
  public virtual bool? AllocateNonBillable
  {
    get => this._AllocateNonBillable;
    set => this._AllocateNonBillable = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transactions allocated by this allocation rule could be used in subsequent allocations.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Can Be Used as a Source in Another Allocation")]
  public virtual bool? MarkAsNotAllocated
  {
    get => this._MarkAsNotAllocated;
    set => this._MarkAsNotAllocated = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the notes can be copied.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? CopyNotes
  {
    get => this._CopyNotes;
    set => this._CopyNotes = value;
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

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AllocationID" />
  public abstract class allocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.allocationID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.StepID" />
  public abstract class stepID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.stepID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.Description" />
  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.SelectOption" />
  public abstract class selectOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.selectOption>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.Post" />
  public abstract class post : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.post>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.QtyFormula" />
  public abstract class qtyFormula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.qtyFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.BillableQtyFormula" />
  public abstract class billableQtyFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.billableQtyFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AmountFormula" />
  public abstract class amountFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.amountFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.DescriptionFormula" />
  public abstract class descriptionFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.descriptionFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.RangeStart" />
  public abstract class rangeStart : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.rangeStart>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.RangeEnd" />
  public abstract class rangeEnd : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.rangeEnd>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.RateTypeID" />
  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.rateTypeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountGroupFrom" />
  public abstract class accountGroupFrom : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.accountGroupFrom>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountGroupTo" />
  public abstract class accountGroupTo : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.accountGroupTo>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.Method" />
  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.method>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.UpdateGL" />
  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.updateGL>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.SourceBranchID" />
  public abstract class sourceBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.sourceBranchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetBranchOrigin" />
  public abstract class offsetBranchOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetBranchOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.TargetBranchID" />
  public abstract class targetBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.targetBranchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.ProjectOrigin" />
  public abstract class projectOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.projectOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.ProjectID" />
  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.projectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.TaskOrigin" />
  public abstract class taskOrigin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.taskOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.CostCodeOrigin" />
  public abstract class costCodeOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.costCodeOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.TaskID" />
  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.taskID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.TaskCD" />
  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.taskCD>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.costCodeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountGroupOrigin" />
  public abstract class accountGroupOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.accountGroupOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountGroupID" />
  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.accountGroupID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountOrigin" />
  public abstract class accountOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.accountOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AccountID" />
  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.accountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.SubMask" />
  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.subMask>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.SubID" />
  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.subID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetProjectOrigin" />
  public abstract class offsetProjectOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetProjectOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetProjectID" />
  public abstract class offsetProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.offsetProjectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetTaskOrigin" />
  public abstract class offsetTaskOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetTaskOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetCostCodeOrigin" />
  public abstract class offsetCostCodeOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetCostCodeOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetTaskID" />
  public abstract class offsetTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.offsetTaskID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetTaskCD" />
  public abstract class offsetTaskCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetTaskCD>
  {
  }

  public abstract class offsetCostCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.offsetCostCodeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetAccountGroupOrigin" />
  public abstract class offsetAccountGroupOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetAccountGroupOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetAccountGroupID" />
  public abstract class offsetAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.offsetAccountGroupID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetAccountOrigin" />
  public abstract class offsetAccountOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetAccountOrigin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetAccountID" />
  public abstract class offsetAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAllocationDetail.offsetAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetSubMask" />
  public abstract class offsetSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.offsetSubMask>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.OffsetSubID" />
  public abstract class offsetSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.offsetSubID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.Reverse" />
  public abstract class reverse : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.reverse>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.UseReversalDateFromOriginal" />
  public abstract class useReversalDateFromOriginal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.useReversalDateFromOriginal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.NoRateOption" />
  public abstract class noRateOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.noRateOption>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.DateSource" />
  public abstract class dateSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAllocationDetail.dateSource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.GroupByItem" />
  public abstract class groupByItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.groupByItem>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.GroupByEmployee" />
  public abstract class groupByEmployee : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.groupByEmployee>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.GroupByDate" />
  public abstract class groupByDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.groupByDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.GroupByVendor" />
  public abstract class groupByVendor : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.groupByVendor>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.FullDetail" />
  public abstract class fullDetail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.fullDetail>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.Allocation" />
  public abstract class allocation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAllocationDetail.allocation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AllocationText" />
  public abstract class allocationText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.allocationText>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AllocateZeroAmount" />
  public abstract class allocateZeroAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.allocateZeroAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AllocateZeroQty" />
  public abstract class allocateZeroQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.allocateZeroQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.AllocateNonBillable" />
  public abstract class allocateNonBillable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.allocateNonBillable>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.MarkAsNotAllocated" />
  public abstract class markAsNotAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAllocationDetail.markAsNotAllocated>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAllocationDetail.CopyNotes" />
  public abstract class copyNotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAllocationDetail.copyNotes>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAllocationDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMAllocationDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAllocationDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAllocationDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMAllocationDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAllocationDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAllocationDetail.lastModifiedDateTime>
  {
  }
}
