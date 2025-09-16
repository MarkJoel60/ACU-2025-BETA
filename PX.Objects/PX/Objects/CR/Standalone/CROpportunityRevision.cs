// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.CROpportunityRevision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

public class CROpportunityRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  public const int OpportunityIDLength = 15;
  protected int? _BranchID;
  protected int? _ContactID;
  protected bool? _AllowOverrideContactAddress;
  protected int? _OpportunityContactID;
  protected int? _OpportunityAddressID;
  protected bool? _AllowOverrideShippingContactAddress;
  protected int? _ShippingContactID;
  protected int? _ShippingAddressID;
  private Decimal? _amount;
  private Decimal? _curyAmount;
  private Decimal? _curyDiscTot;
  protected bool? _DisableAutomaticDiscountCalculation;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXNote(DescriptionField = typeof (CROpportunityRevision.opportunityID), Selector = typeof (CROpportunityRevision.opportunityID), IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string OpportunityID { get; set; }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType)}, null, null, null)]
  public virtual int? BAccountID { get; set; }

  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (Location.descr))]
  public virtual int? LocationID { get; set; }

  [Branch(typeof (Coalesce<Search<Location.cBranchID, Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunityRevision.locationID>>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>), null, true, true, true, IsDetail = false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [ContactRaw(typeof (CROpportunityRevision.bAccountID), WithContactDefaultingByBAccount = true)]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? AllowOverrideContactAddress
  {
    get => this._AllowOverrideContactAddress;
    set => this._AllowOverrideContactAddress = value;
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityContactID))]
  [CROpportunityContact(typeof (Select<PX.Objects.CR.Contact, Where<True, Equal<False>>>))]
  public virtual int? OpportunityContactID
  {
    get => this._OpportunityContactID;
    set => this._OpportunityContactID = value;
  }

  [PXDBInt]
  [CROpportunityAddress(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  public virtual int? OpportunityAddressID
  {
    get => this._OpportunityAddressID;
    set => this._OpportunityAddressID = value;
  }

  [PXDBBool(BqlField = typeof (CROpportunityRevision.allowOverrideShippingContactAddress))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Shipping Info")]
  public virtual bool? AllowOverrideShippingContactAddress
  {
    get => this._AllowOverrideShippingContactAddress;
    set => this._AllowOverrideShippingContactAddress = value;
  }

  [PXDBInt]
  public virtual int? ShipContactID
  {
    get => this._ShippingContactID;
    set => this._ShippingContactID = value;
  }

  [PXDBInt]
  public virtual int? ShipAddressID
  {
    get => this._ShippingAddressID;
    set => this._ShippingAddressID = value;
  }

  [PXDBInt]
  public virtual int? BillContactID { get; set; }

  [PXDBInt]
  public virtual int? BillAddressID { get; set; }

  [CRMParentBAccount(typeof (CROpportunityRevision.bAccountID), null, null, null, null)]
  [PXFormula(typeof (Selector<PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.parentBAccountID>))]
  public virtual int? ParentBAccountID { get; set; }

  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInCR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (CROpportunityRevision.bAccountID))]
  public virtual int? ProjectID { get; set; }

  [PXUIField(DisplayName = "Project ID")]
  [PXDBInt]
  [PXSelector(typeof (Search<PMProject.contractID, Where<PMProject.baseType, Equal<CTPRType.project>>>), SubstituteKey = typeof (PMProject.contractCD), DescriptionField = typeof (PMProject.description))]
  public virtual int? QuoteProjectID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Project ID")]
  [PXDimension("PROJECT")]
  public virtual string QuoteProjectCD { get; set; }

  [PXUIField(DisplayName = "Template", FieldClass = "PROJECT")]
  [PXDimensionSelector("PROJECT", typeof (Search2<PMProject.contractID, LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>, Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.isActive, Equal<True>>>>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.budgetLevel), typeof (ContractBillingSchedule.type), typeof (PMProject.billingID), typeof (PMProject.allocationID), typeof (PMProject.ownerID)}, DescriptionField = typeof (PMProject.description))]
  [PXDBInt]
  public virtual int? TemplateID { get; set; }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXUIField(DisplayName = "Project Manager")]
  public virtual int? ProjectManager { get; set; }

  [PXDBString(255 /*0xFF*/, IsFixed = true)]
  [PXUIField(DisplayName = "External Ref.")]
  public virtual string ExternalRef { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocumentDate { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Source Campaign")]
  [PXSelector(typeof (Search3<CRCampaign.campaignID, OrderBy<Desc<CRCampaign.campaignID>>>), DescriptionField = typeof (CRCampaign.campaignName), Filterable = true)]
  public virtual string CampaignSourceID { get; set; }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup")]
  public virtual int? WorkgroupID { get; set; }

  [Owner(typeof (CROpportunityRevision.workgroupID))]
  public virtual int? OwnerID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CR")]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.lineDiscountTotal))]
  [PXUIField(DisplayName = "Detail Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CostTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.costTotal))]
  [PXUIField(DisplayName = "Total Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCostTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ProgressiveTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.progressiveTotal))]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryProgressiveTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPriceTotal { get; set; }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Subtotal", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtPriceTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.lineDiscountTotal))]
  [PXUIField(DisplayName = "Detail Discount Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDiscountTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineDocDiscountTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.lineDocDiscountTotal))]
  [PXUIField(Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineDocDiscountTotal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.taxTotal))]
  [PXUIField(DisplayName = "Tax Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>The total inclusive tax amount in base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Total in Base Currency", Visible = false)]
  public virtual Decimal? TaxInclTotal { get; set; }

  /// <summary>The total inclusive tax amount.</summary>
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.taxInclTotal))]
  [PXUIField(DisplayName = "Inclusive Tax Total", Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxInclTotal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Amount")]
  public virtual bool? ManualTotalEntry { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? Amount
  {
    [PXDependsOnFields(new System.Type[] {typeof (CROpportunityRevision.lineTotal), typeof (CROpportunityRevision.manualTotalEntry)})] get
    {
      return !this.ManualTotalEntry.GetValueOrDefault() ? this.LineTotal : this._amount;
    }
    set => this._amount = value;
  }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<CROpportunityRevision.manualTotalEntry, Equal<True>>, CROpportunityRevision.amount>, CROpportunityRevision.lineTotal>), typeof (Decimal))]
  public virtual Decimal? RawAmount { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.amount))]
  [PXDependsOnFields(new System.Type[] {typeof (CROpportunityRevision.manualTotalEntry), typeof (CROpportunityRevision.curyLineTotal)})]
  [PXUIField]
  public virtual Decimal? CuryAmount
  {
    get
    {
      return !this.ManualTotalEntry.GetValueOrDefault() ? this.CuryLineTotal : new Decimal?(this._curyAmount.GetValueOrDefault());
    }
    set => this._curyAmount = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision.curyDiscTot">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document of the opportunity(line discounts are not included).
  /// </summary>
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount")]
  public virtual Decimal? CuryDiscTot
  {
    get => this._curyDiscTot;
    set => this._curyDiscTot = value;
  }

  [PXDependsOnFields(new System.Type[] {typeof (CROpportunityRevision.amount), typeof (CROpportunityRevision.discTot), typeof (CROpportunityRevision.manualTotalEntry), typeof (CROpportunityRevision.lineTotal)})]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Products Amount")]
  public virtual Decimal? ProductsAmount
  {
    get
    {
      Decimal? nullable = this.Amount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.DiscTot;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 - valueOrDefault2;
      nullable = this.TaxTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  [PXDependsOnFields(new System.Type[] {typeof (CROpportunityRevision.curyAmount), typeof (CROpportunityRevision.curyDiscTot), typeof (CROpportunityRevision.curyTaxTotal)})]
  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.productsAmount))]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  public virtual Decimal? CuryProductsAmount
  {
    get
    {
      Decimal? nullable = this.CuryAmount;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryDiscTot;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 - valueOrDefault2;
      nullable = this.CuryTaxTotal;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return new Decimal?(num + valueOrDefault3);
    }
  }

  /// <summary>
  /// The total <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision.curyOrderDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document and line discounts of the opportunity.
  /// </summary>
  [PXCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.orderDiscTotal))]
  [PXDBCalced(typeof (Add<CROpportunityRevision.curyDiscTot, CROpportunityRevision.curyLineDiscountTotal>), typeof (Decimal))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <summary>
  /// The total discount of the document, which is calculated as the sum of document and line discounts of the opportunity.
  /// </summary>
  [PXBaseCury]
  [PXUIField(DisplayName = "Discount Total")]
  [PXDBCalced(typeof (Add<CROpportunityRevision.discTot, CROpportunityRevision.lineDiscountTotal>), typeof (Decimal))]
  public virtual Decimal? OrderDiscTotal { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Wgt. Total", Enabled = false)]
  public virtual Decimal? CuryWgtAmount { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.vatExemptTotal))]
  [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  [PXDBCurrency(typeof (CROpportunityRevision.curyInfoID), typeof (CROpportunityRevision.vatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<CROpportunityRevision.branchID>))]
  [PXFormula(typeof (Default<CROpportunityRevision.locationID>))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<Location.cTaxCalcMode, Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunityRevision.locationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXDefault(typeof (Search<Location.taxRegistrationID, Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunityRevision.locationID>>>>>))]
  [PXPersonalDataField]
  public virtual string TaxRegistrationID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  [PXDefault(typeof (Search<Location.cAvalaraExemptionNumber, Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunityRevision.locationID>>>>>))]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0", typeof (Search<Location.cAvalaraCustomerUsageType, Where<Location.bAccountID, Equal<Current<CROpportunityRevision.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunityRevision.locationID>>>>>))]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Quote Status")]
  [CRQuoteStatus]
  public virtual string QuoteStatus { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDocDiscTot { get; set; }

  [PXDefault(typeof (Search2<CustomerClass.termsID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  public virtual string TermsID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Date Created", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ProductCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Approved { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// The language in which the contact prefers to communicate.
  /// </summary>
  /// <value>
  /// By default, the system fills in the box with the locale specified for the contact's country.
  /// This field is displayed on the form only if there are multiple active locales
  /// configured on the System Locales (SM200550) form
  /// (which corresponds to the <see cref="!:LocaleMaintenance" /> graph).
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.description))]
  [ContacLanguageDefault(typeof (CRAddress.countryID))]
  public virtual string LanguageID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<CROpportunityRevision.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? SiteID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string CarrierID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string ShipTermsID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  public virtual string ShipZoneID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string FOBPointID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? Resedential { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? SaturdayDelivery { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? Insurance { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete { get; set; }

  /// <summary>
  /// The flag identified that the <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision.salesTerritoryID" /> is filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" /> or can be assigned manually.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Territory", FieldClass = "SalesTerritoryManagement")]
  public virtual bool? OverrideSalesTerritory { get; set; }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" />. If <see cref="T:PX.Objects.CR.Standalone.CROpportunityRevision.overrideSalesTerritory" />
  /// is <see langword="false" /> then it's filled automatically
  /// based on <see cref="T:PX.Objects.CR.Address.state" /> and <see cref="T:PX.Objects.CR.Address.countryID" />
  /// otherwise it's assigned by user.
  /// </summary>
  [SalesTerritoryField]
  [PXUIEnabled(typeof (Where<BqlOperand<CROpportunityRevision.overrideSalesTerritory, IBqlBool>.IsEqual<True>>))]
  public virtual string SalesTerritoryID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityRevision.selected>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityRevision.noteID>
  {
  }

  public abstract class opportunityID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.opportunityID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.locationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.branchID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.contactID>
  {
  }

  public abstract class allowOverrideContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.allowOverrideContactAddress>
  {
  }

  public abstract class opportunityContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.opportunityContactID>
  {
  }

  public abstract class opportunityAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.opportunityAddressID>
  {
  }

  public abstract class allowOverrideShippingContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class shipContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.shipContactID>
  {
  }

  public abstract class shipAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.shipAddressID>
  {
  }

  public abstract class billContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.billContactID>
  {
  }

  public abstract class billAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.billAddressID>
  {
  }

  public abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.parentBAccountID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.projectID>
  {
  }

  public abstract class quoteProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.quoteProjectID>
  {
  }

  public abstract class quoteProjectCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.quoteProjectCD>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.templateID>
  {
  }

  public abstract class projectManager : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevision.projectManager>
  {
  }

  public abstract class externalRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.externalRef>
  {
  }

  public abstract class documentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityRevision.documentDate>
  {
  }

  public abstract class campaignSourceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.campaignSourceID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.ownerID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityRevision.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CROpportunityRevision.curyInfoID>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.lineTotal>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyLineTotal>
  {
  }

  public abstract class costTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.costTotal>
  {
  }

  public abstract class curyCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyCostTotal>
  {
  }

  public abstract class progressiveTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.progressiveTotal>
  {
  }

  public abstract class curyProgressiveTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyProgressiveTotal>
  {
  }

  public abstract class extPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.extPriceTotal>
  {
  }

  public abstract class curyExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyExtPriceTotal>
  {
  }

  public abstract class lineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.lineDiscountTotal>
  {
  }

  public abstract class curyLineDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyLineDiscountTotal>
  {
  }

  public abstract class lineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.lineDocDiscountTotal>
  {
  }

  public abstract class curyLineDocDiscountTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyLineDocDiscountTotal>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityRevision.isTaxValid>
  {
  }

  public abstract class taxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.taxTotal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyTaxTotal>
  {
  }

  public abstract class taxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.taxInclTotal>
  {
  }

  public abstract class curyTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyTaxTotal>
  {
  }

  public abstract class manualTotalEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.manualTotalEntry>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityRevision.amount>
  {
  }

  public abstract class rawAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.rawAmount>
  {
  }

  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyAmount>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityRevision.discTot>
  {
  }

  public abstract class curyDiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyDiscTot>
  {
  }

  public abstract class productsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.productsAmount>
  {
  }

  public abstract class curyProductsAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyProductsAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.OrderDiscTotal" />
  public abstract class orderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.orderDiscTotal>
  {
  }

  public abstract class curyWgtAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyWgtAmount>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.taxZoneID>
  {
  }

  public abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.taxCalcMode>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.taxRegistrationID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.avalaraCustomerUsageType>
  {
  }

  public abstract class quoteStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.quoteStatus>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.disableAutomaticDiscountCalculation>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public abstract class curyDocDiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityRevision.curyDocDiscTot>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityRevision.termsID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CROpportunityRevision.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.createdByScreenID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityRevision.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityRevision.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityRevision.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityRevision.lastModifiedDateTime>
  {
  }

  public abstract class productCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.productCntr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.lineCntr>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityRevision.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityRevision.rejected>
  {
  }

  public abstract class languageID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.languageID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityRevision.siteID>
  {
  }

  public abstract class carrierID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.carrierID>
  {
  }

  public abstract class shipTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.shipTermsID>
  {
  }

  public abstract class shipZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.shipZoneID>
  {
  }

  public abstract class fOBPointID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.fOBPointID>
  {
  }

  public abstract class resedential : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.resedential>
  {
  }

  public abstract class saturdayDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.saturdayDelivery>
  {
  }

  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityRevision.insurance>
  {
  }

  public abstract class shipComplete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.shipComplete>
  {
  }

  public abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityRevision.overrideSalesTerritory>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityRevision.salesTerritoryID>
  {
  }
}
