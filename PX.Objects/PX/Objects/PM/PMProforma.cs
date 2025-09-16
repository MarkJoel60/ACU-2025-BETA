// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProforma
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
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>The main properties of a pro forma invoice. The records of this type are created during the project billing process and edited through the Pro Forma
/// Invoices (PM307000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProformaEntry" /> graph).</summary>
[PXCacheName("Pro Forma Invoice")]
[PXPrimaryGraph(typeof (ProformaEntry))]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProforma : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _RefNbr;
  protected string _Description;
  protected string _Status;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected int? _ProjectID;
  protected int? _BillAddressID;
  protected int? _LocationID;
  protected string _TaxZoneID;
  protected string _AvalaraCustomerUsageType;
  protected string _CuryID;
  protected string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected string _FinPeriodID;
  protected string _TermsID;
  protected DateTime? _DiscDate;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _LineCntr;
  protected bool? _Released;
  protected string _ExtRefNbr;
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

  /// <summary>
  /// The revision number of the pro forma invoice, which is an integer
  /// that the system assigns sequentially, starting from 1.
  /// </summary>
  [PXUIField(DisplayName = "Revision")]
  [PXDBInt(IsKey = true)]
  [PXDefault(1)]
  public virtual int? RevisionID { get; set; }

  /// <summary>The reference number of the pro forma invoice.</summary>
  /// <value>
  /// The number is generated from the <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>,
  /// which is specified on the <see cref="T:PX.Objects.PM.PMSetup">Projects Preferences</see> (PM101000) form.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMProforma.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.corrected, NotEqual<True>>>>>.And<MatchUserFor<PMProject>>>, PMProforma>.SearchFor<PMProforma.refNbr>), Filterable = true)]
  [PXUIField]
  [ProformaAutoNumber]
  [PXFieldDescription]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>The application number.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string ProjectNbr { get; set; }

  /// <summary>
  /// The description of the pro forma invoice, which is provided by the billing rule
  /// and can be manually modified.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The read-only status of the document.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"H"</c>: On Hold,
  /// <c>"A"</c>: Pending Approval,
  /// <c>"O"</c>: Open,
  /// <c>"C"</c>: Closed,
  /// <c>"R"</c>: Rejected
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [ProformaStatus.List]
  [PXDefault("H")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is on hold.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold")]
  [PXDefault(true)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is approved.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document is rejected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.GL.Branch" /> to which the pro forma invoice belongs.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXDefault]
  [Branch(null, null, true, true, true, IsDetail = false)]
  public virtual int? BranchID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the pro forma invoice.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// </value>
  [PXDefault]
  [PXForeignReference(typeof (Field<PMProforma.projectID>.IsRelatedTo<PMProject.contractID>))]
  [Project(typeof (Where<BqlOperand<PMProject.customerID, IBqlInt>.IsNotNull>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> associated with the pro forma invoice.
  /// </summary>
  /// <value>
  /// Defaults to the customer associated with the project.
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDefault]
  [Customer]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMAddress">Billing Address object</see>, associated with the customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMAddress.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PMAddress(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>>, LeftJoin<PMAddress, On<PMAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<PMAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PMAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PMAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof (PMProforma.customerID))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.AR.ARContact">billing contact</see> associated with the customer.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARContact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (PMContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Billing Contact", Visible = false)]
  [PMContact(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, LeftJoin<PMContact, On<PMContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PMContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PMContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PMContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof (PMProforma.customerID))]
  public virtual int? BillContactID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMAddress">shipping address</see> associated with the customer.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMAddress.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PMShippingAddress(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<PMProforma.locationID>>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PMShippingAddress, On<PMShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<PMShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PMShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PMShippingAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof (PMProforma.customerID))]
  public virtual int? ShipAddressID { get; set; }

  /// <summary>The identifier of the <see cref="T:PX.Objects.PM.PMContact">shipping contact</see> associated with the customer.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMContact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (PMShippingContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Shipping Contact", Visible = false)]
  [PMShippingContact(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<PMProforma.locationID>>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PMShippingContact, On<PMShippingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PMShippingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PMShippingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PMShippingContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof (PMProforma.customerID))]
  public virtual int? ShipContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Location" /> associated with the pro forma invoice.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProforma.customerID>>>))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxZone" /> associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<PMProforma.locationID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>The tax exemption number for reporting purposes. The field is used if the system is integrated with an external tax calculation system and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax
  /// Calculation Integration</see> feature is enabled.</summary>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProforma.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMProforma.locationID>>>>>))]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>The customer entity type for reporting purposes. The field is used if the system is integrated with an external tax calculation system and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax
  /// Calculation Integration</see> feature is enabled.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" />.
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.CAvalaraCustomerUsageType">customer entity type</see>
  /// that is specified for the <see cref="!:CustomerLocationID">location of the customer</see>.
  /// </value>
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProforma.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMProforma.locationID>>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
  }

  /// <summary>
  /// The identifier of the pro forma invoice <see cref="T:PX.Objects.CM.Extensions.Currency">currency</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The original reference number or ID assigned by the customer to the customer document.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  /// <summary>The date on which the pro forma invoice was created.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  /// <summary>
  /// The financial period that corresponds to the <see cref="P:PX.Objects.PM.PMProforma.InvoiceDate">invoice date</see>.
  /// </summary>
  [AROpenPeriod(typeof (PMProforma.invoiceDate), typeof (PMProforma.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, IsHeader = true)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.CS.Terms">credit terms</see> object associated with the document.</summary>
  /// <value>Defaults to the <see cref="P:PX.Objects.AR.Customer.TermsID">credit terms</see> that are selected for the <see cref="P:PX.Objects.PM.PMProforma.CustomerID">customer</see>. The value corresponds to the value of the <see cref="P:PX.Objects.CS.Terms.TermsID" />
  /// field.</value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PMProject.termsID, Where<PMProject.contractID, Equal<Current<PMProforma.projectID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [Terms(typeof (PMProforma.invoiceDate), typeof (PMProforma.dueDate), typeof (PMProforma.discDate), null, null, null)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  /// <summary>The date when the payment for the document is due, in accordance with the <see cref="P:PX.Objects.PM.PMProforma.TermsID">credit terms</see>.</summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DueDate { get; set; }

  /// <summary>
  /// The end date of the cash discount period, which the system calculates by using the <see cref="P:PX.Objects.PM.PMProforma.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>The <see cref="T:PX.Objects.CR.Contact">contact</see> responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDefault(typeof (PX.Objects.CR.BAccount.ownerID))]
  [Owner]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>A counter of the document lines, which is used internally to assign <see cref="P:PX.Objects.PM.PMProformaLine.LineNbr">numbers</see> to newly created lines. We do not recommend that you
  /// rely on this field to determine the exact number of lines because it might not reflect the this number under various conditions.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has been corrected.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Corrected")]
  [PXDefault(false)]
  public virtual bool? Corrected { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has
  /// <see cref="T:PX.Objects.PM.PMProformaProgressLine">lines of project progress billing type</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Progressive Tab")]
  public virtual bool? EnableProgressive { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the document has
  /// <see cref="T:PX.Objects.PM.PMProformaTransactLine">lines of project time and material billing type</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Transactions Tab")]
  public virtual bool? EnableTransactional { get; set; }

  /// <summary>The reference number of the external document.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>The total <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryLineTotal">amount to invoice</see> of the <see cref="T:PX.Objects.PM.PMProformaTransactLine">time and material lines</see> of the document.</summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.transactionalTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Time and Material Total")]
  public virtual Decimal? CuryTransactionalTotal { get; set; }

  /// <summary>The total <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryLineTotal">amount to invoice</see> of the <see cref="T:PX.Objects.PM.PMProformaTransactLine">time and material lines</see> of the document in the base
  /// currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Time and Material Total in Base Currency")]
  public virtual Decimal? TransactionalTotal { get; set; }

  /// <summary>The total <see cref="P:PX.Objects.PM.PMProformaProgressLine.CuryLineTotal">amount to invoice</see> of the <see cref="T:PX.Objects.PM.PMProformaProgressLine">progress billing lines</see> of the document.</summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.progressiveTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Progress Billing Total")]
  public virtual Decimal? CuryProgressiveTotal { get; set; }

  /// <summary>The total <see cref="P:PX.Objects.PM.PMProformaProgressLine.CuryLineTotal">amount to invoice</see> of the <see cref="T:PX.Objects.PM.PMProformaProgressLine">progress billing lines</see> of the document in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Progress Billing Total in Base Currency")]
  public virtual Decimal? ProgressiveTotal { get; set; }

  /// <summary>The total retained amount.</summary>
  /// <value>Calculated as the sum of <see cref="P:PX.Objects.PM.PMProforma.CuryRetainageDetailTotal" /> and <see cref="P:PX.Objects.PM.PMProforma.CuryRetainageTaxTotal" />.</value>
  [PXCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.retainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.curyRetainageDetailTotal), typeof (PMProforma.curyRetainageTaxTotal), typeof (PMProforma.curyRetainageTaxInclTotal)})] get
    {
      Decimal? retainageDetailTotal = this.CuryRetainageDetailTotal;
      Decimal? curyRetainageTotal = this.CuryRetainageTaxTotal;
      Decimal? nullable = retainageDetailTotal.HasValue & curyRetainageTotal.HasValue ? new Decimal?(retainageDetailTotal.GetValueOrDefault() + curyRetainageTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? retainageTaxInclTotal = this.CuryRetainageTaxInclTotal;
      if (nullable.HasValue & retainageTaxInclTotal.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - retainageTaxInclTotal.GetValueOrDefault());
      curyRetainageTotal = new Decimal?();
      return curyRetainageTotal;
    }
  }

  /// <summary>The total retained amount in the base currency.</summary>
  /// <value>Calculated as the sum of <see cref="P:PX.Objects.PM.PMProforma.RetainageDetailTotal" /> and <see cref="P:PX.Objects.PM.PMProforma.RetainageTaxTotal" />.</value>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTotal
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.retainageDetailTotal), typeof (PMProforma.retainageTaxTotal), typeof (PMProforma.retainageTaxInclTotal)})] get
    {
      Decimal? retainageDetailTotal = this.RetainageDetailTotal;
      Decimal? retainageTotal = this.RetainageTaxTotal;
      Decimal? nullable = retainageDetailTotal.HasValue & retainageTotal.HasValue ? new Decimal?(retainageDetailTotal.GetValueOrDefault() + retainageTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? retainageTaxInclTotal = this.RetainageTaxInclTotal;
      if (nullable.HasValue & retainageTaxInclTotal.HasValue)
        return new Decimal?(nullable.GetValueOrDefault() - retainageTaxInclTotal.GetValueOrDefault());
      retainageTotal = new Decimal?();
      return retainageTotal;
    }
  }

  /// <summary>The total retained amount for the <see cref="T:PX.Objects.PM.PMProformaProgressLine">progress billing lines</see> and <see cref="T:PX.Objects.PM.PMProformaTransactLine">time and material lines</see> of the pro forma
  /// invoice.</summary>
  /// <value>Calculated as the sum of the values in the <see cref="P:PX.Objects.PM.PMProformaProgressLine.CuryRetainage">retainage amount</see> column for the lines with progressive type plus the sum of
  /// the values in the <see cref="P:PX.Objects.PM.PMProformaTransactLine.CuryRetainage">retainage amount</see> column for the lines with transaction type of the pro forma invoice.</value>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.retainageDetailTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Detail Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageDetailTotal { get; set; }

  /// <summary>The total retained amount for the <see cref="T:PX.Objects.PM.PMProformaProgressLine">progress billing lines</see> and <see cref="T:PX.Objects.PM.PMProformaTransactLine">time and material lines</see> of the pro forma invoice
  /// in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Detail Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageDetailTotal { get; set; }

  /// <summary>The total retained tax amount.</summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.retainageTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Tax Total", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTaxTotal { get; set; }

  /// <summary>The total retained tax amount in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Tax Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTaxTotal { get; set; }

  /// <summary>
  /// The total inclusive retained tax amount in document currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.retainageTaxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Inclusive Tax Total", Visible = false, Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageTaxInclTotal { get; set; }

  /// <summary>
  /// The total inclusive retained tax amount in base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage Inclusive Tax in Base Currency", Visible = false, Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? RetainageTaxInclTotal { get; set; }

  /// <summary>The total tax amount of the document.</summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total tax amount of the document in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total in Base Currency")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The total inclusive tax amount of the document in document currency.
  /// </summary>
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.taxInclTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Total", Visible = false)]
  public virtual Decimal? CuryTaxInclTotal { get; set; }

  /// <summary>
  /// The total inclusive tax amount of the document in base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Inclusive Tax Total in Base Currency", Visible = false)]
  public virtual Decimal? TaxInclTotal { get; set; }

  /// <summary>The tax amount of the document.</summary>
  /// <value>Calculated as the sum of <see cref="P:PX.Objects.PM.PMProforma.CuryTaxTotal" /> plus <see cref="P:PX.Objects.PM.PMProforma.CuryRetainageTaxTotal" />.</value>
  [PXCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.taxTotalWithRetainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total")]
  public virtual Decimal? CuryTaxTotalWithRetainage
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.curyTaxTotal), typeof (PMProforma.curyRetainageTaxTotal)})] get
    {
      Decimal? curyTaxTotal = this.CuryTaxTotal;
      Decimal? retainageTaxTotal = this.CuryRetainageTaxTotal;
      return !(curyTaxTotal.HasValue & retainageTaxTotal.HasValue) ? new Decimal?() : new Decimal?(curyTaxTotal.GetValueOrDefault() + retainageTaxTotal.GetValueOrDefault());
    }
  }

  /// <summary>The tax amount of the document in the base currency.</summary>
  /// <value>Calculated as the sum of <see cref="P:PX.Objects.PM.PMProforma.TaxTotal" /> and <see cref="P:PX.Objects.PM.PMProforma.RetainageTaxTotal" />.</value>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Due Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? TaxTotalWithRetainage
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.taxTotal), typeof (PMProforma.retainageTaxTotal)})] get
    {
      Decimal? taxTotal = this.TaxTotal;
      Decimal? retainageTaxTotal = this.RetainageTaxTotal;
      return !(taxTotal.HasValue & retainageTaxTotal.HasValue) ? new Decimal?() : new Decimal?(taxTotal.GetValueOrDefault() + retainageTaxTotal.GetValueOrDefault());
    }
  }

  /// <summary>The invoice total.</summary>
  /// <value>The sum of the <see cref="P:PX.Objects.PM.PMProforma.CuryProgressiveTotal">progress billing total</see>, <see cref="P:PX.Objects.PM.PMProforma.CuryTransactionalTotal">time and material total</see>, and tax total values.</value>
  [PXFormula(typeof (Add<PMProforma.curyTaxTotal, Add<PMProforma.curyRetainageTaxTotal, Add<Minus<PMProforma.curyTaxInclTotal>, Add<Minus<PMProforma.curyRetainageTaxInclTotal>, Add<PMProforma.curyProgressiveTotal, PMProforma.curyTransactionalTotal>>>>>))]
  [PXDBCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.docTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Invoice Total")]
  public virtual Decimal? CuryDocTotal { get; set; }

  /// <summary>The invoice total in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Invoice Total in Base Currency")]
  public virtual Decimal? DocTotal { get; set; }

  /// <summary>The amount due.</summary>
  /// <value>The difference between the <see cref="P:PX.Objects.PM.PMProforma.CuryDocTotal">Invoice Total</see> and <see cref="P:PX.Objects.PM.PMProforma.CuryRetainageTotal">Retainage Total</see>.</value>
  [PXCurrency(typeof (PMProforma.curyInfoID), typeof (PMProforma.amountDue))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Due", FieldClass = "Retainage")]
  public virtual Decimal? CuryAmountDue
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.curyDocTotal), typeof (PMProforma.curyRetainageTotal)})] get
    {
      Decimal? curyDocTotal = this.CuryDocTotal;
      Decimal? curyRetainageTotal = this.CuryRetainageTotal;
      return !(curyDocTotal.HasValue & curyRetainageTotal.HasValue) ? new Decimal?() : new Decimal?(curyDocTotal.GetValueOrDefault() - curyRetainageTotal.GetValueOrDefault());
    }
  }

  /// <summary>The amount due in the base currency.</summary>
  /// <value>The difference between the <see cref="P:PX.Objects.PM.PMProforma.DocTotal">invoice total</see> and <see cref="P:PX.Objects.PM.PMProforma.RetainageTotal">retainage total</see>.</value>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Due Total in Base Currency", FieldClass = "Retainage")]
  public virtual Decimal? AmountDue
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMProforma.docTotal), typeof (PMProforma.retainageTotal)})] get
    {
      Decimal? docTotal = this.DocTotal;
      Decimal? retainageTotal = this.RetainageTotal;
      return !(docTotal.HasValue & retainageTotal.HasValue) ? new Decimal?() : new Decimal?(docTotal.GetValueOrDefault() - retainageTotal.GetValueOrDefault());
    }
  }

  /// <summary>The allocated retained total.</summary>
  [PXDBCurrency(typeof (PMProformaLine.curyInfoID), typeof (PMProforma.allocatedRetainedTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Allocated Retained Total", Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? CuryAllocatedRetainedTotal { get; set; }

  /// <summary>The allocated retained total (in the base currency).</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedRetainedTotal { get; set; }

  /// <summary>The retainage in percents.</summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage (%)", Visible = false)]
  public virtual Decimal? RetainagePct { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the amount of tax calculated with the external tax engine (such as Avalara) is up to date. If this field equals
  /// <see langword="false"></see>, the document was updated since the last synchronization with the tax Engine and taxes might need recalculation.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the current version of the AIA report is not up-to-date and should be reprinted.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AIA Is Outdated", Enabled = false)]
  public virtual bool? IsAIAOutdated { get; set; }

  /// <summary>
  /// The type of the corresponding <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> created on release of the pro forma invoice.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARInvoiceType.ListAttribute" />.
  /// </value>
  [ARInvoiceType.List]
  [PXUIField]
  [PXDBString(3)]
  public virtual string ARInvoiceDocType { get; set; }

  /// <summary>
  /// The reference number of the corresponding <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> created on release of the pro forma invoice.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" /> field.
  /// </value>
  [PXUIField]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProforma>.On<BqlOperand<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<PMProforma.aRInvoiceRefNbr>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<BqlField<PMProforma.aRInvoiceDocType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.customerID, IBqlInt>.IsEqual<BqlField<PMProforma.customerID, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.projectID, IBqlInt>.IsEqual<BqlField<PMProforma.projectID, IBqlInt>.FromCurrent>>>, And<BqlOperand<PMProforma.aRInvoiceRefNbr, IBqlString>.IsNull>>>.Or<BqlOperand<PMProforma.aRInvoiceRefNbr, IBqlString>.IsEqual<BqlField<PMProforma.aRInvoiceRefNbr, IBqlString>.FromCurrent>>>, PX.Objects.AR.ARInvoice>.SearchFor<PX.Objects.AR.ARInvoice.refNbr>), ValidateValue = false)]
  [PXDBString(15, IsUnicode = true)]
  public virtual string ARInvoiceRefNbr { get; set; }

  /// <summary>
  /// The status of the document.
  /// The value of the field is determined by the values of the status flags,
  /// such as <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />, <see cref="F:PX.Objects.AR.ARDocStatus.Balanced" />, <see cref="F:PX.Objects.AR.ARDocStatus.Voided" />, <see cref="F:PX.Objects.AR.ARDocStatus.Scheduled" />.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
  /// </value>
  [PXString]
  [PXUIField(DisplayName = "AR Doc. Status", Enabled = false)]
  [ARDocStatus.List]
  [PXDBScalar(typeof (Search<PX.Objects.AR.ARInvoice.status, Where<PX.Objects.AR.ARInvoice.docType, Equal<PMProforma.aRInvoiceDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PMProforma.aRInvoiceRefNbr>>>>))]
  public virtual string ARInvoiceRefStatus { get; set; }

  /// <summary>
  /// The reference name to the corresponding <see cref="T:PX.Objects.AR.ARInvoice">accounts receivable document</see> created on release of the pro forma invoice.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProforma.ARInvoiceRefNbr" /> field.
  /// </value>
  [PXUIField]
  [PXString]
  public virtual string ARInvoiceRefName
  {
    get => this.ARInvoiceRefNbr;
    set => this.ARInvoiceRefNbr = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.AR.ARInvoice">AR document</see> that reverses the AR document specified in the <see cref="P:PX.Objects.PM.PMProforma.ARInvoiceRefNbr">AR Ref. Nbr.</see> field.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARInvoiceType.ListAttribute" />.
  /// </value>
  [ARInvoiceType.List]
  [PXUIField]
  [PXDBString(3)]
  public virtual string ReversedARInvoiceDocType { get; set; }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.AR.ARInvoice">AR document</see> that reverses the AR document specified in the <see cref="P:PX.Objects.PM.PMProforma.ARInvoiceRefNbr">AR Ref. Nbr.</see> field.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" /> field.
  /// </value>
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PMProforma.reversedARInvoiceDocType>>>>))]
  [PXDBString(15, IsUnicode = true)]
  public virtual string ReversedARInvoiceRefNbr { get; set; }

  /// <summary>
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Migrated", Visible = false, Enabled = false)]
  [PXDefault(false)]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// The number of detail lines linked to the pro forma invoice.
  /// </summary>
  [PXInt]
  [PXDefault(0)]
  [PXDBScalar(typeof (Search4<PMProformaLine.lineNbr, Where<PMProformaLine.refNbr, Equal<PMProforma.refNbr>, And<PMProformaLine.revisionID, Equal<PMProforma.revisionID>>>, Aggregate<Count<PMProformaLine.lineNbr>>>))]
  public virtual int? NumberOfLines { get; set; }

  [PXSearchable(2048 /*0x0800*/, "Pro Forma Invoice: {0} - {2}", new System.Type[] {typeof (PMProforma.refNbr), typeof (PMProforma.customerID), typeof (PX.Objects.AR.Customer.acctName)}, new System.Type[] {typeof (PMProforma.description), typeof (PMProforma.projectID), typeof (PMProject.contractCD), typeof (PMProject.description)}, NumberFields = new System.Type[] {typeof (PMProforma.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (PMProforma.invoiceDate), typeof (PMProforma.status), typeof (PMProforma.projectID)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (PMProforma.description)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProforma.customerID>>>), SelectForFastIndexing = typeof (Select2<PMProforma, InnerJoin<PX.Objects.AR.Customer, On<PMProforma.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<PMProject, On<PMProforma.projectID, Equal<PMProject.contractID>>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<PMProforma.projectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.corrected, NotEqual<True>>>>>.And<MatchUserFor<PMProject>>>, PMProforma>.SearchFor<PMProforma.refNbr>), DescriptionField = typeof (PMProforma.refNbr))]
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

  public class Events : PXEntityEventBase<PMProforma>.Container<PMProforma.Events>
  {
    public PXEntityEvent<PMProforma> Release;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.selected>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.revisionID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.refNbr>
  {
    public const int Length = 15;
  }

  public abstract class projectNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.projectNbr>
  {
    public const int Length = 15;
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.description>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.rejected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.branchID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.projectID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.customerID>
  {
  }

  public abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.customerID_Customer_acctName>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.billContactID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.shipContactID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.locationID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.taxZoneID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.avalaraCustomerUsageType>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMProforma.curyInfoID>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProforma.invoiceDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.finPeriodID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.termsID>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProforma.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProforma.discDate>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.ownerID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.lineCntr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.released>
  {
  }

  public abstract class corrected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.corrected>
  {
  }

  public abstract class enableProgressive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProforma.enableProgressive>
  {
  }

  public abstract class enableTransactional : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMProforma.enableTransactional>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProforma.extRefNbr>
  {
  }

  public abstract class curyTransactionalTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyTransactionalTotal>
  {
  }

  public abstract class transactionalTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.transactionalTotal>
  {
  }

  public abstract class curyProgressiveTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyProgressiveTotal>
  {
  }

  public abstract class progressiveTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.progressiveTotal>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.retainageTotal>
  {
  }

  public abstract class curyRetainageDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyRetainageDetailTotal>
  {
  }

  public abstract class retainageDetailTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.retainageDetailTotal>
  {
  }

  public abstract class curyRetainageTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyRetainageTaxTotal>
  {
  }

  public abstract class retainageTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.retainageTaxTotal>
  {
  }

  public abstract class curyRetainageTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyRetainageTaxInclTotal>
  {
  }

  public abstract class retainageTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.retainageTaxInclTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.taxTotal>
  {
  }

  public abstract class curyTaxInclTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyTaxInclTotal>
  {
  }

  public abstract class taxInclTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.taxInclTotal>
  {
  }

  public abstract class curyTaxTotalWithRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyTaxTotalWithRetainage>
  {
  }

  public abstract class taxTotalWithRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.taxTotalWithRetainage>
  {
  }

  public abstract class curyDocTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.curyDocTotal>
  {
  }

  public abstract class docTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.docTotal>
  {
  }

  public abstract class curyAmountDue : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.curyAmountDue>
  {
  }

  public abstract class amountDue : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.amountDue>
  {
  }

  /// <exclude />
  public abstract class curyAllocatedRetainedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.curyAllocatedRetainedTotal>
  {
  }

  /// <exclude />
  public abstract class allocatedRetainedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProforma.allocatedRetainedTotal>
  {
  }

  /// <exclude />
  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMProforma.retainagePct>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.isTaxValid>
  {
  }

  public abstract class isAIAOutdated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.isAIAOutdated>
  {
  }

  public abstract class aRInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.aRInvoiceDocType>
  {
  }

  public abstract class aRInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.aRInvoiceRefNbr>
  {
  }

  public abstract class aRInvoiceRefStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.aRInvoiceRefStatus>
  {
  }

  public abstract class aRInvoiceRefName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.aRInvoiceRefName>
  {
  }

  public abstract class reversedARInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.reversedARInvoiceDocType>
  {
  }

  public abstract class reversedARInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.reversedARInvoiceRefNbr>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProforma.isMigratedRecord>
  {
  }

  public abstract class numberOfLines : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProforma.numberOfLines>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProforma.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProforma.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProforma.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProforma.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProforma.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProforma.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProforma.lastModifiedDateTime>
  {
  }
}
