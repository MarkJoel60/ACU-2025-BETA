// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// AR-specific business account data related to customer payment methods,
/// statement cycles, and credit verification rules.
/// </summary>
[PXTable(new System.Type[] {typeof (PX.Objects.CR.BAccount.bAccountID)})]
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (BusinessAccountMaint), typeof (CustomerMaint), typeof (CustomerMaint), typeof (CustomerMaint), typeof (BusinessAccountMaint)}, new System.Type[] {typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, And<Current<PX.Objects.CR.BAccount.viewInCrm>, Equal<True>>>>), typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, Or<Customer.bAccountID, Equal<Current<BAccountR.bAccountID>>>>>), typeof (Select<Customer, Where<Customer.acctCD, Equal<Current<PX.Objects.CR.BAccount.acctCD>>, Or<Customer.acctCD, Equal<Current<BAccountR.acctCD>>>>>), typeof (Where<BAccountR.bAccountID, Less<Zero>, And<BAccountR.type, Equal<BAccountType.customerType>>>), typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, Or<Current<PX.Objects.CR.BAccount.bAccountID>, Less<Zero>>>>)})]
[PXODataDocumentTypesRestriction(typeof (CustomerMaint))]
[PXCacheName("Customer")]
[Serializable]
public class Customer : PX.Objects.CR.BAccount, IIncludable, IRestricted
{
  protected 
  #nullable disable
  string _CustomerClassID;
  protected int? _DefBillAddressID;
  protected int? _DefBillContactID;
  protected string _TermsID;
  protected int? _DiscTakenAcctID;
  protected int? _DiscTakenSubID;
  protected int? _PrepaymentAcctID;
  protected int? _PrepaymentSubID;
  protected bool? _AutoApplyPayments;
  protected bool? _PrintStatements;
  protected bool? _PrintCuryStatements;
  protected string _CreditRule;
  protected Decimal? _CreditLimit;
  protected short? _CreditDaysPastDue;
  protected string _StatementType;
  protected string _StatementCycleId;
  protected DateTime? _StatementLastDate;
  protected bool? _SmallBalanceAllow;
  protected Decimal? _SmallBalanceLimit;
  protected bool? _FinChargeApply;
  protected new byte[] _GroupMask;
  protected string _DefPaymentMethodID;
  protected int? _DefPMInstanceID;
  protected bool? _PrintInvoices;
  protected bool? _MailInvoices;
  protected bool? _PrintDunningLetters;
  protected bool? _MailDunningLetters;
  protected bool? _Included;

  /// <summary>
  /// The human-readable identifier of the customer account, which is
  /// specified by the user or defined by the auto-numbering sequence during
  /// creation of the customer. This field is a natural key, as opposed
  /// to the surrogate key <see cref="P:PX.Objects.CR.BAccount.BAccountID" />.
  /// </summary>
  [CustomerRaw(IsKey = true)]
  [PXDefault]
  [PXFieldDescription]
  [PXPersonalDataWarning]
  public override string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  /// <summary>
  /// Represents the type of the business account of the customer.
  /// The field defaults to <see cref="F:PX.Objects.CR.BAccountType.CustomerType" />;
  /// however, the field can have a value of <see cref="F:PX.Objects.CR.BAccountType.CombinedType" />
  /// if the customer account has been extended to this type.
  /// </summary>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("CU")]
  [PXUIField(DisplayName = "Type")]
  [BAccountType.List]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// A calculated field that indicates (if set to <c>true</c>) that <see cref="P:PX.Objects.AR.Customer.Type" /> is
  /// either <see cref="F:PX.Objects.CR.BAccountType.CustomerType" /> or <see cref="F:PX.Objects.CR.BAccountType.CombinedType" />.
  /// The field is inherited from the <see cref="T:PX.Objects.CR.BAccount" /> class and must always return
  /// <c>true</c> for a customer account.
  /// </summary>
  [PXBool]
  [PXDefault(true)]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>, True>, False>), typeof (bool))]
  public override bool? IsCustomerOrCombined { get; set; }

  /// <summary>
  /// When set to true indicates that consolidated statements are prepared for the customer and
  /// its parent and siblings. Otherwise, individual statements are prepared.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidate Statements")]
  public virtual bool? ConsolidateStatements { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that:
  /// <list type="number">
  /// <item><description>
  /// Credit control is enabled at the parent level; that is, the group
  /// credit verification settings are specified for the parent account.
  /// </description></item>
  /// <item><description>
  /// Dunning letters are consolidated to the parent account.
  /// </description></item>
  /// </list>
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Share Credit Policy")]
  public virtual bool? SharedCreditPolicy { get; set; }

  /// <summary>
  /// Identifier of the customer, whose statements include data for this customer.
  /// </summary>
  /// <value>
  /// When <see cref="P:PX.Objects.AR.Customer.ConsolidateStatements" /> is true, this field holds the ID of the parent customer (if present).
  /// When <see cref="P:PX.Objects.AR.Customer.ConsolidateStatements" /> is false, individual statements are prepared
  /// and this field is equal to the ID of this customer.
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// The field is populated by a formula, working only in the scope of the Customers (AR303000) form.
  /// See <see cref="M:PX.Objects.AR.CustomerMaint.Customer_StatementCustomerID_CacheAttached(PX.Data.PXCache)" />"
  /// </value>
  [PXDBInt]
  public virtual int? StatementCustomerID { get; set; }

  /// <summary>
  /// Identifier of the customer, through which the credit control is set up and maintained for this customer.
  /// </summary>
  /// <value>
  /// When <see cref="P:PX.Objects.AR.Customer.SharedCreditPolicy" /> is true, this field holds the ID of the parent customer (if present).
  /// When <see cref="P:PX.Objects.AR.Customer.SharedCreditPolicy" /> is false, credit control is executed individually for this customer
  /// and this field is equal to its ID.
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// The field is populated by a formula, working only in the scope of the Customers (AR303000) form.
  /// See <see cref="M:PX.Objects.AR.CustomerMaint.Customer_SharedCreditCustomerID_CacheAttached(PX.Data.PXCache)" />"
  /// </value>
  [PXDBInt]
  public virtual int? SharedCreditCustomerID { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.AR.CustomerClass">customer
  /// class</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (Customer.customerClassID), typeof (PX.Objects.CR.BAccount.noteID), new System.Type[] {typeof (PX.Objects.CR.BAccount.classID), typeof (PX.Objects.AP.Vendor.vendorClassID)})]
  public override string[] Attributes { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AR.CustomerClass">customer class</see>
  /// to which the customer belongs.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<ARSetup.dfltCustomerClassID>))]
  [PXSelector(typeof (Search<CustomerClass.customerClassID, Where<MatchUser>>), CacheGlobal = true, DescriptionField = typeof (CustomerClass.descr))]
  [PXUIField(DisplayName = "Customer Class")]
  [PXForeignReference(typeof (Field<Customer.customerClassID>.IsRelatedTo<CustomerClass.customerClassID>))]
  public virtual string CustomerClassID
  {
    get => this._CustomerClassID;
    set => this._CustomerClassID = value;
  }

  /// <summary>An obsolete field.</summary>
  [PXDBString(4, IsFixed = true)]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public virtual string LanguageID { get; set; }

  /// <summary>An obsolete field.</summary>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public virtual int? DefSOAddressID { get; set; }

  /// <summary>
  /// The billing <see cref="T:PX.Objects.CR.Address" /> associated with the customer.
  /// </summary>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [PXForeignReference(typeof (Field<Customer.defBillAddressID>.IsRelatedTo<PX.Objects.CR.Address.addressID>))]
  public virtual int? DefBillAddressID
  {
    get => this._DefBillAddressID;
    set => this._DefBillAddressID = value;
  }

  /// <summary>
  /// The billing <see cref="T:PX.Objects.CR.Contact" /> associated with the customer.
  /// </summary>
  [PXDBInt]
  [PXUIField]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXForeignReference(typeof (Field<Customer.defBillContactID>.IsRelatedTo<PX.Objects.CR.Contact.contactID>))]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID>), DirtyRead = true)]
  public virtual int? DefBillContactID
  {
    get => this._DefBillContactID;
    set => this._DefBillContactID = value;
  }

  /// <summary>An obsolete field.</summary>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<Customer.bAccountID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>))]
  [PXUIField(DisplayName = "Default Contact")]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public virtual int? BaseBillContactID { get; set; }

  /// <summary>
  /// The identifier of the default <see cref="T:PX.Objects.CS.Terms">terms</see>,
  /// which are applied to the documents of the customer.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  [PXDefault(typeof (Search<CustomerClass.termsID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Terms")]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Currency" />,
  /// which is applied to the documents of the customer.
  /// </summary>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  [PXDefault(typeof (IIf<BqlOperand<Customer.customerClassID, IBqlString>.IsNull, Customer.curyID, Selector<Customer.customerClassID, CustomerClass.curyID>>))]
  public override string CuryID { get; set; }

  /// <summary>
  /// The identifier of the currency rate type,
  /// which is applied to the documents of the customer.
  /// </summary>
  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXDefault(typeof (Selector<Customer.customerClassID, CustomerClass.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<Customer.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public override string CuryRateTypeID { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, indicates that the currency
  /// of customer documents (which is specified by <see cref="P:PX.Objects.AR.Customer.CuryID" />)
  /// can be overridden by a user during document entry.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (IIf<BqlOperand<Customer.customerClassID, IBqlString>.IsNull, Customer.allowOverrideCury, Selector<Customer.customerClassID, CustomerClass.allowOverrideCury>>))]
  [PXUIField(DisplayName = "Enable Currency Override")]
  public override bool? AllowOverrideCury { get; set; }

  /// <summary>
  /// If set to <see langword="true" />, indicates that the currency rate
  /// for customer documents (which is calculated by the system
  /// from the currency rate history) can be overridden by a user
  /// during document entry.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Selector<Customer.customerClassID, CustomerClass.allowOverrideRate>))]
  [PXUIField(DisplayName = "Enable Rate Override")]
  public override bool? AllowOverrideRate { get; set; }

  /// <summary>
  /// The account that is used to process the amounts of
  /// cash discount taken by the customer.
  /// </summary>
  [PXDefault(typeof (Search<CustomerClass.discTakenAcctID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Customer.FK.CashDiscountAccount))]
  public virtual int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  /// <summary>
  /// The subaccount that is used to process the amounts of
  /// cash discount taken by the customer.
  /// </summary>
  [PXDefault(typeof (Search<CustomerClass.discTakenSubID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [SubAccount(typeof (Customer.discTakenAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Customer.discTakenSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> that serves
  /// as the default value of the <see cref="P:PX.Objects.AR.ARRegister.ARAccountID" />
  /// field for the prepayment documents.
  /// </summary>
  [Account]
  [PXDefault(typeof (Search<CustomerClass.prepaymentAcctID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXForeignReference(typeof (Customer.FK.PrepaymentAccount))]
  public virtual int? PrepaymentAcctID
  {
    get => this._PrepaymentAcctID;
    set => this._PrepaymentAcctID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see>
  /// that serves as the default value of the <see cref="P:PX.Objects.AR.ARRegister.ARSubID" />
  /// field for the prepayment documents.
  /// </summary>
  [SubAccount(typeof (Customer.prepaymentAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Customer.prepaymentSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  [PXDefault(typeof (Search<CustomerClass.prepaymentSubID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual int? PrepaymentSubID
  {
    get => this._PrepaymentSubID;
    set => this._PrepaymentSubID = value;
  }

  [Account]
  public virtual int? COGSAcctID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the payments of the customer
  /// should be automatically applied to the open invoices upon release.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<CustomerClass.autoApplyPayments, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Auto-Apply Payments")]
  public virtual bool? AutoApplyPayments
  {
    get => this._AutoApplyPayments;
    set => this._AutoApplyPayments = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that customer
  /// statements should be printed for the customer.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Print Statements")]
  [PXDefault(false, typeof (Search<CustomerClass.printStatements, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? PrintStatements
  {
    get => this._PrintStatements;
    set => this._PrintStatements = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that customer
  /// statements should be generated for the customer in
  /// multi-currency format.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Multi-Currency Statements")]
  [PXDefault(false, typeof (Search<CustomerClass.printCuryStatements, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? PrintCuryStatements
  {
    get => this._PrintCuryStatements;
    set => this._PrintCuryStatements = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search<CustomerClass.sendStatementByEmail, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Send Statements by Email")]
  public virtual bool? SendStatementByEmail { get; set; }

  /// <summary>
  /// The type of credit verification for the customer.
  /// The list of possible values of the field is determined
  /// by <see cref="T:PX.Objects.AR.CreditRuleAttribute" />.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PX.Objects.AR.CreditRule]
  [PXDefault("N", typeof (Search<CustomerClass.creditRule, Where<Current<PX.Objects.CR.BAccount.isBranch>, Equal<False>, And<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>>))]
  [PXUIField(DisplayName = "Credit Verification")]
  public virtual string CreditRule
  {
    get => this._CreditRule;
    set => this._CreditRule = value;
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.Customer.CreditRule" /> enables verification by credit limit,
  /// this field determines the maximum amount of credit allowed for the customer.
  /// </summary>
  [PXDBBaseCury(null, typeof (Customer.baseCuryID))]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<CustomerClass.creditLimit, Where<Current<PX.Objects.CR.BAccount.isBranch>, Equal<False>, And<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>>))]
  [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
  [PXUIField(DisplayName = "Credit Limit")]
  public virtual Decimal? CreditLimit
  {
    get => this._CreditLimit;
    set => this._CreditLimit = value;
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.Customer.CreditRule" /> enables verification by days past due,
  /// this field determines the maximum number of credit days past due
  /// allowed for the customer. The actual number of days past due is
  /// calculated from the due date of the earliest open customer invoice
  /// (which is specified by <see cref="P:PX.Objects.AR.ARBalances.OldInvoiceDate" />).
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 3650)]
  [PXUIField(DisplayName = "Credit Days Past Due")]
  [PXDefault(TypeCode.Int16, "0", typeof (Search<CustomerClass.creditDaysPastDue, Where<Current<PX.Objects.CR.BAccount.isBranch>, Equal<False>, And<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>>))]
  public virtual short? CreditDaysPastDue
  {
    get => this._CreditDaysPastDue;
    set => this._CreditDaysPastDue = value;
  }

  /// <summary>
  /// The type of customer statements generated for the customer.
  /// The list of possible values of the field is determined by
  /// <see cref="T:PX.Objects.AR.StatementTypeAttribute" />.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<CustomerClass.statementType, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [LabelList(typeof (ARStatementType))]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType
  {
    get => this._StatementType;
    set => this._StatementType = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARStatementCycle">statement cycle</see>
  /// to which the customer is assigned.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Statement Cycle ID")]
  [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
  [PXDefault(typeof (Search<CustomerClass.statementCycleId, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual string StatementCycleId
  {
    get => this._StatementCycleId;
    set => this._StatementCycleId = value;
  }

  /// <summary>
  /// The date when the statements were last generated for the customer.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Statement Last Date", Enabled = false)]
  public virtual DateTime? StatementLastDate
  {
    get => this._StatementLastDate;
    set => this._StatementLastDate = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that small balance
  /// write-offs are allowed for the customer.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<CustomerClass.smallBalanceAllow, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Enable Write-Offs")]
  public virtual bool? SmallBalanceAllow
  {
    get => this._SmallBalanceAllow;
    set => this._SmallBalanceAllow = value;
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.Customer.SmallBalanceAllow" /> is set to <c>true</c>, the
  /// field determines the maximum small balance write-off limit for
  /// customer documents.
  /// </summary>
  [PXDBCury(typeof (Customer.curyID), MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<CustomerClass.smallBalanceLimit, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
  [PXUIField(DisplayName = "Write-Off Limit", Enabled = false)]
  public virtual Decimal? SmallBalanceLimit
  {
    get => this._SmallBalanceLimit;
    set => this._SmallBalanceLimit = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that financial charges
  /// can be calculated for the customer.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<CustomerClass.finChargeApply, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXUIField(DisplayName = "Apply Overdue Charges")]
  public virtual bool? FinChargeApply
  {
    get => this._FinChargeApply;
    set => this._FinChargeApply = value;
  }

  /// <summary>
  /// A calculated field. If set to <c>false</c>, indicates that
  /// the customer's billing address is the same as the customer's
  /// default address.
  /// The field is populated by a formula, working only in the scope of the Customers (AR303000) form.
  /// See <see cref="T:PX.Objects.AR.CustomerMaint.CustomerBillSharedAddressOverrideGraphExt" />"
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideBillAddress { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  [Obsolete("Use OverrideBillAddress instead")]
  public virtual bool? IsBillSameAsMain
  {
    get
    {
      if (!this.OverrideBillAddress.HasValue)
        return new bool?();
      bool? overrideBillAddress = this.OverrideBillAddress;
      return !overrideBillAddress.HasValue ? new bool?() : new bool?(!overrideBillAddress.GetValueOrDefault());
    }
  }

  /// <summary>
  /// A calculated field. If set to <c>false</c>, indicates that the
  /// customer's billing contact is the same as the customer's
  /// default contact.
  /// The field is populated by a formula, working only in the scope of the Customers (AR303000) form.
  /// See <see cref="T:PX.Objects.AR.CustomerMaint.CustomerBillSharedContactOverrideGraphExt" />"
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideBillContact { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  [Obsolete("Use OverrideBillContact instead")]
  public virtual bool? IsBillContSameAsMain
  {
    get
    {
      if (!this.OverrideBillContact.HasValue)
        return new bool?();
      bool? overrideBillContact = this.OverrideBillContact;
      return !overrideBillContact.HasValue ? new bool?() : new bool?(!overrideBillContact.GetValueOrDefault());
    }
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Customer Status", Required = true)]
  [PXDefault("A")]
  [CustomerStatus.List]
  public override string Status { get; set; }

  [RestrictOrganization]
  [PXUIField(DisplayName = "Restrict Visibility To", FieldClass = "VisibilityRestriction", Required = false)]
  [PXDefault(0, typeof (Select<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>), SourceField = typeof (CustomerClass.orgBAccountID))]
  public override int? COrgBAccountID { get; set; }

  /// <summary>
  /// The full business account name (as opposed to the
  /// short identifier provided by <see cref="P:PX.Objects.AR.Customer.AcctCD" />).
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataField]
  public override string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  /// <summary>
  /// The group mask of the customer. The value of the field
  /// is used for the purposes of access control.
  /// </summary>
  [PXDBGroupMask(BqlTable = typeof (Customer))]
  [PXDefault(typeof (Select<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public new virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// The identifier of the customer's default <see cref="T:PX.Objects.CA.PaymentMethod" />.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<CustomerClass.defPaymentMethodID, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  [PXSelector(typeof (Search2<PX.Objects.CA.PaymentMethod.paymentMethodID, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>, And<CustomerPaymentMethod.bAccountID, Equal<Current<Customer.bAccountID>>>>>, Where<Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, Or<Where<CustomerPaymentMethod.pMInstanceID, IsNotNull>>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Default Payment Method", Enabled = false)]
  public virtual string DefPaymentMethodID
  {
    get => this._DefPaymentMethodID;
    set => this._DefPaymentMethodID = value;
  }

  /// <summary>An obsolete field.</summary>
  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public virtual string CCProcessingID { get; set; }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Objects.CA.PMInstance" /> object
  /// associated with the customer's <see cref="P:PX.Objects.AR.Customer.DefPaymentMethodID">
  /// default payment method</see>.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CA.PaymentMethod.pMInstanceID, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<Customer.defPaymentMethodID>>>>))]
  [PXDBInt]
  [PXDBChildIdentity(typeof (CustomerPaymentMethod.pMInstanceID))]
  public virtual int? DefPMInstanceID
  {
    get => this._DefPMInstanceID;
    set => this._DefPMInstanceID = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that invoices
  /// should be printed for the customer.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Print Invoices")]
  [PXDefault(false, typeof (Search<CustomerClass.printInvoices, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? PrintInvoices
  {
    get => this._PrintInvoices;
    set => this._PrintInvoices = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that invoices
  /// should be sent to the customer by email.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Send Invoices by Email")]
  [PXDefault(false, typeof (Search<CustomerClass.mailInvoices, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? MailInvoices
  {
    get => this._MailInvoices;
    set => this._MailInvoices = value;
  }

  /// <summary>
  /// The unique identifier of the <see cref="T:PX.Data.Note">note</see>
  /// associated with the customer account.
  /// </summary>
  [PXSearchable(1411, "Customer: {0}", new System.Type[] {typeof (Customer.acctName)}, new System.Type[] {typeof (Customer.acctName), typeof (Customer.acctCD), typeof (Customer.acctName), typeof (Customer.acctCD), typeof (Customer.defContactID), typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Contact.phone2), typeof (PX.Objects.CR.Contact.phone3), typeof (PX.Objects.CR.Contact.webSite)}, NumberFields = new System.Type[] {typeof (Customer.acctCD)}, Line1Format = "{0}{2}{3}{4}", Line1Fields = new System.Type[] {typeof (Customer.acctCD), typeof (Customer.defContactID), typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Contact.eMail)}, Line2Format = "{1}{2}{3}", Line2Fields = new System.Type[] {typeof (Customer.defAddressID), typeof (PX.Objects.CR.Address.displayName), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.state)}, SelectForFastIndexing = typeof (Select2<Customer, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<Customer.defContactID>>>>))]
  [PXUniqueNote(DescriptionField = typeof (Customer.acctCD), Selector = typeof (Customer.acctCD), ActivitiesCountByParent = true, ShowInReferenceSelector = true, PopupTextEnabled = true)]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that dunning letters
  /// should be printed for the customer.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Print Dunning Letters")]
  [PXDefault(false, typeof (Search<CustomerClass.printDunningLetters, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? PrintDunningLetters
  {
    get => this._PrintDunningLetters;
    set => this._PrintDunningLetters = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that dunning letters
  /// should be sent to the customer by email.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Send Dunning Letters by Email")]
  [PXDefault(false, typeof (Search<CustomerClass.mailDunningLetters, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? MailDunningLetters
  {
    get => this._MailDunningLetters;
    set => this._MailDunningLetters = value;
  }

  /// <summary>
  /// An unbound Boolean field that is provided for implementation
  /// of the <see cref="T:PX.SM.IIncludable" /> interface, which is
  /// a part of the row-level security mechanism of Acumatica.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the customer is a child
  /// with the selected 'Share Credit Policy' option
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<Customer.parentBAccountID, IsNotNull, And<Customer.sharedCreditPolicy, Equal<True>, And<FeatureInstalled<FeaturesSet.parentChildAccount>>>>, True, False>))]
  public virtual bool? SharedCreditChild { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the customer is a child
  /// with the selected 'Consolidate Statements' option
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<Customer.parentBAccountID, IsNotNull, And<Customer.consolidateStatements, Equal<True>, And<FeatureInstalled<FeaturesSet.parentChildAccount>>>>, True, False>))]
  public virtual bool? StatementChild { get; set; }

  /// <summary>
  /// A read-only equivalent of the <see cref="P:PX.Objects.AR.Customer.CustomerClassID" />
  /// field, which is used for internal purposes.
  /// </summary>
  [Obsolete("This field should not be used explicitly over Customer dac. Use the CustomerClassID or BAccountClassID field instead.")]
  public override string ClassID => this.CustomerClassID;

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.BAccount.classID" /> field, which is used for internal purposes.
  /// </summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.classID))]
  [PXUIField(DisplayName = "Business Account Class", Visible = false, FieldClass = "CRM")]
  [PXDefault(typeof (CRSetup.defaultCustomerClassID))]
  public virtual string BAccountClassID { get; set; }

  /// <summary>The name of the customer's locale.</summary>
  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXDefault(typeof (Search<CustomerClass.localeName, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public override string LocaleName { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false, typeof (Select<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? RetainageApply { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<Customer.retainageApply>))]
  public virtual Decimal? RetainagePct { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false, typeof (Select<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Suggest Related Items")]
  public virtual bool? SuggestRelatedItems { get; set; }

  /// <summary>
  /// The customer kind, indicating whether the customer is an individual (I) or an organization (O).
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("I")]
  [PXUIField(DisplayName = "Customer Category")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.commerceB2B>))]
  public virtual string CustomerCategory { get; set; }

  /// <summary>
  /// The company codes for which the customer is created in the external exemption certificate management system.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Company Code")]
  public virtual string ECMCompanyCode { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the customer record in the external exemption certificate management (ECM) system is up-to-date.
  /// If the value is <c>false</c>, the customer record has been updated in Acumatica ERP since this record was created with the ECM system.
  /// The record should be updated in the external ECM system.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsECMValid { get; set; }

  public new class PK : PrimaryKeyOf<Customer>.By<Customer.bAccountID>
  {
    public static Customer Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (Customer) PrimaryKeyOf<Customer>.By<Customer.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<Customer>.By<Customer.acctCD>
  {
    public static Customer Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (Customer) PrimaryKeyOf<Customer>.By<Customer.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class CustomerClass : 
      PrimaryKeyOf<CustomerClass>.By<CustomerClass.customerClassID>.ForeignKeyOf<Customer>.By<Customer.customerClassID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<BAccountR.bAccountID>.ForeignKeyOf<Customer>.By<Customer.parentBAccountID>
    {
    }

    public class StatementCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<Customer>.By<Customer.statementCustomerID>
    {
    }

    public class SharedCreditCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<Customer>.By<Customer.sharedCreditCustomerID>
    {
    }

    public class DefaultBillAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Customer>.By<Customer.defBillAddressID>
    {
    }

    public class DefaultBillContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Customer>.By<Customer.defBillContactID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<Customer>.By<Customer.termsID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Customer>.By<Customer.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<Customer>.By<Customer.curyRateTypeID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Customer>.By<Customer.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Customer>.By<Customer.discTakenSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Customer>.By<Customer.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Customer>.By<Customer.prepaymentSubID>
    {
    }

    public class StatementCycle : 
      PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>.ForeignKeyOf<Customer>.By<Customer.statementCycleId>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Customer>.By<Customer.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Customer>.By<Customer.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<Customer>.By<Customer.bAccountID, Customer.defLocationID>
    {
    }

    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Customer>.By<Customer.primaryContactID>
    {
    }

    public class DefaultPaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<Customer>.By<Customer.defPaymentMethodID>
    {
    }

    public class DefaultPMInstance : 
      PrimaryKeyOf<PMInstance>.By<PMInstance.pMInstanceID>.ForeignKeyOf<Customer>.By<Customer.defPMInstanceID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.acctCD>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.type>
  {
  }

  public new abstract class isCustomerOrCombined : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.isCustomerOrCombined>
  {
  }

  public new abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.parentBAccountID>
  {
  }

  public new abstract class consolidateToParent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.consolidateToParent>
  {
  }

  public abstract class consolidateStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.consolidateStatements>
  {
  }

  public abstract class sharedCreditPolicy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.sharedCreditPolicy>
  {
  }

  public new abstract class consolidatingBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Customer.consolidatingBAccountID>
  {
  }

  public abstract class statementCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Customer.statementCustomerID>
  {
  }

  public abstract class sharedCreditCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Customer.sharedCreditCustomerID>
  {
  }

  public abstract class customerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.customerClassID>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public abstract class languageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.languageID>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public abstract class defSOAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defSOAddressID>
  {
  }

  public abstract class defBillAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defBillAddressID>
  {
  }

  public abstract class defBillContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defBillContactID>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public abstract class baseBillContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.baseBillContactID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.termsID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.curyID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Customer.curyRateTypeID>
  {
  }

  public new abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.allowOverrideCury>
  {
  }

  public new abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.allowOverrideRate>
  {
  }

  public abstract class discTakenAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.discTakenAcctID>
  {
  }

  public abstract class discTakenSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.discTakenSubID>
  {
  }

  public abstract class prepaymentAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.prepaymentAcctID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.prepaymentSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.cOGSAcctID>
  {
  }

  public abstract class autoApplyPayments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.autoApplyPayments>
  {
  }

  public abstract class printStatements : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.printStatements>
  {
  }

  public abstract class printCuryStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.printCuryStatements>
  {
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that customer
  /// statements should be sent to the customer by email.
  /// </summary>
  public abstract class sendStatementByEmail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.sendStatementByEmail>
  {
  }

  public abstract class creditRule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.creditRule>
  {
  }

  public abstract class creditLimit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Customer.creditLimit>
  {
  }

  public abstract class creditDaysPastDue : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    Customer.creditDaysPastDue>
  {
  }

  public abstract class statementType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.statementType>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Customer.statementCycleId>
  {
  }

  public abstract class statementLastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Customer.statementLastDate>
  {
  }

  public abstract class smallBalanceAllow : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.smallBalanceAllow>
  {
  }

  public abstract class smallBalanceLimit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Customer.smallBalanceLimit>
  {
  }

  public abstract class finChargeApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.finChargeApply>
  {
  }

  public abstract class overrideBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.overrideBillAddress>
  {
  }

  [Obsolete("Use OverrideBillAddress instead")]
  public abstract class isBillSameAsMain : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.isBillSameAsMain>
  {
  }

  public abstract class overrideBillContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.overrideBillContact>
  {
  }

  [Obsolete("Use OverrideBillContact instead")]
  public abstract class isBillContSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.isBillContSameAsMain>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defLocationID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defAddressID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defContactID>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.status>
  {
  }

  public new abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.cOrgBAccountID>
  {
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.baseCuryID>
  {
  }

  public new abstract class primaryContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.primaryContactID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.acctName>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Customer.groupMask>
  {
  }

  public abstract class defPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Customer.defPaymentMethodID>
  {
  }

  [Obsolete("This field is not used anymore and will be removed in Acumatica 8.0.")]
  public abstract class cCProcessingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.cCProcessingID>
  {
  }

  public abstract class defPMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defPMInstanceID>
  {
  }

  public abstract class printInvoices : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.printInvoices>
  {
  }

  public abstract class mailInvoices : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.mailInvoices>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Customer.noteID>
  {
  }

  public abstract class printDunningLetters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.printDunningLetters>
  {
  }

  public abstract class mailDunningLetters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.mailDunningLetters>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.included>
  {
  }

  public abstract class sharedCreditChild : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.sharedCreditChild>
  {
  }

  public abstract class statementChild : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.statementChild>
  {
  }

  public abstract class bAccountClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.bAccountClassID>
  {
  }

  public new abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.localeName>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.retainageApply>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Customer.retainagePct>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.paymentsByLinesAllowed>
  {
  }

  public abstract class suggestRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.suggestRelatedItems>
  {
  }

  public abstract class customerCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Customer.customerCategory>
  {
  }

  public abstract class eCMCompanyCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.eCMCompanyCode>
  {
  }

  public abstract class isECMValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.isECMValid>
  {
  }
}
