// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProject
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
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>A planned set of related tasks to be executed over a fixed period, within specific cost limit, and with other limitations. Each project consists of tasks that need
/// to be completed to complete the project. The project budget, profitability, and balances are monitored in the scope of account groups.</summary>
[PXCacheName("Project")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProject : 
  PX.Objects.CT.Contract,
  IAssign,
  IIncludable,
  IRestricted,
  INotable,
  IProjectAccountsSource,
  IPXSelectable
{
  protected int? _BillAddressID;
  protected 
  #nullable disable
  string _ExtRefNbr;
  protected bool? _Included;
  protected string _RestrictProjectSelect;

  /// <summary>The project ID.</summary>
  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  [PXUIField(DisplayName = "Project ID")]
  public override int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  /// <exclude />
  public int? ProjectID
  {
    get => this.ContractID;
    set
    {
    }
  }

  /// <summary>The type of the record.</summary>
  /// <value>The value can be either Contract or Project. The default value is Project.</value>
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault("P")]
  [PXUIField(DisplayName = "Base Type", Visible = false)]
  public override string BaseType
  {
    get => base.BaseType;
    set => base.BaseType = value;
  }

  /// <summary>The type of the project.</summary>
  [PXString]
  [PX.Objects.PM.ProjectType.List]
  [PXUIField]
  public virtual string ProjectType
  {
    get => base.BaseType;
    set => base.BaseType = value;
  }

  /// <summary>The project CD. This is a segmented key. Its format is configured on the Segmented Keys (CS202000) form.</summary>
  [PXDimensionSelector("PROJECT", typeof (Search2<PMProject.contractCD, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProject.customerID>>, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>>, Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.customerID), typeof (PMProject.customerID_Customer_acctName), typeof (PMProject.locationID), typeof (PMProject.status), typeof (PMProject.ownerID), typeof (PMProject.startDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (PMProject.description))]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  public override string ContractCD
  {
    get => this._ContractCD;
    set => this._ContractCD = value;
  }

  /// <summary>The project description.</summary>
  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public override string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt]
  public override int? OriginalContractID
  {
    get => this._OriginalContractID;
    set => this._OriginalContractID = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt]
  public override int? MasterContractID
  {
    get => this._MasterContractID;
    set => this._MasterContractID = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt]
  public override int? CaseItemID
  {
    get => this._CaseItemID;
    set => this._CaseItemID = value;
  }

  /// <summary>The detail level of the revenue budget.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c>: Task,
  /// <c>"I"</c>: Task and Item,
  /// <c>"C"</c>: Task and Cost Code,
  /// <c>"D"</c>: Task, Item, and Cost Code
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PMBudgetLevelList]
  [PXDefault("T")]
  [PXUIField(DisplayName = "Revenue Budget Level")]
  public virtual string BudgetLevel { get; set; }

  /// <summary>The detail level of the cost budget.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c>: Task,
  /// <c>"I"</c>: Task and Item,
  /// <c>"C"</c>: Task and Cost Code,
  /// <c>"D"</c>: Task, Item, and Cost Code
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PMBudgetLevelList]
  [PXDefault("T")]
  [PXUIField(DisplayName = "Cost Budget Level")]
  public virtual string CostBudgetLevel { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project budget is locked (using the <b>Lock Budget</b> action).</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? BudgetFinalized { get; set; }

  /// <summary>The identifier of the customer for the project. Projects can be of the internal or external type. Internal projects are those that have the value of this
  /// property equal to NULL and hense are not billable.</summary>
  [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>The customer location.</summary>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProject.customerID>>>), DisplayName = "Customer Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.defLocationID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProject.customerID>>>>))]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDefault]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Base Currency", Enabled = true, FieldClass = "MultipleBaseCurrencies")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  public virtual string BaseCuryID { get; set; }

  /// <summary>
  /// The identifier of the project <see cref="T:PX.Objects.CM.Extensions.CurrencyList">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.CuryID" /> field.
  /// </value>
  [PXDefault]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Budget Currency", IsReadOnly = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  public override string CuryID { get; set; }

  /// <summary>
  /// The identifier of the project <see cref="T:PX.Objects.CM.Extensions.CurrencyList">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.CuryID" /> field.
  /// </value>
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Project Currency", Required = true, FieldClass = "ProjectMultiCurrency")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  public virtual string CuryIDCopy
  {
    get => this.CuryID;
    set => this.CuryID = value;
  }

  /// <summary>
  /// The default <see cref="T:PX.Objects.CM.Extensions.CurrencyRateType">rate type</see> for the currency rate that is used for the budget.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Extensions.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.Extensions.CurrencyRateType.descr))]
  [PXUIField(DisplayName = "Currency Rate Type")]
  public override string RateTypeID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record associated with the project.
  /// </summary>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The default sales subaccount associated with the project. This subaccount can be used in allocation and billing rules.</summary>
  [PXDefault(typeof (Coalesce<Search<PX.Objects.CR.Location.cSalesSubID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProject.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMProject.locationID>>>>>, Search<GLSetup.defaultSubID>>))]
  [SubAccount(typeof (PMProject.defaultSalesAccountID), typeof (PMProject.defaultBranchID), false)]
  public override int? DefaultSalesSubID { get; set; }

  /// <summary>The default cost subaccount associated with the project. This subaccount can be used in allocation and cost transactions.</summary>
  [SubAccount(typeof (PMProject.defaultExpenseAccountID), typeof (PMProject.defaultBranchID), false)]
  public override int? DefaultExpenseSubID { get; set; }

  /// <summary>The default project accrual subaccount. The field is used depending on the <see cref="P:PX.Objects.PM.PMSetup.ExpenseAccrualSubMask" /> mask setting.</summary>
  [SubAccount]
  public override int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.PMBilling">billing rule</see> for the project. The billing rule is set at the <see cref="T:PX.Objects.PM.PMTask" /> level. This field contains the default value for the tasks
  /// created under the given project.</summary>
  [PXSelector(typeof (Search<PMBilling.billingID, Where<PMBilling.isActive, Equal<True>>>), DescriptionField = typeof (PMBilling.description))]
  [PXForeignReference(typeof (Field<PMProject.billingID>.IsRelatedTo<PMBilling.billingID>))]
  [PXUIField(DisplayName = "Billing Rule")]
  [PXDBString(15, IsUnicode = true)]
  public override string BillingID
  {
    get => this._BillingID;
    set => this._BillingID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAddress">billing address</see> that is associated with the customer.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMAddress.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PMBillingAddress(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>>, LeftJoin<PMBillingAddress, On<PMBillingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<PMBillingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PMBillingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PMBillingAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProject.customerID>>>>), typeof (PMProject.customerID))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.AR.ARContact">billing contact</see> that is associated with the customer.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARContact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (PMContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Billing Contact", Visible = false)]
  [PMBillingContact(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, LeftJoin<PMBillingContact, On<PMBillingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PMBillingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PMBillingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PMBillingContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProject.customerID>>>>), typeof (PMProject.customerID))]
  public virtual int? BillContactID { get; set; }

  /// <summary>
  /// The identifier of the billing <see cref="T:PX.Objects.CM.Extensions.CurrencyList">currency</see> of the project,
  /// which is used as the currency of the invoices created during the project billing.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CM.Extensions.CurrencyList.CuryID" /> field.
  /// </value>
  [PXDefault]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Billing Currency")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  public virtual string BillingCuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAddress">project site address</see> record associated with the project.
  /// </summary>
  [PXDBInt]
  [PMSiteAddress(typeof (Select<PMAddress>))]
  public int? SiteAddressID { get; set; }

  /// <summary>
  /// The way how the system manages inventory for the project.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"P"</c>: Track by Project Quantity and Cost,
  /// <c>"V"</c>: Track by Project Quantity,
  /// <c>"L"</c>: Track by Location
  /// </value>
  [PXDBString(1)]
  [PXUIField(DisplayName = "Inventory Tracking")]
  [PXDefault("P")]
  [ProjectAccountingModes.List]
  public override string AccountingMode { get; set; }

  /// <summary>Gets or sets the <see cref="T:PX.Objects.PM.PMAllocation">allocation rule</see> for the project. The allocation rule is set at the <see cref="T:PX.Objects.PM.PMTask" /> level. This field contains the default
  /// value for the tasks created under the given project.</summary>
  [PXForeignReference(typeof (Field<PMProject.allocationID>.IsRelatedTo<PMAllocation.allocationID>))]
  [PXSelector(typeof (Search<PMAllocation.allocationID, Where<PMAllocation.isActive, Equal<True>>>), DescriptionField = typeof (PMAllocation.description))]
  [PXUIField(DisplayName = "Allocation Rule")]
  [PXDBString(15, IsUnicode = true)]
  public override string AllocationID
  {
    get => this._AllocationID;
    set => this._AllocationID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.CS.Terms">credit terms</see> object associated with the document.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AR.Customer.TermsID">credit terms</see> that are selected for the <see cref="P:PX.Objects.PM.PMProject.CustomerID">customer</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CS.Terms.TermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProject.customerID>>>>))]
  [PXUIField(DisplayName = "Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public override string TermsID { get; set; }

  /// <summary>The user who is responsible for managing the project.</summary>
  [PXDefault]
  [Owner]
  public override int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>The project manager for the project. The project manager can approve and reject activities that require approval. An activity requires an approval only if
  /// the <see cref="P:PX.Objects.PM.PMTask.ApproverID" /> is specified for a given <see cref="T:PX.Objects.PM.PMTask" />.</summary>
  [PXDBInt]
  [PXEPEmployeeSelector(Filterable = true)]
  [PXForeignReference(typeof (Field<PMProject.approverID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  [PXUIField]
  public override int? ApproverID
  {
    get => this._ApproverID;
    set => this._ApproverID = value;
  }

  /// <summary>The project manager's assistant.</summary>
  [PXDBInt]
  [PXRestrictor(typeof (Where<PX.Objects.EP.EPEmployee.vStatus, Equal<CustomerStatus.active>>), "The contact is not active.", new System.Type[] {}, SuppressVerify = true)]
  [PXEPEmployeeSelector(Filterable = true)]
  [PXForeignReference(typeof (Field<PMProject.assistantID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  [PXUIField]
  public int? AssistantID { get; set; }

  /// <summary>The <see cref="T:PX.Objects.PM.PMRateTable">rate table</see> for the project.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Rate Table Code")]
  [PXSelector(typeof (PMRateTable.rateTableID), DescriptionField = typeof (PMRateTable.description))]
  [PXForeignReference(typeof (Field<PMProject.rateTableID>.IsRelatedTo<PMRateTable.rateTableID>))]
  public override string RateTableID
  {
    get => this._RateTableID;
    set => this._RateTableID = value;
  }

  /// <summary>The template for the project.</summary>
  [PXUIField]
  [PXDimensionSelector("TMPROJECT", typeof (Search2<PMProject.contractID, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>, Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.isActive, Equal<True>>>>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.budgetLevel), typeof (PMProject.billingID), typeof (ContractBillingSchedule.type), typeof (PMProject.ownerID)}, DescriptionField = typeof (PMProject.description))]
  [PXDBInt]
  [PXForeignReference(typeof (Field<PMProject.templateID>.IsRelatedTo<PMProject.contractID>))]
  public override int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.PM.ProjectStatus">status</see> of the project.</summary>
  [PXDBString(1, IsFixed = true)]
  [ProjectStatus.ProjectStatusList]
  [PXDefault("D")]
  [PXUIField]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt]
  public override int? Duration
  {
    get => this._Duration;
    set => this._Duration = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBString(1, IsFixed = true)]
  public override string DurationType
  {
    get => this._DurationType;
    set => this._DurationType = value;
  }

  /// <summary>The start date of the project.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Start Date")]
  public override DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The end date of a project.</summary>
  [PXDBDate]
  [PXUIField]
  public override DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  /// <summary>The termination date of the project.</summary>
  [PXDBDate]
  [PXUIField]
  public override DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  public override int? GracePeriod
  {
    get => this._GracePeriod;
    set => this._GracePeriod = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public override bool? AutoRenew
  {
    get => this._AutoRenew;
    set => this._AutoRenew = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(0)]
  public override int? AutoRenewDays
  {
    get => this._AutoRenewDays;
    set => this._AutoRenewDays = value;
  }

  /// <summary>The external reference number.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBInt]
  [PXDefault(0)]
  public override int? DetailedBilling
  {
    get => this._DetailedBilling;
    set => this._DetailedBilling = value;
  }

  /// <summary>This field is not used with projects.</summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public override bool? AllowOverride
  {
    get => this._AllowOverride;
    set => this._AllowOverride = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public override bool? RefreshOnRenewal
  {
    get => this._RefreshOnRenewal;
    set => this._RefreshOnRenewal = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public override bool? IsContinuous
  {
    get => this._IsContinuous;
    set => this._IsContinuous = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the project is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the project has been approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public override bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the project has been rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public override bool? Rejected { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is active. Transactions can be added only to the active projects.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public override bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is completed.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public override bool? IsCompleted
  {
    get => this._IsCompleted;
    set => this._IsCompleted = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the allocation should be run every time a <see cref="T:PX.Objects.PM.PMTran" /> is released.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Run Allocation on Release of Project Transactions")]
  public override bool? AutoAllocate
  {
    get => this._AutoAllocate;
    set => this._AutoAllocate = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the GL module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInGL>))]
  [PXUIField(DisplayName = "GL")]
  public override bool? VisibleInGL
  {
    get => this._VisibleInGL;
    set => this._VisibleInGL = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the AP module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAP>))]
  [PXUIField(DisplayName = "AP")]
  public override bool? VisibleInAP
  {
    get => this._VisibleInAP;
    set => this._VisibleInAP = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the AR module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInAR>))]
  [PXUIField(DisplayName = "AR")]
  public override bool? VisibleInAR
  {
    get => this._VisibleInAR;
    set => this._VisibleInAR = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the SO module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInSO>))]
  [PXUIField(DisplayName = "SO")]
  public override bool? VisibleInSO
  {
    get => this._VisibleInSO;
    set => this._VisibleInSO = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the PO module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInPO>))]
  [PXUIField(DisplayName = "PO")]
  public override bool? VisibleInPO
  {
    get => this._VisibleInPO;
    set => this._VisibleInPO = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the EP Time module. If the project is invisible, it will not be displayed in
  /// the field selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInTA>))]
  [PXUIField(DisplayName = "Time Entries")]
  public override bool? VisibleInTA
  {
    get => this._VisibleInTA;
    set => this._VisibleInTA = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the <span>EP Expense</span> module. If the project is invisible, it will not be
  /// displayed in the field selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInEA>))]
  [PXUIField(DisplayName = "Expenses")]
  public override bool? VisibleInEA
  {
    get => this._VisibleInEA;
    set => this._VisibleInEA = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the IN module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInIN>))]
  [PXUIField(DisplayName = "IN")]
  public override bool? VisibleInIN
  {
    get => this._VisibleInIN;
    set => this._VisibleInIN = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the CA module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCA>))]
  [PXUIField(DisplayName = "CA")]
  public override bool? VisibleInCA
  {
    get => this._VisibleInCA;
    set => this._VisibleInCA = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is visible in the CR module. If the project is invisible, it will not be displayed in the field
  /// selectors in this module.</summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PMSetup.visibleInCR>))]
  [PXUIField(DisplayName = "CRM")]
  public override bool? VisibleInCR
  {
    get => this._VisibleInCR;
    set => this._VisibleInCR = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the project is a non-project. Only one project in the system is a non-project. A non-project is used whenever you
  /// have a transaction that is not applicable to any other project.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public override bool? NonProject
  {
    get => this._NonProject;
    set => this._NonProject = value;
  }

  [PXSearchable(2048 /*0x0800*/, "Project: {0} - {2}", new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.customerID), typeof (PX.Objects.CR.BAccount.acctName)}, new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.contractCD), typeof (PMProject.description)}, NumberFields = new System.Type[] {typeof (PMProject.contractCD)}, Line1Format = "{0}{1:d}{2}", Line1Fields = new System.Type[] {typeof (PMProject.templateID), typeof (PMProject.startDate), typeof (PMProject.status)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (PMProject.description)}, WhereConstraint = typeof (Where<Current<PMProject.baseType>, Equal<CTPRType.project>, And<Current<PMProject.nonProject>, NotEqual<True>>>))]
  [PXNote(DescriptionField = typeof (PMProject.contractCD))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>This field in not used with projects.</summary>
  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public override bool? ServiceActivate
  {
    get => this._ServiceActivate;
    set => this._ServiceActivate = value;
  }

  /// <summary>The entity attributes.</summary>
  [CRAttributesField(typeof (PMProject.classID), typeof (PX.Objects.CT.Contract.noteID))]
  public override string[] Attributes { get; set; }

  /// <summary>The class ID for the attributes.</summary>
  /// <value>Always returns the current <see cref="F:PX.Objects.PM.GroupTypes.Project" />.</value>
  [PXString(20)]
  public override string ClassID => "PROJECT";

  /// <summary>An unbound field used in the user interface to include the project into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.</summary>
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  /// <summary>Include Change Orders in Contract Total</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include CO", FieldClass = "ChangeOrder")]
  public override bool? IncludeCO { get; set; }

  /// <summary>
  /// An option which defines whether a project can be selected in the document if the customer
  /// specified in the project differs from the customer specified in the document.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"A"</c>: All Projects,
  /// <c>"C"</c>: Customer Projects
  /// </value>
  [PMRestrictOption.List]
  [PXString(1)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "Restrict Project Selection")]
  [PXDBScalar(typeof (Search<PMSetup.restrictProjectSelect>))]
  public virtual string RestrictProjectSelect
  {
    get => this._RestrictProjectSelect;
    set => this._RestrictProjectSelect = value;
  }

  /// <summary>Stepped Retainage</summary>
  /// Retainage With Steps
  [PXStringList(new string[] {"0", "1"}, new string[] {"Fixed Retainage", "Retainage with Steps"})]
  [PXString]
  [PXUIField(FieldClass = "Retainage", DisplayName = "Retainage Type")]
  public virtual string SteppedRetainageOption
  {
    get => !this.SteppedRetainage.GetValueOrDefault() ? "0" : "1";
    set => this.SteppedRetainage = new bool?(value == "1");
  }

  /// <summary>
  /// The percent of an invoice amount issued for the project that is retained by the customer.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage (%)", FieldClass = "Retainage")]
  public override Decimal? RetainagePct { get; set; }

  /// <summary>The retainage cap amount.</summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMProject.capAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cap Amount", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryCapAmount { get; set; }

  /// <summary>The retainage cap amount (in the base currency).</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CapAmount { get; set; }

  /// <summary>
  /// The source of the expense account to be used in the project drop-ship order.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"O"</c>: Posting Class or Item,
  /// <c>"P"</c>: Project,
  /// <c>"T"</c>: Task
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [DropshipExpenseAccountSourceOption.List]
  [PXDefault(typeof (PMSetup.dropshipExpenseAccountSource))]
  [PXUIField(DisplayName = "Use Expense Account From", Required = true)]
  public override string DropshipExpenseAccountSource { get; set; }

  /// <summary>The subaccount mask for items used in the project drop-ships orders.</summary>
  [PXDefault(typeof (PMSetup.dropshipExpenseSubMask))]
  [DropshipExpenseSubAccountMask(DisplayName = "Combine Expense Sub. From")]
  public override string DropshipExpenseSubMask { get; set; }

  /// <summary>
  /// Defines whether a receipt will be generated for drop-shipped items that are purchased for the project.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"R"</c>: Generate Receipt,
  /// <c>"S"</c>: Skip Receipt Generation
  /// </value>
  [DropshipReceiptProcessingOption.List]
  [PXDBString(1)]
  [PXDefault(typeof (PMSetup.dropshipReceiptProcessing))]
  [PXUIField(DisplayName = "Drop-Ship Receipt Processing")]
  public override string DropshipReceiptProcessing { get; set; }

  /// <summary>
  /// Defines when the expense transaction should be recorded.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"B"</c>: On Bill Release,
  /// <c>"R"</c>: On Receipt Release
  /// </value>
  [DropshipExpenseRecordingOption.List]
  [PXDBString(1)]
  [PXDefault(typeof (PMSetup.dropshipExpenseRecording))]
  [PXUIEnabled(typeof (Where<PMProject.dropshipReceiptProcessing, Equal<DropshipReceiptProcessingOption.generateReceipt>>))]
  [PXUIField(DisplayName = "Record Drop-Ship Expenses")]
  public override string DropshipExpenseRecording { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxZone">cost tax zone</see> associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Cost Tax Zone", Required = false)]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public override string CostTaxZoneID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxZone">revenue tax zone</see> associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Revenue Tax Zone", Required = false)]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public override string RevenueTaxZoneID { get; set; }

  [PXDecimal]
  public Decimal? CuryRate { get; set; }

  /// <summary>
  /// The project group (<see cref="T:PX.Objects.PM.PMProjectGroup">PMProjectGroup</see>),
  /// which the project belongs to.
  /// The project might not belong to any project group (the value is <c>null</c>).
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Project Group")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<MatchUser>, PMProjectGroup>.SearchFor<PMProjectGroup.projectGroupID>), DescriptionField = typeof (PMProjectGroup.description))]
  [PXRestrictor(typeof (Where<PMProjectGroup.isActive, Equal<True>>), "The {0} project group is inactive.", new System.Type[] {typeof (PMProjectGroup.projectGroupID)})]
  [PXForeignReference(typeof (Field<PMProject.projectGroupID>.IsRelatedTo<PMProjectGroup.projectGroupID>))]
  public virtual string ProjectGroupID { get; set; }

  /// <summary>
  /// Specifies the status code if the project is not valid after system upgrade.
  /// </summary>
  [PXDBInt]
  public virtual int? StatusCode { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether materials from stock not allocated to any project can be issued.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Issue from Free Stock", FieldClass = "MaterialManagement")]
  public virtual bool? AllowIssueFromFreeStock { get; set; }

  /// <summary>
  /// The copy of 'ChangeOrderWorkflow' which is used for visibility control in the classic UI.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Change Order Workflow", FieldClass = "CHANGEORDER", Visible = false)]
  public virtual bool? ChangeOrderWorkflowCopy
  {
    get => this.ChangeOrderWorkflow;
    set => this.ChangeOrderWorkflow = value;
  }

  /// <summary>Primary Key.</summary>
  /// <exclude />
  public new class PK : PrimaryKeyOf<PMProject>.By<PMProject.contractID>.Dirty
  {
    public static PMProject Find(PXGraph graph, int? projectID)
    {
      PXGraph pxGraph = graph;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) projectID;
      int? nullable = projectID;
      int num1 = 0;
      int num2 = nullable.GetValueOrDefault() < num1 & nullable.HasValue ? 2 : 0;
      return (PMProject) PrimaryKeyOf<PMProject>.By<PMProject.contractID>.Dirty.FindBy(pxGraph, (object) local, (PKFindOptions) num2);
    }

    public static PMProject Find(PXGraph graph, int? projectID, PKFindOptions options)
    {
      return (PMProject) PrimaryKeyOf<PMProject>.By<PMProject.contractID>.Dirty.FindBy(graph, (object) projectID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  /// <exclude />
  public new static class FK
  {
    /// <summary>Customer</summary>
    /// <exclude />
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<PMProject>.By<PMProject.customerID>
    {
    }

    /// <summary>
    /// Foreign key to <see cref="T:PX.Objects.PM.PMProjectGroup">PMProjectGroup</see>.
    /// </summary>
    public class ProjectGroup : 
      PrimaryKeyOf<PMProjectGroup>.By<PMProjectGroup.projectGroupID>.ForeignKeyOf<PMProject>.By<PMProject.projectGroupID>
    {
    }
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.contractID>
  {
  }

  public new abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.baseType>
  {
  }

  public abstract class projectType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.projectType>
  {
  }

  public new abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.contractCD>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.description>
  {
  }

  public new abstract class originalContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.originalContractID>
  {
  }

  public new abstract class masterContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.masterContractID>
  {
  }

  public new abstract class caseItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.caseItemID>
  {
  }

  public abstract class budgetLevel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.budgetLevel>
  {
  }

  public abstract class costBudgetLevel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.costBudgetLevel>
  {
  }

  public abstract class budgetFinalized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.budgetFinalized>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.customerID>
  {
  }

  public abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.customerID_Customer_acctName>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.locationID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.baseCuryID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.curyID>
  {
  }

  public abstract class curyIDCopy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.curyIDCopy>
  {
  }

  public new abstract class rateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.rateTypeID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMProject.curyInfoID>
  {
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record associated with the project.
  /// </summary>
  public new abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultSalesAccountID>
  {
  }

  public new abstract class defaultSalesSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultSalesSubID>
  {
  }

  public new abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultExpenseAccountID>
  {
  }

  public new abstract class defaultExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultExpenseSubID>
  {
  }

  public new abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultAccrualAccountID>
  {
  }

  public new abstract class defaultAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultAccrualSubID>
  {
  }

  public new abstract class defaultOverbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultOverbillingAccountID>
  {
  }

  public new abstract class defaultOverbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultOverbillingSubID>
  {
  }

  public new abstract class defaultUnderbillingAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultUnderbillingAccountID>
  {
  }

  public new abstract class defaultUnderbillingSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultUnderbillingSubID>
  {
  }

  public new abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.defaultBranchID>
  {
  }

  public new abstract class billingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.billingID>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.billContactID>
  {
  }

  public abstract class billingCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.billingCuryID>
  {
  }

  public abstract class siteAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.siteAddressID>
  {
  }

  public new abstract class accountingMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.accountingMode>
  {
  }

  public new abstract class allocationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.allocationID>
  {
  }

  public new abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.termsID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.workgroupID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.ownerID>
  {
  }

  public new abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.approverID>
  {
  }

  public abstract class assistantID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.assistantID>
  {
  }

  public new abstract class rateTableID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.rateTableID>
  {
  }

  public new abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.templateID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.status>
  {
  }

  public new abstract class duration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.duration>
  {
  }

  public new abstract class durationType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.durationType>
  {
  }

  public new abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProject.startDate>
  {
  }

  public new abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProject.expireDate>
  {
  }

  public new abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProject.terminationDate>
  {
  }

  public new abstract class gracePeriod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.gracePeriod>
  {
  }

  public new abstract class autoRenew : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.autoRenew>
  {
  }

  public new abstract class autoRenewDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.autoRenewDays>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.extRefNbr>
  {
  }

  public new abstract class lastProformaNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.lastProformaNumber>
  {
  }

  public new abstract class certifiedJob : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.certifiedJob>
  {
  }

  public new abstract class restrictToEmployeeList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.restrictToEmployeeList>
  {
  }

  public new abstract class restrictToResourceList : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.restrictToResourceList>
  {
  }

  public new abstract class detailedBilling : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.detailedBilling>
  {
  }

  public new abstract class allowOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.allowOverride>
  {
  }

  public new abstract class refreshOnRenewal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.refreshOnRenewal>
  {
  }

  public new abstract class isContinuous : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.isContinuous>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.hold>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.approved>
  {
  }

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.rejected>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.isActive>
  {
  }

  public new abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.isCompleted>
  {
  }

  public new abstract class autoAllocate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.autoAllocate>
  {
  }

  public new abstract class visibleInGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInGL>
  {
  }

  public new abstract class visibleInAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInAP>
  {
  }

  public new abstract class visibleInAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInAR>
  {
  }

  public new abstract class visibleInSO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInSO>
  {
  }

  public new abstract class visibleInPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInPO>
  {
  }

  public new abstract class visibleInTA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInTA>
  {
  }

  public new abstract class visibleInEA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInEA>
  {
  }

  public new abstract class visibleInIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInIN>
  {
  }

  public new abstract class visibleInCA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInCA>
  {
  }

  public new abstract class visibleInCR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.visibleInCR>
  {
  }

  public new abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.nonProject>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProject.noteID>
  {
  }

  public new abstract class serviceActivate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.serviceActivate>
  {
  }

  public new abstract class attributes : 
    BqlType<IBqlAttributes, string[]>.Field<PMProject.attributes>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.classID>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProject.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.included>
  {
  }

  /// <exclude />
  public new abstract class includeCO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProject.includeCO>
  {
  }

  public abstract class restrictProjectSelect : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.restrictProjectSelect>
  {
  }

  /// <exclude />
  public abstract class steppedRetainageOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.steppedRetainageOption>
  {
  }

  public new abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProject.retainagePct>
  {
  }

  /// <exclude />
  public abstract class curyCapAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProject.curyCapAmount>
  {
  }

  /// <exclude />
  public abstract class capAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProject.capAmount>
  {
  }

  public new abstract class dropshipExpenseAccountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.dropshipExpenseAccountSource>
  {
  }

  public new abstract class dropshipExpenseSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.dropshipExpenseSubMask>
  {
  }

  public new abstract class dropshipReceiptProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.dropshipReceiptProcessing>
  {
  }

  public new abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.dropshipExpenseRecording>
  {
  }

  public new abstract class costTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.costTaxZoneID>
  {
  }

  public new abstract class revenueTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProject.revenueTaxZoneID>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.curyRate>
  {
  }

  public abstract class projectGroupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.projectGroupID>
  {
  }

  public abstract class statusCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.statusCode>
  {
  }

  public abstract class allowIssueFromFreeStock : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.allowIssueFromFreeStock>
  {
  }

  public abstract class changeOrderWorkflowCopy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProject.changeOrderWorkflowCopy>
  {
  }
}
