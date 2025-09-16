// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRQuote
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXCacheName("Sales Quote")]
[CRQuotePrimaryGraph]
[PXODataDocumentTypesRestriction(typeof (QuoteMaint))]
[CREmailContactsView(typeof (Select2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where2<Where<Optional<CRQuote.bAccountID>, IsNull, And<Contact.contactID, Equal<Optional<CRQuote.contactID>>>>, Or2<Where<Optional<CRQuote.bAccountID>, IsNotNull, And<Contact.bAccountID, Equal<Optional<CRQuote.bAccountID>>>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>))]
[PXQuoteProjection(typeof (Select2<PX.Objects.CR.Standalone.CRQuote, InnerJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CRQuote.quoteID>>, LeftJoin<PX.Objects.CR.Standalone.CROpportunity, On<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<CROpportunityRevision.opportunityID>>>>>))]
[PXBreakInheritance]
[Serializable]
public class CRQuote : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  INotable,
  IPXSelectable
{
  protected 
  #nullable disable
  string _QuoteNbr;
  protected string _TermsID;
  protected int? _OpportunityAddressID;
  protected int? _OpportunityContactID;
  protected bool? _AllowOverrideContactAddress;
  protected int? _ShipContactID;
  protected int? _ShipAddressID;
  protected bool? _AllowOverrideShippingContactAddress;
  protected int? _ContactID;
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
  [PXFormula(typeof (CRQuote.noteID))]
  public virtual Guid? QuoteID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.opportunityID))]
  [PXUIField]
  [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BAccount.bAccountID, IsNull, Or<MatchUserFor<BAccount>>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (BAccount.acctName), typeof (Contact.displayName), typeof (CROpportunity.externalRef), typeof (CROpportunity.closeDate)}, Filterable = true)]
  [PXFieldDescription]
  [PXDefault]
  public virtual string OpportunityID { get; set; }

  [AutoNumber(typeof (CRSetup.quoteNumberingID), typeof (AccessInfo.businessDate))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteNbr))]
  [PXSelector(typeof (Search2<CRQuote.quoteNbr, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRQuote.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CRQuote.contactID>>>>, Where2<Where<CRQuote.opportunityID, Equal<Optional<CRQuote.opportunityID>>, Or<CRQuote.opportunityID, IsNull>>, And<Where<BAccount.bAccountID, IsNull, Or<MatchUserFor<BAccount>>>>>, OrderBy<Desc<CRQuote.opportunityID>>>), new System.Type[] {typeof (CRQuote.quoteNbr), typeof (CRQuote.isPrimary), typeof (CRQuote.status), typeof (CRQuote.subject), typeof (BAccount.acctCD), typeof (CRQuote.documentDate), typeof (CRQuote.expirationDate), typeof (CRQuote.externalRef)}, Filterable = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string QuoteNbr
  {
    get => this._QuoteNbr;
    set => this._QuoteNbr = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.quoteType))]
  [PXUIField(DisplayName = "Type")]
  [CRQuoteType]
  [PXDefault("D")]
  public virtual string QuoteType { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.defQuoteID))]
  public virtual Guid? DefQuoteID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Primary", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CRQuote.quoteID, Equal<CRQuote.defQuoteID>>, True>, False>))]
  public virtual bool? IsPrimary { get; set; }

  [PXDBString(255 /*0xFF*/, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.externalRef))]
  [PXUIField(DisplayName = "Ext. Ref. Nbr.")]
  public virtual string ExternalRef { get; set; }

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
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<CRQuote.bAccountID>>>>, Search2<PX.Objects.AR.Customer.termsID, InnerJoin<CROpportunity, On<CROpportunity.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>>))]
  [PXFormula(typeof (Default<CRQuote.bAccountID>))]
  [PXUIField(DisplayName = "Credit Terms")]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBDate(BqlField = typeof (CROpportunityRevision.documentDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocumentDate { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.expirationDate))]
  [PXUIField]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.status))]
  [PXUIField]
  [PMQuoteStatus]
  [PXDefault]
  public virtual string Status { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityAddressID))]
  [CROpportunityAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? OpportunityAddressID
  {
    get => this._OpportunityAddressID;
    set => this._OpportunityAddressID = value;
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityContactID))]
  [CROpportunityContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? OpportunityContactID
  {
    get => this._OpportunityContactID;
    set => this._OpportunityContactID = value;
  }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null, BqlField = typeof (CROpportunityRevision.bAccountID), Enabled = false)]
  [PXDefault(typeof (Search<CROpportunity.bAccountID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual int? BAccountID { get; set; }

  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (Location.descr), BqlField = typeof (CROpportunityRevision.locationID))]
  [PXDefault(typeof (Search<CROpportunity.locationID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual int? LocationID { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipContactID))]
  [CRShippingContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipAddressID))]
  [CRShippingAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

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

  [PXDBInt(BqlField = typeof (CROpportunityRevision.billContactID))]
  [CRBillingContact(typeof (Select<Contact, Where<True, Equal<False>>>))]
  public virtual int? BillContactID { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.billAddressID))]
  [CRBillingAddress(typeof (Select<Address, Where<True, Equal<False>>>))]
  public virtual int? BillAddressID { get; set; }

  [PXDefault(typeof (Search<CROpportunity.contactID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  [ContactRaw(typeof (CRQuote.bAccountID), WithContactDefaultingByBAccount = true, BqlField = typeof (CROpportunityRevision.contactID))]
  [PXRestrictor(typeof (Where2<Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.person>>, And<Where<BAccount.type, IsNull, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRQuote.bAccountID>, IsNull>>>>.Or<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<BqlField<CRQuote.bAccountID, IBqlInt>.FromCurrent>>>>), "Contact '{0}' ({1}) has opportunities for another business account.", new System.Type[] {typeof (Contact.displayName), typeof (Contact.contactID)})]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Subject { get; set; }

  [CRMParentBAccount(typeof (CROpportunity.bAccountID), null, null, null, null, BqlField = typeof (CROpportunityRevision.parentBAccountID))]
  [PXFormula(typeof (Selector<CROpportunity.bAccountID, BAccount.parentBAccountID>))]
  [PXDefault(typeof (Search<CROpportunity.parentBAccountID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual int? ParentBAccountID { get; set; }

  [Branch(typeof (Coalesce<Search<Location.cBranchID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = false, BqlField = typeof (CROpportunityRevision.branchID))]
  [PXDefault(typeof (Search<CROpportunity.branchID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [ProjectDefault("CR", typeof (Search<Location.cDefProjectID, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Current<CRQuote.locationID>>>>>))]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInCR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (CRQuote.bAccountID), BqlField = typeof (CROpportunityRevision.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXUIField(DisplayName = "Project ID")]
  [PXDBInt(BqlField = typeof (CROpportunityRevision.quoteProjectID))]
  [PXSelector(typeof (Search<PMProject.contractID, Where<PMProject.baseType, Equal<CTPRType.project>>>), SubstituteKey = typeof (PMProject.contractCD), DescriptionField = typeof (PMProject.description))]
  public virtual int? QuoteProjectID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CROpportunityRevision.campaignSourceID))]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (Search3<CRCampaign.campaignID, OrderBy<Desc<CRCampaign.campaignID>>>), DescriptionField = typeof (CRCampaign.campaignName), Filterable = true)]
  [PXDefault(typeof (Search<CROpportunity.campaignSourceID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual string CampaignSourceID { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup")]
  public virtual int? WorkgroupID { get; set; }

  [Owner(typeof (CRQuote.workgroupID), BqlField = typeof (CROpportunityRevision.ownerID))]
  [PXDefault(typeof (Search<CROpportunity.ownerID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual int? OwnerID { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.approved))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Approved { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.rejected))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Rejected { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Hold { get; set; }

  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<Current<CRSetup.quoteApprovalMapID>, IsNotNull>, True>, False>))]
  [PXUIField(DisplayName = "Approvable Setup", Visible = false, Enabled = false)]
  public virtual bool? IsSetupApprovalRequired { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disabled", Visible = false)]
  public virtual bool? IsDisabled => new bool?(this.Status != "D");

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CROpportunityRevision.curyID))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlField = typeof (CROpportunityRevision.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.extPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPriceTotal { get; set; }

  [PXUIField(DisplayName = "Detail Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.extPriceTotal), BqlField = typeof (CROpportunityRevision.curyExtPriceTotal))]
  public virtual Decimal? CuryExtPriceTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.lineTotal), BqlField = typeof (CROpportunityRevision.curyLineTotal))]
  [PXUIField(DisplayName = "Detail Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.lineDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDiscountTotal))]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  [PXUIVisible(typeof (Where<CRQuote.manualTotalEntry, Equal<False>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDiscountTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.lineDocDiscountTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDocDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.lineDocDiscountTotal), BqlField = typeof (CROpportunityRevision.curyLineDocDiscountTotal))]
  [PXUIField(Enabled = false)]
  [PXUIVisible(typeof (Where<CRQuote.manualTotalEntry, Equal<False>, And<Not<FeatureInstalled<FeaturesSet.customerDiscounts>>>>))]
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

  [PXDBBool(BqlField = typeof (CROpportunityRevision.isTaxValid))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.taxTotal), BqlField = typeof (CROpportunityRevision.curyTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  [PXUIVisible(typeof (Where<CRQuote.manualTotalEntry, Equal<False>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.amount))]
  public virtual Decimal? Amount
  {
    get => this._amount;
    set => this._amount = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.amount), BqlField = typeof (CROpportunityRevision.curyAmount))]
  [PXFormula(typeof (Switch<Case<Where<CRQuote.manualTotalEntry, Equal<True>>, CRQuote.curyAmount>, CRQuote.curyExtPriceTotal>))]
  [PXUIField]
  public virtual Decimal? CuryAmount
  {
    get => this._curyAmount;
    set => this._curyAmount = value;
  }

  [PXDBBaseCury(BqlField = typeof (CROpportunityRevision.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryDiscTot" />
  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.discTot), BqlField = typeof (CROpportunityRevision.curyDiscTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discounts")]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRQuote.manualTotalEntry, Equal<True>>>>>.Or<Not<FeatureInstalled<FeaturesSet.customerDiscounts>>>>>, CRQuote.curyDiscTot>>, CRQuote.curyLineDocDiscountTotal>, IBqlDecimal>.IfNullThen<decimal0>))]
  public virtual Decimal? CuryDiscTot { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.productsAmount), BqlField = typeof (CROpportunityRevision.curyProductsAmount))]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<CRQuote.manualTotalEntry, Equal<True>>, Sub<CRQuote.curyAmount, CRQuote.curyDiscTot>>, CRQuote.curyProductsAmount>))]
  public virtual Decimal? CuryProductsAmount
  {
    set => this._CuryProductsAmount = value;
    get => this._CuryProductsAmount;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryOrderDiscTotal" />
  [PXCurrency(typeof (CRQuote.curyInfoID), typeof (CRQuote.orderDiscTotal))]
  [PXDBCalced(typeof (Add<CROpportunityRevision.curyDiscTot, CROpportunityRevision.curyLineDiscountTotal>), typeof (Decimal))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OrderDiscTotal" />
  [PXBaseCury]
  [PXUIField(DisplayName = "Discount Total")]
  [PXDBCalced(typeof (Add<CROpportunityRevision.discTot, CROpportunityRevision.lineDiscountTotal>), typeof (Decimal))]
  public virtual Decimal? OrderDiscTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.productsAmount))]
  public virtual Decimal? ProductsAmount
  {
    set => this._ProductsAmount = value;
    get => this._ProductsAmount;
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Wgt. Total", Enabled = false)]
  public virtual Decimal? CuryWgtAmount { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.vatExemptTotal), BqlField = typeof (CROpportunityRevision.curyVatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunity.curyInfoID), typeof (CROpportunity.vatTaxableTotal), BqlField = typeof (CROpportunityRevision.curyVatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxZoneID))]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<CRQuote.branchID>))]
  [PXFormula(typeof (Default<CRQuote.locationID>))]
  [PXDefault(typeof (Search<CROpportunity.taxZoneID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.taxCalcMode))]
  [PXDefault("T", typeof (Search<CROpportunity.taxCalcMode, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (CROpportunityRevision.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXDefault(typeof (Search<CROpportunity.taxRegistrationID, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual string TaxRegistrationID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (CROpportunityRevision.externalTaxExemptionNumber))]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  [PXDefault(typeof (Search<CROpportunity.externalTaxExemptionNumber, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.avalaraCustomerUsageType))]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0", typeof (Search<CROpportunity.avalaraCustomerUsageType, Where<CROpportunity.opportunityID, Equal<Current<CRQuote.opportunityID>>>>))]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  [PXSearchable(1024 /*0x0400*/, "Sales Quote: {0} - {2}", new System.Type[] {typeof (CRQuote.quoteNbr), typeof (CRQuote.bAccountID), typeof (BAccount.acctName)}, new System.Type[] {typeof (CRQuote.subject)}, NumberFields = new System.Type[] {typeof (CRQuote.quoteNbr)}, MatchWithJoin = typeof (LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRQuote.bAccountID>>>), Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (CRQuote.documentDate), typeof (CRQuote.status), typeof (CRQuote.externalRef)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (CRQuote.subject)})]
  [PXNote(DescriptionField = typeof (CRQuote.quoteNbr), Selector = typeof (Search2<CRQuote.quoteNbr, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRQuote.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CRQuote.contactID>>>>, Where<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>, And<Where<BAccount.bAccountID, IsNull, Or<MatchUserFor<BAccount>>>>>, OrderBy<Desc<CRQuote.quoteNbr>>>), BqlField = typeof (PX.Objects.CR.Standalone.CRQuote.noteID), ShowInReferenceSelector = true, FieldList = new System.Type[] {typeof (CRQuote.quoteNbr), typeof (CRQuote.status), typeof (CRQuote.subject), typeof (BAccount.acctCD), typeof (CRQuote.documentDate), typeof (CRQuote.expirationDate), typeof (CRQuote.externalRef)})]
  public virtual Guid? NoteID { get; set; }

  [PXExtraKey]
  [PXDBGuid(false, BqlField = typeof (CROpportunityRevision.noteID))]
  public virtual Guid? RNoteID => this.QuoteID;

  [CRAttributesField(typeof (CRQuote.opportunityClassID))]
  public virtual string[] Attributes { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.productCntr))]
  [PXDefault(0)]
  public virtual int? ProductCntr { get; set; }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.lineCntr))]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.opportunityID))]
  [PXExtraKey]
  public virtual string RefOpportunityID => this.OpportunityID;

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.classID))]
  [PXUIField(DisplayName = "Opportunity Class")]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  public virtual string OpportunityClassID { get; set; }

  [PXDBDate(PreserveTime = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.stageChangedDate))]
  [PXUIField]
  public virtual DateTime? OpportunityStageChangedDate { get; set; }

  [PXDBString(2, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.stageID))]
  [PXUIField(DisplayName = "Opportunity Stage")]
  [CROpportunityStages(typeof (CRQuote.opportunityClassID), typeof (CRQuote.opportunityStageChangedDate), OnlyActiveStages = true)]
  public virtual string OpportunityStageID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.isActive))]
  [PXUIField(Visible = false, DisplayName = "Opportunity Is Active")]
  public virtual bool? OpportunityIsActive { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.CROpportunity.status))]
  [PXUIField]
  [PXStringList(new string[] {}, new string[] {})]
  public virtual string OpportunityStatus { get; set; }

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

  [PXDBInt(BqlField = typeof (CROpportunityRevision.siteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<CRQuote.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? SiteID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.carrierID))]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string CarrierID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (CROpportunityRevision.shipTermsID))]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string ShipTermsID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (CROpportunityRevision.shipZoneID))]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  public virtual string ShipZoneID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (CROpportunityRevision.fOBPointID))]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string FOBPointID { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.resedential))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? Resedential { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.saturdayDelivery))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? SaturdayDelivery { get; set; }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.insurance))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? Insurance { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (CROpportunityRevision.shipComplete))]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete { get; set; }

  public class PK : PrimaryKeyOf<CRQuote>.By<CRQuote.opportunityID, CRQuote.quoteNbr>
  {
    public static CRQuote Find(
      PXGraph graph,
      string opportunityID,
      string quoteNbr,
      PKFindOptions options = 0)
    {
      return (CRQuote) PrimaryKeyOf<CRQuote>.By<CRQuote.opportunityID, CRQuote.quoteNbr>.FindBy(graph, (object) opportunityID, (object) quoteNbr, options);
    }
  }

  public static class FK
  {
    public class Address : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CRQuote>.By<CRQuote.opportunityAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CRQuote>.By<CRQuote.opportunityContactID>
    {
    }

    public class ShipToAddress : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CRQuote>.By<CRQuote.shipAddressID>
    {
    }

    public class ShipToContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CRQuote>.By<CRQuote.shipContactID>
    {
    }

    public class BillToAddress : 
      PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.ForeignKeyOf<CROpportunity>.By<CRQuote.billAddressID>
    {
    }

    public class BillToContactInfo : 
      PrimaryKeyOf<CRContact>.By<CRContact.contactID>.ForeignKeyOf<CROpportunity>.By<CRQuote.billContactID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRQuote>.By<CRQuote.contactID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRQuote>.By<CRQuote.bAccountID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRQuote>.By<CRQuote.parentBAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<CRQuote>.By<CRQuote.bAccountID, CRQuote.locationID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CRQuote>.By<CRQuote.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CRQuote>.By<CRQuote.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CRQuote>.By<CRQuote.curyInfoID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRQuote>.By<CRQuote.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<CRQuote>.By<CRQuote.workgroupID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.selected>
  {
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.quoteID>
  {
  }

  public abstract class opportunityID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.opportunityID>
  {
  }

  public abstract class quoteNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.quoteNbr>
  {
  }

  public abstract class quoteType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.quoteType>
  {
  }

  public abstract class defQuoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.defQuoteID>
  {
  }

  public abstract class isPrimary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.isPrimary>
  {
  }

  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.externalRef>
  {
  }

  public abstract class manualTotalEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.manualTotalEntry>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.termsID>
  {
  }

  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRQuote.documentDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.expirationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.status>
  {
  }

  public abstract class opportunityAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRQuote.opportunityAddressID>
  {
  }

  public abstract class opportunityContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRQuote.opportunityContactID>
  {
  }

  public abstract class allowOverrideContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRQuote.allowOverrideContactAddress>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.locationID>
  {
  }

  public abstract class shipContactID : IBqlField, IBqlOperand
  {
  }

  public abstract class shipAddressID : IBqlField, IBqlOperand
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
    CRQuote.allowOverrideBillingContactAddress>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.billContactID>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.billAddressID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.contactID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.subject>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.parentBAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.branchID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.projectID>
  {
  }

  public abstract class quoteProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.quoteProjectID>
  {
  }

  public abstract class campaignSourceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.campaignSourceID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.ownerID>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.rejected>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.hold>
  {
  }

  public abstract class isSetupApprovalRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRQuote.isSetupApprovalRequired>
  {
  }

  public abstract class isDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.isDisabled>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CRQuote.curyInfoID>
  {
  }

  public abstract class extPriceTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.extPriceTotal>
  {
  }

  public abstract class curyExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyExtPriceTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.lineTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.curyLineTotal>
  {
  }

  public abstract class lineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.lineDiscountTotal>
  {
  }

  public abstract class curyLineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyLineDiscountTotal>
  {
  }

  public abstract class lineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.lineDocDiscountTotal>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyLineDocDiscountTotal>
  {
  }

  public abstract class textForProductsGrid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.textForProductsGrid>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.isTaxValid>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.taxTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.curyTaxTotal>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.amount>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.curyAmount>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.discTot>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.curyDiscTot>
  {
  }

  public abstract class curyProductsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyProductsAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRQuote.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRQuote.OrderDiscTotal" />
  public abstract class orderDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.orderDiscTotal>
  {
  }

  public abstract class productsAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.productsAmount>
  {
  }

  public abstract class curyWgtAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.curyWgtAmount>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuote.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuote.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.taxCalcMode>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.taxRegistrationID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.avalaraCustomerUsageType>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.noteID>
  {
  }

  public abstract class rNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.rNoteID>
  {
  }

  public abstract class attributes : BqlType<IBqlAttributes, string[]>.Field<CRQuote.attributes>
  {
  }

  public abstract class productCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.productCntr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.lineCntr>
  {
  }

  public abstract class refOpportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.refOpportunityID>
  {
  }

  public abstract class opportunityClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.opportunityClassID>
  {
  }

  public abstract class opportunityStageChangedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.opportunityStageChangedDate>
  {
  }

  public abstract class opportunityStageID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.opportunityStageID>
  {
  }

  public abstract class opportunityIsActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRQuote.opportunityIsActive>
  {
  }

  public abstract class opportunityStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.opportunityStatus>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRQuote.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.lastModifiedDateTime>
  {
  }

  public abstract class rCreatedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.rCreatedByID>
  {
  }

  public abstract class rCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.rCreatedByScreenID>
  {
  }

  public abstract class rCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.rCreatedDateTime>
  {
  }

  public abstract class rLastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRQuote.rLastModifiedByID>
  {
  }

  public abstract class rLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRQuote.rLastModifiedByScreenID>
  {
  }

  public abstract class rLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRQuote.rLastModifiedDateTime>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRQuote.siteID>
  {
  }

  public abstract class carrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.carrierID>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.shipTermsID>
  {
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.shipZoneID>
  {
  }

  public abstract class fOBPointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.fOBPointID>
  {
  }

  public abstract class resedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.resedential>
  {
  }

  public abstract class saturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.saturdayDelivery>
  {
  }

  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRQuote.insurance>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRQuote.shipComplete>
  {
  }
}
