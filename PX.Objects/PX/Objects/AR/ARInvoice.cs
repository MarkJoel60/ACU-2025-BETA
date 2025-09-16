// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.SO.Interfaces;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents the accounts receivable invoices, credit and debit memos, overdue charges and credit write-offs
/// as well as the invoices created in the Sales Orders module (see <see cref="T:PX.Objects.SO.SOInvoice" />).
/// The records of this type are created and edited on the Invoices and Memos (AR301000) form
/// (which corresponds to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph).
/// The SO invoices are created and edited through the Invoices (SO303000) form
/// (which corresponds to the <see cref="T:PX.Objects.SO.SOInvoiceEntry" /> graph).
/// </summary>
[PXTable]
[PXSubstitute(GraphType = typeof (ARInvoiceEntry))]
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (SOInvoiceEntry), typeof (ARCashSaleEntry), typeof (ARInvoiceEntry)}, new System.Type[] {typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Current<ARInvoice.refNbr>>, And<ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<ARInvoice.released, Equal<False>>>>>>), typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARRegister.docType>>, And<ARCashSale.refNbr, Equal<Current<ARRegister.refNbr>>>>>), typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Current<ARInvoice.refNbr>>>>>)})]
[PXCacheName("AR Invoice/Memo")]
[PXGroupMask(typeof (InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>, And<Match<Customer, Current<AccessInfo.userName>>>>>))]
[DebuggerDisplay("DocType = {DocType}, RefNbr = {RefNbr}")]
[Serializable]
public class ARInvoice : ARRegister, IInvoice, PX.Objects.CM.IRegister, IDocumentKey, ICreatePaymentDocument
{
  protected int? _BillAddressID;
  protected DateTime? _DiscDate;
  protected 
  #nullable disable
  string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected string _TaxZoneID;
  protected string _AvalaraCustomerUsageType;
  protected string _MasterRefNbr;
  protected short? _InstallmentCntr;
  protected short? _InstallmentNbr;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected string _DrCr;
  protected Decimal? _CuryFreightCost;
  protected Decimal? _FreightCost;
  protected Decimal? _CuryGoodsTotal;
  protected Decimal? _GoodsTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryMiscTot;
  protected Decimal? _MiscTot;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryFreightTot;
  protected Decimal? _FreightTot;
  protected Decimal? _CuryFreightAmt;
  protected Decimal? _FreightAmt;
  protected Decimal? _CuryPremiumFreightAmt;
  protected Decimal? _PremiumFreightAmt;
  protected Decimal? _CuryBalanceWOTotal;
  protected Decimal? _BalanceWOTotal;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected bool? _ApplyOverdueCharge;
  protected DateTime? _LastFinChargeDate;
  protected DateTime? _LastPaymentDate;
  protected Decimal? _CuryCommnblAmt;
  protected Decimal? _CommnblAmt;
  protected bool? _CreditHold;
  protected bool? _ApprovedCredit;
  protected Decimal? _ApprovedCreditAmt;
  protected int? _ProjectID;
  protected string _PaymentMethodID;
  protected int? _PMInstanceID;
  protected int? _CashAccountID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected bool? _Hidden = new bool?(false);
  protected string _HiddenOrderType;
  protected string _HiddenOrderNbr;
  protected bool? _HiddenByShipment = new bool?(false);
  protected bool? _DisableAutomaticDiscountCalculation;

  /// <summary>
  /// The type of the document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARInvoiceType.ListAttribute" />.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARInvoiceType.List]
  [PXUIField]
  [PXFieldDescription]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  /// <summary>
  /// The reference number of the document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// For most document types, the reference number is generated automatically from the corresponding
  /// <see cref="T:PX.Objects.CS.Numbering">numbering sequence</see>, which is specified in the <see cref="T:PX.Objects.AR.ARSetup">Accounts Receivable module preferences</see>.
  /// </value>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARInvoiceType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Optional<ARInvoice.docType>>, And2<Where<ARRegisterAlias.origModule, Equal<BatchModule.moduleAR>, Or<ARRegisterAlias.origModule, Equal<BatchModule.moduleEP>, Or<ARRegisterAlias.released, Equal<True>>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  [ARInvoiceType.Numbering]
  [ARInvoiceNbr]
  [PXReferentialIntegrityCheck]
  [PXFieldDescription]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> record associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [CustomerActive]
  [PXDefault]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARInvoice.branchID>, IsPending>, Null, Case<Where<ARInvoice.customerLocationID, IsNotNull, And<Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<ARInvoice.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<ARInvoice.branchID>, IsNotNull>, Current2<ARInvoice.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARAddress">Billing Address object</see>, associated with the customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARAddress.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [ARAddress(typeof (Select2<Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<Customer.defBillAddressID>>>, LeftJoin<ARAddress, On<ARAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<ARAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<ARAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<ARAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARContact">Billing Contact object</see>, associated with the customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARContact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (ARContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Billing Contact", Visible = false)]
  [ARContact(typeof (Select2<Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>, LeftJoin<ARContact, On<ARContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<ARContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<ARContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<ARContact.isDefaultContact, Equal<True>>>>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>))]
  public virtual int? BillContactID { get; set; }

  /// <summary>
  /// The flag indicating that there are multiple shipments or orders with different addresses included in the invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Multiple Ship-To Addresses", Enabled = false)]
  public virtual bool? MultiShipAddress { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARAddress">Shipping Address object</see>, associated with the customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARAddress.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [ARShippingAddress(typeof (Select2<Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<ARInvoice.customerLocationID>>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<ARShippingAddress, On<ARShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<ARShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<ARShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<ARShippingAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>))]
  public virtual int? ShipAddressID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARContact">Shipping Contact object</see>, associated with the customer.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARContact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (ARShippingContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Shipping Contact", Visible = false)]
  [ARShippingContact(typeof (Select2<Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<ARInvoice.customerLocationID>>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<ARShippingContact, On<ARShippingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<ARShippingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<ARShippingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<ARShippingContact.isDefaultContact, Equal<True>>>>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>))]
  public virtual int? ShipContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Terms">Credit Terms</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AR.Customer.TermsID">credit terms</see> that are selected for the <see cref="P:PX.Objects.AR.ARInvoice.CustomerID">customer</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CS.Terms.TermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<Customer.termsID, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>, And2<Where<Current<ARInvoice.docType>, NotEqual<ARDocType.creditMemo>>, Or<Where<Current<ARSetup.termsInCreditMemos>, Equal<True>>>>>>))]
  [PXUIField]
  [ARTermsSelector]
  [Terms(typeof (ARInvoice.docDate), typeof (ARInvoice.dueDate), typeof (ARInvoice.discDate), typeof (ARInvoice.curyOrigDocAmt), typeof (ARInvoice.curyOrigDiscAmt), typeof (ARInvoice.curyTaxTotal), typeof (ARInvoice.branchID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<ARInvoice.termsID>.IsRelatedTo<PX.Objects.CS.Terms.termsID>))]
  public virtual string TermsID { get; set; }

  /// <summary>The due date of the document.</summary>
  [PXDBDate]
  [PXUIField]
  public override DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the <see cref="P:PX.Objects.AR.ARInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

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

  /// <summary>
  /// The original date assigned by the customer to the customer document.
  /// </summary>
  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Customer Ref. Date")]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxZone" /> associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<ARInvoice.customerLocationID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARInvoice.customerLocationID>>>>>))]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>
  /// The customer entity type for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" />.
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.CAvalaraCustomerUsageType">customer entity type</see>
  /// that is specified for the <see cref="T:PX.Objects.AR.ARInvoice.customerLocationID">location of the customer</see>.
  /// </value>
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARInvoice.customerLocationID>>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
  }

  /// <summary>
  /// For the document representing one of several installments this field stores the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" />
  /// of the master document - the one, to which the installment belongs.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string MasterRefNbr
  {
    get => this._MasterRefNbr;
    set => this._MasterRefNbr = value;
  }

  /// <summary>
  /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
  /// </summary>
  [PXDBShort]
  public virtual short? InstallmentCntr
  {
    get => this._InstallmentCntr;
    set => this._InstallmentCntr = value;
  }

  /// <summary>
  /// For the document representing one of several installments this field stores the number of the installment.
  /// </summary>
  [PXDBShort]
  public virtual short? InstallmentNbr
  {
    get => this._InstallmentNbr;
    set => this._InstallmentNbr = value;
  }

  /// <summary>
  /// The portion of the document total that is exempt from VAT.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// This field is relevant only if the <see cref="!:FeaturesSet.VatReporting">VAT Reporting</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The value of this field is calculated as the taxable amount for the tax with <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> set to <c>true</c>.
  /// </value>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  /// <summary>
  /// The portion of the document total that is exempt from VAT.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// This field is relevant only if the <see cref="!:FeaturesSet.VatReporting">VAT Reporting</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The value of this field is calculated as the taxable amount for the tax with <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> set to <c>true</c>.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  /// <summary>
  /// The portion of the document total that is subjected to VAT.
  /// Given in the <see cref="!:CuryID">currency</see> of the document.
  /// This field is relevant only if the <see cref="!:FeaturesSet.VatReporting">VAT Reporting</see> feature is enabled.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  /// <summary>
  /// The portion of the document total that is subjected to VAT.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// This field is relevant only if the <see cref="!:FeaturesSet.VatReporting">VAT Reporting</see> feature is enabled.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? ExternalTaxesImportInProgress { get; set; }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.initDocBal))]
  [PXUIField]
  [PXUIVerify]
  public override Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// Read-only field indicating whether the document is of debit or credit type.
  /// </summary>
  /// <value>
  /// Possible values are <see cref="F:PX.Objects.GL.DrCr.Credit" /> (for Invoice, Debit Memo, Financial Charge, Small Credit Write-Off and Cash Sale)
  /// and <see cref="F:PX.Objects.GL.DrCr.Debit" /> (for Credit Memo and Cash Return).
  /// </value>
  [PXString(1, IsFixed = true)]
  public virtual string DrCr
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARInvoice.docType), typeof (ARInvoice.pendingPayment)})] get
    {
      return this.DocType == "PPI" && !this.PendingPayment.GetValueOrDefault() ? "D" : ARInvoiceType.DrCr(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Freight cost of the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.freightCost))]
  [PXUIField(DisplayName = "Freight Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightCost
  {
    get => this._CuryFreightCost;
    set => this._CuryFreightCost = value;
  }

  /// <summary>
  /// Freight cost of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightCost
  {
    get => this._FreightCost;
    set => this._FreightCost = value;
  }

  /// <summary>
  /// The total goods amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="T:PX.Objects.AR.ARInvoice.curyID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.goodsTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods Total", Enabled = false)]
  public virtual Decimal? CuryGoodsTotal
  {
    get => this._CuryGoodsTotal;
    set => this._CuryGoodsTotal = value;
  }

  /// <summary>
  /// The total goods amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GoodsTotal
  {
    get => this._GoodsTotal;
    set => this._GoodsTotal = value;
  }

  /// <summary>
  /// The total amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="T:PX.Objects.AR.ARInvoice.curyID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.lineTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  /// <summary>
  /// The total amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.ARInvoice.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.lineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the invoice.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.ARInvoice.groupDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of group discounts of the invoice.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.groupDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? CuryGroupDiscTotal { get; set; }

  /// <summary>
  /// The total group discount of the document, which is calculated as the sum of group discounts of the invoice.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? GroupDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.ARInvoice.documentDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of document discounts of the invoice.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.documentDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? CuryDocumentDiscTotal { get; set; }

  /// <summary>
  /// The total document discount of the document, which is calculated as the sum of document discounts of the invoice.
  /// </summary>
  /// <remarks>
  /// <para>If the <see cref="T:PX.Objects.CS.FeaturesSet.customerDiscounts">Customer Discounts</see> feature is not enabled on
  /// the Enable/Disable Features (CS100000) form,
  /// a user can enter a document-level discount manually. This manual discount has no discount code or
  /// sequence and is not recalculated by the system. If the manual discount needs to be changed, a user has to
  /// correct it manually.</para>
  /// </remarks>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? DocumentDiscTotal { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARInvoiceDiscountDetail.CuryDiscountAmt">group and document discount total</see> for the document.
  /// Given in the <see cref="T:PX.Objects.AR.ARInvoice.curyID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group and Document Discount Total", Enabled = false)]
  public virtual Decimal? CuryDiscTot { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARInvoiceDiscountDetail.DiscountAmt">group and document discount total</see> for the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.ARInvoice.orderDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document and line discounts of the invoice.
  /// </summary>
  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.orderDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<ARInvoice.curyDiscTot, ARInvoice.curyLineDiscTotal>), typeof (Decimal))]
  [PXFormula(typeof (Add<ARInvoice.curyDiscTot, ARInvoice.curyLineDiscTotal>))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <summary>
  /// The total discount of the document, which is calculated as the sum of group, document and line discounts of the invoice.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<ARInvoice.discTot, ARInvoice.lineDiscTotal>), typeof (Decimal))]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? OrderDiscTotal { get; set; }

  /// <summary>
  /// The total misc amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.miscTot))]
  [PXUIField(DisplayName = "Misc. Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscTot
  {
    get => this._CuryMiscTot;
    set => this._CuryMiscTot = value;
  }

  /// <summary>
  /// The total misc amount of the <see cref="T:PX.Objects.AR.ARTran">lines</see> of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? MiscTot
  {
    get => this._MiscTot;
    set => this._MiscTot = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.ARInvoice.goodsExtPriceTotal">total amount</see> on all lines of the document, except for Misc. Charges, before Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.goodsExtPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods")]
  public virtual Decimal? CuryGoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Misc. Charges, before Line-level discounts are applied.
  /// </summary>
  /// <remarks>
  /// <para>This total is calculated as the sum of the amounts in the
  /// <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see> for all stock items and non-stock items that require shipment.
  /// This total does not include the freight amount.</para>
  /// <para>This field is not available for transfer orders.
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.ARInvoice.miscTot">total amount</see> calculated as the sum of the amounts in
  /// <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see> of the order non-stock items
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.miscExtPriceTotal))]
  [PXUIField(DisplayName = "Misc. Charges", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount calculated as the sum of the amounts in <see cref="!:SOLine.curyExtPrice">Ext. Price</see>
  /// of the order non-stock items.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? MiscExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.ARInvoice.detailExtPriceTotal">sum</see> of the
  /// <see cref="T:PX.Objects.AR.ARInvoice.curyGoodsExtPriceTotal">goods</see> and
  /// the <see cref="T:PX.Objects.AR.ARInvoice.curyMiscExtPriceTotal">misc. charges amount</see> values.
  /// </summary>
  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.detailExtPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Switch<Case<Where<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>, Add<ARInvoice.curyLineTotal, ARInvoice.curyLineDiscTotal>>, Add<ARInvoice.curyGoodsExtPriceTotal, ARInvoice.curyMiscExtPriceTotal>>), typeof (Decimal))]
  [PXFormula(typeof (Switch<Case<Where<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>, Add<ARInvoice.curyLineTotal, ARInvoice.curyLineDiscTotal>>, Add<ARInvoice.curyGoodsExtPriceTotal, ARInvoice.curyMiscExtPriceTotal>>))]
  [PXUIField(DisplayName = "Detail Total")]
  public virtual Decimal? CuryDetailExtPriceTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="T:PX.Objects.AR.ARInvoice.goodsExtPriceTotal">goods</see> and
  /// the <see cref="T:PX.Objects.AR.ARInvoice.miscExtPriceTotal">misc. charges amount</see> values.
  /// </summary>
  [PXDecimal(4)]
  [PXDBCalced(typeof (Switch<Case<Where<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>, Add<ARInvoice.lineTotal, ARInvoice.lineDiscTotal>>, Add<ARInvoice.goodsExtPriceTotal, ARInvoice.miscExtPriceTotal>>), typeof (Decimal))]
  public virtual Decimal? DetailExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount of tax associated with the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  /// <summary>
  /// The total amount of tax associated with the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  /// <summary>
  /// The total amount of freight associated with the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.freightTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Freight Total", Enabled = false)]
  public virtual Decimal? CuryFreightTot
  {
    get => this._CuryFreightTot;
    set => this._CuryFreightTot = value;
  }

  /// <summary>
  /// The total amount of freight associated with the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightTot
  {
    get => this._FreightTot;
    set => this._FreightTot = value;
  }

  /// <summary>
  /// The amount of freight associated with the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.freightAmt))]
  [PXUIField(DisplayName = "Freight Price", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightAmt
  {
    get => this._CuryFreightAmt;
    set => this._CuryFreightAmt = value;
  }

  /// <summary>
  /// The amount of freight associated with the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightAmt
  {
    get => this._FreightAmt;
    set => this._FreightAmt = value;
  }

  /// <summary>
  /// The amount of premium freight associated with the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.premiumFreightAmt))]
  [PXUIField(DisplayName = "Premium Freight Price", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPremiumFreightAmt
  {
    get => this._CuryPremiumFreightAmt;
    set => this._CuryPremiumFreightAmt = value;
  }

  /// <summary>
  /// The amount of premium freight associated with the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? PremiumFreightAmt
  {
    get => this._PremiumFreightAmt;
    set => this._PremiumFreightAmt = value;
  }

  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.paymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Paid", Enabled = false)]
  public virtual Decimal? CuryPaymentTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Paid", Enabled = false)]
  public virtual Decimal? PaymentTotal { get; set; }

  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.balanceWOTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Write-Off Total", Enabled = false)]
  public virtual Decimal? CuryBalanceWOTotal
  {
    get => this._CuryBalanceWOTotal;
    set => this._CuryBalanceWOTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BalanceWOTotal
  {
    get => this._BalanceWOTotal;
    set => this._BalanceWOTotal = value;
  }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.unreleasedPaymentAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Not Released", Enabled = false)]
  public virtual Decimal? CuryUnreleasedPaymentAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnreleasedPaymentAmt { get; set; }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.cCAuthorizedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Authorized", Enabled = false)]
  public virtual Decimal? CuryCCAuthorizedAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CCAuthorizedAmt { get; set; }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.paidAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual Decimal? CuryPaidAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaidAmt { get; set; }

  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.unpaidBalance))]
  [PXFormula(typeof (Sub<ARInvoice.curyOrigDocAmt, Add<ARInvoice.curyPaymentTotal, Add<ARInvoice.curyDiscAppliedAmt, ARInvoice.curyBalanceWOTotal>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unpaid Balance", Enabled = false)]
  public Decimal? CuryUnpaidBalance { get; set; }

  [PXDBBaseCury(typeof (ARInvoice.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnpaidBalance { get; set; }

  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.discAppliedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken", Enabled = false)]
  public virtual Decimal? CuryDiscAppliedAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAppliedAmt { get; set; }

  /// <summary>The commission percent used for the salesperson.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission %", Enabled = false)]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  /// <summary>
  /// The commission amount calculated on this document for the salesperson.
  /// Given in the <see cref="!:CuryID">currency</see> of the document.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.commnAmt))]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  /// <summary>
  /// The commission amount calculated on this document for the salesperson.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.applicationBalance), BaseCalc = false)]
  [PXUIField(DisplayName = "Application Balance")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<Sub<ARInvoice.curyPaymentTotal, ARRegister.curyOrigDocAmt>, ARInvoice.curyBalanceWOTotal>))]
  public virtual Decimal? CuryApplicationBalance { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<Sub<ARInvoice.paymentTotal, ARRegister.origDocAmt>, ARInvoice.balanceWOTotal>))]
  public virtual Decimal? ApplicationBalance { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document can be available
  /// on the Calculate Overdue Charges (AR507000) processing form.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? ApplyOverdueCharge
  {
    get => this._ApplyOverdueCharge;
    set => this._ApplyOverdueCharge = value;
  }

  /// <summary>
  /// The date of the most recent <see cref="T:PX.Objects.AR.ARFinChargeTran">Financial Charge</see> associated with this document.
  /// </summary>
  [PXDate]
  [PXUIField]
  public virtual DateTime? LastFinChargeDate
  {
    get => this._LastFinChargeDate;
    set => this._LastFinChargeDate = value;
  }

  /// <summary>
  /// The date of the most recent payment associated with this document.
  /// </summary>
  [PXDate]
  [PXUIField(DisplayName = "Last Payment Date")]
  public virtual DateTime? LastPaymentDate
  {
    get => this._LastPaymentDate;
    set => this._LastPaymentDate = value;
  }

  /// <summary>
  /// The amount used as the base to calculate commission for this document.
  /// Given in the <see cref="!:CuryID">currency</see> of the document.
  /// </summary>
  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Commissionable", Enabled = false)]
  public virtual Decimal? CuryCommnblAmt
  {
    get => this._CuryCommnblAmt;
    set => this._CuryCommnblAmt = value;
  }

  /// <summary>
  /// The amount used as the base to calculate commission for this document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  /// <summary>
  /// The balance of tax withheld on the document.
  /// Given in the <see cref="!:CuryID">currency</see> of the document.
  /// </summary>
  [PXDecimal(4)]
  [PXFormula(typeof (decimal0))]
  public virtual Decimal? CuryWhTaxBal { get; set; }

  /// <summary>
  /// The balance of tax withheld on the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency</see> of the company.
  /// </summary>
  [PXDecimal(4)]
  [PXFormula(typeof (decimal0))]
  public virtual Decimal? WhTaxBal { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is on credit hold,
  /// which means that the credit check failed for the <see cref="P:PX.Objects.AR.ARInvoice.CustomerID">Customer</see>.
  /// The document can't be released while it's on credit hold.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Credit Hold")]
  public virtual bool? CreditHold
  {
    get => this._CreditHold;
    set => this._CreditHold = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that credit has been approved for the document.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedCredit
  {
    get => this._ApprovedCredit;
    set => this._ApprovedCredit = value;
  }

  /// <summary>The amount of credit approved for the document.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ApprovedCreditAmt
  {
    get => this._ApprovedCreditAmt;
    set => this._ApprovedCreditAmt = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that invoice has been approved with CC payment that failed on attempt to capture.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedCaptureFailed { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that invoice has been approved not fully prepaid.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedPrepaymentRequired { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the document
  /// or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see>, which indicates that the document is not related to any particular project.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ProjectID" /> field.
  /// </value>
  [NonProjectBase]
  [ProjectDefault("AR")]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see> that is used for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Search2<CustomerPaymentMethod.paymentMethodID, InnerJoin<Customer, On<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>, And<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>, And<CustomerPaymentMethod.isActive, Equal<True>>>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>, Search2<Customer.defPaymentMethodID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Customer.defPaymentMethodID>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>>, Where<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>>>))]
  [PXSelector(typeof (Search5<PX.Objects.CA.PaymentMethod.paymentMethodID, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>, And<CustomerPaymentMethod.bAccountID, Equal<Current<ARInvoice.customerID>>>>>, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<Where<PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer, Equal<True>, Or<Where<CustomerPaymentMethod.pMInstanceID, IsNotNull>>>>>>, Aggregate<GroupBy<PX.Objects.CA.PaymentMethod.paymentMethodID>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXForeignReference(typeof (Field<ARInvoice.paymentMethodID>.IsRelatedTo<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment method</see> (card or account number) associated with the document.
  /// </summary>
  /// <value>
  /// Defaults according to the settings of the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">customer payment methods</see>
  /// that are specified for the <see cref="P:PX.Objects.AR.ARInvoice.CustomerID">customer</see> associated with the document.
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.PMInstanceID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<Customer.defPMInstanceID, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>, And<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>>>>, Where<Customer.bAccountID, Equal<Current2<ARInvoice.customerID>>, And<CustomerPaymentMethod.isActive, Equal<True>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARInvoice.paymentMethodID>>>>>>, Search2<CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<ARInvoice.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<CustomerPaymentMethod.bAccountID, Equal<Current2<ARInvoice.customerID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARInvoice.paymentMethodID>>, And<CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<CustomerPaymentMethod.expirationDate, Desc<CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<CustomerPaymentMethod.pMInstanceID, Where<CustomerPaymentMethod.bAccountID, Equal<Current2<ARInvoice.customerID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARInvoice.paymentMethodID>>>>>), DescriptionField = typeof (CustomerPaymentMethod.descr))]
  [PXRestrictor(typeof (Where<CustomerPaymentMethod.isActive, Equal<boolTrue>, Or<CustomerPaymentMethod.pMInstanceID, Equal<Current<ARInvoice.pMInstanceID>>>>), "The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new System.Type[] {typeof (CustomerPaymentMethod.descr)})]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  [PXForeignReference(typeof (CompositeKey<Field<ARInvoice.customerID>.IsRelatedTo<CustomerPaymentMethod.bAccountID>, Field<ARInvoice.pMInstanceID>.IsRelatedTo<CustomerPaymentMethod.pMInstanceID>>))]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.CashAccount">Cash Account</see> associated with the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AR.CustomerPaymentMethod.CashAccountID">Cash Account</see> selected for the <see cref="P:PX.Objects.AR.ARInvoice.PMInstanceID">Customer Payment Method</see>,
  /// or (if the above is unavailable) to the Cash Account selected as the default one for Accounts Receivable in the settings of the
  /// <see cref="P:PX.Objects.AR.ARInvoice.PaymentMethodID">Payment Method</see> (see the <see cref="P:PX.Objects.CA.PaymentMethodAccount.ARIsDefault" /> field).
  /// </value>
  [PXDefault(typeof (Coalesce<Search2<CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<CustomerPaymentMethod.bAccountID, Equal<Current<ARInvoice.customerID>>, And<CustomerPaymentMethod.pMInstanceID, Equal<Current2<ARInvoice.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<ARInvoice.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<ARInvoice.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (ARInvoice.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<ARInvoice.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
  [PXForeignReference(typeof (Field<ARInvoice.cashAccountID>.IsRelatedTo<PX.Objects.CA.CashAccount.cashAccountID>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [ARDocStatus.List]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  [PXForeignReference(typeof (Field<ARInvoice.workgroupID>.IsRelatedTo<EPCompanyTree.workGroupID>))]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">Contact</see> responsible for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDefault(typeof (PX.Objects.CR.BAccount.ownerID))]
  [Owner(typeof (ARInvoice.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.CustSalesPeople">salesperson</see> to whom the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustSalesPeople.SalesPersonID" /> field.
  /// </value>
  [SalesPerson(DisplayName = "Default Salesperson")]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<ARRegister.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<ARRegister.customerLocationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  public override int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Note">Note</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(258, "{0} {1}: {2} - {4}", new System.Type[] {typeof (ARInvoice.origModule), typeof (ARInvoice.docType), typeof (ARInvoice.refNbr), typeof (ARInvoice.customerID), typeof (Customer.acctName)}, new System.Type[] {typeof (ARInvoice.invoiceNbr), typeof (ARInvoice.docDesc)}, NumberFields = new System.Type[] {typeof (ARInvoice.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (ARInvoice.docDate), typeof (ARInvoice.status), typeof (ARInvoice.invoiceNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (ARInvoice.docDesc)}, WhereConstraint = typeof (Where<ARInvoice.origModule, Equal<BatchModule.moduleSO>, Or<Where<ARRegister.docType, NotEqual<ARDocType.cashSale>, And<ARRegister.docType, NotEqual<ARDocType.cashReturn>>>>>), MatchWithJoin = typeof (InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>>), SelectForFastIndexing = typeof (Select2<ARInvoice, InnerJoin<Customer, On<ARInvoice.customerID, Equal<Customer.bAccountID>>>, Where<ARInvoice.origModule, Equal<BatchModule.moduleSO>, Or<Where<ARRegister.docType, NotEqual<ARDocType.cashSale>, And<ARRegister.docType, NotEqual<ARDocType.cashReturn>>>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<ARInvoice.refNbr, InnerJoinSingleTable<ARRegister, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegister.customerID, Equal<Customer.bAccountID>>>>, Where2<Where<ARRegister.origModule, In3<BatchModule.moduleAR, BatchModule.moduleEP, BatchModule.moduleSO>, Or<ARRegister.released, Equal<True>>>, And<Match<Customer, Current<AccessInfo.userName>>>>, OrderBy<Desc<ARRegister.refNbr>>>))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Note">Note</see> object associated with the document reference.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXDBGuid(false)]
  public override Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document can be associated with only one sales order
  /// (which happens when <see cref="P:PX.Objects.SO.SOOrder.BillSeparately" /> is set to <c>true</c> for the sales order).
  /// </summary>
  [PXBool]
  public virtual bool? Hidden
  {
    get => this._Hidden;
    set => this._Hidden = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.SO.SOOrder.OrderType" /> type of the related sales order when the document can be associated
  /// with only one sales order (which happens when <see cref="P:PX.Objects.SO.SOOrder.BillSeparately" /> is set to <c>true</c> for the sales order).
  /// </summary>
  [PXString]
  public virtual string HiddenOrderType
  {
    get => this._HiddenOrderType;
    set => this._HiddenOrderType = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.SO.SOOrder.OrderNbr" /> reference number of the related sales order when the document can be associated
  /// with only one sales order (which happens when <see cref="P:PX.Objects.SO.SOOrder.BillSeparately" /> is set to <c>true</c> for the sales order).
  /// </summary>
  [PXString]
  public virtual string HiddenOrderNbr
  {
    get => this._HiddenOrderNbr;
    set => this._HiddenOrderNbr = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document can be associated with only one shipment.
  /// </summary>
  [PXBool]
  public virtual bool? HiddenByShipment
  {
    get => this._HiddenByShipment;
    set => this._HiddenByShipment = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.SO.SOShipment.ShipmentType" /> type of the related shipment when the document can be associated with only one shipment.
  /// </summary>
  [PXString]
  public virtual string HiddenShipmentType { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.SO.SOShipment.ShipmentNbr" /> reference number of the related shipment when the document can be associated with only one shipment.
  /// </summary>
  [PXString]
  public virtual string HiddenShipmentNbr { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the Avalara taxes should be included in the document balance calculation
  /// (because these taxes will be calculated only during the release process).
  /// </summary>
  [PXBool]
  public virtual bool? ApplyPaymentWhenTaxAvailable { get; set; }

  /// <summary>
  /// A Boolean field that indicates whether the payments and prepayment applied to the related sales orders
  /// should be transferred to the invoice during the document creation.
  /// When set to <see langword="false" />, the payments and prepayments will not be transferred to the invoice
  /// during the document creation but will be transferred within the <b>Complete Processing</b> actions
  /// execution when all orders are already added to the invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsPaymentsTransferred { get; set; }

  /// <summary>
  /// If true a corresponding proforma document exists thus making this document (or part of it) read-only.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pro Forma Invoice Exists")]
  public virtual bool? ProformaExists { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document has been revoked.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Revoked", Enabled = true, Visible = false)]
  public virtual bool? Revoked { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public override bool? PaymentsByLinesAllowed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Defer Price/Discount Recalculation")]
  public virtual bool? DeferPriceDiscountRecalculation { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Prices and discounts are up to date.", Enabled = false)]
  public virtual bool? IsPriceAndDiscountsValid { get; set; } = new bool?(true);

  /// <summary>The type of the correction document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARInvoice.DocType" /> field.
  /// </value>
  [PXString(3, IsFixed = true)]
  [ARInvoiceType.List]
  public virtual string CorrectionDocType { get; set; }

  /// <summary>The reference number of the correction document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARInvoice.RefNbr" /> field.
  /// </value>
  [PXString(15, IsUnicode = true)]
  public virtual string CorrectionRefNbr { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that Cancel action was applied to the invoice.
  /// </summary>
  [PXBool]
  public virtual bool? IsUnderCancellation { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PendingProcessingCntr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<ARInvoice.pendingProcessingCntr, Greater<int0>, And<ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<ARInvoice.docType, In3<ARDocType.invoice, ARDocType.debitMemo>, And<ARInvoice.released, Equal<False>>>>>, True>, False>))]
  public override bool? PendingProcessing
  {
    get => base.PendingProcessing;
    set => base.PendingProcessing = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? CaptureFailedCntr { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing", FieldClass = "InterBranch")]
  [PXDefault(false)]
  public virtual bool? IsHiddenInIntercompanySales { get; set; }

  /// <summary>
  /// If <c>true</c>, available applications are not loaded automatically during an invoice creation
  /// that is executed in an import scenario or an API call. If the value is <c>false</c>,
  /// the automatic loading is on. The default value is <c>true</c>.
  /// </summary>
  [PXBool]
  public virtual bool? IsLoadApplications { get; set; }

  /// <summary>
  /// The counter of the applied payments in pre-authorized status.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AuthorizedPaymentCntr { get; set; }

  public new class Events : PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>
  {
    public PXEntityEvent<ARInvoice> ReleaseDocument;
    public PXEntityEvent<ARInvoice> OpenDocument;
    public PXEntityEvent<ARInvoice> CloseDocument;
    public PXEntityEvent<ARInvoice> VoidDocument;
    public PXEntityEvent<ARInvoice> CancelDocument;
    public PXEntityEvent<ARInvoice, PX.Objects.CR.CRQuote> ARInvoiceCreatedFromQuote;
    public PXEntityEvent<ARInvoice> ARInvoiceDeleted;
    public PXEntityEvent<ARInvoice> ProcessingCompleted;
  }

  public new class PK : PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>
  {
    public static ARInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARInvoice) PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.customerID, ARInvoice.customerLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.curyID>
    {
    }

    public class ARAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.aRAccountID>
    {
    }

    public class ARSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.aRSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.scheduleID>
    {
    }

    public class BillAddress : 
      PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.billAddressID>
    {
    }

    public class BillContact : 
      PrimaryKeyOf<ARContact>.By<ARContact.contactID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.billContactID>
    {
    }

    public class ShipAddress : 
      PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.shipAddressID>
    {
    }

    public class ShipContact : 
      PrimaryKeyOf<ARContact>.By<ARContact.contactID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.shipContactID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.termsID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.taxZoneID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.paymentMethodID>
    {
    }
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.selected>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.refNbr>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.tranPeriodID>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.origModule>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.customerID>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.customerID_Customer_acctName>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARInvoice.customerLocationID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.branchID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.curyID>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.billContactID>
  {
  }

  public abstract class multiShipAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.multiShipAddress>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.shipContactID>
  {
  }

  public new abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.aRAccountID>
  {
  }

  public new abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.aRSubID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.termsID>
  {
  }

  public new abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.discDate>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.taxZoneID>
  {
  }

  public new abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.taxCalcMode>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.avalaraCustomerUsageType>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARInvoice.docDate>
  {
  }

  public abstract class masterRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.masterRefNbr>
  {
  }

  public abstract class installmentCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARInvoice.installmentCntr>
  {
  }

  public abstract class installmentNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARInvoice.installmentNbr>
  {
  }

  public new abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.origDocType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.origRefNbr>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.vatTaxableTotal>
  {
  }

  /// <summary>
  /// Indicates that taxes were calculated by an external system
  /// </summary>
  public abstract class externalTaxesImportInProgress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.externalTaxesImportInProgress>
  {
  }

  public new abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.disableAutomaticTaxCalculation>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARInvoice.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyOrigDocAmt>
  {
  }

  public new abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyRetainageTotal>
  {
  }

  public new abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyRetainageUnreleasedAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.origDocAmt>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.docBal>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyInitDocBal>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.discBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.origDiscAmt>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.drCr>
  {
  }

  public abstract class curyFreightCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyFreightCost>
  {
  }

  public abstract class freightCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.freightCost>
  {
  }

  public abstract class curyGoodsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyGoodsTotal>
  {
  }

  public abstract class goodsTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.goodsTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.lineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryLineDiscTotal" />
  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyLineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.LineDiscTotal" />
  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.lineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryGroupDiscTotal" />
  public abstract class curyGroupDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyGroupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.GroupDiscTotal" />
  public abstract class groupDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.groupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryDocumentDiscTotal" />
  public abstract class curyDocumentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyDocumentDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.DocumentDiscTotal" />
  public abstract class documentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.documentDiscTotal>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDiscTot>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.discTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.OrderDiscTotal" />
  public abstract class orderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.orderDiscTotal>
  {
  }

  public abstract class curyMiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyMiscTot>
  {
  }

  public abstract class miscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.miscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryGoodsExtPriceTotal" />
  public abstract class curyGoodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyGoodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryGoodsExtPriceTotal" />
  public abstract class goodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.goodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryMiscExtPriceTotal" />
  public abstract class curyMiscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyMiscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.MiscExtPriceTotal" />
  public abstract class miscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.miscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryDetailExtPriceTotal" />
  public abstract class curyDetailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyDetailExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.DetailExtPriceTotal" />
  public abstract class detailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.detailExtPriceTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.taxTotal>
  {
  }

  public abstract class curyFreightTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyFreightTot>
  {
  }

  public abstract class freightTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.freightTot>
  {
  }

  public abstract class curyFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyFreightAmt>
  {
  }

  public abstract class freightAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.freightAmt>
  {
  }

  public abstract class curyPremiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyPremiumFreightAmt>
  {
  }

  public abstract class premiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.premiumFreightAmt>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyDocDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.docDisc>
  {
  }

  public abstract class curyPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyPaymentTotal>
  {
  }

  public abstract class paymentTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.paymentTotal>
  {
  }

  public abstract class curyBalanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyBalanceWOTotal>
  {
  }

  public abstract class balanceWOTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.balanceWOTotal>
  {
  }

  public abstract class curyUnreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyUnreleasedPaymentAmt>
  {
  }

  public abstract class unreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.unreleasedPaymentAmt>
  {
  }

  public abstract class curyCCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyCCAuthorizedAmt>
  {
  }

  public abstract class cCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.cCAuthorizedAmt>
  {
  }

  public abstract class curyPaidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyPaidAmt>
  {
  }

  public abstract class paidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.paidAmt>
  {
  }

  public abstract class curyUnpaidBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyUnpaidBalance>
  {
  }

  public abstract class unpaidBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.unpaidBalance>
  {
  }

  public abstract class curyDiscAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyDiscAppliedAmt>
  {
  }

  public abstract class discAppliedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.discAppliedAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.hold>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.batchNbr>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnPct>
  {
  }

  public abstract class curyCommnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnAmt>
  {
  }

  public abstract class curyApplicationBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyApplicationBalance>
  {
  }

  public abstract class applicationBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.applicationBalance>
  {
  }

  public abstract class applyOverdueCharge : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.applyOverdueCharge>
  {
  }

  public abstract class lastFinChargeDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoice.lastFinChargeDate>
  {
  }

  public abstract class lastPaymentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoice.lastPaymentDate>
  {
  }

  public abstract class curyCommnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyCommnblAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.commnblAmt>
  {
  }

  public abstract class curyWhTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARInvoice.whTaxBal>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.scheduleID>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.scheduled>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARInvoice.createdByID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARInvoice.lastModifiedByID>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.voided>
  {
  }

  public abstract class creditHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.creditHold>
  {
  }

  public abstract class approvedCredit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.approvedCredit>
  {
  }

  public abstract class approvedCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.approvedCreditAmt>
  {
  }

  public abstract class approvedCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.approvedCaptureFailed>
  {
  }

  public abstract class approvedPrepaymentRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.approvedPrepaymentRequired>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.projectID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.pMInstanceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.cashAccountID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.status>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.docDesc>
  {
  }

  /// <summary>
  /// This attribute is intended for the status syncronization in the ARInvoice<br />
  /// Namely, it sets a corresponeded string to the Status field, depending <br />
  /// upon Voided, Released, CreditHold, Hold, Sheduled,Released, OpenDoc, PrintInvoice,EmailInvoice<br />
  /// of the ARInvoice<br />
  /// [SetStatus()]
  /// </summary>
  protected new class SetStatusAttribute : 
    PXEventSubscriberAttribute,
    IPXRowUpdatingSubscriber,
    IPXRowInsertingSubscriber
  {
    protected ARInvoice.SetStatusAttribute.Definition _Definition;

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this._Definition = PXDatabase.GetSlot<ARInvoice.SetStatusAttribute.Definition>(typeof (ARInvoice.SetStatusAttribute).FullName, new System.Type[1]
      {
        typeof (ARSetup)
      });
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARInvoice.hold>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0)));
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARInvoice.creditHold>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_1)));
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARInvoice.printed>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_2)));
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARInvoice.emailed>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_3)));
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARRegister.dontPrint>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_4)));
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler<ARInvoice.dontEmail>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_5)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.FieldVerifying.AddHandler<ARInvoice.status>(ARInvoice.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__2_6 ?? (ARInvoice.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__2_6 = new PXFieldVerifying((object) ARInvoice.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCacheAttached\u003Eb__2_6))));
      PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
      ARInvoice.SetStatusAttribute setStatusAttribute = this;
      // ISSUE: virtual method pointer
      PXRowSelecting pxRowSelecting = new PXRowSelecting((object) setStatusAttribute, __vmethodptr(setStatusAttribute, RowSelecting));
      rowSelecting.AddHandler<ARInvoice>(pxRowSelecting);
      // ISSUE: method pointer
      sender.Graph.RowSelected.AddHandler(sender.GetItemType(), new PXRowSelected((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_7)));
    }

    protected virtual void StatusSet(
      PXCache cache,
      ARInvoice item,
      bool? HoldVal,
      bool? CreditHoldVal,
      bool? toPrint,
      bool? toEmail)
    {
      if (item.Canceled.GetValueOrDefault())
        item.Status = "L";
      else if (item.Voided.GetValueOrDefault())
        item.Status = "V";
      else if (HoldVal.GetValueOrDefault() && item.OrigModule == "SO")
        item.Status = "H";
      else if (item.PendingProcessing.GetValueOrDefault())
        item.Status = "W";
      else if (CreditHoldVal.GetValueOrDefault() && (item.Approved.GetValueOrDefault() || item.DontApprove.GetValueOrDefault()))
        item.Status = "R";
      else if (HoldVal.GetValueOrDefault() && item.OrigModule != "SO")
        item.Status = "H";
      else if (item.Scheduled.GetValueOrDefault())
        item.Status = "S";
      else if (item.Rejected.GetValueOrDefault())
      {
        item.Status = "J";
      }
      else
      {
        bool? nullable = item.Released;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          if (item.DocType == "INV" || item.DocType == "CRM" || item.DocType == "DRM")
          {
            nullable = item.Approved;
            if (!nullable.GetValueOrDefault())
            {
              nullable = item.DontApprove;
              if (!nullable.GetValueOrDefault())
              {
                item.Status = "D";
                return;
              }
            }
            if (this._Definition != null && this._Definition._PrintBeforeRelease.GetValueOrDefault() && toPrint.GetValueOrDefault())
              item.Status = "P";
            else if (this._Definition != null && this._Definition._EmailBeforeRelease.GetValueOrDefault() && toEmail.GetValueOrDefault())
              item.Status = "E";
            else
              item.Status = "B";
          }
          else
            item.Status = "B";
        }
        else
        {
          nullable = item.OpenDoc;
          if (nullable.GetValueOrDefault())
          {
            item.Status = item.DocType == "PPI" ? "U" : "N";
          }
          else
          {
            nullable = item.OpenDoc;
            bool flag2 = false;
            if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
              return;
            item.Status = "C";
          }
        }
      }
    }

    public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      ARInvoice row = (ARInvoice) e.Row;
      if (row == null)
        return;
      PXCache cache = sender;
      ARInvoice arInvoice = row;
      bool? hold = row.Hold;
      bool? creditHold = row.CreditHold;
      bool? nullable = row.Printed;
      int num1;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.DontPrint;
        num1 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool? toPrint = new bool?(num1 != 0);
      nullable = row.Emailed;
      int num2;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.DontEmail;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      bool? toEmail = new bool?(num2 != 0);
      this.StatusSet(cache, arInvoice, hold, creditHold, toPrint, toEmail);
    }

    public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
    {
      ARInvoice row = (ARInvoice) e.Row;
      PXCache cache = sender;
      ARInvoice arInvoice = row;
      bool? hold = row.Hold;
      bool? creditHold = row.CreditHold;
      bool? nullable;
      int num1;
      if (!row.Printed.GetValueOrDefault())
      {
        nullable = row.DontPrint;
        num1 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool? toPrint = new bool?(num1 != 0);
      nullable = row.Emailed;
      int num2;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.DontEmail;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      bool? toEmail = new bool?(num2 != 0);
      this.StatusSet(cache, arInvoice, hold, creditHold, toPrint, toEmail);
    }

    public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      ARInvoice newRow = (ARInvoice) e.NewRow;
      PXCache cache = sender;
      ARInvoice arInvoice = newRow;
      bool? hold = newRow.Hold;
      bool? creditHold = newRow.CreditHold;
      bool? nullable;
      int num1;
      if (!newRow.Printed.GetValueOrDefault())
      {
        nullable = newRow.DontPrint;
        num1 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool? toPrint = new bool?(num1 != 0);
      nullable = newRow.Emailed;
      int num2;
      if (!nullable.GetValueOrDefault())
      {
        nullable = newRow.DontEmail;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      bool? toEmail = new bool?(num2 != 0);
      this.StatusSet(cache, arInvoice, hold, creditHold, toPrint, toEmail);
    }

    protected class Definition : IPrefetchable, IPXCompanyDependent
    {
      public bool? _PrintBeforeRelease;
      public bool? _EmailBeforeRelease;

      void IPrefetchable.Prefetch()
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<ARSetup>(new PXDataField[2]
        {
          new PXDataField("PrintBeforeRelease"),
          new PXDataField("EmailBeforeRelease")
        }))
        {
          this._PrintBeforeRelease = pxDataRecord != null ? pxDataRecord.GetBoolean(0) : new bool?(false);
          this._EmailBeforeRelease = pxDataRecord != null ? pxDataRecord.GetBoolean(1) : new bool?(false);
        }
      }
    }
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.ownerID>
  {
  }

  public new abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.salesPersonID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARInvoice.noteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARInvoice.refNoteID>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.hidden>
  {
  }

  public abstract class hiddenOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.hiddenOrderType>
  {
  }

  public abstract class hiddenOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.hiddenOrderNbr>
  {
  }

  public abstract class hiddenByShipment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.hiddenByShipment>
  {
  }

  public abstract class hiddenShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.hiddenShipmentType>
  {
  }

  public abstract class hiddenShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.hiddenShipmentNbr>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.isTaxSaved>
  {
  }

  public new abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.nonTaxable>
  {
  }

  public abstract class applyPaymentWhenTaxAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.applyPaymentWhenTaxAvailable>
  {
  }

  public abstract class isPaymentsTransferred : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isPaymentsTransferred>
  {
  }

  public abstract class proformaExists : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.proformaExists>
  {
  }

  public abstract class revoked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.revoked>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.pendingPPD>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isMigratedRecord>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.paymentsByLinesAllowed>
  {
  }

  public new abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.retainageApply>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isRetainageDocument>
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoice.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.disableAutomaticDiscountCalculation>
  {
  }

  public abstract class deferPriceDiscountRecalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.deferPriceDiscountRecalculation>
  {
  }

  public abstract class isPriceAndDiscountsValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.disableAutomaticDiscountCalculation>
  {
  }

  /// <exclude />
  public abstract class correctionDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.correctionDocType>
  {
  }

  /// <exclude />
  public abstract class correctionRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoice.correctionRefNbr>
  {
  }

  /// <exclude />
  public abstract class isUnderCancellation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isUnderCancellation>
  {
  }

  public abstract class pendingProcessingCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARInvoice.pendingProcessingCntr>
  {
  }

  public new abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.pendingProcessing>
  {
  }

  public abstract class captureFailedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoice.captureFailedCntr>
  {
  }

  public new abstract class documentKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARInvoice.documentKey>
  {
  }

  public abstract class isHiddenInIntercompanySales : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isHiddenInIntercompanySales>
  {
  }

  public abstract class isLoadApplications : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARInvoice.isLoadApplications>
  {
  }

  public new abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.pendingPayment>
  {
  }

  public abstract class authorizedPaymentCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARInvoice.authorizedPaymentCntr>
  {
  }

  public new abstract class printInvoice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.printInvoice>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.printed>
  {
  }

  public new abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.dontEmail>
  {
  }

  public new abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARInvoice.emailed>
  {
  }
}
