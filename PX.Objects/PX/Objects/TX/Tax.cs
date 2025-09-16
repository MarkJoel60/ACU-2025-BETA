// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.Tax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>Represents a tax.</summary>
[PXPrimaryGraph(typeof (SalesTaxMaint))]
[PXCacheName("Tax")]
[Serializable]
public class Tax : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxID;
  protected string _Descr;
  protected string _TaxType;
  protected string _TaxCalcType;
  protected string _TaxCalcLevel;
  protected string _TaxCalcRule;
  protected bool? _TaxCalcLevel2Exclude;
  protected string _TaxApplyTermsDisc;
  protected bool? _PendingTax;
  protected bool? _ReverseTax;
  protected bool? _IncludeInTaxable;
  protected bool? _ExemptTax;
  protected bool? _StatisticalTax;
  protected bool? _DirectTax;
  protected int? _TaxVendorID;
  protected int? _SalesTaxAcctID;
  protected int? _SalesTaxSubID;
  protected int? _PurchTaxAcctID;
  protected int? _PurchTaxSubID;
  protected int? _PendingSalesTaxAcctID;
  protected int? _PendingSalesTaxSubID;
  protected int? _PendingPurchTaxAcctID;
  protected int? _PendingPurchTaxSubID;
  protected int? _ExpenseAccountID;
  protected int? _ExpenseSubID;
  protected bool? _Outdated;
  protected DateTime? _OutDate;
  protected bool? _IsImported;
  protected bool? _IsExternal;
  protected bool? _ZeroTaxable;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The tax ID. This is the key field, which can be specified by the user.
  /// </summary>
  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr), CacheGlobal = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// The description of the tax, which can be specified by the user.
  /// </summary>
  [PXDBLocalizableString(100, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>The type of the tax.</summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"S"</c>: Sales tax.
  /// <c>"P"</c>: Use (purchase) tax.
  /// <c>"V"</c>: Value-added tax (VAT).
  /// <c>"W"</c>: Withholding.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("S")]
  [CSTaxType.List]
  [PXUIField]
  public virtual string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  /// <summary>The basis of the tax calculation.</summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"D"</c>: A tax is calculated on a document basis.
  /// <c>"I"</c>: A tax is calculated on a per-line basis.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXStringList(new string[] {"D", "I"}, new string[] {"Document", "Item"})]
  [PXFormula(typeof (Substring<Tax.taxCalcRule, One, One>))]
  public virtual string TaxCalcType
  {
    get => this._TaxCalcType;
    set => this._TaxCalcType = value;
  }

  /// <summary>The tax level.</summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"0"</c>: An inclusive tax.
  /// <c>"1"</c>: A first-level exclusive tax.
  /// <c>"2"</c>: A second-level exclusive tax.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("1")]
  [PXStringList(new string[] {"0", "1", "2"}, new string[] {"Inclusive", "Calc. On Item Amount", "Calc. On Item Amt + Tax Amt"})]
  [PXFormula(typeof (Substring<Tax.taxCalcRule, Two, One>))]
  public virtual string TaxCalcLevel
  {
    get => this._TaxCalcLevel;
    set => this._TaxCalcLevel = value;
  }

  /// <summary>
  /// The aggregated rule of tax calculation based on <see cref="P:PX.Objects.TX.Tax.TaxCalcType" /> and <see cref="P:PX.Objects.TX.Tax.TaxCalcLevel" />.
  /// </summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"I0"</c>: The tax amount is included in item amounts and should be extracted.
  /// <c>"I1"</c>: The tax is a first-level tax calculated on item amounts.
  /// <c>"I2"</c>: The tax is a second-level tax calculated on item amounts.
  /// <c>"D1"</c>: The tax is a first-level tax calculated on the document amount.
  /// <c>"D2"</c>: The tax is a second-level tax calculated on the document amount.
  /// </value>
  [PXString(2, IsFixed = true)]
  [PXStringList(new string[] {"I0", "I1", "I2", "D0", "D1", "D2"}, new string[] {"Inclusive Line-Level", "Exclusive Line-Level", "Compound Line-Level", "Inclusive Document-Level", "Exclusive Document-Level", "Compound Document-Level"})]
  [PXDBCalced(typeof (Add<Tax.taxCalcType, Tax.taxCalcLevel>), typeof (string))]
  [PXFormula(typeof (IsNull<BqlOperand<Current<Tax.taxCalcType>, IBqlString>.Concat<BqlField<Tax.taxCalcLevel, IBqlString>.FromCurrent>, Tax.taxCalcRule>))]
  [PXUIField(DisplayName = "Calculation Rule")]
  public virtual string TaxCalcRule
  {
    get => this._TaxCalcRule;
    set => this._TaxCalcRule = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system should exclude the first-level/per unit tax amount from the tax base
  /// that is used for calculation of the second-level tax amount in case of first-level taxes or all other taxes in case of per unit taxes.
  /// The flag is applicable to only first-level and per unit taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Tax-on-Tax Calculation")]
  public virtual bool? TaxCalcLevel2Exclude
  {
    get => this._TaxCalcLevel2Exclude;
    set => this._TaxCalcLevel2Exclude = value;
  }

  /// <summary>
  /// The method of calculating the tax base amount if a cash discount is applied.
  /// </summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"X"</c>: Reduce the taxable amount.
  /// <c>"P"</c>: Reduce taxable amount on early payment.
  /// <c>"N"</c>: Do not affect taxable amount.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [CSTaxTermsDiscount.List]
  [PXUIField(DisplayName = "Cash Discount")]
  public virtual string TaxApplyTermsDisc
  {
    get => this._TaxApplyTermsDisc;
    set => this._TaxApplyTermsDisc = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax is a pending VAT.
  /// The pending VAT should be calculated in documents, but should not be recorded in the tax report.
  /// Later the VAT of the pending type can be converted into the general VAT.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pending VAT")]
  public virtual bool? PendingTax
  {
    get => this._PendingTax;
    set => this._PendingTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax is a reverse VAT. When the reverse VAT is applied to a company that supplies goods or service to other EU countries,
  /// the liability of reporting VAT is reversed and goes to the customer rather than to the vendor.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Reverse VAT")]
  public virtual bool? ReverseTax
  {
    get => this._ReverseTax;
    set => this._ReverseTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the VAT taxable amount should be displayed in the VAT Taxable Total box in the documents,
  /// such as bills and invoices.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include in VAT Taxable Total")]
  public virtual bool? IncludeInTaxable
  {
    get => this._IncludeInTaxable;
    set => this._IncludeInTaxable = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the calculated amount should be displayed in the VAT Exempt Total box in the documents, such as bills and invoices.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include in VAT Exempt Total")]
  public virtual bool? ExemptTax
  {
    get => this._ExemptTax;
    set => this._ExemptTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax is a statistical VAT.
  /// The statistical VAT is calculated for statistical purposes; the VAT is reported but not paid.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Statistical VAT")]
  public virtual bool? StatisticalTax
  {
    get => this._StatisticalTax;
    set => this._StatisticalTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax can be entered only by the documents from the Tax Bills and Adjustments form (TX303000).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Direct-Entry Tax")]
  public virtual bool? DirectTax
  {
    get => this._DirectTax;
    set => this._DirectTax = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax is a deductible VAT,
  /// which means that a company is allowed to deduct some part of the tax paid to a vendor from its own VAT liability to the government.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Partially Deductible VAT")]
  public virtual bool? DeductibleVAT { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the non-deductible part should be posted to the Tax Expense account.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Tax Expense Account", Visible = false)]
  public virtual bool? ReportExpenseToSingleAccount { get; set; }

  /// <summary>
  /// The unit of measure used by tax. Specific/Per Unit taxes are calculated on quantities in this UOM
  /// </summary>
  [INUnit(DisplayName = "Tax UOM", FieldClass = "PerUnitTaxSupport")]
  public virtual string TaxUOM { get; set; }

  /// <summary>
  /// An per-unit tax post mode for posting during the document release.
  /// Possible options are - post tax amounts on tax account or post to document line's account.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PerUnitTaxPostOptions.List]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Post To")]
  public virtual string PerUnitTaxPostMode { get; set; }

  /// <summary>
  /// The override of <see cref="P:PX.Objects.TX.Tax.SalesTaxAcctID" /> to display different labels for per-unit Tax.
  /// </summary>
  [Account(DisplayName = "Account to Use on Sale", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX", IsDBField = false)]
  public virtual int? SalesTaxAcctIDOverride
  {
    [PXDependsOnFields(new Type[] {typeof (Tax.salesTaxAcctID)})] get => this.SalesTaxAcctID;
    set => this.SalesTaxAcctID = value;
  }

  /// <summary>
  /// The override of <see cref="P:PX.Objects.TX.Tax.SalesTaxSubID" /> to display different labels for per-unit Tax.
  /// </summary>
  [SubAccount(typeof (Tax.salesTaxAcctID), DisplayName = "Subaccount to Use on Sale", DescriptionField = typeof (Sub.description), IsDBField = false)]
  public virtual int? SalesTaxSubIDOverride
  {
    [PXDependsOnFields(new Type[] {typeof (Tax.salesTaxSubID)})] get => this.SalesTaxSubID;
    set => this.SalesTaxSubID = value;
  }

  /// <summary>
  /// The override of <see cref="P:PX.Objects.TX.Tax.PurchTaxAcctID" /> to display different labels for per-unit Tax.
  /// </summary>
  [Account(DisplayName = "Account to Use on Purchase", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX", IsDBField = false)]
  public virtual int? PurchTaxAcctIDOverride
  {
    [PXDependsOnFields(new Type[] {typeof (Tax.purchTaxAcctID)})] get => this.PurchTaxAcctID;
    set => this.PurchTaxAcctID = value;
  }

  /// <summary>
  /// The override of <see cref="P:PX.Objects.TX.Tax.PurchTaxSubID" /> to display different labels for per-unit Tax.
  /// </summary>
  [SubAccount(typeof (Tax.purchTaxAcctID), DisplayName = "Subaccount to Use on Purchase", DescriptionField = typeof (Sub.description), IsDBField = false)]
  public virtual int? PurchTaxSubIDOverride
  {
    [PXDependsOnFields(new Type[] {typeof (Tax.purchTaxSubID)})] get => this.PurchTaxSubID;
    set => this.PurchTaxSubID = value;
  }

  /// <summary>
  /// The Short Printing Label value is used to print tax in the invoice lines.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Short Printing Label")]
  public virtual string ShortPrintingLabel { get; set; }

  /// <summary>
  /// The Long Printing Label of a tax is used to print total tax amount in the invoice totals section.
  /// </summary>
  [PXDBLocalizableString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Long Printing Label")]
  public virtual string LongPrintingLabel { get; set; }

  /// <summary>
  /// The Printing Sequence field value is used to define order in which taxes are printed on the documents.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Printing Sequence")]
  public virtual int? PrintingSequence { get; set; }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.AP.Vendor" />, which specifies the tax agency to which the tax belongs.
  /// The key can be NULL.
  /// </summary>
  [TaxAgencyActive]
  public virtual int? TaxVendorID
  {
    get => this._TaxVendorID;
    set => this._TaxVendorID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies the liability account that accumulates the tax amounts to be paid to a tax agency for the tax reporting period.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.salesTaxAcctID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [Account(DisplayName = "Tax Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.salesTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? SalesTaxAcctID
  {
    get => this._SalesTaxAcctID;
    set => this._SalesTaxAcctID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax payable subaccount.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.salesTaxSubID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [SubAccount(typeof (Tax.salesTaxAcctID), DisplayName = "Tax Payable Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.salesTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? SalesTaxSubID
  {
    get => this._SalesTaxSubID;
    set => this._SalesTaxSubID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies the account that accumulates the tax amounts to be claimed from the tax agency for the tax reporting period.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.purchTaxAcctID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [Account(DisplayName = "Tax Claimable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.purchTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PurchTaxAcctID
  {
    get => this._PurchTaxAcctID;
    set => this._PurchTaxAcctID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax claimable subaccount.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.purchTaxSubID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [SubAccount(typeof (Tax.purchTaxAcctID), DisplayName = "Tax Claimable Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.purchTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? PurchTaxSubID
  {
    get => this._PurchTaxSubID;
    set => this._PurchTaxSubID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies the liability account that accumulates the amount of taxes to be paid to a tax agency for the pending tax.
  /// </summary>
  [Account(DisplayName = "Pending Tax Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.pendingSalesTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PendingSalesTaxAcctID
  {
    get => this._PendingSalesTaxAcctID;
    set => this._PendingSalesTaxAcctID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax payable pending subaccount.
  /// </summary>
  [SubAccount(typeof (Tax.pendingSalesTaxAcctID), DisplayName = "Pending Tax Payable Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.pendingSalesTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? PendingSalesTaxSubID
  {
    get => this._PendingSalesTaxSubID;
    set => this._PendingSalesTaxSubID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies the account that accumulates the tax amounts to be claimed from the tax agency for the pending tax.
  /// </summary>
  [Account(DisplayName = "Pending Tax Claimable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.pendingPurchTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PendingPurchTaxAcctID
  {
    get => this._PendingPurchTaxAcctID;
    set => this._PendingPurchTaxAcctID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax claimable pending subaccount.
  /// </summary>
  [SubAccount(typeof (Tax.pendingPurchTaxAcctID), DisplayName = "Pending Tax Claimable Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.pendingPurchTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? PendingPurchTaxSubID
  {
    get => this._PendingPurchTaxSubID;
    set => this._PendingPurchTaxSubID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies the expense account that is used to record either
  /// the tax amounts of use taxes or the non-deductible tax amounts of deductible value-added taxes.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.taxExpenseAcctID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<Tax.expenseAccountID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? ExpenseAccountID
  {
    get => this._ExpenseAccountID;
    set => this._ExpenseAccountID = value;
  }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding expense subaccount.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.taxExpenseSubID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<Tax.taxVendorID>>>>))]
  [SubAccount(typeof (Tax.expenseAccountID))]
  [PXForeignReference(typeof (Field<Tax.expenseSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that <see cref="P:PX.Objects.TX.Tax.OutDate" /> was specified.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Outdated
  {
    get => this._Outdated;
    set => this._Outdated = value;
  }

  /// <summary>The date after which the tax is not effective.</summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? OutDate
  {
    get => this._OutDate;
    set => this._OutDate = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax configuration was imported from Avalara files.
  /// The field is obsolete.
  /// </summary>
  [Obsolete("This property is obsolete and will be removed in Acumatica 8.0")]
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsImported
  {
    get => this._IsImported;
    set => this._IsImported = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the tax zone is used for the external tax provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsExternal
  {
    get => this._IsExternal;
    set => this._IsExternal = value;
  }

  /// <summary>Allow Zero Taxable</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ZeroTaxable
  {
    get => this._ZeroTaxable;
    set => this._ZeroTaxable = value;
  }

  [PXNote(DescriptionField = typeof (Tax.taxID), Selector = typeof (Search<Tax.taxID>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  [Account(DisplayName = "Retainage Tax Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  public virtual int? RetainageTaxPayableAcctID { get; set; }

  [SubAccount(typeof (Tax.retainageTaxPayableAcctID), DisplayName = "Retainage Tax Payable Subaccount", DescriptionField = typeof (Sub.description))]
  public virtual int? RetainageTaxPayableSubID { get; set; }

  [Account(DisplayName = "Retainage Tax Claimable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  public virtual int? RetainageTaxClaimableAcctID { get; set; }

  [SubAccount(typeof (Tax.retainageTaxClaimableAcctID), DisplayName = "Retainage Tax Claimable Subaccount", DescriptionField = typeof (Sub.description))]
  public virtual int? RetainageTaxClaimableSubID { get; set; }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies liability or asset account
  /// that keeps VAT collected on prepayment invoices for which final invoice is not issued yet.
  /// Uses when the feature <see cref="!:FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [Account(DisplayName = "Tax on AR Prepayment Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.onARPrepaymentTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? OnARPrepaymentTaxAcctID { get; set; }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax on AR prepayments subaccount.
  /// Uses when the feature <see cref="!:FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [SubAccount(typeof (Tax.onARPrepaymentTaxAcctID), DisplayName = "Tax on AR Prepayment Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.onARPrepaymentTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? OnARPrepaymentTaxSubID { get; set; }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Account" />, which specifies liability or asset account
  /// that keeps VAT collected on AP prepayment invoices for which final bill is not issued yet.
  /// Uses when the feature <see cref="!:FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  [Account(DisplayName = "Tax on AP Prepayment Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  [PXForeignReference(typeof (Field<Tax.onAPPrepaymentTaxAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? OnAPPrepaymentTaxAcctID { get; set; }

  /// <summary>
  /// The foreign key to <see cref="T:PX.Objects.GL.Sub" />, which specifies the corresponding tax on AP prepayments subaccount.
  /// Uses when the feature <see cref="!:FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  [SubAccount(typeof (Tax.onAPPrepaymentTaxAcctID), DisplayName = "Tax on AP Prepayment Subaccount", DescriptionField = typeof (Sub.description))]
  [PXForeignReference(typeof (Field<Tax.onAPPrepaymentTaxSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? OnAPPrepaymentTaxSubID { get; set; }

  public class PK : PrimaryKeyOf<Tax>.By<Tax.taxID>
  {
    public static Tax Find(PXGraph graph, string taxID, PKFindOptions options = 0)
    {
      return (Tax) PrimaryKeyOf<Tax>.By<Tax.taxID>.FindBy(graph, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class TaxAgency : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<Tax>.By<Tax.taxVendorID>
    {
    }

    public class TaxPayableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.salesTaxAcctID>
    {
    }

    public class TaxPayableSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.salesTaxSubID>
    {
    }

    public class TaxClaimableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.purchTaxAcctID>
    {
    }

    public class TaxClaimableSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.purchTaxSubID>
    {
    }

    public class PendingTaxPayableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.pendingSalesTaxAcctID>
    {
    }

    public class PendingTaxPayableSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.pendingSalesTaxSubID>
    {
    }

    public class PendingTaxClaimableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.pendingPurchTaxAcctID>
    {
    }

    public class PendingTaxClaimableSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.pendingPurchTaxSubID>
    {
    }

    public class OnARPrepaymentTaxAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.onARPrepaymentTaxAcctID>
    {
    }

    public class OnARPrepaymentTaxSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.onARPrepaymentTaxSubID>
    {
    }

    public class OnAPPrepaymentTaxAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.onAPPrepaymentTaxAcctID>
    {
    }

    public class OnAPPrepaymentTaxSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.onAPPrepaymentTaxSubID>
    {
    }

    public class TaxExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Tax>.By<Tax.expenseAccountID>
    {
    }

    public class TaxExpenseSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<Tax>.By<Tax.expenseSubID>
    {
    }
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxID>
  {
    /// <summary>60</summary>
    public const int Length = 60;
    public const int MaxLengthForExternalTaxID = 55;
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.descr>
  {
    /// <summary>define tax description maximum length.</summary>
    public const int MaxLength = 100;
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxType>
  {
  }

  public abstract class taxCalcType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxCalcType>
  {
  }

  public abstract class taxCalcLevel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxCalcLevel>
  {
  }

  public abstract class taxCalcRule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxCalcRule>
  {
  }

  public abstract class taxCalcLevel2Exclude : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Tax.taxCalcLevel2Exclude>
  {
  }

  public abstract class taxApplyTermsDisc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxApplyTermsDisc>
  {
  }

  public abstract class pendingTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.pendingTax>
  {
  }

  public abstract class reverseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.reverseTax>
  {
  }

  public abstract class includeInTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.includeInTaxable>
  {
  }

  public abstract class exemptTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.exemptTax>
  {
  }

  public abstract class statisticalTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.statisticalTax>
  {
  }

  public abstract class directTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.directTax>
  {
  }

  public abstract class deductibleVAT : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.deductibleVAT>
  {
  }

  public abstract class reportExpenseToSingleAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Tax.reportExpenseToSingleAccount>
  {
  }

  /// <summary>
  /// The unit of measure used by tax. Specific/Per Unit taxes are calculated on quantities in this UOM.
  /// </summary>
  public abstract class taxUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.taxUOM>
  {
  }

  /// <summary>
  /// An per-unit tax post mode for posting during the document release.
  /// Possible options are - post tax amounts on tax account or post to document line's account.
  /// </summary>
  public abstract class perUnitTaxPostMode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.perUnitTaxPostMode>
  {
  }

  public abstract class salesTaxAcctIDOverride : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.salesTaxAcctIDOverride>
  {
  }

  public abstract class salesTaxSubIDOverride : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.salesTaxSubIDOverride>
  {
  }

  public abstract class purchTaxAcctIDOverride : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.purchTaxAcctIDOverride>
  {
  }

  public abstract class purchTaxSubIDOverride : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.purchTaxSubIDOverride>
  {
  }

  /// <summary>
  /// The Short Printing Label value is used to print tax in the invoice lines.
  /// </summary>
  public abstract class shortPrintingLabel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Tax.shortPrintingLabel>
  {
  }

  /// <summary>
  /// The Long Printing Label of a tax is used to print total tax amount in the invoice totals section.
  /// </summary>
  public abstract class longPrintingLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.longPrintingLabel>
  {
  }

  /// <summary>
  /// The Printing Sequence field value is used to define order in which taxes are printed on the documents.
  /// </summary>
  public abstract class printingSequence : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.printingSequence>
  {
  }

  public abstract class taxVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.taxVendorID>
  {
  }

  public abstract class salesTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.salesTaxAcctID>
  {
  }

  public abstract class salesTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.salesTaxSubID>
  {
  }

  public abstract class purchTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.purchTaxAcctID>
  {
  }

  public abstract class purchTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.purchTaxSubID>
  {
  }

  public abstract class pendingSalesTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.pendingSalesTaxAcctID>
  {
  }

  public abstract class pendingSalesTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.pendingSalesTaxSubID>
  {
  }

  public abstract class pendingPurchTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.pendingPurchTaxAcctID>
  {
  }

  public abstract class pendingPurchTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.pendingPurchTaxSubID>
  {
  }

  public abstract class expenseAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.expenseAccountID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Tax.expenseSubID>
  {
  }

  public abstract class outdated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.outdated>
  {
  }

  public abstract class outDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Tax.outDate>
  {
  }

  public abstract class isImported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.isImported>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.isExternal>
  {
  }

  public abstract class zeroTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Tax.zeroTaxable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Tax.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Tax.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Tax.createdByID>
  {
  }

  public abstract class createdByScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tax.createdByScreenID>
  {
  }

  public abstract class createdDateTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Tax.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Tax.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Tax.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Tax.lastModifiedDateTime>
  {
  }

  public abstract class retainageTaxPayableAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.retainageTaxPayableAcctID>
  {
  }

  public abstract class retainageTaxPayableSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.retainageTaxPayableSubID>
  {
  }

  public abstract class retainageTaxClaimableAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.retainageTaxClaimableAcctID>
  {
  }

  public abstract class retainageTaxClaimableSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.retainageTaxClaimableSubID>
  {
  }

  public abstract class onARPrepaymentTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.onARPrepaymentTaxAcctID>
  {
  }

  public abstract class onARPrepaymentTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.onARPrepaymentTaxSubID>
  {
  }

  public abstract class onAPPrepaymentTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.onAPPrepaymentTaxAcctID>
  {
  }

  public abstract class onAPPrepaymentTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Tax.onAPPrepaymentTaxSubID>
  {
  }
}
