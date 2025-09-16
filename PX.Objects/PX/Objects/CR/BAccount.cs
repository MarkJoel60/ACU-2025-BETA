// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccount
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
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.CS.DAC;
using PX.Objects.EP;
using PX.SM;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents a business account used as a prospect, customer, or vendor.
/// Also, this is the base class for derived DACs: <see cref="T:PX.Objects.AR.Customer" />, <see cref="T:PX.Objects.AP.Vendor" />, <see cref="T:PX.Objects.EP.EPEmployee" />.
/// The records of this type are created and edited on the Business Accounts (CR303000) form
/// (which corresponds to the <see cref="T:PX.Objects.CR.BusinessAccountMaint" /> graph).
/// <see cref="T:PX.Objects.AR.Customer">Customers</see> are created and edited on the Customers (AR303000) form
/// (which corresponds to the <see cref="T:PX.Objects.AR.CustomerMaint" /> graph).
/// <see cref="T:PX.Objects.AP.Vendor">Vendors</see> are created and edited on the Vendors (AP303000) form
/// (which corresponds to the <see cref="T:PX.Objects.AP.VendorMaint" /> graph).
/// <see cref="T:PX.Objects.EP.EPEmployee">Employees</see> are created and edited on the Employees (EP203000) form
/// (which corresponds to the <see cref="T:PX.Objects.EP.EmployeeMaint" /> graph).
/// <see cref="T:PX.Objects.CS.DAC.OrganizationBAccount">Companies</see> are created and edited on the Companies (CS101500) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.DAC.OrganizationBAccount" /> graph).
/// </summary>
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (OrganizationMaint), typeof (BranchMaint), typeof (BusinessAccountMaint), typeof (EmployeeMaint), typeof (VendorMaint), typeof (VendorMaint), typeof (CustomerMaint), typeof (CustomerMaint), typeof (VendorMaint), typeof (VendorMaint), typeof (CustomerMaint), typeof (CustomerMaint), typeof (VendorMaint), typeof (CustomerMaint), typeof (BusinessAccountMaint)}, new System.Type[] {typeof (Select<OrganizationBAccount, Where<OrganizationBAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Current<BAccount.isBranch>, Equal<True>>>>), typeof (Select<BranchMaint.BranchBAccount, Where<BranchMaint.BranchBAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Current<BAccount.isBranch>, Equal<True>>>>), typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Where2<Where<Current<BAccount.viewInCrm>, Equal<True>, Or<Current<BAccountR.viewInCrm>, Equal<True>>>, And<BAccount.type, NotEqual<BAccountType.employeeType>>>>>>), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<BAccount.bAccountID>>>>), typeof (SelectFromBase<VendorR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<VendorR.bAccountID, Equal<BqlField<BAccount.bAccountID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Current<BAccountR.viewInAp>, IBqlBool>.IsEqual<True>>>), typeof (SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, Equal<BqlField<BAccountR.bAccountID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Current<BAccountR.viewInAp>, IBqlBool>.IsEqual<True>>>), typeof (SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, Equal<BqlField<BAccount.bAccountID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Current<BAccountR.viewInAr>, IBqlBool>.IsEqual<True>>>), typeof (SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, Equal<BqlField<BAccountR.bAccountID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Current<BAccountR.viewInAr>, IBqlBool>.IsEqual<True>>>), typeof (Select<VendorR, Where<VendorR.bAccountID, Equal<Current<BAccount.bAccountID>>>>), typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<BAccountR.bAccountID>>>>), typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<BAccount.bAccountID>>>>), typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<BAccountR.bAccountID>>>>), typeof (Where<BAccountR.bAccountID, Less<Zero>, And<BAccountR.type, Equal<BAccountType.vendorType>>>), typeof (Where<BAccountR.bAccountID, Less<Zero>, And<BAccountR.type, Equal<BAccountType.customerType>>>), typeof (Select<BAccount, Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.vendorType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>, And<Where<BAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, Or<Current<BAccount.bAccountID>, Less<Zero>>>>>>)})]
[PXODataDocumentTypesRestriction(typeof (VendorMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"VE", "VC"})]
[PXODataDocumentTypesRestriction(typeof (CustomerMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"CU", "VC"})]
[PXODataDocumentTypesRestriction(typeof (BusinessAccountMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"VE", "CU", "VC", "PR"})]
[PXODataDocumentTypesRestriction(typeof (EmployeeMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"EP", "EC"})]
[PXODataDocumentTypesRestriction(typeof (BranchMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"CP", "OR"})]
[PXODataDocumentTypesRestriction(typeof (OrganizationMaint), DocumentTypeField = typeof (BAccount.type), RestrictRightsTo = new object[] {"CP", "OR"})]
[PXCacheName]
[CREmailContactsView(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where<Contact.bAccountID, Equal<Optional<BAccount.bAccountID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>))]
[DebuggerDisplay("{GetType().Name,nq}: BAccountID = {BAccountID,nq}, AcctCD = {AcctCD}, AcctName = {AcctName}")]
[Serializable]
public class BAccount : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IPXSelectable,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BAccountID;
  protected 
  #nullable disable
  string _AcctCD;
  protected string _AcctName;
  protected string _Type;
  protected string _AcctReferenceNbr;
  protected int? _ParentBAccountID;
  protected int? _DefAddressID;
  protected int? _DefContactID;
  protected int? _DefLocationID;
  protected string _TaxRegistrationID;
  protected int? _WorkgroupID;
  protected byte[] _GroupMask;
  protected int? _OwnerID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>The identifier of the business account.</summary>
  /// <remarks>This field is auto-incremental.
  /// This field is a surrogate key, as opposed to the natural key <see cref="P:PX.Objects.CR.BAccount.AcctCD" />.
  /// </remarks>
  [PXDBIdentity]
  [PXUIField]
  [BAccountCascade]
  [PXReferentialIntegrityCheck]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The human-readable identifier of the business account that is
  /// specified by the user or defined by the auto-numbering sequence during the
  /// creation of the account. This field is a natural key, as opposed
  /// to the surrogate key <see cref="P:PX.Objects.CR.BAccount.BAccountID" />.
  /// </summary>
  [PXDimensionSelector("BIZACCT", typeof (Search<BAccount.acctCD, Where<Match<Current<AccessInfo.userName>>>>), typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataWarning]
  public virtual string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  /// <summary>
  /// The full business account name (as opposed to the
  /// short identifier <see cref="P:PX.Objects.CR.BAccount.AcctCD" />).
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  public virtual string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.CRCustomerClass">business acccount class</see>
  /// to which the business account belongs.
  /// </summary>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Business Account Class")]
  [PXSelector(typeof (CRCustomerClass.cRCustomerClassID), DescriptionField = typeof (CRCustomerClass.description), CacheGlobal = true)]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXDefault(typeof (CRSetup.defaultCustomerClassID))]
  public virtual string ClassID { get; set; }

  /// <summary>
  /// The legal name of the company that is used by the
  /// <see cref="P:PX.Objects.CS.FeaturesSet.Reporting1099">1099 Reporting</see> feature only (see <see cref="T:PX.Objects.GL.DAC.Organization" />).
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<EntryStatus, IBqlEntryStatus>.IsEqual<EntryStatus.inserted>>, Row<BAccount.acctName>>>))]
  [PXUIField(DisplayName = "Legal Name")]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  public virtual string LegalName { get; set; }

  /// <summary>Represents the type of the business account.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.BAccountType" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.BAccountType.ProspectType" /> for a prospect,
  /// <see cref="F:PX.Objects.CR.BAccountType.CustomerType" /> for a customer,
  /// and <see cref="F:PX.Objects.CR.BAccountType.VendorType" /> for a vendor.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [BAccountType.List]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// A calculated field that indicates (if set to <c>true</c>) that <see cref="P:PX.Objects.CR.BAccount.Type" /> is either
  /// <see cref="F:PX.Objects.CR.BAccountType.CustomerType" />  or <see cref="F:PX.Objects.CR.BAccountType.CombinedType" />.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXDBCalced(typeof (Switch<Case<Where<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>, True>, False>), typeof (bool))]
  public virtual bool? IsCustomerOrCombined { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.branchType>>, True>, False>))]
  public virtual bool? IsBranch { get; set; }

  /// <summary>The external reference number of the business account.</summary>
  /// <remarks>It can be an additional number of the business account used in external integration.
  /// </remarks>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(true, true)]
  public virtual string AcctReferenceNbr
  {
    get => this._AcctReferenceNbr;
    set => this._AcctReferenceNbr = value;
  }

  /// <summary>The identifier of the parent business account.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field of the parent account.
  /// This field is used for consolidating customer account balances on the parent account from child accounts.
  /// </value>
  [ParentBAccount(typeof (BAccount.bAccountID), null, null, null, null)]
  [PXForeignReference(typeof (Field<BAccount.parentBAccountID>.IsRelatedTo<BAccount.bAccountID>))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? ParentBAccountID
  {
    get => this._ParentBAccountID;
    set => this._ParentBAccountID = value;
  }

  /// <summary>
  /// The total balance of the parent customer account
  /// including balances of its child accounts for which the value of this field is <tt>true</tt>
  /// on the <b>Billing Info</b> tab of this form. The amount includes the balances of all open documents and prepayments.
  /// </summary>
  /// <value>
  /// This field is used in the <see cref="T:PX.Objects.AR.Customer" /> class
  /// and showed only on the Customers (AR303000) form.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidate Balance")]
  public virtual bool? ConsolidateToParent { get; set; }

  /// <summary>The field is preserved for internal use.</summary>
  [PXDBInt]
  [PXFormula(typeof (Switch<Case<Where<BAccount.parentBAccountID, IsNotNull, And<BAccount.consolidateToParent, Equal<True>>>, BAccount.parentBAccountID>, BAccount.bAccountID>))]
  public virtual int? ConsolidatingBAccountID { get; set; }

  [RestrictOrganization]
  [PXUIField(DisplayName = "Customer Restriction Group", FieldClass = "VisibilityRestriction")]
  [PXDefault(0)]
  public virtual int? COrgBAccountID { get; set; }

  [RestrictOrganization]
  [PXUIField(DisplayName = "Vendor Restriction Group", FieldClass = "VisibilityRestriction")]
  [PXDefault(0)]
  public virtual int? VOrgBAccountID { get; set; }

  /// <summary>
  /// The base <see cref="T:PX.Objects.CM.Currency" /> of the Branch.
  /// </summary>
  /// <value>
  /// This unbound field corresponds to the <see cref="P:PX.Objects.GL.DAC.Organization.BaseCuryID" />.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXDefault(typeof (Switch<Case<Where<BAccount.isBranch, Equal<True>>, Null>, Current<AccessInfo.baseCuryID>>))]
  [PXUIField(DisplayName = "Base Currency ID")]
  public virtual string BaseCuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Currency" />,
  /// which is applied to the documents of the business account.
  /// </summary>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), DescriptionField = typeof (PX.Objects.CM.Currency.description), CacheGlobal = true)]
  [PXDefault(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.isCustomerOrCombined, Equal<True>>>>>.Or<BqlOperand<BAccount.classID, IBqlString>.IsNull>, BAccount.curyID, Selector<BAccount.classID, CRCustomerClass.curyID>>))]
  [PXFormula(typeof (Default<BAccount.classID>))]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the currency rate type,
  /// which is applied to the documents of the customer.
  /// </summary>
  /// <remarks>
  /// The field is used only if the business account is a customer
  /// (that is, <see cref="P:PX.Objects.CR.BAccount.IsCustomerOrCombined" /> is <see langword="true" />).
  /// </remarks>
  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXForeignReference(typeof (Field<BAccount.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type", Visible = false, Enabled = false)]
  public virtual string CuryRateTypeID { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, indicates that the currency
  /// of business account documents (which is specified by <see cref="P:PX.Objects.CR.BAccount.CuryID" />)
  /// can be overridden by a user during document entry.
  /// </summary>
  [PXDBBool]
  [PXDefault(true, typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.isCustomerOrCombined, Equal<True>>>>>.Or<BqlOperand<BAccount.classID, IBqlString>.IsNull>, BAccount.allowOverrideCury, Selector<BAccount.classID, CRCustomerClass.allowOverrideCury>>))]
  [PXFormula(typeof (Default<BAccount.classID>))]
  [PXUIField(DisplayName = "Enable Currency Override")]
  public virtual bool? AllowOverrideCury { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, indicates that the currency rate
  /// for customer documents (which is calculated by the system
  /// from the currency rate history) can be overridden by a user
  /// during document entry.
  /// </summary>
  /// <remarks>
  /// The field is used only if the business account is a customer
  /// (that is, <see cref="P:PX.Objects.CR.BAccount.IsCustomerOrCombined" /> is <see langword="true" />).
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Rate Override", Visible = false, Enabled = false)]
  public virtual bool? AllowOverrideRate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Customer Status", Required = true)]
  [PXDefault("R")]
  [PXMassUpdatableField]
  [CustomerStatus.BusinessAccountNonCustomerList]
  public virtual string Status { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Vendor Status")]
  [VendorStatus.List]
  public virtual string VStatus { get; set; }

  /// <summary>
  /// The identifier of the marketing or sales campaign that resulted in creation of the business account.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRCampaign.CampaignID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (Search3<CRCampaign.campaignID, OrderBy<Desc<CRCampaign.campaignID>>>), DescriptionField = typeof (CRCampaign.campaignName), Filterable = true)]
  public virtual string CampaignSourceID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> record used to store address data of the business account.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Address.BAccountID">Address.BAccountID</see> value must be equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID">BAccount.BAccountID</see> value of the current business account.
  /// </remarks>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Address.addressID))]
  [PXForeignReference(typeof (Field<BAccount.defAddressID>.IsRelatedTo<Address.addressID>))]
  [PXUIField]
  [PXSelector(typeof (Search<Address.addressID>), DescriptionField = typeof (Address.displayName), DirtyRead = true)]
  public virtual int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> object used to store additional contact data of the business account.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Contact.BAccountID">Contact.BAccountID</see> value must be equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID">BAccount.BAccountID</see> value of the current business account.
  /// </remarks>
  [PXDBInt]
  [PXUIField]
  [PXForeignReference(typeof (Field<BAccount.defContactID>.IsRelatedTo<Contact.contactID>))]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXSelector(typeof (Search<Contact.contactID>), DirtyRead = true)]
  public virtual int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Location" /> object linked with the business account and marked as default.
  /// The linked location is shown on the <b>Shipping</b> tab.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Location.BAccountID">Location.BAccountID</see> value must also equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID">BAccount.BAccountID</see> value of the current business account.
  /// </remarks>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Location.locationID))]
  [PXUIField]
  [PXSelector(typeof (Search<Location.locationID, Where<Location.bAccountID, Equal<Current<BAccount.bAccountID>>>>), DescriptionField = typeof (Location.locationCD), DirtyRead = true)]
  public virtual int? DefLocationID
  {
    get => this._DefLocationID;
    set => this._DefLocationID = value;
  }

  /// <summary>
  /// The registration ID of the company in the state tax authority.
  /// </summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXPersonalDataField]
  public virtual string TaxRegistrationID
  {
    get => this._TaxRegistrationID;
    set => this._TaxRegistrationID = value;
  }

  /// <inheritdoc />
  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" /> object linked with the business account and marked as primary.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Contact.BAccountID">Contact.BAccountID</see> value must equal to
  /// the <see cref="P:PX.Objects.CR.BAccount.BAccountID">BAccount.BAccountID</see> value of the current business account.
  /// </remarks>
  [PXDBInt]
  [PXVerifySelector(typeof (FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<BqlField<BAccount.bAccountID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.And<BqlOperand<Contact.isActive, IBqlBool>.IsEqual<True>>>, Contact>.SearchFor<Contact.contactID>), new System.Type[] {typeof (Contact.displayName), typeof (Contact.salutation), typeof (Contact.phone1), typeof (Contact.eMail)})]
  [PXUIField(DisplayName = "Primary Contact")]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? PrimaryContactID { get; set; }

  /// <summary>
  /// The group mask that indicates which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the business account belongs to.
  /// </summary>
  [PXDBGroupMask(HideFromEntityTypesList = true)]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// The registered entity for government payroll reporting.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Registered Entity for Government Payroll Reporting", FieldClass = "PayrollUS")]
  public virtual bool? RegisteredEntityForReporting { get; set; }

  /// <inheritdoc />
  [Owner(typeof (BAccount.workgroupID))]
  [PXMassUpdatableField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <inheritdoc />
  [PXSearchable(1024 /*0x0400*/, "{0} {1}", new System.Type[] {typeof (BAccount.type), typeof (BAccount.acctName)}, new System.Type[] {typeof (BAccount.acctCD), typeof (BAccount.defContactID), typeof (Contact.displayName), typeof (Contact.eMail), typeof (Contact.phone1), typeof (Contact.phone2), typeof (Contact.phone3), typeof (Contact.webSite)}, NumberFields = new System.Type[] {typeof (BAccount.acctCD)}, Line1Format = "{0}{1}{3}{4}{5}", Line1Fields = new System.Type[] {typeof (BAccount.type), typeof (BAccount.acctCD), typeof (BAccount.defContactID), typeof (Contact.displayName), typeof (Contact.phone1), typeof (Contact.eMail)}, Line2Format = "{1}{2}{3}", Line2Fields = new System.Type[] {typeof (BAccount.defAddressID), typeof (Address.displayName), typeof (Address.city), typeof (Address.state)}, WhereConstraint = typeof (Where<BAccount.type, Equal<BAccountType.prospectType>>), SelectForFastIndexing = typeof (Select2<BAccount, InnerJoin<Contact, On<Contact.contactID, Equal<BAccount.defContactID>>>, Where<BAccount.type, Equal<BAccountType.prospectType>>>))]
  [PXUniqueNote(DescriptionField = typeof (BAccount.acctCD), Selector = typeof (Search<BAccount.acctCD, Where<BAccount.type, Equal<BAccountType.prospectType>>>), ActivitiesCountByParent = true, ShowInReferenceSelector = true, PopupTextEnabled = true)]
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

  /// <summary>
  /// The attributes list available for the current business account.
  /// The field is preserved for internal use.
  /// </summary>
  [CRAttributesField(typeof (BAccount.classID), new System.Type[] {typeof (PX.Objects.AR.Customer.customerClassID), typeof (PX.Objects.AP.Vendor.vendorClassID)})]
  public virtual string[] Attributes { get; set; }

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

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXInt]
  [Obsolete("This field is not used anymore")]
  public virtual int? CasesCount { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Count")]
  [Obsolete("This field is not used anymore")]
  public virtual int? Count { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Last Activity")]
  [Obsolete("This field is not used anymore")]
  public DateTime? LastActivity { get; set; }

  [PXString]
  [Obsolete("This field is not used anymore")]
  public virtual string PreviewHtml { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "View In CRM")]
  public virtual bool? ViewInCrm { get; set; }

  /// <summary>
  /// The flag identified that the <see cref="T:PX.Objects.CR.BAccount.salesTerritoryID" /> is filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" /> or can be assigned manually.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Territory", FieldClass = "SalesTerritoryManagement")]
  public virtual bool? OverrideSalesTerritory { get; set; }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" />. If <see cref="T:PX.Objects.CR.BAccount.overrideSalesTerritory" />
  /// is <see langword="false" /> then it's filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" />
  /// otherwise it's assigned by user.
  /// </summary>
  [SalesTerritoryField]
  [PXUIEnabled(typeof (Where<BqlOperand<BAccount.overrideSalesTerritory, IBqlBool>.IsEqual<True>>))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXForeignReference(typeof (BAccount.FK.SalesTerritory))]
  public virtual string SalesTerritoryID { get; set; }

  /// <summary>The name of the business account locale.</summary>
  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.description))]
  [ContacLanguageDefault(typeof (Address.countryID))]
  public virtual string LocaleName { get; set; }

  public class PK : PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>
  {
    public static BAccount Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public class UK : PrimaryKeyOf<BAccount>.By<BAccount.acctCD>
  {
    public static BAccount Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public static class FK
  {
    public class Class : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<BAccount>.By<BAccount.classID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<BAccount>.By<BAccount.parentBAccountID>
    {
    }

    public class Address : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<BAccount>.By<BAccount.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccount>.By<BAccount.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<BAccount>.By<BAccount.bAccountID, BAccount.defLocationID>
    {
    }

    public class PrimaryContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccount>.By<BAccount.primaryContactID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<BAccount>.By<BAccount.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<BAccount>.By<BAccount.curyRateTypeID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccount>.By<BAccount.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<BAccount>.By<BAccount.workgroupID>
    {
    }

    public class SalesTerritory : 
      PrimaryKeyOf<PX.Objects.CS.SalesTerritory>.By<PX.Objects.CS.SalesTerritory.salesTerritoryID>.ForeignKeyOf<BAccount>.By<BAccount.salesTerritoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount.selected>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.bAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctCD>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctName>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.classID>
  {
  }

  public abstract class legalName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.legalName>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.type>
  {
  }

  public abstract class isCustomerOrCombined : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccount.isCustomerOrCombined>
  {
  }

  public abstract class isBranch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount.isBranch>
  {
  }

  public abstract class acctReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.acctReferenceNbr>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.parentBAccountID>
  {
  }

  public abstract class consolidateToParent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccount.consolidateToParent>
  {
  }

  public abstract class consolidatingBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccount.consolidatingBAccountID>
  {
  }

  public abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.cOrgBAccountID>
  {
  }

  public abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.vOrgBAccountID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.baseCuryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.curyID>
  {
  }

  public abstract class curyRateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.curyRateTypeID>
  {
  }

  public abstract class allowOverrideCury : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount.allowOverrideCury>
  {
  }

  public abstract class allowOverrideRate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount.allowOverrideRate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.status>
  {
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Active\" instead")]
    public const string Active = "A";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Hold\" instead")]
    public const string Hold = "H";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Inactive\" instead")]
    public const string Inactive = "I";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.OneTime\" instead")]
    public const string OneTime = "T";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.CreditHold\" instead")]
    public const string CreditHold = "C";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.HoldPayments\" instead")]
    public const string HoldPayments = "P";

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.ListAttribute\" instead")]
    public class ListAttribute : CustomerStatus.BusinessAccountNonCustomerListAttribute
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.active\" instead")]
    public class active : CustomerStatus.active
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.hold\" instead")]
    public class hold : CustomerStatus.hold
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.inactive\" instead")]
    public class inactive : CustomerStatus.inactive
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.oneTime\" instead")]
    public class oneTime : CustomerStatus.oneTime
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.creditHold\" instead")]
    public class creditHold : CustomerStatus.creditHold
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.holdPayments\" instead")]
    public class holdPayments : VendorStatus.holdPayments
    {
    }
  }

  public abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.vStatus>
  {
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.Active\" instead")]
    public const string Active = "A";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.Hold\" instead")]
    public const string Hold = "H";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.HoldPayments\" instead")]
    public const string HoldPayments = "P";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.Inactive\" instead")]
    public const string Inactive = "I";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.OneTime\" instead")]
    public const string OneTime = "T";

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.ListAttribute\" instead")]
    public class ListAttribute : VendorStatus.ListAttribute
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.active\" instead")]
    public class active : VendorStatus.active
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.hold\" instead")]
    public class hold : VendorStatus.hold
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.holdPayments\" instead")]
    public class holdPayments : VendorStatus.holdPayments
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.inactive\" instead")]
    public class inactive : VendorStatus.inactive
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"VendorStatus.oneTime\" instead")]
    public class oneTime : VendorStatus.oneTime
    {
    }
  }

  public abstract class campaignSourceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.campaignSourceID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.defAddressID>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.defContactID>
  {
  }

  public abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.defLocationID>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.taxRegistrationID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.workgroupID>
  {
  }

  public abstract class primaryContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.primaryContactID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BAccount.groupMask>
  {
  }

  public abstract class registeredEntityForReporting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccount.registeredEntityForReporting>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.ownerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BAccount.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BAccount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BAccount.lastModifiedDateTime>
  {
  }

  [Obsolete("This field is not used anymore")]
  public abstract class casesCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.casesCount>
  {
  }

  [Obsolete("This field is not used anymore")]
  public abstract class count : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.count>
  {
  }

  [Obsolete("This field is not used anymore")]
  public abstract class lastActivity : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BAccount.lastActivity>
  {
  }

  [Obsolete("This field is not used anymore")]
  public abstract class previewHtml : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.previewHtml>
  {
  }

  public abstract class viewInCrm : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount.viewInCrm>
  {
  }

  public abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccount.overrideSalesTerritory>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount.salesTerritoryID>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.localeName>
  {
  }
}
