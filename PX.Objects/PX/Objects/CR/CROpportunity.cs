// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunity
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
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// An opportunity represents a potential, ongoing, or closed deal with a prospective or existing customer.
/// </summary>
/// <remarks>
/// An opportunity record is created on the <i>Opportunities (CR304000)</i> form, which corresponds to the <see cref="T:PX.Objects.CR.OpportunityMaint" /> graph.
/// Note that this class is a projection of the <see cref="T:PX.Objects.CR.Standalone.CROpportunity" />, <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision" />
/// and <see cref="T:PX.Objects.CR.Standalone.CRQuote" /> classes.
/// </remarks>
[PXCacheName("Opportunity")]
[PXPrimaryGraph(typeof (OpportunityMaint))]
[CREmailContactsView(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where2<Where<Optional<CROpportunity.bAccountID>, IsNull, And<Contact.contactID, Equal<Optional<CROpportunity.contactID>>>>, Or2<Where<Optional<CROpportunity.bAccountID>, IsNotNull, And<Contact.bAccountID, Equal<Optional<CROpportunity.bAccountID>>>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>))]
[PXProjection(typeof (Select2<PX.Objects.CR.Standalone.CROpportunity, InnerJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>, LeftJoin<PX.Objects.CR.Standalone.CRQuote, On<PX.Objects.CR.Standalone.CRQuote.quoteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>>>>), new System.Type[] {typeof (PX.Objects.CR.Standalone.CROpportunity), typeof (CROpportunityRevision)})]
[PXGroupMask(typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>, And<Match<BAccount, Current<AccessInfo.userName>>>>>), WhereRestriction = typeof (Where<BAccount.bAccountID, IsNotNull, Or<CROpportunity.bAccountID, IsNull>>))]
[Serializable]
public class CROpportunity : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IPXSelectable,
  INotable
{
  public const int OpportunityIDLength = 15;
  protected int? _OpportunityAddressID;
  protected int? _OpportunityContactID;
  protected bool? _AllowOverrideContactAddress;
  protected int? _ContactID;
  protected int? _ShipContactID;
  protected int? _ShipAddressID;
  protected bool? _AllowOverrideShippingContactAddress;
  private Decimal? _amount;
  private Decimal? _discTot;
  private Decimal? _curyAmount;
  private Decimal? _curyDiscTot;
  protected 
  #nullable disable
  string _QuoteNbr;

  /// <exclude />
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  /// <summary>The identifier of the opportunity.</summary>
  /// <remarks>
  /// This field depends on <see cref="T:PX.Objects.CR.CRSetup.opportunityNumberingID" />.
  /// </remarks>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.opportunityID))]
  [PXUIField]
  [AutoNumber(typeof (CRSetup.opportunityNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.curyAmount), typeof (CROpportunity.curyID), typeof (CROpportunity.closeDate), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (CROpportunity.isActive), typeof (BAccount.acctName), typeof (Contact.displayName)}, Filterable = true)]
  [PXFieldDescription]
  public virtual string OpportunityID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRAddress" /> object linked with the current document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CR.CRAddress.addressID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityAddressID))]
  [CROpportunityAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? OpportunityAddressID
  {
    get => this._OpportunityAddressID;
    set => this._OpportunityAddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRContact" /> object linked with the current document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CR.CRContact.contactID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityContactID))]
  [CROpportunityContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? OpportunityContactID
  {
    get => this._OpportunityContactID;
    set => this._OpportunityContactID = value;
  }

  /// <summary>
  /// The identifier of the default <see cref="T:PX.Objects.CS.Terms">terms</see>,
  /// which are applied to the documents of the customer.
  /// </summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.termsID))]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<CROpportunity.bAccountID>>>>))]
  [PXFormula(typeof (Default<CROpportunity.bAccountID>))]
  [PXUIField(DisplayName = "Credit Terms")]
  public virtual string TermsID { get; set; }

  /// <summary>
  /// Specifies whether the <see cref="T:PX.Objects.CR.Contact">contact</see>
  /// and <see cref="T:PX.Objects.CR.Address">address</see> information of this opportunity differs from
  /// the contact and address information
  /// of the <see cref="T:PX.Objects.CR.BAccount">business account</see> associated with this opportunity.
  /// </summary>
  /// <remarks>
  /// The behavior is controlled by the <see cref="T:PX.Objects.CR.OpportunityMaint.ContactAddress" /> graph extension derived from the <see cref="T:PX.Objects.CR.Extensions.CROpportunityContactAddress.CROpportunityContactAddressExt`1" />
  /// graph extension.
  /// </remarks>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  /// <summary>
  /// The identifier of the related <see cref="T:PX.Objects.CR.BAccount">business account</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null, BqlField = typeof (CROpportunityRevision.bAccountID))]
  public virtual int? BAccountID { get; set; }

  /// <summary>
  /// The identifier of the default location <see cref="T:PX.Objects.CR.Location" /> object linked with the prospective or existing customer selected in the Business Account box.
  /// If no location is selected in this box, the settings on the <b>Shipping</b> tab are empty and available for editing.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  /// <remarks>
  /// Also, the <see cref="P:PX.Objects.CR.Location.BAccountID">Location.BAccountID</see> value must be equal to
  /// the <see cref="P:PX.Objects.CR.CROpportunity.BAccountID">CROpportunity.BAccountID</see> value of the current opportunity.
  /// </remarks>
  [PXDefault]
  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>>), DisplayName = "Account Location", DescriptionField = typeof (Location.descr), BqlField = typeof (CROpportunityRevision.locationID))]
  public virtual int? LocationID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.GL.Branch" /> that will be used to ship the goods to the customer.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CROpportunity.branchID>, IsNotNull>, Null, Case<Where<CROpportunity.locationID, IsNotNull, And<Selector<CROpportunity.locationID, Location.cBranchID>, IsNotNull>>, Selector<CROpportunity.locationID, Location.cBranchID>, Case<Where<Current2<CROpportunity.branchID>, IsNotNull>, Current2<CROpportunity.branchID>>>>, Current<AccessInfo.branchID>>))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact" />, the representative to be contacted about the opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [ContactRaw(typeof (CROpportunity.bAccountID), WithContactDefaultingByBAccount = true, BqlField = typeof (CROpportunityRevision.contactID))]
  [PXRestrictor(typeof (Where2<Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.person>>, And<Where<BAccount.type, IsNull, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CROpportunity.bAccountID>, IsNull>>>>.Or<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>>), "Contact '{0}' ({1}) has opportunities for another business account.", new System.Type[] {typeof (Contact.displayName), typeof (Contact.contactID)})]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CRLead">lead</see> that has been converted to this opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRLead.NoteID" /> field.
  /// </value>
  [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.leadID))]
  [PXUIField(DisplayName = "Source Lead", Enabled = false)]
  [PXSelector(typeof (Search<Contact.noteID, Where<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>))]
  public virtual Guid? LeadID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.CROpportunityClass" />.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CROpportunityClass.CROpportunityClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.classID))]
  [PXUIField(DisplayName = "Opportunity Class")]
  [PXDefault]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  [PXMassUpdatableField]
  public virtual string ClassID { get; set; }

  /// <summary>The subject or description of the opportunity.</summary>
  /// <value>
  /// An alphanumeric string of up to 255 characters that describes the opportunity.
  /// </value>
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.subject))]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  /// <summary>
  /// The detailed description or any relevant notes of the opportunity
  /// </summary>
  /// <value>The value is in rich text format.</value>
  [PXDBText(IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.details))]
  [PXUIField(DisplayName = "Details")]
  public virtual string Details { get; set; }

  /// <summary>The identifier of the parent business account.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field of the parent account.
  /// This field is used for consolidating customer account balances on the parent account from child accounts.
  /// </value>
  [CRMParentBAccount(typeof (CROpportunity.bAccountID), null, null, null, null, BqlField = typeof (CROpportunityRevision.parentBAccountID))]
  [PXFormula(typeof (Selector<CROpportunity.bAccountID, BAccount.parentBAccountID>))]
  public virtual int? ParentBAccountID { get; set; }

  /// <summary>
  /// The identifier of the shipping contact that is associated with this opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRContact.ContactID" /> field.
  /// </value>
  /// <remark>
  /// The initial value of the field is taken from the corresponding shipping contact.
  /// </remark>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipContactID))]
  [CRShippingContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  /// <summary>
  /// The identifier of the shipping address that is associated with this opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRAddress.AddressID" /> field.
  /// </value>
  /// <remark>
  /// The initial value can be taken from the associated Location or BAccount object, or this behavior can be overridden.
  /// </remark>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipAddressID))]
  [CRShippingAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  /// <summary>
  /// The identifier of the billing contact that is associated with this opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRContact.ContactID" /> field.
  /// </value>
  /// <remark>
  /// The initial value is copied from the business account specified in the associated BAccount object, although this behavior it can be overridden.
  /// </remark>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.billContactID))]
  [CRBillingContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? BillContactID { get; set; }

  /// <summary>
  /// The identifier of the billing address that is associated with this opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRAddress.AddressID" /> field.
  /// </value>
  /// <remark>
  /// The initial value is copied from the business account specified in the Business Account, although it can be overridden.
  /// </remark>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.billAddressID))]
  [CRBillingAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? BillAddressID { get; set; }

  /// <summary>
  /// Specifies whether the shipping <see cref="T:PX.Objects.CR.CRContact">contact</see> of this opportunity differs from
  /// the <see cref="T:PX.Objects.CR.Contact">contact</see> information of the <see cref="T:PX.Objects.CR.BAccount">business account</see>
  /// associated with this opportunity.
  /// </summary>
  /// <remarks>
  /// The behavior is controlled by the <see cref="T:PX.Objects.CR.OpportunityMaint.ContactAddress" /> graph extension derived from the <see cref="T:PX.Objects.CR.Extensions.CROpportunityContactAddress.CROpportunityContactAddressExt`1" />
  /// graph extension.
  /// </remarks>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideShippingContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Shipping Info")]
  public virtual bool? AllowOverrideShippingContactAddress
  {
    get => this._AllowOverrideShippingContactAddress;
    set => this._AllowOverrideShippingContactAddress = value;
  }

  /// <summary>
  /// Virtual field used to set <see cref="P:PX.Objects.CR.CRBillingContact.IsDefaultContact" />
  /// and <see cref="P:PX.Objects.CR.CRBillingAddress.IsDefaultAddress" /> by the workflow.
  /// The behavior is controlled by <see cref="T:PX.Objects.CR.Extensions.CROpportunityContactAddress.CROpportunityContactAddressExt`1" />.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? AllowOverrideBillingContactAddress { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> with which the item is associated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  /// <remark>
  /// The project that is specified in the Location object of the account location. The system uses the project when it creates a document, such as a sales order.
  /// </remark>
  [ProjectDefault("CR", typeof (Search<Location.cDefProjectID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInCR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (CROpportunity.bAccountID), BqlField = typeof (CROpportunityRevision.projectID))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual int? ProjectID { get; set; }

  /// <summary>The document date.</summary>
  /// <value>Date without time.</value>
  /// <remarks>
  /// After the opportunity is closed, this field is equal to <see cref="P:PX.Objects.CR.CROpportunity.CloseDate">CloseDate</see>.
  /// </remarks>
  [PXDBDate(BqlField = typeof (CROpportunityRevision.documentDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocumentDate { get; set; }

  /// <summary>The estimated date of closing the deal.</summary>
  /// <value>Date value.</value>
  [PXDBDate(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.closeDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXMassUpdatableField]
  [PXUIField]
  public virtual DateTime? CloseDate { get; set; }

  /// <summary>The current stage of the opportunity.</summary>
  /// <value>
  /// Possible values are determined by the settings specified for the
  /// <see cref="P:PX.Objects.CR.CROpportunity.ClassID" /> opportunity class. The set of possible values can be changed and extended by using the workflow engine.
  /// </value>
  [PXDBString(2, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.stageID))]
  [PXUIField(DisplayName = "Stage")]
  [CROpportunityStages(typeof (CROpportunity.classID), typeof (CROpportunity.stageChangedDate), OnlyActiveStages = true)]
  [PXDefault]
  [PXMassUpdatableField]
  public virtual string StageID { get; set; }

  /// <summary>
  /// The date when the opportunity status or stage was changed.
  /// </summary>
  /// <value>
  /// The value is controlled by the <see cref="T:PX.Objects.CR.CROpportunityStagesAttribute">CROpportunityStages</see> attribute defined for the <see cref="P:PX.Objects.CR.CROpportunity.StageID" /> property.
  /// </value>
  [PXDBDate(PreserveTime = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.stageChangedDate))]
  [PXUIField]
  public virtual DateTime? StageChangedDate { get; set; }

  /// <summary>
  /// The marketing campaign that resulted in the creation of the opportunity.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CR.CRCampaign.CampaignID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.campaignSourceID))]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (Search3<CRCampaign.campaignID, OrderBy<Desc<CRCampaign.campaignID>>>), DescriptionField = typeof (CRCampaign.campaignName), Filterable = true)]
  public virtual string CampaignSourceID { get; set; }

  /// <summary>The current status of the opportunity.</summary>
  /// <value>
  /// The set of possible values can be changed and extended by using the workflow engine.
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.status))]
  [PXUIField]
  [OpportunityStatus.List]
  [PXDefault]
  public virtual string Status { get; set; }

  /// <summary>Indicates whether the opportunity is active.</summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.isActive))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// The reason why the status of the opportunity has been changed.
  /// </summary>
  /// <value>
  /// The possible values of the field are listed in
  /// the <see cref="T:PX.Objects.CR.OpportunityReason" /> class.
  /// </value>
  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.resolution))]
  [OpportunityReason.List]
  [PXUIField(DisplayName = "Reason")]
  [PXMassUpdatableField]
  public virtual string Resolution { get; set; }

  /// <summary>The date of the assignment of the owner.</summary>
  /// <value>
  /// The date when <see cref="P:PX.Objects.CR.CROpportunity.OwnerID" /> was assigned to the opportunity.
  /// </value>
  [PXDBDate(PreserveTime = true, InputMask = "g", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.assignDate))]
  [PXUIField(DisplayName = "Assignment Date")]
  public virtual DateTime? AssignDate { get; set; }

  /// <summary>The date of closing the opportunity.</summary>
  [PXDBDate(PreserveTime = true, InputMask = "g", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.closingDate))]
  [PXUIField(DisplayName = "Actual Close Date")]
  public virtual DateTime? ClosingDate { get; set; }

  /// <summary>The workgroup associated with the opportunity.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup")]
  [PXMassUpdatableField]
  public virtual int? WorkgroupID { get; set; }

  /// <inheritdoc />
  [Owner(typeof (CROpportunity.workgroupID), BqlField = typeof (CROpportunityRevision.ownerID))]
  [PXMassUpdatableField]
  public virtual int? OwnerID { get; set; }

  /// <summary>The currency of the opportunity.</summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.CM.Extensions.Currency" />.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CROpportunityRevision.curyID))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the transaction.
  /// </summary>
  /// <value>
  /// Generated automatically. Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong(BqlField = typeof (CROpportunityRevision.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.extPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPriceTotal { get; set; }

  [PXUIField(DisplayName = "Detail Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.extPriceTotal), BqlField = typeof (CROpportunityRevision.curyExtPriceTotal))]
  public virtual Decimal? CuryExtPriceTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.lineTotal), BqlField = typeof (CROpportunityRevision.curyLineTotal))]
  [PXUIField(DisplayName = "Detail Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.lineDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDiscountTotal))]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  [PXUIVisible(typeof (Where<CROpportunity.manualTotalEntry, Equal<False>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDiscountTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDocDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDocDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.lineDocDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDocDiscountTotal))]
  [PXUIField(Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDocDiscountTotal { get; set; }

  /// <summary>
  /// Indicates whether the tax amount calculated with the External Tax Provider is actual and does not require recalculation.
  /// </summary>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.isTaxValid))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.TaxTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.taxTotal), BqlField = typeof (CROpportunityRevision.curyTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  [PXUIVisible(typeof (Where<CROpportunity.manualTotalEntry, Equal<False>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.manualTotalEntry))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Amount")]
  public virtual bool? ManualTotalEntry { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.amount))]
  public virtual Decimal? Amount
  {
    get => this._amount;
    set => this._amount = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.discTot))]
  public virtual Decimal? DiscTot
  {
    [PXDependsOnFields(new System.Type[] {typeof (CROpportunity.curyLineDocDiscountTotal), typeof (CROpportunity.manualTotalEntry)})] get
    {
      return this._discTot;
    }
    set => this._discTot = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.amount), BqlField = typeof (CROpportunityRevision.curyAmount))]
  [PXFormula(typeof (Switch<Case<Where<CROpportunity.manualTotalEntry, Equal<True>>, CROpportunity.curyAmount>, CROpportunity.curyExtPriceTotal>))]
  [PXUIField]
  public virtual Decimal? CuryAmount
  {
    get => this._curyAmount;
    set => this._curyAmount = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryDiscTot" />
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.discTot), BqlField = typeof (CROpportunityRevision.curyDiscTot))]
  [PXUIField]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunity.manualTotalEntry, Equal<True>>>>>.Or<Not<FeatureInstalled<FeaturesSet.customerDiscounts>>>>>, CROpportunity.curyDiscTot>>, CROpportunity.curyLineDocDiscountTotal>, IBqlDecimal>.IfNullThen<decimal0>))]
  public virtual Decimal? CuryDiscTot
  {
    get => this._curyDiscTot;
    set => this._curyDiscTot = value;
  }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<CROpportunityRevision.manualTotalEntry, Equal<True>>, CROpportunityRevision.amount>, CROpportunityRevision.lineTotal>), typeof (Decimal))]
  public virtual Decimal? RawAmount { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<CROpportunityRevision.manualTotalEntry, Equal<True>>, CROpportunityRevision.curyAmount>, CROpportunityRevision.curyLineTotal>), typeof (Decimal))]
  public virtual Decimal? CuryRawAmount { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (CROpportunity.amount), typeof (CROpportunity.discTot), typeof (CROpportunity.manualTotalEntry), typeof (CROpportunity.lineTotal)})]
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.productsAmount))]
  [PXUIField(DisplayName = "Products Amount")]
  public virtual Decimal? ProductsAmount { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.productsAmount), BqlField = typeof (CROpportunityRevision.curyProductsAmount))]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CROpportunity.manualTotalEntry, Equal<True>>, Sub<CROpportunity.curyAmount, CROpportunity.curyDiscTot>>, CROpportunity.curyProductsAmount>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryProductsAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryOrderDiscTotal" />
  [PXCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.orderDiscTotal))]
  [PXDBCalced(typeof (Add<CROpportunityRevision.curyDiscTot, CROpportunityRevision.curyLineDiscountTotal>), typeof (Decimal))]
  [PXFormula(typeof (Add<CROpportunity.curyDiscTot, CROpportunity.curyLineDiscountTotal>))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OrderDiscTotal" />
  [PXBaseCury]
  [PXUIField(DisplayName = "Discount Total")]
  [PXDBCalced(typeof (Add<CROpportunityRevision.discTot, CROpportunityRevision.lineDiscountTotal>), typeof (Decimal))]
  public virtual Decimal? OrderDiscTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Weight Total", Enabled = false)]
  public virtual Decimal? CuryWgtAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.CuryVatExemptTotal" />
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.vatExemptTotal), BqlField = typeof (CROpportunityRevision.curyVatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.VatExemptTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.CuryVatTaxableTotal" />
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.vatTaxableTotal), BqlField = typeof (CROpportunityRevision.curyVatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.VatTaxableTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.TaxZoneID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxZoneID))]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  public virtual string TaxZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CA.CABankTran.TaxCalcMode" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.taxCalcMode))]
  [PXDefault("T", typeof (Search<Location.cTaxCalcMode, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.BAccount.TaxRegistrationID" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXDefault(typeof (Search<Location.taxRegistrationID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  public virtual string TaxRegistrationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ExternalTaxExemptionNumber" />
  [PXDBString(30, IsUnicode = true, BqlField = typeof (CROpportunityRevision.externalTaxExemptionNumber))]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  [PXDefault(typeof (Search<Location.cAvalaraExemptionNumber, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.AvalaraCustomerUsageType" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.avalaraCustomerUsageType))]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0", typeof (Search<Location.cAvalaraCustomerUsageType, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  /// <inheritdoc />
  [PXSearchable(1024 /*0x0400*/, "Opportunity: {0} - {2}", new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.bAccountID), typeof (BAccount.acctName)}, new System.Type[] {typeof (CROpportunity.subject)}, MatchWithJoin = typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>>), NumberFields = new System.Type[] {typeof (CROpportunity.opportunityID)}, Line1Format = "{0}{1}{2}{3}{5}", Line1Fields = new System.Type[] {typeof (CROpportunity.status), typeof (CROpportunity.resolution), typeof (CROpportunity.stageID), typeof (CROpportunity.source), typeof (CROpportunity.contactID), typeof (Contact.displayName)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CROpportunity.subject)})]
  [PXNote(DescriptionField = typeof (CROpportunity.opportunityID), Selector = typeof (CROpportunity.opportunityID), ShowInReferenceSelector = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXExtraKey]
  [PXDBGuid(true, BqlField = typeof (CROpportunityRevision.noteID))]
  public virtual Guid? QuoteNoteID => this.DefQuoteID;

  [PXExtraKey]
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.opportunityID))]
  public virtual string QuoteOpportunityID => this.OpportunityID;

  [PXExtraKey]
  [PXDBGuid(true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteID))]
  public virtual Guid? PrimaryQuoteID => this.DefQuoteID;

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteNbr))]
  [PXUIField(DisplayName = "Primary Quote Nbr.", Visible = false)]
  public virtual string PrimaryQuoteNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteType))]
  [PXUIField(DisplayName = "Primary Quote Type", Visible = false)]
  [CRQuoteType]
  [PXDefault("D")]
  public virtual string PrimaryQuoteType { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.source))]
  [PXUIField]
  [PXMassUpdatableField]
  [CRMSources]
  [PXFormula(typeof (BqlOperand<Current<CROpportunity.source>, IBqlString>.When<BqlOperand<Current<CROpportunity.source>, IBqlString>.IsNotNull>.Else<Use<Selector<CROpportunity.contactID, Contact.source>>.AsString>))]
  public virtual string Source { get; set; }

  [PXDBString(255 /*0xFF*/, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.externalRef))]
  [PXUIField(DisplayName = "Ext. Ref. Nbr.")]
  public virtual string ExternalRef { get; set; }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.Tstamp))]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.createdByID))]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.createdDateTime))]
  [PXUIField(DisplayName = "Date Created", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.lastModifiedByID))]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [CRAttributesField(typeof (CROpportunity.classID))]
  public virtual string[] Attributes { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.defQuoteID))]
  public virtual Guid? DefQuoteID { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.productCntr))]
  [PXDefault(0)]
  public virtual int? ProductCntr { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.lineCntr))]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBCreatedByID(BqlField = typeof (CROpportunityRevision.createdByID))]
  public virtual Guid? RCreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (CROpportunityRevision.createdByScreenID))]
  public virtual string RCreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (CROpportunityRevision.createdDateTime))]
  public virtual DateTime? RCreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (CROpportunityRevision.lastModifiedByID))]
  public virtual Guid? RLastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (CROpportunityRevision.lastModifiedByScreenID))]
  public virtual string RLastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (CROpportunityRevision.lastModifiedDateTime))]
  public virtual DateTime? RLastModifiedDateTime { get; set; }

  [PXBool]
  public virtual bool? CuryViewState { get; set; }

  /// <summary>
  /// The language in which the contact prefers to communicate.
  /// </summary>
  /// <value>
  /// By default, the system fills in the box with the locale specified for the contact's country.
  /// This field is displayed on the form only if there are multiple active locales
  /// configured on the <i>System Locales (SM200550)</i> form
  /// (corresponds to the <see cref="!:LocaleMaintenance" /> graph).
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = "", BqlField = typeof (CROpportunityRevision.languageID))]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.description))]
  [ContacLanguageDefault(typeof (CRAddress.countryID))]
  public virtual string LanguageID { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.siteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<CROpportunity.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cSiteID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual int? SiteID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.carrierID))]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cCarrierID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual string CarrierID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.shipTermsID))]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cShipTermsID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual string ShipTermsID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.shipZoneID))]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cShipZoneID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual string ShipZoneID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (CROpportunityRevision.fOBPointID))]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  [PXDefault(typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cFOBPointID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual string FOBPointID { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.resedential))]
  [PXDefault(false, typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cResedential>))]
  [PXUIField(DisplayName = "Residential Delivery")]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual bool? Resedential { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.saturdayDelivery))]
  [PXDefault(false, typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cSaturdayDelivery>))]
  [PXUIField(DisplayName = "Saturday Delivery")]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual bool? SaturdayDelivery { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.insurance))]
  [PXDefault(false, typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cInsurance>))]
  [PXUIField(DisplayName = "Insurance")]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual bool? Insurance { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.shipComplete))]
  [PXDefault("L", typeof (FbqlSelect<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Location.locationID, Equal<BqlField<CROpportunity.locationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<BqlField<CROpportunity.bAccountID, IBqlInt>.FromCurrent>>>, Location>.SearchFor<Location.cShipComplete>))]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  public virtual string ShipComplete { get; set; }

  /// <summary>
  /// The flag identified that the <see cref="T:PX.Objects.CR.CROpportunity.salesTerritoryID" /> is filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" /> or can be assigned manually.
  /// </summary>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.overrideSalesTerritory))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Territory", FieldClass = "SalesTerritoryManagement")]
  public virtual bool? OverrideSalesTerritory { get; set; }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" />. If <see cref="T:PX.Objects.CR.CROpportunity.overrideSalesTerritory" />
  /// is <see langword="false" /> then it's filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" />
  /// otherwise it's assigned by user.
  /// </summary>
  [SalesTerritoryField(BqlField = typeof (CROpportunityRevision.salesTerritoryID))]
  [PXUIEnabled(typeof (Where<BqlOperand<CROpportunity.overrideSalesTerritory, IBqlBool>.IsEqual<True>>))]
  [PXForeignReference(typeof (CROpportunity.FK.SalesTerritory))]
  public virtual string SalesTerritoryID { get; set; }

  public class PK : PrimaryKeyOf<CROpportunity>.By<CROpportunity.opportunityID>
  {
    public static CROpportunity Find(PXGraph graph, string opportunityID, PKFindOptions options = 0)
    {
      return (CROpportunity) PrimaryKeyOf<CROpportunity>.By<CROpportunity.opportunityID>.FindBy(graph, (object) opportunityID, options);
    }
  }

  public static class FK
  {
    public class Class : 
      PrimaryKeyOf<CROpportunityClass>.By<CROpportunityClass.cROpportunityClassID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.classID>
    {
    }

    public class Address : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.opportunityAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.opportunityContactID>
    {
    }

    public class ShipToAddress : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.shipAddressID>
    {
    }

    public class ShipToContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.shipContactID>
    {
    }

    public class BillToAddress : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.billAddressID>
    {
    }

    public class BillToContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.billContactID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.contactID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.bAccountID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.parentBAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.bAccountID, CROpportunity.locationID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.curyInfoID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.workgroupID>
    {
    }

    public class SalesTerritory : 
      PrimaryKeyOf<PX.Objects.CS.SalesTerritory>.By<PX.Objects.CS.SalesTerritory.salesTerritoryID>.ForeignKeyOf<CROpportunity>.By<CROpportunity.salesTerritoryID>
    {
    }
  }

  public class Events : PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>
  {
    public PXEntityEvent<CROpportunity> OpportunityCreatedFromLead;
    public PXEntityEvent<CROpportunity> OpportunityClosed;
    public PXEntityEvent<CROpportunity> OpportunityLost;
    public PXEntityEvent<CROpportunity> OpportunityWon;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.selected>
  {
  }

  public abstract class opportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.opportunityID>
  {
  }

  public abstract class opportunityAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunity.opportunityAddressID>
  {
  }

  public abstract class opportunityContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunity.opportunityContactID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.termsID>
  {
  }

  public abstract class allowOverrideContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunity.allowOverrideContactAddress>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.locationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.branchID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.contactID>
  {
  }

  public abstract class leadID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.leadID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.classID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.subject>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.details>
  {
  }

  public abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunity.parentBAccountID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.shipContactID>
  {
  }

  public abstract class shipAddressID : IBqlField, IBqlOperand
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.billContactID>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.billAddressID>
  {
  }

  public abstract class allowOverrideShippingContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class allowOverrideBillingContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunity.allowOverrideBillingContactAddress>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.projectID>
  {
  }

  public abstract class documentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.documentDate>
  {
  }

  public abstract class closeDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CROpportunity.closeDate>
  {
  }

  public abstract class stageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.stageID>
  {
  }

  public abstract class stageChangedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.stageChangedDate>
  {
  }

  public abstract class campaignSourceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.campaignSourceID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.status>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.isActive>
  {
  }

  public abstract class resolution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.resolution>
  {
  }

  public abstract class assignDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CROpportunity.assignDate>
  {
  }

  public abstract class closingDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.closingDate>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.ownerID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CROpportunity.curyInfoID>
  {
  }

  public abstract class extPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.extPriceTotal>
  {
  }

  public abstract class curyExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyExtPriceTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.lineTotal>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyLineTotal>
  {
  }

  public abstract class lineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.lineDiscountTotal>
  {
  }

  public abstract class curyLineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyLineDiscountTotal>
  {
  }

  public abstract class lineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.lineDocDiscountTotal>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyLineDocDiscountTotal>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.isTaxValid>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.taxTotal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyTaxTotal>
  {
  }

  public abstract class manualTotalEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunity.manualTotalEntry>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.amount>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.discTot>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.curyAmount>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.curyDiscTot>
  {
  }

  public abstract class rawAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunity.rawAmount>
  {
  }

  public abstract class curyRawAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyRawAmount>
  {
  }

  public abstract class productsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.productsAmount>
  {
  }

  public abstract class curyProductsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyProductsAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CROpportunity.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CROpportunity.OrderDiscTotal" />
  public abstract class orderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.orderDiscTotal>
  {
  }

  public abstract class curyWgtAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyWgtAmount>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunity.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.taxCalcMode>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.taxRegistrationID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.avalaraCustomerUsageType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.noteID>
  {
  }

  public abstract class quoteNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.quoteNoteID>
  {
  }

  public abstract class quoteOpportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.quoteOpportunityID>
  {
  }

  public abstract class primaryQuoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.primaryQuoteID>
  {
  }

  public abstract class quoteNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.quoteNbr>
  {
  }

  public abstract class primaryQuoteType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.primaryQuoteType>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.source>
  {
  }

  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.externalRef>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CROpportunity.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunity.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.lastModifiedDateTime>
  {
  }

  public abstract class attributes : 
    BqlType<IBqlAttributes, string[]>.Field<CROpportunity.attributes>
  {
  }

  public abstract class defQuoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.defQuoteID>
  {
  }

  public abstract class productCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.productCntr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.lineCntr>
  {
  }

  public abstract class rCreatedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunity.rCreatedByID>
  {
  }

  public abstract class rCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.rCreatedByScreenID>
  {
  }

  public abstract class rCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.rCreatedDateTime>
  {
  }

  public abstract class rLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunity.rLastModifiedByID>
  {
  }

  public abstract class rLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.rLastModifiedByScreenID>
  {
  }

  public abstract class rLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunity.rLastModifiedDateTime>
  {
  }

  [Obsolete]
  public abstract class curyViewState : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.curyViewState>
  {
  }

  public abstract class languageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.languageID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunity.siteID>
  {
  }

  public abstract class carrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.carrierID>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.shipTermsID>
  {
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.shipZoneID>
  {
  }

  public abstract class fOBPointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.fOBPointID>
  {
  }

  public abstract class resedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.resedential>
  {
  }

  public abstract class saturdayDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunity.saturdayDelivery>
  {
  }

  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunity.insurance>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunity.shipComplete>
  {
  }

  public abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunity.overrideSalesTerritory>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunity.salesTerritoryID>
  {
  }
}
