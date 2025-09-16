// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingRule
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
/// A step of the <see cref="T:PX.Objects.PM.PMBilling">billing rule</see> that defines the calculation rules
/// and other settings that depend on the step <see cref="P:PX.Objects.PM.PMBillingRule.Type">type</see>.
/// The records of this type are created and edited through the Billing Rules (PM207000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.BillingMaint" /> graph).
/// </summary>
[PXCacheName("Billing Rule Step")]
[PXPrimaryGraph(typeof (BillingMaint))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMBillingRule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BillingID;
  protected int? _StepID;
  protected string _Description;
  protected string _Type;
  protected int? _AccountGroupID;
  protected string _InvoiceGroup;
  protected string _BranchSource;
  protected string _BranchSourceBudget;
  protected string _AccountSource;
  protected int? _AccountID;
  protected string _SubMask;
  protected int? _SubID;
  protected bool? _IncludeNonBillable;
  protected bool? _CopyNotes;
  protected string _RateTypeID;
  protected string _NoRateOption;
  protected bool? _GroupByItem;
  protected bool? _GroupByEmployee;
  protected bool? _GroupByDate;
  protected bool? _GroupByVendor;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the billing rule to which this billing rule step belongs.
  /// </summary>
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault(typeof (PMBilling.billingID))]
  [PXParent(typeof (Select<PMBilling, Where<PMBilling.billingID, Equal<Current<PMBillingRule.billingID>>>>))]
  public virtual string BillingID
  {
    get => this._BillingID;
    set => this._BillingID = value;
  }

  /// <summary>The identifier of the billing rule step.</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Step ID")]
  public virtual int? StepID
  {
    get => this._StepID;
    set => this._StepID = value;
  }

  /// <summary>The description of the billing rule.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The type of the billing rule step.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMBillingType.ListAttribute" />.
  /// </value>
  [PMBillingType.List]
  [PXDefault("T")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMAccountGroup">account group</see> whose transactions are involved in this billing step.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMAccountGroup.groupID" /> field.
  /// </value>
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>The identifier for grouping the invoices.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Invoice Group")]
  public virtual string InvoiceGroup
  {
    get => this._InvoiceGroup;
    set => this._InvoiceGroup = value;
  }

  /// <summary>The source branch to be used for billing.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMBillingType.ListAttribute" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PMAccountSource.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Use Destination Branch From", Required = true)]
  public virtual string BranchSource
  {
    get => this._BranchSource;
    set => this._BranchSource = value;
  }

  /// <summary>
  /// The account to be used for billing as a sales account of the billing rule.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.PM.PMAccountSource.ListAttributeBudget" />.
  /// </value>
  [PXString(1, IsFixed = true)]
  [PMAccountSource.ListAttributeBudget]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Use Destination Branch From", Visible = false, Required = true)]
  public virtual string BranchSourceBudget
  {
    get => this._BranchSource;
    set => this._BranchSource = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">target branch</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? TargetBranchID { get; set; }

  /// <summary>
  /// The account to be used for billing of the billing rule.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Sales Account From", Required = true)]
  public virtual string AccountSource
  {
    get => this._AccountSource;
    set => this._AccountSource = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Account">account</see> to be used for billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [Account]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>The sub mask of the billing rule.</summary>
  [PMBillSubAccountMask]
  public virtual string SubMask
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <summary>The sub mask budget of the billing rule.</summary>
  [PMBillBudgetSubAccountMask]
  public virtual string SubMaskBudget
  {
    get => this._SubMask;
    set => this._SubMask = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">subaccount</see> to be used for billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (PMBillingRule.accountID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will include non-billable transactions in the created invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Non-Billable Transactions")]
  public virtual bool? IncludeNonBillable
  {
    get => this._IncludeNonBillable;
    set => this._IncludeNonBillable = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />)that the notes and files are copied to the created invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? CopyNotes
  {
    get => this._CopyNotes;
    set => this._CopyNotes = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMRateType">rate type</see> used in the billing rule step.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMRateType.rateTypeID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (PMRateType.rateTypeID), DescriptionField = typeof (PMRateType.description))]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  /// <summary>
  /// The action to be performed if the rate value has not been defined.
  /// </summary>
  [PMNoRateOption.BillingList]
  [PXDefault("E")]
  [PXDBString(1)]
  [PXUIField(DisplayName = "If @Rate Is Not Defined")]
  public virtual string NoRateOption
  {
    get => this._NoRateOption;
    set => this._NoRateOption = value;
  }

  /// <summary>
  /// The formula to be used to generate the description for the pro forma invoice that is created during billing.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Invoice Description Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string InvoiceFormula { get; set; }

  /// <summary>
  /// The formula for calculating the quantity of a line of the generated invoice.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Line Quantity Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string QtyFormula { get; set; }

  /// <summary>
  /// The formula for calculating the amount of a line of the generated invoice.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Line Amount Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string AmountFormula { get; set; }

  /// <summary>
  /// The formula to be used to generate the description for a line of the generated invoice.
  /// </summary>
  [PXFormulaEditor(DisplayName = "Line Description Formula", IsDBField = true, Enabled = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PMOptionsProviderForFormulaEditor]
  public virtual string DescriptionFormula { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transactions with the same inventory item are grouped in a single line of the generated invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual bool? GroupByItem
  {
    get => this._GroupByItem;
    set => this._GroupByItem = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transactions with the same employee are grouped in a single line of the generated invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Employee")]
  public virtual bool? GroupByEmployee
  {
    get => this._GroupByEmployee;
    set => this._GroupByEmployee = value;
  }

  /// <summary>/// A Boolean value that indicates (if set to <see langword="true" />) that the transactions with the same date are groupped in a single line of the generated invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Date")]
  public virtual bool? GroupByDate
  {
    get => this._GroupByDate;
    set => this._GroupByDate = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the transactions with the same date are grouped in a single line of the generated invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Vendor")]
  public virtual bool? GroupByVendor
  {
    get => this._GroupByVendor;
    set => this._GroupByVendor = value;
  }

  public virtual bool FullDetail
  {
    get
    {
      return !this.GroupByItem.GetValueOrDefault() && !this.GroupByEmployee.GetValueOrDefault() && !this.GroupByDate.GetValueOrDefault() && !this.GroupByVendor.GetValueOrDefault();
    }
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will add a transaction in the created invoice even if the transaction has a zero quantity or a zero amount.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Create Lines with Zero Amount and Quantity")]
  public virtual bool? IncludeZeroAmountAndQty { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the system will add a transaction in the created invoice even if the transaction has an amount of zero.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Create Lines with Zero Amount and Quantity", Visible = false)]
  public virtual bool? IncludeZeroAmount
  {
    get => this.IncludeZeroAmountAndQty;
    set => this.IncludeZeroAmountAndQty = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the rule is available for use in projects and project tasks.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

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
  public class PK : PrimaryKeyOf<PMBillingRule>.By<PMBillingRule.billingID>
  {
    public static PMBillingRule Find(PXGraph graph, string billingID, PKFindOptions options = 0)
    {
      return (PMBillingRule) PrimaryKeyOf<PMBillingRule>.By<PMBillingRule.billingID>.FindBy(graph, (object) billingID, options);
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.BillingID" />
  public abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.billingID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.StepID" />
  public abstract class stepID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRule.stepID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.StepID" />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.Type" />
  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.type>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.AccountGroupID" />
  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRule.accountGroupID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.InvoiceGroup" />
  public abstract class invoiceGroup : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.invoiceGroup>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.BranchSource" />
  public abstract class branchSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.branchSource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.BranchSource" />
  public abstract class branchSourceBudget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.branchSourceBudget>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.TargetBranchID" />
  public abstract class targetBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRule.targetBranchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.TargetBranchID" />
  public abstract class accountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.accountSource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.AccountID" />
  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRule.accountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.SubMask" />
  public abstract class subMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.subMask>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.SubMaskBudget" />
  public abstract class subMaskBudget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.subMaskBudget>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.SubID" />
  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingRule.subID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.IncludeNonBillable" />
  public abstract class includeNonBillable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingRule.includeNonBillable>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.CopyNotes" />
  public abstract class copyNotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingRule.copyNotes>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.CopyNotes" />
  public abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.rateTypeID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.NoRateOption" />
  public abstract class noRateOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.noRateOption>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.InvoiceFormula" />
  public abstract class invoiceFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.invoiceFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.QtyFormula" />
  public abstract class qtyFormula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingRule.qtyFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.AmountFormula" />
  public abstract class amountFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.amountFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.DescriptionFormula" />
  public abstract class descriptionFormula : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.descriptionFormula>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.GroupByItem" />
  public abstract class groupByItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingRule.groupByItem>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.GroupByEmployee" />
  public abstract class groupByEmployee : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingRule.groupByEmployee>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.GroupByDate" />
  public abstract class groupByDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingRule.groupByDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.GroupByVendor" />
  public abstract class groupByVendor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingRule.groupByVendor>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.IncludeZeroAmountAndQty" />
  public abstract class includeZeroAmountAndQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingRule.includeZeroAmountAndQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.IncludeZeroAmount" />
  public abstract class includeZeroAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingRule.includeZeroAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMBillingRule.IsActive" />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingRule.isActive>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBillingRule.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMBillingRule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBillingRule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBillingRule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMBillingRule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingRule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMBillingRule.lastModifiedDateTime>
  {
  }
}
