// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoice
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
using PX.Objects.AP.MigrationMode;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Represents AP Invoices, Credit and Debit Adjustments.
/// The DAC is based on <see cref="T:PX.Objects.AP.APRegister" /> and extends it with the fields
/// relevant to the documents of the above types.
/// </summary>
[PXTable]
[PXSubstitute(GraphType = typeof (APInvoiceEntry))]
[PXSubstitute(GraphType = typeof (TXInvoiceEntry))]
[PXPrimaryGraph(typeof (APInvoiceEntry))]
[PXCacheName("AP document")]
[PXGroupMask(typeof (InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class APInvoice : 
  APRegister,
  IInvoice,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  IProjectTaxes,
  IAssign,
  IApprovable
{
  protected System.DateTime? _DueDate;
  protected System.DateTime? _DiscDate;
  protected 
  #nullable disable
  string _InvoiceNbr;
  protected System.DateTime? _InvoiceDate;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected string _DrCr;
  protected bool? _SeparateCheck;
  protected bool? _PaySel;
  protected int? _PayLocationID;
  protected System.DateTime? _PayDate;
  protected string _PayTypeID;
  protected int? _PayAccountID;
  protected int? _PrebookAcctID;
  protected int? _PrebookSubID;
  protected System.DateTime? _EstPayDate;
  protected bool? _LCEnabled = new bool?(false);
  protected bool? _HasWithHoldTax;
  protected bool? _HasUseTax;
  protected bool? _DisableAutomaticDiscountCalculation;

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APInvoice.branchID>, IsPending>, Null, Case<Where<APInvoice.vendorLocationID, PX.Data.IsNotNull, And<Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, PX.Data.IsNotNull>>, Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<Current2<APInvoice.branchID>, PX.Data.IsNotNull>, Current2<APInvoice.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>Type of the document.</summary>
  /// <value>
  /// Possible values are: "INV" - Invoice, "ACR" - Credit Adjustment, "ADR" - Debit Adjustment,
  /// "PPM" - Prepayment, "PPI" - Prepayment Invoice
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [APMigrationModeDependentInvoiceTypeList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  [PXFieldDescription]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [APInvoiceType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<Optional<APInvoice.docType>>, And2<Where<APRegisterAlias.origModule, NotEqual<BatchModule.moduleTX>, Or<APRegisterAlias.released, Equal<True>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>, PX.Data.OrderBy<Desc<APRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  [APInvoiceType.Numbering]
  [PXFieldDescription]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<APRegister.vendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Coalesce<Search2<Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Vendor.bAccountID>>>>, Where<Vendor.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [PXFormula(typeof (Default<APInvoice.vendorID>))]
  public override int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<BqlField<APInvoice.vendorID, IBqlInt>.FromCurrent>>>))]
  [PXFormula(typeof (IIf<Where<Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.isAPPaymentInfoSameAsMain>, Equal<True>>, Selector<APInvoice.vendorLocationID, PX.Objects.CR.Location.vPaymentInfoLocationID>, APInvoice.vendorLocationID>))]
  public int? PaymentInfoLocationID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor that supplied the goods.
  /// </value>
  [Vendor(DisplayName = "Supplied-By Vendor", DescriptionField = typeof (Vendor.acctName), FieldClass = "VendorRelations", CacheGlobal = true, Filterable = true, Required = false)]
  [PXRestrictor(typeof (Where<Vendor.vStatus, NotEqual<VendorStatus.inactive>, And<Vendor.vStatus, NotEqual<VendorStatus.hold>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (Vendor.vStatus)})]
  [PXRestrictor(typeof (Where<Vendor.bAccountID, Equal<Current<APInvoice.vendorID>>, Or<Vendor.payToVendorID, Equal<Current<APInvoice.vendorID>>>>), "Only the current vendor or any vendor listed on the Supplied-By Vendors tab of the Vendors (AP303000) form for the current vendor can be specified in the Pay-to Vendor box.", new System.Type[] {})]
  [PXFormula(typeof (APInvoice.vendorID))]
  [PXDefault]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APInvoice.suppliedByVendorID>.IsRelatedTo<Vendor.bAccountID>))]
  public virtual int? SuppliedByVendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location">Location</see> of the <see cref="T:PX.Objects.AP.Vendor">Supplied-By Vendor</see>, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field. Defaults to AP bill's <see cref="P:PX.Objects.AP.APInvoice.VendorLocationID">vendor location</see>.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APInvoice.suppliedByVendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DisplayName = "Supplied-By Vendor Location", DescriptionField = typeof (PX.Objects.CR.Location.descr), FieldClass = "VendorRelations", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
  [PXFormula(typeof (Switch<Case2<PX.Data.Where<Not<IsPOLinkedAPBill>>, Switch<Case2<Where2<FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>, And<Current<APInvoice.vendorID>, NotEqual<APInvoice.suppliedByVendorID>>>, Selector<APInvoice.suppliedByVendorID, PX.Objects.CR.BAccount.defLocationID>>, ExternalValue<APInvoice.vendorLocationID>>>, APInvoice.suppliedByVendorLocationID>))]
  [PXDefault]
  [PXForeignReference(typeof (CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<APInvoice.suppliedByVendorID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, PX.Data.ReferentialIntegrity.Attributes.Field<APInvoice.suppliedByVendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? SuppliedByVendorLocationID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CS.Terms">credit terms</see> associated with the document (unavailable for prepayments and debit adjustments).\
  /// Defaults to the <see cref="P:PX.Objects.AP.Vendor.TermsID">credit terms of the vendor</see>.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<Vendor.termsID, Where<Vendor.bAccountID, Equal<Current<APInvoice.vendorID>>, And2<Where<Current<APInvoice.docType>, NotEqual<APDocType.debitAdj>>, PX.Data.Or<Where<Current<APSetup.termsInDebitAdjustments>, Equal<True>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.Visible)]
  [APTermsSelector]
  [Terms(typeof (APInvoice.docDate), typeof (APInvoice.dueDate), typeof (APInvoice.discDate), typeof (APInvoice.curyOrigDocAmt), typeof (APInvoice.curyOrigDiscAmt), typeof (APInvoice.curyTaxTotal), typeof (APInvoice.branchID))]
  public virtual string TermsID { get; set; }

  /// <summary>
  /// The date when payment for the document is due in accordance with the <see cref="P:PX.Objects.AP.APInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Due Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the <see cref="P:PX.Objects.AP.APInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Cash Discount Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

  /// <summary>
  /// For an installment this field holds the <see cref="P:PX.Objects.AP.APInvoice.RefNbr">reference number</see> of the master document.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string MasterRefNbr { get; set; }

  /// <summary>
  /// The number of the installment, which the document represents.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.TermsInstallments.InstallmentNbr">TermsInstallments.InstallmentNbr</see> field.
  /// </value>
  [PXDBShort]
  public virtual short? InstallmentNbr { get; set; }

  /// <summary>Counter of the document's installments.</summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Number of Installments")]
  public virtual short? InstallmentCntr { get; set; }

  /// <summary>
  /// The document’s original reference number as assigned by the vendor (for informational purposes).
  /// The reference to the vendor document is required if <see cref="P:PX.Objects.AP.APSetup.RequireVendorRef" /> is set to <c>true</c>.
  /// The reference should also be unique if <see cref="P:PX.Objects.AP.APSetup.RaiseErrorOnDoubleInvoiceNbr" /> is set to <c>true</c>.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
  [APVendorRefNbr]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  /// <summary>
  /// The document’s original date as assigned by the vendor (for informational purposes).
  /// </summary>
  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Vendor Ref. Date", Visibility = PXUIVisibility.Invisible)]
  public virtual System.DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxZone">tax zone</see> associated with the document.
  /// Defaults to <see cref="P:PX.Objects.CR.Location.VTaxZoneID">vendor's tax zone</see>.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Vendor Tax Zone", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<APInvoice.suppliedByVendorLocationID, APInvoice.suppliedByVendorID, APInvoice.vendorLocationID>))]
  public virtual string TaxZoneID { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ExternalTaxesImportInProgress { get; set; }

  /// <summary>
  /// The total amount of taxes associated with the document. (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.taxTotal))]
  [PXUIField(DisplayName = "Tax Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  /// <summary>
  /// The total amount of taxes associated with the document. (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  /// <summary>
  /// The document total presented in the currency of the document. (See <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.lineTotal))]
  [PXUIField(DisplayName = "Detail Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  /// <summary>
  /// The document total presented in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.taxAmt))]
  [PXUIField(DisplayName = "Tax Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The part of the document total that is exempt from VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are marked as <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>
  /// and are neither <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see> nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.vatExemptTotal))]
  [PXUIField(DisplayName = "Tax Exempt Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  /// <summary>
  /// The part of the document total that is exempt from VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are marked as <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>
  /// and are neither <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see> nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  /// <summary>
  /// The part of the document total, which is subject to VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are neither <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>,
  /// nor <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see>, nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.vatTaxableTotal))]
  [PXUIField(DisplayName = "Taxable Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  /// <summary>
  /// The part of the document total, which is subject to VAT.
  /// This total is calculated as a sum of the taxable amounts for the <see cref="T:PX.Objects.TX.Tax">taxes</see>
  /// of <see cref="P:PX.Objects.TX.Tax.TaxType">type</see> VAT, which are neither <see cref="P:PX.Objects.TX.Tax.ExemptTax">exempt</see>,
  /// nor <see cref="P:PX.Objects.TX.Tax.StatisticalTax">statistical</see>, nor <see cref="P:PX.Objects.TX.Tax.ReverseTax">reverse</see>.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.initDocBal))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXUIVerify(typeof (Where<APInvoice.hold, Equal<True>, Or<APInvoice.isMigratedRecord, NotEqual<True>, PX.Data.Or<Where<APInvoice.curyInitDocBal, GreaterEqual<decimal0>, And<APInvoice.curyInitDocBal, LessEqual<APInvoice.curyOrigDocAmt>>>>>>), PXErrorLevel.Error, "Migrated balance cannot be less than zero or greater than the document amount.", new System.Type[] {}, CheckOnInserted = false, CheckOnRowSelected = false, CheckOnVerify = false, CheckOnRowPersisting = true)]
  public override Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// Read-only field indicating whether the document is of debit or credit type.
  /// The value of this field is based solely on the <see cref="P:PX.Objects.AP.APInvoice.DocType" /> field.
  /// </summary>
  /// <value>
  /// Possible values are <c>"D"</c> (for Invoice, Credit Adjustment, Prepayment, Cash Purchase)
  /// and <c>"C"</c> (for Debit Adjustment and Voided Cash Purchase).
  /// </value>
  [PXString(1, IsFixed = true)]
  public string DrCr
  {
    [PXDependsOnFields(new System.Type[] {typeof (APInvoice.docType)})] get
    {
      return APInvoiceType.DrCr(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document should be paid for by a separate check.
  /// In other words, the payment to such a document must not be consolidated with other payments.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.CR.Location.SeparateCheck">same setting</see> for vendor.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Pay Separately", Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.separateCheck, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APInvoice.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APInvoice.vendorLocationID>>>>>))]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is approved for payment.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Approved for Payment")]
  public bool? PaySel
  {
    get => this._PaySel;
    set => this._PaySel = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.AP.Vendor" /> location for payment.
  /// </summary>
  /// <value>
  /// Defaults to vendor's <see cref="!:Vendor.DefLocationID">default location</see>
  /// or to the first active location of the vendor if the former is not set.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APRegister.vendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Payment Location")]
  [PXDefault(typeof (Coalesce<Search2<Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Vendor.bAccountID>>>>, Where<Vendor.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  public virtual int? PayLocationID
  {
    get => this._PayLocationID;
    set => this._PayLocationID = value;
  }

  /// <summary>The date when the bill has been approved for payment.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Pay Date", Visibility = PXUIVisibility.Visible)]
  [PXDefault]
  [PXFormula(typeof (DateMinusDaysNotLessThenDate<Switch<Case<Where<Selector<APInvoice.paymentInfoLocationID, PX.Objects.CR.Location.vPaymentByType>, Equal<APPaymentBy.discountDate>, And<APInvoice.discDate, PX.Data.IsNotNull>>, APInvoice.discDate>, APInvoice.dueDate>, IsNull<Selector<APInvoice.paymentInfoLocationID, PX.Objects.CR.Location.vPaymentLeadTime>, decimal0>, APInvoice.docDate>))]
  public virtual System.DateTime? PayDate
  {
    get => this._PayDate;
    set => this._PayDate = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see> used for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID">PaymentMethod.PaymentMethodID</see> field.
  /// Defaults to the payment method associated with the vendor location.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.paymentMethodID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APInvoice.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APInvoice.payLocationID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.CashAccount">cash account</see> used for the payment.
  /// </summary>
  /// <value>
  /// Defaults to the cash account associated with the selected <see cref="P:PX.Objects.AP.APInvoice.PayLocationID">location</see> and <see cref="P:PX.Objects.AP.APInvoice.PayTypeID">payment method</see>.
  /// In case such account is not found the default value will be the cash account which is specified as
  /// <see cref="P:PX.Objects.CA.PaymentMethodAccount.APIsDefault">default for AP</see> for the selected payment method.
  /// </value>
  [PXDefault(typeof (Coalesce<Coalesce<Search2<PX.Objects.CR.Location.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CR.Location.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APInvoice.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APInvoice.payLocationID>>, And<PX.Objects.CR.Location.vPaymentMethodID, Equal<Current<APInvoice.payTypeID>>>>>>, Search2<PX.Objects.CR.Location.cashAccountID, InnerJoin<LocationAlias, On<PX.Objects.CR.Location.locationID, Equal<LocationAlias.vPaymentInfoLocationID>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CR.Location.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>, Where<LocationAlias.locationID, NotEqual<LocationAlias.vPaymentInfoLocationID>, And<LocationAlias.bAccountID, Equal<Current<APInvoice.vendorID>>, And<LocationAlias.locationID, Equal<Current<APInvoice.payLocationID>>, And<PX.Objects.CR.Location.vPaymentMethodID, Equal<Current<APInvoice.payTypeID>>>>>>>>, Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<APInvoice.payTypeID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<APInvoice.branchID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [CashAccount(typeof (APInvoice.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APInvoice.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
  [PXFormula(typeof (Validate<APInvoice.curyID>))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APInvoice.payAccountID>.IsRelatedTo<PX.Objects.CA.CashAccount.cashAccountID>))]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  /// <summary>
  /// The expense <see cref="T:PX.Objects.GL.Account">account</see> used to record the expenses pending reclassification.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.Prebooking">Support for Expense Reclassification feature</see> is activated
  /// and the document has or has had the Prebooked (<c>"K"</c>) status. (See <see cref="P:PX.Objects.AP.APRegister.Prebooked" />)
  /// </summary>
  /// <value>
  /// Defaults to the account associated with the vendor of the document.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID">AccountID</see> field.
  /// </value>
  [PXDefault(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APInvoice.vendorID>>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.prebooking>>>>), SourceField = typeof (Vendor.prebookAcctID), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Reclassification Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? PrebookAcctID
  {
    get => this._PrebookAcctID;
    set => this._PrebookAcctID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.Sub">subaccount</see> used to record the expenses pending reclassification.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.Prebooking">Support for Expense Reclassification feature</see> is activated
  /// and the document has or has had the Prebooked (<c>"K"</c>) status. (See <see cref="P:PX.Objects.AP.APRegister.Prebooked" />)
  /// </summary>
  /// <value>
  /// Defaults to the subaccount associated with the vendor of the document.
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID">AccountID</see> field.
  /// </value>
  [PXDefault(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APInvoice.vendorID>>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.prebooking>>>>), SourceField = typeof (Vendor.prebookSubID), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (APInvoice.prebookAcctID), DisplayName = "Reclassification Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? PrebookSubID
  {
    get => this._PrebookSubID;
    set => this._PrebookSubID = value;
  }

  /// <summary>Estimated payment date.</summary>
  /// <value>
  /// The field is calculated and equals either <see cref="P:PX.Objects.AP.APInvoice.PayDate" /> if the document is
  /// <see cref="P:PX.Objects.AP.APInvoice.PaySel">selected for payment</see> or the <see cref="P:PX.Objects.AP.APInvoice.DueDate" /> otherwise.
  /// </value>
  [PXDBCalced(typeof (Switch<Case<Where<APInvoice.paySel, Equal<True>>, APInvoice.payDate>, APInvoice.dueDate>), typeof (System.DateTime))]
  public virtual System.DateTime? EstPayDate
  {
    get => this._EstPayDate;
    set => this._EstPayDate = value;
  }

  /// <summary>
  /// Indicates whether landed cost is enabled for the document.
  /// </summary>
  /// <value>
  /// Equals <c>true</c> if the vendor of the document is a <see cref="P:PX.Objects.AP.Vendor.LandedCostVendor">Landed Cost vendor</see>.
  /// </value>
  [PXBool]
  [PXUIField(Visible = false)]
  public virtual bool? LCEnabled
  {
    get => this._LCEnabled;
    set => this._LCEnabled = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(1, "AP {0}: {1} - {3}", new System.Type[] {typeof (APInvoice.docType), typeof (APInvoice.refNbr), typeof (APInvoice.vendorID), typeof (Vendor.acctName)}, new System.Type[] {typeof (APInvoice.invoiceNbr), typeof (APInvoice.docDesc)}, NumberFields = new System.Type[] {typeof (APInvoice.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (APInvoice.docDate), typeof (APInvoice.status), typeof (APInvoice.invoiceNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (APInvoice.docDesc)}, MatchWithJoin = typeof (InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>>), SelectForFastIndexing = typeof (Select2<APInvoice, InnerJoin<Vendor, On<APInvoice.vendorID, Equal<Vendor.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<APInvoice.refNbr, InnerJoinSingleTable<APRegister, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegister.vendorID, Equal<Vendor.bAccountID>>>>, Where2<Where<APRegister.origModule, NotEqual<BatchModule.moduleTX>, Or<APRegister.released, Equal<True>>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>, PX.Data.OrderBy<Desc<APRegister.refNbr>>>))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (APInvoice.taxZoneID), typeof (APInvoice.taxCalcMode), typeof (APRegister.origModule))]
  public virtual bool? HasWithHoldTax
  {
    get => this._HasWithHoldTax;
    set => this._HasWithHoldTax = value;
  }

  [PXBool]
  [RestrictUseTaxCalcMode(typeof (APInvoice.taxZoneID), typeof (APInvoice.taxCalcMode), typeof (APRegister.origModule))]
  public virtual bool? HasUseTax
  {
    get => this._HasUseTax;
    set => this._HasUseTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document is tax bill and orginally has been created by tax reporting process.
  /// </summary>
  [PXDBBool]
  public virtual bool? IsTaxDocument { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Pay by Line", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  [PXDefault(false)]
  public override bool? PaymentsByLinesAllowed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Joint Payees", FieldClass = "Construction")]
  public bool? IsJointPayees { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AP.APInvoice.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.lineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the invoice.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>Sum of ext value (line total and line discount total)</summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  public virtual Decimal? DetailExtPriceTotal { get; set; }

  /// <summary>Sum of ext value (line total and line discount total)</summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.detailExtPriceTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<Add<APInvoice.curyLineTotal, APInvoice.curyLineDiscTotal>, APRegister.curyLineRetainageTotal>))]
  [PXUIField(DisplayName = "Detail Total")]
  public virtual Decimal? CuryDetailExtPriceTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AP.APInvoice.orderDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document and line discounts of the invoice.
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoice.curyInfoID), typeof (APInvoice.orderDiscTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<APInvoice.curyDiscTot, APInvoice.curyLineDiscTotal>))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  [PXUIVisible(typeof (FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>))]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <summary>
  /// The total discount of the document, which is calculated as the sum of group, document and line discounts of the invoice.
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<APInvoice.discTot, APInvoice.lineDiscTotal>))]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? OrderDiscTotal { get; set; }

  /// <summary>
  /// Indicates that the current document should be excluded from the
  /// approval process.
  /// </summary>
  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Don't Approve", Visible = false, Enabled = false)]
  public override bool? DontApprove { get; set; }

  [PXBool]
  public bool? SetWarningOnDiscount { get; set; }

  [PXBool]
  [PXDBCalced(typeof (False), typeof (bool))]
  public virtual bool? ManualEntry { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Related AR Document", FieldClass = "InterBranch")]
  [DocumentSelector(typeof (SearchFor<PX.Objects.AR.ARInvoice.noteID>.In<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<APInvoice>.On<BqlOperand<PX.Objects.AR.ARInvoice.noteID, IBqlGuid>.IsEqual<APInvoice.intercompanyInvoiceNoteID>>>, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.AR.ARInvoice.customerID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.bAccountID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlOperand<PX.Objects.AR.ARInvoice.branchID, IBqlInt>.IsEqual<BranchAlias.branchID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, PX.Data.And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<APInvoice.branchID, IBqlInt>.FromCurrent>>>, PX.Data.And<BqlOperand<BranchAlias.bAccountID, IBqlInt>.IsEqual<BqlField<APInvoice.vendorID, IBqlInt>.FromCurrent>>>, PX.Data.And<BqlOperand<APInvoice.refNbr, IBqlString>.IsNull>>, PX.Data.And<BqlOperand<PX.Objects.AR.ARInvoice.isHiddenInIntercompanySales, IBqlBool>.IsNotEqual<True>>>, PX.Data.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APInvoice.docType>, In3<APDocType.invoice, APDocType.creditAdj>>>>, PX.Data.And<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsIn<ARDocType.invoice, ARDocType.debitMemo, ARDocType.smallCreditWO, ARDocType.finCharge>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APInvoice.docType>, Equal<APDocType.debitAdj>>>>, PX.Data.And<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.pendingPPD, IBqlBool>.IsNotEqual<True>>>>>>.Or<BqlOperand<PX.Objects.AR.ARInvoice.noteID, IBqlGuid>.IsEqual<BqlField<APInvoice.intercompanyInvoiceNoteID, IBqlGuid>.FromCurrent>>>>), new System.Type[] {typeof (PX.Objects.AR.ARInvoice.docType), typeof (PX.Objects.AR.ARInvoice.refNbr), typeof (PX.Objects.AR.ARInvoice.docDate), typeof (PX.Objects.AR.ARInvoice.finPeriodID), typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt), typeof (PX.Objects.AR.ARInvoice.curyID), typeof (PX.Objects.AR.ARInvoice.docDesc), typeof (PX.Objects.AR.ARInvoice.invoiceNbr)}, SubstituteKey = typeof (PX.Objects.AR.ARInvoice.documentKey), SelectorMode = PXSelectorMode.TextModeReadonly | PXSelectorMode.DisplayModeValue)]
  public virtual Guid? IntercompanyInvoiceNoteID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update", Visible = false)]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Description", FieldClass = "InterBranch")]
  [PXDBCalced(typeof (BqlOperand<PX.Data.Concat<TypeArrayOf<IBqlOperand>.FilledWith<APInvoice.docType, Space>>, IBqlString>.Concat<APInvoice.refNbr>), typeof (string))]
  [PX.Objects.Common.Attributes.DocumentKey(typeof (APDocType.ListAttribute))]
  public virtual string DocumentKey { get; set; }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search2<PX.Objects.CR.Location.cAvalaraExemptionNumber, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<APInvoice.branchID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>
  /// The entity usage type for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" />.
  /// Defaults to the <see cref="P:PX.Objects.AP.APInvoice.BranchID">Tax Exemption Type</see>
  /// that is specified for the <see cref="P:PX.Objects.AP.APInvoice.BranchID">location of the branch</see>.
  /// </value>
  [PXDefault("0", typeof (Search2<PX.Objects.CR.Location.cAvalaraCustomerUsageType, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<APInvoice.branchID>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  public new class PK : PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>
  {
    public static APInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APInvoice>.By<APInvoice.docType, APInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APInvoice>.By<APInvoice.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APInvoice>.By<APInvoice.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APInvoice>.By<APInvoice.vendorID, APInvoice.vendorLocationID>
    {
    }

    public class SuppliedByVendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APInvoice>.By<APInvoice.suppliedByVendorID>
    {
    }

    public class SuppliedByVendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APInvoice>.By<APInvoice.suppliedByVendorID, APInvoice.suppliedByVendorLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APInvoice>.By<APInvoice.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APInvoice>.By<APInvoice.curyID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APInvoice>.By<APInvoice.aPAccountID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APInvoice>.By<APInvoice.aPSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<APInvoice>.By<APInvoice.scheduleID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<APInvoice>.By<APInvoice.termsID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<APInvoice>.By<APInvoice.taxZoneID>
    {
    }

    public class PayAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<APInvoice>.By<APInvoice.payAccountID>
    {
    }

    public class PrebookAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APInvoice>.By<APInvoice.prebookAcctID>
    {
    }

    public class PrebookSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APInvoice>.By<APInvoice.prebookSubID>
    {
    }

    public class PayType : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<APInvoice>.By<APInvoice.payTypeID>
    {
    }
  }

  public new class Events : PXEntityEventBase<APInvoice>.Container<APInvoice.Events>
  {
    public PXEntityEvent<APInvoice> ReleaseDocument;
    public PXEntityEvent<APInvoice> OpenDocument;
    public PXEntityEvent<APInvoice> CloseDocument;
    public PXEntityEvent<APInvoice> VoidDocument;
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.selected>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.branchID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.refNbr>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.status>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.tranPeriodID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.vendorLocationID>
  {
  }

  public abstract class paymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.paymentInfoLocationID>
  {
  }

  public abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.suppliedByVendorID>
  {
  }

  public abstract class suppliedByVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoice.suppliedByVendorLocationID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.curyID>
  {
  }

  public new abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.aPAccountID>
  {
  }

  public new abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.aPSubID>
  {
  }

  public new abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.prepaymentAccountID>
  {
  }

  public new abstract class prepaymentSubID : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.prepaymentSubID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.termsID>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.discDate>
  {
  }

  public abstract class masterRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.masterRefNbr>
  {
  }

  public abstract class installmentNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APInvoice.installmentNbr>
  {
  }

  public abstract class installmentCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  APInvoice.installmentCntr>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.taxZoneID>
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
    APInvoice.externalTaxesImportInProgress>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.docDate>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.taxTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.lineTotal>
  {
  }

  public new abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.discTot>
  {
  }

  public new abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDiscTot>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.taxAmt>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.docDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDocDisc>
  {
  }

  public new abstract class roundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.roundDiff>
  {
  }

  public new abstract class curyRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyRoundDiff>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.vatTaxableTotal>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APInvoice.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.origDocAmt>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.docBal>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyInitDocBal>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.discBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.origDiscAmt>
  {
  }

  public new abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyOrigWhTaxAmt>
  {
  }

  public new abstract class origWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.origWhTaxAmt>
  {
  }

  public new abstract class curyWhTaxBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyWhTaxBal>
  {
  }

  public new abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.whTaxBal>
  {
  }

  public new abstract class curyTaxWheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyTaxWheld>
  {
  }

  public new abstract class taxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.taxWheld>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.drCr>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.separateCheck>
  {
  }

  public abstract class paySel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.paySel>
  {
  }

  public abstract class payLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.payLocationID>
  {
  }

  public abstract class payDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.payDate>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.payTypeID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.payAccountID>
  {
  }

  public abstract class prebookAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.prebookAcctID>
  {
  }

  public abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.prebookSubID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.hold>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.printed>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.voided>
  {
  }

  public new abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.prebooked>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.batchNbr>
  {
  }

  public new abstract class prebookBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.prebookBatchNbr>
  {
  }

  public new abstract class voidBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.voidBatchNbr>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.scheduleID>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.scheduled>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.docDesc>
  {
  }

  public new abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.vendorID_Vendor_acctName>
  {
  }

  public abstract class estPayDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APInvoice.estPayDate>
  {
  }

  public abstract class lCEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.lCEnabled>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isTaxSaved>
  {
  }

  public new abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.nonTaxable>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APInvoice.noteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APInvoice.refNoteID>
  {
  }

  public new abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.taxCalcMode>
  {
  }

  public abstract class hasWithHoldTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.hasUseTax>
  {
  }

  public abstract class isTaxDocument : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isTaxDocument>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.isMigratedRecord>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.paymentsByLinesAllowed>
  {
  }

  public abstract class isJointPayees : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.isJointPayees>
  {
  }

  public new abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.retainageApply>
  {
  }

  public new abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.retainageUnreleasedAmt>
  {
  }

  public new abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.retainageReleased>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.isRetainageDocument>
  {
  }

  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyLineDiscTotal>
  {
  }

  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APInvoice.lineDiscTotal>
  {
  }

  public abstract class detailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.detailExtPriceTotal>
  {
  }

  public abstract class curyDetailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyDetailExtPriceTotal>
  {
  }

  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.curyOrderDiscTotal>
  {
  }

  public abstract class orderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoice.orderDiscTotal>
  {
  }

  public new abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.dontApprove>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.pendingPPD>
  {
  }

  public new abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.pendingPayment>
  {
  }

  public abstract class setWarningOnDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.setWarningOnDiscount>
  {
  }

  public abstract class manualEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APInvoice.manualEntry>
  {
  }

  public new abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.origDocType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APInvoice.origRefNbr>
  {
  }

  public abstract class intercompanyInvoiceNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoice.intercompanyInvoiceNoteID>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.disableAutomaticDiscountCalculation>
  {
  }

  public abstract class documentKey : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APInvoice.documentKey>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APInvoice.projectID>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.externalTaxExemptionNumber>
  {
  }

  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoice.entityUsageType>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoice.hasMultipleProjects>
  {
  }
}
