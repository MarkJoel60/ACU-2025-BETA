// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuote
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
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// The projection of the <see cref="T:PX.Objects.CR.Standalone.CRQuote" />, <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision" /> and <see cref="T:PX.Objects.CR.Standalone.CROpportunity" /> classes, which
/// contains the main properties of a project quote.
/// The records of this type are created and edited through the Project Quotes (PM304500) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.PMQuoteMaint" /> graph).
/// </summary>
[PXCacheName("Project Quote")]
[PXPrimaryGraph(typeof (PMQuoteMaint))]
[CREmailContactsView(typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>, Where2<Where<Optional<PMQuote.bAccountID>, IsNull, And<PX.Objects.CR.Contact.contactID, Equal<Optional<PMQuote.contactID>>>>, Or2<Where<Optional<PMQuote.bAccountID>, IsNotNull, And<PX.Objects.CR.Contact.bAccountID, Equal<Optional<PMQuote.bAccountID>>>>, Or<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>))]
[PXQuoteProjection(typeof (Select2<PX.Objects.CR.Standalone.CRQuote, InnerJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CRQuote.quoteID>>, LeftJoin<PX.Objects.CR.Standalone.CROpportunity, On<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<CROpportunityRevision.opportunityID>>>>, Where<PX.Objects.CR.Standalone.CRQuote.quoteType, Equal<CRQuoteTypeAttribute.project>>>))]
[PXBreakInheritance]
[Serializable]
public class PMQuote : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IPXSelectable,
  INotable
{
  protected 
  #nullable disable
  string _TermsID;
  protected int? _OpportunityAddressID;
  protected int? _OpportunityContactID;
  protected bool? _AllowOverrideContactAddress;
  private int? _BAccountID;
  protected int? _ContactID;
  protected int? _ShipContactID;
  protected int? _ShipAddressID;
  protected bool? _AllowOverrideShippingContactAddress;
  protected int? _BranchID;
  private Decimal? _amount;
  private Decimal? _curyAmount;
  private Decimal? _CuryProductsAmount;
  private Decimal? _ProductsAmount;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteID))]
  [PXFormula(typeof (PMQuote.noteID))]
  public virtual Guid? QuoteID { get; set; }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.CR.CROpportunity">opportunity</see> associated with the project quote.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.opportunityID))]
  [PXUIField]
  [PXSelector(typeof (Search2<PX.Objects.CR.CROpportunity.opportunityID, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.CROpportunity.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.CROpportunity.contactID>>>>, Where<PX.Objects.CR.BAccount.bAccountID, IsNull, Or<Match<PX.Objects.CR.BAccount, Current<AccessInfo.userName>>>>, OrderBy<Desc<PX.Objects.CR.CROpportunity.opportunityID>>>), new System.Type[] {typeof (PX.Objects.CR.CROpportunity.opportunityID), typeof (PX.Objects.CR.CROpportunity.subject), typeof (PX.Objects.CR.CROpportunity.status), typeof (PX.Objects.CR.CROpportunity.stageID), typeof (PX.Objects.CR.CROpportunity.classID), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.CROpportunity.externalRef), typeof (PX.Objects.CR.CROpportunity.closeDate)}, Filterable = true)]
  [PXFieldDescription]
  [PXRestrictor(typeof (Where<PX.Objects.CR.CROpportunity.bAccountID, Equal<Current<PMQuote.bAccountID>>, Or<Current<PMQuote.bAccountID>, IsNull>>), "The opportunity business account is not equal to the quote account of the project.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.CR.CROpportunity.isActive, IBqlBool>.IsEqual<True>>), "The project quote cannot be linked to an opportunity that is not active.", new System.Type[] {})]
  public virtual string OpportunityID { get; set; }

  /// <summary>The reference number of the project quote.</summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>,
  /// which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Projects Preferences</see> (PM101000) form.
  /// </value>
  [AutoNumber(typeof (PMSetup.quoteNumberingID), typeof (AccessInfo.businessDate))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteNbr))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.BAccount>.On<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<PMQuote.bAccountID>>>, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<PMQuote.contactID>>>, FbqlJoins.Left<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMQuote.quoteProjectID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.quoteType, Equal<CRQuoteTypeAttribute.project>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.quoteProjectID, IsNull>>>>.Or<MatchUserFor<PMProject>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.bAccountID, IsNull>>>>.Or<MatchUserFor<PX.Objects.CR.BAccount>>>>, PMQuote>.SearchFor<PMQuote.quoteNbr>), new System.Type[] {typeof (PMQuote.quoteNbr), typeof (PMQuote.status), typeof (PMQuote.subject), typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PMQuote.documentDate), typeof (PMQuote.expirationDate)}, Filterable = true, DescriptionField = typeof (PMQuote.subject))]
  [PXUIField]
  [PXFieldDescription]
  public virtual string QuoteNbr { get; set; }

  /// <summary>The type of the quote.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Sales Quote,
  /// <c>"P"</c>: Project Quote
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteType))]
  [PXUIField(DisplayName = "Type", Visible = false)]
  [CRQuoteType]
  [PXDefault("P")]
  public virtual string QuoteType { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the opportunity associated with the project quote is active.
  /// </summary>
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.isActive))]
  [PXUIField(Visible = false, DisplayName = "Opportunity Is Active")]
  public virtual bool? OpportunityIsActive { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunity.DefQuoteID" />
  [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.defQuoteID))]
  public virtual Guid? DefQuoteID { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the project quote is
  /// the primary quote of the opportunity associated with the project quote.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false, FieldClass = "CRM")]
  [PXFormula(typeof (Switch<Case<Where<PMQuote.defQuoteID, IsNotNull, And<PMQuote.quoteID, Equal<PMQuote.defQuoteID>>>, True>, False>))]
  public virtual bool? IsPrimary { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ManualTotalEntry" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.manualTotalEntry))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Amount")]
  public virtual bool? ManualTotalEntry { get; set; }

  /// <summary>
  /// The identifier of the default <see cref="T:PX.Objects.CS.Terms">terms</see>,
  /// which are applied to the documents of the customer.
  /// </summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.termsID))]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMQuote.bAccountID>>>>, Search<CustomerClass.termsID, Where<CustomerClass.customerClassID, Equal<Current<PX.Objects.AR.Customer.customerClassID>>>>>))]
  [PXFormula(typeof (Default<PMQuote.bAccountID>))]
  [PXUIField(DisplayName = "Credit Terms")]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  /// <summary>The date of the project quote.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.
  /// </value>
  [PXDBDate(BqlField = typeof (CROpportunityRevision.documentDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocumentDate { get; set; }

  /// <summary>The expiration date of the project quote.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.expirationDate))]
  [PXUIField]
  public virtual DateTime? ExpirationDate { get; set; }

  /// <summary>The status of the project quote.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Draft,
  /// <c>"A"</c>: Prepared,
  /// <c>"S"</c>: Sent,
  /// <c>"P"</c>: Pending Approval,
  /// <c>"R"</c>: Rejected,
  /// <c>"T"</c>: Accepted,
  /// <c>"O"</c>: Converted,
  /// <c>"L"</c>: Declined,
  /// <c>"C"</c>: Closed,
  /// <c>"V"</c>: Approved
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.status))]
  [PXUIField]
  [PMQuoteStatus]
  [PXDefault("D")]
  public virtual string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OpportunityAddressID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityAddressID))]
  [CROpportunityAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? OpportunityAddressID
  {
    get => this._OpportunityAddressID;
    set => this._OpportunityAddressID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OpportunityContactID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityContactID))]
  [CROpportunityContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? OpportunityContactID
  {
    get => this._OpportunityContactID;
    set => this._OpportunityContactID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.AllowOverrideContactAddress" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Contact and Address")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  /// <summary>
  /// The customer or prospective customer that is associated with the project quote
  /// and with the project created based on the project quote.
  /// </summary>
  [BAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null, BqlField = typeof (CROpportunityRevision.bAccountID))]
  [PXDefault(typeof (Search<CROpportunityRevision.bAccountID, Where<CROpportunityRevision.noteID, Equal<Current<PMQuote.quoteID>>>>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The location of the <see cref="P:PX.Objects.PM.PMQuote.BAccountID">business account</see>.
  /// </summary>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMQuote.bAccountID>>, Or<Current<PMQuote.bAccountID>, IsNull>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr), BqlField = typeof (CROpportunityRevision.locationID))]
  [PXDefault(typeof (Search<PX.Objects.CR.CROpportunity.locationID, Where<PX.Objects.CR.CROpportunity.opportunityID, Equal<Current<PMQuote.opportunityID>>>>))]
  public virtual int? LocationID { get; set; }

  /// <summary>
  /// The employee of the customer or prospective customer who is the primary contact person
  /// for the project quote and for the project created based on the project quote.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CR.CROpportunity.contactID, Where<PX.Objects.CR.CROpportunity.opportunityID, Equal<Current<PMQuote.opportunityID>>>>))]
  [ContactRaw(typeof (PMQuote.bAccountID), WithContactDefaultingByBAccount = true, BqlField = typeof (CROpportunityRevision.contactID))]
  [PXRestrictor(typeof (Where2<Where2<Where<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>, Or<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.lead>>>, And<Where<PX.Objects.CR.BAccount.type, IsNull, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.prospectType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PMQuote.bAccountID>, IsNull>>>>.Or<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<BqlField<PMQuote.bAccountID, IBqlInt>.FromCurrent>>>>), "Contact '{0}' ({1}) has opportunities for another business account.", new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.contactID)})]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ShipContactID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipContactID))]
  [CRShippingContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ShipAddressID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipAddressID))]
  [CRShippingAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.AllowOverrideShippingContactAddress" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideShippingContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideShippingContactAddress
  {
    get => this._AllowOverrideShippingContactAddress;
    set => this._AllowOverrideShippingContactAddress = value;
  }

  /// <summary>The description of the project quote.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.subject))]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Subject { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ParentBAccountID" />
  [ParentBAccount(typeof (PMQuote.bAccountID), null, null, null, null, BqlField = typeof (CROpportunityRevision.parentBAccountID))]
  [PXFormula(typeof (Selector<CROpportunityRevision.bAccountID, PX.Objects.CR.BAccount.parentBAccountID>))]
  [PXDefault(typeof (Search<CROpportunityRevision.parentBAccountID, Where<CROpportunityRevision.noteID, Equal<Current<PMQuote.quoteID>>>>))]
  public virtual int? ParentBAccountID { get; set; }

  /// <summary>The branch associated with the project quote.</summary>
  [Branch(null, null, true, true, true, IsDetail = false, TabOrder = 0, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CROpportunityRevision.branchID>, IsNotNull>, Null, Case<Where<CROpportunityRevision.locationID, IsNotNull, And<Selector<CROpportunityRevision.locationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<CROpportunityRevision.locationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<CROpportunityRevision.branchID>, IsNotNull>, Current2<CROpportunityRevision.branchID>>>>, Current<AccessInfo.branchID>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ProjectID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.projectID))]
  [PXDefault(0)]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.PM.PMProject">project</see> that was created based on the project quote.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Project ID")]
  [PXDBInt(BqlField = typeof (CROpportunityRevision.quoteProjectID))]
  [PXDimensionSelector("PROJECT", typeof (Search<PMProject.contractID, Where<PMProject.baseType, Equal<CTPRType.project>>>), typeof (PMProject.contractCD), DescriptionField = typeof (PMProject.description))]
  public virtual int? QuoteProjectID { get; set; }

  /// <summary>
  /// The reference number that will be assigned to a <see cref="T:PX.Objects.PM.PMProject">project</see> created based on the project quote.
  /// </summary>
  [PXDBString(BqlField = typeof (CROpportunityRevision.quoteProjectCD))]
  [PXUIField(DisplayName = "New Project ID")]
  [PMQuoteProjectCDDimension]
  public virtual string QuoteProjectCD { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProjectTemplate">project template</see> associated with the project quote.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Project Template", FieldClass = "TMPROJECT")]
  [PXDefault(typeof (Search<PMSetup.quoteTemplateID>))]
  [PXDimensionSelector("TMPROJECT", typeof (Search2<PMProject.contractID, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>, Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.isActive, Equal<True>>>>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.budgetLevel), typeof (PMProject.billingID), typeof (ContractBillingSchedule.type), typeof (PMProject.ownerID)}, DescriptionField = typeof (PMProject.description))]
  [PXDBInt(BqlField = typeof (CROpportunityRevision.templateID))]
  public virtual int? TemplateID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Standalone.EPEmployee">employee</see> who is responsible for the
  /// estimation of the project quote and who will be the project manager.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.Standalone.EPEmployee.bAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (CROpportunityRevision.projectManager))]
  [PMProjectManager]
  [PXUIField(DisplayName = "Project Manager")]
  public virtual int? ProjectManager { get; set; }

  /// <summary>The external reference number of the project quote.</summary>
  [PXDBString(255 /*0xFF*/, IsFixed = true, BqlField = typeof (CROpportunityRevision.externalRef))]
  [PXUIField(DisplayName = "External Ref.")]
  public virtual string ExternalRef { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CampaignSourceID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.campaignSourceID))]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (Search3<CRCampaign.campaignID, OrderBy<Desc<CRCampaign.campaignID>>>), DescriptionField = typeof (CRCampaign.campaignName), Filterable = true)]
  [PXDefault(typeof (Search<CROpportunityRevision.campaignSourceID, Where<CROpportunityRevision.noteID, Equal<Current<PMQuote.quoteID>>>>))]
  public virtual string CampaignSourceID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.WorkgroupID" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup")]
  public virtual int? WorkgroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OwnerID" />
  [Owner(typeof (CROpportunityRevision.workgroupID), BqlField = typeof (CROpportunityRevision.ownerID))]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document was hold.
  /// </summary>
  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? Hold { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been approved.
  /// </summary>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.approved))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Approved { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been rejected.
  /// </summary>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.rejected))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />)
  /// that the submission of the document was canceled due to errors.
  /// This is a service field.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? SubmitCancelled { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the approval workflow is enabled for the project quotes.
  /// </summary>
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<Current<PMSetup.quoteApprovalMapID>, IsNotNull>, True>, False>))]
  [PXUIField(DisplayName = "Approvable Setup", Visible = false, Enabled = false)]
  public virtual bool? IsSetupApprovalRequired { get; set; }

  /// <summary>
  /// A service field, which is used to configure the availability of the project quote fields.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disabled", Visible = false)]
  public virtual bool? IsDisabled
  {
    get
    {
      return new bool?(this.Status == "P" || this.Status == "A" || this.Status == "T" || this.Status == "L" || this.Status == "V" || this.Status == "R" || this.Status == "S");
    }
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsFirstQuote { get; set; }

  /// <summary>
  /// The identifier of the project quote <see cref="T:PX.Objects.CM.Extensions.Currency">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CROpportunityRevision.curyID))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID), new System.Type[] {typeof (CurrencyList.curyID), typeof (CurrencyList.description)}, CacheGlobal = true)]
  [PXUIField]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> record associated with the project quote.
  /// </summary>
  [PXDBLong(BqlField = typeof (CROpportunityRevision.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ExtPriceTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.extPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPriceTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryExtPriceTotal" />
  [PXUIField(DisplayName = "Subtotal", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.extPriceTotal), BqlField = typeof (CROpportunityRevision.curyExtPriceTotal))]
  public virtual Decimal? CuryExtPriceTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.LineTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  /// <summary>
  /// The total estimated sale of the project quote in the project quote currency.
  /// </summary>
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.lineTotal), BqlField = typeof (CROpportunityRevision.curyLineTotal))]
  [PXUIField(DisplayName = "Total Sales", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  /// <summary>
  /// The total estimated cost of the project quote in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.costTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CostTotal { get; set; }

  /// <summary>The total estimated cost of the project quote.</summary>
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.costTotal), BqlField = typeof (CROpportunityRevision.curyCostTotal))]
  [PXUIField(DisplayName = "Total Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCostTotal { get; set; }

  /// <summary>
  /// The estimated gross margin of the project quote in the base currency of the tenant.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin Amount")]
  public virtual Decimal? GrossMarginAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMQuote.lineTotal), typeof (PMQuote.costTotal)})] get
    {
      Decimal? lineTotal = this.LineTotal;
      Decimal? costTotal = this.CostTotal;
      return !(lineTotal.HasValue & costTotal.HasValue) ? new Decimal?() : new Decimal?(lineTotal.GetValueOrDefault() - costTotal.GetValueOrDefault());
    }
  }

  /// <summary>The estimated gross margin of the project quote.</summary>
  /// <value>
  /// Calculated as the difference between <see cref="P:PX.Objects.PM.PMQuote.CuryLineTotal">Total Sales</see> and <see cref="P:PX.Objects.PM.PMQuote.CuryCostTotal">Total Cost</see>.
  /// </value>
  [PXCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.grossMarginAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin Amount")]
  public virtual Decimal? CuryGrossMarginAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMQuote.curyLineTotal), typeof (PMQuote.curyCostTotal)})] get
    {
      Decimal? curyLineTotal = this.CuryLineTotal;
      Decimal? curyCostTotal = this.CuryCostTotal;
      return !(curyLineTotal.HasValue & curyCostTotal.HasValue) ? new Decimal?() : new Decimal?(curyLineTotal.GetValueOrDefault() - curyCostTotal.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The percentage of the estimated gross margin of the project quote.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin (%)")]
  public virtual Decimal? GrossMarginPct
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMQuote.lineTotal), typeof (PMQuote.costTotal)})] get
    {
      Decimal? lineTotal1 = this.LineTotal;
      Decimal num1 = 0M;
      if (lineTotal1.GetValueOrDefault() == num1 & lineTotal1.HasValue)
        return new Decimal?(0M);
      Decimal num2 = (Decimal) 100;
      Decimal? lineTotal2 = this.LineTotal;
      Decimal? nullable1 = this.CostTotal;
      Decimal? nullable2 = lineTotal2.HasValue & nullable1.HasValue ? new Decimal?(lineTotal2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(num2 * nullable2.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      Decimal? lineTotal3 = this.LineTotal;
      return !(nullable4.HasValue & lineTotal3.HasValue) ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() / lineTotal3.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The overall total of the project quote in the base currency of the tenant.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quote Total")]
  public virtual Decimal? QuoteTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMQuote.lineTotal), typeof (PMQuote.taxTotal), typeof (PMQuote.taxInclTotal)})] get
    {
      Decimal? lineTotal = this.LineTotal;
      Decimal? quoteTotal = this.TaxTotal;
      Decimal? nullable = lineTotal.HasValue & quoteTotal.HasValue ? new Decimal?(lineTotal.GetValueOrDefault() + quoteTotal.GetValueOrDefault()) : new Decimal?();
      quoteTotal = this.TaxInclTotal;
      Decimal valueOrDefault = quoteTotal.GetValueOrDefault();
      if (nullable.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - valueOrDefault);
      quoteTotal = new Decimal?();
      return quoteTotal;
    }
  }

  /// <summary>The overall total of the project quote.</summary>
  /// <value>
  /// Calculated as the sum of the <see cref="P:PX.Objects.PM.PMQuote.CuryLineTotal">Total Sales</see> and <see cref="P:PX.Objects.PM.PMQuote.CuryTaxTotal">Tax Total</see> amounts.
  /// </value>
  [PXCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.quoteTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quote Total")]
  public virtual Decimal? CuryQuoteTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMQuote.curyLineTotal), typeof (PMQuote.curyTaxTotal), typeof (PMQuote.curyTaxInclTotal)})] get
    {
      Decimal? curyLineTotal = this.CuryLineTotal;
      Decimal? curyQuoteTotal = this.CuryTaxTotal;
      Decimal? nullable = curyLineTotal.HasValue & curyQuoteTotal.HasValue ? new Decimal?(curyLineTotal.GetValueOrDefault() + curyQuoteTotal.GetValueOrDefault()) : new Decimal?();
      curyQuoteTotal = this.CuryTaxInclTotal;
      Decimal valueOrDefault = curyQuoteTotal.GetValueOrDefault();
      if (nullable.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - valueOrDefault);
      curyQuoteTotal = new Decimal?();
      return curyQuoteTotal;
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.LineDiscountTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDiscountTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryLineDiscountTotal" />
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.lineDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDiscountTotal))]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDiscountTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.LineDocDiscountTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDocDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDocDiscountTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryLineDocDiscountTotal" />
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.lineDocDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDocDiscountTotal))]
  [PXUIField(Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDocDiscountTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.CROpportunityProducts.TextForProductsGrid" />
  [PXUIField(DisplayName = "Availability footer", Enabled = false)]
  [PXString]
  public virtual string TextForProductsGrid
  {
    get
    {
      Decimal? nullable = this.CuryExtPriceTotal;
      string str1 = nullable.ToString();
      nullable = this.CuryLineDiscountTotal;
      string str2 = nullable.ToString();
      return $"Subtotal: {str1}, Line Discount Subtotal: {str2}";
    }
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the amount of tax calculated with the external tax engine, such as Avalara, is up to date.
  /// If the value of this field is <see langword="false" />, the document was updated since last synchronization with the tax engine.
  /// Taxes might need recalculation.
  /// </summary>
  [PXDBBool(BqlField = typeof (CROpportunityRevision.isTaxValid))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// The total tax of the project quote in the base currency of the tenant.
  /// </summary>
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The total tax of the project quote in the project quote currency.
  /// </summary>
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.taxTotal), BqlField = typeof (CROpportunityRevision.curyTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>The total inclusive tax amount in base currency.</summary>
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.taxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Total in Base Currency", Visible = false)]
  public virtual Decimal? TaxInclTotal { get; set; }

  /// <summary>The total inclusive tax amount.</summary>
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.taxInclTotal), BqlField = typeof (CROpportunityRevision.curyTaxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Total", Visible = false)]
  public virtual Decimal? CuryTaxInclTotal { get; set; }

  /// <summary>
  /// The total estimated sale of the project quote in the base currency of the tenant.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.amount))]
  public virtual Decimal? Amount
  {
    get => this._amount;
    set => this._amount = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryAmount" />
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMQuote.curyInfoID), typeof (PMQuote.amount), BqlField = typeof (CROpportunityRevision.curyAmount))]
  [PXFormula(typeof (Switch<Case<Where<PMQuote.manualTotalEntry, Equal<True>>, PMQuote.curyAmount>, PMQuote.curyExtPriceTotal>))]
  [PXUIField]
  public virtual Decimal? CuryAmount
  {
    get => this._curyAmount;
    set => this._curyAmount = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.DiscTot" />
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryDiscTot" />
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.discTot), BqlField = typeof (CROpportunityRevision.curyDiscTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount")]
  [PXFormula(typeof (Switch<Case<Where<PMQuote.manualTotalEntry, Equal<True>>, PMQuote.curyDiscTot>, PMQuote.curyLineDocDiscountTotal>))]
  public virtual Decimal? CuryDiscTot { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryProductsAmount" />
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.productsAmount), BqlField = typeof (CROpportunityRevision.curyProductsAmount))]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryProductsAmount
  {
    set => this._CuryProductsAmount = value;
    get => this._CuryProductsAmount;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ProductsAmount" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.productsAmount))]
  public virtual Decimal? ProductsAmount
  {
    set => this._ProductsAmount = value;
    get => this._ProductsAmount;
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Wgt. Total", Enabled = false)]
  public virtual Decimal? CuryWgtAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryVatExemptTotal" />
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.vatExemptTotal), BqlField = typeof (CROpportunityRevision.curyVatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.VatExemptTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryVatTaxableTotal" />
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.vatTaxableTotal), BqlField = typeof (CROpportunityRevision.curyVatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.VatTaxableTotal" />
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <summary>The tax zone.</summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxZoneID))]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<PMQuote.branchID>))]
  [PXFormula(typeof (Default<PMQuote.locationID>))]
  [PXDefault(typeof (Search<CROpportunityRevision.taxZoneID, Where<CROpportunityRevision.noteID, Equal<Current<PMQuote.quoteID>>>>))]
  public virtual string TaxZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.TaxCalcMode" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.taxCalcMode))]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.cTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMQuote.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMQuote.locationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.TaxRegistrationID" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXDefault(typeof (Search<PX.Objects.CR.CROpportunity.taxRegistrationID, Where<PX.Objects.CR.CROpportunity.opportunityID, Equal<Current<PMQuote.opportunityID>>>>))]
  [PXPersonalDataField]
  public virtual string TaxRegistrationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ExternalTaxExemptionNumber" />
  [PXDBString(30, IsUnicode = true, BqlField = typeof (CROpportunityRevision.externalTaxExemptionNumber))]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMQuote.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMQuote.locationID>>>>>))]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.AvalaraCustomerUsageType" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.avalaraCustomerUsageType))]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMQuote.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMQuote.locationID>>>>>))]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  [PXSearchable(2048 /*0x0800*/, "Project Quote: {0} - {2}", new System.Type[] {typeof (PMQuote.quoteNbr), typeof (PMQuote.bAccountID), typeof (PX.Objects.CR.BAccount.acctName)}, new System.Type[] {typeof (PMQuote.subject)}, NumberFields = new System.Type[] {typeof (PMQuote.quoteNbr)}, MatchWithJoin = typeof (LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PMQuote.bAccountID>>>), Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (PMQuote.documentDate), typeof (PMQuote.status), typeof (PMQuote.externalRef)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (PMQuote.subject)})]
  [PXNote(DescriptionField = typeof (PMQuote.quoteNbr), Selector = typeof (PMQuote.quoteNbr), BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.noteID), ShowInReferenceSelector = true)]
  public virtual Guid? NoteID { get; set; }

  [PXExtraKey]
  [PXDBGuid(false, BqlField = typeof (CROpportunityRevision.noteID))]
  public virtual Guid? RNoteID => this.QuoteID;

  /// <summary>
  /// Provides the values of attributes associated with the project quote.
  /// The field is reserved for internal use.
  /// </summary>
  [CRAttributesField(typeof (PMProject.classID), typeof (PMQuote.quoteID))]
  public virtual string[] Attributes { get; set; }

  /// <summary>
  /// The class ID for the attributes. The field always returns the current <see cref="F:PX.Objects.PM.GroupTypes.Project" />.
  /// </summary>
  [PXString(20)]
  public virtual string ClassID => "PROJECT";

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ProductCntr" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.productCntr))]
  [PXDefault(0)]
  public virtual int? ProductCntr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.LineCntr" />
  [PXDBInt(BqlField = typeof (CROpportunityRevision.lineCntr))]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunity.OpportunityID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.opportunityID))]
  [PXExtraKey]
  public virtual string RefOpportunityID => this.OpportunityID;

  [PXString]
  [PXFormula(typeof (Selector<PMQuote.bAccountID, PX.Objects.CR.BAccount.acctName>))]
  public string FormCaptionDescription { get; set; }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.Tstamp))]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.createdByID))]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.createdDateTime))]
  [PXUIField(DisplayName = "Date Created", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.lastModifiedByID))]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

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

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CarrierID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.carrierID))]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string CarrierID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ShipTermsID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.shipTermsID))]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string ShipTermsID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ShipZoneID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.shipZoneID))]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  public virtual string ShipZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.FOBPointID" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (CROpportunityRevision.fOBPointID))]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string FOBPointID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.Resedential" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.resedential))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? Resedential { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.SaturdayDelivery" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.saturdayDelivery))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? SaturdayDelivery { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.Insurance" />
  [PXDBBool(BqlField = typeof (CROpportunityRevision.insurance))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? Insurance { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.ShipComplete" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.shipComplete))]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete { get; set; }

  public class PK : PrimaryKeyOf<PMQuote>.By<PMQuote.quoteNbr>
  {
    public static PMQuote Find(PXGraph graph, string quoteNbr, PKFindOptions options = 0)
    {
      return (PMQuote) PrimaryKeyOf<PMQuote>.By<PMQuote.quoteNbr>.FindBy(graph, (object) quoteNbr, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.selected>
  {
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.quoteID>
  {
  }

  public abstract class opportunityID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.opportunityID>
  {
  }

  public abstract class quoteNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.quoteNbr>
  {
  }

  public abstract class quoteType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.quoteType>
  {
  }

  public abstract class opportunityIsActive : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMQuote.opportunityIsActive>
  {
  }

  public abstract class defQuoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.defQuoteID>
  {
  }

  public abstract class isPrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.isPrimary>
  {
  }

  public abstract class manualTotalEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.manualTotalEntry>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.termsID>
  {
  }

  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMQuote.documentDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuote.expirationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.status>
  {
  }

  public abstract class opportunityAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMQuote.opportunityAddressID>
  {
  }

  public abstract class opportunityContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMQuote.opportunityContactID>
  {
  }

  public abstract class allowOverrideContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuote.allowOverrideContactAddress>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.locationID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.contactID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.shipContactID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.shipAddressID>
  {
  }

  public abstract class allowOverrideShippingContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuote.allowOverrideShippingContactAddress>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.subject>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.parentBAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.branchID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.projectID>
  {
  }

  public abstract class quoteProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.quoteProjectID>
  {
  }

  public abstract class quoteProjectCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.quoteProjectCD>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.templateID>
  {
  }

  public abstract class projectManager : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.projectManager>
  {
  }

  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.externalRef>
  {
  }

  public abstract class campaignSourceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.campaignSourceID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.ownerID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.rejected>
  {
  }

  public abstract class submitCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.submitCancelled>
  {
  }

  public abstract class isSetupApprovalRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMQuote.isSetupApprovalRequired>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.isDisabled>
  {
  }

  public abstract class isFirstQuote : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.isFirstQuote>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMQuote.curyInfoID>
  {
  }

  public abstract class extPriceTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.extPriceTotal>
  {
  }

  public abstract class curyExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyExtPriceTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.lineTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyLineTotal>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.costTotal>
  {
  }

  public abstract class curyCostTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyCostTotal>
  {
  }

  public abstract class grossMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.grossMarginAmount>
  {
  }

  public abstract class curyGrossMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyGrossMarginAmount>
  {
  }

  public abstract class grossMarginPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.grossMarginPct>
  {
  }

  public abstract class quoteTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.quoteTotal>
  {
  }

  public abstract class curyQuoteTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyQuoteTotal>
  {
  }

  public abstract class lineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.lineDiscountTotal>
  {
  }

  public abstract class curyLineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyLineDiscountTotal>
  {
  }

  public abstract class lineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.lineDocDiscountTotal>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyLineDocDiscountTotal>
  {
  }

  public abstract class textForProductsGrid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.textForProductsGrid>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.isTaxValid>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.taxTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyTaxTotal>
  {
  }

  public abstract class taxInclTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.taxInclTotal>
  {
  }

  public abstract class curyTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyTaxInclTotal>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.amount>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyAmount>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.discTot>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyDiscTot>
  {
  }

  public abstract class curyProductsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyProductsAmount>
  {
  }

  public abstract class productsAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.productsAmount>
  {
  }

  public abstract class curyWgtAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.curyWgtAmount>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMQuote.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMQuote.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.taxCalcMode>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.taxRegistrationID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.avalaraCustomerUsageType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.noteID>
  {
  }

  public abstract class rNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.rNoteID>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<PMQuote.attributes>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.classID>
  {
  }

  public abstract class productCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.productCntr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMQuote.lineCntr>
  {
  }

  public abstract class refOpportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.refOpportunityID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMQuote.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuote.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuote.lastModifiedDateTime>
  {
  }

  public abstract class rCreatedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.rCreatedByID>
  {
  }

  public abstract class rCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.rCreatedByScreenID>
  {
  }

  public abstract class rCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuote.rCreatedDateTime>
  {
  }

  public abstract class rLastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMQuote.rLastModifiedByID>
  {
  }

  public abstract class rLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMQuote.rLastModifiedByScreenID>
  {
  }

  public abstract class rLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMQuote.rLastModifiedDateTime>
  {
  }

  public abstract class carrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.carrierID>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.shipTermsID>
  {
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.shipZoneID>
  {
  }

  public abstract class fOBPointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.fOBPointID>
  {
  }

  public abstract class resedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.resedential>
  {
  }

  public abstract class saturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.saturdayDelivery>
  {
  }

  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMQuote.insurance>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMQuote.shipComplete>
  {
  }
}
