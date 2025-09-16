// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.PaymentProcessor.Data;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Represents Payments, Debit Adjustments, Prepayments, Refunds and Voided Payments in Accounts Payable module.
/// This DAC extends <see cref="T:PX.Objects.AP.APRegister" /> with the fields specific to the documents of the above types.
/// </summary>
[PXTable]
[PXSubstitute(GraphType = typeof (APPaymentEntry))]
[PXPrimaryGraph(new System.Type[] {typeof (APQuickCheckEntry), typeof (APPaymentEntry)}, new System.Type[] {typeof (Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APPayment.docType>>, And<APQuickCheck.refNbr, Equal<Current<APPayment.refNbr>>>>>), typeof (Select<APPayment, Where<APPayment.docType, Equal<Current<APPayment.docType>>, And<APPayment.refNbr, Equal<Current<APPayment.refNbr>>>>>)})]
[PXCacheName("Payment")]
[PXGroupMask(typeof (InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class APPayment : 
  APRegister,
  IInvoice,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  IPrintCheckControlable,
  IAssign,
  IApprovable,
  IApprovalDescription,
  IReserved
{
  protected int? _RemitAddressID;
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected int? _CashAccountID;
  protected System.DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected int? _StubCntr;
  protected int? _BillCntr;
  protected int? _ChargeCntr;
  protected Decimal? _CuryUnappliedBal;
  protected Decimal? _UnappliedBal;
  protected Decimal? _CuryApplAmt;
  protected Decimal? _ApplAmt;
  protected bool? _Cleared;
  protected System.DateTime? _ClearDate;
  private bool? _printCheck;
  protected string _DrCr;
  protected string _AmountToWords;
  protected int? _CARefTranAccountID;
  protected long? _CARefTranID;
  protected int? _CARefSplitLineNbr;
  protected bool? _DepositAsBatch;
  protected System.DateTime? _DepositAfter;
  protected bool? _Deposited;
  protected System.DateTime? _DepositDate;
  protected string _DepositType;
  protected string _DepositNbr;

  /// <summary>Type of the document.</summary>
  /// <value>
  /// Possible values are: "CHK" - Payment, "ADR" - Debit Adjustment,
  /// "PPM" - Prepayment, "REF" - Refund, "VCK" - Voided Payment
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [APPaymentType.List]
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
  [APPaymentType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegisterAlias.docType>, And<APPayment.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<Vendor, On<APRegisterAlias.vendorID, Equal<Vendor.bAccountID>>, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>>>>, Where<APRegisterAlias.docType, Equal<Current<APPayment.docType>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  [APPaymentType.Numbering]
  [PXFieldDescription]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>Remittance address for the document.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.VRemitAddressID">remittance address</see> of the vendor.
  /// </value>
  [PXDBInt]
  [APAddress(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.vRemitAddressID>, PX.Data.And<Where<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APAddress, On<APAddress.vendorID, Equal<PX.Objects.CR.Address.bAccountID>, And<APAddress.vendorAddressID, Equal<PX.Objects.CR.Address.addressID>, And<APAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<APAddress.isDefaultAddress, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APPayment.vendorLocationID>>>>>))]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  /// <summary>Remittance contact for the document.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.VRemitContactID">remittance contact</see> of the vendor.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (APContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Remittance Contact", Visible = false)]
  [APContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.vRemitContactID>, PX.Data.And<Where<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APContact, On<APContact.vendorID, Equal<PX.Objects.CR.Contact.bAccountID>, And<APContact.vendorContactID, Equal<PX.Objects.CR.Contact.contactID>, And<APContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<APContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APPayment.vendorLocationID>>>>>))]
  public virtual int? RemitContactID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
  /// The field must be located before the <see cref="P:PX.Objects.AP.APPayment.APAccountID" /> and <see cref="P:PX.Objects.AP.APPayment.APSubID" /> fields for correct Restriction Groups operation.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APPayment.branchID>, IsPending>, Null, Case<Where<APPayment.vendorLocationID, PX.Data.IsNotNull, And<Selector<APPayment.vendorLocationID, PX.Objects.CR.Location.vBranchID>, PX.Data.IsNotNull>>, Selector<APPayment.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<Current2<APPayment.branchID>, PX.Data.IsNotNull>, Current2<APPayment.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDefault]
  [Account(typeof (APRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP")]
  public override int? APAccountID
  {
    get => this._APAccountID;
    set => this._APAccountID = value;
  }

  [PXDefault]
  [SubAccount(typeof (APRegister.aPAccountID), typeof (APRegister.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible)]
  public override int? APSubID
  {
    get => this._APSubID;
    set => this._APSubID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see> used for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID">PaymentMethod.PaymentMethodID</see> field.
  /// Defaults to the payment method associated with the vendor location.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.paymentMethodID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APPayment.vendorLocationID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, And<Current<APPayment.docType>, In3<APDocType.prepayment, APDocType.check, APDocType.voidCheck>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APPayment.paymentMethodID>.IsRelatedTo<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.CashAccount">cash account</see> associated with the <see cref="P:PX.Objects.AP.APPayment.PaymentMethodID">payment method</see>.
  /// The field is irrelevant for debit adjustments.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID">CashAccount.CashAccountID</see> field.
  /// Defaults to the cash account associated with the payment method and location.
  /// </value>
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.CR.Location.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CR.Location.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APPayment.vendorLocationID>>, And<PX.Objects.CR.Location.vPaymentMethodID, Equal<Current<APPayment.paymentMethodID>>>>>>, Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<APPayment.paymentMethodID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<APPayment.branchID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [CashAccount(typeof (APPayment.branchID), typeof (Search<PX.Objects.CA.CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And2<Where<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, Or<Current<APPayment.docType>, In3<APDocType.refund, APDocType.voidRefund>>>, And<PX.Objects.CA.CashAccount.cashAccountID, In2<Search<PaymentMethodAccount.cashAccountID, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<APPayment.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>>>>), Visibility = PXUIVisibility.Visible, SuppressCurrencyValidation = true)]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBInt]
  public int? JointPayeeID { get; set; }

  /// <summary>
  /// A payment reference number, which can be a system-generated number or an external reference number
  /// (such as a wire transfer number or a bank check number) entered manually.
  /// Irrelevant for Debit Adjustments.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Ref.", Visibility = PXUIVisibility.SelectorVisible)]
  [PaymentRef(typeof (APPayment.cashAccountID), typeof (APPayment.paymentMethodID), typeof (APPayment.stubCntr))]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>The date when the payment is applied.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Application Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of payment application.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.AP.APPayment.AdjDate" /> field, but can be overriden manually.
  /// </value>
  [PXUIField(DisplayName = "Application Period", Visibility = PXUIVisibility.SelectorVisible)]
  [APOpenPeriod(typeof (APPayment.adjDate), typeof (APPayment.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, null, typeof (APPayment.adjTranPeriodID), IsHeader = true)]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of payment application.
  /// </summary>
  /// <value>
  /// Unlike the <see cref="P:PX.Objects.AP.APPayment.AdjFinPeriodID" />, the value of this field is determined solely by the <see cref="P:PX.Objects.AP.APPayment.AdjDate" /> field and can't be overriden.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  /// <summary>The date of the payment document.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Date", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public override System.DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the document corresponding to the <see cref="P:PX.Objects.AP.APPayment.DocDate">payment date</see>.
  /// </summary>
  /// <value>
  /// Unlike the <see cref="P:PX.Objects.AP.APPayment.FinPeriodID" />, the value of this field is determined solely by the <see cref="P:PX.Objects.AP.APPayment.AdjDate" /> field and can't be overriden.
  /// </value>
  [PeriodID(null, null, null, true)]
  public override string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the document.
  /// </summary>
  /// <value>
  /// The value of this field is determined by the <see cref="P:PX.Objects.AP.APPayment.DocDate" /> field, but can be overriden manually.
  /// </value>
  [APOpenPeriod(typeof (APPayment.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, new System.Type[] {typeof (PeriodKeyProviderBase.SourceSpecification<APPayment.branchID, True>), typeof (PeriodKeyProviderBase.SourceSpecification<APPayment.cashAccountID, Selector<APPayment.cashAccountID, PX.Objects.CA.CashAccount.branchID>, False>)}, typeof (APPayment.tranPeriodID))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Period", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// The total payment amount that should be applied to the documents.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.origDocAmt))]
  [PXUIField(DisplayName = "Payment Amount", Visibility = PXUIVisibility.SelectorVisible)]
  public override Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  /// <summary>
  /// The counter of the related pay stubs.
  /// Note that this field is used internally for numbering purposes and its value may not reflect the actual count of the pay stubs.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? StubCntr
  {
    get => this._StubCntr;
    set => this._StubCntr = value;
  }

  /// <summary>
  /// The counter of the related bills.
  /// Note that this field is used internally for numbering purposes and its value may not reflect the actual count of the bills.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BillCntr
  {
    get => this._BillCntr;
    set => this._BillCntr = value;
  }

  /// <summary>
  /// The counter of the related charge entries.
  /// Note that this field is used internally for numbering purposes and its value may not reflect the actual count of the charges.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  /// <summary>
  /// The balance that has not been applied. This will be a nonzero value if the payment amount is not equal to a document’s total amount.
  /// Checks shall always have a zero unapplied balance.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.unappliedBal))]
  [PXUIField(DisplayName = "Unapplied Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXFormula(typeof (Sub<APPayment.curyDocBal, APPayment.curyApplAmt>))]
  public virtual Decimal? CuryUnappliedBal
  {
    get => this._CuryUnappliedBal;
    set => this._CuryUnappliedBal = value;
  }

  /// <summary>
  /// The balance that has not been applied. This will be a nonzero value if the payment amount is not equal to a document’s total amount.
  /// Checks shall always have a zero unapplied balance.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? UnappliedBal
  {
    get => this._UnappliedBal;
    set => this._UnappliedBal = value;
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="!:CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.initDocBal))]
  [PXUIField(DisplayName = "Unapplied Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXUIVerify(typeof (Where<APPayment.hold, Equal<True>, Or<APPayment.openDoc, NotEqual<True>, Or<APPayment.isMigratedRecord, NotEqual<True>, Or2<Where<APPayment.voidAppl, Equal<True>, And<APPayment.curyInitDocBal, LessEqual<decimal0>, And<APPayment.curyInitDocBal, GreaterEqual<APPayment.curyOrigDocAmt>>>>, PX.Data.Or<Where<APPayment.voidAppl, NotEqual<True>, And<APPayment.curyInitDocBal, GreaterEqual<decimal0>, And<APPayment.curyInitDocBal, LessEqual<APPayment.curyOrigDocAmt>>>>>>>>>), PXErrorLevel.Error, "Migrated balance cannot be less than zero or greater than the document amount.", new System.Type[] {}, CheckOnInserted = false, CheckOnRowSelected = false, CheckOnVerify = false, CheckOnRowPersisting = true)]
  public override Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// The amount to be applied on the application date.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.applAmt))]
  [PXUIField(DisplayName = "Application Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryApplAmt
  {
    get => this._CuryApplAmt;
    set => this._CuryApplAmt = value;
  }

  /// <summary>
  /// The amount to be applied on the application date.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? ApplAmt
  {
    get => this._ApplAmt;
    set => this._ApplAmt = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the check was cleared in the process of reconciliation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  /// <summary>The date when the check was cleared.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Voided", Visibility = PXUIVisibility.Visible)]
  [PXDefault(false)]
  public override bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that a check must be printed for the payment represented by this record.
  /// </summary>
  [PXDBBool]
  [FormulaDefault(typeof (IsNull<IIf<Where<APPayment.isMigratedRecord, Equal<True>>, False, Selector<APPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>>, False>))]
  [PXUIField(DisplayName = "Print Check")]
  public virtual bool? PrintCheck
  {
    get => this._printCheck;
    set => this._printCheck = value;
  }

  [PXString]
  public virtual string BatchPaymentRefNbr { get; set; }

  /// <summary>
  /// Indicates that this check under printing processing to prevent update <see cref="T:PX.Objects.CA.CashAccountCheck" /> table by <see cref="T:PX.Objects.AP.PaymentRefAttribute" /> /&gt;
  /// </summary>
  [PXBool]
  public virtual bool? IsPrintingProcess { get; set; }

  /// <summary>
  /// Indicates that this check under release processing to prevent the question about the saving of the last check number by <see cref="T:PX.Objects.AP.PaymentRefAttribute" /> /&gt;
  /// </summary>
  [PXBool]
  public virtual bool? IsReleaseCheckProcess { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (IIf<Where<APPayment.printCheck, NotEqual<True>, And<Selector<APPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>, Equal<True>, Or<Selector<APPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>, NotEqual<True>>>>, True, False>))]
  public override bool? Printed
  {
    get => this._Printed;
    set => this._Printed = value;
  }

  /// <summary>
  /// When <c>true</c> indicates that the document is Voided Payment.
  /// </summary>
  /// <value>
  /// Setting this field to <c>true</c> will change the <see cref="P:PX.Objects.AP.APPayment.DocType">type of the document</see> to Voided Payment (<c>"VCK"</c>).
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Void Application", Visibility = PXUIVisibility.Visible)]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPayment.docType)})] get
    {
      return new bool?(APPaymentType.VoidAppl(this._DocType));
    }
    set
    {
      if (!value.Value || APPaymentType.VoidAppl(this.DocType))
        return;
      this._DocType = APPaymentType.GetVoidingAPDocType(this.DocType);
    }
  }

  /// <summary>
  /// Read-only field indicating whether the document can have balance.
  /// </summary>
  /// <value>
  /// <c>true</c> for Debit Adjustments, Prepayments, Cash Purchases and Voided Payments. <c>false</c> for other documents.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Can Have Balance", Visibility = PXUIVisibility.Visible)]
  [PXDefault(false)]
  public virtual bool? CanHaveBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPayment.docType)})] get
    {
      return new bool?(APPaymentType.CanHaveBalance(this._DocType));
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating whether the document is of debit or credit type.
  /// The value of this field is based solely on the <see cref="P:PX.Objects.AP.APPayment.DocType" /> field.
  /// </summary>
  /// <value>
  /// Possible values are <c>"D"</c> (for Refund and Voided Cash Purchase)
  /// and <c>"C"</c> (for Payment, Voided Payment, Debit Adjustment, Prepayment and Cash Purchase).
  /// </value>
  [PXString(1, IsFixed = true)]
  public string DrCr
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPayment.docType)})] get
    {
      return APPaymentType.DrCr(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Identifier of the related <see cref="T:PX.Objects.CA.CATran">transaction in Cash Management module</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PX.Objects.CA.CATran.CATranID">CATran.CATranID</see> field.
  /// </value>
  [PXDBLong]
  [APCashTranID]
  public virtual long? CATranID { get; set; }

  /// <summary>
  /// Returns the word representation of the amount of the document. (English only)
  /// </summary>
  [ToWords(typeof (APPayment.curyOrigDocAmt))]
  public virtual string AmountToWords
  {
    get => this._AmountToWords;
    set => this._AmountToWords = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  /// <summary>
  /// Together with the <see cref="P:PX.Objects.AP.APPayment.CARefTranID" /> and <see cref="P:PX.Objects.AP.APPayment.CARefSplitLineNbr" /> the field is used to link the payment
  /// to the appropriate Cash Management entities (See <see cref="T:PX.Objects.CA.CATran" />, <see cref="T:PX.Objects.CA.CASplit" />, <see cref="T:PX.Objects.CA.CashAccount" />)
  /// during the payments reclassification process.
  /// </summary>
  public virtual int? CARefTranAccountID
  {
    get => this._CARefTranAccountID;
    set => this._CARefTranAccountID = value;
  }

  /// <summary>
  /// Together with the <see cref="P:PX.Objects.AP.APPayment.CARefTranAccountID" /> and <see cref="P:PX.Objects.AP.APPayment.CARefSplitLineNbr" /> the field is used to link the payment
  /// to the appropriate Cash Management entities (See <see cref="T:PX.Objects.CA.CATran" />, <see cref="T:PX.Objects.CA.CASplit" />, <see cref="T:PX.Objects.CA.CashAccount" />)
  /// during the payments reclassification process.
  /// </summary>
  public virtual long? CARefTranID
  {
    get => this._CARefTranID;
    set => this._CARefTranID = value;
  }

  /// <summary>
  /// Together with the <see cref="P:PX.Objects.AP.APPayment.CARefTranID" /> and <see cref="P:PX.Objects.AP.APPayment.CARefTranAccountID" /> the field is used to link the payment
  /// to the appropriate Cash Management entities (See <see cref="T:PX.Objects.CA.CATran" />, <see cref="T:PX.Objects.CA.CASplit" />, <see cref="T:PX.Objects.CA.CashAccount" />)
  /// during the payments reclassification process.
  /// </summary>
  public virtual int? CARefSplitLineNbr
  {
    get => this._CARefSplitLineNbr;
    set => this._CARefSplitLineNbr = value;
  }

  /// <summary>
  /// Doesn't bear any meaning in the context of <see cref="T:PX.Objects.AP.APPayment" /> records.
  /// </summary>
  public virtual System.DateTime? DiscDate
  {
    get => new System.DateTime?(new System.DateTime());
    set
    {
    }
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the payment can be included in a deposit.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Batch Deposit", Enabled = false)]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<APPayment.cashAccountID>>>>))]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  /// <summary>
  /// Informational date specified on the document, which is the source of the deposit.
  /// </summary>
  /// <value>
  /// Normally defaults to <see cref="P:PX.Objects.AP.APPayment.AdjDate" /> for payments with <see cref="P:PX.Objects.AP.APPayment.DepositAsBatch" /> set to <c>true</c>.
  /// </value>
  [PXDBDate]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = false)]
  public virtual System.DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  /// <summary>
  /// When equal to <c>true</c> indicates that the payment was deposited.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Deposited", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  /// <summary>The date of deposit.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CADeposit.TranDate">CADeposit.TranDate</see> field
  /// </value>
  [PXDate]
  [PXUIField(DisplayName = "Batch Deposit Date", Enabled = false)]
  public virtual System.DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  /// <summary>
  /// The type of the <see cref="T:PX.Objects.CA.CADeposit">deposit document</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CADeposit.TranType">CADeposit.TranType</see> field
  /// </value>
  [PXUIField(Enabled = false)]
  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  /// <summary>
  /// The reference number of the <see cref="T:PX.Objects.CA.CADeposit">deposit document</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CADeposit.RefNbr">CADeposit.RefNbr</see> field
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<APPayment.depositType>>>>))]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(1, "AP {0}: {1} - {3}", new System.Type[] {typeof (APPayment.docType), typeof (APPayment.refNbr), typeof (APPayment.vendorID), typeof (Vendor.acctName)}, new System.Type[] {typeof (APPayment.extRefNbr), typeof (APPayment.docDesc)}, NumberFields = new System.Type[] {typeof (APPayment.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (APPayment.docDate), typeof (APPayment.status), typeof (APPayment.extRefNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (APPayment.docDesc)}, WhereConstraint = typeof (Where<APRegister.docType, NotEqual<APDocType.quickCheck>, And<APRegister.docType, NotEqual<APDocType.voidQuickCheck>, And<APRegister.docType, NotEqual<APDocType.debitAdj>>>>), MatchWithJoin = typeof (InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>), SelectForFastIndexing = typeof (Select2<APPayment, InnerJoin<Vendor, On<APPayment.vendorID, Equal<Vendor.bAccountID>>>, Where<APRegister.docType, NotEqual<APDocType.quickCheck>, And<APRegister.docType, NotEqual<APDocType.voidQuickCheck>, And<APRegister.docType, NotEqual<APDocType.debitAdj>>>>>))]
  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// Status of the document. The field is calculated based on the values of status flag. It can't be changed directly.
  /// The fields tht determine status of a document are: <see cref="!:Hold" />, <see cref="!:Released" />, <see cref="P:PX.Objects.AP.APPayment.Voided" />, <see cref="!:Scheduled" />, <see cref="!:Prebooked" />, <see cref="P:PX.Objects.AP.APPayment.Printed" />
  /// </summary>
  /// <value>
  /// Possible values are: <c>"H"</c> - Hold, <c>"B"</c> - Balanced, <c>"V"</c> - Voided, <c>"S"</c> - Scheduled, <c>"N"</c> - Open, <c>"C"</c> - Closed, <c>"P"</c> - Printed, <c>"K"</c> - Prebooked.
  /// Defaults to Hold.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [APDocStatus.List]
  [PXDependsOnFields(new System.Type[] {typeof (APPayment.voided), typeof (APPayment.hold), typeof (APRegister.scheduled), typeof (APPayment.released), typeof (APPayment.printed), typeof (APRegister.prebooked), typeof (APPayment.openDoc), typeof (APPayment.printCheck), typeof (APRegister.approved), typeof (APRegister.dontApprove), typeof (APRegister.rejected), typeof (APPayment.docType)})]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The total prepayment amount that should be applied to the PO Orders.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.pOApplAmt))]
  [PXUIField(DisplayName = "Applied to Order", Visibility = PXUIVisibility.Visible, Enabled = false, Visible = false)]
  public virtual Decimal? CuryPOApplAmt { get; set; }

  /// <summary>
  /// The total prepayment amount that should be applied to the PO Orders.
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  public virtual Decimal? POApplAmt { get; set; }

  /// <summary>
  /// The total prepayment amount (unreleased documents) that should be applied to the PO Orders.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.pOApplAmt))]
  public virtual Decimal? CuryPOUnreleasedApplAmt { get; set; }

  /// <summary>
  /// The total prepayment amount (unreleased documents) that should be applied to the PO Orders.
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  public virtual Decimal? POUnreleasedApplAmt { get; set; }

  /// <summary>
  /// The total prepayment amount (released and not, request and not documents) that should be applied to the PO Orders.
  /// (Presented in the currency of the document, see <see cref="!:CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APPayment.curyInfoID), typeof (APPayment.pOApplAmt))]
  public virtual Decimal? CuryPOFullApplAmt { get; set; }

  /// <summary>
  /// The total prepayment amount (released and not, request and not documents) that should be applied to the PO Orders.
  /// </summary>
  [PX.Objects.CM.Extensions.PXBaseCury]
  public virtual Decimal? POFullApplAmt { get; set; }

  [PXBool]
  public virtual bool? IsRequestPrepayment { get; set; }

  /// <summary>Payment ID in external payment processor</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "External Payment ID", IsReadOnly = true, Visible = false)]
  public virtual string ExternalPaymentID { get; set; }

  /// <summary>External payment status</summary>
  [PXDBString(50)]
  [PaymentStatus.List]
  [PXUIField(DisplayName = "Processing Status", IsReadOnly = true, Visible = false)]
  public virtual string ExternalPaymentStatus { get; set; }

  /// <summary>External organization id</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "External Organization ID", IsReadOnly = true, Visible = false)]
  public virtual string ExternalOrganizationID { get; set; }

  /// <summary>
  /// Set to true if cancellation of payment is accepted by external payment processor.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ExternalPaymentCanceled { get; set; }

  /// <summary>
  /// Set to true if voiding of payment is accepted by external payment processor.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ExternalPaymentVoided { get; set; }

  /// <summary>
  /// Date of the document updated in external payment processor.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Update Date", IsReadOnly = true, Visible = false)]
  public virtual System.DateTime? ExternalPaymentUpdateTime { get; set; }

  /// <summary>Disbursement Type used to process the payment</summary>
  [PXDBString(25)]
  [PXUIField(DisplayName = "Disbursement Method", IsReadOnly = true, Visible = false)]
  public virtual string ExternalPaymentDisbursementType { get; set; }

  /// <summary>Date of payment sent.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Sent Date", IsReadOnly = true, Visible = false)]
  public virtual System.DateTime? ExternalPaymentSentDate { get; set; }

  /// <summary>External payment check  or card number</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Check Number", IsReadOnly = true, Visible = false)]
  public virtual 
  #nullable enable
  string? ExternalPaymentCheckNbr { get; set; }

  /// <summary>External payment check  or card number</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Card Number", IsReadOnly = true, Visible = false)]
  public virtual string? ExternalPaymentCardNbr { get; set; }

  /// <summary>External payment trace number</summary>
  [PXDBString(10)]
  [PXUIField(DisplayName = "Trace Number", IsReadOnly = true, Visible = false)]
  public virtual string? ExternalPaymentTraceNbr { get; set; }

  /// <summary>External payment Batch number</summary>
  [PXDBString(10)]
  [PXUIField(DisplayName = "Batch Number", IsReadOnly = true, Visible = false)]
  public virtual string? ExternalPaymentBatchNbr { get; set; }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  APPayment>.By<APPayment.docType, APPayment.refNbr>
  {
    public static APPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APPayment>.By<APPayment.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APPayment>.By<APPayment.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APPayment>.By<APPayment.vendorID, APPayment.vendorLocationID>
    {
    }

    public class RemittanceAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<APPayment>.By<APPayment.remitAddressID>
    {
    }

    public class RemittanceContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<APPayment>.By<APPayment.remitContactID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APPayment>.By<APPayment.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APPayment>.By<APPayment.curyID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APPayment>.By<APPayment.aPAccountID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APPayment>.By<APPayment.aPSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<APPayment>.By<APRegister.scheduleID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<APPayment>.By<APPayment.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<APPayment>.By<APPayment.cashAccountID>
    {
    }

    public class CashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<APPayment>.By<APPayment.cashAccountID, APPayment.cATranID>
    {
    }
  }

  public new class Events : PXEntityEventBase<APPayment>.Container<APPayment.Events>
  {
    public PXEntityEvent<APPayment> PrintCheck;
    public PXEntityEvent<APPayment> CancelPrintCheck;
    public PXEntityEvent<APPayment> ReleaseDocument;
    public PXEntityEvent<APPayment> VoidDocument;
    public PXEntityEvent<APPayment> CloseDocument;
    public PXEntityEvent<APPayment> OpenDocument;
    public PXEntityEvent<APPayment> ProcessDocument;
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.selected>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.refNbr>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPayment.vendorLocationID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitContactID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.branchID>
  {
  }

  public new abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.aPAccountID>
  {
  }

  public new abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.aPSubID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.cashAccountID>
  {
  }

  public abstract class jointPayeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.jointPayeeID>
  {
  }

  [Obsolete("The constructor is obsolete ater AC-81186 fix")]
  public abstract class updateNextNumber : IBqlField, IBqlOperand
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.extRefNbr>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.curyID>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.adjTranPeriodID>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.docDate>
  {
  }

  public new abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.finPeriodID>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.curyDocBal>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.origDocAmt>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.adjCntr>
  {
  }

  public abstract class stubCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.stubCntr>
  {
  }

  public abstract class billCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.billCntr>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.chargeCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPayment.curyInfoID>
  {
  }

  public abstract class curyUnappliedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyUnappliedBal>
  {
  }

  public abstract class unappliedBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.unappliedBal>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyInitDocBal>
  {
  }

  public abstract class curyApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.curyApplAmt>
  {
  }

  public abstract class applAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.applAmt>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.clearDate>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.batchNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.hold>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.voided>
  {
  }

  public new abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.vendorID_Vendor_acctName>
  {
  }

  public abstract class printCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.printCheck>
  {
  }

  public abstract class batchPaymentRefNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class isPrintingProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.isPrintingProcess>
  {
  }

  public abstract class isReleaseProcess : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.isReleaseProcess>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.printed>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.voidAppl>
  {
  }

  public abstract class canHaveBalance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.canHaveBalance>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.drCr>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPayment.cATranID>
  {
  }

  public abstract class amountToWords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.amountToWords>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.origTaxDiscAmt>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.deposited>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.depositDate>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositNbr>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.isMigratedRecord>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPayment.noteID>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.docDesc>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.status>
  {
  }

  public class SetStatusCheckAttribute : APRegister.SetStatusAttribute
  {
    protected override void StatusSet(PXCache cache, APRegister item, bool? holdVal)
    {
      base.StatusSet(cache, item, holdVal);
      if (!(item is IPrintCheckControlable checkControlable) || item.Voided.GetValueOrDefault() || item.Hold.GetValueOrDefault() || item.Scheduled.GetValueOrDefault() || item.Released.GetValueOrDefault() || item.Approved.HasValue && (item.Status == "E" || item.Status == "R" || item.Status == "K"))
        return;
      bool? nullable;
      if (item.Status == "P" && checkControlable.Printed.GetValueOrDefault())
      {
        nullable = checkControlable.PrintCheck;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          nullable = item.Approved;
          if (!nullable.GetValueOrDefault())
          {
            nullable = item.DontApprove;
            if (!nullable.GetValueOrDefault())
            {
              item.Status = "E";
              return;
            }
          }
        }
      }
      nullable = item.Printed;
      if (nullable.GetValueOrDefault())
      {
        nullable = checkControlable.PrintCheck;
        if (nullable.GetValueOrDefault())
        {
          item.Status = "P";
          return;
        }
      }
      nullable = item.Printed;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = checkControlable.PrintCheck;
        if (nullable.GetValueOrDefault())
        {
          item.Status = "G";
          return;
        }
      }
      nullable = item.Prebooked;
      if (nullable.GetValueOrDefault())
        item.Status = "K";
      else
        item.Status = "B";
    }
  }

  public abstract class curyPOApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.curyPOApplAmt>
  {
  }

  public abstract class pOApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.pOApplAmt>
  {
  }

  public abstract class curyPOUnreleasedApplAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyPOUnreleasedApplAmt>
  {
  }

  public abstract class pOUnreleasedApplAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.pOUnreleasedApplAmt>
  {
  }

  public abstract class curyPOFullApplAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyPOFullApplAmt>
  {
  }

  public abstract class pOFullApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPayment.pOFullApplAmt>
  {
  }

  public abstract class isRequestPrepayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.isRequestPrepayment>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.hasMultipleProjects>
  {
  }

  public abstract class externalPaymentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentID>
  {
  }

  public abstract class externalPaymentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentStatus>
  {
  }

  public abstract class externalOrganizationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalOrganizationID>
  {
  }

  public abstract class externalPaymentCanceled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.externalPaymentCanceled>
  {
  }

  public abstract class externalPaymentVoided : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPayment.externalPaymentVoided>
  {
  }

  public abstract class externalPaymentUpdateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPayment.externalPaymentUpdateTime>
  {
  }

  public abstract class externalPaymentDisbursementType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentDisbursementType>
  {
  }

  public abstract class externalPaymentSentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPayment.externalPaymentSentDate>
  {
  }

  public abstract class externalPaymentCheckNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentCheckNbr>
  {
  }

  public abstract class externalPaymentCardNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentCardNbr>
  {
  }

  public abstract class externalPaymentTraceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentTraceNbr>
  {
  }

  public abstract class externalPaymentBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.externalPaymentBatchNbr>
  {
  }
}
